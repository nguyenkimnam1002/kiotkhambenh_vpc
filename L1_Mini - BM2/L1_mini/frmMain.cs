using System;
using VNPT.HIS.Common;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;
using Microsoft.Win32;

namespace L1_Mini
{
    public partial class frmMain : Form
    {
        public static frmMain Current;

        Microsoft.Win32.RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public frmMain()
        {
            InitializeComponent();
        }
        //
        private void openForm(Form frm, string optionsPopup = "0", bool optionfullsize = false)
        {
            try
            {
                if (optionsPopup == "0")
                {
                    frm.WindowState = optionfullsize ? FormWindowState.Maximized : FormWindowState.Normal;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.MdiParent = this;
                    frm.Show();
                }
                else
                {
                    frm.WindowState = optionfullsize ? FormWindowState.Maximized : FormWindowState.Normal;
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();
                }
            }
            catch(Exception ex) { }
        }
        public static void closeAll()
        {
            FormCollection fc = Application.OpenForms;
            if (fc.Count > 1)
            {
                for (int i = (fc.Count); i > 1; i--)
                {
                    Form selectedForm = Application.OpenForms[i - 1];
                    if (selectedForm.Text != "")
                        if (Current.Text == "KIOS CẤP SỐ") selectedForm.Hide();
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // kiểm tra khởi động cùng win?
            //MessageBox.Show("thiết lập cũ=== " + (rkApp.GetValue("run_app_mini") == null ? "Ko có" : (string)rkApp.GetValue("run_app_mini")));

            //MessageBox.Show("Thiết lập hiện tại: "+ Application.ExecutablePath.ToString());
 
            // Tạo thiết lập mới nếu khác nhau về đường dẫn: trong trường hợp update bản mới
            if (rkApp.GetValue("run_app_mini") != null
                && (string)rkApp.GetValue("run_app_mini") != Application.ExecutablePath.ToString())
                rkApp.SetValue("run_app_mini", Application.ExecutablePath.ToString());


            //System.Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            Current = this;

            Func.get_config_from_server();
            Reset_CauHinh_BenhVien();


            //if (Const.L1_kieucapso == "") // lần đầu chạy app
            //    open_box_Config();

            // Mở form đăng nhập: trong đó đã có check autologin
            openForm(new Login(), "0");

        }

        

        private void Reset_CauHinh_BenhVien()
        {
            string full_path_logo;
            Func.Reset_Logo_BenhVien(out full_path_logo);
            this.BackgroundImage = Func.getIconFullPath(full_path_logo);
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void Exit()
        {
            //this.Dispose();
            Application.Exit();
        }

        public void Login_Suss()
        {
            // cách hiển thị cấp số
            // 1: nhập tên, mã, thẻ BH;   
            // 2: Quẹt mã bệnh nhân
            if (Const.L1_kieucapso == "2")
            {
                CapSo_QuetMaBN frm = new CapSo_QuetMaBN();
                frm.setReturn_To_Mainform(Listen_From_Subform);
                openForm(frm, "1", true);
            }
            else if (Const.L1_kieucapso == "1")
            {
                SttKham frm = new SttKham();
                frm.setReturn_To_Mainform(Listen_From_Subform);
                openForm(frm, "1", true);
            }
            else if (Const.L1_kieucapso == "3")
            {
                CapSo frm = new CapSo();
                frm.setReturn_To_Mainform(Listen_From_Subform);
                openForm(frm, "1", true);
            }
        }
        private void Listen_From_Subform(object sender, EventArgs e)
        {
            closeAll();
            // thay đổi form kiểu cấp số
            Login_Suss();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.Shift && e.KeyCode == Keys.P)
                || (e.Control && e.Shift && e.KeyCode == Keys.Q))
            {
                bool current_TopMost = this.TopMost;
                FullScreen(false);
                 
                // mở config
                Config frm = new Config(current_TopMost);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.setReturnData(Return_Config);
                frm.ShowDialog();
            }
        }
        private void Return_Config(object sender, EventArgs e)
        {
            Reset_CauHinh_BenhVien();

            string ret = (string)sender;

            if (ret != "thoat")
                FullScreen(ret=="true");
            else if (ret == "thoat")
                this.Close();
        }

        private void FullScreen(bool full)
        {
            FormBorderStyle = full ? FormBorderStyle.None : FormBorderStyle.Sizable;
            TopMost = full;
            this.WindowState = FormWindowState.Maximized;
        }

    }
}
