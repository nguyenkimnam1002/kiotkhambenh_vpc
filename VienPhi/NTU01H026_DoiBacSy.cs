using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H026_DoiBacSy : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string KHOAID;
        private static string MAUBENHPHAMID;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public void SetData(string khoaId, string mauBenhPhamId)
        {
            KHOAID = khoaId;
            MAUBENHPHAMID = mauBenhPhamId;
        }

        public NTU01H026_DoiBacSy()
        {
            InitializeComponent();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NTU01H026_DoiBacSy_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable khoaChiDinhDT = RequestHTTP.get_ajaxExecuteQueryO("COM.KHOACHUCNANG",
                new string[] { "[0]", "[1]", "[2]" },
                new string[] { "-1", "4", "-1" });

                cbbKhoaChiDinh.setData(khoaChiDinhDT, 0, 1);
                cbbKhoaChiDinh.lookUpEdit.Properties.Columns[0].Visible = false;
                cbbKhoaChiDinh.SelectValue = KHOAID;
                cbbKhoaChiDinh.setEvent_Enter(cbb_KeyEnter);
                cbbKhoaChiDinh.setEvent(cbo_SelectedIndexChanged);
                cbo_SelectedIndexChanged(null, null);
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
                DataTable bacSiDT = RequestHTTP.get_ajaxExecuteQueryO("NTU01H024.EV004",
                    new string[] { "[0]"},
                    new string[] { KHOAID });

                if (bacSiDT.Rows.Count <= 0)
                {
                    bacSiDT.Columns.Add("");
                    bacSiDT.Columns.Add("");
                }

                if (bacSiDT.Rows.Count >= 0)
                {
                    DataRow dr = bacSiDT.NewRow();
                    dr[0] = string.Empty;
                    dr[1] = "Chọn";
                    bacSiDT.Rows.InsertAt(dr, 0);
                }

                cbbBSChiDinh.setData(bacSiDT, 0, 1);
                cbbBSChiDinh.lookUpEdit.Properties.Columns[0].Visible = false;
                cbbBSChiDinh.SelectIndex = 0;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void cbb_KeyEnter(object sender, EventArgs e)
        {
            cbbBSChiDinh.Focus();
        }

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cbbBSChiDinh.SelectValue))
                {
                    MessageBox.Show("Chưa chọn bác sĩ", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                var par = MAUBENHPHAMID + "$" + cbbBSChiDinh.SelectValue;
                var result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H024.EV003", par);
                if ("1".Equals(result))
                {
                    DialogResult dialogResult = MessageBox.Show("Chuyển bác sỹ chỉ định thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    if (dialogResult == DialogResult.OK)
                    {
                        ReturnData(true, null);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Chuyển bác sỹ chỉ định không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }
    }
}