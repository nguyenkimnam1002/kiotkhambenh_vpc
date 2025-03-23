using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NTU02D081_Capnhat_PhieuDT : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NTU02D081_Capnhat_PhieuDT()
        {
            InitializeComponent();
        }

        string maubenhphamid = "";
        string khambenhid = "";
        string phieudieutriid = "";
        public void loadData(string maubenhphamid, string khambenhid, string phieudieutriid)
        {
            this.maubenhphamid = maubenhphamid;
            this.khambenhid = khambenhid;
            this.phieudieutriid = phieudieutriid;
        }

        private void NTU02D081_Capnhat_PhieuDT_Load(object sender, EventArgs e)
        {
            // {"func":"ajaxExecuteQuery","params":["","COM.PHIEUDIEUTRI"],"options":[{"name":"[0]","value":"100858"},{"name":"[1]","value":"4"}],"uuid":"6b9f8ac4-1223-4f85-b72d-2311ed4f8639"}
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("", new string[] { "[0]", "[1]" }, new string[] { khambenhid, "4" });
            if (dt.Rows.Count == 0)
            {
                dt.Columns.Add("col1");
                dt.Columns.Add("col2");
                DataRow dr = dt.NewRow();
                dr["col1"] = "-1";
                dr["col2"] = "Chưa có phiếu điều trị";
                dt.Rows.Add(dr);
            }
            ucComboBox1.setData(dt, 0, 1);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string phieudieutriid = ucComboBox1.SelectValue;
            if (phieudieutriid != "-1")
            {
                string ret = RequestHTTP.get_execute("NTU02D081.01", new string[] { "[0]", "[1]" }, new string[] { phieudieutriid, maubenhphamid });

                if (ret == "1")//thành công
                {
                    MessageBox.Show("Cập nhật thành công!");
                }  
            }
            else
            {
                MessageBox.Show("Chưa chọn phiếu điều trị!");
            }
        }
    }
}