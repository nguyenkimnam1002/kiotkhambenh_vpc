using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MainForm.Class
{
    public class ReportDonThuoc
    {
        public string parenName { get; set; }
        public string orgName { get; set; }
        public string maBenhNhan { get; set; }
        public string soPhieuThuoc { get; set; }
        public string tenBenhNhan { get; set; }
        public string tuoi { get; set; }
        public string dvTuoi { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string checkVp { get; set; }
        public string soThe1 { get; set; }
        public string soThe2 { get; set; }
        public string soThe3 { get; set; }
        public string soThe4 { get; set; }
        public string soThe5 { get; set; }
        public string maKCBBD { get; set; }
        public string dungTuyen { get; set; }
        public string hanThe { get; set; }
        public string chanDoan { get; set; }
        public string chanDoanPhu { get; set; }
        public string ngayMauBenhPham { get; set; }
        public string nguoiTao { get; set; }
        //Detail
        public string rowNum { get; set; }
        public string tenDichVu { get; set; }
        public string soLuong { get; set; }
        public string tenDvt { get; set; }
        public string cachDung { get; set; }
    }

    public class Bang_DichVu
    {
        public string parenTest { get; set; }
        public string parenName { get; set; }
        public string orgName { get; set; }
        public string maBenhNhan { get; set; }
        public string soPhieuThuoc { get; set; }
        public string tenBenhNhan { get; set; }
        public string tuoi { get; set; }
        public string dvTuoi { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string checkVp { get; set; }
        public string soThe1 { get; set; }
        public string soThe2 { get; set; }
        public string soThe3 { get; set; }
        public string soThe4 { get; set; }
        public string soThe5 { get; set; }
        public string maKCBBD { get; set; }
        public string dungTuyen { get; set; }
        public string hanThe { get; set; }
        public string chanDoan { get; set; }
        public string chanDoanPhu { get; set; }
        public string ngayMauBenhPham { get; set; }
        public string nguoiTao { get; set; }
        //Detail
        public string rowNum { get; set; }
        public string tenDichVu { get; set; }
        public string soLuong { get; set; }
        public string donGia { get; set; }
        public string BHYT_tra { get; set; }
        public string ND_tra { get; set; }
        public string tenPhong { get; set; }
        
    }


    public class Report_CPKB
    {
        public Report_CPKB(string name)
        {
            TEN_DICH_VU = name;
        }
        public string STT_ABC { get; set; }
        public string STT { get; set; }
        public string TEN_KHOA { get; set; }
        public string TEN_DICH_VU { get; set; }
        public float TONG_THANH_TIEN_BV { get; set; }
        public float TONG_THANH_TIEN_BH { get; set; }
        public float TONG_QUY_BHYT { get; set; }
        public float TONG_NGUOI_BENH_CUNG_TRA { get; set; }
        public float TONG_KHAC { get; set; }
        public float TONG_NGUOI_BENH_TU_TRA { get; set; }
        public List<Report_CPKB_Detail> chitiet { get; set; }
    }
    public class Report_CPKB_Detail
    {
        public Report_CPKB_Detail()
        {
        }
        public string STT { get; set; }
        public string DETAIL_TEN_NOI_DUNG { get; set; }
        public string DETAIL_DON_VI { get; set; }
        public float DETAIL_SL { get; set; }
        public float DETAIL_DON_GIA_BV { get; set; }
        public float DETAIL_DON_GIA_BH { get; set; }

        public float DETAIL_TYLE_DV { get; set; }
        public float DETAIL_TYLE_BHYT { get; set; }
        public float DETAIL_THANH_TIEN_BV { get; set; }
        public float DETAIL_THANH_TIEN_BH { get; set; }
         
        public float DETAIL_QUY_BHYT { get; set; }
        public float DETAIL_NGUOI_BENH_CUNG_TRA { get; set; }
        public float DETAIL_KHAC { get; set; }
        public float DETAIL_NGUOI_BENH_TU_TRA { get; set; }
    } 
    public class Report_BangKeRaVien
    {
        public double ALL_TONG_THANH_TIEN_BV { get; set; }
        public float ALL_TONG_THANH_TIEN_BH { get; set; }
        public float ALL_TONG_QUY_BHYT { get; set; }
        public float ALL_TONG_NGUOI_BENH_CUNG_TRA { get; set; }
        public float ALL_TONG_KHAC { get; set; }
        public float ALL_TONG_NGUOI_BENH_TU_TRA { get; set; }


        public string parenName { get; set; }
        public string orgName { get; set; }
        public string orgKhoa { get; set; }
        public string orgMaKhoa { get; set; }


        public string maBenhNhan { get; set; }
        public string soPhieuThuoc { get; set; }
        public string maBenhAn { get; set; }

        public string tenBenhNhan { get; set; }
        public string NGAYSINH { get; set; }
        public string THOIGIAN_BD { get; set; }
        public string THOIGIAN_KT { get; set; }
        public string DIACHIBHYT { get; set; }
        public string NGAYKHAM { get; set; }
        public string CAP_CUU { get; set; }
        public string DUNG_TUYEN { get; set; }
        public string NOI_CHUYEN_DEN { get; set; }
        public string NOI_CHUYEN_DI { get; set; }
        public string THONG_TUYEN { get; set; }
        public string TRAI_TUYEN { get; set; }

        public string MACHANDOANRAVIEN { get; set; }
        public string MACHANDOANRAVIEN_KEMTHEO { get; set; }
        public string DU5NAM { get; set; } 
        

        public string DKKCB { get; set; }

        public string tuoi { get; set; }
        public string dvTuoi { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string checkVp { get; set; }
        public string soThe1 { get; set; }
        public string soThe2 { get; set; }
        public string soThe3 { get; set; }
        public string soThe4 { get; set; }
        public string soThe5 { get; set; }
        public string maKCBBD { get; set; }
        public string dungTuyen { get; set; }
        public string hanThe { get; set; }
        public string chanDoan { get; set; }
        public string chanDoanPhu { get; set; }
        public string ngayMauBenhPham { get; set; }
        public string nguoiTao { get; set; }
        //Detail
        public string rowNum { get; set; }
        public string tenDichVu { get; set; }
        public string soLuong { get; set; }
        public string tenDvt { get; set; }
        public string cachDung { get; set; }

        public List<Report_CPKB> list { get; set; } 
    }

}
