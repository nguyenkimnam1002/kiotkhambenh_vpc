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
    public partial class NGT02K010_Tuvong : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        DataTable dtTuVong = new DataTable();
        public NGT02K010_Tuvong(DataTable _dtTuVong, string MaBN, string Hoten, string Namsinh, string Nghenghiep, string Diachi)
        {
            InitializeComponent();

            this.dtTuVong = _dtTuVong;
            //[{ "TUVONGID": "255",
            //"TUVONGLUC": null,
            //"THOIGIANTUVONGID": null,
            //"ISCOKHAMNGHIEM": "0",
            //"NGUYENNHANTUVONGID": null,
            //"NGUYENNHANTUVONGCHINH": "",
            //"CHANDOANGIAIPHAUTUTHI": ""}]

            txtMaBN.Text = MaBN;
            txtHoTen.Text = Hoten;
            txtNamSinh.Text = Namsinh;
            txtNgheNgiep.Text = Nghenghiep;
            txtDiaChi.Text = Diachi;

            dtimeTuvong.Focus();
        }

        private void NGT02K010_Tuvong_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (dtTuVong.Rows.Count == 0 || dtTuVong.Rows[0]["TUVONGLUC"] == null || dtTuVong.Rows[0]["TUVONGLUC"].ToString() == "")
            { 
                dtimeTuvong.DateTime = Func.getSysDatetime();
            }

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV002, "50"); //ThoiGianTuVong
            ucThoigian.setData(dt, 0, 1);
            ucThoigian.SelectIndex = 0;

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV002, "81");// NguyenNhan_TuVong
            ucNguyennhan.setData(dt, 0, 1);
            ucNguyennhan.SelectIndex = 0;

            if (dtTuVong.Rows.Count > 0)
            {
                if (dtTuVong.Rows[0]["TUVONGLUC"] != null && dtTuVong.Rows[0]["TUVONGLUC"].ToString() != "")
                    try
                    {
                        DateTime temp = Func.ParseDatetime(dtTuVong.Rows[0]["TUVONGLUC"].ToString());
                        dtimeTuvong.DateTime = temp;
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                if (dtTuVong.Rows[0]["THOIGIANTUVONGID"] != null)
                    ucThoigian.SelectValue = dtTuVong.Rows[0]["THOIGIANTUVONGID"].ToString();

                if (dtTuVong.Rows[0]["NGUYENNHANTUVONGID"] != null)
                    ucNguyennhan.SelectValue = dtTuVong.Rows[0]["NGUYENNHANTUVONGID"].ToString();

                if (dtTuVong.Rows[0]["ISCOKHAMNGHIEM"] != null)
                    ckbCoKhamNghiem.Checked = dtTuVong.Rows[0]["ISCOKHAMNGHIEM"].ToString() == "1";

                if (dtTuVong.Rows[0]["NGUYENNHANTUVONGCHINH"] != null)
                    txtNguyenNhanChinh.Text = dtTuVong.Rows[0]["NGUYENNHANTUVONGCHINH"].ToString();


                if (dtTuVong.Rows[0]["CHANDOANGIAIPHAUTUTHI"] != null)
                    txtGiaiPhau.Text = dtTuVong.Rows[0]["CHANDOANGIAIPHAUTUTHI"].ToString();
            }
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
            if (dtTuVong == null || dtTuVong.Rows.Count == 0)
            {
                dtTuVong = new DataTable();
                dtTuVong.Columns.Add("TUVONGID");
                dtTuVong.Columns.Add("TUVONGLUC");
                dtTuVong.Columns.Add("THOIGIANTUVONGID");
                dtTuVong.Columns.Add("ISCOKHAMNGHIEM");

                dtTuVong.Columns.Add("NGUYENNHANTUVONGID");
                dtTuVong.Columns.Add("NGUYENNHANTUVONGCHINH");
                dtTuVong.Columns.Add("CHANDOANGIAIPHAUTUTHI");

                dtTuVong.Rows.Add("", dtimeTuvong.DateTime.ToString(Const.FORMAT_datetime1), ucThoigian.SelectValue, ckbCoKhamNghiem.EditValue.ToString()
                    , ucNguyennhan.SelectValue, txtNguyenNhanChinh.Text, txtGiaiPhau.Text);
            }
            else
            {
                //dtPopup_Tuvong.Rows[0]["TUVONGID"].ToString()
                dtTuVong.Rows[0]["TUVONGLUC"] = dtimeTuvong.DateTime.ToString(Const.FORMAT_datetime1);
                dtTuVong.Rows[0]["THOIGIANTUVONGID"] = ucThoigian.SelectValue;
                dtTuVong.Rows[0]["ISCOKHAMNGHIEM"] = ckbCoKhamNghiem.Checked ? "1" : "0";
                dtTuVong.Rows[0]["NGUYENNHANTUVONGID"] = ucNguyennhan.SelectValue;
                dtTuVong.Rows[0]["NGUYENNHANTUVONGCHINH"] = txtNguyenNhanChinh.Text;
                dtTuVong.Rows[0]["CHANDOANGIAIPHAUTUTHI"] = txtGiaiPhau.Text;
            }

            ReturnData(dtTuVong, null);
            this.Close();
        }
        

    }
}