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
    // code trên web: NTU02D029_ThongTinSuatAn
    //

    public partial class ucTabPhieuVanChuyen : DevExpress.XtraEditors.XtraUserControl
    {
        private string CAU_HINH = "";

        private string KHAMBENHID = "";
        private string BENHNHANID = "";

        private string lnmbp = "";
        private string modeView = "";
        private string hosobenhanid = "";
        private string loaidichvu = "";

        private string tInPhieuThuKhac = "In phiếu thu khác";

        public ucTabPhieuVanChuyen()
        {
            InitializeComponent();
        }
        protected EventHandler event_BackParentForm;
        public void setEvent_BackParentForm(EventHandler event_BackParentForm)
        {
            this.event_BackParentForm = event_BackParentForm;
        }
        private void ucTabPhieuVanChuyen_Load(object sender, EventArgs e)
        {
            ucGrid_DanhSach.setEvent_FocusedRowChanged(DanhSach_SelectRow);
            ucGrid_DanhSach.setEvent(getData_table);

            ucGrid_ChiTiet.setEvent_FocusedRowChanged(ChiTiet_SelectRow);
            ucGrid_ChiTiet.setEvent(getData_table_ChiTiet);

            ucGrid_DanhSach.gridView.OptionsView.ShowAutoFilterRow = false;// ô search
            ucGrid_ChiTiet.gridView.OptionsView.ShowAutoFilterRow = false;// ô search

            ucGrid_DanhSach.setEvent_MenuPopupClick(MenuPopupClick_DanhSach);
            ucGrid_ChiTiet.setEvent_MenuPopupClick(MenuPopupClick_ChiTiet);
        }

        public bool allow_tab_reload = false;
        public void loadData(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string _hosobenhanid, string _loaidichvu)
        {
            if (allow_tab_reload == false)
                if (KHAMBENHID == _KHAMBENHID) return;

            allow_tab_reload = false;

            this.KHAMBENHID = _KHAMBENHID;
            this.BENHNHANID = _BENHNHANID;
            this.lnmbp = _lnmbp;
            this.modeView = _modeView;
            this.hosobenhanid = _hosobenhanid;
            this.loaidichvu = _loaidichvu; 
             
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

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

                if ("2".Equals(loaidichvu) && "3".Equals(lnmbp))
                {
                    ucGrid_DanhSach.MenuPopup_Enable_Child_byTitle(false, tInPhieuThuKhac);
                }
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


                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "SOPHIEU", "NGUOITAO", "PHONGDIEUTRI", "NGAYMAUBENHPHAM", "LOAIDICHVU" });

                {
                    ucGrid_DanhSach.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DanhSach.setColumnAll(false);

                    ucGrid_DanhSach.setColumn("RN", 0, " ");
                    ucGrid_DanhSach.setColumn("SOPHIEU", 1, "Số phiếu");
                    ucGrid_DanhSach.setColumn("NGUOITAO", 2, "Bác sỹ chỉ định");
                    ucGrid_DanhSach.setColumn("PHONGDIEUTRI", 3, "Phòng");
                    ucGrid_DanhSach.setColumn("NGAYMAUBENHPHAM", 4, "Ngày tạo");
                    ucGrid_DanhSach.setColumn("LOAIDICHVU", 5, "Loại dịch vụ");

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

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "RN", "TENDICHVU", "TYLENGAYGIUONG", "SOLUONG",
                "DONGIA", "TIENBHYTTRA", "MIENGIAM", "NHANDANTRA"    });

                {
                    ucGrid_ChiTiet.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGrid_ChiTiet.setColumnAll(false);

                    ucGrid_ChiTiet.setColumn("RN", 0, " ");
                    ucGrid_ChiTiet.setColumn("TENDICHVU", 1, "Tên dịch vụ");
                    ucGrid_ChiTiet.setColumn("TYLENGAYGIUONG", 2, "Tỷ lệ");
                    ucGrid_ChiTiet.setColumn("SOLUONG", 3, "Số lượng");

                    ucGrid_ChiTiet.setColumn("DONGIA", 4, "Đơn giá");
                    ucGrid_ChiTiet.setColumn("TIENBHYTTRA", 5, "BHYT trả");
                    ucGrid_ChiTiet.setColumn("MIENGIAM", 6, "Miễn giảm");
                    ucGrid_ChiTiet.setColumn("NHANDANTRA", 7, "ND trả"); 

 
                    ucGrid_ChiTiet.gridView.BestFitColumns(true);
                }
            }
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
                ucGrid_ChiTiet.addMenuPopup(Menu_Popup_ChiTiet_contextMenu());
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
                    // codwe web trong NTU02D029_ThongTinSuatAn.js --> hàm: 
                    if (menu.hlink == "updatePSA") //  _updatePhieuSuatAn(rowKey);   
                    {
                        // check quyen xoa du lieu
                        string mess = RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID, 1);// _trangthai=1 để ko check 

                        if (mess != "")
                        {
                            MessageBox.Show(mess, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        //                _grdSuatAn: 'grdSuatAn',	
                        //_grdSuatAnChitiet: 'grdSuatAnChitiet',
                        //_khambenhid: $("#hidKHAMBENHID").val(),
                        //     	_benhnhanid: $("#hidBENHNHANID").val(),
                        //     	_lnmbp: '16',
                        //     	_loaidichvu: "14",
                        //     	_modeView: _flgModeView, // =1 chi view; !=1 la update
                        //     	_hosobenhanid: ""
                        if (loaidichvu == "13")
                        {
                            string result = RequestHTTP.call_ajaxCALL_SP_I("NTU02D077.EV003", drv["MAUBENHPHAMID"].ToString());
                            if (result != "0")
                            {
                                MessageBox.Show("Tồn tại dịch vụ miễn giảm trong phiếu,vui lòng xóa dịch vụ miễn giảm trước khi cập nhật phiếu", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                        }

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "capnhatPhieuVanChuyen";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "delete") //  _deletePhieuSuatAn(rowKey);  
                    {
                        // check quyen xoa du lieu
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền xóa phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa phiếu dịch vụ không?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL.PDV.024", drv["MAUBENHPHAMID"].ToString());
                            if (_return == "1")
                            {
                                MessageBox.Show("Xóa thành công phiếu dịch vụ");
                                reload();
                            }
                            else if (_return == "0")
                            {
                                MessageBox.Show("Xóa không thành công phiếu dịch vụ");
                            }
                            else if (_return == "-1")
                            {
                                MessageBox.Show("Phiếu dịch vụ đã thu tiền nên không được phép xóa");
                            }
                        }
                    }
                    else if (menu.hlink == "printPTK") //  _exportPTKHAC(rowKey);
                    {
                        var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("maubenhphamid", "String", mauBenhPhamId);

                        PrintPreview("", "DKBD_PCD_THEM_CONG_KHAM_A5", table);
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
                    // codwe web trong NTU02D029_ThongTinSuatAn.js --> hàm: 
                    if (menu.hlink == "listActionDown") //   _list_miengiam(rowKey);  
                    { 
                        if (loaidichvu != "13") return;
                        NTU02D077_DanhSachMienGiam frm = new NTU02D077_DanhSachMienGiam();
                        frm.setData(drv["DICHVUKHAMBENHID"].ToString());
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "actionDown") //  _miengiam(rowKey); 
                    {
                        if (loaidichvu != "13") return;
                       
                        if (drv["LOAIDOITUONG"].ToString() != "4" && drv["LOAIDOITUONG"].ToString() != "6")
                        {
                            MessageBox.Show("Chỉ miễn giảm cho các dịch vụ yêu cầu");
                            return;
                        }

                        DataRowView rowDataP = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();

                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = rowDataP;
                        data.key = "phieu_mien_giam";
                        data.id = Func.Parse(drv["DICHVUKHAMBENHID"].ToString());
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs()); 
                    }
                    //else if (menu.hlink == "DSPTVT_KEM") //  _dsPhieuThuocVatTuDiKem(rowKey); 
                    //{
                    //    DataRowView rowDataP = (DataRowView)ucGrid_DanhSach.gridView.GetFocusedRow();

                    //    NTU02D043_DanhSachPhieuThuocDiKem frm = new NTU02D043_DanhSachPhieuThuocDiKem();
                    //    frm.setData(rowDataP["KHAMBENHID"].ToString(), rowDataP["DICHVUKHAMBENHID"].ToString());                            
                    //}
                    else if (menu.hlink == "TAOPHIEUTHUOCKEM_HAOPHI") //   _taoPhieuThuocDiKem_haophi(rowKey);
                    {
                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "tao_phieu_thuoc_di_kem_hao_phi";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs()); 
                    }
                    else if (menu.hlink == "TAOPHIEUVATTUKEM_HAOPHI") //   _taoPhieuVatTuDiKem_haophi(rowKey);
                    {
                        ojbDatarowview data = new ojbDatarowview();
                        data.drv = drv;
                        data.key = "tao_phieu_vat_tu_di_kem_hao_phi";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }


        private List<MenuFunc> Menu_Popup_DanhSach_contextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Phiếu dịch vụ", "", "", ""));

            listMenu.Add(new MenuFunc("Cập nhật dịch vụ", "updatePSA", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Xóa phiếu", "delete", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc(tInPhieuThuKhac, "printPTK", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_ChiTiet_contextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Danh sách miễn giảm", "listActionDown", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Miễn giảm", "actionDown", "0", "barButtonItem3.Glyph.png"));
            //listMenu.Add(new MenuFunc("Danh sách phiếu thuốc đi kèm", "DSPTVT_KEM", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo phiếu thuốc đi kèm hao phí", "TAOPHIEUTHUOCKEM_HAOPHI", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo phiếu vật tư đi kèm hao phí", "TAOPHIEUVATTUKEM_HAOPHI", "0", "barButtonItem3.Glyph.png"));

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

    }
}
