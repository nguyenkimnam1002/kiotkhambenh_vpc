using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon.Helpers;
using VNPT.HIS.UserControl.SubForm;

namespace VNPT.HIS.UserControl
{
    public class ucMyRibbonControl : RibbonControl
    {
        public ucMyRibbonControl() : base() { }

        protected override DevExpress.XtraBars.Ribbon.ViewInfo.RibbonViewInfo CreateViewInfo()
        {
            return new MyRibbonViewInfo(this);
        }

        bool showCaption = true;
        public bool ShowGroupCaption
        {
            get { return showCaption; }
            set
            {
                if (showCaption != value)
                    showCaption = value;
            }
        }

        protected override void ShowMinimizedRibbon(bool activateKeyboardNavigation)
        {
            XtraForm.SuppressDeactivation = true;
            try
            {
                if (MinimizedRibbonPopupForm != null)
                {
                    (MinimizedRibbonPopupForm as CustomMinimizedRibbonPopupForm).FocusForm();
                    if (MinimizedRibbonPopupForm != null)
                    {
                        MinimizedRibbonPopupForm.UpdateRibbon();
                        return;
                    }
                }
                CustomMinimizedRibbonPopupForm form = new CustomMinimizedRibbonPopupForm(this);
                form.UpdateRibbon();
                MinimizedRibbonPopupForm = form;
                form.ShowPopup();
                 if (IsKeyboardActive || activateKeyboardNavigation)
                    (form.Control as CustomRibbonMinimizedControl).ActivateKeyboardNavigation();
            }
            finally
            {
                XtraForm.SuppressDeactivation = false;
            }
        }
    }
}
