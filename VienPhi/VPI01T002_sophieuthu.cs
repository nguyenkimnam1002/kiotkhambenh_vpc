using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using System.Globalization;
using Newtonsoft.Json;
using System.Collections.Generic;
using DevExpress.XtraEditors;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T002_sophieuthu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool LOADINGFLAG = false;
        private bool ISEDIT = false;
        private bool ISBLOCK = false;
        private DataRowView dataRow;
        private string NHOMPHIEUTHUID;
        private string SOPHIEUSUDUNG, TONGSOPHIEUTHU;
        private string MAPHIEUTHUMAX;
        private const string VALUE = "value";
        private const string NAME = "name";

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T002_sophieuthu()
        {
            InitializeComponent();
        }
        private void SetEnabled(List<SimpleButton> listEnabled, List<SimpleButton> listDisabled)
        {
            if (listEnabled != null)
            {
                foreach (var item in listEnabled)
                {
                    item.Enabled = true;
                }
            }

            if (listDisabled != null)
            {
                foreach (var item in listDisabled)
                {
                    item.Enabled = false;
                }
            }
        }
        private void VPI01T002_sophieuthu_Load(object sender, EventArgs e)
        {
            InitLoaiTT();
            InitLoaiSo();
            InitLoaiSo2();
            InitControl();
            InitGrid();
            SetStatusButton(true, false, false, false, false, false);
        }

        private void InitControl()
        {
            try
            {
                txtGhiChu.Enabled = false;
                txtKHOASOPHIEUTU.Enabled = false;
                txtMaSoCha.Enabled = false;
                chkISLOCK.Enabled = false;
                chkSOAO.Enabled = false;
                chkMACDINH.Enabled = false;
                cboNHOMPHIEUTHUID_ORG.lookUpEdit.Enabled = false;

                txtMANHOMPHIEUTHU.Enabled = false;
                cboHINHTHUCTHANHTOAN.lookUpEdit.Enabled = false;
                cboKIEUTHU.lookUpEdit.Enabled = false;
                cboLOAIPHIEUTHU.lookUpEdit.Enabled = false;
                txtTONGSOPHIEUTHU.Enabled = false;
                txtSOPHIEUFROM.Enabled = false;
                txtSOPHIEUTO.Enabled = false; 

                Enabled = false;
                dEditDen.Enabled = false;
                dEditTu.Enabled = false;





                dEditTu.DateTime = Func.getSysDatetime_Short();
                dEditTu.Properties.DisplayFormat.FormatString = Const.FORMAT_date1;

                dEditDen.DateTime = Func.getSysDatetime_Short();
                dEditDen.Properties.DisplayFormat.FormatString = Const.FORMAT_date1;

                cboHINHTHUCTHANHTOAN.SelectIndex = 0;
                cboLOAIPHIEUTHU.SelectIndex = 0;
                cboKIEUTHU.SelectIndex = 0;

                txtMANHOMPHIEUTHU.Text = string.Empty;
                txtTONGSOPHIEUTHU.Text = string.Empty;
                txtSOPHIEUFROM.Text = string.Empty;
                txtSOPHIEUTO.Text = string.Empty;
                txtGhiChu.Text = string.Empty;
                txtKHOASOPHIEUTU.Text = string.Empty;
                txtMaSoCha.Text = string.Empty;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitLoaiTT()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(VALUE);
                dt.Columns.Add(NAME);

                DataRow dr = dt.NewRow();

                dr[VALUE] = "1";
                dr[NAME] = "Tiền mặt";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "2";
                dr[NAME] = "Chuyển khoản";
                dt.Rows.Add(dr);

                cboHINHTHUCTHANHTOAN.setData(dt, VALUE, NAME);
                cboHINHTHUCTHANHTOAN.lookUpEdit.Properties.Columns[VALUE].Visible = false;
                cboHINHTHUCTHANHTOAN.setColumn(NAME, 0, "", 0);
                cboHINHTHUCTHANHTOAN.SelectIndex = 0;
                cboHINHTHUCTHANHTOAN.setEvent_Enter(cbbLoaiTT_KeyEnter);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitLoaiSo()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(VALUE);
                dt.Columns.Add(NAME);

                DataRow dr = dt.NewRow();

                dr[VALUE] = "1";
                dr[NAME] = "Tổng hợp";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "2";
                dr[NAME] = "BHYT";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "3";
                dr[NAME] = "Viện phí";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "4";
                dr[NAME] = "Thu khác";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "5";
                dr[NAME] = "Sổ trực";
                dt.Rows.Add(dr);

                cboLOAIPHIEUTHU.setData(dt, VALUE, NAME);
                cboLOAIPHIEUTHU.lookUpEdit.Properties.Columns[VALUE].Visible = false;
                cboLOAIPHIEUTHU.SelectIndex = 0;
                cboLOAIPHIEUTHU.setEvent_Enter(cbbLoaiSo_KeyEnter);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitLoaiSo2()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(VALUE);
                dt.Columns.Add(NAME);

                DataRow dr = dt.NewRow();

                dr[VALUE] = "1";
                dr[NAME] = "Hóa đơn";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "2";
                dr[NAME] = "Hoàn ứng";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "3";
                dr[NAME] = "Tạm ứng";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "6";
                dr[NAME] = "Thu tiền";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[VALUE] = "9";
                dr[NAME] = "Tổng hợp(Trừ hóa đơn)";
                dt.Rows.Add(dr);

                cboKIEUTHU.setData(dt, VALUE, NAME);
                cboKIEUTHU.lookUpEdit.Properties.Columns[VALUE].Visible = false;
                cboKIEUTHU.SelectIndex = 0;
                cboKIEUTHU.setEvent_Enter(cbbLoaiSo2_KeyEnter);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitGrid()
        {
            ucGridPhieuThu.gridView.OptionsView.ColumnAutoWidth = false;
            ucGridPhieuThu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGridPhieuThu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGridPhieuThu.gridView.OptionsBehavior.Editable = false;
            ucGridPhieuThu.setMultiSelectMode(false);

            ucGridPhieuThu.setEvent(LoadGridSoPhieuThu);
            ucGridPhieuThu.SetReLoadWhenFilter(true);
            ucGridPhieuThu.gridView.Click += GridView_Click;
            ucGridPhieuThu.setNumberPerPage(new int[] { 200, 300 });

            ucGridPhieuThuChiTiet.gridView.OptionsView.ColumnAutoWidth = false;
            ucGridPhieuThuChiTiet.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGridPhieuThuChiTiet.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGridPhieuThuChiTiet.gridView.OptionsBehavior.Editable = false;
            ucGridPhieuThuChiTiet.gridView.OptionsView.RowAutoHeight = true;
            ucGridPhieuThuChiTiet.setMultiSelectMode(false);

            ucGridPhieuThuChiTiet.setEvent(LoadGridSoPhieuThuCT);
            ucGridPhieuThuChiTiet.addMenuPopup(MenuPopupPhieuThuContextMenu());
            ucGridPhieuThuChiTiet.setEvent_MenuPopupClick(MenuPopupClickPhieuThu);
            ucGridPhieuThuChiTiet.SetReLoadWhenFilter(true);
            ucGridPhieuThuChiTiet.setNumberPerPage(new int[] { 200, 300 });
        }

        private void bbtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void LoadGridSoPhieuThu(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadGridData(pageNum);
        }

        private void LoadGridSoPhieuThuCT(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadGridDataCT(pageNum);
        }

        public void LoadGridData(int page)
        {
            try
            {
                if (LOADINGFLAG) return;
                if (page <= 0) page = 1;

                ResponsList responses = new ResponsList();

                //string jsonFilter = string.Empty;
                //if (ucGridPhieuThu.ReLoadWhenFilter && ucGridPhieuThu.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridPhieuThu.tableFlterColumn);
                //}  

                DataTable phieuThuDT = new DataTable();

                string suDungThuoc = (chkSuDungDuoc.Checked) ? "1" : "0";
                string daHetPhieu = (chkDaHetPhieu.Checked) ? "1" : "0";
                string daKhoa = (chkDaKhoa.Checked) ? "1" : "0";
                string soAo = (chkAO.Checked) ? "1" : "0";

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "VPI01T002.01", page, ucGridPhieuThu.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[1]", "[2]", "[3]", "[3]", "[4]" },
                    new string[] { suDungThuoc, daHetPhieu, daKhoa, soAo, txtTimKiem.Text.Trim(), Const.local_phongId.ToString() }, ucGridPhieuThu.jsonFilter());

                ucGridPhieuThu.clearData();

                phieuThuDT = MyJsonConvert.toDataTable(responses.rows);

                if (phieuThuDT.Rows.Count == 0)
                    phieuThuDT = Func.getTableEmpty(new String[] { "RN", "MANHOMPHIEUTHU", "NGAYLAPPHIEU", "TONGSOPHIEUTHU", "SOPHIEUSUDUNG", "NGUOILAPPHIEU" });

                ucGridPhieuThu.setData(phieuThuDT, responses.total, responses.page, responses.records);
                ucGridPhieuThu.setColumnAll(false);

                ucGridPhieuThu.setColumn("RN", 0, " ", 0);
                ucGridPhieuThu.setColumn("MANHOMPHIEUTHU", 1, "Mã sổ", 0);
                ucGridPhieuThu.setColumn("NGAYLAPPHIEU", 2, "Ngày tạo", 0);
                ucGridPhieuThu.setColumn("TONGSOPHIEUTHU", 3, "Tổng số phiếu", 0);
                ucGridPhieuThu.setColumn("SOPHIEUSUDUNG", 4, "Đã sử dụng", 0);
                ucGridPhieuThu.setColumn("NGUOILAPPHIEU", 5, "Người sử dụng", 0);

                ucGridPhieuThu.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        public void LoadGridDataCT(int page)
        {
            try
            {
                if (ISBLOCK) return;
                if (LOADINGFLAG) return;
                if (page <= 0) page = 1;

                ResponsList responses = new ResponsList();

                //string jsonFilter = string.Empty;
                //if (ucGridPhieuThuChiTiet.ReLoadWhenFilter && ucGridPhieuThuChiTiet.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridPhieuThuChiTiet.tableFlterColumn);
                //}

                DataTable phieuThuDT = new DataTable();

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "VPI01T002.03", page, ucGridPhieuThuChiTiet.ucPage1.getNumberPerPage(),
                    new string[] { "[0]" },
                    new string[] { NHOMPHIEUTHUID }, ucGridPhieuThuChiTiet.jsonFilter());

                ucGridPhieuThuChiTiet.clearData();

                phieuThuDT = MyJsonConvert.toDataTable(responses.rows);

                if (phieuThuDT.Rows.Count == 0)
                    phieuThuDT = Func.getTableEmpty(new String[] { "RN", "DAHUYPHIEU", "MAPHIEUTHU", "MANHOMPHIEUTHU", "DATRA", "LOAIPHIEUTHUID", "NGUOIDUNGID", "NGAYTHU", "MATIEPNHAN", "TENBENHNHAN", "NOIDUNGTHU", "PHIEUTHULOG" });

                ucGridPhieuThuChiTiet.setData(phieuThuDT, responses.total, responses.page, responses.records);
                ucGridPhieuThuChiTiet.setColumnAll(false);

                ucGridPhieuThuChiTiet.setColumn("RN", 0, " ", 0);
                ucGridPhieuThuChiTiet.setColumn("DAHUYPHIEU", 1, " ");
                ucGridPhieuThuChiTiet.setColumn("MAPHIEUTHU", 2, "Mã phiếu", 0);
                ucGridPhieuThuChiTiet.setColumn("MANHOMPHIEUTHU", 3, "Mã số", 0);
                ucGridPhieuThuChiTiet.setColumn("DATRA", 4, "Số tiền", 0);
                ucGridPhieuThuChiTiet.setColumn("LOAIPHIEUTHUID", 5, "Loại phiếu", 0);
                ucGridPhieuThuChiTiet.setColumn("NGUOIDUNGID", 6, "Người lập", 0);
                ucGridPhieuThuChiTiet.setColumn("NGAYTHU", 7, "Ngày lập", 0);
                ucGridPhieuThuChiTiet.setColumn("MATIEPNHAN", 8, "Ma VP", 0);
                ucGridPhieuThuChiTiet.setColumn("TENBENHNHAN", 9, "Bệnh nhân", 0);
                ucGridPhieuThuChiTiet.setColumn("NOIDUNGTHU", 10, "Ghi chú", 0);
                ucGridPhieuThuChiTiet.setColumnMemoEdit("PHIEUTHULOG", 11, "Lịch sử thay đổi số phiếu", 0);

                ucGridPhieuThuChiTiet.setColumnImage("DAHUYPHIEU", new String[] { "2", "0", "1" }
                        , new String[] { "./Resources/Cancel.png", "./Resources/True.png", "./Resources/DeleteRed.png" });

                ucGridPhieuThuChiTiet.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ISBLOCK) return;

                dataRow = null;
                int index = ucGridPhieuThu.gridView.FocusedRowHandle;
                dataRow = (DataRowView)ucGridPhieuThu.gridView.GetRow(index);
                if (dataRow == null) return;

                TONGSOPHIEUTHU = dataRow["TONGSOPHIEUTHU"].ToString();
                NHOMPHIEUTHUID = dataRow["NHOMPHIEUTHUID"].ToString();
                SOPHIEUSUDUNG = dataRow["SOPHIEUSUDUNG"].ToString();
                MAPHIEUTHUMAX = dataRow["MAPHIEUTHUMAX"].ToString();
                var ngayTaoSo = dataRow["NGAYLAPPHIEU"].ToString();

                txtKHOASOPHIEUTU.Text = dataRow["KHOASOPHIEUTU"].ToString();
                txtMaSoCha.Text = dataRow["MASOCHA"].ToString();
                dEditTu.DateTime = DateTime.ParseExact(ngayTaoSo, Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
                dEditTu.Properties.DisplayFormat.FormatString = Const.FORMAT_datetime1;
                dEditDen.DateTime = Func.getSysDatetime();
                dEditDen.Properties.DisplayFormat.FormatString = Const.FORMAT_datetime1;
                LoadGridDataCT(1);
                TienKetChuyen();

                DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("VPI01T002.04", NHOMPHIEUTHUID);

                if (dt == null) return;

                var row = dt.Rows[0];

                if (row == null) return;

                txtMANHOMPHIEUTHU.Text = row["MANHOMPHIEUTHU"].ToString();
                cboHINHTHUCTHANHTOAN.SelectValue = row["HINHTHUCTHANHTOAN"].ToString();
                cboLOAIPHIEUTHU.SelectValue = row["LOAIPHIEUTHU"].ToString();
                cboKIEUTHU.SelectValue = row["KIEUTHU"].ToString();
                txtTONGSOPHIEUTHU.Text = row["TONGSOPHIEUTHU"].ToString();
                txtSOPHIEUFROM.Text = row["SOPHIEUFROM"].ToString();
                txtSOPHIEUTO.Text = row["SOPHIEUTO"].ToString();
                txtGhiChu.Text = row["GHICHU"].ToString();

                chkISLOCK.Checked = ("0".Equals(row["ISLOCK"].ToString())) ? false : true;
                chkSOAO.Checked = ("0".Equals(row["SOAO"].ToString())) ? false : true;
                chkMACDINH.Checked = ("0".Equals(row["MACDINH"].ToString())) ? false : true;

                SetStatusButton(true, true, true, false, false, true);
                dEditTu.Enabled = true;
                dEditDen.Enabled = true;

                // đã check oke
                //setEnabled(["btnThem", "btnSua", "btnXoa", "txtTU", "calTU", "txtDEN", "calDEN", 
                //    "cboIn2", "btnKetChuyen", "cboKetChuyen"]
                //    ,["btnLuu", "btnHuy"]);


            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void TienKetChuyen()
        {
            try
            {
                DataTable dt = RequestHTTP.get_ajaxExecuteQueryO(
                                        "VPI01T002.08",
                                        new string[] { "[0]", "[1]", "[2]" },
                                        new string[] { NHOMPHIEUTHUID, dEditTu.Text, dEditDen.Text });
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    var tongTien = double.Parse(row["TONGTIEN"].ToString());
                    var tamUng = double.Parse(row["TAMUNG"].ToString());
                    var hoanUng = double.Parse(row["HOANUNG"].ToString());
                    lblTongTienThu.Text = String.Format("{0:0,0.00}", tongTien);
                    lblTienTamUng.Text = String.Format("{0:0,0.00}", tamUng);
                    lblTongHoanUng.Text = String.Format("{0:0,0.00}", hoanUng);
                    lblNopKeToan.Text = String.Format("{0:0,0.00}", 0 + tongTien + (hoanUng - tamUng));
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void btnRefreshCT_Click(object sender, EventArgs e)
        {
            LoadGridDataCT(1);
        }

        public void SetStatusButton(bool isThem, bool isSua, bool isXoa, bool isLuu, bool isHuy, bool isKetChuyen)
        {
            btnThem.Enabled = isThem;
            btnSua.Enabled = isSua;
            btnXoa.Enabled = isXoa;
            btnLuu.Enabled = isLuu;
            btnHuy.Enabled = isHuy;
            btnKetChuyen.Enabled = isKetChuyen;
        }

        private bool Enabled
        {
            set
            {
                layoutMaSo.Enabled = value;
                cboHINHTHUCTHANHTOAN.Enabled = value;
                cboLOAIPHIEUTHU.Enabled = value;
                cboKIEUTHU.Enabled = value;
                layoutTongSoPhieu.Enabled = value;
                layoutMaPhieuTu.Enabled = value;
                layoutGhiChu.Enabled = value;
                chkISLOCK.Enabled = value;
                chkSOAO.Enabled = value;
                chkMACDINH.Enabled = value;
                layoutKhoaPhieu.Enabled = value;
                layoutMaSoCha.Enabled = value;
            }
        }

        private void chkSuDungDuoc_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void chkDaHetPhieu_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void chkDaKhoa_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void chkSoAo_CheckedChanged(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void txtTongSoPhieu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value;
                if (int.TryParse(txtTONGSOPHIEUTHU.Text, out value) && int.TryParse(txtSOPHIEUFROM.Text, out value))
                {
                    txtSOPHIEUTO.Text = (double.Parse(txtTONGSOPHIEUTHU.Text) + (double.Parse(txtSOPHIEUFROM.Text) - 1)).ToString();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void txtMaPhieuTu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int value;
                if (int.TryParse(txtTONGSOPHIEUTHU.Text, out value) && int.TryParse(txtSOPHIEUFROM.Text, out value))
                {
                    txtSOPHIEUTO.Text = (double.Parse(txtTONGSOPHIEUTHU.Text) + (double.Parse(txtSOPHIEUFROM.Text) - 1)).ToString();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ISEDIT = false;
            LOADINGFLAG = true;
            ISBLOCK = true;
            chkMACDINH.Checked = true;
            chkISLOCK.Checked = false;
            chkSOAO.Checked = false;
            cboHINHTHUCTHANHTOAN.SelectIndex = 0;
            cboLOAIPHIEUTHU.SelectIndex = 0;
            cboKIEUTHU.SelectIndex = 0;

            InitControl();
            Enabled = true;
            SetStatusButton(false, false, false, true, true, false);
            txtMANHOMPHIEUTHU.Focus();


            // setEnabled(['btnLuu', 'btnHuy','txtMANHOMPHIEUTHU', 'cboHINHTHUCTHANHTOAN', 'cboKIEUTHU','cboLOAIPHIEUTHU', 
            // 'txtSOPHIEUFROM', 'txtTONGSOPHIEUTHU', 'txtGHICHU','txtKHOASOPHIEUTU','txtMASOCHA', 'chkISLOCK', 'chkSOAO', 
            // 'chkMACDINH', 'cboNHOMPHIEUTHUID_ORG'], 
            // ['btnThem', 'btnSua', 'btnXoa']);
            txtMANHOMPHIEUTHU.Enabled = true;
            cboHINHTHUCTHANHTOAN.lookUpEdit.Enabled = true;
            cboKIEUTHU.lookUpEdit.Enabled = true;
            cboLOAIPHIEUTHU.lookUpEdit.Enabled = true;

            txtSOPHIEUFROM.Enabled = true;
            txtSOPHIEUTO.Enabled = true;
            txtTONGSOPHIEUTHU.Enabled = true;
            txtGhiChu.Enabled = true;
            txtKHOASOPHIEUTU.Enabled = true;
            txtMaSoCha.Enabled = true;
            chkISLOCK.Enabled = true;
            chkSOAO.Enabled = true;

            chkMACDINH.Enabled = true;
            cboNHOMPHIEUTHUID_ORG.lookUpEdit.Enabled = true;
        }

        private void cbbLoaiTT_KeyEnter(object sender, EventArgs e)
        {
            cboLOAIPHIEUTHU.Focus();
        }

        private void cbbLoaiSo_KeyEnter(object sender, EventArgs e)
        {
            cboKIEUTHU.Focus();
        }

        private void cbbLoaiSo2_KeyEnter(object sender, EventArgs e)
        {
            txtTONGSOPHIEUTHU.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string daKhoa = (chkISLOCK.Checked) ? "1" : "0";
                string soAo = (chkSOAO.Checked) ? "1" : "0";
                string macDinh = (chkMACDINH.Checked) ? "1" : "0";

                if (!ValidateForm(soAo)) return;

                string objData = string.Empty;

                if (ISEDIT)
                {
                    var obj = new
                    {
                        NHOMPHIEUTHUID = NHOMPHIEUTHUID,
                        MANHOMPHIEUTHU = txtMANHOMPHIEUTHU.Text,
                        TONGSOPHIEUTHU = txtTONGSOPHIEUTHU.Text,
                        SOPHIEUFROM = txtSOPHIEUFROM.Text,
                        SOPHIEUTO = txtSOPHIEUTO.Text,
                        GHICHU = txtGhiChu.Text,
                        KHOASOPHIEUTU = txtKHOASOPHIEUTU.Text,
                        MASOCHA = txtMaSoCha.Text,
                        HINHTHUCTHANHTOAN = cboHINHTHUCTHANHTOAN.SelectValue,
                        LOAIPHIEUTHU = cboLOAIPHIEUTHU.SelectValue,
                        KIEUTHU = cboKIEUTHU.SelectValue,
                        ISLOCK = daKhoa,
                        SOAO = soAo,
                        MACDINH = macDinh,
                        PHONGID = Const.local_phongId.ToString()
                    };

                    objData = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
                }
                else
                {
                    var obj1 = new
                    {
                        MANHOMPHIEUTHU = txtMANHOMPHIEUTHU.Text,
                        TONGSOPHIEUTHU = txtTONGSOPHIEUTHU.Text,
                        SOPHIEUFROM = txtSOPHIEUFROM.Text,
                        SOPHIEUTO = txtSOPHIEUTO.Text,
                        GHICHU = txtGhiChu.Text,
                        KHOASOPHIEUTU = txtKHOASOPHIEUTU.Text,
                        MASOCHA = txtMaSoCha.Text,
                        HINHTHUCTHANHTOAN = cboHINHTHUCTHANHTOAN.SelectValue,
                        LOAIPHIEUTHU = cboLOAIPHIEUTHU.SelectValue,
                        KIEUTHU = cboKIEUTHU.SelectValue,
                        ISLOCK = daKhoa,
                        SOAO = soAo,
                        MACDINH = macDinh,
                        PHONGID = Const.local_phongId.ToString()
                    };

                    objData = JsonConvert.SerializeObject(obj1).Replace("\"", "\\\"");
                }

                string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T002.07", objData);
                LOADINGFLAG = false;

                if ("1".Equals(fl))
                {
                    InitControl();
                    LoadGridData(1);
                    ISEDIT = false;
                    ISBLOCK = false;
                    SetStatusButton(true, false, false, false, false, false);
                }
                else if ("0".Equals(fl))
                {
                    MessageBox.Show("Mã sổ đã tồn tại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else if ("2".Equals(fl))
                {
                    LoadGridData(1);
                }
                else if ("9".Equals(fl))
                {
                    MessageBox.Show("Đã tồn tại loại sổ này", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else if ("99".Equals(fl))
                {
                    MessageBox.Show("Chưa chọn khoa phòng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else if ("999".Equals(fl))
                {
                    MessageBox.Show("Không thể tạo sổ hóa đơn là sổ ảo", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private bool ValidateForm(string soAo)
        {
            int value;
            string message = string.Empty;
            bool isMaSo = false;
            bool isTongSoPhieu = false;
            bool isMaPhieuTu = false;
            bool isMaPhieuDen = false;
            bool isKhoaSoPhieuTu = false;

            if (string.IsNullOrWhiteSpace(txtMANHOMPHIEUTHU.Text))
            {
                message = "Mã sổ không được để trống hoặc chỉ bao gồm các dấu cách.";
                isMaSo = true;
            }


            if (txtMANHOMPHIEUTHU.Text.Trim().Length > 10)
            {
                message = "Giá trị Mã sổ phải chứa giá trị là chuỗi tối đa 10 ký tự.";
                isMaSo = true;
            }


            if (string.IsNullOrWhiteSpace(txtTONGSOPHIEUTHU.Text)
                || !int.TryParse(txtTONGSOPHIEUTHU.Text, out value)
                || int.Parse(txtTONGSOPHIEUTHU.Text) <= 0)
            {
                message += "\n" + "Giá trị Tổng số phiếu phải là số lớn hơn 0.";
                isTongSoPhieu = true;
            }

            if (string.IsNullOrWhiteSpace(txtSOPHIEUFROM.Text)
                || !int.TryParse(txtSOPHIEUFROM.Text, out value)
                || int.Parse(txtSOPHIEUFROM.Text) <= 0)
            {
                message += "\n" + "Giá trị Số phiếu từ phải là số lớn hơn 0.";
                isMaPhieuTu = true;
            }

            if (string.IsNullOrWhiteSpace(txtSOPHIEUTO.Text)
                || !int.TryParse(txtSOPHIEUTO.Text, out value)
                || int.Parse(txtSOPHIEUTO.Text) <= 0)
            {
                message += "\n" + "Giá trị Số phiếu đén phải là số lớn hơn 0.";
                isMaPhieuDen = true;
            }

            if (!string.IsNullOrWhiteSpace(txtKHOASOPHIEUTU.Text)
                && (!int.TryParse(txtKHOASOPHIEUTU.Text, out value)
                || int.Parse(txtKHOASOPHIEUTU.Text) <= 0))
            {
                message += "\n" + "Giá trị Khóa số phiếu từ phải là số lớn hơn 0.";
                isKhoaSoPhieuTu = true;
            }

            if (txtMaSoCha.Text.Trim().Length > 10)
            {
                message += "\n" + "Giá trị Mã sổ cha phải chứa giá trị là chuỗi tối đa 10 ký tự.";
                isMaSo = true;
            }


            if (isMaSo)
            {
                MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMANHOMPHIEUTHU.Focus();
                return false;
            }
            else if (isTongSoPhieu)
            {
                MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTONGSOPHIEUTHU.Focus();
                return false;
            }
            else if (isMaPhieuTu || isMaPhieuDen)
            {
                MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSOPHIEUFROM.Focus();
                return false;
            }
            else if (isKhoaSoPhieuTu)
            {
                MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKHOASOPHIEUTU.Focus();
                return false;
            }

            if (Const.local_phongId <= 0)
            {
                MessageBox.Show("Chưa chọn khoa phòng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if ("1".Equals(cboKIEUTHU.SelectValue) && "1".Equals(soAo))
            {
                MessageBox.Show("Không thể tạo sổ hóa đơn là sổ ảo", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtKHOASOPHIEUTU.Text) && (int.TryParse(txtKHOASOPHIEUTU.Text, out value))
                || !string.IsNullOrWhiteSpace(MAPHIEUTHUMAX))
            {
                MAPHIEUTHUMAX = (string.IsNullOrWhiteSpace(MAPHIEUTHUMAX)) ? "0" : MAPHIEUTHUMAX;
                if (int.Parse(txtKHOASOPHIEUTU.Text) <= int.Parse(MAPHIEUTHUMAX)
                    || int.Parse(txtKHOASOPHIEUTU.Text) < int.Parse(txtSOPHIEUFROM.Text)
                    || int.Parse(txtKHOASOPHIEUTU.Text) > int.Parse(txtSOPHIEUTO.Text))
                {
                    MessageBox.Show("Khóa số phiếu từ phải nằm trong dải số của số hóa đơn và lớn hơn số phiếu lớn nhất đã sử dụng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            this.Enabled = true;
            ISBLOCK = true;
            ISEDIT = true;
            //SetStatusButton(false, false, false, true, true, true);
            txtMANHOMPHIEUTHU.Focus();

            SetEnabled(new List<SimpleButton>() { btnLuu, btnHuy }, new List<SimpleButton>() { btnThem, btnSua, btnXoa });

            txtGhiChu.Enabled = true;
            txtKHOASOPHIEUTU.Enabled = true;
            txtMaSoCha.Enabled = true;
            chkISLOCK.Enabled = true;
            chkSOAO.Enabled = true;
            chkMACDINH.Enabled = true;
            cboNHOMPHIEUTHUID_ORG.lookUpEdit.Enabled = true;

            if (Func.Parse(SOPHIEUSUDUNG) > 0)
            {
                txtMANHOMPHIEUTHU.Enabled = false;
                cboHINHTHUCTHANHTOAN.lookUpEdit.Enabled = false;
                cboKIEUTHU.lookUpEdit.Enabled = false;
                cboLOAIPHIEUTHU.lookUpEdit.Enabled = false;
                txtTONGSOPHIEUTHU.Enabled = false;
                txtSOPHIEUFROM.Enabled = false;
                txtSOPHIEUTO.Enabled = false;
            }
            else
            {
                txtMANHOMPHIEUTHU.Enabled = true;
                cboHINHTHUCTHANHTOAN.lookUpEdit.Enabled = true;
                cboKIEUTHU.lookUpEdit.Enabled = true;
                cboLOAIPHIEUTHU.lookUpEdit.Enabled = true;
                txtTONGSOPHIEUTHU.Enabled = true;
                txtSOPHIEUFROM.Enabled = true;
                txtSOPHIEUTO.Enabled = true;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            InitControl();
            LOADINGFLAG = false;
            ISEDIT = false;
            ISBLOCK = false;
            NHOMPHIEUTHUID = string.Empty;
            SOPHIEUSUDUNG = string.Empty;
            MAPHIEUTHUMAX = string.Empty;
            SetStatusButton(true, false, false, false, false, false);
        }

        private void btnKetChuyen_Click(object sender, EventArgs e)
        {
            try
            {

                int soPhieuConLai = Func.Parse(TONGSOPHIEUTHU) - Func.Parse(SOPHIEUSUDUNG);

                if (soPhieuConLai <= 0)
                {
                    MessageBox.Show("Sổ đã hết phiếu", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                if (dEditTu.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa nhập trường từ ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    dEditTu.Focus();
                    return;
                }
                if (dEditDen.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa nhập trường đến ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    dEditDen.Focus();
                    return;
                }

                if (dEditTu.DateTime > dEditDen.DateTime)
                {
                    MessageBox.Show("Trường từ ngày phải nhỏ hơn trường đến ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("VPI01T002.02",
                    new string[] { "[0]", "[1]" },
                    new string[] { NHOMPHIEUTHUID, Const.local_phongId.ToString() });

                if (dt.Rows.Count > 0)
                {
                    VPI01T002_danhsachsophieuthu form = new VPI01T002_danhsachsophieuthu();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetReturnData(ReturnData);
                    form.SetData(NHOMPHIEUTHUID, Const.local_phongId.ToString(), dataRow, dEditTu.Text, dEditDen.Text);
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không có sổ phiếu thu kết chuyển tới", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ReturnData(object sender, EventArgs e)
        {
            try
            {
                bool flag = (bool)sender;

                if (flag)
                {
                    LoadGridDataCT(1);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void dEditTu_TextChanged(object sender, EventArgs e)
        {
            TienKetChuyen();
        }

        private void dEditDen_TextChanged(object sender, EventArgs e)
        {
            TienKetChuyen();
        }

        private List<MenuFunc> MenuPopupPhieuThuContextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Phiếu thu lỗi", "", "", ""));

            listMenu.Add(new MenuFunc("Thêm phiếu lỗi trước", "themPhieuLoiTruoc", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Thêm phiếu lỗi sau", "themPhieuLoiSau", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Xóa phiếu lỗi", "xoaPhieuLoi", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Phiếu kết chuyển", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa Phiếu Kết Chuyển", "xoaPhieuKetChuyen", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Cập nhật phiếu thu", "", "", ""));

            listMenu.Add(new MenuFunc("Cập nhật số phiếu", "capNhatSoPhieu", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void MenuPopupClickPhieuThu(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGridPhieuThuChiTiet.gridView.GetFocusedRow());

                if (drv != null)
                {
                    if ("themPhieuLoiTruoc".Equals(menu.hlink))
                    {
                        string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T002.11", drv.Row["PHIEUTHUID"].ToString());
                        if ("-1".Equals(fl))
                        {
                            MessageBox.Show("Có lỗi xảy ra", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Thêm phiếu lỗi trước thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        LoadGridDataCT(1);
                    }
                    else if ("themPhieuLoiSau".Equals(menu.hlink))
                    {
                        string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T002.SAU", drv.Row["PHIEUTHUID"].ToString());
                        if ("-1".Equals(fl))
                        {
                            MessageBox.Show("Có lỗi xảy ra", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Thêm phiếu lỗi sau thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        LoadGridDataCT(1);
                    }
                    else if ("xoaPhieuLoi".Equals(menu.hlink))
                    {
                        string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T002.12", drv.Row["PHIEUTHUID"].ToString());
                        if ("0".Equals(fl))
                        {
                            MessageBox.Show("Xóa không thành công, phiếu đã chọn không phải phiếu lỗi", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                        else if ("-1".Equals(fl))
                        {
                            MessageBox.Show("Có lỗi xảy ra", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Xóa phiếu lỗi thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        LoadGridDataCT(1);
                    }
                    else if ("xoaPhieuKetChuyen".Equals(menu.hlink))
                    {
                        VPI01T002_xoaphieuketchuyen form = new VPI01T002_xoaphieuketchuyen();
                        form.SetData(drv.Row["PHIEUTHUID"].ToString());
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.SetReturnData(ReturnData);
                        form.ShowDialog();
                    }
                    else if ("capNhatSoPhieu".Equals(menu.hlink))
                    {
                        if (LOADINGFLAG) return;
                        VPI01T002_capnhatsophieu form = new VPI01T002_capnhatsophieu();
                        form.SetData(drv.Row["PHIEUTHUID"].ToString(), drv.Row["MAPHIEUTHU"].ToString(), drv.Row["PHIEUTHULOG"].ToString());
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.SetReturnData(ReturnData);
                        form.ShowDialog();
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Xác nhận xóa", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T002.09", NHOMPHIEUTHUID);
                    if ("1".Equals(fl))
                    {
                        LoadGridData(1);
                    }
                    else if ("0".Equals(fl))
                    {
                        MessageBox.Show("Sổ đã sử dụng, không được xóa", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Xảy ra lỗi", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        return;
                    }
                    InitControl();
                    LoadGridData(1);
                    SetStatusButton(true, false, false, false, false, false);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void VPI01T002_sophieuthu_Shown(object sender, EventArgs e)
        {
            txtTimKiem.Focus();
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkISLOCK_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}