using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net;

using VNPT.HIS.Common;

namespace VNPT.HIS.NgoaiTru.Class
{
    public class ServicePhieuKhamBenh
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DataTable getThongTinKhamBenh(string KHAMBENHID, string PHONGID)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT02K005_PKBCT","2378$208",0],"uuid":"10c6c3d1-d9bf-41cb-b940-66435447415c"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NGT02K005_PKBCT", KHAMBENHID + "$" + PHONGID }, new int[] { 0 }
                    );
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(resultSet.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //{ "result": "[{"KHAMBENHID": "2378","TIEPNHANID": "2724","HOSOBENHANID": "3751","PHONGKHAMDANGKYID": "1762","MAHOSOBENHAN": "BA00002413","PHONGID": "208",
            //    "MABENHNHAN": "BN00001689","ORG_NAME": "PK Sản P.307","NGAYTIEPNHAN": "09:03 21/04/2017","NGAYTN": "21/04/2017 09:03","DOITUONGBENHNHANID": "2","MA_BHYT": "",
            //    "BHYT_BD": "","BHYT_KT": "","MA_KCBBD": "","TENBENHVIEN": "","DICHVUID": "1001","MACHANDOANBANDAU": "A00.9","CHANDOANBANDAU": "Bệnh tả, không xác định",
            //    "MACHANDOANRAVIEN": "C11.1","CHANDOANRAVIEN": "U ác của vách sau của hầu-mũi","GHICHU_BENHCHINH": "","MACHANDOANRAVIEN_KEMTHEO": "",
            //    "CHANDOANRAVIEN_KEMTHEO": "A00-Bệnh tả;A01.1-Bệnh phó thương hàn A","MACHANDOANRAVIEN_KEMTHEO1": "","CHANDOANRAVIEN_KEMTHEO1": "","MACHANDOANRAVIEN_KEMTHEO2": "",
            //    "CHANDOANRAVIEN_KEMTHEO2": "","MACHANDOANRAVIEN_KHAC": "","CHANDOANRAVIEN_KHAC": "A00-Bệnh tả;A01.1-Bệnh phó thương hàn A","XUTRIKHAMBENHID": "2",
            //    "YEUCAUKHAM": "43-Phá thai","MAXUTRIKHAMBENHID": "2","THOIGIANRAVIEN": "18/09/2017 11:19","BHYT_LOAIID": null,"BENHNHANID": "2762","DIACHI":
            //    "Xã Nghĩa Lộ-Huyện Cát Hải-TP Hải Phòng","SOTHUTU": "0003","KHOA": "27","TAT_POPUP_TTRAVIEN": "1","TUDONGINBANGKE": "0","XTMACDINH": "-1","SUB_DTBNID": null}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }





    }
}
