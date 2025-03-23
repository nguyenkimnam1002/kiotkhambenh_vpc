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
using System.Text.RegularExpressions;

//https://documentation.devexpress.com/WindowsForms/1501/Controls-and-Libraries/Editors-and-Simple-Controls/Simple-Editors/Concepts/Masks/Mask-Type-Extended-Regular-Expressions

namespace VNPT.HIS.Controls.NgoaiTru
{
    public partial class ucThongTinHanhChinh_TiepNhan : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ucThongTinHanhChinh_TiepNhan()
        {
            InitializeComponent();
        }

        private void ucThongTinHanhChinh_TiepNhan_Load(object sender, EventArgs e)
        {
            try
            { 
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
                log.Info("load tt 1 1");

                dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_Dantoc);
                ucDantoc.setData(dt, 0, 1);
                ucDantoc.setEvent_Enter(ucDantoc_KeyEnter);


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
                dt = RequestHTTP.Cache_getTinhTP(true); // mã1 - Tên - số TT - mã2 ["100000000","TP Hà Nội","1","01"]
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["col1"] = "-1";
                    dr["col2"] = "-- Chọn --";
                    dr["col3"] = "";
                    dr["col4"] = "-1";
                    dt.Rows.InsertAt(dr, 0);

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
                }

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

            ucGioitinh.SelectIndex = 1;
            ucNghenghiep.SelectIndex = 0;
            ucDantoc.SelectIndex = 24;
            ucQuoctich.SelectIndex = 0;
            dtimeNgaysinh.EditValue = null;
            txtNamsinh.Text = "";
            txtTuoi.Text = "";
            cboTuoi.SelectedIndex = 0;

            txtSoNha.Text = "";
            txtNoilamviec.Text = "";
            txtDienThoaiBN.Text = "";
            txtTenNN.Text = "";
            txtDiaChiNN.Text = "";
            txtDienThoaiNN.Text = "";

            ucTinhHuyenXa.searchLookUpEdit.EditValue = Const.local_user.HOSPITAL_CODE;
            ucTinh.SelectIndex = ucTinh.GetIndexByColumn("col1", Const.local_user.PROVINCE_ID);
            //ucHuyen.SelectIndex = 0;
            //ucXa.SelectIndex = 0;
            set_txtDcBN();
        }

        public DataTable dtTTHanhChinh = new DataTable();
 
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
                    
                    try
                    {
                        if (dtTTHanhChinh.Rows[0]["NGAY_SINH"].ToString() != "")
                        {
                            DateTime dtime = Func.ParseDate(dtTTHanhChinh.Rows[0]["NGAY_SINH"].ToString());
                            dtimeNgaysinh.DateTime = dtime;
                        }
                    }
                    catch (Exception ex) { log.Fatal(ex.ToString()); }

                    txtNamsinh.Text = dtTTHanhChinh.Rows[0]["NAMSINH"].ToString();
                    txtTuoi.Text = dtTTHanhChinh.Rows[0]["TUOI"].ToString(); // ko cần gán
                     
                    txtSoNha.Text = dtTTHanhChinh.Rows[0]["SONHA"].ToString();
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

                    //NGUOITHAN": "", 
                    txtNoilamviec.Text = dtTTHanhChinh.Rows[0]["NOILAMVIEC"].ToString();
                    txtTenNN.Text = dtTTHanhChinh.Rows[0]["TENNGUOITHAN"].ToString();
                    txtDienThoaiNN.Text = dtTTHanhChinh.Rows[0]["DIENTHOAINGUOITHAN"].ToString();
                    txtDiaChiNN.Text = dtTTHanhChinh.Rows[0]["DIACHINGUOITHAN"].ToString();
                    txtDienThoaiBN.Text = dtTTHanhChinh.Rows[0]["SDTBENHNHAN"].ToString();


                    //////"CHANDOANTUYENDUOI": "Bệnh tả", 
                    //////MACHANDOANTUYENDUOI": "A00", 
                    //////txtChuandoan.Text = // ko gán trường này
                    //txtChuandoan.Text = benhnhan.CHANDOANTUYENDUOI;

                    //DataTable dt = ServiceTiepNhanBenhNhan.getLichSuKhamBenh(benhnhan.BENHNHANID);
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    txtLichsukham.Text += dt.Rows[i][1].ToString() + "\r\n";
                    //}
                    //////MANOIGIOITHIEU: "", 
                    //////HINHTHUCVAOVIENID": "3", // 2 cấp cứu, khám 3
                    //rbtCapCuu.EditValue = benhnhan.HINHTHUCVAOVIENID;

                    //////TRANGTHAIKHAMBENH: "1", // 1 chờ, 4 đang khám, 9 kết thúc
                    //////DTBNID": "1", // 1 Bảo hiểm, 2 khám viện phí, 3 khám dịch vụ
                    //ucDoituong.SelectValue = benhnhan.DTBNID;
                    //////UUTIENKHAMID": "0",  
                    //ckbUuTien.Checked = benhnhan.UUTIENKHAMID == "1";

                    //// "19/06/2017 10:18"
                    //try
                    //{
                    //    if (benhnhan.NGAYTIEPNHAN != "")
                    //    {
                    //        DateTime dtime = DateTime.ParseExact(benhnhan.NGAYTIEPNHAN, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    //        dtimeNgaykham.DateTime = dtime;
                    //    }
                    //}
                    //catch (Exception ex) {log.Fatal(ex.ToString()); }

                    //if (benhnhan.MA_BHYT != "")
                    //{
                    //    txtSoThe.Text = benhnhan.MA_BHYT;
                    //}

                    ////BHYT_BD": "22/10/2016", 
                    //try
                    //{
                    //    if (benhnhan.BHYT_BD != "")
                    //    {
                    //        DateTime dtime = DateTime.ParseExact(benhnhan.BHYT_BD, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //        dtimeTungay.DateTime = dtime;
                    //    }
                    //    //BHYT_KT": "21/10/2022,
                    //    if (benhnhan.BHYT_KT != "")
                    //    {
                    //        DateTime dtime = DateTime.ParseExact(benhnhan.BHYT_KT, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //        dtimeDenngay.DateTime = dtime;
                    //    }
                    //}
                    //catch (Exception ex) { }
                    ////"MA_KCBBD": "35148", 
                    ////TEN_KCBBD: "Bệnh viện sản nhi Hà Nam", 
                    //ucDKKCB.SelectValue = benhnhan.MA_KCBBD;
                    ////DIACHI_BHYT": "Phường Trần Hưng Đạo-Quận Hoàn Kiếm-TP Hà Nội",   
                    //txtDcBaoHiem.Text = benhnhan.DIACHI_BHYT;

                    ////BHYT_LOAIID": "1", đúng tuyến, 2 đúng tuyến gt, 3 đúng ccuu, 4 trái
                    //ucTuyen.SelectValue = benhnhan.BHYT_LOAIID;

                    ////DT_SINHSONG": "5", 
                    //ucNoisong.SelectValue = benhnhan.DT_SINHSONG;

                    ////DU5NAM6THANGLUONGCOBAN": "0", 
                    //ckbDu5n6t.Checked = benhnhan.DU5NAM6THANGLUONGCOBAN == "1";

                    ////QUYEN_LOI": null,
                    ////"MUC_HUONG": null,
                    //txtMuchuong.Text = benhnhan.MUC_HUONG;

                    //////DICHVUID": null,
                    ////"THUKHAC": "0", 
                    //ucThukhac.SelectValue = benhnhan.THUKHAC;

                    //////TENNOIGIOITHIEU": "", 
                    //ucNoichuyen.SelectValue = benhnhan.TENNOIGIOITHIEU;



                    //////SLXN": "0", 
                    //////SLCDHA": "0", 
                    //////"ANHBENHNHAN": null,  
                    //////SLCHUYENKHOA": "0", 
                    //////CONGKHAM": "0", 
                    //////NGAYTHUOC": "06/06/2017 16:31:00"}]",

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
            ucXa.clearData();             
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
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["col1"] = "-1";
                        dr["col2"] = "-- Chọn --";
                        dr["col3"] = "";
                        dr["col4"] = "-1";
                        dt.Rows.InsertAt(dr, 0);

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
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["col1"] = "-1";
                        dr["col2"] = "-- Chọn --";
                        dr["col3"] = "";
                        dr["col4"] = "-1";
                        dt.Rows.InsertAt(dr, 0);

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
                //if (txtThonPho.Text.Trim() != "") txtDcBN_Text += "-" + txtThonPho.Text.Trim();
                if (ucXa.SelectIndex > 0) txtDcBN_Text += "-" + ucXa.SelectText;
                if (ucHuyen.SelectIndex > 0) txtDcBN_Text += "-" + ucHuyen.SelectText;
                if (ucTinh.SelectIndex > 0) txtDcBN_Text += "-" + ucTinh.SelectText;
                if (txtDcBN_Text.StartsWith("-")) txtDcBN_Text = txtDcBN_Text.Substring(1);

                txtDiaChiBN.Text = txtDcBN_Text;

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
            if (dtimeNgaysinh.Text.Trim()!="" && dtimeNgaysinh.DateTime < new DateTime(1900, 1, 1))
            {
                MessageBox.Show("Ngày sinh không được nhỏ hơn ngày 01/01/1900");
                dtimeNgaysinh.Focus();
                return false;
            }
            
            if (txtNamsinh.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập năm sinh");
                txtNamsinh.Focus();
                return false;
            }
            if (Func.Parse(txtNamsinh.Text.Trim()) < 1900)
            {
                MessageBox.Show("Năm sinh là số nguyên dương và năm sinh không nhỏ hơn năm 1900");
                txtNamsinh.Focus();
                return false;
            }

            if (dtimeNgaysinh.Text.Trim() != "" && txtNamsinh.Text.Trim() != "" && dtimeNgaysinh.DateTime.Year != Func.Parse(txtNamsinh.Text))
            {
                MessageBox.Show("Năm sinh không khớp với Ngày sinh!");
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
            if (txtDiaChiBN.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ");
                txtDiaChiBN.Focus();
                return false;
            }

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
            if (event_Load_BN != null) event_Load_BN(txtMaBN.Text, null);
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
                dtimeNgaysinh.Leave -= dtimeNgaysinh_Leave;
                txtNamsinh.Focus();
                dtimeNgaysinh_Leave(null, null);
                dtimeNgaysinh.Leave += dtimeNgaysinh_Leave;
            }
        }
        private void txtNamsinh_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtNamsinh.Leave -= txtNamsinh_Leave;
                txtTuoi.Focus();
                txtNamsinh_Leave(null, null);
                txtNamsinh.Leave += txtNamsinh_Leave;
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
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) ucTinhHuyenXa.Focus();
        }
        //private void txtThonPho_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) ucTinhHuyenXa.Focus();
        //}
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
            txtDiaChiBN.Focus();
        }
        private void txtNoilamviec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtTenNN.Focus();
        }
        //private void txtNguoiNha_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtTenNN.Focus();
        //}
        private void txtTenNN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtDiaChiNN.Focus();
        }
        private void txtDiaChiNN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtDienThoaiNN.Focus();
        }
        private void txtDiaChiBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtDienThoaiBN.Focus();
        }

        private void txtDienThoaiBN_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtNoilamviec.Focus();
        }

        private void txtDienThoaiNN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                if (event_KeyEnter != null) event_KeyEnter(null, null);
        }

         
        #endregion

        #region Sk phần Năm sinh-Tuổi
        private void Nhap_NgaySinh()
        {
            try
            {
                if (dtimeNgaysinh.EditValue == null || dtimeNgaysinh.Text.Trim() == "") return;

                DateTime sys_dtime = Func.getSysDatetime_Short();

                if (dtimeNgaysinh.DateTime > sys_dtime)
                {
                    MessageBox.Show(Const.mess_erro_ngaysinh);
                    dtimeNgaysinh.EditValue = null; 
                    dtimeNgaysinh.Focus();
                    return;
                } 
               // txtNamsinh.TextChanged -= new EventHandler(txtNamsinh_TextChanged);
                txtNamsinh.Text = dtimeNgaysinh.DateTime.Year.ToString();
               // txtNamsinh.TextChanged += new EventHandler(txtNamsinh_TextChanged);

                DateTime dtNhap = dtimeNgaysinh.DateTime.AddDays(0);
                int chenhNam = sys_dtime.Year - dtNhap.Year;
               
                DateTime dt2 = dtNhap.AddYears(chenhNam);
                int chenhThang = chenhNam * 12 + sys_dtime.Month - dtNhap.Month;
              
                if (chenhThang >= 36)// hiển thị số tuổi
                { 
                    txtTuoi.Text = chenhNam.ToString(); 
                    cboTuoi.SelectedIndex = 0;

                    if (chenhNam > 6) event_Check_TheTE(false, null);// ckbTheTE.Enabled = false;
                    else event_Check_TheTE(true, null);
                }
                else
                {
                    if (chenhThang >= 1)// hiển thị số tháng
                    {
                        //if (chenhNgay % 30 > 0) chenhThang++; 
                        txtTuoi.Text = chenhThang.ToString(); 
                        cboTuoi.SelectedIndex = 1;
                    }
                    else
                    {
                        TimeSpan ts = TimeSpan.FromTicks(sys_dtime.Ticks - dtNhap.Ticks);
                        Double chenhNgay = ts.TotalDays + 1; 
                        txtTuoi.Text = (chenhNgay).ToString(); 
                        cboTuoi.SelectedIndex = 2;
                    }
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }

        private void dtimeNgaysinh_Leave(object sender, EventArgs e)
        {
            if (dtimeNgaysinh.EditValue == null || dtimeNgaysinh.Text == "") return;
            Nhap_NgaySinh();
        }
        private void dtimeNgaysinh_EditValueChanged(object sender, EventArgs e)
        {
            //if (dtimeNgaysinh.EditValue == null || dtimeNgaysinh.Text == "") return;
            //Nhap_NgaySinh();
        }
         
        
        private void txtNamsinh_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtNamsinh.Text.Trim() == "") return;

                DateTime dtimeNow = Func.getSysDatetime_Short();
                int NhapNam = Func.Parse(txtNamsinh.Text.Trim());
                txtNamsinh.Text = NhapNam.ToString();
                if (NhapNam > dtimeNow.Year)
                {
                    MessageBox.Show(Const.mess_erro_namsinh);
                    txtNamsinh.Text = "";
                    txtNamsinh.Focus();
                    return;
                }

                txtTuoi.Text = (dtimeNow.Year - NhapNam).ToString();
                cboTuoi.SelectedIndex = 0;

                if (NhapNam != dtimeNgaysinh.DateTime.Year)
                {
                    dtimeNgaysinh.EditValue = null;
                }

                if (dtimeNow.Year - NhapNam > 6) event_Check_TheTE(false, null);// ckbTheTE.Enabled = false;
                else event_Check_TheTE(true, null);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                dtimeNgaysinh.EditValue = null;
                txtNamsinh.Text = "";
                txtTuoi.Text = "";
                cboTuoi.SelectedIndex = 0;
            }
        }

         
        private void txtTuoi_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtTuoi.Text.Trim() == "") return;

                DateTime dtimeNow = Func.getSysDatetime_Short();
                int NhapTuoi = Func.Parse(txtTuoi.Text.Trim());
                txtTuoi.Text = NhapTuoi.ToString();

                txtNamsinh.Text = (dtimeNow.Year - NhapTuoi).ToString();
                cboTuoi.SelectedIndex = 0;

                if (txtNamsinh.Text != dtimeNgaysinh.DateTime.Year.ToString())
                {
                    dtimeNgaysinh.EditValue = null;
                }

                if (NhapTuoi > 6) event_Check_TheTE(false, null);// ckbTheTE.Enabled = false;
                else event_Check_TheTE(true, null);
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

        protected EventHandler event_KeyEnter;
        public void setEvent_KeyEnter(EventHandler eventKeyEnter)
        {
            event_KeyEnter = eventKeyEnter;
        }
         
        private void txt_KeyPress_OnlyNumber(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //if (e.Handled == false && !char.IsControl(e.KeyChar) && txtNamsinh.Text.Length >= 4) e.Handled = true; 
        }

        private void txt_TextChanged_OnlyNumber(object sender, EventArgs e)
        { 
            //txtNamsinh.TextChanged -= txt_TextChanged_OnlyNumber;
            ////TextEdit txt = (TextEdit)sender;
            //Regex rgx = new Regex("[^0-9]");
            //string x= rgx.Replace(txtNamsinh.Text, "");

            //txtNamsinh.Text = x;
            txtNamsinh.TextChanged += txt_TextChanged_OnlyNumber;
        }
         
    }
}
