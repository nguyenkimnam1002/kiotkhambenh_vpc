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
using VNPT.HIS.CommonForm.Class;
using VNPT.HIS.Common;
using System.Globalization;
using VNPT.HIS.CommonForm;
using Newtonsoft.Json;
//Mục đích  : Giao diện màn hình
//   + Chỉ định thuốc
//   + Chỉ định vật tư
//   + Trả Thuốc
//   + Trả vật tư
//   ....(các chức năng liên quan chỉ định thuốc, vật tư)
//Tham số vào : 
//   khambenhid 		: Mã khám bệnh ID 
//   maubenhphamid 	: Mẫu bệnh phẩm ID, để load thông tin về phiếu đã được chỉ định
//   OPTION 			: Tham số định nghĩa loại màn hình tương ứng
//       + 	OPTION 	= '02D010' -> Màn hình chỉ định thuốc
//       + 	OPTION 	= '02D014' -> Màn hình phiếu trả thuốc
//       + 	OPTION 	= '02D015' -> Màn hình phiếu vật tư
//       + 	OPTION 	= '02D016' -> Màn hình phiếu trả vật tư
//   LOAIKEDON 		: Nếu là Ngoại trú truyền vào là 0; Nội trú theo dựa theo 1 biến cấu hình rồi truyền vào.
//       +	LOAIKEDON = 1 -> Kê số lượng thuốc tổng hợp  --> trên web sử dụng ô txtSOLUONG_TONG; ở đây sử dụng luôn txtSOLUONG_CHITIET, và ẩn các ô (sáng, trưa, chiều, tối) đi
//       +	LOAIKEDON = 0 -> Kê đơn chi tiết (sáng, trưa, chiều, tối)  --> sử dụng ô txtSOLUONG_CHITIET
namespace VNPT.HIS.NgoaiTru
{
    public partial class NTU02D010_CapThuoc : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        // biến đã oke: ĐƯỢC GÁN KHI SELECT 1 loại thuốc trong lookup 
        private string _nhom_mabhyt_id = "";
        private string _tyle_bhyt_tt_tvt = ""; //TYLEBHYT_TVT

        // Các biến được gán khi sửa phiếu, sẽ load ra dl trước
        private double _TRAN_BHYT = 0;
        private double _TYLE_BHYT_TT = 0;
        private double _TONGTIENDV_BH = 0;
        private double _tong_tien_dv_tt = 0;
        private string _tongtien = "0";
        private string _tientu = "0";
        private string _danop = "0";


        private string _loai_don = "";
        private string _loainhommaubenhpham_id = "";
        private string _kechungthuocvt = "0";
        private string _hinhthuc_kho = "";
        private string _loai_tvt_kho = "";
        private string _doituongbenhnhanid = "";
        private string r_checkicd = "0";
        private string _loai_khothuoc = "";
        private string _srch_hoatchat = "0";
        private string _lbl_kho = "";
        private string _lbl_text = "";
        public string _kieutra = "";
        private string _badaingay = "0";
        private string _luu = "0";
        private string hdHUONGDANTHUCHIEN = "";
        private string _GLB_CACH_TINH_TIEN = "1";//0: Tinh binh thuong; 1: Tinh duoi gia tran


        private string hidCANHBAOSOLUONG = "";
        private string hdSOLUONGKHADUNG = "100000";
        private string _macdinh_hao_phi = "0";
        //private string _loainhom = ""; 

        string title_macdinh_cbo_KhoThuoc = "--- Chọn ---";

        public NTU02D010_CapThuoc()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(Const.screen.Width - 50, this.Size.Height);
        }
        // CÁC THAM SỐ ĐƯỢC TRUYỀN VÀO
        string OPTION;
        string LOAIKEDON;
        string KHAMBENHID;
        string MABENHNHAN;
        string MAUBENHPHAMID;
        string DICHVUCHAID;
        string BENHNHANID;
        string i_action = "";

        string menu;
        DataRowView drv;
        public void loadData(string menu, DataRowView drv)
        {
            this.menu = menu;
            this.drv = drv;
            loadData_local(this.menu, this.drv);
        }
        private void loadData_local(string menu, DataRowView drv)
        {
            if (drv.DataView.Table.Columns.Contains("KHAMBENHID")) this.KHAMBENHID = drv["KHAMBENHID"].ToString();
            if (drv.DataView.Table.Columns.Contains("MABENHNHAN")) this.MABENHNHAN = drv["MABENHNHAN"].ToString();
            if (drv.DataView.Table.Columns.Contains("MAUBENHPHAMID")) this.MAUBENHPHAMID = drv["MAUBENHPHAMID"].ToString();
            if (drv.DataView.Table.Columns.Contains("BENHNHANID")) this.BENHNHANID = drv["BENHNHANID"].ToString();

            #region THUỐC
            if (menu == "chi_dinh_thuoc")
            {
                this.Text = "Chỉ định thuốc"; 

                this.OPTION = "02D010";
                this.LOAIKEDON = "0";
                this.DICHVUCHAID = ""; //trên web truyền rỗng    
                this.i_action = "Add";
            }
            else if (menu == "tao_phieu_thuoc_di_kem_hao_phi")
            {
                //  var _msg = "Tạo phiếu thuốc đi kèm hao phí"; 
                //                  khambenhid: $("#hidKHAMBENHID").val(),
                //maubenhphamid: "",
                //loaikedon: 1,
                //dichvuchaid: rowData.DICHVUKHAMBENHID,
                //opt: "02D010"
                //               macdinh_hao_phi: 9
                this.Text = "Tạo phiếu thuốc đi kèm hao phí"; 

                this.MABENHNHAN = "";
                this.MAUBENHPHAMID = "";
                this.BENHNHANID = "";
                this.OPTION = "02D010";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = drv["DICHVUKHAMBENHID"].ToString();
                _macdinh_hao_phi = "9";
            }
            else if (menu == "tao_phieu_thuoc_di_kem")
            {
                //    var _opt = "02D010"
                //var _msg = "Tạo phiếu thuốc đi kèm";
                //      paramInput ={
                //      khambenhid: $("#hidKHAMBENHID").val(),
                //maubenhphamid: "",
                //loaikedon: 1,
                //dichvuchaid: rowData.DICHVUKHAMBENHID,
                //opt: _opt // tao phieu thuoc     					

                //       };

                this.Text = "Tạo phiếu thuốc đi kèm"; 

                this.OPTION = "02D010";
                this.MABENHNHAN = "";
                this.MAUBENHPHAMID = "";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = drv["DICHVUKHAMBENHID"].ToString();
            }
            else if (menu == "don_thuoc_mua_ngoai")
            {
                //               paramInput ={
                //               khambenhid: $("#hidKHAMBENHID").val(),
                //                  maubenhphamid: "",
                //                  loaikedon: 1,
                //                  dichvuchaid: "",
                //                  opt: "02D011" 
                //                };
                //               dlgPopup = DlgUtil.buildPopupUrl("divDlgTaoPhieuThuoc" + "02D011", "divDlg", "manager.jsp?func=../noitru/NTU02D010_CapThuoc", paramInput, "Tạo đơn thuốc mua ngoài", 1300, 590);
                //               DlgUtil.open("divDlgTaoPhieuThuoc" + "02D011");
                this.Text = "Tạo đơn thuốc mua ngoài";
                  
                this.OPTION = "02D011";
                this.MAUBENHPHAMID = "";
                this.LOAIKEDON = "0"; // khi gọi truyền 1, vào trong lại fix=0
                this.DICHVUCHAID = ""; //trên web truyền rỗng  

                  title_macdinh_cbo_KhoThuoc = "--- Mua ngoài ---";
            }
            else if (menu == "mua_thuoc_nha_thuoc")
            {
                //        khambenhid: $("#hidKHAMBENHID").val(),
                //mabenhnhan: $("#txtMABENHNHAN").val(),
                //maubenhphamid: "",
                //opt: 02D019,
                //phongId: $('#hidPHONGID').val(), 
                //loaikedon: 0,
                //dichvuchaid: ''
                //    dlgPopup = DlgUtil.buildPopupUrl("dlgCDT", "divDlg", "manager.jsp?func=../noitru/NTU02D010_CapThuoc", myVar, "Mua thuốc nhà thuốc", 1300, 600);
                this.Text = "Mua thuốc nhà thuốc";
                 
                this.OPTION = "02D019";
                this.MAUBENHPHAMID = "";
                this.LOAIKEDON = "0";
                this.DICHVUCHAID = ""; //trên web truyền rỗng  

                _loainhommaubenhpham_id = "7";
            }
            else if (menu == "updatePHIEUTHUOC")
            {
                this.Text = "Cập nhật phiếu thuốc"; 

                if (drv["LOAIDONTHUOC"].ToString() == "1") this.Text = "Cập nhật phiếu thuốc không thuốc";

                this.OPTION = "";
                if (drv.DataView.Table.Columns.Contains("TENKHO") == false || drv["TENKHO"].ToString() == "")
                    this.OPTION = "02D011";
                if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() == "1") this.OPTION = "02D010";
                else if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() == "2") this.OPTION = "02D014";

                this.LOAIKEDON = "0";
                if (drv["LOAIKEDON"].ToString() == "1") this.LOAIKEDON = "0";
                else this.LOAIKEDON = "1";

                this.DICHVUCHAID = ""; //trên web truyền rỗng  

                //lay loai thuoc 
                DataTable arr_loaithuoc = RequestHTTP.call_ajaxCALL_SP_O("NTU02D033_LOAITHUOC", drv["MAUBENHPHAMID"].ToString(), 0);
                if (arr_loaithuoc.Rows.Count > 0)
                {
                    if (arr_loaithuoc.Rows[0]["LOAI"].ToString() == "3")
                    {
                        this.OPTION = "02D017";
                        this.LOAIKEDON = "1";
                        this.Text = "Cập nhật thuốc YHCT";
                    }
                }

                this.i_action = "Upd";

                //tiepnhanid: drv["tiepnhanid"].ToString()
                //                  hosobenhanid: drv["hosobenhanid"].ToString()
                //                  doituongbenhnhanid: drv["doituongbenhnhanid"].ToString()  

            }
            else if (menu == "tradonthuoc")
            {
                this.Text = "Tạo phiếu trả thuốc"; 

                this.OPTION = "02D014";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = ""; //trên web truyền rỗng 
                this._kieutra = "1";
            }
            #endregion
            #region VẬT TƯ
            if (menu == "updatePHIEUVATTU")
            {
                this.Text = "Cập nhật phiếu vật tư"; 

                this.OPTION = "";
                if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() == "1") this.OPTION = "02D015";
                else if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() == "2") this.OPTION = "02D016";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = "";
            }
            else if (menu == "tao_phieu_vat_tu_di_kem_hao_phi")
            {
                //  var _msg = "Tạo phiếu vật tư đi kèm hao phí";
                //  paramInput ={
                //  khambenhid: _self.options._khambenhid,
                //maubenhphamid: "",
                //loaikedon: 1,
                //dichvuchaid: rowData.DICHVUKHAMBENHID,
                //opt: 02D015
                //               macdinh_hao_phi: 9
                this.Text = "Tạo phiếu vật tư đi kèm hao phí"; 

                this.MABENHNHAN = "";
                this.MAUBENHPHAMID = "";
                this.BENHNHANID = "";
                this.OPTION = "02D015";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = drv["DICHVUKHAMBENHID"].ToString();
                _macdinh_hao_phi = "9";
            }
            else if (menu == "tao_phieu_vat_tu_di_kem")
            {
                this.Text = "Tạo phiếu vật tư đi kèm"; 

                this.MABENHNHAN = "";
                this.MAUBENHPHAMID = "";
                this.BENHNHANID = "";
                this.OPTION = "02D015";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = drv["DICHVUKHAMBENHID"].ToString();
            }

            else if (menu == "travattu")
            {
                this.Text = "Tạo phiếu trả vật tư"; 

                this.OPTION = "02D016";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = ""; //trên web truyền rỗng 
                this._kieutra = "1";
            }
            else if (menu == "tao_phieu_vat_tu")
            {
                this.Text = "Chỉ định vật tư"; 

                this.OPTION = "02D015";
                this.LOAIKEDON = "0";
                this.DICHVUCHAID = ""; //trên web truyền rỗng  
            }
            #endregion
            #region ĐÔNG Y
            else if (menu == "tao_don_thuoc_dongy") 
            {
                this.Text = "Chỉ định thuốc YHCT"; 

                this.OPTION = "02D017";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = ""; //trên web truyền rỗng  
                this.MAUBENHPHAMID = "";

                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else if (menu == "tao_phieu_tra_thuoc_dongy")
            {
                this.Text = "Trả thuốc YHCT"; 

                this.OPTION = "02D018";
                this.LOAIKEDON = "1";
                this.DICHVUCHAID = ""; //trên web truyền rỗng  
                this.MAUBENHPHAMID = "";
            }

            #endregion


            set_tieu_de_form(this.OPTION);

            if (LOAIKEDON == "1")
            {

                layoutControlItem_toi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_chieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_trua.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_sang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_ThoiGian.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_SoNgay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutitemcontrol12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            }
        } 

        private void set_tieu_de_form(string _option)
        {
            if (_option == "02D010")
            {//Phieu thuoc 
             //            r_checkicd = "1";
             //            _loai_khothuoc = "2,3,8,10";
                _loai_don = "1";
                //            _loainhommaubenhpham_id = "7"; 
                //            _gridCaption = ""; 
                //            _srch_hoatchat = 1;
                //            _lbl_kho = "Kho thuốc";
                //            _lbl_text = " thuốc";
                //            if (_dichvucha_id != "")
                //$("#dvHeaderName").text("Phiếu cấp thuốc đi kèm dịch vụ");
                //   else
                //$("#dvHeaderName").text("Phiếu cấp thuốc"); 
                //            setTextToButton("btnAdd", "Thêm thuốc", "glyphicon glyphicon-pencil");
                //            setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                //            _loai_tvt_kho = 0;  

                layoutControlItem_txtSONGAY_KE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                layoutControlItem_PhieuDieuTri.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //$("#divDonThuocVT").hide(); >Đơn thuốc/VT trả
                //$("#btnTraAllPhieu").hide(); Trả cả phiếu
                layoutControlItem_ucComboBoxLoaiThuoc.Text = "Loại thuốc";
                //$("#dvLOAI_THUOC").show(); --> Ko tồn tại 

                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (_option == "02D011")
            {//Don thuoc mua ngoai 
             //             _loai_khothuoc = "4";
                _loai_don = "1";
                //             _loaikedon = 0;
                //             _srch_hoatchat = 1;
                //             _loainhommaubenhpham_id = "7";
                //             _lbl_kho = "Kho thuốc";
                //             _lbl_text = " thuốc";
                //$("#dvHeaderName").text("Đơn thuốc mua ngoài");
                //$("#lbSearchName").text("Tên thuốc/ tên hoạt chất");
                //$("#lbTenThuoc").text("Tên hoạt chất");
                //             setTextToButton("btnAdd", "Thêm thuốc", "glyphicon glyphicon-pencil");
                //             setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                //             _loai_tvt_kho = 0;
                //$("#grMenuRight").css("display", 'none');


                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide();

                //$("#divPhieuDT").show();
                layoutControlItem_PhieuDieuTri.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //$("#divSLTHANG").hide();
                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#dvlLIEU_DUNG").hide();  --> trên web chỉ là hàng label tieu đề
                //$("#dvLIEU_DUNG").hide();
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#cboDVQUYDOI").hide(); Đơn vị quy đổi
            }
            else if (_option == "02D014")
            {
                //             _loai_khothuoc = "2,3,8,10";
                _loai_don = "2";
                //             _loainhommaubenhpham_id = "7";
                //             _srch_hoatchat = 1;  
                //             _lbl_kho = "Kho thuốc";
                //             _lbl_text = " thuốc"; 
                //             if (_dichvucha_id != "")
                //	$("#dvHeaderName").text("Phiếu trả thuốc đi kèm dịch vụ");
                //else
                //	$("#dvHeaderName").text("Phiếu trả thuốc");
                //$("#lbSearchName").text("Tên thuốc/ tên hoạt chất");
                //$("#lbTenThuoc").text("Tên hoạt chất");
                //$("#grMenuRight").css("display", 'none'); 
                //_loai_tvt_kho = 0;

                //$("#btnDTMau").hide();
                btnDonThuocMau.Visible = false;
                //$("#btnDTCu").hide();
                btnDonThuocCu.Visible = false;
                //$("#btnTConSD").hide();	
                btnThuocConSD.Visible = false;
                //$('#btnTDiUng').hide(); 
                btnDiUngThuoc.Visible = false;
                //$("#txtTEXT_TEMP").hide();
                //$("#btnSaveTemp").hide();
                panelControl_footer_right.Visible = false;

                //$("#lblLoaiTra").text("Đơn thuốc trả");			
                //$("#divDonThuocVT").show();
                //$("#btnTraAllPhieu").show();

                //$("#divPhieuDT").hide();
                layoutControlItem_PhieuDieuTri.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#dvLOAI_THUOC").hide(); --> Ko tồn tại 

                //$("#divSLTHANG").hide();
                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#btnXuTri").hide();
                btnXuTri.Visible = false;

                //$("#dvlLIEU_DUNG").hide();
                //$("#dvLIEU_DUNG").hide();
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#cboDVQUYDOI").hide(); Đơn vị quy đổi
            }
            else if (_option == "02D015")
            {//Phieu vat tu 
             //             r_checkicd = "1";
             //             _loai_khothuoc = "7, 9, 11";
                _loai_don = "3";
                //             _loainhommaubenhpham_id = "8"; 
                //             _lbl_kho = "Kho vật tư";
                //             _lbl_text = " vật tư";  
                //         if (_dichvucha_id != "")
                //	$("#dvHeaderName").text("Phiếu cấp vật tư đi kèm dịch vụ");
                //else
                //	$("#dvHeaderName").text("Phiếu cấp vật tư"); 
                //$("#lbSearchName").text("Tên vật tư");
                //$("#lbTenThuoc").text("Tên vật tư");
                //             _loai_tvt_kho = 1;

                btnThemThuoc.Text = "Thêm vật tư";

                //$("#lblloaithuoc").text("Loại vật tư");
                cbo_LoaiThuoc.Caption = "Loại vật tư";
                layoutControlIThuoc.Text = "Tên vật tư<color=red>(*)";
                cbo_KhoThuoc.Caption = "Kho vật tư";

                btnDonThuocCu.Text = "Mẫu VT cũ";
                btnDonThuocMau.Text = "Mẫu VT";
			  
			    //$("#btnTConSD").hide();
                btnThuocConSD.Visible = false; 
			    //$('#btnTDiUng').hide();
                btnDiUngThuoc.Visible = false;
                //$("#txtTEXT_TEMP").hide();
                //$("#btnSaveTemp").hide();
                panelControl_footer_right.Visible = false;
                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide(); 

                //$("#divSLTHANG").hide();
                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //$("#btnXuTri").hide();
                btnXuTri.Visible = false;
                //$("#dvlLIEU_DUNG").hide();
                //$("#dvLIEU_DUNG").hide();
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never; 
            }
            else if (_option == "02D016")
            {//Phieu tra vat tu
             //             _loai_khothuoc = "7, 9, 11";
                _loai_don = "4";
                //             _loainhommaubenhpham_id = "8";  
                //             _loaikedon = 1;
                //             _lbl_kho = "Kho vật tư";
                //             _lbl_text = " vật tư";
                //             if (_dichvucha_id != "")
                //	$("#dvHeaderName").text("Phiếu trả vật tư đi kèm dịch vụ");
                //else
                //	$("#dvHeaderName").text("Phiếu trả vật tư");
                //$("#lbSearchName").text("Tên vật tư");
                //$("#lbTenThuoc").text("Tên vật tư");
                //_loai_tvt_kho = 1; 

                btnThemThuoc.Text = "Thêm vật tư"; 
                //$("#btnDTMau").hide();
                btnDonThuocMau.Visible = false;
                //$("#btnDTCu").hide();
                btnDonThuocCu.Visible = false;
                //$("#btnTConSD").hide();	
                btnThuocConSD.Visible = false;
                //$('#btnTDiUng').hide(); 
                btnDiUngThuoc.Visible = false;
                //$("#txtTEXT_TEMP").hide();
                //$("#btnSaveTemp").hide();
                panelControl_footer_right.Visible = false;
			//$("#lblLoaiTra").text("Phiếu VT trả");
			//$("#divDonThuocVT").show();
			//$("#btnTraAllPhieu").show();

			//$("#divPhieuDT").hide();
                layoutControlItem_PhieuDieuTri.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //$("#divSLTHANG").hide();
                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //$("#btnXuTri").hide();
                btnXuTri.Visible = false;
			//$("#dvlLIEU_DUNG").hide();
			//$("#dvLIEU_DUNG").hide();
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else if (_option == "02D017")
            {//Cấp thuốc đông y
             //             r_checkicd = "1";
             //             _loai_khothuoc = "5";
                _loai_don = "1";
                //             _loainhommaubenhpham_id = "7"; 
                //             _srch_hoatchat = 1;
                //             _lbl_kho = "Kho thuốc";
                //             _lbl_text = " vị thuốc YHCT";
                layoutControlIThuoc.Text = "Tên vị thuốc YHCT<color=red>(*)";

                //             _loai_tvt_kho = 0;

                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide();	
                 

                layoutControlItem31.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //$("#cboDUONG_DUNG").css('display', 'none');

                //$("#txtTG_HENKHAM").val($("#txtSLTHANG").val());
                txtTG_HENKHAM.Text = txtSLTHANG.Text;

                //$("#cboDVQUYDOI").hide();
                //$("#dvlLIEU_DUNG").hide();
                //$("#dvLIEU_DUNG").hide();
            }
            else if (_option == "02D018")
            {//trả thuốc đông y
             //             _loai_khothuoc = "5";
                _loai_don = "1";
                //             _loainhommaubenhpham_id = "7";  
                //             _srch_hoatchat = 1; 
                //             _lbl_text = " vị thuốc YHCT";
                //$("#lbSearchName").text("Tên vị thuốc YHCT");
                //             _loai_tvt_kho = 0;

                btnThemThuoc.Text = "Thêm thuốc trả";
                //$("#btnDTMau").hide();
                btnDonThuocMau.Visible = false;
                //$("#btnDTCu").hide();
                btnDonThuocCu.Visible = false;
                //$("#btnTConSD").hide();	
                btnThuocConSD.Visible = false; 
			    //$('#btnTDiUng').hide();
                btnDiUngThuoc.Visible = false;
			    //$('#btnPdDt').hide();
                btnPhacDo.Visible = false;
                //$("#txtTEXT_TEMP").hide();
                //$("#btnSaveTemp").hide();
                panelControl_footer_right.Visible = false;
                //$("#lblLoaiTra").text("Đơn thuốc trả");
                //$("#divDonThuocVT").show();
                //$("#btnTraAllPhieu").show();

                //$("#divPhieuDT").hide();
                layoutControlItem_PhieuDieuTri.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#divSLTHANG").hide();
                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //$("#btnXuTri").hide();
                btnXuTri.Visible = false;
                //$("#dvlLIEU_DUNG").hide();
                //$("#dvLIEU_DUNG").hide();
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                  
			//$("#lbTenThuoc").hide();
			//$("#dvTENTHUOC").css('display', 'none');
			//$("#lblDUONGDUNG").css('display', 'none');
			//$("#cboDUONG_DUNG").css('display', 'none'); 
			//$("#dvDUONG_DUNG").css('display', 'none');
              
            }
            else if (_option == "02D019")
            {//phiếu thuốc mua từ nhà thuốc 
             //             r_checkicd = "1";
             //             _loai_khothuoc = "4";
                _loai_don = "1";
                //             _loainhommaubenhpham_id = "7"; 
                //             _gridCaption = ""; 
                //             _srch_hoatchat = 1;
                //             _lbl_kho = "Kho thuốc";
                //             _lbl_text = " thuốc";
                //             if (_dichvucha_id != "")
                //	$("#dvHeaderName").text("Phiếu cấp thuốc đi kèm dịch vụ");
                //else
                //	$("#dvHeaderName").text("Phiếu cấp thuốc");
                //$("#lbSearchName").text("Tên thuốc/ tên hoạt chất");
                //$("#lbTenThuoc").text("Tên hoạt chất");
                //             setTextToButton("btnAdd", "Thêm thuốc", "glyphicon glyphicon-pencil");
                //             setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                //             _loai_tvt_kho = 0;


                //$("#divPhieuDT").hide();
                layoutControlItem_PhieuDieuTri.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide();		

                //$("#lblloaithuoc").text("Loại thuốc");
                layoutControlItem_ucComboBoxLoaiThuoc.Text = "Loại thuốc";
                //$("#dvLOAI_THUOC").hide(); --> Ko tồn tại 

                //$("#divSLTHANG").hide();
                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#dvlLIEU_DUNG").hide();
                //$("#dvLIEU_DUNG").hide();
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                //$("#cboDVQUYDOI").hide(); Đơn vị quy đổi
            }
            else if (_option == "02D020")
            {//Phieu thuoc kê BN nội trú ra viện 
             //             r_checkicd = "1";
             //             _loai_khothuoc = "2,3,8,10";
                _loai_don = "1";
                //             _loainhommaubenhpham_id = "7"; 
                //             _loaikedon = 0;
                //             _srch_hoatchat = 1;
                //             _lbl_kho = "Kho thuốc";
                //             _lbl_text = " thuốc";
                //             if (_dichvucha_id != "")
                //	$("#dvHeaderName").text("Phiếu cấp thuốc đi kèm dịch vụ");
                //else
                //	$("#dvHeaderName").text("Phiếu cấp thuốc");
                //$("#lbSearchName").text("Tên thuốc/ tên hoạt chất");
                //$("#lbTenThuoc").text("Tên hoạt chất"); 
                //_loai_tvt_kho = 0;

                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide();		

                //$("#divSLTHANG").hide();
                layoutControlItem_SoThang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        #region INIT FORM
        private void formChiDinhThuocPU_Load_1(object sender, EventArgs e)
        {
            // Khởi tại form Phieu thuoc/Don thuoc mua ngoai/Phieu tra thuoc/Vat tu,...
            init_Form();
            //Khởi tạo
            init_Control1();
            layThongTinBenhNhan_CauHinh();
            init_Control2();// Khởi tạo phụ thuộc vào tt của BN- đã chuyển phần gán _hinhthuc_kho vào init_Control2
            // Lấy dữ liệu bệnh nhân
            layThongTinBenhNhan();

            loadData_local(this.menu, this.drv);
                
            init_ButtonLocation(); // Sắp xếp lại vị trí các button

            this.Activated += AfterLoading;
        }
        string ThongBao = "";
        string ThongBao_check_tien = "-1";
        private void AfterLoading(object sender, EventArgs e)
        {
            this.Activated -= AfterLoading;
            //Write your code here.
            if (ThongBao != "") MessageBox.Show(ThongBao);
            if (ThongBao_check_tien != "" && ThongBao_check_tien != "-1") MessageBox.Show(ThongBao_check_tien);

            ThongBao_check_tien = "";
        }
        /**
        *    Load hiển thị và dữ liệu 
        * */
        private void init_Form()
        {
            #region Khởi tạo các biến, tiêu đề các control,... theo từng loại Form
            if (OPTION == "02D010")
            {//Phieu thuoc
                this.Text = "Chỉ định thuốc";

                r_checkicd = "1";
                _loai_khothuoc = "2,3,8,10";
                _loai_don = "1";
                _loainhommaubenhpham_id = "7";
                _srch_hoatchat = "1";
                _lbl_kho = "Kho thuốc";
                _lbl_text = " thuốc";
                layoutControlIThuoc.Text = "Tên thuốc/ tên hoạt chất<color=red>(*)";
                //layoutControlIHoatChat.Text = "Tên hoạt chất <color=red>(*)";
                //setTextToButton("btnAdd", "Thêm thuốc", "glyphicon glyphicon-pencil");
                //setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                _loai_tvt_kho = "0";
                //$("#divPhieuDT").show();
                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide();		
                //$("#lblloaithuoc").text("Loại thuốc");
                //$("#dvLOAI_THUOC").show();
                //$("#divSLTHANG").hide();
            }
            else if (OPTION == "02D011")
            {//Don thuoc mua ngoai
                _loai_khothuoc = "4";
                _loai_don = "1";
                LOAIKEDON = "0";
                _srch_hoatchat = "1";
                _loainhommaubenhpham_id = "7";
                _lbl_kho = "Kho thuốc";
                _lbl_text = " thuốc";
                //$("#dvHeaderName").text("Đơn thuốc mua ngoài");
                //$("#lbSearchName").text("Tên thuốc");
                //$("#lbTenThuoc").text("Tên hoạt chất");
                //setTextToButton("btnAdd", "Thêm thuốc", "glyphicon glyphicon-pencil");
                //setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                //_loai_tvt_kho 			= 0;
                //$("#grMenuRight").css("display", 'none');
                //$("#divDonThuocVT").hide();
                //$("#divPhieuDT").hide();
                //$("#btnTraAllPhieu").hide();
                //$("#divSLTHANG").hide();
            }
            else if (OPTION == "2D014")
            {//Phieu tra thuoc
                _loai_khothuoc = "2,3,8,10";
                _loai_don = "2";
                _loainhommaubenhpham_id = "7";
                _srch_hoatchat = "1";
                _lbl_kho = "Kho thuốc";
                _lbl_text = " thuốc";
                //Truong hop phieu tra chi hien thi la ke tong 
                //$("#lbSearchName").text("Tên thuốc");
                //$("#lbTenThuoc").text("Tên hoạt chất");
                //$("#grMenuRight").css("display", 'none');

                //setTextToButton("btnAdd", "Thêm thuốc", "glyphicon glyphicon-pencil");
                //setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                _loai_tvt_kho = "0";

                //$("#btnDTMau").hide();
                //$("#btnDTCu").hide();
                //$("#btnTConSD").hide();	
                ////tuyennx_add_start_20170816 yc L2DKBD-195
                //$('#btnTDiUng').hide();
                ////tuyennx_add_end_20170816 yc L2DKBD-195
                //$("#txtTEXT_TEMP").hide();
                //$("#btnSaveTemp").hide();
                //$("#lblLoaiTra").text("Đơn thuốc trả");			
                //$("#divDonThuocVT").show();
                //$("#divPhieuDT").hide();
                //$("#btnTraAllPhieu").show();
                //$("#dvLOAI_THUOC").hide();
                //$("#divSLTHANG").hide();
                //$("#btnXuTri").hide();
            }
            else if (OPTION == "02D015")
            {//Phieu vat tu 
                r_checkicd = "1";
                _loai_khothuoc = "7, 9, 11";
                _loai_don = "3";
                _loainhommaubenhpham_id = "8";
                _lbl_kho = "Kho vật tư";
                _lbl_text = " vật tư";
                //$("#lbSearchName").text("Tên vật tư");
                //$("#lbTenThuoc").text("Tên vật tư");
                //setTextToButton("btnAdd", "Thêm vật tư", "glyphicon glyphicon-pencil");
                //setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                _loai_tvt_kho = "1";

                //$("#btnDTMau").text("Mẫu VT");
                //$("#btnDTCu").text("Mẫu VT Cũ");

                //$("#btnDTMau").show();
                //$("#btnDTCu").show();
                //$("#btnTConSD").hide();	
                ////tuyennx_add_start_20170816 yc L2DKBD-195
                // $('#btnTDiUng').hide();
                // //tuyennx_add_end_20170816 yc L2DKBD-195
                //$("#txtTEXT_TEMP").show();
                //$("#btnSaveTemp").show();
                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide();
                //$("#lblloaithuoc").text("Loại vật tư");
                //$("#dvLOAI_THUOC").show();
                //$("#divSLTHANG").hide();
                //$("#btnXuTri").hide();
                //$("#divPhieuDT").hide();
            }
            else if (OPTION == "02D016")
            {//Phieu tra vat tu
                _loai_khothuoc = "7, 9, 11";
                _loai_don = "4";
                _loainhommaubenhpham_id = "8";
                //Truong hop phieu tra chi hien thi la ke tong
                LOAIKEDON = "1";
                _lbl_kho = "Kho vật tư";
                _lbl_text = " vật tư";
                //$("#lbSearchName").text("Tên vật tư");
                //$("#lbTenThuoc").text("Tên vật tư");
                //setTextToButton("btnAdd", "Thêm vật tư", "glyphicon glyphicon-pencil");
                //setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                _loai_tvt_kho = "1";
                //$("#grMenuRight").css("display", 'none');

                //$("#btnDTMau").hide();
                //$("#btnDTCu").hide();
                //$("#btnTConSD").hide();	
                ////tuyennx_add_start_20170816 yc L2DKBD-195
                //   $('#btnTDiUng').hide();
                //   //tuyennx_add_end_20170816 yc L2DKBD-195
                //$("#txtTEXT_TEMP").hide();
                //$("#btnSaveTemp").hide();
                //$("#lblLoaiTra").text("Phiếu VT trả");
                //$("#divDonThuocVT").show();
                //$("#divPhieuDT").hide();
                //$("#btnTraAllPhieu").show();
                //$("#divSLTHANG").hide();
                //$("#btnXuTri").hide();
            }
            else if (OPTION == "02D017")
            {//Cấp thuốc đông y
                r_checkicd = "1";
                _loai_khothuoc = "5";
                _loai_don = "1";
                _loainhommaubenhpham_id = "7";
                _srch_hoatchat = "1";
                _lbl_kho = "Kho thuốc";
                _lbl_text = " vị thuốc YHCT";
                //$("#lbSearchName").text("Tên vị thuốc YHCT");

                //setTextToButton("btnAdd", "Thêm thuốc", "glyphicon glyphicon-pencil");
                //setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                _loai_tvt_kho = "0";
                //$("#divPhieuDT").show();
                //$("#divDonThuocVT").hide();
                //$("#btnTraAllPhieu").hide();		
                //$("#lblloaithuoc").text("Loại thuốc");
                //$("#lblcachdung").text("Ghi chú");
                //$("#dvLOAI_THUOC").show();

                //$("#lbTenThuoc").hide();
                //$("#dvTENTHUOC").css('display','none');
                //$("#divSLTHANG").show();
                //$("#lblDUONGDUNG").css('display','none');
                //$("#cboDUONG_DUNG").css('display','none');
                //$("#dvTenThuoc").css('display','none');
                //$("#dvDUONG_DUNG").css('display','none');
            }
            else if (OPTION == "02D018")
            {//trả thuốc đông y
                _loai_khothuoc = "5";
                _loai_don = "1";
                _loainhommaubenhpham_id = "7";
                //Truong hop phieu tra chi hien thi la ke tong
                _srch_hoatchat = "1";
                _lbl_kho = "Kho thuốc";
                _lbl_text = " vị thuốc YHCT";
                //$("#lbSearchName").text("Tên vị thuốc YHCT");
                //setTextToButton("btnAdd", "Thêm thuốc trả", "glyphicon glyphicon-pencil");
                //setTextToButton("btnSave", "Lưu", "glyphicon glyphicon-floppy-disk");
                _loai_tvt_kho = "0";
                //$("#btnDTMau").hide();
                //$("#btnDTCu").hide();
                //$("#btnTConSD").hide();	
                ////tuyennx_add_start_20170816 yc L2DKBD-195
                //   $('#btnTDiUng').hide();
                //   //tuyennx_add_end_20170816 yc L2DKBD-195
                //   $('#btnPdDt').hide();
                //$("#txtTEXT_TEMP").hide();
                //$("#btnSaveTemp").hide();
                //$("#lblLoaiTra").text("Đơn thuốc trả");			
                //$("#divDonThuocVT").show();
                //$("#divPhieuDT").hide();
                //$("#btnTraAllPhieu").show();
                //$("#dvLOAI_THUOC").hide();
                //$("#divSLTHANG").hide();

                //$("#lbTenThuoc").hide();
                //$("#dvTENTHUOC").css('display','none');
                //$("#lblDUONGDUNG").css('display','none');
                //$("#cboDUONG_DUNG").css('display','none');
                //$("#dvTenThuoc").css('display','none');
                //$("#dvDUONG_DUNG").css('display','none');
                //$("#btnXuTri").hide();
            }

            #endregion

            if (LOAIKEDON == "1") // ở đây sử dụng luôn txtSOLUONG_CHITIET , và ẩn các ô (sáng, trưa, chiều, tối) đi
            {
                if (OPTION == "02D010" || OPTION == "02D015") layoutControlItem_txtSONGAY_KE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else layoutControlItem_txtSONGAY_KE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                layoutControlItem_sang.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_trua.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_chieu.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem_toi.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                layoutControlItem_txtSONGAY_KE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            //check an hien nut sua ghi chu benh chinh
            //var NGT_GHICHU_BENHCHINH = jsonrpc.AjaxJson.ajaxCALL_SP_I('COM.CAUHINH.THEOMA','NGT_GHICHU_BENHCHINH');
            //if(NGT_GHICHU_BENHCHINH=='1'){
            //    $("#divBc").removeClass("col-md-8");
            //    $("#divBc").addClass("col-md-6");
            //    $('#divSuaBc').css('display','');
            //}


        }

        private void init_Control1()
        {
            //getSystemDate
            dtimeNgayChiDinh.DateTime = Func.getSysDatetime();// = ServiceChiDinhThuoc.getSystemDate("DD/MM/YYYY HH24:MI:SS");
            dtimeNgayDung.DateTime = Func.getSysDatetime();//.Text = ServiceChiDinhThuoc.getSystemDate("DD/MM/YYYY HH24:MI:SS");

            // Load chuẩn đoán và chuẩn đoán kèm theo
            DataTable dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
            ucSearchLookupICD.setData(dt, "ICD10CODE", "ICD10NAME");
            ucSearchLookupICD.setColumn("RN", -1, "", 0);
            ucSearchLookupICD.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucSearchLookupICD.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            ucSearchLookupBP.setData(dt, "ICD10CODE", "ICD10NAME");
            ucSearchLookupBP.setEvent_Enter(ucSearchLookupBP_KeyEnter);
            ucSearchLookupBP.setColumn("RN", -1, "", 0);
            ucSearchLookupBP.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucSearchLookupBP.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
            ucSearchLookupBP.setEvent_Check(ucSearchLookupBP_Check);
            ucSearchLookupBP.btnReset.Visible = true;
            ucSearchLookupBP.btnEdit.Visible = true;
            ucSearchLookupBP.btnReset.Text = "Xóa bệnh KT";
            ucSearchLookupBP.btnEdit.Text = "Sửa BP";

            // Lời dặn BS - {"total": 1,"page": 1,"records": 2,"rows" : [{"RN": "1","LOIDANBS": "Uống thuốc đúng giờ"},{"RN": "2","LOIDANBS": "Ăn cơm trước khi uống thuốc"}] }
            ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging("DMC.LOIDANBS", 1, 100000, new string[] { }, new string[] { }, "");
            dt = MyJsonConvert.toDataTable(ds.rows);
            ucSearchLookup_LoiDan.setData(dt, 0, 1);
            ucSearchLookup_LoiDan.setColumn(0, 0, "STT", 0);
            ucSearchLookup_LoiDan.setColumn(1, 1, "Lời dặn", 0);

            DataRow dr;
            //Phiếu điều trị
            if (layoutControlItem_PhieuDieuTri.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                dt = ServiceChiDinhThuoc.getPhieuDieuTri("NTU02D010.08", this.KHAMBENHID);
                if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                dr = dt.NewRow();
                dr[0] = "-1";
                dr[1] = "--- Chọn ---";
                dt.Rows.InsertAt(dr, 0);
                cbo_PhieuDieuTri.setData(dt, 0, 1);
                cbo_PhieuDieuTri.setColumnAll(false);
                cbo_PhieuDieuTri.setColumn(1, true);
                cbo_PhieuDieuTri.SelectIndex = 0;
                cbo_PhieuDieuTri.setEvent(PhieuDieuTri_Change);
            }

            // Đường dùng - [["610","Bình khí lỏng hoặc nén"],["611","Bình khí nén"],...
            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DuongDung); // {"func":"ajaxExecuteQuery","params":["","NTU02D010.07"],"options":[],"uuid":"eacd7874-d0f1-48e8-980c-4c06a0dc5b58"}
            if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
            dr = dt.NewRow();
            dr[0] = "-1";
            dr[1] = "";
            dt.Rows.InsertAt(dr, 0);
            cbo_DuongDung.setData(dt, 0, 1);
            cbo_DuongDung.setColumnAll(false);
            cbo_DuongDung.setColumn(1, true);

            // Cách dùng thuốc
            ds = RequestHTTP.get_ajaxExecuteQueryPaging("DMC.CACHDUNG.THUOC", 1, 100000, new string[] { "[0]" }, new string[] { "1" }, "");
            dt = MyJsonConvert.toDataTable(ds.rows);
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            for (int i=0; i< dt.Rows.Count; i++) collection.Add(dt.Rows[i]["CACHDUNG"].ToString());
            lookup_CachDung.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            lookup_CachDung.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            lookup_CachDung.MaskBox.AutoCompleteCustomSource = collection;

            //lookup_CachDung.setData(dt, "CACHDUNG", "CACHDUNG");
            //lookup_CachDung.setColumn("RN", -1, "", 0);
            //lookup_CachDung.setColumn("CACHDUNG", 0, "Cách dùng", 0);
            //lookup_CachDung.searchLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            //lookup_CachDung.setEvent(lookup_CachDung_Change);
             


            // thời gian dùng thuốc
            ds = RequestHTTP.get_ajaxExecuteQueryPaging("DMC.CACHDUNG.THUOC", 1, 100000, new string[] { "[0]" }, new string[] { "2" }, "");
            dt = MyJsonConvert.toDataTable(ds.rows);
            lookup_ThoiGian.setData(dt, 0, 1);
            lookup_ThoiGian.setColumn(0, -1, "", 0);
            lookup_ThoiGian.setColumn(1, 0, "Thời gian", 0);
            lookup_ThoiGian.searchLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            lookup_ThoiGian.setEvent(lookup_ThoiGian_Change);

            // Lookup thuốc
            lookup_Thuoc.searchLookUpEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ucSearchLookupThuoc_KeyDown);
            lookup_Thuoc.searchLookUpEdit1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ucSearchLookupThuoc_MouseClick);
            lookup_Thuoc.setEvent(ucSearchLookupThuoc_OnChange);

            // khởi tạo Bảng các thuốc đã kê           
            #region 
            dataRowAdd.Columns.Add("TEN_THUOC");
            dataRowAdd.Columns.Add("HOATCHAT");
            dataRowAdd.Columns.Add("DUONG_DUNG");
            dataRowAdd.Columns.Add("DON_GIA");  // bảng lookup tên là: GIA_BAN
            dataRowAdd.Columns.Add("SOLUONG");
            dataRowAdd.Columns.Add("THANH_TIEN");
            dataRowAdd.Columns.Add("BH_TRA");
            dataRowAdd.Columns.Add("ND_TRA");
            dataRowAdd.Columns.Add("LOAI_DT_CU");
            dataRowAdd.Columns.Add("LOAI_DT_MOI");
            dataRowAdd.Columns.Add("DUONGDUNGE");
            dataRowAdd.Columns.Add("DONVI_TINH");
            dataRowAdd.Columns.Add("GIATRANBHYT");
            dataRowAdd.Columns.Add("NHOM_MABHYT_ID");
            dataRowAdd.Columns.Add("TYLEBHYT_TVT");
            dataRowAdd.Columns.Add("THUOCVATTUID");
            dataRowAdd.Columns.Add("ID_DT_MOI");
            dataRowAdd.Columns.Add("STT");
            dataRowAdd.Columns.Add("ID_DT_CU");
            dataRowAdd.Columns.Add("MA_THUOC");
            dataRowAdd.Columns.Add("HUONGDAN_SD");
            dataRowAdd.Columns.Add("DUONGDUNGID");
            dataRowAdd.Columns.Add("DICHVUKHAMBENHID");
            dataRowAdd.Columns.Add("ACTION");
            dataRowAdd.Columns.Add("MAHOATCHAT");
            dataRowAdd.Columns.Add("OLDVALUE");
            dataRowAdd.Columns.Add("KHOANMUCID");
            dataRowAdd.Columns.Add("LIEUDUNG");
            dataRowAdd.Columns.Add("KHO_THUOCID");
            dataRowAdd.Columns.Add("DVQD");
            dataRowAdd.Columns.Add("KETRUNGHOATCHAT");
            dataRowAdd.Columns.Add("LOAITVTID"); 
            dataRowAdd.Columns.Add("THUOCSAO");

            dataRowAdd.Columns.Add("up");
            dataRowAdd.Columns.Add("down");
            dataRowAdd.Columns.Add("delete"); 
            #endregion


            ucDSThuoc.gridView.OptionsView.ShowViewCaption = false;
            ucDSThuoc.gridView.OptionsView.ShowAutoFilterRow = false;
            ucDSThuoc.gridView.OptionsView.ColumnAutoWidth = true;
            ucDSThuoc.Set_HidePage(true);
            ucDSThuoc.onIndicator();
            ucDSThuoc.setData(dataRowAdd, 0, 0);
            ucDSThuoc.setEditColumn("SOLUONG");

            ucDSThuoc.gridView.CellValueChanged += gridView1_cellChange; 

            ucDSThuoc.gridView.OptionsView.ShowFooter = true;
            ucDSThuoc.gridView.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

            ucDSThuoc.gridView.Columns["THANH_TIEN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucDSThuoc.gridView.Columns["THANH_TIEN"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "THANH_TIEN", "{0:0,0}") // "N1", en-US
            });

            ucDSThuoc.gridView.Columns["BH_TRA"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucDSThuoc.gridView.Columns["BH_TRA"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BH_TRA", "{0:0,0}")
            });

            ucDSThuoc.gridView.Columns["ND_TRA"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucDSThuoc.gridView.Columns["ND_TRA"].Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "ND_TRA", "{0:0,0}")
            });

            ucDSThuoc.gridView.Columns["delete"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            ucDSThuoc.setColumnAll(false);

            ucDSThuoc.setColumn("TEN_THUOC", 1, "Tên thuốc");
            ucDSThuoc.setColumn("HOATCHAT", 2, "Hoạt chất");
            ucDSThuoc.setColumn("DONVI_TINH", 3, "ĐVT");
            ucDSThuoc.setColumn("DUONG_DUNG", 4, "Đường dùng");
            ucDSThuoc.setColumn("DON_GIA", 5, "Đơn giá");
            ucDSThuoc.setColumn("SOLUONG", 6, "SL");
            ucDSThuoc.setColumn("THANH_TIEN", 7, "Thành tiền");
            ucDSThuoc.setColumn("LOAI_DT_CU", 8, "Loại TT cũ");
            ucDSThuoc.setColumn("LOAI_DT_MOI", 9, "Loại TT mới");
            ucDSThuoc.setColumn("DUONGDUNGE", 10, "Cách dùng", 300); 
            ucDSThuoc.setColumn("LIEUDUNG", 11, "Liều dùng", 240);

            ucDSThuoc.setColumn("up", 12, " ");
            ucDSThuoc.setColumn("down", 13, " ");
            ucDSThuoc.setColumn("delete", 14, "Xóa"); 

            ucDSThuoc.setColumnButton("DUONGDUNGE", btnCachDung_Click);
            ucDSThuoc.setColumnButtonImage("delete", "delete.png", btnDelete_Click);
            ucDSThuoc.setColumnButtonImage("up", "icon_up.png", btnUp_Click);
            ucDSThuoc.setColumnButtonImage("down", "icon_down.png", btnDown_Click);

        }
        private void gridView1_cellChange(object sender, EventArgs e)
        {
            loadAll("", "");
        }
        private void init_Control2()
        {
            if (_row.LOAITIEPNHANID.Equals("0")) _hinhthuc_kho = "12";  
            else if (_row.LOAITIEPNHANID.Equals("1"))  _hinhthuc_kho = "13";  
            else if (_row.LOAITIEPNHANID.Equals("3"))  _hinhthuc_kho = "10";  
            //Nếu là mua ngoài -> băt buộc = 9
            if (OPTION == "02D011") _hinhthuc_kho = "9";


            if (OPTION == "02D019")
            {
                _loai_tvt_kho = "0";
                _hinhthuc_kho = "9";
                _doituongbenhnhanid = "0";
            }
            //Kho thuốc - [[\"770\",\"Kho lẻ Ngoại trú BHYT (KNGT)\",\"KNGT\"],[\"742\",\"Kho Đông y (KYHCT)\",\"KYHCT\"],[\"715\",\"Tủ trực thuốc khoa khám bệnh (TT_KKB)\",\"TT_KKB\"]]
            DataTable dt = ServiceChiDinhThuoc.getKhoThuoc(_loai_tvt_kho + "$" + _hinhthuc_kho + "$" + Const.local_khoaId.ToString() + "$" + _row.DOITUONGBENHNHANID + "$0$");
            if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2", "col3" });
            DataRow drkt = dt.NewRow();
            drkt[0] = "0";
            drkt[1] = title_macdinh_cbo_KhoThuoc;
            drkt[2] = "";
            dt.Rows.InsertAt(drkt, 0);

            cbo_KhoThuoc.setEvent(ucKhoThuoc_Change);

            cbo_KhoThuoc.setData(dt, 0, 1);
            cbo_KhoThuoc.setColumnAll(false);
            cbo_KhoThuoc.setColumn(1, true);
            cbo_KhoThuoc.SelectIndex = 0;


            //Loại thuốc
            string sql_loaithuoc = "";
            if (_kechungthuocvt == "1")
            {
                sql_loaithuoc = "LOAITHUOCVATTU.01";
            }
            else
            {
                if (_loainhommaubenhpham_id.Equals("7")) sql_loaithuoc = "LOAITHUOC.01";
                else if (_loainhommaubenhpham_id.Equals("8")) sql_loaithuoc = "LOAIVATTU.01";
            }

            dt = ServiceChiDinhThuoc.getInfo(true, "LOAITHUOCVATTU_01_" + sql_loaithuoc, sql_loaithuoc);
            if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
            DataRow dr = dt.NewRow();
            dr[0] = "";
            dr[1] = "--- Tất cả ---";
            dt.Rows.InsertAt(dr, 0);
            cbo_LoaiThuoc.setData(dt, 0, 1);
            cbo_LoaiThuoc.setColumnAll(false);
            cbo_LoaiThuoc.setColumn(1, true);
            cbo_LoaiThuoc.SelectIndex = 0;
            cbo_LoaiThuoc.setEvent(LoaiThuoc_Change);

            //        // Bác sỹ kê đơn
            //ComboUtil.getComboTag("cboBACSIID","NGT02K016.EV002",[{"name":"[0]", "value":_opts.khoaId}], "", {value:'-1',text:'Chọn'},"sql");
            //// Đơn thuốc/VT trả
            //if(OPTION == '02D014' || OPTION == '02D016' || OPTION == '02D024'){
            //    var _par_phieutra=[];
            //    _par_phieutra.push({"name":"[0]","value":_loainhommaubenhpham_id});
            //    _par_phieutra.push({"name":"[1]","value":_khambenhid});
            //    _par_phieutra.push({"name":"[2]","value":_loainhommaubenhpham_id == "7" ? "1" : "0"});
            //    ComboUtil.getComboTag("cboDONTHUOCVT", "NTU02D010.PHIEUTRA" , _par_phieutra, "0", {extval: true,value:'0',text:'--Chọn--'},"sql","",false);
            //    _phieutraid = $('#cboDONTHUOCVT').val();
            //}    






        }


        DataTable dataRowAdd = new DataTable(); // bảng link với datasource của ds thuốc sẽ kê.

        DataBN _row; // Lưu thông tin của bệnh nhân
        DataTable _objTmpThuoc = null;// dùng để check trùng mỗi khi chọn thuốc định kê. Được load lúc đầu.
        DataTable _jsonThuoc24h = null; // lưu ds thuốc đã kê trong 24h trước đó, Được load lúc đầu.



        private void layThongTinBenhNhan_CauHinh()
        {
            //LẤY THÔNG TIN BỆNH NHÂN
            _row = new DataBN();
            _row = ServiceChiDinhThuoc.getThongTinBN(KHAMBENHID + "$" + Const.local_phongId);

            _doituongbenhnhanid = _row.DOITUONGBENHNHANID;
        }
        private void layThongTinBenhNhan()
        {
            #region Load thong tin phieu dieu tri của bệnh nhân
            txtMABENHNHAN.Text = _row.MABENHNHAN;
            txtTENBENHNHAN.Text = _row.TENBENHNHAN;
            txtNAMSINH.Text = _row.NAMSINH;
            txtGioiTinh.Text = _row.GIOITINH;
            txtSoThe.Text = _row.MA_BHYT;
            txtTyLe.Text = _row.TYLE_BHYT + "%";
            txtDoiTuong.Text = _row.TEN_DTBN;

            if (_row.LOAITIEPNHANID.Equals("0"))
            {
                _hinhthuc_kho = "12";
                ucSearchLookupICD.SelectedValue = _row.MACHANDOANVAOKHOA;
                ucSearchLookupBP.SelectedText = _row.CHANDOANVAOKHOA_KEMTHEO;
            }
            else if (_row.LOAITIEPNHANID.Equals("1"))
            {
                _hinhthuc_kho = "13";
                ucSearchLookupICD.SelectedValue = _row.MACHANDOANRAVIEN;
                ucSearchLookupBP.SelectedText = _row.CHANDOANRAVIEN_KEMTHEO;
                //$("#txtGHICHU_BENHCHINH").val(dataBN.GHICHU_BENHCHINH);
            }
            else if (_row.LOAITIEPNHANID.Equals("3"))
            {
                _hinhthuc_kho = "10";
                ucSearchLookupICD.SelectedValue = _row.MACHANDOANVAOKHOA;
                ucSearchLookupBP.SelectedText = _row.CHANDOANVAOKHOA_KEMTHEO;
            }
            //Nếu là mua ngoài -> băt buộc = 9
            if (OPTION == "02D011") _hinhthuc_kho = "9";

            #endregion


            #region Cấu hình theo thông tin BN

            if (_row.KENHIEUNGAY != "1")
                layoutControlItem_txtSONGAY_KE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                layoutControlItem_txtSONGAY_KE.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            if (_row.BENHNHANID != "") btnThuocConSD.Visible = true;
            else btnThuocConSD.Visible = false;

            // tham số cho phép kê thuốc lẻ hay ko: if (_row.CAPTHUOCLE == "1") --> cho phép nhập dấu phẩy trong các sự kiện Keypress của ô: số ngày, sáng trưa chiều tối, số lượng.

            if (_row.CHECKDIUNGTHUOC != "1") btnDiUngThuoc.Visible = false;
            if (_row.ANTIMCACHDUNGDT != "1")
            {
                //$('#dvSearchCD').remove();
                //$('#dvGhiChu').removeClass('col-xs-2');
                //$('#dvGhiChu').addClass('col-xs-3');
            }
            if (_row.SUDUNGPHACDO != "1") btnPhacDo.Visible = false;
            if (_row.LOAITIEPNHANID != "1") btnXuTri.Visible = false;
            if (_row.KTCODONTHUOC == "1" && _row.KOKTKHICODONTHUOC != "1") btnXuTri.Visible = false;

            // chỉ check với cấp thuốc - vật tư - đông y
            if (OPTION == "02D010" || OPTION == "02D015" || OPTION == "02D017")
            {
                if (_row.DOITUONGBENHNHANID == "1" && (Func.Parse(_row.HANTHE) <= Func.Parse(_row.NGAYTHE)))
                    ThongBao = "Thẻ của bệnh nhân sắp hết hạn, còn " + _row.HANTHE + " ngày sử dụng";
            }

            if (_row.CACHDUNG == "1")
            {
                layoutControlItem_TG_HENKHAM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem_ckbCapPhieuHenKham.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            if (_row.AN_CBO_LOAITHUOC == "1") cbo_LoaiThuoc.Enabled = false;
            else cbo_LoaiThuoc.Enabled = true;

            //if(_row.BACSI_KE == "1" && LOAIKEDON == "1")
            //$('#divbske').css('display','');


            if (_row.LOAITIEPNHANID == "0")
            {
                //$('#dvSearchName').removeClass("col-xs-2");
                //$('#dvSearchName').addClass("col-xs-3");

                //$('#dvDS_THUOC').removeClass("col-xs-2");
                //$('#dvDS_THUOC').addClass("col-xs-3");

                //$('#dvGhiChu').removeClass("col-xs-2");
                //$('#dvGhiChu').addClass("col-xs-3");

                //$('#dvlLIEU_DUNG').removeClass("col-xs-2");
                //$('#dvlLIEU_DUNG').addClass("col-xs-3");

                //$('#dvLIEU_DUNG').removeClass("col-xs-2");
                //$('#dvLIEU_DUNG').addClass("col-xs-3");
            }
            else if (_row.LOAITIEPNHANID == "3")
            {
                //$('#dvlGhiChu').removeClass("col-xs-3");
                //$('#dvlGhiChu').addClass("col-xs-2");
            }

            if (_row.SUDUNG_LIEUDUNG == "1" && (this.OPTION == "02D010" || this.OPTION == "02D017"))
            {
                layoutControlItem_SL_Lan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem_Lan_Ngay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem_LieuDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }

            if (_kieutra == "1" && (this.OPTION == "02D014" || this.OPTION == "02D016") && this.MAUBENHPHAMID != "")
            {
                //i_action = "Add";
                dtimeNgayChiDinh.DateTime = Func.getSysDatetime();
                //$("#cboDONTHUOCVT').val(_maubenhpham_id);					
                //$('#cboDONTHUOCVT').change();
            }
            #endregion

            // lấy thông tin thanh toán
            // [{\"TONGTIENDV\": \"39000\",\"TAMUNG\": \"0\",\"DANOP\": \"0\"}]
            DataTable vp_ar = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.VIENPHI", _row.TIEPNHANID, 0);
            if (vp_ar != null && vp_ar.Rows.Count > 0)
            {
                _tongtien = vp_ar.Rows[0]["TONGTIENDV"].ToString();
                _tientu = vp_ar.Rows[0]["TAMUNG"].ToString();
                _danop = vp_ar.Rows[0]["DANOP"].ToString();
            }
            

            #region Hiển thị Các thông tin trong th Update
            if (!string.IsNullOrEmpty(this.MAUBENHPHAMID))
            {
                i_action = "Upd";
                cbo_KhoThuoc.lookUpEdit.ReadOnly = true;
                DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NTU02D010.11", MAUBENHPHAMID, 0);
                if (dt.Rows.Count > 0)
                {
                    dtimeNgayChiDinh.Text = dt.Rows[0]["NGAYMAUBENHPHAM"].ToString();
                    dtimeNgayDung.Text = dt.Rows[0]["NGAYMAUBENHPHAM_SUDUNG"].ToString();
                    //$("#txtMACHANDOANICD").val(dt.Rows[0]["MACHANDOAN"].ToString();
                    //$("#txtTENCHANDOANICD").val(dt.Rows[0]["CHANDOAN"].ToString();
                    if (Func.Parse(dt.Rows[0]["PHIEUDIEUTRIID"].ToString()) > 0)
                        cbo_PhieuDieuTri.SelectValue = dt.Rows[0]["PHIEUDIEUTRIID"].ToString();
                    cbo_KhoThuoc.SelectValue = dt.Rows[0]["KHOTHUOCID"].ToString();

                    //$('#txtDS_THUOC').trigger("focus");
                    //                    $('#txtSONGAY_KE').val('1');

                    ucSearchLookup_LoiDan.SelectedText = dt.Rows[0]["LOIDANBACSI"].ToString();
                    txtTG_HENKHAM.Text = dt.Rows[0]["SONGAYHEN"].ToString();
                    if (dt.Rows[0]["PHIEUHEN"].ToString() == "1")
                    {
                        ckbCapPhieuHenKham.CheckedChanged -= ckbCapPhieuHenKham_CheckedChanged;
                        ckbCapPhieuHenKham.Checked = true;
                        ckbCapPhieuHenKham.CheckedChanged += ckbCapPhieuHenKham_CheckedChanged;
                    }
                    //$('#txtSLTHANG').val(dt.Rows[0]["SLTHANG"].ToString();
                    //$("#cboDONTHUOCVT").val(dt.Rows[0]["MAUBENHPHAMCHA_ID"].ToString(); 
                }
                else
                    ucSearchLookupICD.Focus();

                //// kiểu trả = 1 là trả theo tiện ích
                //if (_kieutra != "1")
                //{
                //    loadGridDonThuoc('DONTHUOC', _maubenhpham_id);
                //}
                //var jsonGridData = jQuery("#" + _gridDonThuoc).jqGrid('getRowData');
                //for (var i = 0; i < jsonGridData.length; i++)
                //{
                //    //doAddDrugToJson(ui, _objDrug, 1);
                //    var objRowData = jQuery("#" + _gridDonThuoc).getRowData(jsonGridData[i]);
                //    doAddDrugToJson(objRowData, _objDrug, 1);
                //}			

                //if (OPTION == '02D014' || OPTION == '02D016')
                //{
                //            $("#cboDONTHUOCVT").change();
                //}



                // load đơn thuốc cũ
                Load_DonCu(this.MAUBENHPHAMID, "");
                tinhTruocTien();
            }
            else
            {
                i_action = "Add";
                //_first_load = 1;
                ucSearchLookupICD.Focus();
            }
            #endregion




            //doLoadPrescription();


        }
        private void tinhTruocTien()
        {
            double _tong_tien_dv_tt = 0;
            DataTable _data_ar = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.05", _row.TIEPNHANID, 0);
            if (_data_ar.Rows.Count > 0)
            {
                _TRAN_BHYT = Func.Parse_double(_data_ar.Rows[0]["TRAN_BHYT"].ToString());
                _TYLE_BHYT_TT = Func.Parse_double(_data_ar.Rows[0]["TYLE_BHYT"].ToString());
                _TONGTIENDV_BH = Func.Parse_double(_data_ar.Rows[0]["TONGTIENDV_BH"].ToString());
                _tong_tien_dv_tt = _TONGTIENDV_BH;
            }

            for (var i = 0; i < dataRowAdd.Rows.Count; i++)
            {
                //_so_luong = dataRowAdd.Rows[i]["SO_LUONG"].ToString();
                double _don_gia = Func.Parse_double(dataRowAdd.Rows[i]["DON_GIA"].ToString().Replace(",", ""));
                // _obj_new = dataRowAdd.Rows[i]["ID_DT_MOI"].ToString();
                #region tạo tham số
                DataTable objTinhTien = new DataTable();
                objTinhTien.Columns.Add("DOITUONGBENHNHANID", typeof(String));
                objTinhTien.Columns.Add("MUCHUONG", typeof(String));
                objTinhTien.Columns.Add("GIATRANBH", typeof(String));
                objTinhTien.Columns.Add("GIABHYT", typeof(String));
                objTinhTien.Columns.Add("GIAND", typeof(String));
                objTinhTien.Columns.Add("GIADV", typeof(String));
                objTinhTien.Columns.Add("GIANN", typeof(String));
                objTinhTien.Columns.Add("DOITUONGCHUYEN", typeof(String));
                objTinhTien.Columns.Add("GIADVKTC", typeof(String));
                objTinhTien.Columns.Add("MANHOMBHYT", typeof(String));
                objTinhTien.Columns.Add("SOLUONG", typeof(String));
                objTinhTien.Columns.Add("CANTRENDVKTC", typeof(String));
                objTinhTien.Columns.Add("THEDUTHOIGIAN", typeof(String));
                objTinhTien.Columns.Add("DUOCVANCHUYEN", typeof(String));
                objTinhTien.Columns.Add("TYLETHUOCVATTU", typeof(String));
                objTinhTien.Columns.Add("NHOMDOITUONG", typeof(String));
                objTinhTien.Columns.Add("NGAYHANTHE", typeof(String));
                objTinhTien.Columns.Add("NGAYDICHVU", typeof(String));
                objTinhTien.Columns.Add("TYLE_MIENGIAM", typeof(String));
                DataRow dr_objTinhTien = objTinhTien.NewRow();
                dr_objTinhTien["DOITUONGBENHNHANID"] = _doituongbenhnhanid;
                dr_objTinhTien["MUCHUONG"] = _TYLE_BHYT_TT;
                dr_objTinhTien["GIATRANBH"] = Func.Parse_double(dataRowAdd.Rows[i]["GIATRANBHYT"].ToString());
                dr_objTinhTien["GIABHYT"] = _don_gia;
                dr_objTinhTien["GIAND"] = _don_gia;
                dr_objTinhTien["GIADV"] = _don_gia;
                dr_objTinhTien["GIANN"] = _don_gia;
                dr_objTinhTien["DOITUONGCHUYEN"] = dataRowAdd.Rows[i]["ID_DT_MOI"].ToString();
                dr_objTinhTien["GIADVKTC"] = 0;
                dr_objTinhTien["MANHOMBHYT"] = _nhom_mabhyt_id;
                dr_objTinhTien["SOLUONG"] = dataRowAdd.Rows[i]["SOLUONG"].ToString();
                dr_objTinhTien["CANTRENDVKTC"] = 0;
                dr_objTinhTien["THEDUTHOIGIAN"] = _row.TRADU6THANGLUONGCOBAN;
                dr_objTinhTien["DUOCVANCHUYEN"] = _row.DUOC_VAN_CHUYEN;
                dr_objTinhTien["TYLETHUOCVATTU"] = _tyle_bhyt_tt_tvt;
                dr_objTinhTien["NGAYHANTHE"] = _row.BHYT_KT;
                dr_objTinhTien["NGAYDICHVU"] = dtimeNgayChiDinh.DateTime.ToString(Const.FORMAT_date1);
                dr_objTinhTien["TYLE_MIENGIAM"] = _row.TYLE_MIENGIAM;
                dr_objTinhTien["NHOMDOITUONG"] = "";
                objTinhTien.Rows.Add(dr_objTinhTien);
                #endregion
                DataTable r = Func.vienphi_tinhtien_dichvu(objTinhTien);
                double row_Price = Func.Parse_double(r.Rows[0]["tong_cp"].ToString());
                double row_Insr = Func.Parse_double(r.Rows[0]["bh_tra"].ToString());
                double row_End = Func.Parse_double(r.Rows[0]["nd_tra"].ToString());
                _tong_tien_dv_tt = _tong_tien_dv_tt + row_Price;

                string _ngt_check_tu = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "NGT_CHECK_TU");

                // check BN khác BHYT, BN là NGT và BN nằm ngoài HĐ
                if (_ngt_check_tu == "1" && _doituongbenhnhanid != "1" && _row.LOAITIEPNHANID == "1"
                    && "02D010 02D015 02D017".Contains(this.OPTION) && Func.Parse(_row.HOPDONGID) == 0)
                {

                    double checktien = Func.Parse_double(_tongtien) + _tong_tien_dv_tt - Func.Parse_double(_danop);
                    if (Func.Parse_double(_tientu) < checktien)
                    {
                        if (ThongBao_check_tien == "-1") ThongBao_check_tien = "Tổng tiền thanh toán > tiền tạm ứng";
                        else MessageBox.Show("Tổng tiền thanh toán > tiền tạm ứng");

                        if (_row.KIEUCANHBAOTIENTAMUNG == "1") btnLuu.Enabled = false;
                        else btnLuu.Enabled = true;
                    }
                }

                if (_TRAN_BHYT < _tong_tien_dv_tt) _GLB_CACH_TINH_TIEN = "0";
                else _GLB_CACH_TINH_TIEN = "1";
            }
        }

        private void init_ButtonLocation()
        {
            // Sắp xếp lại vị trí các button
            int x = 2;
            int y = btnThemThuoc.Location.Y;
            if (btnThemThuoc.Visible)
            {
                btnThemThuoc.Location = new Point(x, y);
                x += btnThemThuoc.Size.Width + 6;
            }
            if (btnLuu.Visible)
            {
                btnLuu.Location = new Point(x, y);
                x += btnLuu.Size.Width + 6;
            }
            if (btnXuTri.Visible)
            {
                btnXuTri.Location = new Point(x, y);
                x += btnXuTri.Size.Width + 6;
            }
            if (btnDonThuocMau.Visible)
            {
                btnDonThuocMau.Location = new Point(x, y);
                x += btnDonThuocMau.Size.Width + 6;
            }
            if (btnDonThuocCu.Visible)
            {
                btnDonThuocCu.Location = new Point(x, y);
                x += btnDonThuocCu.Size.Width + 6;
            }
            if (btnThuocConSD.Visible)
            {
                btnThuocConSD.Location = new Point(x, y);
                x += btnThuocConSD.Size.Width + 6;
            }
            if (btnDiUngThuoc.Visible)
            {
                btnDiUngThuoc.Location = new Point(x, y);
                x += btnDiUngThuoc.Size.Width + 6;
            }
            if (btnPhacDo.Visible)
            {
                btnPhacDo.Location = new Point(x, y);
                x += btnPhacDo.Size.Width + 6;
            }
            if (btnDong.Visible)
            {
                btnDong.Location = new Point(x, y);
                x += btnDong.Size.Width + 6;
            }
            layoutControlItem_footer_button.ControlMaxSize = new Size(x, layoutControlItem_footer_button.ControlMaxSize.Height);
        }
        private void initThemThuoc()
        {
            lookup_Thuoc.searchLookUpEdit1.EditValue = "";
            cbo_DuongDung.lookUpEdit.EditValue = "";
            txtSO_NGAY.Text = "";
            txtSANG.Text = "";
            txtTRUA.Text = "";
            txtCHIEU.Text = "";
            txtTOI.Text = "";
            txtSOLUONG_CHITIET.Text = "";
            txtGHICHU.EditValue = "";
            lookup_ThoiGian.SelectIndex = -1;
            lookup_CachDung.Text = "";
            txtSoLuongTrenLan.Text = "";
            txtSoLanTrenNgay.Text = "";
            txtLieuDung.Text = "";
        }

        #endregion

        #region Hàm xử lý dữ liệu;

        // Lấy Danh sách tìm kiếm Thuốc tùy theo combo Kho thuốc
        private void get_DSTenThuoc()
        {
            string _sql = "";
            String[] listValue;

            string company_id = Const.local_user.COMPANY_ID.ToString();
            string khothuocID = cbo_KhoThuoc.SelectValue;
            string loaithuocid = cbo_LoaiThuoc.SelectValue;
            if (loaithuocid == "") loaithuocid = "-1";
            bool getfromCache = true;

            if (OPTION == "02D011")//Mua ngoai
            {
                _sql = Const.tbl_ThuocMuaNgoai;
                listValue = new String[] { company_id };
                //_col = "THUOCVATTUID,THUOCVATTUID,0,0,t,c;Tên" + _lbl_text + ",TEN_THUOC,25,0,f,l;Hoạt chất,HOATCHAT,15,0,f,l;Đơn vị,TEN_DVT,10,0,f,l;Mã" + _lbl_text 
                //+ ",MA_THUOC,15,0,f,l;SL khả dụng,SLKHADUNG,10,0,f,r;Giá DV,GIA_BAN,12,0,f,r;Biệt dược,BIETDUOC,13,0,f,c;DUONGDUNGID,DUONGDUNGID,0,0,t,c;DUONG_DUNG
                //,DUONG_DUNG,0,0,t,c;HUONGDAN_SD,HUONGDAN_SD,0,0,t,c;TENKHO,TENKHO,0,0,t,c;KHOID,KHOID,0,0,t,c;TUONGTACTHUOC,TUONGTACTHUOC,0,0,t,c;NHOM_MABHYT_ID
                //,NHOM_MABHYT_ID,0,0,t,c;GIATRANBHYT,GIATRANBHYT,0,0,t,c;KHOANMUCID,KHOANMUCID,0,0,t,c;TYLEBHYT_TVT,TYLEBHYT_TVT,0,0,t,c;DICHVUKHAMBENHID,DICHVUKHAMBENHID,0,0,t,c";
            }
            else if (OPTION == "02D014" || OPTION == "02D016")//Phieu tra thuoc
            {
                string _phieutraid = ""; // Đơn thuốc/VT trả
                _sql = Const.tbl_ThuocPhieuTraThuocVT;
                getfromCache = false; // ko lưu cache vì dl này kèm theo KHAMBENHID
                listValue = new String[] { KHAMBENHID, company_id, _loainhommaubenhpham_id, _phieutraid };
                //_col = "THUOCVATTUID,THUOCVATTUID,0,0,t,c;Tên" + _lbl_text + ",TEN_THUOC,25,0,f,l;Hoạt chất,HOATCHAT,15,0,f,l;Đơn vị,TEN_DVT,10,0,f,l;Mã" + _lbl_text + 
                //",MA_THUOC,15,0,f,l;SL khả dụng,SLKHADUNG,10,0,f,l;Giá DV,GIA_BAN,12,0,f,r;Biệt dược,BIETDUOC,13,0,f,c;DUONGDUNGID,DUONGDUNGID,0,0,t,c;DUONG_DUNG
                //,DUONG_DUNG,0,0,t,c;HUONGDAN_SD,HUONGDAN_SD,0,0,t,c;TENKHO,TENKHO,0,0,t,c;KHOID,KHOID,0,0,t,c;TUONGTACTHUOC,TUONGTACTHUOC,0,0,t,c;NHOM_MABHYT_ID
                //,NHOM_MABHYT_ID,0,0,t,c;GIATRANBHYT,GIATRANBHYT,0,0,t,c;KHOANMUCID,KHOANMUCID,0,0,t,c;TYLEBHYT_TVT,TYLEBHYT_TVT,0,0,t,c;DICHVUKHAMBENHID
                //,DICHVUKHAMBENHID,0,0,t,c;KHO_THUOCID,KHO_THUOCID,0,0,t,l";
            }
            else if (OPTION == "02D017" || OPTION == "02D018")//Phieu tra thuoc YHCT
            {
                _sql = Const.tbl_Thuoc;
                listValue = new String[] { khothuocID, company_id, _loainhommaubenhpham_id, loaithuocid };
                // _col = "THUOCVATTUID,THUOCVATTUID,0,0,t,c;Tên Tên vị thuốc YHCT,TEN_THUOC,25,0,f,l;Đơn vị,TEN_DVT,10,0,f,l;Mã" + _lbl_text + ",MA_THUOC,15,0,f,l;
                //SL khả dụng,SLKHADUNG,10,0,f,l;Giá DV,GIA_BAN,12,0,f,r;HUONGDAN_SD,HUONGDAN_SD,0,0,t,c;TENKHO,TENKHO,0,0,t,c;KHOID,KHOID,0,0,t,c;TUONGTACTHUOC
                //,TUONGTACTHUOC,0,0,t,c;NHOM_MABHYT_ID,NHOM_MABHYT_ID,0,0,t,c;GIATRANBHYT,GIATRANBHYT,0,0,t,c;KHOANMUCID,KHOANMUCID,0,0,t,c;TYLEBHYT_TVT,TYLEBHYT_TVT
                //,0,0,t,c;DICHVUKHAMBENHID,DICHVUKHAMBENHID,0,0,t,c;KHO_THUOCID,KHO_THUOCID,0,0,t,l";                 
            }
            else
            {
                _sql = Const.tbl_Thuoc;
                //Ko cho ke thuoc da co trong danh sach 
                if (_kechungthuocvt.Equals("1")) _loainhommaubenhpham_id = "-1";
                listValue = new String[] { khothuocID, company_id, _loainhommaubenhpham_id, loaithuocid };
                //_col = "THUOCVATTUID,THUOCVATTUID,0,0,t,c;Tên" + _lbl_text + ",TEN_THUOC,25,0,f,l;Hoạt chất,HOATCHAT,15,0,f,l;Liều lượng,LIEULUONG,15,0,f,l;Đơn vị
                //,TEN_DVT,10,0,f,l;Mã" + _lbl_text + ",MA_THUOC,15,0,t,c;SL khả dụng,SLKHADUNG,10,0,f,l;Giá DV,GIA_BAN,12,0,f,l;Biệt dược,BIETDUOC,13,0,f,c;DUONGDUNGID
                //,DUONGDUNGID,0,0,t,c;DUONG_DUNG,DUONG_DUNG,0,0,t,c;HUONGDAN_SD,HUONGDAN_SD,0,0,t,c;TENKHO,TENKHO,0,0,t,c;KHOID,KHOID,0,0,t,c;TUONGTACTHUOC,TUONGTACTHUOC
                //,0,0,t,c;NHOM_MABHYT_ID,NHOM_MABHYT_ID,0,0,t,c;GIATRANBHYT,GIATRANBHYT,0,0,t,c;KHOANMUCID,KHOANMUCID,0,0,t,c;TYLEBHYT_TVT,TYLEBHYT_TVT,0,0,t,c;
                //DICHVUKHAMBENHID,DICHVUKHAMBENHID,0,0,t,c;MAHOATCHAT,MAHOATCHAT,0,0,t,c;OldValue,OLDVALUE,0,0,t,l;KETRUNGHOATCHAT,KETRUNGHOATCHAT,0,0,t,l;KHO_THUOCID
                //,KHO_THUOCID,0,0,t,l"; //OldValue,OLDVALUE,0,0,f,t,0
            }

            getfromCache = false; // bỏ việc lưu cache vì dl thuốc thay đổi liên tục mỗi lần 1 đơn thuốc đc kê
            DataTable dtthuoc = RequestHTTP.Cache_ajaxExecuteQueryPaging(getfromCache, _sql, listValue);// ServiceChiDinhThuoc.getDSThuoc(false, "", _sql
            if (dtthuoc == null || dtthuoc.Rows.Count == 0) dtthuoc = Func.getTableEmpty(
                new string[] { "RN", "THUOCVATTUID","MAHOATCHAT","MA_THUOC","DUONGDUNGID","DUONG_DUNG","HUONGDAN_SD","TENKHO","KHOAID"
                ,"GIATRANBHYT","KHOANMUCID","TYLEBHYT_TVT","CHOLANHDAODUYET","KETRUNGHOATCHAT","CANHBAOSOLUONG","NHOM_MABHYT_ID","CHUY"
                ,"HOATCHAT","TEN_THUOC","LIEULUONG","TEN_DVT","SLKHADUNG","GIA_BAN","BIETDUOC"
            });

            //load thuoc 
            lookup_Thuoc.setData(dtthuoc, "THUOCVATTUID", "TEN_THUOC");
            lookup_Thuoc.setAllColumn(false);

            lookup_Thuoc.setColumn("HOATCHAT", 1, "Hoạt chất", 0);
            lookup_Thuoc.setColumn("TEN_THUOC", 0, "Tên thuốc", 0);
            lookup_Thuoc.setColumn("LIEULUONG", 2, "Liều lượng", 0);
            lookup_Thuoc.setColumn("TEN_DVT", 3, "Đơn vị", 0);
            lookup_Thuoc.setColumn("SLKHADUNG", 4, "SL khả dụng", 0);
            lookup_Thuoc.setColumn("GIA_BAN", 5, "Giá DV", 0);

            lookup_Thuoc.setColumn("BIETDUOC", 6, "Biệt dược", 0);
            lookup_Thuoc.searchLookUpEdit.Properties.PopupFormSize = new Size(850, 350);

        }
        //Kiểm tra THUOCVATTUID có trong ds thuốc đã kê trước đó và đang kê ko?
        private bool CheckThuoc_DangKe_DaKe(string MAHOATCHAT)
        {
            //Lấy ds thuốc kê đã từng kê trước đó
            //LẤY DS THUỐC ĐÃ DÙNG TRONG ĐỢT ĐIỀU TRỊ: dùng để check trùng mỗi khi chọn thuốc định kê, hoặc khi load đơn thuốc mẫu, đơn cũ.
            // {"func":"ajaxCALL_SP_O","params":["GET_MAHOATCHAT","95429",0],"uuid":"e8134777-9950-4540-a97d-1f310d4bb53b"}
            // [{\"THUOCVATTUID\": \"416040\",\"MAHOATCHAT\": \"40.491\",\"KETRUNGHOATCHAT\": \"0\"},{\"THUOCVATTUID\": \"416040\",\"MAHOATCHAT\": \"40.491\",\"KETRUNGHOATCHAT\": \"0\"}] 
            if (_objTmpThuoc == null)
                _objTmpThuoc = RequestHTTP.call_ajaxCALL_SP_O("GET_MAHOATCHAT", _row.TIEPNHANID, 0);  

            for (int i = 0; i < _objTmpThuoc.Rows.Count; i++) 
                if (_objTmpThuoc.Rows[i]["KETRUNGHOATCHAT"].ToString() != "1"
                    && _objTmpThuoc.Rows[i]["MAHOATCHAT"].ToString() != "undefined"
                    && _objTmpThuoc.Rows[i]["MAHOATCHAT"].ToString() == MAHOATCHAT)
                    return true; 

            return false;
        }
        //Kiểm tra THUOCVATTUID có trong ds thuốc _jsonThuoc24h ko?
        private bool CheckThuoc_24h(string THUOCVATTUID)
        {
            //Lấy ds thuốc kê trong 24h
            if (_jsonThuoc24h == null) _jsonThuoc24h = RequestHTTP.call_ajaxCALL_SP_O("NTU02D010.19", this.KHAMBENHID, 0);

            for (int i = 0; i < _jsonThuoc24h.Rows.Count; i++)
                if (_jsonThuoc24h.Rows[i]["THUOCVATTUID"].ToString() == THUOCVATTUID)
                    return true;

            return false;
        }
        //Kiểm tra THUOCVATTUID có trong ds thuốc đang kê ko?
        private bool CheckThuoc_DangKe(string THUOCVATTUID)
        {
            for (int i = 0; i < dataRowAdd.Rows.Count; i++)
                if (dataRowAdd.Rows[i]["THUOCVATTUID"].ToString() == THUOCVATTUID)
                    return true;

            return false;
        }

        string hidLOAITVTID = "";
        //Mỗi khi chọn 1 thuốc định kê, sẽ check trùng với thuốc đã kê trước đó trong bảng _objTmpThuoc
        private bool CheckTrungThuoc(DataRowView drv)
        {
            // drv: dòng thuốc chọn định kê
            // {"RN": "1","THUOCVATTUID": "281802","TEN_THUOC": "Mezafen","HOATCHAT": "Loxoprofen","MAHOATCHAT": "40.40","TEN_DVT": "Viên","SLKHADUNG": "3185"
            // ,"MA_THUOC": "MEZ02","GIA_BAN": "882","BIETDUOC": " ","DUONGDUNGID": "513","DUONG_DUNG": "Uống","HUONGDAN_SD": "","TENKHO": "Kho lẻ Ngoại trú BHYT"
            // ,"KHOAID": "4929","NHOM_MABHYT_ID": "4","GIATRANBHYT": "882","KHOANMUCID": "373","TYLEBHYT_TVT": "100","CHOLANHDAODUYET": null,"LIEULUONG": "60mg"
            // ,"CHUY": "","KETRUNGHOATCHAT": "0","CANHBAOSOLUONG": "0","MABYT": "40.40"},

            //if ($.inArray(_option, ['02D011', '02D019']) >= 0){
            //    _ui.GIATRANBHYT = "0";
            //    _ui.GIA_BAN = "0";
            //}
            if (_row.CHECKTRUNGHOATCHAT == "1")
            {
                // check ngoai tru
                if (_row.LOAITIEPNHANID == "1")
                {

                    if (OPTION != "02D015"
                        && (drv.DataView.Table.Columns.Contains("KETRUNGHOATCHAT") && drv["KETRUNGHOATCHAT"].ToString() != "1")
                        && drv["MAHOATCHAT"].ToString() != "undefined")
                    {
                        if (CheckThuoc_DangKe_DaKe(drv["MAHOATCHAT"].ToString()) == true)
                        {
                            MessageBox.Show("Thuốc [" + drv["TEN_THUOC"].ToString() + "] có hoạt chất trùng với thuốc đã kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày");
                            if (_row.CHANHOATCHAT == "1")
                            {
                                btnLuu.Enabled = false;
                                lookup_Thuoc.SelectIndex = -1;
                                return false;
                            }
                        }
                        else btnLuu.Enabled = true;
                    }
                }
                // check hoat chat va duong dung
                else if (_row.KIEUCHECK_HOATCHAT == "1")
                {
                    for (int i = 0; i < dataRowAdd.Rows.Count; i++)
                    {
                        if (OPTION != "02D015"
                            && drv["MAHOATCHAT"].ToString() != "undefined"
                            && dataRowAdd.Rows[i]["MAHOATCHAT"].ToString() != "undefined"
                            && dataRowAdd.Rows[i]["MAHOATCHAT"].ToString() == drv["MAHOATCHAT"].ToString()
                            && dataRowAdd.Rows[i]["DUONGDUNGID"].ToString() == drv["DUONGDUNGID"].ToString())
                        {
                            MessageBox.Show("Thuốc [" + drv["TEN_THUOC"].ToString() + "] có hoạt chất và đường dùng trùng với thuốc đã kê trong đơn");
                            if (_row.CHANHOATCHAT == "1")
                            {
                                btnLuu.Enabled = false;
                                lookup_Thuoc.SelectIndex = -1;
                                return false;
                            }
                            else
                            {
                                btnLuu.Enabled = true;
                            }
                        }
                    }
                }
            }

            // Chỉ dùng cho Nội trú, check mã thuốc là Ô xy thì cho hiển thị combo Đơn vị quy đổi.
            //if (drv["MABYT"].ToString() == "40.17")
            //        {
            //            var _par_quydoi =[];
            //            _par_quydoi.push({
            //                "name":"[0]","value":drv["THUOCVATTUID});
            //            ComboUtil.getComboTag("cboDVQUYDOI", "DMC_QUYDOIOXY", _par_quydoi, "", { extval: true,value: "0",text: ""},"sql");					
            //$("#cboDVQUYDOI").prop("disabled", false);
            //$("#cboDVQUYDOI").addClass("notnullandlonhon0");
            //            }
            //            else
            //            {
            //$("#cboDVQUYDOI").prop("disabled", true);
            //                }

            if (
                (OPTION == "02D010" || OPTION == "02D011")
                && CheckThuoc_24h(drv["THUOCVATTUID"].ToString()) == true
                )
            {//Neu thuoc dc ke -> phai confirm
                string _msgInfo = "Thuốc " + drv["TEN_THUOC"].ToString() + " đã được kê khoảng 24h trước, bạn có muốn kê đơn với thuốc này nữa không?";
                if (MessageBox.Show(_msgInfo, "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _nhom_mabhyt_id = drv["NHOM_MABHYT_ID"].ToString();
                    _tyle_bhyt_tt_tvt = drv["TYLEBHYT_TVT"].ToString();

                    // mấy cái này làm gì ???
                    hidLOAITVTID = drv["LOAI"].ToString();
                    //$("#hidLOAITVTID").val(drv["LOAI);

                    //$("#hidMABYT").val(drv["MABYT); 
                    //            if (OPTION != "02D011")//Don mua ngoai
                    //	$("#hdSOLUONGKHADUNG").val(drv["SLKHADUNG);
                    //else
                    //	$("#hdSOLUONGKHADUNG").val(100000);
                    //$("#txtGHICHU").val("");
                    //$("#hdHUONGDANTHUCHIEN").val("");
                    //$("#hidCANHBAOSOLUONG").val(drv["CANHBAOSOLUONG);

                    if (LOAIKEDON == "1") txtSOLUONG_CHITIET.Focus();
                    else txtSO_NGAY.Focus();
                }
                else
                {
                    lookup_Thuoc.SelectIndex = -1;
                    return false;
                }
            }
            else
            {//Neu thuoc chua dc ke -> Cu the ma ke
             // check thuốc tồn tại trong đơn
                if (CheckThuoc_DangKe(drv["THUOCVATTUID"].ToString()) == true)
                {
                    MessageBox.Show("Thuốc đã có trong đơn!");
                    lookup_Thuoc.SelectIndex = -1;
                    return false;
                }


                if (_row.CANHBAOPHACDO == "1" && ucSearchLookupICD.SelectedValue != "")
                {
                    string resultDvPd = RequestHTTP.call_ajaxCALL_SP_S_result("NTU02D009.EV013", drv["THUOCVATTUID"].ToString() + "$" + ucSearchLookupICD.SelectedValue + "$" + "2");
                    if (!string.IsNullOrEmpty(resultDvPd) && resultDvPd != "undefined")
                        MessageBox.Show("Thuốc " + resultDvPd + " có hoạt chất không tồn tại trong phác đồ điều trị mã bệnh " + ucSearchLookupICD.SelectedValue);
                }

                object objData = new
                {
                    THUOCVATTUID = drv["THUOCVATTUID"].ToString(),
                    BENHNHANID = BENHNHANID,
                    MAHOATCHAT = drv["MAHOATCHAT"].ToString()
                };
                bool _diung = true;
                string fl = RequestHTTP.call_ajaxCALL_SP_I("CHECK.DIUNGTHUOC", Newtonsoft.Json.JsonConvert.SerializeObject(objData).Replace("\"", "\\\""));
                if (Func.Parse(fl) > 0 && _row.CHECKDIUNGTHUOC == "1")
                {
                    MessageBox.Show("Thuốc [" + drv["TEN_THUOC"].ToString() + "] nằm trong danh sách thuốc dị ứng của bệnh nhân");
                    lookup_Thuoc.SelectIndex = -1;
                    lookup_Thuoc.Focus();
                    _diung = false;
                }

                if (drv["CHOLANHDAODUYET"].ToString() == "1") MessageBox.Show(drv["CHUY"].ToString());

                _nhom_mabhyt_id = drv["NHOM_MABHYT_ID"].ToString();
                _tyle_bhyt_tt_tvt = drv["TYLEBHYT_TVT"].ToString();

                // mấy cái này làm gì ??? 
                hidLOAITVTID = drv["LOAI"].ToString();
                //$("#hidLOAITVTID").val(drv["LOAI);

                //$("#hidMABYT").val(drv["MABYT);
                //$("#txtGHICHU").val(drv["HUONGDAN_SD);
                //$("#cboDUONG_DUNG").val(drv["DUONGDUNGID);
                //$("#hdHUONGDANTHUCHIEN").val("");
                //$("#hidCANHBAOSOLUONG").val(drv["CANHBAOSOLUONG);
                //      if (_loai_don == "2" || _loai_don == "4")
                //	$("#cboMA_KHO").val(drv["KHOID);
                //      if (OPTION != "02D011")//Don mua ngoai
                //	$("#hdSOLUONGKHADUNG").val(drv["SLKHADUNG);
                //else
                //	$("#hdSOLUONGKHADUNG").val(100000);
                //$("#hdDONVI").val(drv["TEN_DVT);

                if (LOAIKEDON == "1") txtSOLUONG_CHITIET.Focus();
                else txtSO_NGAY.Focus();

                if (_diung == false) return false;
            }

            return true;
        }
        //Tính toán số lần uống, cách uống, liều lượng,....
        private double doInputDrugType(string kieu)
        {
            double _total = 0;
            double _tong_lan = 0;
            double _trung_binh = 0;
            string _text_duongdung = cbo_DuongDung.Text;
            double _songay = Func.Parse_double(txtSO_NGAY.Text.ToString());
            double _sang = Func.Parse_double(txtSANG.Text.ToString());

            if (_sang > 0) _tong_lan++;
            double _trua = Func.Parse_double(txtTRUA.Text.ToString());
            if (_trua > 0) _tong_lan++;
            double _chieu = Func.Parse_double(txtCHIEU.Text.ToString());
            if (_chieu > 0) _tong_lan++;
            double _toi = Func.Parse_double(txtTOI.Text.ToString());
            if (_toi > 0) _tong_lan++;

            _trung_binh = (_sang + _trua + _chieu + _toi) / _tong_lan;
             
            if (_songay > 0) 
            {
                //_total = Math.Round(((_sang + _trua + _chieu + _toi) * _songay), 0, MidpointRounding.AwayFromZero);
                _total = (_sang + _trua + _chieu + _toi) * _songay;
                if (this.LOAIKEDON == "0")
                    if (kieu != "1") // set lai so luong
                    {
                        if (_row.NGT_LAMTRON_KETHUOC == "1") txtSOLUONG_CHITIET.EditValue = Math.Round(_total); 
                        else txtSOLUONG_CHITIET.EditValue = _total;
                    } 

                hdHUONGDANTHUCHIEN = _songay + "@" + _text_duongdung + "@_param_huongdan@" + _total + "@" + _sang + "@" + _trua + "@" + _chieu + "@" + _toi;

                DataRowView drvThuoc = lookup_Thuoc.SelectDataRowView;
                txtGHICHU.EditValue = (_songay + " ngày, " + "Ngày " + (_total / _songay) + " "
                    + (drvThuoc == null ? "" : drvThuoc["TEN_DVT"].ToString())
                    + " chia " + _tong_lan);

                #region tạm check với HCM
                if (_row.CACHDUNG == "1")
                {
                    txtGHICHU.Text = "";
                    string dvt = (drvThuoc == null ? "" : drvThuoc["TEN_DVT"].ToString());
                    string _ghichu = "";
                    string sang = "";
                    string trua = "";
                    string chieu = "";
                    string toi = "";
                    string _tgsd = lookup_ThoiGian.SelectText;
                    string _cachdung = lookup_CachDung.Text;
                    string _duongdung = cbo_DuongDung.SelectText;

                    if (_sang != 0) sang = "Sáng " + _sang + " " + dvt +", "; 
                    if (_trua != 0) trua = "Trưa " + _trua + " " + dvt + ", "; 
                    if (_chieu != 0) chieu = "Chiều " + _chieu + " " + dvt + ", "; 
                    if (_toi != 0) toi = "Tối " + _toi + " " + dvt + ", "; 

                    if (_row.FORMAT_CD == "1")
                    { // đường dùng + thời gian dùng + cách dùng + sang/trua/chieu/toi. 						
                        _ghichu = "";
                        if (_duongdung != "") _duongdung += ", ";
                        if (_tgsd != "") _tgsd += ", ";
                        if (_cachdung != "") _cachdung += ", ";
                        _ghichu = _duongdung + _tgsd + _cachdung + sang + trua + chieu + toi;
                    }
                    else if (_row.FORMAT_CD == "2")
                    { // đường dùng + ngày + sl + DVT + thời gian dùng + cách dùng + sang/trua/chieu/toi.                        
                        _ghichu = "";
                        if (_duongdung != "") _duongdung += ", ";
                        if (_tgsd != "") _tgsd += ", ";
                        if (_cachdung != "") _cachdung += ", ";
                        double _soluong = Math.Round((_sang + _trua + _chieu + _toi), 0, MidpointRounding.AwayFromZero);
                        
                        if (_soluong <= 0)
                        {
                            _soluong = Func.Parse_double(txtSOLUONG_CHITIET.Text) / _songay;
                            _duongdung = cbo_DuongDung.SelectValue;
                            if (_duongdung.ToUpper() != "UỐNG") _ghichu = _duongdung + "Ngày " + _tgsd  + _cachdung;
                            else _ghichu = _duongdung + "Ngày " + _soluong + " " + dvt + " " + _tgsd + _cachdung;
                        }
                        else _ghichu = _duongdung + "Ngày " + _soluong + " " + dvt + " " + _tgsd   + _cachdung   + sang + trua + chieu + toi;
                    }
                    else
                    {
                        _ghichu = sang + trua + chieu + toi;
                        if (_songay != 0) _ghichu = _songay + " ngày, " + _ghichu;
                    }

                    if (_ghichu.EndsWith(", ")) _ghichu = _ghichu.Substring(0, _ghichu.Length - 2);

                    txtGHICHU.Text = _ghichu;
                }
                #endregion
            }
            else
            {
                _total = 0;
                hdHUONGDANTHUCHIEN = "@" + _text_duongdung + "@_param_huongdan@" + _total + "@@@@";

                txtGHICHU.Text = "";
                if (this.LOAIKEDON == "0") txtSOLUONG_CHITIET.EditValue = "";
            }

            if (OPTION == "02D017") hidLIEUDUNGBD = txtSOLUONG_CHITIET.Text + "g * 1 thang * " + txtSLTHANG.Text + " ngày"; 

            return _total;
        }
        private string hidLIEUDUNGBD = "";

        #endregion

        #region SỰ KIỆN ONCHANGE;
        private void PhieuDieuTri_Change(object sender, EventArgs e)
        {
            //    $("#cboPHIEU_CD").on('change', function (e) {
            //    var _sel = this.value;
            //    if(_sel != ""){
            //        var data_ar = jsonrpc.AjaxJson.ajaxCALL_SP_O("NTU02D010.09", _sel,[]);
            //        if(data_ar != null && data_ar.length > 0){
            //            var _row = data_ar[0];
            //            $("#txtMACHANDOANICD").val(_row.MACHANDOAN);
            //            $("#txtTENCHANDOANICD").val(_row.CHANDOAN);
            //            $("#txtTENCHANDOANICD_KT").val(_row.CHANDOAN_KEMTHEO);
            //        }
            //    }else{
            //        layThongTinBenhNhan();
            //    }
            //});
        }
        private void LoaiThuoc_Change(object sender, EventArgs e)
        {
            get_DSTenThuoc();
        }
        private void ucKhoThuoc_Change(object sender, EventArgs e)
        {
            get_DSTenThuoc();
        }

        private void ucSearchLookupThuoc_OnChange(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                cbo_DuongDung.lookUpEdit.EditValue = drv["DUONGDUNGID"].ToString();
                txtSO_NGAY.Focus();

                //Check trùng thuốc đã kê
                if (CheckTrungThuoc(drv) == false)
                {
                    lookup_Thuoc.SelectText = "";
                    lookup_Thuoc.Focus();
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        
        private void textEditSoNgay_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1") e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textEditSang_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1") e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textEditTrua_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1") e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textEditChieu_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1") e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textEditToi_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1") e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textEditSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textEdit2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1") e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textEdit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            if (_row.CAPTHUOCLE == "1") e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
            else e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSO_NGAY_EditValueChanged(object sender, EventArgs e)
        {
            doInputDrugType("");
        }
        private void txtSANG_EditValueChanged(object sender, EventArgs e)
        {
            doInputDrugType("");
        }
        private void txtTRUA_EditValueChanged(object sender, EventArgs e)
        {
            doInputDrugType("");
        }
        private void txtCHIEU_EditValueChanged(object sender, EventArgs e)
        {
            doInputDrugType("");
        }
        private void txtTOI_EditValueChanged(object sender, EventArgs e)
        {
            doInputDrugType("");
        }

        private void txtSOLUONG_CHITIET_EditValueChanged(object sender, EventArgs e)
        {
            doInputDrugType("1");
        }
        private void lookup_ThoiGian_Change(object sender, EventArgs e)
        {
            doInputDrugType("1");
        }
       
        #endregion

        #region Lưu, đóng, xóa, button click...
        #region Xóa, di chuyển trong ds các thuốc 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ucDSThuoc.gridControl.DataSource;
            dt.Rows.RemoveAt(ucDSThuoc.gridView.FocusedRowHandle);
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            int index = ucDSThuoc.gridView.FocusedRowHandle;
            if (index > 0)
            {
                DataTable dt = (DataTable)ucDSThuoc.gridControl.DataSource;
                DataRow firstSelectedRow = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++) firstSelectedRow[dt.Columns[i].ColumnName] = dt.Rows[index][i];

                dt.Rows.RemoveAt(index);
                dt.Rows.InsertAt(firstSelectedRow, index - 1);
            }
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = ucDSThuoc.gridView.FocusedRowHandle;
            DataTable dt = (DataTable)ucDSThuoc.gridControl.DataSource;
            if (index < dt.Rows.Count - 1)
            {
                DataRow firstSelectedRow = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++) firstSelectedRow[dt.Columns[i].ColumnName] = dt.Rows[index][i];

                dt.Rows.RemoveAt(index);
                dt.Rows.InsertAt(firstSelectedRow, index + 1);
            }
        }
        #endregion
        #region Các button click
        private void sbtnThem_Click(object sender, EventArgs e)
        {
            #region Check nhập Thuốc


            if (lookup_Thuoc.SelectValue == "")
            {
                lookup_Thuoc.Focus();
                MessageBox.Show("Bạn phải nhập thuốc để kê đơn!");
                return;
            }
            //if (cbo_DuongDung.SelectText == "")
            //{
            //    cbo_DuongDung.Focus();
            //    MessageBox.Show("Bạn phải nhập đường dùng!");
            //    return;
            //}

            DataRowView dr_thuoc = lookup_Thuoc.SelectDataRowView;

            int songay = 0;
            if (LOAIKEDON == "1")
            {
                if (Func.Parse(txtSONGAY_KE.Text) > Func.Parse(_row.SONGAYKEMAX))
                {
                    txtSONGAY_KE.Focus();
                    MessageBox.Show("Số ngày kê đơn không được lớn hơn " + _row.SONGAYKEMAX);
                    return;
                }
                if (_row.LOAICHECK == "0" && Func.Parse(txtSOLUONG_CHITIET.Text) > Func.Parse(_row.SOLUONGTHUOCMAX))
                {
                    txtSOLUONG_CHITIET.Focus();
                    MessageBox.Show("Số lượng kê đơn thuốc không được lớn hơn " + _row.SOLUONGTHUOCMAX);
                    return;
                }
                songay = Func.Parse(txtSONGAY_KE.Text);
            }
            else
            {
                if (_row.LOAICHECK == "0" && Func.Parse(txtSOLUONG_CHITIET.Text) > Func.Parse(_row.SOLUONGTHUOCMAX))
                {
                    txtSOLUONG_CHITIET.Focus();
                    MessageBox.Show("Số lượng kê đơn thuốc không được lớn hơn " + _row.SOLUONGTHUOCMAX);
                    return;
                }
                songay = Func.Parse(txtSO_NGAY.Text);
            }

            // bệnh nhân ngoại trú và kê thuốc.
            if (_row.LOAITIEPNHANID == "1" && (this.OPTION == "02D010" || this.OPTION == "02D017"))
            {
                int _ngayhanthe = 365;
                if (_row.DOITUONGBENHNHANID == "1") _ngayhanthe = Func.Parse(_row.HANTHE);

                if (_row.KIEUCHECK == "1" && _ngayhanthe <= 5 && songay > 7)
                {
                    MessageBox.Show("Hạn thẻ BHYT <= 5 ngày, số ngày kê thuốc ko được lớn hơn 7 ngày");
                    return;
                }

                if (_row.KIEUCHECK == "2" && _ngayhanthe < songay)
                {
                    MessageBox.Show("Số ngày kê thuốc không thể lớn hơn số ngày còn hạn thẻ");
                    return;
                }
            }

            if (layoutControlItem_SoNgay.Visibility== DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                && Func.Parse(txtSO_NGAY.Text) <= 0)
            {
                MessageBox.Show("Số ngày kê thuốc phải lớn hơn 0");
                return;
            }

            float _soluong_thuoc = Func.Parse_float(txtSOLUONG_CHITIET.Text);

            if (_row.LOAICHECK == "1"
                && Func.Parse_float(hidCANHBAOSOLUONG) > 0
                && _soluong_thuoc > Func.Parse_float(hidCANHBAOSOLUONG))
            {
                MessageBox.Show("Thuốc/Vật tư kê không được kê quá số lượng " + hidCANHBAOSOLUONG);
                return;
            }

            if (_soluong_thuoc > 0)
            { 
                if (_soluong_thuoc > Func.Parse_float(dr_thuoc["SLKHADUNG"].ToString()))
                {
                    txtSOLUONG_CHITIET.Focus();
                    MessageBox.Show("Số lượng kê đơn phải nhỏ hơn hoặc bằng số lượng khả dụng ("+ dr_thuoc["SLKHADUNG"].ToString() + ")");
                    return;
                }
            }
            else
            {
                txtSOLUONG_CHITIET.Focus();
                MessageBox.Show("Số lượng " + _lbl_text + " kê đơn phải lớn hơn 0!");
                return;
            }
            if (this.OPTION == "02D014" || this.OPTION == "02D016")
            {
                if (Math.Round(_soluong_thuoc) != _soluong_thuoc)
                {
                    txtSOLUONG_CHITIET.Focus();
                    MessageBox.Show("Số lượng trả " + _lbl_text + " phải là số nguyên dương!");
                    return;
                }
            }
            if (LOAIKEDON == "1") hdHUONGDANTHUCHIEN = "1@" + cbo_DuongDung.SelectText + "@" + txtGHICHU.Text + "@" + _soluong_thuoc + "@@@@";
            else hdHUONGDANTHUCHIEN = hdHUONGDANTHUCHIEN.Replace("_param_huongdan", txtGHICHU.Text);


            if (layoutControlItem_SL_Lan.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && txtSoLuongTrenLan.Text.Trim() == "")
            {
                txtSoLuongTrenLan.Focus();
                MessageBox.Show("Bạn phải nhập số lượng trên lần!");
                return;
            }
            if (layoutControlItem_Lan_Ngay.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && txtSoLanTrenNgay.Text == "")
            {
                txtSoLanTrenNgay.Focus();
                MessageBox.Show("Bạn phải nhập số lần trên ngày!");
                return;
            }

            #endregion

            //thêm 1 dòng vào table dataRowAdd
            addThuoc();
            //tính toán lại gia trị 1 số trường trong table dataRowAdd
            loadAll("", "");
            //reset lại các ô ngang: tên thuốc, sluong,...
            initThemThuoc();
        }
        public void addThuoc()
        {
            DataRow dr = dataRowAdd.NewRow();
            DataRowView drvThuoc = lookup_Thuoc.SelectDataRowView; 

            dr["TEN_THUOC"] = lookup_Thuoc.SelectText;
            dr["HOATCHAT"] = drvThuoc["HOATCHAT"].ToString();
            dr["DUONG_DUNG"] = cbo_DuongDung.SelectText;
            if (OPTION == "02D011" || OPTION == "02D019")
            {
                dr["DON_GIA"] = "0";
                dr["GIATRANBHYT"] = "0";
            }
            else
            {
                dr["DON_GIA"] = drvThuoc["GIA_BAN"].ToString();
                dr["GIATRANBHYT"] = drvThuoc["GIATRANBHYT"].ToString();
            }

                dr["SOLUONG"] = txtSOLUONG_CHITIET.Text;
            dr["THANH_TIEN"] = "";
            dr["BH_TRA"] = "";
            dr["ND_TRA"] = "";
            dr["LOAI_DT_CU"] = "";  
            dr["LOAI_DT_MOI"] = "";
            dr["DUONGDUNGE"] = txtGHICHU.EditValue;
            dr["DONVI_TINH"] = drvThuoc["TEN_DVT"].ToString();
            dr["NHOM_MABHYT_ID"] = drvThuoc["NHOM_MABHYT_ID"].ToString();  
           dr["TYLEBHYT_TVT"] = drvThuoc["TYLEBHYT_TVT"].ToString();
            dr["THUOCVATTUID"] = drvThuoc["THUOCVATTUID"].ToString();
            dr["ID_DT_MOI"] = _macdinh_hao_phi;  //  ==_itemDrug.ID_DT_MOI chi tiết của Thuốc   if(_option == '02D014' || _option == '02D016' || _option == '02D018'){  _macdinh_hao_phi = _itemDrug.ID_DT_MOI;
            dr["STT"] = "";
            dr["ID_DT_CU"] = _doituongbenhnhanid;
            dr["MA_THUOC"] = drvThuoc["MA_THUOC"].ToString();
            dr["HUONGDAN_SD"] = hdHUONGDANTHUCHIEN;
            dr["DUONGDUNGID"] = drvThuoc["DUONGDUNGID"].ToString();
            dr["DICHVUKHAMBENHID"] = "";
            dr["ACTION"] = "";
            dr["MAHOATCHAT"] = drvThuoc.DataView.Table.Columns.Contains("MAHOATCHAT") ? drvThuoc["MAHOATCHAT"].ToString() : "";
            dr["OLDVALUE"] = txtSOLUONG_CHITIET.Text;
            dr["KHOANMUCID"] = drvThuoc["KHOANMUCID"].ToString();

            dr["KHO_THUOCID"] = cbo_KhoThuoc.SelectValue;
            dr["DVQD"] = "";

            string r_lieudung = "";
            if (_row.SUDUNG_LIEUDUNG == "1")
            {
                r_lieudung = txtLieuDung.Text;
                if (layoutControlItem_LieuDung.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                if (r_lieudung == "" && ( OPTION == "02D010" || OPTION == "02D017"))
                {
                    MessageBox.Show("Hãy nhập liều dùng");
                    txtLieuDung.Focus();
                    return;
                }
            }
            else
            {
                r_lieudung = txtGHICHU.Text;
                if (OPTION == "02D017") r_lieudung = hidLIEUDUNGBD;  
            }
            dr["LIEUDUNG"] = r_lieudung;

            dr["KETRUNGHOATCHAT"] = drvThuoc.DataView.Table.Columns.Contains("KETRUNGHOATCHAT") ? drvThuoc["KETRUNGHOATCHAT"].ToString() : "";
            dr["LOAITVTID"] = hidLOAITVTID;
            dr["THUOCSAO"] = "";

            dataRowAdd.Rows.Add(dr);

            hidLOAITVTID = "";
        }
        private void loadAll(string _rowId, string _loadDTMoi)
        {
            tinhTruocTien();


            string _obj_new = "";
            DataRowView _param;
            float _number;
            float _don_gia;

            for (var i = 0; i < dataRowAdd.Rows.Count; i++)
            {
                float row_Price = 0;
                float row_Insr = 0;
                float row_End = 0;
                string row_ten_loai_tt_moi = "";
                _param = dataRowAdd.DefaultView[i];
                if (_rowId == _param["THUOCVATTUID"].ToString()) _obj_new = _loadDTMoi;
                else _obj_new = _param["ID_DT_MOI"].ToString();

                _number = Func.Parse_float(_param["SOLUONG"].ToString());
                if (_number > 0)
                {
                    _don_gia = Func.Parse_float(_param["DON_GIA"].ToString());
                    #region tạo tham số
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
                    dt.Columns.Add("NGAYHANTHE", typeof(String));
                    dt.Columns.Add("NGAYDICHVU", typeof(String));
                    dt.Columns.Add("TYLE_MIENGIAM", typeof(String));
                    dt.Columns.Add("NHOMDOITUONG", typeof(String));
                    dt.Columns.Add("STT", typeof(String));

                    DataRow dr = dt.NewRow();
                    dr[0] = _doituongbenhnhanid;
                    dr[1] = _row.TYLE_BHYT;
                    dr[2] = _param["GIATRANBHYT"].ToString();
                    dr[3] = _don_gia;
                    dr[4] = _don_gia;
                    dr[5] = _don_gia;
                    dr[6] = _don_gia;
                    dr[7] = _obj_new;
                    dr[8] = "0";
                    dr[9] = _param["NHOM_MABHYT_ID"].ToString();
                    dr[10] = _number;
                    dr[11] = "0";
                    dr[12] = _row.TRADU6THANGLUONGCOBAN;
                    dr[13] = _row.DUOC_VAN_CHUYEN;
                    dr[14] = _param["TYLEBHYT_TVT"].ToString();
                    dr[15] = _row.BHYT_KT;
                    dr[16] = Func.getSysDatetime(Const.FORMAT_date1);
                    dr[17] = _row.TYLE_MIENGIAM == null ? "" : _row.TYLE_MIENGIAM;
                    dr[18] = "";
                    dr[19] = "";
                    dt.Rows.InsertAt(dr, 0);
                    #endregion
                    // function vien phi 
                    DataTable dtVienPhi = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dt);

                    if (dtVienPhi.Rows[0]["tong_cp"].ToString() == "-1")
                    {
                        if (_loadDTMoi != "") MessageBox.Show("Chuyển đối tượng thanh toán không hợp lệ!");
                    }
                    else
                    {
                        row_Price = Func.Parse_float(dtVienPhi.Rows[0]["tong_cp"].ToString());
                        row_Insr = Func.Parse_float(dtVienPhi.Rows[0]["bh_tra"].ToString());
                        row_End = Func.Parse_float(dtVienPhi.Rows[0]["nd_tra"].ToString());
                         

                        if (_GLB_CACH_TINH_TIEN == "1")
                        {
                            row_Insr = (_don_gia * _number * Func.Parse_float(_param["TYLEBHYT_TVT"].ToString())) / 100;
                            if (row_Price > 0) row_End = row_Price - row_Insr;
                            else row_End = 0;
                        } 

                        if (dtVienPhi.Columns.Contains("ten_loai_tt_moi")) row_ten_loai_tt_moi = dtVienPhi.Rows[0]["ten_loai_tt_moi"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng thuốc phải là số nguyên dương!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                dataRowAdd.Rows[i]["THANH_TIEN"] = row_Price.ToString("N0", CultureInfo.InvariantCulture);
                dataRowAdd.Rows[i]["BH_TRA"] = row_Insr.ToString();
                dataRowAdd.Rows[i]["ND_TRA"] = row_End.ToString();
                dataRowAdd.Rows[i]["LOAI_DT_CU"] = _row.TEN_DTBN; // web là _ten_doituong_benhnhan
                dataRowAdd.Rows[i]["LOAI_DT_MOI"] = row_ten_loai_tt_moi;
                //$("#"+_gridDonThuoc).jqGrid('setCell',rowIds[i],13,_doituongbenhnhanid);
                //$("#"+_gridDonThuoc).jqGrid('setCell',rowIds[i],14,_obj_new); 
            }
            //Cap nhat lai tien don thuoc khi sua so luong
            //$('#lblMUCHUONG_BHYT').val(_row.TYLE_BHYT +"%");
            //$('#lblMA_BHYT').val(_ma_bhyt);
            //$('#lblDT_THANHTOAN').val(_row.TEN_DTBN);
        }

        private void sbtnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                Luu_DonThuoc(this.i_action);
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void Luu_DonThuoc(string r_action)
        {
            //r_action: Add  Lưu đơn thuốc
            //r_action: Upd  update
            //r_action: SAVE_TEMP Lưu MAUBENHPHAMID
            //r_action:  
            #region Check nhập
            if ((r_action == "Upd" || r_action == "Add") && ucSearchLookupICD.SelectedValue == "" && r_checkicd == "1")
            {
                MessageBox.Show("Bạn phải nhập ICD10!");
                ucSearchLookupICD.Focus();
                return;
            }

            string sysdate = Func.getSysDatetime(Const.FORMAT_date1);
            DateTime b_ngaytiepnhan = Func.ParseDate(_row.NGAYTIEPNHAN.Substring(0, 10));
            DateTime b_ngaykham_tn = Func.ParseDate(sysdate);
            DateTime b_ngay_chidinh = Func.ParseDate(dtimeNgayChiDinh.DateTime.ToString(Const.FORMAT_date1));
            DateTime b_ngay_dung = Func.ParseDate(dtimeNgayDung.DateTime.ToString(Const.FORMAT_date1));

            // check ngày cấp thuốc với ngày hiện tại
            if (_row.LOAITIEPNHANID == "1" && b_ngay_chidinh > b_ngaykham_tn)
            {
                dtimeNgayChiDinh.Focus();
                MessageBox.Show("Ngày chỉ định không lớn hơn ngày hiện tại!");
                return;
            }

            if (_row.LOAITIEPNHANID == "0" && DateTime.ParseExact(_row.NGAYKEMAX, "ddMMyyyy", CultureInfo.InvariantCulture) < b_ngay_chidinh)
            {
                MessageBox.Show("Thời gian chỉ định không được vượt quá số ngày cho phép!");
                return;
            }

            // check ngày cấp thuốc với ngày tiep nhan 
            if (b_ngay_chidinh < b_ngaytiepnhan)
            {
                dtimeNgayChiDinh.Focus();
                MessageBox.Show("Ngày giờ chỉ định không nhỏ hơn ngày giờ tiếp nhận!");
                return;
            }

            if (Func.Parse(txtSONGAY_KE.Text) > Func.Parse(_row.SONGAYKEMAX))
            {
                txtSONGAY_KE.Focus();
                MessageBox.Show("Số ngày kê thuốc không lớn hơn " + _row.SONGAYKEMAX + " ngày");
                return;
            }

            //_thoigian_vaovien
            DateTime _tg_vaovien = Func.ParseDate(_row.THOIGIANVAOVIEN.Substring(0, 10));
            if (_tg_vaovien > b_ngay_chidinh)
            {
                dtimeNgayChiDinh.Focus();
                MessageBox.Show("Thời gian kê đơn phải lớn hơn thời gian vào viện!");
                return;
            }
            if (b_ngay_chidinh > b_ngay_dung)
            {
                dtimeNgayDung.Focus();
                MessageBox.Show("Thời gian kê đơn phải nhỏ hơn thời gian dùng!");
                return;
            }

            if (dataRowAdd.Rows.Count <= 0)
            {
                MessageBox.Show("Bạn chưa nhập" + _lbl_text + " cho đơn thuốc!");
                return;
            }

            for (int k = 0; k < dataRowAdd.Rows.Count; k++)
            {
                //if($('#'+_gridDonThuoc).find("input[id*='SOLUONG']").length > 0){
                //    MessageBox.Show('Tồn tại trường số lượng đang sửa trong đơn'+_lbl_text+'!');
                //$('#btnSave').prop('disabled', false);
                //return false;
                //}

                if (dataRowAdd.Rows[k]["ID_DT_MOI"] == null || dataRowAdd.Rows[k]["ID_DT_MOI"].ToString() == "")
                {
                    MessageBox.Show("Thuốc chưa có loại đối tượng thanh toán");
                    return;
                }
            }
            #endregion

            #region Tạo dl Lưu
            bool _print = false;
            DataTable dt_tem = dataRowAdd.Copy();
            dt_tem.Columns["SOLUONG"].ColumnName = "SO_LUONG"; 
            string jsonDSThuoc = Newtonsoft.Json.JsonConvert.SerializeObject(dt_tem);

            string json = "";
            json += "\"DS_THUOC\":" + jsonDSThuoc + ","; 
            json += Func.json_item("BENHNHANID", "");
            json += Func.json_item("MA_CHANDOAN", ucSearchLookupICD.searchLookUpEdit1.Text);  
            json += Func.json_item("CHANDOAN", ucSearchLookupICD.textEdit1.Text);
            json += Func.json_item("CHANDOAN_KT", ucSearchLookupBP.textEdit1.Text);
            json += Func.json_item("DUONG_DUNG", dt_tem.Rows[0]["DUONGDUNGID"].ToString());
            json += Func.json_item("NGUOIDUNG_ID", Const.local_user.USER_ID);
            json += Func.json_item("CSYT_ID", Const.local_user.HOSPITAL_ID);

            json += Func.json_item("INS_TYPE", _loai_don);
            json += Func.json_item("I_ACTION", r_action);
            json += Func.json_item("MAUBENHPHAMID", MAUBENHPHAMID);
            if (cbo_PhieuDieuTri.SelectValue != null && cbo_PhieuDieuTri.SelectValue != "-1")  
                json += Func.json_item("PHIEUDIEUTRI_ID", cbo_PhieuDieuTri.SelectValue);
            else
                json += Func.json_item("PHIEUDIEUTRI_ID", "");

            json += Func.json_item("NGAYMAUBENHPHAM", dtimeNgayChiDinh.Text);
            json += Func.json_item("NGAYMAUBENHPHAM_SUDUNG", dtimeNgayDung.Text);  
            json += Func.json_item("DICHVUCHA_ID", DICHVUCHAID);
            json += Func.json_item("DOITUONG_BN_ID", _doituongbenhnhanid);
            json += Func.json_item("TYLE_BHYT", _row.TYLE_BHYT);
            json += Func.json_item("NGAY_BHYT_KT", _row.BHYT_KT);
            //json += Func.json_item("HINH_THUC_KE", _param["MAHOATCHAT"].ToString());
            if (layoutControlItem_txtSONGAY_KE.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                json += Func.json_item("SONGAY_KE", "");
            else
                json += Func.json_item("SONGAY_KE", txtSONGAY_KE.Text);

                if (LOAIKEDON !="0")
            json += Func.json_item("KIEUKEDON", LOAIKEDON); 
            else
                json += Func.json_item("KIEUKEDON", "");

            json += Func.json_item("MAUBENHPHAMCHA_ID", ""); // _phieutraid value của cbo >Đơn thuốc/VT trả  
            json += Func.json_item("KHAMBENHID", KHAMBENHID);
            json += Func.json_item("KHOAID", "" + Const.local_khoaId);
            json += Func.json_item("PHONGID", "" + Const.local_phongId);
            json += Func.json_item("GHICHU_BENHCHINH", "");
            json += Func.json_item("TEMP_CODE", txtLuuMau.Text);
            if (!dt_tem.Columns.Contains("DUONGDUNGE")) json += Func.json_item("DUONGDUNGE", "");
            json += Func.json_item("YKIENBACSY", ucSearchLookup_LoiDan.SelectedText);

            if (OPTION == "02D017" || OPTION == "02D018") 
            {
                json += Func.json_item("SLTHANG", txtSLTHANG.Text);
            }
            else json += Func.json_item("SLTHANG", "1");

            json += Func.json_item("NGAYHEN", txtTG_HENKHAM.Text);
            if (ckbCapPhieuHenKham.Checked) json += Func.json_item("PHIEUHEN", "1");
            else json += Func.json_item("PHIEUHEN", "0");

            json += Func.json_item("HINH_THUC_KE", "");
            json += Func.json_item("OPTION", this.OPTION);
            if (OPTION != "02D011" && OPTION != "02D019")
            {
                json += Func.json_item("LOAITVTID", cbo_LoaiThuoc.SelectValue);
            }
            json += Func.json_item("DUONGDUNG", dt_tem.Rows[0]["DUONGDUNGID"].ToString()); 
           json += Func.json_item("KHO_THUOCID", cbo_KhoThuoc.SelectValue);

            
             
            json += Func.json_item("DUONGDUNGE", dt_tem.Rows[0]["DUONGDUNGE"].ToString());  



            json = Func.json_item_end(json);
            json = json.Replace("\"", "\\\"");

            //            {"func":"ajaxCALL_SP_S","params":["NTU02D010.12","{\"DS_THUOC\":[{\"THUOCVATTUID\"  
            //:\"412076\",\"TEN_THUOC\":\"Kim Lấy Thuốc các cỡ- Vinahankook-VN\",\"HOATCHAT\":\"Kim lấy máu, lấy thuốc các loại, các cỡ\"
            //,\"DONVI_TINH\":\"cái\",\"DUONG_DUNG\":\"\",\"DON_GIA\":\"348\",\"SO_LUONG\":\"4\",\"THANH_TIEN\":\"1,392\",\"BH_TRA\":\"0\"
            //,\"ND_TRA\":\"1,392\",\"LOAI_DT_CU\":\"\",\"LOAI_DT_MOI\":\"\",\"ID_DT_CU\":\"2\",\"ID_DT_MOI\":\"0\",\"MA_THUOC\":\"HNM_DK_00003\"
            //,\"HUONGDAN_SD\":\"2@@2 ngày, Sáng 1, Trưa1@4@1@1@0@0\",\"DUONGDUNGID\":\"0\",\"NHOM_MABHYT_ID\":\"0\",\"GIATRANBHYT\":\"0\",
            //\"KHOANMUCID\":\"505\",\"DICHVUKHAMBENHID\":\"\",\"DUONGDUNGE\":\"<button type=\\\"button\\\" class=\\\"btn btn-link\\\" 
            //style=\\\"white-space: normal;\\\" id=\\\"btnTHUOC_412076\\\">2 ngày, Sáng 1, Trưa1</button>\",\"ACTION\":\"\",\"MAHOATCHAT\":
            //\"N03.02.060\",\"OLDVALUE\":\"4\"}],\"BENHNHANID\":\"\",\"MA_CHANDOAN\":\"A19.1\",\"CHANDOAN\":\"Lao kê cấp của nhiều vị trí\",
            //\"CHANDOAN_KT\":\"\",\"DUONG_DUNG\":\"0\",\"NGUOIDUNG_ID\":\"1226\",\"CSYT_ID\":\"\",\"KHO_THUOCID\":\"571\",\"INS_TYPE\":\"1\",
            //\"I_ACTION\":\"Add\",\"MAUBENHPHAMID\":\"\",\"PHIEUDIEUTRI_ID\":\"\",\"NGAYMAUBENHPHAM\":\"04/10/2017 09:11:33\",\"NGAYMAUBENHPHAM_SUDUNG\"
            //:\"04/10/2017 09:11:33\",\"DICHVUCHA_ID\":\"\",\"DOITUONG_BN_ID\":\"2\",\"TYLE_BHYT\":\"0\",\"NGAY_BHYT_KT\":\"\",\"HINH_THUC_KE\":
            //\"\",\"SONGAY_KE\":\"\",\"MAUBENHPHAMCHA_ID\":\"\",\"KHAMBENHID\":\"8527\",\"KHOAID\":\"4001\",\"PHONGID\":\"4136\",\"GHICHU_BENHCHINH\":
            //\"\",\"TEMP_CODE\":\"\",\"DUONGDUNGE\":\"\",\
            //"YKIENBACSY\":\"\",\"SLTHANG\":\"1\",\"NGAYHEN\":\"\",\"PHIEUHEN\":\"0\"}"],"uuid":"d497d021-7eb9-48b2-a533-29919a1fdbc5"}
            #endregion

            json = json.Replace(",\\\"up\\\":null,\\\"down\\\":null,\\\"delete\\\":null", "");

            string ret = ServiceChiDinhThuoc.getSP_S("NTU02D010.12", json);
            string[] rets = ret.Split(',');
            _badaingay = rets[0];

            #region Thông báo Kết quả Lưu
            if (Func.Parse(rets[0]) >= 1) // lưu thành công
            {
                this.Close();
                event_ListenFrm_KetQua_Thuoc_ChiDinhDV("", null);
                get_DSTenThuoc(); // lấy lại ds thuốc vì mỗi lần kê đơn số lượng khả dụng thay đổi

                string msg = "";
                if (r_action != "SAVE_TEMP")
                {
                    //Cấu hình ko hiển thị thông báo
                    string _hienthiThongBaoDonThuoc = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KBH_TUDONGLUU_KBHB$");

                    if (OPTION == "02D010")
                    {
                        _print = true;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo phiếu thuốc thành công";
                    }
                    else if (OPTION == "02D014")
                    {
                        _print = false;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo phiếu trả thuốc thành công";
                    }
                    else if (OPTION == "02D015")
                    {
                        _print = true;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo phiếu vật tư thành công";
                    }
                    else if (OPTION == "02D016")
                    {
                        _print = false;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo phiếu trả vật tư thành công";
                    }
                    else if (OPTION == "02D011")
                    {
                        _print = true;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo đơn thuốc mua ngoài thành công";
                    }
                    else if (OPTION == "02D017")
                    {
                        _print = true;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo đơn thuốc YHCT thành công";
                    }
                    else if (OPTION == "02D018")
                    {
                        _print = true;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo đơn thuốc trả YHCT thành công";
                    }
                    else if (OPTION == "02D019")
                    {
                        _print = true;
                        msg = _hienthiThongBaoDonThuoc == "1" ? "" : "Tạo đơn thuốc nhà thuốc thành công";
                    }

                    if (_print)
                    {

                        //if (_loaitiepnhanid == "1" && (_loainhommaubenhpham_id == "7" || _loainhommaubenhpham_id == '-1'))
                        //{
                        //    _inDonThuoc(rets[0], _opts.phongId);
                        //}
                        //else if (_indonthuoc_noitru == "1" && (_loainhommaubenhpham_id == "7" || _loainhommaubenhpham_id == '-1'))
                        //{
                        //    _inDonThuoc(rets[0], _opts.phongId);
                        //}

                        // --> đoạn code ngớ ngẩn trên của web đc sửa như sau:

                        if (    (_loainhommaubenhpham_id == "7" || _loainhommaubenhpham_id == "-1")
                           &&   (_row.LOAITIEPNHANID == "1" || _row.INDONTHUOC == "1")
                            )
                                _inDonThuoc(rets[0]);

                        // đóng form lại, về form cha mở Xử trí-Phiếu khám bệnh
                        if (event_ListenFrm_KetQua_Thuoc_ChiDinhDV!=null) event_ListenFrm_KetQua_Thuoc_ChiDinhDV("mo_form_xu_tri", null);
                    }

                    _luu = "1"; 
                }
                else
                {
                    btnLuu.Enabled = false;
                    MessageBox.Show("Tạo đơn thuốc mẫu thành công!");
                }
            }
            else if (ret == "-1")
            {
                btnLuu.Enabled = true;
                MessageBox.Show("Lưu không thành công!");
            }
            //tuyennx_add_start_20170727  y/c HISL2BVDKHN-247
            else if (ret == "ngaydichvu")
            {
                btnLuu.Enabled = true;
                MessageBox.Show("Bệnh nhân có thời gian chỉ định dịch vụ lớn hơn thời gian ra viện đã cấp thuốc thành công nhưng chưa kết thúc khám");
            }
            //tuyennx_add_end_20170727 
            else if (ret == "-2")
            {
                btnLuu.Enabled = true;
                MessageBox.Show("Có lỗi khi chuyển phiếu!");
            }
            else if (ret == "-4")
            {
                btnLuu.Enabled = true;
                MessageBox.Show("Lỗi khi hủy" + _lbl_text + "!");
            }
            else if (ret == "-5")
            {
                btnLuu.Enabled = true;
                MessageBox.Show("Lỗi khi tính giá dịch vụ cao vượt mức trần Bảo hiểm!");
            }
            else if (ret == "-6")
            {
                btnLuu.Enabled = true;
                MessageBox.Show("Lỗi khi yêu cầu trả" + _lbl_text + "!");
            }
            else if (ret == "-7")
            {
                MessageBox.Show("Lỗi do nhập sai mã ICD10!");
                btnLuu.Enabled = true;
                ucSearchLookupICD.Focus();
            }
            else if (ret == "cophieudangsua")
            {
                MessageBox.Show("Bệnh nhân có phiếu CLS/Đơn thuốc đang sửa, không kết thúc khám được.");
            }
            else
            {
                btnLuu.Enabled = true;
                MessageBox.Show(ret);
            }
            #endregion
        }
        private void btnLuuMau_Click(object sender, EventArgs e)
        {
            if (txtLuuMau.Text.Trim() != "")
                Luu_DonThuoc("SAVE_TEMP");
            else
                MessageBox.Show("Bạn phải nhập tên đơn thuốc mẫu!");
        }
        private void btnDonThuocMau_Click(object sender, EventArgs e)
        {
            NGT02K018_DonThuocMau frm = new NGT02K018_DonThuocMau();
            frm.setParam(_loainhommaubenhpham_id, _row.LOAITIEPNHANID);
            frm.setReturnData(ReturnData_NGT02K018_DonThuocMau);
            openForm(frm, "1");
        }
        string kho_id_DTmau = "";
        private void ReturnData_NGT02K018_DonThuocMau(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)sender;
                if (!(dt != null && dt.Rows.Count > 0 && dt.Columns.Contains("MAUBENHPHAM_TEMPID") && dt.Columns.Contains("KHOID")))
                    return;

                string maubenhpham_id = dt.Rows[0]["MAUBENHPHAM_TEMPID"].ToString();
                kho_id_DTmau = dt.Rows[0]["KHOID"].ToString();

                if (string.IsNullOrEmpty(maubenhpham_id)) return;

                // Thiết lập Kho theo kho của đơn mẫu và khóa ko cho thay đổi
                cbo_KhoThuoc.SelectValue = kho_id_DTmau;
                if (cbo_KhoThuoc.SelectText =="")
                {
                    MessageBox.Show("Đơn thuốc mẫu có kho thuốc không phù hợp!");
                    return;
                }
                cbo_KhoThuoc.Enabled = false;

                // 
                string check = "";
                // {"func":"ajaxCALL_SP_S","params":["NTU02D010.21","4302$100765$06/02/2018 08:42:48"],"uuid":"80c83951-bc18-4717-8d9d-ba830838e14b"}
                if (_row.LOAITIEPNHANID != "0")
                    check = RequestHTTP.call_ajaxCALL_SP_S_result("NTU02D010.21", maubenhpham_id + "$" + this.KHAMBENHID + "$" + dtimeNgayDung.DateTime.ToString(Const.FORMAT_datetime1));

                if (!string.IsNullOrEmpty(check))
                {
                    if (MessageBox.Show(check + " đã được chỉ định trong ngày, có đồng ý load mẫu không có chỉ định trùng?"
                        , "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        string dichvu_id = RequestHTTP.call_ajaxCALL_SP_S_result("NTU02D010.23", maubenhpham_id + "$" + this.KHAMBENHID + "$" + dtimeNgayDung.DateTime.ToString(Const.FORMAT_datetime1));
                        // load từ đơn mẫu không có chỉ định trùng
                        Load_DonMau(maubenhpham_id, dichvu_id);
                        //loadGridDonThuoc('TEMP', _maubenhpham_temp_id, '1');
                    }
                    else return;
                }
                else
                {
                    // load từ đơn mẫu
                    Load_DonMau(maubenhpham_id, "");
                    // loadGridDonThuoc('TEMP', _maubenhpham_temp_id);/
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        // load từ đơn mẫu
        private void Load_DonMau(string maubenhpham_id, string dichvu_id)
        {
            // Lấy lời dặn BS
            // {"func":"ajaxCALL_SP_O","params":["NTU02D010.20","4302",0],"uuid":"80c83951-bc18-4717-8d9d-ba830838e14b"}
            // {"result": "[{\"LOIDANBS\": \"\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
            string LOIDANBS = "";
            DataTable dtLoiDanBS = RequestHTTP.call_ajaxCALL_SP_O("NTU02D010.20", maubenhpham_id, 0);
            if (dtLoiDanBS.Rows.Count > 0 && dtLoiDanBS.Columns.Contains("LOIDANBS"))
                LOIDANBS = dtLoiDanBS.Rows[0]["LOIDANBS"].ToString();
            // $('#txtLOIDANBS').val(data_ar[0].LOIDANBS);



            string[] listName = new string[] { "[0]", "[1]", "[2]" };
            string[] listValue = new string[] { Const.local_user.COMPANY_ID, _loainhommaubenhpham_id, maubenhpham_id };

            string paramsName = "NTU02D010.05";

            if (dichvu_id != "")
            {
                paramsName = "NTU02D010.22";
                Array.Resize(ref listName, listName.Length + 1);
                listName[listName.Length - 1] = "[" + (listName.Length - 1) + "]";
                Array.Resize(ref listValue, listValue.Length + 1);
                listValue[listValue.Length - 1] = dichvu_id;
            }
            //     if (diff == '2')
            //{
            //    paramsName = "NTU02D075.EV006";
            //    listName = new string[] { "[0]", "[1]"};
            //    listValue = new string[] { this._doituongbenhnhanid, maubenhpham_id }; 
            //} 

            #region Check trùng hoạt chất
            if (_row.LOAITIEPNHANID == "1")
            {
                dataRowAdd.Rows.Clear(); // xóa toàn bộ ds thuốc đang kê để load đt mẫu

                // {"func":"ajaxExecuteQueryO","params":["","NTU02D010.05"],
                // "options":[{"name":"[0]","value":"902"},{"name":"[1]","value":"7"},{"name":"[2]","value":"4302"}],
                // "uuid":"80c83951-bc18-4717-8d9d-ba830838e14b"}
                DataTable vtmp = RequestHTTP.get_ajaxExecuteQueryO(paramsName, listName, listValue);
                DataTable DSThuoc_objTmpThuoc = (DataTable)ucDSThuoc.gridControl.DataSource;

                int dtGrid_count = DSThuoc_objTmpThuoc.Rows.Count;
                for (int k = 0; k < vtmp.Rows.Count; k++)
                {
                    bool ischeckhc = false;
                    string vtmp_KETRUNGHOATCHAT = vtmp.Columns.Contains("KETRUNGHOATCHAT") ? vtmp.Rows[k]["KETRUNGHOATCHAT"].ToString() : "";
                    for (int i = 0; i < dtGrid_count; i++)
                    {
                        string temp_KETRUNGHOATCHAT = DSThuoc_objTmpThuoc.Rows[i]["KETRUNGHOATCHAT"].ToString();
                        // if(vtmp[k].KETRUNGHOATCHAT != "1" && _objTmpThuoc[i].KETRUNGHOATCHAT != "1" && (vtmp[k].MAHOATCHAT == _objTmpThuoc[i].MAHOATCHAT)){
                        if (vtmp_KETRUNGHOATCHAT != "1" && temp_KETRUNGHOATCHAT != "1" 
                            && (vtmp.Rows[k]["MAHOATCHAT"].ToString() == DSThuoc_objTmpThuoc.Rows[i]["MAHOATCHAT"].ToString()))
                        {
                            ischeckhc = true;
                            break;
                        }
                    }

                    if (ischeckhc)
                    {
                        string thuoc_trung = "," + vtmp.Rows[k]["TEN_THUOC"].ToString();
                        MessageBox.Show("Thuốc [" + thuoc_trung + "] có hoạt chất trùng với thuốc đã kê tại phòng khám hiện tại hoặc phòng khám khác trong ngày!");
                        if (_row.CHANHOATCHAT == "1") return;
                    }
                }
            }
            #endregion

            #region Thêm vào grid
            Array.Resize(ref listName, listName.Length + 1);
            listName[listName.Length - 1] = "[S" + (listName.Length - 1) + "]";
            Array.Resize(ref listValue, listValue.Length + 1);
            listValue[listValue.Length - 1] = Const.local_user.COMPANY_ID;

            DataTable dtAddFromDTMau = RequestHTTP.get_ajaxExecuteQueryO(paramsName, listName, listValue);
            for (int k = 0; k < dtAddFromDTMau.Rows.Count; k++)
            {
                // thêm vào grid
                DataRow dr = dataRowAdd.NewRow();

                dr["TEN_THUOC"] = dtAddFromDTMau.Rows[k]["TEN_THUOC"].ToString();

                lookup_Thuoc.SelectValue = dtAddFromDTMau.Rows[k]["THUOCVATTUID"].ToString();
                DataRowView drvThuoc = lookup_Thuoc.SelectDataRowView;
                if (drvThuoc != null) dr["HOATCHAT"] = drvThuoc["HOATCHAT"].ToString();
                else dr["HOATCHAT"] = "";
                lookup_Thuoc.SelectValue = "";

                dr["DUONG_DUNG"] = dtAddFromDTMau.Rows[k]["DUONG_DUNG"].ToString();
                dr["DON_GIA"] = dtAddFromDTMau.Rows[k]["DON_GIA"].ToString();
                dr["SOLUONG"] = dtAddFromDTMau.Rows[k]["SO_LUONG"].ToString();
                dr["THANH_TIEN"] = dtAddFromDTMau.Rows[k]["THANH_TIEN"].ToString();
                dr["BH_TRA"] = dtAddFromDTMau.Rows[k]["BH_TRA"].ToString();
                dr["ND_TRA"] = dtAddFromDTMau.Rows[k]["ND_TRA"].ToString();
                dr["LOAI_DT_CU"] = dtAddFromDTMau.Rows[k]["LOAI_DT_CU"].ToString();
                dr["LOAI_DT_MOI"] = dtAddFromDTMau.Rows[k]["LOAI_DT_MOI"].ToString();
                try
                {
                    string DUONGDUNGE = dtAddFromDTMau.Rows[k]["HUONGDAN_SD"].ToString();// "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0"
                    DUONGDUNGE = DUONGDUNGE.Substring(DUONGDUNGE.IndexOf("@") + 1);
                    DUONGDUNGE = DUONGDUNGE.Substring(DUONGDUNGE.IndexOf("@") + 1);
                    DUONGDUNGE = DUONGDUNGE.Substring(0, DUONGDUNGE.IndexOf("@"));
                    dr["DUONGDUNGE"] = DUONGDUNGE; //5 ngày, Ngày 4 Viên chia 2
                }
                catch (Exception ex)
                {
                    dr["DUONGDUNGE"] = "";
                }
                dr["DONVI_TINH"] = dtAddFromDTMau.Rows[k]["DONVI_TINH"].ToString();
                dr["GIATRANBHYT"] = "0";
                dr["NHOM_MABHYT_ID"] = _nhom_mabhyt_id;
                dr["TYLEBHYT_TVT"] = dtAddFromDTMau.Rows[k]["TYLEBHYT_TVT"].ToString();
                dr["THUOCVATTUID"] = dtAddFromDTMau.Rows[k]["THUOCVATTUID"].ToString();
                dr["ID_DT_MOI"] = dtAddFromDTMau.Rows[k]["ID_DT_MOI"].ToString();
                dr["STT"] = "";
                dr["ID_DT_CU"] = dtAddFromDTMau.Rows[k]["ID_DT_CU"].ToString();
                dr["MA_THUOC"] = dtAddFromDTMau.Rows[k]["MA_THUOC"].ToString();
                dr["HUONGDAN_SD"] = dtAddFromDTMau.Rows[k]["HUONGDAN_SD"].ToString();
                dr["DUONGDUNGID"] = dtAddFromDTMau.Rows[k]["DUONGDUNGID"].ToString();
                dr["DICHVUKHAMBENHID"] = "";
                dr["ACTION"] = "";
                dr["MAHOATCHAT"] = dtAddFromDTMau.Rows[k]["MAHOATCHAT"].ToString();
                dr["OLDVALUE"] = dtAddFromDTMau.Rows[k]["OLDVALUE"].ToString();
                dr["KHOANMUCID"] = dtAddFromDTMau.Rows[k]["KHOANMUCID"].ToString();

                dr["KHO_THUOCID"] = kho_id_DTmau;
                //if (OPTION != "02D011") dr["KHO_THUOCID"] = kho_id_DTmau;
                //else dr["KHO_THUOCID"] = "";
                dr["DVQD"] = "";
                dr["LIEUDUNG"] = "";
                dr["KETRUNGHOATCHAT"] = dtAddFromDTMau.Columns.Contains("KETRUNGHOATCHAT") ? dtAddFromDTMau.Rows[k]["KETRUNGHOATCHAT"].ToString() : ""; //????

                dataRowAdd.Rows.Add(dr);
            }
            #endregion
        }

        private void btnDonThuocCu_Click(object sender, EventArgs e)
        {
            VNPT.HIS.CommonForm.NGT02K019_DonThuocCu frm = new VNPT.HIS.CommonForm.NGT02K019_DonThuocCu();
            frm.loadData(this.BENHNHANID);
            frm.setReturnData(ReturnData_NGT02K019_DonThuocCu);
            frm.FormClosing += childFormClosing;
            frm.StartPosition = FormStartPosition.CenterScreen;
            Enabled = false;
            frm.Show();
        }
        private void ReturnData_NGT02K019_DonThuocCu(object sender, EventArgs e)
        {
            try
            {
                string _maubenhpham_id = (string)sender;

                string check = RequestHTTP.call_ajaxCALL_SP_S_result("NTU02D010.24", _maubenhpham_id + "$" + _row.TIEPNHANID);
                string list_id_thuoc = "";
                if (check != "")
                {
                    if (MessageBox.Show(check + " đã tồn tại trong các đơn thuốc của bệnh nhân, có đồng ý load đơn không trùng hoạt chất?"
                        , "", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        // lấy list ID, cách nhau dấu phẩy ,
                        list_id_thuoc = RequestHTTP.call_ajaxCALL_SP_S_result("NTU02D010.25", _maubenhpham_id + "$" + _row.TIEPNHANID);
                    }
                }

                Load_DonCu(_maubenhpham_id, list_id_thuoc);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        private void Load_DonCu(string maubenhpham_id, string list_id_thuoc)
        {
            string[] listName = new string[] { "[0]", "[1]", "[2]" };
            string[] listValue = new string[] { _loainhommaubenhpham_id, Const.local_user.COMPANY_ID, maubenhpham_id };

            string paramsName = "NTU02D010.04";

            if (list_id_thuoc != "")
            {
                //  "params":["NTU02D010.26"],"options":[{"name":"[0]","value":"7"},{"name":"[1]","value":"902"},{"name":"[2]","value":"310218"},{"name":"[3]","value":"281877"},{"name":"[S0]","value":"902"}]}
                paramsName = "NTU02D010.26";
                Array.Resize(ref listName, listName.Length + 1);
                listName[listName.Length - 1] = "[" + (listName.Length - 1) + "]";
                Array.Resize(ref listValue, listValue.Length + 1);
                listValue[listValue.Length - 1] = list_id_thuoc;
            }

            #region Thêm vào grid
            Array.Resize(ref listName, listName.Length + 1);
            listName[listName.Length - 1] = "[S" + (listName.Length - 1) + "]";
            Array.Resize(ref listValue, listValue.Length + 1);
            listValue[listValue.Length - 1] = Const.local_user.COMPANY_ID;

            DataTable dtAddFromDonCu = RequestHTTP.get_ajaxExecuteQueryO(paramsName, listName, listValue);
            for (int k = 0; k < dtAddFromDonCu.Rows.Count; k++)
            {
                // thêm vào grid
                DataRow dr = dataRowAdd.NewRow();

                dr["TEN_THUOC"] = dtAddFromDonCu.Rows[k]["TEN_THUOC"].ToString();
                dr["HOATCHAT"] = dtAddFromDonCu.Rows[k]["HOATCHAT"].ToString();
                dr["DUONG_DUNG"] = dtAddFromDonCu.Rows[k]["DUONG_DUNG"].ToString();
                dr["DON_GIA"] = dtAddFromDonCu.Rows[k]["DON_GIA"].ToString();
                dr["SOLUONG"] = dtAddFromDonCu.Rows[k]["SO_LUONG"].ToString();
                dr["THANH_TIEN"] = dtAddFromDonCu.Rows[k]["THANH_TIEN"].ToString();
                dr["BH_TRA"] = dtAddFromDonCu.Rows[k]["BH_TRA"].ToString();
                dr["ND_TRA"] = dtAddFromDonCu.Rows[k]["ND_TRA"].ToString();
                dr["LOAI_DT_CU"] = dtAddFromDonCu.Columns.Contains("LOAI_DT_CU") ? dtAddFromDonCu.Rows[k]["LOAI_DT_CU"].ToString() : ""; //????
                dr["LOAI_DT_MOI"] = dtAddFromDonCu.Columns.Contains("LOAI_DT_MOI") ? dtAddFromDonCu.Rows[k]["LOAI_DT_MOI"].ToString() : ""; //????
                try
                {
                    string DUONGDUNGE = dtAddFromDonCu.Rows[k]["HUONGDAN_SD"].ToString();// "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0"
                    DUONGDUNGE = DUONGDUNGE.Substring(DUONGDUNGE.IndexOf("@") + 1);
                    DUONGDUNGE = DUONGDUNGE.Substring(DUONGDUNGE.IndexOf("@") + 1);
                    DUONGDUNGE = DUONGDUNGE.Substring(0, DUONGDUNGE.IndexOf("@"));
                    dr["DUONGDUNGE"] = DUONGDUNGE; //5 ngày, Ngày 4 Viên chia 2
                }
                catch (Exception ex)
                {
                    dr["DUONGDUNGE"] = "";
                }
                dr["DONVI_TINH"] = dtAddFromDonCu.Rows[k]["DONVI_TINH"].ToString();
                dr["GIATRANBHYT"] = ""; //???
                dr["NHOM_MABHYT_ID"] = dtAddFromDonCu.Rows[k]["NHOM_MABHYT_ID"].ToString();
                dr["TYLEBHYT_TVT"] = dtAddFromDonCu.Rows[k]["TYLEBHYT_TVT"].ToString();
                dr["THUOCVATTUID"] = dtAddFromDonCu.Rows[k]["THUOCVATTUID"].ToString();
                dr["ID_DT_MOI"] = dtAddFromDonCu.Rows[k]["ID_DT_MOI"].ToString();
                dr["STT"] = "";
                dr["ID_DT_CU"] = dtAddFromDonCu.Columns.Contains("ID_DT_CU") ? dtAddFromDonCu.Rows[k]["ID_DT_CU"].ToString() : _doituongbenhnhanid; //????
                dr["MA_THUOC"] = dtAddFromDonCu.Rows[k]["MA_THUOC"].ToString();
                dr["HUONGDAN_SD"] = dtAddFromDonCu.Rows[k]["HUONGDAN_SD"].ToString();
                dr["DUONGDUNGID"] = dtAddFromDonCu.Rows[k]["DUONGDUNGID"].ToString();
                dr["DICHVUKHAMBENHID"] = "";
                dr["ACTION"] = "";
                dr["MAHOATCHAT"] = dtAddFromDonCu.Rows[k]["MAHOATCHAT"].ToString();
                dr["OLDVALUE"] = dtAddFromDonCu.Rows[k]["OLDVALUE"].ToString();
                dr["KHOANMUCID"] = dtAddFromDonCu.Rows[k]["KHOANMUCID"].ToString();

                if (OPTION != "02D011") dr["KHO_THUOCID"] = dtAddFromDonCu.Rows[k]["KHO_THUOCID"].ToString(); //???? 
                else dr["KHO_THUOCID"] = "";
                dr["DVQD"] = "";
                dr["LIEUDUNG"] = "";
                dr["KETRUNGHOATCHAT"] = dtAddFromDonCu.Columns.Contains("KETRUNGHOATCHAT") ? dtAddFromDonCu.Rows[k]["KETRUNGHOATCHAT"].ToString() : ""; //????

                dataRowAdd.Rows.Add(dr);
            }
            #endregion
        }

        private void btnDiUngThuoc_Click(object sender, EventArgs e)
        {
            NGT02K057_ThuocDiUng frm = new NGT02K057_ThuocDiUng();
            frm.setParam(this.BENHNHANID);
            //frm.setReturnData(ReturnData_NGT02K018_DonThuocMau);
            openForm(frm, "1");

        }
        private void btnPhacDo_Click(object sender, EventArgs e)
        {
            NTU02D075_PhacDoMau frm = new NTU02D075_PhacDoMau();
            Dictionary<string, string> myVar = new Dictionary<string, string>();
            myVar.Add("maChanDoan", ucSearchLookupICD.SelectedValue);
            myVar.Add("loaiDv", "1");
            myVar.Add("khoid", cbo_KhoThuoc.SelectValue);
            frm.initData(myVar);
            frm.raiseEvent(listenFrmPhacDo);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show(this);
        }
        private void listenFrmPhacDo(object sender, EventArgs e)
        {
        }
        private void btnThuocConSD_Click(object sender, EventArgs e)
        {
            NGT02K020_ThuocConSuDung frm = new NGT02K020_ThuocConSuDung();
            frm.setParam(this.BENHNHANID);
            //frm.setReturnData(ReturnData_NGT02K018_DonThuocMau);
            openForm(frm, "1");
        }
        private void sbtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void ckbCapPhieuHenKham_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCapPhieuHenKham.Checked)
            {
                VNPT.HIS.CommonForm.NGT02K008_Thongtin_Lichkham frm = new VNPT.HIS.CommonForm.NGT02K008_Thongtin_Lichkham(
                    this.KHAMBENHID, this.BENHNHANID, _row.HOSOBENHANID, Func.getSysDatetime_Short().AddDays(Func.Parse(txtTG_HENKHAM.Text.Trim()))
                    );
                openForm(frm, "1");
            }
        }

        private void btnCachDung_Click(object sender, EventArgs e)
        {
            int rowId = ucDSThuoc.gridView.FocusedRowHandle;
            VNPT.HIS.CommonForm.NGT02K043_DuongDung frm = new VNPT.HIS.CommonForm.NGT02K043_DuongDung((DataTable)cbo_DuongDung.lookUpEdit.Properties.DataSource
                , rowId
                , dataRowAdd.Rows[rowId]["HUONGDAN_SD"].ToString()
                );

            frm.setReturnData(ReturnData_NGT02K043_DuongDung);
            frm.StartPosition = FormStartPosition.CenterParent;

            frm.ShowDialog();
        }
        private void ReturnData_NGT02K043_DuongDung(object sender, EventArgs e)
        {
            string[] list = (string[])sender;

            int rowId = Func.Parse(list[1]);
            dataRowAdd.Rows[rowId]["HUONGDAN_SD"] = list[0];//e.text
            dataRowAdd.Rows[rowId]["DUONG_DUNG"] = list[2];
            dataRowAdd.Rows[rowId]["DUONGDUNGE"] = list[3];

            ucDSThuoc.gridView.UpdateCurrentRow();
            //ucDSThuoc.setColumnButton("DUONGDUNGE", btnCachDung_Click);
        }

        #endregion

        #region SỰ KIỆN KeyDown
        private void textEditSoNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSO_NGAY.Text.ToString()) || Func.Parse(txtSO_NGAY.Text.ToString()) <= 0)
                {
                    MessageBox.Show("Số ngày, không được để trống hoặc phải lớn hơn 0.");
                    return;
                }
                else txtSANG.Focus();
            }
        }
        private void ucSearchLookupBP_KeyEnter(object sender, EventArgs e)
        {
            //ucXu.Focus();
        }
        private void ucSearchLookupBP_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;

                if (drv["ICD10CODE"].ToString() == ucSearchLookupICD.SelectedValue)
                    ucSearchLookupBP.messageError = "Bệnh phụ vừa nhập không được trùng với bệnh chính.";
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void textEditSang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSANG.Text))
                    txtSANG.EditValue = "0";
                txtTRUA.Focus();
            }
        }

        private void textEditTrua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtTRUA.Text.ToString()))
                    txtTRUA.EditValue = "0";
                txtCHIEU.Focus();
            }
        }

        private void textEditChieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtCHIEU.Text.ToString()))
                    txtCHIEU.EditValue = "0";
                txtTOI.Focus();
            }
        }

        private void textEditToi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtTOI.Text.ToString()))
                    txtTOI.EditValue = "0";
                doInputDrugType("1");
                txtSOLUONG_CHITIET.Focus();
            }
        }

        private void textEditSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSOLUONG_CHITIET.Text.ToString()) || Func.Parse(txtSOLUONG_CHITIET.Text.ToString()) <= 0)
                {
                    MessageBox.Show("Số lượng, không được để trống hoặc phải lớn hơn 0.");
                    return;
                }
                else
                    lookup_CachDung.Focus();
            }
        }

        private void ucSearchLookupCachDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sbtnThem_Click(null, null);
            }
        }


        private void ucSearchLookupThuoc_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void ucSearchLookupThuoc_MouseClick(object sender, MouseEventArgs e)
        {
            if (Func.Parse(cbo_KhoThuoc.SelectValue) <= 0)
            {
                if (OPTION == "02D014" || OPTION == "02D016")
                {
                    MessageBox.Show("Hãy chọn đơn thuốc/Vật tư cần trả");
                    cbo_KhoThuoc.Focus();
                }
                if (OPTION == "02D010" || OPTION == "02D015")
                {
                    MessageBox.Show("Hãy chọn kho thuốc/vật tư");
                    cbo_KhoThuoc.Focus();
                }
            }
        }

        private void txtGHICHU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_row.SUDUNG_LIEUDUNG == "1" && (this.OPTION == "02D010" || this.OPTION == "02D017"))
                    txtSoLuongTrenLan.Focus();
                else btnThemThuoc.Focus();
            }
        }
        private void txtSoLuongTrenLan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtSoLanTrenNgay.Focus();
        }
        private void txtSoLanTrenNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtLieuDung.Focus();
        }

        private void txtSoLanTrenNgay_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView drvThuoc = lookup_Thuoc.SelectDataRowView;
            txtLieuDung.Text = txtSoLuongTrenLan.Text + " "
                + (drvThuoc == null ? "" : drvThuoc["TEN_DVT"].ToString())
                + "/Lần * " + txtSoLanTrenNgay.Text + " lần/Ngày";
        }
        private void txtSoLuongTrenLan_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView drvThuoc = lookup_Thuoc.SelectDataRowView;
            txtLieuDung.Text = txtSoLuongTrenLan.Text + " "
                + (drvThuoc == null ? "" : drvThuoc["TEN_DVT"].ToString())
                + "/Lần * " + txtSoLanTrenNgay.Text + " lần/Ngày";
        }


        #endregion


        private void _inDonThuoc(string ret_maubenhpham_id)
        {
            string _type = Func.get_HIS_FILEEXPORT_TYPE();

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");      
            table.Columns.Add("value");
            table.Rows.Add("maubenhphamid", "String", ret_maubenhpham_id); 

            //string rpName = "NGT020_TODIEUTRI_39BV01_QD4069_A4_" + RequestHTTP.getSysDatetime() + "." + "rtf";
                       
            if (Const.local_user.HOSPITAL_ID == "913")
            {
                Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "NGT006_DONTHUOC_17DBV01_TT052016_A5_913"); // openReport('window', "NGT006_DONTHUOC_17DBV01_TT052016_A5_913", "pdf", par);
                openForm(frm);
            }
            else
            {
                DataTable arr_loaithuoc = RequestHTTP.call_ajaxCALL_SP_O("NTU02D033_LOAITHUOC", ret_maubenhpham_id, 0);
                string _loaithuoc = "0";
                if (arr_loaithuoc.Rows.Count > 0)
                {
                    if (arr_loaithuoc.Rows[0]["LOAI"].ToString() == "3")
                    { 
                    }
                    string dc_phong = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "PHONG_TUDONG_IN");
                    dc_phong = "," + dc_phong + ",";
                    bool phong_tu_dong_in = dc_phong.IndexOf("," + Const.local_phongId + ",") >= 0;
                    //string[] dc_phongs = dc_phong.Split(',');
                    for (int i = 0; i < arr_loaithuoc.Rows.Count; i++)
                    {
                        _loaithuoc = arr_loaithuoc.Rows[i]["LOAI"].ToString();
                        string code = "";
                        if (_loaithuoc == "3") code = "NGT020_DONTHUOCTHANGNGOAITRU"; //thuoc dong y --DONTHUOCTHANG_NGOAITRU//var rpName = "VNPTHIS_IN_A5_DONTHUOC_";
                        else if (_loaithuoc == "6") code = "NGT013_DONTHUOCHUONGTHAN_TT052016_A5"; //thuoc huong than  //  var rpName = "VNPTHIS_IN_A5_DONTHUOC_"; 
                        else if (_loaithuoc == "7") code = "NGT013_DONTHUOCGAYNGHIEN_TT052016_A5"; //don thuoc gay nghien var rpName = "VNPTHIS_IN_A5_DONTHUOC_";
                        else // don thuoc khac
                        {
                            if (Const.local_user.HOSPITAL_ID == "944")
                            {
                                if (_row.DOITUONGBENHNHANID != "1") code = "NGT006_DONTHUOC1L_17DBV01_TT052016_A5_944";
                                else code = "NGT006_DONTHUOC_17DBV01_TT052016_A5";
                            }
                            else code = "NGT006_DONTHUOC_17DBV01_TT052016_A5";
                        }

                        if (_row.TUDONGIN == "1" || phong_tu_dong_in)
                        {
                            Func.Print_Luon(table, code);
                            // dùng cho cách in: down file với tên đúng định dạng về thư mục để 1 app khác chuyển in tự động
                            //var rpName = "VNPTHIS_IN_A5_DONTHUOC_";
                            //rpName += $('#lblPATIENTCODE').val();
                            //rpName += "_" + (jsonrpc.AjaxJson.getSystemDate('DDMMYY-HH24MISS'));
                            //rpName += "." + _type; 
                        }
                        else
                        {
                            openForm(new Controls.SubForm.frmPrint(table, code));
                        }
                    }

                    if (Const.local_user.HOSPITAL_ID == "902" 
                        && ckbCapPhieuHenKham.Checked
                        )
                    {
                        DataTable table2 = new DataTable();
                        table2.Columns.Add("name");
                        table2.Columns.Add("type");
                        table2.Columns.Add("value");
                        table2.Rows.Add("khambenhid", "String", this,KHAMBENHID); 
                        if (_row.TUDONGIN == "1")
                            Func.Print_Luon(table, "NGT014_GIAYHENKHAMLAI_TT402015_A4"); // var rpName = "VNPTHIS_IN_A4_DONTHUOC_";
                        else
                            openForm(new Controls.SubForm.frmPrint(table, "NGT014_GIAYHENKHAMLAI_TT402015_A4"));
                    }
                }
            }
        }

        private void openForm(Form frm, string optionsPopup="1")
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }
        private void childFormClosing(object sender, EventArgs e)
        {
            Enabled = true;
        }

        private void btnXuTri_Click(object sender, EventArgs e)
        {
            try
            {
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default != null) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    this.Close();

                    NGT02K005_PhieuKhamBenh frm = new NGT02K005_PhieuKhamBenh();
                    frm.loadData(Const.drvBenhNhan, Const.drvBenhNhan_ChiTiet);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        // Hàm gửi sự kiện đến form cha
        protected EventHandler event_ListenFrm_KetQua_Thuoc_ChiDinhDV;
        public void setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(EventHandler eventChangeValue)
        {
            event_ListenFrm_KetQua_Thuoc_ChiDinhDV = eventChangeValue;
        }

        private void lookup_CachDung_Leave(object sender, EventArgs e)
        {
            doInputDrugType("1");
        }
    }
}