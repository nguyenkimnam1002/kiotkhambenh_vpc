using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPT.HIS.Common;
using System.Data;
using Newtonsoft.Json;
using System.Windows.Forms;

/**
 *   Service
 *   @ Author : HaNv
 *   @ CreateDate : 01/08/2017
 * */
namespace VNPT.HIS.Controls.Class
{
    public class ServiceChiDinhDichVu
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /**
             * Các trường hợp lấy dữ liệu
             *  - Lấy dữ liệu trực tiếp từ ws
             *  - Lấy dữ liệu từ cache: updateCache = false
             *  - Lấy dữ liệu và update cache: updateCache = true
             */
        public static DataTable ajaxCALL_SP_O(string sqlName, string paramsValue, int x, string tableCache = "", bool updateCache = false)
        {
            DataTable dt = new DataTable();
            try
            {
                // Kiểm tra lấy từ cache              
                if (!string.IsNullOrEmpty(tableCache))
                {
                    Const.SQLITE.CacheObject_Select(tableCache, out dt);
                    if (!updateCache)
                    {
                        if (dt.Rows.Count > 0) return dt;
                    }
                }
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { sqlName, paramsValue }, new int[] { x });
                string resp = RequestHTTP.sendRequest(request);
                ResponsObj resultSet = new ResponsObj();
                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                dt = (DataTable)JsonConvert.DeserializeObject(resultSet.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();

                //Cập nhật vào Cache
                if (!string.IsNullOrEmpty(tableCache)) Const.SQLITE.CacheObject_Create(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public static DataTable ajaxExecuteQuery(string sqlName, string[] nameList, string[] valueList, string tableCache = "", bool updateCache = false)
        {
            DataTable dt = new DataTable();
            try
            {
                // Kiểm tra lấy từ cache
                if (!string.IsNullOrEmpty(tableCache))
                {
                    Const.SQLITE.CacheObject_Select(tableCache, out dt);
                    if (!updateCache)
                    {
                        if (dt.Rows.Count > 0) return dt;
                    }
                }
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", sqlName }, nameList, valueList);
                string resp = RequestHTTP.sendRequest(request);
                dt = Func.fill_ArrayStr_To_Datatable(resp, tableCache);
                if (dt.Rows.Count == 0)
                {
                    dt.Columns.Add("col1");
                    dt.Columns.Add("col2");
                }
                //Cập nhật vào Cache
                if (!string.IsNullOrEmpty(tableCache)) Const.SQLITE.CacheObject_Create(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public static ResponsList ajaxExecuteQueryPaging(string sqlName, int page, int rows, string[] nameList, string[] valueList, string jsonFilter = "")
        {
            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { sqlName }, nameList, valueList);
                string data = 
                    "postData=" + request + (jsonFilter == "" ? "" : "&_search=true" + "&filters={\"groupOp\":\"AND\",\"rules\":" + jsonFilter + "}") +
                    "&rows=" + rows + "&page=" + page + "&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return ds;
        }

        public static DataTable ajaxExecuteQueryO(string sqlName, string param)
        {
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryO", new String[] { "", sqlName }, new string[] { "[0]" }, new string[] { param });
                string resp = RequestHTTP.sendRequest(request);

                dt = (DataTable)JsonConvert.DeserializeObject(resp, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public static string ajaxCALL_SP_S(string sqlName, string param)
        {
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { sqlName, param });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();
                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return ret;
        }

        public static string ajaxCALL_SP_I(string sqlName, string param)
        {
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { sqlName, param });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
            return ret;
        }
    }
}
