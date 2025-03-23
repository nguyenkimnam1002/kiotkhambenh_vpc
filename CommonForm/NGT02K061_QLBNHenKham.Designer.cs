namespace VNPT.HIS.CommonForm
{
    partial class NGT02K061_QLBNHenKham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K061_QLBNHenKham));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.ucCbbTrangThai = new VNPT.HIS.UserControl.ucComboBox();
            this.lblDen = new DevExpress.XtraEditors.LabelControl();
            this.lblTu = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ucGrid_DSBN = new VNPT.HIS.UserControl.ucGridview();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(904, 377);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.ucGrid_DSBN);
            this.panelControl2.Location = new System.Drawing.Point(2, 31);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(900, 344);
            this.panelControl2.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.panelControl1.Appearance.Options.UseFont = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.dtDenNgay);
            this.panelControl1.Controls.Add(this.dtTuNgay);
            this.panelControl1.Controls.Add(this.ucCbbTrangThai);
            this.panelControl1.Controls.Add(this.lblDen);
            this.panelControl1.Controls.Add(this.lblTu);
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.MaximumSize = new System.Drawing.Size(0, 30);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(900, 25);
            this.panelControl1.TabIndex = 4;
            // 
            // btnSearch
            // 
            this.btnSearch.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Appearance.Options.UseFont = true;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(573, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.Location = new System.Drawing.Point(161, 5);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dtDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Size = new System.Drawing.Size(100, 20);
            this.dtDenNgay.TabIndex = 5;
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.EditValue = null;
            this.dtTuNgay.Location = new System.Drawing.Point(27, 5);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dtTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Size = new System.Drawing.Size(100, 20);
            this.dtTuNgay.TabIndex = 4;
            // 
            // ucCbbTrangThai
            // 
            this.ucCbbTrangThai.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucCbbTrangThai.Appearance.Options.UseFont = true;
            this.ucCbbTrangThai.Caption = "Trạng thái";
            this.ucCbbTrangThai.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucCbbTrangThai.CaptionValidate = true;
            this.ucCbbTrangThai.CaptionWidth = 134;
            this.ucCbbTrangThai.Location = new System.Drawing.Point(267, 5);
            this.ucCbbTrangThai.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucCbbTrangThai.Name = "ucCbbTrangThai";
            this.ucCbbTrangThai.SelectDataRowView = null;
            this.ucCbbTrangThai.SelectIndex = -1;
            this.ucCbbTrangThai.SelectText = "";
            this.ucCbbTrangThai.SelectValue = "";
            this.ucCbbTrangThai.Size = new System.Drawing.Size(300, 20);
            this.ucCbbTrangThai.TabIndex = 3;
            // 
            // lblDen
            // 
            this.lblDen.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDen.Location = new System.Drawing.Point(133, 6);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(22, 14);
            this.lblDen.TabIndex = 2;
            this.lblDen.Text = "Đến";
            // 
            // lblTu
            // 
            this.lblTu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTu.Location = new System.Drawing.Point(5, 6);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(16, 14);
            this.lblTu.TabIndex = 0;
            this.lblTu.Text = "Từ";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(904, 377);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(904, 29);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl2;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(904, 348);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucGrid_DSBN
            // 
            this.ucGrid_DSBN.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucGrid_DSBN.Appearance.Options.UseFont = true;
            this.ucGrid_DSBN.caption = "DANH SÁCH BỆNH NHÂN HẸN KHÁM";
            this.ucGrid_DSBN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGrid_DSBN.Location = new System.Drawing.Point(2, 2);
            this.ucGrid_DSBN.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucGrid_DSBN.Name = "ucGrid_DSBN";
            this.ucGrid_DSBN.SelectedRow = null;
            this.ucGrid_DSBN.Size = new System.Drawing.Size(896, 340);
            this.ucGrid_DSBN.TabIndex = 2;
            // 
            // NGT02K061_QLBNHenKham
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 377);
            this.Controls.Add(this.layoutControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K061_QLBNHenKham";
            this.ShowIcon = false;
            this.Text = "Quản lý hẹn khám";
            this.Load += new System.EventHandler(this.NGT02K061_QLBNHenKham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LabelControl lblDen;
        private DevExpress.XtraEditors.LabelControl lblTu;
        private DevExpress.XtraEditors.DateEdit dtTuNgay;
        private UserControl.ucComboBox ucCbbTrangThai;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private UserControl.ucGridview ucGrid_DSBN;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}