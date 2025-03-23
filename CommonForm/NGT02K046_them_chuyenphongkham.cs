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
    public partial class NGT02K046_them_chuyenphongkham : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
              log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string khamBenhID = "";
        private string tiepNhanID = "";
        private string dichVuID = "";
        private string kieu = "";
        private string phongCuID = "";
        private string doiTuongBNID = "";
        private string phongKhamDangKyID = "";
        private bool isInitial;

        public NGT02K046_them_chuyenphongkham()
        {
            InitializeComponent();
        }

        public void Load_Data(string khamBenhID, string tiepNhanID, string dichVuID, string kieu, string phongCuID, string doiTuongBNID, string phongKhamDangKyID)
        {
            this.khamBenhID = khamBenhID;
            this.tiepNhanID = tiepNhanID;
            this.dichVuID = dichVuID;
            this.kieu = kieu;
            this.phongCuID = phongCuID;
            this.doiTuongBNID = doiTuongBNID;
            this.phongKhamDangKyID = phongKhamDangKyID;
        }

        private void Init_Form()
        {
            isInitial = true;
            string tinhThuKhac = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "NGT_TINHTHUKHAC_PKGIAOSU");
            if ( tinhThuKhac == "1")
            {
                uccb_ThuKhac.Enabled = true;
                echeck_CongKham.Enabled = true;

                DataTable dtThuKhac = RequestHTTP.get_ajaxExecuteQuery("DMC01.GDV", new string[] { "[0]", "[1]", "[2]" }, new string[] { "1", "-1", "-1" });
                if (dtThuKhac.Rows.Count == 0) dtThuKhac = Func.getTableEmpty(new string[] { "col1", "col2" });

                DataRow dr_ThuKhac = dtThuKhac.NewRow();
                dr_ThuKhac["col1"] = "-1";
                dr_ThuKhac["col2"] = "--- Chọn thu khác ---";
                dtThuKhac.Rows.InsertAt(dr_ThuKhac, 0);
                uccb_ThuKhac.setData(dtThuKhac, 0, 1);
                uccb_ThuKhac.SelectIndex = 0;
                uccb_ThuKhac.setColumn(0, false);
            }
            else
            {
                uccb_ThuKhac.Enabled = false;
                echeck_CongKham.Enabled = false;
            }

            string NGT_INPHIEUKHAM_CHUYENPHONG = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "NGT_INPHIEUKHAM_CHUYENPHONG");
            if (NGT_INPHIEUKHAM_CHUYENPHONG != "1")
                btn_InPhieu.Visible = false;

            string LOAD_YEUCAUKHAM_THEO_DT = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "LOAD_YEUCAUKHAM_THEO_DT");
            DataTable dt_YCKham = new DataTable();
            dt_YCKham = RequestHTTP.get_ajaxExecuteQuery("NGTDV.002", new string[] { "[0]" }, new string[] { (LOAD_YEUCAUKHAM_THEO_DT != "0" ? doiTuongBNID : "0") });
            if (dt_YCKham.Rows.Count == 0) dt_YCKham = Func.getTableEmpty(new string[] { "col1", "col2" });

            DataRow dr_YCKham = dt_YCKham.NewRow();
            dr_YCKham["col1"] = "-1";
            dr_YCKham["col2"] = "Chọn yêu cầu khám";
            dt_YCKham.Rows.InsertAt(dr_YCKham, 0);
            uccbsearch_YCKham.setData(dt_YCKham, 0, 1);
            uccbsearch_YCKham.setEvent(Change_YCKham);
            uccbsearch_YCKham.SelectValue = dichVuID;
            Change_YCKham(null, null);

            if (doiTuongBNID == "2")
            {
                echeck_BHYT_DV.Enabled = false;
            }
            else
            {
                string par = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "NGT_AN_CHECKBOX_BHYT_DV");
                if (par != "1") echeck_BHYT_DV.Enabled = true;
                else echeck_BHYT_DV.Enabled = false;
            }

        }

        private void Change_YCKham(object sender, EventArgs e)
        {
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV", new string[] { "[0]" }, new string[] { uccbsearch_YCKham.SelectValue });

            uccbsearch_PhongKham.setData(dt, 0, 1);

            if (isInitial)
                uccbsearch_PhongKham.SelectValue = phongCuID;
            else
                uccbsearch_PhongKham.SelectIndex = 0;

            isInitial = false;
        }

        private void NGT02K046_them_chuyenphongkham_Load(object sender, EventArgs e)
        {
            Init_Form();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            lb_ThongBao.Visible = false;
            string phongID_Moi = uccbsearch_PhongKham.SelectValue;
            if (phongID_Moi == "")
            {
                MessageBox.Show("Hãy chọn phòng");
                return;
            }
            if (RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "NGT_TINHTHUKHAC_PKGIAOSU") == "1")
            {
                string _phonggiaosu = uccbsearch_PhongKham.SelectValue;
                if (uccb_ThuKhac.SelectValue == "" && _phonggiaosu == "1" && (doiTuongBNID == "1" || doiTuongBNID == "6"))
                {
                    MessageBox.Show("Hãy chọn thông tin thu khác");
                    return;
                }
            }

            //=========== check dich vu thanh toan dong thoi; 
            string checkTt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CANHBAO_KHONG_TTDT");
            string msgCheckTt = RequestHTTP.call_ajaxCALL_SP_S_error_msg("NTU01H001.EV018", this.tiepNhanID);
            if (msgCheckTt != "")
            {
                MessageBox.Show("Các dịch vụ " + msgCheckTt + " miễn giảm thanh toán đồng thời");
                if (checkTt == "1")
                {
                    return;
                }
            }

            //========
            string BHYT_DV = "0";
            string boCongKham = "0";
            if (echeck_BHYT_DV.Checked) BHYT_DV = "1";
            if (echeck_CongKham.Checked) boCongKham = "1";
            
            if (phongID_Moi == "" || phongID_Moi == "0")
            {
                MessageBox.Show("Hãy chọn phòng");
                return;
            }

            if (kieu != "0")
            {
                string checkMax_Phongkham = RequestHTTP.check_maxPK(phongID_Moi);
                if (checkMax_Phongkham == "-1")
                {
                    MessageBox.Show("Phòng khám hết số");
                    return;
                }
            }

            //Lưu
            string json = "{\"khambenhid\":\"" + khamBenhID + "\",\"phongid\":\"" + phongID_Moi + "\",\"dichvuid\":\""
                + uccbsearch_YCKham.SelectValue + "\",\"kieu\":\"" + kieu + "\",\"phongcuid\":\"" + phongCuID + "\",\"phongkhamdangkyid\":\""
                + phongKhamDangKyID + "\",\"bhyt_dv\":\"" + BHYT_DV + "\",\"thukhacid\":\"" + uccb_ThuKhac.SelectValue + "\",\"dtbnid\":\""
                + doiTuongBNID + "\",\"bocongkham\":\"" + boCongKham + "\"}";

            string ret = RequestHTTP.call_ajaxCALL_SP_S_result("NGT02K046.CNDATA", json.Replace("\\", "\\\\").Replace("\"", "\\\""));

            if (ret.Split(',')[0] == "1")
            {
                lb_ThongBao.Text = "Bệnh nhân có số thứ tự mới: " + ret.Split(',')[1];
                lb_ThongBao.Visible = true;
                MessageBox.Show("Cập nhật thông tin thành công");
            }
            else if (ret.Equals("tontaipk"))
                MessageBox.Show("Bệnh nhân đã đăng ký phòng khám" + uccbsearch_PhongKham.SelectDataRowView["col2"].ToString());
            else if (ret.Equals("dachuyenphong"))
                MessageBox.Show("Bệnh nhân đã chuyển phòng khám");
            else if (ret == "khongtaothukhac")
                MessageBox.Show("Không tạo được thu khác cho bệnh nhân này");
            else if (ret == "bckgiakhacnhau")
                MessageBox.Show("Giá công khám cũ và công khám mới khác nhau. Không chuyển được.");
            else if (ret == "bcktontaidvbhyt")
                MessageBox.Show("Y/c khám cũ tồn tại dịch vụ BHYT. Không chuyển được");
            else if (ret == "bckkhongxacdinh")
                MessageBox.Show("Lỗi xảy ra trong khi bỏ công khám.");
            else if (ret == "bckkhongthaydv" || ret == "bckkhongthaymbp" || ret == "bckkhongthaydvkb")
                MessageBox.Show("Lỗi xảy ra trong khi bỏ công khám .");
            else
                MessageBox.Show("Cập nhật thông tin không thành công");
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}