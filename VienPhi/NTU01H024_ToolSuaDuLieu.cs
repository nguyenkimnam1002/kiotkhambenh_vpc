using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H024_ToolSuaDuLieu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private const string CHUYEN_PHONG_CD = "Đổi phòng chỉ định";
        private const string CHUYEN_PHONG_TH = "Đổi phòng thực hiện";
        private static string SELECTED_VALUE;
        private static string SELECTED_TEXT;
        private static bool ISINITIAL_TABDV;
        private static bool ISINITIAL_TABHC;
        private static bool ISINITIAL_DV;
        private static bool ISINITIAL_HC;
        private static bool ISDTKHAIBAO;
        private static bool ISBENHPHU;
        private static DataRow DATA_ROW_PHIEU;
        private static DataRow DATA_ROW_HANHCHINH;
        private static DataRow DATA_ROW_KHOADIEUTRI;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NTU01H024_ToolSuaDuLieu()
        {
            InitializeComponent();
        }

        private void NTU01H024_ToolSuaDuLieu_Load(object sender, EventArgs e)
        {
            try
            {
                ISINITIAL_TABDV = true;
                ISINITIAL_TABHC = true;
                tabToolSuaDuLieu.SelectedPageChanged += tabToolSuaDuLieu_SelectedPageIndexChanged;
                tabToolSuaDuLieu_SelectedPageIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void tabToolSuaDuLieu_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabToolSuaDuLieu.SelectedTabPage == tabDSDV)
                {
                    if (ISINITIAL_TABDV)
                    {
                        ISINITIAL_DV = true;
                        SetGridDSDichVu();
                        ISINITIAL_TABDV = false;
                    }
                }
                else
                {
                    if (ISINITIAL_TABHC)
                    {
                        ISINITIAL_HC = true;
                        ISDTKHAIBAO = false;

                        SetGridHanhChinh();
                        SetGridThongTinKhoaDieuTri();
                        SetCombobox();
                        ISINITIAL_TABHC = false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void bbtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #region DS dịch vụ
        private void SetGridDSDichVu()
        {
            try
            {
                ucGridPhieu.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridPhieu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridPhieu.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                ucGridPhieu.gridView.OptionsBehavior.Editable = false;

                ucGridPhieu.setEvent(LoadGridPhieuThu);
                ucGridPhieu.SetReLoadWhenFilter(true);
                ucGridPhieu.setEvent_FocusedRowChanged(GridPhieu_Click);
                ucGridPhieu.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void LoadGridPhieuThu(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadGridData(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void LoadGridData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                DATA_ROW_PHIEU = null;

                ResponsList responses = new ResponsList();

                DataTable phieuThuDT = new DataTable();

                if (!ISINITIAL_DV)
                {
                    string soPhieu = (string.IsNullOrWhiteSpace(txtSoPhieu.Text)) ? "-1" : txtSoPhieu.Text;
                    string maBN = (string.IsNullOrWhiteSpace(txtMaBenhNhan.Text)) ? "-1" : txtMaBenhNhan.Text;
                    string maBA = (string.IsNullOrWhiteSpace(txtMaBenhAn.Text)) ? "-1" : txtMaBenhAn.Text;
                    string radioValue = (rdTimTatCa.Checked) ? "1" : "2";

                    responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                        "NTU01H024.EV001", page, ucGridPhieu.ucPage1.getNumberPerPage(),
                        new string[] { "[0]", "[1]", "[2]", "[3]" },
                        new string[] { soPhieu, maBN, maBA, radioValue }, ucGridPhieu.jsonFilter());
                }

                ucGridPhieu.clearData();

                phieuThuDT = MyJsonConvert.toDataTable(responses.rows);

                if (phieuThuDT.Rows.Count == 0)
                    phieuThuDT = Func.getTableEmpty(new String[] { "RN", "SOPHIEU", "NGAYMAUBENHPHAM", "OFFICER_NAME", "KHOAPHIEU", "KHOABENHNHAN", "TEN_TRANGTHAI", "TENBENHNHAN", "MABENHNHAN", "MABENHAN" });

                ucGridPhieu.setData(phieuThuDT, responses.total, responses.page, responses.records);
                ucGridPhieu.setColumnAll(false);

                ucGridPhieu.setColumn("RN", 0, " ", 0);
                ucGridPhieu.setColumn("SOPHIEU", 1, "Số phiếu", 0);
                ucGridPhieu.setColumn("NGAYMAUBENHPHAM", 2, "Ngày phiếu", 0);
                ucGridPhieu.setColumn("OFFICER_NAME", 3, "Bác sĩ chỉ định", 0);
                ucGridPhieu.setColumn("KHOAPHIEU", 4, "Khoa của phiếu", 0);
                ucGridPhieu.setColumn("KHOABENHNHAN", 5, "Khoa của BN", 0);
                ucGridPhieu.setColumn("TEN_TRANGTHAI", 6, "Loại phiếu", 0);
                ucGridPhieu.setColumn("TENBENHNHAN", 7, "Tên BN", 0);
                ucGridPhieu.setColumn("MABENHNHAN", 8, "Mã BN", 0);
                ucGridPhieu.setColumn("MABENHAN", 9, "Mã BA", 0);

                ucGridPhieu.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void GridPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                DATA_ROW_PHIEU = ucGridPhieu.gridView.GetFocusedDataRow();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnTimKiemDV_Click(object sender, EventArgs e)
        {
            ISINITIAL_DV = false;
            LoadGridData(1);
        }

        private void btnChuyenPhongCD_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_PHIEU != null)
                {
                    NTU01H025_DoiPhongChiDinh form = new NTU01H025_DoiPhongChiDinh();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetReturnData(ReturnData);
                    form.SetData(CHUYEN_PHONG_CD, DATA_ROW_PHIEU["MAUBENHPHAMID"].ToString(), "0");
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnChuyenBS_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_PHIEU != null)
                {
                    NTU01H026_DoiBacSy form = new NTU01H026_DoiBacSy();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetReturnData(ReturnData);
                    form.SetData(DATA_ROW_PHIEU["KHOAID"].ToString(), DATA_ROW_PHIEU["MAUBENHPHAMID"].ToString());
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
            }

        }

        private void btnChuyenThoiGian_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_PHIEU != null)
                {
                    NTU01H027_DoiGioChiDinh form = new NTU01H027_DoiGioChiDinh();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetReturnData(ReturnData);
                    form.SetData(DATA_ROW_PHIEU["MAUBENHPHAMID"].ToString(), DATA_ROW_PHIEU["NGAYMAUBENHPHAM"].ToString(), DATA_ROW_PHIEU["NGAYMAUBENHPHAM_HOANTHANH"].ToString());
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnChuyenPhongTH_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_PHIEU != null)
                {
                    NTU01H025_DoiPhongChiDinh form = new NTU01H025_DoiPhongChiDinh();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetReturnData(ReturnData);
                    form.SetData(CHUYEN_PHONG_TH, DATA_ROW_PHIEU["MAUBENHPHAMID"].ToString(), "1");
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnSuaPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_PHIEU != null)
                {
                    NTU01H036_Suaphieu form = new NTU01H036_Suaphieu();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetData(DATA_ROW_PHIEU["KHOAID"].ToString(), DATA_ROW_PHIEU["MAUBENHPHAMID"].ToString());
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnSuaBSTH_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_PHIEU != null)
                {
                    var loaiMauBenhPham = DATA_ROW_PHIEU["LOAINHOMMAUBENHPHAM"].ToString();

                    if ("1".Equals(loaiMauBenhPham) || "2".Equals(loaiMauBenhPham))
                    {
                        NTU01H038_SuaBacSiTH form = new NTU01H038_SuaBacSiTH();
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.SetData(DATA_ROW_PHIEU["MAUBENHPHAMID"].ToString(), loaiMauBenhPham);
                        form.ShowDialog();
                    }
                    else if ("5".Equals(loaiMauBenhPham))
                    {
                        NTU01H038_SuaBacSiTH1 form = new NTU01H038_SuaBacSiTH1();
                        form.StartPosition = FormStartPosition.CenterScreen;
                        form.SetData(DATA_ROW_PHIEU["MAUBENHPHAMID"].ToString(), loaiMauBenhPham);
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Chỉ sửa bác sĩ thực hiện cho các phiếu cận lâm sàng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void rdTimTatCa_CheckedChanged(object sender, EventArgs e)
        {
            SetStatusTextbox(false);
        }

        private void rdTimTheoDieuKien_CheckedChanged(object sender, EventArgs e)
        {
            SetStatusTextbox(true);
        }

        private void SetStatusTextbox(bool isEnable)
        {
            txtSoPhieu.Enabled = isEnable;
            txtMaBenhNhan.Enabled = isEnable;
        }

        private void ReturnData(object sender, EventArgs e)
        {
            try
            {
                bool flag = (bool)sender;

                if (flag)
                {
                    LoadGridData(1);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData(1);
        }
        #endregion

        #region Hành chính
        private void SetGridHanhChinh()
        {
            try
            {
                ucGridHanhChinh.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridHanhChinh.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridHanhChinh.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridHanhChinh.gridView.OptionsBehavior.Editable = false;

                ucGridHanhChinh.setEvent(LoadGridHanhChinh);
                ucGridHanhChinh.onIndicator();
                ucGridHanhChinh.SetReLoadWhenFilter(true);
                ucGridHanhChinh.setEvent_FocusedRowChanged(GridHanhChinh_Click);
                ucGridHanhChinh.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void GridHanhChinh_Click(object sender, EventArgs e)
        {
            try
            {
                ISINITIAL_HC = false;
                DATA_ROW_HANHCHINH = ucGridHanhChinh.gridView.GetFocusedDataRow();
                if ("2".Equals(DATA_ROW_HANHCHINH["TRANGTHAITIEPNHAN"].ToString()))
                {
                    string message = "Bệnh nhân chưa gỡ duyệt bảo hiểm. Vui lòng thực hiện gỡ duyệt bảo \n";
                    message += "hiểm để cập nhật thông tin";
                    MessageBox.Show(message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                LoadGridThongTinKhoaDieuTri(null, null);

                GetThongTinHanhChinh();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void SetGridThongTinKhoaDieuTri()
        {
            try
            {
                ucGridThongTinDieuTri.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridThongTinDieuTri.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridThongTinDieuTri.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridThongTinDieuTri.gridView.OptionsBehavior.Editable = false;

                ucGridThongTinDieuTri.setEvent(LoadGridThongTinKhoaDieuTri);
                ucGridThongTinDieuTri.SetReLoadWhenFilter(true);
                ucGridThongTinDieuTri.setEvent_FocusedRowChanged(GridThongTinKhoaDieuTri_Click);
                ucGridThongTinDieuTri.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void LoadGridHanhChinh(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadGridHCData(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void LoadGridHCData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                DATA_ROW_HANHCHINH = null;

                ResponsList responses = new ResponsList();

                DataTable hanhChinhDT = new DataTable();

                if (!ISINITIAL_HC)
                {
                    string vaoVienTuNgay = (string.IsNullOrWhiteSpace(dEditVaoVienTu.Text)) ? "-1" : dEditVaoVienTu.Text;
                    string vaoVienDenNgay = (string.IsNullOrWhiteSpace(dEditVaoVienDen.Text)) ? "-1" : dEditVaoVienDen.Text;
                    string raVienTuNgay = (string.IsNullOrWhiteSpace(dEditRaVienTu.Text)) ? "-1" : dEditRaVienTu.Text;
                    string raVienDenNgay = (string.IsNullOrWhiteSpace(dEditRaVienDen.Text)) ? "-1" : dEditRaVienDen.Text;

                    responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                        "NTU01H024.EV006", page, ucGridHanhChinh.ucPage1.getNumberPerPage(),
                        new string[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]", "[9]" },
                        new string[] { vaoVienTuNgay, vaoVienDenNgay, raVienTuNgay, raVienDenNgay, txtMaBN.Text, txtHoTen.Text, txtMaChanDoan.Text, txtRaVien.Text, txtMaBA.Text, txtMaBHYT.Text },
                        ucGridHanhChinh.jsonFilter());
                }

                ucGridHanhChinh.clearData();

                hanhChinhDT = MyJsonConvert.toDataTable(responses.rows);

                if (hanhChinhDT.Rows.Count == 0)
                    hanhChinhDT = Func.getTableEmpty(new String[] { "MAHOSOBENHAN", "MABENHNHAN", "TENBENHNHAN", "GIOITINH", "NGAYSINH", "MA_BHYT", "DIACHI", "NGAYVAOVIEN", "NGAYHOSORAVIEN", "CHANDOANRAVIEN" });

                ucGridHanhChinh.setData(hanhChinhDT, responses.total, responses.page, responses.records);
                ucGridHanhChinh.setColumnAll(false);

                //ucGridHanhChinh.setColumn("RN", 0, " ", 0);
                ucGridHanhChinh.setColumn("MAHOSOBENHAN", 1, "Mã BA", 0);
                ucGridHanhChinh.setColumn("MABENHNHAN", 2, "Mã BN", 0);
                ucGridHanhChinh.setColumn("TENBENHNHAN", 3, "Tên BN", 0);
                ucGridHanhChinh.setColumn("GIOITINH", 4, "Giới tính", 0);
                ucGridHanhChinh.setColumn("NGAYSINH", 5, "Ngày sinh", 0);
                ucGridHanhChinh.setColumn("MA_BHYT", 6, "Mã BHYT", 0);
                ucGridHanhChinh.setColumn("DIACHI", 7, "Địa chỉ", 0);
                ucGridHanhChinh.setColumn("NGAYVAOVIEN", 8, "Ngày vào viện", 0);
                ucGridHanhChinh.setColumn("NGAYHOSORAVIEN", 9, "Ngày ra viện", 0);
                ucGridHanhChinh.setColumnMemoEdit("CHANDOANRAVIEN", 10, "Chẩn đoán", 0);

                ucGridHanhChinh.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnTimKiemHC_Click(object sender, EventArgs e)
        {
            ISINITIAL_HC = false;
            LoadGridHCData(1);
        }

        private void btnMoTrangThai_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_HANHCHINH != null)
                {
                    string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV022", DATA_ROW_HANHCHINH["TIEPNHANID"].ToString());
                    if ("1".Equals(result))
                    {
                        MessageBox.Show("Cập nhật trạng thái thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if ("2".Equals(result))
                    {
                        MessageBox.Show("Bệnh nhân có trạng thái đúng. Không thể cập nhật", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else if ("0".Equals(result))
                    {
                        MessageBox.Show("Cập nhật trạng thái thất bại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn bệnh nhân", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnSuaTheBHYT_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_HANHCHINH != null)
                {
                    NTU01H037_SuaTheBhyt form = new NTU01H037_SuaTheBhyt();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetData(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString());
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Chưa chọn bệnh nhân", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnChinhSuaGiuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW_HANHCHINH != null)
                {
                    NTU01H039_ChinhSuaGiuong form = new NTU01H039_ChinhSuaGiuong();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.SetReturnData(ReturnData);
                    form.SetData(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString(), DATA_ROW_HANHCHINH["TIEPNHANID"].ToString(), DATA_ROW_HANHCHINH["BENHNHANID"].ToString());
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Chưa chọn bệnh nhân", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnRefreshHC_Click(object sender, EventArgs e)
        {
            LoadGridHCData(1);
        }

        private void LoadGridThongTinKhoaDieuTri(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadGridTTData(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void LoadGridTTData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                DATA_ROW_KHOADIEUTRI = null;

                ResponsList responses = new ResponsList();

                DataTable hanhChinhDT = new DataTable();

                if (!ISINITIAL_HC)
                {
                    string vaoVienTuNgay = (string.IsNullOrWhiteSpace(dEditVaoVienTu.Text)) ? "-1" : dEditVaoVienTu.Text;
                    string vaoVienDenNgay = (string.IsNullOrWhiteSpace(dEditVaoVienDen.Text)) ? "-1" : dEditVaoVienDen.Text;
                    string raVienTuNgay = (string.IsNullOrWhiteSpace(dEditRaVienTu.Text)) ? "-1" : dEditRaVienTu.Text;
                    string raVienDenNgay = (string.IsNullOrWhiteSpace(dEditRaVienDen.Text)) ? "-1" : dEditRaVienDen.Text;

                    responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                        "NTU01H024.EV005", page, ucGridThongTinDieuTri.ucPage1.getNumberPerPage(),
                        new string[] { "[0]" },
                        new string[] { DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString() }, ucGridThongTinDieuTri.jsonFilter());
                }

                ucGridThongTinDieuTri.clearData();

                hanhChinhDT = MyJsonConvert.toDataTable(responses.rows);

                if (hanhChinhDT.Rows.Count == 0)
                    hanhChinhDT = Func.getTableEmpty(new String[] { "RN", "MATIEPNHAN", "KHOA", "PHONG", "THOIGIANBD", "THOIGIANKT", "TRANGTHAI" });

                ucGridThongTinDieuTri.setData(hanhChinhDT, responses.total, responses.page, responses.records);
                ucGridThongTinDieuTri.setColumnAll(false);

                ucGridThongTinDieuTri.setColumn("RN", 0, " ", 0);
                ucGridThongTinDieuTri.setColumn("MATIEPNHAN", 1, "Mã BA", 0);
                ucGridThongTinDieuTri.setColumn("KHOA", 2, "Mã BN", 0);
                ucGridThongTinDieuTri.setColumn("PHONG", 3, "Tên BN", 0);
                ucGridThongTinDieuTri.setColumn("THOIGIANBD", 4, "Giới tính", 0);
                ucGridThongTinDieuTri.setColumn("THOIGIANKT", 5, "Ngày sinh", 0);
                ucGridThongTinDieuTri.setColumn("TRANGTHAI", 6, "Mã BHYT", 0);

                ucGridThongTinDieuTri.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void GetThongTinHanhChinh()
        {
            try
            {
                DataTable thongTinDT = RequestHTTP.call_ajaxCALL_SP_O("NTU01H024.EV007", DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString(), 0);
                if (thongTinDT != null && thongTinDT.Rows.Count > 0)
                {
                    var dataRow = thongTinDT.Rows[0];

                    if (!string.IsNullOrWhiteSpace(dataRow["VAOVIEN"].ToString()))
                        dEditVaoVien.DateTime = DateTime.ParseExact(dataRow["VAOVIEN"].ToString(), Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
                    else
                        dEditVaoVien.EditValue = DBNull.Value;

                    if (!string.IsNullOrWhiteSpace(dataRow["RAVIEN"].ToString()))
                        dEditRaVien.DateTime = DateTime.ParseExact(dataRow["RAVIEN"].ToString(), Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
                    else
                        dEditRaVien.EditValue = DBNull.Value;

                    txtDiaChiBN.Text = dataRow["DIACHI"].ToString();
                    txtTenBNHC.Text = dataRow["TENBENHNHANUD"].ToString();

                    if (!string.IsNullOrWhiteSpace(dataRow["NGAYSINH"].ToString()))
                        dEditNgaySinh.DateTime = DateTime.ParseExact(dataRow["NGAYSINH"].ToString(), Const.FORMAT_date1, CultureInfo.InvariantCulture);
                    else
                        dEditNgaySinh.EditValue = DBNull.Value;

                    txtNamSinh.Text = dataRow["NAMSINH"].ToString();
                    txtTuoi.Text = dataRow["TUOI"].ToString();

                    int value;
                    if (!string.IsNullOrWhiteSpace(dataRow["DVTUOI"].ToString())
                        && int.TryParse(dataRow["DVTUOI"].ToString(), out value))
                        cbbTuoi.SelectedIndex = int.Parse(dataRow["DVTUOI"].ToString()) - 1;

                    cbbGioiTinh.SelectValue = dataRow["GIOITINH"].ToString();
                    txtNguoiNha.Text = dataRow["NGUOITHAN"].ToString();
                    txtTenNguoiNha.Text = dataRow["TENNGUOITHAN"].ToString();
                    txtSDTNguoiNha.Text = dataRow["DIENTHOAINGUOITHAN"].ToString();
                    txtDCNguoiNha.Text = dataRow["DIACHINGUOITHAN"].ToString();
                    cbbVaoTu.SelectValue = dataRow["HINHTHUCVAOVIENID"].ToString();
                    ucNoiCD.SelectedValue = dataRow["TKNOICD"].ToString();
                    ucBenhChinh.SelectedValue = dataRow["TKCHANDOANRAVIENID"].ToString();
                    ucBenhPhu.SelectedText = dataRow["CHANDOANRAVIEN_KEMTHEO"].ToString();

                    GetDiaChi(dataRow["DIAPHUONGID"].ToString());
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void GetDiaChi(string diaPhuongID)
        {
            try
            {
                DataTable diaChiDT = RequestHTTP.call_ajaxCALL_SP_O("COM.DIACHI", diaPhuongID);
                if (diaChiDT != null && diaChiDT.Rows.Count > 0)
                {
                    DataRowView drv = null;
                    var row = diaChiDT.Rows[0];
                    if (!"-1".Equals(row["ID_TINH"].ToString()))
                    {
                        var dataTable = RequestHTTP.Cache_getTinhTP(true);
                        var dataRows = dataTable.Select(string.Format("{0} = '{1}'", "col1", row["ID_TINH"].ToString()));
                        cbbTinh.SelectValue = dataRows[0]["col3"].ToString();

                        string valueHuyen = string.Empty;
                        if (!"-1".Equals(row["ID_HUYEN"].ToString()))
                        {
                            drv = cbbTinh.SelectDataRowView;
                            if (drv != null)
                            {
                                if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                                {
                                    string id_tinh = drv["col1"].ToString();
                                    dataTable = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_tinh);
                                    dataRows = dataTable.Select(string.Format("{0} = '{1}'", "col1", row["ID_HUYEN"].ToString()));
                                    valueHuyen = dataRows[0]["col3"].ToString();
                                }
                            }
                        }

                        cbbHuyen.SelectValue = valueHuyen;

                        string valueXa = string.Empty;
                        if (!"-1".Equals(row["ID_XA"].ToString()))
                        {
                            drv = cbbHuyen.SelectDataRowView;
                            if (drv != null)
                            {
                                if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                                {
                                    string id_huyen = drv["col1"].ToString();

                                    dataTable = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_huyen);
                                    dataTable.Columns["col3"].SetOrdinal(0);
                                    dataTable.Columns["col2"].SetOrdinal(1);
                                    dataTable.Columns["col1"].SetOrdinal(2);
                                    dataTable = SetCaptionTinhHuyenXaCombobox(dataTable);
                                    cbbXa.setData(dataTable, "col3", "col2");
                                    cbbXa.setColumn("col3", 0, "STT", 0);
                                    cbbXa.setColumn("col2", 1, "Xã(P)", 0);
                                    cbbXa.setColumn("col1", -1, "Mã xã", 0);
                                    cbbXa.setColumn("col4", -1, "", 0);

                                    dataRows = dataTable.Select(string.Format("{0} = '{1}'", "col1", row["ID_XA"].ToString()));
                                    valueXa = dataRows[0]["col3"].ToString();
                                }
                            }
                        }
                        cbbXa.SelectValue = valueXa;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void GridThongTinKhoaDieuTri_Click(object sender, EventArgs e)
        {
            try
            {
                DATA_ROW_KHOADIEUTRI = ucGridThongTinDieuTri.gridView.GetFocusedDataRow();

                if (!string.IsNullOrWhiteSpace(DATA_ROW_KHOADIEUTRI["KHAMBENHID"].ToString()))
                {
                    DataTable data = RequestHTTP.call_ajaxCALL_SP_O("NTU01H024.EV014", DATA_ROW_KHOADIEUTRI["KHAMBENHID"].ToString());
                    if (data != null && data.Rows.Count > 0)
                    {
                        var row = data.Rows[0];
                        if (!string.IsNullOrWhiteSpace(row["RAKHOA"].ToString()))
                            dEditRaKhoa.DateTime = DateTime.ParseExact(row["RAKHOA"].ToString(), Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
                        if (!string.IsNullOrWhiteSpace(row["VAOKHOA"].ToString()))
                            dEditVaoKhoa.DateTime = DateTime.ParseExact(row["VAOKHOA"].ToString(), Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void SetCombobox()
        {
            try
            {
                // Giới tính
                DataTable gioiTinhDT = RequestHTTP.get_ajaxExecuteQuery("NT.0010", new string[] { "[0]" }, new string[] { "1" });
                cbbGioiTinh.setData(gioiTinhDT, 0, 1);
                cbbGioiTinh.SelectIndex = 0;
                cbbGioiTinh.setColumn(0, false);

                // Vào từ
                DataTable vaoVienDT = RequestHTTP.get_ajaxExecuteQuery("NT.0010", new string[] { "[0]" }, new string[] { "4" });
                cbbVaoTu.setData(vaoVienDT, 0, 1);
                cbbVaoTu.SelectIndex = 0;
                cbbVaoTu.setColumn(0, false);

                // Nơi chuyển đến
                DataTable noiCD_DT = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NT.009", new string[] { });
                ucNoiCD.setData(noiCD_DT, "BENHVIENKCBBD", "TENBENHVIEN");
                ucNoiCD.setColumn("RN", -1, "", 0);
                ucNoiCD.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 0);
                ucNoiCD.setColumn("TENBENHVIEN", 1, "Bệnh viện", 0);
                ucNoiCD.setColumn("DIACHI", 2, "Địa chỉ", 0);

                // ĐTBHYT
                DataTable data = RequestHTTP.call_ajaxCALL_SP_O("COM.DS_CAUHINH", "HIS_SUDUNG_DOITUONG_KHAIBAO");
                if (data != null && data.Rows.Count > 0)
                {
                    var row = data.Rows[0];
                    if ("1".Equals(row["HIS_SUDUNG_DOITUONG_KHAIBAO"].ToString()))
                    {
                        ISDTKHAIBAO = true;
                        DataTable doituongBNDT = RequestHTTP.get_ajaxExecuteQuery("NT.007.01", new string[] { }, new string[] { });
                        cbbDTBHYT.setData(doituongBNDT, 0, 1);
                        //cbbDTBHYT.SelectIndex = -1;
                        cbbDTBHYT.setColumn(0, false);
                    }
                }

                DataTable benh_DT = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NT.008", new string[] { });

                // Bệnh chính
                ucBenhChinh.setData(benh_DT, "ICD10CODE", "ICD10NAME");
                ucBenhChinh.setColumn("RN", -1, "", 0);
                ucBenhChinh.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucBenhChinh.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

                // Bệnh phụ
                ucBenhPhu.searchLookUpEdit1.EditValueChanged += ucBenhPhu_EditValueChanged;
                ucBenhPhu.setData(benh_DT, "ICD10CODE", "ICD10NAME");
                ucBenhPhu.setColumn("RN", -1, "", 0);
                ucBenhPhu.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucBenhPhu.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
                ISBENHPHU = true;

                // Tỉnh
                DataTable dt = RequestHTTP.Cache_getTinhTP(true);
                dt.Columns["col3"].SetOrdinal(0);
                dt.Columns["col2"].SetOrdinal(1);
                dt.Columns["col1"].SetOrdinal(2);
                cbbTinh.setEvent(cboTinh_SelectedIndexChanged);
                dt = SetCaptionTinhHuyenXaCombobox(dt);
                cbbTinh.setData(dt, "col3", "col2");
                cbbTinh.setEvent_Enter(cbbTinh_KeyEnter);
                cbbTinh.setColumn("col3", 0, "STT", 0);
                cbbTinh.setColumn("col2", 1, "Tỉnh/TP", 0);
                cbbTinh.setColumn("col1", -1, "Mã tỉnh", 0);
                cbbTinh.setColumn("col4", -1, "", 0);

                // Huyện
                DataTable huyenXaDT = SetCaptionHuyenXaCombobox();
                cbbHuyen.setEvent(cboHuyen_SelectedIndexChanged);
                cbbHuyen.setData(huyenXaDT, "col3", "col2");
                cbbHuyen.setEvent_Enter(cbbHuyen_KeyEnter);

                // Xã
                cbbXa.setEvent(cboXa_SelectedIndexChanged);
                cbbXa.setData(huyenXaDT, "col3", "col2");
                cbbXa.setEvent_Enter(cbbXa_KeyEnter);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ucBenhPhu_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ISBENHPHU)
                {
                    SELECTED_TEXT = ucBenhPhu.SelectedText;
                    SELECTED_VALUE = ucBenhPhu.SelectedValue;
                }

                ISBENHPHU = false;
                ucBenhPhu.SelectedValue = string.Empty;
                ucBenhPhu.SelectedText = SELECTED_TEXT;
                ISBENHPHU = true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void txtSoNha_Leave(object sender, EventArgs e)
        {
            SetDiaChiBN();
        }

        private DataTable SetCaptionTinhHuyenXaCombobox(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["col3"] = string.Empty;
            dr["col2"] = "Chọn";
            dr["col1"] = string.Empty;
            dr["col4"] = string.Empty;
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        private DataTable SetCaptionHuyenXaCombobox()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("col3", typeof(string));
            dt.Columns.Add("col2", typeof(string));
            dt.Columns.Add("col1", typeof(string));
            dt.Columns.Add("col4", typeof(string));

            DataRow dr = dt.NewRow();
            dr["col3"] = string.Empty;
            dr["col2"] = "Chọn";
            dr["col1"] = string.Empty;
            dr["col4"] = string.Empty;
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        private void cboTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbbHuyen.clearData();
                cbbXa.clearData();
                SetDiaChiBN();
                DataRowView drv = cbbTinh.SelectDataRowView;
                DataTable dataTable = SetCaptionHuyenXaCombobox();
                if (drv != null)
                {
                    if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                    {
                        string id_tinh = drv["col1"].ToString();
                        DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_tinh);
                        dt.Columns["col3"].SetOrdinal(0);
                        dt.Columns["col2"].SetOrdinal(1);
                        dt.Columns["col1"].SetOrdinal(2);

                        dt = SetCaptionTinhHuyenXaCombobox(dt);
                        cbbHuyen.setData(dt, "col3", "col2");
                        cbbHuyen.setColumn("col2", 1, "Huyện(Q)", 0);
                        cbbHuyen.setColumn("col3", 0, "STT", 0);
                        cbbHuyen.setColumn("col1", -1, "Mã huyện", 0);
                        cbbHuyen.setColumn("col4", -1, "", 0);
                    }
                    else
                    {
                        cbbHuyen.setData(dataTable, "col3", "col2");
                    }

                    cbbXa.setData(dataTable, "col3", "col2");
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void cboHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbbXa.clearData();
                SetDiaChiBN();
                DataRowView drv = cbbHuyen.SelectDataRowView;
                if (drv != null)
                {
                    if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                    {
                        string id_huyen = drv["col1"].ToString();
                        DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_huyen);
                        dt.Columns["col3"].SetOrdinal(0);
                        dt.Columns["col2"].SetOrdinal(1);
                        dt.Columns["col1"].SetOrdinal(2);

                        dt = SetCaptionTinhHuyenXaCombobox(dt);
                        cbbXa.setData(dt, "col3", "col2");
                        cbbXa.setColumn("col3", 0, "STT", 0);
                        cbbXa.setColumn("col2", 1, "Xã(P)", 0);
                        cbbXa.setColumn("col1", -1, "Mã xã", 0);
                        cbbXa.setColumn("col4", -1, "", 0);
                    }
                    else
                    {
                        DataTable dataTable = SetCaptionHuyenXaCombobox();
                        cbbXa.setData(dataTable, "col3", "col2");
                    }

                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void cboXa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetDiaChiBN();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void SetDiaChiBN()
        {
            try
            {
                string diaChiBN = string.Empty;

                if (cbbTinh.SelectIndex >= 0 && !string.IsNullOrWhiteSpace(cbbTinh.SelectDataRowView.Row["col1"].ToString()))
                    diaChiBN += cbbTinh.SelectText;

                if (cbbHuyen.SelectIndex >= 0 && !string.IsNullOrWhiteSpace(cbbHuyen.SelectDataRowView.Row["col1"].ToString()))
                    diaChiBN += "-" + cbbHuyen.SelectText;

                if (cbbXa.SelectIndex >= 0 && !string.IsNullOrWhiteSpace(cbbXa.SelectDataRowView.Row["col1"].ToString()))
                    diaChiBN += "-" + cbbXa.SelectText;

                if (!string.IsNullOrWhiteSpace(txtSoNha.Text) && !string.IsNullOrWhiteSpace(diaChiBN))
                    diaChiBN = txtSoNha.Text + "-" + diaChiBN;

                if (diaChiBN.StartsWith("-")) diaChiBN = diaChiBN.Substring(1);
                txtDiaChiBN.Text = diaChiBN;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void cbbTinh_KeyEnter(object sender, EventArgs e)
        {
            cbbHuyen.Focus();
        }

        private void cbbHuyen_KeyEnter(object sender, EventArgs e)
        {
            cbbXa.Focus();
        }

        private void cbbXa_KeyEnter(object sender, EventArgs e)
        {
            txtDiaChiBN.Focus();
        }

        private void btnClearNoiCD_Click(object sender, EventArgs e)
        {
            ucNoiCD.SelectedValue = string.Empty;
        }

        private void txtCanNang_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnRefresh_KhoaDieuTri_Click(object sender, EventArgs e)
        {
            LoadGridTTData(1);
        }

        private void btnADVaoRaVien_Click(object sender, EventArgs e)
        {
            try
            {
                string tiepNhanID = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["TIEPNHANID"].ToString())) ? "" : DATA_ROW_HANHCHINH["TIEPNHANID"].ToString();
                string par = dEditVaoVien.Text + "$" + dEditRaVien.Text + "$" + tiepNhanID;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV008", par);

                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật thời gian ra vào viện thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật thời gian ra vào viện không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADDiaChi_Click(object sender, EventArgs e)
        {
            try
            {
                string diaPhuongId = string.Empty;
                if (!string.IsNullOrEmpty(cbbTinh.SelectValue))
                    diaPhuongId = cbbTinh.SelectDataRowView["col1"].ToString();
                else
                {
                    MessageBox.Show("Chưa chọn tỉnh", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                if (!string.IsNullOrEmpty(cbbHuyen.SelectValue))
                    diaPhuongId = cbbHuyen.SelectDataRowView["col1"].ToString();

                if (!string.IsNullOrEmpty(cbbXa.SelectValue))
                    diaPhuongId = cbbXa.SelectDataRowView["col1"].ToString();

                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string bnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["BENHNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["BENHNHANID"].ToString();
                string par = diaPhuongId + "$" + txtDiaChiBN.Text + "$" + hsbnId + "$" + bnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV009", par);

                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật địa chỉ bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật địa chỉ bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADTenBN_Click(object sender, EventArgs e)
        {
            try
            {
                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string bnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["BENHNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["BENHNHANID"].ToString();
                string par = txtTenBNHC.Text.Trim() + "$" + hsbnId + "$" + bnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV019", par);

                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật tên bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật tên bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADNamSinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNamSinh.Text) || string.IsNullOrEmpty(txtTuoi.Text) || string.IsNullOrEmpty(cbbTuoi.Text))
                {
                    MessageBox.Show("Chưa nhập/enter/tab dữ liệu ngày sinh để sinh dữ liệu năm sinh/tuổi", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string bnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["BENHNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["BENHNHANID"].ToString();
                string par = dEditNgaySinh.Text + "$" + txtNamSinh.Text + "$" + txtTuoi.Text + "$" + (cbbTuoi.SelectedIndex + 1).ToString() + "$" + hsbnId + "$" + bnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV010", par);

                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật ngày sinh/năm sinh/tuổi bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật ngày sinh/năm sinh/tuổi bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADGioiTinh_Click(object sender, EventArgs e)
        {
            try
            {
                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string bnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["BENHNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["BENHNHANID"].ToString();
                string par = cbbGioiTinh.SelectValue + "$" + hsbnId + "$" + bnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV017", par);

                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật giới tính bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật giới tính bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADNguoiNha_Click(object sender, EventArgs e)
        {
            try
            {
                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string bnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["BENHNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["BENHNHANID"].ToString();
                string par = txtNguoiNha.Text + "$" + txtTenNguoiNha.Text + "$" + txtSDTNguoiNha.Text + "$" + txtDCNguoiNha.Text + "$" + hsbnId + "$" + bnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV020", par);
                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật thông tin người nhà bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật thông tin người nhà bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADVaoTu_Click(object sender, EventArgs e)
        {
            try
            {
                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string par = cbbVaoTu.SelectValue + "$" + hsbnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV021", par);
                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật hình thức vào viện bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật hình thức vào viện bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADNoiCD_Click(object sender, EventArgs e)
        {
            try
            {
                string tiepNhanId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["TIEPNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["TIEPNHANID"].ToString();
                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string par = ucNoiCD.SelectedValue + "$" + tiepNhanId + "$" + hsbnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV016", par);
                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật nơi đăng ký khám chữa bệnh ban đầu, nơi chuyến đến bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else if ("2".Equals(result))
                    MessageBox.Show("Không tồn tại thẻ để cập nhật", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật nơi đăng ký khám chữa bệnh ban đầu, nơi chuyến đến bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADBenh_Click(object sender, EventArgs e)
        {
            try
            {
                string tiepNhanId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["TIEPNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["TIEPNHANID"].ToString();
                string hsbnId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["HOSOBENHANID"].ToString();
                string par = ucBenhChinh.SelectedValue + "$" + ucBenhChinh.SelectedText + "$" + ucBenhPhu.SelectedText + "$" + tiepNhanId + "$" + hsbnId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV013", par);

                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật bệnh chính, bệnh phụ bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật bệnh chính, bệnh phụ bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADCanNang_Click(object sender, EventArgs e)
        {
            try
            {
                var regex = new Regex(@"/^\-?[0-9]*\.?[0-9]+$/");
                var matches = regex.Matches(txtCanNang.Text);

                if (matches.Count > 0)
                {
                    MessageBox.Show("Giá trị cân nặng phải chứa giá trị là số và lớn hơn 0", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string tiepNhanId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["TIEPNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["TIEPNHANID"].ToString();
                string par = txtCanNang.Text + "$" + tiepNhanId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV023", par);

                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật cân nặng bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật cân nặng bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADDTBHYT_Click(object sender, EventArgs e)
        {
            try
            {
                if (ISDTKHAIBAO)
                {
                    string tiepNhanId = (string.IsNullOrWhiteSpace(DATA_ROW_HANHCHINH["TIEPNHANID"].ToString())) ? string.Empty : DATA_ROW_HANHCHINH["TIEPNHANID"].ToString();
                    var row = cbbDTBHYT.SelectDataRowView;
                    string par = row["col3"].ToString() + "$" + cbbDTBHYT.SelectValue + "$" + tiepNhanId;
                    string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV024", par);

                    if ("1".Equals(result))
                        MessageBox.Show("Cập nhật đối tượng BN khai báo thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    else if ("2".Equals(result))
                        MessageBox.Show("BN không phải là đối tượng này để cập nhật", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    else
                        MessageBox.Show("Cập nhật đối tượng BN khai báo không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                else
                {
                    MessageBox.Show("Hiện đang không cấu hình đối tượng khai báo nên không thể chuyển đối tượng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnADVaoRaKhoa_Click(object sender, EventArgs e)
        {
            try
            {
                string khamBenhId = (string.IsNullOrWhiteSpace(DATA_ROW_KHOADIEUTRI["KHAMBENHID"].ToString())) ? string.Empty : DATA_ROW_KHOADIEUTRI["KHAMBENHID"].ToString();
                string par = dEditVaoKhoa.Text + "$" + dEditRaKhoa.Text + "$" + khamBenhId;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV015", par);
                if ("1".Equals(result))
                    MessageBox.Show("Cập nhật thời gian ra vào khoa của bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                else
                    MessageBox.Show("Cập nhật thời gian ra vào khoa của bệnh nhân không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        #endregion
    }
}