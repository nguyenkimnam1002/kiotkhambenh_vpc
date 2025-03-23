namespace VNPT.HIS.CommonForm
{
    partial class NGT02K056_TKBN_THEOPK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K056_TKBN_THEOPK));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.deDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.deTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.ucGridDanhSachBenhNhan = new VNPT.HIS.UserControl.ucGridview();
            this.ucCboKhoa = new VNPT.HIS.UserControl.ucComboBox();
            this.ucCboPhong = new VNPT.HIS.UserControl.ucComboBox();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucGridDanhSachBenhNhan);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.ucCboKhoa);
            this.layoutControl1.Controls.Add(this.ucCboPhong);
            this.layoutControl1.Controls.Add(this.deDenNgay);
            this.layoutControl1.Controls.Add(this.deTuNgay);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1071, 418);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnDong);
            this.panelControl1.Controls.Add(this.btnTimKiem);
            this.panelControl1.Location = new System.Drawing.Point(882, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(177, 31);
            this.panelControl1.TabIndex = 8;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.Location = new System.Drawing.Point(8, 4);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 23);
            this.btnTimKiem.TabIndex = 5;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // deDenNgay
            // 
            this.deDenNgay.EditValue = null;
            this.deDenNgay.EnterMoveNextControl = true;
            this.deDenNgay.Location = new System.Drawing.Point(254, 18);
            this.deDenNgay.Name = "deDenNgay";
            this.deDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deDenNgay.Properties.Appearance.Options.UseFont = true;
            this.deDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.deDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.deDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.deDenNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deDenNgay.Properties.NullValuePrompt = "dd/MM/yyyy";
            this.deDenNgay.Properties.NullValuePromptShowForEmptyValue = true;
            this.deDenNgay.Size = new System.Drawing.Size(116, 20);
            this.deDenNgay.StyleController = this.layoutControl1;
            this.deDenNgay.TabIndex = 2;
            // 
            // deTuNgay
            // 
            this.deTuNgay.EditValue = null;
            this.deTuNgay.EnterMoveNextControl = true;
            this.deTuNgay.Location = new System.Drawing.Point(67, 18);
            this.deTuNgay.Name = "deTuNgay";
            this.deTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deTuNgay.Properties.Appearance.Options.UseFont = true;
            this.deTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.deTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.deTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.deTuNgay.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deTuNgay.Properties.NullValuePrompt = "dd/MM/yyyy";
            this.deTuNgay.Properties.NullValuePromptShowForEmptyValue = true;
            this.deTuNgay.Size = new System.Drawing.Size(122, 20);
            this.deTuNgay.StyleController = this.layoutControl1;
            this.deTuNgay.TabIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1071, 418);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.deTuNgay;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(110, 35);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 8, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(181, 35);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Từ ngày";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.deDenNgay;
            this.layoutControlItem2.Location = new System.Drawing.Point(181, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(110, 35);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 8, 2);
            this.layoutControlItem2.Size = new System.Drawing.Size(181, 35);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "Đến ngày";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.panelControl1;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem5.Location = new System.Drawing.Point(870, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(181, 35);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(181, 35);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(181, 35);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(104, 4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(68, 23);
            this.btnDong.TabIndex = 11;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // ucGridDanhSachBenhNhan
            // 
            this.ucGridDanhSachBenhNhan.caption = "";
            this.ucGridDanhSachBenhNhan.Location = new System.Drawing.Point(12, 47);
            this.ucGridDanhSachBenhNhan.Name = "ucGridDanhSachBenhNhan";
            this.ucGridDanhSachBenhNhan.SelectedRow = null;
            this.ucGridDanhSachBenhNhan.Size = new System.Drawing.Size(1047, 359);
            this.ucGridDanhSachBenhNhan.TabIndex = 9;
            // 
            // ucCboKhoa
            // 
            this.ucCboKhoa.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucCboKhoa.Appearance.Options.UseFont = true;
            this.ucCboKhoa.Caption = "Khoa";
            this.ucCboKhoa.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucCboKhoa.CaptionValidate = false;
            this.ucCboKhoa.CaptionWidth = 50;
            this.ucCboKhoa.Location = new System.Drawing.Point(380, 18);
            this.ucCboKhoa.Name = "ucCboKhoa";
            this.ucCboKhoa.SelectDataRowView = null;
            this.ucCboKhoa.SelectIndex = -1;
            this.ucCboKhoa.SelectText = "";
            this.ucCboKhoa.SelectValue = "";
            this.ucCboKhoa.Size = new System.Drawing.Size(196, 25);
            this.ucCboKhoa.TabIndex = 3;
            // 
            // ucCboPhong
            // 
            this.ucCboPhong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucCboPhong.Appearance.Options.UseFont = true;
            this.ucCboPhong.Caption = "Phòng";
            this.ucCboPhong.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucCboPhong.CaptionValidate = false;
            this.ucCboPhong.CaptionWidth = 50;
            this.ucCboPhong.Location = new System.Drawing.Point(586, 18);
            this.ucCboPhong.Name = "ucCboPhong";
            this.ucCboPhong.SelectDataRowView = null;
            this.ucCboPhong.SelectIndex = -1;
            this.ucCboPhong.SelectText = "";
            this.ucCboPhong.SelectValue = "";
            this.ucCboPhong.Size = new System.Drawing.Size(292, 25);
            this.ucCboPhong.TabIndex = 4;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ucCboPhong;
            this.layoutControlItem3.Location = new System.Drawing.Point(568, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 8, 2);
            this.layoutControlItem3.Size = new System.Drawing.Size(302, 35);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ucCboKhoa;
            this.layoutControlItem4.Location = new System.Drawing.Point(362, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 8, 2);
            this.layoutControlItem4.Size = new System.Drawing.Size(206, 35);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ucGridDanhSachBenhNhan;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1051, 363);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // NGT02K056_TKBN_THEOPK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 418);
            this.Controls.Add(this.layoutControl1);
            this.Name = "NGT02K056_TKBN_THEOPK";
            this.ShowIcon = false;
            this.Text = "Thống kê bệnh nhân theo phòng khám";
            this.Load += new System.EventHandler(this.NGT02K056_TKBN_THEOPK_Load);
            this.Shown += new System.EventHandler(this.NGT02K056_TKBN_THEOPK_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.ucComboBox ucCboKhoa;
        private UserControl.ucComboBox ucCboPhong;
        private DevExpress.XtraEditors.DateEdit deDenNgay;
        private DevExpress.XtraEditors.DateEdit deTuNgay;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private UserControl.ucGridview ucGridDanhSachBenhNhan;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraEditors.SimpleButton btnDong;
    }
}