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
    public partial class NGT02K047_LichSuKCB : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        string ma_the = "";
        string ho_ten = "";
        string ngay_sinh = "";
        //string ma_CSKCB = "";
        string gioi_tinh = "";
        string chedo = "";

        public NGT02K047_LichSuKCB(string ma_the, string ho_ten, string ngay_sinh, string gioi_tinh, string ma_CSKCB, string ngay_bd, string ngay_kt)
        {
            InitializeComponent();

            this.ma_the = ma_the;
            this.ho_ten = ho_ten;
            this.ngay_sinh = ngay_sinh;
            this.gioi_tinh = gioi_tinh;
            //this.ma_CSKCB = ma_CSKCB;
            chedo = ma_the == "" ? "0" : "1";


        }

        private void NGT02K047_LichSuKCB_Load(object sender, EventArgs e)
        {
            //txtMaThe.Text = ma_the;
            //txtHoTen.Text = ho_ten;
            //txtNgaySinh.Text = ngay_sinh;

            //ucDS.gridView.OptionsView.ColumnAutoWidth = true;
            ucDS.gridView.OptionsView.ShowAutoFilterRow = true;
            ucDS.Set_Caption("Thông tin lịch sử KCB");
            ucDS.setEvent_DoubleClick(ucDS_DoubleClick);
            ucDS.Set_HidePage(false);
            ucDS.onIndicator();

            ucGrid_DSNguoiXem.gridView.OptionsView.ShowAutoFilterRow = true;
            ucGrid_DSNguoiXem.Set_Caption("Thông tin người dùng tra cứu Cổng BHXH");
             
            if (chedo == "1")
            {
                txtMaThe.Text = ma_the;
                txtHoTen.Text = ho_ten;
                txtNgaySinh.Text = ngay_sinh;
                getHistory010917();
            }
            else
            {
                txtMaThe.Text = "";
                txtHoTen.Text = "";
                txtNgaySinh.Text = "";
                clear2DanhSach();
            }
        }

        private void ucDS_DoubleClick(object sender, EventArgs e)
        {
            DataRowView dataRow = ucDS.SelectedRow;
            if (dataRow != null)
            {
                NGT02K048_LichSuKCB_CT frm = new NGT02K048_LichSuKCB_CT();
                frm.setParam(dataRow["MAHOSO"].ToString());
                // frm.setReturnData(ReturnData_NGT02K019_DonThuocCu);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        private void clear2DanhSach()
        {
            ucDS.clearData();
            ucGrid_DSNguoiXem.clearData();
        }

        private void getHistory010917()
        {
            var msg = string.Empty;
            try
            { 

                DataTable dt = new DataTable();

                //ret1 = _checkCongBHYT(i_u, i_p, i_mabhyt, i_tenbenhnhan, i_ngaysinh, i_gioitinh, i_macskcb, "", "", "1");			// cominf
                wsBHYT_LichSu_respons_2018 LS = ServiceBHYT.Get_History010118(
                    txtMaThe.Text,
                    txtHoTen.Text,
                    txtNgaySinh.Text
                    );
                // {"maKetQua":"090","hoTen":"Nguyễn Hồng Hải","gioiTinh":"Nam","diaChi":"11 trệt nguyễn duy dương,p8,q5","maDKBD":"79014","cqBHXH":"Bảo hiểm Xã hội quận 5","gtTheTu":"01/01/2017","gtTheDen":"31/12/2021","maKV":"","ngayDu5Nam":"01/12/2015",
                // "dsLichSuKCB":[{"maHoSo":535595540,"maCSKCB":"79014","tuNgay":"28/09/2017","denNgay":"28/09/2017","tenBenh":"E11....;","tinhTrang":"1","kqDieuTri":"1"},{"maHoSo":443409000,"maCSKCB":"79014","tuNgay":"03/05/2017","denNgay":"03/05/2017","tenBenh":"J00 -  - Viêm mũi họng cấp [cảm thường]","tinhTrang":"","kqDieuTri":""}]}

                if (LS == null)
                {
                    MessageBox.Show("Không có dữ liệu lịch sử KCB. Yêu cầu kiểm tra thông tin đầu vào.");
                    clear2DanhSach();
                    return;
                }

                if (LS.dsLichSuKT2018 != null)
                {
                    var thoiGianKT = string.Empty;
                    for (int i = 0; i < LS.dsLichSuKT2018.Count; i++)
                    {
                        thoiGianKT = string.IsNullOrEmpty(LS.dsLichSuKT2018[i].thoiGianKT) ? "" : LS.dsLichSuKT2018[i].thoiGianKT.Substring(6, 2)
                                                        + "/" + LS.dsLichSuKT2018[i].thoiGianKT.Substring(4, 2)
                                                        + "/" + LS.dsLichSuKT2018[i].thoiGianKT.Substring(0, 4)
                                                        + " " + LS.dsLichSuKT2018[i].thoiGianKT.Substring(8, 2)
                                                        + ":" + LS.dsLichSuKT2018[i].thoiGianKT.Substring(10, 2);
                        LS.dsLichSuKT2018[i].thoiGianKT = thoiGianKT;
                    }
                }

                if (LS.dsLichSuKCB2018 != null)
                {
                    msg = Func.LayNoiDungLoiCheckBHYT(LS.maKetQua, "1");
                    var str = string.Empty;
                    var tmp = string.Empty;
                    var tmp1 = string.Empty;
                    var ngayVao = string.Empty;
                    var ngayRa = string.Empty;
                    for (int i = 0; i < LS.dsLichSuKCB2018.Count; i++)
                    {
                        str = string.Empty;
                        tmp = string.Empty;
                        tmp1 = string.Empty;
                        ngayVao = string.Empty;
                        ngayRa = string.Empty;

                        str = LS.dsLichSuKCB2018[i].tenBenh + "";
                        LS.dsLichSuKCB2018[i].tenBenh = str;
                        // lay ra ket qua dieu tri;
                        tmp = LS.dsLichSuKCB2018[i].kqDieuTri;
                        LS.dsLichSuKCB2018[i].kqDieuTri = getKetQuaDT(tmp);
                        // lay ra tinh trang ra vien;
                        tmp1 = LS.dsLichSuKCB2018[i].tinhTrang;
                        LS.dsLichSuKCB2018[i].tinhTrang = getTinhTrang(tmp1);

                        ngayVao = string.IsNullOrEmpty(LS.dsLichSuKCB2018[i].ngayVao) ? "" : LS.dsLichSuKCB2018[i].ngayVao.Substring(6, 2)
                                                        + "/" + LS.dsLichSuKCB2018[i].ngayVao.Substring(4, 2)
                                                        + "/" + LS.dsLichSuKCB2018[i].ngayVao.Substring(0, 4)
                                                        + " " + LS.dsLichSuKCB2018[i].ngayVao.Substring(8, 2)
                                                        + ":" + LS.dsLichSuKCB2018[i].ngayVao.Substring(10, 2);
                        LS.dsLichSuKCB2018[i].ngayVao = ngayVao;

                        ngayRa = string.IsNullOrEmpty(LS.dsLichSuKCB2018[i].ngayRa) ? "" : LS.dsLichSuKCB2018[i].ngayRa.Substring(6, 2)
                                                        + "/" + LS.dsLichSuKCB2018[i].ngayRa.Substring(4, 2)
                                                        + "/" + LS.dsLichSuKCB2018[i].ngayRa.Substring(0, 4)
                                                        + " " + LS.dsLichSuKCB2018[i].ngayRa.Substring(8, 2)
                                                        + ":" + LS.dsLichSuKCB2018[i].ngayRa.Substring(10, 2);
                        LS.dsLichSuKCB2018[i].ngayRa = ngayRa;

                        var data_ar = RequestHTTP.call_ajaxCALL_SP_O("NGT02K047.GETCSYT", LS.dsLichSuKCB2018[i].maCSKCB, 0);
                        if (data_ar != null && data_ar.Rows.Count > 0)
                        {
                            LS.dsLichSuKCB2018[i].tenCSKCB = data_ar.Rows[0]["ORG_NAME"].ToString();
                        }
                        else
                        {
                            LS.dsLichSuKCB2018[i].tenCSKCB = "Không xác định";
                        }
                    }

                    // do data vao grid ucDS
                    dt = LS.dsLichSuKCB2018.ConvertListToDataTable<wsBHYT_LichSu_respons_2018_detail>();
                    //dt.Columns.Add("ls_chitiet");
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "maHoSo", "maCSKCB", "tuNgay", "denNgay", "tenBenh", "tinhTrang", "kqDieuTri", "tinhTrang" });

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    dt.Rows[i]["kqDieuTri"] = getKetQuaDT(dt.Rows[i]["kqDieuTri"].ToString());
                    //    dt.Rows[i]["tinhTrang"] = getTinhTrang(dt.Rows[i]["tinhTrang"].ToString());
                    //}
                    ucDS.setData(dt, 1, 1);
                    ucDS.setColumnAll(false);
                    ucDS.setColumn("maHoSo", 0, "Số Hồ sơ");
                    ucDS.setColumn("tenBenh", 1, "Tên bệnh");
                    ucDS.setColumn("ngayVao", 2, "Ngày vào viện");
                    ucDS.setColumn("ngayRa", 3, "Ngày ra viện");
                    ucDS.setColumn("tenCSKCB", 4, "Tên CSKCB");
                    ucDS.setColumn("tinhTrang", 5, "Tình trạng");
                    ucDS.setColumn("tinhTrang", 6, "Tình trạng");
                    ucDS.setColumn("kqDieuTri", 7, "Kết quả Đ.Trị");
                    //ucDS.setColumnButtonImage("ls_chitiet", "Zoom.png", ucDS_DoubleClick);

                    // do data vao grid ucGrid_DSNguoiXem
                    dt = LS.dsLichSuKT2018.ConvertListToDataTable<wsBHYT_LichSuKT_respons_2018_detail>();
                    //dt.Columns.Add("ls_chitiet");
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "userKT", "thoiGianKT", "maLoi", "thongBao" });

                    ucGrid_DSNguoiXem.setData(dt, 1, 1);
                    ucGrid_DSNguoiXem.setColumnAll(false);
                    ucGrid_DSNguoiXem.setColumn("userKT", 0, "User tra cứu", 0);
                    ucGrid_DSNguoiXem.setColumn("thoiGianKT", 1, "Thời gian", 0);
                    ucGrid_DSNguoiXem.setColumn("maLoi", 2, "Mã lỗi", 0);
                    ucGrid_DSNguoiXem.setColumnMemoEdit("thongBao", 3, "Ghi chú", 0);
                }
                else
                {
                    if (string.IsNullOrEmpty(msg))
                    {
                        msg = Func.LayNoiDungLoiCheckBHYT(LS.maKetQua, "1");
                    }
                    MessageBox.Show("Không có dữ liệu KCB ứng với thông tin đầu vào này.");
                    ucDS.clearData();
                }

                msg = "";
                msg += Func.LayNoiDungLoiCheckBHYT(LS.maKetQua, "0");
                msg += LS.hoTen == null || LS.hoTen == "" ? "" : "Họ tên: " + LS.hoTen + ",";
                msg += LS.ngaySinh == null || LS.ngaySinh == "" ? "" : "Ngày sinh: " + LS.ngaySinh + ",";
                msg += LS.gioiTinh == null || LS.gioiTinh == "" ? "" : "Giới tính : " + LS.gioiTinh + ",";
                msg += LS.diaChi == null || LS.diaChi == "" ? "" : "(ĐC: " + LS.diaChi + "),";
                msg += LS.maDKBD == null || LS.maDKBD == "" ? "" : "Nơi KCBBĐ: " + LS.maDKBD + ",";
                msg += LS.gtTheTu == null || LS.gtTheTu == "" ? "" : "Hạn thẻ: " + LS.gtTheTu + " - " + LS.gtTheDen + ",";
                msg += LS.ngayDu5Nam == null || LS.ngayDu5Nam == "" ? "" : "Thời điểm đủ 5 năm liên tục: " + LS.ngayDu5Nam + ",";
                msg += LS.maThe != LS.maTheMoi && LS.maTheMoi != null && LS.maTheMoi != "" ? "Mã thẻ mới: " + LS.maTheMoi + "," : "";
                msg += LS.maThe != LS.maTheMoi && LS.maTheMoi != null && LS.maTheMoi != "" ? "Hạn thẻ mới: " + LS.gtTheTuMoi + " - " + LS.gtTheDenMoi + "." : "";

                if (msg.Length > 188)
                {
                    layoutControlItem5.Height = 40;
                } else
                {
                    layoutControlItem5.Height = 20;
                }

                lblMessage.Text = msg;

                //dt = LS.dsLichSuKCB2018.ConvertListToDataTable<wsBHYT_LichSu_respons_2018_detail>();
                //dt.Columns.Add("ls_chitiet");

                //if (dt.Rows.Count == 0)
                //    dt = Func.getTableEmpty(new String[] { "maHoSo", "maCSKCB", "tuNgay", "denNgay", "tenBenh", "tinhTrang", "kqDieuTri", "ls_chitiet" });

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    dt.Rows[i]["kqDieuTri"] = getKetQuaDT(dt.Rows[i]["kqDieuTri"].ToString());
                //    dt.Rows[i]["tinhTrang"] = getTinhTrang(dt.Rows[i]["tinhTrang"].ToString());
                //}

                //ucDS.setData(dt, 1, 1);
                //ucDS.setColumnAll(false);
                //ucDS.setColumn("maHoSo", 0, "Số Hồ sơ");
                //ucDS.setColumn("maCSKCB", 1, "Tên bệnh");
                //ucDS.setColumn("tuNgay", 2, "Ngày vào viện");
                //ucDS.setColumn("denNgay", 3, "Ngày ra viện");
                //ucDS.setColumn("tenBenh", 4, "Tên CSKCB");
                //ucDS.setColumn("tinhTrang", 5, "Tình trạng");
                //ucDS.setColumn("kqDieuTri", 6, "Kết quả Đ.Trị");
                //ucDS.setColumn("tinhTrang", 7, "Tình trạng");

                //ucDS.setColumnButtonImage("ls_chitiet", "Zoom.png", ucDS_DoubleClick);
                ////ucDS.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông tin đăng nhập chưa đúng hoặc không thể lấy dữ liệu từ cổng.");
                log.Fatal(ex.Message);
            }
        } 
        private string getKetQuaDT(string tmp)
        {
            var msg = "";
            if (tmp == "1") { msg = "Khỏi bệnh"; }
            if (tmp == "2") { msg = "Đỡ"; }
            if (tmp == "3") { msg = "Không đổi"; }
            if (tmp == "4") { msg = "Nặng hơn"; }
            if (tmp == "5") { msg = "Tử vong"; }
            return msg;
        }

        private string getTinhTrang(string tmp)
        {
            var msg = "";
            if (tmp == "1") { msg = "Ra viện"; }
            if (tmp == "2") { msg = "Chuyển viện"; }
            if (tmp == "3") { msg = "Trốn viện"; }
            if (tmp == "4") { msg = "Xin ra viện"; }
            return msg;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtMaThe.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu nhập mã BHYT");
                return;
            }
            if (txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Yêu cầu nhập tên bệnh nhân");
                return;
            }

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                getHistory010917();
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}