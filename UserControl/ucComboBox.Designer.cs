namespace VNPT.HIS.UserControl
{
    partial class ucComboBox
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelStr = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelStr);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(115, 20);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(71, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Padding = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.labelControl2.Size = new System.Drawing.Size(20, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "(*)";
            // 
            // labelStr
            // 
            this.labelStr.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelStr.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelStr.Location = new System.Drawing.Point(0, 0);
            this.labelStr.Name = "labelStr";
            this.labelStr.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.labelStr.Size = new System.Drawing.Size(71, 17);
            this.labelStr.TabIndex = 0;
            this.labelStr.Text = "ucComboBox";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lookUpEdit1.Location = new System.Drawing.Point(115, 0);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.lookUpEdit1.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lookUpEdit1.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.ShowFooter = false;
            this.lookUpEdit1.Size = new System.Drawing.Size(185, 20);
            this.lookUpEdit1.TabIndex = 1;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            this.lookUpEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lookUpEdit1_KeyDown);
            // 
            // ucComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucComboBox";
            this.Size = new System.Drawing.Size(300, 20);
            this.Load += new System.EventHandler(this.ucSearchLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelStr;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
    }
}
