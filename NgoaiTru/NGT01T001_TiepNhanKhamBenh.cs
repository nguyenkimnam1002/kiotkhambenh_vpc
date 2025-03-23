using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.NgoaiTru.Class;
using System.Reflection;
using DevExpress.XtraGrid.Columns;
using VNPT.HIS.CommonForm;
using DevExpress.Utils;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT01T001_TiepNhanKhamBenh : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT01T001_TiepNhanKhamBenh()
        {
            InitializeComponent();
        }

        private void frmTiepNhanKhamBenh_Load(object sender, EventArgs e)
        {
            //var data = new
            //{
            //    requestCode = "123",
            //    cancelReason = "HIS hủy yêu cầu",
            //    cancelBy = Const.local_user.USER_ID
            //};
            //string send = RISConnector.Send_RIS_DELETE_REQUEST("123", Newtonsoft.Json.JsonConvert.SerializeObject(data));

            //// string send = RISConnector.RequestPOST("123", "{\"test\":\"test\"}");


            Init_Form();

            string HopDong = getPara("hopdong");
            if (HopDong == "1")
            {
                //panelHopDong.Visible = true;
                gboxGoiKham.Visible = true;
                this.Text = "Tiếp nhận bệnh nhân hợp đồng";
                DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DSHopDong);
                ucHopDong.setData(dt, 0, 1);
                ucHopDong.setColumn(0, false);
                ucHopDong.SelectIndex = 0;
            }
            else panelHopDong.Visible = false;

        }
        string lbPara = "#";
        private string getPara(string name)
        {//  &kbtraingay=1&x=2&
            string ret = "";
            try
            {
                if (lbPara == "#")
                {
                    Control[] control = this.Controls.Find("lbPara", false);
                    if (control.Length > 0)
                    {
                        if (control[0].GetType().ToString() == "DevExpress.XtraEditors.LabelControl")
                        {
                            DevExpress.XtraEditors.LabelControl lbControl = (DevExpress.XtraEditors.LabelControl)control[0];
                            lbPara = "&" + lbControl.Text + "&";
                        }
                    }
                }

                if (lbPara.IndexOf("&" + name) > -1)
                {
                    string temp = lbPara.Substring(lbPara.IndexOf("&" + name) + ("&" + name).Length + 1);
                    if (temp.IndexOf("&") > -1) ret = temp.Substring(0, temp.IndexOf("&"));
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }

            return ret;
        }

        public void Init_Form()
        {
            for (int i = 0; i < tabTiepNhanBN.Pages.Count; i++)
                tabTiepNhanBN.ButtonsPanel.Buttons[i].Properties.Appearance.Font = Const.fontDefault;

            HienThiGoiKham();
            ThietLapCauHinh();

            load_tab1();
        }

        string LNMBP_XetNghiem = "1";
        string LNMBP_CDHA = "2";
        string LNMBP_ChuyenKhoa = "5";
        string LNMBP_DieuTri = "4";
        string LNMBP_Phieuvattu = "8";
        string LNMBP_Phieuthuoc = "7";
        string LNMBP_PhieuVanChuyen = "16";

        string LNMBP_PhieuThuKhac = "17";
        string LNMBP_TienCongKham = "3";

        string _flgModeView = "0";
        int _songayluiBHYT = 1; 

        string loadBy_KHAMBENHID = "";
        private void tabTiepNhanBN_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

            try
            {
                if (tabTiepNhanBN.SelectedPage == tabTiepNhan)
                {
                    if (loadBy_KHAMBENHID != "")
                    {
                        TimKiem_KhamBenh(loadBy_KHAMBENHID);
                        loadBy_KHAMBENHID = "";
                    }
                }
                else if (tabTiepNhanBN.SelectedPage == tabDsBN)
                {
                    if (bLoad_tab2)
                    {
                        load_tab2();
                        //init_tab2();
                        bLoad_tab2 = false;
                    }
                    getData_table(1, null);
                }
                else
                {
                    if (Const.drvBenhNhan != null)
                    {
                        string KHAMBENHID = Const.drvBenhNhan["KHAMBENHID"].ToString();
                        getChiTiet_KhamBenh(KHAMBENHID); // để lấy BENHNHANID

                        if (dtKHAMBENH.Rows.Count > 0)
                        {
                            string BENHNHANID = dtKHAMBENH.Rows[0]["BENHNHANID"].ToString();  // đc  BENHNHANID

                            if (tabTiepNhanBN.SelectedPage == tabXetNghiem)
                                ucTabXetNghiem1.loadData_2(KHAMBENHID, BENHNHANID, LNMBP_XetNghiem, _flgModeView, "", "", "");

                            else if (tabTiepNhanBN.SelectedPage == tabCDHA)
                                ucTabCDHA1.loadData_2(KHAMBENHID, BENHNHANID, LNMBP_CDHA, _flgModeView, "", "", "", "");

                            else if (tabTiepNhanBN.SelectedPage == tabPhauThuat)
                                ucTabPhauThuatThuThuat1.loadData_2(KHAMBENHID, BENHNHANID, LNMBP_ChuyenKhoa, _flgModeView, "", "", "", "");

                            else if (tabTiepNhanBN.SelectedPage == tabTienCongKham)
                                ucTabTienCongKham.loadData(KHAMBENHID, BENHNHANID, LNMBP_TienCongKham, _flgModeView, "", "2");

                            else if (tabTiepNhanBN.SelectedPage == tabPhieuThuKhac)
                                ucTabPhieuThuKhac.loadData(KHAMBENHID, BENHNHANID, LNMBP_PhieuThuKhac, _flgModeView, "", "1");
                        }
                    }
                }
            }
            finally
            {
                //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        bool bLoad_tab2 = true;

        #region CÁC HÀM LẤY DL CHUNG
        string savePHONGID = "";
        string saveKHAMBENHID = "";
        private DataTable dtKHAMBENH = new DataTable();
        private void getChiTiet_KhamBenh(string KHAMBENHID)
        {
            if (saveKHAMBENHID != KHAMBENHID || dtKHAMBENH.Rows.Count == 0)
            {
                DataTable dt = RequestHTTP.getChiTiet_KhamBenh(KHAMBENHID);
                if (dt.Rows.Count > 0)
                {
                    saveKHAMBENHID = KHAMBENHID;
                    savePHONGID = dt.Rows[0]["PHONGID"].ToString();
                    dtKHAMBENH = dt;
                }
                else
                {
                    saveKHAMBENHID = "";
                    dtKHAMBENH = new DataTable();
                }
            }
        }
        private void TimKiem_KhamBenh(string KHAMBENHID)
        {
            getChiTiet_KhamBenh(KHAMBENHID);

            if (dtKHAMBENH.Rows.Count > 0)
            {
                set_defaul_value();
                ucThongTinHanhChinh1.load_benhnhan_theoMa(dtKHAMBENH);
                loadThongTinKhamBenh(dtKHAMBENH);

            }
        }
        private void TimKiem_BenhNhan(string ma, string kieu)
        {
            //1 theo mã bệnh nhân
            //2 theo mã bhyt

            //string tenbenhnhan = ucThongTinHanhChinh1.txtHoten.Text;
            //string ngaysinh = ucThongTinHanhChinh1.dtimeNgaysinh.Text;
            //string gioitinhid = ucThongTinHanhChinh1.ucGioitinh.SelectValue;
            string tenbenhnhan = "";
            string ngaysinh = "";
            string gioitinhid = ucThongTinHanhChinh1.ucGioitinh.SelectValue;

            DataTable dtTT_BenhNhan = Common.RequestHTTP.getChiTiet_BenhNhan(ma, kieu, tenbenhnhan, ngaysinh, gioitinhid);

            if (dtTT_BenhNhan.Rows.Count > 0)
            {
                set_defaul_value();

                saveKHAMBENHID = "";
                dtKHAMBENH = new DataTable(); // saveKHAMBENHID được gán mới thì phải reset dtKHAMBENH

                // với bệnh nhân đã kết thúc khám sẽ ko trả về KHAMBENHID
                if (dtTT_BenhNhan.Columns.Contains("KHAMBENHID") == true)
                {
                    saveKHAMBENHID = dtTT_BenhNhan.Rows[0]["KHAMBENHID"].ToString();
                    savePHONGID = dtTT_BenhNhan.Rows[0]["PHONGID"].ToString();
                } 

                ThayDoiMenu(dtTT_BenhNhan.Rows[0]["TRANGTHAIKHAMBENH"].ToString());

                ucThongTinHanhChinh1.load_benhnhan_theoMa(dtTT_BenhNhan);
                loadThongTinKhamBenh(dtTT_BenhNhan);
            }

        }

        #endregion


        #region GỌI KHÁM + THIẾT LẬP CẤU HÌNH
        //THIẾT LẬP CẤU HÌNH
        private DataTable dtCauHinh = new DataTable();
        private DataTable dtCauHinh_GOI = new DataTable();
        private string LOAD_YEUCAUKHAM_THEO_DT = "0";
        private void ThietLapCauHinh()
        {
            LOAD_YEUCAUKHAM_THEO_DT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "LOAD_YEUCAUKHAM_THEO_DT");

            dtCauHinh = RequestHTTP.get_ajaxExecuteQueryO("NGT_STT_DT", DateTime.Now.ToString(Const.FORMAT_date1));
            // SOTHUTU: 0001  TSFOCUS: -1  TSDOITUONG: -1  DTUUTIEN: -1  THUKHAC: -1  MOPOPUPPHONGKHAM: -1  SETYEUCAUKHAM: -1  NGAYTN: 1  
            // BTNTHUTIEN: 0  CAPCUU: -1  SONGAYBHYT: 2  NGAYVP: 1  DTMIENGIAM: 1  CHUPANH: 0  SINHTON: 0  GOIKHAM: 1  INPHIEU: 0  ANPHIEU: 0  
            // ANCHECKBOXBHYTDV: 0  CHANNHIEUPHONG: 0  CHECK_BHYT_CONG: 1  TECHUSER: user  TECHPASS: 7c4a8d09ca3762af61e59520943dc26494f8941b  
            // TECHKCBBD: 01929  URLWEBSITE: localhost:8080  MAHONGHEO: N01, N02, N3A, N3B ; HCN  BATCHECKNGHEO: 79  DTKHAIBAO: 0  YEUCAUKHAM: 0  
            // AN_DOI_CONGKHAM_PK: 0  ANDENNGAYBHYT: 0  AN_SINHSO_UUTIEN: 0}	 TUDONGINPHIEUKHAM HOPDONG
            dtCauHinh_GOI = ServiceTiepNhanBenhNhan.getNGT_STT_GOI();
            //if (dtCauHinh_GOI.Rows.Count > 0)
            //{
            //    ucDoituong.SelectValue = dtCauHinh_GOI.Rows[0]["TSFOCUSDT"].ToString();
            //}


            if (dtCauHinh.Rows.Count > 0)
            {
                try
                { 
                    btnThuTien.Visibility = dtCauHinh.Rows[0]["BTNTHUTIEN"].ToString() == "0" ? DevExpress.XtraBars.BarItemVisibility.Never : DevExpress.XtraBars.BarItemVisibility.Always;

                    btnXuTri.Visibility = dtCauHinh.Rows[0]["CHANNHIEUPHONG"].ToString() == "0" ? DevExpress.XtraBars.BarItemVisibility.Always : btnXuTri.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                    //Áp dụng đối tượng miễn giảm:
                    //- 1: có áp dụng
                    //- giá trị khác 1: không áp dụng
                    cboMienGiam.Enabled = dtCauHinh.Rows[0]["DTMIENGIAM"].ToString() == "1";
                    //Sử dụng chức năng ghi nhận dấu hiệu sinh tồn trên màn hình tiếp nhận:
                    //- 1: Có sử dụng
                    //- Giá trị khác 1: Không sử dụng
                    layoutSinhTon.Visibility = dtCauHinh.Rows[0]["SINHTON"].ToString() == "1" ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //Sử dụng chức năng chụp ảnh ngoại trú:
                    //- 1: Có sử dụng
                    //- Giá trị khác 1: Không sử dụng
                    layoutChupAnh.Visibility = dtCauHinh.Rows[0]["CHUPANH"].ToString() == "1" ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem_Anh.Visibility = dtCauHinh.Rows[0]["CHUPANH"].ToString() == "1" ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    // 0: Hien thi ; Nguoc lai an 
                    layoutInPhieu.Visibility = dtCauHinh.Rows[0]["ANPHIEU"].ToString() == "0" ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    btnIn.Enabled = dtCauHinh.Rows[0]["ANPHIEU"].ToString() == "0";

                    int newwwidth =
                        (layoutLuu.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 80 : 0)
                        + (layoutNhapMoi.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 90 : 0)

                        + (layoutChuyentuyen.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 120 : 0)
                        + (layoutInPhieu.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 90 : 0)

                        + (layoutSinhTon.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 90 : 0)
                        + (layoutDSHenKham.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 120 : 0)

                        + (layoutChupAnh.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 100 : 0)
                        + (layoutDong.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always ? 80 : 0);


                    LCI_button.ControlMaxSize = new Size(newwwidth, 0);

                    if (dtCauHinh.Columns.Contains("SONGAYBHYT")) _songayluiBHYT = Func.Parse(dtCauHinh.Rows[0]["SONGAYBHYT"].ToString());

                    ServiceBYT.ServiceBYT_Url = dtCauHinh.Rows[0]["BYTURL"].ToString(); // = "https://congdulieuyte.vn/hPortal/services/IC/WSPortal?wsdl";
                    ServiceBYT.ServiceBYT_BYTDAYDL = dtCauHinh.Rows[0]["BYTDAYDL"].ToString();
                    ServiceBYT.ServiceBYT_BYTSTOPCHUCNANG = dtCauHinh.Rows[0]["BYTSTOPCHUCNANG"].ToString();
                    ServiceBYT.ServiceBYT_TECHKCBBD = dtCauHinh.Rows[0]["TECHKCBBD"].ToString();
                }
                catch (Exception ex)
                { log.Fatal(ex.ToString()); }
            }
        }

        //GỌI KHÁM
        string HIS_HIENTHI_GOIKHAM = "";
        private void HienThiGoiKham()
        {
            //Thiết lập cấu hình cách thức gọi khám trên form tiếp nhận:
            //- 0 : Không sử dụng gọi khám
            //- 1 : Gọi khám theo cách cũ
            //- 2 : Gọi khám theo cách mới (BVNT)
            //- 4 : Gọi khám theo cách mới(BVBD HCM) 

            gboxGoiKham.Size = new Size(gboxGoiKham.Size.Width, 30);
            panelGoiKham_BVNgTrai.Visible = false;
            panelGoiKham_KieuCu.Visible = false;
            panelGoiKham_BVBuuDien.Visible = false;

            HIS_HIENTHI_GOIKHAM = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_HIENTHI_GOIKHAM");
            if (HIS_HIENTHI_GOIKHAM == "2" || HIS_HIENTHI_GOIKHAM == "4")
            {
                if (Const.local_phongId.ToString() == "0" || Const.local_phongId.ToString() == "")
                {
                    MessageBox.Show("Yêu cầu thực hiện thiết lập phòng.");
                    return;
                }
            }

            string HIS_HIENTHI_GOIKHAM_HT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_HIENTHI_GOIKHAM_HT");

            string NGT01T001_CHECKHT = RequestHTTP.call_ajaxCALL_SP_S_result("NGT01T001.CHECKHT", Const.local_phongId.ToString());

            if (HIS_HIENTHI_GOIKHAM == "1") // kiểu cũ
            {
                panelGoiKham_KieuCu.Visible = true;
            }
            else if (HIS_HIENTHI_GOIKHAM == "2")// bv Nguyễn trãi        
            {
                if (HIS_HIENTHI_GOIKHAM_HT == "0" || (HIS_HIENTHI_GOIKHAM_HT == "1" && NGT01T001_CHECKHT != "0"))
                {
                    panelGoiKham_BVNgTrai.Visible = true;
                    Goi_Kham("0");
                }
                else gboxGoiKham.Visible = false;
            }
            else if (HIS_HIENTHI_GOIKHAM == "4")// bv Nguyễn trãi        
            {
                if (HIS_HIENTHI_GOIKHAM_HT == "0" || (HIS_HIENTHI_GOIKHAM_HT == "1" && NGT01T001_CHECKHT != "0"))
                {
                    panelGoiKham_BVBuuDien.Visible = true;
                    //Goi_Kham("0");
                }
                else gboxGoiKham.Visible = false;
            }

            else // ẩn hết
                gboxGoiKham.Visible = false;
        }
        private void Goi_Kham(string source)
        {
            string phongid = Const.local_phongId.ToString();
            // {"func":"ajaxCALL_SP_O","params":["NGT02K053.LCD","$4763$0",0],"uuid":"a01b679d-9d71-45b2-8647-629ccb3cbe54"}
            DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K053.LCD", txtstt_bd1.Text + "$" + phongid + "$" + source, 0);
            // "result": "[{\n\"ORG_ID\": \"4763\",\n\"MANHINHID\": \"1\",\n\"STT\": \"60\",\n\"ORG_NAME\": \"QUẦY 01\",\n\"NGAY\": \"09/11/2017\",\n\"TEN\": \"SYT-Bệnh Viện Nguyễn Trãi-Khoa Khám Bệnh\",\n\"PATH\": \"../common/image/logo_902.jpg\",\n\"CHECKED\": \"1\"}]",
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["CHECKED"].ToString() == "1")
                {
                    lbQuay.Text = dt.Rows[i]["ORG_NAME"].ToString();
                    txtstt_bd1.Text = Func.addZezo(Convert.ToInt32(dt.Rows[i]["STT"]), 4);
                    txtstt_kt1.Text = Func.addZezo(Convert.ToInt32(dt.Rows[i]["STT"]) + 1, 4);
                    break;
                }
            }


            txtstt_bd1.Location = new Point(lbQuay.Location.X + lbQuay.Size.Width + 40, txtstt_bd1.Location.Y);
            txtstt_kt1.Location = new Point(lbQuay.Location.X + lbQuay.Size.Width + 40 + 100, txtstt_kt1.Location.Y);
            btnGoiKham.Location = new Point(lbQuay.Location.X + lbQuay.Size.Width + 40 + 200, btnGoiKham.Location.Y);

        }

        private void btnGoiKham_Click(object sender, EventArgs e)
        {
            Goi_Kham("1");
        }


        #endregion

        #region TAB Tiếp nhận

        private void load_tab1()
        {
            DataTable dt = new DataTable();

            ucThongTinHanhChinh1.setEvent_DiaChi_Change(DiaChi_Change);
            ucThongTinHanhChinh1.setEvent_Check_TheTE(Check_TheTE);
            ucThongTinHanhChinh1.setEvent_Load_BN(loadBN_from_BoxMaBN);
            ucThongTinHanhChinh1.setEvent_KeyEnter(ucTTHanhChinh_KeyEnter);

            ucTabXetNghiem1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabCDHA1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabPhauThuatThuThuat1.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabPhieuThuKhac.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);
            ucTabTienCongKham.setEvent_BackParentForm(listenFrm_Mo_Thuoc_ChiDinhDV);


            #region THÔNG TIN KHÁM
            //Ngày khám 12/06/2017 14:44:12

            //cbo Doituong
            dt = Common.RequestHTTP.get_ajaxExecuteQuery("NT.007", new string[] { "[S0]", "[S1]", "[S2]", "[S3]" }
                    , new String[] { Const.local_user.HOSPITAL_ID, Const.local_user.USER_ID, Const.local_user.USER_GROUP_ID, Const.local_user.PROVINCE_ID });
            // thêm dòng:  6: BHYT + DV
            if (dt.Rows.Count > 0) dt.Rows.Add("6", "BHYT + DV");

            ucDoituong.setEvent(ucDoituong_SelectedIndexChanged);
            ucDoituong.setEvent_Enter(ucDoituong_KeyEnter);
            ucDoituong.lookUpEdit.Properties.ShowHeader = false;
            ucDoituong.setData(dt, 0, 1);
            ucDoituong.setColumn("col1", -1, "", 0);
            ucDoituong.CaptionValidate = true;

            //yêu cầu khám
            ucYeuCauKham.setEvent_Enter(ucYeuCauKham_KeyEnter);
            ucYeuCauKham.setEvent(ucYeuCauKham_SelectedIndexChanged);
            ucYeuCauKham.CaptionValidate = true;

            //Phòng khám
            ucPhongKham.setEvent_Enter(ucPhongKham_KeyEnter);
            ucPhongKham.setEvent(ucPhongKham_SelectedChanged);
            ucPhongKham.CaptionValidate = true;

            //Nơi đk KCB và BV chuyển đến (cùng dl)
            dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_NoiDKKCB);
            //ucDKKCB.setEvent(cboTinh_SelectedIndexChanged);
            ucDKKCB.setData(dt, "BENHVIENKCBBD", "TENBENHVIEN");
            ucDKKCB.setEvent_Enter(ucDKKCB_KeyDown);
            ucDKKCB.setColumn("RN", -1, "", 0);
            ucDKKCB.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 35);
            ucDKKCB.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
            ucDKKCB.setColumn("DIACHI", 2, "Địa chỉ", 0);
            //  ucDKKCB.CaptionValidate = ucDKKCB.Enabled;

            ucNoichuyen.setData(dt, "BENHVIENKCBBD", "TENBENHVIEN");
            ucNoichuyen.setColumn("RN", -1, "", 0);
            ucNoichuyen.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 30);
            ucNoichuyen.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
            ucNoichuyen.setColumn("DIACHI", 2, "Địa chỉ", 0);
            ucNoichuyen.setEvent_Enter(ucNoichuyen_Enter);

            // Nơi sống
            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Noisong, "76");
            ucNoisong.lookUpEdit.Properties.ShowHeader = false;
            ucNoisong.setData(dt, 0, 1);
            ucNoisong.setColumn("col1", -1, "", 0);
            ucNoisong.setEvent_Enter(ucNoisong_KeyDown);

            // cboTuyen
            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Tuyen, "58");
            ucTuyen.lookUpEdit.Properties.ShowHeader = false;
            ucTuyen.setData(dt, 0, 1);
            ucTuyen.setEvent(ucTuyen_SelectedIndexChanged);
            ucTuyen.setEvent_Enter(ucTuyen_Enter);
            ucTuyen.SelectIndex = 0;
            //   ucTuyen.CaptionValidate = ucTuyen.Enabled;

            //Đối tượng Miễn giảm 

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DTMienGiam);
            cboMienGiam.setData(dt, "col1", "col2");
            cboMienGiam.setEvent_Enter(cboMienGiam_Enter);
            cboMienGiam.setColumn("col1", 0, "STT", 0);
            cboMienGiam.setColumn("col2", 1, "Đối tượng", 0);
            cboMienGiam.setColumn("col3", 2, "Mã", 0);
            cboMienGiam.setColumn("col4", -1, "", 0);

            // Thu dịch vụ khác
            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DichVuKhac);
            ucThukhac.setData(dt, 0, 1);
            ucThukhac.setEvent_Enter(ucThukhac_Enter);

            //Danh sách bệnh chuẩn đoán
            dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
            ucChuandoan.setData(dt, "ICD10CODE", "ICD10NAME");
            ucChuandoan.setColumn("RN", -1, "", 0);
            ucChuandoan.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucChuandoan.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
            ucChuandoan.setEvent(ucChuandoan_SelectedIndexChanged);
            ucChuandoan.setEvent_Enter(ucChuandoan_KeyEnter);
            ucChuandoan.Option_CaptionValidate = ucChuandoan.Enabled;

            ucNoichuyen.Option_CaptionValidate = ucNoichuyen.Enabled;

            #endregion

            reset_value();
            set_defaul_value();
        }

        private void reset_value()
        {
            // Khởi tạo các biến 
            saveKHAMBENHID = "";
            dtKHAMBENH = new DataTable();

            BN_Sua = null;
        }
        private void set_defaul_value()
        {
            try
            {
                // Form
                ucThongTinHanhChinh1.set_defaul_value();

                DateTime dtime = Func.getSysDatetime();
                dtimeNgaykham.DateTime = dtime;
                dtimeTungay.DateTime = new DateTime(dtime.Year, 1, 1);
                dtimeDenngay.DateTime = new DateTime(dtime.Year, 12, 31);

                ucDoituong.SelectValue = dtCauHinh_GOI.Rows[0]["TSFOCUSDT"].ToString();

                ckbUuTien.Checked = dtCauHinh.Rows[0]["DTUUTIEN"].ToString() == "1";

                ucYeuCauKham.SelectIndex = 0;
                ucYeuCauKham.SelectTextSearch = "";

                ucPhongKham.SelectValue = "";
                ucPhongKham.SelectTextSearch = "";

                ucNoisong.SelectIndex = -1;

                // Cấu hình mặc định
                ckbCheckBHYT.Checked = dtCauHinh.Rows[0]["CHECK_BHYT_CONG"].ToString() == "1";
                rbtCapCuu.EditValue = dtCauHinh.Rows[0]["CAPCUU"].ToString() == "1" ? "2" : "3"; // 2 là cấp cứu

                ucDKKCB.SelectedValue = Const.local_user.HOSPITAL_ID;

                txtSoThe.Text = "";
                ckbTheTE.Checked = false;
                ckbGiuBHYT.Checked = false;

                txtMADOITUONGNGHEO.Text = "";
                txtMuchuong.Text = "";

                cboMienGiam.SelectIndex = -1;
                ucDKKCB.searchLookUpEdit.EditValue = Const.local_user.HOSPITAL_CODE;

                ucTuyen.SelectIndex = 0;
                ckbDu5n6t.Checked = false;
                dtimeNgaydu.Text = "";

                ucChuandoan.SelectedText = "";
                ucChuandoan.SelectedValue = "";

                ucNoichuyen.SelectedIndex = -1;
                txtLichsukham.Text = "";

                ucThukhac.SelectValue = "";

                btnIn.Enabled = true;
                btnThongBao.Enabled = true;
                btnDichVuCLS.Enabled = false;
                btnXuTri.Enabled = false;
                btnThuKhac.Enabled = false;
                btnHoaHong.Enabled = false;
                btnLichSu.Enabled = true;



                // 
                dtChuyenVien = new DataTable();
                dtChuyenVien.Columns.Add("ucBenhVien");
                dtChuyenVien.Columns.Add("ucCDTD");
                dtChuyenVien.Columns.Add("ucHinhThucChuyen");
                dtChuyenVien.Columns.Add("ucLyDoChuyen");
                dtChuyenVien.Columns.Add("rbtChuyen");
                dtChuyenVien.Rows.Add("", "", "", "", "");

                ucYeuCauKham.ReadOnly = false;
                ucPhongKham.ReadOnly = false;
                btnMoRongPK.Enabled = true;

                ucThongTinHanhChinh1.txtHoten.Focus();

                btnLuu.Enabled = true; 

                picAnhBenhNhan.Image = Func.getIcon("nobody.png");
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ThayDoiMenu(string trangthai) // [["1","Chờ khám"],["4","Đang khám"],["9","Kết thúc khám"]]    
        { 
            if (trangthai != "")
            {
                ucYeuCauKham.ReadOnly = true; 
                    ucPhongKham.ReadOnly = true;
                    btnMoRongPK.Enabled = false;

                if (layoutSinhTon.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    btnSinhton.Enabled = true;
                if (layoutChupAnh.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    btnChupHinh.Enabled = true;
                 
                if (trangthai != "9")
                {
                    _flgModeView = "0";
                    btnDichVuCLS.Enabled = true;
                    btnXuTri.Enabled = true;
                    btnThuKhac.Enabled = true;
                    btnHoaHong.Enabled = true;
                    

                    if (trangthai == "1") // "1","Chờ khám"
                    {
                        btnSinhton.Enabled = true;
                        btnChupHinh.Enabled = true;
                        btnLuu.Enabled = true;

                    }
                    else if(trangthai == "4") //"4","Đang khám"
                    {
                        //btnSinhton.Enabled = false;
                        btnChupHinh.Enabled = false;
                        btnLuu.Enabled = false;
                    }
                }
                else if (trangthai == "9") //  9:kết thúc khám
                {
                    _flgModeView = "1";
                    btnDichVuCLS.Enabled = false;
                    btnXuTri.Enabled = false;
                    btnThuKhac.Enabled = false;
                    btnHoaHong.Enabled = false;
                    btnSinhton.Enabled = false;
                    btnChupHinh.Enabled = false;
                    btnLuu.Enabled = false;
                     
                }
            }
        }

        private void DiaChi_Change(object sender, EventArgs e)
        {
            txtDcBaoHiem.Text = (string)sender;
        }
        private void Check_TheTE(object sender, EventArgs e)
        {
            //ckbTheTE.Enabled = (bool)sender;
        }

        private void loadBN_from_BoxMaBN(object sender, EventArgs e)
        {
            string maBN = (string)sender;
            TimKiem_BenhNhan(maBN, "1");
        }



        DataRow BN_Sua = null;
        private void loadThongTinKhamBenh(DataTable dtBN)
        {
            try
            {
                if (dtBN.Rows.Count > 0)
                {
                    BN_Sua = dtBN.Rows[0];  

                    ////"CHANDOANTUYENDUOI": "Bệnh tả",  THUKHAC
                    ////MACHANDOANTUYENDUOI": "A00", 
                    ////txtChuandoan.Text = // ko gán trường này
                    ucChuandoan.memoEdit1.Text = BN_Sua["CHANDOANTUYENDUOI"].ToString();

                    DataTable dt = RequestHTTP.getLichSuKhamBenh(BN_Sua["BENHNHANID"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        txtLichsukham.Text += dt.Rows[i][1].ToString() + "\r\n";
                    }
                    ////MANOIGIOITHIEU: "", 
                    ////HINHTHUCVAOVIENID": "3", // 2 cấp cứu, khám 3
                    rbtCapCuu.EditValue = BN_Sua["HINHTHUCVAOVIENID"].ToString();

                    ////TRANGTHAIKHAMBENH: "1", // 1 chờ, 4 đang khám, 9 kết thúc
                    ThayDoiMenu(BN_Sua["TRANGTHAIKHAMBENH"].ToString());

                    ////DTBNID": "1", // 1 Bảo hiểm, 2 khám viện phí, 3 khám dịch vụ
                    if (BN_Sua["DTBNID"].ToString() == "6") BN_Sua["DTBNID"] = "1";
                    ucDoituong.SelectValue = BN_Sua["DTBNID"].ToString();

                    ucYeuCauKham.SelectValue = BN_Sua["DICHVUID"].ToString();
                    try
                    {
                        // TH load từ ô Mã BN thì ko có trường này
                        ucPhongKham.SelectValue = BN_Sua["PHONGKHAMID"].ToString();
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                    ////UUTIENKHAMID": "0",  
                    ckbUuTien.Checked = BN_Sua["UUTIENKHAMID"].ToString() == "1" || BN_Sua["UUTIENKHAMID"].ToString() == "3";
                    ckbTheTE.Checked = BN_Sua["SINHTHEBHYT"].ToString() == "1";
                    // "19/06/2017 10:18"
                    try
                    {
                        //dtimeNgaykham.Text = BN_Sua["NGAYTIEPNHAN"].ToString();

                        if (BN_Sua["NGAYTIEPNHAN"].ToString() != "")
                        {
                            if (BN_Sua["NGAYTIEPNHAN"].ToString().IndexOf(":") != BN_Sua["NGAYTIEPNHAN"].ToString().LastIndexOf(":"))
                                dtimeNgaykham.DateTime = Func.ParseDatetime(BN_Sua["NGAYTIEPNHAN"].ToString());
                            else if (BN_Sua["NGAYTIEPNHAN"].ToString().IndexOf(":") > -1)
                                dtimeNgaykham.DateTime = DateTime.ParseExact(BN_Sua["NGAYTIEPNHAN"].ToString(), Const.FORMAT_datetime2, CultureInfo.InvariantCulture);
                            else
                                dtimeNgaykham.DateTime = Func.ParseDate(BN_Sua["NGAYTIEPNHAN"].ToString());
                        }
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                    if (BN_Sua["MA_BHYT"].ToString() != "")
                    {
                        txtSoThe.Text = BN_Sua["MA_BHYT"].ToString();
                    }


                    try
                    {
                        //BHYT_BD": "22/10/2016", 
                        if (BN_Sua["BHYT_BD"].ToString() != "")
                        {
                            DateTime dtime = Func.ParseDate(BN_Sua["BHYT_BD"].ToString());
                            dtimeTungay.DateTime = dtime;
                        }
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                    try
                    {
                        //BHYT_KT": "21/10/2022,
                        if (BN_Sua["BHYT_KT"].ToString() != "")
                        {
                            DateTime dtime = Func.ParseDate(BN_Sua["BHYT_KT"].ToString());
                            dtimeDenngay.DateTime = dtime;
                        }
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                    //"MA_KCBBD": "35148", 
                    //TEN_KCBBD: "Bệnh viện sản nhi Hà Nam", 
                    ucDKKCB.SelectedValue = BN_Sua["MA_KCBBD"].ToString();
                    //DIACHI_BHYT": "Phường Trần Hưng Đạo-Quận Hoàn Kiếm-TP Hà Nội",   
                    txtDcBaoHiem.Text = BN_Sua["DIACHI_BHYT"].ToString();

                    //BHYT_LOAIID": "1", đúng tuyến, 2 đúng tuyến gt, 3 đúng ccuu, 4 trái
                    ucTuyen.SelectValue = BN_Sua["BHYT_LOAIID"].ToString();

                    //DT_SINHSONG": "5", 
                    ucNoisong.SelectValue = BN_Sua["DT_SINHSONG"].ToString();

                    //DU5NAM6THANGLUONGCOBAN": "0", 
                    ckbDu5n6t.Checked = BN_Sua["DU5NAM6THANGLUONGCOBAN"].ToString() == "1";

                    //QUYEN_LOI": null,
                    //"MUC_HUONG": null,
                    txtMuchuong.Text = BN_Sua["MUC_HUONG"].ToString();

                    ////DICHVUID": null,
                    //"THUKHAC": "0", 
                    ucThukhac.SelectValue = BN_Sua["THUKHAC"].ToString();

                    ////TENNOIGIOITHIEU": "", 
                    ucNoichuyen.SelectedValue = BN_Sua["MANOIGIOITHIEU"].ToString();

                    try
                    {
                        if (BN_Sua.Table.Columns.Contains("ANHBENHNHAN") && BN_Sua["ANHBENHNHAN"].ToString().Trim() != "")
                            picAnhBenhNhan.LoadAsync(BN_Sua["ANHBENHNHAN"].ToString().Trim());
                    }
                    catch (Exception ex) { }


                    ////SLXN": "0", 
                    ////SLCDHA": "0", 
                    ////"ANHBENHNHAN": null,  
                    ////SLCHUYENKHOA": "0", 
                    ////CONGKHAM": "0", 
                    ////NGAYTHUOC": "06/06/2017 16:31:00"}]",

                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }


        #region SỰ KIỆN BẤM Enter, KeyDown
        private void ucDoituong_KeyEnter(object sender, EventArgs e)
        {
            if (txtSoThe.Enabled) txtSoThe.Focus();
            else btnLuu.Focus();
        }
        private void ucYeuCauKham_KeyEnter(object sender, EventArgs e)
        {
            ucPhongKham.Focus();
        }
        private void ucPhongKham_KeyEnter(object sender, EventArgs e)
        {
            ucDoituong.Focus();
        }
        private void ucChuandoan_KeyEnter(object sender, EventArgs e)
        {
            ucNoichuyen.Focus();
        }

        private void ucTTHanhChinh_KeyEnter(object sender, EventArgs e)
        {
            dtimeNgaykham.Focus();
        }


        private void rbtCapCuu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ckbUuTien.Focus();
            }
        }

        private void ckbUuTien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ucYeuCauKham.Focus();
            }
        }

        private void btnPhongkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ucNoisong.Focus();
            }
        }
        private void ucNoisong_KeyDown(object sender, EventArgs e)
        {
            rbtCapCuu.Focus();
        }
        private void cboMienGiam_Enter(object sender, EventArgs e)
        {
            if (txtMADOITUONGNGHEO.Enabled) txtMADOITUONGNGHEO.Focus();
            else ucDKKCB.Focus();
        }
        private void ucDKKCB_KeyDown(object sender, EventArgs e)
        {
            dtimeTungay.Focus();
        }

        private void txtSoThe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ckbCheckBHYT.Focus();
            }
        }

        private void ckbTheTE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ckbGiuBHYT.Focus();
            }
        }

        private void ckbGiuBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboMienGiam.Focus();
            }
        }

        private void dtimeTungay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtimeDenngay.Focus();
            }
        }

        private void dtimeDenngay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDcBaoHiem.Focus();
            }
        }

        private void txtDcBaoHiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ucThukhac.Focus();
            }
        }
        private void ucThukhac_Enter(object sender, EventArgs e)
        {
            if (txtMuchuong.Enabled) txtMuchuong.Focus();
            else ucTuyen.Focus();
        }

        private void txtMuchuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ucTuyen.Focus();
            }
        }
        private void ucTuyen_Enter(object sender, EventArgs e)
        {
            ckbDu5n6t.Focus();
        }

        private void ckbDu5n6t_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtimeNgaydu.Focus();
            }
        }

        private void dtimeNgaydu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ucChuandoan.Enabled) ucChuandoan.Focus();
                else txtLichsukham.Focus();
            }
        }
        private void ucNoichuyen_Enter(object sender, EventArgs e)
        {
            txtLichsukham.Focus();
        }

        private void txtLichsukham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLuu.Focus();
            }
        }


        private void btnLuu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //luu
                btnLuu_Click(null, null);
            }
        }
        private void btnNhapmoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Nhập mới
                btnNhapmoi_Click(null, null);
            }
        }
        private void btnChuyentuyen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Chuyển tuyến
                btnChuyentuyen_Click(null, null);
            }
        }
        private void btnSinhton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSinhton_Click(null, null);
            }
        }
        private void btnDsHenkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // ds hẹn khám
                btnDsHenkham_Click(null, null);
            }
        }

        #endregion

        #region SỰ KIỆN PHẦN TT KHÁM BỆNH
        string hideBARCODE = "";
        private void ucDoituong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucDoituong.SelectIndex > -1)
            {
                bool enable = true;
                if (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6") //BHYT
                {
                    enable = true;
                    ucTuyen_SelectedIndexChanged(null, null);
                    dtimeTungay.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    dtimeDenngay.DateTime = new DateTime(DateTime.Now.Year, 12, 31);

                    ucDKKCB.searchLookUpEdit.EditValue = Const.local_user.HOSPITAL_CODE;
                }
                else
                {
                    enable = false;
                    //ẩn
                    ucChuandoan.SelectedValue = "";
                    ucNoichuyen.SelectedValue = "";

                    ucChuandoan.Enabled = false;
                    ucNoichuyen.Enabled = false;
                    btnChuyentuyen.Enabled = false;
                    // ucChuandoan.CaptionValidate = false;
                    // ucNoichuyen.CaptionValidate = false;


                    //Thiết lập mặc định lại các control của BHYT
                    ucNoisong.SelectIndex = -1;
                    // Cấu hình mặc định
                    ckbCheckBHYT.Checked = dtCauHinh.Rows[0]["CHECK_BHYT_CONG"].ToString() == "1";
                    rbtCapCuu.EditValue = dtCauHinh.Rows[0]["CAPCUU"].ToString() == "1" ? "2" : "3"; // 2 là cấp cứu

                    txtSoThe.Text = "";
                    ckbTheTE.Checked = false;
                    ckbGiuBHYT.Checked = false;

                    txtMADOITUONGNGHEO.Text = "";
                    txtMuchuong.Text = "Ngoại (0%)-Nội (0%)";

                    cboMienGiam.SelectIndex = -1;
                    ucDKKCB.searchLookUpEdit.EditValue = Const.local_user.HOSPITAL_CODE;

                    ucTuyen.SelectIndex = 0;
                    ckbDu5n6t.Checked = false;
                    dtimeNgaydu.Text = "";

                    ucChuandoan.SelectedText = "";
                    ucChuandoan.SelectedValue = "";

                    ucNoichuyen.SelectedIndex = -1;

                    ucThukhac.SelectValue = "";

                    txtDcBaoHiem.Text = "";
                }


                ucNoisong.Enabled = enable; // labelControl20.Enabled =true; 
                txtSoThe.Enabled = enable; layoutControlItem2.Text = "Số thẻ" + (enable ? " <color=Red>(*)</color>" : ""); //labelControl19.Visible = true; 
                ckbCheckBHYT.Enabled = enable;

                ckbTheTE.Enabled = enable;

                ckbGiuBHYT.Enabled = enable; ckbGiuBHYT.Checked = enable;
                cboMienGiam.Enabled = enable;
                ucDKKCB.Enabled = enable; ucDKKCB.Option_CaptionValidate = enable;
                dtimeTungay.Enabled = enable; LCI_TuNgayBHYT.Text = "Từ ngày" + (enable ? " <color=Red>(*)</color>" : ""); //labelControl25.Visible = true; labelControl23.Enabled = true;
                dtimeDenngay.Enabled = enable; layoutControlItem11.Text = "Đến ngày" + (enable ? " <color=Red>(*)</color>" : ""); //labelControl26.Visible = true; labelControl24.Enabled = true;
                txtDcBaoHiem.Enabled = enable; LCI_DiaChiBH.Text = "Địa chỉ BH" + (enable ? " <color=Red>(*)</color>" : ""); //labelControl18.Visible = true; labelControl17.Enabled = true;
                ucThukhac.Enabled = enable;
                //txtMuchuong.Enabled = enable;
                ucTuyen.Enabled = enable; ucTuyen.CaptionValidate = enable;
                //labelControl28.Enabled = true;

                ckbDu5n6t.Enabled = enable; //labelControl29.Enabled = true;
                dtimeNgaydu.Enabled = enable;
                //labelControl30.Enabled = true; 


                // Chọn yêu cầu khám
                DataTable dt_yeucaukham = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_YeuCauKham, LOAD_YEUCAUKHAM_THEO_DT == "0" ? "0" : ucDoituong.SelectValue);
                if (dt_yeucaukham.Rows.Count > 0)
                {
                    DataRow dr = dt_yeucaukham.NewRow();
                    dr[0] = "";
                    dr[1] = "-- Chọn yêu cầu khám --";
                    dt_yeucaukham.Rows.InsertAt(dr, 0);
                }
                ucYeuCauKham.setData(dt_yeucaukham, "col1", "col2");
            }
        }
        private void ucYeuCauKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // xóa combo phòng khám cboPKham.clearData();
                if (ucYeuCauKham.SelectIndex >= 0)
                {
                    // Lấy ds các pk đang hoạt động - ko lưu cache
                    string ID = ucYeuCauKham.SelectValue.ToString();
                    DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV", new string[] { "[0]" }, new string[] { ID });

                    ucPhongKham.setData(dt, "col1", "col2");
                    ucPhongKham.SelectIndex = 0;
                    ucPhongKham.SelectTextSearch = "";
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void ucPhongKham_SelectedChanged(object sender, EventArgs e)
        {
            savePHONGID = ucPhongKham.SelectValue;
        }
        private void txtSoThe_Leave(object sender, EventArgs e)
        {
            string sobhyt = txtSoThe.Text.Trim().ToUpper();

            if (sobhyt.Length == 15 && sobhyt.IndexOf("_") < 0)
            {
                TimKiem_BenhNhan(sobhyt, "2");
            }
            else if (sobhyt.Length > 15 && sobhyt.IndexOf("|") > -1)
            {
                hideBARCODE = sobhyt;
                // DN401AA31900313|4cc3aa205669e1bb87742048c6b06e67|04/09/1985|1|437479207068e1baa76e206de1bb816d20564e50542d20434e2074e1bb956e6720437479204456205654|01 - 043|01/07/2017|31/12/2017|21/07/2017|01059710639215|-|4|01/07/2022|1f2fa22c47f0bd18-7102|$
                string[] sobhyt_catchuoi = sobhyt.Split('|');

                //{"func":"ajaxCALL_SP_O","params":["NGT01T002.TKMABHYT","DN401AA31900313$",0],"uuid":"730afe7f-703a-4333-b158-897b7bf37043"}
                DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NGT01T002.TKMABHYT", sobhyt_catchuoi[0].Trim() + "$", 0);
                if (dt.Rows.Count > 10)
                {
                    TimKiem_BenhNhan(sobhyt_catchuoi[0].Trim(), "2");
                }
                else
                {
                    txtSoThe.Text = sobhyt_catchuoi[0].Trim();
                    ucThongTinHanhChinh1.txtHoten.Text = Func.FromHex(sobhyt_catchuoi[1]);

                    if (sobhyt_catchuoi[2].Trim().Length > 4)
                        ucThongTinHanhChinh1.dtimeNgaysinh.EditValue = sobhyt_catchuoi[2].Trim();
                    else
                        ucThongTinHanhChinh1.txtNamsinh.Text = sobhyt_catchuoi[2].Trim();

                    dtimeTungay.EditValue = sobhyt_catchuoi[6];
                    dtimeDenngay.EditValue = sobhyt_catchuoi[7];

                    // set khu vực sinh sống.
                    string ma_kv = "0";
                    switch (sobhyt_catchuoi[11])
                    {
                        case "5": ma_kv = "1"; break;
                        case "6": ma_kv = "2"; break;
                        case "7": ma_kv = "3"; break;
                        default: ma_kv = "0"; break;
                    }
                    ucNoisong.SelectValue = ma_kv;

                    string matinh = sobhyt_catchuoi[0].Trim().Substring(3, 5);
                    ucThongTinHanhChinh1.ucTinh.SelectValue = matinh;

                    // $("#cboHC_TINHID").find("option[extval1='"+matinh+"']").attr("selected","selected");
                    // $("#txtTKHC_TINHID").val($('#cboHC_TINHID'+" option:selected").attr('extval0'));

                    string diachi = Func.FromHex(sobhyt_catchuoi[4]);
                    string[] sd = diachi.Split('-');

                    string mahuyen = sobhyt_catchuoi[0].Trim().Substring(5, 7);
                    ucThongTinHanhChinh1.ucHuyen.SelectValue = mahuyen;

                    ucThongTinHanhChinh1.txtDiaChiBN.Text = diachi;
                    txtDcBaoHiem.Text = diachi;

                    ucThongTinHanhChinh1.ucGioitinh.SelectValue = sobhyt_catchuoi[3] == "1" ? "1" : "2";


                    string noidk = sobhyt_catchuoi[5].Trim().Replace(" – ", "").Replace("-", "").Replace(" ", "").Replace(" ", "");
                    ucDKKCB.SelectedValue = noidk;
                    //$("#txtMA_KCBBD").val(noidk);
                    //$('#txtMA_KCBBD').combogrid("setValue",noidk);
                }
            }

            Lay_MucHuong_BHYT();

            checkMaTheKhoa();

            CheckMaHoNgheo();
            //popUpTrangThaiKham(); 
        }
        //private void popUpTrangThaiKham()
        //{
        //    if (_trangthaikhambenhcheck == "4")
        //    {
        //        DlgUtil.showMsg("Bệnh nhân đang khám.");
        //    }
        //    else if (_trangthaikhambenhcheck == "9")
        //    {
        //        DlgUtil.showMsg("Bệnh nhân đã khám xong.");
        //    }
        //}
        private void CheckMaHoNgheo()
        {
            try
            {
                string mabhyt = txtSoThe.Text.Trim().ToUpper();
                if (mabhyt.Substring(0, 2) == "GD" || mabhyt.Substring(0, 2) == "CN")
                { // hiển
                    cboMienGiam.Enabled = true;
                    txtMADOITUONGNGHEO.Enabled = true;
                    //$("#cboDOITUONGDB").prop('disabled', false);
                    //$("#txtMADOITUONGNGHEO").prop('disabled', false);
                }
                else
                { // mờ
                    cboMienGiam.Enabled = false;
                    txtMADOITUONGNGHEO.Enabled = false;
                    //$("#cboDOITUONGDB").prop('disabled', true);
                    //$("#txtMADOITUONGNGHEO").prop('disabled', true);
                }

                if (dtCauHinh.Rows[0]["BATCHECKNGHEO"].ToString() == "00"
                    || dtCauHinh.Rows[0]["BATCHECKNGHEO"].ToString() == ""
                    || dtCauHinh.Rows[0]["BATCHECKNGHEO"].ToString() == "0")
                {
                    return;
                }

                txtMADOITUONGNGHEO.Text = "";
                // Chi cho phep ma the: GD4 + matinh & CNx + matinh; 
                if (!(
                        (mabhyt.Substring(0, 5) == "GD4" + dtCauHinh.Rows[0]["BATCHECKNGHEO"].ToString())
                        || (mabhyt.Substring(0, 2) == "CN" && mabhyt.Substring(3, 2) == dtCauHinh.Rows[0]["BATCHECKNGHEO"].ToString())
                    ))
                {
                    return;
                }

                string dcbhyt = txtDcBaoHiem.Text.Trim().ToUpper();
                if (dcbhyt.Length < 14) { return; }

                string madtncn = dcbhyt.Substring(dcbhyt.Length - 3, dcbhyt.Length); 		// 3 ky tu cuoi; 
                string dsdtncn = dtCauHinh.Rows[0]["MAHONGHEO"].ToString();
                string[] list_dsdtncn = dsdtncn.Split(';');

                string strdoituongngheo = dcbhyt.Substring(dcbhyt.Length - 14, dcbhyt.Length);
                if (strdoituongngheo != "" && (list_dsdtncn[0].IndexOf(madtncn) != -1 || list_dsdtncn[1].IndexOf(madtncn) != -1))
                {
                    txtMADOITUONGNGHEO.Text = strdoituongngheo;
                    string dau_ma = mabhyt.Substring(0, 2);
                    // set combo Miễn giảm theo giá trị của đầu mã BHYT
                    DataTable dt = (DataTable)cboMienGiam.lookUpEdit.Properties.DataSource;
                    if (dt != null)
                        for (int i = 0; i < dt.Rows.Count; i++)
                            if (dt.Rows[i][3].ToString() == dau_ma)
                            {
                                cboMienGiam.SelectIndex = i;
                                break;
                            }
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void rbtCapCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lay_MucHuong_BHYT();
        }
        string hidMUCHUONG_NGT = "";
        string hidBHYT_DOITUONG_ID = "";
        private void Lay_MucHuong_BHYT()
        {
            // {"func":"ajaxCALL_SP_O","params":["COM.MUCHUONG.BHYT","27$DN4$1$1$3",0],"uuid":"730afe7f-703a-4333-b158-897b7bf37043"}
            try
            {
                string phongid = Const.local_khoaId + ""; // code js đổi thành khoa???

                //if (!(txtSoThe.Text.Length >= 14 && txtSoThe.Text.ToUpper().StartsWith("TE"))) return;
                string madoituong = txtSoThe.Text.Substring(0, 3).ToUpper();

                if (ucTuyen.SelectIndex < 0) return;
                string tuyen = ucTuyen.SelectValue.ToString(); ////BHYT_LOAIID": "1", đúng tuyến, 2 đúng tuyến gt, 3 đúng ccuu, 4 trái

                string hinhthucvaovienid = rbtCapCuu.EditValue.ToString();// cap cuu "2" ; khám thường "3";

                DataTable dt = ServiceTiepNhanBenhNhan.getMucHuong_BHYT(phongid, madoituong, tuyen, hinhthucvaovienid); ;
                if (dt.Rows.Count > 0)
                {
                    txtMuchuong.Text = "Ngoại (" + dt.Rows[0]["MUCHUONG_NGOAI"].ToString() + "%) - Nội ("
                        + dt.Rows[0]["MUCHUONG_NOI"].ToString() + "%)";
                    hidMUCHUONG_NGT = dt.Rows[0]["MUCHUONG_NGOAI"].ToString();
                    hidBHYT_DOITUONG_ID = dt.Rows[0]["BHYT_DOITUONG_ID"].ToString();
                    // // {"result": "[{\n\"MUCHUONG_NOI\": \"100\",\n\"MUCHUONG_NGOAI\": \"100\",\n\"BHYT_DOITUONG_ID\": \"98\"}]
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void ckbTheTE_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTheTE.Checked)
            {
                // đk sinh thẻ TE: <= 6 tuổi(đã bỏ?); nhập phần: ngày sinh; Huyện; Tỉnh.
                if (ucThongTinHanhChinh1.dtimeNgaysinh.DateTime == null || ucThongTinHanhChinh1.dtimeNgaysinh.Text == "")
                {
                    MessageBox.Show(Const.mess_erro_chuanhapngaysinh);
                    ckbTheTE.Checked = false;
                    ucThongTinHanhChinh1.dtimeNgaysinh.Focus();
                    return;
                }
                if (ucThongTinHanhChinh1.dtimeNgaysinh.DateTime < Func.getSysDatetime().AddYears(-6))
                {
                    MessageBox.Show("Bệnh nhân không phải đối tượng trẻ em dưới 6 tuổi");
                    ckbTheTE.Checked = false;
                    ucThongTinHanhChinh1.dtimeNgaysinh.Focus();
                    return;
                }

                DataRowView drvTinh = ucThongTinHanhChinh1.ucTinh.SelectDataRowView;
                if (drvTinh == null || drvTinh["col1"].ToString() == "-1")
                {
                    MessageBox.Show(Const.mess_erro_chuanhaptinh);
                    ckbTheTE.Checked = false;
                    ucThongTinHanhChinh1.ucTinh.Focus();
                    return;
                }
                DataRowView drvHuyen = ucThongTinHanhChinh1.ucHuyen.SelectDataRowView;
                if (drvHuyen == null || drvHuyen["col1"].ToString() == "-1")
                {
                    MessageBox.Show(Const.mess_erro_chuanhaphuyen);
                    ckbTheTE.Checked = false;
                    ucThongTinHanhChinh1.ucHuyen.Focus();
                    return;
                }

                // gọi lên SV sinh mã TE:

                string tinh = drvTinh["col4"].ToString();
                string huyen = drvHuyen["col4"].ToString();
                string ngay_sinh = "";

                string theTE = ServiceTiepNhanBenhNhan.sinhThe_BHYT(tinh, huyen, ngay_sinh);

                txtSoThe.Text = theTE;
                if (txtDcBaoHiem.Enabled == true) ucThongTinHanhChinh1.set_txtDcBN();
                dtimeTungay.DateTime = ucThongTinHanhChinh1.dtimeNgaysinh.DateTime;
                dtimeDenngay.DateTime = ucThongTinHanhChinh1.dtimeNgaysinh.DateTime.AddYears(6).AddDays(-1);

                ucDKKCB.SelectedValue = tinh + "000"; 

                Lay_MucHuong_BHYT();
            }
            else
            {
                txtSoThe.Text = "";
                txtDcBaoHiem.Text = "";
                dtimeTungay.Text = "";
                dtimeDenngay.Text = "";

                ucDKKCB.SelectedText = "";
                ucDKKCB.SelectedValue = ""; 
            }
        }
        private void ucTuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ucTuyen.SelectValue == "2")
                {
                    ucChuandoan.Enabled = true;
                    ucNoichuyen.Enabled = true;
                    btnChuyentuyen.Enabled = true;
                    ucChuandoan.Option_CaptionValidate = true;
                    ucNoichuyen.Option_CaptionValidate = true;
                }
                else
                {
                    ucChuandoan.SelectedValue = "";
                    ucNoichuyen.SelectedValue = "";

                    ucChuandoan.Enabled = false;
                    ucNoichuyen.Enabled = false;
                    btnChuyentuyen.Enabled = false;
                    // ucChuandoan.CaptionValidate = false;
                    // ucNoichuyen.CaptionValidate = false;
                }

                Lay_MucHuong_BHYT();
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void ucChuandoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)sender;
        }

        private void btnMoRongPK_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ucPhongKham.lookUpEdit.Properties.DataSource;
            if (dt != null && dt.Rows.Count > 0)
            {
                VNPT.HIS.CommonForm.NGT02K045_DanhSachPhongKhamYCK frm =
                    new VNPT.HIS.CommonForm.NGT02K045_DanhSachPhongKhamYCK(ucYeuCauKham.SelectValue.ToString(), ucYeuCauKham.SelectText, ucPhongKham.SelectValue);
                frm.setReturnData(NGT02K045_DanhSachPhongKhamYCK_ReturnData);
                openForm(frm, "1");
            }
        }
        private void NGT02K045_DanhSachPhongKhamYCK_ReturnData(object sender, EventArgs e)
        {
            string PK_ID = (string)sender;
            ucPhongKham.SelectValue = PK_ID;
        }


        #endregion

        #endregion


        #region Chức năng: Lưu, xóa, In, Đổi công khám,...
        // Lưu
        private bool KiemTra_Nhap()
        {
            if (ucThongTinHanhChinh1.KiemTraNhap() == false) return false;

            DateTime ngay_sinh = Func.getDatetime_Short(ucThongTinHanhChinh1.dtimeNgaysinh.DateTime);
            DateTime ngay_kham = Func.getDatetime_Short(dtimeNgaykham.DateTime);
            DateTime ngay_hientai = Func.getSysDatetime_Short();



            if (dtimeNgaykham.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập ngày khám");
                dtimeNgaykham.Focus();
                return false;
            }

            if (ngay_sinh > ngay_kham)
            {
                MessageBox.Show("Ngày sinh phải nhỏ hơn ngày khám");
                ucThongTinHanhChinh1.dtimeNgaysinh.Focus();
                return false;
            }

            if (ngay_kham > ngay_hientai)
            {
                MessageBox.Show("Thời gian khám không được lớn hơn thời gian hiện tại");
                dtimeNgaykham.Focus();
                return false;
            }

            if (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6")
                if (ngay_kham < ngay_hientai.Date.AddDays(-1 * _songayluiBHYT))
                {
                    MessageBox.Show("Ngày đến khám không được nhỏ hơn " + _songayluiBHYT + " ngày");
                    dtimeNgaykham.Focus();
                    return false;
                }

            if (ucThongTinHanhChinh1.txtTenNN.Text.Trim() == ""
                && (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6")
                && (ucThongTinHanhChinh1.cboTuoi.SelectedIndex != 0 || Func.Parse(ucThongTinHanhChinh1.txtTuoi.Text) < 7))
            {
                MessageBox.Show("Hãy nhập thông tin người nhà");
                ucThongTinHanhChinh1.txtTenNN.Focus();
                return false;
            }


            if (ucYeuCauKham.SelectIndex <= 0)
            {
                MessageBox.Show("Hãy nhập yêu cầu khám");
                ucYeuCauKham.Focus();
                return false;
            }
            if (ucPhongKham.SelectIndex < 0)
            {
                MessageBox.Show("Hãy nhập phòng khám");
                ucPhongKham.Focus();
                return false;
            }
            if (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6")
            {
                if (txtSoThe.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập Mã thẻ BHYT");
                    txtSoThe.Focus();
                    return false;
                }
                if (txtSoThe.Text.Trim().Length != 15)
                {
                    MessageBox.Show("Sai Mã thẻ BHYT");
                    txtSoThe.Focus();
                    return false;
                }
                if (dtimeTungay.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập Từ ngày BHYT");
                    dtimeTungay.Focus();
                    return false;
                }
                if (dtimeDenngay.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập đến ngày BHYT");
                    dtimeDenngay.Focus();
                    return false;
                }
                if (Func.getDatetime_Short(dtimeTungay.DateTime) > ngay_kham)
                {
                    MessageBox.Show("Thời gian BHYT chưa đến hạn");
                    dtimeTungay.Focus();
                    return false;
                }
                if (Func.getDatetime_Short(dtimeDenngay.DateTime) < ngay_kham)
                {
                    MessageBox.Show("Thời gian BHYT hết hạn");
                    dtimeDenngay.Focus();
                    return false;
                }
                if (Func.getDatetime_Short(dtimeTungay.DateTime) >= Func.getDatetime_Short(dtimeDenngay.DateTime))
                {
                    MessageBox.Show("Từ ngày phải nhỏ hơn đến ngày");
                    dtimeDenngay.Focus();
                    return false;
                }
                if (ucDKKCB.SelectedIndex < 0)
                {
                    MessageBox.Show("Hãy nhập Nơi ĐKKCB");
                    ucDKKCB.Focus();
                    return false;
                }
                if (txtDcBaoHiem.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập địa chỉ BHYT");
                    txtDcBaoHiem.Focus();
                    return false;
                }
            }
            if (BN_Sua != null && BN_Sua.Table.Columns.Contains("TRANGTHAIKHAMBENH") == true && BN_Sua["TRANGTHAIKHAMBENH"].ToString() != "1")
            {
                if (BN_Sua["TRANGTHAIKHAMBENH"].ToString() == "4") MessageBox.Show("Bệnh nhân đang khám, không thể cập nhật thông tin");
                else MessageBox.Show("Bệnh nhân kết thúc khám, không thể cập nhật thông tin");

                ucThongTinHanhChinh1.txtHoten.Focus();
                return false;
            }
            //if (ucNoisong.Enabled && ucNoisong.SelectIndex < 0)
            //{
            //    MessageBox.Show("Chưa có thông tin nơi sống.");
            //    ucNoisong.Focus();
            //    return false;
            //}

            if (ucTuyen.SelectValue == "2")
            {
                if (ucChuandoan.SelectedText == "")
                {
                    MessageBox.Show("Hãy nhập Chẩn đoán TD");
                    ucChuandoan.Focus();
                    return false;
                }
                if (ucNoichuyen.SelectedText == "")
                {
                    MessageBox.Show("Hãy nhập Nơi chuyển");
                    ucNoichuyen.Focus();
                    return false;
                }
            }

            return true;
        }
        private bool KiemTra_TrungThongTinBN()
        {
            // Kiểm tra trùng thông tin bệnh nhân
            if (BN_Sua == null) // chỉ dùng cho TH nhập mới
            {
                DataTable listBN = ServiceTiepNhanBenhNhan.check_TrungBenhNhan(ucThongTinHanhChinh1.txtHoten.Text.Trim()
                    , ucThongTinHanhChinh1.dtimeNgaysinh.DateTime.ToString(Const.FORMAT_date1), ucThongTinHanhChinh1.ucGioitinh.SelectValue);
                if (listBN.Rows.Count > 0)
                {
                    //ten : $("#txtTENBENHNHAN").val()==''?'-1':$("#txtTENBENHNHAN").val(), 										
                    //ngaysinh : $("#txtNGAYSINH").val()==''?'-1':$("#txtNGAYSINH").val(),
                    //gioitinh : $("#cboGIOITINHID").val(),
                    //mabhyt : '-1',
                    //type : '1' // ko cap nhat
                    NTU01H013_TimKiemBenhNhan frm = new NTU01H013_TimKiemBenhNhan("",
                        ucThongTinHanhChinh1.txtHoten.Text
                        , ucThongTinHanhChinh1.dtimeNgaysinh.DateTime.ToString(Const.FORMAT_date1)
                        , ucThongTinHanhChinh1.ucGioitinh.SelectValue
                        , "-1", "1"
                        );
                    frm.bLuu = true;
                    frm.setEvent_Return(ReturnForm_NTU01H013_TimKiemBenhNhan);
                    openForm(frm, "1");
                    return false;
                }
            }
            return true;
        }


        string Ktra_TheBHYT_maKQ = "";
        private bool KiemTra_TheBHYT()
        {
            // CAP NHAT CHECK CONG BHYT MOI THEO CONG VAN BYT-CNTT NGAY 31/1/2018
            // dòng 2064 đến 2140  --> fill kq bn tìm đc khi tìm kiếm trên cổng bhyt
            if (!
                (ckbCheckBHYT.Checked && ckbTheTE.Checked == false && (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6"))
                )
                return true;

            string namsinh = "";
            if (ucThongTinHanhChinh1.dtimeNgaysinh.DateTime == null || ucThongTinHanhChinh1.dtimeNgaysinh.Text == "")
                namsinh = ucThongTinHanhChinh1.dtimeNgaysinh.DateTime.ToString(Const.FORMAT_date1);
            else namsinh = ucThongTinHanhChinh1.txtNamsinh.Text.Trim();

            wsBHYT_LichSu_respons_2018 ret1 = ServiceBHYT.Get_History010118(
                   txtSoThe.Text.Trim(),
                   ucThongTinHanhChinh1.txtHoten.Text,
                   namsinh
                   );

            if (ret1 == null || ret1.maKetQua == null) // lỗi lấy thông tin
            {
                MessageBox.Show("Không có dữ liệu lịch sử KCB. Yêu cầu kiểm tra thông tin đầu vào.");
                return false;
            }

            bool the_sai_hoten_ngaysinh = ret1.maKetQua != "060" && ret1.maKetQua != "061" && ret1.maKetQua != "070";//  không sai=true; sai = false
            bool maKQ_rong_Hoac_theDen_rong = ret1.maKetQua != "" && ret1.gtTheDen != null && ret1.gtTheDen != "" && ret1.gtTheDen != "null";//  không rỗng=true; rỗng = false

            if (maKQ_rong_Hoac_theDen_rong == true) // không rỗng
            {
                ucThongTinHanhChinh1.ucGioitinh.SelectValue = ret1.gioiTinh.ToUpper() == "NAM" ? "1" : "2";
                dtimeTungay.Text = ret1.gtTheTu;
                dtimeDenngay.Text = ret1.gtTheDen;
            }
            string LayNoiDung = Func.LayNoiDungLoiCheckBHYT(ret1.maKetQua, "0");

            // check các trường hợp đặc biệt trả về từ cổng.  
            if (ret1.ghiChu.IndexOf("Token") != -1)
            {
                MessageBox.Show("Thông báo từ cổng BHXH " + ret1.maKetQua + " => "
                        + LayNoiDung + " => " + ret1.ghiChu + ". Thông điệp này không có trong công văn BHXH 2018.");
            }

            //string Ktra_TheBHYT_msg = ret1.maKetQua == "004" ? "" : LayNoiDung;            // ma 004 mac dinh dung;  

            // hop le, cho tiep don; 
            if (ret1.maKetQua == "004")
            {
                MessageBox.Show(ret1.ghiChu + ". Bệnh nhân sẽ được tiếp đón bình thường.");
                return true;
            }
            else if (ret1.maKetQua == "000")
            {
                return true;
            }

            // khong hop le, cap nhat lai roi cho tiep don
            if (dtCauHinh.Rows[0]["TUDONGFILLBHXH"].ToString() == "0"
                || maKQ_rong_Hoac_theDen_rong == false   // dl rỗng
                || the_sai_hoten_ngaysinh == true      // không sai tên/ngay sinh 
                )
            {
                MessageBox.Show(ret1.ghiChu);
                return false;
            }

            //  sai tên/NS --> Hỏi cap nhat lai roi cho tiep don
            DialogResult dialogResult = MessageBox.Show(ret1.ghiChu + "Bạn có muốn cập nhật lại thông tin từ Cổng BHXH ?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ucThongTinHanhChinh1.txtHoten.Text = ret1.hoTen.ToUpper();

                if (ret1.ngaySinh.Length == 4)
                {
                    ucThongTinHanhChinh1.dtimeNgaysinh.EditValue = null;
                    ucThongTinHanhChinh1.txtNamsinh.Text = ret1.ngaySinh;
                    //$("#txtNAMSINH").change();
                }
                else
                {
                    ucThongTinHanhChinh1.dtimeNgaysinh.Text = ret1.ngaySinh;
                    //$("#txtNGAYSINH").change();
                }
                return true;
            }
            else return false;
        }

        private bool KiemTra_DungTuyen()
        {
            string kcbbd = dtCauHinh.Rows[0]["TECHKCBBD"].ToString();
            if (kcbbd == "") kcbbd = "0";

            if (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6")
            {
                if (
                    (kcbbd == "0" && Const.local_user.HOSPITAL_CODE != ucDKKCB.SelectedValue && ucTuyen.SelectValue != "4")
                    || (kcbbd == "0" && Const.local_user.HOSPITAL_CODE == ucDKKCB.SelectedValue && ucTuyen.SelectValue == "4")
                    || (kcbbd != "0" && kcbbd.IndexOf(ucDKKCB.SelectedValue) == -1 && ucTuyen.SelectValue != "4")
                    || (kcbbd != "0" && kcbbd.IndexOf(ucDKKCB.SelectedValue) > -1 && ucTuyen.SelectValue == "4")
                    )
                {
                    // neu la doi tuong tre em, hoac co hen kham, luu luon; 
                    if (ckbTheTE.Checked ||dtCauHinh.Rows[0]["CHECKDUNGTUYEN"].ToString() == "1") // || $("#chkHENKHAM").prop('checked') == true 
                        return true;

                    DialogResult dialogResult = MessageBox.Show("Dữ liệu đúng tuyến/trái tuyến không hợp lệ. Có tiếp tục?", "", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool checkMaTheKhoa()
        {
            if (txtSoThe.Text.Trim() == "") return true;
            if (txtSoThe.Text.Trim().Length < 3)
            {
                MessageBox.Show("Mã đầu thẻ không tồn tại");
                txtSoThe.Focus();
                return false;
            }

            if (txtSoThe.Text.Length > 15)
            {
                MessageBox.Show("Mã thẻ không được quá 15 ký tự");
                txtSoThe.Focus();
                return false;
            }

            string parKhoa = txtSoThe.Text.Trim().ToUpper().Substring(0, 3);
            string resultKhoa = ServiceTiepNhanBenhNhan.checkMaTheKhoa(parKhoa);

            if (resultKhoa == "1")
            {
                MessageBox.Show("Mã đầu thẻ đã bị khóa");
                txtSoThe.Focus();
                return false;
            }

            if (resultKhoa == "2")
            {
                MessageBox.Show("Mã đầu thẻ không tồn tại");
                txtSoThe.Focus();
                return false;
            }

            return true;
        }

        private string checkSoLuongMaxPhongKham(string PhongKhamID)
        {
            // {"func":"ajaxCALL_SP_S","params":["COM.CAUHINH","NGT_CHECK_SOLUONG_PK_ALL"],"uuid":"0f30e2b7-0d32-4f51-8fba-0d1d5d9b3eef"}
            // {"result": "0","out_var": "[]","error_code": 0,"error_msg": ""}

            string cauhinh = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "NGT_CHECK_SOLUONG_PK_ALL");

            if ((ucDoituong.SelectValue == "2" || ucDoituong.SelectValue == "3") && cauhinh == "0")
                return "1";

            string rets = RequestHTTP.call_ajaxCALL_SP_S_result("CHECK.MAXPHONGKHAM", PhongKhamID + '$');
            return rets;
        }
        private BN_TiepNhan initDLBenhNhan()
        {
            BN_TiepNhan bn = new BN_TiepNhan();

            bn.USERID = Const.local_user.USER_ID;
            bn.KHOAID = Const.local_khoaId.ToString();
            bn.PHONGIDTIEPNHAN = Const.local_phongId.ToString(); 
            bn.PHONGID = Const.local_phongId.ToString();
            bn.TKPHONGID = bn.PHONGID;

            bn.MABENHNHAN = ucThongTinHanhChinh1.txtMaBN.Text;
            bn.TENBENHNHAN = ucThongTinHanhChinh1.txtHoten.Text;
            bn.NGAYSINH = ucThongTinHanhChinh1.dtimeNgaysinh.Text;
            bn.NAMSINH = ucThongTinHanhChinh1.txtNamsinh.Text;
            bn.TUOI = ucThongTinHanhChinh1.txtTuoi.Text;
            bn.DVTUOI = (ucThongTinHanhChinh1.cboTuoi.SelectedIndex + 1) + "";
            bn.SONHA = ucThongTinHanhChinh1.txtSoNha.Text;
            bn.SDTBENHNHAN = ucThongTinHanhChinh1.txtDienThoaiBN.Text;

            bn.GIOITINHID = ucThongTinHanhChinh1.ucGioitinh.SelectValue;
            bn.NGHENGHIEPID = ucThongTinHanhChinh1.ucNghenghiep.SelectValue.ToString();
            bn.DANTOCID = ucThongTinHanhChinh1.ucDantoc.SelectValue.ToString();
            bn.QUOCGIAID = ucThongTinHanhChinh1.ucQuoctich.SelectValue.ToString();
            bn.TENQUOCGIA = ucThongTinHanhChinh1.ucQuoctich.Text;

            if (ucThongTinHanhChinh1.ucTinh.SelectDataRowView != null)
                bn.HC_TINHID = ucThongTinHanhChinh1.ucTinh.SelectDataRowView["col1"].ToString();
            bn.TENTINH = ucThongTinHanhChinh1.ucTinh.Text;

            if (ucThongTinHanhChinh1.ucHuyen.SelectDataRowView != null)
                bn.HC_HUYENID = ucThongTinHanhChinh1.ucHuyen.SelectDataRowView["col1"].ToString();
            bn.TENHUYEN = ucThongTinHanhChinh1.ucHuyen.Text;

            if (ucThongTinHanhChinh1.ucXa.SelectDataRowView != null)
                bn.HC_XAID = ucThongTinHanhChinh1.ucXa.SelectDataRowView["col1"].ToString();
            bn.TENXA = ucThongTinHanhChinh1.ucXa.Text;


            bn.DIACHI = ucThongTinHanhChinh1.txtDiaChiBN.Text;
            bn.NOILAMVIEC = ucThongTinHanhChinh1.txtNoilamviec.Text;
            bn.TENNGUOITHAN = ucThongTinHanhChinh1.txtTenNN.Text;
            bn.DIACHINGUOITHAN = ucThongTinHanhChinh1.txtDiaChiNN.Text;
            bn.DIENTHOAINGUOITHAN = ucThongTinHanhChinh1.txtDienThoaiNN.Text;
            bn.NGAYTIEPNHAN = dtimeNgaykham.DateTime.ToString(Const.FORMAT_datetime1);
            bn.MA_BHYT = txtSoThe.Text;

            //    string text = txtDKKCB.Text;
            bn.MA_KCBBD = ucDKKCB.SelectedValue;
            bn.MAKCBBD = ucDKKCB.SelectedValue;
            bn.TEN_KCBBD = ucDKKCB.searchLookUpEdit.SelectedText;

            // Chỉ các TH BHYT cần, nếu ko bỏ qua cũng được
            bn.BHYT_BD = dtimeTungay.DateTime.ToString(Const.FORMAT_date1);
            bn.BHYT_KT = dtimeDenngay.DateTime.ToString(Const.FORMAT_date1);

            bn.DIACHI_BHYT = txtDcBaoHiem.Text;
             
            bn.NGAYDU5NAM = ckbDu5n6t.Checked ? "1" : "0";
            bn.CHANDOANTUYENDUOI = ucChuandoan.SelectedText;


            bn.DT_SINHSONG = ucNoisong.SelectValue;

            bn.DICHVUID = ucYeuCauKham.SelectValue;
            bn.TKDICHVUID = bn.DICHVUID;
            bn.DICHVUKHAMID = bn.DICHVUID;

            bn.YEUCAUKHAM = ucYeuCauKham.Text;

            bn.PHONGKHAMID = ucPhongKham.SelectValue;

            bn.DOITUONGBENHNHANID = ucDoituong.SelectValue == null ? "" : ucDoituong.SelectValue.ToString(); // bhyt / viện phí,...
            
            bn.DOITUONGDB = cboMienGiam.SelectValue == null ? "" : cboMienGiam.SelectValue.ToString();  // id của đt miễn giảm
            bn.MUCHUONG_NGT = hidMUCHUONG_NGT;// trả về khi tính mức hưởng
            bn.MUCHUONG = txtMuchuong.Text;
            bn.BHYT_DOITUONG_ID = hidBHYT_DOITUONG_ID;// trả về khi tính mức hưởng
            //bn.TYLE_MIENGIAM = "15";

            bn.DVTHUKHAC = ucThukhac.SelectValue;
            bn.BHYT_LOAIID = ucTuyen.SelectValue;//đúng tuyến...
            bn.TKBHYT_LOAIID = bn.BHYT_LOAIID;

            bn.MANOIGIOITHIEU = ucNoichuyen.SelectedValue;
            bn.TKMANOIGIOITHIEU = bn.MANOIGIOITHIEU;
            bn.TENNOIGIOITHIEU = ucNoichuyen.searchLookUpEdit.SelectedText;

            //bn.TKMACHANDOANTUYENDUOI =  ; ko cần vì chuẩn đoạn là 1 list

            //bn.LS = txtLichsukham.Text; Ko cần

            // Các biến truyền lại khi Sửa

            bn.TIEPNHANID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("TIEPNHANID") == true) ? "" : BN_Sua["TIEPNHANID"].ToString();

            bn.BENHNHANID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("BENHNHANID") == true) ? "" : BN_Sua["BENHNHANID"].ToString();

            bn.KHAMBENHID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("KHAMBENHID") == true) ? "" : BN_Sua["KHAMBENHID"].ToString();

            bn.HOSOBENHANID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("HOSOBENHANID") == true) ? "" : BN_Sua["HOSOBENHANID"].ToString();
            // bn.PHONGID_CU = benhnhan == null ? "" :  
            bn.LOAITIEPNHANID = "1";// 0 nội, 1 ngoại trú

            bn.TRANGTHAIKHAMBENH = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("TRANGTHAIKHAMBENH") == true) ? "1" : BN_Sua["TRANGTHAIKHAMBENH"].ToString();// 1 chờ khám

            bn.MABENHAN = (dtKHAMBENH == null || dtKHAMBENH.Rows.Count == 0 || dtKHAMBENH.Columns.Contains("MABENHAN") == false) ? ""
                : dtKHAMBENH.Rows[0]["MABENHAN"].ToString();  // mã bệnh án

            bn.PHONGKHAMDANGKYID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("PHONGKHAMDANGKYID") == true) ? "" : BN_Sua["PHONGKHAMDANGKYID"].ToString();

            bn.MAUBENHPHAMID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("MAUBENHPHAMID") == true) ? "" : BN_Sua["MAUBENHPHAMID"].ToString();

            bn.DICHVUKHAMBENHID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("DICHVUKHAMBENHID") == true) ? "" : BN_Sua["DICHVUKHAMBENHID"].ToString();


            //    //bn.BHYTID =// bỏ
            //    //bn.NOICHUYENID = ""; bỏ


            DataRowView dr = ucThongTinHanhChinh1.ucTinhHuyenXa.SelectedDataRowView;
            bn.DIAPHUONGID = dr == null ? "" : dr["VALUE"].ToString();

            // ưu tiên Lấy mã của đơn vị (tỉnh, huyện, xã) nhỏ nhất nếu như nó khác rỗng
            DataRowView drXa = ucThongTinHanhChinh1.ucXa.SelectDataRowView;
            if (drXa != null) bn.BNDIAPHUONGID = drXa["col1"].ToString();
            else
            {
                DataRowView drHuyen = ucThongTinHanhChinh1.ucHuyen.SelectDataRowView;
                if (drHuyen != null) bn.BNDIAPHUONGID = drHuyen["col1"].ToString();
                else
                {
                    DataRowView drTinh = ucThongTinHanhChinh1.ucTinh.SelectDataRowView;
                    if (drTinh != null) bn.BNDIAPHUONGID = drTinh["col1"].ToString();
                }
            }

            bn.SINHTHEBHYT = ckbTheTE.Checked ? "1" : "0";
            bn.DAGIUTHEBHYT = ckbGiuBHYT.Checked ? "1" : "0";

            bn.UUTIENKHAMID = ckbUuTien.Checked ? "1" : "0";
            if (bn.UUTIENKHAMID == "1") bn.UUTIENKHAMID = "3";

            bn.DU5NAM6THANGLUONGCOBAN = ckbDu5n6t.Checked ? "1" : "0";
            bn.TRADU6THANGLCB = ckbDu5n6t.Checked ? "1" : "0";
            bn.HINHTHUCVAOVIENID = rbtCapCuu.EditValue.ToString();// 2 cấp cuus, 3 thuong

            // Chuyển viện 
            if (dtChuyenVien.Rows.Count > 0)
            {
                bn.CV_CHUYENVIEN_HINHTHUCID = dtChuyenVien.Rows[0]["ucHinhThucChuyen"].ToString();
                bn.CV_CHUYENVIEN_LYDOID = dtChuyenVien.Rows[0]["ucLyDoChuyen"].ToString();

                bn.CV_CHUYENDUNGTUYEN = dtChuyenVien.Rows[0]["rbtChuyen"].ToString();
                bn.CV_CHUYENVUOTTUYEN = bn.CV_CHUYENDUNGTUYEN == "1" ? "0" : "1";
            }

            bn.BARCODE = hideBARCODE;

            // Check sinh thẻ TE hay ko?
            bn.COGIAYKS = ckbTheTE.Checked ? "1" : "0";

            bn.DTBNID = ucDoituong.SelectValue == null ? "" : ucDoituong.SelectValue.ToString();
            bn.CHECKBHYTDV = "0"; 
            if (bn.DTBNID == "6")
            {
                bn.DTBNID = "1";
                bn.CHECKBHYTDV = "1";
            }

            //    //bn.LICHHENID = "";
            //    //bn.TRANGTHAIDICHVU = ""; 
            //    //bn.DIABANID = "";// ko dùng
            //    //bn.MACHANDOANTUYENDUOI =  ;//  ko dùng DVTHUKHAC
            //    //bn.KHAMBENH_MACH = "";
            //    //bn.KHAMBENH_NHIETDO = "";
            //    //bn.KHAMBENH_HUYETAP_LOW = "";
            //    //bn.KHAMBENH_HUYETAP_HIGH = "";
            //    //bn.KHAMBENH_NHIPTHO = "";
            //    //bn.KHAMBENH_CANNANG = "";
            //    //bn.KHAMBENH_CHIEUCAO = "";


            bn.CHECKCONG = ckbCheckBHYT.Checked ? "1" : "0";
            bn.URLWEBSITE = dtCauHinh.Rows[0]["URLWEBSITE"].ToString();
            bn.TECHPASS = dtCauHinh.Rows[0]["TECHPASS"].ToString();
            bn.TECHUSER = dtCauHinh.Rows[0]["TECHUSER"].ToString();
            // bn.GHICHU = "Check cổng " + ckbCheckBHYT.Checked.ToString(); sẽ đc gán sau khi check cổng byt


            bn.TENQUAY = lbQuay.Text;
            bn.STT_BD1 = txtstt_bd1.Text; // bv NT
            bn.STT_KT1 = txtstt_kt1.Text; // bv NT
            //bn.STTKHAM4	="1"; //bv bưu điện
            //bn.STT_KT	="1"; // bv cũ
            //bn.STT_BD	="1"; // bv cũ
            //bn.SL_GOI	="1";
            //SOTHUTUCLSID :"" 


            bn.KCBBD = dtCauHinh.Rows[0]["TECHKCBBD"].ToString();
            //bn.BHYT_LOAIID	="1"; 
            bn.MAHONGHEO = txtMADOITUONGNGHEO.Text;
            //bn.INDEX	="1";

            // Phần Hợp đồng: có cbo Hợp đồng, khởi tạo khi load form nếu được thiết lập khi gọi.
            bn.HOPDONGID = ucHopDong.SelectValue;



            return bn;
        }
        private void LuuBN(BN_TiepNhan bn)
        {
            string ret = ServiceTiepNhanBenhNhan.submitBenhNhanTiepNhan(bn);
            string[] retArr = ret.Split(',');

            if (retArr.Length > 1) //thành công
            {
                saveKHAMBENHID = retArr[0];

                if (dtCauHinh.Rows.Count > 0 && dtCauHinh.Rows[0]["INPHIEU"].ToString() == "1")  // Cấu hình của hệ thống
                {
                    if (dtCauHinh.Rows[0]["TUDONGINPHIEUKHAM"].ToString() == "1") // Cấu hình của người dùng
                        InPhieu_Luon(); // In luôn
                    else
                        InPhieu_Preview(); // mở preview ra.
                }

                DialogResult dlResult = MessageBoxEx.Show(Const.mess_tiepnhan_suss + (dtCauHinh.Rows[0]["TRAVEMABENHAN"].ToString() == "1" ? (" Mã bệnh án: " + retArr[9]) : ""), 10000);
                if (dlResult == System.Windows.Forms.DialogResult.OK)
                {

                }

                //Sau 10 giây reset lại
                btnNhapmoi_Click(null, null);

                //$('#hidKHAMBENHID').val(rets[0]);
                //$('#txtMABENHNHAN').val(rets[1]);
                //$('#hidPHONGKHAMDANGKYID').val(rets[2]);
                //$('#hidTIEPNHANID').val(rets[3]);
                //$('#hidHOSOBENHANID').val(rets[4]);
                //$('#hidBENHNHANID').val(rets[5]);
                //$('#hidBHYTID').val(rets[6]);
                //$('#hidMAUBENHPHAMID').val(rets[7]);	
                //$('#hidDICHVUKHAMBENHID').val(rets[8]);
            }
            else //lỗi
            {
                if (ret == "the_ko_hop_le")
                {
                    //setErrValidate('txtMA_BHYT');
                    MessageBox.Show("Thẻ BHYT không hợp lệ");

                }
                else if (ret == "trung_the")
                {
                    //setErrValidate('txtMA_BHYT");
                    MessageBox.Show("Trùng thẻ BHYT");
                }
                else if (ret == "da_tiepnhan_pk")
                {
                    //setErrValidate('txtMA_BHYT");
                    MessageBox.Show("Bệnh nhân đã tiếp nhận vào phòng khám trong ngày");
                }
                else if (ret == "dakhamtrongngay")
                {
                    //setErrValidate('txtMA_BHYT");
                    MessageBox.Show("Bệnh nhân đã đăng ký khám trong ngày");
                }
                else if (ret == "kocodvcon")
                {
                    MessageBox.Show("Không có dịch vụ con trong goi");
                }
                else if (ret == "vi_pham_tt_kham")
                {
                    MessageBox.Show("Bệnh nhân đang khám hoặc đang điều trị, không tiếp nhận lại được");
                }
                else if (ret == "dvdathanhtoan")
                {
                    MessageBox.Show("Dịch vụ yêu cầu đã thanh toán, phải hủy hóa đơn nếu muốn cập nhật dịch vụ khác");
                }
                else if (ret == "loitenbn")
                {
                    ucThongTinHanhChinh1.txtHoten.Focus();
                    MessageBox.Show("Tên bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại");
                }
                //else if (ret == "loidiachibn")
                //{
                //    ucThongTinHanhChinh1.txtDcBN.Focus();
                //    MessageBox.Show("Địa chỉ bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại");
                //}
                else if (ret == "loidiachibhyt")
                {
                    txtDcBaoHiem.Focus();
                    MessageBox.Show("Địa chỉ BHYT bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại");
                }
                else if (ret == "kocapnhatbncu")
                {
                    MessageBox.Show("Không thể cập nhật lại thông tin bệnh nhân tiếp đón ngày hôm trước");
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin không thành công");
                }
            }


        }
        private void btnLuu_Click(object sender, EventArgs e)
        { 
            //kiểm tra  
            if (KiemTra_TrungThongTinBN() == false) return;

            if (KiemTra_TheBHYT() == false) return;

            if (KiemTra_Nhap() == false) return;

            Tiep_Tuc_Luu_Sau_KTra_Trung();
        }
        private void Tiep_Tuc_Luu_Sau_KTra_Trung()
        {
            if (KiemTra_DungTuyen() == false) return;

            if (checkMaTheKhoa() == false) return;

            if (checkSoLuongMaxPhongKham(ucPhongKham.SelectValue) == "-1")
            {
                MessageBox.Show("Phòng khám hết số");
                return;
            }

            //submit         
            BN_TiepNhan bn = initDLBenhNhan();

            // Day len cong BYT de check thong tin lam dung the;
            if (wsBYT_check_lam_dung_the() == false) return;

            bn.GHICHU = "BHXH Check:  " + ckbCheckBHYT.Checked.ToString()
                + ";BYT DAYDL: " + ServiceBYT.ServiceBYT_BYTDAYDL
                             + ";BYT LDT: " + transidLAMDUNGTHE;

            LuuBN(bn);

            // nếu lưu thành công đẩy tt lên coongt BYT
            wsBYT_day_thong_tin_tiep_nhan(bn);
        } 
        string transidLAMDUNGTHE = "";
        
        private bool wsBYT_check_lam_dung_the()
        {
            ServiceBYT.Lay_thong_tin_ws_BYT();

            if (ServiceBYT.ServiceBYT_BYTSTOPCHUCNANG != "1") return true;

            try
            {
                // chỗ nào code js gọi _capnhat4(objData, param_arr_pk);  thì return true;

                if (! (ServiceBYT.ServiceBYT_BYTDAYDL == "1"
                    && (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6")) )
                    return true;

                // code js từ dòng: 2315 đến 2357

                var objLAMDUNGTHE = new
                {
                    MABENHVIEN = ServiceBYT.ServiceBYT_MACSYT,
                    MA_THE = txtSoThe.Text,
                    NGAY_SINH = (ucThongTinHanhChinh1.dtimeNgaysinh.EditValue == null || ucThongTinHanhChinh1.dtimeNgaysinh.Text.Trim() == "") ?
                            (ucThongTinHanhChinh1.txtNamsinh.Text + "0101") : (ucThongTinHanhChinh1.dtimeNgaysinh.DateTime.ToString("yyyyMMdd")),// hoac nam sinh ; 
                    GIOI_TINH = ucThongTinHanhChinh1.ucGioitinh.SelectValue
                };

                var objHeader = new
                {
                    Message_Version = "1.0",
                    Sender_Code = ServiceBYT.ServiceBYT_MACSYT,
                    Sender_Name = "",
                    Transaction_Type = "M0005",
                    Transaction_Name = "",
                    Transaction_Date = "",
                    Transaction_ID = "",
                    Request_ID = "",
                    //Action_Type = "" bỏ vì code trên web ko đc gán trị trị nào undefined // 0: bắt đầu khám, 1: kết thúc khám
                };

                //var obj3 = XML_BYT_TaoKhung(objHeader, objLAMDUNGTHE, "2"); // tao JSON full => XML
                var obj3 = ServiceBYT.XML_BYT_TaoKhung(objHeader, objLAMDUNGTHE, "2");

                var resultCongBYT = ServiceBYT.tc_ls_KCB("CongDLYTWS", "lamdungthe",
                               new String[] {
                            "_usr",
                            "_pwd",
                            "xmlData" },
                               new String[] {
                            ServiceBYT.ServiceBYT_Username,
                            ServiceBYT.ServiceBYT_Password,
                            Convert.ToBase64String(Encoding.UTF8.GetBytes(obj3.ToString())) });

                if (resultCongBYT.Length <= 0)
                {
                    MessageBox.Show("Gửi thông tin Cổng BYT thất bại, yêu cầu kiểm tra lại thông tin.");
                    return false;
                }

                var rets = resultCongBYT.Split(';');
                // luu transid lam dung the;
                if (rets.Length > 2) transidLAMDUNGTHE = rets[2];

                // benh nhan k lam dung the
                if (rets[0] == "0") return true;

                // benh nhan lam dung the, xac nhan luu thong tin ?  
                var sttr = "";
                for (int i = 0; i < rets.Length; i++)
                    if (i != 0 && i != 2) sttr += rets[i];    // 0 ma loi; 2 transid loi;  

                DialogResult Result = MessageBox.Show("Cổng BYT: [" + sttr + "]" + ". Bạn có muốn tiếp nhận bệnh nhân này?", "", MessageBoxButtons.YesNo);
                if (Result == DialogResult.Yes) return true;
                else
                {
                    MessageBox.Show("Không lưu thông tin do thao tác hủy của người dùng.");
                    return false;
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
            return false;
        }

        private void wsBYT_day_thong_tin_tiep_nhan(BN_TiepNhan bn)
        {
            ServiceBYT.Lay_thong_tin_ws_BYT();
            try
            {
                //code js từ dòng: 2376 --> 2400
                //============= Day len cong BYT thong tin tiep nhan moi;
                if (ServiceBYT.ServiceBYT_BYTDAYDL == "1")
                {
                    var actionType = bn.BENHNHANID == "" ? "0" : "1";
                    //var objFirst = createObjectCHECKIN(rets[4], "001", i_u1, actionType);// tao dau vao day du du lieu

                    var objHeader = new
                    {
                        Message_Version = "1.0",
                        Sender_Code = ServiceBYT.ServiceBYT_MACSYT,
                        Sender_Name = "",
                        Transaction_Type = "M0001",
                        Transaction_Name = "",
                        Transaction_Date = "",
                        Transaction_ID = "",
                        Request_ID = "",
                        Action_Type = actionType// 0: bắt đầu khám, 1: kết thúc khám
                    };

                    var MA_THE = string.Empty;
                    var MA_KCBBD = string.Empty;
                    var TU_NGAY = string.Empty;
                    var DEN_NGAY = string.Empty;
                    var NGAYDU5NAM = string.Empty;
                    var TINHTRANGVAOVIEN = string.Empty;
                    //$('#cboHC_TINHID' + " option:selected").attr('extval1') + $('#cboHC_HUYENID' + " option:selected").attr('extval1');
                    var MATINHQUANHUYEN = bn.HC_TINHID + bn.HC_HUYENID;
                    var MA_NOICHUYENDEN = bn.BHYT_LOAIID == "2" ? bn.MANOIGIOITHIEU : "";

                    if (bn.HINHTHUCVAOVIENID == "2") TINHTRANGVAOVIEN = "2";          // Tình trạng vào viện (1: Đúng tuyến , 2: Cấp cứu, 3 : Trái tuyến)                     
                    else if (bn.BHYT_LOAIID == "1" || bn.BHYT_LOAIID == "2" || bn.BHYT_LOAIID == "3") TINHTRANGVAOVIEN = "1";
                    else TINHTRANGVAOVIEN = "3"; 

                    if (ucDoituong.SelectValue == "1" || ucDoituong.SelectValue == "6")
                    {
                        MA_THE = bn.MA_BHYT;
                        MA_KCBBD = bn.MA_KCBBD;
                        TU_NGAY = bn.BHYT_BD;
                        DEN_NGAY = bn.BHYT_KT;
                        NGAYDU5NAM = bn.NGAYDU5NAM;
                        // Mã tỉnh quận huyện (2 ký tự mã tỉnh + 2 ký tự mã huyện trên thẻ BHYT)
                        MATINHQUANHUYEN = !string.IsNullOrEmpty(bn.MA_BHYT) ? bn.MA_BHYT.Substring(3, 7) : "";
                    }

                    var objCHECKIN = new
                    {
                        MA_LK = bn.MABENHNHAN,
                        MABENHVIEN = ServiceBYT.ServiceBYT_MACSYT,
                        HO_TEN = Func.addSpecialElement(bn.TENBENHNHAN, "CDATA"),
                        NGAY_SINH = bn.NGAYSINH,
                        NAM_SINH = bn.NAMSINH,
                        GIOI_TINH = bn.GIOITINHID,
                        DIA_CHI = Func.addSpecialElement(bn.DIACHI, "CDATA"),
                        MA_THE = MA_THE,
                        MA_KCBBD = MA_KCBBD,
                        TU_NGAY = TU_NGAY,
                        DEN_NGAY = DEN_NGAY,
                        NGAYDU5NAM = NGAYDU5NAM,
                        MATINHQUANHUYEN = MATINHQUANHUYEN,
                        NGAYGIOVAO = Func.getSysDatetime("YYYYMMDDHH24MI"),// 201603091521
                        MA_NOICHUYENDEN = bn.MANOIGIOITHIEU,
                        LYDODENKHAM = Func.addSpecialElement(ucYeuCauKham.SelectText, "CDATA"),
                        TINHTRANGVAOVIEN = TINHTRANGVAOVIEN,// Tình trạng vào viện (1: Đúng tuyến , 2: Cấp cứu, 3 : Trái tuyến)
                        SOKHAMBENH = "001",
                        SODIENTHOAI_LH = Func.addSpecialElement(bn.SDTBENHNHAN, "CDATA"),
                        NGUOILIENHE = Func.addSpecialElement(bn.TENNGUOITHAN, "CDATA"),
                        MA_KHUVUC = ucNoisong.SelectValue == "0" ? "" : ucNoisong.SelectValue,
                        NGAYGIORA = "",            // 201603091521
                        MA_LOAI_KCB = "1",        // 1: Khám bệnh, 2: Điều trị ngoại trú, 3: Điều trị nội trú
                        TEN_BENH = "",
                        MA_BENH = "",                // Mã bệnh chính theo ICD 10
                        MA_BENHKHAC = "",        // Mã bệnh kèm theo theo ICD 10, có nhiều mã ICD được phân cách bằng ký tự chấm phẩy
                        CHANDOAN = "",
                        NGAYHETTHUOC = "",
                        NGAYGIOKHAM = "",
                        LYDO_CHUYEN = "",
                        NGAY_CHUYENTUYEN = "",
                        KHAMDIEUTRITAI = "",
                        GT_THE_DEN = "",
                        GT_THE_TU = "",
                        SOHOSO = "", 
                        TENCSKCBDEN = "",
                        MACSKCBDEN = "",
                        TENCSKCBDI = "",
                        MACSKCBDI = ""
                    };

                    var obj3 = ServiceBYT.XML_BYT_TaoKhung(objHeader, objCHECKIN, "1");

                    var resultCongBYT = ServiceBYT.tc_ls_KCB("CongDLYTWS", "guiHSNV",
                               new String[] {
                            "_usr",
                            "_pwd",
                            "csytid",
                            "matinh",
                            "slhs",
                            "xmlData" },
                               new String[] {
                            ServiceBYT.ServiceBYT_Username,
                            ServiceBYT.ServiceBYT_Password,
                            ServiceBYT.ServiceBYT_MACSYT,
                             "01",
                             "1",
                            Convert.ToBase64String(Encoding.UTF8.GetBytes(obj3.ToString())) });

                    // 82023/123a@
                    if (resultCongBYT.Length > 0)
                    {
                        var arr = resultCongBYT.Split(';');
                        if (arr[0] != "0")
                        {
                            MessageBox.Show("Cổng BYT: " + arr[1]);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gửi thông tin Cổng BYT thất bại, yêu cầu kiểm tra lại thông tin. ");
                    }
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void btnNhapmoi_Click(object sender, EventArgs e)
        {
            reset_value();
            set_defaul_value();
        }

        private NGT02K054_ChuyenYCKham frm_NGT02K054_ChuyenYCKham = null;

        private NGT01T001_chuyenvien frm_NGT01T001_chuyenvien = null;
        DataTable dtChuyenVien;
        private void btnChuyentuyen_Click(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (frm_NGT01T001_chuyenvien == null)
                {
                    frm_NGT01T001_chuyenvien = new NGT01T001_chuyenvien();
                    frm_NGT01T001_chuyenvien.setReturnData(ReturnForm_NGT01T001_chuyenvien);
                }

                dtChuyenVien.Rows[0]["ucBenhVien"] = ucNoichuyen.SelectedValue;
                dtChuyenVien.Rows[0]["ucCDTD"] = ucChuandoan.SelectedText;
                frm_NGT01T001_chuyenvien.setData(dtChuyenVien);

                openForm(frm_NGT01T001_chuyenvien, "1");
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void ReturnForm_NGT01T001_chuyenvien(object sender, EventArgs e)
        {
            dtChuyenVien = (DataTable)sender;
            ucNoichuyen.SelectedValue = dtChuyenVien.Rows[0]["ucBenhVien"].ToString();
            ucChuandoan.SelectedText = dtChuyenVien.Rows[0]["ucCDTD"].ToString();
        }


        private void btnSinhton_Click(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (dtKHAMBENH.Rows.Count > 0)
                {

                    NGT01T001_sinhton frm = new NGT01T001_sinhton();
                    frm.setParam(Const.drvBenhNhan["KHAMBENHID"].ToString(), dtKHAMBENH.Rows[0]["TRANGTHAIKHAMBENH"].ToString());
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }

        }

        private void btnDsHenkham_Click(object sender, EventArgs e)
        {
            NTU01H013_TimKiemBenhNhan frm = new NTU01H013_TimKiemBenhNhan("1", "", "", "", "", "");
            frm.setEvent_Return(ReturnForm_NTU01H013_TimKiemBenhNhan);
            openForm(frm, "1");
        }
        private void ReturnForm_NTU01H013_TimKiemBenhNhan(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)sender;
            if (drv != null)
                TimKiem_BenhNhan(drv["MABENHNHAN"].ToString(), "1");
            else // TH lưu BN: nếu bỏ qua thì vẫn lưu
                Tiep_Tuc_Luu_Sau_KTra_Trung();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnDichVuCLS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    getChiTiet_KhamBenh(Const.drvBenhNhan["KHAMBENHID"].ToString());

                    if (dtKHAMBENH.Rows.Count > 0)
                    {
                        NGT02K016_ChiDinhDichVu frm = new NGT02K016_ChiDinhDichVu();
                        frm.loadData("dich vu cls", dtKHAMBENH.DefaultView[0], "0", "5");
                        frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                        openForm(frm, "1");
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        
        private void btnThuKhac_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    getChiTiet_KhamBenh(Const.drvBenhNhan["KHAMBENHID"].ToString());

                    if (dtKHAMBENH.Rows.Count > 0)
                    {
                        NGT02K016_ChiDinhDichVu frm = new NGT02K016_ChiDinhDichVu();
                        frm.loadData("thu khac", dtKHAMBENH.DefaultView[0], null, "1");
                        frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                        openForm(frm, "1");
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnHoaHong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    getChiTiet_KhamBenh(Const.drvBenhNhan["KHAMBENHID"].ToString());

                    if (dtKHAMBENH.Rows.Count > 0)
                    {
                        NGT01T006_hoahong frm = new NGT01T006_hoahong(dtKHAMBENH.Rows[0]["KHAMBENHID"].ToString(), dtKHAMBENH.Rows[0]["MABENHAN"].ToString());
                        openForm(frm, "1");
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnChuyenPhongKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Chuyen_Them_Pk("0");
        }
        private void btnThemPhongKham_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Chuyen_Them_Pk("2");
        }
        private void Chuyen_Them_Pk(string kieu)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    //        kieu : kieu, //thêm phòng
                    //khambenhid : $('#hidKHAMBENHID').val(),
                    //dichvuid : $('#cboDICHVUID').val(),
                    //phongid : $('#cboPHONGKHAMID').val(),
                    //phongkhamdangkyid : $('#hidPHONGKHAMDANGKYID').val()

                    getChiTiet_KhamBenh(Const.drvBenhNhan["KHAMBENHID"].ToString());

                    if (dtKHAMBENH.Rows.Count > 0)
                    {
                        NGT01T004_Tiepnhan_Chuyenphongkham frm = new NGT01T004_Tiepnhan_Chuyenphongkham();
                        frm.setKhamBenhID(kieu, dtKHAMBENH.Rows[0]["KHAMBENHID"].ToString(), dtKHAMBENH.Rows[0]["DICHVUID"].ToString()
                            , dtKHAMBENH.Rows[0]["PHONGID"].ToString(), dtKHAMBENH.Rows[0]["PHONGKHAMDANGKYID"].ToString());
                        openForm(frm, "1");
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void openForm(Form frm, string optionsPopup)
        {
            // optionsPopup==1 kiểu popup
            // optionsPopup==0 kiểu toàn màn hình
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btnPhieuKhamBenh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InPhieu_Luon(); // In luôn
        }
        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            InPhieu_Preview(); // Xem và In
        }

        private void btnXemInPhieuKhamBenh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InPhieu_Preview(); // Xem và In
        }

        private void InPhieu_Luon()
        {
            if (saveKHAMBENHID == "")
            {
                MessageBox.Show("Hãy chọn bệnh nhân muốn in phiếu");
                return;
            }
            else if (savePHONGID == "")
            {
                MessageBox.Show("Hãy chọn phòng khám");
                return;
            }

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("khambenhid", "String", saveKHAMBENHID);
                table.Rows.Add("phongid", "String", savePHONGID);
                table.Rows.Add("i_sch", "String", "HIS_DATA2");

                // "pdf" --> "pdf"
                // "doc" --> "rtf"  
                Func.Print_Luon(table, "NGT_STT");  
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void InPhieu_Preview()
        {
            if (saveKHAMBENHID == "")
            {
                MessageBox.Show("Hãy chọn bệnh nhân muốn in phiếu");
                return;
            }
            else if (savePHONGID == "")
            {
                MessageBox.Show("Hãy chọn phòng khám");
                return;
            }

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("khambenhid", "String", saveKHAMBENHID);
                table.Rows.Add("phongid", "String", savePHONGID);
                table.Rows.Add("i_sch", "String", "HIS_DATA2");

                VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint("In phiếu", "NGT_STT", table);
                openForm(frm, "1");
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }


        private void btnLStheoBHYT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                string ma_the = "";
                string ho_ten = "";
                string ngay_sinh = "";
                string ngay_bd = "";
                string ngay_kt = "";
                string ma_CSKCB = "";
                string gioi_tinh = "";

                if (dtKHAMBENH.Rows.Count > 0)
                {
                    ma_the = dtKHAMBENH.Rows[0]["MA_BHYT"].ToString();
                    ho_ten = dtKHAMBENH.Rows[0]["TENBENHNHAN"].ToString();
                    ngay_sinh = dtKHAMBENH.Rows[0]["NGAY_SINH"].ToString();
                    ngay_bd = dtKHAMBENH.Rows[0]["BHYT_BD"].ToString();
                    ngay_kt = dtKHAMBENH.Rows[0]["BHYT_KT"].ToString();
                    ma_CSKCB = dtKHAMBENH.Rows[0]["MA_KCBBD"].ToString();
                    gioi_tinh = dtKHAMBENH.Rows[0]["GIOITINHID"].ToString();
                }

                NGT02K047_LichSuKCB frm = new NGT02K047_LichSuKCB(
                    ma_the, ho_ten, ngay_sinh, gioi_tinh,
                    ma_CSKCB,
                    ngay_bd, ngay_kt);
                openForm(frm, "1");
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnThongBao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (HIS_HIENTHI_GOIKHAM == "1") // kiểu cũ
            {
                NGT02K027_ThongbaoDangKyKham frm = new NGT02K027_ThongbaoDangKyKham();
                frm.Show();
                //openForm(frm, "1");
            }
            else if (HIS_HIENTHI_GOIKHAM == "2")// bv Nguyễn trãi        
            {
                NGT02K053_PK_LCD frm = new NGT02K053_PK_LCD();
                frm.Show();
                //openForm(frm, "1");
            }
            else if (HIS_HIENTHI_GOIKHAM == "4")//      
            {
                NGT02K053_PK_LCDBD frm = new NGT02K053_PK_LCDBD();
                frm.Show();
                //openForm(frm, "1");
            }
        }

        #endregion


        #region TAB DS BỆNH NHÂN
        private void load_tab2()
        {
            DateTime dtime = Func.getSysDatetime_Short();
            dtimeTu.DateTime = dtime;// Const.sys_date;
            dtimeDen.DateTime = dtime;

            DataTable dt = new DataTable();
            // Các trạng thái khám
            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThaiKham);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "-- Tất cả --";
                dt.Rows.InsertAt(dr, 0);
            }
            ucTrangthai.setData(dt, 0, 1);
            ucTrangthai.lookUpEdit.Properties.ShowHeader = false;
            ucTrangthai.setColumn("col1", -1, "", 0);
            ucTrangthai.SelectIndex = 0;
            ucTrangthai.setEvent(btnTimkiem_Click);

            // Danh sách phòng khám
            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Phongkham);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "-- Tất cả --";
                dt.Rows.InsertAt(dr, 0);
            }
            ucTabDS_Phongkham.setData(dt, 0, 1);
            ucTabDS_Phongkham.lookUpEdit.Properties.ShowHeader = false;
            ucTabDS_Phongkham.setColumn(0, -1, "", 0);
            ucTabDS_Phongkham.SelectIndex = 0;
            ucTabDS_Phongkham.setEvent(btnTimkiem_Click);

            //grid
            ucGrid_DsBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_DsBN.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGrid_DsBN.setEvent(getData_table);
            ucGrid_DsBN.setEvent_FocusedRowChanged(DsBN_Change_SelectRow);
            ucGrid_DsBN.setEvent_DoubleClick(DoubleClick);
            ucGrid_DsBN.setEvent_MenuPopupClick(MenuPopupClick);
            ucGrid_DsBN.SetReLoadWhenFilter(true);

            // da co trong default ucGrid_DsBN.gridView.OptionsView.ColumnAutoWidth = true;

            ucGrid_DsBN.addMenuPopup(Menu_Popup());

            ucGrid_DsBN.gridView.OptionsCustomization.AllowSort = false;
        }
        private void init_tab2()
        {
            ucTrangthai.SelectIndex = 0;
            ucTabDS_Phongkham.SelectIndex = 0;
            getData_table(1, null);
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (dtimeTu.DateTime == null || dtimeTu.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Từ ngày!");
                dtimeTu.Focus();
                return;
            }
            if (dtimeDen.DateTime == null || dtimeDen.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Đến ngày!");
                dtimeDen.Focus();
                return;
            }
            if (dtimeTu.DateTime > dtimeDen.DateTime)
            {
                MessageBox.Show("Từ ngày không thể lớn hơn Đến ngày!");
                return;
            }
            getData_table(1, null);
        }
        private void dtimeTu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimkiem_Click(null, null);
            }
        }
        private void dtimeDen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimkiem_Click(null, null);
            }
        }
        private void getData_table(object sender, EventArgs e)
        {
            //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") +"=====  getData_table");
            int page = (int)sender;
            if (page > 0)
            {
                string jsonFilter = "";
                //// Lấy điều kiện filter
                //if (ucGrid_DsBN.ReLoadWhenFilter)
                //{
                //    if (ucGrid_DsBN.tableFlterColumn.Rows.Count > 0)
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsBN.tableFlterColumn);
                //}

                DsBenhnhan ds = ServiceTiepNhanBenhNhan.getDS_BenhNhan_TiepNhan(
                dtimeTu.Text, dtimeDen.Text, ucTrangthai.SelectValue, ucTabDS_Phongkham.SelectValue,
                page, ucGrid_DsBN.ucPage1.getNumberPerPage(), ucGrid_DsBN.jsonFilter());

                ucGrid_DsBN.clearData();
                // Gán lại tiêu đề các tab, reset lại dữ liệu các tab về rỗng.
                tabXetNghiem.PageText = "Xét nghiệm";
                tabCDHA.PageText = "CĐHA";
                tabPhauThuat.PageText = "Phẫu thuật thủ thuật";
                tabTienCongKham.PageText = "Tiền công khám";
                tabPhieuThuKhac.PageText = "Phiếu thu khác";
                // gán lại giá trị select BN là rỗng
                Const.drvBenhNhan = null;
                saveKHAMBENHID = "";
                dtKHAMBENH = new DataTable();
                ucTabXetNghiem1.clearAllGrid_frmTiepNhan();
                ucTabCDHA1.clearAllGrid_frmTiepNhan();
                ucTabPhauThuatThuThuat1.clearAllGrid_frmTiepNhan();
                ucTabTienCongKham.clearAllGrid_frmTiepNhan();
                ucTabPhieuThuKhac.clearAllGrid_frmTiepNhan();



                DataTable dt = new DataTable();
                if (ds!=null && ds.rows == null || ds.rows.Count == 0) dt = Func.getTableEmpty(new String[]
                    { "RN", "TRANGTHAIKHAMBENH", "KQCLS", "MAHOSOBENHAN", "MABENHNHAN", "TENBENHNHAN"
                    , "NGAYSINH", "GIOITINH", "MA_BHYT", "NGAYTIEPNHAN", "ORG_NAME", "NGUOITN" });
                else dt = ds.rows.ConvertListToDataTable<benhnhan>();

                ucGrid_DsBN.setData(dt, ds.total, ds.page, ds.records);

                ucGrid_DsBN.setColumnAll(false);

                //ucGrid_DsBN.setColumn("RN", 0, "STT", 50);
                //ucGrid_DsBN.setColumn("TRANGTHAIKHAMBENH", 1, " ", 50);
                //ucGrid_DsBN.setColumn("KQCLS", 2, "KQCLS", 50);
                //ucGrid_DsBN.setColumn("MAHOSOBENHAN", 3, "Mã BA", 110);
                //ucGrid_DsBN.setColumn("MABENHNHAN", 4, "Mã BN", 110);
                //ucGrid_DsBN.setColumn("TENBENHNHAN", 5, "Tên Bệnh nhân", 170);
                //ucGrid_DsBN.setColumn("NGAYSINH", 6, "Ngày sinh", 90);
                //ucGrid_DsBN.setColumn("GIOITINH", 7, "Giới tính", 50);
                //ucGrid_DsBN.setColumn("MA_BHYT", 8, "Mã BHYT", 110);
                //ucGrid_DsBN.setColumn("NGAYTIEPNHAN", 9, "Ngày tiếp nhận", 120);
                //ucGrid_DsBN.setColumn("ORG_NAME", 10, "Phòng khám", 0);

                ucGrid_DsBN.setColumn("RN", 0, "STT");
                ucGrid_DsBN.setColumn("TRANGTHAIKHAMBENH", 1, " ");
                ucGrid_DsBN.setColumn("KQCLS", 2, "KQCLS");
                ucGrid_DsBN.setColumn("MAHOSOBENHAN", 3, "Mã BA");
                ucGrid_DsBN.setColumn("MABENHNHAN", 4, "Mã BN");
                ucGrid_DsBN.setColumn("TENBENHNHAN", 5, "Tên Bệnh nhân");
                ucGrid_DsBN.setColumn("NGAYSINH", 6, "Ngày sinh");
                ucGrid_DsBN.setColumn("GIOITINH", 7, "Giới tính");
                ucGrid_DsBN.setColumn("MA_BHYT", 8, "Mã BHYT");
                ucGrid_DsBN.setColumn("NGAYTIEPNHAN", 9, "Ngày tiếp nhận");
                ucGrid_DsBN.setColumn("ORG_NAME", 10, "Phòng khám");
                ucGrid_DsBN.setColumn("NGUOITN", 11, "Người TN");


                ucGrid_DsBN.setColumnImage("TRANGTHAIKHAMBENH", new String[] { "1", "4", "9" }
                    , new String[] { "./Resources/metacontact_away.png", "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png" });

                ucGrid_DsBN.setColumnImage("KQCLS", new String[] { "1", "2" }
                   , new String[] { "./Resources/Flag_Red_New.png", "./Resources/True.png" });

                ucGrid_DsBN.gridView.BestFitColumns(true);

            }
        }

        private void DsBN_Change_SelectRow(object sender, EventArgs e)
        {
            reload_title_of_Tabs((DataRowView)sender);

            //Const.drvBenhNhan = (DataRowView)sender;
            //loadBy_KHAMBENHID = Const.drvBenhNhan["KHAMBENHID"].ToString();

            //bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            //try
            //{
            //    if (existSplash)
            //        DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

            //    getChiTiet_KhamBenh(loadBy_KHAMBENHID);

            //    if (dtKHAMBENH.Rows.Count > 0)
            //    {
            //        ThayDoiMenu(dtKHAMBENH.Rows[0]["TRANGTHAIKHAMBENH"].ToString());

            //        tabXetNghiem.PageText = dtKHAMBENH.Rows[0]["SLXN"].ToString() == "0" ? "Xét nghiệm" : "Xét nghiệm(" + dtKHAMBENH.Rows[0]["SLXN"].ToString() + ")";
            //        tabCDHA.PageText = dtKHAMBENH.Rows[0]["SLCDHA"].ToString() == "0" ? "CĐHA" : "CĐHA(" + dtKHAMBENH.Rows[0]["SLCDHA"].ToString() + ")";
            //        tabPhauThuat.PageText = dtKHAMBENH.Rows[0]["SLCHUYENKHOA"].ToString() == "0" ? "Phẫu thuật thủ thuật" : "Phẫu thuật thủ thuật(" + dtKHAMBENH.Rows[0]["SLCHUYENKHOA"].ToString() + ")";
            //        tabTienCongKham.PageText = dtKHAMBENH.Rows[0]["CONGKHAM"].ToString() == "0" ? "Tiền công khám" : "Tiền công khám(" + dtKHAMBENH.Rows[0]["CONGKHAM"].ToString() + ")";
            //        tabPhieuThuKhac.PageText = dtKHAMBENH.Rows[0]["THUKHAC"].ToString() == "0" ? "Phiếu thu khác" : "Phiếu thu khác(" + dtKHAMBENH.Rows[0]["THUKHAC"].ToString() + ")";
            //    }
            //}
            //finally
            //{
            //    //Close Wait Form
            //    if (existSplash)
            //        DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            //}
        }
        private void reload_title_of_Tabs(DataRowView selectedBenhNhan = null)
        {
            if (selectedBenhNhan == null) // TH BN có update, cần gọi lấy lại dl biến dtKHAMBENH
            {
                saveKHAMBENHID = "";// cần reset biến này để hàm getChiTiet_KhamBenh gọi dl mới  
            }

            Const.drvBenhNhan = ucGrid_DsBN.SelectedRow;
            loadBy_KHAMBENHID = Const.drvBenhNhan["KHAMBENHID"].ToString();

            getChiTiet_KhamBenh(loadBy_KHAMBENHID);

            if (dtKHAMBENH.Rows.Count > 0)
            {
                // load lại dl của tab đang được chọn, còn các tab khác cho phép reload lại lần sau khi select
                ucTabXetNghiem1.allow_tab_reload = true;
                ucTabCDHA1.allow_tab_reload = true;
                ucTabPhauThuatThuThuat1.allow_tab_reload = true;
                ucTabTienCongKham.allow_tab_reload = true;
                ucTabPhieuThuKhac.allow_tab_reload = true;

                if (tabTiepNhanBN.SelectedPage == tabXetNghiem) ucTabXetNghiem1.reload();
                else if (tabTiepNhanBN.SelectedPage == tabCDHA) ucTabCDHA1.reload();
                else if (tabTiepNhanBN.SelectedPage == tabPhauThuat) ucTabPhauThuatThuThuat1.reload();
                else if (tabTiepNhanBN.SelectedPage == tabTienCongKham) ucTabTienCongKham.reload();
                else if (tabTiepNhanBN.SelectedPage == tabPhieuThuKhac) ucTabPhieuThuKhac.reload();

                ThayDoiMenu(dtKHAMBENH.Rows[0]["TRANGTHAIKHAMBENH"].ToString());

                tabXetNghiem.PageText = dtKHAMBENH.Rows[0]["SLXN"].ToString() == "0" ? "Xét nghiệm" : "Xét nghiệm(" + dtKHAMBENH.Rows[0]["SLXN"].ToString() + ")";
                tabCDHA.PageText = dtKHAMBENH.Rows[0]["SLCDHA"].ToString() == "0" ? "CĐHA" : "CĐHA(" + dtKHAMBENH.Rows[0]["SLCDHA"].ToString() + ")";
                tabPhauThuat.PageText = dtKHAMBENH.Rows[0]["SLCHUYENKHOA"].ToString() == "0" ? "Phẫu thuật thủ thuật" : "Phẫu thuật thủ thuật(" + dtKHAMBENH.Rows[0]["SLCHUYENKHOA"].ToString() + ")";
                tabTienCongKham.PageText = dtKHAMBENH.Rows[0]["CONGKHAM"].ToString() == "0" ? "Tiền công khám" : "Tiền công khám(" + dtKHAMBENH.Rows[0]["CONGKHAM"].ToString() + ")";
                tabPhieuThuKhac.PageText = dtKHAMBENH.Rows[0]["THUKHAC"].ToString() == "0" ? "Phiếu thu khác" : "Phiếu thu khác(" + dtKHAMBENH.Rows[0]["THUKHAC"].ToString() + ")";
            }
        }
        private void DoubleClick(object sender, EventArgs e)
        {
            if (ucGrid_DsBN.SelectedRow != null)
            {
                Const.drvBenhNhan = ucGrid_DsBN.SelectedRow;
                loadBy_KHAMBENHID = Const.drvBenhNhan["KHAMBENHID"].ToString();

                tabTiepNhanBN.SelectedPage = tabTiepNhan;
            }
        }

        #endregion


        #region Menu popup cho grid
        private void MenuPopupClick(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DsBN.gridView.GetFocusedRow());
                if (drv != null)
                {
                    if (menu.hlink == "menuPop_Xoa")
                    {
                        Xoa_BenhNhan(drv);
                    }
                    else if (menu.hlink == "menuPop_Sinhsouutien")
                    {
                        Sinh_souutien(drv);
                    }
                    else if (menu.hlink == "menuPop_Inlaiphieu")
                    {
                        In_laiphieu(drv);
                    }
                    else if (menu.hlink == "menuPop_Doicongkham")
                    {
                        Doi_congkham(drv);
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private List<MenuFunc> Menu_Popup()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            //if (dtCauHinh.Rows.Count > 0 && dtCauHinh.Rows[0]["CAPNHATHD"].ToString() == "1")
            //    listMenu.Add(new MenuFunc("Cập nhật lại HĐ", "menuPop_capnhathdkb", "0", "barButtonItem3.Glyph.png"));

            if (dtCauHinh.Rows.Count > 0 && dtCauHinh.Rows[0]["AN_DOI_CONGKHAM_PK"].ToString() != "1")
                listMenu.Add(new MenuFunc("Đổi công khám/phòng khám", "menuPop_Doicongkham", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Xóa bệnh nhân", "menuPop_Xoa", "0", "barButtonItem3.Glyph.png"));

            if (dtCauHinh.Rows.Count > 0 && dtCauHinh.Rows[0]["AN_SINHSO_UUTIEN"].ToString() != "1")
                listMenu.Add(new MenuFunc("Sinh số ưu tiên mới", "menuPop_Sinhsouutien", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In lại phiếu", "menuPop_Inlaiphieu", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void Xoa_BenhNhan(DataRowView drv)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn xóa bệnh nhân? ", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (drv["TRANGTHAIKHAMBENH"].ToString() != "1")
                {
                    MessageBox.Show("Bệnh nhân đang khám hoặc kết thúc khám không được xóa bệnh nhân");
                    return;
                }

                getChiTiet_KhamBenh(drv["KHAMBENHID"].ToString());

                if (dtKHAMBENH.Rows.Count > 0)
                {
                    string kieu = "1";
                    string khambenhid = drv["KHAMBENHID"].ToString();
                    int phongkhamdangkyid = -1;
                    string tiepnhanid = dtKHAMBENH.Rows[0]["TIEPNHANID"].ToString();
                    string hosobenhanid = dtKHAMBENH.Rows[0]["HOSOBENHANID"].ToString();

                    string json = "{\"kieu\":\"" + kieu + "\",\"khambenhid\":\"" + khambenhid + "\",\"phongkhamdangkyid\":" + phongkhamdangkyid + ",\"tiepnhanid\":\""
                        + tiepnhanid + "\",\"hosobenhanid\":\"" + hosobenhanid + "\"}";

                    string ret = RequestHTTP.xoaBenhNhan(json.Replace("\\", "\\\\").Replace("\"", "\\\""));

                    if (ret == "1")
                    {
                        MessageBox.Show("Xóa thành công");
                        getData_table(ucGrid_DsBN.ucPage1.Current(), null);
                        //ucGrid_DsBN.clearData(ucGrid_DsBN.gridView.FocusedRowHandle);
                    }
                    else if (ret == "dacophitt")
                    {
                        MessageBox.Show("Bệnh nhân đã có tiền thanh toán dịch vụ, không được xóa bệnh nhân.");
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công");
                    }
                }
            }
        }
        private void Sinh_souutien(DataRowView drv)
        {
            NGT02K044_sinhsothutumoi frm = new NGT02K044_sinhsothutumoi();
            frm.setKhamBenhID("1", drv["KHAMBENHID"].ToString());
            openForm(frm, "1");
        }
        private void In_laiphieu(DataRowView drv)
        {
            NGT02K044_sinhsothutumoi frm = new NGT02K044_sinhsothutumoi();
            frm.setKhamBenhID("2", drv["KHAMBENHID"].ToString());
            openForm(frm, "1");
        }
        private void Doi_congkham(DataRowView drv)
        {
            if (frm_NGT02K054_ChuyenYCKham == null)
            {
                frm_NGT02K054_ChuyenYCKham = new NGT02K054_ChuyenYCKham();
            }

            getChiTiet_KhamBenh(drv["KHAMBENHID"].ToString());

            if (dtKHAMBENH.Rows.Count > 0)
            {
                string tiepnhanid = dtKHAMBENH.Rows[0]["TIEPNHANID"].ToString();
                string hosobenhanid = dtKHAMBENH.Rows[0]["HOSOBENHANID"].ToString();
                string doituongbenhnhanid = dtKHAMBENH.Rows[0]["DOITUONGBENHNHANID"].ToString();

                frm_NGT02K054_ChuyenYCKham.setData(drv["KHAMBENHID"].ToString(), tiepnhanid, hosobenhanid, doituongbenhnhanid);

                openForm(frm_NGT02K054_ChuyenYCKham, "1");
            }
        }


        #endregion

        private void NGT01T001_TiepNhanKhamBenh_Shown(object sender, EventArgs e)
        {
            ucThongTinHanhChinh1.txtHoten.Focus();
        }

        private NGT02K037_DSBN_HopDong frm;
        private void lbHopDong_Click(object sender, EventArgs e)
        {
            if (frm == null)
            {
                frm = new NGT02K037_DSBN_HopDong();
                frm.setEvent_Return(NGT02K037_DSBN_HopDong_Return);
            }
            openForm(frm, "1");
        }
        private void NGT02K037_DSBN_HopDong_Return(object sender, EventArgs e)
        {
            // HopDong_ID,MaBenhNhan
            string ret = (string)sender;

            string[] list = ret.Split(',');
            if (list.Length == 1)
            {// Nhập mới
                ucHopDong.SelectValue = list[0];
                btnNhapmoi_Click(null, null);
            }
            else if (list.Length >= 2)
            {
                ucHopDong.SelectValue = list[0];
                ucThongTinHanhChinh1.txtHoten.Text = list[2];
                try
                {
                    if (list[3] != "")
                    {
                        DateTime dtime = Func.ParseDate(list[3]);
                        ucThongTinHanhChinh1.dtimeNgaysinh.DateTime = dtime;
                    }
                }
                catch (Exception ex) { log.Fatal(ex.ToString()); }
                ucThongTinHanhChinh1.ucGioitinh.SelectValue = list[4];

                TimKiem_BenhNhan(list[1], "1");
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F3)
            {
                NTU01H013_TimKiemBenhNhan frm = new NTU01H013_TimKiemBenhNhan("",
                        ucThongTinHanhChinh1.txtHoten.Text
                        , ucThongTinHanhChinh1.dtimeNgaysinh.Text
                        , ucThongTinHanhChinh1.ucGioitinh.SelectValue
                        , "-1", "1"
                        );
                frm.setEvent_Return(ReturnForm_NTU01H013_TimKiemBenhNhan);
                openForm(frm, "1");
                // Ko gọi keypress của các control khác
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dtimeNgaykham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ucNoisong.Enabled) ucNoisong.Focus();
                else rbtCapCuu.Focus();
            }
        }

        private void ckbCheckBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ckbTheTE.Focus();
        }

        private void btnChupHinh_Click(object sender, EventArgs e)
        {
            if (BN_Sua == null)
            {
                MessageBox.Show("Vui lòng chọn bệnh nhân");
                return;
            }

            string hoSoBenhNhanID = (BN_Sua != null && BN_Sua["HOSOBENHANID"] != null) ? BN_Sua["HOSOBENHANID"].ToString() : string.Empty;
            if (string.IsNullOrEmpty(hoSoBenhNhanID))
            {
                MessageBox.Show("Không tìm thấy hồ sơ bệnh nhân");
                return;
            }

            VNPT.HIS.CommonForm.NGT02K026_ChupAnhBenhNhan frm = new VNPT.HIS.CommonForm.NGT02K026_ChupAnhBenhNhan();
            frm.SetParams(hoSoBenhNhanID);
            frm.SetEvent_Return(ReturnForm_NGT02K026_ChupAnhBenhNhan);
            openForm(frm, "1");
        }

        private void ReturnForm_NGT02K026_ChupAnhBenhNhan(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            if (img != null)
            {
                picAnhBenhNhan.Image = img;
            }
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            VNPT.HIS.CommonForm.NGT02K059_show_img frm = new VNPT.HIS.CommonForm.NGT02K059_show_img(picAnhBenhNhan.Image);
            openForm(frm, "1");
        }

        private void btnLsBYT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                string MABHYT = "";
                string TENBENHNHAN = "";
                string NGAYSINH = "";
                string QRCODE = "";
                string GIOITINH = "";
                DateTime? TUNGAY = DateTime.ParseExact("01/01/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime? DENNGAY = DateTime.ParseExact("31/12/2018", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                 
                if (dtKHAMBENH.Rows.Count > 0)
                {
                    MABHYT = dtKHAMBENH.Rows[0]["MA_BHYT"].ToString();
                    TENBENHNHAN = dtKHAMBENH.Rows[0]["TENBENHNHAN"].ToString();
                    NGAYSINH = dtKHAMBENH.Rows[0]["NGAY_SINH"].ToString();
                    if (NGAYSINH == "") NGAYSINH = dtKHAMBENH.Rows[0]["NAMSINH"].ToString();
                    GIOITINH = dtKHAMBENH.Rows[0]["GIOITINHID"].ToString();
                    //TUNGAY = dtKHAMBENH.Rows[0]["BHYT_BD"].ToString();
                    //DENNGAY = dtKHAMBENH.Rows[0]["BHYT_KT"].ToString();
                }

                NGT02K049_TraCuuCongBYT frm = new NGT02K049_TraCuuCongBYT();
                frm.setParam(MABHYT, TENBENHNHAN, NGAYSINH, QRCODE, GIOITINH, TUNGAY, DENNGAY);
                openForm(frm, "1");
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }

        }

        #region GỌI KHÁM: BV Bưu điện HCM
        private void btnLAYTHONGTIN4_Click(object sender, EventArgs e)
        {
            try
            {
                string host = dtCauHinh.Rows[0]["URLWEBSITE"].ToString().Trim();            // link upload img
                string host1 = dtCauHinh.Rows[0]["URLWEBSITE1"].ToString().Trim();         // link local bd
                string user = dtCauHinh.Rows[0]["TECHUSER"].ToString().Trim();
                string pass = dtCauHinh.Rows[0]["TECHPASS"].ToString().Trim();

                //host = "dakhoabuudien.vnptsoftware.vn";
                //host1 = "http://10.70.124.252:8082"; --> code gọi vào link host1

                string stt = txtSTTKHAM4.Text.Trim();

                if (stt == "" || Func.Parse(stt) == 0)
                {
                    MessageBox.Show("Chưa nhập STT / STT không phải dạng số", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                // dang nhap de lay API key: 

                if (user == "")
                {
                    MessageBox.Show("Chưa cấu hình thông tin đăng nhập TECH.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // login vao lay thong tin dang nhap; 
                wsTECHS_respons login = ServiceTECHS.Login(host1, user, pass);
                if (login == null)
                {
                    MessageBox.Show("Lỗi không xác thực được thông tin đăng nhập", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (login.status.statusCode != "1")
                {
                    MessageBox.Show("Thông tin đăng nhập không đúng: " + login.status.statusDesc, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                string userid = login.data.userId;
                string apikey = login.data.apiKey;
                //		 {\"patientSeq\":11,\"fullName\":\"Nguyễn Hoàng Tuấn\",\"birthday\":\"2012-12-21\",
                // \"gender\":1,\"address\":\"Bến Thành, Quận 1,  TP. Hồ Chí Minh\",\"hasInsurrance\":1,\"insurranceId\":\"DN4745072500058\",\"hospitalId\":
                // \"79 - 071\",\"startDate\":\"2017-01-01\",\"expirationDate\":\"2017-03-31\"}";		 
                PatientInfo info = ServiceTECHS.getPatientInfoFile(host1, host, userid, apikey, stt);

                if (info == null)
                {
                    MessageBox.Show("Có lỗi xảy ra, không lấy được thông tin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string loi = "";
                if (info.status.statusCode != "1") loi = "Lỗi dữ liệu trả về: " + info.status.statusDesc;
                if (info.status.statusCode == "6") loi = "Tham số truyền vào không phù hợp.";
                if (info.status.statusCode == "7") loi = "Thông tin xác thực sử dụng không đúng.";
                if (info.status.statusCode == "8") loi = "Không tìm thấy thông tin xác thực phù hợp.";
                if (info.status.statusCode == "200") loi = "Lỗi chức năng, yêu cầu kiểm tra lại";

                if (loi != "")
                {
                    MessageBox.Show(loi, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // TUY VAO BENH VIEN SE CO CACH SET MAC DINH DOI TUONG KHAI BAO DUOC CHON KHAC NHAU. 
                var doituong_khaibao = dtCauHinh.Rows[0]["DTKHAIBAO"].ToString().Trim();
                if (info.data.hasInsurrance == "1")
                {
                    //// set doi tuong mac dinh khi chua co du lieu populate vao; 
                    //if (doituong_khaibao == "1")
                    //{
                    //		$("#cboDTBNID").find("option[extval0='2']").attr("selected", "selected");       // doi tuong BHYT ngoài ngành
                    //}
                    //else
                    //{
                    //		$("#cboDTBNID").val("1");
                    //}

                    txtSoThe.Text = info.data.insurranceId;
                    ucDKKCB.SelectedValue = getStandardString(info.data.hospitalId);
                    dtimeTungay.DateTime = Func.ParseDate(info.data.startDate, "yyyy-MM-dd");  // "2017-01-01
                    dtimeDenngay.DateTime = Func.ParseDate(info.data.expirationDate, "yyyy-MM-dd");
                }
                else
                {
                    //if (doituong_khaibao == "1")
                    //{
                    //		$("#cboDTBNID").find("option[extval0='6']").attr("selected", "selected");
                    //}
                    //else
                    //{
                    //		$("#cboDTBNID").val("2");
                    //} 
                }

                ucThongTinHanhChinh1.txtHoten.Text = info.data.fullName;
                if (info.data.birthday.Length < 10) ucThongTinHanhChinh1.txtNamsinh.Text = info.data.birthday;	// nam sinh 
                else ucThongTinHanhChinh1.dtimeNgaysinh.DateTime = Func.ParseDate(info.data.birthday, "yyyy-MM-dd");

                ucThongTinHanhChinh1.ucGioitinh.SelectValue = info.data.gender;
                ucThongTinHanhChinh1.txtDiaChiBN.Text = info.data.address;
                txtDcBaoHiem.Text = info.data.address;
            }

            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnLAYSO4_Click(object sender, EventArgs e)
        {
            txtSTTKHAM4.Text = goiKhamTech("1");
        }

        private void btnGOILAI4_Click(object sender, EventArgs e)
        {
            int vl = Func.Parse(txtGOILAI4.Text.Trim());
            if (txtGOILAI4.Text.Trim() == "")
            {
                MessageBox.Show("Giá trị gọi lại phải là số");
                txtGOILAI4.Focus();
                return;
            }
            string par = Const.local_phongId.ToString() + "$" + vl;
            string dt = RequestHTTP.call_ajaxCALL_SP_I("NGT02K088.GL", par);
            int num = Func.Parse(dt);
            if (num < 0)
            {
                MessageBox.Show("Lỗi không gọi lại được STT.");
            }
            else
            {
                txtGOILAI4.Text = "";
            }
        }

        private void btnLAYBN4_Click(object sender, EventArgs e)
        {
            // mở form NGT02K053_PK_LCDBD1
            NGT02K053_PK_LCDBD1 frm = new NGT02K053_PK_LCDBD1();
            frm.Show();
        }
        private string goiKhamTech(string source)
        {
            string stt = txtSTTKHAM4.Text.Trim();
            string deptid = Const.local_phongId.ToString();
            string par = source + "$" + deptid + "$" + stt;

            string dt = RequestHTTP.call_ajaxCALL_SP_I("NGT02K088.STT", par);
            return dt;
        }
        private string getStandardString(string str)
        {
            string[] arr = str.Split('-');
            string ret = "";
            for (int i = 0; i < arr.Length; i++)
            {
                ret += arr[i].Trim();
            }
            return ret;
        }
        #endregion

        private void KeyPress_Textbox_ChiNhapSo(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtMADOITUONGNGHEO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ucDKKCB.Focus();
            }
        }

        #region Sự kiện xử lý khi có Kết quả trả về từ form con

        // Hàm nhận về kết quả xử lý của form (chỉ của 2 form: Thuốc + Dịch vụ)
        private void listenFrm_KetQua_Thuoc_ChiDinhDV(object sender, EventArgs e)
        {
            string mess = (string)sender;
            if (!string.IsNullOrEmpty(mess)) MessageBox.Show(mess);

            //Cập nhật lại các tab: tiêu đề và dl
            reload_title_of_Tabs();
        }
        // Hàm gọi lại của form con (các tab xét nghiệm, thuốc, ... hoặc Phiếu KH, KB hỏi bệnh) để mở ra form: Thuốc + Dịch vụ
        private void listenFrm_Mo_Thuoc_ChiDinhDV(object sender, EventArgs e)
        {
            ojbDatarowview data = (ojbDatarowview)sender;

            if (data.key == "updatePHIEUVATTU" || data.key == "travattu" || data.key == "updatePHIEUTHUOC" || data.key == "tradonthuoc"
                || data.key == "tao_phieu_thuoc_di_kem" || data.key == "tao_phieu_vat_tu_di_kem" || data.key == "tao_phieu_thuoc_di_kem_hao_phi"
                || data.key == "tao_phieu_vat_tu_di_kem_hao_phi"
                )
            {
                NTU02D010_CapThuoc frm = new NTU02D010_CapThuoc();
                frm.loadData(data.key, data.drv);
                frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                openForm(frm, "1");
            }
            else if (data.key == "cap_nhat_don_thuoc_khong_thuoc")
            {
                NGT02K044_CapThuocK frm = new NGT02K044_CapThuocK();
                frm.Load_Data(data.drv["KHAMBENHID"].ToString(), data.drv["PHONGID"].ToString(),
                    data.drv["BENHNHANID"].ToString(), data.drv["DOITUONGBENHNHANID"].ToString(),
                    data.drv["HOSOBENHANID"].ToString(), data.drv["MAUBENHPHAMID"].ToString());
                frm.set_para_UPDATE(data.drv);
                openForm(frm, "1");
            }
            else // form chỉ định dv
            {
                NGT02K016_ChiDinhDichVu frm = new NGT02K016_ChiDinhDichVu();
                frm.loadData(data.key, data.drv, _flgModeView, "5");
                if (data.key == "phieu_mien_giam") frm.set_DICHVUKHAMBENHID(data.id.ToString());
                frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(listenFrm_KetQua_Thuoc_ChiDinhDV);
                openForm(frm, "1");
            }
        }

        #endregion

    }
}