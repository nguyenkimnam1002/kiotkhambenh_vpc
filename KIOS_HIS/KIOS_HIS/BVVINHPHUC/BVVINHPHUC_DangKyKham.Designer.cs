namespace L1_Mini
{
    partial class BVVINHPHUC_DangKyKham
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lbTieude1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnXoaBS = new System.Windows.Forms.Button();
            this.cboBacSi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_BacSi = new System.Windows.Forms.Panel();
            this.lbTieude2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel_BacSi.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(1107, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(38, 65);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "ĐỒNG Ý";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lbTieude1
            // 
            this.lbTieude1.AutoSize = true;
            this.lbTieude1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTieude1.ForeColor = System.Drawing.Color.Green;
            this.lbTieude1.Location = new System.Drawing.Point(12, 13);
            this.lbTieude1.Name = "lbTieude1";
            this.lbTieude1.Size = new System.Drawing.Size(579, 58);
            this.lbTieude1.TabIndex = 6;
            this.lbTieude1.Text = "CHỌN YÊU CẦU KHÁM";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnXoaBS);
            this.panel1.Controls.Add(this.cboBacSi);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.lbTieude1);
            this.panel1.Controls.Add(this.btnSubmit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(1362, 73);
            this.panel1.TabIndex = 7;
            // 
            // btnXoaBS
            // 
            this.btnXoaBS.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaBS.ForeColor = System.Drawing.Color.Red;
            this.btnXoaBS.Location = new System.Drawing.Point(1029, 10);
            this.btnXoaBS.Margin = new System.Windows.Forms.Padding(0);
            this.btnXoaBS.Name = "btnXoaBS";
            this.btnXoaBS.Size = new System.Drawing.Size(52, 52);
            this.btnXoaBS.TabIndex = 9;
            this.btnXoaBS.Text = "X";
            this.btnXoaBS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXoaBS.UseVisualStyleBackColor = true;
            this.btnXoaBS.Click += new System.EventHandler(this.btnXoaBS_Click);
            // 
            // cboBacSi
            // 
            this.cboBacSi.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBacSi.FormattingEnabled = true;
            this.cboBacSi.IntegralHeight = false;
            this.cboBacSi.ItemHeight = 53;
            this.cboBacSi.Location = new System.Drawing.Point(298, 11);
            this.cboBacSi.MaxDropDownItems = 12;
            this.cboBacSi.Name = "cboBacSi";
            this.cboBacSi.Size = new System.Drawing.Size(728, 61);
            this.cboBacSi.TabIndex = 8;
            this.cboBacSi.Visible = false;
            this.cboBacSi.Click += new System.EventHandler(this.cboBacSi_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(3, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1000, 2);
            this.label2.TabIndex = 7;
            this.label2.Text = "label2";
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.CadetBlue;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Image = global::KIOS_HIS.Properties.Resources.btnBack;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(1148, 5);
            this.btnBack.Margin = new System.Windows.Forms.Padding(0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(209, 63);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "Quay lại";
            this.btnBack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 146);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1362, 387);
            this.panel2.TabIndex = 8;
            // 
            // panel_BacSi
            // 
            this.panel_BacSi.Controls.Add(this.lbTieude2);
            this.panel_BacSi.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_BacSi.ForeColor = System.Drawing.Color.Red;
            this.panel_BacSi.Location = new System.Drawing.Point(0, 73);
            this.panel_BacSi.Name = "panel_BacSi";
            this.panel_BacSi.Size = new System.Drawing.Size(1362, 73);
            this.panel_BacSi.TabIndex = 9;
            this.panel_BacSi.Visible = false;
            // 
            // lbTieude2
            // 
            this.lbTieude2.AutoSize = true;
            this.lbTieude2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTieude2.ForeColor = System.Drawing.Color.Green;
            this.lbTieude2.Location = new System.Drawing.Point(12, 14);
            this.lbTieude2.Name = "lbTieude2";
            this.lbTieude2.Size = new System.Drawing.Size(579, 58);
            this.lbTieude2.TabIndex = 7;
            this.lbTieude2.Text = "CHỌN YÊU CẦU KHÁM";
            // 
            // BVVINHPHUC_DangKyKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 533);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_BacSi);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BVVINHPHUC_DangKyKham";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.BVVINHPHUC_DangKyKham_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_BacSi.ResumeLayout(false);
            this.panel_BacSi.PerformLayout();
            this.ResumeLayout(false);

            
        }

        #endregion
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lbTieude1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_BacSi;
        private System.Windows.Forms.ComboBox cboBacSi;
        private System.Windows.Forms.Label lbTieude2;
        private System.Windows.Forms.Button btnXoaBS;
    }
}