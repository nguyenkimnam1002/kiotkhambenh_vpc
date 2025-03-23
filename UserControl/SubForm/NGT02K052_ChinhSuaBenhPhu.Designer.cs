namespace VNPT.HIS.UserControl.SubForm
{
    partial class NGT02K052_ChinhSuaBenhPhu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K052_ChinhSuaBenhPhu));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.ucDSBenh = new VNPT.HIS.UserControl.ucGridview();
            this.ucSearchLookup1 = new VNPT.HIS.UserControl.ucSearchLookup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.ucDSBenh);
            this.layoutControl1.Controls.Add(this.ucSearchLookup1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(584, 361);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Location = new System.Drawing.Point(207, 323);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(170, 36);
            this.panelControl1.TabIndex = 5;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(88, 8);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "Đóng";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(6, 8);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Lưu";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // ucDSBenh
            // 
            this.ucDSBenh.caption = "Danh sách bệnh phụ";
            this.ucDSBenh.Location = new System.Drawing.Point(2, 37);
            this.ucDSBenh.Name = "ucDSBenh";
            this.ucDSBenh.SelectedRow = null;
            this.ucDSBenh.Size = new System.Drawing.Size(580, 282);
            this.ucDSBenh.TabIndex = 4;
            // 
            // ucSearchLookup1
            // 
            this.ucSearchLookup1.Caption = "";
            this.ucSearchLookup1.CaptionDock = System.Windows.Forms.DockStyle.Top;
            this.ucSearchLookup1.CaptionValidate = false;
            this.ucSearchLookup1.CaptionWidth = 0;
            this.ucSearchLookup1.findColumns = "*";
            this.ucSearchLookup1.Location = new System.Drawing.Point(110, 12);
            this.ucSearchLookup1.Name = "ucSearchLookup1";
            this.ucSearchLookup1.SelectDataRowView = null;
            this.ucSearchLookup1.SelectIndex = -1;
            this.ucSearchLookup1.SelectText = "";
            this.ucSearchLookup1.SelectValue = "";
            this.ucSearchLookup1.Size = new System.Drawing.Size(472, 21);
            this.ucSearchLookup1.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(584, 361);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucSearchLookup1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 2, 2, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(584, 25);
            this.layoutControlItem1.Text = "Mã bệnh phụ";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(85, 14);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucDSBenh;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(584, 286);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 311);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(174, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(584, 40);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // NGT02K052_ChinhSuaBenhPhu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K052_ChinhSuaBenhPhu";
            this.ShowIcon = false;
            this.Text = "Chỉnh sửa bệnh phụ";
            this.Load += new System.EventHandler(this.NGT02K052_ChinhSuaBenhPhu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucSearchLookup ucSearchLookup1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private UserControl.ucGridview ucDSBenh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}