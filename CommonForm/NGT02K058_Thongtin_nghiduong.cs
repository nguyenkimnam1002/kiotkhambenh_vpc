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
using System.IO;
using DevExpress.XtraPdfViewer;
using System.Drawing.Printing;
using DevExpress.Pdf;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K058_Thongtin_nghiduong : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        DataTable dt = new DataTable();
        private string KHAMBENHID = "";
        #endregion

        #region Init Form
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K058_Thongtin_nghiduong()
        {
            InitializeComponent();
        }
        public void setParam(string KHAMBENHID)
        {
            this.KHAMBENHID = KHAMBENHID;
            Form_Init();
            loadData();
        }
       
        private void Form_Init()
        {
            spSoNgayNghi.Value = 0;
            datTuNgay.DateTime = DateTime.Today;
            datDenNgay.DateTime = DateTime.Today;
        }
        #endregion

        #region Load dữ liệu
        private void loadData()
        {
            dt = RequestHTTP.get_ajaxExecuteQueryO("NGT02K058.LAYDS", KHAMBENHID);
            if (dt.Rows.Count > 0)
            {
                spSoNgayNghi.Value = int.Parse(dt.Rows[0]["SONGAY_NGHI"].ToString());
                datTuNgay.DateTime = Func.ParseDate(dt.Rows[0]["TUNGAY"].ToString());
                datDenNgay.DateTime = Func.ParseDate(dt.Rows[0]["DENNGAY"].ToString());

                txtDonVi.Text = dt.Rows[0]["DONVI_CONGTAC"].ToString();
                txtHoTenCha.Text = dt.Rows[0]["HOTENCHA"].ToString();
                txtHoTenMe.Text = dt.Rows[0]["HOTENME"].ToString();
            }
            else
            {
                spSoNgayNghi.Value = 0;
                datTuNgay.DateTime = DateTime.Today;
                datDenNgay.DateTime = DateTime.Today;

                txtDonVi.Text = "";
                txtHoTenCha.Text = "";
                txtHoTenMe.Text = "";
            }
        }
        #endregion

        #region Thêm, Xóa, Cập nhật
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            InPhieu();
        }
       
        private void InPhieu()
        {
            if (KHAMBENHID == "")
            {
                MessageBox.Show("Hãy chọn bệnh nhân muốn in");
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
                table.Rows.Add("khambenhid", "String", KHAMBENHID);

                Print("", "RPT_GIAYNGHI_BHXH", table, "pdf");
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void Print(string title, string code, DataTable parTable, string type, int height = 650, int width = 900)
        {
            string url = Func.getUrlReport(code, parTable, type);
            Func.SaveFileFromUrl(url, code);

            PrintPreview(title, code, parTable);
        }

        private void PrintPreview(string title, string code, DataTable parTable, int height = 650, int width = 900)
        {

            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(title, code, parTable, height, width);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_validate()) return;

                string json = "";
                json += Func.json_item("DONVI_CONGTAC", txtDonVi.Text);
                json += Func.json_item("SONGAY_NGHI", spSoNgayNghi.Value.ToString());
                json += Func.json_item("TUNGAY", datTuNgay.DateTime.ToString(Const.FORMAT_date1));
                json += Func.json_item("DENNGAY", datDenNgay.DateTime.ToString(Const.FORMAT_date1));
                json += Func.json_item("HOTENCHA", txtHoTenCha.Text);
                json += Func.json_item("HOTENME", txtHoTenMe.Text);
                json += Func.json_item("ID", "");
                json += Func.json_item("KHAMBENHID", KHAMBENHID);
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"");

                string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K058.INSERT", json);
                if (ret == "1")
                {
                    loadData();
                    XtraMessageBox.Show("Cập nhật thông tin nghỉ hưởng BHXH thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Cập nhật thông tin nghỉ hưởng BHXH không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion
        
        #region Hàm
        private bool _validate()
        {
            if (txtDonVi.Text == ""){
                XtraMessageBox.Show("Hãy nhập đơn vị bệnh nhân đang công tác", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonVi.Focus();
                return false;
            }

            if (spSoNgayNghi.Value == 0)
            {
                XtraMessageBox.Show("Hãy nhập số ngày nghỉ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                spSoNgayNghi.Focus();
                return false;
            }

            if (datDenNgay.DateTime == null)
            {
                XtraMessageBox.Show("Hãy nhập từ ngày", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                datDenNgay.Focus();
                return false;
            }

            if (datDenNgay.DateTime == null)
            {
                XtraMessageBox.Show("Hãy nhập đến ngày", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                datDenNgay.Focus();
                return false;
            }
            return true;
        }
        private void TinhDenNgay()
        {
            decimal SoNgay = spSoNgayNghi.Value;
            datDenNgay.DateTime = datTuNgay.DateTime.AddDays((double)SoNgay);
        }
        #endregion

        #region Change, Keydown
        private void spSoNgayNghi_EditValueChanged(object sender, EventArgs e)
        {
            TinhDenNgay();
        }

        private void txtDonVi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                spSoNgayNghi.Focus();
            }
        }

        private void spSoNgayNghi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                datTuNgay.Focus();
            }
        }

        private void datTuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHoTenCha.Focus();
            }
        }

        private void txtHoTenCha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHoTenMe.Focus();
            }
        }

        private void txtHoTenMe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLuu.Focus();
            }
        }
        #endregion
    }
}