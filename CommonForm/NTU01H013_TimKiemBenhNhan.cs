 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;

namespace VNPT.HIS.CommonForm
{
    public partial class NTU01H013_TimKiemBenhNhan : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        string ten="-1";
        string ngaysinh="-1";
        string gioitinh="-1";
        string mabhyt="-1";
        string type="0";
        string ishenkham="0";

        string FUNC = "NTU01H002.EV007"; // hẹn khám thì = "DS.BN.HENKHAM"

        //Tìm kiếm khi Lưu BN
        public bool bLuu = false;

        public NTU01H013_TimKiemBenhNhan(string ishenkham, string ten, string ngaysinh, string gioitinh, string mabhyt, string type)
        {
            InitializeComponent();

            if (ishenkham.Trim() != "") this.ishenkham = ishenkham;            
            if (ten.Trim() != "") this.ten = ten;
            if (ngaysinh.Trim() != "") this.ngaysinh = ngaysinh;
            if (gioitinh.Trim() != "") this.gioitinh = gioitinh;
            if (mabhyt.Trim() != "") this.mabhyt = mabhyt;
            if (type.Trim() != "") this.type = type;
        }

        private void NTU01H013_TimKiemBenhNhan_Load(object sender, EventArgs e)
        {
            if (ishenkham == "1")
            {
                this.Text = "DANH SÁCH BỆNH NHÂN HẸN KHÁM";
                this.FUNC = "DS.BN.HENKHAM";
                simpleButton2.Text = "Đóng";
            }
            else
            {
                this.Text = "TÌM KIẾM BỆNH NHÂN";
                this.FUNC = "NTU01H002.EV007";
                simpleButton2.Text = "Bỏ Qua";
            }

            ucGrid_DsBN.gridView.OptionsView.ColumnAutoWidth = true;
            //ucGrid_DsBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_DsBN.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGrid_DsBN.setEvent(getData_table);
            //ucGrid_DsBN.setEvent_FocusedRowChanged(change_selectRow);
            ucGrid_DsBN.SetReLoadWhenFilter(true);
            ucGrid_DsBN.gridView.OptionsBehavior.Editable = false; // chế độ ko sửa mới cho Double click, readonly thì không
            ucGrid_DsBN.setEvent_DoubleClick(DoubleClick);
             
            getData_table(1, null);            
        }
         
        private void getData_table(object sender, EventArgs e)
        {                
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;

            try
            { 
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                int page = (int)sender;
                if (page > 0)
                {
//postData:{"func":"ajaxExecuteQueryPaging","uuid":"a01b679d-9d71-45b2-8647-629ccb3cbe54","params":["DS.BN.HENKHAM"],
//    "options":[{"name":"[0]","value":"{\"_param\":[\"902\",\"6082\",\"2\",\"7900000000\"],\"_uuid\":\"a01b679d-9d71-45b2-8647-629ccb3cbe54\",
//        \"_deptId\":\"4324\",\"ten\":\"-1\",\"ngaysinh\":\"-1\",\"gioitinh\":\"-1\",\"mabhyt\":\"-1\",\"type\":\"0\",\"ishenkham\":1}"}]}
//rows:20
//page:1
//sord:asc

                    //string jsonFilter = ""; // Lấy điều kiện filter
                    //if (ucGrid_DsBN.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DsBN.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsBN.tableFlterColumn);
                    //}

                    string json_in =
                        "{\"_param\":[\"" + Const.local_user.HOSPITAL_ID + "\",\"" + Const.local_user.USER_ID + "\",\"" + Const.local_user.USER_GROUP_ID + "\",\"" + Const.local_user.PROVINCE_ID + "\"]," +
                        "\"_uuid\":\""+Const.local_user.UUID+"\","+
                        "\"_deptId\":\"" + Const.local_khoaId + "\",\"ten\":\"" + ten + "\",\"ngaysinh\":\"" + ngaysinh + "\",\"gioitinh\":\"" + gioitinh + "\",\"mabhyt\":\"" + mabhyt + "\",\"type\":\"" + type + "\",\"ishenkham\":" + ishenkham + "}";
                    json_in = json_in.Replace("\"", "\\\"");
                    ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging(this.FUNC, page, ucGrid_DsBN.ucPage1.getNumberPerPage(), new string[] { "[0]" }, new string[] { json_in }, ucGrid_DsBN.jsonFilter());
//"RN": "1",
//"LICHHENID": "5111",
//"MABENHNHAN": "BN00059903",
//"TENBENHNHAN": "NGUYỄN VĂN CÂY",
//"MABHYT": "",
//"GIOITINH": "Nam",
//"DIACHI": "Long An",
//"MAXRECORD": "1"},{
                    DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                    ucGrid_DsBN.clearData();
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MABENHNHAN", "TENBENHNHAN", "GIOITINH", "MABHYT", "DIACHI" });
                    
                    {
                        ucGrid_DsBN.setData(dt, ds.total, ds.page, ds.records);
                        ucGrid_DsBN.setColumnAll(false); 

                        ucGrid_DsBN.setColumn("RN", 0, " ");
                        ucGrid_DsBN.setColumn("MABENHNHAN", 1, "Mã bệnh nhân");
                        ucGrid_DsBN.setColumn("TENBENHNHAN", 2, "Tên bệnh nhân");
                        ucGrid_DsBN.setColumn("GIOITINH", 3, "Giới tính");
                        ucGrid_DsBN.setColumn("MABHYT", 4, "Mã BHYT");
                        ucGrid_DsBN.setColumn("DIACHI", 5, "Địa chỉ"); 

                        ucGrid_DsBN.gridView.BestFitColumns(true);
                    }
                }
            }
            finally
            {
                //Close Wait Form
                if (existSplash) 
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        
        protected EventHandler event_Return;
        public void setEvent_Return(EventHandler eventReturn)
        {
            event_Return = eventReturn;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {// Bỏ qua
            this.Close();
            if (event_Return != null && bLuu) event_Return(null, null); // TH lưu BN: nếu bỏ qua thì vẫn lưu
        }

        private void btnChon_Click(object sender, EventArgs e)
        {// Chọn
            DataRowView drv = ucGrid_DsBN.SelectedRow;
            if (event_Return != null)
            {
                if (drv != null) event_Return(drv, null);
                else
                {
                    MessageBox.Show("Yêu cầu chọn bệnh nhân.");
                    return;
                }
            }
            this.Close();
        }
        private void DoubleClick(object sender, EventArgs e)
        {
            btnChon_Click(null, null);
        }
       
    }
}