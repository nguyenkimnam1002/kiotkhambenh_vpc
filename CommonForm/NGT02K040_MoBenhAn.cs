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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K040_MoBenhAn : DevExpress.XtraEditors.XtraForm
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

        public NGT02K040_MoBenhAn()
        {
            InitializeComponent();
        }

        private void NGT02K040_MoBenhAn_Load(object sender, EventArgs e)
        {
            Init_Form();
        }
        private void Init_Form()
        {
            init_Control();
            getData_table(1, null);
        }

        private void init_Control()
        {
            try
            {

                //Combo Khoa phòng
                // khoa, userid, hospitalid;
                DataTable dt = RequestHTTP.get_dbCALL_SP_R("DEPT.P01", "4" + "$" + Const.local_user.USER_ID.ToString() + "$" + Const.local_user.HOSPITAL_ID);
                if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow drkt = dt.NewRow();
                drkt[0] = "-1";
                drkt[1] = "--- Tất cả ---";
                dt.Rows.InsertAt(drkt, 0);
                cboKhoaPhong.setData(dt, 0, 1);
                cboKhoaPhong.setColumnAll(false);
                cboKhoaPhong.setColumn(1, true);
                cboKhoaPhong.SelectIndex = 0;
                cboKhoaPhong.setEvent(ucKhoaPhong_Change);

                cboKhoaPhong.Focus();

                //grid

                ucGrid_DsBA.setNumberPerPage(new int[] { 500, 800, 1000 });

                ucGrid_DsBA.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsBA.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsBA.setEvent(getData_table);
                ucGrid_DsBA.setEvent_FocusedRowChanged(ucGrid_DsBA_Change_SelectRow);
                //ucGrid_DsBA.setEvent_DoubleClick(DoubleClick);
                //ucGrid_DsBA.setEvent_MenuPopupClick(MenuPopupClick);
                ucGrid_DsBA.SetReLoadWhenFilter(true);
                //ucGrid_DsBA.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
                //ucGrid_DsBA.gridView.OptionsBehavior.ReadOnly = false;
                // ucGrid_DsBA.gridView.OptionsBehavior.Editable = false;

                ucGrid_DsBA.gridView.OptionsView.ShowViewCaption = true;
                //ucGrid_DsBA.gridView.OptionsView.ColumnAutoWidth = true;

                //ucGrid_DsBA.addMenuPopup(Menu_Popup());

                ucGrid_DsBA.gridView.OptionsCustomization.AllowSort = false;
                ucGrid_DsBA.setNumberPerPage(new int[] { 500, 800, 1000 });
                ucGrid_DsBA.onIndicator();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion

        #region Load dữ liệu
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

                    //Lấy Khoa Phòng từ combobox
                    string khoa_id = "-1";
                    string subdept_id = Const.local_phongId.ToString();
                    DataRowView drvKhoa = cboKhoaPhong.SelectDataRowView;
                    if (drvKhoa != null)
                        khoa_id = drvKhoa["col1"].ToString();

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K040.DSBENHAN", page, ucGrid_DsBA.ucPage1.getNumberPerPage(), new string[] { "[0]", "[1]" }, new string[] { khoa_id, subdept_id }
                    , ucGrid_DsBA.jsonFilter());
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    
                    if (dt == null || dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "RN", "MABENHNHAN", "MAHOSOBENHAN", "TENBENHNHAN", "GIOITINH", "NGAYSINH", "NGAYYEUCAU"
                            , "NGAYTHUCHIEN", "PHONG", "NOIDUNG", "NGUOIYC", "NGUOIMO" });

                    ucGrid_DsBA.setData(dt, ds.total, ds.page, ds.records);

                    //ucGrid_DsBA.ucPage1.setData(dt.Rows.Count, ucGrid_DsBA.ucPage1.Current());
                    //loadDSBA(page);

                    ucGrid_DsBA.setColumnAll(false);

                    //ucGrid_DsBA.setColumn("RN", 0, "STT");

                    ucGrid_DsBA.setColumn("MABENHNHAN", 2, "Mã BN");
                    ucGrid_DsBA.gridView.Columns["MABENHNHAN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("MAHOSOBENHAN", 3, "Mã BA");
                    ucGrid_DsBA.gridView.Columns["MAHOSOBENHAN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("TENBENHNHAN", 4, "Họ và tên");
                    ucGrid_DsBA.setColumn("GIOITINH", 5, "Giới tính");
                    ucGrid_DsBA.setColumn("NGAYSINH", 6, "Ngày sinh");
                    ucGrid_DsBA.gridView.Columns["NGAYSINH"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("NGAYYEUCAU", 7, "Ngày YC");
                    ucGrid_DsBA.gridView.Columns["NGAYYEUCAU"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("NGAYTHUCHIEN", 8, "Ngày TH");
                    ucGrid_DsBA.gridView.Columns["NGAYTHUCHIEN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGrid_DsBA.setColumn("PHONG", 9, "Phòng");
                    ucGrid_DsBA.setColumn("NOIDUNG", 10, "Nội dung");
                    ucGrid_DsBA.setColumn("NGUOIYC", 11, "Người YC");
                    ucGrid_DsBA.setColumn("NGUOIMO", 12, "Người mở");

                    //ucGrid_DsBA.gridView.BestFitColumns(true);

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

        #region Change, Keydown, Validate

        //private void GridView_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view.ActiveEditor is TextEdit)
        //    {
        //        TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //        textEdit.Text = textEdit.Text.Trim();
        //    }
        //}

        private void ucGrid_DsBA_Change_SelectRow(object sender, EventArgs e)
        {
            DataRow row = (ucGrid_DsBA.gridView.GetRow(ucGrid_DsBA.gridView.FocusedRowHandle) as DataRowView).Row;
            if (row == null) return;
            dr = row;
            lblTenBN.Text = "Đã chọn bệnh nhân: " + dr["TENBENHNHAN"].ToString();
        }

        private void ucKhoaPhong_Change(object sender, EventArgs e)
        {
            //if (cboKhoaPhong.SelectValue == "-1") return;
            getData_table(1, null);
        }
        #endregion

        #region Thêm, Xóa, Cập nhật
        private void btnMoBA_Click(object sender, EventArgs e)
        {
            try
            {
                if (dr == null) {
                    MessageBox.Show("Bạn chưa chọn bệnh nhân để mở lại bệnh án.");
                    return;
                }

                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn mở lại bệnh án cho bệnh nhân được chọn?", "",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string json = "";
                    json += Func.json_item("benhnhanid", dr["BENHNHANID"].ToString());
                    json += Func.json_item("khambenhid", dr["KHAMBENHID"].ToString());
                    json += Func.json_item("khoaid", dr["KHOAID"].ToString());
                    json += Func.json_item_num("kieu", 1);
                    json += Func.json_item("noidung", dr["NOIDUNG"].ToString());
                    json += Func.json_item("phongid", dr["PHONGID"].ToString());
                    json += Func.json_item("tiepnhanid", dr["TIEPNHANID"].ToString());
                    json += Func.json_item("yeucaumobenhanid", dr["YEUCAUMOBENHANID"].ToString());
                    json = Func.json_item_end(json);
                    json = json.Replace("\"", "\\\"").Replace("//","");

                    string ret = RequestHTTP.call_ajaxCALL_SP_S_result("NGT02K001.MOBA", json);
                    if (ret == "-100")
                    {
                        XtraMessageBox.Show("Bệnh nhân chưa có yêu cầu mở, hãy gửi yêu cầu mở trước khi mở bệnh án", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (ret == "-200")
                    {
                        XtraMessageBox.Show("Bệnh nhân đã gửi yêu cầu mở nhưng chưa mở bệnh án, hãy kiểm tra lại", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (ret == "-500")
                    {
                        XtraMessageBox.Show("Bệnh nhân mở bệnh án thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (ret == "-600")
                    {
                        XtraMessageBox.Show("Yêu cầu đã được mở, hãy gửi yêu cầu mở khác", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (ret == "-700")
                    {
                        XtraMessageBox.Show("Bệnh nhân đã kết thúc điều trị nội/ngoại trú không mở được bệnh án", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (ret == "-400")
                    {
                        XtraMessageBox.Show("Đã duyệt kế toán/bảo hiểm ko thể mở lại bệnh án", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        getData_table(1, null);
                        lblTenBN.Text = "Chưa chọn bệnh nhân";
                        XtraMessageBox.Show("Bệnh nhân mở bệnh án thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
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