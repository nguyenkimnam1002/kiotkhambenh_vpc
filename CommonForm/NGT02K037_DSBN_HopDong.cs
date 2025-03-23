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
    public partial class NGT02K037_DSBN_HopDong : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K037_DSBN_HopDong()
        {
            InitializeComponent();
        }
         
        private void NGT02K037_DSBN_HopDong_Load(object sender, EventArgs e)
        { 
            DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DSHopDong);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[1] = "-- Chọn hợp đồng --";
                dr[2] = "";
                dt.Rows.InsertAt(dr, 0);
            }

            ucHopDong.setData(dt, 0, 1);
            ucHopDong.setColumn(0, false);
            ucHopDong.SelectIndex = 0;
            ucHopDong.setEvent(HopDong_Change);

            ucGrid_DsBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGrid_DsBN.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            //ucGrid_DsBN.setEvent_FocusedRowChanged(DsBN_Change_SelectRow);
            ucGrid_DsBN.setEvent_DoubleClick(DoubleClick);
            ucGrid_DsBN.gridView.OptionsBehavior.Editable = false; // chế độ ko sửa mới cho Double click, readonly thì không
            //ucGrid_DsBN.gridView.OptionsView.ColumnAutoWidth = true;
            //ucGrid_DsBN.Set_HidePage(false);

            ucGrid_DsBN.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
            ucGrid_DsBN.onIndicator();

            ucGrid_DsBN.SetReLoadWhenFilter(true);

            ucGrid_DsBN.setEvent(getData_table);
            
        }
        private void HopDong_Change(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)sender;
            if (dr != null && dr[0].ToString() != "")
            {
                string HopDongID = ucHopDong.SelectValue;
                dt = RequestHTTP.get_ajaxExecuteQueryO("DS.BN.HOPDONG", HopDongID);
            }
            else
                dt = new DataTable();

            getData_table(1, null);
        }
        DataTable dt = new DataTable();
        private void getData_table(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                // Lấy ds tất cả dl
                int page = (int)sender;
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "MABENHNHAN", "TENBENHNHAN", "GIOITINH", "NGAYSINH", "DIACHI" });
                //if (dt.Rows.Count == 0)
                //{
                //    string HopDongID = ucHopDong.SelectValue;
                //    dt = RequestHTTP.get_ajaxExecuteQueryO("DS.BN.HOPDONG", HopDongID);

                //    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "MABENHNHAN", "TENBENHNHAN", "GIOITINH", "NGAYSINH", "DIACHI" });
                //} 


                // lọc lại dl có filter --> view
                DataTable view = dt.Copy();
                DevExpress.XtraGrid.Views.Base.ViewFilter colFilter = ucGrid_DsBN.gridView.ActiveFilter;
                for (int i = 0; i < colFilter.Count; i++)
                {
                    string name = colFilter[i].Column.FieldName;
                    string value = colFilter[i].Column.FilterInfo.Value.ToString();

                    if (!string.IsNullOrEmpty(value))
                    {
                        DataTable view_temp = view.Clone();
                        view.AsEnumerable().Where(row => row.Field<string>(name).ToLower().Contains(value.ToLower())).ToList().ForEach(row => view_temp.ImportRow(row));
                        view = view_temp.Copy();
                    }
                }


                // lọc ra 1 trang dl --> show
                int number = ucGrid_DsBN.getNumberPerPage();
                DataTable show = view.Clone();
                int to_row = page * number;
                if (to_row > view.Rows.Count) to_row = view.Rows.Count;

                for (int i = (page - 1) * number; i < to_row; i++)
                {
                    show.ImportRow(view.Rows[i]);
                }// dt.AsEnumerable().Where(row => row.).ToList().ForEach(row => dt_view.ImportRow(row));


                // Hiển thị ds show ra.
                int total_page = (view.Rows.Count - 1) / number + 1;

                ucGrid_DsBN.clearData();
                ucGrid_DsBN.setData(show, total_page, page, view.Rows.Count);
                ucGrid_DsBN.setColumnAll(false);
                 
                ucGrid_DsBN.setColumn("MABENHNHAN","Mã bệnh nhân");
                ucGrid_DsBN.setColumn("TENBENHNHAN","Tên bệnh nhân");
                ucGrid_DsBN.setColumn("GIOITINH","Giới tính");
                ucGrid_DsBN.setColumn("NGAYSINH","Ngày sinh");
                ucGrid_DsBN.setColumn("DIACHI","Địa chỉ");
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        protected EventHandler event_Return;
        public void setEvent_Return(EventHandler eventReturn)
        {
            event_Return = eventReturn;
        }



        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            this.Close();
            if (event_Return != null) event_Return(ucHopDong.SelectValue, null);
        }
        private void DoubleClick(object sender, EventArgs e)
        {
            btnChon_Click(null, null);
        }
        private void btnChon_Click(object sender, EventArgs e)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataRowView drv = ucGrid_DsBN.SelectedRow;
                if (event_Return != null)
                {
                    if (drv == null)
                        MessageBox.Show("Yêu cầu chọn bệnh nhân để tiếp tục.");
                    else
                    {
                        event_Return(ucHopDong.SelectValue + "," + drv["MABENHNHAN"].ToString()
                            + "," + drv["TENBENHNHAN"].ToString() + "," + drv["NGAYSINH"].ToString() + ","
                            + (drv["GIOITINH"].ToString().ToUpper() == "NAM" ? "1" : "2"), null);

                        this.Close();
                    }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }


    }
}