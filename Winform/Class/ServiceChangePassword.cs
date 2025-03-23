using System; 
using Newtonsoft.Json;
using VNPT.HIS.Common;


namespace VNPT.HIS.MainForm.Class
{
    class ServiceChangePassword
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string checkOldPassword(string userId, string oldPass)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.TKBN","BN00003949$1$",0],"uuid":"9fde1db6-dac9-425b-8e2d-39aee49daf77"}

            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "Change.Pass.01", userId + "$" + oldPass }, new int[] { 0 }
                    );
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


                return ret.result.Trim();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "-1";
        }

        public static string changePassword(string userId, string newPass)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.TKBN","BN00003949$1$",0],"uuid":"9fde1db6-dac9-425b-8e2d-39aee49daf77"}

            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "Change.Pass.02", newPass + "$" + userId +"$" }, new int[] { 0 }
                    );
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                return ret.result.Trim();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return "-1";
        }
    }
}
