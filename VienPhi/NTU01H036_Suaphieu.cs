using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H036_Suaphieu : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string KHOAID;
        private static string MAUBENHPHAMID;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public void SetData(string khoaId, string mauBenhPhamId)
        {
            KHOAID = khoaId;
            MAUBENHPHAMID = mauBenhPhamId;
        }

        public NTU01H036_Suaphieu()
        {
            InitializeComponent();
        }

        private void NTU01H036_Suaphieu_Load(object sender, EventArgs e)
        {
            try
            {
                ucGridPhieu.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridPhieu.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridPhieu.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridPhieu.gridView.OptionsBehavior.Editable = false;
                ucGridPhieu.gridView.OptionsView.ShowAutoFilterRow = false;

                ucGridPhieu.setEvent(LoadGridPhieuThu);
                ucGridPhieu.gridView.Click += GridView_Click;
                ucGridPhieu.setNumberPerPage(new int[] { 200, 250, 300 });
                ucGridPhieu.onIndicator();

                InitNhomBHYT();
                InitLoaiGiuong();
                cbo_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitNhomBHYT()
        {
            try
            {
                DataTable BHYT_DT = RequestHTTP.get_ajaxExecuteQueryO("VPI01T004.05",
                    new string[] { },
                    new string[] { });

                if (BHYT_DT.Rows.Count <= 0)
                {
                    BHYT_DT.Columns.Add("");
                    BHYT_DT.Columns.Add("");
                }

                if (BHYT_DT.Rows.Count >= 0)
                {
                    DataRow dr = BHYT_DT.NewRow();
                    dr[0] = string.Empty;
                    dr[1] = "Chọn";
                    BHYT_DT.Rows.InsertAt(dr, 0);
                }

                cbbNhomBHYT.setData(BHYT_DT, 0, 1);
                cbbNhomBHYT.lookUpEdit.Properties.Columns[0].Visible = false;
                cbbNhomBHYT.SelectIndex = 0;
                cbbNhomBHYT.setEvent(cbo_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitLoaiGiuong()
        {
            try
            {
                DataTable loaiGiuongDT = RequestHTTP.get_ajaxExecuteQueryO("NTU01H014.EV001",
                    new string[] { },
                    new string[] { });

                if (loaiGiuongDT.Rows.Count <= 0)
                {
                    loaiGiuongDT.Columns.Add("");
                    loaiGiuongDT.Columns.Add("");
                }

                if (loaiGiuongDT.Rows.Count >= 0)
                {
                    DataRow dr = loaiGiuongDT.NewRow();
                    dr[0] = string.Empty;
                    dr[1] = "Chọn";
                    loaiGiuongDT.Rows.InsertAt(dr, 0);
                }

                cbbLoaiGiuong.setData(loaiGiuongDT, 0, 1);
                cbbLoaiGiuong.lookUpEdit.Properties.Columns[0].Visible = false;
                cbbLoaiGiuong.SelectIndex = 0;
                cbbLoaiGiuong.setEvent(cbo_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string loaiGiuong = (string.IsNullOrWhiteSpace(cbbLoaiGiuong.SelectValue)) ? "-1" : cbbLoaiGiuong.SelectValue;
                DataTable bacSiDT = RequestHTTP.get_ajaxExecuteQueryO("NTU01H032.EV007",
                    new string[] { "[0]", "[1]" },
                    new string[] { KHOAID, loaiGiuong });

                if (bacSiDT.Rows.Count <= 0)
                {
                    bacSiDT.Columns.Add("");
                    bacSiDT.Columns.Add("");
                }

                if (bacSiDT.Rows.Count >= 0)
                {
                    DataRow dr = bacSiDT.NewRow();
                    dr[0] = string.Empty;
                    dr[1] = "Chọn giường";
                    bacSiDT.Rows.InsertAt(dr, 0);
                }

                cbbMaGiuong.setData(bacSiDT, 0, 1);
                cbbMaGiuong.lookUpEdit.Properties.Columns[0].Visible = false;
                cbbMaGiuong.SelectIndex = 0;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void LoadGridPhieuThu(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadGridData(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        public void LoadGridData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                ResponsList responses = new ResponsList();

                DataTable phieuThuDT = new DataTable();

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NTU01H036.EV001", page, ucGridPhieu.ucPage1.getNumberPerPage(),
                    new string[] { "[0]" },
                    new string[] { MAUBENHPHAMID }, ucGridPhieu.jsonFilter());

                ucGridPhieu.clearData();

                phieuThuDT = MyJsonConvert.toDataTable(responses.rows);

                if (phieuThuDT.Rows.Count == 0)
                    phieuThuDT = Func.getTableEmpty(new String[] { "RN", "TENDICHVU", "TENDICHVUBHYT", "TIEN_BHYT", "TIEN_NHANDAN", "TIEN_DICHVU", "BHYT_DINHMUC", "TIEN_CHITRA", "TIEN_BHYT_TRA", "MAGIUONG", "LOAIGIUONGID" });

                ucGridPhieu.setData(phieuThuDT, responses.total, responses.page, responses.records);
                ucGridPhieu.setColumnAll(false);

                ucGridPhieu.setColumn("RN", 0, " ", 0);
                ucGridPhieu.setColumn("TENDICHVU", 1, "Tên DVBH", 0);
                ucGridPhieu.setColumn("TENDICHVUBHYT", 2, "Tên DVBH", 0);
                ucGridPhieu.setColumn("TIEN_BHYT", 3, "Giá BHYT", 0);
                ucGridPhieu.setColumn("TIEN_NHANDAN", 4, "Giá ND", 0);
                ucGridPhieu.setColumn("TIEN_DICHVU", 5, "Tiền DV", 0);
                ucGridPhieu.setColumn("BHYT_DINHMUC", 6, "BHYT định mức", 0);
                ucGridPhieu.setColumn("TIEN_CHITRA", 7, "Đơn giá TT", 0);
                ucGridPhieu.setColumn("TIEN_BHYT_TRA", 8, "Tiền BHYT trả", 0);
                ucGridPhieu.setColumn("MAGIUONG", 9, "Mã giường", 0);
                ucGridPhieu.setColumn("LOAIGIUONGID", 10, "Loại giường", 0);

                ucGridPhieu.gridView.BestFitColumns(true);
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
                var rowData = ucGridPhieu.gridView.GetFocusedDataRow();

                txtDonGiaBHYT.Text = rowData["TIEN_BHYT"].ToString();
                txtDonGiaVP.Text = rowData["TIEN_NHANDAN"].ToString();
                txtDonGiaDV.Text = rowData["TIEN_DICHVU"].ToString();
                txtBHYTTran.Text = rowData["BHYT_DINHMUC"].ToString();
                txtDonGiaBNTra.Text = rowData["TIEN_CHITRA"].ToString();
                txtTienBHYTTra.Text = rowData["TIEN_BHYT_TRA"].ToString();
                cbbLoaiGiuong.SelectValue = rowData["LOAIGIUONGID"].ToString();
                txtLoaiDT.Text = rowData["LOAIDOITUONG"].ToString();
                cbbNhomBHYT.SelectValue = rowData["NHOM_MABHYT_ID"].ToString();
                txtTenDV.Text = rowData["TENDICHVU"].ToString();
                txtTenDVBH.Text = rowData["TENDICHVU"].ToString();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                var rowData = ucGridPhieu.gridView.GetFocusedDataRow();

                if (rowData != null)
                {
                    var message = "Việc cập nhật thông tin này có thể ảnh hưởng đến thông tin hóa đơn \n";
                    message += "thanh toán, đẩy bảo hiểm và được ghi logs. Bạn có chắc chắn cập nhật";
                    DialogResult dialogResult = MessageBox.Show(message, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.None);

                    if (dialogResult == DialogResult.Yes)
                    {

                        var obj = new
                        {
                            BHYT_DINHMUC = txtBHYTTran.Text,
                            LOAIDOITUONG = txtLoaiDT.Text,
                            LOAIGIUONGID = cbbLoaiGiuong.SelectValue,
                            MAGIUONG = cbbMaGiuong.SelectValue,
                            NHOM_MABHYT_ID = cbbNhomBHYT.SelectValue,
                            TENDICHVU = txtTenDV.Text,
                            TENDICHVUBH = txtTenDVBH.Text,
                            TIEN_BHYT = txtDonGiaBHYT.Text,
                            TIEN_BHYT_TRA = txtTienBHYTTra.Text,
                            TIEN_CHITRA = txtDonGiaBNTra.Text,
                            TIEN_DICHVU = txtDonGiaDV.Text,
                            TIEN_NHANDAN = txtDonGiaVP.Text
                        };

                        string json = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
                        json += "$" + rowData["DICHVUKHAMBENHID"].ToString();

                        string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H036.EV002", json);

                        if ("1".Equals(result))
                        {
                            MessageBox.Show("Cập nhật dich vụ cho phiếu thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật dich vụ cho phiếu thất bại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }
    }
}