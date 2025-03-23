using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using System.Globalization;
using Newtonsoft.Json;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T001_Xacnhan : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DialogResult dialogResult = DialogResult.Cancel;
        private string _phieuthuid = "";
        private string _loaiThanhToan = "";

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T001_Xacnhan()
        {
            InitializeComponent();
        }

        public void SetParams(string phieuthuid, string loaiThanhToan)
        {
            this._phieuthuid = phieuthuid;
            this._loaiThanhToan = loaiThanhToan;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teLyDoHuyPhieu.Text))
            {
                MessageBox.Show("Chưa nhập lý do.");
                return;
            }

            var obj = new
            {
                PHIEUTHUID = _phieuthuid,
                THOIGIANHUYPHIEU = Func.getSysDatetime().ToString("dd/MM/yyyy HH:mm:ss"),
                HINHTHUCTHANHTOANHUY = _loaiThanhToan,
                LYDOHUYPHIEU = teLyDoHuyPhieu.Text.Trim(),
            };

            var fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.09", JsonConvert.SerializeObject(obj).Replace("\"", "\\\""));
            if ("1".Equals(fl))
            {
                dialogResult = DialogResult.OK;
                this.Close();
            }
            else if ("0".Equals(fl))
            {
                MessageBox.Show("Bạn không có quyển hủy phiếu thu này");
                return;
            }
            else if ("-1".Equals(fl))
            {
                MessageBox.Show("Cập nhật không thành công");
                return;
            }
            else if ("-2".Equals(fl))
            {
                MessageBox.Show("Có dịch vụ viện phí đã thực hiện, không thể hủy phiếu");
                return;
            }
            else if ("-3".Equals(fl))
            {
                MessageBox.Show("Đã duyệt kế toán, không thể hủy phiếu");
                return;
            }
            else if ("-4".Equals(fl))
            {
                MessageBox.Show("Đã chốt hóa đơn, không thể hủy phiếu");
                return;
            }
            else if ("-5".Equals(fl))
            {
                MessageBox.Show("Đã có dịch vụ thu tiền, không thể hủy phiếu hoàn dịch vụ");
                return;
            }
            else if ("-6".Equals(fl))
            {
                MessageBox.Show("Đã có phiếu hoàn, không thể hủy phiếu");
                return;
            }
            //tuyennx_add_start_20171221 yc HISL2CORE-654 khong cho huy hoa don khi da gui sang hcsn
            else if ("-7".Equals(fl))
            {
                MessageBox.Show("Hóa đơn đã gửi sang phần mềm kế toán không thể hủy ");
                return;
            }
        }

        private void teLyDoHuyPhieu_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teLyDoHuyPhieu.Text))
            {
                btnLuu.Enabled = false;
            }
            else
            {
                btnLuu.Enabled = true;
            }
        }

        private void VPI01T001_Xacnhan_Shown(object sender, EventArgs e)
        {
            teLyDoHuyPhieu.Focus();
        }
    }
}