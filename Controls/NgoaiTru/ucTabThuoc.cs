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
    // code trên web: NTU02D033_ThongTinThuoc
    //
    public partial class ucTabThuoc : DevExpress.XtraEditors.XtraUserControl
    {
        private string CAU_HINH = "SHOW_COPY_PHIEUTHUOC";


        private string KHAMBENHID = "";
        private string HOSOBENHANID = "";
        private string BENHNHANID = "";

        private string lnmbp = "7";
        private string modeView = "0";
        private string hosobenhanid = "";

        // biến dùng cho Nội trú
        string KHOAID = "";
        public ucTabThuoc()
        {
            InitializeComponent();
        }
        protected EventHandler event_BackParentForm;
        public void setEvent_BackParentForm(EventHandler event_BackParentForm)
        {
            this.event_BackParentForm = event_BackParentForm;
        }
        

        protected EventHandler event_listenFrm;
        public void setEvent_listenFrm(EventHandler event_listenFrm)
        {
            this.event_listenFrm = event_listenFrm;
        }
        private void ucTabThuoc_Load(object sender, EventArgs e)
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

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TRANGTHAIMAUBENHPHAM", "SOPHIEU", "PHIEU_LINH", "NGUOITAO", "PHONGDIEUTRI", "NGAYMAUBENHPHAM"
                        , "NGAYMAUBENHPHAM_SUDUNG", "TENKHO", "SOTHUTUCHIDINH", "LOAIPHIEU", "TRANGTHAI_PHIEU" }
                        );

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
                    ucGrid_DanhSach.setColumn("SOTHUTUCHIDINH", 9, "STT");
                    ucGrid_DanhSach.setColumn("LOAIPHIEU", 10, "Loại phiếu");
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

                ResponsList ds_KQ = ServiceTabDanhSachBenhNhan.getChiTietPhieuThuoc(page, ucGrid_ChiTiet.ucPage1.getNumberPerPage(), MAUBENHPHAMID);
                DataTable dt_KQ = MyJsonConvert.toDataTable(ds_KQ.rows);

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "RN", "MADICHVU", "TENDICHVU", "NDHL", "SOLUONG", "SOLUONGPHUTROI"
                    ,"SOLUONGTRA", "TIENCHITRA", "DVT", "NGAYDUNG", "DUONGDUNG", "HUONGDANSUDUNG", "LOAIDOITUONG" });

                {
                    ucGrid_ChiTiet.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGrid_ChiTiet.setColumnAll(false);

                    ucGrid_ChiTiet.setColumn("RN"," ");
                    ucGrid_ChiTiet.setColumn("MADICHVU","Mã thuốc");
                    ucGrid_ChiTiet.setColumn("TENDICHVU","Tên thuốc");
                    ucGrid_ChiTiet.setColumn("NDHL", "Nồng độ/hàm lượng");
                    ucGrid_ChiTiet.setColumn("SOLUONG", "Số lượng");
                    ucGrid_ChiTiet.setColumn("SOLUONGPHUTROI", "SL Phụ trội");
                    ucGrid_ChiTiet.setColumn("SOLUONGTRA", "SL trả");
                    ucGrid_ChiTiet.setColumn("TIENCHITRA", "Đơn giá");
                    ucGrid_ChiTiet.setColumn("DVT", "ĐVT");
                    ucGrid_ChiTiet.setColumn("NGAYDUNG", "Số ngày");
                    ucGrid_ChiTiet.setColumn("DUONGDUNG", "Đường dùng");
                    ucGrid_ChiTiet.setColumn("HUONGDANSUDUNG",  "Hướng dẫn sử dụng");
                    ucGrid_ChiTiet.setColumn("LOAIDOITUONG", "Loại TT"); 

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
        private void clearAllGrid()
        {
            ucGrid_DanhSach.clearData();
            ucGrid_ChiTiet.clearData();
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
                    // codwe web trong NTU02D033_ThongTinThuoc.js --> hàm: 
                    if (menu.hlink == "sentRequest" || menu.hlink == "sentRequestDtkh") //  _sendRequest(rowKey);
                    {
                        string ret = _sendRequest(modeView, drv, KHOAID);
                        if (ret.StartsWith("TRUE "))
                        {
                            MessageBox.Show(ret.Substring("TRUE ".Length));
                            reload();
                            if (event_listenFrm != null) event_listenFrm(null, null); // load lại số lượng các tab ở form cha
                            // EventUtil.raiseEvent("assignDrug_cancel",{ option: ''});   load lại số lượng
                        }
                        else
                            MessageBox.Show(ret, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else if (menu.hlink == "tradonthuoc") //  _phieuTraThuoc(rowKey);
                    {
                        if (drv["TRANGTHAIMAUBENHPHAM"].ToString() != "6" && drv["LOAIKHO"].ToString() != "8" && drv["LOAIKHO"].ToString() != "13")
                        {
                            MessageBox.Show("Phiếu thuốc không trả được vì trạng thái phiếu không phải là đã nhận", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                        data.key = "tradonthuoc";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "deleteRequest") //  _deleteRequest(rowKey);
                    {
                        _deleteRequest(drv);
                    }
                    else if (menu.hlink == "delete" || menu.hlink == "deleteDtkh") // _deletePhieuThuocVatTu(rowKey); 
                    {
                        _deletePhieuThuocVatTu(drv);
                    }
                    else if (menu.hlink == "updatePHIEUTHUOC") //  _updatePhieuVatTu(rowKey);
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
                        data.key = "updatePHIEUTHUOC";
                        if (drv["LOAIDONTHUOC"].ToString() == "1") data.key = "cap_nhat_don_thuoc_khong_thuoc";
                        if (event_BackParentForm != null) event_BackParentForm(data, new EventArgs());
                    }
                    else if (menu.hlink == "copyNote") //  _copyPhieuThuocVatTu(rowKey);
                    {
                        // check quyen xoa du lieu
                        if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                        {
                            MessageBox.Show("Bạn không có quyền tạo bản sao cho phiếu này!");
                            return;
                        }

                        NTU02D070_ThoiGianDonThuoc frm = new NTU02D070_ThoiGianDonThuoc();
                     //   string thoigianvaovien = ""; // từ ngoại trú 
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), "7");
                        openForm(frm, "1");
                    }
                    else if (menu.hlink == "chidinhLPDK") //  _createNoteAttach(rowKey);
                    {
                        NTU02D003_DichVuDinhKem frm = new NTU02D003_DichVuDinhKem();
                        frm.setData(drv["TIEPNHANID"].ToString(), drv["MAUBENHPHAMID"].ToString());
                        openForm(frm, "1");
                    }
                    //else if (menu.hlink == "editOrg") //  _editOrgDone(rowKey);
                    //{
                    //    // check quyen xoa du lieu
                    //    if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
                    //    {
                    //        MessageBox.Show("Bạn không có quyền sửa phòng chỉ định phiếu này!");
                    //        return;
                    //    }
                    //    int _trangthai = 0;
                    //    if (drv.DataView.Table.Columns.Contains("TRANGTHAIMAUBENHPHAM")) _trangthai = Func.Parse(drv["TRANGTHAIMAUBENHPHAM"].ToString());
                    //    if (_trangthai == 3)
                    //    {
                    //        MessageBox.Show("Phiếu đã hoàn thành nên bạn không thể sửa phòng chỉ định");
                    //        return;
                    //    }
                    //    NTU02D039_SuaPhongChiDinh frm = new NTU02D039_SuaPhongChiDinh();
                    //    frm.setData(drv["MAUBENHPHAMID"].ToString(), drv["PHONGID"].ToString(), "3", lnmbp);
                    //    openForm(frm, "1");

                    //}
                    else if (menu.hlink == "printPT") //  _tuDongInDonThuoc(rowKey);
                    {
                        InDonThuoc(drv);
                    }
                    else if (menu.hlink == "printPhieu") // _tuDongInDonThuoc(rowKey); 
                    {
                        InDonThuoc(drv);
                    }  
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void InDonThuoc(DataRowView drv)
        {
            var _type = "pdf";
            var pars = new List<object>() { "HIS_FILEEXPORT_TYPE" };
            var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", string.Join("$", pars));
            if (!"-1".Equals(data_ar))
            {
                _type = data_ar;
            }

            var hopital_id = Const.local_user.HOSPITAL_ID;
            var doiTuongBenhNhan = drv["DOITUONGBENHNHANID"].ToString();
            var phongId = drv["PHONGID"].ToString();
            var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
            var soPhieu = drv["SOPHIEU"].ToString();
            var phieuHen = drv["PHIEUHEN"].ToString();
            var khamBenhId = drv["KHAMBENHID"].ToString();
            var loaiDonThuoc = drv["LOAIDONTHUOC"].ToString();
            var userId = Const.local_user;

            pars = new List<object>() { "PHONG_TUDONG_IN" };
            var dc_phong = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", string.Join("$", pars));
            List<string> dsPhong = string.IsNullOrEmpty(dc_phong) ? new List<string>() : dc_phong.Split(new string[] { "," }, StringSplitOptions.None).ToList();
            if (!dsPhong.Any(o => o.Equals(phongId)))
            {
                MessageBox.Show("Phòng khám chưa được cấu hình in tự động");
                return;
            }

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");

            table.Rows.Add("maubenhphamid", "String", mauBenhPhamId);

            var rpName = "VNPTHIS_IN_A5_";
            rpName += soPhieu;
            rpName += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
            rpName += "." + _type;

            if ("913".Equals(hopital_id))
            {
                InPhieu("", "NGT006_DONTHUOC_17DBV01_TT052016_A5_913", table, _type);
            }
            else
            {
                var array = new List<object>()
                                {
                                    mauBenhPhamId
                                };

                var arr_loaiThuoc = RequestHTTP.call_ajaxCALL_SP_O("NTU02D033_LOAITHUOC", string.Join("$", array), 0);
                string _loaiThuoc = "";
                #region arr_loaiThuoc
                if (arr_loaiThuoc != null && arr_loaiThuoc.Rows.Count > 0)
                {
                    for (var i = 0; i < arr_loaiThuoc.Rows.Count; i++)
                    {
                        _loaiThuoc = arr_loaiThuoc.Rows[i]["LOAI"].ToString();
                        if ("3".Equals(_loaiThuoc))
                        {
                            InPhieu("", "NGT020_DONTHUOCTHANGNGOAITRU", table, _type);
                        }
                        else if ("6".Equals(_loaiThuoc))
                        {
                            InPhieu("", "NGT013_DONTHUOCHUONGTHAN_TT052016_A5", table, _type);
                        }
                        else if ("7".Equals(_loaiThuoc))
                        {
                            InPhieu("", "NGT013_DONTHUOCGAYNGHIEN_TT052016_A5", table, _type);
                        }
                        else
                        {
                            if ("944".Equals(hopital_id))
                            {
                                if (!"1".Equals(doiTuongBenhNhan))
                                {
                                    InPhieu("", "NGT006_DONTHUOC1L_17DBV01_TT052016_A5_944", table, _type);
                                }
                                else
                                {
                                    InPhieu("", "NGT006_DONTHUOC1L_17DBV01_TT052016_A5_944", table, _type);
                                }
                            }
                            else
                            {
                                InPhieu("", "NGT006_DONTHUOC_17DBV01_TT052016_A5", table, _type);
                            }
                        }
                    }

                    if ("1".Equals(phieuHen))
                    {
                        if ("902".Equals(hopital_id))
                        {
                            DataTable table2 = new DataTable();
                            table2.Columns.Add("name");
                            table2.Columns.Add("type");
                            table2.Columns.Add("value");

                            table2.Rows.Add("khambenhid", "String", khamBenhId);
                            table2.Rows.Add("maubenhphamid", "String", mauBenhPhamId);

                            var rpName2 = "VNPTHIS_IN_A4_";
                            rpName2 += soPhieu;
                            rpName2 += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                            rpName2 += "." + _type;

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type);
                        }
                        else
                        {
                            DataTable table2 = new DataTable();
                            table2.Columns.Add("name");
                            table2.Columns.Add("type");
                            table2.Columns.Add("value");

                            table2.Rows.Add("khambenhid", "String", khamBenhId);

                            var rpName2 = "VNPTHIS_IN_A4_";
                            rpName2 += soPhieu;
                            rpName2 += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                            rpName2 += "." + _type;

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type);
                        }
                    }
                }
                #endregion
                #region loaidonthuoc
                if ("1".Equals(loaiDonThuoc))
                {
                    rpName = "VNPTHIS_IN_A5_";
                    rpName += soPhieu;
                    rpName += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                    rpName += "." + _type;
                    InPhieu("", "NGT006_DONTHUOCK_17DBV01_TT052016_A5", table, _type);
                    if ("1".Equals(phieuHen))
                    {
                        if ("902".Equals(hopital_id))
                        {
                            DataTable table2 = new DataTable();
                            table2.Columns.Add("name");
                            table2.Columns.Add("type");
                            table2.Columns.Add("value");

                            table2.Rows.Add("khambenhid", "String", khamBenhId);
                            table2.Rows.Add("maubenhphamid", "String", mauBenhPhamId);

                            var rpName2 = "VNPTHIS_IN_A4_";
                            rpName2 += soPhieu;
                            rpName2 += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                            rpName2 += "." + _type;

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type);
                        }
                        else
                        {
                            DataTable table2 = new DataTable();
                            table2.Columns.Add("name");
                            table2.Columns.Add("type");
                            table2.Columns.Add("value");

                            table2.Rows.Add("khambenhid", "String", khamBenhId);

                            var rpName2 = "VNPTHIS_IN_A4_";
                            rpName2 += soPhieu;
                            rpName2 += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
                            rpName2 += "." + _type;

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type);
                        }
                    }
                }
                #endregion
            }
        }

        private void InPhieu(string title, string code, DataTable parTable, string type, int height = 650, int width = 900)
        {
            string url = Func.getUrlReport(code, parTable, type);
            Func.SaveFileFromUrl(url, code);

            PrintPreview(title, code, parTable);
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

            listMenu.Add(new MenuFunc("Gửi đơn thuốc", "sentRequest", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Trả thuốc", "tradonthuoc", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Hủy đơn thuốc", "deleteRequest", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("Cập nhật phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("Xóa", "delete", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Cập nhật phiếu thuốc", "updatePHIEUTHUOC", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Tạo bản sao phiếu thuốc", "copyNote", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("Chỉ định là phiếu đi kèm", "chidinhLPDK", "0", "barButtonItem3.Glyph.png"));
            //listMenu.Add(new MenuFunc("Sửa phòng chỉ định", "editOrg", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("In đơn thuốc", "printPT", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private List<MenuFunc> Menu_Popup_DanhSach_contextMenuPrint()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            //listMenu.Add(new MenuFunc("In phiếu", ""));

            listMenu.Add(new MenuFunc("In đơn thuốc", "printPhieu", "0", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private List<MenuFunc> Menu_Popup_DanhSach_contextMenuDTKH()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            listMenu.Add(new MenuFunc("Gửi đơn thuốc", "sentRequestDtkh", "0", "barButtonItem3.Glyph.png"));
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

        private string _sendRequest(string modeView, DataRowView drv, string KHOAID)
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
                string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.C.DUYETPHIEU", "2$" + drv["MAUBENHPHAMID"].ToString());
                if (Func.Parse(_return) > 0)
                {
                    ret = "TRUE Phiếu đã được gửi yêu cầu thành công!";
                }
                else
                    ret = "Phiếu gửi yêu cầu thất bại!";
            }
            else
            {
                ret = "Phiếu đã được gửi yêu cầu!";
            }
            return ret;
        }
        private void _deleteRequest(DataRowView drv)
        {
            // check quyen xoa du lieu
            if (RequestHTTP._checkRoles(drv["NGUOITAO_ID"].ToString(), Const.local_user.USER_ID) == false)
            {
                MessageBox.Show("Bạn không có quyền xóa yêu cầu phiếu này!");
                return;
            }
            if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() == "2" &&
                (drv["LOAIKHO"].ToString() == "8" || drv["LOAIKHO"].ToString() == "9" || drv["LOAIKHO"].ToString() == "13"))
            {
                MessageBox.Show("Không hủy phiếu trả từ tủ trực!");
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
            else if (_trangthai > 2 && (drv["LOAIKHO"].ToString() != "8" && drv["LOAIKHO"].ToString() != "13"))
            {
                MessageBox.Show("Phiếu đã được xử lý nên không thể hủy yêu cầu");
                return;
            }

            //go duyet phieu, chuyen trang thai chua duyet 
            ResponsObj _return = RequestHTTP.call_ajaxCALL_SP_S("NT.G.DUYETPHIEU", "1$" + drv["MAUBENHPHAMID"].ToString());
            if (_return !=null)
            {
                if (Func.Parse(_return.result) > 0)
                {
                    MessageBox.Show("Phiếu đã được hủy yêu cầu thành công!");
                    reload();
                }
                else MessageBox.Show(_return.error_msg);
            }
            else
            {
                MessageBox.Show("Hủy yêu cầu thất bại!");
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
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa phiếu thuốc không?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string _return = RequestHTTP.call_ajaxCALL_SP_I("NT.DEL.034", drv["MAUBENHPHAMID"].ToString());
                    if (Func.Parse(_return) >= 1)
                    {
                        MessageBox.Show("Xóa thành công phiếu thuốc");
                        reload();
                    }
                    else if (Func.Parse(_return) == -1)
                    {
                        MessageBox.Show("Xóa không thành công phiếu thuốc");
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
