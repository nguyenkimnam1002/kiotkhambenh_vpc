namespace KIOS_HIS.LVVPC
{
    partial class LVVPC_Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LVVPC_Setting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelHeader = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveSetting = new System.Windows.Forms.Button();
            this.cbInSTT = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanelHeader);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 309);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current setting";
            // 
            // tableLayoutPanelHeader
            // 
            this.tableLayoutPanelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelHeader.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanelHeader.ColumnCount = 2;
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHeader.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanelHeader.Controls.Add(this.cbInSTT, 1, 0);
            this.tableLayoutPanelHeader.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanelHeader.Name = "tableLayoutPanelHeader";
            this.tableLayoutPanelHeader.RowCount = 2;
            this.tableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelHeader.Size = new System.Drawing.Size(764, 279);
            this.tableLayoutPanelHeader.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(96, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cài đặt in";
            // 
            // btnSaveSetting
            // 
            this.btnSaveSetting.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveSetting.Image")));
            this.btnSaveSetting.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnSaveSetting.Location = new System.Drawing.Point(680, 327);
            this.btnSaveSetting.Name = "btnSaveSetting";
            this.btnSaveSetting.Size = new System.Drawing.Size(108, 30);
            this.btnSaveSetting.TabIndex = 4;
            this.btnSaveSetting.Text = "Lưu cài đặt   ";
            this.btnSaveSetting.UseVisualStyleBackColor = true;
            this.btnSaveSetting.Click += new System.EventHandler(this.btnSaveSetting_Click);
            // 
            // cbInSTT
            // 
            this.cbInSTT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.cbInSTT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbInSTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbInSTT.Location = new System.Drawing.Point(171, 5);
            this.cbInSTT.Name = "cbInSTT";
            this.cbInSTT.Size = new System.Drawing.Size(180, 29);
            this.cbInSTT.TabIndex = 1;
            this.cbInSTT.Text = "In số thứ tự";
            this.cbInSTT.UseVisualStyleBackColor = true;
            // 
            // LVVPC_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 371);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSaveSetting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LVVPC_Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cài đặt hiển thị";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanelHeader.ResumeLayout(false);
            this.tableLayoutPanelHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveSetting;
        private System.Windows.Forms.CheckBox cbInSTT;
    }
}