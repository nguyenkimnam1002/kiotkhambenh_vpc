using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H038_SuaBacSiTH : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string LOAI_NHOM_MAU_BENH_PHAM;
        private static string MAU_BENH_PHAM_ID;
        private static string SELECTED_VALUE;
        private static string SELECTED_TEXT;
        private static bool ISINITIAL;
        private static DataRowView DATA_ROW_DICHVU;
        private static DataRow DATA_ROW_KQ_DICHVU;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public void SetData(string mauBenhPhamId, string mauBenhPham)
        {
            LOAI_NHOM_MAU_BENH_PHAM = mauBenhPham;
            MAU_BENH_PHAM_ID = mauBenhPhamId;
        }

        public NTU01H038_SuaBacSiTH()
        {
            InitializeComponent();
        }

        private void NTU01H038_SuaBacSiTH_Load(object sender, EventArgs e)
        {
            InitGridDichVu();
            InitGridKQDichVu();
            LoadCombobox();
        }

        private void InitGridDichVu()
        {
            try
            {
                ucGridDichVu.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridDichVu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridDichVu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridDichVu.gridView.OptionsBehavior.Editable = false;
                ucGridDichVu.gridView.OptionsView.ShowAutoFilterRow = false;

                ucGridDichVu.setEvent(LoadGridDichVu);
                ucGridDichVu.setEvent_FocusedRowChanged(LoadGridKQDichVu);
                ucGridDichVu.onIndicator();
                ucGridDichVu.setNumberPerPage(new int[] { 10, 20, 30 });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitGridKQDichVu()
        {
            try
            {
                ucGridKQDichVu.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridKQDichVu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridKQDichVu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridKQDichVu.gridView.OptionsBehavior.Editable = false;
                ucGridKQDichVu.gridView.OptionsView.ShowAutoFilterRow = false;

                ucGridKQDichVu.setEvent_FocusedRowChanged(GetFocusRow);
                ucGridKQDichVu.setNumberPerPage(new int[] { 10, 20, 30 });
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void LoadGridDichVu(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadGridDVData(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void LoadGridDVData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NTU01H036.EV001", page, ucGridDichVu.ucPage1.getNumberPerPage(),
                    new string[] { "[0]" },
                    new string[] { MAU_BENH_PHAM_ID }, ucGridDichVu.jsonFilter());

                ucGridDichVu.clearData();

                DataTable dichVuDT = MyJsonConvert.toDataTable(responses.rows);

                if (dichVuDT.Rows.Count == 0)
                    dichVuDT = Func.getTableEmpty(new String[] { "RN", "TENDICHVU", "TENDICHVUBHYT", "TIEN_BHYT", "TIEN_NHANDAN", "TIEN_DICHVU", "FULLNAME" });

                ucGridDichVu.setData(dichVuDT, responses.total, responses.page, responses.records);
                ucGridDichVu.setColumnAll(false);

                ucGridDichVu.setColumn("RN", 0, " ", 0);
                ucGridDichVu.setColumn("TENDICHVU", 1, "Tên DVBH", 0);
                ucGridDichVu.setColumn("TENDICHVUBHYT", 2, "Tên DVBH", 0);
                ucGridDichVu.setColumn("TIEN_BHYT", 3, "Giá BHYT", 0);
                ucGridDichVu.setColumn("TIEN_NHANDAN", 4, "Giá ND", 0);
                ucGridDichVu.setColumn("TIEN_DICHVU", 5, "Tiền DV", 0);
                ucGridDichVu.setColumn("FULLNAME", 6, "Bác sĩ thực thi", 0);

                ucGridDichVu.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void LoadGridKQDichVu(object sender, EventArgs e)
        {
            try
            {
                DATA_ROW_DICHVU = (DataRowView)sender;
                LoadGridKQDVData(1);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void LoadGridKQDVData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                DATA_ROW_KQ_DICHVU = null;

                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NTU01H038.EV001", page, ucGridKQDichVu.ucPage1.getNumberPerPage(),
                    new string[] { "[0]" },
                    new string[] { DATA_ROW_DICHVU["DICHVUKHAMBENHID"].ToString() }, ucGridKQDichVu.jsonFilter());

                ucGridKQDichVu.clearData();

                DataTable dichVuDT = MyJsonConvert.toDataTable(responses.rows);

                if (dichVuDT.Rows.Count == 0)
                    dichVuDT = Func.getTableEmpty(new String[] { "RN", "MADICHVU", "TENDICHVU", "FULL_NAME" });

                ucGridKQDichVu.setData(dichVuDT, responses.total, responses.page, responses.records);
                ucGridKQDichVu.setColumnAll(false);

                ucGridKQDichVu.setColumnMemoEdit("RN", 0, " ", 0);
                ucGridKQDichVu.setColumnMemoEdit("MADICHVU", 1, "Mã DV", 0);
                ucGridKQDichVu.setColumnMemoEdit("TENDICHVU", 2, "Tên DV", 0);
                ucGridKQDichVu.setColumnMemoEdit("FULL_NAME", 3, "Bác sĩ thực hiện", 0);

                ucGridKQDichVu.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void LoadCombobox()
        {
            try
            {
                cbbBacSiThucHien.searchLookUpEdit1.EditValueChanged += EditValueChanged;
                DataTable bacSiDT = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU01H038.LOAD_USER",
                                                new string[] { Const.local_user.HOSPITAL_ID, "0" });

                cbbBacSiThucHien.setData(bacSiDT, "USER_ID", "FULLNAME");
                cbbBacSiThucHien.setColumn("RN", -1, " ", 0);
                cbbBacSiThucHien.setColumn("USER_ID", -1, " ", 0);
                cbbBacSiThucHien.setColumn("USERNAME", 0, "Tài khoản", 0);
                cbbBacSiThucHien.setColumn("FULLNAME", 1, "Tên bác sĩ", 0);

                cbbBacSiThucHien.SelectedText = "--Lựa chọn--";
                ISINITIAL = true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void GetFocusRow(object sender, EventArgs e)
        {
            try
            {
                DATA_ROW_KQ_DICHVU = ucGridKQDichVu.gridView.GetFocusedDataRow();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ISINITIAL)
                {
                    SELECTED_TEXT = cbbBacSiThucHien.SelectedText;
                    SELECTED_VALUE = cbbBacSiThucHien.SelectedValue;
                }

                ISINITIAL = false;
                cbbBacSiThucHien.SelectedValue = string.Empty;
                cbbBacSiThucHien.SelectedText = SELECTED_TEXT;
                ISINITIAL = true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (DATA_ROW_KQ_DICHVU != null)
            {
                string par = DATA_ROW_KQ_DICHVU["KETQUACLSID"].ToString() + "$" + string.Empty + "$" + cbbBacSiThucHien.SelectedValue;
                string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H038.EV002", par);
                if ("1".Equals(result))
                {
                    MessageBox.Show("Cập nhật bác sĩ thực hiện thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    LoadGridKQDVData(1);
                }
                else
                {
                    MessageBox.Show("Cập nhật bác sĩ thực hiện thất bại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn kết quả thực hiện", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
        }

        private void btnRefreshDV_Click(object sender, EventArgs e)
        {
            LoadGridDVData(1);
        }

        private void btnRefreshKQDV_Click(object sender, EventArgs e)
        {
            LoadGridKQDVData(1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbbBacSiThucHien.SelectedText = "--Lựa chọn--";
        }
    }
}