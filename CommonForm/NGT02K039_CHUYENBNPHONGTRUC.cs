using System;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;
using Newtonsoft.Json;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K039_CHUYENBNPHONGTRUC : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K039_CHUYENBNPHONGTRUC()
        {
            InitializeComponent();
        }

        private void NGT02K039_CHUYENBNPHONGTRUC_Load(object sender, EventArgs e)
        {
            InitForm();
        }
         
         
        private void InitForm()
        {
            try
            {
                ucGridBenhNhan.gridView.OptionsView.ShowViewCaption = false;
                ucGridBenhNhan.gridView.OptionsBehavior.Editable = false;
                ucGridBenhNhan.gridView.BestFitColumns(true);
                ucGridBenhNhan.setMultiSelectMode(true);
                ucGridBenhNhan.SetReLoadWhenFilter(true);
                ucGridBenhNhan.gridView.Click += GridView_Click;
                ucGridBenhNhan.onIndicator();
                ucGridBenhNhan.Set_HidePage(false);
                //ucGridBenhNhan.gridView.ColumnFilterChanged += GridView_ColumnFilterChanged;

                ucGridBenhNhan.setEvent(SetGridBenhNhan);
                SetGridBenhNhan(0, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void GridView_Click(object sender, EventArgs e)
        {
            try
            {
                if (!"DX$CheckboxSelectorColumn".Equals(ucGridBenhNhan.gridView.FocusedColumn.FieldName))
                {
                    int index = ucGridBenhNhan.gridView.FocusedRowHandle;
                    if (ucGridBenhNhan.gridView.GetSelectedRows().Any(o => o == index))
                    {
                        ucGridBenhNhan.gridView.UnselectRow(index);
                    }
                    else
                    {
                        ucGridBenhNhan.gridView.SelectRow(index);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        //private void GridView_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    GridView view = sender as GridView;
        //    if (view.ActiveEditor is TextEdit)
        //    {
        //        TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //        textEdit.Text = textEdit.Text.Trim();
        //    }
        //}

        private void SetGridBenhNhan(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int initialFlag = (int)sender;
            dt = RequestHTTP.call_ajaxCALL_SP_O("DS.CHUYENPHONGTRUC", "-1", 0);

            if (initialFlag <= 0 || dt.Rows.Count <= 0)
            {
                dt = Func.getTableEmpty(new String[] { "MABENHNHAN", "TENBENHNHAN", "NGAYSINH", "MA_BHYT", "NGAYTIEPNHAN", "ORG_NAME" });
            }

            ucGridBenhNhan.clearData();
            ucGridBenhNhan.setData(dt, dt.Rows.Count, 1);
            ucGridBenhNhan.setColumnAll(false);
            ucGridBenhNhan.setColumn("MABENHNHAN", 1, "MÃ BỆNH NHÂN", 0);
            ucGridBenhNhan.setColumn("TENBENHNHAN", 2, "TÊN BỆNH NHÂN", 0);
            ucGridBenhNhan.setColumn("NGAYSINH", 3, "NGÀY SINH", 0);
            ucGridBenhNhan.setColumn("MA_BHYT", 4, "MÃ BHYT", 0);
            ucGridBenhNhan.setColumn("NGAYTIEPNHAN", 5, "NGÀY TIẾP NHẬN", 0);
            ucGridBenhNhan.setColumn("ORG_NAME", 6, "PHÒNG KHÁM", 0);
        }

        private void NGT02K039_CHUYENBNPHONGTRUC_Shown(object sender, EventArgs e)
        {
           // btnLoad.Focus();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SetGridBenhNhan(1, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                int[] index = ucGridBenhNhan.gridView.GetSelectedRows();

                if (index.Length <= 0)
                {
                    MessageBox.Show("Hãy chọn bệnh nhân muốn chuyển từ phòng khám về phòng trực", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("Bạn chắc chắn muốn chuyển về phòng trực ?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {
                    var phongKhamIdList = new List<object>();

                    for (int i = 0; i < index.Length; i++)
                    {
                        var dataRow = (DataRowView)ucGridBenhNhan.gridView.GetRow(index[i]);

                        var obj = new
                        {
                            PHONGKHAMDANGKYID = dataRow["PHONGKHAMDANGKYID"].ToString()
                        };

                        phongKhamIdList.Add(obj);
                    }

                    string json = JsonConvert.SerializeObject(phongKhamIdList).Replace("\"", "\\\"");
                    string ret = RequestHTTP.call_ajaxCALL_SP_I("CN.CHUYENPHONGTRUC", json);

                    if ("1".Equals(ret))
                        MessageBox.Show("Chuyển phòng trực thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                    else
                        MessageBox.Show("Chuyển phòng trực không thành công", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetGridBenhNhan(1, null);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }
    }
}