using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K044_sinhsothutumoi : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        string kieu = ""; string khambenhid = "";
        public NGT02K044_sinhsothutumoi()
        {
            InitializeComponent();
        }
        public void setKhamBenhID(string kieu, string khambenhid)
        {
            this.kieu = kieu;
            this.khambenhid = khambenhid;
        }
        private void NGT02K044_sinhsothutumoi_Load(object sender, EventArgs e)
        {

            // {"func":"ajaxExecuteQuery","params":["","DS.PKLAYSOUUTIEN"],"options":[{"name":"[0]","value":"3379"}],"uuid":"2d4c5e66-9324-421c-818c-0b2c07b9cebc"}
            // [["214","PK Mắt P.314","2496"]]
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("DS.PKLAYSOUUTIEN", new String[] { "[0]" }, new String[] { this.khambenhid });
            ucPhongKham.setData(dt, 0, 1);
            ucPhongKham.SelectIndex = 0;

            if (kieu == "1")
            { // sinh lại số ưu tiên
                btnSinhso.Visible = true;
                btnInphieu.Enabled = false;

                btnInphieu.Location = new Point(178, 88);
                btnDong.Location = new Point(267, 88);
            }
            else
            { // In phiếu
                btnSinhso.Visible = false;
                btnInphieu.Enabled = true;

                btnInphieu.Location = new Point(btnInphieu.Location.X - 50, btnInphieu.Location.Y);
                btnDong.Location = new Point(btnDong.Location.X - 50, btnDong.Location.Y);
            }
        }

        private void btnSinhso_Click(object sender, EventArgs e)
        {
            string checkMax_Phongkham = RequestHTTP.check_maxPK(ucPhongKham.SelectValue);
            if (checkMax_Phongkham == "-1")
            {
				MessageBox.Show("Phòng khám hết số");
                return;
            }
            string phongkhamdangkyid = ucPhongKham.SelectDataRowView[2].ToString();
            string phongid = ucPhongKham.SelectValue;

            // {"func":"ajaxCALL_SP_S","params":["DMC.SINHSO.UUTIEN","{\"khambenhid\":\"3379\",\"phongkhamdangkyid\":\"2496\",\"phongid\":\"214\"}"],"uuid":"2d4c5e66-9324-421c-818c-0b2c07b9cebc"}
            string json = "{\"khambenhid\":\"" + khambenhid + "\",\"phongkhamdangkyid\":\"" + phongkhamdangkyid + "\",\"phongid\":\"" + phongid + "\"}";
            string ret = RequestHTTP.call_ajaxCALL_SP_S_result("DMC.SINHSO.UUTIEN", json.Replace("\\", "\\\\").Replace("\"", "\\\"") );
            if (ret == "1")
            {
                btnInphieu.Enabled = true;
                btnInphieu_Click(null, null);
            }
            else if (ret == "-100")
            {
                MessageBox.Show("Đã kết thúc khám trong phòng khám");
            }
        }

        private void btnInphieu_Click(object sender, EventArgs e)
        {                
            string phongid = ucPhongKham.SelectValue;

            if (phongid == "" || khambenhid == "")
            {
                MessageBox.Show("Hãy chọn bệnh nhân muốn in phiếu");
                return;
            }

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("khambenhid", "String", khambenhid);
                table.Rows.Add("phongid", "String", phongid);

                VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint("In phiếu", "NGT_STT", table);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}