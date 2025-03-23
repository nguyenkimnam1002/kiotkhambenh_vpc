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
    public partial class frmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
        }

        private void frmChangePassword_Shown(object sender, EventArgs e)
        {
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            submit();

        }
        private void submit()
        {
            if (!checkForm()) return;

            string userName = Const.local_user.USER_NAME;
            string userId = Const.local_user.USER_ID;
            string oldPass = txtOldPass.Text;
            string res = ServiceChangePassword.checkOldPassword(userId, oldPass);
            if (res == "0")
            {
                MessageBox.Show("Mật khẩu cũ không đúng!");

                txtOldPass.Text = "";
                txtNewPass.Text = "";
                txtNewPassAgain.Text = "";

                txtOldPass.Focus();
            }
            else
            {
                string newPass = txtNewPass.Text;
                string res_1 = ServiceChangePassword.changePassword(userId, newPass);

                if (res_1 == "1")
                {
                    MessageBox.Show("Đổi mật khẩu thành công!");
                    txtOldPass.Text = "";
                    txtNewPass.Text = "";
                    txtNewPassAgain.Text = "";
                    txtOldPass.Focus();
                }
                else MessageBox.Show("Đổi mật khẩu thất bại!");
            }
        }

        private bool checkForm()
        {
            string newPassword = txtNewPass.Text;
            string retypeNewPassword = txtNewPassAgain.Text;
            if (newPassword != retypeNewPassword || newPassword=="")
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu mới nhập lại không giống nhau!");
                txtNewPass.Text = "";
                txtNewPassAgain.Text = "";
                txtNewPass.Focus();
                return false;
            }
            return true;
        }

        private void txtOldPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewPass.Focus();
            }
        }

        private void txtNewPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNewPassAgain.Focus();
            }
        }

        private void txtNewPassAgain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.Focus();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

    }
}