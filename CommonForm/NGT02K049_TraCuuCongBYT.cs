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
    public partial class NGT02K049_TraCuuCongBYT : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private string MABHYT;
        private string TENBENHNHAN;
        private string NGAYSINH;
        private string QRCODE;
        private string GIOITINH;
        private DateTime? TUNGAY;
        private DateTime? DENNGAY; 
        // private string BYTURL; biến này đã khai trong class serviceBYT rồi
        private string CHEDO;
        private string i_u;
        private string i_p;
        private string MACSYT;

        public NGT02K049_TraCuuCongBYT()
        {
            InitializeComponent();
        }

        public void setParam(string _MABHYT, string _TENBENHNHAN, string _NGAYSINH, string _QRCODE, string _GIOITINH, DateTime? _TUNGAY, DateTime? _DENNGAY)
        {
            MABHYT = _MABHYT;
            TENBENHNHAN = _TENBENHNHAN;
            NGAYSINH = _NGAYSINH;
            QRCODE = _QRCODE;
            GIOITINH = _GIOITINH;
            TUNGAY = _TUNGAY;
            DENNGAY = _DENNGAY;

            ServiceBYT.Lay_thong_tin_ws_BYT();
            i_u = ServiceBYT.ServiceBYT_Username;
            i_p = ServiceBYT.ServiceBYT_Password;
            MACSYT = ServiceBYT.ServiceBYT_MACSYT;            
        }

        private void NGT02K049_TraCuuCongBYT_Load(object sender, EventArgs e)
        {
            try
            {
                initializeComboBox();
                CHEDO = string.IsNullOrEmpty(MABHYT) ? "0" : "1";

                ucGrid_ThongTin.clearData();

                //GridUtil.init(_grd_LichSuKCB, "100%", "330px", _title, false, _colHeader, true);
                setUcGrid_ThongTin(1, null);
                NGT02K049_ucGrid_ThongTin_Load(null, null);
                //bindEvent();
                              
                if (CHEDO == "1")
                {
                    txtMaThe.Text = MABHYT;
                    txtLayTuNgay.DateTime = TUNGAY == null ? DateTime.Now : TUNGAY.GetValueOrDefault();
                    txtLayDenNgay.DateTime = DENNGAY == null ? DateTime.Now : DENNGAY.GetValueOrDefault();
                    txtNgaySinh.DateTime = string.IsNullOrEmpty(NGAYSINH) ? DateTime.Now : DateTime.ParseExact(NGAYSINH, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ucCbbGioiTinh.Text = GIOITINH;
                    //doLoadGrid();
                }
                else
                {
                    txtMaThe.Text = "";
                    txtLayTuNgay.DateTime = DateTime.Now;
                    txtLayDenNgay.DateTime = DateTime.Now;
                    txtNgaySinh.DateTime = DateTime.Now;
                    ucCbbGioiTinh.Text = "";
                    ucGrid_ThongTin.clearData();
                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void initializeComboBox()
        {
            try
            {
                DataTable dt = new DataTable();

                DataColumn colName = new DataColumn("Name");
                DataColumn colValue = new DataColumn("value");
                dt.Columns.Add(colName);
                dt.Columns.Add(colValue);

                DataRow row = dt.NewRow();
                row = dt.NewRow();
                row[0] = "1";
                row[1] = "Nam";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row[0] = "2";
                row[1] = "Nữ";
                dt.Rows.Add(row);

                ucCbbGioiTinh.setData(dt, 0, 1);
                ucCbbGioiTinh.setColumnAll(false);
                ucCbbGioiTinh.setColumn(1, true);
                ucCbbGioiTinh.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        //private void setValue_ucCbbGioiTinh(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //loadDataGrid(1, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }
        //}


        private void NGT02K049_ucGrid_ThongTin_Load(object sender, EventArgs e)
        {
            ucGrid_ThongTin.gridView.OptionsBehavior.Editable = false;
            ucGrid_ThongTin.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_ThongTin.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
            ucGrid_ThongTin.gridView.OptionsView.ShowAutoFilterRow = true;
            ucGrid_ThongTin.Set_HidePage(true);
        }

        //private string GetNGAY(string ngay)
        //{
        //    // 31/12/2018 => 20181231
        //    if (string.IsNullOrEmpty(ngay))
        //    {
        //        return ngay;
        //    }

        //    return ngay.Substring(6, 10)
        //            + ngay.Substring(3, 5)
        //            + ngay.Substring(0, 2);
        //}

        private string ConvertToVNFormatDate(string ngay)
        {
            if (ngay == "") { return ngay; }
            try
            {
                // 20181231 => 31/12/2018
                if (ngay.Length == 8)
                {
                    return ngay.Substring(6, 8)
                            + "/" + ngay.Substring(4, 6)
                            + "/" + ngay.Substring(0, 4);
                }
                return ngay.Substring(6, 8)
                        + "/" + ngay.Substring(4, 6)
                        + "/" + ngay.Substring(0, 4)
                        + " " + ngay.Substring(8, 10)
                        + ":" + ngay.Substring(10, 12);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return "";
        }

        private string GetTinhTrangVaoVien(string TinhTrang)
        {
            var StringTinhTrang = string.Empty;
            try
            {
                if (TinhTrang == "1")
                {
                    StringTinhTrang = "Đúng tuyến";
                }
                else if (TinhTrang == "2")
                {
                    StringTinhTrang = "Cấp cứu";
                }
                else if (TinhTrang == "3")
                {
                    StringTinhTrang = "Trái tuyến";
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return StringTinhTrang;
        }


        //private object createObjecttc_ls_KCB(
        //    string macsyt,
        //    string mathe,
        //    string ngaysinh,
        //    string gioitinh,
        //    string laytungay,
        //    string laydenngay)
        //{
        //    var objtc_ls_KCB = new
        //    {
        //        Sender_Code = macsyt,
        //        Sender_Name = "",
        //        Transaction_Type = "M0007",
        //        Transaction_Name = "",
        //        Transaction_Date = "",
        //        MA_THE = mathe,
        //        MABENHVIEN = macsyt,
        //        NGAY_SINH = ngaysinh,
        //        GIOI_TINH = gioitinh,
        //        LAYTUNGAY = laytungay,
        //        LAYDENNGAY = laydenngay
        //    };

        //    return objtc_ls_KCB;
        //}

        private void setUcGrid_ThongTin(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    ucGrid_ThongTin.clearData();

                    var objtc_ls_KCB = new
                    {
                        MA_THE = txtMaThe.Text,
                        MABENHVIEN = MACSYT,
                        NGAY_SINH = txtNgaySinh.DateTime.ToString("yyyyMMdd"),
                        GIOI_TINH = ucCbbGioiTinh.SelectValue,
                        LAYTUNGAY = txtLayTuNgay.DateTime.ToString("yyyyMMdd"),
                        LAYDENNGAY = txtLayDenNgay.DateTime.ToString("yyyyMMdd")
                    };

                    var objHeader = new
                    {
                        Message_Version = "1.0",
                        Sender_Code = MACSYT,
                        Sender_Name = "",
                        Transaction_Type = "M0007",
                        Transaction_Name = "",
                        Transaction_Date = "",
                        Transaction_ID = "",
                        Request_ID = "",
                        //Action_Type = objjj.Action_Type              // 0: bắt đầu khám, 1: kết thúc khám
                    };

                    var obj3 = ServiceBYT.XML_BYT_TaoKhung(objHeader, objtc_ls_KCB, "7");

                    var resultCongBYT = ServiceBYT.tc_ls_KCB("CongDLYTWS", "tc_ls_KCB",
                            new String[] {
                            "_usr",
                            "_pwd",
                            "xmlData" },
                            new String[] {
                            i_u,
                            i_p,
                            Convert.ToBase64String(Encoding.UTF8.GetBytes(obj3.ToString())) });

                    var rets = resultCongBYT.Split(';');

                    DataTable _khambenh = null;
                    if ("0" == rets[0])
                    {
                        _khambenh = MyJsonConvert.toDataTable(rets[2]);

                        for (var i = 0; i < _khambenh.Rows.Count; i++)
                        {
                            _khambenh.Rows[i]["NGAYGIOKHAM"] = ConvertToVNFormatDate(_khambenh.Rows[i]["NGAYGIOKHAM"].ToString());
                            _khambenh.Rows[i]["NGAYGIORA"] = ConvertToVNFormatDate(_khambenh.Rows[i]["NGAYGIORA"].ToString());
                            _khambenh.Rows[i]["NGAYGIOVAO"] = ConvertToVNFormatDate(_khambenh.Rows[i]["NGAYGIOVAO"].ToString());
                            _khambenh.Rows[i]["NGAYHETTHUOC"] = ConvertToVNFormatDate(_khambenh.Rows[i]["NGAYHETTHUOC"].ToString());
                            _khambenh.Rows[i]["TINHTRANGVAOVIEN"] = GetTinhTrangVaoVien(_khambenh.Rows[i]["TINHTRANGVAOVIEN"].ToString());
                        }
                    }
                    else
                    {
                        //MessageBox.Show(rets[1]);
                        ucGrid_ThongTin.clearData();
                    }

                    if (_khambenh == null || _khambenh.Rows.Count == 0) _khambenh = Func.getTableEmpty(new String[]
                    {
                        "MA_LK", "MABENHVIEN", "MA_BENH", "TEN_BENH", "MA_BENHKHAC", "MA_LOAI_KCB", "NGAYGIOKHAM",
                        "NGAYGIOVAO", "NGAYGIORA", "NGAYHETTHUOC", "SOKHAMBENH", "TINHTRANGVAOVIEN", "CHANDOAN", "LYDODENKHAM"
                    });

                    ucGrid_ThongTin.setData(_khambenh, 1, 1);
                    ucGrid_ThongTin.setColumnAll(false);
                    ucGrid_ThongTin.setColumn("MA_LK", 0, "Mã lượt khám", 0);
                    ucGrid_ThongTin.setColumn("MABENHVIEN", 1, "Mã bệnh viện", 0);
                    ucGrid_ThongTin.setColumn("MA_BENH", 2, "Mã bệnh", 0);
                    ucGrid_ThongTin.setColumn("TEN_BENH", 3, "Tên bệnh", 0);
                    ucGrid_ThongTin.setColumn("MA_BENHKHAC", 4, "Mã bệnh khác", 0);
                    ucGrid_ThongTin.setColumn("MA_LOAI_KCB", 5, "Mã loại KCB", 0);
                    ucGrid_ThongTin.setColumn("NGAYGIOKHAM", 6, "Ngày giờ khám", 0);
                    ucGrid_ThongTin.setColumn("NGAYGIOVAO", 7, "Ngày vào", 0);
                    ucGrid_ThongTin.setColumn("NGAYGIORA", 8, "Ngày ra", 0);
                    ucGrid_ThongTin.setColumn("NGAYHETTHUOC", 9, "Ngày hết thuốc", 0);
                    ucGrid_ThongTin.setColumn("SOKHAMBENH", 10, "Số khám bệnh", 0);
                    ucGrid_ThongTin.setColumn("TINHTRANGVAOVIEN", 11, "Tình trạng", 0);
                    ucGrid_ThongTin.setColumn("CHANDOAN", 12, "Chẩn đoán", 0);
                    ucGrid_ThongTin.setColumn("LYDODENKHAM", 13, "Lý do", 0);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaThe.Text))
            {
                MessageBox.Show("Chưa nhập thông tin mã thẻ");
                txtMaThe.Focus();
            }
            if (string.IsNullOrEmpty(txtNgaySinh.Text))
            {
                MessageBox.Show("Chưa nhập ngày sinh / năm sinh");
                txtNgaySinh.Focus();
            }
            if (txtLayTuNgay.DateTime == null || txtLayDenNgay.DateTime == null)
            {
                MessageBox.Show("Chưa nhập ngày sinh / năm sinh");
            }
            setUcGrid_ThongTin(1, null);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaThe_KeyUp(object sender, KeyEventArgs e)
        {
            var MaThe = txtMaThe.Text.Trim();
            MaThe = MaThe.ToUpper();
            txtMaThe.Text = MaThe;
        }
    }
}
