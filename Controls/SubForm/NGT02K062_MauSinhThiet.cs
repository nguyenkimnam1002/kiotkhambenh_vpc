using System;
using System.Data;
using System.Windows.Forms;
using Newtonsoft.Json;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NGT02K062_MauSinhThiet : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string dichvukhambenhid = string.Empty;
        private string maubenhphamid = string.Empty;
        private string tendichvu = string.Empty;

        // Item an tren web
        private string txtDICHVUID = string.Empty;// không thấy sử dụng
        private string txtST_ID = string.Empty;
        private string txtKHAMBENHID = string.Empty;// không thấy sử dụng
        private string txtPHONGID = string.Empty;
        private string txtKHOAID = string.Empty;

        private string resultCheck = "1";

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K062_MauSinhThiet()
        {
            InitializeComponent();
        }

        public void setParam(string dichvukhambenhid, string maubenhphamid, string tendichvu)
        {
            this.dichvukhambenhid = dichvukhambenhid;
            this.maubenhphamid = maubenhphamid;
            this.tendichvu = tendichvu;

            txtTENDICHVU.Text = tendichvu;
            getData();
        }

        private void getData()
        {
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NGT02K062.LAYCT", maubenhphamid + "$" + dichvukhambenhid }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                DataTable dt = new DataTable();
                ret.result = ret.result.Trim();
                //dt = Func.fill_ArrayStr_To_Datatable(ret.result, "NGT02K062.LAYCT");
                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();

                setObjectToForm(dt);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void NGT02K062_MauSinhThiet_Load(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if ("0" == txtST_ID || string.IsNullOrEmpty(txtST_ID))
                {
                    MessageBox.Show("Dịch vụ này chưa có sinh thiết. ");
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa mẫu sinh thiết này ? ", "", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    deleteData();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void deleteData()
        {
            try
            {
                resultCheck = RequestHTTP.call_ajaxCALL_SP_I("NGT02K062.XOA", txtST_ID);

                if (resultCheck == "1")
                {
                    MessageBox.Show("Xóa thông tin sinh thiết thành công ");
                    clearForm();
                    txtST_ID = "0";
                    //$("#txtDICHVUKHAMBENHID").val(opt.dichvukhambenhid);
                    txtTENDICHVU.Text = tendichvu;
                }
                else if (resultCheck == "2")
                {
                    MessageBox.Show("Mẫu sinh thiết đã được sử dụng, không được xóa. ");
                }
                else if (resultCheck == "3")
                {
                    MessageBox.Show("Thông tin sinh thiết đã bị xóa, yêu cầu kiểm tra lại. ");
                }
                else
                {
                    MessageBox.Show("Lỗi xóa thông tin sinh thiết. ");
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void clearForm()
        {
            txtTENDICHVU.Text = string.Empty;
            txtST_LAYTU.Text = string.Empty;
            txtCDBANGDD.Text = string.Empty;
            txtDAUHIEU_CLS.Text = string.Empty;
            txtQT_DIEUTRI.Text = string.Empty;
            txtNX_KHILAYST.Text = string.Empty;
            txtKQST_LANTRUOC.Text = string.Empty;
            txtCHANDOAN_LAMSANG.Text = string.Empty;
            txtST_LAYTU.Focus();
        }

        private void setObjectToForm(DataTable dt)
        {
            try
            {
                //txtTENDICHVU.Text = string.Empty;
                txtST_LAYTU.Text = dt.Rows[0]["ST_LAYTU"].ToString();
                txtCDBANGDD.Text = dt.Rows[0]["CDBANGDD"].ToString();
                txtDAUHIEU_CLS.Text = dt.Rows[0]["DAUHIEU_CLS"].ToString();
                txtQT_DIEUTRI.Text = dt.Rows[0]["QT_DIEUTRI"].ToString();
                txtNX_KHILAYST.Text = dt.Rows[0]["NX_KHILAYST"].ToString();
                txtKQST_LANTRUOC.Text = dt.Rows[0]["KQST_LANTRUOC"].ToString();
                txtCHANDOAN_LAMSANG.Text = dt.Rows[0]["CHANDOAN_LAMSANG"].ToString();

                // set value item an tren web
                txtDICHVUID = string.Empty;
                txtST_ID = dt.Rows[0]["ST_ID"].ToString();
                txtKHAMBENHID = string.Empty;
                txtPHONGID = dt.Rows[0]["PHONGID"].ToString();
                txtKHOAID = dt.Rows[0]["KHOAID"].ToString();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                var data = setFormToObject();
                if (data != null)
                {
                    resultCheck = RequestHTTP.call_ajaxCALL_SP_I("NGT02K062.SUA", JsonConvert.SerializeObject(data).Replace("\"", "\\\""));
                    if (resultCheck != "-1")
                    {
                        MessageBox.Show("Lưu thông tin sinh thiết thành công ");
                        txtST_ID = resultCheck;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi lưu thông tin sinh thiết ");
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private object setFormToObject()
        {
            var data = new
            {
                DICHVUID = txtDICHVUID,
                ST_ID = txtST_ID,
                KHAMBENHID = txtKHAMBENHID,
                PHONGID = txtPHONGID,
                KHOAID = txtKHOAID,
                TENDICHVU = txtTENDICHVU.Text,
                ST_LAYTU = txtST_LAYTU.Text,
                CDBANGDD = txtCDBANGDD.Text,
                DAUHIEU_CLS = txtDAUHIEU_CLS.Text,
                QT_DIEUTRI = txtQT_DIEUTRI.Text,
                NX_KHILAYST = txtNX_KHILAYST.Text,
                KQST_LANTRUOC = txtKQST_LANTRUOC.Text,
                CHANDOAN_LAMSANG = txtCHANDOAN_LAMSANG.Text,
                DICHVUKHAMBENHID = dichvukhambenhid
            };

            return data;
        }

        private void btnInphieu_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(dichvukhambenhid))
            {
                return;
            }

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("dichvukhambenhid", "String", dichvukhambenhid);

                frmPrint frm = new frmPrint("In phiếu", "PHIEU_SINHTHIET_A4_965", table);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }

        }
    }
}
