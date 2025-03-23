using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.Controls.Class;
using System.Reflection;
using VNPT.HIS.Controls.SubForm;

namespace VNPT.HIS.Controls.NgoaiTru
{
    //
    // code trên web: NTU02D026_ThongTinChuyenKhoa
    //
    public partial class ucTabPhauThuatThuThuat : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string KHAMBENHID = "";
        private string HOSOBENHANID = "";
        private string BENHNHANID = "";

        private string lnmbp = "5";
        private string modeView = "0";
        private string hosobenhanid = "";
        private string deleteDV = "0";
        private bool checkLoad = false;
        private string trangthai_mbp = "-1";


        // biến dùng cho Nội trú
        string KHOAID = "";
        public ucTabPhauThuatThuThuat()
        {
            InitializeComponent();
        }
        protected EventHandler event_BackParentForm;
        public void setEvent_BackParentForm(EventHandler event_BackParentForm)
        {
            this.event_BackParentForm = event_BackParentForm;
        }
        private void ucTabPhauThuatThuThuat_Load(object sender, EventArgs e)
        {
            ucGrid_DanhSach.setEvent_FocusedRowChanged(DanhSach_SelectRow);
            ucGrid_DanhSach.setEvent(getData_table);

            ucGrid_ChiTiet.setEvent_FocusedRowChanged(ChiTiet_SelectRow);
            ucGrid_ChiTiet.setEvent(getData_table_ChiTiet);

            ucGrid_DanhSach.gridView.OptionsView.ShowAutoFilterRow = false;// ô search
            ucGrid_ChiTiet.gridView.OptionsView.ShowAutoFilterRow = false;// ô search

            ucGrid_DanhSach.setEvent_MenuPopupClick(MenuPopupClick_DanhSach);
            ucGrid_ChiTiet.setEvent_MenuPopupClick(MenuPopupClick_ChiTiet);

            //List<MenuFunc> listMenu = new List<MenuFunc>();

            //MenuFunc InAn = new MenuFunc("In", "", "0", "barButtonItem16.Glyph.png");
            //InAn.addChildren(new MenuFunc("In phiếu PTTT", "x", "0", "barButtonItem16.Glyph.png"));
            //InAn.addChildren(new MenuFunc("In giải trình phụ thu", "x", "0", "barButtonItem16.Glyph.png"));
            //InAn.addChildren(new MenuFunc("In phiếu chuyên khoa", "x", "0", "barButtonItem16.Glyph.png"));

            //listMenu.Add(InAn);

            //listMenu.Add(new MenuFunc("Cập nhật PTTT", "x", "0", "barButtonItem16.Glyph.png"));

            //MenuFunc Thuoc = new MenuFunc("Thuốc, VT đi kèm", "", "0", "barButtonItem16.Glyph.png");
            //Thuoc.addChildren(new MenuFunc("Tạo phiếu thuốc đi kèm hao phí", "x", "0", "barButtonItem16.Glyph.png"));
            //Thuoc.addChildren(new MenuFunc("Tạo phiếu vật tư đi kèm hao phí", "x", "0", "barButtonItem16.Glyph.png"));
            //Thuoc.addChildren(new MenuFunc("Tạo phiếu thuốc đi kèm", "x", "0", "barButtonItem16.Glyph.png"));
            //Thuoc.addChildren(new MenuFunc("Tạo phiếu vật tư đi kèm", "x", "0", "barButtonItem16.Glyph.png"));
            //Thuoc.addChildren(new MenuFunc("Danh sách phiếu thuốc đi kèm", "x", "0", "barButtonItem16.Glyph.png"));

            //listMenu.Add(Thuoc);

            //MenuFunc PTTT = new MenuFunc("Loại PTTT", "", "0", "barButtonItem16.Glyph.png");
            //PTTT.addChildren(new MenuFunc("PTTT chính", "x", "0", "barButtonItem16.Glyph.png"));
            //PTTT.addChildren(new MenuFunc("PTTT phụ không thay ekip mổ", "x", "0", "barButtonItem16.Glyph.png"));
            //PTTT.addChildren(new MenuFunc("PTTT phụ có thay ekip mổ", "x", "0", "barButtonItem16.Glyph.png"));
            //PTTT.addChildren(new MenuFunc("PTTT không thanh toán đồng thời", "x", "0", "barButtonItem16.Glyph.png"));

            //listMenu.Add(PTTT);


            //MenuFunc Phuthu = new MenuFunc("Phụ thu", "", "0", "barButtonItem16.Glyph.png");
            //Phuthu.addChildren(new MenuFunc("Tạo phiếu phụ thu", "x", "0", "barButtonItem16.Glyph.png"));
            //Phuthu.addChildren(new MenuFunc("Danh sách phiếu phụ thu", "x", "0", "barButtonItem16.Glyph.png"));

            //listMenu.Add(Phuthu);

            //ucBarMenu1.addMenu(listMenu);
            ucBarMenu1.setEvent(MenuPopupClick_ChiTiet);

            // đóng menu ngang ở giữa
            //layoutControl2.Visible = false;
        }
        private void openMenu_Bar(object sender, EventArgs e)
        {
            MenuFunc menu = (MenuFunc)sender;
            string formName = menu.text;
            string pathForm = menu.hlink;

            try
            {
                if (pathForm.LastIndexOf(".") > -1)
                {
                    {
                        string pathDll = pathForm.Substring(0, pathForm.LastIndexOf(".")) + ".dll";
                        Assembly ass = Assembly.LoadFrom(pathDll);
                        Form frm = (Form)ass.CreateInstance(pathForm);
                        frm.Tag = formName;
                        frm.Name = pathForm;
                        if (menu.options == "0")
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.MdiParent = this.ParentForm;

                            frm.Show();
                        }
                        else
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                //MessageBox.Show("Không mở được Form này!");
            }
        }

        public bool allow_tab_reload = false;
        public void loadData_2(string _KHAMBENHID, string _BENHNHANID
            , string _lnmbp, string _modeView, string _hosobenhanid, string _deleteDV, string _checkLoad, string _trangthai_mbp)
        {
            if (allow_tab_reload == false)
                if (HOSOBENHANID == _hosobenhanid && KHAMBENHID == _KHAMBENHID) return;

            allow_tab_reload = false; 
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                KHAMBENHID = _KHAMBENHID;
                HOSOBENHANID = _hosobenhanid;
                BENHNHANID = _BENHNHANID;

                if (_lnmbp != "") lnmbp = _lnmbp;
                if (_modeView != "") modeView = _modeView;
                if (_hosobenhanid != "") hosobenhanid = _hosobenhanid;
                if (_checkLoad == "1") checkLoad = true;
                else if (_checkLoad == "0") checkLoad = false;
                if (_deleteDV != "") deleteDV = _deleteDV;
                if (_trangthai_mbp != "") trangthai_mbp = _trangthai_mbp;


                load_menu_popup_forGrid();

                getData_table(1, null);
            }
            finally
            {
                //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void load_menu_popup_forGrid()
        {
            if (modeView == "0")
            {
                ucGrid_DanhSach.addMenuPopup(Menu_Popup_DanhSach_contextMenu());
            }
            else if (modeView == "1")
            {
                ucGrid_DanhSach.addMenuPopup(Menu_Popup_DanhSach_contextMenuPrint());
            }
        }
        public void reload()
        {
            getData_table(1, null);
        }
        private void getData_table(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                clearAllGrid();

                ResponsList ds = ServiceTabDanhSachBenhNhan.getDsXetNghiem(page, ucGrid_DanhSach.ucPage1.getNumberPerPage(), KHAMBENHID, BENHNHANID, lnmbp, hosobenhanid);
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);
                //  "RN": "1",
                //  "DOITUONGBENHNHANID": "1",
                //  "MABENHNHAN": "BN00000362",
                //  "TENBENHNHAN": "PHÙNG VĂN HƯƠNG",
                //  "TIEPNHANID": "3287",
                //  "HOSOBENHANID": "3446",
                //  "KHAMBENHID": "3099",
                //  "MAUBENHPHAMID": "12278",
                //  "BENHNHANID": "3301",
                //  "BARCODE": "0233",
                //  "SOPHIEU": "170604000001",
                //  "PHONGDIEUTRI": "103. Phòng khám sản",
                //  "PHONGCHIDINH": "Phòng xét nghiệm giải phẫu bệnh sinh thiết",
                //  "TRANGTHAIMAUBENHPHAM": "1",
                //  "SOTHUTUCHIDINH": "3466",
                //  "PHONGLAYMAU": "",
                //  "SOTHUTU_LAYMAU": null,
                //  "LOAIDONTHUOC": null,
                //  "SLTHANG": null,
                //  "PHONGCHUYENDENID": "4193",
                //  "NGUOITAO": "Quản trị hệ thống bệnh viện",
                //  "KHANCAP": "Bình thường",
                //  "KHOACHIDINH": "Khoa Giải phẫu bệnh",
                //  "KHOADIEUTRI": "Khoa Khám bệnh",
                //  "TENKHO": "",
                //  "LOAIPHIEU": "Nhận",
                //  "GHICHU": "",
                //  "PHONGID": "4125",
                //  "LOAIPHIEUMAUBENHPHAM": "1",
                //  "LOAIDICHVU": "BenhPham",
                //  "DICHVUCHA_ID": null,
                //  "NGUOITAO_ID": "1226",
                //  "TRANGTHAI_PHIEU": "Đang sửa phiếu",
                //  "TENNGHENGHIEP": "Công nhân",
                //  "DACODICHVUTHUTIEN": null,
                //  "KHOAID": "4001",
                //  "CHITIETGHICHU": "",
                //  "PHIEU_LINH": "",
                //  "NGUOITRAKETQUA": "",
                //  "PHIEUHEN": "0",
                //  "NGAYMAUBENHPHAM": "04/06/2017 14:04:28",
                //  "NGAYSINH": "10/10/1980 00:00:00",
                //  "NGAYMAUBENHPHAM_SUDUNG": "",
                //  "DIKEM": "0",
                //  "LOAIKEDON": "3",
                //  "PHIEUTRA_ID": null

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TRANGTHAIMAUBENHPHAM", "BARCODE", "SOPHIEU", "NGUOITAO", "NGUOITRAKETQUA"
                        , "NGAYMAUBENHPHAM", "PHONGCHIDINH","PHONGLAYMAU", "PHONGDIEUTRI", "KHANCAP", "TRANGTHAI_PHIEU" });

                {
                    ucGrid_DanhSach.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DanhSach.setColumnAll(false);

                    ucGrid_DanhSach.setColumn("RN", 0, " ");
                    ucGrid_DanhSach.setColumn("TRANGTHAIMAUBENHPHAM", 1, " ");
                    ucGrid_DanhSach.setColumn("BARCODE", 2, "Barcode");
                    ucGrid_DanhSach.setColumn("SOPHIEU", 3, "Số phiếu");
                    ucGrid_DanhSach.setColumn("NGUOITAO", 4, "Bác sỹ chỉ định");
                    ucGrid_DanhSach.setColumn("NGUOITRAKETQUA", 5, "Bác sỹ thực hiện");
                    ucGrid_DanhSach.setColumn("NGAYMAUBENHPHAM", 6, "Thời gian chỉ định");
                    ucGrid_DanhSach.setColumn("PHONGCHIDINH", 7, "P. Thực hiện");
                    ucGrid_DanhSach.setColumn("PHONGLAYMAU", 8, "P. Lấy mẫu");
                    ucGrid_DanhSach.setColumn("PHONGDIEUTRI", 9, "Phòng");
                    ucGrid_DanhSach.setColumn("KHANCAP", 10, "Khẩn");
                    ucGrid_DanhSach.setColumn("TRANGTHAI_PHIEU", 11, "Trạng thái");

                    //ucGrid_DSXetNghiem.setColumn("", , "");
                    ucGrid_DanhSach.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "3", "4" }
                        , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });

                    ucGrid_DanhSach.gridView.BestFitColumns(true);
                }
            }

            DanhSach_SelectRow(ucGrid_DanhSach.gridView.GetFocusedRow(), null);
        }
        private void getData_table_ChiTiet(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ucGrid_ChiTiet.clearData();

                ResponsList ds_KQ = ServiceTabDanhSachBenhNhan.getKqPhauThuatThuThuat(page, ucGrid_ChiTiet.ucPage1.getNumberPerPage(), MAUBENHPHAMID);
                DataTable dt_KQ = MyJsonConvert.toDataTable(ds_KQ.rows);

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "RN", "TENDICHVU", "SOLUONG", "LOAIPTTT", "TENTRANGTHAI", "NGUOITHUCHIEN", "NGAYPHAUTHUATTHUTHUAT" });

                {
                    ucGrid_ChiTiet.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGrid_ChiTiet.setColumnAll(false);

                    ucGrid_ChiTiet.setColumn("RN", 0, " ");
                    ucGrid_ChiTiet.setColumn("TENDICHVU", 1, "Tên dịch vụ");
                    ucGrid_ChiTiet.setColumn("SOLUONG", 2, "Số lượng");
                    ucGrid_ChiTiet.setColumn("LOAIPTTT", 3, "Loại PTTT");
                    ucGrid_ChiTiet.setColumn("TENTRANGTHAI", 4, "Trạng thái");
                    ucGrid_ChiTiet.setColumn("NGUOITHUCHIEN", 5, "Bác sỹ thực hiện");
                    ucGrid_ChiTiet.setColumn("NGAYPHAUTHUATTHUTHUAT", 6, "Thời gian thực hiện");


                    ucGrid_ChiTiet.gridView.BestFitColumns(true);
                }
            }
        }

        string MAUBENHPHAMID = "";
        public void DanhSach_SelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
                MAUBENHPHAMID = selected["MAUBENHPHAMID"].ToString();
                getData_table_ChiTiet(1, null);
                string CONFIGMENU= selected["CONFIGMENU"].ToString();
                string ttmbp = selected["TRANGTHAIMAUBENHPHAM"].ToString();

                if (modeView =="0"  && ( ttmbp != "3" || CONFIGMENU == "1") )
                {
                    ucGrid_ChiTiet.addMenuPopup(Menu_Popup_ChiTiet_contextMenuDetail());
                    ucBarMenu1.addMenu(Menu_Popup_ChiTiet_contextMenuDetail());
                }
                else if (modeView == "1" || modeView == "3" || ttmbp == "3")
                {
                    if (deleteDV == "0")
                    {
                        ucGrid_ChiTiet.addMenuPopup(Menu_Popup_ChiTiet_contextMenuDetailPrint());
                        ucBarMenu1.addMenu(Menu_Popup_ChiTiet_contextMenuDetailPrint());
                    }
                    else if (deleteDV == "1")
                    {
                        ucGrid_ChiTiet.addMenuPopup(Menu_Popup_ChiTiet_contextMenuDetaiDeleteDV());
                        ucBarMenu1.addMenu(Menu_Popup_ChiTiet_contextMenuDetaiDeleteDV());
                    }
                }
            }
            else
                ucGrid_ChiTiet.clearData_frmTiepNhan();
        }

        string TRANGTHAIKETQUA = "";
        public void ChiTiet_SelectRow(object sender, EventArgs e)
        { 
        }
        private void clearAllGrid()
        {
            ucGrid_DanhSach.clearData();
            ucGrid_ChiTiet.clearData();
        }
        public void clearAllGrid_frmTiepNhan()
        {
            KHAMBENHID = "";
            ucGrid_DanhSach.clearData_frmTiepNhan();
            ucGrid_ChiTiet.clearData_frmTiepNhan();
        }

        private void MenuPopupClick_DanhSach(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DanhSach.gridView.GetFocusedRow());
                //  "RN": "1",
                //  "DOITUONGBENHNHANID": "1",
                //  "MABENHNHAN": "BN00000362",
                //  "TENBENHNHAN": "PHÙNG VĂN HƯƠNG",
                //  "TIEPNHANID": "3287",
                //  "HOSOBENHANID": "3446",
                //  "KHAMBENHID": "3099",
                //  "MAUBENHPHAMID": "12278",
                //  "BENHNHANID": "3301",
                //  "BARCODE": "0233",
                //  "SOPHIEU": "170604000001",
                //  "PHONGDIEUTRI": "103. Phòng khám sản",
                //  "PHONGCHIDINH": "Phòng xét nghiệm giải phẫu bệnh sinh thiết",
                //  "TRANGTHAIMAUBENHPHAM": "1",
                //  "SOTHUTUCHIDINH": "3466",
                //  "PHONGLAYMAU": "",
                //  "SOTHUTU_LAYMAU": null,
                //  "LOAIDONTHUOC": null,
                //  "SLTHANG": null,
                //  "PHONGCHUYENDENID": "4193",
                //  "NGUOITAO": "Quản trị hệ thống bệnh viện",
                //  "KHANCAP": "Bình thường",
                //  "KHOACHIDINH": "Khoa Giải phẫu bệnh",
                //  "KHOADIEUTRI": "Khoa Khám bệnh",
                //  "TENKHO": "",
                //  "LOAIPHIEU": "Nhận",
                //  "GHICHU": "",
                //  "PHONGID": "4125",
                //  "LOAIPHIEUMAUBENHPHAM": "1",
                //  "LOAIDICHVU": "BenhPham",
                //  "DICHVUCHA_ID": null,
                //  "NGUOITAO_ID": "1226",
                //  "TRANGTHAI_PHIEU": "Đang sửa phiếu",
                //  "TENNGHENGHIEP": "Công nhân",
                //  "DACODICHVUTHUTIEN": null,
                //  "KHOAID": "4001",
                //  "CHITIETGHICHU": "",
                //  "PHIEU_LINH": "",
                //  "NGUOITRAKETQUA": "",
                //  "PHIEUHEN": "0",
                //  "NGAYMAUBENHPHAM": "04/06/2017 14:04:28",
                //  "NGAYSINH": "10/10/1980 00:00:00",
                //  "NGAYMAUBENHPHAM_SUDUNG": "",
                //  "DIKEM": "0",
                //  "LOAIKEDON": "3",
                //  "PHIEUTRA_ID": null 
                if (drv != null)
                {
                    // codwe web trong NTU02D026_ThongTinChuyenKhoa.js --> hàm: 
                    if (menu.hlink == "sentRequest") // _sendRequest
                    {
                        string ret = ServiceTabDanhSachBenhNhan.Gui_yeu_cau(modeView, drv, KHOAID);
                        if (ret.StartsWith("TRUE "))
                        {
                            MessageBox.Show(ret.Substring("TRUE ".Length));
                            reload();
                        }
                        else
                            MessageBox.Show(ret, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (menu.hlink == "deleteRequest") // _deleteRequest
                    {
                        // check quyen xoa du lieu
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền hủy yêu cầu phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        int _trangthai = 0;
                        if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                        if (_trangthai == 2)
                        {
                            int _sophieudikem = 0;
                            DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NTU.024.4", drv["MAUBENHPHAMID"].ToString());
                            if (dt.Rows.Count > 0 && dt.Columns.Contains("SOPHIEUDIKEM"))
                                _sophieudikem = Func.Parse(dt.Rows[0]["SOPHIEUDIKEM"].ToString());

                            if (_sophieudikem > 0)
                            {
                                MessageBox.Show("Phiếu phẫu thuật thủ thuật đã có phiếu thuốc đi kèm,\n không được phép hủy yêu cầu", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                            string _return = RequestHTTP.call_ajaxCALL_SP_I("CLS.DEL.SENT.REQ", drv["MAUBENHPHAMID"].ToString() + "$1$1");
                            if (_return == "1")
                            {
                                MessageBox.Show("Phiếu đã được hủy yêu cầu thành công!");
                                reload();
                            }
                            else if (_return == "0")
                                MessageBox.Show("Hủy yêu cầu phiếu thất bại!");
                            else if (_return == "-1")
                                MessageBox.Show("Phiếu PTTT đã thu tiền nên không được hủy yêu cầu");
                        }
                        else if (_trangthai == 1)
                        {
                            MessageBox.Show("Phiếu đã được hủy yêu cầu!");
                        }
                        else if (_trangthai > 2)
                        {
                            MessageBox.Show("Phiếu đã được xử lý nên không thể hủy yêu cầu!");
                        }
                    }
                    else if (menu.hlink == "delete") // _xoaPhieuDichVu
                    {
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền xóa phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        int _trangthai = 0;
                        if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                        if (_trangthai == 1)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa phiếu phẫu thuật thủ thuật không?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL.PDV.024", drv["MAUBENHPHAMID"].ToString());
                                if (_return == "1")
                                {
                                    MessageBox.Show("Xóa thành công Phiếu phẫu thuật thủ thuật");
                                    reload();
                                }
                                else if (_return == "0")
                                {
                                    MessageBox.Show("Xóa không thành công phiếu Phẫu thuật thủ thuật");
                                }
                                else if (_return == "-1")
                                {
                                    MessageBox.Show("Phiếu Phẫu thuật thủ thuật đã thu tiền nên không được phép xóa");
                                }
                            }
                        }
                        else if (_trangthai >= 2)
                        {
                            MessageBox.Show("Phiếu đã được xử lý nên không thể xóa!");
                        }
                    }
                    else if (menu.hlink == "updatePhieuPTTT") // _updatePhieuChuyenKhoa
                    {
                        // check quyen xoa du lieu
                        int _trangthai = 0;
                        if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                        string mess = RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID, _trangthai);

                        if (mess != "")
                        {
                            MessageBox.Show(mess, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "capnhatPhieuPTTT";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "copyNote") // _copyPhieuChuyenKhoa
                    {
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền tạo bản sao cho phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        NTU02D070_ThoiGianDonThuoc frm = new NTU02D070_ThoiGianDonThuoc();
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), "5");
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "editOrg") // _editOrgDone
                    {
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền sửa phòng thực hiện phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        int _trangthai = 0;
                        if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                        if (_trangthai == 3)
                        {
                            MessageBox.Show("Phiếu đã hoàn thành nên bạn không thể sửa phòng thực hiện");
                            return;
                        }

                        NTU02D038_SuaPhongThucHien frm = new NTU02D038_SuaPhongThucHien();
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), lnmbp, "29");
                        frm.setReturnData(Return_NTU02D038_SuaPhongThucHien);
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "printChiDinhAuto") // _exportPXNCDAuto
                    {
                        var _type = "pdf";
                        var pars = new List<object>() { "HIS_FILEEXPORT_TYPE" };
                        var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", string.Join("$", pars));
                        if (!"-1".Equals(data_ar))
                        {
                            _type = data_ar;
                        }

                        var hopital_id = Const.local_user.HOSPITAL_ID;
                        var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                        var soPhieu = drv["SOPHIEU"].ToString();
                        var doiTuongBenhNhan = drv["DOITUONGBENHNHANID"].ToString();

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("maubenhphamid", "String", mauBenhPhamId);

                        if ("965".Equals(hopital_id))
                        {
                            var rpName = "VNPTHIS_IN_A4_";
                            rpName += soPhieu;
                            rpName += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                            rpName += "." + _type;

                            if ("1".Equals(doiTuongBenhNhan))
                            {
                                var array = new List<object>()
                                {
                                    mauBenhPhamId
                                };

                                var arr_loaidichvu = RequestHTTP.call_ajaxCALL_SP_O("NTU02D075_LOAIDICHVU", string.Join("$", array), 0);
                                string _loaidichvu = "";
                                if (arr_loaidichvu != null && arr_loaidichvu.Rows.Count > 0)
                                {
                                    for (var i = 0; i < arr_loaidichvu.Rows.Count; i++)
                                    {
                                        _loaidichvu = arr_loaidichvu.Rows[i]["BHYT"].ToString();
                                        if ("1".Equals(_loaidichvu))
                                        {
                                            string url = Func.getUrlReport("PHIEU_PTTT_A4_965", table, _type);
                                            Func.SaveFileFromUrl(url, rpName);

                                            PrintPreview("", "PHIEU_PTTT_A4_965", table);

                                        }
                                        else
                                        {
                                            string url = Func.getUrlReport("PHIEU_PTTTDICHVU_A4_965", table, _type);
                                            Func.SaveFileFromUrl(url, rpName);

                                            PrintPreview("", "PHIEU_PTTTDICHVU_A4_965", table);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string url = Func.getUrlReport("PHIEU_PTTTDICHVU_A4_965", table, _type);
                                Func.SaveFileFromUrl(url, rpName);

                                PrintPreview("", "PHIEU_PTTTDICHVU_A4_965", table);
                            }
                        }
                        else
                        {
                            var rpName = "VNPTHIS_IN_A4_";
                            rpName += soPhieu;
                            rpName += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                            rpName += "." + _type;

                            string url = Func.getUrlReport("PHIEU_XETNGHIEM", table, _type);
                            Func.SaveFileFromUrl(url, rpName);

                            PrintPreview("", "PHIEU_PHAUTHUAT_A4", table);
                        }
                    }
                    else if (menu.hlink == "deleteAllDVPTTT") // _deleteAllDVCK
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa HẾT dịch vụ phẫu thuật thủ thuật không?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataTable dt = (DataTable)ucGrid_ChiTiet.gridControl.DataSource;
                            string strJSON = "[";
                            for (var i = 0; i < dt.Rows.Count; i++)
                            {
                                string json = "";
                                json += Func.json_item("DICHVUKHAMBENHID", dt.Rows[i]["DICHVUKHAMBENHID"].ToString());
                                json += Func.json_item("MAUBENHPHAMID", dt.Rows[i]["MAUBENHPHAMID"].ToString());
                                json = Func.json_item_end(json);
                                json = json.Replace("\"", "\\\"");

                                strJSON += json + ",";
                            }
                            strJSON = strJSON.Substring(0, strJSON.Length - 1) + "]";

                            // {"func":"ajaxCALL_SP_I","params":["NT.DELETE_DVCK_ALL","[{\"DICHVUKHAMBENHID\":\"659034\",\"MAUBENHPHAMID\":\"310752\"}]"],"uuid"
                            string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DELETE_DVCK_ALL", strJSON);
                            if (_return == "1")
                            {
                                MessageBox.Show("Xóa thành công dịch vụ phẫu thuật thủ thuật");
                                reload();
                            }
                            else if (_return == "0")
                            {
                                MessageBox.Show("Xóa không thành công dịch vụ phẫu thuật thủ thuật");
                            }
                            else if (_return == "-1")
                            {
                                MessageBox.Show("Dịch vụ đã thu tiền nên không được phép xóa");
                            }
                            else if (_return == "-2")
                            {
                                MessageBox.Show("Bệnh nhân đã ra viên nên không được phép xóa dịch vụ, Bạn phải mở lại bệnh án để sửa!");
                            }
                            //HaNv_20170801: check phiếu dịch vụ cls đã trả kết quả không cho phép xóa
                            else if (_return == "-3")
                            {
                                MessageBox.Show("Phiếu dịch vụ đã trả kết quả nên không được phép xóa!");
                            }
                            //End HaNv_20170801
                        }
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void PrintPreview(string title, string code, DataTable parTable, int height = 650, int width = 900)
        {

            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(title, code, parTable, height, width);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void Return_NTU02D038_SuaPhongThucHien(object sender, EventArgs e)
        {
            reload();
        }
        private void MenuPopupClick_ChiTiet(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_ChiTiet.gridView.GetFocusedRow());
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
                if (drv != null)
                {
                    if (menu.hlink == "PTTTChinh") // _updateLoaiPTTT(rowKey,0);
                    {
                        _updateLoaiPTTT(drv, 0);
                    }
                    else if (menu.hlink == "PTTTPhu1") // _updateLoaiPTTT(rowKey,1);
                    {
                        _updateLoaiPTTT(drv, 1);
                    }
                    else if (menu.hlink == "PTTTPhu2") // _updateLoaiPTTT(rowKey,2);
                    {
                        _updateLoaiPTTT(drv, 2);
                    }
                    else if (menu.hlink == "PTTTKhongThanhToanDT") // _updatePTTTKhongThanhToanDT(rowKey,2);
                    {
                        _updatePTTTKhongThanhToanDT(drv);
                    }
                    else if (menu.hlink == "updateTTPTTT") // _updatePTTT(rowKey); 
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        _updatePTTT(drv, rowDataCk);
                    }
                    else if (menu.hlink == "DSPTVT_KEM") // _dsPhieuThuocVatTuDiKem(rowKey);
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        _dsPhieuThuocVatTuDiKem(drv, rowDataCk);
                    }
                    else if (menu.hlink == "TAOPHIEUTHUOCKEM") // _taoPhieuThuocDiKem(rowKey);
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        if (rowDataCk["KHOAID"].ToString() != rowDataCk["KHOACHUYENDENID"].ToString())
                        {
                            MessageBox.Show("Không thể kê thuốc đi kèm khi bệnh nhân không nằm trong khoa");
                            return;
                        }

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "tao_phieu_thuoc_di_kem";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "TAOPHIEUVATTUKEM") // _taoPhieuVatTuDiKem(rowKey);
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        if (rowDataCk["KHOAID"].ToString() != rowDataCk["KHOACHUYENDENID"].ToString())
                        {
                            MessageBox.Show("Không thể kê thuốc đi kèm khi bệnh nhân không nằm trong khoa");
                            return;
                        }

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "tao_phieu_vat_tu_di_kem";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "TAOPHIEUTHUOCKEM_HAOPHI") // _taoPhieuThuocDiKem_haophi(rowKey);
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        if (rowDataCk["KHOAID"].ToString() != rowDataCk["KHOACHUYENDENID"].ToString())
                        {
                            MessageBox.Show("Không thể kê thuốc đi kèm khi bệnh nhân không nằm trong khoa");
                            return;
                        }

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "tao_phieu_thuoc_di_kem_hao_phi";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "TAOPHIEUVATTUKEM_HAOPHI") // _taoPhieuVatTuDiKem_haophi(rowKey);
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        if (rowDataCk["KHOAID"].ToString() != rowDataCk["KHOACHUYENDENID"].ToString())
                        {
                            MessageBox.Show("Không thể kê thuốc đi kèm khi bệnh nhân không nằm trong khoa");
                            return;
                        }

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "tao_phieu_vat_tu_di_kem_hao_phi";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    //else if (menu.hlink == "DSPPThu") // _getDanhsachphieuphuthu(rowKey);
                    //{
                    //    NTU02D040_DanhSachPhieuPhuThu frm = new NTU02D040_DanhSachPhieuPhuThu();
                    //    frm.setData(drv["KHAMBENHID"].ToString(), drv["DICHVUKHAMBENHID"].ToString());
                    //    openForm(frm, "1");
                    //}
                    else if (menu.hlink == "TPPThu") // _taophieuphuthu(rowKey);
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = rowDataCk;
                        data.key = "tao_phieu_phu_thu";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "printView") // _exportPTTT(rowKey);
                    {
                        var dichVuKhamBenhId = drv["DICHVUKHAMBENHID"].ToString();
                        var benhNhanId = this.BENHNHANID;

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("dichvukhambenhid", "String", dichVuKhamBenhId);
                        table.Rows.Add("benhnhanid", "String", benhNhanId);

                        PrintPreview("", "NTU030_PHIEUPHAUTHUATTHUTHUAT_14BV01_QD4069_A4", table);
                    }
                    else if (menu.hlink == "printPhuThuView") // _exportPhuthu(rowKey);
                    {
                        var dichVuKhamBenhId = drv["DICHVUKHAMBENHID"].ToString();

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("dichvukhambenhid", "String", dichVuKhamBenhId);

                        PrintPreview("", "NGT040_GIAITRINHPHUTHU_A4", table);
                    }
                    else if (menu.hlink == "printChuyenKhoaView") // _exportChuyenKhoa(rowKey);
                    {
                        var dichVuKhamBenhId = drv["DICHVUKHAMBENHID"].ToString();
                        var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                        var benhNhanId = this.BENHNHANID;
                        var khamBenhId = drv["KHAMBENHID"].ToString();

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("i_dichvukhambenhid", "String", dichVuKhamBenhId);
                        table.Rows.Add("i_maubenhphamid", "String", mauBenhPhamId);
                        table.Rows.Add("i_benhnhanid", "String", benhNhanId);
                        table.Rows.Add("i_khambenhid", "String", khamBenhId);

                        PrintPreview("", "NTU010_PHIEUKHAMCHUYENKHOA", table);
                    }
                    else if (menu.hlink == "printCamDoanPTTT") // _exportCamDoanPTTT(rowKey);
                    {
                        var dichVuKhamBenhId = drv["DICHVUKHAMBENHID"].ToString();
                        var khamBenhId = drv["KHAMBENHID"].ToString();

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("i_khambenhid", "String", khamBenhId);
                        table.Rows.Add("i_dichvukhambenhid", "String", dichVuKhamBenhId);

                        PrintPreview("", "NTU027_GIAYCAMDOANCHAPNHANPTTT_A4_944", table);
                    }
                    else if (menu.hlink == "DSPTVT_KEM_View") // _dsPhieuThuocVatTuDiKem(rowKey);
                    {
                        DataRowView rowDataCk = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();
                        _dsPhieuThuocVatTuDiKem(drv, rowDataCk);
                    }
                    //else if (menu.hlink == "DSPPThuView") // _getDanhsachphieuphuthu(rowKey);  
                    //{
                    //    NTU02D040_DanhSachPhieuPhuThu frm = new NTU02D040_DanhSachPhieuPhuThu();
                    //    frm.setData(drv["KHAMBENHID"].ToString(), drv["DICHVUKHAMBENHID"].ToString());
                    //    openForm(frm, "1");
                    //}
                    else if (menu.hlink == "deleteDV") // _deleteDVCK(rowKey);
                    {
                        _deleteDVCK(drv);
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


        private List<MenuFunc> Menu_Popup_DanhSach_contextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xử lý yêu cầu", "", "", ""));

            listMenu.Add(new MenuFunc("Gửi yêu cầu", "sentRequest", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Hủy yêu cầu", "deleteRequest", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Cập nhật phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa", "delete", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Cập nhật phiếu PTTT", "updatePhieuPTTT", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo bản sao", "copyNote", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Sửa phòng thực hiện", "editOrg", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In phiếu chỉ định", "printChiDinhAuto", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_DanhSach_contextMenuPrint()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In phiếu chỉ định", "printChiDinhAuto", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Xóa tất cả dịch vụ", "deleteAllDVPTTT", "0", "barButtonItem3.Glyph.png"));

            return listMenu;
        }

        private List<MenuFunc> Menu_Popup_ChiTiet_contextMenuDetail()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Thông tin và Loại PTTT", "", "", ""));

            listMenu.Add(new MenuFunc("PTTT chính", "PTTTChinh", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("PTTT phụ không thay ekip mổ", "PTTTPhu1", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("PTTT phụ có thay ekip mổ", "PTTTPhu2", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("PTTT miễn giảm TT đồng thời", "PTTTKhongThanhToanDT", "0", "barButtonItem3.Glyph.png"));
            //listMenu.Add(new MenuFunc("Cập nhật thông tin PTTT", "updateTTPTTT", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Thuốc, vật tư đi kèm", "", "", ""));

            listMenu.Add(new MenuFunc("Danh sách phiếu thuốc đi kèm", "DSPTVT_KEM", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo phiếu thuốc đi kèm", "TAOPHIEUTHUOCKEM", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo phiếu vật tư đi kèm", "TAOPHIEUVATTUKEM", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo phiếu thuốc đi kèm hao phí", "TAOPHIEUTHUOCKEM_HAOPHI", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo phiếu vật tư đi kèm hao phí", "TAOPHIEUVATTUKEM_HAOPHI", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Phụ thu", "", "", ""));

            //listMenu.Add(new MenuFunc("Danh sách phiếu phụ thu", "DSPPThu", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo phiếu phụ thu", "TPPThu", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In phiếu PTTT", "printView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In giải trình phụ thu", "printPhuThuView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In phiếu chuyên khoa", "printChuyenKhoaView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In giấy cam đoan PTTT", "printCamDoanPTTT", "0", "barButtonItem3.Glyph.png"));

            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_ChiTiet_contextMenuDetailPrint()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Thuốc, vật tư đi kèm", "", "", ""));

            listMenu.Add(new MenuFunc("Danh sách phiếu thuốc đi kèm", "DSPTVT_KEM_View", "0", "barButtonItem3.Glyph.png"));

            //listMenu.Add(new MenuFunc("Phụ thu", "", "", ""));

            //listMenu.Add(new MenuFunc("Danh sách phiếu phụ thu", "DSPPThuView", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In phiếu PTTT", "printView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In giải trình phụ thu", "printPhuThuView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In phiếu chuyên khoa", "printChuyenKhoaView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In giấy cam đoan PTTT", "printCamDoanPTTT", "0", "barButtonItem3.Glyph.png"));

            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_ChiTiet_contextMenuDetaiDeleteDV()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xóa dịch vụ", "", "", ""));
            listMenu.Add(new MenuFunc("Xóa", "deleteDV", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In phiếu PTTT", "printView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In giải trình phụ thu", "printPhuThuView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In phiếu chuyên khoa", "printChuyenKhoaView", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In giấy cam đoan PTTT", "printCamDoanPTTT", "0", "barButtonItem3.Glyph.png"));

            return listMenu;
        }

        private void _updateLoaiPTTT(DataRowView rowData, int loaiPTTT)
        {
            if (rowData.DataView.Table.Columns.Contains("DICHVUKHAMBENHID") && rowData["DICHVUKHAMBENHID"].ToString() != "")
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT026.UPD.LOAI.PTTT", rowData["DICHVUKHAMBENHID"].ToString() + "$" + loaiPTTT);
                if (_return == "1")
                {
                    MessageBox.Show("Cập nhật thành công loại PTTT!");
                    getData_table_ChiTiet(1, null);
                }
                else if (_return == "0")
                {
                    MessageBox.Show("Cập nhật thất bại loại PTTT!");
                }
            }
        }
        private void _updatePTTTKhongThanhToanDT(DataRowView rowData)
        {
            if (rowData.DataView.Table.Columns.Contains("DICHVUKHAMBENHID") && rowData["DICHVUKHAMBENHID"].ToString() != "")
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT026.EV003", rowData["DICHVUKHAMBENHID"].ToString());
                if (_return == "1")
                {
                    MessageBox.Show("Cập nhật thành công dịch vụ miễn giảm thanh toán đồng thời");
                    getData_table_ChiTiet(1, null);
                }
                else if (_return == "0")
                {
                    MessageBox.Show("Cập nhật không thành công dịch vụ miễn giảm thanh toán đồng thời");
                }
                else if (_return == "2")
                {
                    MessageBox.Show("Bệnh nhân không phải là đối tượng BHYT hoặc không phải dịch vụ bảo hiểm");
                }
                else if (_return == "3")
                {
                    MessageBox.Show("Không tồn tại dịch vụ chính trong phiếu");
                }
                else if (_return == "4")
                {
                    MessageBox.Show("Dịch vụ đã thu tiền");
                }
                else if (_return == "5")
                {
                    MessageBox.Show("Bệnh nhân đã kết thúc điều trị. Không thể cập nhật");
                }
            }
        }
        private void _updatePTTT(DataRowView rowData, DataRowView rowDataCk)
        {
            //   Check da thu tien hoac tam ung du tien                
            string data_dv_kb = RequestHTTP.call_ajaxCALL_SP_I("LAYDV_KB", rowData["DICHVUKHAMBENHID"].ToString() + "$" + rowDataCk["TIEPNHANID"].ToString());
            if (data_dv_kb == "0")
            {
                MessageBox.Show("Chưa thanh toán tiền cho dịch vụ này");
                return;
            }

            if (rowDataCk["PHONGCHUYENDENID"].ToString() != Const.local_phongId.ToString()
                && rowDataCk["PHONGCHUYENDENID"].ToString() != Const.local_phongId.ToString())
            {
                MessageBox.Show("Người dùng không phải thuộc phòng thực hiện nên không được phép cập nhật!");
                return;
            }

            NTU02D037_PhauthuatThuThuat frm = new NTU02D037_PhauthuatThuThuat();
            string THOIGIANVAOVIEN = ""; // ngoại trú THOIGIANVAOVIEN = ""
            frm.setData(rowDataCk["MABENHNHAN"].ToString(), rowDataCk["KHAMBENHID"].ToString(), rowDataCk["TENBENHNHAN"].ToString(), rowDataCk["NGAYSINH"].ToString()
                , rowDataCk["TENNGHENGHIEP"].ToString(), rowDataCk["HOSOBENHANID"].ToString(), rowDataCk["TIEPNHANID"].ToString(), Const.local_phongId.ToString()
                 , rowDataCk["BENHNHANID"].ToString(), rowDataCk["KHOAID"].ToString(), rowDataCk["PHONGDIEUTRI"].ToString()
                 , THOIGIANVAOVIEN, rowData["MAUBENHPHAMID"].ToString(), rowData["DICHVUKHAMBENHID"].ToString());

            openForm(frm, "1");
        }
        private void _dsPhieuThuocVatTuDiKem(DataRowView rowData, DataRowView rowDataCk)
        {
            NTU02D078_DanhSachPhieuThuocVatTu frm = new NTU02D078_DanhSachPhieuThuocVatTu();
            frm.setData(rowDataCk["KHAMBENHID"].ToString(), rowDataCk["BENHNHANID"].ToString(), rowDataCk["TRANGTHAIKHAMBENH"].ToString(), rowData["DICHVUKHAMBENHID"].ToString());
            frm.setEvent_BackParentForm(BackParentForm);
            openForm(frm, "1");
        }

        private void BackParentForm(object sender, EventArgs e)
        {
            try
            {
                var data = (ojbDatarowview)sender;
                if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void _deleteDVCK(DataRowView rowData)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa dịch vụ phẫu thuật thủ thuật không?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DELETE_DVCK", rowData["DICHVUKHAMBENHID"].ToString() + "$" + rowData["MAUBENHPHAMID"].ToString());
                if (_return == "1")
                {
                    MessageBox.Show("Xóa thành công dịch vụ phẫu thuật thủ thuật");
                    reload();
                }
                else if (_return == "0")
                {
                    MessageBox.Show("Xóa không thành công dịch vụ phẫu thuật thủ thuật");
                }
                else if (_return == "-1")
                {
                    MessageBox.Show("Dịch vụ đã thu tiền nên không được phép xóa");
                }
                else if (_return == "-2")
                {
                    MessageBox.Show("Bệnh nhân đã ra viên nên không được phép xóa dịch vụ, Bạn phải mở lại bệnh án để sửa!");
                }
                // check phiếu dịch vụ cls đã trả kết quả không cho phép xóa
                else if (_return == "-3")
                {
                    MessageBox.Show("Phiếu dịch vụ đã trả kết quả nên không được phép xóa!");
                }
            }
        }

    }
}
