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
    public partial class NGT02K054_ChuyenYCKham : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K054_ChuyenYCKham()
        {
            InitializeComponent();
        }

        string khambenhid=""; string tiepnhanid=""; string hosobenhanid=""; string doituongbenhnhanid=""; string sqlPar = "";
        public void setData(string khambenhid, string tiepnhanid, string hosobenhanid, string doituongbenhnhanid)
        {
            this.khambenhid = khambenhid;
            this.tiepnhanid = tiepnhanid;
            this.hosobenhanid = hosobenhanid;
            this.doituongbenhnhanid = doituongbenhnhanid;

            string cauhinh = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "LOAD_YEUCAUKHAM_THEO_DT");
            if (cauhinh == "0") this.sqlPar = "0";
        }
        private void NGT02K054_ChuyenYCKham_Load(object sender, EventArgs e)
        {
            DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_YeuCauKham, sqlPar);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[1] = "-- Chọn yêu cầu khám --";
                dt.Rows.InsertAt(dr, 0);
            }
            ucYCKham.setData(dt, 0, 1);
            ucYCKham.setEvent(Change_YCKham);
            ucYCKham.SelectValue = "";
            Change_YCKham(sender, e);

            ucPhong.gridView.OptionsView.ShowAutoFilterRow = false;
            ucPhong.gridView.OptionsView.ShowViewCaption = false;
            ucPhong.setEvent(getData_table);
            ucPhong.setEvent_FocusedRowChanged(Change_PK);
            getData_table(1, null); 
        }
        private void getData_table(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ResponsList ds = RequestHTTP.getPhongKham_NGT02K054(page, ucPhong.ucPage1.getNumberPerPage(), khambenhid);

                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                ucPhong.clearData();
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TENPHONG", "TENDICHVU", "TRANGTHAI_STT" });
                 
                ucPhong.setData(dt, ds.total, ds.page, ds.records);                    
                    
                ucPhong.setColumnAll(false);                    
                ucPhong.setColumn("RN", " ");                    
                ucPhong.setColumn("TENPHONG", "Tên phòng khám");                    
                ucPhong.setColumn("TENDICHVU", "Yêu cầu khám");                    
                ucPhong.setColumn("TRANGTHAI_STT", "T.Thái");                    
                ucPhong.gridView.BestFitColumns(true);

                //ucPhong
            }
        }
        private void Change_PK(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)sender;
            if (drv != null)
            {
                ucYCKham.SelectValue = drv["DICHVUID"].ToString();

                ucPhongKhamMoi.SelectValue = drv["PHONGID"].ToString();
            } 
        }
        private void Change_YCKham(object sender, EventArgs e)
        {
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV", new string[] { "[0]" }, new string[] { ucYCKham.SelectValue }); 
                //  {"func":"ajaxExecuteQuery","params":["","NGTPK.DV"],"options":[{"name":"[0]","value":"289729"}],"uuid":"401b43ec-e50f-431b-b927-035583b00abb"}
                // [["3594","[00/01/001] Phòng khám 110"],["3595","[00/00/000] Phòng khám 111"],["3662","[00/00/000] Phòng khám nhi ngoài giờ"]]

            ucPhongKhamMoi.setData(dt, 0, 1);
            ucPhongKhamMoi.SelectIndex = 0;
        }
        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            DataRowView drv = ucPhong.SelectedRow;

            if (drv == null)
            {
                MessageBox.Show("Yêu cầu chọn phòng khám ở lưới danh sách trái.");
                return;
            }

            string dichvuidcu = drv["DICHVUID"].ToString();
            string dichvuidmoi = ucYCKham.SelectValue;
            string phongkhamidmoi = ucPhongKhamMoi.SelectValue;

            if (string.IsNullOrWhiteSpace(dichvuidmoi))
            {
                MessageBox.Show("Yêu cầu chọn yêu cầu khám mới");
                return;
            }


            if (string.IsNullOrWhiteSpace(phongkhamidmoi))
            {
                MessageBox.Show("Yêu cầu chọn phòng khám mới");
                return;
            }

            string phongkhamidcu = drv["PHONGID"].ToString();
            string trangthaistt = drv["TRANGTHAI_STTID"].ToString();
            // check doi cong kham khi da dong tien; 
            // {"func":"ajaxCALL_SP_I","params":["NGT01.DOIYCK_TIEN","3386$1$1004$1004"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}
            string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT01.DOIYCK_TIEN", this.khambenhid + "$" + this.doituongbenhnhanid + "$" + dichvuidcu + "$" + dichvuidmoi);
            if (ret != "1")
            {
                MessageBox.Show("Không đổi được yêu cầu khám do đã đóng tiền công khám. ");
                return;
            }

            //=========== check dich vu thanh toan dong thoi; 
            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_CANHBAO_KHONG_TTDT"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}
            string checkTt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH","HIS_CANHBAO_KHONG_TTDT"); 
						
            // {"func":"ajaxCALL_SP_S","params":["NTU01H001.EV018","3673"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}
            string msgCheckTt = RequestHTTP.call_ajaxCALL_SP_S_error_msg("NTU01H001.EV018", this.tiepnhanid);

            if (msgCheckTt != "")
            {
                MessageBox.Show("Các dịch vụ " + msgCheckTt + " miễn giảm thanh toán đồng thời");
                if (checkTt == "1")
                {
                    return;
                }
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn cập nhật thông tin này?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // {"func":"ajaxCALL_SP_I","params":["NGT02K054.CAPNHAT",
                // "{\"KHAMBENHID\":\"3386\",\"TIEPNHANID\":\"3673\",\"HOSOBENHANID\":\"3895\",\"DICHVUIDCU\":\"1004\",
                // \"PHONGIDCU\":\"214\",\"DICHVUIDMOI\":\"20000\",\"PHONGIDMOI\":\"210\"}"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}
                string json_in = "";
                json_in += Func.json_item("KHAMBENHID", khambenhid);
                json_in += Func.json_item("TIEPNHANID", tiepnhanid);
                json_in += Func.json_item("HOSOBENHANID", hosobenhanid);
                json_in += Func.json_item("DICHVUIDCU", dichvuidcu);
                json_in += Func.json_item("PHONGIDCU", phongkhamidcu);
                json_in += Func.json_item("DICHVUIDMOI", dichvuidmoi);
                json_in += Func.json_item("PHONGIDMOI", phongkhamidmoi); 
                json_in = "{" + json_in.Substring(0, json_in.Length - 1) + "}";
                json_in = json_in.Replace("\"", "\\\"");

                string ret_capnhat = RequestHTTP.call_ajaxCALL_SP_I("NGT02K054.CAPNHAT", json_in);
                if (ret_capnhat == "1")
                {
                    MessageBox.Show("Cập nhật thành công.");
                    getData_table(1, null); 
                }
                else if (ret_capnhat == "2")
                {
                    MessageBox.Show("Phòng khám đã chọn đang khám hoặc đã khám xong. Không cập nhật được.  ");
                    getData_table(1, null); 
                }
                else if (ret_capnhat == "3")
                {
                    MessageBox.Show("Không tồn tại phòng khám đăng ký này.");
                    getData_table(1, null); 
                }
                else if (ret_capnhat == "4")
                {
                    MessageBox.Show("Phòng khám chuyển tới trùng với phòng khám cũ trong danh sách.");
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật chức năng. ");
                }
            } 
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            DataRowView drv = ucPhong.SelectedRow;
            if (drv==null){
				MessageBox.Show("Yêu cầu chọn phòng khám ở lưới danh sách trái.");
				return; 
			}           

                //var objData = new Object();
                //FormUtil.setFormToObject("tabTiepNhan","",objData);
                //var par = [ {
                //    name : 'khambenhid',
                //    type : 'String',
                //    value : khambenhid
                //}, {
                //    name : 'phongid',
                //    type : 'String',
                //    value : phongkhamidcu
                //} ];
                //CommonUtil.inPhieu('window', 'NGT_STT', 'pdf', par);
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                string phongid = drv["PHONGID"].ToString();
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("khambenhid", "String", khambenhid);
                table.Rows.Add("phongid", "String", phongid);

                VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint("In phiếu", "NGT_STT", table);
                openForm(frm, "1");
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void openForm(Form frm, string optionsPopup)
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

    }
}