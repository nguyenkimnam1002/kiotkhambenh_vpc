using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using DevExpress.XtraGrid.Views.Grid;

namespace MainForm
{
    public partial class formKeDV : DevExpress.XtraEditors.XtraForm
    {
        private DataRowView drvBn;
        private string mbpId = "";
        private bool isEdit = false;

        public formKeDV()
        {
            InitializeComponent();
        }

        public void loadData(DataRowView drvBenhNhan, string MauBenhPhamId = "")
        {
            if (drvBenhNhan != null)
                drvBn = drvBenhNhan;
            if (!string.IsNullOrEmpty(MauBenhPhamId))
            {
                mbpId = MauBenhPhamId;
                isEdit = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            frm.Close();
        }

        private void formKeDV_Load(object sender, EventArgs e)
        {
            string sqlQuery = "";
            DataTable dt = new DataTable();
            //Load thông tin bệnh nhân
            txtMaBN.Text = drvBn["MABENHNHAN"].ToString();
            txtTenBN.Text = drvBn["TENBENHNHAN"].ToString();
            txtTheBhyt.Text = drvBn["MATHEBHYT"].ToString();
            txtNgayChiDinh.DateTime = DateTime.Now;
            // cbo Loại đối tượng
            dt.Columns.Add("LOAIDOITUONGID", typeof(String));
            dt.Columns.Add("TENLOAIDOITUONG", typeof(String));
            dt.Rows.Add(new string[] { "1", "BHYT" });
            dt.Rows.Add(new string[] { "4", "Viện phí" });
            dt.Rows.Add(new string[] { "6", "Dịch vụ" });
            cboLoaiDoiTuong.setData(dt, 0, 1);
            if (drvBn["DOITUONGBENHNHANID"].ToString().Equals("1"))
                cboLoaiDoiTuong.SelectValue = "1";
            else if (drvBn["DOITUONGBENHNHANID"].ToString().Equals("2"))
                cboLoaiDoiTuong.SelectValue = "4";
            else if (drvBn["DOITUONGBENHNHANID"].ToString().Equals("3"))
                cboLoaiDoiTuong.SelectValue = "6";
            
            // cbo dịch vụ
            sqlQuery = "SELECT DICHVUID, MADICHVU, TENDICHVU, DONVI, GIADICHVU, GIANHANDAN, GIABHYT, LOAINHOMDICHVU, NGAYCAPNHAT, DICHVU_BHYT_DINHMUC, NHOM_MABHYT_ID ";
            sqlQuery += "FROM DMC_DICHVU WHERE LOAIDICHVU = 0 AND LOAINHOMDICHVU IN (3,4,5)";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            ucDichVu.setData(dt, "MADICHVU", "TENDICHVU");
            ucDichVu.setColumn("MADICHVU", 0, "Mã dịch vụ", 35);
            ucDichVu.setColumn("TENDICHVU", 1, "Tên dịch vụ", 80);
            ucDichVu.setColumn("DICHVUID", -1, "", 0);
            ucDichVu.setColumn("DONVI", -1, "", 0);
            ucDichVu.setColumn("GIADICHVU", -1, "", 0);
            ucDichVu.setColumn("GIANHANDAN", -1, "", 0);
            ucDichVu.setColumn("GIABHYT", -1, "", 0);
            ucDichVu.setColumn("LOAINHOMDICHVU", -1, "", 0);
            ucDichVu.setColumn("NGAYCAPNHAT", -1, "", 0);
            ucDichVu.setColumn("DICHVU_BHYT_DINHMUC", -1, "", 0);
            ucDichVu.setColumn("NHOM_MABHYT_ID", -1, "", 0);
            ucDichVu.setEvent(ucDichVu_SelectedIndexChanged);

            // Set gridChiDinhDv
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("MADICHVU");
            dtGrid.Columns.Add("TENDICHVU");
            dtGrid.Columns.Add("PHONG_TH");
            dtGrid.Columns.Add("LOAIDOITUONG_TEXT");
            dtGrid.Columns.Add("LOAIDOITUONG");
            dtGrid.Columns.Add("SOLUONG");
            dtGrid.Columns.Add("DONGIA");
            dtGrid.Columns.Add("BHYT_TRA");
            dtGrid.Columns.Add("ND_TRA");
            dtGrid.Columns.Add("TYLEDV");
            dtGrid.Columns.Add("DICHVUID");
            dtGrid.Columns.Add("GIANHANDAN");
            dtGrid.Columns.Add("GIABHYT");
            dtGrid.Columns.Add("GIADICHVU");
            dtGrid.Columns.Add("LOAINHOMDICHVU");
            dtGrid.Columns.Add("NGAYCAPNHAT");
            dtGrid.Columns.Add("SOLUONG_OLD");
            gridChiDinhDv.DataSource = dtGrid;

            repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;

            // Load Grid dịch vụ chỉ định khi sửa mbp
            if (isEdit)
            {
                sqlQuery = "SELECT A.DICHVUID, A.MADICHVU, A.TENDICHVU, A.DONVI, A.GIADICHVU, A.GIANHANDAN, A.GIABHYT, C.PHONGTHUCHIENID, ";
                sqlQuery += "A.LOAINHOMDICHVU, A.NGAYCAPNHAT, A.DICHVU_BHYT_DINHMUC, A.NHOM_MABHYT_ID ";
                sqlQuery += "FROM DMC_DICHVU A INNER JOIN KBH_DICHVU_KHAMBENH B ON A.DICHVUID = B.DICHVUID ";
                sqlQuery += "INNER JOIN KBH_MAUBENHPHAM C ON B.MAUBENHPHAMID = C.MAUBENHPHAMID ";
                sqlQuery += "WHERE B.MAUBENHPHAMID = " + mbpId;
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRowView drDv = dt.DefaultView[i];
                    // Thêm dịch vụ load từ db -> addFromDb = true
                    addNewRow(drDv, true);
                }
            }
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }

        private void ucDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ucDichVu.SelectedIndex >= 0)
                {
                    //Lấy dữ liệu dịch vụ chỉ định
                    DataRowView drDv = (DataRowView)sender;
                    int rowIds = gridView1.DataRowCount;
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        DataRowView rowData = (DataRowView)gridView1.GetRow(i);
                        if (drDv["DICHVUID"].Equals(rowData["DICHVUID"]))
                        {
                            MessageBox.Show(drDv["TENDICHVU"].ToString() + " đã được chỉ định trong phiếu");
                            return;
                        }
                    }
                    // Thêm dịch vụ khi select dịch vụ mới trên form -> addFromDb = false
                    addNewRow(drDv, false);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void addNewRow(DataRowView drDv, bool addFromDb)
        {
            try
            {
                string sqlQuery = "";
                //Tính vienphi
                DataTable dtVp = new DataTable();
                DataTable r = new DataTable();
                dtVp.Columns.Add("DOITUONGBENHNHANID", typeof(String));
                dtVp.Columns.Add("MUCHUONG", typeof(String));
                dtVp.Columns.Add("GIATRANBH", typeof(String));
                dtVp.Columns.Add("GIABHYT", typeof(String));
                dtVp.Columns.Add("GIAND", typeof(String));
                dtVp.Columns.Add("GIADV", typeof(String));
                dtVp.Columns.Add("GIANN", typeof(String));
                dtVp.Columns.Add("DOITUONGCHUYEN", typeof(String));
                dtVp.Columns.Add("GIADVKTC", typeof(String));
                dtVp.Columns.Add("MANHOMBHYT", typeof(String));
                dtVp.Columns.Add("SOLUONG", typeof(String));
                dtVp.Columns.Add("CANTRENDVKTC", typeof(String));
                dtVp.Columns.Add("THEDUTHOIGIAN", typeof(String));
                dtVp.Columns.Add("DUOCVANCHUYEN", typeof(String));
                dtVp.Columns.Add("TYLETHUOCVATTU", typeof(String));
                dtVp.Columns.Add("NHOMDOITUONG", typeof(String));
                dtVp.Columns.Add("NGAYHANTHE", typeof(String));
                dtVp.Columns.Add("NGAYDICHVU", typeof(String));
                dtVp.Columns.Add("TYLE_MIENGIAM", typeof(String));
                DataRow drVp = dtVp.NewRow();
                drVp["DOITUONGBENHNHANID"] = drvBn["DOITUONGBENHNHANID"].ToString();
                drVp["MUCHUONG"] = drvBn["TYLEBH"].ToString();
                drVp["GIATRANBH"] = !string.IsNullOrEmpty(drDv["DICHVU_BHYT_DINHMUC"].ToString()) ? drDv["DICHVU_BHYT_DINHMUC"].ToString() : "0";
                drVp["GIABHYT"] = drDv["GIABHYT"].ToString();
                drVp["GIAND"] = drDv["GIANHANDAN"].ToString();
                drVp["GIADV"] = drDv["GIADICHVU"].ToString();
                drVp["GIANN"] = "0";
                drVp["DOITUONGCHUYEN"] = cboLoaiDoiTuong.SelectValue;
                drVp["GIADVKTC"] = "0";
                sqlQuery = "SELECT * FROM DMC_NHOM_MABHYT WHERE NHOM_MABHYT_ID=" + drDv["NHOM_MABHYT_ID"].ToString();
                DataTable dtMaNhomBhyt = new DataTable();
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtMaNhomBhyt);
                if (dtMaNhomBhyt.Rows.Count > 0)
                    drVp["MANHOMBHYT"] = dtMaNhomBhyt.Rows[0]["MANHOM_BHYT"].ToString();
                drVp["SOLUONG"] = "1";
                drVp["CANTRENDVKTC"] = "0";
                drVp["THEDUTHOIGIAN"] = "0";
                drVp["DUOCVANCHUYEN"] = "0";
                drVp["TYLETHUOCVATTU"] = "100";
                drVp["NHOMDOITUONG"] = "0";
                drVp["NGAYHANTHE"] = drvBn["THOIGIAN_KT"].ToString();
                drVp["NGAYDICHVU"] = drvBn["NGAYKHAM"].ToString().Substring(0, 10);
                drVp["TYLE_MIENGIAM"] = drvBn["TYLEMIENGIAM"].ToString();
                dtVp.Rows.InsertAt(drVp, 0);
                r = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtVp);
                DataRow r_dr = r.Rows[0];
                if (r_dr["bh_tra"].ToString() == "-1" && r_dr["nd_tra"].ToString() == "-1" && r_dr["tong_cp"].ToString() == "-1")
                {
                    MessageBox.Show("Giá tiền dịch vụ của bệnh nhân không thể bằng 0");
                    return;
                }

                gridView1.AddNewRow();
                int rowHandle = gridView1.DataRowCount;
                gridView1.SetFocusedRowCellValue("MADICHVU", drDv["MADICHVU"].ToString());
                gridView1.SetFocusedRowCellValue("TENDICHVU", drDv["TENDICHVU"].ToString());
                gridView1.SetFocusedRowCellValue("LOAIDOITUONG_TEXT", cboLoaiDoiTuong.Text);
                gridView1.SetFocusedRowCellValue("LOAIDOITUONG", cboLoaiDoiTuong.SelectValue);
                gridView1.SetFocusedRowCellValue("SOLUONG", "1");
                gridView1.SetFocusedRowCellValue("DONGIA", float.Parse(r_dr["tong_cp"].ToString()).ToString("N0"));
                gridView1.SetFocusedRowCellValue("BHYT_TRA", float.Parse(r_dr["bh_tra"].ToString()).ToString("N0"));
                gridView1.SetFocusedRowCellValue("ND_TRA", float.Parse(r_dr["nd_tra"].ToString()).ToString("N0"));
                gridView1.SetFocusedRowCellValue("TYLEDV", "1");
                gridView1.SetFocusedRowCellValue("DICHVUID", drDv["DICHVUID"].ToString());
                gridView1.SetFocusedRowCellValue("GIANHANDAN", drDv["GIANHANDAN"].ToString());
                gridView1.SetFocusedRowCellValue("GIABHYT", drDv["GIABHYT"].ToString());
                gridView1.SetFocusedRowCellValue("GIADICHVU", drDv["GIADICHVU"].ToString());
                gridView1.SetFocusedRowCellValue("LOAINHOMDICHVU", drDv["LOAINHOMDICHVU"].ToString());
                gridView1.SetFocusedRowCellValue("NGAYCAPNHAT", drDv["NGAYCAPNHAT"].ToString());
                gridView1.SetFocusedRowCellValue("SOLUONG_OLD", "1");

                // Set column PHONG_TH
                DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit myLookup = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                DataTable dtPhong = new DataTable();
                sqlQuery = "SELECT ORG_ID, ORG_NAME FROM ORG_PHONG WHERE ORG_TYPE = 2";
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtPhong);
                myLookup.DataSource = dtPhong;
                myLookup.DisplayMember = "ORG_NAME";
                myLookup.ValueMember = "ORG_ID";
                myLookup.View.OptionsBehavior.AutoPopulateColumns = false;
                myLookup.NullText = "Chọn phòng thực hiện ...";
                DevExpress.XtraGrid.Columns.GridColumn col1 = myLookup.View.Columns.AddField("ORG_ID");
                col1.VisibleIndex = 0;
                col1.Caption = "Mã Phòng";
                DevExpress.XtraGrid.Columns.GridColumn col2 = myLookup.View.Columns.AddField("ORG_NAME");
                col2.VisibleIndex = 1;
                col2.Caption = "Tên Phòng";
                gridView1.Columns["PHONG_TH"].ColumnEdit = myLookup;
                if (addFromDb)
                {
                    gridView1.SetFocusedRowCellValue("PHONG_TH", drDv["PHONGTHUCHIENID"].ToString());
                }
                // Set các trường ko được chỉnh sửa
                gridView1.Columns["MADICHVU"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["TENDICHVU"].OptionsColumn.ReadOnly = true;
                gridView1.Columns["LOAIDOITUONG"].OptionsColumn.ReadOnly = true;
                gridView1.UpdateCurrentRow();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private bool KiemTraNhap()
        {
            int rowIds = gridView1.DataRowCount;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)gridView1.GetRow(i);
                if (string.IsNullOrEmpty(rowData["PHONG_TH"].ToString()))
                {
                    MessageBox.Show("Mã dịch vụ " + rowData["MADICHVU"].ToString() + " chưa chỉ định phòng thực hiện");
                    return false;
                }
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)gridChiDinhDv.DataSource;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa chỉ định dịch vụ");
                    return;
                }
                Dictionary<string, DataTable> map = new Dictionary<string, DataTable>();
                if (KiemTraNhap() == false) return;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    string loaiNhomMbp = "0";
                    DataRow rowData = dt.Rows[i];
                    //Loại nhóm Mbp: 1 xét nghiệm, 2 là cdha, 5 là chuyên khoa
                    if (rowData["LOAINHOMDICHVU"].ToString() == "3")
                        loaiNhomMbp = "1";
                    else
                        loaiNhomMbp = rowData["LOAINHOMDICHVU"].ToString() == "4" ? "2" : "5";
                    DataTable dtList = dt.Clone();
                    if (map.ContainsKey(rowData["PHONG_TH"].ToString() + "," + loaiNhomMbp + ","))
                    {
                        dtList = (DataTable)map[rowData["PHONG_TH"].ToString() + "," + loaiNhomMbp + ","];
                        var drRow = dtList.NewRow();
                        drRow.ItemArray = rowData.ItemArray;
                        dtList.Rows.InsertAt(drRow, 0);
                        map[rowData["PHONG_TH"].ToString() + "," + loaiNhomMbp + ","] = dtList;
                    }
                    else
                    {
                        var drRow = dtList.NewRow();
                        drRow.ItemArray = rowData.ItemArray;
                        dtList.Rows.InsertAt(drRow, 0);
                        map.Add(rowData["PHONG_TH"].ToString() + "," + loaiNhomMbp + ",", dtList);
                    }
                }
                MauBenhPhamObj mbp = new MauBenhPhamObj();
                mbp.BENHNHANID = drvBn["BENHNHANID"].ToString();
                mbp.NGAYMAUBENHPHAM = txtNgayChiDinh.DateTime.ToString("dd/MM/yyyy");
                mbp.NGUOITAO = drvBn["BACSIDIEUTRIID"].ToString();
                if (isEdit)
                    mbp.MAUBENHPHAMID = mbpId;

                bool result = LocalConst.LOCAL_SQLITE.sqliteTransaction_ChiDinhDichVu(map, mbp, isEdit);
                if (result)
                {
                    evenChange(1, null);
                    Form frm = this.FindForm();
                    frm.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi chỉ định dịch vụ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        // Hàm gửi sự kiện để reload formMain sau khi chỉ định dịch vụ
        protected EventHandler evenChange;
        public void setEvent(EventHandler eventChangeValue)
        {
            evenChange = eventChangeValue;
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DataRowView rowData = (DataRowView)gridView1.GetRow(e.RowHandle);
            if (rowData.Row != null && e.Column.FieldName == "SOLUONG")
            {
                float value;
                float oldValue = float.Parse(rowData["SOLUONG_OLD"].ToString());
                bool isFloat = float.TryParse(rowData["SOLUONG"].ToString(), out value);
                if (isFloat && float.Parse(rowData["SOLUONG"].ToString()) > 0 && float.Parse(rowData["SOLUONG"].ToString()) != oldValue)
                {
                    if (value < 1 && value != 0.5)
                    {
                        gridView1.SetRowCellValue(e.RowHandle, "SOLUONG", oldValue.ToString());
                    }
                    else if (value > 99)
                    {
                        MessageBox.Show("Số lượng kê dịch vụ quá lớn!");
                        gridView1.SetRowCellValue(e.RowHandle, "SOLUONG", oldValue.ToString());
                    }
                    else
                    {
                        //loadChange(oldValue, value, e.RowHandle); chưa làm
                        gridView1.SetRowCellValue(e.RowHandle, "DONGIA", (float.Parse(rowData["DONGIA"].ToString()) / oldValue * value).ToString("N0"));
                        gridView1.SetRowCellValue(e.RowHandle, "BHYT_TRA", (float.Parse(rowData["BHYT_TRA"].ToString()) / oldValue * value).ToString("N0"));
                        gridView1.SetRowCellValue(e.RowHandle, "ND_TRA", (float.Parse(rowData["ND_TRA"].ToString()) / oldValue * value).ToString("N0"));
                        gridView1.SetRowCellValue(e.RowHandle, "SOLUONG_OLD", value.ToString());
                    }
                }
                else
                    gridView1.SetRowCellValue(e.RowHandle, "SOLUONG", oldValue.ToString());
            }
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {

        }
    }
}