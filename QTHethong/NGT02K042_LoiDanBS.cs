using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.QTHethong.Class;
using VNPT.HIS.Common;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;

namespace VNPT.HIS.QTHethong
{
    public partial class NGT02K042_LoiDanBS : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
              log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ResponsList ds = new ResponsList();
        DataTable dt = new DataTable();
        
        private bool isEdit = false;
        private string selectedID;

        public NGT02K042_LoiDanBS()
        {
            InitializeComponent();
            ememo_LoiDan.ReadOnly = true;
        }

        private void ResetForm(string Action)
        {
            switch (Action)
            {
                case "Load":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = true;
                    btn_Xoa.Enabled = true;

                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;
                    ememo_LoiDan.ReadOnly = true;
                    break;
                case "Insert":
                    isEdit = false;
                    btn_Them.Enabled = false;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;

                    btn_Luu.Enabled = true;
                    btn_Huy.Enabled = true;
                    ememo_LoiDan.ReadOnly = false;
                    break;
                case "Update":
                    isEdit = true;
                    btn_Them.Enabled = false;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;

                    btn_Luu.Enabled = true;
                    btn_Huy.Enabled = true;
                    ememo_LoiDan.ReadOnly = false;
                    break;
                case "Cancle":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = true;
                    btn_Xoa.Enabled = true;

                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;
                    ememo_LoiDan.ReadOnly = true;
                    break;
                case "Save":
                    btn_Them.Enabled = true;
                    btn_Sua.Enabled = false;
                    btn_Xoa.Enabled = false;

                    btn_Luu.Enabled = false;
                    btn_Huy.Enabled = false;
                    ememo_LoiDan.ReadOnly = true;
                    break;
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            selectedID = "";
            ememo_LoiDan.Text = "";
            ResetForm("Insert");
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            ResetForm("Update");
        }

        private void btn_Xoa_Click(object sender, EventArgs e) //Chưa hoàn thành
        {
            try
            {
                DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn xóa chi tiết lời dặn này?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string json = "";
                    json += Func.json_item("ID", selectedID);
                    json += Func.json_item("LOIDANBS", ememo_LoiDan.Text);
                    json = Func.json_item_end(json);
                    json = json.Replace("\"", "\\\"");

                    string ret = RequestHTTP.call_ajaxCALL_SP_I("DMC54.XOA", json);
                    if (ret == "1")
                    {
                        getData_table(1, null);
                        selectedID = "";
                        ememo_LoiDan.Text = "";
                        ResetForm("Save");
                        MessageBox.Show("Xóa thành công");
                    }
                    else MessageBox.Show("Xóa không thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            try
            {
                string loiDan = ememo_LoiDan.Text;
                while (loiDan.IndexOf("  ") >= 0)
                    loiDan = loiDan.Replace("  ", " ");
                if (loiDan != "" && loiDan != " ")
                {
                    if (isEdit)
                    {
                        string json = "";
                        json += Func.json_item("ID", selectedID);
                        json += Func.json_item("LOIDANBS", ememo_LoiDan.Text);
                        json = Func.json_item_end(json);
                        json = json.Replace("\"", "\\\"");

                        string ret = RequestHTTP.call_ajaxCALL_SP_I("DMC54.SUA", json);
                        if (ret == "1")
                        {
                            getData_table(1, null);
                            ResetForm("Save");
                            MessageBox.Show("Cập nhật thành công");
                        }
                        else MessageBox.Show("Cập nhật không thành công");
                    }
                    else
                    {
                        string json = "";
                        json += Func.json_item("ID", "");
                        json += Func.json_item("LOIDANBS", ememo_LoiDan.Text);
                        json = Func.json_item_end(json);
                        json = json.Replace("\"", "\\\"");

                        string ret = RequestHTTP.call_ajaxCALL_SP_I("DMC54.THEM", json);
                        if (ret == "1")
                        {
                            getData_table(1, null);
                            ResetForm("Save");
                            MessageBox.Show("Thêm thành công");
                        }
                        else MessageBox.Show("Thêm không thành công");
                    }
                }
                else
                {
                    MessageBox.Show("Lời dặn không được để trống.");
                    return;
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            ResetForm("Cancle");
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gviewDanhSachLoiDan_Load(object sender, EventArgs e)
        {
            ucgview_DSLoiDan.gridView.OptionsView.ColumnAutoWidth = true;
            ucgview_DSLoiDan.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucgview_DSLoiDan.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucgview_DSLoiDan.setEvent(getData_table);
            ucgview_DSLoiDan.setEvent_FocusedRowChanged(change_selectRow);
            ucgview_DSLoiDan.gridView.OptionsBehavior.ReadOnly = true;
            ucgview_DSLoiDan.SetReLoadWhenFilter(true);
            getData_table(1, null);
        }
        
        private void getData_table(object sender, EventArgs e)
        {
            try
            {
                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    //// Lấy điều kiện filter
                    //if (ucgview_DSLoiDan.ReLoadWhenFilter)
                    //{
                    //    if (ucgview_DSLoiDan.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSLoiDan.tableFlterColumn);
                    //}

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("DMC54.LAYDS", 1, 100000, new string[] { }, new string[] { }, "");
                    dt = MyJsonConvert.toDataTable(ds.rows);

                    ucgview_DSLoiDan.clearData();
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "ID", "LOIDANBS", "NGUOITAO", "NGAYTAO" });
                    {
                        ucgview_DSLoiDan.setData(dt, ds.total, ds.page, ds.records);
                        ucgview_DSLoiDan.setColumnAll(false);

                        ucgview_DSLoiDan.setColumn("LOIDANBS", 0, "Chi tiết lời dặn");
                        ucgview_DSLoiDan.setColumn("NGAYTAO", 1, "Ngày tạo");

                        ucgview_DSLoiDan.gridView.BestFitColumns(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void change_selectRow(object sender, EventArgs e)
        {
            DataRow row = (ucgview_DSLoiDan.gridView.GetRow(ucgview_DSLoiDan.gridView.FocusedRowHandle) as DataRowView).Row;
            if (row == null) return;

            selectedID = row["ID"].ToString();
            ememo_LoiDan.Text = row["LOIDANBS"].ToString();

            ResetForm("Load");
        }
    }
}