using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars.Ribbon.Helpers;
using DevExpress.XtraBars.Ribbon;

namespace VNPT.HIS.UserControl.SubForm
{
    public class CustomMinimizedRibbonPopupForm : MinimizedRibbonPopupForm
    {
        public CustomMinimizedRibbonPopupForm(RibbonControl sourceRibbon)
            : base(sourceRibbon)
        {

        }

        protected override DevExpress.XtraBars.Ribbon.RibbonControl CreateRibbon()
        {
            return new CustomRibbonMinimizedControl(SourceRibbon.Manager, SourceRibbon);
        }

        internal new void FocusForm()
        {
            base.FocusForm();
        }
    }
}
