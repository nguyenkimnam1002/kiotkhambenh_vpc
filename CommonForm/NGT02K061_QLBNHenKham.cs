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
using Newtonsoft.Json;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K061_QLBNHenKham : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        private string tuNgayCbb;
        private string denNgayCbb;
        private string trangThaiCbb;
        private const string formatDate = "dd\\/MM\\/yyyy";

        public NGT02K061_QLBNHenKham()
        {
            InitializeComponent();
        }

        private void NGT02K061_QLBNHenKham_Load(object sender, EventArgs e)
        {
            try
            {
                dtTuNgay.DateTime = Func.getSysDatetime();
                dtDenNgay.DateTime = Func.getSysDatetime();
                initializeComboBox();

                ucGrid_DSBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DSBN.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                // Hiển thị dòng filter
                ucGrid_DSBN.gridView.OptionsView.ShowAutoFilterRow = true;
                ucGrid_DSBN.gridView.OptionsView.ColumnAutoWidth = false;
                ucGrid_DSBN.SetReLoadWhenFilter(true);
                ucGrid_DSBN.setEvent(setucGrid_DSBN);
                setucGrid_DSBN(1, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void initializeComboBox()
        {
            try
            {
                DataTable trangThai = new DataTable();

                DataColumn colName = new DataColumn("Name");
                DataColumn colValue = new DataColumn("value");
                trangThai.Columns.Add(colName);
                trangThai.Columns.Add(colValue);

                DataRow row = trangThai.NewRow();
                row[0] = "-1";
                row[1] = "--- Tất cả ---";
                trangThai.Rows.Add(row);

                row = trangThai.NewRow();
                row[0] = "0";
                row[1] = "Chưa tái khám";
                trangThai.Rows.Add(row);

                row = trangThai.NewRow();
                row[0] = "1";
                row[1] = "Đã tái khám";
                trangThai.Rows.Add(row);

                ucCbbTrangThai.setData(trangThai, 0, 1);
                ucCbbTrangThai.setColumnAll(false);
                ucCbbTrangThai.setColumn(1, true);
                ucCbbTrangThai.SelectIndex = 0;
                ucCbbTrangThai.setEvent(setValue_ucCbbTrangThai);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setValue_ucCbbTrangThai(object sender, EventArgs e)
        {
            try
            {
                loadDataGrid(1, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setucGrid_DSBN(object sender, EventArgs e)
        {
            try
            {
                loadDataGrid(sender, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void loadDataGrid(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;

                if (page > 0)
                {
                    ResponsList responses = new ResponsList();

                    //string jsonFilter = string.Empty;
                    //if (ucGrid_DSBN.ReLoadWhenFilter)
                    //{
                    //    if (ucGrid_DSBN.tableFlterColumn.Rows.Count > 0)
                    //    {
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DSBN.tableFlterColumn);
                    //    }
                    //}

                    tuNgayCbb = dtTuNgay.DateTime.ToString(formatDate);
                    denNgayCbb = dtDenNgay.DateTime.ToString(formatDate);
                    trangThaiCbb = ucCbbTrangThai.SelectValue;

                    if (dtTuNgay.DateTime > dtDenNgay.DateTime)
                    {
                        MessageBox.Show("Sai điều kiện tìm kiếm, từ ngày không thể lớn hơn đến ngày");
                        return;
                    }

                    //responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K038.DS.HENKHAM", page, ucGrid_DSBN.ucPage1.getNumberPerPage(),
                    //    new String[] { "[0]", "[1]", "[2]" }, new string[] { "01/08/2017", "30/01/2018", trangThaiCbb }, "");
                    responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K038.DS.HENKHAM", page, ucGrid_DSBN.ucPage1.getNumberPerPage(),
                        new String[] { "[0]", "[1]", "[2]" }, new string[] { tuNgayCbb, denNgayCbb, trangThaiCbb }, ucGrid_DSBN.jsonFilter());

                    ucGrid_DSBN.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(responses.rows);
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MABENHNHAN", "TENBENHNHAN", "GIOITINH", "NGAYSINH", "DIACHI", "THOIGIANLICHHEN", "LIENLACVOI_BN" });

                    ucGrid_DSBN.setData(dt, responses.total, responses.page, responses.records);
                    ucGrid_DSBN.setColumnAll(false);
                    ucGrid_DSBN.setColumn("RN", 0, " ", 0);
                    ucGrid_DSBN.setColumn("MABENHNHAN", 1, "Mã BN", 0);
                    ucGrid_DSBN.setColumn("TENBENHNHAN", 2, "Họ và tên", 0);
                    ucGrid_DSBN.setColumn("GIOITINH", 3, "Giới Tính", 0);
                    ucGrid_DSBN.setColumn("NGAYSINH", 4, "Ngày Sinh", 0);
                    ucGrid_DSBN.setColumn("DIACHI", 5, "Địa chỉ", 0);
                    ucGrid_DSBN.setColumn("THOIGIANLICHHEN", 6, "Ngày hẹn khám", 0);
                    ucGrid_DSBN.setColumn("LIENLACVOI_BN", 7, "TT Liên hệ BN", 0);
                    //ucGrid_DSBN.gridView.BestFitColumns(true);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadDataGrid(1, null);
        }
    }
}
