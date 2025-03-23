using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPT.HIS.QTHethong.Class
{
    public class MenuFunc
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string text { get; set; }
        public string hlink { get; set; }
        public string options { get; set; }
        public List<MenuFunc> children { get; set; }
    }


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
}
