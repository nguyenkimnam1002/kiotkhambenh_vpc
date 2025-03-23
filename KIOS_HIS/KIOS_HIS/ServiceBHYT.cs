using System;
using System.Net;
using System.IO;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace L1_Mini
{
    public class ServiceBHYT
    {
        //=====================================RequestPOST(string url, string param)==============================
        private static string RequestPOST(string url, string param)
        {
            string ret = "";
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

                request.ContentType = "application/json";
                request.Method = "POST";
                request.Timeout = 8000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(param);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

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
                Func.set_log_file("LOI request BHYT :" + url+ " | " + param + " | " + ex.ToString());
            }
            return ret;
        }

        //====================================================Lay_thong_tin_ws_BHYT()=============================================================
        static wsBHYT_respons obLogin = null;
        static DateTime expires_in;
        
        // Biến dùng cho ws Bảo hiểm y tế
        public static string ServiceBHYT_Url = "";
        public static string ServiceBHYT_Username = "";
        public static string ServiceBHYT_PasswordMD5 = "";
        public static string ServiceBHYT_MACSKCB = ""; 

        static string LOGIN = "/api/token/take";
        public static bool Lay_thong_tin_ws_BHYT()
        {
            ServiceBHYT_Url = Const.ServiceBHYT_Url;

            if (ServiceBHYT_Username == "")
            {
                DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K047.TTTK", "1", 0);
                if (dt.Rows.Count > 0)
                {
                    ServiceBHYT_Username = dt.Rows[0]["I_U"].ToString();
                    ServiceBHYT_PasswordMD5 = Func.GetMd5Hash(dt.Rows[0]["I_P"].ToString());
                    ServiceBHYT_MACSKCB = dt.Rows[0]["I_MACSKCB"].ToString();

                    //// nhân tiện lấy cho BYT
                    //ServiceBYT.ServiceBYT_Username = dt.Rows[0]["I_U1"].ToString();
                    //ServiceBYT.ServiceBYT_Password = Func.GetMd5Hash(dt.Rows[0]["I_P1"].ToString());
                    return true;
                }
            }

            if (ServiceBHYT_Username == "" || ServiceBHYT_Url == "")
            { 
                MessageBox.Show("Không lấy được thông tin truy cập cổng BHYT!");
                return false;
            }
            else
                return true;
        }
        //=====================================Login()==============================
        public static bool Login()
        {
            DateTime dt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            if (obLogin != null && expires_in >= dt) return true; 

            try
            {
                string ret = "";

                if (ServiceBHYT_Username == "" || ServiceBHYT_PasswordMD5 == "" || ServiceBHYT_Url == "")
                    if (Lay_thong_tin_ws_BHYT() == false) return false;

                string url = ServiceBHYT_Url + LOGIN;
                string jsonData = "{\"username\":\"" + ServiceBHYT_Username + "\",\"password\":\"" + ServiceBHYT_PasswordMD5 + "\"}";
                ret = RequestPOST(url, jsonData);

                Func.set_log_file("Login :"+ url +"|"+ jsonData + "|"+ ret);

                // {"maKetQua":"200","APIKey":{"access_token":"UmdyY0lLaWJJTkU5amFxMmxoVVVMbHVDK1lSSTBwMTlweFVlcFZCTk1yRT06NzkwMTRfQlY6MTMxNTUwMTgwODAxMDQ3MTg3",
                // "id_token":"51094abb-3e1f-4744-bf5b-4e49a1795ae2","token_type":"Bearer","username":"79014_BV","expires_in":"2017-11-13T03:51:20.1047187Z"}}
                //ret = "{\"maKetQua\":\"200\",\"APIKey\":{\"access_token\":\"Rkw4S0lEMUxaSVlzTktxSmU1K1luZGJsMHRieGcxVnNqWVBIcTFwR0kyUT06NzkwMTRfQlY6MTMxNTUwMTQ5MTQ0MTA0OTg3\",\"id_token\":\"d2c89470-5867-4f77-aa87-36db23baacd1\",\"token_type\":\"Bearer\",\"username\":\"79014_BV\",\"expires_in\":\"2017-11-13T02:58:34.4104987Z\"}}";
                obLogin = JsonConvert.DeserializeObject<wsBHYT_respons>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                if (obLogin != null && obLogin.maKetQua == "200")
                {
                    // "2017-11-13T03:51:20.1047187Z"
                    string str = obLogin.APIKey.expires_in.Substring(0, obLogin.APIKey.expires_in.LastIndexOf(".")).Replace("T", " ");
                    expires_in = DateTime.ParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    //DateTime dt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Func.set_log_file("lỗi Login cổng bhyt:" + ex);
            }
            return false;
        }

        //=====================================wsBHYT_LichSu_respons_2018 Get_History010118(string ma_the, string ho_ten, string ngay_sinh)==============================
        static string URI_HISTORY_010118 = "/api/egw/KQNhanLichSuKCB2024";
        public static wsBHYT_LichSu_respons_2018 Get_History010118(string ma_the, string ho_ten, string ngay_sinh)
        {
            if (Login() == false)
            {
                Func.set_log_file("LOI_login");
                return null;
            }

            string json = "";

            json += Func.json_item("maThe", ma_the.Trim());
            json += Func.json_item("hoTen", ho_ten.Trim());
            json += Func.json_item("ngaySinh", ngay_sinh.Trim() == "" ? "2020" : ngay_sinh.Trim()); // ko đc rỗng: phải là dạng dd/MM/yyyy hoặc yyyy

            DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("KIOS.CCCD", "0$", 0);

            if (dt.Rows.Count > 0)
            {
                json += Func.json_item("cccdCb", dt.Rows[0]["CMND"].ToString());
                json += Func.json_item("hoTenCb", dt.Rows[0]["TEN"].ToString());
            }
            json = "{" + json.Substring(0, json.Length - 1) + "}";

            wsBHYT_LichSu_respons_2018 KetQua = null;
            try
            {
                Func.set_log_file("req bhyt:");
                string strParam = "?token=" + obLogin.APIKey.access_token + "&id_token=" + obLogin.APIKey.id_token
                                        + "&username=" + ServiceBHYT_Username + "&password=" + ServiceBHYT_PasswordMD5;
                string url = ServiceBHYT_Url + URI_HISTORY_010118 + strParam;

                string ret = RequestPOST(url, json);
                Func.set_log_file("KQ từ cổng bhyt:" + ret);

                KetQua = JsonConvert.DeserializeObject<wsBHYT_LichSu_respons_2018>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                //table = MyJsonConvert.toDataTable("["+ret+"]");
            }
            catch (Exception ex)
            {
                //log.Fatal(ex.ToString());
                Func.set_log_file("LOI2: lỗi tra từ cổng bhyt:" + ex);
            }
            return KetQua;

            // {"maKetQua":"090","hoTen":"Nguyễn Hồng Hải","gioiTinh":"Nam","diaChi":"11 trệt nguyễn duy dương,p8,q5","maDKBD":"79014","cqBHXH":"Bảo hiểm Xã hội quận 5","gtTheTu":"01/01/2017","gtTheDen":"31/12/2021","maKV":"","ngayDu5Nam":"01/12/2015",
            // "dsLichSuKCB":[{"maHoSo":535595540,"maCSKCB":"79014","tuNgay":"28/09/2017","denNgay":"28/09/2017","tenBenh":"E11....;","tinhTrang":"1","kqDieuTri":"1"},{"maHoSo":443409000,"maCSKCB":"79014","tuNgay":"03/05/2017","denNgay":"03/05/2017","tenBenh":"J00 -  - Viêm mũi họng cấp [cảm thường]","tinhTrang":"","kqDieuTri":""}]}

        }

        //===================================wsBHYT_LichSu_respons_Detail1 Get_HistoryDetail1(string maHoSo)================================
        static string URI_HISTORY_DETAIL1 = "/api/egw/nhanHoSoKCBChiTiet";
        public static wsBHYT_LichSu_respons_Detail1 Get_HistoryDetail1(string maHoSo)
        {
            if (Login() == false) return null;

            string json = "";
            wsBHYT_LichSu_respons_Detail1 KetQua = null;
            try
            {
                string strParam = "?token=" + obLogin.APIKey.access_token + "&id_token=" + obLogin.APIKey.id_token
                                        + "&username=" + ServiceBHYT_Username + "&password=" + ServiceBHYT_PasswordMD5 + "&maHoSo=" + maHoSo;
                string url = ServiceBHYT_Url + URI_HISTORY_DETAIL1 + strParam;

                string ret = RequestPOST(url, json);

                KetQua = JsonConvert.DeserializeObject<wsBHYT_LichSu_respons_Detail1>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                
            }
            catch (Exception ex)
            {
               // log.Fatal(ex.ToString());
            }
            return KetQua;
        } 
    }

    //=====================================START CLASS DEFINITION==============================
    public class wsBHYT_respons
    {
        public string maKetQua { get; set; }
        public wsBHYT_respons_detail APIKey { get; set; }
    }
    public class wsBHYT_respons_detail
    {
        public string access_token { get; set; }
        public string id_token { get; set; }
        public string token_type { get; set; }
        public string username { get; set; }
        public string expires_in { get; set; }
    }

    public class wsBHYT_LichSu_respons
    {
        public string maKetQua { get; set; }
        public string hoTen { get; set; }
        public string gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string maDKBD { get; set; }
        public string cqBHXH { get; set; }
        public string gtTheTu { get; set; }
        public string gtTheDen { get; set; }
        public string maKV { get; set; }
        public string ngayDu5Nam { get; set; }
        public List<wsBHYT_LichSu_respons_detail> dsLichSuKCB { get; set; }
    }
    public class wsBHYT_LichSu_respons_detail
    {
        public string maHoSo { get; set; }
        public string maCSKCB { get; set; }
        public string tuNgay { get; set; }
        public string denNgay { get; set; }
        public string tenBenh { get; set; }
        public string tinhTrang { get; set; }
        public string kqDieuTri { get; set; }
    }

    public class wsBHYT_LichSu_respons_2018
    {
        public string maKetQua { get; set; }
        public string ghiChu { get; set; }
        public string maThe { get; set; }
        public string hoTen { get; set; }
        public string ngaySinh { get; set; }
        public string diaChi { get; set; }
        public string gioiTinh { get; set; }
        public string maDKBD { get; set; }
        public string cqBHXH { get; set; }
        public string gtTheTu { get; set; }
        public string gtTheDen { get; set; }
        public string maKV { get; set; }
        public string ngayDu5Nam { get; set; }
        public string maSoBHXH { get; set; }
        public string maTheCu { get; set; }
        public string maTheMoi { get; set; }
        public string gtTheTuMoi { get; set; }
        public string gtTheDenMoi { get; set; }
        public List<wsBHYT_LichSu_respons_2018_detail> dsLichSuKCB2018 { get; set; }
        public List<wsBHYT_LichSuKT_respons_2018_detail> dsLichSuKT2018 { get; set; }
    }
    public class wsBHYT_LichSu_respons_2018_detail
    {
        public string maHoSo { get; set; }
        public string maCSKCB { get; set; }
        public string tenCSKCB { get; set; }
        public string ngayVao { get; set; }
        public string ngayRa { get; set; }
        public string tenBenh { get; set; }
        public string tinhTrang { get; set; }
        public string kqDieuTri { get; set; }
    }
    public class wsBHYT_LichSuKT_respons_2018_detail
    {
        public string userKT { get; set; }
        public string thoiGianKT { get; set; }
        public string thongBao { get; set; }
        public string maLoi { get; set; }
    }

    public class wsBHYT_LichSu_respons_Detail1
    {
        public string maKetQua { get; set; }
        public wsBHYT_LichSu_respons_Detail1_hoSoKCB hoSoKCB { get; set; }
    }

    public class wsBHYT_LichSu_respons_Detail1_hoSoKCB
    {
        public wsBHYT_LichSu_respons_Detail1_xml1 xml1 { get; set; }
        public List<wsBHYT_LichSu_respons_Detail1_dsXml2> dsXml2 { get; set; }
        public List<wsBHYT_LichSu_respons_Detail1_dsXml3> dsXml3 { get; set; }
        public List<wsBHYT_LichSu_respons_Detail1_dsXml4> dsXml4 { get; set; }
        public List<wsBHYT_LichSu_respons_Detail1_dsXml5> dsXml5 { get; set; }
    }

    public class wsBHYT_LichSu_respons_Detail1_xml1
    {
        public string maKetQua { get; set; }
        public string HoTen { get; set; }
        public string TenChame { get; set; }
        public string CanNang { get; set; }
        public string MaBn { get; set; }
        public string MaKhoa { get; set; }
        public string TinhTrangRv { get; set; }
        public string MaBenh { get; set; }
        public string TenBenh { get; set; }
        public string MaBenhkhac { get; set; }
        public string MaPtttQt { get; set; }
        public string MaTaiNan { get; set; }
        public string SoNgayDtri { get; set; }
        public string NgayVao { get; set; }
        public string NgayRa { get; set; }
        public string Ngaynhap { get; set; }
        public string Ngaythanhtoan { get; set; }

        public string MaThe { get; set; }
        public string GtTheTu { get; set; }
        public string GtTheDen { get; set; }
        public string MucHuong { get; set; }

        public string TTongchi { get; set; }
        public string TBhtt { get; set; }
        public string TBntt { get; set; }
        public string TNguonkhac { get; set; }
        public string TNgoaids { get; set; }
        public string TThuoc { get; set; }
        public string Tienkham { get; set; }
        public string TVtyt { get; set; }
        public string Tiengiuong { get; set; }
        public string Tienvanchuyen { get; set; }
    }

    public class wsBHYT_LichSu_respons_Detail1_dsXml2
    {
        public string Id { get; set; }
        public string MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string DonViTinh { get; set; }
        public string HamLuong { get; set; }
        public string DuongDung { get; set; }
        public string LieuDung { get; set; }
        public string ThanhTien { get; set; }
    }

    public class wsBHYT_LichSu_respons_Detail1_dsXml3
    {
        public string Id { get; set; }
        public string MaDichVu { get; set; }
        public string TenDichVu { get; set; }
        public string DonViTinh { get; set; }
        public string SoLuong { get; set; }
        public string ThanhTien { get; set; }
    }

    public class wsBHYT_LichSu_respons_Detail1_dsXml4
    {
        public string Id { get; set; }
    }

    public class wsBHYT_LichSu_respons_Detail1_dsXml5
    {
    }
    //=====================================END CLASS DEFINITION==============================
}
