using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Ribbon.Helpers;
using DevExpress.XtraBars.Ribbon.ViewInfo;

namespace VNPT.HIS.UserControl.SubForm
{
    public class CustomRibbonMinimizedControl : RibbonMinimizedControl
    {
        public CustomRibbonMinimizedControl(DevExpress.XtraBars.Ribbon.RibbonBarManager manager, DevExpress.XtraBars.Ribbon.RibbonControl sourceRibbon)
            : base(manager, sourceRibbon)
        {

        }

        protected override DevExpress.XtraBars.Ribbon.ViewInfo.RibbonViewInfo CreateViewInfo()
        {
            return new CustomRibbonMinimizedControlViewInfo(this);
        }

        internal new void ActivateKeyboardNavigation()
        {
            base.ActivateKeyboardNavigation();
        }
    }
}
