using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

using VNPT.HIS.Common;

namespace L1_Mini
{
    public partial class Config : Form
    {
        bool old_full_screen;
        Microsoft.Win32.RegistryKey rkApp = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public Config(bool old_full_screen, string call_from = "")
        {
            InitializeComponent();

            ckbFull.Checked = old_full_screen;
            this.old_full_screen = old_full_screen;

            // mặc định call_from = "" gọi từ from SttKham
            if (call_from.ToLower() == "frmmain")// màn hình đăng nhập
            {
                // từ form main thì ẩn fulll màn hình
                ckbFull.Visible = false;
                ckbAutoLogin.Visible = false;
            }

            
            //if (Const.L1_BV_DEFAULT == 4)
            //{
            //    cboCoSo.Visible = true;
            //}
             
        } 
        private void Config_Load(object sender, EventArgs e)
        { 
            if (Const.L1_kieucapso == "2")
                rb2.Checked = true;
            else if (Const.L1_kieucapso == "3")
                rb3.Checked = true;
            else
                rb1.Checked = true;
              
            hien_thi_config(); 
        }
        private void hien_thi_config()
        {
            Func.get_config_file();

            cboCoSo.SelectedIndex = 0;
            if (Const.L1_CoSoID == "7984") cboCoSo.SelectedIndex = 1;
            else if (Const.L1_CoSoID == "7985") cboCoSo.SelectedIndex = 2; 
            txtLoaiBHYT.Text = Const.L1_LoaiBHYT;


            ckbXemPhieuSTT.Checked = Const.L1_XemPhieuSTT;

            //this.cboLogo.SelectedIndexChanged -= new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            cboLogo.SelectedIndex = Const.L1_BV_DEFAULT;
            //this.cboLogo.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);

            if (ckbAutoLogin.Visible) ckbAutoLogin.Checked = Const.L1_autologin;
            txtServer.Text = Const.LinkService;// server_old;

            //cấp số kiểu nhập mã tìm kiếm
            ckbDangky.Checked  = Const.L1_dkkham == "1";// Func.load_config("dangkykham").ToLower() == "true";
            ckbCheckBHYT.Checked = Const.L1_ktraBHYT == "1";//Func.load_config("checkbhyt").ToLower() == "true";

            //cấp số kiểu quẹt mã BN
            ckbTuDongIn.Checked = Const.L1_autoPrinter == "1";//Func.load_config("tudongin").ToLower() == "true";

            string phanhe = Const.L1_phanhe;
            int mode = 1;
            if (phanhe.IndexOf("_") > -1)
            {
                phanhe = phanhe.Substring(2);
                mode = Func.Parse(Const.L1_phanhe.Substring(0, 1));
            }

            cboMode.SelectedIndex = mode;

            for (int i = 0; i < cboPhanHe.Items.Count; i++)
                    if (cboPhanHe.Items[i].ToString().StartsWith("(" + phanhe + ")"))
                        cboPhanHe.SelectedIndex = i;

            ckbStart.Checked = (rkApp.GetValue("run_app_mini") != null);


            txtAcsTcp_IP.Text = Const.AcsTcp_IP;
            txtAcsTcp_Port.Text = Const.AcsTcp_Port;


        }
        private void thay_doi_kieu_cap_so()
        {
            if (cboMode.SelectedIndex == -1) cboMode.SelectedIndex = 1;

            if (rb1.Checked)
            {
                //Hiển thị combo chọn phân hệ
                label4.Visible = false;
                cboMode.Visible = false;
                cboPhanHe.Visible = false; 

                // hiển thị chọn Tự động in
                ckbTuDongIn.Visible = false; 
                
                // ẩn chọn: Nút đăng ký khám
                ckbDangky.Visible = true;
                //ẩn chọn: check thẻ BHYT
                ckbCheckBHYT.Visible = true;

                //IP - Port thẻ admin
                label5.Visible = true; txtAcsTcp_IP.Visible = true; txtAcsTcp_Port.Visible = true;
            }
            else if (rb2.Checked)//màn Quẹt mã bệnh nhân
            {
                //Hiển thị combo chọn phân hệ
                label4.Visible = true;
                cboMode.Visible = false;
                cboMode.SelectedIndex = 0;
                cboPhanHe.Visible = true; cboPhanHe.Location = cboMode.Location;
                //cboPhanHe.Location = ckbCheckBHYT.Location;

                // hiển thị chọn Tự động in
                ckbTuDongIn.Visible = true;
                ckbTuDongIn.Location = ckbDangky.Location;
                
                // ẩn chọn: Nút đăng ký khám
                ckbDangky.Visible = false;
                //ẩn chọn: check thẻ BHYT
                ckbCheckBHYT.Visible = false;

                //IP - Port thẻ admin
                label5.Visible = true; txtAcsTcp_IP.Visible = true; txtAcsTcp_Port.Visible = true;
            }
            else if (rb3.Checked)//màn kiểu các button
            {
                //Hiển thị combo chọn phân hệ
                label4.Visible = true;
                cboMode.Visible = true;
                cboPhanHe.Visible = true; cboPhanHe.Location = new Point(cboMode.Location.X + cboPhanHe.Size.Width + 12, cboMode.Location.Y);
                //cboPhanHe.Location = ckbCheckBHYT.Location;

                // hiển thị chọn Tự động in
                ckbTuDongIn.Visible = false;
                ckbTuDongIn.Location = ckbDangky.Location; 
                // ẩn chọn: Nút đăng ký khám
                ckbDangky.Visible = false;
                //ẩn chọn: check thẻ BHYT
                ckbCheckBHYT.Visible = false;

                //IP - Port thẻ admin
                label5.Visible = false; txtAcsTcp_IP.Visible = false; txtAcsTcp_Port.Visible = false;

            }
        }


        protected EventHandler ReturnData; 
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            if (ReturnData != null) ReturnData(old_full_screen.ToString(), null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ===
            // === Biến lưu local
            // ===
            Const.L1_CoSoID = "";
            if (cboCoSo.SelectedIndex == 1) Const.L1_CoSoID = "7984";
            else if (cboCoSo.SelectedIndex == 2) Const.L1_CoSoID = "7985";
            Const.L1_LoaiBHYT = txtLoaiBHYT.Text.Trim();

            Const.L1_BV_DEFAULT = cboLogo.SelectedIndex; // 0:TEST; 1:BM2; 2:VD2; 3:BND; 4:KHA; 5:HNM;
            string full_path_logo;
            Func.Reset_Logo_BenhVien(out full_path_logo);// gọi để thiết lập biến Const.L1_MAU_STT
            Const.L1_XemPhieuSTT = ckbXemPhieuSTT.Checked;            
            string cu = Const.LinkService;
            Const.LinkService = txtServer.Text.Trim(); 
            if (ckbAutoLogin.Visible) Const.L1_autologin = ckbAutoLogin.Checked;
            Const.AcsTcp_IP = txtAcsTcp_IP.Text;
            Const.AcsTcp_Port = txtAcsTcp_Port.Text;

            object data_local = new
            {
                L1_BV_DEFAULT = Const.L1_BV_DEFAULT,
                L1_mini = Const.L1_mini,
                L1_MAU_STT = Const.L1_MAU_STT,
                L1_XemPhieuSTT = Const.L1_XemPhieuSTT,
                L1_autologin = Const.L1_autologin,
                LinkService = Const.LinkService,
                AcsTcp_IP = Const.AcsTcp_IP,
                AcsTcp_Port = Const.AcsTcp_Port,
                L1_CoSoID= Const.L1_CoSoID
                ,L1_LoaiBHYT= Const.L1_LoaiBHYT
            };
            string json = "[" + Newtonsoft.Json.JsonConvert.SerializeObject(data_local) + "]";
            Func.config("LOCAL_CAUHINH_KIOS", json);
            Func.config("AcsTcp_IP", txtAcsTcp_IP.Text);
            Func.config("AcsTcp_Port", txtAcsTcp_Port.Text);
            Func.config("LinkService", Const.LinkService);

            // ===
            // === Lưu khởi động cùng window
            // === 
            {
                if (ckbStart.Checked)
                    rkApp.SetValue("run_app_mini", Application.ExecutablePath.ToString());
                else
                {
                    if (rkApp.GetValue("run_app_mini") != null) rkApp.DeleteValue("run_app_mini");
                }
            }

            // ===
            // === Biến lưu Server
            // ===
            if (rb1.Checked) Const.L1_kieucapso = "1";
            else if (rb2.Checked) Const.L1_kieucapso = "2";
            else if (rb3.Checked) Const.L1_kieucapso = "3";

            Const.L1_dkkham = ckbDangky.Checked ? "1" : "0";
            Const.L1_ktraBHYT = ckbCheckBHYT.Checked ? "1" : "0";
            Const.L1_autoPrinter = ckbTuDongIn.Checked ? "1" : "0";
             
            try
            {
                string id = cboPhanHe.Text.Substring(0, cboPhanHe.Text.IndexOf(" ")).Trim();
                id = id.Substring(1, id.Length - 2);
                Const.L1_phanhe = cboMode.SelectedIndex + "_" + id;
            } catch(Exception ex) { }

              
            // LƯU LÊN SERVER
            object data_server = new
            {
                L1_kieucapso = Const.L1_kieucapso,
                L1_dkkham = Const.L1_dkkham,
                L1_ktraBHYT = Const.L1_ktraBHYT,
                L1_autoPrinter = Const.L1_autoPrinter,
                L1_phanhe = Const.L1_phanhe 
            };
            string gia_tri = "[" + Newtonsoft.Json.JsonConvert.SerializeObject(data_server) + "]";
            gia_tri = gia_tri.Replace("\"","\\\"");
            string mo_ta = "kieu|dkkham|ktraBHYT|autoPrinter|phanhe";
            string ten_cau_hinh = "Cấu hình cho app tại Kios";
            //string gia_tri = Const.L1_kieucapso + "|" + Const.L1_dkkham + "|" + Const.L1_ktraBHYT + "|" + Const.L1_autoPrinter + "|" + Const.L1_phanhe;
                        //Func.config("CAUHINH_KIOS", gia_tri);
            if (Const.local_user != null)
            {
                string save_CAUHINH = RequestHTTP.call_ajaxCALL_SP_I("API.CAUHINH.UPD", "CAUHINH.KIOS$" + ten_cau_hinh + "$" + mo_ta + "$" + gia_tri);
            }


            Func.set_config_file();
             
            // ===
            // === Trở lại form trước
            // === 
            if (ReturnData != null)
            {
                if (cu != Const.LinkService)
                {
                    MessageBox.Show("Đã lưu cấu hình. Link server bị thay đổi nên cần phải thoát ra đăng nhập lại!");
                    ReturnData("dang_nhap_lai", null);
                }
                else
                if (thoat)
                {
                    ReturnData("thoat", null);
                }
                else
                {
                    ReturnData(ckbFull.Checked.ToString().ToLower(), null);
                }
            }
            this.Close();
        }
        bool thoat = false;
        private void button3_Click(object sender, EventArgs e)
        {
            thoat = true;
            button1_Click(null, null);
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            cboPhanHe.DroppedDown = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1.Checked)
            {
                rb2.Checked = false;
                rb3.Checked = false;
                thay_doi_kieu_cap_so();
            }
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2.Checked)
            {
                rb1.Checked = false;
                rb3.Checked = false;
                thay_doi_kieu_cap_so();
            }
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3.Checked)
            {
                rb1.Checked = false;
                rb2.Checked = false;
                thay_doi_kieu_cap_so();
            }
        }

        private void ckbStart_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckbFull_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckbAutoLogin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxCauHinhRieng.Text = cboLogo.Text;
            foreach (Control X in boxCauHinhRieng.Controls) X.Visible = false;
            //cboCoSo.Visible = false;

            // 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM;  
            if (cboLogo.SelectedIndex == 0)
            {
                txtServer.Text = "https://histestl2.vncare.vn/vnpthis/RestService";
                if (Const.default_test == 1)
                {
                }
                else if (Const.default_test == 2)
                {
                }
                else if (Const.default_test == 3)
                {
                }
                else if (Const.default_test == 4)
                {
                    cboCoSo.Visible = true;
                    lbLoaiBHYT.Visible = true;
                    lbLoaiBHYT2.Visible = true;
                    txtLoaiBHYT.Visible = true;
                }
                else if (Const.default_test == 5) // đk hà nam
                { 
                }
                else if (Const.default_test == 6) // // đk chung: BVDAKHOA
                {
                }
            }
            else if (cboLogo.SelectedIndex == 1)
                txtServer.Text = "https://bvbachmai2.vncare.vn/vnpthis/RestService";
            else if (cboLogo.SelectedIndex == 2)
                txtServer.Text = "https://bvvietduc2.vncare.vn/vnpthis/RestService";
            else if (cboLogo.SelectedIndex == 3)
                txtServer.Text = "https://bvtamthan.vnptsoftware.vn/vnpthis/RestService";
            else if (cboLogo.SelectedIndex == 4)
            {
                txtServer.Text = "https://benhnhietdoi.vncare.vn/vnpthis/RestService";
                cboCoSo.Visible = true;
                lbLoaiBHYT.Visible = true;
                lbLoaiBHYT2.Visible = true;
                txtLoaiBHYT.Visible = true;
            }
            else if (cboLogo.SelectedIndex == 5) // đk hà nam
            {
                txtServer.Text = "https://dakhoahanam.vnptsoftware.vn/vnpthis/RestService";
            }
            else if (cboLogo.SelectedIndex == 6) // // đk chung: BVDAKHOA
            {
                txtServer.Text = "https://bvdakhoa.vncare.vn/vnpthis/RestService";
            }
            else
            {
                txtServer.Text = "https://histestl2.vncare.vn/vnpthis/RestService";
            }
        }

        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMode.SelectedIndex == 0)
            {
                cboPhanHe.Enabled = true;
                if (cboPhanHe.SelectedIndex == -1) cboPhanHe.SelectedIndex = 0;
            }
            else
                cboPhanHe.Enabled = false;
        }
    }
}
 