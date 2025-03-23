namespace VNPT.HIS.CommonForm
{
    partial class NGT02K018_DonThuocMau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K018_DonThuocMau));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.ucGrid_DsMau = new VNPT.HIS.UserControl.ucGridview();
            this.ucGrid_DsThuoc = new VNPT.HIS.UserControl.ucGridview();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnChon = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.ucGrid_DsMau);
            this.layoutControl1.Controls.Add(this.ucGrid_DsThuoc);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1170, 598);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnChon);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Location = new System.Drawing.Point(499, 567);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(171, 29);
            this.panelControl1.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(75, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 22);
            this.btnClose.TabIndex = 31;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucGrid_DsMau
            // 
            this.ucGrid_DsMau.caption = "";
            this.ucGrid_DsMau.Location = new System.Drawing.Point(2, 2);
            this.ucGrid_DsMau.Name = "ucGrid_DsMau";
            this.ucGrid_DsMau.SelectedRow = null;
            this.ucGrid_DsMau.Size = new System.Drawing.Size(466, 561);
            this.ucGrid_DsMau.TabIndex = 0;
            this.ucGrid_DsMau.Load += new System.EventHandler(this.NGT02K018_DonThuocMau_Load);
            // 
            // ucGrid_DsThuoc
            // 
            this.ucGrid_DsThuoc.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucGrid_DsThuoc.Appearance.Options.UseFont = true;
            this.ucGrid_DsThuoc.caption = "";
            this.ucGrid_DsThuoc.Location = new System.Drawing.Point(472, 2);
            this.ucGrid_DsThuoc.Name = "ucGrid_DsThuoc";
            this.ucGrid_DsThuoc.SelectedRow = null;
            this.ucGrid_DsThuoc.Size = new System.Drawing.Size(696, 561);
            this.ucGrid_DsThuoc.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1170, 598);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 565);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(104, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1170, 33);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucGrid_DsMau;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(470, 565);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucGrid_DsThuoc;
            this.layoutControlItem2.Location = new System.Drawing.Point(470, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(700, 565);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 565);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(175, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1170, 33);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            //
            // btnChon
            // 
            this.btnChon.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnChon.Appearance.Options.UseFont = true;
            this.btnChon.Image = ((System.Drawing.Image)(resources.GetObject("btnChon.Image")));
            this.btnChon.Location = new System.Drawing.Point(5, 4);
            this.btnChon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(64, 22);
            this.btnChon.TabIndex = 54;
            this.btnChon.Text = "Chọn";
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // NGT02K018_DonThuocMau
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 598);
            this.Controls.Add(this.layoutControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K018_DonThuocMau";
            this.ShowIcon = false;
            this.Text = "Đơn thuốc mẫu/vật tư";
            this.Load += new System.EventHandler(this.NGT02K018_DonThuocMau_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucGridview ucGrid_DsMau;
        private UserControl.ucGridview ucGrid_DsThuoc;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnChon;
    }
}