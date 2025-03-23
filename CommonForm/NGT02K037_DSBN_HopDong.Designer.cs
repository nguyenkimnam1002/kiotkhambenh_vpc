namespace VNPT.HIS.CommonForm
{
    partial class NGT02K037_DSBN_HopDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K037_DSBN_HopDong));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnThemMoi = new DevExpress.XtraEditors.SimpleButton();
            this.btnChon = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ucGrid_DsBN = new VNPT.HIS.UserControl.ucGridview();
            this.ucHopDong = new VNPT.HIS.UserControl.ucComboBox();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucHopDong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1108, 44);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1108, 44);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.panelControl1);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControl2.Location = new System.Drawing.Point(0, 528);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(1108, 49);
            this.layoutControl2.TabIndex = 1;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnDong);
            this.panelControl1.Controls.Add(this.btnThemMoi);
            this.panelControl1.Controls.Add(this.btnChon);
            this.panelControl1.Location = new System.Drawing.Point(391, 7);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(326, 35);
            this.panelControl1.TabIndex = 4;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(224, 9);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 23);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemMoi.Appearance.Options.UseFont = true;
            this.btnThemMoi.Image = ((System.Drawing.Image)(resources.GetObject("btnThemMoi.Image")));
            this.btnThemMoi.Location = new System.Drawing.Point(108, 9);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(110, 23);
            this.btnThemMoi.TabIndex = 3;
            this.btnThemMoi.Text = "Thêm mới BN";
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // btnChon
            // 
            this.btnChon.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnChon.Appearance.Options.UseFont = true;
            this.btnChon.Image = ((System.Drawing.Image)(resources.GetObject("btnChon.Image")));
            this.btnChon.Location = new System.Drawing.Point(2, 9);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(100, 23);
            this.btnChon.TabIndex = 2;
            this.btnChon.Text = "Chọn BN";
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1108, 49);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(330, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1098, 39);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucGrid_DsBN
            // 
            this.ucGrid_DsBN.caption = "";
            this.ucGrid_DsBN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGrid_DsBN.Location = new System.Drawing.Point(0, 44);
            this.ucGrid_DsBN.Name = "ucGrid_DsBN";
            this.ucGrid_DsBN.SelectedRow = null;
            this.ucGrid_DsBN.Size = new System.Drawing.Size(1108, 484);
            this.ucGrid_DsBN.TabIndex = 2;
            // 
            // ucHopDong
            // 
            this.ucHopDong.Caption = "Hợp đồng";
            this.ucHopDong.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucHopDong.CaptionValidate = true;
            this.ucHopDong.CaptionWidth = 90;
            this.ucHopDong.Location = new System.Drawing.Point(304, 12);
            this.ucHopDong.Name = "ucHopDong";
            this.ucHopDong.SelectDataRowView = null;
            this.ucHopDong.SelectIndex = -1;
            this.ucHopDong.SelectText = "";
            this.ucHopDong.SelectValue = "";
            this.ucHopDong.Size = new System.Drawing.Size(500, 20);
            this.ucHopDong.TabIndex = 1;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucHopDong;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(504, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1088, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // NGT02K037_DSBN_HopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 577);
            this.Controls.Add(this.ucGrid_DsBN);
            this.Controls.Add(this.layoutControl2);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K037_DSBN_HopDong";
            this.ShowIcon = false;
            this.Text = "Thông tin hợp đồng";
            this.Load += new System.EventHandler(this.NGT02K037_DSBN_HopDong_Load);
            this.Click += new System.EventHandler(this.btnChon_Click);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.ucComboBox ucHopDong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnThemMoi;
        private DevExpress.XtraEditors.SimpleButton btnChon;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private UserControl.ucGridview ucGrid_DsBN;
    }
}