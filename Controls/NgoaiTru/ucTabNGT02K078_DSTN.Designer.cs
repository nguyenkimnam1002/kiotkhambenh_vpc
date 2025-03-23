namespace VNPT.HIS.Controls.NgoaiTru
{
    partial class ucTabNGT02K078_DSTN
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucTabNGT02K078_DSTN));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.dtimeTu = new DevExpress.XtraEditors.DateEdit();
            this.dtTu = new DevExpress.XtraLayout.LayoutControlItem();
            this.dtimeDen = new DevExpress.XtraEditors.DateEdit();
            this.dtDen = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.ucGrid_DsBN = new VNPT.HIS.UserControl.ucGridview();
            this.ucPhongKham = new VNPT.HIS.UserControl.ucComboBox();
            this.ucTrangthai = new VNPT.HIS.UserControl.ucComboBox();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeTu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeDen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeDen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucGrid_DsBN);
            this.layoutControl1.Controls.Add(this.btnTimKiem);
            this.layoutControl1.Controls.Add(this.ucPhongKham);
            this.layoutControl1.Controls.Add(this.ucTrangthai);
            this.layoutControl1.Controls.Add(this.dtimeDen);
            this.layoutControl1.Controls.Add(this.dtimeTu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1024, 403);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.layoutControlGroup1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.dtTu,
            this.dtDen,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1024, 403);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // dtimeTu
            // 
            this.dtimeTu.EditValue = null;
            this.dtimeTu.Location = new System.Drawing.Point(47, 12);
            this.dtimeTu.Name = "dtimeTu";
            this.dtimeTu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtimeTu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});


            this.dtimeTu.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtimeTu.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtimeTu.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtimeTu.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime; 
            this.dtimeTu.Properties.NullValuePrompt = "dd/MM/yyyy";
            this.dtimeTu.Properties.NullValuePromptShowForEmptyValue = true;


            this.dtimeTu.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtimeTu.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtimeTu.Size = new System.Drawing.Size(112, 20);
            this.dtimeTu.StyleController = this.layoutControl1;
            this.dtimeTu.TabIndex = 4;
            // 
            // dtTu
            // 
            this.dtTu.Control = this.dtimeTu;
            this.dtTu.Location = new System.Drawing.Point(0, 0);
            this.dtTu.Name = "dtTu";
            this.dtTu.Size = new System.Drawing.Size(151, 26);
            this.dtTu.Text = "Từ";
            this.dtTu.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.dtTu.TextSize = new System.Drawing.Size(30, 14);
            this.dtTu.TextToControlDistance = 5;
            // 
            // dtimeDen
            // 
            this.dtimeDen.EditValue = null;
            this.dtimeDen.Location = new System.Drawing.Point(216, 12);
            this.dtimeDen.Name = "dtimeDen";
            this.dtimeDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtimeDen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtimeDen.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtimeDen.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtimeDen.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtimeDen.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtimeDen.Properties.NullValuePrompt = "dd/MM/yyyy";
            this.dtimeDen.Properties.NullValuePromptShowForEmptyValue = true;
            this.dtimeDen.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtimeDen.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtimeDen.Size = new System.Drawing.Size(112, 20);
            this.dtimeDen.StyleController = this.layoutControl1;
            this.dtimeDen.TabIndex = 5;
            // 
            // dtDen
            // 
            this.dtDen.Control = this.dtimeDen;
            this.dtDen.Location = new System.Drawing.Point(151, 0);
            this.dtDen.Name = "dtDen";
            this.dtDen.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 2, 2, 2);
            this.dtDen.Size = new System.Drawing.Size(169, 26);
            this.dtDen.Text = "Đến";
            this.dtDen.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.dtDen.TextSize = new System.Drawing.Size(30, 14);
            this.dtDen.TextToControlDistance = 5;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.Location = new System.Drawing.Point(908, 12);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(104, 22);
            this.btnTimKiem.StyleController = this.layoutControl1;
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnTimKiem;
            this.layoutControlItem3.Location = new System.Drawing.Point(896, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(108, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(108, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(108, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ucGrid_DsBN
            // 
            this.ucGrid_DsBN.caption = "Danh sách tiếp nhận";
            this.ucGrid_DsBN.Location = new System.Drawing.Point(12, 38);
            this.ucGrid_DsBN.Name = "ucGrid_DsBN";
            this.ucGrid_DsBN.SelectedRow = null;
            this.ucGrid_DsBN.Size = new System.Drawing.Size(1000, 353);
            this.ucGrid_DsBN.TabIndex = 9;
            // 
            // ucPhongKham
            // 
            this.ucPhongKham.Caption = "";
            this.ucPhongKham.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucPhongKham.CaptionValidate = false;
            this.ucPhongKham.CaptionWidth = 0;
            this.ucPhongKham.Location = new System.Drawing.Point(627, 12);
            this.ucPhongKham.Name = "ucPhongKham";
            this.ucPhongKham.SelectDataRowView = null;
            this.ucPhongKham.SelectIndex = -1;
            this.ucPhongKham.SelectText = "";
            this.ucPhongKham.SelectValue = "";
            this.ucPhongKham.Size = new System.Drawing.Size(277, 22);
            this.ucPhongKham.TabIndex = 7;
            // 
            // ucTrangthai
            // 
            this.ucTrangthai.Caption = "";
            this.ucTrangthai.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucTrangthai.CaptionValidate = false;
            this.ucTrangthai.CaptionWidth = 0;
            this.ucTrangthai.Location = new System.Drawing.Point(350, 12);
            this.ucTrangthai.Name = "ucTrangthai";
            this.ucTrangthai.SelectDataRowView = null;
            this.ucTrangthai.SelectIndex = -1;
            this.ucTrangthai.SelectText = "";
            this.ucTrangthai.SelectValue = "";
            this.ucTrangthai.Size = new System.Drawing.Size(182, 22);
            this.ucTrangthai.TabIndex = 6;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucTrangthai;
            this.layoutControlItem1.Location = new System.Drawing.Point(320, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 2, 2, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(204, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucPhongKham;
            this.layoutControlItem2.Location = new System.Drawing.Point(524, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 2, 2, 2);
            this.layoutControlItem2.Size = new System.Drawing.Size(372, 26);
            this.layoutControlItem2.Text = "Phòng Khám";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.ucGrid_DsBN;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1004, 357);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // ucTabNGT02K078_DSTN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucTabNGT02K078_DSTN";
            this.Size = new System.Drawing.Size(1024, 403);
            this.Load += new System.EventHandler(this.ucTabNGT02K078_DSTN_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeTu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeDen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtimeDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.ucComboBox ucPhongKham;
        private UserControl.ucComboBox ucTrangthai;
        private DevExpress.XtraEditors.DateEdit dtimeDen;
        private DevExpress.XtraEditors.DateEdit dtimeTu;
        private DevExpress.XtraLayout.LayoutControlItem dtTu;
        private DevExpress.XtraLayout.LayoutControlItem dtDen;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private UserControl.ucGridview ucGrid_DsBN;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}
