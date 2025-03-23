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

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K053_PK_LCDBD1 : DevExpress.XtraEditors.XtraForm
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
        public NGT02K053_PK_LCDBD1()
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
                        if (lblTime.Text != null) lblTime.Text = time;
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }));
                }
                else
                {
                    lock (this)
                    {
                        if (lblTime.Text != null) lblTime.Text = time;
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }
                }
                //
            }
        }

        float font = 43F;
        int rows = 0;

        private void NGT02K053_PK_LCDBD1_Load(object sender, EventArgs e)
        {

            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            refresh_data();

            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            thread.Start();
        }

        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("TIEUDE");
            table.Columns.Add("GIATRI");
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
                json += Func.json_item("PHONGID", Const.local_phongId.ToString());
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"").Replace("//", "");

                dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K053.PKLCDBD1", json, 0);

                rows = dt.Rows.Count;

                // Create table
                DataTable data = new DataTable();
                data.Columns.Add("TIEUDE");
                data.Columns.Add("GIATRI");

                if (rows == 1)
                {
                    data.Rows.Add("TÊN BỆNH NHÂN", dt.Rows[0]["TENBENHNHAN"].ToString());
                    data.Rows.Add("GIỚI TÍNH", dt.Rows[0]["GIOITINH"].ToString());
                    data.Rows.Add("NGÀY SINH", dt.Rows[0]["NGAYSINH"].ToString());

                    int stt = Convert.ToInt32(dt.Rows[0]["SOTHUTU"].ToString() != "" ? dt.Rows[0]["SOTHUTU"].ToString() : "0");
                    data.Rows.Add("SỐ THỨ TỰ", Func.addZezo(stt, 4));

                    lblTieuDe.Text = dt.Rows[0]["PHONGTIEPNHAN"].ToString();
                    lblPhongKham.Text = "Mời vào " + dt.Rows[0]["PHONGKHAM"].ToString();
                }
                // set data 
                gridControl1.DataSource = data;

                // Set font, height,...

                gridView1.Columns["TIEUDE"].Width = 550;
                gridView1.Columns["TIEUDE"].AppearanceCell.ForeColor = Color.White;
                gridView1.Columns["TIEUDE"].AppearanceCell.BackColor = ColorTranslator.FromHtml("#2F74B5");

                gridView1.Columns["GIATRI"].Width = 800;
                gridView1.Columns["GIATRI"].AppearanceCell.ForeColor = Color.White;
                gridView1.Columns["GIATRI"].AppearanceCell.BackColor = ColorTranslator.FromHtml("#2F74B5");
               
                // re-calculate height 
                Cal();

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        
        private void Cal()
        {
            if (rows <= 0) return;
            float w = this.Width;

            if (w > 1000)
            {
                font = 36;
                gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", font, System.Drawing.FontStyle.Bold);
                gridView1.UserCellPadding = new Padding(25);
            }
            else
            {
                font = 28;
                gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", font, System.Drawing.FontStyle.Bold);
                gridView1.UserCellPadding = new Padding(18);
            }


            //gridView1.BestFitColumns(true);
        }

        private void NGT02K053_PK_LCDBD1_Resize(object sender, EventArgs e)
        {
            Cal(); // re-calculate height 
        }

        private void NGT02K053_PK_LCDBD1_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }
    }
}