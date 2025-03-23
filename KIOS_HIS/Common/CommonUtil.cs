using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VNPT.HIS.Common
{
    class CommonUtil
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static CultureInfo CulenUS = new CultureInfo("en-US");

        public class VNMessage
        {
            public int Type { get; set; }
            public string Message { get; set; }
            public VNMessage(int type, string message)
            {
                this.Type = type;
                this.Message = message;
            }
        }

        //public static string RemoveBom(string p)
        //{
        //    byte[] withBom = { 0xef, 0xbb, 0xbf, 0x41 };
        //    string viaEncoding = Encoding.UTF8.GetString(withBom);
        //    string viaStreamReader;
        //    using (StreamReader reader = new StreamReader
        //           (new MemoryStream(withBom), Encoding.UTF8))
        //    {
        //        viaStreamReader = reader.ReadToEnd();
        //    }
        //    return viaStreamReader;
        //}

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        public static String PrintXML(String XML)
        {
            String Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                String FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
                mStream.Close();
                writer.Close();
            }
            catch (XmlException)
            {
                return XML;
            }


            return Result;
        }

        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static object NVL(object obj, object defaultValue)
        {
            return obj != null ? obj : defaultValue;
        }

        public static string NVL(object obj)
        {
            return obj != null ? obj.ToString() : "";
        }

        public static string getDecrypt(string key)
        {
            try
            {
                string ret = "";
                ret = CryptorEngine.Decrypt(key, true);
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return key;
            }
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
    }
}
