using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T001_Capnhatphieuthu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _phieuThuId = string.Empty;
        private string _maPhieuThuCu = string.Empty;
        private string _log = string.Empty;
        public DialogResult dialogResult = DialogResult.Cancel;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T001_Capnhatphieuthu()
        {
            InitializeComponent();
        }

        public void SetParams(string phieuThuId, string maPhieuThu, string log)
        {
            _phieuThuId = phieuThuId;
            _maPhieuThuCu = maPhieuThu;
            _log = log;
        }

        private bool IsNumber(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var _mapthu = teMaPhieuThu.Text.Trim();
            if (!IsNumber(_mapthu))
            {
                MessageBox.Show("Mã phiếu thu phải là số");
                return;
            }

            var fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.PT_UPD", _phieuThuId + '$' + _mapthu);
            if ("1".Equals(fl))
            {
                dialogResult = DialogResult.OK;
                this.Close();
            }
            else if ("1".Equals(fl))
            {
                MessageBox.Show("Xảy ra lỗi");
            }
            else if ("2".Equals(fl))
            {
                MessageBox.Show("Mã phiếu thu nằm ngoài khoảng số của sổ!");
            }
            else if ("3".Equals(fl))
            {
                MessageBox.Show("Sổ phiếu thu bị trùng!");
            }
            else if ("0".Equals(fl))
            {
                MessageBox.Show("Bạn không có quyền sửa phiếu thu này!");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VPI01T001_Capnhatphieuthu_Load(object sender, EventArgs e)
        {
            teMaPhieuThu.Text = _maPhieuThuCu;
            teLichSuThayDoi.Text = _log.Replace("\n", "\r\n");
        }

        private void teMaPhieuThu_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teMaPhieuThu.Text) || _maPhieuThuCu.Equals(teMaPhieuThu.Text.Trim()))
            {
                btnLuu.Enabled = false;
            }
            else
            {
                btnLuu.Enabled = true;
            }
        }
    }
}