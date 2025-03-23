namespace VNPT.HIS.MainForm.HeThong
{
    partial class frmChangeService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeService));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtLinkService = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtLinkReport = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSubDomain_BenhVien = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkService.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubDomain_BenhVien.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.txtSubDomain_BenhVien);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.btnDong);
            this.groupControl1.Controls.Add(this.btnSubmit);
            this.groupControl1.Controls.Add(this.txtLinkService);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtLinkReport);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(-2, -38);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(514, 270);
            this.groupControl1.TabIndex = 0;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(265, 210);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 15;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Appearance.Options.UseFont = true;
            this.btnSubmit.Image = ((System.Drawing.Image)(resources.GetObject("btnSubmit.Image")));
            this.btnSubmit.Location = new System.Drawing.Point(173, 210);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 14;
            this.btnSubmit.Text = "Lưu";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtLinkService
            // 
            this.txtLinkService.Location = new System.Drawing.Point(78, 136);
            this.txtLinkService.Name = "txtLinkService";
            this.txtLinkService.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtLinkService.Properties.Appearance.Options.UseFont = true;
            this.txtLinkService.Properties.ReadOnly = true;
            this.txtLinkService.Size = new System.Drawing.Size(424, 20);
            this.txtLinkService.TabIndex = 2;
            this.txtLinkService.TabStop = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Location = new System.Drawing.Point(10, 165);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(62, 14);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "Link Report";
            // 
            // txtLinkReport
            // 
            this.txtLinkReport.Location = new System.Drawing.Point(78, 162);
            this.txtLinkReport.Name = "txtLinkReport";
            this.txtLinkReport.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtLinkReport.Properties.Appearance.Options.UseFont = true;
            this.txtLinkReport.Properties.ReadOnly = true;
            this.txtLinkReport.Size = new System.Drawing.Size(424, 20);
            this.txtLinkReport.TabIndex = 3;
            this.txtLinkReport.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(150, 101);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Bệnh viện:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Location = new System.Drawing.Point(132, 58);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(284, 21);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Thiết lập kết nối Server cho Bệnh viện";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl4.Location = new System.Drawing.Point(10, 139);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "Link Sevice";
            // 
            // txtSubDomain_BenhVien
            // 
            this.txtSubDomain_BenhVien.Location = new System.Drawing.Point(214, 98);
            this.txtSubDomain_BenhVien.Name = "txtSubDomain_BenhVien";
            this.txtSubDomain_BenhVien.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSubDomain_BenhVien.Properties.Appearance.Options.UseFont = true;
            this.txtSubDomain_BenhVien.Size = new System.Drawing.Size(169, 20);
            this.txtSubDomain_BenhVien.TabIndex = 1;
            this.txtSubDomain_BenhVien.EditValueChanged += new System.EventHandler(this.txtSubDomain_BenhVien_EditValueChanged);
            this.txtSubDomain_BenhVien.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubDomain_BenhVien_KeyDown);
            // 
            // frmChangeService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 217);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangeService";
            this.ShowIcon = false;
            this.Text = "Thiết lập Server";
            this.Load += new System.EventHandler(this.frmChangeService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkService.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLinkReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubDomain_BenhVien.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtLinkService;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.TextEdit txtLinkReport;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtSubDomain_BenhVien;
    }
}