using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net;

using VNPT.HIS.Common;

namespace VNPT.HIS.NgoaiTru.Class
{
    public class ServiceTiepNhanBenhNhan
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DataTable get123(bool getFromCache, bool updateCache, string tblName)
        {
            DataTable dt = new DataTable();

            // Kiểm tra lấy từ cache
            if (getFromCache)
            {
                Const.SQLITE.CacheObject_Select(tblName, out dt);
                if (dt.Rows.Count > 0) return dt;
            }

            try
            {
                string request = RequestHTTP.makeRequestParam("dbCALL_SP_R", new String[] { "", "DEPT.P01", "4$" + Const.local_user.USER_ID + "$" + Const.local_user.HOSPITAL_ID, "0" });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();

                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });



                dt = (DataTable)JsonConvert.DeserializeObject(resultSet.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
                dt.TableName = "DEPT_P01";

                //Cập nhật vào Cache
                if (updateCache) Const.SQLITE.CacheObject_Create(dt.TableName, dt);
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return dt;
        }
        // các đối tượng miễn giảm
        public static DataTable getDTDACBIET(bool getFromCache, bool updateCache, string tblName)
        {
            // {"func":"ajaxExecuteQuery","params":["","DMC.DTDACBIET"],"options":[],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            DataTable dt = new DataTable();
            // Kiểm tra lấy từ cache
            if (getFromCache)
            {
                Const.SQLITE.CacheObject_Select(tblName, out dt);
                if (dt.Rows.Count > 0) return dt;
            }
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", "DMC.DTDACBIET" });
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, tblName);

                //Cập nhật vào Cache
                if (updateCache) Const.SQLITE.CacheObject_Create(dt);
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return dt;
            // [["1","Người nghèo(5%)","5","CN"],["2","Cận nghèo(15%)","15","GD"]]
        }
        
        

        #region Lay ds phòng khám trong tìm kiếm
        // {"func":"ajaxExecuteQuery","params":["","LOADPK.TIMKIEM"],"options":[],"uuid":"e50d4962-0e22-4407-979f-aebf2d140891"}
         
        public static DsBenhnhan getDS_BenhNhan_TiepNhan(string tu_ngay, string den_ngay, string trangthaiId, string phongId
            , int page, int rows, string jsonFilter)
        {
            DsBenhnhan ds = new DsBenhnhan();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NGT.001" }
                    , new String[] { "[0]", "[1]", "[3]", "[4]" }
                    , new String[] { tu_ngay + " 00:00:00", den_ngay + " 23:59:59", trangthaiId, phongId });

                string data = 
                   "postData=" + request +
                   (jsonFilter == "" ? "" : "&_search=true" + "&filters={\"groupOp\":\"AND\",\"rules\":" + jsonFilter + "}") +
                    //"&nd=1496306404630"+
                   "&rows=" + rows +
                   "&page=" + page +
                    //"&sidx="+
                    "&sord=asc"
                ;


                string ret = RequestHTTP.getRequest(data);

                ds = JsonConvert.DeserializeObject<DsBenhnhan>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }

            return ds;
            //{"total": 1,"page": 1,"records": 1,"rows" : [{
            //"RN": "1",
            //"KHAMBENHID": "9386",
            //"MABENHNHAN": "BN00005927",
            //"TENBENHNHAN": "DATTTTT",
            //"MA_BHYT": "TE1401100000135",
            //"NGAYSINH": "04/09/2012",
            //"GIOITINH": "Nữ",
            //"TRANGTHAIKHAMBENH": "4",
            //"ORG_NAME": "231. Phòng khám Mắt",
            //"MAHOSOBENHAN": "BA00006126",
            //"KQCLS": "0",
            //"NGAYTIEPNHAN": "05/09/2017 09:20:51"  NGUOITN }] }
        }

        //Lấy chi tiết BN theo mã BN
        public static benhnhan_detail getBenhnhanDetail(string MaBN)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT01T002.TKBN","BN00003949$1$",0],"uuid":"9fde1db6-dac9-425b-8e2d-39aee49daf77"}
            benhnhan_detail benhnhan = new benhnhan_detail();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NGT01T002.TKBN", MaBN + "$1$"}, new int[] {0}
                    );
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret =new ResponsObj();                 
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                ret.result = ret.result.Trim();
                if (ret.result.StartsWith("[")) ret.result = ret.result.Substring(1);
                if (ret.result.EndsWith("]")) ret.result = ret.result.Substring(0, ret.result.Length-1);
                benhnhan = JsonConvert.DeserializeObject<benhnhan_detail>(ret.result, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return benhnhan;  
//                {
//"result": "[{"CHANDOANTUYENDUOI": "","MACHANDOANTUYENDUOI": "","MANOIGIOITHIEU": "","HINHTHUCVAOVIENID": "3","UUTIENKHAMID": "0","NGAYTIEPNHAN": "18/09/2017 14:42","DTBNID": "2","BENHNHANID": "2762","MABENHNHAN": "BN00001689","TENBENHNHAN": "307999999999999","NGAY_SINH": "01/04/2017","NAMSINH": "2017","TUOI": "20","TRANGTHAIKHAMBENH": "1","MA_BHYT": "","BHYT_BD": "","BHYT_KT": "","MA_KCBBD": "","DIACHI_BHYT": "","BHYT_LOAIID": null,"DT_SINHSONG": "","DU5NAM6THANGLUONGCOBAN": null,"QUYEN_LOI": null,"MUC_HUONG": null,"GIOITINHID": "2","NGHENGHIEPID": "3","DANTOCID": "25","QUOCGIAID": "0","SONHA": "","DIAPHUONGID": "3131711920","DIABANID": null,"TENDIAPHUONG": "Xã Nghĩa Lộ-Huyện Cát Hải-TP Hải Phòng","NOILAMVIEC": "","NGUOITHAN": "","TENNGUOITHAN": "","DIENTHOAINGUOITHAN": "","DIACHINGUOITHAN": "","TEN_KCBBD": "","TENNOIGIOITHIEU": "","SLXN": "0","SLCDHA": "0","DIACHI": "Xã Nghĩa Lộ-Huyện Cát Hải-TP Hải Phòng","DICHVUID": null,"ANHBENHNHAN": null,"THUKHAC": "0","SLCHUYENKHOA": "0","CONGKHAM": "0","SDTBENHNHAN": "","SINHTHEBHYT": "0","NGAYTHUOC": "01/01/1990 00:00:00","CHUADUYETKT": "0","NGAYMAUBENHPHAM": "20170918"}]",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}
        }
  
        
                

        //public static string getHienThiGoiKham()
        //{
        //    // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","HIS_HIENTHI_GOIKHAM"],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
        //    string ret = "";
        //    try
        //    {
        //        string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "COM.CAUHINH", "HIS_HIENTHI_GOIKHAM" });
        //        string resp = RequestHTTP.sendRequest(request);

        //        ResponsObj resultSet = new ResponsObj();

        //        resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        //        ret = resultSet.result;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }
        //    return ret;
        //    // {"result": "1","out_var": "[]","error_code": 0,"error_msg": ""}
        //}

        public static DataTable getMucHuong_BHYT(string phongid, string madoituong, string tuyen, string hinhthucvaovienid)
        {
            // {"func":"ajaxCALL_SP_O","params":["COM.MUCHUONG.BHYT","3559$TE1$1$1$3",0],"uuid":"c0a512e7-e407-4eb9-a262-09bfda6a1d42"}
            DataTable dt = new DataTable(); 
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "COM.MUCHUONG.BHYT", phongid + "$" + madoituong + "$" + tuyen + "$1$" + hinhthucvaovienid });
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
            // {"result": "[{\n\"MUCHUONG_NOI\": \"100\",\n\"MUCHUONG_NGOAI\": \"100\",\n\"BHYT_DOITUONG_ID\": \"98\"}]","out_var": "[]","error_code": 0,"error_msg":""}
        }
        
        // Lấy nơi đky khám chữa bệnh cho BHYT
        public static DataTable getNoiDK_KhamBHYT(string ID_DKKCB)
        {
            //  {"func":"ajaxExecuteQuery","params":["","DMC.BVKCBBD"],"options":[{"name":"[0]","value":"35148"}],"uuid":"8a69b92d-90f5-4484-813c-96df0d5149b3"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQuery", new String[] { "", "DMC.BVKCBBD" }
                    , new String[] { "[0]" }
                    , new String[] { ID_DKKCB });
                string resp = RequestHTTP.sendRequest(request);

                dt = Func.fill_ArrayStr_To_Datatable(resp, "DMC_BVKCBBD" + ID_DKKCB);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            // [["35148","Bệnh viện sản nhi Hà Nam"]]
        }
        

        
        
        public static DataTable getNGT_STT_GOI()
        {
            // {"func":"ajaxExecuteQueryO","params":["","NGT_STT_GOI"],"options":[{"name":"[0]","value":"05/06/2017"}],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryO", new String[] { "", "NGT_STT_GOI" }
                    , new String[] { "[0]" }
                    , new String[] { Func.getSysDatetime(Const.FORMAT_date1) });
                string resp = RequestHTTP.sendRequest(request);
                 
                dt = (DataTable)JsonConvert.DeserializeObject(resp, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            // [{"STT_BD": "-1","SL_GOI": "-1","DANGGOI": "0","SETYEUCAUKHAM": "0","TSFOCUS": "0","TSFOCUSDT": "3"}]
        }

        public static DataTable getNGT_STT_DT()
        {
            // {"func":"ajaxExecuteQueryO","params":["","NGT_STT_DT"],"options":[{"name":"[0]","value":"05/06/2017"}],"uuid":"24c456a8-21c0-4e8d-8f21-2dc7327e9e98"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryO", new String[] { "", "NGT_STT_DT" }
                    , new String[] { "[0]" }
                    , new String[] { Func.getSysDatetime(Const.FORMAT_date1) });
                string resp = RequestHTTP.sendRequest(request);

                dt = (DataTable)JsonConvert.DeserializeObject(resp, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //[{
            //"SOTHUTU": "0005",
            //"TSFOCUS": "0",
            //"TSDOITUONG": "1",
            //"DTUUTIEN": "-1",
            //"THUKHAC": "-1",
            //"MOPOPUPPHONGKHAM": "-1",
            //"NGAYTN": "1",
            //"BTNTHUTIEN": "0",
            //"CAPCUU": "-1",
            //"SONGAYBHYT": "1",
            //"NGAYVP": "1",
            //"DTMIENGIAM": "1",
            //"CHUPANH": "0",
            //"SINHTON": "0",
            //"GOIKHAM": "1",
            //"INPHIEU": "1",
            //"ANCHECKBOXBHYTDV": "1"}]
        }





        public static DataTable check_TrungBenhNhan(string tenbenhnhan, string ngaysinh, string gioitinhid) //01/08/2001
        {
            //{"func":"ajaxCALL_SP_O","params":["NGT01T002.TKBN","$3${"tenbenhnhan":"Nguyễn văn bb","ngaysinh":"01/08/2001","gioitinhid":"2"}"],"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", 
                    new String[] { "NGT01T002.TKBN", "$3${\\\"tenbenhnhan\\\":\\\"" + tenbenhnhan + "\\\",\\\"ngaysinh\\\":\\\"" + ngaysinh + "\\\",\\\"gioitinhid\\\":\\\"" + gioitinhid + "\\\"}" }
                    , new int[] { 0 }
                    );
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();
                resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = MyJsonConvert.toDataTable(resultSet.result);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;  // 

            //result	"[{"BENHNHANID": "8551","MAHOADULIEU": "0","MABENHNHAN": "BN00005519","NGAYHETHANLUUTRUHSBA": null,"NAMHETHANLUUTRUHSBA": null,"TENBENHNHAN": "NGUYỄN VĂN BB","NGAYSINH": "01/08/2001 00:00:00","NAMSINH": "2001","GIOITINHID": "2","NGHENGHIEPID": "3","DANTOCID": "25","QUOCGIAID": "0","SONHA": "","DIABANID": null,"DIAPHUONGID": "3500000000","NOILAMVIEC": "","NGUOITHAN": "","TENNGUOITHAN": "","DIENTHOAINGUOITHAN": "","DIACHINGUOITHAN": "","CMND": "","NOICAPCMND": "","NGAYCAPCMND": null,"NGAYDANGKY": null,"STTDANGKY": null,"NGAYCAPNHAT": "03/08/2017 15:33:00","ANHBENHNHAN": null,"KICHTHUOCANH": null,"GHICHU": "","VERSION": null,"SYNC_FLAG": null,"UPDATE_FLAG": null,"CSYTID": "915","BENHNHANCHAID": null},{"BENHNHANID": "8524","MAHOADULIEU": "0","MABENHNHAN": "BN00005517","NGAYHETHANLUUTRUHSBA": null,"NAMHETHANLUUTRUHSBA": null,"TENBENHNHAN": "NGUYỄN VĂN BB","NGAYSINH": "01/08/2001 00:00:00","NAMSINH": "2001","GIOITINHID": "2","NGHENGHIEPID": "3","DANTOCID": "25","QUOCGIAID": "0","SONHA": "","DIABANID": null,"DIAPHUONGID": "3500000000","NOILAMVIEC": "","NGUOITHAN": "","TENNGUOITHAN": "","DIENTHOAINGUOITHAN": "","DIACHINGUOITHAN": "","CMND": "","NOICAPCMND": "","NGAYCAPCMND": null,"NGAYDANGKY": null,"STTDANGKY": null,"NGAYCAPNHAT": "03/08/2017 15:28:00","ANHBENHNHAN": null,"KICHTHUOCANH": null,"GHICHU": "","VERSION": null,"SYNC_FLAG": null,"UPDATE_FLAG": null,"CSYTID": "915","BENHNHANCHAID": null}]"
            //out_var	"[]"
            //error_code	0
            //error_msg	""
        }
        public static string sinhThe_BHYT(string tinh, string huyen, string ngay_sinh)
        {
            //{"func":"ajaxCALL_SP_S","params":["COM.SINHTHE.BHYT","35$02$07067"],"uuid":"2d48a53c-d29b-4316-8e3e-467626367d76"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "COM.SINHTHE.BHYT", tinh + "$" + huyen + "$" + ngay_sinh });
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
            // {"result": "TE1350200000492","out_var": "[]","error_code": 0,"error_msg": ""}
        }
        public static string checkMaTheKhoa(string Dau_Ma_The)
        {
            // {"func":"ajaxCALL_SP_I","params":["COM.CHECK.THE.KHOA","ABC"],"uuid":"0596fe1d-2487-41a9-8894-94a5ab6b9f72"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_I", new String[] { "COM.CHECK.THE.KHOA", Dau_Ma_The});
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
            // {
//"result": "2",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}    
        }
        


        //SUBMIT tiếp nhận BN

        //getsysdate

        //{"func":"ajaxCALL_SP_O","params":["NGT01T002.TKBN","$3${\"tenbenhnhan\":\"test 2222\",\"ngaysinh\":\"\",\"gioitinhid\":\"2\"}",0],"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"}
        //res = {"result": "[]","out_var": "[]","error_code": 0,"error_msg": ""}



        /*
        {"func":"ajaxCALL_SP_S","params":["NGT01T002.LUUTT","{\"STT_BD\":\"1\",\"STT_KT\":\"1\",\"SL_GOI\":\"1
\",\"TENQUAY\":\"\",\"STT_BD1\":\"\",\"STT_KT1\":\"\",\"MABENHNHAN\":\"\",\"TENBENHNHAN\":\"test 2222
\",\"NGAYSINH\":\"\",\"NAMSINH\":\"1990\",\"TUOI\":\"27\",\"TKGIOITINHID\":\"2\",\"TKNGHENGHIEPID\":
\"1\",\"TKDANTOCID\":\"25\",\"TKQUOCGIAID\":\"0\",\"SONHA\":\"\",\"TKDIAPHUONGID\":\"\",\"TKHC_TINHID
\":\"35\",\"TKHC_HUYENID\":\"\",\"TKHC_XAID\":\"\",\"DIACHI\":\"Hà Nam\",\"NOILAMVIEC\":\"\",\"TENNGUOITHAN
\":\"\",\"DIACHINGUOITHAN\":\"\",\"DIENTHOAINGUOITHAN\":\"\",\"NGAYTIEPNHAN\":\"08/06/2017 13:53:26\"
,\"TKDICHVUID\":\"\",\"TKPHONGID\":\"\",\"MA_BHYT\":\"\",\"MA_KCBBD\":\"\",\"BHYT_BD\":\"01/01/2017\"
,\"BHYT_KT\":\"31/12/2017\",\"DIACHI_BHYT\":\"\",\"MUCHUONG\":\"Ngoại (0%)-Nội (0%)\",\"TKBHYT_LOAIID
\":\"1\",\"NGAYDU5NAM\":\"\",\"TKMACHANDOANTUYENDUOI\":\"\",\"CHANDOANTUYENDUOI\":\"\",\"TKMANOIGIOITHIEU
\":\"\",\"HOPDONGID\":null,\"DVTUOI\":\"1\",\"GIOITINHID\":\"2\",\"NGHENGHIEPID\":\"1\",\"DANTOCID\"
:\"25\",\"QUOCGIAID\":\"0\",\"TENQUOCGIA\":\"Việt Nam\",\"DIAPHUONGID\":null,\"HC_TINHID\":\"3500000000
\",\"TENTINH\":\"Hà Nam\",\"HC_HUYENID\":\"\",\"TENHUYEN\":\"Chọn\",\"HC_XAID\":\"\",\"TENXA\":\"Chọn
\",\"DT_SINHSONG\":\"\",\"DICHVUID\":\"291080\",\"YEUCAUKHAM\":\"1-Khám KHHGD Test LAN\",\"PHONGKHAMID
\":\"3594\",\"DTBNID\":\"2\",\"DOITUONGDB\":\"0\",\"MAKCBBD\":null,\"TEN_KCBBD\":\"\",\"DVTHUKHAC\":
\"0\",\"BHYT_LOAIID\":\"1\",\"MANOIGIOITHIEU\":\"\",\"TENNOIGIOITHIEU\":\"\",\"LS\":null,\"TIEPNHANID
\":\"\",\"BENHNHANID\":\"\",\"BHYTID\":\"\",\"PHONGKHAMDANGKYID\":\"\",\"KHAMBENHID\":\"\",\"MAUBENHPHAMID
\":\"\",\"USERID\":\"2124\",\"NOICHUYENID\":\"\",\"HOSOBENHANID\":\"\",\"DICHVUKHAMBENHID\":\"\",\"SOTHUTUID
\":\"\",\"SOTHUTUCLSID\":\"\",\"PHONGID_CU\":\"\",\"MUCHUONG_NGT\":\"0\",\"DOITUONGBENHNHANID\":\"\"
,\"LOAITIEPNHANID\":\"-1\",\"TRANGTHAIKHAMBENH\":\"\",\"MABENHAN\":\"\",\"BARCODE\":\"\",\"DICHVUKHAMID
\":\"\",\"BHYT_DOITUONG_ID\":\"\",\"PHONGID\":\"\",\"BNDIAPHUONGID\":\"3500000000\",\"INDEX\":\"-1\"
,\"SINHTHEBHYT\":\"0\",\"LICHHENID\":\"\",\"TRANGTHAIDICHVU\":\"\",\"CV_CHUYENVIEN_HINHTHUCID\":\"\"
,\"CV_CHUYENVIEN_LYDOID\":\"\",\"CV_CHUYENDUNGTUYEN\":\"0\",\"CV_CHUYENVUOTTUYEN\":\"1\",\"UUTIENKHAMID
\":\"0\",\"COGIAYKS\":\"0\",\"DAGIUTHEBHYT\":\"0\",\"CHECKBHYTDV\":\"0\",\"DU5NAM6THANGLUONGCOBAN\":
\"0\",\"HINHTHUCVAOVIENID\":\"3\",\"DIABANID\":\"\",\"MACHANDOANTUYENDUOI\":\"\",\"KHOAID\":\"3559\"
,\"KHAMBENH_MACH\":\"\",\"KHAMBENH_NHIETDO\":\"\",\"KHAMBENH_HUYETAP_LOW\":\"\",\"KHAMBENH_HUYETAP_HIGH
\":\"\",\"KHAMBENH_NHIPTHO\":\"\",\"KHAMBENH_CANNANG\":\"\",\"KHAMBENH_CHIEUCAO\":\"\"}$[]"]
,"uuid":"06acc2b5-d84d-41a1-9e60-3d798016d0ae"
}
         */
        public static string submitBenhNhanTiepNhan(BN_TiepNhan bn)
        {
            string ret = "";
            try
            {
                string strJson = JsonConvert.SerializeObject(bn);
                string request = "{\"func\":\"ajaxCALL_SP_S\",\"params\":[\"NGT01T002.LUUTT\",\""
                    + strJson.Replace("\\", "\\\\").Replace("\"", "\\\"")
                    + "$[]\"],\"uuid\":\""
                    + Const.local_user.UUID
                    + "\",\"code\":\"thu@nnc\"}";
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
            //res = {"result": "8282,BN00003951,5181,6232,6423,6236,,50766,80427","out_var": "[]","error_code": 0,"error_msg": ""}
        }
        
        





        #endregion
         
    }
}
