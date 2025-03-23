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
    public partial class formKeThuoc : DevExpress.XtraEditors.XtraForm
    {
        private DataRowView drvBn;
        private string mbpId = "";
        private bool isEdit = false;

        public formKeThuoc()
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

        private void formKeThuoc_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string sqlQuery = "";
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
            cboLoaiDoiTuong.setEvent_Enter(cboLoaiDoiTuong_KeyEnter);

            //cbo Kho thuốc
            cboTenThuoc.searchLookUpEdit1.Focus();
            sqlQuery = "SELECT KHOID, TENKHO  FROM DUC_KHO WHERE TRANGTHAI = '1' AND CSYTID = '" + LocalConst.csytId + "' ORDER BY TENKHO";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            DataRow dr = dt.NewRow();
            dr[0] = "-1";
            dr[1] = "--- Chọn ---";
            dt.Rows.InsertAt(dr, 0);
            cboKhoThuoc.setData(dt, 0, 1);
            cboKhoThuoc.SelectIndex = 0;
            cboKhoThuoc.setEvent_Enter(cboKhoThuoc_KeyEnter);

            //cbo Tên thuốc
            sqlQuery = "SELECT TVT.*, DV.TEN_DVT FROM DUC_THUOCVATTU TVT, DUC_DONVITINH DV WHERE TVT.DONVITINHID = DV.DONVITINHID";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboTenThuoc.setData(dt, "THUOCVATTUID", "TEN");
            cboTenThuoc.setColumn("TEN", 0, "Tên thuốc", 0);
            cboTenThuoc.setColumn("HOATCHAT", 1, "Hoạt chất", 0);
            cboTenThuoc.setColumn("TEN_DVT", 2, "Đơn vị", 0);
            cboTenThuoc.setColumn("GIABAN", 3, "Giá DV", 0);
            cboTenThuoc.setColumn("THUOCVATTUID", -1, "", 0);
            cboTenThuoc.setColumn("MA", -1, "", 0);
            cboTenThuoc.setColumn("MAHOATCHAT", -1, "", 0);
            cboTenThuoc.setColumn("KIEU", -1, "", 0);
            cboTenThuoc.setColumn("LOAI", -1, "", 0);
            cboTenThuoc.setColumn("DONVITINHID", -1, "", 0);
            cboTenThuoc.setColumn("DUONGDUNGID", -1, "", 0);
            cboTenThuoc.setColumn("CHUY", -1, "", 0);
            cboTenThuoc.setColumn("KHOA", -1, "", 0);
            cboTenThuoc.setColumn("TYLEBHYT", -1, "", 0);
            cboTenThuoc.setColumn("GIABHYT", -1, "", 0);
            cboTenThuoc.setColumn("GIANHANDAN", -1, "", 0);
            cboTenThuoc.setColumn("GIANHAP", -1, "", 0);
            cboTenThuoc.setColumn("GIANUOCNGOAI", -1, "", 0);
            cboTenThuoc.setColumn("GIATRANBHYT", -1, "", 0);
            cboTenThuoc.setColumn("GIAVIENPHI", -1, "", 0);
            cboTenThuoc.searchLookUpEdit.Properties.PopupFormSize = new Size(850, 350);
            cboTenThuoc.setEvent_Enter(cboTenThuoc_KeyEnter);
            cboTenThuoc.searchLookUpEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboTenThuoc_KeyDown);
            cboTenThuoc.searchLookUpEdit1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboTenThuoc_MouseClick);
            cboTenThuoc.setEvent(cboTenThuoc_OnChange);

            //cbo Cách dùng
            sqlQuery = "SELECT CACHDUNG FROM KBH_CACHDUNG";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboCachDung.setData(dt, "CACHDUNG", "CACHDUNG");
            cboCachDung.setColumn("CACHDUNG", 0, "Cách dùng", 0);
            cboCachDung.setEvent(cboCachDung_OnChange);
            cboCachDung.textEdit1.KeyDown += cboCachDung_KeyDown;

            // Load gridThuoc
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("MATHUOC");
            dtGrid.Columns.Add("TENTHUOC");
            dtGrid.Columns.Add("LOAIDOITUONG_TEXT");
            dtGrid.Columns.Add("SOLUONG");
            dtGrid.Columns.Add("DONGIA");
            dtGrid.Columns.Add("BHYT_TRA");
            dtGrid.Columns.Add("ND_TRA");
            dtGrid.Columns.Add("CACHDUNG");

            dtGrid.Columns.Add("TYLEBHYT");
            dtGrid.Columns.Add("THUOCVATTUID");
            dtGrid.Columns.Add("LOAIDOITUONG");
            dtGrid.Columns.Add("GIANHANDAN");
            dtGrid.Columns.Add("GIABHYT");
            dtGrid.Columns.Add("GIADICHVU");
            dtGrid.Columns.Add("SOLUONG_OLD");
            dtGrid.Columns.Add("KHOTHUOCID");
            dtGrid.Columns.Add("LOAI");
            gridThuoc.DataSource = dtGrid;

            cboTenThuoc.Select();
            repositoryItemHyperLinkEdit1.Click += repositoryItemHyperLinkEdit1_Click;
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }

        private void btnThemThuoc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboTenThuoc.SelectedValue))
            {
                cboTenThuoc.searchLookUpEdit1.Focus();
                MessageBox.Show("Bạn phải nhập thuốc để kê đơn!");
                return;
            }
            if (string.IsNullOrEmpty(txtSoLuong.Text) || float.Parse(txtSoNgay.Text) == 0)
            {
                MessageBox.Show("Số lượng, không được để trống hoặc phải lớn hơn 0.");
                txtSoLuong.Focus();
                return;
            }

            string sqlQuery = "";
            DataRowView drThuoc = cboTenThuoc.SelectedDataRowView;
            int rowIds = gridView1.DataRowCount;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)gridView1.GetRow(i);
                if (drThuoc["THUOCVATTUID"].Equals(rowData["THUOCVATTUID"]))
                {
                    MessageBox.Show(drThuoc["THUOCVATTUID"].ToString() + " đã được chỉ định trong phiếu");
                    return;
                }
            }
            //Tính vienphi
            DataTable dtVp = new DataTable();
            DataTable r = new DataTable();
            string donGia = !string.IsNullOrEmpty(drThuoc["GIABAN"].ToString()) ? drThuoc["GIABAN"].ToString() : "0";
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
            drVp["GIATRANBH"] = !string.IsNullOrEmpty(drThuoc["GIATRANBHYT"].ToString()) ? drThuoc["GIATRANBHYT"].ToString() : "0";
            drVp["GIABHYT"] = donGia;
            drVp["GIAND"] = donGia;
            drVp["GIADV"] = donGia;
            drVp["GIANN"] = donGia;
            drVp["DOITUONGCHUYEN"] = cboLoaiDoiTuong.SelectValue;
            drVp["GIADVKTC"] = "0";

            sqlQuery = "SELECT * FROM DMC_NHOM_MABHYT WHERE NHOM_MABHYT_ID=" + drThuoc["NHOM_MABHYT_ID"].ToString();
            DataTable dtMaNhomBhyt = new DataTable();
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtMaNhomBhyt);
            if (dtMaNhomBhyt.Rows.Count > 0)
                drVp["MANHOMBHYT"] = dtMaNhomBhyt.Rows[0]["MANHOM_BHYT"].ToString();
            else
                drVp["MANHOMBHYT"] = "";
            drVp["SOLUONG"] = txtSoLuong.Text;
            drVp["CANTRENDVKTC"] = "0";
            drVp["THEDUTHOIGIAN"] = "0";
            drVp["DUOCVANCHUYEN"] = "0";
            drVp["TYLETHUOCVATTU"] = drThuoc["TYLEBHYT"].ToString();
            drVp["NHOMDOITUONG"] = "0";
            drVp["NGAYHANTHE"] = drvBn["THOIGIAN_KT"].ToString();
            drVp["NGAYDICHVU"] = drvBn["NGAYKHAM"].ToString().Substring(0, 10);
            drVp["TYLE_MIENGIAM"] = drvBn["TYLEMIENGIAM"].ToString();
            dtVp.Rows.InsertAt(drVp, 0);
            r = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtVp);
            DataRow r_dr = r.Rows[0];

            gridView1.AddNewRow();
            int rowHandle = gridView1.DataRowCount;
            gridView1.SetFocusedRowCellValue("MATHUOC", drThuoc["MA"].ToString());
            gridView1.SetFocusedRowCellValue("TENTHUOC", drThuoc["TEN"].ToString());
            gridView1.SetFocusedRowCellValue("LOAIDOITUONG_TEXT", cboLoaiDoiTuong.Text);
            gridView1.SetFocusedRowCellValue("LOAIDOITUONG", cboLoaiDoiTuong.SelectValue);
            gridView1.SetFocusedRowCellValue("SOLUONG", txtSoLuong.Text);
            gridView1.SetFocusedRowCellValue("DONGIA", float.Parse(r_dr["tong_cp"].ToString()).ToString("N0"));
            gridView1.SetFocusedRowCellValue("BHYT_TRA", float.Parse(r_dr["bh_tra"].ToString()).ToString("N0"));
            gridView1.SetFocusedRowCellValue("ND_TRA", float.Parse(r_dr["nd_tra"].ToString()).ToString("N0"));
            gridView1.SetFocusedRowCellValue("CACHDUNG", cboCachDung.SelectedText);
            gridView1.SetFocusedRowCellValue("TYLEBHYT", drThuoc["TYLEBHYT"].ToString());
            gridView1.SetFocusedRowCellValue("THUOCVATTUID", drThuoc["THUOCVATTUID"].ToString());
            gridView1.SetFocusedRowCellValue("GIANHANDAN", drThuoc["GIANHANDAN"].ToString());
            gridView1.SetFocusedRowCellValue("GIABHYT", drThuoc["GIABHYT"].ToString());
            gridView1.SetFocusedRowCellValue("GIADICHVU", drThuoc["GIAVIENPHI"].ToString());
            gridView1.SetFocusedRowCellValue("SOLUONG_OLD", "1");
            gridView1.SetFocusedRowCellValue("KHOTHUOCID", cboKhoThuoc.SelectValue);
            gridView1.SetFocusedRowCellValue("LOAI", drThuoc["LOAI"].ToString());
            gridView1.UpdateCurrentRow();

            clearform();
        }

        private void clearform()
        {
            cboTenThuoc.searchLookUpEdit1.EditValue = "";
            cboCachDung.SelectedText = "";
            cboCachDung.SelectedIndex = -1;
            txtSoNgay.Text = "";
            txtSang.Text = "";
            txtTrua.Text = "";
            txtChieu.Text = "";
            txtToi.Text = "";
            txtSoLuong.Text = "";
        }

        // Xử lý các sự kiện bấm Enter = bấm phím tab
        private float outFloat;
        private void cboKhoThuoc_KeyEnter(object sender, EventArgs e)
        {
            txtNgayChiDinh.Focus();
        }

        private void txtNgayChiDinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) cboLoaiDoiTuong.Focus();
        }

        private void cboLoaiDoiTuong_KeyEnter(object sender, EventArgs e)
        {
            cboTenThuoc.Focus();
        }

        private void cboTenThuoc_KeyEnter(object sender, EventArgs e)
        {
            txtSoNgay.Focus();
        }

        private void cboTenThuoc_KeyDown(object sender, KeyEventArgs e)
        {
            DataRowView drvKho = cboKhoThuoc.SelectDataRowView;
            if (drvKho == null || drvKho[0].ToString() == "-1")
            {
                MessageBox.Show("Hãy chọn kho thuốc/vật tư");
                cboKhoThuoc.Focus();
            }
        }

        private void cboTenThuoc_MouseClick(object sender, MouseEventArgs e)
        {
            DataRowView drvKho = cboKhoThuoc.SelectDataRowView;
            if (drvKho == null || drvKho[0].ToString() == "-1")
            {
                MessageBox.Show("Hãy chọn kho thuốc/vật tư");
                cboKhoThuoc.Focus();
            }
        }

        private void txtSoNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(txtSoNgay.Text) || float.Parse(txtSoNgay.Text) == 0)
                {
                    MessageBox.Show("Số ngày, không được để trống hoặc phải lớn hơn 0.");
                }
                else
                    txtSang.Focus();
            }
        }

        private void txtSoNgay_KeyPress(object sender, KeyPressEventArgs e)
        {   // check ký tự nhập là số
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void txtSoNgay_TextChanged(object sender, EventArgs e)
        {   // check regex chuỗi nhập bắt đầu bằng số
            if (System.Text.RegularExpressions.Regex.IsMatch(txtSoNgay.Text, "  ^ [0-9]"))
                txtSoNgay.Text = "";
        }

        private void txtSang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(txtSang.Text) || !float.TryParse(txtSang.Text, out outFloat))
                    txtSang.Text = "0";
                txtTrua.Focus();
            }
        }

        private void txtTrua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(txtTrua.Text) || !float.TryParse(txtTrua.Text, out outFloat))
                    txtTrua.Text = "0";
                txtChieu.Focus();
            }
        }

        private void txtChieu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(txtChieu.Text) || !float.TryParse(txtChieu.Text, out outFloat))
                    txtChieu.Text = "0";
                txtToi.Focus();
            }
        }

        private void txtToi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (string.IsNullOrEmpty(txtToi.Text) || !float.TryParse(txtToi.Text, out outFloat))
                    txtToi.Text = "0";
                txtSoLuong.Focus();
                loadCachDung();
            }
        }

        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                cboCachDung.Focus();
            }
        }

        private void txtSoLuong_Click(object sender, EventArgs e)
        {
            loadCachDung();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtSoLuong.Text, "  ^ [0-9]"))
                txtSoLuong.Text = "";
        }

        private void cboCachDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnThemThuoc_Click(null, null);
        }

        private void loadCachDung()
        {
            try
            {
                float sang = float.TryParse(txtSang.Text, out sang) ? sang : 0;
                int lanSang = (sang > 0) ? 1 : 0;
                float trua = float.TryParse(txtTrua.Text, out trua) ? trua : 0;
                int lanTrua = (trua > 0) ? 1 : 0;
                float chieu = float.TryParse(txtChieu.Text, out chieu) ? chieu : 0;
                int lanChieu = (chieu > 0) ? 1 : 0;
                float toi = float.TryParse(txtToi.Text, out toi) ? toi : 0;
                int lanToi = (toi > 0) ? 1 : 0;
                int tongLan = lanSang + lanTrua + lanChieu + lanToi;
                if (string.IsNullOrEmpty(txtSoNgay.Text) || float.Parse(txtSoNgay.Text) == 0)
                {
                    MessageBox.Show("Số ngày, không được để trống hoặc phải lớn hơn 0.");
                    txtSoNgay.Focus();
                    return;
                }
                float soNgay = float.Parse(txtSoNgay.Text);
                double total = Math.Round(((sang + trua + chieu + toi) * soNgay), 0, MidpointRounding.AwayFromZero);
                txtSoLuong.Text = total.ToString();

                //cboCachDung.textEdit1.Text = (soNgay + " ngày, " + "Ngày " + (total / soNgay) + " " + drvThuoc["TEN_DVT"].ToString() + " chia " + tongLan);
                string dvt = "";
                DataRowView drvThuoc = null;

                if (cboTenThuoc.searchLookUpEdit1.EditValue != null)
                {
                    drvThuoc = cboTenThuoc.SelectedDataRowView;
                }
                if (drvThuoc != null && !string.IsNullOrEmpty(drvThuoc["TEN_DVT"].ToString()))
                    dvt = drvThuoc["TEN_DVT"].ToString();
                string ghiChu = "";
                string cachDung = cboCachDung.SelectedText;

                if (sang != 0)
                    ghiChu += "Sáng " + sang + " " + dvt;
                if (trua != 0)
                {
                    if (string.IsNullOrEmpty(ghiChu))
                    {
                        ghiChu += "Trưa " + trua + " " + dvt;
                    }
                    else
                    {
                        ghiChu += ", Trưa " + trua + " " + dvt;
                    }
                }
                if (chieu != 0)
                {
                    if (string.IsNullOrEmpty(ghiChu))
                    {
                        ghiChu += "Chiều " + chieu + " " + dvt;
                    }
                    else
                    {
                        ghiChu += ", Chiều " + chieu + " " + dvt;
                    }
                }
                if (toi != 0)
                {
                    if (string.IsNullOrEmpty(ghiChu))
                    {
                        ghiChu += "Tối " + toi + " " + dvt;
                    }
                    else
                    {
                        ghiChu += ", Tối " + toi + " " + dvt;
                    }
                }
                if (!string.IsNullOrEmpty(cachDung))
                    ghiChu = cachDung + ", " + ghiChu;

                if (soNgay > 0)
                {
                    ghiChu = soNgay + " ngày, " + ghiChu;
                }
                cboCachDung.SelectedText = ghiChu;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void cboCachDung_OnChange(object sender, EventArgs e)
        {
            loadCachDung();
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            try
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
                            MessageBox.Show("Số lượng kê thuốc quá lớn!");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void cboTenThuoc_OnChange(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (!string.IsNullOrEmpty(cboTenThuoc.SelectedValue))
                {
                    cboCachDung.SelectedText = "";
                    cboCachDung.SelectedIndex = -1;
                    txtSoNgay.Focus();
                }
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private bool KiemTraNhap()
        {
            DataRowView drvKho = cboKhoThuoc.SelectDataRowView;
            if (drvKho == null || drvKho[0].ToString() == "-1")
            {
                MessageBox.Show("Hãy chọn kho thuốc/vật tư");
                cboKhoThuoc.Focus();
                return false;
            }

            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)gridThuoc.DataSource;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa chỉ định thuốc");
                    return;
                }
                Dictionary<string, DataTable> map = new Dictionary<string, DataTable>();
                if (KiemTraNhap() == false) return;
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    string loaiNhomMbp = "0";
                    DataRow rowData = dt.Rows[i];
                    string loai = rowData["LOAI"].ToString();
                    if (loai.Equals("1") || loai.Equals("2"))
                        loaiNhomMbp = "8";
                    else
                        loaiNhomMbp = "7";
                    DataTable dtList = dt.Clone();
                    if (map.ContainsKey(rowData["KHOTHUOCID"].ToString() + "," + loaiNhomMbp + ","))
                    {
                        dtList = (DataTable)map[rowData["KHOTHUOCID"].ToString() + "," + loaiNhomMbp + ","];
                        var drRow = dtList.NewRow();
                        drRow.ItemArray = rowData.ItemArray;
                        dtList.Rows.InsertAt(drRow, 0);
                        map[rowData["KHOTHUOCID"].ToString() + "," + loaiNhomMbp + ","] = dtList;
                    }
                    else
                    {
                        var drRow = dtList.NewRow();
                        drRow.ItemArray = rowData.ItemArray;
                        dtList.Rows.InsertAt(drRow, 0);
                        map.Add(rowData["KHOTHUOCID"].ToString() + "," + loaiNhomMbp + ",", dtList);
                    }
                }
                MauBenhPhamObj mbp = new MauBenhPhamObj();
                mbp.BENHNHANID = drvBn["BENHNHANID"].ToString();
                mbp.NGAYMAUBENHPHAM = txtNgayChiDinh.DateTime.ToString("dd/MM/yyyy");
                mbp.NGUOITAO = drvBn["BACSIDIEUTRIID"].ToString();
                if (isEdit)
                    mbp.MAUBENHPHAMID = mbpId;

                bool result = LocalConst.LOCAL_SQLITE.sqliteTransaction_ChiDinhThuoc(map, mbp, isEdit);
                if (result)
                {
                    evenChange(1, null);
                    Form frm = this.FindForm();
                    frm.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi chỉ định thuốc");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        // Hàm gửi sự kiện để reload formMain sau khi chỉ định thuốc
        protected EventHandler evenChange;
        public void setEvent(EventHandler eventChangeValue)
        {
            evenChange = eventChangeValue;
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            btnLuu_Click(1, null);
            //reportInDonThuoc();
        }
    }
}