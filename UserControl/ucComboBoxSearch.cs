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
    public partial class ucComboBoxSearch : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //SendKeys.Send("{DOWN}"); --> chủ động truyền phím bấm
        public ucComboBoxSearch()  
        {
            InitializeComponent();
        }
        private void ucSearchLookup_Load(object sender, EventArgs e)
        {
            labelControl2.Visible = Validate;


            //searchLookUpEdit1.Properties.ShowClearButton = false;
            //searchLookUpEdit1.Properties.ShowFooter = false;
            //lookUpEdit1.Properties.ShowHeader = true; hiển thị tiêu đề các cột của phần popup
            //lookUpEdit1.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup; --> tự động dãn rộng bằng độ rộng của dl
        }

        #region THUỘC TÍNH
        private DataTable dtSource;
        private int _DefaultIndex = 0;
        
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
        public string SelectSearchText
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
        private bool Validate = true;
        public bool CaptionValidate
        {
            set
            {
                Validate = value;
                labelControl2.Visible = Validate;
            }
            get
            {
                return Validate;
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
        
        public int GetIndexByColumn(string columnName, string value) 
        {
            DataTable dt = (DataTable)lookUpEdit1.Properties.DataSource;
            if (dt == null || dt.Rows.Count == 0) return -1;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][columnName].ToString() == value)
                {
                    return i;
                }
            }
            return -1;
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
        private bool SearchByID = true; // true tìm kiếm theo id, false tìm kiếm theo text hiển thị
        public bool Option_SearchByID
        {
            set
            {
                SearchByID = value;
            }
            get
            {
                return SearchByID;
            }
        }
        public bool Option_Readonly
        {
            set
            {
                lookUpEdit1.ReadOnly = value;
                textEdit1.ReadOnly = value;
            }
            get
            {
                return lookUpEdit1.ReadOnly;
            }
        }

        #endregion
        #region SỰ KIỆN
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
            else if (e.KeyCode == Keys.Tab && enter_key != null)
            {
                // nhảy đúng nhg ko chọn
                enter_key(sender, e);
            }

            //else
            //    this.ucComboBoxSearch_KeyDown(sender, e);

            //if (!searchLookUpEdit1.IsPopupOpen) searchLookUpEdit1.ShowPopup();
            
            //searchLookUpEdit1View.Focus();
            //SendKeys.Send("{DOWN}");
        } 
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (SearchByID) textEdit1.Text = lookUpEdit1.EditValue.ToString();
            object item = lookUpEdit1.Properties.GetDataSourceRowByKeyValue(lookUpEdit1.EditValue);
            // co the dung: (DataRowView)lookUpEdit1.GetSelectedDataRow()
            if (item != null && select_change != null) select_change(item, null);
        }
            
        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            if (SearchByID) Search_Value();
            else Search_Display();            
        }
        private void Search_Value()
        {
            DataRowView dv = (DataRowView)lookUpEdit1.Properties.GetDataSourceRowByKeyValue(textEdit1.Text);
            if (dv != null)
            {
                lookUpEdit1.EditValue = textEdit1.Text;
            }
            else
                lookUpEdit1.EditValue = "";
        }
        private void Search_Display()
        {
            if (dtSource == null) return;
            DataTable dt = dtSource.Copy();
            for (int i = dt.Rows.Count-1; i >=0 ; i--)
            {
                if (dt.Rows[i][lookUpEdit1.Properties.DisplayMember].ToString().IndexOf(textEdit1.Text.Trim()) == -1)
                    dt.Rows.RemoveAt(i);
            }

            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.DropDownRows = dt.Rows.Count < 15 ? dt.Rows.Count : 15;
            setColumnAll(false);
            lookUpEdit1.Properties.Columns[lookUpEdit1.Properties.DisplayMember].Visible = true;

            SelectIndex = _DefaultIndex; 
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {
            if (SearchByID)
            {
                DataRowView dv = (DataRowView)lookUpEdit1.Properties.GetDataSourceRowByKeyValue(textEdit1.Text);
                if (dv == null)
                {
                    SelectIndex = _DefaultIndex;
                }
            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lookUpEdit1.Focus();
                lookUpEdit1.ShowPopup();
            }
        }

        private void ucComboBoxSearch_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == false) CaptionValidate = false;
        }

        private void lookUpEdit1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (LockKeyTab)
            {
                if (e.KeyData == Keys.Tab) e.IsInputKey = true;
            }

        }

        #endregion
        #region HÀM XỬ LÝ
        public void clearData()
        {
            lookUpEdit1.Properties.DataSource = null;
            textEdit1.Text = "";
        }
        public void setData(DataTable dt, int indexValue, int indexDisplay)
        {
            clearData();
            if (dt == null || dt.Rows.Count == 0 || dt.Columns.Count <= indexValue || dt.Columns.Count <= indexDisplay) return;
            this_setData(dt, dt.Columns[indexValue].ColumnName, dt.Columns[indexDisplay].ColumnName);
        }

        public void setData(DataTable dt, string value, string display)
        {
            clearData();
            if (dt == null || dt.Rows.Count == 0 || !dt.Columns.Contains(value) || !dt.Columns.Contains(display)) return;
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
            lookUpEdit1.Properties.Columns[lookUpEdit1.Properties.DisplayMember].Visible = true;

            dtSource = dt.Copy();
        }
        public void setColumnAll(bool show)
        {
            for (int i=0; i< lookUpEdit1.Properties.Columns.Count; i++)
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
                    //lookUpEdit1.Properties.Columns[columnName].set = index;
                    lookUpEdit1.Properties.Columns[columnName].Caption = caption;
                    if (width > 0) lookUpEdit1.Properties.Columns[columnName].Width = width;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        #endregion


        
        

        
         
    }
}

