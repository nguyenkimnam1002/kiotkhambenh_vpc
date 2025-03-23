namespace VNPT.HIS.VienPhi
{
    partial class NTU01H039_ChinhSuaGiuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NTU01H039_ChinhSuaGiuong));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnRefreshGrid = new DevExpress.XtraEditors.SimpleButton();
            this.ucGridGiuong = new VNPT.HIS.UserControl.ucGridview();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
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
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.ucGridGiuong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(834, 470);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.btnDong);
            this.panelControl2.Controls.Add(this.btnXoa);
            this.panelControl2.Location = new System.Drawing.Point(327, 441);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(180, 25);
            this.panelControl2.TabIndex = 6;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(104, 1);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnXoa.Appearance.Options.UseFont = true;
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.Location = new System.Drawing.Point(3, 1);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(95, 23);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa giường";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnRefreshGrid);
            this.panelControl1.Location = new System.Drawing.Point(4, 412);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(28, 25);
            this.panelControl1.TabIndex = 5;
            // 
            // btnRefreshGrid
            // 
            this.btnRefreshGrid.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnRefreshGrid.Appearance.Options.UseFont = true;
            this.btnRefreshGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshGrid.Image")));
            this.btnRefreshGrid.Location = new System.Drawing.Point(0, 1);
            this.btnRefreshGrid.Name = "btnRefreshGrid";
            this.btnRefreshGrid.Size = new System.Drawing.Size(23, 23);
            this.btnRefreshGrid.TabIndex = 3;
            this.btnRefreshGrid.TabStop = false;
            this.btnRefreshGrid.Click += new System.EventHandler(this.btnRefreshGrid_Click);
            // 
            // ucGridGiuong
            // 
            this.ucGridGiuong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucGridGiuong.Appearance.Options.UseFont = true;
            this.ucGridGiuong.caption = "Danh sách giường";
            this.ucGridGiuong.Location = new System.Drawing.Point(4, 4);
            this.ucGridGiuong.Name = "ucGridGiuong";
            this.ucGridGiuong.SelectedRow = null;
            this.ucGridGiuong.Size = new System.Drawing.Size(826, 404);
            this.ucGridGiuong.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
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
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(834, 470);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.ucGridGiuong;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(830, 408);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 408);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(32, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 28);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(830, 29);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl2;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 437);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(184, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(5, 28);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(830, 29);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // NTU01H039_ChinhSuaGiuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 470);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NTU01H039_ChinhSuaGiuong";
            this.ShowIcon = false;
            this.Text = "Chỉnh sửa giường";
            this.Load += new System.EventHandler(this.NTU01H039_ChinhSuaGiuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.ucGridview ucGridGiuong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnRefreshGrid;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
    }
}