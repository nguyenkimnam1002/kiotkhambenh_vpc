using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using MainForm.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace MainForm
{
    public partial class formMain : DevExpress.XtraEditors.XtraForm
    {
        private DataRowView drvBenhNhan;
        private string mbpThuocSelect = "";
        private string mbpDichVuSelect = "";

        public formMain()
        {
            InitializeComponent();
        }

        //Hàm tạo các bảng trong sqlite - chỉ chạy 1 lần đầu khi chưa có bảng
        private void Create_Table_Sqlite()
        {
            string TABLE_DMC_BENHNHAN = "CREATE TABLE DMC_BENHNHAN (MABENHNHAN TEXT, TENBENHNHAN TEXT, NGAYSINH TEXT, NAMSINH TEXT, TUOI TEXT, DVTUOI TEXT, GIOITINHID TEXT, NGHENGHIEPID TEXT, QUOCTICHID TEXT, DANTOCID TEXT, DIAPHUONGID TEXT, DIACHI TEXT, NGUOINHA TEXT, NGAYKHAM TEXT, PHONGKHAMID TEXT, HINHTHUCVAOVIENID TEXT, BACSIDIEUTRIID TEXT, DOITUONGBENHNHANID TEXT, MATHEBHYT TEXT, THOIGIAN_BD TEXT, THOIGIAN_KT TEXT, SINHTHETE TEXT, DU5NAM TEXT, DU6THANG TEXT, DIACHIBHYT TEXT, DKKCBBDID TEXT, TUYENID TEXT, DOITUONGMIENGIAMID TEXT, TYLEBH TEXT, TYLEMIENGIAM TEXT, MACHANDOANRAVIEN TEXT, CHANDOANRAVIEN TEXT, CHANDOANRAVIEN_KT TEXT)";

            string TABLE_KBH_MAUBENHPHAM = "CREATE TABLE KBH_MAUBENHPHAM (SOPHIEU TEXT, LOAINHOMMAUBENHPHAM TEXT, BENHNHANID TEXT, NGAYMAUBENHPHAM TEXT, NGUOITAO TEXT, PHONGTHUCHIENID TEXT)";

            string TABLE_KBH_DICHVU_KHAMBENH = "CREATE TABLE KBH_DICHVU_KHAMBENH (MAUBENHPHAMID TEXT, BENHNHANID TEXT, NGAYDICHVU TEXT, DICHVUID TEXT, TENDICHVU TEXT, SOLUONG TEXT, DONGIA TEXT, GIABHYT TEXT, GIAVP TEXT, GIADV TEXT, TYLEDV TEXT, BHYTTRA TEXT, NHANDANTRA TEXT, LOAIDOITUONG TEXT)";


            //Danh mục
            string TABLE_DMC_NHOM_MABHYT = "CREATE TABLE DMC_NHOM_MABHYT (NHOM_MABHYT_ID TEXT, MANHOM_BHYT  TEXT);";
            string TABLE_ORG_PHONG = "CREATE TABLE ORG_PHONG ( ORG_ID TEXT, ORG_NAME TEXT, ORG_TYPE TEXT)";
            string TABLE_DUC_KHO = "CREATE TABLE DUC_KHO (KHOID TEXT, TENKHO TEXT, TRANGTHAI TEXT, CSYTID TEXT)";
            string TABLE_ADM_USER = "CREATE TABLE ADM_USER (USER_ID TEXT, USER_NAME TEXT, OFFICER_NAME TEXT, OFFICER_TYPE TEXT)";

            string TABLE_DMC_BENHVIEN = "CREATE TABLE DMC_BENHVIEN ( BENHVIENKCBBD TEXT, TENBENHVIEN TEXT, DIACHI TEXT)";
            string TABLE_DMC_DOITUONG_DACBIET = "CREATE TABLE DMC_DOITUONG_DACBIET (DOITUONGDACBIETID TEXT, TENDOITUONGDACBIET TEXT, TYLEMIENGIAM TEXT, MA_BHYT TEXT, SUDUNG TEXT)";
            string TABLE_DMC_ICD = "CREATE TABLE DMC_ICD (ICD10CODE TEXT, ICD10NAME TEXT, ICD10NAME_EN TEXT, ICD10NAME_THUONGGOI TEXT)";
            string TABLE_DMC_DIAPHUONG = "CREATE TABLE DMC_DIAPHUONG (DIAPHUONGID TEXT, TENDIAPHUONG TEXT, MATIMKIEM TEXT, MABH TEXT, MADIAPHUONG TEXT, TENVIETTAT TEXT, CAP TEXT, DIAPHUONGCHAID TEXT, tendiaphuongdaydu TEXT, tenviettatdaydu TEXT)";

            string TABLE_DMC_DICHVU = "CREATE TABLE DMC_DICHVU (LOAIDICHVU TEXT,LOAINHOMDICHVU TEXT, TENDICHVU TEXTTEXT,khoa TEXT,daxoa TEXT, GIABHYT TEXT, GIANHANDAN TEXT, GIADICHVU TEXT)";
            string TABLE_DMC_DOITUONG_BHYT = "CREATE TABLE DMC_DOITUONG_BHYT (TYLE_MIENGIAM TEXT, DOITUONG_BHYT_ID TEXT, MA_DOITUONG_BHYT TEXT)";
            string TABLE_DMC_HANG_BHTRAITUYEN = "CREATE TABLE DMC_HANG_BHTRAITUYEN (HANG_BHTRAITUYEN_ID TEXT, TYLE_NOITRU TEXT, TYLE_NGOAITRU TEXT)";


            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_BENHNHAN);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_KBH_MAUBENHPHAM);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_KBH_DICHVU_KHAMBENH);

            ////Danh mục
            ////LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_NHOM_MABHYT);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_ORG_PHONG);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DUC_KHO);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_ADM_USER);

            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_BENHVIEN);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DOITUONG_DACBIET);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_ICD);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DIAPHUONG);

            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DICHVU);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_DOITUONG_BHYT);
            //LocalConst.LOCAL_SQLITE.execute(TABLE_DMC_HANG_BHTRAITUYEN);

            // LocalConst.LOCAL_SQLITE.execute("insert into DMC_NHOM_MABHYT (NHOM_MABHYT_ID , MANHOM_BHYT)  VALUES ('111','222')  ");
            //DataTable dt;
            // LocalConst.LOCAL_SQLITE.SqliteTable_Select("DMC_DICHVU", out dt);

            //string TABLE_DMC_DICHVsssU = "";

        }

        private void formMain_Load(object sender, EventArgs e)
        {
            LocalConst.INIT();



            Create_Table_Sqlite();


            //set Ngày tìm kiếm
            txtTuNgay.DateTime = DateTime.Now.AddDays(-30);
            txtDenNgay.DateTime = DateTime.Now;
            //Đối tượng
            DataTable dt = new DataTable();
            dt.Columns.Add("DTBNID", typeof(String));
            dt.Columns.Add("TENDTBN", typeof(String));
            dt.Rows.Add(new string[] { "0", "Tất cả" });
            dt.Rows.Add(new string[] { "1", "BHYT" });
            dt.Rows.Add(new string[] { "2", "Viện phí" });
            dt.Rows.Add(new string[] { "3", "Dịch vụ" });
            cboDoiTuong.setEvent_Enter(cboDoiTuong_KeyEnter);
            cboDoiTuong.setData(dt, 0, 1);
            cboDoiTuong.SelectIndex = 0;
            gridBenhNhan.setEvent(getData_table);
            getData_table(1, null);
        }

        private void loadChiDinhDichVu(object sender, EventArgs e)
        {
            try
            {
                if (drvBenhNhan == null) return;

                int page = (int)sender;
                if (page > 0)
                {
                    string sqlQuery = "";
                    DataTable dt = new DataTable();
                    string benhNhanId = drvBenhNhan["BENHNHANID"].ToString();
                    sqlQuery = "SELECT A.*, B.ORG_NAME TENPHONG, C.USER_NAME ";
                    sqlQuery += " FROM KBH_MAUBENHPHAM A ";
                    sqlQuery += " LEFT JOIN ORG_PHONG B ON A.PHONGTHUCHIENID = B.ORG_ID ";
                    sqlQuery += " LEFT JOIN ADM_USER C ON A.NGUOITAO = C.USER_ID ";
                    sqlQuery += " WHERE A.BENHNHANID = " + benhNhanId + " AND A.LOAINHOMMAUBENHPHAM IN (1,2,3,5) ";

                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    dt.Columns.Add("XOA");
                    dt.Columns.Add("SUA");
                    gridCDDV_MBP.setEvent_FocusedRowChanged(gridCDDV_MBP_ChangeSelectRow);

                    RepositoryItemHyperLinkEdit gridCDDV_MBPItemXoa = new RepositoryItemHyperLinkEdit();
                    gridCDDV_MBPItemXoa.AutoHeight = false;
                    gridCDDV_MBPItemXoa.Image = Func.getIcon("del.png");
                    gridCDDV_MBPItemXoa.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridCDDV_MBPItemXoa.Click += gridCDDV_MBPItemXoa_Click;

                    RepositoryItemHyperLinkEdit gridCDDV_MBPItemSua = new RepositoryItemHyperLinkEdit();
                    gridCDDV_MBPItemSua.AutoHeight = false;
                    gridCDDV_MBPItemSua.Image = Func.getIcon("edit.png");
                    gridCDDV_MBPItemSua.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridCDDV_MBPItemSua.Click += gridCDDV_MBPItemSua_Click;

                    if (dt.Rows.Count == 0)
                    {
                        dt = Func.getTableEmpty(new String[] { "SOPHIEU", "NGAYMAUBENHPHAM", "USER_NAME", "TENPHONG", "XOA", "SUA" });
                        mbpDichVuSelect = "";
                        loadDSDichVu(1, null);
                    }
                    int numPerPage = gridCDDV_MBP.ucPage1.getNumberPerPage();
                    int totalPage = (int)Math.Ceiling((double)dt.Rows.Count / numPerPage);
                    gridCDDV_MBP.setData(dt, totalPage, page);
                    gridCDDV_MBP.setColumnAll(false);
                    gridCDDV_MBP.setColumn("SOPHIEU", 0, "Số phiếu", 80);
                    gridCDDV_MBP.setColumn("NGAYMAUBENHPHAM", 1, "Ngày tạo", 80);
                    gridCDDV_MBP.setColumn("USER_NAME", 2, "Người tạo", 100);
                    gridCDDV_MBP.setColumn("TENPHONG", 3, "Phòng thực hiện", 250);
                    gridCDDV_MBP.setColumn("XOA", 4, "", 25);
                    gridCDDV_MBP.setColumn("SUA", 5, "", 25);
                    gridCDDV_MBP.gridView.Columns["XOA"].ColumnEdit = gridCDDV_MBPItemXoa;
                    gridCDDV_MBP.gridView.Columns["SUA"].ColumnEdit = gridCDDV_MBPItemSua;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void loadChiDinhThuoc(object sender, EventArgs e)
        {
            try
            {
                if (drvBenhNhan == null) return;

                int page = (int)sender;
                if (page > 0)
                {
                    string sqlQuery = "";
                    DataTable dt = new DataTable();
                    string benhNhanId = drvBenhNhan["BENHNHANID"].ToString();
                    sqlQuery = "SELECT a.*, b.TENKHO AS KHO, c.USER_NAME ";
                    sqlQuery += " FROM KBH_MAUBENHPHAM a ";
                    sqlQuery += " LEFT JOIN DUC_KHO b ON a.KHOTHUOCID = b.KHOID ";
                    sqlQuery += " LEFT JOIN ADM_USER c ON a.nguoitao = c.USER_ID ";
                    sqlQuery += " WHERE a.BENHNHANID = " + benhNhanId + " AND a.LOAINHOMMAUBENHPHAM IN (7,8) ";

                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    dt.Columns.Add("XOA");
                    dt.Columns.Add("SUA");
                    gridCDThuoc_MBP.setEvent_FocusedRowChanged(gridCDThuoc_MBP_ChangeSelectRow);

                    RepositoryItemHyperLinkEdit gridCDThuoc_MBPItemXoa = new RepositoryItemHyperLinkEdit();
                    gridCDThuoc_MBPItemXoa.AutoHeight = false;
                    gridCDThuoc_MBPItemXoa.Image = Func.getIcon("del.png");
                    gridCDThuoc_MBPItemXoa.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridCDThuoc_MBPItemXoa.Click += gridCDThuoc_MBPItemXoa_Click;

                    RepositoryItemHyperLinkEdit gridCDThuoc_MBPItemSua = new RepositoryItemHyperLinkEdit();
                    gridCDThuoc_MBPItemSua.AutoHeight = false;
                    gridCDThuoc_MBPItemSua.Image = Func.getIcon("edit.png");
                    gridCDThuoc_MBPItemSua.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridCDThuoc_MBPItemSua.Click += gridCDThuoc_MBPItemSua_Click;

                    if (dt.Rows.Count == 0)
                    {
                        dt = Func.getTableEmpty(new String[] { "SOPHIEU", "NGAYMAUBENHPHAM", "USER_NAME", "KHO", "XOA", "SUA" });
                        mbpThuocSelect = "";
                        loadDSThuoc(1, null);
                    }

                    int numPerPage = gridCDThuoc_MBP.ucPage1.getNumberPerPage();
                    int totalPage = (int)Math.Ceiling((double)dt.Rows.Count / numPerPage);
                    gridCDThuoc_MBP.setData(dt, totalPage, page);
                    gridCDThuoc_MBP.setColumnAll(false);
                    gridCDThuoc_MBP.setColumn("SOPHIEU", 0, "Số phiếu", 100);
                    gridCDThuoc_MBP.setColumn("NGAYMAUBENHPHAM", 1, "Ngày tạo", 100);
                    gridCDThuoc_MBP.setColumn("USER_NAME", 2, "Người tạo", 100);
                    gridCDThuoc_MBP.setColumn("KHO", 3, "Kho", 250);
                    gridCDThuoc_MBP.setColumn("XOA", 4, "", 25);
                    gridCDThuoc_MBP.setColumn("SUA", 5, "", 25);
                    gridCDThuoc_MBP.gridView.Columns["XOA"].ColumnEdit = gridCDThuoc_MBPItemXoa;
                    gridCDThuoc_MBP.gridView.Columns["SUA"].ColumnEdit = gridCDThuoc_MBPItemSua;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        #region Xử lý các button xóa, sửa trong grid
        private void gridCDDV_MBPItemXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(mbpDichVuSelect))
                {
                    MessageBox.Show("Chọn bệnh phẩm cần xóa");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa bệnh phẩm này", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool result = LocalConst.LOCAL_SQLITE.sqliteTransaction_XoaMauBenhPham(mbpDichVuSelect);
                    if (result)
                        MessageBox.Show("Xóa bệnh phẩm thành công");
                    else
                        MessageBox.Show("Có lỗi khi xóa bệnh phẩm");
                    loadChiDinhDichVu(1, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void gridCDDV_MBPItemSua_Click(object sender, EventArgs e)
        {
            try
            {
                formKeDV form = new formKeDV();
                form.loadData(drvBenhNhan, mbpDichVuSelect);
                form.setEvent(listenEven_FormKeDichVu);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void gridCDThuoc_MBPItemXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(mbpThuocSelect))
                {
                    MessageBox.Show("Chọn bệnh phẩm cần xóa");
                    return;
                }
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa bệnh phẩm này", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bool result = LocalConst.LOCAL_SQLITE.sqliteTransaction_XoaMauBenhPham(mbpThuocSelect);
                    if (result)
                        MessageBox.Show("Xóa bệnh phẩm thành công");
                    else
                        MessageBox.Show("Có lỗi khi xóa bệnh phẩm");
                    loadChiDinhThuoc(1, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void gridCDThuoc_MBPItemSua_Click(object sender, EventArgs e)
        {
            try
            {
                formKeThuoc form = new formKeThuoc();
                form.loadData(drvBenhNhan, mbpThuocSelect);
                form.setEvent(listenEven_FormKeThuoc);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void loadDSDichVu(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    string sqlQuery = "";
                    DataTable dt = new DataTable();
                    sqlQuery = "SELECT A.DICHVUID, A.DONGIA, A.SOLUONG, A.BHYTTRA, A.NHANDANTRA, A.LOAIDOITUONG, B.MADICHVU, B.TENDICHVU ";
                    sqlQuery += "FROM KBH_DICHVU_KHAMBENH a INNER JOIN DMC_DICHVU b ON a.dichvuid = b.dichvuid WHERE a.maubenhphamid = '" + mbpDichVuSelect + "'";

                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "MADICHVU", "TENDICHVU", "DONGIA", "SOLUONG", "BHYTTRA", "NHANDANTRA" });
                    int numPerPage = gridCDDV_DSCD.ucPage1.getNumberPerPage();
                    int totalPage = (int)Math.Ceiling((double)dt.Rows.Count / numPerPage);
                    gridCDDV_DSCD.setData(dt, totalPage, page);
                    gridCDDV_DSCD.setColumnAll(false);
                    gridCDDV_DSCD.setColumn("MADICHVU", 0, "Mã dịch vụ", 100);
                    gridCDDV_DSCD.setColumn("TENDICHVU", 1, "Tên dịch vụ", 250);
                    gridCDDV_DSCD.setColumn("DONGIA", 2, "Đơn giá", 100);
                    gridCDDV_DSCD.setColumn("SOLUONG", 3, "Số lượng", 100);
                    gridCDDV_DSCD.setColumn("BHYTTRA", 4, "BHYT trả", 100);
                    gridCDDV_DSCD.setColumn("NHANDANTRA", 5, "ND trả", 100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void loadDSThuoc(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    string sqlQuery = "";
                    DataTable dt = new DataTable();
                    sqlQuery = "SELECT a.DICHVUID, a.DONGIA, a.SOLUONG, a.BHYTTRA, a.NHANDANTRA, b.ma MADICHVU, b.ten TENDICHVU, a.GHICHU ";
                    sqlQuery += "FROM KBH_DICHVU_KHAMBENH a INNER JOIN DUC_THUOCVATTU b ON a.dichvuid = b.thuocvattuid AND a.maubenhphamid = '" + mbpThuocSelect + "'";

                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "MADICHVU", "TENDICHVU", "DONGIA", "SOLUONG", "BHYTTRA", "NHANDANTRA", "GHICHU" });
                    int numPerPage = gridCDThuoc_DSCD.ucPage1.getNumberPerPage();
                    int totalPage = (int)Math.Ceiling((double)dt.Rows.Count / numPerPage);
                    gridCDThuoc_DSCD.setData(dt, totalPage, page);
                    gridCDThuoc_DSCD.setColumnAll(false);
                    gridCDThuoc_DSCD.setColumn("MADICHVU", 0, "Mã dịch vụ", 100);
                    gridCDThuoc_DSCD.setColumn("TENDICHVU", 1, "Tên dịch vụ", 250);
                    gridCDThuoc_DSCD.setColumn("DONGIA", 2, "Đơn giá", 100);
                    gridCDThuoc_DSCD.setColumn("SOLUONG", 3, "Số lượng", 100);
                    gridCDThuoc_DSCD.setColumn("BHYTTRA", 4, "BHYT trả", 100);
                    gridCDThuoc_DSCD.setColumn("NHANDANTRA", 5, "ND trả", 100);
                    gridCDThuoc_DSCD.setColumn("GHICHU", 6, "Cách dùng", 250);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void btnThemBN_ItemClick(object sender, ItemClickEventArgs e)
        {
            formThemSuaBN form = new formThemSuaBN();
            form.setEvent(listenEven_FormThemSuaBn);
            form.ShowDialog();
        }

        private void btnSuaBN_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (drvBenhNhan == null)
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
                return;
            }
            formThemSuaBN form = new formThemSuaBN();
            form.loadData(drvBenhNhan);
            form.setEvent(listenEven_FormThemSuaBn);
            form.ShowDialog();
        }

        #endregion

        #region Hàm lắng nghe sự kiện khi lưu bệnh nhân thành công
        private void listenEven_FormThemSuaBn(object sender, EventArgs e)
        {
            getData_table(1, null);
        }

        private void btnXoaBN_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (drvBenhNhan == null || string.IsNullOrEmpty(drvBenhNhan["BENHNHANID"].ToString()))
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn xóa bệnh nhân này", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string benhNhanId = drvBenhNhan["BENHNHANID"].ToString();
                bool result = LocalConst.LOCAL_SQLITE.sqliteTransaction_XoaBenhNhan(benhNhanId);
                if (result)
                    MessageBox.Show("Xóa bệnh nhân thành công");
                else
                    MessageBox.Show("Có lỗi khi xóa bệnh nhân");
                getData_table(1, null);
            }
        }

        private void btnKeThuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (drvBenhNhan == null)
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
                return;
            }
            formKeThuoc form = new formKeThuoc();
            form.loadData(drvBenhNhan);
            form.setEvent(listenEven_FormKeThuoc);
            form.ShowDialog();
        }

        // Hàm lắng nghe sự kiện khi chỉ định thuốc thành công
        private void listenEven_FormKeThuoc(object sender, EventArgs e)
        {
            MessageBox.Show("Chỉ định thuốc thành công");
            loadChiDinhThuoc(1, null);
        }

        private void btnKeDV_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (drvBenhNhan == null)
            {
                MessageBox.Show("Chưa chọn bệnh nhân");
                return;
            }
            formKeDV form = new formKeDV();
            form.loadData(drvBenhNhan);
            form.setEvent(listenEven_FormKeDichVu);
            form.ShowDialog();
        }

        // Hàm lắng nghe sự kiện khi chỉ định dịch vụ thành công
        private void listenEven_FormKeDichVu(object sender, EventArgs e)
        {
            MessageBox.Show("Chỉ định dịch vụ thành công");
            loadChiDinhDichVu(1, null);
        }

        private void btnInDonThuoc_ItemClick(object sender, ItemClickEventArgs e)
        {
            string sqlQuery = "";
            DataTable dtSql = new DataTable();
            reportInDonThuoc report = new reportInDonThuoc();
            List<ReportDonThuoc> listDtl = new List<ReportDonThuoc>();
            if (string.IsNullOrEmpty(mbpThuocSelect))
            {
                MessageBox.Show("Chọn đơn thuốc để in");
                return;
            }
            DataRowView drMbp = gridCDThuoc_MBP.SelectedRow;
            DataTable dtDSCD = (DataTable)gridCDThuoc_DSCD.gridControl.DataSource;
            for (int i = 0; i < dtDSCD.Rows.Count; i++)
            {
                ReportDonThuoc dt = new ReportDonThuoc();
                dt.parenName = "SYT TP HỒ CHÍ MINH";
                dt.orgName = "BỆNH VIỆN NGUYỄN TRÃI";
                dt.maBenhNhan = drvBenhNhan["MABENHNHAN"].ToString().ToUpper();
                dt.soPhieuThuoc = drMbp["SOPHIEU"].ToString();
                dt.tenBenhNhan = drvBenhNhan["TENBENHNHAN"].ToString().ToUpper();
                dt.tuoi = drvBenhNhan["TUOI"].ToString();
                if (drvBenhNhan["DVTUOI"].ToString().Equals("1"))
                    dt.dvTuoi = "Tuổi";
                else if (drvBenhNhan["DVTUOI"].ToString().Equals("2"))
                    dt.dvTuoi = "Tháng";
                else if (drvBenhNhan["DVTUOI"].ToString().Equals("3"))
                    dt.dvTuoi = "Ngày";
                else
                    dt.dvTuoi = "Giờ";
                dt.gioiTinh = drvBenhNhan["GIOITINHID"].ToString().Equals("1") ? "Nam" : (drvBenhNhan["GIOITINHID"].ToString().Equals("2") ? "Nữ" : "Khác");
                dt.diaChi = drvBenhNhan["DIACHI"].ToString();
                dt.checkVp = drvBenhNhan["DOITUONGBENHNHANID"].ToString().Equals("2") ? "X" : "";
                dt.maKCBBD = drvBenhNhan["DKKCBBDID"].ToString();
                dt.dungTuyen = "Tuyến BHYT: ";
                if (drvBenhNhan["TUYENID"].ToString().Equals("1"))
                    dt.dungTuyen = dt.dungTuyen + "Đúng tuyến";
                else if (drvBenhNhan["TUYENID"].ToString().Equals("2"))
                    dt.dungTuyen = dt.dungTuyen + "Đúng tuyến giới thiệu";
                else if (drvBenhNhan["TUYENID"].ToString().Equals("3"))
                    dt.dungTuyen = dt.dungTuyen + "Đúng tuyến cấp cứu";
                else if (drvBenhNhan["TUYENID"].ToString().Equals("4"))
                    dt.dungTuyen = dt.dungTuyen + "Trái tuyến";
                if (drvBenhNhan["DOITUONGBENHNHANID"].ToString().Equals("1"))
                {
                    string soThe = drvBenhNhan["MATHEBHYT"].ToString();
                    dt.soThe1 = soThe.Substring(0, 3).ToUpper();
                    dt.soThe2 = soThe.Substring(3, 2).ToUpper();
                    dt.soThe3 = soThe.Substring(5, 2).ToUpper();
                    dt.soThe4 = soThe.Substring(7, 3).ToUpper();
                    dt.soThe5 = soThe.Substring(10, 5).ToUpper();
                    dt.hanThe = "Hạn thẻ từ: " + drvBenhNhan["THOIGIAN_BD"].ToString() + " đến " + drvBenhNhan["THOIGIAN_KT"].ToString();
                }
                else
                {
                    dt.soThe1 = "";
                    dt.soThe2 = "";
                    dt.soThe3 = "";
                    dt.soThe4 = "";
                    dt.soThe5 = "";
                    dt.hanThe = "";
                }

                dt.chanDoan = "Chẩn đoán: " + drvBenhNhan["CHANDOANRAVIEN"].ToString() + " (" + drvBenhNhan["MACHANDOANRAVIEN"].ToString() + ")";
                dt.chanDoanPhu = "Chẩn đoán phụ: " + drvBenhNhan["CHANDOANRAVIEN_KT"].ToString();


                string ngayMbp = drMbp["NGAYMAUBENHPHAM"].ToString();
                dt.ngayMauBenhPham = "Ngày " + ngayMbp.Substring(0, 2) + " Tháng " + ngayMbp.Substring(3, 2) + " Năm " + ngayMbp.Substring(6, 4);
                sqlQuery = "SELECT OFFICER_NAME FROM ADM_USER WHERE USER_ID = '" + drMbp["NGUOITAO"].ToString() + "'";
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtSql);
                dt.nguoiTao = dtSql.Rows[0]["OFFICER_NAME"].ToString().ToUpper();

                DataRow dr = dtDSCD.Rows[i];
                dt.rowNum = (i + 1) + ". ";
                dt.soLuong = dr["SOLUONG"].ToString();

                // "INSERT INTO KBH_MAUBENHPHAM (SOPHIEU, LOAINHOMMAUBENHPHAM, BENHNHANID, NGAYMAUBENHPHAM, NGUOITAO, KHOTHUOCID) 
                // "INSERT INTO KBH_DICHVU_KHAMBENH (MAUBENHPHAMID, BENHNHANID, NGAYDICHVU, DICHVUID, TENDICHVU, SOLUONG, DONGIA, GIABHYT, GIAVP, GIADV, TYLEDV, BHYTTRA, NHANDANTRA, LOAIDOITUONG, GHICHU) VALUES (";

                sqlQuery = "SELECT DV.TEN_DVT, TVT.HOATCHAT FROM DUC_THUOCVATTU TVT, DUC_DONVITINH DV WHERE TVT.DONVITINHID = DV.DONVITINHID AND TVT.THUOCVATTUID = '" + dr["DICHVUID"].ToString() + "'";
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtSql);
                dt.tenDichVu = dtSql.Rows[0]["HOATCHAT"].ToString() + " (" + dr["TENDICHVU"].ToString().ToUpper() + ")";
                dt.tenDvt = dtSql.Rows[0]["TEN_DVT"].ToString();
                dt.cachDung = dr["GHICHU"].ToString();
                listDtl.Add(dt);
            }
            report.DataSource = listDtl;
            report.BottomMargin.HeightF = 1100F - report.Detail.HeightF - report.TopMargin.HeightF - 100F;

            ReportPrintTool tool = new ReportPrintTool(report);
            tool.PreviewRibbonForm.PrintControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToWholePage, null);
            tool.ShowRibbonPreviewDialog();

            //using (DevExpress.XtraReports.UI.ReportPrintTool tool = new DevExpress.XtraReports.UI.ReportPrintTool(report))
            //{
            //    tool.PreviewRibbonForm.PrintControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToWholePage, null);
            //    tool.ShowRibbonPreviewDialog();
            //}
        }

        private void txtTuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            txtDenNgay.Focus();
        }

        private void txtDenNgay_KeyDown(object sender, KeyEventArgs e)
        {
            cboDoiTuong.Focus();
        }

        private void cboDoiTuong_KeyEnter(object sender, EventArgs e)
        {
            txtMaBN.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTuNgay.DateTime > txtDenNgay.DateTime)
            {
                MessageBox.Show("Sai điều kiện tìm kiếm, từ ngày không thể lớn hơn đến ngày!");
                return;
            }
            getData_table(1, null);
        }

        private void getData_table(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    DataTable dt = new DataTable();
                    string sqlQuery = "SELECT a.*, b.ORG_NAME, b.PARENT_ID ";
                    sqlQuery += "      FROM DMC_BENHNHAN a, ORG_PHONG b ";
                    sqlQuery += "      WHERE a.PHONGKHAMID = b.ORG_ID";
                    if (!string.IsNullOrEmpty(txtTuNgay.Text))
                    {
                        string tuNgay = txtTuNgay.DateTime.ToString("yyyyMMdd");
                        sqlQuery += "  AND substr(a.NGAYKHAM,7,4)||substr(a.NGAYKHAM,4,2)||substr(a.NGAYKHAM,1,2) >= '" + tuNgay + "'";
                    }
                    if (!string.IsNullOrEmpty(txtDenNgay.Text))
                    {
                        string denNgay = txtDenNgay.DateTime.ToString("yyyyMMdd");
                        sqlQuery += "  AND substr(a.NGAYKHAM,7,4)||substr(a.NGAYKHAM,4,2)||substr(a.NGAYKHAM,1,2) <= '" + denNgay + "'"; ;
                    }
                    if (cboDoiTuong.SelectValue != "0")
                    {
                        sqlQuery += "  AND a.DOITUONGBENHNHANID = '" + cboDoiTuong.SelectValue + "'";
                    }
                    if (!string.IsNullOrEmpty(txtMaBN.Text))
                    {
                        string txtBn = "%" + txtMaBN.Text.ToUpper() + "%";
                        sqlQuery += "  AND (a.MABENHNHAN like '" + txtBn + "' OR a.TENBENHNHAN LIKE '" + txtBn + "')";
                    }
                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    gridBenhNhan.setEvent_FocusedRowChanged(gridBenhNhan_ChangeSelectRow);
                    gridBenhNhan.clearData();

                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "MABENHNHAN", "TENBENHNHAN", "MATHEBHYT", "NGAYSINH", "NGAYKHAM", "ORG_NAME", "CHANDOANRAVIEN", "CHANDOANRAVIEN_KT" });
                    int numPerPage = gridBenhNhan.ucPage1.getNumberPerPage();
                    int totalPage = (int)Math.Ceiling((double)dt.Rows.Count / numPerPage);
                    gridBenhNhan.setData(dt, totalPage, page);
                    gridBenhNhan.setColumnAll(false);
                    gridBenhNhan.setColumn("MABENHNHAN", 0, "Mã bệnh nhân");
                    gridBenhNhan.setColumn("TENBENHNHAN", 1, "Tên bệnh nhân");
                    gridBenhNhan.setColumn("MATHEBHYT", 2, "Mã thẻ BHYT");
                    gridBenhNhan.setColumn("NGAYSINH", 3, "Ngày sinh");
                    gridBenhNhan.setColumn("NGAYKHAM", 4, "Ngày Khám");
                    gridBenhNhan.setColumn("ORG_NAME", 5, "Phòng khám");
                    gridBenhNhan.setColumn("CHANDOANRAVIEN", 6, "Chẩn đoán");
                    gridBenhNhan.setColumn("CHANDOANRAVIEN_KT", 7, "Chẩn đoán phụ");
                    gridBenhNhan.gridView.BestFitColumns(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void gridBenhNhan_ChangeSelectRow(object sender, EventArgs e)
        {
            try
            {
                DataRowView selectedBenhNhan = (DataRowView)sender;
                if (selectedBenhNhan != null)
                {
                    drvBenhNhan = selectedBenhNhan;
                    if (tabPane1.SelectedPage == tabNavigationPage1)
                        loadChiDinhThuoc(1, null);
                    else
                        loadChiDinhDichVu(1, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (tabPane1.SelectedPage == tabNavigationPage1)
                loadChiDinhThuoc(1, null);
            else
                loadChiDinhDichVu(1, null);
        }

        private void gridCDThuoc_MBP_ChangeSelectRow(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr = (DataRowView)sender;
                if (dr != null && !string.IsNullOrEmpty(dr["MAUBENHPHAMID"].ToString()))
                {
                    mbpThuocSelect = dr["MAUBENHPHAMID"].ToString();
                    loadDSThuoc(1, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }

        }

        private void gridCDDV_MBP_ChangeSelectRow(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr = (DataRowView)sender;
                if (dr != null && !string.IsNullOrEmpty(dr["MAUBENHPHAMID"].ToString()))
                {
                    mbpDichVuSelect = dr["MAUBENHPHAMID"].ToString();
                    loadDSDichVu(1, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }
        #endregion

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            ManageCache frm = new ManageCache();
            frm.ShowDialog();

        }

        private void btnInBangKe_ItemClick(object sender, ItemClickEventArgs e)
        {
            string sqlQuery = "";
            DataTable dtSql = new DataTable();

            report_BangKe report = new report_BangKe();
            List<Bang_DichVu> listDtl = new List<Bang_DichVu>();

            if (string.IsNullOrEmpty(mbpDichVuSelect))
            {
                MessageBox.Show("Chọn dịch vụ để in");
                return;
            }

            DataRowView drMbp = gridCDDV_MBP.SelectedRow;
            DataTable dtDSCD = (DataTable)gridCDDV_DSCD.gridControl.DataSource;
            for (int i = 0; i < dtDSCD.Rows.Count; i++)
            {
                Bang_DichVu dt = new Bang_DichVu();
                dt.parenName = "SYT TP HỒ CHÍ MINH";


                dt.parenName = "SYT TP HỒ CHÍ MINH";
                dt.orgName = "BỆNH VIỆN NGUYỄN TRÃI";
                dt.maBenhNhan = drvBenhNhan["MABENHNHAN"].ToString().ToUpper();
                dt.soPhieuThuoc = "P000000001";
                dt.tenBenhNhan = drvBenhNhan["TENBENHNHAN"].ToString().ToUpper();
                dt.tuoi = drvBenhNhan["TUOI"].ToString();
                if (drvBenhNhan["DVTUOI"].ToString().Equals("1"))
                    dt.dvTuoi = "Tuổi";
                else if (drvBenhNhan["DVTUOI"].ToString().Equals("2"))
                    dt.dvTuoi = "Tháng";
                else if (drvBenhNhan["DVTUOI"].ToString().Equals("3"))
                    dt.dvTuoi = "Ngày";
                else
                    dt.dvTuoi = "Giờ";
                dt.gioiTinh = drvBenhNhan["GIOITINHID"].ToString().Equals("1") ? "Nam" : (drvBenhNhan["GIOITINHID"].ToString().Equals("2") ? "Nữ" : "Khác");
                dt.diaChi = drvBenhNhan["DIACHI"].ToString();
                dt.checkVp = drvBenhNhan["DOITUONGBENHNHANID"].ToString().Equals("2") ? "X" : "";
                dt.maKCBBD = drvBenhNhan["DKKCBBDID"].ToString();
                dt.dungTuyen = "Tuyến BHYT: ";
                if (drvBenhNhan["TUYENID"].ToString().Equals("1"))
                    dt.dungTuyen = dt.dungTuyen + "Đúng tuyến";
                else if (drvBenhNhan["TUYENID"].ToString().Equals("2"))
                    dt.dungTuyen = dt.dungTuyen + "Đúng tuyến giới thiệu";
                else if (drvBenhNhan["TUYENID"].ToString().Equals("3"))
                    dt.dungTuyen = dt.dungTuyen + "Đúng tuyến cấp cứu";
                else if (drvBenhNhan["TUYENID"].ToString().Equals("4"))
                    dt.dungTuyen = dt.dungTuyen + "Trái tuyến";
                if (drvBenhNhan["DOITUONGBENHNHANID"].ToString().Equals("1"))
                {
                    string soThe = drvBenhNhan["MATHEBHYT"].ToString();
                    dt.soThe1 = soThe.Substring(0, 3).ToUpper();
                    dt.soThe2 = soThe.Substring(3, 2).ToUpper();
                    dt.soThe3 = soThe.Substring(5, 2).ToUpper();
                    dt.soThe4 = soThe.Substring(7, 3).ToUpper();
                    dt.soThe5 = soThe.Substring(10, 5).ToUpper();
                    dt.hanThe = "Hạn thẻ từ: " + drvBenhNhan["THOIGIAN_BD"].ToString() + " đến " + drvBenhNhan["THOIGIAN_KT"].ToString();
                }
                else
                {
                    dt.soThe1 = "";
                    dt.soThe2 = "";
                    dt.soThe3 = "";
                    dt.soThe4 = "";
                    dt.soThe5 = "";
                    dt.hanThe = "";
                }

                dt.chanDoan = "Chẩn đoán: " + drvBenhNhan["CHANDOANRAVIEN"].ToString() + " (" + drvBenhNhan["MACHANDOANRAVIEN"].ToString() + ")";
                dt.chanDoanPhu = "Chẩn đoán phụ: " + drvBenhNhan["CHANDOANRAVIEN_KT"].ToString();
                string ngayMbp = drMbp["NGAYMAUBENHPHAM"].ToString();
                dt.ngayMauBenhPham = "Ngày " + ngayMbp.Substring(0, 2) + " Tháng " + ngayMbp.Substring(3, 2) + " Năm " + ngayMbp.Substring(6, 4);


                sqlQuery = "SELECT OFFICER_NAME FROM ADM_USER WHERE USER_ID = '" + drMbp["NGUOITAO"].ToString() + "'";
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtSql);
                dt.nguoiTao = dtSql.Rows[0]["OFFICER_NAME"].ToString().ToUpper();


                dt.tenPhong = drMbp["TENPHONG"].ToString();

                DataRow dr = dtDSCD.Rows[i];
                dt.rowNum = (i + 1) + ". ";
                dt.soLuong = dr["SOLUONG"].ToString();

                // Dịch vụ
                // "INSERT INTO KBH_MAUBENHPHAM (SOPHIEU, LOAINHOMMAUBENHPHAM, BENHNHANID, NGAYMAUBENHPHAM, NGUOITAO, PHONGTHUCHIENID) VALUES (";

                //Danh sách dịch vụ
                // "INSERT INTO KBH_DICHVU_KHAMBENH (MAUBENHPHAMID, BENHNHANID, NGAYDICHVU, DICHVUID, TENDICHVU, SOLUONG, DONGIA, GIABHYT, GIAVP, GIADV, TYLEDV, BHYTTRA, NHANDANTRA, LOAIDOITUONG) VALUES (";


                dt.tenDichVu = dr["TENDICHVU"].ToString();
                dt.donGia = dr["DONGIA"].ToString();
                dt.BHYT_tra = dr["BHYTTRA"].ToString();
                dt.ND_tra = dr["NHANDANTRA"].ToString();


                listDtl.Add(dt);
            }
            report.DataSource = listDtl;
            report.BottomMargin.HeightF = 1100F - report.Detail.HeightF - report.TopMargin.HeightF - 100F;

            ReportPrintTool tool = new ReportPrintTool(report);
            tool.PreviewRibbonForm.PrintControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToWholePage, null);
            tool.ShowRibbonPreviewDialog();

        }

        // BẢNG KÊ RA VIỆN
        private void btnInHoaDon_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (drvBenhNhan == null)
            {
                MessageBox.Show("Chọn bệnh nhân để in");
                return;
            }




            Report_BangKeRaVien data = new Report_BangKeRaVien();



            //Thông tin bệnh nhân 
            #region 
            DataTable dtTemp;
            data.parenName = "SỞ Y TẾ TP.HCM";
            data.orgName = "Bệnh Viện Nguyễn Trãi";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql("Select org_id,org_code,org_name from ORG_KHOA where org_id = '"+ drvBenhNhan["parent_id"].ToString() + "'", out dtTemp);
            data.orgKhoa = dtTemp.Rows[0]["org_name"].ToString();
            data.orgMaKhoa = "Mã khoa: " + dtTemp.Rows[0]["org_code"].ToString();


            data.maBenhNhan = drvBenhNhan["MABENHNHAN"].ToString().ToUpper();
            data.soPhieuThuoc = "Số khám bệnh: ";
            //"Mã số người bệnh: " + 

            data.tenBenhNhan = drvBenhNhan["TENBENHNHAN"].ToString().ToUpper();
            data.NGAYSINH = drvBenhNhan["NGAYSINH"].ToString();
            data.NGAYKHAM = drvBenhNhan["NGAYKHAM"].ToString();
             
            if (drvBenhNhan["TUYENID"].ToString().Equals("1"))
                data.DUNG_TUYEN = "x";
            else if (drvBenhNhan["TUYENID"].ToString().Equals("2"))
                data.THONG_TUYEN = "x";
            else if (drvBenhNhan["TUYENID"].ToString().Equals("3"))
                data.CAP_CUU = "x";
            else if (drvBenhNhan["TUYENID"].ToString().Equals("4"))
                data.TRAI_TUYEN = "x";


            data.MACHANDOANRAVIEN = drvBenhNhan["MACHANDOANRAVIEN"].ToString();
            data.MACHANDOANRAVIEN_KEMTHEO = "";
            data.DU5NAM = drvBenhNhan["DU5NAM"].ToString();

            //data.tuoi = drvBenhNhan["TUOI"].ToString();
            if (drvBenhNhan["DVTUOI"].ToString().Equals("1"))
                data.dvTuoi = "Tuổi";
            else if (drvBenhNhan["DVTUOI"].ToString().Equals("2"))
                data.dvTuoi = "Tháng";
            else if (drvBenhNhan["DVTUOI"].ToString().Equals("3"))
                data.dvTuoi = "Ngày";
            else
                data.dvTuoi = "Giờ";
            data.gioiTinh = drvBenhNhan["GIOITINHID"].ToString().Equals("1") ? "Nam" : (drvBenhNhan["GIOITINHID"].ToString().Equals("2") ? "Nữ" : "Khác");
            data.diaChi = drvBenhNhan["DIACHI"].ToString();
            data.checkVp = drvBenhNhan["DOITUONGBENHNHANID"].ToString().Equals("2") ? "X" : "";
            data.maKCBBD = drvBenhNhan["DKKCBBDID"].ToString();
            

            if (drvBenhNhan["DOITUONGBENHNHANID"].ToString().Equals("1"))
            {
                string soThe = drvBenhNhan["MATHEBHYT"].ToString();
                data.soThe1 = soThe.Substring(0, 3).ToUpper();
                data.soThe2 = soThe.Substring(3, 2).ToUpper();
                data.soThe3 = soThe.Substring(5, 2).ToUpper();
                data.soThe4 = soThe.Substring(7, 3).ToUpper();
                data.soThe5 = soThe.Substring(10, 5).ToUpper();
                data.hanThe = "Hạn thẻ từ: " + drvBenhNhan["THOIGIAN_BD"].ToString() + " đến " + drvBenhNhan["THOIGIAN_KT"].ToString();
            }
            else
            {
                data.soThe1 = "";
                data.soThe2 = "";
                data.soThe3 = "";
                data.soThe4 = "";
                data.soThe5 = "";
                data.hanThe = "";
            }

            data.chanDoan = "Chẩn đoán: " + drvBenhNhan["CHANDOANRAVIEN"].ToString() + " (" + drvBenhNhan["MACHANDOANRAVIEN"].ToString() + ")";
            data.chanDoanPhu = "Chẩn đoán phụ: " + drvBenhNhan["CHANDOANRAVIEN_KT"].ToString();


            DataRowView drMbp = gridCDThuoc_MBP.SelectedRow;
            DataTable dtDSCD = (DataTable)gridCDDV_DSCD.gridControl.DataSource;

            //data.soPhieuThuoc = drMbp["SOPHIEU"].ToString();
            //string ngayMbp = drMbp["NGAYMAUBENHPHAM"].ToString();
            //   data.ngayMauBenhPham = "Ngày " + ngayMbp.Substring(0, 2) + " Tháng " + ngayMbp.Substring(3, 2) + " Năm " + ngayMbp.Substring(6, 4);
            //string sqlQuery = "SELECT OFFICER_NAME FROM ADM_USER WHERE USER_ID = '" + drMbp["NGUOITAO"].ToString() + "'";
            //DataTable dtSql;
            //LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtSql);
            //data.nguoiTao = dtSql.Rows[0]["OFFICER_NAME"].ToString().ToUpper();

            // "INSERT INTO KBH_MAUBENHPHAM (SOPHIEU, LOAINHOMMAUBENHPHAM, BENHNHANID, NGAYMAUBENHPHAM, NGUOITAO, KHOTHUOCID) 
            // "INSERT INTO KBH_DICHVU_KHAMBENH (MAUBENHPHAMID, BENHNHANID, NGAYDICHVU, DICHVUID, TENDICHVU, SOLUONG, DONGIA, GIABHYT, GIAVP, GIADV, TYLEDV, BHYTTRA, NHANDANTRA, LOAIDOITUONG, GHICHU) VALUES (";

            //sqlQuery = "SELECT DV.TEN_DVT, TVT.HOATCHAT FROM DUC_THUOCVATTU TVT, DUC_DONVITINH DV WHERE TVT.DONVITINHID = DV.DONVITINHID AND TVT.THUOCVATTUID = '" + dr["DICHVUID"].ToString() + "'";
            //LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtSql);
            //data.tenDichVu = dtSql.Rows[0]["HOATCHAT"].ToString() + " (" + dr["TENDICHVU"].ToString().ToUpper() + ")";
            //data.cachDung = dr["GHICHU"].ToString();

            #endregion

            data.list = new List<Report_CPKB>();

            #region CP khám bệnh
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(
                //"SELECT a.*,b.* FROM KBH_DICHVU_KHAMBENH a, DMC_DICHVU b WHERE a.BENHNHANID = " + drvBenhNhan["BENHNHANID"].ToString() + " AND a.LOAIDOITUONG='' and a.DICHVUID=b.DICHVUID "
                "SELECT a.*  FROM KBH_DICHVU_KHAMBENH a WHERE a.BENHNHANID = " + drvBenhNhan["BENHNHANID"].ToString() + " AND a.LOAIDOITUONG=''  "
                , out dtTemp); 

            Report_CPKB cp_khambenh = new Report_CPKB("Khám bệnh:");

            cp_khambenh.STT = "1";
            cp_khambenh.STT_ABC = "A";
            cp_khambenh.TEN_KHOA = data.orgKhoa;

            cp_khambenh.chitiet = new List<Report_CPKB_Detail>();

            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                Report_CPKB_Detail chi_tiet = new Report_CPKB_Detail();
                chi_tiet.STT = (i + 1) + "";

                chi_tiet.DETAIL_TEN_NOI_DUNG= dtTemp.Rows[0]["TENDICHVU"].ToString();
                //chi_tiet.DETAIL_DON_VI= dtTemp.Rows[0]["donvi"].ToString();
                chi_tiet.DETAIL_SL= Func.Parse_float(dtTemp.Rows[0]["soluong"].ToString());

                chi_tiet.DETAIL_DON_GIA_BV= Func.Parse_float(dtTemp.Rows[0]["giaDV"].ToString());
                chi_tiet.DETAIL_DON_GIA_BH= Func.Parse_float(dtTemp.Rows[0]["giaBHYT"].ToString());

                chi_tiet.DETAIL_TYLE_DV= dtTemp.Rows[0]["TYLEDV"].ToString()=="1"? 100: Func.Parse_float(dtTemp.Rows[0]["TYLEDV"].ToString());
                chi_tiet.DETAIL_TYLE_BHYT = 100- chi_tiet.DETAIL_TYLE_DV;

                chi_tiet.DETAIL_THANH_TIEN_BV= (chi_tiet.DETAIL_DON_GIA_BV* chi_tiet.DETAIL_SL);
                chi_tiet.DETAIL_THANH_TIEN_BH = (chi_tiet.DETAIL_DON_GIA_BH * chi_tiet.DETAIL_SL);

                chi_tiet.DETAIL_QUY_BHYT = chi_tiet.DETAIL_TYLE_BHYT * chi_tiet.DETAIL_THANH_TIEN_BH;
                chi_tiet.DETAIL_NGUOI_BENH_CUNG_TRA = 0;
                chi_tiet.DETAIL_KHAC=0;
                chi_tiet.DETAIL_NGUOI_BENH_TU_TRA = Func.Parse_float(dtTemp.Rows[0]["nhandantra"].ToString());
                //Func.Parse_float(dtTemp.Rows[0]["nhandantra"].ToString());

                cp_khambenh.chitiet.Add(chi_tiet);

                cp_khambenh.TONG_THANH_TIEN_BV += chi_tiet.DETAIL_THANH_TIEN_BV;
                cp_khambenh.TONG_THANH_TIEN_BH += chi_tiet.DETAIL_THANH_TIEN_BH;
                cp_khambenh.TONG_QUY_BHYT += chi_tiet.DETAIL_QUY_BHYT;
                cp_khambenh.TONG_NGUOI_BENH_CUNG_TRA += chi_tiet.DETAIL_NGUOI_BENH_CUNG_TRA;
                cp_khambenh.TONG_KHAC += chi_tiet.DETAIL_KHAC;
                cp_khambenh.TONG_NGUOI_BENH_TU_TRA += chi_tiet.DETAIL_NGUOI_BENH_TU_TRA; 
            }
            data.list.Add(cp_khambenh);

            data.ALL_TONG_THANH_TIEN_BV  += cp_khambenh.TONG_THANH_TIEN_BV;
            data.ALL_TONG_THANH_TIEN_BH += cp_khambenh.TONG_THANH_TIEN_BH;
            data.ALL_TONG_QUY_BHYT += cp_khambenh.TONG_QUY_BHYT;
            data.ALL_TONG_NGUOI_BENH_CUNG_TRA += cp_khambenh.TONG_NGUOI_BENH_CUNG_TRA;
            data.ALL_TONG_KHAC += cp_khambenh.TONG_KHAC;
            data.ALL_TONG_NGUOI_BENH_TU_TRA += cp_khambenh.TONG_NGUOI_BENH_TU_TRA;
            #endregion

            #region CP dịch vụ
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(
               "SELECT a.*  FROM KBH_DICHVU_KHAMBENH a  WHERE a.BENHNHANID = " + drvBenhNhan["BENHNHANID"].ToString()
               + " AND a.LOAIDOITUONG !='' and a.ghichu=''  "
               //"SELECT a.*,b.* FROM KBH_DICHVU_KHAMBENH a, DMC_DICHVU b WHERE a.BENHNHANID = " + drvBenhNhan["BENHNHANID"].ToString()
               //+ " AND a.LOAIDOITUONG !='' and a.ghichu='' and a.DICHVUID=b.DICHVUID "
               , out dtTemp);

            Report_CPKB cp_dichvu = new Report_CPKB("Dịch vụ");

            cp_dichvu.STT = "1";
            cp_dichvu.STT_ABC = "A";
            cp_dichvu.TEN_KHOA = data.orgKhoa; 

            cp_dichvu.chitiet = new List<Report_CPKB_Detail>();
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                Report_CPKB_Detail chi_tiet = new Report_CPKB_Detail();
                chi_tiet.STT = (i + 1) + "";

                chi_tiet.DETAIL_TEN_NOI_DUNG = dtTemp.Rows[0]["TENDICHVU"].ToString();
                //chi_tiet.DETAIL_DON_VI = dtTemp.Rows[0]["donvi"].ToString();
                chi_tiet.DETAIL_SL = Func.Parse_float(dtTemp.Rows[0]["soluong"].ToString());

                chi_tiet.DETAIL_DON_GIA_BV = Func.Parse_float(dtTemp.Rows[0]["giaDV"].ToString());
                chi_tiet.DETAIL_DON_GIA_BH = Func.Parse_float(dtTemp.Rows[0]["giaBHYT"].ToString());

                chi_tiet.DETAIL_TYLE_DV = dtTemp.Rows[0]["TYLEDV"].ToString() == "1" ? 100 : Func.Parse_float(dtTemp.Rows[0]["TYLEDV"].ToString());
                chi_tiet.DETAIL_TYLE_BHYT = 100 - chi_tiet.DETAIL_TYLE_DV;

                chi_tiet.DETAIL_THANH_TIEN_BV = (chi_tiet.DETAIL_DON_GIA_BV * chi_tiet.DETAIL_SL);
                chi_tiet.DETAIL_THANH_TIEN_BH = (chi_tiet.DETAIL_DON_GIA_BH * chi_tiet.DETAIL_SL);

                chi_tiet.DETAIL_QUY_BHYT = chi_tiet.DETAIL_TYLE_BHYT * chi_tiet.DETAIL_THANH_TIEN_BH;
                chi_tiet.DETAIL_NGUOI_BENH_CUNG_TRA = 0;
                chi_tiet.DETAIL_KHAC = 0;
                chi_tiet.DETAIL_NGUOI_BENH_TU_TRA = Func.Parse_float(dtTemp.Rows[0]["nhandantra"].ToString());
                //Func.Parse_float(dtTemp.Rows[0]["nhandantra"].ToString());

                cp_dichvu.chitiet.Add(chi_tiet);

                cp_dichvu.TONG_THANH_TIEN_BV += chi_tiet.DETAIL_THANH_TIEN_BV;
                cp_dichvu.TONG_THANH_TIEN_BH += chi_tiet.DETAIL_THANH_TIEN_BH;
                cp_dichvu.TONG_QUY_BHYT += chi_tiet.DETAIL_QUY_BHYT;
                cp_dichvu.TONG_NGUOI_BENH_CUNG_TRA += chi_tiet.DETAIL_NGUOI_BENH_CUNG_TRA;
                cp_dichvu.TONG_KHAC += chi_tiet.DETAIL_KHAC;
                cp_dichvu.TONG_NGUOI_BENH_TU_TRA += chi_tiet.DETAIL_NGUOI_BENH_TU_TRA;
            }

            if (dtTemp.Rows.Count > 0)
            {
                data.list.Add(cp_dichvu);

                data.ALL_TONG_THANH_TIEN_BV += cp_dichvu.TONG_THANH_TIEN_BV;
                data.ALL_TONG_THANH_TIEN_BH += cp_dichvu.TONG_THANH_TIEN_BH;
                data.ALL_TONG_QUY_BHYT += cp_dichvu.TONG_QUY_BHYT;
                data.ALL_TONG_NGUOI_BENH_CUNG_TRA += cp_dichvu.TONG_NGUOI_BENH_CUNG_TRA;
                data.ALL_TONG_KHAC += cp_dichvu.TONG_KHAC;
                data.ALL_TONG_NGUOI_BENH_TU_TRA += cp_dichvu.TONG_NGUOI_BENH_TU_TRA;
            }
            #endregion

            #region CP thuốc men
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(
               "SELECT a.*  FROM KBH_DICHVU_KHAMBENH a  WHERE a.BENHNHANID = " + drvBenhNhan["BENHNHANID"].ToString()
               + " AND a.LOAIDOITUONG !='' and a.ghichu != ''   "
               //"SELECT a.*,b.* FROM KBH_DICHVU_KHAMBENH a, DMC_DICHVU b WHERE a.BENHNHANID = " + drvBenhNhan["BENHNHANID"].ToString()
               //+ " AND a.LOAIDOITUONG !='' and a.ghichu != '' and a.DICHVUID=b.DICHVUID "
               , out dtTemp);
            Report_CPKB cp_thuoc = new Report_CPKB("Thuốc");

            cp_thuoc.STT = "1";
            cp_thuoc.STT_ABC = "A";
            cp_thuoc.TEN_KHOA = data.orgKhoa;

            cp_thuoc.chitiet = new List<Report_CPKB_Detail>();
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                Report_CPKB_Detail chi_tiet = new Report_CPKB_Detail();
                chi_tiet.STT = (i + 1) + "";

                chi_tiet.DETAIL_TEN_NOI_DUNG = dtTemp.Rows[0]["TENDICHVU"].ToString();
                //chi_tiet.DETAIL_DON_VI = dtTemp.Rows[0]["donvi"].ToString();
                chi_tiet.DETAIL_SL = Func.Parse_float(dtTemp.Rows[0]["soluong"].ToString());

                chi_tiet.DETAIL_DON_GIA_BV = Func.Parse_float(dtTemp.Rows[0]["giaDV"].ToString());
                chi_tiet.DETAIL_DON_GIA_BH = Func.Parse_float(dtTemp.Rows[0]["giaBHYT"].ToString());

                chi_tiet.DETAIL_TYLE_DV = dtTemp.Rows[0]["TYLEDV"].ToString() == "1" ? 100 : Func.Parse_float(dtTemp.Rows[0]["TYLEDV"].ToString());
                chi_tiet.DETAIL_TYLE_BHYT = 100 - chi_tiet.DETAIL_TYLE_DV;

                chi_tiet.DETAIL_THANH_TIEN_BV = (chi_tiet.DETAIL_DON_GIA_BV * chi_tiet.DETAIL_SL);
                chi_tiet.DETAIL_THANH_TIEN_BH = (chi_tiet.DETAIL_DON_GIA_BH * chi_tiet.DETAIL_SL);

                chi_tiet.DETAIL_QUY_BHYT = chi_tiet.DETAIL_TYLE_BHYT * chi_tiet.DETAIL_THANH_TIEN_BH;
                chi_tiet.DETAIL_NGUOI_BENH_CUNG_TRA = 0;
                chi_tiet.DETAIL_KHAC = 0;
                chi_tiet.DETAIL_NGUOI_BENH_TU_TRA = Func.Parse_float(dtTemp.Rows[0]["nhandantra"].ToString());
                //Func.Parse_float(dtTemp.Rows[0]["nhandantra"].ToString());

                cp_thuoc.chitiet.Add(chi_tiet);

                cp_thuoc.TONG_THANH_TIEN_BV += chi_tiet.DETAIL_THANH_TIEN_BV;
                cp_thuoc.TONG_THANH_TIEN_BH += chi_tiet.DETAIL_THANH_TIEN_BH;
                cp_thuoc.TONG_QUY_BHYT += chi_tiet.DETAIL_QUY_BHYT;
                cp_thuoc.TONG_NGUOI_BENH_CUNG_TRA += chi_tiet.DETAIL_NGUOI_BENH_CUNG_TRA;
                cp_thuoc.TONG_KHAC += chi_tiet.DETAIL_KHAC;
                cp_thuoc.TONG_NGUOI_BENH_TU_TRA += chi_tiet.DETAIL_NGUOI_BENH_TU_TRA;
            }

            if (dtTemp.Rows.Count > 0)
            {
                data.list.Add(cp_thuoc);

                data.ALL_TONG_THANH_TIEN_BV += cp_thuoc.TONG_THANH_TIEN_BV;
                data.ALL_TONG_THANH_TIEN_BH += cp_thuoc.TONG_THANH_TIEN_BH;
                data.ALL_TONG_QUY_BHYT += cp_thuoc.TONG_QUY_BHYT;
                data.ALL_TONG_NGUOI_BENH_CUNG_TRA += cp_thuoc.TONG_NGUOI_BENH_CUNG_TRA;
                data.ALL_TONG_KHAC += cp_thuoc.TONG_KHAC;
                data.ALL_TONG_NGUOI_BENH_TU_TRA += cp_thuoc.TONG_NGUOI_BENH_TU_TRA;
            }
            #endregion


            report_BangKe_ChiPhi_KB report = new report_BangKe_ChiPhi_KB();
            List<Report_BangKeRaVien> listData = new List<Report_BangKeRaVien>();
            listData.Add(data);
            report.DataSource = listData;

            ReportPrintTool tool = new ReportPrintTool(report);
            tool.PreviewRibbonForm.PrintControl.ExecCommand(DevExpress.XtraPrinting.PrintingSystemCommand.ZoomToWholePage, null);
            tool.ShowRibbonPreviewDialog();

        }
        #region Các hàm lấy dl cho bảng kê chi phí ra viện


        #endregion
    }
}
