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
    public class ServiceTECHS
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string getRequest(string link, string data, string API_KEY)
        {
            string ret = "";
            string URI = link + "?" + data;

            try
            {
                System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
                if (API_KEY != "") req.Headers["API_KEY"] = API_KEY;
                System.Net.WebResponse resp = req.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

                ret = sr.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return ret;
        }

        //=================================================================================================================


        static string LOGIN = "/apikey/login";
        public static wsTECHS_respons Login(string url, string username, string SHA1_password)
        {
            wsTECHS_respons obLogin = null;
            try
            {
                string ret = "";

                url = url + LOGIN;
                string data = "username=" + username + "&password=" + SHA1_password;
                ret = getRequest(url, data, "");

                //  "{"status":{"statusCode":1,"statusDesc":"success"},"data":{"userId":2,"apiKey":"2309db0a4805912e978232a57da2c739"}}"
                obLogin = JsonConvert.DeserializeObject<wsTECHS_respons>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                 
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return null;
            }
            return obLogin;
        }


//        Cấu trúc đường dẫn:
//http://<Server_IP>:<Server_Port>/patient/getPatientInfoFile/{userId}?patientSeq=<Patient_Seq> 
//Phương thức: GET
//            Yêu cầu phải đính kèm API key(lấy từ mục II.1) vào gói tin HTTP Header: API_KEY=<api_key>
//Trong đó:   -  {userId}: lấy từ API đăng nhập ở mục II.1.
//            -  Patient_Seq: Số thứ tự của bệnh nhân.
        static string GET_PATTIEN = "/patient/getPatientInfoFile/";
        public static PatientInfo getPatientInfoFile(string host1, string host, string userid, string apikey, string Patient_Seq)
        {
            PatientInfo info = null;
            try
            {
                string ret = "";

                string url = host1 + GET_PATTIEN + userid;
                string data = "patientSeq=" + Patient_Seq;
                ret = getRequest(url, data, apikey);
                 
                info = JsonConvert.DeserializeObject<PatientInfo>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return null;
            }
            return info;
        }





    }

    //{
    //“status”: {
    //“statusCode”: 1,
    //“statusDesc”: “success”
    //},
    //“data”: {
    //“userId”: <user_id>,
    //“apiKey”: “<api_key_with_32_character>”
    //}
    //}
    public class wsTECHS_respons
    { 
        public status_detail status { get; set; }
        public data_detail data { get; set; }
    }
    public class status_detail
    {
        public string statusCode { get; set; }
        public string statusDesc { get; set; }
    }
    public class data_detail
    {
        public string userId { get; set; }
        public string apiKey { get; set; }
    }

    //{
    //“status”: {
    //“statusCode”: 1,
    //“statusDesc”: “success”
    //},
    //“data”: {
    //“patientSeq”: <patient_seq>,
    //“fullName”: “<full_name>”,
    //“birthday”: “<birthday>”,
    //“gender”: <gender>,
    //“address”: “<address>”,
    //“hasInsurrance”: <has_insurrance>,
    //“insurranceId”: “<insurrance_id>”,
    //“hospitalId”: “<hospital_id>”,
    //“startDate”: “<start_date>”,
    //“expirationDate”: “<expiration_date>”
    //}
//}

    public class PatientInfo
    {
        public status_detail status { get; set; }
        public PatientInfo_detail data { get; set; }
    } 
    public class PatientInfo_detail
    {
        public string patientSeq { get; set; }
        public string fullName { get; set; }
        public string birthday { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string hasInsurrance { get; set; }
        public string insurranceId { get; set; }
        public string hospitalId { get; set; }        
        public string startDate { get; set; }
        public string expirationDate { get; set; }
    }
}
