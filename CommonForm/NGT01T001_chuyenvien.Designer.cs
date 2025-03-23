namespace VNPT.HIS.CommonForm
{
    partial class NGT01T001_chuyenvien
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT01T001_chuyenvien));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            this.rbtChuyenDungTuyen = new DevExpress.XtraEditors.RadioGroup();
            this.ucLyDoChuyen = new VNPT.HIS.UserControl.ucComboBox();
            this.ucHinhThucChuyen = new VNPT.HIS.UserControl.ucComboBox();
            this.ucCDTD = new VNPT.HIS.UserControl.ucSearchLookup2();
            this.ucBenhVien = new VNPT.HIS.UserControl.ucSearchLookup2();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtChuyenDungTuyen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.rbtChuyenDungTuyen);
            this.layoutControl1.Controls.Add(this.ucLyDoChuyen);
            this.layoutControl1.Controls.Add(this.ucHinhThucChuyen);
            this.layoutControl1.Controls.Add(this.ucCDTD);
            this.layoutControl1.Controls.Add(this.ucBenhVien);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(660, 270);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnDong);
            this.panelControl1.Controls.Add(this.btnLuu);
            this.panelControl1.Location = new System.Drawing.Point(230, 210);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(200, 48);
            this.panelControl1.TabIndex = 57;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(103, 12);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.Location = new System.Drawing.Point(21, 12);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(75, 23);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // rbtChuyenDungTuyen
            // 
            this.rbtChuyenDungTuyen.EditValue = "3";
            this.rbtChuyenDungTuyen.Location = new System.Drawing.Point(12, 159);
            this.rbtChuyenDungTuyen.Name = "rbtChuyenDungTuyen";
            this.rbtChuyenDungTuyen.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rbtChuyenDungTuyen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rbtChuyenDungTuyen.Properties.Appearance.Options.UseBackColor = true;
            this.rbtChuyenDungTuyen.Properties.Appearance.Options.UseFont = true;
            this.rbtChuyenDungTuyen.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rbtChuyenDungTuyen.Properties.Columns = 1;
            this.rbtChuyenDungTuyen.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "Chuyển đúng tuyến CMKT gồm các trường hợp chuyển người bệnh theo đúng quy định tạ" +
                    "i các khoản 1,2,3,4 điều 5 Thông tư"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "Chuyển vượt tuyến CMKT gồm các trường hợp chuyển người bệnh không đúng quy định t" +
                    "ại các khoản 1,2,3,4 điều 5 Thông tư")});
            this.rbtChuyenDungTuyen.Size = new System.Drawing.Size(636, 47);
            this.rbtChuyenDungTuyen.StyleController = this.layoutControl1;
            this.rbtChuyenDungTuyen.TabIndex = 56;
            // 
            // ucLyDoChuyen
            // 
            this.ucLyDoChuyen.Caption = "Lý do chuyển";
            this.ucLyDoChuyen.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucLyDoChuyen.CaptionValidate = false;
            this.ucLyDoChuyen.CaptionWidth = 0;
            this.ucLyDoChuyen.Location = new System.Drawing.Point(116, 124);
            this.ucLyDoChuyen.Name = "ucLyDoChuyen";
            this.ucLyDoChuyen.SelectDataRowView = null;
            this.ucLyDoChuyen.SelectIndex = -1;
            this.ucLyDoChuyen.SelectText = "";
            this.ucLyDoChuyen.SelectValue = "";
            this.ucLyDoChuyen.Size = new System.Drawing.Size(532, 31);
            this.ucLyDoChuyen.TabIndex = 7;
            // 
            // ucHinhThucChuyen
            // 
            this.ucHinhThucChuyen.Caption = "Hình thức chuyển";
            this.ucHinhThucChuyen.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucHinhThucChuyen.CaptionValidate = false;
            this.ucHinhThucChuyen.CaptionWidth = 0;
            this.ucHinhThucChuyen.Location = new System.Drawing.Point(116, 102);
            this.ucHinhThucChuyen.Name = "ucHinhThucChuyen";
            this.ucHinhThucChuyen.SelectDataRowView = null;
            this.ucHinhThucChuyen.SelectIndex = -1;
            this.ucHinhThucChuyen.SelectText = "";
            this.ucHinhThucChuyen.SelectValue = "";
            this.ucHinhThucChuyen.Size = new System.Drawing.Size(532, 18);
            this.ucHinhThucChuyen.TabIndex = 6;
            // 
            // ucCDTD
            // 
            this.ucCDTD.Location = new System.Drawing.Point(116, 38);
            this.ucCDTD.Name = "ucCDTD";
            this.ucCDTD.Option_Caption = "Chẩn đoán TD";
            this.ucCDTD.Option_CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucCDTD.Option_CaptionValidate = false;
            this.ucCDTD.Option_CaptionWidth = 0;
            this.ucCDTD.Option_Fill = 2;
            this.ucCDTD.Option_FindColumns = "*";
            this.ucCDTD.Option_LockKeyTab = false;
            this.ucCDTD.Option_MemoEditHeight = 60;
            this.ucCDTD.Option_Readonly = false;
            this.ucCDTD.Option_ReadOnlyText = true;
            this.ucCDTD.Option_SearchTextWidth = 100;
            this.ucCDTD.Option_Type = 2;
            this.ucCDTD.SelectedDataRowView = null;
            this.ucCDTD.SelectedIndex = -1;
            this.ucCDTD.SelectedText = "";
            this.ucCDTD.SelectedValue = "";
            this.ucCDTD.Size = new System.Drawing.Size(532, 60);
            this.ucCDTD.TabIndex = 5;
            // 
            // ucBenhVien
            // 
            this.ucBenhVien.Location = new System.Drawing.Point(116, 12);
            this.ucBenhVien.Name = "ucBenhVien";
            this.ucBenhVien.Option_Caption = "Chuyển từ viện";
            this.ucBenhVien.Option_CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucBenhVien.Option_CaptionValidate = false;
            this.ucBenhVien.Option_CaptionWidth = 0;
            this.ucBenhVien.Option_Fill = 1;
            this.ucBenhVien.Option_FindColumns = "*";
            this.ucBenhVien.Option_LockKeyTab = false;
            this.ucBenhVien.Option_MemoEditHeight = 18;
            this.ucBenhVien.Option_Readonly = false;
            this.ucBenhVien.Option_ReadOnlyText = true;
            this.ucBenhVien.Option_SearchTextWidth = 100;
            this.ucBenhVien.Option_Type = 1;
            this.ucBenhVien.SelectedDataRowView = null;
            this.ucBenhVien.SelectedIndex = -1;
            this.ucBenhVien.SelectedText = "";
            this.ucBenhVien.SelectedValue = "";
            this.ucBenhVien.Size = new System.Drawing.Size(532, 22);
            this.ucBenhVien.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(660, 270);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHtmlStringInCaption = true;
            this.layoutControlItem1.Control = this.ucBenhVien;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(640, 26);
            this.layoutControlItem1.Text = "Chuyển từ viện <color=255, 0, 0>(*)</color>";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(101, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AllowHtmlStringInCaption = true;
            this.layoutControlItem2.Control = this.ucCDTD;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(640, 64);
            this.layoutControlItem2.Text = "Chẩn đoán TD <color=255, 0, 0>(*)</color>";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(101, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AllowHtmlStringInCaption = true;
            this.layoutControlItem3.Control = this.ucHinhThucChuyen;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 90);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(640, 22);
            this.layoutControlItem3.Text = "Hình thức chuyển <color=255, 0, 0>(*)</color>";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(101, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AllowHtmlStringInCaption = true;
            this.layoutControlItem4.Control = this.ucLyDoChuyen;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 112);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(640, 35);
            this.layoutControlItem4.Text = "Lý do chuyển <color=255, 0, 0>(*)</color>";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(101, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.rbtChuyenDungTuyen;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 147);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(640, 51);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.panelControl1;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 198);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(204, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(640, 52);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // NGT01T001_chuyenvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 270);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT01T001_chuyenvien";
            this.ShowIcon = false;
            this.Text = "THÔNG TIN CHUYỂN TUYẾN";
            this.Load += new System.EventHandler(this.NGT01T001_chuyenvien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbtChuyenDungTuyen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        public UserControl.ucSearchLookup2 ucCDTD;
        public UserControl.ucSearchLookup2 ucBenhVien;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        public UserControl.ucComboBox ucLyDoChuyen;
        public UserControl.ucComboBox ucHinhThucChuyen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        public DevExpress.XtraEditors.RadioGroup rbtChuyenDungTuyen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}