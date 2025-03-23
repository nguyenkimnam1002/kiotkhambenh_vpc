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
    public partial class HOSO_QUANLYSUCKHOECANHAN : DevExpress.XtraEditors.XtraForm
    {
        public HOSO_QUANLYSUCKHOECANHAN()
        {
            InitializeComponent();
        }

        string khambenhid = "";
        string benhnhanId = "";
        string mabenhnhan = "";
        string khoaid = Const.local_khoaId.ToString();
        string phongid = Const.local_phongId.ToString();
        public void setData(string khambenhid, string benhnhanId, string mabenhnhan)
        {
            this.khambenhid = khambenhid;
            this.benhnhanId = benhnhanId;
            this.mabenhnhan = mabenhnhan; 
        }
    }
}