namespace VNPT.HIS.Controls.NgoaiTru
{
    partial class ucTabThuoc
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
            this.ucGrid_DanhSach = new VNPT.HIS.UserControl.ucGridview();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.ucGrid_ChiTiet = new VNPT.HIS.UserControl.ucGridview();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucGrid_DanhSach);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(761, 300);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ucGrid_DanhSach
            // 
            this.ucGrid_DanhSach.caption = "Danh sách phiếu thuốc";
            this.ucGrid_DanhSach.Location = new System.Drawing.Point(7, 2);
            this.ucGrid_DanhSach.Name = "ucGrid_DanhSach";
            this.ucGrid_DanhSach.Size = new System.Drawing.Size(747, 296);
            this.ucGrid_DanhSach.TabIndex = 0;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(761, 300);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucGrid_DanhSach;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(751, 300);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.ucGrid_ChiTiet);
            this.layoutControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl3.Location = new System.Drawing.Point(0, 300);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(761, 223);
            this.layoutControl3.TabIndex = 3;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // ucGrid_ChiTiet
            // 
            this.ucGrid_ChiTiet.caption = "Danh sách chi tiết phiếu thuốc";
            this.ucGrid_ChiTiet.Location = new System.Drawing.Point(7, 2);
            this.ucGrid_ChiTiet.Name = "ucGrid_ChiTiet";
            this.ucGrid_ChiTiet.Size = new System.Drawing.Size(747, 219);
            this.ucGrid_ChiTiet.TabIndex = 4;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(761, 223);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ucGrid_ChiTiet;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(751, 223);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ucTabThuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl3);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucTabThuoc";
            this.Size = new System.Drawing.Size(761, 523);
            this.Load += new System.EventHandler(this.ucTabThuoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ucGridview ucGrid_DanhSach;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private UserControl.ucGridview ucGrid_ChiTiet;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;


    }
}
