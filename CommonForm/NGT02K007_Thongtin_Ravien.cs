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
    public partial class NGT02K007_Thongtin_Ravien : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        DataTable dtData = new DataTable();
        public NGT02K007_Thongtin_Ravien(DataTable _dt, string MaBN, string Hoten, string Namsinh, string Nghenghiep, string Diachi)
        {
            InitializeComponent();

            this.dtData = _dt;
            //"RAVIENID": "893",
            //"PTHOIGIANRAVIEN": "",
            //"TINHTRANGNGUOIBENHRAVIEN": "",
            //"PHUONGPHAPDIEUTRI": "",
            //"HUONGDIEUTRITIEPTHEO": "",
            //"PTHOIGIANLICHHEN": "",
            //"LOIDANBACSI": ""}]

            //txtMaBN.Text = MaBN;
            //txtHoTen.Text = Hoten;
            //txtNamSinh.Text = Namsinh;
            //txtNgheNgiep.Text = Nghenghiep;
            //txtDiaChi.Text = Diachi;

            dtimeRaVien.Focus();
        }

        private void NGT02K007_Thongtin_Ravien_Load(object sender, EventArgs e)
        {
            if (dtData.Rows.Count == 0) return;

            if (dtData.Rows[0]["PTHOIGIANRAVIEN"] != null && dtData.Rows[0]["PTHOIGIANRAVIEN"].ToString() != "")
            {
                DateTime dtime = Func.ParseDatetime(dtData.Rows[0]["PTHOIGIANRAVIEN"].ToString());
                dtimeRaVien.DateTime = dtime;
            }
            if (dtData.Rows[0]["PTHOIGIANLICHHEN"] != null && dtData.Rows[0]["PTHOIGIANLICHHEN"].ToString() != "")
            {
                DateTime dtime = Func.ParseDatetime(dtData.Rows[0]["PTHOIGIANLICHHEN"].ToString());
                dtimeRaVien.DateTime = dtime;
            }

            if (dtData.Rows[0]["LOIDANBACSI"] != null) txtLoiDan.Text = dtData.Rows[0]["LOIDANBACSI"].ToString();
            if (dtData.Rows[0]["HUONGDIEUTRITIEPTHEO"] != null) txtDieuTriTiep.Text = dtData.Rows[0]["HUONGDIEUTRITIEPTHEO"].ToString();
            if (dtData.Rows[0]["PHUONGPHAPDIEUTRI"] != null) txtDieuTri.Text = dtData.Rows[0]["PHUONGPHAPDIEUTRI"].ToString();
            if (dtData.Rows[0]["TINHTRANGNGUOIBENHRAVIEN"] != null) txtTinhTrang.Text = dtData.Rows[0]["TINHTRANGNGUOIBENHRAVIEN"].ToString();
            
        }
        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtData == null || dtData.Rows.Count == 0)
            {
                dtData = new DataTable();
                dtData.Columns.Add("RAVIENID");
                dtData.Columns.Add("PTHOIGIANRAVIEN");
                dtData.Columns.Add("TINHTRANGNGUOIBENHRAVIEN");
                dtData.Columns.Add("PHUONGPHAPDIEUTRI");

                dtData.Columns.Add("HUONGDIEUTRITIEPTHEO");
                dtData.Columns.Add("PTHOIGIANLICHHEN");
                dtData.Columns.Add("LOIDANBACSI");

                dtData.Rows.Add(""
                    , dtimeRaVien.DateTime.ToString(Const.FORMAT_datetime1)
                    , txtTinhTrang.Text
                    , txtDieuTri.Text

                    , txtDieuTriTiep.Text
                    , dtimeThoiGianHen.DateTime.ToString(Const.FORMAT_datetime1)
                    , txtLoiDan.Text);
            }
            else
            {
                //dtData.Rows[0]["RAVIENID"].ToString()
                dtData.Rows[0]["PTHOIGIANRAVIEN"] = dtimeRaVien.DateTime.ToString(Const.FORMAT_datetime1);
                dtData.Rows[0]["TINHTRANGNGUOIBENHRAVIEN"] = txtTinhTrang.Text;
                dtData.Rows[0]["PHUONGPHAPDIEUTRI"] = txtDieuTri.Text;
                dtData.Rows[0]["HUONGDIEUTRITIEPTHEO"] = txtDieuTriTiep.Text;
                dtData.Rows[0]["PTHOIGIANLICHHEN"] = dtimeThoiGianHen.DateTime.ToString(Const.FORMAT_datetime1);
                dtData.Rows[0]["LOIDANBACSI"] = txtLoiDan.Text;
            }

            ReturnData(dtData, null);
            this.Close();
        }
        

    }
}