using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPT.HIS.QTHethong;
using VNPT.HIS.Common;
using System.Data;
using Newtonsoft.Json;

namespace VNPT.HIS.QTHethong.Class
{
    class ServiceDMC18CHNV
    {
        //public static DSCauhinhUser getDSCauhinhUser(int page, int rows)
        //{
        //    DSCauhinhUser ds = new DSCauhinhUser();
        //    try
        //    {
        //        // {"func":"ajaxExecuteQuery","params":["","LOADPK.TIMKIEM"],"options":[],"uuid":"e50d4962-0e22-4407-979f-aebf2d140891"}
        //        string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "DMC18.CHNV.01" }
        //            , new String[] { }
        //            , new String[] { });

        //        string data = "_search=false" +
        //            //"&nd=1496306404630"+
        //           "&page=" + page +
        //           "&postData=" + request +
        //           "&rows=" + rows +
        //            //"&sidx="+
        //            "&sord=asc";

        //        string ret = RequestHTTP.getRequest(data);

        //        ds = JsonConvert.DeserializeObject<DSCauhinhUser>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return ds;
        //}

        public static DSCauhinhUser getDSCauhinhUser(int page, int rows)
        {
            DSCauhinhUser ds = new DSCauhinhUser();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "DMC18.CHNV.01" }
                    , new String[] { }
                    , new String[] { });

                string data = "_search=false" +
                    //"&nd=1496306404630"+
                   "&page=" + page +
                   "&postData=" + request +
                   "&rows=" + rows +
                    //"&sidx="+
                    "&sord=asc";

                string ret = RequestHTTP.getRequest(data);

                ds = JsonConvert.DeserializeObject<DSCauhinhUser>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
            }

            return ds;
        }

        #region tab Danh sách cau hinh user
        public static ResponsList getDSCauhinhNguoidung(int page, int number, string jsonFilter)
        {
            //_search	false
            //nd	1502676699603
            //page	1
            //postData		
//{"func":"ajaxExecuteQueryPaging","uuid":"c7077ea4-b264-4f1e-a446-6c8a94533718","params":["NGT02K001.EV001"
//],"options":[{"name":"[0]","value":"04/10/2016"},{"name":"[1]","value":"14/08/2017"},{"name":"[2]","value"
//:"4126"},{"name":"[3]","value":"49"},{"name":"[4]","value":""}]}
            //rows	100
            //sidx	
            //sord asc
            ResponsList ds = new ResponsList(); 
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "DMC18.CHNV.01" }
                    , new String[] { }
                    , new String[] { });

                string data = "page=" +page+
                   "&postData=" + request +
                   (jsonFilter == "" ? "" : "&_search=true" + "&filters={\"groupOp\":\"AND\",\"rules\":" + jsonFilter + "}") +
                   "&rows=" + number;

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        public static DataTable getChiTietUser(string cauhinhId)
        {  
            
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "DMC18.CHNV.02", cauhinhId + "$" }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                dt = MyJsonConvert.toDataTable(ret.result);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }



        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string insertOrUpdateCHUser(CauhinhUser chUser)
        {

            string resp = "-1";

            try
            {
                String chUserToJson =
                JsonConvert.SerializeObject(chUser);

                chUserToJson = chUserToJson.Replace("\"", "\\\"");
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "DMC18.CHNV.03", chUserToJson });
                string httpRes = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(httpRes, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret.result = ret.result.Trim();
                return ret.result;
            }
            catch (Exception ex)
            {
            }

            return resp;

        }
        #endregion
    }
}
