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

using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K045_DanhSachPhongKhamYCK1 : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K045_DanhSachPhongKhamYCK1()
        {
            InitializeComponent();

        } 

        private void NGT02K045_DanhSachPhongKhamYCK1_Load(object sender, EventArgs e)
        {
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            refresh_data();  
        }  
        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("name");
            for (int i = 0; i < new Random().Next(20)+7; i++) //new Random().Next(11)
            table.Rows.Add(""+i, "Phòng khám tim mạch tai mũi họng");
            return table;
        }
        private void refresh_data()
        { 
            //Get datafrom sv
            // DataTable dt= new DataTable();
            DataTable dt = CreateDataTable();
           // dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K053.LCD1", "{\"SOURCE\":\"0\",\"PHONGID\":\""+Const.local_phongId+"\"}", 0);
             
            // Create table
            DataTable data = new DataTable();
            data.Columns.Add("c1");
            data.Columns.Add("c2");
            data.Columns.Add("c3");
            data.Columns.Add("c4");
            data.Columns.Add("c5");
            for (int i = 0; i < 1 + (dt.Rows.Count-1)/5; i++)
            {
                int k = i*5;
                string c1 = k < dt.Rows.Count ? dt.Rows[k]["name"].ToString() : ""; k++;
                string c2 = k < dt.Rows.Count ? dt.Rows[k]["name"].ToString() : ""; k++;
                string c3 = k < dt.Rows.Count ? dt.Rows[k]["name"].ToString() : ""; k++;
                string c4 = k < dt.Rows.Count ? dt.Rows[k]["name"].ToString() : ""; k++;
                string c5 = k < dt.Rows.Count ? dt.Rows[k]["name"].ToString() : ""; k++;

                data.Rows.Add(c1,c2,c3,c4,c5);
            }

            // set data 
            gridControl1.DataSource = data;

            DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit noteMemo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            gridControl1.RepositoryItems.Add(noteMemo);

            gridView1.Columns["c1"].ColumnEdit = noteMemo;
            gridView1.Columns["c2"].ColumnEdit = noteMemo;
            gridView1.Columns["c3"].ColumnEdit = noteMemo;
            gridView1.Columns["c4"].ColumnEdit = noteMemo;
            gridView1.Columns["c5"].ColumnEdit = noteMemo;
   
            //gridView1.Columns["STT"].AppearanceCell.ForeColor = Color.White;
            //gridView1.Columns["STT0"].AppearanceCell.BackColor = SystemColors.MenuHighlight;
            //gridView1.Columns["STT"].AppearanceCell.ForeColor = Color.Red;
       
        }  
        private void NGT02K045_DanhSachPhongKhamYCK1_Resize(object sender, EventArgs e)
        {
            
        }
         
        private void NGT02K045_DanhSachPhongKhamYCK1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string tenPK = (string)e.CellValue;
            if (ReturnData != null) ReturnData(tenPK, null);
        }
        int CellPadding = 10;
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            //Rectangle bounds = e.Bounds;
            //e.Appearance.FillRectangle(e.Cache, e.Bounds);
            //bounds.X += CellPadding;
            //bounds.Width -= 2 * CellPadding;
            //bounds.Y += CellPadding;
            //bounds.Height -= 2 * CellPadding;
            //e.Cache.DrawString(e.DisplayText, e.Appearance.GetFont(), e.Appearance.GetForeBrush(e.Cache), bounds, e.Appearance.GetStringFormat());
            //e.Handled = true;
        }

        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
} 