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

namespace VNPT.HIS.VienPhi
{
    public partial class VPI02G001_duyetbhyt4210 : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string VALUE = "value";
        private const string NAME = "name";
        private static string TIEPNHANID;

        private string VP_GUI_DULIEU_KHIDUYET = "0";
        private string VPI_GUI_BH = "0";
        private string VPI_KIEMTRA_TYLE = "0";
        private string HIS_TIMKIEM_VIENPHI = "0";

        public VPI02G001_duyetbhyt4210()
        {
            InitializeComponent();
        }

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }


        private void VPI02G001_duyetbhyt4210_Load(object sender, EventArgs e)
        {
            cboLoaiThe.SelectedIndex = 0;

            ucGridDSBenhNhan.Set_HidePage(false);
            ucGridDSDichVu.Set_HidePage(false);

            DataTable dt_configArr = RequestHTTP.call_ajaxCALL_SP_O("VPI.LAY.CAUHINH", "");
            if (dt_configArr.Rows.Count > 0)
            { 
                VP_GUI_DULIEU_KHIDUYET = dt_configArr.Rows[0]["VP_GUI_DULIEU_KHIDUYET"].ToString();
                VPI_GUI_BH = dt_configArr.Rows[0]["VPI_GUI_BH"].ToString();
                HIS_TIMKIEM_VIENPHI = dt_configArr.Rows[0]["HIS_TIMKIEM_VIENPHI"].ToString();
                VPI_KIEMTRA_TYLE = dt_configArr.Rows[0]["VPI_KIEMTRA_TYLE"].ToString();
            }

            InitCbbTrangThai();
            InitCbbLoaiVP();
            InitDate();
            InitGridDSBN();
            InitGridDSDichVu();
            InitGridDSPhieuThu();
            this.ReadOnly = true;
        }

        private bool ReadOnly
        {
            set
            {
                txtMATIEPNHAN.ReadOnly = value;
                txtMABENHNHAN.ReadOnly = value;
                txtTONGTIENDV.ReadOnly = value;
                txtTENBENHNHAN.ReadOnly = value;
                txtTONGTIENBH.ReadOnly = value;
                txtDIACHI.ReadOnly = value;
                txtMIENGIAMDV.ReadOnly = value;
                txtTUYEN.ReadOnly = value;
                txtBHYT_THANHTOAN.ReadOnly = value;
                txtHINHTHUCVV.ReadOnly = value;
                txtVIENPHI.ReadOnly = value;
                txtMABHYT.ReadOnly = value;
                txtTAMUNG.ReadOnly = value;
                txtDOITUONG.ReadOnly = value;
                txtNGAYRAVIEN.ReadOnly = value;
                txtHOANUNG.ReadOnly = value;
                txtORG_NAME.ReadOnly = value;
                txtDANOP.ReadOnly = value;
                txtNGUOIDUYET.ReadOnly = value;
                cboQUYDUYET.ReadOnly = value;
                txtMIENGIAM.ReadOnly = value;
                txtSTT.ReadOnly = value;
                txtNGAYDUYET.ReadOnly = value;
                txtNOPTHEM.ReadOnly = value;
            }
        }

        private void InitGridDSBN()
        {
            ucGridDSBenhNhan.gridView.OptionsView.ColumnAutoWidth = false;
            ucGridDSBenhNhan.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGridDSBenhNhan.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGridDSBenhNhan.gridView.OptionsBehavior.Editable = false;

            ucGridDSBenhNhan.setEvent(LoadGridDSBenhNhan);
            ucGridDSBenhNhan.setEvent_FocusedRowChanged(ChangedGridDSBenhNhan);
            ucGridDSBenhNhan.setMultiSelectMode(true);
            ucGridDSBenhNhan.SetReLoadWhenFilter(true);
            ucGridDSBenhNhan.onIndicator();
            ucGridDSBenhNhan.setNumberPerPage(new int[] { 100, 200, 300, 400, 500 });
            ucGridDSBenhNhan.gridView.Click += GridView_Click;
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                int index = ucGridDSBenhNhan.gridView.FocusedRowHandle;
                if (!"DX$CheckboxSelectorColumn".Equals(ucGridDSBenhNhan.gridView.FocusedColumn.FieldName))
                {
                    if (ucGridDSBenhNhan.gridView.GetSelectedRows().Any(o => o == index))
                    {
                        ucGridDSBenhNhan.gridView.UnselectRow(index);
                    }
                    else
                    {
                        ucGridDSBenhNhan.gridView.SelectRow(index);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ChangedGridDSBenhNhan(object sender, EventArgs e)
        {
            if (flagLoading) return;
            ucGridDSBenhNhan.setEvent_FocusedRowChanged(null); // bỏ event click, chống việc click liên tiếp

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;

            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataRowView selectedBenhNhan = (DataRowView)sender;
                if (selectedBenhNhan != null)
                {
                    TIEPNHANID = selectedBenhNhan["TIEPNHANID"].ToString();
                    layTTTiepNhan();
                    btnIn.Enabled = true;
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                ucGridDSBenhNhan.setEvent_FocusedRowChanged(ChangedGridDSBenhNhan);
            }
        }
        DataTable dtBenhNhan = new DataTable();
        private void layTTTiepNhan()
        {
            dtBenhNhan = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.11", TIEPNHANID);
            if (dtBenhNhan.Rows.Count > 0)
            {
                if (dtBenhNhan.Rows[0]["TRANGTHAITIEPNHAN4210"].ToString() != "2")
                {
                    btnDuyet.Enabled = true;
                    btnGoDuyet.Enabled = false;
                }
                else
                {
                    btnGoDuyet.Enabled = true;
                    btnDuyet.Enabled = false;
                }

                setObjectToForm_ttVienphi(dtBenhNhan);
                setObjectToForm_ttDuyet(dtBenhNhan);
            }


            LoadDataGridDSDichVu(1);
            LoadDataGridDSPhieuThu(1);

        }
        private void tinhTongTien(DataTable _dvData)
        {  
            DataTable _vp_giaodich = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.06", TIEPNHANID); 
            DataTable _vpData = Func.tinhtien_dv(_dvData, _vp_giaodich);

            if (_vpData.Rows.Count > 0)
            {
                txtTONGTIENDV.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["TONGTIENDV"].ToString());
                txtTONGTIENBH.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["TONGTIENBH"].ToString());
                txtMIENGIAMDV.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["MIENGIAMDV"].ToString());
                txtBHYT_THANHTOAN.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["BHYT_THANHTOAN"].ToString());
                txtVIENPHI.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["VIENPHI"].ToString());

                txtTAMUNG.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["TAMUNG"].ToString());
                txtHOANUNG.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["HOANUNG"].ToString());
                txtDANOP.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["DANOP"].ToString());
                txtMIENGIAM.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["MIENGIAM"].ToString());
                txtNOPTHEM.Text = Func.formatMoneyEng_GiuThapPhan(_vpData.Rows[0]["NOPTHEM"].ToString());

                //TONGTIENDV = _tien_dv
                //, TONGTIENBH = _tong_tien_bh
                //, TYLE_TT = _tong_tien_bh == 0 ? 0 : Math.Round(100 * _bhyt_tra / _tong_tien_bh)
                //, BHYT_THANHTOAN = _bhyt_tra
                //, MIENGIAMDV = _miengiam_dv
                //, VIENPHI = _vienphi
                //, MIENGIAM = miengiam
                //, TAMUNG = tamung
                //, HOANUNG = hoanung
                //, DANOP = danop
                //, DANOP_NGT = danop_ngt
                //, MIENGIAMBH = 0
                //, TAMUNGBH = 0
                //, HOANUNGBH = 0
                //, NOPTHEM = _nopthem
                //, THANHTOAN = _thanhtoan
                //, BHYT_NOPTHEM = _nopthem_bh
                //, BHYT_BNTT = _tien_bhyt_bntt
            }
        }
        private void setObjectToForm_ttVienphi(DataTable dtBenhNhan)
        {
            //KETOAN_DUYET_BH: 0   TIEPNHANID: 96768    MATIEPNHAN: TN000001830 KHAMBENHID_BD:101793    KHAMBENHID_KT: 101793    KHAMBENHID: 101793
            //NHAPNOITRU: 0    TRANGTHAITIEPNHAN: 1 TRANGTHAITIEPNHAN_BH: 0  TRANGTHAITIEPNHAN_VP: 0  LOAITIEPNHANID: 1    BHYT_GIOIHANBHYTTRAHOANTOAN: 195000
            //TYLE_THE: 80 TYLE_TUYEN: 100  MUCHUONG: 100    MABHYT: GD4791300215920(Mức hưởng: 100 %)   THAMGIABHYTDU5NAM: 0 TRADU6THANGLUONGCOBAN: 0 
            //DATHUTIENKHAM: 0 DAGIUTHEBHYT: 1  DATRONVIEN: 0    NGAYTIEPNHAN: 04 / 07 / 2018 10:05:39    NGAYRAVIEN: 04 / 07 / 2018 10:44:00  NGAY_RA_VIEN: 04 / 07 / 2018 NGAYDUYET:
            //NGUOIDUYET: STT: QUYDUYET: BENHNHANID: 13705    DOITUONGBENHNHANID: 1    DOITUONG: BHYT DT_QUANNHAN:0   PHONGID: 4952    KHOAID: 4902
            //HOSOBENHANID: 119060 TRANGTHAIHOSO: 1 TENBENHNHAN: BIỆN THỊ XỨNG MABENHNHAN:BN00011596 MAHOSOBENHAN:701 / 3.8 / 18 / 000068  NOILAMVIEC:
            //DIACHI: 341 / 109 C LạC LONG QUÂN P5 - Q11   TENXA: Chọn TENHUYEN:Quận 11    TENTINH: TP Hồ Chí Minh  TEN_NGUOITHAN: SDTBENHNHAN: 0911496451
            //ORG_NAME: Khoa Khám bệnh TUYEN:Đúng tuyến    HINHTHUCVV: Khoa khám bệnh LYDO_VAOVIEN:1  HINHTHUCVAOVIENID: 3 TRANGTHAITIEPNHAN4210: 1
            txtMATIEPNHAN.Text = dtBenhNhan.Rows[0]["MATIEPNHAN"].ToString();
            txtMABENHNHAN.Text = dtBenhNhan.Rows[0]["MABENHNHAN"].ToString();
            txtTENBENHNHAN.Text = dtBenhNhan.Rows[0]["TENBENHNHAN"].ToString(); 
            txtDIACHI.Text = dtBenhNhan.Rows[0]["DIACHI"].ToString();
            txtTUYEN.Text = dtBenhNhan.Rows[0]["TUYEN"].ToString();

            txtHINHTHUCVV.Text = dtBenhNhan.Rows[0]["HINHTHUCVV"].ToString();
            txtMABHYT.Text = dtBenhNhan.Rows[0]["MABHYT"].ToString();
            txtDOITUONG.Text = dtBenhNhan.Rows[0]["DOITUONG"].ToString();
            try
            {
                txtNGAYRAVIEN.Text = dtBenhNhan.Rows[0]["NGAYRAVIEN"].ToString();
            }
            catch (Exception ex) { }
            txtORG_NAME.Text = dtBenhNhan.Rows[0]["ORG_NAME"].ToString();
        }
        private void setObjectToForm_ttDuyet(DataTable dtBenhNhan)
        {

            txtNGUOIDUYET.Text = dtBenhNhan.Rows[0]["NGUOIDUYET"].ToString();
            cboQUYDUYET.SelectedIndex = Func.Parse(dtBenhNhan.Rows[0]["QUYDUYET"].ToString()) - 1;
            txtSTT.Text = dtBenhNhan.Rows[0]["STT"].ToString();
            try
            {
                txtNGAYDUYET.Text = dtBenhNhan.Rows[0]["NGAYDUYET"].ToString();
            }
            catch (Exception ex) { } 

        }
        private void InitGridDSDichVu()
        {
            ucGridDSDichVu.gridView.OptionsView.ColumnAutoWidth = false;
            ucGridDSDichVu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGridDSDichVu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGridDSDichVu.gridView.OptionsBehavior.Editable = false;
            ucGridDSDichVu.gridView.OptionsView.ShowAutoFilterRow = false;

            ucGridDSDichVu.setEvent(LoadGridDSDichVu);
            LoadGridDSDichVu(null, null);
            //ucGridDSDichVu.setEvent_FocusedRowChanged(LoadGridDSDichVu);
            ucGridDSDichVu.Set_HidePage(true);
        }

        private void InitGridDSPhieuThu()
        {
            ucGridDSPhieuThu.gridView.OptionsView.ColumnAutoWidth = false;
            ucGridDSPhieuThu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGridDSPhieuThu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGridDSPhieuThu.gridView.OptionsBehavior.Editable = false;
            ucGridDSPhieuThu.gridView.OptionsView.ShowAutoFilterRow = false;

            ucGridDSPhieuThu.setEvent(LoadGridDSPhieuThu);
            //ucGridDSBenhNhan.setEvent_FocusedRowChanged(LoadGridKQDichVu);
            ucGridDSPhieuThu.onIndicator();
            ucGridDSPhieuThu.Set_HidePage(true);
        }

        private void InitCbbTrangThai()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(VALUE);
                dt.Columns.Add(NAME);

                DataRow dr = dt.NewRow();

                dr[VALUE] = "10";
                dr[NAME] = "-- Chọn --";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "2";
                dr[NAME] = "Đã đóng bệnh án";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "3";
                dr[NAME] = "Đã duyệt kế toán";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "5";
                dr[NAME] = "Đã duyệt 4210";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "11";
                dr[NAME] = "Đã duyệt 917 chưa duyệt 4210";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "12";
                dr[NAME] = "Đã duyệt 4210 chưa duyệt 917";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "13";
                dr[NAME] = "Đã duyệt 4210 mở bệnh án";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "14";
                dr[NAME] = "Đã duyệt 4210 chưa duyệt KT";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "15";
                dr[NAME] = "Đã duyệt KT chưa duyệt 4210";
                dt.Rows.Add(dr);

                cbbTrangThai.setData(dt, VALUE, NAME);
                cbbTrangThai.setColumn(0, false);
                cbbTrangThai.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitCbbLoaiVP()
        {
            try
            {
                var result = RequestHTTP.call_ajaxCALL_SP_O("LOAIVIENPHI", "$");
                if (result.Rows.Count > 0)
                {
                    //DataRow dr = result.NewRow();
                    //dr["col3"] = string.Empty;
                    //dr["col2"] = "Tất cả";
                    //dr["col1"] = string.Empty;
                    //dr["col4"] = string.Empty;
                    //result.Rows.InsertAt(dr, 0);
                    cbbLoaiVienPhi.setData(result, 0, 1);
                    cbbLoaiVienPhi.setColumn(0, false);
                    cbbLoaiVienPhi.SelectIndex = 0;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitDate()
        {
            txtTU.DateTime = Func.getSysDatetime_Short();
            txtDEN.DateTime = Func.getSysDatetime_Short();
        }

        private void LoadGridDSBenhNhan(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadDataGridDSBenhNhan(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadDataGridDSBenhNhan(int page)
        {
            if (flagLoading) return;
            try
            {
                if (page <= 0) page = 1;
                if (txtTU.DateTime > txtDEN.DateTime)
                { 
                   MessageBox.Show("Trường từ ngày không được lớn hơn trường đến ngày");
                    txtTU.Focus();
                    return;
                }

                string tuNgay = txtTU.DateTime.ToString(Const.FORMAT_date1);
                string denNgay = txtDEN.DateTime.ToString(Const.FORMAT_date1);
                string loaiVP = cbbLoaiVienPhi.SelectValue;
                string doiTuongBN = "1";
                string rdValue = (rdNgayVaoVien.Checked) ? "0" : "1";
                string trangThai = cbbTrangThai.SelectValue;
                string dsBa = string.Empty;

                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging("VPI01T001.01.4210", page, ucGridDSBenhNhan.ucPage1.getNumberPerPage(),
                    new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]" }
                    , new string[] { doiTuongBN, loaiVP, trangThai, rdValue, tuNgay, denNgay, Const.local_khoaId.ToString(), dsBa, (cboLoaiThe.SelectedIndex-1).ToString() }, ucGridDSBenhNhan.jsonFilter());

                ucGridDSBenhNhan.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "TRANGTHAITIEPNHAN_VP", "TRANGTHAITIEPNHAN_BH", "TRANGTHAITIEPNHAN4210", "STT_DUYET", "MABENHNHAN", "TENBENHNHAN", "NGAYSINH", "GIOITINH", "MA_BHYT", "MATIEPNHAN", "MAHOSOBENHAN", "NGAYTIEPNHAN", "NGAY_RAVIEN" });

                ucGridDSBenhNhan.setData(dt, responses.total, responses.page, responses.records);
                ucGridDSBenhNhan.setColumnAll(false);
                ucGridDSBenhNhan.setColumn("TRANGTHAITIEPNHAN_VP", 1, " ", 0);
                ucGridDSBenhNhan.setColumn("TRANGTHAITIEPNHAN4210", 2, " ", 0);
                ucGridDSBenhNhan.setColumn("STT_DUYET", 3, "STT", 0);
                ucGridDSBenhNhan.setColumn("MABENHNHAN", 4, "Mã bệnh nhân", 0);
                ucGridDSBenhNhan.setColumn("TENBENHNHAN", 5, "Họ tên", 0);
                ucGridDSBenhNhan.setColumn("NGAYSINH", 6, "Ngày sinh", 0);
                ucGridDSBenhNhan.setColumn("GIOITINH", 7, "GT", 0);
                ucGridDSBenhNhan.setColumn("MA_BHYT", 8, "Mã BHYT", 0);
                ucGridDSBenhNhan.setColumn("MATIEPNHAN", 9, "Mã viện phí", 0);
                ucGridDSBenhNhan.setColumn("MAHOSOBENHAN", 10, "Mã bệnh án", 0);
                ucGridDSBenhNhan.setColumn("NGAYTIEPNHAN", 11, "Vào viện", 0);
                ucGridDSBenhNhan.setColumn("MATIEPNHAN", 12, "Ra viện", 0);
                ucGridDSBenhNhan.gridView.BestFitColumns(true);

                ucGridDSBenhNhan.setColumnImage("TRANGTHAITIEPNHAN_VP", new String[] { "1", "0", "" }
                        , new String[] { "./Resources/True.png", "./Resources/Flag_Red.png", "./Resources/Flag_Red.png" });
                ucGridDSBenhNhan.setColumnImage("TRANGTHAITIEPNHAN4210", new String[] { "2" }
                    , new String[] { "./Resources/Circle_Green.png" });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadGridDSDichVu(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadDataGridDSDichVu(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        string _khoa = "";
        private void LoadDataGridDSDichVu(int page)
        {
            if (flagLoading) return;
            try
            {
                if (page <= 0) page = 1;

                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging("VPI01T001.21", page, ucGridDSDichVu.ucPage1.getNumberPerPage(),
                    new String[] { "[0]", "[1]" }, new string[] { TIEPNHANID, "0" }, ucGridDSDichVu.jsonFilter());

                ucGridDSDichVu.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "DOITUONG", "NHOM_MABHYT", "TENDICHVU", "SOLUONG", "TIENDICHVU", "TIEN_BHYT_TRA", "THUCTHU", "TYLE_BHYT_TRA", "TIEN_MIENGIAM" });

                ucGridDSDichVu.setData(dt, responses.total, responses.page, responses.records);
                ucGridDSDichVu.setColumnAll(false);
                ucGridDSDichVu.setColumn("TENDICHVU", 0, "Tên dịch vụ", 0);
                ucGridDSDichVu.setColumn("SOLUONG", 1, "SL", 0);
                ucGridDSDichVu.setColumn("TIENDICHVU", 2, "Giá tiền", 0);
                ucGridDSDichVu.setColumn("TIEN_BHYT_TRA", 3, "BHYT trả", 0);
                ucGridDSDichVu.setColumn("THUCTHU", 4, "BN trả", 0);
                ucGridDSDichVu.setColumn("TYLE_BHYT_TRA", 5, "Tỷ lệ %", 0);
                ucGridDSDichVu.setColumn("TIEN_MIENGIAM", 6, "Miễn giảm", 0);
                ucGridDSDichVu.gridView.BestFitColumns(true);

                 
                ucGridDSDichVu.gridView.Columns["DOITUONG"].Group();
                ucGridDSDichVu.gridView.Columns["NHOM_MABHYT"].Group();
                ucGridDSDichVu.gridView.ExpandAllGroups();

                // Tô màu các dòng dl
                _khoa = ",";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string _dathutien = dt.Rows[i]["DATHUTIEN"].ToString();
                    string _soluong = dt.Rows[i]["SOLUONG"].ToString();
                    string _tyle_bhyt_tra = dt.Rows[i]["TYLE_BHYT_TRA"].ToString();
                    string _tyle_the = dt.Rows[i]["TYLE"].ToString();
                    string _loaidoituong = dt.Rows[i]["LOAIDOITUONG"].ToString();
                    string _tyle_dv = dt.Rows[i]["TYLE_DV"].ToString();
                    string _loainhommbp = dt.Rows[i]["LOAINHOMMAUBENHPHAM"].ToString();
                    string _tien_dv = dt.Rows[i]["TIENDICHVU"].ToString();
                    string _vattu04 = dt.Rows[i]["VATTU04"].ToString();
                    if (_dathutien == "3")
                    {
                        //$("#" + _gridId_DV).jqGrid('setRowData', id, "", {
                        //color: 'blue'
                        //});
                    }
                    if ((_loaidoituong == "1" || _loaidoituong == "2" || _loaidoituong == "3")
                                 && _loainhommbp != "16"
                                 && _tyle_the != _tyle_bhyt_tra && _tyle_dv != "0" && _soluong != "0"
                                 && (_loainhommbp != "3" || _tien_dv != "0")
                                 && _vattu04 == "0")
                    {
                        //$("#" + _gridId_DV).jqGrid('setRowData', id, "", {
                        //color: 'red'
                        //            });
                        string _khoadv = dt.Rows[i]["KHOA"].ToString();
                        if (!_khoa.Contains("," + _khoadv + ",")) _khoa += _khoadv + ", ";
                    }
                }
                _khoa = _khoa.Trim();
                if (_khoa.StartsWith(",")) _khoa = _khoa.Substring(1);
                if (_khoa.EndsWith(",")) _khoa = _khoa.Substring(0, _khoa.Length-1);
                
                tinhTongTien(dt);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadGridDSPhieuThu(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadDataGridDSPhieuThu(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadDataGridDSPhieuThu(int page)
        {
            if (flagLoading) return;
            try
            {
                if (page <= 0) page = 1;

                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging("VPI01T001.03", page, ucGridDSPhieuThu.ucPage1.getNumberPerPage(),
                    new String[] { "[0]"}, new string[] { TIEPNHANID }, ucGridDSPhieuThu.jsonFilter());

                ucGridDSPhieuThu.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "DAHUYPHIEU", "MAPHIEUTHU", "DATRA", "LOAIPHIEUTHUID_2", "NGAYTHU", "LYDOHUYPHIEU" });

                ucGridDSPhieuThu.setData(dt, responses.total, responses.page, responses.records);
                ucGridDSPhieuThu.setColumnAll(false);
                ucGridDSPhieuThu.setColumn("DAHUYPHIEU", 1, " ", 0);
                ucGridDSPhieuThu.setColumn("MAPHIEUTHU", 2, "Mã phiếu thu", 0);
                ucGridDSPhieuThu.setColumn("DATRA", 3, "Số tiền", 0);
                ucGridDSPhieuThu.setColumn("LOAIPHIEUTHUID_2", 4, "Loại phiếu", 0);
                ucGridDSPhieuThu.setColumn("NGAYTHU", 5, "Ngày thanh toán", 0);
                ucGridDSPhieuThu.setColumn("LYDOHUYPHIEU", 6, "Lý do hủy", 0);
                ucGridDSPhieuThu.gridView.BestFitColumns(true);

                ucGridDSPhieuThu.setColumnImage("DAHUYPHIEU", new String[] { "1"}
                        , new String[] { "./Resources/DeleteRed.png" });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
       
        private void bbtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (flagLoading) return;
            LoadDataGridDSBenhNhan(1);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtNGAYDUYET_EditValueChanged(object sender, EventArgs e)
        { 
            int month = txtNGAYDUYET.DateTime.Month + 1;
            if (month <= 3) cboQUYDUYET.SelectedIndex = 0;
            else if (month <= 6) cboQUYDUYET.SelectedIndex = 1;
            else if (month <= 9) cboQUYDUYET.SelectedIndex = 2;
            else cboQUYDUYET.SelectedIndex = 3;
        }

        bool flagLoading = false;
        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (flagLoading) return;

            txtNGAYDUYET.DateTime = Func.getSysDatetime();
           
            int month = txtNGAYRAVIEN.DateTime.Month + 1;
            if (month <= 3) cboQUYDUYET.SelectedIndex = 0;
            else if (month <= 6) cboQUYDUYET.SelectedIndex = 1;
            else if (month <= 9) cboQUYDUYET.SelectedIndex = 2;
            else cboQUYDUYET.SelectedIndex = 3; 

            txtNGUOIDUYET.Text = Const.local_user.USER_ID.ToString();
            //setEnabled(['btnLuu', 'btnHuyBo', 'txtNGAYDUYET', 'dpkNGAYDUYET'], ['btnDuyet']);
            btnLuu.Enabled = true;
            btnHuyBo.Enabled = true;
            txtNGAYDUYET.ReadOnly = false;
            //dpkNGAYDUYET.Enabled = true;

            btnDuyet.Enabled = false;

            flagLoading = true;
        }


        private string getXmlTo_Insr(DataTable dt)
        {
            return "";
        }
        private string getXmlTo_Medical(DataTable dt)
        {
            return "";
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtBenhNhan.Rows.Count <= 0) return;

            if (VPI_KIEMTRA_TYLE == "0")
            {
                if (_khoa.Length > 0)
                {
                    MessageBox.Show("Có dịch vụ sai tỷ lệ tại " + _khoa);
                    return;
                }

                if (dtBenhNhan.Rows[0]["LOAITIEPNHANID"].ToString() != "0" && dtBenhNhan.Rows[0]["LYDO_VAOVIEN"].ToString() == "3" 
                    && Func.Parse(dtBenhNhan.Rows[0]["MUCHUONG"].ToString()) > 0)
                {
                    MessageBox.Show("Bệnh nhân ngoại trú trái tuyến đang có mức hưởng , xem lại thông tin hành chính");
                    return;
                }
            }
            object objData = new {
                NGUOIDUYET = txtNGUOIDUYET.Text
                , QUYDUYET = cboQUYDUYET.SelectedIndex + 1
                , STT = txtSTT.Text
                , NGAYDUYET = txtNGAYDUYET.Text
                , TIEPNHANID = TIEPNHANID
                , HOSPITAL_CODE = Const.local_user.HOSPITAL_CODE
            }; 
             
            if (txtNGAYDUYET.DateTime < txtNGAYRAVIEN.DateTime)
            {
                MessageBox.Show("Ngày duyệt phải lớn hơn ngày ra viện");
                return;
            }

            string fl = RequestHTTP.call_ajaxCALL_SP_S_result("VPI02G001.01.4210", Newtonsoft.Json.JsonConvert.SerializeObject(objData).Replace("\"", "\\\""));
            if (fl == "1")
            {
                if (fl.Length > 10)
                {
                    MessageBox.Show(fl);
                }

                string _ngayhientai = Func.getSysDatetime(Const.FORMAT_date1);// "dd\\/MM\\/yyyy";
                var _ngay_ra = dtBenhNhan.Rows[0]["NGAY_RA_VIEN"].ToString();
                if (_ngay_ra != _ngayhientai)
                {
                    MessageBox.Show("Hồ sơ mã " + dtBenhNhan.Rows[0]["MAHOSOBENHAN"].ToString() + " có ngày ra viện khác ngày thanh toán. Yêu cầu: BỔ SUNG HỒ SƠ VÀO NGÀY " + _ngay_ra);
                }

                flagLoading = false;
                btnGoDuyet.Enabled = true;

                btnDuyet.Enabled = false;
                btnLuu.Enabled = false;
                btnHuyBo.Enabled = false;
                cboQUYDUYET.ReadOnly = true;

                //var _icon = '<center><img src="' + _opts.imgPath[2] + '" width="15px"></center>';
                //         var id = $("#"+_gridId_BN).jqGrid("getGridParam","selrow");
                //$("#"+_gridId_BN).jqGrid('setCell', id, 3, _icon);

                //Gui bao hiem & y te
                if (VP_GUI_DULIEU_KHIDUYET == "1")
                {
                    string _ret_bhxh = "";
                    string _ret_byt = "";
                    string _ngayravien = dtBenhNhan.Rows[0]["NGAYRAVIEN"].ToString();

                    DataTable data_bh = new DataTable();
                    DataTable data_byt = new DataTable();

                    if (VP_GUI_DULIEU_KHIDUYET != "0")
                    {
                        if (VPI_GUI_BH == "1")
                        {
                            data_bh = RequestHTTP.call_ajaxCALL_SP_O("XML.4210", 1 + '$' + _ngayravien + '$' + _ngayravien + '$' + 1 + '$' + TIEPNHANID);
                            data_byt = data_bh;
                        }
                        else
                        {
                            data_bh = RequestHTTP.call_ajaxCALL_SP_O("VPI02G001.03", TIEPNHANID + '$' + _ngayravien + '$' + _ngayravien);
                            data_byt = RequestHTTP.call_ajaxCALL_SP_O("BH01.XML01", _ngayravien + '$' + _ngayravien + '$' + TIEPNHANID);
                        }
                    }
                    if (VP_GUI_DULIEU_KHIDUYET == "1")
                    {
                        _ret_bhxh = getXmlTo_Insr(data_bh);
                        _ret_byt = getXmlTo_Medical(data_byt);

                    }
                    else if (VP_GUI_DULIEU_KHIDUYET == "2")
                    {
                        _ret_bhxh = getXmlTo_Insr(data_bh);

                    }
                    else if (VP_GUI_DULIEU_KHIDUYET == "3")
                    {
                        _ret_byt = getXmlTo_Medical(data_byt);

                    }
                    if (_ret_bhxh != "" || _ret_byt != "")
                    {
                        string ret = RequestHTTP.call_ajaxCALL_SP_I("NTU02D061.02", TIEPNHANID + "$" + _ret_bhxh + "$" + _ret_byt);
                        if (ret == "-1")
                        {
                            MessageBox.Show("Cập nhật trạng thái không thành công");
                        }
                    }

                }
                //FormUtil.clearForm('ttDuyet', "");
                txtNGUOIDUYET.Text = "";
                cboQUYDUYET.SelectedIndex = 0;//$("#cboQUYDUYET").val(1);
                txtSTT.Text = "";
                txtNGAYDUYET.Text = "";


            }
            else if (fl == "0")
            {
                MessageBox.Show("Hồ sơ của bệnh nhân đã khóa, không thể duyệt");
            }
            else if (fl == "2")
            {
                MessageBox.Show("Bệnh nhân không có tiền đề nghị thanh toán, không thể duyệt");
            }
            else if (fl == "-1")
            {
                MessageBox.Show("Duyệt không thành công");
            }
            else
            {
                MessageBox.Show(fl);
            }
				
        }
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            flagLoading = false;
            //FormUtil.clearForm('ttDuyet', "");
            txtNGUOIDUYET.Text = "";
            cboQUYDUYET.SelectedIndex = -1;
            txtSTT.Text = "";
            txtNGAYDUYET.Text = "";

            btnDuyet.Enabled = true;

            btnLuu.Enabled = false;
            btnHuyBo.Enabled = false;
            cboQUYDUYET.ReadOnly = true;
            txtNGUOIDUYET.ReadOnly = true;
            txtSTT.ReadOnly = true;
            txtNGAYDUYET.ReadOnly = true;
        }

        private void btnDuyetTatCa_Click(object sender, EventArgs e)
        {
            int[] index = ucGridDSBenhNhan.gridView.GetSelectedRows();
            string _dstn = "";
            for (int i = 0; i < index.Length; i++)
            {
                DataRowView dataRow = (DataRowView)ucGridDSBenhNhan.gridView.GetRow(index[i]);
                if (dataRow != null)
                {
                    _dstn += dataRow["TIEPNHANID"].ToString() + ","; 
                }
                if (_dstn.EndsWith(",")) _dstn = _dstn.Substring(0, _dstn.Length - 1);
            }

            if (_dstn == "") _dstn = "-1";
             
            string fl = RequestHTTP.call_ajaxCALL_SP_S_error_msg("VPI.BH.4210"
                , txtTU.DateTime.ToString("yyyyMMdd") + '$' + txtDEN.DateTime.ToString("yyyyMMdd") + '$' + _dstn);

			MessageBox.Show(fl);

            LoadDataGridDSBenhNhan(1);
        }

        private void btnGoDuyet_Click(object sender, EventArgs e)
        {
            string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI02G001.02.4210", TIEPNHANID);
            if (fl == "1")
            { 
                flagLoading = false;
                btnDuyet.Enabled = true;

                btnLuu.Enabled = false;
                btnGoDuyet.Enabled = false;

                //var id = $("#"+_gridId_BN).jqGrid("getGridParam","selrow");
                //$("#"+_gridId_BN).jqGrid('setCell', id, 3, null);
                LoadDataGridDSBenhNhan(1);
            }
			else if(fl == "0"){
				MessageBox.Show("Hồ sơ của bệnh nhân đã khóa, không thể gỡ duyệt");
			}
			else if(fl == "-2"){
				MessageBox.Show("Hết thời gian xử lý hồ sơ, liên hệ với người quản trị");
			}
			else if(fl == "-3"){
				MessageBox.Show("Bạn không có quyền gỡ duyệt hồ sơ này");
			}
			else if(fl == "-4"){
				MessageBox.Show("Hồ sơ chưa được duyệt");
			}
			else {
				MessageBox.Show("Hủy duyệt không thành công");
			}
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dtBenhNhan.Rows.Count <= 0) return;

            string _dtbnid = dtBenhNhan.Rows[0]["DOITUONGBENHNHANID"].ToString();
            string _loaitiepnhanid = dtBenhNhan.Rows[0]["LOAITIEPNHANID"].ToString();

            string opt = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_TACH_BANGKE");
            string IN_BK_VP = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "c");
            string IN_GOP_BKNTRU = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VPI_GOP_BANGKENTRU");
            if (IN_BK_VP == "") IN_BK_VP = "0";
            if (opt == "1")
            {
                string flag = RequestHTTP.call_ajaxCALL_SP_I("VPI01T004.10", TIEPNHANID);
                if (_loaitiepnhanid == "0")
                {
                    if (IN_GOP_BKNTRU == "1")
                    {
                        this.inPhoiVP("1", TIEPNHANID, "NTU001_BKCPKCBBHYTNOITRU_02BV_QD3455_A4");
                    }
                    else
                    {
                        if (_dtbnid == "1")
                        {
                            RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", TIEPNHANID);
                            this.inPhoiVP("1", TIEPNHANID, "NTU001_BKCPKCBBHYTNOITRU_02BV_QD3455_A4");
                            if (IN_BK_VP == "0" && flag == "1")
                                this.inPhoiVP("1", TIEPNHANID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                        }
                        else
                        {
                            if (flag == "1")
                                this.inPhoiVP("1", TIEPNHANID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                        }
                    }
                }
                else
                {
                    if (_dtbnid == "1")
                    {
                        RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", TIEPNHANID);
                        this.inPhoiVP("1", TIEPNHANID, "NGT001_BKCPKCBBHYTNGOAITRU_01BV_QD3455_A4");
                        if (IN_BK_VP == "0" && flag == "1")
                            this.inPhoiVP("1", TIEPNHANID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                    }
                    else
                    {
                        if (flag == "1")
                            this.inPhoiVP("1", TIEPNHANID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                    }
                }
            }
            else
            {
                RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", TIEPNHANID);
                if (_loaitiepnhanid == "0")
                {
                    this.inPhoiVP("1", TIEPNHANID, "NTU001_BKCPKCBNOITRU_02BV_QD3455_A4");
                }
                else
                {
                    this.inPhoiVP("1", TIEPNHANID, "NGT001_BKCPKCBNGOAITRU_01BV_QD3455_A4");
                }
            }
            // in bang ke hao phi neu co
            string flag_haophi = RequestHTTP.call_ajaxCALL_SP_I("VPI01T005.11", TIEPNHANID);
            string opt_haophi = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_BANGKE_HAOPHI");
            if (opt_haophi == "1")
            {
                if (flag_haophi == "1")
                {
                    this.inPhoiVP("1", TIEPNHANID, "NGT001_BKCPKCB_HAOPHI_01BV_QD3455_A4");
                }
            }
        }
        private void inPhoiVP(string _inbangkechuan, string _TIEPNHANID, string _report_code)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("inbangkechuan", "String", _inbangkechuan);
            table.Rows.Add("tiepnhanid", "String", _TIEPNHANID);

            Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, _report_code, "pdf", 720, 1200);
            openForm(frm); 
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

    }
}