using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T003_danhsachphieuthu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static bool IS_START;
        private static DataRow LUY_KE_ROW;
        private static string PHONG_ID;
        private static string QUYEN_SO;
        private static string LOAI_SO;
        private static string RADIO_VALUE = "0";
        private static string VPI_KIEUIN_HOADON = "0";
        private static string HIS_IN_HOADONCHITIET = "0";
        private static string DAHUYPHIEU;
        private const string TAT_CA = "-- Tất cả --";
        private const string THU_TIEN = "1";
        private const string TAM_UNG = "3";
        private const string HOAN_UNG = "2";
        private const string THU_THEM = "6";
        private bool ISBLOCK = false;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T003_danhsachphieuthu()
        {
            InitializeComponent();
        }

        private void VPI01T003_danhsachphieuthu_Load(object sender, EventArgs e)
        {
            InitControl();

            QUYEN_SO = (string.IsNullOrWhiteSpace(cbbQuyenSo.SelectValue)) ? "-1" : cbbQuyenSo.SelectValue;
            LOAI_SO = (string.IsNullOrWhiteSpace(cbbLoaiso.SelectValue)) ? "-1" : cbbLoaiso.SelectValue;
            VPI_KIEUIN_HOADON = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VPI_KIEUIN_HOADON");
            HIS_IN_HOADONCHITIET = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "HIS_IN_HOADONCHITIET");

            InitGrid();
        }

        private void InitControl()
        {
            PHONG_ID = Const.local_phongId.ToString();
            DateTime now = Func.getSysDatetime(); IS_START = false;
            dEditTu.DateTime = new DateTime(Func.getSysDatetime().Year, Func.getSysDatetime().Month, 1);
            dEditDen.DateTime = Func.getSysDatetime();

            SetStatusButton(false, false, false);

            DataTable loaiSoDT = RequestHTTP.get_ajaxExecuteQueryO("COM.TRANGTHAI",
                    new string[] { "[0]", "[1]", "[2]" },
                    new string[] { "94", "in", "1,2,3" });

            if (loaiSoDT.Rows.Count >= 0)
            {
                DataRow dr = loaiSoDT.NewRow();
                dr[0] = string.Empty;
                dr[1] = TAT_CA;
                loaiSoDT.Rows.InsertAt(dr, 0);
            }

            cbbLoaiso.setData(loaiSoDT, 0, 1);
            cbbLoaiso.lookUpEdit.Properties.Columns[0].Visible = false;
            cbbLoaiso.SelectIndex = 0;
            cbbLoaiso.setEvent_Enter(cbbLoaiso_KeyEnter);
            cbbLoaiso.setEvent(cbo_SelectedIndexChanged);

            DataTable loaiPhieuDT = RequestHTTP.get_ajaxExecuteQueryO("COM.TRANGTHAI",
                    new string[] { "[0]", "[1]", "[2]" },
                    new string[] { "11", "in", "1,2,3" });

            cbbLoaiPhieu.setData(loaiPhieuDT, 0, 1);
            cbbLoaiPhieu.lookUpEdit.Properties.Columns[0].Visible = false;
            cbbLoaiPhieu.SelectIndex = 0;
            cbbLoaiPhieu.setEvent_Enter(cbbLoaiPhieu_KeyEnter);
            cbbLoaiPhieu.setEvent(cbo_SelectedIndexChanged);

            DataTable quyenSoDT = RequestHTTP.get_ajaxExecuteQueryO("VPI02T001.DS.SPT",
                    new string[] { "[0]" },
                    new string[] { PHONG_ID });

            if (quyenSoDT.Rows.Count <= 0)
            {
                quyenSoDT.Columns.Add("");
                quyenSoDT.Columns.Add("");
            }

            if (quyenSoDT.Rows.Count > 0)
            {
                DataRow dr = quyenSoDT.NewRow();
                dr[0] = string.Empty;
                dr[1] = TAT_CA;
                quyenSoDT.Rows.InsertAt(dr, 0);
            }

            cbbQuyenSo.setData(quyenSoDT, 0, 1);
            cbbQuyenSo.lookUpEdit.Properties.Columns[0].Visible = false;
            cbbQuyenSo.SelectIndex = 0;
            cbbQuyenSo.setEvent_Enter(cbbQuyenSo_KeyEnter);
            cbbQuyenSo.setEvent(cbo_SelectedIndexChanged);

            DataTable trangThaiDT = RequestHTTP.get_ajaxExecuteQueryO("COM.TRANGTHAI",
                    new string[] { "[0]", "[1]", "[2]" },
                    new string[] { "95", "not in", "-1" });

            DataRow dr_tt = trangThaiDT.NewRow();
            dr_tt[0] = "-1";
            dr_tt[1] = "--- Tất cả ---";
            trangThaiDT.Rows.InsertAt(dr_tt, 0);

            cbbTrangThai.setData(trangThaiDT, 0, 1);
            cbbTrangThai.lookUpEdit.Properties.Columns[0].Visible = false;
            if (trangThaiDT.Rows.Count>1) cbbTrangThai.SelectIndex = 1;
            cbbTrangThai.setEvent_Enter(cbbTrangThai_KeyEnter);
            cbbTrangThai.setEvent(cbo_SelectedIndexChanged);
        }

        private void InitGrid()
        {
            ucGridPhieuThu.gridView.OptionsView.ColumnAutoWidth = false;
            ucGridPhieuThu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGridPhieuThu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGridPhieuThu.gridView.OptionsBehavior.Editable = false;
            ucGridPhieuThu.setMultiSelectMode(true);
            ucGridPhieuThu.onIndicator();

            ucGridPhieuThu.setEvent(LoadGridPhieuThu);
            ucGridPhieuThu.SetReLoadWhenFilter(true);
            ucGridPhieuThu.gridView.Click += GridView_Click;
            ucGridPhieuThu.gridView.CustomDrawRowIndicator += CustomDrawRowIndicator;

            ucGridPhieuThu.setNumberPerPage(new int[] { 100, 200, 300, 500, 1000 });

            IS_START = true;
        }

        private void LoadGridPhieuThu(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadGridData(pageNum);
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                ISBLOCK = true;
                int index = ucGridPhieuThu.gridView.FocusedRowHandle;

                if (!"DX$CheckboxSelectorColumn".Equals(ucGridPhieuThu.gridView.FocusedColumn.FieldName))
                {
                    if (ucGridPhieuThu.gridView.GetSelectedRows().Any(o => o == index))
                    {
                        ucGridPhieuThu.gridView.UnselectRow(index);
                    }
                    else
                    {
                        ucGridPhieuThu.gridView.SelectRow(index);
                    }

                    ucGridPhieuThu.gridView.FocusedColumn = ucGridPhieuThu.gridView.Columns["DX$CheckboxSelectorColumn"];

                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void CustomDrawRowIndicator(object sender, EventArgs e)
        {
            if (ISBLOCK)
            {
                ButtonStatus();
                CalPhieuThu();
            }
            ISBLOCK = false;
        }

        private void ButtonStatus()
        {
            if (ucGridPhieuThu.gridView.GetSelectedRows().Length > 0)
            {
                SetStatusButton(true, true, true);
            }
            else
            {
                SetStatusButton(false, false, false);
            }
        }

        private void SetStatusButton(bool isIn, bool isDuyet, bool isGoDuyet)
        {
            btnIn.Enabled = isIn;
            btnDuyet.Enabled = isDuyet;
            btnGoDuyet.Enabled = isGoDuyet;
        }

        private void CalPhieuThu()
        {
            int[] index;
            float sum = 0;
            float value;

            index = ucGridPhieuThu.gridView.GetSelectedRows();

            for (int i = 0; i < index.Length; i++)
            {
                var dataRow = (DataRowView)ucGridPhieuThu.gridView.GetRow(index[i]);
                if (dataRow != null
                    && !string.IsNullOrWhiteSpace(dataRow["DADUYET"].ToString())
                    && ("0").Equals(dataRow["DADUYET"].ToString()))
                {
                    if (!string.IsNullOrWhiteSpace(dataRow["DADUYET"].ToString())
                        && float.TryParse(dataRow["DADUYET"].ToString(), out value))
                        sum += float.Parse(dataRow["DATRA"].ToString());
                }
            }

            lblTongTrongNgay.Text = String.Format("{0:0,0.00}", (float.Parse(LUY_KE_ROW["TONGNGAY"].ToString()) + sum));
            lblLuyKe.Text = String.Format("{0:0,0.00}", (float.Parse(LUY_KE_ROW["LUYKE"].ToString()) + sum));
        }

        private void LoadGridData(int page)
        {
            try
            {
                page = (page <= 0) ? 1 : page;

                QUYEN_SO = (string.IsNullOrWhiteSpace(cbbQuyenSo.SelectValue)) ? "-1" : cbbQuyenSo.SelectValue;
                LOAI_SO = (string.IsNullOrWhiteSpace(cbbLoaiso.SelectValue)) ? "-1" : cbbLoaiso.SelectValue;

                ResponsList responses = new ResponsList();

                //string jsonFilter = string.Empty;
                //if (ucGridPhieuThu.ReLoadWhenFilter && ucGridPhieuThu.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridPhieuThu.tableFlterColumn);
                //}

                DataTable phieuThuDT = new DataTable();

                //if (dEditTu.Text.Trim() == "")
                //{
                //    MessageBox.Show("Chưa nhập trường từ ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                //    dEditTu.Focus();
                //    return;
                //}
                //if (dEditDen.Text.Trim() == "")
                //{
                //    MessageBox.Show("Chưa nhập trường đến ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                //    dEditDen.Focus();
                //    return;
                //}
                if (dEditTu.DateTime > dEditDen.DateTime
                    && dEditTu.Text.Trim() != "" && dEditDen.Text.Trim() != "")
                {
                    MessageBox.Show("Trường từ ngày không được lớn hơn trường đến ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                string tuNgay = dEditTu.Text + " 00:00:00";
                string denNgay = dEditDen.Text + " 23:59:59";

                if (dEditTu.Text.Trim() == "") tuNgay = "";
                if (dEditDen.Text.Trim() == "") denNgay = "";

                DataTable luykeDT = RequestHTTP.call_ajaxCALL_SP_O("VPI03.LUYKE", PHONG_ID + "$" + QUYEN_SO + "$" + cbbLoaiPhieu.SelectValue + "$" + denNgay, 0);

                if (luykeDT == null || luykeDT.Columns.Count <= 0) return;

                LUY_KE_ROW = luykeDT.Rows[0];

                if (LUY_KE_ROW == null) return;

                lblTongTrongNgay.Text = String.Format("{0:0,0.00}", double.Parse(LUY_KE_ROW["TONGNGAY"].ToString()));
                lblCacNgayTruoc.Text = String.Format("{0:0,0.00}", double.Parse(LUY_KE_ROW["CACNGAY"].ToString()));
                lblLuyKe.Text = String.Format("{0:0,0.00}", double.Parse(LUY_KE_ROW["LUYKE"].ToString()));

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "VPI01T003.01", page, ucGridPhieuThu.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]" },
                    new string[] { tuNgay, denNgay, txtTimKiem.Text, cbbTrangThai.SelectValue, LOAI_SO, cbbLoaiPhieu.SelectValue, QUYEN_SO, PHONG_ID, RADIO_VALUE }, ucGridPhieuThu.jsonFilter());


                ucGridPhieuThu.clearData();

                phieuThuDT = MyJsonConvert.toDataTable(responses.rows);

                foreach (DataRow row in phieuThuDT.Rows)
                {
                    row["DATRA"] = String.Format("{0:0,0.00}", float.Parse(row["DATRA"].ToString()));
                    row["MIENGIAM"] = String.Format("{0:0,0.00}", float.Parse(row["MIENGIAM"].ToString()));
                }

                if (phieuThuDT.Rows.Count == 0)
                    phieuThuDT = Func.getTableEmpty(new String[] { "PHIEUTHUID", "DADUYET", "LOAIPHIEUTHU", "NHOMPHIEUTHUID", "DAHUYPHIEU", "MAPHIEUTHU", "MANHOMPHIEUTHU", "TENBENHNHAN", "DATRA", "TRANGTHAI", "NGAYDUYET", "MIENGIAM", "LYDOMIENGIAM", "NGAYTHU", "NGUOIDUNGID" });

                ucGridPhieuThu.setData(phieuThuDT, responses.total, responses.page, responses.records);
                ucGridPhieuThu.setColumnAll(false);

                ucGridPhieuThu.setColumn("DADUYET", 1, " ");
                ucGridPhieuThu.setColumn("MAPHIEUTHU", 2, "Mã phiếu", 0);
                ucGridPhieuThu.setColumn("MANHOMPHIEUTHU", 3, "Mã sổ", 0);
                ucGridPhieuThu.setColumn("TENBENHNHAN", 4, "Tên bệnh nhân", 0);
                ucGridPhieuThu.setColumn("DATRA", 5, "Số tiền", 0);
                ucGridPhieuThu.setColumn("TRANGTHAI", 6, "Trạng thái", 0);
                ucGridPhieuThu.setColumn("NGAYDUYET", 7, "Ngày duyệt", 0);
                ucGridPhieuThu.setColumn("MIENGIAM", 8, "Miễn giảm", 0);
                ucGridPhieuThu.setColumn("LYDOMIENGIAM", 9, "Lý do", 0);
                ucGridPhieuThu.setColumn("NGAYTHU", 10, "Ngày lập", 0);
                ucGridPhieuThu.setColumn("NGUOIDUNGID", 11, "Người lập", 0);

                ucGridPhieuThu.setColumnImage("DADUYET", new String[] { "0", "1" }
                        , new String[] { "./Resources/Empty.png", "./Resources/True.png" });

                ucGridPhieuThu.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void rbNgayDuyet_CheckedChanged(object sender, EventArgs e)
        {
            RADIO_VALUE = rbNgayDuyet.Checked ? "0" : "1";
            LoadGridData(1);
        }

        private void rbNgayThu_CheckedChanged(object sender, EventArgs e)
        {
            RADIO_VALUE = rbNgayThu.Checked ? "1" : "0";
            LoadGridData(1);
        }

        private void bbtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadGridData(1);
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void cbbLoaiso_KeyEnter(object sender, EventArgs e)
        {
            dEditTu.Focus();
            //cbbLoaiSo.Focus();
        }

        private void cbbLoaiPhieu_KeyEnter(object sender, EventArgs e)
        {
            cbbQuyenSo.Focus();
            //cbbLoaiSo.Focus();
        }

        private void cbbQuyenSo_KeyEnter(object sender, EventArgs e)
        {
            cbbLoaiso.Focus();
            //cbbLoaiSo.Focus();
        }

        private void cbbTrangThai_KeyEnter(object sender, EventArgs e)
        {
            rbNgayThu.Focus();
            //cbbLoaiSo.Focus();
        }

        private void dEditTu_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IS_START)
                {
                    LoadGridData(1);
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void dEditDen_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (IS_START)
                {
                    LoadGridData(1);
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            try
            {
                DuyetPhieu(1);
                LoadGridData(1);

                btnDuyet.Enabled = false;
                btnGoDuyet.Enabled = false;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void btnGoDuyet_Click(object sender, EventArgs e)
        {
            try
            {
                DuyetPhieu(0);
                LoadGridData(1);
                
                btnDuyet.Enabled = false;
                btnGoDuyet.Enabled = false;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void DuyetPhieu(int value)
        {
            int[] index = ucGridPhieuThu.gridView.GetSelectedRows();

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < index.Length; i++)
            {
                var dataRow = (DataRowView)ucGridPhieuThu.gridView.GetRow(index[i]);
                strBuilder.Append(dataRow["PHIEUTHUID"].ToString());
                if (index.Length - (i + 1) != 0)
                {
                    strBuilder.Append(",");
                }
            }

            string phieuThuId = strBuilder.ToString();

            if (!string.IsNullOrWhiteSpace(phieuThuId))
            {
                string result = RequestHTTP.call_ajaxCALL_SP_S_result("VPI03.KHOA.PHIEUTHU", phieuThuId + "$" + value);
                MessageBox.Show(result, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private void btnInChiTiet_Click(object sender, EventArgs e)
        {
            InPhieuThu(0);
        }

        private void btnInTongHop_Click(object sender, EventArgs e)
        {
            InPhieuThu(1);
        }

        private void InPhieuThu(int type)
        {
            // 0: chi tiết, 1: tổng hợp
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                VNPT.HIS.Controls.SubForm.frmPrint frm;

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");

                table.Rows.Add("dt_ngay", "String", dEditTu.Text);
                table.Rows.Add("dt_denngay", "String", dEditDen.Text);
                table.Rows.Add("i_key", "String", txtTimKiem.Text);
                table.Rows.Add("i_trangthai", "String", cbbTrangThai.SelectValue);
                table.Rows.Add("i_loaiso", "String", LOAI_SO);
                table.Rows.Add("i_loaiphieu", "String", cbbLoaiPhieu.SelectValue);
                table.Rows.Add("i_quyenso", "String", QUYEN_SO);
                table.Rows.Add("i_phongid", "String", PHONG_ID);
                table.Rows.Add("i_type", "String", RADIO_VALUE);

                if (type == 0)
                {
                    table.Rows.Add("i_sum_luyke", "String", LUY_KE_ROW["LUYKE"].ToString());
                    table.Rows.Add("i_sum_hientai", "String", LUY_KE_ROW["TONGNGAY"].ToString());
                    table.Rows.Add("i_sum_ngaytruoc", "String", LUY_KE_ROW["CACNGAY"].ToString());
                    frm = new VNPT.HIS.Controls.SubForm.frmPrint("In chi tiết", "NGUYENTRAI_DSPHIEUTHUSOCHITIETTHU_A4_902", table, 650, 900);
                }
                else
                {
                    frm = new VNPT.HIS.Controls.SubForm.frmPrint("In tổng hợp", "NGUYENTRAI_DSPHIEUTHUNGAY_LOCHOADON_NGT_A4_902", table, 650, 900);
                }

                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            int[] index = ucGridPhieuThu.gridView.GetSelectedRows();
            var dataRow = (DataRowView)ucGridPhieuThu.gridView.GetRow(index[index.Length - 1]);
            string loaiPhieuThu = dataRow["LOAIPHIEUTHU"].ToString();
            string phieuThuId = dataRow["PHIEUTHUID"].ToString();
            DAHUYPHIEU = dataRow["DAHUYPHIEU"].ToString();

            string isDCT = RequestHTTP.getOneValue("VPI01T001.18", new string[] { "[0]", "[1]" }, new string[] { phieuThuId, phieuThuId });
            return;
            if (THU_TIEN.Equals(loaiPhieuThu))
            {
                if ("0".Equals(isDCT))
                {
                    InHoaDonVP(phieuThuId, "NGT036_HOADONGTGT_A4", "NGT037_BANGKEKEMHDGTGT_A4");
                }
                else
                {
                    InHoaDonVP(phieuThuId, "NGT039_HOADONDCT_A4", "NGT037_BANGKEKEMHDGTGT_A4");
                }
            }
            else if (TAM_UNG.Equals(loaiPhieuThu))
            {
                InHoaDonVP(phieuThuId, "NGT034_PHIEUTAMUNG_A4", string.Empty);
            }
            else if (THU_THEM.Equals(loaiPhieuThu))
            {
                InHoaDonVP(phieuThuId, "NGT034_PHIEUTAMUNG_A4", string.Empty);
            }
            else if (HOAN_UNG.Equals(loaiPhieuThu))
            {
                InHoaDonVP(phieuThuId, "NGT038_PHIEUHOANUNG_A4", string.Empty);
            }
        }

        private void InHoaDonVP(string phieuThuId, string reportCode, string reportCodeCT)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");

                table.Rows.Add("phieuthuid", "String", phieuThuId);
                string typeExport = "pdf";

                if ("1".Equals(VPI_KIEUIN_HOADON))
                {
                    InNhieuPhieu(reportCode, table, typeExport, "ifmBill");
                }
                else
                {
                    OpenReport(reportCode, table, typeExport, "ifmBill");
                }

                if ("0".Equals(HIS_IN_HOADONCHITIET)
                    || string.IsNullOrWhiteSpace(reportCodeCT)
                    || string.IsNullOrWhiteSpace(DAHUYPHIEU)
                    || "1".Equals(DAHUYPHIEU))
                    return;

                if ("1".Equals(HIS_IN_HOADONCHITIET))
                {
                    InNhieuPhieu(reportCodeCT, table, typeExport, "ifmDetail");
                }
                else
                {
                    OpenReport(reportCodeCT, table, typeExport, "ifmDetail");
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void InNhieuPhieu(string reportCode, DataTable par, string typeExport, string loai)
        {
            Func.PrintFile_FromData(reportCode, par, typeExport);
        }

        private void OpenReport(string reportCode, DataTable par, string typeExport, string loai)
        {
            PrintPreview("", reportCode, par);
        }

        private void PrintPreview(string title, string code, DataTable parTable, int height = 650, int width = 900)
        {
            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(title, code, parTable, height, width);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}