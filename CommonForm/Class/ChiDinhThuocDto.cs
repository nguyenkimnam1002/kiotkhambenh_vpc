using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VNPT.HIS.CommonForm.Class
{
    class ChiDinhThuocDto
    {
    }
    public class OPTS
    {
        public string khambenh_id { get; set; }
        public string user_id { get; set; }
        public string dept_id { get; set; }
        public string loaikedon { get; set; }
        public string khoaId { get; set; }
        public string phongId { get; set; }
        public string dichvuchaid { get; set; }
        public string maubenhpham_id { get; set; }
        public string phieudieutriid { get; set; }
        public string macdinh_hao_phi { get; set; }
        public string doituongbenhnhanid { get; set; }
        public string option { get; set; }
    }

    public class DataBN
    {
        public string BENHNHANID { get; set; } 
        public string MABENHNHAN { get; set; }
        public string TENBENHNHAN { get; set; }
        public string NAMSINH { get; set; }
        public string TIEPNHANID { get; set; }
        public string KHAMBENHID { get; set; }
        public string GHICHU_BENHCHINH { get; set; }
        public string HOSOBENHANID { get; set; }
        public string DIACHI { get; set; }
        public string NGHENGHIEP { get; set; }
        public string TRANGTHAIKHAMBENH { get; set; }
        public string MACHANDOANRAVIEN { get; set; }
        public string CHANDOANRAVIEN { get; set; }
        public string TENCHANDOANICD_KT { get; set; }
        public string MA_BHYT { get; set; }
        public string GIOITINH { get; set; }
        public string DOITUONGBENHNHANID { get; set; }
        public string TEN_DTBN { get; set; }
        public string TYLE_BHYT { get; set; }
        public string LOAITIEPNHANID { get; set; }
        public string THOIGIANVAOVIEN { get; set; }
        public string NGAYTIEPNHAN { get; set; }
        public string CHANDOANRAVIEN_KEMTHEO { get; set; }
        public string MACHANDOANVAOKHOA { get; set; }
        public string CHANDOANVAOKHOA { get; set; }
        public string CHANDOANVAOKHOA_KEMTHEO { get; set; }
        public string TRADU6THANGLUONGCOBAN { get; set; }
        public string DUOC_VAN_CHUYEN { get; set; }
        public string BHYT_KT { get; set; }
        public string INDONTHUOC { get; set; }
        public string SONGAYKEMAX { get; set; }
        public string SOLUONGTHUOCMAX { get; set; }
        public string KENHIEUNGAY { get; set; }
        public string INHUONGTHAN { get; set; }
        public string CHANHOATCHAT { get; set; }
        public string KECHUNGTHUOCVT { get; set; }
        public string LOAICHECK { get; set; }
        public string CACHDUNG { get; set; }
        public string NGAYKEMAX { get; set; }
        public string MAKHO { get; set; }
        public string KTCODONTHUOC { get; set; }
        public string KOKTKHICODONTHUOC { get; set; }
        public string HANTHE { get; set; }
        public string NGAYTHE { get; set; }
        public string CAPTHUOCLE { get; set; }
        public string TYLE_MIENGIAM { get; set; }

        public string CANHBAOPHACDO { get; set; }
        public string CHECKDIUNGTHUOC { get; set; }
        public string ANTIMCACHDUNGDT { get; set; }
        public string SUDUNGPHACDO { get; set; }
        public string AN_CBO_LOAITHUOC { get; set; }
        public string BACSI_KE { get; set; }
        public string SUDUNG_LIEUDUNG { get; set; }
        public string FORMAT_CD { get; set; }
        public string KIEUCHECK { get; set; }
        public string HOPDONGID { get; set; }
        public string KIEUCANHBAOTIENTAMUNG { get; set; }
        public string CHECKTRUNGHOATCHAT { get; set; }
        public string KIEUCHECK_HOATCHAT { get; set; }
        public string TUDONGIN { get; set; }
        public string NGT_LAMTRON_KETHUOC { get; set; }
        

    }

    public class DSThuocDto
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<DSThuoc> rows { get; set; }

    }

    public class DSThuoc
    {
        public int RN { get; set; }
        public string THUOCVATTUID { get; set; }
        public string TEN_THUOC { get; set; }
        public string HOATCHAT { get; set; }
        public string MAHOATCHAT { get; set; }
        public string TEN_DVT { get; set; }
        public string SLKHADUNG { get; set; }
        public string MA_THUOC { get; set; }
        public string GIA_BAN { get; set; }
        public string BIETDUOC { get; set; }
        public string DUONGDUNGID { get; set; }
        public string DUONG_DUNG { get; set; }
        public string HUONGDAN_SD { get; set; }
        public string TENKHO { get; set; }
        public string KHOAID { get; set; }
        public string NHOM_MABHYT_ID { get; set; }
        public string GIATRANBHYT { get; set; }
        public string KHOANMUCID { get; set; }
        public string TYLEBHYT_TVT { get; set; }
        public string CHOLANHDAODUYET { get; set; }
        public string LIEULUONG { get; set; }
        public string CHUY { get; set; }
        public string KETRUNGHOATCHAT { get; set; }
        public string CANHBAOSOLUONG { get; set; }
    }

    public class DSCachDungDto
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<DSCachDung> rows { get; set; }

    }

    public class DSCachDung
    {
        public int RN { get; set; }
        public string CACHDUNG { get; set; }
    }
}