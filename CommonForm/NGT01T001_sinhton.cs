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
using System.Text.RegularExpressions;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT01T001_sinhton : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
              log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string KHAMBENHID = "";
        private string TRANGTHAIKHAMBENH = "";
        DataTable dt = new DataTable();
        #endregion

        #region Init Form
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT01T001_sinhton()
        {
            InitializeComponent();
        } 
        public void setParam(string KHAMBENHID, string TRANGTHAIKHAMBENH)
        {
            this.KHAMBENHID = KHAMBENHID;
            this.TRANGTHAIKHAMBENH = TRANGTHAIKHAMBENH;

            initControl();
        }
        
        private void initControl()
        {
            if (this.TRANGTHAIKHAMBENH != "1")
            {
                btnLuu.Enabled = false;
                btnXoa.Enabled = false;
            }
            loadSinhTon();
            txtMach.Focus();
            ChiSo_BMI();
        }

        //Load dữ liệu lên control
        private void loadSinhTon()
        {
            dt = RequestHTTP.get_ajaxExecuteQueryO("NGT01T001.SINHTON", KHAMBENHID);
            if (dt.Rows.Count > 0)
            {
                txtMach.Text = dt.Rows[0]["KHAMBENH_MACH"].ToString();
                txtNhietDo.Text = dt.Rows[0]["KHAMBENH_NHIETDO"].ToString();
                txtHuyetApThap.Text = dt.Rows[0]["KHAMBENH_HUYETAP_LOW"].ToString();
                txtHuyetApCao.Text = dt.Rows[0]["KHAMBENH_HUYETAP_HIGH"].ToString();
                txtNhipTho.Text = dt.Rows[0]["KHAMBENH_NHIPTHO"].ToString();
                txtCanNang.Text = dt.Rows[0]["KHAMBENH_CANNANG"].ToString();
                txtChieuCao.Text = dt.Rows[0]["KHAMBENH_CHIEUCAO"].ToString();
            }
            else
            {
                clearControl();
            }
        }
        #endregion
        
        #region Thêm, Xóa, Cập nhật
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_validate()) return;

                string json = "";
                json += Func.json_item("KHAMBENH_MACH", txtMach.Text);
                json += Func.json_item("KHAMBENH_NHIETDO", txtNhietDo.Text);
                json += Func.json_item("KHAMBENH_HUYETAP_LOW", txtHuyetApThap.Text);
                json += Func.json_item("KHAMBENH_HUYETAP_HIGH", txtHuyetApCao.Text);
                json += Func.json_item("KHAMBENH_NHIPTHO", txtNhipTho.Text);
                json += Func.json_item("KHAMBENH_CANNANG", txtCanNang.Text);
                json += Func.json_item("KHAMBENH_CHIEUCAO", txtChieuCao.Text);
                json += Func.json_item("KHAMBENHID", KHAMBENHID);
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"");

                string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT01T001.ST_UPD", json);
                if (ret == "1")
                {
                    //loadSinhTon();
                    XtraMessageBox.Show("Cập nhật sinh tồn thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Cập nhật sinh tồn không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string json = "";
                json += Func.json_item("KHAMBENHID", KHAMBENHID);
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"");

                string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT01T001.ST_UPD", json);
                if (ret == "1")
                {
                    clearControl();
                    XtraMessageBox.Show("Xóa sinh tồn thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    XtraMessageBox.Show("Xóa sinh tồn không thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Hàm
        //Kiểm tra Lưu
        private bool _validate()
        {
            if (txtMach.Text == "")
            {
                XtraMessageBox.Show("Hãy nhập thông tin mạch", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMach.Focus();
                return false;
            }
            if (txtNhietDo.Text == "")
            {
                XtraMessageBox.Show("Hãy nhập thông tin nhiệt độ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhietDo.Focus();
                return false;
            }
            if (txtNhipTho.Text == "")
            {
                MessageBox.Show("Hãy nhập thông tin nhịp thở", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhipTho.Focus();
                return false;
            }
            if (txtHuyetApThap.Text == "")
            {
                XtraMessageBox.Show("Hãy nhập thông tin huyết áp", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHuyetApThap.Focus();
                return false;
            }
            if (txtHuyetApCao.Text == "")
            {
                XtraMessageBox.Show("Hãy nhập thông tin huyết áp", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHuyetApCao.Focus();
                return false;
            }
            if (txtNhipTho.Text == "")
            {
                XtraMessageBox.Show("Hãy nhập thông tin nhịp thở", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNhipTho.Focus();
                return false;
            }
            return true;
        }
        
        //Clear Các control
        private void clearControl()
        {
            txtMach.Text = "";
            txtNhietDo.Text = "";
            txtHuyetApThap.Text = "";
            txtHuyetApCao.Text = "";
            txtNhipTho.Text = "";
            txtCanNang.Text = "";
            txtChieuCao.Text = "";
        }

        //Tính chỉ số BMI
        private void ChiSo_BMI()
        {
            if (txtCanNang.Text != "" && txtChieuCao.Text != "")
            {
                try
                {
                    float chieucao = float.Parse(txtChieuCao.Text.Trim());
                    float cannang = float.Parse(txtCanNang.Text.Trim());
                    float bmi = Func.BMI(cannang, chieucao);
                    lblBMI.Text = bmi.ToString("n2") + " " + Func.BMI_Mess(bmi);
                }
                catch { }
            }
        }

        #endregion

        #region Change, Keydown
        private void txtCanNang_EditValueChanged(object sender, EventArgs e)
        {
            ChiSo_BMI();
        }

        private void txtChieuCao_EditValueChanged(object sender, EventArgs e)
        {
            ChiSo_BMI();
        }

        private void txtMach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNhietDo.Focus();
            }
        }

        private void txtNhietDo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNhipTho.Focus();
            }
        }

        private void txtNhipTho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHuyetApThap.Focus();
            }
        }

        private void txtHuyetApThap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHuyetApCao.Focus();
            }
        }

        private void txtHuyetApCao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCanNang.Focus();
            }
        }

        private void txtCanNang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChieuCao.Focus();
            }
        }

        private void txtChieuCao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLuu.Focus();
            }
        }
        #endregion
    }
}