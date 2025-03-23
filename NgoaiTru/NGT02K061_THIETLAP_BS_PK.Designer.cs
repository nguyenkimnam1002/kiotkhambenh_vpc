namespace VNPT.HIS.NgoaiTru
{
    partial class NGT02K061_THIETLAP_BS_PK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K061_THIETLAP_BS_PK));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.ucGridDanhSachPhongKham = new VNPT.HIS.UserControl.ucGridview();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lctrliDSPK = new DevExpress.XtraLayout.LayoutControlItem();
            this.lctrliChucNang = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lctrliDSPK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lctrliChucNang)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.ucGridDanhSachPhongKham);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(608, 481);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnDong);
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Location = new System.Drawing.Point(223, 433);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(162, 46);
            this.panelControl1.TabIndex = 5;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(84, 13);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 22);
            this.btnDong.TabIndex = 9;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(3, 13);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 22);
            this.btnLuu.TabIndex = 8;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // ucGridDanhSachPhongKham
            // 
            this.ucGridDanhSachPhongKham.caption = "DANH SÁCH PHÒNG KHÁM";
            this.ucGridDanhSachPhongKham.Location = new System.Drawing.Point(0, 0);
            this.ucGridDanhSachPhongKham.Name = "ucGridDanhSachPhongKham";
            this.ucGridDanhSachPhongKham.SelectedRow = null;
            this.ucGridDanhSachPhongKham.Size = new System.Drawing.Size(608, 431);
            this.ucGridDanhSachPhongKham.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lctrliDSPK,
            this.lctrliChucNang});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(608, 481);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lctrliDSPK
            // 
            this.lctrliDSPK.Control = this.ucGridDanhSachPhongKham;
            this.lctrliDSPK.Location = new System.Drawing.Point(0, 0);
            this.lctrliDSPK.Name = "lctrliDSPK";
            this.lctrliDSPK.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lctrliDSPK.Size = new System.Drawing.Size(608, 431);
            this.lctrliDSPK.TextSize = new System.Drawing.Size(0, 0);
            this.lctrliDSPK.TextVisible = false;
            // 
            // lctrliChucNang
            // 
            this.lctrliChucNang.Control = this.panelControl1;
            this.lctrliChucNang.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.lctrliChucNang.Location = new System.Drawing.Point(0, 431);
            this.lctrliChucNang.MaxSize = new System.Drawing.Size(166, 0);
            this.lctrliChucNang.MinSize = new System.Drawing.Size(5, 5);
            this.lctrliChucNang.Name = "lctrliChucNang";
            this.lctrliChucNang.Size = new System.Drawing.Size(608, 50);
            this.lctrliChucNang.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lctrliChucNang.TextSize = new System.Drawing.Size(0, 0);
            this.lctrliChucNang.TextVisible = false;
            // 
            // NGT02K061_THIETLAP_BS_PK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 481);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NGT02K061_THIETLAP_BS_PK";
            this.ShowIcon = false;
            this.Text = "Thiết lập BS - PK";
            this.Load += new System.EventHandler(this.NGT02K061_THIETLAP_BS_PK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lctrliDSPK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lctrliChucNang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private UserControl.ucGridview ucGridDanhSachPhongKham;
        private DevExpress.XtraLayout.LayoutControlItem lctrliDSPK;
        private DevExpress.XtraLayout.LayoutControlItem lctrliChucNang;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
    }
}