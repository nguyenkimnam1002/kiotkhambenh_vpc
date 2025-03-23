using System;
using System.Windows.Forms;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T002_xoaphieuketchuyen : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string PHIEUTHUID;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T002_xoaphieuketchuyen()
        {
            InitializeComponent();
        }

        public void SetData(string phieuThuId)
        {
            PHIEUTHUID = phieuThuId;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new
                {
                    PHIEUTHUID = PHIEUTHUID,
                    THOIGIANHUYPHIEU = Func.getSysDatetime(Const.FORMAT_datetime1),
                    HINHTHUCTHANHTOANHUY = "1",
                    LYDOHUYPHIEU = txtLyDoHuyPhieu.Text
                };

                string json = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
                string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T002.13", json);
                if ("1".Equals(fl))
                {
                    ReturnData(true, null);
                    this.Close();
                }
                else if ("0".Equals(fl))
                {
                    MessageBox.Show("Không được phép", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
                else if ("-1".Equals(fl))
                {
                    MessageBox.Show("Cập nhật không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
    }
}