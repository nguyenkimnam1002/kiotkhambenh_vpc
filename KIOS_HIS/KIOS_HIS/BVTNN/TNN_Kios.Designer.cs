namespace KIOS_HIS.BVTNN
{
    partial class TNN_Kios
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
            this.gbDangKyKhamBenh = new System.Windows.Forms.GroupBox();
            this.lbLoi = new System.Windows.Forms.Label();
            this.lbTheUutien = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaBN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.Button();
            this.gbLaySoKhamBenh = new System.Windows.Forms.GroupBox();
            this.lbSTT = new System.Windows.Forms.Label();
            this.lbKhamMoi = new System.Windows.Forms.Label();
            this.btnDichvu = new System.Windows.Forms.Button();
            this.btnKhamHuyetAp = new System.Windows.Forms.Button();
            this.buttonLaysoUT = new System.Windows.Forms.Button();
            this.btnKhamTieuDuong = new System.Windows.Forms.Button();
            this.btnKhamMoi = new System.Windows.Forms.Button();
            this.gbDangKyKhamBenh.SuspendLayout();
            this.gbLaySoKhamBenh.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDangKyKhamBenh
            // 
            this.gbDangKyKhamBenh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDangKyKhamBenh.Controls.Add(this.lbLoi);
            this.gbDangKyKhamBenh.Controls.Add(this.lbTheUutien);
            this.gbDangKyKhamBenh.Controls.Add(this.btnRefresh);
            this.gbDangKyKhamBenh.Controls.Add(this.btnDangKy);
            this.gbDangKyKhamBenh.Controls.Add(this.label2);
            this.gbDangKyKhamBenh.Controls.Add(this.txtMaBN);
            this.gbDangKyKhamBenh.Controls.Add(this.label3);
            this.gbDangKyKhamBenh.Controls.Add(this.txtHoTen);
            this.gbDangKyKhamBenh.Controls.Add(this.label1);
            this.gbDangKyKhamBenh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDangKyKhamBenh.Location = new System.Drawing.Point(12, 381);
            this.gbDangKyKhamBenh.Name = "gbDangKyKhamBenh";
            this.gbDangKyKhamBenh.Size = new System.Drawing.Size(1170, 360);
            this.gbDangKyKhamBenh.TabIndex = 6;
            this.gbDangKyKhamBenh.TabStop = false;
            this.gbDangKyKhamBenh.Text = "Đăng ký khám bệnh";
            // 
            // lbLoi
            // 
            this.lbLoi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLoi.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLoi.ForeColor = System.Drawing.Color.Red;
            this.lbLoi.Location = new System.Drawing.Point(3, 92);
            this.lbLoi.Margin = new System.Windows.Forms.Padding(0);
            this.lbLoi.Name = "lbLoi";
            this.lbLoi.Size = new System.Drawing.Size(1167, 45);
            this.lbLoi.TabIndex = 4;
            this.lbLoi.Text = "Lỗi: Nhập sai mã Bệnh nhân!";
            this.lbLoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLoi.Visible = false;
            // 
            // lbTheUutien
            // 
            this.lbTheUutien.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTheUutien.ForeColor = System.Drawing.Color.Green;
            this.lbTheUutien.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbTheUutien.Location = new System.Drawing.Point(81, 297);
            this.lbTheUutien.Margin = new System.Windows.Forms.Padding(3);
            this.lbTheUutien.Name = "lbTheUutien";
            this.lbTheUutien.Size = new System.Drawing.Size(403, 40);
            this.lbTheUutien.TabIndex = 5;
            this.lbTheUutien.Text = "Dùng thẻ ưu tiên!";
            this.lbTheUutien.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbTheUutien.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.CadetBlue;
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(973, 276);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(170, 70);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Xóa";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDangKy
            // 
            this.btnDangKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDangKy.BackColor = System.Drawing.Color.CadetBlue;
            this.btnDangKy.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.Location = new System.Drawing.Point(537, 276);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(430, 70);
            this.btnDangKy.TabIndex = 3;
            this.btnDangKy.Text = "ĐĂNG KÝ KHÁM";
            this.btnDangKy.UseVisualStyleBackColor = false;
            this.btnDangKy.Visible = false;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 53);
            this.label2.TabIndex = 19;
            this.label2.Text = "Mã tìm kiếm";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMaBN
            // 
            this.txtMaBN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaBN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaBN.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaBN.Location = new System.Drawing.Point(329, 138);
            this.txtMaBN.Margin = new System.Windows.Forms.Padding(3, 10, 300, 3);
            this.txtMaBN.MaxLength = 500;
            this.txtMaBN.Name = "txtMaBN";
            this.txtMaBN.Size = new System.Drawing.Size(814, 56);
            this.txtMaBN.TabIndex = 17;
            this.txtMaBN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaBN_KeyDown);
            this.txtMaBN.Leave += new System.EventHandler(this.txtMaBN_Leave);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(326, 53);
            this.label3.TabIndex = 20;
            this.label3.Text = "Tên Bệnh nhân";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHoTen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHoTen.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHoTen.Location = new System.Drawing.Point(329, 204);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 10, 300, 3);
            this.txtHoTen.MaxLength = 500;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(814, 56);
            this.txtHoTen.TabIndex = 18;
            this.txtHoTen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHoTen_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Location = new System.Drawing.Point(1, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(300, 3, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1164, 60);
            this.label1.TabIndex = 16;
            this.label1.Text = "NHẬP/QUÉT THẺ ĐỂ ĐĂNG KÝ KHÁM BỆNH";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetting
            // 
            this.btnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetting.Image = global::KIOS_HIS.Properties.Resources.list;
            this.btnSetting.Location = new System.Drawing.Point(1131, 12);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(51, 44);
            this.btnSetting.TabIndex = 4;
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // gbLaySoKhamBenh
            // 
            this.gbLaySoKhamBenh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLaySoKhamBenh.Controls.Add(this.lbSTT);
            this.gbLaySoKhamBenh.Controls.Add(this.lbKhamMoi);
            this.gbLaySoKhamBenh.Controls.Add(this.btnDichvu);
            this.gbLaySoKhamBenh.Controls.Add(this.btnKhamHuyetAp);
            this.gbLaySoKhamBenh.Controls.Add(this.buttonLaysoUT);
            this.gbLaySoKhamBenh.Controls.Add(this.btnKhamTieuDuong);
            this.gbLaySoKhamBenh.Controls.Add(this.btnKhamMoi);
            this.gbLaySoKhamBenh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLaySoKhamBenh.Location = new System.Drawing.Point(12, 74);
            this.gbLaySoKhamBenh.Name = "gbLaySoKhamBenh";
            this.gbLaySoKhamBenh.Size = new System.Drawing.Size(1170, 277);
            this.gbLaySoKhamBenh.TabIndex = 5;
            this.gbLaySoKhamBenh.TabStop = false;
            this.gbLaySoKhamBenh.Text = "Lấy số khám bệnh";
            // 
            // lbSTT
            // 
            this.lbSTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSTT.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSTT.ForeColor = System.Drawing.Color.Red;
            this.lbSTT.Location = new System.Drawing.Point(3, 89);
            this.lbSTT.Margin = new System.Windows.Forms.Padding(0);
            this.lbSTT.Name = "lbSTT";
            this.lbSTT.Size = new System.Drawing.Size(1167, 45);
            this.lbSTT.TabIndex = 18;
            this.lbSTT.Text = "Số tiếp theo: 1";
            this.lbSTT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSTT.Visible = false;
            // 
            // lbKhamMoi
            // 
            this.lbKhamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKhamMoi.Font = new System.Drawing.Font("Tahoma", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbKhamMoi.ForeColor = System.Drawing.Color.Green;
            this.lbKhamMoi.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbKhamMoi.Location = new System.Drawing.Point(6, 30);
            this.lbKhamMoi.Margin = new System.Windows.Forms.Padding(300, 3, 3, 3);
            this.lbKhamMoi.Name = "lbKhamMoi";
            this.lbKhamMoi.Size = new System.Drawing.Size(1158, 60);
            this.lbKhamMoi.TabIndex = 15;
            this.lbKhamMoi.Text = "LẤY SỐ KHÁM BỆNH VỚI BỆNH NHÂN MỚI";
            this.lbKhamMoi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDichvu
            // 
            this.btnDichvu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnDichvu.BackColor = System.Drawing.Color.Teal;
            this.btnDichvu.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Bold);
            this.btnDichvu.Location = new System.Drawing.Point(553, 220);
            this.btnDichvu.Name = "btnDichvu";
            this.btnDichvu.Size = new System.Drawing.Size(314, 50);
            this.btnDichvu.TabIndex = 17;
            this.btnDichvu.Text = "VÉ DỊCH VỤ";
            this.btnDichvu.UseVisualStyleBackColor = false;
            this.btnDichvu.Visible = false;
            this.btnDichvu.Click += new System.EventHandler(this.btnDichvu_Click);
            // 
            // btnKhamHuyetAp
            // 
            this.btnKhamHuyetAp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnKhamHuyetAp.BackColor = System.Drawing.Color.Teal;
            this.btnKhamHuyetAp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhamHuyetAp.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.btnKhamHuyetAp.Location = new System.Drawing.Point(413, 139);
            this.btnKhamHuyetAp.Name = "btnKhamHuyetAp";
            this.btnKhamHuyetAp.Size = new System.Drawing.Size(350, 73);
            this.btnKhamHuyetAp.TabIndex = 13;
            this.btnKhamHuyetAp.Text = "KHÁM HUYẾT ÁP";
            this.btnKhamHuyetAp.UseVisualStyleBackColor = false;
            this.btnKhamHuyetAp.Click += new System.EventHandler(this.btnKhamHuyetAp_Click);
            // 
            // buttonLaysoUT
            // 
            this.buttonLaysoUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.buttonLaysoUT.BackColor = System.Drawing.Color.Teal;
            this.buttonLaysoUT.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Bold);
            this.buttonLaysoUT.Location = new System.Drawing.Point(216, 220);
            this.buttonLaysoUT.Name = "buttonLaysoUT";
            this.buttonLaysoUT.Size = new System.Drawing.Size(331, 50);
            this.buttonLaysoUT.TabIndex = 16;
            this.buttonLaysoUT.Text = "LẤY SỐ ƯU TIÊN";
            this.buttonLaysoUT.UseVisualStyleBackColor = false;
            this.buttonLaysoUT.Visible = false;
            this.buttonLaysoUT.Click += new System.EventHandler(this.buttonLaysoUT_Click);
            // 
            // btnKhamTieuDuong
            // 
            this.btnKhamTieuDuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnKhamTieuDuong.BackColor = System.Drawing.Color.Teal;
            this.btnKhamTieuDuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhamTieuDuong.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.btnKhamTieuDuong.Location = new System.Drawing.Point(793, 139);
            this.btnKhamTieuDuong.Name = "btnKhamTieuDuong";
            this.btnKhamTieuDuong.Size = new System.Drawing.Size(350, 73);
            this.btnKhamTieuDuong.TabIndex = 14;
            this.btnKhamTieuDuong.Text = "KHÁM TIỂU ĐƯỜNG";
            this.btnKhamTieuDuong.UseVisualStyleBackColor = false;
            this.btnKhamTieuDuong.Click += new System.EventHandler(this.btnKhamTieuDuong_Click);
            // 
            // btnKhamMoi
            // 
            this.btnKhamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnKhamMoi.BackColor = System.Drawing.Color.Teal;
            this.btnKhamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhamMoi.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold);
            this.btnKhamMoi.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnKhamMoi.Location = new System.Drawing.Point(28, 139);
            this.btnKhamMoi.Name = "btnKhamMoi";
            this.btnKhamMoi.Size = new System.Drawing.Size(352, 73);
            this.btnKhamMoi.TabIndex = 12;
            this.btnKhamMoi.Text = "KHÁM BỆNH";
            this.btnKhamMoi.UseVisualStyleBackColor = false;
            this.btnKhamMoi.Click += new System.EventHandler(this.btnKhamBenh_Click);
            // 
            // TNN_Kios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 780);
            this.Controls.Add(this.gbDangKyKhamBenh);
            this.Controls.Add(this.gbLaySoKhamBenh);
            this.Controls.Add(this.btnSetting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "TNN_Kios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KIOS CẤP SỐ TỰ ĐỘNG";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TNN_Kios_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TNN_Kios_KeyDown);
            this.gbDangKyKhamBenh.ResumeLayout(false);
            this.gbDangKyKhamBenh.PerformLayout();
            this.gbLaySoKhamBenh.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.GroupBox gbDangKyKhamBenh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaBN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Label lbLoi;
        private System.Windows.Forms.Label lbTheUutien;
        private System.Windows.Forms.GroupBox gbLaySoKhamBenh;
        private System.Windows.Forms.Label lbSTT;
        private System.Windows.Forms.Label lbKhamMoi;
        private System.Windows.Forms.Button btnDichvu;
        private System.Windows.Forms.Button btnKhamHuyetAp;
        private System.Windows.Forms.Button buttonLaysoUT;
        private System.Windows.Forms.Button btnKhamTieuDuong;
        private System.Windows.Forms.Button btnKhamMoi;
    }
}