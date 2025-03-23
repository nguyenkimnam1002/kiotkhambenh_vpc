namespace L1_Mini
{
    partial class BVVINHPHUC_KIEU3
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.Green;
            this.lbTitle.Location = new System.Drawing.Point(3, 37);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(750, 59);
            this.lbTitle.TabIndex = 2;
            this.lbTitle.Text = "LẤY SỐ THỨ TỰ KHÁM BỆNH";
            // 
            // BVVINHPHUC_KIEU3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 361);
            this.Controls.Add(this.lbTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BVVINHPHUC_KIEU3";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CẤP SỐ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BVVINHPHUC_KIEU3_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BVVINHPHUC_KIEU3_FormClosed);
            this.Load += new System.EventHandler(this.BVVINHPHUC_KIEU3_Load);
            this.SizeChanged += new System.EventHandler(this.BVVINHPHUC_KIEU3_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BVVINHPHUC_KIEU3_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbTitle;
    }
}