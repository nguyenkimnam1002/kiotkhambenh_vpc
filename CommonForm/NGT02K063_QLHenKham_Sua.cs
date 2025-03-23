using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K063_QLHenKham_Sua : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K063_QLHenKham_Sua()
        {
            InitializeComponent();
        }

        private string data;
        public void loadData(string data)
        {
            try
            {
                this.data = data;
                ucBacSiKham.Focus();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NGT02K063_QLHenKham_Sua_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_BacSyKham);
                DataRow dr = dt.NewRow();
                dr["col1"] = string.Empty;
                dr["col2"] = "Chọn bác sĩ khám";
                dt.Rows.InsertAt(dr, 0);
                ucBacSiKham.setData(dt, 0, 1);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateData())
                {
                    string ngayHen = dEditNgayHen.DateTime.ToString(Const.FORMAT_datetime1);
                    string bacSiKham = ucBacSiKham.SelectValue;
                    string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K061.SUAHK", ngayHen + "$" + bacSiKham + "$" + data);
                    if ("1".Equals(ret))
                    {
                        if (ReturnData != null)
                        {
                            this.Close();
                            ReturnData(true, null);
                        }
                    }
                    else
                        MessageBox.Show("Xảy ra lỗi !", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }

        private bool ValidateData()
        {
            DateTime dateTime;
            if (string.IsNullOrWhiteSpace(dEditNgayHen.Text))
            {
                MessageBox.Show("Hãy nhập ngày hẹn khám", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (!DateTime.TryParseExact(dEditNgayHen.Text, Const.FORMAT_datetime1, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                MessageBox.Show("Ngày hẹn khám không đúng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (dEditNgayHen.DateTime < Func.getSysDatetime())
            {
                MessageBox.Show("Thời gian hẹn khám không được nhỏ hơn thời gian hiện tại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(ucBacSiKham.SelectValue))
            {
                MessageBox.Show("Hãy nhập bác sĩ khám", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
    }
}