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

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K061_THIETLAP_BS_PK : DevExpress.XtraEditors.XtraForm
    {
        #region Variable

        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #endregion

        #region Private

        /// <summary>
        /// Khởi tạo giá trị ban đầu
        /// </summary>
        private void InitForm ()
        {
            try
            {
                ucGridDanhSachPhongKham.gridView.OptionsBehavior.Editable = false;
                ucGridDanhSachPhongKham.setMultiSelectMode(true);
                
                ucGridDanhSachPhongKham.setEvent(PageLoad_ucGridDanhSachPhongKham);
                ucGridDanhSachPhongKham.SetReLoadWhenFilter(true);
                ucGridDanhSachPhongKham.gridView.Click += GridView_Click;
                //ucGridDanhSachPhongKham.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;

                ucGridDanhSachPhongKham.setNumberPerPage(new int[] { 20, 100, 200, 300, 2000 });
                ucGridDanhSachPhongKham.onIndicator();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadDataGrid(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                string userId = Const.local_user.USER_ID;
                //string jsonFilter = string.Empty;
                ResponsList responses = new ResponsList();

                //if (ucGridDanhSachPhongKham.ReLoadWhenFilter
                //    && ucGridDanhSachPhongKham.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridDanhSachPhongKham.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NGT02K061.DSPK"
                    , page
                    , ucGridDanhSachPhongKham.ucPage1.getNumberPerPage()
                    , new String[] { "[0]" }
                    , new string[] { userId }
                    , ucGridDanhSachPhongKham.jsonFilter());

                ucGridDanhSachPhongKham.clearData();

                DataTable dtDSPK = new DataTable();
                dtDSPK = MyJsonConvert.toDataTable(responses.rows);
                if (dtDSPK.Rows.Count <= 0)
                {
                    dtDSPK = Func.getTableEmpty(new String[] { "ORG_NAME" });
                }

                ucGridDanhSachPhongKham.setData(dtDSPK, responses.total, responses.page, responses.records);

                ucGridDanhSachPhongKham.setColumnAll(false);
                ucGridDanhSachPhongKham.setColumn("ORG_NAME", 0, "Phòng", 0);

                ucGridDanhSachPhongKham.gridView.BestFitColumns(true);
                //GridView_ColumnFilterChanged(ucGridDanhSachPhongKham.gridView, null);

                for (int i = 0; i < dtDSPK.Rows.Count; i++)
                {
                    if (dtDSPK.Rows[i]["CHECKED"] != null && "1".Equals(dtDSPK.Rows[i]["CHECKED"].ToString()))
                    {
                        ucGridDanhSachPhongKham.gridView.SelectRow(i);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Lưu thiết lập phòng khám
        /// </summary>
        private void Luu ()
        {
            try
            {
                // lấy tất cả index dòng đã được chọn
                int[] idxSelectRows = ucGridDanhSachPhongKham.gridView.GetSelectedRows();
                if (idxSelectRows.Length <= 0)
                {
                    MessageBox.Show(Const.mess_thietlap_bs_pk_chonphongkhamthietlap);
                    return;
                }

                string userId = Const.local_user.USER_ID;
                var listPhongID = new List<object>(); 
                DataRowView drView;
                for (int i = 0; i < idxSelectRows.Length; i++)
                {
                    drView = (DataRowView)ucGridDanhSachPhongKham.gridView.GetRow(idxSelectRows[i]);
                    if (drView != null && drView["PHONGID"] != null)
                    {
                        listPhongID.Add(new {
                            PHONGID = drView["PHONGID"].ToString(),
                        });
                    }
                }

                var dataDSPKJson = JsonConvert.SerializeObject(listPhongID).Replace("\"", "\\\"");
                var dataRequest = new string[2];
                dataRequest[0] = userId;
                dataRequest[1] = dataDSPKJson;

                DialogResult dialogResult = MessageBox.Show(
                    Const.mess_thietlap_bs_pk_xacnhantruockhicapnhat,
                    "",
                    MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.OK)
                {
                    var rs = RequestHTTP.call_ajaxCALL_SP_I("NGT02K061.CN", string.Join("$", dataRequest));
                    int fl = 0;
                    int.TryParse(rs, out fl);
                    if (fl > 0)
                    {
                        MessageBox.Show(Const.mess_thietlap_bs_pk_capnhatthanhcong);
                    }
                    else
                    {
                        MessageBox.Show(Const.mess_thietlap_bs_pk_capnhatthatbai);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Thoát khỏi màn hình
        /// </summary>
        private void Thoat ()
        {
            this.Close();
        }

        #endregion

        #region public

        public NGT02K061_THIETLAP_BS_PK()
        {
            InitializeComponent();
        }
        
        #endregion

        #region Events

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        //private void GridView_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    string filter = view.ActiveFilterString;
        //    if (view.ActiveEditor is TextEdit)
        //    {
        //        TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //        var text = textEdit.Text.Trim();
        //        textEdit.Text = " ";
        //        textEdit.Text = text;
        //    }
        //}

        private void PageLoad_ucGridDanhSachPhongKham(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadDataGrid(pageNum);
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucGridDanhSachPhongKham.gridView.FocusedColumn.FieldName))
            {
                int idx = ucGridDanhSachPhongKham.gridView.FocusedRowHandle;
                if (ucGridDanhSachPhongKham.gridView.GetSelectedRows().Any(o => o == idx))
                {
                    ucGridDanhSachPhongKham.gridView.UnselectRow(idx);
                }
                else
                {
                    ucGridDanhSachPhongKham.gridView.SelectRow(idx);
                }
            }
        }
        
        private void NGT02K061_THIETLAP_BS_PK_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.Luu();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Thoat();
        }

        #endregion
    }
}