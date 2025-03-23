using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using System.Globalization;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using System.Net;
using VNPT.HIS.CommonForm;
using VNPT.HIS.Controls.Class;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors.Repository;
using VNPT.HIS.Controls.SubForm;

/**
 *   Chi dinh dich vu Ngoai tru
 *   @ Author : HaNv
 *   @ CreateDate : 01/08/2017
 *   @ UpdateCode : 11/04/2018
 * */
namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K016_ChiDinhDichVu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        #region INIT VARIABLE LOAD
        public static Options opt = new Options();
        CultureInfo culture = new CultureInfo("en-US");

        bool flagMsg = true;
        bool flagMsgMoney = false;
        bool flagLoad = true;
        string icdchinh = "";
        string textchinh = "";
        string icdphu = "";
        bool noEdit = false;
        bool printPrivare = true;
        bool book = false;
        bool bookAll = false;
        bool appointment = false;
        bool printAll = false;
        bool existConfig = false;
        string config = "1";
        bool isTranspost = false;
        bool plDv = false;
        int record = 100;
        bool isColspan = false;
        bool isSwapColumn = false;
        bool isDisplayColumnTt37 = false;
        bool isGroupService = false;
        bool isNotEdit = false;
        bool isShowNumService = false;
        bool isSearchLike = false;
        DateTime currentTime;
        float tongbh = 0;
        float _insurance_bf = 0;
        bool hideColCk = false;
        bool isCbPd = false;
        bool isEditPttt = false;
        bool isShowMsgWr = false;
        bool isKeGiuongTheoNgay = false;
        bool isBacsiKe = false;
        bool isKeChung = false;
        bool isTachCdha = false;
        bool isCbNbh = false;
        bool isCbQuaTg = false;
        string loaigiuongid = opt.LOAIGIUONGID;
        string magiuong = opt.MAGIUONG;
        string isNhomDvKtp = "";
        bool isHienThiMaGiuong = false;
        bool isLayBenhPham = false;
        bool isAutoPrint = false;
        bool isUncheckDuplicate = false;
        bool isNhapGiuongTp = false;
        bool isPhongcd = false;
        bool isNgaydv = false;
        bool isKeleCls = false;
        bool isChanMg = false;
        bool isChancbTu = false;
        string isQn = "";
        string isHtvv = "";
        string isAutoP = "";
        bool isCPdt = false;
        bool isKhoabs = false;
        bool isGiuongdv = false;
        string giuongdv = "";
        string _loainhom_mau = "0";// nhom mau chi dinh dich vu
        bool checkKctt = true;
        bool checkInthukhac = false;
        string _typePrint = "pdf";

        // Các biến hidden
        string hidNGAYTIEPNHAN = "1";
        string hidQUYENLOI = "";
        string hidTUYEN = "";
        string hidTRAN_BHYT = "";
        string hidQUYENLOITHE = "";
        string hidCANTRENKTC = "";
        string hidDUOCVC = "";
        string hidBHFULL = "";
        string hidNHOMDOITUONG = "";
        string hidNGAYHANTHE = "";
        string hidTYLE_THEBHYT = "";
        string hidCHANDOAN_KEMTHEO_BD = "";
        string hidTYLEMIENGIAM = "";
        string hidKHOABNID = "";

        // Biến khởi tạo cho winform
        string[] valueGrdXN = new string[] { };
        string[] valueGrdCDHA = new string[] { };
        string[] valueGrdPTTT = new string[] { };
        DataTable dtCauHinhBv = new DataTable();
        #endregion

        #region LOADFORM + INIT PARENT LAYOUT
        public NGT02K016_ChiDinhDichVu()
        {
            InitializeComponent();
        } 
        
        public void loadData(string menu, DataRowView BenhNhan, string _flgModeView, string _loaidichvu, string subDeptId = "")
        {
            try
            {
                opt = new Options();
                opt.DEPTID = Const.local_khoaId.ToString();
                opt.SUBDEPTID = string.IsNullOrEmpty(subDeptId) ? "-1" : subDeptId;
                opt.SUBDEPTID_LOGIN = Const.local_phongId.ToString();
                opt.SUBDEPTNAME_LOGIN = Const.local_phong.ToString();

                if (BenhNhan.DataView.Table.Columns.Contains("KHAMBENHID")) opt.KHAMBENHID = BenhNhan["KHAMBENHID"].ToString();
                if (BenhNhan.DataView.Table.Columns.Contains("TIEPNHANID")) opt.TIEPNHANID = BenhNhan["TIEPNHANID"].ToString();
                if (BenhNhan.DataView.Table.Columns.Contains("DOITUONGBENHNHANID")) opt.DOITUONGBENHNHANID = BenhNhan["DOITUONGBENHNHANID"].ToString();
                if (BenhNhan.DataView.Table.Columns.Contains("BENHNHANID")) opt.BENHNHANID = BenhNhan["BENHNHANID"].ToString();
                if (BenhNhan.DataView.Table.Columns.Contains("HOSOBENHANID")) opt.HOSOBENHANID = BenhNhan["HOSOBENHANID"].ToString();

                if (BenhNhan.DataView.Table.Columns.Contains("LOAITIEPNHANID")) opt.LOAITIEPNHANID = BenhNhan["LOAITIEPNHANID"].ToString();
                if (BenhNhan.DataView.Table.Columns.Contains("MAUBENHPHAMID")) opt.MAUBENHPHAMID = BenhNhan["MAUBENHPHAMID"].ToString();
                 
                if (_flgModeView != null) opt.MODEFUNCTION = _flgModeView;
                if (_loaidichvu != null) opt.LOAIDICHVU = _loaidichvu;

                if (menu == "dich vu cls")
                {
                    this.Text = "Tạo phiếu chỉ định dịch vụ";
                }
                else if (menu == "taophieuchidinhdichvu")
                {
                    this.Text = "Tạo phiếu chỉ định dịch vụ";
                }
                else if (menu == "thu khac")
                {
                    this.Text = "Phiếu thu khác";
                    opt.CHIDINHDICHVU = "1";
                    opt.LOAIPHIEUMBP = "17";
                }
                else if (menu == "capnhatxetnghiem")
                {
                    this.Text = "Cập nhật phiếu xét nghiệm";
                    //opt.MABENHNHAN = BenhNhan["MABENHNHAN"].ToString(); 
                    opt.LOAIPHIEU = "1";
                }
                else if (menu == "capnhatCDHA")
                {
                    this.Text = "Cập nhật phiếu chuẩn đoán hình ảnh";
                    //opt.MABENHNHAN = BenhNhan["MABENHNHAN"].ToString(); 
                    opt.LOAIPHIEU = "2";
                }
                else if (menu == "capnhatPhieuPTTT")
                {
                    this.Text = "Cập nhật phiếu phẫu thuật thủ thuật";
                    //opt.MABENHNHAN = BenhNhan["MABENHNHAN"].ToString(); 
                    opt.LOAIPHIEU = "5";
                }
                else if (menu == "PhieuVanChuyen")
                {
                    this.Text = "Phiếu vận chuyển"; 
                    opt.CHIDINHDICHVU = "1";
                    opt.LOAIDICHVU = "14";

                    opt.LOAIPHIEU = "16";
                    opt.LOAIPHIEUMBP = opt.LOAIPHIEU;
                }
                else if (menu == "capnhatPhieuVanChuyen")
                {
                    this.Text = "Cập nhật phiếu dịch vụ";
                    //opt.MABENHNHAN = BenhNhan["MABENHNHAN"].ToString(); 
                    opt.CHIDINHDICHVU = "1";
                    opt.LOAIDICHVU = "14";

                    opt.LOAIPHIEU = "16";
                    opt.LOAIPHIEUMBP = opt.LOAIPHIEU;
                }
                else if (menu == "tao_phieu_phu_thu")
                {
                    //              chidinhdichvu: '1',
                    //	 loaidichvu: '8',
                    //	 loaiphieumbp: '6',
                    //	 benhnhanid: rowDataCk.BENHNHANID,
                    //	 khambenhid: rowDataCk.KHAMBENHID,
                    //	 hosobenhanid: rowDataCk.HOSOBENHANID,
                    //	 tiepnhanid: rowDataCk.TIEPNHANID,
                    //	 doituongbenhnhanid: rowDataCk.DOITUONGBENHNHANID,
                    //	 hinhthucvaovienid: $("#hidHINHTHUCVAOVIENID").val(),
                    //	 loaibenhanid: $("#hidLOAIBENHANID").val(),
                    //	 loaitiepnhanid: $("#hidLOAITIEPNHANID").val(),
                    //	 maubenhphamchaid: rowData.MAUBENHPHAMID,
                    //dichvukhambenhid: rowData.DICHVUKHAMBENHID,
                    //subDeptId: $("#hidPHONGID").val() 
                    //      func =../ ngoaitru / NGT02K016_ChiDinhDV" + " & loaidichvu = " + 8, paramInput, "Tạo phiếu phụ thu", 1300, 600);   

                    this.Text = "Tạo phiếu phụ thu"; 
                    opt.CHIDINHDICHVU = "1";
                    opt.LOAIDICHVU = "8"; 
                    opt.LOAIPHIEUMBP = "6";

                    //hinhthucvaovienid
                    // hinhthucvaovienid
                    if (BenhNhan.DataView.Table.Columns.Contains("DICHVUKHAMBENHID")) opt.DICHVUKHAMBENHID = BenhNhan["DICHVUKHAMBENHID"].ToString();
                }

                else if (menu == "phieu_mien_giam")
                {
                    //            chidinhdichvu: '1',
                    //loaidichvu: '19',
                    //loaiphieumbp: '18',
                    //benhnhanid: rowDataP.BENHNHANID,
                    //khambenhid: rowDataP.KHAMBENHID,
                    //hosobenhanid: rowDataP.HOSOBENHANID,
                    //tiepnhanid: rowDataP.TIEPNHANID,
                    //doituongbenhnhanid: rowDataP.DOITUONGBENHNHANID,
                    //loaitiepnhanid: rowDataP.LOAITIEPNHANID,
                    //dichvukhambenhid: rowData.DICHVUKHAMBENHID --> đc truyền vào từ hàm set_DICHVUKHAMBENHID

                    // dlgPopup = DlgUtil.buildPopupUrl("divDlgDichVu", "divDlg", "manager.jsp?func=../ngoaitru/NGT02K016_ChiDinhDV"
                    //paramInput, "Phiếu miễn giảm", 1300, 600); 
                    this.Text = "Phiếu miễn giảm"; 
                    opt.CHIDINHDICHVU = "1";
                    opt.LOAIDICHVU = "19";
                    opt.LOAIPHIEUMBP ="18"; 
                }

                // Fix option
                //opt.MODEKHOA = "1";
                //opt.BENHNHANID = "10889";
                //opt.CHIDINHDICHVU = "1";
                //opt.DOITUONGBENHNHANID = "2";
                //opt.HOSOBENHANID = "11023";
                //opt.KHAMBENHID = "11060";
                //opt.LOAIDICHVU = "13";
                //opt.LOAIPHIEUMBP = "12";
                //opt.LOAITIEPNHANID = "0";
                //opt.MAUBENHPHAMID = "263288";
                //opt.MODEFUNCTION = "0";
                //opt.SUBDEPTID = "6554";
                //opt.TIEPNHANID = "10674";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        } 
        public void set_DICHVUKHAMBENHID(string DICHVUKHAMBENHID)
        {
            opt.DICHVUKHAMBENHID = DICHVUKHAMBENHID;
        }
        private void NGT02K016_ChiDinhDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < tabPane1.Pages.Count; i++)
                    tabPane1.ButtonsPanel.Buttons[i].Properties.Appearance.Font = Const.fontDefault;

                for (int i = 0; i < tabPane3.Pages.Count; i++)
                    tabPane3.ButtonsPanel.Buttons[i].Properties.Appearance.Font = Const.fontDefault;

                LISConnector.LoadLISConfig();
                txtTGCHIDINH.DateTime = DateTime.ParseExact(RequestHTTP.getSysDatetime(), Const.FORMAT_datetime1, culture);
                currentTime = DateTime.ParseExact(RequestHTTP.getSysDatetime(), Const.FORMAT_datetime1, culture);
                initLayoutRight();

                if (!string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                {
                    //Chỉ định dịch vụ khác
                    initDichVuKhac();
                }
                else
                {
                    //Chỉ định dịch vụ cận lâm sàng
                    initDichVuCLS();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        //Load hiển thị và dữ liệu ở layout bên phải - thông tin bệnh nhân, viện phí, dịch vụ đã chỉ định ...
        private void initLayoutRight()
        {
            try
            {
                #region load cấu hình bệnh viện và bác sỹ
                // Cấu hình cho bệnh viện
                string paramsValue = "HIS_EDIT_SOLUONG_GIUONG;HIS_IN_PHIEU_XET_NGIEM_CHUNG;HIS_HEN_XET_NGHIEM;HIS_CLS_BANGHI;HIS_PRINT_CLS_CHUNG;" +
                    "HIS_NHAP_CHUANDOAN;HIS_PL_DICHVU;HIS_CHIDINH_DICHVU;HIS_DUNGTUYEN_VC;HIS_COLSPAN_PARENT;HIS_SWAP_COLUMN_DV;" +
                    "HIS_DISPLAY_CDDV_COLUMN_TT37;NGT_GHICHU_BENHCHINH;HIS_NHOMDV_CHIDINH;HIS_EDIT_SL_DICHVU;HIS_SOLUONG_DICHVU;" +
                    "HIS_TIM_KIEM_GAN_DUNG;HIS_CK_COL_HIDE;HIS_DIEUTRI_THEO_PHACDO;HIS_CANHBAO_PHACDO;HIS_EDIT_PTTT;HIS_SEARCH_NHOM_CK;" +
                    "HIS_KEGIUONG_THEONGAY;HIS_BACSI_KE;HIS_CHIDINHCHUNG;HIS_TACH_CDHA;HIS_CANHBAO_DICHVU_NBH;HIS_CHAN_DICHVU_QUANGAY;" +
                    "HIS_NHOMDV_KHONGTP;HIS_LAYBENHPHAM;HIS_HIEN_THI_MAGIUONG;HIS_BO_CHECK_TRUNGDV;HIS_FILEEXPORT_TYPE;HIS_NHAPGIUONG_TP;" +
                    "HIS_PCD_PTH;HIS_CHAN_NGAY_DV;HIS_KELE_DICHVU_CLS;HIS_CHAN_MAGIUONG;MSG_CANHBAO_DT_TIENG_TU;PHONG_TUDONG_IN;HIS_CHON_PDT;" +
                    "HIS_GIUONG_DICHVU";
                dtCauHinhBv = ServiceChiDinhDichVu.ajaxCALL_SP_O("COM.DS_CAUHINH", paramsValue, 0);
                if (dtCauHinhBv.Rows.Count > 0)
                {
                    if (dtCauHinhBv.Rows[0]["HIS_GIUONG_DICHVU"].ToString() == "1")
                    {
                        isGiuongdv = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CHON_PDT"].ToString() == "1" && opt.LOAITIEPNHANID != "1")
                    {
                        isCPdt = true;
                        if (string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                        {
                            //$("#divPhieuDT").addClass("required");
                            layout_cboMAUBENHPHAMID.AllowHtmlStringInCaption = true;
                            layout_cboMAUBENHPHAMID.Text = "Phiếu ĐT <color = red>(*)<color>";
                        }
                    }
                    if (!string.IsNullOrEmpty(dtCauHinhBv.Rows[0]["PHONG_TUDONG_IN"].ToString()) && dtCauHinhBv.Rows[0]["PHONG_TUDONG_IN"].ToString() != "0")
                    {
                        isAutoP = dtCauHinhBv.Rows[0]["PHONG_TUDONG_IN"].ToString();
                    }
                    if (dtCauHinhBv.Rows[0]["MSG_CANHBAO_DT_TIENG_TU"].ToString() == "1")
                    {
                        isChancbTu = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CHAN_MAGIUONG"].ToString() == "1")
                    {
                        isChanMg = true;
                    }

                    if (dtCauHinhBv.Rows[0]["HIS_KELE_DICHVU_CLS"].ToString() == "1")
                    {
                        isKeleCls = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CHAN_NGAY_DV"].ToString() == "1")
                    {
                        isNgaydv = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_PCD_PTH"].ToString() == "1")
                    {
                        isPhongcd = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_NHAPGIUONG_TP"].ToString() != "0")
                    {
                        isNhapGiuongTp = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_FILEEXPORT_TYPE"].ToString() != "-1")
                    {
                        _typePrint = dtCauHinhBv.Rows[0]["HIS_FILEEXPORT_TYPE"].ToString();
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_BO_CHECK_TRUNGDV"].ToString() != "0")
                    {
                        isUncheckDuplicate = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_HIEN_THI_MAGIUONG"].ToString() != "0")
                    {
                        isHienThiMaGiuong = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_LAYBENHPHAM"].ToString() != "0")
                    {
                        isLayBenhPham = true;
                        layout_BenhPham.Visibility = LayoutVisibility.Always;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_NHOMDV_KHONGTP"].ToString() != "0")
                    {
                        isNhomDvKtp = dtCauHinhBv.Rows[0]["HIS_NHOMDV_KHONGTP"].ToString();
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CHAN_DICHVU_QUANGAY"].ToString() != "0")
                    {
                        isCbQuaTg = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CANHBAO_DICHVU_NBH"].ToString() != "0")
                    {
                        isCbNbh = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_EDIT_SOLUONG_GIUONG"].ToString() != "1")
                    {
                        noEdit = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_IN_PHIEU_XET_NGIEM_CHUNG"].ToString() == "1")
                    {
                        printPrivare = false;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_HEN_XET_NGHIEM"].ToString() == "1")
                    {
                        book = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CLS_BANGHI"].ToString() != "0")
                    {
                        record = int.Parse(dtCauHinhBv.Rows[0]["HIS_CLS_BANGHI"].ToString());
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_PRINT_CLS_CHUNG"].ToString() != "0")
                    {
                        printAll = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CHIDINH_DICHVU"].ToString() == "1")
                    {
                        existConfig = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_DUNGTUYEN_VC"].ToString() == "1")
                    {
                        isTranspost = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_COLSPAN_PARENT"].ToString() == "1")
                    {
                        isColspan = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_SWAP_COLUMN_DV"].ToString() == "1")
                    {
                        isSwapColumn = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_DISPLAY_CDDV_COLUMN_TT37"].ToString() == "1")
                    {
                        isDisplayColumnTt37 = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_NHOMDV_CHIDINH"].ToString() == "1")
                    {
                        isGroupService = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_PL_DICHVU"].ToString() == "1")
                    {
                        plDv = true;
                        layout_chkAllService.Visibility = LayoutVisibility.Always;
                    }
                    else
                    {
                        layout_chkAllService.Visibility = LayoutVisibility.Never;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_EDIT_SL_DICHVU"].ToString() == "1")
                    {
                        isNotEdit = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_TIM_KIEM_GAN_DUNG"].ToString() == "1")
                    {
                        isSearchLike = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CK_COL_HIDE"].ToString() == "1")
                    {
                        hideColCk = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_DIEUTRI_THEO_PHACDO"].ToString() == "1") {
                        layout_btnPhacDo.Visibility = LayoutVisibility.Always;
                    } else {
                        layout_btnPhacDo.Visibility = LayoutVisibility.Never;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CANHBAO_PHACDO"].ToString() == "1")
                    {
                        isCbPd = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_EDIT_PTTT"].ToString() == "1")
                    {
                        isEditPttt = true;
                    }

                    if (dtCauHinhBv.Rows[0]["HIS_BACSI_KE"].ToString() == "1" && string.IsNullOrEmpty(opt.CHIDINHDICHVU) && opt.LOAITIEPNHANID.Equals("0"))
                    {
                        isBacsiKe = true;
                    }
                    else
                    {
                        layout_cboBACSIID.Visibility = LayoutVisibility.Never;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_CHIDINHCHUNG"].ToString() == "1")
                    {
                        isKeChung = true;
                        tabNavigationPage1.Caption = "Dịch vụ CLS";
                        tabPane1.Pages.RemoveRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { tabNavigationPage2, tabNavigationPage3 });
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_KEGIUONG_THEONGAY"].ToString() == "1")
                    {
                        isKeGiuongTheoNgay = true;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_SOLUONG_DICHVU"].ToString() == "1")
                    {
                        if (opt.LOAIDICHVU == "13")
                        {
                            // Hide layout_SONGAYGIUONG
                            layout_SONGAYGIUONG.Visibility = LayoutVisibility.Never;
                            // Show layout_SOLUONGDICHVU
                            layout_SOLUONGDICHVU.Visibility = LayoutVisibility.Always;
                            txtSOLUONGDICHVU.Text = "1";
                            isShowNumService = true;
                        }
                    }
                    if (dtCauHinhBv.Rows[0]["NGT_GHICHU_BENHCHINH"].ToString() == "1")
                    {
                        layout_GHICHU_BENHCHINH.Visibility = LayoutVisibility.Always;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_NHAP_CHUANDOAN"].ToString() != "1")
                    {
                        cboChanDoan.Enabled = false;
                        cboChanDoanKT.Enabled = false;
                        btnCLEARCHANDOANKT.Enabled = false;
                    }
                    if (dtCauHinhBv.Rows[0]["HIS_TACH_CDHA"].ToString() == "1")
                    {
                        isTachCdha = true;
                    }
                }

                //cau hinh cho bac si
                paramsValue = "HIS_HEN_XET_NGHIEM;HIS_CANH_BAO_TRAN;NGT_PHIEUKHAM_TUDONGIN;HIS_BACSI_KE_THEOKHOA";
                DataTable dtCauHinhBs = new DataTable();
                dtCauHinhBs = ServiceChiDinhDichVu.ajaxCALL_SP_O("COM.DS_CAUHINH_ND", paramsValue, 0);
                if (dtCauHinhBs.Rows.Count > 0)
                {
                    if (dtCauHinhBs.Rows[0]["HIS_HEN_XET_NGHIEM"].ToString() == "1")
                    {
                        bookAll = true;
                    }
                    if (dtCauHinhBs.Rows[0]["HIS_CANH_BAO_TRAN"].ToString() == "1")
                    {
                        isShowMsgWr = true;
                    }
                    if (dtCauHinhBs.Rows[0]["NGT_PHIEUKHAM_TUDONGIN"].ToString() == "1")
                    {
                        isAutoPrint = true;
                    }
                    if (dtCauHinhBs.Rows[0]["HIS_BACSI_KE_THEOKHOA"].ToString() == "1" && opt.MODEKHOA == "1")
                    {
                        //$('#divKhoaBacSi').show();
                        layout_cboKHOACHIDINHCHID.Visibility = LayoutVisibility.Always;
                        layout_cboPHONGCHIDINHCHID.Visibility = LayoutVisibility.Always;
                        layout_cboBACSIKHOACDID.Visibility = LayoutVisibility.Always;
                        //Load dữ liệu combobox khoa chỉ định
                        DataTable dtKhoaCd = new DataTable();
                        dtKhoaCd = ServiceChiDinhDichVu.ajaxExecuteQuery("NTU02D037.EV001", new String[] { }, new String[] { });
                        DataRow drKhoaCd = dtKhoaCd.NewRow();
                        drKhoaCd[0] = "";
                        drKhoaCd[1] = "---Chọn---";
                        dtKhoaCd.Rows.InsertAt(drKhoaCd, 0);
                        cboKHOACHIDINHCHID.setData(dtKhoaCd, "col1", "col2");
                        cboKHOACHIDINHCHID.setColumn("col1", -1, "", 0);
                        cboKHOACHIDINHCHID.setColumn("col2", 1, "", 0);
                        cboKHOACHIDINHCHID.SelectIndex = 0;
                        cboKHOACHIDINHCHID.setEvent(cboKHOACHIDINHCHID_OnChange);
                        isKhoabs = true;
                    }
                }
                #endregion

                #region Lấy dữ liệu bệnh nhân
                DataTable dtBenhNhan = new DataTable();
                //DataBenhNhan dataBN = new DataBenhNhan();
                //dataBN = ServiceChiDinhDichVu.getThongTinBN(opt.KHAMBENHID + "$" + opt.SUBDEPTID);
                dtBenhNhan = ServiceChiDinhDichVu.ajaxCALL_SP_O("NGT02K016.LAYDL", opt.KHAMBENHID + "$" + opt.SUBDEPTID, 0);
                if (dtBenhNhan.Rows.Count > 0)
                {
                    //setObjectToForm
                    /*
                        BENHNHANID: 19751,
                        MABENHNHAN: BN00000539,
                        TENBENHNHAN: MTEST KÊ GIƯỜNG 1912,
                        NAMSINH: 1990,
                        DIACHI: Xã Hương Sơn-Huyện Quang Bình-Hà Giang,
                        DOITUONGBENHNHANID: 1,
                        LOAITIEPNHANID: 0,
                        TYLEMIENGIAM: ,
                        TEN_DTBN: ,
                        MA_BHYT: DN4555938888121,
                        HANBHYT: 01/01/2017-31/12/2017,
                        QUYENLOI: 80,
                        NGAYTIEPNHAN: 19/12/2017 15:29:05,
                        BHFULL: 0,
                        GHICHU_BENHCHINH: ,
                        TKCHANDOAN_NEW: ,
                        CHANDOAN_NEW: ,
                        CHANDOAN_KT_NEW: ,
                        TUYEN: 1,
                        CANTRENKTC: 1,
                        DUOCVC: 0,
                        NHOMDOITUONG: 4,
                        NGAYHANTHE: 31/12/2017,
                        THONGTINDIEUTRI: Ngày tiếp nhận:19/12/2017 15:29:05-Ngày ra viện:22/12/2017 09:36:14-Số ngày:4,
                        TYLE_THEBHYT: 80
                     */
                    hidTYLE_THEBHYT = dtBenhNhan.Rows[0]["TYLE_THEBHYT"].ToString();
                    hidCANTRENKTC = dtBenhNhan.Rows[0]["CANTRENKTC"].ToString();
                    hidBHFULL = dtBenhNhan.Rows[0]["BHFULL"].ToString();
                    hidDUOCVC = dtBenhNhan.Rows[0]["DUOCVC"].ToString();
                    hidNHOMDOITUONG = dtBenhNhan.Rows[0]["NHOMDOITUONG"].ToString();
                    hidNGAYHANTHE = dtBenhNhan.Rows[0]["NGAYHANTHE"].ToString();
                    hidTYLEMIENGIAM = dtBenhNhan.Rows[0]["TYLEMIENGIAM"].ToString();
                    hidTUYEN = dtBenhNhan.Rows[0]["TUYEN"].ToString();
                    hidNGAYTIEPNHAN = dtBenhNhan.Rows[0]["NGAYTIEPNHAN"].ToString();

                    txtMABENHNHAN.Text = dtBenhNhan.Rows[0]["MABENHNHAN"].ToString();
                    txtTENBENHNHAN.Text = dtBenhNhan.Rows[0]["TENBENHNHAN"].ToString();
                    txtNAMSINH.Text = dtBenhNhan.Rows[0]["NAMSINH"].ToString();
                    txtDIACHI.Text = dtBenhNhan.Rows[0]["DIACHI"].ToString();
                    txtTEN_DTBN.Text = dtBenhNhan.Rows[0]["TEN_DTBN"].ToString();
                    txtQUYENLOI.Text = dtBenhNhan.Rows[0]["QUYENLOI"].ToString();
                    txtMA_BHYT.Text = dtBenhNhan.Rows[0]["MA_BHYT"].ToString();
                    txtHANBHYT.Text = dtBenhNhan.Rows[0]["HANBHYT"].ToString();
                    txtTHONGTINDIEUTRI.Text = dtBenhNhan.Rows[0]["THONGTINDIEUTRI"].ToString();
                    txtGHICHU_BENHCHINH.Text = dtBenhNhan.Rows[0]["GHICHU_BENHCHINH"].ToString();
                    icdchinh = dtBenhNhan.Rows[0]["TKCHANDOAN_NEW"].ToString();
                    icdphu = dtBenhNhan.Rows[0]["CHANDOAN_KT_NEW"].ToString();
                    textchinh = dtBenhNhan.Rows[0]["CHANDOAN_NEW"].ToString();
                    isQn = dtBenhNhan.Columns.Contains("SUB_DTBNID") ? dtBenhNhan.Rows[0]["SUB_DTBNID"].ToString() : "" ;
                    isHtvv = dtBenhNhan.Columns.Contains("HINHTHUCVAOVIENID") ? dtBenhNhan.Rows[0]["HINHTHUCVAOVIENID"].ToString() : "";

                    // Load thông tin viện phí
                    DataTable dtVienPhi = new DataTable();
                    dtVienPhi = ServiceChiDinhDichVu.ajaxCALL_SP_O("VPI01T001.26", opt.TIEPNHANID, 0);
                    if (dtVienPhi.Rows.Count > 0)
                    {
                        lblTAMUNG.Text = float.Parse(dtVienPhi.Rows[0]["TAMUNG"].ToString()).ToString("N2", culture) + "đ";
                        lblMIENGIAM.Text = float.Parse(dtVienPhi.Rows[0]["MIENGIAM"].ToString()).ToString("N2", culture) + "đ";
                        lblDANOP.Text = dtVienPhi.Rows[0]["DANOP"].ToString() + "đ";
                        lblTONGCHIPHI.Text = float.Parse(dtVienPhi.Rows[0]["TONGTIENDV"].ToString()).ToString("N2", culture) + "đ";
                        lblBHTRA.Text = float.Parse(dtVienPhi.Rows[0]["BHYT_THANHTOAN"].ToString()).ToString("N2", culture) + "đ";
                        lblBNTRA.Text = (float.Parse(dtVienPhi.Rows[0]["TONGTIENDV"].ToString()) - float.Parse(dtVienPhi.Rows[0]["BHYT_THANHTOAN"].ToString())).ToString("N2", culture) + "đ";
                        
                        tongbh = float.Parse(dtVienPhi.Rows[0]["TONGTIENDV_BH"].ToString());
                        float nopthem = float.Parse(dtVienPhi.Rows[0]["TAMUNG"].ToString()) - float.Parse(dtVienPhi.Rows[0]["TONGTIENDV"].ToString()) +
                            float.Parse(dtVienPhi.Rows[0]["BHYT_THANHTOAN"].ToString()) + float.Parse(dtVienPhi.Rows[0]["MIENGIAM"].ToString()) +
                            float.Parse(dtVienPhi.Rows[0]["DANOP"].ToString());
                        if (nopthem <= 0)
                        {
                            if (dtBenhNhan.Rows[0]["LOAITIEPNHANID"].ToString().Equals("0") && flagMsg)
                            {
                                flagMsgMoney = true;
                            }
                            lblCHENHLECH.Text = nopthem.ToString("N2", culture) + "đ";
                            lblNOPTHEM.Text = (-1 * nopthem).ToString("N2", culture) + "đ";
                        }
                        else
                        {
                            lblCHENHLECH.Text = nopthem.ToString("N2", culture) + "đ";
                            lblNOPTHEM.Text = 0 + "đ";
                        }
                        hidTRAN_BHYT = dtVienPhi.Rows[0]["TRAN_BHYT"].ToString();
                        hidQUYENLOI = dtVienPhi.Rows[0]["TYLE_BHYT"].ToString();
                        hidQUYENLOITHE = dtVienPhi.Rows[0]["TYLE_THE"].ToString();
                    }
                    if(dtBenhNhan.Rows[0]["LOAITIEPNHANID"].ToString().Equals("1")){
				        isBacsiKe = false;
                        layout_cboBACSIID.Visibility = LayoutVisibility.Never;
			        }
                }
                #endregion

                #region Load Phiếu điều trị, chuẩn đoán và chuẩn đoán kèm theo
                DataTable dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
                // ServiceChiDinhDichVu.getChuanDoan(true, Const.tbl_CDDV_ICD, new String[] { "CG.ICD10" }, new String[] { }, new String[] { });
                cboChanDoan.setData(dt, "ICD10CODE", "ICD10NAME");
                cboChanDoan.setColumn("RN", -1, "", 0);
                cboChanDoan.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                cboChanDoan.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
                cboChanDoan.setEvent_Check(cboChanDoan_Check);

                cboChanDoanKT.setData(dt, "ICD10CODE", "ICD10NAME");
                cboChanDoanKT.setColumn("RN", -1, "", 0);
                cboChanDoanKT.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                cboChanDoanKT.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
                cboChanDoan.setEvent_Check(cboChanDoanKT_Check);

                //Load dữ liệu combobox phiếu điều trị
                DataTable dtPhieuDT = new DataTable();
                dtPhieuDT = ServiceChiDinhDichVu.ajaxExecuteQuery("COM.PHIEUDIEUTRI", new String[] { "[0]", "[1]" }, new String[] { opt.KHAMBENHID, "4" });
                DataRow drPhieuDT = dtPhieuDT.NewRow();
                drPhieuDT[0] = "";
                drPhieuDT[1] = "Chưa có phiếu điều trị";
                dtPhieuDT.Rows.InsertAt(drPhieuDT, 0);
                cboMAUBENHPHAMID.setData(dtPhieuDT, "col1", "col2");
                cboMAUBENHPHAMID.setColumn("col1", -1, "", 0);
                cboMAUBENHPHAMID.setColumn("col2", 1, "", 0);
                cboMAUBENHPHAMID.SelectIndex = 0;
                cboMAUBENHPHAMID.setEvent(cboMAUBENHPHAMID_OnChange);

                // Load cboBACSI
                if (isBacsiKe)
                {
                    DataTable dtBacSi = ServiceChiDinhDichVu.ajaxExecuteQuery("NGT02K016.EV002", new String[] { "[0]" }, new String[] { opt.DEPTID });
                    DataRow drBacSi = dtBacSi.NewRow();
                    drBacSi[0] = "";
                    drBacSi[1] = "Chọn";
                    dt.Rows.InsertAt(drBacSi, 0);
                    cboBACSIID.setData(dt, 0, 1);
                    cboBACSIID.setColumn("col1", -1, "", 0);
                    cboBACSIID.setColumn("col2", 0, "", 0);
                }

                // Load dữ liệu từ mẫu bệnh phẩm khi sửa phiếu
                if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                {
                    //$.when(dGridCddv.promise(), dGridXn.promise(), dGridCdha.promise(), dGridCk.promise()).then
                    if (opt.LOAIPHIEU == "1")
                    {
                        tabPane1.SelectedPage = tabNavigationPage1;
                    }
                    else if (opt.LOAIPHIEU == "2")
                    {
                        tabPane1.SelectedPage = tabNavigationPage2;
                    }
                    else if (opt.LOAIPHIEU == "5")
                    {
                        tabPane1.SelectedPage = tabNavigationPage3;
                    }

                    setGridDSCD(1, null);
                    layout_SONGAYGIUONG.Visibility = LayoutVisibility.Never;
                    // Load chẩn đoán và chẩn đoán kèm theo của phiếu chỉ định
                    //ChanDoanInfo chanDoan = ServiceChiDinhDichVu.getThongTinChuanDoan(opt.MAUBENHPHAMID);
                    DataTable dtChanDoan = ServiceChiDinhDichVu.ajaxExecuteQueryO("NTU02D009.EV004", opt.MAUBENHPHAMID);
                    //[{"CHANDOAN": "Viêm ruột do Salmonella","MACHANDOAN": "A02.0","GHICHU_BENHCHINH": "","CHANDOAN_KEMTHEO": "A02.1-Nhiễm trùng huyết do Salmonella",
                    //"CHANDOAN_KEMTHEO_BD": "","MAGIUONG": "","LOAIGIUONGID": null,"PHIEUDIEUTRIID": null,"TGCHIDINH": "26/12/2017 10:27:29","BENHPHAM": "0","BACSYDIEUTRIID": "10529"}]
                    if (dtChanDoan.Rows.Count > 0)
                    {
                        // setObjectToForm
                        hidCHANDOAN_KEMTHEO_BD = dtChanDoan.Rows[0]["CHANDOAN_KEMTHEO_BD"].ToString();
                        txtTGCHIDINH.DateTime = DateTime.ParseExact(dtChanDoan.Rows[0]["TGCHIDINH"].ToString(), Const.FORMAT_datetime1, culture);
                        txtGHICHU_BENHCHINH.Text = dtChanDoan.Rows[0]["GHICHU_BENHCHINH"].ToString();
                        if (string.IsNullOrEmpty(loaigiuongid)){
                            loaigiuongid = dtChanDoan.Rows[0]["LOAIGIUONGID"].ToString();
                        }
                        if (string.IsNullOrEmpty(magiuong))
                        {
                            magiuong = dtChanDoan.Rows[0]["MAGIUONG"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dtChanDoan.Rows[0]["BACSYDIEUTRIID"].ToString()))
                        {
                            cboBACSIID.SelectValue = dtChanDoan.Rows[0]["BACSYDIEUTRIID"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dtChanDoan.Rows[0]["PHIEUDIEUTRIID"].ToString()))
                        {
                            cboMAUBENHPHAMID.SelectValue = dtChanDoan.Rows[0]["PHIEUDIEUTRIID"].ToString();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(dtChanDoan.Rows[0]["MACHANDOAN"].ToString()))
                            {
                                cboChanDoan.SelectedValue = dtChanDoan.Rows[0]["MACHANDOAN"].ToString();
                            }
                            if (!string.IsNullOrEmpty(dtChanDoan.Rows[0]["CHANDOAN_KEMTHEO"].ToString()))
                            {
                                cboChanDoanKT.SelectedText = dtChanDoan.Rows[0]["CHANDOAN_KEMTHEO"].ToString();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mẫu bệnh phẩm");
                        return;
                    }
                }
                else
                {
                    if (opt.PHIEUDIEUTRIID != null)
                    {
                        cboMAUBENHPHAMID.SelectValue = opt.PHIEUDIEUTRIID;
                    }
                    else
                    {
                        cboChanDoan.searchLookUpEdit.EditValue = icdchinh;
                        cboChanDoanKT.SelectedText = icdphu;
                    }

                    if (flagMsgMoney)
                    {
                        MessageBox.Show("Bệnh nhân có chi phí phát sinh lớn hơn tiền tạm ứng");
                        if (Const.local_user.HOSPITAL_ID == "3" && opt.LOAITIEPNHANID == "1")
                        {
                            btnLuuIn.Enabled = false;
                            btnLuu.Enabled = false;
                        }
                        flagMsgMoney = false;
                    }
                }
                #endregion

                // danh sach dich vu da chi dinh gridDSCDOld
                tabPane3.SelectedPage = tabNavigationPage7;
                gridDSCDOld.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                gridDSCDOld.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                gridDSCDOld.gridView.OptionsView.ShowAutoFilterRow = false;
                gridDSCDOld.SetReLoadWhenFilter(true);
                gridDSCDOld.setEvent(setGridDSCDOld);
                setGridDSCDOld(1, null);
                tabPane3.SelectedPage = tabNavigationPage6;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void initDichVuCLS()
        {
            config = ServiceChiDinhDichVu.ajaxCALL_SP_S("COM.CAUHINH", "HIS_CAUHINH_YEUCAU_CHUANDOAN_DICHVU");
            if (config == "0")
            {
                cboChanDoan.Option_CaptionValidate = false;
            }

            DataTable dt = new DataTable();
            if (!book)
            {
                gridView4.Columns[2].VisibleIndex = -1;// $("#grdDSCD").jqGrid('hideCol', 'HEN');
            }
            if (isNotEdit)
            {
                gridView4.Columns[4].OptionsColumn.AllowEdit = false; //gridDSCD_SOLUONG
            }

            // Nếu kê chung -> chỉ còn lại 1 tab chỉ định duy nhất
            if (!isKeChung)
            {
                //1.  Set data combobox NhomXetNghiem
                tabPane1.SelectedPage = tabNavigationPage1;
                dt = ServiceChiDinhDichVu.ajaxExecuteQuery("DMC.CBNDV.01", new String[] { "[0]" }, new String[] { "3" }, Const.tbl_CDDV_DSXETNGHIEM);
                DataRow drXetNghiem = dt.NewRow();
                drXetNghiem[0] = "-1";
                drXetNghiem[1] = "--- Tất cả ---";
                dt.Rows.InsertAt(drXetNghiem, 0);
                cboNHOMXETNGHIEMID.setEvent(cboNHOMXETNGHIEMID_OnChange);
                cboNHOMXETNGHIEMID.setData(dt, 0, 1);
                cboNHOMXETNGHIEMID.setColumn("col1", 0, "Mã nhóm", 0);
                cboNHOMXETNGHIEMID.setColumn("col2", 1, "Tên nhóm", 0);
                cboNHOMXETNGHIEMID.SelectedIndex = 0;

                //2.  Set data combobox CDHA
                tabPane1.SelectedPage = tabNavigationPage2;
                dt = ServiceChiDinhDichVu.ajaxExecuteQuery("DMC.CBNDV.01", new String[] { "[0]" }, new String[] { "4" }, Const.tbl_CDDV_DSCDHA);
                DataRow drCDHA = dt.NewRow();
                drCDHA[0] = "-1";
                drCDHA[1] = "--- Tất cả ---";
                dt.Rows.InsertAt(drCDHA, 0);
                cboNHOMCDHAID.setEvent(cboNHOMCDHAID_OnChange);
                cboNHOMCDHAID.setData(dt, 0, 1);
                cboNHOMCDHAID.setColumn("col1", 0, "Mã nhóm", 0);
                cboNHOMCDHAID.setColumn("col2", 1, "Tên nhóm", 0);
                cboNHOMCDHAID.SelectedIndex = 0;

                //3.  Set data combobox PTTT
                tabPane1.SelectedPage = tabNavigationPage3;
                if (dtCauHinhBv.Rows[0]["HIS_SEARCH_NHOM_CK"].ToString() == "1")
                {
                    dt = ServiceChiDinhDichVu.ajaxExecuteQuery("DMC.CBNDV.01", new String[] { "[0]" }, new String[] { "5" });
                }
                else
                {
                    dt = ServiceChiDinhDichVu.ajaxExecuteQuery("DMC.CHUYENKHOA.01", new String[] { }, new String[] { });
                }
                DataRow drCK = dt.NewRow();
                drCK[0] = "-1";
                drCK[1] = "--- Tất cả ---";
                dt.Rows.InsertAt(drCK, 0);
                cboCHUYENKHOAID.setEvent(cboNHOMPTTTID_OnChange);
                cboCHUYENKHOAID.setData(dt, 0, 1);
                cboCHUYENKHOAID.setColumn("col1", 0, "Mã chuyên khoa", 0);
                cboCHUYENKHOAID.setColumn("col2", 1, "Tên chuyên khoa", 0);
                cboCHUYENKHOAID.SelectedIndex = 0;
                tabPane1.SelectedPage = tabNavigationPage1;
                if (!string.IsNullOrEmpty(opt.LOAIPHIEU))
                {
                    if (opt.LOAIPHIEU == "2")
                    {
                        tabPane1.SelectedPage = tabNavigationPage2;
                    }
                    else if (opt.LOAIPHIEU == "5")
                    {
                        tabPane1.SelectedPage = tabNavigationPage3;
                    }
                }
            }
            else
            {
                dt = ServiceChiDinhDichVu.ajaxExecuteQuery("NGT02K016.EV004", new String[] { }, new String[] { });
                DataRow drKeChung = dt.NewRow();
                drKeChung[0] = "-1";
                drKeChung[1] = "--- Tất cả ---";
                dt.Rows.InsertAt(drKeChung, 0);
                cboNHOMXETNGHIEMID.setEvent(cboNHOMXETNGHIEMID_OnChange);
                cboNHOMXETNGHIEMID.setData(dt, 0, 1);
                cboNHOMXETNGHIEMID.setColumn("col1", 0, "Mã nhóm", 0);
                cboNHOMXETNGHIEMID.setColumn("col2", 1, "Tên nhóm", 0);
                cboNHOMXETNGHIEMID.SelectedIndex = 0;
            }
            // Set data gridview
            setDataToGridDvCLS();
        }

        private void initDichVuKhac()
        {
            //btnLuuIn.Hide();
            // Hiển thị layout dịch vụ khác, ẩn layout DV cận lâm sàng
            layout_DVKhac.Visibility = LayoutVisibility.Always;
            layout_DVCLS.Visibility = LayoutVisibility.Never;
            if (isHienThiMaGiuong && opt.LOAIDICHVU.Equals("13"))
            {
                //$('#divXepGiuong').show();
                layout_cboLOAIGIUONG.Visibility = LayoutVisibility.Always;
                layout_cboMAGIUONG.Visibility = LayoutVisibility.Always;
                //$('#divChanDoanC').hide();
                layout_cboChanDoan.Visibility = LayoutVisibility.Never;
                layout_GHICHU_BENHCHINH.Visibility = LayoutVisibility.Never;
                layout_chkKHAN.Visibility = LayoutVisibility.Never;

                DataTable dtGiuong = ServiceChiDinhDichVu.ajaxExecuteQueryO("NTU02D009.EV015", opt.KHAMBENHID);
                //[{"KHOAID": "5068","MAGIUONG": "","LOAIGIUONG": null,"DICHVUID": null}]
                if (dtGiuong.Rows.Count > 0)
                {
                    hidKHOABNID = dtGiuong.Rows[0]["KHOAID"].ToString();
                    DataTable dtLoai = ServiceChiDinhDichVu.ajaxExecuteQuery("NTU01H014.EV001", new String[] { }, new String[] { });
                    DataRow drLoai = dtLoai.NewRow();
                    drLoai[0] = "";
                    drLoai[1] = "Chọn";
                    dtLoai.Rows.InsertAt(drLoai, 0);
                    cboLOAIGIUONG.setData(dtLoai, 0, 1);
                    cboLOAIGIUONG.setColumn("col1", -1, "", 0);
                    cboLOAIGIUONG.setColumn("col2", 0, "", 0);
                    cboLOAIGIUONG.setEvent(cboLOAIGIUONG_OnChange);
                    cboLOAIGIUONG.SelectValue = string.IsNullOrEmpty(loaigiuongid) ? dtGiuong.Rows[0]["LOAIGIUONG"].ToString() : loaigiuongid;
                    cboMAGIUONG.SelectValue = string.IsNullOrEmpty(magiuong) ? dtGiuong.Rows[0]["MAGIUONG"].ToString() : magiuong;
                }
            }
            checkInthukhac = true;
            cboChanDoan.Option_CaptionValidate = false;

            // Khởi tạo lại hiển thị grid danh sách dv chỉ định cho dịch vụ khác
            gridView4.Columns[2].VisibleIndex = -1; //gridDSCD_HEN
            gridView4.Columns[6].VisibleIndex = -1; //gridDSCD_PHONG_TH
            gridView4.Columns[5].VisibleIndex = 2; //gridDSCD_TYLEDV

            if (opt.LOAIDICHVU == "13")
            {
                // $('#divChuanDoanKT').hide();
                layout_cboChanDoanKT.Visibility = LayoutVisibility.Never;
                layoutControlItem20.Visibility = LayoutVisibility.Never;
                layoutControlItem21.Visibility = LayoutVisibility.Never;
                // $('#divThongTinGiuong').show();
                layout_THONGTINDIEUTRI.Visibility = LayoutVisibility.Always;
                layout_SONGAYGIUONG.Visibility = LayoutVisibility.Always;
                txtSONGAYGIUONG.Text = "1";

                if (noEdit)
                {
                    gridView4.Columns[4].OptionsColumn.AllowEdit = false; //gridDSCD_SOLUONG
                }
            }
            else
            {
                gridView4.Columns[5].VisibleIndex = -1; //$("#grdDSCD").jqGrid('hideCol', 'TYLEDV');
            }

            // load gridDichVuKhac
            gridDichVuKhac.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            gridDichVuKhac.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            // Không hiển thị checkbox checkall ở header
            gridDichVuKhac.gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            // Không hiển thị checkbox checkall trong group
            gridDichVuKhac.gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            // Hiển thị checkbox
            gridDichVuKhac.gridView.OptionsSelection.MultiSelect = true;
            gridDichVuKhac.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // editable
            gridDichVuKhac.gridView.OptionsBehavior.Editable = false;
            // Chiều rộng các cột dãn kín grid
            gridDichVuKhac.gridView.OptionsView.ColumnAutoWidth = false;
            // Hiển thị dòng filter
            gridDichVuKhac.gridView.OptionsView.ShowAutoFilterRow = true;
            gridDichVuKhac.SetReLoadWhenFilter(true);
            gridDichVuKhac.setEvent(setGridDichVuKhac);
            gridDichVuKhac.gridView.RowClick += gridDichVuKhac_RowClick;
            setGridDichVuKhac(1, null);

            if (opt.LOAIDICHVU.Equals("19"))
            {
                gridView4.Columns[9].VisibleIndex = -1; // $("#grdDSCD").jqGrid('hideCol', 'BHYT_TRA');
                gridView4.Columns[10].VisibleIndex = -1; // $("#grdDSCD").jqGrid('hideCol', 'MIENGIAM');
            }
        }
        #endregion

        #region THIẾT LẬP DỮ LIỆU, HIỂN THỊ CHO CÁC GRID (setDataToGridDvCLS, setGridXetNghiem, setGridDichVuKhac...)
        private void setDataToGridDvCLS()
        {
            string loadPlService = "0";
            if (plDv)
            {
                if (chkAllService.Checked)
                {
                    loadPlService = "0";
                }
                else
                {
                    loadPlService = "1";
                }
            }
            else
            {
                loadPlService = "0";
            }

            // load gridXetNghiem
            valueGrdXN = new string[] { opt.SUBDEPTID, "3", opt.DEPTID, "-1", opt.KHAMBENHID, txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1), loadPlService };
            gridXetNghiem.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            gridXetNghiem.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            // Không hiển thị checkbox checkall ở header
            gridXetNghiem.gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
            // Không hiển thị checkbox checkall trong group
            gridXetNghiem.gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
            // Hiển thị checkbox
            gridXetNghiem.gridView.OptionsSelection.MultiSelect = true;
            gridXetNghiem.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            // editable
            gridXetNghiem.gridView.OptionsBehavior.Editable = false;
            // Chiều rộng các cột dãn kín grid
            gridXetNghiem.gridView.OptionsView.ColumnAutoWidth = false;
            // Hiển thị dòng filter
            gridXetNghiem.gridView.OptionsView.ShowAutoFilterRow = true;
            gridXetNghiem.SetReLoadWhenFilter(true);
            gridXetNghiem.setEvent(setGridXetNghiem);
            gridXetNghiem.gridView.RowClick += gridXetNghiem_RowClick;
            setGridXetNghiem(1, null);

            if (!isKeChung)
            {
                // load gridCDHA
                valueGrdCDHA = new string[] { opt.SUBDEPTID, "4", opt.DEPTID, "-1", opt.KHAMBENHID, txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1), loadPlService };
                gridCDHA.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                gridCDHA.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                // Không hiển thị checkbox checkall ở header
                gridCDHA.gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                // Không hiển thị checkbox checkall trong group
                gridCDHA.gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
                // Hiển thị checkbox
                gridCDHA.gridView.OptionsSelection.MultiSelect = true;
                gridCDHA.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                // editable
                gridCDHA.gridView.OptionsBehavior.Editable = false;
                // Chiều rộng các cột dãn kín grid
                gridCDHA.gridView.OptionsView.ColumnAutoWidth = false;
                // Hiển thị dòng filter
                gridCDHA.gridView.OptionsView.ShowAutoFilterRow = true;
                gridCDHA.SetReLoadWhenFilter(true);
                gridCDHA.setEvent(setGridCDHA);
                gridCDHA.gridView.RowClick += gridCDHA_RowClick;
                setGridCDHA(1, null);

                // load gridPTTT
                valueGrdPTTT = new string[] { opt.SUBDEPTID, "5", opt.DEPTID, "-1", opt.KHAMBENHID, txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1), loadPlService };
                gridPTTT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                gridPTTT.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                // Không hiển thị checkbox checkall ở header
                gridPTTT.gridView.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.False;
                // Không hiển thị checkbox checkall trong group
                gridPTTT.gridView.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.False;
                // Hiển thị checkbox
                gridPTTT.gridView.OptionsSelection.MultiSelect = true;
                gridPTTT.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                // editable
                gridPTTT.gridView.OptionsBehavior.Editable = false;
                // Chiều rộng các cột dãn kín grid
                gridPTTT.gridView.OptionsView.ColumnAutoWidth = false;
                // Hiển thị dòng filter
                gridPTTT.gridView.OptionsView.ShowAutoFilterRow = true;
                gridPTTT.SetReLoadWhenFilter(true);
                gridPTTT.setEvent(setGridPTTT);
                gridPTTT.gridView.RowClick += gridPTTT_RowClick;
                setGridPTTT(1, null);
            }
        }

        private void setGridXetNghiem(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    ResponsList ds = new ResponsList();
                    //if (gridXetNghiem.ReLoadWhenFilter)
                    //{
                    //    if (gridXetNghiem.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridXetNghiem.tableFlterColumn);
                    //}
                    if (isKeChung)
                    {
                        // Load dữ liệu kê chung
                        ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D009.EV014", page, gridXetNghiem.ucPage1.getNumberPerPage(),
                            new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]" }, valueGrdXN);
                    }
                    else
                    {
                        // Load dữ liệu tab xét nghiệm
                        ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D009.EV001", page, gridXetNghiem.ucPage1.getNumberPerPage(),
                            new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]" }, valueGrdXN);
                    }
                    gridXetNghiem.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "CHUYENKHOAID", "MADICHVU", "TENDICHVU", "TEN_TT37", "GIABHYT", "GIANHANDAN", "GIADICHVU" });
                    gridXetNghiem.setData(dt, ds.total, ds.page, ds.records);
                    gridXetNghiem.setColumnAll(false);
                    setPropColumnGrid(gridXetNghiem.gridView);
                    gridXetNghiem.gridView.BestFitColumns(true);
                    gridXetNghiem.gridView.RowStyle += gridXetNghiem_RowStyle;

                    if (dt.Columns.Contains("TEN_TT37") && !isDisplayColumnTt37)
                    {
                        //$("#grdXetNghiem").jqGrid('hideCol', 'TEN_TT37');
                        gridXetNghiem.gridView.Columns[10].VisibleIndex = -1;
                    }
                    if (hideColCk)
                    {
                        //$("#grdXetNghiem").jqGrid('hideCol', 'CHUYENKHOAID');
                        gridXetNghiem.gridView.Columns[11].VisibleIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setGridCDHA(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    ResponsList ds = new ResponsList();
                    //if (gridCDHA.ReLoadWhenFilter)
                    //{
                    //    if (gridCDHA.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridCDHA.tableFlterColumn);
                    //}

                    // Load dữ liệu tab xét nghiệm
                    ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D009.EV001", page, gridCDHA.ucPage1.getNumberPerPage(),
                        new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]" }, valueGrdCDHA);
                    gridCDHA.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "CHUYENKHOAID", "MADICHVU", "TENDICHVU", "TEN_TT37", "GIABHYT", "GIANHANDAN", "GIADICHVU" });
                    gridCDHA.setData(dt, ds.total, ds.page, ds.records);
                    gridCDHA.setColumnAll(false);
                    setPropColumnGrid(gridCDHA.gridView);
                    gridCDHA.gridView.BestFitColumns(true);

                    if (dt.Columns.Contains("TEN_TT37") && !isDisplayColumnTt37)
                    {
                        //$("#grdXetNghiem").jqGrid('hideCol', 'TEN_TT37');
                        gridCDHA.gridView.Columns[10].VisibleIndex = -1;
                    }
                    if (hideColCk)
                    {
                        //$("#grdXetNghiem").jqGrid('hideCol', 'CHUYENKHOAID');
                        gridCDHA.gridView.Columns[11].VisibleIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setGridPTTT(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    ResponsList ds = new ResponsList();
                    //if (gridPTTT.ReLoadWhenFilter)
                    //{
                    //    if (gridPTTT.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridPTTT.tableFlterColumn);
                    //}
                    // Load dữ liệu tab PTTT
                    ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D009.EV001", page, gridPTTT.ucPage1.getNumberPerPage(),
                        new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]" }, valueGrdPTTT);
                    gridPTTT.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "CHUYENKHOAID", "MADICHVU", "TENDICHVU", "TEN_TT37", "GIABHYT", "GIANHANDAN", "GIADICHVU" });
                    gridPTTT.setData(dt, ds.total, ds.page, ds.records);
                    gridPTTT.setColumnAll(false);
                    setPropColumnGrid(gridPTTT.gridView);
                    gridPTTT.gridView.BestFitColumns(true);

                    if (dt.Columns.Contains("TEN_TT37") && !isDisplayColumnTt37)
                    {
                        gridPTTT.gridView.Columns[10].VisibleIndex = -1;
                    }
                    if (hideColCk)
                    {
                        gridCDHA.gridView.Columns[11].VisibleIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setGridDSCDOld(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    string loaiDv = (!string.IsNullOrEmpty(opt.CHIDINHDICHVU)) ? opt.LOAIDICHVU : "-1";
                    //if (gridDSCDOld.ReLoadWhenFilter)
                    //{
                    //    if (gridDSCDOld.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridDSCDOld.tableFlterColumn);
                    //}
                    ResponsList ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D009.EV009", page, gridDSCDOld.ucPage1.getNumberPerPage(),
                        new String[] { "[0]", "[1]" }, new String[] { opt.TIEPNHANID, loaiDv });
                    gridDSCDOld.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "DICHVUID", "TENDICHVU", "PHONGCHIDINH" });
                    gridDSCDOld.setData(dt, ds.total, ds.page, ds.records);
                    gridDSCDOld.setColumnAll(false);
                    gridDSCDOld.setColumn("DICHVUID", -1, "");
                    gridDSCDOld.setColumn("TENDICHVU", 0, "Tên dịch vụ");
                    gridDSCDOld.setColumn("PHONGCHIDINH", 1, "Phòng chỉ định");
                    gridDSCDOld.gridView.BestFitColumns(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setGridDSCD(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                DataTable dt = new DataTable();
                if (page > 0)
                {
                    ResponsList ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D009.EV002", page, 100,
                        new String[] { "[0]", "[1]" }, new String[] { opt.MAUBENHPHAMID, opt.KHAMBENHID });

                    dt = MyJsonConvert.toDataTable(ds.rows);
                    //Add thêm một số cột ở grid định nghĩa không có trong dữ liệu trả về
                    dt.Columns.Add("HEN");
                    dt.Columns.Add("TYLEDV");
                    dt.Columns.Add("LOAITT_CU");
                    dt.Columns.Add("GIA_DVC");
                    dt.Columns.Add("LOAITT_MOI");
                    dt.Columns.Add("MANHOM_BHYT");
                    dt.Columns.Add("DICHVUCHINH");
                    dt.Columns.Add("DICHVUDIKEM");
                    dt.Columns.Add("HAS_ORDER");
                    dt.Columns.Add("TACHPHIEU");
                    if (dt.Rows.Count > 0)
                    {
                        gridDSCD.DataSource = dt;
                    }
                }

                //$("#grdDSCD").bind("jqGridAfterGridComplete")
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (opt.DOITUONGBENHNHANID == "1" && (dr["LOAIDOITUONG"].ToString() == "1" || dr["LOAIDOITUONG"].ToString() == "2"))
                    {
                        if (dr["LOAIDOITUONG"].ToString() == "1" || dr["LOAIDOITUONG"].ToString() == "2")
                        {
                            if (tongbh > (string.IsNullOrEmpty(hidTRAN_BHYT) ? float.Parse(hidTRAN_BHYT) : 0))
                            {
                                gridView4.SetRowCellValue(i, "BHYT_TRA", float.Parse(dr["BHYT_TRAFINAL"].ToString()).ToString("N2", culture));
                                gridView4.SetRowCellValue(i, "THANH_TIEN", float.Parse(dr["THANH_TIENFINAL"].ToString()).ToString("N2", culture));
                                gridView4.SetRowCellValue(i, "MIENGIAM", float.Parse(dr["MIENGIAMFINAL"].ToString()).ToString("N2", culture));
                            }
                            else
                            {
                                gridView4.SetRowCellValue(i, "BHYT_TRA", float.Parse(dr["BHYT_TRAFULL"].ToString()).ToString("N2", culture));
                                gridView4.SetRowCellValue(i, "THANH_TIEN", float.Parse(dr["THANH_TIENFULL"].ToString()).ToString("N2", culture));
                                gridView4.SetRowCellValue(i, "MIENGIAM", float.Parse(dr["MIENGIAMFULL"].ToString()).ToString("N2", culture));
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                    {
                        // dtOption : sắp sếp lại thứ tự hiển thị của cboPHONG_TH
                        DataTable dtOption = new DataTable();
                        if (!string.IsNullOrEmpty(dr["PHONG_TH"].ToString()))
                        {
                            DataTable data_ar = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV003", dr["PHONG_TH"].ToString(), 0);
                            if (data_ar.Rows.Count > 0)
                            {
                                dtOption = data_ar.Clone();
                                bool checkExistDept = false;
                                bool checkEqualDept = false;
                                // Vòng for để sắp xếp thứ tự hiển thị và giá trị hiển thị của cboPHONG_TH trên mỗi row
                                for (int i1 = 0; i1 < data_ar.Rows.Count; i1++)
                                {
                                    if (data_ar.Rows[i1]["PHONGID"].ToString() == opt.SUBDEPTID_LOGIN)
                                    {
                                        checkExistDept = true;
                                    }
                                    if (data_ar.Rows[i1]["PHONGID"].ToString() != dr["PHONG_TH1"].ToString())
                                    {
                                        // Insert lần lượt vào cuối
                                        DataRow drOption = dtOption.NewRow();
                                        drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                        dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                                    }
                                    else
                                    {
                                        // Nếu = giá trị PHONG_TH từ phiếu mẫu -> đưa giá trị đó hiển thị đầu tiên
                                        checkEqualDept = true;
                                        DataRow drOption = dtOption.NewRow();
                                        drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                        dtOption.Rows.InsertAt(drOption, 0);
                                    }
                                }

                                if (!checkExistDept && dr["LOAINHOMDICHVU"].ToString() == "5")
                                {
                                    if (!checkEqualDept)
                                    {
                                        DataRow drOption = dtOption.NewRow();
                                        drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                                        drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                                        dtOption.Rows.InsertAt(drOption, 0);
                                    }
                                    else
                                    {
                                        DataRow drOption = dtOption.NewRow();
                                        drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                                        drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                                        dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(dr["PHONG_TH1"].ToString()))
                            {
                                DataTable data_ar = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV003", dr["PHONG_TH1"].ToString(), 0);
                                if (data_ar.Rows.Count > 0)
                                {
                                    dtOption = data_ar.Clone();
                                    DataRow drOption = dtOption.NewRow();
                                    drOption.ItemArray = data_ar.Rows[0].ItemArray;
                                    dtOption.Rows.InsertAt(drOption, 0);
                                }
                            }
                        }
                        // set cboPHONG_TH
                        RepositoryItemLookUpEdit cboPHONG_TH = new RepositoryItemLookUpEdit();
                        cboPHONG_TH.DataSource = dtOption;
                        cboPHONG_TH.DisplayMember = "PHONGNAME";
                        cboPHONG_TH.ValueMember = "PHONGID";
                        cboPHONG_TH.ShowHeader = false;
                        cboPHONG_TH.ShowFooter = false;
                        cboPHONG_TH.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PHONGNAME", 0, ""));
                        cboPHONG_TH.AutoSearchColumnIndex = 1;
                        gridView4.Columns["PHONG_TH"].ColumnEdit = cboPHONG_TH;
                        if (dr["PHONG_TH"].ToString().Contains(";"))
                            gridView4.SetRowCellValue(i, "PHONG_TH", dtOption.Rows[0]["PHONGID"].ToString());

                        if (book)
                        {
                            if (dr.Table.Columns.Contains("PHIEUHEN") && dr["PHIEUHEN"].ToString() == "1")
                                gridView4.SetRowCellValue(i, "HEN", "1");
                        }
                    }
                    else
                    {
                        if (opt.LOAIDICHVU == "13")
                        {
                            DataTable dtTyle = new DataTable();
                            dtTyle.Columns.Add("name");
                            dtTyle.Columns.Add("value");
                            dtTyle.Rows.Add("100%", "1");
                            dtTyle.Rows.Add("70%", "0.7");
                            dtTyle.Rows.Add("50%", "0.5");
                            dtTyle.Rows.Add("30%", "0.3");
                            RepositoryItemLookUpEdit cboTYLEDV = new RepositoryItemLookUpEdit();
                            cboTYLEDV.DataSource = dt;
                            cboTYLEDV.DisplayMember = "name";
                            cboTYLEDV.ValueMember = "value";
                            cboTYLEDV.ShowHeader = false;
                            cboTYLEDV.ShowFooter = false;
                            cboTYLEDV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", 0, ""));
                            cboTYLEDV.AutoSearchColumnIndex = 1;
                            cboTYLEDV.EditValueChanged += cboTYLEDV_EditValueChanged;
                            gridView4.Columns["TYLEDV"].ColumnEdit = cboTYLEDV;

                            // Set dữ liệu cho cboTYLEDV
                            if (dt.Columns.Contains("TYLEDVTEMP") && !string.IsNullOrEmpty(dr["TYLEDVTEMP"].ToString()))
                            {
                                if (dr["TYLEDVTEMP"].ToString().IndexOf("0.3") != -1)
                                {
                                    gridView4.SetRowCellValue(i, "TYLEDV", "0.3");
                                }
                                else if (dr["TYLEDVTEMP"].ToString().IndexOf("0.5") != -1)
                                {
                                    gridView4.SetRowCellValue(i, "TYLEDV", "0.5");
                                }
                                else if (dr["TYLEDVTEMP"].ToString().IndexOf("0.7") != -1)
                                {
                                    gridView4.SetRowCellValue(i, "TYLEDV", "0.7");
                                }
                                else
                                {
                                    gridView4.SetRowCellValue(i, "TYLEDV", "1");
                                }
                            }
                            else
                            {
                                gridView4.SetRowCellValue(i, "TYLEDV", "1");
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setGridDichVuKhac(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                //string jsonFilter = "";
                //if (gridDichVuKhac.ReLoadWhenFilter)
                //{
                //    if (gridDichVuKhac.tableFlterColumn.Rows.Count > 0)
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridDSCDOld.tableFlterColumn);
                //}
                ResponsList ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU01H003.EV001", page, gridDichVuKhac.ucPage1.getNumberPerPage(),
                    new String[] { "[0]", "[1]", "[2]" }, new String[] { opt.LOAIDICHVU, opt.KHAMBENHID, txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1) });
                gridDichVuKhac.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(ds.rows);
                if (dt.Rows.Count == 0)
                    dt = Func.getTableEmpty(new String[] { "CHUYENKHOAID", "MADICHVU", "TENDICHVU", "TEN_TT37", "GIABHYT", "GIANHANDAN", "GIADICHVU" });
                gridDichVuKhac.setData(dt, ds.total, ds.page, ds.records);
                gridDichVuKhac.setColumnAll(false);
                setPropColumnGrid(gridDichVuKhac.gridView);
                gridDichVuKhac.gridView.BestFitColumns(true);

                if (dt.Columns.Contains("TEN_TT37") && !isDisplayColumnTt37)
                {
                    //hideCol TEN_TT37
                    gridDichVuKhac.gridView.Columns[3].VisibleIndex = -1;
                }
            }
        }

        private void gridXetNghiem_RowStyle(object sender, RowStyleEventArgs e)
        {
            for (int i = 0; i < gridXetNghiem.gridView.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)gridXetNghiem.gridView.GetRow(i);
                if (float.Parse(rowData["GIABHYT"].ToString()) <= 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                    //e.Appearance.BackColor = Color.LightGreen;
                    e.HighPriority = true;
                }
            }
        }

        private void setPropColumnGrid(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            try
            {
                // Các cột nhìn thấy
                setColumn(gridView, "CHUYENKHOAID", 0, "Mã CK", 50);
                setColumn(gridView, "MADICHVU", 1, "Mã DV", 100);
                setColumn(gridView, "TENDICHVU", 2, "Tên DV", 250);
                setColumn(gridView, "TEN_TT37", 3, "Tên TT37", 0);
                setColumn(gridView, "GIABHYT", 4, "Giá BH", 80);
                setColumn(gridView, "GIANHANDAN", 5, "Giá ND", 80);
                setColumn(gridView, "GIADICHVU", 6, "Giá YC", 80);
                // Các cột ẩn
                //setColumn(gridView, "NHOMDICHVUID", -1, "MANHOM", 0);
                //setColumn(gridView, "TENNHOM", -1, "Tên nhóm", 0);
                //setColumn(gridView, "DICHVUID", -1, "DICHVUID", 0);
                //setColumn(gridView, "ORG_NAME", -1, "Phòng thực hiện", 0);
                //setColumn(gridView, "PHONGID", -1, "Phòng ID,", 0);
                //setColumn(gridView, "LOAINHOMDICHVU", -1, "Loại", 0);
                //setColumn(gridView, "NHOM_MABHYT_ID", -1, "Loại nhóm BHYT", 0);
                //setColumn(gridView, "DICHVU_BHYT_DINHMUC", -1, "DICHVU_BHYT_DINHMUC", 0);
                //setColumn(gridView, "GIA_DVC", -1, "GIA_DVC", 0);
                //setColumn(gridView, "MANHOM_BHYT", -1, "NHOM MABHYT", 0);
                //setColumn(gridView, "DONVI", -1, "DONVI", 0);
                //setColumn(gridView, "KHOANMUCID", -1, "KHOANMUCID", 0);
                //setColumn(gridView, "LDV", -1, "LOAIDV", 0);
                //setColumn(gridView, "DICHVUCHINH", -1, "DICHVUCHINH", 0);
                //setColumn(gridView, "DICHVUDIKEM", -1, "DICHVUDIKEM", 0);
                //setColumn(gridView, "MAGIUONG", -1, "MAGIUONG", 0);
                //setColumn(gridView, "LOAIGIUONGID", -1, "LOAIGIUONGID", 0);

                // tạo group dữ liệu theo TENNHOM
                try
                {
                    gridView.Columns["TENNHOM"].Group();
                }
                catch (Exception ex)
                { }
                
                gridView.OptionsView.ShowGroupedColumns = true;
                gridView.OptionsView.ShowGroupPanel = false;
                if (isColspan)
                    gridView.OptionsBehavior.AutoExpandAllGroups = false;
                else
                    gridView.OptionsBehavior.AutoExpandAllGroups = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        public void setColumn(DevExpress.XtraGrid.Views.Grid.GridView gridView, string columnName, int index, string caption, int width)
        {
            DevExpress.XtraGrid.Columns.GridColumn col = gridView.Columns[columnName];
            if (col != null)
            {
                col.Caption = caption;
                col.VisibleIndex = index;
                if (width > 0) col.Width = width;
            }
        }
        #endregion

        #region EVENT PHÍM TẮT F6,F7,F8, COMBOBOX ONCHANGE, CHECKED CHANGE ...
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                tabPane1.SelectedPage = tabNavigationPage1;
                return true;
            }
            else if (keyData == Keys.F7)
            {
                tabPane1.SelectedPage = tabNavigationPage2;
                return true;
            }
            else if (keyData == Keys.F8)
            {
                tabPane1.SelectedPage = tabNavigationPage3;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cboNHOMXETNGHIEMID_OnChange(object sender, EventArgs e)
        {
            string loadPlService = "0";
            if (plDv)
            {
                loadPlService = chkAllService.Checked ? "0" : "1";
            }
            else
            {
                loadPlService = "0";
            }
            valueGrdXN = new string[] { opt.SUBDEPTID, "3", opt.DEPTID, cboNHOMXETNGHIEMID.SelectedValue ?? "-1", opt.KHAMBENHID, txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1), loadPlService };
            setGridXetNghiem(1, null);
        }

        private void cboNHOMCDHAID_OnChange(object sender, EventArgs e)
        {
            string loadPlService = "0";
            if (plDv)
            {
                loadPlService = chkAllService.Checked ? "0" : "1";
            }
            else
            {
                loadPlService = "0";
            }
            valueGrdCDHA = new string[] { opt.SUBDEPTID, "4", opt.DEPTID, cboNHOMCDHAID.SelectedValue ?? "-1", opt.KHAMBENHID, txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1), loadPlService };
            setGridCDHA(1, null);
        }

        private void cboNHOMPTTTID_OnChange(object sender, EventArgs e)
        {
            string loadPlService = "0";
            if (plDv)
            {
                loadPlService = chkAllService.Checked ? "0" : "1";
            }
            else
            {
                loadPlService = "0";
            }
            valueGrdPTTT = new string[] { opt.SUBDEPTID, "5", opt.DEPTID, cboCHUYENKHOAID.SelectedValue ?? "-1", opt.KHAMBENHID, txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1), loadPlService };
            setGridPTTT(1, null);
        }

        private void cboMAUBENHPHAMID_OnChange(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr = (DataRowView)sender;
                if (dr != null && !string.IsNullOrEmpty(dr["col1"].ToString()))
                {
                    DataTable dt = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV008", dr["col1"].ToString(), 0);
                    //{"result": "[{\"MAUBENHPHAMID\": \"263375\",\"TKCHANDOAN\": \"A00.1\",\"CHANDOAN_KT\": \"A01.3-Bệnh phó thương hàn C\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
                    if (dt.Rows.Count > 0)
                    {
                        // setObjectToForm
                        cboChanDoan.searchLookUpEdit.EditValue = dt.Rows[0]["TKCHANDOAN"].ToString();
                        cboChanDoanKT.SelectedText = dt.Rows[0]["CHANDOAN_KT"].ToString();
                    }
                }
                else
                {
                    cboChanDoan.SelectedValue = icdchinh;
                    cboChanDoanKT.SelectedText = icdphu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void cboLOAIGIUONG_OnChange(object sender, EventArgs e)
        {
            try
            {
                DataTable dtMaGiuong = ServiceChiDinhDichVu.ajaxExecuteQuery("NTU01H032.EV007", new String[] { "[0]", "[1]" },
                    new String[] { hidKHOABNID, cboLOAIGIUONG.SelectValue });
                DataRow drMaGiuong = dtMaGiuong.NewRow();
                drMaGiuong[0] = "";
                drMaGiuong[1] = "Chọn giường";
                dtMaGiuong.Rows.InsertAt(drMaGiuong, 0);
                cboMAGIUONG.setData(dtMaGiuong, "col1", "col2");
                cboMAGIUONG.setColumn("col1", -1, "", 0);
                cboMAGIUONG.setColumn("col2", 0, "", 0);
                if (!string.IsNullOrEmpty(giuongdv))
                {
                    cboMAGIUONG.SelectValue = giuongdv;
                }
                else
                {
                    cboMAGIUONG.SelectIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void cboKHOACHIDINHCHID_OnChange(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPhongCd = ServiceChiDinhDichVu.ajaxExecuteQuery("COM.PHONGKHOA", new String[] { "[0]", "[1]", "[2]" },
                        new String[] { "-1", "5", cboKHOACHIDINHCHID.SelectValue });
                DataRow drPhongCd = dtPhongCd.NewRow();
                drPhongCd[0] = "";
                drPhongCd[1] = "---Chọn---";
                dtPhongCd.Rows.InsertAt(drPhongCd, 0);
                cboPHONGCHIDINHCHID.setData(dtPhongCd, "col1", "col2");
                cboPHONGCHIDINHCHID.setColumn("col1", -1, "", 0);
                cboPHONGCHIDINHCHID.setColumn("col2", 0, "", 0);
                cboPHONGCHIDINHCHID.SelectIndex = 0;

                DataTable dtBacSi = ServiceChiDinhDichVu.ajaxExecuteQuery("NGT02K016.EV002", new String[] { "[0]" }, new String[] { cboKHOACHIDINHCHID.SelectValue });
                DataRow drBacSi = dtBacSi.NewRow();
                drBacSi[0] = "";
                drBacSi[1] = "---Chọn---";
                dtBacSi.Rows.InsertAt(drBacSi, 0);
                cboBACSIKHOACDID.setData(dtBacSi, "col1", "col2");
                cboBACSIKHOACDID.setColumn("col1", -1, "", 0);
                cboBACSIKHOACDID.setColumn("col2", 0, "", 0);
                cboBACSIKHOACDID.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void cboTYLEDV_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                changeTYLEDV();
                DataRow rowData = gridView4.GetFocusedDataRow();
                gridView4.SetFocusedRowCellValue("TYLEDVTEMP", rowData["TYLEDV"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void chkAllService_CheckedChanged(object sender, EventArgs e)
        {
            setDataToGridDvCLS();
        }

        private void cboChanDoan_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (cboChanDoanKT.SelectList.ContainsKey(drv["ICD10CODE"].ToString()))
                {
                    cboChanDoan.SelectedText = "";
                    cboChanDoan.messageError = "Bệnh chính trùng bệnh phụ";
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void cboChanDoanKT_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (drv["ICD10CODE"].ToString() == cboChanDoan.SelectedValue)
                {
                    cboChanDoanKT.searchLookUpEdit.Text = "";
                    cboChanDoanKT.messageError = "Bệnh phụ trùng bệnh chính";
                    return;
                }
                if (cboChanDoanKT.SelectList.ContainsKey(drv["ICD10CODE"].ToString()))
                {
                    cboChanDoanKT.searchLookUpEdit.Text = "";
                    cboChanDoanKT.messageError = "Bệnh phụ trùng";
                    return;
                }
                cboChanDoanKT.searchLookUpEdit.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion

        #region XỬ LÝ CÁC SỰ KIỆN GRID_ONSELECTROW
        Dictionary<string, DataTable> comboRitems = new Dictionary<string, DataTable>();
        private void OnSelectRow(GridView gridview, int id, bool selected, bool template = false, string templateid = "", string fullTemp = "")
        {
            try
            {
                int rowCount = gridView4.RowCount;
                float _insurance = 0; // tỉ lệ bảo hiểm của bn hiện tại
                float _insurance_full = 0; // tỉ lệ hưởng dưới trần - dưới trần: 100% tỉ lệ thẻ, trên trần: đúng với tỉ lệ thẻ
                float _insurance_final = 0; // tỉ lệ thật trên thẻ
                DataTable dataArr = new DataTable();
                string dichVuId = null;
                if (!string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                {
                    // Dịch vụ khác
                    DataRowView drv = (DataRowView)gridview.GetRow(id);
                    dataArr = drv.Row.Table.Clone();
                    dichVuId = drv["DICHVUID"].ToString();
                    dataArr.ImportRow(drv.Row);
                }
                else
                {
                    // Dịch vụ CLS
                    DataRowView drv = (DataRowView)gridview.GetRow(id);
                    dichVuId = drv["DICHVUID"].ToString();
                    string paramArr = "";
                    if (template) // Chọn từ phiếu mẫu
                    {
                        if (fullTemp == "1")
                        {
                            paramArr = opt.SUBDEPTID + "$" + "3" + "$" + opt.DEPTID + "$" + "-1" + "$" + opt.KHAMBENHID + "$" +
                                txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1) + "$" + templateid + "$" + "3";
                        }
                        else if (fullTemp == "2")
                        {
                            paramArr = opt.SUBDEPTID + "$" + "3" + "$" + opt.DEPTID + "$" + "-1" + "$" + opt.KHAMBENHID + "$" +
                                txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1) + "$" + templateid + "$" + "4";
                        }
                        else
                        {
                            paramArr = opt.SUBDEPTID + "$" + "3" + "$" + opt.DEPTID + "$" + "-1" + "$" + opt.KHAMBENHID + "$" +
                                txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1) + "$" + templateid + "$" + "1";
                        }
                        dataArr = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV0010", paramArr, 0);
                        if (dataArr.Rows.Count == 0)
                        {
                            return;
                        }
                    }
                    else // chọn từ grid
                    {
                        if (drv.DataView.Table.Columns.Contains("LDV") && drv["LDV"].ToString() == "1")
                        {
                            paramArr = opt.SUBDEPTID + "$" + "3" + "$" + opt.DEPTID + "$" + "-1" + "$" + opt.KHAMBENHID + "$" +
                                txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1) + "$" + dichVuId + "$" + "0";
                        }
                        else
                        {
                            paramArr = opt.SUBDEPTID + "$" + "3" + "$" + opt.DEPTID + "$" + "-1" + "$" + opt.KHAMBENHID + "$" +
                                txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1) + "$" + dichVuId + "$" + "2";
                        }
                        dataArr = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV0010", paramArr, 0);
                        //dataArr = ServiceChiDinhDichVu.getThongTinDichVuOnClick(paramArr);
                        if (dataArr.Rows.Count == 0)
                        {
                            MessageBox.Show("Chưa cấu hình phòng thực hiện cho dịch vụ");
                            gridview.UnselectRow(id);
                            return;
                        }
                    }

                    if (isPhongcd)
                    {
                        if (dataArr.Rows.Count > 0)
                        {
                            for (int i5 = 0; i5 < dataArr.Rows.Count; i5++)
                            {
                                if (!string.IsNullOrEmpty(dataArr.Rows[i5]["PHONGID"].ToString()))
                                {
                                    DataTable results = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV016", dataArr.Rows[i5]["DICHVUID"].ToString() + "$" + opt.SUBDEPTID, 0);
                                    if (results.Rows.Count > 0)
                                    {
                                        List<string> strArrTmp = new List<string>();
                                        string[] strArr = dataArr.Rows[i5]["PHONGID"].ToString().Split(';');
                                        for (int i6 = 0; i6 < strArr.Length; i6++)
                                        {
                                            bool check = false;
                                            for (int i7 = 0; i7 < results.Rows.Count; i7++)
                                            {
                                                if (strArr[i6] == results.Rows[i7]["PHONGTHUCHIENID"].ToString())
                                                {
                                                    check = true;
                                                }
                                            }
                                            if (check)
                                            {
                                                strArrTmp.Add(strArr[i6]);
                                            }
                                        }
                                        if (strArrTmp.Count > 0)
                                        {
                                            for (int i8 = 0; i8 < strArrTmp.Count; i8++)
                                            {
                                                dataArr.Rows[i5]["PHONGID"] += strArrTmp[i8] + ";";
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Chưa cấu hình phòng thực hiện cho dịch vụ");
                                            gridview.UnselectRow(id);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Chưa cấu hình phòng thực hiện cho dịch vụ");
                                        gridview.UnselectRow(id);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }

                if (selected)
                {
                    DataTable dataArrSub = dataArr.Clone();
                    int rowIdsSub = gridView4.DataRowCount;
                    for (int n2 = 0; n2 < dataArr.Rows.Count; n2++)
                    {
                        bool checkSub = false;
                        for (int n1 = 0; n1 < rowIdsSub; n1++)
                        {
                            DataRowView rowDataSub = (DataRowView)gridView4.GetRow(n1);
                            if (rowDataSub["DICHVUID"].ToString() == dataArr.Rows[n2]["DICHVUID"].ToString())
                            {
                                checkSub = true;
                                break;
                            }
                        }

                        if (!checkSub)
                        {
                            dataArrSub.ImportRow(dataArr.Rows[n2]);
                            if (isShowNumService)
                            {
                                int soLuongDv = int.Parse(txtSOLUONGDICHVU.Text.Trim());
                                for (int n3 = 0; n3 < soLuongDv - 1; n3++)
                                {
                                    dataArrSub.ImportRow(dataArr.Rows[n2]);
                                }
                            }
                        }
                    }
                    dataArr = dataArrSub;
                }
                if (selected && string.IsNullOrEmpty(opt.CHIDINHDICHVU) && isCbPd && !string.IsNullOrEmpty(cboChanDoan.SelectedValue))
                {
                    string dichvuidLst = "";
                    for (int n4 = 0; n4 < dataArr.Rows.Count; n4++)
                    {
                        if (n4 == 0)
                        {
                            dichvuidLst = dataArr.Rows[n4]["DICHVUID"].ToString();
                        }
                        else
                        {
                            dichvuidLst = dichvuidLst + "," + dataArr.Rows[n4]["DICHVUID"].ToString();
                        }
                    }
                    string paramArrDvPd = dichvuidLst + "$" + cboChanDoan.SelectedValue + "$" + "1";
                    string resultDvPd = ServiceChiDinhDichVu.ajaxCALL_SP_S("NTU02D009.EV013", paramArrDvPd);
                    if (!string.IsNullOrEmpty(resultDvPd))
                    {
                        MessageBox.Show("Dịch vụ " + resultDvPd + " không tồn tại trong phác đồ điều trị mã bệnh " + cboChanDoan.SelectedValue);
                        gridview.SelectRow(id);
                    }
                }

                for (var i = 0; i < dataArr.Rows.Count; i++)
                {
                    DataRow ret = dataArr.Rows[i];
                    if (opt.DOITUONGBENHNHANID == "1" && float.Parse(ret["GIABHYT"].ToString()) > 0)
                    {
                        if (selected)
                        {
                            tongbh = tongbh + float.Parse(ret["GIABHYT"].ToString()) * float.Parse(ret["GIABHYT"].ToString() ?? "1");
                            if (tongbh > float.Parse(hidTRAN_BHYT ?? "0"))
                            {
                                _insurance = !string.IsNullOrEmpty(hidQUYENLOITHE) ? float.Parse(hidQUYENLOITHE) : float.Parse(txtQUYENLOI.Text);
                            }
                            else
                            {
                                _insurance = !string.IsNullOrEmpty(hidQUYENLOI) ? float.Parse(hidQUYENLOI) : float.Parse(txtQUYENLOI.Text);
                            }
                            _insurance_full = !string.IsNullOrEmpty(hidQUYENLOI) ? float.Parse(hidQUYENLOI) : float.Parse(txtQUYENLOI.Text);
                            _insurance_final = !string.IsNullOrEmpty(hidQUYENLOITHE) ? float.Parse(hidQUYENLOITHE) : float.Parse(txtQUYENLOI.Text);

                            if (rowCount != 0 && _insurance_bf != _insurance)
                            {
                                int rowIds = gridView4.DataRowCount;
                                for (int i1 = 0; i1 < rowIds; i1++)
                                {
                                    DataRowView rowData = (DataRowView)gridView4.GetRow(i1);
                                    if (rowData["LOAIDOITUONG"].ToString() == "1" || rowData["LOAIDOITUONG"].ToString() == "2")
                                    {
                                        if (tongbh > float.Parse(hidTRAN_BHYT ?? "0"))
                                        {
                                            gridView4.SetRowCellValue(i1, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFINAL"].ToString()).ToString("N2", culture));
                                            gridView4.SetRowCellValue(i1, "THANH_TIEN", float.Parse(rowData["THANH_TIENFINAL"].ToString()).ToString("N2", culture));
                                            gridView4.SetRowCellValue(i1, "MIENGIAM", float.Parse(rowData["MIENGIAMFINAL"].ToString()).ToString("N2", culture));
                                            LoadPay(0, float.Parse(rowData["BHYT_TRAFINAL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                        }
                                        else
                                        {
                                            gridView4.SetRowCellValue(i1, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFULL"].ToString()).ToString("N2", culture));
                                            gridView4.SetRowCellValue(i1, "THANH_TIEN", float.Parse(rowData["THANH_TIENFULL"].ToString()).ToString("N2", culture));
                                            gridView4.SetRowCellValue(i1, "MIENGIAM", float.Parse(rowData["MIENGIAMFULL"].ToString()).ToString("N2", culture));
                                            LoadPay(0, float.Parse(rowData["BHYT_TRAFULL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                        }
                                    }
                                }
                            }
                            _insurance_bf = _insurance;
                        }
                    }

                    float _payStart = 0;
                    float _payIns = 0;
                    float _payEnd = 0;
                    string _benhPhamGroup = "0";
                    if (selected)
                    {
                        int rowIds = gridView4.DataRowCount;
                        DataTable r = new DataTable();
                        DataTable r_full = new DataTable();
                        DataTable r_final = new DataTable();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("DOITUONGBENHNHANID", typeof(String));
                        dt.Columns.Add("MUCHUONG", typeof(String));
                        dt.Columns.Add("GIATRANBH", typeof(String));
                        dt.Columns.Add("GIABHYT", typeof(String));
                        dt.Columns.Add("GIAND", typeof(String));
                        dt.Columns.Add("GIADV", typeof(String));
                        dt.Columns.Add("GIANN", typeof(String));
                        dt.Columns.Add("DOITUONGCHUYEN", typeof(String));
                        dt.Columns.Add("GIADVKTC", typeof(String));
                        dt.Columns.Add("MANHOMBHYT", typeof(String));
                        dt.Columns.Add("SOLUONG", typeof(String));
                        dt.Columns.Add("CANTRENDVKTC", typeof(String));
                        dt.Columns.Add("THEDUTHOIGIAN", typeof(String));
                        dt.Columns.Add("DUOCVANCHUYEN", typeof(String));
                        dt.Columns.Add("TYLETHUOCVATTU", typeof(String));
                        dt.Columns.Add("NHOMDOITUONG", typeof(String));
                        dt.Columns.Add("NGAYHANTHE", typeof(String));
                        dt.Columns.Add("NGAYDICHVU", typeof(String));
                        dt.Columns.Add("TYLE_MIENGIAM", typeof(String));
                        DataRow dr = dt.NewRow();

                        if (opt.LOAIDICHVU == "19")
                        {
                            dr["DOITUONGBENHNHANID"] = "2";
                        }
                        else
                        {
                            dr["DOITUONGBENHNHANID"] = opt.DOITUONGBENHNHANID;
                        }

                        if (opt.LOAIDICHVU == "14" && isTranspost)
                        {
                            dr["MUCHUONG"] = hidTYLE_THEBHYT;
                        }
                        else
                        {
                            dr["MUCHUONG"] = _insurance;
                        }
                        dr["GIATRANBH"] = dataArr.Columns.Contains("DICHVU_BHYT_DINHMUC") ? ret["DICHVU_BHYT_DINHMUC"].ToString() : "";
                        dr["GIABHYT"] = ret["GIABHYT"].ToString();
                        dr["GIAND"] = ret["GIANHANDAN"].ToString();
                        dr["GIADV"] = ret["GIADICHVU"].ToString();
                        dr["GIANN"] = "0";
                        dr["DOITUONGCHUYEN"] = "0";
                        dr["GIADVKTC"] = (opt.DOITUONGBENHNHANID == "1" && dataArr.Columns.Contains("GIA_DVC") && float.Parse(ret["GIA_DVC"].ToString()) > 0)
                            ? ret["GIA_DVC"].ToString() : "0";
                        dr["MANHOMBHYT"] = ret["MANHOM_BHYT"].ToString();
                        dr["SOLUONG"] = dataArr.Columns.Contains("SOLUONG") ? ret["SOLUONG"].ToString() : "1";
                        dr["CANTRENDVKTC"] = hidCANTRENKTC;
                        dr["THEDUTHOIGIAN"] = hidBHFULL;
                        dr["DUOCVANCHUYEN"] = hidDUOCVC;
                        dr["TYLETHUOCVATTU"] = "100";
                        dr["NHOMDOITUONG"] = hidNHOMDOITUONG;
                        dr["NGAYHANTHE"] = hidNGAYHANTHE;
                        dr["NGAYDICHVU"] = !string.IsNullOrEmpty(txtTGCHIDINH.Text) ? txtTGCHIDINH.DateTime.ToString(Const.FORMAT_date1) : "";
                        dr["TYLE_MIENGIAM"] = hidTYLEMIENGIAM;
                        dt.Rows.InsertAt(dr, 0);
                        DataTable dtFull = dt.Copy();
                        DataTable dtFinal = dt.Copy();

                        // function vien phi
                        r = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dt);
                        DataRow r_dr = r.Rows[0];
                        if (r_dr["bh_tra"].ToString() == "-1" && r_dr["nd_tra"].ToString() == "-1" && r_dr["tong_cp"].ToString() == "-1")
                        {
                            MessageBox.Show("Giá tiền dịch vụ của bệnh nhân không thể bằng 0");
                            gridview.UnselectRow(id);
                            return;
                        }

                        DataRow drFull = dtFull.Rows[0];
                        drFull["MUCHUONG"] = _insurance_full;
                        r_full = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtFull);
                        DataRow r_full_dr = r_full.Rows[0];

                        DataRow drFinal = dtFinal.Rows[0];
                        if (opt.LOAIDICHVU == "14" && isTranspost)
                        {
                            drFinal["MUCHUONG"] = hidTYLE_THEBHYT;
                        }
                        else
                        {
                            drFinal["MUCHUONG"] = _insurance_final;
                        }
                        r_final = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtFinal);
                        DataRow r_final_dr = r_final.Rows[0];

                        _payStart = float.Parse(r_dr["tong_cp"].ToString());
                        _payIns = float.Parse(r_dr["bh_tra"].ToString());
                        _payEnd = float.Parse(r_dr["nd_tra"].ToString());
                        if (ret["LOAINHOMDICHVU"].ToString() == "3")
                        {
                            _benhPhamGroup = "1";
                        }
                        else
                        {
                            _benhPhamGroup = ret["LOAINHOMDICHVU"].ToString() == "4" ? "2" : "5";
                        }

                        DataTable dataRowAdd = new DataTable();
                        dataRowAdd.Columns.Add("NHOMDICHVUID");
                        dataRowAdd.Columns.Add("DICHVUID");
                        dataRowAdd.Columns.Add("TENDICHVU");
                        dataRowAdd.Columns.Add("SOLUONG");
                        dataRowAdd.Columns.Add("GIA_TIEN");
                        dataRowAdd.Columns.Add("BHYT_TRA");
                        dataRowAdd.Columns.Add("MIENGIAM");
                        dataRowAdd.Columns.Add("THANH_TIEN");
                        dataRowAdd.Columns.Add("LOAITT_CU");
                        dataRowAdd.Columns.Add("PHONG_TH");
                        dataRowAdd.Columns.Add("LOAINHOMDICHVU");
                        dataRowAdd.Columns.Add("NHOM_MABHYT_ID");
                        dataRowAdd.Columns.Add("GIABHYT");
                        dataRowAdd.Columns.Add("GIANHANDAN");
                        dataRowAdd.Columns.Add("GIADICHVU");
                        dataRowAdd.Columns.Add("LOAIDOITUONG");
                        dataRowAdd.Columns.Add("MAUBENHPHAMID");
                        dataRowAdd.Columns.Add("NHOMBENHPHAM");
                        dataRowAdd.Columns.Add("DICHVU_BHYT_DINHMUC");
                        dataRowAdd.Columns.Add("LOAI_DT_MOI");
                        dataRowAdd.Columns.Add("GIA_CHENH");
                        dataRowAdd.Columns.Add("LOAITT_MOI");
                        dataRowAdd.Columns.Add("GIA_DVC");
                        dataRowAdd.Columns.Add("PHONG_TH1");
                        dataRowAdd.Columns.Add("MANHOM_BHYT");
                        dataRowAdd.Columns.Add("DONVI");
                        dataRowAdd.Columns.Add("KHOANMUCID");
                        dataRowAdd.Columns.Add("BHYT_TRAFULL");
                        dataRowAdd.Columns.Add("MIENGIAMFULL");
                        dataRowAdd.Columns.Add("THANH_TIENFULL");
                        dataRowAdd.Columns.Add("BHYT_TRAFINAL");
                        dataRowAdd.Columns.Add("MIENGIAMFINAL");
                        dataRowAdd.Columns.Add("THANH_TIENFINAL");
                        dataRowAdd.Columns.Add("DICHVUCHINH");
                        dataRowAdd.Columns.Add("DICHVUDIKEM");
                        dataRowAdd.Columns.Add("OLDVALUE");
                        dataRowAdd.Columns.Add("TYLEDVTEMP");
                        dataRowAdd.Columns.Add("HEN");
                        dataRowAdd.Columns.Add("TYLEDV");
                        dataRowAdd.Columns.Add("HAS_ORDER");
                        dataRowAdd.Columns.Add("GHICHU");
                        dataRowAdd.Columns.Add("TONGTIEN");
                        dataRowAdd.Columns.Add("TACHPHIEU");
                        if (gridView4.DataRowCount == 0)
                            gridDSCD.DataSource = dataRowAdd;

                        gridView4.AddNewRow();
                        gridView4.SetFocusedRowCellValue("NHOMDICHVUID", ret["NHOMDICHVUID"].ToString());
                        gridView4.SetFocusedRowCellValue("DICHVUID", ret["DICHVUID"].ToString());
                        gridView4.SetFocusedRowCellValue("TENDICHVU", ret["TENDICHVU"].ToString());
                        gridView4.SetFocusedRowCellValue("SOLUONG", dataArr.Columns.Contains("SOLUONG") ? ret["SOLUONG"].ToString() : "1");
                        gridView4.SetFocusedRowCellValue("GIA_TIEN", (_payStart / float.Parse(dataArr.Columns.Contains("SOLUONG") ? ret["SOLUONG"].ToString() : "1")).ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("BHYT_TRA", _payIns.ToString("N2", culture) ?? "0");
                        gridView4.SetFocusedRowCellValue("MIENGIAM", !string.IsNullOrEmpty(r_dr["mien_giam"].ToString()) ? float.Parse(r_dr["mien_giam"].ToString()).ToString("N2", culture) : "0");
                        gridView4.SetFocusedRowCellValue("THANH_TIEN", _payEnd.ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("LOAITT_CU", r_dr["ten_loai_tt"].ToString());
                        gridView4.SetFocusedRowCellValue("PHONG_TH", dataArr.Columns.Contains("PHONGID") ? ret["PHONGID"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("LOAINHOMDICHVU", ret["LOAINHOMDICHVU"].ToString());
                        gridView4.SetFocusedRowCellValue("NHOM_MABHYT_ID", ret["NHOM_MABHYT_ID"].ToString());
                        gridView4.SetFocusedRowCellValue("GIABHYT", ret["GIABHYT"].ToString());
                        gridView4.SetFocusedRowCellValue("GIANHANDAN", ret["GIANHANDAN"].ToString());
                        gridView4.SetFocusedRowCellValue("GIADICHVU", ret["GIADICHVU"].ToString());
                        gridView4.SetFocusedRowCellValue("LOAIDOITUONG", r_dr["loai_dt"].ToString());
                        gridView4.SetFocusedRowCellValue("MAUBENHPHAMID", "");
                        gridView4.SetFocusedRowCellValue("NHOMBENHPHAM", _benhPhamGroup);
                        gridView4.SetFocusedRowCellValue("DICHVU_BHYT_DINHMUC", dataArr.Columns.Contains("DICHVU_BHYT_DINHMUC") ? ret["DICHVU_BHYT_DINHMUC"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("LOAI_DT_MOI", "");
                        gridView4.SetFocusedRowCellValue("GIA_CHENH", "0");
                        gridView4.SetFocusedRowCellValue("LOAITT_MOI", "");
                        gridView4.SetFocusedRowCellValue("GIA_DVC", dataArr.Columns.Contains("GIA_DVC") ? ret["GIA_DVC"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("PHONG_TH1", "");
                        gridView4.SetFocusedRowCellValue("MANHOM_BHYT", ret["MANHOM_BHYT"].ToString());
                        gridView4.SetFocusedRowCellValue("DONVI", ret["DONVI"].ToString());
                        gridView4.SetFocusedRowCellValue("KHOANMUCID", ret["KHOANMUCID"].ToString());
                        gridView4.SetFocusedRowCellValue("BHYT_TRAFULL", !string.IsNullOrEmpty(r_full_dr["bh_tra"].ToString()) ? r_full_dr["bh_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("MIENGIAMFULL", !string.IsNullOrEmpty(r_full_dr["mien_giam"].ToString()) ? r_full_dr["mien_giam"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("THANH_TIENFULL", !string.IsNullOrEmpty(r_full_dr["nd_tra"].ToString()) ? r_full_dr["nd_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("BHYT_TRAFINAL", !string.IsNullOrEmpty(r_final_dr["bh_tra"].ToString()) ? r_final_dr["bh_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("MIENGIAMFINAL", !string.IsNullOrEmpty(r_final_dr["mien_giam"].ToString()) ? r_final_dr["mien_giam"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("THANH_TIENFINAL", !string.IsNullOrEmpty(r_final_dr["nd_tra"].ToString()) ? r_final_dr["nd_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("DICHVUCHINH", dataArr.Columns.Contains("DICHVUCHINH") ? ret["DICHVUCHINH"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("DICHVUDIKEM", dataArr.Columns.Contains("DICHVUDIKEM") ? ret["DICHVUDIKEM"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("OLDVALUE", "1");
                        gridView4.SetFocusedRowCellValue("HAS_ORDER", dataArr.Columns.Contains("HAS_ORDER") ? ret["HAS_ORDER"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("GHICHU", dataArr.Columns.Contains("GHICHU") ? ret["GHICHU"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("TONGTIEN", _payEnd);
                        gridView4.SetFocusedRowCellValue("TACHPHIEU", dataArr.Columns.Contains("TACHPHIEU") ? ret["TACHPHIEU"].ToString() : "");

                        if (opt.LOAIDICHVU == "13" && isGiuongdv)
                        {
                            giuongdv = dataArr.Columns.Contains("MAGIUONG") ? ret["MAGIUONG"].ToString() : "";
                            cboLOAIGIUONG.SelectValue = ret["LOAIGIUONGID"].ToString();
                        }
                        gridView4.UpdateCurrentRow();

                        // lay tam tong chi phi va bao hiem trong luc cho doi cong thuc
                        LoadPay(_payStart, _payIns);

                        //Load checkbox HEN, cboPHONG_TH, cboTYLE_DV
                        if (string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                        {
                            #region load cboPHONG_TH
                            DataRowView rowData = (DataRowView)gridView4.GetRow(gridView4.DataRowCount-1);
                            // dtOption : sắp sếp lại thứ tự hiển thị của cboPHONG_TH
                            DataTable dtOption = new DataTable();
                            if (comboRitems.ContainsKey(rowData["DICHVUID"].ToString()))
                                dtOption = (DataTable)comboRitems[rowData["DICHVUID"].ToString()];
                            else
                            {
                                if (rowData != null && !string.IsNullOrEmpty(rowData["PHONG_TH"].ToString()))
                                {
                                    DataTable data_ar = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV003", rowData["PHONG_TH"].ToString(), 0);
                                    if (data_ar.Rows.Count > 0)
                                    {
                                        dtOption = data_ar.Clone();
                                        bool checkExistDept = false;
                                        bool checkEqualDept = false;
                                        // Vòng for để sắp xếp thứ tự hiển thị và giá trị hiển thị của cboPHONG_TH trên mỗi row
                                        for (int i1 = 0; i1 < data_ar.Rows.Count; i1++)
                                        {
                                            if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                                            {
                                                if (data_ar.Rows[i1]["PHONGID"].ToString() == opt.SUBDEPTID)
                                                {
                                                    checkExistDept = true;
                                                }
                                                if (data_ar.Rows[i1]["PHONGID"].ToString() != rowData["PHONG_TH1"].ToString())
                                                {
                                                    // Insert lần lượt vào cuối
                                                    DataRow drOption = dtOption.NewRow();
                                                    drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                                    dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                                                }
                                                else
                                                {
                                                    // Nếu = giá trị PHONG_TH từ phiếu mẫu -> đưa giá trị đó hiển thị đầu tiên
                                                    checkEqualDept = true;
                                                    DataRow drOption = dtOption.NewRow();
                                                    drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                                    dtOption.Rows.InsertAt(drOption, 0);
                                                }
                                            }
                                            else
                                            {
                                                if (data_ar.Rows[i1]["PHONGID"].ToString() == opt.SUBDEPTID_LOGIN)
                                                {
                                                    checkExistDept = true;
                                                    if (rowData["LOAINHOMDICHVU"].ToString() == "5")
                                                    {
                                                        DataRow drOption = dtOption.NewRow();
                                                        drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                                        dtOption.Rows.InsertAt(drOption, 0);
                                                    }
                                                    else
                                                    {
                                                        if (rowData["HAS_ORDER"].ToString() == "-1")
                                                        {
                                                            DataRow drOption = dtOption.NewRow();
                                                            drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                                            dtOption.Rows.InsertAt(drOption, 0);
                                                        }
                                                        else
                                                        {
                                                            DataRow drOption = dtOption.NewRow();
                                                            drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                                            dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    DataRow drOption = dtOption.NewRow();
                                                    drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                                                    dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                                                }
                                            }
                                        }

                                        if (!checkExistDept && rowData["LOAINHOMDICHVU"].ToString() == "5")
                                        {
                                            if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                                            {
                                                if (!checkEqualDept)
                                                {
                                                    DataRow drOption = dtOption.NewRow();
                                                    drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                                                    drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                                                    dtOption.Rows.InsertAt(drOption, 0);
                                                }
                                                else
                                                {
                                                    DataRow drOption = dtOption.NewRow();
                                                    drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                                                    drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                                                    dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                                                }
                                            }
                                            else
                                            {
                                                DataRow drOption = dtOption.NewRow();
                                                drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                                                drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                                                dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    dtOption.Columns.Add("PHONGID");
                                    dtOption.Columns.Add("PHONGNAME");
                                    DataRow drOption = dtOption.NewRow();
                                    drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                                    drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                                    dtOption.Rows.InsertAt(drOption, 0);
                                }
                                comboRitems.Add(rowData["DICHVUID"].ToString(), dtOption);
                            }

                            // set cboPHONG_TH
                            RepositoryItemLookUpEdit cboPHONG_TH = new RepositoryItemLookUpEdit();
                            cboPHONG_TH.DataSource = dtOption;
                            cboPHONG_TH.DisplayMember = "PHONGNAME";
                            cboPHONG_TH.ValueMember = "PHONGID";
                            cboPHONG_TH.ShowHeader = false;
                            cboPHONG_TH.ShowFooter = false;
                            cboPHONG_TH.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PHONGNAME", 0, ""));
                            cboPHONG_TH.AutoSearchColumnIndex = 1;
                            gridView4.Columns["PHONG_TH"].ColumnEdit = cboPHONG_TH;

                            if (rowData["PHONG_TH"].ToString().Contains(";"))
                                gridView4.SetFocusedRowCellValue("PHONG_TH", dtOption.Rows[0]["PHONGID"].ToString());
                            #endregion
                            #region load checkBox HEN
                            if (book)
                            {
                                if (bookAll)
                                {
                                    gridView4.SetFocusedRowCellValue("HEN", "1");
                                }
                            }
                            #endregion
                        }
                        else if(opt.LOAIDICHVU == "13")
                        {
                            #region load cbo_TYLEDV
                            DataTable dtTyle = new DataTable();
                            dtTyle.Columns.Add("name");
                            dtTyle.Columns.Add("value");
                            dtTyle.Rows.Add("100%", "1");
                            dtTyle.Rows.Add("70%", "0.7");
                            dtTyle.Rows.Add("50%", "0.5");
                            dtTyle.Rows.Add("30%", "0.3");
                            RepositoryItemLookUpEdit cboTYLEDV = new RepositoryItemLookUpEdit();
                            cboTYLEDV.DataSource = dt;
                            cboTYLEDV.DisplayMember = "name";
                            cboTYLEDV.ValueMember = "value";
                            cboTYLEDV.ShowHeader = false;
                            cboTYLEDV.ShowFooter = false;
                            cboTYLEDV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", 0, ""));
                            cboTYLEDV.AutoSearchColumnIndex = 1;
                            cboTYLEDV.EditValueChanged += cboTYLEDV_EditValueChanged;
                            gridView4.Columns["TYLEDV"].ColumnEdit = cboTYLEDV;
                            gridView4.SetFocusedRowCellValue("TYLEDV", "1");
                            #endregion
                        }
                    }
                    else
                    {
                        int rowIds = gridView4.DataRowCount;
                        for (int k = 0; k < rowIds; k++)
                        {
                            DataRowView rowData = (DataRowView)gridView4.GetRow(k);
                            if (rowData["DICHVUID"].ToString() == ret["DICHVUID"].ToString())
                            {
                                _payStart = float.Parse(rowData["GIA_TIEN"].ToString(), culture) * float.Parse(rowData["SOLUONG"].ToString());
                                _payIns = float.Parse(rowData["BHYT_TRA"].ToString(), culture);
                                dvIdReds.Remove(rowData["DICHVUID"].ToString());
                                gridView4.DeleteRow(k);
                                break;
                            }
                        }
                        LoadPay(-_payStart, -_payIns);
                        if (opt.DOITUONGBENHNHANID == "1" && _payIns > 0)
                        {
                            tongbh = tongbh - _payStart;
                            if (tongbh > float.Parse(hidTRAN_BHYT ?? "0"))
                            {
                                _insurance = !string.IsNullOrEmpty(hidQUYENLOITHE) ? float.Parse(hidQUYENLOITHE) : float.Parse(txtQUYENLOI.Text);
                            }
                            else
                            {
                                _insurance = !string.IsNullOrEmpty(hidQUYENLOI) ? float.Parse(hidQUYENLOI) : float.Parse(txtQUYENLOI.Text);
                            }
                            _insurance_bf = _insurance;

                            rowIds = gridView4.DataRowCount;
                            for (int i1 = 0; i1 < rowIds; i1++)
                            {
                                DataRowView rowData = (DataRowView)gridView4.GetRow(i1);
                                if (rowData["LOAIDOITUONG"].ToString() == "1" || rowData["LOAIDOITUONG"].ToString() == "2")
                                {
                                    if (tongbh > (string.IsNullOrEmpty(hidTRAN_BHYT) ? float.Parse(hidTRAN_BHYT) : 0))
                                    {
                                        gridView4.SetRowCellValue(i1, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFINAL"].ToString()).ToString("N2", culture));
                                        gridView4.SetRowCellValue(i1, "THANH_TIEN", float.Parse(rowData["THANH_TIENFINAL"].ToString()).ToString("N2", culture));
                                        gridView4.SetRowCellValue(i1, "MIENGIAM", float.Parse(rowData["MIENGIAMFINAL"].ToString()).ToString("N2", culture));
                                        LoadPay(0, float.Parse(rowData["BHYT_TRAFINAL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                    }
                                    else
                                    {
                                        gridView4.SetRowCellValue(i1, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFULL"].ToString()).ToString("N2", culture));
                                        gridView4.SetRowCellValue(i1, "THANH_TIEN", float.Parse(rowData["THANH_TIENFULL"].ToString()).ToString("N2", culture));
                                        gridView4.SetRowCellValue(i1, "MIENGIAM", float.Parse(rowData["MIENGIAMFULL"].ToString()).ToString("N2", culture));
                                        LoadPay(0, float.Parse(rowData["BHYT_TRAFULL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                    }
                                }
                            }
                        }
                    }
                }

                if (selected && isShowMsgWr && opt.LOAITIEPNHANID == "1" && tongbh > float.Parse(hidTRAN_BHYT ?? "0"))
                {
                    MessageBox.Show("Tổng tiền dịch vụ bảo hiểm vượt trần");
                    gridview.SelectRow(id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridView4_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            try
            {
                // HANV_NOTE @TODO : Phần này phải chuyển về 2 nơi -> onSelectRow và setGridDSCD chứ ko thể xét colum ngay trong hàm này được
                if (e.RowHandle < 0) return;
                DataRowView rowData = (DataRowView)gridView4.GetRow(e.RowHandle);

                #region edit SOLUONG
                if (rowData.Row != null && e.Column.FieldName == "SOLUONG" && !string.IsNullOrEmpty(rowData["SOLUONG"].ToString()))
                {
                    float value;
                    float oldValue = float.Parse(rowData["OLDVALUE"].ToString());
                    bool isFloat = float.TryParse(rowData["SOLUONG"].ToString(), out value);

                    if (string.IsNullOrEmpty(opt.CHIDINHDICHVU) && !isKeleCls)
                    {
                        if (isEditPttt && rowData["LOAINHOMDICHVU"].ToString() != "5")
                        {
                            gridView4.SetRowCellValue(e.RowHandle, "SOLUONG", oldValue.ToString());
                            return;
                        }
                    }

                    if (isFloat && value > 0 && value != oldValue)
                    {
                        if (!isNhapGiuongTp && value < 1 && value != 0.5)
                        {
                            gridView4.SetRowCellValue(e.RowHandle, "SOLUONG", oldValue.ToString());
                        }
                        else
                        {
                            if (opt.LOAIDICHVU == "13" && !isNhapGiuongTp && isKeGiuongTheoNgay && value != 1 && value != 0.5)
                            {
                                gridView4.SetRowCellValue(e.RowHandle, "SOLUONG", oldValue.ToString());
                            }
                            else
                            {
                                loadChange(oldValue, value, e.RowHandle);
                                gridView4.SetRowCellValue(e.RowHandle, "OLDVALUE", value.ToString());
                            }
                        }
                    }
                    else
                    {
                        gridView4.SetRowCellValue(e.RowHandle, "SOLUONG", oldValue.ToString());
                    }
                }
                #endregion

                //#region load cboPHONG_TH
                //if (rowData.Row != null && e.Column.FieldName == "PHONG_TH")
                //{
                //    if (string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                //    {
                //        // dtOption : sắp sếp lại thứ tự hiển thị của cboPHONG_TH
                //        DataTable dtOption = new DataTable();
                //        if (comboRitems.ContainsKey(rowData["DICHVUID"].ToString()))
                //            dtOption = (DataTable)comboRitems[rowData["DICHVUID"].ToString()];
                //        else
                //        {
                //            if (rowData != null && !string.IsNullOrEmpty(rowData["PHONG_TH"].ToString()))
                //            {
                //                DataTable data_ar = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D009.EV003", rowData["PHONG_TH"].ToString(), 0);
                //                if (data_ar.Rows.Count > 0)
                //                {
                //                    dtOption = data_ar.Clone();
                //                    bool checkExistDept = false;
                //                    bool checkEqualDept = false;
                //                    // Vòng for để sắp xếp thứ tự hiển thị và giá trị hiển thị của cboPHONG_TH trên mỗi row
                //                    for (int i1 = 0; i1 < data_ar.Rows.Count; i1++)
                //                    {
                //                        if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                //                        {
                //                            if (data_ar.Rows[i1]["PHONGID"].ToString() == opt.SUBDEPTID)
                //                            {
                //                                checkExistDept = true;
                //                            }
                //                            if (data_ar.Rows[i1]["PHONGID"].ToString() != rowData["PHONG_TH1"].ToString())
                //                            {
                //                                // Insert lần lượt vào cuối
                //                                DataRow drOption = dtOption.NewRow();
                //                                drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                //                                dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                //                            }
                //                            else
                //                            {
                //                                // Nếu = giá trị PHONG_TH từ phiếu mẫu -> đưa giá trị đó hiển thị đầu tiên
                //                                checkEqualDept = true;
                //                                DataRow drOption = dtOption.NewRow();
                //                                drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                //                                dtOption.Rows.InsertAt(drOption, 0);
                //                            }
                //                        }
                //                        else
                //                        {
                //                            if (data_ar.Rows[i1]["PHONGID"].ToString() == opt.SUBDEPTID_LOGIN)
                //                            {
                //                                checkExistDept = true;
                //                                if (rowData["LOAINHOMDICHVU"].ToString() == "5")
                //                                {
                //                                    DataRow drOption = dtOption.NewRow();
                //                                    drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                //                                    dtOption.Rows.InsertAt(drOption, 0);
                //                                }
                //                                else
                //                                {
                //                                    if (rowData["HAS_ORDER"].ToString() == "-1")
                //                                    {
                //                                        DataRow drOption = dtOption.NewRow();
                //                                        drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                //                                        dtOption.Rows.InsertAt(drOption, 0);
                //                                    }
                //                                    else
                //                                    {
                //                                        DataRow drOption = dtOption.NewRow();
                //                                        drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                //                                        dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                //                                    }
                //                                }
                //                            }
                //                            else
                //                            {
                //                                DataRow drOption = dtOption.NewRow();
                //                                drOption.ItemArray = data_ar.Rows[i1].ItemArray;
                //                                dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                //                            }
                //                        }
                //                    }

                //                    if (!checkExistDept && rowData["LOAINHOMDICHVU"].ToString() == "5")
                //                    {
                //                        if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                //                        {
                //                            if (!checkEqualDept)
                //                            {
                //                                DataRow drOption = dtOption.NewRow();
                //                                drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                //                                drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                //                                dtOption.Rows.InsertAt(drOption, 0);
                //                            }
                //                            else
                //                            {
                //                                DataRow drOption = dtOption.NewRow();
                //                                drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                //                                drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                //                                dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                //                            }
                //                        }
                //                        else
                //                        {
                //                            DataRow drOption = dtOption.NewRow();
                //                            drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                //                            drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                //                            dtOption.Rows.InsertAt(drOption, dtOption.Rows.Count);
                //                        }
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                dtOption.Columns.Add("PHONGID");
                //                dtOption.Columns.Add("PHONGNAME");
                //                DataRow drOption = dtOption.NewRow();
                //                drOption["PHONGID"] = opt.SUBDEPTID_LOGIN;
                //                drOption["PHONGNAME"] = opt.SUBDEPTNAME_LOGIN;
                //                dtOption.Rows.InsertAt(drOption, 0);
                //            }
                //            comboRitems.Add(rowData["DICHVUID"].ToString(), dtOption);
                //        }

                //        // set cboPHONG_TH
                //        RepositoryItemLookUpEdit cboPHONG_TH = new RepositoryItemLookUpEdit();
                //        cboPHONG_TH.DataSource = dtOption;
                //        cboPHONG_TH.DisplayMember = "PHONGNAME";
                //        cboPHONG_TH.ValueMember = "PHONGID";
                //        cboPHONG_TH.ShowHeader = false;
                //        cboPHONG_TH.ShowFooter = false;
                //        cboPHONG_TH.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PHONGNAME", 0, ""));
                //        cboPHONG_TH.AutoSearchColumnIndex = 1;
                //        e.RepositoryItem = cboPHONG_TH;

                //        if (rowData["PHONG_TH"].ToString().Contains(";"))
                //            gridView4.SetFocusedRowCellValue("PHONG_TH", dtOption.Rows[0]["PHONGID"].ToString());
                //    }
                //}
                //#endregion

                //#region load checkBox HEN
                //if (rowData.Row != null && e.Column.FieldName == "HEN")
                //{
                //    if (string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                //    {
                //        if (book)
                //        {
                //            if (bookAll)
                //            {
                //                gridView4.SetFocusedRowCellValue("HEN", "1");
                //            }
                //            if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                //            {
                //                if (rowData.DataView.Table.Columns.Contains("PHIEUHEN") && rowData["PHIEUHEN"].ToString() == "1")
                //                    gridView4.SetFocusedRowCellValue("HEN", "1");
                //                else
                //                    gridView4.SetFocusedRowCellValue("HEN", "0");
                //            }
                //        }
                //    }
                //}
                //#endregion

                //#region load cbo_TYLEDV
                //if (rowData.Row != null && e.Column.FieldName == "TYLEDV")
                //{
                //    if (!string.IsNullOrEmpty(opt.CHIDINHDICHVU) && opt.LOAIDICHVU == "13")
                //    {
                //        DataTable dt = new DataTable();
                //        dt.Columns.Add("name");
                //        dt.Columns.Add("value");
                //        dt.Rows.Add("100%", "1");
                //        dt.Rows.Add("70%", "0.7");
                //        dt.Rows.Add("50%", "0.5");
                //        dt.Rows.Add("30%", "0.3");
                //        RepositoryItemLookUpEdit cboTYLEDV = new RepositoryItemLookUpEdit();
                //        cboTYLEDV.DataSource = dt;
                //        cboTYLEDV.DisplayMember = "name";
                //        cboTYLEDV.ValueMember = "value";
                //        cboTYLEDV.ShowHeader = false;
                //        cboTYLEDV.ShowFooter = false;
                //        cboTYLEDV.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("name", 0, ""));
                //        cboTYLEDV.AutoSearchColumnIndex = 1;
                //        cboTYLEDV.EditValueChanged += cboTYLEDV_EditValueChanged;
                //        e.RepositoryItem = cboTYLEDV;
                //    }
                //}
                //#endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridXetNghiem_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                DataRowView ret = (DataRowView)gridXetNghiem.gridView.GetRow(e.RowHandle);
                GridView gridView = gridXetNghiem.gridView;
                if (ret != null)
                {
                    bool selected = !gridView.IsRowSelected(e.RowHandle); // Để biến selected giống trên web
                    if (selected)
                    {
                        int rowIds = gridView4.DataRowCount;
                        for (int i = 0; i < rowIds; i++)
                        {
                            DataRowView rowData = (DataRowView)gridView4.GetRow(i);
                            if (ret["DICHVUID"].ToString().Equals(rowData["DICHVUID"].ToString()))
                            {
                                MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được chỉ định trong phiếu");
                                gridView.SelectRow(e.RowHandle);
                                return;
                            }
                        }

                        if (isNgaydv && opt.DOITUONGBENHNHANID == "1")
                        {
                            string resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_S("NTU02D009.EV017",
                                txtMA_BHYT.Text + "$" + (!string.IsNullOrEmpty(txtTGCHIDINH.Text) ? txtTGCHIDINH.DateTime.ToString(Const.FORMAT_date1) : ""));
                            if (resultCheck == "0")
                            {
                                MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được sử dụng vào ngày " + resultCheck + ". Chưa đến hạn sử dụng tiếp theo của BHYT");
                                return;
                            }
                        }

                        // Note: Khi click vào checkbox thì tự động selectRow rồi nên ko cần select nữa, còn click vào các cell khác -> selectRow
                        if (!gridView.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                        {
                            gridView.SelectRow(e.RowHandle);
                        }
                    }
                    else
                    {
                        if (!gridView.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                        {
                            gridView.UnselectRow(e.RowHandle);
                        }
                    }
                    
                    bool checkKctt = CheckKhongCungThanhToan(gridView, e.RowHandle, selected);
                    if (checkKctt)
                    {
                        string resultCheck = "1";
                        if (selected && !isUncheckDuplicate)
                        {
                            string param = opt.KHAMBENHID + "$" + ret["DICHVUID"].ToString() + "$" + txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1);
                            resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NTU02D009.EV007", param);
                        }
                        if (resultCheck == "0")
                        {
                            if (existConfig)
                            {
                                DialogResult dialogResult = MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày, có tiếp tục chỉ định dịch vụ", "", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    if (!selected || opt.DOITUONGBENHNHANID != "1")
                                    {
                                        OnSelectRow(gridView, e.RowHandle, selected);
                                    }
                                    else
                                    {
                                        resultCheck = "0";
                                        if (!isCbNbh)
                                        {
                                            resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NGT02K016.CHECK_CDDV", ret["DICHVUID"].ToString());
                                        }
                                        if (resultCheck == "1")
                                        {
                                            dialogResult = MessageBox.Show("Có chắc chắn chỉ định dịch vụ nằm ngoài danh mục BHYT thanh toán cho bệnh nhân?", "", MessageBoxButtons.YesNo);
                                            if (dialogResult == DialogResult.Yes)
                                            {
                                                OnSelectRow(gridView, e.RowHandle, selected);
                                            }
                                            else if (dialogResult == DialogResult.No)
                                            {
                                                gridView.UnselectRow(e.RowHandle);
                                            }
                                        }
                                        else
                                        {
                                            OnSelectRow(gridView, e.RowHandle, selected);
                                        }
                                    }

                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    gridView.UnselectRow(e.RowHandle);
                                }
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày, đề nghị lựa chọn dịch vụ khác");
                                gridView.UnselectRow(e.RowHandle);
                            }
                        }
                        else
                        {
                            if (!selected || opt.DOITUONGBENHNHANID != "1")
                            {
                                OnSelectRow(gridView, e.RowHandle, selected);
                            }
                            else
                            {
                                resultCheck = "0";
                                if (!isCbNbh)
                                {
                                    resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NGT02K016.CHECK_CDDV", ret["DICHVUID"].ToString());
                                    //resultCheck = ServiceChiDinhDichVu.checkDvNgoaiBaoHiem(ret["DICHVUID"].ToString());
                                }
                                if (resultCheck == "1")
                                {
                                    DialogResult dialogResult = MessageBox.Show("Có chắc chắn chỉ định dịch vụ nằm ngoài danh mục BHYT thanh toán cho bệnh nhân?", "", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        OnSelectRow(gridView, e.RowHandle, selected);
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        gridView.UnselectRow(e.RowHandle);
                                    }
                                }
                                else
                                {
                                    OnSelectRow(gridView, e.RowHandle, selected);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridCDHA_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                DataRowView ret = (DataRowView)gridCDHA.gridView.GetRow(e.RowHandle);
                GridView gridView = gridCDHA.gridView;
                if (ret != null)
                {
                    bool selected = !gridView.IsRowSelected(e.RowHandle); // Để biến selected giống trên web
                    if (selected)
                    {
                        int rowIds = gridView4.DataRowCount;
                        for (int i = 0; i < rowIds; i++)
                        {
                            DataRowView rowData = (DataRowView)gridView4.GetRow(i);
                            if (ret["DICHVUID"].ToString().Equals(rowData["DICHVUID"].ToString()))
                            {
                                MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được chỉ định trong phiếu");
                                gridView.SelectRow(e.RowHandle);
                                return;
                            }
                        }
                        // Note: Khi click vào checkbox thì tự động selectRow rồi nên ko cần select nữa, còn click vào các cell khác -> selectRow
                        if (!gridView.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                        {
                            gridView.SelectRow(e.RowHandle);
                        }
                    }
                    else
                    {
                        if (!gridView.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                        {
                            gridView.UnselectRow(e.RowHandle);
                        }
                    }
                    bool checkKctt = CheckKhongCungThanhToan(gridView, e.RowHandle, selected);
                    if (checkKctt)
                    {
                        string resultCheck = "1";
                        if (selected && !isUncheckDuplicate)
                        {
                            string param = opt.KHAMBENHID + "$" + ret["DICHVUID"].ToString() + "$" + txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1);
                            resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NTU02D009.EV007", param);
                        }
                        if (resultCheck == "0")
                        {
                            if (existConfig)
                            {
                                DialogResult dialogResult = MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày, có tiếp tục chỉ định dịch vụ", "", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    if (!selected || opt.DOITUONGBENHNHANID != "1")
                                    {
                                        OnSelectRow(gridView, e.RowHandle, selected);
                                    }
                                    else
                                    {
                                        resultCheck = "0";
                                        if (!isCbNbh)
                                        {
                                            resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NGT02K016.CHECK_CDDV", ret["DICHVUID"].ToString());
                                        }
                                        if (resultCheck == "1")
                                        {
                                            dialogResult = MessageBox.Show("Có chắc chắn chỉ định dịch vụ nằm ngoài danh mục BHYT thanh toán cho bệnh nhân?", "", MessageBoxButtons.YesNo);
                                            if (dialogResult == DialogResult.Yes)
                                            {
                                                OnSelectRow(gridView, e.RowHandle, selected);
                                            }
                                            else if (dialogResult == DialogResult.No)
                                            {
                                                gridView.UnselectRow(e.RowHandle);
                                            }
                                        }
                                        else
                                        {
                                            OnSelectRow(gridView, e.RowHandle, selected);
                                        }
                                    }

                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    gridView.UnselectRow(e.RowHandle);
                                }
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày, đề nghị lựa chọn dịch vụ khác");
                                gridView.UnselectRow(e.RowHandle);
                            }
                        }
                        else
                        {
                            if (!selected || opt.DOITUONGBENHNHANID != "1")
                            {
                                OnSelectRow(gridView, e.RowHandle, selected);
                            }
                            else
                            {
                                resultCheck = "0";
                                if (!isCbNbh)
                                {
                                    resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NGT02K016.CHECK_CDDV", ret["DICHVUID"].ToString());
                                }
                                if (resultCheck == "1")
                                {
                                    DialogResult dialogResult = MessageBox.Show("Có chắc chắn chỉ định dịch vụ nằm ngoài danh mục BHYT thanh toán cho bệnh nhân?", "", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        OnSelectRow(gridView, e.RowHandle, selected);
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        gridView.UnselectRow(e.RowHandle);
                                    }
                                }
                                else
                                {
                                    OnSelectRow(gridView, e.RowHandle, selected);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridPTTT_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                DataRowView ret = (DataRowView)gridPTTT.gridView.GetRow(e.RowHandle);
                GridView gridView = gridPTTT.gridView;
                if (ret != null)
                {
                    bool selected = !gridView.IsRowSelected(e.RowHandle); // Để biến selected giống trên web
                    if (selected)
                    {
                        int rowIds = gridView4.DataRowCount;
                        for (int i = 0; i < rowIds; i++)
                        {
                            DataRowView rowData = (DataRowView)gridView4.GetRow(i);
                            if (ret["DICHVUID"].ToString().Equals(rowData["DICHVUID"].ToString()))
                            {
                                MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được chỉ định trong phiếu");
                                gridView.SelectRow(e.RowHandle);
                                return;
                            }
                        }
                        // Note: Khi click vào checkbox thì tự động selectRow rồi nên ko cần select nữa, còn click vào các cell khác -> selectRow
                        if (!gridView.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                        {
                            gridView.SelectRow(e.RowHandle);
                        }
                    }
                    else
                    {
                        if (!gridView.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                        {
                            gridView.UnselectRow(e.RowHandle);
                        }
                    }
                    bool checkKctt = CheckKhongCungThanhToan(gridView, e.RowHandle, selected);
                    if (checkKctt)
                    {
                        string resultCheck = "1";
                        if (selected && !isUncheckDuplicate)
                        {
                            string param = opt.KHAMBENHID + "$" + ret["DICHVUID"].ToString() + "$" + txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1);
                            resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NTU02D009.EV007", param);
                        }
                        if (resultCheck == "0")
                        {
                            if (existConfig)
                            {
                                DialogResult dialogResult = MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày, có tiếp tục chỉ định dịch vụ", "", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    if (!selected || opt.DOITUONGBENHNHANID != "1")
                                    {
                                        OnSelectRow(gridView, e.RowHandle, selected);
                                    }
                                    else
                                    {
                                        resultCheck = "0";
                                        if (!isCbNbh)
                                        {
                                            resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NGT02K016.CHECK_CDDV", ret["DICHVUID"].ToString());
                                        }
                                        if (resultCheck == "1")
                                        {
                                            dialogResult = MessageBox.Show("Có chắc chắn chỉ định dịch vụ nằm ngoài danh mục BHYT thanh toán cho bệnh nhân?", "", MessageBoxButtons.YesNo);
                                            if (dialogResult == DialogResult.Yes)
                                            {
                                                OnSelectRow(gridView, e.RowHandle, selected);
                                            }
                                            else if (dialogResult == DialogResult.No)
                                            {
                                                gridView.UnselectRow(e.RowHandle);
                                            }
                                        }
                                        else
                                        {
                                            OnSelectRow(gridView, e.RowHandle, selected);
                                        }
                                    }

                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    gridView.UnselectRow(e.RowHandle);
                                }
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show(ret["TENDICHVU"].ToString() + " đã được kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày, đề nghị lựa chọn dịch vụ khác");
                                gridView.UnselectRow(e.RowHandle);
                            }
                        }
                        else
                        {
                            if (!selected || opt.DOITUONGBENHNHANID != "1")
                            {
                                OnSelectRow(gridView, e.RowHandle, selected);
                            }
                            else
                            {
                                resultCheck = "0";
                                if (!isCbNbh)
                                {
                                    resultCheck = ServiceChiDinhDichVu.ajaxCALL_SP_I("NGT02K016.CHECK_CDDV", ret["DICHVUID"].ToString());
                                }
                                if (resultCheck == "1")
                                {
                                    DialogResult dialogResult = MessageBox.Show("Có chắc chắn chỉ định dịch vụ nằm ngoài danh mục BHYT thanh toán cho bệnh nhân?", "", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        OnSelectRow(gridView, e.RowHandle, selected);
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        gridView.UnselectRow(e.RowHandle);
                                    }
                                }
                                else
                                {
                                    OnSelectRow(gridView, e.RowHandle, selected);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridDichVuKhac_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                DataRowView ret = (DataRowView)gridDichVuKhac.gridView.GetRow(e.RowHandle);
                GridView gridView = gridDichVuKhac.gridView;
                if (ret != null)
                {
                    bool selected = !gridView.IsRowSelected(e.RowHandle);
                    // Note: Khi click vào checkbox thì tự động selectRow rồi nên ko cần select nữa, còn click vào các cell khác -> selectRow
                    if (!gridView.FocusedColumn.FieldName.Equals("DX$CheckboxSelectorColumn"))
                    {
                        if (selected)
                        {
                            gridView.SelectRow(e.RowHandle);
                        }
                        else
                        {
                            gridView.UnselectRow(e.RowHandle);
                        }
                    }
                    OnSelectRow(gridView, e.RowHandle, selected);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridView4_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            }
        }

        #endregion

        #region CÁC HÀM XỬ LÝ VÀ TÍNH TOÁN KHÁC + LƯU VÀ IN
        private bool CheckKhongCungThanhToan(GridView gridview, int id, bool selected)
        {
            try
            {
                DataRowView ret = (DataRowView)gridview.GetRow(id);
                if (selected)
                {
                    string idSelected = null;
                    int dscdIds = gridView4.DataRowCount;
                    for (int i = 0; i < dscdIds; i++)
                    {
                        DataRowView rowData = (DataRowView)gridView4.GetRow(i);
                        if (string.IsNullOrEmpty(idSelected))
                        {
                            idSelected = rowData["DICHVUID"].ToString();
                        }
                        else
                        {
                            idSelected = idSelected + ";" + rowData["DICHVUID"].ToString();
                        }
                    }
                    string param = ret["DICHVUID"].ToString() + "$" + opt.TIEPNHANID + "$" + txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1) + "$" + idSelected;
                    string res = ServiceChiDinhDichVu.ajaxCALL_SP_S("NTU02D009.KCTT", param);
                    if (!string.IsNullOrEmpty(res))
                    {
                        DialogResult dialogResult = MessageBox.Show("Dịch vụ " + res + " và " + ret["MADICHVU"].ToString() + " không được thanh toán cùng ngày. Bạn có muốn tiếp tục chỉ định?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            return true;
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            gridview.UnselectRow(id);
                            return false;
                        }

                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return true;
        }

        private void LoadPay(float _bd, float _bh)
        {
            try
            {
                float tongcp = float.Parse(lblTONGCHIPHI.Text.Replace("đ", ""), culture);
                float bhtra = float.Parse(lblBHTRA.Text.Replace("đ", ""), culture);
                tongcp = tongcp + _bd;
                bhtra = bhtra + _bh;
                lblTONGCHIPHI.Text = tongcp.ToString("N2", culture) + " đ";
                lblBHTRA.Text = bhtra.ToString("N2", culture) + " đ";
                lblBNTRA.Text = (tongcp - bhtra).ToString("N2", culture) + " đ";

                float tamung = float.Parse(lblTAMUNG.Text.Replace("đ", ""), culture);
                float miengiam = float.Parse(lblMIENGIAM.Text.Replace("đ", ""), culture);
                float danop = float.Parse(lblDANOP.Text.Replace("đ", ""), culture);
                float chenhlech = tamung - tongcp + bhtra;
                float nopthem = tamung - tongcp + bhtra + miengiam + danop;
                if (nopthem < 0)
                {
                    if (opt.LOAITIEPNHANID == "0")
                    {
                        if (flagMsg && flagMsgMoney)
                        {
                            MessageBox.Show("Bệnh nhân có chi phí phát sinh lớn hơn tiền tạm ứng");
                            flagMsg = false;
                        }
                    }
                    else
                    {
                        if (Const.local_user.HOSPITAL_ID == "944")
                        {
                            MessageBox.Show("Bệnh nhân có chi phí phát sinh lớn hơn tiền tạm ứng");
                            if (isChancbTu && opt.DOITUONGBENHNHANID != "1" && isQn != "4" && isHtvv != "2")
                            {
                                btnLuuIn.Enabled = false;
                                btnLuu.Enabled = false;
                            }
                        }
                    }
                    lblNOPTHEM.Text = (-1 * nopthem).ToString("N2", culture) + " đ";
                }
                else
                {
                    lblNOPTHEM.Text = "0đ";
                }
                lblCHENHLECH.Text = nopthem.ToString("N2", culture) + " đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void loadChange(float oldValue, float newValue, int rowId)
        {
            try
            {
                int rowIds = gridView4.DataRowCount;
                if (gridView4.DataRowCount > 0)
                {
                    float _totalTemp = 0;
                    float _insurranceTemp = 0;
                    float bhyt_af = 0;
                    float tyledv = 1;
                    DataRowView _param = (DataRowView)gridView4.GetRow(rowId);

                    if (opt.LOAIDICHVU == "13" && opt.DOITUONGBENHNHANID == "1")
                    {
                        tyledv = float.Parse(_param["TYLEDV"].ToString());
                    }

                    tongbh = tongbh + float.Parse(_param["GIABHYT"].ToString()) * (newValue - oldValue);
                    _totalTemp = float.Parse(_param["GIA_TIEN"].ToString(), culture) * (newValue - oldValue);
                    _insurranceTemp = float.Parse(_param["BHYT_TRA"].ToString(), culture) / oldValue * newValue - float.Parse(_param["BHYT_TRA"].ToString(), culture);
                    if (opt.DOITUONGBENHNHANID == "1" && float.Parse(_param["BHYT_TRA"].ToString(), culture) > 0)
                    {
                        for (int i = 0; i < gridView4.DataRowCount; i++)
                        {
                            DataRowView rowData = (DataRowView)gridView4.GetRow(i);
                            if ((rowData["LOAIDOITUONG"].ToString() == "1" || rowData["LOAIDOITUONG"].ToString() == "2") && i != rowId)
                            {
                                if (tongbh > (string.IsNullOrEmpty(hidTRAN_BHYT) ? float.Parse(hidTRAN_BHYT) : 0))
                                {
                                    gridView4.SetRowCellValue(i, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFINAL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "THANH_TIEN", float.Parse(rowData["THANH_TIENFINAL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "MIENGIAM", float.Parse(rowData["MIENGIAMFINAL"].ToString()).ToString("N2", culture));
                                    LoadPay(0, float.Parse(rowData["BHYT_TRAFINAL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                }
                                else
                                {
                                    gridView4.SetRowCellValue(i, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFULL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "THANH_TIEN", float.Parse(rowData["THANH_TIENFULL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "MIENGIAM", float.Parse(rowData["MIENGIAMFULL"].ToString()).ToString("N2", culture));
                                    LoadPay(0, float.Parse(rowData["BHYT_TRAFULL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                }
                            }
                        }
                    }
                    if (tongbh > (string.IsNullOrEmpty(hidTRAN_BHYT) ? float.Parse(hidTRAN_BHYT) : 0))
                    {
                        bhyt_af = float.Parse(_param["BHYT_TRAFINAL"].ToString()) / oldValue * newValue;
                        gridView4.SetRowCellValue(rowId, "BHYT_TRA", (float.Parse(_param["BHYT_TRAFINAL"].ToString()) / oldValue * newValue).ToString("N2", culture));
                        gridView4.SetRowCellValue(rowId, "MIENGIAM", (float.Parse(_param["MIENGIAMFINAL"].ToString()) / oldValue * newValue).ToString("N2", culture));
                        gridView4.SetRowCellValue(rowId, "THANH_TIEN", (float.Parse(_param["GIA_TIEN"].ToString(), culture) * newValue * tyledv
                            - float.Parse(_param["BHYT_TRAFINAL"].ToString()) / oldValue * newValue
                            - float.Parse(_param["MIENGIAMFINAL"].ToString()) / oldValue * newValue).ToString("N2", culture));

                        gridView4.SetRowCellValue(rowId, "BHYT_TRAFULL", float.Parse(_param["BHYT_TRAFULL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "MIENGIAMFULL", float.Parse(_param["MIENGIAMFULL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "THANH_TIENFULL", float.Parse(_param["GIA_TIEN"].ToString(), culture) * newValue * tyledv
                            - float.Parse(_param["BHYT_TRAFULL"].ToString()) / oldValue * newValue
                            - float.Parse(_param["MIENGIAMFULL"].ToString()) / oldValue * newValue);

                        gridView4.SetRowCellValue(rowId, "BHYT_TRAFINAL", float.Parse(_param["BHYT_TRAFINAL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "MIENGIAMFINAL", float.Parse(_param["MIENGIAMFINAL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "THANH_TIENFINAL", float.Parse(_param["GIA_TIEN"].ToString(), culture) * newValue * tyledv
                            - float.Parse(_param["BHYT_TRAFINAL"].ToString()) / oldValue * newValue
                            - float.Parse(_param["MIENGIAMFINAL"].ToString()) / oldValue * newValue);
                    }
                    else
                    {
                        bhyt_af = float.Parse(_param["BHYT_TRAFULL"].ToString()) / oldValue * newValue;
                        gridView4.SetRowCellValue(rowId, "BHYT_TRA", (float.Parse(_param["BHYT_TRAFULL"].ToString()) / oldValue * newValue).ToString("N2", culture));
                        gridView4.SetRowCellValue(rowId, "MIENGIAM", (float.Parse(_param["MIENGIAMFULL"].ToString()) / oldValue * newValue).ToString("N2", culture));
                        gridView4.SetRowCellValue(rowId, "THANH_TIEN", (float.Parse(_param["GIA_TIEN"].ToString(), culture) * newValue * tyledv
                            - float.Parse(_param["BHYT_TRAFULL"].ToString()) / oldValue * newValue
                            - float.Parse(_param["MIENGIAMFULL"].ToString()) / oldValue * newValue).ToString("N2", culture));

                        gridView4.SetRowCellValue(rowId, "BHYT_TRAFULL", float.Parse(_param["BHYT_TRAFULL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "MIENGIAMFULL", float.Parse(_param["MIENGIAMFULL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "THANH_TIENFULL", float.Parse(_param["GIA_TIEN"].ToString(), culture) * newValue * tyledv
                            - float.Parse(_param["BHYT_TRAFULL"].ToString()) / oldValue * newValue
                            - float.Parse(_param["MIENGIAMFULL"].ToString()) / oldValue * newValue);

                        gridView4.SetRowCellValue(rowId, "BHYT_TRAFINAL", float.Parse(_param["BHYT_TRAFINAL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "MIENGIAMFINAL", float.Parse(_param["MIENGIAMFINAL"].ToString()) / oldValue * newValue);
                        gridView4.SetRowCellValue(rowId, "THANH_TIENFINAL", float.Parse(_param["GIA_TIEN"].ToString(), culture) * newValue * tyledv
                            - float.Parse(_param["BHYT_TRAFINAL"].ToString()) / oldValue * newValue
                            - float.Parse(_param["MIENGIAMFINAL"].ToString()) / oldValue * newValue);
                    }
                    _insurranceTemp = bhyt_af - float.Parse(_param["BHYT_TRA"].ToString(), culture);
                    LoadPay(_totalTemp, _insurranceTemp);

                    gridView4.SetFocusedRowCellValue("TONGTIEN",_totalTemp - _insurranceTemp
                            + float.Parse(_param["SOLUONG"].ToString()) * float.Parse(_param.DataView.Table.Columns.Contains("GIACHENH") ?
                            (_param["GIACHENH"].ToString() ?? "0") : "0"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void changeTYLEDV()
        {
            try
            {
                int rowId = gridView4.FocusedRowHandle;
                float _insurance = 0;
                float _insurance_full = 0;
                float _insurance_final = 0;

                DataRowView rowData = (DataRowView)(gridView4.GetFocusedRow());
                if (rowData != null)
                {
                    if (tongbh > float.Parse(hidTRAN_BHYT ?? "0"))
                    {
                        _insurance = !string.IsNullOrEmpty(hidQUYENLOITHE) ? float.Parse(hidQUYENLOITHE) : float.Parse(txtQUYENLOI.Text);
                    }
                    else
                    {
                        _insurance = !string.IsNullOrEmpty(hidQUYENLOI) ? float.Parse(hidQUYENLOI) : float.Parse(txtQUYENLOI.Text);
                    }
                    _insurance_full = !string.IsNullOrEmpty(hidQUYENLOI) ? float.Parse(hidQUYENLOI) : float.Parse(txtQUYENLOI.Text);
                    _insurance_final = !string.IsNullOrEmpty(hidQUYENLOITHE) ? float.Parse(hidQUYENLOITHE) : float.Parse(txtQUYENLOI.Text);

                    DataTable r = new DataTable();
                    DataTable r_full = new DataTable();
                    DataTable r_final = new DataTable();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("DOITUONGBENHNHANID", typeof(String));
                    dt.Columns.Add("MUCHUONG", typeof(String));
                    dt.Columns.Add("GIATRANBH", typeof(String));
                    dt.Columns.Add("GIABHYT", typeof(String));
                    dt.Columns.Add("GIAND", typeof(String));
                    dt.Columns.Add("GIADV", typeof(String));
                    dt.Columns.Add("GIANN", typeof(String));
                    dt.Columns.Add("DOITUONGCHUYEN", typeof(String));
                    dt.Columns.Add("GIADVKTC", typeof(String));
                    dt.Columns.Add("MANHOMBHYT", typeof(String));
                    dt.Columns.Add("SOLUONG", typeof(String));
                    dt.Columns.Add("CANTRENDVKTC", typeof(String));
                    dt.Columns.Add("THEDUTHOIGIAN", typeof(String));
                    dt.Columns.Add("DUOCVANCHUYEN", typeof(String));
                    dt.Columns.Add("TYLETHUOCVATTU", typeof(String));
                    dt.Columns.Add("NHOMDOITUONG", typeof(String));
                    dt.Columns.Add("NGAYHANTHE", typeof(String));
                    dt.Columns.Add("NGAYDICHVU", typeof(String));
                    dt.Columns.Add("TYLE_MIENGIAM", typeof(String));
                    dt.Columns.Add("TYLEDV", typeof(String));
                    DataRow dr = dt.NewRow();

                    dr["DOITUONGBENHNHANID"] = opt.DOITUONGBENHNHANID;
                    if (opt.LOAIDICHVU == "14" && isTranspost)
                    {
                        dr["MUCHUONG"] = hidTYLE_THEBHYT;
                    }
                    else
                    {
                        dr["MUCHUONG"] = _insurance;
                    }
                    dr["GIATRANBH"] = rowData.DataView.Table.Columns.Contains("DICHVU_BHYT_DINHMUC") ? rowData["DICHVU_BHYT_DINHMUC"].ToString() : "";
                    dr["GIABHYT"] = rowData["GIABHYT"].ToString();
                    dr["GIAND"] = rowData["GIANHANDAN"].ToString();
                    dr["GIADV"] = rowData["GIADICHVU"].ToString();
                    dr["GIANN"] = "0";
                    dr["DOITUONGCHUYEN"] = "0";
                    dr["GIADVKTC"] = (opt.DOITUONGBENHNHANID == "1" && rowData.DataView.Table.Columns.Contains("GIA_DVC") && float.Parse(rowData["GIA_DVC"].ToString()) > 0)
                        ? rowData["GIA_DVC"].ToString() : "0";
                    dr["MANHOMBHYT"] = rowData["MANHOM_BHYT"].ToString();
                    dr["SOLUONG"] = rowData.DataView.Table.Columns.Contains("SOLUONG") ? rowData["SOLUONG"].ToString() : "1";
                    dr["CANTRENDVKTC"] = hidCANTRENKTC;
                    dr["THEDUTHOIGIAN"] = hidBHFULL;
                    dr["DUOCVANCHUYEN"] = hidDUOCVC;
                    dr["TYLETHUOCVATTU"] = "100";
                    dr["NHOMDOITUONG"] = hidNHOMDOITUONG;
                    dr["NGAYHANTHE"] = hidNGAYHANTHE;
                    dr["NGAYDICHVU"] = !string.IsNullOrEmpty(txtTGCHIDINH.Text) ? txtTGCHIDINH.DateTime.ToString(Const.FORMAT_date1) : "";
                    dr["TYLE_MIENGIAM"] = hidTYLEMIENGIAM;
                    if (opt.LOAIDICHVU == "13")
                    {
                        dr["TYLEDV"] = rowData["TYLEDV"].ToString();
                    }
                    else
                    {
                        dr["TYLEDV"] = "1";
                    }
                    dt.Rows.InsertAt(dr, 0);
                    DataTable dtFull = dt.Copy();
                    DataTable dtFinal = dt.Copy();

                    // function vien phi
                    r = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dt);
                    dtFull.Rows[0]["MUCHUONG"] = _insurance_full;
                    r_full = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtFull);
                    if (opt.LOAIDICHVU == "14" && isTranspost)
                    {
                        dtFinal.Rows[0]["MUCHUONG"] = hidTYLE_THEBHYT;
                    }
                    else
                    {
                        dtFinal.Rows[0]["MUCHUONG"] = _insurance_final;
                    }
                    r_final = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtFinal);

                    if (r.Rows[0]["bh_tra"].ToString() != "-1" && r.Rows[0]["nd_tra"].ToString() != "-1")
                    {
                        var _oldBHYTDataRow = rowData["BHYT_TRA"].ToString();
                        var _oldTTDataRow = rowData["THANH_TIEN"].ToString();

                        var _newBHYTDataRow = float.Parse(r.Rows[0]["bh_tra"].ToString());
                        var _newTTDataRow = float.Parse(r.Rows[0]["tong_cp"].ToString());

                        var _payBHYTChange = _newBHYTDataRow - float.Parse(_oldBHYTDataRow, culture);
                        var _payTTChange = 0;
                        LoadPay(_payTTChange, _payBHYTChange);

                        gridView4.SetFocusedRowCellValue("BHYT_TRA", float.Parse(r.Rows[0]["bh_tra"].ToString()).ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("MIENGIAM",
                            float.Parse(!string.IsNullOrEmpty(r.Rows[0]["mien_giam"].ToString()) ? r.Rows[0]["mien_giam"].ToString() : "0").ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("THANH_TIEN", float.Parse(r.Rows[0]["nd_tra"].ToString()).ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("TONGTIEN",
                            float.Parse(r.Rows[0]["nd_tra"].ToString()) * float.Parse(rowData["SOLUONG"].ToString()) - float.Parse(r.Rows[0]["bh_tra"].ToString())
                            + float.Parse(rowData["SOLUONG"].ToString()) * float.Parse(rowData.DataView.Table.Columns.Contains("GIACHENH") ? 
                            (rowData["GIACHENH"].ToString() ?? "0") : "0"));
                        gridView4.SetFocusedRowCellValue("BHYT_TRAFINAL",
                            !string.IsNullOrEmpty(r_final.Rows[0]["bh_tra"].ToString()) ? r_final.Rows[0]["bh_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("MIENGIAMFINAL",
                            !string.IsNullOrEmpty(r_final.Rows[0]["mien_giam"].ToString()) ? r_final.Rows[0]["mien_giam"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("THANH_TIENFINAL",
                            !string.IsNullOrEmpty(r_final.Rows[0]["nd_tra"].ToString()) ? r_final.Rows[0]["nd_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("BHYT_TRAFULL",
                            !string.IsNullOrEmpty(r_full.Rows[0]["bh_tra"].ToString()) ? r_full.Rows[0]["bh_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("MIENGIAMFULL",
                            !string.IsNullOrEmpty(r_full.Rows[0]["mien_giam"].ToString()) ? r_full.Rows[0]["mien_giam"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("THANH_TIENFULL",
                            !string.IsNullOrEmpty(r_full.Rows[0]["nd_tra"].ToString()) ? r_full.Rows[0]["nd_tra"].ToString() : "0");
                    }
                    else
                    {
                        MessageBox.Show("Không thể đổi tỷ lệ cho bệnh nhân này");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            currentTime = DateTime.ParseExact(RequestHTTP.getSysDatetime(), Const.FORMAT_datetime1, culture);
            if (!isCbQuaTg && txtTGCHIDINH.DateTime > currentTime)
            {
                DialogResult dialogResult = MessageBox.Show("Thời gian chỉ định lớn hơn thời gian hiện tại. Bạn có tiếp tục?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    saveData(0);
                }
            }
            else
            {
                saveData(0);
            }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            if (txtTGCHIDINH.DateTime > currentTime)
            {
                DialogResult dialogResult = MessageBox.Show("Thời gian chỉ định lớn hơn thời gian hiện tại. Bạn có tiếp tục?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    saveData(1);
                }
            }
            else
            {
                saveData(1);
            }
        }

        private void saveData(int mode)
        {
            try
            {
                DataTable dt = (DataTable)gridDSCD.DataSource;
                if (gridView4.DataRowCount > 0)
                {
                    if (validate())
                    {
                        string func = "";
                        bool delMbp = true;
                        Dictionary<string, DataTable> map = new Dictionary<string, DataTable>();
                        Dictionary<string, DataTable> mapMain = new Dictionary<string, DataTable>();
                        if (!string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                        {
                            func = mode == 0 ? "NTU01H003.EV002" : "NTU01H003.EV003";
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataRow rowData = dt.Rows[i];
                                rowData["BHYT_TRA"] = rowData["BHYT_TRAFINAL"];
                                // Format lại các trường tiền tệ
                                rowData["GIA_TIEN"] = float.Parse(rowData["GIA_TIEN"].ToString(), culture);
                                rowData["MIENGIAM"] = float.Parse(rowData["MIENGIAM"].ToString(), culture);
                                rowData["THANH_TIEN"] = float.Parse(rowData["THANH_TIEN"].ToString(), culture);

                                rowData["PHONG_TH"] = "1";
                                if (opt.LOAIDICHVU != "13")
                                {
                                    rowData["TYLEDV"] = "1";
                                }
                                DataTable dtList = dt.Clone();
                                if (!map.ContainsKey(rowData["PHONG_TH"].ToString() + ","))
                                {
                                    var drRow = dtList.NewRow();
                                    drRow.ItemArray = rowData.ItemArray;
                                    dtList.Rows.InsertAt(drRow, 0);
                                    map.Add(rowData["PHONG_TH"].ToString() + ",", dtList);
                                }
                                else
                                {
                                    dtList = (DataTable)map[rowData["PHONG_TH"].ToString() + ","];
                                    var drRow = dtList.NewRow();
                                    drRow.ItemArray = rowData.ItemArray;
                                    dtList.Rows.InsertAt(drRow, 0);
                                    map[rowData["PHONG_TH"].ToString() + ","] = dtList;
                                }
                                if (!string.IsNullOrEmpty(rowData["MAUBENHPHAMID"].ToString()))
                                {
                                    delMbp = false;
                                    mapMain.Add(rowData["PHONG_TH"].ToString() + "," + rowData["MAUBENHPHAMID"].ToString(), dtList);
                                }
                            }

                            if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                            {
                                if (delMbp)
                                {
                                    map.Add("-1," + opt.MAUBENHPHAMID, new DataTable());
                                }
                            }
                        }
                        else
                        {
                            func = "NGT02K016.INS_PRT";
                            delMbp = true;
                            if (opt.DOITUONGBENHNHANID == "1")
                            {
                                for (int i2 = 0; i2 < dt.Rows.Count; i2++)
                                {
                                    DataRow rowData = dt.Rows[i2];
                                    if (!string.IsNullOrEmpty(rowData["DICHVUCHINH"].ToString()) && rowData["LOAIDOITUONG"].ToString() == "1")
                                    {
                                        string[] dvDiKem = rowData["DICHVUDIKEM"].ToString().Split(';');
                                        for (int i3 = 0; i3 < dvDiKem.Length; i3++)
                                        {
                                            for (int i4 = 0; i4 < dt.Rows.Count; i4++)
                                            {
                                                if (dvDiKem[i3] == dt.Rows[i4]["DICHVUID"].ToString())
                                                {
                                                    dt.Rows[i4]["BHYT_TRA"] = "0";
                                                    dt.Rows[i4]["THANH_TIEN"] = "0";
                                                    dt.Rows[i4]["LOAIDOITUONG"] = "12";
                                                    dt.Rows[i4]["GIA_TIEN"] = "0";
                                                    dt.Rows[i4]["LOAI_DT_MOI"] = "";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataRow rowData = dt.Rows[i];
                                rowData["BHYT_TRA"] = rowData["BHYT_TRAFINAL"];
                                // Format lại các trường tiền tệ
                                rowData["GIA_TIEN"] = float.Parse(rowData["GIA_TIEN"].ToString(), culture);
                                rowData["MIENGIAM"] = float.Parse(rowData["MIENGIAM"].ToString(), culture);
                                rowData["THANH_TIEN"] = float.Parse(rowData["THANH_TIEN"].ToString(), culture);

                                if (isGroupService)
                                {
                                    rowData["NHOMDICHVUID"] = "1";
                                }
                                else
                                {
                                    if (isTachCdha && rowData["NHOMDICHVUID"].ToString() == "4"
                                        && isNhomDvKtp.IndexOf(rowData["NHOMDICHVUID"].ToString()) == -1)
                                    {
                                        rowData["NHOMDICHVUID"] = rowData["NHOMDICHVUID"] + "_" + i;
                                    }
                                }
                                if (rowData["HEN"].ToString() == "1")
                                {
                                    appointment = true;
                                }

                                if (!string.IsNullOrEmpty(rowData["TYLEDVTEMP"].ToString()) && rowData["TYLEDVTEMP"].ToString() != "1")
                                {
                                    rowData["TYLEDV"] = rowData["TYLEDVTEMP"].ToString();
                                }
                                else
                                {
                                    rowData["TYLEDV"] = "1";
                                }
                                DataTable dtList = dt.Clone();
                                if (!map.ContainsKey(rowData["NHOMDICHVUID"].ToString() + "," + rowData["PHONG_TH"].ToString() + "," + rowData["NHOMBENHPHAM"].ToString() + ","))
                                {
                                    var drRow = dtList.NewRow();
                                    drRow.ItemArray = rowData.ItemArray;
                                    dtList.Rows.InsertAt(drRow, 0);
                                    map.Add(rowData["NHOMDICHVUID"].ToString() + "," + rowData["PHONG_TH"].ToString() + "," + rowData["NHOMBENHPHAM"].ToString() + ",", dtList);
                                }
                                else
                                {
                                    dtList = (DataTable)map[rowData["NHOMDICHVUID"].ToString() + "," + rowData["PHONG_TH"].ToString() + "," + rowData["NHOMBENHPHAM"].ToString() + ","];
                                    var drRow = dtList.NewRow();
                                    drRow.ItemArray = rowData.ItemArray;
                                    dtList.Rows.InsertAt(drRow, 0);
                                    map[rowData["NHOMDICHVUID"].ToString() + "," + rowData["PHONG_TH"].ToString() + "," + rowData["NHOMBENHPHAM"].ToString() + ","] = dtList;
                                }
                            }
                            if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                            {
                                if (delMbp)
                                {
                                    map.Add("-1," + "-1," + "-1," + opt.MAUBENHPHAMID, new DataTable());
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(opt.MAUBENHPHAMID))
                        {
                            foreach (string keyM in mapMain.Keys)
                            {
                                foreach (string key in map.Keys)
                                {
                                    if (keyM.IndexOf(key) != -1)
                                    {
                                        map[keyM] = map[key];
                                        map.Remove(key);
                                    }
                                }
                            }
                        }
                        string _hienThiThongBao = ServiceChiDinhDichVu.ajaxCALL_SP_S("COM.CAUHINH", "KBH_TUDONGLUU_KBHB");
                        string _msggg = _hienThiThongBao == "1" ? "" : "Chỉ định dịch vụ thành công";

                        DataTable objData = setFormToObj_ThongTinBN();
                        string strJson1 = JsonConvert.SerializeObject(objData).Replace("\"", "\\\"").Replace("[", "").Replace("]", "");
                        string strJson2 = JsonConvert.SerializeObject(map).Replace("\"", "\\\"");

                        if (mode == 0)
                        {
                            string ret = ServiceChiDinhDichVu.ajaxCALL_SP_S(func, strJson1 + "$" + strJson2);
                            // ret : ";309605-1-180115000001-0001"
                            if (ret == "0")
                            {
                                MessageBox.Show("Có lỗi khi chỉ định dịch vụ");
                            }
                            else
                            {
                                if (LISConnector.LIS_CONNECTION_TYPE == "1" || LISConnector.LIS_CONNECTION_TYPE == "2"
                                    && !string.IsNullOrEmpty(LISConnector.LIS_SERVICE_DOMAIN_NAME))
                                {
                                    string[] arrMbp = ret.Split(';');
                                    for (int i = 0; i < arrMbp.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(arrMbp[i]))
                                        {
                                            string[] param = arrMbp[i].Split('-');
                                            if (param.Length > 3 && param[1] == "1")
                                            {
                                                SendRequestToLab(param[2], param[0], param[3]);
                                            }
                                        }
                                    }
                                }
                                if (event_ListenFrm_KetQua_Thuoc_ChiDinhDV != null)
                                {
                                    Form frm = this.FindForm();
                                    frm.Close();
                                    event_ListenFrm_KetQua_Thuoc_ChiDinhDV("Chỉ định dịch vụ thành công", null);
                                }
                            }
                        }
                        else
                        {
                            string ret = ServiceChiDinhDichVu.ajaxCALL_SP_S(func, strJson1 + "$" + strJson2);
                            if (ret == "0")
                            {
                                MessageBox.Show("Có lỗi khi chỉ định dịch vụ");
                            }
                            else
                            {
                                // Xử lý in
                                string[] arrMbp = ret.Split(';');
                                bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;

                                try
                                {
                                    if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                                    if (printPrivare)
                                    {
                                        for (int i = 0; i < arrMbp.Length; i++)
                                        {
                                            if (!string.IsNullOrEmpty(arrMbp[i]))
                                            {
                                                string[] param = arrMbp[i].Split('-');
                                                DataTable par = new DataTable();
                                                par.Columns.Add("name");
                                                par.Columns.Add("type");
                                                par.Columns.Add("value");
                                                par.Rows.Add("maubenhphamid", "String", param[0]);
                                                par.Rows.Add("i_hid", "String", Const.local_user.HOSPITAL_ID);
                                                par.Rows.Add("i_sch", "String", Const.local_user.DB_SCHEMA);

                                                if (param[1] == "1")
                                                {
                                                    if (Const.local_user.HOSPITAL_ID == "965")
                                                    {
                                                        if (opt.DOITUONGBENHNHANID == "1")
                                                        {
                                                            var _loaiDichVu = "0";
                                                            var _par_loai = param[0];
                                                            DataTable dtLoaiDv = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D075_LOAIDICHVU", _par_loai, 0);
                                                            if (dtLoaiDv.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow dr in dtLoaiDv.Rows)
                                                                {
                                                                    _loaiDichVu = dr["BHYT"].ToString();
                                                                    if (_loaiDichVu == "1")
                                                                    {
                                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                        {
                                                                            //Print
                                                                            Func.PrintFile_FromData("PHIEU_XETNGHIEM_A4_965", par, _typePrint);
                                                                        }
                                                                        else
                                                                        {
                                                                            //ShowDialog
                                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_XETNGHIEM_A4_965", par);
                                                                            frm.WindowState = FormWindowState.Normal;
                                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                                            frm.ShowDialog();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                        {
                                                                            //Print
                                                                            Func.PrintFile_FromData("PHIEU_XETNGHIEMDICHVU_A4_965", par, _typePrint);
                                                                        }
                                                                        else
                                                                        {
                                                                            //ShowDialog
                                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_XETNGHIEMDICHVU_A4_965", par);
                                                                            frm.WindowState = FormWindowState.Normal;
                                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                                            frm.ShowDialog();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                {
                                                                    //Print
                                                                    Func.PrintFile_FromData("PHIEU_XETNGHIEMDICHVU_A4_965", par, _typePrint);
                                                                }
                                                                else
                                                                {
                                                                    //ShowDialog
                                                                    frmPrint frm = new frmPrint("In phiếu", "PHIEU_XETNGHIEMDICHVU_A4_965", par);
                                                                    frm.WindowState = FormWindowState.Normal;
                                                                    frm.StartPosition = FormStartPosition.CenterParent;
                                                                    frm.ShowDialog();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                        {
                                                            //Print
                                                            Func.PrintFile_FromData("PHIEU_XETNGHIEM", par, _typePrint);
                                                        }
                                                        else
                                                        {
                                                            //ShowDialog
                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_XETNGHIEM", par);
                                                            frm.WindowState = FormWindowState.Normal;
                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                            frm.ShowDialog();
                                                        }
                                                    }

                                                    if (LISConnector.LIS_CONNECTION_TYPE == "1" || LISConnector.LIS_CONNECTION_TYPE == "2"
                                                        && !string.IsNullOrEmpty(LISConnector.LIS_SERVICE_DOMAIN_NAME))
                                                    {
                                                        if (param.Length > 3 && param[1] == "1")
                                                        {
                                                            SendRequestToLab(param[2], param[0], param[3]);
                                                        }
                                                    }
                                                }
                                                else if (param[1] == "2")
                                                {
                                                    if (Const.local_user.HOSPITAL_ID == "965")
                                                    {
                                                        if (opt.DOITUONGBENHNHANID == "1")
                                                        {
                                                            var _loaiDichVuC = "0";
                                                            var _par_loaiC = param[0];
                                                            DataTable dtLoaiDvC = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D075_LOAIDICHVU", _par_loaiC, 0);
                                                            if (dtLoaiDvC.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow dr in dtLoaiDvC.Rows)
                                                                {
                                                                    _loaiDichVuC = dr["BHYT"].ToString();
                                                                    if (_loaiDichVuC == "1")
                                                                    {
                                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                        {
                                                                            //Print
                                                                            Func.PrintFile_FromData("PHIEU_CDHA_A4_965", par, _typePrint); 
                                                                        }
                                                                        else
                                                                        {
                                                                            //ShowDialog
                                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_CDHA_A4_965", par);
                                                                            frm.WindowState = FormWindowState.Normal;
                                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                                            frm.ShowDialog();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                        {
                                                                            //Print
                                                                            Func.PrintFile_FromData("PHIEU_CDHADICHVU_A4_965", par, _typePrint); 
                                                                        }
                                                                        else
                                                                        {
                                                                            //ShowDialog
                                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_CDHADICHVU_A4_965", par);
                                                                            frm.WindowState = FormWindowState.Normal;
                                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                                            frm.ShowDialog();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                {
                                                                    //Print
                                                                    Func.PrintFile_FromData("PHIEU_CDHADICHVU_A4_965", par, _typePrint); 
                                                                }
                                                                else
                                                                {
                                                                    //ShowDialog
                                                                    frmPrint frm = new frmPrint("In phiếu", "PHIEU_CDHADICHVU_A4_965", par);
                                                                    frm.WindowState = FormWindowState.Normal;
                                                                    frm.StartPosition = FormStartPosition.CenterParent;
                                                                    frm.ShowDialog();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                        {
                                                            //Print
                                                            Func.PrintFile_FromData("PHIEU_CDHA", par, _typePrint); 
                                                        }
                                                        else
                                                        {
                                                            //ShowDialog
                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_CDHA", par);
                                                            frm.WindowState = FormWindowState.Normal;
                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                            frm.ShowDialog();
                                                        }
                                                    }
                                                }
                                                // nghiant 20092017 L2DKBD-404
                                                else if (checkInthukhac)
                                                {
                                                    if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                    {
                                                        //Print
                                                        Func.PrintFile_FromData("DKBD_PCD_THEM_CONG_KHAM_A5", par, _typePrint); 
                                                    }
                                                    else
                                                    {
                                                        //ShowDialog
                                                        frmPrint frm = new frmPrint("In phiếu", "DKBD_PCD_THEM_CONG_KHAM_A5", par);
                                                        frm.WindowState = FormWindowState.Normal;
                                                        frm.StartPosition = FormStartPosition.CenterParent;
                                                        frm.ShowDialog();
                                                    }
                                                }
                                                // end nghiant 20092017 L2DKBD-404
                                                else
                                                {
                                                    if (Const.local_user.HOSPITAL_ID == "965")
                                                    {
                                                        if (opt.DOITUONGBENHNHANID == "1")
                                                        {
                                                            var _loaiDichVuD = "0";
                                                            var _par_loaiD = param[0];
                                                            DataTable dtLoaiDvD = ServiceChiDinhDichVu.ajaxCALL_SP_O("NTU02D075_LOAIDICHVU", _par_loaiD, 0);
                                                            if (dtLoaiDvD.Rows.Count > 0)
                                                            {
                                                                foreach (DataRow dr in dtLoaiDvD.Rows)
                                                                {
                                                                    _loaiDichVuD = dr["BHYT"].ToString();
                                                                    if (_loaiDichVuD == "1")
                                                                    {
                                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                        {
                                                                            //Print
                                                                            Func.PrintFile_FromData("PHIEU_PTTT_A4_965", par, _typePrint); 
                                                                        }
                                                                        else
                                                                        {
                                                                            //ShowDialog
                                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_PTTT_A4_965", par);
                                                                            frm.WindowState = FormWindowState.Normal;
                                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                                            frm.ShowDialog();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                        {
                                                                            //Print
                                                                            Func.PrintFile_FromData("PHIEU_PTTTDICHVU_A4_965", par, _typePrint); 
                                                                        }
                                                                        else
                                                                        {
                                                                            //ShowDialog
                                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_PTTTDICHVU_A4_965", par);
                                                                            frm.WindowState = FormWindowState.Normal;
                                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                                            frm.ShowDialog();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                                {
                                                                    //Print
                                                                    Func.PrintFile_FromData("PHIEU_PTTTDICHVU_A4_965", par, _typePrint); 
                                                                }
                                                                else
                                                                {
                                                                    //ShowDialog
                                                                    frmPrint frm = new frmPrint("In phiếu", "PHIEU_PTTTDICHVU_A4_965", par);
                                                                    frm.WindowState = FormWindowState.Normal;
                                                                    frm.StartPosition = FormStartPosition.CenterParent;
                                                                    frm.ShowDialog();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                                        {
                                                            //Print
                                                            Func.PrintFile_FromData("PHIEU_PHAUTHUAT_A4", par, _typePrint); 
                                                        }
                                                        else
                                                        {
                                                            //ShowDialog
                                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_PHAUTHUAT_A4", par);
                                                            frm.WindowState = FormWindowState.Normal;
                                                            frm.StartPosition = FormStartPosition.CenterParent;
                                                            frm.ShowDialog();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string nhomMauBenhPhamId = "";
                                        for (int i = 0; i < arrMbp.Length; i++)
                                        {
                                            if (!string.IsNullOrEmpty(arrMbp[i]))
                                            {
                                                string[] param = arrMbp[i].Split('-');
                                                string funcName = "";
                                                if (param[1] == "1")
                                                {
                                                    funcName = "PHIEU_XETNGHIEM";
                                                    if (LISConnector.LIS_CONNECTION_TYPE == "1" || LISConnector.LIS_CONNECTION_TYPE == "2"
                                                        && !string.IsNullOrEmpty(LISConnector.LIS_SERVICE_DOMAIN_NAME))
                                                    {
                                                        if (param.Length > 3)
                                                        {
                                                            SendRequestToLab(param[2], param[0], param[3]);
                                                        }
                                                    }
                                                    nhomMauBenhPhamId = string.IsNullOrEmpty(nhomMauBenhPhamId) ? param[0] : nhomMauBenhPhamId + ";" + param[0];
                                                }
                                                else if (param[1] == "2")
                                                {
                                                    funcName = "PHIEU_CDHA";
                                                    nhomMauBenhPhamId = string.IsNullOrEmpty(nhomMauBenhPhamId) ? param[0] : nhomMauBenhPhamId + ";" + param[0];
                                                }
                                                else
                                                {
                                                    funcName = "PHIEU_PHAUTHUAT_A4";
                                                    nhomMauBenhPhamId = string.IsNullOrEmpty(nhomMauBenhPhamId) ? param[0] : nhomMauBenhPhamId + ";" + param[0];
                                                }
                                            }
                                        }

                                        DataTable par = new DataTable();
                                        par.Columns.Add("name");
                                        par.Columns.Add("type");
                                        par.Columns.Add("value");
                                        par.Rows.Add("maubenhphamid", "String", nhomMauBenhPhamId);
                                        par.Rows.Add("i_hid", "String", Const.local_user.HOSPITAL_ID);
                                        par.Rows.Add("i_sch", "String", Const.local_user.DB_SCHEMA);
                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                        {
                                            //Print
                                            Func.PrintFile_FromData("PHIEU_CLS_ALL", par, _typePrint); 
                                        }
                                        else
                                        {
                                            //ShowDialog
                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_CLS_ALL", par);
                                            frm.WindowState = FormWindowState.Normal;
                                            frm.StartPosition = FormStartPosition.CenterParent;
                                            frm.ShowDialog();
                                        }
                                    }

                                    if (printAll)
                                    {
                                        DataTable par = new DataTable();
                                        par.Columns.Add("name");
                                        par.Columns.Add("type");
                                        par.Columns.Add("value");
                                        par.Rows.Add("khambenhid", "String", opt.KHAMBENHID);
                                        par.Rows.Add("i_hid", "String", Const.local_user.HOSPITAL_ID);
                                        par.Rows.Add("i_sch", "String", Const.local_user.DB_SCHEMA);
                                        if (isAutoPrint || isAutoP.Split(',').Any(o => o == opt.SUBDEPTID_LOGIN))
                                        {
                                            //Print
                                            Func.PrintFile_FromData("PHIEU_CLSC", par, _typePrint); 
                                        }
                                        else
                                        {
                                            //ShowDialog
                                            frmPrint frm = new frmPrint("In phiếu", "PHIEU_CLSC", par);
                                            frm.WindowState = FormWindowState.Normal;
                                            frm.StartPosition = FormStartPosition.CenterParent;
                                            frm.ShowDialog();
                                        }
                                    }
                                }
                                finally
                                {   //Close Wait Form
                                    if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                                }
                            }
                            if (event_ListenFrm_KetQua_Thuoc_ChiDinhDV != null)
                            {
                                Form frm = this.FindForm();
                                frm.Close();
                                event_ListenFrm_KetQua_Thuoc_ChiDinhDV("Chỉ định dịch vụ thành công", null);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chỉ định dịch vụ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private DataTable setFormToObj_ThongTinBN()
        {
            DataTable dt = new DataTable();
            try
            {
                dt.Columns.Add("BACSIID");
                dt.Columns.Add("BACSIKHOACDID");
                dt.Columns.Add("BENHNHANID");
                dt.Columns.Add("BENHPHAM");
                dt.Columns.Add("BHFULL");
                dt.Columns.Add("CANTRENKTC");
                dt.Columns.Add("CHANDOAN");
                dt.Columns.Add("CHANDOAN_KEMTHEO_BD");
                dt.Columns.Add("CHANDOAN_KT");
                dt.Columns.Add("DEPTID");
                dt.Columns.Add("DIACHI");
                dt.Columns.Add("DICHVUIDKHAC");
                dt.Columns.Add("DICHVUKHAMBENHCHAID");
                dt.Columns.Add("DOITUONGBENHNHANID");
                dt.Columns.Add("DUOCVC");
                dt.Columns.Add("GHICHU_BENHCHINH");
                dt.Columns.Add("HANBHYT");
                dt.Columns.Add("HEN");
                dt.Columns.Add("HOSOBENHANID");
                dt.Columns.Add("KHAMBENHID");
                dt.Columns.Add("KHAN");
                dt.Columns.Add("KHOABNID");
                dt.Columns.Add("KHOACHIDINHCHID");
                dt.Columns.Add("LOAIGIUONG");
                dt.Columns.Add("LOAIPHIEUBP");
                dt.Columns.Add("LOAITIEPNHANID");
                dt.Columns.Add("MABENHNHAN");
                dt.Columns.Add("MACHANDOAN");
                dt.Columns.Add("MAGIUONG");
                dt.Columns.Add("MAUBENHPHAMID");
                dt.Columns.Add("MA_BHYT");
                dt.Columns.Add("NAMSINH");
                dt.Columns.Add("NGAYHANTHE");
                dt.Columns.Add("NGAYTIEPNHAN");
                dt.Columns.Add("NHOMDOITUONG");
                dt.Columns.Add("PHONGCHIDINHCHID");
                dt.Columns.Add("QUYENLOI");
                dt.Columns.Add("QUYENLOITHE");
                dt.Columns.Add("SETKHOACD");
                dt.Columns.Add("SOLUONGDICHVU");
                dt.Columns.Add("SONGAYGIUONG");
                dt.Columns.Add("SUBDEPT");
                dt.Columns.Add("TENBENHNHAN");
                dt.Columns.Add("TEN_DTBN");
                dt.Columns.Add("TGCHIDINH");
                dt.Columns.Add("THONGTINDIEUTRI");
                dt.Columns.Add("TIEPNHANID");
                dt.Columns.Add("TKCHANDOAN");
                dt.Columns.Add("TKCHANDOANKT");
                dt.Columns.Add("TRAN_BHYT");
                dt.Columns.Add("TUYEN");
                dt.Columns.Add("TYLEMIENGIAM");
                dt.Columns.Add("TYLE_THEBHYT");

                DataRow dr = dt.NewRow();
                dr["BACSIID"] = cboBACSIID.SelectValue;
                dr["BACSIKHOACDID"] = cboBACSIKHOACDID.SelectValue;
                dr["BENHNHANID"] = opt.BENHNHANID;
                dr["BENHPHAM"] = chkBENHPHAM.Checked ? "1" : "0";
                dr["BHFULL"] = hidBHFULL;
                dr["CANTRENKTC"] = hidCANTRENKTC;
                dr["CHANDOAN"] = cboChanDoan.SelectedText;
                dr["CHANDOAN_KEMTHEO_BD"] = hidCHANDOAN_KEMTHEO_BD;
                dr["CHANDOAN_KT"] = cboChanDoanKT.SelectedText;
                dr["DEPTID"] = string.IsNullOrEmpty(opt.DEPTID) ? Const.local_khoaId.ToString() : opt.DEPTID;
                dr["DIACHI"] = txtDIACHI.Text;
                dr["DICHVUIDKHAC"] = !string.IsNullOrEmpty(opt.DICHVUIDKHAC) ? opt.DICHVUIDKHAC : "0";
                dr["DICHVUKHAMBENHCHAID"] = !string.IsNullOrEmpty(opt.DICHVUKHAMBENHID) ? opt.DICHVUKHAMBENHID : "";
                dr["DOITUONGBENHNHANID"] = opt.DOITUONGBENHNHANID;
                dr["DUOCVC"] = hidDUOCVC;
                dr["GHICHU_BENHCHINH"] = txtGHICHU_BENHCHINH.Text;
                dr["HANBHYT"] = txtHANBHYT.Text;
                dr["HEN"] = appointment ? "1" : "0";
                dr["HOSOBENHANID"] = opt.HOSOBENHANID;
                dr["KHAMBENHID"] = opt.KHAMBENHID;
                dr["KHAN"] = chkKHAN.Checked ? "2" : "1";
                dr["KHOABNID"] = hidKHOABNID;
                dr["KHOACHIDINHCHID"] = cboKHOACHIDINHCHID.SelectValue;
                dr["LOAIGIUONG"] = cboLOAIGIUONG.SelectValue;
                dr["LOAIPHIEUBP"] = string.IsNullOrEmpty(opt.LOAIPHIEUMBP) ? "-1" : opt.LOAIPHIEUMBP;
                dr["LOAITIEPNHANID"] = opt.LOAITIEPNHANID;
                dr["MABENHNHAN"] = txtMABENHNHAN.Text;
                dr["MACHANDOAN"] = cboChanDoan.SelectedValue;
                dr["MAGIUONG"] = cboMAGIUONG.SelectValue;
                dr["MAUBENHPHAMID"] = cboMAUBENHPHAMID.SelectValue;
                dr["MA_BHYT"] = txtMA_BHYT.Text;
                dr["NAMSINH"] = txtNAMSINH.Text;
                dr["NGAYHANTHE"] = hidNGAYHANTHE;
                dr["NGAYTIEPNHAN"] = hidNGAYTIEPNHAN;
                dr["NHOMDOITUONG"] = hidNHOMDOITUONG;
                dr["PHONGCHIDINHCHID"] = cboPHONGCHIDINHCHID.SelectValue;
                dr["QUYENLOI"] = txtQUYENLOI.Text;
                dr["QUYENLOITHE"] = hidQUYENLOITHE;
                dr["SETKHOACD"] = isKhoabs ? "1" : "0";
                dr["SOLUONGDICHVU"] = txtSOLUONGDICHVU.Text;
                dr["SONGAYGIUONG"] = txtSONGAYGIUONG.Text;
                dr["SUBDEPT"] = string.IsNullOrEmpty(opt.SUBDEPTID) ? "-1" : opt.SUBDEPTID;
                dr["TENBENHNHAN"] = txtTENBENHNHAN.Text;
                dr["TEN_DTBN"] = txtTEN_DTBN.Text;
                dr["TGCHIDINH"] = txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1);
                dr["THONGTINDIEUTRI"] = txtTHONGTINDIEUTRI.Text;
                dr["TIEPNHANID"] = opt.TIEPNHANID;
                dr["TKCHANDOAN"] = "";
                dr["TKCHANDOANKT"] = "";
                dr["TRAN_BHYT"] = hidTRAN_BHYT;
                dr["TUYEN"] = hidTUYEN;
                dr["TYLEMIENGIAM"] = hidTYLEMIENGIAM;
                dr["TYLE_THEBHYT"] = hidTYLE_THEBHYT;
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        private bool validate()
        {
            if (string.IsNullOrEmpty(txtTGCHIDINH.Text))
            {
                MessageBox.Show("Thời gian chỉ định không được để trống");
                return false;
            }

            if (string.IsNullOrEmpty(opt.CHIDINHDICHVU) && string.IsNullOrEmpty(opt.MODEFUNCTION) && config == "1")
            {
                if (string.IsNullOrEmpty(cboChanDoan.SelectedValue))
                {
                    MessageBox.Show("Chuẩn đoán không được để trống");
                    return false;
                }
            }

            if (isKhoabs)
            {
                if (string.IsNullOrEmpty(cboKHOACHIDINHCHID.SelectValue) || string.IsNullOrEmpty(cboBACSIKHOACDID.SelectValue)
                    || string.IsNullOrEmpty(cboPHONGCHIDINHCHID.SelectValue))
                {
                    MessageBox.Show("Chưa chọn khoa chỉ định, phòng, bác sĩ chỉ định");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(opt.CHIDINHDICHVU) && isCbQuaTg && txtTGCHIDINH.DateTime > currentTime)
            {
                MessageBox.Show("Thời gian chỉ định lớn hơn thời gian hiện tại");
                return false;
            }

            if (string.IsNullOrEmpty(opt.CHIDINHDICHVU) && isBacsiKe && string.IsNullOrEmpty(cboBACSIID.SelectValue))
            {
                MessageBox.Show("Chưa chọn bác sĩ chỉ định");
                return false;
            }

            if (string.IsNullOrEmpty(opt.CHIDINHDICHVU) && isCPdt && string.IsNullOrEmpty(cboMAUBENHPHAMID.SelectValue))
            {
                MessageBox.Show("Chưa chọn phiếu điều trị");
                return false;
            }

            if (DateTime.ParseExact(hidNGAYTIEPNHAN, Const.FORMAT_datetime1, culture) > txtTGCHIDINH.DateTime)
            {
                MessageBox.Show("Ngày chỉ định không được nhỏ hơn thời gian nhập viện");
                return false;
            }

            if (isHienThiMaGiuong && isChanMg)
            {
                if (string.IsNullOrEmpty(cboLOAIGIUONG.SelectValue))
                {
                    MessageBox.Show("Chưa chọn loại giường");
                    return false;
                }
                if (string.IsNullOrEmpty(cboMAGIUONG.SelectValue))
                {
                    MessageBox.Show("Chưa chọn mã giường");
                    return false;
                }
            }
            return true;
        }

        private void SendRequestToLab(string soPhieu, string mauBenhPhamId, string barCode)
        {
            try
            {
                string requestUrl = LISConnector.LIS_SERVICE_DOMAIN_NAME + LISConnector.LIS_SEND_REQUEST;
                LabRequestSet request = new LabRequestSet();
                if (!string.IsNullOrEmpty(LISConnector.LIS_AUTHENTICATION_GATE))
                {
                    request = LISConnector.CreateLabRequest(mauBenhPhamId);
                    if (LISConnector.LIS_CONNECTION_TYPE == "1" || LISConnector.LIS_CONNECTION_TYPE == "2"
                        && !string.IsNullOrEmpty(LISConnector.LIS_SERVICE_DOMAIN_NAME))
                    {
                        HttpWebRequest webRequest = null;
                        HttpWebResponse webResponse = null;
                        var responseValue = String.Empty;

                        webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/json; charset=utf-8";
                        webRequest.Headers.Add("Username", LISConnector.LIS_USERNAME);
                        webRequest.Headers.Add("Identify-Code", soPhieu);
                        webRequest.Headers.Add("SID", soPhieu);
                        webRequest.Headers.Add("Token", LISConnector.GetLabToken());
                        var encoding = new UTF8Encoding();
                        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                        var bytes = Encoding.GetEncoding("UTF-8").GetBytes(jsonData);
                        webRequest.ContentLength = bytes.Length;
                        using (var writeStream = webRequest.GetRequestStream())
                        {
                            writeStream.Write(bytes, 0, bytes.Length);
                        }

                        using (webResponse = (HttpWebResponse)webRequest.GetResponse())
                        {
                            if (webResponse.StatusCode != HttpStatusCode.OK)
                            {
                                var message = String.Format("Request failed. Received HTTP {0}", webResponse.StatusCode);
                                throw new ApplicationException(message);
                            }

                            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                            using (var responseStream = webResponse.GetResponseStream())
                            {
                                if (responseStream != null)
                                    LISConnector.RefreshLabToken();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            frm.Close();
        }

        private void btnCLEARCHANDOANKT_Click(object sender, EventArgs e)
        {
            cboChanDoanKT.SelectedText = "";
            cboChanDoanKT.searchLookUpEdit.Text = "";
        }


        private void btnEDITCHANDOANKT_Click(object sender, EventArgs e)
        {
            VNPT.HIS.UserControl.SubForm.NGT02K052_ChinhSuaBenhPhu frm = new VNPT.HIS.UserControl.SubForm.NGT02K052_ChinhSuaBenhPhu(
                cboChanDoanKT.searchLookUpEdit.Properties.DataSource, "ICD10CODE", "ICD10NAME", "Mã bệnh", "Tên bệnh", cboChanDoanKT.SelectedValue);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.setReturnData(ReturnData_NGT02K052_ChinhSuaBenhPhu);
            frm.ShowDialog();
        }

        private void ReturnData_NGT02K052_ChinhSuaBenhPhu(object sender, EventArgs e)
        {
            Dictionary<string, string> returnList = (Dictionary<string, string>)sender;
            btnCLEARCHANDOANKT_Click(null, null);
            foreach (string key in returnList.Keys)
            {
                cboChanDoanKT.searchLookUpEdit1.EditValue = key;
            }
            cboChanDoanKT.searchLookUpEdit1.EditValue = "";
        }
        #endregion

        #region XỬ LÝ CÁC BUTTON LƯU MẪU, PHIẾU MẪU, PHÁC ĐỒ ĐIỀU TRỊ
        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTEXT_TEMP.Text))
            {
                DoSavePhieuMau();
            }
            else
            {
                txtTEXT_TEMP.Focus();
                MessageBox.Show("Bạn phải nhập tên phiếu mẫu!");
            }
        }

        private void DoSavePhieuMau()
        {
            if (gridView4.DataRowCount > 0)
            {
                PhieuMau jsonPhieuMau = new PhieuMau();
                jsonPhieuMau.TEN_MAUPHIEU = txtTEXT_TEMP.Text.Trim();
                jsonPhieuMau.KHAMBENHID = opt.KHAMBENHID;
                jsonPhieuMau.LOAINHOM_MAU = _loainhom_mau;
                jsonPhieuMau.DS_DICHVU = (DataTable)gridDSCD.DataSource;
                string strJson = JsonConvert.SerializeObject(jsonPhieuMau).Replace("\"", "\\\"");
                string ret = ServiceChiDinhDichVu.ajaxCALL_SP_S("NGT02K016.LUUMAU", strJson);
                if (ret == "1")
                {
                    MessageBox.Show("Tạo phiếu mẫu thành công!");
                }
                else if (ret == "0")
                {
                    txtTEXT_TEMP.Focus();
                    MessageBox.Show("Đã tồn tại tên phiếu mẫu này!");
                }
                else
                {
                    MessageBox.Show("Tạo phiếu mẫu không thành công!");
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn dịch vụ chỉ định trước!");
            }
        }

        private void btnPhieuMau_Click(object sender, EventArgs e)
        {
            NGT02K041_PhieuChiDinhMau frm = new NGT02K041_PhieuChiDinhMau();
            frm.initData(_loainhom_mau);
            //temp_presc_success
            frm.raiseEvent(listenFrmPhieuMau);
            //frm.ShowDialog();
            frm.Show(this);
        }

        private void listenFrmPhieuMau(object sender, EventArgs e)
        {
            string idPhieuMau = (string)sender;
            if (!string.IsNullOrEmpty(idPhieuMau))
            {
                string param = idPhieuMau + "$" + opt.KHAMBENHID + "$" + txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1);
                string ret = ServiceChiDinhDichVu.ajaxCALL_SP_S("NGT02K016.EV001", param);
                if (!string.IsNullOrEmpty(ret))
                {
                    DialogResult dialogResult = MessageBox.Show(ret + " đã được chỉ định trong ngày, có đồng ý load mẫu dịch vụ?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OnSelectRow(gridXetNghiem.gridView, 0, true, true, idPhieuMau);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    OnSelectRow(gridXetNghiem.gridView, 0, true, true, idPhieuMau);
                }
            }
        }

        private void btnPhacDo_Click(object sender, EventArgs e)
        {
            NTU02D075_PhacDoMau frm = new NTU02D075_PhacDoMau();
            Dictionary<string, string> myVar = new Dictionary<string, string>();
            myVar.Add("maChanDoan", cboChanDoan.SelectedValue);
            myVar.Add("loaiDv", "0");
            frm.initData(myVar);
            frm.raiseEvent(listenFrmPhacDo);
            //frm.ShowDialog();
            frm.Show(this);
        }

        private void listenFrmPhacDo(object sender, EventArgs e)
        {
            string id = (string)sender;
            if (!string.IsNullOrEmpty(id))
            {
                string param = id + "$" + opt.KHAMBENHID + "$" + txtTGCHIDINH.DateTime.ToString(Const.FORMAT_datetime1);
                string ret = ServiceChiDinhDichVu.ajaxCALL_SP_S("NTU02D075.EV003", param);
                if (!string.IsNullOrEmpty(ret))
                {
                    DialogResult dialogResult = MessageBox.Show(ret + " đã được chỉ định trong ngày, có đồng ý load phác đồ mẫu không có chỉ định trùng?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        OnSelectRow(gridXetNghiem.gridView, 0, true, true, id, "2");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    OnSelectRow(gridXetNghiem.gridView, 0, true, true, id, "2");
                }
            }
        }
        #endregion

        #region CHUỘT PHẢI VÀ CÁC HÀM XỬ LÝ TRONG GRIDDSCD
        List<DXMenuItem> menuItems = new List<DXMenuItem>();
        private void gridView4_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                menuItems = new List<DXMenuItem>();
                addMennuItem("Xóa dịch vụ", "delService", "trash.png");
                addMennuItem("Chuyển BHYT", "changeBHYT", "pencil.png");
                addMennuItem("Chuyển BHYT+Dịch vụ", "changeBHYTYC", "pencil.png");
                addMennuItem("Chuyển Viện phí", "changeVP", "pencil.png");
                addMennuItem("Chuyển Viện phí+Dịch vụ", "changeVPYC", "pencil.png");
                addMennuItem("Chuyển Dịch vụ", "changeYC", "pencil.png");

                foreach (DXMenuItem item in menuItems)
                    e.Menu.Items.Add(item);
            }
        }

        private void addMennuItem(string text, string tag, string icon)
        {
            DXMenuItem btn = new DXMenuItem(text, ItemMenuPopup_Click);
            btn.Tag = tag;
            btn.Image = Func.getIcon(icon);
            btn.Appearance.Font = Const.fontDefault;
            menuItems.Add(btn);
        }

        private void ItemMenuPopup_Click(object sender, EventArgs e)
        {
            DXMenuItem btn = (DXMenuItem)sender;
            if (btn.Tag.Equals("delService"))
            {
                delServiceGrid();
            }
            else if (btn.Tag.Equals("changeBHYT"))
            {
                changeObject("1");
            }
            else if (btn.Tag.Equals("changeBHYTYC"))
            {
                changeObject("2");
            }
            else if (btn.Tag.Equals("changeVP"))
            {
                changeObject("4");
            }
            else if (btn.Tag.Equals("changeVPYC"))
            {
                changeObject("11");
            }
            else if (btn.Tag.Equals("changeYC"))
            {
                changeObject("6");
            }
        }

        private void delServiceGrid()
        {
            try
            {
                DataRowView drv = (DataRowView)(gridView4.GetFocusedRow());
                if (drv != null)
                {
                    LoadPay(-float.Parse(drv["THANH_TIEN"].ToString(), culture) - float.Parse(drv["BHYT_TRA"].ToString(), culture),
                        -float.Parse(drv["BHYT_TRA"].ToString(), culture));
                    if (opt.DOITUONGBENHNHANID == "1" && float.Parse(drv["BHYT_TRA"].ToString(), culture) > 0)
                    {
                        tongbh = tongbh - float.Parse(drv["BHYT_TRA"].ToString(), culture) - float.Parse(drv["THANH_TIEN"].ToString(), culture);
                        int rowIds = gridView4.DataRowCount;
                        for (int i1 = 0; i1 < rowIds; i1++)
                        {
                            DataRowView rowData = (DataRowView)gridView4.GetRow(i1);
                            if (rowData["LOAIDOITUONG"].ToString() == "1" || rowData["LOAIDOITUONG"].ToString() == "2")
                            {
                                if (tongbh > (string.IsNullOrEmpty(hidTRAN_BHYT) ? float.Parse(hidTRAN_BHYT) : 0))
                                {
                                    gridView4.SetRowCellValue(i1, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFINAL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i1, "THANH_TIEN", float.Parse(rowData["THANH_TIENFINAL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i1, "MIENGIAM", float.Parse(rowData["MIENGIAMFINAL"].ToString()).ToString("N2", culture));
                                    LoadPay(0, float.Parse(rowData["BHYT_TRAFINAL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                }
                                else
                                {
                                    gridView4.SetRowCellValue(i1, "BHYT_TRA", float.Parse(rowData["BHYT_TRAFULL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i1, "THANH_TIEN", float.Parse(rowData["THANH_TIENFULL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i1, "MIENGIAM", float.Parse(rowData["MIENGIAMFULL"].ToString()).ToString("N2", culture));
                                    LoadPay(0, float.Parse(rowData["BHYT_TRAFULL"].ToString()) - float.Parse(rowData["BHYT_TRA"].ToString(), culture));
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(opt.CHIDINHDICHVU))
                    {
                        loadGridAfterRemove(gridDichVuKhac.gridView, drv["DICHVUID"].ToString());
                    }
                    else
                    {
                        loadGridAfterRemove(gridXetNghiem.gridView, drv["DICHVUID"].ToString());
                        loadGridAfterRemove(gridCDHA.gridView, drv["DICHVUID"].ToString());
                        loadGridAfterRemove(gridPTTT.gridView, drv["DICHVUID"].ToString());
                    }
                    dvIdReds.Remove(drv["DICHVUID"].ToString());
                    gridView4.DeleteRow(gridView4.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        // UnSelect dòng bị xóa trên grid
        private void loadGridAfterRemove(GridView gridView, string dichVuId)
        {
            try
            {
                int[] rowIds = gridView.GetSelectedRows();
                for (int i = 0; i < rowIds.Length; i++)
                {
                    DataRowView rowData = (DataRowView)gridView.GetRow(rowIds[i]);
                    if (dichVuId.Equals(rowData["DICHVUID"].ToString()))
                    {
                        gridView.UnselectRow(rowIds[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        List<string> dvIdReds = new List<string>();
        private void changeObject(string loaiDt)
        {
            try
            {
                if (opt.LOAIDICHVU == "19") return;
                int rowId = gridView4.FocusedRowHandle;
                float _insurance = 0;
                float _insurance_full = 0;
                float _insurance_final = 0;

                DataRowView rowData = (DataRowView)(gridView4.GetFocusedRow());
                if (rowData != null)
                {
                    if (((opt.DOITUONGBENHNHANID.Equals("1") && loaiDt.Equals("1")) || (opt.DOITUONGBENHNHANID.Equals("2") && loaiDt.Equals("4"))
                        || (opt.DOITUONGBENHNHANID.Equals("3") && loaiDt.Equals("6"))) && string.IsNullOrEmpty(rowData["LOAI_DT_MOI"].ToString())
                        && string.IsNullOrEmpty(rowData["LOAITT_MOI"].ToString()))
                    {
                        MessageBox.Show("Không thể chuyển loại thanh toán trùng với loại của đối tượng bệnh nhân");
                        return;
                    }
                    if ((loaiDt.Equals("1") || loaiDt.Equals("2")) &&
                        (string.IsNullOrEmpty(rowData["GIABHYT"].ToString()) || float.Parse(rowData["GIABHYT"].ToString()) <= 0))
                    {
                        MessageBox.Show("Không thể chuyển loại đối tượng khi giá bảo hiểm bằng 0");
                        return;
                    }
                    if ((loaiDt.Equals("4") || loaiDt.Equals("11")) &&
                        (string.IsNullOrEmpty(rowData["GIANHANDAN"].ToString()) || float.Parse(rowData["GIANHANDAN"].ToString()) <= 0))
                    {
                        MessageBox.Show("Không thể chuyển loại đối tượng khi giá nhân dân bằng 0");
                        return;
                    }
                    if (loaiDt.Equals("6") &&
                        (string.IsNullOrEmpty(rowData["GIADICHVU"].ToString()) || float.Parse(rowData["GIADICHVU"].ToString()) <= 0))
                    {
                        MessageBox.Show("Không thể chuyển loại đối tượng khi giá dịch vụ bằng 0");
                        return;
                    }

                    if (opt.DOITUONGBENHNHANID.Equals("1") && float.Parse(rowData["GIABHYT"].ToString()) > 0)
                    {
                        if (rowData["LOAIDOITUONG"].ToString().Equals("1") || rowData["LOAIDOITUONG"].ToString().Equals("2"))
                        {
                            if (loaiDt.Equals("4") || loaiDt.Equals("11") || loaiDt.Equals("6"))
                            {
                                tongbh = tongbh - float.Parse(rowData["BHYT_TRA"].ToString(), culture) - float.Parse(rowData["THANH_TIEN"].ToString(), culture);
                            }
                        }
                        if (!rowData["LOAIDOITUONG"].ToString().Equals("1") || !rowData["LOAIDOITUONG"].ToString().Equals("2"))
                        {
                            if (loaiDt.Equals("1") || loaiDt.Equals("2"))
                            {
                                tongbh = tongbh + float.Parse(rowData["BHYT_TRA"].ToString(), culture) + float.Parse(rowData["THANH_TIEN"].ToString(), culture);
                            }
                        }
                        if (tongbh > float.Parse(hidTRAN_BHYT ?? "0"))
                        {
                            _insurance = !string.IsNullOrEmpty(hidQUYENLOITHE) ? float.Parse(hidQUYENLOITHE) : float.Parse(txtQUYENLOI.Text);
                        }
                        else
                        {
                            _insurance = !string.IsNullOrEmpty(hidQUYENLOI) ? float.Parse(hidQUYENLOI) : float.Parse(txtQUYENLOI.Text);
                        }
                        _insurance_full = !string.IsNullOrEmpty(hidQUYENLOI) ? float.Parse(hidQUYENLOI) : float.Parse(txtQUYENLOI.Text);
                        _insurance_final = !string.IsNullOrEmpty(hidQUYENLOITHE) ? float.Parse(hidQUYENLOITHE) : float.Parse(txtQUYENLOI.Text);


                        for (int i = 0; i < gridView4.DataRowCount; i++)
                        {
                            DataRowView drv = (DataRowView)gridView4.GetRow(i);
                            if ((drv["LOAIDOITUONG"].ToString() == "1" || drv["LOAIDOITUONG"].ToString() == "2") && i != rowId)
                            {
                                if (tongbh > (string.IsNullOrEmpty(hidTRAN_BHYT) ? float.Parse(hidTRAN_BHYT) : 0))
                                {
                                    gridView4.SetRowCellValue(i, "BHYT_TRA", float.Parse(drv["BHYT_TRAFINAL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "THANH_TIEN", float.Parse(drv["THANH_TIENFINAL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "MIENGIAM", float.Parse(drv["MIENGIAMFINAL"].ToString()).ToString("N2", culture));
                                    LoadPay(0, float.Parse(drv["BHYT_TRAFINAL"].ToString()) - float.Parse(drv["BHYT_TRA"].ToString(), culture));
                                }
                                else
                                {
                                    gridView4.SetRowCellValue(i, "BHYT_TRA", float.Parse(drv["BHYT_TRAFULL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "THANH_TIEN", float.Parse(drv["THANH_TIENFULL"].ToString()).ToString("N2", culture));
                                    gridView4.SetRowCellValue(i, "MIENGIAM", float.Parse(drv["MIENGIAMFULL"].ToString()).ToString("N2", culture));
                                    LoadPay(0, float.Parse(drv["BHYT_TRAFULL"].ToString()) - float.Parse(drv["BHYT_TRA"].ToString(), culture));
                                }
                            }
                        }
                    }

                    DataTable r = new DataTable();
                    DataTable r_full = new DataTable();
                    DataTable r_final = new DataTable();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("DOITUONGBENHNHANID", typeof(String));
                    dt.Columns.Add("MUCHUONG", typeof(String));
                    dt.Columns.Add("GIATRANBH", typeof(String));
                    dt.Columns.Add("GIABHYT", typeof(String));
                    dt.Columns.Add("GIAND", typeof(String));
                    dt.Columns.Add("GIADV", typeof(String));
                    dt.Columns.Add("GIANN", typeof(String));
                    dt.Columns.Add("DOITUONGCHUYEN", typeof(String));
                    dt.Columns.Add("GIADVKTC", typeof(String));
                    dt.Columns.Add("MANHOMBHYT", typeof(String));
                    dt.Columns.Add("SOLUONG", typeof(String));
                    dt.Columns.Add("CANTRENDVKTC", typeof(String));
                    dt.Columns.Add("THEDUTHOIGIAN", typeof(String));
                    dt.Columns.Add("DUOCVANCHUYEN", typeof(String));
                    dt.Columns.Add("TYLETHUOCVATTU", typeof(String));
                    dt.Columns.Add("NHOMDOITUONG", typeof(String));
                    dt.Columns.Add("NGAYHANTHE", typeof(String));
                    dt.Columns.Add("NGAYDICHVU", typeof(String));
                    dt.Columns.Add("TYLE_MIENGIAM", typeof(String));
                    dt.Columns.Add("TYLEDV", typeof(String));
                    DataRow dr = dt.NewRow();

                    dr["DOITUONGBENHNHANID"] = opt.DOITUONGBENHNHANID;
                    if (opt.LOAIDICHVU == "14" && isTranspost)
                    {
                        dr["MUCHUONG"] = hidTYLE_THEBHYT;
                    }
                    else
                    {
                        dr["MUCHUONG"] = _insurance;
                    }
                    dr["GIATRANBH"] = rowData.DataView.Table.Columns.Contains("DICHVU_BHYT_DINHMUC") ? rowData["DICHVU_BHYT_DINHMUC"].ToString() : "";
                    dr["GIABHYT"] = rowData["GIABHYT"].ToString();
                    dr["GIAND"] = rowData["GIANHANDAN"].ToString();
                    dr["GIADV"] = rowData["GIADICHVU"].ToString();
                    dr["GIANN"] = "0";
                    dr["DOITUONGCHUYEN"] = loaiDt;
                    dr["GIADVKTC"] = (opt.DOITUONGBENHNHANID == "1" && rowData.DataView.Table.Columns.Contains("GIA_DVC") && float.Parse(rowData["GIA_DVC"].ToString()) > 0)
                        ? rowData["GIA_DVC"].ToString() : "0";
                    dr["MANHOMBHYT"] = rowData["MANHOM_BHYT"].ToString();
                    dr["SOLUONG"] = rowData.DataView.Table.Columns.Contains("SOLUONG") ? rowData["SOLUONG"].ToString() : "1";
                    dr["CANTRENDVKTC"] = hidCANTRENKTC;
                    dr["THEDUTHOIGIAN"] = hidBHFULL;
                    dr["DUOCVANCHUYEN"] = hidDUOCVC;
                    dr["TYLETHUOCVATTU"] = "100";
                    dr["NHOMDOITUONG"] = hidNHOMDOITUONG;
                    dr["NGAYHANTHE"] = hidNGAYHANTHE;
                    dr["NGAYDICHVU"] = !string.IsNullOrEmpty(txtTGCHIDINH.Text) ? txtTGCHIDINH.DateTime.ToString(Const.FORMAT_date1) : "";
                    dr["TYLE_MIENGIAM"] = hidTYLEMIENGIAM;
                    if (opt.LOAIDICHVU == "13")
                    {
                        dr["TYLEDV"] = rowData["TYLEDV"].ToString();
                    }
                    else
                    {
                        if (rowData.DataView.Table.Columns.Contains("LOAIPTTT") && !string.IsNullOrEmpty(rowData["LOAIPTTT"].ToString()))
                        {
                            if (rowData.DataView.Table.Columns.Contains("TYLEDVTEMP") && !string.IsNullOrEmpty(rowData["TYLEDVTEMP"].ToString()))
                            {
                                dr["TYLEDV"] = rowData["TYLEDVTEMP"].ToString();
                            }
                            else
                            {
                                dr["TYLEDV"] = "1";
                            }
                        }
                        else
                        {
                            dr["TYLEDV"] = "1";
                        }
                    }
                    dt.Rows.InsertAt(dr, 0);
                    DataTable dtFull = dt.Copy();
                    DataTable dtFinal = dt.Copy();

                    // function vien phi
                    r = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dt);
                    dtFull.Rows[0]["MUCHUONG"] = _insurance_full;
                    r_full = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtFull);
                    if (opt.LOAIDICHVU == "14" && isTranspost)
                    {
                        dtFinal.Rows[0]["MUCHUONG"] = hidTYLE_THEBHYT;
                    }
                    else
                    {
                        dtFinal.Rows[0]["MUCHUONG"] = _insurance_final;
                    }
                    r_final = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtFinal);

                    if (rowData["LOAIDOITUONG"].ToString().Equals(r.Rows[0]["loai_dt"].ToString())
                        && !string.IsNullOrEmpty(rowData["LOAI_DT_MOI"].ToString())
                        && r.Columns.Contains("loai_dt_moi") && !string.IsNullOrEmpty(r.Rows[0]["loai_dt_moi"].ToString())
                        && rowData["LOAI_DT_MOI"].ToString().Equals(r.Rows[0]["loai_dt_moi"].ToString()))
                    {
                        MessageBox.Show("Không thể chuyển loại thanh toán trùng với loại của đối tượng bệnh nhân");
                        return;
                    }

                    if (r.Rows[0]["bh_tra"].ToString() != "-1" && r.Rows[0]["nd_tra"].ToString() != "-1")
                    {
                        var _oldBHYTDataRow = rowData["BHYT_TRA"].ToString();
                        var _oldTTDataRow = rowData["THANH_TIEN"].ToString();

                        var _newBHYTDataRow = float.Parse(r.Rows[0]["bh_tra"].ToString());
                        var _newTTDataRow = float.Parse(r.Rows[0]["tong_cp"].ToString());

                        var _payBHYTChange = _newBHYTDataRow - float.Parse(_oldBHYTDataRow, culture);
                        var _payTTChange = _newTTDataRow - float.Parse(_oldTTDataRow, culture)
                                - float.Parse(_oldBHYTDataRow, culture);
                        LoadPay(_payTTChange, _payBHYTChange);

                        if (r.Rows[0]["loai_dt"].ToString() == "4" || r.Rows[0]["loai_dt"].ToString() == "6")
                        {
                            dvIdReds.Add(rowData["DICHVUID"].ToString());
                            gridView4.RefreshRow(rowId);
                        }
                        gridView4.SetFocusedRowCellValue("GIA_TIEN",
                            (float.Parse(r.Rows[0]["tong_cp"].ToString()) / float.Parse(rowData.DataView.Table.Columns.Contains("SOLUONG") ? rowData["SOLUONG"].ToString() : "1")
                            + float.Parse(r.Columns.Contains("nd_tra_chenh") ? r.Rows[0]["nd_tra_chenh"].ToString() : "0")).ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("BHYT_TRA", float.Parse(r.Rows[0]["bh_tra"].ToString()).ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("MIENGIAM", float.Parse(!string.IsNullOrEmpty(r.Rows[0]["mien_giam"].ToString()) ? r.Rows[0]["mien_giam"].ToString() : "0").ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("THANH_TIEN", float.Parse(r.Rows[0]["nd_tra"].ToString()).ToString("N2", culture));
                        gridView4.SetFocusedRowCellValue("BHYT_TRAFINAL", !string.IsNullOrEmpty(r_final.Rows[0]["bh_tra"].ToString()) ? r_final.Rows[0]["bh_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("MIENGIAMFINAL", !string.IsNullOrEmpty(r_final.Rows[0]["mien_giam"].ToString()) ? r_final.Rows[0]["mien_giam"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("THANH_TIENFINAL", !string.IsNullOrEmpty(r_final.Rows[0]["nd_tra"].ToString()) ? r_final.Rows[0]["nd_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("BHYT_TRAFULL", !string.IsNullOrEmpty(r_full.Rows[0]["bh_tra"].ToString()) ? r_full.Rows[0]["bh_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("MIENGIAMFULL", !string.IsNullOrEmpty(r_full.Rows[0]["mien_giam"].ToString()) ? r_full.Rows[0]["mien_giam"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("THANH_TIENFULL", !string.IsNullOrEmpty(r_full.Rows[0]["nd_tra"].ToString()) ? r_full.Rows[0]["nd_tra"].ToString() : "0");
                        gridView4.SetFocusedRowCellValue("GIA_CHENH", r.Columns.Contains("nd_tra_chenh") ? r.Rows[0]["nd_tra_chenh"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("LOAI_DT_MOI", r.Columns.Contains("loai_dt_moi") ? r.Rows[0]["loai_dt_moi"].ToString() : "");
                        gridView4.SetFocusedRowCellValue("LOAITT_MOI", r.Columns.Contains("ten_loai_tt_moi") ? r.Rows[0]["ten_loai_tt_moi"].ToString() : "");
                        var nd_chenh_temp = r.Columns.Contains("nd_tra_chenh") ? float.Parse(r.Rows[0]["nd_tra_chenh"].ToString()) : 0;
                        gridView4.SetFocusedRowCellValue("TONGTIEN", float.Parse(r.Rows[0]["nd_tra"].ToString()) + nd_chenh_temp);
                    }
                    else
                    {
                        MessageBox.Show("Không thể chuyển loại thanh toán vừa chọn cho đối tượng bệnh nhân này");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridView4_RowStyle(object sender, RowStyleEventArgs e)
        {
            for (int i = 0; i < gridView4.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)gridView4.GetRow(i);
                if (dvIdReds.Contains(rowData["DICHVUID"].ToString()) && e.RowHandle == i)
                {
                    e.Appearance.ForeColor = Color.Red;
                    //e.Appearance.BackColor = Color.LightGreen;
                    e.HighPriority = true;
                }
            }
        }
        #endregion

        // Hàm gửi sự kiện đến form chứa ucChiDinhDv
        protected EventHandler event_ListenFrm_KetQua_Thuoc_ChiDinhDV;
        public void setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(EventHandler eventChangeValue)
        {
            event_ListenFrm_KetQua_Thuoc_ChiDinhDV = eventChangeValue;
        }
    }
}