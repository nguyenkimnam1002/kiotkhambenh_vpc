using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using VNPT.HIS.Common;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K028_HienthiDanhSachKham : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        int second = 30;
        int rows = 0;
        bool isFullScreen = false;

        public NGT02K028_HienthiDanhSachKham()
        {
            InitializeComponent();
        }

        private Thread thread;

        public void ThreadJob()
        {
            int count = 0;
            while (thread.IsAlive)
            {
                Thread.Sleep(1000);

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        count = (count + 1) % second;
                        if (count == 0) RefreshData();
                    }));
                }
                else
                {
                    lock (this)
                    {
                        count = (count + 1) % second;
                        if (count == 0) RefreshData();
                    }
                }
            }
        }

        private void RefreshData()
        {
            //Get datafrom sv
            DataTable dt = new DataTable();
            dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K001.LCD", Const.local_phongId.ToString(), 0);

            rows = dt.Rows.Count;

            // Create table
            DataTable data = new DataTable();
            data.Columns.Add("SOTHUTU");
            data.Columns.Add("TENBENHNHAN");
            data.Columns.Add("DANGGOIKHAM");
            data.Columns.Add("UUTIENKHAM");
            data.Columns.Add("LANGOIKHAM");
            data.Columns.Add("UUTIENKHAMID");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string uutienkham = string.Empty;

                if (!"0".Equals(dt.Rows[i]["LANGOIKHAM"].ToString()))
                {
                    uutienkham = "CÓ";
                }
                else
                {
                    uutienkham = "KHÔNG";
                }

                data.Rows.Add(dt.Rows[i]["SOTHUTU"].ToString()
                    , dt.Rows[i]["TENBENHNHAN"].ToString()
                    , dt.Rows[i]["DANGGOIKHAM"].ToString()
                    , uutienkham
                    , dt.Rows[i]["LANGOIKHAM"].ToString()
                    , dt.Rows[i]["UUTIENKHAMID"].ToString());
            }

            //Dummy Data
            //data.Rows.Add("0002", "Test", "0", "CÓ", "0", "0");
            //data.Rows.Add("0003", "Test1", "1", "CÓ", "1", "1");
            //data.Rows.Add("0004", "Test2", "0", "CÓ", "1", "1");
            //data.Rows.Add("0004", "Test2", "1", "CÓ", "1", "0");

            // set data 
            gridControl1.DataSource = data;

            // Set font, height,...
            gridView1.Columns["SOTHUTU"].Caption = "SỐ KHÁM";
            gridView1.Columns["TENBENHNHAN"].Caption = "HỌ TÊN";
            gridView1.Columns["UUTIENKHAM"].Caption = "ƯU TIÊN";
            gridView1.Columns["LANGOIKHAM"].Caption = "LẦN GỌI";
            gridView1.Columns["DANGGOIKHAM"].Visible = false;
            gridView1.Columns["UUTIENKHAMID"].Visible = false;

            text_temp.Focus();
        }

        private void NGT02K028_HienthiDanhSachKham_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }

        private void NGT02K028_HienthiDanhSachKham_Load(object sender, EventArgs e)
        {
            // Set properties label
            this.lblTitle.Text = Const.local_phong;
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.BackColor = System.Drawing.ColorTranslator.FromHtml("#004B7C");

            // Set properties label
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            RefreshData();

            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            thread.Start();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // Status of properties to set layout
            bool isBlink = false;
            bool isBlink1 = false;
            bool isBlinkUT = false;
            bool isSetFont = false;
            bool isSetFontUT = false;
            bool isHightlightRow = false;

            DataRowView data = (DataRowView)gridView1.GetRow(e.RowHandle);

            if (data == null)
            {
                return;
            }

            if ("1".Equals(data.Row["DANGGOIKHAM"].ToString()))
            {
                if (e.RowHandle == 0)
                {
                    if ("SOTHUTU".Equals(e.Column.FieldName))
                    {
                        isBlink = true;
                        isSetFont = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                    else if ("TENBENHNHAN".Equals(e.Column.FieldName))
                    {
                        isBlink = true;
                        isSetFont = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                    else if ("UUTIENKHAM".Equals(e.Column.FieldName))
                    {
                        isBlink = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                    else if ("LANGOIKHAM".Equals(e.Column.FieldName))
                    {
                        isBlink = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                }
                else
                {
                    if (!"0".Equals(data.Row["UUTIENKHAMID"].ToString()))
                    {
                        if ("SOTHUTU".Equals(e.Column.FieldName))
                        {
                            isBlinkUT = true;
                            isSetFont = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("TENBENHNHAN".Equals(e.Column.FieldName))
                        {
                            isBlinkUT = true;
                            isSetFont = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("UUTIENKHAM".Equals(e.Column.FieldName))
                        {
                            isBlinkUT = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("LANGOIKHAM".Equals(e.Column.FieldName))
                        {
                            isBlinkUT = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                    }
                    else
                    {
                        if ("SOTHUTU".Equals(e.Column.FieldName))
                        {
                            isBlink = true;
                            isSetFont = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("TENBENHNHAN".Equals(e.Column.FieldName))
                        {
                            isBlink = true;
                            isSetFont = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("UUTIENKHAM".Equals(e.Column.FieldName))
                        {
                            isBlink = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("LANGOIKHAM".Equals(e.Column.FieldName))
                        {
                            isBlink = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                    }
                }
            }

            if ("0".Equals(data.Row["DANGGOIKHAM"].ToString()))
            {
                if (e.RowHandle == 0)
                {
                    if ("SOTHUTU".Equals(e.Column.FieldName))
                    {
                        isHightlightRow = true;
                        isSetFont = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                    else if ("TENBENHNHAN".Equals(e.Column.FieldName))
                    {
                        isHightlightRow = true;
                        isSetFont = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                    else if ("UUTIENKHAM".Equals(e.Column.FieldName))
                    {
                        isHightlightRow = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                    else if ("LANGOIKHAM".Equals(e.Column.FieldName))
                    {
                        isHightlightRow = true;
                        UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                    }
                }
                else
                {
                    if (!"0".Equals(data.Row["UUTIENKHAMID"].ToString()))
                    {
                        if ("SOTHUTU".Equals(e.Column.FieldName))
                        {
                            isSetFontUT = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("TENBENHNHAN".Equals(e.Column.FieldName))
                        {
                            isSetFontUT = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("UUTIENKHAM".Equals(e.Column.FieldName))
                        {
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("LANGOIKHAM".Equals(e.Column.FieldName))
                        {
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                    }
                    else
                    {
                        if ("SOTHUTU".Equals(e.Column.FieldName))
                        {
                            isSetFont = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("TENBENHNHAN".Equals(e.Column.FieldName))
                        {
                            isSetFont = true;
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("UUTIENKHAM".Equals(e.Column.FieldName))
                        {
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                        else if ("LANGOIKHAM".Equals(e.Column.FieldName))
                        {
                            UpdateStyle(e, isBlink, isBlink1, isBlinkUT, isSetFont, isSetFontUT, isHightlightRow);
                        }
                    }
                }
            }
        }

        private void UpdateStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e, bool isBlink, bool isBlink1, bool isBlinkUT, bool isSetFont, bool isSetFontUT, bool isHightlightRow)
        {
            if (isBlink)
            {
                e.Appearance.BackColor = SystemColors.MenuHighlight;
                e.Appearance.Font = new Font("Tahoma", 20);
                if (isSetFont)
                {
                    e.Appearance.Font = new Font("Tahoma", 40);
                }
            }
            else if (isBlink1)
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.BackColor = SystemColors.MenuHighlight;
                e.Appearance.Font = new Font("Tahoma", 20);
                if (isSetFont)
                {
                    e.Appearance.Font = new Font("Tahoma", 40, FontStyle.Bold);
                }
            }
            else if (isBlinkUT)
            {
                e.Appearance.Font = new Font("Tahoma", 20);
                if (isSetFont)
                {
                    e.Appearance.Font = new Font("Tahoma", 40);
                }
            }
            else if (isHightlightRow)
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.Font = new Font("Tahoma", 20);
                if (isSetFont)
                {
                    e.Appearance.Font = new Font("Tahoma", 40, FontStyle.Bold);
                }
            }
            else if (isSetFont)
            {
                e.Appearance.Font = new Font("Tahoma", 40);
            }
            else if (isSetFontUT)
            {
                e.Appearance.Font = new Font("Tahoma", 20);
            }
            else
            {
                e.Appearance.Font = new Font("Tahoma", 20);
            }
        }

        private void NGT02K028_HienthiDanhSachKham_DoubleClick(object sender, EventArgs e)
        {
            if (!isFullScreen)
            {
                this.WindowState = FormWindowState.Maximized;
                isFullScreen = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                isFullScreen = false;
            }
        }
    }
}