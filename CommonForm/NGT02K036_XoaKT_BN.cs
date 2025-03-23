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
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K036_XoaKT_BN : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ResponsList ds = new ResponsList();
        DataTable dt = new DataTable();
        DataRow dr = null;
        #endregion

        #region Init Form
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K036_XoaKT_BN()
        {
            InitializeComponent();
        }

        private void NGT02K036_XoaKT_BN_Load(object sender, EventArgs e)
        {
            LoadCauHinh();
            Form_Init();
        }
        #endregion

        #region Load dữ liệu
        private void LoadCauHinh()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = RequestHTTP.call_ajaxCALL_SP_O("LAY.CAUHINH", "", 0);
                if (dt == null && dt.Rows.Count > 0) //dt = Func.getTableEmpty(new string[] { "CH_KETTHUCKHAM", "NGAYPK", "KETHUCKHAM_BN", "XOA_BN", "CONFIGBACSI", "CHECK_24H", "KEDONTHUOC_CHITIET_NTU", "DK_MOBENHAN", "HIDE_BTN_MO_BA", "MAPHONGTRUC", "CHUPANH", "ANBANGT", "HIDEDONTHUOCKT", "CHECKTIEN" });
                {
                    if (dt.Rows[0]["KETHUCKHAM_BN"].ToString() == "0")
                    {
                        btnKetThucKham.Enabled = true;
                    }
                    else
                    {
                        btnKetThucKham.Enabled = false;
                    }

                    if (dt.Rows[0]["XOA_BN"].ToString() == "0")
                    {
                        btnXoaDanhSach.Enabled = true;
                    }
                    else
                    {
                        btnXoaDanhSach.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        private void Form_Init()
        {
            try
            {
                datTuNgay.DateTime = DateTime.Today;

                DataTable dt = new DataTable();
                dt = RequestHTTP.get_ajaxExecuteQuery("NGT02K009.RV005");
                if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow drkt = dt.NewRow();
                drkt[0] = "0";
                drkt[1] = "--- Tất cả ---";
                dt.Rows.InsertAt(drkt, 0);
                ucTrangThaiKB.setData(dt, 0, 1);
                ucTrangThaiKB.setColumnAll(false);
                ucTrangThaiKB.setColumn(1, true);
                ucTrangThaiKB.SelectIndex = 1;
                ucTrangThaiKB.setEvent(ucTrangThaiKB_Change);

                DataTable dtCB = Func.getTableEmpty(new string[] { "col1", "col2" });

                drkt = dtCB.NewRow();
                drkt["col1"] = "1";
                drkt["col2"] = "Chưa thanh toán";
                dtCB.Rows.Add(drkt);

                drkt = dtCB.NewRow();
                drkt["col1"] = "2";
                drkt["col2"] = "Đã thanh toán";
                dtCB.Rows.Add(drkt);

                drkt = dtCB.NewRow();
                drkt[0] = "0";
                drkt[1] = "--- Tất cả ---";
                dtCB.Rows.InsertAt(drkt, 0);

                ucTrangThaiTT.setData(dtCB, 0, 1);
                ucTrangThaiTT.setColumnAll(false);
                ucTrangThaiTT.setColumn(1, true);
                ucTrangThaiTT.SelectIndex = 0;
                ucTrangThaiTT.setEvent(ucTrangThaiTT_Change);

                //grid
                ucGrid_DsBA.setNumberPerPage(new int[] { 20, 100, 200, 300, 2000 });
                ucGrid_DsBA.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsBA.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsBA.setEvent(getData_table);
                //ucGrid_DsBA.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
                //ucGrid_DsBA.setEvent_FocusedRowChanged(ucGrid_DsBA_Change_SelectRow);
                //ucGrid_DsBA.setEvent_DoubleClick(DoubleClick);
                //ucGrid_DsBA.setEvent_MenuPopupClick(MenuPopupClick);
                //ucGrid_DsBA.ucPage1.setEvent(ucGrid_DsBA_Change_Page);
                ucGrid_DsBA.gridView.Click += GridView_Click;
                ucGrid_DsBA.SetReLoadWhenFilter(false);
                //ucGrid_DsBA.gridView.OptionsBehavior.ReadOnly = false;
                ucGrid_DsBA.gridView.OptionsBehavior.Editable = false;
                

                //ucGrid_DsBA.addMenuPopup(Menu_Popup());

                ucGrid_DsBA.gridView.OptionsCustomization.AllowSort = false;
                ucGrid_DsBA.setMultiSelectMode(true);

                ucGrid_DsBA.onIndicator();

                datTuNgay.Focus();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
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
        private void ucTrangThaiKB_Change(object sender, EventArgs e)
        {
            if (ucTrangThaiKB.SelectValue != "9" && ucTrangThaiKB.SelectValue != "0")
            {
                btnKetThucKham.Enabled = true;
                btnXoaDanhSach.Enabled = true;
            }
            else
            {
                btnKetThucKham.Enabled = false;
                btnXoaDanhSach.Enabled = false;
            }

            getData_table(1, null);
        }
        private void ucTrangThaiTT_Change(object sender, EventArgs e)
        {
            getData_table(1, null);
        }
        private void GridView_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucGrid_DsBA.gridView.FocusedColumn.FieldName))
            {
                int idx = ucGrid_DsBA.gridView.FocusedRowHandle;
                if (ucGrid_DsBA.gridView.GetSelectedRows().Any(o => o == idx))
                {
                    ucGrid_DsBA.gridView.UnselectRow(idx);
                }
                else
                {
                    ucGrid_DsBA.gridView.SelectRow(idx);
                }
            }
        }
        private void getData_table(object sender, EventArgs e)
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
                    //if (ucGrid_DsBA.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DsBA.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsBA.tableFlterColumn);
                    //}

                    ucGrid_DsBA.clearData();

                    string dept_id = Const.local_khoaId.ToString();
                    string trangthai = "";
                    string trangthaitt = "";
                    DataRowView drvTT = ucTrangThaiKB.SelectDataRowView;
                    if (drvTT != null)
                        trangthai = drvTT["col1"].ToString();


                    DataRowView drvTTTT = ucTrangThaiTT.SelectDataRowView;
                    if (drvTTTT != null)
                        trangthaitt = drvTTTT["col1"].ToString();

                    string ngay = datTuNgay.DateTime.ToString(Const.FORMAT_date1);

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("DS.NGT.XOA.KT", page, ucGrid_DsBA.ucPage1.getNumberPerPage(), new string[] { "[0]", "[1]", "[2]", "[3]" }, new string[] { ngay, trangthai, trangthaitt, dept_id }
                    , ucGrid_DsBA.jsonFilter());

                    dt = MyJsonConvert.toDataTable(ds.rows);

                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "DATT", "HOSOBENHANID", "KHAMBENHID", "KQCLS", "MABENHNHAN", "MAHOSOBENHAN", "MA_BHYT", "NGAYSINH", "NGAYTIEPNHAN", "ORG_NAME", "PHONGKHAMDANGKYID", "RN", "SLTHUOC", "TENBENHNHAN", "TENXUTRIKHAMBENH", "TIEPNHANID", "TRANGTHAIKHAMBENH" });

                    ucGrid_DsBA.setData(dt, ds.total, ds.page, ds.records);
                    //ucGrid_DsBA.ucPage1.setData(dt.Rows.Count, ucGrid_DsBA.ucPage1.Current());
                    //loadDSBA(page);

                    ucGrid_DsBA.setColumnAll(false);
                    ucGrid_DsBA.gridView.OptionsView.ColumnAutoWidth = false;
                    ucGrid_DsBA.gridView.BestFitColumns(true);

                    //ucGrid_DsBA.setColumn("RN", 1, "STT", 40);
                    //ucGrid_DsBA.gridView.Columns["RN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("DATT", 2, " ", 30);
                    ucGrid_DsBA.gridView.Columns["DATT"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    ucGrid_DsBA.setColumnImage("DATT", new String[] { "1", "2", "3", "4" }
                       , new String[] { "./Resources/Dollar.png", "./Resources/Dollar.png", "./Resources/Dollar.png", "./Resources/Dollar.png" });

                    ucGrid_DsBA.setColumn("KQCLS", 3, " ", 30);
                    ucGrid_DsBA.gridView.Columns["KQCLS"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    ucGrid_DsBA.setColumnImage("KQCLS", new String[] { "1", "2", "3", "4" }
                       , new String[] { "./Resources/Flag_Red_New.png", "./Resources/True.png", "./Resources/True.png", "./Resources/True.png" });

                    ucGrid_DsBA.setColumn("TRANGTHAIKHAMBENH", 4, " ", 30);
                    ucGrid_DsBA.gridView.Columns["TRANGTHAIKHAMBENH"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    ucGrid_DsBA.setColumnImage("TRANGTHAIKHAMBENH", new String[] { "1", "4", "9" }
                        , new String[] { "./Resources/metacontact_away.png", "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png" });

                    ucGrid_DsBA.setColumn("SLTHUOC", 5, " ", 30);
                    ucGrid_DsBA.gridView.Columns["SLTHUOC"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    ucGrid_DsBA.setColumnImage("SLTHUOC", new String[] { "1" }
                       , new String[] { "./Resources/Icon_NhaThuoc.png" });

                    ucGrid_DsBA.setColumn("MABENHNHAN", 6, "Mã BN");
                    ucGrid_DsBA.gridView.Columns["MABENHNHAN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("MAHOSOBENHAN", 7, "Mã BA");
                    ucGrid_DsBA.gridView.Columns["MAHOSOBENHAN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("TENBENHNHAN", 8, "Tên bệnh nhân");
                    ucGrid_DsBA.setColumn("NGAYSINH", 9, "Ngày sinh");
                    //ucGrid_DsBA.gridView.Columns["NGAYSINH"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("MA_BHYT", 10, "Mã BHYT");

                    ucGrid_DsBA.setColumn("NGAYTIEPNHAN", 11, "Ngày tiếp nhận");
                    ucGrid_DsBA.gridView.Columns["NGAYTIEPNHAN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("ORG_NAME", 12, "Phòng khám");
                    ucGrid_DsBA.setColumn("TENXUTRIKHAMBENH", 13, "Xử trí");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
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

        #region Thêm, Xóa, Cập nhật
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            getData_table(1, null);
        }

        private void btnXoaDanhSach_Click(object sender, EventArgs e)
        {
            try
            {
                int[] idxSelectRows = ucGrid_DsBA.gridView.GetSelectedRows();
                if (idxSelectRows.Length == 0)
                {
                    XtraMessageBox.Show("Hãy chọn bệnh nhân muốn xóa", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    DataRowView drView;
                    for (int i = 0; i < idxSelectRows.Length; i++)
                    {
                        drView = (DataRowView)ucGrid_DsBA.gridView.GetRow(i);
                        if (drView != null)
                        {
                            if (drView["DATT"].ToString() != "0" || drView["KQCLS"].ToString() == "2" || drView["SLTHUOC"].ToString() != "0")
                            {
                                XtraMessageBox.Show("Bệnh nhân " + drView["MABENHNHAN"].ToString() + " đã thanh toán, có đơn thuốc hoặc đã có kết quả CLS không thể xóa", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }

                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn xóa danh sách?", "",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (idxSelectRows.Length > 0)
                    {
                        DataRowView drView;
                        string json = "";
                        bool success = false;
                        string ret = "";
                        string error = "";
                        for (int i = 0; i < idxSelectRows.Length; i++)
                        {
                            drView = (DataRowView)ucGrid_DsBA.gridView.GetRow(i);
                            if (drView != null)
                            {
                                json = "";
                                json += Func.json_item("kieu", "1");
                                json += Func.json_item("khambenhid", drView["KHAMBENHID"].ToString());
                                json += Func.json_item("phongkhamdangkyid", drView["PHONGKHAMDANGKYID"].ToString());
                                json += Func.json_item("tiepnhanid", drView["TIEPNHANID"].ToString());
                                json += Func.json_item("hosobenhanid", drView["HOSOBENHANID"].ToString());
                                json = Func.json_item_end(json);
                                json = json.Replace("\"", "\\\"").Replace("//", "");

                                ret = RequestHTTP.call_ajaxCALL_SP_S_result("CN.XOA.KETTHUC", json);
                                if (ret == "1")
                                {
                                    success = true;
                                }
                                else
                                {
                                    error += drView["TENBENHNHAN"].ToString() + "\n";
                                    success = false;
                                }
                            }
                        }
                        if (success)
                        {
                            getData_table(1, null);
                            XtraMessageBox.Show("Xóa thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show(error + "Xóa không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnKetThucKham_Click(object sender, EventArgs e)
        {
            try
            {
                int[] idxSelectRows = ucGrid_DsBA.gridView.GetSelectedRows();
                if (idxSelectRows.Length <= 0)
                {
                    XtraMessageBox.Show("Hãy chọn bệnh nhân muốn kết thúc khám", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn kết thúc khám danh sách bệnh nhân?", "",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (idxSelectRows.Length > 0)
                    {
                        DataRowView drView;
                        string json = "";
                        bool success = false;
                        string ret = "";
                        string error = "";
                        for (int i = 0; i < idxSelectRows.Length; i++)
                        {
                            drView = (DataRowView)ucGrid_DsBA.gridView.GetRow(i);
                            if (drView != null)
                            {
                                json = "";
                                json += Func.json_item("kieu", "2");
                                json += Func.json_item("khambenhid", drView["KHAMBENHID"].ToString());
                                json += Func.json_item("phongkhamdangkyid", drView["PHONGKHAMDANGKYID"].ToString());
                                json += Func.json_item("tiepnhanid", drView["TIEPNHANID"].ToString());
                                json = Func.json_item_end(json);
                                json = json.Replace("\"", "\\\"").Replace("//", "");

                                ret = RequestHTTP.call_ajaxCALL_SP_S_result("CN.XOA.KETTHUC", json);
                                if (ret == "1")
                                {
                                    success = true;
                                }
                                else
                                {
                                    error += drView["TENBENHNHAN"].ToString() + "\n";
                                    success = false;
                                }
                            }
                        }
                        if (success)
                        {
                            getData_table(1, null);
                            XtraMessageBox.Show("Kết thúc khám thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show(error + "Kết thúc khám không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnXoaXuTri_Click(object sender, EventArgs e)
        {
            try
            {
                int[] idxSelectRows = ucGrid_DsBA.gridView.GetSelectedRows();
                if (idxSelectRows.Length <= 0)
                {
                    XtraMessageBox.Show("Hãy chọn bệnh nhân muốn xóa xử trí", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn xóa xử trí danh sách bệnh nhân đã chọn?", "",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (idxSelectRows.Length > 0)
                    {
                        DataRowView drView;
                        string json = "";
                        bool success = false;
                        string ret = "";
                        string error = "";
                        for (int i = 0; i < idxSelectRows.Length; i++)
                        {
                            drView = (DataRowView)ucGrid_DsBA.gridView.GetRow(i);
                            if (drView != null)
                            {
                                json = drView["KHAMBENHID"].ToString() + "$" + drView["TIEPNHANID"].ToString();
                                ret = RequestHTTP.call_ajaxCALL_SP_I("NGT.XOA.XUTRI", json);
                                if (ret == "1")
                                {
                                    success = true;
                                }
                                else
                                {
                                    error += drView["TENBENHNHAN"].ToString() + "\n";
                                    success = false;
                                }
                            }
                        }
                        if (success)
                        {
                            getData_table(1, null);
                            XtraMessageBox.Show("Xóa xử trí thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            XtraMessageBox.Show(error + "Xóa xử trí không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

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

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}