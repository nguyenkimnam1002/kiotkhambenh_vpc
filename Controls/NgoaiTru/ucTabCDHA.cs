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

namespace VNPT.HIS.Controls.NgoaiTru
{
    //
    // code trên web: noitru/NTU02D025_ThongTinCDHA
    //
    public partial class ucTabCDHA : DevExpress.XtraEditors.XtraUserControl
    {
        private string KHAMBENHID = "";
        private string HOSOBENHANID = "";
        private string BENHNHANID = "";

        private string lnmbp = "2";
        private string modeView = "0";
        private string hosobenhanid = "";
        private string deleteDV = "0";
        private bool checkLoad = false;
        private string studyInstanceUID = "";

        // biến dùng cho Nội trú
        string KHOAID = "";
        public ucTabCDHA()
        {
            InitializeComponent();
        }
        protected EventHandler event_BackParentForm;
        public void setEvent_BackParentForm(EventHandler event_BackParentForm)
        {
            this.event_BackParentForm = event_BackParentForm;
        }
        private void ucTabCDHA_Load(object sender, EventArgs e)
        {
            ucGrid_DsCDHA.setEvent_FocusedRowChanged(DsCDHA_SelectRow);
            ucGrid_DsCDHA.setEvent(getData_table);

            ucGrid_KqCDHA.setEvent_FocusedRowChanged(KqCDHA_SelectRow);
            ucGrid_KqCDHA.setEvent(getData_table_KqCDHA);

            ucGrid_DsCDHA.gridView.OptionsView.ShowAutoFilterRow = false;// ô search
            ucGrid_KqCDHA.gridView.OptionsView.ShowAutoFilterRow = false;// ô search


            ucGrid_DsCDHA.setEvent_MenuPopupClick(MenuPopupClick_DsCDHA);
            ucGrid_KqCDHA.setEvent_MenuPopupClick(MenuPopupClick_KqCDHA);
        }
        public bool allow_tab_reload = false;
        public void loadData_2(string _KHAMBENHID, string _BENHNHANID
            , string _lnmbp, string _modeView, string _hosobenhanid, string _deleteDV, string _checkLoad, string _studyInstanceUID)
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
                if (_studyInstanceUID != "") studyInstanceUID = _studyInstanceUID;

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
                ucGrid_DsCDHA.addMenuPopup(Menu_Popup_DanhSach_contextMenu());
            }
            else if (modeView == "1")
            {
                ucGrid_DsCDHA.addMenuPopup(Menu_Popup_DanhSach_contextMenuPrint());
            }
            else if (modeView == "2")
            {
                ucGrid_DsCDHA.addMenuPopup(Menu_Popup_DanhSach_contextMenuDtkh());
            }

            if (modeView == "1")
            {
                if (deleteDV == "1")
                    ucGrid_KqCDHA.addMenuPopup(Menu_Popup_KqCDHA_contextMenuDeleteDV());
            }
            else
            {
                ucGrid_KqCDHA.addMenuPopup(Menu_Popup_KqCDHA_contextMenuDVKTTDT());
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

                ResponsList ds = ServiceTabDanhSachBenhNhan.getDsXetNghiem(page, ucGrid_DsCDHA.ucPage1.getNumberPerPage(), KHAMBENHID, BENHNHANID, lnmbp, hosobenhanid);
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
                        , "NGAYMAUBENHPHAM", "PHONGCHIDINH", "PHONGLAYMAU", "PHONGDIEUTRI", "KHANCAP", "TRANGTHAI_PHIEU" });

                {
                    ucGrid_DsCDHA.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsCDHA.setColumnAll(false);

                    ucGrid_DsCDHA.setColumn("RN", 0, " ");
                    ucGrid_DsCDHA.setColumn("TRANGTHAIMAUBENHPHAM", 1, " ");
                    ucGrid_DsCDHA.setColumn("BARCODE", 2, "Barcode");
                    ucGrid_DsCDHA.setColumn("SOPHIEU", 3, "Số phiếu");
                    ucGrid_DsCDHA.setColumn("NGUOITAO", 4, "Bác sỹ chỉ định");
                    ucGrid_DsCDHA.setColumn("NGUOITRAKETQUA", 5, "Bác sỹ thực hiện");
                    ucGrid_DsCDHA.setColumn("NGAYMAUBENHPHAM", 6, "Thời gian chỉ định");
                    ucGrid_DsCDHA.setColumn("PHONGCHIDINH", 7, "P. Thực hiện");
                    ucGrid_DsCDHA.setColumn("PHONGLAYMAU", 8, "P. Lấy mẫu");
                    ucGrid_DsCDHA.setColumn("PHONGDIEUTRI", 9, "Phòng");
                    ucGrid_DsCDHA.setColumn("KHANCAP", 10, "Khẩn");
                    ucGrid_DsCDHA.setColumn("TRANGTHAI_PHIEU", 11, "Trạng thái");

                    //ucGrid_DSXetNghiem.setColumn("", , "");
                    ucGrid_DsCDHA.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "3", "4" }
                        , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });

                    ucGrid_DsCDHA.gridView.BestFitColumns(true);
                }
            }

            DsCDHA_SelectRow(ucGrid_DsCDHA.gridView.GetFocusedRow(), null);
        }
        private void getData_table_KqCDHA(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ucGrid_KqCDHA.clearData();

                ResponsList ds_KQ = ServiceTabDanhSachBenhNhan.getKQXetNghiem(page, ucGrid_KqCDHA.ucPage1.getNumberPerPage(), MAUBENHPHAMID);
                DataTable dt_KQ = MyJsonConvert.toDataTable(ds_KQ.rows);

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "TENDICHVU", "GIATRI_KETQUA", "GHICHU1", "GHICHU2", "NGUOITRAKETQUA" });

                {
                    ucGrid_KqCDHA.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGrid_KqCDHA.setColumnAll(false);

                    ucGrid_KqCDHA.setColumn("TENDICHVU", 0, "Tên dịch vụ");
                    ucGrid_KqCDHA.setColumn("GIATRI_KETQUA", 1, "Kết luận");
                    ucGrid_KqCDHA.setColumn("GHICHU1", 2, "Ghi chú CĐ");
                    ucGrid_KqCDHA.setColumn("GHICHU2", 3, "Ghi chú KQ");
                    ucGrid_KqCDHA.setColumn("NGUOITRAKETQUA", 4, "Bác sỹ thực hiện");

                    ucGrid_KqCDHA.gridView.BestFitColumns(true);
                }
            }
        }

        string MAUBENHPHAMID = "";
        public void DsCDHA_SelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
                MAUBENHPHAMID = selected["MAUBENHPHAMID"].ToString();
                getData_table_KqCDHA(1, null);
            }
            else
            {
                ucGrid_KqCDHA.clearData_frmTiepNhan();
            }
        }
        public void KqCDHA_SelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
            }
        }

        private void clearAllGrid()
        {
            ucGrid_DsCDHA.clearData();
            ucGrid_KqCDHA.clearData();
        }
        public void clearAllGrid_frmTiepNhan()
        {
            KHAMBENHID = "";
            ucGrid_DsCDHA.clearData_frmTiepNhan();
            ucGrid_KqCDHA.clearData_frmTiepNhan();
        }

        private void MenuPopupClick_DsCDHA(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_DsCDHA.gridView.GetFocusedRow());
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
                    if (menu.hlink == "sentRequest" || menu.hlink == "sentRequestDtkh")// NTU02D025_ThongTinCDHA.js --> _sendRequest:function(rowId){
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
                    else if (menu.hlink == "deleteRequest") //NTU02D025_ThongTinCDHA.js-- > _deleteRequest:function(rowId){
                    {
                        string ret = ServiceTabDanhSachBenhNhan.Xoa_yeu_cau(modeView, drv, KHOAID);
                        if (ret.StartsWith("TRUE "))
                        {
                            // _self._deleteRequestSynDicon(rowId); // khi huy yeu cau thanh cong thi huy dong bo   gọi ws  
                            var data = new
                            {
                                requestCode = drv["SOPHIEU"].ToString(),
                                cancelReason = "HIS hủy yêu cầu",
                                cancelBy = Const.local_user.USER_ID
                            };
                            string send = RISConnector.Send_RIS_DELETE_REQUEST(drv["SOPHIEU"].ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(data));

                            MessageBox.Show(ret.Substring("TRUE ".Length));
                            reload();
                        }
                        else
                            MessageBox.Show(ret, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (menu.hlink == "delete" || menu.hlink == "deleteDtkh")// NTU02D025_ThongTinCDHA.js --> _xoaPhieuDichVu:function(rowId){
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
                            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa phiếu CĐHA không?", "", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL.PDV.024", drv["MAUBENHPHAMID"].ToString());
                                if (_return == "1")
                                {
                                    MessageBox.Show("Xóa thành công phiếu CĐHA");
                                    reload();
                                }
                                else if (_return == "0")
                                    MessageBox.Show("Xóa không thành công phiếu CĐHA");
                                else if (_return == "-1")
                                    MessageBox.Show("Phiếu CĐHA đã thu tiền nên không được phép xóa");
                            }
                        }
                        else if (_trangthai >= 2 && _trangthai != 8)
                        {
                            MessageBox.Show("Phiếu không ở trạng thái sửa phiếu hoặc hủy nên không thể xóa!");
                        }
                    }
                    else if (menu.hlink == "deleteAll")// NTU02D025_ThongTinCDHA.js --> _xoaPhieuDichVuKhongCoKetQua:function(rowId){
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
                    else if (menu.hlink == "editOrg")// NTU02D025_ThongTinCDHA.js --> _editOrgDone: function(rowId){
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
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), lnmbp, "7");
                        frm.setReturnData(Return_NTU02D038_SuaPhongThucHien);
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "updateCDHA")// NTU02D025_ThongTinCDHA.js --> _updatePhieuCDHA: function(rowId){
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
                        data.key = "capnhatCDHA";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "copyNote")// NTU02D025_ThongTinCDHA.js --> _copyPhieuCDHA: function(rowId){
                    {
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền tạo bản sao cho phiếu này!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        NTU02D070_ThoiGianDonThuoc frm = new NTU02D070_ThoiGianDonThuoc();
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), "2");
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "printView")// NTU02D025_ThongTinCDHA.js -->  _exportPCDHA: function(rowId){ 
                    {
                        var hopital_id = Const.local_user.HOSPITAL_ID;
                        var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                        var doiTuongBenhNhan = drv["DOITUONGBENHNHANID"].ToString();

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("maubenhphamid", "String", mauBenhPhamId);
                        if ("965".Equals(hopital_id))
                        {
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
                                            PrintPreview("", "PHIEU_CDHA_A4_965", table);

                                        }
                                        else
                                        {
                                            PrintPreview("", "PHIEU_CDHADICHVU_A4_965", table);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                PrintPreview("", "PHIEU_CDHADICHVU_A4_965", table);
                            }
                        }
                        else
                        {
                            PrintPreview("", "PHIEU_CDHA", table);
                        }
                    }
                    else if (menu.hlink == "printKQView")// NTU02D025_ThongTinCDHA.js -->  _exportKetQuaPCDHA: function(rowId){
                    {
                        var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
                        var trangThaiMauBenhPhamStr = drv["TRANGTHAIMAUBENHPHAM"].ToString();

                        if ("1".Equals(trangThaiMauBenhPhamStr) || "2".Equals(trangThaiMauBenhPhamStr))
                        {
                            MessageBox.Show("Phiếu chưa thực hiện nên chưa có kết quả để in!");
                            return;
                        }

                        DataTable table = new DataTable();
                        table.Columns.Add("name");
                        table.Columns.Add("type");
                        table.Columns.Add("value");

                        table.Rows.Add("id_maubenhpham", "String", mauBenhPhamId);

                        PrintPreview("", "CLS_KQ_CDHA", table);
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


        private void MenuPopupClick_KqCDHA(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGrid_KqCDHA.gridView.GetFocusedRow());
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
                if (drv != null)
                {
                    if (menu.hlink == "deleteDV")// NTU02D025_ThongTinCDHA.js --> _deleteDVCDHA:function(rowId){
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa dịch vụ CĐHA không?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL_DVXN_REJECT", drv["DICHVUKHAMBENHID"].ToString() + "$" + drv["MAUBENHPHAMID"].ToString());
                            if (_return == "1")
                            {
                                MessageBox.Show("Xóa thành công dịch vụ CĐHA");
                                getData_table_KqCDHA(1, null);
                            }
                            else if (_return == "0")
                            {
                                MessageBox.Show("Xóa không thành công dịch vụ CĐHA");
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
                    else if (menu.hlink == "DVKTTDT")// NTU02D025_ThongTinCDHA.js -->_updatePTTTKhongThanhToanDT:function(rowId){ 
                    {
                        DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn chuyển loại dịch vụ CDHA sang dịch vụ miễn giảm thanh toán đồng thời không?", "", MessageBoxButtons.YesNo);
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

        private List<MenuFunc> Menu_Popup_DanhSach_contextMenu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xử lý yêu cầu", "", "", ""));

            listMenu.Add(new MenuFunc("Gửi yêu cầu", "sentRequest", "0", ""));// NTU02D025_ThongTinCDHA.js --> _sendRequest:function(rowId){
            listMenu.Add(new MenuFunc("Hủy yêu cầu", "deleteRequest", "0", "")); // NTU02D025_ThongTinCDHA.js --> _deleteRequest: function(rowId){

            listMenu.Add(new MenuFunc("Cập nhật phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa", "delete", "0", ""));// NTU02D025_ThongTinCDHA.js --> _xoaPhieuDichVu:function(rowId){
            listMenu.Add(new MenuFunc("Xóa các dịch vụ không có kết quả", "deleteAll", "0", ""));// NTU02D025_ThongTinCDHA.js --> _xoaPhieuDichVuKhongCoKetQua:function(rowId){
            listMenu.Add(new MenuFunc("Sửa phòng thực hiện", "editOrg", "0", ""));// NTU02D025_ThongTinCDHA.js --> _editOrgDone: function(rowId){
            listMenu.Add(new MenuFunc("Cập nhật phiếu CĐHA", "updateCDHA", "0", ""));// NTU02D025_ThongTinCDHA.js --> _updatePhieuCDHA: function(rowId){
            listMenu.Add(new MenuFunc("Tạo bản sao", "copyNote", "0", "")); // NTU02D025_ThongTinCDHA.js --> _copyPhieuCDHA: function(rowId){

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In Phiếu CĐHA", "printView", "0", ""));// NTU02D025_ThongTinCDHA.js -->  _exportPCDHA: function(rowId){ 
            listMenu.Add(new MenuFunc("In Kết quả CĐHA", "printKQView", "0", ""));// NTU02D025_ThongTinCDHA.js -->  _exportKetQuaPCDHA: function(rowId){
            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_DanhSach_contextMenuPrint()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));
            listMenu.Add(new MenuFunc("In Phiếu CĐHA", "printView", "0", ""));// NTU02D025_ThongTinCDHA.js --> _exportPCDHA: function(rowId){ 
            listMenu.Add(new MenuFunc("In Kết quả CĐHA", "printKQView", "0", ""));// NTU02D025_ThongTinCDHA.js --> _exportKetQuaPCDHA: function(rowId){   

            return listMenu;
        }
        private List<MenuFunc> Menu_Popup_KqCDHA_contextMenuDeleteDV()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Xóa dịch vụ", "", "", ""));
            listMenu.Add(new MenuFunc("Xóa", "deleteDV", "0", ""));// NTU02D025_ThongTinCDHA.js --> _deleteDVCDHA:function(rowId){

            return listMenu;
        }

        private List<MenuFunc> Menu_Popup_KqCDHA_contextMenuDVKTTDT()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Dịch vụ", "", "", ""));

            listMenu.Add(new MenuFunc("Dịch vụ miễn giảm TT đồng thời", "DVKTTDT", "0", "")); // NTU02D025_ThongTinCDHA.js -->_updatePTTTKhongThanhToanDT:function(rowId){ 

            listMenu.Add(new MenuFunc("Thông tin sinh thiết", "", "", ""));

            listMenu.Add(new MenuFunc("Nhập sinh thiết", "NhapSinhThiet", "0", "")); // NTU02D025_ThongTinCDHA.js --> _nhapSinhThiet:function(rowId){

            return listMenu;
        }

        private List<MenuFunc> Menu_Popup_DanhSach_contextMenuDtkh()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Gửi yêu cầu", "sentRequestDtkh", "0", "")); // NTU02D025_ThongTinCDHA.js --> _sendRequest:function(rowId){

            listMenu.Add(new MenuFunc("Xóa", "deleteDtkh", "0", "")); // NTU02D025_ThongTinCDHA.js --> _xoaPhieuDichVuKhongCoKetQua:function(rowId){

            return listMenu;
        }

        private void Return_NTU02D038_SuaPhongThucHien(object sender, EventArgs e)
        {
            reload();
        }

    }
}
