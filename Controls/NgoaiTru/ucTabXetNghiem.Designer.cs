namespace VNPT.HIS.Controls.NgoaiTru
{
    partial class ucTabXetNghiem
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucGrid_DSXetNghiem = new VNPT.HIS.UserControl.ucGridview();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.ucGrid_DichVuChiDinh = new VNPT.HIS.UserControl.ucGridview();
            this.tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.ucGrid_KQXetNghiem = new VNPT.HIS.UserControl.ucGridview();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            this.tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucGrid_DSXetNghiem);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(879, 300);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ucGrid_DSXetNghiem
            // 
            this.ucGrid_DSXetNghiem.caption = "Danh sách xét nghiệm";
            this.ucGrid_DSXetNghiem.Location = new System.Drawing.Point(7, 2);
            this.ucGrid_DSXetNghiem.Name = "ucGrid_DSXetNghiem";
            this.ucGrid_DSXetNghiem.Size = new System.Drawing.Size(865, 296);
            this.ucGrid_DSXetNghiem.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(879, 300);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucGrid_DSXetNghiem;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(869, 300);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.tabPane1);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 300);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(879, 341);
            this.layoutControl2.TabIndex = 1;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // tabPane1
            // 
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Controls.Add(this.tabNavigationPage2);
            this.tabPane1.Location = new System.Drawing.Point(7, 2);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1,
            this.tabNavigationPage2});
            this.tabPane1.RegularSize = new System.Drawing.Size(865, 337);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.Size = new System.Drawing.Size(865, 337);
            this.tabPane1.TabIndex = 4;
            this.tabPane1.Text = "tabPane1";
            this.tabPane1.SelectedPageChanged += new DevExpress.XtraBars.Navigation.SelectedPageChangedEventHandler(this.tabPane1_SelectedPageChanged);
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Caption = "Dịch vụ chỉ định";
            this.tabNavigationPage1.Controls.Add(this.ucGrid_DichVuChiDinh);
            this.tabNavigationPage1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Size = new System.Drawing.Size(847, 292);
            // 
            // ucGrid_DichVuChiDinh
            // 
            this.ucGrid_DichVuChiDinh.caption = "";
            this.ucGrid_DichVuChiDinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGrid_DichVuChiDinh.Location = new System.Drawing.Point(0, 0);
            this.ucGrid_DichVuChiDinh.Name = "ucGrid_DichVuChiDinh";
            this.ucGrid_DichVuChiDinh.Size = new System.Drawing.Size(847, 292);
            this.ucGrid_DichVuChiDinh.TabIndex = 1;
            // 
            // tabNavigationPage2
            // 
            this.tabNavigationPage2.Caption = "Kết quả xét nghiệm";
            this.tabNavigationPage2.Controls.Add(this.ucGrid_KQXetNghiem);
            this.tabNavigationPage2.Name = "tabNavigationPage2";
            this.tabNavigationPage2.Size = new System.Drawing.Size(847, 292);
            // 
            // ucGrid_KQXetNghiem
            // 
            this.ucGrid_KQXetNghiem.caption = "";
            this.ucGrid_KQXetNghiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGrid_KQXetNghiem.Location = new System.Drawing.Point(0, 0);
            this.ucGrid_KQXetNghiem.Name = "ucGrid_KQXetNghiem";
            this.ucGrid_KQXetNghiem.Size = new System.Drawing.Size(847, 292);
            this.ucGrid_KQXetNghiem.TabIndex = 0;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(879, 341);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tabPane1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(869, 341);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucTabXetNghiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl2);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucTabXetNghiem";
            this.Size = new System.Drawing.Size(879, 641);
            this.Load += new System.EventHandler(this.ucTabXetNghiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            this.tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.ucGridview ucGrid_DSXetNghiem;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private UserControl.ucGridview ucGrid_KQXetNghiem;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private UserControl.ucGridview ucGrid_DichVuChiDinh;
    }
}
