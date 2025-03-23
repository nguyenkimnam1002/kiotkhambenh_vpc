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
             
            Func.get_config_from_server();
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
            ckbXemPhieuSTT.Checked = Const.L1_XemPhieuSTT;

            cboLogo.SelectedIndex = Const.L1_logo;

            if (ckbAutoLogin.Visible) ckbAutoLogin.Checked = Const.L1_autologin == "1";// auto_login;
            txtServer.Text = Const.L1_server;// server_old;

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

            txtAcsTcp_IP.Text = Func.load_config("AcsTcp_IP");
            txtAcsTcp_Port.Text = Func.load_config("AcsTcp_Port");


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
            Const.L1_XemPhieuSTT = ckbXemPhieuSTT.Checked;

            Const.L1_logo = cboLogo.SelectedIndex;
            Func.config("L1_logo", Const.L1_logo.ToString());

            Func.config("AcsTcp_IP", txtAcsTcp_IP.Text);
            Func.config("AcsTcp_Port", txtAcsTcp_Port.Text);


            // 2 biến lưu ở local
            Const.L1_server = txtServer.Text.Trim();
            Const.LinkService = Const.L1_server;
            Func.config("LinkService", Const.L1_server);

            //Các biến lưu lên sv
            if (ckbAutoLogin.Visible)
            {
                Const.L1_autologin = ckbAutoLogin.Checked ? "1" : "0";
                Func.config("l1_mini", ckbAutoLogin.Checked ? Const.L1_mini : "");
            }

            if (rb1.Checked) Const.L1_kieucapso = "1";
            else if (rb2.Checked) Const.L1_kieucapso = "2";
            else if (rb3.Checked) Const.L1_kieucapso = "3";

            Const.L1_dkkham = ckbDangky.Checked ? "1" : "0";
            Const.L1_ktraBHYT = ckbCheckBHYT.Checked ? "1" : "0";
            Const.L1_autoPrinter = ckbTuDongIn.Checked ? "1" : "0";
            //comboBox1
            try
            {
                string id = cboPhanHe.Text.Substring(0, cboPhanHe.Text.IndexOf(" ")).Trim();
                id = id.Substring(1, id.Length - 2);
                Const.L1_phanhe = cboMode.SelectedIndex + "_" + id;
            } catch(Exception ex) { }

            //ckbStart
            {  
                if (ckbStart.Checked)
                    rkApp.SetValue("run_app_mini", Application.ExecutablePath.ToString());
                else
                {
                    if (rkApp.GetValue("run_app_mini") != null) rkApp.DeleteValue("run_app_mini");
                }
            }

            // LƯU LÊN SERVER
            string mo_ta = "kieu|dkkham|ktraBHYT|autoPrinter|phanhe";
            string ten_cau_hinh = "Cấu hình cho app tại Kios";
            string gia_tri = Const.L1_kieucapso + "|" + Const.L1_dkkham + "|" + Const.L1_ktraBHYT + "|" + Const.L1_autoPrinter + "|" + Const.L1_phanhe;
            
            Func.config("CAUHINH_KIOS", gia_tri);

            if (Const.local_user != null)
            {
                string save_CAUHINH = RequestHTTP.call_ajaxCALL_SP_I("API.CAUHINH.UPD", "CAUHINH.KIOS$" + ten_cau_hinh + "$" + mo_ta + "$" + gia_tri);
            }

            // trả về
            if (ReturnData != null)
            {
                if (thoat) ReturnData("thoat", null);
                else ReturnData(ckbFull.Checked.ToString().ToLower(), null);
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
 