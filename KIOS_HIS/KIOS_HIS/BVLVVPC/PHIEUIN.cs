using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNPT.HIS.Common;

namespace KIOS_HIS.BVLVVPC
{
    public class PHIEUIN
    {
		public string SoYTe;
        public string TenBV;
        public string HoTen;
        public string TheBH;
        public string Muc;
        public string STT;
        public string PhanKhu;
        public string InChung;
        public string KhamUT;
        public string STTHienTai;
        public string TenPKham;
        public string LoaiKham;                                // kham thuong, kham uu tien; 
        public string Ngay;
        public string Gio;
        public string MaBN;
        public string GioiTinh;
        public string NamSinh;
        public string SDT;
        public string DiaChi;
        public string DoiTuong;								// BHYT, VPI ...
        public string NgayGio;
        public string Tuoi;
        public string MaKCBBD;
        public string MaBenhNhan; 

        public PHIEUIN(string hoTen, string theBH, string muc, string sTT, string phanKhu, string inChung, string khamUT, 
                            string sTTHienTai, string tenPKham, string loaiKham, string maBN, string gioiTinh, 
                            string namSinh, string sDT, string diaChi, string doiTuong, string tuoi, string maKCBBD, string maBenhNhan)
        {
            HoTen = hoTen;
            TheBH = theBH;
            Muc = muc;
            STT = sTT;
            PhanKhu = phanKhu;
            InChung = inChung;
            KhamUT = khamUT;
            STTHienTai = sTTHienTai;
            TenPKham = tenPKham;
            LoaiKham = loaiKham;
            MaBN = maBN;
            GioiTinh = gioiTinh;
            NamSinh = namSinh;
            SDT = sDT;
            DiaChi = diaChi;
            DoiTuong = doiTuong;
            Tuoi = tuoi;
            MaKCBBD = maKCBBD;
            MaBenhNhan = maBenhNhan; 

            SoYTe = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_SYT");
            TenBV = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KIOS_APP_TENBV");
            Ngay = DateTime.Now.ToString("dd/MM/yyyy");
            Gio = DateTime.Now.ToString("hh:mm:ss tt");
            NgayGio = DateTime.Now.ToString("dd/MM/yyyy, HH:mm"); 
        }


    }
}
