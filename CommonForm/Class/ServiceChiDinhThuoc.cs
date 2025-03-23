using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPT.HIS.Common;
using System.Data;
using Newtonsoft.Json;

namespace VNPT.HIS.CommonForm.Class
{
    public class ServiceChiDinhThuoc
    {

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //get databenhnhan
        public static DataBN getThongTinBN(string param) 
        {
            // {"func":"ajaxCALL_SP_O","params":["NTU02D010.10","100670$4954",0],"uuid":"eddbc3aa-fc62-4161-b2a1-3edfb23b906c"} 
            // {"result": "[{\"BENHNHANID\": \"45371\",\"MABENHNHAN\": \"BN00042019\",\"TENBENHNHAN\": \"MTEST &LT; 6 TUỔI\",\"NAMSINH\": \"2014\"
            //,\"TIEPNHANID\": \"95684\",\"KHAMBENHID\": \"100670\",\"GHICHU_BENHCHINH\": \"\",\"MAHOSOBENHAN\": \"BA18000052\",\"GIOITINHID\": \"2\"
            //    ,\"NGAYSINH\": \"01012014\",\"HOSOBENHANID\": \"117956\",\"DIACHI\": \"Xã Bình Khánh-Huyện Cần Giờ-TP Hồ Chí Minh\",\"NGHENGHIEP\": \"\"
            //    ,\"TRANGTHAIKHAMBENH\": \"4\",\"MACHANDOANRAVIEN\": \"A20.2\",\"CHANDOANRAVIEN\": \"Dịch hạch thể phổi\",\"TENCHANDOANICD_KT\": \"\"
            //    ,\"MA_BHYT\": \"Không có\",\"GIOITINH\": \"Nữ\",\"DOITUONGBENHNHANID\": \"2\",\"TEN_DTBN\": \"Viện phí\",\"TYLE_BHYT\": \"0\",\"LOAITIEPNHANID\": \"1\"
            //    ,\"HOPDONGID\": \"0\",\"THOIGIANVAOVIEN\": \"23/01/2018 11:08:51\",\"NGAYTIEPNHAN\": \"23/01/2018 11:05:07\",\"CHANDOANRAVIEN_KEMTHEO\": \"\"
            //    ,\"MACHANDOANVAOKHOA\": \"A02.2\",\"CHANDOANVAOKHOA\": \"Nhiễm salmonella khu trú\",\"CHANDOANVAOKHOA_KEMTHEO\": \"\",\"TRADU6THANGLUONGCOBAN\": \"0\"
            //    ,\"DUOC_VAN_CHUYEN\": \"0\",\"BHYT_KT\": \"\",\"INDONTHUOC\": \"0\",\"SONGAYKEMAX\": \"3\",\"SOLUONGTHUOCMAX\": \"200\",\"KENHIEUNGAY\": \"0\"
            //    ,\"INHUONGTHAN\": \"1\",\"CHANHOATCHAT\": \"1\",\"KECHUNGTHUOCVT\": \"0\",\"LOAICHECK\": \"0\",\"CACHDUNG\": \"1\",\"KIEUCHECK\": \"2\"
            //    ,\"CANHBAOPHACDO\": \"1\",\"SUDUNGPHACDO\": \"1\",\"CHECKDIUNGTHUOC\": \"1\",\"ANTIMCACHDUNGDT\": \"1\",\"CHECKTRUNGHOATCHAT\": \"1\"
            //    ,\"AN_CBO_LOAITHUOC\": \"0\",\"BACSI_KE\": \"0\",\"FORMAT_CD\": \"1\",\"KE_TUNHIEU_KHO\": \"1\",\"KIEUCHECK_HOATCHAT\": \"1\",\"SUDUNG_LIEUDUNG\": \"1\"
            //    ,\"KIEUCANHBAOTIENTAMUNG\": \"0\",\"SUDUNG_DVQD_KETHUOC\": \"0\",\"DS_ID_LOAITVT\": \"0,3,6,7,8,9\",\"PHIEUDTRI_KEDON\": \"0\"
            //    ,\"PHIEUDTRI_LOAIKEDON\": \"'02D010','02D011', '02D015','02D017', '02D019'\",\"PHIEUDTRI_TRATHUOC\": \"0\",\"AN_MENU_PHAI_KEDON\": \"0\"
            //    ,\"NGAYKEMAX\": \"20180413\",\"MAKHO\": \"1\",\"FOCUS_KETHUOC\": \"-1\",\"KTCODONTHUOC\": \"1\",\"KOKTKHICODONTHUOC\": \"1\",\"TUDONGIN\": \"0\"
            //    ,\"HANTHE\": null,\"NGAYTHE\": \"4\",\"CAPTHUOCLE\": \"1\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
            DataBN dataBN = new DataBN();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NTU02D010.10", param }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ret.result = ret.result.Trim();
                if (ret.result.StartsWith("[")) ret.result = ret.result.Substring(1);
                if (ret.result.EndsWith("]")) ret.result = ret.result.Substring(0, ret.result.Length - 1);
                dataBN = JsonConvert.DeserializeObject<DataBN>(ret.result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                System.Console.WriteLine("Bệnh nhân: " + ret.result);

                if (dataBN == null) dataBN = new DataBN();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dataBN;
        }
         
        public static string getSP_S(String funs, String ten)
        {
            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_HIENTHI_GOIKHAM"],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { funs, ten });
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
            // {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}
        }

        // loai thuoc
        public static DataTable getInfo(bool getFromCache, string tblName, string sql)
        {
            // {"func":"ajaxExecuteQuery","params":["","LOAITHUOCVATTU.01"],"options":[],"uuid":"4908971c-16a4-4ed2-a57e-386be8724013"}
            DataTable dt = new DataTable();
            // Kiểm tra lấy từ cache
            if (getFromCache)
            {
                Const.SQLITE.CacheObject_Select(tblName, out dt);
                if (dt.Rows.Count > 0) return dt;
            }
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", sql });
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, tblName);

                //Cập nhật vào Cache
                Const.SQLITE.CacheObject_Create(dt);
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return dt;
            // [["10","Thuốc trong danh mục BHYT"],["11","Thuốc ngoài danh mục"],["15","Vật tư y tế trong danh mục BHYT"],["17","Vật tư ngoài danh mục"]]
        }

        // phieu dieu tri
        public static DataTable getPhieuDieuTri(string funs, string value)
        {
            // {"func":"ajaxExecuteQuery","params":["","NTU02D010.08"],"options":[{"name":"[0]","value":"970"}],"uuid":"0a3c5aca-b15c-4136-9ae0-2f7a41c093f0"}
            DataTable dt = new DataTable(); 
            try
            {

                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", funs }
                   , new String[] { "[0]" }
                   , new String[] { value });
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, "NTU02D010.08"); 
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return dt;
            // [["10","Thuốc trong danh mục BHYT"],["11","Thuốc ngoài danh mục"],["15","Vật tư y tế trong danh mục BHYT"],["17","Vật tư ngoài danh mục"]]
        }


        // kho thuoc
        public static DataTable getKhoThuoc(string param1)
        {
            // {"func":"ajaxExecuteQuery","params":["","NTU02D010.08"],"options":[{"name":"[0]","value":"970"}],"uuid":"0a3c5aca-b15c-4136-9ae0-2f7a41c093f0"}
            DataTable dt = new DataTable(); 
            try
            {  
                string request = RequestHTTP.makeRequestParam("dbCALL_SP_R", new String[] { "", "NTU02D010.17", param1 }
                                        , new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);
                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ret.result = ret.result.Trim();

                dt = Func.fill_ArrayStr_To_Datatable(ret.result, "NTU02D010.17");
                 
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return dt;
            
        }
         
        public static DataTable getDSCachDung(bool getFromCache, string tblName, string[] sqlName, string[] nameList, string[] valueList)
        {
            DSCachDungDto dto = new DSCachDungDto();
            DataTable dt = new DataTable();
            if (getFromCache)// Kiểm tra lấy từ cache
            {
                Const.SQLITE.CacheObject_Select(tblName, out dt);
                if (dt.Rows.Count > 0) return dt;
            }
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", sqlName, nameList, valueList);
                string data = "func=doComboGrid" + "&page=1" + "&postData=" + request + "&rows=100000" + "&searchTerm=" + "&sidx=" + "&sord=";
                string ret = RequestHTTP.getRequest(data);
                dto = JsonConvert.DeserializeObject<DSCachDungDto>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                dt = dto.rows.ConvertListToDataTable<DSCachDung>();
                if (getFromCache)
                {
                    dt.TableName = tblName;
                    //Cập nhật vào Cache
                    Const.SQLITE.CacheObject_Create(dt);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }
    }

}
