namespace VNPT.HIS.QTHethong
{
    partial class DMC54_PHONGHIENTHI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DMC54_PHONGHIENTHI));
            this.layout_DMC54 = new DevExpress.XtraLayout.LayoutControl();
            this.panel_Button = new DevExpress.XtraEditors.PanelControl();
            this.btn_Dong = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Luu = new DevExpress.XtraEditors.SimpleButton();
            this.label_ThongBao = new DevExpress.XtraEditors.LabelControl();
            this.layout_Group = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layout_ThongBao = new DevExpress.XtraLayout.LayoutControlItem();
            this.layout_Button = new DevExpress.XtraLayout.LayoutControlItem();
            this.ucgview_DSPHT = new VNPT.HIS.UserControl.ucGridview();
            this.layout_DSPHT = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layout_DMC54)).BeginInit();
            this.layout_DMC54.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).BeginInit();
            this.panel_Button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Group)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_ThongBao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_DSPHT)).BeginInit();
            this.SuspendLayout();
            // 
            // layout_DMC54
            // 
            this.layout_DMC54.Controls.Add(this.ucgview_DSPHT);
            this.layout_DMC54.Controls.Add(this.panel_Button);
            this.layout_DMC54.Controls.Add(this.label_ThongBao);
            this.layout_DMC54.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout_DMC54.Location = new System.Drawing.Point(0, 0);
            this.layout_DMC54.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layout_DMC54.Name = "layout_DMC54";
            this.layout_DMC54.Root = this.layout_Group;
            this.layout_DMC54.Size = new System.Drawing.Size(729, 494);
            this.layout_DMC54.TabIndex = 0;
            this.layout_DMC54.Text = "layoutControl1";
            // 
            // panel_Button
            // 
            this.panel_Button.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel_Button.Controls.Add(this.btn_Dong);
            this.panel_Button.Controls.Add(this.btn_Luu);
            this.panel_Button.Location = new System.Drawing.Point(551, 12);
            this.panel_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_Button.Name = "panel_Button";
            this.panel_Button.Size = new System.Drawing.Size(166, 35);
            this.panel_Button.TabIndex = 5;
            // 
            // btn_Dong
            // 
            this.btn_Dong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Dong.Appearance.Options.UseFont = true;
            this.btn_Dong.Image = ((System.Drawing.Image)(resources.GetObject("btn_Dong.Image")));
            this.btn_Dong.Location = new System.Drawing.Point(85, 3);
            this.btn_Dong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Dong.Name = "btn_Dong";
            this.btn_Dong.Size = new System.Drawing.Size(69, 25);
            this.btn_Dong.TabIndex = 1;
            this.btn_Dong.Text = "Đóng";
            this.btn_Dong.Click += new System.EventHandler(this.btn_Dong_Click);
            // 
            // btn_Luu
            // 
            this.btn_Luu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_Luu.Appearance.Options.UseFont = true;
            this.btn_Luu.Image = ((System.Drawing.Image)(resources.GetObject("btn_Luu.Image")));
            this.btn_Luu.Location = new System.Drawing.Point(10, 3);
            this.btn_Luu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Size = new System.Drawing.Size(69, 25);
            this.btn_Luu.TabIndex = 0;
            this.btn_Luu.Text = "Lưu";
            this.btn_Luu.Click += new System.EventHandler(this.btn_Luu_Click);
            // 
            // label_ThongBao
            // 
            this.label_ThongBao.Appearance.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label_ThongBao.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.label_ThongBao.Location = new System.Drawing.Point(12, 12);
            this.label_ThongBao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label_ThongBao.Name = "label_ThongBao";
            this.label_ThongBao.Size = new System.Drawing.Size(535, 27);
            this.label_ThongBao.StyleController = this.layout_DMC54;
            this.label_ThongBao.TabIndex = 4;
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
            this.layout_ThongBao,
            this.layout_Button,
            this.layout_DSPHT});
            this.layout_Group.Location = new System.Drawing.Point(0, 0);
            this.layout_Group.Name = "layout_Group";
            this.layout_Group.OptionsItemText.TextToControlDistance = 4;
            this.layout_Group.Size = new System.Drawing.Size(729, 494);
            this.layout_Group.TextVisible = false;
            // 
            // layout_ThongBao
            // 
            this.layout_ThongBao.Control = this.label_ThongBao;
            this.layout_ThongBao.Location = new System.Drawing.Point(0, 0);
            this.layout_ThongBao.Name = "layout_ThongBao";
            this.layout_ThongBao.Size = new System.Drawing.Size(539, 39);
            this.layout_ThongBao.TextSize = new System.Drawing.Size(0, 0);
            this.layout_ThongBao.TextVisible = false;
            // 
            // layout_Button
            // 
            this.layout_Button.Control = this.panel_Button;
            this.layout_Button.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layout_Button.Location = new System.Drawing.Point(539, 0);
            this.layout_Button.MaxSize = new System.Drawing.Size(170, 39);
            this.layout_Button.MinSize = new System.Drawing.Size(170, 39);
            this.layout_Button.Name = "layout_Button";
            this.layout_Button.Size = new System.Drawing.Size(170, 39);
            this.layout_Button.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layout_Button.TextSize = new System.Drawing.Size(0, 0);
            this.layout_Button.TextVisible = false;
            // 
            // ucgview_DSPHT
            // 
            this.ucgview_DSPHT.caption = "DANH SÁCH PHÒNG HIỂN THỊ";
            this.ucgview_DSPHT.Location = new System.Drawing.Point(12, 51);
            this.ucgview_DSPHT.Name = "ucgview_DSPHT";
            this.ucgview_DSPHT.SelectedRow = null;
            this.ucgview_DSPHT.Size = new System.Drawing.Size(705, 431);
            this.ucgview_DSPHT.TabIndex = 6;
            // 
            // layout_DSPHT
            // 
            this.layout_DSPHT.Control = this.ucgview_DSPHT;
            this.layout_DSPHT.Location = new System.Drawing.Point(0, 39);
            this.layout_DSPHT.Name = "layout_DSPHT";
            this.layout_DSPHT.Size = new System.Drawing.Size(709, 435);
            this.layout_DSPHT.TextSize = new System.Drawing.Size(0, 0);
            this.layout_DSPHT.TextVisible = false;
            // 
            // DMC54_PHONGHIENTHI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 494);
            this.Controls.Add(this.layout_DMC54);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DMC54_PHONGHIENTHI";
            this.ShowIcon = false;
            this.Text = "Phòng hiển thị";
            this.Load += new System.EventHandler(this.DMC54_PHONGHIENTHI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layout_DMC54)).EndInit();
            this.layout_DMC54.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel_Button)).EndInit();
            this.panel_Button.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layout_Group)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_ThongBao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layout_DSPHT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layout_DMC54;
        private DevExpress.XtraLayout.LayoutControlGroup layout_Group;
        private UserControl.ucGridview ucgview_DSPHT;
        private DevExpress.XtraEditors.PanelControl panel_Button;
        private DevExpress.XtraEditors.SimpleButton btn_Luu;
        private DevExpress.XtraEditors.LabelControl label_ThongBao;
        private DevExpress.XtraLayout.LayoutControlItem layout_ThongBao;
        private DevExpress.XtraLayout.LayoutControlItem layout_Button;
        private DevExpress.XtraLayout.LayoutControlItem layout_DSPHT;
        private DevExpress.XtraEditors.SimpleButton btn_Dong;
    }
}