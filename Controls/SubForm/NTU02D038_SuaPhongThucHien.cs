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

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NTU02D038_SuaPhongThucHien : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NTU02D038_SuaPhongThucHien()
        {
            InitializeComponent();
        }
        private void NTU02D038_SuaPhongThucHien_Load(object sender, EventArgs e)
        {
            ucGrid_DsBN.gridView.OptionsView.ColumnAutoWidth = true;
            ucGrid_DsBN.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGrid_DsBN.gridView.OptionsView.ShowViewCaption = false;
            ucGrid_DsBN.setEvent(getData_table);
            ucGrid_DsBN.setEvent_DoubleClick(btnLuu_Click);
            ucGrid_DsBN.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
            ucGrid_DsBN.setEvent_FocusedRowChanged(change_selectRow);


            getData_table(1, null);
        }
        string maubenhphamid = "";
        string loaiPhieu = "";
        string org_type = "";
        public void loadData(string maubenhphamid, string loaiPhieu, string org_type)
        {
            this.maubenhphamid = maubenhphamid;
            this.loaiPhieu = loaiPhieu;
            this.org_type = org_type;
        }
        private void getData_table(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging("NT.035.1", page, ucGrid_DsBN.ucPage1.getNumberPerPage(),
                    new string[] { "[0]" }, new string[] { maubenhphamid }, "");
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);
                // [{"RN": "1","ORG_ID": "6589","PARENT_ID": "6479","ORG_CODE": "GPBENH.1","ORG_NAME": "Phòng xét nghiệm giải phẫu bệnh sinh thiết"
                // ,"ORG_TYPE": "Xét nghiệm","PARENT_NAME": "Khoa Giải phẫu bệnh"}] }
                ucGrid_DsBN.clearData();
                if (dt.Rows.Count == 0)
                    dt = Func.getTableEmpty(new string[] { "ORG_CODE", "ORG_NAME", "ORG_TYPE", "PARENT_NAME" });

                ucGrid_DsBN.setData(dt, ds.total, ds.page, ds.records);
                ucGrid_DsBN.setColumnAll(false);

                ucGrid_DsBN.setColumn("PARENT_NAME", "Khoa");
                ucGrid_DsBN.setColumn("ORG_CODE", "Mã phòng");
                ucGrid_DsBN.setColumn("ORG_NAME", "Tên phòng");
                ucGrid_DsBN.setColumn("ORG_TYPE", "Loại phòng"); 
                ucGrid_DsBN.gridView.BestFitColumns(true);
            }
        }
        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
        DataRowView drv_select = null;
        private void change_selectRow(object sender, EventArgs e)
        {
            drv_select = (DataRowView)ucGrid_DsBN.gridView.GetFocusedRow();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        { 
            if (drv_select == null)
            {
                MessageBox.Show("Bạn chưa chọn phòng thực hiện");
                return;
            }
             
            string _phongid = drv_select["ORG_ID"].ToString();
            string _khoaid = drv_select["PARENT_ID"].ToString();

            //{ "func":"execute","params":["","NT.035.2"],"options":[{"name":"[0]","value":"6589"},{"name":"[1]","value":"6479"},{"name":"[2]","value":"277325"}]
            string ret = RequestHTTP.get_execute("NT.035.2", new string[] { "[0]", "[1]", "[2]" }, new string[] { _phongid, _khoaid, maubenhphamid });

            if (ret == "1")//thành công
            {
                MessageBox.Show("Sửa thành công phòng thực hiện");
                ////xu ly su kien callback
                //var objReturn = new Object();
                //objReturn.loaiPhieu = opts.loaiPhieu;
                //objReturn.msg = "Sửa thành công phòng thực hiện"; 

                if (ReturnData != null) ReturnData("Sửa thành công phòng thực hiện", null);
                this.Close();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         
    }
}