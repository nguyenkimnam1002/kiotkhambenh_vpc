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

// CÁC BIẾN CẤU HÌNH LƯU TRÊN WEB: L1_dkkham
// KIOS_APP_BG : ẢNH NỀN BACKGROUD, mặc định bg_default.png
// KIOS_APP_MAUSTT : phiếu in STT (Const.L1_MAU_STT)
// KIOS_APP_XEMSTT : 0 chế độ chỉ xem phiếu, ko in (Const.L1_XemPhieuSTT) mặc định 1 có in
// KIOS_APP_KIEUCAPSO : kiểu cấp số 1 nhập mã tk, 2 quét mã bn, 3 chọn loại khám (Const.L1_kieucapso) mặc định 1

// KIOS_APP_DKKHAM : 1 hiển thị nút đăng ký khám; 0 ẩn (Const.L1_dkkham) mđ:1
// KIOS_APP_KTRBHYT : check thẻ bhyt, mđ 1 có check (Const.L1_ktraBHYT)


// KIOS_APP_AUTOPRINT : dùng cho kiểu: cấp số kiểu quẹt mã BN (Const.L1_autoPrinter) mđ 1 auto in; 0 ko in (có thể dùng biến KIOS_APP_XEMSTT)
// KIOS_APP_PHANHE :   (Const.L1_phanhe) // dạng: mode_phanhe vidu: 1_2
// KIOS_APP_KTR_LOAIBHYT : Chỉ tiếp nhận với các loại BHYT này, cách nhau dấu phẩy,(Const.L1_LoaiBHYT) mđ: 0 hoặc rỗng là ko cần kiểm tra(1:ĐT 2:ĐTGT 3:ĐTCC 4:TrT 5:ThôngT) 

    //Cấu hình trong phiếu in
// KIOS_APP_TENBV : Tên bệnh viện ghi trong phiếu STT
// KIOS_APP_MAUSTT : phiếu in STT (Const.L1_MAU_STT)
// KIOS_APP_MAUSTT : phiếu in STT (Const.L1_MAU_STT)
// KIOS_APP_MAUSTT : phiếu in STT (Const.L1_MAU_STT)

namespace L1_Mini
{
    public partial class ConfigNew : Form
    {
        bool old_full_screen;
        Microsoft.Win32.RegistryKey rkApp = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public ConfigNew(bool old_full_screen, string call_from = "")
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

            if (Const.local_user != null)
            {
                DataTable cauhinh = RequestHTTP.get_ajaxExecuteQueryO("KIOS.DSCAUHINH", Const.local_user.HOSPITAL_ID);
                // SELECT a.macauhinh, a.tencauhinh, a.giatri_macdinh,b.giatri_thietlap,b.csyt_id
                // FROM dmc_cauhinh a, DMC_CAUHINH_THIETLAP b
                // where a.cauhinhid = b.cauhinhid  and b.csyt_id =[HID] and a.macauhinh like 'KIOS_%'
                dataGridView1.DataSource = cauhinh;
            }
        } 
        private void ConfigNew_Load(object sender, EventArgs e)
        { 
            //ko dung: Const.L1_CoSoID Const.L1_LoaiBHYT 
             
              
            if (ckbAutoLogin.Visible) ckbAutoLogin.Checked = Const.L1_autologin; 
            txtServer.Text = Const.LinkService;
            ckbStart.Checked = (rkApp.GetValue("run_app_mini") != null); // khởi động cùng win
        }
        
        private void thay_doi_kieu_cap_so()
        { 
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

        private void btnLuu_Click(object sender, EventArgs e)
        {          
            string cu = Const.LinkService;
            Const.LinkService = txtServer.Text.Trim(); 
            if (ckbAutoLogin.Visible) Const.L1_autologin = ckbAutoLogin.Checked; 
 
            object data_local = new
            { 
                L1_autologin = Const.L1_autologin,
                LinkService = Const.LinkService 
            };
            string json = "[" + Newtonsoft.Json.JsonConvert.SerializeObject(data_local) + "]";
            System.IO.File.WriteAllText(Const.FolderSaveFilePrint + "\\data.dat", Func.ToHex(json));



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
        private void btnLuuThoat_Click(object sender, EventArgs e)
        {
            thoat = true;
            btnLuu_Click(null, null);
        }
         
    }
}
 