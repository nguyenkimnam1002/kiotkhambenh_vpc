using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Controls.Class;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.CommonForm
{
    public partial class NTU02D075_PhacDoMau : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private string maChanDoan;
        private string loaiDv;
        private string KhoId;

        public NTU02D075_PhacDoMau()
        {
            InitializeComponent();
        }

        public void initData(Dictionary<string, string> opt)
        {
            this.maChanDoan = opt["maChanDoan"];
            this.loaiDv = opt["loaiDv"];
            this.KhoId = opt.ContainsKey("khoid") ? opt["khoid"] : "";
        }

        private void NTU02D075_PhacDoMau_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
            txtTEXT_ICD.Text = maChanDoan;
            gridMau.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            gridMau.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            // Hiển thị dòng filter
            gridMau.gridView.OptionsView.ShowAutoFilterRow = true;
            gridMau.SetReLoadWhenFilter(true);
            gridMau.Set_HidePage(false);
            //gridMau.gridView.OptionsBehavior.Editable = true;
            //gridMau.gridView.OptionsBehavior.ReadOnly = false;
            gridMau.setEvent(setGridMau);
            gridMau.gridView.FocusedRowChanged += gridMau_RowClick;
            setGridMau(1, null);
            gridChiTiet.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            gridChiTiet.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            // Hiển thị dòng filter
            gridChiTiet.gridView.OptionsView.ShowAutoFilterRow = true;
            gridChiTiet.SetReLoadWhenFilter(true);
            gridChiTiet.Set_HidePage(false);
            gridChiTiet.setEvent(setGridChiTiet);
        }

        private void setGridMau(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    ResponsList ds = new ResponsList();
                    //string jsonFilter = "";
                    //if (gridMau.ReLoadWhenFilter)
                    //{
                    //    if (gridMau.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridMau.tableFlterColumn);
                    //}

                    // Load dữ liệu
                    ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D075.EV001", page, gridMau.ucPage1.getNumberPerPage(),
                        new String[] { "[0]" }, new string[] { txtTEXT_ICD.Text });
                    gridMau.clearData();

                    DataTable dt = new DataTable();
                    dt = MyJsonConvert.toDataTable(ds.rows);
                    dt.Columns.Add("ACT");
                    if (dt.Rows.Count == 0)
                        dt = Func.getTableEmpty(new String[] { "RN", "PHACDODTID", "MAPHACDODT", "TENPHACDODT" });
                    gridMau.setData(dt, ds.total, ds.page, ds.records);
                    gridMau.setColumnAll(false);
                    gridMau.setColumn("PHACDODTID", -1);
                    gridMau.setColumn("RN", 0, " ", 30);
                    gridMau.setColumn("MAPHACDODT", 1, "Mã PĐ", 100);
                    gridMau.setColumn("TENPHACDODT", 2, "Tên phác đồ", 200);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setGridChiTiet(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    ResponsList ds = new ResponsList();
                    //string jsonFilter = "";
                    //if (gridChiTiet.ReLoadWhenFilter)
                    //{
                    //    if (gridChiTiet.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(gridChiTiet.tableFlterColumn);
                    //}

                    // Load dữ liệu
                    string[] sqlPar = new string[] { }; 
                    DataRow dr = (DataRow)gridMau.gridView.GetDataRow(gridMau.gridView.FocusedRowHandle);
                    if (dr != null)
                    {
                        if (loaiDv == "0") // call từ form chỉ định dịch vụ
                        {
                            ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D075.EV002", page, gridMau.ucPage1.getNumberPerPage(),
                                new String[] { "[0]" }, new string[] { dr["PHACDODTID"].ToString() });
                            gridChiTiet.clearData();

                            DataTable dt = new DataTable();
                            dt = MyJsonConvert.toDataTable(ds.rows);
                            if (dt.Rows.Count == 0)
                                dt = Func.getTableEmpty(new String[] { "RN", "DICHVUID", "TEN", "SOLUONG", "TEN_DVT"});
                            gridChiTiet.setData(dt, ds.total, ds.page, ds.records);
                            gridChiTiet.setColumnAll(false);
                            gridChiTiet.setColumn("DICHVUID", -1, "id");
                            gridChiTiet.setColumn("RN", 0, " ", 30);
                            gridChiTiet.setColumn("TEN", 1, "Tên dịch vụ", 400);
                            gridChiTiet.setColumn("SOLUONG", 2, "Số lượng", 80);
                            gridChiTiet.setColumn("TEN_DVT", 3, "ĐVT", 80);
                        }
                        else // call từ form thuốc
                        {
                            ds = ServiceChiDinhDichVu.ajaxExecuteQueryPaging("NTU02D075.EV004", page, gridMau.ucPage1.getNumberPerPage(),
                                new String[] { "[0]", "[1]" }, new string[] { dr["PHACDODTID"].ToString(), KhoId });
                            gridChiTiet.clearData();

                            DataTable dt = new DataTable();
                            dt = MyJsonConvert.toDataTable(ds.rows);
                            if (dt.Rows.Count == 0)
                                dt = Func.getTableEmpty(new String[] { "RN", "THUOCVATTUID", "TEN", "TENHOATCHAT"});
                            gridChiTiet.setData(dt, ds.total, ds.page, ds.records);
                            gridChiTiet.setColumnAll(false);
                            gridChiTiet.setColumn("THUOCVATTUID", -1, "id");
                            gridChiTiet.setColumn("RN", 0, " ", 30);
                            gridChiTiet.setColumn("TEN", 1, "Tên thuốc", 400);
                            gridChiTiet.setColumn("TENHOATCHAT", 2, "Hoạt chất", 200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void gridMau_RowClick(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            setGridChiTiet(1, null);
        }

        private void btnSearchIcd_Click(object sender, EventArgs e)
        {
            setGridMau(1, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DataRow dr = (DataRow)gridMau.gridView.GetDataRow(gridMau.gridView.FocusedRowHandle);
            if (dr != null)
            {
                if ("0".Equals(loaiDv))
                {
                    Form frm = this.FindForm();
                    frm.Close();
                    pddt_presc_success(dr["PHACDODTID"].ToString(), null);
                }
                else
                {
                    if (pddt_presc_success != null)
                    {
                        var rowIds = gridChiTiet.gridView.DataRowCount;
                        var ret_arr = "";
                        for (int i = 0; i < rowIds; i++)
                        {
                            var ret_tmp = (DataRowView)gridChiTiet.gridView.GetRow(i);
                            if (i == 0)
                            {
                                ret_arr = ret_tmp["THUOCVATTUID"].ToString();
                            }
                            else
                            {
                                ret_arr = ret_arr + "," + ret_tmp["THUOCVATTUID"].ToString();
                            }
                        }

                        var obj = new
                        {
                            Id = ret_arr,
                            KhoId = KhoId,
                        };

                        pddt_presc_success(obj, null);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            frm.Close();
        }

        // Hàm gửi sự kiện đến form cha
        protected EventHandler pddt_presc_success;
        public void raiseEvent(EventHandler eventChangeValue)
        {
            pddt_presc_success = eventChangeValue;
        }

        private void NTU02D075_PhacDoMau_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }
    }
}