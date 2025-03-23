using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 *   DTO cho form chỉ định dịch vụ
 *   @ Author : HaNv
 *   @ CreateDate : 01/08/2017
 * */
namespace VNPT.HIS.Controls.Class
{
    public class Options
    {
        //dich vu khac
        public string CHIDINHDICHVU { get; set; }
        public string LOAIDICHVU { get; set; }
        public string LOAIPHIEUMBP { get; set; }
        //dich vu cls-xn-cdha
        public string MODEFUNCTION { get; set; }
        public string BENHNHANID { get; set; }
        public string KHAMBENHID { get; set; }
        public string TIEPNHANID { get; set; }
        public string HOSOBENHANID { get; set; }
        public string DOITUONGBENHNHANID { get; set; }
        public string LOAITIEPNHANID { get; set; }
        //cap nhat dich vu
        public string MAUBENHPHAMID { get; set; }
        //cap nhat dich vu cls-xn-cdha
        public string LOAIPHIEU { get; set; }
        //bo sung cho phieu phu thu
        public string DICHVUKHAMBENHID { get; set; }
        public string MAUBENHPHAMCHAID { get; set; }
        public string SUBDEPTID { get; set; }
        public string DEPTID { get; set; }
        public string PHIEUDIEUTRIID { get; set; }
        public string DICHVUIDKHAC { get; set; }
        public string SUBDEPTID_LOGIN { get; set; }
        public string SUBDEPTNAME_LOGIN { get; set; }
        public string MAGIUONG { get; set; }
        public string LOAIGIUONGID { get; set; }
        public string MODEKHOA { get; set; }
    }

    public class LabRequestSet{
	    public string OrderID { get; set; }
	    public string OrderDate { get; set; }
	    public string OrderTime { get; set; }
	    public string OrderDoctor { get; set; }
	    public string PatientCode { get; set; }
	    public string PatientName { get; set; }
	    public string Sex { get; set; }
	    public string Birthday { get; set; }
	    public string Address { get; set; }
	    public string Phone { get; set; }
	    public string SocialNumber { get; set; }
	    public string FromDate { get; set; }
	    public string ToDate { get; set; }
	    public string Department { get; set; }
	    public string Ward { get; set; }
	    public string Bed { get; set; }
	    public string Emergency { get; set; }
	    public string RightLine { get; set; }
	    public string SID { get; set; }
        public List<string> TestCodeList { get; set; }
    }

    public class LISToken
    {
        public LISTokenSub result { get; set; }
        public String error_code { get; set; }
        public String error_msg { get; set; }
    }

    public class LISTokenSub
    {
        public String access_token { get; set; }
        public String expiry_time { get; set; }
    }

    public class PhieuMau
    {
        public String TEN_MAUPHIEU { get; set; }
        public String KHAMBENHID { get; set; }
        public String LOAINHOM_MAU { get; set; }
        public DataTable DS_DICHVU { get; set; }
    }
}
