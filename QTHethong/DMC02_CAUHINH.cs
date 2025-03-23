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
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.QTHethong
{
    public partial class DMC02_CAUHINH : DevExpress.XtraEditors.XtraForm
    {
        #region Variable

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string actionThem = "Them";
        private readonly string actionSua = "Sua";
        private readonly string actionXoa = "Xoa";
        private readonly string actionLuu = "Luu";
        private readonly string actionHuy = "Huy";
        private readonly string actionRong = "Rong";
        
        private string cauHinhId = string.Empty;

        private bool isInsertOrUpdate = false;
        private bool isActionThem = false;
        private bool isEdit = false;

        private DataTable dtCauHinhId = null;

        #endregion

        #region Private

        /// <summary>
        /// Khởi tạo giá trị khi form load
        /// </summary>
        private void InitForm()
        {
            try
            {
                this.InitGrid();
                this.InitComboBox();
                teSearchMaCauHinh.TextChanged += TeSearchMaCauHinh_TextChanged;
                meMoTa.Enter += MeMoTa_Enter;

                this.SetEnabledByActionName(actionRong);
                this.SetData(null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void MeMoTa_Enter(object sender, EventArgs e)
        {
            btnLuu.Focus();
        }

        /// <summary>
        /// Khởi tạo giá trị Grid
        /// </summary>
        private void InitGrid()
        {
            ucGridDanhSachCauHinh.gridView.ViewCaption = "Danh sách cấu hình";
            ucGridDanhSachCauHinh.gridView.OptionsBehavior.Editable = false;
            ucGridDanhSachCauHinh.gridView.OptionsView.ColumnAutoWidth = false;
            ucGridDanhSachCauHinh.gridView.GroupFormat = "[#image]{1} {2}";

            ucGridDanhSachCauHinh.setEvent(PageLoad_ucGridDanhSachCauHinh);
            ucGridDanhSachCauHinh.gridView.Click += GridView_Click;
            //ucGridDanhSachCauHinh.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
            ucGridDanhSachCauHinh.SetReLoadWhenFilter(true);

            ucGridDanhSachCauHinh.setNumberPerPage(new int[] { 100, 200, 300 });
            ucGridDanhSachCauHinh.onIndicator();
        }

        /// <summary>
        /// Khởi tạo giá trị cho SearchLookup
        /// </summary>
        private void InitComboBox()
        {
            try
            {
                // load danh sách cấu hình
                dtCauHinhId = RequestHTTP.get_ajaxExecuteQuery("DMC02.04");

                if (dtCauHinhId == null || dtCauHinhId.Rows.Count <= 0)
                {
                    dtCauHinhId = Func.getTableEmpty(new string[] { "col1", "col2" });
                }

                DataTable dt = dtCauHinhId.Copy();

                SetDataSourceToCauHinhId(dt);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Set datasource cho SearchLookup CauHinhId
        /// </summary>
        /// <param name="dt"></param>
        private void SetDataSourceToCauHinhId(DataTable dt)
        {
            ucCboCauHinhID.setData(dt, "col1", "col2");
            ucCboCauHinhID.setColumn(0, false);
            ucCboCauHinhID.setEvent(ucCboCauHinhID_EditValueChanged);

            ucCboCauHinhID.SelectValue = dt.Rows[0]["col1"].ToString();
        }

        /// <summary>
        /// Load danh sách cấu hình
        /// </summary>
        /// <param name="page"></param>
        private void LoadDataGrid(int? page)
        {
            try
            {
                int pageNum = 0;
                if (page == null)
                {
                    pageNum = ucGridDanhSachCauHinh.ucPage1.Current();
                }
                else
                {
                    pageNum = page.GetValueOrDefault();
                }

                if (pageNum <= 0)
                {
                    pageNum = 1;
                }

                ResponsList responses = new ResponsList();
                //string jsonFilter = string.Empty;

                //if (ucGridDanhSachCauHinh.ReLoadWhenFilter
                //    && ucGridDanhSachCauHinh.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridDanhSachCauHinh.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "DMC02.01"
                    , pageNum
                    , ucGridDanhSachCauHinh.ucPage1.getNumberPerPage()
                    , new String[] {  }
                    , new string[] {  }
                    , ucGridDanhSachCauHinh.jsonFilter());

                ucGridDanhSachCauHinh.clearData();

                DataTable dtDSPK = new DataTable();
                dtDSPK = MyJsonConvert.toDataTable(responses.rows);
                if (dtDSPK == null || dtDSPK.Rows.Count <= 0)
                {
                    dtDSPK = Func.getTableEmpty(new String[] { "NHOMID", "TENNHOMCAUHINH", "CSYT_ID", "CAUHINHID", "MACAUHINH", "GIATRI_THIETLAP", "TENCAUHINH", "MOTA" });
                }

                ucGridDanhSachCauHinh.setData(dtDSPK, responses.total, responses.page, responses.records);

                ucGridDanhSachCauHinh.setColumnAll(false);
                ucGridDanhSachCauHinh.setColumnMemoEdit("MACAUHINH", 0, "Mã cấu hình", 200);
                ucGridDanhSachCauHinh.setColumnMemoEdit("GIATRI_THIETLAP", 1, "Giá trị TL", 100);
                ucGridDanhSachCauHinh.setColumnMemoEdit("TENCAUHINH", 2, "Tên cấu hình", 300);

                ucGridDanhSachCauHinh.gridView.Columns["TENNHOMCAUHINH"].GroupIndex = 0;
                ucGridDanhSachCauHinh.gridView.ExpandAllGroups();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Set giá trị khi chọn 1 dòng trên danh sách cấu hình
        /// </summary>
        /// <param name="drCauHinh"></param>
        private void SetData(DataRowView drCauHinh)
        {
            try
            {
                // nếu drCauHinh = null, clear giá trị của item, ngược lại thì set giá trị cho item
                if (drCauHinh == null)
                {
                    this.cauHinhId = string.Empty;
                    teSearchMaCauHinh.Text = string.Empty;
                    ucCboCauHinhID.SelectValue = string.Empty;
                    teTenCauHinh.Text = string.Empty;
                    ucCboCauHinhID.SelectText = " ";
                    teGiaTri.Text = string.Empty;
                    meMoTa.Text = string.Empty;
                    teSearchMaCauHinh.Focus();
                }
                else if (!isInsertOrUpdate)
                {
                    SetDataSourceToCauHinhId(dtCauHinhId);

                    this.cauHinhId = drCauHinh["CAUHINHID"].ToString();
                    ucCboCauHinhID.SelectValue = this.cauHinhId;
                    teGiaTri.Text = drCauHinh["GIATRI_THIETLAP"].ToString();

                    SetEnabledByActionName(string.Empty);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Them()
        {
            SetData(null);
            SetEnabledByActionName(actionThem);
            isInsertOrUpdate = true;
            isActionThem = true;
            isEdit = false;
        }

        private void Sua()
        {
            SetEnabledByActionName(actionSua);
            teSearchMaCauHinh.Properties.ReadOnly = true;
            ucCboCauHinhID.lookUpEdit.Properties.ReadOnly = true;
            isInsertOrUpdate = true;
            isActionThem = false;
            isEdit = true;
            teGiaTri.Focus();
        }
        
        private void Luu()
        {
            try
            {
                isActionThem = false;
                string giaTri = teGiaTri.Text.Trim();
                string moTa = meMoTa.Text.Trim().Replace("\r\n", "\n");
                string tenCauHinh = teTenCauHinh.Text.Trim();
                string maCauHinh = ucCboCauHinhID.Text;
                if (string.IsNullOrWhiteSpace(maCauHinh))
                {
                    MessageBox.Show("Mã cấu hình không được để trống");
                    ucCboCauHinhID.Focus();
                    return;
                }

                isInsertOrUpdate = false;
                var data = new
                {
                    CAUHINHID = this.cauHinhId,
                    GIATRI_THIETLAP = giaTri,
                    MOTA = "",
                    TENCAUHINH = "",
                };

                var dataJson = JsonConvert.SerializeObject(data).Replace("\"", "\\\"");
                string fl = null;
                if (isEdit)
                {
                    fl = RequestHTTP.call_ajaxCALL_SP_I("DMC02.03", dataJson);
                }
                else
                {
                    fl = RequestHTTP.call_ajaxCALL_SP_I("DMC02.06", dataJson);
                }

                if ("1".Equals(fl))
                {
                    MessageBox.Show("Thêm mới thành công !");
                    LoadDataGrid(null);
                }
                else if ("2".Equals(fl))
                {
                    MessageBox.Show("Cập nhật thành công !");
                    LoadDataGrid(null);
                }
                else if ("0".Equals(fl))
                {
                    MessageBox.Show("Mã cấu hình đã có !");
                    LoadDataGrid(null);
                }
                else if ("3".Equals(fl))
                {
                    MessageBox.Show("Mã cấu hình không tồn tại trong dữ liệu !");
                    LoadDataGrid(null);
                }
                else
                {
                    MessageBox.Show("Không thành công !");
                }

                SetEnabledByActionName(isActionThem ? actionRong : actionHuy);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Huy()
        {
            SetEnabledByActionName(isActionThem ? actionRong : actionHuy);
            isInsertOrUpdate = false;
            isActionThem = false;
        }

        private bool ReadOnly
        {
            set
            {
                teSearchMaCauHinh.ReadOnly = value;
                ucCboCauHinhID.lookUpEdit.ReadOnly = value;
                teGiaTri.ReadOnly = value;
            }
        }

        private void SetEnabledByActionName(string loai)
        {
            if (loai.Equals(actionThem)
                || loai.Equals(actionSua))
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;

                this.ReadOnly = false;
            }
            else if (loai.Equals(actionLuu)
                || loai.Equals(actionXoa))
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;

                this.ReadOnly = true;
            }
            else if (loai.Equals(actionRong))
            {
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;

                this.ReadOnly = true;
            }
            else
            {
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;

                this.ReadOnly = true;
            }
        }

        #endregion

        #region Public

        public DMC02_CAUHINH()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Events

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private void DMC02_CAUHINH_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            this.Them();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            this.Sua();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.Luu();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Huy();
        }

        private void PageLoad_ucGridDanhSachCauHinh(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadDataGrid(pageNum);
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView drCauHinh = (DataRowView)ucGridDanhSachCauHinh.gridView.GetFocusedRow();
                if (drCauHinh != null)
                {
                    this.SetData(drCauHinh);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        //private void GridView_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridView view = sender as GridView;
        //        if (view.ActiveEditor is TextEdit)
        //        {
        //            TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //            textEdit.Text = textEdit.Text.Trim();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }
        //}

        private void ucCboCauHinhID_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null && !string.IsNullOrEmpty(ucCboCauHinhID.SelectValue))
                {
                    DataRowView drCauHinh = (DataRowView)sender;
                    var cauHinhId = drCauHinh["col1"].ToString();
                    if (!string.IsNullOrEmpty(cauHinhId))
                    {
                        var data_ar = RequestHTTP.call_ajaxCALL_SP_O("DMC02.05", cauHinhId, 0);
                        if (data_ar != null && data_ar.Rows.Count > 0)
                        {
                            this.cauHinhId = cauHinhId;
                            teTenCauHinh.Text = data_ar.Rows[0]["TENCAUHINH"].ToString();
                            meMoTa.Text = data_ar.Rows[0]["MOTA"].ToString().Replace("\n", "\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void TeSearchMaCauHinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var sMaCauHinh = teSearchMaCauHinh.Text.ToLower().Trim();
                if (string.IsNullOrWhiteSpace(sMaCauHinh))
                {
                    SetDataSourceToCauHinhId(dtCauHinhId);
                    return;
                }

                var rows = dtCauHinhId.AsEnumerable().Where(r => r.Field<string>("col2").ToLower().Contains(sMaCauHinh));
                if (rows.Any())
                {
                    var dt = rows.CopyToDataTable();
                    SetDataSourceToCauHinhId(dt);
                }
                else
                {
                    var dt = Func.getTableEmpty(new string[] { "col1", "col2" });

                    SetDataSourceToCauHinhId(dt);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
         
        #endregion

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}