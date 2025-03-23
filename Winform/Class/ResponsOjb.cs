using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPT.HIS.MainForm.Class
{
    
    
    


    class DSCauhinhUser
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<CauhinhUser> rows { get; set; }
    }
    class CauhinhUser
    {
        public int RN { get; set; }
        public int CSYT_ID { get; set; }
        public string USER_NAME { get; set; }
        public string MACAUHINH { get; set; }
        public string TENCAUHINH { get; set; }
        public string CAUHINHID { get; set; }
        public string GIATRI_THIETLAP { get; set; }
        public string MOTA { get; set; }
    }

    class CauhinhUserDetails
    {
        public int CSYT_ID { get; set; }
        public string USER_NAME { get; set; }
        public string MACAUHINH { get; set; }
        public string TENCAUHINH { get; set; }
        public string CAUHINHID { get; set; }
        public string GIATRI_THIETLAP { get; set; }
        public string MOTA { get; set; }
    }

    public class KhoaPhong_Detail
    {
        public String KHOA_NAME { get; set; }
        public String PHONG_NAME { get; set; }
        public String TT_HOTRO { get; set; }
        
    }
     
}
