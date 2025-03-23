namespace VNPT.HIS.Duoc.subform
{
    partial class DUC51S001_DSDonThuoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DUC51S001_DSDonThuoc));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.grvDonThuocCT = new VNPT.HIS.UserControl.ucGridview();
            this.grvDonThuoc = new VNPT.HIS.UserControl.ucGridview();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.grvThuocCT = new VNPT.HIS.UserControl.ucGridview();
            this.grvThuoc = new VNPT.HIS.UserControl.ucGridview();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(870, 417);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.grvDonThuocCT);
            this.xtraTabPage1.Controls.Add(this.grvDonThuoc);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(864, 389);
            this.xtraTabPage1.Text = "Thông tin đơn thuốc/VT";
            // 
            // grvDonThuocCT
            // 
            this.grvDonThuocCT.caption = "Chi tiết thuốc/VT theo đơn";
            this.grvDonThuocCT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvDonThuocCT.Location = new System.Drawing.Point(0, 199);
            this.grvDonThuocCT.Name = "grvDonThuocCT";
            this.grvDonThuocCT.SelectedRow = null;
            this.grvDonThuocCT.Size = new System.Drawing.Size(864, 190);
            this.grvDonThuocCT.TabIndex = 1;
            // 
            // grvDonThuoc
            // 
            this.grvDonThuoc.caption = "Danh sách đơn thuốc/VT";
            this.grvDonThuoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.grvDonThuoc.Location = new System.Drawing.Point(0, 0);
            this.grvDonThuoc.Name = "grvDonThuoc";
            this.grvDonThuoc.SelectedRow = null;
            this.grvDonThuoc.Size = new System.Drawing.Size(864, 199);
            this.grvDonThuoc.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.grvThuocCT);
            this.xtraTabPage2.Controls.Add(this.grvThuoc);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(864, 389);
            this.xtraTabPage2.Text = "Thông tin thuốc/VT";
            // 
            // grvThuocCT
            // 
            this.grvThuocCT.caption = "Danh sách đơn theo thuốc/VT";
            this.grvThuocCT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvThuocCT.Location = new System.Drawing.Point(0, 195);
            this.grvThuocCT.Name = "grvThuocCT";
            this.grvThuocCT.SelectedRow = null;
            this.grvThuocCT.Size = new System.Drawing.Size(864, 194);
            this.grvThuocCT.TabIndex = 1;
            // 
            // grvThuoc
            // 
            this.grvThuoc.caption = "Danh sách thuốc/VT";
            this.grvThuoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.grvThuoc.Location = new System.Drawing.Point(0, 0);
            this.grvThuoc.Name = "grvThuoc";
            this.grvThuoc.SelectedRow = null;
            this.grvThuoc.Size = new System.Drawing.Size(864, 195);
            this.grvThuoc.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnDong);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 417);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(870, 46);
            this.panelControl1.TabIndex = 1;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(388, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(78, 23);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // DUC51S001_DSDonThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 463);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DUC51S001_DSDonThuoc";
            this.ShowIcon = false;
            this.Text = "Thông tin đơn thuốc/VT";
            this.Load += new System.EventHandler(this.DUC51S001_DSDonThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private UserControl.ucGridview grvDonThuocCT;
        private UserControl.ucGridview grvDonThuoc;
        private UserControl.ucGridview grvThuocCT;
        private UserControl.ucGridview grvThuoc;
        private DevExpress.XtraEditors.SimpleButton btnDong;
    }
}