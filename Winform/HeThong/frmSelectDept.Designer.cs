namespace VNPT.HIS.MainForm.HeThong
{
    partial class frmSelectDept
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectDept));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnSubmit = new DevExpress.XtraEditors.SimpleButton();
            this.ucSearchPhong = new VNPT.HIS.UserControl.ucSearchLookup();
            this.ucSearchKhoa = new VNPT.HIS.UserControl.ucSearchLookup();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupControl1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(398, 179);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupControl1
            // 
            this.groupControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl1.Controls.Add(this.btnDong);
            this.groupControl1.Controls.Add(this.btnSubmit);
            this.groupControl1.Controls.Add(this.ucSearchPhong);
            this.groupControl1.Controls.Add(this.ucSearchKhoa);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(2, -32);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(394, 274);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Thiết lập phòng";
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.Image")));
            this.btnDong.Location = new System.Drawing.Point(209, 157);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(75, 23);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.Appearance.Options.UseFont = true;
            this.btnSubmit.Image = ((System.Drawing.Image)(resources.GetObject("btnSubmit.Image")));
            this.btnSubmit.Location = new System.Drawing.Point(111, 157);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(82, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Thiết lập";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ucSearchPhong
            // 
            this.ucSearchPhong.Caption = "Phòng:";
            this.ucSearchPhong.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucSearchPhong.CaptionValidate = false;
            this.ucSearchPhong.CaptionWidth = 70;
            this.ucSearchPhong.findColumns = "*";
            this.ucSearchPhong.Location = new System.Drawing.Point(19, 105);
            this.ucSearchPhong.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucSearchPhong.Name = "ucSearchPhong";
            this.ucSearchPhong.SelectDataRowView = null;
            this.ucSearchPhong.SelectIndex = -1;
            this.ucSearchPhong.SelectValue = "";
            this.ucSearchPhong.Size = new System.Drawing.Size(357, 20);
            this.ucSearchPhong.TabIndex = 1;
            // 
            // ucSearchKhoa
            // 
            this.ucSearchKhoa.Caption = "Khoa:";
            this.ucSearchKhoa.CaptionDock = System.Windows.Forms.DockStyle.Left;
            this.ucSearchKhoa.CaptionValidate = false;
            this.ucSearchKhoa.CaptionWidth = 70;
            this.ucSearchKhoa.findColumns = "*";
            this.ucSearchKhoa.Location = new System.Drawing.Point(19, 59);
            this.ucSearchKhoa.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucSearchKhoa.Name = "ucSearchKhoa";
            this.ucSearchKhoa.SelectDataRowView = null;
            this.ucSearchKhoa.SelectIndex = -1;
            this.ucSearchKhoa.SelectValue = "";
            this.ucSearchKhoa.Size = new System.Drawing.Size(357, 20);
            this.ucSearchKhoa.TabIndex = 0;
            // 
            // frmSelectDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 179);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectDept";
            this.ShowIcon = false;
            this.Text = "Thiết lập phòng";
            this.Load += new System.EventHandler(this.frmSelectDept_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnSubmit;
        private UserControl.ucSearchLookup ucSearchPhong;
        private UserControl.ucSearchLookup ucSearchKhoa;
        private DevExpress.XtraEditors.SimpleButton btnDong;

    }
}