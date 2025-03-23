using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using VNPT.HIS.Common;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K059_show_img : DevExpress.XtraEditors.XtraForm
    { 
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K059_show_img(Image img)
        {
            InitializeComponent();

            pictureEdit1.Image = img;
        }

        private void NGT02K059_show_img_Load(object sender, EventArgs e)
        {
            pictureEdit1.Focus();
        }
         
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            string fname = Const.FolderSaveFilePrint + "\\temp" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + new Random().Next(100) + ".jpg";
            pictureEdit1.Image.Save(fname); 
            VNPT.HIS.Common.Func.Print_From_File(fname);

            this.Close();
        }
    }
}