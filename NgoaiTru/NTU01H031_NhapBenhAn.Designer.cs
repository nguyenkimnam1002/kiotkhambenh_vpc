namespace VNPT.HIS.NgoaiTru
{
    partial class NTU01H031_NhapBenhAn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NTU01H031_NhapBenhAn));
            this.layout_NTU01H031 = new DevExpress.XtraLayout.LayoutControl();
            this.etext_SoVaoVien = new DevExpress.XtraEditors.TextEdit();
            this.panel_Button = new DevExpress.XtraEditors.PanelControl();
            this.btn_Dong = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Luu = new DevExpress.XtraEditors.SimpleButton();
            this.uccbox_BenhAn = new VNPT.HIS.UserControl.ucComboBox();
            this.layout_Group = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layout_BenhAn = new DevExpress.XtraLayout.LayoutControlItem();
            this.layout_Button = new DevExpress.XtraLayout.LayoutControlItem();
            this.layout_SoVaoVien = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layout_NTU01H031)).BeginInit();
            this.layout_NTU01H031.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.etext_SoVaoVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).BeginInit();
            this.panel_Button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_BenhAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_SoVaoVien)).BeginInit();
            this.SuspendLayout();
            // 
            // layout_NTU01H031
            // 
            this.layout_NTU01H031.Controls.Add(this.etext_SoVaoVien);
            this.layout_NTU01H031.Controls.Add(this.panel_Button);
            this.layout_NTU01H031.Controls.Add(this.uccbox_BenhAn);
            this.layout_NTU01H031.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_NTU01H031.Location = new System.Drawing.Point(0, 0);
            this.layout_NTU01H031.Name = "layout_NTU01H031";
            this.layout_NTU01H031.Root = this.layout_Group;
            this.layout_NTU01H031.Size = new System.Drawing.Size(526, 208);
            this.layout_NTU01H031.TabIndex = 0;
            this.layout_NTU01H031.Text = "layoutControl1";
            // 
            // etext_SoVaoVien
            // 
            this.etext_SoVaoVien.Location = new System.Drawing.Point(124, 52);
            this.etext_SoVaoVien.Name = "etext_SoVaoVien";
            this.etext_SoVaoVien.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.etext_SoVaoVien.Properties.Appearance.Options.UseFont = true;
            this.etext_SoVaoVien.Size = new System.Drawing.Size(384, 24);
            this.etext_SoVaoVien.StyleController = this.layout_NTU01H031;
            this.etext_SoVaoVien.TabIndex = 7;
            // 
            // panel_Button
            // 
            this.panel_Button.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel_Button.Controls.Add(this.btn_Dong);
            this.panel_Button.Controls.Add(this.btn_Luu);
            this.panel_Button.Location = new System.Drawing.Point(182, 84);
            this.panel_Button.Name = "panel_Button";
            this.panel_Button.Size = new System.Drawing.Size(162, 34);
            this.panel_Button.TabIndex = 6;
            // 
            // btn_Dong
            // 
            this.btn_Dong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Dong.Appearance.Options.UseFont = true;
            this.btn_Dong.Image = ((System.Drawing.Image)(resources.GetObject("btn_Dong.Image")));
            this.btn_Dong.Location = new System.Drawing.Point(84, 3);
            this.btn_Dong.Name = "btn_Dong";
            this.btn_Dong.Size = new System.Drawing.Size(75, 28);
            this.btn_Dong.TabIndex = 2;
            this.btn_Dong.Text = "Đóng";
            this.btn_Dong.Click += new System.EventHandler(this.btn_Dong_Click);
            // 
            // btn_Luu
            // 
            this.btn_Luu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Luu.Appearance.Options.UseFont = true;
            this.btn_Luu.Image = ((System.Drawing.Image)(resources.GetObject("btn_Luu.Image")));
            this.btn_Luu.Location = new System.Drawing.Point(3, 3);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Size = new System.Drawing.Size(75, 28);
            this.btn_Luu.TabIndex = 1;
            this.btn_Luu.Text = "Lưu";
            this.btn_Luu.Click += new System.EventHandler(this.btn_Luu_Click);
            // 
            // uccbox_BenhAn
            // 
            this.uccbox_BenhAn.Caption = "Bệnh án";
            this.uccbox_BenhAn.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.uccbox_BenhAn.CaptionValidate = true;
            this.uccbox_BenhAn.CaptionWidth = 106;
            this.uccbox_BenhAn.Location = new System.Drawing.Point(18, 18);
            this.uccbox_BenhAn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uccbox_BenhAn.Name = "uccbox_BenhAn";
            this.uccbox_BenhAn.SelectDataRowView = null;
            this.uccbox_BenhAn.SelectIndex = -1;
            this.uccbox_BenhAn.SelectText = "";
            this.uccbox_BenhAn.SelectValue = "";
            this.uccbox_BenhAn.Size = new System.Drawing.Size(490, 24);
            this.uccbox_BenhAn.TabIndex = 4;
            // 
            // layout_Group
            // 
            this.layout_Group.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.layout_Group.AppearanceGroup.Options.UseFont = true;
            this.layout_Group.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.layout_Group.AppearanceItemCaption.Options.UseFont = true;
            this.layout_Group.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layout_Group.GroupBordersVisible = false;
            this.layout_Group.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layout_BenhAn,
            this.layout_Button,
            this.layout_SoVaoVien});
            this.layout_Group.Location = new System.Drawing.Point(0, 0);
            this.layout_Group.Name = "layout_Group";
            this.layout_Group.OptionsItemText.TextToControlDistance = 4;
            this.layout_Group.Size = new System.Drawing.Size(526, 208);
            this.layout_Group.TextVisible = false;
            // 
            // layout_BenhAn
            // 
            this.layout_BenhAn.Control = this.uccbox_BenhAn;
            this.layout_BenhAn.Location = new System.Drawing.Point(0, 0);
            this.layout_BenhAn.MaxSize = new System.Drawing.Size(0, 34);
            this.layout_BenhAn.MinSize = new System.Drawing.Size(11, 34);
            this.layout_BenhAn.Name = "layout_BenhAn";
            this.layout_BenhAn.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layout_BenhAn.Size = new System.Drawing.Size(500, 34);
            this.layout_BenhAn.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layout_BenhAn.TextSize = new System.Drawing.Size(0, 0);
            this.layout_BenhAn.TextVisible = false;
            // 
            // layout_Button
            // 
            this.layout_Button.Control = this.panel_Button;
            this.layout_Button.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layout_Button.Location = new System.Drawing.Point(0, 68);
            this.layout_Button.MaxSize = new System.Drawing.Size(168, 40);
            this.layout_Button.MinSize = new System.Drawing.Size(168, 40);
            this.layout_Button.Name = "layout_Button";
            this.layout_Button.Size = new System.Drawing.Size(500, 114);
            this.layout_Button.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layout_Button.TextSize = new System.Drawing.Size(0, 0);
            this.layout_Button.TextVisible = false;
            // 
            // layout_SoVaoVien
            // 
            this.layout_SoVaoVien.AllowHtmlStringInCaption = true;
            this.layout_SoVaoVien.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.layout_SoVaoVien.AppearanceItemCaption.Options.UseFont = true;
            this.layout_SoVaoVien.Control = this.etext_SoVaoVien;
            this.layout_SoVaoVien.Location = new System.Drawing.Point(0, 34);
            this.layout_SoVaoVien.Name = "layout_SoVaoVien";
            this.layout_SoVaoVien.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layout_SoVaoVien.Size = new System.Drawing.Size(500, 34);
            this.layout_SoVaoVien.Text = "Số vào viện<color=red> (*)";
            this.layout_SoVaoVien.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layout_SoVaoVien.TextSize = new System.Drawing.Size(101, 18);
            this.layout_SoVaoVien.TextToControlDistance = 5;
            // 
            // NTU01H031_NhapBenhAn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 208);
            this.Controls.Add(this.layout_NTU01H031);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NTU01H031_NhapBenhAn";
            this.ShowIcon = false;
            this.Text = "NTU01H031_NhapBenhAn";
            this.Load += new System.EventHandler(this.NTU01H031_NhapBenhAn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layout_NTU01H031)).EndInit();
            this.layout_NTU01H031.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.etext_SoVaoVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).EndInit();
            this.panel_Button.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_BenhAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_SoVaoVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layout_NTU01H031;
        private DevExpress.XtraEditors.TextEdit etext_SoVaoVien;
        private DevExpress.XtraEditors.PanelControl panel_Button;
        private DevExpress.XtraEditors.SimpleButton btn_Dong;
        private DevExpress.XtraEditors.SimpleButton btn_Luu;
        private UserControl.ucComboBox uccbox_BenhAn;
        private DevExpress.XtraLayout.LayoutControlGroup layout_Group;
        private DevExpress.XtraLayout.LayoutControlItem layout_BenhAn;
        private DevExpress.XtraLayout.LayoutControlItem layout_Button;
        private DevExpress.XtraLayout.LayoutControlItem layout_SoVaoVien;
    }
}