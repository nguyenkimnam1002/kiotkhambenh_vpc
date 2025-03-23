using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.CommonForm.Class;
using VNPT.HIS.NgoaiTru.Class;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K005_PhieuKhamBenh : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K005_PhieuKhamBenh()
        {
            InitializeComponent();
        }

        protected DataRowView BenhNhan = null, BenhNhan_ChiTiet = null;
        protected DataTable dtThongTinKhamBenh = new DataTable();


        public void loadData(DataRowView _drvBN, DataRowView _drvBN_ChiTiet)
        {
            BenhNhan = _drvBN;
            BenhNhan_ChiTiet = _drvBN_ChiTiet;
            //if (KHAMBENHID == _drvBN["KHAMBENHID"].ToString()) return;

            //_drvBN:
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
            //ucGrid_DsBN.setColumn("KHAMBENHID", -1);


            //_drvBN_ChiTiet:
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
        }



        protected string KhambenhID;
        protected string PhongID;
        protected string NgheNghiep;
        protected string NamSinh;
        protected string TenBenhNhan;

        protected string tudonginbangke;
        protected string tatpopupravien;
        protected DateTime dtimeNGAYTIEPNHAN;

        private void formPhieuKhamBenh_Shown(object sender, EventArgs e)
        {
        }
        private void NGT02K005_PhieuKhamBenh_Load(object sender, EventArgs e)
        {
            ucThongTinHanhChinh1.set_all_control_readonly(true);
            initThongTinKhamBenh();

            //DataTable dt = Common.RequestHTTP.call_ajaxCALL_SP_O("COM.BENHNHAN", "2762", 0);
            load_BN(BenhNhan["MABENHNHAN"].ToString(), null);

        }

        private void load_BN(object sender, EventArgs e)
        {
            string maBN = (string)sender;
            ucThongTinHanhChinh1.load_benhnhan_theoMa(maBN);

            KhambenhID = BenhNhan["KHAMBENHID"].ToString();
            PhongID = Const.local_phongId.ToString();
            NgheNghiep = ucThongTinHanhChinh1.ucNghenghiep.Text;
            NamSinh = ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["NAMSINH"].ToString();
            TenBenhNhan = ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["TENBENHNHAN"].ToString();

            load_ThongTin_KhamBenh(KhambenhID, PhongID);
        }

        private void initThongTinKhamBenh()
        {
            LCI_LichSuKham_DieuTri.Text = "Lịch sử khám \r\n- điều trị";
            try
            {
                DataTable dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_NoiDKKCB);

                //cbo Doituong
                dt = Common.RequestHTTP.get_ajaxExecuteQuery("NT.007", new string[] { "[S0]", "[S1]", "[S2]", "[S3]" } 
                    , new String[] { Const.local_user.HOSPITAL_ID, Const.local_user.USER_ID, Const.local_user.USER_GROUP_ID, Const.local_user.PROVINCE_ID });
                ucDoituong.setEvent_Enter(ucDoituong_KeyEnter);
                ucDoituong.lookUpEdit.Properties.ShowHeader = false;
                ucDoituong.setData(dt, 0, 1);
                ucDoituong.setColumn("col1", -1, "", 0);
                //ucDoituong.CaptionValidate = false;

                ////yêu cầu khám
                //dt_yeucaukham = Common.RequestHTTP.getYeuCauKham(true, Const.tbl_YeuCauKham, ucDoituong.SelectValue);
                //ucYeuCauKham.setData(dt_yeucaukham, "col1", "col2");
                //ucYeuCauKham.setEvent_Enter(ucYeuCauKham_KeyEnter);
                //ucYeuCauKham.setEvent(ucYeuCauKham_SelectedIndexChanged);
                ////ucYeuCauKham.CaptionValidate = false;


                // cboTuyen
                dt = Common.RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Tuyen);
                ucTuyen.lookUpEdit.Properties.ShowHeader = false;
                ucTuyen.setData(dt, 0, 1);
                ucTuyen.setEvent_Enter(ucTuyen_KeyEnter);

                //Nơi đk KCB và BV chuyển đến (cùng dl)
                dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_NoiDKKCB);
                ucDKKCB.setData(dt, "BENHVIENKCBBD", "TENBENHVIEN");
                ucDKKCB.setEvent_Enter(ucDKKCB_KeyEnter);
                ucDKKCB.setColumn("RN", -1, "", 0);
                ucDKKCB.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 35);
                ucDKKCB.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
                ucDKKCB.setColumn("DIACHI", 2, "Địa chỉ", 0);

                //Danh sách bệnh chuẩn đoán
                dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
                ucCDSoBo.setData(dt, "ICD10CODE", "ICD10NAME");
                ucCDSoBo.setEvent_Enter(ucCDSoBo_KeyEnter);
                ucCDSoBo.setColumn("RN", -1, "", 0);
                ucCDSoBo.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucCDSoBo.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

                ucBenhChinh.setData(dt, "ICD10CODE", "ICD10NAME");
                ucBenhChinh.setEvent_Enter(ucBenhChinh_KeyEnter);
                ucBenhChinh.setColumn("RN", -1, "", 0);
                ucBenhChinh.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucBenhChinh.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
                ucBenhChinh.setEvent_Check(ucBenhChinh_Check);

                ucBenhPhu.setData(dt, "ICD10CODE", "ICD10NAME");
                ucBenhPhu.setEvent_Enter(ucBenhPhu_KeyEnter);
                ucBenhPhu.setColumn("RN", -1, "", 0);
                ucBenhPhu.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucBenhPhu.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
                ucBenhPhu.setEvent_Check(ucBenhPhu_Check);
                ucBenhPhu.btnReset.Visible = true;
                ucBenhPhu.btnEdit.Visible = true;
                ucBenhPhu.btnReset.Text = "Reset BP";
                ucBenhPhu.btnEdit.Text = "Sửa BP";

                //Xử trí               
                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_XuTriKB);
                ucXuTri.setEvent_Enter(ucXuTri_KeyEnter);
                ucXuTri.lookUpEdit.Properties.ShowHeader = false;
                ucXuTri.setData(dt, 0, 1);
                ucXuTri.CaptionValidate = true;

                // Chuyển tới khoa                
                ucToiKhoa.setEvent_Enter(ucToiKhoa_KeyEnter);

            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        public void load_ThongTin_KhamBenh(string KHAMBENHID, string PHONGID)
        {
            try
            {
                dtThongTinKhamBenh = ServicePhieuKhamBenh.getThongTinKhamBenh(KHAMBENHID, PHONGID);
                if (dtThongTinKhamBenh.Rows.Count > 0)
                {
                    //{ "result": "[{"KHAMBENHID": "2378","TIEPNHANID": "2724","HOSOBENHANID": "3751","PHONGKHAMDANGKYID": "1762","MAHOSOBENHAN": "BA00002413","PHONGID": "208",
                    //    "MABENHNHAN": "BN00001689","ORG_NAME": "PK Sản P.307","NGAYTIEPNHAN": "09:03 21/04/2017","NGAYTN": "21/04/2017 09:03","DOITUONGBENHNHANID": "2","MA_BHYT": "",
                    //    "BHYT_BD": "","BHYT_KT": "","MA_KCBBD": "","TENBENHVIEN": "","DICHVUID": "1001","MACHANDOANBANDAU": "A00.9","CHANDOANBANDAU": "Bệnh tả, không xác định",
                    //    "MACHANDOANRAVIEN": "C11.1","CHANDOANRAVIEN": "U ác của vách sau của hầu-mũi","GHICHU_BENHCHINH": "","MACHANDOANRAVIEN_KEMTHEO": "",
                    //    "CHANDOANRAVIEN_KEMTHEO": "A00-Bệnh tả;A01.1-Bệnh phó thương hàn A","MACHANDOANRAVIEN_KEMTHEO1": "","CHANDOANRAVIEN_KEMTHEO1": "","MACHANDOANRAVIEN_KEMTHEO2": "",
                    //    "CHANDOANRAVIEN_KEMTHEO2": "","MACHANDOANRAVIEN_KHAC": "","CHANDOANRAVIEN_KHAC": "A00-Bệnh tả;A01.1-Bệnh phó thương hàn A","XUTRIKHAMBENHID": "2",
                    //    "YEUCAUKHAM": "43-Phá thai","MAXUTRIKHAMBENHID": "2","THOIGIANRAVIEN": "18/09/2017 11:19","BHYT_LOAIID": null,"BENHNHANID": "2762","DIACHI":
                    //    "Xã Nghĩa Lộ-Huyện Cát Hải-TP Hải Phòng","SOTHUTU": "0003","KHOA": "27","TAT_POPUP_TTRAVIEN": "1","TUDONGINBANGKE": "0","XTMACDINH": "-1","SUB_DTBNID": null}]",
                    tudonginbangke = dtThongTinKhamBenh.Rows[0]["TUDONGINBANGKE"].ToString();
                    tatpopupravien = dtThongTinKhamBenh.Rows[0]["TAT_POPUP_TTRAVIEN"].ToString();
                    initUcXuTri(
                        (dtThongTinKhamBenh.Rows[0]["XUTRIKHAMBENHID"].ToString() == "" || dtThongTinKhamBenh.Rows[0]["XUTRIKHAMBENHID"].ToString() == "0") ?
                        dtThongTinKhamBenh.Rows[0]["XTMACDINH"].ToString() :
                        dtThongTinKhamBenh.Rows[0]["XUTRIKHAMBENHID"].ToString()
                    );

                    try
                    {
                        if (dtThongTinKhamBenh.Rows[0]["NGAYTIEPNHAN"] != null && dtThongTinKhamBenh.Rows[0]["NGAYTIEPNHAN"].ToString() != "") // 09:03 21/04/2017
                        {
                            dtimeNGAYTIEPNHAN = DateTime.ParseExact(dtThongTinKhamBenh.Rows[0]["NGAYTIEPNHAN"].ToString(), "HH:mm dd\\/MM\\/yyyy", CultureInfo.InvariantCulture);
                            dtimeDenLuc.DateTime = dtimeNGAYTIEPNHAN;
                        }
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                    ucDoituong.SelectValue = dtThongTinKhamBenh.Rows[0]["DOITUONGBENHNHANID"].ToString();
                    txtSoThe.Text = dtThongTinKhamBenh.Rows[0]["MA_BHYT"].ToString();

                    txtYeuCauKhamID.Text = dtThongTinKhamBenh.Rows[0]["DICHVUID"].ToString();
                    txtYeuCauKhamName.Text = dtThongTinKhamBenh.Rows[0]["DICHVUID"].ToString();

                    if (dtThongTinKhamBenh.Rows[0]["BHYT_LOAIID"] == null) ucTuyen.SelectIndex = -1;
                    else ucTuyen.SelectValue = dtThongTinKhamBenh.Rows[0]["BHYT_LOAIID"].ToString();

                    try
                    {
                        //BHYT_BD": "22/10/2016", 
                        if (dtThongTinKhamBenh.Rows[0]["BHYT_BD"].ToString() != "")
                        {
                            DateTime dtime = Func.ParseDate(dtThongTinKhamBenh.Rows[0]["BHYT_BD"].ToString());
                            dtimeTuNgay.DateTime = dtime;
                        }
                        //BHYT_KT": "21/10/2022,
                        if (dtThongTinKhamBenh.Rows[0]["BHYT_KT"].ToString() != "")
                        {
                            DateTime dtime = Func.ParseDate(dtThongTinKhamBenh.Rows[0]["BHYT_KT"].ToString());
                            dtimeDenNgay.DateTime = dtime;
                        }
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                    ucDKKCB.SelectedValue = dtThongTinKhamBenh.Rows[0]["MA_KCBBD"].ToString();

                    ucCDSoBo.SelectedValue = dtThongTinKhamBenh.Rows[0]["MACHANDOANBANDAU"].ToString();
                    ucBenhChinh.SelectedValue = dtThongTinKhamBenh.Rows[0]["MACHANDOANRAVIEN"].ToString();
                    ucBenhPhu.SelectedText = dtThongTinKhamBenh.Rows[0]["CHANDOANRAVIEN_KEMTHEO"].ToString();

                    DataTable dt_LichSu = RequestHTTP.getLichSuKhamBenh(dtThongTinKhamBenh.Rows[0]["BENHNHANID"].ToString());
                    for (int i = 0; i < dt_LichSu.Rows.Count; i++)
                    {
                        txtLichSuKham.Text += dt_LichSu.Rows[i][1].ToString() + ";\r\n";
                    }

                    try
                    {
                        if (dtThongTinKhamBenh.Rows[0]["THOIGIANRAVIEN"].ToString() != "")// 18/09/2017 14:59
                        {
                            DateTime dtime = Func.ParseDatetime2(dtThongTinKhamBenh.Rows[0]["THOIGIANRAVIEN"].ToString());
                            dtimeRaVien.DateTime = dtime;
                        }
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        #region SỰ KIỆN PHẦN TT KHÁM BỆNH
        private void initUcXuTri(string XuTriID)
        {
            this.XuTriID = XuTriID;
            if (this.XuTriID != "0")
            {
                if (this.XuTriID == "2" || this.XuTriID == "6")
                {
                    ucXuTri.setEvent(ucXuTri_SelectedIndexChanged);
                    ucXuTri.SelectValue = this.XuTriID;
                }
                else
                {
                    ucXuTri.SelectValue = this.XuTriID;
                    ucXuTri.setEvent(ucXuTri_SelectedIndexChanged);
                }
            }

            if (XuTriID == "1" || XuTriID == "3" || XuTriID == "9")
            { // Cấp toa cho về, hẹn, khác
                // _fileReport = "NTU009_GIAYRAVIEN_01BV01_QD4069_A5";
                btnInPhieu.Enabled = true;
            }
            else if (XuTriID == "4" || XuTriID == "5")
            { // hẹn khám tiếp, khám mới
                //_fileReport = "NGT014_GIAYHENKHAMLAI_TT402015_A4";
                btnInPhieu.Enabled = true;
            }
            else if (XuTriID == "7")
            { // chuyển viện
                //_fileReport = "NGT003_GIAYCHUYENTUYEN_TT14_A4";
                btnInPhieu.Enabled = true;
            }
            else if (XuTriID == "8")
            { // tử vong				
                //_fileReport = "";
                btnInPhieu.Enabled = true;
            }
        }
        private void ucXuTri_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            btnLuuVaDong.Enabled = true;
            try
            {
                DataRowView drv = (DataRowView)sender;

                LCI_ToiKhoa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                XuTriID = drv[0].ToString();
                if (XuTriID == "1" || XuTriID == "3" || XuTriID == "9")
                { // Cấp toa cho về, khác
                    btnInPhieu.Enabled = true;
                    if (tatpopupravien != "1")
                    {
                        btnHienThi.Enabled = true;
                    }
                    else
                    {
                        btnHienThi.Enabled = false;
                    }
                }
                else if (XuTriID == "4" || XuTriID == "5")
                { // hẹn khám tiếp, khám mới
                    btnInPhieu.Enabled = true;
                    btnHienThi.Enabled = true;
                }
                else if (XuTriID == "7")
                { // chuyển viện
                    btnInPhieu.Enabled = true;
                    btnHienThi.Enabled = true;
                }
                else if (XuTriID == "8")
                { // tử vong
                    btnInPhieu.Enabled = true;
                    btnHienThi.Enabled = true;
                }
                else if (XuTriID == "2")
                { // Điều trị ng oại trú
                    LCI_ToiKhoa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    btnInPhieu.Enabled = false;
                    btnHienThi.Enabled = false;

                    //ucToiKhoa.lookUpEdit.Properties.ShowHeader = false;
                    DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_KhoaDTNgT);
                    ucToiKhoa.setData(dt, 0, 1);
                    if (dtThongTinKhamBenh.Rows[0]["KHOA"] != null && dtThongTinKhamBenh.Rows[0]["KHOA"].ToString() != "")
                        ucToiKhoa.SelectValue = dtThongTinKhamBenh.Rows[0]["KHOA"].ToString();
                }
                else if (XuTriID == "6")
                {
                    LCI_ToiKhoa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    btnInPhieu.Enabled = false;
                    btnHienThi.Enabled = false;

                    DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_KhoaDTNT);
                    ucToiKhoa.setData(dt, 0, 1);
                    if (dtThongTinKhamBenh.Rows[0]["KHOA"] != null && dtThongTinKhamBenh.Rows[0]["KHOA"].ToString() != "")
                        ucToiKhoa.SelectValue = dtThongTinKhamBenh.Rows[0]["KHOA"].ToString();
                }

                openPopup();
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        string XuTriID = "";
        private void openPopup()
        {

            //{ "result": "[{"KHAMBENHID": "2378","TIEPNHANID": "2724","HOSOBENHANID": "3751","PHONGKHAMDANGKYID": "1762","MAHOSOBENHAN": "BA00002413","PHONGID": "208",
            //    "MABENHNHAN": "BN00001689","ORG_NAME": "PK Sản P.307","NGAYTIEPNHAN": "09:03 21/04/2017","NGAYTN": "21/04/2017 09:03","DOITUONGBENHNHANID": "2","MA_BHYT": "",
            //    "BHYT_BD": "","BHYT_KT": "","MA_KCBBD": "","TENBENHVIEN": "","DICHVUID": "1001","MACHANDOANBANDAU": "A00.9","CHANDOANBANDAU": "Bệnh tả, không xác định",
            //    "MACHANDOANRAVIEN": "C11.1","CHANDOANRAVIEN": "U ác của vách sau của hầu-mũi","GHICHU_BENHCHINH": "","MACHANDOANRAVIEN_KEMTHEO": "",
            //    "CHANDOANRAVIEN_KEMTHEO": "A00-Bệnh tả;A01.1-Bệnh phó thương hàn A","MACHANDOANRAVIEN_KEMTHEO1": "","CHANDOANRAVIEN_KEMTHEO1": "","MACHANDOANRAVIEN_KEMTHEO2": "",
            //    "CHANDOANRAVIEN_KEMTHEO2": "","MACHANDOANRAVIEN_KHAC": "","CHANDOANRAVIEN_KHAC": "A00-Bệnh tả;A01.1-Bệnh phó thương hàn A","XUTRIKHAMBENHID": "2",
            //    "YEUCAUKHAM": "43-Phá thai","MAXUTRIKHAMBENHID": "2","THOIGIANRAVIEN": "18/09/2017 11:19","BHYT_LOAIID": null,"BENHNHANID": "2762","DIACHI":
            //    "Xã Nghĩa Lộ-Huyện Cát Hải-TP Hải Phòng","SOTHUTU": "0003","KHOA": "27","TAT_POPUP_TTRAVIEN": "1","TUDONGINBANGKE": "0","XTMACDINH": "-1","SUB_DTBNID": null}]",
            //TENCHUYENVIENDEN
            if (XuTriID == "1" || XuTriID == "3" || XuTriID == "9")
            { // Cấp toa cho về, khác
                if (tatpopupravien != "1")
                {
                    //_showDialog("NGT02K007_Thongtin_Ravien", param, 'Thông tin ra viện',1000,380);

                    if (dtPopup_Ravien.Rows.Count == 0) dtPopup_Ravien = RequestHTTP.get_ajaxExecuteQueryO("NGT02K007.RV001", BenhNhan["KHAMBENHID"].ToString());
                    VNPT.HIS.CommonForm.NGT02K007_Thongtin_Ravien frm = new VNPT.HIS.CommonForm.NGT02K007_Thongtin_Ravien(dtPopup_Ravien
                        , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["MABENHNHAN"].ToString()
                        , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["TENBENHNHAN"].ToString()
                        , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["NAMSINH"].ToString()
                        , BenhNhan_ChiTiet["TENNGHENGHIEP"].ToString()
                        , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["DIACHI"].ToString()
                        );

                    frm.setReturnData(ReturnData);
                    frm.StartPosition = FormStartPosition.CenterParent;

                    frm.ShowDialog();

                }
            }
            else if (XuTriID == "4" || XuTriID == "5")
            { // hẹn khám tiếp, khám mới 
                //_showDialog("NGT02K008_Thongtin_Lichkham", param, 'Thông tin lịch hẹn',950,330);

                if (dtPopup_Henkham.Rows.Count == 0) dtPopup_Henkham = RequestHTTP.get_ajaxExecuteQueryO("NGT02K008.RV001", BenhNhan["KHAMBENHID"].ToString());
                
                VNPT.HIS.CommonForm.NGT02K008_Thongtin_Lichkham frm = new VNPT.HIS.CommonForm.NGT02K008_Thongtin_Lichkham(dtPopup_Henkham);
                
                frm.setReturnData(ReturnData);
                frm.StartPosition = FormStartPosition.CenterParent;

                frm.ShowDialog();
            }
            else if (XuTriID == "7")
            { // chuyển viện 
                //_showDialog("NGT02K009_Chuyenvien", param, 'Thông tin chuyển viện',1200,440);

                if (dtPopup_Chuyenvien.Rows.Count == 0) dtPopup_Chuyenvien = RequestHTTP.get_ajaxExecuteQueryO("NGT02K009.RV004", BenhNhan["KHAMBENHID"].ToString());
                VNPT.HIS.CommonForm.NGT02K009_Chuyenvien frm = new VNPT.HIS.CommonForm.NGT02K009_Chuyenvien(dtPopup_Chuyenvien
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["MABENHNHAN"].ToString()
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["TENBENHNHAN"].ToString()
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["NAMSINH"].ToString()
                    , BenhNhan_ChiTiet["TENNGHENGHIEP"].ToString()
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["DIACHI"].ToString()
                    , BenhNhan["KHAMBENHID"].ToString()
                    , dtimeNGAYTIEPNHAN
                    );

                frm.setReturnData(ReturnData);
                frm.StartPosition = FormStartPosition.CenterParent;

                frm.ShowDialog();
            }
            else if (XuTriID == "8")
            { // tử vong
                //_showDialog("NGT02K010_Tuvong", param, 'Thông tin tử vong',1000,330);
                if (dtPopup_Tuvong.Rows.Count == 0) dtPopup_Tuvong = RequestHTTP.get_ajaxExecuteQueryO("NGT02K010.RV001", BenhNhan["KHAMBENHID"].ToString());
                VNPT.HIS.CommonForm.NGT02K010_Tuvong frm = new VNPT.HIS.CommonForm.NGT02K010_Tuvong(dtPopup_Tuvong
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["MABENHNHAN"].ToString()
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["TENBENHNHAN"].ToString()
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["NAMSINH"].ToString()
                    , BenhNhan_ChiTiet["TENNGHENGHIEP"].ToString()
                    , ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["DIACHI"].ToString()
                    );

                frm.setReturnData(ReturnData);
                frm.StartPosition = FormStartPosition.CenterParent;

                frm.ShowDialog();
            }
        }
        private void ReturnData(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)sender;
            if ((XuTriID == "8"))
            {
                dtPopup_Tuvong = dt;
            }
            else if ((XuTriID == "1" || XuTriID == "3" || XuTriID == "9"))
            {
                dtPopup_Ravien = dt;
            }
            else if ((XuTriID == "4" || XuTriID == "5"))
            {
                dtPopup_Henkham = dt;
            }
            else if ((XuTriID == "7"))
            {
                dtPopup_Chuyenvien = dt;
            }
        }
        private void btnHienThi_Click(object sender, EventArgs e)
        {
            openPopup();
        }

        private void ucBenhChinh_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;

                if (ucBenhPhu.SelectList.ContainsKey(drv["ICD10CODE"].ToString()))
                    ucBenhChinh.messageError = "Bệnh chính không được trùng với bệnh phụ.";
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void ucBenhPhu_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (drv["ICD10CODE"].ToString() == ucBenhChinh.SelectedValue)
                {
                    ucBenhPhu.messageError = "Bệnh phụ vừa nhập không được trùng với bệnh chính.";
                }
                else if (ucBenhPhu.SelectedText.IndexOf("; " + drv["ICD10CODE"].ToString() + "-") > -1)
                {
                    ucBenhPhu.messageError = "Bệnh phụ đã được nhập.";
                }
                else // check bệnh phụ trùng với phòng khám khác
                {
                    string check = RequestHTTP.checkTrungBenh(dtThongTinKhamBenh.Rows[0]["KhambenhID"].ToString(), dtThongTinKhamBenh.Rows[0]["PhongID"].ToString(), drv["ICD10CODE"].ToString());
                    if (check == "0") ucBenhPhu.messageError = "Đã tồn tại mã bệnh phụ trùng với phòng khám khác";
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        protected DataTable dtPopup_Tuvong = new DataTable();
        protected DataTable dtPopup_Ravien = new DataTable();
        protected DataTable dtPopup_Henkham = new DataTable();
        protected DataTable dtPopup_Chuyenvien = new DataTable();
        private void btnLuu_Click(object sender, EventArgs e)
        {// test bn: BA00005957	BN00005786	VŨ DUY CHINH
            if (validate() == false) return;
            CAP_NHAT(false);

        }
        private void CAP_NHAT(bool LuuVaDong)
        {
            if (string.IsNullOrWhiteSpace(ucBenhChinh.SelectedValue))
            {
                MessageBox.Show("Hãy nhập bệnh chính", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            if (string.IsNullOrWhiteSpace(ucXuTri.SelectValue))
            {
                MessageBox.Show("Hãy chọn xử trí", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            if (string.IsNullOrWhiteSpace(dtimeRaVien.Text))
            {
                MessageBox.Show("Hãy nhập ngày ra viện", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }

            if ((dtPopup_Tuvong.Rows.Count == 0) && (XuTriID == "8"))
            {
                dtPopup_Tuvong = RequestHTTP.get_ajaxExecuteQueryO("NGT02K010.RV001", BenhNhan["KHAMBENHID"].ToString());
            }
            else if ((dtPopup_Ravien.Rows.Count == 0) && (XuTriID == "1" || XuTriID == "3" || XuTriID == "9"))
            {
                dtPopup_Ravien = RequestHTTP.get_ajaxExecuteQueryO("NGT02K007.RV001", BenhNhan["KHAMBENHID"].ToString());
                //"RAVIENID": "893",
                //"PTHOIGIANRAVIEN": "",
                //"TINHTRANGNGUOIBENHRAVIEN": "",
                //"PHUONGPHAPDIEUTRI": "",
                //"HUONGDIEUTRITIEPTHEO": "",
                //"PTHOIGIANLICHHEN": "",
                //"LOIDANBACSI": ""}]
            }
            else if ((dtPopup_Henkham.Rows.Count == 0) && (XuTriID == "4" || XuTriID == "5"))
            {
                dtPopup_Henkham = RequestHTTP.get_ajaxExecuteQueryO("NGT02K008.RV001", BenhNhan["KHAMBENHID"].ToString());
                //"LICHHENID": "221",
                //"THOIGIANLICHHEN": "26/05/2017  00:05:00",
                //"PHONGID": "534",
                //"PDICHVUID": null,
                //"LOIDANBACSI": "sad",
                //"SOLUUTRU": "ádasd",
                //"LIENLACVOI_BN": "ádad"}]
            }
            else if ((dtPopup_Chuyenvien.Rows.Count == 0) && (XuTriID == "7"))
            {
                dtPopup_Chuyenvien = RequestHTTP.get_ajaxExecuteQueryO("NGT02K009.RV004", BenhNhan["KHAMBENHID"].ToString());
            }


            string json_in = "";

            //            TIEPNHANID:2555
            //HOSOBENHANID:2654
            //BENHNHANID:2582   
            //KHAMBENHID:2200
            json_in += json_item("TIEPNHANID", dtThongTinKhamBenh.Rows[0]["TIEPNHANID"].ToString());
            json_in += json_item("HOSOBENHANID", dtThongTinKhamBenh.Rows[0]["HOSOBENHANID"].ToString());
            json_in += json_item("BENHNHANID", dtThongTinKhamBenh.Rows[0]["BENHNHANID"].ToString());
            json_in += json_item("KHAMBENHID", dtThongTinKhamBenh.Rows[0]["KHAMBENHID"].ToString());
            json_in += json_item("MACHANDOANBANDAU", dtThongTinKhamBenh.Rows[0]["MACHANDOANBANDAU"].ToString());
            json_in += json_item("CHANDOANBANDAU", dtThongTinKhamBenh.Rows[0]["CHANDOANBANDAU"].ToString());

            json_in += json_item("MACHANDOANRAVIEN", ucBenhChinh.SelectedValue);
            json_in += json_item("CHANDOANRAVIEN", ucBenhChinh.SelectedText);
            json_in += json_item("MACHANDOANRAVIEN_KHAC", ucBenhPhu.SelectedValue); // để rỗng vì chuẩn đoán khác là 1 list.
            json_in += json_item("CHANDOANRAVIEN_KHAC", ucBenhPhu.SelectedText);

            json_in += json_item("MAXUTRIKHAMBENHID", XuTriID);//:2
            json_in += json_item("XUTRIKHAMBENHID", XuTriID);//:2
            json_in += json_item("THOIGIANRAVIEN", dtimeRaVien.DateTime.ToString(Const.FORMAT_datetime2));  //  20/09/2017 14:10

            json_in += json_item("DOITUONGBENHNHANID", dtThongTinKhamBenh.Rows[0]["DOITUONGBENHNHANID"].ToString());
            json_in += json_item("MA_BHYT", dtThongTinKhamBenh.Rows[0]["MA_BHYT"].ToString());
            json_in += json_item("BHYT_LOAIID", dtThongTinKhamBenh.Rows[0]["BHYT_LOAIID"].ToString());
            json_in += json_item("MA_KCBBD", dtThongTinKhamBenh.Rows[0]["MA_KCBBD"].ToString());
            json_in += json_item("TENBENHVIEN", dtThongTinKhamBenh.Rows[0]["TENBENHVIEN"].ToString());
            json_in += json_item("PHONGKHAMDANGKYID", dtThongTinKhamBenh.Rows[0]["PHONGKHAMDANGKYID"].ToString());
            json_in += json_item("PHONGKHAMID", dtThongTinKhamBenh.Rows[0]["PhongID"].ToString());

            json_in += json_item("SAVE", "1");

            if (XuTriID == "8" && dtPopup_Tuvong.Rows.Count > 0) // ok
            {
                //8 Tu vong:
                //{"TUVONGID":"555","TUVONGLUC":"21/09/2017 14:07:00","THOIGIANTUVONGID":"1","ISCOKHAMNGHIEM":"0"
                //,"NGUYENNHANTUVONGID":"1","NGUYENNHANTUVONGCHINH":"123","CHANDOANGIAIPHAUTUTHI":"321",
                json_in += json_item("TUVONGID", dtPopup_Tuvong.Rows[0]["TUVONGID"].ToString());
                json_in += json_item("TUVONGLUC", dtPopup_Tuvong.Rows[0]["TUVONGLUC"].ToString());
                json_in += json_item("THOIGIANTUVONGID", dtPopup_Tuvong.Rows[0]["THOIGIANTUVONGID"].ToString());
                json_in += json_item("ISCOKHAMNGHIEM", dtPopup_Tuvong.Rows[0]["ISCOKHAMNGHIEM"].ToString());
                json_in += json_item("NGUYENNHANTUVONGID", dtPopup_Tuvong.Rows[0]["NGUYENNHANTUVONGID"].ToString());
                json_in += json_item("NGUYENNHANTUVONGCHINH", dtPopup_Tuvong.Rows[0]["NGUYENNHANTUVONGCHINH"].ToString());
                json_in += json_item("CHANDOANGIAIPHAUTUTHI", dtPopup_Tuvong.Rows[0]["CHANDOANGIAIPHAUTUTHI"].ToString());
            }
            else if ((XuTriID == "1" || XuTriID == "3" || XuTriID == "9") && dtPopup_Ravien.Rows.Count > 0) // ok
            { // Cấp toa cho về, khác
                json_in += json_item("RAVIENID", dtPopup_Ravien.Rows[0]["RAVIENID"].ToString());
                json_in += json_item("PTHOIGIANRAVIEN", dtPopup_Ravien.Rows[0]["PTHOIGIANRAVIEN"].ToString());
                json_in += json_item("TINHTRANGNGUOIBENHRAVIEN", dtPopup_Ravien.Rows[0]["TINHTRANGNGUOIBENHRAVIEN"].ToString());
                json_in += json_item("PHUONGPHAPDIEUTRI", dtPopup_Ravien.Rows[0]["PHUONGPHAPDIEUTRI"].ToString());
                json_in += json_item("HUONGDIEUTRITIEPTHEO", dtPopup_Ravien.Rows[0]["HUONGDIEUTRITIEPTHEO"].ToString());
                json_in += json_item("PTHOIGIANLICHHEN", dtPopup_Ravien.Rows[0]["PTHOIGIANLICHHEN"].ToString());
                json_in += json_item("LOIDANBACSI", dtPopup_Ravien.Rows[0]["LOIDANBACSI"].ToString());
            }
            else if ((XuTriID == "4" || XuTriID == "5") && dtPopup_Henkham.Rows.Count > 0) // ok
            { // hẹn khám tiếp, khám mới 
                json_in += json_item("LICHHENID", dtPopup_Henkham.Rows[0]["LICHHENID"].ToString());
                json_in += json_item("THOIGIANLICHHEN", dtPopup_Henkham.Rows[0]["THOIGIANLICHHEN"].ToString());
                json_in += json_item("PHONGID", dtPopup_Henkham.Rows[0]["PHONGID"].ToString());
                json_in += json_item("PDICHVUID", dtPopup_Henkham.Rows[0]["PDICHVUID"].ToString());
                json_in += json_item("LOIDANBACSI", dtPopup_Henkham.Rows[0]["LOIDANBACSI"].ToString());
                json_in += json_item("SOLUUTRU", dtPopup_Henkham.Rows[0]["SOLUUTRU"].ToString());
                json_in += json_item("LIENLACVOI_BN", dtPopup_Henkham.Rows[0]["LIENLACVOI_BN"].ToString());
            }
            else if (XuTriID == "7" && dtPopup_Chuyenvien.Rows.Count > 0)
            { //  chuyển viện 
                json_in += json_item("THOIGIANCHUYENVIEN", dtPopup_Chuyenvien.Rows[0]["THOIGIANCHUYENVIEN"].ToString());
                json_in += json_item("MABENHVIENCHUYENDEN", dtPopup_Chuyenvien.Rows[0]["MABENHVIENCHUYENDEN"].ToString());
                json_in += json_item("DIACHIBV", dtPopup_Chuyenvien.Rows[0]["DIACHIBV"].ToString());

                json_in += json_item("DAUHIEULAMSANG", dtPopup_Chuyenvien.Rows[0]["DAUHIEULAMSANG"].ToString());
                json_in += json_item("XETNGHIEM", dtPopup_Chuyenvien.Rows[0]["XETNGHIEM"].ToString());
                json_in += json_item("CHANDOAN", dtPopup_Chuyenvien.Rows[0]["CHANDOAN"].ToString());

                json_in += json_item("THUOC", dtPopup_Chuyenvien.Rows[0]["THUOC"].ToString());
                json_in += json_item("TINHTRANGNGUOIBENH", dtPopup_Chuyenvien.Rows[0]["TINHTRANGNGUOIBENH"].ToString());
                json_in += json_item("HUONGDIEUTRI", dtPopup_Chuyenvien.Rows[0]["HUONGDIEUTRI"].ToString());

                json_in += json_item("PHUONGTIENVANCHUYEN", dtPopup_Chuyenvien.Rows[0]["PHUONGTIENVANCHUYEN"].ToString());
                json_in += json_item("NGUOIVANCHUYEN", dtPopup_Chuyenvien.Rows[0]["NGUOIVANCHUYEN"].ToString());
                json_in += json_item("CHUYENVIENDEN", dtPopup_Chuyenvien.Rows[0]["CHUYENVIENDEN"].ToString());

                json_in += json_item("HINHTHUCID", dtPopup_Chuyenvien.Rows[0]["HINHTHUCID"].ToString());
                json_in += json_item("LYDOID", dtPopup_Chuyenvien.Rows[0]["LYDOID"].ToString());
                json_in += json_item("LOAIID", dtPopup_Chuyenvien.Rows[0]["LOAIID"].ToString());

                json_in += json_item("CHUYENVIENID", dtPopup_Chuyenvien.Rows[0]["CHUYENVIENID"].ToString());
                
                if (dtPopup_Chuyenvien.Columns.Contains("TENCHUYENVIENDEN"))
                    json_in += json_item("TENCHUYENVIENDEN", dtPopup_Chuyenvien.Rows[0]["TENCHUYENVIENDEN"].ToString());
            }
            else if ((XuTriID == "2" || XuTriID == "6") && ucToiKhoa.SelectValue != null) // ok
            {
                json_in += json_item("KHOA", ucToiKhoa.SelectValue.ToString());
            }

            json_in = "{" + json_in.Substring(0, json_in.Length - 1) + "}";
            json_in = json_in.Replace("\"", "\\\"");

            string cap_nhat = ServiceCommonForm.capNhat_XuTri(json_in);
            string[] rets = cap_nhat.Split(',');

            int ret = Func.Parse(rets[0]); 

            if (ret != -1 && ret != 0)
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                btnCapThuoc.Enabled = false;

                // tự động in bảng kê
                if (ret == 100) printBangKe();

                if (LuuVaDong) this.Close();
            }
            else if (cap_nhat == "dieutrinoitru")
            {
                MessageBox.Show("Bệnh nhân đang điều trị nội trú");
            }
            else if (cap_nhat == "dieutringoaitru")
            {
                MessageBox.Show("Bệnh nhân đang điều trị ngoại trú");
            }
            else if (cap_nhat == "chuahoanthanhpttt")
            {
                MessageBox.Show("Bệnh nhân đang có chỉ định PTTT chưa hoàn thành");
            }
            else if (cap_nhat == "codonthuoc")
            {
                MessageBox.Show("Bệnh nhân có đơn thuốc");
            }
            else if (cap_nhat == "connotien")
            {
                MessageBox.Show("Bệnh nhân còn nợ tiền, phải thanh toán mới kết thúc khám.");
            }
            else if (cap_nhat == "ngaydichvu")
            {
                MessageBox.Show("Bệnh nhân có thời gian chỉ định dịch vụ lớn hơn thời gian ra viện, không thể kết thúc khám.");
            }
            else if (cap_nhat == "cophieudangsua")
            {
                MessageBox.Show("Bệnh nhân có phiếu đang sửa.");
            }

            else if (cap_nhat == "dvcls")
            {
                MessageBox.Show("Chưa hoàn thành dịch vụ CLS");
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin-" + cap_nhat);
            }
        }
        private bool validate()
        {
            string XuTriID = ucXuTri.SelectValue;

            if ((XuTriID == "2" || XuTriID == "6") && ucToiKhoa.SelectIndex == -1)
            {
                ucToiKhoa.Focus();
                MessageBox.Show("Hãy chọn khoa");
                return false;
            }
            if (dtimeRaVien.Text == "")
            {
                dtimeRaVien.Focus();
                MessageBox.Show("Hãy nhập ngày ra viên");
                return false;
            }
            if (dtimeRaVien.DateTime < dtimeDenLuc.DateTime)
            {
                dtimeRaVien.Focus();
                MessageBox.Show("Ngày ra viện không thể nhỏ hơn ngày tiếp nhận");
                return false;
            }
            if (ucBenhChinh.SelectedIndex == -1)
            {
                ucBenhChinh.Focus();
                MessageBox.Show("Hãy nhập bệnh chính");
                return false;
            }
            if (ucXuTri.SelectIndex == -1)
            {
                ucXuTri.Focus();
                MessageBox.Show("Hãy chọn xử trí");
                return false;
            }


            string doituongID = ucDoituong.SelectValue;
            if (doituongID == "3" && (XuTriID == "2" || XuTriID == "6"))
            {
                MessageBox.Show("Không xử trí nhập viện hoặc điều trị ngoại trú với bệnh nhân dịch vụ");
                return false;
            }

            if (XuTriID == "1")
            {
                if (ServiceCommonForm.check_DonThuoc(BenhNhan["KHAMBENHID"].ToString()) == "0")
                {
                    MessageBox.Show("Bệnh nhân chưa có đơn thuốc");
                    btnLuu.Enabled = false;
                    btnLuuVaDong.Enabled = false;
                    btnInPhieu.Enabled = false;
                    return false;
                }
            }

            return true;
        }
        private void printBangKe()
        {
            //var flag = jsonrpc.AjaxJson.ajaxCALL_SP_I("VPI01T004.10",$("#hidTIEPNHANID").val());
            //var _dtbnid=$("#cboDOITUONGBENHNHANID").val();
            //var _tiepnhanid=$("#hidTIEPNHANID").val();
            //if(_dtbnid == 1) {
            //    vienphi_tinhtien.inPhoiVP('1', _tiepnhanid, 'NGT001_BKCPKCBBHYTNGOAITRU_01BV_QD3455_A4');
            //    if(flag==1)
            //        vienphi_tinhtien.inPhoiVP('1', _tiepnhanid, 'NGT035_BKCPKCBTUTUCNGOAITRU_A4');
            //} else {
            //    if(flag==1)
            //        vienphi_tinhtien.inPhoiVP('1', _tiepnhanid, 'NGT035_BKCPKCBTUTUCNGOAITRU_A4');
            //}
        }
        private string json_item(string name, string value)
        {
            return "\"" + name + "\":\"" + value + "\"" + ",";
        }
        #endregion

        #region SỰ KIỆN enter

        private void ucDKKCB_KeyEnter(object sender, EventArgs e)
        {
            txtLichSuKham.Focus();
        }
        private void ucDoituong_KeyEnter(object sender, EventArgs e)
        {
            txtYeuCauKhamID.Focus();
        }
        //private void ucYeuCauKham_KeyEnter(object sender, EventArgs e)
        //{
        //    //ucPhongKham.Focus();
        //}
        private void ucCDSoBo_KeyEnter(object sender, EventArgs e)
        {
            ucBenhChinh.Focus();
        }
        private void ucTuyen_KeyEnter(object sender, EventArgs e)
        {
            dtimeTuNgay.Focus();
        }
        private void ucXuTri_KeyEnter(object sender, EventArgs e)
        {
            btnHienThi.Focus();
        }
        private void ucBenhPhu_KeyEnter(object sender, EventArgs e)
        {
            ucXuTri.Focus();
        }
        private void ucBenhChinh_KeyEnter(object sender, EventArgs e)
        {
            ucBenhPhu.Focus();
        }
        private void ucToiKhoa_KeyEnter(object sender, EventArgs e)
        {
            dtimeRaVien.Focus();
        }

        private void dtimeDenLuc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ucDoituong.Focus();
        }

        private void txtYeuCauKhamID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtYeuCauKhamName.Focus();
        }

        private void txtYeuCauKhamName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtSoThe.Focus();
        }

        private void txtSoThe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ucTuyen.Focus();
        }

        private void dtimeTuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) dtimeDenNgay.Focus();
        }

        private void dtimeDenNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ucDKKCB.Focus();
        }

        private void txtLichSuKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ucCDSoBo.Focus();
        }

        private void btnHienThi_KeyDown(object sender, KeyEventArgs e)
        {
            // mở popup
            openPopup();
        }
        #endregion

        private void btnLuuVaDong_Click(object sender, EventArgs e)
        {
            CAP_NHAT(true);
        }



        protected EventHandler event_ListenFrm_KetQua_Thuoc_ChiDinhDV; // dùng để quay lại form cha (Tiếp nhận / Khám bệnh)  
        public void setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(EventHandler event_BackParentForm)
        {
            this.event_ListenFrm_KetQua_Thuoc_ChiDinhDV = event_BackParentForm;
        }
        private void btnCapThuoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default != null) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    this.Close();

                    NTU02D010_CapThuoc frm = new NTU02D010_CapThuoc();
                    frm.loadData("chi_dinh_thuoc", Const.drvBenhNhan);
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(event_ListenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string _getTypePrint()
        {
            var fileReport = "";
            string value = ucXuTri.SelectValue;

            if (value == "1" || value == "3" || value == "9")
            { // Cấp toa cho về, hẹn, khác
                fileReport = "NTU009_GIAYRAVIEN_01BV01_QD4069_A5";
            }
            else if (value == "4" || value == "5")
            { // hẹn khám tiếp, khám mới
                fileReport = "NGT014_GIAYHENKHAMLAI_TT402015_A4";
            }
            else if (value == "7")
            { // chuyển viện
                fileReport = "NGT003_GIAYCHUYENTUYEN_TT14_A4";
            }
            else if (value == "6")
            { // nhap vien
                fileReport = "NGT005_PHIEUKBVAOVIENCHUNG_42BV01_QD4069_A4";
            }
            else if (value == "8")
            { // tử vong
                fileReport = "";
            }
            return fileReport;
        }
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            string _typeIn = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "IN_GOPPHIEU_NV");
            string fileReport = _getTypePrint();
            if (_typeIn == "1" && ucXuTri.SelectValue == "6")
            {
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("benhnhanid", "String", KhambenhID);

                Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, fileReport, "pdf", 720, 1200);
                openForm(frm);
                //_printPhieu($('#hidKHAMBENHID').val(), fileReport); // phiếu NV
                //  openReport('window', "BENHLICH_A4_951", 'pdf', par);// bệnh lịch.	
                 


                DataTable table1 = new DataTable();
                table1.Columns.Add("name");
                table1.Columns.Add("type");
                table1.Columns.Add("value");
                table1.Rows.Add("hosobenhanid", "String", dtThongTinKhamBenh.Rows[0]["HOSOBENHANID"].ToString());

                Controls.SubForm.frmPrint frm1 = new Controls.SubForm.frmPrint(table1, "NGT005_PHIEUDANHGIANHAPVIEN_A4_951", "pdf", 720, 1200);
                openForm(frm1);
                //var _fileBA = Optvalue = $('#cboLOAIBA' + " option:selected").attr('extval0');
                //openReport('window', _fileBA, 'pdf', par1); // bệnh an 1 tờ đâu.
                //them phieu danh gia ban dau benh nhan
                //_printPhieu($('#hidKHAMBENHID').val(), "NGT005_PHIEUDANHGIANHAPVIEN_A4_951");



                DataTable table2 = new DataTable();
                table2.Columns.Add("name");
                table2.Columns.Add("type");
                table2.Columns.Add("value");
                table2.Rows.Add("khambenhid", "String", KhambenhID);
                table2.Rows.Add("khoaid", "String", Const.local_khoaId.ToString());

                Controls.SubForm.frmPrint frm2 = new Controls.SubForm.frmPrint(table2, "PHIEU_SANGLOC_DANHGIA_DINHDUONG_A4_951", "pdf", 720, 1200);
                openForm(frm2);
                //tich hop phieu sang loc va danh gia dinh duong 
                //openReport('window', 'PHIEU_SANGLOC_DANHGIA_DINHDUONG_A4_951', 'pdf', par);
            }
            else
            {
                DataTable table2 = new DataTable();
                table2.Columns.Add("name");
                table2.Columns.Add("type");
                table2.Columns.Add("value");
                table2.Rows.Add("khambenhid", "String", KhambenhID);

                Controls.SubForm.frmPrint frm2 = new Controls.SubForm.frmPrint(table2, fileReport, "pdf", 720, 1200);
                openForm(frm2);
                Controls.SubForm.frmPrint frm3 = new Controls.SubForm.frmPrint(table2, fileReport, "pdf", 720, 1200);
                openForm(frm3);
            }
        }
        private void openForm(Form frm, string optionsPopup = "1")
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        } 
    }
}
public class Cap_Nhat
{
    public int error_code { get; set; }
    public String error_msg { get; set; }
    public String out_var { get; set; }
    public String result { get; set; }
    //public User_Detail detail { get; set; }
}