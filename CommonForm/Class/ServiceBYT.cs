using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions; 
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Data;
using VNPT.HIS.Common;
using DevExpress.XtraPrinting.HtmlExport.Native;

namespace VNPT.HIS.CommonForm
{
    public class ServiceBYT
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string _soapEnvelope = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Body></soap:Body></soap:Envelope>";

        // Biến dùng cho ws Bộ y tế
        public static string ServiceBYT_BYTDAYDL = ""; // có cấu hình đẩy cổng BYT ko?
        public static string ServiceBYT_BYTSTOPCHUCNANG = "";
        public static string ServiceBYT_TECHKCBBD = "";

        public static string ServiceBYT_Url = "";
        public static string ServiceBYT_Username = "";
        public static string ServiceBYT_Password = "";
        public static string ServiceBYT_MACSYT = "";
        
        public static bool Lay_thong_tin_ws_BYT()
        { 
            if (ServiceBYT_Url == "")
            {
                DataTable dtCauHinh = RequestHTTP.get_ajaxExecuteQueryO("NGT_STT_DT", DateTime.Now.ToString(Const.FORMAT_date1));
                if (dtCauHinh.Rows.Count > 0)
                {
                    ServiceBYT_Url = dtCauHinh.Rows[0]["BYTURL"].ToString();
                    ServiceBYT_BYTDAYDL = dtCauHinh.Rows[0]["BYTDAYDL"].ToString();
                    ServiceBYT_BYTSTOPCHUCNANG = dtCauHinh.Rows[0]["BYTSTOPCHUCNANG"].ToString();
                    ServiceBYT_TECHKCBBD = dtCauHinh.Rows[0]["TECHKCBBD"].ToString();
                    
                }
            }

            if (ServiceBYT_Username == "")
            {
                DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K047.TTTK", "1", 0);
                if (dt.Rows.Count > 0)
                {
                    ServiceBYT_Username = dt.Rows[0]["I_U1"].ToString();
                    ServiceBYT_MACSYT = dt.Rows[0]["I_U1"].ToString();
                    ServiceBYT_Password = dt.Rows[0]["I_P1"].ToString();

                    // nhân tiện lấy luôn cho BHYT
                    ServiceBHYT.ServiceBHYT_Username = dt.Rows[0]["I_U"].ToString();
                    ServiceBHYT.ServiceBHYT_PasswordMD5 = Func.GetMd5Hash(dt.Rows[0]["I_P"].ToString());
                    ServiceBHYT.ServiceBHYT_MACSKCB = dt.Rows[0]["I_MACSKCB"].ToString();                                 
                }
            }

            if (ServiceBYT_Url == "" || ServiceBYT_Username == "" || ServiceBYT_Password == "")
            {
                MessageBox.Show("Không lấy được thông tin truy cập cổng BYT!");
                return false;
            }
            else
                return true; 
        }
        public static object XML_BYT_TaoKhung(object objHeader, object objComponent, string mode)
        {
            var str = "";
            var str1 = "";
            var objAll = new Object();
            var objData = new Object();
            var objBody = new Object();
            var objCHECKIN = new Object();
            try
            {
                // Tuy vao the truyen vao ma tao khung phu hop; 
                switch (mode)
                {
                    case "1":
                    case "5":
                        objBody = new { CHECKIN = objComponent };
                        break;
                    case "2":
                        objBody = new { LAMDUNGTHE = objComponent };
                        break;
                    case "3":
                        objBody = new { CHECKOUT = objComponent };
                        break;
                    case "6":
                        objBody = new { NHANTTCT = objComponent };
                        break;
                    case "7":
                        objBody = new { LSKCB = objComponent };
                        break;
                    case "8":
                        objBody = new { LAMDUNGTHUOC = objComponent };
                        break;
                    default:
                        break;
                }

                objData = new { Header = objHeader, Body = objBody };
                objAll = new { Data = objData };

                str = JsonConvert.SerializeObject(objAll);
                XmlDocument xml = JsonConvert.DeserializeXmlNode(str);
                str1 = xml.InnerXml.ToString();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return str1;
        }

        public static bool WebSiteIsAvailable(string Url)
        {
            string Message = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Url);

            // Set the credentials to the current user account
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Method = "GET";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Do nothing; we're only testing to see if we can get the response
                }
            }
            catch (WebException ex)
            {
                Message += ((Message.Length > 0) ? "\n" : "") + ex.Message;
            }

            return (Message.Length == 0);
        }

        static private HttpWebRequest createWebRequest(String wsUrl, String wsMethod)
        {
            HttpWebRequest webRequest = null;
            try {
                webRequest = (HttpWebRequest)WebRequest.Create(wsUrl);
                webRequest.Headers.Add("SOAPAction", "\"http://tempuri.org/" + wsMethod + "\"");
                webRequest.Headers.Add("To", wsUrl);
                webRequest.Timeout = 20000;
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return webRequest;
        }

        public static string createSoapEnvelope(String wsMethod, String[] wsParam, String[] wsValue)
        {
            StringBuilder sb = null;
            try
            {
                string MethodCall = "<" + wsMethod + ">";
                string StrParameters = string.Empty;
                for (int i = 0; i < wsParam.Length; i++)
                {
                    StrParameters = StrParameters + "<" + wsParam[i] + ">" + DXHttpUtility.HtmlEncode(wsValue[i]) + "</" + wsParam[i] + ">";
                }

                MethodCall = MethodCall + StrParameters + "</" + wsMethod + ">";

                sb = new StringBuilder(_soapEnvelope);
                sb.Insert(sb.ToString().IndexOf("</soap:Body>"), MethodCall);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return sb.ToString();
        }

        static private string stripResponse(string wsMethod, string SoapResponse)
        {
            string RegexExtract = "";
            string s = "";
            try
            {
                if (wsMethod == "ws_dyncall")
                {
                    RegexExtract = @"<" + wsMethod + "Return xsi:type=\"soapenc:string\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\">(?<Return>.*?)</" + wsMethod + "Return>";
                    s = Regex.Match(SoapResponse, RegexExtract).Groups["Return"].Captures[0].Value;
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(SoapResponse);
                    XmlNodeList elemlist = doc.GetElementsByTagName(wsMethod + "Return");

                    s = elemlist[0].InnerXml;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return s;
        }

        public static string guiTTBDK(string wsUrl, string wsClassName, string wsMethod, string[] wsParam, string[] wsValue)
        {
            try
            {
                bool IsAlive = WebSiteIsAvailable(wsUrl);
                bool IsRetry = false;

                if (!IsAlive)
                {
                    if (IsRetry)
                    {
                        while (!IsAlive)
                        {
                            Thread.Sleep(1000);
                            IsAlive = WebSiteIsAvailable(wsUrl);
                        }
                    }
                    else
                        return "";
                }

                WebResponse response = null;

                string strResponse = "";

                //Create the request
                HttpWebRequest req = createWebRequest(wsUrl, wsMethod);

                //write the soap envelope to request stream
                using (Stream stm = req.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        String soap = createSoapEnvelope(wsMethod, wsParam, wsValue);
                        stmw.Write(soap);
                    }
                }

                //get the response from the web service
                response = req.GetResponse();

                Stream str = response.GetResponseStream();

                StreamReader sr = new StreamReader(str);

                strResponse = sr.ReadToEnd();

                var wsrt = stripResponse(wsMethod, DXHttpUtility.HtmlDecode(strResponse));
                wsrt = Encoding.UTF8.GetString(Convert.FromBase64String(wsrt));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(wsrt);
                XmlNodeList ErrorNumber = xmlDoc.GetElementsByTagName("Error_Number");
                XmlNodeList ErrorMessage = xmlDoc.GetElementsByTagName("Error_Message");

                return ErrorNumber[0].InnerText + ";" + ErrorMessage[0].InnerText;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return "";
            }
        }

        public static string tc_ls_KCB(string wsClassName, string wsMethod, string[] wsParam, string[] wsValue)
        {
            if (Lay_thong_tin_ws_BYT() == false) return "";

            string wsUrl = ServiceBYT_Url;
            try
            {
                bool IsAlive = WebSiteIsAvailable(wsUrl);
                bool IsRetry = false;

                if (!IsAlive)
                {
                    if (IsRetry)
                    {
                        while (!IsAlive)
                        {
                            Thread.Sleep(1000);
                            IsAlive = WebSiteIsAvailable(wsUrl);
                        }
                    }
                    else
                        return "";
                }

                WebResponse response = null;

                string strResponse = "";

                //Create the request
                HttpWebRequest req = createWebRequest(wsUrl, wsMethod);

                //write the soap envelope to request stream
                using (Stream stm = req.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stm))
                    {
                        String soap = createSoapEnvelope(wsMethod, wsParam, wsValue);
                        stmw.Write(soap);
                    }
                }

                //get the response from the web service
                response = req.GetResponse();

                Stream str = response.GetResponseStream();

                StreamReader sr = new StreamReader(str);

                strResponse = sr.ReadToEnd();

                var wsrt = stripResponse(wsMethod, DXHttpUtility.HtmlDecode(strResponse));
                wsrt = Encoding.UTF8.GetString(Convert.FromBase64String(wsrt));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(wsrt);

                var TabKhamBenh = xmlDoc.GetElementsByTagName("KHAM_BENH");
                var TableString = "";
                if (TabKhamBenh.Count != 0)
                {
                    var DataTable = ConvertXmlNodeListToDataTable(xmlDoc.GetElementsByTagName("KHAM_BENH"));
                    TableString = JsonConvert.SerializeObject(DataTable);
                }

                XmlNodeList ErrorNumber = xmlDoc.GetElementsByTagName("Error_Number");
                XmlNodeList ErrorMessage = xmlDoc.GetElementsByTagName("Error_Message");

                return ErrorNumber[0].InnerText + ";" + ErrorMessage[0].InnerText + (string.IsNullOrEmpty(TableString) ? "" : ";" + TableString);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return "";
            }
        }

        private static DataTable ConvertXmlNodeListToDataTable(XmlNodeList xnl)
        {
            DataTable dt = new DataTable();
            int TempColumn = 0;

            foreach (XmlNode node in xnl.Item(0).ChildNodes)
            {
                TempColumn++;
                DataColumn dc = new DataColumn(node.Name, System.Type.GetType("System.String"));
                if (dt.Columns.Contains(node.Name))
                {
                    dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
                }
                else
                {
                    dt.Columns.Add(dc);
                }
            }

            int ColumnsCount = dt.Columns.Count;
            for (int i = 0; i < xnl.Count; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < ColumnsCount; j++)
                {
                    dr[j] = xnl.Item(i).ChildNodes[j].InnerText;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
