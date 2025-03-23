using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using VNPT.HIS.Controls.Class;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K019_DonThuocCu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K019_DonThuocCu()
        {
            InitializeComponent();
        }

        string BENHNHANID;
        public void loadData(string benhnhanId)
        {
            this.BENHNHANID = benhnhanId;
        }

        private void NGT02K019_DonThuocCu_Load(object sender, EventArgs e)
        { 
            try
            {
                ucGrid_DonThuocCu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                // Hiển thị dòng filter 
                ucGrid_DonThuocCu.setEvent(setGridDonThuocCu);
                ucGrid_DonThuocCu.setEvent_FocusedRowChanged(SetGridChiTiet);
                ucGrid_DonThuocCu.setEvent_DoubleClick(btnChon_Click);
                ucGrid_DonThuocCu.SetReLoadWhenFilter(true);
                //ucGrid_DonThuocCu.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
                ucGrid_DonThuocCu.onIndicator();

                setGridDonThuocCu(1, null);

                ucGrid_ChiTiet.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_ChiTiet.Set_HidePage(false);
                ucGrid_ChiTiet.onIndicator();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setGridDonThuocCu(object sender, EventArgs e)
        {
            try
            {
                string benhNhanId = this.BENHNHANID;
                int page = (int)sender;
                if (page > 0)
                {
                    ResponsList ds = new ResponsList();
                    //string jsonFilter = "";
                    //if (ucGrid_DonThuocCu.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DonThuocCu.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DonThuocCu.tableFlterColumn);
                    //}

                    // Load dữ liệu
                    ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NGT02K019.LAYDL", page, ucGrid_DonThuocCu.ucPage1.getNumberPerPage(),
                        new String[] { "[0]" }, new string[] { benhNhanId });
                  
                    ucGrid_ChiTiet.clearData_frmTiepNhan();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows); 
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "SOPHIEU", "CHANDOAN", "FULL_NAME", "ORG_NAME", "NGAYMAUBENHPHAM", "TENKHO" });

                    ucGrid_DonThuocCu.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DonThuocCu.setColumnAll(false);                    
                    ucGrid_DonThuocCu.setColumn("SOPHIEU", 1, "Số phiếu", 0);
                    ucGrid_DonThuocCu.setColumn("CHANDOAN", 2, "Chẩn đoán", 0);
                    ucGrid_DonThuocCu.setColumn("FULL_NAME", 3, "Bác sĩ chỉ định", 0);
                    ucGrid_DonThuocCu.setColumn("ORG_NAME", 4, "Phòng", 0);
                    ucGrid_DonThuocCu.setColumn("NGAYMAUBENHPHAM", 5, "Ngày chỉ định", 0);
                    ucGrid_DonThuocCu.setColumn("TENKHO", 6, "Kho thuốc", 0);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void gridDonThuocCu_RowClick(object sender, EventArgs e)
        {
            if (ucGrid_DonThuocCu.gridView.FocusedRowHandle >= 0)
            {
                SetGridChiTiet(1, null);
            }
            else
            {
                ClearData(ucGrid_ChiTiet);
            }
        }

        private void SetGridChiTiet(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                 
                ResponsList ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NGT02K019.LAYCT", 1, 10000,
                    new String[] { "[0]" }, new string[] { drv["MAUBENHPHAMID"].ToString() });

                DataTable dt = MyJsonConvert.toDataTable(ds.rows);
                if (dt.Rows.Count == 0)
                    dt = Func.getTableEmpty(new String[] { "STT", "TEN", "SOLUONGQUYETTOAN", "SOLUONG", "TEN_DVT", "NGAYDICHVU", "DUONGDUNG", "HUONGDANSUDUNG" });
                ucGrid_ChiTiet.setData(dt, ds.total, ds.page, ds.records);
                ucGrid_ChiTiet.setColumnAll(false);
                ucGrid_ChiTiet.setColumn("STT", 0, " ");
                ucGrid_ChiTiet.setColumn("TEN", 1, "Tên thuốc", 0);
                ucGrid_ChiTiet.setColumn("SOLUONGQUYETTOAN", 2, "Số lượng kê đơn", 0);
                ucGrid_ChiTiet.setColumn("SOLUONG", 3, "Số lượng", 0);
                ucGrid_ChiTiet.setColumn("TEN_DVT", 4, "ĐVT", 0);
                ucGrid_ChiTiet.setColumn("NGAYDICHVU", 5, "Ngày dùng", 0);
                ucGrid_ChiTiet.setColumn("DUONGDUNG", 6, "Đường dùng", 0);
                ucGrid_ChiTiet.setColumn("HUONGDANSUDUNG", 7, "HDSD", 0);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            setGridDonThuocCu(1, null);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = ".xls|*.xls";
                sf.FileName = "DanhSachDonThuocCu_" + Func.getSysDatetime("yyyyMMddHHmmss");
                if (sf.ShowDialog() == DialogResult.OK && sf.FileName != "")
                    ucGrid_DonThuocCu.gridView.ExportToXls(sf.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView dataRow = ucGrid_DonThuocCu.SelectedRow;

                if (dataRow == null)
                {
                    MessageBox.Show("Chưa chọn đơn thuốc nào!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ReturnData != null)
                {
                    this.Close();
                    ReturnData(dataRow["MAUBENHPHAMID"].ToString(), null);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        //private void GridView_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view.ActiveEditor is TextEdit)
        //    {
        //        TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //        textEdit.Text = textEdit.Text.Trim();
        //    }
        //}

        private void ClearData(UserControl.ucGridview grid)
        {
            grid.gridControl.DataSource = null;
            grid.gridView.Columns.Clear();
        }

        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
         
    }
}