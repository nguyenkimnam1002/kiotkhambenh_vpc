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

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K061_QLHenKham_VienPhi : DevExpress.XtraEditors.XtraForm
    {
        #region Khai báo
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ResponsList ds = new ResponsList();
        DataTable dt = new DataTable();
        DataRow dr = null;
        string DOITUONGID = "";

        #endregion

        #region Init Form
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K061_QLHenKham_VienPhi()
        {
            InitializeComponent();
        }
        private void NGT02K061_QLHenKham_VienPhi_Load(object sender, EventArgs e)
        {
            Form_Init();
            getData_table(1, null);
        }
        public void setParam(string DOITUONGID)
        {
            this.DOITUONGID = DOITUONGID;
            Form_Init();
            getData_table(1, null);
        }
        #endregion

        #region Load dữ liệu
        private void Form_Init()
        {
            try
            {
                //datTuNgay.DateTime = DateTime.Today;
                //datDenNgay.DateTime = DateTime.Today;

                //Loại HK
                DataTable dtCB = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow drkt = dtCB.NewRow();
                drkt["col1"] = "1";
                drkt["col2"] = "1 - Ngoại trú";
                dtCB.Rows.Add(drkt);

                drkt = dtCB.NewRow();
                drkt["col1"] = "2";
                drkt["col2"] = "2 - Nội trú";
                dtCB.Rows.Add(drkt);

                drkt = dtCB.NewRow();
                drkt["col1"] = "3";
                drkt["col2"] = "3 - Qua website";
                dtCB.Rows.Add(drkt);

                drkt = dtCB.NewRow();
                drkt["col1"] = "4";
                drkt["col2"] = "4 - Qua tổng đài";
                dtCB.Rows.Add(drkt);

                drkt = dtCB.NewRow();
                drkt[0] = "-10";
                drkt[1] = "--- Tất cả ---";
                dtCB.Rows.InsertAt(drkt, 0);

                cbLoaiHK.setData(dtCB, 0, 1);
                cbLoaiHK.setColumnAll(false);
                cbLoaiHK.setColumn(1, true);
                cbLoaiHK.SelectIndex = 0;
                cbLoaiHK.setEvent(cbLoaiHK_Change);

                //YC khám
                DataTable dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow drYC = dt.NewRow();
                drYC[0] = "";
                drYC[1] = "Chọn yêu cầu khám";
                dt.Rows.InsertAt(drYC, 0);
                cbYCKham.setData(dt, 0, 1);
                cbYCKham.setColumnAll(false);
                cbYCKham.setColumn(1, true);
                cbYCKham.SelectIndex = 1;
                cbYCKham.setEvent(cbYCKham_Change);

                //YC phòng khám

                //grid
                ucGridDS.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
                ucGridDS.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridDS.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucGridDS.setEvent(getData_table);
                //ucGridDS.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;
                ucGridDS.setEvent_FocusedRowChanged(ucGridDS_Change_SelectRow);
                //ucGridDS.setEvent_DoubleClick(DoubleClick);
                //ucGridDS.setEvent_MenuPopupClick(MenuPopupClick);
                ucGridDS.SetReLoadWhenFilter(false);
                //ucGridDS.gridView.OptionsBehavior.ReadOnly = false;
                ucGridDS.gridView.OptionsBehavior.Editable = false;


                //ucGridDS.addMenuPopup(Menu_Popup());

                ucGridDS.gridView.OptionsCustomization.AllowSort = false;
                //ucGridDS.setMultiSelectMode(true);

                ucGridDS.onIndicator();

                cbLoaiHK.Focus();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void setYCKham(string DICHVUID)
        {
            try
            {
                cbYCKham.clearData();

                string _loadyeucaukhamtheodt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "LOAD_YEUCAUKHAM_THEO_DT");

                DataTable dt = new DataTable();
                dt = RequestHTTP.get_ajaxExecuteQuery("NGTDV.002", new string[] { "[0]" }, new string[] { (_loadyeucaukhamtheodt == "0" ? "0" : DOITUONGID) });
                if (dt == null || dt.Rows.Count == 0) dt = Func.getTableEmpty(new string[] { "col1", "col2" });
                DataRow drYC = dt.NewRow();
                drYC[0] = "";
                drYC[1] = "Chọn yêu cầu khám";
                dt.Rows.InsertAt(drYC, 0);
                cbYCKham.setData(dt, 0, 1);
                cbYCKham.setColumnAll(false);
                cbYCKham.setColumn(1, true);
                cbYCKham.SelectIndex = 1;
                if (DICHVUID != null)
                {
                    cbYCKham.SelectValue = DICHVUID;
                }
                cbYCKham.setEvent(cbYCKham_Change);

                //setPhongKham();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        private void setPhongKham()
        {
            try
            {
                cbPhongKham.clearData();

                string dichvuid = "";
                DataRowView drvTT = cbYCKham.SelectDataRowView;
                if (drvTT != null)
                {
                    dichvuid = drvTT["col1"].ToString();
                }

                DataTable dtPK = new DataTable();
                dtPK = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV", new string[] { "[0]" }, new string[] { dichvuid });
                if (dtPK == null || dtPK.Rows.Count == 0) dtPK = Func.getTableEmpty(new string[] { "col1", "col2" });
                cbPhongKham.setData(dtPK, 0, 1);
                cbPhongKham.setColumnAll(false);
                cbPhongKham.setColumn(1, true);
                cbPhongKham.SelectIndex = 0;

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
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
                    //if (ucGridDS.ReLoadWhenFilter)
                    //{
                    //    if (ucGridDS.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridDS.tableFlterColumn);
                    //}

                    ucGridDS.clearData();

                    string loai = "";
                    DataRowView drvTT = cbLoaiHK.SelectDataRowView;
                    if (drvTT != null)
                        loai = drvTT["col1"].ToString();

                    string tungay = "01/01/2017";
                    if (datTuNgay.DateTime != DateTime.MinValue)
                        tungay = datTuNgay.DateTime.ToString(Const.FORMAT_date1);

                    string denngay = "01/01/2050";
                    if (datDenNgay.DateTime != DateTime.MinValue)
                        denngay = datDenNgay.DateTime.ToString(Const.FORMAT_date1);

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K061.DSHK", page, ucGridDS.ucPage1.getNumberPerPage(), new string[] { "[0]", "[1]", "[2]" }, new string[] { loai, tungay, denngay }, ucGridDS.jsonFilter());

                    dt = MyJsonConvert.toDataTable(ds.rows);

                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "BENHNHANID", "DIACHI", "DICHVUID", "KHAMBENHID", "LICHHENID", "LOAIHENKHAM", "MABENHNHAN", "NGAYSINH", "NGAYTHUCHIEN", "RN", "TENBENHNHAN", "THOIGIANLICHHEN", "TRANGTHAI" });

                    ucGridDS.setData(dt, ds.total, ds.page, ds.records);
                    //ucGridDS.ucPage1.setData(dt.Rows.Count, ucGridDS.ucPage1.Current());
                    //loadDSBA(page);

                    ucGridDS.setColumnAll(false);
                    //ucGridDS.gridView.OptionsView.ColumnAutoWidth = false;
                    ucGridDS.gridView.BestFitColumns(true);

                    ucGridDS.setColumn("MABENHNHAN", 1, "Mã BN");
                    ucGridDS.gridView.Columns["MABENHNHAN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGridDS.setColumn("TENBENHNHAN", 2, "Tên bệnh nhân");
                    ucGridDS.setColumn("NGAYSINH", 3, "Ngày sinh");

                    ucGridDS.setColumn("THOIGIANLICHHEN", 4, "Giờ hẹn");
                    ucGridDS.gridView.Columns["THOIGIANLICHHEN"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    ucGridDS.setColumn("NGAYTHUCHIEN", 5, "Ngày khám");
                    ucGridDS.setColumn("TRANGTHAI", 6, "Đã đến");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
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

        #region Change, Keydown, Validate
        private void cbLoaiHK_Change(object sender, EventArgs e)
        {
            getData_table(1, null);
        }
        private void datTuNgay_EditValueChanged(object sender, EventArgs e)
        {
            getData_table(1, null);
        }
        private void datDenNgay_EditValueChanged(object sender, EventArgs e)
        {
            getData_table(1, null);
        }
        private void cbYCKham_Change(object sender, EventArgs e)
        {
            setPhongKham();
        }
        private void ucGridDS_Change_SelectRow(object sender, EventArgs e)
        {
            try
            {
                DataRow row = (ucGridDS.gridView.GetRow(ucGridDS.gridView.FocusedRowHandle) as DataRowView).Row;
                if (row == null) return;
                dr = row;
                string DICHVUID = "";
                if (dr["DICHVUID"].ToString() != "")
                {
                    DICHVUID = dr["DICHVUID"].ToString();
                }
                setYCKham(DICHVUID);

            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion

        #region Thêm, Xóa, Cập nhật
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVaoKham_Click(object sender, EventArgs e)
        {
            try
            {
                if (dr == null)
                {
                    MessageBox.Show("Yêu cầu chọn bệnh nhân hẹn khám.");
                    return;
                }

                string json = "";
                json += Func.json_item("LICHHENID", dr["LICHHENID"].ToString());
                json += Func.json_item("KHAMBENHID", dr["KHAMBENHID"].ToString());
                json += Func.json_item("BENHNHANID", dr["BENHNHANID"].ToString());
                json += Func.json_item("MABENHNHAN", dr["MABENHNHAN"].ToString());
                string TENBENHNHAN = dr["TENBENHNHAN"].ToString();
                json += Func.json_item("TENBENHNHAN", TENBENHNHAN);
                json += Func.json_item("NGAYSINH", dr["NGAYSINH"].ToString());
                json += Func.json_item("THOIGIANLICHHEN", dr["THOIGIANLICHHEN"].ToString());
                json += Func.json_item("NGAYTHUCHIEN", dr["NGAYTHUCHIEN"].ToString());
                json += Func.json_item("DIACHI", dr["DIACHI"].ToString());
                json += Func.json_item("TRANGTHAI", dr["TRANGTHAI"].ToString());

                DataRowView drvYC = cbYCKham.SelectDataRowView;
                if (drvYC != null)
                    json += Func.json_item("DICHVUID", drvYC["col1"].ToString());

                DataRowView drvPK = cbPhongKham.SelectDataRowView;
                if (drvPK != null)
                    json += Func.json_item("PHONGID", drvPK["col1"].ToString());

                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"").Replace("//", "");

                DataTable ret = RequestHTTP.call_ajaxCALL_SP_O("NGT02K061.LUU", json, 0);
                if (ret != null && ret.Rows.Count > 0)
                {
                    string RES = ret.Rows[0]["RES"].ToString().Trim();
                    if (RES == "-3")
                    {
                        XtraMessageBox.Show("Bệnh nhân đã quay lại khám", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (RES == "-2")
                    {
                        XtraMessageBox.Show("Không tồn tại thông tin bệnh nhân", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (RES == "1")
                    {
                        DialogResult result = XtraMessageBox.Show(this, "Cập nhật thông tin hẹn khám thành công", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            this.Close();
                            //getData_table(1, null);
                            XtraMessageBox.Show("Đã hẹn khám bệnh nhân " + TENBENHNHAN, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Lỗi cập nhật thông tin 1", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Lỗi cập nhật thông tin", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion
    }
}