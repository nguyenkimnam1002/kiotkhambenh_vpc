using System; 
using System.Drawing; 
using System.Windows.Forms; 

using DevExpress.Skins;
using DevExpress.Skins.XtraForm;
using DevExpress.Utils;

namespace VNPT.HIS.UserControl
{
    public class MyFormPainter : FormPainter
    {
        public MyFormPainter(Control owner, ISkinProvider provider) : base(owner, provider) { }

        protected override void DrawText(DevExpress.Utils.Drawing.GraphicsCache cache)
        {
            string text = Text;
            if (text == null || text.Length == 0 || this.TextBounds.IsEmpty) return;
            if (text.IndexOf(" - ") > 0) text = text.Substring(text.IndexOf(" - ") + " - ".Length);

            //if (text.Contains("Phần Mềm Quản Lý Hệ Thống Thông Tin Bệnh viện") && !text.Contains(Const.SubDomain + "-"))
            //    text = Const.SubDomain + "-" + text;

            AppearanceObject appearance = new AppearanceObject(GetDefaultAppearance());
            appearance.Font = new Font(appearance.Font.Name, 9);
            appearance.TextOptions.Trimming = Trimming.EllipsisCharacter;
            Rectangle r = RectangleHelper.GetCenterBounds(TextBounds, new Size(TextBounds.Width, appearance.CalcDefaultTextSize(cache.Graphics).Height));
            
            //DrawTextShadow(cache, appearance, r); 
            cache.DrawString(text, appearance.Font, new SolidBrush(Color.FromName("Wheat")), r, appearance.GetStringFormat()); //  
            
            r.X--; r.Y--;
            cache.DrawString(text, appearance.Font, appearance.GetForeBrush(cache), r, appearance.GetStringFormat()); 
        }
    }
}

 