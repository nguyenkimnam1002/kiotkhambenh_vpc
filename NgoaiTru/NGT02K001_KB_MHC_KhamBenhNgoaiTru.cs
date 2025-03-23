using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.CommonForm;
using VNPT.HIS.Common;
using System.Reflection;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Threading;

// 2 Biến Const.drvBenhNhan; Const.drvBenhNhan_ChiTiet được gán trong ucTabDanhSachBenhNhan khi mỗi lần click chọn BN trong danh sách.
//(Const.drvBenhNhan còn đc gán trong form Tiếp nhận khi chọn 1 BN trong tab danh sách)
//  Const.drvBenhNhan =	RN: 1,	PHONGKHAMDANGKYID: 1995,	SOTHUTU: 0001,	TRANGTHAI_CLS: null, 	TRANGTHAI_STT: 4,	DASANSANG: 0,	KHAMBENHID: 2635,
//	    DATHUTIENKHAM: null,	DAGIUTHEBHYT: 1,	TRANGTHAIKHAMBENH: 4,	UUTIENKHAMID: 0,	DOITUONGBENHNHANID: 1,	LANGOI: 0,	ORD: 0,	YEUCAUKHAM: 7-Khám Nội,
//      HINHTHUCVAOVIENID: 3,	MAHOADULIEU: 0,	BENHNHANID: 2987,	MAHOSOBENHAN: BA00000096,  	MABENHNHAN: BN00000090,	HOSOBENHANID: 3045,	XUTRIKHAMBENHID: 1,
//	    TENTRANGTHAIKB: Đang khám, TENBENHNHAN: TET 2305,	MA_BHYT: TE1350100000178,	MA_KCBBD: 35001,	TIEPNHANID: 2963,	LOAITIEPNHANID: 1,	KQCLS: null,
//	    MADICHVU: 02.1896,	SSS: 1
//  Const.drvBenhNhan_ChiTiet = MABENHNHAN: BN00000090, TENBENHNHAN: TET 2305, PHONGDK: 219. Phòng khám Nội tiết, NGAYSINH: 03/11/2015, NAMSINH: 2015, DANTOC: Kinh, 
//	    QUOCGIA: Việt Nam, DIACHI: Phường Minh Khai-Thành Phố Phủ Lý-Hà Nam, TENNGHENGHIEP: Trẻ em, NGAYRAVIEN: 23/05/2017 00:00:00, 
//	    DENKHAMLUC: 23/05/2017 00:34:11 -> 23/05/2017 00:00:00, NOILAMVIEC: , GIOITINH: Nữ, DOITUONG: BHYT, KCBBD: 35001, SOTHEBHYT: TE1350100000178, 
//	    BHYTDEN: 02/11/2021, BAOTINCHO: 1-, YEUCAUKHAM: 7-Khám Nội, PHONGKHAM: 219. Phòng khám Nội tiết, XUTRI: Cấp toa cho về, TUYEN: Đúng tuyến, NGAYTN: 201705230034, 
//	    LOAIBENHAN: Khám bệnh, CDTD: , ANHBENHNHAN: , CDC: Bệnh tả do Vibrio cholerae 01, typ sinh học eltor, CDP: A19.8-Lao kê khác, MAKHAMBENH: KB000000131, 
//	    MATIEPNHAN: TN000000116, MABENHAN: BA00000096, DUYETKETOAN: 0, DUYETBH: 0, BTNTHUOC: 1, TRANGTHAI_STT: 4, SLXN: 0, SLCDHA: 0, SLCHUYENKHOA: 0, SLTHUOC: 2, 
//	    SLVATTU: 0, SLVANCHUYEN: 0, CONGKHAM: 1, DICHVUID: 400035, PHONGID: 4126
namespace VNPT.HIS.NgoaiTru
{ 
    public partial class NGT02K001_KB_MHC_KhamBenhNgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }


        public NGT02K001_KB_MHC_KhamBenhNgoaiTru()
        {
            InitializeComponent();
        }
        string lbPara = "#";
        private string getPara(string name)
        {//  &kbtraingay=1&x=2&
            string ret = "";
            try
            {
                if (lbPara == "#")
                {
                    Control[] control = this.Controls.Find("lbPara", false);
                    if (control.Length > 0)
                    {
                        if (control[0].GetType().ToString() == "DevExpress.XtraEditors.LabelControl")
                        {
                            DevExpress.XtraEditors.LabelControl lbControl = (DevExpress.XtraEditors.LabelControl)control[0];
                            lbPara = "&" + lbControl.Text + "&";
                        }
                    }
                }

                if (lbPara.IndexOf("&" + name) > -1)
                {
                    string temp = lbPara.Substring(lbPara.IndexOf("&" + name) + ("&" + name).Length + 1);
                    if (temp.IndexOf("&") > -1) ret = temp.Substring(0, temp.IndexOf("&"));
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }

            return ret;
        }

        string LNMBP_XetNghiem = "1";
        string LNMBP_CDHA = "2";
        string LNMBP_ChuyenKhoa = "5";
        string LNMBP_DieuTri = "4";
        string LNMBP_Phieuvattu = "8";
        string LNMBP_Phieuthuoc = "7";
        string LNMBP_PhieuVanChuyen = "16";

        string _flgModeView = "0";
        string _hosobenhanid = "";
        string _deleteDV = "";
        string _checkLoad = "";
        string _studyInstanceUID = "";

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (tabPane1.SelectedPage == tabDSTiepNhan)
            {
                ucTabNGT02K078_DSTN1.loadData();
            }
            else
            {
                if (Const.drvBenhNhan != null)
                {
                    string KHAMBENHID = Const.drvBenhNhan["KHAMBENHID"].ToString();
                    getChiTiet_KhamBenh(KHAMBENHID); // để lấy BENHNHANID

                    if (dtKHAMBENH.Rows.Count > 0)
                    {
                        string BENHNHANID = dtKHAMBENH.Rows[0]["BENHNHANID"].ToString();  // đc  BENHNHANID

                        if (tabPane1.SelectedPage == tabPageXetNghiem)
                            ucTabXetNghiem1.loadData_2(KHAMBENHID, BENHNHANID, LNMBP_XetNghiem, _flgModeView, "", "", "");

                        else if (tabPane1.SelectedPage == tabPageCDHA)
                            ucTabCDHA1.loadData_2(KHAMBENHID, BENHNHANID, LNMBP_CDHA, _flgModeView, "", "", "", "");

                        else if (tabPane1.SelectedPage == tabPagePhauThuat)
                            ucTabPhauThuatThuThuat1.loadData_2(KHAMBENHID, BENHNHANID, LNMBP_ChuyenKhoa, _flgModeView, "", "", "", "");

                        else if (tabPane1.SelectedPage == tabPageBenhAn)
                            ucTabBenhAn1.loadData(KHAMBENHID);

                        else if (tabPane1.SelectedPage == tabPageThuoc)
                            ucTabThuoc1.loadData(KHAMBENHID, BENHNHANID, LNMBP_Phieuthuoc, _flgModeView, "");

                        else if (tabPane1.SelectedPage == tabPageVatTu)
                            ucTabVatTu1.loadData(KHAMBENHID, BENHNHANID, LNMBP_Phieuvattu, _flgModeView, "");

                        else if (tabPane1.SelectedPage == tabPagePhieuVanChuyen)
                            ucTabPhieuVanChuyen1.loadData(KHAMBENHID, BENHNHANID, LNMBP_PhieuVanChuyen, _flgModeView, "", "14");

                        else if (tabPane1.SelectedPage == tabPageVienPhi)
                            ucTabVienPhi1.loadData(Const.drvBenhNhan["KHAMBENHID"].ToString(), Const.drvBenhNhan["TIEPNHANID"].ToString());

                    }



                }
            }
        }
        string saveKHAMBENHID = "";
        private DataTable dtKHAMBENH = new DataTable();
        private void getChiTiet_KhamBenh(string KHAMBENHID)
        {
            if (saveKHAMBENHID != KHAMBENHID || dtKHAMBENH.Rows.Count == 0)
            {
                DataTable dt = RequestHTTP.getChiTiet_KhamBenh(KHAMBENHID);
                if (dt.Rows.Count > 0)
                {
                    saveKHAMBENHID = KHAMBENHID;
                    //savePHONGID = dt.Rows[0]["PHONGID"].ToString();
                    dtKHAMBENH = dt;
                }
                else
                {
                    saveKHAMBENHID = "";
                    dtKHAMBENH = new DataTable();
                }
            }
        }

        string _enable_mobenhan = "0";
        string _tsmobenhan = "0";
        string kbtraingay = "";
        private void frmKhamBenhNgoaiTru_Load(object sender, EventArgs e)
        {
            kbtraingay = getPara("kbtraingay");

            for (int i = 0; i < tabPane1.Pages.Count; i++)
                tabPane1.ButtonsPanel.Buttons[i].Properties.Appearance.Font = Const.fontDefault;

            load_CauHinh();
            //

            ucTabDanhSachBenhNhan1.setEvent(ucTabDanhSachBenhNhan1_ChangeSelected);
            ucTabDanhSachBenhNhan1.setEvent_MenuPopupClick(MenuPopupClick);
            ucTabDanhSachBenhNhan1.Init_Form(kbtraingay);


            ucTabXetNghiem1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabCDHA1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabPhauThuatThuThuat1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabThuoc1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabVatTu1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabPhieuVanChuyen1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);


            ucTabThuoc1.setEvent_listenFrm(listenFrm_ucTabThuoc);
            

            //menuDichVu.Enabled = false;
        }

        private void load_CauHinh()
        {
            DataTable dt = VNPT.HIS.Common.RequestHTTP.call_ajaxCALL_SP_O("LAY.CAUHINH", "", 0);
            // [{\n\"CH_KETTHUCKHAM\": \"1\",\n\"NGAYPK\": \"1\",\n\"KETHUCKHAM_BN\": \"-1\",\n\"XOA_BN\": \"-1\",\n\"CONFIGBACSI\": \"0\",\n\"CHECK_24H\": \"1\",\n\"KEDONTHUOC_CHITIET_NTU\": \"1\",\n\"DK_MOBENHAN\": \"1\",\n\"HIDE_BTN_MO_BA\": \"1\",\n\"MAPHONGTRUC\": \"0\",\n\"CHUPANH\": \"0\",\n\"ANBANGT\": \"0\",\n\"HIDEDONTHUOCKT\": \"0\",\n\"CHECKTIEN\": \"0\"}]",
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["DK_MOBENHAN"].ToString() == "1")
                {
                    _tsmobenhan = "1";
                }

                if (dt.Rows[0]["NGAYPK"].ToString() == "0")
                {
                    //$('#toolbarIdtxtFromDate').attr("disabled", true);
                    //$('#lblTuNgay').attr("disabled", true);
                    //$('#toolbarIdtxtToDate').attr("disabled", true);
                    //$('#lblDenNgay').attr("disabled", true);
                }
                else
                {
                    //$('#toolbarIdtxtFromDate').attr("disabled", false);
                    //$('#lblTuNgay').attr("disabled", false);
                    //$('#lblTuNgay').attr("disabled", false);
                    //$('#lblDenNgay').attr("disabled", false);
                }

                if (dt.Rows[0]["HIDE_BTN_MO_BA"].ToString() == "1")
                {
                    _enable_mobenhan = "1";
                }

                ucTabDanhSachBenhNhan1.HienThi_AnhChup(dt.Rows[0]["CHUPANH"].ToString() == "1");

                if (dt.Rows[0]["ANBANGT"].ToString() == "0")
                {
                    menuBANGT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //$("#toolbarIdbtnBANGT").hide();
                }
                else
                {
                    menuBANGT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    //$("#toolbarIdbtnBANGT").show();
                }

                if (dt.Rows[0]["HIDEDONTHUOCKT"].ToString() == "0")
                {
                    menuTaoDonThuocKhongThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //$("#toolbarIddrug_1kt").hide(); 
                }
                else
                {
                    menuTaoDonThuocKhongThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    //$("#toolbarIddrug_1kt").show(); 
                }
            }
        }

        private void ucTabDanhSachBenhNhan1_ChangeSelected(object sender, EventArgs e)
        {
            if (e == null)
            {
                reload_title_of_Tabs((DataRowView)sender);
            }
            else
            {
                try
                {
                    if (sender.GetType().ToString() == "System.String")
                    {
                        initMenu((string)sender);
                    }
                    else
                    {
                        DataRowView selectedBenhNhan = (DataRowView)sender;
                        if (selectedBenhNhan != null)
                        {
                            _selectedRow(selectedBenhNhan);
                        }
                    }
                }
                catch (Exception ex) { log.Fatal(ex.ToString()); }
            }
        }
        private void reload_title_of_Tabs(DataRowView selectedBenhNhan_chitiet = null)
        {
            if (selectedBenhNhan_chitiet == null)
            {
                DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
                if (selectedBenhNhan != null)
                {
                    DataTable dt_ChiTiet = VNPT.HIS.Controls.Class.ServiceTabDanhSachBenhNhan.getChiTietBN(selectedBenhNhan["KHAMBENHID"].ToString()
                            , Const.local_phongId + "");
                    if (dt_ChiTiet.Rows.Count > 0)
                    {
                        Const.drvBenhNhan_ChiTiet = dt_ChiTiet.DefaultView[0];
                        selectedBenhNhan_chitiet = dt_ChiTiet.DefaultView[0];
                    }
                }
            }

            if (selectedBenhNhan_chitiet != null)
            {
                // load lại dl của tab đang được chọn, còn các tab khác cho phép reload lại lần sau khi select
                ucTabXetNghiem1.allow_tab_reload = true;
                ucTabCDHA1.allow_tab_reload = true;
                ucTabPhauThuatThuThuat1.allow_tab_reload = true;
                ucTabThuoc1.allow_tab_reload = true;
                ucTabVatTu1.allow_tab_reload = true;
                ucTabPhieuVanChuyen1.allow_tab_reload = true;
                ucTabBenhAn1.allow_tab_reload = true;
                ucTabVienPhi1.allow_tab_reload = true;

                if (tabPane1.SelectedPage == tabPageXetNghiem) ucTabXetNghiem1.reload();
                else if (tabPane1.SelectedPage == tabPageCDHA) ucTabCDHA1.reload();
                else if (tabPane1.SelectedPage == tabPagePhauThuat) ucTabPhauThuatThuThuat1.reload();
                else if (tabPane1.SelectedPage == tabPageThuoc) ucTabThuoc1.reload();
                else if (tabPane1.SelectedPage == tabPageVatTu) ucTabVatTu1.reload();
                else if (tabPane1.SelectedPage == tabPagePhieuVanChuyen) ucTabPhieuVanChuyen1.reload();

                else if (tabPane1.SelectedPage == tabPageBenhAn) ucTabBenhAn1.reload();
                else if (tabPane1.SelectedPage == tabPageVienPhi) ucTabVienPhi1.reload();

                tabPageXetNghiem.PageText = selectedBenhNhan_chitiet["SLXN"].ToString() == "0" ? "Xét nghiệm" : "Xét nghiệm(" + selectedBenhNhan_chitiet["SLXN"].ToString() + ")";
                tabPageCDHA.PageText = selectedBenhNhan_chitiet["SLCDHA"].ToString() == "0" ? "CDHA" : "CDHA(" + selectedBenhNhan_chitiet["SLCDHA"].ToString() + ")";
                tabPagePhauThuat.PageText = selectedBenhNhan_chitiet["SLCHUYENKHOA"].ToString() == "0" ? "Phẫu thuật thủ thuật" : "Phẫu thuật thủ thuật(" + selectedBenhNhan_chitiet["SLCHUYENKHOA"].ToString() + ")";
                tabPageThuoc.PageText = selectedBenhNhan_chitiet["SLTHUOC"].ToString() == "0" ? "Thuốc" : "Thuốc(" + selectedBenhNhan_chitiet["SLTHUOC"].ToString() + ")";
                tabPageVatTu.PageText = selectedBenhNhan_chitiet["SLVATTU"].ToString() == "0" ? "Vật tư" : "Vật tư(" + selectedBenhNhan_chitiet["SLVATTU"].ToString() + ")";
                tabPagePhieuVanChuyen.PageText = selectedBenhNhan_chitiet["SLVANCHUYEN"].ToString() == "0" ? "Phiếu vận chuyển" : "Phiếu vận chuyển(" + selectedBenhNhan_chitiet["SLVANCHUYEN"].ToString() + ")";
            }
        }

        #region Thiết lập trạng thái các menu, ... khi chọn 1 BN
        bool cho_phep_attr_menu_con = false;

        private void ListenFrm_ReturnData(object sender, EventArgs e)
        {
            try
            {
                bool flag = (bool)sender;
                if (flag) XuTriBenhNhan(2);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void initMenu(string dtimeTu ="")
        {
            string hom_nay = Func.getSysDatetime(Const.FORMAT_date1);
            dtimeTu = ucTabDanhSachBenhNhan1.dtimeTu.DateTime.ToString(Const.FORMAT_date1);

            if (kbtraingay == "1" // trái ngày
                ||  (kbtraingay == "" && dtimeTu == hom_nay)
                )
            {
                cho_phep_attr_menu_con = true;

                menuDSKham.Enabled = true;
                menuBANGT.Enabled = true;
                menuGoiKham.Enabled = true;
                menuBatDau.Enabled = false;
                menuKhamBenh.Enabled = false;
                menuDichVu.Enabled = false;
                menuThuoc.Enabled = false;
                menuXuTriKB.Enabled = false;
                menuKhac.Enabled = false;
                menuKetThucKham.Enabled = false;
                //		$("#toolbarId button").removeClass('disabled'); hiển thị 

                return;
            }

            // trong ngày và ngày bắt đầu phải < hôm nay 
            {
                cho_phep_attr_menu_con = false;
                //
                menuDSKham.Enabled = false;
                menuBANGT.Enabled = false;
                menuGoiKham.Enabled = false;
                menuBatDau.Enabled = false;
                menuKhamBenh.Enabled = false;
                menuDichVu.Enabled = false;
                menuThuoc.Enabled = false;
                menuXuTriKB.Enabled = false;
                menuKhac.Enabled = false;
                menuKetThucKham.Enabled = false;

                //	$("#toolbarId button").addClass('disabled'); ẩn


                menuInAn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //	$("#toolbarIdbtnPrint").removeClass('disabled'); hiển thị 
            }

            
        }
        private void _selectedRow(DataRowView selectedBenhNhan)
        {
            string _khambenhid = selectedBenhNhan["KHAMBENHID"].ToString();
            string _hosobenhanid = selectedBenhNhan["HOSOBENHANID"].ToString();
            string _phongkhamdangkyid = selectedBenhNhan["PHONGKHAMDANGKYID"].ToString();
            string _benhnhanid = selectedBenhNhan["BENHNHANID"].ToString();
            string _doituongbenhnhanid = selectedBenhNhan["DOITUONGBENHNHANID"].ToString();
            string _tiepnhanid = selectedBenhNhan["TIEPNHANID"].ToString();
            string _loaitiepnhanid = selectedBenhNhan["LOAITIEPNHANID"].ToString();
            string _trangthaikhambenh = selectedBenhNhan["TRANGTHAIKHAMBENH"].ToString();
            string _xutrikhambenh = selectedBenhNhan["XUTRIKHAMBENHID"].ToString();
            string _sothutu = selectedBenhNhan["SOTHUTU"].ToString();

            menuInAn.Enabled = true;

            initMenu();

            loadTabStatus(_trangthaikhambenh, _xutrikhambenh);
        }
        private void _setButton(bool value)
        {
            if (cho_phep_attr_menu_con)
            {
                menuKhamBenh.Enabled = !value;
                //$("#toolbarIdbtnExam").attr("disabled", value);
                //$("#toolbarIdbtnTreat").attr("disabled", value);   // không có id=toolbarIdbtnTreat

                menuDichVu.Enabled = !value;
                //$("#toolbarIdbtnService").attr("disabled", value);
                ////$("#toolbarIdbtndrug").attr("disabled", value);
                //$("#toolbarIdbtnhandling").attr("disabled", value);    // không có id=toolbarIdbtnhandling

                menuXuTriKB.Enabled = !value;
                //$("#toolbarIdbtnPhieuKham").attr("disabled", value);
                //$("#toolbarIdbtnHoaHong").attr("disabled", value);   // không có id=toolbarIdbtnHoaHong

                //ucBarMenu1.HideShow_byTitle(!value, "Bắt đầu");
                ////$("#toolbarIdbtnStart").attr("disabled", value);

                menuKetThucKham.Enabled = !value;
                //$("#toolbarIdbtnKTKH").attr("disabled", value);
                ////$("#toolbarIdbtnCall").attr("disabled", value);

                menuKhac.Enabled = !value;
                //$("#toolbarIdbtnKHAC").attr("disabled", value);

                menuThuoc.Enabled = !value;
                //$("#toolbarIdbtndrug").attr("disabled", value);

                menuChuyenPhongKham.Enabled = !value;
                //$("#toolbarIdhandling_1").attr("disabled", value);	
            }
        }
        private void _disableMenuXuTri(string xutriid)
        {
            //var arrID = ['toolbarIdbtnKHAC_0', 'toolbarIdbtnKHAC_1', 'toolbarIdbtnKHAC_2', 'toolbarIdbtnKHAC_4', 'toolbarIdbtnKHAC_5', 'toolbarIdbtnKHAC_6', 'toolbarIdbtnKHAC_7'];		
            //for(var i = 0; i < arrID.length; i++){
            //    $("#"+arrID[i]).addClass("disabled");
            //}
            //string[] arrTitle={"Thông tin tử vong","Kiểm điểm tử vong","Thông tin ra viện","Chuyển viện","Hẹn khám mới","Hẹn khám tiếp"};

            menuThongTinTuVong.Enabled = false;
            menuKiemDiemTuVong.Enabled = false;
            menuThongTinRaVien.Enabled = false;
            menuChuyenVien.Enabled = false;
            menuHenKhamMoi.Enabled = false;
            menuHenKhamTiep.Enabled = false;

            if (xutriid == "8")
            {
                menuThongTinTuVong.Enabled = true;
                //$('#toolbarIdbtnKHAC_0').removeClass("disabled");

                //$('#toolbarIdbtnKHAC_1').removeClass("disabled"); Menu đã comment

                menuKiemDiemTuVong.Enabled = true;
                //$('#toolbarIdbtnKHAC_2').removeClass("disabled");
            }
            else if (xutriid == "1" || xutriid == "3" || xutriid == "9")
            {
                menuThongTinRaVien.Enabled = true;
                //$('#toolbarIdbtnKHAC_4').removeClass("disabled");
            }
            else if (xutriid == "4")
            {// hẹn kham tiep
                menuHenKhamTiep.Enabled = true;
                //$('#toolbarIdbtnKHAC_7').removeClass("disabled");
            }
            else if (xutriid == "7")
            {
                menuChuyenVien.Enabled = true;
                //$('#toolbarIdbtnKHAC_5').removeClass("disabled");
            }
            else if (xutriid == "5")
            { // hẹn kham moi
                menuHenKhamMoi.Enabled = true;
                //$('#toolbarIdbtnKHAC_6').removeClass("disabled");
            }
        }
        private void loadTabStatus(string _trangthaikhambenh, string _xutrikhambenh)
        {
            if (_trangthaikhambenh == "9")
            {
                _flgModeView = "1";

                menuBangKe.Enabled = true;
                //$("#toolbarIdgroup_0_4").removeClass("disabled");
                ucTabDanhSachBenhNhan1.ucGrid_DsBN.MenuPopup_Enable_Child_byTitle(false, "Hủy chuyển khám"); // menu popup
                //$("#goilaibnchuyenphong").addClass("disabled");

                _setButton(true);

                if (cho_phep_attr_menu_con)
                {
                    menuThuoc.Enabled = false;
                    //$("#toolbarIdbtndrug").attr("disabled", true);
                    menuBatDau.Enabled = false;
                    //$("#toolbarIdbtnStart").attr("disabled", true);
                }

                if (_enable_mobenhan != "1")
                {
                    ucTabDanhSachBenhNhan1.ucGrid_DsBN.MenuPopup_Enable_Child_byTitle(false, "Yêu cầu mở lại bệnh án"); // menu popup
                    //$("#yeucaumolaibenhan").addClass('disabled');

                    ////$("#molaibenhan").remove(); Menu: Mở lại bệnh án --> đã comment 
                }
                else
                {
                    ucTabDanhSachBenhNhan1.ucGrid_DsBN.MenuPopup_Enable_Child_byTitle(true, "Yêu cầu mở lại bệnh án"); // menu popup
                    //$("#yeucaumo  laibenhan").removeClass('disabled');
                }
            }
            else
            {
                _flgModeView = "0";
                menuBangKe.Enabled = false;
                //$("#toolbarIdgroup_0_4").addClass("disabled");
                ucTabDanhSachBenhNhan1.ucGrid_DsBN.MenuPopup_Enable_Child_byTitle(false, "Yêu cầu mở lại bệnh án"); // menu popup
                //$("#yeucaumolaibenhan").addClass('disabled');

                if (_trangthaikhambenh == "4")
                {
                    ucTabDanhSachBenhNhan1.ucGrid_DsBN.MenuPopup_Enable_Child_byTitle(true, "Hủy chuyển khám"); // menu popup
                    //$("#goilaibnchuyenphong").removeClass("disabled");

                    _setButton(false);

                    _disableMenuXuTri(_xutrikhambenh);

                    if (cho_phep_attr_menu_con) menuBatDau.Enabled = false;
                    //$("#toolbarIdbtnStart").attr("disabled", true);

                    menuTaiNanThuongTich.Enabled = true;
                    barButtonItem30.Enabled = false;
                    //$("#toolbarIdbtnKHAC_3").removeClass("disabled");
                    menuPhieuVanChuyen.Enabled = true;
                    //$("#toolbarIdbtnKHAC_8").removeClass("disabled");
                    menuChuyenPhongKham.Enabled = true;
                    //$("#toolbarIdhandling_1").removeClass("disabled");
                }
                else if (_trangthaikhambenh == "1")
                {
                    if (cho_phep_attr_menu_con) menuBatDau.Enabled = true;
                    //$("#toolbarIdbtnStart").attr("disabled", false);
                    if (cho_phep_attr_menu_con) menuThuoc.Enabled = false;
                    //$("#toolbarIdbtndrug").attr("disabled", true);

                    ucTabDanhSachBenhNhan1.ucGrid_DsBN.MenuPopup_Enable_Child_byTitle(false, "Hủy chuyển khám"); // menu popup
                    //$("#goilaibnchuyenphong").addClass("disabled");

                    _setButton(true);

                    if (cho_phep_attr_menu_con) menuKhac.Enabled = true;
                    //$("#toolbarIdbtnKHAC").attr("disabled", false);

                    menuTaiNanThuongTich.Enabled = false;
                    barButtonItem30.Enabled = true;
                    //$("#toolbarIdbtnKHAC_3").addClass("disabled");
                    menuPhieuVanChuyen.Enabled = false;
                    //$("#toolbarIdbtnKHAC_8").addClass("disabled");
                    menuChuyenPhongKham.Enabled = false;
                    //$("#toolbarIdhandling_1").addClass("disabled");
                    _disableMenuXuTri(_xutrikhambenh);
                }
            }
        }

        #endregion

        #region Menu popup chuột phải click
        private void MenuPopupClick(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                if (Const.drvBenhNhan != null && menu != null) MenuPopupClick_Work(menu.hlink);
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void MenuPopupClick_Work(string hlink)
        {
            if (hlink == "VNPT.HIS.CommonForm.NGT02K029_YeuCauMoBenhAn")
            {
                string mess = "";
                if (Const.drvBenhNhan_ChiTiet["DUYETKETOAN"].ToString() == "1" || Const.drvBenhNhan_ChiTiet["DUYETBH"].ToString() == "2")
                    mess = "Đã duyệt kế toán/bảo hiểm ko thể mở lại bệnh án";
                else if (_tsmobenhan != "1")
                    mess = "Bệnh nhân đã nhập viện không thể mở bệnh án";
                else
                {
                    // {"func":"ajaxCALL_SP_I","params":["NGT.CHECK_MOBA","95726"],"uuid":"14330e88-644d-495c-b779-ec9713c537b7"}
                    string check = RequestHTTP.call_ajaxCALL_SP_I("NGT.CHECK_MOBA", Const.drvBenhNhan["TIEPNHANID"].ToString());
                    if (check == "1")
                        mess = "Đã duyệt kế toán/bảo hiểm không thể mở lại bệnh án";
                }

                if (mess != "")
                {
                    MessageBox.Show(mess, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                string kieu = "0"; // 1: mo benh an, 0: yêu cầu mở.
                string khambenhid = Const.drvBenhNhan["KHAMBENHID"].ToString();
                string benhnhanId = Const.drvBenhNhan["BENHNHANID"].ToString();
                string tiepnhanid = Const.drvBenhNhan["TIEPNHANID"].ToString();
                string hosobenhanid = Const.drvBenhNhan["HOSOBENHANID"].ToString();
                string phongkhamdangkyid = Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString();

                NGT02K029_YeuCauMoBenhAn frm = new NGT02K029_YeuCauMoBenhAn();
                frm.setParam(kieu, khambenhid, benhnhanId, tiepnhanid, hosobenhanid, phongkhamdangkyid);
                frm.SetEvent_Return(return_NGT02K029_YeuCauMoBenhAn);
                openForm(frm, "1");
            }
            else if (hlink == "VNPT.HIS.CommonForm.NGT02K025_LichSuDieuTri")
            {
                NGT02K025_LichSuDieuTri frm = new NGT02K025_LichSuDieuTri();
                frm.setParam(Const.drvBenhNhan["BENHNHANID"].ToString());
                openForm(frm, "1");
            }
            else if (hlink == "VNPT.HIS.CommonForm.NGT02K025_LichSuDieuTri_theodot")
            {
                //NGT02K025_LichSuDieuTri frm = new NGT02K025_LichSuDieuTri();
                //frm.setParam(Const.drvBenhNhan["BENHNHANID"].ToString());
                //openForm(frm, "1");
            }
            
            else if (hlink == "huy_chuyen_kham") // Hủy chuyển khám
            {
                // code web: ngoaitru/NGT02K001_KB_MHC.js --> function _goilaibnchuyenkham(){
                //             var myVar ={
                //         phongkhamdangkyid : $("#hidPHONGKHAMDANGKYID").val(),
                //tiepnhanid: $("#hidTIEPNHANID").val(),
                //doituongbtid: $("#hidDOITUONGBENHNHANID").val()

                //     };
                //         var check = jsonrpc.AjaxJson.ajaxCALL_SP_S("GOILAI.CHUYENKHAM", JSON.stringify(myVar));
                object data = new
                {
                    phongkhamdangkyid = Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString(),
                    tiepnhanid = Const.drvBenhNhan["TIEPNHANID"].ToString(),
                    doituongbtid = Const.drvBenhNhan["DOITUONGBENHNHANID"].ToString()
                };

                string dataJson = JsonConvert.SerializeObject(data).Replace("\"", "\\\"");

                string check = RequestHTTP.call_ajaxCALL_SP_S_result("GOILAI.CHUYENKHAM", dataJson);
                if (check == "kochuyenphong")
                {
                    MessageBox.Show("Bệnh nhân chưa chuyển sang phòng khám khác từ phòng này", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (check == "dakham")
                {
                    MessageBox.Show("Phòng chuyển tới đã hoặc đang khám, không gọi lại được", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (check == "dathutien")
                {
                    MessageBox.Show("Phòng chuyển tới bệnh nhân đã đóng tiền, không hủy được", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (check == "dathuphi")
                {
                    MessageBox.Show("Bệnh nhân đã đóng tiền không hủy được, muốn hủy phải hủy hóa đơn trước", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (check == "1")
                {
                    MessageBox.Show("Hủy chuyển phòng khám thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else if (hlink == "VNPT.HIS.VienPhi.VPI01T006_thanhtoanvienphi")
            {
                VNPT.HIS.VienPhi.VPI01T006_thanhtoanvienphi frm = new VNPT.HIS.VienPhi.VPI01T006_thanhtoanvienphi();
                frm.setParam(Const.drvBenhNhan["TIEPNHANID"].ToString());
                openForm(frm, "1");
            }
            else if (hlink == "VNPT.HIS.CommonForm.NGT02K047_LichSuKCB") // "Lịch sử theo cổng BHYT
            {
                string ma_the = "";
                string ho_ten = "";
                string ngay_sinh = "";
                string ngay_bd = "";
                string ngay_kt = "";
                string ma_CSKCB = "";
                string gioi_tinh = "";

                getChiTiet_KhamBenh(Const.drvBenhNhan["KHAMBENHID"].ToString());

                if (dtKHAMBENH.Rows.Count > 0)
                {
                    ma_the = dtKHAMBENH.Rows[0]["MA_BHYT"].ToString();
                    ho_ten = dtKHAMBENH.Rows[0]["TENBENHNHAN"].ToString();
                    ngay_sinh = dtKHAMBENH.Rows[0]["NGAY_SINH"].ToString();
                    ngay_bd = dtKHAMBENH.Rows[0]["BHYT_BD"].ToString();
                    ngay_kt = dtKHAMBENH.Rows[0]["BHYT_KT"].ToString();
                    ma_CSKCB = dtKHAMBENH.Rows[0]["MA_KCBBD"].ToString();
                    gioi_tinh = dtKHAMBENH.Rows[0]["GIOITINHID"].ToString();
                }

                NGT02K047_LichSuKCB frm = new NGT02K047_LichSuKCB(
                    ma_the, ho_ten, ngay_sinh, gioi_tinh,
                    ma_CSKCB,
                    ngay_bd, ngay_kt);
                openForm(frm, "1");
            }
        }
        private void return_NGT02K029_YeuCauMoBenhAn(object sender, EventArgs e)
        {
            ucTabDanhSachBenhNhan1.Reload();
        }
        #endregion

        #region menu toolbar click

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void menuDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void openForm(Form frm, string optionsPopup)
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }
        
        private void menuXuTriKB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NGT02K005_PhieuKhamBenh frm = new NGT02K005_PhieuKhamBenh();
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                    frm.loadData(Const.drvBenhNhan, Const.drvBenhNhan_ChiTiet);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void menuDichVu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    NGT02K016_ChiDinhDichVu frm = new NGT02K016_ChiDinhDichVu();
                    frm.loadData("taophieuchidinhdichvu", Const.drvBenhNhan, null, "5");
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
                
        private void listenFrm_ucTabThuoc(object sender, EventArgs e)
        { 
            // Cập nhật lại số lượng
            //reload_title_of_Tabs(null);
        }

        private void menuKhamBenh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    NGT02K002_KhamBenhHoiBenh frm = new NGT02K002_KhamBenhHoiBenh();
                    frm.loadData(Const.drvBenhNhan);
                    frm.SetEvent_ListenFrm_ReturnData(ListenFrm_ReturnData);
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void menuBatDau_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    //"Code tiếp phần comment trong: VNPT.HIS.NgoaiTru.NGT02K001_KB_MHC_KhamBenhNgoaiTru.menuBatDau_ItemClick
                    //Code js: NGT02K001_KB_MHC.js
                    //$(""#toolbarIdbtnStart"").on(""click"", function() {

                    // Thực hiện thay đổi trạng thái BN tại đây - khi bấm vào bắt đầu
                    //....
                    // check lại gọi form NGT02K002_KhamBenhHoiBenh đúng theo trạng thái thay đổi trên ko?
                    //....

                    string result = RequestHTTP.getOneValue("NGTCHECK.DUYETKT", Const.drvBenhNhan["TIEPNHANID"].ToString());

                    if ("1" == result)
                    {
                        MessageBox.Show("Bệnh nhân đã duyệt kế toán ko thể bắt đầu khám");
                        return;
                    }

                    var ObjData = new
                    {
                        PHONGKHAMDANGKYID = Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString(),
                        KHAMBENHID = Const.drvBenhNhan["KHAMBENHID"].ToString(),
                        TIEPNHANID = Const.drvBenhNhan["TIEPNHANID"].ToString(),
                        DOITUONGID = Const.drvBenhNhan["DOITUONGBENHNHANID"].ToString(),
                        PHONGID = Const.drvBenhNhan["PHONGID"].ToString()
                        //Const.drvBenhNhan_ChiTiet["PHONGID"].ToString()
                    };

                    var MaBenhNhan = Const.drvBenhNhan_ChiTiet["SOTHEBHYT"].ToString();
                    var Loai = "2";
                    var ttbnObj = new
                    {
                        tenbenhnhan = Const.drvBenhNhan_ChiTiet["TENBENHNHAN"].ToString(),
                        ngaysinh = Const.drvBenhNhan_ChiTiet["NGAYSINH"].ToString(),
                        gioitinhid = Const.drvBenhNhan_ChiTiet["GIOITINH"].ToString() == "Nam" ? "1" : "2",
                        benhnhanid = Const.drvBenhNhan["BENHNHANID"].ToString()
                    };

                    DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NGT01T002.TKBN1", MaBenhNhan + "$" + Loai + "$" + JsonConvert.SerializeObject(ttbnObj).Replace("\"", "\\\""), 0);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["NGAYTHUOC"].ToString()))
                        {
                            var NgayThuoc = dt.Rows[0]["NGAYTHUOC"].ToString();
                            var Ngay = NgayThuoc.Split(new string[] { "/" }, StringSplitOptions.None);
                            NgayThuoc = Ngay[2].Substring(0, 4) + Ngay[1] + Ngay[0];
                            var NgayTN = Func.getSysDatetime("yyyyMMdd");
                            var NgayKeDon = dt.Rows[0]["NGAYMAUBENHPHAM"].ToString();

                            if (int.Parse(NgayKeDon) != int.Parse(NgayTN) && int.Parse(NgayThuoc) > int.Parse(NgayTN))
                            {
                                DialogResult dialogResult = MessageBox.Show("Bệnh nhân đang còn thuốc của lần khám trước, có tiếp tục bắt đầu khám", "", MessageBoxButtons.YesNo);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    BatDauKham(ObjData);
                                }
                            }
                            else
                            {
                                BatDauKham(ObjData);
                            }
                        }
                        else
                        {
                            BatDauKham(ObjData);
                        }
                    }
                    else
                    {
                        BatDauKham(ObjData);
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void BatDauKham(object ObjData)
        {
            try
            {
                var rs = DayCongBYT();
                if (1 == rs)
                {
                    return;
                }

                var ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K001.EV004", JsonConvert.SerializeObject(ObjData).Replace("\"", "\\\""));
                if ("1" == ret)
                {
                    menuBatDau.Enabled = false;

                    //var index = $('#hidINDEX').val();
                    //_icon = '<center><img src="' + _opt.imgPath[1] + '" width="15px"></center>';

                    //$("#" + _gridId).jqGrid('setCell', index, 1, _icon);
                    //$("#" + _gridId).jqGrid('setCell', index, 11, 4);

                    // dang kham == "4"
                    // khong co xu tri == "0"
                    ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow["TRANGTHAIKHAMBENH"] = "4";
                    loadTabStatus("4", "0");

                    NGT02K002_KhamBenhHoiBenh frm = new NGT02K002_KhamBenhHoiBenh();
                    frm.loadData(Const.drvBenhNhan);
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1");
                }
                else if ("200" == ret)
                {
                    MessageBox.Show("Bệnh nhân đối tượng BHYT + DV nhưng chưa thanh toán tiền DV");
                }
                else if ("300" == ret)
                {
                    MessageBox.Show("Bệnh nhân chưa thanh toán tiền công khám");
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private int DayCongBYT()
        {
            try
            {
                ServiceBYT.Lay_thong_tin_ws_BYT(); 

                if (ServiceBYT.ServiceBYT_BYTDAYDL == "1")
                {
                    var Param = new string[] { "1" };
                    var Param1 = new string[] { }; 
                    var Data_bv = RequestHTTP.call_ajaxCALL_SP_O("NGT.GETTT_BV", "", 0);

                    var checkinObj = new
                    {
                        MA_LK = Const.drvBenhNhan["MAHOSOBENHAN"].ToString(),
                        Sender_Code = ServiceBYT.ServiceBYT_TECHKCBBD,
                        Sender_Name = "",
                        Action_Type = "1",
                        Transaction_Type = "M0001",
                        MABENHVIEN = ServiceBYT.ServiceBYT_MACSYT,
                        MA_THE = Const.drvBenhNhan_ChiTiet["SOTHEBHYT"].ToString(),
                        NGAYGIOKHAM = Func.getSysDatetime("YYYYMMDDHh24mi")
                    };

                    //function XML_BYT_TaoHeader(objjj)
                    //    objHeader.Action_Type = objjj.Action_Type;              // 0: bắt đầu khám, 1: kết thúc khám
                    var objHeader = new
                    {
                        Message_Version = "1.0",
                        Sender_Code = ServiceBYT.ServiceBYT_TECHKCBBD,
                        Sender_Name = "",
                        Transaction_Type = "M0001",
                        Transaction_Name = "",
                        Transaction_Date = "",
                        Transaction_ID = "",
                        Request_ID = "",
                        Action_Type = "1"
                    };

                    //function XML_BYT_TaoTheCHECKIN(objjj){
                    //    var objCHECKIN = new Object();

                    //    objCHECKIN.MA_LK = objjj.MA_LK;
                    //    objCHECKIN.MABENHVIEN = objjj.MABENHVIEN;
                    //    objCHECKIN.MA_THE = objjj.MA_THE;
                    //    objCHECKIN.MA_KCBBD = objjj.MA_KCBBD;
                    //    objCHECKIN.HO_TEN = objjj.HO_TEN;
                    //    objCHECKIN.NGAY_SINH = objjj.NGAY_SINH;
                    //    objCHECKIN.NAM_SINH = objjj.NAM_SINH;
                    //    objCHECKIN.GIOI_TINH = objjj.GIOI_TINH;
                    //    objCHECKIN.DIA_CHI = objjj.DIA_CHI;
                    //    objCHECKIN.TU_NGAY = objjj.TU_NGAY;
                    //    objCHECKIN.DEN_NGAY = objjj.DEN_NGAY;
                    //    objCHECKIN.MATINHQUANHUYEN = objjj.MATINHQUANHUYEN;
                    //    objCHECKIN.NGAYGIOVAO = objjj.NGAYGIOVAO;                       // 201603091521
                    //    objCHECKIN.NGAYDU5NAM = objjj.NGAYDU5NAM;
                    //    objCHECKIN.MA_NOICHUYENDEN = objjj.MA_NOICHUYENDEN;
                    //    objCHECKIN.LYDODENKHAM = objjj.LYDODENKHAM;
                    //    objCHECKIN.TINHTRANGVAOVIEN = objjj.TINHTRANGVAOVIEN;           // Tình trạng vào viện (1: Đúng tuyến , 2: Cấp cứu, 3 : Trái tuyến)
                    //    objCHECKIN.SOKHAMBENH = objjj.SOKHAMBENH;
                    //    objCHECKIN.SODIENTHOAI_LH = objjj.SODIENTHOAI_LH;
                    //    objCHECKIN.NGUOILIENHE = objjj.NGUOILIENHE;
                    //    objCHECKIN.MA_KHUVUC = objjj.MA_KHUVUC;
                    //    objCHECKIN.NGAYGIORA = objjj.NGAYGIORA;                         // 201603091521
                    //    objCHECKIN.MA_LOAI_KCB = objjj.MA_LOAI_KCB;                     // 1: Khám bệnh; 2: Điều trị ngoại trú; 3: Điều trị nội trú
                    //    objCHECKIN.TEN_BENH = objjj.TEN_BENH;
                    //    objCHECKIN.MA_BENH = objjj.MA_BENH;                             // Mã bệnh chính theo ICD 10
                    //    objCHECKIN.MA_BENHKHAC = objjj.MA_BENHKHAC;                     // Mã bệnh kèm theo theo ICD 10, có nhiều mã ICD được phân cách bằng ký tự chấm phẩy
                    //    objCHECKIN.CHANDOAN = objjj.CHANDOAN;
                    //    objCHECKIN.NGAYHETTHUOC = objjj.NGAYHETTHUOC;
                    //    objCHECKIN.NGAYGIOKHAM = objjj.NGAYGIOKHAM;
                    //    objCHECKIN.LYDO_CHUYEN = objjj.LYDO_CHUYEN;
                    //    objCHECKIN.NGAY_CHUYENTUYEN = objjj.NGAY_CHUYENTUYEN;
                    //    objCHECKIN.KHAMDIEUTRITAI = objjj.KHAMDIEUTRITAI;
                    //    objCHECKIN.GT_THE_DEN = objjj.GT_THE_DEN;
                    //    objCHECKIN.GT_THE_TU = objjj.GT_THE_TU;
                    //    objCHECKIN.SOHOSO = objjj.SOHOSO;
                    //    objCHECKIN.TENCSKCBDEN = objjj.TENCSKCBDEN;
                    //    objCHECKIN.MACSKCBDEN = objjj.MACSKCBDEN;
                    //    objCHECKIN.TENCSKCBDI = objjj.TENCSKCBDI;
                    //    objCHECKIN.MACSKCBDI = objjj.MACSKCBDI;
                    //    return objCHECKIN;
                    //}
                    var objIn = new
                    {
                        MA_LK = Const.drvBenhNhan["MAHOSOBENHAN"].ToString(),
                        MABENHVIEN = ServiceBYT.ServiceBYT_MACSYT,
                        MA_THE = Const.drvBenhNhan_ChiTiet["SOTHEBHYT"].ToString(),
                        //MA_KCBBD = "",
                        //HO_TEN = "",
                        //NGAY_SINH = "",
                        //NAM_SINH = "",
                        //GIOI_TINH = "",
                        //DIA_CHI = "",
                        //TU_NGAY = "",
                        //DEN_NGAY = "",
                        //MATINHQUANHUYEN = "",
                        //NGAYGIOVAO = "",
                        //NGAYDU5NAM = "",
                        //MA_NOICHUYENDEN = "",
                        //LYDODENKHAM = "",
                        //TINHTRANGVAOVIEN = "",
                        //SOKHAMBENH = "",
                        //SODIENTHOAI_LH = "",
                        //NGUOILIENHE = "",
                        //MA_KHUVUC = "",
                        //NGAYGIORA = "",
                        //MA_LOAI_KCB = "",
                        //TEN_BENH = "",
                        //MA_BENH = "",
                        //MA_BENHKHAC = "",
                        //CHANDOAN = "",
                        //NGAYHETTHUOC = "",
                        NGAYGIOKHAM = Func.getSysDatetime("yyyyMMddHHmm"),
                        //LYDO_CHUYEN = "",
                        //NGAY_CHUYENTUYEN = "",
                        //KHAMDIEUTRITAI = "",
                        //GT_THE_DEN = "",
                        //GT_THE_TU = "",
                        //SOHOSO = "",
                        //TENCSKCBDEN = "",
                        //MACSKCBDEN = "",
                        //TENCSKCBDI = "",
                        //MACSKCBDI = ""
                    };

                    var obj3 = ServiceBYT.XML_BYT_TaoKhung(objHeader, objIn, "1");
                    //var ws = "https://congdulieuyte.vn/hPortal/services/IC/WSPortal";

                    var resultCongBYT = ServiceBYT.guiTTBDK(ServiceBYT.ServiceBYT_Url, "CongDLYTWS", "guiTTBDK",
                        new String[] {
                            "_usr"
                            ,"_pwd"
                            ,"csytid"
                            ,"matinh"
                            ,"xmlContent" },
                        new String[] {
                            ServiceBYT.ServiceBYT_Username
                            ,ServiceBYT.ServiceBYT_Password
                            ,ServiceBYT.ServiceBYT_MACSYT
                            ,Data_bv.Rows[0]["MADIAPHUONG"].ToString()
                            ,Convert.ToBase64String(Encoding.UTF8.GetBytes(obj3.ToString())) });

                    var rets = resultCongBYT.Split(';');
                    if ("0" != rets[0])
                    {
                        DialogResult DialogResult = MessageBox.Show("Lỗi đẩy dữ liệu cổng y tế: " + rets[1], "");
                        if (ServiceBYT.ServiceBYT_BYTSTOPCHUCNANG == "1")
                        {
                            return 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return 0;
        }


        private void menuChuyenPhongKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NGT02K046_them_chuyenphongkham frm = new NGT02K046_them_chuyenphongkham();
                    frm.Load_Data(Const.drvBenhNhan["KHAMBENHID"].ToString(), Const.drvBenhNhan["TIEPNHANID"].ToString(),
                        Const.drvBenhNhan_ChiTiet["DICHVUID"].ToString(), "1",
                        Const.drvBenhNhan_ChiTiet["PHONGID"].ToString(), Const.drvBenhNhan["DOITUONGBENHNHANID"].ToString(),
                        Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString());
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        #endregion

        private void menuNghiHuongBHXH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    string khambenhid = Const.drvBenhNhan["KHAMBENHID"].ToString();
                    NGT02K058_Thongtin_nghiduong frm = new NGT02K058_Thongtin_nghiduong();
                    frm.setParam(khambenhid);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void menuTaoDonThuocKhongThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //$("#toolbarIddrug_1kt").on("click", function(e){
            //    //tuyennx_add_start 
            //    var _sql_par = [{ "name":"[0]", "value":$("#hidTIEPNHANID").val()}];
            //    var _result = jsonrpc.AjaxJson.getOneValue("NGTCHECK.DUYETKT", _sql_par);
            //    if (_result == 1)
            //    {
            //        DlgUtil.showMsg('Bệnh nhân đã duyệt kế toán ko thể chỉ định dịch vụ');
            //        return;
            //    }
            //    //tuyennx_add_end
            //    EventUtil.setEvent("assignDrug_fail", function(e){
            //        DlgUtil.close("dlgCDT");
            //    });

            //    _openDialogThuocK('02K044', 0, "Chỉ định thuốc không thuốc");
            //});
            _openDialogThuoc_KhongThuoc("");
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NGT01T004_Tiepnhan_Chuyenphongkham frm = new NGT01T004_Tiepnhan_Chuyenphongkham();
                    frm.setKhamBenhID("1", Const.drvBenhNhan["KHAMBENHID"].ToString()
                        , Const.drvBenhNhan_ChiTiet["DICHVUID"].ToString()
                        , Const.drvBenhNhan_ChiTiet["PHONGID"].ToString()
                        , Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString());
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void menuDSKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NGT02K028_HienthiDanhSachKham frm = new NGT02K028_HienthiDanhSachKham();
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void menuKiemDiemTuVong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NGT02K033_KiemdiemTuVong frm = new NGT02K033_KiemdiemTuVong();
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form  
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }


        private void btnInDonThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NGT02K034_InDonThuoc frm = new NGT02K034_InDonThuoc();
                    openForm(frm, "1");
                }
                else
                {
                    MessageBox.Show("Hãy chọn bệnh nhân muốn in đơn thuốc.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            finally
            {   //Close Wait Form  
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        #region menu GỌI KHÁM - KẾT THÚC KHÁM
        private void menuGoiKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                //code:
                DataRowView dataRow = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
                if (dataRow != null)
                {
                    var stt = dataRow["SOTHUTU"].ToString();
                    if (!string.IsNullOrWhiteSpace(stt))
                    {
                        try
                        {
                            BackgroundWorker backgroundWorker = new BackgroundWorker();
                            backgroundWorker.DoWork += new DoWorkEventHandler(br_DoWork);
                            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(br_RunWorkerCompleted);
                            backgroundWorker.RunWorkerAsync(stt);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        ucTabDanhSachBenhNhan1.Reload();
                    }
                }
            }
            finally
            {   //Close Wait Form  
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }


        private static string urlPath = Application.StartupPath + @"\Resources\";
        private static string moiBenhNhanSoFile = "sound_MoiBenhNhanSo";
        private static string vaoKhamFile = "sound_VaoKham";
        private static string muteFile = "im_lang";
        private static string extension = ".m4a";

        private bool phat_audio_xong = true;
        private void br_DoWork(object sender, DoWorkEventArgs e)
        {
            if (phat_audio_xong == false) return;

            phat_audio_xong = false;

            //WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            //WMPLib.IWMPPlaylist playlist = wplayer.playlistCollection.newPlaylist("playlist");
            //playlist.appendItem((wplayer.newMedia(urlPath + moiBenhNhanSoFile + extension)));

            new System.Media.SoundPlayer("./Resources/sound_MoiBenhNhanSo.wav").PlaySync();

            string stt = (string)e.Argument;
            string sttArr = LeftPad(stt, 4);
            for (int index = 0; index < sttArr.Length; index++)
            {
                //playlist.appendItem(wplayer.newMedia(urlPath + sttArr[index] + extension));
                new System.Media.SoundPlayer("./Resources/"+ sttArr[index] + ".wav").PlaySync();
            }
             

            //playlist.appendItem(wplayer.newMedia(urlPath + vaoKhamFile + extension));
            //playlist.appendItem(wplayer.newMedia(urlPath + muteFile + extension));
            //wplayer.currentPlaylist = playlist;
            //Thread.Sleep(10000);

            new System.Media.SoundPlayer("./Resources/sound_VaoKham.wav").PlaySync();

            new System.Media.SoundPlayer("./Resources/im_lang.wav").PlaySync();

            phat_audio_xong = true;
            
            var obj = new
            {
                PHONGKHAMDANGKYID = Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString()
            };

            var objData = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
            RequestHTTP.call_ajaxCALL_SP_I("NGT02K001.GOIKHAM", objData);

        }

        private void br_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ucTabDanhSachBenhNhan1.Reload();
        }

        private static string LeftPad(string number, int targetLength)
        {
            var output = number;
            if (output.Length == 4) return output;
            while (output.Length < targetLength)
            {
                output = '0' + output;
            }
            return output;
        }

        private void menuKetThucKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                //code:
                XuTriBenhNhan(2);
            }
            finally
            {   //Close Wait Form  
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void XuTriBenhNhan(int Kieu)
        {
            var MyVar = new
            {
                khambenhid = Const.drvBenhNhan["KHAMBENHID"].ToString()
            };

            string json = "";
            json += Func.json_item("khambenhid", Const.drvBenhNhan["KHAMBENHID"].ToString());
            json = Func.json_item_end(json);
            json = json.Replace("\"", "\\\"").Replace("//", "");

            var check = RequestHTTP.call_ajaxCALL_SP_S_result("CHECK.KTKHAM", json);
            if ("codvcls" == check)
            {
                MessageBox.Show("Bệnh nhân đang có chỉ định dịch vụ trạng thái đang chờ tiếp nhận, có thể hủy phiếu để kết thúc khám");
                //return false;
            }
            else if ("ngaydichvu" == check)
            {
                MessageBox.Show("Bệnh nhân có thời gian chỉ định dịch vụ lớn hơn thời gian ra viện không thể kết thúc khám");
                //return false;
            }
            //tuyennx_add_end_20170727
            else if ("pasdvcls" == check)
            {
                DialogResult DialogResult = MessageBox.Show("Bệnh nhân có dịch vụ đang thực hiện, bạn có muốn kết thúc khám không.", "", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.Yes)
                {
                    KetThucKham(Kieu);
                }
            }
            else if ("1" == check)
            {
                KetThucKham(Kieu);
            }
        }

        private void KetThucKham(int Kieu)
        {
            //=========== check dich vu thanh toan dong thoi; chi check khi CLICK KET THUC KHAM;
            if (2 == Kieu)
            {
                var par11 = "HIS_CANHBAO_KHONG_TTDT";
                var checkTt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", par11);

                if ("1" == checkTt)
                {
                    var msgCheckTt = RequestHTTP.call_ajaxCALL_SP_S_result("NTU01H001.EV018", Const.drvBenhNhan["TIEPNHANID"].ToString());
                    if (!string.IsNullOrEmpty(msgCheckTt))
                    {
                        MessageBox.Show("Các dịch vụ " + msgCheckTt + " miễn giảm thanh toán đồng thời");
                        return;
                    }
                }

                string json = "";
                json += Func.json_item("kieu", Kieu.ToString());
                json += Func.json_item("khambenhid", Const.drvBenhNhan["KHAMBENHID"].ToString());
                json += Func.json_item("phongkhamdangkyid", Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString());
                json += Func.json_item("tiepnhanid", Const.drvBenhNhan["TIEPNHANID"].ToString());
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"").Replace("//", "");

                var ret = RequestHTTP.call_ajaxCALL_SP_S_result("NGT02K001.KTKHAM", json);
                var rets = ret.Split(',');
                if ("1" == rets[0])
                {
                    if (!string.IsNullOrEmpty(rets[1]) && "0" != rets[1] && "100" != rets[1])
                    {
                        DialogResult DialogResult = MessageBox.Show("Bệnh nhân có ICD bệnh án dài ngày, bạn muốn nhập thông tin bệnh án?", "", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            //_mobenhan_daingay();
                            //LoadGridData(Const.drvBenhNhan["PHONGID"].ToString());
                            ucTabDanhSachBenhNhan1.Reload();
                            _setButton(true);
                            menuThuoc.Enabled = false;
                            MessageBox.Show("Cập nhật thông tin thành công");
                            //_loadGridData(_opt.phongid);
                        }
                    }
                    else
                    {
                        //LoadGridData(Const.drvBenhNhan["PHONGID"].ToString());
                        ucTabDanhSachBenhNhan1.Reload();
                        _setButton(true);
                        menuThuoc.Enabled = false;
                        MessageBox.Show("Cập nhật thông tin thành công");
                        //_loadGridData(_opt.phongid);
                    }
                }
                else if ("kocoxutri" == ret)
                {
                    MessageBox.Show("Bệnh nhân hiện chưa có xử trí.");
                }
                else if ("coxutri" == ret)
                {
                    MessageBox.Show("Bệnh nhân có xử trí không trả bệnh nhân.");
                }
                else if ("codvcls" == ret)
                {
                    MessageBox.Show("Bệnh nhân đang có chỉ định dịch vụ CLS hoặc đã kê đơn thuốc");
                }
                else if ("connotien" == ret)
                {
                    MessageBox.Show("Bệnh nhân còn nợ tiền, phải thanh toán mới kết thúc khám.");
                }
                else if ("cophieudangsua" == ret)
                {
                    MessageBox.Show("Bệnh nhân có phiếu CLS/Đơn thuốc đang sửa, không kết thúc khám được.");
                }
                else if ("chuacochandoan" == ret)
                {
                    MessageBox.Show("Bệnh nhân chưa có chẩn đoán.");
                }
                else if ("kococannang" == ret)
                {
                    MessageBox.Show("Bệnh nhân chưa có thông tin cân nặng");
                }
                else if ("thoigianxtri" == ret)
                {
                    MessageBox.Show("Bệnh nhân chưa có thời gian ra viện hoạc thời gian ra viện nhỏ hơn ngày hiện tại, cập nhật lại ngày ra viện trước.");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin không thành công");
                }
            }
        }
        #endregion

        #region MENU THUỐC
        private void menuTaoDonThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //$("#toolbarIddrug_1").on("click", function(e){
            //    //tuyennx_add_start 
            //    var _sql_par = [{ "name":"[0]", "value":$("#hidTIEPNHANID").val()}];
            //    var _result = jsonrpc.AjaxJson.getOneValue("NGTCHECK.DUYETKT", _sql_par);
            //    if (_result == 1)
            //    {
            //        DlgUtil.showMsg('Bệnh nhân đã duyệt kế toán ko thể chỉ định dịch vụ');
            //        return;
            //    }
            //    //tuyennx_add_end
            //    EventUtil.setEvent("assignDrug_fail", function(e){
            //        DlgUtil.close("dlgCDT");
            //    });

            //    _openDialogThuoc('02D010', 0, "Chỉ định thuốc");
            //});
            if (!check_duyet_ke_toan()) return;
            _openDialogThuoc("chi_dinh_thuoc");
        }
        private void menuDonThuocMuaNgoai_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //$("#toolbarIddrug_mn").on("click", function() {
            //              //tuyennx_add_start 
            //              var _sql_par = [{ "name":"[0]", "value":$("#hidTIEPNHANID").val()}];
            //              var _result = jsonrpc.AjaxJson.getOneValue("NGTCHECK.DUYETKT", _sql_par);
            //              if (_result == 1)
            //              {
            //                  DlgUtil.showMsg('Bệnh nhân đã duyệt kế toán ko thể chỉ định dịch vụ');
            //                  return;
            //              }
            //              //tuyennx_add_end
            //              paramInput ={
            //              khambenhid: $("#hidKHAMBENHID").val(),
            //			maubenhphamid: "",
            //			loaikedon: 1,
            //			dichvuchaid: "",
            //			opt: "02D011"

            //          };

            //              dlgPopup = DlgUtil.buildPopupUrl("divDlgTaoPhieuThuoc" + "02D011", "divDlg", "manager.jsp?func=../noitru/NTU02D010_CapThuoc", paramInput, "Tạo đơn thuốc mua ngoài", 1300, 590);
            //              DlgUtil.open("divDlgTaoPhieuThuoc" + "02D011");
            //          });

            if (!check_duyet_ke_toan()) return;
            _openDialogThuoc("don_thuoc_mua_ngoai");
        }

        private void menuMuaThuocNhaThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //EventUtil.setEvent("assignDrug_fail", function(e){
            //    DlgUtil.close("dlgCDT");
            //});

            //_openDialogThuoc('02D019', 0, "Mua thuốc nhà thuốc");

            _openDialogThuoc("mua_thuoc_nha_thuoc");
        }

        private void menuTaoDonThuocDongY_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //var _sql_par = [{ "name":"[0]", "value":$("#hidTIEPNHANID").val()}];
            //var _result = jsonrpc.AjaxJson.getOneValue("NGTCHECK.DUYETKT", _sql_par);
            //if (_result == 1)
            //{
            //    DlgUtil.showMsg('Bệnh nhân đã duyệt kế toán ko thể chỉ định dịch vụ');
            //    return;
            //}
            ////tuyennx_add_end
            //EventUtil.setEvent("assignDrug_fail", function(e){
            //    DlgUtil.close("dlgCDT");
            //});

            //_openDialogThuoc('02D017', 1, "Chỉ định thuốc YHCT");

            if (!check_duyet_ke_toan()) return;
            _openDialogThuoc("tao_don_thuoc_dongy");
        }

        private void menuTaoPhieuTraThuocDongY_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //$("#toolbarIddrug_2dy").on("click", function(e){
            //    //tuyennx_add_start 
            //    var _sql_par = [{ "name":"[0]", "value":$("#hidTIEPNHANID").val()}];
            //    var _result = jsonrpc.AjaxJson.getOneValue("NGTCHECK.DUYETKT", _sql_par);
            //    if (_result == 1)
            //    {
            //        DlgUtil.showMsg('Bệnh nhân đã duyệt kế toán ko thể chỉ định dịch vụ');
            //        return;
            //    }
            //    //tuyennx_add_end
            //    EventUtil.setEvent("assignDrug_fail", function(e){
            //        DlgUtil.close("dlgCDT");
            //    });

            //    _openDialogThuoc('02D018', 1, "Trả thuốc YHCT");
            //});

            if (!check_duyet_ke_toan()) return;
            _openDialogThuoc("tao_phieu_tra_thuoc_dongy");
        }

        private void menuTaoPhieuVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //$("#toolbarIddrug_3").on("click", function(e){
            //    //tuyennx_add_start 
            //    var _sql_par = [{ "name":"[0]", "value":$("#hidTIEPNHANID").val()}];
            //    var _result = jsonrpc.AjaxJson.getOneValue("NGTCHECK.DUYETKT", _sql_par);
            //    if (_result == 1)
            //    {
            //        DlgUtil.showMsg('Bệnh nhân đã duyệt kế toán ko thể chỉ định dịch vụ');
            //        return;
            //    }
            //    //tuyennx_add_end
            //    EventUtil.setEvent("assignDrug_fail", function(e){
            //        DlgUtil.close("dlgCDT");
            //    });
            //    _openDialogThuoc('02D015', 1, "Chỉ định vật tư");
            //});

            if (!check_duyet_ke_toan()) return;
            _openDialogThuoc("tao_phieu_vat_tu");
        }

        private void _openDialogThuoc(string menu)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    NTU02D010_CapThuoc frm = new NTU02D010_CapThuoc();
                    frm.loadData(menu, Const.drvBenhNhan);
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form  
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void _openDialogThuoc_KhongThuoc(string menu)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    NGT02K044_CapThuocK frm = new NGT02K044_CapThuocK();
                    string mauBenhPhamID = menu == "cap_nhat_don_thuoc_khong_thuoc" ? Const.drvBenhNhan["MAUBENHPHAMID"].ToString() : "";

                    frm.Load_Data(Const.drvBenhNhan["KHAMBENHID"].ToString(), Const.drvBenhNhan["PHONGID"].ToString(),
                        Const.drvBenhNhan["BENHNHANID"].ToString(), Const.drvBenhNhan["DOITUONGBENHNHANID"].ToString(),
                        Const.drvBenhNhan["HOSOBENHANID"].ToString(), mauBenhPhamID);

                    if (menu == "cap_nhat_don_thuoc_khong_thuoc") frm.set_para_UPDATE(Const.drvBenhNhan);

                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form  
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private bool check_duyet_ke_toan()
        {
            string check = RequestHTTP.getOneValue("NGTCHECK.DUYETKT", Const.drvBenhNhan["TIEPNHANID"].ToString());
            if (check == "1")
            {
                MessageBox.Show("Bệnh nhân đã duyệt kế toán không thể chỉ định dịch vụ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        #endregion

        
        private void PrintPreview(string title, string code, DataTable parTable, int height = 650, int width = 900)
        {
            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(title, code, parTable, height, width);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btnGiayRaVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var khamBenhId = selectedBenhNhan["KHAMBENHID"].ToString();
                if (string.IsNullOrEmpty(khamBenhId) || "-1".Equals(khamBenhId))
                {
                    MessageBox.Show("Hãy chọn bệnh nhân muốn in giấy ra viện.");
                    return;
                }

                var xuTriKhamBenhId = selectedBenhNhan["XUTRIKHAMBENHID"].ToString();
                if (!"1".Equals(xuTriKhamBenhId) && !"9".Equals(xuTriKhamBenhId) && !"3".Equals(xuTriKhamBenhId))
                {
                    MessageBox.Show("Bệnh nhân không phải xử trí ra viện, In phiếu chỉ với những bệnh nhân ra viện/khác.");
                    return;
                }

                var hoppital_id = Const.local_user.HOSPITAL_ID;
                var db_schema = Const.local_user.DB_SCHEMA;

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");

                table.Rows.Add("khambenhid", "String", khamBenhId);
                table.Rows.Add("i_hid", "String", hoppital_id);
                table.Rows.Add("i_sch", "String", db_schema);

                PrintPreview("In Giấy Ra Viện", "NTU009_GIAYRAVIEN_01BV01_QD4069_A5", table);
            }
        }

        private void btnGiayChuyenVien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var khamBenhId = selectedBenhNhan["KHAMBENHID"].ToString();
                if (string.IsNullOrEmpty(khamBenhId) || "-1".Equals(khamBenhId))
                {
                    MessageBox.Show("Hãy chọn bệnh nhân muốn in giấy ra viện.");
                    return;
                }

                var xuTriKhamBenhId = selectedBenhNhan["XUTRIKHAMBENHID"].ToString();
                if (!"7".Equals(xuTriKhamBenhId))
                {
                    MessageBox.Show("Bệnh nhân không phải xử trí chuyển viện, In phiếu chỉ với những bệnh nhân chuyển viện.");
                    return;
                }

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");

                table.Rows.Add("khambenhid", "String", khamBenhId);

                PrintPreview("In Giấy Chuyển Viện", "NGT003_GIAYCHUYENTUYEN_TT14_A4", table);
            }
        }

        private void btnGiayHenKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var khamBenhId = selectedBenhNhan["KHAMBENHID"].ToString();
                if (string.IsNullOrEmpty(khamBenhId) || "-1".Equals(khamBenhId))
                {
                    MessageBox.Show("Hãy chọn bệnh nhân muốn in giấy ra viện.");
                    return;
                }

                var obj = new
                {
                    KHAMBENHID = khamBenhId
                };

                var objJson = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");

                var ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K001.HK", objJson);
                int rs = 0;
                int.TryParse(ret, out rs);

                if (rs > 0)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("name");
                    table.Columns.Add("type");
                    table.Columns.Add("value");

                    table.Rows.Add("khambenhid", "String", khamBenhId);

                    PrintPreview("In Giấy Hẹn Khám", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table);
                }
                else
                {
                    MessageBox.Show("Không có thông tin hẹn khám của bệnh nhân này.");
                }
            }
        }

        private void InPhoiVP(string inBangKeChuan, string tiepNhanId, string code)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");

            table.Rows.Add("inbangkechuan", "String", inBangKeChuan);
            table.Rows.Add("tiepnhanid", "String", tiepNhanId);

            PrintPreview("In Bảng Kê", code, table);
        }

        private void menuBangKe_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var tiepNhanId = selectedBenhNhan["TIEPNHANID"].ToString();
                var doiTuongBenhNhanId = selectedBenhNhan["DOITUONGBENHNHANID"].ToString();

                var flag = RequestHTTP.call_ajaxCALL_SP_I("VPI01T004.10", tiepNhanId);
                if ("1".Equals(doiTuongBenhNhanId))
                {
                    InPhoiVP("1", tiepNhanId, "NGT001_BKCPKCBBHYTNGOAITRU_01BV_QD3455_A4");
                    if ("1".Equals(flag))
                    {
                        InPhoiVP("1", tiepNhanId, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                    }
                }
                else if ("1".Equals(flag))
                {
                    InPhoiVP("1", tiepNhanId, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                }
            }
        }

        private void OpenReportCLS(string reportName, string khamBenhId, int loaiNhomMauBenhPham)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");

            table.Rows.Add("khambenhid", "String", khamBenhId);
            table.Rows.Add("i_loainhommaubenhpham", "String", loaiNhomMauBenhPham);

            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint("In Phiếu Chỉ Định CLS", reportName, table, 650, 900);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void btnPhieuChiDinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var khamBenhId = selectedBenhNhan["KHAMBENHID"].ToString();
                var hoppital_id = Const.local_user.HOSPITAL_ID;
                var db_schema = Const.local_user.DB_SCHEMA;
                if ("922".Equals(hoppital_id))
                {
                    OpenReportCLS("PHIEU_CLSC_922", khamBenhId, 1);
                    OpenReportCLS("PHIEU_CLSC_922", khamBenhId, 2);
                    OpenReportCLS("PHIEU_CLSC_922", khamBenhId, 5);
                }
                else
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("name");
                    table.Columns.Add("type");
                    table.Columns.Add("value");

                    table.Rows.Add("i_hid", "String", hoppital_id);
                    table.Rows.Add("i_sch", "String", db_schema);
                    table.Rows.Add("khambenhid", "String", khamBenhId);

                    PrintPreview("In Phiếu Chỉ Định CLS", "PHIEU_CLSC", table);
                }
            }
        }

        private void OpenReportPhieuKhamBenh(string khamBenhId, string code)
        {
            var hoppital_id = Const.local_user.HOSPITAL_ID;
            var db_schema = Const.local_user.DB_SCHEMA;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");

            table.Rows.Add("i_hid", "String", hoppital_id);
            table.Rows.Add("i_sch", "String", db_schema);
            table.Rows.Add("khambenhid", "String", khamBenhId);

            PrintPreview("In Phiếu Khám Bệnh", code, table);
        }

        private void btnPhieuKhamBenh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var khamBenhId = selectedBenhNhan["KHAMBENHID"].ToString();
                if (string.IsNullOrEmpty(khamBenhId) || "-1".Equals(khamBenhId))
                {
                    MessageBox.Show("Hãy chọn bệnh nhân muốn in.");
                    return;
                }

                var doiTuongBenhNhanId = selectedBenhNhan["DOITUONGBENHNHANID"].ToString();
                if ("3".Equals(doiTuongBenhNhanId))
                {
                    OpenReportPhieuKhamBenh(khamBenhId, "NGT022_GIAYKCBTHEOYEUCAU_05BV01_QD4069_A4");
                }
                else
                {
                    OpenReportPhieuKhamBenh(khamBenhId, "NGT005_PHIEUKBVAOVIENCHUNG_42BV01_QD4069_A4");
                }
            }
        }

        private void btnInToDieuTri_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var benhNhanId = selectedBenhNhan["BENHNHANID"].ToString();
                var khamBenhId = selectedBenhNhan["KHAMBENHID"].ToString();
                var phongId = Const.local_phongId;
                var type = "docx";

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");

                table.Rows.Add("i_benhnhanid", "String", benhNhanId);
                table.Rows.Add("i_khambenhid", "String", khamBenhId);
                table.Rows.Add("i_phongid", "String", phongId);
                
                string fname = "NGT020_TODIEUTRI_39BV01_QD4069_A4_" + DateTime.Now.ToString("ddMMyy-HHmmss") + "." + type;
                Func.PrintFile_FromData("NGT020_TODIEUTRI_39BV01_QD4069_A4_965", table, type, fname);
            }
        }

        private void btnInBangKeVTHaoPhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucTabDanhSachBenhNhan1.ucGrid_DsBN.SelectedRow;
            if (selectedBenhNhan != null)
            {
                var tiepNhanId = selectedBenhNhan["TIEPNHANID"].ToString();
                var doiTuongBenhNhanId = selectedBenhNhan["DOITUONGBENHNHANID"].ToString();

                var flag = RequestHTTP.call_ajaxCALL_SP_I("VPI01T004.10", tiepNhanId);

                InPhoiVP("1", tiepNhanId, "NGT001_BKCPKCB_HAOPHI_01BV_QD3455_A4");
            }
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                { 
                     HOSO_QUANLYSUCKHOECANHAN frm = new HOSO_QUANLYSUCKHOECANHAN();
                     frm.setData(
                        Const.drvBenhNhan["KHAMBENHID"].ToString()
                        , Const.drvBenhNhan["BENHNHANID"].ToString()
                        , Const.drvBenhNhan["MABENHNHAN"].ToString()
                        );
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            } 
        }

        private void menuTaiNanThuongTich_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                { 
                    NTU02D035_Thongtintainanthuongtich frm = new NTU02D035_Thongtintainanthuongtich();
                    frm.setData(Const.drvBenhNhan["KHAMBENHID"].ToString(), Const.drvBenhNhan["BENHNHANID"].ToString(), Const.drvBenhNhan["HOSOBENHANID"].ToString());
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void menuPhieuVanChuyen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NGT02K016_ChiDinhDichVu frm = new NGT02K016_ChiDinhDichVu();
                    frm.loadData("phieuvanchuyen", Const.drvBenhNhan, _flgModeView, "14"); 
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1"); 
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                { 
                    NTU01H021_PhieuTamUngBenhNhan frm = new NTU01H021_PhieuTamUngBenhNhan();
                    frm.setData(
                       Const.drvBenhNhan["KHAMBENHID"].ToString()
                       , Const.drvBenhNhan["BENHNHANID"].ToString()
                       , Const.drvBenhNhan_ChiTiet["DENKHAMLUC"].ToString()
                       );
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
            
        }

        private void menuNhapBenhAn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    NTU01H031_NhapBenhAn frm = new NTU01H031_NhapBenhAn();
                    frm.Load_Data(
                        Const.drvBenhNhan["KHAMBENHID"].ToString(),
                        Const.drvBenhNhan["HOSOBENHANID"].ToString(),
                        Const.drvBenhNhan["BENHNHANID"].ToString()
                        ); 
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            XuTriBenhNhan(1);
        }


        #region Sự kiện xử lý khi có Kết quả trả về từ form con

        // Hàm nhận về kết quả xử lý của form con (chỉ của 2 form: Thuốc + Dịch vụ) (có thể qua form con trung gian là: các tab xét nghiệm, thuốc, ... hoặc Phiếu KH, KB hỏi bệnh)
        private void listenFrm_KetQua_Thuoc_ChiDinhDV(object sender, EventArgs e)
        {
            string mess = (string)sender;

            if (mess == "mo_form_xu_tri")  // từ form Cấp thuốc, lưu đơn thuốc thành công sẽ mở form xử trí
            {
                menuXuTriKB_ItemClick(null, null);
            }
            else if (!string.IsNullOrEmpty(mess)) MessageBox.Show(mess);

            //Cập nhật lại các tab: tiêu đề và dl
            reload_title_of_Tabs();
        }
        // Hàm gọi lại của form con (các tab xét nghiệm, thuốc, ...) để mở ra form: Thuốc + Dịch vụ
        private void listenFrm_Mo_Thuoc_ChiDinhDV(object sender, EventArgs e)
        {
            ojbDatarowview data = (ojbDatarowview)sender; // capnhatxetnghiem

            if (data.key == "updatePHIEUVATTU" || data.key == "travattu" || data.key == "updatePHIEUTHUOC" || data.key == "tradonthuoc"
                || data.key == "tao_phieu_thuoc_di_kem" || data.key == "tao_phieu_vat_tu_di_kem" || data.key == "tao_phieu_thuoc_di_kem_hao_phi"
                || data.key == "tao_phieu_vat_tu_di_kem_hao_phi"
                )
            {
                NTU02D010_CapThuoc frm = new NTU02D010_CapThuoc();
                frm.loadData(data.key, data.drv);
                frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                openForm(frm, "1");
            }
            else if (data.key == "cap_nhat_don_thuoc_khong_thuoc")// đổi với form Thuốc ko thuốc
            {
                _openDialogThuoc_KhongThuoc(data.key);
            }
            else // form chỉ định dv
            {
                NGT02K016_ChiDinhDichVu frm = new NGT02K016_ChiDinhDichVu();
                frm.loadData(data.key, data.drv, _flgModeView, "5");
                if (data.key == "phieu_mien_giam") frm.set_DICHVUKHAMBENHID(data.id.ToString());
                frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                openForm(frm, "1");
            }
        }

        #endregion
    }
}