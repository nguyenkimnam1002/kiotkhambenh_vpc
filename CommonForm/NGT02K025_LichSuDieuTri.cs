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

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K025_LichSuDieuTri : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        private string BENHNHANID = string.Empty;
        private string HIDBENHNHANID = string.Empty;
        private string HIDKHAMBENHID = string.Empty;
        private string HIDPHONGID = string.Empty;
        private string LNMBP_XetNghiem = "1";
        private string LNMBP_CDHA = "2";
        private string LNMBP_DieuTri = "4";
        private string LNMBP_ChuyenKhoa = "5";
        private string LNMBP_Phieuthuoc = "7";
        private string LNMBP_Phieuvattu = "8";
        private string LNMBP_ChamSoc = "9";
        private string LNMBP_TruyenDich = "13";
        private string LNMBP_TaoPhanUngThuoc = "14";
        private string LNMBP_HoiChan = "15";
        private string LNMBP_PhieuVanChuyen = "16";
        private string LNMBP_Phieusuatan = "11";
        private string _flgModeView = "1";
        private string HOSOBENHANID = string.Empty;

        public NGT02K025_LichSuDieuTri()
        {
            InitializeComponent();
        }

        public void setParam(string BENHNHANID)
        {
            this.BENHNHANID = BENHNHANID;
        }

        private void NGT02K025_LichSuDieuTri_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            try
            {
                loadDataComboBox();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void loadDataComboBox()
        {
            try
            {
                // load data comobox
                DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGT02K025.DSHS", new string[] { "[0]" }, new string[] { BENHNHANID });

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                ucCbbHoSoBA.setData(dt, 0, 1);
                ucCbbHoSoBA.setColumnAll(false);
                ucCbbHoSoBA.setColumn(1, true);
                ucCbbHoSoBA.SelectIndex = 0;
                ucCbbHoSoBA.setEvent(ucCbbHoSoBA_Change);
                LoaducGridDsDieuTri(1, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucCbbHoSoBA_Change(object sender, EventArgs e)
        {
            LoaducGridDsDieuTri(1, null);
        }

        private void ucGrid_DsDieuTri_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsDieuTri.gridView.OptionsView.ShowGroupPanel = false;
                ucGrid_DsDieuTri.gridView.OptionsView.ShowViewCaption = false;
                ucGrid_DsDieuTri.gridView.OptionsView.ShowAutoFilterRow = true;
                ucGrid_DsDieuTri.SetReLoadWhenFilter(true);
                ucGrid_DsDieuTri.gridView.OptionsView.RowAutoHeight = true;
                ucGrid_DsDieuTri.setEvent(LoaducGridDsDieuTri);
                ucGrid_DsDieuTri.setEvent_FocusedRowChanged(ucGrid_DsDieuTri_ChangeSelected);

                tabLichSuDieuTri.SelectedPageChanged += tabLichSuDieuTri_SelectedPageIndexChanged;

                ucGrid_DsPhieuDT_Load(null, null);
                ucGrid_ChamSoc_Load(null, null);
                ucGrid_DSSuatAn_Load(null, null);
                ucGrid_DsTruyenDich_Load(null, null);
                ucGrid_DsPUThuoc_Load(null, null);
                ucGrid_DsHoiChan_Load(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoaducGridDsDieuTri(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    ResponsList responses = new ResponsList();

                    //string jsonFilter = string.Empty;
                    //if (ucGrid_DsDieuTri.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DsDieuTri.tableFlterColumn.Rows.Count > 0)
                    //    {
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsDieuTri.tableFlterColumn);
                    //    }
                    //}

                    responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K025.LAYDL", page, ucGrid_DsDieuTri.ucPage1.getNumberPerPage(),
                        new String[] { "[0]" }, new string[] { ucCbbHoSoBA.SelectValue }, ucGrid_DsDieuTri.jsonFilter());

                    //ucGrid_DsDieuTri.clearData();
                    ClearData(ucGrid_DsDieuTri);

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(responses.rows);
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "BENHNHANID", "KHAMBENHID", "MAKHAMBENH", "TENKHOA", "TENPHONG", "PHONGID", "KHOAID" });
                    ucGrid_DsDieuTri.setData(dt, responses.total, responses.page, responses.records);
                    ucGrid_DsDieuTri.setColumnAll(false);
                    ucGrid_DsDieuTri.setColumnMemoEdit("MAKHAMBENH", 0, "Mã điều trị", 0);
                    ucGrid_DsDieuTri.setColumnMemoEdit("TENKHOA", 1, "Khoa", 0);
                    ucGrid_DsDieuTri.setColumnMemoEdit("TENPHONG", 2, "Phòng", 0);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DsDieuTri_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DsDieuTri.gridView.FocusedRowHandle >= 0)
                {
                    DataRow dr = (DataRow)ucGrid_DsDieuTri.gridView.GetDataRow(ucGrid_DsDieuTri.gridView.FocusedRowHandle);
                    HIDBENHNHANID = dr["BENHNHANID"].ToString();
                    HIDKHAMBENHID = dr["KHAMBENHID"].ToString();
                    HIDPHONGID = dr["PHONGID"].ToString();

                    // so luong cua cac tab
                    DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NT021.TAB.SOPHIEU", HIDKHAMBENHID + "$" + "" + "$" + "1", 0);

                    DataRow selectedBenhNhan = dt.Rows[0];
                    if (selectedBenhNhan != null)
                    {
                        ucTabDieuTri.Text = selectedBenhNhan["SLDIEUTRI"].ToString() == "0" ? "Điều trị" : "Điều trị(" + selectedBenhNhan["SLDIEUTRI"].ToString() + ")";
                        ucTabXetNghiem.Text = selectedBenhNhan["SLXN"].ToString() == "0" ? "Xét nghiệm" : "Xét nghiệm(" + selectedBenhNhan["SLXN"].ToString() + ")";
                        ucTabCDHA.Text = selectedBenhNhan["SLCDHA"].ToString() == "0" ? "CDHA" : "CDHA(" + selectedBenhNhan["SLCDHA"].ToString() + ")";
                        ucTabPTTT.Text = selectedBenhNhan["SLCHUYENKHOA"].ToString() == "0" ? "PTTT" : "PTTT(" + selectedBenhNhan["SLCHUYENKHOA"].ToString() + ")";
                        ucTabChamSoc.Text = selectedBenhNhan["SLCHAMSOC"].ToString() == "0" ? "Chăm sóc" : "Chăm sóc(" + selectedBenhNhan["SLCHAMSOC"].ToString() + ")";
                        ucTabSuatAn.Text = selectedBenhNhan["SLSUATAN"].ToString() == "0" ? "Suất ăn" : "Suất ăn(" + selectedBenhNhan["SLSUATAN"].ToString() + ")";
                        ucTabTruyenDich.Text = selectedBenhNhan["SLTRUYENDICH"].ToString() == "0" ? "Truyền dịch" : "Truyền dịch(" + selectedBenhNhan["SLTRUYENDICH"].ToString() + ")";
                        ucTabThuPhanUngThuoc.Text = selectedBenhNhan["SLPHANUNGTHUOC"].ToString() == "0" ? "Thử PU thuốc" : "Thử PU thuốc(" + selectedBenhNhan["SLPHANUNGTHUOC"].ToString() + ")";
                        ucTabHoiChan.Text = selectedBenhNhan["SLHOICHAN"].ToString() == "0" ? "Hội chẩn" : "Hội chẩn(" + selectedBenhNhan["SLHOICHAN"].ToString() + ")";
                        ucTabThuoc.Text = selectedBenhNhan["SLTHUOC"].ToString() == "0" ? "Thuốc" : "Thuốc(" + selectedBenhNhan["SLTHUOC"].ToString() + ")";
                        ucTabVatTu.Text = selectedBenhNhan["SLVATTU"].ToString() == "0" ? "Vật tư" : "Vật tư(" + selectedBenhNhan["SLVATTU"].ToString() + ")";
                    }

                    tabLichSuDieuTri_SelectedPageIndexChanged(null, null); // lấy lại dữ liệu tab đang chọn
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ClearDataTabHanhChinh()
        {
            lblKHOA.Text = "";
            lblPHONG.Text = "";
            lblMAHOSOBENHAN.Text = "";
            lblSOVAOVIEN.Text = "";
            lblMAKHAMBENH.Text = "";
            //chua biet ma the la cai gi
            lblMATHE.Text = "";
            lblTENBENHNHAN.Text = "";
            lblNGAYSINH.Text = "";
            lblGIOITINH.Text = "";
            lblTENNGHENGHIEP.Text = "";
            lblTEN_DTBN.Text = "";
            lblMABHYT.Text = "";
            lblTUYEN.Text = "";
            lblTHOIGIAN.Text = "";
            lblNOILAMVIEC.Text = "";
            lblBAOTINCHO.Text = "";
            lblTHOIGIANVAOVIEN.Text = "";
            lblNOIGIOITHIEU.Text = "";
            lblCHANDOANTUYENDUOI.Text = "";
            lblNHANTU.Text = "";
            lblRAKHOA.Text = "";
            lblBENHCHINH.Text = "";
            lblBENHPHU.Text = "";
            lblKETQUA.Text = "";
            lblXUTRI.Text = "";
        }

        private void tabLichSuDieuTri_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
            try
            {
                if (tabLichSuDieuTri.SelectedTabPage == ucTabHanhChinh)
                {
                    setDataTabHanhChinh(HIDKHAMBENHID, HIDPHONGID, HOSOBENHANID);
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabKham)
                {
                    ucTabBenhAn1.loadData(HIDKHAMBENHID);
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabDieuTri)
                {
                    setDataTabDieuTri(HIDKHAMBENHID, BENHNHANID, LNMBP_DieuTri, _flgModeView, "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabXetNghiem)
                {
                    ucTabXetNghiem1.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_XetNghiem, _flgModeView, HOSOBENHANID, "", "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabCDHA)
                {
                    ucTabCDHA1.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_CDHA, _flgModeView, HOSOBENHANID, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabPTTT)
                {
                    ucTabPhauThuatThuThuat1.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_ChuyenKhoa, _flgModeView, HOSOBENHANID, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabChamSoc)
                {
                    setDataTabChamSoc(HIDKHAMBENHID, BENHNHANID, LNMBP_ChamSoc, _flgModeView, "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabSuatAn)
                {
                    setDataTabSuatAn(HIDKHAMBENHID, BENHNHANID, LNMBP_Phieusuatan, "12", _flgModeView, HOSOBENHANID);
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabTruyenDich)
                {
                    setDataTabTruyenDich(HIDKHAMBENHID, BENHNHANID, LNMBP_TruyenDich, _flgModeView, "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabThuPhanUngThuoc)
                {
                    setDataTabPUThuoc(HIDKHAMBENHID, BENHNHANID, LNMBP_TaoPhanUngThuoc, _flgModeView, "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabHoiChan)
                {
                    setDataTabHoiChan(HIDKHAMBENHID, BENHNHANID, LNMBP_HoiChan, _flgModeView, "");
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabThuoc)
                {
                    ucTabThuoc1.loadData(HIDKHAMBENHID, BENHNHANID, LNMBP_Phieuthuoc, _flgModeView, HOSOBENHANID);
                }
                else if (tabLichSuDieuTri.SelectedTabPage == ucTabVatTu)
                {
                    ucTabVatTu1.loadData(HIDKHAMBENHID, BENHNHANID, LNMBP_Phieuvattu, _flgModeView, HOSOBENHANID);
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

        private void setDataTabHanhChinh(string HIDKHAMBENHID, string HIDPHONGID, string HOSOBENHANID)
        {
            try
            {
                var SqlName = string.Empty;

                List<object> SqlParam = new List<object>();
                if (!string.IsNullOrEmpty(HIDKHAMBENHID))
                {
                    SqlName = "NT.005";
                    SqlParam = new List<object>();
                    SqlParam.Add(HIDKHAMBENHID);

                    if (!string.IsNullOrEmpty(HIDPHONGID))
                    {
                        SqlName = "NT.005.NGT";
                        SqlParam.Add(HIDPHONGID);
                    }
                }
                else if (!string.IsNullOrEmpty(HOSOBENHANID))
                {
                    SqlName = "NT.005.HSBA";
                    SqlParam = new List<object>();
                    SqlParam.Add(HOSOBENHANID);

                    if (!string.IsNullOrEmpty(HIDPHONGID))
                    {
                        SqlName = "NT.005.NGT.HSBA";
                        SqlParam.Add(HIDPHONGID);
                    }
                }

                DataTable dtHanhChinh = RequestHTTP.call_ajaxCALL_SP_O("NT.005.NGT", string.Join("$", SqlParam), 0);

                lblKHOA.Text = dtHanhChinh.Rows[0]["KHOA"].ToString();
                lblPHONG.Text = dtHanhChinh.Rows[0]["PHONG"].ToString();
                lblMAHOSOBENHAN.Text = dtHanhChinh.Rows[0]["MAHOSOBENHAN"].ToString();
                lblSOVAOVIEN.Text = dtHanhChinh.Rows[0]["SOVAOVIEN"].ToString();
                lblMAKHAMBENH.Text = dtHanhChinh.Rows[0]["MAKHAMBENH"].ToString();
                //chua biet ma the la cai gi
                //lblMATHE.Text = dtHanhChinh.Rows[0]["MABHYT"].ToString();
                lblTENBENHNHAN.Text = dtHanhChinh.Rows[0]["TENBENHNHAN"].ToString();
                lblNGAYSINH.Text = dtHanhChinh.Rows[0]["NGAYSINH"].ToString();
                lblGIOITINH.Text = dtHanhChinh.Rows[0]["GIOITINH"].ToString();
                lblTENNGHENGHIEP.Text = dtHanhChinh.Rows[0]["TENNGHENGHIEP"].ToString();
                lblTEN_DTBN.Text = dtHanhChinh.Rows[0]["TEN_DTBN"].ToString();
                lblMABHYT.Text = dtHanhChinh.Rows[0]["MABHYT"].ToString();
                lblTUYEN.Text = dtHanhChinh.Rows[0]["TUYEN"].ToString();
                lblTHOIGIAN.Text = dtHanhChinh.Rows[0]["THOIGIAN"].ToString();
                lblNOILAMVIEC.Text = dtHanhChinh.Rows[0]["NOILAMVIEC"].ToString();
                lblBAOTINCHO.Text = dtHanhChinh.Rows[0]["BAOTINCHO"].ToString();
                lblTHOIGIANVAOVIEN.Text = dtHanhChinh.Rows[0]["THOIGIANVAOVIEN"].ToString();
                lblNOIGIOITHIEU.Text = dtHanhChinh.Rows[0]["NOIGIOITHIEU"].ToString();
                lblCHANDOANTUYENDUOI.Text = dtHanhChinh.Rows[0]["CHANDOANTUYENDUOI"].ToString();
                lblNHANTU.Text = dtHanhChinh.Rows[0]["NHANTU"].ToString();
                lblRAKHOA.Text = dtHanhChinh.Rows[0]["RAKHOA"].ToString();
                lblBENHCHINH.Text = dtHanhChinh.Rows[0]["BENHCHINH"].ToString();
                lblBENHPHU.Text = dtHanhChinh.Rows[0]["BENHPHU"].ToString();
                lblKETQUA.Text = dtHanhChinh.Rows[0]["KETQUA"].ToString();
                lblXUTRI.Text = dtHanhChinh.Rows[0]["XUTRI"].ToString();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        #region dieu tri
        private void ucGrid_DsPhieuDT_Load(object sender, EventArgs e)
        {
            ucGrid_DsPhieuDT.gridView.OptionsView.ShowGroupPanel = false;
            ucGrid_DsPhieuDT.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGrid_ChamSoc.gridView.OptionsView.RowAutoHeight = true;
            ucGrid_DsPhieuDT.setEvent_FocusedRowChanged(ucGrid_DsPhieuDT_ChangeSelected);
            ucGrid_DsPhieuDT.gridView.Click += Grid_DsPhieuDT_Click;
            ucGrid_DsPhieuDT.setEvent_MenuPopupClick(MenuPopupClickDsPhieuDT);
            DisableItemTabDieuTri();
        }

        private void Grid_DsPhieuDT_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Any_Click(null, null, ucGrid_DsPhieuDT);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private List<MenuFunc> MenuPopupDsPhieuDTContextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();
            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In tờ điều trị", "dieuTriPrintView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In tất cả", "dieuTriPrintAllView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In phiếu thực hiện y lệnh", "dieuTriPrintYLenhView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In phiếu công khai dịch vụ", "dieuTriPrintDVView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In các phiếu đã chọn", "dieuTriPrintSelectedView", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void MenuPopupClickDsPhieuDT(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DsPhieuDT.gridView.GetFocusedRow());

                if (menu.hlink == "dieuTriPrintView")
                {
                    DieuTriPrintView(drv, 0);
                }
                else if (menu.hlink == "dieuTriPrintAllView")
                {
                    DieuTriPrintAllView(drv, 0);
                }
                else if (menu.hlink == "dieuTriPrintYLenhView")
                {
                    DieuTriPrintYLenhView(drv, 0);
                }
                else if (menu.hlink == "dieuTriPrintDVView")
                {
                    DieuTriPrintDVView(drv, 0);
                }
                else if (menu.hlink == "dieuTriPrintSelectedView")
                {
                    DieuTriPrintSelectedView(drv, 0);
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void DieuTriPrintView(DataRowView drv, int typePrint)
        {
            string ReportName = "NTU020_TODIEUTRI_39BV01_QD4069_A4_ONE";
            string ReportType = "pdf";
            if (drv != null)
            {
                var BENHNHANID = drv["BENHNHANID"].ToString();
                var MAUBENHPHAMID = drv["MAUBENHPHAMID"].ToString();
                var KHOAID = drv["KHOAID"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("i_benhnhanid", "String", BENHNHANID);
                dt.Rows.Add("i_maubenhphamid", "String", MAUBENHPHAMID);
                dt.Rows.Add("i_khoaid", "String", KHOAID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                if (typePrint == 0)
                {
                    PrintPreview(ReportName, dt, ReportType);
                }
            }
        }

        private void DieuTriPrintAllView(DataRowView drv, int typePrint)
        {
            string ReportName = "NTU020_TODIEUTRI_39BV01_QD4069_A4_ALL";
            string ReportType = "pdf";
            if (drv != null)
            {
                var BENHNHANID = drv["BENHNHANID"].ToString();
                var KHOAID = drv["KHOAID"].ToString();
                var KHAMBENHID = drv["KHAMBENHID"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("i_benhnhanid", "String", BENHNHANID);
                dt.Rows.Add("i_khoaid", "String", KHOAID);
                dt.Rows.Add("i_khambenhid", "String", KHAMBENHID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                if (typePrint == 0)
                {
                    PrintPreview(ReportName, dt, ReportType);
                }
            }
        }

        private void DieuTriPrintYLenhView(DataRowView drv, int typePrint)
        {
            string ReportName = "NTU020_TOYLENH_39BV01_QD4069_A4_944";
            string ReportType = "pdf";
            if (drv != null)
            {
                var BENHNHANID = drv["BENHNHANID"].ToString();
                var MAUBENHPHAMID = drv["MAUBENHPHAMID"].ToString();
                var KHOAID = drv["KHOAID"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("i_benhnhanid", "String", BENHNHANID);
                dt.Rows.Add("i_maubenhphamid", "String", MAUBENHPHAMID);
                dt.Rows.Add("i_khoaid", "String", KHOAID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                if (typePrint == 0)
                {
                    PrintPreview(ReportName, dt, ReportType);
                }
            }
        }

        private void DieuTriPrintDVView(DataRowView drv, int typePrint)
        {
            string ReportName = "PHIEU_CONGKHAI_DICHVU_944";
            string ReportType = "pdf";
            if (drv != null)
            {
                var KHAMBENHID = drv["KHAMBENHID"].ToString();
                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("khambenhid", "String", KHAMBENHID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                if (typePrint == 0)
                {
                    PrintPreview(ReportName, dt, ReportType);
                }
            }
        }

        private void DieuTriPrintSelectedView(DataRowView drv, int typePrint)
        {
            string ReportName = "NTU020_TODIEUTRI_39BV01_QD4069_A4_MULTI_944";
            string ReportType = "pdf";
            string KHAMBENHID = "-1";
            string BENHNHANID = "-1";
            string KHOAID = "-1";
            //string MAUBENHPHAMID = "-1";
            string LSTMAUBENHPHAMID = "";
            int[] indexs = ucGrid_DsPhieuDT.gridView.GetSelectedRows();

            if (drv != null)
            {
                for (int i = 0; i < indexs.Length; i++)
                {
                    var DataRow = (DataRowView)ucGrid_DsPhieuDT.gridView.GetRow(indexs[i]);
                    if (DataRow != null)
                    {
                        BENHNHANID = DataRow["BENHNHANID"].ToString();
                        KHAMBENHID = DataRow["KHAMBENHID"].ToString();
                        KHOAID = DataRow["KHOAID"].ToString();
                        LSTMAUBENHPHAMID = string.IsNullOrEmpty(LSTMAUBENHPHAMID) ? DataRow["MAUBENHPHAMID"].ToString() : LSTMAUBENHPHAMID + "," + DataRow["MAUBENHPHAMID"].ToString();
                    }
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("i_benhnhanid", "String", BENHNHANID);
                dt.Rows.Add("i_khoaid", "String", KHOAID);
                dt.Rows.Add("i_khambenhid", "String", KHAMBENHID);
                dt.Rows.Add("i_maubenhphamids", "String", LSTMAUBENHPHAMID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                if (typePrint == 0)
                {
                    PrintPreview(ReportName, dt, ReportType);
                }
            }
        }

        private void DisableItemTabDieuTri()
        {
            txtDIENBIENBENH.Enabled = false;
            txtTOANTHAN.Enabled = false;
            txtKHAMBOPHAN.Enabled = false;
            txtMACH.Enabled = false;
            txtNHIETDO.Enabled = false;
            txtHUYETAP1.Enabled = false;
            txtHUYETAP2.Enabled = false;
            txtNHIPTHO.Enabled = false;
            txtCANNANG.Enabled = false;
            txtKETQUACLS.Enabled = false;
            txtCHUANDOAN.Enabled = false;
            txtBENHKEMTHEO.Enabled = false;
            txtXULY.Enabled = false;
        }

        private void ClearDataTabDieuTri()
        {
            txtDIENBIENBENH.Text = "";
            txtTOANTHAN.Text = "";
            txtKHAMBOPHAN.Text = "";
            txtMACH.Text = "";
            txtNHIETDO.Text = "";
            txtHUYETAP1.Text = "";
            txtHUYETAP2.Text = "";
            txtNHIPTHO.Text = "";
            txtCANNANG.Text = "";
            txtKETQUACLS.Text = "";
            txtCHUANDOAN.Text = "";
            txtBENHKEMTHEO.Text = "";
            txtXULY.Text = "";
        }

        private void ucGrid_DsPhieuDT_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DsPhieuDT.gridView.FocusedRowHandle >= 0)
                {
                    DataRow dr = (DataRow)ucGrid_DsPhieuDT.gridView.GetDataRow(ucGrid_DsPhieuDT.gridView.FocusedRowHandle);

                    if (dr != null)
                    {
                        //clear du lieu truoc khi set
                        ClearDataTabDieuTri();

                        var dt = RequestHTTP.call_ajaxCALL_SP_O("NT.024.2.DETAIL", dr["MAUBENHPHAMID"].ToString() + "$", 0);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "1")
                            {
                                txtTOANTHAN.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "2")
                            {
                                txtKHAMBOPHAN.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "3")
                            {
                                txtMACH.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "4")
                            {
                                txtNHIETDO.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "5")
                            {
                                txtHUYETAP1.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "6")
                            {
                                txtHUYETAP2.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "7")
                            {
                                txtNHIPTHO.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "8")
                            {
                                txtCANNANG.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "9")
                            {
                                txtKETQUACLS.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "10")
                            {
                                txtCHUANDOAN.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "11")
                            {
                                txtCHUANDOAN.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString() + "-" + txtCHUANDOAN.Text;
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "12")
                            {
                                txtXULY.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "15")
                            {
                                txtDIENBIENBENH.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "16")
                            {
                                txtBENHKEMTHEO.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                        }
                    }
                }
                //else
                //{
                //ClearData(ucGrid_DsThuoc);
                //}
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setDataTabDieuTri(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string HOSOBENHANID)
        {
            try
            {
                //if (HIDKHAMBENHID == _KHAMBENHID) return;
                // clear data
                ucGrid_DsPhieuDT.clearData();
                ClearDataTabDieuTri();
                // end clear data

                var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CHECK_ALL_DTNT$");
                if (data_ar == "K74")
                {
                    ucGrid_DsPhieuDT.setMultiSelectMode(true);
                    ucGrid_DsPhieuDT.onIndicator();
                }
                else
                {
                    ucGrid_DsPhieuDT.setMultiSelectMode(false);
                }

                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                        + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + _KHAMBENHID
                        + "\"},{\"name\":\"[1]\",\"value\":\"" + _BENHNHANID
                        + "\"},{\"name\":\"[2]\",\"value\":" + _lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + HOSOBENHANID + "\"}]}";

                string data = "page=" + 1 + "&postData=" + request + "&rows=" + 10000;

                string ret = RequestHTTP.getRequest(data);
                ResponsList ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "KHAMBENHID", "MAUBENHPHAMID", "BENHNHANID", "SOPHIEU", "KHOADIEUTRI", "KHOAID"
                        , "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "NGUOITAO", "NGUOITAO_ID" });
                {
                    ucGrid_DsPhieuDT.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsPhieuDT.setColumnAll(false);

                    ucGrid_DsPhieuDT.setColumn("SOPHIEU", 1, "Số phiếu");
                    ucGrid_DsPhieuDT.setColumn("NGUOITAO", 2, "Người tạo");
                    ucGrid_DsPhieuDT.setColumn("KHOADIEUTRI", 3, "Khoa");
                    ucGrid_DsPhieuDT.setColumn("PHONGDIEUTRI", 4, "Phòng");
                    ucGrid_DsPhieuDT.setColumn("NGAYMAUBENHPHAM", 5, "Thời gian chỉ định");
                }

                ucGrid_DsPhieuDT.addMenuPopup(MenuPopupDsPhieuDTContextMenu());
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        //private void LoaducGridDsPhieuDT(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string jsonFilter = string.Empty;
        //        ResponsList responses = new ResponsList();

        //        if (ucGrid_DsPhieuDT.ReLoadWhenFilter)
        //        {
        //            if (ucGrid_DsPhieuDT.tableFlterColumn.Rows.Count > 0)
        //            {
        //                jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsPhieuDT.tableFlterColumn);
        //            }
        //        }

        //        responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K025.LAYDL", 1, 10000,
        //            new String[] { "[0]" }, new string[] { ucCbbHoSoBA.SelectValue }, jsonFilter);

        //        //ucGrid_DsPhieuDT.clearData();
        //        ClearData(ucGrid_DsPhieuDT);

        //        DataTable dt = new DataTable();
        //        dt = MyJsonConvert.toDataTable(responses.rows);
        //        if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "BENHNHANID", "KHAMBENHID", "MAKHAMBENH", "TENKHOA", "TENPHONG", "PHONGID", "KHOAID" });
        //        ucGrid_DsPhieuDT.setData(dt, responses.total, responses.page, responses.records);
        //        ucGrid_DsPhieuDT.setColumnAll(false);
        //        ucGrid_DsPhieuDT.setColumnMemoEdit("MAKHAMBENH", 0, "Mã điều trị", 0);
        //        ucGrid_DsPhieuDT.setColumnMemoEdit("TENKHOA", 1, "Khoa", 0);
        //        ucGrid_DsPhieuDT.setColumnMemoEdit("TENPHONG", 2, "Phòng", 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }
        //}
        #endregion dieu tri

        #region cham soc
        private void ucGrid_ChamSoc_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_ChamSoc.gridView.OptionsView.ShowGroupPanel = false;
                ucGrid_ChamSoc.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_ChamSoc.gridView.OptionsView.RowAutoHeight = true;
                ucGrid_ChamSoc.gridView.Click += Grid_DsChamSoc_Click;
                ucGrid_ChamSoc.setEvent_FocusedRowChanged(ucGrid_DsChamSoc_ChangeSelected);
                ucGrid_ChamSoc.setEvent_MenuPopupClick(MenuPopupClickDsChamSoc);
                DisableItemTabChamSoc();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Grid_DsChamSoc_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Any_Click(null, null, ucGrid_ChamSoc);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ucGrid_DsChamSoc_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_ChamSoc.gridView.FocusedRowHandle >= 0)
                {
                    DataRow dr = (DataRow)ucGrid_ChamSoc.gridView.GetDataRow(ucGrid_ChamSoc.gridView.FocusedRowHandle);

                    if (dr != null)
                    {
                        //clear du lieu truoc khi set
                        ClearDataTabChamSoc();

                        var dt = RequestHTTP.call_ajaxCALL_SP_O("NT.024.2.DETAIL", dr["MAUBENHPHAMID"].ToString() + "$", 0);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "15")
                            {
                                txt_CS_TDDB.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "16")
                            {
                                txt_CS_YLCS.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "17")
                            {
                                txt_CS_Mach.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "18")
                            {
                                txt_CS_NhietDo.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "19")
                            {
                                txt_CS_HuyetAp_High.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "20")
                            {
                                txt_CS_HuyetAp_Low.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "21")
                            {
                                txt_CS_NhipTho.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                        }
                    }
                }
                //else
                //{
                //ClearData(ucGrid_DsThuoc);
                //}
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        private void DisableItemTabChamSoc()
        {
            txt_CS_TDDB.Enabled = false;
            txt_CS_YLCS.Enabled = false;
            txt_CS_Mach.Enabled = false;
            txt_CS_NhietDo.Enabled = false;
            txt_CS_HuyetAp_High.Enabled = false;
            txt_CS_HuyetAp_Low.Enabled = false;
            txt_CS_NhipTho.Enabled = false;
        }

        private void ClearDataTabChamSoc()
        {
            txt_CS_TDDB.Text = "";
            txt_CS_YLCS.Text = "";
            txt_CS_Mach.Text = "";
            txt_CS_NhietDo.Text = "";
            txt_CS_HuyetAp_High.Text = "";
            txt_CS_HuyetAp_Low.Text = "";
            txt_CS_NhipTho.Text = "";
        }

        private List<MenuFunc> MenuPopupDsChamSocContextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();
            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In phiếu chăm sóc", "chamSocPrintView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In tất cả phiếu", "chamSocPrintAllView", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void MenuPopupClickDsChamSoc(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_ChamSoc.gridView.GetFocusedRow());

                if (menu.hlink == "chamSocPrintView")
                {
                    ChamSocPrintView(drv, 0);
                }
                else if (menu.hlink == "chamSocPrintAllView")
                {
                    ChamSocPrintView(drv, 1);
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void ChamSocPrintView(DataRowView drv, int typePrint)
        {
            string ReportName = "NTU028_PHIEUCHAMSOC_09BV01_QD4069_A4";
            string ReportType = "pdf";
            if (drv != null)
            {
                var MAUBENHPHAMID = "-1";
                var KHAMBENHID = "-1";
                if (typePrint == 0)
                {
                    MAUBENHPHAMID = drv["MAUBENHPHAMID"].ToString();
                }
                else if (typePrint == 1)
                {
                    KHAMBENHID = drv["KHAMBENHID"].ToString();
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("maubenhphamid", "String", MAUBENHPHAMID);
                dt.Rows.Add("khambenhid", "String", KHAMBENHID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                PrintPreview(ReportName, dt, ReportType);
            }
        }

        private void setDataTabChamSoc(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string HOSOBENHANID)
        {
            try
            {
                //if (HIDKHAMBENHID == _KHAMBENHID) return;
                // clear data
                ClearData(ucGrid_ChamSoc);
                ClearDataTabChamSoc();
                // end clear data

                var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CHECK_ALL_DTNT$");
                if (data_ar == "K74")
                {
                    ucGrid_ChamSoc.setMultiSelectMode(true);
                    ucGrid_ChamSoc.onIndicator();
                }
                else
                {
                    ucGrid_ChamSoc.setMultiSelectMode(false);
                }

                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                        + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + _KHAMBENHID
                        + "\"},{\"name\":\"[1]\",\"value\":\"" + _BENHNHANID
                        + "\"},{\"name\":\"[2]\",\"value\":" + _lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + HOSOBENHANID + "\"}]}";

                string data = "page=" + 1 + "&postData=" + request + "&rows=" + 10000;

                string ret = RequestHTTP.getRequest(data);
                ResponsList ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "KHAMBENHID", "MAUBENHPHAMID", "BENHNHANID", "SOPHIEU", "KHOADIEUTRI"
                        , "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "NGUOITAO", "NGUOITAO_ID" });
                {
                    ucGrid_ChamSoc.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_ChamSoc.setColumnAll(false);

                    ucGrid_ChamSoc.setColumn("SOPHIEU", 1, "Số phiếu");
                    ucGrid_ChamSoc.setColumn("NGUOITAO", 2, "Bác sỹ chỉ định");
                    ucGrid_ChamSoc.setColumn("KHOADIEUTRI", 3, "Khoa");
                    ucGrid_ChamSoc.setColumn("PHONGDIEUTRI", 4, "Phòng");
                    ucGrid_ChamSoc.setColumn("NGAYMAUBENHPHAM", 5, "Thời gian chỉ định");
                }

                ucGrid_ChamSoc.addMenuPopup(MenuPopupDsChamSocContextMenu());
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        #endregion cham soc

        #region Suat an
        private void ucGrid_DSSuatAn_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsSuatAn.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsSuatAn.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsSuatAn.gridView.FocusedRowChanged += Grid_DsSuatAn_RowClick;
                ucGrid_DsSuatAn.Set_HidePage(false);

                ucGrid_DsSuatAnCT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsSuatAnCT.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsSuatAnCT.Set_HidePage(false);
                setGrid_DSSuatAnCT();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setGrid_DSSuatAnCT()
        {
            try
            {
                ResponsList ds = new ResponsList();

                // Load dữ liệu
                DataRow dr = (DataRow)ucGrid_DsSuatAn.gridView.GetDataRow(ucGrid_DsSuatAn.gridView.FocusedRowHandle);

                if (dr != null)
                {
                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("NT.024.3", 1, 10000,
                        new String[] { "[0]" }, new string[] { dr["MAUBENHPHAMID"].ToString() }, "");

                    ucGrid_DsSuatAnCT.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "RN", "STT", "THUOCVATTUID", "TEN", "SOLUONG", "TEN_DVT", "DUONGDUNG", "HUONGDANSUDUNG" });
                    ucGrid_DsSuatAnCT.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsSuatAnCT.setColumnAll(false);
                    ucGrid_DsSuatAnCT.setColumn("TENDICHVU", 0, "Tên dịch vụ", 0);
                    ucGrid_DsSuatAnCT.setColumn("MAGIUONG", 1, "Mã giường", 0);
                    ucGrid_DsSuatAnCT.setColumn("TYLENGAYGIUONG", 2, "Tỷ lệ", 0);
                    ucGrid_DsSuatAnCT.setColumn("SOLUONG", 3, "Số lượng", 0);
                    ucGrid_DsSuatAnCT.setColumn("DONGIA", 4, "Đơn giá", 0);
                    ucGrid_DsSuatAnCT.setColumn("TIENBHYTTRA", 5, "BHYT trả", 0);
                    ucGrid_DsSuatAnCT.setColumn("MIENGIAM", 6, "Miễn giảm", 0);
                    ucGrid_DsSuatAnCT.setColumn("NHANDANTRA", 7, "ND trả", 0);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Grid_DsSuatAn_RowClick(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (ucGrid_DsSuatAn.gridView.FocusedRowHandle >= 0)
                {
                    setGrid_DSSuatAnCT();
                }
                else
                {
                    ClearData(ucGrid_DsSuatAnCT);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setDataTabSuatAn(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string LOAIDICHVU, string _modeView, string HOSOBENHANID)
        {
            try
            {
                //if (HIDKHAMBENHID == _KHAMBENHID) return;
                // clear data
                ClearData(ucGrid_DsSuatAn);
                ClearData(ucGrid_DsSuatAnCT);
                // end clear data

                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                        + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + _KHAMBENHID
                        + "\"},{\"name\":\"[1]\",\"value\":\"" + _BENHNHANID
                        + "\"},{\"name\":\"[2]\",\"value\":" + _lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + HOSOBENHANID + "\"}]}";

                string data = "page=" + 1 + "&postData=" + request + "&rows=" + 10000;
                string ret = RequestHTTP.getRequest(data);
                ResponsList ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "KHAMBENHID", "MAUBENHPHAMID", "BENHNHANID", "SOPHIEU"
                        , "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "NGUOITAO", "NGUOITAO_ID", "LOAIDICHVU" });
                {
                    ucGrid_DsSuatAn.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsSuatAn.setColumnAll(false);

                    ucGrid_DsSuatAn.setColumn("SOPHIEU", 0, "Số phiếu");
                    ucGrid_DsSuatAn.setColumn("NGUOITAO", 1, "Người tạo");
                    ucGrid_DsSuatAn.setColumn("PHONGDIEUTRI", 2, "Phòng");
                    ucGrid_DsSuatAn.setColumn("NGAYMAUBENHPHAM", 3, "Ngày tạo");
                    ucGrid_DsSuatAn.setColumn("LOAIDICHVU", 4, "Loại dịch vụ");
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        #endregion Suat an

        #region Truyen dich
        private void ucGrid_DsTruyenDich_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsTruyenDich.gridView.OptionsView.ShowGroupPanel = false;
                ucGrid_DsTruyenDich.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsTruyenDich.gridView.OptionsView.RowAutoHeight = true;
                ucGrid_DsTruyenDich.gridView.Click += Grid_DsTruyenDich_Click;
                ucGrid_DsTruyenDich.setEvent_FocusedRowChanged(ucGrid_DsTruyenDich_ChangeSelected);
                ucGrid_DsTruyenDich.setEvent_MenuPopupClick(MenuPopupClickDsTruyenDich);
                DisableItemTabTruyenDich();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Grid_DsTruyenDich_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Any_Click(null, null, ucGrid_DsTruyenDich);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ucGrid_DsTruyenDich_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DsTruyenDich.gridView.FocusedRowHandle >= 0)
                {
                    DataRow dr = (DataRow)ucGrid_DsTruyenDich.gridView.GetDataRow(ucGrid_DsTruyenDich.gridView.FocusedRowHandle);

                    if (dr != null)
                    {
                        //clear du lieu truoc khi set
                        ClearDataTabTruyenDich();

                        var dt = RequestHTTP.call_ajaxCALL_SP_O("NT.024.2.DETAIL", dr["MAUBENHPHAMID"].ToString() + "$", 0);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "29")
                            {
                                txt_TD_DichTruyenHamLuong.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "30")
                            {
                                txt_TD_SoLuong.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "31")
                            {
                                txt_TD_LoSanXuat.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "32")
                            {
                                txt_TD_TocDo.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "33")
                            {
                                txt_TD_ThoiGianBatDau.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "34")
                            {
                                txt_TD_ThoiGianKetThuc.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "35")
                            {
                                txt_TD_BacSiChiDinh.Text = GetOfficer(dt.Rows[i]["GIATRI_KETQUA"].ToString());
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "36")
                            {
                                txt_TD_YTaDieuDuong.Text = GetOfficer(dt.Rows[i]["GIATRI_KETQUA"].ToString());
                            }
                        }
                    }
                }
                //else
                //{
                //ClearData(ucGrid_DsThuoc);
                //}
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void DisableItemTabTruyenDich()
        {
            txt_TD_DichTruyenHamLuong.Enabled = false;
            txt_TD_SoLuong.Enabled = false;
            txt_TD_LoSanXuat.Enabled = false;
            txt_TD_TocDo.Enabled = false;
            txt_TD_ThoiGianBatDau.Enabled = false;
            txt_TD_ThoiGianKetThuc.Enabled = false;
            txt_TD_BacSiChiDinh.Enabled = false;
            txt_TD_YTaDieuDuong.Enabled = false;
        }

        private void ClearDataTabTruyenDich()
        {
            txt_TD_DichTruyenHamLuong.Text = "";
            txt_TD_SoLuong.Text = "";
            txt_TD_LoSanXuat.Text = "";
            txt_TD_TocDo.Text = "";
            txt_TD_ThoiGianBatDau.Text = "";
            txt_TD_ThoiGianKetThuc.Text = "";
            txt_TD_BacSiChiDinh.Text = "";
            txt_TD_YTaDieuDuong.Text = "";
        }

        private List<MenuFunc> MenuPopupDsTruyenDichContextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();
            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In phiếu truyền dịch", "truyenDichPrintView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In tất cả", "truyenDichPrintAllView", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void MenuPopupClickDsTruyenDich(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DsTruyenDich.gridView.GetFocusedRow());

                if (menu.hlink == "truyenDichPrintView")
                {
                    TruyenDichPrintView(drv, 0);
                }
                else if (menu.hlink == "truyenDichPrintAllView")
                {
                    TruyenDichPrintView(drv, 1);
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void TruyenDichPrintView(DataRowView drv, int typePrint)
        {
            string ReportName = "NTU033_PHIEUTHEODOITRUYENDICH_17BV01_QD4069_A4";
            string ReportType = "pdf";
            if (drv != null)
            {
                var MAUBENHPHAMID = "-1";
                var KHAMBENHID = "-1";
                if (typePrint == 0)
                {
                    MAUBENHPHAMID = drv["MAUBENHPHAMID"].ToString();
                }
                else if (typePrint == 1)
                {
                    KHAMBENHID = drv["KHAMBENHID"].ToString();
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("maubenhphamid", "String", MAUBENHPHAMID);
                dt.Rows.Add("khambenhid", "String", KHAMBENHID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                PrintPreview(ReportName, dt, ReportType);
            }
        }

        private void setDataTabTruyenDich(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string HOSOBENHANID)
        {
            try
            {
                //if (HIDKHAMBENHID == _KHAMBENHID) return;
                // clear data
                ClearData(ucGrid_DsTruyenDich);
                ClearDataTabTruyenDich();
                // end clear data

                var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CHECK_ALL_DTNT$");
                if (data_ar == "K74")
                {
                    ucGrid_DsTruyenDich.setMultiSelectMode(true);
                    ucGrid_DsTruyenDich.onIndicator();
                }
                else
                {
                    ucGrid_DsTruyenDich.setMultiSelectMode(false);
                }

                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                        + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + _KHAMBENHID
                        + "\"},{\"name\":\"[1]\",\"value\":\"" + _BENHNHANID
                        + "\"},{\"name\":\"[2]\",\"value\":" + _lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + HOSOBENHANID + "\"}]}";

                string data = "page=" + 1 + "&postData=" + request + "&rows=" + 10000;
                string ret = RequestHTTP.getRequest(data);
                ResponsList ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "SOPHIEU", "KHAMBENHID", "MAUBENHPHAMID", "BENHNHANID", "KHOADIEUTRI"
                        , "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "NGUOITAO", "NGUOITAO_ID" });
                {
                    ucGrid_DsTruyenDich.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsTruyenDich.setColumnAll(false);

                    ucGrid_DsTruyenDich.setColumn("SOPHIEU", 1, "Số phiếu");
                    ucGrid_DsTruyenDich.setColumn("NGUOITAO", 2, "Người tạo phiếu");
                    ucGrid_DsTruyenDich.setColumn("KHOADIEUTRI", 3, "Khoa");
                    ucGrid_DsTruyenDich.setColumn("PHONGDIEUTRI", 4, "Phòng");
                    ucGrid_DsTruyenDich.setColumn("NGAYMAUBENHPHAM", 5, "Thời gian tạo");
                }

                ucGrid_DsTruyenDich.addMenuPopup(MenuPopupDsTruyenDichContextMenu());
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        #endregion Truyen dich

        #region thu phan ung thuoc
        private void ucGrid_DsPUThuoc_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsPUThuoc.gridView.OptionsView.ShowGroupPanel = false;
                ucGrid_DsPUThuoc.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsPUThuoc.gridView.OptionsView.RowAutoHeight = true;
                ucGrid_DsPUThuoc.gridView.Click += Grid_DsPUThuoc_Click;
                ucGrid_DsPUThuoc.setEvent_FocusedRowChanged(ucGrid_DsPUThuoc_ChangeSelected);
                ucGrid_DsPUThuoc.setEvent_MenuPopupClick(MenuPopupClickDsPUThuoc);
                DisableItemTabPUThuoc();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Grid_DsPUThuoc_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Any_Click(null, null, ucGrid_DsPUThuoc);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ucGrid_DsPUThuoc_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DsPUThuoc.gridView.FocusedRowHandle >= 0)
                {
                    DataRow dr = (DataRow)ucGrid_DsPUThuoc.gridView.GetDataRow(ucGrid_DsPUThuoc.gridView.FocusedRowHandle);

                    if (dr != null)
                    {
                        //clear du lieu truoc khi set
                        ClearDataTabPUThuoc();

                        var dt = RequestHTTP.call_ajaxCALL_SP_O("NT.024.2.DETAIL", dr["MAUBENHPHAMID"].ToString() + "$", 0);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "37")
                            {
                                txt_PU_TenThuoc.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "38")
                            {
                                txt_PU_PhuongPhap.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "39")
                            {
                                txt_PU_ThoiGianBatDau.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "40")
                            {
                                txt_PU_ThoiGianDocKetQua.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "41")
                            {
                                txt_PU_BSChiDinh.Text = GetOfficer(dt.Rows[i]["GIATRI_KETQUA"].ToString());
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "42")
                            {
                                txt_PU_NguoiThu.Text = GetOfficer(dt.Rows[i]["GIATRI_KETQUA"].ToString());
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "43")
                            {
                                txt_PU_BSDocKetQua.Text = GetOfficer(dt.Rows[i]["GIATRI_KETQUA"].ToString());
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "44")
                            {
                                txt_PU_KetQua.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                        }
                    }
                }
                //else
                //{
                //ClearData(ucGrid_DsThuoc);
                //}
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void DisableItemTabPUThuoc()
        {
            txt_PU_TenThuoc.Enabled = false;
            txt_PU_PhuongPhap.Enabled = false;
            txt_PU_ThoiGianBatDau.Enabled = false;
            txt_PU_ThoiGianDocKetQua.Enabled = false;
            txt_PU_BSChiDinh.Enabled = false;
            txt_PU_NguoiThu.Enabled = false;
            txt_PU_BSDocKetQua.Enabled = false;
            txt_PU_KetQua.Enabled = false;
        }

        private void ClearDataTabPUThuoc()
        {
            txt_PU_TenThuoc.Text = "";
            txt_PU_PhuongPhap.Text = "";
            txt_PU_ThoiGianBatDau.Text = "";
            txt_PU_ThoiGianDocKetQua.Text = "";
            txt_PU_BSChiDinh.Text = "";
            txt_PU_NguoiThu.Text = "";
            txt_PU_BSDocKetQua.Text = "";
            txt_PU_KetQua.Text = "";
        }

        private List<MenuFunc> MenuPopupDsPUThuocContextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();
            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In giấy thử phản ứng thuốc", "puThuocPrintView", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void MenuPopupClickDsPUThuoc(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DsPUThuoc.gridView.GetFocusedRow());

                if (menu.hlink == "puThuocPrintView")
                {
                    PUThuocPrintView(drv);
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void PUThuocPrintView(DataRowView drv)
        {
            string ReportName = "NTU053_GIAYTHUPHANUNGTHUOC_06BV01_QD4069_A4";
            string ReportType = "pdf";
            if (drv != null)
            {
                var MAUBENHPHAMID = drv["MAUBENHPHAMID"].ToString();
                var KHAMBENHID = drv["KHAMBENHID"].ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("maubenhphamid", "String", MAUBENHPHAMID);
                dt.Rows.Add("khambenhid", "String", KHAMBENHID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                PrintPreview(ReportName, dt, ReportType);
            }
        }

        private void setDataTabPUThuoc(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string HOSOBENHANID)
        {
            try
            {
                //if (HIDKHAMBENHID == _KHAMBENHID) return;
                // clear data
                ClearData(ucGrid_DsPUThuoc);
                ClearDataTabPUThuoc();
                // end clear data

                var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CHECK_ALL_DTNT$");
                if (data_ar == "K74")
                {
                    ucGrid_DsPUThuoc.setMultiSelectMode(true);
                    ucGrid_DsPUThuoc.onIndicator();
                }
                else
                {
                    ucGrid_DsPUThuoc.setMultiSelectMode(false);
                }

                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                        + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + _KHAMBENHID
                        + "\"},{\"name\":\"[1]\",\"value\":\"" + _BENHNHANID
                        + "\"},{\"name\":\"[2]\",\"value\":" + _lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + HOSOBENHANID + "\"}]}";

                string data = "page=" + 1 + "&postData=" + request + "&rows=" + 10000;
                string ret = RequestHTTP.getRequest(data);
                ResponsList ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "SOPHIEU", "KHAMBENHID", "MAUBENHPHAMID", "BENHNHANID", "KHOADIEUTRI"
                        , "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "NGUOITAO", "NGUOITAO_ID" });
                {
                    ucGrid_DsPUThuoc.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsPUThuoc.setColumnAll(false);

                    ucGrid_DsPUThuoc.setColumn("SOPHIEU", 1, "Số phiếu");
                    ucGrid_DsPUThuoc.setColumn("NGUOITAO", 2, "Người tạo phiếu");
                    ucGrid_DsPUThuoc.setColumn("KHOADIEUTRI", 3, "Khoa");
                    ucGrid_DsPUThuoc.setColumn("PHONGDIEUTRI", 4, "Phòng");
                    ucGrid_DsPUThuoc.setColumn("NGAYMAUBENHPHAM", 5, "Thời gian tạo");
                }

                // gridComplete
                //if(_self.options._modeView == "1") // form nay duy nhat = 1
                // gắn menu popup cho grid
                //if (_flgModeView == "1")
                //{
                ucGrid_DsPUThuoc.addMenuPopup(MenuPopupDsPUThuocContextMenu());
                //}
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        #endregion thu phan ung thuoc

        #region hoi chan
        private void ucGrid_DsHoiChan_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsHoiChan.gridView.OptionsView.ShowGroupPanel = false;
                ucGrid_DsHoiChan.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsHoiChan.gridView.OptionsView.RowAutoHeight = true;
                ucGrid_DsHoiChan.gridView.Click += Grid_DsHoiChan_Click;
                ucGrid_DsHoiChan.setEvent_FocusedRowChanged(ucGrid_DsHoiChan_ChangeSelected);
                ucGrid_DsHoiChan.setEvent_MenuPopupClick(MenuPopupClickDsHoiChan);
                DisableItemTabHoiChan();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Grid_DsHoiChan_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Any_Click(null, null, ucGrid_DsHoiChan);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ucGrid_DsHoiChan_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DsHoiChan.gridView.FocusedRowHandle >= 0)
                {
                    DataRow dr = (DataRow)ucGrid_DsHoiChan.gridView.GetDataRow(ucGrid_DsHoiChan.gridView.FocusedRowHandle);

                    if (dr != null)
                    {
                        //clear du lieu truoc khi set
                        ClearDataTabHoiChan();

                        var dt = RequestHTTP.call_ajaxCALL_SP_O("NT.024.2.DETAIL", dr["MAUBENHPHAMID"].ToString() + "$", 0);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "21")
                            {
                                txt_HC_TrichBienBan.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "22")
                            {
                                txt_HC_ChuToa.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "23")
                            {
                                txt_HC_ThuKy.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "24")
                            {
                                txt_HC_ThamGia.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "25")
                            {
                                txt_HC_TomTat.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "26")
                            {
                                txt_HC_KetLuan.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                            else if (dt.Rows[i]["DICHVUTHUCHIENID"].ToString() == "27")
                            {
                                txt_HC_HuongDieuTri.Text = dt.Rows[i]["GIATRI_KETQUA"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void DisableItemTabHoiChan()
        {
            txt_HC_TrichBienBan.Enabled = false;
            txt_HC_ChuToa.Enabled = false;
            txt_HC_ThuKy.Enabled = false;
            txt_HC_ThamGia.Enabled = false;
            txt_HC_TomTat.Enabled = false;
            txt_HC_KetLuan.Enabled = false;
            txt_HC_HuongDieuTri.Enabled = false;
        }

        private void ClearDataTabHoiChan()
        {
            txt_HC_TrichBienBan.Text = "";
            txt_HC_ChuToa.Text = "";
            txt_HC_ThuKy.Text = "";
            txt_HC_ThamGia.Text = "";
            txt_HC_TomTat.Text = "";
            txt_HC_KetLuan.Text = "";
            txt_HC_HuongDieuTri.Text = "";
        }

        private List<MenuFunc> MenuPopupDsHoiChanContextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();
            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In biên bản hội chẩn", "hoiChanPrintView", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void MenuPopupClickDsHoiChan(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DsHoiChan.gridView.GetFocusedRow());

                if (menu.hlink == "hoiChanPrintView")
                {
                    HoiChanPrintView(drv);
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void HoiChanPrintView(DataRowView drv)
        {
            string ReportName = "NTU045_TRICHBIENBANHOICHAN_40BV01_QD4069_A4";
            string ReportType = "pdf";
            if (drv != null)
            {
                var MAUBENHPHAMID = drv["MAUBENHPHAMID"].ToString();

                DataTable dt = new DataTable();
                dt.Columns.Add("name");
                dt.Columns.Add("type");
                dt.Columns.Add("value");
                dt.Rows.Add("maubenhphamid", "String", MAUBENHPHAMID);

                Func.PrintFile_FromData(ReportName, dt, ReportType);
                PrintPreview(ReportName, dt, ReportType);
            }
        }

        private void setDataTabHoiChan(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string HOSOBENHANID)
        {
            try
            {
                //if (HIDKHAMBENHID == _KHAMBENHID) return;
                // clear data
                ClearData(ucGrid_DsHoiChan);
                ClearDataTabHoiChan();
                // end clear data

                var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CHECK_ALL_DTNT$");
                if (data_ar == "K74")
                {
                    ucGrid_DsHoiChan.setMultiSelectMode(true);
                    ucGrid_DsHoiChan.onIndicator();
                }
                else
                {
                    ucGrid_DsHoiChan.setMultiSelectMode(false);
                }

                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                        + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + _KHAMBENHID
                        + "\"},{\"name\":\"[1]\",\"value\":\"" + _BENHNHANID
                        + "\"},{\"name\":\"[2]\",\"value\":" + _lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + HOSOBENHANID + "\"}]}";

                string data = "page=" + 1 + "&postData=" + request + "&rows=" + 10000;
                string ret = RequestHTTP.getRequest(data);
                ResponsList ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "SOPHIEU", "KHAMBENHID", "MAUBENHPHAMID", "BENHNHANID", "KHOADIEUTRI"
                        , "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "NGUOITAO", "NGUOITAO_ID" });
                {
                    ucGrid_DsHoiChan.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsHoiChan.setColumnAll(false);

                    ucGrid_DsHoiChan.setColumn("SOPHIEU", 1, "Số phiếu");
                    ucGrid_DsHoiChan.setColumn("NGUOITAO", 2, "Người tạo");
                    ucGrid_DsHoiChan.setColumn("KHOADIEUTRI", 3, "Khoa");
                    ucGrid_DsHoiChan.setColumn("PHONGDIEUTRI", 4, "Phòng");
                    ucGrid_DsHoiChan.setColumn("NGAYMAUBENHPHAM", 5, "Thời gian chỉ định");
                }

                // gridComplete
                //if(_self.options._modeView == "1") // form nay duy nhat = 1
                // gắn menu popup cho grid
                //if (_flgModeView == "1")
                //{
                ucGrid_DsHoiChan.addMenuPopup(MenuPopupDsHoiChanContextMenu());
                //}
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        #endregion hoi chan

        private string GetOfficer(string OfficerId)
        {
            DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NT.024.OFFIER", OfficerId);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["OFFICER_NAME"].ToString();
            }
            return "";
        }

        private void ClearData(UserControl.ucGridview grid)
        {
            try
            {
                grid.gridControl.DataSource = null;
                grid.gridView.Columns.Clear();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void PrintPreview(string code, DataTable parTable, string title, int height = 650, int width = 900)
        {
            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(title, code, parTable, height, width);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void Grid_Any_Click(object sender, EventArgs e, UserControl.ucGridview grid)
        {
            try
            {
                int index = grid.gridView.FocusedRowHandle;

                if (!"DX$CheckboxSelectorColumn".Equals(grid.gridView.FocusedColumn.FieldName))
                {
                    if (grid.gridView.GetSelectedRows().Any(o => o == index))
                    {
                        grid.gridView.UnselectRow(index);
                    }
                    else
                    {
                        grid.gridView.SelectRow(index);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }
    }
}
