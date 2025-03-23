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
    public partial class NTU02D037_PhauthuatThuThuat : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NTU02D037_PhauthuatThuThuat()
        {
            InitializeComponent();
        }
        string mabenhnhan = "";
        string khambenhid = "";
        string tenbenhnhan = "";
        string namsinh = "";
        string nghenghiep = "";
        string hosobenhanid = "";
        string tiepnhanid = "";
        string phongid = "";
        string benhnhanid = "";
        string khoaid = "";
        string tenphong = "";
        string thoigianvaovien = "";
        string maubenhphamid = "";
        string dichvukhambenhid = "";
        public void setData(string mabenhnhan, string khambenhid, string tenbenhnhan, string namsinh, string nghenghiep, string hosobenhanid, string tiepnhanid
                    , string phongid, string benhnhanid, string khoaid, string tenphong, string thoigianvaovien, string maubenhphamid, string dichvukhambenhid)
        {
            this.mabenhnhan = mabenhnhan; 
            this.khambenhid = khambenhid;
            this.tenbenhnhan = tenbenhnhan;
            this.namsinh = namsinh;
            this.nghenghiep = nghenghiep;
            this.hosobenhanid = hosobenhanid;
            this.tiepnhanid = tiepnhanid;
            this.phongid = phongid;
            this.benhnhanid = benhnhanid;
            this.khoaid = khoaid;
            this.tenphong = tenphong;
            this.thoigianvaovien = thoigianvaovien;
            this.maubenhphamid = maubenhphamid;
            this.dichvukhambenhid = dichvukhambenhid;

        }

    }
}