using System;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T002_capnhatsophieu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
          log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string PHIEUTHUID, MAPHIEUTHU, LICHSUTHAYDOI;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T002_capnhatsophieu()
        {
            InitializeComponent();
        }

        public void SetData(string phieuThuId, string maPhieuThu, string lichSuThayDoi)
        {
            PHIEUTHUID = phieuThuId;
            MAPHIEUTHU = maPhieuThu;
            LICHSUTHAYDOI = lichSuThayDoi;
        }

        private void VPI01T002_capnhatsophieu_Load(object sender, EventArgs e)
        {
            txtMaPhieuThu.Text = MAPHIEUTHU;
            txtLichSuThayDoi.Text = LICHSUTHAYDOI;
            btnLuu.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                float value;
                if (!float.TryParse(txtMaPhieuThu.Text, out value))
                {
                    MessageBox.Show("Mã phiếu thu phải là số", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.PT_UPD", PHIEUTHUID + "$" + txtMaPhieuThu.Text);
                if ("1".Equals(fl))
                {
                    ReturnData(true, null);
                    this.Close();
                }
                else if ("-1".Equals(fl))
                {
                    MessageBox.Show("Xảy ra lỗi", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else if ("2".Equals(fl))
                {
                    MessageBox.Show("Mã phiếu thu nằm ngoài khoảng số của sổ!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else if ("3".Equals(fl))
                {
                    MessageBox.Show("Sổ phiếu thu bị trùng!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else if ("0".Equals(fl))
                {
                    MessageBox.Show("Bạn không có quyền sửa phiếu thu này!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void txtMaPhieuThu_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhieuThu.Text) 
                || MAPHIEUTHU.Equals(txtMaPhieuThu.Text))
                btnLuu.Enabled = false;
            else btnLuu.Enabled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
    }
}