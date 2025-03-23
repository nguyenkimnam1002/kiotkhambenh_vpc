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
using Newtonsoft.Json;

namespace VNPT.HIS.QTHethong
{
    public partial class DMC54_PHONGHIENTHI : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public DMC54_PHONGHIENTHI()
        {
            InitializeComponent();
        }

        private void Init_Form()
        {
            try
            {
                ucgview_DSPHT.gridView.OptionsView.ColumnAutoWidth = true;
                ucgview_DSPHT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucgview_DSPHT.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                ucgview_DSPHT.gridView.OptionsBehavior.Editable = false;
                ucgview_DSPHT.setMultiSelectMode(true);

                ucgview_DSPHT.setEvent(ucgview_DSPHT_Load);
                ucgview_DSPHT.SetReLoadWhenFilter(true);
                ucgview_DSPHT.gridView.Click += ucgview_DSPHT_Click;
                //ucgview_DSPHT.gridView.ColumnFilterChanged += ucgview_DSPHT_ColumnFilterChanged;

                ucgview_DSPHT.setNumberPerPage(new int[] {  100, 200, 500 });
                ucgview_DSPHT.onIndicator();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        
        private void Load_DataGrid(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }
                
                ResponsList responses = new ResponsList();

                //string jsonFilter = "";
                //if (ucgview_DSPHT.ReLoadWhenFilter && ucgview_DSPHT.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSPHT.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "DMC54.GETTHIETLAP", page, ucgview_DSPHT.ucPage1.getNumberPerPage(), new string[] { "[0]" }, new string[] { "1" }, ucgview_DSPHT.jsonFilter());

                ucgview_DSPHT.clearData();

                DataTable dt_DSPHT = new DataTable();
                dt_DSPHT = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSPHT.Rows.Count == 0)
                    dt_DSPHT = Func.getTableEmpty(new String[] { "MAPHONG", "TENPHONG"});

                ucgview_DSPHT.setData(dt_DSPHT, responses.total, responses.page, responses.records);
                ucgview_DSPHT.setColumnAll(false);
                
                ucgview_DSPHT.setColumn("MAPHONG", 1, "Mã phòng");
                ucgview_DSPHT.setColumn("TENPHONG", 2, "Tên phòng");

                ucgview_DSPHT.gridView.BestFitColumns(true);

                for (int i = 0; i < dt_DSPHT.Rows.Count; i++)
                {
                    if (dt_DSPHT.Rows[i]["CHECKED"] != null && "1".Equals(dt_DSPHT.Rows[i]["CHECKED"].ToString()))
                    {
                        ucgview_DSPHT.gridView.SelectRow(i);
                    }
                }

                int ct = 0;
                for (int i = 0; i < dt_DSPHT.Rows.Count; i++)
                {
                    if (dt_DSPHT.Rows[i]["CHECKED"].ToString() != "0")
                    {
                        ct++;
                    }
                }

                string message = ct == 0 ? "Chưa có phòng thiết lập hiển thị với User này" :
                    "Đã có " + ct + " phòng được thiết lập với User này.";
                label_ThongBao.Text = message;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void LuuThongTin()
        {
            try
            {
                int[] selectRow = ucgview_DSPHT.gridView.GetSelectedRows();

                var list = new List<object>();
                DataRowView dr;
                for (int i = 0; i < selectRow.Length; i++)
                {
                    dr = (DataRowView)ucgview_DSPHT.gridView.GetRow(selectRow[i]);
                    if (dr != null && dr["IDPHONG"] != null)
                    {
                        list.Add(new
                        {
                            IDPHONG = dr["IDPHONG"].ToString(),
                            MAPHONG = dr["MAPHONG"].ToString(),
                            TENPHONG = dr["TENPHONG"].ToString(),
                            CHECKED = dr["CHECKED"].ToString()
                        });
                    }
                }
                string dataJson = JsonConvert.SerializeObject(list).Replace("\"", "\\\"");

                DialogResult dialogResult = MessageBox.Show("Các thiết lập cũ sẽ mất đi khi cập nhật lại. Bạn có tiếp tục ?", "",
                    MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.OK)
                {
                    var rs = RequestHTTP.call_ajaxCALL_SP_I("DMC54.THEMTHIETLAP", string.Join("$", dataJson));
                    int fl = 0;
                    int.TryParse(rs, out fl);
                    if (fl > 0)
                    {
                        MessageBox.Show("Cập nhật thiết lập phòng hiển thị thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cập nhật thiết lập phòng hiển thị.");
                    }
                }
                Load_DataGrid(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void DMC54_PHONGHIENTHI_Load(object sender, EventArgs e)
        {
            Init_Form();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            LuuThongTin();
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucgview_DSPHT_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGrid(pageNum);
        }

        private void ucgview_DSPHT_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucgview_DSPHT.gridView.FocusedColumn.FieldName))
            {
                int id = ucgview_DSPHT.gridView.FocusedRowHandle;
                if (ucgview_DSPHT.gridView.GetSelectedRows().Any(o => o == id))
                {
                    ucgview_DSPHT.gridView.UnselectRow(id);
                }
                else
                {
                    ucgview_DSPHT.gridView.SelectRow(id);
                }
            }
        }

        //private void ucgview_DSPHT_ColumnFilterChanged(object sender, EventArgs e)
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
    }
}