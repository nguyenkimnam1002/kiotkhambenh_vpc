namespace VNPT.HIS.UserControl
{
    partial class ucPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPage));
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cboPage = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbTotal = new DevExpress.XtraEditors.LabelControl();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.cboHienThi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lb_TongBanGhi = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.cboPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHienThi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnBack.Appearance.Options.UseFont = true;
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBack.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnBack.Enabled = false;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBack.Location = new System.Drawing.Point(27, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(30, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnNext.Appearance.Options.UseFont = true;
            this.btnNext.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNext.Location = new System.Drawing.Point(170, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(57, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Trang";
            // 
            // cboPage
            // 
            this.cboPage.EditValue = "1";
            this.cboPage.Location = new System.Drawing.Point(91, 0);
            this.cboPage.Name = "cboPage";
            this.cboPage.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboPage.Properties.Appearance.Options.UseFont = true;
            this.cboPage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPage.Size = new System.Drawing.Size(50, 20);
            this.cboPage.TabIndex = 4;
            this.cboPage.SelectedIndexChanged += new System.EventHandler(this.cboPage_SelectedIndexChanged);
            // 
            // lbTotal
            // 
            this.lbTotal.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lbTotal.Location = new System.Drawing.Point(143, 3);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(5, 14);
            this.lbTotal.TabIndex = 5;
            this.lbTotal.Text = "/";
            // 
            // btnLast
            // 
            this.btnLast.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLast.Appearance.Options.UseFont = true;
            this.btnLast.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLast.Location = new System.Drawing.Point(197, 0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(30, 23);
            this.btnLast.TabIndex = 6;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnFirst.Appearance.Options.UseFont = true;
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFirst.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnFirst.Enabled = false;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnFirst.Location = new System.Drawing.Point(0, 0);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(30, 23);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // cboHienThi
            // 
            this.cboHienThi.EditValue = "20";
            this.cboHienThi.Location = new System.Drawing.Point(241, 0);
            this.cboHienThi.Name = "cboHienThi";
            this.cboHienThi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboHienThi.Properties.Appearance.Options.UseFont = true;
            this.cboHienThi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboHienThi.Properties.Items.AddRange(new object[] {
            "20",
            "100",
            "200",
            "300"});
            this.cboHienThi.Size = new System.Drawing.Size(55, 20);
            this.cboHienThi.TabIndex = 7;
            this.cboHienThi.SelectedIndexChanged += new System.EventHandler(this.cboHienThi_SelectedIndexChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lb_TongBanGhi);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl1.Location = new System.Drawing.Point(512, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(110, 20);
            this.panelControl1.TabIndex = 8;
            // 
            // lb_TongBanGhi
            // 
            this.lb_TongBanGhi.Dock = System.Windows.Forms.DockStyle.Right;
            this.lb_TongBanGhi.Location = new System.Drawing.Point(110, 0);
            this.lb_TongBanGhi.Name = "lb_TongBanGhi";
            this.lb_TongBanGhi.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lb_TongBanGhi.Size = new System.Drawing.Size(0, 16);
            this.lb_TongBanGhi.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.layoutControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(622, 20);
            this.panelControl2.TabIndex = 9;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl3);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(622, 20);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.lbTotal);
            this.panelControl3.Controls.Add(this.btnBack);
            this.panelControl3.Controls.Add(this.cboHienThi);
            this.panelControl3.Controls.Add(this.btnNext);
            this.panelControl3.Controls.Add(this.btnFirst);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.btnLast);
            this.panelControl3.Controls.Add(this.cboPage);
            this.panelControl3.Location = new System.Drawing.Point(162, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(297, 20);
            this.panelControl3.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(622, 20);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl3;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(297, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(622, 20);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // ucPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Name = "ucPage";
            this.Size = new System.Drawing.Size(622, 20);
            this.Load += new System.EventHandler(this.ucPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cboPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboHienThi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cboPage;
        private DevExpress.XtraEditors.LabelControl lbTotal;
        private DevExpress.XtraEditors.SimpleButton btnLast;
        private DevExpress.XtraEditors.SimpleButton btnFirst;
        private DevExpress.XtraEditors.ComboBoxEdit cboHienThi;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lb_TongBanGhi;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
