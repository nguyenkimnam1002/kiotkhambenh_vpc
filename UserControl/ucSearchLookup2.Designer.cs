namespace VNPT.HIS.UserControl
{
    partial class ucSearchLookup2
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
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl_Textbox = new DevExpress.XtraEditors.PanelControl();
            this.panelControl_Search = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Textbox)).BeginInit();
            this.panelControl_Textbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Search)).BeginInit();
            this.panelControl_Search.SuspendLayout();
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
            this.panelControl1.Size = new System.Drawing.Size(80, 68);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(64, 0);
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
            this.labelStr.Size = new System.Drawing.Size(64, 17);
            this.labelStr.TabIndex = 0;
            this.labelStr.Text = "ucSearch_2";
            // 
            // searchLookUpEdit1
            // 
            this.searchLookUpEdit1.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchLookUpEdit1.Location = new System.Drawing.Point(0, 0);
            this.searchLookUpEdit1.Name = "searchLookUpEdit1";
            this.searchLookUpEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.searchLookUpEdit1.Properties.Appearance.Options.UseFont = true;
            this.searchLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchLookUpEdit1.Properties.NullText = "";
            this.searchLookUpEdit1.Properties.ShowClearButton = false;
            this.searchLookUpEdit1.Properties.ShowFooter = false;
            this.searchLookUpEdit1.Properties.View = this.searchLookUpEdit1View;
            this.searchLookUpEdit1.Size = new System.Drawing.Size(100, 20);
            this.searchLookUpEdit1.TabIndex = 0;
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
            // textEdit1
            // 
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit1.Location = new System.Drawing.Point(0, 0);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.textEdit1.Properties.Appearance.Options.UseFont = true;
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(250, 20);
            this.textEdit1.TabIndex = 1;
            this.textEdit1.VisibleChanged += new System.EventHandler(this.textEdit1_VisibleChanged);
            this.textEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyDown);
            this.textEdit1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textEdit1_PreviewKeyDown);
            // 
            // memoEdit1
            // 
            this.memoEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit1.Location = new System.Drawing.Point(0, 0);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.memoEdit1.Properties.Appearance.Options.UseFont = true;
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new System.Drawing.Size(250, 68);
            this.memoEdit1.TabIndex = 2;
            this.memoEdit1.Visible = false;
            this.memoEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.memoEdit1_KeyDown);
            this.memoEdit1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.memoEdit1_PreviewKeyDown);
            // 
            // btnReset
            // 
            this.btnReset.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnReset.Appearance.Options.UseFont = true;
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReset.Location = new System.Drawing.Point(0, 20);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnEdit.Appearance.Options.UseFont = true;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEdit.Location = new System.Drawing.Point(0, 43);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 23);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panelControl_Textbox
            // 
            this.panelControl_Textbox.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl_Textbox.Controls.Add(this.textEdit1);
            this.panelControl_Textbox.Controls.Add(this.memoEdit1);
            this.panelControl_Textbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl_Textbox.Location = new System.Drawing.Point(180, 0);
            this.panelControl_Textbox.Name = "panelControl_Textbox";
            this.panelControl_Textbox.Size = new System.Drawing.Size(250, 68);
            this.panelControl_Textbox.TabIndex = 2;
            // 
            // panelControl_Search
            // 
            this.panelControl_Search.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl_Search.Controls.Add(this.btnEdit);
            this.panelControl_Search.Controls.Add(this.btnReset);
            this.panelControl_Search.Controls.Add(this.searchLookUpEdit1);
            this.panelControl_Search.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl_Search.Location = new System.Drawing.Point(80, 0);
            this.panelControl_Search.Name = "panelControl_Search";
            this.panelControl_Search.Size = new System.Drawing.Size(100, 68);
            this.panelControl_Search.TabIndex = 1;
            // 
            // ucSearchLookup2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl_Textbox);
            this.Controls.Add(this.panelControl_Search);
            this.Controls.Add(this.panelControl1);
            this.Name = "ucSearchLookup2";
            this.Size = new System.Drawing.Size(430, 68);
            this.Load += new System.EventHandler(this.ucSearchLookup2_Load);
            this.EnabledChanged += new System.EventHandler(this.ucSearchLookup2_EnabledChanged);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Textbox)).EndInit();
            this.panelControl_Textbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl_Search)).EndInit();
            this.panelControl_Search.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1; 
        public DevExpress.XtraEditors.SearchLookUpEdit searchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelStr;
        public DevExpress.XtraEditors.TextEdit textEdit1;
        public DevExpress.XtraEditors.MemoEdit memoEdit1;
        public DevExpress.XtraEditors.SimpleButton btnReset;
        public DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.PanelControl panelControl_Textbox;
        private DevExpress.XtraEditors.PanelControl panelControl_Search;
    }
}
