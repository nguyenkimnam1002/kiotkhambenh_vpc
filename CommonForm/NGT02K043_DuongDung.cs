using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K043_DuongDung : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        string hdHUONGDANTHUCHIEN = "";
        int rowId = 0;
        DataTable dtCachDung = new DataTable();
        public NGT02K043_DuongDung(DataTable _dtCachDung, int rowId, string hdHUONGDANTHUCHIEN)
        {
            InitializeComponent();

            this.hdHUONGDANTHUCHIEN = hdHUONGDANTHUCHIEN; // 2@Uống@_param_huongdan@4@2@0@0@0
            this.dtCachDung = _dtCachDung.Copy();
            this.rowId = rowId;
        }

        string txtTGSD = "";
        string txtSearchCD = "";
        string txtDONVI = "";
        private void NGT02K043_DuongDung_Load(object sender, EventArgs e)
        {
            if (dtCachDung == null || dtCachDung.Rows.Count == 0) dtCachDung = Func.getTableEmpty(new string[] { "col1", "col2" });
            if (dtCachDung.Rows.Count > 0 && dtCachDung.Rows[0]["col2"].ToString().Trim() == "") dtCachDung.Rows.RemoveAt(0);
            cbo_DuongDung.setData(dtCachDung, 0, 1);
            cbo_DuongDung.setColumnAll(false);
            cbo_DuongDung.setColumn(1, true);
            cbo_DuongDung.setEvent(updateCACHDUNG);

            string[] hdsd = hdHUONGDANTHUCHIEN.Split('@');

            if (hdsd.Length > 7)
            {
                txtNGAY.Text = hdsd[0];
                cbo_DuongDung.SelectText = hdsd[1];
                txtCACHDUNG.Text = hdsd[2];
                txtSOLUONG.Text = hdsd[3];
                txtSANG.Text = hdsd[4];
                txtTRUA.Text = hdsd[5];
                txtCHIEU.Text = hdsd[6];
                txtTOI.Text = hdsd[7];
                if (hdsd.Length > 8) txtTGSD = hdsd[8];
                if (hdsd.Length > 9) txtSearchCD = hdsd[9];
                if (hdsd.Length > 10) txtDONVI = hdsd[10];
            }
            allowUpdateCACHDUNG = true;

        }
        bool allowUpdateCACHDUNG = false;
        private void updateCACHDUNG(object sender, EventArgs e)
        {
            if (allowUpdateCACHDUNG == false) return;
            string _duongdung = cbo_DuongDung.SelectText;
            string _sang = txtSANG.Text.Trim();
            string _trua = txtTRUA.Text.Trim();
            string _chieu = txtCHIEU.Text.Trim();
            string _toi = txtTOI.Text.Trim();

            string hdsd = "";
            hdsd += _duongdung == "" ? "" : _duongdung;
            hdsd += txtTGSD == "" ? "" :  ", "+txtTGSD ;
            hdsd += _sang == "0" ? "" : ", Sáng " + _sang;
            hdsd += _trua == "0" ? "" : ", Trưa " + _trua ;
            hdsd += _chieu == "0" ? "" : ", Chiều " + _chieu;
            hdsd += _toi == "0" ? "" : ", Tối " + _toi;
            hdsd += ".";

            txtCACHDUNG.Text = hdsd;
        }

        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            double _total = 0;
            string _text = "";
            float _tong_lan = 0;
            string _text_duongdung = cbo_DuongDung.SelectText;

            float _sang = Func.Parse_float(txtSANG.Text);
            float _trua = Func.Parse_float(txtTRUA.Text);
            float _chieu = Func.Parse_float(txtCHIEU.Text);
            float _toi = Func.Parse_float(txtTOI.Text);

            if (_sang > 0) _tong_lan++;
            else txtSANG.Text = "0";

            if (_trua > 0) _tong_lan++;
            else txtTRUA.Text = "0";

            if (_chieu > 0) _tong_lan++;
            else txtCHIEU.Text = "0";

            if (_toi > 0) _tong_lan++;
            else txtTOI.Text = "0";

            float _trung_binh = (_sang + _trua + _chieu + _toi) / _tong_lan;

            if (txtNGAY.Text != "" && txtNGAY.Text != "0")
            {
                _total = Math.Round((_sang + _trua + _chieu + _toi) * Func.Parse_float(txtNGAY.Text));

                _text = txtNGAY.Text + "@" + _text_duongdung + "@" + txtCACHDUNG.Text + "@" + _total + "@" + _sang + "@" + _trua + "@" + _chieu + "@" + _toi;
            }
            else
            {
                _total = Func.Parse_double(txtSOLUONG.Text);
                _text = _text_duongdung + "@" + txtCACHDUNG.Text + "@" + _total + "@@@@";
            }

            _text += "@" + txtTGSD + "@" + txtSearchCD + "@" + txtDONVI;

            string[] list = new string[10];
            list[0] = _text;
            list[1] = rowId.ToString();
            list[2] = _text_duongdung;
            list[3] = txtCACHDUNG.Text;

            if (ReturnData != null) ReturnData(list, null);
            //EventUtil.raiseEvent("saveHDSD",{text:_text, rowid:_opts.rowid, duongdung:_text_duongdung, soluong:_total});
            this.Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSANG_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số, cả số thập phân
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
        }

        private void txtTRUA_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số, cả số thập phân
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
        }

        private void txtCHIEU_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số, cả số thập phân
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
        }

        private void txtTOI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số, cả số thập phân
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !e.KeyChar.Equals('.');
        }

        private void txtSANG_EditValueChanged(object sender, EventArgs e)
        {
            updateCACHDUNG(sender, e);
        }

        private void txtTRUA_EditValueChanged(object sender, EventArgs e)
        {
            updateCACHDUNG(sender, e);
        }

        private void txtCHIEU_EditValueChanged(object sender, EventArgs e)
        {
            updateCACHDUNG(sender, e);
        }

        private void txtTOI_EditValueChanged(object sender, EventArgs e)
        {
            updateCACHDUNG(sender, e);
        }
    }
}