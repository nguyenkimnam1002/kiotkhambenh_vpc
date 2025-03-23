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
using VNPT.HIS.Controls.Class;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K041_PhieuChiDinhMau : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private string loaiNhomMau;
        public NGT02K041_PhieuChiDinhMau()
        {
            InitializeComponent();
        }

        public void initData(string loainhom_mau)
        {
            this.loaiNhomMau = loainhom_mau;
        }

        private void NGT02K041_PhieuChiDinhMau_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
            gridMau.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            gridMau.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            // Hiển thị dòng filter
            gridMau.gridView.OptionsView.ShowAutoFilterRow = true;
            gridMau.SetReLoadWhenFilter(true);
            gridMau.Set_HidePage(false);
            gridMau.gridView.OptionsBehavior.Editable = true;
            gridMau.gridView.OptionsBehavior.ReadOnly = false;
            gridMau.setEvent(setGridMau);
            gridMau.gridView.FocusedRowChanged += gridMau_RowClick;
            gridMau.gridView.DoubleClick += gridMau_DoubleClick;
            setGridMau(1, null);
            gridChiTiet.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            gridChiTiet.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            // Hiển thị dòng filter
            gridChiTiet.gridView.OptionsView.ShowAutoFilterRow = true;
            gridChiTiet.SetReLoadWhenFilter(true);
            gridChiTiet.Set_HidePage(false);
            gridChiTiet.setEvent(setGridChiTiet);
        }

        private void setGridMau(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    ResponsList ds = new ResponsList();
                    //string jsonFilter = "";
                    //if (gridMau.ReLoadWhenFilter)
                    //{
                    //    if (gridMau.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridMau.tableFlterColumn);
                    //}

                    // Load dữ liệu
                    ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NGT02K041.DSMAU", page, gridMau.ucPage1.getNumberPerPage(),
                        new String[] { "[0]", "[1]" }, new string[] { Const.local_khoaId.ToString(), loaiNhomMau });
                    gridMau.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    dt.Columns.Add("ACT");
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "RN", "MAUBENHPHAM_TEMPID", "TENMAUBENHPHAM_TEMP", "KHOID", "ACT" });
                    gridMau.setData(dt, ds.total, ds.page, ds.records);
                    gridMau.setColumnAll(false);
                    gridMau.setColumn("RN", 0, " ", 30);
                    gridMau.setColumn("TENMAUBENHPHAM_TEMP", 1, "Tên mẫu", 200);
                    gridMau.setColumn("ACT", 2, "Xóa");
                    // Thiết lập thuộc tính AlowEdit cho từng columns
                    gridMau.gridView.Columns["RN"].OptionsColumn.AllowEdit = false;
                    gridMau.gridView.Columns["TENMAUBENHPHAM_TEMP"].OptionsColumn.AllowEdit = false;
                    gridMau.gridView.Columns["ACT"].OptionsColumn.AllowEdit = true;
                    gridMau.gridView.Columns["ACT"].OptionsColumn.ReadOnly = true;
                    gridMau.setColumnButtonImage("ACT", "delete.png", btnDelete_Click);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setGridChiTiet(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    ResponsList ds = new ResponsList();
                    //string jsonFilter = "";
                    //if (gridChiTiet.ReLoadWhenFilter)
                    //{
                    //    if (gridChiTiet.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridChiTiet.tableFlterColumn);
                    //}

                    // Load dữ liệu
                    DataRow dr = (DataRow)gridMau.gridView.GetDataRow(gridMau.gridView.FocusedRowHandle);
                    if (dr != null)
                    {
                        ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NGT02K041.CT", page, gridMau.ucPage1.getNumberPerPage(),
                        new String[] { "[0]" }, new string[] { dr["MAUBENHPHAM_TEMPID"].ToString() });
                        gridChiTiet.clearData();

                        DataTable dt = new DataTable();
                        dt = MyJsonConvert.toDataTable(ds.rows);
                        if (dt.Rows.Count == 0)
                            dt = Func.getTableEmpty(new String[] { "RN", "THUOCVATTUID", "TEN", "SOLUONG", "TEN_DVT", "GIANHANDAN" });
                        gridChiTiet.setData(dt, ds.total, ds.page, ds.records);
                        gridChiTiet.setColumnAll(false);
                        gridChiTiet.setColumn("THUOCVATTUID", -1, "id");
                        gridChiTiet.setColumn("RN", 0, " ", 30);
                        gridChiTiet.setColumn("TEN", 1, "Tên dịch vụ", 300);
                        gridChiTiet.setColumn("SOLUONG", 2, "Số lượng", 80);
                        gridChiTiet.setColumn("TEN_DVT", 3, "ĐVT", 80);
                        gridChiTiet.setColumn("GIANHANDAN", 4, "Giá tiền", 80);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = (DataRow)gridMau.gridView.GetDataRow(gridMau.gridView.FocusedRowHandle);
            if (dr != null)
            {
                string ret = ServiceChiDinhDichVu.ajaxCALL_SP_I("NGT02K041.DEL", dr["MAUBENHPHAM_TEMPID"].ToString());
                if (ret == "1")
                    setGridMau(1, null);
            }
        }

        private void gridMau_RowClick(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            setGridChiTiet(1, null);
        }

        private void gridMau_DoubleClick(object sender, EventArgs e)
        {
            btnChon_Click(1, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            frm.Close();
        }

        // Hàm gửi sự kiện đến form cha
        protected EventHandler temp_presc_success;
        public void raiseEvent(EventHandler eventChangeValue)
        {
            temp_presc_success = eventChangeValue;
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = (DataRow)gridMau.gridView.GetDataRow(gridMau.gridView.FocusedRowHandle);
                if (dr != null)
                {
                    Form frm = this.FindForm();
                    frm.Close();
                    temp_presc_success(dr["MAUBENHPHAM_TEMPID"].ToString(), null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void NGT02K041_PhieuChiDinhMau_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }
    }
}