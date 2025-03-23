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
    public partial class NGT01T006_hoahong : DevExpress.XtraEditors.XtraForm
    {
        string KHAMBENHID = "";
        string MABENHAN = "";
        public NGT01T006_hoahong(string KHAMBENHID, string MABENHAN)
        {
            InitializeComponent();

            this.KHAMBENHID = KHAMBENHID;
            this.MABENHAN = MABENHAN;
        }

        string NGTID = "";
        string HOAHONGID = "";
        private void NGT01T006_hoahong_Load(object sender, EventArgs e)
        {
            txtMaBenhAn.Text = MABENHAN;
            // {"func":"ajaxExecuteQueryO","params":["","NGT.NGT01"],"options":[{"name":"[0]","value":"100415"}],"uuid":"bd575da2-56b9-49d6-a2f7-79e7545f079a"}
            DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NGT.NGT01", KHAMBENHID);
            // [{"NGTID": "181","HOAHONGID": "181","TEN": "hung","DIACHI": "HN","SODIENTHOAI": "0123456","NGANHANG": "ck","GHICHU": "ko co gi"}]
            if (dt != null && dt.Rows.Count > 0)
            {
                btnXoa.Enabled = true;

                txtTEN.Text = dt.Rows[0]["TEN"].ToString();
                txtSODIENTHOAI.Text = dt.Rows[0]["SODIENTHOAI"].ToString();
                txtDIACHI.Text = dt.Rows[0]["DIACHI"].ToString();
                txtNGANHANG.Text = dt.Rows[0]["NGANHANG"].ToString();
                txtGHICHU.Text = dt.Rows[0]["GHICHU"].ToString();
                NGTID = dt.Rows[0]["NGTID"].ToString();
                HOAHONGID = dt.Rows[0]["HOAHONGID"].ToString();
            }
            else
                btnXoa.Enabled = false;

        }
        private string commit(string act)
        {
            // {"func":"ajaxCALL_SP_S","params":["NGT01T001.NHAPHH","{\"MABENHAN\":\"BA00001012\",\"TEN\":\"hung\",\"SODIENTHOAI\":\"0123456\"
            // ,\"DIACHI\":\"HN\",\"NGANHANG\":\"ck1\",\"GHICHU\":\"ko co gi\",\"NGTID\":\"181\",\"HOAHONGID\":\"181\",\"KHAMBENHID\":\"100415\"}"],"uuid":"bd575da2-56b9-49d6-a2f7-79e7545f079a"}
            string json = "";
            if (act == "delete") json += Func.json_item("act", act);
            json += Func.json_item("MABENHAN", MABENHAN);
            json += Func.json_item("TEN", txtTEN.Text);
            json += Func.json_item("SODIENTHOAI", txtSODIENTHOAI.Text);
            json += Func.json_item("DIACHI", txtDIACHI.Text);
            json += Func.json_item("NGANHANG", txtNGANHANG.Text);
            json += Func.json_item("GHICHU", txtGHICHU.Text);
            json += Func.json_item("NGTID", NGTID);
            json += Func.json_item("HOAHONGID", HOAHONGID);
            json += Func.json_item("KHAMBENHID", KHAMBENHID);
            json = Func.json_item_end(json);

            return RequestHTTP.call_ajaxCALL_SP_S_result("NGT01T001.NHAPHH", json.Replace("\\", "\\\\").Replace("\"", "\\\""));
            // {"result": "181,181","out_var": "[]","error_code": 0,"error_msg": ""}
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTEN.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tên");
                return;
            }
            if (txtSODIENTHOAI.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại");
                return;
            }
            if (txtDIACHI.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ");
                return;
            }


            string result = commit("");


            string[] ret = result.Split(',');
            string tbao = "";
            int n;
            bool conv = int.TryParse(ret[0], out n);

            if (conv && n > 0) // thành công
            {
                tbao = "Cập nhật thông tin hoa hồng thành công";

                btnXoa.Enabled = true;

                NGTID = ret[0];
                HOAHONGID = ret[1];
            }
            else // không thành công
            {
                tbao = "Cập nhật thông tin hoa hồng không thành công";
            }

            MessageBox.Show(tbao);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa thông tin hoa hồng?", "", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {

                string result = commit("delete");

                string[] ret = result.Split(',');
                string tbao = "";
                int n;
                bool conv = int.TryParse(ret[0], out n);

                if (conv && n == 1) // thành công
                {
                    tbao = "Xóa dữ liệu hoa hồng thành công";

                    btnXoa.Enabled = false;

                    txtTEN.Text = "";
                    txtSODIENTHOAI.Text = "";
                    txtDIACHI.Text = "";
                    txtNGANHANG.Text = "";
                    txtGHICHU.Text = "";
                    NGTID = "";
                    HOAHONGID = "";
                }
                else // không thành công
                {
                    tbao = "Xóa dữ liệu hoa hồng không thành công";
                }

                MessageBox.Show(tbao);

            }
            // {"func":"ajaxCALL_SP_S","params":["NGT01T001.NHAPHH","{\"act\":\"delete\",\"MABENHAN\":\"BA00001012\",\"TEN\":\"hung\",\"SODIENTHOAI\":\"0123456\"
            // ,\"DIACHI\":\"HN\",\"NGANHANG\":\"ck1\",\"GHICHU\":\"ko co gi\",\"NGTID\":\"181\",\"HOAHONGID\":\"181\",\"KHAMBENHID\":\"100415\"}"],"uuid":"bd575da2-56b9-49d6-a2f7-79e7545f079a"}

            // {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}
        }

    }
}