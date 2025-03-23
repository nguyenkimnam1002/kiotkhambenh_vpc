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
    public partial class ucSearchLookup : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSearchLookup()
        {
            InitializeComponent();
        }

        private void ucSearchLookup_Load(object sender, EventArgs e)
        {
            //searchLookUpEdit1.Properties.ShowClearButton = false;
            //searchLookUpEdit1.Properties.ShowFooter = false;
        }
        public bool typeTable = false;
        public SearchLookUpEdit searchLookUpEdit
        {
            set
            {
                searchLookUpEdit1 = value;
            }
            get
            {
                return searchLookUpEdit1;
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
        public string findColumns
        {
            set
            {
                searchLookUpEdit1.Properties.View.OptionsFind.FindFilterColumns = value;
            }
            get
            {
                return searchLookUpEdit1.Properties.View.OptionsFind.FindFilterColumns;
            }
        }
        public int SelectIndex
        {
            set
            {// chỉ dùng cho datasource là Datatable
                try
                {
                    if (typeTable && searchLookUpEdit1.Properties.DataSource != null && ((DataTable)searchLookUpEdit1.Properties.DataSource).Rows.Count > value)
                        searchLookUpEdit1.EditValue = ((DataTable)searchLookUpEdit1.Properties.DataSource).Rows[value][searchLookUpEdit1.Properties.ValueMember].ToString();
                }
                catch (Exception ex) { System.Console.WriteLine(ex.ToString()); searchLookUpEdit1.EditValue = ""; }
            }
            get
            {
                try
                {
                    return searchLookUpEdit1.Properties.GetIndexByKeyValue(searchLookUpEdit1.EditValue.ToString());
                }
                catch (Exception ex) { return -1; }
            }
        }
        public string SelectValue
        {
            set
            {// chỉ dùng cho datasource là Datatable
                if (typeTable && searchLookUpEdit1.Properties.DataSource != null)
                    searchLookUpEdit1.EditValue = value;
            }
            get
            {
                try
                {
                    return searchLookUpEdit1.EditValue.ToString();
                }
                catch (Exception ex) { return ""; }
            }
        }
        public string SelectText
        {
            set
            {// chỉ dùng cho datasource là Datatable
                if (typeTable && searchLookUpEdit1.Properties.DataSource != null)
                    searchLookUpEdit1.Text = value;
            }
            get
            {
                try
                {
                    return searchLookUpEdit1.Text;
                }
                catch (Exception ex) { return ""; }
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
                    return ((DataTable)searchLookUpEdit1.Properties.DataSource).DefaultView[this.SelectIndex];
                }
                catch (Exception ex) { return null; }
            }
        }
        private void searchLookUpEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                searchLookUpEdit1.ShowPopup();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (searchLookUpEdit1.IsPopupOpen == false && enter_key != null) enter_key(sender, e);
            }
            //if (!searchLookUpEdit1.IsPopupOpen) searchLookUpEdit1.ShowPopup();
            
            //searchLookUpEdit1View.Focus();
            //SendKeys.Send("{DOWN}");
        }
        protected EventHandler enter_key;
        public void setEvent_Enter(EventHandler eventKeyEnter)
        {
            enter_key = eventKeyEnter;
        }
        protected EventHandler select_change = null;
        public void setEvent(EventHandler eventChangeValue)
        {
            select_change = eventChangeValue;
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string id = searchLookUpEdit1.EditValue.ToString();
            int ind = searchLookUpEdit1.Properties.GetIndexByKeyValue(id);
            if (ind < 0) return;
            //DataRow item = (DataRowView)searchLookUpEdit1View.GetDataRow(ind).Table.DefaultView[0];//
            DataRowView item = ((DataTable)searchLookUpEdit1.Properties.DataSource).DefaultView[ind];
            if (item != null && select_change!=null) select_change(item, null);
        }

        public void setData(DataTable dt, int indexValue, int indexDisplay)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            setData(dt, dt.Columns[indexValue].ColumnName, dt.Columns[indexDisplay].ColumnName);
        }
        public void setData(DataTable dt, string value, string display)
        {
            typeTable = true;
            searchLookUpEdit1.EditValue = "";
            searchLookUpEdit1.Properties.DataSource = dt;
            searchLookUpEdit1.Properties.PopulateViewColumns();// lệnh đổ dl ra View --> thì lệnh sau mới truy cập đc searchLookUpEdit1.Properties.View.Columns[0].Caption = "ID";
            searchLookUpEdit1.Properties.ValueMember = value;
            searchLookUpEdit1.Properties.DisplayMember = display;            
        }
        
        //public void setData(object list, string value, string display, string defaultValue)
        //{
        //    searchLookUpEdit1.Properties.DataSource = list;
        //    searchLookUpEdit1.Properties.PopulateViewColumns();
        //    searchLookUpEdit1.Properties.ValueMember = value;
        //    searchLookUpEdit1.Properties.DisplayMember = display;
        //    if (defaultValue != "") searchLookUpEdit1.EditValue = defaultValue;
        //}

        public void setAllColumn(bool visiable)
        {
            if (searchLookUpEdit1.Properties.DataSource == null) return;
            for (int i=0; i< searchLookUpEdit1.Properties.View.Columns.Count; i++)
                searchLookUpEdit1View.Columns[i].Visible = visiable;
        }
        private void this_setColumn(string columnName, int index, string caption, int width)
        {
            if (index < 0)
                searchLookUpEdit1.Properties.View.Columns[columnName].Visible = false;
            else
            {
                searchLookUpEdit1View.Columns[columnName].VisibleIndex = index;
                searchLookUpEdit1View.Columns[columnName].Caption = caption;
                if (width > 0) searchLookUpEdit1View.Columns[columnName].Width = width;
            }
        }
        public void setColumn(string columnName, int index, string caption, int width)
        {
            if (searchLookUpEdit1.Properties.DataSource == null || searchLookUpEdit1View.Columns[columnName] == null) return;

            this_setColumn(columnName, index, caption, width);
        }
        public void setColumn(int columnIndex, int index, string caption, int width)
        {
            if (searchLookUpEdit1.Properties.DataSource == null || searchLookUpEdit1View.Columns.Count <= columnIndex) return;

            this_setColumn(searchLookUpEdit1View.Columns[columnIndex].FieldName, index, caption, width);
        }

        private void ucSearchLookup_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false) CaptionValidate = false;
        }
         

    }
}
