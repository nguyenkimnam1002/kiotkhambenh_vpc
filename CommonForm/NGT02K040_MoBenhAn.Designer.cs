namespace VNPT.HIS.CommonForm
{
    partial class NGT02K040_MoBenhAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NGT02K040_MoBenhAn));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblTenBN = new DevExpress.XtraEditors.LabelControl();
            this.btnMoBA = new DevExpress.XtraEditors.SimpleButton();
            this.ucGrid_DsBA = new VNPT.HIS.UserControl.ucGridview();
            this.cboKhoaPhong = new VNPT.HIS.UserControl.ucComboBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btn_Dong = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_Dong);
            this.layoutControl1.Controls.Add(this.lblTenBN);
            this.layoutControl1.Controls.Add(this.btnMoBA);
            this.layoutControl1.Controls.Add(this.ucGrid_DsBA);
            this.layoutControl1.Controls.Add(this.cboKhoaPhong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1044, 491);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblTenBN
            // 
            this.lblTenBN.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenBN.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTenBN.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTenBN.Location = new System.Drawing.Point(454, 12);
            this.lblTenBN.Name = "lblTenBN";
            this.lblTenBN.Size = new System.Drawing.Size(318, 17);
            this.lblTenBN.StyleController = this.layoutControl1;
            this.lblTenBN.TabIndex = 8;
            this.lblTenBN.Text = "Chưa chọn bệnh nhân";
            // 
            // btnMoBA
            // 
            this.btnMoBA.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnMoBA.Appearance.Options.UseFont = true;
            this.btnMoBA.Image = ((System.Drawing.Image)(resources.GetObject("btnMoBA.Image")));
            this.btnMoBA.Location = new System.Drawing.Point(794, 12);
            this.btnMoBA.Name = "btnMoBA";
            this.btnMoBA.Size = new System.Drawing.Size(138, 22);
            this.btnMoBA.StyleController = this.layoutControl1;
            this.btnMoBA.TabIndex = 7;
            this.btnMoBA.Text = "Mở lại bệnh án";
            this.btnMoBA.Click += new System.EventHandler(this.btnMoBA_Click);
            // 
            // ucGrid_DsBA
            // 
            this.ucGrid_DsBA.caption = "DANH SÁCH MỞ BỆNH ÁN";
            this.ucGrid_DsBA.Location = new System.Drawing.Point(12, 38);
            this.ucGrid_DsBA.Name = "ucGrid_DsBA";
            this.ucGrid_DsBA.SelectedRow = null;
            this.ucGrid_DsBA.Size = new System.Drawing.Size(1020, 441);
            this.ucGrid_DsBA.TabIndex = 6;
            // 
            // cboKhoaPhong
            // 
            this.cboKhoaPhong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cboKhoaPhong.Appearance.Options.UseFont = true;
            this.cboKhoaPhong.Caption = "Chọn khoa";
            this.cboKhoaPhong.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.cboKhoaPhong.CaptionValidate = false;
            this.cboKhoaPhong.CaptionWidth = 115;
            this.cboKhoaPhong.Location = new System.Drawing.Point(12, 12);
            this.cboKhoaPhong.Name = "cboKhoaPhong";
            this.cboKhoaPhong.SelectDataRowView = null;
            this.cboKhoaPhong.SelectIndex = -1;
            this.cboKhoaPhong.SelectText = "";
            this.cboKhoaPhong.SelectValue = "";
            this.cboKhoaPhong.Size = new System.Drawing.Size(420, 22);
            this.cboKhoaPhong.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1044, 491);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cboKhoaPhong;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(5, 5);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(424, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ucGrid_DsBA;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1024, 445);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnMoBA;
            this.layoutControlItem2.Location = new System.Drawing.Point(782, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(142, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(142, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(142, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblTenBN;
            this.layoutControlItem4.Location = new System.Drawing.Point(424, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(400, 21);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(110, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 20, 2, 2);
            this.layoutControlItem4.Size = new System.Drawing.Size(358, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // btn_Dong
            // 
            this.btn_Dong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Dong.Appearance.Options.UseFont = true;
            this.btn_Dong.Image = ((System.Drawing.Image)(resources.GetObject("btn_Dong.Image")));
            this.btn_Dong.Location = new System.Drawing.Point(936, 12);
            this.btn_Dong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Dong.Name = "btn_Dong";
            this.btn_Dong.Size = new System.Drawing.Size(96, 22);
            this.btn_Dong.StyleController = this.layoutControl1;
            this.btn_Dong.TabIndex = 9;
            this.btn_Dong.Text = "Đóng";
            this.btn_Dong.Click += new System.EventHandler(this.btn_Dong_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btn_Dong;
            this.layoutControlItem5.Location = new System.Drawing.Point(924, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(100, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(100, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // NGT02K040_MoBenhAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 491);
            this.Controls.Add(this.layoutControl1);
            this.Name = "NGT02K040_MoBenhAn";
            this.ShowIcon = false;
            this.Text = "Mở bệnh án";
            this.Load += new System.EventHandler(this.NGT02K040_MoBenhAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnMoBA;
        private UserControl.ucGridview ucGrid_DsBA;
        private UserControl.ucComboBox cboKhoaPhong;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl lblTenBN;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.SimpleButton btn_Dong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}