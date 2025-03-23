using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using VNPT.HIS.Common;
using System.Text;
using System.Windows.Forms; 

namespace L1_Mini
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        
        private void Login_Load(object sender, EventArgs e)
        {
            if (Const.L1_autologin == "1")
            {
                string x = Func.load_config("l1_mini");
                if (x != "" && x.IndexOf("@mini@") > -1)
                {
                    txtUser.Text = x.Substring(0, x.IndexOf("@mini@"));
                    txtPass.Text = x.Substring(x.IndexOf("@mini@") + "@mini@".Length);
                    btnDangNhap_Click(null, null);
                }
            }
            label4.Text = "";
        } 
        bool log_suss = false;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtPass.Text != "" && txtUser.Text != "")
            {
                UserLogin ret = RequestHTTP.Login_Service(txtUser.Text, txtPass.Text);
                if (ret != null && ret.UUID != null && ret.UUID != "") // thanh cong
                {
                    Const.local_user = ret;
                    Const.local_username = Const.local_user.USER_NAME;

                    // ĐN thành công: lưu tạm lại u/p
                    Const.L1_mini = txtUser.Text + "@mini@" + txtPass.Text;

                    Func.config("l1_mini", ckbAutoLogin.Checked ? Const.L1_mini : "");
                     
                    Const.L1_autologin = ckbAutoLogin.Checked ? "1" : "0";

                    //Lấy config từ sv
                    Func.get_config_from_server();

                    //Lưu lại cấu hình lấy từ sv xuống local
                    string gia_tri = Const.L1_kieucapso + "|" + Const.L1_dkkham + "|" + Const.L1_ktraBHYT + "|" + Const.L1_autoPrinter + "|" + Const.L1_phanhe;
                    Func.config("CAUHINH_KIOS", gia_tri);
                    
                    txtUser.Text = "";
                    txtPass.Text = "";
                    
                    log_suss = true;

                    try
                    {
                        Form frm = this.FindForm();
                        frm.Close();
                    }
                    catch (Exception) { }

                    frmMain.Current.Login_Suss(); 
                }
                else
                { 
                    MessageBox.Show(Const.mess_login_erro);
                }
            }
            else
                MessageBox.Show(Const.mess_erro_datanull);
           
        } 
       
        private void btnThoat_Click(object sender, EventArgs e)
        {
            frmMain.Current.Exit(); 
        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (log_suss == false)
            {
                frmMain.Current.Exit();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap_Click(null, null);
            }
        }
    }
}
