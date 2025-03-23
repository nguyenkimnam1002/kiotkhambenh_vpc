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
    public partial class NGT02K057_ThuocDiUng : DevExpress.XtraEditors.XtraForm
    {
        #region Class

        public class DsThuoc
        {
            public int total { get; set; }
            public int page { get; set; }
            public int records { get; set; }
            public List<Thuoc> rows { get; set; }

        }
        public class Thuoc
        {
            public string MA { get; set; }
            public string TEN { get; set; }
            public string HOATCHAT { get; set; }
            public string MAHOATCHAT { get; set; }
            public string RN { get; set; }
            public string THUOCVATTUID { get; set; }
        }

        #endregion

        #region Variable

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string actionThem = "Them";
        private readonly string actionSua = "Sua";
        private readonly string actionXoa = "Xoa";
        private readonly string actionLuu = "Luu";
        private readonly string actionHuy = "Huy";
        private readonly string actionRong = "Rong";

        private string benhNhanId = string.Empty;
        private string id = string.Empty;
        private string thuocVatTuId = string.Empty;

        private bool isInsertOrUpdate = false;
        private bool isActionThem = false;

        #endregion
        
        #region Private

        /// <summary>
        /// Khởi tạo giá trị khi form load
        /// </summary>
        private void InitForm ()
        {
            this.InitGrid();
            this.InitComboBox();
            
            this.SetEnabledByActionName(actionRong);
            this.SetData(null);
        }

        /// <summary>
        /// Khởi tạo giá trị Grid
        /// </summary>
        private void InitGrid()
        {
            ucGridDanhSachThuocDiDung.gridView.OptionsView.ShowViewCaption = false;
            ucGridDanhSachThuocDiDung.gridView.OptionsBehavior.Editable = false;

            ucGridDanhSachThuocDiDung.setEvent(PageLoad_ucGridDanhSachThuocDiDung);
            ucGridDanhSachThuocDiDung.gridView.Click += GridView_Click;
            ucGridDanhSachThuocDiDung.SetReLoadWhenFilter(true);

            ucGridDanhSachThuocDiDung.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
            ucGridDanhSachThuocDiDung.onIndicator();
        }

        /// <summary>
        /// Khởi tạo giá trị cho SearchLookup
        /// </summary>
        private void InitComboBox()
        {
            try
            {
                // load danh sách thuốc
                DataTable dt = GetDSThuoc();

                if (dt == null || dt.Rows.Count <= 0)
                {
                    dt = Func.getTableEmpty(new string[] { "MA", "TEN", "HOATCHAT", "MAHOATCHAT", "THUOCVATTUID", "RN" });
                }

                ucSearchLookupHoatChat.setData(dt, "MAHOATCHAT", "HOATCHAT");
                ucSearchLookupHoatChat.setAllColumn(false);
                ucSearchLookupHoatChat.setColumn("MA", 0, "MÃ THUỐC", 0);
                ucSearchLookupHoatChat.setColumn("TEN", 1, "TÊN THUỐC", 0);
                ucSearchLookupHoatChat.setColumn("HOATCHAT", 2, "TÊN HOẠT CHẤT", 0);
                ucSearchLookupHoatChat.searchLookUpEdit1View.BestFitColumns(true);
                ucSearchLookupHoatChat.setEvent(ucSearchLookupHoatChat_EditValueChanged);

                ucSearchLookupMaThuoc.setData(dt, "MA", "TEN");
                ucSearchLookupMaThuoc.setAllColumn(false);
                ucSearchLookupMaThuoc.setColumn("MA", 0, "MÃ THUỐC", 0);
                ucSearchLookupMaThuoc.setColumn("TEN", 1, "TÊN THUỐC", 0);
                ucSearchLookupMaThuoc.setColumn("HOATCHAT", 2, "TÊN HOẠT CHẤT", 0);
                ucSearchLookupMaThuoc.searchLookUpEdit1View.BestFitColumns(true);
                ucSearchLookupMaThuoc.setEvent(ucSearchLookupMaThuoc_EditValueChanged);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Load danh sách thuốc dị ứng
        /// </summary>
        /// <param name="page"></param>
        private void LoadDataGrid(int? page)
        {
            try
            {
                int pageNum = 0;
                if (page == null)
                {
                    pageNum = ucGridDanhSachThuocDiDung.ucPage1.Current();
                }
                else
                {
                    pageNum = page.GetValueOrDefault();
                }

                if (pageNum <= 0)
                {
                    pageNum = 1;
                }

                ResponsList responses = new ResponsList();
                //string jsonFilter = string.Empty;

                //if (ucGridDanhSachThuocDiDung.ReLoadWhenFilter
                //    && ucGridDanhSachThuocDiDung.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridDanhSachThuocDiDung.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "NGT057.LAYDS"
                    , pageNum
                    , ucGridDanhSachThuocDiDung.ucPage1.getNumberPerPage()
                    , new String[] { "[0]" }
                    , new string[] { this.benhNhanId }
                    , ucGridDanhSachThuocDiDung.jsonFilter());

                ucGridDanhSachThuocDiDung.clearData();

                DataTable dtDSPK = new DataTable();
                dtDSPK = MyJsonConvert.toDataTable(responses.rows);
                if (dtDSPK.Rows.Count <= 0)
                {
                    dtDSPK = Func.getTableEmpty(new String[] { "TEN", "MA", "HOATCHAT", "MAHOATCHAT", "GHICHU" });
                }

                ucGridDanhSachThuocDiDung.setData(dtDSPK, responses.total, responses.page, responses.records);

                ucGridDanhSachThuocDiDung.setColumnAll(false);
                ucGridDanhSachThuocDiDung.setColumnMemoEdit("TEN", 0, "Tên thuốc", 0);
                ucGridDanhSachThuocDiDung.setColumnMemoEdit("MA", 1, "Mã thuốc", 0);
                ucGridDanhSachThuocDiDung.setColumnMemoEdit("HOATCHAT", 2, "Tên hoạt chất", 0);
                ucGridDanhSachThuocDiDung.setColumnMemoEdit("MAHOATCHAT", 3, "Mã hoạt chất", 0);
                ucGridDanhSachThuocDiDung.setColumnMemoEdit("GHICHU", 4, "Ghi chú", 0);

                ucGridDanhSachThuocDiDung.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        
        /// <summary>
        /// Set giá trị khi chọn 1 dòng trên danh sách thuốc dị ứng
        /// </summary>
        /// <param name="drThuoc"></param>
        private void SetData(DataRowView drThuoc)
        {
            // nếu drThuoc = null, clear giá trị của item, ngược lại thì set giá trị cho item
            if (drThuoc == null)
            {
                this.id = string.Empty;
                this.thuocVatTuId = string.Empty;
                meGhiChu.Text = string.Empty;
                ucSearchLookupHoatChat.SelectValue = string.Empty;
                ucSearchLookupMaThuoc.SelectValue = string.Empty;
                ucCboTenThuoc.SelectValue = string.Empty;
                ucSearchLookupHoatChat.SelectText = " ";
                ucSearchLookupMaThuoc.SelectText = " ";
                ucCboTenThuoc.SelectText = " ";
            }
            else if (!isInsertOrUpdate)
            {
                ucSearchLookupMaThuoc.SelectValue = drThuoc["Ma"].ToString();

                this.id = drThuoc["ID"].ToString();
                meGhiChu.Text = drThuoc["GHICHU"].ToString();

                SetEnabledByActionName(string.Empty);
            }
        }

        /// <summary>
        /// Set giá trị khi thuốc từ ucSearchLookupHoatChat, ucSearchLookupMaThuoc có thay đổi
        /// </summary>
        /// <param name="drThuoc"></param>
        private void SetDataThuoc(DataRowView drThuoc)
        {
            try
            {
                this.thuocVatTuId = drThuoc["THUOCVATTUID"].ToString();

                string maThuoc = drThuoc["Ma"].ToString();
                string tenThuoc = drThuoc["TEN"].ToString();
                string maHoatChat = drThuoc["MAHOATCHAT"].ToString();
               
                ucSearchLookupHoatChat.SelectValue = maHoatChat;
                ucSearchLookupMaThuoc.SelectValue = maThuoc;
                ucCboTenThuoc.SelectValue = tenThuoc;

                // set tên thuốc vào danh sách tên thuốc cho datasource ucCboTenThuoc
                if (!string.IsNullOrEmpty(maThuoc))
                {
                    DataTable dtTenThuoc = ucCboTenThuoc.lookUpEdit.Properties.DataSource as DataTable;
                    if (dtTenThuoc == null || dtTenThuoc.Rows.Count <= 0)
                    {
                        dtTenThuoc = Func.getTableEmpty(new string[] { "TEN" });
                    }
                    bool isFlag = false;
                    foreach (System.Data.DataRow item in dtTenThuoc.Rows)
                    {
                        if (tenThuoc.Equals(item["TEN"].ToString()))
                        {
                            isFlag = true;
                            break;
                        }
                    }

                    if (!isFlag)
                    {
                        DataRow row = dtTenThuoc.NewRow();
                        row["TEN"] = tenThuoc;

                        dtTenThuoc.Rows.Add(row);
                        ucCboTenThuoc.setColumnAll(false);
                        ucCboTenThuoc.setData(dtTenThuoc, "TEN", "TEN");
                        ucCboTenThuoc.setColumn("TEN", 0, "", 0);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Them()
        {
            SetData(null);
            SetEnabledByActionName(actionThem);
            isInsertOrUpdate = true;
            isActionThem = true;
        }

        private void Sua()
        {
            SetEnabledByActionName(actionSua);

            isInsertOrUpdate = true;
            isActionThem = false;
        }

        private void Xoa()
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa chi tiết dị ứng thuốc này ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.OK)
            {
                string ma = ucSearchLookupMaThuoc.SelectValue;
                string ten = ucCboTenThuoc.Text;
                string hoatChat = ucSearchLookupHoatChat.SelectText;
                string ghiChu = meGhiChu.Text.Trim();

                var data = new
                {
                    GHICHU = ghiChu,
                    HOATCHAT = hoatChat,
                    ID = this.id,
                    MA = ma,
                    THUOC = ten,
                    THUOCVATTUID = this.thuocVatTuId
                };

                var dataJson = JsonConvert.SerializeObject(data).Replace("\"", "\\\"");

                var rs = RequestHTTP.call_ajaxCALL_SP_I("NGT057.XOA", dataJson);
                if ("1".Equals(rs))
                {
                    MessageBox.Show("Xóa thành công !");
                    LoadDataGrid(null);
                    isInsertOrUpdate = false;
                    SetData(null);
                    SetEnabledByActionName(actionRong);
                    isActionThem = false;
                }
                else
                {
                    MessageBox.Show("Lỗi xóa chi tiết dị ứng thuốc !");
                }
            }
        }

        private void Luu()
        {
            isActionThem = false;
            string ma = ucSearchLookupMaThuoc.SelectValue;
            string ten = ucCboTenThuoc.Text;
            string hoatChat = ucSearchLookupHoatChat.SelectText;
            string ghiChu = meGhiChu.Text.Trim();

            string msg = "";
            if (string.IsNullOrWhiteSpace(ma))
            {
                msg += "Thuốc không được để trống.";
            }

            if (string.IsNullOrWhiteSpace(hoatChat))
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    msg += "\n";
                }

                msg += "Hoạt chất không được để trống.";
            }

            if (string.IsNullOrWhiteSpace(ghiChu))
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    msg += "\n";
                }

                msg += "Ghi chú không được để trống.";
            }

            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg);
                return;
            }

            isInsertOrUpdate = false;
            var data = new
            {
                BENHNHANID = this.benhNhanId,
                GHICHU = ghiChu,
                HOATCHAT = hoatChat,
                ID = this.id,
                KHOAID = Const.local_khoaId,
                MA = ma,
                PHONGID = Const.local_phongId,
                THUOC = ten,
                THUOCVATTUID = this.thuocVatTuId
            };

            var dataJson = JsonConvert.SerializeObject(data).Replace("\"", "\\\"");

            if (string.IsNullOrEmpty(this.id))
            {              
                var rs = RequestHTTP.call_ajaxCALL_SP_I("NGT057.THEM", dataJson);
                if ("1".Equals(rs))
                {
                    MessageBox.Show("Thêm mới thành công !");
                    LoadDataGrid(null);
                    SetEnabledByActionName(actionRong);

                }
                else if ("2".Equals(rs))
                {
                    MessageBox.Show("Thuốc thêm mới đã tồn tại !");
                }
                else
                {
                    MessageBox.Show("Lỗi thêm mới dị ứng thuốc !");
                }
            }
            else
            {
                var rs = RequestHTTP.call_ajaxCALL_SP_I("NGT057.SUA", dataJson);
                if ("1".Equals(rs))
                {
                    MessageBox.Show("Cập nhật thành công !");
                    LoadDataGrid(null);
                    SetEnabledByActionName(actionRong);
                    
                }
                else if ("2".Equals(rs))
                {
                    MessageBox.Show("Thuốc cập nhật đã tồn tại !");
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật thông tin dị ứng thuốc !");
                }
            }
        }

        private void Huy()
        {
            SetEnabledByActionName(isActionThem ? actionRong : actionHuy);
            isInsertOrUpdate = false;
            isActionThem = false;
            this.SetData(null);
        }
        
        private bool ReadOnly
        {
            set
            {
                ucSearchLookupMaThuoc.searchLookUpEdit1.ReadOnly = value;
                ucSearchLookupHoatChat.searchLookUpEdit1.ReadOnly = value;
                ucCboTenThuoc.lookUpEdit.ReadOnly = value;
                meGhiChu.ReadOnly = value;
            }
        }

        private void SetEnabledByActionName(string loai)
        {
            if (loai.Equals(actionThem)
                || loai.Equals(actionSua))
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;

                this.ReadOnly = false;
            }
            else if (loai.Equals(actionLuu)
                || loai.Equals(actionXoa))
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = true;
                btnHuy.Enabled = true;

                this.ReadOnly = true;
            }
            else if (loai.Equals(actionRong))
            {
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;

                this.ReadOnly = true;
            }
            else
            {
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;

                this.ReadOnly = true;
            }
        }
        
        #endregion

        #region Public

        public NGT02K057_ThuocDiUng()
        {
            InitializeComponent();
        }

        public DataTable GetDSThuoc()
        {
            DataTable dt = new DataTable();
            DsThuoc ds = new DsThuoc();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxExecuteQueryPaging", new String[] { "NGT.LOADTHUOC" }
                    , new String[] { }
                    , new String[] { });

                string data = "func=doComboGrid" +
                   "&page=1" +
                   "&postData=" + request +
                   "&rows=100000" +
                   "&searchTerm=" +
                   "&sidx=" +
                   "&sord=";

                string ret = RequestHTTP.getRequest(data);

                ds = JsonConvert.DeserializeObject<DsThuoc>(ret, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                dt = ds.rows.ConvertListToDataTable<Thuoc>();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return dt;
        }

        public void setParam(string benhNhanId)
        {
            this.benhNhanId = benhNhanId; 
        }

        #endregion

        #region Events

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private void NGT02K057_ThuocDiUng_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            this.Them();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            this.Sua();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.Xoa();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.Luu();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Huy();
        }

        private void PageLoad_ucGridDanhSachThuocDiDung(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            LoadDataGrid(pageNum);
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            DataRowView drThuoc = (DataRowView)ucGridDanhSachThuocDiDung.gridView.GetFocusedRow();
            if (drThuoc != null)
            {
                this.SetData(drThuoc);
            }
        }

        private void ucSearchLookupMaThuoc_EditValueChanged(object sender, EventArgs e)
        {
            if (sender != null && !string.IsNullOrEmpty(ucSearchLookupMaThuoc.SelectValue))
            {
                DataRowView drThuoc = (DataRowView)sender;
                this.SetDataThuoc(drThuoc);
            }
        }

        private void ucSearchLookupHoatChat_EditValueChanged(object sender, EventArgs e)
        {
            if (sender != null && !string.IsNullOrEmpty(ucSearchLookupHoatChat.SelectValue))
            {
                DataRowView drThuoc = (DataRowView)sender;
                this.SetDataThuoc(drThuoc);
            }
        }

        #endregion
    }
}