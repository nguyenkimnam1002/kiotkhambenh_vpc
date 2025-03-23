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

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H039_ChinhSuaGiuong : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string BENHNHANID;
        private static string HOSOBENHANID;
        private static string TIEPNHANID;
        private static DataRow DATA_ROW;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public void SetData(string hsbnId, string tiepNhanId, string benhNhanId)
        {
            HOSOBENHANID = hsbnId;
            TIEPNHANID = tiepNhanId;
            BENHNHANID = benhNhanId;
        }

        public NTU01H039_ChinhSuaGiuong()
        {
            InitializeComponent();
        }

        private void NTU01H039_ChinhSuaGiuong_Load(object sender, EventArgs e)
        {
            try
            {
                ucGridGiuong.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridGiuong.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridGiuong.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                ucGridGiuong.gridView.OptionsBehavior.Editable = false;

                ucGridGiuong.setEvent(LoadGridGiuong);
                ucGridGiuong.SetReLoadWhenFilter(true);
                ucGridGiuong.setEvent_FocusedRowChanged(GridView_Click);
                ucGridGiuong.setNumberPerPage(new int[] { 200, 250, 300 });
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
                DATA_ROW = ucGridGiuong.gridView.GetFocusedDataRow();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void LoadGridGiuong(object sender, EventArgs e)
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
                    "NTU01H039.01", page, ucGridGiuong.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[1]", "[2]" },
                    new string[] { BENHNHANID, HOSOBENHANID, TIEPNHANID }, ucGridGiuong.jsonFilter());

                ucGridGiuong.clearData();

                DataTable bhytDT = MyJsonConvert.toDataTable(responses.rows);

                if (bhytDT.Rows.Count == 0)
                    bhytDT = Func.getTableEmpty(new String[] { "MABENHNHAN", "TENBENHNHAN", "TENDICHVU", "THOIGIAN_BD", "THOIGIAN_KT", "ORG_NAME", "SUDUNG" });

                ucGridGiuong.setData(bhytDT, responses.total, responses.page, responses.records);
                ucGridGiuong.setColumnAll(false);

                ucGridGiuong.setColumn("MABENHNHAN", 0, "Mã BN", 0);
                ucGridGiuong.setColumn("TENBENHNHAN", 1, "Tên BN", 0);
                ucGridGiuong.setColumn("TENDICHVU", 2, "Tên giường", 0);
                ucGridGiuong.setColumn("THOIGIAN_BD", 3, "TG bắt đầu", 0);
                ucGridGiuong.setColumn("THOIGIAN_KT", 4, "TG kết thúc", 0);
                ucGridGiuong.setColumn("ORG_NAME", 5, "Tên phòng", 0);
                ucGridGiuong.setColumn("SUDUNG", 5, "Sử dụng", 0);

                ucGridGiuong.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            LoadGridData(1);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (DATA_ROW != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa giường này không?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.None);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string result = RequestHTTP.call_ajaxCALL_SP_I("NTU01H039.02", DATA_ROW["BENHNHAN_GIUONGID"].ToString());
                        if ("1".Equals(result))
                        {
                            MessageBox.Show("Xóa giường thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            LoadGridData(1);
                        }
                        else
                        {
                            MessageBox.Show("Xóa giường lỗi", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chưa chọn giường", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    return;
                }
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

        protected EventHandler ReturnData;
        public void SetReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
    }
}