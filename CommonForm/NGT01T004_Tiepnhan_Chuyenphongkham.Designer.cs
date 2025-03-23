namespace VNPT.HIS.CommonForm
{
    partial class NGT01T004_Tiepnhan_Chuyenphongkham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT01T004_Tiepnhan_Chuyenphongkham));
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.btnInphieu = new DevExpress.XtraEditors.SimpleButton();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.ucYeuCau = new VNPT.HIS.UserControl.ucComboBox();
            this.ucPhongKham = new VNPT.HIS.UserControl.ucComboBoxSearch();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(98, 134);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(76, 23);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnInphieu
            // 
            this.btnInphieu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnInphieu.Appearance.Options.UseFont = true;
            this.btnInphieu.Image = ((System.Drawing.Image)(resources.GetObject("btnInphieu.Image")));
            this.btnInphieu.Location = new System.Drawing.Point(177, 134);
            this.btnInphieu.Name = "btnInphieu";
            this.btnInphieu.Size = new System.Drawing.Size(82, 23);
            this.btnInphieu.TabIndex = 2;
            this.btnInphieu.Text = "In Phiếu";
            this.btnInphieu.Click += new System.EventHandler(this.btnInphieu_Click);
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(262, 134);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // ucYeuCau
            // 
            this.ucYeuCau.Caption = "Yêu cầu khám";
            this.ucYeuCau.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucYeuCau.CaptionValidate = false;
            this.ucYeuCau.CaptionWidth = 80;
            this.ucYeuCau.Location = new System.Drawing.Point(21, 22);
            this.ucYeuCau.Name = "ucYeuCau";
            this.ucYeuCau.SelectDataRowView = null;
            this.ucYeuCau.SelectIndex = -1;
            this.ucYeuCau.SelectText = "";
            this.ucYeuCau.SelectValue = "";
            this.ucYeuCau.Size = new System.Drawing.Size(387, 20);
            this.ucYeuCau.TabIndex = 0;
            // 
            // ucPhongKham
            // 
            this.ucPhongKham.Caption = "Phòng khám";
            this.ucPhongKham.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucPhongKham.CaptionValidate = false;
            this.ucPhongKham.CaptionWidth = 80;
            this.ucPhongKham.DefaultIndex = 0;
            this.ucPhongKham.Location = new System.Drawing.Point(21, 68);
            this.ucPhongKham.MinimumSize = new System.Drawing.Size(0, 20);
            this.ucPhongKham.Name = "ucPhongKham";
            this.ucPhongKham.Option_LockKeyTab = false;
            this.ucPhongKham.Option_Readonly = false;
            this.ucPhongKham.Option_SearchByID = false;
            this.ucPhongKham.SearchTextWidth = 80;
            this.ucPhongKham.SelectDataRowView = null;
            this.ucPhongKham.SelectIndex = -1;
            this.ucPhongKham.SelectSearchText = "";
            this.ucPhongKham.SelectText = "";
            this.ucPhongKham.SelectValue = "";
            this.ucPhongKham.Size = new System.Drawing.Size(387, 20);
            this.ucPhongKham.TabIndex = 4;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl1.Location = new System.Drawing.Point(21, 105);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(70, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "labelControl1";
            this.labelControl1.Visible = false;
            // 
            // NGT01T004_tiepnhan_chuyenphongkham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 211);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.ucPhongKham);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnInphieu);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.ucYeuCau);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT01T004_tiepnhan_chuyenphongkham";
            this.ShowIcon = false;
            this.Text = "Chuyển phòng khám";
            this.Load += new System.EventHandler(this.NGT01T004_tiepnhan_chuyenphongkham_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControl.ucComboBox ucYeuCau;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnInphieu;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private UserControl.ucComboBoxSearch ucPhongKham;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}