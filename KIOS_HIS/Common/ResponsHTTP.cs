using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNPT.HIS.Common
{
    public class ResponsObj
    {
        public int error_code { get; set; }
        public String error_msg { get; set; }
        public String out_var { get; set; }
        public String result { get; set; }
        //public User_Detail detail { get; set; }
    }

    public class ResponsObj_login
    {
        public int error_code { get; set; }
        public String error_msg { get; set; }
        public String out_var { get; set; }
        public UserLogin result { get; set; }
        //public User_Detail detail { get; set; }
    }
    public class ResponsList
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public object rows { get; set; }

    }

    public class UserLogin
    {
        public String USER_ID { get; set; }
        public String USER_NAME { get; set; }
        public String USER_PWD { get; set; }
        public String FULL_NAME { get; set; }
        public String USER_GROUP_ID { get; set; }
        public String USER_LEVEL { get; set; }
        public String OFFICER_ID { get; set; }
        public String ENCRYPT_CHECK { get; set; }
        public String NOTE { get; set; }
        public String COMPANY_ID { get; set; }
        public String STATUS { get; set; }
        public String HOSPITAL_ID { get; set; }
        public String DEPT_ID { get; set; }
        public String DB_NAME { get; set; }
        public String DB_SCHEMA { get; set; }
        public String PROVINCE_ID { get; set; }
        public String ORG_ADDRESS { get; set; }
        public String ORG_TAX_CODE { get; set; }
        public String ORG_BANK_ACCOUNT { get; set; }
        public String ORG_TEL { get; set; }
        public String ORG_FAX { get; set; }
        public String ORG_EMAIL { get; set; }
        public String ORG_WEBSITE { get; set; }
        public String START_PAGE { get; set; }
        public String HOSPITAL_CODE { get; set; }
        public String UUID { get; set; }
    }
    public class MenuFunc
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string text { get; set; }
        public string hlink { get; set; }
        public string options { get; set; }
        public List<MenuFunc> children { get; set; }
        public MenuFunc(string text, string hlink, string options, string icon)
        {
            this.text = text;
            this.hlink = hlink;
            this.options = options;
            this.icon = icon;
        }        
        public void addChildren(MenuFunc child)
        {
            if (children == null) children = new List<MenuFunc>();
            children.Add(child);
        }
    }

    public class DsBV_BHYT
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<BV_BHYT> rows { get; set; }

    }
    public class BV_BHYT
    {
        public int RN { get; set; }
        public string BENHVIENKCBBD { get; set; }
        public string TENBENHVIEN { get; set; }
        public string DIACHI { get; set; }
    }

    public class DsBenh_ChuanDoan
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<Benh_ChuanDoan> rows { get; set; }

    }
    public class Benh_ChuanDoan
    {
        public int RN { get; set; }
        public string ICD10CODE { get; set; }
        public string ICD10NAME { get; set; }
    }
    public class DsTinhHuyenXa
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<tinhhuyenxa> rows { get; set; }

    }
    public class tinhhuyenxa
    {
        public int RN { get; set; }
        public string VALUE { get; set; }
        public string NAME { get; set; }
        public string TENVIETTATDAYDU { get; set; }
    }


    public class Dia_Chi
    {
        public String THON_PHO { get; set; }
        public String DIABANID { get; set; }
        public String MA_DIACHI { get; set; }
        public String DIA_CHI { get; set; }
        public String MA_TINH { get; set; }
        public String ID_TINH { get; set; }
        public String TEN_TINH { get; set; }
        public String MA_HUYEN { get; set; }
        public String ID_HUYEN { get; set; }
        public String TEN_HUYEN { get; set; }
        public String MA_XA { get; set; }
        public String ID_XA { get; set; }
        public String TEN_XA { get; set; }
    }


    public class ojbDatarowview
    {
        public int id { get; set; }
        public string key { get; set; }
        public System.Data.DataRowView drv { get; set; } 
    }
}
