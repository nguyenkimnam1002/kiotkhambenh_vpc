using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.CommonForm;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using VNPT.HIS.Controls.SubForm;
using System.Xml;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T001_thuvienphi : DevExpress.XtraEditors.XtraForm
    {
        public class PhieuThuCT
        {
            public string KHOANMUCID { get; internal set; }
            public string MAKHOANMUC { get; internal set; }
            public string TENKHOANMUC { get; internal set; }
            public string TONGTIEN { get; internal set; }
        }

        public class PhieuThuDichVu
        {
            public string THUCTHU { get; set; }
            public string TIEN_BHYT_TRA { get; set; }
            public string TIEN_MIENGIAM { get; set; }
            public string DICHVUKHAMBENHID { get; set; }
            public string KHOANMUCID { get; set; }
            public string TENKHOANMUC { get; set; }
            public string MAKHOANMUC { get; set; }
            public string VERSION_OLD { get; set; }
        }

        public class PhieuThuObj2 : ICloneable
        {
            public string DANHSACHDOITUONGDICHVU { get; set; }
            public List<PhieuThuDichVu> DSDV { get; set; }
            public string DS_MAUBENHPHAMID { get; set; }
            public string KHOASOPHIEUTU { get; set; }
            public string LOAIDOITUONG { get; set; }
            public string LOAIPHIEUTHU { get; set; }
            public string LOAIPHIEUTHUID { get; set; }
            public string LOAITHUTIEN { get; set; }
            public string MANHOMPHIEUTHU { get; set; }
            public string MAPHIEUTHU { get; set; }
            public string MIENGIAMDV { get; set; }
            public string NHOMPHIEUTHUID { get; set; }
            public string NHOMTHANHTOAN { get; set; }
            public string NOIDUNGIN { get; set; }
            public string NOIDUNGTHU { get; set; }
            public string PHONGID_DANGNHAP { get; set; }
            public List<PhieuThuCT> PTCT { get; set; }
            public string SOPHIEUFROM { get; set; }
            public string SOPHIEUTO { get; set; }
            public string THUCTHU { get; set; }
            public string TIEN_BHYT_TRA { get; set; }
            public string TONGTIEN { get; set; }

            #region ICloneable Members
            public object Clone()
            {
                return this.MemberwiseClone();
            }

            object ICloneable.Clone()
            {
                return this.MemberwiseClone();
            }
            #endregion
        }

        #region Variable

        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DataRow vpiConfig;
        private bool flagLoading = false;
        private DataRowView drvDichVu = null;
        private DataRowView drvBenhNhan = null;
        private DataRow _benhNhan = null;
        private DataRow drVpData = null;
        private DataTable arr_DV_CT = null;
        private DataTable _dsSo_2 = null;
        private DataTable dtDVSai = null;

        private string _THUTIEN = "1";
        private string _TAMUNG = "3";
        private string _HOANUNG = "2";
        private string _THUTHEM = "6";
        private string _HOANDICHVU = "8";
        private string _DT_VIENPHI = "2";
        private string _SL_VIENPHI = "8";
        private string _DT_DICHVU = "3";
        private string _SL_DICHVU = "3";
        private string _phieuthuid = "-1";

        private string tiepNhanId = "-1";
        private bool flagTinhTongTien = true;
        private bool chot = false;
        private bool _flag_duyet = false;
        private List<string> listKhoa = new List<string>();

        private List<PhieuThuObj2> phieuInfo = new List<PhieuThuObj2>();

        private readonly string tXoaDichVu = "Xóa dịch vụ";
        private readonly string tDichVu = "Dịch vụ";
        private readonly string rHuyPhieuThu = "rHuyPhieuThu";
        private readonly string rKhoiPhuc = "rKhoiPhuc";
        private readonly string rCapNhatPhieuThu = "rCapNhatPhieuThu";
        private readonly string rNhapThongTinThanhToan = "rNhapThongTinThanhToan";
        private readonly string rXemHDDT = "rXemHDDT";
        private readonly string rGuiHDDT = "rGuiHDDT";
        private readonly string tHuyPhieuThu = "Hủy phiếu thu";
        private readonly string tKhoiPhuc = "Khôi phục phiếu";
        private readonly string tCapNhatPhieuThu = "Cập nhật phiếu thu";
        private readonly string tNhapThongTinThanhToan = "Nhập thông tin thanh toán BN";
        private readonly string tXemHDDT = "Xem hóa đơn";
        private readonly string tGuiHDDT = "Gửi hóa đơn";
        private readonly string tLichSuThanhToan = "Thanh toán viện phí";
        private readonly string rLichSuThanhToan = "VNPT.HIS.VienPhi.VPI01T004_lichsuthanhtoan";
        private readonly string rThemMaSoThue = "rThemMaSoThue";
        private readonly string tThemMaSoThue = "Thêm MST";
        private readonly string rInBangKe = "rInBangKe";
        private readonly string tInBangKe = "In bảng kê";
        private readonly string rLichSuTheoCongBHYT = "rLichSuTheoCongBHYT";
        private readonly string tLichSuTheoCongBHYT = "Lịch sử theo cổng BHYT";
        private readonly string rLichSuDieuTri = "rLichSuDieuTri";
        private readonly string tLichSuDieuTri = "Lịch sử điều trị";
        private readonly string rHenKham = "rHenKham";
        private readonly string tHenKham = "Hẹn khám";
        private readonly string rDuyetThucHienCLS = "rDuyetThucHienCLS";
        private readonly string tDuyetThucHienCLS = "Duyệt thực hiện CLS";

        #endregion

        #region Public

        public VPI01T001_thuvienphi()
        {
            InitializeComponent();
        }

        #endregion

        #region Private

        /// <summary>
        /// Khởi tạo giá trị ban đầu
        /// </summary>
        private void InitForm()
        {
            try
            {
                #region deTuNgay, deDenNgay
                deTuNgay.DateTime = Func.getSysDatetime_Short();
                deDenNgay.DateTime = deTuNgay.DateTime;
                #endregion

                #region load data ucCboTrangThai
                DataTable dtTrangThai = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow rowTrangThai;
                rowTrangThai = dtTrangThai.NewRow();
                rowTrangThai["col1"] = "0";
                rowTrangThai["col2"] = "Tất cả";
                dtTrangThai.Rows.Add(rowTrangThai);

                rowTrangThai = dtTrangThai.NewRow();
                rowTrangThai["col1"] = "1";
                rowTrangThai["col2"] = "Viện phí mở";
                dtTrangThai.Rows.Add(rowTrangThai);

                rowTrangThai = dtTrangThai.NewRow();
                rowTrangThai["col1"] = "2";
                rowTrangThai["col2"] = "Viện phí đóng";
                dtTrangThai.Rows.Add(rowTrangThai);

                rowTrangThai = dtTrangThai.NewRow();
                rowTrangThai["col1"] = "3";
                rowTrangThai["col2"] = "Đã duyệt kế toán";
                dtTrangThai.Rows.Add(rowTrangThai);

                //rowTrangThai = dtTrangThai.NewRow();
                //rowTrangThai["col1"] = "4";
                //rowTrangThai["col2"] = "Kết thúc thanh toán + trốn viện";
                //dtTrangThai.Rows.Add(rowTrangThai);

                rowTrangThai = dtTrangThai.NewRow();
                rowTrangThai["col1"] = "5";
                rowTrangThai["col2"] = "Đã duyệt BHYT";
                dtTrangThai.Rows.Add(rowTrangThai);

                rowTrangThai = dtTrangThai.NewRow();
                rowTrangThai["col1"] = "6";
                rowTrangThai["col2"] = "Duyệt KT chưa duyệt BH";
                dtTrangThai.Rows.Add(rowTrangThai);

                rowTrangThai = dtTrangThai.NewRow();
                rowTrangThai["col1"] = "9";
                rowTrangThai["col2"] = "Duyệt BH chưa duyệt KT";
                dtTrangThai.Rows.Add(rowTrangThai);

                ucCboTrangThai.setData(dtTrangThai, "col1", "col2");
                ucCboTrangThai.SelectIndex = 0;
                ucCboTrangThai.setColumn(0, false);
                #endregion

                #region load data ucCboLoaiTT
                DataTable dtLoaiTT = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow rowLoaiTT;
                rowLoaiTT = dtLoaiTT.NewRow();
                rowLoaiTT["col1"] = "1";
                rowLoaiTT["col2"] = "Tiền mặt";
                dtLoaiTT.Rows.Add(rowLoaiTT);

                rowLoaiTT = dtLoaiTT.NewRow();
                rowLoaiTT["col1"] = "2";
                rowLoaiTT["col2"] = "Chuyển khoản";
                dtLoaiTT.Rows.Add(rowLoaiTT);

                ucCboLoaiTT.setData(dtLoaiTT, "col1", "col2");
                ucCboLoaiTT.setColumn(0, false);

                ucCboLoaiTT.SelectIndex = 0;
                #endregion

                #region load data ucCboLoaiVienPhi
                var resultLoaiVienPhi = RequestHTTP.call_ajaxCALL_SP_O("LOAIVIENPHI", "$", 0);
                List<string> dsLoaiVienPhi = new List<string>();
                string loaiVienPhi = string.Empty;
                bool tatCaVP = false;
                DataTable dtLoaiVienPhi = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow rowLoaiVienPhi;
                if (resultLoaiVienPhi != null && resultLoaiVienPhi.Rows.Count > 0)
                {
                    for (int i = 0; i < resultLoaiVienPhi.Rows.Count; i++)
                    {
                        loaiVienPhi = resultLoaiVienPhi.Rows[i]["LOAIVIENPHIID"].ToString();
                        if ("100".Equals(loaiVienPhi))
                        {
                            tatCaVP = true;
                        }
                        else if (!string.IsNullOrEmpty(loaiVienPhi))
                        {
                            dsLoaiVienPhi.Add(loaiVienPhi);
                        }
                        rowLoaiVienPhi = dtLoaiVienPhi.NewRow();
                        rowLoaiVienPhi["col1"] = loaiVienPhi;
                        rowLoaiVienPhi["col2"] = resultLoaiVienPhi.Rows[i]["LOAIVIENPHI"].ToString();

                        dtLoaiVienPhi.Rows.Add(rowLoaiVienPhi);
                    }

                    if (!tatCaVP && dsLoaiVienPhi.Count > 0)
                    {
                        rowLoaiVienPhi = dtLoaiVienPhi.NewRow();
                        rowLoaiVienPhi["col1"] = string.Join(",", dsLoaiVienPhi);
                        rowLoaiVienPhi["col2"] = "-- Chọn --";

                        dtLoaiVienPhi.Rows.InsertAt(rowLoaiVienPhi, 0);
                    }
                }
                ucCboLoaiVienPhi.setData(dtLoaiVienPhi, "col1", "col2");
                ucCboLoaiVienPhi.SelectIndex = 0;
                ucCboLoaiVienPhi.setColumn(0, false);
                #endregion

                #region load data ucCboDoiTuong
                var resultDoiTuongBenhNhan = RequestHTTP.call_ajaxCALL_SP_O("THUTHEO_DOITUONG", "$", 0);
                List<string> dsLoaiDoiTuong = new List<string>();
                string doiTuongId = string.Empty;
                bool tatCaDT = false;
                DataTable dtDoiTuong = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow rowDoiTuong;
                if (resultDoiTuongBenhNhan != null && resultDoiTuongBenhNhan.Rows.Count > 0)
                {
                    for (int i = 0; i < resultDoiTuongBenhNhan.Rows.Count; i++)
                    {
                        doiTuongId = resultDoiTuongBenhNhan.Rows[i]["DTBNID"].ToString();
                        if ("100".Equals(doiTuongId))
                        {
                            tatCaDT = true;
                        }
                        else if (!string.IsNullOrEmpty(doiTuongId))
                        {
                            dsLoaiDoiTuong.Add(doiTuongId);
                        }
                        rowDoiTuong = dtDoiTuong.NewRow();
                        rowDoiTuong["col1"] = doiTuongId;
                        rowDoiTuong["col2"] = resultDoiTuongBenhNhan.Rows[i]["TEN_DTBN"].ToString();

                        dtDoiTuong.Rows.Add(rowDoiTuong);
                    }

                    if (!tatCaDT && dsLoaiDoiTuong.Count > 0)
                    {
                        rowDoiTuong = dtDoiTuong.NewRow();
                        rowDoiTuong["col1"] = string.Join(",", dsLoaiDoiTuong);
                        rowDoiTuong["col2"] = "-- Chọn --";

                        dtDoiTuong.Rows.InsertAt(rowDoiTuong, 0);
                    }
                }
                ucCboDoiTuong.setData(dtDoiTuong, "col1", "col2");
                ucCboDoiTuong.SelectIndex = 0;
                ucCboDoiTuong.setColumn(0, false);
                #endregion

                #region load data ucCboNhomDV
                var resultNhomDV = RequestHTTP.call_ajaxCALL_SP_O("VPI_LAY_LOAIQUAY", Const.local_phongId.ToString(), 0);
                DataTable dtNhomDV = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow rowNhomDV;
                if (resultNhomDV != null && resultNhomDV.Rows.Count > 0)
                {
                    for (int i = 0; i < resultNhomDV.Rows.Count; i++)
                    {
                        rowNhomDV = dtNhomDV.NewRow();
                        rowNhomDV["col1"] = resultNhomDV.Rows[i]["LOAI"].ToString();
                        rowNhomDV["col2"] = resultNhomDV.Rows[i]["TEN"].ToString();

                        dtNhomDV.Rows.Add(rowNhomDV);
                    }
                }
                ucCboNhomDV.setData(dtNhomDV, "col1", "col2");
                ucCboNhomDV.SelectIndex = 0;
                ucCboNhomDV.setColumn(0, false);
                #endregion

                #region load config
                var resultConfig = RequestHTTP.call_ajaxCALL_SP_O("VPI.LAY.CAUHINH", "$", 0);
                if (resultConfig != null && resultConfig.Rows.Count > 0)
                {
                    vpiConfig = resultConfig.Rows[0];
                }
                #endregion

                #region thu khác
                if ("0".Equals(vpiConfig["HIS_THUNGAN_THUKHAC"].ToString()))
                {
                    btnThuKhac.Visible = false;
                }
                #endregion

                #region ucCboLoaiPhieu loại phiếu thu
                DataTable dtLoaiPhieu = Func.getTableEmpty(new string[] { "col1", "col2" });
                var rowLoaiPhieu = dtLoaiPhieu.NewRow();
                rowLoaiPhieu["col1"] = "6";
                rowLoaiPhieu["col2"] = "Thu tiền";
                dtLoaiPhieu.Rows.Add(rowLoaiPhieu);

                rowLoaiPhieu = dtLoaiPhieu.NewRow();
                rowLoaiPhieu["col1"] = "3";
                rowLoaiPhieu["col2"] = "Tạm ứng";
                dtLoaiPhieu.Rows.Add(rowLoaiPhieu);

                rowLoaiPhieu = dtLoaiPhieu.NewRow();
                rowLoaiPhieu["col1"] = "2";
                rowLoaiPhieu["col2"] = "Hoàn ứng";
                dtLoaiPhieu.Rows.Add(rowLoaiPhieu);

                if ("1".Equals(vpiConfig["VPI_HOANDICHVU"].ToString()))
                {
                    rowLoaiPhieu = dtLoaiPhieu.NewRow();
                    rowLoaiPhieu["col1"] = "8";
                    rowLoaiPhieu["col2"] = "Hoàn dịch vụ";
                    dtLoaiPhieu.Rows.Add(rowLoaiPhieu);
                }

                ucCboLoaiPhieu.setData(dtLoaiPhieu, "col1", "col2");
                ucCboLoaiPhieu.setEvent(SelectionChange_ucCboLoaiPhieu);

                #endregion

                #region load ucGridDichVu
                ucGridDichVu.gridView.OptionsView.ShowViewCaption = false;
                ucGridDichVu.gridView.OptionsBehavior.Editable = false;
                ucGridDichVu.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGridDichVu.gridView.GroupFormat = "[#image]{1} {2}";
                ucGridDichVu.gridView.OptionsBehavior.Editable = true;
                ucGridDichVu.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridDichVu.gridView.Click += ucGridDichVu_Click;
                ucGridDichVu.gridView.RowStyle += GridDichVu_RowStyle;
                ucGridDichVu.gridView.CellValueChanged += GridDichVu_CellValueChanged;

                ucGridDichVu.addMenuPopup(new List<MenuFunc>()
                        {
                            new MenuFunc(this.tDichVu,"","0",""),
                            new MenuFunc(this.tXoaDichVu,"XoaDichVu","0","barButtonItem3.Glyph.png"),
                        });
                ucGridDichVu.setEvent_MenuPopupClick(MenuPopupGridDichVuClick);

                #endregion

                #region load ucGridPhieuThu
                ucGridPhieuThu.gridView.OptionsView.ShowViewCaption = false;
                ucGridPhieuThu.gridView.OptionsBehavior.Editable = false;
                ucGridPhieuThu.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGridPhieuThu.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridPhieuThu.gridView.RowStyle += GridPhieuThu_RowStyle;
                ucGridPhieuThu.gridView.Click += GridPhieuThu_Click;
                ucGridPhieuThu.addMenuPopup(new List<MenuFunc>()
                {
                    new MenuFunc("Phiếu thu","","0",""),
                    new MenuFunc(tHuyPhieuThu,rHuyPhieuThu,"0","barButtonItem3.Glyph.png"),
                    new MenuFunc(tKhoiPhuc,rKhoiPhuc,"0","barButtonItem3.Glyph.png"),
                    new MenuFunc(tCapNhatPhieuThu,rCapNhatPhieuThu,"0","barButtonItem3.Glyph.png"),
                    new MenuFunc(tNhapThongTinThanhToan,rNhapThongTinThanhToan,"0","barButtonItem3.Glyph.png"),
                    new MenuFunc("Hóa đơn điện tử","","0",""),
                    new MenuFunc(tXemHDDT,rXemHDDT,"0","barButtonItem3.Glyph.png"),
                    new MenuFunc(tGuiHDDT,rGuiHDDT,"0","barButtonItem3.Glyph.png"),
                });
                ucGridPhieuThu.setEvent_MenuPopupClick(MenuPopupGridPhieuThuClick);
                #endregion

                #region load ucGridBenhNhan
                ucGridBenhNhan.gridView.OptionsView.ShowViewCaption = true;
                ucGridBenhNhan.gridView.ViewCaption = "Danh sách bệnh nhân";
                ucGridBenhNhan.gridView.OptionsBehavior.Editable = false;
                ucGridBenhNhan.gridView.OptionsView.ColumnAutoWidth = false;


                ucGridBenhNhan.gridView.Click += GridBenhNhan_Click;
                ucGridBenhNhan.SetReLoadWhenFilter(true);

                ucGridBenhNhan.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
                ucGridBenhNhan.onIndicator();
                ucGridBenhNhan.addMenuPopup(new List<MenuFunc>()
                {
                    new MenuFunc("Viện phí","","0",""),
                    new MenuFunc(tLichSuThanhToan, rLichSuThanhToan,"0",""),
                    new MenuFunc(tThemMaSoThue,rThemMaSoThue,"0",""),
                    new MenuFunc(tInBangKe,rInBangKe,"0",""),
                    new MenuFunc(tLichSuTheoCongBHYT,rLichSuTheoCongBHYT,"0",""),
                    new MenuFunc("Bệnh án","","0",""),
                    new MenuFunc(tLichSuDieuTri,rLichSuDieuTri,"0",""),
                    new MenuFunc(tHenKham,rHenKham,"0",""),
                    new MenuFunc("Dịch vụ","","0",""),
                    new MenuFunc(tDuyetThucHienCLS,rDuyetThucHienCLS,"0",""),
                });
                ucGridBenhNhan.setEvent_MenuPopupClick(MenuPopupGridBenhNhanClick);

                #endregion

                this.EnabledButton = false;
                this.ReadOnly = true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void SelectionChange_ucCboLoaiPhieu(object sender, EventArgs e)
        {
            if (flagLoading)
            {
                ucCboLoaiPhieu.setEvent(null);

                var _loaiPhieuThu = ucCboLoaiPhieu.SelectValue;
                this.LoadDataGridDichVu(null, "", _loaiPhieuThu);

                ucCboLoaiPhieu.setEvent(SelectionChange_ucCboLoaiPhieu);
            }
        }

        private void SelectionChange_ucCboMaSo(object sender, EventArgs e)
        {
            if (flagLoading)
            {
                var _loaiPhieuThu = Func.Parse(ucCboMaSo.SelectValue);
                if (_dsSo_2 != null && _dsSo_2.Rows.Count > 0 && obj != null && obj_2 != null)
                {
                    SetText(teMaPhieu, GetValueByName(_dsSo_2.Rows[_loaiPhieuThu], "MAPHIEUTHU"));
                    SetText(ucCboLoaiPhieu, obj.LOAIPHIEUTHUID);
                    obj_2.LOAIPHIEUTHUID = _THUTIEN;
                    ucCboMaSo.SelectValue = _loaiPhieuThu.ToString();
                    foreach (var item in phieuInfo)
                    {
                        item.NHOMPHIEUTHUID = GetValueByName(_dsSo_2.Rows[_loaiPhieuThu], "NHOMPHIEUTHUID");
                        item.MANHOMPHIEUTHU = GetValueByName(_dsSo_2.Rows[_loaiPhieuThu], "MANHOMPHIEUTHU");
                        item.MAPHIEUTHU = GetValueByName(_dsSo_2.Rows[_loaiPhieuThu], "MAPHIEUTHU");
                        item.LOAIPHIEUTHU = GetValueByName(_dsSo_2.Rows[_loaiPhieuThu], "LOAIPHIEUTHU");
                    }
                }
            }
        }

        private void GridDichVu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.drvDichVu == null)
            {
                return;
            }

            var rowDichVuCur = (DataRowView)ucGridDichVu.gridView.GetFocusedRow();
            if (rowDichVuCur == null)
            {
                return;
            }

            var soLuongCu = this.drvDichVu["SOLUONG"].ToString();
            var soLuong = rowDichVuCur["SOLUONG"].ToString();
            var dichVuKhamBenhId = this.drvDichVu["DICHVUKHAMBENHID"].ToString();
            var loaiDoiTuong = this.drvDichVu["LOAIDOITUONG"].ToString();
            var daThuTien = this.drvDichVu["DATHUTIEN"].ToString();

            if (flagLoading
                || "3".Equals(daThuTien)
                || "1".Equals(loaiDoiTuong)
                || "2".Equals(loaiDoiTuong)
                || "3".Equals(loaiDoiTuong))
            {
                rowDichVuCur["SOLUONG"] = soLuongCu;
            }
            else
            {
                int iSoLuong = 0;
                int.TryParse(soLuong, out iSoLuong);
                int iSoLuongCu = 0;
                int.TryParse(soLuongCu, out iSoLuongCu);

                var regex = new Regex(@"/^\d+$/i");
                var isMatch = regex.IsMatch(soLuong);
                if (isMatch && iSoLuong > 0 && iSoLuong < iSoLuongCu)
                {
                    var dialogResult = MessageBox.Show("Xác nhận sửa số lượng dịch vụ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var result = RequestHTTP.call_ajaxCALL_SP_I("VPI.TACH.DV", dichVuKhamBenhId + "$" + soLuong);
                        if ("1".Equals(result))
                        {
                            LoadDataGridDichVu(null);
                        }
                        else if ("0".Equals(result))
                        {
                            rowDichVuCur["SOLUONG"] = soLuongCu;
                            MessageBox.Show("Không thể sửa số lượng dịch vụ này");
                        }
                        else
                        {
                            rowDichVuCur["SOLUONG"] = soLuongCu;
                            MessageBox.Show("Cập nhật không thành công");
                        }
                    }
                    else
                    {
                        rowDichVuCur["SOLUONG"] = soLuongCu;
                    }
                }
                else
                {
                    rowDichVuCur["SOLUONG"] = soLuongCu;
                }
            }
        }

        private void GridDichVu_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DataRowView dr = (DataRowView)ucGridDichVu.gridView.GetRow(e.RowHandle);
            if (dr == null)
            {
                return;
            }

            if (ucGridDichVu.gridView.IsGroupRow(e.RowHandle))
            {
                return;
            }

            if ("3".Equals(dr["DATHUTIEN"].ToString()))
            {
                e.Appearance.ForeColor = Color.Blue;
            }

            if (("1".Equals(dr["LOAIDOITUONG"].ToString()) || "2".Equals(dr["LOAIDOITUONG"].ToString()) || "3".Equals(dr["LOAIDOITUONG"].ToString()))
                && !"16".Equals(GetValueByName(dr, "LOAINHOMMAUBENHPHAM"))
                && !GetValueByName(dr, "TYLE").Equals(GetValueByName(dr, "TYLE_BHYT_TRA"))
                && !"0".Equals(GetValueByName(dr, "TYLE_DV"))
                && !"0".Equals(dr["SOLUONG"].ToString())
                && (!"3".Equals(GetValueByName(dr, "LOAINHOMMAUBENHPHAM")) || !"0".Equals(dr["TIENDICHVU"].ToString()))
                && "0".Equals(dr["VATTU04"].ToString()))
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        private void GridPhieuThu_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DataRowView dr = (DataRowView)ucGridPhieuThu.gridView.GetRow(e.RowHandle);
            if (dr == null)
            {
                return;
            }

            if ("1".Equals(dr["DAHUYPHIEU"].ToString()))
            {
                e.Appearance.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Load danh sách bệnh nhân
        /// </summary>
        /// <param name="page"></param>
        private void LoadDataGridBenhNhan(int? page)
        {
            try
            {
                if (flagLoading)
                {
                    return;
                }

                if (deTuNgay.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa nhập trường từ ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    deTuNgay.Focus();
                    return;
                }
                if (deDenNgay.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa nhập trường đến ngày", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    deDenNgay.Focus();
                    return;
                }
                if (deTuNgay.DateTime > deDenNgay.DateTime)
                {
                    MessageBox.Show("Trường từ ngày không được lớn hơn trường đến ngày");
                    deTuNgay.Focus();
                    return;
                }

                var loaiVienPhi = ucCboLoaiVienPhi.SelectValue;
                var doiTuongId = ucCboDoiTuong.SelectValue;
                if (string.IsNullOrEmpty(loaiVienPhi) || string.IsNullOrEmpty(doiTuongId))
                {
                    return;
                }
                var trangThai = ucCboTrangThai.SelectValue;
                var loaiNgayVaoRa = radNgayVao.Checked ? "0" : "1";
                var tuNgay = deTuNgay.DateTime.ToString("dd/MM/yyyy");
                var denNgay = deDenNgay.DateTime.ToString("dd/MM/yyyy");

                int pageNum = 0;
                if (page == null)
                {
                    pageNum = ucGridBenhNhan.ucPage1.Current();
                }
                else
                {
                    pageNum = page.GetValueOrDefault();
                }

                if (pageNum <= 0)
                {
                    pageNum = 1;
                }

                //flagLoading = true;
                ResponsList responses = new ResponsList();
                //string jsonFilter = string.Empty;

                //if (ucGridBenhNhan.ReLoadWhenFilter
                //    && ucGridBenhNhan.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridBenhNhan.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "VPI01T001.01"
                    , pageNum
                    , ucGridBenhNhan.ucPage1.getNumberPerPage()
                    , new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", }
                    , new string[] { doiTuongId, loaiVienPhi, trangThai, loaiNgayVaoRa, tuNgay, denNgay }
                    , ucGridBenhNhan.jsonFilter());


                DataTable dtBenhNhan = new DataTable();
                dtBenhNhan = MyJsonConvert.toDataTable(responses.rows);
                if (dtBenhNhan.Rows.Count <= 0)
                {
                    dtBenhNhan = Func.getTableEmpty(
                        new String[] {
                                "ICON1"
                                ,"ICON2"
                                ,"TIEPNHANID"
                                , "TRANGTHAITIEPNHAN"
                                , "TRANGTHAITIEPNHAN_BH"
                                , "TRANGTHAITIEPNHAN_VP"
                                , "MAHOSOBENHAN"
                                , "TENBENHNHAN"
                                , "DIACHI"
                                , "NGAYSINH"
                                , "GIOITINH"
                                , "MA_BHYT"
                                , "KHOA"
                                , "MATIEPNHAN"
                                , "MABENHNHAN"
                                , "NGAYTIEPNHAN"
                                , "NGAY_RAVIEN"
                                , "STT"
                        });
                }
                else
                {
                    dtBenhNhan.Columns.Add("ICON1");
                    dtBenhNhan.Columns.Add("ICON2");
                }

                for (int i = 0; i < dtBenhNhan.Rows.Count; i++)
                {
                    if ("1".Equals(dtBenhNhan.Rows[i]["TRANGTHAITIEPNHAN"].ToString())
                        || "2".Equals(dtBenhNhan.Rows[i]["TRANGTHAITIEPNHAN"].ToString()))
                    {
                        dtBenhNhan.Rows[i]["ICON1"] = "3";
                        if ("1".Equals(dtBenhNhan.Rows[i]["TRANGTHAITIEPNHAN_VP"].ToString())
                            || "1".Equals(dtBenhNhan.Rows[i]["TRANGTHAITIEPNHAN_BH"].ToString()))
                        {
                            dtBenhNhan.Rows[i]["ICON1"] = "4";
                        }
                    }

                    if ("2".Equals(dtBenhNhan.Rows[i]["TRANGTHAITIEPNHAN"].ToString()))
                    {
                        dtBenhNhan.Rows[i]["ICON2"] = "2";
                    }
                }

                //for (int i = 0; i < dtBenhNhan.Rows.Count; i++)
                //{
                //    dtBenhNhan.Rows[i]["MABENHNHAN"] = null;
                //    dtBenhNhan.Rows[i]["NGUOIDUYET"] = "";
                //}
                ucGridBenhNhan.setData(dtBenhNhan, responses.total, responses.page, responses.records);

                #region hiển thị theo HIS_TIMKIEM_VIENPHI
                if ("0".Equals(vpiConfig["HIS_TIMKIEM_VIENPHI"].ToString()))
                {
                    #region 0
                    ucGridBenhNhan.setColumnAll(false);
                    ucGridBenhNhan.setColumn("ICON1", 0, " ", 25);
                    ucGridBenhNhan.setColumn("ICON2", 1, " ", 25);
                    ucGridBenhNhan.setColumn("MABENHNHAN", 2, "Mã bệnh nhân", 90);
                    ucGridBenhNhan.setColumnMemoEdit("TENBENHNHAN", 3, "Họ tên", 140);
                    ucGridBenhNhan.setColumnMemoEdit("DIACHI", 4, "Địa chỉ", 220);
                    ucGridBenhNhan.setColumn("NGAYSINH", 5, "Ngày sinh", 80);
                    ucGridBenhNhan.setColumn("GIOITINH", 6, "GT", 40);
                    ucGridBenhNhan.setColumn("MA_BHYT", 7, "Mã BHYT", 120);
                    ucGridBenhNhan.setColumnMemoEdit("KHOA", 8, "Khoa", 130);
                    ucGridBenhNhan.setColumn("MAHOSOBENHAN", 9, "Mã bệnh án", 90);
                    ucGridBenhNhan.setColumn("MATIEPNHAN", 10, "Mã viện phí", 95);
                    ucGridBenhNhan.setColumn("NGAYTIEPNHAN", 11, "Vào viện", 140);
                    ucGridBenhNhan.setColumn("NGAY_RAVIEN", 12, "Ra viện", 140);
                    #endregion
                }
                else if ("1".Equals(vpiConfig["HIS_TIMKIEM_VIENPHI"].ToString()))
                {
                    #region 1
                    ucGridBenhNhan.setColumnAll(false);
                    ucGridBenhNhan.setColumn("ICON1", 0, " ", 25);
                    ucGridBenhNhan.setColumn("ICON2", 1, " ", 25);
                    ucGridBenhNhan.setColumn("MAHOSOBENHAN", 2, "Mã bệnh án", 90);
                    ucGridBenhNhan.setColumnMemoEdit("TENBENHNHAN", 3, "Họ tên", 140);
                    ucGridBenhNhan.setColumnMemoEdit("DIACHI", 4, "Địa chỉ", 220);
                    ucGridBenhNhan.setColumn("NGAYSINH", 5, "Ngày sinh", 80);
                    ucGridBenhNhan.setColumn("GIOITINH", 6, "GT", 40);
                    ucGridBenhNhan.setColumn("MA_BHYT", 7, "Mã BHYT", 120);
                    ucGridBenhNhan.setColumnMemoEdit("KHOA", 8, "Khoa", 130);
                    ucGridBenhNhan.setColumn("MATIEPNHAN", 9, "Mã viện phí", 95);
                    ucGridBenhNhan.setColumn("MABENHNHAN", 10, "Mã bệnh nhân", 90);
                    ucGridBenhNhan.setColumn("NGAYTIEPNHAN", 11, "Vào viện", 140);
                    ucGridBenhNhan.setColumn("NGAY_RAVIEN", 12, "Ra viện", 140);
                    #endregion
                }
                else if ("2".Equals(vpiConfig["HIS_TIMKIEM_VIENPHI"].ToString()))
                {
                    #region 2
                    ucGridBenhNhan.setColumnAll(false);
                    ucGridBenhNhan.setColumn("ICON1", 0, " ", 25);
                    ucGridBenhNhan.setColumn("ICON2", 1, " ", 25);
                    ucGridBenhNhan.setColumn("MATIEPNHAN", 2, "Mã viện phí", 85);
                    ucGridBenhNhan.setColumnMemoEdit("TENBENHNHAN", 3, "Họ tên", 140);
                    ucGridBenhNhan.setColumnMemoEdit("DIACHI", 4, "Địa chỉ", 220);
                    ucGridBenhNhan.setColumn("NGAYSINH", 5, "Ngày sinh", 80);
                    ucGridBenhNhan.setColumn("GIOITINH", 6, "GT", 40);
                    ucGridBenhNhan.setColumn("MA_BHYT", 7, "Mã BHYT", 120);
                    ucGridBenhNhan.setColumnMemoEdit("KHOA", 8, "Khoa", 130);
                    ucGridBenhNhan.setColumn("MAHOSOBENHAN", 9, "Mã bệnh án", 90);
                    ucGridBenhNhan.setColumn("MABENHNHAN", 10, "Mã bệnh nhân", 90);
                    ucGridBenhNhan.setColumn("NGAYTIEPNHAN", 11, "Vào viện", 140);
                    ucGridBenhNhan.setColumn("NGAY_RAVIEN", 12, "Ra viện", 140);
                    #endregion
                }
                else if ("3".Equals(vpiConfig["HIS_TIMKIEM_VIENPHI"].ToString()))
                {
                    #region 3
                    ucGridBenhNhan.setColumnAll(false);
                    ucGridBenhNhan.setColumn("ICON1", 0, " ", 25);
                    ucGridBenhNhan.setColumn("ICON2", 1, " ", 25);
                    ucGridBenhNhan.setColumn("MAHOSOBENHAN", 2, "Mã bệnh án", 90);
                    ucGridBenhNhan.setColumn("MABENHNHAN", 3, "Mã bệnh nhân", 90);
                    ucGridBenhNhan.setColumnMemoEdit("TENBENHNHAN", 5, "Họ tên", 140);
                    ucGridBenhNhan.setColumnMemoEdit("DIACHI", 6, "Địa chỉ", 220);
                    ucGridBenhNhan.setColumn("NGAYSINH", 7, "Ngày sinh", 80);
                    ucGridBenhNhan.setColumn("GIOITINH", 8, "GT", 40);
                    ucGridBenhNhan.setColumn("MA_BHYT", 9, "Mã BHYT", 120);
                    ucGridBenhNhan.setColumnMemoEdit("NGUOIDUYET", 4, "Người duyệt", 175);// - Ngày duyệt KT
                    ucGridBenhNhan.setColumn("MATIEPNHAN", 10, "Mã viện phí", 90);
                    ucGridBenhNhan.setColumnMemoEdit("KHOA", 11, "Khoa", 130);
                    ucGridBenhNhan.setColumn("NGAYTIEPNHAN", 12, "Vào viện", 140);
                    ucGridBenhNhan.setColumn("NGAY_RAVIEN", 13, "Ra viện", 140);
                    #endregion
                }
                #endregion

                ucGridBenhNhan.setColumnImage("ICON1", new String[] { "3", "4" }
                            , new String[] { "./Resources/Flag_Red_New.png", "./Resources/True.png" });

                ucGridBenhNhan.setColumnImage("ICON2", new String[] { "2" }
                   , new String[] { "./Resources/Circle_Green.png" });

                //flagLoading = false;
                if (dtBenhNhan.Rows.Count <= 0)
                {
                    this.ClearThongTinThuTien();
                    this.SetEnabled(null, new List<SimpleButton>() { btnThem });
                    this.drvBenhNhan = null;
                    this.drvDichVu = null;
                    this._benhNhan = null;
                    this.tiepNhanId = "-1";
                }
                else
                {
                    ucGridBenhNhan.gridView.SelectRow(0);
                    GridBenhNhan_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                //flagLoading = false;
                log.Fatal(ex.ToString());
            }


            if (this.drvBenhNhan == null)
            {
                this.drvBenhNhan = (DataRowView)ucGridBenhNhan.gridView.GetRow(0);
                if (this.drvBenhNhan != null)
                {
                    LoadThongTinPhieuThu(this.drvBenhNhan);
                }
            }
             
        }

        /// <summary>
        /// Load danh sách dịch vụ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="phieuThuId"></param>
        private void LoadDataGridDichVu(int? page, string phieuThuId = "", string loaiPhieuThu = "")
        {
            try
            {
                int pageNum = 0;
                if (page == null)
                {
                    pageNum = ucGridDichVu.ucPage1.Current();
                }
                else
                {
                    pageNum = page.GetValueOrDefault();
                }

                if (pageNum <= 0)
                {
                    pageNum = 1;
                }

                ResponsList responses = new ResponsList();

                if (string.IsNullOrEmpty(phieuThuId))
                {
                    responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                        "VPI01T001.21"
                        , pageNum
                        , 999999
                        , new String[] { "[0]", "[1]" }
                        , new string[] { this.tiepNhanId, "0" }
                        , "");
                }
                else
                {
                    responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                        "VPI01T001.22"
                        , pageNum
                        , 999999
                        , new String[] { "[0]" }
                        , new string[] { phieuThuId }
                        , "");
                }


                DataTable dtDichVu = new DataTable();
                dtDichVu = MyJsonConvert.toDataTable(responses.rows);
                if (dtDichVu.Rows.Count <= 0)
                {
                    dtDichVu = Func.getTableEmpty(
                        new String[] {
                                "DICHVUKHAMBENHID"
                                ,"TIEPNHANID"
                                , "KHOA"
                                , "KHOAID"
                                , "PHONGID"
                                , "KHAMBENHID"
                                , "BHYT_DV"
                                , "BHYT_BNTT"
                                , "LOAI_DOITUONG"
                                , "MAUBENHPHAMID"
                                , "LOAINHOMMAUBENHPHAM"
                                , "KHOANMUCID"
                                , "MAKHOANMUC"
                                , "TENKHOANMUC"
                                , "DOITUONGBENHNHANID"
                                , "NHOMTHANHTOAN"
                                , "DOITUONGDV"
                                , "DOITUONG"
                                , "NHOM_MABHYT"
                                , "DATHUTIEN"
                                , "TENDICHVU"
                                , "SOLUONG"
                                , "SOLUONG_OLD"
                                , "TIENDICHVU"
                                , "THANHTIEN"
                                , "TIEN_BHYT_TRA"
                                , "THUCTHU"
                                , "TYLE_BHYT_TRA"
                                , "TIEN_MIENGIAM"
                                , "TYLE_DV"
                                , "VERSION_OLD"
                                , "TYLE"
                                , "TYLE_MIENGIAM"
                                , "VATTU04"
                        });
                }
                else
                {
                    if (!dtDichVu.Columns.Contains("TYLE_BHYT_TRA"))
                    {
                        dtDichVu.Columns.Add("TYLE_BHYT_TRA");
                    }

                    if (!dtDichVu.Columns.Contains("TYLE_MIENGIAM"))
                    {
                        dtDichVu.Columns.Add("TYLE_MIENGIAM");
                    }
                }

                for (int i = 0; i < dtDichVu.Rows.Count; i++)
                {
                    dtDichVu.Rows[i]["TIENDICHVU"] = FormatMoney(dtDichVu.Rows[i]["TIENDICHVU"].ToString());
                    dtDichVu.Rows[i]["TIEN_BHYT_TRA"] = FormatMoney(dtDichVu.Rows[i]["TIEN_BHYT_TRA"].ToString());
                    dtDichVu.Rows[i]["THUCTHU"] = FormatMoney(dtDichVu.Rows[i]["THUCTHU"].ToString());
                    dtDichVu.Rows[i]["TIEN_MIENGIAM"] = FormatMoney(dtDichVu.Rows[i]["TIEN_MIENGIAM"].ToString());
                }

                ucGridDichVu.setData(dtDichVu, responses.total, responses.page, responses.records);

                ucGridDichVu.setColumnAll(false);
                ucGridDichVu.setColumnMemoEdit("TENDICHVU", 0, "Tên dịch vụ", 180);
                ucGridDichVu.setColumn("SOLUONG", 1, "SL", 35);
                ucGridDichVu.setColumn("TIENDICHVU", 2, "Giá tiền", 85);
                ucGridDichVu.setColumn("TIEN_BHYT_TRA", 3, "BHYT trả", 85);
                ucGridDichVu.setColumn("THUCTHU", 4, "BN trả", 85);
                ucGridDichVu.setColumn("TYLE_BHYT_TRA", 5, "TL %", 50);
                ucGridDichVu.setColumn("TYLE_MIENGIAM", 6, "% mg", 50);
                ucGridDichVu.setColumn("TIEN_MIENGIAM", 7, "Miễn giảm", 85);
                ucGridDichVu.setColumn("DOITUONG", 8, "", 90);
                ucGridDichVu.setColumn("NHOM_MABHYT", 9, "", 90);

                ucGridDichVu.gridView.Columns["TENDICHVU"].OptionsColumn.AllowEdit = false;
                ucGridDichVu.gridView.Columns["SOLUONG"].OptionsColumn.AllowEdit = false;

                if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("TIENDICHVU")))
                {
                    ucGridDichVu.gridView.Columns["TIENDICHVU"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    ucGridDichVu.gridView.Columns["TIENDICHVU"].OptionsColumn.AllowEdit = false;
                }

                if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("TIEN_BHYT_TRA")))
                {
                    ucGridDichVu.gridView.Columns["TIEN_BHYT_TRA"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    ucGridDichVu.gridView.Columns["TIEN_BHYT_TRA"].OptionsColumn.AllowEdit = false;
                }

                if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("THUCTHU")))
                {
                    ucGridDichVu.gridView.Columns["THUCTHU"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    ucGridDichVu.gridView.Columns["THUCTHU"].OptionsColumn.AllowEdit = false;
                }

                if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("TYLE_BHYT_TRA")))
                {
                    ucGridDichVu.gridView.Columns["TYLE_BHYT_TRA"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    ucGridDichVu.gridView.Columns["TYLE_BHYT_TRA"].OptionsColumn.AllowEdit = false;
                }

                if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("TYLE_MIENGIAM")))
                {
                    ucGridDichVu.gridView.Columns["TYLE_MIENGIAM"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                }

                if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("TIEN_MIENGIAM")))
                {
                    ucGridDichVu.gridView.Columns["TIEN_MIENGIAM"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    ucGridDichVu.gridView.Columns["TIEN_MIENGIAM"].OptionsColumn.AllowEdit = false;
                }

                ucGridDichVu.gridView.Columns["DOITUONG"].GroupIndex = 0;
                ucGridDichVu.gridView.Columns["NHOM_MABHYT"].GroupIndex = 1;
                ucGridDichVu.gridView.ExpandAllGroups();
                ucGridDichVu.setMultiSelectMode(false);


                LoadDataGridDichVu_continue(dtDichVu, loaiPhieuThu);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        private void LoadDataGridDichVu_continue(DataTable dtDichVu, string loaiPhieuThu)
        {
            try
            {


                if (this.flagTinhTongTien)
                {
                    this.TinhTongTien(dtDichVu);
                }

                this.chot = true;
                if (!flagLoading && _benhNhan != null)
                {
                    ucGridDichVu.gridView.Columns["SOLUONG"].OptionsColumn.AllowEdit = true;
                    if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("TYLE_MIENGIAM")))
                    {
                        ucGridDichVu.gridView.Columns["TYLE_MIENGIAM"].OptionsColumn.AllowEdit = true;
                    }

                    if (!"1".Equals(_benhNhan["TRANGTHAITIEPNHAN_VP"].ToString()) && !"2".Equals(_benhNhan["TRANGTHAITIEPNHAN"].ToString()))
                    {
                        // gắn menu vô
                        ucGridDichVu.MenuPopup_Visible_Child_byTitle(false, tXoaDichVu);
                        ucGridDichVu.MenuPopup_Visible_Parent_byTitle(false, tDichVu);
                    }

                    dtDVSai = Func.getTableEmpty(
                        new String[] {
                                "DICHVUKHAMBENHID"
                                ,"TIEPNHANID"
                                , "KHOA"
                                , "KHOAID"
                                , "PHONGID"
                                , "KHAMBENHID"
                                , "BHYT_DV"
                                , "BHYT_BNTT"
                                , "LOAI_DOITUONG"
                                , "MAUBENHPHAMID"
                                , "LOAINHOMMAUBENHPHAM"
                                , "KHOANMUCID"
                                , "MAKHOANMUC"
                                , "TENKHOANMUC"
                                , "DOITUONGBENHNHANID"
                                , "NHOMTHANHTOAN"
                                , "DOITUONGDV"
                                , "DOITUONG"
                                , "NHOM_MABHYT"
                                , "DATHUTIEN"
                                , "TENDICHVU"
                                , "SOLUONG"
                                , "SOLUONG_OLD"
                                , "TIENDICHVU"
                                , "THANHTIEN"
                                , "TIEN_BHYT_TRA"
                                , "THUCTHU"
                                , "TYLE_BHYT_TRA"
                                , "TIEN_MIENGIAM"
                                , "TYLE_DV"
                                , "VERSION_OLD"
                                , "TYLE"
                                , "TYLE_MIENGIAM"
                                , "VATTU04"
                        });

                    string khoa = "";
                    for (int i = 0; i < dtDichVu.Rows.Count; i++)
                    {
                        if (("1".Equals(dtDichVu.Rows[i]["LOAIDOITUONG"].ToString()) || "2".Equals(dtDichVu.Rows[i]["LOAIDOITUONG"].ToString()) || "3".Equals(dtDichVu.Rows[i]["LOAIDOITUONG"].ToString()))
                                && !"16".Equals(GetValueByName(dtDichVu.Rows[i], "LOAINHOMMAUBENHPHAM"))
                                && !GetValueByName(dtDichVu.Rows[i], "TYLE").Equals(GetValueByName(dtDichVu.Rows[i], "TYLE_BHYT_TRA"))
                                && !"0".Equals(GetValueByName(dtDichVu.Rows[i], "TYLE_DV"))
                                && !"0".Equals(dtDichVu.Rows[i]["SOLUONG"].ToString())
                                && (!"3".Equals(GetValueByName(dtDichVu.Rows[i], "LOAINHOMMAUBENHPHAM")) || !"0".Equals(dtDichVu.Rows[i]["TIENDICHVU"].ToString()))
                                && "0".Equals(dtDichVu.Rows[i]["VATTU04"].ToString()))
                        {
                            dtDVSai.ImportRow(dtDichVu.Rows[i]);

                            khoa = dtDichVu.Rows[i]["KHOA"].ToString();
                            if (!listKhoa.Any(p => p.Equals(khoa)) && !string.IsNullOrEmpty(khoa))
                            {
                                listKhoa.Add(khoa);
                            }
                        }
                    }

                    this.SetEnabled(new List<SimpleButton>() { btnThem }, null);
                }
                else
                {
                    ucGridDichVu.setMultiSelectMode(true);
                    ucGridDichVu.gridView.ClearSelection();
                    ucGridDichVu.gridView.Columns["SOLUONG"].OptionsColumn.AllowEdit = false;
                    if (ucGridDichVu.gridView.Columns.Any(o => o.FieldName.Equals("TYLE_MIENGIAM")))
                    {
                        ucGridDichVu.gridView.Columns["TYLE_MIENGIAM"].OptionsColumn.AllowEdit = false;
                    }
                    this.SetTTThuTien(null);
                    this.KiemTraBenhNhan();
                    ucCboLoaiTT.SelectValue = "1";
                    ucCboLoaiPhieu.SelectValue = "1";
                    teMienGiam.Text = "0";

                    if (!string.IsNullOrEmpty(loaiPhieuThu))
                    {

                    }
                    else if ("2".Equals(this.vpiConfig["VPI_QUYTRINH_VIENPHI"].ToString())
                        && "0".Equals(this._benhNhan["TRANGTHAITIEPNHAN"].ToString()))
                    {
                        loaiPhieuThu = _TAMUNG;
                    }
                    else if ("0".Equals(this._benhNhan["TRANGTHAITIEPNHAN"].ToString())
                        && ("0".Equals(GetValueByName(this.drVpData, "NOPTHEM")) || "0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID"))))
                    {
                        loaiPhieuThu = _TAMUNG;
                    }
                    else if ("1".Equals(this.vpiConfig["VPI_QUYTRINH_VIENPHI"].ToString())
                        && !"0".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN"))
                        && (Func.Parse_double(GetValueByName(this.drVpData, "TAMUNG")) - Func.Parse_double(GetValueByName(this.drVpData, "HOANUNG")) > 0))
                    {
                        loaiPhieuThu = _HOANUNG;
                    }
                    else if (Func.Parse_double(GetValueByName(this.drVpData, "NOPTHEM")) >= 0)
                    {
                        loaiPhieuThu = _THUTHEM;
                    }
                    else
                    {
                        loaiPhieuThu = _HOANUNG;
                    }

                    ucCboLoaiPhieu.SelectValue = loaiPhieuThu;

                    var nhomTTPT = "0";
                    arr_DV_CT = Func.getTableEmpty(
                        new String[] {
                                "DICHVUKHAMBENHID"
                                ,"TIEPNHANID"
                                , "KHOA"
                                , "KHOAID"
                                , "PHONGID"
                                , "KHAMBENHID"
                                , "BHYT_DV"
                                , "BHYT_BNTT"
                                , "LOAI_DOITUONG"
                                , "MAUBENHPHAMID"
                                , "LOAINHOMMAUBENHPHAM"
                                , "KHOANMUCID"
                                , "MAKHOANMUC"
                                , "TENKHOANMUC"
                                , "DOITUONGBENHNHANID"
                                , "NHOMTHANHTOAN"
                                , "DOITUONGDV"
                                , "DOITUONG"
                                , "NHOM_MABHYT"
                                , "DATHUTIEN"
                                , "TENDICHVU"
                                , "SOLUONG"
                                , "SOLUONG_OLD"
                                , "TIENDICHVU"
                                , "THANHTIEN"
                                , "TIEN_BHYT_TRA"
                                , "THUCTHU"
                                , "TYLE_BHYT_TRA"
                                , "TIEN_MIENGIAM"
                                , "TYLE_DV"
                                , "VERSION_OLD"
                                , "TYLE"
                                , "TYLE_MIENGIAM"
                                , "VATTU04"
                        });

                    for (int i = dtDichVu.Rows.Count - 1; i >= 0; i--)
                    {
                        var daThuTien = GetValueByName(dtDichVu.Rows[i], "DATHUTIEN");
                        var nhomThanhToan = GetValueByName(dtDichVu.Rows[i], "NHOMTHANHTOAN");
                        var doiTuong_DV = GetValueByName(dtDichVu.Rows[i], "DOITUONGDV");
                        var doiTuongDV = ucCboNhomDV.SelectValue;
                        var thucThu = GetValueByName(dtDichVu.Rows[i], "THUCTHU");

                        if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_QUYTRINH_VIENPHI")) && "2".Equals(loaiPhieuThu))
                        {
                            ucGridDichVu.gridView.DeleteRow(i);
                        }
                        else if ("2".Equals(GetValueByName(this.vpiConfig, "VPI_QUYTRINH_VIENPHI"))
                            && "0".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN"))
                            && !"7".Equals(nhomThanhToan))
                        {
                            ucGridDichVu.gridView.DeleteRow(i);
                        }
                        else if ("3".Equals(loaiPhieuThu))
                        {
                            ucGridDichVu.gridView.DeleteRow(i);
                        }
                        else if ("8".Equals(loaiPhieuThu))
                        {
                            if (!"3".Equals(daThuTien) || "0".Equals(thucThu))
                            {
                                ucGridDichVu.gridView.DeleteRow(i);
                            }
                        }
                        else if ("3".Equals(daThuTien) || "0".Equals(thucThu))
                        {
                            ucGridDichVu.gridView.DeleteRow(i);
                        }
                        else if ((!"4".Equals(doiTuongDV) && !"0".Equals(doiTuongDV) && !doiTuongDV.Equals(doiTuong_DV))
                            || ("4".Equals(doiTuongDV) && !"1".Equals(doiTuong_DV) && !"2".Equals(doiTuong_DV)))
                        {
                            arr_DV_CT.ImportRow(dtDichVu.Rows[i]);
                            ucGridDichVu.gridView.DeleteRow(i);
                        }
                        else
                        {
                            arr_DV_CT.ImportRow(dtDichVu.Rows[i]);
                            var arrRow = ucGridDichVu.gridView.GetSelectedRows();
                            if (_DT_VIENPHI.Equals(doiTuongDV)
                                && !"0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID"))
                                && arrRow.Count() >= Func.Parse_double(_SL_VIENPHI))
                            {

                            }
                            else if (_DT_DICHVU.Equals(doiTuongDV) && arrRow.Count() >= Func.Parse_double(_SL_DICHVU))
                            {

                            }
                            else if ("1".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN"))
                                || "2".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN")))
                            {
                                ucGridDichVu.gridView.UnselectRow(i);
                            }
                            else
                            {
                                if ("1".Equals(doiTuong_DV)
                                    && ("0".Equals(this.vpiConfig["VP_THU_BHYT_NGOAITRU_CHUADBA"].ToString())
                                    || !"1".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID"))))
                                {
                                    ucGridDichVu.gridView.DeleteRow(i);
                                }
                                else
                                {
                                    if ("0".Equals(this.vpiConfig["VP_TACH_HOADON"].ToString())
                                        || "1".Equals(this.vpiConfig["VPI_TUDONGTACH_HOADON"].ToString()))
                                    {
                                        ucGridDichVu.gridView.SelectRow(i);
                                    }
                                    else
                                    {
                                        if (nhomThanhToan.Equals(nhomTTPT) || "0".Equals(nhomTTPT))
                                        {
                                            nhomTTPT = nhomThanhToan;
                                            ucGridDichVu.gridView.SelectRow(i);
                                        }
                                        else
                                        {
                                            ucGridDichVu.gridView.DeleteRow(i);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (Func.Parse_double(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN")) >= 1)
                    {
                        var arrRow = ucGridDichVu.gridView.GetSelectedRows();
                        var dvSize = arr_DV_CT.Rows.Count;
                        var selDVSize = arrRow.Count();
                        if (dvSize == selDVSize)
                        {
                            this.chot = true;
                        }
                        else
                        {
                            this.chot = false;
                        }

                        var loaiPhieuThuId = ucCboLoaiPhieu.SelectValue;
                        if (!"1".Equals(this.vpiConfig["VPI_QUYTRINH_VIENPHI"].ToString())
                            && "2".Equals(loaiPhieuThuId)
                            && dvSize != selDVSize)
                        {
                            ucCboLoaiPhieu.SelectValue = "6";
                        }

                        if (!"1".Equals(this.vpiConfig["VPI_QUYTRINH_VIENPHI"].ToString())
                            && "6".Equals(loaiPhieuThuId)
                            && dvSize == selDVSize
                            && Func.Parse_double(GetValueByName(this.drVpData, "NOPTHEM")) < 0)
                        {
                            ucCboLoaiPhieu.SelectValue = "2";
                        }
                    }

                    this.LoadDV();
                    ucCboLoaiTT.lookUpEdit.ReadOnly = false;
                    deNgay.Properties.ReadOnly = false;
                    ucCboLoaiPhieu.lookUpEdit.ReadOnly = false;
                    this.SetEnabled(null, new List<SimpleButton>() { btnIn });
                    if (!_TAMUNG.Equals(loaiPhieuThu))
                    {
                        ucCboMaSo.lookUpEdit.ReadOnly = false;
                    }


                    if (_THUTIEN.Equals(loaiPhieuThu))
                    {
                        teMaPhieu.ReadOnly = false;
                    }

                    if (_TAMUNG.Equals(loaiPhieuThu))
                    {
                        teMaPhieu.ReadOnly = true;
                        ucCboMaSo.lookUpEdit.ReadOnly = true;
                        teSoTien.ReadOnly = false;
                        teMienGiam.ReadOnly = true;
                        teLyDo.ReadOnly = true;
                        teThucThu.ReadOnly = true;
                        ucCboLoaiTT.lookUpEdit.ReadOnly = true;
                    }

                    layoutControlItem27.Text = "Thực thu";
                    if (_HOANUNG.Equals(loaiPhieuThu))
                    {
                        teMaPhieu.ReadOnly = false;
                        teSoTien.ReadOnly = true;
                        layoutControlItem27.Text = "Hoàn tiền";
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }


        }

        private bool coDV = false;
        private double tienHoaDon = 0;
        private string loaiPhieuThu = "";
        //private DataTable dtObjData;
        private bool hetPhieu2 = false;
        private bool hetPhieu = false;
        private List<string> dsNhomTT = new List<string>();
        private Dictionary<int, List<int>> arrHoaDon = new Dictionary<int, List<int>>();
        private double _loaidt = 0;
        private string _phieutamungid = string.Empty;
        private Dictionary<string, string> objData = new Dictionary<string, string>();
        private PhieuThuObj2 obj = new PhieuThuObj2();
        private PhieuThuObj2 obj_2 = new PhieuThuObj2();

        private void AddOb(Dictionary<string, string> list, string key, string value)
        {
            if (list.ContainsKey(key))
            {
                list[key] = value;
            }
            else
            {
                list.Add(key, value);
            }
        }

        private void LoadDV()
        {
            phieuInfo = new List<PhieuThuObj2>();
            objData = new Dictionary<string, string>();
            obj = new PhieuThuObj2();
            obj_2 = new PhieuThuObj2();
            dsNhomTT = new List<string>();
            arrHoaDon = new Dictionary<int, List<int>>();

            tienHoaDon = 0;
            loaiPhieuThu = ucCboLoaiPhieu.SelectValue;
            var loaiTT = ucCboLoaiTT.SelectValue;
            var loaiSo = "1";
            var _loai_dichvu = "0";
            coDV = false;
            teMaPhieu.Properties.ReadOnly = true;

            AddOb(objData, "TIEPNHANID", GetValueByName(this._benhNhan, "TIEPNHANID"));

            if (_TAMUNG.Equals(loaiPhieuThu))
            {
                ucCboMaSo.lookUpEdit.ReadOnly = true;
                hetPhieu2 = false;
            }
            else
            {
                if ("1".Equals(this.vpiConfig["VP_TACH_HOADON"].ToString()))
                {
                    ucCboMaSo.lookUpEdit.ReadOnly = true;
                }
                else
                {
                    ucCboMaSo.lookUpEdit.ReadOnly = false;
                }
                var selRowIds = ucGridDichVu.gridView.GetSelectedRows();
                //dsNhomTT = new List<string>();
                //arrHoaDon = new Dictionary<int, List<int>>();
                if ("0".Equals(this.vpiConfig["VP_TACH_HOADON"].ToString()))
                {
                    dsNhomTT.Add("0");
                    arrHoaDon.Add(arrHoaDon.Count, selRowIds.ToList());
                }
                else
                {
                    for (int k = 0; k < selRowIds.Count(); k++)
                    {
                        var _nhomThanhToan = ((DataRowView)ucGridDichVu.gridView.GetRow(selRowIds[k]))["NHOMTHANHTOAN"].ToString();
                        if (!dsNhomTT.Any(o => o.Equals(_nhomThanhToan)))
                        {
                            dsNhomTT.Add(_nhomThanhToan);
                        }
                    }

                    for (int q = 0; q < dsNhomTT.Count; q++)
                    {
                        var count = 0;
                        var _hoaDon = new List<int>();
                        for (int p = 0; p < selRowIds.Count(); p++)
                        {
                            var _nhom_TT = ((DataRowView)ucGridDichVu.gridView.GetRow(selRowIds[p]))["NHOMTHANHTOAN"].ToString();
                            if ((count >= Func.Parse_double(_SL_DICHVU) && _nhom_TT.Equals(_DT_DICHVU))
                                || (!"0".Equals(this._benhNhan["LOAITIEPNHANID"].ToString()) && count >= Func.Parse_double(_SL_VIENPHI) && _nhom_TT.Equals(_DT_VIENPHI)))
                            {
                                arrHoaDon.Add(arrHoaDon.Count, _hoaDon);
                                count = 0;
                                _hoaDon = new List<int>();
                            }
                            if (_nhom_TT.Equals(dsNhomTT[q]))
                            {
                                _hoaDon.Add(selRowIds[p]);
                                count++;
                            }
                        }

                        arrHoaDon.Add(arrHoaDon.Count, _hoaDon);
                    }
                }

                for (var j = 0; j < arrHoaDon.Count; j++)
                {
                    double _tongtien = 0;
                    double _thucthu = 0;
                    double _miengiamdv = 0;
                    double _bhyt_tra = 0;
                    var _DSDV = new List<PhieuThuDichVu>();

                    var _ds_maubpid = new List<string>();
                    var _ds_dtdvid = new List<string>();
                    var _ds_dvkbid = new List<string>();
                    var _hoa_don = arrHoaDon[j];
                    for (var i = 0; i < _hoa_don.Count; i++)
                    {
                        if (_hoa_don[i] < 0)
                        {
                            continue;
                        }

                        var _dvRow = (DataRowView)ucGridDichVu.gridView.GetRow(_hoa_don[i]);
                        if (_dvRow != null && !ucGridDichVu.gridView.IsGroupRow(_hoa_don[i]))
                        {
                            PhieuThuDichVu _objRow = new PhieuThuDichVu();
                            _ds_dvkbid.Add(_dvRow["DICHVUKHAMBENHID"].ToString());

                            _objRow.THUCTHU = Func.Parse_double(_dvRow["THUCTHU"].ToString()).ToString();
                            _objRow.TIEN_BHYT_TRA = Func.Parse_double(_dvRow["TIEN_BHYT_TRA"].ToString()).ToString();
                            _objRow.TIEN_MIENGIAM = Func.Parse_double(_dvRow["TIEN_MIENGIAM"].ToString()).ToString();
                            _objRow.DICHVUKHAMBENHID = _dvRow["DICHVUKHAMBENHID"].ToString();
                            _objRow.KHOANMUCID = _dvRow["KHOANMUCID"].ToString();
                            _objRow.TENKHOANMUC = _dvRow["TENKHOANMUC"].ToString();
                            _objRow.MAKHOANMUC = _dvRow["MAKHOANMUC"].ToString();
                            _objRow.VERSION_OLD = _dvRow["VERSION_OLD"].ToString();
                            _DSDV.Add(_objRow);

                            _tongtien += Func.Parse_double(_dvRow["TIENDICHVU"].ToString()) * Func.Parse_double(_dvRow["SOLUONG"].ToString());
                            _bhyt_tra += Func.Parse_double(_dvRow["TIEN_BHYT_TRA"].ToString());
                            _thucthu += Func.Parse_double(_dvRow["THUCTHU"].ToString());
                            _miengiamdv += Func.Parse_double(_dvRow["TIEN_MIENGIAM"].ToString());
                            var _mbpid = Func.Parse_double(_dvRow["MAUBENHPHAMID"].ToString());
                            var _loaidtdv = Func.Parse_double(_dvRow["LOAIDOITUONG"].ToString());
                            _loaidt = _loaidtdv;
                            _loai_dichvu = _dvRow["NHOMTHANHTOAN"].ToString();
                            if (!_ds_maubpid.Any(o => o.Equals(_mbpid.ToString())))
                            {
                                _ds_maubpid.Add(_mbpid.ToString());
                            }
                            if (!_ds_dtdvid.Any(o => o.Equals(_loaidtdv.ToString())))
                            {
                                _ds_dtdvid.Add(_loaidtdv.ToString());
                            }
                        }
                    }

                    var _strds_dvkbid = string.Join(",", _ds_dvkbid);
                    var _ds_maubpid_str = string.Join(",", _ds_maubpid);
                    var _ds_dtdvid_str = string.Join(",", _ds_dtdvid);

                    AddOb(objData, "LOAIDOITUONG", _loaidt.ToString());
                    AddOb(objData, "DS_MAUBENHPHAMID", _ds_maubpid_str);
                    AddOb(objData, "DANHSACHDOITUONGDICHVU", _ds_dtdvid_str);
                    AddOb(objData, "TONGTIEN", _thucthu.ToString());
                    AddOb(objData, "THUCTHU", _thucthu.ToString());
                    AddOb(objData, "TIEN_BHYT_TRA", _bhyt_tra.ToString());
                    AddOb(objData, "MIENGIAMDV", _miengiamdv.ToString());
                    AddOb(objData, "DOITUONGBENHNHANID", this._benhNhan["DOITUONGBENHNHANID"].ToString());
                    AddOb(objData, "DSDVKBID", "");

                    if (_DSDV.Count > 0 && _thucthu != 0)
                    {
                        if (loaiPhieuThu.Equals(_HOANDICHVU))
                        {
                            tienHoaDon += _thucthu;
                            AddOb(objData, "DSDVKBID", string.Join(",", _ds_dvkbid));
                            obj.TONGTIEN = _thucthu.ToString();
                            obj.THUCTHU = _thucthu.ToString();

                            hetPhieu2 = false;
                            coDV = false;
                        }
                        else
                        {
                            if ("0".Equals(this.vpiConfig["VP_TACH_HOADON"].ToString()))
                            {
                                if ("1".Equals(this._benhNhan["DOITUONGBENHNHANID"].ToString()))
                                {
                                    loaiSo = "2";
                                }
                                else
                                {
                                    loaiSo = "3";
                                }
                            }
                            else
                            {
                                if ("1".Equals(_loai_dichvu))
                                {
                                    loaiSo = "2";
                                }
                                else if ("2".Equals(_loai_dichvu))
                                {
                                    loaiSo = "3";
                                }
                                else if ("3".Equals(_loai_dichvu))
                                {
                                    loaiSo = "4";
                                }
                                else if ("4".Equals(_loai_dichvu))
                                {
                                    loaiSo = "1";
                                }
                                else if ("5".Equals(_loai_dichvu))
                                {
                                    loaiSo = "4";
                                }
                                else if ("6".Equals(_loai_dichvu))
                                {
                                    loaiSo = "1";
                                }
                                else if ("7".Equals(_loai_dichvu))
                                {
                                    loaiSo = "4";
                                }
                            }
                            //var _sql_par_2 = [];
                            var _kieuthu_2 = _THUTIEN;
                            //_sql_par_2.push({ name: "[0]", value: _loaiso});
                            //_sql_par_2.push({ name: "[1]", value: (_kieuthu_2)});
                            //_sql_par_2.push({ name: "[2]", value: _loaitt});
                            //_sql_par_2.push({ name: "[3]", value: _phong_id});
                            var _input_2 = loaiSo + "$" + _kieuthu_2 + "$" + loaiTT + "$" + Const.local_phongId;
                            var _result_2 = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.13", _input_2, 0);
                            _dsSo_2 = _result_2;
                            if (_dsSo_2.Rows.Count == 0)
                            {
                                hetPhieu2 = true;
                                obj_2.MAPHIEUTHU = string.Empty;
                                obj_2.NHOMPHIEUTHUID = string.Empty;
                                obj_2.MANHOMPHIEUTHU = string.Empty;
                                obj_2.LOAIPHIEUTHU = string.Empty;
                                break;
                            }
                            else
                            {
                                ucCboMaSo.SelectValue = null;
                                var dtMaSo = Func.getTableEmpty(new string[] { "col1", "col2" });
                                for (int i = 0; i < _dsSo_2.Rows.Count; i++)
                                {
                                    var drMaSo = dtMaSo.NewRow();
                                    drMaSo["col1"] = i.ToString();
                                    drMaSo["col2"] = _dsSo_2.Rows[i]["MANHOMPHIEUTHU"].ToString();
                                    dtMaSo.Rows.Add(drMaSo);
                                }

                                ucCboMaSo.setData(dtMaSo, "col1", "col2");
                                ucCboMaSo.setColumn(0, false);
                                ucCboMaSo.SelectValue = "0";
                                hetPhieu2 = false;

                                obj_2.MAPHIEUTHU = _dsSo_2.Rows[0]["MAPHIEUTHU"].ToString();
                                obj_2.SOPHIEUTO = _dsSo_2.Rows[0]["SOPHIEUTO"].ToString();
                                obj_2.KHOASOPHIEUTU = _dsSo_2.Rows[0]["KHOASOPHIEUTU"].ToString();
                                obj_2.SOPHIEUFROM = _dsSo_2.Rows[0]["SOPHIEUFROM"].ToString();
                                obj_2.NHOMPHIEUTHUID = _dsSo_2.Rows[0]["NHOMPHIEUTHUID"].ToString();
                                obj_2.MANHOMPHIEUTHU = _dsSo_2.Rows[0]["MANHOMPHIEUTHU"].ToString();
                                obj_2.LOAIPHIEUTHU = _dsSo_2.Rows[0]["LOAIPHIEUTHU"].ToString();
                                obj_2.LOAIPHIEUTHUID = _THUTIEN.ToString();
                                obj_2.LOAIDOITUONG = _loaidt.ToString();
                                obj_2.DS_MAUBENHPHAMID = _ds_maubpid_str;
                                obj_2.DANHSACHDOITUONGDICHVU = _ds_dtdvid_str;
                                obj_2.TONGTIEN = _thucthu.ToString();
                                obj_2.THUCTHU = _thucthu.ToString();
                                obj_2.TIEN_BHYT_TRA = _bhyt_tra.ToString();
                                obj_2.MIENGIAMDV = _miengiamdv.ToString();
                                obj_2.NHOMTHANHTOAN = _loai_dichvu.ToString();

                                tienHoaDon += _thucthu;

                                string strValue = "";
                                var arr = new List<string>();
                                var _PTCT = new List<PhieuThuCT>();
                                for (var y = 0; y < _DSDV.Count; y++)
                                {
                                    strValue = _DSDV[y].KHOANMUCID;

                                    if (!arr.Any(o => o.Equals(strValue)))
                                    {
                                        arr.Add(strValue);
                                    }
                                }

                                for (var m = 0; m < arr.Count; m++)
                                {
                                    var _km = new PhieuThuCT();
                                    double tongTien = 0;
                                    for (var n = 0; n < _DSDV.Count; n++)
                                    {
                                        strValue = _DSDV[n].KHOANMUCID;

                                        if (strValue.Equals(arr[m]))
                                        {
                                            _km.KHOANMUCID = strValue;

                                            _km.TENKHOANMUC = _DSDV[n].TENKHOANMUC;
                                            _km.MAKHOANMUC = _DSDV[n].MAKHOANMUC;
                                            tongTien += Func.Parse_double(_DSDV[n].THUCTHU);
                                        }
                                    }
                                    _km.TONGTIEN = tongTien.ToString();

                                    _PTCT.Add(_km);
                                }

                                List<string> listNoiDungThu = new List<string>();
                                for (var x = 0; x < _PTCT.Count; x++)
                                {
                                    listNoiDungThu.Add(_PTCT[x].TENKHOANMUC + ": " + _PTCT[x].TONGTIEN);
                                }

                                var _noidungthu = string.Join(", ", listNoiDungThu);


                                if ("1".Equals(this.vpiConfig["VP_TACH_HOADON"].ToString())
                                                && ("1".Equals(objData["LOAIDOITUONG"].ToString())
                                                || "2".Equals(objData["LOAIDOITUONG"].ToString())
                                                || "3".Equals(objData["LOAIDOITUONG"].ToString())))
                                {
                                    _noidungthu = "Đồng chi trả BHYT( " + _noidungthu + ")";
                                    obj_2.LOAITHUTIEN = "1";
                                }
                                else
                                {
                                    _noidungthu = "Thu tiền cho viện phí " + this._benhNhan["MATIEPNHAN"].ToString() + "( " + _noidungthu + ")";
                                    obj_2.LOAITHUTIEN = "0";
                                }

                                obj_2.NOIDUNGTHU = _noidungthu;
                                obj_2.NOIDUNGIN = _noidungthu;
                                obj_2.DSDV = _DSDV;
                                obj_2.PTCT = _PTCT;

                                phieuInfo.Add(obj_2);
                            }

                            coDV = true;
                            if (!"1".Equals(this.vpiConfig["VPI_TUDONGTACH_HOADON"].ToString()))
                            {
                                teMaPhieu.Properties.ReadOnly = false;
                            }
                        }
                    }
                }
            }

            if ("1".Equals(this._benhNhan["DOITUONGBENHNHANID"].ToString()))
            {
                loaiSo = "2";
            }
            else
            {
                loaiSo = "3";
            }

            //var _sql_par = [];
            var _kieuthu = loaiPhieuThu;
            //_sql_par.push({ name: "[0]", value: _loaiso});
            //_sql_par.push({ name: "[1]", value: (_kieuthu)});
            //_sql_par.push({ name: "[2]", value: _loaitt});
            //_sql_par.push({ name: "[3]", value: _phong_id});
            var _input = loaiSo + "$" + _kieuthu + "$" + loaiTT + "$" + Const.local_phongId;
            var _result = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.13", _input, 0);
            var _dsSo = _result;
            if (_dsSo.Rows.Count == 0)
            {
                hetPhieu = true;
                obj.MAPHIEUTHU = "";
                obj.NHOMPHIEUTHUID = "";
                obj.MANHOMPHIEUTHU = "";
                obj.LOAIPHIEUTHU = "";
                obj.LOAIPHIEUTHUID = loaiPhieuThu.ToString();
            }
            else
            {
                hetPhieu = false;
                obj.MAPHIEUTHU = _dsSo.Rows[0]["MAPHIEUTHU"].ToString();
                obj.SOPHIEUTO = _dsSo.Rows[0]["SOPHIEUTO"].ToString();
                obj.KHOASOPHIEUTU = _dsSo.Rows[0]["KHOASOPHIEUTU"].ToString();
                obj.SOPHIEUFROM = _dsSo.Rows[0]["SOPHIEUFROM"].ToString();
                obj.NHOMPHIEUTHUID = _dsSo.Rows[0]["NHOMPHIEUTHUID"].ToString();
                obj.MANHOMPHIEUTHU = _dsSo.Rows[0]["MANHOMPHIEUTHU"].ToString();
                obj.LOAIPHIEUTHU = _dsSo.Rows[0]["LOAIPHIEUTHU"].ToString();
                obj.LOAIPHIEUTHUID = loaiPhieuThu.ToString();
                obj.LOAIDOITUONG = "-1";
                obj.LOAITHUTIEN = "-1";
                obj.DS_MAUBENHPHAMID = "";
                obj.DANHSACHDOITUONGDICHVU = "";
                obj.TIEN_BHYT_TRA = "0";
                obj.MIENGIAMDV = "0";
                AddOb(objData, "PHONGID_DANGNHAP", Const.local_phongId.ToString());
            }

            teNguoiLap.Text = Const.local_username;
            deNgay.DateTime = Func.getSysDatetime();
            if (coDV && _dsSo_2 != null && _dsSo_2.Rows.Count > 0)
            {
                var dtObj2Dis = Func.getTableEmpty(new string[] { "LOAIPHIEUTHUID", "MAPHIEUTHU" });
                var obj_2_dis = dtObj2Dis.NewRow();
                obj_2_dis["LOAIPHIEUTHUID"] = obj.LOAIPHIEUTHUID;
                obj_2_dis["MAPHIEUTHU"] = obj_2.MAPHIEUTHU;
                dtObj2Dis.Rows.Add(obj_2_dis);
                this.SetTTThuTien(obj_2_dis);
                ucCboMaSo.SelectValue = "0";
            }
            else if (_dsSo != null && _dsSo.Rows.Count > 0)
            {
                var dtObjDis = Func.getTableEmpty(new string[] { "LOAIPHIEUTHUID", "MAPHIEUTHU" });
                var obj_dis = dtObjDis.NewRow();
                obj_dis["LOAIPHIEUTHUID"] = obj.LOAIPHIEUTHUID;
                obj_dis["MAPHIEUTHU"] = obj.MAPHIEUTHU;
                dtObjDis.Rows.Add(obj_dis);
                this.SetTTThuTien(obj_dis);
                ucCboMaSo.SelectValue = null;

                var dtMaSo = Func.getTableEmpty(new string[] { "col1", "col2" });
                for (int i = 0; i < _dsSo.Rows.Count; i++)
                {
                    var drMaSo = dtMaSo.NewRow();
                    drMaSo["col1"] = i.ToString();
                    drMaSo["col2"] = _dsSo.Rows[i]["MANHOMPHIEUTHU"].ToString();
                    dtMaSo.Rows.Add(drMaSo);
                }

                ucCboMaSo.setData(dtMaSo, "col1", "col2");
                ucCboMaSo.setColumn(0, false);
                ucCboMaSo.SelectValue = "0";
            }

            cal(loaiPhieuThu);
            this.SetEnabled(new List<SimpleButton>() { btnLuu, btnHuyBo }, new List<SimpleButton>() { btnThem });
        }

        private void cal(string _loaiphieuthu)
        {
            _phieutamungid = null;
            double _sotien = 0;
            var _noidungthu = "";
            Dictionary<string, string> objTien = new Dictionary<string, string>();
            if (_loaiphieuthu.Equals(_THUTIEN))
            {
                teMienGiam.Properties.ReadOnly = false;
                teLyDo.Properties.ReadOnly = false;
                teSoTien.Properties.ReadOnly = true;
                var _miengiam = Func.Parse_double(teMienGiam.Text);
                teThucThu.Text = (Func.Parse_double(teSoTien.Text) - _miengiam).ToString();
                _sotien = Func.Parse_double(teSoTien.Text);

                AddOb(objTien, "DANOP", (Func.Parse_double(this.drVpData["DANOP"].ToString()) + _sotien).ToString());
                AddOb(objTien, "TAMUNG", this.drVpData["TAMUNG"].ToString());
                AddOb(objTien, "HOANUNG", this.drVpData["HOANUNG"].ToString());
                AddOb(objTien, "NOPTHEM", (Func.Parse_double(this.drVpData["NOPTHEM"].ToString()) - _sotien).ToString());
                AddOb(objTien, "MIENGIAM", (Func.Parse_double(this.drVpData["MIENGIAM"].ToString()) + _miengiam).ToString());

                this.SetTongVP(objTien);

                if (coDV)
                {
                    if ("1".Equals(GetValue(objData, "LOAIDOITUONG"))
                        || "2".Equals(GetValue(objData, "LOAIDOITUONG"))
                        || "3".Equals(GetValue(objData, "LOAIDOITUONG")))
                    {
                        _noidungthu = "Đồng chi trả BHYT";
                        AddOb(objData, "LOAITHUTIEN", "1");
                    }
                    else
                    {
                        _noidungthu = "Thu tiền cho viện phí " + this._benhNhan["MATIEPNHAN"].ToString();
                        AddOb(objData, "LOAITHUTIEN", "0");
                    }
                    txtGHICHU.Text = _noidungthu;
                }
            }
            else if (_loaiphieuthu.Equals(_THUTHEM))
            {
                teMienGiam.Text = "0";
                teMienGiam.Properties.ReadOnly = false;
                teLyDo.Properties.ReadOnly = false;
                teSoTien.Properties.ReadOnly = true;
                _sotien = tienHoaDon;
                if ("1".Equals(this._benhNhan["TRANGTHAITIEPNHAN"].ToString())
                    || "2".Equals(this._benhNhan["TRANGTHAITIEPNHAN"].ToString()))
                {
                    if (!"1".Equals(this.vpiConfig["VPI_QUYTRINH_VIENPHI"].ToString()) && this.chot)
                        _sotien = Func.Parse_double(this.drVpData["NOPTHEM"].ToString());
                }
                _sotien = _sotien > 0 ? _sotien : 0;
                teSoTien.Text = FormatMoney(_sotien.ToString(), 0);
                teThucThu.Text = FormatMoney(_sotien.ToString(), 0);
                AddOb(objTien, "DANOP", (Func.Parse_double(this.drVpData["DANOP"].ToString()) + _sotien).ToString());
                AddOb(objTien, "TAMUNG", this.drVpData["TAMUNG"].ToString());
                AddOb(objTien, "HOANUNG", this.drVpData["HOANUNG"].ToString());
                AddOb(objTien, "NOPTHEM", (Func.Parse_double(this.drVpData["NOPTHEM"].ToString()) - _sotien).ToString());
                AddOb(objTien, "MIENGIAM", this.drVpData["MIENGIAM"].ToString());

                this.SetTongVP(objTien);

                txtGHICHU.Text = "Thu tiền cho viện phí " + this._benhNhan["MATIEPNHAN"].ToString();
                AddOb(objData, "TONGTIEN", _sotien.ToString());
            }
            else if (_loaiphieuthu.Equals(_TAMUNG))
            {
                teMienGiam.Text = "0";
                var _arr_yctu = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.YCTU", this.tiepNhanId, 0);

                if (_arr_yctu.Rows.Count > 0)
                {
                    var _yctu = _arr_yctu.Rows[0];
                    var _sotien_yc = Func.Parse_double(_yctu["SOTIEN"].ToString());
                    _phieutamungid = _yctu["PHIEUTAMUNGID"].ToString();
                    if (_sotien_yc > 0)
                    {
                        teSoTien.Text = FormatMoney(_sotien_yc.ToString(), 0);
                    }
                }
                else if ("0".Equals(this._benhNhan["LOAITIEPNHANID"].ToString()))
                {
                    teSoTien.Text = FormatMoney(this.vpiConfig["VPI_SOTIEN_TAMUNGNTU"].ToString(), 0);
                }
                else
                {
                    teSoTien.Text = FormatMoney(Func.Parse_double(this.drVpData["NOPTHEM"].ToString()) > 0 ? Func.Parse_double(this.drVpData["NOPTHEM"].ToString()).ToString() : "0", 0);
                }
                teSoTien.Properties.ReadOnly = false;
                teMienGiam.Properties.ReadOnly = true;
                teLyDo.Properties.ReadOnly = true;
                teThucThu.Text = teSoTien.Text;
                _sotien = Func.Parse_double(teThucThu.Text) <= 0 ? 0 : Func.Parse_double(teThucThu.Text);

                AddOb(objTien, "DANOP", this.drVpData["DANOP"].ToString());
                AddOb(objTien, "TAMUNG", (Func.Parse_double(this.drVpData["TAMUNG"].ToString()) + _sotien).ToString());
                AddOb(objTien, "HOANUNG", this.drVpData["HOANUNG"].ToString());
                AddOb(objTien, "NOPTHEM", (Func.Parse_double(this.drVpData["NOPTHEM"].ToString()) - _sotien).ToString());
                AddOb(objTien, "MIENGIAM", this.drVpData["MIENGIAM"].ToString());

                this.SetTongVP(objTien);
                txtGHICHU.Text = "Tạm ứng cho viện phí " + this._benhNhan["MATIEPNHAN"].ToString();
                AddOb(objData, "TONGTIEN", _sotien.ToString());
                teSoTien.Focus();

            }
            else if (_loaiphieuthu.Equals(_HOANUNG))
            {
                teMienGiam.Text = "0";
                teMienGiam.Properties.ReadOnly = false;
                teLyDo.Properties.ReadOnly = false;
                teSoTien.Properties.ReadOnly = true;

                var _hoanung = Func.Parse_double(this.drVpData["NOPTHEM"].ToString());
                _hoanung = _hoanung < 0 ? _hoanung * (-1) : 0;
                if ("1".Equals(this.vpiConfig["VPI_QUYTRINH_VIENPHI"].ToString()))
                {
                    var _chenh_lech = Func.Parse_double(this.drVpData["TAMUNG"].ToString()) - Func.Parse_double(this.drVpData["HOANUNG"].ToString());
                    _chenh_lech = _chenh_lech > 0 ? _chenh_lech : 0;
                    _hoanung = _chenh_lech;
                }
                teSoTien.Text = FormatMoney(_hoanung.ToString(), 0);
                teThucThu.Text = FormatMoney(_hoanung.ToString(), 0);
                _sotien = Func.Parse_double(teThucThu.Text) <= 0 ? 0 : Func.Parse_double(teThucThu.Text);

                AddOb(objTien, "DANOP", this.drVpData["DANOP"].ToString());
                AddOb(objTien, "TAMUNG", this.drVpData["TAMUNG"].ToString());
                AddOb(objTien, "HOANUNG", (Func.Parse_double(this.drVpData["HOANUNG"].ToString()) + _sotien).ToString());
                AddOb(objTien, "NOPTHEM", (Func.Parse_double(this.drVpData["NOPTHEM"].ToString()) + _sotien).ToString());
                AddOb(objTien, "MIENGIAM", this.drVpData["MIENGIAM"].ToString());

                this.SetTongVP(objTien);
                txtGHICHU.Text = "Hoàn ứng cho viện phí " + this._benhNhan["MATIEPNHAN"].ToString();
                AddOb(objData, "TONGTIEN", teSoTien.Text);
            }
            else if (_loaiphieuthu.Equals(_HOANDICHVU))
            {
                teMienGiam.Text = "0";
                teSoTien.Properties.ReadOnly = false;
                teMienGiam.Properties.ReadOnly = true;
                _sotien = tienHoaDon;
                _sotien = _sotien > 0 ? _sotien : 0;
                teSoTien.Text = FormatMoney(_sotien.ToString(), 0);
                teThucThu.Text = FormatMoney(_sotien.ToString(), 0);
                AddOb(objTien, "DANOP", (Func.Parse_double(this.drVpData["DANOP"].ToString()) - _sotien).ToString());
                AddOb(objTien, "TAMUNG", this.drVpData["TAMUNG"].ToString());
                AddOb(objTien, "HOANUNG", this.drVpData["HOANUNG"].ToString());
                AddOb(objTien, "NOPTHEM", (Func.Parse_double(this.drVpData["NOPTHEM"].ToString()) + _sotien).ToString());
                AddOb(objTien, "MIENGIAM", this.drVpData["MIENGIAM"].ToString());

                this.SetTongVP(objTien);
                txtGHICHU.Text = "Hoàn tiền dịch vụ không làm cho bệnh nhân " + this._benhNhan["MATIEPNHAN"].ToString();
                AddOb(objData, "TONGTIEN", _sotien.ToString());
            }
        }

        /// <summary>
        /// Load danh sách phiếu thu
        /// </summary>
        /// <param name="page"></param>
        private void LoadDataGridPhieuThu(int? page)
        {
            try
            {
                int pageNum = 0;
                if (page == null)
                {
                    pageNum = ucGridPhieuThu.ucPage1.Current();
                }
                else
                {
                    pageNum = page.GetValueOrDefault();
                }

                if (pageNum <= 0)
                {
                    pageNum = 1;
                }

                ResponsList responses = new ResponsList();

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "VPI01T001.03"
                    , pageNum
                    , 999999
                    , new String[] { "[0]" }
                    , new string[] { this.tiepNhanId }
                    , "");

                ucGridPhieuThu.clearData();

                DataTable dtPhieuThu = new DataTable();
                dtPhieuThu = MyJsonConvert.toDataTable(responses.rows);
                if (dtPhieuThu.Rows.Count <= 0)
                {
                    dtPhieuThu = Func.getTableEmpty(
                        new String[] {
                                "ICON",
                                "PHIEUTHUID"
                                ,"MACDINH"
                                , "DAHUYPHIEU"
                                , "TIEPNHANID"
                                , "MAPHIEUTHU"
                                , "DATRA"
                                , "LOAIPHIEUTHU"
                                , "LOAIPHIEUTHUID"
                                , "LOAIPHIEUTHUID_2"
                                , "NGAYTHU"
                                , "NGUOITHU"
                                , "LYDOHUYPHIEU"
                                , "PHIEUTHULOG"
                                , "TENCONGTYBN"
                                , "DIACHI_CTYBN"
                                , "MASOTHUE_CTYBN"
                        });
                }
                else
                {
                    dtPhieuThu.Columns.Add("ICON");
                }

                for (int i = 0; i < dtPhieuThu.Rows.Count; i++)
                {
                    if ("1".Equals(dtPhieuThu.Rows[i]["DAHUYPHIEU"].ToString()))
                    {
                        dtPhieuThu.Rows[i]["ICON"] = "1";
                    }

                    dtPhieuThu.Rows[i]["DATRA"] = FormatMoney(dtPhieuThu.Rows[i]["DATRA"].ToString());
                }
                ucGridPhieuThu.setData(dtPhieuThu, responses.total, responses.page, responses.records);

                ucGridPhieuThu.setColumnAll(false);
                ucGridPhieuThu.setColumn("ICON", 0, " ", 25);
                ucGridPhieuThu.setColumn("MAPHIEUTHU", 1, "Mã phiếu thu", 90);
                ucGridPhieuThu.setColumn("DATRA", 2, "Số tiền", 100);
                ucGridPhieuThu.setColumnMemoEdit("LOAIPHIEUTHUID_2", 3, "Loại phiếu", 110);
                ucGridPhieuThu.setColumn("NGAYTHU", 4, "Ngày thanh toán", 140);
                ucGridPhieuThu.setColumnMemoEdit("NGUOITHU", 5, "Người thu", 150);
                ucGridPhieuThu.setColumnMemoEdit("LYDOHUYPHIEU", 6, "Lý do hủy", 80);

                ucGridPhieuThu.gridView.Columns["DATRA"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                ucGridPhieuThu.setColumnImage("ICON", new String[] { "1" }
                            , new String[] { "./Resources/delete.png" });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void LoadThongTinPhieuThu(DataRowView drBenhNhan)
        {
            if (flagLoading)
            {
                return;
            }

            this.SetEnabled(null, new List<SimpleButton>() { btnThem, btnDuyetKeToan });
            this.ClearThongTinThuTien();
            this.tiepNhanId = drBenhNhan["TIEPNHANID"].ToString();
            this._phieuthuid = "-1";
            this.LayThongTinTiepNhan();
            ucCboLoaiPhieu.SelectValue = "1";
            ucCboLoaiTT.SelectValue = "1";
            this.SetEnabled(null, new List<SimpleButton>() { btnHuyPhieu });
        }

        private void LayThongTinTiepNhan()
        {
            SetEnabled(null, new List<SimpleButton>() { btnThuKhac });

            var dtBenhNhan = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.11", this.tiepNhanId, 0);
            if (dtBenhNhan != null && dtBenhNhan.Rows.Count > 0)
            {
                _benhNhan = dtBenhNhan.Rows[0];
                this.SetTTVienPhi(_benhNhan);
                if ("1".Equals(_benhNhan["TRANGTHAITIEPNHAN_BH"].ToString()) || "1".Equals(_benhNhan["TRANGTHAITIEPNHAN_VP"].ToString()))
                {
                    btnDuyetKeToan.Text = "Gỡ duyệt kế toán";
                    this._flag_duyet = false;
                    SetEnabled(null, new List<SimpleButton>() { btnThem });
                }
                else
                {
                    btnDuyetKeToan.Text = "Duyệt kế toán";
                    this._flag_duyet = true;
                }

                this.KiemTraBenhNhan();
                this.LoadDataGridDichVu(null);
                this.LoadDataGridPhieuThu(null);
            }
        }

        private void KiemTraBenhNhan()
        {
            if (_benhNhan == null)
            {
                return;
            }

            cheTronVien.Checked = "1".Equals(_benhNhan["DATRONVIEN"].ToString());

            if ("1".Equals(_benhNhan["DOITUONGBENHNHANID"].ToString()))
            {
                cheDaGiuTheBH.Text = "Đã giữ thẻ BH";
                cheDaGiuTheBH.Checked = "1".Equals(_benhNhan["DAGIUTHEBHYT"].ToString());
            }
            else
            {
                cheDaGiuTheBH.Text = "Đã thu tiền khám";
                cheDaGiuTheBH.Checked = "1".Equals(_benhNhan["DATHUTIENKHAM"].ToString());
            }

            if (_benhNhan["TRANGTHAITIEPNHAN"].ToString() == "0" && vpiConfig["VP_INPHOI_DONGBA"].ToString() == "0")
                SetEnabled(null, new List<SimpleButton> { btnInPhoi });
            else
                SetEnabled(new List<SimpleButton> { btnInPhoi }, null);
        }

        private void TinhTongTien(DataTable dtDichVu)
        {
            var tranBHYT = this._benhNhan["BHYT_GIOIHANBHYTTRAHOANTOAN"].ToString();
            var tyleTuyen = this._benhNhan["TYLE_TUYEN"].ToString();
            var tyleBHYT = this._benhNhan["TYLE_THE"].ToString();

            var dtDaGiaoDich = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.06", this.tiepNhanId, 0);
            DataRow rVPGiaoDich = null;
            if (dtDaGiaoDich != null && dtDaGiaoDich.Rows.Count > 0)
            {
                rVPGiaoDich = dtDaGiaoDich.Rows[0];
            }
            var vpData = this.TinhTienDV(dtDichVu, rVPGiaoDich, tranBHYT, tyleTuyen, tyleBHYT);
            if (vpData != null && vpData.Rows.Count > 0)
            {
                drVpData = vpData.Rows[0];
                this.SetTongVP(drVpData);
                if ((!"0".Equals(this._benhNhan["DT_QUANNHAN"].ToString()) || "0".Equals(drVpData["NOPTHEM"].ToString()))
                    && ("1".Equals(this._benhNhan["TRANGTHAITIEPNHAN"].ToString()) || "2".Equals(this._benhNhan["TRANGTHAITIEPNHAN"].ToString())))
                {
                    SetEnabled(new List<SimpleButton>() { btnDuyetKeToan }, null);
                }
                else
                {
                    SetEnabled(null, new List<SimpleButton>() { btnDuyetKeToan });
                }
            }
        }

        private void InBangKe(string _tiepNhanID, string _dTBNID, string _loaiTiepNhanID)
        {
            string opt = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_TACH_BANGKE");
            string IN_BK_VP = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "c");
            string IN_GOP_BKNTRU = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VPI_GOP_BANGKENTRU");
            if ("1".Equals(opt))
            {
                string flag = RequestHTTP.call_ajaxCALL_SP_I("VPI01T004.10", _tiepNhanID);
                if ("0".Equals(_loaiTiepNhanID))
                {
                    if ("1".Equals(IN_GOP_BKNTRU))
                    {
                        InPhoiVP("1", _tiepNhanID, "NTU001_BKCPKCBBHYTNOITRU_02BV_QD3455_A4");
                    }
                    else
                    {
                        if ("1".Equals(_dTBNID))
                        {
                            RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                            InPhoiVP("1", _tiepNhanID, "NTU001_BKCPKCBBHYTNOITRU_02BV_QD3455_A4");
                            if ("0".Equals(IN_BK_VP) && "1".Equals(flag))
                            {
                                InPhoiVP("1", _tiepNhanID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                            }
                        }
                        else
                        {
                            if ("1".Equals(flag))
                            {
                                InPhoiVP("1", _tiepNhanID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                            }
                        }
                    }
                }
                else
                {
                    if ("1".Equals(_dTBNID))
                    {
                        RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                        InPhoiVP("1", _tiepNhanID, "NGT001_BKCPKCBBHYTNGOAITRU_01BV_QD3455_A4");
                        if ("0".Equals(IN_BK_VP) && "1".Equals(flag))
                        {
                            InPhoiVP("1", _tiepNhanID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                        }
                    }
                    else
                    {
                        if ("1".Equals(flag))
                        {
                            InPhoiVP("1", _tiepNhanID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                        }
                    }
                }
            }
            else
            {
                RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                if ("0".Equals(_loaiTiepNhanID))
                {
                    InPhoiVP("1", _tiepNhanID, "NTU001_BKCPKCBNOITRU_02BV_QD3455_A4");
                }
                else
                {
                    InPhoiVP("1", _tiepNhanID, "NGT001_BKCPKCBNGOAITRU_01BV_QD3455_A4");
                }
            }
            // in bang ke hao phi neu co
            string flag_HaoPhi = RequestHTTP.call_ajaxCALL_SP_I("VPI01T005.11", _tiepNhanID);
            string opt_HaoPhi = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_BANGKE_HAOPHI");
            if ("1".Equals(opt_HaoPhi))
            {
                if ("1".Equals(flag_HaoPhi))
                {
                    InPhoiVP("1", _tiepNhanID, "NGT001_BKCPKCB_HAOPHI_01BV_QD3455_A4");
                }
            }
        }

        private void InPhoiVP(string _inBangKeChuan, string _tiepNhanID, string _report_code)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("inbangkechuan", "String", _inBangKeChuan);
            table.Rows.Add("tiepnhanid", "String", _tiepNhanID);

            frmPrint frm = new frmPrint(_report_code, _report_code, table, 650, 900);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private string FormatMoney(string money, int soKyTuThapPhan = 2)
        {
            if (soKyTuThapPhan == 0 && "0".Equals(money))
            {
                return money;
            }
            else if (soKyTuThapPhan == 2 && "0".Equals(money))
            {
                return "0.00";
            }
            else if (string.IsNullOrEmpty(money))
            {
                return "";
            }

            string dinhDang = "{0:0,0";
            for (int i = 0; i < soKyTuThapPhan; i++)
            {
                if (i == 0)
                {
                    dinhDang += ".";
                }

                dinhDang += "0";
            }

            dinhDang += "}";

            return string.Format(dinhDang, Func.Parse_double(money));// "{0:0,0.00}"
        }

        private string FormatMoney(double money)
        {
            return string.Format("{0:0,0.00}", money);
        }

        private string GetValueByName(DataRowView dr, string name, int soKyTuThapPhan = -1)
        {
            try
            {
                if (dr != null && !string.IsNullOrEmpty(name) && dr.Row.Table.Columns.Contains(name))
                {
                    var rs = dr[name].ToString();
                    if (!"00.00".Equals(rs))
                    {
                        if (soKyTuThapPhan == -1)
                        {
                            return rs;
                        }

                        if (IsNumber(rs))
                        {
                            return FormatMoney(rs, soKyTuThapPhan);
                        }

                        return rs;
                    }

                    return FormatMoney("0", soKyTuThapPhan);
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        private string GetValueByName(DataRow dr, string name, int soKyTuThapPhan = -1)
        {
            try
            {
                if (dr != null)
                {
                    if (!string.IsNullOrEmpty(name) && dr.Table.Columns.Contains(name))
                    {
                        var rs = dr[name].ToString();
                        if (!"00.00".Equals(rs))
                        {
                            if (soKyTuThapPhan == -1)
                            {
                                return rs;
                            }

                            if (IsNumber(rs))
                            {
                                return FormatMoney(rs, soKyTuThapPhan);
                            }

                            return rs;
                        }

                        return FormatMoney("0", soKyTuThapPhan);
                    }

                    return null;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return null;
            }
        }

        private string GetValue(Dictionary<string, string> list, string key, int soKyTuThapPhan = -1)
        {
            try
            {
                string rs = "";
                list.TryGetValue(key, out rs);
                if (!"00.00".Equals(rs))
                {
                    if (soKyTuThapPhan == -1)
                    {
                        return rs;
                    }

                    if (IsNumber(rs))
                    {
                        return FormatMoney(rs, soKyTuThapPhan);
                    }

                    return rs;
                }

                return FormatMoney("0", soKyTuThapPhan);

            }
            catch
            {
                return null;
            }
        }

        private bool IsNumber(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }

        private DataTable TinhTienDV(DataTable dtDichVu, DataRow rVPGiaoDich, string tranBHYT, string tyleTuyen, string tyleBHYT)
        {
            double danop = Func.Parse_double(rVPGiaoDich["DANOP"].ToString());
            double danop_ngt = Func.Parse_double(rVPGiaoDich["DANOP_NGT"].ToString());
            double tamung = Func.Parse_double(rVPGiaoDich["TAMUNG"].ToString());
            double hoanung = Func.Parse_double(rVPGiaoDich["HOANUNG"].ToString());
            double miengiam = Func.Parse_double(rVPGiaoDich["MIENGIAM"].ToString());

            double _tong_tien_bh = 0;
            double _tien_dv = 0;
            double _bhyt_tra = 0;
            double _miengiam_dv = 0;
            double _vienphi = 0;
            double _nopthem = 0;
            double _thanhtoan = 0;
            double _nopthem_bh = 0;
            double _tien_bhyt_bntt = 0;
            for (var i = 0; i < dtDichVu.Rows.Count; i++)
            {
                _tien_dv += Func.Parse_double(dtDichVu.Rows[i]["THANHTIEN"].ToString());
                _bhyt_tra += Func.Parse_double(dtDichVu.Rows[i]["TIEN_BHYT_TRA"].ToString());
                _miengiam_dv += Func.Parse_double(dtDichVu.Rows[i]["TIEN_MIENGIAM"].ToString());
                _vienphi += Func.Parse_double(dtDichVu.Rows[i]["THUCTHU"].ToString());
                _tien_bhyt_bntt += Func.Parse_double(dtDichVu.Rows[i]["TIEN_BHYT_BNTT"].ToString());
                if ("1".Equals(dtDichVu.Rows[i]["LOAIDOITUONG"].ToString())
                    || "2".Equals(dtDichVu.Rows[i]["LOAIDOITUONG"].ToString())
                    || "3".Equals(dtDichVu.Rows[i]["LOAIDOITUONG"].ToString()))
                {
                    _tong_tien_bh += Func.Parse_double(dtDichVu.Rows[i]["THANHTIEN"].ToString());
                }
            }

            _nopthem = _vienphi - danop - miengiam - tamung + hoanung;
            _thanhtoan = _vienphi - tamung - danop_ngt;
            _nopthem_bh = _vienphi - _tien_bhyt_bntt;

            DataTable dt = Func.getTableEmpty(new string[]
            {
                "TONGTIENDV",
                "TONGTIENBH",
                "TYLE_TT",
                "BHYT_THANHTOAN",
                "MIENGIAMDV",
                "VIENPHI",
                "MIENGIAM",
                "TAMUNG",
                "HOANUNG",
                "DANOP",
                "DANOP_NGT",
                "MIENGIAMBH",
                "TAMUNGBH",
                "HOANUNGBH",
                "NOPTHEM",
                "THANHTOAN",
                "BHYT_NOPTHEM",
                "BHYT_BNTT"
            });
            DataRow dr = dt.NewRow();

            dr["TONGTIENDV"] = _tien_dv;
            dr["TONGTIENBH"] = _tong_tien_bh;
            dr["TYLE_TT"] = _tong_tien_bh == 0 ? (double)0 : ((100 * _bhyt_tra) / _tong_tien_bh);
            dr["BHYT_THANHTOAN"] = _bhyt_tra;
            dr["MIENGIAMDV"] = _miengiam_dv;
            dr["VIENPHI"] = _vienphi;
            dr["MIENGIAM"] = miengiam;
            dr["TAMUNG"] = tamung;
            dr["HOANUNG"] = hoanung;
            dr["DANOP"] = danop;
            dr["DANOP_NGT"] = danop_ngt;
            dr["MIENGIAMBH"] = 0;
            dr["TAMUNGBH"] = 0;
            dr["HOANUNGBH"] = 0;
            dr["NOPTHEM"] = _nopthem;
            dr["THANHTOAN"] = _thanhtoan;
            dr["BHYT_NOPTHEM"] = _nopthem_bh;
            dr["BHYT_BNTT"] = _tien_bhyt_bntt;

            dt.Rows.Add(dr);

            return dt;
        }

        private void XoaDichVu()
        {
            var drDichVu = (DataRowView)ucGridDichVu.gridView.GetFocusedRow();
            if (drDichVu == null)
            {
                return;
            }

            var dichVuKhamBenhId = drDichVu["DICHVUKHAMBENHID"].ToString();
            var tenDichVu = drDichVu["TENDICHVU"].ToString();

            var dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ " + tenDichVu + " ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                var rs = RequestHTTP.call_ajaxCALL_SP_S_result("VPI.XOADICHVU", dichVuKhamBenhId);
                MessageBox.Show(rs);
                LoadDataGridDichVu(null);
            }
        }

        private void SetEnabled(List<SimpleButton> listEnabled, List<SimpleButton> listDisabled)
        {
            if (listEnabled != null)
            {
                foreach (var item in listEnabled)
                {
                    item.Enabled = true;
                }
            }

            if (listDisabled != null)
            {
                foreach (var item in listDisabled)
                {
                    item.Enabled = false;
                }
            }
        }

        private bool ReadOnly
        {
            set
            {
                deNgay.Properties.ReadOnly = value;
                //ucCboMaSo.lookUpEdit.ReadOnly = value;
                ucCboLoaiPhieu.lookUpEdit.ReadOnly = value;
                teMienGiam.Properties.ReadOnly = value;
                teLyDo.Properties.ReadOnly = value;
                ucCboLoaiTT.lookUpEdit.ReadOnly = value;
                //txtGHICHU.Properties.ReadOnly = value;
            }
        }

        private bool EnabledButton
        {
            set
            {
                btnThem.Enabled = value;
                btnLuu.Enabled = value;
                btnHuyBo.Enabled = value;
                btnIn.Enabled = value;
                btnHuyPhieu.Enabled = value;
                btnDuyetKeToan.Enabled = value;
                btnThuKhac.Enabled = value;
                btnInPhoi.Enabled = value;
            }
        }

        private void ClearThongTinThuTien()
        {
            teHoTen.Text = string.Empty;
            teMaBHYT.Text = string.Empty;
            teNgayRa.Text = string.Empty;
            teNguoiLap.Text = string.Empty;
            deNgay.EditValue = null;
            teMaPhieu.Text = string.Empty;
            ucCboMaSo.SelectValue = null;
            ucCboMaSo.SelectText = string.Empty;
            ucCboLoaiPhieu.SelectValue = null;
            ucCboLoaiPhieu.SelectText = string.Empty;
            teSoTien.Text = string.Empty;
            teMienGiam.Text = string.Empty;
            teLyDo.Text = string.Empty;
            teThucThu.Text = string.Empty;
            cheTronVien.Checked = false;
            cheDaGiuTheBH.Checked = false;
            txtGHICHU.Text = string.Empty;
            teTongTienDichVu.Text = string.Empty;
            teMienGiamDichVu.Text = string.Empty;
            teBHYTThanhToan.Text = string.Empty;
            teVienPhi.Text = string.Empty;
            teTamUng.Text = string.Empty;
            teHoanUng.Text = string.Empty;
            teDaNop.Text = string.Empty;
            teMienGiamTT.Text = string.Empty;
            teNopThem.Text = string.Empty;
            teThanhToan.Text = string.Empty;
        }

        private void SetTTVienPhi(DataRow drBenhNhan)
        {
            SetText(teHoTen, GetValueByName(drBenhNhan, "TENBENHNHAN"));
            SetText(teMaBHYT, GetValueByName(drBenhNhan, "MABHYT"));
            SetText(teNgayRa, GetValueByName(drBenhNhan, "NGAYRAVIEN"));
        }

        private void SetTTThuTien(DataRow drPhieuThu)
        {
            SetText(teNguoiLap, GetValueByName(drPhieuThu, "NGUOILAP"));
            SetText(deNgay, GetValueByName(drPhieuThu, "NGAYLAP"));
            SetText(teMaPhieu, GetValueByName(drPhieuThu, "MAPHIEUTHU"));
            SetText(ucCboMaSo, GetValueByName(drPhieuThu, "MANHOMPHIEUTHU"));
            SetText(ucCboLoaiPhieu, "1".Equals(GetValueByName(drPhieuThu, "LOAIPHIEUTHUID")) ? "6" : GetValueByName(drPhieuThu, "LOAIPHIEUTHUID"));
            SetText(teSoTien, FormatMoney(GetValueByName(drPhieuThu, "TONGTIEN"), 0));
            SetText(teMienGiam, FormatMoney(GetValueByName(drPhieuThu, "MIENGIAM_PT"), 0));
            SetText(teLyDo, GetValueByName(drPhieuThu, "LYDO"));
            SetText(teThucThu, FormatMoney(GetValueByName(drPhieuThu, "THUCTHU"), 0));
            SetText(txtGHICHU, GetValueByName(drPhieuThu, "GHICHU"));
        }

        private void SetTongVP(DataRow dtVienPhi)
        {
            SetText(teTongTienDichVu, GetValueByName(dtVienPhi, "TONGTIENDV", 2));
            SetText(teMienGiamDichVu, GetValueByName(dtVienPhi, "MIENGIAMDV", 2));
            SetText(teBHYTThanhToan, GetValueByName(dtVienPhi, "BHYT_THANHTOAN", 2));
            SetText(teVienPhi, GetValueByName(dtVienPhi, "VIENPHI", 2));
            SetText(teTamUng, GetValueByName(dtVienPhi, "TAMUNG", 2));
            SetText(teHoanUng, GetValueByName(dtVienPhi, "HOANUNG", 2));
            SetText(teDaNop, GetValueByName(dtVienPhi, "DANOP", 2));
            SetText(teMienGiamTT, GetValueByName(dtVienPhi, "MIENGIAM", 2));
            SetText(teNopThem, GetValueByName(dtVienPhi, "NOPTHEM", 2));
            SetText(teThanhToan, GetValueByName(dtVienPhi, "THANHTOAN", 2));
        }

        private void SetTongVP(Dictionary<string, string> dtVienPhi)
        {
            SetText(teTongTienDichVu, GetValue(dtVienPhi, "TONGTIENDV", 2));
            SetText(teMienGiamDichVu, GetValue(dtVienPhi, "MIENGIAMDV", 2));
            SetText(teBHYTThanhToan, GetValue(dtVienPhi, "BHYT_THANHTOAN", 2));
            SetText(teVienPhi, GetValue(dtVienPhi, "VIENPHI", 2));
            SetText(teTamUng, GetValue(dtVienPhi, "TAMUNG", 2));
            SetText(teHoanUng, GetValue(dtVienPhi, "HOANUNG", 2));
            SetText(teDaNop, GetValue(dtVienPhi, "DANOP", 2));
            SetText(teMienGiamTT, GetValue(dtVienPhi, "MIENGIAM", 2));
            SetText(teNopThem, GetValue(dtVienPhi, "NOPTHEM", 2));
            SetText(teThanhToan, GetValue(dtVienPhi, "THANHTOAN", 2));
        }

        private void SetText(Control control, string value)
        {
            if (value != null)
            {
                if (control is TextEdit)
                {
                    ((TextEdit)control).Text = value;
                }
                else if (control is UserControl.ucComboBox)
                {
                    ((UserControl.ucComboBox)control).SelectValue = value;
                }
                else if (control is DateEdit)
                {
                    ((DateEdit)control).EditValue = value;
                }
            }
        }

        private void openForm(Form frm, string optionsPopup)
        {
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

        private void PopupWServiceInit(string loaiPhieuThu)
        {
            if (dtDVSai != null && dtDVSai.Rows.Count > 0)
            {
                VPI01T001_Dichvusai frm = new VPI01T001_Dichvusai();
                frm.SetParams(dtDVSai);

                this.openForm(frm, "1");

                if ("0".Equals(GetValueByName(this.vpiConfig, "VPI_KIEMTRA_TYLE")))
                {
                    MessageBox.Show("Số tiền bảo hiểm trả không khớp với mức hưởng, hãy kiểm tra lại.");
                    return;
                }
                var dialogResult = MessageBox.Show("Số tiền bảo hiểm trả không khớp với mức hưởng, bạn có chắc chắn tiếp tục ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.ThuTien(loaiPhieuThu);
                }
            }
            else
            {
                this.ThuTien(loaiPhieuThu);
            }
        }

        private void ThuTien(string loaiPhieuThu)
        {
            if (flagLoading
                && (_TAMUNG.Equals(loaiPhieuThu)
                    || _HOANDICHVU.Equals(loaiPhieuThu)
                    || _HOANUNG.Equals(loaiPhieuThu)
                    || coDV))
            {
                string strSoTien = Func.Parse_double(teSoTien.Text).ToString();
                if (!IsNumber(strSoTien))
                {
                    MessageBox.Show("Số tiền phải là số");
                    return;
                }

                string mienGiam = Func.Parse_double(teMienGiam.Text).ToString();
                if (!IsNumber(mienGiam))
                {
                    MessageBox.Show("Miễn giảm phải là số");
                    return;
                }

                string thucThu = Func.Parse_double(teThucThu.Text).ToString();
                if (!IsNumber(thucThu))
                {
                    MessageBox.Show("Thực thu phải là số");
                    return;
                }

                var soTien = Func.Parse_double(strSoTien);
                if (soTien < 0 || (soTien == 0 && !coDV))
                {
                    MessageBox.Show("Hãy nhập số tiền");
                    return;
                }

                if (_THUTIEN.Equals(loaiPhieuThu))
                {

                }
                else if (_THUTHEM.Equals(loaiPhieuThu))
                {
                    if ("0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                    {
                        if ("0".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN")))
                        {
                            MessageBox.Show("Không thể thu tiền cho bệnh nhân nội trú chưa ra viện");
                            return;
                        }

                        if ("1".Equals(GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"))
                            && !"2".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN"))
                            && "1".Equals(GetValueByName(this.vpiConfig, "VPI_DUYETBH_THANHTOANTU")))
                        {
                            MessageBox.Show("Không thể thanh toán cho bệnh nhân nội trú chưa duyệt bảo hiểm");
                            return;
                        }
                    }
                }
                else if (_TAMUNG.Equals(loaiPhieuThu))
                {
                    if ("1".Equals(GetValueByName(this.vpiConfig, "HIS_KHONG_TAMUNG_NGOAITRU"))
                        && !"0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                    {
                        var _TUNGT = RequestHTTP.call_ajaxCALL_SP_I("VPI_LAY_QUYEN", Const.local_phongId + "$" + "TUNGT");
                        if ("0".Equals(_TUNGT))
                        {
                            MessageBox.Show("Không cho phép bệnh nhân ngoại trú tạm ứng");
                            return;
                        }
                    }
                    if ("0".Equals(GetValueByName(this.vpiConfig, "VPI_TAMUNG_CHONHAPKHOA"))
                        && "1".Equals(GetValueByName(this._benhNhan, "NHAPNOITRU")))
                    {
                        MessageBox.Show("Không cho phép bệnh nhân đang chờ nhập khoa tạm ứng");
                        return;
                    }
                }
                else if (_HOANUNG.Equals(loaiPhieuThu))
                {
                    if ("2".Equals(GetValueByName(this.vpiConfig, "VPI_QUYTRINH_VIENPHI"))
                        && !"0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID"))
                        && "0".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN")))
                    {
                        MessageBox.Show("Không thể hoàn ứng cho bệnh nhân ngoại trú chưa ra viện");
                        return;
                    }
                    if ("0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                    {
                        if ("0".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN")))
                        {
                            MessageBox.Show("Không thể hoàn ứng cho bệnh nhân nội trú chưa ra viện");
                            return;
                        }
                        if ("1".Equals(GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"))
                            && !"2".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN"))
                            && "1".Equals(GetValueByName(this.vpiConfig, "VPI_DUYETBH_THANHTOANNTU")))
                        {
                            MessageBox.Show("Không thể thanh toán cho bệnh nhân nội trú chưa duyệt bảo hiểm");
                            return;
                        }
                    }
                    var _tien_conlai = Func.Parse_double(GetValueByName(this.vpiConfig, "NOPTHEM"));
                    if ((!"1".Equals(GetValueByName(this.vpiConfig, "VPI_QUYTRINH_VIENPHI")) && soTien > _tien_conlai * (-1))
                        || ("1".Equals(GetValueByName(this.vpiConfig, "VPI_QUYTRINH_VIENPHI"))
                            && soTien + Func.Parse_double(GetValueByName(this.vpiConfig, "HOANUNG")) > Func.Parse_double(GetValueByName(this.vpiConfig, "TAMUNG"))))
                    {
                        MessageBox.Show("Không thể hoàn ứng số tiền trên");
                        return;
                    }
                }
                else if (_HOANDICHVU.Equals(loaiPhieuThu) && !"1".Equals(GetValueByName(this.vpiConfig, "VPI_HOANDICHVU")))
                {
                    MessageBox.Show("Không cho phép hoàn dịch vụ");
                    return;
                }
                var ngay_thu = deNgay.EditValue == null ? "" : deNgay.DateTime.ToString("dd/MM/yyyy HH:mm:ss");

                AddOb(objData, "CHOT", this.chot ? "1" : "0");
                AddOb(objData, "BENHNHANID", GetValueByName(this._benhNhan, "BENHNHANID"));
                AddOb(objData, "HOSOBENHANID", GetValueByName(this._benhNhan, "HOSOBENHANID"));
                AddOb(objData, "NGAYTHU", ngay_thu);
                AddOb(objData, "KHOAID", GetValueByName(this._benhNhan, "KHOAID"));
                AddOb(objData, "PHONGID", GetValueByName(this._benhNhan, "PHONGID"));
                AddOb(objData, "CHIETKHAU", "0");
                AddOb(objData, "DATRA", GetValue(objData, "TONGTIEN"));
                AddOb(objData, "CONNO", "0");
                AddOb(objData, "CONNO_HENTHANHTOAN", "01/01/0001 00:00:00");
                AddOb(objData, "MAHOADULIEU", "0");
                AddOb(objData, "TENBENHNHAN", GetValueByName(this._benhNhan, "TENBENHNHAN"));
                AddOb(objData, "DAHUYPHIEU", "0");
                AddOb(objData, "DAYEUCAU", "0");
                AddOb(objData, "KHAC", "0");
                AddOb(objData, "THOIGIANHUYPHIEU", "01/01/0001 00:00:00");
                AddOb(objData, "NOIDUNGTHU", txtGHICHU.Text);
                AddOb(objData, "NOIDUNGIN", txtGHICHU.Text);
                AddOb(objData, "MIENGIAM", teMienGiam.Text);
                AddOb(objData, "LYDOMIENGIAM", teLyDo.Text);
                AddOb(objData, "HINHTHUCTHANHTOAN", ucCboLoaiTT.SelectValue);
                AddOb(objData, "LOAIPHIEUTHUID", loaiPhieuThu);
                AddOb(objData, "PHIEUTAMUNGID", _TAMUNG.Equals(loaiPhieuThu) ? _phieutamungid : null + "");
                AddOb(objData, "NGUONHACHTOAN", "0");
                AddOb(objData, "KHOACHUYENDEN", "0");
                AddOb(objData, "PHONGHACHTOAN", "0");

                obj.TONGTIEN = Func.Parse_double(teSoTien.Text).ToString();
                obj.THUCTHU = Func.Parse_double(teThucThu.Text).ToString();
                obj.NOIDUNGTHU = txtGHICHU.Text;
                obj.NOIDUNGIN = txtGHICHU.Text;
                obj.NHOMTHANHTOAN = "";

                if (Func.Parse_double(obj.MAPHIEUTHU) > Func.Parse_double(obj.SOPHIEUTO))
                {
                    MessageBox.Show("Số phiếu lớn hơn số phiếu lớn nhất của quyển sổ");
                    return;
                }

                //tuyennx_add_start_20171121 yc HISL2CORE-599
                string khoaSoPhieuTU = obj.KHOASOPHIEUTU;
                string maPhieuThu = obj.MAPHIEUTHU;
                if (!string.IsNullOrEmpty(khoaSoPhieuTU) && !string.IsNullOrEmpty(maPhieuThu) && Func.Parse_double(maPhieuThu) > Func.Parse_double(khoaSoPhieuTU))
                {
                    MessageBox.Show("Số phiếu đã bị khóa");
                    return;
                }

                //tuyennx_add_end_20171121 yc HISL2CORE-599
                if (Func.Parse_double(maPhieuThu) < Func.Parse_double(obj.SOPHIEUFROM))
                {
                    MessageBox.Show("Số phiếu nhỏ hơn số phiếu nhỏ nhất của quyển sổ");
                    return;
                }

                var _phieu = (PhieuThuObj2)obj.Clone();
                var _dsPhieu = new List<PhieuThuObj2>();
                _dsPhieu.Add(_phieu);
                double _offset = 0;
                if (phieuInfo.Count > 0)
                {
                    _offset = Func.Parse_double(teMaPhieu.Text) - Func.Parse_double(phieuInfo[phieuInfo.Count - 1].MAPHIEUTHU);
                }
                for (var i = 0; i < phieuInfo.Count; i++)
                {
                    phieuInfo[i].MAPHIEUTHU = (Func.Parse_double(phieuInfo[i].MAPHIEUTHU) + _offset + i) + "";
                    khoaSoPhieuTU = phieuInfo[i].KHOASOPHIEUTU;

                    if (Func.Parse_double(phieuInfo[i].MAPHIEUTHU) > Func.Parse_double(phieuInfo[i].SOPHIEUTO))
                    {
                        MessageBox.Show("Số phiếu lớn hơn số phiếu lớn nhất của quyển sổ");
                        return;
                    }
                    else if (Func.Parse_double(phieuInfo[i].MAPHIEUTHU) < Func.Parse_double(phieuInfo[i].SOPHIEUFROM))
                    {
                        MessageBox.Show("Số phiếu nhỏ hơn số phiếu nhỏ nhất của quyển sổ");
                        return;
                        //tuyennx_add_start_20171121 yc HISL2CORE-599
                    }
                    else if (!string.IsNullOrEmpty(khoaSoPhieuTU) && Func.Parse_double(phieuInfo[i].MAPHIEUTHU) >= Func.Parse_double(khoaSoPhieuTU))
                    {
                        MessageBox.Show("Số phiếu bị khóa");
                        return;
                        //tuyennx_add_end_20171121 yc HISL2CORE-599
                    }
                    else
                    {
                        _dsPhieu.Add(phieuInfo[i]);
                    }
                }

                if (_dsPhieu.Count == 0)
                {
                    return;
                }

                var objRequest = new
                {
                    BENHNHANID = GetValue(objData, "BENHNHANID"),
                    CHIETKHAU = GetValue(objData, "CHIETKHAU"),
                    CHOT = GetValue(objData, "CHOT"),
                    CONNO = GetValue(objData, "CONNO"),
                    CONNO_HENTHANHTOAN = GetValue(objData, "CONNO_HENTHANHTOAN"),
                    DAHUYPHIEU = GetValue(objData, "DAHUYPHIEU"),
                    DANHSACHDOITUONGDICHVU = GetValue(objData, "DANHSACHDOITUONGDICHVU"),
                    DATRA = GetValue(objData, "DATRA"),
                    DAYEUCAU = GetValue(objData, "DAYEUCAU"),
                    DOITUONGBENHNHANID = GetValue(objData, "DOITUONGBENHNHANID"),
                    DSDVKBID = GetValue(objData, "DSDVKBID"),
                    DSPHIEU = new List<object>(),
                    DS_MAUBENHPHAMID = GetValue(objData, "DS_MAUBENHPHAMID"),
                    HINHTHUCTHANHTOAN = GetValue(objData, "HINHTHUCTHANHTOAN"),
                    HOSOBENHANID = GetValue(objData, "HOSOBENHANID"),
                    KHAC = GetValue(objData, "KHAC"),
                    KHOACHUYENDEN = GetValue(objData, "KHOACHUYENDEN"),
                    KHOAID = GetValue(objData, "KHOAID"),
                    LOAIDOITUONG = GetValue(objData, "LOAIDOITUONG"),
                    LOAIPHIEUTHUID = GetValue(objData, "LOAIPHIEUTHUID"),
                    LYDOMIENGIAM = GetValue(objData, "LYDOMIENGIAM"),
                    MAHOADULIEU = GetValue(objData, "MAHOADULIEU"),
                    MIENGIAM = GetValue(objData, "MIENGIAM"),
                    MIENGIAMDV = GetValue(objData, "MIENGIAMDV"),
                    NGAYTHU = GetValue(objData, "NGAYTHU"),
                    NGUONHACHTOAN = GetValue(objData, "NGUONHACHTOAN"),
                    NOIDUNGIN = GetValue(objData, "NOIDUNGIN"),
                    NOIDUNGTHU = GetValue(objData, "NOIDUNGTHU"),
                    PHIEUTAMUNGID = GetValue(objData, "PHIEUTAMUNGID"),
                    PHONGHACHTOAN = GetValue(objData, "PHONGHACHTOAN"),
                    PHONGID = GetValue(objData, "PHONGID"),
                    PHONGID_DANGNHAP = GetValue(objData, "PHONGID_DANGNHAP"),
                    TENBENHNHAN = GetValue(objData, "TENBENHNHAN"),
                    THOIGIANHUYPHIEU = GetValue(objData, "THOIGIANHUYPHIEU"),
                    THUCTHU = GetValue(objData, "THUCTHU"),
                    TIEN_BHYT_TRA = GetValue(objData, "TIEN_BHYT_TRA"),
                    TIEPNHANID = GetValue(objData, "TIEPNHANID"),
                    TONGTIEN = GetValue(objData, "TONGTIEN"),
                };

                for (int i = 0; i < _dsPhieu.Count; i++)
                {
                    var phieu = new
                    {
                        DANHSACHDOITUONGDICHVU = _dsPhieu[i].DANHSACHDOITUONGDICHVU,
                        DSDV = new List<object>(),
                        DS_MAUBENHPHAMID = _dsPhieu[i].DS_MAUBENHPHAMID,
                        KHOASOPHIEUTU = _dsPhieu[i].KHOASOPHIEUTU,
                        LOAIDOITUONG = _dsPhieu[i].LOAIDOITUONG,
                        LOAIPHIEUTHU = _dsPhieu[i].LOAIPHIEUTHU,
                        LOAIPHIEUTHUID = _dsPhieu[i].LOAIPHIEUTHUID,
                        LOAITHUTIEN = _dsPhieu[i].LOAITHUTIEN,
                        MANHOMPHIEUTHU = _dsPhieu[i].MANHOMPHIEUTHU,
                        MAPHIEUTHU = _dsPhieu[i].MAPHIEUTHU,
                        MIENGIAMDV = _dsPhieu[i].MIENGIAMDV,
                        NHOMPHIEUTHUID = _dsPhieu[i].NHOMPHIEUTHUID,
                        NHOMTHANHTOAN = _dsPhieu[i].NHOMTHANHTOAN,
                        NOIDUNGIN = _dsPhieu[i].NOIDUNGIN,
                        NOIDUNGTHU = _dsPhieu[i].NOIDUNGTHU,
                        PTCT = new List<object>(),
                        SOPHIEUFROM = _dsPhieu[i].SOPHIEUFROM,
                        SOPHIEUTO = _dsPhieu[i].SOPHIEUTO,
                        THUCTHU = _dsPhieu[i].THUCTHU,
                        TIEN_BHYT_TRA = _dsPhieu[i].TIEN_BHYT_TRA,
                        TONGTIEN = _dsPhieu[i].TONGTIEN,
                    };

                    var objDSDV = _dsPhieu[i].DSDV;

                    if (objDSDV != null)
                    {
                        for (int j = 0; j < objDSDV.Count; j++)
                        {
                            var dv = new
                            {
                                DICHVUKHAMBENHID = objDSDV[j].DICHVUKHAMBENHID,
                                KHOANMUCID = objDSDV[j].KHOANMUCID,
                                MAKHOANMUC = objDSDV[j].MAKHOANMUC,
                                TENKHOANMUC = objDSDV[j].TENKHOANMUC,
                                THUCTHU = objDSDV[j].THUCTHU,
                                TIEN_BHYT_TRA = objDSDV[j].TIEN_BHYT_TRA,
                                TIEN_MIENGIAM = objDSDV[j].TIEN_MIENGIAM,
                                VERSION_OLD = objDSDV[j].VERSION_OLD,
                            };

                            phieu.DSDV.Add(dv);
                        }
                    }

                    var objPTCT = _dsPhieu[i].PTCT;
                    if (objPTCT != null)
                    {
                        for (int j = 0; j < objPTCT.Count; j++)
                        {
                            var ct = new
                            {
                                KHOANMUCID = objPTCT[j].KHOANMUCID,
                                MAKHOANMUC = objPTCT[j].MAKHOANMUC,
                                TENKHOANMUC = objPTCT[j].TENKHOANMUC,
                                TONGTIEN = objPTCT[j].TONGTIEN,
                            };

                            phieu.PTCT.Add(ct);
                        }
                    }

                    objRequest.DSPHIEU.Add(phieu);
                }

                var input = JsonConvert.SerializeObject(objRequest).Replace("\"", "\\\"");

                var fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.10", input);

                if (Func.Parse_double(fl) >= 1)
                {
                    flagLoading = false;
                    this.SetTTThuTien(null);
                    this.KiemTraBenhNhan();
                    ucCboLoaiPhieu.SelectValue = "1";
                    ucCboLoaiTT.SelectValue = "1";
                    this.LoadDataGridPhieuThu(null);
                    this.LoadDataGridDichVu(null);
                    this.SetEnabled(new List<SimpleButton>() { btnThem, btnIn, btnHuyPhieu }, new List<SimpleButton>() { btnHuyBo, btnLuu });
                    this.ReadOnly = true;
                    ucCboMaSo.lookUpEdit.ReadOnly = true;
                    _phieuthuid = fl;

                    if ("3".Equals(GetValueByName(this.vpiConfig, "HIS_FOCUS_MABN")))
                    {

                    }

                    //TUYENNX_ADD_START
                    var INVOICES_IMPORT_AUTO = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_IMPORT_AUTO");
                    if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_DAY_HOADONDT")) && "1".Equals(INVOICES_IMPORT_AUTO))
                    {
                        var sql_par = new List<string>() { _phieuthuid };
                        var rs = RequestHTTP.call_ajaxCALL_SP_S_result("VPI.IMPORT.INVOICES", string.Join("$", sql_par));
                        var INVOICES_URL_IMPORT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_URL_IMPORT");
                        var INVOICES_WS_USER = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_USER");
                        var INVOICES_WS_PWD = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_PWD");
                        var INVOICES_WS_USER_ACC = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_USER_ACC");
                        var INVOICES_WS_PWD_ACC = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_PWD_ACC");

                        var ret = "";// ajaxSvc.InvoicesWS.importHoaDon(rs,INVOICES_URL_IMPORT,INVOICES_WS_USER_ACC, INVOICES_WS_PWD_ACC, INVOICES_WS_USER, INVOICES_WS_PWD);
                        if (string.IsNullOrEmpty(ret) || ret.Contains("ERR"))
                        {
                            MessageBox.Show("Lỗi đẩy hóa đơn điện tử");
                        }
                        else
                        {
                            RequestHTTP.get_ajaxExecuteQuery("VPI.DAYHDDT.UPDATE",
                                new string[] { "[0]", "[1]" },
                                new string[] { _phieuthuid, "1" });
                        }
                    }
                }
                else if ("-1".Equals(fl))
                {
                    MessageBox.Show("Cập nhật không thành công");
                }
                else if ("-2".Equals(fl))
                {
                    MessageBox.Show("Có dịch vụ đã thu tiền, không thể hoàn");
                }
                else if ("-3".Equals(fl))
                {
                    MessageBox.Show("Có dịch vụ đã thực hiện, không thể hoàn");
                }
                else if ("-4".Equals(fl))
                {
                    MessageBox.Show("Có dịch vụ đã thu tiền");
                }
                else if ("-5".Equals(fl))
                {
                    MessageBox.Show("Dữ liệu đã thay đổi, hãy chọn lại bệnh nhân");
                }
                else if ("-6".Equals(fl))
                {
                    MessageBox.Show("Hết phiếu");
                }
                else if ("-7".Equals(fl))
                {
                    MessageBox.Show("Mã phiếu đã sử dụng");
                }
                else if ("-8".Equals(fl))
                {
                    MessageBox.Show("Số phiếu lớn hơn số phiếu lớn nhất của quyển sổ");
                }
                else if ("-9".Equals(fl))
                {
                    MessageBox.Show("Không cho phép thu tiền khi bệnh nhân chưa đóng bệnh án");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn dịch vụ để thanh toán");
                return;
            }
        }

        #endregion

        #region Events

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private void VPI01T001_thuvienphi_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void PageLoad_ucGridBenhNhan(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadDataGridBenhNhan(pageNum);
        }

        private void GridBenhNhan_Click(object sender, EventArgs e)
        {
            this.drvBenhNhan = (DataRowView)ucGridBenhNhan.gridView.GetFocusedRow();
            if (this.drvBenhNhan != null)
            {
                LoadThongTinPhieuThu(this.drvBenhNhan);
            }
        }

        private void GridPhieuThu_Click(object sender, EventArgs e)
        {
            if (flagLoading)
            {
                return;
            }
            this.flagTinhTongTien = false;
            var drvPhieuThu = (DataRowView)ucGridPhieuThu.gridView.GetFocusedRow();
            if (drvPhieuThu != null)
            {
                _phieuthuid = drvPhieuThu["PHIEUTHUID"].ToString();
                LoadDataGridDichVu(null, _phieuthuid);
                this.flagTinhTongTien = true;
                var daHuyPhieu = drvPhieuThu["DAHUYPHIEU"].ToString();

                if ("1".Equals(this.drvBenhNhan["TRANGTHAITIEPNHAN_BH"].ToString())
                    || "1".Equals(this.drvBenhNhan["TRANGTHAITIEPNHAN_VP"].ToString()))
                {
                    this.SetEnabled(null, new List<SimpleButton>() { btnHuyPhieu });
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tCapNhatPhieuThu);
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tHuyPhieuThu);
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tKhoiPhuc);
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tNhapThongTinThanhToan);
                }
                else if ("0".Equals(this.drvBenhNhan["TRANGTHAITIEPNHAN_BH"].ToString())
                    || "0".Equals(this.drvBenhNhan["TRANGTHAITIEPNHAN_VP"].ToString()))
                {
                    if ("1".Equals(daHuyPhieu))
                    {
                        ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tCapNhatPhieuThu);
                        ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tHuyPhieuThu);
                        ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tNhapThongTinThanhToan);
                        if ("1".Equals(this.vpiConfig["VPI_KHOIPHUCPHIEU"].ToString()))
                        {
                            ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(true, tKhoiPhuc);
                        }
                        else
                        {
                            ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tKhoiPhuc);
                        }
                    }
                    else
                    {
                        this.SetEnabled(new List<SimpleButton>() { btnHuyPhieu }, null);
                        ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(true, tCapNhatPhieuThu);
                        ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(true, tHuyPhieuThu);
                        ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tKhoiPhuc);
                        ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(true, tNhapThongTinThanhToan);
                    }
                }

                var loaiPhieuThuId = drvPhieuThu["LOAIPHIEUTHUID"].ToString();
                if (!"1".Equals(daHuyPhieu)
                    && "1".Equals(loaiPhieuThuId)
                    && "1".Equals(this.vpiConfig["VPI_DAY_HOADONDT"].ToString()))
                {
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(true, tXemHDDT);
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(true, tGuiHDDT);
                }
                else
                {
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tXemHDDT);
                    ucGridPhieuThu.MenuPopup_Enable_Child_byTitle(false, tGuiHDDT);
                }

                var dtPhieuThu = RequestHTTP.get_ajaxExecuteQueryO("VPI01T001.14",
                    new string[] { "[0]" },
                    new string[] { _phieuthuid });

                if (dtPhieuThu != null && dtPhieuThu.Rows.Count > 0)
                {
                    SetTTThuTien(dtPhieuThu.Rows[0]);

                    DataTable dtMaSo = Func.getTableEmpty(new string[] { "col1", "col2" });
                    DataRow rowMaSo;

                    rowMaSo = dtMaSo.NewRow();
                    rowMaSo["col1"] = "0";
                    rowMaSo["col2"] = dtPhieuThu.Rows[0]["MANHOMPHIEUTHU"].ToString();
                    dtMaSo.Rows.Add(rowMaSo);

                    ucCboMaSo.setData(dtMaSo, "col1", "col2");
                    ucCboMaSo.setColumn(0, false);
                    ucCboMaSo.SelectIndex = 0;

                    this.SetEnabled(new List<SimpleButton>() { btnIn }, null);
                }
            }
        }

        private void ucGridDichVu_Click(object sender, EventArgs e)
        {
            this.drvDichVu = (DataRowView)ucGridDichVu.gridView.GetFocusedRow();
            if (this.drvDichVu != null)
            {
                var doiTuongDV = ucCboDoiTuong.SelectValue;
                if ("3".Equals(this.drvDichVu["DATHUTIEN"].ToString()))
                {
                    ucGridDichVu.MenuPopup_Visible_Child_byTitle(false, this.tXoaDichVu);
                    ucGridDichVu.MenuPopup_Visible_Parent_byTitle(false, this.tDichVu);
                }
                else
                {
                    ucGridDichVu.MenuPopup_Visible_Child_byTitle(true, this.tXoaDichVu);
                    ucGridDichVu.MenuPopup_Visible_Parent_byTitle(false, this.tDichVu);
                }

                if ("1".Equals(this.vpiConfig["HIS_THUNGAN_THUKHAC"].ToString())
                    && !"2".Equals(this.drvBenhNhan["TRANGTHAITIEPNHAN"].ToString())
                    && !"1".Equals(this.drvBenhNhan["TRANGTHAITIEPNHAN_VP"].ToString()))
                {
                    this.SetEnabled(new List<SimpleButton>() { btnThuKhac }, null);
                }
                else
                {
                    this.SetEnabled(null, new List<SimpleButton>() { btnThuKhac });
                }
            }
        }

        #endregion

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.LoadDataGridBenhNhan(null);
        }

        private void radNgayVao_CheckedChanged(object sender, EventArgs e)
        {
            radNgayRa.Checked = !radNgayVao.Checked;
        }

        #region Menu popup chuột phải click

        private void MenuPopupGridPhieuThuClick(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                }

                MenuFunc menu = (MenuFunc)menuFunc;
                if (this.drvBenhNhan != null && menu != null)
                {
                    MenuPopupGridPhieuThuClick_Work(menu.hlink);
                }
            }
            finally
            {
                if (existSplash)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                }
            }
        }

        private void MenuPopupGridBenhNhanClick(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                }

                MenuFunc menu = (MenuFunc)menuFunc;
                if (this.drvBenhNhan != null && menu != null)
                {
                    MenuPopupGridBenhNhanClick_Work(menu.hlink);
                }
            }
            finally
            {
                if (existSplash)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                }
            }
        }

        private void MenuPopupGridDichVuClick(object menuFunc, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                }

                MenuFunc menu = (MenuFunc)menuFunc;
                if (this.drvBenhNhan != null && menu != null)
                {
                    MenuPopupGridDichVuClick_Work(menu.hlink);
                }
            }
            finally
            {
                if (existSplash)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                }
            }
        }

        private void MenuPopupGridPhieuThuClick_Work(string hlink)
        {
            if (rHuyPhieuThu.Equals(hlink))
            {
                btnHuyPhieu_Click(null, null);
            }
            else if (rKhoiPhuc.Equals(hlink))
            {
                if (flagLoading && "1".Equals(GetValueByName(this.vpiConfig, "VPI_KHOIPHUCPHIEU")))
                {
                    return;
                }

                var drPhieuThu = (DataRowView)ucGridPhieuThu.gridView.GetFocusedRow();
                if (drPhieuThu != null)
                {
                    var phieuThuId = GetValueByName(drPhieuThu, "PHIEUTHUID");
                    var result = RequestHTTP.call_ajaxCALL_SP_S_result("VPI.RESTORE", phieuThuId);
                    MessageBox.Show(result);
                    LoadDataGridPhieuThu(null);
                    LoadDataGridDichVu(null);
                    this.SetEnabled(new List<SimpleButton>() { btnHuyPhieu }, null);
                }
            }
            else if (rCapNhatPhieuThu.Equals(hlink))
            {
                if (flagLoading)
                {
                    return;
                }

                var drPhieuThu = (DataRowView)ucGridPhieuThu.gridView.GetFocusedRow();
                if (drPhieuThu != null)
                {
                    var phieuThuId = GetValueByName(drPhieuThu, "PHIEUTHUID");
                    var maPhieuThu = GetValueByName(drPhieuThu, "MAPHIEUTHU");
                    var maLog = GetValueByName(drPhieuThu, "PHIEUTHULOG");

                    VPI01T001_Capnhatphieuthu frm = new VPI01T001_Capnhatphieuthu();
                    frm.SetParams(phieuThuId, maPhieuThu, maLog);
                    openForm(frm, "1");
                    if (frm.dialogResult == DialogResult.OK)
                    {
                        LoadDataGridPhieuThu(null);
                    }
                }
            }
            else if (rNhapThongTinThanhToan.Equals(hlink))
            {
                if (flagLoading)
                {
                    return;
                }

                var drPhieuThu = (DataRowView)ucGridPhieuThu.gridView.GetFocusedRow();
                if (drPhieuThu != null)
                {
                    var phieuThuId = GetValueByName(drPhieuThu, "PHIEUTHUID");
                    var maPhieuThu = GetValueByName(drPhieuThu, "MAPHIEUTHU");
                    var tenCongTyBN = GetValueByName(drPhieuThu, "TENCONGTYBN");
                    var diaChiCongTyBN = GetValueByName(drPhieuThu, "DIACHI_CTYBN");
                    var maSoThueCongTyBN = GetValueByName(drPhieuThu, "MASOTHUE_CTYBN");

                    VPI01T001_Nhapthongtintt frm = new VPI01T001_Nhapthongtintt();
                    frm.SetParams(phieuThuId, maPhieuThu, tenCongTyBN, diaChiCongTyBN, maSoThueCongTyBN);
                    openForm(frm, "1");
                    if (frm.dialogResult == DialogResult.OK)
                    {
                        LoadDataGridPhieuThu(null);
                    }
                }
            }
            else if (rXemHDDT.Equals(hlink))
            {
                if (flagLoading)
                {
                    return;
                }

                var drPhieuThu = (DataRowView)ucGridPhieuThu.gridView.GetFocusedRow();
                if (drPhieuThu != null)
                {
                    var phieuThuId = GetValueByName(drPhieuThu, "PHIEUTHUID");
                    var INVOICES_URL_VIEW = GetValueByName(this.vpiConfig, "INVOICES_URL_VIEW");
                    var INVOICES_WS_USER = GetValueByName(this.vpiConfig, "INVOICES_WS_USER");
                    var INVOICES_WS_PWD = GetValueByName(this.vpiConfig, "INVOICES_WS_PWD");

                    var result = ViewHoaDon(phieuThuId, INVOICES_URL_VIEW, INVOICES_WS_USER, INVOICES_WS_PWD);
                    if (result.Contains("ERR:6"))
                    {
                        MessageBox.Show("Không tìm thấy hóa đơn");
                    }
                    else if (string.IsNullOrEmpty(result) || result.ToUpper().Contains("ERR"))
                    {
                        MessageBox.Show("Lỗi trong quá trình xử lý");
                    }
                    else
                    {
                        //var randomnumber = Math.floor((Math.ra() * 100) + 1);
                        //var win = window.open('', "_blank", 'PopUp', randomnumber, 'scrollbars=1,menubar=0,resizable=1,width=850,height=500');
                        //win.document.write(result);
                    }
                }
            }
            else if (rGuiHDDT.Equals(hlink))
            {
                if (flagLoading)
                {
                    return;
                }

                var drPhieuThu = (DataRowView)ucGridPhieuThu.gridView.GetFocusedRow();
                if (drPhieuThu != null)
                {
                    var phieuThuId = GetValueByName(drPhieuThu, "PHIEUTHUID");
                    var data = RequestHTTP.get_ajaxExecuteQueryO("VPI.DAYHDDT.CHECK",
                            new string[] { "[0]" },
                            new string[] { phieuThuId });

                    if (data != null && data.Rows.Count > 0)
                    {
                        var check = data.Rows[0]["SYNC_FLAG"].ToString();
                        if ("1".Equals(check))
                        {
                            MessageBox.Show("Hóa đơn đã được gửi");
                        }
                        else
                        {
                            DayThongTinBenhNhan();
                            var fl = RequestHTTP.call_ajaxCALL_SP_S_result("VPI.IMPORT.INVOICES", string.Join("$", new List<string>() { phieuThuId }));

                            //var INVOICES_URL_IMPORT = GetValueByName(this.vpiConfig, "INVOICES_URL_IMPORT");
                            //var INVOICES_WS_USER = GetValueByName(this.vpiConfig, "INVOICES_WS_USER");
                            //var INVOICES_WS_PWD = GetValueByName(this.vpiConfig, "INVOICES_WS_PWD");
                            //var INVOICES_WS_USER_ACC = GetValueByName(this.vpiConfig, "INVOICES_WS_USER_ACC");
                            //var INVOICES_WS_PWD_ACC = GetValueByName(this.vpiConfig, "INVOICES_WS_PWD_ACC");

                            string ret = ImportHoaDon(fl);
                            if (string.IsNullOrEmpty(ret) || ret.ToUpper().Contains("ERR"))
                            {
                                MessageBox.Show("Lỗi trong quá trình xử lý");
                            }
                            else
                            {
                                RequestHTTP.get_ajaxExecuteQuery("VPI.DAYHDDT.UPDATE"
                                    , new string[] { "[0]", "[1]" }
                                    , new string[] { phieuThuId, "1" });

                                MessageBox.Show("Gửi hóa đơn điện tử thành công");
                            }
                        }
                    }
                }
            }
        }

        private void DayThongTinBenhNhan()
        {
            var objCusData = new Object();

            var objCustomer = new
            {
                Name = GetValueByName(this._benhNhan, "TENBENHNHAN"),
                Code = GetValueByName(this._benhNhan, "MABENHNHAN"),
                TaxCode = "",
                Address = GetValueByName(this._benhNhan, "DIACHI"),
                BankAccountName = "",
                BankNumber = "",
                Email = "",
                Fax = "",
                Phone = GetValueByName(this._benhNhan, "SDTBENHNHAN"),
                ContactPerson = GetValueByName(this._benhNhan, "TEN_NGUOITHAN"),
                RepresentPerson = "",
                CusType = "0",
            };

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<Customers>");
            stringBuilder.Append("<Customer>");
            stringBuilder.Append("<Name>" + objCustomer.Name + "</Name>");
            stringBuilder.Append("<Code>" + objCustomer.Code + "</Code>");
            stringBuilder.Append("<TaxCode></TaxCode>");
            stringBuilder.Append("<Address>" + objCustomer.Address + "</Address>");
            stringBuilder.Append("<BankAccountName></BankAccountName>");
            stringBuilder.Append("<BankNumber></BankNumber>");
            stringBuilder.Append("<Email></Email>");
            stringBuilder.Append("<Fax></Fax>");
            stringBuilder.Append("<Phone>" + objCustomer.Phone + "</Phone>");
            stringBuilder.Append("<ContactPerson>" + objCustomer.ContactPerson + "</ContactPerson>");
            stringBuilder.Append("<RepresentPerson></RepresentPerson>");
            stringBuilder.Append("<CusType>0</CusType>");
            stringBuilder.Append("</Customer>");
            stringBuilder.Append("</Customers>");

            var str = stringBuilder.ToString();

            var iNVOICES_URL_IMPORT = GetValueByName(this.vpiConfig, "INVOICES_URL_IMPORT");
            var iNVOICES_WS_USER = GetValueByName(this.vpiConfig, "INVOICES_WS_USER");
            var iNVOICES_WS_PWD = GetValueByName(this.vpiConfig, "INVOICES_WS_PWD");

            Dictionary<string, string> data = new Dictionary<string, string>();

            ServiceHDDT.SetConfigs(iNVOICES_URL_IMPORT, "", "", "hisl2service", "123456aA@", "", "");
            var result = ServiceHDDT.UpdateCus(str, "0");

            if (!"1".Equals(result))
            {
                MessageBox.Show("Lỗi đẩy thông tin BN lên hệ thống HĐĐT: " + result);
            };
        }

        private string ImportHoaDon(string xmlInvData)
        {
            var iNVOICES_URL_IMPORT = GetValueByName(this.vpiConfig, "INVOICES_URL_IMPORT");
            var iNVOICES_WS_USER = GetValueByName(this.vpiConfig, "INVOICES_WS_USER");
            var iNVOICES_WS_PWD = GetValueByName(this.vpiConfig, "INVOICES_WS_PWD");
            var iNVOICES_WS_USER_ACC = GetValueByName(this.vpiConfig, "INVOICES_WS_USER_ACC");
            var iNVOICES_WS_PWD_ACC = GetValueByName(this.vpiConfig, "INVOICES_WS_PWD_ACC");
            var iNVOICES_WS_PATTERN = GetValueByName(this.vpiConfig, "INVOICES_WS_PATTERN");
            var iNVOICES_WS_SERIAL = GetValueByName(this.vpiConfig, "INVOICES_WS_SERIAL");

            Dictionary<string, string> data = new Dictionary<string, string>();

            ServiceHDDT.SetConfigs(iNVOICES_URL_IMPORT, iNVOICES_WS_USER_ACC, iNVOICES_WS_PWD_ACC, iNVOICES_WS_USER, iNVOICES_WS_PWD, iNVOICES_WS_PATTERN, iNVOICES_WS_SERIAL);
            var rs = ServiceHDDT.ImportAndPublishIny(xmlInvData, "0");

            return rs;
        }


        private void MenuPopupGridDichVuClick_Work(string hlink)
        {
            if (hlink == "XoaDichVu")
            {
                XoaDichVu();
            }
        }

        private void MenuPopupGridBenhNhanClick_Work(string hlink)
        {
            if (rLichSuThanhToan.Equals(hlink))
            {
                VPI01T004_lichsuthanhtoan frm = new VPI01T004_lichsuthanhtoan();
                frm.SetData(this.tiepNhanId);
                openForm(frm, "1");
            }
            else if (rThemMaSoThue.Equals(hlink))
            {
                VPI01T001_ThemMST frm = new VPI01T001_ThemMST();
                frm.SetParams(this.tiepNhanId);
                openForm(frm, "1");
            }
            else if (rInBangKe.Equals(hlink))
            {
                btnInPhoi_Click(null, null);
            }
            else if (rLichSuTheoCongBHYT.Equals(hlink))
            {
                var resultThe = RequestHTTP.call_ajaxCALL_SP_O("NTU01H001.EV021", this.tiepNhanId, 0);
                if (resultThe != null && resultThe.Rows.Count > 0)
                {
                    string maThe = GetValueByName(resultThe.Rows[0], "MA_BHYT");
                    string hoTen = GetValueByName(resultThe.Rows[0], "TENBENHNHAN");
                    string ngaySinh = GetValueByName(resultThe.Rows[0], "NGAYSINH");
                    string gioiTinh = GetValueByName(resultThe.Rows[0], "GIOITINHID");
                    string maCSKCB = GetValueByName(resultThe.Rows[0], "MA_KCBBD");
                    string ngayBD = "";
                    string ngayKT = "";

                    NGT02K047_LichSuKCB frm = new NGT02K047_LichSuKCB(maThe, hoTen, ngaySinh, gioiTinh, maCSKCB, ngayBD, ngayKT);
                    openForm(frm, "1");
                }
            }
            else if (rLichSuDieuTri.Equals(hlink))
            {
                NGT02K025_LichSuDieuTri frm = new NGT02K025_LichSuDieuTri();
                frm.setParam(_benhNhan["BENHNHANID"].ToString());
                openForm(frm, "1");
            }
            else if (rHenKham.Equals(hlink))
            {
                btnHenKham_Click(null, null);
            }
            else if (rDuyetThucHienCLS.Equals(hlink))
            {

            }
        }

        private string ViewHoaDon(string _phieuThuId, string INVOICES_URL_VIEW, string INVOICES_WS_USER, string INVOICES_WS_PWD)
        {
            return "";
        }

        private void InHoaDon(string BHD, string BHP)
        {
            //Them mau khac khi in doi tuong benh nhan bhyt cho bvnt (tuonglt 02082017)
            if ("902".Equals(Const.local_user.HOSPITAL_ID))
            {
                //them loai dich vu cua tung benh nhan benh vien buu dien
                string _loaidichvu = "0";
                string _loaidichvutru = "0";
                var _par_loai = new List<string>() { _phieuthuid };
                var arr_loaidichvu = RequestHTTP.call_ajaxCALL_SP_O("NTU02D075.EV01", string.Join("$", _par_loai), 0);
                string _loaidvbv = "0";

                var arr_loaidichvunt = RequestHTTP.call_ajaxCALL_SP_O("HOADONDVBV", string.Join("$", _par_loai), 0);
                var arr_dvbvntru = RequestHTTP.call_ajaxCALL_SP_O("HOADONDVNTRU", string.Join("$", _par_loai), 0);
                if (!string.IsNullOrWhiteSpace(teMaBHYT.Text))
                {
                    if (arr_loaidichvu != null && arr_loaidichvu.Rows.Count > 0)
                    {
                        for (var i = 0; i < arr_loaidichvu.Rows.Count; i++)
                        {
                            _loaidichvu = arr_loaidichvu.Rows[i]["BHYT"].ToString();
                            if ("1".Equals(_loaidichvu))
                            {
                                if ("0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                                {
                                    InHoadonVP(_phieuthuid, "NGT036_HOADON_A4_NTRU_BHYT_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                }
                                else
                                {
                                    InHoadonVP(_phieuthuid, "NGT036_HOADONGTGT_BHYT_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                }
                            }
                            else
                            {
                                if ("1".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                                {
                                    if (arr_loaidichvunt != null && arr_loaidichvunt.Rows.Count > 0)
                                    {
                                        for (var i1 = 0; i1 < arr_loaidichvunt.Rows.Count; i1++)
                                        {
                                            _loaidvbv = arr_loaidichvunt.Rows[i1]["DVBV"].ToString();
                                            if ("1".Equals(_loaidvbv))
                                            {
                                                InHoadonVP(_phieuthuid, "NGT036_HOADONDVTHUKHAC_A4_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                            }
                                            else
                                            {
                                                InHoadonVP(_phieuthuid, "NGT036_HOADONGTGT_A4_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                            }
                                        }
                                    }
                                }
                                else if ("0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                                {
                                    if (arr_dvbvntru != null && arr_dvbvntru.Rows.Count > 0)
                                    {
                                        for (var i2 = 0; i2 < arr_dvbvntru.Rows.Count; i2++)
                                        {
                                            _loaidichvutru = arr_dvbvntru.Rows[i2]["DVBV"].ToString();
                                            if ("1".Equals(_loaidichvutru))
                                            {
                                                InHoadonVP(_phieuthuid, "NGT036_HOADONDV_NTRU_A4_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                            }
                                            else
                                            {
                                                InHoadonVP(_phieuthuid, "NGT036_HOADON_A4_NTRU_VP_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if ("1".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                    {
                        if (arr_loaidichvunt != null && arr_loaidichvunt.Rows.Count > 0)
                        {
                            for (var i1 = 0; i1 < arr_loaidichvunt.Rows.Count; i1++)
                            {
                                _loaidvbv = arr_loaidichvunt.Rows[i1]["DVBV"].ToString();
                                if ("1".Equals(_loaidvbv))
                                {
                                    InHoadonVP(_phieuthuid, "NGT036_HOADONDVTHUKHAC_A4_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                }
                                else
                                {
                                    InHoadonVP(_phieuthuid, "NGT036_HOADONGTGT_A4_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                }
                            }
                        }
                    }
                    else if ("0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                    {
                        if (arr_dvbvntru != null && arr_dvbvntru.Rows.Count > 0)
                        {
                            for (var i2 = 0; i2 < arr_dvbvntru.Rows.Count; i2++)
                            {
                                _loaidichvutru = arr_dvbvntru.Rows[i2]["DVBV"].ToString();
                                if ("1".Equals(_loaidichvutru))
                                {
                                    InHoadonVP(_phieuthuid, "NGT036_HOADONDV_NTRU_A4_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                }
                                else
                                {
                                    InHoadonVP(_phieuthuid, "NGT036_HOADON_A4_NTRU_VP_902", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                                }
                            }
                        }
                    }
                }
            }
            else if ("965".Equals(Const.local_user.HOSPITAL_ID))
            {
                //them loai dich vu cua tung benh nhan benh vien buu dien
                string _loaidichvu = "0";
                var _par_loai = new List<string>() { _phieuthuid };
                var arr_loaidichvu = RequestHTTP.call_ajaxCALL_SP_O("NTU02D075.EV01", string.Join("$", _par_loai), 0);
                if (arr_loaidichvu != null && arr_loaidichvu.Rows.Count > 0)
                {
                    for (var i = 0; i < arr_loaidichvu.Rows.Count; i++)
                    {
                        _loaidichvu = arr_loaidichvu.Rows[i]["BHYT"].ToString();
                        if ("1".Equals(_loaidichvu))
                        {
                            InHoadonVP(_phieuthuid, "NGT036_HOADONGTGT_BHYT_A4_965", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                        }
                        else
                        {
                            InHoadonVP(_phieuthuid, "NGT036_HOADONGTGT_A4_965", "NGT037_BANGKEKEMHDGTGT_A4", BHP);
                        }
                    }
                }
            }
            else
            {
                InHoadonVP(_phieuthuid, BHD, "NGT037_BANGKEKEMHDGTGT_A4", BHP);
            }
        }

        private void InHoadonVP(string _phieuthuid, string _report_code, string _report_code_chitiet, string _dahuyphieu)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");

            table.Rows.Add("phieuthuid", "String", _phieuthuid);

            string typeExport = "pdf";

            if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_KIEUIN_HOADON")))
            {
                InNhieuPhieu(_report_code, table, typeExport, "ifmBill");
            }
            else
            {
                OpenReport(_report_code_chitiet, table, typeExport, "ifmDetail");
            }

            if ("0".Equals(GetValueByName(this.vpiConfig, "HIS_IN_HOADONCHITIET"))
                || string.IsNullOrEmpty(_report_code_chitiet)
                || ("1".Equals(_dahuyphieu)))
            {
                return;
            }

            if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_KIEUIN_HOADON")))
            {
                InNhieuPhieu(_report_code_chitiet, table, typeExport, "ifmDetail");
            }
            else
            {
                OpenReport(_report_code_chitiet, table, typeExport, "ifmDetail");
            }
        }

        private void InNhieuPhieu(string _report_code, DataTable par, string typeExport, string loai)
        {
            Func.PrintFile_FromData(_report_code, par, typeExport);
        }

        private void OpenReport(string _report_code, DataTable par, string typeExport, string loai)
        {
            PrintPreview("", _report_code, par);
        }

        private void PrintPreview(string title, string code, DataTable parTable, int height = 650, int width = 900)
        {
            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(title, code, parTable, height, width);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        #endregion

        private void btnHenKham_Click(object sender, EventArgs e)
        {
            NGT02K061_QLHenKham_VienPhi frm = new NGT02K061_QLHenKham_VienPhi();
            openForm(frm, "1");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ("-1".Equals(this.tiepNhanId))
            {
                return;
            }

            flagLoading = true;

            this.SetEnabled(null, new List<SimpleButton>() { btnIn, btnLuu, btnHuyBo });

            LoadDataGridDichVu(null);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (hetPhieu2)
            {
                MessageBox.Show("Hết phiếu hóa đơn");
                return;
            }

            if (hetPhieu)
            {
                MessageBox.Show("Hết phiếu Tạm ứng/Thu tiền/Hoàn ứng");
                return;
            }

            SetEnabled(null, new List<SimpleButton> { btnLuu });

            var loaiPhieuThu = ucCboLoaiPhieu.SelectValue;
            var tienChot = Func.Parse_double(GetValueByName(this.vpiConfig, "VIENPHI"))
                - Func.Parse_double(GetValueByName(this.vpiConfig, "DANOP"))
                - Func.Parse_double(GetValueByName(this.vpiConfig, "MIENGIAM"));

            var strTienChot = FormatMoney(tienChot.ToString(), 2);
            if (!_TAMUNG.Equals(loaiPhieuThu)
                && Func.Parse_double(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN")) >= 1
                && this.chot
                && tienHoaDon != tienChot)
            {
                var dialogResult = MessageBox.Show("Số tiền trên hóa đơn không khớp với các phiếu thu khác, bạn có chắc chắn tiếp tục ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.PopupWServiceInit(loaiPhieuThu);
                }
            }
            else
            {
                this.PopupWServiceInit(loaiPhieuThu);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            ucGridDichVu.setMultiSelectMode(false);
            ucCboMaSo.SelectValue = "";
            this.SetTTThuTien(null);
            this.KiemTraBenhNhan();
            ucCboLoaiPhieu.SelectValue = "1";
            ucCboLoaiTT.SelectValue = "1";
            this.SetEnabled(new List<SimpleButton>() { btnThem }, new List<SimpleButton>() { btnHuyBo, btnLuu });
            this.ReadOnly = true;
            ucCboMaSo.lookUpEdit.ReadOnly = true;
            flagLoading = false;

            this.LoadDataGridDichVu(null);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            var _is_dct = RequestHTTP.getOneValue("VPI01T001.18"
                , new string[] { "[0]", "[1]" }
                , new string[] { _phieuthuid, _phieuthuid });

            string fl = "";
            var _selRowId = (DataRowView)ucGridPhieuThu.gridView.GetFocusedRow();
            var _loaiPhieuThu = GetValueByName(_selRowId, "LOAIPHIEUTHUID");
            var _daHuyPhieu = GetValueByName(_selRowId, "DAHUYPHIEU");

            if (_THUTIEN.Equals(_loaiPhieuThu))
            {
                if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_SOLAN_INHOADON")))
                {
                    fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.CHECKPT", _phieuthuid);
                    if ("1".Equals(fl))
                    {
                        MessageBox.Show("Hóa đơn đã được in, không thể in lại");
                        return;
                    }
                }

                var _rp_code = "NGT036_HOADONGTGT_A4";
                if ("1".Equals(GetValueByName(this.vpiConfig, "VP_TACH_HOADON")) && "1".Equals(_is_dct))
                {
                    _rp_code = "NGT039_HOADONDCT_A4";
                }

                InHoaDon(_rp_code, _daHuyPhieu);
            }
            else if (_THUTHEM.Equals(loaiPhieuThu))
            {
                InHoadonVP(_phieuthuid, "NGT041_PHIEUTHUTIEN_A4", "", "");
            }
            else if (_TAMUNG.Equals(loaiPhieuThu))
            {
                if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_SOLAN_INTAMUNG")))
                {
                    fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.CHECKPT", _phieuthuid);
                    if ("1".Equals(fl))
                    {
                        MessageBox.Show("Phiếu thu đã được in, không thể in lại");
                        return;
                    }
                }

                InHoadonVP(_phieuthuid, "NGT034_PHIEUTAMUNG_A4", "", "");
            }
            else if (_HOANUNG.Equals(loaiPhieuThu))
            {
                InHoadonVP(_phieuthuid, "NGT038_PHIEUHOANUNG_A4", "", "");
            }

            fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T001.IN", _phieuthuid);
            if ("-1".Equals(fl))
            {
                MessageBox.Show("Cập nhật trạng thái phiếu thu không thành công");
            }

            if ("0".Equals(GetValueByName(this.vpiConfig, "HIS_FOCUS_MABN")))
            {

            }
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {
            if (flagLoading)
            {
                return;
            }

            string loaiThanhToan = ucCboLoaiTT.SelectValue;

            VPI01T001_Xacnhan frm = new VPI01T001_Xacnhan();
            frm.SetParams(this._phieuthuid, loaiThanhToan);
            openForm(frm, "1");
            if (frm.dialogResult == DialogResult.OK)
            {
                this.SetTTThuTien(null);
                this.KiemTraBenhNhan();
                ucCboLoaiPhieu.SelectValue = "1";
                ucCboLoaiTT.SelectValue = "1";
                this.SetEnabled(new List<SimpleButton>() { btnThem }, new List<SimpleButton>() { btnLuu, btnHuyPhieu });
                this.ReadOnly = true;
                this.LoadDataGridPhieuThu(null);
                this.LoadDataGridDichVu(null);

                if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_DAY_HOADONDT")))
                {
                    var INVOICES_URL_CANCEL = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_URL_CANCEL");
                    var INVOICES_WS_USER = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_USER");
                    var INVOICES_WS_PWD = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_PWD");
                    var INVOICES_WS_USER_ACC = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_USER_ACC");
                    var INVOICES_WS_PWD_ACC = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "INVOICES_WS_PWD_ACC");

                    // code gọi hóa đơn điện tử
                    //   ret = ajaxSvc.InvoicesWS.cancelHoaDon(_phieuthuid,INVOICES_URL_CANCEL,INVOICES_WS_USER_ACC, INVOICES_WS_PWD_ACC,
                    //INVOICES_WS_USER, INVOICES_WS_PWD);

                    RequestHTTP.get_ajaxExecuteQuery("VPI.DAYHDDT.UPDATE"
                        , new string[] { "[0]", "[1]" }
                        , new string[] { _phieuthuid, "0" });
                }
            }
        }

        private string GetXmlTo_Insr(DataTable dt)
        {
            return string.Empty;
        }

        private string GetXmlTo_Medical(DataTable dt)
        {
            return string.Empty;
        }

        private void btnDuyetKeToan_Click(object sender, EventArgs e)
        {
            if (flagLoading)
            {
                return;
            }

            string _khoa = string.Join(", ", listKhoa);

            if ("1".Equals(GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"))
                && "0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID"))
                && !"2".Equals(GetValueByName(this._benhNhan, "TRANGTHAITIEPNHAN"))
                && "1".Equals(GetValueByName(this.vpiConfig, "VPI_DUYETBH_THANHTOANNTU")))
            {
                MessageBox.Show("Không thể duyệt kế toán cho bệnh nhân nội trú chưa duyệt bảo hiểm");
                return;
            }

            string objData_DUYET = "";
            objData_DUYET += Func.json_item("TIEPNHANID", this.tiepNhanId);
            objData_DUYET += Func.json_item("NGAY", Func.getSysDatetime("dd/MM/yyyy HH:mm:ss"));
            objData_DUYET += Func.json_item_num("DATRONVIEN", 0);
            objData_DUYET += Func.json_item_num("SOLUONGQUYETTOAN", 0);
            objData_DUYET += Func.json_item_num("LOAIDUYETBHYT", 0);

            if (_flag_duyet)
            {
                objData_DUYET += Func.json_item_num("DUYET", 1);
                if ("0".Equals(GetValueByName(this.vpiConfig, "VPI_KIEMTRA_TYLE")))
                {
                    if (listKhoa != null && listKhoa.Count > 0)
                    {
                        MessageBox.Show("Có dịch vụ sai tỷ lệ tại " + _khoa);
                        return;
                    }

                    if (!"0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID"))
                        && "3".Equals(GetValueByName(this._benhNhan, "LYDO_VAOVIEN"))
                        && Func.Parse_double(GetValueByName(this._benhNhan, "MUCHUONG")) > 0)
                    {
                        MessageBox.Show("Bệnh nhân ngoại trú trái tuyến đang có mức hưởng , xem lại thông tin hành chính");
                        return;
                    }
                }
            }
            else
            {
                //kiem tra benh nhan ngoai tru da xuat thuoc thi ko cho go duyet
                var check = RequestHTTP.call_ajaxCALL_SP_I("VPI.CHECKGODUYETKT", this.tiepNhanId);
                if ("1".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")) && Func.Parse_double(check) > 0)
                {
                    MessageBox.Show("Bệnh nhân đã xuất thuốc ngoại trú, không thể gỡ duyệt");
                    return;
                }

                objData_DUYET += Func.json_item_num("DUYET", 0);
            }

            if ("1".Equals(GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"))
                    && ("0".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN"))
                            || ("2".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN")) && !"0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                            || ("3".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN")) && "0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                        )
               )
            {
                objData_DUYET += Func.json_item_num("FLAG_DUYET_BH", 0);

                string ngay = Func.getSysDatetime("dd/MM/yyyy HH:mm:ss");
                string obj_BH = "";
                obj_BH += Func.json_item("TIEPNHANID", this.tiepNhanId);
                obj_BH += Func.json_item("NGAYDUYET", ngay);

                var month = Func.Parse_double(ngay.Split(new string[] { "/" }, StringSplitOptions.None)[1]);
                if (month <= 3)
                {
                    obj_BH += Func.json_item("QUYDUYET", "1");
                }
                else if (month <= 6)
                {
                    obj_BH += Func.json_item("QUYDUYET", "2");
                }
                else if (month <= 9)
                {
                    obj_BH += Func.json_item("QUYDUYET", "3");
                }
                else
                {
                    obj_BH += Func.json_item("QUYDUYET", "4");
                }

                obj_BH += Func.json_item("HOSPITAL_CODE", Const.local_user.HOSPITAL_CODE);

                obj_BH = Func.json_item_end(obj_BH);

                objData_DUYET += Func.json_item_num("DATA_BH", obj_BH);
            }
            else
            {
                objData_DUYET += Func.json_item_num("FLAG_DUYET_BH", 1);
            }


            objData_DUYET = Func.json_item_end(objData_DUYET);
            objData_DUYET = objData_DUYET.Replace("\"", "\\\"");

            var _ngayhientai = Func.getSysDatetime("dd/MM/yyyy");
            var fl = RequestHTTP.call_ajaxCALL_SP_S_result("VPI01T001.08", objData_DUYET);

            if (fl == "-1")
            {
                MessageBox.Show("Cập nhật không thành công");
            }
            else if (fl == "-96")
            {
                MessageBox.Show("Còn khoa/phòng chưa kết thúc");
            }
            else if (fl == "-97")
            {
                MessageBox.Show("Còn phòng khám chưa kết thúc");
            }
            else if (fl == "-98")
            {
                MessageBox.Show("Bệnh nhân chưa thanh toán viện phí");
            }
            else if (fl == "-99")
            {
                MessageBox.Show("Dữ liệu đã cũ, hãy chọn lại bệnh nhân");
            } 
            else
            {
                var drBenhNhan = (DataRowView)ucGridBenhNhan.gridView.GetFocusedRow();
                if (_flag_duyet)
                {
                    _flag_duyet = false;
                    btnDuyetKeToan.Text = "Gỡ duyệt kế toán";
                    this._benhNhan["TRANGTHAITIEPNHAN_VP"] = 1;
                    this._benhNhan["TRANGTHAITIEPNHAN_BH"] = 1;

                    drBenhNhan["ICON1"] = "4";
                    ucGridBenhNhan.gridView.RefreshData();

                    if ("1".Equals(GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"))
                            && ("0".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN"))
                                    || ("2".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN")) && !"0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                                    || ("3".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN")) && "0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))))
                    {
                        if ("0".Equals(fl))
                        {
                            MessageBox.Show("Hồ sơ của bệnh nhân đã khóa, không thể duyệt bảo hiểm");
                            return;
                        }
                        else if ("2".Equals(fl))
                        {
                            MessageBox.Show("Bệnh nhân không có tiền đề nghị thanh toán, không thể duyệt");
                            return;
                        }
                        else if (fl != "1")
                        {
                            MessageBox.Show(fl);
                            return;
                        }

                        this._benhNhan["TRANGTHAITIEPNHAN"] = 2;
                        drBenhNhan["ICON2"] = "2";
                        ucGridBenhNhan.gridView.RefreshData();

                        var _ngay_ra = GetValueByName(this._benhNhan, "NGAY_RA_VIEN");
                        if (_ngay_ra != _ngayhientai)
                        {
                            MessageBox.Show("Hồ sơ mã " + GetValueByName(this._benhNhan, "MAHOSOBENHAN") + " có ngày ra viện khác ngày thanh toán. Yêu cầu: BỔ SUNG HỒ SƠ VÀO NGÀY " + _ngay_ra);
                        }

                        var _ret_bhxh = "";
                        var _ret_byt = "";
                        DataTable data_bh = null;
                        DataTable data_byt = null;
                        var _ngayravien = GetValueByName(this._benhNhan, "NGAYRAVIEN");
                        if (!"0".Equals(GetValueByName(this.vpiConfig, "VP_GUI_DULIEU_KHIDUYET")))
                        {
                            if ("1".Equals(GetValueByName(this.vpiConfig, "VPI_GUI_BH")))
                            {
                                data_bh = RequestHTTP.call_ajaxCALL_SP_O("XML.4210", 1 + "$" + _ngayravien + "$" + _ngayravien + "$" + 1 + "$" + this.tiepNhanId, 0);
                                data_byt = data_bh;
                            }
                            else
                            {
                                data_bh = RequestHTTP.call_ajaxCALL_SP_O("VPI02G001.03", this.tiepNhanId + "$" + _ngayravien + "$" + _ngayravien, 0);
                                data_byt = RequestHTTP.call_ajaxCALL_SP_O("BH01.XML01", _ngayravien + "$" + _ngayravien + "$" + this.tiepNhanId, 0);
                            }
                        }


                        if ("1".Equals(GetValueByName(this.vpiConfig, "VP_GUI_DULIEU_KHIDUYET")))
                        {
                            _ret_bhxh = GetXmlTo_Insr(data_bh);
                            _ret_byt = GetXmlTo_Medical(data_byt);

                        }
                        else if ("2".Equals(GetValueByName(this.vpiConfig, "VP_GUI_DULIEU_KHIDUYET")))
                        {
                            _ret_bhxh = GetXmlTo_Insr(data_bh);
                        }
                        else if ("3".Equals(GetValueByName(this.vpiConfig, "VP_GUI_DULIEU_KHIDUYET")))
                        {
                            _ret_byt = GetXmlTo_Medical(data_byt);
                        }


                        if (_ret_bhxh != "" || _ret_byt != "")
                        {
                            var ret = RequestHTTP.call_ajaxCALL_SP_I("NTU02D061.02", this.tiepNhanId + "$" + _ret_bhxh + "$" + _ret_byt);
                            if ("-1".Equals(ret))
                            {
                                MessageBox.Show("Cập nhật trạng thái không thành công");
                            }
                        }
                    }
                    else if (GetValueByName(this._benhNhan, "DOITUONGBENHNHANID") == "2")
                    {
                        var _ret_byt = "";
                        DataTable data_byt = null;
                        var _ngayravien = GetValueByName(this._benhNhan, "NGAYRAVIEN");
                        if (true)
                        {
                            data_byt = RequestHTTP.call_ajaxCALL_SP_O("XML.VP", 1 + "$" + _ngayravien + "$" + _ngayravien + "$" + 1 + "$" + this.tiepNhanId, 0);
                            _ret_byt = GetXmlTo_Medical(data_byt);
                        }
                        if (_ret_byt != "")
                        {
                            var ret = RequestHTTP.call_ajaxCALL_SP_I("NTU02D061.02.VP", this.tiepNhanId + "$" + "-1" + "$" + _ret_byt);
                            if (ret == "-1")
                            {
                                MessageBox.Show("Cập nhật trạng thái không thành công");
                            }
                        }
                    }

                    this.SetEnabled(null, new List<SimpleButton>() { btnThem, btnHuyPhieu });

                    if ("1".Equals(GetValueByName(this.vpiConfig, "HIS_FOCUS_MABN")))
                    {
                    }
                }
                else
                {
                    _flag_duyet = true;
                    btnDuyetKeToan.Text = "Duyệt kế toán";
                    this._benhNhan["TRANGTHAITIEPNHAN_VP"] = 0;
                    this._benhNhan["TRANGTHAITIEPNHAN_BH"] = 0;

                    drBenhNhan["ICON1"] = "3";
                    ucGridBenhNhan.gridView.RefreshData();

                    this.SetEnabled(new List<SimpleButton>() { btnThem }, null);


                    if ("0".Equals(fl))
                    {
                        MessageBox.Show("Hồ sơ của bệnh nhân đã khóa, không thể gỡ duyệt bảo hiểm");
                        return;
                    }

                    if ("1".Equals(GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"))
                            && ("0".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN"))
                                    || ("2".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN")) && !"0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))
                                    || ("3".Equals(GetValueByName(this.vpiConfig, "VP_DUYET_BH_KHI_DUYET_KETOAN")) && "0".Equals(GetValueByName(this._benhNhan, "LOAITIEPNHANID")))))
                    {
                        if (fl == "0")
                        {
                            MessageBox.Show("Hồ sơ của bệnh nhân đã khóa, không thể gỡ duyệt bảo hiểm");
                            return;
                        }
                        else if (fl == "-2")
                        {
                            MessageBox.Show("Hết thời gian xử lý bảo hiểm của hồ sơ này, liên hệ với người quản trị");
                            return;
                        }
                        else if (fl == "-3")
                        {
                            MessageBox.Show("Bạn không có quyền gỡ duyệt bảo hiểm hồ sơ này");
                            return;
                        }
                        else if (fl == "-4")
                        {
                            MessageBox.Show("Hồ sơ chưa được duyệt bảo hiểm, không thể gỡ duyệt bảo hiểm");
                            return;
                        }
                        else if (fl != "1")
                        {
                            MessageBox.Show(fl);
                            return;
                        }

                        this._benhNhan["TRANGTHAITIEPNHAN"] = 1;
                        drBenhNhan["ICON2"] = "";
                        ucGridBenhNhan.gridView.RefreshData();

                    }
                }
            }
        }
        private void btnInPhoi_Click(object sender, EventArgs e)
        {
            InBangKe(this.tiepNhanId, GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"), GetValueByName(this._benhNhan, "LOAITIEPNHANID"));
        }

        private void btnThuKhac_Click(object sender, EventArgs e)
        {
            if (flagLoading)
            {
                return;
            }

            var selRowIds = ucGridDichVu.gridView.GetSelectedRows();
            if (selRowIds == null || selRowIds.Count() == 0)
            {
                return;
            }

            var selRowId = selRowIds[0];
            var drDichVu = (DataRowView)ucGridDichVu.gridView.GetRow(selRowId);

            var _khoaid = GetValueByName(drDichVu, "KHOAID");
            var _phongid = GetValueByName(drDichVu, "PHONGID");
            var _khambenhid = GetValueByName(drDichVu, "KHAMBENHID");
            var _dichvukhambenhid = GetValueByName(drDichVu, "DICHVUKHAMBENHID");
            var _tiepnhanid_dv = GetValueByName(drDichVu, "TIEPNHANID");
            if (!_tiepnhanid_dv.Equals(GetValueByName(this._benhNhan, "TIEPNHANID")))
            {
                MessageBox.Show("Chọn lại dịch vụ để chỉ định thu khác");
                return;
            }

            var paramInput = new
            {
                chidinhdichvu = "1",
                loaidichvu = "1",
                loaiphieumbp = "17",
                benhnhanid = GetValueByName(this._benhNhan, "BENHNHANID"),
                khambenhid = _khambenhid,
                hosobenhanid = GetValueByName(this._benhNhan, "HOSOBENHANID"),
                tiepnhanid = GetValueByName(this._benhNhan, "TIEPNHANID"),
                doituongbenhnhanid = GetValueByName(this._benhNhan, "DOITUONGBENHNHANID"),
                loaitiepnhanid = GetValueByName(this._benhNhan, "LOAITIEPNHANID"),
                subDeptId = _phongid,
                deptId = _khoaid,
                dichvuidKhac = _dichvukhambenhid
            };

            var parJson = JsonConvert.SerializeObject(paramInput).Replace("\"", "\\\"");
            var checkjson = RequestHTTP.call_ajaxCALL_SP_I("NTU.LOGS", parJson);
        }

        private void teSoTien_KeyUp(object sender, KeyEventArgs e)
        {
            var n = Func.Parse_double(teSoTien.Text);
            if (!IsNumber(n.ToString()))
            {
                teSoTien.SelectAll();
                teSoTien.Focus();
            }

            var _mg = Func.Parse_double(teMienGiam.Text);
            var thucThu = n - _mg;
            SetText(teThucThu, FormatMoney(thucThu.ToString(), 0));
            var _loaiPhieuThu = ucCboLoaiPhieu.SelectValue;

            if (_THUTIEN.Equals(_loaiPhieuThu))
            {
                SetText(teDaNop, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "DANOP")) + thucThu).ToString(), 2));
                SetText(teNopThem, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "NOPTHEM")) - thucThu).ToString(), 2));
            }
            else if (_TAMUNG.Equals(_loaiPhieuThu))
            {
                SetText(teTamUng, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "TAMUNG")) + thucThu).ToString(), 2));
                SetText(teNopThem, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "NOPTHEM")) - thucThu).ToString(), 2));
            }
            else if (_HOANUNG.Equals(_loaiPhieuThu))
            {
                SetText(teHoanUng, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "HOANUNG")) + thucThu).ToString(), 2));
                SetText(teNopThem, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "NOPTHEM")) + thucThu).ToString(), 2));
            }
            else
            {
                SetText(teDaNop, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "DANOP")) + thucThu).ToString(), 2));
                SetText(teNopThem, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "NOPTHEM")) - thucThu).ToString(), 2));
            }

            AddOb(objData, "TONGTIEN", thucThu.ToString());
        }

        private void teMienGiam_KeyUp(object sender, KeyEventArgs e)
        {
            var mienGiam = Func.Parse_double(teMienGiam.Text);
            if (!IsNumber(mienGiam.ToString()))
            {
                teMienGiam.SelectAll();
                teMienGiam.Focus();
            }

            var _loaiPhieuThu = ucCboLoaiPhieu.SelectValue;
            var soTien = Func.Parse_double(teSoTien.Text);
            if (_HOANUNG.Equals(_loaiPhieuThu))
            {
                SetText(teThucThu, FormatMoney((soTien + mienGiam).ToString(), 0));
                SetText(teDaNop, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "HOANUNG")) + soTien).ToString(), 2));
            }
            else
            {
                var thucThu = soTien - mienGiam;
                SetText(teThucThu, FormatMoney(thucThu.ToString(), 0));
                SetText(teDaNop, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "DANOP")) + thucThu).ToString(), 2));
            }

            SetText(teMienGiam, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "MIENGIAM")) + mienGiam).ToString(), 2));

            //var _sotien = parseFloat(_tien_hoadon);
            //var _tlmg = get_val_m('txtTLMIENGIAM');
            //if (parseFloat(_tlmg) > 100 || parseFloat (_tlmg) < 0)
            //{
            //    val_m('txtMIENGIAM_PT', 0);
            //    val_m('txtTLMIENGIAM', 0);
            //    return false;
            //}
            //var _miengiam = _sotien * _tlmg / 100;
            //_miengiam = !_miengiam ? 0 : _miengiam;
            //val_m('txtMIENGIAM_PT', _miengiam);
            //var _loaiphieuthu = $("#cboLOAIPHIEUTHUID").val();
            //if (_loaiphieuthu == _HOANUNG)
            //{
            //    val_m('txtTHUCTHU', (parseFloat(get_val_m('txtTONGTIEN')) + _miengiam).toFixed(2));
            //    val_m('txtHOANUNG', (parseFloat(_vpData.HOANUNG) + parseFloat(get_val_m('txtTHUCTHU'))).toFixed(2));
            //}
            //else
            //{
            //    var _sotien = parseFloat(get_val_m('txtTONGTIEN'));
            //    var _thucthu = _sotien - _miengiam; _thucthu = _thucthu.toFixed(2);
            //    val_m('txtTHUCTHU', _thucthu);
            //    val_m('txtDANOP', (parseFloat(_vpData.DANOP) + parseFloat(get_val_m('txtTHUCTHU'))).toFixed(2));
            //}
            //val_m('txtMIENGIAM', (parseFloat(_vpData.MIENGIAM) + _miengiam).toFixed(2));

        }

        private void teMienGiam_EditValueChanged(object sender, EventArgs e)
        {
            var _loaiphieuthu = ucCboLoaiPhieu.SelectValue;
            var _miengiam = Func.Parse_double(teMienGiam.Text);
            
            var _sotien = Func.Parse_double(teSoTien.Text);
            if (_loaiphieuthu == _HOANUNG)
            {
                var _thucthu = _sotien + _miengiam;
                if (_thucthu <= 0)
                {
                    ucCboLoaiPhieu.SelectValue = _THUTHEM; 
					//$("#cboLOAIPHIEUTHUID").change();
                    return;
                }
                SetText(teThucThu, FormatMoney((_sotien + _miengiam).ToString(), 2));
                SetText(teHoanUng, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "HOANUNG")) + Func.Parse_double(teThucThu.Text)).ToString(), 2));

                //val_m('txtTHUCTHU', (parseFloat(get_val_m('txtTONGTIEN')) + _miengiam).toFixed(2));
                //val_m('txtHOANUNG', (parseFloat(_vpData.HOANUNG) + parseFloat(get_val_m('txtTHUCTHU'))).toFixed(2));
            }
            else
            {
                var _thucthu = _sotien - _miengiam;
                if (_thucthu < 0)
                {
                    ucCboLoaiPhieu.SelectValue = _HOANUNG; 
					//$("#cboLOAIPHIEUTHUID").change();
                    return;
                }

                SetText(teThucThu, FormatMoney(_thucthu.ToString(), 2));
                SetText(teDaNop, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "DANOP")) + Func.Parse_double(teThucThu.Text)).ToString(), 2));

                //val_m('txtTHUCTHU', _thucthu);
                //val_m('txtDANOP', (parseFloat(_vpData.DANOP) + parseFloat(get_val_m('txtTHUCTHU'))).toFixed(2));
            }

            SetText(teMienGiam, FormatMoney((Func.Parse_double(GetValueByName(this.drVpData, "MIENGIAM")) + _miengiam).ToString(), 2));
            //val_m('txtMIENGIAM', (parseFloat(_vpData.MIENGIAM) + _miengiam).toFixed(2));
        }

        private void teSoTien_Leave(object sender, EventArgs e)
        {
            teSoTien.Text = FormatMoney(teSoTien.Text, 0);
        }

        private void VPI01T001_thuvienphi_Shown(object sender, EventArgs e)
        {
            ucGridBenhNhan.setEvent(PageLoad_ucGridBenhNhan);
            this.LoadDataGridBenhNhan(null);
        }

    }
}