using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using Newtonsoft.Json;
using System.Globalization;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H037_SuaTheBhyt : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string HOSOBENHANID;
        private static DataRow DATA_ROW;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public void SetData(string hsbnId)
        {
            HOSOBENHANID = hsbnId;
        }

        public NTU01H037_SuaTheBhyt()
        {
            InitializeComponent();
        }

        private void NTU01H037_SuaTheBhyt_Load(object sender, EventArgs e)
        {
            try
            {
                ucGridDSTheBHYT.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridDSTheBHYT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridDSTheBHYT.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                ucGridDSTheBHYT.gridView.OptionsBehavior.Editable = false;

                ucGridDSTheBHYT.setEvent(LoadGridTheBHYT);
                ucGridDSTheBHYT.SetReLoadWhenFilter(true);
                ucGridDSTheBHYT.setEvent_FocusedRowChanged(GridView_Click);
                ucGridDSTheBHYT.setNumberPerPage(new int[] { 200, 250, 300 });

                // Tuyến
                DataTable tuyenDT = RequestHTTP.get_ajaxExecuteQuery("COM.LOAIBHYT", new string[] { }, new string[] { });
                cbbTuyen.setData(tuyenDT, 0, 1);
                cbbTuyen.SelectIndex = 0;
                cbbTuyen.setColumn(0, false);

                DataTable noiDKKBD = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NT.009", new string[] { });

                // Nơi DKKBD
                ucNoiDKKBD.setData(noiDKKBD, "BENHVIENKCBBD", "TENBENHVIEN");
                ucNoiDKKBD.setColumn("RN", -1, "", 0);
                ucNoiDKKBD.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 0);
                ucNoiDKKBD.setColumn("TENBENHVIEN", 1, "Bệnh viện", 0);
                ucNoiDKKBD.setColumn("DIACHI", 2, "Địa chỉ", 0);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                DATA_ROW = ucGridDSTheBHYT.gridView.GetFocusedDataRow();
                txtSoThe.Text = DATA_ROW["MA_BHYT"].ToString();
                if (!string.IsNullOrWhiteSpace(DATA_ROW["BHYT_BD"].ToString()))
                    dEditNgayBD.DateTime = DateTime.ParseExact(DATA_ROW["BHYT_BD"].ToString(), Const.FORMAT_date1, CultureInfo.InvariantCulture);
                else
                    dEditNgayBD.EditValue = DBNull.Value;

                if (!string.IsNullOrWhiteSpace(DATA_ROW["BHYT_KT"].ToString()))
                    dEditNgayKT.DateTime = DateTime.ParseExact(DATA_ROW["BHYT_KT"].ToString(), Const.FORMAT_date1, CultureInfo.InvariantCulture);
                else
                    dEditNgayKT.EditValue = DBNull.Value;

                txtDiaChiBH.Text = DATA_ROW["DIACHI_BHYT"].ToString();
                ucNoiDKKBD.SelectedValue = DATA_ROW["MA_KCBBD"].ToString();
                cbbTuyen.SelectValue = DATA_ROW["TUYENID"].ToString();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void LoadGridTheBHYT(object sender, EventArgs e)
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

        private void LoadGridData(int page)
        {
            try
            {
                if (page <= 0) page = 1;

                DATA_ROW = null;

                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NTU01H037.EV001", page, ucGridDSTheBHYT.ucPage1.getNumberPerPage(),
                    new string[] { "[0]" },
                    new string[] { HOSOBENHANID }, ucGridDSTheBHYT.jsonFilter());

                ucGridDSTheBHYT.clearData();

                DataTable bhytDT = MyJsonConvert.toDataTable(responses.rows);

                if (bhytDT.Rows.Count == 0)
                    bhytDT = Func.getTableEmpty(new String[] { "RN", "MA_BHYT", "MA_KCBBD", "BHYT_BD", "BHYT_KT", "DIACHI_BHYT" });

                ucGridDSTheBHYT.setData(bhytDT, responses.total, responses.page, responses.records);
                ucGridDSTheBHYT.setColumnAll(false);

                ucGridDSTheBHYT.setColumn("RN", 0, " ", 0);
                ucGridDSTheBHYT.setColumn("MA_BHYT", 1, "Mã BHYT", 0);
                ucGridDSTheBHYT.setColumn("MA_KCBBD", 2, "Nơi DKKCBBD", 0);
                ucGridDSTheBHYT.setColumn("BHYT_BD", 3, "Ngày BĐ", 0);
                ucGridDSTheBHYT.setColumn("BHYT_KT", 4, "Ngày KT", 0);
                ucGridDSTheBHYT.setColumn("DIACHI_BHYT", 5, "Địa chỉ BHYT", 0);

                ucGridDSTheBHYT.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnSuaThe_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW != null)
                {
                    var test = "Việc cập nhật thông tin này có thể ảnh hưởng đến thông tin hóa đơn \n";
                    test += "thanh toán, đẩy bảo hiểm và được ghi logs. Bạn có chắc chắn cập nhật";
                    DialogResult dialogResult = MessageBox.Show(test, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.None);

                    if (dialogResult == DialogResult.Yes)
                    {
                        var obj = new
                        {
                            DIACHIBHYT = txtDiaChiBH.Text,
                            DKKBBD = ucNoiDKKBD.SelectedText,
                            DKKBBDID = ucNoiDKKBD.SelectedValue,
                            MA_BHYT = txtSoThe.Text,
                            NGAYBD = dEditNgayBD.DateTime.ToString(Const.FORMAT_date1),
                            NGAYKT = dEditNgayKT.DateTime.ToString(Const.FORMAT_date1),
                            TKDKKBBD = ucNoiDKKBD.SelectedValue,
                            TUYENID = cbbTuyen.SelectValue
                        };

                        string json = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
                        string par = DATA_ROW["HOSOBENHANID"].ToString() + "$" + DATA_ROW["TIEPNHANID"].ToString() + "$" + DATA_ROW["HISTORYID"].ToString() + "$" + json;

                        string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H037.EV002", par);

                        if ("1".Equals(result))
                        {
                            MessageBox.Show("Cập nhật thẻ BHYT thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thẻ BHYT thất bại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn thẻ BHYT", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnXoaThe_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW != null)
                {
                    var message = "Việc xóa thẻ này có thể ảnh hưởng đến thông tin hóa đơn thanh toán, đẩy bảo hiểm và được ghi logs.";
                    message += "Sau khi xóa thẻ, bạn phải chuyển đối tượng lại các dịch vụ theo thẻ mới. ";
                    message += "Bạn có chắc chắn cập nhật";
                    DialogResult dialogResult = MessageBox.Show(message, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.None);

                    if (dialogResult == DialogResult.Yes)
                    {
                        string par = DATA_ROW["HOSOBENHANID"].ToString() + "$" + DATA_ROW["TIEPNHANID"].ToString() + "$" + DATA_ROW["HISTORYID"].ToString();

                        string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H037.EV003", par);

                        if ("1".Equals(result))
                        {
                            MessageBox.Show("Xóa thẻ BHYT thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                        else if ("2".Equals(result))
                        {
                            MessageBox.Show("Thẻ BHYT đang được sử dụng. Không được xóa", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                        else if ("0".Equals(result))
                        {
                            MessageBox.Show("Xóa thẻ BHYT lỗi", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn thẻ BHYT", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            LoadGridData(1);
        }
    }
}