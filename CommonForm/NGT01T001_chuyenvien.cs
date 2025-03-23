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
    public partial class NGT01T001_chuyenvien : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT01T001_chuyenvien()
        {
            InitializeComponent();
        }
        private void Init_Form()
        {
            dtChuyenVien = new DataTable();
            dtChuyenVien.Columns.Add("ucBenhVien");
            dtChuyenVien.Columns.Add("ucCDTD");
            dtChuyenVien.Columns.Add("ucHinhThucChuyen");
            dtChuyenVien.Columns.Add("ucLyDoChuyen");
            dtChuyenVien.Columns.Add("rbtChuyen");
            dtChuyenVien.Rows.Add("", "", "", "", "");

            DataTable dt = new DataTable();

            dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_NoiDKKCB); // ds BV chuyen đến
            ucBenhVien.setData(dt, "BENHVIENKCBBD", "TENBENHVIEN");
            ucBenhVien.setColumn("RN", -1, "", 0);
            ucBenhVien.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 35);
            ucBenhVien.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
            ucBenhVien.setColumn("DIACHI", 2, "Địa chỉ", 0);

            ucCDTD.btnReset.Visible = true;
            ucCDTD.btnReset.Text = "Xóa";
            dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
            ucCDTD.setData(dt, "ICD10CODE", "ICD10NAME");
            ucCDTD.setEvent_Check(ucCDTD_Check);
            ucCDTD.setColumn("RN", -1, "", 0);
            ucCDTD.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucCDTD.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV002, "82"); // Hình thức chuyển viện 
            ucHinhThucChuyen.setData(dt, 0, 1);
            ucHinhThucChuyen.setColumn(0, false);
            ucHinhThucChuyen.SelectIndex = 0;

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV002, "84"); //LyDo ChuyenVien 
            ucLyDoChuyen.setData(dt, 0, 1);
            ucLyDoChuyen.setColumn(0, false);
            ucLyDoChuyen.SelectIndex = 0;
        }

        bool bInit = true;
        private void NGT01T001_chuyenvien_Load(object sender, EventArgs e)
        {
            if (bInit)
            {
                Init_Form();
                bInit = false;
            }

            if (dtChuyenVien.Rows[0]["ucBenhVien"].ToString() != "")
                ucBenhVien.SelectedValue = dtChuyenVien.Rows[0]["ucBenhVien"].ToString();

            if (dtChuyenVien.Rows[0]["ucCDTD"].ToString() != "")
                ucCDTD.SelectedText = dtChuyenVien.Rows[0]["ucCDTD"].ToString();
            if (dtChuyenVien.Rows[0]["ucHinhThucChuyen"].ToString() != "")
                ucHinhThucChuyen.SelectValue = dtChuyenVien.Rows[0]["ucHinhThucChuyen"].ToString();
            if (dtChuyenVien.Rows[0]["ucLyDoChuyen"].ToString() != "")
                ucLyDoChuyen.SelectValue = dtChuyenVien.Rows[0]["ucLyDoChuyen"].ToString();

            if (dtChuyenVien.Rows[0]["rbtChuyen"].ToString() != "")
                rbtChuyenDungTuyen.EditValue = dtChuyenVien.Rows[0]["rbtChuyen"].ToString();
        }

        private void ucCDTD_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (ucCDTD.SelectedText.IndexOf(drv["ICD10CODE"].ToString() + "-" + drv["ICD10NAME"].ToString()) > -1)
                {
                    ucCDTD.messageError = "Chuẩn đoán này đã được nhập.";
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        DataTable dtChuyenVien;
        public void setData(DataTable _dtChuyenVien)
        {
            dtChuyenVien = _dtChuyenVien;
        }
        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ucBenhVien.SelectedIndex < 0)
            {
                MessageBox.Show("Yêu cầu nhập thông tin chuyển từ viện", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ucBenhVien.Focus();
                return;
            }
            if (ucCDTD.SelectedText.Trim() =="")
            {
                MessageBox.Show("Yêu cầu nhập thông tin chẩn đoán tuyến dưới", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                ucCDTD.Focus();
                return;
            }

            if (ReturnData != null)
            {
                dtChuyenVien.Rows[0]["ucBenhVien"] = ucBenhVien.SelectedValue;
                dtChuyenVien.Rows[0]["ucCDTD"] = ucCDTD.SelectedText;
                dtChuyenVien.Rows[0]["ucHinhThucChuyen"] = ucHinhThucChuyen.SelectValue;
                dtChuyenVien.Rows[0]["ucLyDoChuyen"] = ucLyDoChuyen.SelectValue;
                dtChuyenVien.Rows[0]["rbtChuyen"] = rbtChuyenDungTuyen.EditValue;

                ReturnData(dtChuyenVien, null);
            }
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}