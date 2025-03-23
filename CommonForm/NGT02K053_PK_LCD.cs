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
    public partial class NGT02K053_PK_LCD : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        int secon = 2;
        public NGT02K053_PK_LCD()
        {
            InitializeComponent();

        }
        
        private Thread thread;
        
        public void ThreadJob()
        {
            int dem = 0;
            while (thread.IsAlive)
            {
                Thread.Sleep(1000);

                string time = DateTime.Now.ToString("HH:mm:ss");

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate()
                    {
                        if (gridView1.Columns["ORG_NAME"] != null) gridView1.Columns["ORG_NAME"].Caption = time;
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }));
                }
                else
                {
                    lock (this)
                    {
                        if (gridView1.Columns["ORG_NAME"] != null) gridView1.Columns["ORG_NAME"].Caption = time;
                        dem = (dem + 1) % secon;
                        if (dem == 0) refresh_data();
                    }
                }  
                //
            }
        }
// {"func":"ajaxCALL_SP_O","params":["NGT02K053.LCD1","{\"SOURCE\":\"0\",\"PHONGID\":\"4763\"}",0],"uuid":"8814820e-71c5-4007-8b15-ba364006e6ad"}
//"result": "[{\n\"ORG_ID\": \"4763\",\n\"ORG_NAME\": \"QUẦY 01\",\n\"STT\": \"1\",\n\"TEN\": \"SYT-Bệnh Viện Nguyễn Trãi-Khoa Khám Bệnh\",\n\"PATH\": \"../common/image/logo_902.jpg\"},{\n\"ORG_ID\": \"4783\",\n\"ORG_NAME\": \"QUẦY 02\",\n\"STT\": \"24\",\n\"TEN\": \"SYT-Bệnh Viện Nguyễn Trãi-Khoa Khám Bệnh\",\n\"PATH\": \"../common/image/logo_902.jpg\"}]",
//"out_var": "[]",
//"error_code": 0,
//"error_msg": ""}

        private void NGT02K053_PK_LCD_Load(object sender, EventArgs e)
        {
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsSelection.EnableAppearanceFocusedRow = false;
            gridView1.OptionsSelection.EnableAppearanceHideSelection = false;

            refresh_data(); 

            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            thread.Start();
        } 
        float font = 53F;
        int rows = 0;
        
        private DataTable CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ORG_NAME");
            table.Columns.Add("STT");
            for (int i = 0; i < new Random().Next(10); i++) //new Random().Next(11)
            table.Rows.Add("Quầy 01", "999");
            return table;
        }
        private void refresh_data()
        { 
            //Get datafrom sv
            DataTable dt= new DataTable();
            //dt = CreateDataTable();
            dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K053.LCD1", "{\"SOURCE\":\"0\",\"PHONGID\":\""+Const.local_phongId+"\"}", 0);

            rows = dt.Rows.Count;
            
            // Create table
            DataTable data = new DataTable();
            data.Columns.Add("ORG_NAME");
            data.Columns.Add("STT");
            data.Columns.Add("STT1");
            data.Columns.Add("STT2");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int stt = Convert.ToInt32(dt.Rows[i]["STT"]);
                int stt1 = stt + 1;
                int stt2 = stt + 2;

                data.Rows.Add(dt.Rows[i]["ORG_NAME"].ToString()
                    , Func.addZezo(stt, 4) + stt
                    , Func.addZezo(stt1, 4) + stt1
                    , Func.addZezo(stt2, 4) + stt2);
            }

            // set data 
            gridControl1.DataSource = data;

            // Set font, height,...
            gridView1.Columns["STT"].Caption = "SỐ THỨ TỰ";
            gridView1.Columns["STT1"].Caption = "SỐ KẾ TIẾP 1";
            gridView1.Columns["STT2"].Caption = "SỐ KẾ TIẾP 2";

            gridView1.Columns["ORG_NAME"].AppearanceCell.ForeColor = Color.White;
            gridView1.Columns["ORG_NAME"].AppearanceCell.BackColor = SystemColors.MenuHighlight;
            gridView1.Columns["STT"].AppearanceCell.ForeColor = Color.Red;

            gridView1.Columns["ORG_NAME"].AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold);
                       
            // re-calculate height 
            Cal(); 
            
            text_temp.Focus();
        } 
        private void Cal()
        {
            if (rows <= 0) return;
            float f= (gridControl1.Size.Height - 51 - 10) / rows;
            int i = (int)(f / 0.185);

            font = ((float)i) / 10;

            // 51 là chiều cao dòng Header với font 30F
            if (font > 53) font = 53;
            if (font < 10) font = 10; 
            
            gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", font, System.Drawing.FontStyle.Bold);

            gridView1.BestFitColumns(true);

            //Text =  " font=" + this.font + " - " + " " + gridControl1.Size.Height;
        }
         
        private void NGT02K053_PK_LCD_Resize(object sender, EventArgs e)
        {
            Cal(); // re-calculate height 
        }
         
        private void NGT02K053_PK_LCD_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
        }

    }
} 