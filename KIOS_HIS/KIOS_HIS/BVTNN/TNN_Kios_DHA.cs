using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using L1_Mini;
using Spire.Pdf.General.Render.Decode.Jpeg2000.j2k.wavelet.synthesis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VNPT.HIS.Common;
using static Spire.Pdf.General.Render.Decode.Jpeg2000.j2k.codestream.HeaderInfo;

namespace KIOS_HIS.BVTNN
{
    public partial class TNN_Kios_DHA : Form
    {
        protected EventHandler Return_To_Mainform;
        protected bool exit = true;
        private DataTable dataTableBenhNhan = new DataTable();
        private DataTable dataTablePhongKham = new DataTable();

        string SoYTe = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_SYT");
        string TenBV = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_TENBV");

        public TNN_Kios_DHA(bool parent_fullscreen = false)
        {
            InitializeComponent();

            if (parent_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                TopMost = parent_fullscreen;
                if (TopMost) TopLevel = true;
            }
            txtMaTimKiem.Text = "";
            txtMaTimKiem.Focus();
            txtHoTen.Visible = false;
            lb_message.Visible = false;
            btnDangKyKham.Visible = false;

            lbSoYTe.Text = SoYTe.ToUpper();
            lbTenBenhVien.Text = TenBV.ToUpper();
        }

        public void setReturn_To_Mainform(EventHandler eventReturnData)
        {
            Return_To_Mainform = eventReturnData;
        }
        public void TNN_Kios_DHA_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (exit) frmMain.Current.Exit();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            //Thực hiện truy xuất thông tin bệnh nhân thông qua Mã BHYT
            string maTimKiem = txtMaTimKiem.Text.Trim().ToUpper();
            int length = maTimKiem.Length;
            bool notInclude_ = maTimKiem.IndexOf("_") < 0;
            bool isScanQR = maTimKiem.IndexOf("|") > 0;
            bool showPhongKham = false;
            lb_message.Text = "";
            lb_message.Visible = false;
            btnDangKyKham.Visible = false;

            txtHoTen.Text = "";
            txtHoTen.Visible = true;

            if (length == 0)
            {
                lb_message.Text = "Vui lòng điền thông tin để tiếp tục!";
                lb_message.ForeColor = System.Drawing.Color.Red;
                lb_message.Visible = true;
                txtMaTimKiem.Focus();
            }
            else
            {
                // các trường hợp gồm có: 
                // Thẻ 10 ký tự: chuỗi 10 ký tự chỉ toàn số  => Nếu k thấy có dữ liệu thì check theo mã BN; 
                // Thẻ 15 ký tự: chuỗi 15 ký tự có 2 ký tự đầu là chữ, 10 ký tự cuối là số 
                // Mã QR thẻ: độ dài > 100 
                // Mã BN: các trường hợp còn lại

                if (maTimKiem.Length == 10 && notInclude_) //Nhập mã tìm kiếm 10 kí tự => có thể là số thẻ bhyt kiểu mới (10 kí tự) hoặc là mã bệnh nhân trên hệ thống
                {
                    dataTableBenhNhan = TimKiem_BenhNhan(maTimKiem, "6");

                    //tìm thông tin bn trong database theo mã thẻ 10 số
                    if (dataTableBenhNhan.Rows.Count > 0)
                    {
                        txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                        txtHoTen.Visible = true;
                        showPhongKham = true;
                    }
                    else //nếu không có trong db thì tìm theo mã bệnh nhân 10 kí tự
                    {
                        dataTableBenhNhan = TimKiem_BenhNhan(maTimKiem, "1");

                        if (dataTableBenhNhan.Rows.Count > 0 && dataTableBenhNhan.Columns.Contains("TENBENHNHAN"))
                        {
                            txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                            txtHoTen.Visible = true;
                            showPhongKham = true;
                        }
                        else
                        {
                            lb_message.Text = "Không tìm thấy thông tin của bạn! Vui lòng đăng ký khám tại quầy tiếp đón.";
                            lb_message.ForeColor = System.Drawing.Color.Red;
                            lb_message.Visible = true;
                        }
                    }
                }
                else if (length == 15 && notInclude_) //Nhập mã tìm kiếm 15 kí tự => thẻ BHYT loại cũ có cả mã đối tượng ở đầu
                {
                    dataTableBenhNhan = TimKiem_BenhNhan(maTimKiem, "2");
                    if (dataTableBenhNhan.Rows.Count > 0 && dataTableBenhNhan.Columns.Contains("TENBENHNHAN"))
                    {
                        txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                        txtHoTen.Visible = true;
                        showPhongKham = true;
                    }
                    else
                    {
                        lb_message.Text = "Không tìm thấy thông tin của bạn! Vui lòng đăng ký khám tại quầy tiếp đón.";
                        lb_message.ForeColor = System.Drawing.Color.Red;
                        lb_message.Visible = true;
                    }
                }
                else if (length > 15 && isScanQR) //Dữ liệu QR code, cần check xem là dữ liệu của CCCD hay là thẻ BHYT
                {
                    bool isQRBHYT = maTimKiem.EndsWith("$"); //check xem QR được quét có phải là QR của thẻ BHYT hay không
                    string[] sobhyt_catchuoi = maTimKiem.Split('|');
                    string maBaoHiem = sobhyt_catchuoi[0];

                    if (isQRBHYT) //nếu là thẻ BHYT
                    {
                        txtMaTimKiem.Text = maBaoHiem;

                        if (maBaoHiem.Length == 10) //Thẻ BHYT có 10 kí tự
                        {
                            dataTableBenhNhan = TimKiem_BenhNhan(maBaoHiem, "6");

                            //tìm thông tin bn trong database theo mã thẻ 10 số
                            if (dataTableBenhNhan.Rows.Count > 0)
                            {
                                txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                                txtHoTen.Visible = true;
                                showPhongKham = true;
                            }
                            else //nếu không có trong db thì tìm theo mã bệnh nhân 10 kí tự
                            {
                                dataTableBenhNhan = TimKiem_BenhNhan(maTimKiem, "1");

                                if (dataTableBenhNhan.Rows.Count > 0 && dataTableBenhNhan.Columns.Contains("TENBENHNHAN"))
                                {
                                    txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                                    txtHoTen.Visible = true;
                                    showPhongKham = true;
                                }
                                else
                                {
                                    lb_message.Text = "Không tìm thấy thông tin của bạn! Vui lòng đăng ký khám tại quầy tiếp đón.";
                                    lb_message.ForeColor = System.Drawing.Color.Red;
                                    lb_message.Visible = true;
                                }
                            }
                        }
                        else //loại thẻ 15 kí tự
                        {
                            dataTableBenhNhan = TimKiem_BenhNhan(maTimKiem, "2");
                            if (dataTableBenhNhan.Rows.Count > 0 && dataTableBenhNhan.Columns.Contains("TENBENHNHAN"))
                            {
                                txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                                txtHoTen.Visible = true;
                                showPhongKham = true;
                            }
                            else
                            {
                                lb_message.Text = "Không tìm thấy thông tin của bạn! Vui lòng đăng ký khám tại quầy tiếp đón.";
                                lb_message.ForeColor = System.Drawing.Color.Red;
                                lb_message.Visible = true;
                            }
                        }
                    }
                    else //nếu không thì là CCCD
                    {
                        string sobhyt = sobhyt_catchuoi[0];
                        string namsinh = sobhyt_catchuoi[3].Substring(4, 4);
                        string hoten = sobhyt_catchuoi[2];

                        Func.set_log_file("Log Check từ cổng với thông tin: " + hoten + " | " + namsinh + " | " + sobhyt);

                        wsBHYT_LichSu_respons_2018 response = ServiceBHYT.Get_History010118(sobhyt, hoten, namsinh);
                        if (response.maKetQua == "004" || response.maKetQua == "000")
                        {
                            txtMaTimKiem.Text = response.maThe;

                            dataTableBenhNhan = TimKiem_BenhNhan(txtMaTimKiem.Text, "2");
                            if (dataTableBenhNhan.Rows.Count > 0 && dataTableBenhNhan.Columns.Contains("TENBENHNHAN"))
                            {
                                txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                                txtHoTen.Visible = true;
                                showPhongKham = true;
                            }
                            else
                            {
                                lb_message.Text = "Không tìm thấy thông tin của bạn! Vui lòng đăng ký khám tại quầy tiếp đón.";
                                lb_message.ForeColor = System.Drawing.Color.Red;
                                lb_message.Visible = true;

                                txtHoTen.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thông tin thẻ CCCD!");
                        }
                    }
                }
                else
                {
                    dataTableBenhNhan = TimKiem_BenhNhan(maTimKiem, "1");

                    if (dataTableBenhNhan.Rows.Count > 0 && dataTableBenhNhan.Columns.Contains("TENBENHNHAN"))
                    {
                        txtHoTen.Text = "Xin chào " + dataTableBenhNhan.Rows[0]["TENBENHNHAN"].ToString() + " (" + dataTableBenhNhan.Rows[0]["NAMSINH"].ToString() + ")";
                        txtHoTen.Visible = true;
                        showPhongKham = true;
                    }
                    else
                    {
                        lb_message.Text = "Không tìm thấy thông tin của bạn! Vui lòng đăng ký khám tại quầy tiếp đón.";
                        lb_message.ForeColor = System.Drawing.Color.Red;
                        lb_message.Visible = true;
                    }
                }
            }

            if (showPhongKham)
            {
                btnDangKyKham.Visible = true;
            }
        }

        #region funtion EH
        private DataTable TimKiem_BenhNhan(string maTimKiem, string kieu)
        {
            //MaTimKiem = "HT3343422121270";
            //DOI_TUONG_ID = MaTimKiem == "2" ? "1" : "2";  // 2 vp; 1 bh
            //1 theo mã bệnh nhân
            //2 theo mã bhyt

            lb_message.Text = "";

            string tenBenhNhan = "";
            string ngaySinh = "";
            string gioiTinh = "1";// phải để mặc định nếu ko có 

            DataTable dt = RequestHTTP.getChiTiet_BenhNhan(maTimKiem, kieu, tenBenhNhan, ngaySinh, gioiTinh);

            if (dt.Rows.Count == 0)
            {
                lb_message.Text = "Không tìm thấy thông tin bệnh nhân!";
                lb_message.ForeColor = System.Drawing.Color.Red;
            }

            return dt;
        }

        string ten_Pkham = "";
        string GIOITINHID = "";
        string NAMSINH = "";
        string SDTBENHNHAN = "";
        string DIACHI = "";
        string TUOI = "";
        string MAKCBBD = "";
        string MABENHNHAN = "";
        string hide_STT = "";
        string DiaChi_PK = "";
        string So_PK = "";
        string hide_Benh_An = "";
        private void Return_YeuCauKham(object sender, EventArgs e)
        {
            //trả kết quả về khi gửi submit đăng ký khám lên sv
            string ret = (string)sender;
            ten_Pkham = ""; GIOITINHID = ""; NAMSINH = ""; SDTBENHNHAN = ""; DIACHI = ""; TUOI = ""; MAKCBBD = ""; MABENHNHAN = "";
            hide_STT = ""; DiaChi_PK = ""; So_PK = "";

            if (ret.IndexOf(" f# ") > -1)
            {
                // NULL|GIOITINHID|NAMSINH|SDTBENHNHAN|DIACHI|TUOI|MAKCBBD|TENPHONGKHAM|DIACHIPK|SOPK F#
                string thong_tin = ret.Substring(0, ret.IndexOf(" f# "));
                string[] arr_thong_tin = thong_tin.Split('|');
                MABENHNHAN = arr_thong_tin[0];
                GIOITINHID = arr_thong_tin[1] == "1" ? "Nam" : (arr_thong_tin[1] == "2" ? "Nữ" : "");
                NAMSINH = arr_thong_tin[2];
                SDTBENHNHAN = arr_thong_tin[3];
                DIACHI = arr_thong_tin[4];
                TUOI = arr_thong_tin[5];
                MAKCBBD = arr_thong_tin[6];
                ten_Pkham = arr_thong_tin[7];
                DiaChi_PK = arr_thong_tin[8];
                So_PK = arr_thong_tin[9];

                ret = ret.Substring(ret.IndexOf(" f# ") + 4);
            }


            if (ret.StartsWith("ret_true"))//thành công thì in phiếu STT luôn
            {
                string[] retArr = ret.Substring("ret_true".Length).Split(',');
                //13 biến: RETURN b_khambenhid||','||vmabenhnhan||','||b_phongkhamdangkyid||','||b_tiepnhanid||','||b_hosobenhanid||','||b_benhnhanid||','||b_bhytid||','||b_maubenhphamid||','||b_dichvukhambenhid || ',' || b_mahosobenhan || ',' || b_thukhac || ',' || r_socapcuu || ',' || b_sothutupkdk;	
                string maBN_IN_DNO, tenBn = "";
                if (retArr.Length >= 9) hide_Benh_An = retArr[9];
                if (retArr.Length >= 12) hide_STT = retArr[12];
                if (retArr.Length >= 1) maBN_IN_DNO = retArr[1];
                if (retArr.Length >= 16) tenBn = retArr[15];

                bool inSTT = Convert.ToBoolean(Properties.Settings.Default["InSTT"]);
                if (inSTT)
                {
                    InSTT(hide_STT, tenBn, NAMSINH, ten_Pkham, DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy"));
                }

                btnXoa_Click(sender, e);
                MessageBoxEx.Show("Đăng ký khám bệnh thành công!", "Thông báo", 10000);
            }
            else
            {
                // lỗi đăng ký khám
                MessageBox.Show(ret.ToString(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtMaTimKiem.Focus();
            lb_message.Text = "";
            lb_message.Visible = false;
            txtHoTen.Text = "";
            txtHoTen.Visible = false;
            btnDangKyKham.Visible = false;
        }

        string maDKBD = "";
        string gtTheTu = "";
        string gtTheDen = "";
        string diaChi = "";
        string ngayDu5Nam = "";
        private bool KiemTra_TheBHYT()
        {
            string sobhyt = "";
            string namsinh = "";
            string hoten = "";

            string tt = "";
            // nếu quẹt từ thẻ bh thì lấy tt của thẻ làm biến đầu vào
            string[] sobhyt_catchuoi = txtMaTimKiem.Text.Trim().ToUpper().Split('|');
            if (sobhyt_catchuoi.Length >= 10)
            {
                sobhyt = sobhyt_catchuoi[0];
                namsinh = sobhyt_catchuoi[2].Trim();
                hoten = Func.FromHex(sobhyt_catchuoi[1]);
                tt = "Thẻ";
            }
            else if (dataTableBenhNhan.Rows.Count > 0) // ko quẹt thẻ thì lấy từ tt BN tìm kiếm đc trên db về
            {
                if (dataTableBenhNhan.Columns.Contains("MA_BHYT")) sobhyt = dataTableBenhNhan.Rows[0]["MA_BHYT"].ToString();
                namsinh = dataTableBenhNhan.Rows[0]["namsinh"].ToString();
                hoten = dataTableBenhNhan.Rows[0]["tenbenhnhan"].ToString();
                tt = "DB";

                if (dataTableBenhNhan.Columns.Contains("MABENHNHAN"))
                    tt += " (" + dataTableBenhNhan.Rows[0]["MABENHNHAN"].ToString() + ")";
            }

            if (sobhyt == "")
            {
                Func.set_log_file("LOI1: ko có tt sobhyt để check từ cổng: " + tt + " | " + hoten + " | " + namsinh + " | " + sobhyt);
                return true;
            }
            Func.set_log_file("Log Check từ cổng với : " + tt + " | " + hoten + " | " + namsinh + " | " + sobhyt);

            // CHECK CONG BHYT KIEMTRA_THEBHYT 741
            wsBHYT_LichSu_respons_2018 ret1 = ServiceBHYT.Get_History010118(
                   sobhyt,
                   hoten,
                   namsinh
                   );
            // {"maKetQua":"090","hoTen":"Nguyễn Hồng Hải","gioiTinh":"Nam","diaChi":"11 trệt nguyễn duy dương,p8,q5","maDKBD":"79014","cqBHXH":"Bảo hiểm Xã hội quận 5",
            //  "gtTheTu":"01/01/2017","gtTheDen":"31/12/2021","maKV":"","ngayDu5Nam":"01/12/2015",
            // "dsLichSuKCB":[{"maHoSo":535595540,"maCSKCB":"79014","tuNgay":"28/09/2017","denNgay":"28/09/2017","tenBenh":"E11....;","tinhTrang":"1","kqDieuTri":"1"},{"maHoSo":443409000,"maCSKCB":"79014","tuNgay":"03/05/2017","denNgay":"03/05/2017","tenBenh":"J00 -  - Viêm mũi họng cấp [cảm thường]","tinhTrang":"","kqDieuTri":""}]}

            if (ret1 == null || ret1.maKetQua == null || ret1.maKetQua == "null") // lỗi lấy thông tin
            {
                Func.set_log_file("LOI3: trên cổng ko có");
                //"LỖI ĐĂNG KIỂM TRA THẺ BHYT", MessageBoxButtons.OK, MessageBoxIcon.Error , 
                DialogResult dlResult = MessageBoxEx.Show("Mời bạn đăng ký khám tại quầy (Không có dữ liệu lịch sử KCB. Yêu cầu kiểm tra thông tin đầu vào)", 8000);

                return false;
            }

            maDKBD = ret1.maDKBD;
            gtTheTu = ret1.gtTheTu;
            gtTheDen = ret1.gtTheDen;
            diaChi = ret1.diaChi;
            ngayDu5Nam = ret1.ngayDu5Nam;


            bool the_sai_hoten_ngaysinh = ret1.maKetQua != "060" && ret1.maKetQua != "061" && ret1.maKetQua != "070";//  không sai=true; sai = false
            bool maKQ_rong_Hoac_theDen_rong = ret1.maKetQua != "" && ret1.gtTheDen != null && ret1.gtTheDen != "" && ret1.gtTheDen != "null";//  không rỗng=true; rỗng = false

            if (ret1.maKetQua == "004")
            {
                //MessageBox.Show(ret1.ghiChu + ". Bệnh nhân sẽ được tiếp đón bình thường.");
                return true;
            }
            else if (ret1.maKetQua == "000")
            {
                return true;
            }

            // khong hop le, cap nhat lai roi cho tiep don
            if (
                 maKQ_rong_Hoac_theDen_rong == false   // dl rỗng
                || the_sai_hoten_ngaysinh == true      // không sai tên/ngay sinh 
                )
            {
                //"LỖI ĐĂNG KIỂM TRA THẺ BHYT", MessageBoxButtons.OK, MessageBoxIcon.Error,
                Func.set_log_file("LOI4: cổng trả về: " + ret1.ghiChu);
                DialogResult dlResult = MessageBoxEx.Show("Mời bạn đăng ký khám tại quầy (" + ret1.ghiChu + ")", 8000);
                return false;
            }

            //kiểm tra hạn thẻ nằm ngoài thời gian hiện tại? //  "gtTheTu":"01/01/2017","gtTheDen":"31/12/2021"
            bool han_the_bh = true;
            DateTime myDate_Tu, myDate_Den;
            DateTime hien_tai = Func.getDatetime_Short(DateTime.Now);
            gtTheTu = gtTheTu.Replace("-", "/");
            gtTheDen = gtTheDen.Replace("-", "/");

            if (DateTime.TryParseExact(gtTheTu, Const.FORMAT_date1, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None
                , out myDate_Tu) == false) han_the_bh = false;
            if (DateTime.TryParseExact(gtTheDen, Const.FORMAT_date1, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None
                , out myDate_Den) == false) han_the_bh = false;

            if (han_the_bh == false || hien_tai < myDate_Tu || hien_tai > myDate_Den)
            {
                Func.set_log_file("LOI5: Sai thời hạn thẻ:" + maDKBD + " | " + gtTheTu + " | " + gtTheDen + " | " + diaChi);
                DialogResult dlResult = MessageBoxEx.Show("Sai thời hạn thẻ bảo hiểm, mời bạn đăng ký khám tại quầy.", 10000);
                return false;
            }

            return true;
        }
        #endregion

        #region Config
        private void TNN_Kios_DHA_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.Shift && e.KeyCode == Keys.P)
                  || (e.Control && e.Shift && e.KeyCode == Keys.Q))
            {
                bool current_TopMost = this.TopMost;
                //FullScreen(false);

                // mở config
                ConfigNew frm = new ConfigNew(current_TopMost);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.setReturnData(Return_Config);
                frm.ShowDialog();
            }
        }

        private void Return_Config(object sender, EventArgs e)
        {
            string ret = (string)sender;

            if (ret == "thoat")
            {
                exit = true;
                this.Close();
                return;
            }
            else if (ret == "dang_nhap_lai")
            {
                exit = false;
                this.Close();
                Return_To_Mainform(ret, null);
                return;
            }
            else if (ret != "thoat")
            {
                FullScreen(ret == "true");
            }
        }

        private void FullScreen(bool full)
        {
            FormBorderStyle = full ? FormBorderStyle.None : FormBorderStyle.Sizable;
            TopMost = full;
            if (TopMost) TopLevel = true;
            WindowState = FormWindowState.Maximized;
        }
        #endregion

        private void btnDangKyKham_Click(object sender, EventArgs e)
        {
            //kiểm tra xem thẻ bhyt
            if (KiemTra_TheBHYT())
            {
                string doiTuongBHYT = "1"; //1 là có thẻ BHYT, 2 là viện phí
                BVTNN_DangKyKham frm = new BVTNN_DangKyKham(this.Width, this.Height, this.TopMost);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.setReturnData(Return_YeuCauKham);
                frm.setDt_BenhNhan(dataTableBenhNhan, doiTuongBHYT, "");
                frm.setTT_TheBHYT(maDKBD, gtTheTu, gtTheDen, diaChi, ngayDu5Nam);
                frm.FormClosed += Frm_FormClosed;
                frm.ShowDialog();

                txtMaTimKiem.Focus();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMaTimKiem.Text = "";
            txtMaTimKiem.Focus();
            lb_message.Text = "";
            lb_message.Visible = false;
            txtHoTen.Text = "";
            txtHoTen.Visible = false;
            btnDangKyKham.Visible = false;
            btnLaySo.Enabled = true;
        }

        private void txtMaTimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnCheck_Click(sender, e);
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            TNN_Setting setting = new TNN_Setting();
            setting.ShowDialog();

            //nếu có save setting thì reload giao diện form cha
            if (setting.SaveSuccess)
            {

            }
        }

        private void InSTT(string STT, string TenBenhNhan, string NamSinh, string LoaiKham, string ThoiGian)
        {
            string Report = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_MAUSTT");

            //Thông tin file mẫu và file lưu ra
            string MAU = @"./Resources/" + Report;
            string NAME = Report + "_TEST_" + DateTime.Now.ToString("yyMMddHHmmss") + "_" + new Random().Next(1000);
            string OUT = @"./Resources/" + NAME;

            string MAU_FILE_DOCX = System.Windows.Forms.Application.StartupPath + MAU.Substring(1).Replace("/", "\\") + ".docx";
            string OUT_FILE_DOC = System.Windows.Forms.Application.StartupPath + OUT.Substring(1).Replace("/", "\\") + ".docx";

            //Copy tạo 1 file mẫu mới
            File.Copy(MAU_FILE_DOCX, OUT_FILE_DOC, true);

            //Đọc file html to text
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(OUT_FILE_DOC, true))
            {
                // Xử lý text
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("zzstt", STT);
                docText = docText.Replace("zzhoten", TenBenhNhan);
                docText = docText.Replace("zznamsinh", NamSinh.Length != 0 ? "(" + NamSinh + ")" : NamSinh);
                docText = docText.Replace("zzloaikham", LoaiKham);
                docText = docText.Replace("zzthoigian", ThoiGian);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }

            //infile
            PrintWordDocument(OUT_FILE_DOC);
        }
        private void PrintWordDocument(string inputPath)
        {
            // Create an instance of Word Application
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false; // Set Visible property to false to hide the application
            wordApp.ScreenUpdating = false;
            wordApp.ShowAnimation = false;
            wordApp.ShowStylePreviews = false;

            // Open the document
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(inputPath);

            // Print the document
            doc.PrintOut(Background: true);

            Thread.Sleep(100);
            // Close the document and the application
            doc.Close();
            wordApp.Quit();

            File.Delete(inputPath);
        }

        private void btnLaySo_Click(object sender, EventArgs e)
        {
            try
            {
                btnLaySo.Enabled = false;
                string HoTen = "", MaBN = "", MaBaoHiem = "", barcode = "", loaikham = "Khám bệnh";
                string id_dinhdanh_kios_tmp = "0";
                DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KIOS.CAPSOUT", HoTen + "$" + MaBN + "$" + MaBaoHiem + "$" + "" + "$" + barcode + "$" + "0" + "$" + id_dinhdanh_kios_tmp.ToString());

                if (dt.Rows.Count > 0 && dt.Columns.Contains("STT"))
                {
                    Int64 stt = Convert.ToInt64(dt.Rows[0]["STT"].ToString());
                    InSTT(stt <= 9 ? "0" + stt.ToString() : stt.ToString(), "", "", loaikham, DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"));
                    btnLaySo.Text = "LẤY SỐ " + (stt + 1);
                }
                btnLaySo.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lấy số khám bệnh!");
                Func.set_log_file("Log Lấy số khám bệnh: " + ex.ToString());
            }
        }


    }
}
