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

namespace VNPT.HIS.BaoCao
{
    public partial class NTU02D059_TraCuuBenhNhan : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private string HIDBENHNHANID = string.Empty;
        private string HIDHOSOBENHANID = string.Empty;
        private string HIDTIEPNHANID = string.Empty;
        private string HIDMAUBENHPHAMID = string.Empty;
        string LNMBP_XetNghiem = "1";
        string LNMBP_CDHA = "2";
        string LNMBP_DieuTri = "4";
        string LNMBP_ChuyenKhoa = "5";
        string LNMBP_Phieuthuoc = "7";
        string LNMBP_Phieuvattu = "8";
        string LNMBP_ChamSoc = "9";
        string LNMBP_TruyenDich = "13";
        string LNMBP_TaoPhanUngThuoc = "14";
        string LNMBP_HoiChan = "15";
        string LNMBP_PhieuVanChuyen = "16";
        string LNMBP_Phieusuatan = "11";
        string _flgModeView = "1";
        string _hosobenhanid = "";

        public NTU02D059_TraCuuBenhNhan()
        {
            InitializeComponent();
        }

        private void NTU02D059_TraCuuBenhNhan_Load(object sender, EventArgs e)
        {
            // Init combobox
            InitControl();
        }

        private void InitControl()
        {
            // Load chuẩn đoán và chuẩn đoán kèm theo
            DataTable dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
            ucSearchLookupICD.setData(dt, "ICD10CODE", "ICD10NAME");
            ucSearchLookupICD.setColumn("RN", -1, "", 0);
            ucSearchLookupICD.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucSearchLookupICD.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
        }

        private void ucGrid_DSBenhNhan_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DSBenhNhan.gridView.OptionsView.ShowGroupPanel = false;
                ucGrid_DSBenhNhan.gridView.OptionsView.ShowViewCaption = false;
                ucGrid_DSBenhNhan.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DSBenhNhan.gridView.OptionsView.RowAutoHeight = true;
                ucGrid_DSBenhNhan.setEvent(LoadUcGrid_DSBenhNhan);
                ucGrid_DSBenhNhan.setEvent_FocusedRowChanged(ucGrid_DSBenhNhan_ChangeSelected);
                tabLichSuDieuTri.SelectedPageIndexChanged += tabLichSuDieuTri_SelectedPageIndexChanged;

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadUcGrid_DSBenhNhan(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    string jsonFilter = string.Empty;
                    ResponsList responses = new ResponsList();

                    if (txtMaBN.Text == null
                        && txtMaBA.Text == null
                        && txtMaBHYT.Text == null
                        && txtVVTuNgay.DateTime == null
                        && txtVVDenNgay.DateTime == null
                        && txtHoTen.Text == null
                        && ucSearchLookupICD.textEdit1 == null
                        && ucSearchLookupICD.searchLookUpEdit1 == null
                        && txtRVTuNgay.DateTime == null
                        && txtRVDenNgay.DateTime == null)
                    {
                        DialogResult DialogResult = MessageBox.Show("Vui lòng nhập thông tin tìm kiếm cụ thể", "", MessageBoxButtons.YesNo);
                        if (DialogResult == DialogResult.Yes)
                        {
                            txtMaBN.Focus();
                        }
                    }

                    var lookup_sql = "NTUD059.DS_TCBN";
                    responses = RequestHTTP.get_ajaxExecuteQueryPaging(lookup_sql, 1, ucGrid_DSBenhNhan.ucPage1.getNumberPerPage(),
                    new String[]
                    {
                        "[0]",
                        "[1]",
                        "[2]",
                        "[3]",
                        "[4]",
                        "[5]",
                        "[6]",
                        "[7]",
                        "[8]",
                        "[9]"
                    }, new string[] {
                        txtVVTuNgay.DateTime == DateTime.MinValue ? "-1" : txtVVTuNgay.DateTime.ToString("dd/MM/yyyy"),
                        txtVVDenNgay.DateTime == DateTime.MinValue ? "-1" : txtVVDenNgay.DateTime.ToString("dd/MM/yyyy"),
                        txtRVTuNgay.DateTime == DateTime.MinValue ? "-1" : txtRVTuNgay.DateTime.ToString("dd/MM/yyyy"),
                        txtRVDenNgay.DateTime == DateTime.MinValue ? "-1" : txtRVDenNgay.DateTime.ToString("dd/MM/yyyy"),
                        txtMaBN.Text,
                        txtHoTen.Text,
                        ucSearchLookupICD.SelectedValue,
                        ucSearchLookupICD.SelectedText,
                        txtMaBA.Text,
                        txtMaBHYT.Text
                    }, jsonFilter);

                    //responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K025.LAYDL", page, ucGrid_DSBenhNhan.ucPage1.getNumberPerPage(),
                    //    new String[] { "[0]" }, new string[] { ucCbbHoSoBA.SelectValue }, jsonFilter);

                    ucGrid_DSBenhNhan.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(responses.rows);
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                     {
                        "RN", "TIEPNHANID", "BENHNHANID", "HOSOBENHANID", "MAHOSOBENHAN", "MABENHNHAN", "TENBENHNHAN", "GIOITINH",
                        "NGAYSINH", "MA_BHYT", "DIACHI", "NGAYVAOVIEN", "NGAYHOSORAVIEN", "CHANDOANRAVIEN"
                     });
                    ucGrid_DSBenhNhan.setData(dt, responses.total, responses.page, responses.records);
                    ucGrid_DSBenhNhan.setColumnAll(false);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("MAHOSOBENHAN", 0, "Mã BA", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("MABENHNHAN", 1, "Mã BN", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("TENBENHNHAN", 2, "Tên BN", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("GIOITINH", 3, "Giới tính", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("NGAYSINH", 4, "Ngày sinh", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("MA_BHYT", 5, "Mã BHYT", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("DIACHI", 6, "Địa chỉ", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("NGAYVAOVIEN", 7, "Ngày vào viện", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("NGAYHOSORAVIEN", 8, "Ngày ra viện", 0);
                    ucGrid_DSBenhNhan.setColumnMemoEdit("CHANDOANRAVIEN", 9, "Chẩn đoán", 0);

                    //setDataTabHanhChinh(HIDHOSOBENHANID);
                    //tabLichSuDieuTri.SelectedPageIndexChanged += tabLichSuDieuTri_SelectedPageIndexChanged;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DSBenhNhan_ChangeSelected(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DSBenhNhan.gridView.FocusedRowHandle >= 0)
                {
                    // xu ly gia tri cac tab
                    DataRow dr = (DataRow)ucGrid_DSBenhNhan.gridView.GetDataRow(ucGrid_DSBenhNhan.gridView.FocusedRowHandle);
                    HIDBENHNHANID = dr["BENHNHANID"].ToString();
                    HIDHOSOBENHANID = dr["HOSOBENHANID"].ToString();
                    _hosobenhanid = HIDHOSOBENHANID;
                    HIDTIEPNHANID = dr["TIEPNHANID"].ToString();

                    // so luong cua cac tab
                    DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NT021.TAB.SOPHIEU", "$" + HIDHOSOBENHANID + "$" + "2", 0);

                    DataRow selectedBenhNhan = dt.Rows[0];
                    //DataRowView selectedBenhNhan = (DataRowView)sender;
                    if (selectedBenhNhan != null)
                    {
                        //ucTabKham.PageText = selectedBenhNhan["SLXN"].ToString() == "0" ? "Xét nghiệm" : "Xét nghiệm(" + selectedBenhNhan["SLXN"].ToString() + ")";
                        ucTabDieuTri.PageText = selectedBenhNhan["SLDIEUTRI"].ToString() == "0" ? "Điều trị" : "Điều trị(" + selectedBenhNhan["SLDIEUTRI"].ToString() + ")";
                        ucTabXetNghiem.PageText = selectedBenhNhan["SLXN"].ToString() == "0" ? "Xét nghiệm" : "Xét nghiệm(" + selectedBenhNhan["SLXN"].ToString() + ")";
                        ucTabCDHA.PageText = selectedBenhNhan["SLCDHA"].ToString() == "0" ? "CDHA" : "CDHA(" + selectedBenhNhan["SLCDHA"].ToString() + ")";
                        ucTabPTTT.PageText = selectedBenhNhan["SLCHUYENKHOA"].ToString() == "0" ? "PTTT" : "PTTT(" + selectedBenhNhan["SLCHUYENKHOA"].ToString() + ")";
                        ucTabChamSoc.PageText = selectedBenhNhan["SLCHAMSOC"].ToString() == "0" ? "Chăm sóc" : "Chăm sóc(" + selectedBenhNhan["SLCHAMSOC"].ToString() + ")";
                        ucTabSuatAn.PageText = selectedBenhNhan["SLSUATAN"].ToString() == "0" ? "Suất ăn" : "Suất ăn(" + selectedBenhNhan["SLSUATAN"].ToString() + ")";
                        ucTabTruyenDich.PageText = selectedBenhNhan["SLTRUYENDICH"].ToString() == "0" ? "Truyền dịch" : "Truyền dịch(" + selectedBenhNhan["SLTRUYENDICH"].ToString() + ")";
                        ucTabThuPhanUngThuoc.PageText = selectedBenhNhan["SLPHANUNGTHUOC"].ToString() == "0" ? "Thử PU thuốc" : "Thử PU thuốc(" + selectedBenhNhan["SLPHANUNGTHUOC"].ToString() + ")";
                        ucTabHoiChan.PageText = selectedBenhNhan["SLHOICHAN"].ToString() == "0" ? "Hội chẩn" : "Hội chẩn(" + selectedBenhNhan["SLHOICHAN"].ToString() + ")";
                        ucTabThuoc.PageText = selectedBenhNhan["SLTHUOC"].ToString() == "0" ? "Thuốc" : "Thuốc(" + selectedBenhNhan["SLTHUOC"].ToString() + ")";
                        ucTabVatTu.PageText = selectedBenhNhan["SLVATTU"].ToString() == "0" ? "Vật tư" : "Vật tư(" + selectedBenhNhan["SLVATTU"].ToString() + ")";
                    }

                    //clearDataTabHanhChinh();
                    //setDataTabHanhChinh(HIDHOSOBENHANID);
                    //tabLichSuDieuTri.SelectedPage = ucTabHanhChinh;
                    //tabLichSuDieuTri.SelectedPageIndexChanged += tabLichSuDieuTri_SelectedPageIndexChanged;
                    // ket thuc xu ly gia tri cac tab

                    LoadUcGrid_DSBenhNhanCT(1, null);
                }
                else
                {
                    ClearData(ucGrid_DSBenhNhanCT);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DSBenhNhanCT_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DSBenhNhanCT.gridView.OptionsView.ShowGroupPanel = false;
                ucGrid_DSBenhNhanCT.gridView.OptionsView.ShowViewCaption = true;
                ucGrid_DSBenhNhanCT.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DSBenhNhanCT.gridView.OptionsView.RowAutoHeight = true;
                //ucGrid_DSBenhNhan.setEvent(LoadUcGrid_DSBenhNhanCT);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadUcGrid_DSBenhNhanCT(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    string jsonFilter = string.Empty;
                    ResponsList responses = new ResponsList();

                    responses = RequestHTTP.get_ajaxExecuteQueryPaging("NTU02D059.EV001", page, ucGrid_DSBenhNhan.ucPage1.getNumberPerPage(),
                        new String[] { "[0]" }, new string[] { HIDHOSOBENHANID }, jsonFilter);

                    ucGrid_DSBenhNhanCT.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(responses.rows);
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                     {
                        "RN", "BENHNHANID", "HOSOBENHANID", "MATIEPNHAN", "KHOA", "PHONG",
                        "THOIGIANBD", "THOIGIANKT", "TRANGTHAI", "SOTHUTU"
                     });
                    ucGrid_DSBenhNhanCT.setData(dt, responses.total, responses.page, responses.records);
                    ucGrid_DSBenhNhanCT.setColumnAll(false);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("RN", 0, " ", 0);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("MATIEPNHAN", 1, "Mã TN", 0);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("KHOA", 2, "Khoa", 0);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("PHONG", 3, "Phòng", 0);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("THOIGIANBD", 4, "Thời gian vào", 0);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("THOIGIANKT", 5, "Thời gian ra", 0);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("TRANGTHAI", 6, "Trạng thái", 0);
                    ucGrid_DSBenhNhanCT.setColumnMemoEdit("SOTHUTU", 7, "Số thứ tự", 0);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadUcGrid_DSBenhNhan(1, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

        }

        private void tabLichSuDieuTri_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            //DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
            try
            {
                if (tabLichSuDieuTri.SelectedPage == ucTabHanhChinh)
                {
                    setDataTabHanhChinh(HIDHOSOBENHANID);
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabKham)
                {
                    ucTabBenhAn1.loadData("");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabDieuTri)
                {
                    //setDataTabDieuTri(HIDKHAMBENHID, BENHNHANID, LNMBP_DieuTri, "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabXetNghiem)
                {
                    ucTabXetNghiem1.loadData_2("", HIDBENHNHANID, LNMBP_XetNghiem, _flgModeView, _hosobenhanid, "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabCDHA)
                {
                    ucTabCDHA1.loadData_2("", HIDBENHNHANID, LNMBP_CDHA, _flgModeView, _hosobenhanid, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabPTTT)
                {
                    ucTabPhauThuatThuThuat1.loadData_2("", HIDBENHNHANID, LNMBP_ChuyenKhoa, _flgModeView, _hosobenhanid, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabChamSoc)
                {
                    //ucTabChamSoc.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_ChamSoc, _flgModeView, _hosobenhanid, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabSuatAn)
                {
                    //ucTabSuatAn.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_Phieusuatan, _flgModeView, _hosobenhanid, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabTruyenDich)
                {
                    //ucTabTruyenDich.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_TruyenDich, _flgModeView, _hosobenhanid, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabThuPhanUngThuoc)
                {
                    //ucTabThuPhanUngThuoc.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_TaoPhanUngThuoc, _flgModeView, _hosobenhanid, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabHoiChan)
                {
                    //ucTabHoiChan.loadData_2(HIDKHAMBENHID, BENHNHANID, LNMBP_HoiChan, _flgModeView, _hosobenhanid, "", "", "");
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabThuoc)
                {
                    ucTabThuoc1.loadData("", HIDBENHNHANID, LNMBP_Phieuthuoc, _flgModeView, _hosobenhanid);
                }
                else if (tabLichSuDieuTri.SelectedPage == ucTabVatTu)
                {
                    ucTabVatTu1.loadData("", HIDBENHNHANID, LNMBP_Phieuvattu, _flgModeView, _hosobenhanid);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                //DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void setDataTabHanhChinh(string HIDHOSOBENHANID)
        {
            try
            {
                DataTable dtHanhChinh = RequestHTTP.call_ajaxCALL_SP_O("NT.005.HSBA", HIDHOSOBENHANID, 0);

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

        private void ucGrid_DsPhieuDT_Load(object sender, EventArgs e)
        {
            ucGrid_DsPhieuDT.gridView.OptionsView.ShowGroupPanel = false;
            ucGrid_DsPhieuDT.gridView.OptionsView.ShowViewCaption = false;
            ucGrid_DsPhieuDT.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGrid_DsPhieuDT.setEvent(LoaducGridDsPhieuDT);
            //ucGrid_DsPhieuDT.setEvent_FocusedRowChanged(ucGrid_DsPhieuDT_ChangeSelected);
        }

        private void setDataTabDieuTri(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string _hosobenhanid)
        {

            try
            {
                if (HIDHOSOBENHANID == _KHAMBENHID) return;

                // clear data
                ucGrid_DsPhieuDT.clearData();
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
                // end clear data


                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                        + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + _KHAMBENHID
                        + "\"},{\"name\":\"[1]\",\"value\":\"" + _BENHNHANID
                        + "\"},{\"name\":\"[2]\",\"value\":" + _lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + _hosobenhanid + "\"}]}";

                string data = "page=" + 1 + "&postData=" + request + "&rows=" + 10000;

                string ret = RequestHTTP.getRequest(data);
                ResponsList ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                //"KHAMBENHID,KHAMBENHID,0,0,t,l;MAUBENHPHAMID,MAUBENHPHAMID,0,0,t,l;BENHNHANID,BENHNHANID,0,0,t,l;Số phiếu,SOPHIEU,150,0,f,l;Người tạo,NGUOITAO,225,0,f,l;Khoa,KHOADIEUTRI,280,0,f,l;" +
                //        "Phòng,PHONGDIEUTRI,280,0,f,l;Thời gian chỉ định,NGAYMAUBENHPHAM,155,0,f,l;NGUOITAO_ID,NGUOITAO_ID,0,0,t,l;KHOAID,KHOAID,0,0,t,l"

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TRANGTHAIMAUBENHPHAM", "SOPHIEU", "PHIEU_LINH", "NGUOITAO", "PHONGDIEUTRI"
                        , "NGAYMAUBENHPHAM", "NGAYMAUBENHPHAM_SUDUNG", "TENKHO", "SOTHUTUCHIDINH", "DIKEM", "LOAIPHIEU", "TRANGTHAI_PHIEU" });
                {
                    ucGrid_DsPhieuDT.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsPhieuDT.setColumnAll(false);

                    ucGrid_DsPhieuDT.setColumn("RN", 0, " ");
                    ucGrid_DsPhieuDT.setColumn("TRANGTHAIMAUBENHPHAM", 1, " ");
                    ucGrid_DsPhieuDT.setColumn("SOPHIEU", 2, "Số phiếu");
                    ucGrid_DsPhieuDT.setColumn("PHIEU_LINH", 3, "Phiếu lĩnh");
                    ucGrid_DsPhieuDT.setColumn("NGUOITAO", 4, "Bác sỹ chỉ định");
                    ucGrid_DsPhieuDT.setColumn("PHONGDIEUTRI", 5, "Phòng");
                    ucGrid_DsPhieuDT.setColumn("NGAYMAUBENHPHAM", 6, "Ngày chỉ định");
                    ucGrid_DsPhieuDT.setColumn("NGAYMAUBENHPHAM_SUDUNG", 7, "Ngày sử dụng");
                    ucGrid_DsPhieuDT.setColumn("TENKHO", 8, "Kho");
                    ucGrid_DsPhieuDT.setColumn("SOTHUTUCHIDINH", 10, "STT");
                    ucGrid_DsPhieuDT.setColumn("DIKEM", 9, "Đi kèm");
                    ucGrid_DsPhieuDT.setColumn("LOAIPHIEU", 11, "Loại phiếu");
                    ucGrid_DsPhieuDT.setColumn("TRANGTHAI_PHIEU", 12, "Trạng thái");

                    ucGrid_DsPhieuDT.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "3", "4" }
                        , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });
                    ucGrid_DsPhieuDT.setColumnImage("DIKEM", new String[] { "1" }
                        , new String[] { "./Resources/Pin.png" });

                    ucGrid_DsPhieuDT.gridView.BestFitColumns(true);
                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoaducGridDsPhieuDT(object sender, EventArgs e)
        {
            try
            {
                //string jsonFilter = string.Empty;
                ResponsList responses = new ResponsList();

                //if (ucGrid_DsPhieuDT.ReLoadWhenFilter)
                //{
                //    if (ucGrid_DsPhieuDT.tableFlterColumn.Rows.Count > 0)
                //    {
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsPhieuDT.tableFlterColumn);
                //    }
                //}

                //responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K025.LAYDL", 1, 10000,
                //    new String[] { "[0]" }, new string[] { ucCbbHoSoBA.SelectValue }, jsonFilter);

                ucGrid_DsPhieuDT.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "BENHNHANID", "KHAMBENHID", "MAKHAMBENH", "TENKHOA", "TENPHONG", "PHONGID", "KHOAID" });
                ucGrid_DsPhieuDT.setData(dt, responses.total, responses.page, responses.records);
                ucGrid_DsPhieuDT.setColumnAll(false);
                ucGrid_DsPhieuDT.setColumnMemoEdit("MAKHAMBENH", 0, "Mã điều trị", 0);
                ucGrid_DsPhieuDT.setColumnMemoEdit("TENKHOA", 1, "Khoa", 0);
                ucGrid_DsPhieuDT.setColumnMemoEdit("TENPHONG", 2, "Phòng", 0);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_ChamSoc_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_ChamSoc.gridView.OptionsView.ShowGroupPanel = false;
                //ucGrid_ChamSoc.gridView.OptionsView.ShowViewCaption = false;
                ucGrid_ChamSoc.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_ChamSoc.SetReLoadWhenFilter(true);
                ucGrid_ChamSoc.gridView.OptionsView.RowAutoHeight = true;
                ucGrid_ChamSoc.setEvent(LoaducGridDsChamSoc);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoaducGridDsChamSoc(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    //string jsonFilter = string.Empty;
                    //ResponsList responses = new ResponsList();

                    //if (ucGrid_DsDieuTri.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DsDieuTri.tableFlterColumn.Rows.Count > 0)
                    //    {
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsDieuTri.tableFlterColumn);
                    //    }
                    //}

                    //responses = RequestHTTP.get_ajaxExecuteQueryPaging("NT.024.DSPHIEU", page, ucGrid_DsDieuTri.ucPage1.getNumberPerPage(),
                    //    new String[] { "[0]" }, new string[] { ucCbbHoSoBA.SelectValue }, jsonFilter);

                    //ucGrid_DsDieuTri.clearData();

                    //DataTable dt = new DataTable();
                    //dt = MyJsonConvert.toDataTable(responses.rows);
                    //if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MAUBENHPHAMID", "KHAMBENHID", "BENHNHANID", "SOPHIEU", "NGUOITAO", "KHOADIEUTRI", "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "NGUOITAO_ID" });
                    //ucGrid_DsDieuTri.setData(dt, responses.total, responses.page);
                    //ucGrid_DsDieuTri.setColumnAll(false);
                    //ucGrid_DsDieuTri.setColumnMemoEdit("SOPHIEU", 0, "Số phiếu", 0);
                    //ucGrid_DsDieuTri.setColumnMemoEdit("NGUOITAO", 1, "Bác sỹ chỉ định", 0);
                    //ucGrid_DsDieuTri.setColumnMemoEdit("KHOADIEUTRI", 2, "Khoa", 0);
                    //ucGrid_DsDieuTri.setColumnMemoEdit("PHONGDIEUTRI", 3, "Phòng", 0);
                    //ucGrid_DsDieuTri.setColumnMemoEdit("NGAYMAUBENHPHAM", 4, "Thời gian chỉ định", 0);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DSXuatAn_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsSuatAn.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                //ucGrid_DsSuatAn.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsSuatAn.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsSuatAn.gridView.FocusedRowChanged += Grid_DsSuatAn_RowClick;
                ucGrid_DsSuatAn.Set_HidePage(false);
                setGrid_DSSuatAn();

                ucGrid_DsSuatAnCT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsSuatAnCT.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsSuatAnCT.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsSuatAnCT.Set_HidePage(false);
                setGrid_DSSuatAnCT();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setGrid_DSSuatAn()
        {
            try
            {
                //string jsonFilter = string.Empty;
                //ResponsList responses = new ResponsList();

                //if (ucGrid_DSSuatAn.ReLoadWhenFilter)
                //{
                //    if (ucGrid_DSSuatAn.tableFlterColumn.Rows.Count > 0)
                //    {
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DSSuatAn.tableFlterColumn);
                //    }
                //}

                // load data

                //not yet
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
                //string jsonFilter = string.Empty;
                //ResponsList responses = new ResponsList();

                //if (ucGrid_DSSuatAn.ReLoadWhenFilter)
                //{
                //    if (ucGrid_DSSuatAn.tableFlterColumn.Rows.Count > 0)
                //    {
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DSSuatAn.tableFlterColumn);
                //    }
                //}

                // load data

                //not yet
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

        private void ClearData(UserControl.ucGridview grid)
        {
            grid.gridControl.DataSource = null;
            grid.gridView.Columns.Clear();
        }

        private void ucGrid_DsTruyenDich_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsTruyenDich.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                //ucGrid_DsTruyenDich.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsTruyenDich.gridView.OptionsView.ShowAutoFilterRow = false;
                //ucGrid_DsTruyenDich.gridView.FocusedRowChanged += Grid_DSSuatAn_RowClick;
                ucGrid_DsTruyenDich.Set_HidePage(false);
                //setGrid_DsTruyenDich();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DsPUThuoc_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsPUThuoc.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                //ucGrid_DsPUThuoc.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsPUThuoc.gridView.OptionsView.ShowAutoFilterRow = false;
                //ucGrid_DsPUThuoc.gridView.FocusedRowChanged += Grid_DSSuatAn_RowClick;
                ucGrid_DsPUThuoc.Set_HidePage(false);
                //setGrid_DsPUThuoc();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucGrid_DsHoiChan_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsHoiChan.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                //ucGrid_DsHoiChan.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsHoiChan.gridView.OptionsView.ShowAutoFilterRow = false;
                //ucGrid_DsHoiChan.gridView.FocusedRowChanged += Grid_DSSuatAn_RowClick;
                ucGrid_DsHoiChan.Set_HidePage(false);
                //setGrid_DsHoiChan();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
    }
}
