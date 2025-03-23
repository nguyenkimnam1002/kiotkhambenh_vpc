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
using Newtonsoft.Json;

namespace VNPT.HIS.Duoc
{
    public partial class DUC01S002_PhieuYeuCau : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public DUC01S002_PhieuYeuCau()
        {
            InitializeComponent();
        }
        private string _btnDuyet = "Duyệt";
        private string _btnNhapKho = "Nhập kho";

        private int TAOMOI = 1;
        private int CHONHAP = 2;
        private int DAHUY = 3;
        private int KETTHUC = 4;
        private int CHODUYET = 5;
        private int DADUYET = 6;
        private int KHONGDUYET = 7;
        private int YEUCAU = 8;
        private int XULY = 9;
        private int DADUYETYC = 10;
        private int TUCHOIYC = 11;
        private int DAXUAT = 12;
        private int DANHAP = 13;
        private int DATAOPHIEU = 14;
        // tuan them de check hien thi man hinh du tru tren nguyen trai
        private string PHARMA_GIAODIEN_NHAP_NCC = "0";
        private int PHARMA_BC_NHAP_KHO = 0;
        private int PHARMA_BC_XUAT_KHO = 0;

        string PHARMA_KETNOI_CONG_BYT = "";
        string canhBao = "";
        string canhBaoloaithuoc = "";


        string lbPara = "#";
        private string lk;
        private string ht;
        private string tt;
        private string lp;
        private string gd;
        private string td;
        private string type;
        private string title;
        private string cs;
        // VNPT.HIS.Duoc.DUC01S002_PhieuYeuCau?lk=7&lp=2,3&ht=12&tt=5,6,7&type=45&gd=VATTU
        //../duoc/DUC01S002_PhieuYeuCau&lk=2,6,5,12&lp=2,3&ht=13&tt=5,6,7&type=45&cs=1&gd=THUOC&ssid=


        private void DUC01S002_PhieuYeuCau_Load(object sender, EventArgs e)
        {
            int kk = Const.local_khoaId;
            lk = getPara("lk");
            ht = getPara("ht");
            tt = getPara("tt");
            lp = getPara("lp");
            gd = getPara("gd");
            td = getPara("td");
            type = getPara("type");
            cs = getPara("cs");

            InitControl();
            Button_Locate();
            _bindEvent();
            GetThamSoKhoiTao();

            _loadDSPhieu(1, null);
            checkRolePhieuIn();

            ////hard code tự xử lý thay đổi tiêu đề góc phải
            //var paramLK = getPara("lk");
            //var paramGD = getPara("gd");

            //if(paramLK == "2" && paramGD == "THUOC") {
            //    lblTitle.Text = "Duyệt phiếu yêu cầu thuốc đơn thuốc bệnh nhân";
            //}
            //else if (paramLK == "7" && paramGD == "VATTU")
            //{
            //    lblTitle.Text = "Duyệt phiếu yêu cầu thuốc phiếu lĩnh thuốc nội trú";
            //}
            //else if (paramLK == "3" && paramGD == "THUOC")
            //{
            //    lblTitle.Text = "Duyệt phiếu yêu cầu vật tư đơn thuốc bệnh nhân ";
            //}
            //else if (paramLK == "7" && paramGD == "VATTU")
            //{
            //    lblTitle.Text = "Duyệt phiếu yêu cầu vật tư phiếu lĩnh thuốc nội trú";
            //}

            grdPhieu.setEvent(_loadDSPhieu);
            grdPhieu.SetReLoadWhenFilter(true);
            grdPhieu.gridView.OptionsView.ColumnAutoWidth = false;
            //ucGrid_DsBenhNhan.setEvent_FocusedRowChanged(viewDetail);
            //2 hàm dưới để load grid ra, mặc định chọn view chi tiết vào row đầu tiên.
            grdPhieu.gridView.OptionsBehavior.Editable = false;
            //grdPhieu.gridView.Click += clickrow;
            grdPhieu.setEvent_FocusedRowChanged(_loadChiTiet);

            grdThuoc.setEvent(_loadDSThuoc);
            grdThuoc.gridView.OptionsView.ColumnAutoWidth = false;
        }

        #region INIT CONTROL
        private void initUcCboKho()
        {
            try
            {
                // BIẾN LT KO CÓ
                //if (jQuery.type(that.opt.lt) === "undefined")
                //{
                //    var sql_par = RSUtil.buildParam("",[that.opt.lk]);
                //    ComboUtil.getComboTag("cboKho", "DUC01S002.DSKHO", sql_par, "", "", "sql", "", false);
                //}
                //else
                //{
                //    var sql_par = that.opt.lk + '$' + that.opt.lt + '$';
                //    ComboUtil.getComboTag("cboKho", "DUC01S002.DSKHO5",
                //            sql_par, "", "", "sp", '', function(){ });
                //}

                DataTable dt = RequestHTTP.get_ajaxExecuteQuery("DUC01S002.DSKHO", new string[] { "[0]" }, new string[] { lk });

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                cboKho.setData(dt, 0, 1);
                cboKho.setColumnAll(false);
                cboKho.setColumn(1, true);
                cboKho.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void initUcCboHinhThuc()
        {
            try
            {
                //sql_par = RSUtil.buildParam("",[that.opt.ht]);
                //if (!that.opt.ht.includes(","))
                //    ComboUtil.getComboTag("cboHinhThuc", "DUC01S002.HINHTHUC", sql_par, "", "", "sql", "", false);
                //else
                //    ComboUtil.getComboTag("cboHinhThuc", "DUC01S002.HINHTHUC", sql_par, "",{ value: that.opt.ht,text: '--Toàn bộ--'},"sql","",false);
                //sql_par = RSUtil.buildParam("",[that.opt.tt]);

                DataTable dt = RequestHTTP.get_ajaxExecuteQuery("DUC01S002.HINHTHUC", new string[] { "[0]" }, new string[] { ht });
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });

                if (ht.Contains(","))
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ht;
                    dr[1] = "--Toàn bộ--";
                    dt.Rows.InsertAt(dr, 0);
                }

                cboHinhThuc.setData(dt, 0, 1);
                cboHinhThuc.setColumnAll(false);
                cboHinhThuc.setColumn(1, true);
                cboHinhThuc.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void initUcCboTrangThai()
        {
            try
            {
                //if (!that.opt.tt.includes(","))
                //    ComboUtil.getComboTag("cboTrangThaiDuyet", "DUC01S002.TRANGTHAI", sql_par, "", "", "sql", "", false);
                //else
                //    ComboUtil.getComboTag("cboTrangThaiDuyet", "DUC01S002.TRANGTHAI", sql_par, "",{ value: that.opt.tt,text: '--Toàn bộ--'},"sql","",false);


                DataTable dt = RequestHTTP.get_ajaxExecuteQuery("DUC01S002.TRANGTHAI", new string[] { "[0]" }, new string[] { tt });
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });

                if (tt.Contains(","))
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = tt;
                    dr[1] = "--Toàn bộ--";
                    dt.Rows.InsertAt(dr, 0);
                }
                cboTrangThaiDuyet.setData(dt, 0, 1);
                cboTrangThaiDuyet.setColumnAll(false);
                cboTrangThaiDuyet.setColumn(1, true);
                cboTrangThaiDuyet.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void initUcCboLoaiPhieu()
        {
            try
            {
                DataTable dt = RequestHTTP.get_ajaxExecuteQuery("DUC01S002.LOAIPHIEU", new string[] { "[0]", "[1]" }, new string[] { lp, ht });
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });

                //              if (!lp.Contains(","))
                //	$("#cboLoaiPhieu option[value="0,1,2,3"]").remove();
                //else
                //	$("#cboLoaiPhieu option[value="0,1,2,3"]").val(lp);
                if (lp.Contains(","))
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = lp;
                    dr[1] = "--Toàn bộ--";
                    dt.Rows.InsertAt(dr, 0);
                }
                cboLoaiPhieu.setData(dt, 0, 1);
                cboLoaiPhieu.setColumnAll(false);
                cboLoaiPhieu.setColumn(1, true);
                cboLoaiPhieu.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void initMenuInPhieu()
        {
            tbDuyet.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbTaoPhieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbYCNhap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            btnNhapKho.Visible = false;
            tbYCXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnXuatTra.Visible = false;

            tbNhapKhoNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //btnNhapKhoNCC.Visible = false;
            tbXuatTraNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbXuatTraNCC_TK.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbYCNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSuaNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbYCXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSuaXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbYCXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSuaXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbYCXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSuaXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            tbYCXuatDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbXuatDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbXuatYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbHTraYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            tbSuaDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSuaYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            tbHoanTra.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // ban thuoc
            tbBanThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbSuaPhieuBanTHuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbTraThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            tbDuyetHoanTra.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbInPXThuocGayNghienHT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnInPhieu.Visible = false;

            tbInPhieuLinhThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbInPhieuDuTru.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbInPLThuocGayNghienHT1Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            tbInPhieuLinhVatTu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbphieutrathuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            print_11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tbInPhieuDuTru.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            if (this.type.Contains("0"))
            {
                btnNhapKho.Visible = true;
                tbYCXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("1"))
            {
                btnXuatTra.Visible = true;
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("2"))
            {
                tbYCNhap.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("3"))
            {
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("4") || this.td == "1")
            {
                tbDuyet.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("5"))
            {
            }
            if (this.type.Contains("6"))
            {
                tbNhapKhoNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                //btnNhapKhoNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }


            if (this.type.Contains("7"))
            {
                tbXuatTraNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbXuatTraNCC_TK.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("8"))
            {
                tbNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("9"))
            {
                tbYCNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("A"))
            {
                tbYCXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("B"))
            {
                tbSuaXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("C"))
            {
                tbYCXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("D"))
            {
                tbSuaXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("E"))
            {
                tbHoanTra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("I"))
            {
                tbYCXuatDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbXuatDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("F"))
            {
                tbDuyetHoanTra.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("G"))
            {
                tbBanThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaPhieuBanTHuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbTraThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;


            }
            if (this.type.Contains("H"))
            {
                tbYCXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("J"))
            {
                tbSuaXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if (this.type.Contains("K"))
            {
                tbSuaYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbXuatYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbHTraYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            if (this.ht == "2")
            {
                //btnDaThanhToan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
                btnGoNhapkho.Visible = true;

            }
            if ((this.ht == "2" || this.ht == "9") && PHARMA_GIAODIEN_NHAP_NCC == "DUC02N001_NhapThuocNCC_BVNT")
            {
                tbInPhieuLinhThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInPhieuLinhVatTu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInPLThuocGayNghienHT1Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            if (this.ht == "2" || this.ht == "9")
            {
                tbInPhieuDuTru.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInPXThuocGayNghienHT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            }
            else
            {
                //btnDaThanhToan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnGoNhapkho.Visible = false;
            }
            if (this.ht == "1")
            {
                //btnDaThanhToan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
                btnGoNhapkho.Visible = true;

            }

            // cau hinh phieu in
            //1. Phieu in nha cung cap. lk=1&lp=0,1&tt=1,4&ht=1&type=67&gd=THUOC
            //		if(this.type=="67"&&this.ht=="1"){
            //			tbIn.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
            //		}
            //2. phieu linh thuoc va phieu linh gay nghien huong than, phieu tra thuoc.lp=2,3&ht=12&tt=5,6,7&type=45&gd=THUOC
            if (this.type == "45" && this.lp == "2,3" && this.gd == "THUOC"
                && (this.ht == "12" || this.ht == "13"
                ))
            {
                print_1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                tbInPhieu2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInPhieuLinh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                //tbInPhieuLinhHoaChat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 

                tbInPLinhThuocNGT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInPhieuLinhTVTHaoPhi2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInLinhTVTBoSungCoSoTuTruc2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInHoanTraCoSoTuTruc2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInNhapXuatThuocTuNhaThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; //20180906
                tbInBienBanTraThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; //20180927 
            }
            if (this.ht == "12")
            {
                print_11.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            if ((this.lp == "2,3" && (this.ht == "2" || this.ht == "9") && this.type == "0123E" && this.gd == "THUOC")
                || ((this.lp == "2,3" || this.lp == "3") && (this.ht == "2" || this.ht == "9" || this.ht == "12") && this.type == "45" && this.gd == "THUOC")
                )
            {
                print_3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                print_10.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                // $("#toolbartbInPhieuDuTru.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
                tbInPXThuocGayNghienHT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInPhieu2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInPhieuLinh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                //tbInPhieuLinhHoaChat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
                tbInPhieuLinhTVTHaoPhi2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInLinhTVTBoSungCoSoTuTruc2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInHoanTraCoSoTuTruc2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; //20180906
                tbInNhapXuatThuocTuNhaThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbInBienBanTraThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; //20180927
                tbInXuatThuocTraKhoChinh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; //20181008
            }
            if (this.gd == "VATTU")
            {
                //tbInPhieuVatTu2Lien.Visibility = DevExpress.XtraBars.BarItemVisibility.Always; 
            }

            //Phieu Biên bản thanh lý thuốc và hóa chất vật tư
            //Biên bản xác nhận thuốc/hóa chất/vật tư y tế mất/hỏng/vỡ
            //lp=3&ht=7&type=AB&gd=XUATHUYTHUOC
            //lp=3&ht=7&type=AB&gd=XUATHUYVATTU
            //lp=2,3&ht=5,6,7&type=4&gd=THUOC
            //lp=2,3&ht=5,6,7&type=4&gd=VATTU
            if ((this.lp == "3" && this.ht == "7" && this.type == "AB" && (this.gd == "XUATHUHAOCB" || this.gd == "XUATHUYTHUOC" || this.gd == "XUATHUYVATTU"))
                    || (this.lp == "2,3" && this.ht == "5,6,7" && this.type == "4" && (this.gd == "THUOC" || this.gd == "VATTU"))
                )
            {
                print_4.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                print_5.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            //phieu tra thuoc benh nhan.
            if (this.gd == "THUOC" && this.type == "4" && this.ht == "13")
            {
                print_6.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            //Phieu tra vat tu
            if (((this.ht == "2" || this.ht == "9") && this.gd == "VATTU")
                || ((this.ht == "13" || this.ht == "2") && this.gd == "VATTU" && this.type == "45"
                    ) || (this.ht == "9" && this.type == "45"))
            {
                print_7.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            // Ban Thuoc cho khach le
            if (this.type.Contains("G") && this.ht.Contains("15"))
            {
                print_8.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbphieutrathuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

            }
            if (this.gd == "XUATHUHAOCB")
            {
                tbYCXuatHuy.Caption = "YC Hư Hao CB";
            }

        }

        public void InitControl()
        {
            try
            {
                canhBao = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "PHARMA_CANHBAO_DUYET_LOAITHUOC");
                PHARMA_KETNOI_CONG_BYT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "PHARMA_KETNOI_CONG_BYT");
                PHARMA_GIAODIEN_NHAP_NCC = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "PHARMA_GIAODIEN_NHAP_NCC");
                PHARMA_BC_NHAP_KHO = Func.Parse(RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH.THEOMA", "PHARMA_BC_NHAP_KHO"));
                PHARMA_BC_XUAT_KHO = Func.Parse(RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH.THEOMA", "PHARMA_BC_XUAT_KHO"));


                string AnHien = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_HIENTHI_GOIKHAM_THUOC");
                if (AnHien == "1" && ht == "13")
                    layoutControlItem_GoiSTT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                else
                    layoutControlItem_GoiSTT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                string PHARMA_O_TIMKIEMKHO = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "PHARMA_O_TIMKIEMKHO");
                if (PHARMA_O_TIMKIEMKHO == "1")
                {
                    layoutControlItem_txtTimKho.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }

                txtNgayBD.DateTime = Func.getSysDatetime();
                txtNgayKT.DateTime = Func.getSysDatetime();

                var tungay = RequestHTTP.call_ajaxCALL_SP_S_result("DUC01S002_GETTUNGAY", "");
                if (tungay != null && tungay != "undefined") txtNgayBD.DateTime = Func.ParseDate(tungay);

                initUcCboKho();
                initUcCboHinhThuc();
                initUcCboTrangThai();
                initUcCboLoaiPhieu();


                //header + title
                var _header = "";
                if (type.Contains("4"))
                {
                    _header = _header + "Duyệt phiếu";
                }
                else
                {
                    _header = _header + "Danh sách phiếu";
                }

                if (lp.Contains("2") || lp.Contains("3"))
                {
                    _header = _header + " yêu cầu";
                }
                else
                {
                    if (lp.Contains("0"))
                    {
                        _header = _header + " nhập";
                    }
                    if (lp.Contains("1"))
                    {
                        _header = _header + " xuất";
                    }
                }

                title = "";
                if (gd == "THUOC" || gd == "NHAPBUTHUOC" || gd == "XUATHUYTHUOC" || gd == "XUATTHIEUTHUOC")
                {
                    title = " thuốc";
                }
                else if (gd == "VATTU" || gd == "NHAPBUVATTU" || gd == "XUATHUYVATTU" || gd == "XUATTHIEUVATTU")
                {
                    title = " vật tư";
                }
                _header = _header + title;
                if (!ht.Contains(","))
                {
                    _header = _header + " " + cboHinhThuc.SelectText.ToLower();
                }
                if (td == "1")
                {
                    _header = "Xác nhận giao/nhận theo phiếu lĩnh";
                    _btnDuyet = "Xác nhận";

                }

                string _btnText = "Dự trù";
                if (ht == "9") _btnText = "Bổ sung";
                tbYCNhap.Caption = _btnText;

                initMenuInPhieu();


                #region Cấu hình hiển thị grid 
                //              var _gridHeader;
                //              if (that.opt.ht.includes("13"))
                //              {
                //                  var _par = ['PHARMA_COL_PHIEUYC_BN'];
                //                  var result = jsonrpc.AjaxJson.ajaxCALL_SP_S("COM.CAUHINH",
                //                          _par.join('$'));
                //                  if (result != '0' && result != null)
                //                      _gridHeader = result;
                //                  else
                //                      _gridHeader = ",icon,30,0,ns,l;nhapxuatid,NHAPXUATID,0,0,t,l;" +
                //                              "trangthaiid,TRANGTHAIID,0,0,t,l;" +
                //                              "hinhthucid,HINHTHUCID,0,0,t,l;" +
                //                              "Phiếu,MA,60,0,f,l;" +
                //                              "Loại,TEN_KIEU,60,0,f,l;" +
                //                              "Đơn thuốc/VT,SOPHIEU,90,0,f,l;" +
                //                              "Mã BN,MABENHNHAN,85,0,f,l;" +
                //                              "Họ tên,TENBENHNHAN,150,0,f,l;" +
                //                              "Ngày sinh,NGAYSINH,70,0,f,c;" +
                //                              "GT,GIOITINH,40,0,f,l;" +
                //                              "Khoa,TENKHO,110,0,f,l;" +
                //                              "Trạng thái,TRANGTHAI,80,0,f,l;" +
                //                              "Ngày NX,NGAYNX,90,0,f,c;" +
                //                              "Số CT,SOCHUNGTU,50,0,f,l;" +
                //                              "kieu,KIEU,0,0,t,l;" +
                //                              "doiungid,DOIUNGID,0,0,t,l;" +
                //                              "nhapid,NHAPID,0,0,t,l;" +
                //                              "xuatid,XUATID,0,0,t,l;" +
                //                              "MAUBENHPHAMID,MAUBENHPHAMID,0,0,t,l;" +
                //                              "ttphieunx,TTPHIEUNX,0,0,t,l";
                //                  GridUtil.init("grdPhieu", "100%", "370px", "Danh sách phiếu", false, _gridHeader, false);
                //                  GridUtil.addExcelButton("grdPhieu", 'Xuất excel', true);
                //              }
                //              else if (that.opt.ht.includes("12"))
                //              {
                //                  var _par = ['PHARMA_COL_PHIEUYC'];
                //                  var result = jsonrpc.AjaxJson.ajaxCALL_SP_S("COM.CAUHINH",
                //                          _par.join('$'));
                //                  if (result != '0' && result != null)
                //                      _gridHeader = result;
                //                  else

                //                      _gridHeader = " ,icon,30,0,ns,l;nhapxuatid,NHAPXUATID,0,0,t,l;" +
                //                              "trangthaiid,TRANGTHAIID,0,0,t,l;" +
                //                              "hinhthucid,HINHTHUCID,0,0,t,l;" +
                //                              "Số CT,SOCHUNGTU,50,0,f,l;" +
                //                              "Phiếu,MA,110,0,f,l;" +
                //                              "Loại,TEN_KIEU,80,0,f,l;" +
                //                              "Đối tượng NX,TENKHO,130,0,f,l;" +
                //                              "Trạng thái,TRANGTHAI,90,0,f,l;" +
                //                              "Ngày NX,NGAYNX,110,0,f,c;" +
                //                              "kieu,KIEU,0,0,t,l;" +
                //                              "doiungid,DOIUNGID,0,0,t,l;" +
                //                              "nhapid,NHAPID,0,0,t,l;" +
                //                              "xuatid,XUATID,0,0,t,l;" +
                //                              "ttphieunx,TTPHIEUNX,0,0,t,l";
                //                  GridUtil.init("grdPhieu", "100%", "370px", "Danh sách phiếu", false, _gridHeader, false);
                //                  GridUtil.addExcelButton("grdPhieu", 'Xuất excel', true);
                //              }
                //              else if (that.opt.ht.includes("9"))
                //              {
                //                  var _par = ['PHARMA_COL_PHIEUTUTRUC'];
                //                  var result = jsonrpc.AjaxJson.ajaxCALL_SP_S("COM.CAUHINH",
                //                          _par.join('$'));
                //                  if (result != '0' && result != null)
                //                      _gridHeader = result;
                //                  else

                //                      _gridHeader = " ,icon,30,0,ns,l;nhapxuatid,NHAPXUATID,0,0,t,l;" +
                //                             "trangthaiid,TRANGTHAIID,0,0,t,l;" +
                //                             "hinhthucid,HINHTHUCID,0,0,t,l;" +
                //                             "Số CT,SOCHUNGTU,50,0,t,l;" +
                //                             "Phiếu,MA,90,0,f,l;" +
                //                             "Loại,TEN_KIEU,80,0,f,l;" +
                //                             "Đối tượng NX,TENKHO,130,0,f,l;" +
                //                             "Trạng thái,TRANGTHAI,90,0,f,l;" +
                //                             "Ngày NX,NGAYNX,110,0,f,c;" +
                //                             "kieu,KIEU,0,0,t,l;" +
                //                             "doiungid,DOIUNGID,0,0,t,l;" +
                //                             "nhapid,NHAPID,0,0,t,l;" +
                //                             "xuatid,XUATID,0,0,t,l;" +
                //                             "ttphieunx,TTPHIEUNX,0,0,t,l;" +
                //                             "Người duyệt,NGUOIDUYET,100,0,f,l";
                //                  GridUtil.init("grdPhieu", "100%", "370px", "Danh sách phiếu", false, _gridHeader, false);
                //                  GridUtil.addExcelButton("grdPhieu", 'Xuất excel', true);
                //              }
                //              else
                //              {
                //                  //duyet (hien thi ten nguoi duyet)
                //                  _gridHeader = " ,icon,30,0,ns,l;nhapxuatid,NHAPXUATID,0,0,t,l;" +
                //                      "trangthaiid,TRANGTHAIID,0,0,t,l;" +
                //                      "hinhthucid,HINHTHUCID,0,0,t,l;" +
                //                      "Số CT,SOCHUNGTU,50,0,f,l;" +
                //                      "Phiếu,MA,110,0,f,l;" +
                //                      "Loại,TEN_KIEU,80,0,f,l;" +
                //                      "Đối tượng NX,TENKHO,130,0,f,l;" +
                //                      "Trạng thái,TRANGTHAI,90,0,f,l;" +
                //                      "Ngày NX,NGAYNX,110,0,f,c;" +
                //                      "kieu,KIEU,0,0,t,l;" +
                //                      "doiungid,DOIUNGID,0,0,t,l;" +
                //                      "nhapid,NHAPID,0,0,t,l;" +
                //                      "xuatid,XUATID,0,0,t,l;" +
                //                      "ttphieunx,TTPHIEUNX,0,0,t,l;" +
                //                      "Người duyệt,NGUOIDUYET,100,0,f,l";

                //                  GridUtil.init("grdPhieu", "100%", "370px", "Danh sách phiếu", false, _gridHeader, false);
                //                  GridUtil.addExcelButton("grdPhieu", 'Xuất excel', true);

                //              } 
                //$('#gs_icon').hide();
                //$(".clearsearchclass:first").html("");
                //              var Ancot = 'f'; // f: ko an cot, t là ẩn cột
                //              if (checkAnGia(that.opt.ht) == 1)
                //              {
                //                  Ancot = 't';
                //	$('[name=divGia]').hide();
                //              };
                //              _gridHeader = "nhapxuatctid,NHAPXUATCTID,0,0,t,l;" +
                //                      "Mã" + that.opt.title + ",MA,60,0,f,l;" +
                //                      "Tên" + that.opt.title + ",TEN,150,0,f,l;" +
                //                      "Hàm lượng,LIEULUONG,60,0,f,l;" +
                //                      "Đơn vị,TEN_DVT,60,0,f,l;" +
                //                      "SL yêu cầu,SOLUONG,60,number!3,f,r;" +
                //                      "SL duyệt,SOLUONGDUYET,60,number!3,f,r;" +
                //                      "Đơn giá,GIANHAP,80,number!3," + Ancot + ",r;" +
                //                      "VAT,XUATVAT,80,number!3," + Ancot + ",r;" +
                //                      "Thành tiền,THANHTIEN,80,number!3," + Ancot + ",r;" +
                //                      "Số lô,SOLO,60,0,f,c;" +
                //                      "HSD,HANSUDUNG,60,0,f,c;" +
                //                      "BSDuyet,CHOLANHDAODUYET,60,0,f,r;" +
                //                      "CHUY,CHUY,60,0,f,r;" +
                //                      "LOAI,LOAI,60,0,t,l;" +
                //                      "TENLOAI,TENLOAI,60,0,t,l;" +
                //                      "TenPhongLuu,TENPHONGLUU,0,0,t,l";
                //              var _group ={
                //              groupField : ['TENPHONGLUU'],
                //		groupColumnShow : [false],
                //		groupText : ['<b>{0} ({1} khoản)</b>']
                //  };
                //  GridUtil.initGroup("grdThuoc","100%","140px","",false,_group,_gridHeader,false,"");
                #endregion

                #region init ẩn hiện các menu

                //              $('#search_grdPhieu').hide();
                //$('#search_grdThuoc').hide();

                //$("#toolbarIdtbDuyet").prop("disabled", true); 
                //$("#toolbarIdtbInNhap").prop("disabled", true);
                //$("#toolbarIdtbInNhapKT").prop("disabled", true);
                //$("#toolbarIdtbBBKiemNhapHoaDon").prop("disabled", true);
                //$("#toolbarIdtbInXuat").prop("disabled", true);
                //$("#toolbarIdtbGiayTT").prop("disabled", true);	

                //$("#btnNhapKho").prop("disabled", true);-Enabled
                //$("#toolbarIdtbYCXuat").prop("disabled", true);
                //$("#toolbarIdtbSua").prop("disabled", true);

                //$("#btnXuatTra").prop("disabled", true);Enabled
                //$("#toolbarIdtbXuatTraNCC").prop("disabled", true);

                //$("#toolbarIdtbSuaNCC").prop("disabled", true);
                //$("#toolbarIdtbNhapBu").prop("disabled", true);
                //$("#toolbarIdtbSuaNhapBu").prop("disabled", true);
                //$("#toolbarIdtbXuatHuy").prop("disabled", true);
                //$("#toolbarIdtbSuaXuatHuy").prop("disabled", true);
                //$("#toolbarIdtbXuatKhac").prop("disabled", true);
                //$("#toolbarIdtbSuaXuatKhac").prop("disabled", true);
                //$("#toolbarIdtbXuatThieu").prop("disabled", true);
                //$("#toolbarIdtbSuaXuatThieu").prop("disabled", true); 

                //$("#toolbarIdtbXuatDTTH").prop("disabled", true);

                //$("#toolbarIdtbSuaDTTH").prop("disabled", true);
                //$("#toolbarIdtbSuaYLenhLT").prop("disabled", true);

                //                  tbDuyet.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbDuyet.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                tbInNhap.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbInNhapKT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbBBKiemNhapHoaDon.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbInXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbGiayTT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                btnNhapKho.Visible = false;
                tbYCXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                btnXuatTra.Visible = false;
                tbXuatTraNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                tbXuatDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                tbSuaDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                btnGoDuyet.Enabled = false;
                btnHuyGuiPhieu.Visible = false;
                #endregion
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void _bindEvent()
        {
            // vẽ dòng màu đỏ
            //        var rowCount = $("#grdThuoc").getGridParam("reccount");
            //        if (rowCount > 0)
            //        {
            //            checkLanhDaoDuyet();
            //            var ids = $("#grdThuoc").getDataIDs();
            //            for (var i = 0; i < ids.length; i++)
            //            {
            //                var id = ids[i];
            //                var row = $("#grdThuoc").jqGrid(
            //                        'getRowData', id);
            //                rowData = $('#grdThuoc').jqGrid(
            //                        'getRowData', ids[i]);
            //                if (parseFloat(rowData['SOLUONG']) != parseFloat(rowData['SOLUONGDUYET']))
            //                { 
            //				$("#grdThuoc").jqGrid('setRowData', ids[i], "", { color: 'red' });
            //        }

            cboKho.setEvent(_loadDSPhieu);
            cboHinhThuc.setEvent(_loadDSPhieu);
            cboTrangThaiDuyet.setEvent(_loadDSPhieu);
            cboLoaiPhieu.setEvent(_loadDSPhieu);


        }

        #endregion


        #region LOAD DỮ LIỆU
        private void _loadDSPhieu(object sender, EventArgs e)
        {
            //var sql_par = RSUtil.buildParam("",[$("#cboKho").val(),$("#cboHinhThuc").val(),$("#cboTrangThaiDuyet").val(),$("#cboLoaiPhieu").val(),$('#txtNgayBD').val().trim(),$('#txtNgayKT').val().trim(), this.type]);
            // postData: {"func":"ajaxExecuteQueryPaging","uuid":"8018d3c2-5264-494b-be27-35f5f7ae9799",
            // "params":["DUC01S002.LAYDL"],"options":[{"name":"[0]","value":"873"},{"name":"[1]","value":"13"},
            // { "name":"[2]","value":"5,6,7"},{"name":"[3]","value":"2,3"},{"name":"[4]","value":"12/11/2018"},
            // { "name":"[5]","value":"12/11/2018"},{"name":"[6]","value":"45"}]}
            int page = (int)sender;
            if (page > 0)
            {
                ResponsList responses = new ResponsList();
                //sql_par=RSUtil.buildParam("",[$("#cboKho").val(),$("#cboHinhThuc").val(),$("#cboTrangThaiDuyet").val(),$("#cboLoaiPhieu").val(),$('#txtNgayBD').val().trim(),$('#txtNgayKT').val().trim(),this.type]);
                responses = RequestHTTP.get_ajaxExecuteQueryPaging("DUC01S002.LAYDL", page, grdPhieu.getNumberPerPage(),
                       new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]" },
                       new string[] { cboKho.SelectValue, cboHinhThuc.SelectValue, cboTrangThaiDuyet.SelectValue, cboLoaiPhieu.SelectValue
                        , txtNgayBD.DateTime.ToString("dd/MM/yyyy"), txtNgayKT.DateTime.ToString("dd/MM/yyyy"), this.type
                       },
                       grdPhieu.jsonFilter());

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                // {"total": 1,"page": 1,"records": 1,"rows" : [{"RN": "1","NHAPID": null,"XUATID": null,"TTPHIEUNX": "1","DOIUNGID": "873","NHAPXUATID": "109906"
                //,"TRANGTHAIID": "5","MA": "XK109906","KIEU": "3","TEN_KIEU": "Xuất kho","SOCHUNGTU": "","NGAYCT": "","NGAYNX": "11/10/18 14:02","TRANGTHAI": "Chờ duyệt"
                //,"HINHTHUCID": "13","TENHINHTHUC": "Đơn thuốc bệnh nhân","TENKHO": "Khoa Khám bệnh","SOPHIEU": "P000002226","MABENHNHAN": "BN00043001"
                //,"TENBENHNHAN": "TEST _ 1510","NGAYSINH": "1991","GIOITINH": "Nữ","NGAYDUYET": "","NGAYYLENH": "","NGUOIDUYET": "","MAUBENHPHAMID": "314092"
                //,"MAHOSOBENHAN": "BA18000969"}] }

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MA", "TEN_KIEU","TRANGTHAIID", "SOPHIEU", "MABENHNHAN"
                    , "TENBENHNHAN", "NGAYSINH", "GIOITINH", "TENKHO", "TRANGTHAI", "NGAYNX"
                        , "SOCHUNGTU", "NGAYDUYET", "NGUOIDUYET"
                });

                grdPhieu.setData(dt, responses.total, responses.page, responses.records);
                grdPhieu.setColumnAll(false);

                grdPhieu.onIndicator();
                grdPhieu.setColumn("TRANGTHAIID", " ");
                grdPhieu.setColumn("MA", "Phiếu");
                grdPhieu.setColumn("TEN_KIEU", "Loại");
                grdPhieu.setColumn("SOPHIEU", "Đơn thuốc / VT");
                grdPhieu.setColumn("MABENHNHAN", "Mã BN");
                grdPhieu.setColumn("TENBENHNHAN", "Họ tên");
                grdPhieu.setColumn("NGAYSINH", "Ngày sinh");
                grdPhieu.setColumn("GIOITINH", "GT");
                grdPhieu.setColumn("TENKHO", "Khoa");
                grdPhieu.setColumn("TRANGTHAI", "Trạng thái");
                grdPhieu.setColumn("NGAYNX", "Ngày NX");
                grdPhieu.setColumn("SOCHUNGTU", "Số CT");
                grdPhieu.setColumn("NGAYDUYET", "Ngày duyệt");
                grdPhieu.setColumn("NGUOIDUYET", "Người duyệt");

                //TAOMOI = 1;
                //KETTHUC = 4;
                //CHODUYET = 5;
                //DADUYET = 6;
                //KHONGDUYET = 7;
                grdPhieu.setColumnImage("TRANGTHAIID", new String[] { DADUYET.ToString(), CHODUYET.ToString(), KETTHUC.ToString() }
                       , new String[] { "./Resources/Circle_Green.png", "./Resources/Circle_Yellow.png", "./Resources/Circle_Red.png" });


                _loadChiTiet(1, null);
            }
        }
        private void _loadDSThuoc(object sender, EventArgs e)
        {
            int index = grdPhieu.gridView.FocusedRowHandle;
            DataRowView rowData = (DataRowView)grdPhieu.gridView.GetRow(index);
            if (rowData == null) return;
            // {"total": 1,"page": 1,"records": 1,"rows" : [{"RN": "1","NHAPID": null,"XUATID": null,"TTPHIEUNX": "1","DOIUNGID": "873","NHAPXUATID": "109906"
            //,"TRANGTHAIID": "5","MA": "XK109906","KIEU": "3","TEN_KIEU": "Xuất kho","SOCHUNGTU": "","NGAYCT": "","NGAYNX": "11/10/18 14:02","TRANGTHAI": "Chờ duyệt"
            //,"HINHTHUCID": "13","TENHINHTHUC": "Đơn thuốc bệnh nhân","TENKHO": "Khoa Khám bệnh","SOPHIEU": "P000002226","MABENHNHAN": "BN00043001"
            //,"TENBENHNHAN": "TEST _ 1510","NGAYSINH": "1991","GIOITINH": "Nữ","NGAYDUYET": "","NGAYYLENH": "","NGUOIDUYET": "","MAUBENHPHAMID": "314092"
            //,"MAHOSOBENHAN": "BA18000969"}] }

            string NHAPXUATID = rowData["NHAPXUATID"].ToString();
            string TRANGTHAIID = rowData["TRANGTHAIID"].ToString();
            string KIEU = rowData["KIEU"].ToString();
            string _loaiphieu = KIEU;

            int page = (int)sender;
            if (page > 0)
            {
                ResponsList responses = new ResponsList();
                responses = RequestHTTP.get_ajaxExecuteQueryPaging("DUC01S002.CTTHUOC", page, grdThuoc.getNumberPerPage(),
                       new String[] { "[0]" },
                       new string[] { NHAPXUATID },
                       grdThuoc.jsonFilter());

                DataTable dt = new DataTable();
                if (responses != null) dt = MyJsonConvert.toDataTable(responses.rows);

                //postData: {"func":"ajaxExecuteQueryPaging","uuid":"ea01560f-e989-4ae2-97e1-666c442fe421"
                // ,"params":["DUC01S002.CTTHUOC"],"options":[{"name":"[0]","value":"109906"}]}

                // {"total": 1,"page": 1,"records": 1,"rows" : [{"RN": "1","NHAPXUATCTID": "636888","MA": "ACE00","TEN": "Acetazolamid","CHOLANHDAODUYET": "0"
                // ,"TEN_DVT": "Viên","SOLUONG": "2","SOLUONGDUYET": "2","SLKHADUNG": "98","GIANHAP": "554433","THANHTIEN": "1108866","XUATVAT": "5","SOLO": "01"
                // ,"HANSUDUNG": "01/01/2019","TENKHO": "Kho Ngoại trú YHCT","NGUOIDUYET": "","NHOMTKBAOCAO": "","CHUY": "","LIEULUONG": "250mg","LOAI": "0"
                // ,"TENLOAI": "THUỐC TÂN DƯỢC","TENPHONGLUU": "Không có phòng lưu"}] }
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MA", "TEN", "LIEULUONG", "TEN_DVT"
                    , "SOLUONG", "SOLUONGDUYET", "GIANHAP", "XUATVAT", "THANHTIEN", "SOLO"
                        , "HANSUDUNG", "NGUOIDUYET", "CHUY"
                });

                grdThuoc.setData(dt, responses.total, responses.page, responses.records);
                grdThuoc.setColumnAll(false);

                grdThuoc.onIndicator();
                grdThuoc.setColumn("MA", "Mã thuốc");
                grdThuoc.setColumn("TEN", "Tên thuốc");
                grdThuoc.setColumn("LIEULUONG", "Hàm lượng");
                grdThuoc.setColumn("TEN_DVT", "Đơn vị");
                grdThuoc.setColumn("SOLUONG", "SL yêu cầu");
                grdThuoc.setColumn("SOLUONGDUYET", "SL duyệt");
                grdThuoc.setColumn("GIANHAP", "Đơn giá");
                grdThuoc.setColumn("XUATVAT", "VAT");
                grdThuoc.setColumn("THANHTIEN", "Thành tiền");
                grdThuoc.setColumn("SOLO", "Số lô");
                grdThuoc.setColumn("HANSUDUNG", "HSD");
                grdThuoc.setColumn("NGUOIDUYET", "BSDuyet");
                grdThuoc.setColumn("CHUY", "CHÚ Ý");
            }
        }
        private void _loadChiTiet(object sender, EventArgs e)
        {
            int index = grdPhieu.gridView.FocusedRowHandle;
            DataRowView rowData = (DataRowView)grdPhieu.gridView.GetRow(index);
            if (rowData == null) return;
            // {"total": 1,"page": 1,"records": 1,"rows" : [{"RN": "1","NHAPID": null,"XUATID": null,"TTPHIEUNX": "1","DOIUNGID": "873","NHAPXUATID": "109906"
            //,"TRANGTHAIID": "5","MA": "XK109906","KIEU": "3","TEN_KIEU": "Xuất kho","SOCHUNGTU": "","NGAYCT": "","NGAYNX": "11/10/18 14:02","TRANGTHAI": "Chờ duyệt"
            //,"HINHTHUCID": "13","TENHINHTHUC": "Đơn thuốc bệnh nhân","TENKHO": "Khoa Khám bệnh","SOPHIEU": "P000002226","MABENHNHAN": "BN00043001"
            //,"TENBENHNHAN": "TEST _ 1510","NGAYSINH": "1991","GIOITINH": "Nữ","NGAYDUYET": "","NGAYYLENH": "","NGUOIDUYET": "","MAUBENHPHAMID": "314092"
            //,"MAHOSOBENHAN": "BA18000969"}] }

            string NHAPXUATID = rowData["NHAPXUATID"].ToString();
            string TRANGTHAIID = rowData["TRANGTHAIID"].ToString();
            string KIEU = rowData["KIEU"].ToString();
            string _loaiphieu = KIEU;

            clickrow(null, null);

            #region On select Row

            //$("#txtID5").val(ret.MAUBENHPHAMID);
            txtHOTEN5.Text = rowData["TENBENHNHAN"].ToString();
            //         $("#toolbarIdtbInNhap").prop("disabled", false);
            //$("#toolbarIdtbInNhapKT").prop("disabled", false);
            //$("#toolbarIdtbInXuat").prop("disabled", false);
            //$("#toolbarIdtbBBKiemNhapHoaDon").prop("disabled", false);
            //$("#toolbarIdtbGiayTT").prop("disabled", false);
            if (this.ht.Contains("12") || this.ht.Contains("13") || this.ht.Contains("14"))
            {
                //$("#btnDonThuoc").show();
                btnDonThuoc.Visible = true;
                //$("#btnDonThuoc").prop("disabled", false);
                btnDonThuoc.Enabled = true;
            }
            else
            {
                //$("#btnDonThuoc").prop("disabled", true);
                btnDonThuoc.Enabled = false;
            }
            if (rowData["KIEU"].ToString() == "2" && rowData["XUATID"].ToString() != "" && !this.type.Contains("45"))
            {
                btnXemPhieu.Enabled = true;
                //$("#btnXemPhieu").prop("disabled", false);
            }
            else if (rowData["KIEU"].ToString() == "2" && rowData["NHAPID"].ToString() != "" && this.type.Contains("45"))
            {
                btnXemPhieu.Enabled = true;
                //$("#btnXemPhieu").prop("disabled", false);
            }
            else if (rowData["KIEU"].ToString() == "3" && rowData["NHAPID"].ToString() != "" && !this.type.Contains("45"))
            {
                btnXemPhieu.Enabled = true;
                //$("#btnXemPhieu").prop("disabled", false);
            }
            else if (rowData["KIEU"].ToString() == "3" && rowData["XUATID"].ToString() != "" && this.type.Contains("45"))
            {
                btnXemPhieu.Enabled = true;
                //$("#btnXemPhieu").prop("disabled", false);
            }
            else if (rowData["KIEU"].ToString() == "3" && rowData["XUATID"].ToString() != "" && this.type.Contains("4"))
            {
                btnXemPhieu.Enabled = true;
                //$("#btnXemPhieu").prop("disabled", false);
            }
            else if (rowData["KIEU"].ToString() == "2" && rowData["NHAPID"].ToString() != "" && this.type.Contains("4"))
            {
                btnXemPhieu.Enabled = true;
                //$("#btnXemPhieu").prop("disabled", false);
            }
            else
            {
                btnXemPhieu.Enabled = false;
                //$("#btnXemPhieu").prop("disabled", true);
            }




            if (TRANGTHAIID == CHODUYET.ToString())
            {
                //$("#toolbarIdtbDuyet").prop("disabled", false);
                tbDuyet.Enabled = true;
                //ban thuoc khach le - tra thuoc
                if (this.ht.Contains("15"))
                    tbTraThuoc.Enabled = false;
            }
            else
            {
                //$("#toolbarIdtbDuyet").prop("disabled", true);
                tbDuyet.Enabled = false;

                if (this.ht.Contains("15"))
                    if (KIEU == "3")
                        //$("#toolbarIdtbTraThuoc").prop("disabled", false);
                        tbTraThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    else
                        //$("#toolbarIdtbTraThuoc").prop("disabled", true);
                        tbTraThuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (TRANGTHAIID == DADUYET.ToString())
            {
                if (this.type.Contains("4") || this.type.Contains("5"))
                {
                    btnGoDuyet.Visible = true;
                    checkRole("btnGoDuyet");
                }

                if (KIEU == "2")
                {
                    //$("#btnNhapKho").prop("disabled", true);
                    btnNhapKho.Enabled = false;
                    //$("#toolbarIdtbYCXuat").prop("disabled", true); 
                    tbYCXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //$("#btnXuatTra").prop("disabled", false);
                    btnXuatTra.Enabled = false;
                }
                else if (KIEU == "3")
                {
                    //$("#btnNhapKho").prop("disabled", false); 
                    btnNhapKho.Enabled = true;
                    //$("#btnXuatTra").prop("disabled", true);
                    btnXuatTra.Enabled = false;
                    //$("#toolbarIdtbYCXuat").prop("disabled", false);
                    tbYCXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }

                //$("#toolbarIdtbNhapBu").prop("disabled", false);
                //$("#toolbarIdtbXuatThieu").prop("disabled", false);
                //$("#toolbarIdtbXuatHuy").prop("disabled", false);
                //$("#toolbarIdtbXuatKhac").prop("disabled", false);
                //$("#toolbarIdtbXuatDTTH").prop("disabled", false);
                tbNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbXuatDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                //$("#btnGoDuyet").prop("disabled", true);			 
                //$("#btnNhapKho").prop("disabled", true); 
                //$("#btnXuatTra").prop("disabled", true);
                btnGoDuyet.Enabled = false;
                btnNhapKho.Enabled = false;
                btnXuatTra.Enabled = false;

                //$("#toolbarIdtbYCXuat").prop("disabled", true);
                //$("#toolbarIdtbNhapBu").prop("disabled", true);
                //$("#toolbarIdtbXuatThieu").prop("disabled", true);
                //$("#toolbarIdtbXuatHuy").prop("disabled", true);
                //$("#toolbarIdtbXuatKhac").prop("disabled", true);
                //$("#toolbarIdtbXuatDTTH").prop("disabled", true);
                tbYCXuat.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbXuatDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

            if (TRANGTHAIID == KETTHUC.ToString() || TRANGTHAIID == DADUYET.ToString())
            {
                //$("#btnGoNhapkho").show();
                btnGoNhapkho.Visible = true;
                if (_loaiphieu == "0" || _loaiphieu == "1")
                {
                    //$("#toolbarIdtbXuatTraNCC").prop("disabled", false); 
                    tbXuatTraNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    checkRole("btnGoNhapkho");
                    //$("#toolbarIdtbSuaNCC").prop("disabled", false);
                    tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    //$("#toolbarIdtbXuatTraNCC").prop("disabled", true);
                    tbXuatTraNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //$("#btnGoNhapkho").prop("disabled", true);
                    btnGoNhapkho.Enabled = false;
                }
            }
            else
            {
                //$("#toolbarIdtbXuatTraNCC").prop("disabled", true); 
                tbXuatTraNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //$("#btnGoNhapkho").hide();
                btnGoNhapkho.Visible = false;
            }
            if (TRANGTHAIID == TAOMOI.ToString())
            {
                //$("#toolbarIdtbSua").prop("disabled", false);
                //$("#toolbarIdtbSuaNCC").prop("disabled", false);
                //$("#toolbarIdtbSuaNhapBu").prop("disabled", false);
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                //$("#toolbarIdtbSuaXuatHuy").prop("disabled", false);
                //$("#toolbarIdtbSuaXuatKhac").prop("disabled", false);
                //$("#toolbarIdtbSuaXuatThieu").prop("disabled", false);
                //$("#toolbarIdtbSuaDTTH").prop("disabled", false);
                //$("#toolbarIdtbSuaYLenhLT").prop("disabled", false);
                tbSuaXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaXuatKhac.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaXuatThieu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaDTTH.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tbSuaYLenhLT.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                // ban thuoc khach le
                //$("#toolbarIdtbSuaPhieuBanTHuoc").prop("disabled", false);
                tbSuaPhieuBanTHuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                //$("#toolbarIdtbSua").prop("disabled", true);
                //$("#toolbarIdtbSuaNCC").prop("disabled", false);
                tbSua.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                //$("#toolbarIdtbSuaNhapBu").prop("disabled", true);
                //$("#toolbarIdtbSuaXuatHuy").prop("disabled", true);
                tbSuaNhapBu.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaXuatHuy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //$("#toolbarIdtbSuaXuatKhac").prop("disabled", true);
                //$("#toolbarIdtbSuaXuatThieu").prop("disabled", true);
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //$("#toolbarIdtbSuaDTTH").prop("disabled", true);
                //$("#toolbarIdtbSuaYLenhLT").prop("disabled", true);
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                tbSuaNCC.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                // ban thuoc khach le
                //$("#toolbarIdtbSuaPhieuBanTHuoc").prop("disabled", true);
                tbSuaPhieuBanTHuoc.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            //{"func":"ajaxCALL_SP_O","params":["DUC01S002.CTPHIEU","109906",0],"uuid":"219e38c6-a769-4de8-99cb-4d2d59e3f21a"}
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC01S002.CTPHIEU", NHAPXUATID);
            //{"result": "[{\"NHAPID\": null,\"XUATID\": null,\"NOILAP\": \"Khoa Khám bệnh\",\"NGAYNX\": \"11/10/2018 14:02\",\"NGUOINX\": \"Quản trị hệ thống bệnh viện mức 1\"
            // ,\"NGUOIDUYET\": \"\",\"LANIN\": null,\"SOCHUNGTU\": \"\",\"NGAYCHUNGTU\": \"\",\"MA\": \"XK109906\",\"NHACUNGCAP\": \"\",\"CHIETKHAU\": null
            // ,\"GHICHU\": \"BN:.TEST _ 1510. (MS:BN00043001;Phieu:P000002226)\",\"TIENDON\": null,\"TONGCONG\": \"1108866\",\"TONGTIENDATRA\": null,\"TRANGTHAIID\": \"5\"
            // ,\"TRANGTHAI\": \"Chờ duyệt\",\"MANHAP\": \"\",\"MAXUAT\": \"\",\"DUYETVIENPHI\": \"1\",\"SOPHIEUBN\": \"0\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
            if (data_ar.Rows.Count > 0)
            {
                //FormUtil.setObjectToForm("divCT", "", row);
                txtNOILAP.Text = data_ar.Rows[0]["NOILAP"].ToString();
                txtNGAYNX.Text = data_ar.Rows[0]["NGAYNX"].ToString();
                txtSOCHUNGTU.Text = data_ar.Rows[0]["SOCHUNGTU"].ToString();
                txtNHACUNGCAP.Text = data_ar.Rows[0]["NHACUNGCAP"].ToString();

                if (data_ar.Columns.Contains("PHIEUYC")) txtPHIEUYC.Text = data_ar.Rows[0]["PHIEUYC"].ToString();
                txtCHIETKHAU.Text = data_ar.Rows[0]["CHIETKHAU"].ToString();
                txtGHICHU.Text = data_ar.Rows[0]["GHICHU"].ToString();
                txtNGUOINX.Text = data_ar.Rows[0]["NGUOINX"].ToString();
                txtLANIN.Text = data_ar.Rows[0]["LANIN"].ToString();
                txtNGAYCHUNGTU.Text = data_ar.Rows[0]["NGAYCHUNGTU"].ToString();
                txtMA.Text = data_ar.Rows[0]["MA"].ToString();
                txtTIENDON.Text = data_ar.Rows[0]["TIENDON"].ToString();

                //if (data_ar.Columns.Contains("TIENCHIETKHAU")) txtTIENCHIETKHAU.Text = data_ar.Rows[0]["TIENCHIETKHAU"].ToString();
                //txtTONGCONG.Text = data_ar.Rows[0]["TONGCONG"].ToString();
                txtTONGTIENDATRA.Text = data_ar.Rows[0]["TONGTIENDATRA"].ToString();


                //if (row.CHIETKHAU == null)
                //    row.CHIETKHAU = '';
                //if (row.TIENDON == null)
                //    row.TIENDON = '';
                //if (row.TONGCONG == null)
                //    row.TONGCONG = '';
                //if (row.TONGTIENDATRA == null)
                //    row.TONGTIENDATRA = '';

                // $("#txtCHIETKHAU").val(Number(row.CHIETKHAU).format(2, 3, ',')); --> chuyển thanfh dạng số 0.00

                float tienchietkhau = 0;
                try
                {
                    tienchietkhau = (Func.Parse_float(data_ar.Rows[0]["TONGCONG"].ToString()) * Func.Parse_float(data_ar.Rows[0]["CHIETKHAU"].ToString())) / 100;
                }
                catch (Exception ex) { }
                if (tienchietkhau > 0)
                    //$("#txtTIENCHIETKHAU").val(Number((row.TONGCONG) * (row.CHIETKHAU) / 100).format(2, 3, ',')); 
                    txtTIENCHIETKHAU.Text = tienchietkhau.ToString();  //  --> chuyển thanfh dạng số 0.00

                //$("#txtTIENDON").val(Number(row.TIENDON).format(2, 3, ',')); //  --> chuyển thanfh dạng số 0.00

                //$("#txtTONGCONG").val(Number(Number(row.TONGCONG) - tienchietkhau).format(2, 3, ','));//  --> chuyển thanfh dạng số 0.00
                txtTONGCONG.Text = (Func.Parse_float(data_ar.Rows[0]["TONGCONG"].ToString()) - tienchietkhau).ToString();

                //$("#txtTONGTIENDATRA").val(Number(row.TONGTIENDATRA).format(2, 3, ','));//  --> chuyển thanfh dạng số 0.00
            }
            #endregion


            //Chỉnh lại vị trí các button
            Button_Locate();
        }
        private void clickrow(object sender, EventArgs e)
        {
            int index = grdPhieu.gridView.FocusedRowHandle;
            DataRowView rowData = (DataRowView)grdPhieu.gridView.GetRow(index);
            if (rowData == null) return;

            // {"total": 1,"page": 1,"records": 1,"rows" : [{"RN": "1","NHAPID": null,"XUATID": null,"TTPHIEUNX": "1","DOIUNGID": "873","NHAPXUATID": "109906"
            //,"TRANGTHAIID": "5","MA": "XK109906","KIEU": "3","TEN_KIEU": "Xuất kho","SOCHUNGTU": "","NGAYCT": "","NGAYNX": "11/10/18 14:02","TRANGTHAI": "Chờ duyệt"
            //,"HINHTHUCID": "13","TENHINHTHUC": "Đơn thuốc bệnh nhân","TENKHO": "Khoa Khám bệnh","SOPHIEU": "P000002226","MABENHNHAN": "BN00043001"
            //,"TENBENHNHAN": "TEST _ 1510","NGAYSINH": "1991","GIOITINH": "Nữ","NGAYDUYET": "","NGAYYLENH": "","NGUOIDUYET": "","MAUBENHPHAMID": "314092"
            //,"MAHOSOBENHAN": "BA18000969"}] }

            string NHAPXUATID = rowData["NHAPXUATID"].ToString();
            string TRANGTHAIID = rowData["TRANGTHAIID"].ToString();
            string KIEU = rowData["KIEU"].ToString();
            string _loaiphieu = KIEU;

            _loadDSThuoc(1, null);

            #region On click Row
            if (TRANGTHAIID == "1")
            {
                if (this.type.Contains("0") || this.type.Contains("1") || this.type.Contains("2") ||
                   this.type.Contains("3") || this.type.Contains("E")
                        )
                {
                    //tbSuaClick();
                    tbSua_ItemClick(null, null);
                }

                if (this.type.Contains("6") || this.type.Contains("7"))
                {
                    tbSuaNCC_ItemClick(null, null);
                }

                if (this.type.Contains("8") || this.type.Contains("9"))
                {
                    tbSuaNhapBu_ItemClick(null, null);
                }

                if (this.type.Contains("A") || this.type.Contains("B"))
                {
                    tbSuaXuatHuy_ItemClick(null, null);
                }

                if (this.type.Contains("C") || this.type.Contains("D"))
                {
                    tbSuaXuatThieu_ItemClick(null, null);
                }

                if (this.type.Contains("I"))
                {
                    tbSuaDTTH_ItemClick(null, null);
                }
            }
            #endregion

        }

        #endregion


        // Menu thanh ngang bên trên
        private void tbDuyet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DlgUtil.close("dlgAppr");
            //_loadDSPhieu();

            DataRowView row;
            if (getRowFocus(out row) == false) return;
            //row["KIEU"].ToString()

            string _id = row["NHAPXUATID"].ToString();
            string _kieu = row["KIEU"].ToString();
            string _hinhthuc = row["HINHTHUCID"].ToString();


            //             //_loadDSPhieu();
            //             if ((_kieu == "2" && e.ttnhap != 4) || (_kieu == "3" && e.ttxuat != 4))
            //             {
            //                 var _nhapxuatid = e.nhapid;
            //                 if (_kieu != 2)
            //                 {
            //                     _nhapxuatid = e.xuatid;
            //                 }
            //                 EventUtil.setEvent("nhapkho_cancel2", function(e){
               //                     DlgUtil.close("dlgTTPhieu");
            //                     _loadDSPhieu();
            //                 });
            //                 var myVar = { nhapxuatid:_nhapxuatid, nxid: _id, kieu: _kieu,edit: that.opt.cs};
            //             dlgPopup = DlgUtil.buildPopupUrl("dlgTTPhieu", "divDlg", "manager.jsp?func=../duoc/DUC35T001_ThongTinPhieu", myVar, "Thông tin phiếu", 1200, 580);
            //             DlgUtil.open("dlgTTPhieu");


            //var myVar = { nhapxuatId:_id, kieu: _kieu, hinhthuc: _hinhthuc, ht: this.ht, edit: this.cs, khoid: $("#cboKho").val() };
            if (this.td == "1")
            {
                //dlgPopup=DlgUtil.buildPopupUrl("dlgAppr","divDlg","manager.jsp?func=../duoc/DUC75T001_XacNhanPhieuLinh",myVar,"Duyệt phiếu",1100,550);
                //DlgUtil.open("dlgAppr");
            }
            else if (PHARMA_GIAODIEN_NHAP_NCC == "DUC02N001_NhapThuocNCC_BVNT" && this.ht != "17")
            {
                //dlgPopup=DlgUtil.buildPopupUrl("dlgAppr","divDlg","manager.jsp?func=../duoc/DUC47T001_NGT_YeuCauNhapThuoc",myVar,"Duyệt phiếu",1100,550);
                //DlgUtil.open("dlgAppr"); 
                subform.DUC47T001_NGT_YeuCauNhapThuoc frm = new subform.DUC47T001_NGT_YeuCauNhapThuoc(_id, _kieu, _hinhthuc, this.ht, this.cs, cboKho.SelectValue);
                openForm(frm);

                //reload

            }
            else
            {
                //dlgPopup =DlgUtil.buildPopupUrl("dlgAppr","divDlg","manager.jsp?func=../duoc/DUC47T001_YeuCauNhapThuoc",myVar,"Duyệt phiếu",1100,550);
                //DlgUtil.open("dlgAppr");
                subform.DUC47T001_YeuCauNhapThuoc frm = new subform.DUC47T001_YeuCauNhapThuoc(_id, _kieu, _hinhthuc, this.ht, this.cs, cboKho.SelectValue);
                openForm(frm);
            }
        }

        private void menuDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        // Các button
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtNgayBD.DateTime > txtNgayKT.DateTime)
            {
                MessageBox.Show("Giá trị từ ngày không được vượt quá giá trị đến ngày");
                txtNgayBD.Focus();
                return;
            }

            _loadDSPhieu(1, null);

            string json = "";
            json += Func.json_item("TENKHO", cboKho.SelectValue);
            json += Func.json_item("TUNGAY", txtNgayBD.DateTime.ToString(Const.FORMAT_date1));
            json += Func.json_item("DENNGAY", txtNgayKT.DateTime.ToString(Const.FORMAT_date1));
            json += Func.json_item("LOAIPHIEU", cboLoaiPhieu.SelectValue);
            json += Func.json_item("HINHTHUC", cboHinhThuc.SelectValue);
            json += Func.json_item("TRANGTHAI", cboTrangThaiDuyet.SelectValue);
            json = Func.json_item_end(json);
            //json = json.Replace("\"", "\\\"");
            //{"TENKHO":"873","TUNGAY":"01/10/2018","DENNGAY":"13/11/2018","LOAIPHIEU":"2,3","HINHTHUC":"13","TRANGTHAI":"5,6,7"}

            //var param_ar = $(location).attr('href') +'$'+ gia_tri_json+ '$';
            // https://histest.vnptsoftware.vn/vnpthis/main/manager.jsp?func=../duoc/DUC01S002_PhieuYeuCau&lk=2,6,5,12&lp=2,3&ht=13&tt=5,6,7&type=45&cs=1&gd=THUOC&ssid=7285D310DA870C250ECA922C073D3319.TEST1${"TENKHO":"873","TUNGAY":"01/10/2018","DENNGAY":"13/11/2018","LOAIPHIEU":"2,3","HINHTHUC":"13","TRANGTHAI":"5,6,7"}$"
            string param_ar = Const.LinkService.Substring(0, Const.LinkService.LastIndexOf('/')) + "/main/manager.jsp?func=../duoc/DUC01S002_PhieuYeuCau&" + URL_PARA + "$" + json + "$";
            string result = RequestHTTP.call_ajaxCALL_SP_S_result("DUC_ADDTHAMSOKT", param_ar);
        }

        private void btnGoDuyet_Click(object sender, EventArgs e)
        {
            //       $("#btnGoDuyet").on("click", function(e){

            DataRowView row;
            if (getRowFocus(out row) == false) return;

            var _id = row["NHAPXUATID"].ToString();
            var _loaiphieu = row["KIEU"].ToString();
            var _maphieu = row["MA"].ToString();

            DialogResult dialogResult = MessageBox.Show("Bạn có thực sự muốn gỡ duyệt?", "", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;

            DataTable ret = RequestHTTP.call_ajaxCALL_SP_S_table("DUC47T001.CANCEL", _id);
            if (ret.Rows.Count == 0) return;

            if (Func.Parse(ret.Rows[0]["SUCCESS"].ToString()) > 0)
            {
                MessageBox.Show("Gỡ duyệt thành công");
                //load lại
                _loadDSPhieu(grdPhieu.ucPage1.Current(), null);

                //$('#grdPhieu').jqGrid('setCell', row, 7, 'Không duyệt');
                //$('#grdPhieu').jqGrid('setCell', row, 3, KHONGDUYET);
                //_loadChiTiet(_id, KHONGDUYET);
                //_formatRow(row, KHONGDUYET - 1);
                //_loadDSPhieu();
            }
            else
            {
                MessageBox.Show(ret.Rows[0]["MESSAGE"].ToString());
            }
        }

        //Đơn thuốc/VT
        private void btnDonThuoc_Click(object sender, EventArgs e)
        {
            //$("#btnDonThuoc").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            var _id = row["NHAPXUATID"].ToString();

            subform.DUC51S001_DSDonThuoc frm = new subform.DUC51S001_DSDonThuoc(_id, this.gd);
            openForm(frm);

          //  var myVar = { nhapxuatid:_id,gd: _opt.gd};
          //dlgPopup=DlgUtil.buildPopupUrl("dlgTTPhieu","divDlg","manager.jsp?func=../duoc/DUC51S001_DSDonThuoc",myVar,"Thông tin đơn thuốc/VT",1200,590);

        }

        private void btnGoNhapkho_Click(object sender, EventArgs e)
        { // Hủy phiếu
          //$("#btnGoNhapkho").on("click", function(e){

            DataRowView row;
            if (getRowFocus(out row) == false) return;

            var _parId = row["NHAPXUATID"].ToString();
            var _loaiphieu = row["KIEU"].ToString();
            var _maphieu = row["MA"].ToString();

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn hủy phiếu không?", "", MessageBoxButtons.YesNo);
            if (dialogResult != DialogResult.Yes) return;
             
            var ctl_sql = "DUC01S001.10";
            if (_loaiphieu == "0") ctl_sql = "DUC01S001.09";


            DataTable ret = RequestHTTP.call_ajaxCALL_SP_S_table(ctl_sql, _parId);
            if (ret.Rows.Count == 0) return;

            if (ret.Rows[0]["SUCCESS"].ToString() == "0")
            {
                MessageBox.Show(ret.Rows[0]["MESSAGE"].ToString());
            }
            else
            {
                if (_loaiphieu == "0" && PHARMA_KETNOI_CONG_BYT == "1")
                {
                    // thuc hien huy phieu len cong BYT
                    DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC_NTHUOC_BYT", _maphieu + "$2");
                    var user = data_ar.Rows[0]["WS_USR"].ToString();
                    var pas = data_ar.Rows[0]["WS_PWD"].ToString();
                    var url = data_ar.Rows[0]["WS_URL"].ToString();
                    var xmldata = data_ar.Rows[0]["NTBYT"].ToString();

                    //var resultCongBYT = XML_BYT_nhapThuoc(url, user, pas, xmldata);
                    //var res = JSON.parse(resultCongBYT);
                    //var _error = res.Data.Error.Error_Message;
                    //DlgUtil.showMsg("Hủy phiếu thành công");
                    //DlgUtil.showMsg("Thông báo Gửi Dữ liệu hủy phiếu sang BYT :" + _error);
                }
                else
                {
                    MessageBox.Show("Hủy phiếu thành công");
                }

                //load lại
                _loadDSPhieu(grdPhieu.ucPage1.Current(), null);
            }
        }

        private void btnHuyGuiPhieu_Click(object sender, EventArgs e)
        {

        }





        private void txtTimKho_EditValueChanged(object sender, EventArgs e)
        {
            // Tạm ko dùng ô nhập tìm kho
            //var sql_par = that.opt.lk + '$' + $("#txtTimKho").val().trim() + '$';
            //var result = jsonrpc.AjaxJson.ajaxCALL_SP_O(
            //        "DUC01S002.DSKHO4", sql_par);
            //ComboUtil.getComboTag("cboKho", "DUC01S002.DSKHO4",
            //        sql_par, "", "", "sp", '', function(){ });

        } 
        private void btnLCDNHO5_Click(object sender, EventArgs e)
        {
            VNPT.HIS.CommonForm.NGT02K053_PK_LCD frm = new VNPT.HIS.CommonForm.NGT02K053_PK_LCD();
            frm.Show();
        }




        #region CÁC HÀM PHỤ


        private string URL_PARA = "";
        private string getPara(string name)
        {
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
                            lbPara = "&" + lbControl.Text.Replace("&amp;", "&") + "&";
                            URL_PARA = lbPara;
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
        private void checkAnGia(string _hinhthuc)
        {
            //var _par = ['PHARMA_GD_AN_GIA_TVT'];
            //var result = jsonrpc.AjaxJson.ajaxCALL_SP_S("COM.CAUHINH",
            //        _par.join('$'));
            //if (typeof result === "undefined") return 0;
            //var hinhthuc = result.split(",");
            //if (hinhthuc.indexOf(_hinhthuc) != -1) return 1;
            //return 0;
        }
        private void checkLanhDaoDuyet()
        {
            //            var rowIds = $('#grdThuoc').jqGrid('getDataIDs');
            //            for (i = 0; i < rowIds.length; i++)
            //            {
            //                rowData = $('#grdThuoc').jqGrid('getRowData', rowIds[i]);
            //                var _color = '#FF9900';
            //                if (rowData['CHOLANHDAODUYET'] == '1')
            //                {			
            //				$('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td').each(function(index, element) {			        
            //					$(element).css('background-color', _color);
            //					$('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td').attr('title', rowData['CHUY']);
            //                    });

            //        }
            //			if (canhBao.indexOf(rowData["LOAI"])!= -1 && that.opt.ht=="13" ) {

            //				$('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td').each(function(index, element)
            //        {			        
            //					$(element).css('background-color', _color);
            //					$('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td').attr('title', rowData['TENLOAI']);
            //        });
            //				canhBaoloaithuoc = rowData['TENLOAI'];
            //			}
            //}
        }
        private void _formatRow(string _rId, string _fIndex)
        {
            //    var _icon = '';
            //    if (that.opt.imgPath[_fIndex] != '')
            //        _icon = '<center><img src="../common/image/' + that.opt.imgPath[_fIndex] + '" width="15px"></center>'; 
            //$("#grdPhieu").setRowData(_rId,{ icon: _icon});
            //$("#grdPhieu").find("tr[id='" + _rId + "']").find('td').each(function(index, element) {
            //        if ($("#grdPhieu").jqGrid('getRowData', _rId).TTPHIEUNX == 4)
            //		$(element).css({ 'color':that.opt.foreColor[_fIndex]}); 
            //	else
            //		$(element).css({ 'color':'#FF0000'});
            //    });
        }
        private void checkRole(string control_name)
        {
            // btnGoDuyet
            // btnGoNhapkho
            string _parPQ = "DUC01S002_PhieuYeuCau" + "%ht=" + this.ht + "$";
            DataTable result = RequestHTTP.call_ajaxCALL_SP_O("DUC.PQSCREEN.03", _parPQ);
            for (int i = 0; i < result.Rows.Count; i++)
            {
                if (control_name == "btnGoDuyet" && result.Rows[i]["ELEMENT_ID"].ToString() == "btnGoDuyet")
                {
                    if (result.Rows[i]["ROLES"].ToString() == "1")
                        btnGoDuyet.Enabled = true;
                    //$('#' + result.Rows[i]["ELEMENT_ID"].ToString()).prop('disabled', false);

                    if (result.Rows[i]["ROLES"].ToString() == "0" || result.Rows[i]["ROLES"].ToString() == "")
                        btnGoDuyet.Enabled = false;
                    //$('#' + result.Rows[i]["ELEMENT_ID"].ToString()).prop('disabled', true);
                }
                else if (control_name == "btnGoNhapkho" && result.Rows[i]["ELEMENT_ID"].ToString() == "btnGoNhapkho")
                {
                    if (result.Rows[i]["ROLES"].ToString() == "1")
                        btnGoNhapkho.Visible = true;
                    if (result.Rows[i]["ROLES"].ToString() == "0" || result.Rows[i]["ROLES"].ToString() == "")
                        btnGoNhapkho.Visible = false;
                }
            }
        }
        public void GetThamSoKhoiTao()
        {
            try
            {
                //var param = getPara("lk");
                //hard code
                string url = "https://histest.vnptsoftware.vn/vnpthis/main/manager.jsp?func=../duoc/DUC01S002_PhieuYeuCau&lk=2,6,5,12&lp=2,3&ht=13&tt=5,6,7&type=45&cs=1&gd=THUOC&ssid=DDC2E516267B41767E2F65223E6360B7.TEST1$";

                url = Const.LinkService.Substring(0, Const.LinkService.LastIndexOf('/')) + "/main/manager.jsp?func=../duoc/DUC01S002_PhieuYeuCau&" + URL_PARA;


                var ketQua = RequestHTTP.call_ajaxCALL_SP_S_result("DUC_GETTHAMSOKT", url);
                if (string.IsNullOrEmpty(ketQua))
                {
                    return;
                }

                var KetQuaDto = JsonConvert.DeserializeObject<KetQuaDto>(ketQua, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                cboKho.SelectValue = KetQuaDto.TENKHO;
                cboLoaiPhieu.SelectValue = KetQuaDto.LOAIPHIEU;
                cboHinhThuc.SelectValue = KetQuaDto.HINHTHUC;
                cboTrangThaiDuyet.SelectValue = KetQuaDto.TRANGTHAI;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void Button_Locate()
        {

            int ww = 0, hh = 0;
            int pading = 5;
            ww += (btnNhapKho.Visible ? btnNhapKho.Size.Width + pading : 0) +
                (btnXuatTra.Visible ? btnXuatTra.Size.Width + pading : 0) +
                (btnGoDuyet.Visible ? btnGoDuyet.Size.Width + pading : 0) +
                (btnGoNhapkho.Visible ? btnGoNhapkho.Size.Width + pading : 0) +
                (btnHuyGuiPhieu.Visible ? btnHuyGuiPhieu.Size.Width + pading : 0) +
                (btnInPhieu.Visible ? btnInPhieu.Size.Width + pading : 0) +
                (btnXemPhieu.Visible ? btnXemPhieu.Size.Width + pading : 0) +
                (btnDonThuoc.Visible ? btnDonThuoc.Size.Width + pading : 0);
            ww -= pading;
            int Y0 = simpleButton1.Location.Y;
            int x0 = simpleButton1.Location.X - ww / 2;
            int addX0 = 0;
            if (btnNhapKho.Visible)
            {
                btnNhapKho.Location = new Point(x0 + addX0, Y0);
                addX0 += btnNhapKho.Size.Width + pading;
            }
            if (btnXuatTra.Visible)
            {
                btnXuatTra.Location = new Point(x0 + addX0, Y0);
                addX0 += btnXuatTra.Size.Width + pading;
            }
            if (btnGoDuyet.Visible)
            {
                btnGoDuyet.Location = new Point(x0 + addX0, Y0);
                addX0 += btnGoDuyet.Size.Width + pading;
            }
            if (btnGoNhapkho.Visible)
            {
                btnGoNhapkho.Location = new Point(x0 + addX0, Y0);
                addX0 += btnGoNhapkho.Size.Width + pading;
            }
            if (btnHuyGuiPhieu.Visible)
            {
                btnHuyGuiPhieu.Location = new Point(x0 + addX0, Y0);
                addX0 += btnHuyGuiPhieu.Size.Width + pading;
            }
            if (btnInPhieu.Visible)
            {
                btnInPhieu.Location = new Point(x0 + addX0, Y0);
                addX0 += btnInPhieu.Size.Width + pading;
            }
            if (btnXemPhieu.Visible)
            {
                btnXemPhieu.Location = new Point(x0 + addX0, Y0);
                addX0 += btnXemPhieu.Size.Width + pading;
            }
            if (btnDonThuoc.Visible)
            {
                btnDonThuoc.Location = new Point(x0 + addX0, Y0);
                addX0 += btnDonThuoc.Size.Width + pading;
            }
        }
        private void checkRolePhieuIn()
        {
            // "/vnpthis/main/manager.jsp?func=../duoc/DUC01S002_PhieuYeuCau&lk=2,6,5,12&lp=2,3&ht=13&tt=5,6,7&type=45&cs=1&gd=THUOC$"
            string _parPQ = "/vnpthis/main/manager.jsp?func=../duoc/DUC01S002_PhieuYeuCau&" + URL_PARA + "$";

            DataTable result = RequestHTTP.call_ajaxCALL_SP_O("DUC81S001.04", _parPQ);

            for (int i = 0; i < result.Rows.Count; i++)
            {
                Control[] control_hide = this.Controls.Find(result.Rows[i]["PHIEUINID"].ToString(), true);
                if (control_hide.Length > 0) control_hide[0].Visible = false;
                //  $('#' + result[i].PHIEUINID).hide();
            }
        }

        private void openForm(Form frm, string optionsPopup = "1")
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
        private bool getRowFocus(out DataRowView row)
        {
            row = null;
            bool ret = true;
            try
            {
                int index = grdPhieu.gridView.FocusedRowHandle;
                row = (DataRowView)grdPhieu.gridView.GetRow(index);
                // {"total": 1,"page": 1,"records": 1,"rows" : [{"RN": "1","NHAPID": null,"XUATID": null,"TTPHIEUNX": "1","DOIUNGID": "873","NHAPXUATID": "109906"
                //,"TRANGTHAIID": "5","MA": "XK109906","KIEU": "3","TEN_KIEU": "Xuất kho","SOCHUNGTU": "","NGAYCT": "","NGAYNX": "11/10/18 14:02","TRANGTHAI": "Chờ duyệt"
                //,"HINHTHUCID": "13","TENHINHTHUC": "Đơn thuốc bệnh nhân","TENKHO": "Khoa Khám bệnh","SOPHIEU": "P000002226","MABENHNHAN": "BN00043001"
                //,"TENBENHNHAN": "TEST _ 1510","NGAYSINH": "1991","GIOITINH": "Nữ","NGAYDUYET": "","NGAYYLENH": "","NGUOIDUYET": "","MAUBENHPHAMID": "314092"
                //,"MAHOSOBENHAN": "BA18000969"}] }

                //string NHAPXUATID = rowData["NHAPXUATID"].ToString();
                //string TRANGTHAIID = rowData["TRANGTHAIID"].ToString();
                //string KIEU = rowData["KIEU"].ToString();

                if (row == null) ret = false;
            }
            catch(Exception ex)
            {
                ret= false;
            }

            if (ret == false)
            {
                MessageBox.Show("Chưa chọn phiếu!");
            }

            return ret;
        }
         

        #endregion

        #region Menu In ấn 
        private void tbInPhieuLinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 
        {
            //    $("#toolbarIdtbInPhieuLinh").on("click", function(e) {
            DataRowView row;
            if (getRowFocus(out row) == false) return;
 
            subform.DUC_DanhSachPhieuIn frm = new subform.DUC_DanhSachPhieuIn(
                row["NHAPXUATID"].ToString(), this.ht,loaiphieu: this.type);
            openForm(frm); 
        }
        
        private void tbInPhieu2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //      $("#toolbarIdtbInPhieu2Lien").on("click", function(e) {
            DataRowView row;
            if (getRowFocus(out row) == false) return;
             
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "NTU004_PHIEULINHTHUOC_01DBV01_TT23_A4", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbInPLinhThuocNGT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInPLinhThuocNGT").on("click", function(e) {
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC004_PHIEUCAPTHUOCNGOAITRU", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void tbInPhieuNoThuoc2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         $("#toolbarIdtbInPhieuNoThuoc2Lien").on("click", function(e) {
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());
            table.Rows.Add("thuocvattu", "String", "THUỐC/VẬT TƯ");

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_PHIEUNO_THUOCVATTU_A4", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void tbInPhieuLinhThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         $("#toolbarIdtbInPhieuLinhThuoc").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString()); 

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_PHIEU_LINH_THUOC", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void tbInPhieuLinhVatTu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         $("#toolbarIdtbInPhieuLinhVatTu").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "Long", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_Phieu_Linh_Vattu", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void tbInPLThuocGayNghienHT1Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         $("#toolbarIdtbInPLThuocGayNghienHT1Lien").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "Long", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "NTU025_PHIEULINHTHUOCGAYNGHIENHT_1LIEN_A4", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void tbInPXThuocGayNghienHT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdtbInPXThuocGayNghienHT").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC015_PHIEUXUATKHOTHUOCGAYNGHIENTAMTHANTIENCHATDUNGLAMTHUOC_MS3_TT192014_A4", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void print_1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_1").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "NTU025_PHIEULINHTHUOCTPGAYNGHIENHUONGTHANTIENCHAT_MS8_TT192014_A4", "pdf", 720, 1200);
            openForm(frm);

            //           var _par = ['PHARMA_BC_THUOC_HUONG_THAN'];
            //           var cauhinhHT = jsonrpc.AjaxJson.ajaxCALL_SP_S("COM.CAUHINH.THEOMA",
            //                   _par.join('$'));
            //           if (cauhinhHT == '1')
            //           {
            //               CommonUtil.openReportGet('window', 'NTU025_PHIEULINHTHUOCTPGAYNGHIENTIENCHAT_MS8_TT192014_A4', "pdf", par);
            //           } 
        }

        private void tbInNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInNhap").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());
             
            string PHARMA_BC_TACH_PN = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH.THEOMA", "PHARMA_INTACHMA_PHIEUNHAP");
            if (PHARMA_BC_TACH_PN == "1")
            {
                //openReport('window', 'DUC008_PHIEUNHAPKHO_QD_BTC_THUOCTHUONG_TTKHA', 'pdf', par);
                //openReport('window', 'DUC008_PHIEUNHAPKHO_QD_BTC_GNHT_TTKHA', 'pdf', par);
                VNPT.HIS.Controls.SubForm.frmPrint frm1 = new Controls.SubForm.frmPrint(table, "DUC008_PHIEUNHAPKHO_QD_BTC_THUOCTHUONG_TTKHA", "pdf", 720, 1200);
                openForm(frm1);
                VNPT.HIS.Controls.SubForm.frmPrint frm2 = new Controls.SubForm.frmPrint(table, "DUC008_PHIEUNHAPKHO_QD_BTC_GNHT_TTKHA", "pdf", 720, 1200);
                openForm(frm2);
            }
            else
            {

                if (PHARMA_BC_NHAP_KHO == 1)
                {
                    //var rpName = "DUC008_PHIEUNHAPKHO_QD_BTC_A4_SN_HNM" + jsonrpc.AjaxJson.getSystemDate('DDMMYY-HH24MISS') + "." + 'xls';
                    //CommonUtil.inPhieu('window', "DUC008_PHIEUNHAPKHO_QD_BTC_A4_SN_HNM", 'xls', par, rpName);
                    
                    VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC008_PHIEUNHAPKHO_QD_BTC_A4_SN_HNM", "pdf", 720, 1200);
                    openForm(frm);
                }
                else
                {
                    //var rpName = "DUC008_PHIEUNHAPKHO_QD_BTC_A4" + jsonrpc.AjaxJson.getSystemDate('DDMMYY-HH24MISS') + "." + 'xlsx';
                    //CommonUtil.inPhieu('window', "DUC008_PHIEUNHAPKHO_QD_BTC_A4", 'xlsx', par, rpName);

                    VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC008_PHIEUNHAPKHO_QD_BTC_A4", "pdf", 720, 1200);
                    openForm(frm);
                }
            }
        }

        private void tbInNhapKT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInNhapKT").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC008_PHIEUNHAPKHOKT_QD_BTC_A4", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void tbInXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         $("#toolbarIdtbInXuat").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            if (PHARMA_BC_XUAT_KHO == 1)
            {
                //var rpName = "DUC009_PHIEUXUATKHO_QD_BTC_A4_SN_HNM" + jsonrpc.AjaxJson.getSystemDate('DDMMYY-HH24MISS') + "." + 'xls';
                //CommonUtil.inPhieu('window', "DUC009_PHIEUXUATKHO_QD_BTC_A4_SN_HNM", 'xls', par, rpName);

                VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC009_PHIEUXUATKHO_QD_BTC_A4_SN_HNM", "pdf", 720, 1200);
                openForm(frm);
            }
            else if (PHARMA_BC_NHAP_KHO == 919 || PHARMA_BC_NHAP_KHO == 944)
            {
                subform.DUC_DSInPhieuXuatTTKHA frm = new subform.DUC_DSInPhieuXuatTTKHA(row["NHAPXUATID"].ToString());
                openForm(frm);
            }
            else
            {
                //var rpName = "DUC009_PHIEUXUATKHO_QD_BTC_A4" + jsonrpc.AjaxJson.getSystemDate('DDMMYY-HH24MISS') + "." + 'xlsx';
                //    CommonUtil.inPhieu('window', "DUC009_PHIEUXUATKHO_QD_BTC_A4", 'xlsx', par, rpName);

                VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC009_PHIEUXUATKHO_QD_BTC_A4", "pdf", 720, 1200);
                openForm(frm);
            }
        }

        private void tbGiayTT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         $("#toolbarIdtbGiayTT").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_GIAYDENGHITHANHTOAN", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void print_3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_3").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "NTU007_PHIEUTRALAITHUOCHOACHATVTYTTIEUHAO_05DBV01_TT23_A4", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void print_4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_4").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;
              
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC007.01", row["NHAPXUATID"].ToString());
            if (data_ar.Rows.Count == 0) return;

            if (data_ar.Rows[0]["NHAPXUATID"].ToString() == "0")
            {
                MessageBox.Show("Phiếu chưa có xuất hủy thuốc");
            }
            else
            {  
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("soyte", "String", data_ar.Rows[0]["SO_Y_TE"].ToString());
                table.Rows.Add("tenbenhvien", "String", data_ar.Rows[0]["BENH_VIEN"].ToString());
                table.Rows.Add("khoa", "String", data_ar.Rows[0]["KHOA"].ToString());
                table.Rows.Add("so", "String", data_ar.Rows[0]["MA_PHIEU"].ToString());
                table.Rows.Add("chutichhd", "String", data_ar.Rows[0]["CHUTICHHD"].ToString());
                table.Rows.Add("thuki", "String", data_ar.Rows[0]["THUKI"].ToString());
                table.Rows.Add("truongphongtckt", "String", data_ar.Rows[0]["TRUONGPHONGTCKT"].ToString());
                table.Rows.Add("truongkhoaduoc", "String", data_ar.Rows[0]["TRUONGKHOADUOC"].ToString());
                table.Rows.Add("thongkeduoc", "String", data_ar.Rows[0]["THONGKEDUOC"].ToString());
                table.Rows.Add("ykiendexuat", "String", data_ar.Rows[0]["YKIENDEXUAT"].ToString());
                table.Rows.Add("giohop", "String", data_ar.Rows[0]["GIOHOP"].ToString());
                table.Rows.Add("phuthop", "String", data_ar.Rows[0]["PHUTHOP"].ToString());
                table.Rows.Add("ngayhop", "String", data_ar.Rows[0]["NGAYHOP"].ToString());
                table.Rows.Add("giohopden", "String", data_ar.Rows[0]["GIOHOPDEN"].ToString());
                table.Rows.Add("phuthopden", "String", data_ar.Rows[0]["PHUTHOPDEN"].ToString());
                table.Rows.Add("ngayhopden", "String", data_ar.Rows[0]["NGAYHOPDEN"].ToString());
                table.Rows.Add("nhapxuatid", "String", data_ar.Rows[0]["NHAPXUATID"].ToString());

                VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC007_BBTHANHLYTHUOCHOACHATVATTUYTTIEUHAO_15DBV01_TT22_A4", "pdf", 720, 1200);
                openForm(frm);
            }
        }

        private void print_5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_5").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC013.01", row["NHAPXUATID"].ToString());
            if (data_ar.Rows.Count == 0) return;

            if (data_ar.Rows[0]["NHAPXUATID"].ToString() == "0")
            {
                MessageBox.Show("Phiếu chưa có xuất hủy thuốc");
            }
            else
            {
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("soyte", "String", data_ar.Rows[0]["SO_Y_TE"].ToString());
                table.Rows.Add("tenbenhvien", "String", data_ar.Rows[0]["BENH_VIEN"].ToString());
                table.Rows.Add("khoa", "String", data_ar.Rows[0]["KHOA"].ToString());
                table.Rows.Add("so", "String", data_ar.Rows[0]["MA_PHIEU"].ToString());
                table.Rows.Add("giobb", "String", data_ar.Rows[0]["GIOBB"].ToString());
                table.Rows.Add("phutbb", "String", data_ar.Rows[0]["NGAYBB"].ToString());
                table.Rows.Add("chutichhd", "String", data_ar.Rows[0]["CHUTICHHD"].ToString());
                table.Rows.Add("cdchutichhd", "String", data_ar.Rows[0]["CDCHUTICHHD"].ToString());
                 
                table.Rows.Add("thuki", "String", data_ar.Rows[0]["THUKI"].ToString());
                table.Rows.Add("cdthuki", "String", data_ar.Rows[0]["CDTHUKI"].ToString());
                table.Rows.Add("truongphongtckt", "String", data_ar.Rows[0]["TRUONGPHONGTCKT"].ToString());
                table.Rows.Add("cdtruongphongtckt", "String", data_ar.Rows[0]["CDTRUONGPHONGTCKT"].ToString());

                table.Rows.Add("truongkhoaduoc", "String", data_ar.Rows[0]["TRUONGKHOADUOC"].ToString());
                table.Rows.Add("cdtruongkhoaduoc", "String", data_ar.Rows[0]["CDTRUONGKHOADUOC"].ToString());

                table.Rows.Add("thongkeduoc", "String", data_ar.Rows[0]["THONGKEDUOC"].ToString());
                table.Rows.Add("cdthongkeduoc", "String", data_ar.Rows[0]["CDTHONGKEDUOC"].ToString());

                table.Rows.Add("ykiendexuat", "String", data_ar.Rows[0]["YKIENDEXUAT"].ToString());
                table.Rows.Add("taitro", "String", data_ar.Rows[0]["TAITRO"].ToString());
                table.Rows.Add("tinhtrang", "String", data_ar.Rows[0]["TINHTRANG"].ToString());
                 
                table.Rows.Add("nhapxuatid", "String", data_ar.Rows[0]["NHAPXUATID"].ToString());

                VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC013_BBXACNHANTHUOCHOACHATVTYTMATHONGVO_14DBV01_TT22_A4", "pdf", 720, 1200);
                openForm(frm); 
            } 
        }

        private void print_6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_6").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC069_PHIEUTRATHUOC_A4", "pdf", 720, 1200);
            openForm(frm);  
        }

        private void print_7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_7").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC071_PHIEUTRAVATTU_A4", "pdf", 720, 1200);
            openForm(frm);
        }

        private void print_8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_8").on("click", function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC075_HOADONBANLE_A4", "pdf", 720, 1200);
            openForm(frm); 
        }

        private void print_9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //$("#toolbarIdprint_9").on("click", function(e){
            //    var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
            //    var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
            //    _print(_id, '13');
            //});
            DataRowView row;
            if (getRowFocus(out row) == false) return;


            //else if (_loaiphieu == 13)//phieu xuat	huy	

            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC01S001.04", row["NHAPXUATID"].ToString());
            if (data_ar.Rows.Count == 0) return;

            string tien_chu = Func.Doc_So_Thanh_Chu(data_ar.Rows[0]["TONG_TIEN"].ToString(), "");
            int _no = Func.Parse(data_ar.Rows[0]["NO"].ToString());  // format(0, 3, ',');
            int _co = Func.Parse(data_ar.Rows[0]["CO"].ToString()); //format(0, 3, ',');

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");

            table.Rows.Add("khoa", "String", data_ar.Rows[0]["KHOA"].ToString());
            table.Rows.Add("soPhieu", "String", data_ar.Rows[0]["SOPHIEU"].ToString());
            table.Rows.Add("co", "String", _co);
            table.Rows.Add("no", "String", _no);
            table.Rows.Add("nguoi_giao", "String", data_ar.Rows[0]["NGUOI_GIAO"].ToString());
            table.Rows.Add("donViNhan", "String", data_ar.Rows[0]["DONVINHAN"].ToString());
            table.Rows.Add("lyDoXuat", "String", data_ar.Rows[0]["LYDOXUAT"].ToString());
            table.Rows.Add("khoXuat", "String", data_ar.Rows[0]["KHOXUAT"].ToString());
            table.Rows.Add("tienBangChu", "String", data_ar.Rows[0]["TIEN_BANG_CHU"].ToString());
            table.Rows.Add("soChungTu", "String", data_ar.Rows[0]["SO_CHUNG_TU"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_PHIEUXUATKHO_KIEMVANCHUYEN_NB", "pdf", 720, 1200);
            openForm(frm);
        }

        private void print_10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdprint_10").on("click", function(e){ 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_PHIEUHOANTRA_NHQNM", "pdf", 720, 1200);
            openForm(frm);
        }

        private void print_11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //$("#toolbarIdprint_11").on("click", function(e){ 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "BC_BANGYLENH_THUOCVATTU_HANGNGAY_996", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbInPhieuDuTru_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdtbInPhieuDuTru").on("click", function(e){ 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_PHIEU_DU_TRU", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbphieutrathuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //       $("#toolbarIdtbphieutrathuoc").on("click", function(e){ 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC004_PHIEUTRATHUOCKHACHLE", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbBBKiemNhapHoaDon_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbBBKiemNhapHoaDon").on("click", function(e){ 

            //            var rpName = "DUC017_BBKIEMKETTHUOCVATTUTIEUHAO_NHIQUANGNGAI_A4" + jsonrpc.AjaxJson.getSystemDate('DDMMYY-HH24MISS') + "." + 'xls';
            //            CommonUtil.inPhieu('window', "DUC017_BBKIEMKETTHUOCVATTUTIEUHAO_NHIQUANGNGAI_A4", 'xls', par, rpName);

            //            var rpNameword = "DUC017_BBKIEMKETTHUOCVATTUTIEUHAO_NHIQUANGNGAI_A4" + jsonrpc.AjaxJson.getSystemDate('DDMMYY-HH24MISS') + "." + 'rtf';
            //            CommonUtil.inPhieu('window', "DUC017_BBKIEMKETTHUOCVATTUTIEUHAO_NHIQUANGNGAI_A4", 'rtf', par, rpNameword);
             
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC017_BBKIEMKETTHUOCVATTUTIEUHAO_NHIQUANGNGAI_A4", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbInPhieuLinhTVTHaoPhi2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInPhieuLinhTVTHaoPhi2Lien").on("click", function(e) { 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_LINHTHUOCVATTUHAOPHITHEOKHOAPHONG_A4_957", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbInLinhTVTBoSungCoSoTuTruc2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInLinhTVTBoSungCoSoTuTruc2Lien").on("click", function(e) { 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_LINHTHUOCVATTUBOSUNGCOSOTUTRUC_A4_957", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbInHoanTraCoSoTuTruc2Lien_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInHoanTraCoSoTuTruc2Lien").on("click", function(e) {

            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_PHIEUHOANTRACOSOTUTRUC_A4_957", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbInNhapXuatThuocTuNhaThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInNhapXuatThuocTuNhaThuoc").on("click", function(e) { 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_NHAPXUATTHUOCTUNHATHUOC_BDHCM", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbInBienBanTraThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         $("#toolbarIdtbInBienBanTraThuoc").on("click", function(e) {
            DataRowView row;
            if (getRowFocus(out row) == false) return;
             
            DataTable jsonLoaiTVT = RequestHTTP.get_ajaxExecuteQueryO("DUC_LOAITVT.05", row["NHAPXUATID"].ToString());
            if (jsonLoaiTVT.Rows.Count <= 0) return;

            string parLoai = "";
            string thuocThuongArr = "";
            for (int i = 0; i < jsonLoaiTVT.Rows.Count; i++)
            {
                if (jsonLoaiTVT.Rows[i]["LOAI"].ToString() == "6") parLoai += "," + "6";
                else thuocThuongArr += "," + jsonLoaiTVT.Rows[i]["LOAI"].ToString();
            }

            parLoai = parLoai + thuocThuongArr;
            string[] arr = parLoai.Split(',');

            for (int i = 0; i < arr.Length; i++)
                if (arr[i] != "")
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("name");
                    table.Columns.Add("type");
                    table.Columns.Add("value");
                    table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());
                    table.Rows.Add("loaitvt", "String", arr[i]);

                    // var params = [parNhapXuatObj, parLoaiObj];
                    //openReport('window', 'DUC_BIENBANTRATHUOC_TAMTHAN', 'pdf', params);
                    VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_NHAPXUATTHUOCTUNHATHUOC_BDHCM", "pdf", 720, 1200);
                    openForm(frm);
                }
        }

        private void tbInXuatThuocTraKhoChinh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //        $("#toolbarIdtbInXuatThuocTraKhoChinh").on("click", function(e) { 
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", row["NHAPXUATID"].ToString());

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "DUC_XUATTHUOCTRAKHOCHINH_BM2", "pdf", 720, 1200);
            openForm(frm);
        }

        private void tbSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //         var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
            //         var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
            //         var _doiungid = $("#grdPhieu").jqGrid('getRowData', row).DOIUNGID;
            //         EventUtil.setEvent("YCNhap_success", function(e){
            //             DlgUtil.close("dlgYCNhap");
            //             _loadDSPhieu();
            //         });

            //         var ht = 0;
            //         var kieu = $("#grdPhieu").jqGrid('getRowData', row).KIEU;
            //         if (kieu == 2) { ht = 1; } //hoan tra
            //         var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: _id,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht,loaiphieu: that.opt.lp , hoantra: ht};

            //if(PHARMA_GIAODIEN_NHAP_NCC == 'DUC02N001_NhapThuocNCC_BVNT'){
            //	dlgPopup=DlgUtil.buildPopupUrl("dlgYCNhap","divDlg","manager.jsp?func=../duoc/DUC11N001_NGT_YeuCauNhapThuoc",myVar,"Yêu cầu dự trù" + that.opt.title,1200,610);
            //	DlgUtil.open("dlgYCNhap");
            //}
            //else{
            //	dlgPopup=DlgUtil.buildPopupUrl("dlgYCNhap","divDlg","manager.jsp?func=../duoc/DUC11N001_YeuCauNhapThuoc",myVar,"Yêu cầu dự trù" + that.opt.title,1200,610);
            //	DlgUtil.open("dlgYCNhap");
            //}

        }

        private void tbSuaNCC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
//            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
//            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
//            var _kieu = $("#grdPhieu").jqGrid('getRowData', row).KIEU;
//            var _trangthaiid = $("#grdPhieu").jqGrid('getRowData', row).TRANGTHAIID;
//            var _sochungtu = $("#grdPhieu").jqGrid('getRowData', row).SOCHUNGTU;
//            // xac dinh phieu tra theo phieu nhap hay phieu tra theo lô 
//            var _xuatid = $("#grdPhieu").jqGrid('getRowData', row).XUATID;
//            if (_kieu == 0)
//            {
//                EventUtil.setEvent("nhapkho_success", function(e){
//                    DlgUtil.close("dlgNhapKho");
//                    _loadDSPhieu();
//                });
//                EventUtil.setEvent("nhapkho_cancel", function(e){
//                    DlgUtil.close("dlgNhapKho");
//                    _loadDSPhieu();
//                });
//                var _title = "";
//                if (_trangthaiid == "4")
//                {
//                    _title = "Sửa phiếu" + that.opt.title + " đã Nhập Kho";
//                }
//                else
//                {
//                    _title = "Sửa phiếu" + that.opt.title + " đã tạo";
//                }

//                if (that.opt.lk == '4' && PHARMA_BC_NHAP_KHO == 965)
//                {
//                    var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,trangthai: _trangthaiid,loaiphieu: _kieu,edit: that.opt.cs};
//                dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC_NhaThuoc_BVBD", myVar, "Sửa phiếu nhập kho nhà thuốc", 1210, 600);
//                DlgUtil.open("dlgNhapKho");

//            }
//            else if (that.opt.lk == '4')
//            {
//                var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,trangthai: _trangthaiid,loaiphieu: _kieu,edit: that.opt.cs};
//            dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC_NhaThuoc", myVar, "Sửa phiếu nhập kho nhà thuốc", 1200, 580);
//            DlgUtil.open("dlgNhapKho");

//        }
//			   else if(PHARMA_GIAODIEN_NHAP_NCC == 0){
//						//var myVar={khoid:$("#cboKho").val(),nhapxuatid:0,tenkho:$("#cboKho option:selected").text(),kieu:that.opt.gd,edit:that.opt.cs};
//						var myVar = { khoid:$("#cboKho").val(), nhapxuatid:_id, tenkho:$("#cboKho option:selected").text(), kieu:that.opt.gd, trangthai:_trangthaiid, loaiphieu:_kieu, edit:that.opt.cs };
//        dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC",myVar,_title,1200,600);
//						  DlgUtil.open("dlgNhapKho");
//					}			  
//			else{
//				var myVar = { khoid:$("#cboKho").val(), nhapxuatid:_id, tenkho:$("#cboKho option:selected").text(), kieu:that.opt.gd, trangthai:_trangthaiid, loaiphieu:_kieu, edit:that.opt.cs, ht:that.opt.ht };
//    dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/" + PHARMA_GIAODIEN_NHAP_NCC,myVar,_title,1210,610);
//				DlgUtil.open("dlgNhapKho");
//				// san nhi ham nam
//			/*	var myVar={khoid:$("#cboKho").val(),nhapxuatid:_id,tenkho:$("#cboKho option:selected").text(),kieu:that.opt.gd,trangthai:_trangthaiid,loaiphieu:_kieu,edit:that.opt.cs};
//				dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC",myVar,_title,1200,600);
//				DlgUtil.open("dlgNhapKho");*/
//			}
//			}
//			else if (_kieu==1 &&  _sochungtu != ''){
//				EventUtil.setEvent("xuattra_success",function(e)
//{
//    DlgUtil.close("dlgXuatTra");
//    _loadDSPhieu();
//});
//				EventUtil.setEvent("xuattra_cancel",function(e)
//{
//    DlgUtil.close("dlgXuatTra");
//    _loadDSPhieu();
//});
//				var myVar = { khoid:$("#cboKho").val(), nhapxuatid:_id, tenkho:$("#cboKho option:selected").text(), kieu:that.opt.gd, trangthai:_trangthaiid, sochungtu:_sochungtu };
//dlgPopup=DlgUtil.buildPopupUrl("dlgXuatTra","divDlg","manager.jsp?func=../duoc/DUC03X001_XuatTraThuocNCC",myVar,"Sửa phiếu xuất trả"+that.opt.title ,1200,600);
//				DlgUtil.open("dlgXuatTra");
//			}
			
//			else if (_kieu==1 && _sochungtu == ''){
//				var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
//var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
//				var _khoid = $("#grdPhieu").jqGrid('getRowData', row).KHOID;
//				EventUtil.setEvent("YCNhap_success",function(e)
//{
//    DlgUtil.close("dlgYCNhap");
//    _loadDSPhieu();
//});
				
				
//				var myVar = { loaiKhoId:$("#cboKho").val(), nhapxuatid:_id, loaiGiaoDien:that.opt.gd, hinhThucId:"1", loaiphieu:"1", trangthai:_trangthaiid, loainhapbu:"XUATTRANCC_TK" };
//dlgPopup=DlgUtil.buildPopupUrl("dlgYCNhap","divDlg","manager.jsp?func=../duoc/DUC11N001_YeuCauNhapThuoc",myVar,"Sửa xuất trả "+ that.opt.title +" tồn kho NCC",1200,610);
//				DlgUtil.open("dlgYCNhap");
//			}
        }

        private void tbSuaNhapBu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //        var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //        EventUtil.setEvent("nhapkho_success", function(e){
    //            DlgUtil.close("dlgNhapKho");
    //            _loadDSPhieu();
				//$("#toolbarIdtbSuaNhapBu").prop("disabled", true);
    //        });
    //        EventUtil.setEvent("nhapkho_cancel", function(e){
    //            DlgUtil.close("dlgNhapKho");
    //            _loadDSPhieu();
				//$("#toolbarIdtbSuaNhapBu").prop("disabled", true);
    //        });
    //        var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_NHAPBU"};
    //    dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/"+ PHARMA_GIAODIEN_NHAP_NCC,myVar,"Nhập bù" + that.opt.title ,1210,610);
				//  DlgUtil.open("dlgNhapKho"); 
			
        }

        private void tbSuaXuatHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
   //         var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
   //         var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
   //         EventUtil.setEvent("nhapkho_success", function(e){
   //             DlgUtil.close("dlgNhapKho");
   //             _loadDSPhieu();
			//	$("#toolbarIdtbSuaXuatHuy").prop("disabled", true);
   //         });
   //         EventUtil.setEvent("nhapkho_cancel", function(e){
   //             DlgUtil.close("dlgNhapKho");
   //             _loadDSPhieu();
			//	$("#toolbarIdtbSuaXuatHuy").prop("disabled", true);
   //         });
   //         var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATHUY"};
			//if(that.opt.gd=='XUATHUHAOCB')
			//	dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/DUC02N001_HuHaoCheBien",myVar,"YC tính hư hao chế biến" ,1200,600);
			//else 
			//	dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/"+ PHARMA_GIAODIEN_NHAP_NCC,myVar,"Xuất hủy" + that.opt.title ,1210,610);
			//DlgUtil.open("dlgNhapKho");
        }

        private void tbSuaXuatThieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
   //         var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
   //         var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
   //         EventUtil.setEvent("nhapkho_success", function(e){
   //             DlgUtil.close("dlgNhapKho");
   //             _loadDSPhieu();
			//	$("#toolbarIdtbSuaXuatThieu").prop("disabled", true);
   //         });
   //         EventUtil.setEvent("nhapkho_cancel", function(e){
   //             DlgUtil.close("dlgNhapKho");
   //             _loadDSPhieu();
			//	$("#toolbarIdtbSuaXuatThieu").prop("disabled", true);
   //         });
   //         var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATTHIEU"};
   //     dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC",myVar,"Xuất thiếu" + that.opt.title ,1200,600); 
			//DlgUtil.open("dlgNhapKho");
        }

        private void tbSuaDTTH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
   //         var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
   //         var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
   //         EventUtil.setEvent("nhapkho_success", function(e){
   //             DlgUtil.close("dlgNhapKho");
   //             _loadDSPhieu();
			//	$("#toolbarIdtbSuaDTTH").prop("disabled", true);
   //         });
   //         EventUtil.setEvent("nhapkho_cancel", function(e){
   //             DlgUtil.close("dlgNhapKho");
   //             _loadDSPhieu();
			//	$("#toolbarIdtbSuaDTTH").prop("disabled", true);
   //         });
   //         var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATDTTHKP"};
   //     dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC",myVar,"Sửa dự trù tiêu hao khoa phòng" + that.opt.title ,1200,600);
			//DlgUtil.open("dlgNhapKho");
        }

        private void btnXemPhieu_Click(object sender, EventArgs e)
        {
            //$("#btnXemPhieu").on("click",function(e){
            DataRowView row;
            if (getRowFocus(out row) == false) return;

            string _id;

            if ((this.type == "45" || this.type == "4") && row["KIEU"].ToString() == "2")
                _id = row["NHAPID"].ToString();
            else _id = row["XUATID"].ToString();

            //EventUtil.setEvent("nhapkho_cancel2", function(e){
            //    DlgUtil.close("dlgTTPhieu"); 
            //}); 
            subform.DUC35T001_ThongTinPhieu frm = new subform.DUC35T001_ThongTinPhieu(
                _id,
                row["NHAPXUATID"].ToString(),
                row["KIEU"].ToString()
                );
            openForm(frm);

            //dlgPopup=DlgUtil.buildPopupUrl("dlgTTPhieu","divDlg","manager.jsp?func=../duoc/DUC35T001_ThongTinPhieu",myVar,"Thông tin phiếu",1200,580);

        }

        private void tbDuyetHoanTra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbDuyetHoanTra").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            EventUtil.setEvent("appr_success", function(e){
    //                DlgUtil.close("dlgAppr");
				//$('#grdPhieu').jqGrid('setCell', row, 7, 'Đã duyệt');
				//$('#grdPhieu').jqGrid('setCell', row, 3, DADUYET);
    //                _loadChiTiet(_id, DADUYET);
    //                _formatRow(row, DADUYET - 1);
    //            });
    //            var myVar = { nhapxuatId:_id};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgAppr", "divDlg", "manager.jsp?func=../duoc/DUC47T001_YeuCauNhapThuoc", myVar, "Duyệt phiếu", 1100, 550);
    //        DlgUtil.open("dlgAppr");
    //    });
        }


        private void tbTaoPhieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //$("#toolbarIdtbTaoPhieu").on("click", function(e){
            //    var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
            //    var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
            //    var _kieu = $("#grdPhieu").jqGrid('getRowData', row).KIEU;
            //    var sql_par;
            //    if (_kieu == 2)
            //        sql_par =['DUC47T001', _id, 'NHAP'];
            //    else
            //        sql_par =['DUC47T001', _id, 'XUAT'];
            //    var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
            //    if (ret == 0)
            //    {
            //        DlgUtil.showMsg("Phiếu đã tạo");
            //        return;
            //    }
            //    ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC47T001.CREATE", _id);
            //    if (ret == 1)
            //    {
            //        DlgUtil.showMsg("Tạo phiếu thành công");
            //    }
            //    else
            //    {
            //        DlgUtil.showMsg("Tạo phiếu không thành công");
            //    }
            //});
        }

        private void tbYCNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbYCNhap").on("click", function(e){
        //        EventUtil.setEvent("YCNhap_success", function(e){
        //            DlgUtil.close("dlgYCNhap");
        //            _loadDSPhieu();
        //        });
        //        var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: 0,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht};
        //    if (PHARMA_GIAODIEN_NHAP_NCC == 'DUC02N001_NhapThuocNCC_BVNT')
        //    {
        //        dlgPopup = DlgUtil.buildPopupUrl("dlgYCNhap", "divDlg", "manager.jsp?func=../duoc/DUC11N001_NGT_YeuCauNhapThuoc", myVar, "Yêu cầu dự trù" + that.opt.title, 1200, 610);
        //        DlgUtil.open("dlgYCNhap");
        //    }
        //    else
        //    {
        //        dlgPopup = DlgUtil.buildPopupUrl("dlgYCNhap", "divDlg", "manager.jsp?func=../duoc/DUC11N001_YeuCauNhapThuoc", myVar, "Yêu cầu dự trù" + that.opt.title, 1200, 610);
        //        DlgUtil.open("dlgYCNhap");
        //    }

        //});
        }

        private void tbHoanTra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbHoanTra").on("click", function(e){
        //        EventUtil.setEvent("YCNhap_success", function(e){
        //            DlgUtil.close("dlgYCNhap");
        //            _loadDSPhieu();
        //        });
        //        var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: 0,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht,loaiphieu: that.opt.lp,hoantra: '1'};
        //    if (PHARMA_GIAODIEN_NHAP_NCC == 'DUC02N001_NhapThuocNCC_BVNT')
        //    {
        //        dlgPopup = DlgUtil.buildPopupUrl("dlgYCNhap", "divDlg", "manager.jsp?func=../duoc/DUC11N001_NGT_YeuCauNhapThuoc", myVar, "Yêu cầu dự trù" + that.opt.title, 1200, 610);
        //        DlgUtil.open("dlgYCNhap");
        //    }
        //    else
        //    {
        //        dlgPopup = DlgUtil.buildPopupUrl("dlgYCNhap", "divDlg", "manager.jsp?func=../duoc/DUC11N001_YeuCauNhapThuoc", myVar, "Yêu cầu hoàn trả" + that.opt.title, 1200, 610);
        //        DlgUtil.open("dlgYCNhap");
        //    }
        //});
        }

        private void btnNhapKho_Click(object sender, EventArgs e)
        {
   //         $("#btnNhapKho").on("click", function(e){
   //             var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
   //             var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
   //             var _doiungid = $("#grdPhieu").jqGrid('getRowData', row).DOIUNGID;
   //             var sql_par =['DUC57N001', _id, 'NHAPKHO'];
   //             var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
   //             if (ret == 0)
   //             {
   //                 DlgUtil.showMsg("Phiếu đã nhập kho");
   //                 return;
   //             }
   //             EventUtil.setEvent("NhapKho_success", function(e){
   //                 DlgUtil.close("dlgNhapKho");
   //                 _loadDSPhieu();

   //             });
   //             if ($("#cboKho").val() == _doiungid){
   //                 DlgUtil.showMsg("Phiếu đang thuộc kho cung ứng. Không được nhập kho");
   //             }
			//else{
   //                 var myVar = { nhapxuatid:_id,loaiGiaoDien: that.opt.gd};
   //             dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC57N001_NhapThuocTKK", myVar, "Nhập kho" + that.opt.title, 1000, 500);
   //             DlgUtil.open("dlgNhapKho");
   //         }
   //     });
        }

        private void btnXuatTra_Click(object sender, EventArgs e)
        {
   //         $("#btnXuatTra").on("click", function(e){
   //             var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
   //             var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
   //             var _xuatId = $("#grdPhieu").jqGrid('getRowData', row).XUATID;
   //             var _doiungid = $("#grdPhieu").jqGrid('getRowData', row).DOIUNGID;
   //             var sql_par =['DUC58X001', _id, 'XUATTRA'];
   //             var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
   //             if (ret == 0)
   //             {
   //                 DlgUtil.showMsg("Phiếu đã xuất trả");
   //                 return;
   //             }
   //             EventUtil.setEvent("XuatTra_success", function(e){
   //                 DlgUtil.close("dlgXuatTra");
   //                 _loadDSPhieu();
   //             });
   //             if ($("#cboKho").val() == _doiungid){
   //                 DlgUtil.showMsg("Khong duoc xuat tra");
   //             }
			//else{
   //                 var myVar = { nhapxuatid:_xuatId,loaiGiaoDien: that.opt.gd};
   //             dlgPopup = DlgUtil.buildPopupUrl("dlgXuatTra", "divDlg", "manager.jsp?func=../duoc/DUC58X001_XuatTraThuocChoKhoKhac", myVar, "Xuất trả" + that.opt.title, 1000, 500);
   //             DlgUtil.open("dlgXuatTra");
   //         }
   //     });
        }

        private void tbYCXuat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
   //         $("#toolbarIdtbYCXuat").on("click", function(e){
   //             var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
   //             var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
   //             var _doiungid = $("#grdPhieu").jqGrid('getRowData', row).DOIUNGID;
   //             var _nhapId = $("#grdPhieu").jqGrid('getRowData', row).NHAPID;

   //             if (_nhapId != 0 && _nhapId != "")
   //             {
   //                 EventUtil.setEvent("YCXuat_success", function(e){
   //                     DlgUtil.close("dlgYCXuat");
   //                     _loadDSPhieu();
   //                 });
   //                 var myVar =
   //                         {loaiKhoId:$("#cboKho").val(),
			//			hinhThucId: that.opt.ht,
			//			nhapxuatid: _nhapId,
			//			loaiGiaoDien: that.opt.gd};
   //             dlgPopup = DlgUtil.buildPopupUrl("dlgYCXuat", "divDlg", "manager.jsp?func=../duoc/DUC30X001_KhoaPhongYCNhapTraThuoc", myVar, "Yêu cầu hoàn trả" + that.opt.title, 1000, 500);
   //             DlgUtil.open("dlgYCXuat");
   //         }
			//else{
   //             DlgUtil.showMsg("Phiếu chưa nhập kho");
   //             return;
   //         }
   //     });
        }

        private void tbNhapKhoNCC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
//            $("#toolbarIdtbNhapKhoNCC").on("click", function(e){
//                EventUtil.setEvent("nhapkho_success", function(e){
//                    DlgUtil.close("dlgNhapKho");
//                    _loadDSPhieu();
//                });
//                EventUtil.setEvent("nhapkho_cancel", function(e){
//                    DlgUtil.close("dlgNhapKho");
//                    _loadDSPhieu();
//                });
//                if (that.opt.lk == '4' && PHARMA_BC_NHAP_KHO == 965)
//                {
//                    var myVar = { khoid:$("#cboKho").val(),nhapxuatid: 0,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,edit: that.opt.cs};
//                dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC_NhaThuoc_BVBD", myVar, "Nhập kho từ NCC vào nhà thuốc", 1210, 600);
//                DlgUtil.open("dlgNhapKho");

//            }
//			else if (that.opt.lk == '4')
//            {
//                var myVar = { khoid:$("#cboKho").val(),nhapxuatid: 0,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,edit: that.opt.cs};
//            dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC_NhaThuoc", myVar, "Nhập kho từ NCC vào nhà thuốc", 1200, 580);
//            DlgUtil.open("dlgNhapKho");

//        }else if(PHARMA_GIAODIEN_NHAP_NCC == 0){
//				var myVar = { khoid:$("#cboKho").val(), nhapxuatid:0, tenkho:$("#cboKho option:selected").text(), kieu:that.opt.gd, edit:that.opt.cs };
//        dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC",myVar,"Nhập kho từ NCC" + that.opt.title ,1200,600);
//				  DlgUtil.open("dlgNhapKho");
//			}else if(that.opt.lt == '4'){
//				var myVar = { khoid:$("#cboKho").val(), nhapxuatid:0, tenkho:$("#cboKho option:selected").text(), kieu:that.opt.gd, edit:that.opt.cs, ht:that.opt.ht, lt:that.opt.lt };
//    dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/DUC_Mau_NhapMauNCC",myVar,"Nhập kho từ NCC máu",1210,610);
//				  DlgUtil.open("dlgNhapKho");
//			}
//			else{
//				var myVar = { khoid:$("#cboKho").val(), nhapxuatid:0, tenkho:$("#cboKho option:selected").text(), kieu:that.opt.gd, edit:that.opt.cs, ht:that.opt.ht };
//dlgPopup=DlgUtil.buildPopupUrl("dlgNhapKho","divDlg","manager.jsp?func=../duoc/"+ PHARMA_GIAODIEN_NHAP_NCC,myVar,"Nhập kho từ NCC" + that.opt.title ,1210,610);
//			  DlgUtil.open("dlgNhapKho"); 
//			} 
//		});
        }

        private void tbXuatTraNCC_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbXuatTraNCC").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            EventUtil.setEvent("xuattra_success", function(e){
    //                DlgUtil.close("dlgXuatTra");
    //                _loadDSPhieu();
				//$('#grdPhieu').jqGrid('setCell', row, 7, 'Tạo mới');
				//$('#grdPhieu').jqGrid('setCell', row, 3, TAOMOI);
    //                _loadChiTiet(_id, TAOMOI);
    //                _formatRow(row, TAOMOI - 1);
    //            });
    //            EventUtil.setEvent("xuattra_cancel", function(e){
    //                DlgUtil.close("dlgXuatTra");
    //                _loadDSPhieu();
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgXuatTra", "divDlg", "manager.jsp?func=../duoc/DUC03X001_XuatTraThuocNCC", myVar, "Xuất trả NCC" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgXuatTra");
    //    });
        }

        private void tbXuatTraNCC_TK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbXuatTraNCC_TK").on("click", function(e){
        //        EventUtil.setEvent("YCNhap_success", function(e){
        //            DlgUtil.close("dlgYCNhap");
        //            _loadDSPhieu();
        //        });
        //        var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: 0,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht,loainhapbu: "XUATTRANCC_TK"};
        //    dlgPopup = DlgUtil.buildPopupUrl("dlgYCNhap", "divDlg", "manager.jsp?func=../duoc/DUC11N001_YeuCauNhapThuoc", myVar, "Xuất trả " + that.opt.title + " tồn kho NCC", 1200, 610);
        //    DlgUtil.open("dlgYCNhap");
        //});
        }

        private void tbYCNhapBu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbYCNhapBu").on("click", function(e){
    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaNhapBu").prop("disabled", true);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaNhapBu").prop("disabled", true);
    //            });
    //            var _loainhapbu = 'YC_NHAPBU';
    //            var _text = "Yêu cầu nhập bù" + that.opt.title;
    //            if (that.opt.gd == 'NHAPKHACTHUOC' || that.opt.gd == 'NHAPKHACVATTU')
    //            {
    //                _loainhapbu = 'YC_NHAPKHAC'; _text = 'Yêu cầu nhập khác'
    
    //                }
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: 0,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: _loainhapbu};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/" + PHARMA_GIAODIEN_NHAP_NCC, myVar, _text, 1210, 610);
    //        DlgUtil.open("dlgNhapKho");

    //    });
        }

        private void tbNhapBu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbNhapBu").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            var sql_par =['DUC02N001', _id, 'NHAP'];
    //            var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
    //            if (ret == 0)
    //            {
    //                DlgUtil.showMsg("Đã tạo phiếu nhập bù. Không tiếp tục tạo phiếu nhập bù được");
    //                return;
    //            }

    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
				//$('#grdPhieu').jqGrid('setCell', row, 7, 'Đã kết thúc');
				//$('#grdPhieu').jqGrid('setCell', row, 3, KETTHUC);
    //                _loadChiTiet(_id, KETTHUC);
    //                _formatRow(row, KETTHUC - 1);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "NHAPBU"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/" + PHARMA_GIAODIEN_NHAP_NCC, myVar, "Nhập bù" + that.opt.title, 1210, 610);
    //        DlgUtil.open("dlgNhapKho");

    //    });
        }

        private void tbYCXuatHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbYCXuatHuy").on("click", function(e){
    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatHuy").prop("disabled", true);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatHuy").prop("disabled", true);
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: 0,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATHUY"};
    //        if (that.opt.gd == 'XUATHUHAOCB')
    //            dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_HuHaoCheBien", myVar, "YC tính hư hao chế biến", 1200, 600);
    //        else
    //            dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/" + PHARMA_GIAODIEN_NHAP_NCC, myVar, "YC Xuất hủy" + that.opt.title, 1210, 610);

    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbXuatHuy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbXuatHuy").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            var sql_par =['DUC02N001', _id, 'XUAT'];
    //            var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
    //            if (ret == 0)
    //            {
    //                DlgUtil.showMsg("Đã tạo phiếu xuất hủy. Không tiếp tục tạo phiếu xuất hủy được");
    //                return;
    //            }

    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
				//$('#grdPhieu').jqGrid('setCell', row, 7, 'Đã kết thúc');
				//$('#grdPhieu').jqGrid('setCell', row, 3, KETTHUC);
    //                _loadChiTiet(_id, KETTHUC);
    //                _formatRow(row, KETTHUC - 1);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "XUATHUY"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/" + PHARMA_GIAODIEN_NHAP_NCC, myVar, "Xuất hủy" + that.opt.title, 1210, 610);
    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbYCXuatKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbYCXuatKhac").on("click", function(e){
    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatKhac").prop("disabled", true);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatKhac").prop("disabled", true);
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: 0,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATKHAC"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC", myVar, "YC Xuất khác" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbXuatKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbXuatKhac").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            var sql_par =['DUC02N001', _id, 'XUAT'];
    //            var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
    //            if (ret == 0)
    //            {
    //                DlgUtil.showMsg("Đã tạo phiếu xuất khác. Không tiếp tục tạo phiếu xuất khác được");
    //                return;
    //            }

    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
				//$('#grdPhieu').jqGrid('setCell', row, 7, 'Đã kết thúc');
				//$('#grdPhieu').jqGrid('setCell', row, 3, KETTHUC);
    //                _loadChiTiet(_id, KETTHUC);
    //                _formatRow(row, KETTHUC - 1);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "XUATKHAC"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC", myVar, "Xuất khác" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbSuaXuatKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbSuaXuatKhac").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatKhac").prop("disabled", true);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatKhac").prop("disabled", true);
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATKHAC"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC", myVar, "Xuất khác" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgNhapKho");
    //    }
    }

        private void tbYCXuatThieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbYCXuatThieu").on("click", function(e){
    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatThieu").prop("disabled", true);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaXuatThieu").prop("disabled", true);
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: 0,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATTHIEU"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC", myVar, "YC Xuất thiếu" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbXuatThieu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbXuatThieu").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            var sql_par =['DUC02N001', _id, 'XUAT'];
    //            var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
    //            if (ret == 0)
    //            {
    //                DlgUtil.showMsg("Đã tạo phiếu xuất thiếu. Không tiếp tục tạo phiếu xuất thiếu được");
    //                return;
    //            }
    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
				//$('#grdPhieu').jqGrid('setCell', row, 7, 'Đã kết thúc');
				//$('#grdPhieu').jqGrid('setCell', row, 3, KETTHUC);
    //                _loadChiTiet(_id, KETTHUC);
    //                _formatRow(row, KETTHUC - 1);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "XUATTHIEU"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC", myVar, "Xuất thiếu" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbBanThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbBanThuoc").on("click", function(e){
        //        EventUtil.setEvent("banthuocbn", function(e){
        //            DlgUtil.close("dlgbanthuoc");
        //            _loadDSPhieu();
        //        });
        //        var myVar = { khoid:that.opt.lk};
        //    dlgPopup = DlgUtil.buildPopupUrl("dlgbanthuoc", "divDlg",
        //            "manager.jsp?func=../duoc/DUC34X001_BanThuocChoKhachLe", myVar, "Bán thuốc", 1200, 580);

        //    dlgPopup.open("dlgbanthuoc");
        //});
        }

        private void tbSuaPhieuBanTHuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbSuaPhieuBanTHuoc").on("click", function(e){
        //        var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
        //        var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
        //        EventUtil.setEvent("banthuocbn", function(e){
        //            DlgUtil.close("dlgbanthuoc");
        //            _loadDSPhieu();
        //        });
        //        var myVar = { khoid:that.opt.lk,nhapxuatid: _id};
        //    dlgPopup = DlgUtil.buildPopupUrl("dlgbanthuoc", "divDlg",
        //            "manager.jsp?func=../duoc/DUC34X001_BanThuocChoKhachLe", myVar, "Bán thuốc", 1200, 580);

        //    dlgPopup.open("dlgbanthuoc");
        //});
        }

        private void tbTraThuoc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbTraThuoc").on("click", function(e){
        //        var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
        //        var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
        //        var _xuatid = $("#grdPhieu").jqGrid('getRowData', row).XUATID;

        //        EventUtil.setEvent("trathuoc", function(e){
        //            DlgUtil.close("dlgtrathuoc");
        //            _loadDSPhieu();
        //        });
        //        var myVar = { nhapxuatid:_xuatid};
        //    dlgPopup = DlgUtil.buildPopupUrl("dlgtrathuoc", "divDlg",
        //            "manager.jsp?func=../duoc/DUC34X003_PhieuTraThuoc", myVar, "Trả thuốc khách lẻ", 1200, 580);

        //    dlgPopup.open("dlgtrathuoc");
        //});
        }

        private void tbYCXuatDTTH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbYCXuatDTTH").on("click", function(e){
    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaDTTH").prop("disabled", true);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
				//$("#toolbarIdtbSuaDTTH").prop("disabled", true);
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: 0,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "YC_XUATDTTHKP"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC", myVar, "YC Xuất dự trù tiêu hao khoa phòng" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbXuatDTTH_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
    //        $("#toolbarIdtbXuatDTTH").on("click", function(e){
    //            var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
    //            var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
    //            var sql_par =['DUC02N001', _id, 'XUAT'];
    //            var ret = jsonrpc.AjaxJson.ajaxCALL_SP_I("DUC01S002.CHECK", sql_par.join('$'));
    //            if (ret == 0)
    //            {
    //                DlgUtil.showMsg("Đã tạo phiếu dự trù tiêu hao khoa phòng, Không tiếp tục tạo phiếu được");
    //                return;
    //            }

    //            EventUtil.setEvent("nhapkho_success", function(e){
    //                DlgUtil.close("dlgNhapKho");
				//$('#grdPhieu').jqGrid('setCell', row, 7, 'Đã kết thúc');
				//$('#grdPhieu').jqGrid('setCell', row, 3, KETTHUC);
    //                _loadChiTiet(_id, KETTHUC);
    //                _formatRow(row, KETTHUC - 1);
    //            });
    //            EventUtil.setEvent("nhapkho_cancel", function(e){
    //                DlgUtil.close("dlgNhapKho");
    //                _loadDSPhieu();
    //            });
    //            var myVar = { khoid:$("#cboKho").val(),nhapxuatid: _id,tenkho:$("#cboKho option:selected").text(),kieu: that.opt.gd,loainhapbu: "XUATDTTHKP"};
    //        dlgPopup = DlgUtil.buildPopupUrl("dlgNhapKho", "divDlg", "manager.jsp?func=../duoc/DUC02N001_NhapThuocNCC", myVar, "Xuất dự trù tiêu hao khoa phòng" + that.opt.title, 1200, 600);
    //        DlgUtil.open("dlgNhapKho");
    //    });
        }

        private void tbXuatYLenhLT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbXuatYLenhLT").on("click", function(e){
        //        EventUtil.setEvent("dlgXuatKhac", function(e){
        //            DlgUtil.close("dlgXuatKhac");
        //            _loadDSPhieu();
        //        });
        //        var subdept = _opt.subdept;
        //        var dept = _opt.dept;
        //        var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: 0,khoa_id: dept,phong_id: subdept,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht};

        //    dlgPopup = DlgUtil.buildPopupUrl("dlgXuatKhac", "divDlg", "manager.jsp?func=../duoc/DUC12X001_XuatKhacChoYLenhLinhThuoc", myVar, "Yêu cầu xuất khác cho y lệnh lĩnh thuốc", 1200, 610);
        //    DlgUtil.open("dlgXuatKhac");


        //});
        }

        private void tbHTraYLenhLT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    $("#toolbarIdtbHTraYLenhLT").on("click", function(e){
        //        EventUtil.setEvent("dlgHoanTraYL", function(e){
        //            DlgUtil.close("dlgHoanTraYL");
        //            _loadDSPhieu();
        //        });
        //        var subdept = _opt.subdept;
        //        var dept = _opt.dept;
        //        var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: 0,khoa_id: dept,phong_id: subdept,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht};

        //    dlgPopup = DlgUtil.buildPopupUrl("dlgHoanTraYL", "divDlg", "manager.jsp?func=../duoc/DUC13N001_NhapHoanTraChoYLenhLinhThuoc", myVar, "Yêu cầu hoàn trả cho y lệnh lĩnh thuốc", 1200, 610);
        //    DlgUtil.open("dlgHoanTraYL");


        //});
        }

        private void tbSuaYLenhLT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
   //         $("#toolbarIdtbSuaYLenhLT").on("click", function(e){
   //             var row = $("#grdPhieu").jqGrid('getGridParam', 'selrow');
   //             var _id = $("#grdPhieu").jqGrid('getRowData', row).NHAPXUATID;
   //             var _kieu = $("#grdPhieu").jqGrid('getRowData', row).KIEU;
   //             EventUtil.setEvent("dlgXuatKhac", function(e){
   //                 DlgUtil.close("dlgXuatKhac");
   //                 _loadDSPhieu();
   //             });
   //             EventUtil.setEvent("dlgHoanTraYL", function(e){
   //                 DlgUtil.close("dlgHoanTraYL");
   //                 _loadDSPhieu();
   //             });
   //             var subdept = 0;
   //             var dept = 0;

   //             var sql_par = [_id];
   //             var data_ar = jsonrpc.AjaxJson.ajaxCALL_SP_O("DUC12X001.05", sql_par
   //                     .join('$'));


   //             if (data_ar != null && data_ar.length > 0)
   //             {
   //                 var row = data_ar[0];
   //                 dept = row.KHOAID;
   //                 subdept = row.PHONGID;
   //             }
   //             if (_kieu == 3)
   //             {
   //                 var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: _id,khoa_id: dept,phong_id: subdept,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht};

   //             dlgPopup = DlgUtil.buildPopupUrl("dlgXuatKhac", "divDlg", "manager.jsp?func=../duoc/DUC12X001_XuatKhacChoYLenhLinhThuoc", myVar, "Yêu cầu xuất khác cho y lệnh lĩnh thuốc", 1200, 610);
   //             DlgUtil.open("dlgXuatKhac");
   //         }
			//else{
   //             var myVar = { loaiKhoId:$("#cboKho").val(),nhapxuatid: _id,khoa_id: dept,phong_id: subdept,loaiGiaoDien: that.opt.gd,hinhThucId: that.opt.ht};

   //         dlgPopup = DlgUtil.buildPopupUrl("dlgHoanTraYL", "divDlg", "manager.jsp?func=../duoc/DUC13N001_NhapHoanTraChoYLenhLinhThuoc", myVar, "Yêu cầu hoàn trả cho y lệnh lĩnh thuốc", 1200, 610);
   //         DlgUtil.open("dlgHoanTraYL");

   //     }

   // });
        }



        #endregion

        private void btnInPhieu_Click(object sender, EventArgs e)
        {

        }
    }

    public class KetQuaDto
    {
        public string TENKHO { get; set; }
        public string TUNGAY { get; set; }
        public string DENNGAY { get; set; }
        public string LOAIPHIEU { get; set; }
        public string HINHTHUC { get; set; }
        public string TRANGTHAI { get; set; }
    }
}
