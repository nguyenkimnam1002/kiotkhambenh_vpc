using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using VNPT.HIS.Common;
using System.Text;
using System.IO;
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
            //if (Const.L1_BV_DEFAULT == 5)// test cho đk hà nam
            //{
            //    txtUser.Text= "DHYTB.ADMIN";
            //    txtPass.Text = "Gppm2#2018";
            //}
            //txtUser.Text = "VIMEHNI.ADMIN";
            //txtPass.Text = "Yd0o@2sc";
            //txtUser.Text = "DKVTTBH.ADMIN";
            //txtPass.Text = "Rt3ux@1x";


            try
            {
                if (Const.L1_autologin)
                {
                    if (Const.L1_mini != "" && Const.L1_mini.IndexOf("@mini@") > -1 )
                    {
                        txtUser.Text = Const.L1_mini.Substring(0, Const.L1_mini.IndexOf("@mini@"));
                        txtPass.Text = Const.L1_mini.Substring(Const.L1_mini.IndexOf("@mini@") + "@mini@".Length);
                        btnDangNhap_Click(null, null);
                    }
                }
            }
            catch(Exception ex) { }


            Const.L1_autologin = true;
            label4.Text = "";
        } 
        bool log_suss = false;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //txtPass.Text = "Gppm2#2018";
            //txtUser.Text = "bnd.admin";

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
                     
                    Const.L1_autologin = ckbAutoLogin.Checked;

                    //luu
                    Func.set_config_file();

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
