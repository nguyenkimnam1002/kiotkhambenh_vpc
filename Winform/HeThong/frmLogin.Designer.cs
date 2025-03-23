namespace VNPT.HIS.MainForm.HeThong
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.txtPass = new DevExpress.XtraEditors.TextEdit();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Appearance.Options.UseFont = true;
            this.btnSubmit.Image = ((System.Drawing.Image)(resources.GetObject("btnSubmit.Image")));
            this.btnSubmit.Location = new System.Drawing.Point(121, 118);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(99, 23);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Đăng nhập";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(215, 30);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtUser.Properties.Appearance.Options.UseFont = true;
            this.txtUser.Size = new System.Drawing.Size(160, 20);
            this.txtUser.TabIndex = 10;
            this.txtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUser_KeyDown);
            // 
            // txtPass
            // 
            this.txtPass.EditValue = "Test@2018";
            this.txtPass.Location = new System.Drawing.Point(215, 70);
            this.txtPass.Name = "txtPass";
            this.txtPass.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtPass.Properties.Appearance.Options.UseFont = true;
            this.txtPass.Properties.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(160, 20);
            this.txtPass.TabIndex = 11;
            this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyDown);
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(237, 118);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(93, 23);
            this.btnDong.TabIndex = 12;
            this.btnDong.Text = "Hủy bỏ";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 160);
            this.label1.TabIndex = 13;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Location = new System.Drawing.Point(121, 72);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(54, 14);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Mật khẩu:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl4.Location = new System.Drawing.Point(121, 32);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(88, 14);
            this.labelControl4.TabIndex = 14;
            this.labelControl4.Text = "Tên đăng nhập:";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 160);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnSubmit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.Text = "Đăng nhập";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPass.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.TextEdit txtPass;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;




    }
}