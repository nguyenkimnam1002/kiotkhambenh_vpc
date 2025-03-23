using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.NgoaiTru
{
    public partial class ucTabNGT02K078_DSTN : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTabNGT02K078_DSTN()
        {
            InitializeComponent();
        }
        public void loadData()
        {
            btnTimKiem_Click(null, null);
        }
        
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (dtimeTu.DateTime > dtimeDen.DateTime)
            {
                MessageBox.Show("Từ ngày không thể lớn hơn Đến ngày!");
                return;
            }
            getData_table(1, null);
        }

        private void ucTabNGT02K078_DSTN_Load(object sender, EventArgs e)
        {
            DateTime dtime = Func.getSysDatetime_Short();
            dtimeTu.DateTime = dtime;// Const.sys_date;
            dtimeDen.DateTime = dtime;

            DataTable dt = new DataTable();
            // Các trạng thái khám
            dt = RequestHTTP.get_ajaxExecuteQuery("NGT02K009.RV005");
            if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
            DataRow drkt = dt.NewRow();
            drkt[0] = "0";
            drkt[1] = "--- Tất cả ---";
            dt.Rows.InsertAt(drkt, 0);
            ucTrangthai.setData(dt, 0, 1);
            ucTrangthai.lookUpEdit.Properties.ShowHeader = false;
            ucTrangthai.setColumn("col1", -1, "", 0);
            ucTrangthai.SelectIndex = 0;
            ucTrangthai.setEvent(btnTimKiem_Click);

            // Danh sách phòng khám
            dt = RequestHTTP.get_ajaxExecuteQuery("LOADPK.TIMKIEM");
            if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
            drkt = dt.NewRow();
            drkt[0] = "0";
            drkt[1] = "--- Tất cả ---";
            dt.Rows.InsertAt(drkt, 0);
            ucPhongKham.setData(dt, 0, 1);
            ucPhongKham.lookUpEdit.Properties.ShowHeader = false;
            ucPhongKham.setColumn(0, -1, "", 0);
            ucPhongKham.SelectIndex = 0;
            ucPhongKham.setEvent(btnTimKiem_Click);

            //grid
            ucGrid_DsBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_DsBN.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGrid_DsBN.setEvent(getData_table);
            //ucGrid_DsBN.setEvent_FocusedRowChanged(DsBN_Change_SelectRow); 
            ucGrid_DsBN.SetReLoadWhenFilter(true);
            ucGrid_DsBN.setNumberPerPage(new int[] { 20, 50, 100 });
            //ucGrid_DsBN.gridView.OptionsCustomization.AllowSort = false;
        }
        private void getData_table(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                //string jsonFilter = "";
                //// Lấy điều kiện filter
                //if (ucGrid_DsBN.ReLoadWhenFilter)
                //{
                //    if (ucGrid_DsBN.tableFlterColumn.Rows.Count > 0)
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsBN.tableFlterColumn);
                //}

                ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging("NGT.001", page, ucGrid_DsBN.ucPage1.getNumberPerPage(),
                    new string[] { "[0]" , "[1]" , "[2]" , "[3]" }, new string[] { dtimeTu.Text + " 00:00:00", dtimeDen.Text+ " 23:59:59", ucTrangthai.SelectValue, ucPhongKham.SelectValue }, ucGrid_DsBN.jsonFilter());
                                 

                ucGrid_DsBN.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(ds.rows);
                //{"RN": "1","KHAMBENHID": "100892","MABENHNHAN": "BN00016457","TENBENHNHAN": "NGUYỄN TRỌNG THIẾT","MA_BHYT": "HT2791702900008","NGAYSINH": "1934"
                //,"GIOITINH": "Nam","TRANGTHAIKHAMBENH": "1","ORG_NAME": "Phòng 12A: Răng Hàm Mặt (K112A)","MAHOSOBENHAN": "BA18000202","XUTRIKHAMBENHID": "0"
                //    ,"KQCLS": "1","NGAYTIEPNHAN": "05/03/2018 11:04:04","NGUOITN": "BVNT.ADMIN - Quản trị hệ thống bệnh viện mức 1"}
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                    { "RN", "TRANGTHAIKHAMBENH", "KQCLS", "MAHOSOBENHAN", "MABENHNHAN", "TENBENHNHAN"
                    , "NGAYSINH", "GIOITINH", "MA_BHYT", "NGAYTIEPNHAN", "ORG_NAME" });
                
                ucGrid_DsBN.setData(dt, ds.total, ds.page, ds.records);

                ucGrid_DsBN.setColumnAll(false);
                 
                ucGrid_DsBN.setColumn("RN", 0, "STT");
                ucGrid_DsBN.setColumn("TRANGTHAIKHAMBENH", 1, " ");
                ucGrid_DsBN.setColumn("KQCLS", 2, "CLS");
                ucGrid_DsBN.setColumn("MAHOSOBENHAN", 3, "Mã BA");
                ucGrid_DsBN.setColumn("MABENHNHAN", 4, "Mã BN");
                ucGrid_DsBN.setColumn("TENBENHNHAN", 5, "Tên Bệnh nhân");
                ucGrid_DsBN.setColumn("NGAYSINH", 6, "Ngày sinh");
                ucGrid_DsBN.setColumn("GIOITINH", 7, "Giới tính");
                ucGrid_DsBN.setColumn("MA_BHYT", 8, "Mã BHYT");
                ucGrid_DsBN.setColumn("NGAYTIEPNHAN", 9, "Ngày tiếp nhận");
                ucGrid_DsBN.setColumn("ORG_NAME", 10, "Phòng khám");


                ucGrid_DsBN.setColumnImage("TRANGTHAIKHAMBENH", new String[] { "1", "4", "9" }
                    , new String[] { "./Resources/metacontact_away.png", "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png" });

                ucGrid_DsBN.setColumnImage("KQCLS", new String[] { "1", "2" }
                   , new String[] { "./Resources/Flag_Red_New.png", "./Resources/True.png" });

                ucGrid_DsBN.gridView.BestFitColumns(true);

            }
        }

    }
}
