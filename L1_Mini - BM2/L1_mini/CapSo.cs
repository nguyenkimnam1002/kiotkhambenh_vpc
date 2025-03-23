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
    public partial class CapSo : Form
    {
        public CapSo()
        {
            InitializeComponent();
        }

        private void CapSo_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmMain.Current.Exit();
        }

        DataTable dt = new DataTable();
        string he = "1";  // 0: 
        private void CapSo_Load(object sender, EventArgs e)
        {
            check_thay_doi_KhoaPhong();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            Load_Data();
            Load_Button();

            Reset_CauHinh_BenhVien();
        }
        private void CapSo_SizeChanged(object sender, EventArgs e)
        {
            Load_Button();
        }
        private bool check_thay_doi_KhoaPhong()
        {
            int khoaId_new = RequestHTTP.getIdKhoa();
            //int phongId_new = ServiceSelectDept.getIdPhong();

            if (Const.local_khoaId != khoaId_new)// || Const.local_phongId != phongId_new
            {
                Const.local_khoaId = khoaId_new;
                //Const.local_phongId = phongId_new;

                //Const.local_khoa = "";
                //Const.local_phong = "";

                return true;
            }
            else
                return false;
        }
        private void Load_Data()
        { 
            // {"result": "[{\"ID\": \"1\",\"TEN_HIENTHI\": \"BHYT\",\"STT\": \"11\"}
            // ,{\"ID\": \"2\",\"TEN_HIENTHI\": \"VP\",\"STT\": \"20\"}
            //        ,{\"ID\": \"3\",\"TEN_HIENTHI\": \"Uu tien\",\"STT\": \"30\"}]","out_var": "[]","error_code": 0,"error_msg": ""}

            if (Const.local_khoaId == 0) check_thay_doi_KhoaPhong();// lấy KhoaID đã được thiết lập trên web

            if (Const.local_khoaId == 0)
            {
                MessageBox.Show("Chưa thiết lập Khoa, đăng nhập tài khoản này trên website đê thiết lập Khoa phòng!");
                return;
            }

            string mode = "1";
            if (Const.L1_phanhe.StartsWith("0_")) mode = "0";
            string phanhe = Const.L1_phanhe;
            if (phanhe.IndexOf("_") > -1) phanhe = phanhe.Substring(2);

            //= 0-- > phân hệ: 1 - KBH; 2 - VPI; 3 - TIEPNHAN; 4 - CDHA; 5 - THUOC; 6 - XN;
            //= 1-- > truyền ID Khoa thiết lập
            // = 
            string param = mode + "$";
            if (mode == "1") param += Const.local_khoaId; // "$"+"7029"
            else param += phanhe;

            dt = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", param, 0);  // Const.local_khoaId

            if (dt.Rows.Count == 0)
            {
                if (mode == "1")
                    MessageBox.Show("Không có dữ liệu cấp số (KBH.CAPSOLAYDL) với khoa id "+ Const.local_khoaId);
                else
                    MessageBox.Show("Không có dữ liệu cấp số (KBH.CAPSOLAYDL) với phân hệ " + phanhe + " (1 - KBH; 2 - VPI; 3 - TIEPNHAN; 4 - CDHA; 5 - THUOC; 6 - XN)");
                return;
            }

            int k = 0;
            foreach (Control sub_control in Controls)
                if (sub_control.GetType() == typeof(Button))
                {
                    Button btn = (Button)sub_control;
                    btn.Text = dt.Rows[k]["TEN_HIENTHI"].ToString() + "\r\n(" + dt.Rows[k]["STT"].ToString() + ")"; // Name
                    k++;
                }
        }
        private void Load_Button()
        {
            int number = 2;// 3;
            int pading = 200;// 100;
            int x0 = pading; int y0 = pading;
            int w = (Width - (number + 1) * pading) / number;
            int h = w * 70 / 100;

            Controls.Clear();
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                int i = (k) / number;
                int j = k - number * i;

                Button btn = new Button();


                btn.Font = new System.Drawing.Font("Tahoma", 28F);
                //btn.UseFont = true;
                //btn.Appearance.Options.UseTextOptions = true;
                //btn.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                //btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                //btn.LookAndFeel.UseDefaultLookAndFeel = false;
                btn.Size = new System.Drawing.Size(w, h);
                btn.Click += new EventHandler(btnSelect);
                // btn.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
                //btn.BorderColor = System.Drawing.Color.Gray;

                btn.Location = new System.Drawing.Point(x0 + j * (w + pading), y0 + i * (h + pading));
                btn.Text = dt.Rows[k]["TEN_HIENTHI"].ToString() + "\r\n(" + dt.Rows[k]["STT"].ToString() + ")"; // Name
                btn.Tag = dt.Rows[k]["ID"].ToString(); // ID 

                Controls.Add(btn);
            }
            this.AutoScroll = true; 
        }
        private void btnSelect(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.Tag.ToString();

            DataTable dt_temp = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYCT", id, 0);

            Load_Data();

            if (dt_temp.Rows.Count > 0 && dt_temp.Columns.Contains("STT"))
            {
                if (in_file_stt("", "", "", dt_temp.Rows[0]["STT"].ToString()))
                { 
                    DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3500);
                }                
            }
        }


        #region CHẾ ĐỘ FULLSCREEN VÀ TẮT AUTO LOGIN
        private void CapSo_KeyDown(object sender, KeyEventArgs e)
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
                //if (Const.L1_kieucapso != "3")
                {
                    exit = false;
                    this.Close();
                    Return_To_Mainform(null, null);
                    //return;
                }

                //Load_Data();
                //FullScreen(ret == "true");
            }

            Reset_CauHinh_BenhVien();

        }

        protected EventHandler Return_To_Mainform;
        public void setReturn_To_Mainform(EventHandler eventReturnData)
        {
            Return_To_Mainform = eventReturnData;
        }
        private void Reset_CauHinh_BenhVien()
        {
            string full_path_logo;
            Func.Reset_Logo_BenhVien(out full_path_logo);
            this.BackgroundImage = Func.getIconFullPath(full_path_logo);
            this.BackgroundImageLayout = ImageLayout.Stretch;
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
                string NAME = Report + "_C2_" + DateTime.Now.ToString("yyMMddHHmmss") + "_" + new Random().Next(1000);
                string OUT = @"./Resources/" + NAME;


                string MAU_FILE_DOCX = MAU + ".docx";
                string OUT_FILE_DOC = OUT + ".docx";


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

                string fullName = Application.StartupPath + OUT_FILE_DOC.Substring(1).Replace("/", "\\");
                VNPT.HIS.Common.Func.Print_80mm(fullName, false);
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


        public bool exit = true;
        private void CapSo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit) frmMain.Current.Exit();
        }
    }
}
