using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using System.Globalization;
using Newtonsoft.Json;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T001_ThemMST : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DialogResult dialogResult = DialogResult.Cancel;
        private string _tiepNhanId = "";

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T001_ThemMST()
        {
            InitializeComponent();
        }

        public void SetParams(string tiepNhanId)
        {
            this._tiepNhanId = tiepNhanId;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teNhapMaSoThue.Text))
            {
                MessageBox.Show("Chưa nhập lý do.");
                return;
            }

            var obj = new
            {
                TIEPNHANID = this._tiepNhanId,
                MST = teNhapMaSoThue.Text.Trim()
            };

            var fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.12", JsonConvert.SerializeObject(obj).Replace("\"", "\\\""));
            if ("-1".Equals(fl))
            {
                MessageBox.Show("Cập nhật không thành công");
                return;
            }
            else
            {
                dialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void VPI01T001_Xacnhan_Shown(object sender, EventArgs e)
        {
            teNhapMaSoThue.Focus();
        }

        private void teNhapMaSoThue_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(teNhapMaSoThue.Text))
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