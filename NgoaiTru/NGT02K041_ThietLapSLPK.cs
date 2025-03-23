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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K041_ThietLapSLPK : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ResponsList ds = new ResponsList();
        DataTable dt = new DataTable();
        #endregion
        
        #region Init Form
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K041_ThietLapSLPK()
        {
            InitializeComponent();
        }
        private void NGT02K041_ThietLapSLPK_Load(object sender, EventArgs e)
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
                //grid
                ucGrid_DsPhong.Set_HidePage(false);
                //ucGrid_DsPhong.setNumberPerPage(new int[] { 500, 800, 1000 });
                ucGrid_DsPhong.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsPhong.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsPhong.setEvent(getData_table);
                //ucGrid_DsPhong.setEvent_FocusedRowChanged(DsBN_Change_SelectRow);
                //ucGrid_DsPhong.setEvent_DoubleClick(DoubleClick);
                //ucGrid_DsPhong.setEvent_MenuPopupClick(MenuPopupClick);
                //ucGrid_DsPhong.SetReLoadWhenFilter(true);
                ucGrid_DsPhong.gridView.CellValueChanged += GridView_CellValueChanged;
                ucGrid_DsPhong.gridView.OptionsBehavior.ReadOnly = false;
                ucGrid_DsPhong.gridView.OptionsBehavior.Editable = true;
                //ucGrid_DsPhong.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;

                ucGrid_DsPhong.gridView.OptionsView.ShowViewCaption = true;
                //ucGrid_DsPhong.addMenuPopup(Menu_Popup());

                ucGrid_DsPhong.gridView.OptionsCustomization.AllowSort = false;

                ucGrid_DsPhong.onIndicator();

                btnLuu.Focus();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        #endregion

        #region Load dữ liệu
        private void loadPhong()
        {
            ds = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K041.DSSLPK", 1, 500, new string[] { }, new string[] { }, "");
            dt = MyJsonConvert.toDataTable(ds.rows);
            if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "IDKHOA", "IDPHONG", "MAKHOA", "MAPHONG", "RN", "SLBH", "SLDV", "SLKHAM", "SLKHAMCHIEU", "SLKHAMCHIEUTT", "SLKHAMSANG", "SLKHAMSANGTT", "SLKHAMXONG", "SLVP", "TENKHOA", "TENPHONG" });

            dt.Columns.Add("Action", typeof(string));

            ucGrid_DsPhong.setData(dt, ds.total, ds.page, ds.records);
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
                    //if (ucGrid_DsPhong.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DsPhong.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsPhong.tableFlterColumn);
                    //}


                    ucGrid_DsPhong.clearData();
                    
                    loadPhong();

                    ucGrid_DsPhong.setColumnAll(false);
                    ucGrid_DsPhong.gridView.OptionsView.ColumnAutoWidth = false;
                    ucGrid_DsPhong.gridView.BestFitColumns(true);

                    //ucGrid_DsPhong.setColumn("RN", 0, "STT", 50);

                    GridColumn col;
                    RepositoryItemSpinEdit edit;


                    col = new GridColumn();
                    col.Caption = "Tên Phòng";
                    col.FieldName = "TENPHONG";
                    col.VisibleIndex = 1;
                    col.Width = 400;
                    col.OptionsColumn.AllowEdit = false;
                    ucGrid_DsPhong.gridView.Columns.Add(col);


                    col = new GridColumn();
                    col.Caption = "SÁNG";
                    col.FieldName = "SLKHAMSANG";
                    col.VisibleIndex = 2;
                    col.Width = 100;
                    col.OptionsColumn.AllowEdit = true;
                    //col.DisplayFormat.FormatString = "###.###.###";
                    edit = new RepositoryItemSpinEdit();
                    //edit.EditFormat.FormatString = "###.###.###";
                    edit.EditMask = "d";
                    //edit.MaxValue = 999;
                    //edit.MinValue = 0;
                    //edit.NullText = "0";
                    //edit.NullValuePrompt = "Yêu cầu nhập số lượng khám sáng";
                    edit.Validating += SLKHAMSANG_Validating;
                    col.ColumnEdit = edit;
                    ucGrid_DsPhong.gridView.Columns.Add(col);


                    col = new GridColumn();
                    col.Caption = "CHIỀU";
                    col.FieldName = "SLKHAMCHIEU";
                    col.VisibleIndex = 3;
                    col.Width = 100;
                    col.OptionsColumn.AllowEdit = true;
                    //col.DisplayFormat.FormatString = "###.###.###";
                    edit = new RepositoryItemSpinEdit();
                    //edit.EditFormat.FormatString = "###.###.###";
                    edit.EditMask = "d";
                    //edit.MaxValue = 999;
                    //edit.MinValue = 0;
                    //edit.NullText = "0";
                    //edit.NullValuePrompt = "Yêu cầu nhập số lượng khám chiều";
                    edit.Validating += SLKHAMCHIEU_Validating;
                    
                    col.ColumnEdit = edit;
                    ucGrid_DsPhong.gridView.Columns.Add(col);
                    

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
        private void SLKHAMSANG_Validating(object sender, CancelEventArgs e)
        {
            SpinEdit edit = sender as SpinEdit;
            if (edit.Text != "")
            {
                var num = edit.Value;
                if (num < 0 || num > 999)
                {
                    XtraMessageBox.Show("Số lượng sáng không được nhỏ hơn 0 và vượt quá 999", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
            else
            {
                XtraMessageBox.Show("Yêu cầu nhập số lượng khám sáng", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }

        private void SLKHAMCHIEU_Validating(object sender, CancelEventArgs e)
        {
            SpinEdit edit = sender as SpinEdit;
            if (edit.Text != "")
            {
                var num = edit.Value;
                if (num < 0 || num > 999)
                {
                    XtraMessageBox.Show("Số lượng chiều không được nhỏ hơn 0 và vượt quá 999", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }
            }
            else
            {
                XtraMessageBox.Show("Yêu cầu nhập số lượng khám chiều", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
        private void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DataRow row = (ucGrid_DsPhong.gridView.GetRow(ucGrid_DsPhong.gridView.FocusedRowHandle) as DataRowView).Row;
            if (row == null) return;
            row["Action"] = "Update";
        }
        #endregion
        
        #region Thêm, Xóa, Cập nhật
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Action"].ToString() == "Update")
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    XtraMessageBox.Show("Không có phòng khám nào được chọn/ sửa", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = XtraMessageBox.Show(this, "Có "+ count +" phòng khám được sửa đổi. Bạn có muốn lưu lại?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string json_in = getJson();

                    string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K041.CNSLPK", json_in);
                    if (ret == "1")
                    {
                        XtraMessageBox.Show("Lưu thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadPhong();
                    }
                    else
                        XtraMessageBox.Show("Lưu không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        private string getJson()
        {
            string json_in = "";
            string json_id = "";
            string json_id2 = "";
            string json_data = "";
            string json_data2 = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Action"].ToString() == "Update")
                {
                    json_id2 = Func.json_item("IDPHONG", dt.Rows[i]["IDPHONG"].ToString());
                    json_id += "{" + json_id2.Substring(0, json_id2.Length - 1) + "},";

                    json_data2 = Func.json_item("SLKHAMSANG", dt.Rows[i]["SLKHAMSANG"].ToString());
                    json_data2 += Func.json_item("SLKHAMCHIEU", dt.Rows[i]["SLKHAMCHIEU"].ToString());
                    json_data += "{" + json_data2.Substring(0, json_data2.Length - 1) + "},";
                }
            }
            
            json_in = "[" + json_id.Substring(0, json_id.Length - 1).Replace("\"", "\\\"") + "]" 
                + "$" 
                + "[" + json_data.Substring(0, json_data.Length - 1).Replace("\"", "\\\"") + "]";
           
            return json_in;
        }
        #endregion

    }
}