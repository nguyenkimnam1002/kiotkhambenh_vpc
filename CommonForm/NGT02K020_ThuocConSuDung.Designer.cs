namespace VNPT.HIS.CommonForm
{
    partial class NGT02K020_ThuocConSuDung
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
            this.layout_NGT02K020 = new DevExpress.XtraLayout.LayoutControl();
            this.panel_Button = new DevExpress.XtraEditors.PanelControl();
            this.btn_Dong = new DevExpress.XtraEditors.SimpleButton();
            this.ucgview_ThuocConSD = new VNPT.HIS.UserControl.ucGridview();
            this.layout_Group = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layout_ThuocConSD = new DevExpress.XtraLayout.LayoutControlItem();
            this.layout_Button = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layout_NGT02K020)).BeginInit();
            this.layout_NGT02K020.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).BeginInit();
            this.panel_Button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_ThuocConSD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Button)).BeginInit();
            this.SuspendLayout();
            // 
            // layout_NGT02K020
            // 
            this.layout_NGT02K020.Controls.Add(this.panel_Button);
            this.layout_NGT02K020.Controls.Add(this.ucgview_ThuocConSD);
            this.layout_NGT02K020.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_NGT02K020.Location = new System.Drawing.Point(0, 0);
            this.layout_NGT02K020.Name = "layout_NGT02K020";
            this.layout_NGT02K020.Root = this.layout_Group;
            this.layout_NGT02K020.Size = new System.Drawing.Size(954, 525);
            this.layout_NGT02K020.TabIndex = 0;
            this.layout_NGT02K020.Text = "layoutControl1";
            // 
            // panel_Button
            // 
            this.panel_Button.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel_Button.Controls.Add(this.btn_Dong);
            this.panel_Button.Location = new System.Drawing.Point(435, 479);
            this.panel_Button.Name = "panel_Button";
            this.panel_Button.Size = new System.Drawing.Size(83, 30);
            this.panel_Button.TabIndex = 5;
            // 
            // btn_Dong
            // 
            this.btn_Dong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Dong.Appearance.Options.UseFont = true;
            this.btn_Dong.Location = new System.Drawing.Point(1, 1);
            this.btn_Dong.Name = "btn_Dong";
            this.btn_Dong.Size = new System.Drawing.Size(81, 28);
            this.btn_Dong.TabIndex = 0;
            this.btn_Dong.Text = "Đóng";
            this.btn_Dong.Click += new System.EventHandler(this.btn_Dong_Click);
            // 
            // ucgview_ThuocConSD
            // 
            this.ucgview_ThuocConSD.caption = "";
            this.ucgview_ThuocConSD.Location = new System.Drawing.Point(16, 16);
            this.ucgview_ThuocConSD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucgview_ThuocConSD.Name = "ucgview_ThuocConSD";
            this.ucgview_ThuocConSD.SelectedRow = null;
            this.ucgview_ThuocConSD.Size = new System.Drawing.Size(922, 457);
            this.ucgview_ThuocConSD.TabIndex = 4;
            // 
            // layout_Group
            // 
            this.layout_Group.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.layout_Group.AppearanceGroup.Options.UseFont = true;
            this.layout_Group.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.layout_Group.AppearanceItemCaption.Options.UseFont = true;
            this.layout_Group.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layout_Group.GroupBordersVisible = false;
            this.layout_Group.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layout_ThuocConSD,
            this.layout_Button});
            this.layout_Group.Location = new System.Drawing.Point(0, 0);
            this.layout_Group.Name = "layout_Group";
            this.layout_Group.OptionsItemText.TextToControlDistance = 4;
            this.layout_Group.Size = new System.Drawing.Size(954, 525);
            this.layout_Group.TextVisible = false;
            // 
            // layout_ThuocConSD
            // 
            this.layout_ThuocConSD.Control = this.ucgview_ThuocConSD;
            this.layout_ThuocConSD.Location = new System.Drawing.Point(0, 0);
            this.layout_ThuocConSD.Name = "layout_ThuocConSD";
            this.layout_ThuocConSD.Size = new System.Drawing.Size(928, 463);
            this.layout_ThuocConSD.TextSize = new System.Drawing.Size(0, 0);
            this.layout_ThuocConSD.TextVisible = false;
            // 
            // layout_Button
            // 
            this.layout_Button.Control = this.panel_Button;
            this.layout_Button.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layout_Button.Location = new System.Drawing.Point(0, 463);
            this.layout_Button.MaxSize = new System.Drawing.Size(89, 36);
            this.layout_Button.MinSize = new System.Drawing.Size(89, 36);
            this.layout_Button.Name = "layout_Button";
            this.layout_Button.Size = new System.Drawing.Size(928, 36);
            this.layout_Button.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layout_Button.TextSize = new System.Drawing.Size(0, 0);
            this.layout_Button.TextVisible = false;
            // 
            // NGT02K020_ThuocConSuDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 525);
            this.Controls.Add(this.layout_NGT02K020);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NGT02K020_ThuocConSuDung";
            this.ShowIcon = false;
            this.Text = "Thuốc còn sử dụng ";
            this.Load += new System.EventHandler(this.NGT02K020_ThuocConSuDung_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layout_NGT02K020)).EndInit();
            this.layout_NGT02K020.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).EndInit();
            this.panel_Button.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_ThuocConSD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Button)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layout_NGT02K020;
        private DevExpress.XtraLayout.LayoutControlGroup layout_Group;
        private UserControl.ucGridview ucgview_ThuocConSD;
        private DevExpress.XtraLayout.LayoutControlItem layout_ThuocConSD;
        private DevExpress.XtraEditors.PanelControl panel_Button;
        private DevExpress.XtraEditors.SimpleButton btn_Dong;
        private DevExpress.XtraLayout.LayoutControlItem layout_Button;
    }
}