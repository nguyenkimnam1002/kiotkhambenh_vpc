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

namespace VNPT.HIS.UserControl.SubForm
{
    public class MyRibbonPageGroupViewInfo : RibbonPageGroupViewInfo
    {
        public MyRibbonPageGroupViewInfo(RibbonViewInfo viewInfo, RibbonPageGroup group) : base(viewInfo, group) { }

        protected override DevExpress.Utils.Drawing.GroupObjectInfoArgs SetupDrawArgs()
        {
            GroupObjectInfoArgs args = base.SetupDrawArgs();
            MyRibbonPageGroupViewInfo viewInfo = args.Tag as MyRibbonPageGroupViewInfo;
            ucMyRibbonControl ribbon = Ribbon;
            if (ribbon == null)
                ribbon = viewInfo.ViewInfo.Manager.Ribbon as ucMyRibbonControl;
            args.ShowCaption = ribbon.ShowGroupCaption;
            return args;
        }



        protected new ucMyRibbonControl Ribbon
        {
            get { return base.Ribbon as ucMyRibbonControl; }
        }
    }
}
