namespace VNPT.HIS.MainForm.HeThong
{
    partial class frmChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePassword));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.txtNewPassAgain = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPass = new DevExpress.XtraEditors.TextEdit();
            this.txtOldPass = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassAgain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPass.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupControl1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(470, 206);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl1.Controls.Add(this.btnDong);
            this.groupControl1.Controls.Add(this.btnSubmit);
            this.groupControl1.Controls.Add(this.txtNewPassAgain);
            this.groupControl1.Controls.Add(this.txtNewPass);
            this.groupControl1.Controls.Add(this.txtOldPass);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(38, -22);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(394, 274);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(216, 168);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 14;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Appearance.Options.UseFont = true;
            this.btnSubmit.Image = ((System.Drawing.Image)(resources.GetObject("btnSubmit.Image")));
            this.btnSubmit.Location = new System.Drawing.Point(121, 168);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(89, 23);
            this.btnSubmit.TabIndex = 13;
            this.btnSubmit.Text = "Thay đổi";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtNewPassAgain
            // 
            this.txtNewPassAgain.Location = new System.Drawing.Point(132, 127);
            this.txtNewPassAgain.Name = "txtNewPassAgain";
            this.txtNewPassAgain.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtNewPassAgain.Properties.Appearance.Options.UseFont = true;
            this.txtNewPassAgain.Properties.PasswordChar = '*';
            this.txtNewPassAgain.Size = new System.Drawing.Size(239, 20);
            this.txtNewPassAgain.TabIndex = 12;
            this.txtNewPassAgain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassAgain_KeyDown);
            // 
            // txtNewPass
            // 
            this.txtNewPass.Location = new System.Drawing.Point(132, 91);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtNewPass.Properties.Appearance.Options.UseFont = true;
            this.txtNewPass.Properties.PasswordChar = '*';
            this.txtNewPass.Size = new System.Drawing.Size(239, 20);
            this.txtNewPass.TabIndex = 11;
            this.txtNewPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPass_KeyDown);
            // 
            // txtOldPass
            // 
            this.txtOldPass.Location = new System.Drawing.Point(132, 56);
            this.txtOldPass.Name = "txtOldPass";
            this.txtOldPass.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtOldPass.Properties.Appearance.Options.UseFont = true;
            this.txtOldPass.Properties.PasswordChar = '*';
            this.txtOldPass.Size = new System.Drawing.Size(239, 20);
            this.txtOldPass.TabIndex = 10;
            this.txtOldPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOldPass_KeyDown);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl3.Location = new System.Drawing.Point(23, 129);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(89, 14);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "Nhập lại MK mới:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Location = new System.Drawing.Point(23, 93);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(77, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Mật khẩu mới:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(23, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Mật khẩu cũ:";
            // 
            // frmChangePassword
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 206);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangePassword";
            this.ShowIcon = false;
            this.Text = "Đổi mật khẩu";
            this.Load += new System.EventHandler(this.frmChangePassword_Load);
            this.Shown += new System.EventHandler(this.frmChangePassword_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPassAgain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPass.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private DevExpress.XtraEditors.TextEdit txtNewPassAgain;
        private DevExpress.XtraEditors.TextEdit txtNewPass;
        private DevExpress.XtraEditors.TextEdit txtOldPass;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnDong;
    }
}