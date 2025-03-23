using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using VNPT.HIS.Common;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Text;

namespace L1_Mini
{
    public partial class CapSo_QuetMaBN : Form
    { 
        public CapSo_QuetMaBN()
        {
            InitializeComponent(); 
        }

        private void CapSo_QuetBHYT_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
           
        private void CapSo_QuetBHYT_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            Reset_CauHinh_BenhVien();

            reset();

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _loadBenhNhan();
            }
        }
        private void reset()
        {
            //lbThongBao.Text = "";

            txtTENBENHNHAN.Text = "";
            txtNGAYSINH.Text = "";
            txtGIOITINH.Text = "";
            txtSTT.Text = "";

            txtMaBN.Focus();
        }
        private void _loadBenhNhan()
        {
            lbThongBao.Visible = false;
            bool thanhcong = false;
            // VP: 2
            // XN: 6
            // P thuoc: 5
            if (txtMaBN.Text.Trim() == "")
            {
                //DialogResult dlResult0 = MessageBoxEx.Show("Chưa nhập Mã bệnh nhân!",  4000); 
                lbThongBao.Text = "Chưa nhập Mã bệnh nhân!";
                lbThongBao.Visible = true;
                return;
            } 

            string MaBN = txtMaBN.Text.Trim();

            string phanhe = Const.L1_phanhe;
            if (phanhe.IndexOf("_") > -1) phanhe = phanhe.Substring(2);

            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("NGT02K068.LAYDL"
                , MaBN + "$" + Const.local_khoaId + "$" + Const.local_phongId + "$" + phanhe
                );

            reset();

            if (data_ar.Rows.Count > 0)
            {
                string result = data_ar.Rows[0]["RESULT"].ToString();
                if (result == "1")
                {
                    thanhcong = true;
                    lbThongBao.Text = "Thêm vào hàng đợi thành công!";

                    txtTENBENHNHAN.Text = "Họ và tên: " + data_ar.Rows[0]["TENBENHNHAN"].ToString();
                    txtNGAYSINH.Text = "Ngày sinh: " + data_ar.Rows[0]["NGAYSINH"].ToString();
                    txtGIOITINH.Text = "Giới tính: " + data_ar.Rows[0]["GIOITINH"].ToString();
                    txtSTT.Text = "Số thứ tự: " + data_ar.Rows[0]["SOTHUTU"].ToString();

                    //txtTEN_DTBN.Text = data_ar.Rows[0]["TEN_DTBN"].ToString();
                    //txtMA_BHYT.Text = data_ar.Rows[0]["MA_BHYT"].ToString();
                    //txtNGAYTIEPNHAN.Text = data_ar.Rows[0]["NGAYTIEPNHAN"].ToString();
                    //txtMABENHNHAN1.Text = data_ar.Rows[0]["MABENHNHAN1"].ToString();
                    // hidKHAMBENHID").val(dt.KHAMBENHID);
                    //hidPHONGID1").val(dt.PHONGID); 

                    if (Const.L1_autoPrinter == "1")
                    {
                        in_file_stt(data_ar.Rows[0]["TENBENHNHAN"].ToString(), "", "", data_ar.Rows[0]["SOTHUTU"].ToString());
                        
                        lbThongBao.Text = "Mời quý khách lấy Số thứ tự.";
                    }

                }
                else// if (result == "2")
                {
                    lbThongBao.Text = result;// "Mã bệnh nhân này chưa có thông tin tiếp nhận ngày hôm nay.";
                }
                //else if (result == "3")
                //{
                //    lbThongBao.Text = "Bệnh nhân đã kết thúc khám/đang điều trị ngoại trú.";
                //}
                //else if (result == "4")
                //{
                //    lbThongBao.Text = "Bệnh nhân đã có trong danh sách đợi.";
                //}
                //else if (result == "5")
                //{
                //    lbThongBao.Text = "Bệnh nhân hiện không đăng ký phòng khám này.";
                //}
                //else if (result == "6")
                //{
                //    lbThongBao.Text = "Phân hệ truyền vào không phù hợp.";
                //}
                //else
                //{
                //    lbThongBao.Text = "Có lỗi trong quá trình nhận dữ liệu";
                //}

                txtMaBN.SelectAll();
            }
            else
            {
                lbThongBao.Text = "Lỗi lấy thông tin bệnh nhân";
            }


            if (thanhcong)
            {
                DialogResult dlResult_TC = MessageBoxEx.Show(lbThongBao.Text, 4000);
                lbThongBao.Text = "";
            }
            else
            {
                //DialogResult dlResult_Loi = MessageBoxEx.Show(lbThongBao.Text, "LỖI TÌM KIẾM BỆNH NHÂN", MessageBoxButtons.OK, MessageBoxIcon.Error, 4000);
                lbThongBao.Visible = true; 
            }

            txtMaBN.Text = "";
            reset();
        }


        #region CONFIG 
        private void CapSo_QuetBHYT_KeyDown(object sender, KeyEventArgs e)
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
        private void FullScreen(bool full)
        {
            FormBorderStyle = full ? FormBorderStyle.None : FormBorderStyle.Sizable;
            TopMost = full;
            this.WindowState = FormWindowState.Maximized;
        }

        private void Return_Config(object sender, EventArgs e)
        {
            string ret = (string)sender;

            if (ret == "thoat")
            {
                this.Close();
                return;
            }
            else if (ret != "thoat")
            {
                if (Const.L1_kieucapso != "2")
                {
                    exit = false;
                    this.Close();
                    Return_To_Mainform(null, null);
                    return;
                }

                FullScreen(ret == "true");
            }

            Reset_CauHinh_BenhVien();

        }
        private void Reset_CauHinh_BenhVien()
        {
            string full_path_logo;
            Func.Reset_Logo_BenhVien(out full_path_logo);
            this.BackgroundImage = Func.getIconFullPath(full_path_logo);
            this.BackgroundImageLayout = ImageLayout.Stretch;


            string phanhe = Const.L1_phanhe;
            if (phanhe.IndexOf("_") > -1) phanhe = phanhe.Substring(2);

            if (phanhe == "1") label1.Text = "CẤP SỐ KHÁM BỆNH";
            else if (phanhe == "2") label1.Text = "CẤP SỐ VIỆN PHÍ";
            else if (phanhe == "3") label1.Text = "CẤP SỐ TIẾP NHẬN";
            else if (phanhe == "4") label1.Text = "CẤP SỐ CĐHA";
            else if (phanhe == "5") label1.Text = "CẤP SỐ PHÁT THUỐC";
            else if (phanhe == "6") label1.Text = "CẤP SỐ XÉT NGHIỆM";
//(1) Khám Bệnh
//(2) Viện Phí
//(3) Tiếp Nhận
//(4) CĐHA
//(5) Phát Thuốc
//(6) Xét nghiệm
        }


        protected EventHandler Return_To_Mainform;
        public void setReturn_To_Mainform(EventHandler eventReturnData)
        {
            Return_To_Mainform = eventReturnData;
        }
        #endregion

        #region XỬ LÝ WORD 
        string error = "";
        private bool in_file_stt(string HoTen, string TheBH, string Muc, string STT)
        {
            try
            {
                //Lấy dữ liệu  
                string Report = Const.L1_MAU_STT;//"MAU_STT";

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

                    docText = replace_text(docText, HoTen, TheBH, Muc, STT);

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }

                }

                //string fullName = Application.StartupPath + OUT_FILE_DOC.Substring(1).Replace("/", "\\");
                VNPT.HIS.Common.Func.Print_80mm(OUT_FILE_DOC, true);
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
        private string replace_text(string docText, string HoTen, string TheBH, string Muc, string STT)
        {
            DateTime NgayBao = DateTime.Now;

            if (TheBH == "" && HoTen == "")
            {
                docText = docText.Replace(xml_hoten_thebh, "");
            }
            else
            {
                docText = docText.Replace("@hoten@", HoTen);

                if (TheBH == "")
                {
                    docText = docText.Replace("Thẻ BH:", "");
                    docText = docText.Replace("@thebh@", "");
                }
                else
                    docText = docText.Replace("@thebh@", TheBH);
            }

            docText = docText.Replace("@muc@", Muc);
            docText = docText.Replace("@stt@", STT);
            docText = docText.Replace("@ngay@", DateTime.Now.ToString("dd/MM/yyyy, HH:mm"));


            return docText;
        }


        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            

        }

        public bool exit = true;
        private void CapSo_QuetMaBN_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit) frmMain.Current.Exit();
        }
    }
}
