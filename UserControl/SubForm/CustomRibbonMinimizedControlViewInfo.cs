using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Ribbon.ViewInfo;

namespace VNPT.HIS.UserControl.SubForm
{
    public class CustomRibbonMinimizedControlViewInfo : RibbonMinimizedControlViewInfo
    {
        public CustomRibbonMinimizedControlViewInfo(DevExpress.XtraBars.Ribbon.RibbonControl control)
            : base(control)
        {

        }

        protected override RibbonPanelViewInfo CreatePanelInfo()
        {
            return new MyRibbonPanelViewInfo(this);
        }
    }
}
