using System;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace VNPT.HIS.Common
{
    public class RequestHTTP
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Login
        public static String Login_MakeParam(String func, params String[] listParam)
        {
            String strParam = "{\"func\":\"" + func + "\",\"uuid\":\"" + "" + "\",\"params\":[";
            if (listParam.Length > 0)
            {
                for (int i = 0; i < listParam.Length - 1; i++)
                {
                    strParam += "\"" + listParam[i] + "\",";
                }
                strParam += "\"" + listParam[listParam.Length - 1] + "\"";
            }
            strParam += "]}";

            return strParam;
        }
        public static UserLogin Login_Service(string username, string password)
        {
            try
            {
                string param = Login_MakeParam("doLogin", new String[] { "{?=call prc_login(?2S,?3S)}", username, password });
                //  {"func":"doLogin","uuid":"","params":["{?=call prc_login(?2S,?3S)}","winform","1"]}
                // {"func":"doLogin","uuid":"","params":["{?=call prc_login(?2S,?3S)}","ttkha_admin","Gppm2#2018"]}

                String resp = sendRequest(param);
                UserLogin user;

                //Kiểu cũ
                try
                {
                    ResponsObj resultSet = new ResponsObj();
                    resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    user = JsonConvert.DeserializeObject<UserLogin>(resultSet.result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
                catch (Exception ex)
                {
                    ResponsObj_login resultSet_login = JsonConvert.DeserializeObject<ResponsObj_login>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    user = resultSet_login.result;// JsonConvert.DeserializeObject<UserLogin>(resultSet.result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                }
                return user;
            }
            catch (Exception ex)
            {
            }
            return null;
            //{
            //"USER_ID": "2124",
            //"USER_NAME": "SNHNM.ADMIN",
            //"USER_PWD": "C4CA4238A0B923820DCC509A6F75849B",
            //"FULL_NAME": "Quản trị hệ thống bệnh viện",
            //"USER_GROUP_ID": "29",
            //"USER_LEVEL": "3",
            //"OFFICER_ID": "4239",
            //"ENCRYPT_CHECK": "1",
            //"NOTE": "978",
            //"COMPANY_ID": "978",
            //"STATUS": "1",
            //"HOSPITAL_ID": "978",
            //"DEPT_ID": "3552",
            //"DB_NAME": "jdbc/HISL2DS",
            //"DB_SCHEMA": "HIS_SANNHI_HNM",
            //"PROVINCE_ID": "3500000000",
            //"ORG_ADDRESS": "Đường Trường Chinh, Phường Minh khai, T.P Phủ lý, Tỉnh Hà Nam",
            //"ORG_TAX_CODE": "0700778412",
            //"ORG_BANK_ACCOUNT": null,
            //"ORG_TEL": null,
            //"ORG_FAX": null,
            //"ORG_EMAIL": null,
            //"ORG_WEBSITE": null,
            //"START_PAGE": "/main/manager.jsp?func=../main/frmMain",
            //"HOSPITAL_CODE": "35148",
            //"ORG_LOGO": "data:image/jpeg;base64,/9j/..."}
        }
        public static List<MenuFunc> Login_GetUserFunc()
        {
            // {"func":"ajaxCALL_SP_X","uuid":"0e7a5c17-8d7a-472c-95e5-27669393c2df","params":["GET_MENUCONTENT","DONTIEPNGT$",0]}
            List<MenuFunc> listmenu = new List<MenuFunc>();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_X", new String[] { "GET_MENUCONTENT", Const.local_user.USER_ID + "$" }, new int[] { 0 }
                    );
                string resp = RequestHTTP.sendRequest(request);
                listmenu = JsonConvert.DeserializeObject<List<MenuFunc>>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                //DataTable dt = (DataTable)JsonConvert.DeserializeObject(resp, (typeof(DataTable)));
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return listmenu;
            //{"result": "[{ \"id\" : \"1\",\"icon\":\"icon-user\", \"text\" : \"Há»‡ thá»‘ng\",\"hlink\":\"#\",\"options\":\"0\",\"children\" : [{ \"id\" : \"101\",\"icon\":\"icon-user\", \"text\" : \"ÄÄƒng xuáº¥t\",\"hlink\":\"/vnpthis/login/login.jsp\",\"options\":\"0\"} ,{ \"id\" : \"102\",\"icon\":\"icon-user\", \"text\" : \"Äá»•i máº­t kháº©u\",\"hlink\":\"/vnpthis/main/manager.jsp?func=../admin/changePassword\",\"options\":\"0\"} ,{ \"id\" : \"103\",\"icon\":\"icon-user\", \"text\" : \"Thiáº¿t láº­p phÃ²ng\",\"hlink\":\"/vnpthis/main/manager.jsp?func=../admin/SelDept\",\"options\":\"0\"} ,{ \"id\" : \"105\",\"icon\":\"icon-user\", \"text\" : \"Cáº¥u hÃ¬nh xuáº¥t thuá»‘c/váº­t 
        }
        public static int getIdKhoa()
        {
            int ret = 0;
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "DEPT.P02" }, new int[] { 1 } );
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
        #region CÁC HÀM CƠ BẢN --> KHÔNG SỬA
        public static String makeRequestParam(string func, params string[] listParam)
        {
            String strParam = "{\"func\":\"" + func + "\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID + "\",\"params\":[";
            if (listParam.Length > 0)
            {
                for (int i = 0; i < listParam.Length - 1; i++)
                {
                    strParam += "\"" + listParam[i] + "\",";
                }
                strParam += "\"" + listParam[listParam.Length - 1] + "\"";
            }
            strParam += "]}";

            return strParam;
        }
        public static String makeRequestParam(string func, string[] listParam, int[] listParamInt)
        {
            String strParam = "{\"func\":\"" + func + "\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID + "\",\"params\":[";
            if (listParam.Length > 0)
            {
                for (int i = 0; i < listParam.Length - 1; i++)
                {
                    strParam += "\"" + listParam[i] + "\",";
                }
                strParam += "\"" + listParam[listParam.Length - 1] + "\"";
            }
            if (listParamInt.Length > 0)
            {
                if (listParam.Length > 0) strParam += ",";
                for (int i = 0; i < listParamInt.Length - 1; i++)
                {
                    strParam += listParamInt[i] + ",";
                }
                strParam += listParamInt[listParamInt.Length - 1] + "";
            }
            strParam += "]}";

            return strParam;
        }
        public static String makeRequestParam(string func, string[] listParam, string[] optionsName, string[] optionsValue)
        {
            String strParam = "{\"func\":\"" + func + "\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID + "\",\"params\":[";
            if (listParam.Length > 0)
            {
                for (int i = 0; i < listParam.Length - 1; i++)
                {
                    strParam += "\"" + listParam[i] + "\",";
                }
                strParam += "\"" + listParam[listParam.Length - 1] + "\"";
            }
            strParam += "]";

            strParam += ",\"options\":[";
            if (optionsName.Length > 0)
            {
                //   {\"name\":\"[0]\",\"value\":\"01/05/2017 00:00:00\"},{\"name\":\"[1]\",\"value\":\"01/06/2017 23:59:59\"},{\"name\":\"[3]\",\"value\":\"0\"},{\"name\":\"[4]\",\"value\":\"0\"}
                for (int i = 0; i < optionsName.Length - 1; i++)
                {
                    strParam += "{\"name\":\"" + optionsName[i] + "\",\"value\":\"" + optionsValue[i] + "\"},";
                }
                strParam += "{\"name\":\"" + optionsName[optionsName.Length - 1] + "\",\"value\":\"" + optionsValue[optionsName.Length - 1] + "\"}";
            }
            strParam += "]";

            strParam += "}";

            return strParam;
        }
        public static String makeRequestParam(string func, string[] listParam, string[] optionsValue)
        {
            // string[] optionsName mặc định là [0] [1] ....
            String strParam = "{\"func\":\"" + func + "\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID + "\",\"params\":[";
            if (listParam.Length > 0)
            {
                for (int i = 0; i < listParam.Length - 1; i++)
                {
                    strParam += "\"" + listParam[i] + "\",";
                }
                strParam += "\"" + listParam[listParam.Length - 1] + "\"";
            }
            strParam += "]";

            strParam += ",\"options\":[";
            if (optionsValue.Length > 0)
            {
                //   {\"name\":\"[0]\",\"value\":\"01/05/2017 00:00:00\"},{\"name\":\"[1]\",\"value\":\"01/06/2017 23:59:59\"},{\"name\":\"[3]\",\"value\":\"0\"},{\"name\":\"[4]\",\"value\":\"0\"}
                for (int i = 0; i < optionsValue.Length - 1; i++)
                {
                    strParam += "{\"name\":\"[" + i + "]\",\"value\":\"" + optionsValue[i] + "\"},";
                }
                strParam += "{\"name\":\"[" + (optionsValue.Length - 1) + "]\",\"value\":\"" + optionsValue[optionsValue.Length - 1] + "\"}";
            }
            strParam += "]";

            strParam += "}";

            return strParam;
        }

        //public static HttpWebRequest request = null;
        private static CookieContainer cookie = new CookieContainer();
        public static String sendRequest(String reqData)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            var responseValue = String.Empty; 

            try
            {
                if (!String.IsNullOrEmpty(reqData))
                {
                    request = (HttpWebRequest)WebRequest.Create(Const.LinkService);
                    request.CookieContainer = cookie;
                    request.Method = "POST";
                    //request.ContentLength = 0;
                    request.ContentType = "text/plain; charset=utf-8";// "text/plain; charset=utf-8";
                    
                    //request.KeepAlive = true;  

                    var encoding = new UTF8Encoding();
                    var bytes = Encoding.GetEncoding("UTF-8").GetBytes(reqData);
                    request.ContentLength = bytes.Length;

                    // 6/12 sv thay đổi, nên cần thêm lệnh Tls12
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender
                        , System.Security.Cryptography.X509Certificates.X509Certificate certificate
                        , System.Security.Cryptography.X509Certificates.X509Chain chain
                        , System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
                    ServicePointManager.Expect100Continue = false;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// hoặc: SecurityProtocolType.Tls12; cần cài thêm: https://support.microsoft.com/en-us/help/3154518/support-for-tls-system-default-versions-included-in-the-net-framework
                    //


                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }

            log.Info(reqData + "\n --> " + responseValue);

            // hết phiên đn: {"result":"1","error_code":1,"error_msg":"Lỗi chưa đăng nhập"}
            if (responseValue.Contains(",\"error_code\":1,") && responseValue.Contains("Lỗi chưa đăng nhập"))
                if (Const.login_timeout != 0) Const.login_timeout = 2;

            return responseValue;
        }
        private static String sendRequest_GET(string data)
        {
            string URI_full = Const.LinkService + "?" + data + "&code=thu@nnc";

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            var responseValue = String.Empty;

            try
            {
                if (!String.IsNullOrEmpty(URI_full))
                {
                    request = (HttpWebRequest)WebRequest.Create(URI_full);
                    request.CookieContainer = cookie;
                    request.Method = "GET"; 
                    request.ContentType = "text/plain; charset=utf-8";// "text/plain; charset=utf-8";
                      
                    // 6/12 sv thay đổi, nên cần thêm lệnh Tls12
                    ServicePointManager.ServerCertificateValidationCallback =
                        delegate (object sender
                        , System.Security.Cryptography.X509Certificates.X509Certificate certificate
                        , System.Security.Cryptography.X509Certificates.X509Chain chain
                        , System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
                    ServicePointManager.Expect100Continue = false;
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;// hoặc: SecurityProtocolType.Tls12; cần cài thêm: https://support.microsoft.com/en-us/help/3154518/support-for-tls-system-default-versions-included-in-the-net-framework
                   
                }

                using (response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }

            //log.Info(URI_full + "\n --> " + responseValue);

            // hết phiên đn: {"result":"1","error_code":1,"error_msg":"Lỗi chưa đăng nhập"}
            if (responseValue.Contains(",\"error_code\":1,") && responseValue.Contains("Lỗi chưa đăng nhập"))
                if (Const.login_timeout != 0) Const.login_timeout = 2;

            return responseValue;
        }
        public static string getRequest(string data)
        {
            return sendRequest_GET(data);




            // Cách cũ ko dùng đc khi:  6/12 sv thay đổi, nên cần thêm lệnh Tls12
            string URI = Const.LinkService + "?" + data + "&code=thu@nnc";
            string responseValue = "";
            try
            {  
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);           
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                
                responseValue = sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            log.Info(URI + "\n --> " + responseValue);

            // hết phiên đn: {"result":"1","error_code":1,"error_msg":"Lỗi chưa đăng nhập"}
            if (responseValue.Contains(",\"error_code\":1,") && responseValue.Contains("Lỗi chưa đăng nhập"))
                if (Const.login_timeout != 0) Const.login_timeout = 2;

            return responseValue;
        }

        public static string getSysDatetime()
        {   //  {"func":"getSystemDate","params":["DD/MM/YYYY HH24:MI:SS"],"uuid":"e50d4962-0e22-4407-979f-aebf2d140891"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("getSystemDate", new String[] { "DD/MM/YYYY HH24:MI:SS" });
                string resp = RequestHTTP.sendRequest(request);

                ret = resp;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return ret;
        }
        #endregion


        #region CÁC HÀM ĐƯỢC SỬ DỤNG CHUNG, NẾU THẤY BẢN TIN GỬI LÊN PHÙ HỢP VỚI HÀM NÀO THÌ SỬ DỤNG HÀM ĐÓ --> KO CẦN VIẾT THÀNH HÀM RIÊNG.
        public static DataTable call_ajaxCALL_SP_O(string paramsName, string ID, int x)
        {
            // {"func":"ajaxCALL_SP_O","params":["VPI01T004.01","8557",0],"uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60"}
            //{"result": "[{\n\"DOITUONGBENHNHANID\": \"1\",\n\"BHYT_BENHNHANID\": \"7375\",\n\"HOSOBENHANID\": \"8789\",\n\"TRANGTHAITIEPNHAN\": \"0\",\n\"LOAITIEPNHANID\": \"1\",\n\"DOITUONGBN\": \"BHYT\",\n\"BHYT_GIOIHANBHYTTRAHOANTOAN\": \"195000\",\n\"MABN\": \"BN00005547\",\n\"TENBN\": \"TEST PK SẢN\",\n\"NAMSINH\": \"1993\",\n\"TUOI\": \"24\",\n\"SOTHE\": \"DN4350101010907\",\n\"MUCHUONG\": \"80\",\n\"GIATRITHETU\": \"01/01/2017\",\n\"GIATRITHEDEN\": \"31/12/2017\",\n\"THAMGIABHYTDU5NAM\": \"0\",\n\"TRADU6THANGLUONGCOBAN\": \"0\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}

            // {"func":"ajaxCALL_SP_O","params":["VPI01T001.05","8557",0],"uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60"}
            //{"result": "[{\n\"TONGTIENDV\": \"52175660\",\n\"TONGTIENDV_BH\": \"51475660\",\n\"BHYT_THANHTOAN\": \"41180528\",\n\"BNTRA\": \"10995132\"
            // ,\n\"MIENGIAMDV\": \"0\",\n\"DAMIENGIAM\": \"0\",\n\"BHYT_DANOP\": \"0\",\n\"DANOP\": \"0\",\n\"TRAN_BHYT\": \"195000\",\n\"FREE\": \"0\"
            // ,\n\"TRAITUYEN\": \"0\",\n\"TYLE_BHYT_HIENTAI\": \"80\",\n\"TYLE_BHYT\": \"100\",\n\"TYLE_THE\": \"80\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}

            //{"func":"ajaxCALL_SP_O","params":["VPI01T001.06","8557",0],"uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60"}
            //{"result": "[{\n\"TONGTIENDV\": \"52175660\",\n\"VIENPHI\": \"10995132\",\n\"DANOP\": \"0\",\n\"TAMUNG\": \"0\",\n\"HOANUNG\": \"0\"
            // ,\n\"MIENGIAM\": \"0\",\n\"MIENGIAMDV\": \"0\",\n\"CHENHLECH\": \"10995132\",\n\"CHECK_TAMUNG\": \"0\",\n\"TIEN_PHAINOP\": \"-10995132\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}

            // {"func":"ajaxCALL_SP_O","params":["LAY.CAUHINH","",0],"uuid":"780ac503-77a6-4ea8-bdbd-b2a0fb933b54"}
            //{
            //"result": "[{\n\"CH_KETTHUCKHAM\": \"1\",\n\"NGAYPK\": \"1\",\n\"KETHUCKHAM_BN\": \"-1\",\n\"XOA_BN\": \"-1\",\n\"CONFIGBACSI\": \"0\",\n\"CHECK_24H\": \"1\",\n\"KEDONTHUOC_CHITIET_NTU\": \"1\",\n\"DK_MOBENHAN\": \"1\",\n\"HIDE_BTN_MO_BA\": \"1\",\n\"MAPHONGTRUC\": \"0\",\n\"CHUPANH\": \"0\",\n\"ANBANGT\": \"0\",\n\"HIDEDONTHUOCKT\": \"0\",\n\"CHECKTIEN\": \"0\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}

            // {"func":"ajaxCALL_SP_O","params":["NGT02K002.LAYDL","3303$214",0],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}
            // {"func":"ajaxCALL_SP_O","params":["NGT02K002.MAUHB_SEL","1004",0],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}

            // {"func":"ajaxCALL_SP_O","params":["NGT02K047.TTTK","1",0],"uuid":"fbfca98d-4bb2-41ab-a3f9-0de274a39080"}
            //"result": "[{\n\"I_U\": \"79014_BV\",\n\"I_P\": \"cntt@314\",\n\"I_U1\": \"79014\",\n\"I_P1\": \"123a@\",\n\"I_MACSKCB\": \"79014\"}]",

            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { paramsName, ID }, new int[] { x }
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
        } 
        public static DataTable call_ajaxCALL_SP_O(string paramsName, string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { paramsName, ID }
                    );
                //System.Console.WriteLine("request=" + request);

                string resp = RequestHTTP.sendRequest(request);
                //System.Console.WriteLine("resp=" + resp);

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
        }
        public static string call_ajaxCALL_SP_S_result(string paramsName, string ID)
        {
            //{"func":"ajaxCALL_SP_S","params":["CHECK.MAXPHONGKHAM","3594$"],"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"}
            //res = {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}

            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_CANHBAO_KHONG_TTDT"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}

            // {"func":"ajaxCALL_SP_S","params":["CHECK.MAXPHONGKHAM","3594$"],"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { paramsName, ID });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();
                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            if (ret == null) ret = "";
            return ret;
        }
        public static string call_ajaxCALL_SP_S_error_msg(string paramsName, string ID)
        {
            //{"func":"ajaxCALL_SP_S","params":["CHECK.MAXPHONGKHAM","3594$"],"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"}
            //res = {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}

            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_CANHBAO_KHONG_TTDT"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { paramsName, ID });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();
                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret = resultSet.error_msg;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
        }
        public static ResponsObj call_ajaxCALL_SP_S(string paramsName, string ID)
        {
            //{"func":"ajaxCALL_SP_S","params":["CHECK.MAXPHONGKHAM","3594$"],"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"}
            //res = {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}

            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_CANHBAO_KHONG_TTDT"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}

            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { paramsName, ID });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();
                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return resultSet;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return null;
        }
        public static DataTable call_ajaxCALL_SP_S_table(string paramsName, string ID)
        {
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { paramsName, ID }
                    );
                //System.Console.WriteLine("request=" + request);

                string resp = RequestHTTP.sendRequest(request);
                //System.Console.WriteLine("resp=" + resp);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                if (!resultSet.result.StartsWith("[")) resultSet.result = "[" + resultSet.result + "]";

                dt = (DataTable)JsonConvert.DeserializeObject(resultSet.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable(); 
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }
        public static DataTable get_ajaxExecuteQuery(string paramsName)
        {
            // {"func":"ajaxExecuteQuery","params":["","NGT02K002.MAUHB_LST"],"options":[],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}
            // [["1007","xxx"],["1004","Mẫu 02"],["1005","Mẫu chuẩn"],["1008","1234567890"]]

            // {"func":"ajaxExecuteQuery","params":["","NGT02K002.MAUKB_LST"],"options":[],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}
            // [["4","Mẫu khám số 2"],["6","Mẫu khám số 1"],["5","Mẫu khám số 1"],["11","Mẫu mới tạo Nguyễn Ngọc Quang"]]

            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", paramsName }, new string[] { }, new string[] { });
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, "");
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }
        public static DataTable get_ajaxExecuteQuery(string paramsName, string[] listName, string[] listValue)
        {
            // {"func":"ajaxExecuteQuery","params":["","NGT02K002.MAUHB_LST"],"options":[],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}
            // [["1007","xxx"],["1004","Mẫu 02"],["1005","Mẫu chuẩn"],["1008","1234567890"]]

            // {"func":"ajaxExecuteQuery","params":["","NGT02K002.MAUKB_LST"],"options":[],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}
            // [["4","Mẫu khám số 2"],["6","Mẫu khám số 1"],["5","Mẫu khám số 1"],["11","Mẫu mới tạo Nguyễn Ngọc Quang"]]

            // Lấy phòng khám theo dịch vụ ID
            // {"func":"ajaxExecuteQuery","params":["","NGTPK.DV"],"options":[{"name":"[0]","value":"1004"}],"uuid":"ce7ed424-01db-4ddd-8b45-00844ed5cacc"}  
            // [["214","[01/00/001] PK Mắt P.314"],["676","[00/00/000] PK Nội tiết đái đường P410"],["210","[00/00/000] PK Răng Hàm Mặt P.313"],["215","[00/00/000] PK Ung Bướu P.303"],["789","[00/00/000] Phòng Khám Hiếm Muộn"]]


            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", paramsName }, listName, listValue);
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, "");
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }
        public static DataTable get_ajaxExecuteQuery(string paramsName, string[] listValue) // listName mặc định là [0], [1] ...
        {
            // {"func":"ajaxExecuteQuery","params":["","NGTPK.DV"],"options":[{"name":"[0]","value":"1004"}],"uuid":"ce7ed424-01db-4ddd-8b45-00844ed5cacc"}  
            // [["214","[01/00/001] PK Mắt P.314"],["676","[00/00/000] PK Nội tiết đái đường P410"],["210","[00/00/000] PK Răng Hàm Mặt P.313"],["215","[00/00/000] PK Ung Bướu P.303"],["789","[00/00/000] Phòng Khám Hiếm Muộn"]]

            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", paramsName }, listValue);
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, "");
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public static DataTable get_ajaxExecuteQueryO(string paramsName, string ID)
        {
            //  {"func":"ajaxExecuteQueryO","params":["","NGT02K010.RV001"],"options":[{"name":"[0]","value":"970"}],"uuid":"2fd18ccd-d1ba-40cc-a7b3-dfbc5a1ee1f6"}                
            //[{ "TUVONGID": "255",
            //"TUVONGLUC": null,
            //"THOIGIANTUVONGID": null,
            //"ISCOKHAMNGHIEM": "0",
            //"NGUYENNHANTUVONGID": null,
            //"NGUYENNHANTUVONGCHINH": "",
            //"CHANDOANGIAIPHAUTUTHI": ""}]

            // {"func":"ajaxExecuteQueryO","params":["","NGT02K009.RV004"],"options":[{"name":"[0]","value":"2226"}],"uuid":"c9e8c9a7-dc53-4e02-b2d4-fcf67755cafb"}	


            // {"func":"ajaxExecuteQueryO","params":["","DS.BN.HOPDONG"],"options":[{"name":"[0]","value":"5"}],"uuid":"62873f6c-87c4-4006-b5b2-da28b157df44"}
            // [{"MABENHNHAN": "BN00001381","TENBENHNHAN": "55555555","DIACHI": "Xã Hồng Nam-Thị xã Hưng Yên-Hưng Yên","NGAYSINH": "01/03/2017","GIOITINH": "Nữ"},{"MABENHNHAN": "BN00001391","TENBENHNHAN": "BN 02","DIACHI": "Xã Hùng Sơn-Huyện Anh Sơn-Nghệ An","NGAYSINH": "06/03/2017","GIOITINH": "Nữ"},{"MABENHNHAN": "BN00001393","TENBENHNHAN": "BN 03","DIACHI": "Xã Hồng Nam - Thị xã Hưng Yên - Tỉnh Hưng Yên","NGAYSINH": "01/03/2017","GIOITINH": "Nữ"},{"MABENHNHAN": "BN00001389","TENBENHNHAN": "BN 01","DIACHI": "Xã Hoà Hiệp Nam-Huyện Đông Hoà-Phú Yên","NGAYSINH": "01/03/2017","GIOITINH": "Nữ"},{"MABENHNHAN": "BN00001402","TENBENHNHAN": "KHAM BENH 01","DIACHI": "Xã Hoà Hiệp Nam-Huyện Đông Hoà-Phú Yên","NGAYSINH": "01/03/2017","GIOITINH": "Nữ"},{"MABENHNHAN": "BN00001424","TENBENHNHAN": "345435435435","DIACHI": "Xã Hồng Nam-Thị xã Hưng Yên-Hưng Yên","NGAYSINH": "01/03/2017","GIOITINH": "Nữ"},{"MABENHNHAN": "BN00001425","TENBENHNHAN": "333333312313123","DIACHI": "Xã Hồng Nam-Thị xã Hưng Yên-Hưng Yên","NGAYSINH": "01/03/2017","GIOITINH": "Nữ"},{"MABENHNHAN": "BN00001428","TENBENHNHAN": "THÊM MỚI VÀO","DIACHI": "567-Thị trấn Nam Phước-Huyện Duy Xuyên-Quảng Nam","NGAYSINH": "15/03/2017","GIOITINH": "Nam"}]
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryO", new String[] { "", paramsName }, new string[] { "[0]" }, new string[] { ID });
                string resp = RequestHTTP.sendRequest(request);

                dt = (DataTable)JsonConvert.DeserializeObject(resp, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }
        public static DataTable get_ajaxExecuteQueryO(string paramsName, string[] listName, string[] listValue)
        {
            // {"func":"ajaxExecuteQueryO","params":["","NTU02D010.05"],"options":[{"name":"[0]","value":"902"},{"name":"[1]","value":"7"},{"name":"[2]","value":"4302"}],"uuid":"80c83951-bc18-4717-8d9d-ba830838e14b"}
            // [{"THUOCVATTUID": "281802","OLDVALUE": "20","MAHOATCHAT": "40.40","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "Mezafen","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "882","SO_LUONG": "20","THANH_TIEN": "17,640","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "MEZ02","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281833","OLDVALUE": "20","MAHOATCHAT": "40.45","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "Naburelax","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "1,360","SO_LUONG": "20","THANH_TIEN": "27,200","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "NAB00","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "407583","OLDVALUE": "20","MAHOATCHAT": "40.495","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "SAVI CANDESARTAN 4","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "2,205","SO_LUONG": "20","THANH_TIEN": "44,100","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "SAV17010","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281752","OLDVALUE": "20","MAHOATCHAT": "40.50","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "Panactol Codein plus","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "580","SO_LUONG": "20","THANH_TIEN": "11,600","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "PAN17","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "407958","OLDVALUE": "6","MAHOATCHAT": "40.679","HUONGDAN_SD": "3@Uống@3 ngày, Ngày 2 Viên chia 2@6@1@0@1@0","TEN_THUOC": "PANTOPRAZOLE STADA 40MG","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "1,500","SO_LUONG": "6","THANH_TIEN": "9,000","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "PAN17013","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281876","OLDVALUE": "5","MAHOATCHAT": "40.937","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 1 Viên chia 1@5@1@0@0@0","TEN_THUOC": "Rotundin 60mg","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "482","SO_LUONG": "5","THANH_TIEN": "2,410","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "ROT01","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281436","OLDVALUE": "5","MAHOATCHAT": "40.1064","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 1 Viên chia 1@5@0@0@0@1","TEN_THUOC": "Vitamin PP","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "163","SO_LUONG": "5","THANH_TIEN": "815","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "PP 06","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "406999","OLDVALUE": "10","MAHOATCHAT": "05C.183","HUONGDAN_SD": "10@Uống@10 ngày, Ngày 1 Viên chia 1@10@1@0@0@0","TEN_THUOC": "Bổ khí thông huyết BVP","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "1,500","SO_LUONG": "10","THANH_TIEN": "15,000","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "BOK17002","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281869","OLDVALUE": "3","MAHOATCHAT": "40.65","HUONGDAN_SD": "3@Uống@3 ngày, Ngày 1 Viên chia 1@3@1@0@0@0","TEN_THUOC": "Risenate","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "6,290","SO_LUONG": "3","THANH_TIEN": "18,870","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "RIS05","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"}]
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryO", new String[] { "", paramsName }, listName, listValue);
                string resp = RequestHTTP.sendRequest(request);

                dt = (DataTable)JsonConvert.DeserializeObject(resp, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }
        public static DataTable get_ajaxExecuteQueryO(string paramsName, string[] listValue)// listName mặc định là [0], [1] ...
        {
            // {"func":"ajaxExecuteQueryO","params":["","NTU02D010.05"],"options":[{"name":"[0]","value":"902"},{"name":"[1]","value":"7"},{"name":"[2]","value":"4302"}],"uuid":"80c83951-bc18-4717-8d9d-ba830838e14b"}
            // [{"THUOCVATTUID": "281802","OLDVALUE": "20","MAHOATCHAT": "40.40","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "Mezafen","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "882","SO_LUONG": "20","THANH_TIEN": "17,640","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "MEZ02","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281833","OLDVALUE": "20","MAHOATCHAT": "40.45","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "Naburelax","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "1,360","SO_LUONG": "20","THANH_TIEN": "27,200","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "NAB00","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "407583","OLDVALUE": "20","MAHOATCHAT": "40.495","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "SAVI CANDESARTAN 4","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "2,205","SO_LUONG": "20","THANH_TIEN": "44,100","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "SAV17010","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281752","OLDVALUE": "20","MAHOATCHAT": "40.50","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 4 Viên chia 2@20@2@0@2@0","TEN_THUOC": "Panactol Codein plus","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "580","SO_LUONG": "20","THANH_TIEN": "11,600","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "PAN17","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "407958","OLDVALUE": "6","MAHOATCHAT": "40.679","HUONGDAN_SD": "3@Uống@3 ngày, Ngày 2 Viên chia 2@6@1@0@1@0","TEN_THUOC": "PANTOPRAZOLE STADA 40MG","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "1,500","SO_LUONG": "6","THANH_TIEN": "9,000","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "PAN17013","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281876","OLDVALUE": "5","MAHOATCHAT": "40.937","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 1 Viên chia 1@5@1@0@0@0","TEN_THUOC": "Rotundin 60mg","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "482","SO_LUONG": "5","THANH_TIEN": "2,410","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "ROT01","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281436","OLDVALUE": "5","MAHOATCHAT": "40.1064","HUONGDAN_SD": "5@Uống@5 ngày, Ngày 1 Viên chia 1@5@0@0@0@1","TEN_THUOC": "Vitamin PP","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "163","SO_LUONG": "5","THANH_TIEN": "815","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "PP 06","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "406999","OLDVALUE": "10","MAHOATCHAT": "05C.183","HUONGDAN_SD": "10@Uống@10 ngày, Ngày 1 Viên chia 1@10@1@0@0@0","TEN_THUOC": "Bổ khí thông huyết BVP","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "1,500","SO_LUONG": "10","THANH_TIEN": "15,000","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "BOK17002","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"},{"THUOCVATTUID": "281869","OLDVALUE": "3","MAHOATCHAT": "40.65","HUONGDAN_SD": "3@Uống@3 ngày, Ngày 1 Viên chia 1@3@1@0@0@0","TEN_THUOC": "Risenate","LOAI_DT_CU": "","LOAI_DT_MOI": "","DONVI_TINH": "Viên","DUONGDUNGID": "513","DUONG_DUNG": "Uống","DON_GIA": "6,290","SO_LUONG": "3","THANH_TIEN": "18,870","BH_TRA": "0","ND_TRA": "0","MA_THUOC": "RIS05","ID_DT_CU": null,"ID_DT_MOI": "0","KHOANMUCID": "373","TYLEBHYT_TVT": "100"}]
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryO", new String[] { "", paramsName }, listValue);
                string resp = RequestHTTP.sendRequest(request);

                dt = (DataTable)JsonConvert.DeserializeObject(resp, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public static string call_ajaxCALL_SP_I(string paramsName, string ID)
        {
            // {"func":"ajaxCALL_SP_I","params":["NGT02K002.MAUKB_DEL","6"],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}
            // {"func":"ajaxCALL_SP_I","params":["NGT02K002.LUUKB",

            // {"func":"ajaxCALL_SP_I","params":["NGT01.DOIYCK_TIEN","3386$1$1004$1004"],"uuid":"522618e8-727e-4a82-8268-e58a71175e71"}

            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { paramsName, ID });
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
            //            {
            //"result": "1",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }
        public static DataTable get_dbCALL_SP_R(string paramsName, string param1)
        {
            // {"func":"ajaxExecuteQuery","params":["","NTU02D010.08"],"options":[{"name":"[0]","value":"970"}],"uuid":"0a3c5aca-b15c-4136-9ae0-2f7a41c093f0"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("dbCALL_SP_R", new String[] { "", paramsName, param1 }
                                        , new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);
                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ret.result = ret.result.Trim();
                dt = Func.fill_ArrayStr_To_Datatable(ret.result, paramsName);

            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return dt;

        }


        public static ResponsList get_ajaxExecuteQueryPaging(string paramsName, int page, int number, string[] listName, string[] listValue, string jsonFilter, string sidx="")
        {
            //postData:{"func":"ajaxExecuteQueryPaging","uuid":"ce7ed424-01db-4ddd-8b45-00844ed5cacc","params":["NGT02K054.PK"],"options":[{"name":"[0]","value":"3381"}]}
            //rows:20
            //page:1
            //sord:asc

            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { paramsName }, listName, listValue);

                string data = "postData=" + request +
                    (jsonFilter == "" ? "" : "&_search=true" + "&filters={\"groupOp\":\"AND\",\"rules\":" + jsonFilter + "}") +
                    "&rows=" + number +
                    "&page=" + page;
                if (sidx != "")
                    data += "&sidx=" + sidx;

                data += "&sord=asc";

                string ret = RequestHTTP.getRequest(data); 

                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return ds;
        }
        public static string get_execute(string paramsName, string[] listName, string[] listValue)
        {
            //{ "func":"execute","params":["","NT.035.2"],"options":[{"name":"[0]","value":"6589"},{"name":"[1]","value":"6479"},{"name":"[2]","value":"277325"}]
            // 1

            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("execute", new String[] { "", paramsName }, listName, listValue);
                ret = RequestHTTP.sendRequest(request);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;
        }
        #endregion 

        #region Các hàm RIÊNG LẺ
        public static bool _checkRoles(string _nguoitaoid, string _nguoidungid)
        {
            bool check = false;

            if (_nguoitaoid == null || _nguoitaoid == "") check = false;
            else if (_nguoitaoid == _nguoidungid) check = true;
            else
            {
                DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("DATA_ROLE", new string[] { "[0]", "[1]" }, new string[] { _nguoidungid, _nguoitaoid });
                if (dt.Rows.Count > 0 && dt.Columns.Contains("SL"))
                    if (Func.Parse(dt.Rows[0]["SL"].ToString()) > 0) check = true;
            }

            return check;
        }
        public static string _checkRoles(string _nguoitaoid, string _nguoidungid, int _trangthai)
        {
            string mess = "";
            bool check = false;

            if (_nguoitaoid == null || _nguoitaoid == "") check = false;
            else if (_nguoitaoid == _nguoidungid) check = true;
            else
            {
                DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("DATA_ROLE", new string[] { "[0]", "[1]" }, new string[] { _nguoidungid, _nguoitaoid });
                if (dt.Rows.Count > 0 && dt.Columns.Contains("SL"))
                    if (Func.Parse(dt.Rows[0]["SL"].ToString()) > 0) check = true;
            }

            if (check == false)
            {
                mess = "Bạn không có quyền cập nhật phiếu này!";
                return mess;
            }
            else if (_trangthai > 1)
            {
                mess = "Không thể sửa phiếu này!\nPhiếu này đã hoặc đang được xử lý!";
                return mess;
            }

            return mess;
        }
        public static string call_uploadMediaBase64(string data, string fileType)
        {
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("uploadMediaBase64", new String[] { data, fileType });
                ret = RequestHTTP.sendRequest(request);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return ret;
        }
        public static DataTable getDS_PK_YEUCAUKHAM(string paramsName, string ID)
        {
            //  {"func":"ajaxCALL_SP_O","params":["DS.PK.YEUCAUKHAM","289803",0],"uuid":"d0fb4cf2-920f-454b-af2b-53c481df41fd"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { paramsName, ID }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();

                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //  {"result": "[{\"ORG_ID\": \"371\",\"ORG_NAME\": \"TT Thuốc\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"247\",\"ORG_NAME\": \"QL Thuốc\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"372\",\"ORG_NAME\": \"TT Vật Tư\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"300\",\"ORG_NAME\": \"QL Thuốc\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"353\",\"ORG_NAME\": \"Phòng Mổ Khoa GMHS\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"182\",\"ORG_NAME\": \"Hành Chính\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"294\",\"ORG_NAME\": \"Hành Chính\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"248\",\"ORG_NAME\": \"QL Vật Tư\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"745\",\"ORG_NAME\": \"Phòng khám sức khỏe khoa GMHS\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"352\",\"ORG_NAME\": \"Duyệt Phẫu Thuật\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"302\",\"ORG_NAME\": \"Buồng Điều Trị\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"724\",\"ORG_NAME\": \"Phòng khám sức khỏe chấn thương 1\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"375\",\"ORG_NAME\": \"TT Thuốc\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"183\",\"ORG_NAME\": \"Buồng Điều Trị\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"491\",\"ORG_NAME\": \"QL Vật Tư\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"301\",\"ORG_NAME\": \"QL Vật Tư\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"376\",\"ORG_NAME\": \"TT Vật Tư\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"},{\"ORG_ID\": \"2618\",\"ORG_NAME\": \"t2\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
        }

        public static string xoaBenhNhan(string json)
        {
            // {"func":"ajaxCALL_SP_S","params":["CN.XOA.KETTHUC","{\"kieu\":\"1\",\"khambenhid\":\"3367\",\"phongkhamdangkyid\":-1,\"tiepnhanid\":\"3654\",\"hosobenhanid\":\"3867\"}"],"uuid":"2d4c5e66-9324-421c-818c-0b2c07b9cebc"}
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "CN.XOA.KETTHUC", json });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
            //"result": "1",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }
        public static string checkTrungBenh(string KHAMBENHID, string PHONGID, string MA_BENH)
        {
            //  {"func":"ajaxCALL_SP_I","params":["CHECK.ICD.TR","3033$214$A16$1"],"uuid":"7a431660-6e39-4d56-9568-28f2b6c60991"}

            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "CHECK.ICD.TR", KHAMBENHID + "$" + PHONGID + "$" + MA_BENH + "$1" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
            //            {
            //"result": "1",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }
        public static DataTable getLichSuKhamBenh(string BENHNHAN_ID)
        {
            //  {"func":"dbCALL_SP_R","params":["","NGT01T001.LSDT","6230$",0],"uuid":"c0a512e7-e407-4eb9-a262-09bfda6a1d42"
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("dbCALL_SP_R", new String[] { "", "NGT01T001.LSDT", BENHNHAN_ID + "$" }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                //if (resultSet.result.StartsWith("[")) resultSet.result = resultSet.result.Substring(1);
                //if (resultSet.result.EndsWith("]")) resultSet.result = resultSet.result.Substring(0, resultSet.result.Length - 1);
                dt = Func.fill_ArrayStr_To_Datatable(resultSet.result, "");
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //{
            //"result": "[[\"6225\",\"1-NgayKham:05/06/2017 16:25;Ngay rv:05/06/2017 16:33;YCK:2-Khám Nhi(Phòng kh
            //ám 110);Xutri:Cấp toa cho về\"]]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }
        public static DataTable getChiTiet_BenhNhan(string Ma, string kieu, string tenbenhnhan, string ngaysinh, string gioitinhid)
        {
            // Ma: mã BN hoặc Mã BHYT, 
            // kieu = 1 theo mã BN:   {"func":"ajaxCALL_SP_O","params":["NGT01T002.TKBN","BN00002499$1${\"tenbenhnhan\":\"\",\"ngaysinh\":\"\",\"gioitinhid\":\"1\"}",0],"uuid":"994da6ee-8222-4bd1-9cb1-a6e67048b677"}
            // kieu = 2 theo số BHYT: {"func":"ajaxCALL_SP_O","params":["NGT01T002.TKBN","DN401AA31900313$2${\"tenbenhnhan\":\"TEST\",\"ngaysinh\":\"04/07/2017\",\"gioitinhid\":\"2\"}",0],"uuid":"994da6ee-8222-4bd1-9cb1-a6e67048b677"}
            DataTable dt = new DataTable();

            try
            {
                string json = "";
                json += Func.json_item("tenbenhnhan", tenbenhnhan);
                json += Func.json_item("ngaysinh", ngaysinh);
                json += Func.json_item("gioitinhid", gioitinhid);
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"");
//Const.local_user.UUID = "eyJhbGciOiJIUzUxMiJ9.eyJqdGkiOiJjNDQ1NmMwZi0xYmEzLTQwZDQtOTc3ZC00OTY4YWQ2NDFiZWIiLCJpYXQiOjE1NjA5MDc2OTcsInN1YiI6IkJORC5BRE1JTiIsImlzcyI6IiIsImV4cCI6MTU2MDkyMjA5N30.JAOzBegzZQ1rR4Un-v7JxC91H4TlraqPxI2YsAqyUiZC0XnW9D4ZX0xjWg60MTNWNQJhI2S50oxrNLMcBEVNEg";

                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NGT01T002.TKBN", Ma + "$" + kieu + "$" + json }, new int[] { 0 }
                    );
                
                string resp = RequestHTTP.sendRequest(request);
                Func.set_log_file("Timkiem BN: "+ request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            // {"result": "[{\"KHAMBENHID\": \"3492\",\"CHANDOANTUYENDUOI\": \"\",\"MACHANDOANTUYENDUOI\": \"\",\"MANOIGIOITHIEU\": \"\",\"HINHTHUCVAOVIENID\": \"3\",\"UUTIENKHAMID\": \"0\",\"TIEPNHANID\": \"3803\",\"NGAYTIEPNHAN\": \"17/11/2017 09:47\",\"PHONGID\": \"214\",\"DTBNID\": \"1\",\"BENHNHANID\": \"3832\",\"MABENHNHAN\": \"BN00002483\",\"TENBENHNHAN\": \"TEST AHIHIH\",\"HOSOBENHANID\": \"4006\",\"NGAY_SINH\": \"12/12/2015\",\"NAMSINH\": \"2015\",\"TUOI\": \"23\",\"BHYTID\": \"2545\",\"MA_BHYT\": \"TE1401100000277\",\"BHYT_BD\": \"12/12/2015\",\"BHYT_KT\": \"11/12/2021\",\"MA_KCBBD\": \"35148\",\"DIACHI_BHYT\": \"Xã Tân Hợp-Huyện Tân Kỳ-Nghệ An\",\"BHYT_LOAIID\": \"1\",\"DT_SINHSONG\": \"2\",\"DU5NAM6THANGLUONGCOBAN\": \"0\",\"TRADU6THANGLCB\": \"0\",\"QUYEN_LOI\": null,\"MUC_HUONG\": null,\"GIOITINHID\": \"2\",\"NGHENGHIEPID\": \"3\",\"DANTOCID\": \"25\",\"QUOCGIAID\": \"0\",\"SONHA\": \"\",\"DIAPHUONGID\": \"4042317269\",\"DIABANID\": null,\"TENDIAPHUONG\": \"Xã Tân Hợp-Huyện Tân Kỳ-Nghệ An\",\"NOILAMVIEC\": \"\",\"NGUOITHAN\": \"\",\"TENNGUOITHAN\": \"ffffffffffff\",\"DIENTHOAINGUOITHAN\": \"\",\"DIACHINGUOITHAN\": \"\",\"DICHVUKHAMBENHID\": \"10247\",\"DICHVUID\": \"1004\",\"PHONGKHAMDANGKYID\": \"2558\",\"TEN_KCBBD\": \"Bệnh viện sản nhi Hà Nam\",\"MAUBENHPHAMID\": \"11500\",\"SOTHUTU\": \"1\",\"TENNOIGIOITHIEU\": \"\",\"ORG_NAME\": \"PK Mắt P.314\",\"SLXN\": \"0\",\"SLCDHA\": \"0\",\"DIACHI\": \"Xã Tân Hợp-Huyện Tân Kỳ-Nghệ An\",\"THUKHAC\": \"0\",\"SLCHUYENKHOA\": \"0\",\"CONGKHAM\": \"1\",\"TKMACHANDOANTUYENDUOI\": \"\",\"TKMANOIGIOITHIEU\": \"\",\"TRANGTHAIKHAMBENH\": \"4\",\"NGAYTHUOC\": \"01/01/1990 00:00:00\",\"CHUADUYETKT\": \"0\",\"BHYT_DV\": \"0\",\"SUB_DTBNID\": \"0\",\"NGAYMAUBENHPHAM\": \"20171117\",\"PHONGKHAMID\": \"214\",\"SDTBENHNHAN\": \"\",\"SINHTHEBHYT\": \"1\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
        }
        public static DataTable getChiTiet_KhamBenh(string KHAMBENHID)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NGT01T002.LECT", KHAMBENHID }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = MyJsonConvert.toDataTable(resultSet.result);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //"result": "[{\n\"KHAMBENHID\": \"9386\",\n\"CHANDOANTUYENDUOI\": \"\",\n\"MACHANDOANTUYENDUOI\": \"\",\n\"TKMACHANDOANTUYENDUOI\": \"\",\n\"MANOIGIOITHIEU\": \"\",\n\"TKMANOIGIOITHIEU\": \"\",\n\"HINHTHUCVAOVIENID\": \"3\",\n\"UUTIENKHAMID\": \"0\",\n\"CV_CHUYENVIEN_HINHTHUCID\": null,\n\"CV_CHUYENVIEN_LYDOID\": null,\n\"CV_CHUYENDUNGTUYEN\": \"0\",\n\"CV_CHUYENVUOTTUYEN\": \"1\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"\",\n\"KHAMBENH_CHIEUCAO\": \"\",\n\"TIEPNHANID\": \"9220\",\n\"NGAYTIEPNHAN\": \"05/09/2017 09:20:51\",\n\"DTBNID\": \"1\",\n\"MUC_HUONG\": \"100\",\n\"BENHNHANID\": \"9238\",\n\"MABENHNHAN\": \"BN00005927\",\n\"TENBENHNHAN\": \"DATTTTT\",\n\"BHYTID\": \"7914\",\n\"MA_BHYT\": \"TE1401100000135\",\n\"BHYT_BD\": \"04/09/2012\",\n\"BHYT_KT\": \"03/09/2018\",\n\"MA_KCBBD\": \"35001\",\n\"DIACHI_BHYT\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_LOAIID\": \"1\",\n\"DT_SINHSONG\": \"\",\n\"DU5NAM6THANGLUONGCOBAN\": \"0\",\n\"HOSOBENHANID\": \"9550\",\n\"NGAY_SINH\": \"04/09/2012\",\n\"NAMSINH\": \"2012\",\n\"TUOI\": \"5\",\n\"DVTUOI\": \"1\",\n\"GIOITINHID\": \"2\",\n\"NGHENGHIEPID\": \"3\",\n\"DANTOCID\": \"25\",\n\"QUOCGIAID\": \"0\",\n\"SONHA\": \"\",\n\"DIAPHUONGID\": \"4042317287\",\n\"DIABANID\": null,\n\"TENDIAPHUONG\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"NOILAMVIEC\": \"\",\n\"NGUOITHAN\": \"\",\n\"TENNGUOITHAN\": \"adsd\",\n\"DIENTHOAINGUOITHAN\": \"\",\n\"DIACHINGUOITHAN\": \"\",\n\"DICHVUKHAMBENHID\": \"149014\",\n\"DICHVUID\": \"400025\",\n\"DICHVUKHAMID\": \"400025\",\n\"PHONGKHAMDANGKYID\": \"8133\",\n\"PHONGID\": \"4136\",\n\"PHONGKHAMID\": \"4136\",\n\"TEN_KCBBD\": \"Bệnh viện đa khoa tỉnh\",\n\"MAUBENHPHAMID\": \"271395\",\n\"STTDONTIEP\": \"0001\",\n\"TENNOIGIOITHIEU\": \"\",\n\"ORG_NAME\": \"231. Phòng khám Mắt\",\n\"DOITUONGBENHNHANID\": \"1\",\n\"LOAITIEPNHANID\": \"1\",\n\"TRANGTHAIDICHVU\": null,\n\"TRANGTHAIKHAMBENH\": \"4\",\n\"ANHBENHNHAN\": \"\",\n\"MABENHAN\": \"BA00006126\",\n\"DIACHI\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_DV\": \"0\",\n\"DOITUONGDB\": \"0\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"THUKHAC\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"CONGKHAM\": \"1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }

        public static Dia_Chi getDIACHI(string ID)
        {
            //  {"func":"ajaxCALL_SP_O","params":["COM.DIACHI","100000000",0],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            Dia_Chi dc = new Dia_Chi();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "COM.DIACHI", ID }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                if (resultSet.result.StartsWith("[")) resultSet.result = resultSet.result.Substring(1);
                if (resultSet.result.EndsWith("]")) resultSet.result = resultSet.result.Substring(0, resultSet.result.Length - 1);
                dc = JsonConvert.DeserializeObject<Dia_Chi>(resultSet.result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dc;
            // {"result": "[{\n\"THON_PHO\": \"\",\n\"DIABANID\": \"-1\",\n\"MA_DIACHI\": \"HN\",\n\"DIA_CHI\": \"TP Hà Nội\",\n\"MA_TINH\": \"HN\",\n\"ID_TINH\": "100000000\",\n\"TEN_TINH\": \"TP Hà Nội\",\n\"MA_HUYEN\": \"\",\n\"ID_HUYEN\": \"-1\",\n\"TEN_HUYEN\": \"\",\n\"MA_XA\": \"\",\n\"ID_XA\": \"-1\",\n\"TEN_XA\": "\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
        }

        public static ResponsList getKQCLS(int page, int number, string KHAMBENHID)
        {
            //PostData:{"func":"ajaxExecuteQueryPaging","uuid":"aec7b1cf-25ab-4c64-b473-e55f5b25e6bd","params":["NGT01T001.KQCLS"],"options":[{"name":"[0]","value":"3249"}]}
            //rows:20
            //page:1
            //sord:asc

            //{"total": 5,"page": 1,"records": 84,"rows" : [{
            //"RN": "1",
            //"TENDICHVU": "Soi trực tiếp tìm hồng cầu, bạch cầu trong phân",
            //"DONVI": "Lần",
            //"GIATRILONNHAT": "",
            //"GIATRINHONHAT": "",
            //"GIATRI_KETQUA": "",
            //"GHICHU": ""},{
            //"RN": "2",
            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NGT01T001.KQCLS" }
                    , new String[] { "[0]" }
                    , new String[] { KHAMBENHID });

                string data = "postData=" + request +
                   "&rows=" + number +
                    "&page=" + page +
                    "&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return ds;
        }
        public static ResponsList getPhongKham_NGT02K054(int page, int number, string KHAMBENHID)
        {
            //postData:{"func":"ajaxExecuteQueryPaging","uuid":"ce7ed424-01db-4ddd-8b45-00844ed5cacc","params":["NGT02K054.PK"],"options":[{"name":"[0]","value":"3381"}]}
            //rows:20
            //page:1
            //sord:asc

            //{"total": 1,"page": 1,"records": 1,"rows" : [{
            //"RN": "1",
            //"PHONGKHAMDANGKYID": "2498",
            //"NGAY": "26/10/2017",
            //"PHONGID": "214",
            //"KHOAID": "27",
            //"TRANGTHAI_STT": "Chờ khám",
            //"TRANGTHAI_STTID": "1",
            //"KHAMBENHID": "3381",
            //"BENHNHANID": "3684",
            //"DICHVUID": "1003",
            //"SANG": "Sáng",
            //"TENPHONG": "PK Mắt P.314",
            //"TENKHOA": "Khoa Khám Bệnh",
            //"TENDICHVU": "Khám KHHGĐ"}] }
            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NGT02K054.PK" }
                    , new String[] { "[0]" }
                    , new String[] { KHAMBENHID });

                string data = "postData=" + request +
                   "&rows=" + number +
                    "&page=" + page +
                    "&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return ds;
        }
        public static string check_maxPK(string PHONGKHAMID)
        {
            //{"func":"ajaxCALL_SP_S","params":["CHECK.MAXPHONGKHAM","3594$"],"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "CHECK.MAXPHONGKHAM", PHONGKHAMID + "$" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();
                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ret;  // "result": "-1", tức là Phòng khám hết số.

            //res = {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}
        }

        public static string getOneValue(string paramsName, string ID)
        {
            //"func":"getOneValue",
            //"params":["", arguments[0]],
            //"options":arguments[1]
            string resp = string.Empty;

            // {"func":"getOneValue","params":["","NGTCHECK.DUYETKT"],"options":[{"name":"[0]","value":"95793"}],"uuid":"9d88eb41-282f-45fb-aad4-21122541b2fb"}

            try
            {
                string request = RequestHTTP.makeRequestParam("getOneValue", new String[] { "", paramsName }
                    , new String[] { "[0]" }
                    , new String[] { ID });

                resp = RequestHTTP.sendRequest(request);

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return resp;
        }
        public static string getOneValue(string paramsName, string[] listName, string[] listValue)
        {
            string resp = string.Empty;

            // {"func":"getOneValue","params":["","NGTCHECK.DUYETKT"],"options":[{"name":"[0]","value":"95793"}],"uuid":"9d88eb41-282f-45fb-aad4-21122541b2fb"}

            try
            {
                string request = RequestHTTP.makeRequestParam("getOneValue", new String[] { "", paramsName }, listName, listValue);

                resp = RequestHTTP.sendRequest(request);

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return resp;
        }
        #endregion


        #region HÀM CÓ SỬ DỤNG CACHE
        public static DataTable Cache_getTinhTP(bool getFromCache)
        {
            //  {"func":"ajaxExecuteQuery","params":["","NGTTI.002"],"options":[{"name":"[S0]","value":"3"},{"name":"[S1]","value":"1"},{"name":"[S2]","value":"1"},{"name":"[S3]","value":"100000000"}],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}             
            // [["100000000","TP Hà Nội","1","01"],["200000000","Hà Giang","2","02"],["400000000","Cao Bằng","4","04"],["600000000","Bắc Kạn","6","06"],["800000000","Tuyên Quang","8","08"],["1000000000","Lào Cai","10","10"],["1100000000","Điện Biên","11","11"],["1200000000","Lai Châu","12","12"],["1400000000","Sơn La","14","14"], .........

            string resp = "";
            // Kiểm tra lấy từ cache
            if (getFromCache) resp = Const.SQLITE.test_cache_get(Const.tbl_DsTinh);

            bool addCache = getFromCache && resp == ""; // đánh dấu nếu ko lấy được dl từ cache thì sau đó update vào cache

            if (String.IsNullOrEmpty(resp)) resp = WS_getTinhTP();

            DataTable dt = Func.fill_ArrayStr_To_Datatable(resp);

            if (addCache && dt.Rows.Count > 0) Const.SQLITE.test_cache_add(Const.tbl_DsTinh, resp);

            return dt;
        }
        public static string WS_getTinhTP()
        {
            //  {"func":"ajaxExecuteQuery","params":["","NGTTI.002"],"options":[{"name":"[S0]","value":"3"},{"name":"[S1]","value":"1"},{"name":"[S2]","value":"1"},{"name":"[S3]","value":"100000000"}],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}             
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", Const.tbl_DsTinh }
                    , new String[] { "[S0]", "[S1]", "[S2]", "[S3]" }
                    , new String[] { Const.local_user.HOSPITAL_ID, Const.local_user.USER_ID, Const.local_user.USER_GROUP_ID, Const.local_user.PROVINCE_ID });
                string resp = RequestHTTP.sendRequest(request);

                return resp;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return "";
            // [["100000000","TP Hà Nội","1","01"],["200000000","Hà Giang","2","02"],["400000000","Cao Bằng","4","04"],["600000000","Bắc Kạn","6","06"],["800000000","Tuyên Quang","8","08"],["1000000000","Lào Cai","10","10"],["1100000000","Điện Biên","11","11"],["1200000000","Lai Châu","12","12"],["1400000000","Sơn La","14","14"], .........
        }

        public static DataTable Cache_getKhoa(bool getFromCache)
        {
            //{"func":"dbCALL_SP_R","params":["","DEPT.P01","4$1$3",0],"uuid":"de992b74-ffc6-4364-bffa-805944d3d680"}

            string resp = "";
            // Kiểm tra lấy từ cache
            if (getFromCache) resp = Const.SQLITE.test_cache_get(Const.tbl_DsKhoa);

            bool addCache = getFromCache && resp == ""; // đánh dấu nếu ko lấy được dl từ cache thì sau đó update vào cache

            if (String.IsNullOrEmpty(resp)) resp = WS_getKhoa();

            DataTable dt = Func.fill_ArrayStr_To_Datatable(resp);

            if (addCache && dt.Rows.Count > 0) Const.SQLITE.test_cache_add(Const.tbl_DsKhoa + "_" + Const.local_user.USER_ID + "_" + Const.local_user.HOSPITAL_ID, resp);

            return dt;
        }
        public static string WS_getKhoa()
        {
            try
            {
                string request = RequestHTTP.makeRequestParam("dbCALL_SP_R", new String[]
                { "", Const.tbl_DsKhoa, "4$" + Const.local_user.USER_ID + "$" + Const.local_user.HOSPITAL_ID, "0" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return "";
        }

        public static DataTable Cache_getPhong(bool getFromCache, string idKhoa)
        {   //  {"func":"dbCALL_SP_R","params":["","DEPT.P01","5$1$13",0],"uuid":"3bed9086-4b49-4322-bda4-183176859991"}

            string resp = "";
            // Kiểm tra lấy từ cache
            if (getFromCache) resp = Const.SQLITE.test_cache_get(Const.tbl_DsKhoa + "_" + Const.local_user.USER_ID + "_" + idKhoa);

            bool addCache = getFromCache && resp == ""; // đánh dấu nếu ko lấy được dl từ cache thì sau đó update vào cache

            if (String.IsNullOrEmpty(resp)) resp = WS_getPhong(idKhoa);

            DataTable dt = Func.fill_ArrayStr_To_Datatable(resp);

            if (addCache && dt.Rows.Count > 0) Const.SQLITE.test_cache_add(Const.tbl_DsKhoa + "_" + Const.local_user.USER_ID + "_" + idKhoa, resp);

            return dt;
        }
        public static string WS_getPhong(string idKhoa)
        {   //  {"func":"dbCALL_SP_R","params":["","DEPT.P01","5$1$13",0],"uuid":"3bed9086-4b49-4322-bda4-183176859991"} 
            try
            {
                string request = RequestHTTP.makeRequestParam("dbCALL_SP_R", new String[]
                { "", Const.tbl_DsKhoa, "5$" + Const.local_user.USER_ID + "$" + idKhoa, "0" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return resultSet.result;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
        }

        public static DataTable Cache_ajaxExecuteQuery(bool getFromCache, string table) // getDantoc   tbl_Nghenghiep   tbl_Quoctich
        {
            string resp = "";
            // Kiểm tra lấy từ cache
            if (getFromCache) resp = Const.SQLITE.test_cache_get(table);

            bool addCache = getFromCache && resp == ""; // đánh dấu nếu ko lấy được dl từ cache thì sau đó update vào cache

            if (String.IsNullOrEmpty(resp)) resp = WS_ajaxExecuteQuery(table);

            DataTable dt = Func.fill_ArrayStr_To_Datatable(resp);

            if (addCache && dt.Rows.Count > 0) Const.SQLITE.test_cache_add(table, resp);

            return dt;
        }
        public static string WS_ajaxExecuteQuery(string table) // 
        {
            // {"func":"ajaxExecuteQuery","params":["","COM.DANTOC"],"uuid":"b5023b64-0448-456a-bc79-49925ea1a442"}
            // [["1","Ba na"],["2","Bá»‘ y"],["3","BrÃ¢u"],["4","ChÄƒm"],["5","ChÆ¡ ro"],
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", table });
                string resp = RequestHTTP.sendRequest(request);
                return resp;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
        }

        public static DataTable Cache_ajaxExecuteQuery(bool getFromCache, string table, string ID) // getHuyen( getXa(  getGioiTinh tbl_Noisong
        {
            string resp = null;
            // Kiểm tra lấy từ cache
            if (getFromCache) resp = Const.SQLITE.test_cache_get(table + "_" + ID);

            bool addCache = getFromCache && resp == ""; // đánh dấu nếu ko lấy được dl từ cache thì sau đó update vào cache

            if (String.IsNullOrEmpty(resp)) resp = WS_ajaxExecuteQuery(table, ID);

            DataTable dt = Func.fill_ArrayStr_To_Datatable(resp);

            if (addCache && dt.Rows.Count > 0) Const.SQLITE.test_cache_add(table + "_" + ID, resp);

            return dt;
        }
        public static string WS_ajaxExecuteQuery(string table, string ID)
        {
            // {"func":"ajaxExecuteQuery","params":["","DMDP.001"],"options":[{"name":"[0]","value":"100000000"}],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            // {"func":"ajaxExecuteQuery","params":["","DMDP.001"],"options":[{"name":"[0]","value":"3535200000"}],"uuid":"b5023b64-0448-456a-bc79-49925ea1a442"}
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", table }, new String[] { ID });
                string resp = RequestHTTP.sendRequest(request);
                return resp;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
            // [["127100000","Huyện Ba Vì","1","17"],["127700000","Huyện Chương Mỹ","2","23"],["101800000","Huyện Gia Lâm","3","12"],........
            // [["3535213501","Thị trấn Bình Mỹ","1","13501"],["3535213561","Xã An Lão","2","13561"],["3535213537","X.....
        }

        public static DataTable Cache_ajaxExecuteQueryPaging(bool getFromCache, string table)
        {
            string resp = "";
            // Kiểm tra lấy từ cache
            if (getFromCache) resp = Const.SQLITE.test_cache_get(table);

            bool addCache = getFromCache && resp == ""; // đánh dấu nếu ko lấy được dl từ cache thì sau đó update vào cache

            if (String.IsNullOrEmpty(resp)) resp = WS_ajaxExecuteQueryPaging(table);

            DataTable dt = new DataTable();
            if (table == Const.tbl_TinhhuyenxaViettat)
            {
                DsTinhHuyenXa ds = new DsTinhHuyenXa();
                ds = JsonConvert.DeserializeObject<DsTinhHuyenXa>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                dt = ds.rows.ConvertListToDataTable<tinhhuyenxa>();
            }
            else if (table == Const.tbl_NoiDKKCB)
            {
                DsBV_BHYT ds = new DsBV_BHYT();
                ds = JsonConvert.DeserializeObject<DsBV_BHYT>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                dt = ds.rows.ConvertListToDataTable<BV_BHYT>();
            }
            else if (table == Const.tbl_DsBenh)
            {
                DsBenh_ChuanDoan ds = new DsBenh_ChuanDoan();
                ds = JsonConvert.DeserializeObject<DsBenh_ChuanDoan>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                dt = ds.rows.ConvertListToDataTable<Benh_ChuanDoan>();
            }

            if (dt == null) dt = new DataTable();
            if (addCache && dt.Rows.Count > 0) Const.SQLITE.test_cache_add(table, resp);

            return dt;
        }
        public static string WS_ajaxExecuteQueryPaging(string table) // "DMDP.002" getListTinhHuyenXa_VietTat() "NT.009" getDsBV_BHYT() "NT.008" getDsBenh_ChuanDoan()
        {
            // trả về 1 mảng, ko có tên cột
            // [["7978500000","Huyện Bình Chánh","1","22"],["7978700000","Huyện Cần Giờ","2","24"],["7978300000","Huyện Củ Chi","3","20"], ...
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { table });
                request = request.Substring(0, request.Length - 1) + ",\"options\":[]}";
                string data = "func=doComboGrid" +
                   "&page=1" +
                   "&postData=" + request +
                   "&rows=1000000" +
                    "&searchTerm=" +
                    "&sidx=" +
                    "&sord=asc";
                string ret = RequestHTTP.getRequest(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
        }

        public static DataTable Cache_ajaxExecuteQueryPaging(bool getFromCache, string paramsName, String[] listValue)
        {
            string table = paramsName;
            for (int i = 0; i < listValue.Length; i++) table += "_" + listValue[i];
            string resp = "";
            // Kiểm tra lấy từ cache
            if (getFromCache) resp = Const.SQLITE.test_cache_get(table);

            bool addCache = getFromCache && resp == ""; // đánh dấu nếu ko lấy được dl từ cache thì sau đó update vào cache

            if (String.IsNullOrEmpty(resp)) resp = WS_ajaxExecuteQueryPaging(paramsName, listValue);

            ResponsList responsList = JsonConvert.DeserializeObject<ResponsList>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            DataTable dt = MyJsonConvert.toDataTable(responsList.rows);

            if (dt == null) dt = new DataTable();
            if (addCache && dt.Rows.Count > 0) Const.SQLITE.test_cache_add(table, resp);

            return dt;
        }
        public static string WS_ajaxExecuteQueryPaging(string paramsName, String[] listValue) // "DMDP.002" getListTinhHuyenXa_VietTat() "NT.009" getDsBV_BHYT() "NT.008" getDsBenh_ChuanDoan()
        {
            // trả về 1 list, có tên cột
            // {"total": 1,"page": 1,"records": 269,"rows" : [{"RN": "1","THUOCVATTUID": "281802","TEN_THUOC": "Mezafen","HOATCHAT": "Loxoprofen","MAHOATCHAT": "40.40","TEN_DVT": "Viên","SLKHADUNG": "3185","MA_THUOC": "MEZ02","GIA_BAN": "882","BIETDUOC": " ","DUONGDUNGID": "513","DUONG_DUNG": "Uống","HUONGDAN_SD": "","TENKHO": "Kho lẻ Ngoại trú BHYT","KHOAID": "4929","NHOM_MABHYT_ID": "4","GIATRANBHYT": "882","KHOANMUCID": "373","TYLEBHYT_TVT": "100","CHOLANHDAODUYET": null,"LIEULUONG": "60mg","CHUY": "","KETRUNGHOATCHAT": "0","CANHBAOSOLUONG": "0","MABYT": "40.40"},
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { paramsName }, listValue);
                string data = "func=doComboGrid" +
                   "&page=1" +
                   "&postData=" + request +
                   "&rows=1000000" +
                    "&searchTerm=" +
                    "&sidx=" +
                    "&sord=asc";
                string ret = RequestHTTP.getRequest(data);
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "";
        }

        //L2PT-22067, L2PT-21936
        public static DataTable getDanhsach_Dichvu(String [] strParam)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string madichvu = strParam[0];
                string tendichvu = strParam[1];
                string loaidichvu = strParam[2];


                string requestDSDV = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.DMC01.TCDV", madichvu + "$" + tendichvu + "$" + loaidichvu }, new int[] { 0 }
                    );

                string respDSDV = RequestHTTP.sendRequest(requestDSDV);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSDV, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //"result": "[{\n\"KHAMBENHID\": \"9386\",\n\"CHANDOANTUYENDUOI\": \"\",\n\"MACHANDOANTUYENDUOI\": \"\",\n\"TKMACHANDOANTUYENDUOI\": \"\",\n\"MANOIGIOITHIEU\": \"\",\n\"TKMANOIGIOITHIEU\": \"\",\n\"HINHTHUCVAOVIENID\": \"3\",\n\"UUTIENKHAMID\": \"0\",\n\"CV_CHUYENVIEN_HINHTHUCID\": null,\n\"CV_CHUYENVIEN_LYDOID\": null,\n\"CV_CHUYENDUNGTUYEN\": \"0\",\n\"CV_CHUYENVUOTTUYEN\": \"1\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"\",\n\"KHAMBENH_CHIEUCAO\": \"\",\n\"TIEPNHANID\": \"9220\",\n\"NGAYTIEPNHAN\": \"05/09/2017 09:20:51\",\n\"DTBNID\": \"1\",\n\"MUC_HUONG\": \"100\",\n\"BENHNHANID\": \"9238\",\n\"MABENHNHAN\": \"BN00005927\",\n\"TENBENHNHAN\": \"DATTTTT\",\n\"BHYTID\": \"7914\",\n\"MA_BHYT\": \"TE1401100000135\",\n\"BHYT_BD\": \"04/09/2012\",\n\"BHYT_KT\": \"03/09/2018\",\n\"MA_KCBBD\": \"35001\",\n\"DIACHI_BHYT\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_LOAIID\": \"1\",\n\"DT_SINHSONG\": \"\",\n\"DU5NAM6THANGLUONGCOBAN\": \"0\",\n\"HOSOBENHANID\": \"9550\",\n\"NGAY_SINH\": \"04/09/2012\",\n\"NAMSINH\": \"2012\",\n\"TUOI\": \"5\",\n\"DVTUOI\": \"1\",\n\"GIOITINHID\": \"2\",\n\"NGHENGHIEPID\": \"3\",\n\"DANTOCID\": \"25\",\n\"QUOCGIAID\": \"0\",\n\"SONHA\": \"\",\n\"DIAPHUONGID\": \"4042317287\",\n\"DIABANID\": null,\n\"TENDIAPHUONG\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"NOILAMVIEC\": \"\",\n\"NGUOITHAN\": \"\",\n\"TENNGUOITHAN\": \"adsd\",\n\"DIENTHOAINGUOITHAN\": \"\",\n\"DIACHINGUOITHAN\": \"\",\n\"DICHVUKHAMBENHID\": \"149014\",\n\"DICHVUID\": \"400025\",\n\"DICHVUKHAMID\": \"400025\",\n\"PHONGKHAMDANGKYID\": \"8133\",\n\"PHONGID\": \"4136\",\n\"PHONGKHAMID\": \"4136\",\n\"TEN_KCBBD\": \"Bệnh viện đa khoa tỉnh\",\n\"MAUBENHPHAMID\": \"271395\",\n\"STTDONTIEP\": \"0001\",\n\"TENNOIGIOITHIEU\": \"\",\n\"ORG_NAME\": \"231. Phòng khám Mắt\",\n\"DOITUONGBENHNHANID\": \"1\",\n\"LOAITIEPNHANID\": \"1\",\n\"TRANGTHAIDICHVU\": null,\n\"TRANGTHAIKHAMBENH\": \"4\",\n\"ANHBENHNHAN\": \"\",\n\"MABENHAN\": \"BA00006126\",\n\"DIACHI\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_DV\": \"0\",\n\"DOITUONGDB\": \"0\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"THUKHAC\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"CONGKHAM\": \"1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }

        //L2PT-22067, L2PT-21936
        public static DataTable getDanhsach_Loainhom_DV()
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string madichvu = "5";
                string tendichvu = "5";
                string loaidichvu = "5";

                //lay loainhom dv
                string requestDSDV = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.DMC01.DMC", madichvu + "$" + tendichvu + "$" + loaidichvu }, new int[] { 0 }
                    );
                


                string respDSDV = RequestHTTP.sendRequest(requestDSDV);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSDV, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                //if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //"result": "[{\n\"KHAMBENHID\": \"9386\",\n\"CHANDOANTUYENDUOI\": \"\",\n\"MACHANDOANTUYENDUOI\": \"\",\n\"TKMACHANDOANTUYENDUOI\": \"\",\n\"MANOIGIOITHIEU\": \"\",\n\"TKMANOIGIOITHIEU\": \"\",\n\"HINHTHUCVAOVIENID\": \"3\",\n\"UUTIENKHAMID\": \"0\",\n\"CV_CHUYENVIEN_HINHTHUCID\": null,\n\"CV_CHUYENVIEN_LYDOID\": null,\n\"CV_CHUYENDUNGTUYEN\": \"0\",\n\"CV_CHUYENVUOTTUYEN\": \"1\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"\",\n\"KHAMBENH_CHIEUCAO\": \"\",\n\"TIEPNHANID\": \"9220\",\n\"NGAYTIEPNHAN\": \"05/09/2017 09:20:51\",\n\"DTBNID\": \"1\",\n\"MUC_HUONG\": \"100\",\n\"BENHNHANID\": \"9238\",\n\"MABENHNHAN\": \"BN00005927\",\n\"TENBENHNHAN\": \"DATTTTT\",\n\"BHYTID\": \"7914\",\n\"MA_BHYT\": \"TE1401100000135\",\n\"BHYT_BD\": \"04/09/2012\",\n\"BHYT_KT\": \"03/09/2018\",\n\"MA_KCBBD\": \"35001\",\n\"DIACHI_BHYT\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_LOAIID\": \"1\",\n\"DT_SINHSONG\": \"\",\n\"DU5NAM6THANGLUONGCOBAN\": \"0\",\n\"HOSOBENHANID\": \"9550\",\n\"NGAY_SINH\": \"04/09/2012\",\n\"NAMSINH\": \"2012\",\n\"TUOI\": \"5\",\n\"DVTUOI\": \"1\",\n\"GIOITINHID\": \"2\",\n\"NGHENGHIEPID\": \"3\",\n\"DANTOCID\": \"25\",\n\"QUOCGIAID\": \"0\",\n\"SONHA\": \"\",\n\"DIAPHUONGID\": \"4042317287\",\n\"DIABANID\": null,\n\"TENDIAPHUONG\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"NOILAMVIEC\": \"\",\n\"NGUOITHAN\": \"\",\n\"TENNGUOITHAN\": \"adsd\",\n\"DIENTHOAINGUOITHAN\": \"\",\n\"DIACHINGUOITHAN\": \"\",\n\"DICHVUKHAMBENHID\": \"149014\",\n\"DICHVUID\": \"400025\",\n\"DICHVUKHAMID\": \"400025\",\n\"PHONGKHAMDANGKYID\": \"8133\",\n\"PHONGID\": \"4136\",\n\"PHONGKHAMID\": \"4136\",\n\"TEN_KCBBD\": \"Bệnh viện đa khoa tỉnh\",\n\"MAUBENHPHAMID\": \"271395\",\n\"STTDONTIEP\": \"0001\",\n\"TENNOIGIOITHIEU\": \"\",\n\"ORG_NAME\": \"231. Phòng khám Mắt\",\n\"DOITUONGBENHNHANID\": \"1\",\n\"LOAITIEPNHANID\": \"1\",\n\"TRANGTHAIDICHVU\": null,\n\"TRANGTHAIKHAMBENH\": \"4\",\n\"ANHBENHNHAN\": \"\",\n\"MABENHAN\": \"BA00006126\",\n\"DIACHI\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_DV\": \"0\",\n\"DOITUONGDB\": \"0\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"THUKHAC\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"CONGKHAM\": \"1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }

        public static DataTable getDanhsach_Thuoc_VT(String[] strParam)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string mathuoc = strParam[0]; 
                string cboLoai = strParam[1];
                string ma_parent = "";
                string _loai = "0,3,4,5,6,7,8,9,10,12,14,15,16,17,19,20,1,2,11,13,18";

                //lay loainhom dv
                string requestDSTVT = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.DMC01.TVT", mathuoc + "$" + cboLoai + "$" + ma_parent + "$" + _loai }, new int[] { 0 }
                    );

                string respDSTVT = RequestHTTP.sendRequest(requestDSTVT);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSTVT, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //"result": "[{\n\"KHAMBENHID\": \"9386\",\n\"CHANDOANTUYENDUOI\": \"\",\n\"MACHANDOANTUYENDUOI\": \"\",\n\"TKMACHANDOANTUYENDUOI\": \"\",\n\"MANOIGIOITHIEU\": \"\",\n\"TKMANOIGIOITHIEU\": \"\",\n\"HINHTHUCVAOVIENID\": \"3\",\n\"UUTIENKHAMID\": \"0\",\n\"CV_CHUYENVIEN_HINHTHUCID\": null,\n\"CV_CHUYENVIEN_LYDOID\": null,\n\"CV_CHUYENDUNGTUYEN\": \"0\",\n\"CV_CHUYENVUOTTUYEN\": \"1\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"\",\n\"KHAMBENH_CHIEUCAO\": \"\",\n\"TIEPNHANID\": \"9220\",\n\"NGAYTIEPNHAN\": \"05/09/2017 09:20:51\",\n\"DTBNID\": \"1\",\n\"MUC_HUONG\": \"100\",\n\"BENHNHANID\": \"9238\",\n\"MABENHNHAN\": \"BN00005927\",\n\"TENBENHNHAN\": \"DATTTTT\",\n\"BHYTID\": \"7914\",\n\"MA_BHYT\": \"TE1401100000135\",\n\"BHYT_BD\": \"04/09/2012\",\n\"BHYT_KT\": \"03/09/2018\",\n\"MA_KCBBD\": \"35001\",\n\"DIACHI_BHYT\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_LOAIID\": \"1\",\n\"DT_SINHSONG\": \"\",\n\"DU5NAM6THANGLUONGCOBAN\": \"0\",\n\"HOSOBENHANID\": \"9550\",\n\"NGAY_SINH\": \"04/09/2012\",\n\"NAMSINH\": \"2012\",\n\"TUOI\": \"5\",\n\"DVTUOI\": \"1\",\n\"GIOITINHID\": \"2\",\n\"NGHENGHIEPID\": \"3\",\n\"DANTOCID\": \"25\",\n\"QUOCGIAID\": \"0\",\n\"SONHA\": \"\",\n\"DIAPHUONGID\": \"4042317287\",\n\"DIABANID\": null,\n\"TENDIAPHUONG\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"NOILAMVIEC\": \"\",\n\"NGUOITHAN\": \"\",\n\"TENNGUOITHAN\": \"adsd\",\n\"DIENTHOAINGUOITHAN\": \"\",\n\"DIACHINGUOITHAN\": \"\",\n\"DICHVUKHAMBENHID\": \"149014\",\n\"DICHVUID\": \"400025\",\n\"DICHVUKHAMID\": \"400025\",\n\"PHONGKHAMDANGKYID\": \"8133\",\n\"PHONGID\": \"4136\",\n\"PHONGKHAMID\": \"4136\",\n\"TEN_KCBBD\": \"Bệnh viện đa khoa tỉnh\",\n\"MAUBENHPHAMID\": \"271395\",\n\"STTDONTIEP\": \"0001\",\n\"TENNOIGIOITHIEU\": \"\",\n\"ORG_NAME\": \"231. Phòng khám Mắt\",\n\"DOITUONGBENHNHANID\": \"1\",\n\"LOAITIEPNHANID\": \"1\",\n\"TRANGTHAIDICHVU\": null,\n\"TRANGTHAIKHAMBENH\": \"4\",\n\"ANHBENHNHAN\": \"\",\n\"MABENHAN\": \"BA00006126\",\n\"DIACHI\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_DV\": \"0\",\n\"DOITUONGDB\": \"0\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"THUKHAC\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"CONGKHAM\": \"1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }

        public static DataTable get_tracuu_ba(String[] strParam)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string maba = strParam[0];
                string requestTraCuu = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.TRAUCUU.LS", maba}, new int[] { 0 }
                    );

                string respDSTVT = RequestHTTP.sendRequest(requestTraCuu);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSTVT, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        //L2PT-22067, L2PT-21936
        public static DataTable getDanhsach_Khoa()
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string madichvu = "5";
                string tendichvu = "5";
                string loaidichvu = "5";

                //lay loainhom dv
                string requestDSDV = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.DMC.DSKHOA", madichvu + "$" + tendichvu + "$" + loaidichvu }, new int[] { 0 }
                    );

                string respDSDV = RequestHTTP.sendRequest(requestDSDV);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSDV, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                //if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //"result": "[{\n\"KHAMBENHID\": \"9386\",\n\"CHANDOANTUYENDUOI\": \"\",\n\"MACHANDOANTUYENDUOI\": \"\",\n\"TKMACHANDOANTUYENDUOI\": \"\",\n\"MANOIGIOITHIEU\": \"\",\n\"TKMANOIGIOITHIEU\": \"\",\n\"HINHTHUCVAOVIENID\": \"3\",\n\"UUTIENKHAMID\": \"0\",\n\"CV_CHUYENVIEN_HINHTHUCID\": null,\n\"CV_CHUYENVIEN_LYDOID\": null,\n\"CV_CHUYENDUNGTUYEN\": \"0\",\n\"CV_CHUYENVUOTTUYEN\": \"1\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"\",\n\"KHAMBENH_CHIEUCAO\": \"\",\n\"TIEPNHANID\": \"9220\",\n\"NGAYTIEPNHAN\": \"05/09/2017 09:20:51\",\n\"DTBNID\": \"1\",\n\"MUC_HUONG\": \"100\",\n\"BENHNHANID\": \"9238\",\n\"MABENHNHAN\": \"BN00005927\",\n\"TENBENHNHAN\": \"DATTTTT\",\n\"BHYTID\": \"7914\",\n\"MA_BHYT\": \"TE1401100000135\",\n\"BHYT_BD\": \"04/09/2012\",\n\"BHYT_KT\": \"03/09/2018\",\n\"MA_KCBBD\": \"35001\",\n\"DIACHI_BHYT\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_LOAIID\": \"1\",\n\"DT_SINHSONG\": \"\",\n\"DU5NAM6THANGLUONGCOBAN\": \"0\",\n\"HOSOBENHANID\": \"9550\",\n\"NGAY_SINH\": \"04/09/2012\",\n\"NAMSINH\": \"2012\",\n\"TUOI\": \"5\",\n\"DVTUOI\": \"1\",\n\"GIOITINHID\": \"2\",\n\"NGHENGHIEPID\": \"3\",\n\"DANTOCID\": \"25\",\n\"QUOCGIAID\": \"0\",\n\"SONHA\": \"\",\n\"DIAPHUONGID\": \"4042317287\",\n\"DIABANID\": null,\n\"TENDIAPHUONG\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"NOILAMVIEC\": \"\",\n\"NGUOITHAN\": \"\",\n\"TENNGUOITHAN\": \"adsd\",\n\"DIENTHOAINGUOITHAN\": \"\",\n\"DIACHINGUOITHAN\": \"\",\n\"DICHVUKHAMBENHID\": \"149014\",\n\"DICHVUID\": \"400025\",\n\"DICHVUKHAMID\": \"400025\",\n\"PHONGKHAMDANGKYID\": \"8133\",\n\"PHONGID\": \"4136\",\n\"PHONGKHAMID\": \"4136\",\n\"TEN_KCBBD\": \"Bệnh viện đa khoa tỉnh\",\n\"MAUBENHPHAMID\": \"271395\",\n\"STTDONTIEP\": \"0001\",\n\"TENNOIGIOITHIEU\": \"\",\n\"ORG_NAME\": \"231. Phòng khám Mắt\",\n\"DOITUONGBENHNHANID\": \"1\",\n\"LOAITIEPNHANID\": \"1\",\n\"TRANGTHAIDICHVU\": null,\n\"TRANGTHAIKHAMBENH\": \"4\",\n\"ANHBENHNHAN\": \"\",\n\"MABENHAN\": \"BA00006126\",\n\"DIACHI\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_DV\": \"0\",\n\"DOITUONGDB\": \"0\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"THUKHAC\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"CONGKHAM\": \"1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }

        //L2PT-53273
        public static DataTable getDanhsach_Kios()
        {
            DataTable dt = new DataTable();
            try
            {
                //lay loainhom dv
                string requestDSDV = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "DMC.KIOS.DS1", "1"}, new int[] { 0 }
                    );

                string respDSDV = RequestHTTP.sendRequest(requestDSDV);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSDV, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                //if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public static DataTable getDanhsach_KIOS()
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string madichvu = "5";
                string tendichvu = "5";
                string loaidichvu = "5";

                //lay loainhom dv
                string requestDSKIOS = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.DS_KIOS", madichvu + "$" + tendichvu + "$" + loaidichvu }, new int[] { 0 }
                    );
                /*string requestDSDV = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.DMC.DSKHOA", madichvu + "$" + tendichvu + "$" + loaidichvu }, new int[] { 0 }
                    );*/

                string respDSKIOS = RequestHTTP.sendRequest(requestDSKIOS);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSKIOS, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                //if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //"result": "[{\n\"KHAMBENHID\": \"9386\",\n\"CHANDOANTUYENDUOI\": \"\",\n\"MACHANDOANTUYENDUOI\": \"\",\n\"TKMACHANDOANTUYENDUOI\": \"\",\n\"MANOIGIOITHIEU\": \"\",\n\"TKMANOIGIOITHIEU\": \"\",\n\"HINHTHUCVAOVIENID\": \"3\",\n\"UUTIENKHAMID\": \"0\",\n\"CV_CHUYENVIEN_HINHTHUCID\": null,\n\"CV_CHUYENVIEN_LYDOID\": null,\n\"CV_CHUYENDUNGTUYEN\": \"0\",\n\"CV_CHUYENVUOTTUYEN\": \"1\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"\",\n\"KHAMBENH_CHIEUCAO\": \"\",\n\"TIEPNHANID\": \"9220\",\n\"NGAYTIEPNHAN\": \"05/09/2017 09:20:51\",\n\"DTBNID\": \"1\",\n\"MUC_HUONG\": \"100\",\n\"BENHNHANID\": \"9238\",\n\"MABENHNHAN\": \"BN00005927\",\n\"TENBENHNHAN\": \"DATTTTT\",\n\"BHYTID\": \"7914\",\n\"MA_BHYT\": \"TE1401100000135\",\n\"BHYT_BD\": \"04/09/2012\",\n\"BHYT_KT\": \"03/09/2018\",\n\"MA_KCBBD\": \"35001\",\n\"DIACHI_BHYT\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_LOAIID\": \"1\",\n\"DT_SINHSONG\": \"\",\n\"DU5NAM6THANGLUONGCOBAN\": \"0\",\n\"HOSOBENHANID\": \"9550\",\n\"NGAY_SINH\": \"04/09/2012\",\n\"NAMSINH\": \"2012\",\n\"TUOI\": \"5\",\n\"DVTUOI\": \"1\",\n\"GIOITINHID\": \"2\",\n\"NGHENGHIEPID\": \"3\",\n\"DANTOCID\": \"25\",\n\"QUOCGIAID\": \"0\",\n\"SONHA\": \"\",\n\"DIAPHUONGID\": \"4042317287\",\n\"DIABANID\": null,\n\"TENDIAPHUONG\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"NOILAMVIEC\": \"\",\n\"NGUOITHAN\": \"\",\n\"TENNGUOITHAN\": \"adsd\",\n\"DIENTHOAINGUOITHAN\": \"\",\n\"DIACHINGUOITHAN\": \"\",\n\"DICHVUKHAMBENHID\": \"149014\",\n\"DICHVUID\": \"400025\",\n\"DICHVUKHAMID\": \"400025\",\n\"PHONGKHAMDANGKYID\": \"8133\",\n\"PHONGID\": \"4136\",\n\"PHONGKHAMID\": \"4136\",\n\"TEN_KCBBD\": \"Bệnh viện đa khoa tỉnh\",\n\"MAUBENHPHAMID\": \"271395\",\n\"STTDONTIEP\": \"0001\",\n\"TENNOIGIOITHIEU\": \"\",\n\"ORG_NAME\": \"231. Phòng khám Mắt\",\n\"DOITUONGBENHNHANID\": \"1\",\n\"LOAITIEPNHANID\": \"1\",\n\"TRANGTHAIDICHVU\": null,\n\"TRANGTHAIKHAMBENH\": \"4\",\n\"ANHBENHNHAN\": \"\",\n\"MABENHAN\": \"BA00006126\",\n\"DIACHI\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_DV\": \"0\",\n\"DOITUONGDB\": \"0\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"THUKHAC\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"CONGKHAM\": \"1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }

        public static DataTable getFile_Image(String[] strParam)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.LECT","9386",0],"uuid":"8db61b4f-3d6f-4bde-a71e-c4fdf5c06111"}  9386=KHAMBENHID 
            DataTable dt = new DataTable();
            try
            {
                string kiosid = strParam[0]; ;
                string khoaid = strParam[1]; ;
                string loaidichvu = "5";

                //lay loainhom dv
                string requestDSDV = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "KIOS.DMC01.MAP", kiosid + "$" + khoaid + "$" + loaidichvu }, new int[] { 0 }
                    );

                string respDSDV = RequestHTTP.sendRequest(requestDSDV);
                //Func.set_log_file("Timkiem BN: " + request + "  resp= " + resp);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(respDSDV, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = (DataTable)JsonConvert.DeserializeObject(ret.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //"result": "[{\n\"KHAMBENHID\": \"9386\",\n\"CHANDOANTUYENDUOI\": \"\",\n\"MACHANDOANTUYENDUOI\": \"\",\n\"TKMACHANDOANTUYENDUOI\": \"\",\n\"MANOIGIOITHIEU\": \"\",\n\"TKMANOIGIOITHIEU\": \"\",\n\"HINHTHUCVAOVIENID\": \"3\",\n\"UUTIENKHAMID\": \"0\",\n\"CV_CHUYENVIEN_HINHTHUCID\": null,\n\"CV_CHUYENVIEN_LYDOID\": null,\n\"CV_CHUYENDUNGTUYEN\": \"0\",\n\"CV_CHUYENVUOTTUYEN\": \"1\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"\",\n\"KHAMBENH_CHIEUCAO\": \"\",\n\"TIEPNHANID\": \"9220\",\n\"NGAYTIEPNHAN\": \"05/09/2017 09:20:51\",\n\"DTBNID\": \"1\",\n\"MUC_HUONG\": \"100\",\n\"BENHNHANID\": \"9238\",\n\"MABENHNHAN\": \"BN00005927\",\n\"TENBENHNHAN\": \"DATTTTT\",\n\"BHYTID\": \"7914\",\n\"MA_BHYT\": \"TE1401100000135\",\n\"BHYT_BD\": \"04/09/2012\",\n\"BHYT_KT\": \"03/09/2018\",\n\"MA_KCBBD\": \"35001\",\n\"DIACHI_BHYT\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_LOAIID\": \"1\",\n\"DT_SINHSONG\": \"\",\n\"DU5NAM6THANGLUONGCOBAN\": \"0\",\n\"HOSOBENHANID\": \"9550\",\n\"NGAY_SINH\": \"04/09/2012\",\n\"NAMSINH\": \"2012\",\n\"TUOI\": \"5\",\n\"DVTUOI\": \"1\",\n\"GIOITINHID\": \"2\",\n\"NGHENGHIEPID\": \"3\",\n\"DANTOCID\": \"25\",\n\"QUOCGIAID\": \"0\",\n\"SONHA\": \"\",\n\"DIAPHUONGID\": \"4042317287\",\n\"DIABANID\": null,\n\"TENDIAPHUONG\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"NOILAMVIEC\": \"\",\n\"NGUOITHAN\": \"\",\n\"TENNGUOITHAN\": \"adsd\",\n\"DIENTHOAINGUOITHAN\": \"\",\n\"DIACHINGUOITHAN\": \"\",\n\"DICHVUKHAMBENHID\": \"149014\",\n\"DICHVUID\": \"400025\",\n\"DICHVUKHAMID\": \"400025\",\n\"PHONGKHAMDANGKYID\": \"8133\",\n\"PHONGID\": \"4136\",\n\"PHONGKHAMID\": \"4136\",\n\"TEN_KCBBD\": \"Bệnh viện đa khoa tỉnh\",\n\"MAUBENHPHAMID\": \"271395\",\n\"STTDONTIEP\": \"0001\",\n\"TENNOIGIOITHIEU\": \"\",\n\"ORG_NAME\": \"231. Phòng khám Mắt\",\n\"DOITUONGBENHNHANID\": \"1\",\n\"LOAITIEPNHANID\": \"1\",\n\"TRANGTHAIDICHVU\": null,\n\"TRANGTHAIKHAMBENH\": \"4\",\n\"ANHBENHNHAN\": \"\",\n\"MABENHAN\": \"BA00006126\",\n\"DIACHI\": \"Xã Đồng Văn-Huyện Tân Kỳ-Nghệ An\",\n\"BHYT_DV\": \"0\",\n\"DOITUONGDB\": \"0\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"THUKHAC\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"CONGKHAM\": \"1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }

        #endregion

    }
}
