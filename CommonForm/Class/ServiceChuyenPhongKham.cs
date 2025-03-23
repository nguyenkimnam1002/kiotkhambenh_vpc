using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.CommonForm.Class
{
    class ServiceChuyenPhongKham
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static string getCauHinh(String ten)
        {
            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_HIENTHI_GOIKHAM"],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "COM.CAUHINH", ten });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
            // {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}
        }

        public static string getSP_S(String funs, String ten)
        {
            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_HIENTHI_GOIKHAM"],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { funs, ten });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
            // {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}
        }


        public static string getSaveDaTa(String funs, String ten)
        {
            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_HIENTHI_GOIKHAM"],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { funs, ten });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
            // {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}
        }


        public static DataTable getPhongKham(bool getFromCache, string value)
        {
            // {"func":"ajaxExecuteQuery","params":["","DV.BHYT.001"],"options":[{"name":"[0]","value":"58"}],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            DataTable dt = new DataTable();
            // Kiểm tra lấy từ cache
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", "NGTPK.DV" }
                    , new String[] { "[0]" }
                    , new String[] { value });
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, "NGTPK_DV");

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public static DataTable getYcKham(bool getFromCache, string tblName, String param)
        {
            // {"func":"ajaxExecuteQuery","params":["","DV.BHYT.001"],"options":[{"name":"[0]","value":"58"}],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            DataTable dt = new DataTable();
            // Kiểm tra lấy từ cache
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", "NGTDV.002" }
                    , new String[] { "[0]" }
                    , new String[] { param });
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, tblName);

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            // [["1","Đúng tuyến"],["2","Đúng tuyến giới thiệu"]]
        }
    }
}
