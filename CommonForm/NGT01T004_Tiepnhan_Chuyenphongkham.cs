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
    public partial class NGT01T004_Tiepnhan_Chuyenphongkham : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        string kieu = ""; 
        string khambenhid = "";
        string dichvuid = "";
        string phongid = "";
        string phongkhamdangkyid = "";

        string tiepnhanid = "";
        string doituongbenhnhanid = "";
        
        public NGT01T004_Tiepnhan_Chuyenphongkham()
        {
            InitializeComponent();
        }
        public void setKhamBenhID(string kieu, string khambenhid, string dichvuid, string phongid, string phongkhamdangkyid)
        {
            this.kieu = kieu;
            this.khambenhid = khambenhid;
            this.dichvuid = dichvuid;
            this.phongid = phongid;
            this.phongkhamdangkyid = phongkhamdangkyid;
        }
        private void NGT01T004_tiepnhan_chuyenphongkham_Load(object sender, EventArgs e)
        {
            // {"func":"ajaxCALL_SP_I","params":["COM.CAUHINH.THEOMA","LOAD_YEUCAUKHAM_THEO_DT"],"uuid":"6ac7d3b8-24a4-46ea-8c00-ec4504d2252a"}
            // "result": "1",
            string ret = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "LOAD_YEUCAUKHAM_THEO_DT");

            // {"func":"ajaxExecuteQuery","params":["","NGTDV.002"],"options":[{"name":"[0]"}],"uuid":"6ac7d3b8-24a4-46ea-8c00-ec4504d2252a"}
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGTDV.002", new string[] { "[0]", "[1]" }, new string[] { (ret != "0" ? doituongbenhnhanid : "0"), dichvuid });

            if (dt.Rows.Count==0) dt= Func.getTableEmpty(new string[] {"col1", "col2"} );
            dt.Rows.Add(new string[] { "0", "Chọn yêu cầu khám" });

            ucYeuCau.setData(dt, 0, 1);
            ucYeuCau.SelectValue = dichvuid;
            ucYeuCau.setEvent(loadPhongKham);
            ucYeuCau.Enabled = false;
            loadPhongKham(null, null);

            if (kieu == "0")
            { // Chuyển phòng khám 
                this.Text = "Chuyển phòng khám ";
            }
            else if (kieu == "2")
            { // Thêm phòng khám
                this.Text = "Thêm phòng khám";
            }
        } 
        private void loadPhongKham(object sender, EventArgs e)
        {
            string dichvuid_moi = ucYeuCau.SelectValue;
            //{"func":"ajaxExecuteQuery","params":["","NGTDV.002"],"options":[{"name":"[0]"}],"uuid":"6ac7d3b8-24a4-46ea-8c00-ec4504d2252a"}
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV", new string[] { "[0]" }, new string[] { dichvuid_moi });
            
            ucPhongKham.setData(dt, 0, 1);
            ucPhongKham.SelectIndex = 0;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            labelControl1.Visible = false;
            string phongid_moi = ucPhongKham.SelectValue;
            string dichvuid_moi = ucYeuCau.SelectValue;

            if(phongid_moi == "" || phongid_moi =="0"){
                MessageBox.Show("Hãy chọn phòng");
				return;
			}

            if (kieu != "0")
            {
                string checkMax_Phongkham = RequestHTTP.check_maxPK(phongid_moi);
                if (checkMax_Phongkham == "-1")
                {
                    MessageBox.Show("Phòng khám hết số");
                    return;
                }
            }

            //=========== check dich vu thanh toan dong thoi; 
            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_CANHBAO_KHONG_TTDT"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}
            string checkTt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CANHBAO_KHONG_TTDT");

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
             
            //Lưu
            // {"func":"ajaxCALL_SP_S","params":["NGT01T001.TCPK","{\"khambenhid\":\"3469\",\"phongid\":null,\"dichvuid\":\"\",\"kieu\":0,\"phongkhamdangkyid\":\"2519\"}"],"uuid":"6ac7d3b8-24a4-46ea-8c00-ec4504d2252a"}
            string json = "{\"khambenhid\":\"" + khambenhid + "\",\"phongid\":\"" + phongid_moi + "\",\"dichvuid\":\"" 
                + dichvuid_moi + "\",\"kieu\":\"" + kieu + "\",\"phongkhamdangkyid\":\"" + phongkhamdangkyid + "\"}";
            string ret = RequestHTTP.call_ajaxCALL_SP_S_result("NGT01T001.TCPK", json.Replace("\\", "\\\\").Replace("\"", "\\\"") );
            
                if (ret.Split(',')[0] == "1")
                {
                    labelControl1.Text = "Bệnh nhân có số thứ tự mới: " + ret.Split(',')[1];
                    labelControl1.Visible = true;
                    MessageBox.Show("Cập nhật thông tin thành công");
                }
                else if (ret.Equals("tontaipk"))
                    MessageBox.Show("Bệnh nhân đã đăng ký phòng khám" + ucPhongKham.SelectDataRowView["col2"].ToString());
                else if (ret.Equals("dachuyenphong"))
                    MessageBox.Show("Bệnh nhân đã chuyển phòng khám");
                else if (ret.Equals("codonthuoc"))
                    MessageBox.Show("Bệnh nhân đang có đơn thuốc");
                else
                    MessageBox.Show("Cập nhật thông tin không thành công");
             
        }

        private void btnInphieu_Click(object sender, EventArgs e)
        {
            string phongid = ucPhongKham.SelectValue;

            if (phongid == "" || khambenhid == "")
            {
                MessageBox.Show("Hãy chọn bệnh nhân muốn in phiếu");
                return;
            }

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                                
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("khambenhid", "String", khambenhid);
                table.Rows.Add("phongid", "String", phongid);

                VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint("In phiếu", "NGT_STT", table);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}