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
//using AccessTcp;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;

// 
//  login BND: bnd.admin / Test@2018
//

namespace L1_Mini
{
    public partial class BVVINHPHUC_Kios : Form
    {
        // Add a PictureBox for the VNPT logo
        // End vpc edit interface 
        private PictureBox pictureBoxVNPTLogo;
        private Panel headerPanel;
        private Label headerLabel;

        // Method to initialize the modern UI components
        private void InitializeModernUI()
        {
            // Tạo panel header hiện đại
            headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(69, 196, 252); // Màu xanh VNPT
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;

            // Tạo và thêm logo VNPT (sử dụng Properties.Resources)
            pictureBoxVNPTLogo = new PictureBox();
            pictureBoxVNPTLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxVNPTLogo.Size = new Size(60, 60);
            pictureBoxVNPTLogo.Location = new Point(10, 10);
            pictureBoxVNPTLogo.BackColor = Color.Transparent;

            try
            {
                // Sử dụng resource nhúng (KHUYẾN NGHỊ)
                // pictureBoxVNPTLogo.Image = Properties.Resources.vnpt_logo;

                // Hoặc, tải từ file (ít khuyến nghị hơn)
                string logoPath = Path.Combine(Application.StartupPath, "vnpt_logo.png");
                if (File.Exists(logoPath))
                {
                    pictureBoxVNPTLogo.Image = Image.FromFile(logoPath);
                    Console.WriteLine("Tải logo thành công từ: " + logoPath);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy file logo: " + logoPath);
                    // Vẫn hiển thị ô logo trống
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tải logo: " + ex.Message);
            }

            headerPanel.Controls.Add(pictureBoxVNPTLogo);

            // Thêm title của cơ sở y tế
            Label titleLabel = new Label();
            titleLabel.Text = "TRUNG TÂM Y TẾ VĨNH TƯỜNG";
            titleLabel.Font = new Font("Tahoma", 14, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(pictureBoxVNPTLogo.Right + 30, 10);
            headerPanel.Controls.Add(titleLabel);

            // Tạo header label (tiêu đề chính)
            headerLabel = new Label();
            headerLabel.Text = "HỆ THỐNG ĐĂNG KÝ KHÁM BỆNH";
            headerLabel.Font = new Font("Tahoma", 24, FontStyle.Bold);
            headerLabel.ForeColor = Color.White;
            headerLabel.TextAlign = ContentAlignment.MiddleCenter;
            headerLabel.Size = new Size(this.ClientSize.Width - 20, 40);
            headerLabel.Location = new Point(10, 35);

            headerPanel.Controls.Add(headerLabel);

            // Thêm panel vào form
            this.Controls.Add(headerPanel);

            // Cập nhật vị trí TabControl
            tabControl1.Location = new Point(1, headerPanel.Height + 1);
            tabControl1.Size = new Size(this.ClientSize.Width - 2, this.ClientSize.Height - headerPanel.Height - 2);
            tabControl1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            // Thêm nút đóng ứng dụng
            AddCloseButton();

            // Cập nhật các nút và textbox
            ModernizeTabControl();
            ModernizeButtons();
            ModernizeTextBoxes();

            // Thiết lập title của form (vẫn cần để hiển thị ở thanh taskbar)
            this.Text = "Trung tâm Y tế Vĩnh Tường";

            // Thiết lập các thuộc tính khác
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            // Đăng ký sự kiện điều chỉnh khi kích thước form thay đổi
            this.Resize += (s, e) =>
            {
                if (headerLabel != null)
                    headerLabel.Size = new Size(this.ClientSize.Width - 20, 40);
            };
        }

        private void AddCloseButton()
        {
            Button closeButton = new Button();
            closeButton.Text = "X";
            closeButton.Size = new Size(40, 40);
            closeButton.Location = new Point(this.ClientSize.Width - 45, 5);
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.BackColor = Color.FromArgb(192, 0, 0);
            closeButton.ForeColor = Color.White;
            closeButton.Font = new Font("Arial", 14, FontStyle.Bold);
            closeButton.Cursor = Cursors.Hand;

            closeButton.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show("Bạn có muốn thoát khỏi ứng dụng?",
                    "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            };

            headerPanel.Controls.Add(closeButton);
        }

        // Modernize the tab control
        private void ModernizeTabControl()
        {
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += TabControl1_DrawItem;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new Size(200, 45);
            tabControl1.Font = new Font("Tahoma", 14, FontStyle.Regular);
        }
        
        // Event handler for custom tab drawing
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tabPage = tabControl1.TabPages[e.Index];
            Rectangle tabBounds = tabControl1.GetTabRect(e.Index);
            
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            
            // Fill background
            using (SolidBrush brush = new SolidBrush(isSelected ? Color.White : Color.FromArgb(240, 240, 240)))
            {
                g.FillRectangle(brush, tabBounds);
            }
            
            // Draw text
            using (SolidBrush textBrush = new SolidBrush(isSelected ? Color.FromArgb(0, 102, 204) : Color.Gray))
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                
                // Use bold font for selected tab
                using (Font font = new Font("Tahoma", 14, isSelected ? FontStyle.Bold : FontStyle.Regular))
                {
                    g.DrawString(tabPage.Text, font, textBrush, tabBounds, stringFormat);
                }
            }
            
            // Draw bottom line for selected tab
            if (isSelected)
            {
                using (Pen pen = new Pen(Color.FromArgb(0, 102, 204), 3))
                {
                    g.DrawLine(pen, tabBounds.Left, tabBounds.Bottom, tabBounds.Right, tabBounds.Bottom);
                }
            }
        }
        
        // Modernize all buttons
        private void ModernizeButtons()
        {
            foreach (Control control in this.Controls.Find("tableLayoutPanel1", true)[0].Controls)
            {
                if (control is Button)
                {
                    StyleButton(control as Button);
                }
                else if (control is Panel)
                {
                    foreach (Control panelControl in control.Controls)
                    {
                        if (panelControl is Button)
                        {
                            StyleButton(panelControl as Button);
                        }
                    }
                }
            }
            
            // Style specific buttons
            if (btnKhamMoi != null) StyleButton(btnKhamMoi, Color.FromArgb(0, 120, 215));
            if (buttonLaysoUT != null) StyleButton(buttonLaysoUT, Color.FromArgb(0, 120, 215));
            if (btnDichvu != null) StyleButton(btnDichvu, Color.FromArgb(0, 120, 215));
            if (btnDangKy != null) StyleButton(btnDangKy, Color.FromArgb(0, 150, 136));
            if (btnRefresh != null) StyleButton(btnRefresh, Color.FromArgb(120, 120, 120));
        }
        
        // Style a specific button
        private void StyleButton(Button button, Color? backgroundColor = null)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backgroundColor ?? Color.FromArgb(0, 102, 204);
            button.ForeColor = Color.White;
            button.Font = new Font("Tahoma", 16, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            
            // Add rounded corners
            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
            
            // Add hover effect
            button.MouseEnter += (s, e) => 
            {
                button.BackColor = ControlPaint.Light(button.BackColor);
            };
            
            button.MouseLeave += (s, e) => 
            {
                button.BackColor = backgroundColor ?? Color.FromArgb(0, 102, 204);
            };
        }
        
        // Create rounded rectangle region
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        
        // Modernize all textboxes
        private void ModernizeTextBoxes()
        {
            foreach (Control control in this.Controls.Find("tableLayoutPanel1", true)[0].Controls)
            {
                if (control is Panel)
                {
                    foreach (Control panelControl in control.Controls)
                    {
                        if (panelControl is TextBox)
                        {
                            StyleTextBox(panelControl as TextBox);
                        }
                    }
                }
            }
        }
        
        // Style a specific textbox
        private void StyleTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.White;
            
            // Create a border panel
            Panel borderPanel = new Panel();
            borderPanel.Size = new Size(textBox.Width, textBox.Height + 5);
            borderPanel.Location = new Point(textBox.Location.X, textBox.Location.Y - 2);
            borderPanel.BackColor = Color.White;
            borderPanel.Paint += (s, e) => 
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(0, 102, 204), 2), 
                    new Rectangle(0, 0, borderPanel.Width - 1, borderPanel.Height - 1));
            };
            
            textBox.Parent.Controls.Add(borderPanel);
            borderPanel.SendToBack();
        }
        
        // Override the original load method to incorporate our new UI
        private void ModernizeUI_Load(object sender, EventArgs e)
        {
            // Call the original load functionality
            BVVINHPHUC_Kios_Load(sender, e);
            
            // Initialize modern UI components
            InitializeModernUI();
            
            // Adjust form to fit screen
            this.WindowState = FormWindowState.Maximized;
        }

        // End vpc edit interface 
        private string[] the_uutien = { "KIOSDKHNM01", "KIOSDKHNM02" };
        //L2PT-4659
        string csytid = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "OPT_HOSPITAL_ID");
        string cauhinh_layso_uutien = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_KIOS_UUTIEN");
        string KIOS_BATBUOC_DINHDANH  = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_BATBUOC_DINHDANH");
        string KIOS_APP_PHONGTN = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_PHONGTN");
        string KIOS_APP_SETMAYKIOS = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_SETMAYKIOS");

        private int id_dinhdanh_kios_tmp = 0;

        public BVVINHPHUC_Kios(bool parent_fullscreen = false)
        {
            InitializeComponent();

            if (parent_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                TopMost = parent_fullscreen;
                if (TopMost) TopLevel = true;
            }
            //su kien onclick vao tabpage
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            // in_file_stt("Nguyễn Hồng Thị Nguyên", "Mã thẻ BN", "", "BN123123");
        }
        private void CapSo_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void BVVINHPHUC_Kios_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;

            //Init
            lbLoi.Text = "";
            lbTheUutien.Text = "";
            hideBARCODE_BHYT = "";
            txtMaBN.Text = "";
            txtHoTen.Text = "";
            txtMaBN.Focus();

            Reset_CauHinh_BenhVien();
            
            string KIOS_APP_CHUYEN2KIEU = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_CHUYEN2KIEU");
            if (KIOS_APP_CHUYEN2KIEU == "1")
            {
                lbKhamMoi.Visible = true;
                btnKhamMoi.Visible = true;
                if(cauhinh_layso_uutien == "1")
                {
                    buttonLaysoUT.Visible = true;
                    btnDichvu.Visible = true;
                }
                else
                {
                    buttonLaysoUT.Visible = false;
                    btnDichvu.Visible = false;
                }
                
                Load_Data();
            }
            
            string KIOS_APP_TENBV = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_TENBV");
            if (KIOS_APP_TENBV.Length > 1) this.Text = KIOS_APP_TENBV;
            
            string KIOS_SHOW_TAB_TRACUU = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_SHOW_TAB_TRACUU");
            if (KIOS_SHOW_TAB_TRACUU == "0")
            {
                this.tabControl1.Controls.Remove(this.tabPage2);
                this.tabControl1.Controls.Remove(this.tabPage3);
                this.tabControl1.Controls.Remove(this.tabPage4);
            }
            
            string KIOS_AN_TAB_TRACUU_BD = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_AN_TAB_TRACUU_BD");
            if (KIOS_AN_TAB_TRACUU_BD == "1")
            {
                this.tabControl1.Controls.Remove(this.tabPage4);
            }
            
            string KIOS_TAB_TRACUU_BN = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_TAB_TRACUU_BN");
            if (KIOS_TAB_TRACUU_BN == "0")
            {
                this.tabControl1.Controls.Remove(this.tabPage5);
            }
            
            string KIOS_TAB_TRACUU_TVT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_TAB_TRACUU_TVT");
            if (KIOS_TAB_TRACUU_TVT == "1")
            {
                this.tabControl1.Controls.Remove(this.tabPage3);
            }
            
            // Thiết lập kiểm tra reset theo ngày
            SetTimer();
            
            // Gọi phương thức khởi tạo giao diện hiện đại
            InitializeModernUI();
        }

        // start sondn thiet lap interval check ngay hien tai trong he thong; 
        private static System.Timers.Timer aTimer;
        private String ngayhethong = DateTime.Now.ToString("dd/MM/YYYY");                       // ngay bat cay kios len; 
        private String ngayhientai; 

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(1 * 60 * 1000);                                // 1 phút check thoi gian he thong 1 lan; 
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //MessageBoxEx.Show("Khởi tạo interval gọi theo ngày", 5000);
            ngayhientai = DateTime.Now.ToString("dd/MM/YYYY");
            if (!ngayhientai.Equals(ngayhethong))
            {
                btnKhamMoi.Text = "LẤY SỐ";
                buttonLaysoUT.Text = "LẤY SỐ ƯT";
                ngayhethong = DateTime.Now.ToString("dd/MM/YYYY");                      // set lai ngay he thong; 
                //MessageBoxEx.Show("Reset lại thông tin theo ngày", 10000);
            }

        }
        // end sondn thiet lap interval check ngay hien tai trong he thong; 

        private void Reset_CauHinh_BenhVien()
        { 
                string KIOS_APP_BG = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_BG");
                string full_path_logo1 = Application.StartupPath + ("./Resources/" + KIOS_APP_BG).Substring(1).Replace("/", "\\");
                this.BackgroundImage = Func.getIconFullPath(full_path_logo1);
                this.BackgroundImageLayout = ImageLayout.Stretch;

                //this.tableLayoutPanel1.BackgroundImage = Func.getIconFullPath(full_path_logo1);
                //this.tableLayoutPanel1.BackgroundImageLayout = ImageLayout.Stretch;

            //AcsTcp_IP = ConfigurationManager.AppSettings["AcsTcp_IP"];
            //AcsTcp_Port = Func.Parse(ConfigurationManager.AppSettings["AcsTcp_Port"]);

        }

        public bool exit = true;
        private void BVVINHPHUC_Kios_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit) frmMain.Current.Exit();
        }

        #region XỬ LÝ WORD 

        string error = "";
        private bool in_file_stt(string HoTen, string TheBH, string Muc, string STT, string phankhu, string inchung, string kham_UT, string stt_hientai)
        {
            try
            {
                // in chung: 1: bam nút lay so; 0: bam nut dang ky kham; 
                // KIOS_IN_RIENG_MAU = 0: luon in vao mau KIOS_APP_MAUSTT
                // KIOS_IN_RIENG_MAU = 1: bn dang ky kham in mau KIOS_APP_MAUSTT; bn lay so in mau KIOS_APP_MAUSTT_RIENG

                //string KIOS_APP_MAUSTT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT");                               // MAU IN LUC LAY SO;        
                //string KIOS_IN_RIENG_MAU = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_IN_RIENG_MAU");                           // MAU IN LUC DANG KY KHAM; 

                //string Report = KIOS_APP_MAUSTT;                                        // Const.L1_MAU_STT;//"MAU_STT";



                //if (inchung == "1" && KIOS_IN_RIENG_MAU == "1")
                //{
                //    Report = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT_RIENG");
                //}

                string Report = ""; 
                if (inchung == "1")
                {
                    // bam nut lay so; 
                    Report = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT");
                }
                else
                {
                    // bam nut dang ky kham; 
                    Report = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT_RIENG");
                }

                

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

                    // khoi tao doi tuong phieu in; 
                    string loaiKham = kham_UT == "0" ? "KHÁM THƯỜNG" : "KHÁM ƯU TIÊN";
                    string doiTuong = TheBH == null || TheBH == "" ? "" : "BHYT"; 
                    KIOS_HIS.BVVINHPHUC.PHIEUIN PHIEUIN = new KIOS_HIS.BVVINHPHUC.PHIEUIN(HoTen, TheBH, Muc, STT, phankhu,
                                                                                    inchung, kham_UT, stt_hientai, ten_Pkham,
                                                                                    loaiKham, hide_Benh_An, GIOITINHID, NAMSINH, SDTBENHNHAN, DIACHI, doiTuong, "20", "10101", MABENHNHAN );
                    docText = replace_text1(docText, PHIEUIN);

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
                MessageBoxEx.Show("Lỗi: " + error, 10000);
            }

            return false;
        }

        string xml_hoten_thebh = "<w:p w14:paraId=\"782965AF\" w14:textId=\"7E00E8B1\" w:rsidR=\"00052F27\" w:rsidRDefault=\"00283741\" w:rsidP=\"00FF68B2\"><w:pPr><w:spacing w:after=\"120\" w:line=\"240\" w:lineRule=\"auto\"/><w:ind w:right=\"-368\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>@hoten@</w:t></w:r></w:p><w:p w14:paraId=\"6A65EB5D\" w14:textId=\"093FD050\" w:rsidR=\"0071440C\" w:rsidRPr=\"006A3470\" w:rsidRDefault=\"001A3437\" w:rsidP=\"005E2955\"><w:pPr><w:spacing w:after=\"0\" w:line=\"240\" w:lineRule=\"auto\"/><w:ind w:right=\"-368\"/><w:jc w:val=\"both\"/><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr></w:pPr><w:r w:rsidRPr=\"001A3437\"><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t>Thẻ BH:</w:t></w:r><w:r w:rsidRPr=\"001A3437\"><w:rPr><w:rFonts w:ascii=\"Times New Roman\" w:hAnsi=\"Times New Roman\" w:cs=\"Times New Roman\"/><w:b/><w:color w:val=\"000000\" w:themeColor=\"text1\"/><w:sz w:val=\"20\"/><w:szCs w:val=\"20\"/></w:rPr><w:t xml:space=\"preserve\"> @thebh@</w:t></w:r></w:p>";
        
        private string replace_text1(string docText, KIOS_HIS.BVVINHPHUC.PHIEUIN PHIEUIN)
        {
            string KIOS_IN_CHUNG = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_IN_CHUNG");           // 1 LACVIET, 2 DNO, 3 DKLAN; 0: CAC DON VI CON LAI 
            DateTime NgayBao = DateTime.Now;

            // thay the cac bien mac dinh; 
            docText = docText.Replace("KIOS_APP_SYT", PHIEUIN.SoYTe);
            docText = docText.Replace("KIOS_APP_BENHVIEN", PHIEUIN.TenBV);
            docText = docText.Replace("kios_app_syt", PHIEUIN.SoYTe);
            docText = docText.Replace("kios_app_tenbv", PHIEUIN.TenBV);

            docText = docText.Replace("zzdoituong", PHIEUIN.DoiTuong == null || PHIEUIN.DoiTuong == "" ? "" : PHIEUIN.DoiTuong);
            docText = docText.Replace("zzthebh", PHIEUIN.TheBH);
            //docText = docText.Replace("zzmakcbbd", PHIEUIN.TheBH == null || PHIEUIN.TheBH == "" ? "" : PHIEUIN.MaKCBBD);
            docText = docText.Replace("zzmakcbbd", dtTT_BenhNhan.Columns.Contains("MA_KCBBD") ? dtTT_BenhNhan.Rows[0]["MA_KCBBD"].ToString() : "");
            docText = docText.Replace("zzngaysinh", dtTT_BenhNhan.Columns.Contains("NGAY_SINH") ? dtTT_BenhNhan.Rows[0]["NGAY_SINH"].ToString() : "");
            docText = docText.Replace("zzngay", PHIEUIN.Ngay);
            docText = docText.Replace("zzgioitinh", PHIEUIN.GioiTinh);
            docText = docText.Replace("zzgio", PHIEUIN.Gio);
            docText = docText.Replace("zzloaikham", PHIEUIN.LoaiKham);
            docText = docText.Replace("zzmabenhnhan", PHIEUIN.MaBenhNhan);
            docText = docText.Replace("zzphongkham", PHIEUIN.TenPKham);
            docText = docText.Replace("zzdiachiphong", DiaChi_PK);
            docText = docText.Replace("zzsttphong", So_PK);

            //vậy là có trường gioitinh, trường doituong với trường thebh, trường mabn, trường STT Phòng, địa chỉ phòng, kcbbd

            if (PHIEUIN.STT.Length == 1) 
            {
                PHIEUIN.STT = "0" + PHIEUIN.STT;
            } 

            
            if (KIOS_IN_CHUNG == "1")
            {
                // LVVPC 
                docText = docText.Replace("zzthebh", PHIEUIN.TheBH == null || PHIEUIN.TheBH == "" ? "" : "Thẻ BH: " + PHIEUIN.TheBH);
                docText = docText.Replace("zzmakcbbd", PHIEUIN.TheBH == null || PHIEUIN.TheBH == "" ? "" : PHIEUIN.MaKCBBD);
                docText = docText.Replace("zzhoten", PHIEUIN.HoTen == "" ? "" : PHIEUIN.HoTen);

                if (ten_Pkham == "")
                {
                    docText = docText.Replace("zzphankhu", "");
                }
                else
                {
                    docText = docText.Replace("zzphankhu", PHIEUIN.HoTen != "" ? PHIEUIN.TenPKham : "");
                }
            }
            else if (KIOS_IN_CHUNG == "2")
            {
                // DKDNO
                docText = docText.Replace("zzstt", "Số thứ tự: " + PHIEUIN.STT);
                docText = docText.Replace("zzngay", "Ngày giờ: " + PHIEUIN.NgayGio);

                if (maBN_IN_DNO != null && !maBN_IN_DNO.Equals(""))
                {
                    docText = docText.Replace("zzmabn", "Mã BN: " + maBN_IN_DNO);
                    maBN_IN_DNO = "";
                }
                else
                {
                    docText = docText.Replace("zzmabn", "");
                }

                docText = docText.Replace("zzthebh", PHIEUIN.TheBH == null || PHIEUIN.TheBH == "" ? "" : "Số thẻ BH: " + PHIEUIN.TheBH);
                docText = docText.Replace("zzdoituong", PHIEUIN.TheBH == null || PHIEUIN.TheBH == "" ? "" : "Đối tượng:" + PHIEUIN.DoiTuong);
                docText = docText.Replace("zzhoten", PHIEUIN.HoTen == "" ? "" : "Tên BN: " + PHIEUIN.HoTen);
               

                if (ten_Pkham == "")
                {
                    docText = docText.Replace("zzphongkham", KIOS_APP_PHONGTN.Split('@')[1]);
                }
                else
                {
                    docText = docText.Replace("zzphongkham", PHIEUIN.HoTen != "" ? PHIEUIN.TenPKham : "");
                }
            }
            else if (KIOS_IN_CHUNG == "3")
            {
                // LAN 
                docText = docText.Replace("zzloaikham", PHIEUIN.LoaiKham);
                docText = docText.Replace("zzstt", String.Format("{0:0000}", Convert.ToInt64(PHIEUIN.STT)));
                docText = docText.Replace("zzhoten", PHIEUIN.HoTen == "" ? "" : PHIEUIN.HoTen);
               
                if (PHIEUIN.STTHienTai != "")
                {
                    docText = docText.Replace("zzsohientai", String.Format("{0:0000}", Convert.ToInt64(PHIEUIN.STTHienTai)));
                }
            }
            else
            {
                // Các don vi con lai;
                if (PHIEUIN.TenPKham == "" && PHIEUIN.HoTen == "")
                {
                    docText = docText.Replace(xml_hoten_thebh, "");
                    docText = docText.Replace("zzhoten", "");
                    docText = docText.Replace("zzThẻ BH:", "");
                    docText = docText.Replace("zzthebh", PHIEUIN.TheBH);
                    docText = docText.Replace("zzstt", PHIEUIN.STT);
                    docText = docText.Replace("zzngay", DateTime.Now.ToString("dd/MM/yyyy, HH:mm"));
                }
                else
                {
                    docText = docText.Replace("zzhoten", PHIEUIN.HoTen);
                    docText = docText.Replace("Thẻ BH:", "");
                    docText = docText.Replace("zzthebh", PHIEUIN.TheBH);
                }
            }

            docText = docText.Replace("zzphankhu", PHIEUIN.PhanKhu);
            docText = docText.Replace("zzmuc", PHIEUIN.Muc);
            docText = docText.Replace("zzstt", PHIEUIN.STT);
            docText = docText.Replace("zzmabn", PHIEUIN.MaBN);                                                    //  Mã bệnh án.
            
            docText = docText.Replace("zznamsinh", PHIEUIN.NamSinh);
            docText = docText.Replace("zzsdt", PHIEUIN.SDT);
            docText = docText.Replace("zztuoi", PHIEUIN.Tuoi);
            docText = docText.Replace("zzdiachi", PHIEUIN.DiaChi);
            
            return docText;
        }

        #endregion XỬ LÝ WORD


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
            try {
                hideBARCODE_BHYT = input;
                string str_sobhyt = input;// DN401AA31900313|4cc3aa205669e1bb87742048c6b06e67|04/09/1985|1|437479207068e1baa76e206de1bb816d20564e50542d20434e2074e1bb956e6720437479204456205654|01 - 043|01/07/2017|31/12/2017|21/07/2017|01059710639215|-|4|01/07/2022|1f2fa22c47f0bd18-7102|$
                string[] sobhyt_catchuoi = str_sobhyt.Split('|');

                string hoten = Func.FromHex(sobhyt_catchuoi[1]);
                string sobhyt = sobhyt_catchuoi[0];
                //Tìm kiếm theo số BHYT
                input = input.Replace("$", "**##");
                Cap_STT_Kham(hoten.ToUpper(), "", sobhyt, input);
            }
            catch (Exception ex)
            {
                //error = ex.ToString();
                txtMaBN.Text = "";
                txtHoTen.Text = "";
                hideBARCODE_BHYT = "";
                hideBARCODE_BHYT_LCI = "";
                txtMaBN.Focus();
                MessageBoxEx.Show("Có lỗi xảy ra, vui lòng quét lại thẻ BHYT", 8000);
            }
            
        }

        DataTable dtTT_BenhNhan = new DataTable();
        //string DOI_TUONG_ID = "";
        string hideBARCODE_BHYT = "";
        string hideBARCODE_BHYT_LCI = "";

        string hoten_temp = "";
        string mathe_temp = "";
        // ham tim kiem thong tin benh nhan trong DB va fill thong tin len man hinh kios; 
        private void HienThi_TenBN_TheoMaTimKiem()
        {
            string input = txtMaBN.Text.Trim().ToUpper();
            // các trường hợp gồm có: 
            // Thẻ 10 ký tự: chuỗi 10 ký tự chỉ toàn số  => Nếu k thấy có dữ liệu thì check theo mã BN; 
            // Thẻ 15 ký tự: chuỗi 15 ký tự có 2 ký tự đầu là chữ, 10 ký tự cuối là số 
            // mã QR thẻ: độ dài > 100 
            // Mã BN: các trường hợp còn lại 

            if (input.Length == 10 && input.IndexOf("_") < 0) 
            {
                // tim theo ma the 10 ky tu; 
                hideBARCODE_BHYT = "";
                string MaBaoHiem = input;
                if (mathe_temp == MaBaoHiem)
                {
                    txtHoTen.Text = hoten_temp;
                }

                dtTT_BenhNhan = TimKiem_BenhNhan(MaBaoHiem, "6");
                if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                {
                    txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                    txtMaBN.Text = dtTT_BenhNhan.Rows[0]["MA_BHYT"].ToString();                         // LAY RA THE BHYT TRONG DB RA; 
                }
                else
                {
                    // tim theo ma BN 10 ky tu; 
                    string MaBN = input;
                    hideBARCODE_BHYT = "";

                    txtHoTen.Text = "";
                    dtTT_BenhNhan = TimKiem_BenhNhan(MaBN, "1");
                    if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                    {
                        txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                    }
                }
            }
            else if (input.Length == 15 && input.IndexOf("_") < 0)
            {
                hideBARCODE_BHYT = "";
                string MaBaoHiem = input;
                if (mathe_temp == MaBaoHiem)
                {
                    txtHoTen.Text = hoten_temp;
                }

                dtTT_BenhNhan = TimKiem_BenhNhan(MaBaoHiem, "2");
                if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                {
                    txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                    // THE BHYT GIU NGUYEN K THAY DOI 
                }       
            }
            else if (input.Length > 15 && input.IndexOf("|") > -1)
            {
                

                string[] sobhyt_catchuoi = input.Split('|');
                string MaBaoHiem = sobhyt_catchuoi[0];
                if (MaBaoHiem.Length == 10)
                {
                    // tim theo ma the 10 ky tu; 
                    hideBARCODE_BHYT = "";
                    if (mathe_temp == MaBaoHiem)
                    {
                        txtHoTen.Text = hoten_temp;
                    }

                    dtTT_BenhNhan = TimKiem_BenhNhan(MaBaoHiem, "6");
                    if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                    {
                        txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                        txtMaBN.Text = dtTT_BenhNhan.Rows[0]["MA_BHYT"].ToString();                         // LAY RA THE BHYT TRONG DB RA; 
                    }
                    else
                    {
                        // tim theo ma BN 10 ky tu; 
                        string MaBN = input;
                        hideBARCODE_BHYT = "";

                        txtHoTen.Text = "";
                        dtTT_BenhNhan = TimKiem_BenhNhan(MaBN, "1");
                        if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                        {
                            txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                        }
                    }
                }
                // tim kiem cccd
                else if (MaBaoHiem.Length == 12)
                {

                    string sobhyt = sobhyt_catchuoi[0];
                    string namsinh = sobhyt_catchuoi[3].Substring(4, 4);
                    string hoten = sobhyt_catchuoi[2];

                    string tt = "Thẻ";
                    
                    Func.set_log_file("Log Check từ cổng với : " + tt + " | " + hoten + " | " + namsinh + " | " + sobhyt);

                    // CHECK CONG BHYT KIEMTRA_THEBHYT 741
                    wsBHYT_LichSu_respons_2018 ret1 = ServiceBHYT.Get_History010118(
                           sobhyt,
                           hoten,
                           namsinh
                           );
                    if (ret1.maKetQua == "004" || ret1.maKetQua == "000")
                    {
                        hideBARCODE_BHYT = "";
                        MaBaoHiem = ret1.maThe;
                        txtHoTen.Text = hoten;
                        txtMaBN.Text = ret1.maThe;
                        dtTT_BenhNhan = TimKiem_BenhNhan(MaBaoHiem, "2");
                        if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                        {
                            txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                            // THE BHYT GIU NGUYEN K THAY DOI 
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin thẻ cccd!");
                    }

                }
                else
                {
                    txtMaBN.Enabled = false;
                    txtHoTen.Enabled = false;
                    btnKhamMoi.Enabled = false;
                    buttonLaysoUT.Enabled = false;                                              //L2PT-4659
                    btnDichvu.Enabled = false;
                    mathe_temp = MaBaoHiem;
                    hoten_temp = Func.FromHex(sobhyt_catchuoi[1]);
                    txtMaBN.Text = MaBaoHiem;
                    hideBARCODE_BHYT = input;
                    hideBARCODE_BHYT_LCI = input;

                    dtTT_BenhNhan = TimKiem_BenhNhan(MaBaoHiem, "2");

                    txtHoTen.Text = hoten_temp;

                    if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                    {
                        txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                    }

                    txtMaBN.Enabled = true;
                    txtHoTen.Enabled = true;
                    btnKhamMoi.Enabled = true;
                    buttonLaysoUT.Enabled = true;//L2PT-4659
                    btnDichvu.Enabled = true;
                }
            }
            else if (input.Length > 0)
            {
                string MaBN = input;
                hideBARCODE_BHYT = "";
                
                txtHoTen.Text = "";
                dtTT_BenhNhan = TimKiem_BenhNhan(MaBN, "1");
                if (dtTT_BenhNhan.Rows.Count > 0 && dtTT_BenhNhan.Columns.Contains("TENBENHNHAN"))
                {
                    txtHoTen.Text = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                } 
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
                    string KIOS_IN_CHUNG = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_IN_CHUNG");                   // 1 LACVIET, 2 DNO, 3 DKLAN; 0: CAC DON VI CON LAI 

                    //string HoTen, string TheBH, string Muc, string STT, string phankhu, string inchung, string kham_UT, string stt_hientai

                    if (KIOS_IN_CHUNG == "1" || KIOS_IN_CHUNG == "2")
                    {
                        if (in_file_stt(dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString(), dtTT_BenhNhan.Rows[0]["MA_BHYT"].ToString(), "", hide_STT, "", "0","",""))
                        {
                            txtMaBN.Text = "";
                            txtHoTen.Text = "";
                            hideBARCODE_BHYT = "";
                            txtMaBN.Focus();

                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3000);
                        }
                    }
                    else
                    {
                        if (in_file_stt(dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString(), dtTT_BenhNhan.Rows[0]["MA_BHYT"].ToString(), "", hide_STT, "", "0","",""))
                        {
                            txtMaBN.Text = "";
                            txtHoTen.Text = "";
                            hideBARCODE_BHYT = "";
                            txtMaBN.Focus();

                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3000);
                            
                        }
                    }
                    
                }

                // sau mỗi lần tìm kiếm BN thì reset thẻ
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

            Thread.Sleep(10);

            if (txtMaBN.Text.Trim() != "")
            {
                XuLy_NhapMaBN();
            }   
            else if (txtHoTen.Text.Trim() != "")
            {
                XuLy_NhapHoTen();
            }
            else
            {
                XuLy_NhapHoTen();
            }

                
        }

        private DataTable TimKiem_BenhNhan(string MaTimKiem, string kieu)
        {
            //MaTimKiem = "HT3343422121270";
            lbLoi.Text = "";
            //DOI_TUONG_ID = MaTimKiem == "2" ? "1" : "2";  // 2 vp; 1 bh
            //1 theo mã bệnh nhân
            //2 theo mã bhyt
            string tenbenhnhan = "";
            string ngaysinh = "";
            string gioitinhid = "1";// phải để mặc định nếu ko có 

            DataTable dt = RequestHTTP.getChiTiet_BenhNhan(MaTimKiem, kieu, tenbenhnhan, ngaysinh, gioitinhid);

            string KIOS_APP_DKKHAM = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_DKKHAM");
            string KIOS_MA_KCB_BD_DNO = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_MA_KCB_BD_DNO");
            if (dt.Rows.Count > 0 && KIOS_APP_DKKHAM == "1") {
                //KIOS_MA_KCB_BD_DNO luu cac ma KCB ban dau dung tuyen cua da khoa dak nong
                if (KIOS_MA_KCB_BD_DNO != null && KIOS_MA_KCB_BD_DNO != "0")
                {
                    if (dt.Rows[0]["ma_kcbbd"] != null && dt.Rows[0]["ma_kcbbd"] != "")
                    {
                        int index = KIOS_MA_KCB_BD_DNO.IndexOf(dt.Rows[0]["ma_kcbbd"].ToString());
                        if (index >= 0) {//dung tuyen
                            //btnDangKy.Visible = true;
                            dt.Rows[0]["BHYT_LOAIID"] = '1';
                        }
                        else {//trai tuyen
                              //btnDangKy.Visible = false;
                            dt.Rows[0]["BHYT_LOAIID"] = '4';
                        }
                        btnDangKy.Visible = true;
                    }
                    else
                    {
                        btnDangKy.Visible = true;
                    }
                }
                else
                {
                    btnDangKy.Visible = true;
                }
            } 
            else btnDangKy.Visible = false;

            if (dt.Rows.Count == 0 && MaTimKiem != "") lbLoi.Text = "Không tìm thấy";

            return dt;
        }
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            
        }
        string ten_Pkham = "", GIOITINHID = "", NAMSINH = "", SDTBENHNHAN = "", DIACHI = "", TUOI = "", MAKCBBD = "", MABENHNHAN = "", DiaChi_PK = "", So_PK = ""; 
        string hide_STT = "", hide_Benh_An = "" ,maBN_IN_DNO = "";
        private void Return_YeuCauKham(object sender, EventArgs e)
        {
            //trả kết quả về khi gửi submit đăng ký khám lên sv
            string ret = (string)sender;
            ten_Pkham = ""; GIOITINHID = ""; NAMSINH = ""; SDTBENHNHAN = ""; DIACHI = ""; TUOI = ""; MAKCBBD = ""; MABENHNHAN = ""; 
            hide_STT = ""; DiaChi_PK = ""; So_PK = ""; 

            if (ret.IndexOf(" f# ") > -1)
            {
                // NULL|GIOITINHID|NAMSINH|SDTBENHNHAN|DIACHI|TUOI|MAKCBBD|TENPHONGKHAM|DIACHIPK|SOPK F#
                string thong_tin = ret.Substring(0, ret.IndexOf(" f# "));
                string[] arr_thong_tin = thong_tin.Split('|');
                MABENHNHAN = arr_thong_tin[0];
                GIOITINHID = arr_thong_tin[1] == "1" ? "Nam" : (arr_thong_tin[1] == "2" ? "Nữ" : "");
                NAMSINH = arr_thong_tin[2];
                SDTBENHNHAN = arr_thong_tin[3];
                DIACHI = arr_thong_tin[4];
                TUOI = arr_thong_tin[5];
                MAKCBBD = arr_thong_tin[6];
                ten_Pkham = arr_thong_tin[7];
                DiaChi_PK = arr_thong_tin[8];
                So_PK = arr_thong_tin[9];

                ret = ret.Substring(ret.IndexOf(" f# ") + 4);
            }


            if (ret.StartsWith("ret_true"))//thành công thì in phiếu STT luôn
            {
                string[] retArr = ret.Substring("ret_true".Length).Split(',');

                //MessageBox.Show("thành công"); 
                //13 biến: RETURN b_khambenhid||','||vmabenhnhan||','||b_phongkhamdangkyid||','||b_tiepnhanid||','||b_hosobenhanid||','||b_benhnhanid||','||b_bhytid||','||b_maubenhphamid||','||b_dichvukhambenhid || ',' || b_mahosobenhan || ',' || b_thukhac || ',' || r_socapcuu || ',' || b_sothutupkdk;	
                if (retArr.Length >= 9) hide_Benh_An = retArr[9];
                if (retArr.Length >= 12) hide_STT = retArr[12];
                if (retArr.Length >= 1) maBN_IN_DNO = retArr[1];

                btnLaySTT_Click_cu(null, null);                                     // sau khi dk kham thanh cong, goi lenh nay in ra phieu stt kios; 
                txtMaBN.Focus();
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

            // CHECK CONG BHYT KIEMTRA_THEBHYT 741
            wsBHYT_LichSu_respons_2018 ret1 = ServiceBHYT.Get_History010118(
                   sobhyt,
                   hoten,
                   namsinh
                   );
            // {"maKetQua":"090","hoTen":"Nguyễn Hồng Hải","gioiTinh":"Nam","diaChi":"11 trệt nguyễn duy dương,p8,q5","maDKBD":"79014","cqBHXH":"Bảo hiểm Xã hội quận 5",
            //  "gtTheTu":"01/01/2017","gtTheDen":"31/12/2021","maKV":"","ngayDu5Nam":"01/12/2015",
            // "dsLichSuKCB":[{"maHoSo":535595540,"maCSKCB":"79014","tuNgay":"28/09/2017","denNgay":"28/09/2017","tenBenh":"E11....;","tinhTrang":"1","kqDieuTri":"1"},{"maHoSo":443409000,"maCSKCB":"79014","tuNgay":"03/05/2017","denNgay":"03/05/2017","tenBenh":"J00 -  - Viêm mũi họng cấp [cảm thường]","tinhTrang":"","kqDieuTri":""}]}

            if (ret1 == null || ret1.maKetQua == null || ret1.maKetQua == "null") // lỗi lấy thông tin
            {
                //L2PT-5620-L2PT-5621
                //rieng cho daknong
                if (csytid == "948")
                {
                    if (sobhyt_catchuoi[11] != null )
                    {
                        string kv;
                        switch (sobhyt_catchuoi[11])
                        {
                            case "5": kv = "1"; break;
                            case "6": kv = "2"; break;
                            case "7": kv = "3"; break;
                            default: kv = "0"; break;
                        }
                        if (dtTT_BenhNhan.Rows.Count > 0)
                        {
                            dtTT_BenhNhan.Rows[0]["DT_SINHSONG"] = kv;
                        }
                        else
                        {
                            DataRow toInsert = dtTT_BenhNhan.NewRow();
                            dtTT_BenhNhan.Rows.InsertAt(toInsert, 0);
                            dtTT_BenhNhan.Rows[0]["DT_SINHSONG"] = kv;
                        }
                    }
                    
                }
                else
                {
                    Func.set_log_file("LOI3: trên cổng ko có");
                    //"LỖI ĐĂNG KIỂM TRA THẺ BHYT", MessageBoxButtons.OK, MessageBoxIcon.Error , 
                    DialogResult dlResult = MessageBoxEx.Show("Mời bạn đăng ký khám tại quầy (Không có dữ liệu lịch sử KCB. Yêu cầu kiểm tra thông tin đầu vào)", 8000);

                    return false;
                }

            }
            //L2PT-5620-L2PT-5621
            //rieng cho daknong
            else
            {
                if (csytid == "948")
                {
                    var kv = "0";
                    for (int i = 1; i <= 3; i++)
                    {
                        if (ret1.maKV == "K" + i)
                        {
                            kv = i+"";
                        }
                    }

                    if (dtTT_BenhNhan.Rows.Count > 0)
                    {
                        dtTT_BenhNhan.Rows[0]["DT_SINHSONG"] = kv;
                    }
                    else
                    {
                        DataRow toInsert = dtTT_BenhNhan.NewRow();
                        dtTT_BenhNhan.Rows.InsertAt(toInsert, 0);
                        dtTT_BenhNhan.Rows[0]["DT_SINHSONG"] = kv;
                    }
                }
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
                DialogResult dlResult = MessageBoxEx.Show("Mời bạn đăng ký khám tại quầy ..(" + ret1.ghiChu+")", 8000);
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
        #endregion  XỬ LÝ SK MÀN HÌNH CHÍNH

        private string CARD_VALUE = "";

        #region Xử lý cấp số kiểu 3
        private void Load_Data()
        {
            if (Const.local_khoaId == 0) 
            {
                check_thay_doi_KhoaPhong();                                 // lấy KhoaID đã được thiết lập trên web
            } 

            if (Const.local_khoaId == 0)
            {
                MessageBox.Show("Chưa thiết lập Khoa, đăng nhập tài khoản này trên website đê thiết lập Khoa phòng!");
                return;
            }

            string KIOS_APP_CAPSO_LOAI = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_CAPSO_LOAI");// mặc định =3
            string param = "";
            if (KIOS_APP_CAPSO_LOAI == "0") param = "1$" + Const.local_khoaId;
            else param = "0$" + KIOS_APP_CAPSO_LOAI;

            DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", param, 0);  // Const.local_khoaId

            if (dt.Rows.Count == 0) 
                MessageBox.Show("Không có dữ liệu cấp số (KBH.CAPSOLAYDL) "
                    +(KIOS_APP_CAPSO_LOAI == "1"? "với khoa " +Const.local_khoa
                    : "với tham số " + param)
                    );
            else
            {
                
                string ch_chedo_layso = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_CHEDO_LAYSO");
                if (ch_chedo_layso == "1")
                {
                    //L2PT-4659
                    if (cauhinh_layso_uutien == "1")
                    {
                        btnKhamMoi.Text = "VÉ THƯỜNG";
                        buttonLaysoUT.Text = "VÉ ƯU TIÊN";
                    }
                    else
                    {
                        btnKhamMoi.Text = "LẤY SỐ";// + (stt < 10 ? "0" + stt : "" + stt);
                    }
                    
                    //btnKhamMoi.Tag = dt.Rows[0]["ID"].ToString();
                }
                else
                {
                        Int64 stt = Convert.ToInt64(dt.Rows[0]["STT"]) + 1;
                        btnKhamMoi.Text = "LẤY SỐ " + (stt < 10 ? "0" + stt : "" + stt);
                        btnKhamMoi.Tag = dt.Rows[0]["ID"].ToString();
                    
                }
                
            }
        } 

        private bool check_thay_doi_KhoaPhong()
        {
            int khoaId_new = RequestHTTP.getIdKhoa(); 

            if (Const.local_khoaId != khoaId_new) 
            {
                Const.local_khoaId = khoaId_new; 
                return true;
            }
            else
                return false;
        }
        #endregion


        #region CARD 

        //public AccessTcp.TCPAcs AcsTcp = null;
        public string AcsTcp_IP = ConfigurationManager.AppSettings["AcsTcp_IP"];
        public int AcsTcp_Port = Func.Parse(ConfigurationManager.AppSettings["AcsTcp_Port"]);
        private void InitReadCard()
        {
            try
            {
                /*AcsTcp = new AccessTcp.TCPAcs();
                AcsTcp.OnEvent += new ITCPAcsEvents_OnEventEventHandler(OnEvent);
                //MessageBox.Show("C1 ");
                TControlType ty;
                ty = TControlType.TwoDoor;
                AcsTcp.IntControl(ty, 23456, false, true, false, 30);
                //MessageBox.Show("C2 ");
                AcsTcp.Open(AcsTcp_IP, "", AcsTcp_Port);*/

                //if (Const.L1_test) MessageBox.Show("AcsTcp Oke ");

            }
            catch (Exception ex)
            {
                //if (Const.L1_test) MessageBox.Show("catch " + ex.ToString());
            }
        }
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
        private void BVVINHPHUC_Kios_KeyDown(object sender, KeyEventArgs e)
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

        private void btnDangKy_Click_1(object sender, EventArgs e)
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

                string load_yckham_theo_dt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "LOAD_YEUCAUKHAM_THEO_DT");
                if(load_yckham_theo_dt == "0")
                {
                    dt_bhyt = "0";
                }
                //test
                //return;
                // Chọn yêu cầu khám
                BVVINHPHUC_DangKyKham frm = new BVVINHPHUC_DangKyKham(this.Width, this.Height, this.TopMost);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.setReturnData(Return_YeuCauKham);
                frm.setDt_BenhNhan(dtTT_BenhNhan, dt_bhyt, hideBARCODE_BHYT);
                frm.setTT_TheBHYT(maDKBD, gtTheTu, gtTheDen, diaChi, ngayDu5Nam);
                
                // Voi truong họp cau hinh KIOS_APP_PHONGTN da co du lieu, tiep nhan bn nay luon (DKDNO)
                if (KIOS_APP_PHONGTN != "0")
                {
                    frm.submitBN_DNO();
                }
                else
                {
                    frm.ShowDialog();
                }
                
            }
        }

        private int TGCHO_BNKETIEP = Int32.Parse(RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_TGCHO_BNKT"));                                    // thoi gian cho la 7s; 
        private void btnKhamMoi_Click_1(object sender, EventArgs e)
        {
            try
            {
                btnKhamMoi.Visible = false;
                //MessageBox.Show("disable khám mới 1");

                string ch_chedo_layso = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_CHEDO_LAYSO");
                if (ch_chedo_layso == "1")
                {
                    string input = txtHoTen.Text.Trim();
                    string HoTen = "";
                    string MaBaoHiem = "";
                    string MaBN = "";
                    string barcode = "";
                    // Quẹt thẻ BHYT
                    if (hideBARCODE_BHYT_LCI != "")
                    {
                        txtHoTen.Text = "";
                        //XuLy_QuetTheBHYT(input);
                        string str_sobhyt = hideBARCODE_BHYT_LCI;// DN401AA31900313|4cc3aa205669e1bb87742048c6b06e67|04/09/1985|1|437479207068e1baa76e206de1bb816d20564e50542d20434e2074e1bb956e6720437479204456205654|01 - 043|01/07/2017|31/12/2017|21/07/2017|01059710639215|-|4|01/07/2022|1f2fa22c47f0bd18-7102|$
                        string[] sobhyt_catchuoi = str_sobhyt.Split('|');

                        string hoten = Func.FromHex(sobhyt_catchuoi[1]);
                        string sobhyt = sobhyt_catchuoi[0];
                        //Tìm kiếm theo số BHYT
                        str_sobhyt = str_sobhyt.Replace("$", "**##");
                        HoTen = hoten.ToUpper();
                        MaBaoHiem = sobhyt;
                        barcode = str_sobhyt;
                    }
                    // Nhập Tên BN
                    else
                    {
                        HoTen = input;

                        if (txtMaBN.Text.Trim().Length == 15)
                            MaBaoHiem = txtMaBN.Text.Trim();
                        else
                            MaBN = txtMaBN.Text.Trim();

                    }

                    if (KIOS_BATBUOC_DINHDANH == "1" && id_dinhdanh_kios_tmp == 0)
                    {
                        MessageBox.Show("Chưa cấu hình định danh máy KIOS");
                        return;
                    }
                    
                    DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KIOS.CAPSOUT"
                        , HoTen + "$" + MaBN + "$" + MaBaoHiem + "$" + "" + "$" + barcode + "$" + "0" + "$" + id_dinhdanh_kios_tmp.ToString());
                    Int64 stt = 0;
                    if (dt.Rows.Count > 0 && dt.Columns.Contains("STT"))
                    {

                        var sttValue = dt.Rows[0]["STT"];
                        if (sttValue != DBNull.Value && !string.IsNullOrEmpty(sttValue.ToString()))
                        {
                            // Thêm TryParse để tránh lỗi chuyển đổi
                            if (!Int64.TryParse(sttValue.ToString(), out stt))
                            {
                                // Xử lý khi không chuyển đổi được
                                MessageBox.Show("Giá trị STT không hợp lệ");
                                return;
                            }
                        }
                        stt++; // Tăng STT lên 1
                        //L2PT-4659
                        if (cauhinh_layso_uutien == "1")
                        {

                            btnKhamMoi.Text = "VÉ THƯỜNG " + String.Format("{0:0000}", stt);// (stt < 10 ? "0" + stt : "" + stt);
                            //btnDichvu.Text = "VÉ DỊCH VỤ " + String.Format("{0:0000}", stt);// (stt < 10 ? "0" + stt : "" + stt);
                            //buttonLaysoUT.Text = "VÉ ƯU TIÊN " + (stt < 10 ? "0" + stt : "" + stt);
                        }
                        else
                        {
                            btnKhamMoi.Text = "LẤY SỐ " + (stt < 10 ? "0" + stt : "" + stt);
                        }
                        

                        if (in_file_stt(HoTen, MaBaoHiem, "", dt.Rows[0]["STT"].ToString(), dt.Rows[0]["KHUCHANLE"].ToString(), "1","0", dt.Rows[0]["STTTRUOCDO"].ToString()))
                        {
                            txtMaBN.Text = "";
                            txtHoTen.Text = "";
                            hideBARCODE_BHYT_LCI = "";
                            txtMaBN.Focus();
                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3500);
                        }
                    }
                }
                else
                {
                    string id = btnKhamMoi.Tag.ToString();
                    DataTable dt_temp = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYCT", id, 0);

                    Load_Data();

                    if (dt_temp.Rows.Count > 0 && dt_temp.Columns.Contains("STT"))
                    {
                        if (in_file_stt("", "", "", dt_temp.Rows[0]["STT"].ToString(),"", "1","",""))
                        {
                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3500);
                        }
                    }
                }

                System.Threading.Thread.Sleep(TGCHO_BNKETIEP * 1000);                                            // disable trong 10s; 
                //MessageBox.Show("enable khám mới 1");
                btnKhamMoi.Visible = true;
            }
            catch (Exception ex) { }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
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
                    cboLOAIDV.DataSource = new BindingSource(dic, null);
                    cboLOAIDV.DisplayMember = "Value";
                    cboLOAIDV.ValueMember = "Key";
                    lblDSDV_notify.Visible = false;
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                }

            }
            else if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("0", "Nhóm thuốc VT");
                dic.Add("1", "Thuốc Vật tư");

                cboLoaiTVT.DataSource = new BindingSource(dic, null);
                cboLoaiTVT.DisplayMember = "Value";
                cboLoaiTVT.ValueMember = "Key";
                lblDSTVT_notify.Visible = false;
            }
            else if (tabControl1.SelectedTab.Name == "tabPage4")
            {
                try
                {
                    string show_chiduong = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_SHOW_CHIDUONG");
                    if (show_chiduong == "0")
                    {
                        btnViewMap.Visible = false;
                        label7.Visible = false;
                        cboKHOA.Visible = false;
                    }

                    //if (Const.id_dinhdanh_kios == 0)
                    /*    if (id_dinhdanh_kios_tmp == 0)
                        {
                            MessageBox.Show("Chưa cấu hình định danh máy KIOS");
                            cboDinhdanhKIOS.Visible = true;

                            //set data cbo danh sach kios
                            DataTable dtDSKIOS = RequestHTTP.getDanhsach_KIOS();
                            if (dtDSKIOS == null) dtDSKIOS = new DataTable();
                            Dictionary<string, string> dicKIOS = new Dictionary<string, string>();
                            foreach (DataRow row in dtDSKIOS.Rows)
                            {
                                string loaiid = row["kios_id"].ToString();
                                string description = row["kios_name"].ToString();
                                dicKIOS.Add(loaiid, description);

                            }
                            cboDinhdanhKIOS.DataSource = new BindingSource(dicKIOS, null);
                            cboDinhdanhKIOS.DisplayMember = "Value";
                            cboDinhdanhKIOS.ValueMember = "Key";
                        }
                        else
                        {
                            //cboDinhdanhKIOS.Visible = false;
                            //TH chinh sua lai may kios bat cau hinh
                            if (KIOS_APP_SETMAYKIOS == "1")
                            {
                                cboDinhdanhKIOS.Visible = true;
                            }
                            else
                            {
                                cboDinhdanhKIOS.Visible = false;
                            }
                        }*/

                    //set data cbo kios 
                    DataTable dtDSKIOS = RequestHTTP.getDanhsach_KIOS();
                    if (dtDSKIOS == null) dtDSKIOS = new DataTable();
                    Dictionary<string, string> dic1 = new Dictionary<string, string>();
                    foreach (DataRow row in dtDSKIOS.Rows)
                    {
                        string loaiid = row["kios_id"].ToString();
                        string description = row["kios_name"].ToString();
                        dic1.Add(loaiid, description);

                    }
                    cboKIOS.DataSource = new BindingSource(dic1, null);
                    cboKIOS.DisplayMember = "Value";
                    cboKIOS.ValueMember = "Key";


                    //set data cbo khoa
                    DataTable dtDSKHOA = RequestHTTP.getDanhsach_Khoa();
                    if (dtDSKHOA == null) dtDSKHOA = new DataTable();
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    foreach (DataRow row in dtDSKHOA.Rows)
                    {
                        string loaiid = row["org_id"].ToString();
                        string description = row["org_name"].ToString();
                        dic.Add(loaiid, description);

                    }
                    cboKHOA.DataSource = new BindingSource(dic, null);
                    cboKHOA.DisplayMember = "Value";
                    cboKHOA.ValueMember = "Key";

                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                }
            }
            else if (tabControl1.SelectedTab.Name == "tabPage5")
            {
                lblTKBA_notify.Visible = false;
                lbTenBN.Text = "";
            }
        }
        private void cboLOAIDV_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] strParam = buildParram_DMDV();
            fill_Danhsach_Dichvu(strParam);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] strParam = buildParram_DMDV();
            fill_Danhsach_Dichvu(strParam);
        }

        private void fill_Danhsach_Dichvu(String[] strParam)
        {
            DataTable dataTableDichvu = RequestHTTP.getDanhsach_Dichvu(strParam);
            if (dataTableDichvu.Rows.Count > 0)
            {
                lblDSDV_notify.Visible = false;
                //string path_loading_gif = Application.StartupPath + "./Resources/loading.gif".Substring(1).Replace("/", "\\");
                button1.Visible = false;
                //button1.Image = Image.FromFile(path_loading_gif);
                dataGridView1.DataSource = dataTableDichvu;

                dataGridView1.Columns[0].HeaderText = "Tên nhóm";
                dataGridView1.Columns[1].HeaderText = "Mã Dịch vụ";
                dataGridView1.Columns[2].HeaderText = "Tên dịch vụ";
                dataGridView1.Columns[3].HeaderText = "Đơn vị";
                dataGridView1.Columns[4].HeaderText = "Giá BHYT";
                dataGridView1.Columns[5].HeaderText = "Giá viện phí";
                dataGridView1.Columns[6].HeaderText = "Giá yêu cầu";

                DataGridViewColumn column0 = dataGridView1.Columns[0];
                column0.Width = 150;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 175;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 400;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 100;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 150;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 150;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 150;
                //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                /*foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                    col.DefaultCellStyle.Font = new Font("Arial",18, FontStyle.Bold,GraphicsUnit.Pixel);
                }*/
                button1.Visible = true;
                //button1.Image = null ;
                //DefaultCellStyle.WrapMode
            }
            else
            {
                lblDSDV_notify.Visible = true;
                try
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                }

            }
        }

        private void fill_Danhsach_TVT(String[] strParam)
        {
            DataTable dataTableDichvu = RequestHTTP.getDanhsach_Thuoc_VT(strParam);
            if (dataTableDichvu.Rows.Count > 0)
            {
                lblDSTVT_notify.Visible = false;
                //string path_loading_gif = Application.StartupPath + "./Resources/loading.gif".Substring(1).Replace("/", "\\");
                btnTKTVT.Visible = false;
                //button1.Image = Image.FromFile(path_loading_gif);
                dataGridView2.DataSource = dataTableDichvu;

                dataGridView2.Columns[0].HeaderText = "Mã";
                dataGridView2.Columns[1].HeaderText = "Tên thuốc VT";
                dataGridView2.Columns[2].HeaderText = "Hoạt chất";
                dataGridView2.Columns[3].HeaderText = "Hàm lượng";
                dataGridView2.Columns[4].HeaderText = "Đơn giá";
                dataGridView2.Columns[5].HeaderText = "Hãng SX";
                dataGridView2.Columns[6].HeaderText = "Đơn vị";

                DataGridViewColumn column0 = dataGridView2.Columns[0];
                column0.Width = 125;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 320;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 220;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 180;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 130;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 150;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 150;
                //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                /*foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                    col.DefaultCellStyle.Font = new Font("Arial",18, FontStyle.Bold,GraphicsUnit.Pixel);
                }*/
                btnTKTVT.Visible = true;
                //button1.Image = null ;
                //DefaultCellStyle.WrapMode
            }
            else
            {
                lblDSTVT_notify.Visible = true;
                try
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.Refresh();
                }
                catch (Exception ex)
                {
                    error = ex.ToString();
                }

            }
        }
        private void fill_thongtin_ba(String[] strParam)
        {
            DataTable dataTableDichvu = RequestHTTP.get_tracuu_ba(strParam);
            if (dataTableDichvu.Rows.Count > 0)
            {
                lblTKBA_notify.Visible = false;
                
                lbTenBN.Text = dataTableDichvu.Rows[0]["TENBENHNHAN"].ToString();
                rTBThongTinKham.Text = dataTableDichvu.Rows[0]["THONGTINKHAM"].ToString().Replace("\\n", "\n");
                rTBThongTinCLS.Text = dataTableDichvu.Rows[0]["THONGTINCLS"].ToString().Replace("\\n", "\n");
                rTBThongTinThuoc.Text = dataTableDichvu.Rows[0]["THONGTINTHUOC"].ToString().Replace("\\n", "\n");
            }
            else
            {
                lblTKBA_notify.Visible = true;
                lbTenBN.Text = "";
                rTBThongTinKham.Text = "";
                rTBThongTinCLS.Text = "";
                rTBThongTinThuoc.Text = "";
            }
        }

        private void txtMADV_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String[] strParam = buildParram_DMDV();
                fill_Danhsach_Dichvu(strParam);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            
        }

        private void txtTENDV_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String[] strParam = buildParram_DMDV();
                fill_Danhsach_Dichvu(strParam);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            
        }

        private void cboLoaiTVT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String[] strParam = buildParram_DMTVT();
                fill_Danhsach_TVT(strParam);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            
        }
        private void btnTKTVT_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtMATVT.Text.Trim() == "")
                {
                    MessageBox.Show("Nhập Mã/Tên thuốc cụ thể");
                }
                else
                {
                    String[] strParam = buildParram_DMTVT();
                    fill_Danhsach_TVT(strParam);
                }
                
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            
        }

        private String [] buildParram_DMDV()
        {
            List<string> list = new List<string>();
            list.Add(txtMADV.Text.Trim());
            list.Add(txtTENDV.Text.Trim());
            string value = ((KeyValuePair<string, string>)cboLOAIDV.SelectedItem).Key;
            list.Add(value);
            String[] strParam = list.ToArray();
            return strParam;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                btnDichvu.Visible = false;
                //MessageBox.Show("disable khám mới 1");

                string ch_chedo_layso = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_CHEDO_LAYSO");
                if (ch_chedo_layso == "1")
                {
                    string input = txtHoTen.Text.Trim();
                    string HoTen = "";
                    string MaBaoHiem = "";
                    string MaBN = "";
                    string barcode = "";
                    // Quẹt thẻ BHYT
                    if (hideBARCODE_BHYT_LCI != "")
                    {
                        txtHoTen.Text = "";
                        //XuLy_QuetTheBHYT(input);
                        string str_sobhyt = hideBARCODE_BHYT_LCI;// DN401AA31900313|4cc3aa205669e1bb87742048c6b06e67|04/09/1985|1|437479207068e1baa76e206de1bb816d20564e50542d20434e2074e1bb956e6720437479204456205654|01 - 043|01/07/2017|31/12/2017|21/07/2017|01059710639215|-|4|01/07/2022|1f2fa22c47f0bd18-7102|$
                        string[] sobhyt_catchuoi = str_sobhyt.Split('|');

                        string hoten = Func.FromHex(sobhyt_catchuoi[1]);
                        string sobhyt = sobhyt_catchuoi[0];
                        //Tìm kiếm theo số BHYT
                        str_sobhyt = str_sobhyt.Replace("$", "**##");
                        HoTen = hoten.ToUpper();
                        MaBaoHiem = sobhyt;
                        barcode = str_sobhyt;
                    }
                    // Nhập Tên BN
                    else
                    {
                        HoTen = input;

                        if (txtMaBN.Text.Trim().Length == 15)
                            MaBaoHiem = txtMaBN.Text.Trim();
                        else
                            MaBN = txtMaBN.Text.Trim();

                    }

                    if (KIOS_BATBUOC_DINHDANH == "1" && id_dinhdanh_kios_tmp == 0)
                    {
                        MessageBox.Show("Chưa cấu hình định danh máy KIOS");
                        return;
                    }

                    DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KIOS.CAPSOUT"
                        , HoTen + "$" + MaBN + "$" + MaBaoHiem + "$" + "" + "$" + barcode + "$" + "2" + "$" + id_dinhdanh_kios_tmp.ToString());

                    if (dt.Rows.Count > 0 && dt.Columns.Contains("STT"))
                    {
                        Int64 stt = Convert.ToInt64(dt.Rows[0]["STT"].ToString()) + 1;
                        //L2PT-4659
                        if (cauhinh_layso_uutien == "1")
                        {

                            //btnKhamMoi.Text = "VÉ THƯỜNG " + String.Format("{0:0000}", stt);// (stt < 10 ? "0" + stt : "" + stt);
                            btnDichvu.Text = "VÉ DV " + String.Format("{0:0000}", stt);// (stt < 10 ? "0" + stt : "" + stt);
                            //buttonLaysoUT.Text = "VÉ ƯU TIÊN " + (stt < 10 ? "0" + stt : "" + stt);
                        }
                        else
                        {
                            btnKhamMoi.Text = "LẤY SỐ " + (stt < 10 ? "0" + stt : "" + stt);
                        }


                        if (in_file_stt(HoTen, MaBaoHiem, "", dt.Rows[0]["STT"].ToString(), dt.Rows[0]["KHUCHANLE"].ToString(), "1", "0", dt.Rows[0]["STTTRUOCDO"].ToString()))
                        {
                            txtMaBN.Text = "";
                            txtHoTen.Text = "";
                            hideBARCODE_BHYT_LCI = "";
                            txtMaBN.Focus();
                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3500);
                        }
                    }
                }
                else
                {
                    string id = btnDichvu.Tag.ToString();
                    DataTable dt_temp = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYCT", id, 0);

                    Load_Data();

                    if (dt_temp.Rows.Count > 0 && dt_temp.Columns.Contains("STT"))
                    {
                        if (in_file_stt("", "", "", dt_temp.Rows[0]["STT"].ToString(), "", "1", "", ""))
                        {
                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3500);
                        }
                    }
                }

                System.Threading.Thread.Sleep(TGCHO_BNKETIEP * 1000);                                            // disable trong 10s; 
                //MessageBox.Show("enable khám mới 1");
                btnDichvu.Visible = true;
            }
            catch (Exception ex) { }
        }

        private void btnTKIEMBA_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMABA.Text.Trim() == "")
                {
                    MessageBox.Show("Nhập BA để tra cứu");
                }
                else
                {
                    
                    List<string> list = new List<string>();
                    list.Add(txtMABA.Text.Trim());
                    String[] strParam = list.ToArray();
                    fill_thongtin_ba(strParam);
                }

            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
        }

        private void btnViewMap_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Const.id_dinhdanh_kios == 0)
                /*if (id_dinhdanh_kios_tmp == 0) */
                if (id_dinhdanh_kios_tmp == 1)
                {
                    //MessageBox.Show("Chưa cấu hình định danh máy KIOS");
                    if (((KeyValuePair<string, string>)cboDinhdanhKIOS.SelectedItem).Key != null
                        && ((KeyValuePair<string, string>)cboDinhdanhKIOS.SelectedItem).Key != "")
                    {
                        //Const.id_dinhdanh_kios = Int32.Parse(((KeyValuePair<string, string>)cboDinhdanhKIOS.SelectedItem).Key);
                        id_dinhdanh_kios_tmp = Int32.Parse(((KeyValuePair<string, string>)cboDinhdanhKIOS.SelectedItem).Key);
                    }
                    else
                    {
                        MessageBox.Show("Chưa cấu hình định danh máy KIOS");
                    }

                }
                else
                {
                    cboDinhdanhKIOS.Visible = false;

                    /*string kios_id = id_dinhdanh_kios_tmp + ""; //Const.id_dinhdanh_kios + "";*/
                    string kios_id = ((KeyValuePair<string, string>)cboKIOS.SelectedItem).Key;
                    string khoa_id = ((KeyValuePair<string, string>)cboKHOA.SelectedItem).Key;
                    //string kios_id = "1";
                    //string khoa_id = "7467";

                    List<string> list = new List<string>();
                    /*list.Add(id_dinhdanh_kios_tmp + "");*/
                    list.Add(kios_id + "");
                    string value = ((KeyValuePair<string, string>)cboKHOA.SelectedItem).Key;
                    list.Add(value);
                    String[] strParam = list.ToArray();

                    DataTable dtImage = RequestHTTP.getFile_Image(strParam);

                    //DataTable dtImage = RequestHTTP.get_ajaxExecuteQueryO("KIOS.DMC01.MAP", new string[] { "[0]", "[1]" }, new string[] { kios_id, khoa_id });

                    /*        if (dtImage.Rows.Count > 0 && dtImage.Columns.Contains("file_name"))
                            {
                                //string fn = "bg_vietduc.png";
                                pictureBox1.ImageLocation = Application.StartupPath + "./Resources/" + dtImage.Rows[0]["file_name"].ToString();
                                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                            }
                            else
                            {
                                pictureBox1.ImageLocation = null;
                                MessageBox.Show("Chưa xác định vị trí");
                            }*/
                    pictureBox1.ImageLocation = pictureBox1.ImageLocation = Application.StartupPath + "./Resources/" + "bg_vietduc.png".ToString();
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

                }

                
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }

        }

        private void cboDinhdanhKIOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                id_dinhdanh_kios_tmp = Int32.Parse(((KeyValuePair<string, string>)cboDinhdanhKIOS.SelectedItem).Key);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            
        }

        private void txtMATVT_TextChanged(object sender, EventArgs e)
        {
            try
            {
                String[] strParam = buildParram_DMTVT();
                fill_Danhsach_TVT(strParam);
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            /*string txtHoTen_truocdo;
            if (check_the_uutien(txtHoTen.Text, out txtHoTen_truocdo))
            {
                txtHoTen.Text = txtHoTen_truocdo;
            }*/
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtMaBN.Text = "";
            txtHoTen.Text = "";
            btnDangKy.Visible = false;
        }

        private String[] buildParram_DMTVT()
        {
            
            List<string> list = new List<string>();
            list.Add(txtMATVT.Text.Trim());
            string value = ((KeyValuePair<string, string>)cboLoaiTVT.SelectedItem).Key;
            list.Add(value);
            String[] strParam = list.ToArray();
            return strParam;
        }

        private void buttonLaysoUT_Click(object sender, EventArgs e)
        {
            try
            {
                buttonLaysoUT.Visible = false;

                string ch_chedo_layso = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_CHEDO_LAYSO");
                if (ch_chedo_layso == "1")
                {
                    string input = txtHoTen.Text.Trim();
                    string HoTen = "";
                    string MaBaoHiem = "";
                    string MaBN = "";
                    string barcode = "";
                    // Quẹt thẻ BHYT
                    if (hideBARCODE_BHYT_LCI != "")
                    {
                        txtHoTen.Text = "";
                        //XuLy_QuetTheBHYT(input);
                        string str_sobhyt = hideBARCODE_BHYT_LCI;// DN401AA31900313|4cc3aa205669e1bb87742048c6b06e67|04/09/1985|1|437479207068e1baa76e206de1bb816d20564e50542d20434e2074e1bb956e6720437479204456205654|01 - 043|01/07/2017|31/12/2017|21/07/2017|01059710639215|-|4|01/07/2022|1f2fa22c47f0bd18-7102|$
                        string[] sobhyt_catchuoi = str_sobhyt.Split('|');

                        string hoten = Func.FromHex(sobhyt_catchuoi[1]);
                        string sobhyt = sobhyt_catchuoi[0];
                        //Tìm kiếm theo số BHYT
                        str_sobhyt = str_sobhyt.Replace("$", "**##");
                        HoTen = hoten.ToUpper();
                        MaBaoHiem = sobhyt;
                        barcode = str_sobhyt;
                    }
                    // Nhập Tên BN
                    else
                    {
                        HoTen = input;

                        if (txtMaBN.Text.Trim().Length == 15)
                            MaBaoHiem = txtMaBN.Text.Trim();
                        else
                            MaBN = txtMaBN.Text.Trim();

                    }

                    if (KIOS_BATBUOC_DINHDANH == "1" && id_dinhdanh_kios_tmp == 0)
                    {
                        MessageBox.Show("Chưa cấu hình định danh máy KIOS");
                        return;
                    }

                    DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KIOS.CAPSOUT"
                        , HoTen + "$" + MaBN + "$" + MaBaoHiem + "$" + "" + "$" + barcode + "$" + "1" + "$" + id_dinhdanh_kios_tmp.ToString());

                    if (dt.Rows.Count > 0 && dt.Columns.Contains("STT"))
                    {
                        Int64 stt = Convert.ToInt64(dt.Rows[0]["STT"].ToString()) + 1;
                        //L2PT-4659
                        if (cauhinh_layso_uutien == "1")
                        {

                            //btnKhamMoi.Text = "VÉ THƯỜNG " + (stt < 10 ? "0" + stt : "" + stt);

                            buttonLaysoUT.Text = "LẤY SỐ ƯT " + String.Format("{0:0000}", stt); //(stt < 10 ? "0" + stt : "" + stt);
                        }
                        else
                        {
                            btnKhamMoi.Text = "LẤY SỐ " + (stt < 10 ? "0" + stt : "" + stt);
                        }


                        if (in_file_stt(HoTen, MaBaoHiem, "", dt.Rows[0]["STT"].ToString(), dt.Rows[0]["KHUCHANLE"].ToString(), "1","1", dt.Rows[0]["STTTRUOCDO"].ToString()))
                        {
                            txtMaBN.Text = "";
                            txtHoTen.Text = "";
                            hideBARCODE_BHYT_LCI = "";
                            txtMaBN.Focus();
                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3500);
                        }
                    }
                }
                else
                {
                    string id = btnKhamMoi.Tag.ToString();
                    DataTable dt_temp = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYCT", id, 0);

                    Load_Data();

                    if (dt_temp.Rows.Count > 0 && dt_temp.Columns.Contains("STT"))
                    {
                        if (in_file_stt("", "", "", dt_temp.Rows[0]["STT"].ToString(), "", "1","",""))
                        {
                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 3500);
                        }
                    }
                }

                System.Threading.Thread.Sleep(TGCHO_BNKETIEP * 1000);                                            // disable trong 10s; 
                buttonLaysoUT.Visible = true;
            }
            catch (Exception ex) { }
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
            else if (ret == "dang_nhap_lai")
            {
                exit = false;
                this.Close();
                Return_To_Mainform(ret, null);
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

    }
}
