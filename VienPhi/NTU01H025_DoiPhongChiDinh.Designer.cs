namespace VNPT.HIS.VienPhi
{
    partial class NTU01H025_DoiPhongChiDinh
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NTU01H025_DoiPhongChiDinh));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.cbbPhongChiDinh = new VNPT.HIS.UserControl.ucComboBox();
            this.cbbKhoaChiDinh = new VNPT.HIS.UserControl.ucComboBox();
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
            this.layoutControl1.Controls.Add(this.cbbPhongChiDinh);
            this.layoutControl1.Controls.Add(this.cbbKhoaChiDinh);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(410, 158);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnDong);
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Location = new System.Drawing.Point(125, 60);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(160, 86);
            this.panelControl1.TabIndex = 6;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(82, 3);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(3, 3);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // cbbPhongChiDinh
            // 
            this.cbbPhongChiDinh.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cbbPhongChiDinh.Appearance.Options.UseFont = true;
            this.cbbPhongChiDinh.Caption = "Phòng chỉ định";
            this.cbbPhongChiDinh.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.cbbPhongChiDinh.CaptionValidate = true;
            this.cbbPhongChiDinh.CaptionWidth = 110;
            this.cbbPhongChiDinh.Location = new System.Drawing.Point(12, 36);
            this.cbbPhongChiDinh.Name = "cbbPhongChiDinh";
            this.cbbPhongChiDinh.SelectDataRowView = null;
            this.cbbPhongChiDinh.SelectIndex = -1;
            this.cbbPhongChiDinh.SelectText = "";
            this.cbbPhongChiDinh.SelectValue = "";
            this.cbbPhongChiDinh.Size = new System.Drawing.Size(386, 20);
            this.cbbPhongChiDinh.TabIndex = 5;
            // 
            // cbbKhoaChiDinh
            // 
            this.cbbKhoaChiDinh.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cbbKhoaChiDinh.Appearance.Options.UseFont = true;
            this.cbbKhoaChiDinh.Caption = "Khoa chỉ định";
            this.cbbKhoaChiDinh.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.cbbKhoaChiDinh.CaptionValidate = true;
            this.cbbKhoaChiDinh.CaptionWidth = 110;
            this.cbbKhoaChiDinh.Location = new System.Drawing.Point(12, 12);
            this.cbbKhoaChiDinh.Name = "cbbKhoaChiDinh";
            this.cbbKhoaChiDinh.SelectDataRowView = null;
            this.cbbKhoaChiDinh.SelectIndex = -1;
            this.cbbKhoaChiDinh.SelectText = "";
            this.cbbKhoaChiDinh.SelectValue = "";
            this.cbbKhoaChiDinh.Size = new System.Drawing.Size(386, 20);
            this.cbbKhoaChiDinh.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(410, 158);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbbKhoaChiDinh;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(5, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(390, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cbbPhongChiDinh;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(390, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(164, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(390, 90);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // NTU01H025_DoiPhongChiDinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 158);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NTU01H025_DoiPhongChiDinh";
            this.ShowIcon = false;
            this.Text = "Đổi phòng chỉ định";
            this.Load += new System.EventHandler(this.NTU01H025_DoiPhongChiDinh_Load);
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

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.ucComboBox cbbKhoaChiDinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private UserControl.ucComboBox cbbPhongChiDinh;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
    }
}