using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NTU02D043_DanhSachPhieuThuocDiKem : DevExpress.XtraEditors.XtraForm
    {
        public NTU02D043_DanhSachPhieuThuocDiKem()
        {
            InitializeComponent();
        }
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        string khambenhid = "";
        string dichvucha_id = "";
        public void setData(string khambenhid, string dichvucha_id)
        {
            this.khambenhid = khambenhid;
            this.dichvucha_id = dichvucha_id;
        } 
    }
}