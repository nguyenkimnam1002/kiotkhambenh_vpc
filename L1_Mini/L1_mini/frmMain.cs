using System;
using VNPT.HIS.Common;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing;
using Microsoft.Win32;
using System.IO;
using System.Data;
using Newtonsoft.Json;


namespace L1_Mini
{
    public partial class frmMain : Form
    {
        public static frmMain Current;
        bool config_moi = true;

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

            try
            {
                Directory.CreateDirectory(Const.FolderSaveFilePrint);
            }
            catch (Exception ex) { }

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


            if (config_moi)
            {
                frmMain_Load_NEW(sender, e);
                return;
            } 




            Func.get_config_file();

            if (Const.LOCAL_CAUHINH_KIOS == "") // lần đầu chạy app
            {
                rkApp.SetValue("run_app_mini", Application.ExecutablePath.ToString());

                // ***
                // *********
                // ***
                Const.L1_BV_DEFAULT = 4; // 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM;  
                // nếu là test
                Const.default_test = Const.L1_BV_DEFAULT;// 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM


                if (Const.L1_BV_DEFAULT == 0)
                    Const.LinkService = "https://histest.vnptsoftware.vn/vnpthis/RestService";
                else if (Const.L1_BV_DEFAULT == 1)
                    Const.LinkService = "https://bvbachmai2.vncare.vn/vnpthis/RestService";
                else if (Const.L1_BV_DEFAULT == 2)
                    Const.LinkService = "https://bvvietduc2.vncare.vn/vnpthis/RestService";
                else if (Const.L1_BV_DEFAULT == 3)
                    Const.LinkService = "https://bvtamthan.vnptsoftware.vn/vnpthis/RestService";
                else if (Const.L1_BV_DEFAULT == 4)
                {
                    Const.LinkService = "https://benhnhietdoi.vncare.vn/vnpthis/RestService";
                    Const.L1_ktraBHYT = "1";//
                }
                else if (Const.L1_BV_DEFAULT == 5) // đk hà nam 
                {
                    Const.LinkService = "https://dakhoahanam.vnptsoftware.vn/vnpthis/RestService";
                    Const.L1_ktraBHYT = "1";
                }
                else if (Const.L1_BV_DEFAULT == 6) // đk chung: BVDAKHOA
                    Const.LinkService = "https://bvdakhoa.vncare.vn/vnpthis/RestService";
                else
                    Const.LinkService = "https://histest.vnptsoftware.vn/vnpthis/RestService";
                
                Func.set_config_file();

                // mở config
                {
                    Config frm = new Config(this.TopMost);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.setReturnData(Listen_Children_Form);
                    frm.ShowDialog();
                }
            }

            Reset_CauHinh_BenhVien();

            
            // Mở form đăng nhập: trong đó đã có check autologin
            openForm(new Login(), "0");
        }

        private void frmMain_Load_NEW(object sender, EventArgs e)
        {
            // Load các biến tại local

            //mặc định
            //Const.L1_BV_DEFAULT = 0; // 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM;  
            Const.LinkService = "https://histestl2.vncare.vn/vnpthis/RestService";  
            Const.L1_autologin = true; 
            try
            {
                Const.LOCAL_CAUHINH_KIOS = Func.FromHex(File.ReadAllText(Const.FolderSaveFilePrint + "\\data.dat"));
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(Const.LOCAL_CAUHINH_KIOS, (typeof(DataTable)));
                if (dt != null)
                { 
                    if (dt.Columns.Contains("LinkService")) Const.LinkService = dt.Rows[0]["LinkService"].ToString();
                    if (dt.Columns.Contains("L1_autologin")) Const.L1_autologin = Convert.ToBoolean(dt.Rows[0]["L1_autologin"].ToString());
                    if (dt.Columns.Contains("L1_mini")) Const.L1_mini = dt.Rows[0]["L1_mini"].ToString(); 
                }
            }
            catch (Exception ex) { }

            if (Const.LOCAL_CAUHINH_KIOS == "") // lần đầu chạy app
            {
                rkApp.SetValue("run_app_mini", Application.ExecutablePath.ToString()); 
                // mở config 
                    ConfigNew frm = new ConfigNew(this.TopMost);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.setReturnData(Listen_Children_Form);
                    frm.ShowDialog(); 
            }

            // Mở form đăng nhập: trong đó đã có check autologin
            openForm(new Login(), "0");
        }



        private void Reset_CauHinh_BenhVien()
        {
            if (config_moi)
            {
                string KIOS_APP_BG = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_BG");
                string full_path_logo1 = Application.StartupPath + ("./Resources/" + KIOS_APP_BG).Substring(1).Replace("/", "\\");
                this.BackgroundImage = Func.getIconFullPath(full_path_logo1);
                this.BackgroundImageLayout = ImageLayout.Stretch;
                return;
            }

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
            if (config_moi)
            {
                //LOAD CÁC BIẾN TỪ WEB (chỉ load đc khi đã đn thành công)
                Reset_CauHinh_BenhVien();

                Const.L1_MAU_STT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT");
                Const.L1_MAU_STT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT");
                Const.L1_XemPhieuSTT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_XEMSTT") == "0";
                Const.L1_kieucapso = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_KIEUCAPSO");
                 

                if (Const.L1_kieucapso == "2")
                {
                    CapSo_QuetMaBN frm = new CapSo_QuetMaBN(this.TopMost);
                    frm.setReturn_To_Mainform(Listen_Children_Form);
                    openForm(frm, "1", true);
                }
                else if (Const.L1_kieucapso == "3")
                {
                    CapSo frm = new CapSo(this.TopMost);
                    frm.setReturn_To_Mainform(Listen_Children_Form);
                    openForm(frm, "1", true);
                }
                else if (Const.L1_kieucapso == "1")
                {
                    BVDAKHOA_Kios frm = new BVDAKHOA_Kios(this.TopMost);
                    frm.setReturn_To_Mainform(Listen_Children_Form);
                    openForm(frm, "1", true);
                }
                else
                {
                    MessageBox.Show("Chưa cấu hình KIOS_APP_KIEUCAPSO");
                }

                return;
            }
            //Const.local_user.UUID = "eyJhbGciOiJIUzUxMiJ9.eyJqdGkiOiI2NWU3YWQ4NS00NTc0LTRjZGUtYTg5Ny1hOTI0NWRjNGRlOWQiLCJpYXQiOjE1NjA5MzE1OTIsInN1YiI6IkRLSE5NLkFETUlOIiwiaXNzIjoiIiwiZXhwIjoxNTYwOTQ1OTkyfQ.Dc_0Iaikc3FH-7haHGpmNlPnXexJAY1RfJomZyZ8RtG1xpXfYWPyb3a_gjtnvs_JjrqpJXIuu7YNGvK1hf-yXg";

            // cách hiển thị cấp số
            // 1: nhập tên, mã, thẻ BH;   
            // 2: Quẹt mã bệnh nhân
            if (Const.L1_kieucapso == "2")
            {
                CapSo_QuetMaBN frm = new CapSo_QuetMaBN(this.TopMost);
                frm.setReturn_To_Mainform(Listen_Children_Form);
                openForm(frm, "1", true);
            }
            else if (Const.L1_kieucapso == "1")
            {
                if (Const.L1_BV_DEFAULT == 4)   // 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM;
                {
                    Kios_BND frm = new Kios_BND(this.TopMost);
                    frm.setReturn_To_Mainform(Listen_Children_Form);
                    openForm(frm, "1", true);
                }
                else if (Const.L1_BV_DEFAULT == 1)
                {
                    Kios_BM2 frm = new Kios_BM2(this.TopMost);
                    frm.setReturn_To_Mainform(Listen_Children_Form);
                    openForm(frm, "1", true);
                }
                else if (Const.L1_BV_DEFAULT == 5)
                {
                    Kios_DKHNM frm = new Kios_DKHNM(this.TopMost);
                    frm.setReturn_To_Mainform(Listen_Children_Form);
                    openForm(frm, "1", true);
                }
                else if (Const.L1_BV_DEFAULT == 6)
                {
                    Kios_DKHNM frm = new Kios_DKHNM(this.TopMost);
                    frm.setReturn_To_Mainform(Listen_Children_Form);
                    openForm(frm, "1", true);
                }
                else
                {
                    if (Const.default_test == 4)
                    {
                        Kios_BND frm = new Kios_BND(this.TopMost);
                        frm.setReturn_To_Mainform(Listen_Children_Form);
                        openForm(frm, "1", true);
                    }
                    else if (Const.default_test == 5)
                    {
                        Kios_DKHNM frm = new Kios_DKHNM(this.TopMost);
                        frm.setReturn_To_Mainform(Listen_Children_Form);
                        openForm(frm, "1", true);
                    }
                    else if (Const.default_test == 6)
                    {
                        Kios_DKHNM frm = new Kios_DKHNM(this.TopMost);
                        frm.setReturn_To_Mainform(Listen_Children_Form);
                        openForm(frm, "1", true);
                    }

                    else 
                    {
                        Kios_BND frm = new Kios_BND(this.TopMost);
                        frm.setReturn_To_Mainform(Listen_Children_Form);
                        openForm(frm, "1", true);
                    }
                }
            }
            else if (Const.L1_kieucapso == "3")
            {
                CapSo frm = new CapSo(this.TopMost);
                frm.setReturn_To_Mainform(Listen_Children_Form);
                openForm(frm, "1", true);
            }
        }
        private void Listen_From_Subform(object sender, EventArgs e)
        {
            string ret = (string)sender;

            if (ret == "dang_nhap_lai")
            {
                Listen_Children_Form("dang_nhap_lai", null);
                return;
            }

            
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.Shift && e.KeyCode == Keys.P)
                || (e.Control && e.Shift && e.KeyCode == Keys.Q))
            {
                bool current_TopMost = this.TopMost;
                FullScreen(false);

                // mở config
                if (config_moi)
                {
                    ConfigNew frm = new ConfigNew(current_TopMost);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.setReturnData(Listen_Children_Form);
                    frm.ShowDialog();
                }
                else
                {
                    Config frm = new Config(current_TopMost);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.setReturnData(Listen_Children_Form);
                    frm.ShowDialog();
                }
            }
        }
        private void Listen_Children_Form(object sender, EventArgs e)
        {
            string ret = (string)sender;

            if (ret == "dang_nhap_lai")
            {
                Const.local_user = null;
                Const.L1_autologin = false;
                openForm(new Login(), "0");
            }
            else if (ret == "thoat")
            {
                this.Close();
            }
            else if (ret == "reset_config")
            {
                FullScreen(ret == "true");
                closeAll();
                // thay đổi form kiểu cấp số
                Login_Suss();
            }

            Reset_CauHinh_BenhVien();

        }

        private void FullScreen(bool full)
        {
            FormBorderStyle = full ? FormBorderStyle.None : FormBorderStyle.Sizable;
            TopMost = full;
            //TopLevel = full;
            this.WindowState = FormWindowState.Maximized;
        }

    }
}
