using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MainForm
{
    public partial class report_BangKe_ChiPhi_KB : DevExpress.XtraReports.UI.XtraReport
    {
        public report_BangKe_ChiPhi_KB()
        {
            InitializeComponent();

            //report_BangKe_ChiPhi_KB.my
            //Ho_ten.Value = "test sfsdf sdfsdf asdfs sdf ";
        }

        private void report_BangKe_ChiPhi_KB_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
    }
}
