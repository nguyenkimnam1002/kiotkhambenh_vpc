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
    public partial class NGT02K008_Thongtin_Lichkham : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        string khambenhid = "";
        string benhnhanid = "";
        string hosobenhanid = "";
        DateTime ngaytiepnhan;
        string capnhat = "0"; // =1 là có cập nhật lên server;  capnhat= 0 là chỉ trả lại form cha

        public NGT02K008_Thongtin_Lichkham(string khambenhid, string benhnhanid, string hosobenhanid, DateTime ngaytiepnhan)
        {
            InitializeComponent();

            this.khambenhid = khambenhid;
            this.benhnhanid = benhnhanid;
            this.hosobenhanid = hosobenhanid;
            this.ngaytiepnhan = ngaytiepnhan;
            this.capnhat = "1";

            dtimeThoiGianHen.Focus();
            this.dtimeThoiGianHen.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtimeThoiGianHen.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtimeThoiGianHen.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtimeThoiGianHen.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
        }
        //capnhat= 0 là chỉ trả lại form cha
        public NGT02K008_Thongtin_Lichkham(DataTable dtData)
        {
            InitializeComponent();

            this.dtData = dtData;
            this.capnhat = "0";

            dtimeThoiGianHen.Focus();
        }

        private DataTable dtData = new DataTable();
        private void NGT02K008_Thongtin_Lichkham_Load(object sender, EventArgs e)
        {
            dtimeThoiGianHen.DateTime = ngaytiepnhan;

            DataTable dt = new DataTable();

            dt = RequestHTTP.get_ajaxExecuteQuery("NGTPK.003");
            if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
            DataRow dr = dt.NewRow();
            dr[0] = Const.local_phongId;
            dr[1] = Const.local_phong;
            dt.Rows.InsertAt(dr, 0);
            ucPhongKham.setData(dt, 0, 1);
            ucPhongKham.setColumn(0, false);
            ucPhongKham.SelectIndex = 0;

            if (dtData.Rows.Count == 0) dtData = RequestHTTP.get_ajaxExecuteQueryO("NGT02K008.RV001", khambenhid);
            if (dtData.Rows.Count > 0)
            {
                //[{"LICHHENID": "2643","THOIGIANLICHHEN": "14/04/2018  00:04:00","PHONGID": "4946","PDICHVUID": null,"LOIDANBACSI": "11"
                // ,"SOLUUTRU": "22","LIENLACVOI_BN": "33"}]
                //LICHHENID = dtData.Rows[0]["LICHHENID"].ToString();
                string ngay = dtData.Rows[0]["THOIGIANLICHHEN"].ToString().Replace("  ", " "); 
                //ngay = ngay.Substring(0, ngay.IndexOf(" "));
                dtimeThoiGianHen.DateTime = Func.ParseDatetime(ngay);
                ucPhongKham.SelectValue = dtData.Rows[0]["PHONGID"].ToString();
                txtLoiDan.Text = dtData.Rows[0]["LOIDANBACSI"].ToString();
                txtSoLuuTru.Text = dtData.Rows[0]["SOLUUTRU"].ToString();
                txtLienHe.Text = dtData.Rows[0]["LIENLACVOI_BN"].ToString();
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
            if (Func.getSysDatetime_Short() >= Func.getDatetime_Short(dtimeThoiGianHen.DateTime) )
            {
                MessageBox.Show("Thời gian hẹn phải lớn hơn ngày hiện tại");
                return;
            }

            if (dtData == null || dtData.Rows.Count == 0)
            {
                dtData = new DataTable();
                dtData.Columns.Add("LICHHENID");

                dtData.Columns.Add("THOIGIANLICHHEN");
                dtData.Columns.Add("PHONGID");
                dtData.Columns.Add("PDICHVUID");

                dtData.Columns.Add("LOIDANBACSI");
                dtData.Columns.Add("SOLUUTRU");
                dtData.Columns.Add("LIENLACVOI_BN");

                dtData.Rows.Add(""
                    , dtimeThoiGianHen.DateTime.ToString(Const.FORMAT_datetime1)
                    , ucPhongKham.SelectValue
                    , ""
                    , txtLoiDan.Text
                    , txtSoLuuTru.Text
                    , txtLienHe.Text
                    );
            }
            else
            {
                //dtData.Rows[0]["LICHHENID"].ToString()
                dtData.Rows[0]["THOIGIANLICHHEN"] = dtimeThoiGianHen.DateTime.ToString(Const.FORMAT_datetime1);
                dtData.Rows[0]["PHONGID"] = ucPhongKham.SelectValue;
                dtData.Rows[0]["PDICHVUID"] = "";
                dtData.Rows[0]["LOIDANBACSI"] = txtLoiDan.Text;
                dtData.Rows[0]["SOLUUTRU"] = txtSoLuuTru.Text;
                dtData.Rows[0]["LIENLACVOI_BN"] = txtLienHe.Text;
            }

            if (capnhat == "0")
            {
                if (ReturnData != null)
                {
                    ReturnData(dtData, null);
                    this.Close();
                }
            }
            else if (capnhat == "1")
            {
                // {"func":"ajaxCALL_SP_I","params":["NGT02K008.CNLH","{\"THOIGIANLICHHEN\":\"13/04/2018\",\"LOIDANBACSI\":\"11\",\"SOLUUTRU\":\"22\"
                // ,\"LIENLACVOI_BN\":\"33\",\"PHONGID\":\"4946\",\"BENHNHANID\":\"45884\",\"KHAMBENHID\":\"101116\",\"HOSOBENHANID\":\"118423\"}"]
                // ,"uuid":"4ec6a039-5dab-4aa7-989b-d478183b1424"}
                var json = new object();
                json = new
                {
                    THOIGIANLICHHEN = dtData.Rows[0]["THOIGIANLICHHEN"].ToString(),
                    LOIDANBACSI = dtData.Rows[0]["LOIDANBACSI"].ToString(),
                    SOLUUTRU = dtData.Rows[0]["SOLUUTRU"].ToString(),
                    LIENLACVOI_BN = dtData.Rows[0]["LIENLACVOI_BN"].ToString(),
                    PHONGID = dtData.Rows[0]["PHONGID"].ToString(),
                    BENHNHANID = benhnhanid,
                    KHAMBENHID = khambenhid,
                    HOSOBENHANID = hosobenhanid
                };
                string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K008.CNLH", Newtonsoft.Json.JsonConvert.SerializeObject(json).Replace("\"","\\\""));

                if (Func.Parse(ret) > 0)
                {
                    MessageBox.Show("Cập nhật thông tin hẹn khám thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin hẹn khám không thành công");
                }
            }


        }


    }
}