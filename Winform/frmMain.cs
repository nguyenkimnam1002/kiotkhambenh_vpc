using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Globalization;
using DevExpress.LookAndFeel;
using VNPT.HIS.MainForm.Properties;

using VNPT.HIS.MainForm.HeThong;
using VNPT.HIS.Common;
using VNPT.HIS.NgoaiTru;
using VNPT.HIS.BaoCao;
using VNPT.HIS.MainForm.Class;

using System.Deployment.Application;

namespace VNPT.HIS.MainForm
{

    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        MainThread mainThread = new MainThread();
        public static frmMain Current;

        public frmMain()
        {
            InitializeComponent();
            ribbonControl1.Pages.Clear();

        }
        private void Init()
        {
            Current = this;
            Const.screen = this.Size;

            Const.INIT();

            this.Text = Const.SubDomain + "-" + Text;

            string temp = Const.FolderSaveFilePrint;
            if (Const.FolderSaveFilePrint == "") temp = "temp";

            try
            {
                //System.I O.Directory.Delete(temp, true);
            }
            catch (Exception ex) { }
            try
            {
                Directory.CreateDirectory(temp);
            }
            catch (Exception ex) { }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            log.Info("APP START");

            Init();

            openMenu(new MenuFunc("Đăng nhập", "VNPT.HIS.MainForm.HeThong.frmLogin", "1", ""));
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            appDispose();
        }

        private void appDispose()
        {
            mainThread.stop(); 
            this.Dispose();
        }
        public void appLogout()
        {
            Const.local_user = null;
            foreach (Form childForm in MdiChildren)
                childForm.Close();

            ribbonControl1.Pages.Clear();
            ribbonControl1.Visible = false;
            rbLoginMenu.Visible = true;

            openMenu(new MenuFunc("Đăng nhập", "VNPT.HIS.MainForm.HeThong.frmLogin", "1", ""));
        }

        public void DangNhapThanhCong(UserLogin user)
        {
            log.Info("Login thành công!" + (user != null));
            mainThread.start();
            //Đánh dấu trong thời gian session đăng nhập thành công
            Const.login_timeout = 1;

            //CloseAllchildForm();
            //Hiển thị tên đn
            ReloadTenDangNhap(user);

            // Phân quyền menu cho user
            ReloadMenu();

            // Lấy ngày giờ của hệ thống
            Get_System_Datetime();

            //
            Select_KhoaPhong();

            // trước đó chưa thiết lập - thường là lần đầu tiên chạy app chưa lưu cache
            // hoặc check lại khoa phòng có bị thay đổi ko? - do thay đổi trên app khác hoặc trên web
            if (Const.local_khoa == "" || Const.local_phong == ""
                || Const.local_khoaId == 0 || Const.local_phongId == 0
                || check_thay_doi_KhoaPhong()
                )

                Open_KhoaPhong();
            else
            {
                Show_KhoaPhong(Const.local_khoa, Const.local_phong);
            }

        }
        public void Loi_Setup_sqlite()
        {
            btnSetupSQLite.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            MessageBox.Show("Thiếu cài đặt SQLite, bạn hãy vào menu Hệ Thống --> Cài đặt SQLite, tải và cài đặt bản bổ xung."); 
        }

        public void Update_KhoaPhong()
        {
            //update vào sqlite
            Const.SQLITE.SelectDept_updatename(Const.local_khoaId, Const.local_khoa, Const.local_phongId, Const.local_phong);
        }
        public void Select_KhoaPhong()
        {
            //lấy từ sqlite
            Const.SQLITE.SelectDept_getname(out Const.local_khoaId, out Const.local_khoa, out Const.local_phongId, out Const.local_phong);
        }
        public void Show_KhoaPhong(string khoa, string phong)
        {
            lbKhoa.Caption = khoa;
            lbPhong.Caption = phong;
        }
        public void Open_KhoaPhong()
        {
            openMenu(new MenuFunc("Thiết lập phòng", "VNPT.HIS.MainForm.HeThong.frmSelectDept", "1", ""));
        }
        private bool check_thay_doi_KhoaPhong()
        {
            int khoaId_new = ServiceSelectDept.getIdKhoa();
            int phongId_new = ServiceSelectDept.getIdPhong();

            if (Const.local_khoaId != khoaId_new || Const.local_phongId != phongId_new)
            {
                Const.local_khoaId = khoaId_new;
                Const.local_phongId = phongId_new;

                Const.local_khoa = "";
                Const.local_phong = "";

                return true;
            }
            else
                return false;
        }


        public void ReloadTenDangNhap(UserLogin user)
        {
            try
            {
                Const.local_user = user;
                Const.local_username = Const.local_user.USER_NAME;
                Const.SQLITE.cache_add("local_username", Const.local_username);

                lbUser.Caption = Const.local_user.USER_NAME;
                lbUserNhom.Caption = Const.local_user.FULL_NAME;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        public void Get_System_Datetime()
        {
            string sys_datetime = RequestHTTP.getSysDatetime();

            DateTime localdate = DateTime.Now;
            DateTime sysdate = Func.ParseDatetime(sys_datetime);

            Const.diffInSeconds = (sysdate - localdate).TotalSeconds;
        }
        public void ReloadMenu()
        {
            rbLoginMenu.Visible = false;
            ribbonControl1.Visible = true;

            List<MenuFunc> listMenu = RequestHTTP.Login_GetUserFunc();

            try
            {
                if (listMenu != null)
                    for (int i = 0; i < listMenu.Count; i++) addMenu(listMenu[i], true, null, null, 1);
                else
                {
                    DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage("Hệ thống");
                    ribbonPage1.Appearance.Font = Const.fontBoldDefault;
                    ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { new DevExpress.XtraBars.Ribbon.RibbonPageGroup() });
                    ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }

            // Menu CuaSo, skin, thoat 
            ribbonControl1.Pages[0].Groups[0].ItemLinks.Add(btnSkin, true);
            ribbonControl1.Pages[0].Groups[0].ItemLinks.Add(btnThoat, true);

            ribbonControl1.Pages.Add(rbCuaSo);
            ribbonControl1.Pages.Add(rbGioiThieu);

            ribbonControl1.Minimized = true;


            //ribbonControl1.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            //ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
        }

        private void addMenu(MenuFunc menu, bool page, DevExpress.XtraBars.Ribbon.RibbonPageGroup group, DevExpress.XtraBars.BarSubItem subItem, int level)
        {
            if (page)
            {
                DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage(menu.text);
                ribbonPage1.Appearance.Font = Const.fontBoldDefault;
                ribbonPage1.Appearance.Options.UseFont = true;

                DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupX = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();

                this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1 });

                if (menu.children != null)
                    for (int i = 0; i < menu.children.Count; i++)
                    {
                        if (i == 0 || i == 10)
                        {
                            ribbonPageGroupX = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
                            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroupX });
                        }
                        addMenu(menu.children[i], false, ribbonPageGroupX, null, level + 1);
                    }
            }
            else
            {
                if (menu.children == null)  //chức năng
                {
                    DevExpress.XtraBars.BarButtonItem btnItem = new DevExpress.XtraBars.BarButtonItem();
                    btnItem.Caption = menu.text;
                    btnItem.Description = JsonConvert.SerializeObject(menu);
                    btnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.menu_Click);

                    if (subItem != null)
                    {
                        btnItem.ItemInMenuAppearance.Hovered.Font = level < 3 ? new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold) : Const.fontBoldDefault;
                        btnItem.ItemInMenuAppearance.Hovered.Options.UseFont = true;

                        btnItem.ItemInMenuAppearance.Normal.Font = level < 3 ? new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold) : Const.fontBoldDefault;
                        btnItem.ItemInMenuAppearance.Normal.Options.UseFont = true;

                        btnItem.Glyph = getIcon(menu.hlink);

                        subItem.AddItem(btnItem);
                    }
                    else if (group != null)
                    {
                        btnItem.ItemAppearance.Hovered.Font = level < 3 ? new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold) : Const.fontBoldDefault;
                        btnItem.ItemAppearance.Hovered.Options.UseFont = true;

                        btnItem.ItemAppearance.Normal.Font = level < 3 ? new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold) : Const.fontBoldDefault;
                        btnItem.ItemAppearance.Normal.Options.UseFont = true;

                        btnItem.LargeGlyph = getIcon(menu.hlink);

                        group.ItemLinks.Add(btnItem, true);
                    }
                }
                else // menu cha
                {
                    DevExpress.XtraBars.BarSubItem barSubItem1 = new DevExpress.XtraBars.BarSubItem();
                    barSubItem1.Caption = menu.text;

                    barSubItem1.ItemAppearance.Hovered.Font = level < 3 ? new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold) : Const.fontBoldDefault;
                    barSubItem1.ItemAppearance.Hovered.Options.UseFont = true;

                    barSubItem1.ItemAppearance.Normal.Font = level < 3 ? new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold) : Const.fontBoldDefault;
                    barSubItem1.ItemAppearance.Normal.Options.UseFont = true;

                    group.ItemLinks.Add(barSubItem1, true);
                    string icon_parent = "";

                    if (menu.children != null)
                    {
                        if (menu.children.Count > 0) icon_parent = menu.children[0].hlink;
                        for (int i = 0; i < menu.children.Count; i++)
                        {
                            addMenu(menu.children[i], false, null, barSubItem1, level + 1);
                        }
                    }

                    barSubItem1.LargeGlyph = getIcon(icon_parent);
                }
            }


        }
        private System.Drawing.Image getIcon(string name)
        {
            try
            {
                if (name.IndexOf("&") > -1) name = name.Substring(0, name.IndexOf("&"));
                name = name.Replace("?", "").Replace("=", "");
                if (Const.listIcon.IndexOf("|" + name + ".") > -1)
                    return System.Drawing.Image.FromFile("./Resources/" + name + ".png");
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            return Const.imgDefault;//System.Drawing.Image.FromFile("./Resources/default.png");
        }
        private void menu_Click(object Sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                //(this, typeof(DevExpress.XtraWaitForm.WaitForm), true, true, false);

                DevExpress.XtraBars.BarButtonItem btnItem = (DevExpress.XtraBars.BarButtonItem)e.Item;
                MenuFunc menu = JsonConvert.DeserializeObject<MenuFunc>(btnItem.Description);
                openMenu(menu);
            }
            finally
            {
                //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void openMenu(MenuFunc menu)
        {
            if (menu == null) return;

            string formName = menu.text;
            string pathForm = menu.hlink;
            string para = "";
            if (pathForm.IndexOf("?") > -1)
            {
                para = pathForm.Substring(pathForm.IndexOf("?") + 1);
                pathForm = pathForm.Substring(0, pathForm.IndexOf("?"));
            }

            try
            {
                //string name = pathForm.Substring(pathForm.LastIndexOf(".") + 1);


                if (pathForm.Equals("VNPT.HIS.MainForm.HeThong.frmLogout"))
                {
                    appLogout();
                }
                else if (pathForm.Equals("VNPT.HIS.MainForm.HeThong.frmMain"))
                {
                    // trang chủ
                    miniAllchildForm("", "");
                }
                else if (pathForm.StartsWith("VNPT.HIS.MainForm.HeThong."))
                {
                    //if (!pathForm.EndsWith("frmChangePassword") && !pathForm.EndsWith("frmSelectDept"))
                    //    miniAllchildForm(pathForm, formName);

                    Form findF = findForm(pathForm, formName);
                    if (findF != null)
                    {
                        System.Console.WriteLine("đn ! null");
                        findF.WindowState = FormWindowState.Normal;
                        findF.Activate();
                    }
                    else
                    {
                        //System.Console.WriteLine("đn = null");
                        Type type = Type.GetType(pathForm);
                        Form frm = Activator.CreateInstance(type) as Form;
                        frm.Tag = formName;
                        frm.Name = pathForm;
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        if (menu.options == "0")
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.MdiParent = this;
                            frm.Show();
                        }
                        else
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();
                        }
                    }
                }
                else if (pathForm.LastIndexOf(".") > -1)
                {
                    //miniAllchildForm(pathForm, formName);

                    Form findF = findForm(pathForm, formName);
                    if (findF != null)
                    {
                        findF.WindowState = FormWindowState.Maximized;
                        findF.Activate();
                    }
                    else
                    {
                        string pathDll = pathForm.Substring(0, pathForm.LastIndexOf(".")) + ".dll";
                        Assembly ass = Assembly.LoadFrom(pathDll);
                        Form frm = (Form)ass.CreateInstance(pathForm);
                        frm.Tag = formName;
                        frm.Name = pathForm;
                        DevExpress.XtraEditors.LabelControl lbPara = new DevExpress.XtraEditors.LabelControl();
                        lbPara.Name = "lbPara";
                        lbPara.Text = para;
                        lbPara.Visible = false;
                        frm.Controls.Add(lbPara);

                        if (menu.options == "0")
                        {
                            frm.WindowState = FormWindowState.Maximized;
                            frm.MdiParent = this;
                            frm.Show();
                        }
                        else
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                //MessageBox.Show("Không mở được Form này!");
            }
        }
        private Form findForm(string name, string tag)
        {
            foreach (Form childForm in MdiChildren)
            {
                if (childForm.Name == name && childForm.Tag.ToString() == tag)
                    return childForm;
            }



            return null;
        }
        public void miniAllchildForm(string name, string tag)
        {
            try
            {
                foreach (Form childForm in MdiChildren)
                {
                    if ((childForm.Name != name || childForm.Tag.ToString() != tag)
                        && childForm.WindowState != FormWindowState.Minimized)

                        //childForm.WindowState = FormWindowState.Minimized;
                        childForm.Close();
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void frmMain_MdiChildActivate(object sender, EventArgs e)
        {

        }




        private void frmMain_ParentChanged(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            if (form != null)
            {
                if (form.MdiParent != null)
                {
                    // MDI child form will activate
                    // ... your code here
                }
                else
                {
                    // MDI child form will close
                    //form.ParentChanged -= MdiFormParentChangedHandler;
                    // ... your code here
                }
            }
        }


        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
        }

        private void btnDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openMenu(new MenuFunc("Đăng nhập", "VNPT.HIS.MainForm.HeThong.frmLogin", "1", ""));
        }

        private void btnChangeService_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openMenu(new MenuFunc("Thay đổi Service", "VNPT.HIS.MainForm.HeThong.frmChangeService", "1", ""));
        }

        private void btnThayDoiService_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openMenu(new MenuFunc("Thay đổi Service", "VNPT.HIS.MainForm.HeThong.frmChangeService", "1", ""));
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (Const.local_user == null) // đã đăng xuất rồi thì đóng app
            this.Close();
            //else // đăng xuất
            //openMenu(new MenuFunc("", "VNPT.HIS.MainForm.HeThong.frmLogout", "", ""));
        }

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private void mnuArrangeVertical_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void mnuArrangeHorizontal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mnuArrangeCascade_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void mnuCloseAllWindows_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            while (MdiChildren.Length > 0)
            {
                MdiChildren[0].Close();
            }
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Settings.Default["ApplicationSkinName"] = UserLookAndFeel.Default.SkinName;
            Settings.Default.Save();
        }

        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show(string.Format("Phần Mềm Quản Lý Hệ Thống Thông Tin Bệnh viện - v1.0.0.1"
                + "\r\nBệnh viện: " + Const.SubDomain
                ));

            //  if (ApplicationDeployment.IsNetworkDeployed)
            //{
            //    MessageBox.Show(string.Format("Phần Mềm Quản Lý Hệ Thống Thông Tin Bệnh viện - v{0}"
            //        , ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4))
            //         );
            //}

            //string version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            // AssemblyVersion.Major.ToString() + "." + AssemblyVersion.Minor.ToString() + "." + AssemblyVersion.Build.ToString() + "." + AssemblyVersion.Revision.ToString();

        }

        private void btnHelp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ManageCache frm = new ManageCache();
            frm.Show();
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
        }
    }


}