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

namespace VNPT.HIS.QTHethong
{
    public partial class DMC02_DM_CAUHINH_USER : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public DMC02_DM_CAUHINH_USER()
        {
            InitializeComponent();
        }

        private string cauHinhID = string.Empty;
        private bool isEdit = false;

        private void Init_Form()
        {
            try
            {
                ucgview_DSCHNV.gridView.ViewCaption = "Danh sách cấu hình người dùng";
                ucgview_DSCHNV.gridView.OptionsView.ColumnAutoWidth = true;
                ucgview_DSCHNV.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucgview_DSCHNV.gridView.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid
                ucgview_DSCHNV.gridView.OptionsBehavior.Editable = false;

                ucgview_DSCHNV.setEvent(ucgview_DSCHNV_Load);
                ucgview_DSCHNV.SetReLoadWhenFilter(true);
                ucgview_DSCHNV.gridView.Click += ucgview_DSCHNV_Click;
                //ucgview_DSCHNV.gridView.ColumnFilterChanged += ucgview_DSCHNV_ColumnFilterChanged;

                ucgview_DSCHNV.setNumberPerPage(new int[] { 100, 200, 300 });
                ucgview_DSCHNV.onIndicator();

                DataTable dt_NhomCauHinh = new DataTable();
                dt_NhomCauHinh = RequestHTTP.get_ajaxExecuteQuery("DMC02.DMUSER.02");
                if (dt_NhomCauHinh == null || dt_NhomCauHinh.Rows.Count <= 0)
                {
                    dt_NhomCauHinh = Func.getTableEmpty(new string[] { "col1", "col2" });
                }
                uccbox_NhomCH.setData(dt_NhomCauHinh, "col1", "col2");
                uccbox_NhomCH.setColumn(0, false);

                uccbox_NhomCH.SelectValue = dt_NhomCauHinh.Rows[0]["col1"].ToString();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Load_DataGrid(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                ResponsList responses = new ResponsList();

                //string jsonFilter = "";
                //if (ucgview_DSCHNV.ReLoadWhenFilter && ucgview_DSCHNV.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSCHNV.tableFlterColumn);
                //}

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "DMC02.DMUSER.01", page, ucgview_DSCHNV.ucPage1.getNumberPerPage(), new string[] { }, new string[] { }, ucgview_DSCHNV.jsonFilter());

                ucgview_DSCHNV.clearData();

                DataTable dt_DSPHT = new DataTable();
                dt_DSPHT = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSPHT.Rows.Count == 0)
                    dt_DSPHT = Func.getTableEmpty(new String[] { "CAUHINHID", "TENNHOMCAUHINH", "MACAUHINH", "GIATRI_MACDINH", "TENCAUHINH", "NHOMCAUHINHID", "MOTA", "SUDUNG" });

                ucgview_DSCHNV.setData(dt_DSPHT, responses.total, responses.page, responses.records);
                ucgview_DSCHNV.setColumnAll(false);

                ucgview_DSCHNV.setColumn("MACAUHINH", 1, "Mã cấu hình");
                ucgview_DSCHNV.setColumn("GIATRI_MACDINH", 2, "Giá trị mặc định");
                ucgview_DSCHNV.setColumn("TENCAUHINH", 3, "Tên cấu hình");

                ucgview_DSCHNV.gridView.Columns["TENNHOMCAUHINH"].GroupIndex = 0;
                ucgview_DSCHNV.gridView.ExpandAllGroups();

                ucgview_DSCHNV.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void Set_Data(DataRowView dr_CauHinh)
        {
            try
            {
                cauHinhID = dr_CauHinh["CAUHINHID"].ToString();
                var data_ar = RequestHTTP.call_ajaxCALL_SP_O("DMC02.DMUSER.03", cauHinhID, 0);
                if (data_ar != null && data_ar.Rows.Count > 0)
                {
                    etext_TenCH.Text = data_ar.Rows[0]["TENCAUHINH"].ToString();
                    etext_MaCH.Text = data_ar.Rows[0]["MACAUHINH"].ToString();
                    uccbox_NhomCH.SelectValue = data_ar.Rows[0]["NHOMCAUHINHID"].ToString();
                    etext_GiaTri.Text = data_ar.Rows[0]["GIATRI_MACDINH"].ToString();
                    ememo_MoTa.Text = data_ar.Rows[0]["MOTA"].ToString().Replace("\n", "\r\n");
                    echeck_SuDung.Checked = data_ar.Rows[0]["SUDUNG"].ToString() == "1" ? true : false;

                    if (data_ar.Rows[0]["SUDUNG"].ToString() == "0")
                        Set_Enabled("Load0");
                    else
                        Set_Enabled("Load1");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void DMC02_DM_CAUHINH_USER_Load(object sender, EventArgs e)
        {
            Init_Form();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            isEdit = false;
            etext_TenCH.Text = "";
            etext_MaCH.Text = "";
            uccbox_NhomCH.SelectIndex = 0;
            etext_GiaTri.Text = "";
            ememo_MoTa.Text = "";
            echeck_SuDung.Checked = true;

            Set_Enabled("Them");
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            isEdit = true;
            Set_Enabled("Sua");
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            string request = RequestHTTP.call_ajaxCALL_SP_I("DMC02.DMUSER.05", cauHinhID);
            if (request == "1")
            {
                MessageBox.Show("Xóa thành công !");
                Load_DataGrid(1);
            }
            else
            {
                MessageBox.Show("Không thành công !");
            }
            Load_DataGrid(1);
            Set_Enabled("Xoa");
        }

        private void btn_KhoiPhuc_Click(object sender, EventArgs e)
        {
            string request = RequestHTTP.call_ajaxCALL_SP_I("DMC02.DMUSER.06", cauHinhID);
            if (request == "1")
            {
                MessageBox.Show("Khôi phục thành công !");
                Load_DataGrid(1);
            }
            else
            {
                MessageBox.Show("Không thành công !");
            }
            Load_DataGrid(1);
            Set_Enabled("KhoiPhuc");
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string tenCH = etext_TenCH.Text.Trim();
            string maCH = etext_MaCH.Text.Trim();
            while (tenCH.IndexOf("  ") >= 0)
                tenCH = tenCH.Replace("  ", " ");
            while (maCH.IndexOf("  ") >= 0)
                maCH = maCH.Replace("  ", " ");
            if (tenCH != "" && tenCH != " " && maCH != "" && maCH != " ")
            {
                string json = "";
                json += Func.json_item("TENCAUHINH", tenCH);
                json += Func.json_item("MACAUHINH", maCH);
                json += Func.json_item("GIATRI_MACDINH", etext_GiaTri.Text);
                json += Func.json_item("MOTA", ememo_MoTa.Text.Replace("\n", "\r\n"));
                json += Func.json_item("NHOMCAUHINHID", uccbox_NhomCH.SelectValue);
                json += Func.json_item("SUDUNG", echeck_SuDung.Checked == true ? "1" : "0");
                if (isEdit)
                    json += Func.json_item("CAUHINHID", cauHinhID);
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"");

                string request = RequestHTTP.call_ajaxCALL_SP_I("DMC02.DMUSER.04", json);
                if (request == "1")
                {
                    MessageBox.Show("Thêm mới thành công !");
                    Load_DataGrid(1);
                }
                else if (request == "2")
                {
                    MessageBox.Show("Cập nhật thành công !");
                    Load_DataGrid(1);
                }
                else if (request == "0")
                {
                    MessageBox.Show("Mã cấu hình đã có !");
                    Load_DataGrid(1);
                }
                else
                {
                    MessageBox.Show("Không thành công !");
                }
                echeck_SuDung.Checked = false;
                Set_Enabled("Luu");
            }
            else
            {
                MessageBox.Show("Tên hoặc mã cấu hình không được để trống.");
                return;
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            if (isEdit)
                Set_Enabled("Huy1");
            else
                Set_Enabled("Huy0");
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucgview_DSCHNV_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGrid(pageNum);
        }

        private void ucgview_DSCHNV_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr_CauHinh = (DataRowView)ucgview_DSCHNV.gridView.GetFocusedRow();
                if (dr_CauHinh != null)
                {
                    this.Set_Data(dr_CauHinh);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        //private void ucgview_DSCHNV_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridView view = sender as GridView;
        //        if (view.ActiveEditor is TextEdit)
        //        {
        //            TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //            textEdit.Text = textEdit.Text.Trim();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }
        //}

        private void Set_Enabled(string loai)
        {
            switch (loai)
            {
                case "Load0":
                case "Load1":
                    etext_TenCH.Enabled = false;
                    etext_MaCH.Enabled = false;
                    uccbox_NhomCH.Enabled = false;
                    etext_GiaTri.Enabled = false;
                    ememo_MoTa.Enabled = false;
                    echeck_SuDung.Enabled = false;

                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = true;
                    btn_Xoa.Enabled = true;
                    btn_KhoiPhuc.Enabled = true;
                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;
                    break;
                case "Them":
                case "Sua":
                    etext_TenCH.Enabled = true;
                    etext_MaCH.Enabled = true;
                    uccbox_NhomCH.Enabled = true;
                    etext_GiaTri.Enabled = true;
                    ememo_MoTa.Enabled = true;
                    echeck_SuDung.Enabled = false;

                    btn_Them.Enabled = false;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;
                    btn_KhoiPhuc.Enabled = false;
                    btn_Luu.Enabled = true;
                    btn_Huy.Enabled = true;
                    break;
                case "Xoa":
                case "KhoiPhuc":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;
                    btn_KhoiPhuc.Enabled = false;
                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;
                    break;
                case "Luu":
                case "Huy0":
                case "Huy1":
                    etext_TenCH.Enabled = false;
                    etext_MaCH.Enabled = false;
                    uccbox_NhomCH.Enabled = false;
                    etext_GiaTri.Enabled = false;
                    ememo_MoTa.Enabled = false;
                    echeck_SuDung.Enabled = false;

                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;
                    btn_KhoiPhuc.Enabled = false;
                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;
                    break;
            }
            if (loai == "Load1")
                btn_KhoiPhuc.Enabled = false;
            if (loai == "Sua")
                etext_MaCH.Enabled = false;
            if (loai == "Huy1")
                btn_Sua.Enabled = true;
        }
    }
}