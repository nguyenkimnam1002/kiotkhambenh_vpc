using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.Controls.Class;
using VNPT.HIS.Controls.SubForm;

namespace VNPT.HIS.Controls.NgoaiTru
{
    //
    // code trên web: noitru/NTU02D024_ThongTinXetNghiem
    //
    public partial class ucTabXetNghiem : DevExpress.XtraEditors.XtraUserControl
    {
        private string KHAMBENHID = "";
        private string HOSOBENHANID = "";
        private string BENHNHANID = "";

        private string lnmbp = "";
        private string modeView = "0";
        private string hosobenhanid = "";
        private string deleteDV = "0";
        private bool checkLoad = false;

        // biến dùng cho Nội trú
        string KHOAID = "";
        public ucTabXetNghiem()
        {
            InitializeComponent();
        }

        protected EventHandler event_BackParentForm;
        public void setEvent_BackParentForm(EventHandler event_BackParentForm)
        {
            this.event_BackParentForm = event_BackParentForm;
        }

        private void ucTabXetNghiem_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < tabPane1.Pages.Count; i++)
                tabPane1.ButtonsPanel.Buttons[i].Properties.Appearance.Font = Const.fontDefault;

            ucGrid_DSXetNghiem.setEvent_FocusedRowChanged(DSXetNghiem_SelectRow);
            ucGrid_DSXetNghiem.SetReLoadWhenFilter(true);

            ucGrid_DSXetNghiem.onIndicator();
            ucGrid_KQXetNghiem.onIndicator();
            ucGrid_DichVuChiDinh.onIndicator();

            ucGrid_DSXetNghiem.setNumberPerPage(new int[] { 50, 75, 100 });
            ucGrid_KQXetNghiem.setNumberPerPage(new int[] { 10, 20, 30 });
            ucGrid_DichVuChiDinh.setNumberPerPage(new int[] { 10, 20, 30 });

            ucGrid_DSXetNghiem.setEvent(getData_table);
            ucGrid_KQXetNghiem.setEvent(getData_table_KQ);
            ucGrid_DichVuChiDinh.setEvent(getData_table_ChiDinh);

            ucGrid_KQXetNghiem.gridView.OptionsView.ShowViewCaption = false;
            ucGrid_DichVuChiDinh.gridView.OptionsView.ShowViewCaption = false;
             
            ucGrid_KQXetNghiem.gridView.OptionsView.ShowAutoFilterRow = false;// ô search
            ucGrid_DichVuChiDinh.gridView.OptionsView.ShowAutoFilterRow = false;// ô search

            ucGrid_DSXetNghiem.setEvent_MenuPopupClick(MenuPopupClick_DSXetNghiem);
            ucGrid_DichVuChiDinh.setEvent_MenuPopupClick(MenuPopupClick_DichVuChiDinh);
        }
        public void reload()
        {
            getData_table(1, null);
        } 
        public bool allow_tab_reload = false;
        public void loadData_2(string _KHAMBENHID, string _BENHNHANID, string _lnmbp, string _modeView, string _hosobenhanid, string _deleteDV, string _checkLoad)
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
                if (_deleteDV != "") deleteDV = _deleteDV;
                if (_checkLoad == "1") checkLoad = true;
                else if (_checkLoad == "0") checkLoad = false;

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
            if (modeView == "0") // theo trạng thái của bệnh nhân
            {
                ucGrid_DSXetNghiem.addMenuPopup(Menu_Popup_DSXetNghiem_contextMenu());
                ucGrid_DichVuChiDinh.addMenuPopup(Menu_Popup_DichVuChiDinh_contextMenuDeleteDVXNReject());
            }
            else if (modeView == "1")
            {
                ucGrid_DSXetNghiem.addMenuPopup(Menu_Popup_DSXetNghiem_contextMenuPrint());
                if (deleteDV == "1")
                    ucGrid_DichVuChiDinh.addMenuPopup(Menu_Popup_DichVuChiDinh_contextMenuDeleteDV());
            }
            else if (modeView == "2")
            {
                ucGrid_DSXetNghiem.addMenuPopup(Menu_Popup_DSXetNghiem_contextMenuDtkh());
            }
        }
        private void getData_table(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                clearAllGrid();

                ResponsList ds = ServiceTabDanhSachBenhNhan.getDsXetNghiem(page, ucGrid_DSXetNghiem.ucPage1.getNumberPerPage()
                    , KHAMBENHID, BENHNHANID, lnmbp, hosobenhanid);
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
                    ucGrid_DSXetNghiem.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DSXetNghiem.setColumnAll(false);

                    ucGrid_DSXetNghiem.setColumn("RN", " ");
                    ucGrid_DSXetNghiem.setColumn("TRANGTHAIMAUBENHPHAM", " ");
                    ucGrid_DSXetNghiem.setColumn("BARCODE", "Barcode");
                    ucGrid_DSXetNghiem.setColumn("SOPHIEU", "Số phiếu");
                    ucGrid_DSXetNghiem.setColumn("NGUOITAO", "Bác sỹ chỉ định");
                   // ucGrid_DSXetNghiem.setColumn("NGUOITRAKETQUA", "Bác sỹ thực hiện");
                    ucGrid_DSXetNghiem.setColumn("NGAYMAUBENHPHAM", "Thời gian chỉ đinh");
                    ucGrid_DSXetNghiem.setColumn("PHONGCHIDINH", "P. Thực hiện");
                    ucGrid_DSXetNghiem.setColumn("PHONGLAYMAU", "P. Lấy mẫu");
                    ucGrid_DSXetNghiem.setColumn("PHONGDIEUTRI", "Phòng");
                    ucGrid_DSXetNghiem.setColumn("KHANCAP", "Khẩn");
                    ucGrid_DSXetNghiem.setColumn("TRANGTHAI_PHIEU", "Trạng thái");

                    //ucGrid_DSXetNghiem.setColumn("", , "");
                    ucGrid_DSXetNghiem.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "3", "4" }
                        , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });


                    ucGrid_DSXetNghiem.gridView.BestFitColumns(true);
                }
            }

            DSXetNghiem_SelectRow(ucGrid_DSXetNghiem.gridView.GetFocusedRow(), null);
        }
        private void getData_table_KQ(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ucGrid_KQXetNghiem.clearData();

                ResponsList ds_KQ = ServiceTabDanhSachBenhNhan.getKQXetNghiem(page, ucGrid_KQXetNghiem.ucPage1.getNumberPerPage(), MAUBENHPHAMID);
                DataTable dt_KQ = MyJsonConvert.toDataTable(ds_KQ.rows);

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "MADICHVU", "TENDICHVU", "GIATRI_KETQUA", "TRISOBINHTHUONG", "TRANGTHAIKETQUA", "THOIGIANTRAKETQUA", "GHICHU", "NGUOITRAKETQUA" });
                 
                ucGrid_KQXetNghiem.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGrid_KQXetNghiem.setColumnAll(false);

                    ucGrid_KQXetNghiem.setColumn("MADICHVU", 0, "Mã xét nghiệm");
                    ucGrid_KQXetNghiem.setColumn("TENDICHVU", 1, "Tên xét nghiệm");
                    ucGrid_KQXetNghiem.setColumn("GIATRI_KETQUA", 2, "Kết quả");
                    ucGrid_KQXetNghiem.setColumn("TRISOBINHTHUONG", 3, "Trị số bình thường");
                    ucGrid_KQXetNghiem.setColumn("TRANGTHAIKETQUA", 4, "Trạng thái");
                    ucGrid_KQXetNghiem.setColumn("THOIGIANTRAKETQUA", 5, "Thực hiện");
                    ucGrid_KQXetNghiem.setColumn("GHICHU", 6, "Ghi chú CĐ");
                    ucGrid_KQXetNghiem.setColumn("NGUOITRAKETQUA", 7, "Bác sỹ thực hiện");

                    ucGrid_KQXetNghiem.gridView.BestFitColumns(true);
            
            }
        }
        private void getData_table_ChiDinh(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ucGrid_DichVuChiDinh.clearData();

                ResponsList ds = ServiceTabDanhSachBenhNhan.getDVChiDinh(page, ucGrid_DichVuChiDinh.getNumberPerPage(), MAUBENHPHAMID);
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);
                if (dt.Rows.Count > 0)
                {
                    ucGrid_DichVuChiDinh.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DichVuChiDinh.setColumnAll(false);

                    ucGrid_DichVuChiDinh.setColumn("MADICHVU", 0, "Mã xét nghiệm");
                    ucGrid_DichVuChiDinh.setColumn("TENDICHVU", 1, "Tên xét nghiệm");
                    ucGrid_DichVuChiDinh.setColumn("TENTRANGTHAI", 2, "Trạng thái");

                    ucGrid_DichVuChiDinh.gridView.BestFitColumns(true);
                }
            }
        }

        string MAUBENHPHAMID = ""; 
        public void DSXetNghiem_SelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
                MAUBENHPHAMID = selected["MAUBENHPHAMID"].ToString(); 
                getData_table_ChiDinh(1, null);
                getData_table_KQ(1, null);
            }
            else
            {
                ucGrid_DichVuChiDinh.clearData_frmTiepNhan();
                ucGrid_KQXetNghiem.clearData_frmTiepNhan();
            }
        }

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            ucGrid_KQXetNghiem.gridView.OptionsView.ShowViewCaption = false;
            ucGrid_KQXetNghiem.gridView.OptionsView.ShowAutoFilterRow = false;// ô search
        }

        private void clearAllGrid()
        {
            ucGrid_DSXetNghiem.clearData();
            ucGrid_KQXetNghiem.clearData();
            ucGrid_DichVuChiDinh.clearData();
        }
        private void MenuPopupClick_DSXetNghiem(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DSXetNghiem.gridView.GetFocusedRow());
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
                    if (menu.hlink == "sentRequest" || menu.hlink == "sentRequestDtkh")// NTU02D024_ThongTinXetNghiem.js --> _sendRequest:function(rowId){
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
                    else if (menu.hlink == "deleteRequest")// NTU02D024_ThongTinXetNghiem.js --> _deleteRequest:function(rowId){
                    {
                        string ret = ServiceTabDanhSachBenhNhan.Xoa_yeu_cau(modeView, drv, KHOAID);
                        if (ret.StartsWith("TRUE "))
                        {
                            // _self.deleteRequestOnLab(rowId); ???

                            string soPhieu = drv["SOPHIEU"].ToString();
                            string mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                            LISConnector.deleteRequestOnLab(soPhieu, mauBenhPhamId);

                            MessageBox.Show(ret.Substring("TRUE ".Length));
                            reload();
                        }
                        else
                            MessageBox.Show(ret, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (menu.hlink == "delete")// NTU02D024_ThongTinXetNghiem.js --> _xoaPhieuDichVu:function(rowId){
                    {
                        // check quyen xoa du lieu
                        if (modeView != "2")
                            if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                            {
                                MessageBox.Show("Bạn không có quyền xóa phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }

                        int _trangthai = 0;
                        if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                        if (_trangthai == 1 || _trangthai == 8)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa phiếu xét nghiệm không?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL.PDV.024", drv["MAUBENHPHAMID"].ToString());
                                if (_return == "1")
                                {
                                    MessageBox.Show("Xóa thành công phiếu xét nghiệm");
                                    reload();
                                }
                                else if (_return == "0")
                                    MessageBox.Show("Xóa không thành công phiếu phiếu xét nghiệm");
                                else if (_return == "-1")
                                    MessageBox.Show("Phiếu xét nghiệm đã thu tiền nên không được phép xóa");
                            }
                        }
                        else if (_trangthai >= 2 && _trangthai != 8)
                        {
                            MessageBox.Show("Phiếu đã được xử lý nên không thể xóa!");
                        }
                    }
                    else if (menu.hlink == "deleteAll" || menu.hlink == "deleteDtkh")// NTU02D024_ThongTinXetNghiem.js --> _xoaPhieuDichVuKhongCoKetQua:function(rowId){
                    {
                        int _trangthai = 0;
                        if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                        if (_trangthai == 3)
                        {
                            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa các dịch vụ không có kết quả không?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL.PDV.NO_RLT", drv["MAUBENHPHAMID"].ToString());
                                if (_return == "1")
                                {
                                    MessageBox.Show("Xóa thành công các dịch vụ không có kết quả");
                                    reload();
                                }
                                else if (_return == "0")
                                    MessageBox.Show("Xóa không thành công phiếu xét nghiệm");
                                else if (_return == "2")
                                    MessageBox.Show("Phiếu không có dịch vụ không có kết quả");
                            }
                        }
                        else if (_trangthai < 3)
                        {
                            MessageBox.Show("Phiếu này chưa kết thúc!");
                        }
                    }
                    else if (menu.hlink == "NTU02D038_SuaPhongThucHien")
                    {
                        // check quyen xoa du lieu
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
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), lnmbp, "6");
                        frm.setReturnData(Return_NTU02D038_SuaPhongThucHien);
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "NGT02K016_ChiDinhDichVu")
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
                        data.key = "capnhatxetnghiem";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "NTU02D070_ThoiGianDonThuoc")
                    {
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền tạo bản sao cho phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        NTU02D070_ThoiGianDonThuoc frm = new NTU02D070_ThoiGianDonThuoc();
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), "1");
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "NTU02D081_Capnhat_PhieuDT")
                    {
                        // {"func":"ajaxExecuteQueryO","params":["","NTU02D009.EV004"],"options":[{"name":"[0]","value":"310546"}],"uuid":"6b9f8ac4-1223-4f85-b72d-2311ed4f8639"}
                        DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NTU02D009.EV004", drv["MAUBENHPHAMID"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            NTU02D081_Capnhat_PhieuDT frm = new NTU02D081_Capnhat_PhieuDT();
                            frm.loadData(drv["MAUBENHPHAMID"].ToString(), drv["KHAMBENHID"].ToString(), dt.Rows[0]["PHIEUDIEUTRIID"].ToString());
                            openForm(frm, "1");
                        }

                    }
                    else if (menu.hlink == "printAuto") //"In Phiếu chỉ định" // NTU02D024_ThongTinXetNghiem.js -->  _exportPXNCDAuto: function(rowId){
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
                                            string url = Func.getUrlReport("PHIEU_XETNGHIEM_A4_965", table, _type);
                                            Func.SaveFileFromUrl(url, rpName);

                                            PrintPreview("", "PHIEU_XETNGHIEM_A4_965", table);

                                        }
                                        else
                                        {
                                            string url = Func.getUrlReport("PHIEU_XETNGHIEMDICHVU_A4_965", table, _type);
                                            Func.SaveFileFromUrl(url, rpName);

                                            PrintPreview("", "PHIEU_XETNGHIEMDICHVU_A4_965", table);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string url = Func.getUrlReport("PHIEU_XETNGHIEMDICHVU_A4_965", table, _type);
                                Func.SaveFileFromUrl(url, rpName);

                                PrintPreview("", "PHIEU_XETNGHIEMDICHVU_A4_965", table);
                            }
                        }
                        else
                        {
                            var rpName = "VNPTHIS_IN_A5_";
                            rpName += soPhieu;
                            rpName += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                            rpName += "." + _type;
                            
                            string url = Func.getUrlReport("PHIEU_XETNGHIEM", table, _type);
                            Func.SaveFileFromUrl(url, rpName);

                            PrintPreview("", "PHIEU_XETNGHIEM", table);
                        }
                    }
                    else if (menu.hlink == "printKQAuto") //"In Kết quả chỉ định"// NTU02D024_ThongTinXetNghiem.js --> _exportKetQuaPXNCDAuto: function(rowId){
                    {
                        var _type = "pdf";
                        var pars = new List<object>() { "HIS_FILEEXPORT_TYPE" };
                        var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", string.Join("$", pars));
                        if (!"-1".Equals(data_ar))
                        {
                            _type = data_ar;
                        }

                        var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                        var soPhieu = drv["SOPHIEU"].ToString();
                        var trangThaiMauBenhPhamStr = drv["TRANGTHAIMAUBENHPHAM"].ToString();

                        if ("1".Equals(trangThaiMauBenhPhamStr) || "2".Equals(trangThaiMauBenhPhamStr))
                        {
                            MessageBox.Show("Phiếu chưa thực hiện nên chưa có kết quả để in!");
                            return;
                        }

                        var rpName = "VNPTHIS_IN_A5_";
                        rpName += soPhieu;
                        rpName += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                        rpName += "." + _type;

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("id_maubenhpham", "String", mauBenhPhamId);
                        
                        string url = Func.getUrlReport("PhieuXetNghiem", table, _type);
                        Func.SaveFileFromUrl(url, rpName);

                        PrintPreview("", "PhieuXetNghiem", table);
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
        private void MenuPopupClick_DichVuChiDinh(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DichVuChiDinh.gridView.GetFocusedRow());
                // {"RN": "1",
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
                //"MAUBENHPHAMID": "12278"}
                if (drv != null)
                {
                    if (menu.hlink == "deleteDV")// NTU02D024_ThongTinXetNghiem.js --> _deleteDVXetNghiem: function(rowId){
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa dịch vụ xét nghiệm không?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL_DVXN_REJECT", drv["DICHVUKHAMBENHID"].ToString() + "$" + drv["MAUBENHPHAMID"].ToString());
                            if (_return == "1")
                            {
                                MessageBox.Show("Xóa thành công dịch vụ xét nghiệm");
                                getData_table_ChiDinh(1, null);
                            }
                            else if (_return == "0")
                            {
                                MessageBox.Show("Xóa không thành công dịch vụ xét nghiệm");
                            }
                            else if (_return == "-1")
                            {
                                MessageBox.Show("Dịch vụ đã thu tiền nên không được phép xóa");
                            }
                            else if (_return == "-2")
                            {
                                MessageBox.Show("Bệnh nhân đã ra viên nên không được phép xóa dịch vụ, Bạn phải mở lại bệnh án để sửa!");
                            }
                            else if (_return == "-3")
                            {
                                MessageBox.Show("Phiếu dịch vụ đã trả kết quả nên không được phép xóa!");
                            }
                        }
                    }
                    else if (menu.hlink == "deleteDVXNReject")// NTU02D024_ThongTinXetNghiem.js --> _deleteDVXetNghiemReject:function(rowId){
                    {
                        int _trangthaiid = -1;
                        if (drv.DataView.Table.Columns.Contains("TRANGTHAI_ID")) _trangthaiid = Func.Parse(drv["TRANGTHAI_ID"].ToString());
                        if (_trangthaiid != 9)
                        {
                            MessageBox.Show("Dịch vụ không bị từ chối nên không được phép xóa!");
                            return;
                        }

                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa dịch vụ xét nghiệm không?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL_DVXN_REJECT", drv["DICHVUKHAMBENHID"].ToString() + "$" + drv["MAUBENHPHAMID"].ToString());
                            if (_return == "1")
                            {
                                MessageBox.Show("Xóa thành công dịch vụ xét nghiệm");
                                getData_table_ChiDinh(1, null);
                            }
                            else if (_return == "0")
                            {
                                MessageBox.Show("Xóa không thành công dịch vụ xét nghiệm");
                            }
                            else if (_return == "-1")
                            {
                                MessageBox.Show("Dịch vụ đã thu tiền nên không được phép xóa");
                            }
                            else if (_return == "-2")
                            {
                                MessageBox.Show("Bệnh nhân đã ra viên nên không được phép xóa dịch vụ, Bạn phải mở lại bệnh án để sửa!");
                            }
                        }
                    }
                    else if (menu.hlink == "DVKTTDT") // NTU02D024_ThongTinXetNghiem.js --> _updateDVKhongThanhToanDT: function(rowId){
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn chuyển loại dịch vụ xét nghiệm sang dịch vụ miễn giảm thanh toán đồng thời không?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string _return = RequestHTTP.call_ajaxCALL_SP_I("NT026.EV003", drv["DICHVUKHAMBENHID"].ToString());
                            if (_return == "1")
                            {
                                MessageBox.Show("Cập nhật thành công dịch vụ miễn giảm thanh toán đồng thời"); 
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
                    else if (menu.hlink == "NhapSinhThiet")
                    {
                        NGT02K062_MauSinhThiet frm = new NGT02K062_MauSinhThiet();
                        frm.setParam(drv["DICHVUKHAMBENHID"].ToString(), drv["MAUBENHPHAMID"].ToString(), drv["TENDICHVU"].ToString());
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
        public void clearAllGrid_frmTiepNhan()
        {
            KHAMBENHID = "";
            ucGrid_DSXetNghiem.clearData_frmTiepNhan();
            ucGrid_KQXetNghiem.clearData_frmTiepNhan();
            ucGrid_DichVuChiDinh.clearData_frmTiepNhan();
        }
        private List<MenuFunc> Menu_Popup_DSXetNghiem_contextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xử lý yêu cầu", "", "", ""));

            listMenu.Add(new MenuFunc("Gửi yêu cầu", "sentRequest", "0", ""));  // NTU02D024_ThongTinXetNghiem.js --> _sendRequest:function(rowId){
            listMenu.Add(new MenuFunc("Hủy yêu cầu", "deleteRequest", "0", ""));  // NTU02D024_ThongTinXetNghiem.js --> _deleteRequest:function(rowId){

            listMenu.Add(new MenuFunc("Cập nhật phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa", "delete", "0", "")); // NTU02D024_ThongTinXetNghiem.js --> _xoaPhieuDichVu:function(rowId){
            listMenu.Add(new MenuFunc("Xóa các dịch vụ không có kết quả", "deleteAll", "0", "")); // NTU02D024_ThongTinXetNghiem.js --> _xoaPhieuDichVuKhongCoKetQua:function(rowId){
            listMenu.Add(new MenuFunc("Sửa phòng thực hiện", "NTU02D038_SuaPhongThucHien", "0", ""));//mở popup // NTU02D024_ThongTinXetNghiem.js --> _editOrgDone: function(rowId){
            listMenu.Add(new MenuFunc("Cập nhật phiếu xét nghiệm", "NGT02K016_ChiDinhDichVu", "0", ""));//mở popup // NTU02D024_ThongTinXetNghiem.js --> _updatePhieuXetNghiem: function(rowId){
            listMenu.Add(new MenuFunc("Tạo bản sao", "NTU02D070_ThoiGianDonThuoc", "0", "")); // NTU02D024_ThongTinXetNghiem.js --> _copyPhieuXetNghiem:function(rowId){
            listMenu.Add(new MenuFunc("Cập nhật phiếu điều trị", "NTU02D081_Capnhat_PhieuDT", "0", ""));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In Phiếu chỉ định", "printAuto", "0", "")); // NTU02D024_ThongTinXetNghiem.js -->  _exportPXNCDAuto: function(rowId){
            listMenu.Add(new MenuFunc("In Kết quả chỉ định", "printKQAuto", "0", "")); // NTU02D024_ThongTinXetNghiem.js --> _exportKetQuaPXNCDAuto: function(rowId){

            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_DSXetNghiem_contextMenuPrint()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In Phiếu chỉ định", "printAuto", "0", "")); // NTU02D024_ThongTinXetNghiem.js -->_exportPXNCDAuto: function(rowId){
            listMenu.Add(new MenuFunc("In Kết quả chỉ định", "printKQAuto", "0", ""));  // NTU02D024_ThongTinXetNghiem.js --> _exportKetQuaPXNCDAuto: function(rowId){

            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_DSXetNghiem_contextMenuDtkh()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Gửi yêu cầu", "sentRequestDtkh", "0", ""));  // NTU02D024_ThongTinXetNghiem.js --> _sendRequest:function(rowId){

            listMenu.Add(new MenuFunc("Xóa", "deleteDtkh", "0", ""));   // NTU02D024_ThongTinXetNghiem.js --> _xoaPhieuDichVuKhongCoKetQua: function(rowId){
            return listMenu;
        }

        private List<MenuFunc> Menu_Popup_DichVuChiDinh_contextMenuDeleteDV()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xóa dịch vụ", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa", "deleteDV", "0", "")); // NTU02D024_ThongTinXetNghiem.js --> _deleteDVXetNghiem: function(rowId){
            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_DichVuChiDinh_contextMenuDeleteDVXNReject()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xóa dịch vụ", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa", "deleteDVXNReject", "0", "")); // NTU02D024_ThongTinXetNghiem.js --> _deleteDVXetNghiemReject:function(rowId){
            listMenu.Add(new MenuFunc("Dịch vụ miễn giảm TT đồng thời", "DVKTTDT", "0", "")); // NTU02D024_ThongTinXetNghiem.js --> _updateDVKhongThanhToanDT: function(rowId){

            listMenu.Add(new MenuFunc("Thông tin sinh thiết", "", "", ""));

            listMenu.Add(new MenuFunc("Nhập sinh thiết", "NhapSinhThiet", "0", ""));


            return listMenu;
        }







    }
}
