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
    public partial class NGT02K027_ThongbaoDangKyKham : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Thread thread;
        int secon = 2;
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K027_ThongbaoDangKyKham()
        {
            InitializeComponent();
        }
        public void ThreadJob()
        {
            int dem = 0;
            while (thread.IsAlive)
            {
                Thread.Sleep(1000);

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }));
                }
                else
                {
                    lock (this)
                    {
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }
                }
            }
        }
        private void refresh_data()
        {
            try
            {
                lblSTT.Text = "";
                string stt_bd = "1";
                string stt_kt = "1";
                string sl_goi = "5";
                //Get datafrom sv
                DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NGT_STT_GOI", DateTime.Now.ToString("dd/MM/yyyy"));

                if (dt.Rows.Count > 0)
                {
                    stt_bd = dt.Rows[0]["STT_BD"].ToString();
                    sl_goi = dt.Rows[0]["SL_GOI"].ToString();
                    stt_kt = (int.Parse(stt_bd) + int.Parse(sl_goi) - 1).ToString();
                }
                if (stt_bd != stt_kt)
                {
                    lblSTT.Text = _leftPad(stt_bd, 4) + " - " + _leftPad(stt_kt, 4);
                    lblQuay.Text = "SỐ 1";
                }
                else
                {
                    lblSTT.Text = _leftPad(stt_bd, 4);
                    lblQuay.Text = "SỐ 1";
                }

                if (stt_bd == "-1")
                {
                    lblSTT.Text = "0001";
                    lblQuay.Text = "SỐ 1";
                    // neu chua co thi mac dinh benh nhan la 0001; 
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        private string _leftPad(string number, int targetLength)
        {
            string output = number;
            if (output.Length == 4) return output;
            while (output.Length < targetLength)
            {
                output = "0" + output;
            }
            return output;
        }

        private void NGT02K027_ThongbaoDangKyKham_Load(object sender, EventArgs e)
        {
            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            thread.Start();
        }
        private void Cal()
        {
            if (this.Width > 1000)
                lblSTT.Appearance.Font = new System.Drawing.Font("Tahoma", 92F, System.Drawing.FontStyle.Bold);
            else
                lblSTT.Appearance.Font = new System.Drawing.Font("Tahoma", 72F, System.Drawing.FontStyle.Bold);
        }

        private void NGT02K027_ThongbaoDangKyKham_Resize(object sender, EventArgs e)
        {
            Cal();
        }

        private void NGT02K027_ThongbaoDangKyKham_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }
    }
}