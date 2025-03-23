using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using VNPT.HIS.Common;  
using System.Text;

namespace VNPT.HIS.CommonForm.Class
{
    public class ServiceCommonForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string check_DonThuoc(string khambenhid)
        {
            // {"func":"ajaxCALL_SP_S","params":["CHECK.DONTHUOC","{\"khambenhid\":\"970\"}"],"uuid":"2fd18ccd-d1ba-40cc-a7b3-dfbc5a1ee1f6"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "CHECK.DONTHUOC", "{\"khambenhid\":\"" + khambenhid + "\"}" }
                    );
                string resp = RequestHTTP.sendRequest(request);
                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return resultSet.result;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return "";
// {"result": "0",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}
        }

        public static string capNhat_XuTri(string json_in)
        {
// {"func":"ajaxCALL_SP_S","params":["NGT02K005_PKBNHAP","{\"TUVONGID\":\"555\",\"TUVONGLUC\":\"21/09/2017 14:07:00\",\"THOIGIANTUVONGID\":\"1\",\"ISCOKHAMNGHIEM\":\"0\",\"NGUYENNHANTUVONGID\":\"1\",\"NGUYENNHANTUVONGCHINH\":\"123\",\"CHANDOANGIAIPHAUTUTHI\":\"321\",\"NGAYTIEPNHAN\":\"10:18 14/10/2016\",\"DOITUONGBENHNHANID\":\"2\",\"DICHVUID\":null,\"MA_BHYT\":\"\",\"BHYT_LOAIID\":\"1\",\"BHYT_BD\":\"\",\"BHYT_KT\":\"\",\"MA_KCBBD\":\"\",\"TENBENHVIEN\":\"\",\"MACHANDOANBANDAU\":\"\",\"MACHANDOANRAVIEN\":\"A01.1\",\"CHANDOANRAVIEN\":\"Bệnh phó thương hàn A\",\"MACHANDOANRAVIEN_KHAC\":\"\",\"CHANDOANRAVIEN_KHAC\":\"\",\"MAXUTRIKHAMBENHID\":\"8\",\"KHOAID\":\"\",\"MAVIEN\":\"\",\"TENVIEN\":\"\",\"THOIGIANRAVIEN\":\"21/09/2017 14:59\",\"YEUCAUKHAM\":\"\",\"CHANDOANBANDAU\":\"\",\"LS\":null,\"XUTRIKHAMBENHID\":\"8\",\"KHOA\":null,\"TIEPNHANID\":\"1352\",\"HOSOBENHANID\":\"1536\",\"PHONGKHAMDANGKYID\":\"752\",\"BENHNHANID\":\"1509\",\"KHAMBENHID\":\"970\",\"DIACHI\":\"Xã An Hiệp-Huyện Ba Tri-Tỉnh Bến Tre\",\"NGAYTN\":\"14/10/2016 10:18\",\"MACHANDOANRAVIEN_KEMTHEO1\":\"\",\"MACHANDOANRAVIEN_KEMTHEO2\":\"\",\"PHONGKHAMID\":\"214\"}"],"uuid":"2fd18ccd-d1ba-40cc-a7b3-dfbc5a1ee1f6"}
        
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "NGT02K005_PKBNHAP", json_in }
                    );
                string resp = RequestHTTP.sendRequest(request);
                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return resultSet.result;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return "";
//{ "result": "970,-1",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}
        }

       
    }

}
