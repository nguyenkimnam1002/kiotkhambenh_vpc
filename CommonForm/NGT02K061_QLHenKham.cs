using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;
using Newtonsoft.Json;
using System.Globalization;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;
using System.Threading;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K061_QLHenKham : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string tuNgayCbb;
        private string denNgayCbb;
        private string bacSiCbb;
        private string trangThaiCbb;
        private string benhnhanID;
        private string lichhenID;
        private const string caption = "Chọn";
        private const string valueNotNull = "Hãy nhập";
        private DataTable dt;
        private bool isBlock = false;

        public NGT02K061_QLHenKham()
        {
            InitializeComponent();
        }

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private void NGT02K061_QLHenKham_Load(object sender, EventArgs e)
        {
            try
            {
                InitForm();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitForm()
        {
            try
            {
                dEditTungay.DateTime = Func.getSysDatetime();
                dEditDenNgay.DateTime = Func.getSysDatetime();

                InitializeStatusComboBox();
                InitializeAppointmentComboBox();
                InitStatusButton();
                SetDataForCombobox();
                this.ReadOnly = false;
                CaptionValidate = true;
                InitGridView();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void SetDataForCombobox()
        {
            try
            {
                //cbo Gioi tinh
                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Gioitinh, "1");
                ucGioiTinh.setData(dt, 0, 1);
                ucGioiTinh.setEvent_Enter(ucGioitinh_KeyEnter);

                // cboNgheNghiep
                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Nghenghiep);
                ucNgheNghiep.setData(dt, 0, 1);
                ucNgheNghiep.Option_LockKeyTab = true;
                ucNgheNghiep.setEvent_Enter(ucNghenghiep_KeyEnter);

                // cboDanToc
                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Dantoc);
                ucDanToc.setData(dt, 0, 1);
                ucDanToc.setEvent_Enter(ucDantoc_KeyEnter);

                // cboQuocTich
                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Quoctich);
                ucQuocTich.setData(dt, 0, 1);
                ucQuocTich.Option_LockKeyTab = true;
                ucQuocTich.setEvent_Enter(ucQuoctich_KeyEnter);

                // CboTinh
                dt = RequestHTTP.Cache_getTinhTP(true);
                dt.Columns["col3"].SetOrdinal(0);
                dt.Columns["col2"].SetOrdinal(1);
                dt.Columns["col1"].SetOrdinal(2);
                ucTinh.setEvent(cboTinh_SelectedIndexChanged);
                dt = SetCaptionTinhHuyenXaCombobox(dt);
                ucTinh.setData(dt, "col3", "col2");
                ucTinh.setEvent_Enter(ucTinh_KeyEnter);
                ucTinh.setColumn("col3", 0, "STT", 0);
                ucTinh.setColumn("col2", 1, "Tỉnh/TP", 0);
                ucTinh.setColumn("col1", -1, "Mã tỉnh", 0);
                ucTinh.setColumn("col4", -1, "", 0);

                // cboHuyen
                DataTable huyenXaDT = SetCaptionHuyenXaCombobox();
                ucHuyen.setEvent(cboHuyen_SelectedIndexChanged);
                ucHuyen.setData(huyenXaDT, "col3", "col2");
                ucHuyen.setEvent_Enter(ucHuyen_KeyEnter);
                ucHuyen.Option_LockKeyTab = true;

                // cboXa
                ucXa.setEvent(cboXa_SelectedIndexChanged);
                ucXa.setData(huyenXaDT, "col3", "col2");
                ucXa.setEvent_Enter(ucXa_KeyEnter);

                // cboYeuCauKham
                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_YeuCauKham, "0");
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "";
                    dr[1] = "-- Chọn yêu cầu khám --";
                    dt.Rows.InsertAt(dr, 0);
                }
                ucYeuCauKham.setData(dt, 0, 1);
                ucYeuCauKham.setEvent(Change_YCKham);

                // cboBacSiKham
                dt = SetDataForDoctorCombobox("-- Tất cả --");
                ucBacSiSearch.setData(dt, 0, 1);
                dt = SetDataForDoctorCombobox("Chọn bác sĩ khám");
                ucBacSiKham.setData(dt, 0, 1);

                // Set default value
                ucGioiTinh.SelectValue = "2";
                ucNgheNghiep.SelectValue = "1";
                ucDanToc.SelectValue = "25";
                ucQuocTich.SelectValue = "0";
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void InitGridView()
        {
            try
            {
                ucGridBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucGridBN.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                                                                      // Hiển thị dòng filter
                ucGridBN.gridView.OptionsView.ShowAutoFilterRow = true;
                ucGridBN.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridBN.setMultiSelectMode(true);
                ucGridBN.SetReLoadWhenFilter(true);
                ucGridBN.setEvent(SetGridBenhNhan);
                ucGridBN.gridView.Click += GridView_Click;

                ucGridBN.setNumberPerPage(new int[] { 20, 100, 200, 300, 2000 });
                ucGridBN.onIndicator();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private bool ReadOnly
        {
            set
            {
                txtHoTen.Enabled = value;
                dEditNgaySinh.Enabled = value;
                txtNam.Enabled = value;
                txtTuoi.Enabled = value;
                cboTuoi.Enabled = value;
                ucGioiTinh.Enabled = value;
                ucNgheNghiep.Enabled = value;
                ucDanToc.Enabled = value;
                ucQuocTich.Enabled = value;
                txtSoNha.Enabled = value;
                ucTinh.Enabled = value;
                ucHuyen.Enabled = value;
                ucXa.Enabled = value;
                txtDiaChi.Enabled = value;
                txtDienThoai.Enabled = value;
                dEditNgayHen.Enabled = value;
                txtNoiLamViec.Enabled = value;
                ucLoaiHenKham.Enabled = value;

                ucYeuCauKham.Enabled = value;
                ucYeuCauKham.Enabled = value;
                ucPhongKham.Enabled = value;
                ucPhongKham.Enabled = value;
                ucBacSiKham.Enabled = value;
                ucBacSiKham.Enabled = value;
            }
        }

        private bool CaptionValidate
        {
            set {
                ucGioiTinh.CaptionValidate = value;
                ucDanToc.CaptionValidate = value;
                ucQuocTich.CaptionValidate = value;
                ucTinh.CaptionValidate = value;
                ucYeuCauKham.CaptionValidate = value;
                ucPhongKham.CaptionValidate = value;
            }
        }

        private void SetDefaultData()
        {
            txtHoTen.Text = string.Empty;
            dEditNgaySinh.Text = string.Empty;
            txtNam.Text = string.Empty;
            txtTuoi.Text = string.Empty;
            cboTuoi.SelectedIndex = -1;
            ucGioiTinh.SelectIndex = 1;
            ucNgheNghiep.SelectIndex = 0;
            ucDanToc.SelectIndex = 24;
            ucQuocTich.SelectIndex = 0;
            txtSoNha.Text = string.Empty;
            ucTinh.SelectIndex = 0;
            ucHuyen.SelectIndex = 0;
            ucXa.SelectIndex = 0;
            txtDiaChi.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            dEditNgayHen.Text = string.Empty;
            txtNoiLamViec.Text = string.Empty;
            ucYeuCauKham.SelectIndex = 0;
            ucPhongKham.clearData();
            ucLoaiHenKham.SelectIndex = 0;
            ucBacSiKham.SelectIndex = 0;
        }

        private void InitializeStatusComboBox()
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
                row[1] = "Đã đăng ký hẹn khám";
                trangThai.Rows.Add(row);

                row = trangThai.NewRow();
                row[0] = "2";
                row[1] = "Đã lên lịch hẹn";
                trangThai.Rows.Add(row);

                row = trangThai.NewRow();
                row[0] = "1";
                row[1] = "Đã đến khám";
                trangThai.Rows.Add(row);

                ucTrangThai.setData(trangThai, 0, 1);
                ucTrangThai.setColumnAll(false);
                ucTrangThai.setColumn(1, true);
                ucTrangThai.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void InitializeAppointmentComboBox()
        {
            try
            {
                DataTable loaiHenKham = new DataTable();
                DataColumn colName = new DataColumn("Name");
                DataColumn colValue = new DataColumn("value");
                loaiHenKham.Columns.Add(colName);
                loaiHenKham.Columns.Add(colValue);

                DataRow row = loaiHenKham.NewRow();
                row[0] = "4";
                row[1] = "Qua tổng đài";
                loaiHenKham.Rows.Add(row);

                row = loaiHenKham.NewRow();
                row[0] = "3";
                row[1] = "Qua website";
                loaiHenKham.Rows.Add(row);

                ucLoaiHenKham.setData(loaiHenKham, 0, 1);
                ucLoaiHenKham.setColumnAll(false);
                ucLoaiHenKham.setColumn(1, true);
                ucLoaiHenKham.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void InitStatusButton()
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnMultiEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private DataTable SetDataForDoctorCombobox(string caption)
        {
            DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_BacSyKham);
            DataRow dr = dt.NewRow();
            dr["col1"] = string.Empty;
            dr["col2"] = caption;
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        private void Change_YCKham(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV", new string[] { "[0]" }, new string[] { ucYeuCauKham.SelectValue });

                ucPhongKham.setData(dt, 0, 1);
                ucPhongKham.SelectIndex = 0;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void cboTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucHuyen.clearData();
                ucXa.clearData();
                SetDiaChiBN();
                DataRowView drv = ucTinh.SelectDataRowView;
                DataTable dataTable = SetCaptionHuyenXaCombobox();
                if (drv != null)
                {
                    if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                    {
                        string id_tinh = drv["col1"].ToString();
                        DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_tinh);
                        dt.Columns["col3"].SetOrdinal(0);
                        dt.Columns["col2"].SetOrdinal(1);
                        dt.Columns["col1"].SetOrdinal(2);

                        dt = SetCaptionTinhHuyenXaCombobox(dt);
                        ucHuyen.setData(dt, "col3", "col2");
                        ucHuyen.setColumn("col2", 1, "Huyện(Q)", 0);
                        ucHuyen.setColumn("col3", 0, "STT", 0);
                        ucHuyen.setColumn("col1", -1, "Mã huyện", 0);
                        ucHuyen.setColumn("col4", -1, "", 0);
                    }
                    else
                    {
                        ucHuyen.setData(dataTable, "col3", "col2");
                    }
                    ucXa.setData(dataTable, "col3", "col2");
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void cboHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ucXa.clearData();
                SetDiaChiBN();
                DataRowView drv = ucHuyen.SelectDataRowView;
                if (drv != null)
                {
                    if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                    {
                        string id_huyen = drv["col1"].ToString();
                        DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_huyen);
                        dt.Columns["col3"].SetOrdinal(0);
                        dt.Columns["col2"].SetOrdinal(1);
                        dt.Columns["col1"].SetOrdinal(2);

                        dt = SetCaptionTinhHuyenXaCombobox(dt);
                        ucXa.setData(dt, "col3", "col2");
                        ucXa.setColumn("col3", 0, "STT", 0);
                        ucXa.setColumn("col2", 1, "Xã(P)", 0);
                        ucXa.setColumn("col1", -1, "Mã xã", 0);
                        ucXa.setColumn("col4", -1, "", 0);
                    }
                    else
                    {
                        DataTable dataTable = SetCaptionHuyenXaCombobox();
                        ucXa.setData(dataTable, "col3", "col2");
                    }

                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void cboXa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SetDiaChiBN();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private DataTable SetCaptionHuyenXaCombobox()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("col3", typeof(string));
            dt.Columns.Add("col2", typeof(string));
            dt.Columns.Add("col1", typeof(string));
            dt.Columns.Add("col4", typeof(string));

            DataRow dr = dt.NewRow();
            dr["col3"] = string.Empty;
            dr["col2"] = "Chọn";
            dr["col1"] = string.Empty;
            dr["col4"] = string.Empty;
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        private DataTable SetCaptionTinhHuyenXaCombobox(DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["col3"] = string.Empty;
            dr["col2"] = "Chọn";
            dr["col1"] = string.Empty;
            dr["col4"] = string.Empty;
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        public void SetDiaChiBN()
        {
            try
            {
                string diaChiBN = string.Empty;

                if (!string.IsNullOrWhiteSpace(txtSoNha.Text))
                {
                    diaChiBN = txtSoNha.Text;
                }

                if (ucXa.SelectIndex >= 0 && !string.IsNullOrWhiteSpace(ucXa.SelectDataRowView.Row["col1"].ToString())
                    && ucHuyen.SelectIndex >= 0 && !string.IsNullOrWhiteSpace(ucHuyen.SelectDataRowView.Row["col1"].ToString())
                    && ucTinh.SelectIndex >= 0 && !string.IsNullOrWhiteSpace(ucTinh.SelectDataRowView.Row["col1"].ToString()))
                {
                    diaChiBN += "-" + ucXa.SelectText;
                    diaChiBN += "-" + ucHuyen.SelectText;
                    diaChiBN += "-" + ucTinh.SelectText;
                }
                else
                {
                    diaChiBN = string.Empty;
                }

                if (diaChiBN.StartsWith("-")) diaChiBN = diaChiBN.Substring(1);
                txtDiaChi.Text = diaChiBN;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        #region Sk phần Năm sinh-Tuổi
        private void Nhap_NgaySinh()
        {
            try
            {
                DateTime dtimeNow = Func.getSysDatetime_Short();
                if (dEditNgaySinh.DateTime > dtimeNow)
                {
                    MessageBox.Show(Const.mess_erro_ngaysinh);
                    dEditNgaySinh.Focus();
                    return;
                }
                txtNam.TextChanged -= new EventHandler(txtNamsinh_TextChanged);
                txtNam.Text = dEditNgaySinh.DateTime.Year.ToString();
                txtNam.TextChanged += new EventHandler(txtNamsinh_TextChanged);

                DateTime dtNhap = dEditNgaySinh.DateTime.AddDays(0);
                int chenhNam = dtimeNow.Year - dtNhap.Year;

                DateTime dt2 = dtNhap.AddYears(chenhNam);
                int chenhThang = chenhNam * 12 + dtimeNow.Month - dtNhap.Month;

                if (chenhThang >= 36)// hiển thị số tuổi
                {
                    txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                    txtTuoi.Text = chenhNam.ToString();
                    txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                    cboTuoi.SelectedIndex = 0;
                }
                else
                {
                    if (chenhThang >= 1)// hiển thị số tháng
                    {
                        txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                        txtTuoi.Text = chenhThang.ToString();
                        txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                        cboTuoi.SelectedIndex = 1;
                    }
                    else
                    {
                        TimeSpan ts = TimeSpan.FromTicks(dtimeNow.Ticks - dtNhap.Ticks);
                        double chenhNgay = ts.TotalDays + 1;
                        txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                        txtTuoi.Text = (chenhNgay).ToString();
                        txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                        cboTuoi.SelectedIndex = 2;
                    }
                }

                cboTuoi.Enabled = false;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void txtNamsinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtimeNow = Func.getSysDatetime_Short();
                int NhapNam = Convert.ToInt32(txtNam.Text.Trim());
                if (NhapNam > dtimeNow.Year)
                {
                    MessageBox.Show(Const.mess_erro_namsinh);
                    txtNam.Focus();
                    return;
                }

                txtTuoi.Text = (dtimeNow.Year - NhapNam).ToString();
                cboTuoi.SelectedIndex = 0;

                dEditNgaySinh.Text = string.Empty;
                cboTuoi.Enabled = false;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                dEditNgaySinh.Text = string.Empty;
                txtNam.Text = string.Empty;
                txtTuoi.Text = string.Empty;
                cboTuoi.SelectedIndex = 0;
            }
        }

        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtimeNow = Func.getSysDatetime_Short();
                int NhapTuoi = Convert.ToInt32(txtTuoi.Text.Trim());

                txtNam.Text = (dtimeNow.Year - NhapTuoi).ToString();
                cboTuoi.SelectedIndex = 0;
                cboTuoi.Enabled = false;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        #endregion

        private void SetGridBenhNhan(object sender, EventArgs e)
        {
            try
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadDataGrid(pageNum);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                int index = ucGridBN.gridView.FocusedRowHandle;
                if (!isBlock)
                {
                    SetValueThongTinBenhNhan(index);
                }

                if (!"DX$CheckboxSelectorColumn".Equals(ucGridBN.gridView.FocusedColumn.FieldName))
                {
                    if (ucGridBN.gridView.GetSelectedRows().Any(o => o == index))
                    {
                        ucGridBN.gridView.UnselectRow(index);
                    }
                    else
                    {
                        ucGridBN.gridView.SelectRow(index);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void SetValueThongTinBenhNhan(int index)
        {
            try
            {
                var dataRow = (DataRowView)ucGridBN.gridView.GetRow(index);
                DataTable diaChiDT = RequestHTTP.call_ajaxCALL_SP_O("COM.DIACHI", dataRow["DIAPHUONGID"].ToString(), 0);
                DataRowView drv = null;
                txtHoTen.Text = dataRow["TENBENHNHAN"].ToString();

                if (!string.IsNullOrWhiteSpace(dataRow["NGAYSINH"].ToString()))
                {
                    dEditNgaySinh.DateTime = DateTime.ParseExact(dataRow["NGAYSINH"].ToString(), Const.FORMAT_date1, CultureInfo.InvariantCulture);
                }

                txtNam.Text = dataRow["NAMSINH"].ToString();
                ucGioiTinh.SelectValue = dataRow["GIOITINHID"].ToString();
                ucNgheNghiep.SelectValue = dataRow["NGHENGHIEPID"].ToString();
                ucDanToc.SelectValue = dataRow["DANTOCID"].ToString();
                ucQuocTich.SelectValue = dataRow["QUOCGIAID"].ToString();
                txtSoNha.Text = dataRow["SONHA"].ToString();

                var dataTable = RequestHTTP.Cache_getTinhTP(true);
                var dataRows = dataTable.Select(string.Format("{0} = '{1}'", "col1", diaChiDT.Rows[0]["ID_TINH"].ToString()));
                ucTinh.SelectValue = dataRows[0]["col3"].ToString();

                string valueHuyen = string.Empty;
                if (Convert.ToInt64(diaChiDT.Rows[0]["ID_HUYEN"].ToString()) > 0)
                {
                    drv = ucTinh.SelectDataRowView;
                    if (drv != null)
                    {
                        if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                        {
                            string id_tinh = drv["col1"].ToString();
                            dataTable = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_tinh);
                            dataRows = dataTable.Select(string.Format("{0} = '{1}'", "col1", diaChiDT.Rows[0]["ID_HUYEN"].ToString()));
                            valueHuyen = dataRows[0]["col3"].ToString();
                        }
                    }
                }

                ucHuyen.SelectValue = valueHuyen;

                string valueXa = string.Empty;
                if (Convert.ToInt64(diaChiDT.Rows[0]["ID_XA"].ToString()) > 0)
                {
                    drv = ucHuyen.SelectDataRowView;
                    if (drv != null)
                    {
                        if (!string.IsNullOrWhiteSpace(drv["col1"].ToString()))
                        {
                            string id_huyen = drv["col1"].ToString();
                            dataTable = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_huyen);
                            dataRows = dataTable.Select(string.Format("{0} = '{1}'", "col1", diaChiDT.Rows[0]["ID_XA"].ToString()));
                            valueXa = dataRows[0]["col3"].ToString();
                        }
                    }
                }
                ucXa.SelectValue = valueXa;

                dEditNgayHen.DateTime = DateTime.ParseExact(dataRow["NGAYHEN"].ToString(), Const.FORMAT_datetime1, CultureInfo.InvariantCulture);
                txtDiaChi.Text = dataRow["DIACHI"].ToString();
                txtDienThoai.Text = dataRow["SDTBENHNHAN"].ToString();
                txtNoiLamViec.Text = dataRow["NOILAMVIEC"].ToString();
                ucYeuCauKham.SelectValue = dataRow["DICHVUID"].ToString();
                ucPhongKham.SelectValue = dataRow["PHONGKHAMID"].ToString();
                ucBacSiKham.SelectValue = dataRow["USER_ID"].ToString();
                ucLoaiHenKham.SelectValue = dataRow["LOAIHENKHAM"].ToString();
                benhnhanID = dataRow["BENHNHANID"].ToString();
                lichhenID = dataRow["LICHHENID"].ToString();

                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnMultiEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void LoadDataGrid(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                ResponsList responses = new ResponsList();

                //string jsonFilter = string.Empty;
                //if (ucGridBN.ReLoadWhenFilter)
                //{
                //    if (ucGridBN.tableFlterColumn.Rows.Count > 0)
                //    {
                //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridBN.tableFlterColumn);
                //    }
                //}

                tuNgayCbb = dEditTungay.DateTime.ToString(Const.FORMAT_date1);
                denNgayCbb = dEditDenNgay.DateTime.ToString(Const.FORMAT_date1);
                bacSiCbb = ucBacSiSearch.SelectValue;
                trangThaiCbb = ucTrangThai.SelectValue;

                if (dEditTungay.DateTime > dEditDenNgay.DateTime)
                {
                    MessageBox.Show("Sai điều kiện tìm kiếm, từ ngày không thể lớn hơn đến ngày");
                    return;
                }

                responses = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K061.DSBNHK", page, ucGridBN.ucPage1.getNumberPerPage(),
                    new String[] { "[0]", "[1]", "[2]", "[3]" }, new string[] { tuNgayCbb, denNgayCbb, bacSiCbb, trangThaiCbb }, ucGridBN.jsonFilter());

                ucGridBN.clearData();

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "MABENHNHAN", "TENBENHNHAN", "GIOITINH", "NGAYHEN", "TENDICHVU", "TENLOAIHENKHAM", "DIACHI" });

                ucGridBN.setData(dt, responses.total, responses.page, responses.records);
                ucGridBN.setColumnAll(false);
                ucGridBN.setColumn("MABENHNHAN", 1, "Mã BN", 0);
                ucGridBN.setColumn("TENBENHNHAN", 2, "Họ tên", 0);
                ucGridBN.setColumn("GIOITINH", 3, "Giới Tính", 0);
                ucGridBN.setColumn("NGAYHEN", 4, "Ngày hẹn khám", 0);
                ucGridBN.setColumn("TENDICHVU", 5, "Yêu cầu khám", 0);
                ucGridBN.setColumn("TENLOAIHENKHAM", 6, "Loại hẹn khám", 0);
                ucGridBN.setColumn("DIACHI", 7, "Địa chỉ", 0);
                ucGridBN.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                DisplayTextEditError("họ tên", txtHoTen);
                return false;
            }

            if (string.IsNullOrWhiteSpace(dEditNgaySinh.Text) && cboTuoi.SelectedIndex != 0)
            {
                DisplayDateEditError("ngày sinh", dEditNgaySinh);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(dEditNgaySinh.Text) && Convert.ToInt64(dEditNgaySinh.DateTime.ToString("yyyyMMdd")) < 19000101)
            {
                MessageBox.Show("Ngày sinh không được nhỏ hơn ngày 01/01/1900", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dEditNgaySinh.Focus();
                return false;
            }

            var regex = new Regex(@"/^(0?[1-9]|[12][0-9]|3[01])[\/](0?[1-9]|1[012])[\/]\d{4}$/");
            var matches = regex.Matches(dEditNgaySinh.Text);
            DateTime dateTime;

            if (!string.IsNullOrWhiteSpace(dEditNgaySinh.Text) && (matches.Count > 0 || !DateTime.TryParse(dEditNgaySinh.DateTime.ToString(Const.FORMAT_date1), out dateTime)))
            {
                MessageBox.Show("Ngày sinh không đúng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dEditNgaySinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNam.Text))
            {
                DisplayTextEditError("năm sinh", txtNam);
                return false;
            }

            int integer;
            if (!int.TryParse(txtNam.Text, out integer))
            {
                MessageBox.Show("Năm sinh là số nguyên dương/ năm sinh không nhỏ hơn năm 1900", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNam.Focus();
                return false;
            }

            if (Convert.ToInt64(txtNam.Text) < 1900)
            {
                MessageBox.Show("Năm sinh là số nguyên dương/ năm sinh không nhỏ hơn năm 1900", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNam.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTuoi.Text))
            {
                DisplayTextEditError("tuổi", txtTuoi);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtTuoi.Text) && !int.TryParse(txtTuoi.Text, out integer))
            {
                MessageBox.Show("Tuổi là số nguyên dương", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTuoi.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ucGioiTinh.SelectValue))
            {
                MessageBox.Show(valueNotNull + " " + "giới tính", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ucGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ucDanToc.SelectValue))
            {
                MessageBox.Show(valueNotNull + " " + "dân tộc", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ucDanToc.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ucQuocTich.SelectValue))
            {
                MessageBox.Show(valueNotNull + " " + "quốc tịch", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ucQuocTich.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ucTinh.SelectValue))
            {
                MessageBox.Show(valueNotNull + " " + "tỉnh", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ucTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                DisplayTextEditError("địa chỉ", txtDiaChi);
                return false;
            }

            if (string.IsNullOrWhiteSpace(dEditNgayHen.Text))
            {
                DisplayDateEditError("ngày hẹn khám", dEditNgayHen);
                return false;
            }

            if (!DateTime.TryParseExact(dEditNgayHen.Text, Const.FORMAT_datetime1, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                MessageBox.Show("Ngày hẹn khám không đúng", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dEditNgayHen.Focus();
                return false;
            }

            if (dEditNgayHen.DateTime < Func.getSysDatetime())
            {
                MessageBox.Show("Thời gian hẹn khám không được nhỏ hơn thời gian hiện tại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dEditNgayHen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ucYeuCauKham.SelectValue))
            {
                MessageBox.Show(valueNotNull + " " + "yêu cầu khám", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ucYeuCauKham.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(ucPhongKham.SelectValue))
            {
                MessageBox.Show(valueNotNull + " " + "phòng khám", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ucPhongKham.Focus();
                return false;
            }

            return true;
        }

        private void DisplayTextEditError(string content, TextEdit text)
        {
            MessageBox.Show(valueNotNull + " " + content, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            text.Focus();
        }

        private void DisplayDateEditError(string content, DateEdit date)
        {
            MessageBox.Show(valueNotNull + " " + content, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            date.Focus();
        }

        #region Button Event
        private void bbtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataGrid(1);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataGrid(1);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                benhnhanID = string.Empty;
                lichhenID = string.Empty;

                this.ReadOnly = true;
                cboTuoi.Enabled = true;
                SetDefaultData();
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnMultiEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                isBlock = true;
                txtHoTen.Focus();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                this.ReadOnly = true;
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnMultiEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
                isBlock = true;
                txtHoTen.Focus();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnMultiEdit_Click(object sender, EventArgs e)
        {
            try
            {
                NGT02K063_QLHenKham_Sua frm = new NGT02K063_QLHenKham_Sua();
                int[] index = ucGridBN.gridView.GetSelectedRows();
                var benhNhanList = new List<object>();

                for (int i = 0; i < index.Length; i++)
                {
                    var dataRow = (DataRowView)ucGridBN.gridView.GetRow(index[i]);

                    var obj = new
                    {
                        MABENHNHAN = dataRow["MABENHNHAN"].ToString(),
                        TENBENHNHAN = dataRow["TENBENHNHAN"].ToString(),
                        GIOITINH = dataRow["GIOITINH"].ToString(),
                        NGAYHEN = dataRow["NGAYHEN"].ToString(),
                        TENDICHVU = dataRow["TENDICHVU"].ToString(),
                        TENLOAIHENKHAM = dataRow["TENLOAIHENKHAM"].ToString(),
                        DIACHI = dataRow["DIACHI"].ToString(),
                        BENHNHANID = dataRow["BENHNHANID"].ToString(),
                        NGAYSINH = dataRow["NGAYSINH"].ToString(),
                        NAMSINH = dataRow["NAMSINH"].ToString(),
                        NGHENGHIEPID = dataRow["NGHENGHIEPID"].ToString(),
                        DANTOCID = dataRow["DANTOCID"].ToString(),
                        USER_ID = dataRow["USER_ID"].ToString(),
                        QUOCGIAID = dataRow["QUOCGIAID"].ToString(),
                        SONHA = dataRow["SONHA"].ToString(),
                        TRANGTHAI = dataRow["TRANGTHAI"].ToString(),
                        DIAPHUONGID = dataRow["DIAPHUONGID"].ToString(),
                        DICHVUID = dataRow["DICHVUID"].ToString(),
                        PHONGKHAMID = dataRow["PHONGKHAMID"].ToString(),
                        GIOITINHID = dataRow["GIOITINHID"].ToString(),
                        NOILAMVIEC = dataRow["NOILAMVIEC"].ToString(),
                        SDTBENHNHAN = dataRow["SDTBENHNHAN"].ToString(),
                        LOAIHENKHAM = dataRow["LOAIHENKHAM"].ToString(),
                        LICHHENID = dataRow["LICHHENID"].ToString(),
                    };

                    benhNhanList.Add(obj);
                }

                string objData = JsonConvert.SerializeObject(benhNhanList).Replace("\"", "\\\"");

                frm.loadData(objData);
                frm.SetReturnData(ReturnQLHENKHAMSuaData);
                //frm.FormClosing += childFormClosing;
                frm.StartPosition = FormStartPosition.CenterScreen;
                //Enabled = false;
                frm.Show();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void ReturnQLHENKHAMSuaData(object sender, EventArgs e)
        {
            try
            {
                bool flag = (bool)sender;

                if (flag)
                {
                    MessageBox.Show("Cập nhật thành công !", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    LoadDataGrid(1);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        //private void childFormClosing(object sender, EventArgs e)
        //{
        //    Enabled = true;
        //}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa các bản ghi được chọn ?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                int[] index = ucGridBN.gridView.GetSelectedRows();
                var benhNhanList = new List<object>();

                for (int i = 0; i < index.Length; i++)
                {
                    var dataRow = (DataRowView)ucGridBN.gridView.GetRow(index[i]);

                    var obj = new
                    {
                        BENHNHANID = dataRow["BENHNHANID"].ToString(),
                        DANTOCID = dataRow["DANTOCID"].ToString(),
                        DIABANID = dataRow["DIABANID"].ToString(),
                        DIACHI = dataRow["DIACHI"].ToString(),
                        DIAPHUONGID = dataRow["DIAPHUONGID"].ToString(),
                        DICHVUID = dataRow["DICHVUID"].ToString(),
                        GIOITINH = dataRow["GIOITINH"].ToString(),
                        GIOITINHID = dataRow["GIOITINHID"].ToString(),
                        LICHHENID = dataRow["LICHHENID"].ToString(),
                        LOAIHENKHAM = dataRow["LOAIHENKHAM"].ToString(),
                        MABENHNHAN = dataRow["MABENHNHAN"].ToString(),
                        NAMSINH = dataRow["NAMSINH"].ToString(),
                        NGAYHEN = dataRow["NGAYHEN"].ToString(),
                        NGAYSINH = dataRow["NGAYSINH"].ToString(),
                        NGHENGHIEPID = dataRow["NGHENGHIEPID"].ToString(),
                        NOILAMVIEC = dataRow["NOILAMVIEC"].ToString(),
                        PHONGKHAMID = dataRow["PHONGKHAMID"].ToString(),
                        QUOCGIAID = dataRow["QUOCGIAID"].ToString(),
                        SDTBENHNHAN = dataRow["SDTBENHNHAN"].ToString(),
                        SONHA = dataRow["SONHA"].ToString(),
                        TENBENHNHAN = dataRow["TENBENHNHAN"].ToString(),
                        TENDICHVU = dataRow["TENDICHVU"].ToString(),
                        TENLOAIHENKHAM = dataRow["TENLOAIHENKHAM"].ToString(),
                        TRANGTHAI = dataRow["TRANGTHAI"].ToString(),
                        USER_ID = dataRow["USER_ID"].ToString()
                    };

                    benhNhanList.Add(obj);
                }

                string objData = JsonConvert.SerializeObject(benhNhanList).Replace("\"", "\\\"");
                string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K061.XOAHK", objData);
                if ("1".Equals(ret))
                {
                    MessageBox.Show("Xóa thành công !", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    InitStatusButton();
                    SetDefaultData();
                }
                else
                    MessageBox.Show("Xảy ra lỗi !", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGrid(1);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateData())
                {
                    string BNDIAPHUONGID = string.Empty;
                    if (ucXa.SelectIndex >= 0 && !caption.Equals(ucXa.SelectText))
                    {
                        DataRow row = ucXa.SelectDataRowView.Row;
                        BNDIAPHUONGID = row["col1"].ToString();
                    }
                    else if (ucHuyen.SelectIndex >= 0 && !caption.Equals(ucHuyen.SelectText))
                    {
                        DataRow row = ucHuyen.SelectDataRowView.Row;
                        BNDIAPHUONGID = row["col1"].ToString();
                    }
                    else if (ucTinh.SelectIndex >= 0 && !caption.Equals(ucTinh.SelectText))
                    {
                        DataRow row = ucTinh.SelectDataRowView.Row;
                        BNDIAPHUONGID = row["col1"].ToString();
                    }

                    var obj = new
                    {
                        BENHNHANID = benhnhanID,
                        BNDIAPHUONGID = BNDIAPHUONGID,
                        DANTOCID = ucDanToc.SelectValue,
                        DIABANID = string.Empty,
                        DIACHI = txtDiaChi.Text.Trim(),
                        DICHVUID = ucYeuCauKham.SelectValue,
                        DVTUOI = cboTuoi.SelectedIndex + 1,
                        GIOITINHID = ucGioiTinh.SelectValue,
                        HC_HUYENID = ucHuyen.SelectDataRowView.Row["col1"].ToString(),
                        HC_TINHID = ucTinh.SelectDataRowView.Row["col1"].ToString(),
                        HC_XAID = ucXa.SelectDataRowView.Row["col1"].ToString(),
                        LICHHENID = lichhenID,
                        LOAIHENKHAM = ucLoaiHenKham.SelectValue,
                        NAMSINH = txtNam.Text.Trim(),
                        NGAYHEN = dEditNgayHen.DateTime.ToString(Const.FORMAT_datetime1),
                        NGAYSINH = dEditNgaySinh.DateTime.ToString(Const.FORMAT_date1),
                        NGHENGHIEPID = ucNgheNghiep.SelectValue,
                        NOILAMVIEC = txtNoiLamViec.Text.Trim(),
                        PHONGKHAMID = ucPhongKham.SelectValue,
                        QUOCGIAID = ucQuocTich.SelectValue,
                        SDTBENHNHAN = txtDienThoai.Text.Trim(),
                        SONHA = txtSoNha.Text.Trim(),
                        TENBENHNHAN = txtHoTen.Text.Trim(),
                        TENHUYEN = ucHuyen.SelectText,
                        TENQUOCGIA = ucQuocTich.SelectText,
                        TENTINH = ucTinh.SelectText,
                        TENXA = ucXa.SelectText,
                        TKDANTOCID = ucDanToc.SelectValue,
                        TKDICHVUID = string.Empty,
                        TKGIOITINHID = ucGioiTinh.SelectValue,
                        TKHC_HUYENID = ucHuyen.SelectValue,
                        TKHC_TINHID = ucTinh.SelectValue,
                        TKHC_XAID = ucXa.SelectValue,
                        TKNGHENGHIEPID = ucNgheNghiep.SelectValue,
                        TKPHONGID = string.Empty,
                        TKQUOCGIAID = ucQuocTich.SelectValue,
                        TUOI = txtTuoi.Text.Trim(),
                        USER_ID = ucBacSiKham.SelectValue,
                        USER_NAME = string.Empty,
                        YEUCAUKHAM = ucYeuCauKham.SelectText
                    };

                    string objData = JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");

                    string ret = RequestHTTP.call_ajaxCALL_SP_S_result("NGT01T008.LUUHK", objData);
                    var rets = ret.Split(',');
                    if (rets.Length > 1)
                    {
                        MessageBox.Show("Cập nhật thông tin bệnh nhân thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                        InitStatusButton();
                        SetDefaultData();
                        LoadDataGrid(1);
                        isBlock = false;
                        this.ReadOnly = false;
                        CaptionValidate = true;
                        dEditTungay.Focus();
                    }
                    else if ("loitenbn".Equals(ret))
                    {
                        txtDiaChi.Focus();
                        MessageBox.Show("Tên bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if ("loidiachibn".Equals(ret))
                    {
                        MessageBox.Show("Địa chỉ bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thông tin không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                benhnhanID = string.Empty;
                lichhenID = string.Empty;
                SetDefaultData();
                InitStatusButton();
                this.ReadOnly = false;
                CaptionValidate = true;
                isBlock = false;
                dEditTungay.Focus();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            VNPT.HIS.CommonForm.NGT02K064_QLHenKham_GuiTN frm = new VNPT.HIS.CommonForm.NGT02K064_QLHenKham_GuiTN();
            //frm.FormClosing += childFormClosing;
            frm.StartPosition = FormStartPosition.CenterScreen;
            //Enabled = false;
            frm.Show();
        }
        #endregion

        #region Event control
        private void txtSoNha_Leave(object sender, EventArgs e)
        {
            SetDiaChiBN();
        }

        private void dEditNgaySinh_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dEditNgaySinh.Text)) return;
            Nhap_NgaySinh();
        }

        private void dEditNgaySinh_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dEditNgaySinh.Text)) return;
            Nhap_NgaySinh();
        }

        private void txtNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ucGioitinh_KeyEnter(object sender, EventArgs e)
        {
            ucNgheNghiep.Focus();
        }

        private void ucNghenghiep_KeyEnter(object sender, EventArgs e)
        {
            ucDanToc.Focus();
        }

        private void ucDantoc_KeyEnter(object sender, EventArgs e)
        {
            ucQuocTich.Focus();
        }

        private void ucQuoctich_KeyEnter(object sender, EventArgs e)
        {
            txtSoNha.Focus();
        }

        private void txtSoNha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) ucTinh.Focus();
        }

        private void ucTinh_KeyEnter(object sender, EventArgs e)
        {
            ucHuyen.Focus();
        }

        private void ucHuyen_KeyEnter(object sender, EventArgs e)
        {
            ucXa.Focus();
        }

        private void ucXa_KeyEnter(object sender, EventArgs e)
        {
            txtDiaChi.Focus();
        }

        private void cboTuoi_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyData == Keys.Tab) e.IsInputKey = true;
        }

        private void txtDiaChi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtDienThoai.Focus();
        }

        private void txtDienThoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) dEditNgayHen.Focus();
        }

        private void dEditNgayHen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtNoiLamViec.Focus();
            }
        }

        private void txtNoiLamViec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) ucYeuCauKham.Focus();
        }

        private void dEditNgaySinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtNam.Focus();
            }
        }

        private void ucYeuCauKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                ucPhongKham.Focus();
            }
        }

        private void ucPhongKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                ucBacSiKham.Focus();
            }
        }

        private void ucBacSiKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                ucLoaiHenKham.Focus();
            }
        }

        private void cboTuoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTuoi.SelectedIndex != 0)
            {
                ucNgheNghiep.SelectIndex = 2;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtTuoi.Text) && Convert.ToInt64(txtTuoi.Text.Trim()) < 7)
                {
                    ucNgheNghiep.SelectIndex = 2;
                }
                else
                {
                    ucNgheNghiep.SelectIndex = 0;
                }
            }
        }
        #endregion

        private void ucLoaiHenKham_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnAdd.Focus();
        }

        private void txtTuoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (cboTuoi.Enabled)
                    cboTuoi.Focus();
                else
                    ucGioiTinh.Focus();
            }
        }

        private void txtTuoi_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab) e.IsInputKey = true;
        }

        private void ucTrangThai_Leave(object sender, EventArgs e)
        {
            //txtHoTen.Focus();
        }
    }
}