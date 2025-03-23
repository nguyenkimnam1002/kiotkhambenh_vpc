using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.XtraEditors;
using VNPT.HIS.Controls.Class;
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.NgoaiTru
{
    public partial class ucTabDanhSachBenhNhan : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ucTabDanhSachBenhNhan()
        {
            InitializeComponent();
        }
        string kbtraingay = "0";
        public void Init_Form(string kbtraingay)
        {
            this.kbtraingay = kbtraingay;
            if (this.kbtraingay == "") this.kbtraingay = "0";

            DateTime dtime = Func.getSysDatetime_Short();
            dtimeTu.EditValueChanged -= dtimeTu_EditValueChanged;
            dtimeDen.EditValueChanged -= dtimeDen_EditValueChanged;

            dtimeTu.DateTime = dtime;// Const.sys_date;
            dtimeDen.DateTime = dtime;

            dtimeTu.EditValueChanged += dtimeTu_EditValueChanged;
            dtimeDen.EditValueChanged += dtimeDen_EditValueChanged;



            ucGrid_DsBN.gridView.OptionsView.ColumnAutoWidth = true;
            ucGrid_DsBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_DsBN.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGrid_DsBN.setEvent(getData_table);
            ucGrid_DsBN.setEvent_FocusedRowChanged(change_selectRow);
            ucGrid_DsBN.SetReLoadWhenFilter(true);
            ucGrid_DsBN.setNumberPerPage(new int[] { 100, 200, 300 });


            ucGrid_DsBN.addMenuPopup(Default_Menu_Popup());
            
            DataTable dt = new DataTable();

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV002, "87");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "Tất cả";
                dt.Rows.InsertAt(dr, 0);
            }
            ucTrangThai.setData(dt, 0, 1);
            ucTrangThai.SelectIndex = dt.Rows.Count - 1;
            ucTrangThai.setColumn(0, false);
            ucTrangThai.setEvent(btnTimkiem_Click);

            getData_table(1, null);

        }

        public void Reload()
        {
            btnTimkiem_Click(null, null);
        }
       
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (dtimeTu.DateTime > dtimeDen.DateTime)
            {
                MessageBox.Show("Sai điều kiện tìm kiếm, từ ngày không thể lớn hơn đến ngày!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            getData_table(1, null);
        }
        private void getData_table(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    //// Lấy điều kiện filter
                    //if (ucGrid_DsBN.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DsBN.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsBN.tableFlterColumn);
                    //}

                     ResponsList ds = ServiceTabDanhSachBenhNhan.getDsBenhNhan(page, ucGrid_DsBN.getNumberPerPage()
                    , dtimeTu.DateTime.ToString(Const.FORMAT_date1), dtimeDen.DateTime.ToString(Const.FORMAT_date1)
                    , Const.local_phongId + "", ucTrangThai.SelectValue, txtMaBN_TimKiem.Text.Trim()
                    , ucGrid_DsBN.jsonFilter()
                    , kbtraingay
                    );
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
                    //"SSS": "1"} 

                    if (ds == null) return;
                    DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                    ucGrid_DsBN.clearData();
                    reset();

                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN",  "TRANGTHAIKHAMBENH", "KQCLS", "SOTHUTU", "MAHOSOBENHAN", "MABENHNHAN", "TENBENHNHAN", "MA_BHYT" });

                    {

                        ucGrid_DsBN.setData(dt, ds.total, ds.page, ds.records);
                        ucGrid_DsBN.setColumnAll(false);
                        System.Console.WriteLine(DateTime.Now.ToString("HHmmss") + "=" + " lay dl-----"+ dt.Rows.Count);

                        ucGrid_DsBN.setColumn("RN", 0, " ");
                        ucGrid_DsBN.setColumn("TRANGTHAIKHAMBENH", 1, " ");
                        ucGrid_DsBN.setColumn("KQCLS", 2, " ");
                        ucGrid_DsBN.setColumn("SOTHUTU", 3, "Số TT");
                        ucGrid_DsBN.setColumn("MAHOSOBENHAN", 4, "Mã BA");
                        ucGrid_DsBN.setColumn("MABENHNHAN", 5, "Mã BN");
                        ucGrid_DsBN.setColumn("TENBENHNHAN", 6, "Tên Bệnh nhân");
                        ucGrid_DsBN.setColumn("MA_BHYT", 7, "Mã BHYT");


                        ucGrid_DsBN.setColumnImage("TRANGTHAIKHAMBENH", new String[] { "1", "4", "9" }
                            , new String[] { "./Resources/metacontact_away.png", "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png" });

                        ucGrid_DsBN.setColumnImage("KQCLS", new String[] { "1", "2" }
                           , new String[] { "./Resources/Flag_Red_New.png", "./Resources/True.png" });

                        ucGrid_DsBN.gridView.BestFitColumns(true);
                    }

                    //DataRowView drv = (DataRowView)(ucGrid_DsBN.gridView.GetFocusedRow());
                    //change_selectRow(drv, null);
                }
            }
            finally
            {
                //Close Wait Form
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        //DataRowView selectedBenhNhan = null;
        private void change_selectRow(object sender, EventArgs e)
        {
            ucGrid_DsBN.setEvent_FocusedRowChanged(null); // bỏ event click, chống việc click liên tiếp
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataRowView selectedBenhNhan = (DataRowView)sender;
                if (selectedBenhNhan != null)
                {
                    //  Const.drvBenhNhan =	RN: 1,	PHONGKHAMDANGKYID: 1995,	SOTHUTU: 0001,	TRANGTHAI_CLS: null, 	TRANGTHAI_STT: 4,	DASANSANG: 0,	KHAMBENHID: 2635,
                    //	    DATHUTIENKHAM: null,	DAGIUTHEBHYT: 1,	TRANGTHAIKHAMBENH: 4,	UUTIENKHAMID: 0,	DOITUONGBENHNHANID: 1,	LANGOI: 0,	ORD: 0,	YEUCAUKHAM: 7-Khám Nội,
                    //      HINHTHUCVAOVIENID: 3,	MAHOADULIEU: 0,	BENHNHANID: 2987,	MAHOSOBENHAN: BA00000096,  	MABENHNHAN: BN00000090,	HOSOBENHANID: 3045,	XUTRIKHAMBENHID: 1,
                    //	    TENTRANGTHAIKB: Đang khám, TENBENHNHAN: TET 2305,	MA_BHYT: TE1350100000178,	MA_KCBBD: 35001,	TIEPNHANID: 2963,	LOAITIEPNHANID: 1,	KQCLS: null,
                    //	    MADICHVU: 02.1896,	SSS: 1
                    Const.drvBenhNhan = selectedBenhNhan;
                    // trả về form cha với dòng dl: Const.drvBenhNhan --> để xử lý khi chọn bệnh nhân
                    if (select_change != null) select_change(Const.drvBenhNhan, new EventArgs()); 

                    lbSTT.Text = selectedBenhNhan["SOTHUTU"].ToString();
                    DataTable dt_TongTienDV = ServiceTabDanhSachBenhNhan.getTongTienDV(selectedBenhNhan["TIEPNHANID"].ToString());

                    if (dt_TongTienDV.Rows.Count > 0)
                    {
                        txtTongChiPhi.Text = Func.formatMoneyEng(dt_TongTienDV.Rows[0]["TONGTIENDV"].ToString()) + " đ";
                        txtTienTamUng.Text = Func.formatMoneyEng(dt_TongTienDV.Rows[0]["TAMUNG"].ToString()) + " đ";
                        txtMaBN.Text = selectedBenhNhan["MABENHNHAN"].ToString();
                    }
                     
                    DataTable dt_ChiTiet = ServiceTabDanhSachBenhNhan.getChiTietBN(selectedBenhNhan["KHAMBENHID"].ToString()
                        , Const.local_phongId + "");


                    if (dt_ChiTiet.Rows.Count > 0)
                    {
                        //  Const.drvBenhNhan_ChiTiet = MABENHNHAN: BN00000090, TENBENHNHAN: TET 2305, PHONGDK: 219. Phòng khám Nội tiết, NGAYSINH: 03/11/2015, NAMSINH: 2015, DANTOC: Kinh, 
                        //	    QUOCGIA: Việt Nam, DIACHI: Phường Minh Khai-Thành Phố Phủ Lý-Hà Nam, TENNGHENGHIEP: Trẻ em, NGAYRAVIEN: 23/05/2017 00:00:00, 
                        //	    DENKHAMLUC: 23/05/2017 00:34:11 -> 23/05/2017 00:00:00, NOILAMVIEC: , GIOITINH: Nữ, DOITUONG: BHYT, KCBBD: 35001, SOTHEBHYT: TE1350100000178, 
                        //	    BHYTDEN: 02/11/2021, BAOTINCHO: 1-, YEUCAUKHAM: 7-Khám Nội, PHONGKHAM: 219. Phòng khám Nội tiết, XUTRI: Cấp toa cho về, TUYEN: Đúng tuyến, NGAYTN: 201705230034, 
                        //	    LOAIBENHAN: Khám bệnh, CDTD: , ANHBENHNHAN: , CDC: Bệnh tả do Vibrio cholerae 01, typ sinh học eltor, CDP: A19.8-Lao kê khác, MAKHAMBENH: KB000000131, 
                        //	    MATIEPNHAN: TN000000116, MABENHAN: BA00000096, DUYETKETOAN: 0, DUYETBH: 0, BTNTHUOC: 1, TRANGTHAI_STT: 4, SLXN: 0, SLCDHA: 0, SLCHUYENKHOA: 0, SLTHUOC: 2, 
                        //	    SLVATTU: 0, SLVANCHUYEN: 0, CONGKHAM: 1, DICHVUID: 400035, PHONGID: 4126
                        Const.drvBenhNhan_ChiTiet = dt_ChiTiet.DefaultView[0];

                        txtMaVienPhi.Text = dt_ChiTiet.Rows[0]["MATIEPNHAN"].ToString();
                        txtHoTen.Text = dt_ChiTiet.Rows[0]["TENBENHNHAN"].ToString();
                        txtDiaChi.Text = dt_ChiTiet.Rows[0]["DIACHI"].ToString();
                        txtNgaySinh.Text = dt_ChiTiet.Rows[0]["NGAYSINH"].ToString();
                        txtGioitinh.Text = dt_ChiTiet.Rows[0]["GIOITINH"].ToString();

                        txtBaoTinCho.Text = dt_ChiTiet.Rows[0]["BAOTINCHO"].ToString();
                        txtYeuCauKham.Text = dt_ChiTiet.Rows[0]["YEUCAUKHAM"].ToString();
                        txtDoiTuong.Text = dt_ChiTiet.Rows[0]["DOITUONG"].ToString();

                        txtKCBBD.Text = dt_ChiTiet.Rows[0]["KCBBD"].ToString();

                        txtThoiHanBHYT.Text = dt_ChiTiet.Rows[0]["BHYTDEN"].ToString();
                        txtTuyen.Text = dt_ChiTiet.Rows[0]["TUYEN"].ToString();

                        txtSoTheBHYT.Text = dt_ChiTiet.Rows[0]["SOTHEBHYT"].ToString();
                        txtThoiGianTu.Text = dt_ChiTiet.Rows[0]["DENKHAMLUC"].ToString();
                        txtXuTri.Text = dt_ChiTiet.Rows[0]["XUTRI"].ToString();
                        txtPhongDangKy.Text = dt_ChiTiet.Rows[0]["PHONGKHAM"].ToString();
                        txtChuanDoanTD.Text = dt_ChiTiet.Rows[0]["CDTD"].ToString();
                        txtChuanDoanChinh.Text = dt_ChiTiet.Rows[0]["CDC"].ToString();
                        txtChuanDoanPhu.Text = dt_ChiTiet.Rows[0]["CDP"].ToString();
                        try
                        {
                            if (layoutControlItem_AnhChup.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                            {
                                pictureBox1.Image = Func.getIcon("nobody.png");
                                if(dt_ChiTiet.Rows[0]["ANHBENHNHAN"] != null && dt_ChiTiet.Rows[0]["ANHBENHNHAN"].ToString() != "")
                                    pictureBox1.Load(dt_ChiTiet.Rows[0]["ANHBENHNHAN"].ToString());
                            }
                        }
                        catch (Exception ex) {  }

                        // trả về form cha với dòng dl: Const.drvBenhNhan_ChiTiet --> để load lại số lượng phiếu của các tabs
                        select_change(Const.drvBenhNhan_ChiTiet, null);
                    }

                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                ucGrid_DsBN.setEvent_FocusedRowChanged(change_selectRow);
            }
        }
        private void reset()
        {
            txtTienTamUng.Text = "";
            txtTongChiPhi.Text = "";
            txtMaBN.Text = "";

            txtMaVienPhi.Text = "";
            txtHoTen.Text = "";
            txtDiaChi.Text = "";
            txtNgaySinh.Text = "";
            txtGioitinh.Text = "";

            txtBaoTinCho.Text = "";
            txtYeuCauKham.Text = "";
            txtDoiTuong.Text = "";

            txtKCBBD.Text = "";

            txtThoiHanBHYT.Text = "";
            txtTuyen.Text = "";

            txtSoTheBHYT.Text = "";
            txtThoiGianTu.Text = "";
            txtXuTri.Text = "";
            txtPhongDangKy.Text = "";
            txtChuanDoanTD.Text = "";
            txtChuanDoanChinh.Text = "";
            txtChuanDoanPhu.Text = "";

            //picAnhBenhNhan.Image = Func.getIcon("nobody.png");
            pictureBox1.Image = Func.getIcon("nobody.png");
        }

        protected EventHandler select_change;
        public void setEvent(EventHandler eventChangeValue)
        {
            select_change = eventChangeValue;
        }

        public void setEvent_MenuPopupClick(EventHandler eventMenuPopupClick)
        {
            ucGrid_DsBN.setEvent_MenuPopupClick(eventMenuPopupClick);
        }
        



        public void HienThi_AnhChup(bool hienthi)
        {
            if (hienthi)
                layoutControlItem_AnhChup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                layoutControlItem_AnhChup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private List<MenuFunc> Default_Menu_Popup()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();
             

            listMenu.Add(new MenuFunc("BỆNH ÁN", "", "0", ""));

            listMenu.Add(new MenuFunc("Yêu cầu mở lại bệnh án", "VNPT.HIS.CommonForm.NGT02K029_YeuCauMoBenhAn", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Hủy chuyển khám", "huy_chuyen_kham", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("LỊCH SỬ", "", "0", ""));

            listMenu.Add(new MenuFunc("Lịch sử khám / điều trị chi tiết", "VNPT.HIS.CommonForm.NGT02K025_LichSuDieuTri", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Lịch sử theo đợt điều trị", "VNPT.HIS.CommonForm.NGT02K025_LichSuDieuTri_theodot", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Lịch sử theo cổng BHYT", "VNPT.HIS.CommonForm.NGT02K047_LichSuKCB", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("VIỆN PHÍ", "", "0", ""));

            listMenu.Add(new MenuFunc("Thanh toán viện phí", "VNPT.HIS.VienPhi.VPI01T006_thanhtoanvienphi", "0", "barButtonItem3.Glyph.png")); 

            return listMenu;
        }

        private void dtimeTu_EditValueChanged(object sender, EventArgs e)
        {
            btnTimkiem_Click(null, null);
        }
        private void dtimeDen_EditValueChanged(object sender, EventArgs e)
        {
            btnTimkiem_Click(null, null);
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

        private void txtMaBN_TimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimkiem_Click(null, null);
            }
        }

    }
}
