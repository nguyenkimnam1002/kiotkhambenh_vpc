using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using VNPT.HIS.Controls;
using VNPT.HIS.Common;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.CommonForm
{
    public partial class CapSo : DevExpress.XtraEditors.XtraForm
    {
        public CapSo()
        {
            InitializeComponent();
        }
        string lbPara = "#";
        private string getPara(string name)
        {//  &kbtraingay=1&x=2&
            string ret = "";
            try
            {
                if (lbPara == "#")
                {
                    Control[] control = this.Controls.Find("lbPara", false);
                    if (control.Length > 0)
                    {
                        if (control[0].GetType().ToString() == "DevExpress.XtraEditors.LabelControl")
                        {
                            DevExpress.XtraEditors.LabelControl lbControl = (DevExpress.XtraEditors.LabelControl)control[0];
                            lbPara = "&" + lbControl.Text + "&";
                        }
                    }
                }

                if (lbPara.IndexOf("&" + name) > -1)
                {
                    string temp = lbPara.Substring(lbPara.IndexOf("&" + name) + ("&" + name).Length + 1);
                    if (temp.IndexOf("&") > -1) ret = temp.Substring(0, temp.IndexOf("&"));
                }
            }
            catch (Exception ex) {  }

            return ret;
        }

        private void CapSo_Load(object sender, EventArgs e)
        { 
            Load_Data(true);


            //Func.printing3("C:\\temphis2018\\abc.pdf");
            //Func.Print_From_File("C:\\temphis2018\\abc.pdf");

        }
        DataTable dt = new DataTable();
        string he = "";
        private void Load_Data(bool re_call_data)
            { 
            he = getPara("he");
            if (he == "1") this.Text = "Cấp số khám bệnh";
            else if (he == "2") this.Text = "Cấp số viện phí";
            else if (he == "3") this.Text = "Cấp số xét nghiệm";
            else if (he == "4") this.Text = "Cấp số CĐHA";
            else if (he == "5") this.Text = "Cấp số phát thuốc";

            //int pading = 50;
            //int x0 = 50; int y0 = 50;
            //int w = 189; int h = 150;
            //int number = 2;

            int number = 2;
            int pading = 50;
            int x0 = pading; int y0 = pading;
            int w = (Width-(number+1) * pading)/2; int h = w*70/100;

            // {"result": "[{\"ID\": \"1\",\"TEN_HIENTHI\": \"BHYT\",\"STT\": \"11\"}
            // ,{\"ID\": \"2\",\"TEN_HIENTHI\": \"VP\",\"STT\": \"20\"}
            //        ,{\"ID\": \"3\",\"TEN_HIENTHI\": \"Uu tien\",\"STT\": \"30\"}]","out_var": "[]","error_code": 0,"error_msg": ""}
            if (re_call_data)
                dt = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYDL", he, 0);

            xtraScrollableControl1.Controls.Clear();
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                int i = (k) / number;
                int j = k - number * i;

                DevExpress.XtraEditors.SimpleButton btn = new DevExpress.XtraEditors.SimpleButton();

                btn.Appearance.Font = new System.Drawing.Font("Tahoma", 28F);
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
                btn.Text = dt.Rows[k]["TEN_HIENTHI"].ToString() + "\r\n(" + dt.Rows[k]["STT"].ToString() + ")"; // Name
                btn.Tag = dt.Rows[k]["ID"].ToString(); // ID
                //if (dt.Rows[k]["ORG_ID"].ToString() == selectvalue)
                //{
                //    btn.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));

                //    btn.Appearance.Options.UseBackColor = true;
                //    btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                //    btn.LookAndFeel.UseDefaultLookAndFeel = false;
                //}
                //if (dt.Rows[k]["SLKHAM"].ToString() == dt.Rows[k]["SLDANGKHAM"].ToString()) btn.Enabled = false;

                xtraScrollableControl1.Controls.Add(btn);
            }
            this.AutoScroll = true;
        }
        private void btnSelect(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.SimpleButton btn = (DevExpress.XtraEditors.SimpleButton)sender;
            string id = btn.Tag.ToString();

            DataTable dt_temp = RequestHTTP.call_ajaxCALL_SP_O("KBH.CAPSOLAYCT", id, 0);
            
            Load_Data(true);

            if (dt_temp.Rows.Count > 0 && dt_temp.Columns.Contains("STT"))
            {
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("TENPHIEU", "String", "STT Khám Bệnh - ");
                table.Rows.Add("SOTHUTU", "String", dt_temp.Rows[0]["STT"].ToString());

                // "pdf" --> "pdf"
                // "doc" --> "rtf"  
                //if (he == "1")
                    Func.Print_Luon(table, "NGT_CAP_SOTHUTU", "80mm");// 
                //else
                //    Func.Print_Luon_temp_C(table, "NGT_CAP_SOTHUTU", Const.FileInPhieu);
                //{
                //    //Func.Print_Luon(table, "NGT_CAP_SOTHUTU", Const.FileInPhieu);
                //    Controls.SubForm.frmPrint frm = new Controls.SubForm.frmPrint(table, "NGT_CAP_SOTHUTU");
                //    openForm(frm);
                //}
            }
        }

        private void CapSo_SizeChanged(object sender, EventArgs e)
        {
            Load_Data(false);
        }

        private void openForm(Form frm, string optionsPopup = "1")
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }
 
    }
}