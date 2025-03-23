using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using System.Globalization;
using Newtonsoft.Json;
using System.Drawing;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T001_Dichvusai : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T001_Dichvusai()
        {
            InitializeComponent();
        }

        public void SetParams(DataTable dtDichVu)
        { 
            
            if (dtDichVu == null || dtDichVu.Rows.Count <= 0)
            {
                dtDichVu = Func.getTableEmpty(
                    new String[] {
                                "DICHVUKHAMBENHID"
                                ,"TIEPNHANID"
                                , "KHOA"
                                , "KHOAID"
                                , "PHONGID"
                                , "KHAMBENHID"
                                , "BHYT_DV"
                                , "BHYT_BNTT"
                                , "LOAI_DOITUONG"
                                , "MAUBENHPHAMID"
                                , "LOAINHOMMAUBENHPHAM"
                                , "KHOANMUCID"
                                , "MAKHOANMUC"
                                , "TENKHOANMUC"
                                , "DOITUONGBENHNHANID"
                                , "NHOMTHANHTOAN"
                                , "DOITUONGDV"
                                , "DOITUONG"
                                , "NHOM_MABHYT"
                                , "DATHUTIEN"
                                , "TENDICHVU"
                                , "SOLUONG"
                                , "SOLUONG_OLD"
                                , "TIENDICHVU"
                                , "THANHTIEN"
                                , "TIEN_BHYT_TRA"
                                , "THUCTHU"
                                , "TYLE_BHYT_TRA"
                                , "TIEN_MIENGIAM"
                                , "TYLE_DV"
                                , "VERSION_OLD"
                                , "TYLE"
                                , "TYLEMIENGIAM"
                                , "VATTU04"
                    });
            }

            for (int i = 0; i < dtDichVu.Rows.Count; i++)
            {
                dtDichVu.Rows[i]["TIENDICHVU"] = FormatMoney(dtDichVu.Rows[i]["TIENDICHVU"].ToString());
                dtDichVu.Rows[i]["TIEN_BHYT_TRA"] = FormatMoney(dtDichVu.Rows[i]["TIEN_BHYT_TRA"].ToString());
                dtDichVu.Rows[i]["THUCTHU"] = FormatMoney(dtDichVu.Rows[i]["THUCTHU"].ToString());
                dtDichVu.Rows[i]["TIEN_MIENGIAM"] = FormatMoney(dtDichVu.Rows[i]["TIEN_MIENGIAM"].ToString());
            }

            ucGridDichVu.setData(dtDichVu, 999999, 1);

            ucGridDichVu.setColumnAll(false);
            ucGridDichVu.setColumnMemoEdit("TENDICHVU", 0, "Tên dịch vụ", 180);
            ucGridDichVu.setColumn("SOLUONG", 1, "SL", 35);
            ucGridDichVu.setColumn("TIENDICHVU", 2, "Giá tiền", 85);
            ucGridDichVu.setColumn("TIEN_BHYT_TRA", 3, "BHYT trả", 85);
            ucGridDichVu.setColumn("THUCTHU", 4, "BN trả", 85);
            ucGridDichVu.setColumn("TYLE_BHYT_TRA", 5, "TL %", 40);
            ucGridDichVu.setColumn("TYLE_MIENGIAM", 6, "% mg", 40);
            ucGridDichVu.setColumn("TIEN_MIENGIAM", 7, "Miễn giảm", 85);
            ucGridDichVu.setColumn("DOITUONG", 8, "", 90);
            ucGridDichVu.setColumn("NHOM_MABHYT", 9, "", 90);

            ucGridDichVu.gridView.Columns["TENDICHVU"].OptionsColumn.AllowEdit = false;
            ucGridDichVu.gridView.Columns["SOLUONG"].OptionsColumn.AllowEdit = false;
            ucGridDichVu.gridView.Columns["TIENDICHVU"].OptionsColumn.AllowEdit = false;
            ucGridDichVu.gridView.Columns["TIEN_BHYT_TRA"].OptionsColumn.AllowEdit = false;
            ucGridDichVu.gridView.Columns["THUCTHU"].OptionsColumn.AllowEdit = false;
            ucGridDichVu.gridView.Columns["TYLE_BHYT_TRA"].OptionsColumn.AllowEdit = false;
            ucGridDichVu.gridView.Columns["TIEN_MIENGIAM"].OptionsColumn.AllowEdit = false;

            ucGridDichVu.gridView.Columns["TIENDICHVU"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucGridDichVu.gridView.Columns["TIEN_BHYT_TRA"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucGridDichVu.gridView.Columns["THUCTHU"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucGridDichVu.gridView.Columns["TYLE_BHYT_TRA"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucGridDichVu.gridView.Columns["TYLE_MIENGIAM"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            ucGridDichVu.gridView.Columns["TIEN_MIENGIAM"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

            ucGridDichVu.gridView.Columns["DOITUONG"].GroupIndex = 0;
            ucGridDichVu.gridView.Columns["NHOM_MABHYT"].GroupIndex = 1;
            ucGridDichVu.gridView.ExpandAllGroups();
        }

        private string FormatMoney(string money, int soKyTuThapPhan = 2)
        {
            if (soKyTuThapPhan == 0 && "0".Equals(money))
            {
                return money;
            }

            string dinhDang = "{0:0,0";
            for (int i = 0; i < soKyTuThapPhan; i++)
            {
                if (i == 0)
                {
                    dinhDang += ".";
                }

                dinhDang += "0";
            }

            dinhDang += "}";

            return string.Format(dinhDang, Func.Parse_double(money));// "{0:0,0.00}"
        }

        #region Private

        /// <summary>
        /// Khởi tạo giá trị ban đầu
        /// </summary>
        private void InitForm()
        {
            try
            {
                #region load ucGridDichVu
                ucGridDichVu.gridView.OptionsView.ShowViewCaption = false;
                ucGridDichVu.gridView.OptionsBehavior.Editable = false;
                ucGridDichVu.gridView.OptionsView.ShowAutoFilterRow = false;
                ucGridDichVu.gridView.GroupFormat = "[#image]{1} {2}";
                ucGridDichVu.gridView.OptionsBehavior.Editable = true;
                ucGridDichVu.gridView.OptionsView.ColumnAutoWidth = false;
                ucGridDichVu.gridView.RowStyle += GridDichVu_RowStyle;
                
                #endregion
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void GridDichVu_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DataRowView dr = (DataRowView)ucGridDichVu.gridView.GetRow(e.RowHandle);
            if (dr == null)
            {
                return;
            }

            if (ucGridDichVu.gridView.IsGroupRow(e.RowHandle))
            {
                return;
            }

            e.Appearance.ForeColor = Color.Red;
        }

        #endregion

        private void bbtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}