using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T001_Nhapthongtintt : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _phieuThuId = string.Empty;
        public DialogResult dialogResult = DialogResult.Cancel;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T001_Nhapthongtintt()
        {
            InitializeComponent();
        }

        public void SetParams(string phieuThuId, string maPhieuThu, string tenCongTyBN, string diaChiCongTyBN, string maSoThueCongTyBN)
        {
            _phieuThuId = phieuThuId;
            teMaPhieuThu.Text = maPhieuThu;
            teTenCongTyBN.Text = tenCongTyBN;
            teDiaChiCongTyBN.Text = diaChiCongTyBN;
            teMaSoThueCongTyBN.Text = maSoThueCongTyBN;
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            var tenCongTy = teTenCongTyBN.Text.Trim();
            var diaChiCongTy = teDiaChiCongTyBN.Text.Trim();
            var maSoThue = teMaSoThueCongTyBN.Text.Trim();

            var fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.PT_UPDBN", _phieuThuId + "$" + tenCongTy + "$" + diaChiCongTy + "$" + maSoThue);
            if ("1".Equals(fl))
            {
                MessageBox.Show("Cập nhật thành công");
                dialogResult = DialogResult.OK;
                this.Close();
            }
            else if ("-1".Equals(fl))
            {
                MessageBox.Show("Xãy ra lỗi");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VPI01T001_Capnhatphieuthu_Load(object sender, EventArgs e)
        {
        }

        private void teMaPhieuThu_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teMaPhieuThu.Text))
            {
                btnLuu.Enabled = false;
            }
            else
            {
                btnLuu.Enabled = true;
            }
        }

        private void VPI01T001_Nhapthongtintt_Shown(object sender, EventArgs e)
        {
            teTenCongTyBN.Focus();
        }
    }
}