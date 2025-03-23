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
using VNPT.HIS.Controls.SubForm;
using System.Reflection;

namespace VNPT.HIS.Controls.NgoaiTru
{
    //
    // code trên web: NTU02D034_ThongTinVatTu
    //
    public partial class ucTabVatTu : DevExpress.XtraEditors.XtraUserControl
    {
        private string CAU_HINH = "";

        private string KHAMBENHID = "";
        private string HOSOBENHANID = "";
        private string BENHNHANID = "";

        private string lnmbp = "8";
        private string modeView = "0";
        private string hosobenhanid = "";

        // biến dùng cho Nội trú
        string KHOAID = "";
        public ucTabVatTu()
        {
            InitializeComponent();
        }
        protected EventHandler event_BackParentForm;
        public void setEvent_BackParentForm(EventHandler event_BackParentForm)
        {
            this.event_BackParentForm = event_BackParentForm;
        }
        private void ucTabVatTu_Load(object sender, EventArgs e)
        {
            ucGrid_DanhSach.setEvent_FocusedRowChanged(DanhSach_SelectRow);
            ucGrid_DanhSach.setEvent(getData_table);

            ucGrid_ChiTiet.setEvent_FocusedRowChanged(ChiTiet_SelectRow);
            ucGrid_ChiTiet.setEvent(getData_table_ChiTiet);

            ucGrid_DanhSach.gridView.OptionsView.ShowAutoFilterRow = false;// ô search
            ucGrid_ChiTiet.gridView.OptionsView.ShowAutoFilterRow = false;// ô search

            ucGrid_DanhSach.setEvent_MenuPopupClick(MenuPopupClick_DanhSach);
        }

        public bool allow_tab_reload = false;
        public void loadData(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string _hosobenhanid)
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
            else if (modeView == "2")
            {
                ucGrid_DanhSach.addMenuPopup(Menu_Popup_DanhSach_contextMenuDTKH());
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
                //    [{"RN": "1","CONFIGMENU": "0","DOITUONGBENHNHANID": "1","MABENHNHAN": "BN00042275","TENBENHNHAN": "TTT003","TIEPNHANID": "95924","HOSOBENHANID": "118214"
                //,"KHAMBENHID": "100933","MAUBENHPHAMID": "310709","BENHNHANID": "45695","BARCODE": "","SOPHIEU": "P000001238"
                //,"PHONGDIEUTRI": "Phòng 28: Ngoại tổng quát (K128)","PHONGCHIDINH": "","TRANGTHAIMAUBENHPHAM": "2","SOTHUTUCHIDINH": "129493","PHONGLAYMAU": ""
                //,"SOTHUTU_LAYMAU": "0","LOAIDONTHUOC": null,"SLTHANG": "1","PHONGCHUYENDENID": "0","NGUOITAO": "Quản trị hệ thống bệnh viện mức 1","KHANCAP": ""
                //,"KHOACHIDINH": "","KHOADIEUTRI": "Khoa Khám bệnh","TENKHO": "Kho VTYT-Hóa chất","LOAIPHIEU": "Nhận","LOAIKHO": "7","GHICHU": "","PHONGID": "4970"
                //,"LOAIPHIEUMAUBENHPHAM": "1","LOAIDICHVU": "PhieuVatTu","DICHVUCHA_ID": null,"NGUOITAO_ID": "10136","TRANGTHAI_PHIEU": "Đã gửi phiếu"
                //,"TENNGHENGHIEP": "Nhân dân","DACODICHVUTHUTIEN": "0","KHOAID": "4902","KHOACHUYENDENID": "0","CHITIETGHICHU": "","PHIEU_LINH": "","NGUOITRAKETQUA": ""
                //,"PHIEUHEN": "0","NGAYMAUBENHPHAM": "08/03/2018 09:57:17","NGAYSINH": "","NGAYMAUBENHPHAM_SUDUNG": "08/03/2018 09:57:17","DIKEM": "0"
                //,"TRANGTHAIKHAMBENH": "4","LOAIKEDON": "1","PHIEUTRA_ID": null,"SOPHIEUTRA": "","PHIEU_DTRI": ""}]

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TRANGTHAIMAUBENHPHAM", "SOPHIEU", "PHIEU_LINH", "NGUOITAO", "PHONGDIEUTRI"
                        , "NGAYMAUBENHPHAM", "NGAYMAUBENHPHAM_SUDUNG", "TENKHO", "SOTHUTUCHIDINH", "DIKEM", "LOAIPHIEU", "TRANGTHAI_PHIEU" });

                {
                    ucGrid_DanhSach.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DanhSach.setColumnAll(false);

                    ucGrid_DanhSach.setColumn("RN", 0, " ");
                    ucGrid_DanhSach.setColumn("TRANGTHAIMAUBENHPHAM", 1, " ");
                    ucGrid_DanhSach.setColumn("SOPHIEU", 2, "Số phiếu");
                    ucGrid_DanhSach.setColumn("PHIEU_LINH", 3, "Phiếu lĩnh");
                    ucGrid_DanhSach.setColumn("NGUOITAO", 4, "Bác sỹ chỉ định");
                    ucGrid_DanhSach.setColumn("PHONGDIEUTRI", 5, "Phòng");
                    ucGrid_DanhSach.setColumn("NGAYMAUBENHPHAM", 6, "Ngày chỉ định");
                    ucGrid_DanhSach.setColumn("NGAYMAUBENHPHAM_SUDUNG", 7, "Ngày sử dụng");
                    ucGrid_DanhSach.setColumn("TENKHO", 8, "Kho");
                    ucGrid_DanhSach.setColumn("SOTHUTUCHIDINH", 10, "STT");
                    ucGrid_DanhSach.setColumn("DIKEM", 9, "Đi kèm");
                    ucGrid_DanhSach.setColumn("LOAIPHIEU", 11, "Loại phiếu");
                    ucGrid_DanhSach.setColumn("TRANGTHAI_PHIEU", 12, "Trạng thái");

                    ucGrid_DanhSach.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "3", "4" }
                        , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });
                    ucGrid_DanhSach.setColumnImage("DIKEM", new String[] { "1" }
                        , new String[] { "./Resources/Pin.png" });

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

                ResponsList ds_KQ = ServiceTabDanhSachBenhNhan.getChiTietPhieuThuoc(page, ucGrid_ChiTiet.ucPage1.getNumberPerPage(), MAUBENHPHAMID);
                DataTable dt_KQ = MyJsonConvert.toDataTable(ds_KQ.rows);

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "RN", "MADICHVU", "TENDICHVU", "SOLUONG"
                        , "DVT", "NGAYDUNG", "DUONGDUNG", "HUONGDANSUDUNG", "LOAIDOITUONG" });

                {
                    ucGrid_ChiTiet.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGrid_ChiTiet.setColumnAll(false);

                    ucGrid_ChiTiet.setColumn("RN", 0, " ");
                    ucGrid_ChiTiet.setColumn("MADICHVU", 1, "Mã vật tư");
                    ucGrid_ChiTiet.setColumn("TENDICHVU", 2, "Tên vật tư");
                    ucGrid_ChiTiet.setColumn("SOLUONG", 3, "Số lượng");
                    ucGrid_ChiTiet.setColumn("DVT", 4, "ĐVT");
                    ucGrid_ChiTiet.setColumn("NGAYDUNG", 5, "Ngày dùng");
                    ucGrid_ChiTiet.setColumn("DUONGDUNG", 6, "Đường dùng");
                    ucGrid_ChiTiet.setColumn("HUONGDANSUDUNG", 7, "Hướng dẫn sử dụng");
                    ucGrid_ChiTiet.setColumn("LOAIDOITUONG", 8, "Loại thanh toán");

                    ucGrid_ChiTiet.gridView.BestFitColumns(true);
                }
            }
        }
        private void clearAllGrid()
        {
            ucGrid_DanhSach.clearData();
            ucGrid_ChiTiet.clearData();
        }

        string MAUBENHPHAMID = "";
        public void DanhSach_SelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
                MAUBENHPHAMID = selected["MAUBENHPHAMID"].ToString();
                getData_table_ChiTiet(1, null);
            }
            else
            {
                ucGrid_ChiTiet.clearData_frmTiepNhan();
            }
        }
        public void ChiTiet_SelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
            }
        }

        private void MenuPopupClick_DanhSach(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DanhSach.gridView.GetFocusedRow());
                //    [{"RN": "1","CONFIGMENU": "0","DOITUONGBENHNHANID": "1","MABENHNHAN": "BN00042275","TENBENHNHAN": "TTT003","TIEPNHANID": "95924","HOSOBENHANID": "118214"
                //,"KHAMBENHID": "100933","MAUBENHPHAMID": "310709","BENHNHANID": "45695","BARCODE": "","SOPHIEU": "P000001238"
                //,"PHONGDIEUTRI": "Phòng 28: Ngoại tổng quát (K128)","PHONGCHIDINH": "","TRANGTHAIMAUBENHPHAM": "2","SOTHUTUCHIDINH": "129493","PHONGLAYMAU": ""
                //,"SOTHUTU_LAYMAU": "0","LOAIDONTHUOC": null,"SLTHANG": "1","PHONGCHUYENDENID": "0","NGUOITAO": "Quản trị hệ thống bệnh viện mức 1","KHANCAP": ""
                //,"KHOACHIDINH": "","KHOADIEUTRI": "Khoa Khám bệnh","TENKHO": "Kho VTYT-Hóa chất","LOAIPHIEU": "Nhận","LOAIKHO": "7","GHICHU": "","PHONGID": "4970"
                //,"LOAIPHIEUMAUBENHPHAM": "1","LOAIDICHVU": "PhieuVatTu","DICHVUCHA_ID": null,"NGUOITAO_ID": "10136","TRANGTHAI_PHIEU": "Đã gửi phiếu"
                //,"TENNGHENGHIEP": "Nhân dân","DACODICHVUTHUTIEN": "0","KHOAID": "4902","KHOACHUYENDENID": "0","CHITIETGHICHU": "","PHIEU_LINH": "","NGUOITRAKETQUA": ""
                //,"PHIEUHEN": "0","NGAYMAUBENHPHAM": "08/03/2018 09:57:17","NGAYSINH": "","NGAYMAUBENHPHAM_SUDUNG": "08/03/2018 09:57:17","DIKEM": "0"
                //,"TRANGTHAIKHAMBENH": "4","LOAIKEDON": "1","PHIEUTRA_ID": null,"SOPHIEUTRA": "","PHIEU_DTRI": ""}]
                if (drv != null)
                {
                    // codwe web trong NTU02D034_ThongTinVatTu.js --> hàm: 
                    if (menu.hlink == "sentRequest" || menu.hlink == "sentRequestDtkh") //  _sendRequest(rowKey);
                    {
                        _sendRequest(drv);
                    }
                    else if (menu.hlink == "travattu") // _phieuTraVT(rowKey);
                    {
                        if (drv["TRANGTHAIMAUBENHPHAM"].ToString() != "6" && drv["LOAIKHO"].ToString() != "9" && drv["LOAIKHO"].ToString() != "13")
                        {
                            MessageBox.Show("Phiếu vật tư không trả được vì trạng thái phiếu không phải là đã nhận", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        if (drv["TRANGTHAIMAUBENHPHAM"].ToString() == "1")
                        {
                            MessageBox.Show("Trạng thái phiếu đang sửa nên không trả được", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() != "1")
                        {
                            MessageBox.Show("Đây không phải là phiếu nhận nên không trả được.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "travattu";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "deleteRequest") // _deleteRequest(rowKey);
                    {
                        _deleteRequest(drv);
                    }
                    else if (menu.hlink == "delete" || menu.hlink == "deleteDtkh") // _deletePhieuThuocVatTu(rowKey);
                    {
                        _deletePhieuThuocVatTu(drv);
                    }
                    else if (menu.hlink == "updatePHIEUVATTU") // _updatePhieuVatTu(rowKey);
                    {
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
                        data.key = "updatePHIEUVATTU";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "copyNote") // _copyPhieuThuocVatTu(rowKey);
                    {
                        // check quyen xoa du lieu
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền tạo bản sao cho phiếu này!");
                            return;
                        }

                        NTU02D070_ThoiGianDonThuoc frm = new NTU02D070_ThoiGianDonThuoc();
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), "8");
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "chidinhLPDK") // _createNoteAttach(rowKey);
                    {
                        NTU02D003_DichVuDinhKem frm = new NTU02D003_DichVuDinhKem();
                        frm.setData(drv["TIEPNHANID"].ToString(), drv["MAUBENHPHAMID"].ToString());
                        openForm(frm, "1");
                    }
                    //else if (menu.hlink == "editOrg") // _editOrgDone(rowKey);
                    //{
                    //    // check quyen xoa du lieu
                    //    if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                    //    {
                    //        MessageBox.Show("Bạn không có quyền sửa phòng chỉ định phiếu này!");
                    //        return;
                    //    }
                    //    int _trangthai = 0;
                    //    if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                    //    {
                    //        MessageBox.Show("Phiếu đã hoàn thành nên bạn không thể sửa phòng chỉ định");
                    //        return;
                    //    }

                    //    NTU02D039_SuaPhongChiDinh frm = new NTU02D039_SuaPhongChiDinh();
                    //    frm.setData(drv["MAUBENHPHAMID"].ToString(), drv["PHONGID"].ToString(), "3", lnmbp);
                    //    openForm(frm, "1");
                    //}
                    else if (menu.hlink == "print") // _inDonThuoc(rowKey);
                    {
                        var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("maubenhphamid", "String", mauBenhPhamId);

                        PrintPreview("", "PHIEU_VATTU_A4", table);
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

        private List<MenuFunc> Menu_Popup_DanhSach_contextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xử lý yêu cầu", "", "", ""));

            listMenu.Add(new MenuFunc("Gửi phiếu vật tư", "sentRequest", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Trả vật tư", "travattu", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Hủy phiếu vật tư", "deleteRequest", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Cập nhật phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa", "delete", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Cập nhật phiếu vật tư", "updatePHIEUVATTU", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo bản sao phiếu vật tư", "copyNote", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Chỉ định là phiếu đi kèm", "chidinhLPDK", "0", "barButtonItem3.Glyph.png"));
            //listMenu.Add(new MenuFunc("Sửa phòng chỉ định", "editOrg", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In phiếu vật tư", "print", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_DanhSach_contextMenuPrint()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In phiếu vật tư", "print", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_DanhSach_contextMenuDTKH()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Gửi phiếu vật tư", "sentRequestDtkh", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Xóa", "deleteDtkh", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
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


        private void _sendRequest(DataRowView drv)
        {
            // check quyen xoa du lieu
            if (modeView != "2")
                if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                {
                    MessageBox.Show("Bạn không có quyền gửi yêu cầu phiếu này!");
                    return;
                }
            int _trangthai = 0;
            if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
            if (_trangthai == 1)
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.C.DUYETPHIEU", "2$" + drv["MAUBENHPHAMID"].ToString());
                if (Func.Parse(_return) > 0)
                {
                    MessageBox.Show("Phiếu đã được gửi yêu cầu thành công!");
                    reload();
                }
                else
                    MessageBox.Show("Phiếu gửi yêu cầu thất bại!");
            }
            else
            {
                MessageBox.Show("Phiếu đã được gửi yêu cầu!");
            }
        }
        private void _deleteRequest(DataRowView drv)
        {
            if (modeView != "2")
                if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                {
                    MessageBox.Show("Bạn không có quyền xóa yêu cầu phiếu này!");
                    return;
                }
            int _trangthai = 0;
            if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
            if (_trangthai == 2)
            {
                if (drv["DACODICHVUTHUTIEN"].ToString() == "1")
                {
                    MessageBox.Show("Phiếu đã có dịch vụ thu tiền nên không được phép hủy yêu cầu!");
                    return;
                }
            }
            else if (_trangthai == 1)
            {
                MessageBox.Show("Phiếu đã được hủy yêu cầu!");
                return;
            }
            else if (_trangthai > 2 && (drv["LOAIKHO"].ToString() != "9" && drv["LOAIKHO"].ToString() != "13"))
            {
                MessageBox.Show("Phiếu đã được xử lý nên không thể hủy yêu cầu");
                return;
            }

            string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.G.DUYETPHIEU", "1$" + drv["MAUBENHPHAMID"].ToString());
            if (Func.Parse(_return) > 0)
            {
                MessageBox.Show("Phiếu đã được hủy yêu cầu thành công!");
                reload();
            }
            else
            {
                MessageBox.Show("Phiếu hủy yêu cầu thất bại!");
            }
        }
        private void _deletePhieuThuocVatTu(DataRowView drv)
        {
            if (modeView != "2")
                if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                {
                    MessageBox.Show("Bạn không có quyền xóa phiếu này!");
                    return;
                }
            int _trangthai = 0;
            if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
            if (_trangthai <= 1)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa phiếu vật tư không?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL.034", drv["MAUBENHPHAMID"].ToString());
                    if (Func.Parse(_return) >= 1)
                    {
                        MessageBox.Show("Xóa thành công phiếu vật tư");
                        reload();
                    }
                    else if (Func.Parse(_return) == -1)
                    {
                        MessageBox.Show("Xóa không thành công phiếu vật tư");
                    }
                }
            }
            else
            {
                MessageBox.Show("Phiếu đã được xử lý nên không thể hủy yêu cầu");
            }
        }





    }
}
