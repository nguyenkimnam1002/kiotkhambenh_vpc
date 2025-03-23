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

// CÁC BIẾN CẤU HÌNH LƯU TRÊN WEB: 
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

// KIOS_APP_LISTSV : danh sách server
// KIOS_APP_CHUYEN2KIEU : cho phép Kiểu khám 1 và 3 chung trên form (dùng cho bv VIME)
//  

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

            //L2PT-6476 --cau hinh khoa phong dat kios
            /*if(Const.KHOAIID == "-1" || Const.PHONGID == "-1")
            {
                try
                {
                    DataTable dtLNDV = RequestHTTP.getDanhsach_Loainhom_DV();
                    if (dtLNDV == null) dtLNDV = new DataTable();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataRow row in dtLNDV.Rows)
                    {
                        string loaiid = row["loai_id"].ToString();
                        string description = row["tenloai"].ToString();
                        dic.Add(loaiid, description);

                    }

                    cboChonnhanh.Items.Add("");
                    for (int i = 0; i < chonnhanh.Length; i++)
                    {
                        chonnhanhName[i] = chonnhanh[i].Substring(0, chonnhanh[i].IndexOf("-"));
                        chonnhanhVal[i] = chonnhanh[i].Substring(chonnhanh[i].IndexOf("-") + 1);
                        cboChonnhanh.Items.Add(chonnhanhName[i]);
                    }
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                }
                
            }*/
            //END L2PT-6476

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

            try
            {
                string KIOS_APP_LISTSV = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_LISTSV");
                if (KIOS_APP_LISTSV.Length <= 1)
                    KIOS_APP_LISTSV = "HisTestL2-histestl2.vncare.vn;BDHCM-dakhoabuudien.vncare.vn;BENHNHIETDOI-benhnhietdoi.vncare.vn;" +
                        "BVDAKHOA-bvdakhoa.vncare.vn;BVNT-bvnguyentrai.vncare.vn;BVSANNHI-bvnhihaiduong.vncare.vn;DKHNM-dakhoahanam.vncare.vn;" +
                        "HISDUNGCHUNG-his.vncare.vn;K74VP-benhvien74tw.vncare.vn;LAOPHOI-bvphoi.vncare.vn;LPLAN-laophoilongan.vncare.vn;" +
                        "MATHNM-bvmathanam.vncare.vn;NHQNM-nhiquangnam.vncare.vn;QUANY15GLI-bvquany.vncare.vn;SNHNM-sannhihanam.vncare.vn;" +
                        "TAMTHAN-bvtamthan.vncare.vn;YDCT-bvyhct.vncare.vn;BVLACVIET-bvlacviet.vncare.vn";
                string[] chonnhanh = KIOS_APP_LISTSV.Split(';');
                cboChonnhanh.Items.Add("");
                for (int i = 0; i < chonnhanh.Length; i++)
                {
                    chonnhanhName[i]=  chonnhanh[i].Substring(0, chonnhanh[i].IndexOf("-"));
                    chonnhanhVal[i] =  chonnhanh[i].Substring(chonnhanh[i].IndexOf("-") + 1); 
                    cboChonnhanh.Items.Add(chonnhanhName[i]);
                }
            }
            catch (Exception ex) { }
        }
        string[] chonnhanhName = new string[100];
        string[] chonnhanhVal = new string[100];
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
        
        private void btnDong_Click(object sender, EventArgs e)
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
                LinkService = Const.LinkService,
                L1_mini = Const.L1_mini
            };
            string json = "[" + Newtonsoft.Json.JsonConvert.SerializeObject(data_local) + "]";
            System.IO.File.WriteAllText(Const.FolderSaveFilePrint + "\\" + Const.config_data, Func.ToHex(json));



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

        private void cboChonnhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChonnhanh.SelectedIndex > 0)
                txtServer.Text = "https://" + chonnhanhVal[cboChonnhanh.SelectedIndex-1] + "/vnpthis/RestService";
         
        }
  
    }
}
 