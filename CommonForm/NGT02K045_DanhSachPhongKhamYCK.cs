using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using VNPT.HIS.Common;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K045_DanhSachPhongKhamYCK : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K045_DanhSachPhongKhamYCK(string idYeuCau, string title, string selectvalue)
        {
            InitializeComponent();

            this.idYeuCau = idYeuCau;
            this.title = title;
            this.selectvalue = selectvalue;
        } 
        string idYeuCau = "";
        string title = "";
        string selectvalue = "";
        
        private void NGT02K045_DanhSachPhongKhamYCK_Load(object sender, EventArgs e)
        {
            labelControl1.Text = "YÊU CẦU KHÁM: " + title; 

            int x0 = 10; int y0 = 45;
            int w = 189; int h = 100;
            int pading = 8;
            int number = 5;


            DataTable dt = RequestHTTP.getDS_PK_YEUCAUKHAM("DS.PK.YEUCAUKHAM", idYeuCau);
            // "[{\"ORG_ID\": \"371\",\"ORG_NAME\": \"TT Thuốc\",\"SLKHAM\": \"40\",\"MAMAU\": \"\",\"SLDANGKHAM\": \"0\"}

            for (int k = 0; k < dt.Rows.Count; k++)
                {
                    int i = (k) / number;
                    int j = k - number * i;

                    DevExpress.XtraEditors.SimpleButton btn = new DevExpress.XtraEditors.SimpleButton();

                    btn.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
                    btn.Appearance.Options.UseFont = true;
                    btn.Appearance.Options.UseTextOptions = true;
                    btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                    btn.LookAndFeel.UseDefaultLookAndFeel = false;
                    btn.Size = new System.Drawing.Size(w, h);
                    btn.Click += new EventHandler(btnSelect);
                    btn.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
                    btn.Appearance.BorderColor = System.Drawing.Color.Gray;

                    //btn = btn;
                    btn.Location = new System.Drawing.Point(x0 + j * (w + pading), y0 + i * (h + pading));
                    btn.Text = dt.Rows[k]["ORG_NAME"].ToString() + "\r\n(" + dt.Rows[k]["SLDANGKHAM"].ToString() + "/" + dt.Rows[k]["SLKHAM"].ToString() + ")"; // Name
                    btn.Tag = dt.Rows[k]["ORG_ID"].ToString(); // ID
                    if (dt.Rows[k]["ORG_ID"].ToString() == selectvalue)
                    {
                        btn.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
                        
                        btn.Appearance.Options.UseBackColor = true;
                        btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                        btn.LookAndFeel.UseDefaultLookAndFeel = false;
                    }
                    if (dt.Rows[k]["SLKHAM"].ToString() == dt.Rows[k]["SLDANGKHAM"].ToString()) btn.Enabled = false;

                    xtraScrollableControl1.Controls.Add(btn); 
                }
            this.AutoScroll = true;
        } 
        private void btnSelect(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton btn = (DevExpress.XtraEditors.SimpleButton)sender;
            string tenPK = btn.Tag.ToString();
            if (ReturnData != null)
            {
                this.Close();
                ReturnData(tenPK, null);
            }
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