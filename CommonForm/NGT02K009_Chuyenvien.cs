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
using System.Globalization;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K009_Chuyenvien : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        DataTable dtData = new DataTable();
        string KhamBenhID = "";
        DateTime dtimeNGAYTIEPNHAN;
        public NGT02K009_Chuyenvien(DataTable _dtData, string MaBN, string Hoten, string Namsinh, string Nghenghiep, string Diachi
            , string _KhamBenhID, DateTime _dtimeNGAYTIEPNHAN)
        {
            InitializeComponent();
             
            this.dtData = _dtData;
            this.KhamBenhID = _KhamBenhID;
           

            txtMaBN.Text = MaBN;
            txtHoTen.Text = Hoten;
            txtNamSinh.Text = Namsinh;
            txtNgheNgiep.Text = Nghenghiep;
            txtDiaChi.Text = Diachi;

            dtimeNGAYTIEPNHAN = _dtimeNGAYTIEPNHAN;

            dtimeTHOIGIANCHUYENVIEN.Focus();
        }

        private void NGT02K009_Chuyenvien_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_NoiDKKCB); // ds BV chuyen đến
            ucChuyenDenVien.setData(dt, "BENHVIENKCBBD", "TENBENHVIEN");
            ucChuyenDenVien.setEvent(ucChuyenDenVien_SelectedIndexChanged);
            ucChuyenDenVien.textEdit1.ReadOnly = true;
            ucChuyenDenVien.setColumn("RN", -1, "", 0);
            ucChuyenDenVien.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 35);
            ucChuyenDenVien.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
            ucChuyenDenVien.setColumn("DIACHI", 2, "Địa chỉ", 0);

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV003, "84"); //  Tuyến chuyển viện 
            ucTuyen.setData(dt, 0, 1);
            ucTuyen.setColumn(0, false);
            ucTuyen.SelectIndex = 0;

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV002, "84"); //LyDo ChuyenVien 
            ucLyDoChuyen.setData(dt, 0, 1);
            ucLyDoChuyen.setColumn(0, false);
            ucLyDoChuyen.SelectIndex = 0;

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV003, "82"); // Hình thức chuyển viện 
            ucHinhThuc.setData(dt, 0, 1);
            ucHinhThuc.setColumn(0, false);
            ucHinhThuc.SelectIndex = 0;

            if (dtData.Rows.Count == 0 || dtData.Rows[0]["THOIGIANCHUYENVIEN"] == null || dtData.Rows[0]["THOIGIANCHUYENVIEN"].ToString() == "")
            { 
                dtimeTHOIGIANCHUYENVIEN.DateTime = Func.getSysDatetime();
            }

            if (dtData.Rows.Count == 0) return;

            if (dtData.Rows[0]["THOIGIANCHUYENVIEN"] != null && dtData.Rows[0]["THOIGIANCHUYENVIEN"].ToString() != "")
                try
                {
                    DateTime temp = Func.ParseDatetime(dtData.Rows[0]["THOIGIANCHUYENVIEN"].ToString());
                    dtimeTHOIGIANCHUYENVIEN.DateTime = temp;
                }
                catch (Exception ex) { log.Fatal(ex.ToString()); }

            if (dtData.Rows[0]["MABENHVIENCHUYENDEN"] != null && dtData.Rows[0]["MABENHVIENCHUYENDEN"].ToString() != "")
                ucChuyenDenVien.SelectedValue = dtData.Rows[0]["MABENHVIENCHUYENDEN"].ToString();

            if (dtData.Rows[0]["DIACHIBV"] != null) txtDIACHIBV.Text = dtData.Rows[0]["DAUHIEULAMSANG"].ToString();
            
            if (dtData.Rows[0]["DAUHIEULAMSANG"] != null) txtDAUHIEULAMSANG.Text = dtData.Rows[0]["DAUHIEULAMSANG"].ToString();
            if (dtData.Rows[0]["XETNGHIEM"] != null) txtXETNGHIEM.Text = dtData.Rows[0]["XETNGHIEM"].ToString();            
            if (dtData.Rows[0]["CHANDOAN"] != null) txtCHANDOAN.Text = dtData.Rows[0]["CHANDOAN"].ToString();
            
            if (dtData.Rows[0]["THUOC"] != null) txtTHUOC.Text = dtData.Rows[0]["THUOC"].ToString();            
            if (dtData.Rows[0]["TINHTRANGNGUOIBENH"] != null) txtTINHTRANGNGUOIBENH.Text = dtData.Rows[0]["TINHTRANGNGUOIBENH"].ToString();            
            if (dtData.Rows[0]["HUONGDIEUTRI"] != null) txtHUONGDIEUTRI.Text = dtData.Rows[0]["HUONGDIEUTRI"].ToString();
            
            if (dtData.Rows[0]["PHUONGTIENVANCHUYEN"] != null) txtPHUONGTIENVANCHUYEN.Text = dtData.Rows[0]["PHUONGTIENVANCHUYEN"].ToString();
            if (dtData.Rows[0]["NGUOIVANCHUYEN"] != null) txtNGUOIVANCHUYEN.Text = dtData.Rows[0]["NGUOIVANCHUYEN"].ToString();
            //if (dtData.Rows[0]["CHUYENVIENDEN"] != null) txtCHUYENVIENDEN.Text = dtData.Rows[0]["CHUYENVIENDEN"].ToString();
         
            if (dtData.Rows[0]["HINHTHUCID"] != null && dtData.Rows[0]["HINHTHUCID"].ToString() != "")
                ucHinhThuc.SelectValue = dtData.Rows[0]["HINHTHUCID"].ToString();

            if (dtData.Rows[0]["LYDOID"] != null && dtData.Rows[0]["LYDOID"].ToString() != "")
                ucLyDoChuyen.SelectValue = dtData.Rows[0]["LYDOID"].ToString();

            if (dtData.Rows[0]["LOAIID"] != null && dtData.Rows[0]["LOAIID"].ToString() != "")
                ucTuyen.SelectValue = dtData.Rows[0]["LOAIID"].ToString(); 
        }
        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
        private void ucChuyenDenVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDIACHIBV.Text = ucChuyenDenVien.SelectedDataRowView["DIACHI"].ToString();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtimeTHOIGIANCHUYENVIEN.EditValue.ToString() =="")
            {
		        MessageBox.Show("Hãy nhập thời gian chuyển viện");
                return;
            }
            if (ucChuyenDenVien.SelectedValue == "")
            {
		        MessageBox.Show("Hãy chọn viện chuyển đến");
                return;
            }
            if (dtimeNGAYTIEPNHAN > dtimeTHOIGIANCHUYENVIEN.DateTime)
            {
			    MessageBox.Show("Ngày chuyển viện không hợp lệ, hãy chọn ngày chuyển viện lớn hơn ngày khám bệnh");
                return;
            }

            if (dtData == null || dtData.Rows.Count == 0)
            {
                dtData = new DataTable();
                dtData.Columns.Add("CHUYENVIENID");

                dtData.Columns.Add("THOIGIANCHUYENVIEN");
                dtData.Columns.Add("MABENHVIENCHUYENDEN");
                dtData.Columns.Add("DIACHIBV");
                
                dtData.Columns.Add("DAUHIEULAMSANG");
                dtData.Columns.Add("XETNGHIEM");
                dtData.Columns.Add("CHANDOAN");

                dtData.Columns.Add("THUOC");
                dtData.Columns.Add("TINHTRANGNGUOIBENH");
                dtData.Columns.Add("HUONGDIEUTRI");
                  
                dtData.Columns.Add("PHUONGTIENVANCHUYEN");
                dtData.Columns.Add("NGUOIVANCHUYEN");
                dtData.Columns.Add("CHUYENVIENDEN");

                dtData.Columns.Add("HINHTHUCID");
                dtData.Columns.Add("LYDOID");
                dtData.Columns.Add("LOAIID");

                dtData.Rows.Add(""
                    , dtimeTHOIGIANCHUYENVIEN.DateTime.ToString(Const.FORMAT_datetime1), ucChuyenDenVien.SelectedValue, txtDIACHIBV.Text
                    , txtDAUHIEULAMSANG.Text, txtXETNGHIEM.Text, txtCHANDOAN.Text
                    , txtTHUOC.Text, txtTINHTRANGNGUOIBENH.Text, txtHUONGDIEUTRI.Text
                    , txtPHUONGTIENVANCHUYEN.Text, txtNGUOIVANCHUYEN.Text, ucChuyenDenVien.SelectedText
                    , ucHinhThuc.SelectValue, ucLyDoChuyen.SelectValue, ucTuyen.SelectValue
                    );
            }
            else
            {
                //dtData.Rows[0]["CHUYENVIENID"].ToString()

                dtData.Rows[0]["THOIGIANCHUYENVIEN"] = dtimeTHOIGIANCHUYENVIEN.DateTime.ToString(Const.FORMAT_datetime1);
                dtData.Rows[0]["MABENHVIENCHUYENDEN"] = ucChuyenDenVien.SelectedValue;
                dtData.Rows[0]["DIACHIBV"] = txtDIACHIBV.Text;

                dtData.Rows[0]["DAUHIEULAMSANG"] = txtDAUHIEULAMSANG.Text;
                dtData.Rows[0]["XETNGHIEM"] = txtXETNGHIEM.Text;
                dtData.Rows[0]["CHANDOAN"] = txtCHANDOAN.Text;

                dtData.Rows[0]["THUOC"] = txtTHUOC.Text;
                dtData.Rows[0]["TINHTRANGNGUOIBENH"] = txtTINHTRANGNGUOIBENH.Text;
                dtData.Rows[0]["HUONGDIEUTRI"] = txtHUONGDIEUTRI.Text;

                dtData.Rows[0]["PHUONGTIENVANCHUYEN"] = txtPHUONGTIENVANCHUYEN.Text;
                dtData.Rows[0]["NGUOIVANCHUYEN"] = txtNGUOIVANCHUYEN.Text;
                dtData.Rows[0]["CHUYENVIENDEN"] = ucChuyenDenVien.SelectedText;

                dtData.Rows[0]["HINHTHUCID"] = ucHinhThuc.SelectValue;
                dtData.Rows[0]["LYDOID"] = ucLyDoChuyen.SelectValue;
                dtData.Rows[0]["LOAIID"] = ucTuyen.SelectValue;
            }

            ReturnData(dtData, null);
            this.Close();
        }
        

    }
}