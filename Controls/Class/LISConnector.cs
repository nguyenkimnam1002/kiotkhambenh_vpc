using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using VNPT.HIS.Common;

/**
 *   LISConnector
 *   @ Author : HaNv
 *   @ CreateDate : 01/08/2017
 * */
namespace VNPT.HIS.Controls.Class
{
    public class LISConnector
    {
        public static string LIS_CONNECTION_TYPE = "";
        public static string LIS_SERVICE_DOMAIN_NAME = "";
        public static string LIS_USERNAME = "";
        public static string LIS_SECRET_KEY = "";

        public static string LIS_AUTHENTICATION_GATE = "";
        public static string LIS_GET_RESULT_SET = "/api/v1/GetResultSet";
        public static string LIS_SEND_REQUEST = "/api/v1/SendRequest";
        public static string LIS_DELETE_REQUEST = "/api/v1/DeleteRequest";
        public static string LIS_UPDATE_STATUS = "/api/v1/UpdateResultStatus";

        public static Dictionary<string, string> localVal;
        public static string labToken = "";
        public static string labTokenExpiryTime = "";

        public static void LoadLISConfig()
        {
            string key = "['LIS_CONNECTION_TYPE','LIS_USERNAME','LIS_SECRET_KEY','LIS_SERVICE_DOMAIN_NAME'," +
                    "'LIS_GET_RESULT_SET','LIS_SEND_REQUEST','LIS_DELETE_REQUEST','LIS_UPDATE_STATUS'," +
                    "'LIS_AUTHENTICATION_GATE']";

            DataTable dt = new DataTable();
            string result = ServiceChiDinhDichVu.ajaxCALL_SP_S("CLS02C001.RISC", key);
            dt = (DataTable)JsonConvert.DeserializeObject("[" + result + "]", (typeof(DataTable)));
            if (dt.Rows.Count > 0)
            {
                LIS_CONNECTION_TYPE = dt.Rows[0]["LIS_CONNECTION_TYPE"].ToString();
                LIS_SERVICE_DOMAIN_NAME = dt.Rows[0]["LIS_SERVICE_DOMAIN_NAME"].ToString();
                LIS_USERNAME = dt.Rows[0]["LIS_USERNAME"].ToString();
                LIS_SECRET_KEY = dt.Rows[0]["LIS_SECRET_KEY"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[0]["LIS_AUTHENTICATION_GATE"].ToString()))
                    LIS_AUTHENTICATION_GATE = dt.Rows[0]["LIS_AUTHENTICATION_GATE"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["LIS_GET_RESULT_SET"].ToString()))
                    LIS_GET_RESULT_SET = dt.Rows[0]["LIS_GET_RESULT_SET"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["LIS_SEND_REQUEST"].ToString()))
                    LIS_SEND_REQUEST = dt.Rows[0]["LIS_SEND_REQUEST"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["LIS_DELETE_REQUEST"].ToString()))
                    LIS_DELETE_REQUEST = dt.Rows[0]["LIS_DELETE_REQUEST"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["LIS_UPDATE_STATUS"].ToString()))
                    LIS_UPDATE_STATUS = dt.Rows[0]["LIS_UPDATE_STATUS"].ToString();
            }
        }

        public static LabRequestSet CreateLabRequest(string idmbp)
        {
            LabRequestSet req = new LabRequestSet();
            DataTable ttbn = ServiceChiDinhDichVu.ajaxCALL_SP_O("CLS01X003.TTBN", idmbp, 0);
            if (ttbn.Rows.Count > 0)
            {
                DataRow row = ttbn.Rows[0];
                req.OrderID = row["SOPHIEU"].ToString();
                req.OrderDate = row["NGAYDICHVU"].ToString();
                req.OrderTime = row["NGAYDICHVU"].ToString();
                req.OrderDoctor = row["TENBACSI"].ToString();
                req.PatientCode = row["MABENHNHAN"].ToString();
                req.PatientName = row["TENBENHNHAN"].ToString();
                req.Sex = row["GIOITINH"].ToString();
                req.Birthday = row["NGAYSINH"].ToString();
                req.Address = row["DIACHI"].ToString();
                req.Phone = row["DIENTHOAI"].ToString();
                req.SocialNumber = row["MATHE"].ToString();
                req.FromDate = row["TUNGAY"].ToString();
                req.ToDate = row["DENNGAY"].ToString();
                req.Department = row["KHOAID"].ToString();
                req.Ward = row["PHONGID"].ToString();
                req.Bed = row["GIUONG"].ToString();
                req.Emergency = row["CAPCUU"].ToString();
                req.RightLine = row["DUNGTUYEN"].ToString();
                req.SID = row["BARCODE"].ToString();
                DataTable dataArr = ServiceChiDinhDichVu.ajaxCALL_SP_O("CLS01X002.DSKQ", idmbp, 0);
                for (int i = 0; i < dataArr.Rows.Count; i++)
                {
                    if (req.TestCodeList == null) req.TestCodeList = new List<string>();
                    req.TestCodeList.Add(dataArr.Rows[i]["MADICHVU"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Không tạo được request với idmbp = " + idmbp);
            }
            return req;
        }

        public static string GetLabToken()
        {
            try
            {
                if (!string.IsNullOrEmpty(LIS_AUTHENTICATION_GATE))
                {
                    if (!string.IsNullOrEmpty(labTokenExpiryTime))
                    {
                        DateTime now = DateTime.Now;
                        // 1/18/2018 9:27:39 AM
                        DateTime expiry_time = DateTime.ParseExact(labTokenExpiryTime, "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                        if (now > expiry_time)
                        {
                            RefreshLabToken();
                        }
                    }
                    else
                    {
                        RefreshLabToken();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
            return labToken;
        }

        public static void RefreshLabToken()
        {
            try
            {
                string requestUrl = LIS_SERVICE_DOMAIN_NAME + LIS_AUTHENTICATION_GATE;
                HttpWebRequest request = null;
                HttpWebResponse response = null;
                var responseValue = String.Empty;

                request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add("Username", LIS_USERNAME);
                request.Headers.Add("Password", LIS_SECRET_KEY);
                request.ContentLength = 0;

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
                        {
                            var reader = new StreamReader(responseStream);
                            responseValue = reader.ReadToEnd();
                            LISToken resultSet = JsonConvert.DeserializeObject<LISToken>(responseValue, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                            if (resultSet != null && resultSet.result != null)
                            {
                                labToken = resultSet.result.access_token;
                                labTokenExpiryTime = resultSet.result.expiry_time;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }

        }


        public static string getHash(string code)
        {
            string input = code + LIS_SECRET_KEY;
            Hashids hashids = new Hashids(input);
            var date = Func.Parse(Func.getSysDatetime("yyyyMMdd"));
            return hashids.Encode(date);
        }
        // Hàm gửi request Xóa
        public static void deleteRequestOnLab(string soPhieu, string mauBenhPhamId)
        {
            if (LIS_CONNECTION_TYPE == "") LoadLISConfig();
            try
            {
                string requestUrl = LIS_SERVICE_DOMAIN_NAME + LIS_DELETE_REQUEST;
                LabRequestSet request = new LabRequestSet();

                if (!string.IsNullOrEmpty(LIS_AUTHENTICATION_GATE))
                {
                    request = CreateLabRequest(mauBenhPhamId);
                    if (LIS_CONNECTION_TYPE == "1" && !string.IsNullOrEmpty(LIS_SERVICE_DOMAIN_NAME))
                    {
                        HttpWebRequest webRequest = null;
                        HttpWebResponse webResponse = null;
                        var responseValue = String.Empty;

                        webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
                        webRequest.Method = "POST";
                        webRequest.ContentType = "application/json; charset=utf-8";
                        webRequest.Headers.Add("Username", LIS_USERNAME);
                        webRequest.Headers.Add("Identify-Code", soPhieu);
                        webRequest.Headers.Add("Lis-Access-Hash", getHash(soPhieu));
                        webRequest.Headers.Add("Token", GetLabToken());

                        var encoding = new UTF8Encoding();
                        string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(request);
                        var bytes = Encoding.GetEncoding("UTF-8").GetBytes(jsonData);
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

                            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                            using (StreamReader responseStream = new StreamReader(webResponse.GetResponseStream())) 
                            {
                                if (responseStream != null)
                                {
                                    string ret = responseStream.ReadToEnd();
                                    RefreshLabToken();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string loi = ex.ToString();
                // log.Fatal(ex.ToString());
            }
        }

    }
}
