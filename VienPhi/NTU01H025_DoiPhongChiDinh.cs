using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H025_DoiPhongChiDinh : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string TITLE;
        private static string MAUBENHPHAMID;
        private static string TYPE;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public void SetData(string title, string mauBenhPhamId, string type)
        {
            TITLE = title;
            MAUBENHPHAMID = mauBenhPhamId;
            TYPE = type;
        }

        public NTU01H025_DoiPhongChiDinh()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NTU01H025_DoiPhongChiDinh_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = TITLE;

                DataTable khoaChiDinhDT = RequestHTTP.get_ajaxExecuteQueryO("COM.KHOACHUCNANG",
                new string[] { "[0]", "[1]", "[2]" },
                new string[] { "-1", "4", "-1" });

                cbbKhoaChiDinh.setData(khoaChiDinhDT, 0, 1);
                cbbKhoaChiDinh.lookUpEdit.Properties.Columns[0].Visible = false;
                cbbKhoaChiDinh.SelectIndex = 0;
                cbbKhoaChiDinh.setEvent_Enter(cbb_KeyEnter);
                cbbKhoaChiDinh.setEvent(cbo_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
            
        }

        private void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable phongChiDinhDT = RequestHTTP.get_ajaxExecuteQueryO("NT.0011",
                    new string[] { "[0]", "[1]", "[2]", "[3]", "[4]" },
                    new string[] { "5", "3", cbbKhoaChiDinh.SelectValue, "ORG_ID", "PARENT_ID" });

                if (phongChiDinhDT.Rows.Count <= 0)
                {
                    phongChiDinhDT.Columns.Add("");
                    phongChiDinhDT.Columns.Add("");
                }

                if (phongChiDinhDT.Rows.Count >= 0)
                {
                    DataRow dr = phongChiDinhDT.NewRow();
                    dr[0] = string.Empty;
                    dr[1] = "Chọn";
                    phongChiDinhDT.Rows.InsertAt(dr, 0);
                }

                cbbPhongChiDinh.setData(phongChiDinhDT, 0, 1);
                cbbPhongChiDinh.lookUpEdit.Properties.Columns[0].Visible = false;
                cbbPhongChiDinh.SelectIndex = 0;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cbbKhoaChiDinh.SelectValue))
                {
                    MessageBox.Show("Chưa chọn khoa", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                if (string.IsNullOrWhiteSpace(cbbPhongChiDinh.SelectValue))
                {
                    MessageBox.Show("Chưa chọn phòng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }

                string par = MAUBENHPHAMID + "$" + cbbKhoaChiDinh.SelectValue + "$" + cbbPhongChiDinh.SelectValue + "$" + TYPE;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NGT02K002.LUUKB", par);
                if ("1".Equals(result))
                {
                    if ("0".Equals(TYPE))
                    {
                        DialogResult dialogResult = MessageBox.Show("Chuyển khoa phòng chỉ định thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        if (dialogResult == DialogResult.OK)
                        {
                            ReturnData(true, null);
                            this.Close();
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Chuyển khoa phòng thực hiện thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        if (dialogResult == DialogResult.OK)
                        {
                            ReturnData(true, null);
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chuyển khoa phòng chỉ định không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void cbb_KeyEnter(object sender, EventArgs e)
        {
            cbbPhongChiDinh.Focus();
        }

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
    }
}