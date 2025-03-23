using DevExpress.XtraPrinting.HtmlExport.Native;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public class ServiceHDDT
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string _soapEnvelope = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Body></soap:Body></soap:Envelope>";

        private static string INVOICES_URL_IMPORT = string.Empty;
        private static string INVOICES_WS_USER = string.Empty;
        private static string INVOICES_WS_PWD = string.Empty;
        private static string INVOICES_WS_USER_ACC = string.Empty;
        private static string INVOICES_WS_PWD_ACC = string.Empty;
        private static string INVOICES_WS_PATTERN = string.Empty;
        private static string INVOICES_WS_SERIAL = string.Empty;

        private static string wsMethodImportAndPublishInv = "ImportAndPublishInv";
        private static string wsMethodUpdateCus = "UpdateCus";

        public static void GetConfigs()
        {
            DataRow vpiConfig;
            var resultConfig = RequestHTTP.call_ajaxCALL_SP_O("VPI.LAY.CAUHINH", "$", 0);
            if (resultConfig != null && resultConfig.Rows.Count > 0)
            {
                vpiConfig = resultConfig.Rows[0];

                INVOICES_URL_IMPORT = vpiConfig["INVOICES_URL_IMPORT"].ToString();
                INVOICES_WS_USER = vpiConfig["INVOICES_WS_USER"].ToString();
                INVOICES_WS_PWD = vpiConfig["INVOICES_WS_PWD"].ToString();
                INVOICES_WS_USER_ACC = vpiConfig["INVOICES_WS_USER_ACC"].ToString();
                INVOICES_WS_PWD_ACC = vpiConfig["INVOICES_WS_PWD_ACC"].ToString();
                INVOICES_WS_PATTERN = vpiConfig["INVOICES_WS_PATTERN"].ToString();
                INVOICES_WS_SERIAL = vpiConfig["INVOICES_WS_SERIAL"].ToString();
            }
        }

        public static void SetConfigs(string iNVOICES_URL_IMPORT, string iNVOICES_WS_USER_ACC, string iNVOICES_WS_PWD_ACC, string iNVOICES_WS_USER, string iNVOICES_WS_PWD, string iNVOICES_WS_PATTERN, string iNVOICES_WS_SERIAL)
        {
            INVOICES_URL_IMPORT = iNVOICES_URL_IMPORT;
            INVOICES_WS_USER = iNVOICES_WS_USER;
            INVOICES_WS_PWD = iNVOICES_WS_PWD;
            INVOICES_WS_USER_ACC = iNVOICES_WS_USER_ACC;
            INVOICES_WS_PWD_ACC = iNVOICES_WS_PWD_ACC;
            INVOICES_WS_PATTERN = iNVOICES_WS_PATTERN;
            INVOICES_WS_SERIAL = iNVOICES_WS_SERIAL;
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

        public static string createSoapEnvelope(String wsMethod, String[] wsParam, String[] wsValue)
        {
            StringBuilder sb = null;
            try
            {
                string MethodCall = "<" + wsMethod + ">";
                string StrParameters = string.Empty;
                for (int i = 0; i < wsParam.Length; i++)
                {
                    StrParameters = StrParameters + "<" + wsParam[i] + ">" + wsValue[i] + "</" + wsParam[i] + ">";
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
                    XmlNodeList elemlist = doc.GetElementsByTagName(wsMethod + "Result");

                    s = elemlist[0].InnerXml;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return s;
        }

        public static HttpWebRequest CreateWebRequest(String wsUrl, String wsMethod)
        {
            HttpWebRequest webRequest = null;
            try
            {
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

        public static string CreateXMLInvData(Dictionary<string, string> data)
        {
            string kiTuXuongDong = "\n";

            string maPhieuThu = "";
            string maKhachHang = "";
            string tenKhachHang = "";
            string diaChiKhachHang = "";
            string maSoThue = "";
            string hinhThucThanhToan = "";
            string tenSanPham = "";
            string donViTinh = "";
            string soLuong = "";
            string donGia = "";
            string thanhTien = "";
            string tongTienTruocThue = "";
            string thueSuatGTGT = "";
            string tienThueGTGT = "";
            string tongTien = "";
            string tongTienBangChu = "";
            string ngayHoaDon = "";

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<Invoices>" + kiTuXuongDong);
            stringBuilder.Append("<Inv>" + kiTuXuongDong);
            stringBuilder.Append("<key>" + maPhieuThu + "</key>" + kiTuXuongDong);
            stringBuilder.Append("<Invoice>" + kiTuXuongDong);
            stringBuilder.Append("<CusCode>" + maKhachHang + "</CusCode>" + kiTuXuongDong);
            stringBuilder.Append("<CusName>" + tenKhachHang + "</CusName>" + kiTuXuongDong);
            stringBuilder.Append("<CusAddress>" + diaChiKhachHang + "</CusAddress>" + kiTuXuongDong);
            stringBuilder.Append("<CusTaxCode>" + maSoThue + "</CusTaxCode>" + kiTuXuongDong);
            stringBuilder.Append("<PaymentMethod>" + hinhThucThanhToan + "</PaymentMethod>" + kiTuXuongDong);
            stringBuilder.Append("<Products>" + kiTuXuongDong);
            stringBuilder.Append("<Product>" + kiTuXuongDong);
            stringBuilder.Append("<ProdName>" + tenSanPham + "</ProdName>" + kiTuXuongDong);
            stringBuilder.Append("<ProdUnit>" + donViTinh + "</ProdUnit>" + kiTuXuongDong);
            stringBuilder.Append("<ProdQuantity>" + soLuong + "</ProdQuantity>" + kiTuXuongDong);
            stringBuilder.Append("<ProdPrice>" + donGia + "</ProdPrice>" + kiTuXuongDong);
            stringBuilder.Append("<Amount>" + thanhTien + "</Amount>" + kiTuXuongDong);
            stringBuilder.Append("</Product>" + kiTuXuongDong);
            stringBuilder.Append("</Products>" + kiTuXuongDong);
            stringBuilder.Append("<Total>" + tongTienTruocThue + "</Total>" + kiTuXuongDong);
            stringBuilder.Append("<VATRate>" + thueSuatGTGT + "</VATRate>" + kiTuXuongDong);
            stringBuilder.Append("<VATAmount>" + tienThueGTGT + "</VATAmount>" + kiTuXuongDong);
            stringBuilder.Append("<Amount>" + tongTien + "</Amount>" + kiTuXuongDong);
            stringBuilder.Append("<AmountInWords>" + tongTienBangChu + "</AmountInWords>" + kiTuXuongDong);
            stringBuilder.Append("<ArisingDate>" + ngayHoaDon + "</ArisingDate>" + kiTuXuongDong);
            stringBuilder.Append("<Extra></Extra>" + kiTuXuongDong);
            stringBuilder.Append("<DanToc></DanToc>" + kiTuXuongDong);
            stringBuilder.Append("<GioiTinh></GioiTinh>" + kiTuXuongDong);
            stringBuilder.Append("<KhoaDieuTri></KhoaDieuTri>" + kiTuXuongDong);
            stringBuilder.Append("<SoBHYT></SoBHYT>" + kiTuXuongDong);
            stringBuilder.Append("<NguonKhacTra></NguonKhacTra>" + kiTuXuongDong);
            stringBuilder.Append("</Invoice>" + kiTuXuongDong);
            stringBuilder.Append("</Inv>" + kiTuXuongDong);
            stringBuilder.Append("</Invoices>");

            return stringBuilder.ToString();
        }

        public static  string ImportAndPublishIny(string xmlInvData, string convert)
        {
            string[] wsParam = new string[]
            {
                "Account",
                "ACpass",
                "xmlInvData",
                "username",
                "pass",
                "pattern",
                "serial",
                "convert"
            };

            string[] wsValue = new string[]
            {
                INVOICES_WS_USER_ACC,
                INVOICES_WS_PWD_ACC,
                xmlInvData,
                INVOICES_WS_USER,
                INVOICES_WS_PWD,
                INVOICES_WS_PATTERN,
                INVOICES_WS_SERIAL,
                convert
            };

            return SendRequest(INVOICES_URL_IMPORT, wsMethodImportAndPublishInv, wsParam, wsValue);
        }

        public static string UpdateCus(string xmlInvData, string convert)
        {
            string[] wsParam = new string[]
            {
                "xmlCusData",
                "username",
                "pass",
                "convert"
            };

            string[] wsValue = new string[]
            {
                xmlInvData,
                INVOICES_WS_USER,
                INVOICES_WS_PWD,
                convert
            };

            return SendRequest(INVOICES_URL_IMPORT, wsMethodUpdateCus, wsParam, wsValue);
        }

        public static string SendRequest(string wsUrl, string wsMethod, string[] wsParam, string[] wsValue)
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
                HttpWebRequest req = CreateWebRequest(wsUrl, wsMethod);

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

                return stripResponse(wsMethod, DXHttpUtility.HtmlDecode(strResponse));
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return "";
            }
        }
    }
}
