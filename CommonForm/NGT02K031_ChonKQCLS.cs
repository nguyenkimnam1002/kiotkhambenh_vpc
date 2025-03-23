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
using System.Globalization;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K031_ChonKQCLS : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        private string KHAMBENHID = "";
        public NGT02K031_ChonKQCLS(string KHAMBENHID)
        {
            InitializeComponent();

            this.KHAMBENHID = KHAMBENHID;
        }

        private void NGT02K031_ChonKQCLS_Load(object sender, EventArgs e)
        {
            ucGrid_DsBN.gridView.OptionsView.ColumnAutoWidth = true;
            ucGrid_DsBN.gridView.OptionsView.ShowAutoFilterRow = false;
            ucGrid_DsBN.gridView.OptionsView.ShowViewCaption = false;
            ucGrid_DsBN.setEvent(getData_table);
            ucGrid_DsBN.setMultiSelectMode(true);


            getData_table(1, null); 
        }

        private void getData_table(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ResponsList ds = RequestHTTP.getKQCLS(page, ucGrid_DsBN.ucPage1.getNumberPerPage(), KHAMBENHID);

                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                ucGrid_DsBN.clearData();
                if (dt.Rows.Count > 0)
                {
                    ucGrid_DsBN.setData(dt, ds.total, ds.page, ds.records); 
                    //ucGrid_DsBN.setColumnAll(false);
  
                    ucGrid_DsBN.setColumn("RN", " ");
                    ucGrid_DsBN.setColumn("TENDICHVU", "TÊN XÉT NGHIỆM");
                    ucGrid_DsBN.setColumn("GIATRI_KETQUA", "KẾT QUẢ");
                    ucGrid_DsBN.setColumn("DONVI", "ĐƠN VỊ");
                    ucGrid_DsBN.setColumn("GIATRINHONHAT", "GH DƯỚI");
                    ucGrid_DsBN.setColumn("GIATRILONNHAT", "GH TRÊN");
                    ucGrid_DsBN.setColumn("GHICHU", "GHI CHÚ CD");
                    ucGrid_DsBN.gridView.BestFitColumns(true);
                }
            }
        }
        

        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (ucGrid_DsBN.gridView.GetSelectedRows().Length <= 0)
            {
                MessageBox.Show("Hãy chọn KQCLS.");
                return;
            }

            string KQ = "";
            for (int i = 0; i < ucGrid_DsBN.gridView.GetSelectedRows().Length; i++)
            {
                DataRowView dr = (DataRowView)ucGrid_DsBN.gridView.GetRow(ucGrid_DsBN.gridView.GetSelectedRows()[i]);
                KQ += dr["TENDICHVU"].ToString() + "; ";
            }
            
            if (ReturnData != null) ReturnData(KQ, null);
            this.Close();
        }
        

    }
}