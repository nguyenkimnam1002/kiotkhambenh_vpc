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
using DevExpress.XtraEditors.Repository;
using System.Collections;

namespace VNPT.HIS.UserControl
{
    public partial class ucSearchLookup2 : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ucSearchLookup2()
        {
            InitializeComponent();
        }

        private void ucSearchLookup2_Load(object sender, EventArgs e)
        {
            //searchLookUpEdit1.Properties.ShowClearButton = false;
            //searchLookUpEdit1.Properties.ShowFooter = false;

            
        }

        #region THUỘC TÍNH
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

        private int fill = 1; // 1: kieu fill chi co Ten; 2: kieu fill: Ma+Ten
        public int Option_Fill
        {
            set
            {
                fill = value;
            }
            get
            {
                return fill;
            }
        }
        private int type = 0; // 1: kieu bệnh chính; 2: kiểu chọn nhiều bệnh phụ 
        public int Option_Type
        {
            set
            {
                type = value;
                if (type == 1)
                {
                    textEdit1.Visible = true;
                    textEdit1.TabStop = true;

                    //textEdit1.Dock = DockStyle.Fill;
                    this.Size = new Size(this.Size.Width, 20);

                    memoEdit1.Visible = false;
                    memoEdit1.TabStop = false;
                }
                else if (type == 2)
                {
                    textEdit1.Visible = false;
                    textEdit1.TabStop = false;

                    //memoEdit1.Dock = DockStyle.Fill;
                    this.Size = new Size(this.Size.Width, 65);

                    memoEdit1.Visible = true;
                    memoEdit1.TabStop = true;
                }
            }
            get
            {
                return type;
            }
        }
        public int Option_CaptionWidth
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
        public int Option_SearchTextWidth
        {
            set
            {
                panelControl_Search.Width = value;
            }
            get
            {
                return panelControl_Search.Width;
            }
        }
        public int Option_MemoEditHeight
        {
            set
            {
                memoEdit1.Height = value;
            }
            get
            {
                return memoEdit1.Height;
            }
        }  
        public string Option_Caption
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
        public bool Option_ReadOnlyText
        {
            set
            {
                textEdit1.ReadOnly = value;
                memoEdit1.ReadOnly = value;
            }
            get
            {
                return textEdit1.ReadOnly;
            }
        }
        public bool Option_CaptionValidate
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
        public DockStyle Option_CaptionDock
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
        public string Option_FindColumns
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
        public int SelectedIndex
        {
            set
            {// chỉ dùng cho datasource là Datatable
                try
                {
                    if (searchLookUpEdit1.Properties.DataSource != null && ((DataTable)searchLookUpEdit1.Properties.DataSource).Rows.Count > value)
                        searchLookUpEdit1.EditValue = ((DataTable)searchLookUpEdit1.Properties.DataSource).Rows[value][searchLookUpEdit1.Properties.ValueMember].ToString();

                }
                catch (Exception ex)
                {
                    searchLookUpEdit1.EditValue = "";
                }
            }
            get
            {
                try
                {
                    return searchLookUpEdit1.Properties.GetIndexByKeyValue(searchLookUpEdit1.EditValue.ToString());
                }
                catch (Exception ex)
                {
                    log.Fatal(ex.ToString());
                    return -1;
                }
            }
        }
        public string SelectedValue
        {
            set
            {// chỉ dùng cho datasource là Datatable
                if (searchLookUpEdit1.Properties.DataSource != null)
                    searchLookUpEdit1.EditValue = value;
            }
            get
            {
                try
                {
                    if (this.Option_Type == 1)
                        return searchLookUpEdit1.EditValue == null ? "" : searchLookUpEdit1.EditValue.ToString();
                    else
                    {
                        string list = "";
                        foreach (string key in SelectList.Keys)
                        {
                            list += "; " + key;
                        }
                        if (list.Length > 0) list = list.Substring("; ".Length);
                        return list;
                    }
                }
                catch (Exception ex) 
                { 
                    log.Fatal(ex.ToString()); 
                    return ""; 
                }
            }
        }
        public string SelectedText
        {
            set
            {
                try
                {
                    if (this.Option_Type == 1) textEdit1.EditValue = value;
                    else memoEdit1.EditValue = value;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }
            get
            {
                try
                {
                    if (this.Option_Type == 1) return textEdit1.EditValue.ToString();
                    else return memoEdit1.EditValue.ToString();
                }
                catch (Exception ex) 
                {
                    System.Console.WriteLine(ex.ToString());
                    return "";
                }
            }
        } 
        public DataRowView SelectedDataRowView
        {
            set
            {
            }
            get
            {

                try
                {
                    return ((DataTable)searchLookUpEdit1.Properties.DataSource).DefaultView[this.SelectedIndex];
                }
                catch (Exception ex) 
                {
                    return null;
                }
            }
        }
        private bool LockKeyTab = false;
        public bool Option_LockKeyTab
        {
            set
            {
                LockKeyTab = value;
            }
            get
            {
                return LockKeyTab;
            }
        }
        public bool Option_Readonly
        {
            set
            {
                searchLookUpEdit1.ReadOnly = value;
                textEdit1.ReadOnly = value;
                memoEdit1.ReadOnly = value;
                btnEdit.Enabled = !value;
                btnReset.Enabled = !value;
            }
            get
            {
                return searchLookUpEdit1.ReadOnly;
            }
        }
        
        #endregion

        #region SỰ KIỆN
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
        protected EventHandler select_check;
        public string messageError = "";
        public void setEvent_Check(EventHandler eventSelectCheck)
        {
            select_check = eventSelectCheck;
        }
        
        

        private void searchLookUpEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                searchLookUpEdit1.ShowPopup();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (Option_Type == 1) textEdit1.Focus();
                else memoEdit1.Focus();
                //if (searchLookUpEdit1.IsPopupOpen == false && enter_key != null) enter_key(sender, e);
            }
            //else if (e.KeyCode == Keys.Tab && enter_key != null)
            //{
                // nhảy đúng nhg ko chọn
            //    enter_key(sender, e);
            //} 
            //if (!searchLookUpEdit1.IsPopupOpen) searchLookUpEdit1.ShowPopup();
            
            //searchLookUpEdit1View.Focus();
            //SendKeys.Send("{DOWN}");
        }
        public Dictionary<string, string> SelectList = new Dictionary<string, string>();

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string id = searchLookUpEdit1.EditValue.ToString();
                int ind = searchLookUpEdit1.Properties.GetIndexByKeyValue(id);
                if (ind < 0)
                {
                    if (this.Option_Type == 1) { textEdit1.Text = ""; textEdit1.ToolTip = ""; }

                    return;
                }
                // Nếu cho phép (Columns[columnName].OptionsColumn.AllowSort=true) sắp xếp lại cột thì dùng: DataRowView item = (DataRowView)Extension.GetRowByKeyValue(searchLookUpEdit1, id);
                DataRowView item = ((DataTable)searchLookUpEdit1.Properties.DataSource).DefaultView[ind];

                //Kiểm tra dl được chọn
                if (item != null && select_check != null)
                {
                    messageError = "";
                    select_check(item, null);
                    if (messageError != "")
                    {
                        searchLookUpEdit1.EditValue = "";
                        MessageBox.Show(messageError);
                        return;
                    }
                }

                if (this.Option_Type == 1)
                {
                    if (fill == 2)
                    {
                        textEdit1.Text = item[searchLookUpEdit1.Properties.ValueMember].ToString() + "-" + item[columnNameDisplay].ToString();
                    }
                    else
                    {
                        textEdit1.Text = item[columnNameDisplay].ToString();
                    }
                    textEdit1.ToolTip = item[columnNameDisplay].ToString();
                }
                else
                {
                    if (fill == 2)
                    {
                        memoEdit1.Text += (memoEdit1.Text == "" ? "" : "; ")
                             + item[searchLookUpEdit1.Properties.ValueMember].ToString() + "-" + item[columnNameDisplay].ToString();
                    }
                    else
                    {
                        memoEdit1.Text += (memoEdit1.Text == "" ? "" : "; ") + item[columnNameDisplay].ToString();
                    }
                    addOrUpdate(item[searchLookUpEdit1.Properties.ValueMember].ToString(), item[columnNameDisplay].ToString());
                }

                if (item != null && select_change != null) select_change(item, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        private void addOrUpdate(string key, string newValue)
        {
            string x = "";
            if (SelectList.TryGetValue(key, out x))
            {
                SelectList[key] = newValue;
            }
            else
            {
                // darn, lets add the value
                SelectList.Add(key, newValue);
            }
        }

        private void ucSearchLookup2_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false) Option_CaptionValidate = false;
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (enter_key != null) enter_key(sender, e);
            }
            else if (e.KeyCode == Keys.Tab && enter_key != null)
            {
                // nhảy đúng nhg ko chọn
                enter_key(sender, e);
            }
        }
        private void textEdit1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (LockKeyTab)
            {
                if (e.KeyData == Keys.Tab) e.IsInputKey = true;
            }

        }
        private void memoEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (enter_key != null) enter_key(sender, e);
            }
            else if (e.KeyCode == Keys.Tab && enter_key != null)
            {
                // nhảy đúng nhg ko chọn
                enter_key(sender, e);
            }
        }
        private void memoEdit1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (LockKeyTab)
            {
                if (e.KeyData == Keys.Tab) e.IsInputKey = true;
            }

        }

        #endregion

        #region HÀM XỬ LÝ

        public void setData(DataTable dt, int indexValue, int indexDisplay)
        {
            if (dt == null || dt.Rows.Count == 0) return;
            setData(dt, dt.Columns[indexValue].ColumnName, dt.Columns[indexDisplay].ColumnName);
        }
        private string columnNameDisplay = "";
        public void setData(DataTable dt, string value, string display)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            searchLookUpEdit1.Properties.DataSource = dt;
            searchLookUpEdit1.Properties.PopulateViewColumns();// lệnh đổ dl ra View --> thì lệnh sau mới truy cập đc searchLookUpEdit1.Properties.View.Columns[0].Caption = "ID";
            searchLookUpEdit1.Properties.ValueMember = value;
            searchLookUpEdit1.Properties.DisplayMember = value;
            columnNameDisplay = display;
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

                searchLookUpEdit1.Properties.View.Columns[columnName].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
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


        #endregion
         

        private void btnReset_Click(object sender, EventArgs e)
        {
            searchLookUpEdit1.EditValue = "";
            memoEdit1.EditValue = "";
            SelectList.Clear();
        }

        private void textEdit1_VisibleChanged(object sender, EventArgs e)
        {
            bool x = textEdit1.Visible;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string ValueMemberName = searchLookUpEdit1View.Columns[searchLookUpEdit1.Properties.ValueMember].Caption;
            string DisplayMemberName = searchLookUpEdit1View.Columns[columnNameDisplay].Caption;

            SubForm.NGT02K052_ChinhSuaBenhPhu frm = new SubForm.NGT02K052_ChinhSuaBenhPhu(
                searchLookUpEdit1.Properties.DataSource
                , searchLookUpEdit1.Properties.ValueMember, columnNameDisplay
                , ValueMemberName, DisplayMemberName
                , this.SelectedValue
                );
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.setReturnData(ReturnData_NGT02K052_ChinhSuaBenhPhu);
            frm.ShowDialog();            
        }
        private void ReturnData_NGT02K052_ChinhSuaBenhPhu(object sender, EventArgs e)
        {
            Dictionary<string, string> returnList = (Dictionary<string, string>)sender;

            btnReset_Click(null, null);
             
            foreach (string key in returnList.Keys)
            {
                searchLookUpEdit1.EditValue = key;
            }

            searchLookUpEdit1.EditValue = "";
        }


    }
    public static class Extension
{
        public static object GetRowByKeyValue(this SearchLookUpEdit edit, string key)
    {
        IListSource listSource = edit.Properties.DataSource as IListSource;
        IList list;
        if (listSource != null)
            list = listSource.GetList();
        else
            list = edit.Properties.DataSource as IList;

        PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(list);
        foreach (object row in list)
        {
            PropertyDescriptor propertyDescriptor = listItemProperties[edit.Properties.ValueMember];
            object val = propertyDescriptor.GetValue(row);
            if (object.Equals(val, key))
                return row;
        }
        return null;
    }
}
}
