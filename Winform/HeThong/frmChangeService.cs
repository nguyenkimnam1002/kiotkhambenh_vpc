using System; 
using System.Windows.Forms; 
using System.Configuration;
using VNPT.HIS.Common;


namespace VNPT.HIS.MainForm.HeThong
{
    public partial class frmChangeService : DevExpress.XtraEditors.XtraForm
    {
        public frmChangeService()
        {
            InitializeComponent(); 
        }
        private void frmChangeService_Load(object sender, EventArgs e)
        { 
            txtLinkReport.Text = Const.LinkService;
            txtLinkService.Text = Const.LinkReport;
            //// https://histest.vnptsoftware.vn/vnpthis/RestService
             
            txtSubDomain_BenhVien.Text = Const.SubDomain;

            txtSubDomain_BenhVien.Focus();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!"".Equals(txtSubDomain_BenhVien.Text.Trim()))
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["LinkService"].Value = txtLinkService.Text;
                configuration.AppSettings.Settings["LinkReport"].Value = txtLinkReport.Text;
                configuration.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
                MessageBox.Show("Lưu thành công");
                Const.LinkService = txtLinkService.Text;
                Const.LinkReport = txtLinkReport.Text;

                string old = Const.SubDomain;
                Const.SubDomain = txtSubDomain_BenhVien.Text.Trim();

                frmMain.Current.Text = frmMain.Current.Text.Replace(old + "-", Const.SubDomain + "-");

                this.Close();
                //Application.Restart();
            } 
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private void txtSubDomain_BenhVien_EditValueChanged(object sender, EventArgs e)
        {
            txtLinkService.Text = "https://" + txtSubDomain_BenhVien.Text.Trim() + ".vnptsoftware.vn/vnpthis/RestService";
            txtLinkReport.Text = "https://" + txtSubDomain_BenhVien.Text.Trim() + ".vnptsoftware.vn/dreport/report/reportDirect.jsp";
 
        }

        private void txtSubDomain_BenhVien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.Focus();
            }
        }
    }
}