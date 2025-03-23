using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.QTHethong.Class;
using VNPT.HIS.Common;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace VNPT.HIS.QTHethong
{
    public partial class NGT02K055_CachDungThuoc : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
              log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ResponsList ds = new ResponsList();
        DataTable dt = new DataTable();
        
        private bool isEdit = false;
        private string selectedID;
        #endregion

        #region Init Form
        public NGT02K055_CachDungThuoc()
        {
            InitializeComponent();
        }

        private void NGT02K055_CachDungThuoc_Load(object sender, EventArgs e)
        {
            Form_Init();
            ResetData();
            ResetForm("Load");
        }
        private void Form_Init()
        {
            DataTable dtCB = Func.getTableEmpty(new string[] { "col1", "col2" });
            DataRow drkt = dtCB.NewRow();
            drkt["col1"] = "1";
            drkt["col2"] = "Cách dùng";
            dtCB.Rows.Add(drkt);

            drkt = dtCB.NewRow();
            drkt["col1"] = "2";
            drkt["col2"] = "Thời gian dùng";
            dtCB.Rows.Add(drkt);

            cbLoai.setData(dtCB, 0, 1);
            cbLoai.setColumnAll(false);
            cbLoai.setColumn(1, true);
            cbLoai.SelectIndex = 0;


            ucGrid_Ds.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
            ucGrid_Ds.gridView.OptionsView.ColumnAutoWidth = true;
            ucGrid_Ds.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_Ds.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGrid_Ds.setEvent(getData_table);
            ucGrid_Ds.setEvent_FocusedRowChanged(change_selectRow);
            ucGrid_Ds.gridView.OptionsBehavior.ReadOnly = true;
            ucGrid_Ds.SetReLoadWhenFilter(true);
            ucGrid_Ds.onIndicator();
            getData_table(1, null);
        }
        #endregion

        #region Load dữ liệu

        private void getData_table(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;

            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    //// Lấy điều kiện filter
                    //if (ucGrid_Ds.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_Ds.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_Ds.tableFlterColumn);
                    //}

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("DMC55.LAYDS", 1, ucGrid_Ds.ucPage1.getNumberPerPage(), new string[] { }, new string[] { }, "");
                    dt = MyJsonConvert.toDataTable(ds.rows);

                    ucGrid_Ds.clearData();
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "CACHDUNG", "GHICHU", "ID", "KHOA", "LOAI", "MA_CD", "NGAY", "NGUOIDUNGID", "NGUOITAO", "PHONG", "RN" });
                    {
                        ucGrid_Ds.setData(dt, ds.total, ds.page, ds.records);
                        ucGrid_Ds.setColumnAll(false);

                        ucGrid_Ds.setColumn("CACHDUNG", 0, "Cách dùng");
                        ucGrid_Ds.setColumn("MA_CD", 1, "Mã");
                        ucGrid_Ds.setColumn("KHOA", 2, "Khoa");
                        ucGrid_Ds.setColumn("PHONG", 3, "Phòng");
                        ucGrid_Ds.setColumn("NGUOITAO", 4, "Người tạo");
                        ucGrid_Ds.setColumn("NGAY", 5, "Ngày tạo");

                        ucGrid_Ds.gridView.BestFitColumns(true);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        #endregion

        #region Thêm, Xóa, Cập nhật
        private void btn_Them_Click(object sender, EventArgs e)
        {
            selectedID = "";
            isEdit = false;
            ResetData();
            ResetForm("Insert");
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            ResetForm("Update");
        }

        private void btn_Xoa_Click(object sender, EventArgs e) //Chưa hoàn thành
        {
            try
            {
                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn xóa chi tiết cách dùng này?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string json = "";
                    json += Func.json_item("ID", selectedID);
                    json += Func.json_item("CACHDUNG", txtCachDung.Text.Trim());
                    json += Func.json_item("MA_CD", txtMaCachDung.Text.Trim());
                    json += Func.json_item("GHICHU", txtGhiChu.Text.Trim());

                    DataRowView drvLoai = cbLoai.SelectDataRowView;
                    if (drvLoai != null)
                        json += Func.json_item("LOAI", drvLoai["col1"].ToString());

                    json = Func.json_item_end(json);
                    json = json.Replace("\"", "\\\"");

                    string ret = RequestHTTP.call_ajaxCALL_SP_I("DMC55.XOA", json);
                    if (ret == "1")
                    {
                        getData_table(1, null);
                        ResetData();
                        ResetForm("Save");
                        XtraMessageBox.Show("Xóa thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("Lỗi xóa chi tiết cách dùng", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_validate()) return;

                string json = "";
                json += Func.json_item("ID", selectedID);
                json += Func.json_item("CACHDUNG", txtCachDung.Text.Trim());
                json += Func.json_item("MA_CD", txtMaCachDung.Text.Trim());
                json += Func.json_item("GHICHU", txtGhiChu.Text.Trim());
                DataRowView drvLoai = cbLoai.SelectDataRowView;
                if (drvLoai != null)
                    json += Func.json_item("LOAI", drvLoai["col1"].ToString());

                json += Func.json_item("KHOAID", Const.local_khoaId.ToString());
                json += Func.json_item("PHONGID", Const.local_phongId.ToString());

                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"");

                if (isEdit)
                {
                    string ret = RequestHTTP.call_ajaxCALL_SP_I("DMC55.SUA", json);
                    if (ret == "1")
                    {
                        getData_table(1, null);
                        ResetData();
                        ResetForm("Save");
                        XtraMessageBox.Show("Cập nhật thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("Lỗi cập nhật thông tin cách dùng", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    string ret = RequestHTTP.call_ajaxCALL_SP_I("DMC55.THEM", json);
                    if (ret == "1")
                    {
                        getData_table(1, null);
                        ResetData();
                        ResetForm("Save");
                        XtraMessageBox.Show("Thêm mới thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show("Lỗi thêm mới cách dùng", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            selectedID = "";
            isEdit = false;
            ResetData();
            ResetForm("Cancle");
        }
        #endregion

        #region Change, Keydown, Validate
        private void change_selectRow(object sender, EventArgs e)
        {
            DataRow row = (ucGrid_Ds.gridView.GetRow(ucGrid_Ds.gridView.FocusedRowHandle) as DataRowView).Row;
            if (row == null) return;

            isEdit = true;
            selectedID = row["ID"].ToString();
            txtCachDung.Text = row["CACHDUNG"].ToString();
            txtMaCachDung.Text = row["MA_CD"].ToString();
            cbLoai.SelectValue = row["LOAI"].ToString();
            txtGhiChu.Text = row["GHICHU"].ToString();
            txtCachDung.Focus();
            ResetForm("View");
        }
        private void txtCachDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMaCachDung.Focus();
            }

        }
        private void txtMaCachDung_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                cbLoai.Focus();
            }
        }
        private void cbLoai_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtGhiChu.Focus();
            }
        }
        private void txtGhiChu_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btn_Luu.Focus();
            }
        }
        #endregion

        #region Hàm
        private bool _validate()
        {
            string CachDung = txtCachDung.Text.Trim();
            if (CachDung == "")
            {
                XtraMessageBox.Show("Vui lòng nhập cách dùng", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCachDung.Focus();
                return false;
            }
            string MaCachDung = txtMaCachDung.Text.Trim();
            if (MaCachDung == "")
            {
                XtraMessageBox.Show("Vui lòng nhập mã cách dùng", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCachDung.Focus();
                return false;
            }
            string Loai = "";
            DataRowView drvLoai = cbLoai.SelectDataRowView;
            if (drvLoai != null)
                Loai = drvLoai["col1"].ToString();

            if (Loai == "")
            {
                XtraMessageBox.Show("Vui lòng chọn loại", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoai.Focus();
                return false;
            }
            return true;
        }
        private void ResetForm(string Action)
        {
            switch (Action)
            {
                case "Load":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;
                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;

                    txtCachDung.ReadOnly = true;
                    txtMaCachDung.ReadOnly = true;
                    cbLoai.lookUpEdit.Properties.ReadOnly = true;
                    txtGhiChu.ReadOnly = true;
                    break;

                case "View":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = true;
                    btn_Xoa.Enabled = true;
                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;

                    txtCachDung.ReadOnly = true;
                    txtMaCachDung.ReadOnly = true;
                    cbLoai.lookUpEdit.Properties.ReadOnly = true;
                    txtGhiChu.ReadOnly = true;
                    break;

                case "Insert":
                    isEdit = false;
                    btn_Them.Enabled = false;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;

                    btn_Luu.Enabled = true;
                    btn_Huy.Enabled = true;

                    txtCachDung.ReadOnly = false;
                    txtMaCachDung.ReadOnly = false;
                    cbLoai.lookUpEdit.Properties.ReadOnly = false;
                    txtGhiChu.ReadOnly = false;
                    break;

                case "Update":
                    isEdit = true;
                    btn_Them.Enabled = false;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;

                    btn_Luu.Enabled = true;
                    btn_Huy.Enabled = true;

                    txtCachDung.ReadOnly = false;
                    txtMaCachDung.ReadOnly = false;
                    cbLoai.lookUpEdit.Properties.ReadOnly = false;
                    txtGhiChu.ReadOnly = false;
                    break;

                case "Cancle":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;

                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;

                    txtCachDung.ReadOnly = true;
                    txtMaCachDung.ReadOnly = true;
                    cbLoai.lookUpEdit.Properties.ReadOnly = true;
                    txtGhiChu.ReadOnly = true;
                    break;

                case "Save":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;

                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;

                    txtCachDung.ReadOnly = true;
                    txtMaCachDung.ReadOnly = true;
                    cbLoai.lookUpEdit.Properties.ReadOnly = true;
                    txtGhiChu.ReadOnly = true;
                    break;
            }
        }
        private void ResetData()
        {
            selectedID = "";
            isEdit = false;
            txtCachDung.Text = "";
            txtMaCachDung.Text = "";
            cbLoai.SelectIndex = 0;
            txtGhiChu.Text = "";

            txtCachDung.Focus();
        }
        #endregion
        
    }
}