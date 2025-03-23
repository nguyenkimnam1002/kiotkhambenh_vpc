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

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K053_THIETLAP_MH_LCD : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ResponsList ds = new ResponsList();
        DataTable dt = null;

        #endregion

        #region Init Form
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K053_THIETLAP_MH_LCD()
        {
            InitializeComponent();
        }
        private void NGT02K053_THIETLAP_MH_LCD_Load(object sender, EventArgs e)
        {
            init_Control();
            getDataLoaiPhong_table(1, null);
            getDataManHinh_table(1, null);
        }
        private void init_Control()
        {
            try
            {

                #region Combo Loại phòng
                DataTable dt = RequestHTTP.get_ajaxExecuteQuery("DMC04.04");
                if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                cbLoaiPhong.setData(dt, 0, 1);
                cbLoaiPhong.setColumnAll(false);
                cbLoaiPhong.setColumn(1, true);
                cbLoaiPhong.SelectIndex = 0;
                cbLoaiPhong.setEvent(ucLoaiPhong_Change);
                cbLoaiPhong.Focus();
                #endregion

                #region Combo Màn hình
                DataTable dtMH = RequestHTTP.get_ajaxExecuteQuery("DMC_MH_LCD");
                if (dtMH == null || dtMH.Rows.Count == 0) dtMH = Func.getTableEmpty(new string[] { "col1", "col2" });
                cbManHinh.setData(dtMH, 0, 1);
                cbManHinh.setColumnAll(false);
                cbManHinh.setColumn(1, true);
                cbManHinh.SelectIndex = 0;
                cbManHinh.setEvent(ucManHinh_Change);
                #endregion

                #region grid Loại phòng

                ucGridLoaiPhong.Set_HidePage(false);
                ucGridLoaiPhong.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridLoaiPhong.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridLoaiPhong.setEvent(getDataLoaiPhong_table);
                //ucGridLoaiPhong.setEvent_FocusedRowChanged(ucGridLoaiPhong_Change_SelectRow);
                //ucGridLoaiPhong.setEvent_DoubleClick(DoubleClick);
                //ucGridLoaiPhong.setEvent_MenuPopupClick(MenuPopupClick);
                ucGridLoaiPhong.SetReLoadWhenFilter(false);
                ucGridLoaiPhong.gridView.Click += GridViewLoaiPhong_Click;
                //ucGridLoaiPhong.gridView.OptionsBehavior.ReadOnly = false;
                ucGridLoaiPhong.gridView.OptionsBehavior.Editable = false;

                ucGridLoaiPhong.gridView.OptionsView.ShowViewCaption = true;
                //ucGridLoaiPhong.gridView.OptionsView.ColumnAutoWidth = true;

                //ucGridLoaiPhong.addMenuPopup(Menu_Popup());
                ucGridLoaiPhong.gridView.OptionsCustomization.AllowSort = false;
                ucGridLoaiPhong.setMultiSelectMode(true);
                ucGridLoaiPhong.onIndicator();
                #endregion

                #region grid Màn hình

                ucGridManHinh.Set_HidePage(false);
                ucGridManHinh.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridManHinh.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridManHinh.setEvent(getDataManHinh_table);
                //ucGridManHinh.setEvent_FocusedRowChanged(ucGridLoaiPhong_Change_SelectRow);
                //ucGridManHinh.setEvent_DoubleClick(DoubleClick);
                //ucGridManHinh.setEvent_MenuPopupClick(MenuPopupClick);
                ucGridManHinh.SetReLoadWhenFilter(false);
                ucGridManHinh.gridView.Click += GridViewManHinh_Click;
                //ucGridManHinh.gridView.OptionsBehavior.ReadOnly = false;
                ucGridManHinh.gridView.OptionsBehavior.Editable = false;

                ucGridManHinh.gridView.OptionsView.ShowViewCaption = true;
                //ucGridManHinh.gridView.OptionsView.ColumnAutoWidth = true;

                //ucGridManHinh.addMenuPopup(Menu_Popup());
                ucGridManHinh.gridView.OptionsCustomization.AllowSort = false;
                ucGridManHinh.setMultiSelectMode(true);
                ucGridManHinh.onIndicator();
                #endregion

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion

        #region Load dữ liệu
        private void getDataLoaiPhong_table(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    //// Lấy điều kiện filter
                    //if (ucGridLoaiPhong.ReLoadWhenFilter)
                    //{
                    //    if (ucGridLoaiPhong.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridLoaiPhong.tableFlterColumn);
                    //}

                    ucGridLoaiPhong.clearData();

                    //Lấy Loại phòng từ combobox
                    string loaiphong = "";
                    DataRowView drvLPhong = cbLoaiPhong.SelectDataRowView;
                    if (drvLPhong != null)
                        loaiphong = drvLPhong["col1"].ToString();

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("DMC54.GETTHIETLAP", page, 500, new string[] { "[0]"}, new string[] { loaiphong }, ucGridLoaiPhong.jsonFilter());
                    dt = MyJsonConvert.toDataTable(ds.rows);

                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "CHECKED", "IDPHONG", "MAPHONG", "RN", "TENPHONG" });
                    

                    ucGridLoaiPhong.setData(dt, ds.total, ds.page, ds.records);

                    //ucGridLoaiPhong.ucPage1.setData(dt.Rows.Count, ucGridLoaiPhong.ucPage1.Current());
                    //loadDSBA(page);

                    ucGridLoaiPhong.setColumnAll(false);
                    //ucGridLoaiPhong.gridView.OptionsView.ColumnAutoWidth = false;
                    ucGridLoaiPhong.gridView.BestFitColumns(true);

                    //ucGridLoaiPhong.setColumn("RN", 0, "STT", 40);

                    ucGridLoaiPhong.setColumn("TENPHONG", 2, "Phòng");
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void getDataManHinh_table(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    //// Lấy điều kiện filter
                    //if (ucGridManHinh.ReLoadWhenFilter)
                    //{
                    //    if (ucGridManHinh.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridManHinh.tableFlterColumn);
                    //}

                    ucGridManHinh.clearData();

                    //Lấy Loại phòng từ combobox
                    string manhinh = "";
                    DataRowView drvMHinh = cbManHinh.SelectDataRowView;
                    if (drvMHinh != null)
                        manhinh = drvMHinh["col1"].ToString();

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("DS_QUAY_MHLCD", page, 500, new string[] { "[0]" }, new string[] { manhinh }, ucGridManHinh.jsonFilter());
                    dt = MyJsonConvert.toDataTable(ds.rows);

                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "ORG_ID", "ORG_NAME", "RN" });


                    ucGridManHinh.setData(dt, ds.total, ds.page, ds.records);

                    //ucGridManHinh.ucPage1.setData(dt.Rows.Count, ucGridManHinh.ucPage1.Current());
                    //loadDSBA(page);

                    ucGridManHinh.setColumnAll(false);
                    //ucGridManHinh.gridView.OptionsView.ColumnAutoWidth = false;
                    ucGridManHinh.gridView.BestFitColumns(true);

                    //ucGridManHinh.setColumn("RN", 0, "STT", 40);

                    ucGridManHinh.setColumn("ORG_NAME", 2, "Danh sách phòng thiết lập");
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        #endregion

        #region Change
        private void ucLoaiPhong_Change(object sender, EventArgs e)
        {
            getDataLoaiPhong_table(1, null);
        }
        private void ucManHinh_Change(object sender, EventArgs e)
        {
            getDataManHinh_table(1, null);
        }

        private void GridViewLoaiPhong_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucGridLoaiPhong.gridView.FocusedColumn.FieldName))
            {
                int idx = ucGridLoaiPhong.gridView.FocusedRowHandle;
                if (ucGridLoaiPhong.gridView.GetSelectedRows().Any(o => o == idx))
                {
                    ucGridLoaiPhong.gridView.UnselectRow(idx);
                }
                else
                {
                    ucGridLoaiPhong.gridView.SelectRow(idx);
                }
            }
        }
        private void GridViewManHinh_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucGridManHinh.gridView.FocusedColumn.FieldName))
            {
                int idx = ucGridManHinh.gridView.FocusedRowHandle;
                if (ucGridManHinh.gridView.GetSelectedRows().Any(o => o == idx))
                {
                    ucGridManHinh.gridView.UnselectRow(idx);
                }
                else
                {
                    ucGridManHinh.gridView.SelectRow(idx);
                }
            }
        }
        #endregion

        #region Thêm, hủy
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int[] idxSelectRows = ucGridLoaiPhong.gridView.GetSelectedRows();
                if (idxSelectRows.Length == 0)
                {
                    XtraMessageBox.Show("Hãy chọn phòng muốn thiết lập", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (idxSelectRows.Length > 0)
                {
                    DataRowView drView;
                    string json_in = "";
                    string json = "";
                    string json2 = "";
                    string ret = "";

                    string manhinh = "";
                    DataRowView drvMHinh = cbManHinh.SelectDataRowView;
                    if (drvMHinh != null)
                        manhinh = drvMHinh["col1"].ToString();

                    for (int i = 0; i < idxSelectRows.Length; i++)
                    {
                        drView = (DataRowView)ucGridLoaiPhong.gridView.GetRow(idxSelectRows[i]);
                        if (drView != null)
                        {
                            json2 = "";
                            json2 += Func.json_item("PHONGID", drView["IDPHONG"].ToString());
                            json2 += Func.json_item("MANHINHID", manhinh);
                            json2 = Func.json_item_end(json2);

                            json += json2 + ",";
                        }
                    }
                    json_in = "[" + json.Substring(0, json.Length - 1).Replace("\"", "\\\"").Replace("//", "") + "]";

                    ret = RequestHTTP.call_ajaxCALL_SP_I("THIETLAP_MH_LCD", json_in);
                    if (ret != "1")
                    {
                        XtraMessageBox.Show("Thiết lập không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        getDataManHinh_table(1, null);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                int[] idxSelectRows = ucGridManHinh.gridView.GetSelectedRows();
                if (idxSelectRows.Length == 0)
                {
                    XtraMessageBox.Show("Hãy chọn phòng muốn hủy", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (idxSelectRows.Length > 0)
                {
                    DataRowView drView;
                    string json_in = "";
                    string json = "";
                    string json2 = "";
                    string ret = "";

                    string manhinh = "";
                    DataRowView drvMHinh = cbManHinh.SelectDataRowView;
                    if (drvMHinh != null)
                        manhinh = drvMHinh["col1"].ToString();

                    for (int i = 0; i < idxSelectRows.Length; i++)
                    {
                        drView = (DataRowView)ucGridManHinh.gridView.GetRow(idxSelectRows[i]);
                        if (drView != null)
                        {
                            json2 = "";
                            json2 += Func.json_item("PHONGID", drView["ORG_ID"].ToString());
                            json2 += Func.json_item("MANHINHID", manhinh);
                            json2 = Func.json_item_end(json2);

                            json += json2 + ",";
                        }
                    }
                    json_in = "[" + json.Substring(0, json.Length - 1).Replace("\"", "\\\"").Replace("//", "") + "]";

                    ret = RequestHTTP.call_ajaxCALL_SP_I("HUY_THIETLAP_MH_LCD", json_in);
                    if (ret != "1")
                    {
                        XtraMessageBox.Show("Hủy thiết lập không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        getDataManHinh_table(1, null);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion
    }
}