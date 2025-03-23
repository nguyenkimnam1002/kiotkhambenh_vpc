using System;
using System.Threading;
using System.Data;
using VNPT.HIS.Common;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
//using GIGATMS.NF;
using System.Configuration;
using System.Drawing;
using AccessTcp;
using System.Drawing.Printing;

// 
//  login BND: bnd.admin / Test@2018
//

namespace L1_Mini
{
    public partial class BVDAKHOA_Kios : Form
    {
        public BVDAKHOA_Kios(bool parent_fullscreen = false)
        {
            InitializeComponent();

                

            if (parent_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                TopMost = parent_fullscreen;
                if (TopMost) TopLevel = true;
            }

            // in_file_stt("Nguyễn Hồng Thị Nguyên", "Mã thẻ BN", "", "BN123123");
        }
        private void CapSo_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void BVDAKHOA_Kios_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            //Init
            //if (Const.L1_dkkham != "1") btnDangKy.Visible = false;
            lbLoi.Text = "";
            lbTheUutien.Text = "";
            hideBARCODE_BHYT = "";
            txtMaBN.Text = "";
            txtHoTen.Text = "";
            txtMaBN.Focus();

            //Thread.Sleep(100);

            //Nhận đầu đọc thẻ
            //string dau_doc_the = Scan_Port();

            //DialogResult dlResult = MessageBoxEx.Show(dau_doc_the==""?"Không tìm thấy đầu đọc thẻ!"
            //    :"Xác nhận đầu đọc thẻ: "+ m_szVersion + "  " + m_szCaption + " - tại: " + m_szLastCommPort
            //, 2000);
            //lbCom.Text = m_szLastCommPort+":";

            //Nhận đầu đọc thẻ
            //InitReadCard();

            txtMaBN.Focus();

            //MessageBox.Show(Size.Height + "");\
            //if (Size.Height < 800)
            //{
            //    lbThongbao.Font= new System.Drawing.Font("Tahoma", 25F, System.Drawing.FontStyle.Bold);
            //}
            // kios: w 1296 x h 1040; 230px --> 22%

            Reset_CauHinh_BenhVien();


            //MABENHAN = "BN123123123"; GIOITINHID = "Nam"; NAMSINH = "1985"; SDTBENHNHAN = "0984888911";
            //in_file_stt("Lê việt Hưng", "", "", "123123");
        }

        private void Reset_CauHinh_BenhVien()
        { 
                string KIOS_APP_BG = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_BG");
                string full_path_logo1 = Application.StartupPath + ("./Resources/" + KIOS_APP_BG).Substring(1).Replace("/", "\\");
                this.BackgroundImage = Func.getIconFullPath(full_path_logo1);
                this.BackgroundImageLayout = ImageLayout.Stretch; 

            //AcsTcp_IP = ConfigurationManager.AppSettings["AcsTcp_IP"];
            //AcsTcp_Port = Func.Parse(ConfigurationManager.AppSettings["AcsTcp_Port"]);

        }


        #region XỬ LÝ WORD 
        string error = "";
        private bool in_file_stt(string HoTen, string TheBH, string Muc, string STT)
        {
            try
            {
                //Lấy dữ liệu  
                string KIOS_APP_MAUSTT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT");
                string Report = KIOS_APP_MAUSTT;// Const.L1_MAU_STT;//"MAU_STT";

                //Thông tin file mẫu và file lưu ra
                string MAU = @"./Resources/" + Report;
                string NAME = Report + "_TEST_" + DateTime.Now.ToString("yyMMddHHmmss") + "_" + new Random().Next(1000);
                string OUT = @"./Resources/" + NAME;


                string MAU_FILE_DOCX = Application.StartupPath + MAU.Substring(1).Replace("/", "\\") + ".docx";
                string OUT_FILE_DOC = Application.StartupPath + OUT.Substring(1).Replace("/", "\\") + ".docx";
                //string MAU_FILE_DOCX = MAU + ".docx";
                //string OUT_FILE_DOC = OUT + ".docx";


                //Copy tạo 1 file mẫu mới
                File.Copy(MAU_FILE_DOCX, OUT_FILE_DOC, true);

                //Đọc file html to text
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(OUT_FILE_DOC, true))
                {
                    // Xử lý text
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    docText = replace_text(docText, HoTen, ten_Pkham, Muc, STT);

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }

                }

                //string fullName = Application.StartupPath + OUT_FILE_DOC.Substring(1).Replace("/","\\");
                string KIOS_APP_XEMSTT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_XEMSTT");                
                if (KIOS_APP_XEMSTT == "0")
                    System.Diagnostics.Process.Start(OUT_FILE_DOC);
                else
                { 
                    VNPT.HIS.Common.Func.Print_80mm(OUT_FILE_DOC, false);
                    this.WindowState = FormWindowState.Maximized;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }

            return false;
        }

        // string xml_thebh = "<w:p w14:paraId=\"344EE517\" w14:textId=\"4F9FBB03\" w:rsidR=\"0071440C\" w:rsidRPr=\"00F87C57\" w:rsidRDefault=\"00283741\" w:rsidP=\"00E24660\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"1701\"/></w:tabs><w:spacing w:after=\"0\" w:line=\"240\" w:lineRule=\"auto\"/><w:ind w:right=\"301\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>T</w:t></w:r><w:r w:rsidR=\"0071440C\" w:rsidRPr=\"0071440C\"><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>hẻ BH:</w:t></w:r></w:p><w:p w14:paraId=\"6A65EB5D\" w14:textId=\"472DD0DA\" w:rsidR=\"0071440C\" w:rsidRDefault=\"00771C02\" w:rsidP=\"00E24660\"><w:pPr><w:tabs><w:tab w:val=\"left\" w:pos=\"1701\"/></w:tabs><w:spacing w:after=\"0\" w:line=\"240\" w:lineRule=\"auto\"/><w:ind w:right=\"301\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>@thebh@</w:t></w:r></w:p>";

        string xml_hoten_thebh = "<w:p w14:paraId=\"782965AF\" w14:textId=\"7E00E8B1\" w:rsidR=\"00052F27\" w:rsidRDefault=\"00283741\" w:rsidP=\"00FF68B2\"><w:pPr><w:spacing w:after=\"120\" w:line=\"240\" w:lineRule=\"auto\"/><w:ind w:right=\"-368\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>@hoten@</w:t></w:r></w:p><w:p w14:paraId=\"6A65EB5D\" w14:textId=\"093FD050\" w:rsidR=\"0071440C\" w:rsidRPr=\"006A3470\" w:rsidRDefault=\"001A3437\" w:rsidP=\"005E2955\"><w:pPr><w:spacing w:after=\"0\" w:line=\"240\" w:lineRule=\"auto\"/><w:ind w:right=\"-368\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r w:rsidRPr=\"001A3437\"><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>Thẻ BH:</w:t></w:r><w:r w:rsidRPr=\"001A3437\"><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> @thebh@</w:t></w:r></w:p>";
        private string replace_text(string docText, string HoTen, string ten_Pkham, string Muc, string STT)
        {
            DateTime NgayBao = DateTime.Now;
            string KIOS_APP_TENBV = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_TENBV");
            docText = docText.Replace("@tenbenhvien@", KIOS_APP_TENBV);

            if (ten_Pkham == "" && HoTen == "")
            {
                docText = docText.Replace(xml_hoten_thebh, "");

                docText = docText.Replace("@hoten@", "");

                docText = docText.Replace("Thẻ BH:", "");
                docText = docText.Replace("@thebh@", "");
            }
            else
            {
                docText = docText.Replace("@hoten@", HoTen);

                if (ten_Pkham == "")
                {
                    docText = docText.Replace("Thẻ BH:", "");
                    docText = docText.Replace("@thebh@", "");
                }
                else
                {
                    docText = docText.Replace("Thẻ BH: ", "");
                    docText = docText.Replace("Thẻ BH:", "");

                    docText = docText.Replace(" @thebh@", "@thebh@");
                    docText = docText.Replace("@thebh@", ten_Pkham);
                }
            }

            docText = docText.Replace("@muc@", Muc);
            docText = docText.Replace("@stt@", STT);
            docText = docText.Replace("@ngay@", DateTime.Now.ToString("dd/MM/yyyy, HH:mm"));

            docText = docText.Replace("@mabn@", hide_Benh_An); //  Mã bệnh án.
            docText = docText.Replace("@gioitinh@", GIOITINHID);
            docText = docText.Replace("@namsinh@", NAMSINH);
            docText = docText.Replace("@sdt@", SDTBENHNHAN);
            docText = docText.Replace("@dc@", DIACHI);
            return docText;
        }
        #endregion


        #region XỬ LÝ SK MÀN HÌNH CHÍNH
        private void XuLy_NhapMaBN()
        {
            string input = txtMaBN.Text.Trim().ToUpper();
            hideBARCODE_BHYT = "";
            // Nhập mã thẻ BH
            if (input.Length == 15)
            {
                string MaBaoHiem = input;
                Cap_STT_Kham(txtHoTen.Text.Trim(), "", MaBaoHiem);
            }
            // Quẹt thẻ BHYT
            else if (input.Length > 15 && input.IndexOf("|") > -1)
            {
                txtMaBN.Text = "";
                XuLy_QuetTheBHYT(input);
            }
            // Nhập Mã BN
            else
            {
                string MaBN = input;
                //Tìm kiếm theo Mã bệnh nhân
                Cap_STT_Kham(txtHoTen.Text.Trim(), MaBN, "");
            }
        }
        private void XuLy_NhapHoTen()
        {
            string input = txtHoTen.Text.Trim();
            hideBARCODE_BHYT = "";
            // Quẹt thẻ BHYT
            if (input.Length > 15 && input.IndexOf("|") > -1)
            {
                txtHoTen.Text = "";
                XuLy_QuetTheBHYT(input);
            }
            // Nhập Tên BN
            else
            {
                string HoTen = input;

                string MaBaoHiem = "";
                string MaBN = "";
                if (txtMaBN.Text.Trim().Length == 15)
                    MaBaoHiem = txtMaBN.Text.Trim();
                else
                    MaBN = txtMaBN.Text.Trim();

                //Tìm kiếm theo Ten bệnh nhân
                Cap_STT_Kham(HoTen, MaBN, MaBaoHiem);
            }
        }
        private void XuLy_QuetTheBHYT(string input)
        {
            hideBARCODE_BHYT = input;
            string str_sobhyt = input;// DN401AA31900313|4cc3aa205669e1bb87742048c6b06e67|04/09/1985|1|437479207068e1baa76e206de1bb816d20564e50542d20434e2074e1bb956e6720437479204456205654|01 - 043|01/07/2017|31/12/2017|21/07/2017|01059710639215|-|4|01/07/2022|1f2fa22c47f0bd18-7102|$
            string[] sobhyt_catchuoi = str_sobhyt.Split('|');

            string hoten = Func.FromHex(sobhyt_catchuoi[1]);
            string sobhyt = sobhyt_catchuoi[0];
            //Tìm kiếm theo số BHYT
            input = input.Replace("$", "**##");
            Cap_STT_Kham(hoten.ToUpper(), "", sobhyt, input);
        }

        DataTable dtTT_BenhNhan = new DataTable();
        //string DOI_TUONG_ID = "";
        string hideBARCODE_BHYT = "";

        string hoten_temp = "";
        string mathe_temp = "";
        private void HienThi_TenBN_TheoMaTimKiem()
        {
            string input = txtMaBN.Text.Trim().ToUpper();

            // Nhập mã thẻ BH
            if (input.Length == 15)
            {
                hideBARCODE_BHYT = "";
                string MaBaoHiem = input;
                //TimKiem_BN(txtHoTen.Text.Trim(), "", MaBaoHiem);

                if (mathe_temp == MaBaoHiem)
                    txtHoTen.Text = hoten_temp;

                dtTT_BenhNhan = TimKiem_BenhNhan(MaBaoHiem, "2");
                if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                    txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
            }
            // Quẹt thẻ BHYT
            else if (input.Length > 15 && input.IndexOf("|") > -1)
            {
                string[] sobhyt_catchuoi = input.Split('|');
                string MaBaoHiem = sobhyt_catchuoi[0];
                mathe_temp = MaBaoHiem;
                hoten_temp = Func.FromHex(sobhyt_catchuoi[1]);
                txtMaBN.Text = MaBaoHiem;
                hideBARCODE_BHYT = input;
                //XuLy_QuetTheBHYT(input);

                dtTT_BenhNhan = TimKiem_BenhNhan(MaBaoHiem, "2");

                txtHoTen.Text = hoten_temp;

                if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                    txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
            }
            // Nhập Mã BN
            else
            {
                string MaBN = input;
                hideBARCODE_BHYT = "";
                //Tìm kiếm theo Mã bệnh nhân
                //TimKiem_BN(txtHoTen.Text.Trim(), MaBN, "");

                txtHoTen.Text = "";
                dtTT_BenhNhan = TimKiem_BenhNhan(MaBN, "1");
                if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                    txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
            }
        }

        // Hàm cấp số thứ tự
        private void Cap_STT_Kham(string HoTen, string MaBN, string MaBHYT, string barcode = "")
        {
            try
            {
                //if (Const.local_user.HOSPITAL_ID == "951")// bv nhiệt đới
                {
                    if (hide_STT == "") return;

                    lbLoi.Text = "";
                    if (in_file_stt(dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString(), "", "", hide_STT))
                    {
                        txtMaBN.Text = "";
                        txtHoTen.Text = "";
                        hideBARCODE_BHYT = "";
                        txtMaBN.Focus();

                        DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3000);
                    }
                }

                // sau mỗi lần tìm kiếm BN thì reset thẻ
                CARD_VALUE_HEX = "";
                CARD_VALUE = "";
                lbTheUutien.Text = "";
                btnDangKy.Visible = false;
                txtMaBN.Text = "";
                txtHoTen.Text = "";
                dtTT_BenhNhan = new DataTable();
                hideBARCODE_BHYT = "";
                txtMaBN.Focus();
            }
            catch (Exception ex) { }
        }
        
        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            //System.Console.WriteLine("vào   txtMaBN_Leave");
            HienThi_TenBN_TheoMaTimKiem();
            //XuLy_NhapMaBN();
        }
        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //System.Console.WriteLine("vào   txtMaBN_KeyDown");
                HienThi_TenBN_TheoMaTimKiem();
                txtHoTen.Focus();

                //System.Console.WriteLine("do " + txtMaBN.Text);
                //XuLy_NhapMaBN();
            }
        }
        private void txtHoTen_Leave(object sender, EventArgs e)
        {
            //XuLy_NhapHoTen();
        }
        private void txtHoTen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                XuLy_NhapHoTen();
        }
        private void btnLaySTT_Click_cu(object sender, EventArgs e)
        {

            //System.Console.WriteLine("vào   btnLaySTT_Click");
            //VNPT.HIS.Common.Func.Print_80mm("C:\\temphis2018\\MAU_STT1.docx", false); return;

            //btnLaySTT.Enabled = false;
            Thread.Sleep(10);

            if (txtMaBN.Text.Trim() != "")
                XuLy_NhapMaBN();
            else if (txtHoTen.Text.Trim() != "")
                XuLy_NhapHoTen();
            else
                XuLy_NhapHoTen();

            //btnLaySTT.Enabled = true;
        }

        private DataTable TimKiem_BenhNhan(string MaTimKiem, string kieu)
        {
            lbLoi.Text = "";
            //DOI_TUONG_ID = MaTimKiem == "2" ? "1" : "2";  // 2 vp; 1 bh
            //1 theo mã bệnh nhân
            //2 theo mã bhyt
            string tenbenhnhan = "";
            string ngaysinh = "";
            string gioitinhid = "1";// phải để mặc định nếu ko có 

            DataTable dt = RequestHTTP.getChiTiet_BenhNhan(MaTimKiem, kieu, tenbenhnhan, ngaysinh, gioitinhid);

            string KIOS_APP_DKKHAM = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_DKKHAM");
            if (dt.Rows.Count > 0 && KIOS_APP_DKKHAM == "1") btnDangKy.Visible = true;
            else btnDangKy.Visible = false;

            if (dt.Rows.Count == 0 && MaTimKiem != "") lbLoi.Text = "Không tìm thấy";

            return dt;
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            //return;
            //HienThi_TenBN_TheoMaTimKiem();

            if (dtTT_BenhNhan != null && dtTT_BenhNhan.Rows.Count > 0)
            {
                string dt_bhyt = "";
                if (dtTT_BenhNhan.Columns.Contains("DTBNID") && dtTT_BenhNhan.Rows[0]["DTBNID"].ToString() == "1")
                {
                    dt_bhyt = "1"; // có mã thẻ BH --> đối tượng BH
                    //Func.set_log_file("Log Đối tượng BHYT, KiemTra_TheBHYT = " + Const.L1_ktraBHYT);

                    string KIOS_APP_KTR_LOAIBHYT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_KTR_LOAIBHYT");
                    if (KIOS_APP_KTR_LOAIBHYT != "" && KIOS_APP_KTR_LOAIBHYT != "0")                        
                    {
                        if (dtTT_BenhNhan.Columns.Contains("BENHNHANID"))
                            if (KiemTra_LoaiBHYT(dtTT_BenhNhan.Rows[0]["BENHNHANID"].ToString()) == false) return;
                    }

                    string KIOS_APP_KTRBHYT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_KTRBHYT");
                    if (KIOS_APP_KTRBHYT == "1")
                    {
                        if (KiemTra_TheBHYT() == false) return;
                    }
                }
                else
                    dt_bhyt = "2";// viện phí

                //test
                //return;

             // Chọn yêu cầu khám
             BVDAKHOA_DangKyKham frm = new BVDAKHOA_DangKyKham(this.Width, this.Height, this.TopMost);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.setReturnData(Return_YeuCauKham);
                frm.setDt_BenhNhan(dtTT_BenhNhan, dt_bhyt, hideBARCODE_BHYT);
                frm.setTT_TheBHYT(maDKBD, gtTheTu, gtTheDen, diaChi, ngayDu5Nam);
                frm.ShowDialog();
            }
        }
        string ten_Pkham = "", GIOITINHID = "", NAMSINH = "", SDTBENHNHAN = "", DIACHI="";
        string hide_STT = "", hide_Benh_An = "";
        private void Return_YeuCauKham(object sender, EventArgs e)
        {
            //trả kết quả về khi gửi submit đăng ký khám lên sv
            string ret = (string)sender;
            ten_Pkham = ""; GIOITINHID = ""; NAMSINH = ""; SDTBENHNHAN = ""; DIACHI = "";
            hide_STT = "";
            if (ret.IndexOf(" f# ") > -1)
            {
                // MABENHAN|GIOITINHID|NAMSINH|SDTBENHNHAN|DIACHI
                string thong_tin = ret.Substring(0, ret.IndexOf(" f# "));
                string[] arr_thong_tin = thong_tin.Split('|');

                //MABENHAN = arr_thong_tin[0]; bỏ
                GIOITINHID = arr_thong_tin[1] == "1" ? "Nam" : (arr_thong_tin[1] == "2" ? "Nữ" : "");
                NAMSINH = arr_thong_tin[2];
                SDTBENHNHAN = arr_thong_tin[3];
                DIACHI = arr_thong_tin[4];
                ten_Pkham = arr_thong_tin[5];

                ret = ret.Substring(ret.IndexOf(" f# ") + 4);
            }


            if (ret.StartsWith("ret_true"))//thành công thì in phiếu STT luôn
            {
                string[] retArr = ret.Substring("ret_true".Length).Split(',');

                //MessageBox.Show("thành công"); 
                //13 biến: RETURN b_khambenhid||','||vmabenhnhan||','||b_phongkhamdangkyid||','||b_tiepnhanid||','||b_hosobenhanid||','||b_benhnhanid||','||b_bhytid||','||b_maubenhphamid||','||b_dichvukhambenhid || ',' || b_mahosobenhan || ',' || b_thukhac || ',' || r_socapcuu || ',' || b_sothutupkdk;	
                if (retArr.Length >= 9) hide_Benh_An = retArr[9];
                if (retArr.Length >= 12) hide_STT = retArr[12];

                btnLaySTT_Click_cu(null, null);
            }
            else
            {
                // lỗi đăng ký khám
                DialogResult dlResult = MessageBoxEx.Show(ret, 8000);
            }
        }
        
        string maDKBD = "";
        string gtTheTu = "";
        string gtTheDen = "";
        string diaChi = "";
        string ngayDu5Nam = "";
        private bool KiemTra_TheBHYT()
        {
            string sobhyt = "";
            string namsinh = "";
            string hoten = "";

            string tt = "";
            // nếu quẹt từ thẻ bh thì lấy tt của thẻ làm biến đầu vào
            string[] sobhyt_catchuoi = hideBARCODE_BHYT.Split('|');
            if (sobhyt_catchuoi.Length >= 10)
            {
                sobhyt = sobhyt_catchuoi[0];
                namsinh = sobhyt_catchuoi[2].Trim();
                hoten = Func.FromHex(sobhyt_catchuoi[1]);
                tt = "Thẻ";
            }
            else if (dtTT_BenhNhan.Rows.Count > 0) // ko quẹt thẻ thì lấy từ tt BN tìm kiếm đc trên db về
            {
                if (dtTT_BenhNhan.Columns.Contains("MA_BHYT")) sobhyt = dtTT_BenhNhan.Rows[0]["MA_BHYT"].ToString();
                namsinh = dtTT_BenhNhan.Rows[0]["namsinh"].ToString();
                hoten = dtTT_BenhNhan.Rows[0]["tenbenhnhan"].ToString();
                tt = "DB";

                if (dtTT_BenhNhan.Columns.Contains("MABENHNHAN"))
                    tt += " (" + dtTT_BenhNhan.Rows[0]["MABENHNHAN"].ToString() + ")";
            }

            if (sobhyt == "")
            {
                Func.set_log_file("LOI1: ko có tt sobhyt để check từ cổng: " + tt + " | " + hoten + " | " + namsinh + " | " + sobhyt);
                return true;
            }
            Func.set_log_file("Log Check từ cổng với : " + tt + " | " + hoten + " | " + namsinh + " | " + sobhyt);


            wsBHYT_LichSu_respons_2018 ret1 = ServiceBHYT.Get_History010118(
                   sobhyt,
                   hoten,
                   namsinh
                   );
            // {"maKetQua":"090","hoTen":"Nguyễn Hồng Hải","gioiTinh":"Nam","diaChi":"11 trệt nguyễn duy dương,p8,q5","maDKBD":"79014","cqBHXH":"Bảo hiểm Xã hội quận 5",
            //  "gtTheTu":"01/01/2017","gtTheDen":"31/12/2021","maKV":"","ngayDu5Nam":"01/12/2015",
            // "dsLichSuKCB":[{"maHoSo":535595540,"maCSKCB":"79014","tuNgay":"28/09/2017","denNgay":"28/09/2017","tenBenh":"E11....;","tinhTrang":"1","kqDieuTri":"1"},{"maHoSo":443409000,"maCSKCB":"79014","tuNgay":"03/05/2017","denNgay":"03/05/2017","tenBenh":"J00 -  - Viêm mũi họng cấp [cảm thường]","tinhTrang":"","kqDieuTri":""}]}

            if (ret1 == null || ret1.maKetQua == null) // lỗi lấy thông tin
            {
                Func.set_log_file("LOI3: trên cổng ko có");
                //"LỖI ĐĂNG KIỂM TRA THẺ BHYT", MessageBoxButtons.OK, MessageBoxIcon.Error , 
                DialogResult dlResult = MessageBoxEx.Show("Mời bạn đăng ký khám tại quầy (Không có dữ liệu lịch sử KCB. Yêu cầu kiểm tra thông tin đầu vào)", 8000);
                 
                return false;
            }

            maDKBD = ret1.maDKBD;
            gtTheTu = ret1.gtTheTu;
            gtTheDen = ret1.gtTheDen;
            diaChi = ret1.diaChi;
            ngayDu5Nam = ret1.ngayDu5Nam;
            

            bool the_sai_hoten_ngaysinh = ret1.maKetQua != "060" && ret1.maKetQua != "061" && ret1.maKetQua != "070";//  không sai=true; sai = false
            bool maKQ_rong_Hoac_theDen_rong = ret1.maKetQua != "" && ret1.gtTheDen != null && ret1.gtTheDen != "" && ret1.gtTheDen != "null";//  không rỗng=true; rỗng = false

            //if (maKQ_rong_Hoac_theDen_rong == true) // không rỗng
            //{
            //    ucThongTinHanhChinh1.ucGioitinh.SelectValue = ret1.gioiTinh.ToUpper() == "NAM" ? "1" : "2";
            //    dtimeTungay.Text = ret1.gtTheTu;
            //    dtimeDenngay.Text = ret1.gtTheDen;
            //}


            //string LayNoiDung = Func.LayNoiDungLoiCheckBHYT(ret1.maKetQua, "0");
            //// check các trường hợp đặc biệt trả về từ cổng.
            //if (ret1.ghiChu.IndexOf("Token") != -1)
            //{
            //    MessageBox.Show("Thông báo từ cổng BHXH " + ret1.maKetQua + " => "
            //            + LayNoiDung + " => " + ret1.ghiChu + ". Thông điệp này không có trong công văn BHXH 2018.");
            //}

            // hop le, cho tiep don; 
            if (ret1.maKetQua == "004")
            {
                //MessageBox.Show(ret1.ghiChu + ". Bệnh nhân sẽ được tiếp đón bình thường.");
                return true;
            }
            else if (ret1.maKetQua == "000")
            {
                return true;
            }

            // khong hop le, cap nhat lai roi cho tiep don
            if (
                 maKQ_rong_Hoac_theDen_rong == false   // dl rỗng
                || the_sai_hoten_ngaysinh == true      // không sai tên/ngay sinh 
                )
            {
                //"LỖI ĐĂNG KIỂM TRA THẺ BHYT", MessageBoxButtons.OK, MessageBoxIcon.Error,
                Func.set_log_file("LOI4: cổng trả về: "+ ret1.ghiChu);
                DialogResult dlResult = MessageBoxEx.Show("Mời bạn đăng ký khám tại quầy (" + ret1.ghiChu+")", 8000);
                return false;
            }


            //kiểm tra hạn thẻ nằm ngoài thời gian hiện tại? //  "gtTheTu":"01/01/2017","gtTheDen":"31/12/2021"
            bool han_the_bh = true;
            DateTime myDate_Tu, myDate_Den;
            DateTime hien_tai = Func.getDatetime_Short(DateTime.Now);
            gtTheTu = gtTheTu.Replace("-", "/");
            gtTheDen = gtTheDen.Replace("-", "/");

            if (DateTime.TryParseExact(gtTheTu, Const.FORMAT_date1, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None
                , out myDate_Tu) == false) han_the_bh = false;
            if (DateTime.TryParseExact(gtTheDen, Const.FORMAT_date1, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None
                , out myDate_Den) == false) han_the_bh = false;

            if (han_the_bh == false || hien_tai < myDate_Tu || hien_tai > myDate_Den)
            {
                Func.set_log_file("LOI5: Sai thời hạn thẻ:" + maDKBD + " | " + gtTheTu + " | " + gtTheDen + " | " + diaChi);
                DialogResult dlResult = MessageBoxEx.Show("Sai thời hạn thẻ bảo hiểm, mời bạn đăng ký khám tại quầy.", 10000);
                return false;
            }


            return true;
        }
        private bool KiemTra_LoaiBHYT(string BenhnhanID)
        {
            // kiểm tra loại bảo hiểm yt có trong config ko 
            DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("KIOS.LOAIBHYTID", new string[] { BenhnhanID });
            if (dt.Rows.Count == 0)
            {
                Func.set_log_file("LOI8: Ko có lần khám cũ:" + BenhnhanID);
                DialogResult dlResult = MessageBoxEx.Show("Tuyến BHYT không phù hợp(*), mời bạn đăng ký khám tại quầy.", 10000);
                return false;
            }
            string KIOS_APP_KTR_LOAIBHYT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_KTR_LOAIBHYT");
            //if (("," + Const.L1_LoaiBHYT + ",").IndexOf("," + dt.Rows[0]["bhyt_loaiid"].ToString() + ",") == -1)
            if (("," + KIOS_APP_KTR_LOAIBHYT + ",").IndexOf("," + dt.Rows[0]["bhyt_loaiid"].ToString() + ",") == -1)
            {
                //Func.set_log_file("LOI9: LoaiBHYT: " + dt.Rows[0]["bhyt_loaiid"].ToString() + " Ko có trong config: " + Const.L1_LoaiBHYT);
                DialogResult dlResult = MessageBoxEx.Show("Tuyến BHYT không phù hợp, mời bạn đăng ký khám tại quầy.", 10000);
                return false;
            }

            //kiểm tra lần khám gần đây nhất là xử trí hẹn khám?
            DataTable dt_xutri = RequestHTTP.get_ajaxExecuteQueryO("KIOS.LOAIXUTRI", new string[] { BenhnhanID });
            if (dt_xutri.Rows.Count == 0)
            {
                Func.set_log_file("LOI10: xử trí gần nhất ko phải hẹn khám:" + BenhnhanID);
                DialogResult dlResult = MessageBoxEx.Show("Không có lịch hẹn của lần khám trước, mời bạn đăng ký khám tại quầy.", 10000);
                return false;
            }

            //kiểm tra lần khám cũ có hẹn khám và đã đến ngày hẹn chưa? oke thì cho đky
            DataTable dt_henkham = RequestHTTP.get_ajaxExecuteQueryO("KIOS.LICHHEN", new string[] { BenhnhanID });
            if (dt_henkham.Rows.Count == 0)
            {
                Func.set_log_file("LOI11: Ko có lịch hẹn khám cũ:" + BenhnhanID);
                DialogResult dlResult = MessageBoxEx.Show("Chưa đến/quá hẹn khám của lần khám trước, mời bạn đăng ký khám tại quầy.", 10000);
                return false;
            }

            return true;
        }
        #endregion

        private string CARD_VALUE_HEX = "";
        private string CARD_VALUE = "";

         

        #region CARD 

        public AccessTcp.TCPAcs AcsTcp = null;
        public string AcsTcp_IP = ConfigurationManager.AppSettings["AcsTcp_IP"];
        public int AcsTcp_Port = Func.Parse(ConfigurationManager.AppSettings["AcsTcp_Port"]);
        private void InitReadCard()
        {
            try
            {
                AcsTcp = new AccessTcp.TCPAcs();
                AcsTcp.OnEvent += new ITCPAcsEvents_OnEventEventHandler(OnEvent);
                //MessageBox.Show("C1 ");
                TControlType ty;
                ty = TControlType.TwoDoor;
                AcsTcp.IntControl(ty, 23456, false, true, false, 30);
                //MessageBox.Show("C2 ");
                AcsTcp.Open(AcsTcp_IP, "", AcsTcp_Port);

                //if (Const.L1_test) MessageBox.Show("AcsTcp Oke ");

            }
            catch (Exception ex)
            {
                //if (Const.L1_test) MessageBox.Show("catch " + ex.ToString());
            }
        }
        int kk = 0;
        public void OnEvent(byte EType, byte second, byte minute, byte hour, byte day, byte Month, int Year,
              byte DoorStatus, byte Ver, byte FuntionByte,
            bool Online, byte CardsofPackage, long CardNo, byte Door, byte EventType, int CardIndex, byte CardStatus, out bool Ack)
        {
            Ack = true;
            try
            {
                switch (EType)
                {
                    case 0:
                        //MessageBox.Show("EType " + EType + "   Status =" + second.ToString()+" --- "  + CardNo.ToString());
                        break;    // status event

                    case 1:
                        CARD_VALUE = (CardNo.ToString());
                        if (CARD_VALUE != "") lbTheUutien.Text = "Dùng thẻ ưu tiên!";
                        //MessageBox.Show("EType " + EType + "   Cardno=" + CardNo.ToString() + " Event:" + EventType.ToString() + " Door:" + Door);

                        break;    // Card event
                    case 2:
                        //MessageBox.Show("EType " + EType + "   alarm=" + EventType.ToString() + ", Door=" + Door.ToString());
                        break; // alarm Event
                    case 3:
                        // MessageBox.Show("EType " + EType + "   Card Status=" + CardIndex.ToString());
                        break; // Card status
                    default:
                        //MessageBox.Show("EType " + EType + "   EventType=" + EventType.ToString() + ", EType=" + EType + "");
                        break;
                }

            }
            catch (Exception ex)
            { }
        }

        #endregion

        #region CONFIG
        private void BVDAKHOA_Kios_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.Shift && e.KeyCode == Keys.P)
                  || (e.Control && e.Shift && e.KeyCode == Keys.Q))
            {
                bool current_TopMost = this.TopMost;
                FullScreen(false);

                // mở config
                ConfigNew frm = new ConfigNew(current_TopMost);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.setReturnData(Return_Config);
                frm.ShowDialog();
            }
        }
        private void FullScreen(bool full)
        {
            FormBorderStyle = full ? FormBorderStyle.None : FormBorderStyle.Sizable;
            TopMost = full;
            if (TopMost) TopLevel = true; 
            this.WindowState = FormWindowState.Maximized;
        } 

        private void Return_Config(object sender, EventArgs e)
        {
            string ret = (string)sender;

            if (ret == "thoat")
            {
                exit = true;
                this.Close();
                return;
            }
            else if (ret != "thoat")
            {
                //if (Const.L1_kieucapso != "1")
                //{
                //    exit = false;
                //    this.Close();
                //    Return_To_Mainform(null, null);
                //    return;
                //}

                FullScreen(ret == "true");
            } 
        }

        protected EventHandler Return_To_Mainform;
        public void setReturn_To_Mainform(EventHandler eventReturnData)
        {
            Return_To_Mainform = eventReturnData;
        }


        #endregion

        public bool exit = true;
        private void BVDAKHOA_Kios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit) frmMain.Current.Exit();
        }
    }
}
