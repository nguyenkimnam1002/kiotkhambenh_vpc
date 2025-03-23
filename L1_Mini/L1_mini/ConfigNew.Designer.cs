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
            this.button2 = new System.Windows.Forms.Button();
            this.btnLuuThoat = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbStart = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.ckbAutoLogin.Checked = true;
            this.ckbAutoLogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoLogin.Location = new System.Drawing.Point(191, 68);
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
            this.ckbFull.Location = new System.Drawing.Point(374, 68);
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
            this.txtServer.Size = new System.Drawing.Size(478, 23);
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
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(438, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Đóng";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.label2.Location = new System.Drawing.Point(533, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "(chú ý nếu thay đổi Server thì cần  khởi động lại App)";
            // 
            // ckbStart
            // 
            this.ckbStart.AutoSize = true;
            this.ckbStart.Checked = true;
            this.ckbStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbStart.Location = new System.Drawing.Point(25, 68);
            this.ckbStart.Name = "ckbStart";
            this.ckbStart.Size = new System.Drawing.Size(149, 17);
            this.ckbStart.TabIndex = 31;
            this.ckbStart.Text = "Khởi động cùng Windows";
            this.ckbStart.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
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
            this.panel1.Controls.Add(this.button2);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnLuuThoat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}