namespace VNPT.HIS.UserControl
{
    partial class ucSearchLookup
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
            this.searchLookUpEdit1 = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
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
            this.panelControl1.Size = new System.Drawing.Size(80, 20);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(50, 0);
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
            this.labelStr.Size = new System.Drawing.Size(50, 17);
            this.labelStr.TabIndex = 0;
            this.labelStr.Text = "ucSearch";
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchLookUpEdit1.Location = new System.Drawing.Point(80, 0);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1.Properties.AppearanceDisabled.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1.Properties.AppearanceDropDown.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1.Properties.AppearanceFocused.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.NullText = "";
            this.searchLookUpEdit1.Properties.ShowClearButton = false;
            this.searchLookUpEdit1.Properties.ShowFooter = false;
            this.searchLookUpEdit1.Properties.View = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(220, 20);
            this.searchLookUpEdit1.TabIndex = 1;
            this.searchLookUpEdit1.EditValueChanged += new System.EventHandler(this.searchLookUpEdit1_EditValueChanged);
            this.searchLookUpEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchLookUpEdit1_KeyDown);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            this.searchLookUpEdit1View.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1View.Appearance.ViewCaption.Options.UseFont = true;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Default;
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // ucSearchLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchLookUpEdit1);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucSearchLookup";
            this.Size = new System.Drawing.Size(300, 20);
            this.Load += new System.EventHandler(this.ucSearchLookup_Load);
            this.EnabledChanged += new System.EventHandler(this.ucSearchLookup_EnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        public DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelStr;
    }
}
