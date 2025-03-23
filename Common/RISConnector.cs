using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Collections.Generic;

namespace VNPT.HIS.Common
{
    public class RISConnector
    {
        public static string RIS_CONNECTION_TYPE = "";
        public static string RIS_USERNAME = "";
        public static string RIS_SECRET_KEY = "";
        public static string RIS_SERVICE_DOMAIN_NAME = "";

        public static string RIS_CANCEL_REQUEST = "/api/public/request/cancel";
        public static string RIS_GET_DICOM_VIEWER = "/api/public/dicomViewer";
        public static string RIS_DELETE_REQUEST = "/api/public/request/delete";

        public static Dictionary<string, string> localVal;
        public static string labToken = "";
        public static string labTokenExpiryTime = "";
        public static string getHash(string code) 
        {
            string input = code + RIS_SECRET_KEY; 
            Hashids hashids = new Hashids(input); 
            var date = Func.Parse(Func.getSysDatetime("yyyyMMdd"));
            return hashids.Encode(date);
        }
        public static void loadRISConfig()
        {
            string key = "['RIS_CONNECTION_TYPE','RIS_USERNAME','RIS_SECRET_KEY','RIS_SERVICE_DOMAIN_NAME','RIS_CANCEL_REQUEST','RIS_GET_DICOM_VIEWER','RIS_DELETE_REQUEST']";

            DataTable dt = new DataTable();
            string result = RequestHTTP.call_ajaxCALL_SP_S_result("CLS02C001.RISC", key);
            dt = (DataTable)JsonConvert.DeserializeObject("[" + result + "]", (typeof(DataTable)));
            if (dt.Rows.Count > 0)
            {
                RIS_CONNECTION_TYPE = dt.Rows[0]["RIS_CONNECTION_TYPE"].ToString();
                RIS_USERNAME = dt.Rows[0]["RIS_USERNAME"].ToString();
                RIS_SECRET_KEY = dt.Rows[0]["RIS_SECRET_KEY"].ToString();
                RIS_SERVICE_DOMAIN_NAME = dt.Rows[0]["RIS_SERVICE_DOMAIN_NAME"].ToString();


                if (dt.Columns.Contains("RIS_CANCEL_REQUEST") && !string.IsNullOrEmpty(dt.Rows[0]["RIS_CANCEL_REQUEST"].ToString()))
                    RIS_CANCEL_REQUEST = dt.Rows[0]["RIS_CANCEL_REQUEST"].ToString();
                if (dt.Columns.Contains("RIS_GET_DICOM_VIEWER") && !string.IsNullOrEmpty(dt.Rows[0]["RIS_GET_DICOM_VIEWER"].ToString()))
                    RIS_GET_DICOM_VIEWER = dt.Rows[0]["RIS_GET_DICOM_VIEWER"].ToString();
                if (dt.Columns.Contains("RIS_DELETE_REQUEST") && !string.IsNullOrEmpty(dt.Rows[0]["RIS_DELETE_REQUEST"].ToString()))
                    RIS_DELETE_REQUEST = dt.Rows[0]["RIS_DELETE_REQUEST"].ToString();
            }
        }

        public static string Send_RIS_DELETE_REQUEST(string soPhieu, string dataJson)
        {
            if (RIS_CONNECTION_TYPE == "") loadRISConfig();

            string ret = "";
            try
            {
                string requestUrl = RIS_SERVICE_DOMAIN_NAME + RIS_DELETE_REQUEST;
                if (RIS_SERVICE_DOMAIN_NAME != "" && RIS_DELETE_REQUEST != "")
                {
                    HttpWebRequest webRequest = null;
                    HttpWebResponse webResponse = null; 

                    webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
                    webRequest.Method = "POST";
                    webRequest.ContentType = "application/json; charset=utf-8";
                    string hash =getHash(soPhieu);
                    webRequest.Headers.Add("Ris-Access-Hash", hash);
                    webRequest.Headers.Add("Identify-Code", soPhieu);

                    var encoding = new UTF8Encoding();
                    var bytes = Encoding.GetEncoding("UTF-8").GetBytes(dataJson);
                    webRequest.ContentLength = bytes.Length;
                    using (var writeStream = webRequest.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }

                    using (webResponse = (HttpWebResponse)webRequest.GetResponse())
                    {
                        if (webResponse.StatusCode != HttpStatusCode.OK)
                        {
                            var message = String.Format("Request failed. Received HTTP {0}", webResponse.StatusCode);
                            throw new ApplicationException(message);
                        }
                        using (StreamReader responseStream = new StreamReader(webResponse.GetResponseStream()))
                        {
                            ret = responseStream.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return ret;
        }
        public static string RequestPOST(string soPhieu, string dataJson)
        {
            if (RIS_CONNECTION_TYPE == null) loadRISConfig();
            if (RIS_SERVICE_DOMAIN_NAME == "" || RIS_DELETE_REQUEST == "") return "";
            
            string url = RIS_SERVICE_DOMAIN_NAME + RIS_DELETE_REQUEST;

            string ret = "";
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

                request.ContentType = "application/json";
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Timeout = 8000;
                string hash = getHash(soPhieu);
                //request.Headers.Add("Ris-Access-Hash", hash);
                //request.Headers.Add("Identify-Code", soPhieu);
                //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                //{
                //    streamWriter.Write(dataJson);
                //    streamWriter.Flush();
                //    streamWriter.Close();
                //}

                using (HttpWebResponse webresponse = request.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(webresponse.GetResponseStream()))
                    {
                        ret = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ret;
        }

    }

}
