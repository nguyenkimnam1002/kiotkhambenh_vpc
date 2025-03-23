namespace VNPT.HIS.CommonForm
{
    partial class NGT02K053_PK_LCDBD1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblTieuDe = new DevExpress.XtraEditors.LabelControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblPhongKham = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.picLogo);
            this.panel1.Controls.Add(this.lblTieuDe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 111);
            this.panel1.TabIndex = 6;
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.picLogo.Image = global::VNPT.HIS.CommonForm.Properties.Resources.logo_965;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(172, 111);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Appearance.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTieuDe.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTieuDe.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblTieuDe.Location = new System.Drawing.Point(178, 0);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Padding = new System.Windows.Forms.Padding(30, 0, 30, 0);
            this.lblTieuDe.Size = new System.Drawing.Size(803, 108);
            this.lblTieuDe.TabIndex = 1;
            this.lblTieuDe.Text = "BỆNH VIỆN ĐA KHOA BƯU ĐIỆN";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblPhongKham);
            this.panel2.Controls.Add(this.lblTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 461);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 100);
            this.panel2.TabIndex = 7;
            // 
            // lblPhongKham
            // 
            this.lblPhongKham.Font = new System.Drawing.Font("Tahoma", 28F, System.Drawing.FontStyle.Bold);
            this.lblPhongKham.ForeColor = System.Drawing.Color.Blue;
            this.lblPhongKham.Location = new System.Drawing.Point(287, 3);
            this.lblPhongKham.Name = "lblPhongKham";
            this.lblPhongKham.Size = new System.Drawing.Size(694, 97);
            this.lblPhongKham.TabIndex = 1;
            this.lblPhongKham.Text = "Mời vào phòng khám";
            this.lblPhongKham.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold);
            this.lblTime.Location = new System.Drawing.Point(0, 3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(281, 97);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "00:00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 111);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(984, 350);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 26F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.OddRow.BackColor = System.Drawing.Color.Silver;
            this.gridView1.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Default;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.None;
            // 
            // NGT02K053_PK_LCDBD1
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimizeBox = false;
            this.Name = "NGT02K053_PK_LCDBD1";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông báo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NGT02K053_PK_LCDBD1_FormClosed);
            this.Load += new System.EventHandler(this.NGT02K053_PK_LCDBD1_Load);
            this.Resize += new System.EventHandler(this.NGT02K053_PK_LCDBD1_Resize);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private DevExpress.XtraEditors.LabelControl lblTieuDe;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Label lblPhongKham;
        private System.Windows.Forms.Label lblTime;
    }
}