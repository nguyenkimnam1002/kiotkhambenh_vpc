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
using System.Globalization;
using Newtonsoft.Json;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K033_KiemdiemTuVong : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string khambenhID = string.Empty;
        private string ngaytn = string.Empty;
        private static string longDateFormat = "yyyyMMddHHmmss";
        private static string valueNull = "không được để trống";
        private static string fullDateFormat = Const.FORMAT_datetime1;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K033_KiemdiemTuVong()
        {
            InitializeComponent();
        }

        private void NGT02K033_KiemdiemTuVong_Load(object sender, EventArgs e)
        {
            InitLoad();
        }

        private void NGT02K033_KiemdiemTuVong_Shown(object sender, EventArgs e)
        {
            txtChuToa.Focus();
        }

        private void InitLoad()
        {
            try
            {
                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    DataRowView benhnhan = Const.drvBenhNhan;
                    DataRowView benhnhanChiTiet = Const.drvBenhNhan_ChiTiet;
                    txtMaBenhNhan.Text = benhnhanChiTiet["MABENHNHAN"].ToString();
                    txtHoTen.Text = benhnhanChiTiet["TENBENHNHAN"].ToString();
                    txtNamSinh.Text = benhnhanChiTiet["NAMSINH"].ToString();
                    txtNgheNghiep.Text = benhnhanChiTiet["TENNGHENGHIEP"].ToString();
                    txtDiaChi.Text = benhnhanChiTiet["DIACHI"].ToString();
                    dtimeNgayKiemDiem.DateTime = Func.getSysDatetime();
                    ngaytn = benhnhanChiTiet["NGAYTN"].ToString();
                    khambenhID = benhnhan["KHAMBENHID"].ToString();

                    var data = RequestHTTP.get_ajaxExecuteQueryO("NGT02K033.TTKDTV", khambenhID);

                    if (data.Rows.Count > 0)
                    {
                        txtChuToa.Text = data.Rows[0]["CHUTOA_KD"].ToString();
                        txtDienBienBenh.Text = data.Rows[0]["DIEN_BIEN_BENH_KD"].ToString();
                        txtThanhVienThamGia.Text = data.Rows[0]["DS_NHANSU_KD"].ToString();
                        txtKetLuan.Text = data.Rows[0]["KET_LUAN_KD"].ToString();
                        dtimeNgayKiemDiem.DateTime = DateTime.ParseExact(data.Rows[0]["NGAY_LAPKD"].ToString().ToString(), fullDateFormat, CultureInfo.InvariantCulture);
                        txtThuKy.Text = data.Rows[0]["THUKY_KD"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateValue()) return;

                string json_in = GetJson();
                string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K033.CNKDTV", json_in);
                int value;

                if (int.TryParse(ret, out value) && !"-1".Equals(ret) && !"0".Equals(ret))
                    MessageBox.Show("Cập nhật thông tin kiểm điểm tử vong thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật thông tin kiểm điểm tử vong không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("khambenhid", "String", this.khambenhID);

                VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint("In Kiểm Điểm Tử Vong", "NGT004_TRICHBBKDTUVONG_41BV01_QD4069_A4", table, 800, 900);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetJson()
        {
            var obj = new
            {
                CHUTOA_KD = txtChuToa.Text,
                DIEN_BIEN_BENH_KD = txtDienBienBenh.Text,
                DS_NHANSU_KD = txtThanhVienThamGia.Text,
                KET_LUAN_KD = txtKetLuan.Text,
                KHAMBENHID = khambenhID,
                KIEU = "0",
                NGAY_LAPKD = dtimeNgayKiemDiem.DateTime.ToString(fullDateFormat),
                THUKY_KD = txtThuKy.Text
            };

            return JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
        }

        private bool ValidateValue()
        {
            var current = double.Parse(Func.getSysDatetime(longDateFormat));
            var kdtv = double.Parse(dtimeNgayKiemDiem.DateTime.ToString(longDateFormat));
            if (current < kdtv)
            {
                MessageBox.Show("Ngày kiểm điểm tử vong không thể lớn hơn ngày hiện tại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtimeNgayKiemDiem.Focus();
                return false;
            }

            var ngayTN = double.Parse(ngaytn);
            if (kdtv < ngayTN)
            {
                MessageBox.Show("Ngày kiểm điểm tử vong không thể nhỏ hơn ngày khám bệnh", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtimeNgayKiemDiem.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtChuToa.Text))
            {
                DisplayError("Chủ tọa", txtChuToa);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtThuKy.Text))
            {
                DisplayError("Thư ký", txtChuToa);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDienBienBenh.Text))
            {
                DisplayError("Diễn biến bệnh", txtChuToa);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtKetLuan.Text))
            {
                DisplayError("Kết luận", txtChuToa);
                return false;
            }

            return true;
        }

        private void DisplayError(string message, TextEdit text)
        {
            MessageBox.Show(message + " " + valueNull, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            text.BackColor = Color.Yellow;
            text.Focus();
        }
    }
}