using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace VNPT.HIS.Common
{
    public class LocalConst 
    {
        public static string csytId = "3";
        public static void INIT()
        {
            try
            {
                LOCAL_SQLITE = new LocalSQLite();
            }
            catch (Exception ex) { }
        }
        public static string LinkService = ConfigurationManager.AppSettings["LinkService"];
        public static string LocalConn_SQLite = ConfigurationManager.AppSettings["Local_ConnectionSqlite"];

        public static LocalSQLite LOCAL_SQLITE;// = new SQLiteDataLayer();

        public static string mess_erro_sys = "Có lỗi xảy ra!";
        public static string mess_erro_datanull = "Chưa nhập/chọn dữ liệu!";
        public static string mess_erro_ngaysinh = "Ngày sinh không được lớn hơn ngày hiện tại!";
        public static string mess_erro_namsinh = "Năm sinh không được lớn hơn năm hiện tại!";
        public static string mess_erro_chuanhapngaysinh = "Bệnh nhân chưa có ngày sinh, hãy nhập ngày sinh!";
        public static string mess_erro_chuanhaptinh = "Bệnh nhân chưa có mã tỉnh";
        public static string mess_erro_chuanhaphuyen = "Bệnh nhân chưa có mã huyện";
        public static string mess_tiepnhan_erro = "Lỗi, chưa lưu được thông tin!";
        public static string mess_tiepnhan_suss = "Cập nhật thông tin bệnh nhân thành công!";

        // Tên các bảng danh mục
        public static string tbl_DMC_NGHENGHIEP = "DMC_NGHENGHIEP";
        public static string tbl_DMC_DANTOC = "DMC_DANTOC";
        public static string tbl_DMC_QUOCTICH = "DMC_QUOCTICH";
    }

    public class ThongTinBenhNhan
    {
        public string BENHNHANID { get; set; }
        public string MABENHNHAN { get; set; }
        public string TENBENHNHAN { get; set; }
        public string NGAYSINH { get; set; }
        public string NAMSINH { get; set; }
        public string TUOI { get; set; }
        public string DVTUOI { get; set; }
        public string GIOITINHID { get; set; }
        public string NGHENGHIEPID { get; set; }
        public string QUOCTICHID { get; set; }
        public string DANTOCID { get; set; }
        public string DIAPHUONGID { get; set; }
        public string DIACHI { get; set; }
        public string NGUOINHA { get; set; }
        public string NGAYKHAM { get; set; }
        public string PHONGKHAMID { get; set; }
        public string HINHTHUCVAOVIENID { get; set; }
        public string BACSIDIEUTRIID { get; set; }
        public string DOITUONGBENHNHANID { get; set; }
        public string MATHEBHYT { get; set; }
        public string THOIGIAN_BD { get; set; }
        public string THOIGIAN_KT { get; set; }
        public string SINHTHETE { get; set; }
        public string DU5NAM { get; set; }
        public string DU6THANG { get; set; }
        public string DIACHIBHYT { get; set; }
        public string DKKCBBDID { get; set; }
        public string TUYENID { get; set; }
        public string DOITUONGMIENGIAMID { get; set; }
        public string TYLEBH { get; set; }
        public string TYLEMIENGIAM { get; set; }
        public string MACHANDOANRAVIEN { get; set; }
        public string CHANDOANRAVIEN { get; set; }
        public string CHANDOANRAVIEN_KT { get; set; }
    }

    public class MauBenhPhamObj
    {	
        public string MAUBENHPHAMID { get; set; }
        public string SOPHIEU { get; set; }
        public string LOAINHOMMAUBENHPHAM { get; set; }
        public string BENHNHANID { get; set; }
        public string NGAYMAUBENHPHAM { get; set; }
        public string NGUOITAO { get; set; }
        public string KHOTHUOCID { get; set; }
        public string PHONGTHUCHIENID { get; set; }
    }

    public class DichVuKhamBenhObj
    {
        public string DICHVUKHAMBENHID { get; set; }
        public string MAUBENHPHAMID { get; set; }
        public string BENHNHANID { get; set; }
        public string NGAYDICHVU { get; set; }
        public string DICHVUID { get; set; }
        public string TENDICHVU { get; set; }
        public string SOLUONG { get; set; }
        public string DONGIA { get; set; }
        public string GIABHYT { get; set; }
        public string GIAVP { get; set; }
        public string GIADV { get; set; }
        public string TYLEDV { get; set; }
        public string BHYTTRA { get; set; }
        public string NHANDANTRA { get; set; }
        public string LOAIDOITUONG { get; set; }
        public string GHICHU { get; set; }
    }
}
