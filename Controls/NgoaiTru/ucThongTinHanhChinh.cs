using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using System.Globalization;

namespace VNPT.HIS.Controls.NgoaiTru
{
    public partial class ucThongTinHanhChinh : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ucThongTinHanhChinh()
        { 
            InitializeComponent();
        }
        public void setType_KhamHoiBenh()
        {
            layoutControlItem11.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem12.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem20.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem22.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem23.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
        }
        public void setType_Default()
        {
            layoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItem19.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        
        private void ucThongTinHanhChinh_Load(object sender, EventArgs e)
        {
            try
            { 
                setType_Default();
                DataTable dt = new DataTable();
                #region HÀNH CHÍNH

                txtMaBN.Focus();

                //cbo Gioi tinh-dan toc-Quoc tịch
                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Gioitinh, "1");
                ucGioitinh.setData(dt, 0, 1);
                ucGioitinh.setEvent_Enter(ucGioitinh_KeyEnter);


                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Nghenghiep);
                ucNghenghiep.setData(dt, 0, 1);
                ucNghenghiep.Option_LockKeyTab = true;
                ucNghenghiep.setEvent_Enter(ucNghenghiep_KeyEnter);


                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Dantoc);
                ucDantoc.setData(dt, 0, 1);
                ucDantoc.setEvent_Enter(ucDantoc_KeyEnter);
                log.Info("load tt 1 2");

                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Quoctich);
                ucQuoctich.setData(dt, 0, 1);
                ucQuoctich.Option_LockKeyTab = true;
                ucQuoctich.setEvent_Enter(ucQuoctich_KeyEnter);
                 

                //Tỉnh huyện xã viết tắt
                dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_TinhhuyenxaViettat);
                ucTinhHuyenXa.Option_LockKeyTab = true;
                ucTinhHuyenXa.setData(dt, "TENVIETTATDAYDU", "NAME");
                ucTinhHuyenXa.setColumn("RN", -1, "", 0);
                ucTinhHuyenXa.setColumn("VALUE", -1, "", 0);
                ucTinhHuyenXa.setColumn("TENVIETTATDAYDU", 0, "Tên viết tắt", 0);
                ucTinhHuyenXa.setColumn("NAME", 1, "Địa phương", 0);
                ucTinhHuyenXa.setEvent_Enter(ucTinhHuyenXa_KeyEnter);
                ucTinhHuyenXa.setEvent(ucTinhHuyenXa_SelectedIndexChanged);
              

                // Ds các tỉnh
                dt = RequestHTTP.Cache_getTinhTP(true);
                dt.Columns["col3"].SetOrdinal(0);
                dt.Columns["col2"].SetOrdinal(1);
                dt.Columns["col1"].SetOrdinal(2);
                ucTinh.setEvent(cboTinh_SelectedIndexChanged);
                ucTinh.setData(dt, "col3", "col2");
                ucTinh.setEvent_Enter(ucTinh_KeyEnter);

                ucTinh.setColumn("col3", 0, "STT", 0);
                ucTinh.setColumn("col2", 1, "Tỉnh/TP", 0);
                ucTinh.setColumn("col1", -1, "Mã tỉnh", 0);
                ucTinh.setColumn("col4", -1, "", 0); 

                ucHuyen.setEvent(cboHuyen_SelectedIndexChanged);
                ucHuyen.setEvent_Enter(ucHuyen_KeyEnter);
                ucHuyen.Option_LockKeyTab = true;

                ucXa.setEvent(cboXa_SelectedIndexChanged);
                ucXa.setEvent_Enter(ucXa_KeyEnter);

                #endregion
                 
                set_defaul_value(); 
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        public void set_defaul_value()
        {
            txtMaBN.Text = "";
            txtHoten.Text = "";

            ucGioitinh.SelectIndex = 0;
            ucNghenghiep.SelectIndex = 0;
            ucDantoc.SelectIndex = 24;
            ucQuoctich.SelectIndex = 0;
            dtimeNgaysinh.Text = "";
            txtNamsinh.Text = "";
            txtTuoi.Text = "";
            cboTuoi.SelectedIndex = 0;

            txtSoNha.Text = "";
            txtNoilamviec.Text = "";
            txtNoilamviec2.Text = "";
            txtTenNN.Text = "";
            txtDiaChiNN.Text = "";
            txtDienThoaiNN.Text = "";

            ucTinhHuyenXa.searchLookUpEdit.EditValue = Const.local_user.HOSPITAL_CODE;
            ucTinh.SelectIndex = ucTinh.GetIndexByColumn("col1", Const.local_user.PROVINCE_ID);
            //ucHuyen.SelectIndex = 0;
            //ucXa.SelectIndex = 0;

        }

        public DataTable dtTTHanhChinh = new DataTable();
        public void load_benhnhan_theoMa(string MaBN)
        {
            MaBN = MaBN.ToUpper();

            DataTable dt = Common.RequestHTTP.getChiTiet_BenhNhan(MaBN, "1", "", "", "");

            load_benhnhan_theoMa(dt);
        }
        public void load_benhnhan_theoMa(DataTable _dtTTHanhChinh)
        {
            try
            {
                dtTTHanhChinh = _dtTTHanhChinh;

                log.Info("TT Hành chính: " + dtTTHanhChinh.Rows[0]["MABENHNHAN"].ToString());


                if (dtTTHanhChinh.Rows.Count > 0)
                {
                    txtMaBN.Text = dtTTHanhChinh.Rows[0]["MABENHNHAN"].ToString();
                    txtHoten.Text = dtTTHanhChinh.Rows[0]["TENBENHNHAN"].ToString();
                    ucGioitinh.SelectValue = dtTTHanhChinh.Rows[0]["GIOITINHID"].ToString();
                    ucNghenghiep.SelectValue = dtTTHanhChinh.Rows[0]["NGHENGHIEPID"].ToString();
                    ucDantoc.SelectValue = dtTTHanhChinh.Rows[0]["DANTOCID"].ToString();
                    ucQuoctich.SelectValue = dtTTHanhChinh.Rows[0]["QUOCGIAID"].ToString();

                    dtimeNgaysinh.Text = dtTTHanhChinh.Rows[0]["NGAY_SINH"].ToString();
                    txtNamsinh.Text = dtTTHanhChinh.Rows[0]["NAMSINH"].ToString();
                    //txtTuoi.Text = dtTTHanhChinh.Rows[0]["TUOI"].ToString();
                    //cboTuoi 

                    txtSoNha.Text = dtTTHanhChinh.Rows[0]["SONHA"].ToString();

                    //NGUOITHAN": "", 
                    txtNGUOITHAN.Text = dtTTHanhChinh.Rows[0]["NGUOITHAN"].ToString();
                    txtNoilamviec.Text = dtTTHanhChinh.Rows[0]["NOILAMVIEC"].ToString();
                    txtNoilamviec2.Text = dtTTHanhChinh.Rows[0]["NOILAMVIEC"].ToString();
                    txtTenNN.Text = dtTTHanhChinh.Rows[0]["TENNGUOITHAN"].ToString();
                    txtDienThoaiNN.Text = dtTTHanhChinh.Rows[0]["DIENTHOAINGUOITHAN"].ToString();
                    txtDiaChiNN.Text = dtTTHanhChinh.Rows[0]["DIACHINGUOITHAN"].ToString();
                    txtDIACHI.Text = dtTTHanhChinh.Rows[0]["DIACHI"].ToString();


                    //ucTinhHuyenXa. = dtTTHanhChinh.Rows[0]["TENDIAPHUONG"].ToString();
                    //txtDcBN.Text = dtTTHanhChinh.Rows[0]["DIACHI"].ToString();
                    DataTable dtTHX = (DataTable)ucTinhHuyenXa.searchLookUpEdit.Properties.DataSource;
                    foreach (DataRow row in dtTHX.Rows)
                    {
                        if (row["VALUE"].ToString() == dtTTHanhChinh.Rows[0]["DIAPHUONGID"].ToString())
                        {
                            ucTinhHuyenXa.searchLookUpEdit.EditValue = row[ucTinhHuyenXa.searchLookUpEdit.Properties.ValueMember];
                            break;
                        }
                    }

                    setComboTinh_Huyen_Xa_byDiaPhuongID(dtTTHanhChinh.Rows[0]["DIAPHUONGID"].ToString());

                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void setComboTinh_Huyen_Xa_byDiaPhuongID(string DiaPhuongID)
        {
            try
            {
                Dia_Chi dc = Common.RequestHTTP.getDIACHI(DiaPhuongID);
                if (dc.ID_TINH != "-1")
                {
                    DataTable dt = (DataTable)ucTinh.lookUpEdit.Properties.DataSource;
                    for (int i = 0; i < dt.Rows.Count; i++)
                        if (dt.Rows[i]["col1"].ToString() == dc.ID_TINH)
                        {
                            ucTinh.SelectValue = dt.Rows[i]["col3"].ToString();
                            break;
                        }
                }

                if (dc.ID_HUYEN != "-1")
                {
                    DataTable dt = (DataTable)ucHuyen.lookUpEdit.Properties.DataSource;
                    for (int i = 0; i < dt.Rows.Count; i++)
                        if (dt.Rows[i]["col1"].ToString() == dc.ID_HUYEN)
                        {
                            ucHuyen.SelectValue = dt.Rows[i]["col3"].ToString();
                            break;
                        }
                }

                if (dc.ID_XA != "-1")
                {
                    DataTable dt = (DataTable)ucXa.lookUpEdit.Properties.DataSource;
                    for (int i = 0; i < dt.Rows.Count; i++)
                        if (dt.Rows[i]["col1"].ToString() == dc.ID_XA)
                        {
                            ucXa.SelectValue = dt.Rows[i]["col3"].ToString();
                            break;
                        }
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        
        public void set_all_control_readonly(bool readOnly) 
        {
            TraverseControls(this, readOnly);
        }
        private void TraverseControls(Control c,bool readOnly)
        {
            if (c is BaseEdit)
            {
                ((BaseEdit)c).Properties.ReadOnly = true;
            }
            foreach (Control control in c.Controls) TraverseControls(control, readOnly);
        }

        private void cboTinh_SelectedIndexChanged(object sender, EventArgs e)
        { //col1 : id của tỉnh vd 4800000000
            // col3 : số thứ tự của tỉnh, vd 35 --> Value của combo = text của ô search
            
            ucHuyen.clearData();            
            set_txtDcBN();

            try
            {
                DataRowView drv = ucTinh.SelectDataRowView;
                if (drv == null)
                {

                }
                else
                {
                    string id_tinh = drv["col1"].ToString();
                    DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_tinh);
                    dt.Columns["col3"].SetOrdinal(0);
                    dt.Columns["col2"].SetOrdinal(1);
                    dt.Columns["col1"].SetOrdinal(2);

                    ucHuyen.setData(dt, "col3", "col2");
                    ucHuyen.setColumn("col2", 1, "Huyện(Q)", 0);
                    ucHuyen.setColumn("col3", 0, "STT", 0);
                    ucHuyen.setColumn("col1", -1, "Mã huyện", 0);
                    ucHuyen.setColumn("col4", -1, "", 0);
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void cboHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucXa.clearData();
            set_txtDcBN();
            try
            {
                DataRowView drv = ucHuyen.SelectDataRowView;
                if (drv == null)
                {

                }
                else
                {
                    string id_huyen = drv["col1"].ToString();
                    DataTable dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DsHuyenXa, id_huyen);
                    dt.Columns["col3"].SetOrdinal(0);
                    dt.Columns["col2"].SetOrdinal(1);
                    dt.Columns["col1"].SetOrdinal(2);

                    ucXa.setData(dt, "col3", "col2");
                    ucXa.setColumn("col3", 0, "STT", 0);
                    ucXa.setColumn("col2", 1, "Xã(P)", 0);
                    ucXa.setColumn("col1", -1, "Mã xã", 0);
                    ucXa.setColumn("col4", -1, "", 0);
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void cboXa_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_txtDcBN();
        }

        public void set_txtDcBN()
        {
            try
            {
                string txtDcBN_Text = txtSoNha.Text;
                if (txtThonPho.Text.Trim() != "") txtDcBN_Text += "-" + txtThonPho.Text.Trim();
                if (ucXa.SelectIndex >= 0) txtDcBN_Text += "-" + ucXa.Text;
                if (ucHuyen.SelectIndex >= 0) txtDcBN_Text += "-" + ucHuyen.Text;
                if (ucTinh.SelectIndex >= 0) txtDcBN_Text += "-" + ucTinh.Text;
                if (txtDcBN_Text.StartsWith("-")) txtDcBN_Text = txtDcBN_Text.Substring(1);

                event_DiaChiChange(txtDcBN_Text, null);
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        public bool KiemTraNhap()
        {
            if (txtHoten.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập họ tên");
                txtHoten.Focus();
                return false;
            }
            if (dtimeNgaysinh.Text.Trim() == "" && cboTuoi.SelectedIndex != 0)
            {
                MessageBox.Show("Hãy nhập ngày sinh");
                dtimeNgaysinh.Focus();
                return false;
            }
            if (txtNamsinh.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập năm sinh");
                txtNamsinh.Focus();
                return false;
            }
            if (txtTuoi.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tuổi");
                txtTuoi.Focus();
                return false;
            }
            if (ucTinh.SelectIndex < 0)
            {
                MessageBox.Show("Hãy chọn Tỉnh/TP");
                ucTinh.Focus();
                return false;
            }
            //// HÀ NAM KHONG CAN CHECK
            //if (ucHuyen.Text.Trim() == "")
            //{
            //    MessageBox.Show("Hãy chọn Huyện/Quận");
            //    ucHuyen.Focus();
            //    return false;
            //}
            //if (ucXa.Text.Trim() == "")
            //{
            //    MessageBox.Show("Hãy chọn Xã/Phường");
            //    ucXa.Focus();
            //    return false;
            //}
            
            return true;
        }
        

        private void ucTinhHuyenXa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr = (DataRowView)sender;
                setComboTinh_Huyen_Xa_byDiaPhuongID(dr[1].ToString());
            }
            catch (Exception ex)
            { log.Fatal(ex.ToString()); }
        }
        
        private void txtMaBN_Leave(object sender, EventArgs e)
        {
            //load_benhnhan_theoMa(txtMaBN.Text);
            if (event_Load_BN != null) event_Load_BN(txtMaBN.Text.ToUpper().Trim(), null);
        }

        private void txtSoNha_Leave(object sender, EventArgs e)
        {
            set_txtDcBN();
        }

        #region các sk bấm Enter = bấm phím tab
        //if (e.KeyData == Keys.Tab) e.IsInputKey = true;
        private void event_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab) e.IsInputKey = true;
        }  
        
        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtHoten.Focus();
        }

        private void txtHoten_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                dtimeNgaysinh.Focus();
        }
        private void dtimeNgaysinh_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtNamsinh.Focus();
            }
        }
        private void txtNamsinh_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtTuoi.Focus();
            }
        }
        private void txtTuoi_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                ucGioitinh.Focus();
            }
        }

        private void ucGioitinh_KeyEnter(object sender, EventArgs e)
        {
            ucNghenghiep.Focus();
        }
        private void ucNghenghiep_KeyEnter(object sender, EventArgs e)
        {
            ucDantoc.Focus();
        }
        private void ucDantoc_KeyEnter(object sender, EventArgs e)
        {
            ucQuoctich.Focus();
        }
        private void ucQuoctich_KeyEnter(object sender, EventArgs e)
        {
            txtSoNha.Focus();
        }

        private void txtSoNha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtThonPho.Focus();
        }
        private void txtThonPho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) ucTinhHuyenXa.Focus();
        }
        private void ucTinhHuyenXa_KeyEnter(object sender, EventArgs e)
        {
            ucTinh.Focus();
        }
        private void ucTinh_KeyEnter(object sender, EventArgs e)
        {
            ucHuyen.Focus();
        }
        private void ucHuyen_KeyEnter(object sender, EventArgs e)
        {
            ucXa.Focus();
        }
        private void ucXa_KeyEnter(object sender, EventArgs e)
        {
            txtNoilamviec.Focus();
        }
        private void txtNoilamviec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtNGUOITHAN.Focus();
        }
        private void txtNguoiNha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtTenNN.Focus();
        }
        private void txtTenNN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtDiaChiNN.Focus();
        }
        private void txtDiaChiNN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtDienThoaiNN.Focus();
        }

        private void txtDienThoaiNN_KeyDown(object sender, KeyEventArgs e)
        {
            SendKeys.Send("{TAB}");
            //if (e.KeyCode == Keys.Enter) txtDiaChiNN.Focus();
        }

         
        #endregion

        #region Sk phần Năm sinh-Tuổi
        private void Nhap_NgaySinh()
        {
            try
            {
                DateTime dtimeNow = Func.getSysDatetime_Short();
                if (dtimeNgaysinh.DateTime > dtimeNow)
                {
                    MessageBox.Show(Const.mess_erro_ngaysinh);
                    dtimeNgaysinh.Focus();
                    return;
                }
                txtNamsinh.TextChanged -= new EventHandler(txtNamsinh_TextChanged);
                txtNamsinh.Text = dtimeNgaysinh.DateTime.Year.ToString();
                txtNamsinh.TextChanged += new EventHandler(txtNamsinh_TextChanged);

                DateTime dtNhap = dtimeNgaysinh.DateTime.AddDays(0);
                int chenhNam = dtimeNow.Year - dtNhap.Year;

                DateTime dt2 = dtNhap.AddYears(chenhNam);
                int chenhThang = chenhNam * 12 + dtimeNow.Month - dtNhap.Month;
                if (dtimeNow.Day > dtNhap.Day) chenhThang++;

                if (chenhThang >= 36)// hiển thị số tuổi
                {
                    txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                    txtTuoi.Text = chenhNam.ToString();
                    txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                    cboTuoi.SelectedIndex = 0;

                    if (event_Check_TheTE != null)
                    {
                        if (chenhNam > 6) event_Check_TheTE(false, null);// ckbTheTE.Enabled = false;
                        else event_Check_TheTE(true, null);
                    }
                }
                else
                {
                    if (chenhThang >= 1)// hiển thị số tháng
                    {
                        //if (chenhNgay % 30 > 0) chenhThang++;
                        txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                        txtTuoi.Text = chenhThang.ToString();
                        txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                        cboTuoi.SelectedIndex = 1;
                    }
                    else
                    {
                        TimeSpan ts = TimeSpan.FromTicks(dtimeNow.Ticks - dtNhap.Ticks);
                        Double chenhNgay = ts.TotalDays + 1;
                        txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                        txtTuoi.Text = (chenhNgay).ToString();
                        txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                        cboTuoi.SelectedIndex = 2;
                    }
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void dtimeNgaysinh_Leave(object sender, EventArgs e)
        {
            if (dtimeNgaysinh.Text == "" || dtimeNgaysinh.ReadOnly) return;
            Nhap_NgaySinh();
        }
        private void dtimeNgaysinh_EditValueChanged(object sender, EventArgs e)
        {
            if (dtimeNgaysinh.Text == "") return;
            Nhap_NgaySinh();
        }

        private void txtNamsinh_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtNamsinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtimeNow = Func.getSysDatetime_Short();
                int NhapNam = Convert.ToInt32(txtNamsinh.Text.Trim());
                if (NhapNam > dtimeNow.Year)
                {
                    MessageBox.Show(Const.mess_erro_namsinh);
                    txtNamsinh.Focus();
                    return;
                }

                txtTuoi.Text = (dtimeNow.Year - NhapNam).ToString();
                cboTuoi.SelectedIndex = 0;

                dtimeNgaysinh.Text = "";

                if (event_Check_TheTE != null)
                {
                    if (dtimeNow.Year - NhapNam > 6) event_Check_TheTE(false, null);// ckbTheTE.Enabled = false;
                    else event_Check_TheTE(true, null);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                dtimeNgaysinh.Text = "";
                txtNamsinh.Text = "";
                txtTuoi.Text = "";
                cboTuoi.SelectedIndex = 0;
            }
        }

        private void txtTuoi_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtimeNow = Func.getSysDatetime_Short();
                int NhapTuoi = Convert.ToInt32(txtTuoi.Text.Trim());

                txtNamsinh.Text = (dtimeNow.Year - NhapTuoi).ToString();
                cboTuoi.SelectedIndex = 0;

                if (event_Check_TheTE != null)
                {
                    if (NhapTuoi > 6) event_Check_TheTE(false, null);// ckbTheTE.Enabled = false;
                    else event_Check_TheTE(true, null);
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }


        #endregion


        protected EventHandler event_DiaChiChange;
        public void setEvent_DiaChi_Change(EventHandler eventDiaChi)
        {
            event_DiaChiChange = eventDiaChi;
        }

        protected EventHandler event_Check_TheTE;
        public void setEvent_Check_TheTE(EventHandler eventCheck_TheTE)
        {
            event_Check_TheTE = eventCheck_TheTE;
        }

        protected EventHandler event_Load_BN;
        public void setEvent_Load_BN(EventHandler eventLoad_BN)
        {
            event_Load_BN = eventLoad_BN;
        }
        
    }
}
