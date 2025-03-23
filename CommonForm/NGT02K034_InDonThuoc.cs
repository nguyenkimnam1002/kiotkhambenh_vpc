using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K034_InDonThuoc : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K034_InDonThuoc()
        {
            InitializeComponent();
        }

        private void NGT02K034_InDonThuoc_Load(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị Tiêu đề của grid
                ucGrid_DonThuoc.gridView.OptionsView.ShowViewCaption = false;
                // Hiển thị dòng filter
                ucGrid_DonThuoc.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DonThuoc.gridView.IndicatorWidth = 40;
                // Hiển thị checkbox
                ucGrid_DonThuoc.setMultiSelectMode(true);
                ucGrid_DonThuoc.setEvent(SetGridDonThuoc);
                ucGrid_DonThuoc.Set_HidePage(false);
                ucGrid_DonThuoc.onIndicator();
                ucGrid_DonThuoc.gridView.Click += GridView_Click;

                SetGridDonThuoc(null, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void SetGridDonThuoc(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Const.drvBenhNhan["KHAMBENHID"].ToString()))
                {
                    // Load dữ liệu
                    DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NGT02K034.INDT", Const.drvBenhNhan["KHAMBENHID"].ToString());

                    if (dt.Rows.Count <= 0)
                    {
                        dt = Func.getTableEmpty(new String[] { "SOPHIEU", "NGAYMAUBENHPHAM" });
                    }

                    ucGrid_DonThuoc.clearData_frmTiepNhan();
                    ucGrid_DonThuoc.setData(dt, dt.Rows.Count, 1);
                    ucGrid_DonThuoc.setColumnAll(false);
                    ucGrid_DonThuoc.setColumn("SOPHIEU", 1, "SỐ PHIẾU", 0);
                    ucGrid_DonThuoc.setColumn("NGAYMAUBENHPHAM", 2, "NGÀY CẤP", 0);
                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucGrid_DonThuoc.gridView.DataRowCount <= 0)
                {
                    MessageBox.Show("Bệnh nhân chưa được cấp thuốc.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int[] index = ucGrid_DonThuoc.gridView.GetSelectedRows();

                if (index.Length <= 0)
                {
                    MessageBox.Show("Hãy chọn phiếu muốn in.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                StringBuilder strBuilder = new StringBuilder();

                for (int i = 0; i < index.Length; i++)
                {
                    var dataRow = (DataRowView)ucGrid_DonThuoc.gridView.GetRow(index[i]);
                    strBuilder.Append(dataRow["MAUBENHPHAMID"].ToString());
                    if (index.Length - (i + 1) != 0)
                    {
                        strBuilder.Append(",");
                    }
                }

                string mauBenhPhamId = strBuilder.ToString();

                if (!string.IsNullOrWhiteSpace(mauBenhPhamId))
                {
                    //MessageBox.Show("Hãy chọn phiếu muốn in.");
                    //return;
                    InDonThuoc(mauBenhPhamId);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void InDonThuoc(string maubenhphamId)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("maubenhphamid", "String", maubenhphamId);

                VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint("In Đơn Thuốc", "NGT006_DONTHUOC_17DBV01_TT052016_A5", table, 650, 900);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                if (!"DX$CheckboxSelectorColumn".Equals(ucGrid_DonThuoc.gridView.FocusedColumn.FieldName))
                {
                    int index = ucGrid_DonThuoc.gridView.FocusedRowHandle;
                    if (ucGrid_DonThuoc.gridView.GetSelectedRows().Any(o => o == index))
                    {
                        ucGrid_DonThuoc.gridView.UnselectRow(index);
                    }
                    else
                    {
                        ucGrid_DonThuoc.gridView.SelectRow(index);
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