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
    public partial class NGT02K020_ThuocConSuDung : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K020_ThuocConSuDung()
        {
            InitializeComponent();
        }

        string benhNhanID = "";
        public void setParam(string benhNhanID)
        {
            this.benhNhanID = benhNhanID; 
        }

        private void Init_Form()
        {
            try
            {
                ucgview_ThuocConSD.gridView.OptionsView.ColumnAutoWidth = true;
                ucgview_ThuocConSD.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucgview_ThuocConSD.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucgview_ThuocConSD.gridView.OptionsBehavior.Editable = false;
                ucgview_ThuocConSD.setMultiSelectMode(true);

                ucgview_ThuocConSD.setEvent(ucgview_ThuocConSD_Load);
                ucgview_ThuocConSD.SetReLoadWhenFilter(true);
                //ucgview_ThuocConSD.gridView.Click += ucgview_ThuocConSD_Click;
                //ucgview_ThuocConSD.gridView.ColumnFilterChanged += ucgview_ThuocConSD_ColumnFilterChanged;

                ucgview_ThuocConSD.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
                ucgview_ThuocConSD.onIndicator();
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
                //if (ucgview_ThuocConSD.ReLoadWhenFilter && ucgview_ThuocConSD.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_ThuocConSD.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NGT02K020.LAYDL", page, ucgview_ThuocConSD.ucPage1.getNumberPerPage(), new string[] { "[0]" }, new string[] { benhNhanID }, ucgview_ThuocConSD.jsonFilter());

                ucgview_ThuocConSD.clearData();

                DataTable dt_ThuocConSD = new DataTable();
                dt_ThuocConSD = MyJsonConvert.toDataTable(responses.rows);

                if (dt_ThuocConSD.Rows.Count == 0)
                    dt_ThuocConSD = Func.getTableEmpty(new String[] { "MAPHONG", "TENPHONG" });

                ucgview_ThuocConSD.setData(dt_ThuocConSD, responses.total, responses.page, responses.records);
                ucgview_ThuocConSD.setColumnAll(false);

                ucgview_ThuocConSD.setColumn("TEN", 1, "Tên thuốc");
                ucgview_ThuocConSD.setColumn("SOLUONG", 2, "SL thuốc còn sử dụng");
                ucgview_ThuocConSD.setColumn("TEN_DVT", 3, "ĐVT");

                ucgview_ThuocConSD.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void NGT02K020_ThuocConSuDung_Load(object sender, EventArgs e)
        {
            Init_Form(); 
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucgview_ThuocConSD_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGrid(pageNum);
        }
    }
}