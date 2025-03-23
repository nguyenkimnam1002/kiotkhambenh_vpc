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
    public partial class ucComboBox : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ucComboBox()
        {
            InitializeComponent();
        }

        private void ucSearchLookup_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Properties.ShowHeader = false;
            //searchLookUpEdit1.Properties.ShowClearButton = false;
            //searchLookUpEdit1.Properties.ShowFooter = false;
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
        public int SelectIndex
        {
            set
            {
                if (lookUpEdit1.Properties.DataSource != null
                    && ((DataTable)lookUpEdit1.Properties.DataSource).Rows.Count > value
                    )
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
        public string SelectText
        {
            set
            {
                lookUpEdit1.Text = value;
            }
            get
            {
                if (lookUpEdit1.EditValue != null) return lookUpEdit1.Text;
                else return "";
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
            //lookUpEdit1.Properties.DataSource = null;
        }
        public void clearData(int ind)
        {
            DataTable dt = lookUpEdit1.Properties.DataSource as DataTable;
            dt.Rows.RemoveAt(ind);
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
            //if (!searchLookUpEdit1.IsPopupOpen) searchLookUpEdit1.ShowPopup();

            //searchLookUpEdit1View.Focus();
            //SendKeys.Send("{DOWN}");
        }

        public void SetIndexByColumnValue(string ColumnName, string ColumnValue)
        {
            DataTable dt = lookUpEdit1.Properties.DataSource as DataTable;
            if (dt.Columns.Contains(ColumnName) == false) return;
            for (int i=0; i< dt.Rows.Count; i++)
                if (dt.Rows[i][ColumnName].ToString() == ColumnValue)
                {
                    SelectIndex = i;
                    return;
                }   
        }

        public void SetIndexByColumnValue(int ColumnIndex, string ColumnValue)
        {
            string ColumnName = lookUpEdit1.Properties.Columns[ColumnIndex].FieldName;
            SetIndexByColumnValue(ColumnName, ColumnValue);
        }

        public string SelectValueByColumnName(string ColumnName)
        {
            try
            {
                DataTable dt = lookUpEdit1.Properties.DataSource as DataTable;
                if (dt.Columns.Contains(ColumnName) == false) return "";
                int ind = this.SelectIndex;
                return dt.Rows[ind][ColumnName].ToString();
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        public string SelectValueByColumnName(int ColumnIndex)
        {
            string ColumnName = lookUpEdit1.Properties.Columns[ColumnIndex].FieldName;
            return SelectValueByColumnName(ColumnName);
        }


        protected EventHandler select_change;
        public void setEvent(EventHandler eventChangeValue)
        {
            select_change = eventChangeValue;
        }
        protected EventHandler enter_key;
        public void setEvent_Enter(EventHandler eventKeyEnter)
        {
            enter_key = eventKeyEnter;
        }
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            object item = lookUpEdit1.Properties.GetDataSourceRowByKeyValue(lookUpEdit1.EditValue);

            if (item != null && select_change != null) select_change(item, null);
        }

        public void setData(DataTable dt, int indexValue, int indexDisplay)
        {
            clearData();
            if (dt == null || dt.Columns.Count == 0) return;
            this_setData(dt, dt.Columns[indexValue].ColumnName, dt.Columns[indexDisplay].ColumnName);
        }
        public void setData(DataTable dt, string value, string display)
        {
            clearData();
            if (dt == null || dt.Columns.Count == 0) return;
            this_setData(dt, value, display);
        }
        private void this_setData(DataTable dt, string value, string display)
        {
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.PopulateColumns();// lệnh đổ dl ra View --> thì lệnh sau mới truy cập đc searchLookUpEdit1.Properties.Columns[0].Caption = "ID";
            lookUpEdit1.Properties.ValueMember = value;
            lookUpEdit1.Properties.DisplayMember = display;
            lookUpEdit1.Properties.DropDownRows = dt.Rows.Count < 15 ? dt.Rows.Count : 15;
            
            setColumnAll(false);
            // Chú ý: nếu control này đang bị ẩn đi, thì  lookUpEdit1.Properties.Columns --> rỗng và câu lệnh dưới sẽ gây lỗi
            if (lookUpEdit1.Properties.Columns.Count > 0)
            {
                lookUpEdit1.Properties.Columns[lookUpEdit1.Properties.ValueMember].Visible = true;
                lookUpEdit1.Properties.Columns[lookUpEdit1.Properties.DisplayMember].Visible = true;
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
        //        //lookUpEdit1.ItemIndex = 0;
        //    }
        //}


        public void setColumnAll(bool show)
        {
            for (int i = 0; i < lookUpEdit1.Properties.Columns.Count; i++)
            {
                lookUpEdit1.Properties.Columns[i].Visible = show;
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
        private void this_setColumn(string columnName, int index, string caption, int width)
        {
            try
            {
                if (index < 0)
                    lookUpEdit1.Properties.Columns[columnName].Visible = false;
                else
                {
                    //lookUpEdit1.Properties.Columns[columnName].VisibleIndex = index;
                    lookUpEdit1.Properties.Columns[columnName].Caption = caption;
                    if (width > 0) lookUpEdit1.Properties.Columns[columnName].Width = width;
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        public void setColumn(int columnIndex, bool visible)
        {
            try
            {
                if (lookUpEdit1.Properties.DataSource == null || lookUpEdit1.Properties.Columns.Count <= columnIndex) return;

                lookUpEdit1.Properties.Columns[columnIndex].Visible = visible;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

    }
}
