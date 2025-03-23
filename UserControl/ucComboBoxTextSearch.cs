using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.UserControl
{
    public partial class ucComboBoxTextSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public ucComboBoxTextSearch()
        {
            InitializeComponent();
        }
        private int _DefaultIndex = 0;
        private void ucSearchLookup_Load(object sender, EventArgs e)
        {
            //searchLookUpEdit1.Properties.ShowClearButton = false;
            //searchLookUpEdit1.Properties.ShowFooter = false;
            //lookUpEdit1.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;  tự động dãn theo dữ liệu
            //lookUpEdit1.Properties.ShowHeader = true; hiển thị tiêu đề của cột popup
        }
        public LookUpEdit lookUpEdit
        {
            set
            {
                lookUpEdit1 = value;
            }
            get
            {
                return lookUpEdit1;
            }
        }
        public int CaptionWidth
        {
            set
            {
                panelControl1.Width = value;
            }
            get
            {
                return panelControl1.Width;
            }
        }
        public int SearchTextWidth
        {
            set
            {
                textEdit1.Width = value;
            }
            get
            {
                return textEdit1.Width;
            }
        }
        public int SelectIndex
        {
            set
            {
                if (lookUpEdit1.Properties.DataSource != null && ((DataTable)lookUpEdit1.Properties.DataSource).Rows.Count > value)
                    lookUpEdit1.ItemIndex = value;
            }
            get
            {
                return lookUpEdit1.ItemIndex;
            }
        }
        public string SelectValue
        {
            set
            {
                lookUpEdit1.EditValue = value;
            }
            get
            {
                if (lookUpEdit1.EditValue != null) return lookUpEdit1.EditValue.ToString();
                else return "";
            }
        }
        public int DefaultIndex
        {
            set
            {
                _DefaultIndex = value;
                if (lookUpEdit1.Properties.DataSource != null && ((DataTable)lookUpEdit1.Properties.DataSource).Rows.Count > value)
                    lookUpEdit1.ItemIndex = value;
            }
            get
            {
                return _DefaultIndex;
            }
        }
        public string Caption
        {
            set
            {
                labelStr.Text = value;
            }
            get
            {
                return labelStr.Text;
            }
        }
        public bool CaptionValidate
        {
            set
            {
                labelControl2.Visible = value;
            }
            get
            {
                return labelControl2.Visible;
            }
        }
        public DockStyle CaptionDock
        {
            set
            {
                labelStr.Dock = value;
                labelControl2.Dock = value;
            }
            get
            {
                return labelStr.Dock;
            }
        }
        public string SelectText
        {
            set
            {
                lookUpEdit1.Text = value;
            }
            get
            {
                return lookUpEdit1.Text;
            }
        }
        public string SelectTextSearch
        {
            set
            {
                textEdit1.Text = value;
            }
            get
            {
                return textEdit1.Text;
            }
        }

        public string Text
        {
            set
            {
                lookUpEdit1.Text = value;
            }
            get
            {
                return lookUpEdit1.Text;
            }
        }

        public bool TextSearchReadOnly
        {
            set
            {
                textEdit1.ReadOnly = value;
            }
            get
            {
                return textEdit1.ReadOnly;
            }
        }
        public bool ReadOnly
        {
            set
            {
                textEdit1.ReadOnly = value;
                lookUpEdit1.ReadOnly = value;
            }
            get
            {
                return textEdit1.ReadOnly;
            }
        }

        private void lookUpEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)//e.KeyCode == Keys.Enter || 
            {
                lookUpEdit1.ShowPopup();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (lookUpEdit1.IsPopupOpen == false && enter_key != null) enter_key(sender, e);
            }
            //else
            //    this.ComboBoxTextSearch_KeyDown(sender, e);

            //if (!searchLookUpEdit1.IsPopupOpen) searchLookUpEdit1.ShowPopup();

            //searchLookUpEdit1View.Focus();
            //SendKeys.Send("{DOWN}");
        }
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //textEdit1.Text = lookUpEdit1.EditValue.ToString();
            object item = lookUpEdit1.Properties.GetDataSourceRowByKeyValue(lookUpEdit1.EditValue);
            // co the dung: (DataRowView)lookUpEdit1.GetSelectedDataRow()
            if (item != null && select_change != null) select_change(item, null);
        }
         
        private DataTable dtSource;
        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                DataTable table1 = dtSource.Clone();
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    if (dtSource.Rows[i][lookUpEdit1.Properties.DisplayMember].ToString().IndexOf(textEdit1.EditValue.ToString(), StringComparison.OrdinalIgnoreCase) >= 0 || textEdit1.EditValue.Equals(""))
                    {
                        DataRow toInsert = dtSource.Rows[i];
                        table1.Rows.Add(toInsert.ItemArray);
                    }
                }

                setData_Filter(table1, lookUpEdit1.Properties.ValueMember, lookUpEdit1.Properties.DisplayMember);
                //CaptionValidate = true;//???
                if (table1.Rows.Count > 0) lookUpEdit1.EditValue = table1.Rows[0][0];
                else lookUpEdit1.EditValue = "";
            }
        }

        protected EventHandler enter_key;
        public void setEvent_Enter(EventHandler eventKeyEnter)
        {
            enter_key = eventKeyEnter;
        }
        protected EventHandler select_change;
        public void setEvent(EventHandler eventChangeValue)
        {
            select_change = eventChangeValue;
        }

        public DataRowView SelectDataRowView
        {
            set
            {
            }
            get
            {
                try
                {
                    return (DataRowView)lookUpEdit1.Properties.GetDataSourceRowByKeyValue(lookUpEdit1.EditValue);
                }
                catch (Exception ex) { System.Console.WriteLine(ex.ToString()); return null; }
            }
        }
        public void clearData()
        {
            lookUpEdit1.Properties.DataSource = null;
            //textEdit1.Text = "";
        }
        public void setData(DataTable dt, int indexValue, int indexDisplay)
        {
            clearData();
            if (dt == null || dt.Rows.Count == 0) return;

            this_setData(dt, dt.Columns[indexValue].ColumnName, dt.Columns[indexDisplay].ColumnName);
        }
        public void setData(DataTable dt, string value, string display)
        {
            clearData();
            if (dt == null || dt.Rows.Count == 0) return;

            this_setData(dt, value, display);
        }
        private void this_setData(DataTable dt, string value, string display)
        { 
            CopyToSource(dt);

            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.PopulateColumns();// lệnh đổ dl ra View --> thì lệnh sau mới truy cập đc searchLookUpEdit1.Properties.Columns[0].Caption = "ID";
            lookUpEdit1.Properties.ValueMember = value;
            lookUpEdit1.Properties.DisplayMember = display;

            if (dt.Rows.Count < 15) lookUpEdit1.Properties.DropDownRows = dt.Rows.Count;
            else lookUpEdit1.Properties.DropDownRows = 8;

            setColumnAll(false);
        }
        // Hàm chỉ dùng cho việc search filter nội bộ của uc
        private void setData_Filter(DataTable dt, string value, string display)
        {
            clearData();
            if (dt == null || dt.Rows.Count == 0) return; 

            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.PopulateColumns();// lệnh đổ dl ra View --> thì lệnh sau mới truy cập đc searchLookUpEdit1.Properties.Columns[0].Caption = "ID";
            lookUpEdit1.Properties.ValueMember = value;
            lookUpEdit1.Properties.DisplayMember = display;

            if (dt.Rows.Count < 15) lookUpEdit1.Properties.DropDownRows = dt.Rows.Count;
            else lookUpEdit1.Properties.DropDownRows = 8;

            setColumnAll(false);
        }
        private void CopyToSource(DataTable dt)
        {
            dtSource = dt.Clone();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtSource.Rows.Add(dt.Rows[i].ItemArray);

            }
        }

        //public void setData(object list, string value, string display)
        //{
        //    if (list !=null)
        //    {
        //        lookUpEdit1.Properties.DataSource = list;
        //        lookUpEdit1.Properties.PopulateColumns();// lệnh đổ dl ra View --> thì lệnh sau mới truy cập đc searchLookUpEdit1.Properties.View.Columns[0].Caption = "ID";
        //        lookUpEdit1.Properties.ValueMember = value;
        //        lookUpEdit1.Properties.DisplayMember = display;

        //    }
        //}
        public void setColumnAll(bool show)
        {
            for (int i = 0; i < lookUpEdit1.Properties.Columns.Count; i++)
                if (lookUpEdit1.Properties.Columns[i].FieldName != lookUpEdit1.Properties.DisplayMember)
                    lookUpEdit1.Properties.Columns[i].Visible = show;
        }
        private void this_setColumn(string columnName, int index, string caption, int width)
        {
            if (index < 0)
                lookUpEdit1.Properties.Columns[columnName].Visible = false;
            else
            {
                //lookUpEdit1.Properties.Columns[columnName].set = index;
                lookUpEdit1.Properties.Columns[columnName].Caption = caption;
                if (width > 0) lookUpEdit1.Properties.Columns[columnName].Width = width;
            }
        }
        public void setColumn(string columnName, int index, string caption, int width)
        {
            if (lookUpEdit1.Properties.DataSource == null || lookUpEdit1.Properties.Columns[columnName] == null) return;

            this_setColumn(columnName, index, caption, width);
        }
        public void setColumn(int columnIndex, int index, string caption, int width)
        {
            if (lookUpEdit1.Properties.DataSource == null || lookUpEdit1.Properties.Columns.Count <= columnIndex) return;

            this_setColumn(lookUpEdit1.Properties.Columns[columnIndex].FieldName, index, caption, width);
        }


        private void textEdit1_Leave(object sender, EventArgs e)
        {
            //DataRowView dv = (DataRowView)lookUpEdit1.Properties.GetDataSourceRowByKeyValue(textEdit1.Text);
            //if (dv == null)
            //{
            //    SelectIndex = _DefaultIndex;
            //}
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lookUpEdit1.Focus();
                lookUpEdit1.ShowPopup();
            }
        }

        private void ComboBoxTextSearch_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false) CaptionValidate = false;
        }



    }
}

