using System; 
using Newtonsoft.Json;
using VNPT.HIS.Common;

namespace VNPT.HIS.MainForm.Class
{
    public class ServiceSelectDept
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region khoa-phong   
        
        public static bool setKhoaPhong(int idKhoa, int idPhong)
        {
            // {"func":"ajaxCALL_SP_I","params":["DEPT.P04","{\"DEPT_ID\":\"109\",\"SUBDEPT_ID\":\"542\"}"],"uuid":"4d67c150-d09c-4a03-971f-70c9129db2b0"}
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "DEPT.P04", "{\\\"DEPT_ID\\\":\\\"" + idKhoa + "\\\",\\\"SUBDEPT_ID\\\":\\\"" + idPhong + "\\\"}" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                //if (resultSet.result == "2") return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return false;
            }

            return true;

        }
        // Lấy dl Khoa cũ
        // {"func":"ajaxCALL_SP_I","params":["DEPT.P02",1],"uuid":"8b636e03-e251-45d6-bffd-d571c0b9ac69"}
        public static int getIdKhoa()
        {
            int ret = 0;
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "DEPT.P02", "1" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret = Convert.ToInt32(resultSet.result);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
        }

        // Lấy dl phòng cũ
        // {"func":"ajaxCALL_SP_I","params":["DEPT.P03",1],"uuid":"8b636e03-e251-45d6-bffd-d571c0b9ac69"}
        public static int getIdPhong()
        {
            int ret = 0;
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "DEPT.P03", "1" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret = Convert.ToInt32(resultSet.result);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
        }
        #endregion
         
    }
}
