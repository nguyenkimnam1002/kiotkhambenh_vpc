using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NTU02D077_DanhSachMienGiam : DevExpress.XtraEditors.XtraForm
    {
        #region Variable

        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string dichVuKhamBenhId = "";

        #endregion

        public NTU02D077_DanhSachMienGiam()
        {
            InitializeComponent();
        }
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        } 
        
        public void setData(string dichVuKhamBenhId)
        {
            this.dichVuKhamBenhId = dichVuKhamBenhId; 

        }

        /// <summary>
        /// Khởi tạo giá trị ban đầu
        /// </summary>
        private void InitForm()
        {
            try
            {
                ucGridDanhSachMienGiam.gridView.OptionsBehavior.Editable = false;
                ucGridDanhSachMienGiam.gridView.OptionsView.ShowViewCaption = false;
                ucGridDanhSachMienGiam.setMultiSelectMode(true);

                ucGridDanhSachMienGiam.setEvent(PageLoad_ucGridDanhSachMienGiam);
                ucGridDanhSachMienGiam.SetReLoadWhenFilter(true);
                //ucGridDanhSachMienGiam.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
                ucGridDanhSachMienGiam.gridView.Click += GridView_Click;

                ucGridDanhSachMienGiam.setNumberPerPage(new int[] { 200, 250, 300 });
                ucGridDanhSachMienGiam.onIndicator();
                
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadDataGrid(int? page)
        {
            try
            {
                if (page == null)
                {
                    page = ucGridDanhSachMienGiam.ucPage1.Current();
                }

                if (page <= 0)
                {
                    page = 1;
                }
                
                ResponsList responses = new ResponsList();

                //string jsonFilter = string.Empty;
                //if (ucGridDanhSachMienGiam.ReLoadWhenFilter
                //    && ucGridDanhSachMienGiam.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridDanhSachMienGiam.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NTU02D077.EV001"
                    , page.GetValueOrDefault()
                    , ucGridDanhSachMienGiam.ucPage1.getNumberPerPage()
                    , new String[] { "[0]" }
                    , new string[] { this.dichVuKhamBenhId }
                    , ucGridDanhSachMienGiam.jsonFilter());

                ucGridDanhSachMienGiam.clearData();

                DataTable dtDSPK = new DataTable();
                dtDSPK = MyJsonConvert.toDataTable(responses.rows);
                if (dtDSPK.Rows.Count <= 0)
                {
                    dtDSPK = Func.getTableEmpty(new String[] {
                        "TENDICHVU",
                        "SOLUONG",
                        "TIEN_CHITRA",
                        "THANHTIEN",
                        "DICHVUKHAMBENHID",
                        "KHAMBENHID",
                        "BENHNHANID",
                        "HOSOBENHANID",
                        "TIEPNHANID"
                    });
                }

                ucGridDanhSachMienGiam.setData(dtDSPK, responses.total, responses.page, responses.records);

                ucGridDanhSachMienGiam.setColumnAll(false);
                ucGridDanhSachMienGiam.setColumn("TENDICHVU", 1, "Tên dịch vụ", 0);
                ucGridDanhSachMienGiam.setColumn("SOLUONG", 2, "Số lượng", 0);
                ucGridDanhSachMienGiam.setColumn("TIEN_CHITRA", 3, "Đơn giá", 0);
                ucGridDanhSachMienGiam.setColumn("THANHTIEN", 4, "Thành tiền", 0);

                ucGridDanhSachMienGiam.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Xoa()
        {
            List<string> listDichVuKhamBenh = new List<string>();
            DataRowView drView;

            int[] idxSelectRows = ucGridDanhSachMienGiam.gridView.GetSelectedRows();
            if (idxSelectRows.Length <= 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi để xóa");
                return;
            }

            for (int i = 0; i < idxSelectRows.Length; i++)
            {
                drView = (DataRowView)ucGridDanhSachMienGiam.gridView.GetRow(idxSelectRows[i]);
                if (drView != null && drView["DICHVUKHAMBENHID"] != null)
                {
                    listDichVuKhamBenh.Add(drView["DICHVUKHAMBENHID"].ToString());
                }
            }

            var _par = new List<object>()
            {
                this.dichVuKhamBenhId,
                string.Join(",", listDichVuKhamBenh),
            };

            var result = RequestHTTP.call_ajaxCALL_SP_I("NTU02D077.EV002", string.Join("$", _par));
            if ("2".Equals(result))
            {
                MessageBox.Show("Dịch vụ khám bệnh đã thu tiền, không thể xóa miễn giảm.");
            }
            else if ("3".Equals(result))
            {
                MessageBox.Show("Bệnh nhân đã kết thúc bệnh án. Không thể kê miễn giảm.");
            }
            else if ("1".Equals(result))
            {
                MessageBox.Show("Xóa dịch vụ miễn giảm thành công.");
                LoadDataGrid(null);
            }
            else
            {
                MessageBox.Show("Xóa dịch vụ miễn giảm không thành công.");
            }
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

        private void GridView_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucGridDanhSachMienGiam.gridView.FocusedColumn.FieldName))
            {
                int idx = ucGridDanhSachMienGiam.gridView.FocusedRowHandle;
                if (ucGridDanhSachMienGiam.gridView.GetSelectedRows().Any(o => o == idx))
                {
                    ucGridDanhSachMienGiam.gridView.UnselectRow(idx);
                }
                else
                {
                    ucGridDanhSachMienGiam.gridView.SelectRow(idx);
                }
            }
        }

        private void PageLoad_ucGridDanhSachMienGiam(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadDataGrid(pageNum);
        }

        private void NTU02D077_DanhSachMienGiam_Load(object sender, EventArgs e)
        {
            InitForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.Xoa();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}