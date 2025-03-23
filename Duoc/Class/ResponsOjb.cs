using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPT.HIS.Duoc.Class
{
    class ResponsOjb { }

    public class DsPhieu
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<phieu> rows { get; set; }

    }
    public class phieu
    {
        public int RN { get; set; }
        public int NHAPID { get; set; }
        public int XUATID { get; set; }
        public string TTPHIEUNX
        {
            get;
            set;
        }
        public int DOIUNGID { get; set; }
        public string NHAPXUATID { get; set; }
        public string TRANGTHAIID { get; set; }
        public string MA { get; set; }
        public string KIEU { get; set; }
        public string TEN_KIEU { get; set; }
        public string SOCHUNGTU { get; set; }
        public string NGAYCT { get; set; }
        public string NGAYNX { get; set; }
        public string TRANGTHAI { get; set; }
        public string HINHTHUCID { get; set; }
        public string TENHINHTHUC { get; set; }
        public string TENKHO { get; set; }
        public string SOPHIEU { get; set; }
        public string MABENHNHAN { get; set; }
        public string TENBENHNHAN { get; set; }
        public string NGAYSINH { get; set; }
        public string GIOITINH { get; set; }        
        public string NGUOIDUYET { get; set; }
        public string MAHOSOBENHAN { get; set; }       

    }


   
    public class DsCTPhieu
    {
        public int NHAPID { get; set; }
        public int XUATID { get; set; }
        public int TRANGTHAIID { get; set; }

        public string NOILAP { get; set; }
        public string NGAYNX { get; set; }
        public string NGUOINX { get; set; }
        public string NGUOIDUYET { get; set; }
        public string LANIN { get; set; }
        public string SOCHUNGTU { get; set; }
        public string NGAYCHUNGTU { get; set; }
        public string MA { get; set; }
        public string NHACUNGCAP { get; set; }
        public string CHIETKHAU { get; set; }
        public string GHICHU { get; set; }
        public string TIENDON { get; set; }
        public string TONGCONG { get; set; }
        public string TONGTIENDATRA { get; set; }
        public string TRANGTHAI { get; set; }
        public string MANHAP { get; set; }
        public string MAXUAT { get; set; }
        

    }

    public class DsCTThuoc
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<ctthuoc> rows { get; set; }

    }
    public static class pram 
    {
        public static string pr_nhapxuatid { get; set; }
    }
    public class ctthuoc
    {
        public int RN { get; set; }
        public int NHAPXUATCTID { get; set; }

        public string MA
        {
            get;
            set;
        }
     
        public string TEN { get; set; }
        public string CHOLANHDAODUYET { get; set; }
        public string TEN_DVT { get; set; }
        public string SOLUONG { get; set; }
        public string SOLUONGDUYET { get; set; }
        public string SLKHADUNG { get; set; }
        public string GIANHAP { get; set; }
        public string THANHTIEN { get; set; }
        public string XUATVAT { get; set; }
        public string SOLO { get; set; }
        public string CHUY { get; set; }
        public string NGUOIDUYET { get; set; }
        public string TENPHONGLUU { get; set; }
       

    }

    

    //textBox1.Text = myString.Replace("\n", Environment.NewLine);

   

}
