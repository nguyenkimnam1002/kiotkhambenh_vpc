using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Net;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.BaoCao.Class
{
    class ServiceViewReport
    {
        String str = EncodeTo64("[{\"name\":\"VPARD_tungay\",\"type\":\"Date:D\",\"value\":\"06/11/2017\"},{\"name\":\"VPARD_denngay\",\"type\":\"Date:D\",\"value\":\"06/11/2017\"}]");
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static Stream GetStreamFromUrl(string url)
        {
            

            return new MemoryStream(getByteArrFromUrl(url));


        }

       public static  byte[] getByteArrFromUrl(string url){

           HttpWebRequest request = null;
           HttpWebResponse response = null;
           byte[] retByte = null;

            request = (HttpWebRequest)WebRequest.Create(url);
            using (response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = String.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }
                WebClient client = new WebClient();

                // grab the response
                Stream responseStream = response.GetResponseStream();
                MemoryStream mStream = new MemoryStream();
                using (mStream)
                {
                    var input = responseStream;

                    var buffer = new byte[8 * 1024];

                    int len;
                    while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        mStream.Write(buffer, 0, len);
                    }
                }

                retByte = mStream.ToArray();

            }
            return retByte;
       }


        public static string ToHex(string str)
        {
                string ret = "0x";
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                for (int i = 0; i < bytes.Length; i++)
                {
                    int so = bytes[i] & 0xFF;
                    string stringValue = Char.ConvertFromUtf32(so);
                    string hexValue = so.ToString("X");
                    //System.Console.WriteLine("-- " + so + " : " + hexValue);
                    ret += hexValue;
                }
                return ret;
            }

        public static String getUrlReport(string reportID, string type, DataTable par_table)
        {

            string reportParam = encoding_btoa(Newtonsoft.Json.JsonConvert.SerializeObject(par_table));

            string url = "";// Const.LinkReport + "/dreport/report/parameter/ParamBuilder?report_id=" + reportID + "&filetype=" + type + "&reportParam=" + reportParam + "&si=" + si;

            return url;
        }

        private static string encoding_btoa(string str)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(str);
            string toReturn = System.Convert.ToBase64String(bytes);
            return toReturn;
        }


        public static DataTable getDataToCbx(string sql)
        {
            // {"func":"ajaxExecuteQuery","params":["","NGT02K009.RV005"],"options":[],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            
            DataTable dt = new DataTable();
            try
            {
                //string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", "NGT02K009.RV005" });
                string request = "{\"func\":\"ajaxExecuteQuery\",\"params\":[\"jdbc/HISL2DS\",\"SQL:" + sql.Replace("[HID]", Const.local_user.HOSPITAL_ID)
                    + "\"],\"options\":[],\"uuid\":\"" + Const.local_user.UUID + "\",\"code\":\"thu@nnc\"}";
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, "");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "-1";
                    dr[1] = "Chọn";
                    dt.Rows.InsertAt(dr, 0);
                }

            }
            catch (Exception ex)
            {
            }
            return dt;
            // [["1","Chờ khám"],["4","Đang khám"],["9","Kết thúc khám"]]
        }

        public static String getUrlReport(string reportID, string param, string type)
        {
            string json_in = "";
            json_in += Func.json_item("CALLER", "WINFORM");
            json_in += Func.json_item("[HID]", Const.local_user.HOSPITAL_ID);
            json_in += Func.json_item("[UID]", Const.local_user.USER_ID);
            json_in += Func.json_item("UUID", Const.local_user.UUID);
            json_in += Func.json_item("[SCH]", Const.local_user.DB_SCHEMA);
            json_in += Func.json_item("thu@nnc", "{'uid':'" + Const.local_user.USER_ID + "','hid':'" + Const.local_user.HOSPITAL_ID
                + "','db':'" + Const.local_user.DB_NAME + "','schema':'" + Const.local_user.DB_SCHEMA + "'}");
            json_in = "{" + json_in.Substring(0, json_in.Length - 1) + "}";

            //"[HID]": "3",
            //"UUID": "3cb1a008-e2ff-4de9-be12-1f7eceb90217",
            //"[UID]": "4973",
            //"[SCH]": "HIS_DATA2",
            //"thu@nnc": "{'uid':'4973','hid':'3','db':'jdbc/HISL2DS','schema':'HIS_DATA2'}",
            //"dept_name": "Khoa Nội Tổng Hợp"}

            //string reportParam = encoding_btoa(param);
            string si = ToHex(json_in);

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(param);
            string reportParam = System.Convert.ToBase64String(plainTextBytes);

            //string type = Const.FileInPhieu;
            if (type.ToLower() == "doc") type = "rtf";

            //string url = Const.LinkReport.Replace("/reportDirect.jsp", "") + "/parameter/ParamBuilder?opt=1&pid=1&cid="
            //        + Const.local_user.HOSPITAL_ID + "&report_id=" + reportID + "&filetype=" + type
            //    + "&reportParam=" + reportParam + "&si=" + si;
            string url = Const.LinkReport + "?report_id=" + reportID + "&filetype=" + type
                    + "&opt=1&pid=1&cid="
                    + Const.local_user.HOSPITAL_ID + "&uid=" + Const.local_user.USER_GROUP_ID + "&usrid=" + Const.local_user.USER_ID
                    + "&db_name=" + Const.local_user.DB_NAME + "&db_schema=" + Const.local_user.DB_SCHEMA
                    + "&reportParam=" + reportParam + "&si=" + si;

            return url;
        }
    }
}
