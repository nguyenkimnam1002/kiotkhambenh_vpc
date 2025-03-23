namespace L1_Mini
{
    partial class PhongKham
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboYCKham = new System.Windows.Forms.ComboBox();
            this.cboPhongKham = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Yêu cầu khám:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 46);
            this.label2.TabIndex = 1;
            this.label2.Text = "Phòng khám:";
            // 
            // cboYCKham
            // 
            this.cboYCKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYCKham.FormattingEnabled = true;
            this.cboYCKham.Location = new System.Drawing.Point(289, 55);
            this.cboYCKham.Name = "cboYCKham";
            this.cboYCKham.Size = new System.Drawing.Size(876, 54);
            this.cboYCKham.TabIndex = 2;
            this.cboYCKham.SelectedIndexChanged += new System.EventHandler(this.cboYCKham_SelectedIndexChanged);
            this.cboYCKham.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboYCKham_MouseClick);
            // 
            // cboPhongKham
            // 
            this.cboPhongKham.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPhongKham.FormattingEnabled = true;
            this.cboPhongKham.Location = new System.Drawing.Point(289, 164);
            this.cboPhongKham.Name = "cboPhongKham";
            this.cboPhongKham.Size = new System.Drawing.Size(876, 54);
            this.cboPhongKham.TabIndex = 3;
            this.cboPhongKham.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cboPhongKham_MouseClick);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(289, 272);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(354, 104);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "ĐỒNG Ý";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(695, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(297, 104);
            this.button1.TabIndex = 5;
            this.button1.Text = "ĐÓNG";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PhongKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 421);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cboPhongKham);
            this.Controls.Add(this.cboYCKham);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PhongKham";
            this.ShowIcon = false;
            this.Text = "Chọn Yêu Cầu Khám";
            this.Load += new System.EventHandler(this.PhongKham_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboYCKham;
        private System.Windows.Forms.ComboBox cboPhongKham;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button button1;
    }
}