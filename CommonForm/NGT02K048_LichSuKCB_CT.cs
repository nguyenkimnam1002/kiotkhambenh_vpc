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
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K048_LichSuKCB_CT : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                  log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K048_LichSuKCB_CT()
        {
            InitializeComponent();
        }

        #region KHỞI TẠO GIÁ TRỊ
        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        string maHoSo = string.Empty;
        public void setParam(string maHoSo)
        {
            this.maHoSo = maHoSo;
        }
        #endregion

        #region LOAD DỮ LIỆU
        /// <summary>
        /// Load dữ liệu
        /// </summary>
        private void Init_Form()
        {
            //ucGridview Danh sách thông tin khám bệnh
            ucgview_TTKBN.gridView.OptionsView.ColumnAutoWidth = false;
            ucgview_TTKBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucgview_TTKBN.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
            ucgview_TTKBN.gridView.OptionsBehavior.Editable = false;

            ucgview_TTKBN.setEvent(ucgview_TTKBN_Load);
            ucgview_TTKBN.SetReLoadWhenFilter(true);
            ucgview_TTKBN.gridView.Click += ucgview_TTKBN_Click;
            ucgview_TTKBN.gridView.ColumnFilterChanged += ucgview_TTKBN_ColumnFilterChanged;

            ucgview_TTKBN.setNumberPerPage(new int[] { 5, 20, 50 });
            ucgview_TTKBN.onIndicator();

            //ucGridview Danh sách Thuốc chi tiết
            ucgview_DSTCT.gridView.OptionsView.ColumnAutoWidth = false;
            ucgview_DSTCT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucgview_DSTCT.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
            ucgview_DSTCT.gridView.OptionsBehavior.Editable = false;

            ucgview_DSTCT.setEvent(ucgview_DSTCT_Load);
            ucgview_DSTCT.SetReLoadWhenFilter(true);
            ucgview_DSTCT.gridView.Click += ucgview_DSTCT_Click;
            ucgview_DSTCT.gridView.ColumnFilterChanged += ucgview_DSTCT_ColumnFilterChanged;

            ucgview_DSTCT.setNumberPerPage(new int[] { 20, 50, 100 });
            ucgview_DSTCT.onIndicator();
        }

        private void loadDataField()
        {
            try
            {
                //if (string.IsNullOrEmpty(Const.ServiceBHYT_Username))
                //{
                //    MessageBox.Show("Lỗi lấy thông tin cần thiết để tạo dữ liệu. Xin xem lại kết nối. ");
                //    return;
                //}

                DataTable dt = new DataTable();
                wsBHYT_LichSu_respons_Detail1 LSCT = ServiceBHYT.Get_HistoryDetail1(maHoSo);

                if (LSCT == null)
                {
                    MessageBox.Show("Không có dữ liệu lịch sử KCBCT. Yêu cầu kiểm tra thông tin đầu vào.");
                    ucgview_TTKBN.clearData();
                    ucgview_DSTCT.clearData();
                    return;
                }
                else
                {
                    etext_HoTen.Text = LSCT.hoSoKCB.xml1.HoTen;
                    etext_ChaMeNguoiGH.Text = LSCT.hoSoKCB.xml1.TenChame;
                    etext_CanNang.Text = LSCT.hoSoKCB.xml1.CanNang;
                    etext_MaBN.Text = LSCT.hoSoKCB.xml1.MaBn;
                    etext_MaKhoa.Text = LSCT.hoSoKCB.xml1.MaKhoa;
                    ecbox_TTRaVien.SelectedIndex = int.Parse(LSCT.hoSoKCB.xml1.TinhTrangRv) - 1;
                    etext_MaBenh.Text = LSCT.hoSoKCB.xml1.MaBenh;
                    etext_TenBenh.Text = LSCT.hoSoKCB.xml1.TenBenh;
                    etext_MaBenhKhac.Text = LSCT.hoSoKCB.xml1.MaBenhkhac;
                    etext_MaPTTTQT.Text = LSCT.hoSoKCB.xml1.MaPtttQt;
                    etext_MaTNTT.Text = LSCT.hoSoKCB.xml1.MaTaiNan;
                    etext_SoNgayDTri.Text = LSCT.hoSoKCB.xml1.SoNgayDtri;
                    edate_NgayVao.Text = string.IsNullOrEmpty(LSCT.hoSoKCB.xml1.NgayVao) ? "" : LSCT.hoSoKCB.xml1.NgayVao.Substring(6, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.NgayVao.Substring(4, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.NgayVao.Substring(0, 4)
                                                        + " " + LSCT.hoSoKCB.xml1.NgayVao.Substring(8, 2)
                                                        + ":" + LSCT.hoSoKCB.xml1.NgayVao.Substring(10, 2);
                    edate_NgayRa.Text = string.IsNullOrEmpty(LSCT.hoSoKCB.xml1.NgayRa) ? "" : LSCT.hoSoKCB.xml1.NgayRa.Substring(6, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.NgayRa.Substring(4, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.NgayRa.Substring(0, 4)
                                                        + " " + LSCT.hoSoKCB.xml1.NgayRa.Substring(8, 2)
                                                        + ":" + LSCT.hoSoKCB.xml1.NgayRa.Substring(10, 2);
                    edate_NgayNhap.Text = string.IsNullOrEmpty(LSCT.hoSoKCB.xml1.Ngaynhap) ? "" : LSCT.hoSoKCB.xml1.Ngaynhap.Substring(6, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.Ngaynhap.Substring(4, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.Ngaynhap.Substring(0, 4)
                                                        + " " + LSCT.hoSoKCB.xml1.Ngaynhap.Substring(8, 2)
                                                        + ":" + LSCT.hoSoKCB.xml1.Ngaynhap.Substring(10, 2);
                    edate_NgayTT.Text = string.IsNullOrEmpty(LSCT.hoSoKCB.xml1.Ngaythanhtoan) ? "" : LSCT.hoSoKCB.xml1.Ngaythanhtoan.Substring(6, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.Ngaythanhtoan.Substring(4, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.Ngaythanhtoan.Substring(0, 4)
                                                        + " " + LSCT.hoSoKCB.xml1.Ngaythanhtoan.Substring(8, 2)
                                                        + ":" + LSCT.hoSoKCB.xml1.Ngaythanhtoan.Substring(10, 2);

                    etext_MaThe.Text = LSCT.hoSoKCB.xml1.MaThe;
                    edate_TuNgay.Text = string.IsNullOrEmpty(LSCT.hoSoKCB.xml1.GtTheTu) ? "" : LSCT.hoSoKCB.xml1.GtTheTu.Substring(6, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.GtTheTu.Substring(4, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.GtTheTu.Substring(0, 4);
                    edate_DenNgay.Text = string.IsNullOrEmpty(LSCT.hoSoKCB.xml1.GtTheDen) ? "" : LSCT.hoSoKCB.xml1.GtTheDen.Substring(6, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.GtTheDen.Substring(4, 2)
                                                        + "/" + LSCT.hoSoKCB.xml1.GtTheDen.Substring(0, 4);
                    etext_MucHuong.Text = LSCT.hoSoKCB.xml1.MucHuong + "%";

                    etext_TongTT.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.TTongchi)) + " VNĐ";
                    etext_BHTra.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.TBhtt)) + " VNĐ";
                    etext_BNTra.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.TBntt)) + " VNĐ";
                    etext_NguonKhac.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.TNguonkhac)) + " VNĐ";
                    etext_NgoaiDS.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.TNgoaids)) + " VNĐ";
                    etext_TienThuoc.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.TThuoc)) + " VNĐ";
                    etext_TienKham.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.Tienkham)) + " VNĐ";
                    etext_TienVTYT.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.TVtyt)) + " VNĐ";
                    etext_TienGiuong.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.Tiengiuong)) + " VNĐ";
                    etext_VanChuyen.Text = String.Format("{0:0,0.00}", double.Parse(LSCT.hoSoKCB.xml1.Tienvanchuyen)) + " VNĐ";

                    Load_DataGridTTKBN(1, LSCT);
                    Load_DataGridDSTCT(1, LSCT);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông tin đăng nhập chưa đúng hoặc không thể lấy dữ liệu từ cổng.");
                log.Fatal(ex.Message);
            }
        }

        private void Load_DataGridTTKBN(int pageNum, wsBHYT_LichSu_respons_Detail1 LSCT)
        {
            try
            {
                if (pageNum <= 0)
                {
                    pageNum = 1;
                }

                ResponsList responses = new ResponsList();

                //string jsonFilter = "";
                //if (ucgview_TTKBN.ReLoadWhenFilter && ucgview_TTKBN.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_TTKBN.tableFlterColumn);
                //}

                DataTable dt = new DataTable();
                dt = LSCT.hoSoKCB.dsXml3.ConvertListToDataTable<wsBHYT_LichSu_respons_Detail1_dsXml3>();

                if (dt.Rows.Count == 0)
                    dt = Func.getTableEmpty(new String[] { "MaDichVu", "TenDichVu", "DonViTinh", "SoLuong", "ThanhTien" });
                ucgview_TTKBN.setData(dt, responses.total, responses.page, responses.records);
                ucgview_TTKBN.setColumnAll(false);

                ucgview_TTKBN.setColumn("MaDichVu", 0, "Mã dịch vụ");
                ucgview_TTKBN.setColumn("TenDichVu", 1, "Tên dịch vụ");
                ucgview_TTKBN.setColumn("DonViTinh", 2, "Đơn vị tính");
                ucgview_TTKBN.setColumn("SoLuong", 3, "Số lượng");
                ucgview_TTKBN.setColumn("ThanhTien", 4, "Thành tiền");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void Load_DataGridDSTCT(int pageNum, wsBHYT_LichSu_respons_Detail1 LSCT)
        {
            try
            {
                if (pageNum <= 0)
                {
                    pageNum = 1;
                }

                ResponsList responses = new ResponsList();

                //string jsonFilter = "";
                //if (ucgview_DSTCT.ReLoadWhenFilter && ucgview_DSTCT.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSTCT.tableFlterColumn);
                //}

                DataTable dt = new DataTable();
                dt = LSCT.hoSoKCB.dsXml2.ConvertListToDataTable<wsBHYT_LichSu_respons_Detail1_dsXml2>();

                if (dt.Rows.Count == 0)
                    dt = Func.getTableEmpty(new String[] { "MaThuoc", "TenThuoc", "DonViTinh", "HamLuong", "DuongDung", "LieuDung", "ThanhTien" });
                ucgview_DSTCT.setData(dt, responses.total, responses.page, responses.records);
                ucgview_DSTCT.setColumnAll(false);

                ucgview_DSTCT.setColumn("MaThuoc", 0, "Mã thuốc");
                ucgview_DSTCT.setColumn("TenThuoc", 1, "Tên thuốc");
                ucgview_DSTCT.setColumn("DonViTinh", 2, "Đơn vị tính");
                ucgview_DSTCT.setColumn("HamLuong", 3, "Hàm lượng");
                ucgview_DSTCT.setColumn("DuongDung", 4, "Đường dùng");
                ucgview_DSTCT.setColumn("LieuDung", 5, "Liều dùng");
                ucgview_DSTCT.setColumn("ThanhTien", 6, "Thành tiền");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion

        #region XỬ LÝ DỮ LIỆU
        /// <summary>
        /// Xử lý dữ liệu
        /// </summary>
        
        #endregion

        #region SỰ KIỆN TRÊN DESIGN
        /// <summary>
        /// Sự kiện trên design
        /// </summary>
        private void NGT02K048_LichSuKCB_CT_Load(object sender, EventArgs e)
        {
            Init_Form();
            loadDataField();
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region SỰ KIỆN KHỞI TẠO TRÊN CODE
        /// <summary>
        /// Sự kiện khởi tạo trên code
        /// </summary>
        private void ucgview_TTKBN_Load(object sender, EventArgs e)
        {
            
        }

        private void ucgview_TTKBN_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucgview_TTKBN.gridView.FocusedColumn.FieldName))
            {
                int id = ucgview_TTKBN.gridView.FocusedRowHandle;
                if (ucgview_TTKBN.gridView.GetSelectedRows().Any(o => o == id))
                {
                    ucgview_TTKBN.gridView.UnselectRow(id);
                }
                else
                {
                    ucgview_TTKBN.gridView.SelectRow(id);
                }
            }
        }

        private void ucgview_TTKBN_ColumnFilterChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.ActiveEditor is TextEdit)
                {
                    TextEdit textEdit = (TextEdit)view.ActiveEditor;
                    textEdit.Text = textEdit.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ucgview_DSTCT_Load(object sender, EventArgs e)
        {
            
        }

        private void ucgview_DSTCT_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucgview_DSTCT.gridView.FocusedColumn.FieldName))
            {
                int id = ucgview_DSTCT.gridView.FocusedRowHandle;
                if (ucgview_DSTCT.gridView.GetSelectedRows().Any(o => o == id))
                {
                    ucgview_DSTCT.gridView.UnselectRow(id);
                }
                else
                {
                    ucgview_DSTCT.gridView.SelectRow(id);
                }
            }
        }

        private void ucgview_DSTCT_ColumnFilterChanged(object sender, EventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view.ActiveEditor is TextEdit)
                {
                    TextEdit textEdit = (TextEdit)view.ActiveEditor;
                    textEdit.Text = textEdit.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        #endregion
    }
}