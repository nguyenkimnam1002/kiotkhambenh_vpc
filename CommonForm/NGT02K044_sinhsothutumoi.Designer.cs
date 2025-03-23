namespace VNPT.HIS.CommonForm
{
    partial class NGT02K044_sinhsothutumoi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K044_sinhsothutumoi));
            this.btnSinhso = new DevExpress.XtraEditors.SimpleButton();
            this.btnInphieu = new DevExpress.XtraEditors.SimpleButton();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.ucPhongKham = new VNPT.HIS.UserControl.ucComboBox();
            this.SuspendLayout();
            // 
            // btnSinhso
            // 
            this.btnSinhso.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSinhso.Appearance.Options.UseFont = true;
            this.btnSinhso.Image = ((System.Drawing.Image)(resources.GetObject("btnSinhso.Image")));
            this.btnSinhso.Location = new System.Drawing.Point(101, 88);
            this.btnSinhso.Name = "btnSinhso";
            this.btnSinhso.Size = new System.Drawing.Size(75, 23);
            this.btnSinhso.TabIndex = 1;
            this.btnSinhso.Text = "Sinh số";
            this.btnSinhso.Click += new System.EventHandler(this.btnSinhso_Click);
            // 
            // btnInphieu
            // 
            this.btnInphieu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnInphieu.Appearance.Options.UseFont = true;
            this.btnInphieu.Image = ((System.Drawing.Image)(resources.GetObject("btnInphieu.Image")));
            this.btnInphieu.Location = new System.Drawing.Point(178, 88);
            this.btnInphieu.Name = "btnInphieu";
            this.btnInphieu.Size = new System.Drawing.Size(87, 23);
            this.btnInphieu.TabIndex = 2;
            this.btnInphieu.Text = "In Phiếu";
            this.btnInphieu.Click += new System.EventHandler(this.btnInphieu_Click);
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(267, 88);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // ucPhongKham
            // 
            this.ucPhongKham.Caption = "Phòng khám";
            this.ucPhongKham.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucPhongKham.CaptionValidate = false;
            this.ucPhongKham.CaptionWidth = 80;
            this.ucPhongKham.Location = new System.Drawing.Point(21, 22);
            this.ucPhongKham.Name = "ucPhongKham";
            this.ucPhongKham.SelectDataRowView = null;
            this.ucPhongKham.SelectIndex = -1;
            this.ucPhongKham.SelectText = "";
            this.ucPhongKham.SelectValue = "";
            this.ucPhongKham.Size = new System.Drawing.Size(387, 20);
            this.ucPhongKham.TabIndex = 0;
            // 
            // NGT02K044_sinhsothutumoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 211);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnInphieu);
            this.Controls.Add(this.btnSinhso);
            this.Controls.Add(this.ucPhongKham);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K044_sinhsothutumoi";
            this.ShowIcon = false;
            this.Text = "Sinh số thứ tự ưu tiên mới";
            this.Load += new System.EventHandler(this.NGT02K044_sinhsothutumoi_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucComboBox ucPhongKham;
        private DevExpress.XtraEditors.SimpleButton btnSinhso;
        private DevExpress.XtraEditors.SimpleButton btnInphieu;
        private DevExpress.XtraEditors.SimpleButton btnDong;
    }
}