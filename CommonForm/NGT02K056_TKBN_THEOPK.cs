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
    public partial class NGT02K056_TKBN_THEOPK : DevExpress.XtraEditors.XtraForm
    {

        #region Variable

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool isFormLoading = true;

        #endregion

        #region Private

        /// <summary>
        /// Khởi tạo giá trị ban đầu
        /// </summary>
        private void InitForm ()
        {
            try
            {
                InitDate();
                InitComboBox();
                InitGrid();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        
        /// <summary>
        /// Khởi tạo giá trị Date
        /// </summary>
        private void InitDate ()
        {
            deTuNgay.DateTime = Func.getSysDatetime();
            deDenNgay.DateTime = Func.getSysDatetime();
        }

        /// <summary>
        /// Khởi tạo giá trị ComboBox
        /// </summary>
        private void InitComboBox()
        {
            InitKhoa();
            InitPhong("0");
        }

        /// <summary>
        /// Khởi tạo giá trị ComboBox Khoa
        /// </summary>
        private void InitKhoa ()
        {
            DataTable dtKhoa = RequestHTTP.get_ajaxExecuteQuery("NGT.LOADKHOA");

            if (dtKhoa.Rows.Count <= 0)
            {
                dtKhoa = Func.getTableEmpty(new String[] { "col1", "col2" });
            }

            DataRow drKhoa = dtKhoa.NewRow();
            drKhoa["col1"] = "0";
            drKhoa["col2"] = "--Tất cả--";
            dtKhoa.Rows.InsertAt(drKhoa, 0);

            ucCboKhoa.setData(dtKhoa, 0, 1);
            ucCboKhoa.setColumn(0, false);
            ucCboKhoa.lookUpEdit.Properties.BestFit();
            ucCboKhoa.setEvent(ucCboKhoa_EditValueChanged);
            ucCboKhoa.lookUpEdit.EnterMoveNextControl = true;
            ucCboKhoa.SelectIndex = 0;
        }

        /// <summary>
        /// Khởi tạo giá trị ComboBox Phòng theo Khoa
        /// </summary>
        /// <param name="maKhoa"></param>
        private void InitPhong (string maKhoa)
        {
            DataTable dtPhong = null;
            if (!"0".Equals(maKhoa))
            {
                dtPhong = RequestHTTP.get_ajaxExecuteQuery("NGT.LOADPHONG", new string[] { "[0]" }, new string[] { maKhoa });
            }

            if (dtPhong == null || dtPhong.Rows.Count <= 0)
            {
                dtPhong = Func.getTableEmpty(new String[] { "col1", "col2" });
            }

            DataRow drPhong = dtPhong.NewRow();
            drPhong["col1"] = "0";
            drPhong["col2"] = "--Tất cả--";
            dtPhong.Rows.InsertAt(drPhong, 0);

            ucCboPhong.setData(dtPhong, 0, 1);
            ucCboPhong.setColumn(0, false);
            ucCboPhong.lookUpEdit.EnterMoveNextControl = true;

            ucCboPhong.SelectIndex = 0;
        }

        /// <summary>
        /// Khởi tạo giá trị Grid
        /// </summary>
        private void InitGrid ()
        {
            ucGridDanhSachBenhNhan.gridView.OptionsView.ShowViewCaption = false;
            ucGridDanhSachBenhNhan.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGridDanhSachBenhNhan.gridView.OptionsBehavior.Editable = false;

            ucGridDanhSachBenhNhan.setEvent(PageLoad_ucGridDanhSachBenhNhan);

            // Set default 200, index = 2
            ucGridDanhSachBenhNhan.setNumberPerPage(new int[] { 20, 100, 200, 300, 2000 }, 2);
            ucGridDanhSachBenhNhan.onIndicator();
        }

        private void TimKiem()
        {
            this.LoadDataGrid(1);
        }

        private void LoadDataGrid (int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                string tuNgay = deTuNgay.EditValue == null ? string.Empty : deTuNgay.DateTime.ToString(Const.FORMAT_date1);
                string denNgay = deDenNgay.EditValue == null ? string.Empty : deDenNgay.DateTime.ToString(Const.FORMAT_date1);
                string khoa = ucCboKhoa.SelectValue;
                string phong = ucCboPhong.SelectValue;

                ResponsList responses = new ResponsList();
                
                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "DS.NGT.TKBN"
                    , page
                    , ucGridDanhSachBenhNhan.ucPage1.getNumberPerPage()
                    , new String[] { "[0]", "[1]", "[2]", "[3]" }
                    , new string[] { tuNgay, denNgay, khoa, phong }
                    , "");

                ucGridDanhSachBenhNhan.clearData();

                DataTable dtDSPK = new DataTable();
                dtDSPK = MyJsonConvert.toDataTable(responses.rows);
                if (dtDSPK.Rows.Count <= 0)
                {
                    dtDSPK = Func.getTableEmpty(new String[] { "ORG_NAME", "BH_CK", "BH_DK", "BH_KTK", "VP_CK", "VP_DK", "VP_KTK", "DV_CK", "DV_DK", "DV_KTK", "TONGSO" });
                }

                ucGridDanhSachBenhNhan.setData(dtDSPK, responses.total, responses.page, responses.records);

                ucGridDanhSachBenhNhan.setColumnAll(false);
                ucGridDanhSachBenhNhan.setColumnMemoEdit("ORG_NAME", 0, "Tên phòng khám", 0);
                ucGridDanhSachBenhNhan.setColumn("BH_CK", 1, "BH-Chờ khám", 0);
                ucGridDanhSachBenhNhan.setColumn("BH_DK", 2, "BH-Đang khám", 0);
                ucGridDanhSachBenhNhan.setColumn("BH_KTK", 3, "BH-KT khám", 0);
                ucGridDanhSachBenhNhan.setColumn("VP_CK", 4, "VP-Chờ khám", 0);
                ucGridDanhSachBenhNhan.setColumn("VP_DK", 5, "VP-Đang khám", 0);
                ucGridDanhSachBenhNhan.setColumn("VP_KTK", 6, "VP-KT Khám", 0);
                ucGridDanhSachBenhNhan.setColumn("DV_CK", 7, "DV-Chờ khám", 0);
                ucGridDanhSachBenhNhan.setColumn("DV_DK", 8, "DV-Đang khám", 0);
                ucGridDanhSachBenhNhan.setColumn("DV_KTK", 9, "DV-KT khám", 0);
                ucGridDanhSachBenhNhan.setColumn("TONGSO", 10, "Tổng số", 0);

                ucGridDanhSachBenhNhan.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        #endregion

        #region Public

        public NGT02K056_TKBN_THEOPK()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private void NGT02K056_TKBN_THEOPK_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void NGT02K056_TKBN_THEOPK_Shown(object sender, EventArgs e)
        {
            this.isFormLoading = false;
            btnTimKiem.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            this.TimKiem();
        }

        private void PageLoad_ucGridDanhSachBenhNhan(object sender, EventArgs e)
        {
            // Nếu Form đang load lần đầu tiên thi không load dữ liệu lên danh sách
            if (!isFormLoading)
            {
                int pageNum = sender != null ? (int)sender : 1;
                LoadDataGrid(pageNum);
            }            
        }

        private void ucCboKhoa_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    DataRowView data = (DataRowView)sender;
                    string maKhoa = data["col1"] == null ? string.Empty : data["col1"].ToString();

                    InitPhong(maKhoa);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        #endregion

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}