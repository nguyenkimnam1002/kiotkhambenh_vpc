using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.CommonForm
{
    public partial class NTU02D035_Thongtintainanthuongtich : DevExpress.XtraEditors.XtraForm
    {
        public NTU02D035_Thongtintainanthuongtich()
        {
            InitializeComponent();
        }

        string khambenhid = "";
        string benhnhanId = "";
        string hosobenhanid = "";
        public void setData(string khambenhid, string benhnhanId, string hosobenhanid)
        {
            this.khambenhid = khambenhid;
            this.benhnhanId = benhnhanId;
            this.hosobenhanid = hosobenhanid;
        }
    }
}