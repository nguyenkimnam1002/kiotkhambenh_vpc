namespace VNPT.HIS.MainForm
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ribbonControl1 = new VNPT.HIS.UserControl.ucMyRibbonControl();
            this.mnuArrangeHorizontal = new DevExpress.XtraBars.BarButtonItem();
            this.mnuArrangeCascade = new DevExpress.XtraBars.BarButtonItem();
            this.mnuCloseAllWindows = new DevExpress.XtraBars.BarButtonItem();
            this.mnuWindowsOpen = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.btnAbout = new DevExpress.XtraBars.BarButtonItem();
            this.rbCuaSo = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.mnuArrangeVertical = new DevExpress.XtraBars.BarButtonItem();
            this.rbGioiThieu = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup11 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnHelp = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnDangNhap = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnThayDoiService = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnSkin = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.btnThoat = new DevExpress.XtraBars.BarButtonItem();
            this.btnSetupSQLite = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.lbTime = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.lbUser = new DevExpress.XtraBars.BarStaticItem();
            this.lbUserNhom = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.lbKhoa = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.lbPhong = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockingMenuItem1 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barDockingMenuItem2 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.barDockingMenuItem3 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.rbLoginMenu = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbLoginMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.mnuArrangeHorizontal,
            this.mnuArrangeCascade,
            this.mnuCloseAllWindows,
            this.mnuWindowsOpen,
            this.btnAbout});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ribbonControl1.MaxItemId = 15;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rbCuaSo,
            this.rbGioiThieu});
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowGroupCaption = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(908, 99);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.Visible = false;
            // 
            // mnuArrangeHorizontal
            // 
            this.mnuArrangeHorizontal.Caption = "Xếp ngang";
            this.mnuArrangeHorizontal.Glyph = ((System.Drawing.Image)(resources.GetObject("mnuArrangeHorizontal.Glyph")));
            this.mnuArrangeHorizontal.Id = 3;
            this.mnuArrangeHorizontal.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuArrangeHorizontal.ItemAppearance.Hovered.Options.UseFont = true;
            this.mnuArrangeHorizontal.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuArrangeHorizontal.ItemAppearance.Normal.Options.UseFont = true;
            this.mnuArrangeHorizontal.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuArrangeHorizontal.LargeGlyph")));
            this.mnuArrangeHorizontal.LargeWidth = 80;
            this.mnuArrangeHorizontal.Name = "mnuArrangeHorizontal";
            this.mnuArrangeHorizontal.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuArrangeHorizontal_ItemClick);
            // 
            // mnuArrangeCascade
            // 
            this.mnuArrangeCascade.Caption = "Xếp chồng";
            this.mnuArrangeCascade.Glyph = ((System.Drawing.Image)(resources.GetObject("mnuArrangeCascade.Glyph")));
            this.mnuArrangeCascade.Id = 4;
            this.mnuArrangeCascade.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuArrangeCascade.ItemAppearance.Hovered.Options.UseFont = true;
            this.mnuArrangeCascade.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuArrangeCascade.ItemAppearance.Normal.Options.UseFont = true;
            this.mnuArrangeCascade.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuArrangeCascade.LargeGlyph")));
            this.mnuArrangeCascade.LargeWidth = 80;
            this.mnuArrangeCascade.Name = "mnuArrangeCascade";
            this.mnuArrangeCascade.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuArrangeCascade_ItemClick);
            // 
            // mnuCloseAllWindows
            // 
            this.mnuCloseAllWindows.Caption = "Đóng tất cả cửa sổ";
            this.mnuCloseAllWindows.Glyph = ((System.Drawing.Image)(resources.GetObject("mnuCloseAllWindows.Glyph")));
            this.mnuCloseAllWindows.Id = 5;
            this.mnuCloseAllWindows.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuCloseAllWindows.ItemAppearance.Hovered.Options.UseFont = true;
            this.mnuCloseAllWindows.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuCloseAllWindows.ItemAppearance.Normal.Options.UseFont = true;
            this.mnuCloseAllWindows.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuCloseAllWindows.LargeGlyph")));
            this.mnuCloseAllWindows.Name = "mnuCloseAllWindows";
            this.mnuCloseAllWindows.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuCloseAllWindows_ItemClick);
            // 
            // mnuWindowsOpen
            // 
            this.mnuWindowsOpen.Caption = "Cửa sổ đang mở";
            this.mnuWindowsOpen.Glyph = ((System.Drawing.Image)(resources.GetObject("mnuWindowsOpen.Glyph")));
            this.mnuWindowsOpen.Id = 6;
            this.mnuWindowsOpen.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuWindowsOpen.ItemAppearance.Hovered.Options.UseFont = true;
            this.mnuWindowsOpen.ItemAppearance.Hovered.Options.UseForeColor = true;
            this.mnuWindowsOpen.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuWindowsOpen.ItemAppearance.Normal.Options.UseFont = true;
            this.mnuWindowsOpen.ItemAppearance.Normal.Options.UseForeColor = true;
            this.mnuWindowsOpen.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuWindowsOpen.LargeGlyph")));
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Hovered.ForeColor = System.Drawing.Color.Blue;
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Hovered.Options.UseFont = true;
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Hovered.Options.UseForeColor = true;
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Normal.ForeColor = System.Drawing.Color.Blue;
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Normal.Options.UseFont = true;
            this.mnuWindowsOpen.MenuAppearance.AppearanceMenu.Normal.Options.UseForeColor = true;
            this.mnuWindowsOpen.Name = "mnuWindowsOpen";
            // 
            // btnAbout
            // 
            this.btnAbout.Caption = "Giới thiệu";
            this.btnAbout.Glyph = ((System.Drawing.Image)(resources.GetObject("btnAbout.Glyph")));
            this.btnAbout.Id = 8;
            this.btnAbout.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnAbout.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbout.ItemAppearance.Normal.Options.UseFont = true;
            this.btnAbout.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnAbout.LargeGlyph")));
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAbout_ItemClick);
            // 
            // rbCuaSo
            // 
            this.rbCuaSo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rbCuaSo.Appearance.Options.UseFont = true;
            this.rbCuaSo.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup6});
            this.rbCuaSo.Name = "rbCuaSo";
            this.rbCuaSo.Text = "Cửa sổ";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.mnuArrangeVertical, true);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnuArrangeHorizontal, true);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnuArrangeCascade, true);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnuCloseAllWindows, true);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnuWindowsOpen, true);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.Text = "ribbonPageGroup6";
            // 
            // mnuArrangeVertical
            // 
            this.mnuArrangeVertical.Caption = "Xếp dọc";
            this.mnuArrangeVertical.Glyph = ((System.Drawing.Image)(resources.GetObject("mnuArrangeVertical.Glyph")));
            this.mnuArrangeVertical.Id = 2;
            this.mnuArrangeVertical.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mnuArrangeVertical.ItemAppearance.Hovered.Options.UseFont = true;
            this.mnuArrangeVertical.ItemAppearance.Normal.BackColor = System.Drawing.Color.Transparent;
            this.mnuArrangeVertical.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuArrangeVertical.ItemAppearance.Normal.Options.UseBackColor = true;
            this.mnuArrangeVertical.ItemAppearance.Normal.Options.UseFont = true;
            this.mnuArrangeVertical.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("mnuArrangeVertical.LargeGlyph")));
            this.mnuArrangeVertical.Name = "mnuArrangeVertical";
            this.mnuArrangeVertical.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuArrangeVertical_ItemClick);
            // 
            // rbGioiThieu
            // 
            this.rbGioiThieu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.rbGioiThieu.Appearance.Options.UseFont = true;
            this.rbGioiThieu.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup11});
            this.rbGioiThieu.Name = "rbGioiThieu";
            this.rbGioiThieu.Text = "Giới thiệu";
            // 
            // ribbonPageGroup11
            // 
            this.ribbonPageGroup11.ItemLinks.Add(this.btnHelp, true);
            this.ribbonPageGroup11.ItemLinks.Add(this.btnAbout, true);
            this.ribbonPageGroup11.Name = "ribbonPageGroup11";
            this.ribbonPageGroup11.Text = "ribbonPageGroup11";
            // 
            // btnHelp
            // 
            this.btnHelp.Caption = "Trợ giúp";
            this.btnHelp.Glyph = ((System.Drawing.Image)(resources.GetObject("btnHelp.Glyph")));
            this.btnHelp.Id = 7;
            this.btnHelp.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnHelp.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.ItemAppearance.Normal.Options.UseFont = true;
            this.btnHelp.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnHelp.LargeGlyph")));
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnHelp_ItemClick);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "barButtonItem6";
            this.barButtonItem6.Id = 11;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "barButtonItem7";
            this.barButtonItem7.Id = 12;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "test";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.LargeGlyph")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.ribbonPage1.Appearance.Options.UseFont = true;
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup1,
            this.ribbonPageGroup3,
            this.ribbonPageGroup5});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Hệ Thống";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnDangNhap);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Caption = "Đăng nhập";
            this.btnDangNhap.Glyph = ((System.Drawing.Image)(resources.GetObject("btnDangNhap.Glyph")));
            this.btnDangNhap.Id = 1;
            this.btnDangNhap.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnDangNhap.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.ItemAppearance.Normal.Options.UseFont = true;
            this.btnDangNhap.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnDangNhap.LargeGlyph")));
            this.btnDangNhap.LargeWidth = 80;
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDangNhap_ItemClick);
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Glyph = ((System.Drawing.Image)(resources.GetObject("ribbonPageGroup1.Glyph")));
            this.ribbonPageGroup1.ItemLinks.Add(this.btnThayDoiService, true);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // btnThayDoiService
            // 
            this.btnThayDoiService.Caption = "Thiết lập Server";
            this.btnThayDoiService.Glyph = ((System.Drawing.Image)(resources.GetObject("btnThayDoiService.Glyph")));
            this.btnThayDoiService.Id = 1;
            this.btnThayDoiService.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnThayDoiService.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnThayDoiService.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnThayDoiService.ItemAppearance.Normal.Options.UseFont = true;
            this.btnThayDoiService.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnThayDoiService.LargeGlyph")));
            this.btnThayDoiService.LargeWidth = 80;
            this.btnThayDoiService.Name = "btnThayDoiService";
            this.btnThayDoiService.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThayDoiService_ItemClick);
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnSkin);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Skin";
            // 
            // btnSkin
            // 
            this.btnSkin.Caption = "Skin";
            this.btnSkin.Id = 2;
            this.btnSkin.Name = "btnSkin";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.btnThoat);
            this.ribbonPageGroup5.ItemLinks.Add(this.btnSetupSQLite, true);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            // 
            // btnThoat
            // 
            this.btnThoat.Caption = "Thoát";
            this.btnThoat.Glyph = ((System.Drawing.Image)(resources.GetObject("btnThoat.Glyph")));
            this.btnThoat.Id = 3;
            this.btnThoat.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnThoat.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ItemAppearance.Normal.Options.UseFont = true;
            this.btnThoat.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ItemInMenuAppearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.btnThoat.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnThoat.ItemInMenuAppearance.Normal.Options.UseForeColor = true;
            this.btnThoat.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnThoat.LargeGlyph")));
            this.btnThoat.LargeWidth = 80;
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnThoat_ItemClick);
            // 
            // btnSetupSQLite
            // 
            this.btnSetupSQLite.Caption = "Cài đặt SQLite";
            this.btnSetupSQLite.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSetupSQLite.Glyph")));
            this.btnSetupSQLite.Id = 5;
            this.btnSetupSQLite.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSetupSQLite.ItemAppearance.Hovered.Options.UseFont = true;
            this.btnSetupSQLite.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSetupSQLite.ItemAppearance.Normal.Options.UseFont = true;
            this.btnSetupSQLite.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSetupSQLite.ItemInMenuAppearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.btnSetupSQLite.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnSetupSQLite.ItemInMenuAppearance.Normal.Options.UseForeColor = true;
            this.btnSetupSQLite.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnSetupSQLite.LargeGlyph")));
            this.btnSetupSQLite.LargeWidth = 80;
            this.btnSetupSQLite.Name = "btnSetupSQLite";
            this.btnSetupSQLite.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.lbTime,
            this.barStaticItem1,
            this.lbUser,
            this.barStaticItem2,
            this.lbKhoa,
            this.barStaticItem3,
            this.lbPhong,
            this.lbUserNhom});
            this.barManager1.MaxItemId = 8;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bar3.BarAppearance.Normal.Options.UseFont = true;
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.lbTime),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.lbUser),
            new DevExpress.XtraBars.LinkPersistInfo(this.lbUserNhom),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.lbKhoa),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.lbPhong)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // lbTime
            // 
            this.lbTime.Id = 0;
            this.lbTime.Name = "lbTime";
            this.lbTime.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItem1.Caption = "Người dùng:";
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lbUser
            // 
            this.lbUser.Id = 2;
            this.lbUser.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbUser.ItemAppearance.Normal.Options.UseFont = true;
            this.lbUser.Name = "lbUser";
            this.lbUser.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // lbUserNhom
            // 
            this.lbUserNhom.Id = 7;
            this.lbUserNhom.Name = "lbUserNhom";
            this.lbUserNhom.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItem2.Caption = "Khoa:";
            this.barStaticItem2.Id = 3;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barStaticItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // lbKhoa
            // 
            this.lbKhoa.Id = 4;
            this.lbKhoa.Name = "lbKhoa";
            this.lbKhoa.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItem3.Caption = "Phòng:";
            this.barStaticItem3.Id = 5;
            this.barStaticItem3.Name = "barStaticItem3";
            this.barStaticItem3.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barStaticItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // lbPhong
            // 
            this.lbPhong.Id = 6;
            this.lbPhong.Name = "lbPhong";
            this.lbPhong.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(908, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 436);
            this.barDockControlBottom.Size = new System.Drawing.Size(908, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 436);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(908, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 436);
            // 
            // barDockingMenuItem1
            // 
            this.barDockingMenuItem1.Caption = "barDockingMenuItem1";
            this.barDockingMenuItem1.Id = 4;
            this.barDockingMenuItem1.Name = "barDockingMenuItem1";
            this.barDockingMenuItem1.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "barSubItem2";
            this.barSubItem2.Id = 8;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 7;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barDockingMenuItem2
            // 
            this.barDockingMenuItem2.Caption = "barDockingMenuItem2";
            this.barDockingMenuItem2.Id = 5;
            this.barDockingMenuItem2.Name = "barDockingMenuItem2";
            // 
            // barDockingMenuItem3
            // 
            this.barDockingMenuItem3.Caption = "barDockingMenuItem3";
            this.barDockingMenuItem3.Id = 6;
            this.barDockingMenuItem3.Name = "barDockingMenuItem3";
            // 
            // rbLoginMenu
            // 
            this.rbLoginMenu.AllowMdiChildButtons = false;
            this.rbLoginMenu.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            this.rbLoginMenu.ExpandCollapseItem.Id = 0;
            this.rbLoginMenu.Font = new System.Drawing.Font("Tahoma", 9F);
            this.rbLoginMenu.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rbLoginMenu.ExpandCollapseItem,
            this.btnDangNhap,
            this.btnThayDoiService,
            this.btnSkin,
            this.btnThoat,
            this.btnSetupSQLite});
            this.rbLoginMenu.Location = new System.Drawing.Point(0, 99);
            this.rbLoginMenu.MaxItemId = 6;
            this.rbLoginMenu.Name = "rbLoginMenu";
            this.rbLoginMenu.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.rbLoginMenu.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
            this.rbLoginMenu.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.rbLoginMenu.Size = new System.Drawing.Size(908, 77);
            this.rbLoginMenu.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Xếp dọc";
            this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.barButtonItem2.ItemAppearance.Hovered.ForeColor = System.Drawing.Color.Blue;
            this.barButtonItem2.ItemAppearance.Hovered.Options.UseFont = true;
            this.barButtonItem2.ItemAppearance.Hovered.Options.UseForeColor = true;
            this.barButtonItem2.ItemAppearance.Normal.BackColor = System.Drawing.Color.Transparent;
            this.barButtonItem2.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonItem2.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.barButtonItem2.ItemAppearance.Normal.Options.UseBackColor = true;
            this.barButtonItem2.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem2.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barButtonItem2.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.LargeGlyph")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Xếp dọc";
            this.barButtonItem3.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.Glyph")));
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ItemAppearance.Hovered.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.barButtonItem3.ItemAppearance.Hovered.ForeColor = System.Drawing.Color.Blue;
            this.barButtonItem3.ItemAppearance.Hovered.Options.UseFont = true;
            this.barButtonItem3.ItemAppearance.Hovered.Options.UseForeColor = true;
            this.barButtonItem3.ItemAppearance.Normal.BackColor = System.Drawing.Color.Transparent;
            this.barButtonItem3.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barButtonItem3.ItemAppearance.Normal.ForeColor = System.Drawing.Color.Blue;
            this.barButtonItem3.ItemAppearance.Normal.Options.UseBackColor = true;
            this.barButtonItem3.ItemAppearance.Normal.Options.UseFont = true;
            this.barButtonItem3.ItemAppearance.Normal.Options.UseForeColor = true;
            this.barButtonItem3.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem3.LargeGlyph")));
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "barButtonItem4";
            this.barButtonItem4.Id = 9;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "barButtonItem5";
            this.barButtonItem5.Id = 10;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "barButtonItem8";
            this.barButtonItem8.Id = 13;
            this.barButtonItem8.Name = "barButtonItem8";
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "barButtonItem9";
            this.barButtonItem9.Id = 14;
            this.barButtonItem9.Name = "barButtonItem9";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::VNPT.HIS.MainForm.Properties.Resources.bground;
            this.ClientSize = new System.Drawing.Size(908, 461);
            this.Controls.Add(this.rbLoginMenu);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.ShowMdiChildCaptionInParentTitle = true;
            this.Text = " Phần Mềm Quản Lý Hệ Thống Thông Tin Bệnh viện";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MdiChildActivate += new System.EventHandler(this.frmMain_MdiChildActivate);
            this.ParentChanged += new System.EventHandler(this.frmMain_ParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbLoginMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VNPT.HIS.UserControl.ucMyRibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraBars.BarStaticItem lbTime;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem lbUser;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        public DevExpress.XtraBars.BarStaticItem lbKhoa;
        private DevExpress.XtraBars.BarStaticItem barStaticItem3;
        public DevExpress.XtraBars.BarStaticItem lbPhong;
        private DevExpress.XtraBars.BarStaticItem lbUserNhom;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem2;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem3;
        private DevExpress.XtraBars.Ribbon.RibbonControl rbLoginMenu;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.BarButtonItem btnDangNhap;
        private DevExpress.XtraBars.BarButtonItem btnThayDoiService;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem btnSkin;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem mnuArrangeVertical;
        private DevExpress.XtraBars.BarButtonItem mnuArrangeHorizontal;
        private DevExpress.XtraBars.BarButtonItem mnuArrangeCascade;
        private DevExpress.XtraBars.BarButtonItem mnuCloseAllWindows;
        private DevExpress.XtraBars.BarMdiChildrenListItem mnuWindowsOpen;
        private DevExpress.XtraBars.BarButtonItem btnHelp;
        private DevExpress.XtraBars.BarButtonItem btnAbout;
        private DevExpress.XtraBars.BarButtonItem btnThoat;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rbCuaSo;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.Ribbon.RibbonPage rbGioiThieu;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup11;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;
        private DevExpress.XtraBars.BarButtonItem btnSetupSQLite;
    }
}