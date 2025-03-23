using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using VNPT.HIS.Common;
using VNPT.HIS.MainForm.Class;

namespace VNPT.HIS.MainForm.HeThong
{
    public partial class frmSelectDept : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public frmSelectDept()
        {
            InitializeComponent();
        }

        private void frmSelectDept_Load(object sender, EventArgs e)
        {
            ucSearchKhoa.setEvent(sleKhoa_EditValueChanged);
            ucSearchPhong.setEvent(slePhong_EditValueChanged);

            DataTable dt = RequestHTTP.Cache_getKhoa(false);

            ucSearchKhoa.setData(dt, 0, 1);
            ucSearchKhoa.searchLookUpEdit.Properties.View.Columns[0].Caption = "ID";
            ucSearchKhoa.searchLookUpEdit.Properties.View.Columns[1].Caption = "Tên khoa";

            if (Const.local_khoaId > 0 && Const.local_phongId > 0)
            {
                try
                {
                    ucSearchKhoa.searchLookUpEdit.EditValue = Const.local_khoaId;
                    ucSearchPhong.searchLookUpEdit.EditValue = Const.local_phongId;

                    Const.local_khoa = ucSearchKhoa.searchLookUpEdit.Text;
                    Const.local_phong = ucSearchPhong.searchLookUpEdit.Text;

                    frmMain.Current.Update_KhoaPhong();
                    frmMain.Current.Show_KhoaPhong(Const.local_khoa, Const.local_phong);
                }
                catch (Exception ex) { log.Fatal(ex.ToString()); }
            }
            else
            {
                ucSearchKhoa.searchLookUpEdit.Text = "";
                ucSearchKhoa.searchLookUpEdit.Text = "";
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int idKhoa = 0;
                if (ucSearchKhoa.searchLookUpEdit.EditValue.ToString() != "") idKhoa = Convert.ToInt32(ucSearchKhoa.searchLookUpEdit.EditValue.ToString());
                int idPhong = 0;
                if (ucSearchPhong.searchLookUpEdit.EditValue.ToString() != "") idPhong = Convert.ToInt32(ucSearchPhong.searchLookUpEdit.EditValue.ToString());

                if (Const.local_khoaId == idKhoa && Const.local_phongId == idPhong) // Ko thay đổi
                {
                    this.Close();
                    //MessageBox.Show("Thiết lập thành công!");
                    return;
                }

                if (idPhong > 0 && idKhoa > 0)
                {
                    if (!ServiceSelectDept.setKhoaPhong(idKhoa, idPhong))
                    {
                        MessageBox.Show(Const.mess_erro_sys, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Const.local_khoaId = idKhoa;
                    Const.local_khoa = ucSearchKhoa.searchLookUpEdit.Text;

                    Const.local_phongId = idPhong;
                    Const.local_phong = ucSearchPhong.searchLookUpEdit.Text;

                    frmMain.Current.Update_KhoaPhong();
                    frmMain.Current.Show_KhoaPhong(Const.local_khoa, Const.local_phong);

                    frmMain.Current.miniAllchildForm("", "");

                    this.Close();
                    //MessageBox.Show("Thiết lập thành công!");
                }
                else
                    MessageBox.Show(Const.mess_erro_datanull, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                MessageBox.Show(Const.mess_erro_sys + ": " + ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sleKhoa_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)sender;

            // Lấy ds phòng
            {
                string id_khoa = dr[0].ToString();
                DataTable dtPhong = RequestHTTP.Cache_getPhong(false, id_khoa);

                ucSearchPhong.setData(dtPhong, 0, 1);
                ucSearchPhong.searchLookUpEdit.Properties.View.Columns[0].Caption = "ID";
                ucSearchPhong.searchLookUpEdit.Properties.View.Columns[1].Caption = "Tên phòng";

                ucSearchPhong.SelectIndex = 0;
            }

            ucSearchPhong.searchLookUpEdit.Focus();
        }

        private void slePhong_EditValueChanged(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
    }
}