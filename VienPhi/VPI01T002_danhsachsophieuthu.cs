using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T002_danhsachsophieuthu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
          log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string NHOMPHIEUTHUID;
        private string PHONGID;
        private string TUNGAY;
        private string DENNGAY;
        private bool ISSELECTED = false;
        private DataRowView DATAROW;
        private DataRowView DATAROWPT;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T002_danhsachsophieuthu()
        {
            InitializeComponent();
        }

        public void SetData(string nhomPhieuThuId, string phongId, DataRowView dataRow, string tuNgay, string denNgay)
        {
            NHOMPHIEUTHUID = nhomPhieuThuId;
            PHONGID = phongId;
            DATAROWPT = dataRow;
            TUNGAY = tuNgay;
            DENNGAY = denNgay;
        }

        private void VPI01T002_danhsachsophieuthu_Load(object sender, EventArgs e)
        {
            ucGrid_DSPhieuThu.gridView.OptionsView.ColumnAutoWidth = false;
            ucGrid_DSPhieuThu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_DSPhieuThu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGrid_DSPhieuThu.gridView.OptionsBehavior.Editable = false;
            ucGrid_DSPhieuThu.setMultiSelectMode(false);

            ucGrid_DSPhieuThu.setEvent(LoadGridSoPhieuThu);
            ucGrid_DSPhieuThu.SetReLoadWhenFilter(true);
            ucGrid_DSPhieuThu.gridView.Click += GridView_Click;
            ucGrid_DSPhieuThu.setNumberPerPage(new int[] { 200, 300 });
        }

        private void LoadGridSoPhieuThu(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadGridData(pageNum);
        }

        public void LoadGridData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                ResponsList responses = new ResponsList();

                //string jsonFilter = string.Empty;
                //if (ucGrid_DSPhieuThu.ReLoadWhenFilter && ucGrid_DSPhieuThu.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DSPhieuThu.tableFlterColumn);
                //}

                DataTable phieuThuDT = new DataTable();

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "VPI01T002.02", page, ucGrid_DSPhieuThu.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[1]" },
                    new string[] { NHOMPHIEUTHUID, PHONGID }, ucGrid_DSPhieuThu.jsonFilter());

                ucGrid_DSPhieuThu.clearData();

                phieuThuDT = MyJsonConvert.toDataTable(responses.rows);

                if (phieuThuDT.Rows.Count == 0)
                    phieuThuDT = Func.getTableEmpty(new String[] { "RN", "MANHOMPHIEUTHU", "NGAYLAPPHIEU", "TONGSOPHIEUTHU", "SOPHIEUSUDUNG", "NGUOILAPPHIEU" });

                ucGrid_DSPhieuThu.setData(phieuThuDT, responses.total, responses.page, responses.records);
                ucGrid_DSPhieuThu.setColumnAll(false);

                ucGrid_DSPhieuThu.setColumn("RN", 0, " ", 0);
                ucGrid_DSPhieuThu.setColumn("MANHOMPHIEUTHU", 1, "Mã sổ", 0);
                ucGrid_DSPhieuThu.setColumn("NGAYLAPPHIEU", 2, "Ngày tạo", 0);
                ucGrid_DSPhieuThu.setColumn("TONGSOPHIEUTHU", 3, "Tổng số phiếu", 0);
                ucGrid_DSPhieuThu.setColumn("SOPHIEUSUDUNG", 4, "Đã sử dụng", 0);
                ucGrid_DSPhieuThu.setColumn("NGUOILAPPHIEU", 5, "Người sử dụng", 0);

                ucGrid_DSPhieuThu.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                DATAROW = null;
                int index = ucGrid_DSPhieuThu.gridView.FocusedRowHandle;
                DATAROW = (DataRowView)ucGrid_DSPhieuThu.gridView.GetRow(index);
                if (DATAROW == null) return;

                if (ucGrid_DSPhieuThu.gridView.GetSelectedRows().Any(o => o == index))
                {
                    ucGrid_DSPhieuThu.gridView.UnselectRow(index);
                    ISSELECTED = true;
                }
                else
                {
                    ucGrid_DSPhieuThu.gridView.SelectRow(index);
                    ISSELECTED = false;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (ISSELECTED)
            {
                KetChuyenSPT(DATAROW["MANHOMPHIEUTHU"].ToString(), DATAROW["NHOMPHIEUTHUID"].ToString());
            }
            else
            {
                MessageBox.Show("Chưa chọn sổ", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void KetChuyenSPT(string nhomptid_kcd, string ma_kcd)
        {
            try
            {
                int value;
                string result = RequestHTTP.getOneValue("VPI01T002.05", new string[] { "[0]" }, new string[] { nhomptid_kcd });

                if (int.TryParse(result, out value) && int.Parse(result) <= 0) return;

                result = RequestHTTP.getOneValue("VPI01T002.06", new string[] { "[0]" }, new string[] { ma_kcd });

                if (int.TryParse(result, out value) && int.Parse(result) <= 0)
                {
                    MessageBox.Show("Không hợp lệ", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ma_kca = DATAROWPT["MANHOMPHIEUTHU"].ToString();
                string nhomptid_kca = DATAROWPT["NHOMPHIEUTHUID"].ToString();

                string noidung_kcd = "Kết chuyển phiếu thu sổ " + ma_kca + " từ ngày " + TUNGAY + " đến ngày " + DENNGAY;
                string noidung_kca = "Kết chuyển từ ngày " + TUNGAY + " đến ngày " + DENNGAY + " vào sổ " + ma_kcd;

                var obj = new
                {
                    NHOMPTID_KCA = nhomptid_kca,
                    NHOMPTID_KCD = nhomptid_kcd,
                    NOIDUNG_KCA = noidung_kca,
                    NOIDUNG_KCD = noidung_kcd,
                    TUNGAY = TUNGAY,
                    DENNGAY = DENNGAY
                };

                string objData = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");

                string fl = RequestHTTP.call_ajaxCALL_SP_I("VPI01T002.10", objData);

                if ("1".Equals(fl))
                {
                    if (ReturnData != null)
                    {
                        this.Close();
                        ReturnData(true, null);
                    }
                }
                else if ("0".Equals(fl))
                {
                    MessageBox.Show("Không hợp lệ", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if ("-1".Equals(fl))
                {
                    MessageBox.Show("Cập nhật không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}