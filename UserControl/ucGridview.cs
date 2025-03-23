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
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using VNPT.HIS.Common;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace VNPT.HIS.UserControl
{
    public partial class ucGridview : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ucGridview()
        {
            InitializeComponent();
        }
        private void ucGridview_Load(object sender, EventArgs e)
        {
            //Thiết lập mặc định 1 số thuộc tính
            gridView1.OptionsView.ShowAutoFilterRow = true;//hiển thị ô search cho các cột
            gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            gridView1.OptionsView.ShowFooter = false; // ẩn footer

            gridView1.OptionsView.ShowGroupPanel = false; // ẩn phần panel group

            gridView1.OptionsView.ShowViewCaption = true;// Hiển thị Tiêu đề của grid

            gridView1.OptionsBehavior.ReadOnly = true; // dl chỉ được đọc

            gridView1.OptionsView.ColumnAutoWidth = true; // chiều rộng các cột dãn kín grid

            //initTableFlterColumn();


            // 1 SỐ THUỘC TÍNH CÓ THỂ DÙNG BÊN NGOÀI KHI GỌI CONTROL
            //gridView1.OptionsBehavior.Editable = false; 
            //gridView1.Columns[2].Group();  tạo group dữ liệu
            //gridView1.OptionsView.ShowGroupedColumns = true;
            //gridView1.OptionsView.ShowGroupPanel = true;
            //gridView1.OptionsBehavior.AutoExpandAllGroups = true;
            //gridView1.OptionsView.ColumnAutoWidth = true;
            // ...

            // CÁC CHẾ ĐỘ HIỂN THỊ ĐỘ RỘNG CỘT
            // MẶC ĐỊNH: gridView1.OptionsView.ColumnAutoWidth = true; Và gridView1.BestFitColumns(true); trong setdata --> dãn kín grid và theo tỷ lệ độ dài data các cột.
            // tùy chọn 1: tùy chỉnh độ rộng từng cột: gridView1.OptionsView.ColumnAutoWidth = false; Và thiết lập độ rộng từng cột theo hàm setColumn(... width)
            // tùy chọn 2: các cột hiển thị dãn tràn ra grid, xuất hiện thanh kéo ngang, tùy theo độ rộng của dữ liệu: gridView1.OptionsView.ColumnAutoWidth = false
        }

        #region Thuốc tính
        public DevExpress.XtraGrid.Views.Grid.GridView gridView
        {
            set
            {
                gridView1 = value;
            }
            get
            {
                return gridView1;
            }
        }
        public DevExpress.XtraGrid.GridControl gridControl
        {
            set
            {
                gridControl1 = value;
            }
            get
            {
                return gridControl1;
            }
        }
        public string caption
        {
            set
            {
                gridView1.ViewCaption = value;
            }
            get
            {
                return gridView1.ViewCaption;
            }
        }
        public void Set_Caption(string caption)
        {
            gridView1.ViewCaption = caption;
        }
        public void Set_HidePage(bool visible)
        {
            //layoutControlItem_Page.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        public DataRowView SelectedRow
        {
            set
            {
                

            }
            get
            {                
                return select_row_zero_first ? null : (DataRowView)gridView1.GetFocusedRow();
            }
        }
        #endregion

        public void clearData()
        {
            //gridControl1.DataSource = null;
            //gridView1.Columns.Clear();
        }
        public void clearData_frmTiepNhan()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
        }

        public void clearData(int ind)
        {
            DataTable dt = (DataTable)gridControl1.DataSource;
            dt.Rows.RemoveAt(ind);
        }

        public bool ReLoadWhenFilter = false;
        public void SetReLoadWhenFilter(bool reLoad)
        {
            ReLoadWhenFilter = reLoad;
            if (ReLoadWhenFilter) initTimer();
        }
        public void setEvent(EventHandler _getData)
        {
            ucPage1.setEvent(_getData);
        }

        protected EventHandler event_FocusedRowChanged;
        public void setEvent_FocusedRowChanged(EventHandler eventFocusedRowChanged)
        {
            event_FocusedRowChanged = eventFocusedRowChanged;
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            select_row_zero_first = false;

            //DataRowView drv = (DataRowView)gridView1.GetRow(e.FocusedRowHandle);
            //if (event_FocusedRowChanged != null && drv != null) event_FocusedRowChanged(drv, null);
            gridView1_FocusedRowChanged_Custom(e.FocusedRowHandle);
        }
        public void gridView1_FocusedRowChanged_Custom(int handle)
        {
            DataRowView drv = (DataRowView)gridView1.GetRow(handle);
            if (event_FocusedRowChanged != null && drv != null) event_FocusedRowChanged(drv, null);
        }

        bool select_row_zero_first = true;

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            if (select_row_zero_first && e.RowHandle == 0)
            {
                select_row_zero_first = false;
                gridView1_FocusedRowChanged_Custom(0);
            }
        }
        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            gridView1_RowClick(sender, e);
        }

        protected EventHandler event_DoubleClick;
        public void setEvent_DoubleClick(EventHandler eventDoubleClick)
        {
            event_DoubleClick = eventDoubleClick;
            resetColumnAllowEdit();
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (event_DoubleClick != null) event_DoubleClick(sender, e);
        }

        public void setData(DataTable datatable, int total, int page)  // list  = Datatable
        {
            setData(datatable, total, page, 0);
        }
        public void setData(DataTable datatable, int total_page, int page, int total_record)  // list  = Datatable
        {
            try
            {
                if (datatable != null)
                {
                    select_row_zero_first = true;

                    gridView1.FocusedRowChanged -= gridView1_FocusedRowChanged;
                    gridView1.ColumnFilterChanged -= gridView1_ColumnFilterChanged;

                    gridControl1.DataSource = datatable;

                    gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
                    gridView1.ColumnFilterChanged += gridView1_ColumnFilterChanged;

                    //gridView1_FocusedRowChanged_Custom(0);

                    if (total_record == 0 || page < total_page) ucPage1.setData(total_page, page, total_record);
                    else ucPage1.setData(total_page, page, (page - 1) * ucPage1.getNumberPerPage() + datatable.Rows.Count);

                    //Dãn các cột tương ứng với độ dài dữ liệu
                    gridView1.BestFitColumns(true);

                    resetColumnAllowEdit();

                    //Set mặc định filter kiểu contains/like
                    if (gridView1.OptionsView.ShowAutoFilterRow)
                    {
                        foreach (DevExpress.XtraGrid.Columns.GridColumn col in gridView1.Columns)
                            col.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                    }

                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        #region set cột có thể sửa,
        public void setEditColumn(int columnIndex)
        {
            if (gridControl1.DataSource == null || gridView1.Columns.Count <= columnIndex) return;

            setEditColumn(gridView1.Columns[columnIndex].FieldName);
        }
        public void setEditColumn(string columnName)
        {
            if (gridControl1.DataSource == null || gridView1.Columns[columnName] == null) return;
            
            if (gridView1.OptionsBehavior.ReadOnly == true)
            {
                gridView1.OptionsBehavior.ReadOnly = false;
                for (int i = 0; i < gridView1.Columns.Count; i++)
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            gridView1.Columns.ColumnByFieldName(columnName).OptionsColumn.AllowEdit = true; 
        }
        #endregion

        #region Thiết lập các cột: vị trí, tiêu đề, độ rộng, ẩn hiện
        public void setColumnAll(bool visible)
        {
            if (gridControl1.DataSource == null) return;

            for (int i = gridView1.Columns.Count - 1; i >= 0; i--)
                gridView1.Columns[i].Visible = visible;
        }
        public void setColumn(int columnIndex, int index, string caption, int width)
        {
            if (gridControl1.DataSource == null || gridView1.Columns.Count <= columnIndex) return;

            setColumn(gridView1.Columns[columnIndex].FieldName, index, caption, width);
        }
        public void setColumn(string columnName, int index, string caption, int width)
        {
            if (gridControl1.DataSource == null || gridView1.Columns[columnName] == null) return;

            gridView1.Columns[columnName].Caption = caption;
            gridView1.Columns[columnName].VisibleIndex = index; //gridView1.Columns[columnName].Visible = visible;
            if (width > 0) gridView1.Columns[columnName].Width = width;
        }

        public void setColumn(int columnIndex, int index, string caption)
        {
            setColumn(columnIndex, index, caption, 0);
        }
        public void setColumn(string columnName, int index, string caption)
        {
            setColumn(columnName, index, caption, 0);
        }
        public void setColumn(string columnName, int index)
        {
            if (gridControl1.DataSource == null || gridView1.Columns[columnName] == null) return;

            gridView1.Columns[columnName].VisibleIndex = index;
        }

        int colIndex = 0;
        bool setCheckBox = false;
        public void setColumn(string columnName, string caption)
        {
            if (setCheckBox && colIndex == 1) colIndex++; // Thứ tự 1 dánh cho ô checkbox
            setColumn(columnName, colIndex++, caption, 0);
        }
        #endregion

        #region Thêm cột check các dòng
        public void setMultiSelectMode(bool setCheck)
        {
            setCheckBox = setCheck;
            gridView1.OptionsSelection.MultiSelect = setCheck;

            if (setCheck)
            {
                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 30;
            }
        }
        bool inSelectorColumnHeader = false;
        private void gridControl1_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (setCheckBox && inSelectorColumnHeader)
            {
                GridView view = gridControl1.FocusedView as GridView;
                if (view == null) return;
                if (e.KeyCode == Keys.Space)
                {
                    if (view.SelectedRowsCount == view.RowCount)
                    {
                        //gridView1.ClearSelection(); 
                        for (int i = 0; i < gridView1.RowCount; i++)
                            if (i != gridView1.FocusedRowHandle)
                                gridView1.UnselectRow(i); 
                    }
                    else
                    {
                        //gridView1.SelectAll();
                        for (int i = 0; i < gridView1.RowCount; i++)
                            if (i != gridView1.FocusedRowHandle)
                                gridView1.SelectRow(i);
                    }
                }
            }
        }
        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (setCheckBox)
            {
                GridHitInfo hi = gridView1.CalcHitInfo(e.Location);
                inSelectorColumnHeader = false;
                if (hi.InColumn && !hi.InRow)
                {
                    if (hi.Column.FieldName == GridView.CheckBoxSelectorColumnName)
                    {
                        inSelectorColumnHeader = true;
                    }
                }
            }
        }

        #endregion

        #region tạo cột Số Thứ Tự - tự động update stt khi số dòng thay đổi vị trí, thêm bớt.
        //Đánh stt có sẵn của grid - chỉ dùng cho grid không phân trang
        bool bIndicator = false;
        string indicatorHeader;
        public void onIndicator()
        {
            onIndicator("", 45);
        }
        public void onIndicator(string indicatorHeader, int indicatorWidth)
        {
            bIndicator = true;
            gridView1.CustomDrawRowIndicator += gridView1_CustomDrawRowIndicator;
            this.indicatorHeader = indicatorHeader;
            gridView1.IndicatorWidth = indicatorWidth;
        }
        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (bIndicator)
            {
                if (!String.IsNullOrEmpty(indicatorHeader) && e.Info.Kind == DevExpress.Utils.Drawing.IndicatorKind.Header)
                {
                    e.Appearance.DrawBackground(e.Cache, e.Bounds);
                    e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    e.Appearance.DrawString(e.Cache, indicatorHeader, e.Bounds);
                    e.Handled = true;
                }
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1 + (ucPage1.Current() - 1) * ucPage1.getNumberPerPage()).ToString();
                    //        e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    //        e.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                }
            }
        }

        ////Đánh stt dùng cho grid có phân trang, mỗi trang là 1 lần request lấy dl từ sv
        //string columnSTT = "";
    //    public void setColumnSTT(string columnName, int columnIndex, string caption, int width)
    //    {
    //        //columnSTT = columnName;
    //        //this.setColumn(columnSTT, columnIndex, caption, width);
    //        //reset_STT();
    //}
    //public void setColumnSTT(string columnName, int columnIndex, string caption)
    //{
    //    //columnSTT = columnName;
    //    //this.setColumn(columnSTT, columnIndex, caption, 0);
    //    //reset_STT();
    //}
    //private void reset_STT()
    //{
    //    DataTable dt = (DataTable)gridControl1.DataSource;
    //    if (dt == null || dt.Columns.Contains(columnSTT) == false) return;

    //    int dem = 1 + (ucPage1.Current() - 1) * ucPage1.getNumberPerPage();
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //        if (dt.Rows[i].RowState == DataRowState.Added)
    //            dt.Rows[i][columnSTT] = "" + (dem++);
    //}
    //private void gridView1_RowCountChanged(object sender, EventArgs e)
    //{
    //    reset_STT();
    //}
    #endregion

        #region tạo cột icon theo giá trị của cột
    public void setColumnImage(int columnIndex, string[] listValues, string[] listImages)
        {
            if (gridControl1.DataSource == null) return;

            setColumnImage(gridView1.Columns[columnIndex].FieldName, listValues, listImages);
        }
        public void setColumnImage(string columnName, object[] listValues, string[] listImages)
        {
            try
            {
                if (gridControl1.DataSource == null) return;

                DevExpress.Utils.ImageCollection imageCollection = new DevExpress.Utils.ImageCollection();
                for (int i = 0; i < listImages.Length; i++)
                    imageCollection.AddImage(System.Drawing.Image.FromFile(listImages[i]));

                DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                //ucGrid_DsBN.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                //        repositoryItemImageComboBox1 });

                //repositoryItemImageComboBox1.AutoHeight = false;
                //repositoryItemImageComboBox1.BestFitWidth = 200;
                //repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                //    new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});

                for (int i = 0; i < listValues.Length; i++)
                    repositoryItemImageComboBox1.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", listValues[i], i));

                repositoryItemImageComboBox1.SmallImages = imageCollection; // imageCollection_TTKham;

                gridView1.Columns[columnName].ColumnEdit = repositoryItemImageComboBox1;
                repositoryItemImageComboBox1.BestFitWidth = 2;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        public void setColumnImage(string columnName, object[] listValues, System.Drawing.Image[] listImages)
        {
            try
            {
                if (gridControl1.DataSource == null) return;

                DevExpress.Utils.ImageCollection imageCollection = new DevExpress.Utils.ImageCollection();
                for (int i = 0; i < listImages.Length; i++)
                    imageCollection.AddImage(listImages[i]);

                DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
                //ucGrid_DsBN.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                //        repositoryItemImageComboBox1 });

                //repositoryItemImageComboBox1.AutoHeight = false;
                //repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                //    new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});

                for (int i = 0; i < listValues.Length; i++)
                    repositoryItemImageComboBox1.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", listValues[i], i));

                repositoryItemImageComboBox1.SmallImages = imageCollection; // imageCollection_TTKham;

                gridView1.Columns[columnName].ColumnEdit = repositoryItemImageComboBox1;
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        #endregion

        #region tạo 1 cột chứa button
        int nButton = 0;
        protected EventHandler[] event_ColumnButtonClick = new EventHandler[99];
        protected string[] ButtonName = new string[99];
        public void setColumnButtonImage(string columnName, string icon, EventHandler eventClick)
        {
            DataTable dt = (DataTable)gridControl1.DataSource;
            if (dt == null || !dt.Columns.Contains(columnName)) return;

            event_ColumnButtonClick[nButton] = eventClick;
            ButtonName[nButton] = columnName;
            nButton++;

            DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit btn = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            btn.Name = columnName;
            btn.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            btn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false,
                DevExpress.XtraEditors.ImageLocation.MiddleCenter, HIS.Common.Func.getIcon(icon),
                new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None),
                new DevExpress.Utils.SerializableAppearanceObject(), "", null, null, true)});
            btn.ButtonClick += event_ButtonClick;

            //Thêm
            gridView1.GridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { btn });
            gridView1.Columns[columnName].ColumnEdit = btn;
            gridView1.Columns[columnName].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            resetColumnAllowEdit();
        }

        public void setColumnMemoEdit(string columnName, int index, string caption, int width)
        {
            try
            {
                DataTable dt = (DataTable)gridControl1.DataSource;
                if (dt == null || !dt.Columns.Contains(columnName)) return;

                DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memo = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                memo.ScrollBars = ScrollBars.None;
                //Thêm  
                gridView1.Columns[columnName].ColumnEdit = memo;
                gridView1.Columns[columnName].Caption = caption;
                gridView1.Columns[columnName].VisibleIndex = index; //gridView1.Columns[columnName].Visible = visible;
                if (width > 0) gridView1.Columns[columnName].Width = width;

                resetColumnAllowEdit();
            }
            catch (Exception ex) {
                setColumn(columnName, index, caption, width);
            }
        }
        public void setColumnButton(string columnName, EventHandler eventClick)
        {
            DataTable dt = (DataTable)gridControl1.DataSource;
            if (dt == null || !dt.Columns.Contains(columnName)) return;

            event_ColumnButtonClick[nButton] = eventClick;
            ButtonName[nButton] = columnName;
            nButton++;

            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btn = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            btn.Name = columnName;

            btn.Click += event_ButtonClick;
            gridView1.Columns[columnName].ColumnEdit = btn;

            resetColumnAllowEdit();
        }
        private void event_ButtonClick(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.ButtonEdit btn = (DevExpress.XtraEditors.ButtonEdit)sender;

            for (int i = 0; i < nButton; i++)
                if (btn.Properties.Name == ButtonName[i])
                {
                    if (event_ColumnButtonClick[i] != null) event_ColumnButtonClick[i](sender, e);
                    break;
                }
        }
        private void resetColumnAllowEdit()
        {
            if (event_DoubleClick != null || event_FocusedRowChanged != null)
                for (int i = 0; i < gridView1.Columns.Count; i++)
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;

            for (int i = 0; i < nButton; i++)
                gridView1.Columns[ButtonName[i]].OptionsColumn.AllowEdit = true;
        }
        #endregion

        #region menu chuột phải popup
        List<DXMenuItem> menuItems = new List<DXMenuItem>();
        public void addMenuPopup(List<MenuFunc> listMenu)
        {
            try
            {
                menuItems = new List<DXMenuItem>();
                for (int i = 0; i < listMenu.Count; i++) addMenu(listMenu[i], null);
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void addMenu(MenuFunc menu, DXSubMenuItem subItem)
        {
            if (menu.hlink != "" && menu.children == null) // Button
            {
                DXMenuItem button = new DXMenuItem(menu.text, ItemMenuPopup_Click);
                button.Tag = JsonConvert.SerializeObject(menu);
                button.Image = Func.getIcon(menu.icon);
                button.Appearance.Font = Const.fontDefault;

                if (subItem == null) menuItems.Add(button);
                else subItem.Items.Add(button);
            }
            else if (menu.hlink == "" && menu.children != null) // menu cha
            {
                DXSubMenuItem parent = new DXSubMenuItem(menu.text);
                parent.Image = Func.getIcon(menu.icon);
                parent.Appearance.Font = Const.fontDefault;

                if (subItem == null) menuItems.Add(parent);
                else subItem.Items.Add(parent);

                for (int i = 0; i < menu.children.Count; i++)
                    addMenu(menu.children[i], parent);

            }
            else if (menu.hlink == "" && menu.children == null) // tiêu đề
            {
                DXMenuHeaderItem menuHeader = new DXMenuHeaderItem();
                menuHeader.Caption = menu.text;
                menuHeader.Image = Func.getIcon(menu.icon);
                menuHeader.Appearance.Font = Const.fontDefault;

                if (subItem == null) menuItems.Add(menuHeader);
                else subItem.Items.Add(menuHeader);
            }

        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;

                //if (radioGroup1.EditValue.ToString() == "Standard Menu")
                //    ContextMenu1.Show(view.GridControl, e.Point);

                //if (radioGroup1.EditValue.ToString() == "DevExpress Menu")
                {
                    foreach (DXMenuItem item in menuItems)
                        e.Menu.Items.Add(item);
                }
            }
        }

        protected EventHandler event_MenuPopupClick;
        public void setEvent_MenuPopupClick(EventHandler eventMenuPopupClick)
        {
            event_MenuPopupClick = eventMenuPopupClick;
        }
        private void ItemMenuPopup_Click(object sender, System.EventArgs e)
        {
            //gridView1.ShowEditor();
            if (event_MenuPopupClick != null)
            {
                DXMenuItem button = (DXMenuItem)sender;
                MenuFunc menu = JsonConvert.DeserializeObject<MenuFunc>(button.Tag.ToString());

                event_MenuPopupClick(menu, null);
            }
        }

        //DevExpress.Utils.Menu.DXSubMenuItem
        //DevExpress.Utils.Menu.DXMenuHeaderItem
        //DevExpress.Utils.Menu.DXMenuItem
        private bool MenuPopup_Option(string EnabledVisible, string TypeofMenu, bool show, string title, DXMenuItem item)
        {
            if (item.GetType().ToString() == TypeofMenu && item.Caption.ToLower() == title.ToLower()) //
            {
                if (EnabledVisible == "Enabled")
                    item.Enabled = show;
                else if (EnabledVisible == "Visible")
                    item.Visible = show;
                return true;
            }

            if (item.GetType().ToString() == "DevExpress.Utils.Menu.DXSubMenuItem") // menu cha
            {
                DXSubMenuItem parent = (DXSubMenuItem)item;
                foreach (DXMenuItem itemChild in parent.Items)
                    if (MenuPopup_Option(EnabledVisible, TypeofMenu, show, title, itemChild)) return true;
            }

            return false;
        }
        private void MenuPopup_Option(string EnabledVisible, string TypeofMenu, bool show, string title)
        {
            foreach (DXMenuItem item in menuItems)
            {
                if (MenuPopup_Option(EnabledVisible, TypeofMenu, show, title, item)) return;
            }
        }

        public void MenuPopup_Enable_Child_byTitle(bool show, string title)
        {
            MenuPopup_Option("Enabled", "DevExpress.Utils.Menu.DXMenuItem", show, title);
        }
        public void MenuPopup_Enable_Parent_byTitle(bool show, string title)
        {
            MenuPopup_Option("Enabled", "DevExpress.Utils.Menu.DXSubMenuItem", show, title);
        }
        public void MenuPopup_Visible_Child_byTitle(bool show, string title)
        {
            MenuPopup_Option("Visible", "DevExpress.Utils.Menu.DXMenuItem", show, title);
        }

        public void MenuPopup_Visible_Parent_byTitle(bool show, string title)
        {
            MenuPopup_Option("Visible", "DevExpress.Utils.Menu.DXSubMenuItem", show, title);
        }
        #endregion

        #region Filter - search theo cột           
        private int wait_time = 1200;// 1300          
        private System.Timers.Timer aTimer = null;
        private void initTimer()
        {
            if (aTimer == null)
            {
                aTimer = new System.Timers.Timer(wait_time);
                aTimer.Elapsed += timer_Tick; 
            }
            else
            {
                if (aTimer.Enabled == false) aTimer.Start();
            } 
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            aTimer.Stop();

            reload_data();
        }

        //private void gridView1_ColumnFilterChanged_cu(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        try
        //        {
        //            if (gridView1.ActiveEditor is TextEdit)
        //            {
        //                TextEdit textbox = (TextEdit)gridView1.ActiveEditor;
        //                string x = textbox.Text.Trim();
        //                textbox.Text = " ";
        //                textbox.Text = x;
        //            }
        //        }
        //        catch (Exception ex) { } 

        //        if (ReLoadWhenFilter == false) return;
        //        if (filterColumnName == "") return; 
        //        if (isFiltering) return;

        //        //isFiltering = true; 

        //        //if (gridView1.EditingValue != null)
        //        //{
        //        //    string text = gridView1.EditingValue.ToString().Trim();
        //        //    System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff")+" =======  "+ text);
        //        //    int i = 0;
        //        //    for (i = tableFlterColumn.Rows.Count-1; i >=0 ; i--)
        //        //        if (tableFlterColumn.Rows[i]["field"].ToString() == filterColumnName)
        //        //        {
        //        //            if (text == "") tableFlterColumn.Rows.RemoveAt(i);
        //        //            else tableFlterColumn.Rows[i]["data"] = text;
        //        //            break;
        //        //        }
        //        //    if (text != "" && i < 0) tableFlterColumn.Rows.Add(filterColumnName, "cn", text);
        //        //    aTimer.Interval = wait_time; // khởi tạo lại thời gian chờ nếu có bất kỳ phím bấm nào
        //        //    System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") + " =======  " + tableFlterColumn.Rows.Count);

        //        //    //System.Console.WriteLine("filter=" + tableFlterColumn.Rows.Count);
        //        //    if (number_startimer == 0)
        //        //    {
        //        //        System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") + " "+number_startimer+" =======  " + aTimer.Enabled);
        //        //        number_startimer++;
        //        //        if (aTimer.Enabled == false) aTimer.Start(); 
        //        //    }
        //        //    else if (b_reload_data) 
        //        //    {
        //        //        System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") + " "+number_startimer + " =======  -> aTimer_init true");
        //        //        //System.Console.WriteLine("filter="+ Newtonsoft.Json.JsonConvert.SerializeObject(tableFlterColumn));
        //        //        b_reload_data = false;
        //        //        reload_data(2);
        //        //        aTimer_init.Start();
        //        //    } 
        //        //}

        //        //isFiltering = false;
        //    }
        //    catch (Exception ex) { log.Fatal(ex.ToString()); }
        //}
      
        private void gridView1_ColumnFilterChanged(object sender, EventArgs e)
        {
            try
            { 
                if (ReLoadWhenFilter == false) return;
                 
                initTimer();
                aTimer.Interval = wait_time; // khởi tạo lại thời gian chờ nếu có bất kỳ phím bấm nào 
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        
        private void reload_data()
        { 
            if (gridControl1.InvokeRequired)
            {
                gridControl1.BeginInvoke(new MethodInvoker(delegate ()
                {
                    TextEdit edit = gridView1.ActiveEditor as TextEdit; 
                    if (edit != null && edit.Text.Trim() != edit.Text)
                    {
                        edit.Text = edit.Text.Trim();
                        //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") + "  xoa_space=" + edit.Text + "#");
                    }

                    if (ucPage1.page_change != null) ucPage1.page_change(1, null);
                }));
            }
            else
            {
                lock (gridControl1)
                {
                   TextEdit edit = gridView1.ActiveEditor as TextEdit;
                    if (edit != null && edit.Text.Trim() != edit.Text)
                    {
                        edit.Text = edit.Text.Trim();
                        //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") + "  xoa_space=" + edit.Text + "#");
                    }

                    ucPage1.page_change(1, null);
                }
            }
        }
        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            TextEdit edit = gridView1.ActiveEditor as TextEdit;
            if (edit != null && edit.Text.Trim() != edit.Text)
            {
                edit.Text = edit.Text.Trim();
                //System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") + "  xoa_space=" + edit.Text + "#");
            }
        }
        private void gridView1_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        { 
        }
        public string jsonFilter()
        {
            string ret = "";
            DataTable tableFlter = new DataTable();
       
            tableFlter.Columns.Add("field");
            tableFlter.Columns.Add("op");
            tableFlter.Columns.Add("data");

            DevExpress.XtraGrid.Views.Base.ViewFilter colFilter = gridView1.ActiveFilter;
            for (int i = 0; i < colFilter.Count; i++)
            {
                string name = colFilter[i].Column.FieldName;
                string value = colFilter[i].Column.FilterInfo.Value.ToString();

                if (!string.IsNullOrEmpty(value))
                    tableFlter.Rows.Add(name, "cn", value); 
            }

            if (tableFlter.Rows.Count>0) ret = Newtonsoft.Json.JsonConvert.SerializeObject(tableFlter);

            //  gridView1.ActiveFilter.DisplayText  = (Contains([MABENHNHAN], ' bn')) AND(Contains([TENBENHNHAN], ' ha'))
            // Cần:  [{ "field":"MABENHNHAN", "op":"cn", "data":"bn"},{"field":"TENBENHNHAN","op":"cn","data":"ha"}]
            return ret;
        }
        #endregion


        #region Set lại tập các số bản ghi/trang - Mặc định là 20-100-200-300
        public void setNumberPerPage(int[] listNumber)
        {
            ucPage1.setNumberPerPage(listNumber);
        }
        public void setNumberPerPage(int[] listNumber, int indexDefault)
        {
            ucPage1.setNumberPerPage(listNumber, indexDefault);
        }
        //lấy số bản ghi/page
        public int getNumberPerPage()
        {
            return ucPage1.getNumberPerPage();
        }



        #endregion


    }
}
