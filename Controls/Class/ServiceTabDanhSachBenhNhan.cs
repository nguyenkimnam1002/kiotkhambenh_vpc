using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Net;

using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.Class
{
    public class ServiceTabDanhSachBenhNhan
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region CLICK MENU PHẢI CÁC TAB
        public static string Gui_yeu_cau(string modeView, DataRowView drv, string KHOAID)
        {
            string ret = "";
            // check quyen xoa du lieu
            if (modeView != "2")
                if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                {
                    ret = "Bạn không có quyền gửi yêu cầu phiếu này!";
                    return ret;
                }
            // DÙNG CHO NỘI TRÚ
            // Check chuyen khoa dieu tri khong co quyen gui phieu
            if (KHOAID != "")
            { 
                string checkDtkh = RequestHTTP.getOneValue("CHECK.DTKH", new string[] { "[0]", "[1]" }, new string[] { KHOAID, drv["KHAMBENHID"].ToString() });
                if (checkDtkh != "0")
                {
                    ret = "Chuyên khoa điều trị kết hợp không được phép gửi yêu cầu!";
                    return ret;
                }
            }
            int _trangthai = 0;
            if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
            if (_trangthai == 1)
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("CLS.DEL.SENT.REQ", drv["MAUBENHPHAMID"].ToString() + "$2$0");
                if (_return == "1")
                {
                    ret = "TRUE Phiếu đã được gửi yêu cầu thành công!";
                    //reload();
                }
                else if (_return == "0")
                    ret = "Gửi yêu cầu phiếu thất bại!";
            }
            else
            {
                ret = "Phiếu đã được gửi yêu cầu!";
            }

            return ret;  
        }
        public static string Xoa_yeu_cau(string modeView, DataRowView drv, string KHOAID)
        {
            string ret = "";
            // check quyen xoa du lieu
            if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
            {
                ret = "Bạn không có quyền hủy yêu cầu phiếu này!" ;
                return ret;
            }
            int _trangthai = 0;
            if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
            if (_trangthai == 2)
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("CLS.DEL.SENT.REQ", drv["MAUBENHPHAMID"].ToString() + "$1$1");
                if (_return == "1")
                {                    
                    ret = "TRUE Phiếu đã được hủy yêu cầu thành công!";
                    //reload();
                }
                else if (_return == "0")
                    ret = "Hủy yêu cầu phiếu thất bại!";
                else if (_return == "-1")
                    ret = "Phiếu CĐHA đã thu tiền nên không được hủy yêu cầu";
            }
            else if (_trangthai == 1)
            {
                ret = "Phiếu đã được hủy yêu cầu!";
            }
            else if (_trangthai > 2)
            {
                ret = "Phiếu đã được xử lý nên không thể hủy yêu cầu";
            }
            return ret;
        }

        #endregion
        #region tab Danh sách bệnh nhân
        public static ResponsList getDsBenhNhan(int page, int number, string TuNgay, string DenNgay, string phongId, string trangthaiId, string maBN
            , string jsonFilter, string kbtraingay)
        {
            //_search	false
            //nd	1502676699603
            //page	1
            //postData		
//{"func":"ajaxExecuteQueryPaging","uuid":"c7077ea4-b264-4f1e-a446-6c8a94533718","params":["NGT02K001.EV001"
//],"options":[{"name":"[0]","value":"04/10/2016"},{"name":"[1]","value":"14/08/2017"},{"name":"[2]","value"
//:"4126"},{"name":"[3]","value":"49"},{"name":"[4]","value":""}]}
            //rows	100
            //sidx	
            //sord asc
            ResponsList ds = new ResponsList(); 
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NGT02K001.EV001" }
                    , new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]" }
                    , new String[] { TuNgay, DenNgay, phongId, trangthaiId, maBN, kbtraingay });

                string data = "page=" +page+
                   (jsonFilter == "" ? "" : "&_search=true" + "&filters={\"groupOp\":\"AND\",\"rules\":" + jsonFilter + "}") +
                   "&postData=" + request +
                   "&rows=" + number;

                string ret = RequestHTTP.getRequest(data);
                    ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
//{"total": 1,"page": 1,"records": 20,"rows" : [{
//"RN": "1",
//"PHONGKHAMDANGKYID": "1995",
//"SOTHUTU": "0001",
//"TRANGTHAI_CLS": null,
//"TRANGTHAI_STT": "4",
//"DASANSANG": "0",
//"KHAMBENHID": "2635",
//"DATHUTIENKHAM": null,
//"DAGIUTHEBHYT": "1",
//"TRANGTHAIKHAMBENH": "4",
//"UUTIENKHAMID": "0",
//"DOITUONGBENHNHANID": "1",
//"LANGOI": "0",
//"ORD": "0",
//"YEUCAUKHAM": "7-Khám Nội",
//"HINHTHUCVAOVIENID": "3",
//"MAHOADULIEU": "0",
//"BENHNHANID": "2987",
//"MAHOSOBENHAN": "BA00000096",
//"MABENHNHAN": "BN00000090",
//"HOSOBENHANID": "3045",
//"XUTRIKHAMBENHID": "1",
//"TENTRANGTHAIKB": "Đang khám",
//"TENBENHNHAN": "TET 2305",
//"MA_BHYT": "TE1350100000178",
//"MA_KCBBD": "35001",
//"TIEPNHANID": "2963",
//"LOAITIEPNHANID": "1",
//"KQCLS": null,
//"MADICHVU": "02.1896",
//"SSS": "1"},{
//"RN": "2",
        }
        public static DataTable getTongTienDV(string tiepnhanId)
        {
            // {"func":"ajaxCALL_SP_O","params":["VPI01T001.VIENPHI","2963",0],"uuid":"c7077ea4-b264-4f1e-a446-6c8a94533718"}
            DataTable dt = new DataTable(); 
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "VPI01T001.VIENPHI", tiepnhanId }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                dt = MyJsonConvert.toDataTable(ret.result);  
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
//            {"result": "[{\n\"TONGTIENDV\": \"1078100\",\n\"TAMUNG\": \"0\"}]",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}
        }
        public static DataTable getChiTietBN(string khambenhId, string phongId)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT02K001.EV002","2635$4126",0],"uuid":"c7077ea4-b264-4f1e-a446-6c8a94533718"}
            DataTable dt = new DataTable(); 
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NGT02K001.EV002", khambenhId + "$" + phongId }, new int[] { 0 });
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj ret = new ResponsObj();
                ret = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                System.Console.WriteLine("BENH NHANH: " + ret.result);
                dt = MyJsonConvert.toDataTable(ret.result);  
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
            //{"result": "[{\n\"MABENHNHAN\": \"BN00000090\",\n\"TENBENHNHAN\": \"TET 2305\",\n\"PHONGDK\": \"219. Ph
//òng khám Nội tiết\",\n\"NGAYSINH\": \"03/11/2015\",\n\"NAMSINH\": \"2015\",\n\"DANTOC\": \"Kinh\",\n
//\"QUOCGIA\": \"Việt Nam\",\n\"DIACHI\": \"Phường Minh Khai-Thành Phố Phủ Lý-Hà Nam\",\n\"TENNGHENGHIEP
//\": \"Trẻ em\",\n\"NGAYRAVIEN\": \"23/05/2017 00:00:00\",\n\"DENKHAMLUC\": \"23/05/2017 00:34:11 -> 23
///05/2017 00:00:00\",\n\"NOILAMVIEC\": \"\",\n\"GIOITINH\": \"Nữ\",\n\"DOITUONG\": \"BHYT\",\n\"KCBBD
//\": \"35001\",\n\"SOTHEBHYT\": \"TE1350100000178\",\n\"BHYTDEN\": \"02/11/2021\",\n\"BAOTINCHO\": \"1-
//\",\n\"YEUCAUKHAM\": \"7-Khám Nội\",\n\"PHONGKHAM\": \"219. Phòng khám Nội tiết\",\n\"XUTRI\": \"Cấp
// toa cho về\",\n\"TUYEN\": \"Đúng tuyến\",\n\"NGAYTN\": \"201705230034\",\n\"LOAIBENHAN\": \"Khám bệnh
//\",\n\"CDTD\": \"\",\n\"ANHBENHNHAN\": \"\",\n\"CDC\": \"Bệnh tả do Vibrio cholerae 01, typ sinh học
// eltor\",\n\"CDP\": \"A19.8-Lao kê khác\",\n\"MAKHAMBENH\": \"KB000000131\",\n\"MATIEPNHAN\": \"TN000000116
//\",\n\"MABENHAN\": \"BA00000096\",\n\"DUYETKETOAN\": \"0\",\n\"DUYETBH\": \"0\",\n\"BTNTHUOC\": \"1\"
//,\n\"TRANGTHAI_STT\": \"4\",\n\"SLXN\": \"0\",\n\"SLCDHA\": \"0\",\n\"SLCHUYENKHOA\": \"0\",\n\"SLTHUOC
//\": \"2\",\n\"SLVATTU\": \"0\",\n\"SLVANCHUYEN\": \"0\",\n\"CONGKHAM\": \"1\",\n\"DICHVUID\": \"400035
//\",\n\"PHONGID\": \"4126\"}]",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}
        }
         
        #endregion

        #region tab Bệnh án
        public static DataTable getBenhAn(string KHAMBENHID)
        {
            // {"func":"ajaxCALL_SP_O","params":["NT.006","3099",0],"uuid":"6084bd7f-c492-4365-ad58-fa099688ea03"}
            DataTable dt = new DataTable(); 
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "NT.006", KHAMBENHID }, new int[] { 0 }
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
//   {
//"result": "[{\n\"LYDOVAOVIEN\": \"\",\n\"QUATRINHBENHLY\": \"\",\n\"TIENSUBENH_BANTHAN\": \"\",\n\"TIENSUBENH_GIADINH\": \"\",\n\"KHAMBENH_TOANTHAN\": \"\",\n\"KHAMBENH_BOPHAN\": \"\",\n\"KHAMBENH_MACH\": \"\",\n\"KHAMBENH_NHIETDO\": \"\",\n\"KHAMBENH_HUYETAP_HIGH\": \"\",\n\"KHAMBENH_HUYETAP_LOW\": \"\",\n\"KHAMBENH_NHIPTHO\": \"\",\n\"KHAMBENH_CANNANG\": \"0\",\n\"KHAMBENH_CHIEUCAO\": \"0\",\n\"TOMTATKQCANLAMSANG\": \"\",\n\"CHANDOANBANDAU\": \"\",\n\"HUONGXULY\": \"\",\n\"BENHCHINH\": \"Viêm phế quản cấp\",\n\"MABENHCHINH\": \"J20\",\n\"BENHKEMTHEO\": \"\",\n\"MABENHKEMTHEO\": \"\",\n\"BENHKEMTHEO1\": \"\",\n\"MABENHKEMTHEO1\": \"\",\n\"BENHKEMTHEO2\": \"\",\n\"MABENHKEMTHEO2\": \"\",\n\"KHAC\": null,\n\"BMI\": \"    .00\"}]",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}
        }
        #endregion

        #region tab Xét nghiệm 
        public static ResponsList getDsXetNghiem(int page, int number, string KHAMBENHID, string BENHNHANID, string lnmbp, string hosobenhanid)
        {
            //postData:{"func":"ajaxExecuteQueryPaging","uuid":"b12e9c3a-a108-46ca-ae80-5c48d00284be",
            //"params":["NT.024.DSPHIEU"],"options":[{"name":"[0]","value":"3099"},{"name":"[1]","value":"3301"},{"name":"[2]","value":1},{"name":"[3]","value":""}]}
            //rows:10
            //page:1 
            ResponsList ds = new ResponsList();
            try
            {
                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                    + "\",\"params\":[\"NT.024.DSPHIEU\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + KHAMBENHID
                    + "\"},{\"name\":\"[1]\",\"value\":\"" + BENHNHANID
                    + "\"},{\"name\":\"[2]\",\"value\":" + lnmbp + "},{\"name\":\"[3]\",\"value\":\"" + hosobenhanid + "\"}]}";
                string data = "page=" + page +
                   "&postData=" + request +
                   "&rows=" + number;

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
            //{total: 2, page: 1, records: 12, rows:
            //[{RN: "1", DOITUONGBENHNHANID: "1", MABENHNHAN: "BN00000362", TENBENHNHAN: "PHÙNG VĂN HƯƠNG",…},…]
        }
        public static ResponsList getKQXetNghiem(int page, int number, string MAUBENHPHAMID)
        {
//postData:{"func":"ajaxExecuteQueryPaging","uuid":"b12e9c3a-a108-46ca-ae80-5c48d00284be"
            //,"params":["NT.024.2"],"options":[{"name":"[0]","value":"12277"}]}
//rows:10
//page:1
//sidx:TENCHIDINH asc, 
//sord:asc
            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NT.024.2" }
                    , new String[] { "[0]" }
                    , new String[] { MAUBENHPHAMID });
                string data = "page=" + page +
                   "&postData=" + request +
                   "&rows=" + number +
                   "&sidx=TENCHIDINH asc&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
            //{"total": 1,"page": 1,"records": 1,"rows" : [{
            //"RN": "1",
            //"MAUBENHPHAMID": "12278",
            //"DICHVUKHAMBENHID": "11385",
            //"KETQUACLSID": "17763",
            //"MADICHVU": "25.0059.1749",
            //"TENDICHVU": "nhuộm Giemsa trên mảnh cắt mô phát hiện HP", 
            //"GIATRI_KETQUA": "Đang xử lý",
            //"GIATRINHONHAT": "GPB_2016",
            //"GIATRILONNHAT": "",
            //"DONVI": "Lần",
            //"GHICHU1": "",
            //"GHICHU2": "",
            //"GHICHU": "",
            //"DICHVUTHUCHIENID": "406667",
            //"KETQUACLS": "",
            //"TRISOBINHTHUONG": "GPB_2016 Lần",
            //"TRANGTHAIKETQUA": "Chờ xử lý",
            //"THOIGIANTRAKETQUA": "",
            //"GHICHUCD": "",
            //"THUTUIN": null,
            //"THUTUINMAUBYT": "1",
            //"TENCHIDINH": "Nhuộm Giemsa trên mảnh cắt mô phát hiện HP"}] }
        }
        public static ResponsList getDVChiDinh(int page, int number, string MAUBENHPHAMID)
        {
//postData:{"func":"ajaxExecuteQueryPaging","uuid":"b12e9c3a-a108-46ca-ae80-5c48d00284be"
            // ,"params":["NT024.CLS.CHIDINH"],"options":[{"name":"[0]","value":"12278"}]}
//rows:10
//page:1
//sord:asc
            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NT024.CLS.CHIDINH" }
                    , new String[] { "[0]" }
                    , new String[] { MAUBENHPHAMID });
                string data = "page=" + page +
                   "&postData=" + request +
                   "&rows="+ number +
                   "&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
// {"total": 1,"page": 1,"records": 1,"rows" : [{
//"RN": "1",
//"DICHVUKHAMBENHID": "11385",
//"DICHVUID": "402657",
//"MADICHVU": "25.0059.1749",
//"TENDICHVU": "Nhuộm Giemsa trên mảnh cắt mô phát hiện HP",
//"GIADICHVU": "262000",
//"GIANHANDAN": "0",
//"GIABHYT": "262000",
//"TRANGTHAIDICHVU": "0",
//"TENTRANGTHAI": "",
//"TRANGTHAI_ID": null,
//"MAUBENHPHAMID": "12278"}] }
        }

        #endregion

        #region tab CDHA
        //???
        public static DataTable getCLS02C001(string a)
        {
            // {"func":"ajaxCALL_SP_S","params":["CLS02C001.RISC","['RIS_CONNECTION_TYPE','RIS_USERNAME','RIS_SECRET_KEY','RIS_SERVICE_DOMAIN_NAME','RIS_CANCEL_REQUEST','RIS_GET_DICOM_VIEWER','RIS_DELETE_REQUEST']"],"uuid":"de5e381c-c0c6-4f39-b3e3-842f9b399593"}

            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "CLS02C001.RISC", "['RIS_CONNECTION_TYPE','RIS_USERNAME','RIS_SECRET_KEY','RIS_SERVICE_DOMAIN_NAME','RIS_CANCEL_REQUEST','RIS_GET_DICOM_VIEWER','RIS_DELETE_REQUEST']" }
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
//{"result": "{\r\n  \"RIS_CONNECTION_TYPE\" : \"0\"\r\n,\r\n  \"RIS_USERNAME\" : \"\"\r\n,\r\n  \"RIS_SECRET_KEY\" : \"vnptris\"\r\n,\r\n  \"RIS_SERVICE_DOMAIN_NAME\" : \"http://ris.ddns.net:8082\"\r\n,\r\n  \"RIS_CANCEL_REQUEST\" : \"/api/public/request/cancel\"\r\n,\r\n  \"RIS_GET_DICOM_VIEWER\" : \"/api/public/dicomViewer\"\r\n,\r\n  \"RIS_DELETE_REQUEST\" : \"api/public/request/delete\"\r\n\r\n}",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}
        }

        public static string getCheck(string a)
        {
            //{"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","CDHA_SHOW_SUA_PTH"],"uuid":"9ce2caf4-e99c-4a04-af41-b42c29a3646a"}
            string ret = "";
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_S", new String[] { "COM.CAUHINH", "CDHA_SHOW_SUA_PTH" }
                    );
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
            //{"result": "1",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
        }
        #endregion

        #region tab phẫu thuật thủ thuật
        public static ResponsList getKqPhauThuatThuThuat(int page, int number, string MAUBENHPHAMID)
        {
//postData:{"func":"ajaxExecuteQueryPaging","uuid":"9ce2caf4-e99c-4a04-af41-b42c29a3646a","params":["NT.024.3"],"options":[{"name":"[0]","value":"11748"}]}
//_search:false
//nd:1502941237174
//rows:10
//page:1
//sidx:
//sord:asc
            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NT.024.3" }
                    , new String[] { "[0]" }
                    , new String[] { MAUBENHPHAMID });
                string data = "page=" + page +
                   "&postData=" + request +
                   "&rows=" + number +
                    "&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
//{"total": 1,"page": 1,"records": 1,"rows" : [{
//"RN": "1",
//"DICHVUKHAMBENHID": "10778",
//"MAUBENHPHAMID": "11748",
//"KHAMBENHID": "2974",
//"TENDICHVU": "Bít thông liên nhĩ [dưới DSA]\n(Chưa gồm vật tư chuyên dụng để can thiệp: bóng, stent, vòng xoắn kim loại,...)",
//"SOLUONG": "1",
//"LOAIPTTT": "",
//"TRANGTHAIKETQUA": null,
//"TENTRANGTHAI": "Đang thực hiện",
//"NGAYPHAUTHUATTHUTHUAT": null,
//"NGUOITHUCHIEN": "",
//"TYLENGAYGIUONG": "100%"}] }
        }
        
        #endregion

        #region tab Thuốc
        public static ResponsList getChiTietPhieuThuoc(int page, int number, string MAUBENHPHAMID)
        {
//postData:{"func":"ajaxExecuteQueryPaging","uuid":"9ce2caf4-e99c-4a04-af41-b42c29a3646a","params":["NT.034.1"],"options":[{"name":"[0]","value":"11626"}]}
//_search:false
//nd:1502954039275
//rows:10
//page:1
//sidx:
//sord:asc
            ResponsList ds = new ResponsList();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NT.034.1" }
                    , new String[] { "[0]" }
                    , new String[] { MAUBENHPHAMID });
                string data = "page=" + page +
                   "&postData=" + request +
                   "&rows=" + number + 
                   "&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
            //{"total": 1,"page": 1,"records": 1,"rows" : [{
            //"RN": "1",
            //"MAUBENHPHAMID": "11626",
            //"DICHVUKHAMBENHID": "0",
            //"MADICHVU": "HNM_DK_01200",
            //"TENDICHVU": "Flumetholon 0,1",
            //"SOLUONGPHUTROI": "0",
            //"SOLUONG": "3",
            //"DVT": "Lọ 5ml",
            //"NGAYDUNG": "1",
            //"DUONGDUNG": "Nhỏ mắt",
            //"HUONGDANSUDUNG": "1 ngày, Ngày 3 Lọ 5ml chia 3",
            //"LOAIDOITUONG": "Bảo hiểm"}] }
        }
        
        #endregion
       
        #region tab Viện phí
        //public static DataTable call_ajaxCALL_SP_O(string paramsName, string ID, int x)
        //{
        //    // {"func":"ajaxCALL_SP_O","params":["VPI01T004.01","8557",0],"uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60"}
        //    //{"result": "[{\n\"DOITUONGBENHNHANID\": \"1\",\n\"BHYT_BENHNHANID\": \"7375\",\n\"HOSOBENHANID\": \"8789\",\n\"TRANGTHAITIEPNHAN\": \"0\",\n\"LOAITIEPNHANID\": \"1\",\n\"DOITUONGBN\": \"BHYT\",\n\"BHYT_GIOIHANBHYTTRAHOANTOAN\": \"195000\",\n\"MABN\": \"BN00005547\",\n\"TENBN\": \"TEST PK SẢN\",\n\"NAMSINH\": \"1993\",\n\"TUOI\": \"24\",\n\"SOTHE\": \"DN4350101010907\",\n\"MUCHUONG\": \"80\",\n\"GIATRITHETU\": \"01/01/2017\",\n\"GIATRITHEDEN\": \"31/12/2017\",\n\"THAMGIABHYTDU5NAM\": \"0\",\n\"TRADU6THANGLUONGCOBAN\": \"0\"}]",
        //    //"out_var": "[]",
        //    //"error_code": 0,
        //    //"error_msg": ""}

        //    // {"func":"ajaxCALL_SP_O","params":["VPI01T001.05","8557",0],"uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60"}
        //    //{"result": "[{\n\"TONGTIENDV\": \"52175660\",\n\"TONGTIENDV_BH\": \"51475660\",\n\"BHYT_THANHTOAN\": \"41180528\",\n\"BNTRA\": \"10995132\"
        //    // ,\n\"MIENGIAMDV\": \"0\",\n\"DAMIENGIAM\": \"0\",\n\"BHYT_DANOP\": \"0\",\n\"DANOP\": \"0\",\n\"TRAN_BHYT\": \"195000\",\n\"FREE\": \"0\"
        //    // ,\n\"TRAITUYEN\": \"0\",\n\"TYLE_BHYT_HIENTAI\": \"80\",\n\"TYLE_BHYT\": \"100\",\n\"TYLE_THE\": \"80\"}]",
        //    //"out_var": "[]",
        //    //"error_code": 0,
        //    //"error_msg": ""}

        //    //{"func":"ajaxCALL_SP_O","params":["VPI01T001.06","8557",0],"uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60"}
        //    //{"result": "[{\n\"TONGTIENDV\": \"52175660\",\n\"VIENPHI\": \"10995132\",\n\"DANOP\": \"0\",\n\"TAMUNG\": \"0\",\n\"HOANUNG\": \"0\"
        //    // ,\n\"MIENGIAM\": \"0\",\n\"MIENGIAMDV\": \"0\",\n\"CHENHLECH\": \"10995132\",\n\"CHECK_TAMUNG\": \"0\",\n\"TIEN_PHAINOP\": \"-10995132\"}]",
        //    //"out_var": "[]",
        //    //"error_code": 0,
        //    //"error_msg": ""}

        //    // {"func":"ajaxCALL_SP_O","params":["LAY.CAUHINH","",0],"uuid":"780ac503-77a6-4ea8-bdbd-b2a0fb933b54"}
        //    //{
        //    //"result": "[{\n\"CH_KETTHUCKHAM\": \"1\",\n\"NGAYPK\": \"1\",\n\"KETHUCKHAM_BN\": \"-1\",\n\"XOA_BN\": \"-1\",\n\"CONFIGBACSI\": \"0\",\n\"CHECK_24H\": \"1\",\n\"KEDONTHUOC_CHITIET_NTU\": \"1\",\n\"DK_MOBENHAN\": \"1\",\n\"HIDE_BTN_MO_BA\": \"1\",\n\"MAPHONGTRUC\": \"0\",\n\"CHUPANH\": \"0\",\n\"ANBANGT\": \"0\",\n\"HIDEDONTHUOCKT\": \"0\",\n\"CHECKTIEN\": \"0\"}]",
        //    //"out_var": "[]",
        //    //"error_code": 0,
        //    //"error_msg": ""}
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { paramsName, ID }, new int[] { x }
        //            );
        //        string resp = RequestHTTP.sendRequest(request);

        //        ResponsObj resultSet = new ResponsObj();

        //        resultSet = JsonConvert.DeserializeObject<ResponsObj>(resp, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        //        dt = (DataTable)JsonConvert.DeserializeObject(resultSet.result, (typeof(DataTable)));
        //    }
        //    catch (Exception ex)
        //    {log.Fatal(ex.ToString());
        //    }
        //    return dt;
        //}

        public static ResponsList getDsVienPhi(int page, int number, string TIEPNHANID, string nhombhyt, string input, string khoaid)
        {
            //postData:{"func":"ajaxExecuteQueryPaging","uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60"
            // ,"params":["VPI01T004.04"],"options":[{"name":"[0]","value":"8557"},{"name":"[1]","value":-1},{"name":"[2]","value":""},{"name":"[3]","value":-1}]}
            //rows:200
            //page:1
            //sord:asc

            ResponsList ds = new ResponsList();
            try
            {
                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                    + "\",\"params\":[\"VPI01T004.04\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + TIEPNHANID
                    + "\"},{\"name\":\"[1]\",\"value\":" + nhombhyt
                    + "},{\"name\":\"[2]\",\"value\":\"" + input + "\"},{\"name\":\"[3]\",\"value\":" + khoaid + "}]}";
                string data = "page=" + page +
                   "&postData=" + request +
                   "&rows=" + number;

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
            //{"total": 1,"page": 1,"records": 116,"rows" : [{
//"RN": "1",
//"DOITUONGBENHNHANID": "1",
//"TIEN_DICHVU": "39000",
//"DICHVUKHAMBENHID": "143982",
//"NGAYDICHVU": "04/08/2017 16:22:17",
//"NGAYCD": "04/08/2017",
//"NGAYMAUBENHPHAM": "04/08/2017",
//"SOPHIEU": "170804000057",
//"TENDICHVU": "Khám Phụ sản",
//"SOLUONG": "1",
//"THANHTIEN": "39000",
//"TIEN_BHYT_TRA": "31200",
//"TIEN_MIENGIAM": "0",
//"TIEN_DANOP": "0",
//"DONVI": "Lần",
//"TYLE_DV": "1",
//"TIENDICHVU": "39000",
//"TRANGTHAIDICHVU": "0",
//"KHOA": "Khoa Khám bệnh",
//"PHONG": "103. Phòng khám sản",
//"LOAIDOITUONG": "1",
//"DICHVUID_ORG": "0",
//"DICHVUID_CHA": "0",
//"LOAITTCU": "BHYT",
//"LOAITTMOI": "BHYT",
//"NGUOICHIDINH": "Vũ Đức Dũng",
//"LOAINHOMMAUBENHPHAM": "3",
//"NHOM_MABHYT_ID": "1",
//"TENNHOM": "Khám bệnh",
//"GHICHU": "Khám Bệnh",
//"GIATRI": "01KB",
//"STT_NGT": "1",
//"STT_NTU": "",
//"MANHOM_BHYT": "13",
//"NHOMID_CHA": "1",
//"TYLE_BHYT_TRA": "80",
//"THUCTHU": "7800"},{
        }
        public static ResponsList getDsVienPhi2(int page, string KHAMBENHID, int LNMBP_XetNghiem, string hosobenhanid)
        {
//postData:{"func":"ajaxExecuteQueryPaging","uuid":"7c144ac1-08c9-413f-86ab-90e9244eab60","params":["VPI01T001.02"]
            // ,"options":[{"name":"[0]","value":"8557"},{"name":"[1]","value":-1},{"name":"[2]","value":""}]}
//rows:20
//page:1
//sidx:LOAI_DOITUONG asc, NHOM_MABHYT asc, 
//sord:asc
            ResponsList ds = new ResponsList();
            try
            {
                string request = "{\"func\":\"ajaxExecuteQueryPaging\",\"code\":\"thu@nnc\",\"uuid\":\"" + Const.local_user.UUID
                    + "\",\"params\":[\"VPI01T001.02\"],\"options\":[{\"name\":\"[0]\",\"value\":\"" + KHAMBENHID
                    + "\"},{\"name\":\"[1]\",\"value\":" + LNMBP_XetNghiem
                    + "},{\"name\":\"[2]\",\"value\":\"" + hosobenhanid + "\"}]}";
                string data = "page=" + page +
                   "&postData=" + request +
                   "&rows=20&sidx=LOAI_DOITUONG asc, NHOM_MABHYT asc&sord=asc";

                string ret = RequestHTTP.getRequest(data);
                ds = JsonConvert.DeserializeObject<ResponsList>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return ds;
            //{"total": 4,"page": 1,"records": 74,"rows" : [{
            //"RN": "1",
            //"DICHVUID": "402844",
            //"LOAIDOITUONG": "1",
            //"LOAI_DOITUONG": "BHYT",
            //"DOITUONG": "BHYT",
            //"NHOM_MABHYT": "Chẩn đoán hình ảnh",
            //"TENDICHVU": "Siêu âm các tuyến nước bọt",
            //"TIENDICHVU": "49000",
            //"DVT": "Lần",
            //"SOLUONG": "1",
            //"DATHUTIEN": "0"},{
            //"RN": "2",
        }
        #endregion


        #region Phiếu khám bệnh --> Thông tin khám bệnh

        #endregion

    }
}
