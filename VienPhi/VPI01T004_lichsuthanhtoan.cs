using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using VNPT.HIS.Common;
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T004_lichsuthanhtoan : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
          log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T004_lichsuthanhtoan()
        {
            InitializeComponent();
        }

        private static string TIEPNHANID = "-1";
        private static string MUCHUONG = "0";

        private static string SQL_TIEPNHAN = "VPI01T004.01";
        private static string SQL_DICHVU_KHAMBENH = "VPI01T004.04";
        private static string SQL_CBO_NHOMBHYT = "VPI01T004.05";
        private static string SQL_TINHTONGTIEN = "VPI01T001.05";
        private static string SQL_DAGIAODICH = "VPI01T001.06";

        private const string UNIT = " đ";

        public void SetData(string tiepNhanID)
        {
            TIEPNHANID = tiepNhanID;
        }

        private void VPI01T004_lichsuthanhtoan_Load(object sender, EventArgs e)
        {
            InitForm();
            LoadTiepNhan();
            LoadVienPhi();
            LoadDSCT();
        }

        private void InitForm()
        {
            DataTable dt_NhomBHYT = RequestHTTP.get_ajaxExecuteQuery(SQL_CBO_NHOMBHYT);
            if (dt_NhomBHYT == null || dt_NhomBHYT.Rows.Count <= 0)
            {
                dt_NhomBHYT = Func.getTableEmpty(new String[] { "col1", "col2" });
            }

            DataRow dr_NhomBHYT = dt_NhomBHYT.NewRow();
            dr_NhomBHYT["col1"] = "-1";
            dr_NhomBHYT["col2"] = "--- Tất cả ---";
            dt_NhomBHYT.Rows.InsertAt(dr_NhomBHYT, 0);
            uccbox_NhomBHYT.setData(dt_NhomBHYT, 0, 1);
            uccbox_NhomBHYT.SelectIndex = 0;
            uccbox_NhomBHYT.setColumn(0, false);
            uccbox_NhomBHYT.setEvent(Load_SearchData);

            DataTable dt_MaThe = RequestHTTP.get_ajaxExecuteQuery("VPI01T004.12", new string[] { "[0]" }, new string[] { TIEPNHANID });
            uccbox_MaThe.setData(dt_MaThe, 0, 1);
            uccbox_MaThe.SelectIndex = 0;
            uccbox_MaThe.setColumn(0, false);
            uccbox_MaThe.setEvent(Load_SearchData);
        }

        private void LoadTiepNhan()
        {
            var maThe = (!string.IsNullOrWhiteSpace(uccbox_MaThe.SelectValue)) ? uccbox_MaThe.SelectValue : "-1";
            DataTable vienPhiDT = RequestHTTP.call_ajaxCALL_SP_O(SQL_TIEPNHAN, TIEPNHANID + "$" + maThe, 0);

            if (vienPhiDT.Rows.Count <= 0)
            {
                return;
            }

            MUCHUONG = vienPhiDT.Rows[0]["MUCHUONG"].ToString();
            etext_MaBN.Text = vienPhiDT.Rows[0]["MABN"].ToString();
            etext_TenBN.Text = vienPhiDT.Rows[0]["TENBN"].ToString();
            etext_NamSinh.Text = vienPhiDT.Rows[0]["NAMSINH"].ToString();
            etext_Tuoi.Text = vienPhiDT.Rows[0]["TUOI"].ToString();
            etext_DoiTuongBN.Text = vienPhiDT.Rows[0]["DOITUONGBN"].ToString();
            etext_SoThe.Text = vienPhiDT.Rows[0]["SOTHE"].ToString();
            edate_GTTheTu.Text = vienPhiDT.Rows[0]["GIATRITHETU"].ToString();
            edate_GTTheDen.Text = vienPhiDT.Rows[0]["GIATRITHEDEN"].ToString();

            LoadDataGridCT(1);
        }

        private void LoadDSCT()
        {
            try
            {
                ucgview_DSCT.gridView.OptionsView.ColumnAutoWidth = false;
                ucgview_DSCT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucgview_DSCT.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucgview_DSCT.gridView.OptionsBehavior.Editable = false;
                ucgview_DSCT.setMultiSelectMode(false);

                ucgview_DSCT.setEvent(ucgview_DSCT_Load);
                ucgview_DSCT.SetReLoadWhenFilter(true);

                ucgview_DSCT.setNumberPerPage(new int[] { 200, 300 });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucgview_DSCT_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadDataGridCT(pageNum);
        }

        private void Load_SearchData(object sender, EventArgs e)
        {
            LoadDataGridCT(1);
        }

        private void LoadDataGridCT(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                ResponsList responses = new ResponsList();

                //string jsonFilter = "";
                //if (ucgview_DSCT.ReLoadWhenFilter && ucgview_DSCT.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSCT.tableFlterColumn);
                //}

                DataTable dt_DSCT = new DataTable();
                var maThe = (!string.IsNullOrWhiteSpace(uccbox_MaThe.SelectValue)) ? uccbox_MaThe.SelectValue : "-1";
                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    SQL_DICHVU_KHAMBENH, page, ucgview_DSCT.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[0]", "[0]", "[0]", "[0]", "[0]", "[0]" },
                    new string[] { TIEPNHANID, uccbox_NhomBHYT.SelectValue, etext_TTTTVienPhi.Text, "-1", "-1", "-1", maThe }, ucgview_DSCT.jsonFilter());

                ucgview_DSCT.clearData();

                dt_DSCT = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSCT.Rows.Count == 0)
                    dt_DSCT = Func.getTableEmpty(new String[] { "NGAYDICHVU", "SOPHIEU", "MADICHVU", "TENNHOM", "TENDICHVU", "SOLUONG", "DONVI",
                            "TIENDICHVU", "THANHTIEN", "TIEN_BHYT_TRA", "TIEN_MIENGIAM", "TYLE", "LOAITTMOI", "KHOA", "PHONG", "NGUOICHIDINH",
                        "LOAIDOITUONG", "TRANGTHAIDICHVU", "LOAINHOMMAUBENHPHAM", "TYLE_BHYT_TRA", "TYLE_DV", "TIEN_DICHVU" });

                ucgview_DSCT.setData(dt_DSCT, responses.total, responses.page, responses.records);
                ucgview_DSCT.setColumnAll(false);

                ucgview_DSCT.setColumn("NGAYDICHVU", 0, "Ngày", 100);
                ucgview_DSCT.setColumn("SOPHIEU", 1, "Số phiếu", 100);
                ucgview_DSCT.setColumn("MADICHVU", 2, "Mã dịch vụ", 100);
                ucgview_DSCT.setColumn("TENNHOM", 3, "Nhóm BHYT", 100);
                ucgview_DSCT.setColumn("TENDICHVU", 4, "Tên thuốc + dịch vụ", 100);
                ucgview_DSCT.setColumn("SOLUONG", 5, "SL", 100);
                ucgview_DSCT.setColumn("DONVI", 6, "Đơn vị", 100);
                ucgview_DSCT.setColumn("TIENDICHVU", 7, "Giá tiền", 100);
                ucgview_DSCT.setColumn("THANHTIEN", 8, "Thành tiền", 100);
                ucgview_DSCT.setColumn("TIEN_BHYT_TRA", 9, "BHYT trả", 100);
                ucgview_DSCT.setColumn("TIEN_MIENGIAM", 10, "Miễn giảm", 100);
                ucgview_DSCT.setColumn("TYLE", 11, "Tỷ lệ TT", 100);
                ucgview_DSCT.setColumn("LOAITTMOI", 12, "Loại TT", 100);
                ucgview_DSCT.setColumn("KHOA", 13, "Khoa", 200);
                ucgview_DSCT.setColumn("PHONG", 14, "Phòng", 200);
                ucgview_DSCT.setColumn("NGUOICHIDINH", 15, "Người chỉ định", 100);

                ucgview_DSCT.gridView.BestFitColumns(true);

                for (int i = 0; i < dt_DSCT.Rows.Count; i++)
                {
                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "1"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "2"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "3")
                        echeck_BHYT.Checked = true;

                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "4"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "11"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "6"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "5")
                        echeck_ThuPhi.Checked = true;

                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "7"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "8"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "9")
                        echeck_HaoPhi.Checked = true;

                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "5")
                        echeck_DiKem.Checked = true;

                    if (dt_DSCT.Rows[0]["TRANGTHAIDICHVU"].ToString() == "3")
                        ucgview_DSCT.gridView.RowStyle += ucgview_DSCT_RowStyleBlue;

                    if ((dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "1"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "2"
                        || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "3")
                        && dt_DSCT.Rows[0]["LOAINHOMMAUBENHPHAM"].ToString() != "16"
                        && MUCHUONG != dt_DSCT.Rows[0]["TYLE_BHYT_TRA"].ToString()
                        && dt_DSCT.Rows[0]["TYLE_DV"].ToString() != "0"
                        && dt_DSCT.Rows[0]["SOLUONG"].ToString() != "0"
                        && (dt_DSCT.Rows[0]["LOAINHOMMAUBENHPHAM"].ToString() != "3"
                        || dt_DSCT.Rows[0]["TIEN_DICHVU"].ToString() != "0"))
                        ucgview_DSCT.gridView.RowStyle += ucgview_DSCT_RowStyleRed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void ucgview_DSCT_RowStyleBlue(object sender, RowStyleEventArgs e)
        {
            for (int i = 0; i < ucgview_DSCT.gridView.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)ucgview_DSCT.gridView.GetRow(i);
                e.Appearance.ForeColor = Color.Blue;
                e.HighPriority = true;
            }
        }

        private void ucgview_DSCT_RowStyleRed(object sender, RowStyleEventArgs e)
        {
            for (int i = 0; i < ucgview_DSCT.gridView.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)ucgview_DSCT.gridView.GetRow(i);
                e.Appearance.ForeColor = Color.Red;
                e.HighPriority = true;
            }
        }

        private void LoadVienPhi()
        {
            DataTable vienPhiDT = RequestHTTP.call_ajaxCALL_SP_O(SQL_TINHTONGTIEN, TIEPNHANID, 0);
            DataTable thanhToanDT = RequestHTTP.call_ajaxCALL_SP_O(SQL_DAGIAODICH, TIEPNHANID, 0);

            if (vienPhiDT.Rows.Count <= 0 || thanhToanDT.Rows.Count <= 0) return;

            var vienPhiRow = vienPhiDT.Rows[0];
            var thanhToanRow = thanhToanDT.Rows[0];

            label_DaThanhToan.Text = String.Format("{0:0,0.00}", double.Parse(thanhToanRow["DANOP"].ToString())) + UNIT;
            label_TamUng.Text = String.Format("{0:0,0.00}", double.Parse(thanhToanRow["TAMUNG"].ToString()) - double.Parse(thanhToanRow["HOANUNG"].ToString())) + UNIT;
            label_TongChiPhi.Text = String.Format("{0:0,0.00}", double.Parse(vienPhiRow["TONGTIENDV"].ToString())) + UNIT;
            label_BHTra.Text = String.Format("{0:0,0.00}", double.Parse(vienPhiRow["BHYT_THANHTOAN"].ToString())) + UNIT;
            label_BNTra.Text = String.Format("{0:0,0.00}", double.Parse(vienPhiRow["TONGTIENDV"].ToString()) - double.Parse(vienPhiRow["BHYT_THANHTOAN"].ToString()) - double.Parse(vienPhiRow["MIENGIAMDV"].ToString())) + UNIT;
            label_MienGiam.Text = String.Format("{0:0,0.00}", double.Parse(thanhToanRow["MIENGIAMDV"].ToString()) + double.Parse(thanhToanRow["MIENGIAM"].ToString())) + UNIT;
            label_ChenhLech.Text = String.Format("{0:0,0.00}", double.Parse(thanhToanRow["TAMUNG"].ToString()) - (double.Parse(vienPhiRow["TONGTIENDV"].ToString()) - double.Parse(vienPhiRow["BHYT_THANHTOAN"].ToString()) - double.Parse(vienPhiRow["MIENGIAMDV"].ToString()) - double.Parse(vienPhiRow["DANOP"].ToString()))) + UNIT;
            label_ThanhToanThem.Text = String.Format("{0:0,0.00}", double.Parse(vienPhiRow["TONGTIENDV"].ToString()) - double.Parse(vienPhiRow["BHYT_THANHTOAN"].ToString()) - double.Parse(vienPhiRow["MIENGIAMDV"].ToString()) - double.Parse(vienPhiRow["DANOP"].ToString())) + UNIT;
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void etext_TTTTVienPhi_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataGridCT(1);
        }
    }
}