using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.MainForm.Class;
  

namespace VNPT.HIS.MainForm.HeThong
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                Const.local_username = Const.SQLITE.cache_get("local_username");
                //Const.local_username = "";
                if (Const.local_username != "")
                {
                    txtUser.Text = Const.local_username;
                    txtPass.Focus();
                }
                else
                {
                    frmMain.Current.Loi_Setup_sqlite();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        { 
            LoginSubmit();
        }

        private void LoginSubmit()
        {
            try
            {
                log.Info("Login: " + txtUser.Text);
                if (txtPass.Text != "" && txtUser.Text != "")
                {
                    UserLogin ret = RequestHTTP.Login_Service(txtUser.Text, txtPass.Text);
                    if (ret != null && ret.UUID != null && ret.UUID != "") // thanh cong
                    {
                        txtUser.Text = "";
                        txtPass.Text = "";
                        //this.WindowState = FormWindowState.Minimized;
                        this.Close();
                        this.Hide();
                        frmMain.Current.DangNhapThanhCong(ret);
                    }
                    else
                    {
                        log.Fatal("Lỗi userlogin: " + (ret == null) + "-" + (ret != null ? ret.UUID : " user null"));
                        MessageBox.Show(Const.mess_login_erro);
                    }
                }
                else
                    MessageBox.Show(Const.mess_erro_datanull);
            }
            catch (Exception ex)
            {
                log.Fatal("Lỗi userlogin2: " + ex.ToString());
                MessageBox.Show(Const.mess_erro_sys + ": " + ex.ToString());
            }
            txtUser.Focus();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPass.Focus();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSubmit.Focus();  

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

    } 
}