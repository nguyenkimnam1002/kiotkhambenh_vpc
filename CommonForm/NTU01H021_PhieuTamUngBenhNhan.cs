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
    public partial class NTU01H021_PhieuTamUngBenhNhan : DevExpress.XtraEditors.XtraForm
    {
        public NTU01H021_PhieuTamUngBenhNhan()
        {
            InitializeComponent();
        }

        string khambenhid = "";
        string benhnhanId = "";
        string thoigianvaovien = "";
        string khoaid = Const.local_khoaId.ToString(); 
        public void setData(string khambenhid, string benhnhanId, string thoigianvaovien)
        {
            this.khambenhid = khambenhid;
            this.benhnhanId = benhnhanId;
            this.thoigianvaovien = thoigianvaovien;
        }
    }
}