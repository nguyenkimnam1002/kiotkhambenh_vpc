namespace VNPT.HIS.CommonForm
{
    partial class NGT02K059_show_img
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K059_show_img));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.btnIn = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureEdit1.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.NullText = "Không có ảnh!";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit1.Size = new System.Drawing.Size(584, 413);
            this.pictureEdit1.TabIndex = 0;
            // 
            // btnIn
            // 
            this.btnIn.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnIn.Appearance.Options.UseFont = true;
            this.btnIn.Image = ((System.Drawing.Image)(resources.GetObject("btnIn.Image")));
            this.btnIn.Location = new System.Drawing.Point(189, 427);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(100, 23);
            this.btnIn.TabIndex = 1;
            this.btnIn.Text = "In ảnh BN";
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(295, 427);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // NGT02K059_show_img
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.pictureEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K059_show_img";
            this.ShowIcon = false;
            this.Text = "Thông tin ảnh";
            this.Load += new System.EventHandler(this.NGT02K059_show_img_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton btnIn;
        private DevExpress.XtraEditors.SimpleButton btnClose;
    }
}