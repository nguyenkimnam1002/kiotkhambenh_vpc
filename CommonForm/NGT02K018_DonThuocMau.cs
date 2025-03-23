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
using DevExpress.XtraGrid.Views.Grid;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K018_DonThuocMau : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private string loaitiepnhanid;
        private string loainhom;

        private string khoaId;
        protected EventHandler ReturnData;
        private string doiTuong = "0";
        private string lookup_sql = string.Empty;

        public NGT02K018_DonThuocMau()
        {
            InitializeComponent();
        }
        public void setParam(string _loainhommaubenhpham_id, string _loaitiepnhanid)
        {
            this.loainhom = _loainhommaubenhpham_id;
            this.loaitiepnhanid = _loaitiepnhanid;
            this.khoaId = Const.local_khoaId.ToString();
        }

        private void NGT02K018_DonThuocMau_Load(object sender, EventArgs e)
        {
            try
            {
                ucGrid_DsMau.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsMau.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsMau.gridView.OptionsView.ShowAutoFilterRow = true;
                ucGrid_DsMau.gridView.FocusedRowChanged += Grid_DsMau_RowClick;
                ucGrid_DsMau.setEvent_DoubleClick(DoubleClick);
                ucGrid_DsMau.Set_HidePage(false);
                setGrid_DsMau();
                //ucGrid_DsMau.gridView.ColumnFilterChanged += ucGrid_Filter_Change;

                ucGrid_DsThuoc.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGrid_DsThuoc.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGrid_DsThuoc.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGrid_DsThuoc.Set_HidePage(false);
                setGrid_DsThuoc();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        //private void ucGrid_Filter_Change(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view.ActiveEditor is TextEdit)
        //        ((TextEdit)view.ActiveEditor).Text = ((TextEdit)view.ActiveEditor).Text.Trim();
        //}

        private void setGrid_DsMau()
        {
            try
            {
                ResponsList responses = new ResponsList();

                //string jsonFilter = string.Empty;
                //if (ucGrid_DsMau.ReLoadWhenFilter)
                //{
                //    if (ucGrid_DsMau.tableFlterColumn.Rows.Count > 0)
                //    {
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsMau.tableFlterColumn);
                //    }
                //}

                // load data
                doiTuong = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "DTMAU_KHOA_NTU");
                if ("0".Equals(loaitiepnhanid) && "1".Equals(doiTuong))
                {
                    lookup_sql = "NGT02K018.DSMAU.KHOA";
                }
                else
                {
                    lookup_sql = "NGT02K018.DSMAU";
                }

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(lookup_sql, 1, 10000,
                    new String[] { "[0]", "[1]" }, new string[] { khoaId, loainhom }, ucGrid_DsMau.jsonFilter());
                ucGrid_DsMau.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                dt.Columns.Add("ACT");
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MAUBENHPHAM_TEMPID", "TENMAUBENHPHAM_TEMP", "KHOID", "TENKHO" });

                ucGrid_DsMau.setData(dt, responses.total, responses.page, responses.records);
                ucGrid_DsMau.setColumnAll(false);
                ucGrid_DsMau.setColumn("RN", 0, " ", 0);
                ucGrid_DsMau.setColumn("TENMAUBENHPHAM_TEMP", 1, "Tên mẫu", 0);
                ucGrid_DsMau.setColumn("TENKHO", 2, "Tên kho", 0);
                ucGrid_DsMau.setColumn("ACT", 3, "Xóa", 0);
                ucGrid_DsMau.setColumnButtonImage("ACT", "delete.png", btnDelete_Click);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Grid_DsMau_RowClick(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (ucGrid_DsMau.gridView.FocusedRowHandle >= 0)
                {
                    setGrid_DsThuoc();
                }
                else
                {
                    ClearData(ucGrid_DsThuoc);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void setGrid_DsThuoc()
        {
            try
            {

                ResponsList ds = new ResponsList();
                //string jsonFilter = "";
                //if (ucGrid_DsThuoc.ReLoadWhenFilter)
                //{
                //    if (ucGrid_DsThuoc.tableFlterColumn.Rows.Count > 0)
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGrid_DsThuoc.tableFlterColumn);
                //}

                // Load dữ liệu
                DataRow dr = (DataRow)ucGrid_DsMau.gridView.GetDataRow(ucGrid_DsMau.gridView.FocusedRowHandle);

                if (dr != null)
                {
                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K018.CT", 1, 10000,
                        new String[] { "[0]" }, new string[] { dr["MAUBENHPHAM_TEMPID"].ToString() }, "");

                    //ds = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K018.CT", page, ucGrid_DsMau.ucPage1.getNumberPerPage(),
                    //new String[] { "[0]" }, new string[] { dr["MAUBENHPHAM_TEMPID"].ToString() });
                    ucGrid_DsThuoc.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "RN", "STT", "THUOCVATTUID", "TEN", "SOLUONG", "TEN_DVT", "DUONGDUNG", "HUONGDANSUDUNG" });
                    ucGrid_DsThuoc.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DsThuoc.setColumnAll(false);
                    ucGrid_DsThuoc.setColumn("THUOCVATTUID", -1, "id");
                    ucGrid_DsThuoc.setColumn("RN", 0, " ", 0);
                    ucGrid_DsThuoc.setColumn("TEN", 1, "Tên thuốc", 0);
                    ucGrid_DsThuoc.setColumn("SOLUONG", 2, "Số lượng", 0);
                    ucGrid_DsThuoc.setColumn("TEN_DVT", 3, "ĐVT", 0);
                    ucGrid_DsThuoc.setColumn("DUONGDUNG", 4, "Đường dùng", 0);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void ClearData(UserControl.ucGridview grid)
        {
            grid.gridControl.DataSource = null;
            grid.gridView.Columns.Clear();
        }

        private void DoubleClick(object sender, EventArgs e)
        {
            returnDataToParent();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            returnDataToParent();
        }

        private void returnDataToParent()
        {
            try
            {
                DataRow dataRow = ucGrid_DsMau.gridView.GetDataRow(ucGrid_DsMau.gridView.FocusedRowHandle);

                if (ReturnData != null && dataRow != null)
                {
                    var data = new DataTable();

                    data.Columns.Add("MAUBENHPHAM_TEMPID");
                    data.Columns.Add("KHOID");

                    var row = data.NewRow();

                    row["MAUBENHPHAM_TEMPID"] = dataRow["MAUBENHPHAM_TEMPID"];
                    row["KHOID"] = dataRow["KHOID"];
                    data.Rows.Add(row);

                    ReturnData(data, null);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = (DataRow)ucGrid_DsMau.gridView.GetDataRow(ucGrid_DsMau.gridView.FocusedRowHandle);
                if (dr != null && !string.IsNullOrEmpty(dr["MAUBENHPHAM_TEMPID"].ToString()))
                {
                    string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K018.DEL", dr["MAUBENHPHAM_TEMPID"].ToString());
                    if (ret == "1")
                        setGrid_DsMau();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
    }
}
