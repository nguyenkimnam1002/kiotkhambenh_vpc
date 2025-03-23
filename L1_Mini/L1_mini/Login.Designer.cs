namespace L1_Mini
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbAutoLogin = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(142, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên đăng nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(142, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mật khẩu";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(238, 29);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(156, 22);
            this.txtUser.TabIndex = 2;
            this.txtUser.Text = "";
            // 
            // txtPass
            // 
            this.txtPass.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.Location = new System.Drawing.Point(238, 70);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(156, 22);
            this.txtPass.TabIndex = 3;
            this.txtPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_KeyDown);
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.Image = global::L1_Mini.Properties.Resources.btnSubmit_Image;
            this.btnDangNhap.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDangNhap.Location = new System.Drawing.Point(171, 131);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(102, 24);
            this.btnDangNhap.TabIndex = 4;
            this.btnDangNhap.Text = "Đăng Nhập";
            this.btnDangNhap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.Image = global::L1_Mini.Properties.Resources.btnDong_Image;
            this.btnThoat.Location = new System.Drawing.Point(293, 131);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(74, 24);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 173);
            this.label3.TabIndex = 14;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbAutoLogin
            // 
            this.ckbAutoLogin.AutoSize = true;
            this.ckbAutoLogin.Checked = true;
            this.ckbAutoLogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoLogin.Location = new System.Drawing.Point(238, 102);
            this.ckbAutoLogin.Name = "ckbAutoLogin";
            this.ckbAutoLogin.Size = new System.Drawing.Size(159, 17);
            this.ckbAutoLogin.TabIndex = 15;
            this.ckbAutoLogin.Text = "Tự động đăng nhập lần sau";
            this.ckbAutoLogin.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Server: ";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 173);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckbAutoLogin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.ShowIcon = false;
            this.Text = "ĐĂNG NHẬP";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbAutoLogin;
        private System.Windows.Forms.Label label4;
    }
}