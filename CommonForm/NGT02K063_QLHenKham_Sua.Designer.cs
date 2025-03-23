namespace VNPT.HIS.CommonForm
{
    partial class NGT02K063_QLHenKham_Sua
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K063_QLHenKham_Sua));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.dEditNgayHen = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ucBacSiKham = new VNPT.HIS.UserControl.ucComboBoxTextSearch();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dEditNgayHen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEditNgayHen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.ucBacSiKham);
            this.layoutControl1.Controls.Add(this.dEditNgayHen);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(409, 106);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Location = new System.Drawing.Point(127, 66);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(155, 28);
            this.panelControl1.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(79, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(2, 1);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(73, 27);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dEditNgayHen
            // 
            this.dEditNgayHen.EditValue = null;
            this.dEditNgayHen.Location = new System.Drawing.Point(105, 12);
            this.dEditNgayHen.Margin = new System.Windows.Forms.Padding(2);
            this.dEditNgayHen.Name = "dEditNgayHen";
            this.dEditNgayHen.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dEditNgayHen.Properties.Appearance.Options.UseFont = true;
            this.dEditNgayHen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dEditNgayHen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dEditNgayHen.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dEditNgayHen.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dEditNgayHen.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
            this.dEditNgayHen.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dEditNgayHen.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm:ss";
            this.dEditNgayHen.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dEditNgayHen.Properties.NullValuePrompt = "dd/MM/yyyy hh:mm:ss";
            this.dEditNgayHen.Properties.NullValuePromptShowForEmptyValue = true;
            this.dEditNgayHen.Size = new System.Drawing.Size(292, 20);
            this.dEditNgayHen.StyleController = this.layoutControl1;
            this.dEditNgayHen.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(409, 106);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHtmlStringInCaption = true;
            this.layoutControlItem1.Control = this.dEditNgayHen;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(147, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(389, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Ngày hẹn <color=Red>(*)</color>";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(88, 19);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(159, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(7, 7);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(389, 32);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ucBacSiKham
            // 
            this.ucBacSiKham.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ucBacSiKham.Appearance.Options.UseFont = true;
            this.ucBacSiKham.Caption = "Bác sĩ khám";
            this.ucBacSiKham.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucBacSiKham.CaptionValidate = true;
            this.ucBacSiKham.CaptionWidth = 94;
            this.ucBacSiKham.DefaultIndex = 0;
            this.ucBacSiKham.Location = new System.Drawing.Point(12, 36);
            this.ucBacSiKham.Name = "ucBacSiKham";
            this.ucBacSiKham.SearchTextWidth = 85;
            this.ucBacSiKham.SelectDataRowView = null;
            this.ucBacSiKham.SelectIndex = -1;
            this.ucBacSiKham.SelectText = "";
            this.ucBacSiKham.SelectTextSearch = "";
            this.ucBacSiKham.SelectValue = "";
            this.ucBacSiKham.Size = new System.Drawing.Size(385, 26);
            this.ucBacSiKham.TabIndex = 5;
            this.ucBacSiKham.TextSearchReadOnly = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucBacSiKham;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(389, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // NGT02K063_QLHenKham_Sua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 106);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K063_QLHenKham_Sua";
            this.ShowIcon = false;
            this.Text = "Cập nhật hẹn khám";
            this.Load += new System.EventHandler(this.NGT02K063_QLHenKham_Sua_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dEditNgayHen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dEditNgayHen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit dEditNgayHen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private UserControl.ucComboBoxTextSearch ucBacSiKham;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}