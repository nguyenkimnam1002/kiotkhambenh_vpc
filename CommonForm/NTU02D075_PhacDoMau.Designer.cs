namespace VNPT.HIS.CommonForm
{
    partial class NTU02D075_PhacDoMau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NTU02D075_PhacDoMau));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnSearchIcd = new DevExpress.XtraEditors.SimpleButton();
            this.txtTEXT_ICD = new DevExpress.XtraEditors.TextEdit();
            this.gridChiTiet = new VNPT.HIS.UserControl.ucGridview();
            this.gridMau = new VNPT.HIS.UserControl.ucGridview();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnSelect = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTEXT_ICD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnSearchIcd);
            this.layoutControl1.Controls.Add(this.txtTEXT_ICD);
            this.layoutControl1.Controls.Add(this.gridChiTiet);
            this.layoutControl1.Controls.Add(this.gridMau);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1007, 475);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnSearchIcd
            // 
            this.btnSearchIcd.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchIcd.Appearance.Options.UseFont = true;
            this.btnSearchIcd.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchIcd.Image")));
            this.btnSearchIcd.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnSearchIcd.Location = new System.Drawing.Point(283, 12);
            this.btnSearchIcd.MaximumSize = new System.Drawing.Size(0, 20);
            this.btnSearchIcd.Name = "btnSearchIcd";
            this.btnSearchIcd.Size = new System.Drawing.Size(76, 20);
            this.btnSearchIcd.StyleController = this.layoutControl1;
            this.btnSearchIcd.TabIndex = 35;
            this.btnSearchIcd.Text = "Tìm";
            this.btnSearchIcd.Click += new System.EventHandler(this.btnSearchIcd_Click);
            // 
            // txtTEXT_ICD
            // 
            this.txtTEXT_ICD.Location = new System.Drawing.Point(33, 12);
            this.txtTEXT_ICD.Name = "txtTEXT_ICD";
            this.txtTEXT_ICD.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTEXT_ICD.Properties.Appearance.Options.UseFont = true;
            this.txtTEXT_ICD.Size = new System.Drawing.Size(246, 20);
            this.txtTEXT_ICD.StyleController = this.layoutControl1;
            this.txtTEXT_ICD.TabIndex = 34;
            // 
            // gridChiTiet
            // 
            this.gridChiTiet.caption = "";
            this.gridChiTiet.Location = new System.Drawing.Point(363, 36);
            this.gridChiTiet.Name = "gridChiTiet";
            this.gridChiTiet.SelectedRow = null;
            this.gridChiTiet.Size = new System.Drawing.Size(632, 395);
            this.gridChiTiet.TabIndex = 7;
            // 
            // gridMau
            // 
            this.gridMau.caption = "";
            this.gridMau.Location = new System.Drawing.Point(12, 36);
            this.gridMau.Name = "gridMau";
            this.gridMau.SelectedRow = null;
            this.gridMau.Size = new System.Drawing.Size(347, 395);
            this.gridMau.TabIndex = 6;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnSelect);
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Location = new System.Drawing.Point(403, 435);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(200, 28);
            this.panelControl2.TabIndex = 33;
            // 
            // btnSelect
            // 
            this.btnSelect.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelect.Appearance.Options.UseFont = true;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(3, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(94, 22);
            this.btnSelect.TabIndex = 31;
            this.btnSelect.Text = "Chọn";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(103, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 22);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1007, 475);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridMau;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(351, 399);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridChiTiet;
            this.layoutControlItem4.Location = new System.Drawing.Point(351, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(636, 399);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl2;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 423);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(204, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(987, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem19";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtTEXT_ICD;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(151, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(271, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "ICD";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(18, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSearchIcd;
            this.layoutControlItem5.Location = new System.Drawing.Point(271, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(80, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(80, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(351, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(636, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // NTU02D075_PhacDoMau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 475);
            this.Controls.Add(this.layoutControl1);
            this.Name = "NTU02D075_PhacDoMau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Phác đồ mẫu";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NTU02D075_PhacDoMau_FormClosed);
            this.Load += new System.EventHandler(this.NTU02D075_PhacDoMau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTEXT_ICD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private UserControl.ucGridview gridChiTiet;
        private UserControl.ucGridview gridMau;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnSelect;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnSearchIcd;
        private DevExpress.XtraEditors.TextEdit txtTEXT_ICD;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}