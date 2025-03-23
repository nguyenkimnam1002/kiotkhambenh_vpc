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
using DevExpress.XtraGrid.Columns;
using VNPT.HIS.NgoaiTru.Class;
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K051_XoaPhongKham : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K051_XoaPhongKham()
        {
            InitializeComponent();
        }

        #region KHỞI TẠO GIÁ TRỊ
        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        private string benhNhanID = "";
        private string tiepNhanID = "";
        private string hoSoBenhAnID = "";
        private string phongID = "";

        private string phongKhamDangKyID = "";
        private string tenPhong = "";
        private string dichVuID = "";
        private string tenDichVu = "";
        #endregion

        #region LOAD DỮ LIỆU
        /// <summary>
        /// Load dữ liệu
        /// </summary>
        private void Init_Form()
        {
            try
            {
                gview_DSTimKiem.gridView.OptionsView.ColumnAutoWidth = true;
                gview_DSTimKiem.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                gview_DSTimKiem.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                gview_DSTimKiem.gridView.OptionsBehavior.Editable = false;

                gview_DSTimKiem.setEvent(gview_DSTimKiem_Load);
                gview_DSTimKiem.SetReLoadWhenFilter(true);
                gview_DSTimKiem.gridView.Click += gview_DSTimKiem_Click;
                gview_DSTimKiem.gridView.ColumnFilterChanged += gview_DSTimKiem_ColumnFilterChanged;

                gview_DSTimKiem.setNumberPerPage(new int[] { 10, 50, 100 });
                gview_DSTimKiem.onIndicator();


                gview_DSPhongKham.gridView.OptionsView.ColumnAutoWidth = true;
                gview_DSPhongKham.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                gview_DSPhongKham.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                gview_DSPhongKham.gridView.OptionsBehavior.Editable = false;

                gview_DSPhongKham.setEvent(gview_DSPhongKham_Load);
                gview_DSPhongKham.SetReLoadWhenFilter(true);
                gview_DSPhongKham.gridView.Click += gview_DSPhongKham_Click;
                gview_DSPhongKham.gridView.ColumnFilterChanged += gview_DSPhongKham_ColumnFilterChanged;

                gview_DSPhongKham.setNumberPerPage(new int[] { 10, 50, 100 });
                gview_DSPhongKham.onIndicator();


                gview_DSDichVu.gridView.OptionsView.ColumnAutoWidth = true;
                gview_DSDichVu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                gview_DSDichVu.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                gview_DSDichVu.gridView.OptionsBehavior.Editable = false;

                gview_DSDichVu.setEvent(gview_DSDichVu_Load);
                gview_DSDichVu.SetReLoadWhenFilter(true);
                gview_DSDichVu.gridView.Click += gview_DSDichVu_Click;
                gview_DSDichVu.gridView.ColumnFilterChanged += gview_DSDichVu_ColumnFilterChanged;

                gview_DSDichVu.setNumberPerPage(new int[] { 10, 50, 100 });
                gview_DSDichVu.onIndicator();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        
        private void Load_DataGrid_TimKiem(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                string jsonFilter = "";
                ResponsList responses = new ResponsList();

                //if (gview_DSTimKiem.ReLoadWhenFilter && gview_DSTimKiem.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gview_DSTimKiem.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging("NTUD059.DS_TCBN", page, gview_DSTimKiem.ucPage1.getNumberPerPage(),
                        new string[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]", "[9]" },
                        new string[] { "-1", "-1", "-1", "-1", etext_MaBN.Text, etext_HoTen.Text, "", "", "", etext_MaBHYT.Text }, "");

                gview_DSTimKiem.clearData();

                DataTable dt_DSPHT = new DataTable();
                dt_DSPHT = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSPHT.Rows.Count == 0)
                    dt_DSPHT = Func.getTableEmpty(new String[] { "RN", "BENHNHANID", "MAHOSOBENHAN",
                        "HOSOBENHANID", "NGAYVAOVIEN", "NGAYHOSORAVIEN", "CHANDOANRAVIEN", "MABENHNHAN", "TENBENHNHAN",
                        "CSYTID", "MA_BHYT", "NGAYSINH", "DIACHI", "TIEPNHANID", "GIOITINH" });

                gview_DSTimKiem.setData(dt_DSPHT, responses.total, responses.page, responses.records);
                gview_DSTimKiem.setColumnAll(false);
                
                gview_DSTimKiem.setColumn("MAHOSOBENHAN", 1, "Mã BA");
                gview_DSTimKiem.setColumn("MABENHNHAN", 2, "Mã BN");
                gview_DSTimKiem.setColumn("TENBENHNHAN", 3, "Tên BN");
                gview_DSTimKiem.setColumn("GIOITINH", 4, "G.Tính");
                gview_DSTimKiem.setColumn("NNGAYSINH", 5, "N.Sinh");
                gview_DSTimKiem.setColumn("MA_BHYT", 6, "Mã BHYT");
                gview_DSTimKiem.setColumn("NGAYVAOVIEN", 7, "Ngày vào viện");
                gview_DSTimKiem.setColumn("NGAYHOSORAVIEN", 8, "Ngày ra viện");
                gview_DSTimKiem.setColumn("CHANDOANRAVIEN", 9, "Chẩn đoán");
                gview_DSTimKiem.setColumn("DIACHI", 10, "Địa chỉ");

                gview_DSTimKiem.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void Load_DataGrid_PhongKham(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                //string jsonFilter = "";
                ResponsList responses = new ResponsList();

                //if (gview_DSPhongKham.ReLoadWhenFilter && gview_DSPhongKham.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gview_DSPhongKham.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K051.PK", page, gview_DSPhongKham.ucPage1.getNumberPerPage(),
                        new string[] { "[0]", "[1]", "[2]" },
                        new string[] { benhNhanID, tiepNhanID, hoSoBenhAnID }, "");

                gview_DSPhongKham.clearData();

                DataTable dt_DSPHT = new DataTable();
                dt_DSPHT = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSPHT.Rows.Count == 0)
                    dt_DSPHT = Func.getTableEmpty(new String[] { "RN", "PHONGKHAMDANGKYID", "NGAY",
                        "PHONGID", "KHOAID", "TRANGTHAI_STT", "KHAMBENHID", "BENHNHANID", "DICHVUID",
                        "SANG", "TENPHONG", "TENKHOA", "TENDICHVU" });

                gview_DSPhongKham.setData(dt_DSPHT, responses.total, responses.page, responses.records);
                gview_DSPhongKham.setColumnAll(false);
                
                gview_DSPhongKham.setColumn("NGAY", 1, "Ngày khám");
                gview_DSPhongKham.setColumn("TENKHOA", 2, "Khoa");
                gview_DSPhongKham.setColumn("TENPHONG", 3, "Phòng");
                gview_DSPhongKham.setColumn("SANG", 4, "Buổi");
                gview_DSPhongKham.setColumn("TRANGTHAI_STT", 5, "T.thái");

                gview_DSTimKiem.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void Load_DataGrid_DichVu(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                //string jsonFilter = "";
                ResponsList responses = new ResponsList();

                //if (gview_DSDichVu.ReLoadWhenFilter && gview_DSDichVu.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gview_DSDichVu.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K051.DV", page, gview_DSDichVu.ucPage1.getNumberPerPage(),
                        new string[] { "[0]", "[1]", "[2]", "[3]" },
                        new string[] { benhNhanID, tiepNhanID, hoSoBenhAnID, phongID }, "");

                gview_DSDichVu.clearData();

                DataTable dt_DSPHT = new DataTable();
                dt_DSPHT = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSPHT.Rows.Count == 0)
                    dt_DSPHT = Func.getTableEmpty(new String[] { "RN", "DICHVUKHAMBENHID", "MAUBENHPHAMID",
                        "DICHVUID", "TENDICHVU", "NGAYDICHVU", "TIEN_DICHVU", "TIEN_BHYT", "SOLUONG",
                        "TEN_DVT", "THUTIEN" });

                gview_DSDichVu.setData(dt_DSPHT, responses.total, responses.page, responses.records);
                gview_DSDichVu.setColumnAll(false);
                
                gview_DSDichVu.setColumn("TENDICHVU", 1, "Tên dịch vụ");
                gview_DSDichVu.setColumn("NGAYDICHVU", 2, "Ngày");
                gview_DSDichVu.setColumn("TIEN_DICHVU", 3, "Tiền DV");
                gview_DSDichVu.setColumn("TIEN_BHYT", 4, "Tiền BHYT");
                gview_DSDichVu.setColumn("SOLUONG", 5, "SL");
                gview_DSDichVu.setColumn("TEN_DVT", 6, "ĐVT");
                gview_DSDichVu.setColumn("THUTIEN", 7, "T.thái");

                gview_DSTimKiem.gridView.BestFitColumns(true);
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
        private void Set_Data_TimKiem(DataRowView dr_TimKiem)
        {
            DataRow row = (gview_DSTimKiem.gridView.GetRow(gview_DSTimKiem.gridView.FocusedRowHandle) as DataRowView).Row;
            if (row == null) return;

            benhNhanID = row["BENHNHANID"].ToString();
            tiepNhanID = row["TIEPNHANID"].ToString();
            hoSoBenhAnID = row["HOSOBENHANID"].ToString();
            Load_DataGrid_PhongKham(1);
            Load_DataGrid_DichVu(1);

            btn_XoaPK.Enabled = false;
            btn_XoaDV.Enabled = false;
        }

        private void Set_Data_PhongKham(DataRowView dr_PhongKham)
        {
            DataRow row = (gview_DSPhongKham.gridView.GetRow(gview_DSPhongKham.gridView.FocusedRowHandle) as DataRowView).Row;
            if (row == null) return;

            phongID = row["PHONGID"].ToString();

            phongKhamDangKyID = row["PHONGKHAMDANGKYID"].ToString();
            tenPhong = row["TENPHONG"].ToString();
            Load_DataGrid_DichVu(1);

            btn_XoaPK.Enabled = true;
            btn_XoaDV.Enabled = false;
        }

        private void Set_Data_DichVu(DataRowView dr_DichVu)
        {
            DataRow row = (gview_DSDichVu.gridView.GetRow(gview_DSDichVu.gridView.FocusedRowHandle) as DataRowView).Row;
            if (row == null) return;

            dichVuID = row["DICHVUKHAMBENHID"].ToString();
            tenDichVu = row["TENDICHVU"].ToString();

            btn_XoaPK.Enabled = true;
            btn_XoaDV.Enabled = true;
        }
        #endregion

        #region SỰ KIỆN TRÊN DESIGN
        /// <summary>
        /// Sự kiện trên design
        /// </summary>
        private void NGT02K051_XoaPhongKham_Load(object sender, EventArgs e)
        {
            Init_Form();
        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            if (etext_MaBN.Text == "" && etext_MaBHYT.Text == "" && etext_HoTen.Text == "")
            {
                MessageBox.Show("Yêu cầu nhập dữ liệu tìm kiếm bệnh nhân");
                return;
            }
            Load_DataGrid_TimKiem(1);
        }

        private void btn_XoaPK_Click(object sender, EventArgs e)
        {
            if (phongID == "")
            {
                MessageBox.Show("Chưa chọn phòng khám cần xóa");
                return;
            }
            try
            {
                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn xóa phòng " + tenPhong + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string fl = RequestHTTP.call_ajaxCALL_SP_I("NGT02K051.XOAPK", phongKhamDangKyID);
                    if (fl == "1")
                    {
                        Load_DataGrid_PhongKham(1);
                        dichVuID = "";
                        tenDichVu = "";
                        phongID = "";
                        tenPhong = "";
                        MessageBox.Show("Xóa thành công!");
                    }
                    else if (fl == "-2")
                        MessageBox.Show("Chỉ được phép xóa phòng Chờ khám hoặc Đang khám.");
                    else if (fl == "-3")
                        MessageBox.Show("Có dịch vụ đã thanh toán cho phòng khám này, không được xóa.");
                    else if (fl == "-4")
                        MessageBox.Show("Phòng khám có thuốc hoặc vật tư, không được xóa.");
                    else if (fl == "-5")
                        MessageBox.Show("Bệnh nhân đang khám 1 phòng, không được xóa.");
                    else
                        MessageBox.Show("Lỗi xóa phòng khám. Yêu cầu kiểm tra lại thông tin phòng khám.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btn_XoaDV_Click(object sender, EventArgs e)
        {
            if (dichVuID == "")
            {
                MessageBox.Show("Chưa chọn dịch vụ cần xóa");
                return;
            }
            try
            {
                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn xóa dịch vụ " + tenDichVu + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string fl = RequestHTTP.call_ajaxCALL_SP_I("NGT02K051.XOADV", dichVuID);
                    if (fl == "1")
                    {
                        Load_DataGrid_DichVu(1);
                        dichVuID = "";
                        tenDichVu = "";
                        MessageBox.Show("Xóa thành công dịch vụ");
                    }
                    else if (fl == "-2")
                        MessageBox.Show("Dịch vụ này đã thanh toán, không được xóa.");
                    else if (fl == "-3")
                        MessageBox.Show("Dịch vụ này là thuốc hoặc vật tư, không được xóa.");
                    else if (fl == "-4")
                        MessageBox.Show("Dịch vụ này là công khám, không được xóa.");
                    else
                        MessageBox.Show("Lỗi xóa dịch vụ. Yêu cầu kiểm tra thông tin dịch vụ.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region SỰ KIỆN KHỞI TẠO TRÊN CODE
        /// <summary>
        /// Sự kiện khởi tạo trên code
        /// </summary>
        private void gview_DSTimKiem_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGrid_TimKiem(pageNum);
        }

        private void gview_DSTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr_TimKiem = (DataRowView)gview_DSTimKiem.gridView.GetFocusedRow();
                if (dr_TimKiem != null)
                {
                    this.Set_Data_TimKiem(dr_TimKiem);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void gview_DSTimKiem_ColumnFilterChanged(object sender, EventArgs e)
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

        private void gview_DSPhongKham_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGrid_PhongKham(pageNum);
        }

        private void gview_DSPhongKham_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr_PhongKham = (DataRowView)gview_DSPhongKham.gridView.GetFocusedRow();
                if (dr_PhongKham != null)
                {
                    this.Set_Data_PhongKham(dr_PhongKham);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void gview_DSPhongKham_ColumnFilterChanged(object sender, EventArgs e)
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

        private void gview_DSDichVu_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGrid_DichVu(pageNum);
        }

        private void gview_DSDichVu_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr_DichVu = (DataRowView)gview_DSDichVu.gridView.GetFocusedRow();
                if (dr_DichVu != null)
                {
                    this.Set_Data_DichVu(dr_DichVu);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void gview_DSDichVu_ColumnFilterChanged(object sender, EventArgs e)
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