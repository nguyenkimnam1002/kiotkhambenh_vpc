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
using DevExpress.XtraBars;
using VNPT.HIS.Common;
using Newtonsoft.Json;

namespace VNPT.HIS.UserControl
{
    public partial class ucBarMenu : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ucBarMenu()
        {
            InitializeComponent();
        }
        private void ucBarMenu_Load(object sender, EventArgs e)
        {
            //bar2.LinksPersistInfo.Clear();
        }

        public void addMenu(List<MenuFunc> listMenu)
        {
            try
            {
                List<MenuFunc> list_temp = new List<MenuFunc>();
                MenuFunc menu_cha = new MenuFunc("", "", "", "");
                for (int i = 0; i < listMenu.Count; i++)
                {
                    MenuFunc menu = listMenu[i];
                    if (menu.hlink != "" && menu.children == null) // Button
                    {
                        menu_cha.addChildren(menu);
                    }
                    //else if (menu.hlink == "" && menu.children != null) // menu cha
                    //else if (menu.hlink == "" && menu.children == null) // tiêu đề
                    else
                    {
                        if (i > 0)
                        {
                            list_temp.Add(menu_cha);
                        }
                        menu_cha = menu;
                    }
                }
                list_temp.Add(menu_cha); 

                bar2.ItemLinks.Clear();
                for (int i = 0; i < list_temp.Count; i++) addMenu(list_temp[i],null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        protected EventHandler Click_Menu;
        public void setEvent(EventHandler eventClick_Menu)
        {
            Click_Menu = eventClick_Menu;
        }


        private void CreateBarSubItem(BarManager barManagerParam, Bar bar)
        {
            BarSubItem subItem = new BarSubItem(barManagerParam, "SubItem");
            subItem.Caption = "SubItem";
            bar.AddItem(subItem);

            for (int i = 0; i < 5; i++)
            {
                String capt = i.ToString();
                BarButtonItem button = new BarButtonItem(barManagerParam, capt);
                subItem.ItemLinks.Add(button);
            }
        }
        private void addMenu(MenuFunc menu, BarSubItem subItem)
        {
            try
            {
                if (menu.hlink != "" && menu.children == null) // Button
                {
                    BarButtonItem button = new BarButtonItem(barManager1, menu.text);
                    button.Description = JsonConvert.SerializeObject(menu);
                    button.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                    button.Glyph = Func.getIcon(menu.icon);
                    button.ItemClick += this.menu_Click;

                    if (menu.hlink == "close") button.Alignment = BarItemLinkAlignment.Right;

                    if (subItem == null) bar2.AddItem(button);
                    else
                    {
                        subItem.ItemLinks.Add(button);
                        subItem.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(button));
                    }
                }
                else if (menu.hlink == "" && menu.children != null) // menu cha
                {
                    BarSubItem parent = new BarSubItem(barManager1, menu.text);
                    parent.Caption = menu.text;
                    parent.Glyph = Func.getIcon(menu.icon);
                    parent.PaintStyle = BarItemPaintStyle.CaptionGlyph;
                     
                    if (subItem == null) bar2.AddItem(parent);
                    else subItem.ItemLinks.Add(parent);

                    barManager1.Items.Add(parent);
                    for (int i = 0; i < menu.children.Count; i++)
                        addMenu(menu.children[i], parent);
                }
                else if (menu.hlink == "" && menu.children == null) // tiêu đề
                {
                    BarHeaderItem header = new BarHeaderItem();
                    header.Glyph = Func.getIcon(menu.icon);
                    header.Caption = menu.text;
                     
                    if (subItem == null) bar2.AddItem(header);
                    else subItem.ItemLinks.Add(header);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }
        private void menu_Click(object Sender, ItemClickEventArgs e)
        {
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                //(this, typeof(DevExpress.XtraWaitForm.WaitForm), true, true, false);

                BarButtonItem btnItem = (BarButtonItem)e.Item;
                MenuFunc menu = JsonConvert.DeserializeObject<MenuFunc>(btnItem.Description);
                Click_Menu(menu, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString()); 
            }
            finally
            {
                //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        public void HideShow_byTitle(bool show, string title)
        {
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.GetType().ToString() == "DevExpress.XtraBars.BarButtonItem" && item.Caption.ToLower() == title.ToLower())
                    item.Enabled = show;
            }
        }
        public void HideShow_All(bool show)
        {
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.GetType().ToString() == "DevExpress.XtraBars.BarButtonItem") item.Enabled = show;
            }
        }

        public void HideShow_byTitle_BarSubItem(bool show, string title)
        {
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.GetType().ToString() == "DevExpress.XtraBars.BarSubItem" && item.Caption.ToLower() == title.ToLower())
                    item.Enabled = show;
            }
        }
        public void Visible_byTitle(bool show, string title)
        {
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.GetType().ToString() == "DevExpress.XtraBars.BarButtonItem" && item.Caption.ToLower() == title.ToLower())
                    item.Visibility = show==true ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
        }

        public void Visible_byTitle_BarSubItem(bool show, string title)
        {
            for (int i = 0; i < barManager1.Items.Count; i++)
            {
                BarItem item = barManager1.Items[i];
                if (item.GetType().ToString() == "DevExpress.XtraBars.BarSubItem" && item.Caption.ToLower() == title.ToLower())
                    item.Visibility = show == true ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
        }
        
        public List<MenuFunc> Default_Menu()
        {
            List<MenuFunc> listMenu = new List<MenuFunc>();

            MenuFunc InAn = new MenuFunc("In ấn", "", "0", "barSubItem1.Glyph.png");
            InAn.addChildren(new MenuFunc("In phiếu", "", "0", ""));
            InAn.addChildren(new MenuFunc("Giấy ra viện", "x", "0", "barButtonItem3.Glyph.png"));
            InAn.addChildren(new MenuFunc("Giấy chuyển viện", "x", "0", "barButtonItem3.Glyph.png"));
            InAn.addChildren(new MenuFunc("Giấy hẹn khám", "x", "0", "barButtonItem3.Glyph.png"));
            InAn.addChildren(new MenuFunc("Bảng kê", "x", "0", "barButtonItem3.Glyph.png"));
            InAn.addChildren(new MenuFunc("Phiếu chỉ định cận lâm sàng", "x", "0", "barButtonItem3.Glyph.png"));
            InAn.addChildren(new MenuFunc("Đơn thuốc", "x", "0", "barButtonItem3.Glyph.png"));
            InAn.addChildren(new MenuFunc("Phiếu khám bệnh vào viện", "x", "0", "barButtonItem3.Glyph.png"));

            listMenu.Add(InAn);

            listMenu.Add(new MenuFunc("DS Khám", "x", "0", "barButtonItem3.Glyph.png"));

            MenuFunc BA_NGT = new MenuFunc("BA NGT", "", "0", "barButtonItem3.Glyph.png");
            BA_NGT.addChildren(new MenuFunc("Bệnh án", "x", "0", ""));
		    BA_NGT.addChildren(new MenuFunc("Chọn bệnh án", "", "0", ""));
		    BA_NGT.addChildren(new MenuFunc("Mở bệnh án", "x", "0", ""));
		    BA_NGT.addChildren(new MenuFunc("Đóng bệnh án", "x", "0", ""));
            BA_NGT.addChildren(new MenuFunc("Đưa ra khỏi bệnh án", "x", "0", ""));
            listMenu.Add(BA_NGT);

            listMenu.Add(new MenuFunc("Gọi khám", "x", "0", "barButtonItem10.Glyph.png"));
            listMenu.Add(new MenuFunc("Bắt đầu", "x", "0", "barButtonItem11.Glyph.png"));
            listMenu.Add(new MenuFunc("Khám bệnh", "x", "0", "barButtonItem12.Glyph.png"));
            listMenu.Add(new MenuFunc("Dịch vụ", "VNPT.HIS.NgoaiTru.NGT02K016_ChiDinhDichVu", "1", "barButtonItem13.Glyph.png"));

            MenuFunc Thuoc = new MenuFunc("Thuốc", "", "0", "barSubItem2.Glyph.png");
            Thuoc.addChildren(new MenuFunc("Thuốc", "", "0", ""));
            Thuoc.addChildren(new MenuFunc("Tạo đơn thuốc", "x", "0", "barButtonItem16.Glyph.png"));
            Thuoc.addChildren(new MenuFunc("Tạo đơn thuốc mua ngoài", "x", "0", "barButtonItem16.Glyph.png"));
            Thuoc.addChildren(new MenuFunc("Tạo đơn thuốc không thuốc", "x", "0", "barButtonItem16.Glyph.png"));
            Thuoc.addChildren(new MenuFunc("Thuốc đông y", "", "0", ""));
            Thuoc.addChildren(new MenuFunc("Tạo đơn thuốc đông y", "x", "0", "barButtonItem16.Glyph.png"));
            Thuoc.addChildren(new MenuFunc("Tạo phiếu trả thuốc đông y", "x", "0", "barButtonItem16.Glyph.png"));
            Thuoc.addChildren(new MenuFunc("Vật tư", "", "0", ""));
            Thuoc.addChildren(new MenuFunc("Tạo phiếu Vật tư", "x", "0", "barButtonItem16.Glyph.png"));

            listMenu.Add(Thuoc);

            listMenu.Add(new MenuFunc("Xử trí KB", "x", "0", "barButtonItem14.Glyph.png"));

            MenuFunc Khac = new MenuFunc("Khác", "", "0", "barSubItem3.Glyph.png");

            Khac.addChildren(new MenuFunc("Khác", "", "0", ""));
            Khac.addChildren(new MenuFunc("Chuyển phòng khám", "VNPT.HIS.CommonForm.NGT01T004_Tiepnhan_Chuyenphongkham", "1", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Hồ sơ quản lý sức khỏe cá nhân", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Tai nạn thương tích", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Phiếu vận chuyển", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Đổi phòng khám", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Lập phiếu tạm ứng", "x", "0", "barButtonItem32.Glyph.png"));

            Khac.addChildren(new MenuFunc("Xử trí", "", "0", ""));
            Khac.addChildren(new MenuFunc("Thông tin tử vong", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Kiểm điểm tử vong", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Thông tin ra viện", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Chuyển viện", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Hẹn khám mới", "x", "0", "barButtonItem32.Glyph.png"));
            Khac.addChildren(new MenuFunc("Hẹn khám tiếp", "x", "0", "barButtonItem32.Glyph.png"));

            listMenu.Add(Khac);

            listMenu.Add(new MenuFunc("Kết thúc khám", "x", "0", "barButtonItem15.Glyph.png"));

            return listMenu;
        }
        
    }
}
