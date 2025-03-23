namespace VNPT.HIS.CommonForm
{
    partial class NGT02K028_HienthiDanhSachKham
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
            this.text_temp = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.text_temp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // text_temp
            // 
            this.text_temp.EditValue = "vnpt";
            this.text_temp.Location = new System.Drawing.Point(-35, -37);
            this.text_temp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.text_temp.Name = "text_temp";
            this.text_temp.Properties.ReadOnly = true;
            this.text_temp.Size = new System.Drawing.Size(35, 22);
            this.text_temp.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.Options.UseTextOptions = true;
            this.panelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.panelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.panelControl1.Controls.Add(this.text_temp);
            this.panelControl1.Controls.Add(this.lblTitle);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1182, 185);
            this.panelControl1.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblTitle.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(2, 2);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1178, 181);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Phòng";
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.Options.UseTextOptions = true;
            this.panelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.panelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Controls.Add(this.textEdit1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 185);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1182, 468);
            this.panelControl2.TabIndex = 4;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(2, 2);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1178, 464);
            this.gridControl1.TabIndex = 3;
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
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 40F, System.Drawing.FontStyle.Bold);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.Appearance.Row.Options.UseTextOptions = true;
            this.gridView1.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Default;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView1_RowCellStyle);
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "vnpt";
            this.textEdit1.Location = new System.Drawing.Point(-35, -37);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(35, 22);
            this.textEdit1.TabIndex = 0;
            // 
            // NGT02K028_HienthiDanhSachKham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "NGT02K028_HienthiDanhSachKham";
            this.ShowIcon = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NGT02K028_HienthiDanhSachKham_FormClosed);
            this.Load += new System.EventHandler(this.NGT02K028_HienthiDanhSachKham_Load);
            this.DoubleClick += new System.EventHandler(this.NGT02K028_HienthiDanhSachKham_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.text_temp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit text_temp;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}