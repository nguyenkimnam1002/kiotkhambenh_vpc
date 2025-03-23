using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KIOS_HIS.LVVPC
{
    public partial class LVVPC_Setting : Form
    {
        public bool SaveSuccess = false;
        public LVVPC_Setting()
        {
            InitializeComponent();
            loadForm();
        }

        private void loadForm()
        {
            bool inSTT = Convert.ToBoolean(Properties.Settings.Default["InSTT"]);
            //bool showButtonKhamBenh = Convert.ToBoolean(Properties.Settings.Default["ShowButtonKhamBenh"]);
            //bool showButtonKhamHuyetAp = Convert.ToBoolean(Properties.Settings.Default["ShowButtonKhamHuyetAp"]);
            //bool showButtonKhamTieuDuong = Convert.ToBoolean(Properties.Settings.Default["ShowButtonKhamTieuDuong"]);

            //cbb_inSTT.Items.Add("Không");
            //cbb_inSTT.Items.Add("Có");
            //cbb_inSTT.SelectedIndex = inSTT ? 1 : 0;

            cbInSTT.Checked = inSTT;
            //cbKhamHuyetAp.Checked = showButtonKhamHuyetAp;
            //cbKhamTieuDuong.Checked = showButtonKhamTieuDuong;
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            try
            {
                //Properties.Settings.Default["InSTT"] = cbb_inSTT.SelectedIndex;
                //Properties.Settings.Default["ShowButtonKhamBenh"] = cbKhamBenh.Checked;
                //Properties.Settings.Default["ShowButtonKhamHuyetAp"] = cbKhamHuyetAp.Checked;
                Properties.Settings.Default["inSTT"] = cbInSTT.Checked;
                Properties.Settings.Default.Save();
                // Hiển thị thông báo lưu thành công
                MessageBox.Show("Lưu cấu hình thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SaveSuccess = true;
                this.Dispose();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lưu thất bại và thông tin lỗi (nếu có)
                MessageBox.Show("Lưu cấu hình thất bại.\n" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SaveSuccess = false;
            }
        }
    }
}
