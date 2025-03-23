using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;
using VNPT.HIS.Controls.Class;

namespace VNPT.HIS.Controls.SubForm
{
    public partial class NTU02D078_DanhSachPhieuThuocVatTu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NTU02D078_DanhSachPhieuThuocVatTu()
        {
            InitializeComponent();
        }

        private string LNMBP_PhieuThuoc = "7";
        private string LNMBP_PhieuVatTu = "8";
        private string ModeView = "0";
        private string KHAMBENHID;
        private string BENHNHANID;
        private string TRANGTHAIKHAMBENH;
        private string HOSOBENHNHANID;
        private string DICHVUKHAMBENHID;
        private string KHOAID;

        public void setData(string khambenhid, string benhnhanid, string trangthai, string dichvu)
        {
            this.KHAMBENHID = khambenhid;
            this.BENHNHANID = benhnhanid;
            this.TRANGTHAIKHAMBENH = trangthai;
            this.HOSOBENHNHANID = string.Empty;
            this.DICHVUKHAMBENHID = dichvu;
        }

        private void NTU02D078_DanhSachPhieuThuocVatTu_Load(object sender, EventArgs e)
        {
            #region Grid Phiếu thuốc
            //ucGridPhieuThuoc.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
            ucGridPhieuThuoc.setEvent(GetDataThuoc);
            ucGridPhieuThuoc.setEvent_FocusedRowChanged(DanhSachThuocSelectRow);
            ucGridPhieuThuoc.SetReLoadWhenFilter(true);
            ucGridPhieuThuoc.setEvent_MenuPopupClick(MenuPopupClickDanhSachThuoc);

            ucGridThuocChiTiet.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGridThuocChiTiet.setEvent(GetDataThuocChiTiet);

            LoadData(KHAMBENHID, BENHNHANID, LNMBP_PhieuThuoc, ModeView, string.Empty);
            #endregion

            #region Grid Vật tư
            //ucGridVatTu.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
            ucGridVatTu.setEvent_FocusedRowChanged(DanhSachVatTuSelectRow);
            ucGridVatTu.setEvent(GetDataVatTu);
            ucGridVatTu.SetReLoadWhenFilter(true);
            ucGridVatTu.setEvent_MenuPopupClick(MenuPopupClickDanhSachVatTu);

            ucGridVatTuChiTiet.setEvent(GetDataVatTuChiTiet);
            ucGridVatTuChiTiet.gridView.OptionsView.ShowAutoFilterRow = false;
            #endregion
        }

        #region Phiếu thuốc
        private void GetDataThuoc(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page >= 0)
            {
                ClearAllGrid(LNMBP_PhieuThuoc);

                ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NT.024.DSTHUOCVT_NT", page, ucGridPhieuThuoc.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[1]", "[2]", "[3]", "[4]" },
                    new string[] { KHAMBENHID, BENHNHANID, LNMBP_PhieuThuoc, HOSOBENHNHANID, DICHVUKHAMBENHID }, ucGridPhieuThuoc.jsonFilter());
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TRANGTHAIMAUBENHPHAM", "SOPHIEU", "PHIEU_LINH", "NGUOITAO", "PHONGDIEUTRI", "NGAYMAUBENHPHAM"
                        , "NGAYMAUBENHPHAM_SUDUNG", "TENKHO", "SOTHUTUCHIDINH", "LOAIPHIEU", "TRANGTHAI_PHIEU" }
                        );

                {
                    ucGridPhieuThuoc.setData(dt, ds.total, ds.page, ds.records);
                    ucGridPhieuThuoc.setColumnAll(false);

                    ucGridPhieuThuoc.setColumn("RN", 0, " ");
                    ucGridPhieuThuoc.setColumn("TRANGTHAIMAUBENHPHAM", 1, " ");
                    ucGridPhieuThuoc.setColumn("SOPHIEU", 2, "Số phiếu");
                    ucGridPhieuThuoc.setColumn("PHIEU_LINH", 3, "Phiếu lĩnh");
                    ucGridPhieuThuoc.setColumn("NGUOITAO", 4, "Bác sỹ chỉ định");
                    ucGridPhieuThuoc.setColumn("PHONGDIEUTRI", 5, "Phòng");
                    ucGridPhieuThuoc.setColumn("NGAYMAUBENHPHAM", 6, "Ngày chỉ định");
                    ucGridPhieuThuoc.setColumn("NGAYMAUBENHPHAM_SUDUNG", 7, "Ngày sử dụng");
                    ucGridPhieuThuoc.setColumn("TENKHO", 8, "Kho");
                    ucGridPhieuThuoc.setColumn("SOTHUTUCHIDINH", 9, "STT");
                    ucGridPhieuThuoc.setColumn("LOAIPHIEU", 10, "Loại phiếu");
                    ucGridPhieuThuoc.setColumn("TRANGTHAI_PHIEU", 11, "Trạng thái");

                    ucGridPhieuThuoc.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "6", "4" }
                        , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });

                    ucGridPhieuThuoc.gridView.BestFitColumns(true);
                }

                GetDataThuocChiTiet(0, null);
            }
        }

        private void GetDataThuocChiTiet(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page >= 0)
            {
                ucGridThuocChiTiet.clearData();

                ResponsList ds_KQ = ServiceTabDanhSachBenhNhan.getChiTietPhieuThuoc(page, ucGridThuocChiTiet.ucPage1.getNumberPerPage(), MAUBENHPHAMID);

                DataTable dt_KQ = MyJsonConvert.toDataTable(ds_KQ.rows);

                if (page == 0)
                {
                    dt_KQ.Clear();
                }

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "RN", "MADICHVU", "TENDICHVU", "SOLUONG", "DVT", "NGAYDUNG", "DUONGDUNG"
                        , "HUONGDANSUDUNG", "LOAIDOITUONG" });

                {
                    ucGridThuocChiTiet.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGridThuocChiTiet.setColumnAll(false);

                    ucGridThuocChiTiet.setColumn("RN", 0, " ");
                    ucGridThuocChiTiet.setColumn("MADICHVU", 1, "Mã thuốc");
                    ucGridThuocChiTiet.setColumn("TENDICHVU", 2, "Tên thuốc");
                    ucGridThuocChiTiet.setColumn("SOLUONG", 3, "Số lượng");
                    ucGridThuocChiTiet.setColumn("DVT", 4, "ĐVT");
                    ucGridThuocChiTiet.setColumn("NGAYDUNG", 5, "Ngày dùng");
                    ucGridThuocChiTiet.setColumn("DUONGDUNG", 6, "Đường dùng");
                    ucGridThuocChiTiet.setColumn("HUONGDANSUDUNG", 7, "Hướng dẫn sử dụng");
                    ucGridThuocChiTiet.setColumn("LOAIDOITUONG", 8, "Loại thanh toán");

                    ucGridThuocChiTiet.gridView.BestFitColumns(true);
                }
            }
        }

        string MAUBENHPHAMID = "";
        public void DanhSachThuocSelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
                MAUBENHPHAMID = selected["MAUBENHPHAMID"].ToString();
                GetDataThuocChiTiet(1, null);
            }
            else
            {
                ucGridThuocChiTiet.clearData_frmTiepNhan();
            }
        }

        private void MenuPopupClickDanhSachThuoc(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGridPhieuThuoc.gridView.GetFocusedRow());

                if (drv != null)
                {
                    // codwe web trong NTU02D033_ThongTinThuoc.js --> hàm: 
                    if (menu.hlink == "sentRequest" || menu.hlink == "sentRequestDtkh") //  _sendRequest(rowKey);
                    {
                        string ret = SendRequestThuoc(ModeView, drv, KHOAID);
                        if (ret.StartsWith("TRUE "))
                        {
                            MessageBox.Show(ret.Substring("TRUE ".Length));
                            Reload(LNMBP_PhieuThuoc);
                            // EventUtil.raiseEvent("assignDrug_cancel",{ option: ''});  ??? 
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
                        DeleteRequestThuoc(drv);
                    }
                    else if (menu.hlink == "delete" || menu.hlink == "deleteDtkh") // _deletePhieuThuocVatTu(rowKey); 
                    {
                        DeletePhieuThuocVatTu(drv);
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
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), LNMBP_PhieuThuoc);
                        OpenForm(frm, "1");
                    }
                    else if (menu.hlink == "chidinhLPDK") //  _createNoteAttach(rowKey);
                    {
                        NTU02D003_DichVuDinhKem frm = new NTU02D003_DichVuDinhKem();
                        frm.setData(drv["TIEPNHANID"].ToString(), drv["MAUBENHPHAMID"].ToString());
                        OpenForm(frm, "1");
                    }
                    else if (menu.hlink == "NTU02D081_Capnhat_PhieuDT")
                    {
                        // {"func":"ajaxExecuteQueryO","params":["","NTU02D009.EV004"],"options":[{"name":"[0]","value":"310546"}],"uuid":"6b9f8ac4-1223-4f85-b72d-2311ed4f8639"}
                        DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NTU02D009.EV004", drv["MAUBENHPHAMID"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            NTU02D081_Capnhat_PhieuDT frm = new NTU02D081_Capnhat_PhieuDT();
                            frm.loadData(drv["MAUBENHPHAMID"].ToString(), drv["KHAMBENHID"].ToString(), dt.Rows[0]["PHIEUDIEUTRIID"].ToString());
                            OpenForm(frm, "1");
                        }

                    }
                    else if (menu.hlink == "printPreview") //  _inDonThuoc(rowKey);
                    {
                        InDonThuoc(drv, 0);
                    }
                    else if (menu.hlink == "print") // _tuDongInDonThuoc(rowKey); 
                    {
                        InDonThuoc(drv, 1);
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void InDonThuoc(DataRowView drv, int typePrint)
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
                InPhieu("", "NGT006_DONTHUOC_17DBV01_TT052016_A5_913", table, _type, typePrint);
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
                            InPhieu("", "NGT020_DONTHUOCTHANGNGOAITRU", table, _type, typePrint);
                        }
                        else if ("6".Equals(_loaiThuoc))
                        {
                            InPhieu("", "NGT013_DONTHUOCHUONGTHAN_TT052016_A5", table, _type, typePrint);
                        }
                        else if ("7".Equals(_loaiThuoc))
                        {
                            InPhieu("", "NGT013_DONTHUOCGAYNGHIEN_TT052016_A5", table, _type, typePrint);
                        }
                        else
                        {
                            if ("944".Equals(hopital_id))
                            {
                                if (!"1".Equals(doiTuongBenhNhan))
                                {
                                    InPhieu("", "NGT006_DONTHUOC1L_17DBV01_TT052016_A5_944", table, _type, typePrint);
                                }
                                else
                                {
                                    InPhieu("", "NGT006_DONTHUOC1L_17DBV01_TT052016_A5_944", table, _type, typePrint);
                                }
                            }
                            else
                            {
                                InPhieu("", "NGT006_DONTHUOC_17DBV01_TT052016_A5", table, _type, typePrint);
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

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type, typePrint);
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

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type, typePrint);
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
                    InPhieu("", "NGT006_DONTHUOCK_17DBV01_TT052016_A5", table, _type, typePrint);
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

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type, typePrint);
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

                            InPhieu("", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table2, _type, typePrint);
                        }
                    }
                }
                #endregion
            }
        }

        private List<MenuFunc> MenuPopupThuocContextMenu()
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
            listMenu.Add(new MenuFunc("Cập nhật phiếu điều trị", "NTU02D081_Capnhat_PhieuDT", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("Xem và in đơn thuốc", "printPreview", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In đơn thuốc", "print", "1", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private string SendRequestThuoc(string modeView, DataRowView drv, string KHOAID)
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

        private void DeleteRequestThuoc(DataRowView drv)
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
            string _return = RequestHTTP.call_ajaxCALL_SP_S_error_msg("NT.G.DUYETPHIEU", "1$" + drv["MAUBENHPHAMID"].ToString());
            if (Func.Parse(_return) > 0)
            {
                MessageBox.Show("Phiếu đã được hủy yêu cầu thành công!");
                Reload(LNMBP_PhieuThuoc);
            }
            else
            {
                MessageBox.Show(_return);
            }
        }
        #endregion

        #region Phiếu vật tư
        private void GetDataVatTu(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page >= 0)
            {
                ClearAllGrid(LNMBP_PhieuVatTu);

                ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NT.024.DSTHUOCVT_NT", page, ucGridPhieuThuoc.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[1]", "[2]", "[3]", "[4]" },
                    new string[] { KHAMBENHID, BENHNHANID, LNMBP_PhieuVatTu, HOSOBENHNHANID, DICHVUKHAMBENHID }, ucGridPhieuThuoc.jsonFilter());
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TRANGTHAIMAUBENHPHAM", "SOPHIEU", "PHIEU_LINH", "NGUOITAO", "PHONGDIEUTRI"
                        , "NGAYMAUBENHPHAM", "NGAYMAUBENHPHAM_SUDUNG", "TENKHO", "SOTHUTUCHIDINH", "DIKEM", "LOAIPHIEU", "TRANGTHAI_PHIEU" });
                {
                    ucGridVatTu.setData(dt, ds.total, ds.page, ds.records);
                    ucGridVatTu.setColumnAll(false);

                    ucGridVatTu.setColumn("RN", 0, " ");
                    ucGridVatTu.setColumn("TRANGTHAIMAUBENHPHAM", 1, " ");
                    ucGridVatTu.setColumn("SOPHIEU", 2, "Số phiếu");
                    ucGridVatTu.setColumn("PHIEU_LINH", 3, "Phiếu lĩnh");
                    ucGridVatTu.setColumn("NGUOITAO", 4, "Bác sỹ chỉ định");
                    ucGridVatTu.setColumn("PHONGDIEUTRI", 5, "Phòng");
                    ucGridVatTu.setColumn("NGAYMAUBENHPHAM", 6, "Ngày chỉ định");
                    ucGridVatTu.setColumn("NGAYMAUBENHPHAM_SUDUNG", 7, "Ngày sử dụng");
                    ucGridVatTu.setColumn("TENKHO", 8, "Kho");
                    ucGridVatTu.setColumn("SOTHUTUCHIDINH", 10, "STT");
                    ucGridVatTu.setColumn("DIKEM", 9, "Đi kèm");
                    ucGridVatTu.setColumn("LOAIPHIEU", 11, "Loại phiếu");
                    ucGridVatTu.setColumn("TRANGTHAI_PHIEU", 12, "Trạng thái");

                    ucGridVatTu.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "6", "4" }
                        , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });
                    ucGridVatTu.setColumnImage("DIKEM", new String[] { "1" }
                        , new String[] { "./Resources/Pin.png" });

                    ucGridVatTu.gridView.BestFitColumns(true);
                }

                GetDataVatTuChiTiet(0, null);
            }
        }

        private void GetDataVatTuChiTiet(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page >= 0)
            {
                ucGridVatTuChiTiet.clearData();

                ResponsList ds_KQ = ServiceTabDanhSachBenhNhan.getChiTietPhieuThuoc(page, ucGridVatTuChiTiet.ucPage1.getNumberPerPage(), MAUBENHPHAMID);
                DataTable dt_KQ = MyJsonConvert.toDataTable(ds_KQ.rows);

                if (page == 0)
                {
                    dt_KQ.Clear();
                }

                if (dt_KQ.Rows.Count == 0) dt_KQ = Func.getTableEmpty(new String[] { "RN", "MADICHVU", "TENDICHVU", "SOLUONG"
                        , "DVT", "NGAYDUNG", "DUONGDUNG", "HUONGDANSUDUNG", "LOAIDOITUONG" });

                {
                    ucGridVatTuChiTiet.setData(dt_KQ, ds_KQ.total, ds_KQ.page, ds_KQ.records);
                    ucGridVatTuChiTiet.setColumnAll(false);

                    ucGridVatTuChiTiet.setColumn("RN", 0, " ");
                    ucGridVatTuChiTiet.setColumn("MADICHVU", 1, "Mã vật tư");
                    ucGridVatTuChiTiet.setColumn("TENDICHVU", 2, "Tên vật tư");
                    ucGridVatTuChiTiet.setColumn("SOLUONG", 3, "Số lượng");
                    ucGridVatTuChiTiet.setColumn("DVT", 4, "ĐVT");
                    ucGridVatTuChiTiet.setColumn("NGAYDUNG", 5, "Ngày dùng");
                    ucGridVatTuChiTiet.setColumn("DUONGDUNG", 6, "Đường dùng");
                    ucGridVatTuChiTiet.setColumn("HUONGDANSUDUNG", 7, "Hướng dẫn sử dụng");
                    ucGridVatTuChiTiet.setColumn("LOAIDOITUONG", 8, "Loại thanh toán");

                    ucGridVatTuChiTiet.gridView.BestFitColumns(true);
                }
            }
        }

        public void DanhSachVatTuSelectRow(object sender, EventArgs e)
        {
            DataRowView selected = (DataRowView)sender;
            if (selected != null)
            {
                MAUBENHPHAMID = selected["MAUBENHPHAMID"].ToString();
                GetDataVatTuChiTiet(1, null);
            }
            else
            {
                ucGridVatTuChiTiet.clearData_frmTiepNhan();
            }
        }

        private void MenuPopupClickDanhSachVatTu(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                MenuFunc menu = (MenuFunc)menuFunc;
                DataRowView drv = (DataRowView)(ucGridVatTu.gridView.GetFocusedRow());

                if (drv != null)
                {
                    // codwe web trong NTU02D034_ThongTinVatTu.js --> hàm: 
                    if (menu.hlink == "sentRequest" || menu.hlink == "sentRequestDtkh") //  _sendRequest(rowKey);
                    {
                        SendRequestVatTu(drv);
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
                        DeleteRequestVatTu(drv);
                    }
                    else if (menu.hlink == "delete" || menu.hlink == "deleteDtkh") // _deletePhieuThuocVatTu(rowKey);
                    {
                        DeletePhieuThuocVatTu(drv);
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
                        frm.loadData(drv["MAUBENHPHAMID"].ToString(), LNMBP_PhieuVatTu);
                        OpenForm(frm, "1");
                    }
                    else if (menu.hlink == "chidinhLPDK") // _createNoteAttach(rowKey);
                    {
                        NTU02D003_DichVuDinhKem frm = new NTU02D003_DichVuDinhKem();
                        frm.setData(drv["TIEPNHANID"].ToString(), drv["MAUBENHPHAMID"].ToString());
                        OpenForm(frm, "1");
                    }
                    else if (menu.hlink == "printPreview") // _inDonThuoc(rowKey);
                    {
                        InVatTu(drv, 0);
                    }
                    else if (menu.hlink == "print") // _tuDongInDonThuoc(rowKey);
                    {
                        InVatTu(drv, 1);
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void InVatTu(DataRowView drv, int typePrint)
        {
            var _type = "pdf";
            var pars = new List<object>() { "HIS_FILEEXPORT_TYPE" };
            var data_ar = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", string.Join("$", pars));
            if (!"-1".Equals(data_ar))
            {
                _type = data_ar;
            }

            var phongId = drv["PHONGID"].ToString();
            var mauBenhPhamId = drv["MAUBENHPHAMID"].ToString();
            var soPhieu = drv["SOPHIEU"].ToString();

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

            var rpName = "VNPTHIS_IN_A4_";
            rpName += soPhieu;
            rpName += "_" + Func.getSysDatetime("DDMMYY-HH24MISS");
            rpName += "." + _type;

            InPhieu("", "PHIEU_VATTU_A4", table, _type, typePrint);
        }

        private List<MenuFunc> MenuPopupVatTuContextMenu()
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

            listMenu.Add(new MenuFunc("In phiếu", "", "", ""));

            listMenu.Add(new MenuFunc("Xem và in phiếu vật tư", "printPreview", "0", "barButtonItem3.Glyph.png"));
            listMenu.Add(new MenuFunc("In phiếu vật tư", "print", "1", "barButtonItem3.Glyph.png"));
            return listMenu;
        }

        private void SendRequestVatTu(DataRowView drv)
        {
            // check quyen xoa du lieu
            if (ModeView != "2")
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
                    Reload(LNMBP_PhieuVatTu);
                }
                else
                    MessageBox.Show("Phiếu gửi yêu cầu thất bại!");
            }
            else
            {
                MessageBox.Show("Phiếu đã được gửi yêu cầu!");
            }
        }

        private void DeleteRequestVatTu(DataRowView drv)
        {
            if (ModeView != "2")
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
                Reload(LNMBP_PhieuVatTu);
            }
            else
            {
                MessageBox.Show("Phiếu hủy yêu cầu thất bại!");
            }
        }

        protected EventHandler event_BackParentForm;
        public void setEvent_BackParentForm(EventHandler event_BackParentForm)
        {
            this.event_BackParentForm = event_BackParentForm;
        }
        #endregion

        #region Event
        private void btnRefreshThuoc_Click(object sender, EventArgs e)
        {
            GetDataThuoc(1, null);
        }

        private void btnRefreshChiTiet_Click(object sender, EventArgs e)
        {
            GetDataThuocChiTiet(1, null);
        }

        private void btnRefreshVatTu_Click(object sender, EventArgs e)
        {
            GetDataVatTu(1, null);
        }

        private void btnRefreshVatTuChiTiet_Click(object sender, EventArgs e)
        {
            GetDataVatTuChiTiet(1, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabThuocVatTu_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabThuocVatTu.SelectedPage == tabThuoc)
                {
                    LoadData(KHAMBENHID, BENHNHANID, LNMBP_PhieuThuoc, ModeView, "");
                }
                else
                {
                    LoadData(KHAMBENHID, BENHNHANID, LNMBP_PhieuVatTu, ModeView, "");
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        //private void GridView_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view.ActiveEditor is TextEdit)
        //    {
        //        TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //        textEdit.Text = textEdit.Text.Trim();
        //    }
        //}
        #endregion

        #region Common method
        private void LoadData(string KhamBenhId, string BenhNhanId, string lnmbpPhieu, string modeView, string hoSoBenhNhan)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (LNMBP_PhieuThuoc.Equals(lnmbpPhieu))
                {
                    LoadMenuPopUpGrid(LNMBP_PhieuThuoc);
                    GetDataThuoc(1, null);
                }
                else if (LNMBP_PhieuVatTu.Equals(lnmbpPhieu))
                {
                    LoadMenuPopUpGrid(LNMBP_PhieuVatTu);
                    GetDataVatTu(1, null);
                }

            }
            finally
            {
                //Close Wait Form
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void LoadMenuPopUpGrid(string lnmbpPhieu)
        {
            if (LNMBP_PhieuThuoc.Equals(lnmbpPhieu))
                ucGridPhieuThuoc.addMenuPopup(MenuPopupThuocContextMenu());
            else if (LNMBP_PhieuVatTu.Equals(lnmbpPhieu))
                ucGridVatTu.addMenuPopup(MenuPopupVatTuContextMenu());
        }

        public void Reload(string lnmbpPhieu)
        {
            if (LNMBP_PhieuThuoc.Equals(lnmbpPhieu))
                GetDataThuoc(1, null);
            else if (LNMBP_PhieuVatTu.Equals(lnmbpPhieu))
                GetDataVatTu(1, null);
        }

        private void ClearAllGrid(string lnmbpPhieu)
        {
            if (LNMBP_PhieuThuoc.Equals(lnmbpPhieu))
            {
                ucGridPhieuThuoc.clearData();
                ucGridThuocChiTiet.clearData_frmTiepNhan();
            }
            else if (LNMBP_PhieuVatTu.Equals(lnmbpPhieu))
            {
                ucGridVatTu.clearData();
                ucGridVatTuChiTiet.clearData_frmTiepNhan();
            }
        }

        private void OpenForm(Form frm, string optionsPopup)
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

        private void DeletePhieuThuocVatTu(DataRowView drv)
        {
            if (ModeView != "2")
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
                        Reload(LNMBP_PhieuThuoc);
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

        private void InPhieu(string title, string code, DataTable parTable, string type, int typePrint, int height = 650, int width = 900)
        {
            Func.PrintFile_FromData(code, parTable, type);

            if (typePrint == 0)
            {
                PrintPreview(title, code, parTable);
            }
        }

        private void PrintPreview(string title, string code, DataTable parTable, int height = 650, int width = 900)
        {
            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(title, code, parTable, height, width);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
        #endregion
    }
}