using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.Duoc.subform
{
    public partial class DUC_DSInPhieuXuatTTKHA : DevExpress.XtraEditors.XtraForm
    {
        string nhapxuatid;
        public DUC_DSInPhieuXuatTTKHA(string nhapxuatid)
        {
            InitializeComponent();

            this.nhapxuatid = nhapxuatid;
        }
        private void doPrint(string _print_type)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("nhapxuatid", "String", nhapxuatid);

            VNPT.HIS.Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, _print_type.Trim(), "pdf", 720, 1200);
            openForm(frm);
        }
        private void openForm(Form frm, string optionsPopup = "1")
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        private void cmdInPhieuXuat_Click(object sender, EventArgs e)
        {
            doPrint("DUC009_PHIEUXUATKHO_QD_BTC_A4_TTKHA");
        }

        private void cmdInPXKhoLe_Click(object sender, EventArgs e)
        {
            doPrint("DUC009_PHIEUXUATKHO_KHOLE_TTKHA");
        }

        private void cmdInPXHuongThan_Click(object sender, EventArgs e)
        {
            doPrint("DUC009_PHIEUXUATKHO_NGT_TAMTHAN_TTKHA");
        }

        private void cmdInPXThuong_Click(object sender, EventArgs e)
        {
            doPrint("DUC009_PHIEUXUATKHO_NGT_THUONG_TTKHA"); 
        }

        private void cmdDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}