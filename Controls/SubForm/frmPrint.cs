using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Windows.Forms; 
using VNPT.HIS.Common;
using System.IO; 

namespace VNPT.HIS.Controls.SubForm
{
    public partial class frmPrint : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        //string title="";

        string code="";
        string type= "pdf";
        DataTable par_table = new DataTable();

        // Hàm chuẩn
        public frmPrint(DataTable par_table, string code, string type = "pdf", int heightForm = 0, int widthForm = 0)
        {
            InitializeComponent();

            this.par_table = par_table;
            this.code = code;
            this.type = type;

            this.Text = code + "." + type;

            if (heightForm != 0) this.Height = heightForm;
            if (widthForm != 0) this.Width = widthForm;
        }

        public frmPrint(string title, string code, DataTable par_table)
        {
            InitializeComponent();
             
            this.code = code;
            //this.type = type;
            this.par_table = par_table;

            this.Text = title;
        }
        public frmPrint(string title, string code, DataTable par_table, int heightForm, int widthForm)
        {
            InitializeComponent();

            this.Height = heightForm;
            this.Width = widthForm;
             
            this.code = code;
            //this.type = type;
            this.par_table = par_table;

            this.Text = title;
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            try
            {
                string url = Func.getUrlReport(code, par_table, type);

                Stream stream = Func.GetStreamFromUrl(url);
                //pdfStream = Func.GetStreamFromUrl(url);
                if (stream != null)
                {
                    pdfViewer1.LoadDocument(stream);
                } 
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString() + " --> " + Newtonsoft.Json.JsonConvert.SerializeObject(par_table));
            }

            //SendKeys.Send("^P");
        }

        private void frmPrint_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        //private  PdfViewer pdfViewer;
        //private PdfBarController pdfBarController;
        //private BarManager barManager; 

        //protected void CreateControlCore() {
        //    System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
        //    pdfViewer = new PdfViewer();
        //    pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
        //    pdfViewer.DetachStreamAfterLoadComplete = false;
        //    panel.Controls.Add(pdfViewer);
        //    pdfBarController = new PdfBarController();
        //    pdfBarController.Control = pdfViewer;
        //    barManager = new BarManager();
        //    barManager.Form = panel;
        //    pdfViewer.Load += pdfViewer_Load;
        //    return panel;
        //} 
        
        //void pdfViewer_Load(object sender, EventArgs e) {
            
        //}
        //Stream pdfStream = new MemoryStream();
        //protected override void ReadValueCore() { 
        //    if (pdfViewer != null) {
        //        pdfStream.Position = 0;
        //        pdfViewer.LoadDocument(pdfStream);
        //    } else {
        //        pdfStream = null;
        //        pdfViewer.CloseDocument();
        //    }
        //}
    }
}