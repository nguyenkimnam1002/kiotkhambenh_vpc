using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Threading;
using VNPT.HIS.Common;
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K053_PK_LCDBD : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        int secon = 2;
        public NGT02K053_PK_LCDBD()
        {
            InitializeComponent();
        }

        private Thread thread;

        public void ThreadJob()
        {
            int dem = 0;
            while (thread.IsAlive)
            {
                Thread.Sleep(1000);

                string time = DateTime.Now.ToString("HH:mm:ss");

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        if (gridView1.Columns["ORG_NAME"] != null) gridView1.Columns["ORG_NAME"].Caption = time;
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }));
                }
                else
                {
                    lock (this)
                    {
                        if (gridView1.Columns["ORG_NAME"] != null) gridView1.Columns["ORG_NAME"].Caption = time;
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }
                }
                //
            }
        }

        private void NGT02K053_PK_LCDBD_Load(object sender, EventArgs e)
        {
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            refresh_data();

            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            thread.Start();
        }
        float font = 53F;
        int rows = 0;

        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ORG_NAME");
            table.Columns.Add("STT");
            table.Columns.Add("CHEDO");
            for (int i = 0; i < new Random().Next(10); i++) //new Random().Next(11)
                table.Rows.Add("Quầy 01", "999");
            return table;
        }
        private void refresh_data()
        {
            try
            {
                //Get datafrom sv
                DataTable dt = new DataTable();
                //dt = CreateDataTable();
                string json = "";
                json += Func.json_item("SOURCE", "0");
                json += Func.json_item("PHONGID", Const.local_phongId.ToString());
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"").Replace("//", "");

                dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K053.PKLCDBD", json, 0);

                rows = dt.Rows.Count;

                // Create table
                DataTable data = new DataTable();
                data.Columns.Add("ORG_NAME");
                data.Columns.Add("STT");
                data.Columns.Add("CHEDO");
                //data.Columns.Add("STT1");
                //data.Columns.Add("STT2");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int stt = Convert.ToInt32(dt.Rows[i]["STT"].ToString() != "" ? dt.Rows[i]["STT"].ToString() : "0");

                    data.Rows.Add(dt.Rows[i]["ORG_NAME"].ToString()
                        , Func.addZezo(stt, 4), dt.Rows[i]["CHEDO"].ToString());

                }
                // set data 
                gridControl1.DataSource = data;

                // Set font, height,...
                gridView1.Columns["STT"].Caption = "SỐ THỨ TỰ";

                gridView1.Columns["ORG_NAME"].AppearanceCell.ForeColor = Color.White;
                gridView1.Columns["ORG_NAME"].AppearanceCell.BackColor = ColorTranslator.FromHtml("#2F74B5");

                gridView1.Columns["CHEDO"].Visible = false;
                //gridView1.Columns["STT"].AppearanceCell.ForeColor = Color.Red;

                gridView1.Columns["ORG_NAME"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold);
                gridView1.RowCellStyle += GridView1_RowCellStyle;
                // re-calculate height 
                Cal();

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void GridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == "STT")
            {
                var value = currentView.GetRowCellValue(e.RowHandle, "CHEDO").ToString();

                if (value == "0")
                    e.Appearance.ForeColor = Color.Black;
                else if (value == "1")
                    e.Appearance.ForeColor = Color.Red;
                else
                    e.Appearance.ForeColor = Color.Gray;
            }

        }

        private void Cal()
        {
            if (rows <= 0) return;
            float f = (gridControl1.Size.Height - 51 - 10) / rows;
            int i = (int)(f / 0.185);

            font = ((float)i) / 10;

            // 51 là chiều cao dòng Header với font 30F
            if (font > 53) font = 53;
            if (font < 10) font = 10;

            gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", font, System.Drawing.FontStyle.Bold);

            gridView1.BestFitColumns(true);

            //Text =  " font=" + this.font + " - " + " " + gridControl1.Size.Height;
        }

        private void NGT02K053_PK_LCDBD_Resize(object sender, EventArgs e)
        {
            Cal(); // re-calculate height 
        }

        private void NGT02K053_PK_LCDBD_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }

    }
}