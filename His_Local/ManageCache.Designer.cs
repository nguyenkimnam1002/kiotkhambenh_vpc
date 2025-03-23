namespace MainForm
{
    partial class ManageCache
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.XoaDL = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.F5 = new DevExpress.XtraEditors.SimpleButton();
            this.XOA = new DevExpress.XtraEditors.SimpleButton();
            this.ucValue = new VNPT.HIS.UserControl.ucGridview();
            this.ucBang = new VNPT.HIS.UserControl.ucGridview();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.XoaDL);
            this.panelControl1.Controls.Add(this.simpleButton5);
            this.panelControl1.Controls.Add(this.F5);
            this.panelControl1.Controls.Add(this.XOA);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(312, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(102, 508);
            this.panelControl1.TabIndex = 2;
            // 
            // XoaDL
            // 
            this.XoaDL.Location = new System.Drawing.Point(7, 21);
            this.XoaDL.Name = "XoaDL";
            this.XoaDL.Size = new System.Drawing.Size(75, 23);
            this.XoaDL.TabIndex = 5;
            this.XoaDL.Text = "Xóa dữ liệu";
            this.XoaDL.Click += new System.EventHandler(this.XoaDL_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(7, 173);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(75, 23);
            this.simpleButton5.TabIndex = 4;
            this.simpleButton5.Text = "tạo cache mới";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // F5
            // 
            this.F5.Location = new System.Drawing.Point(7, 117);
            this.F5.Name = "F5";
            this.F5.Size = new System.Drawing.Size(75, 23);
            this.F5.TabIndex = 3;
            this.F5.Text = "F5";
            this.F5.Click += new System.EventHandler(this.F5_Click);
            // 
            // XOA
            // 
            this.XOA.Location = new System.Drawing.Point(7, 66);
            this.XOA.Name = "XOA";
            this.XOA.Size = new System.Drawing.Size(75, 23);
            this.XOA.TabIndex = 2;
            this.XOA.Text = "Xóa bảng";
            this.XOA.Click += new System.EventHandler(this.XOA_Click);
            // 
            // ucValue
            // 
            this.ucValue.caption = "";
            this.ucValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucValue.Location = new System.Drawing.Point(414, 0);
            this.ucValue.Name = "ucValue";
            this.ucValue.SelectedRow = null;
            this.ucValue.Size = new System.Drawing.Size(570, 508);
            this.ucValue.TabIndex = 1;
            // 
            // ucBang
            // 
            this.ucBang.caption = "Bảng";
            this.ucBang.Dock = System.Windows.Forms.DockStyle.Left;
            this.ucBang.Location = new System.Drawing.Point(0, 0);
            this.ucBang.Name = "ucBang";
            this.ucBang.SelectedRow = null;
            this.ucBang.Size = new System.Drawing.Size(312, 508);
            this.ucBang.TabIndex = 0;
            this.ucBang.Load += new System.EventHandler(this.ucGridview1_Load);
            // 
            // ManageCache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 508);
            this.Controls.Add(this.ucValue);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.ucBang);
            this.Name = "ManageCache";
            this.Text = "Quản lý Cache";
            this.Load += new System.EventHandler(this.ManageCache_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private VNPT.HIS.UserControl.ucGridview ucBang;
        private VNPT.HIS.UserControl.ucGridview ucValue;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton F5;
        private DevExpress.XtraEditors.SimpleButton XOA;
        private DevExpress.XtraEditors.SimpleButton XoaDL;
    }
}