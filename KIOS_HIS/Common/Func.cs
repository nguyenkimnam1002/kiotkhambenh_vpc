using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms; 
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Net;
using System.Security.Cryptography;
using System.Globalization;
using System.Diagnostics;
using System.Drawing.Printing;
using Spire.Pdf; 
using Newtonsoft.Json;

namespace VNPT.HIS.Common
{

    public static class Func
    {

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #region CONVERT PARSE STRING TO INT, FLOAT, DOUBLE, DATETIME
        public static DateTime ParseDatetime(string text) // chỉ sử dụng cho kiểu dd-MM-yyyy HH:mm:ss hoặc dd/MM/yyyy HH:mm:ss
        {
            DateTime x;
            text = text.Replace("-", "/");
            if (DateTime.TryParseExact(text, Const.FORMAT_datetime1
                , CultureInfo.InvariantCulture, DateTimeStyles.None, out x)) return x;
            else return DateTime.Now;
        }
        public static DateTime ParseDatetime2(string text) // chỉ sử dụng cho kiểu dd-MM-yyyy HH:mm hoặc dd/MM/yyyy HH:mm
        {
            DateTime x;
            text = text.Replace("-", "/");
            if (DateTime.TryParseExact(text, Const.FORMAT_datetime2
                , CultureInfo.InvariantCulture, DateTimeStyles.None, out x)) return x;
            else return DateTime.Now;
        }
        public static DateTime ParseDate(string text) // chỉ sử dụng cho kiểu dd-MM-yyyy hoặc dd/MM/yyyy
        {
            DateTime x;
            text = text.Replace("-", "/");
            if (DateTime.TryParseExact(text, Const.FORMAT_date1
                , CultureInfo.InvariantCulture, DateTimeStyles.None, out x)) return x;
            else return DateTime.Now;
        }
        public static DateTime ParseDate(string text, string format)
        {
            DateTime x;
            if (DateTime.TryParseExact(text, format
                , CultureInfo.InvariantCulture, DateTimeStyles.None, out x)) return x;
            else return DateTime.Now;
        }
        public static int Parse(string text)
        {
            int x;
            if (int.TryParse(text, out x)) return x;
            else return 0;
        }
        public static float Parse_float(string text) // Số đều làm trên định dạng Eng
        {
            float x;
            if (float.TryParse(text, out x)) return x;
            else return 0;
        }
        public static double Parse_double(string text) // Số đều làm trên định dạng Eng
        {
            double x;
            if (double.TryParse(text, out x)) return x;
            else return 0;
        }
        public static decimal Parse_decimal(string text) // Số đều làm trên định dạng Eng
        {
            decimal x;
            if (decimal.TryParse(text, out x)) return x;
            else return 0;
        }
        #endregion

        #region GET NGÀY GIỜ CỦA SERVER
        public static string getSysDatetime(string format)
        {
            // lấy ngày giờ trên server theo định dạng
            return DateTime.Now.AddSeconds(Const.diffInSeconds).ToString(format);
        }
        public static DateTime getSysDatetime()
        {
            // lấy ngày giờ trên server.
            return DateTime.Now.AddSeconds(Const.diffInSeconds);
        }
        public static DateTime getSysDatetime_Short()
        {
            // chỉ lấy ngày, tháng, năm của hệ thống từ SV, giờ phút giây để mặc định.
            return Func.ParseDate(DateTime.Now.AddSeconds(Const.diffInSeconds).ToString(Const.FORMAT_date1));
        }
        public static DateTime getDatetime_Short(DateTime dt)
        {
            // chỉ lấy ngày, tháng, năm. giờ phút giây để mặc định.
            return Func.ParseDate(dt.ToString(Const.FORMAT_date1));
        }
        #endregion

        #region GET DEFAYLT VALUE: ICON, getTableEmpty
        public static Image getIcon(string icon)
        {
            if (icon == null || icon == "") return null;
            try
            {
                return Image.FromFile("./Resources/" + icon);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(icon.ToString() + "  " + ex.ToString());
                log.Fatal(icon.ToString());
                
            }
            return null;
        }
        public static Image getIconFullPath(string iconFullPath)
        {
            if (iconFullPath == null || iconFullPath == "") return null;
            try
            {
                return Image.FromFile(iconFullPath);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(iconFullPath.ToString() + "  " + ex.ToString()); 
            }
            return null;
        }
        public static DataTable getTableEmpty(string[] columnName)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < columnName.Length; i++) table.Columns.Add(columnName[i]);
            return table;
        }
        #endregion

        #region CONVERT DANH SÁCH
        public static void fill_Datatable_To_Combo(DataTable dt, System.Windows.Forms.ComboBox cb, string addFirstItem_Title, int selectindex
            , EventHandler selectchange)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string colID = "";
                    string colName = "";
                    if (dt.Rows.Count > 0)
                    {
                        colID = dt.Columns[0].ColumnName;
                        colName = dt.Columns[1].ColumnName;
                    }

                    if (addFirstItem_Title != "")
                    {
                        DataRow dr = dt.NewRow();
                        dr[colID] = "0";
                        dr[colName] = addFirstItem_Title;
                        dt.Rows.InsertAt(dr, 0);
                    }
                    int i = cb.SelectedIndex;

                    if (selectchange != null) cb.SelectedIndexChanged -= selectchange;

                    cb.DataSource = dt.DefaultView;
                    cb.DisplayMember = colName;
                    cb.ValueMember = colID;
                    cb.SelectedIndex = -1;
                    if (addFirstItem_Title != "" && selectindex == 0) cb.SelectedIndex = selectindex;

                    if (selectchange != null) cb.SelectedIndexChanged += selectchange;

                    if (selectindex > -1 && !(addFirstItem_Title != "" && selectindex == 0)) cb.SelectedIndex = selectindex;
                }
                else
                {
                    if (selectchange != null) cb.SelectedIndexChanged -= selectchange;

                    cb.DataSource = dt.DefaultView;

                    if (selectchange != null) cb.SelectedIndexChanged += selectchange;
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        public static void fill_Array_To_Combo_temp(String[] arr, System.Windows.Forms.ComboBox cb, string addFirstItem_Title, int selectindex
            , EventHandler selectchange, bool bSTT)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", System.Type.GetType("System.String"));
            dt.Columns.Add("name", System.Type.GetType("System.String"));

            for (int k = 0; k < arr.Length; k++)
            {
                DataRow dr = dt.NewRow();
                if (bSTT)
                {
                    dr["Id"] = "" + (k + 1);
                    dr["Name"] = (k + 1) + ". " + arr[k];
                }
                else
                {
                    dr["Id"] = arr[k].Substring(0, arr[k].IndexOf("."));
                    dr["Name"] = arr[k];
                }
                dt.Rows.Add(dr);
            }

            Func.fill_Datatable_To_Combo(dt, cb, addFirstItem_Title, selectindex, selectchange);
        }

        public static DataTable ConvertListToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = props[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[props.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        public static DataTable fill_ArrayStr_To_Datatable(string ArrayStr)
        {
            return fill_ArrayStr_To_Datatable(ArrayStr, "");
        }
        public static DataTable fill_ArrayStr_To_Datatable(string ArrayStr, string tableName)
        {
            DataTable dt = new DataTable();
            try
            {
                bool initCol = true;
                string[] array = new string[0];

                JArray jArr = JArray.Parse(ArrayStr);

                foreach (var item in jArr.Children())
                {
                    if (initCol)
                    {
                        initCol = false;
                        int k = 1;
                        Newtonsoft.Json.Linq.JToken temp_jtoken = item.First;
                        while (temp_jtoken != null)
                        {
                            dt.Columns.Add("col" + k, typeof(System.String));
                            temp_jtoken = temp_jtoken.Next;
                            k++;
                        }
                        array = new string[k - 1];
                    }

                    int i = 0;
                    Newtonsoft.Json.Linq.JToken jtoken = item.First;
                    while (jtoken != null && i < dt.Columns.Count)
                    {
                        array[i] = jtoken.ToString();
                        i = i + 1;
                        jtoken = jtoken.Next;
                    }
                    dt.Rows.Add(array);
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }

            dt.TableName = tableName;
            return dt;
        }
        #endregion

        #region TẠO CHUỔI JSON
        public static string json_item(string name, string value)
        {
            return "\"" + name + "\":\"" + value + "\"" + ",";
        }
        public static string json_item_num(string name, double value)
        {
            return "\"" + name + "\":" + value + "" + ",";
        }
        public static string json_item_num(string name, string value)
        {
            return "\"" + name + "\":" + value + "" + ",";
        }
        public static string json_item_end(string json_in)
        {
            return "{" + json_in.Substring(0, json_in.Length - 1) + "}";
        }
        #endregion

        #region HTTP; URL CHO FILE PHIẾU IN, BÁO CÁO.. CÁC HÀM MÃ HÓA
        public static bool SaveFileFromUrl(string url, string name)
        {
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(url, name);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return false;
        }
        public static Stream GetStreamFromUrl(string url)
        {
            try
            {
                byte[] imageData = null;

                using (var wc = new System.Net.WebClient())
                    imageData = wc.DownloadData(url);

                return new MemoryStream(imageData);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return null;
            }
        }

        public static String getUrlReport(string code, DataTable par_table, string type)
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

            //"CALLER": "WINFORM",
            //"[HID]": "3",
            //"UUID": "3cb1a008-e2ff-4de9-be12-1f7eceb90217",
            //"[UID]": "4973",
            //"[SCH]": "HIS_DATA2",
            //"thu@nnc": "{'uid':'4973','hid':'3','db':'jdbc/HISL2DS','schema':'HIS_DATA2'}",
            //"dept_name": "Khoa Nội Tổng Hợp"}

            string reportParam = encoding_btoa(Newtonsoft.Json.JsonConvert.SerializeObject(par_table));
            string si = ToHex(json_in);

            //string type = Const.FileInPhieu;
            if (type.ToLower() == "doc") type = "rtf";

            string url = Const.LinkReport + "?code=" + code + "&filetype=" + type
                + "&opt=1&pid=1&cid="
                + Const.local_user.HOSPITAL_ID + "&uid=" + Const.local_user.USER_GROUP_ID + "&usrid=" + Const.local_user.USER_ID
                + "&db_name=" + Const.local_user.DB_NAME + "&db_schema=" + Const.local_user.DB_SCHEMA
                + "&reportParam=" + reportParam + "&si=" + si;

            return url;
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
        public static string FromHex(string hex)
        {
            if (hex.StartsWith("0x")) hex = hex.Substring(2);
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return Encoding.UTF8.GetString(raw);
        }

        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // tương ứng code tren script window.btoa(str);
        private static string encoding_btoa(string str)
        {
            byte[] bytes = Encoding.GetEncoding(28591).GetBytes(str);
            string toReturn = System.Convert.ToBase64String(bytes);
            return toReturn;

            // string x = Func.encoding_btoa("[{\"name\":\"khambenhid\",\"type\":\"String\",\"value\":\"3386\"},{\"name\":\"phongid\",\"type\":\"String\",\"value\":\"789\"},{\"name\":\"i_sch\",\"type\":\"String\",\"value\":\"HIS_DATA2\"}]");
            // = W3sibmFtZSI6ImtoYW1iZW5oaWQiLCJ0eXBlIjoiU3RyaW5nIiwidmFsdWUiOiIzMzg2In0seyJuYW1lIjoicGhvbmdpZCIsInR5cGUiOiJTdHJpbmciLCJ2YWx1ZSI6Ijc4OSJ9LHsibmFtZSI6Imlfc2NoIiwidHlwZSI6IlN0cmluZyIsInZhbHVlIjoiSElTX0RBVEEyIn1d
        }
 
        #endregion

        #region PRINTER
        // Hàm in luôn: lưu file tạm về mục Temp --> in file này từ local, (file ở temp này sau sẽ bị xóa)
        public static bool Print_Luon(DataTable par_table, string code, string paper = "")
        {
            // dùng file pdf vì file này khi in tự động ko bị lỗi hỏi về Magin của file doc; Chú ý máy tính phải cài Foxit, file pdf mặc định mở bằng Foxit
            try
            {
                string type = "pdf";//"doc"; "pdf"
                string url = Func.getUrlReport(code, par_table, type);

                string fname = Const.FolderSaveFilePrint + "\\temp" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_"
                    + new Random().Next(100) + "." + type;

                if (Func.SaveFileFromUrl(url, fname))
                {
                    string fullName = Application.StartupPath + "\\" + fname;
                    if (paper == "80mm")
                    {
                        Func.Print_80mm(fullName, false);
                    }
                    else
                    {
                        Func.Print_From_File_PDF(fullName, (int)PaperKind.A4, false); // false là ko xóa, true là xóa sau khi in
                    }
                }

                //// sau 10s sẽ xóa file temp này
                //BackgroundWorker bk = new BackgroundWorker();
                //bk.DoWork += Bk_DoWork;
                //bk.RunWorkerAsync(fname);

                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return false;
        }

        public static void Print_From_File(string pathFileName, bool deleteAfterPrint = false)
        {
            //MessageBox.Show("in file :" + pathFileName);
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(pathFileName); //in this pass the file path

                info.Verb = "Print";
                info.CreateNoWindow = false;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                var process = Process.Start(info);
                process.WaitForExit();
            }
            catch (Exception e)
            {
                MessageBox.Show("Loi In file: " + pathFileName + " --> " + e.ToString());
            }

            try
            {
                if (deleteAfterPrint) File.Delete(pathFileName);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("printPDF : Không thể xóa file" + ex.ToString());
            }
        }
        public static void Print_From_File_PDF(string pathFileName, int PAGESIZE = (int)PaperKind.A4, bool deleteAfterPrint = false)
        {
            try
            {
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(pathFileName);

                PrintDocument printDoc = doc.PrintDocument;

                if (PAGESIZE != 0)
                {
                    PaperSize ps = new PaperSize();
                    ps.RawKind = PAGESIZE;
                    printDoc.DefaultPageSettings.PaperSize = ps;
                }

                printDoc.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Print PDF :" + pathFileName + " --> " + ex.ToString());
            }

            try
            {
                if (deleteAfterPrint) File.Delete(pathFileName);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("printPDF : Không thể xóa file" + ex.ToString());
            }

        }
        public static void Print_80mm(string pathFileName, bool deleteAfterPrint = false)
        {
            //MessageBox.Show("file : " + pathFileName);
            try
            {
                ProcessStartInfo info = new ProcessStartInfo(pathFileName); //in this pass the file path

                info.Verb = "Print";
                info.CreateNoWindow = false;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                //info.WindowStyle = ProcessWindowStyle.Minimized;
               
                var process = Process.Start(info);
                
                process.WaitForExit();
            }
            catch (Exception e)
            {
                //MessageBox.Show("Loi In file: " + pathFileName + " --> " + e.ToString());
            }

            try
            {
                if (deleteAfterPrint) File.Delete(pathFileName);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("printPDF : Không thể xóa file" + ex.ToString());
            }
        }
        //public static void Print_80mm(string pathFileName, bool deleteAfterPrint = false)
        //{// hàm này cũng oke
        //    try
        //    {
        //        PrinterSettings printer = new PrinterSettings();
        //        //if (printer.PrinterName == null || "".Equals(printer.PrinterName))
        //        //{
        //        //    MessageBox.Show("Không tìm thấy máy in.");
        //        //    return;
        //        //}

        //        ProcessStartInfo info = new ProcessStartInfo(pathFileName); //in this pass the file path1  

        //        info.Arguments = "\"" + printer.PrinterName + "\"";

        //        info.CreateNoWindow = false;
        //        info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        //        info.UseShellExecute = true;
        //        info.Verb = "PrintTo";

        //        var process = Process.Start(info);
        //        process.WaitForExit();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Printting : Có lỗi xảy ra: " + pathFileName + " -> " + ex.ToString());
        //    }


        //    try
        //    {
        //        if (deleteAfterPrint) File.Delete(pathFileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show("printPDF : Không thể xóa file" + ex.ToString());
        //    }
        //}


        


        public static void PrintFile_FromData(string code, DataTable par_table, string type, string fileName = "")
        {
            try
            {
                string url = Func.getUrlReport(code, par_table, type);

                string fname = Const.FolderSaveFilePrint + "\\temp" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_"
                    + new Random().Next(100) + "." + type;
                Func.SaveFileFromUrl(url, fname);

                string fullName = Application.StartupPath + "\\" + fname;
                Func.Print_From_File(fullName);

                BackgroundWorker bk = new BackgroundWorker();
                bk.DoWork += Bk_DoWork;
                bk.RunWorkerAsync(fullName);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

        }

        private static void Bk_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(10000);

            string fullName = (string)e.Argument;
            File.Delete(fullName);
        }

        #endregion

        #region ĐỊNH DẠNG TIỀN
        public static string formatMoneyEng_GiuThapPhan(string tien) // đầu vào là dạng eng
        {
            double d = Parse_double(tien); // nếu cần độ chính xác cao hơn thì dùng decimal d = Parse_decimal(tien);
            string str = d.ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            //if (str.IndexOf(".") > -1) str = str.Substring(0, str.IndexOf(".")); // cắt bỏ 2 cs phần thập phân đi
            return str;
        }
        public static string formatMoneyEng(string tien) // đầu vào là dạng eng
        {
            double d = Parse_double(tien); // nếu cần độ chính xác cao hơn thì dùng decimal d = Parse_decimal(tien);
            string str = d.ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            if (str.IndexOf(".") > -1) str = str.Substring(0, str.IndexOf(".")); // cắt bỏ 2 cs phần thập phân đi
            return str;
        }
        public static string formatMoneyEng(int tien) // đầu vào là dạng: 12345.567
        {
            string str = tien.ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            if (str.IndexOf(".") > -1) str = str.Substring(0, str.IndexOf("."));
            return str;
        }
        public static string formatMoneyEng(float tien) // đầu vào là dạng: 12345.567
        {
            string str = tien.ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            if (str.IndexOf(".") > -1) str = str.Substring(0, str.IndexOf("."));
            return str;
        }
        public static string formatMoneyEng(double tien) // đầu vào là dạng: 12345.567
        {
            string str = tien.ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            if (str.IndexOf(".") > -1) str = str.Substring(0, str.IndexOf("."));
            return str;
        }
        public static string formatMoneyEng(decimal tien) // đầu vào là dạng: 12345.567
        {
            string str = tien.ToString("N", System.Globalization.CultureInfo.InvariantCulture);
            if (str.IndexOf(".") > -1) str = str.Substring(0, str.IndexOf("."));
            return str;
        }

        public static string formatMoneyVN(string tien) // đầu vào là dạng: 12345.567
        {
            return formatMoney(tien, ",", ".");
        }
        private static string formatMoney(string tien, string dau_le, string dau_ngan) // đầu vào là dạng: 12345.567 --> .567 là phần lẻ
        {
            try
            {
                string ret = "";
                // phần sau dấu phẩy
                string so_le = "";
                string so_nguyen = "";
                if (tien.IndexOf(".") > -1)
                {
                    so_le = dau_le + tien.Substring(tien.IndexOf(".") + 1);
                    so_nguyen = tien.Substring(0, tien.IndexOf("."));
                }
                else
                    so_nguyen = tien;

                int n = so_nguyen.Length;
                for (int i = n - 2; i > 0; i--)
                {
                    if ((n - i) % 3 == 0) so_nguyen = so_nguyen.Insert(i, dau_ngan);
                }

                ret = so_nguyen + so_le;
                return ret;
            }
            catch (Exception ex)
            {
                log.Fatal(tien + " " + ex.ToString());
                return tien;
            }
        }

        #endregion

        #region CHỈ SỐ BMI
        public static float BMI(float cannang, float chieucao) // ĐƠN VỊ kg - cm
        {
            float ret = 0;
            if (chieucao > 0) ret = (cannang / (chieucao * chieucao / 10000));
            return Parse_float(ret.ToString("n2"));
        }
        public static string BMI_Mess(float bmi)
        {
            string ret = "";
            if (bmi > 0)
            {
                if (bmi < 18) ret += " (Người gầy)";
                else if (bmi <= 24.9) ret += " (Người bình thường)";
                else if (bmi <= 29.9) ret += " (Người béo phì độ I)";
                else if (bmi < 34.9) ret += " (Người béo phì độ II)";
                else ret += " (Người béo phì độ III)";
            }
            return ret;
        }

        #endregion

        #region KHÁC

        public static string unescape(string strIn)
        {
            string strVal = strIn;
            strVal = strVal.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").Replace("&quot;", "\"");
            strVal = strVal.Replace("&LT;", "<").Replace("&GT;", ">").Replace("&AMP;", "&").Replace("&QUOT;", "\"").Replace("&#36;", "$");
            return strVal;
        }
        //      unescape: function(strIn)
        //      {
        //          var strVal = strIn;
        //          strVal = strVal.replace(/ &lt;/ g,'<').replace(/ &gt;/ g,'>').replace(/ &amp;/ g,'&').replace(/ &quot;/ g, '"');
        //          strVal = strVal.replace(/ &LT;/ g,'<').replace(/ &GT;/ g,'>').replace(/ &AMP;/ g,'&').replace(/ &QUOT;/ g, '"').replace(/ &#36;/g,'$');//
        //return strVal;
        //      },
        //   escape: function(strIn)
        //      {
        //          //console.log('strIn='+strIn);
        //          var strVal = strIn.trim();
        //          strVal = strVal.replace(/ &/ g, '&amp;').replace(/</ g, '&lt;').replace(/>/ g, '&gt;').replace(/ "/g, '&quot;').replace(/\$/g, '&#36;');//
        //      //console.log('escape strIn='+strVal);
        //          return strVal;
        //      },
        public static string addSpecialElement(string str, string se)
        {
            var str1 = "";
            if (se == "CDATA")
            {
                str1 = "<![CDATA[" + str + "]]>";
            }
            return str1;
        }
        public static string get_HIS_FILEEXPORT_TYPE()
        {
            if (Const.HIS_FILEEXPORT_TYPE == "")
            {
                Const.HIS_FILEEXPORT_TYPE = "pdf";
                string ret = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_FILEEXPORT_TYPE");
                if (!string.IsNullOrEmpty(ret) && !"-1".Equals(ret))
                    Const.HIS_FILEEXPORT_TYPE = ret;
            }

            return Const.HIS_FILEEXPORT_TYPE;
        }
        public static string LayNoiDungLoiCheckBHYT(string ma, string mode)
        {
            string msg = "";
            if (ma == "001") { msg = "Thẻ do BHXH Bộ Quốc Phòng quản lý, đề nghị kiểm tra thẻ và thông tin giấy tờ tùy thân. "; }
            if (ma == "002") { msg = "Thẻ do BHXH Bộ Công An quản lý, đề nghị kiểm tra thẻ và thông tin giấy tờ tùy thân. "; }
            if (ma == "003") { msg = "Thẻ cũ hết giá trị sử dụng nhưng đã được cấp thẻ mới. "; }
            if (ma == "004") { msg = "Thẻ cũ còn giá trị sử dụng nhưng đã được cấp thẻ mới. "; }
            if (ma == "010") { msg = "Thẻ hết giá trị sử dụng."; }
            if (ma == "051") { msg = "Mã thẻ không đúng."; }
            if (ma == "052") { msg = "Mã tỉnh cấp thẻ (ký tự thứ 4,5 của mã thẻ) không đúng."; }
            if (ma == "053") { msg = "Mã quyền lợi thẻ (ký tự thứ 3 của mã thẻ) không đúng."; }
            if (ma == "050") { msg = "Không thấy thông tin thẻ BHYT."; }
            if (ma == "060") { msg = "Thẻ sai họ tên. "; }
            if (ma == "061") { msg = "Thẻ sai họ tên (đúng ký tự đầu). "; }
            if (ma == "070") { msg = "Thẻ sai ngày sinh. "; }
            if (ma == "090") { msg = "Thẻ sai nơi đăng ký KCB ban đầu. "; }
            if (ma == "100") { msg = "Lỗi khi lấy dữ liệu số thẻ. "; }
            if (ma == "101") { msg = "Lỗi Server giám định. "; }
            if (ma == "110") { msg = "Thẻ đã thu hồi."; }
            if (ma == "120") { msg = "Thẻ đã báo giảm. "; }
            if (ma == "121") { msg = "Thẻ đã báo giảm. Giảm chuyển ngoại tỉnh."; }
            if (ma == "122") { msg = "Thẻ đã báo giảm. Giảm chuyển nội tỉnh. "; }
            if (ma == "123") { msg = "Thẻ đã báo giảm. Thu hồi do tăng lại cùng đơn vị. "; }
            if (ma == "124") { msg = "Thẻ đã báo giảm. Ngừng tham gia. "; }
            if (ma == "130") { msg = "Trẻ em không xuất trình thẻ. "; }
            if (ma == "205") { msg = "Lỗi sai định dạng tham số truyền vào. "; }
            if (ma == "401") { msg = "Lỗi xác thực tài khoản. "; }

            if (mode == "0")
            {
                if (ma == "080") { msg = "Thẻ sai giới tính."; }
            }
            return msg;
        }
        public static string addZezo(int x, int len)
        {
            // THÊM CÁC CHỮ SỐ 0 VÀO ĐẦU SỐ x sao cho độ dài là len
            int dem = (x.ToString()).Length;
            string ret = "" + x;
            while (dem < len)
            {
                ret = "0" + ret;
                dem++;
            }
            return ret;
        }
         
        //public static void set_font(Control control)
        //{
        //    foreach (Control sub_control in control.Controls)
        //    {
        //        if (sub_control.GetType().ToString() == "DevExpress.XtraLayout.LayoutControl")
        //        {
        //            DevExpress.XtraLayout.LayoutControl layout = (DevExpress.XtraLayout.LayoutControl)sub_control;
        //            foreach (DevExpress.XtraLayout.BaseLayoutItem sub_item in layout.Root.Items)
        //            {
        //                //sub_item.AppearanceItemCaption.Font = Const.font;
        //            }
        //        }
        //        else if (sub_control.Name == "gridControl1" || sub_control.GetType().ToString() == "DevExpress.XtraGrid.Views.Grid.GridView")
        //        {
        //            //DevExpress.XtraGrid.GridControl gc = (DevExpress.XtraGrid.GridControl)sub_control;
        //            //((DevExpress.XtraGrid.Views.Grid.GridView)gc.MainView).Appearance.HeaderPanel.Font = Const.font;
        //            //((DevExpress.XtraGrid.Views.Grid.GridView)gc.MainView).Appearance.Row.Font = Const.font;
        //        }

        //        sub_control.Font = Const.fontDefault;

        //        set_font(sub_control);
        //    }
        //}
        public static DataTable vienphi_tinhtien_dichvu(DataTable dt)
        {
            DataTable ret = new DataTable();
            try
            {
                DataRow dr = dt.Rows[0];
                ret.Columns.Add("tong_cp", typeof(double));
                ret.Columns.Add("bh_tra", typeof(double));
                ret.Columns.Add("mien_giam", typeof(double));
                ret.Columns.Add("nd_tra", typeof(double));
                ret.Columns.Add("loai_dt");
                ret.Columns.Add("ten_loai_tt");

                DataRow row_result = ret.NewRow();


                // đối tượng bảo hiểm
                string DT_BHYT = "1";
                string DT_VIENPHI = "2";
                string DT_DICHVU = "3";
                string DT_NUOCNGOAI = "4";
                string DT_MIENPHI = "5";

                // đối tượng chuyển
                string LOAIDT_BHYT = "1";
                string LOAIDT_BHYT_DICHVU = "2";
                string LOAIDT_VIEN_PHI = "4";
                string LOAIDT_DI_KEM = "5";
                string LOAIDT_DICH_VU = "6";
                string LOAIDT_HAO_PHI_CONGKHAM = "7";
                string LOAIDT_HAO_PHI_PTTT = "8";
                string LOAIDT_HAO_PHI_KHAC = "9";
                string LOAIDT_DOITUONGKHAC = "10";
                string LOAIDT_VIEN_PHI_DICHVU = "11";
                string LOAIDT_MIENPHI = "15";

                double tyle_thuocvattu = 0;

                if (string.IsNullOrEmpty(dr["TYLE_MIENGIAM"].ToString()))
                {
                    dr["TYLE_MIENGIAM"] = "0";
                }

                //set ty le cho dich vu
                if (!dr.Table.Columns.Contains("TYLEDV"))
                {
                    dt.Columns.Add("TYLEDV", typeof(String));
                    dr["TYLEDV"] = "1";
                }
                else if (string.IsNullOrEmpty(dr["TYLEDV"].ToString()) || dr["TYLEDV"].ToString().Equals("0"))
                {
                    dr["TYLEDV"] = "1";
                }
                else
                {
                    dr["MUCHUONG"] = Func.Parse_double(dr["MUCHUONG"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                }

                //Check neu trong nam da dong chi tra du 6 thang, the da dong 5 nam
                if (!string.IsNullOrEmpty(dr["THEDUTHOIGIAN"].ToString()) && Func.Parse_double(dr["THEDUTHOIGIAN"].ToString()) > 0)
                {
                    dr["MUCHUONG"] = "100";
                    if (!string.IsNullOrEmpty(dr["TYLETHUOCVATTU"].ToString()) && Func.Parse_double(dr["TYLETHUOCVATTU"].ToString()) > 0)
                    {
                        tyle_thuocvattu = 100;
                    }
                    else
                    {
                        tyle_thuocvattu = 0;
                    }
                }
                else
                {
                    tyle_thuocvattu = Func.Parse_double(dr["TYLETHUOCVATTU"].ToString());
                }

                //Check xem co phai la dich vu khong duoc thanh toan hoac la van chuyen khong?
                if (dr["DOITUONGBENHNHANID"].ToString() == "1"
                    && dr["DOITUONGCHUYEN"].ToString() != "7"
                        && dr["DOITUONGCHUYEN"].ToString() != "8"
                            && dr["DOITUONGCHUYEN"].ToString() != "9")
                {
                    if (dr["MANHOMBHYT"].ToString() == "0")
                    {
                        dr["MUCHUONG"] = "0";
                        if (Func.Parse_double(dr["GIAND"].ToString()) > 0)
                        {
                            dr["DOITUONGCHUYEN"] = LOAIDT_VIEN_PHI;
                        }
                        else
                        {
                            dr["DOITUONGCHUYEN"] = LOAIDT_DICH_VU;
                        }
                    }
                    else if (dr["MANHOMBHYT"].ToString() == "12")
                    {
                        if (dr["DUOCVANCHUYEN"].ToString() == "0")
                        {
                            dr["MUCHUONG"] = "0";
                            if (Func.Parse_double(dr["GIAND"].ToString()) > 0)
                            {
                                dr["DOITUONGCHUYEN"] = LOAIDT_VIEN_PHI;
                            }
                            else
                            {
                                dr["DOITUONGCHUYEN"] = LOAIDT_DICH_VU;
                            }
                        }
                    }
                }

                //Check xem co phai doi tuong 5 khong; Neu dung thi mien giam kich tran
                if (dr["MUCHUONG"].ToString() == "100" && dr["NHOMDOITUONG"].ToString() == "5")
                {
                    dr["MUCHUONG"] = "100";
                    dr["GIADVKTC"] = "0";
                    dr["GIATRANBH"] = "0";
                    dr["TYLETHUOCVATTU"] = "100";
                }

                if (dr["DOITUONGBENHNHANID"].ToString() == DT_BHYT && (dr["DOITUONGCHUYEN"].ToString() == "0"
                    || dr["DOITUONGCHUYEN"].ToString() == "1" || dr["DOITUONGCHUYEN"].ToString() == "2"))
                {
                    if (ParseDate(dr["NGAYDICHVU"].ToString()) > ParseDate(dr["NGAYHANTHE"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(dr["GIAND"].ToString()) && Func.Parse_double(dr["GIAND"].ToString()) > 0)
                        {
                            dr["DOITUONGCHUYEN"] = "4";
                        }
                        else if (!string.IsNullOrEmpty(dr["GIADV"].ToString()) && Func.Parse_double(dr["GIADV"].ToString()) > 0)
                        {
                            dr["DOITUONGCHUYEN"] = "6";
                        }
                        else
                        {
                            row_result["bh_tra"] = -1;
                            row_result["tong_cp"] = -1;
                            row_result["nd_tra"] = -1;
                            ret.Rows.InsertAt(row_result, 0);
                            return ret;
                        }

                    }
                }

                if (dr["MUCHUONG"].ToString() == "100")
                {
                    dr["TYLE_MIENGIAM"] = "0";
                }

                if (Func.Parse_float(dr["GIABHYT"].ToString()) == 0 && Func.Parse_float(dr["GIAND"].ToString()) == 0 && Func.Parse_float(dr["GIADV"].ToString()) == 0)
                {
                    row_result["bh_tra"] = 0;
                    row_result["tong_cp"] = 0;
                    row_result["nd_tra"] = 0;
                    ret.Columns.Add("ten_loai_tt_moi");
                    row_result["ten_loai_tt_moi"] = "Miễn phí";
                    ret.Columns.Add("tyledv");
                    row_result["tyledv"] = 0;
                    row_result["loai_dt"] = LOAIDT_MIENPHI;
                    ret.Rows.InsertAt(row_result, 0);
                    return ret;
                }

                // tinh voi doi tuong
                if (dr["DOITUONGCHUYEN"].ToString() == "0")
                {
                    if (dr["DOITUONGBENHNHANID"].ToString().Equals(DT_BHYT))
                    {
                        if (!string.IsNullOrEmpty(dr["GIATRANBH"].ToString()) && Func.Parse_double(dr["GIATRANBH"].ToString()) > 0)
                        {
                            row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString());
                            row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIATRANBH"].ToString()) * Func.Parse_double(dr["MUCHUONG"].ToString()) / 100;
                            row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIATRANBH"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                            row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                            row_result["loai_dt"] = LOAIDT_BHYT;
                            row_result["ten_loai_tt"] = "BHYT";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(dr["GIABHYT"].ToString()) && Func.Parse_double(dr["GIABHYT"].ToString()) > 0)
                            {
                                row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString());
                                row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["MUCHUONG"].ToString()) / 100;
                                row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                                row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                                row_result["loai_dt"] = LOAIDT_BHYT;
                                row_result["ten_loai_tt"] = "BHYT";
                            }
                            else if (!string.IsNullOrEmpty(dr["GIAND"].ToString()) && Func.Parse_double(dr["GIAND"].ToString()) > 0)
                            {
                                row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                                row_result["bh_tra"] = 0;
                                row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                                row_result["loai_dt"] = LOAIDT_VIEN_PHI;
                                row_result["ten_loai_tt"] = "Viện phí";
                                if (row_result["tong_cp"].ToString() == "0" && row_result["bh_tra"].ToString() == "0" && row_result["nd_tra"].ToString() == "0")
                                {
                                    row_result["tong_cp"] = -1;
                                    row_result["bh_tra"] = -1;
                                    row_result["nd_tra"] = -1;
                                    ret.Rows.InsertAt(row_result, 0);
                                    return ret;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(dr["GIADV"].ToString()) && Func.Parse_double(dr["GIADV"].ToString()) > 0)
                                {
                                    row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                                    row_result["bh_tra"] = 0;
                                    row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                                    row_result["loai_dt"] = LOAIDT_DICH_VU;
                                    row_result["ten_loai_tt"] = "Dịch vụ";
                                    if (row_result["tong_cp"].ToString() == "0" && row_result["bh_tra"].ToString() == "0" && row_result["nd_tra"].ToString() == "0")
                                    {
                                        row_result["tong_cp"] = -1;
                                        row_result["bh_tra"] = -1;
                                        row_result["nd_tra"] = -1;
                                        ret.Rows.InsertAt(row_result, 0);
                                        return ret;
                                    }
                                }
                                else
                                {
                                    row_result["tong_cp"] = -1;
                                    row_result["bh_tra"] = -1;
                                    row_result["nd_tra"] = -1;
                                    ret.Rows.InsertAt(row_result, 0);
                                    return ret;
                                }
                            }
                        }
                    }
                    else if (dr["DOITUONGBENHNHANID"].ToString().Equals(DT_VIENPHI))
                    {
                        if (!string.IsNullOrEmpty(dr["GIAND"].ToString()) && Func.Parse_double(dr["GIAND"].ToString()) > 0)
                        {
                            row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                            row_result["bh_tra"] = 0;
                            row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                            row_result["loai_dt"] = LOAIDT_VIEN_PHI;
                            row_result["ten_loai_tt"] = "Viện phí";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(dr["GIADV"].ToString()) && Func.Parse_double(dr["GIADV"].ToString()) > 0)
                            {
                                row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                                row_result["bh_tra"] = 0;
                                row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                                row_result["loai_dt"] = LOAIDT_DICH_VU;
                                row_result["ten_loai_tt"] = "Dịch vụ";
                            }
                            else
                            {
                                row_result["tong_cp"] = -1;
                                row_result["bh_tra"] = -1;
                                row_result["nd_tra"] = -1;
                                ret.Rows.InsertAt(row_result, 0);
                                return ret;
                            }
                        }
                    }
                    else if (dr["DOITUONGBENHNHANID"].ToString().Equals(DT_DICHVU))
                    {
                        row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString());
                        row_result["loai_dt"] = LOAIDT_DICH_VU;
                        row_result["ten_loai_tt"] = "Dịch vụ";
                    }
                    else if (dr["DOITUONGBENHNHANID"].ToString().Equals(DT_NUOCNGOAI))
                    {
                        row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIANN"].ToString());
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIANN"].ToString());
                        row_result["loai_dt"] = LOAIDT_DICH_VU;
                        row_result["ten_loai_tt"] = "Dịch vụ";
                    }
                    else if (dr["DOITUONGBENHNHANID"].ToString().Equals(DT_MIENPHI))
                    {
                        row_result["tong_cp"] = 0;
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = 0;
                        row_result["loai_dt"] = LOAIDT_DICH_VU;

                        ret.Columns.Add("tyledv");
                        row_result["tyledv"] = 0;

                        row_result["ten_loai_tt"] = "Miễn phí";
                    }
                }
                else // tinh khi chuyen doi tuong
                {
                    row_result["loai_dt"] = dr["DOITUONGCHUYEN"].ToString();
                    if (dr["DOITUONGBENHNHANID"].ToString().Equals(DT_BHYT) && dr["DOITUONGCHUYEN"].ToString().Equals(LOAIDT_BHYT))
                    {
                        row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString());
                        if (Func.Parse_double(dr["GIATRANBH"].ToString()) > 0)
                        {
                            row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIATRANBH"].ToString()) * Func.Parse_double(dr["MUCHUONG"].ToString()) / 100;
                            row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIATRANBH"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                            row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                        }
                        else
                        {
                            row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["MUCHUONG"].ToString()) / 100;
                            row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                            row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                        }
                    }
                    else if (dr["DOITUONGBENHNHANID"].ToString().Equals(DT_BHYT) && dr["DOITUONGCHUYEN"].ToString().Equals(LOAIDT_BHYT_DICHVU))
                    {
                        if (Func.Parse_double(dr["GIABHYT"].ToString()) < Func.Parse_double(dr["GIADV"].ToString()))
                        {
                            row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString());
                            row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["MUCHUONG"].ToString()) / 100;
                            row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                            row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                            ret.Columns.Add("loai_dt_moi");
                            ret.Columns.Add("nd_tra_chenh", typeof(double));
                            ret.Columns.Add("ten_loai_tt_moi");
                            row_result["loai_dt_moi"] = LOAIDT_DICH_VU;
                            row_result["nd_tra_chenh"] = Func.Parse_double(dr["GIADV"].ToString()) - Func.Parse_double(dr["GIABHYT"].ToString());
                            row_result["ten_loai_tt_moi"] = "BHYT + Dịch vụ";
                        }
                        else
                        {
                            row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString());
                            row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["MUCHUONG"].ToString()) / 100;
                            row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIABHYT"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                            row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                        }
                        if (Func.Parse_double(dr["GIATRANBH"].ToString()) > 0)
                        {
                            row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIATRANBH"].ToString()) * Func.Parse_double(dr["MUCHUONG"].ToString()) / 100;
                            row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIATRANBH"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                            row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                        }
                    }
                    else if ((dr["DOITUONGBENHNHANID"].ToString() == DT_BHYT
                        || dr["DOITUONGBENHNHANID"].ToString() == DT_VIENPHI)
                        && dr["DOITUONGCHUYEN"].ToString() == LOAIDT_VIEN_PHI_DICHVU)
                    {
                        if (Func.Parse_double(dr["GIAND"].ToString()) < Func.Parse_double(dr["GIADV"].ToString()))
                        {
                            row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                            row_result["bh_tra"] = 0;
                            row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                            ret.Columns.Add("loai_dt_moi");
                            ret.Columns.Add("nd_tra_chenh", typeof(double));
                            ret.Columns.Add("ten_loai_tt_moi");
                            row_result["loai_dt_moi"] = LOAIDT_DICH_VU;
                            row_result["nd_tra_chenh"] = Func.Parse_double(dr["GIADV"].ToString()) - Func.Parse_double(dr["GIAND"].ToString());
                            row_result["ten_loai_tt_moi"] = "Viện phí + Dịch vụ";
                        }
                        else
                        {
                            row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                            row_result["bh_tra"] = 0;
                            row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                        }
                    }
                    else if (dr["DOITUONGCHUYEN"].ToString() == LOAIDT_HAO_PHI_PTTT
                        || dr["DOITUONGCHUYEN"].ToString() == LOAIDT_HAO_PHI_KHAC
                        || dr["DOITUONGCHUYEN"].ToString() == LOAIDT_HAO_PHI_CONGKHAM)
                    {
                        row_result["tong_cp"] = 0;
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = 0;
                        ret.Columns.Add("ten_loai_tt_moi");
                        row_result["ten_loai_tt_moi"] = "Hao phí";
                    }
                    else if (dr["DOITUONGCHUYEN"].ToString() == LOAIDT_DOITUONGKHAC)
                    {
                        row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString());
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString());
                        ret.Columns.Add("ten_loai_tt_moi");
                        row_result["ten_loai_tt_moi"] = "Khác";
                    }
                    else if (dr["DOITUONGCHUYEN"].ToString() == LOAIDT_VIEN_PHI)
                    {
                        row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                        ret.Columns.Add("ten_loai_tt_moi");
                        row_result["ten_loai_tt_moi"] = "Viện phí";
                    }
                    else if (dr["DOITUONGCHUYEN"].ToString() == LOAIDT_DI_KEM)
                    {
                        row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIAND"].ToString());
                        ret.Columns.Add("ten_loai_tt_moi");
                        row_result["ten_loai_tt_moi"] = "Đi kèm";
                    }
                    else if (dr["DOITUONGCHUYEN"].ToString() == LOAIDT_DICH_VU)
                    {
                        row_result["tong_cp"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString());
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADV"].ToString());
                        ret.Columns.Add("ten_loai_tt_moi");
                        row_result["ten_loai_tt_moi"] = "Dịch vụ";
                    }
                    else if (dr["DOITUONGCHUYEN"].ToString() == LOAIDT_MIENPHI)
                    {//doi tuong mien phi
                        row_result["tong_cp"] = 0;
                        row_result["bh_tra"] = 0;
                        row_result["nd_tra"] = 0;
                        row_result["loai_dt"] = LOAIDT_MIENPHI;
                        ret.Columns.Add("tyledv");
                        row_result["tyledv"] = 0;
                        row_result["ten_loai_tt"] = "Miễn phí";

                        ret.Rows.InsertAt(row_result, 0);
                        return ret;
                    }
                    else
                    {
                        row_result["tong_cp"] = -1;
                        row_result["bh_tra"] = -1;
                        row_result["nd_tra"] = -1;
                        ret.Rows.InsertAt(row_result, 0);
                        return ret;
                    }
                }

                if (!string.IsNullOrEmpty(dr["GIADVKTC"].ToString()) && dr["GIADVKTC"].ToString() != "0")
                {
                    if (dr["DOITUONGBENHNHANID"].ToString() == "1"
                        && (dr["DOITUONGCHUYEN"].ToString() == "0"
                            || dr["DOITUONGCHUYEN"].ToString() == "1"
                            || dr["DOITUONGCHUYEN"].ToString() == "2")
                        && Func.Parse_double(dr["GIADVKTC"].ToString()) > 0)
                    {
                        if (!string.IsNullOrEmpty(dr["CANTRENDVKTC"].ToString()) && dr["CANTRENDVKTC"].ToString() == "1")
                        {
                            if (Func.Parse_double(row_result["bh_tra"].ToString()) > Func.Parse_double(dr["GIADVKTC"].ToString()))
                            {
                                row_result["bh_tra"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADVKTC"].ToString());
                                row_result["mien_giam"] = Func.Parse_double(dr["SOLUONG"].ToString()) * Func.Parse_double(dr["GIADVKTC"].ToString()) * Func.Parse_double(dr["TYLE_MIENGIAM"].ToString()) / 100;
                                row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) * Func.Parse_double(dr["TYLEDV"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                            }
                        }
                    }
                }

                //Thuoc, vattu thanh toan theo ty le, hoac ngoai danh muc
                if (tyle_thuocvattu < 100)
                {
                    row_result["bh_tra"] = Func.Parse_double(row_result["bh_tra"].ToString()) * tyle_thuocvattu / 100;
                    row_result["mien_giam"] = Func.Parse_double(row_result["mien_giam"].ToString()) * tyle_thuocvattu / 100;
                    row_result["nd_tra"] = Func.Parse_double(row_result["tong_cp"].ToString()) - Func.Parse_double(row_result["bh_tra"].ToString()) - Func.Parse_double(row_result["mien_giam"].ToString());
                }
                ret.Columns.Add("tyledv");
                row_result["tyledv"] = dr["TYLEDV"];
                ret.Rows.InsertAt(row_result, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
            return ret;
        }
        public static DataTable tinhtien_dv(DataTable obj, DataTable obj2)
        // obj: mảng các dịch vụ khám bệnh
        // obj2: thông tin viện phí đã thanh toán: đã nộp, tạm ứng, hoàn ứng 
        {
            double danop = 0, danop_ngt = 0, tamung = 0, hoanung = 0, miengiam = 0;
            if (obj2.Rows.Count > 0)
            {
                danop = Func.Parse_double(obj2.Rows[0]["DANOP"].ToString());
                danop_ngt = Func.Parse_double(obj2.Rows[0]["DANOP_NGT"].ToString());
                tamung = Func.Parse_double(obj2.Rows[0]["TAMUNG"].ToString());
                hoanung = Func.Parse_double(obj2.Rows[0]["HOANUNG"].ToString());
                miengiam = Func.Parse_double(obj2.Rows[0]["MIENGIAM"].ToString());
            }

            double _tong_tien_bh = 0;
            double _tien_dv = 0;
            double _bhyt_tra = 0;
            double _miengiam_dv = 0;
            double _vienphi = 0;
            double _nopthem = 0;
            double _thanhtoan = 0;
            double _nopthem_bh = 0;
            double _tien_bhyt_bntt = 0;
            for (int i = 0; i < obj.Rows.Count; i++)
            {
                _tien_dv += Func.Parse_double(obj.Rows[i]["THANHTIEN"].ToString());
                _bhyt_tra += Func.Parse_double(obj.Rows[i]["TIEN_BHYT_TRA"].ToString());
                _miengiam_dv += Func.Parse_double(obj.Rows[i]["TIEN_MIENGIAM"].ToString());
                _vienphi += Func.Parse_double(obj.Rows[i]["THUCTHU"].ToString());
                _tien_bhyt_bntt += Func.Parse_double(obj.Rows[i]["TIEN_BHYT_BNTT"].ToString());
                if (obj.Rows[i]["LOAIDOITUONG"].ToString() == "1" || obj.Rows[i]["LOAIDOITUONG"].ToString() == "2" || obj.Rows[i]["LOAIDOITUONG"].ToString() == "3")
                { 
                    _tong_tien_bh += Func.Parse_double(obj.Rows[i]["THANHTIEN"].ToString());
                }
            }
            _tien_dv = Math.Round(_tien_dv, 2);
            _bhyt_tra = Math.Round(_bhyt_tra, 2);
            _miengiam_dv = Math.Round(_miengiam_dv, 2);
            _vienphi = Math.Round(_vienphi, 2);
            _tien_bhyt_bntt = Math.Round(_tien_bhyt_bntt, 2);
            _tong_tien_bh = Math.Round(_tong_tien_bh, 2);

            _nopthem = _vienphi - danop - miengiam - tamung + hoanung;
            _nopthem = Math.Round(_nopthem, 2);
            _thanhtoan = _vienphi - danop_ngt;
            _thanhtoan = Math.Round(_thanhtoan, 2);
            _nopthem_bh = _vienphi - _tien_bhyt_bntt;
            _nopthem_bh = Math.Round(_nopthem_bh, 2);
            danop = Math.Round(danop, 2);
            tamung = Math.Round(tamung, 2);
            hoanung = Math.Round(hoanung, 2);
            miengiam = Math.Round(miengiam, 2);
            
            object _vpData = new { 
                //DVDATA = obj
                 TONGTIENDV = _tien_dv
                , TONGTIENBH = _tong_tien_bh
                , TYLE_TT = _tong_tien_bh == 0 ? 0 : Math.Round(100 * _bhyt_tra / _tong_tien_bh)
                , BHYT_THANHTOAN = _bhyt_tra
                , MIENGIAMDV = _miengiam_dv
                , VIENPHI = _vienphi
                , MIENGIAM = miengiam
                , TAMUNG = tamung
                , HOANUNG = hoanung
                , DANOP = danop
                , DANOP_NGT = danop_ngt
                , MIENGIAMBH = 0
                , TAMUNGBH = 0
                , HOANUNGBH = 0
                , NOPTHEM = _nopthem
                , THANHTOAN = _thanhtoan
                , BHYT_NOPTHEM = _nopthem_bh
                , BHYT_BNTT = _tien_bhyt_bntt
            };

            string json = "[" + JsonConvert.SerializeObject(_vpData) + "]";
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            if (dt == null) dt = new DataTable();

            return dt; 
        }


        //public static void DownloadFile(string code, DataTable par_table, string type, string fileName = "")
        //{
        //    try
        //    {
        //        string url = Func.getUrlReport(code, par_table, type);
        //        SaveFileDialog sf = new SaveFileDialog();
        //        sf.FileName = fileName;
        //        sf.Filter = type.Equals("pdf") ? "PDF Files(*.pdf) | *.pdf" : type.Equals("rtf") || type.Equals("docx") ? "Word Document(*.docx) | *.docx" : "All Files(*.*) | *.*";
        //        if (sf.ShowDialog() == DialogResult.OK)
        //        {
        //            Func.SaveFileFromUrl(url, sf.FileName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }

        //}
        //Hàm mã hóa chuỗi
        public static string EncryptString(string Message, string Passphrase)

        {

            byte[] Results;

            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Buoc 1: Bam chuoi su dung MD5

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();

            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Tao doi tuong TripleDESCryptoServiceProvider moi

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Cai dat bo ma hoa

            TDESAlgorithm.Key = TDESKey;

            TDESAlgorithm.Mode = CipherMode.ECB;

            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert chuoi (Message) thanh dang byte[]

            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Ma hoa chuoi

            try

            {

                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();

                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

            }

            finally

            {

                // Xoa moi thong tin ve Triple DES va HashProvider de dam bao an toan

                TDESAlgorithm.Clear();

                HashProvider.Clear();

            }

            // Step 6. Tra ve chuoi da ma hoa bang thuat toan Base64

            return Convert.ToBase64String(Results);

        }
        //Hàm giải mã chuỗi
        public static string DecryptString(string Message, string Passphrase)

        {

            byte[] Results;

            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. Bam chuoi su dung MD5

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();

            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Tao doi tuong TripleDESCryptoServiceProvider moi

            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Cai dat bo giai ma

            TDESAlgorithm.Key = TDESKey;

            TDESAlgorithm.Mode = CipherMode.ECB;

            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert chuoi (Message) thanh dang byte[]

            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Bat dau giai ma chuoi

            try

            {

                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();

                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);

            }

            finally

            {

                // Xoa moi thong tin ve Triple DES va HashProvider de dam bao an toan

                TDESAlgorithm.Clear();

                HashProvider.Clear();

            }

            // Step 6. Tra ve ket qua bang dinh dang UTF8

            return UTF8.GetString(Results);

        }

        static string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bẩy", " tám", " chín" };
        static string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
        // Hàm đọc số thành chữ
        public static string Doc_So_Thanh_Chu(string _SoTien, string strTail)
        {
            long SoTien = Func.Parse(_SoTien);

            int lan, i;
            long so;
            string KetQua = "", tmp = "";
            int[] ViTri = new int[6];
            if (SoTien < 0) return "";//Số tiền âm !
            if (SoTien == 0) return "Không";
            if (SoTien > 0)
            {
                so = SoTien;
            }
            else
            {
                so = -SoTien;
            }
            //Kiểm tra số quá lớn
            if (SoTien > 8999999999999999)
            {
                SoTien = 0;
                return "";
            }
            ViTri[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
            ViTri[4] = (int)(so / 1000000000000);
            so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
            ViTri[3] = (int)(so / 1000000000);
            so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
            ViTri[2] = (int)(so / 1000000);
            ViTri[1] = (int)((so % 1000000) / 1000);
            ViTri[0] = (int)(so % 1000);
            if (ViTri[5] > 0)
            {
                lan = 5;
            }
            else if (ViTri[4] > 0)
            {
                lan = 4;
            }
            else if (ViTri[3] > 0)
            {
                lan = 3;
            }
            else if (ViTri[2] > 0)
            {
                lan = 2;
            }
            else if (ViTri[1] > 0)
            {
                lan = 1;
            }
            else
            {
                lan = 0;
            }
            for (i = lan; i >= 0; i--)
            {
                tmp = DocSo3ChuSo(ViTri[i]);
                KetQua += tmp;
                if (ViTri[i] != 0) KetQua += Tien[i];
                if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
            }
            if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
            KetQua = KetQua.Trim() + strTail;
            return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
        }

        private static string DocSo3ChuSo(int baso)
        {
            int tram, chuc, donvi;
            string KetQua = "";
            tram = (int)(baso / 100);
            chuc = (int)((baso % 100) / 10);
            donvi = baso % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0)
            {
                KetQua += ChuSo[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) KetQua += " linh";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                KetQua += ChuSo[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " linh";
            }
            if (chuc == 1) KetQua += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        KetQua += " mốt";
                    }
                    else
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    else
                    {
                        KetQua += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        KetQua += ChuSo[donvi];
                    }
                    break;
            }
            return KetQua;
        }
    #endregion

    #region Các hàm cho app M1 mini

    public static string load_config(string name)
        {
            if (name == "l1_mini")
            {
                string DecryptedString = "";
                string l1_mini = ConfigurationManager.AppSettings[name];
                if (l1_mini != "") DecryptedString = Func.DecryptString(l1_mini, name);

                return DecryptedString;
            }
            else
            {
                string value = ConfigurationManager.AppSettings[name];

                return value;
            }
        }
        public static void config(string name, string value)
        {
            if (name == "l1_mini")
                value = Func.EncryptString(value, name);

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[name].Value = value;
            configuration.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }
        public static void get_config_from_server()
        {
            try
            {
                if (Const.local_user != null)
                {
                    string CAUHINH_KIOS = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "CAUHINH.KIOS");

                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(CAUHINH_KIOS, (typeof(DataTable)));
                    if (dt != null)
                    {
                        if (dt.Columns.Contains("L1_kieucapso")) Const.L1_kieucapso = dt.Rows[0]["L1_kieucapso"].ToString();
                        if (dt.Columns.Contains("L1_dkkham")) Const.L1_dkkham = dt.Rows[0]["L1_dkkham"].ToString();
                        if (dt.Columns.Contains("L1_ktraBHYT")) Const.L1_ktraBHYT = dt.Rows[0]["L1_ktraBHYT"].ToString();
                        if (dt.Columns.Contains("L1_autoPrinter")) Const.L1_autoPrinter = dt.Rows[0]["L1_autoPrinter"].ToString();
                        if (dt.Columns.Contains("L1_phanhe")) Const.L1_phanhe = dt.Rows[0]["L1_phanhe"].ToString();

                        return;
                    }
                }
            }
            catch(Exception ex) { }

            //mặc định
            Const.L1_kieucapso = "1";
            Const.L1_dkkham = "1";
            Const.L1_ktraBHYT = "";
            Const.L1_autoPrinter = "";
            Const.L1_phanhe = "";
        } 
        public static void get_config_local()
        {
            try
            {
                // lúc này chưa đăng nhập, lấy thông tin đã lưu trước đó ở App config
                string CAUHINH_KIOS = Func.load_config("LOCAL_CAUHINH_KIOS");
                //Const.LOCAL_CAUHINH_KIOS = CAUHINH_KIOS;

                DataTable dt = (DataTable)JsonConvert.DeserializeObject(CAUHINH_KIOS, (typeof(DataTable)));
                if (dt != null)
                {

                    if (dt.Columns.Contains("L1_BV_DEFAULT")) Const.L1_BV_DEFAULT = Convert.ToInt16(dt.Rows[0]["L1_BV_DEFAULT"].ToString());
                    if (dt.Columns.Contains("L1_mini")) Const.L1_mini = dt.Rows[0]["L1_mini"].ToString();
                    if (dt.Columns.Contains("L1_MAU_STT")) Const.L1_MAU_STT = dt.Rows[0]["L1_MAU_STT"].ToString();
                    if (dt.Columns.Contains("L1_XemPhieuSTT")) Const.L1_XemPhieuSTT = Convert.ToBoolean(dt.Rows[0]["L1_XemPhieuSTT"].ToString());
                    if (dt.Columns.Contains("L1_autologin")) Const.L1_autologin = Convert.ToBoolean(dt.Rows[0]["L1_autologin"].ToString());
                    if (dt.Columns.Contains("LinkService")) Const.LinkService = dt.Rows[0]["LinkService"].ToString();
                    if (dt.Columns.Contains("L1_CoSoID")) Const.L1_CoSoID = dt.Rows[0]["L1_CoSoID"].ToString();
                    return;
                }
            }
            catch (Exception ex) { }

            //mặc định
            Const.L1_BV_DEFAULT = 0; // 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM;
            Const.L1_mini = "";
            Const.L1_MAU_STT = "MAU_STT_DEFAULT";
            //Const.L1_XemPhieuSTT = true;
            Const.L1_autologin = true;
            Const.LinkService = "https://histestl2.vncare.vn/vnpthis/RestService";
            Const.AcsTcp_IP = "192.168.0.102";
            Const.AcsTcp_Port = "8000";


        }
        public static void get_config_file()
        { 
            try
            {
                Const.LOCAL_CAUHINH_KIOS = Func.FromHex(File.ReadAllText(Const.FolderSaveFilePrint + "\\" + Const.config_data));
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(Const.LOCAL_CAUHINH_KIOS, (typeof(DataTable)));
                if (dt != null)
                {

                    if (dt.Columns.Contains("L1_BV_DEFAULT")) Const.L1_BV_DEFAULT = Convert.ToInt16(dt.Rows[0]["L1_BV_DEFAULT"].ToString());
                    if (dt.Columns.Contains("L1_mini")) Const.L1_mini = dt.Rows[0]["L1_mini"].ToString();
                    if (dt.Columns.Contains("L1_MAU_STT")) Const.L1_MAU_STT = dt.Rows[0]["L1_MAU_STT"].ToString();
                    if (dt.Columns.Contains("L1_XemPhieuSTT")) Const.L1_XemPhieuSTT = Convert.ToBoolean(dt.Rows[0]["L1_XemPhieuSTT"].ToString());
                    if (dt.Columns.Contains("L1_autologin")) Const.L1_autologin = Convert.ToBoolean(dt.Rows[0]["L1_autologin"].ToString());
                    if (dt.Columns.Contains("LinkService")) Const.LinkService = dt.Rows[0]["LinkService"].ToString();
                    if (dt.Columns.Contains("L1_CoSoID")) Const.L1_CoSoID = dt.Rows[0]["L1_CoSoID"].ToString();
                    if (dt.Columns.Contains("L1_LoaiBHYT")) Const.L1_LoaiBHYT = (dt.Rows[0]["L1_LoaiBHYT"].ToString());
                
                    if (dt.Columns.Contains("L1_kieucapso")) Const.L1_kieucapso = dt.Rows[0]["L1_kieucapso"].ToString();
                    if (dt.Columns.Contains("L1_dkkham")) Const.L1_dkkham = dt.Rows[0]["L1_dkkham"].ToString();
                    if (dt.Columns.Contains("L1_ktraBHYT")) Const.L1_ktraBHYT = dt.Rows[0]["L1_ktraBHYT"].ToString();
                    if (dt.Columns.Contains("L1_autoPrinter")) Const.L1_autoPrinter = dt.Rows[0]["L1_autoPrinter"].ToString();
                    if (dt.Columns.Contains("L1_phanhe")) Const.L1_phanhe = dt.Rows[0]["L1_phanhe"].ToString();

                    if (dt.Columns.Contains("default_test")) Const.default_test = Convert.ToInt16(dt.Rows[0]["default_test"].ToString());
                    
                    return;
                }
            }
            catch (Exception ex) { }

            //mặc định
            Const.L1_BV_DEFAULT = 0; // 0:TEST; 1:BM2; 2:VD2; 3:KHA; 4:BND; 5:HNM;
            Const.L1_mini = "";
            Const.L1_MAU_STT = "MAU_STT_DEFAULT";
            //Const.L1_XemPhieuSTT = true;
            Const.L1_autologin = true;
            Const.LinkService = "https://histestl2.vncare.vn/vnpthis/RestService";
            Const.AcsTcp_IP = "192.168.0.102";
            Const.AcsTcp_Port = "8000";

            Const.L1_kieucapso = "1";
            Const.L1_dkkham = "1";
            Const.L1_ktraBHYT = "";
            Const.L1_autoPrinter = "";
            Const.L1_phanhe = "";

            Const.L1_LoaiBHYT = "1,2";
        }
        public static void set_config_file()
        {
            try
            {
                object data_local = new
                {
                    L1_BV_DEFAULT = Const.L1_BV_DEFAULT,
                    L1_mini = Const.L1_mini,
                    L1_MAU_STT = Const.L1_MAU_STT,
                    L1_XemPhieuSTT = Const.L1_XemPhieuSTT,
                    L1_autologin = Const.L1_autologin,
                    LinkService = Const.LinkService,
                    AcsTcp_IP = Const.AcsTcp_IP,
                    AcsTcp_Port = Const.AcsTcp_Port,
                    L1_CoSoID = Const.L1_CoSoID,
                    L1_LoaiBHYT = Const.L1_LoaiBHYT,

                    L1_kieucapso = Const.L1_kieucapso,
                    L1_dkkham = Const.L1_dkkham,
                    L1_ktraBHYT = Const.L1_ktraBHYT,
                    L1_autoPrinter = Const.L1_autoPrinter,
                    L1_phanhe = Const.L1_phanhe,

                    default_test=Const.default_test
                };
                string json = "[" + Newtonsoft.Json.JsonConvert.SerializeObject(data_local) + "]";
                File.WriteAllText(Const.FolderSaveFilePrint + "\\" + Const.config_data, Func.ToHex(json));
            }
            catch (Exception ex) { }
        }
        public static void set_log_file(string str)
        {
            try
            {
                string path = Const.FolderSaveFilePrint + "\\loghis_"+System.DateTime.Now.ToString("yyyyMMdd")+".txt";
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Begin log"); 
                    }
                }

                using (StreamWriter sw = File.AppendText(path)) sw.WriteLine(System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss ") + str);
            }
            catch (Exception ex) { }
        }


        public static void Reset_Logo_BenhVien(out string full_path_logo)
        {
//HisTest
//BV Bạch Mai 2
//BV Việt Đức 2
//BV Khánh Hòa
//BV Nhiệt Đới Trung Ương
//BV ĐK Hà Nam
            if (Const.L1_BV_DEFAULT == 1)
            {
                full_path_logo = Application.StartupPath + "./Resources/bg_bachmai.png".Substring(1).Replace("/", "\\"); 
                Const.L1_MAU_STT = "MAU_STT_BACHMAI";
            }
            else if (Const.L1_BV_DEFAULT == 2)
            {
                full_path_logo = Application.StartupPath + "./Resources/bg_vietduc.png".Substring(1).Replace("/", "\\"); 
                Const.L1_MAU_STT = "MAU_STT_VIETDUC";
            }
            else if (Const.L1_BV_DEFAULT == 3)
            {
                full_path_logo = Application.StartupPath + "./Resources/bg_khanhhoa.png".Substring(1).Replace("/", "\\");
                Const.L1_MAU_STT = "MAU_STT_KHANHHOA";
            }
            else if (Const.L1_BV_DEFAULT == 4)
            {
                full_path_logo = Application.StartupPath + "./Resources/bg_nhietdoi.png".Substring(1).Replace("/", "\\"); 
                Const.L1_MAU_STT = "MAU_STT_NHIETDOI";
            }
            else if (Const.L1_BV_DEFAULT == 5)
            {
                full_path_logo = Application.StartupPath + "./Resources/bg_dkhanam.png".Substring(1).Replace("/", "\\");
                Const.L1_MAU_STT = "MAU_STT_DKHANAM";
            }
            else if (Const.L1_BV_DEFAULT > 5)
            {
                full_path_logo = Application.StartupPath + "./Resources/bg_"+ Const.L1_BV_DEFAULT + ".png".Substring(1).Replace("/", "\\");
                Const.L1_MAU_STT = "MAU_STT_"+ Const.L1_BV_DEFAULT;
            }

            else
            {
                full_path_logo = Application.StartupPath + "./Resources/bg_default.png".Substring(1).Replace("/", "\\");
                Const.L1_MAU_STT = "MAU_STT_DEFAULT";
                if (Const.default_test == 1)
                {
                    full_path_logo = Application.StartupPath + "./Resources/bg_bachmai.png".Substring(1).Replace("/", "\\");
                    Const.L1_MAU_STT = "MAU_STT_BACHMAI";
                }
                else if (Const.default_test == 2)
                {
                    full_path_logo = Application.StartupPath + "./Resources/bg_vietduc.png".Substring(1).Replace("/", "\\");
                    Const.L1_MAU_STT = "MAU_STT_VIETDUC";
                }
                else if (Const.default_test == 3)
                {
                    full_path_logo = Application.StartupPath + "./Resources/bg_khanhhoa.png".Substring(1).Replace("/", "\\");
                    Const.L1_MAU_STT = "MAU_STT_KHANHHOA";
                }
                else if (Const.default_test == 4)
                {
                    full_path_logo = Application.StartupPath + "./Resources/bg_nhietdoi.png".Substring(1).Replace("/", "\\");
                    Const.L1_MAU_STT = "MAU_STT_NHIETDOI";
                }
                else if (Const.default_test == 5)
                {
                    full_path_logo = Application.StartupPath + "./Resources/bg_dkhanam.png".Substring(1).Replace("/", "\\");
                    Const.L1_MAU_STT = "MAU_STT_DKHANAM";
                }
                else if (Const.default_test > 5)
                {
                    full_path_logo = Application.StartupPath + "./Resources/bg_" + Const.default_test + ".png".Substring(1).Replace("/", "\\");
                    Const.L1_MAU_STT = "MAU_STT_" + Const.default_test;
                }
            }
        }

        #endregion

    }
    public class ItemCombo
    {
        public String name { get; set; }
        public String value { get; set; }
        public ItemCombo(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
