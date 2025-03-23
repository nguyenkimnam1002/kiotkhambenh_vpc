namespace L1_Mini
{
    partial class ConfigNew
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
            this.ckbAutoLogin = new System.Windows.Forms.CheckBox();
            this.ckbFull = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnLuuThoat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbStart = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboPhongThietLap = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboKhoaThietLap = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChonnhanh = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbAutoLogin
            // 
            this.ckbAutoLogin.AutoSize = true;
            this.ckbAutoLogin.Checked = false;
            this.ckbAutoLogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoLogin.Location = new System.Drawing.Point(165, 65);
            this.ckbAutoLogin.Name = "ckbAutoLogin";
            this.ckbAutoLogin.Size = new System.Drawing.Size(159, 17);
            this.ckbAutoLogin.TabIndex = 16;
            this.ckbAutoLogin.Text = "Tự động đăng nhập lần sau";
            this.ckbAutoLogin.UseVisualStyleBackColor = true;
            // 
            // ckbFull
            // 
            this.ckbFull.AutoSize = true;
            this.ckbFull.Checked = true;
            this.ckbFull.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFull.Location = new System.Drawing.Point(329, 65);
            this.ckbFull.Name = "ckbFull";
            this.ckbFull.Size = new System.Drawing.Size(103, 17);
            this.ckbFull.TabIndex = 17;
            this.ckbFull.Text = "Mở full màn hình";
            this.ckbFull.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Server";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServer.Location = new System.Drawing.Point(49, 27);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(511, 23);
            this.txtServer.TabIndex = 19;
            this.txtServer.Text = "https://histestl2.vncare.vn/vnpthis/RestService";
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(228, 19);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 20;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(438, 19);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 21;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuuThoat
            // 
            this.btnLuuThoat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuThoat.Location = new System.Drawing.Point(309, 19);
            this.btnLuuThoat.Name = "btnLuuThoat";
            this.btnLuuThoat.Size = new System.Drawing.Size(123, 23);
            this.btnLuuThoat.TabIndex = 23;
            this.btnLuuThoat.Text = "Lưu và Thoát";
            this.btnLuuThoat.UseVisualStyleBackColor = true;
            this.btnLuuThoat.Click += new System.EventHandler(this.btnLuuThoat_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(46, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "(chú ý nếu thay đổi Server thì cần đăng nhập lại";
            // 
            // ckbStart
            // 
            this.ckbStart.AutoSize = true;
            this.ckbStart.Checked = true;
            this.ckbStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbStart.Location = new System.Drawing.Point(10, 65);
            this.ckbStart.Name = "ckbStart";
            this.ckbStart.Size = new System.Drawing.Size(149, 17);
            this.ckbStart.TabIndex = 31;
            this.ckbStart.Text = "Khởi động cùng Windows";
            this.ckbStart.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboPhongThietLap);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comboKhoaThietLap);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboChonnhanh);
            this.groupBox2.Controls.Add(this.txtServer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.ckbAutoLogin);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ckbFull);
            this.groupBox2.Controls.Add(this.ckbStart);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(786, 102);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cấu hình chung trên app local";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(601, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "PHÒNG";
            this.label5.Visible = false;
            // 
            // comboPhongThietLap
            // 
            this.comboPhongThietLap.FormattingEnabled = true;
            this.comboPhongThietLap.Location = new System.Drawing.Point(653, 63);
            this.comboPhongThietLap.Name = "comboPhongThietLap";
            this.comboPhongThietLap.Size = new System.Drawing.Size(121, 21);
            this.comboPhongThietLap.TabIndex = 37;
            this.comboPhongThietLap.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "KHOA";
            this.label4.Visible = false;
            // 
            // comboKhoaThietLap
            // 
            this.comboKhoaThietLap.FormattingEnabled = true;
            this.comboKhoaThietLap.Location = new System.Drawing.Point(477, 63);
            this.comboKhoaThietLap.Name = "comboKhoaThietLap";
            this.comboKhoaThietLap.Size = new System.Drawing.Size(121, 21);
            this.comboKhoaThietLap.TabIndex = 35;
            this.comboKhoaThietLap.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(582, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Chọn nhanh";
            // 
            // cboChonnhanh
            // 
            this.cboChonnhanh.FormattingEnabled = true;
            this.cboChonnhanh.Location = new System.Drawing.Point(653, 27);
            this.cboChonnhanh.Name = "cboChonnhanh";
            this.cboChonnhanh.Size = new System.Drawing.Size(121, 21);
            this.cboChonnhanh.TabIndex = 33;
            this.cboChonnhanh.SelectedIndexChanged += new System.EventHandler(this.cboChonnhanh_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(780, 278);
            this.dataGridView1.TabIndex = 44;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDong);
            this.panel1.Controls.Add(this.btnLuu);
            this.panel1.Controls.Add(this.btnLuuThoat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 399);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 54);
            this.panel1.TabIndex = 45;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(786, 297);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Các biến cấu hình riêng trên website";
            // 
            // ConfigNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 453);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigNew";
            this.ShowIcon = false;
            this.Text = "CẤU HÌNH KIỂU CẤP SỐ";
            this.Load += new System.EventHandler(this.ConfigNew_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbAutoLogin;
        private System.Windows.Forms.CheckBox ckbFull;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnLuuThoat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboChonnhanh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboKhoaThietLap;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboPhongThietLap;
    }
}