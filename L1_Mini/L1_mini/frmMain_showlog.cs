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

namespace L1_Mini
{
    public partial class frmMain_showlog : Form
    { 
        public static frmMain_showlog Current;
        public frmMain_showlog()
        {
            InitializeComponent(); 
        } 
        //
        private void openForm(Form frm, string optionsPopup = "0")
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Normal;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }
         
        private void frmMain_showlog_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            in_file_stt("hhhh  hhh", "", "", "");
            //return;


            Current = this;
            mainPanel.Visible = false;

            if (autologin()) Login_Suss();

            txtMaBN.Focus();

            label3.Text = this.Size.Height + " x " + this.Size.Width;
        }
        private bool autologin()
        { 
                string txtUser = "bm2.admin";
                string txtPass = "admin@115";

                if (txtPass != "" && txtUser != "")
                {
                    UserLogin ret = RequestHTTP.Login_Service(txtUser, txtPass);
                    if (ret != null && ret.UUID != null && ret.UUID != "") // thanh cong
                    {
                        Const.local_user = ret;
                        Const.local_username = Const.local_user.USER_NAME;

                        return true;
                    }
                } 
            return false;
        }
        private void config(string str)
        {
            string EncryptedString = Func.EncryptString(str, "l1_mini");

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings["l1_mini"].Value = EncryptedString;
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
        private string load_config()
        {
            string DecryptedString = "";
            string l1_mini = ConfigurationManager.AppSettings["l1_mini"];
            if (l1_mini != "") DecryptedString = Func.DecryptString(l1_mini, "l1_mini");

            return DecryptedString;
        }
        //private void AfterLoading(object sender, EventArgs e)
        //{
        //    this.Activated -= AfterLoading;
        //    txtMaBN.Focus();
        //}
        public void Exit()
        {
            //this.Dispose();
            Application.Exit();
        }
        public void Login_Suss()
        {
            mainPanel.Visible = true;
            this.WindowState = FormWindowState.Maximized; 
            lbLoi.Text = "";
            lbTheUutien.Text = "";
            txtMaBN.Text = "";
            txtHoTen.Text = "";
            txtMaBN.Focus();

            Thread.Sleep(100);

            //Nhận đầu đọc thẻ
            string dau_doc_the = Scan_Port();

            DialogResult dlResult = MessageBoxEx.Show(dau_doc_the == "" ? "Không tìm thấy đầu đọc thẻ!"
                : "Xác nhận đầu đọc thẻ: " + m_szVersion + "  " + m_szCaption + " - tại: " + m_szLastCommPort
            , 4000);
            lbCom.Text = m_szLastCommPort+":";

            txtMaBN.Focus();
            //this.Activated += AfterLoading;
        } 

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

                string fullName = Application.StartupPath + OUT_FILE_DOC.Substring(1).Replace("/","\\");
                DialogResult dlResult = MessageBoxEx.Show(" File In: " + fullName
                , 15000);

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
        private string replace_text(string docText, string HoTen, string TheBH, string Muc, string STT)
        {
            DateTime NgayBao = DateTime.Now;

            if (TheBH == "")
            {
                docText = docText.Replace("Thẻ BH:", "");
                docText = docText.Replace("@thebh@", "");
            }
            else
                docText = docText.Replace("@thebh@", TheBH);

            //thông tin từ phiếu
            docText = docText.Replace("@hoten@", HoTen);
            docText = docText.Replace("@thebh@", TheBH);
            docText = docText.Replace("@muc@", Muc);
            docText = docText.Replace("@stt@", STT);

            docText = docText.Replace("@ngay@", DateTime.Now.ToString("dd/MM/yyyy, HH:mm"));

            return docText;
        }
        #endregion


        #region XỬ LÝ SK MÀN HÌNH CHÍNH
        private void XuLy_NhapMaBN()
        {
            string input = txtMaBN.Text.Trim().ToUpper();

            // Quẹt thẻ BHYT
            if (input.IndexOf("|") > -1)
            {
                txtMaBN.Text = "";
                XuLy_QuetTheBHYT(input);
            }
            // Nhập Mã BN
            else
            {
                string MaBN = input;
                //Tìm kiếm theo Mã bệnh nhân
                TimKiem_BN(txtHoTen.Text.Trim(), MaBN, "");
            }
        }
        private void XuLy_NhapHoTen()
        {
            string input = txtHoTen.Text.Trim();

            // Quẹt thẻ BHYT
            if (input.IndexOf("|") > -1)
            {
                txtHoTen.Text = "";
                XuLy_QuetTheBHYT(input);
            }
            // Nhập Tên BN
            else
            {
                string HoTen = input;
                //Tìm kiếm theo Ten bệnh nhân
                TimKiem_BN(HoTen, txtMaBN.Text.Trim(), "");
            }
        }
        private void XuLy_QuetTheBHYT(string input)
        {
            string str_sobhyt = input;// DN401AA31900313|4cc3aa205669e1bb87742048c6b06e67|04/09/1985|1|437479207068e1baa76e206de1bb816d20564e50542d20434e2074e1bb956e6720437479204456205654|01 - 043|01/07/2017|31/12/2017|21/07/2017|01059710639215|-|4|01/07/2022|1f2fa22c47f0bd18-7102|$
            string[] sobhyt_catchuoi = str_sobhyt.Split('|');

            string hoten = Func.FromHex(sobhyt_catchuoi[1]);
            string sobhyt = sobhyt_catchuoi[0];
            //Tìm kiếm theo số BHYT
            DialogResult dlResult4 = MessageBoxEx.Show("MaBHYT=" + sobhyt, 4000);
            input = input.Replace("$", "**##");
            TimKiem_BN(hoten.ToUpper(), "", sobhyt, input);
        }
        
        private void TimKiem_BN(string HoTen, string MaBN, string MaBHYT, string barcode = "")
        {
            try
            {
                ////DialogResult dlResult4 = MessageBoxEx.Show("BN:" + HoTen + "   MaBN  " + MaBN + "  MaBHYT=" + MaBHYT, 5000);
                //DataTable dt2 = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", "0");
                //dt2 = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", "0");
                //dt2 = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", "0");
                //dt2 = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", "0");
                //dt2 = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", "0");
                //dt2 = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", "0");
                //return;


                if (HoTen == "" && MaBN == "" && MaBHYT == "") return;
                 
                DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KIOS.CAPSO"
                    , HoTen + "$" + MaBN + "$"+ MaBHYT + "$"+ CARD_VALUE + "$" + barcode);
                 
                // {"result": "[{\"STT\": \"BN0003\",\"TENBENHNHAN\": \"DFGDFG\",\"MUC\": \"0\",\"MABHYT\": \"\"
                // ,\"THONGBAO\": \"\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
                // Lấy được stt từ Server
                if (dt.Rows.Count > 0)
                {
                    //DialogResult dlResultqqq = MessageBoxEx.Show("OK1.", 1000);return;

                    if (dt.Rows[0]["THONGBAO"] != null && dt.Rows[0]["THONGBAO"].ToString() != "")
                    {
                        lbLoi.Text = dt.Rows[0]["THONGBAO"].ToString();
                    }
                    else
                    {
                        string TENBENHNHAN = dt.Rows[0]["TENBENHNHAN"].ToString();
                        string MABHYT = dt.Rows[0]["MABHYT"].ToString();// + CARD_VALUE;
                        string MUC = dt.Rows[0]["MUC"].ToString() == "1" ? "Ưu tiên" : "Thường";
                        string STT = dt.Rows[0]["STT"].ToString();

                        DialogResult dlResult4 = MessageBoxEx.Show("Respons BN:"+ TENBENHNHAN+"   STT  "+ STT, 4000);

                        lbLoi.Text = "";

                        if (in_file_stt(TENBENHNHAN, MABHYT, MUC, STT))
                        {
                            txtMaBN.Text = "";
                            txtHoTen.Text = "";
                            txtMaBN.Focus();
                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 2500);
                        }
                    }
                }
                else
                {
                    //DialogResult dlResult2 = MessageBoxEx.Show("Lỗi máy chủ.", 1000);
                    TimKiem_BN2(HoTen, MaBN, MaBHYT, barcode);
                }

                // sau mỗi lần tìm kiếm BN thì reset thẻ
                CARD_VALUE_HEX = "";
                CARD_VALUE = "";
                lbTheUutien.Text = "";
            }
            catch(Exception ex) { }
        }
        private void TimKiem_BN2(string HoTen, string MaBN, string MaBHYT, string barcode = "")
        {
            try
            { 
                if (HoTen == "" && MaBN == "" && MaBHYT == "") return;
                Thread.Sleep(500);
                DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KIOS.CAPSO"
                    , HoTen + "$" + MaBN + "$" + MaBHYT + "$" + CARD_VALUE + "$" + barcode);
                // Lấy được stt từ Server
                if (dt.Rows.Count > 0)
                {
                    //DialogResult dlResultqqq = MessageBoxEx.Show("OK2222.", 1000); return;
                    if (dt.Rows[0]["THONGBAO"] != null && dt.Rows[0]["THONGBAO"].ToString() != "")
                    {
                        lbLoi.Text = dt.Rows[0]["THONGBAO"].ToString();
                    }
                    else
                    {
                        string TENBENHNHAN = dt.Rows[0]["TENBENHNHAN"].ToString();
                        string MABHYT = dt.Rows[0]["MABHYT"].ToString();// + CARD_VALUE;
                        string MUC = dt.Rows[0]["MUC"].ToString() == "1" ? "Ưu tiên" : "Thường";
                        string STT = dt.Rows[0]["STT"].ToString(); 

                        lbLoi.Text = "";

                        if (in_file_stt(TENBENHNHAN, MABHYT, MUC, STT))
                        {
                            txtMaBN.Text = "";
                            txtHoTen.Text = "";

                            DialogResult dlResult = MessageBoxEx.Show("Mời quý khách lấy Số thứ tự.", 2500);
                        }
                    }
                }
                else
                {
                    DialogResult dlResult2 = MessageBoxEx.Show("Lỗi máy chủ.", 1000); 
                }

                // sau mỗi lần tìm kiếm BN thì reset thẻ
                CARD_VALUE_HEX = "";
                CARD_VALUE = "";
                lbTheUutien.Text = "";
            }
            catch (Exception ex) { }
        }
        private void txtMaBN_Leave(object sender, EventArgs e)
        { 
            //XuLy_NhapMaBN();
        }
        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Enter)
            {
                //System.Console.WriteLine("do " + txtMaBN.Text);
                XuLy_NhapMaBN();
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
        private void btnLaySTT_Click(object sender, EventArgs e)
        {
            if (m_bIsExecutingCommand) return; 

            btnLaySTT.Enabled = false;
            Thread.Sleep(100);

            if (txtMaBN.Text.Trim() != "")
                XuLy_NhapMaBN();
            else if (txtHoTen.Text.Trim() != "")
                XuLy_NhapHoTen();

            btnLaySTT.Enabled = true;
        }

        #endregion


        #region XỬ LÝ ĐẦU ĐỌC THẺ

        private string m_szCardSN;
        private string m_szROMType;
        private string m_szVersion;
        private string m_szModuleName;

        private bool m_bDisableAutoMode;
        private string m_szLastCommPort = "";

        private string m_szLastPortSettings = "19200,N,8,1";
        private GIGATMS.NF.MifareReader withEventsField_MF5x1 = new GIGATMS.NF.MifareReader();
        public GIGATMS.NF.MifareReader MF5x1
        {
            get { return withEventsField_MF5x1; }
            set
            {
                if (withEventsField_MF5x1 != null)
                {
                    //withEventsField_MF5x1.OnComm -= MF5x1_OnComm;
                    //withEventsField_MF5x1.OnGNetEvent -= MF5x1_OnGNetEvent;
                    //withEventsField_MF5x1.OnGNetPlus -= MF5x1_OnGNetPlus;
                    //withEventsField_MF5x1.OnPort -= MF5x1_OnPort;
                    //withEventsField_MF5x1.OnReaderEvent -= MF5x1_OnReaderEvent;
                }
                withEventsField_MF5x1 = value;
                if (withEventsField_MF5x1 != null)
                {
                    //withEventsField_MF5x1.OnComm += MF5x1_OnComm;
                    //withEventsField_MF5x1.OnGNetEvent += MF5x1_OnGNetEvent;
                    //withEventsField_MF5x1.OnGNetPlus += MF5x1_OnGNetPlus;
                    //withEventsField_MF5x1.OnPort += MF5x1_OnPort;
                    //withEventsField_MF5x1.OnReaderEvent += MF5x1_OnReaderEvent;
                }
            }

        }

        private bool m_bIsGNetEvent;
        private bool m_bIsSupportTransfer;

        private int m_iSupportSaveKeyCount;

        private string m_szCaption;
        private bool m_bIsExecutingCommand;

        private bool m_bIsDeviceScaning = false;


        // Vị trí lưu
        private byte CARD_READ_SECTOR = 1;
        private byte CARD_READ_BLOCK = 0;
        private string CARD_KEY = "1FFFFFFFFFFF"; 
        private string CARD_VALUE_HEX = "";
        private string CARD_VALUE = "";

        private void MF5x1_OnReaderEvent(GIGATMS.NF.MifareReader.iReaderEventConstants iReaderEvent)
        {
            switch (iReaderEvent)
            {
                case MifareReader.iReaderEventConstants.READER_CARD_PRESENT: // Quẹt thẻ vào
                    System.Console.WriteLine("Card Present");

                    // Đọc giá trị
                    CARD_VALUE_HEX = "";
                    CARD_VALUE = "";

                    authen = true;
                    //Read(CARD_KEY, CARD_READ_SECTOR, CARD_READ_BLOCK, out CARD_VALUE_HEX, out CARD_VALUE);
                    CARD_VALUE = Card_ID();

                    if (authen == false)
                    {
                        authen = true;
                        // thử với key mặc định và ghi dl mới
                        //try_reset_card();
                    } 

                    DialogResult dlResult = MessageBoxEx.Show("Read: "+ Card_ID()+" = "+CARD_VALUE, 4000);

                    if (CARD_VALUE != "") lbTheUutien.Text = "dùng thẻ ưu tiên!";

                    //        lbThongbao.Text =
                    //    //  "\r\n Scan_Port:" + Scan_Port() +
                    ////    "\r\n Request_Card:" + Request_Card()+
                    //    "\r\n Authenticate:" + Authenticate(CARD_KEY, CARD_READ_SECTOR)
                    //   //  + "\r\n Write:" + Write(key, Sector, Block, Value_write)
                    //   + "\r\n Read:" + Read(CARD_KEY, CARD_READ_SECTOR, CARD_READ_BLOCK, out CARD_VALUE_HEX, out CARD_VALUE)
                    //   + "\r\n Hex=" + CARD_VALUE_HEX + "   Value=" + CARD_VALUE
                    //   ;
                    break;
                case MifareReader.iReaderEventConstants.READER_CARD_REMOVE:
                    System.Console.WriteLine("Card Remove");
                    // sau mỗi lần tìm kiếm BN thì reset thẻ
                    CARD_VALUE_HEX = "";
                    CARD_VALUE = "";
                    lbTheUutien.Text = "";
                    break;
                case MifareReader.iReaderEventConstants.READER_KEY_PRESS:
                    System.Console.WriteLine("Hot-Key Press");
                    break;
                case MifareReader.iReaderEventConstants.READER_ON_IRQ:
                    System.Console.WriteLine("On IRQ");
                    break;
            }
        }
        private void try_reset_card()
        {
            string rand = (new Random().Next(999) + 1).ToString() + DateTime.Now.ToString("dd");
            int count = 5 - (rand.Length);
            string rand_Full = new string('0', count) + rand;

            bool set_key = Set_Authenticate("FFFFFFFFFFFF", CARD_KEY, CARD_READ_SECTOR);

            if (set_key)
            {

                bool bWrite = Write("FFFFFFFFFFFF", CARD_READ_SECTOR, CARD_READ_BLOCK, "BVBM.S1B0." + rand_Full);
                if (bWrite)
                {
                    Read(CARD_KEY, CARD_READ_SECTOR, CARD_READ_BLOCK, out CARD_VALUE_HEX, out CARD_VALUE);
                }
            }

        }

        private string Scan_Port()
        {
            short nEvent = 0;
            string[] szSettingsList = new string[] { "19200,N,8,1" };
            string[] szPorts = new string[] { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7" };
            bool bIsFound = false;

            m_bIsDeviceScaning = true;
            m_bDisableAutoMode = false;

            if (MF5x1.PortOpen) // nếu đã open thì close lại
            {
                MF5x1.PortOpen = false;
                System.Threading.Thread.Sleep(100);
            }

            // Show Version 
            bIsFound = false;
            // Loop by Comm Port
            for (int I = 0; I < szPorts.Length; I++)
            {
                if (MF5x1.PortOpen)
                {
                    MF5x1.PortOpen = false;
                    System.Threading.Thread.Sleep(150);
                }

                MF5x1.PortName = szPorts[I];

                if (MF5x1.PortOpen)
                {
                    MF5x1.PortOpen = false;
                    System.Threading.Thread.Sleep(150);
                }

                MF5x1.Settings = szSettingsList[0];

                MF5x1.PortOpen = true;

                if (MF5x1.PortOpen)
                {
                    nEvent = MF5x1.Polling(0);
                    // Addr=0 (Broadcast)
                    if (nEvent != -1)
                    {
                        bIsFound = IdentificationDevices();
                    }
                }


                if (bIsFound)
                {
                    m_szLastCommPort = MF5x1.PortName;
                    m_szLastPortSettings = MF5x1.Settings;
                    break; // TODO: might not be correct. Was : Exit For
                }

                MF5x1.PortOpen = false;
            }

            if (bIsFound)
            {
                if (m_bDisableAutoMode) //Disable Auto Mode
                    MF5x1.mfAutoMode(false);
                else
                    MF5x1.mfAutoMode();

                withEventsField_MF5x1.OnReaderEvent += MF5x1_OnReaderEvent;
                //.Text = "Tìm thấy: " + m_szVersion + "  " + m_szCaption + " - tại COM: " + MF5x1.CommPort;
            }
            else
            {
                //.Text = "Không tìm thấy cổng!";
            }

            m_bIsDeviceScaning = false;
            return m_szLastCommPort;
        }

        private string Request_Card()
        {
            string ret = "";

            if (m_bIsExecutingCommand) return ret;
            m_bIsExecutingCommand = true;

            short nResult = MF5x1.mfRequest();            // Answer & Request, return card class
            if (nResult > 0)
            {
                ret = "ATQA: 0x" + nResult.ToString("X").PadLeft(4, '0');

                if (MF5x1.mfAnticollision(ref m_szCardSN))
                {
                    ret += " - Card ID: ";
                    if (MF5x1.mfIsNextAnticollision)
                    {
                        ret += m_szCardSN.PadLeft(8, '0');// dịch trái? Strings.Left(m_szCardSN.PadLeft(8, "0"), 6); 
                    }
                    else
                    {
                        ret += m_szCardSN;
                    }
                }
            }
            else
            {
                ret = "Nothing!";
            }

            m_bIsExecutingCommand = false;

            return ret;
        }

        private string Card_ID()
        {
            string ret = "";

            if (m_bIsExecutingCommand) return ret;
            m_bIsExecutingCommand = true;

            if (MF5x1.mfAnticollision(ref m_szCardSN))
            { 
                if (MF5x1.mfIsNextAnticollision)
                {
                    ret = m_szCardSN.PadLeft(8, '0');// dịch trái? Strings.Left(m_szCardSN.PadLeft(8, "0"), 6); 
                }
                else
                {
                    ret = m_szCardSN;
                }
                //cmdMF5[2].Enabled = true;
            }
            else
            {
                //.Text = MF5x1.GNetErrorCodeStr(); 
            }
            m_bIsExecutingCommand = false;
            //ret = m_szCardSN;
            return ret;
        }

        private bool Read(string Key, byte Sector_num, byte Block_num, out string Hex, out string Value)
        {
            bool bResult = false;
            Hex = "";
            Value = "";

            if (Authenticate(Key, Sector_num) == false) return bResult;

            if (m_bIsExecutingCommand) return bResult;
            m_bIsExecutingCommand = true;

            string sResult = null;
            byte[] bBuffer = new byte[16];

            if (MF5x1.mfRead(Block_num, ref bBuffer))
            {
                DialogResult dlResult12 = MessageBoxEx.Show("Read  true ", 4000);
                sResult = BytesToHex(bBuffer);
                bResult = true;
                if (MF5x1.mfCurrentClassEx == MifareReader.iCardTypeExConstants.MIFARE_ULTRALIGHT_CL2)
                {
                    Hex = sResult.Substring(0, 8); 
                }
                else
                {
                    Hex = sResult;
                }

                DialogResult dlResult = MessageBoxEx.Show("Read  Hex " + Hex, 4000);

                Value = HexToString(sResult);
                Value = Value.Trim();
                if (Value.EndsWith("\0"))
                    Value = Value.Substring(0, Value.Length - "\0".Length);

                DialogResult dlResult2 = MessageBoxEx.Show("Read  Value " + Value, 4000);
            }
            else
            {
                DialogResult dlResult2 = MessageBoxEx.Show("Read  false ", 4000);
            }

            m_bIsExecutingCommand = false;
            return bResult;
        }

        private bool Write(string Key, byte Sector_num, byte Block_num, string Value)
        {
            bool bResult = false;

            if (Authenticate(Key, Sector_num) == false) return bResult;

            if (m_bIsExecutingCommand) return bResult;
            m_bIsExecutingCommand = true;

            // Write Block Data, using Hex String
            string txtHex = StringToHex(Value);
            int count = 32 - (txtHex.Length);
            string txtHex_Full = txtHex + new string('0', count);

            byte[] bBuffer = HexToBytes(txtHex_Full);

            if (MF5x1.mfWrite(Block_num, bBuffer))
            {
                bResult = true;
            }
            else
            {
                //label_write.Text = "wite error";
            }

            m_bIsExecutingCommand = false;
            return bResult;
        }

        private bool Set_Authenticate(string Key, string new_key, byte Sector)
        {
            bool bResult = false;

            if (Authenticate(Key, Sector) == false) return bResult;

            if (m_bIsExecutingCommand) return bResult;
            m_bIsExecutingCommand = true;

            byte AccessBlock_0 = 0; // read+write
            byte AccessBlock_1 = 0; // read+write
            byte AccessBlock_2 = 0; // read+write
            byte AccessBlock_3 = 4; // chỉ read + Không write

            bResult = MF5x1.mfAccessCondition(new_key, new_key,
                AccessBlock_0, AccessBlock_1, AccessBlock_2, AccessBlock_3); // thứ tự quyền truy cập của lần lượt 4 block (giá trị từ 0 đến 7)

            m_bIsExecutingCommand = false;
            return bResult;
        }
        bool authen = true;
        private bool Authenticate(string Key, byte Sector)
        {
            authen = true;
            // sector mawcjd định = 0
            bool bResult = false;

            if (m_bIsExecutingCommand) return bResult;
            m_bIsExecutingCommand = true;
             
            bResult = MF5x1.mfAuthenticate(Sector, MifareReader.bKeyTypeConstants.KEY_A, Key);

            m_bIsExecutingCommand = false;

            DialogResult dlResult = MessageBoxEx.Show("Authenticate " + Key + " " + Sector + "    " + bResult, 4000);


           authen = bResult;
            return bResult;
        }
        private bool IdentificationDevices()
        {
            bool bIsFound = false;
            double dblVersion = 0;
            m_iSupportSaveKeyCount = 16;

            m_szVersion = MF5x1.GetVersion(); // PGM0487 V1.4R0 (Build:110609)
            // Get RWD version for make sure are AC906
            m_szROMType = m_szVersion.Substring(0, m_szVersion.IndexOf(" "));

            string str_dblVersion = m_szVersion.Substring(m_szVersion.IndexOf(" ") + 1);
            str_dblVersion = str_dblVersion.Substring(0, str_dblVersion.IndexOf(" "));

            if (str_dblVersion.StartsWith("V")) str_dblVersion = str_dblVersion.Substring(1);
            str_dblVersion = str_dblVersion.Substring(0, str_dblVersion.IndexOf("R"));

            dblVersion = Double.Parse(str_dblVersion);

            bIsFound = true;
            switch (m_szROMType)
            {
                case "PGM0488":
                case "PGM-T0488":
                    // #01- AC906
                    m_szModuleName = "AC906";

                    break;
                case "PGM0487":
                case "PGM-T0487":
                    // #02- PCR310/PRW106/MFR350
                    m_szModuleName = "PCR310/PRW106/MFR350";
                    if (dblVersion >= 1.30006)
                    {
                        m_bIsSupportTransfer = true;
                    }

                    break;
                case "PGM0494":
                case "PGM-T0494":
                    // #04- RWD906
                    m_szModuleName = "RWD906-00";

                    break;
                case "PGM0517":
                case "PGM-T0517":
                    // #05- RWD906-UT
                    m_szModuleName = "RWD906-UT";

                    break;
                case "PGM0499":
                case "PGM-T0499":
                    // #06- MF5
                    m_szModuleName = "MF5";
                    if (dblVersion >= 1.3)
                    {
                        m_bIsSupportTransfer = true;
                    }

                    break;
                case "PGM-T0748":
                    // #07- DF5
                    m_szModuleName = "DF5";
                    m_bIsSupportTransfer = true;

                    break;
                case "PGM-T0985":
                    // #08- MF6
                    m_szModuleName = "MF6";
                    m_bIsSupportTransfer = true;

                    break;
                case "PGM-T0811":
                    // #09- MF10 with baudrate 57600
                    m_szModuleName = "MF10";

                    break;
                case "ROM-T0636":
                    // #10- MF5 with baudrate 9600
                    m_szModuleName = "MF5-01 (ODM)";

                    break;
                case "PGM-T0668":
                    // #11- 
                    m_szModuleName = "MF5-02 (ODM)";

                    break;
                case "PGM-T0593":
                    // #12- PCR216
                    m_szModuleName = "PCR216";

                    break;
                case "PGM-T0583":
                    // #13- MF700-00
                    m_szModuleName = "MF700-00";
                    m_bDisableAutoMode = true;

                    break;
                case "PGM-T0604":
                case "PGM-T0724":
                case "PGM-T1023":
                case "PGM-T1000":
                    // #14- MF700-10
                    m_szModuleName = "MF700-10";
                    m_bDisableAutoMode = true;

                    break;
                case "PGM-T0633":
                    // #15- MF700-30
                    m_szModuleName = "MF700-30";
                    m_bDisableAutoMode = true;

                    break;
                case "PGM-T0605":
                    // #16- RWD145-00
                    m_szModuleName = "RWD145-00";

                    break;
                case "PGM-T0829":
                    // #17- MF700-AB
                    m_szModuleName = "MF700-AB";
                    if (dblVersion >= 1.000002)
                    {
                        m_bIsSupportTransfer = true;
                    }

                    break;
                case "PGM-T0830":
                    // #18- MF12-00
                    m_szModuleName = "MF12-00";

                    break;
                case "PGM-T1039":
                    // #19- MF12-01
                    m_szModuleName = "MF12-01";

                    break;
                case "PGM-T0843":
                    // #20- MF700-DK
                    m_szModuleName = "MF700-DK";

                    break;
                case "PGM-T0987":
                    // #21- MF700-STF
                    m_szModuleName = "MF700-STF";
                    m_bIsSupportTransfer = true;
                    m_bDisableAutoMode = true;

                    break;
                case "PGM-T0995":
                    // #22- RF30
                    m_szModuleName = "RF30";
                    m_bIsSupportTransfer = true;

                    break;
                //case "PGM-T0985": thừa
                //    // #23- MF6
                //    m_szModuleName = "MF6";
                //    m_bIsSupportTransfer = true;

                //    break;
                case "PGM-T0999":
                    // #24- DF700
                    m_szModuleName = "DF700";
                    m_bIsSupportTransfer = true;

                    break;
                case "PGM-T1030":
                case "PGM-T1031":
                    // #25- DF750
                    m_szModuleName = "DF750";
                    m_bIsSupportTransfer = true;

                    break;
                case "PGM-T1074":
                    // #26- MF5-AU56
                    m_szModuleName = "MF5-AU56";
                    m_bIsSupportTransfer = true;

                    break;
                case "PGM-T1100":
                    // #28- PCR320
                    m_szModuleName = "PCR320";
                    m_bIsSupportTransfer = true;
                    if (dblVersion >= 1.2)
                    {
                        m_iSupportSaveKeyCount = 40;
                    }

                    break;
                case "PGM-T1186":
                    // #29- DF20
                    m_szModuleName = "DF20";
                    m_bIsSupportTransfer = true;
                    m_iSupportSaveKeyCount = 0;
                    if (dblVersion >= 1.2)
                    {
                        m_iSupportSaveKeyCount = 40;
                    }

                    break;
                case "PGM-T1235":
                    // #31- MF20
                    m_szModuleName = "MF20";
                    m_bIsSupportTransfer = true;
                    if (dblVersion >= 1.1)
                    {
                        m_iSupportSaveKeyCount = 40;
                    }

                    break;

                case "PGM-T1433":
                    // #32- MF320-DEC/MF20-DEC
                    m_szModuleName = "MF320-DEC/MF20-DEC";
                    m_bIsSupportTransfer = true;
                    m_iSupportSaveKeyCount = 40;

                    break;
                case "PGM-T1472":
                    // #33- MF20-PD1
                    m_szModuleName = "MF20-PD1";
                    m_bIsSupportTransfer = true;

                    m_iSupportSaveKeyCount = 40;

                    break;
                case "PGM-T1494":
                    // #34- MF5-MSB
                    m_szModuleName = "MF5-MSB";
                    m_bIsSupportTransfer = true;

                    break;
                case "PGM-T1495":
                    // #35- MF5-LSB
                    m_szModuleName = "MF5-LSB";
                    m_bIsSupportTransfer = true;

                    break;
                case "PGM-T1493":
                    // #36- DF20-01
                    m_szModuleName = "DF20-01";
                    m_bIsSupportTransfer = true;

                    m_iSupportSaveKeyCount = 40;

                    break;
                case "PGM-T1150":
                    // #37- DF700-20
                    m_szModuleName = "DF700-20";
                    m_bIsSupportTransfer = true;
                    m_bDisableAutoMode = true;

                    break;
                case "PGM-T1519":
                    // #38- AC908
                    m_szModuleName = "AC908";
                    // ¦üPCR310/PRW106/MFR350
                    m_bIsSupportTransfer = true;

                    break;

                case "PGM-T1530":
                    // #39- MF20-9600
                    m_szModuleName = "MF20-9600";
                    m_bIsSupportTransfer = true;
                    m_iSupportSaveKeyCount = 40;

                    break;
                case "PGM-T1275":
                    // #40- MF700-96  (¦PMF700-36)
                    m_szModuleName = "MF700-96";
                    m_bDisableAutoMode = true;

                    break;
                default:
                    bIsFound = false;
                    break;
            }
            return bIsFound;
        }

        public string BytesToHex(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');
            }

            return new string(c);
        }
        public byte[] HexToBytes(string szHex)
        {
            byte[] bData = null;
            int L = 0;
            int I = 0;
            L = szHex.Length / 2;
            bData = new byte[L];
            for (I = 0; I < L; I++)
            {
                //bData[(I - 1) / 2] = Conversion.Val("&H" + Strings.Mid(szHex, I, 2));
                bData[I] = Convert.ToByte(szHex.Substring(I * 2, 2), 16);
            }
            return bData;
        }

        public string HexToString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.UTF8.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
        public string StringToHex(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.UTF8.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Hex = "";
            string Value = "";

            lbThongbao.Text =
                 //  "\r\n Scan_Port:" + Scan_Port() +
                 "\r\n Request_Card:" + Request_Card()
                + "\r\n Authenticate:" + Authenticate(CARD_KEY, CARD_READ_SECTOR)
                //  + "\r\n Write:" + Write(key, Sector, Block, Value_write)
                + "\r\n Read:" + Read(CARD_KEY, CARD_READ_SECTOR, CARD_READ_BLOCK, out Hex, out Value)
                + "\r\n Hex=" + Hex + "   Value=" + Value
                ;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string __CARD_VALUE_HEX = "";
            string __CARD_VALUE = "";

            authen = true;
            Read(txtMaBN.Text,(byte) Convert.ToInt16(txtHoTen.Text), 0, out __CARD_VALUE_HEX, out __CARD_VALUE);
            if (authen == false)
            {
                authen = true;
                // thử với key mặc định và ghi dl mới
                //try_reset_card();
            }

            DialogResult dlResult = MessageBoxEx.Show("Read: " + Card_ID() + " = " + __CARD_VALUE, 4000);
        }

        private void frmMain_showlog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Const.L1_MAU_STT = txtMau.Text.Trim();
        }


        #endregion

        //https://code.msdn.microsoft.com/windowsapps/Enabling-Windows-8-Touch-7fb4e6de
        //Insert a reference to UIAutomationClient.dll to your project
        //In the form-load-handler of the application's main window, insert the following code:
        //var asForm = System.Windows.Automation.AutomationElement.FromHandle(this.Handle);
    }
}
