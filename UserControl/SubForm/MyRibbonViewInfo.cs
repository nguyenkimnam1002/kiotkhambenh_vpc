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
    public class MyRibbonViewInfo : RibbonViewInfo
    {
        public MyRibbonViewInfo(RibbonControl ribbon) : base(ribbon) { }
        protected override RibbonPanelViewInfo CreatePanelInfo()
        {
            return new MyRibbonPanelViewInfo(this);
        }
    }
}
