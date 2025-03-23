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
using VNPT.HIS.QTHethong.Class;

namespace VNPT.HIS.QTHethong
{
    public partial class DMC18_CHNV : DevExpress.XtraEditors.XtraForm
    {
        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        private bool flagLoading = false;
        private bool isEdit = false;
        private string selectedId;

        public DMC18_CHNV()
        {
            InitializeComponent();
            setDisableAll();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            isEdit = true;
            flagLoading = true;
            txtGiatriTL.ReadOnly = false;
            txtGiatriTL.Focus();
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            flagLoading = false;
            String res;
            CauhinhUser chUser = getValueFromUI();
            if (isEdit)
            {
                chUser.CAUHINHID = selectedId.ToString();
            }
            else
            {
                chUser.CAUHINHID = null;
            }
            res = ServiceDMC18CHNV.insertOrUpdateCHUser(chUser);
            if (res != null)
            {
                if (res == "1")
                {
                    MessageBox.Show("Thêm mới thành công");
                    setDisableAll();
                }
                else if (res == "2")
                {
                    MessageBox.Show("Cập nhật thành công");
                    setDisableAll();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
            }
            getData_table(1, null);
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            flagLoading = false;
            setDisableAll();
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }

        private void setDisableAll()
        {

            txtNguoidung.ReadOnly = true;
            txtTenCauhinh.ReadOnly = true;
            txtMaCauhinh.ReadOnly = true;
            txtGiatriTL.ReadOnly = true;
            txtMota.ReadOnly = true;
        }

        private void ucGridDSNV_Load(object sender, EventArgs e)
        {
            ucGridDSNV.gridView.OptionsView.ColumnAutoWidth = true;
            ucGridDSNV.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucGridDSNV.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucGridDSNV.setEvent(getData_table);
            ucGridDSNV.setEvent_FocusedRowChanged(change_selectRow);
            ucGridDSNV.SetReLoadWhenFilter(true);
            getData_table(1, null);
        }

        private void getData_table(object sender, EventArgs e)
        {
            if (flagLoading)
                return;
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;

            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                int page = (int)sender;
                if (page > 0)
                {
                    //string jsonFilter = "";
                    //// Lấy điều kiện filter
                    //if (ucGridDSNV.ReLoadWhenFilter)
                    //{
                    //    if (ucGridDSNV.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucGridDSNV.tableFlterColumn);
                    //}

                    ResponsList ds = ServiceDMC18CHNV.getDSCauhinhNguoidung(page, ucGridDSNV.ucPage1.getNumberPerPage(), ucGridDSNV.jsonFilter());

                    DataTable dt = MyJsonConvert.toDataTable(ds.rows);
                    ucGridDSNV.clearData();
                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "CAUHINHID", "MACAUHINH", "GIATRI_THIETLAP", "TENCAUHINH", "USER_NAME" });


                    {
                        ucGridDSNV.setData(dt, ds.total, ds.page, ds.records);
                        ucGridDSNV.setColumnAll(false);

                        ucGridDSNV.setColumn("RN", 0, " ");
                        ucGridDSNV.setColumn("CAUHINHID", 1, "ID cấu hình");
                        ucGridDSNV.setColumn("MACAUHINH", 2, "Mã cấu hình");
                        ucGridDSNV.setColumn("GIATRI_THIETLAP", 3, "Giá trị thiết lập");
                        ucGridDSNV.setColumn("TENCAUHINH", 4, "Tên cấu hình");
                        ucGridDSNV.setColumn("USER_NAME", 5, "Người dùng");

                        ucGridDSNV.gridView.BestFitColumns(true);
                    }
                }
            }
            finally
            {
                //Close Wait Form
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void change_selectRow(object sender, EventArgs e)
        {
            if (flagLoading) return;
            DataRowView selectedUser = (DataRowView)sender;

            if (selectedUser != null)
            {
                selectedId = selectedUser["CAUHINHID"].ToString();
                DataTable dt_ChiTiet = ServiceDMC18CHNV.getChiTietUser(selectedId);
                if (dt_ChiTiet != null && dt_ChiTiet.Rows.Count > 0)
                {
                    txtNguoidung.Text = dt_ChiTiet.Rows[0]["USER_NAME"].ToString();
                    txtTenCauhinh.Text = dt_ChiTiet.Rows[0]["TENCAUHINH"].ToString();
                    txtMaCauhinh.Text = dt_ChiTiet.Rows[0]["MACAUHINH"].ToString();
                    txtGiatriTL.Text = dt_ChiTiet.Rows[0]["GIATRI_THIETLAP"].ToString();
                    txtMota.Text = dt_ChiTiet.Rows[0]["MOTA"].ToString();
                }
            }


            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

        }

        private CauhinhUser getValueFromUI()
        {
            CauhinhUser chUser = new CauhinhUser();

            chUser.USER_NAME = txtNguoidung.Text;
            chUser.MACAUHINH = txtMaCauhinh.Text;
            chUser.TENCAUHINH = txtTenCauhinh.Text;
            chUser.GIATRI_THIETLAP = txtGiatriTL.Text;
            chUser.MOTA = txtMota.Text;

            return chUser;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}