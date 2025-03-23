using System; 
using System.Configuration;
using System.Data; 
using System.IO;

namespace VNPT.HIS.Common
{
    public class Const
    {
        public static int L1_BV_DEFAULT = 0; // 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM;
        public static int default_test = 0;

        public static string LOCAL_CAUHINH_KIOS = "";
        public static string L1_mini = "";
        public static string L1_MAU_STT = "MAU_STT_BACHMAI"; // tên file word mẫu cho app L1 mini
        public static bool L1_XemPhieuSTT = false; // nếu true thì chỉ xem - không tự động in phiếu
        public static bool L1_autologin = true;
        //public static string LinkService = "";
        public static string AcsTcp_IP = "";
        public static string AcsTcp_Port = "";
        //BV Nhiệt đới
        public static string L1_CoSoID = "7984";
        public static string L1_LoaiBHYT = "";

        //lưu server
        public static string L1_kieucapso = "";
        public static string L1_dkkham = "";
        public static string L1_ktraBHYT = "";
        public static string L1_autoPrinter = "";
        public static string L1_phanhe = ""; // dạng: mode_phanhe vidu: 1_2

        public static string ServiceBHYT_Url = ConfigurationManager.AppSettings["ServiceBHYT"];

        public static bool config_moi = true;
        public static string config_data = "data1.dat";

        //L2PT-22069
        public static int id_dinhdanh_kios = 0;

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int login_timeout = 0;// 0: ko check timeout; 1: vẫn trong session; 2: đã hết session
        public static void INIT()
        {
            try
            {
                SQLITE = new SQLiteDataLayer();

                log.Info("Conn sqlite = " + (SQLITE != null));
            }
            catch (Exception ex) {
                log.Fatal(ex.ToString());
            }
            try
            {
                imgDefault = System.Drawing.Image.FromFile("./Resources/default.png");
            }
            catch (Exception ex) {
                log.Fatal(ex.ToString());
            }

            FileInfo[] Files = new DirectoryInfo("Resources").GetFiles("*.png");
            foreach (FileInfo file in Files) listIcon += "|" + file.Name;
             
            if (LinkService.IndexOf("//") > -1 && LinkService.IndexOf(".") > LinkService.IndexOf("//"))
            {
                SubDomain = LinkService.Substring(LinkService.IndexOf("//") + 2);
                SubDomain = SubDomain.Substring(0, SubDomain.IndexOf("."));
            } 
        }

        public static string SubDomain = "";

        //public static string FolderSaveFilePrint = "temphis2018"; //vd: "C:\\temphis2018"  thư mục mặc định để lưu các file in, sẽ tự động xóa sau khi in, nếu = rỗng thì mặc định là tại \\temp của nơi cài đặt app
        public static string FolderSaveFilePrint = "C:\\hisvnpt1";

        public static DataRowView drvBenhNhan = null;
        public static DataRowView drvBenhNhan_ChiTiet = null;

        public static Object formMain = null;
        public static string LinkService = ConfigurationManager.AppSettings["LinkService"];
        public static string LinkReport = ConfigurationManager.AppSettings["LinkReport"];
        public static string CONN_SQLite = ConfigurationManager.AppSettings["ConnectionSqlite"];
        public static string FileInPhieu = ConfigurationManager.AppSettings["FileInPhieu"];

        public static string FORMAT_date1 = "dd\\/MM\\/yyyy";
        public static string FORMAT_date2 = "dd-MM-yyyy";
        public static string FORMAT_datetime1 = "dd\\/MM\\/yyyy HH:mm:ss";
        public static string FORMAT_datetime2 = "dd\\/MM\\/yyyy HH:mm";

        //L2PT-6476
        public static string KHOAIID = "-1";
        public static string PHONGID = "-1";


        public static SQLiteDataLayer SQLITE;// = new SQLiteDataLayer();

        public static string listIcon = "";
        public static System.Drawing.Image imgDefault;//((System.Drawing.Image)(new System.ComponentModel.ComponentResourceManager(typeof(frmMain)).GetObject("menuHeThong_Doimatkhau.LargeGlyph")));
        public static System.Drawing.Font fontDefault = new System.Drawing.Font("Tahoma", 9F);
        public static System.Drawing.Font fontBoldDefault = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

        public static string HIS_FILEEXPORT_TYPE = "";
        public static UserLogin local_user = null;
        public static double diffInSeconds = 0; // chênh lệch số giây, giữa server và client

        public static System.Drawing.Size screen =  new System.Drawing.Size(0, 0);
        // Màu nền nhạt:    system.GradientInactiveCaption
        // Màu nền đậm:     system.GradientActiveCaption
        // Màu chữ selected:system.hightlight

        // Bien cache trên local
        public static string local_username = "";
        public static string local_khoa = "";
        public static string local_phong = "";
        public static int local_khoaId = 0;
        public static int local_phongId = 0;

        //
        public static string mess_erro_sys = "Có lỗi xảy ra!";
        public static string mess_erro_datanull = "Chưa nhập/chọn dữ liệu!";
        public static string mess_erro_ngaysinh = "Ngày sinh không được lớn hơn ngày hiện tại!";
        public static string mess_erro_namsinh = "Năm sinh không được lớn hơn năm hiện tại!";
        public static string mess_erro_chuanhapngaysinh = "Bệnh nhân chưa có ngày sinh, hãy nhập ngày sinh!";
        public static string mess_erro_chuanhaptinh = "Bệnh nhân chưa có mã tỉnh";
        public static string mess_erro_chuanhaphuyen = "Bệnh nhân chưa có mã huyện";

        public static string mess_tiepnhan_erro = "Lỗi, chưa lưu được thông tin!";
        public static string mess_tiepnhan_suss = "Cập nhật thông tin bệnh nhân thành công!";

        //Login
        public static string mess_login_suss = "Đăng nhập thành công!";
        public static string mess_login_erro = "Đăng nhập không thành công!";
        public static string mess_login_passnull = "Chưa Nhập Mật Khẩu!";


        // Tên các bảng lưu cache
        public static string tbl_DsBenh = "NT.008";
        public static string tbl_NoiDKKCB = "NT.009"; 
        public static string tbl_DsTinh = "NGTTI.002";
        public static string tbl_DsHuyenXa = "DMDP.001";
        public static string tbl_DsKhoa = "DEPT.P01"; 
        public static string tbl_TinhhuyenxaViettat = "DMDP.002";

        public static string tbl_Thuoc = "NTU02D010.13";
        public static string tbl_ThuocMuaNgoai = "NTU02D010.02";
        public static string tbl_ThuocPhieuTraThuocVT = "NTU02D010.01"; 

        public static string tbl_Dantoc = "COM.DANTOC";
        public static string tbl_Gioitinh = "COM.GT";
        public static string tbl_Nghenghiep = "NGTNN.002";
        public static string tbl_Quoctich = "NGTQG.002";
        public static string tbl_Noisong = "NT.0010"; 

        public static string tbl_Tuyen = "DV.BHYT.001"; // tuyến giới thiệu
        public static string tbl_DTMienGiam = "DMC.DTDACBIET"; // các đối tượng miễn giảm
        public static string tbl_Phongkham = "LOADPK.TIMKIEM";
        public static string tbl_TrangThaiKham = "NGT02K009.RV005";
        public static string tbl_XuTriKB = "NGT.TT.01";
        public static string tbl_DichVuKhac = "DMC.DVTHUKHAC"; // Thu dịch vụ khác
        public static string tbl_KhoaDTNT = "KHOA.DTNT";
        public static string tbl_KhoaDTNgT = "KHOA.DTNGT";
        public static string tbl_DSHopDong = "DS.HOPDONG";
        public static string tbl_BacSyKham = "NGTHK.BSKHAM";
        public static string tbl_DuongDung = "NTU02D010.07";
        public static string tbl_YeuCauKham = "NGTDV.002"; 
        public static string tbl_TrangThai_RV002 = "NGT02K009.RV002";
        public static string tbl_TrangThai_RV003 = "NGT02K009.RV003";  
        


        public static string tbl_Doituong = "NGTDV_007_"; 

        //Hanv_ các bảng trong chỉ định dịch vụ
        public static string tbl_CDDV_CAUHINHBV = "CDDV_CAUHINHBV";
        public static string tbl_CDDV_ICD = "CDDV_ICD";
        public static string tbl_CDDV_DSXETNGHIEM = "CDDV_DSXETNGHIEM";
        public static string tbl_CDDV_DSCDHA = "CDDV_DSCDHA";
        public static string tbl_CDDV_DSPTTT = "CDDV_DSPTTT";

        // màn hình chụp ảnh
        public static string mess_chupanh_chuanhaphosobenhnhan = "Không tìm thấy hồ sơ bệnh nhân";
        public static string mess_chupanh_uploadanhthanhcong = "Upload và lưu ảnh thành công";
        public static string mess_chupanh_uploadanhthatbai = "Lưu ảnh thất bại, mã lỗi: ";

        // màn hình yêu cầu mở bệnh án
        public static string mess_yeucaumobenhan_benhnhandamobenhan = "Bệnh nhân đã mở bệnh án";
        public static string mess_yeucaumobenhan_daguiyeucaumolaibenhan = "Đã gửi yêu cầu mở lại bệnh án";
        public static string mess_yeucaumobenhan_capnhatthongtinkhongthanhcong = "Cập nhật thông tin không thành công";
        public static string mess_yeucaumobenhan_chuayeucaumo = "Bệnh nhân chưa có yêu cầu mở, hãy gửi yêu cầu mở trước khi mở bệnh án.";
        public static string mess_yeucaumobenhan_daguiyeucaumochungchuamobenhan = "Bệnh nhân đã gửi yêu cầu mở nhưng chưa mở bệnh án, hãy kiểm tra lại.";
        public static string mess_yeucaumobenhan_daduyetketoan = "Đã duyệt kế toán/bảo hiểm ko thể mở lại bệnh án.";
        public static string mess_yeucaumobenhan_danhapkhoanoitru = "Bệnh nhân đã nhập khoa bên nội trú không mở được bệnh án.";
        public static string mess_yeucaumobenhan_yeucaudaduocmohayguiyeucaukhac = "Yêu cầu đã được mở, hãy gửi yêu cầu mở khác.";
        public static string mess_yeucaumobenhan_daketthucdieutri = "Bệnh nhân đã kết thúc điều trị nội/ngoại trú không mở được bệnh án.";
        public static string mess_yeucaumobenhan_phainhapnoidungyeucaumo = "Phải nhập nội dung yêu cầu mở";

        // màn hình thiết lập phòng khám - bác sĩ
        public static string mess_thietlap_bs_pk_chonphongkhamthietlap = "Hãy chọn phòng khám muốn thiết lập.";
        public static string mess_thietlap_bs_pk_xacnhantruockhicapnhat = "Các thiết lập cũ sẽ mất đi khi cập nhật lại. Bạn có tiếp tục ?";
        public static string mess_thietlap_bs_pk_capnhatthanhcong = "Cập nhật thiết lập phòng thành công.";
        public static string mess_thietlap_bs_pk_capnhatthatbai = "Lỗi cập nhật thiết lập phòng.";
    }
}
