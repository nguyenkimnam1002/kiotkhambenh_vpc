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

namespace MainForm
{
    public partial class formThemSuaBN : DevExpress.XtraEditors.XtraForm
    {
        private DataRowView drvBn;
        bool isEdit = false;
        private string dichVuKhamBenhId = "";
        private string mauBenhPhamId = "";
        private string hidMucHuongNgoai = "";

        public formThemSuaBN()
        {
            InitializeComponent();
        }

        private void formThemSuaBN_Load(object sender, EventArgs e)
        {
            loadThongTinBenhNhan();
            loadThongTinKham();
            loadThongTinBaoHiem();
        }

        public void loadData(DataRowView drvBenhNhan)
        {
            if (drvBenhNhan != null)
            {
                drvBn = drvBenhNhan;
                isEdit = true;
            }
        }

        private void loadThongTinBenhNhan()
        {
            txtTenBN.Select();
            if (isEdit)
            {
                txtMaBN.Text = drvBn["MABENHNHAN"].ToString();
                txtTenBN.Text = drvBn["TENBENHNHAN"].ToString();
                txtNgaySinh.Text = drvBn["NGAYSINH"].ToString();
                txtNguoiThan.Text = drvBn["NGUOINHA"].ToString();
                hidMucHuongNgoai = drvBn["TYLEBH"].ToString();
            }
            else
                //cboDvTuoi
                cboDvTuoi.SelectedIndex = 0;

            // cbo Giới tính - Nghề nghiệp - Dân tộc - Quốc tịch
            DataTable dt = new DataTable();
            string sqlQuery = "";
            dt.Columns.Add("GIOITINHID", typeof(String));
            dt.Columns.Add("TEN", typeof(String));
            dt.Rows.Add(new string[] { "1", "Nam" });
            dt.Rows.Add(new string[] { "2", "Nữ" });
            dt.Rows.Add(new string[] { "3", "Khác" });
            cboGioiTinh.setData(dt, 0, 1);
            cboGioiTinh.setEvent_Enter(cboGioiTinh_KeyEnter);
            if(isEdit)
                cboGioiTinh.SelectValue = drvBn["GIOITINHID"].ToString();
            else
                cboGioiTinh.SelectIndex = 0;

            LocalConst.LOCAL_SQLITE.SqliteTable_Select(LocalConst.tbl_DMC_NGHENGHIEP, out dt);
            cboNgheNghiep.setData(dt, 0, 1);
            cboNgheNghiep.setEvent_Enter(cboNgheNghiep_KeyEnter);
            if (isEdit)
                cboNgheNghiep.SelectValue = drvBn["NGHENGHIEPID"].ToString();
            else
                cboNgheNghiep.SelectIndex = 0;

            LocalConst.LOCAL_SQLITE.SqliteTable_Select(LocalConst.tbl_DMC_DANTOC, out dt);
            cboDanToc.setData(dt, 0, 1);
            cboDanToc.setEvent_Enter(cboDanToc_KeyEnter);
            if (isEdit)
                cboDanToc.SelectValue = drvBn["DANTOCID"].ToString();
            else
                cboDanToc.SelectValue = "25";

            sqlQuery = "SELECT DIAPHUONGID, TENDIAPHUONG FROM DMC_DIAPHUONG WHERE CAP = '0'";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboQuocTich.setData(dt, 0, 1);
            cboQuocTich.setEvent_Enter(cboQuocTich_KeyEnter);
            if (isEdit)
                cboQuocTich.SelectValue = drvBn["QUOCTICHID"].ToString();
            else
                cboQuocTich.SelectValue = "0";

            // Tỉnh huyện xã
            sqlQuery = "SELECT diaphuongid AS VALUE, tendiaphuongdaydu AS NAME, tenviettatdaydu FROM DMC_DIAPHUONG";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            ucTinhHuyenXa.Option_LockKeyTab = true;
            ucTinhHuyenXa.setData(dt, "TENVIETTATDAYDU", "NAME");
            ucTinhHuyenXa.setEvent_Enter(ucTinhHuyenXa_KeyEnter);
            ucTinhHuyenXa.setEvent(ucTinhHuyenXa_SelectedIndexChanged);
            ucTinhHuyenXa.setColumn("VALUE", -1, "", 0);
            ucTinhHuyenXa.setColumn("TENVIETTATDAYDU", 0, "Tên viết tắt", 0);
            ucTinhHuyenXa.setColumn("NAME", 1, "Địa phương", 0);

            // Ds các tỉnh
            sqlQuery = "SELECT DIAPHUONGID, TENDIAPHUONG, MATIMKIEM, MADIAPHUONG FROM DMC_DIAPHUONG WHERE CAP = '1' ORDER BY MATIMKIEM";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboTinh.setEvent(cboTinh_SelectedIndexChanged);
            cboTinh.setEvent_Enter(cboTinh_KeyEnter);
            cboTinh.setData(dt, "MATIMKIEM", "TENDIAPHUONG");
            cboTinh.setColumn("MATIMKIEM", 0, "STT", 0);
            cboTinh.setColumn("TENDIAPHUONG", 1, "Tỉnh/TP", 0);
            cboTinh.setColumn("DIAPHUONGID", -1, "", 0);
            cboTinh.setColumn("MADIAPHUONG", -1, "Mã tỉnh", 0);
            cboHuyen.setEvent(cboHuyen_SelectedIndexChanged);
            cboHuyen.setEvent_Enter(cboHuyen_KeyEnter);
            cboXa.setEvent(cboXa_SelectedIndexChanged);
            cboXa.setEvent_Enter(cboXa_KeyEnter);
            if (isEdit)
                setComboTinh_Huyen_Xa_byDiaPhuongID(drvBn["DIAPHUONGID"].ToString());
        }

        private void loadThongTinKham() 
        {
            DataTable dt = new DataTable();
            string sqlQuery = "";
            //set Ngày khám
            if(isEdit)
                txtNgayKham.Text = drvBn["NGAYKHAM"].ToString();
            else
                txtNgayKham.DateTime = DateTime.Now;
            //Đối tượng
            dt.Columns.Add("DTBNID", typeof(String));
            dt.Columns.Add("TENDTBN", typeof(String));
            dt.Rows.Add(new string[] { "1", "BHYT" });
            dt.Rows.Add(new string[] { "2", "Viện phí" });
            dt.Rows.Add(new string[] { "3", "Dịch vụ" });
            cboDoiTuong.setEvent(cboDoiTuong_SelectedIndexChanged);
            cboDoiTuong.setEvent_Enter(cboDoiTuong_KeyEnter);
            cboDoiTuong.setData(dt, 0, 1);
            if (isEdit)
                cboDoiTuong.SelectValue = drvBn["DOITUONGBENHNHANID"].ToString();
            else
                cboDoiTuong.SelectIndex = 0;

            //yêu cầu khám
            sqlQuery = "SELECT DICHVUID,((SELECT COUNT(1)  ";
            sqlQuery += "    FROM (SELECT TENDICHVU FROM DMC_DICHVU  ";
            sqlQuery += "    WHERE loainhomdichvu = '2' AND khoa = 0 AND daxoa = 0 AND loaidichvu <> 1) temp ";
            sqlQuery += "  WHERE temp.TENDICHVU <= t.TENDICHVU) ";
            sqlQuery += "  ||'-'||TENDICHVU||'('||MADICHVU||')') TENDICHVU, MADICHVU ";
            sqlQuery += "FROM ( ";
            sqlQuery += "  (SELECT DICHVUID, MADICHVU, TENDICHVU ";
            sqlQuery += "  FROM DMC_DICHVU ";
            sqlQuery += "  WHERE loainhomdichvu = '2' AND khoa = 0 AND daxoa = 0 AND loaidichvu <> 1 ";
            sqlQuery += "  AND CASE " + cboDoiTuong.SelectValue;
            sqlQuery += "      WHEN 1 THEN giabhyt ";
            sqlQuery += "      WHEN 2 THEN gianhandan ";
            sqlQuery += "      WHEN 3 THEN giadichvu ";
            sqlQuery += "      ELSE 1 ";
            sqlQuery += "    END           > 0 ";
            sqlQuery += "  ORDER BY TENDICHVU) t) ";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboYeuCauKham.setData(dt, "DICHVUID", "TENDICHVU");
            cboYeuCauKham.setEvent_Enter(cboYeuCauKham_KeyEnter);
            cboYeuCauKham.setColumn("DICHVUID", -1, "", 0);
            cboYeuCauKham.setColumn("TENDICHVU", 0, "Yêu cầu khám", 0);
            cboYeuCauKham.setColumn("MADICHVU", -1, "", 0);

            //Phòng khám
            sqlQuery = "SELECT ORG_ID, ORG_NAME FROM ORG_PHONG WHERE ORG_TYPE = 2";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboPhongKham.setData(dt, "ORG_ID", "ORG_NAME");
            cboPhongKham.setEvent_Enter(cboPhongKham_KeyEnter);

            //Bác sĩ điều trị
            sqlQuery = "SELECT USER_ID, USER_NAME, OFFICER_NAME, OFFICER_TYPE FROM ADM_USER";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboBacSiDT.setData(dt, "USER_ID", "OFFICER_NAME");
            cboBacSiDT.setColumn("USER_ID", 0, "ID", 20);
            cboBacSiDT.setColumn("USER_NAME", 1, "Login", 20);
            cboBacSiDT.setColumn("OFFICER_NAME", 2, "Tên Bác sĩ", 20);
            cboBacSiDT.setColumn("OFFICER_TYPE", -1, "", 0);
            cboBacSiDT.setEvent_Enter(cboBacSiDT_KeyEnter);
            if (isEdit)
            {
                sqlQuery = "SELECT mbp.MAUBENHPHAMID, dv.DICHVUKHAMBENHID, dv.DICHVUID FROM KBH_MAUBENHPHAM mbp, KBH_DICHVU_KHAMBENH dv ";
                sqlQuery += "WHERE mbp.MAUBENHPHAMID = dv.MAUBENHPHAMID AND mbp.LOAINHOMMAUBENHPHAM = 3 AND mbp.BENHNHANID = " + drvBn["BENHNHANID"].ToString();
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                cboYeuCauKham.SelectValue = dt.Rows[0]["DICHVUID"].ToString();
                mauBenhPhamId = dt.Rows[0]["MAUBENHPHAMID"].ToString();
                dichVuKhamBenhId = dt.Rows[0]["DICHVUKHAMBENHID"].ToString();
                cboPhongKham.SelectValue = drvBn["PHONGKHAMID"].ToString();
                cboBacSiDT.SelectValue = drvBn["BACSIDIEUTRIID"].ToString();
                rbtCapCuu.EditValue = drvBn["HINHTHUCVAOVIENID"].ToString();
            }
        }

        private void loadThongTinBaoHiem()
        {
            DataTable dt = new DataTable();
            string sqlQuery = "";
            // Nơi ĐK KCBBD
            sqlQuery = "SELECT BENHVIENKCBBD, TENBENHVIEN, DIACHI FROM DMC_BENHVIEN";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            ucDKKCB.setData(dt, "BENHVIENKCBBD", "TENBENHVIEN");
            ucDKKCB.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 35);
            ucDKKCB.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
            ucDKKCB.setColumn("DIACHI", 2, "Địa chỉ", 0);
            ucDKKCB.setEvent_Enter(ucDKKCB_KeyEnter);

            // cbo Tuyến
            dt = new DataTable();
            dt.Columns.Add("VALUE", typeof(String));
            dt.Columns.Add("NAME", typeof(String));
            dt.Rows.Add(new string[] { "1", "Đúng tuyến" });
            dt.Rows.Add(new string[] { "2", "Đúng tuyến giới thiệu" });
            dt.Rows.Add(new string[] { "3", "Đúng tuyến cấp cứu" });
            dt.Rows.Add(new string[] { "4", "Trái tuyến" });
            cboTuyen.setData(dt, 0, 1);
            cboTuyen.SelectIndex = 0;
            cboTuyen.setEvent(cboTuyen_SelectedIndexChanged);
            cboTuyen.setEvent_Enter(cboTuyen_KeyEnter);

            // Đối tượng miễn giảm
            sqlQuery = "SELECT DOITUONGDACBIETID, TENDOITUONGDACBIET||'('||TYLEMIENGIAM||'%)' TENDOITUONGDACBIET, TYLEMIENGIAM, MA_BHYT FROM DMC_DOITUONG_DACBIET WHERE SUDUNG = 1";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            cboDTMienGiam.setData(dt, "DOITUONGDACBIETID", "TENDOITUONGDACBIET");
            cboDTMienGiam.setColumn("DOITUONGDACBIETID", 0, "STT", 0);
            cboDTMienGiam.setColumn("TENDOITUONGDACBIET", 1, "Đối tượng", 0);
            cboDTMienGiam.setColumn("TYLEMIENGIAM", 2, "Mã", 0);
            cboDTMienGiam.setColumn("MA_BHYT", -1, "", 0);
            cboDTMienGiam.setEvent_Enter(cboDTMienGiam_KeyEnter);

            // Load chuẩn đoán và chuẩn đoán phụ
            sqlQuery  = "SELECT ICD10CODE, ICD10NAME, ICD10NAME_EN, ICD10NAME_THUONGGOI FROM DMC_ICD ";
            sqlQuery += "WHERE (ICD10DISABLE != 1 OR ICD10DISABLE IS NULL) ";
            sqlQuery += "AND (ISREMOVE != 1 OR ISREMOVE IS NULL) ";
            sqlQuery += "ORDER BY ICD10CODE ASC ";
            LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
            ucChanDoan.setData(dt, "ICD10CODE", "ICD10NAME");
            ucChanDoan.setColumn("ICD10NAME_EN", -1, "", 0);
            ucChanDoan.setColumn("ICD10NAME_THUONGGOI", -1, "", 0);
            ucChanDoan.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucChanDoan.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
            ucChanDoan.setEvent_Check(ucChanDoan_Check);

            setButtonChanDoanPhu();
            ucChanDoanPhu.setData(dt, "ICD10CODE", "ICD10NAME");
            ucChanDoanPhu.setColumn("ICD10NAME_EN", -1, "", 0);
            ucChanDoanPhu.setColumn("ICD10NAME_THUONGGOI", -1, "", 0);
            ucChanDoanPhu.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucChanDoanPhu.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
            ucChanDoanPhu.setEvent_Check(ucChanDoanPhu_Check);
            if (isEdit)
            {
                txtMaThe.Text = drvBn["MATHEBHYT"].ToString();
                chkTheTE.Checked = (drvBn["SINHTHETE"].ToString() == "1") ? true : false;
                chkDu5Nam.Checked = (drvBn["DU5NAM"].ToString() == "1") ? true : false;
                chkDu6Thang.Checked = (drvBn["DU6THANG"].ToString() == "1") ? true : false;
                txtNgayBd.Text = drvBn["THOIGIAN_BD"].ToString();
                txtNgayKt.Text = drvBn["THOIGIAN_KT"].ToString();
                ucDKKCB.SelectedValue = drvBn["DKKCBBDID"].ToString();
                cboTuyen.SelectValue = drvBn["TUYENID"].ToString();
                cboDTMienGiam.SelectValue = drvBn["DOITUONGMIENGIAMID"].ToString();
                ucChanDoan.searchLookUpEdit.EditValue = drvBn["MACHANDOANRAVIEN"].ToString();
                ucChanDoanPhu.SelectedText = drvBn["CHANDOANRAVIEN_KT"].ToString();
            }
        }

        private void cboTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboHuyen.clearData();            
            set_txtDiaChi();
            try
            {
                DataTable dt = new DataTable();
                DataRowView drv = cboTinh.SelectDataRowView;
                if (drv != null)
                {
                    string id_tinh = drv["DIAPHUONGID"].ToString();
                    string sqlQuery = "SELECT DIAPHUONGID, TENDIAPHUONG, MATIMKIEM, MABH, MADIAPHUONG FROM DMC_DIAPHUONG WHERE DIAPHUONGCHAID = " + id_tinh + " ORDER BY MATIMKIEM";
                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    cboHuyen.setData(dt, "MATIMKIEM", "TENDIAPHUONG");
                    cboHuyen.setColumn("MATIMKIEM", 0, "STT", 0);
                    cboHuyen.setColumn("TENDIAPHUONG", 1, "Huyện(Q)", 0);
                    cboHuyen.setColumn("DIAPHUONGID", -1, "Mã huyện", 0);
                    cboHuyen.setColumn("MADIAPHUONG", -1, "", 0);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void cboHuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboXa.clearData();
            set_txtDiaChi();
            try
            {
                DataTable dt = new DataTable();
                DataRowView drv = cboHuyen.SelectDataRowView;
                if (drv != null)
                {
                    string id_huyen = drv["DIAPHUONGID"].ToString();
                    string sqlQuery = "SELECT DIAPHUONGID, TENDIAPHUONG, MATIMKIEM, MABH, MADIAPHUONG FROM DMC_DIAPHUONG WHERE DIAPHUONGCHAID = " + id_huyen + " ORDER BY MATIMKIEM";
                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    cboXa.setData(dt, "MATIMKIEM", "TENDIAPHUONG");
                    cboXa.setColumn("MATIMKIEM", 0, "STT", 0);
                    cboXa.setColumn("TENDIAPHUONG", 1, "Xã(P)", 0);
                    cboXa.setColumn("DIAPHUONGID", -1, "Mã xã", 0);
                    cboXa.setColumn("MADIAPHUONG", -1, "", 0);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void cboXa_SelectedIndexChanged(object sender, EventArgs e)
        {
            set_txtDiaChi();
        }

        public void set_txtDiaChi()
        {
            try
            {
                //txtDiaChi.Text = "";
                if (cboXa.SelectIndex >= 0) txtDiaChi.Text = cboXa.SelectText;
                if (cboHuyen.SelectIndex >= 0) txtDiaChi.Text += "-" + cboHuyen.SelectText;
                if (cboTinh.SelectIndex >= 0) txtDiaChi.Text += "-" + cboTinh.SelectText;
                if (txtDiaChi.Text.StartsWith("-")) txtDiaChi.Text = txtDiaChi.Text.Substring(1);
                txtDiaChiBhyt.Text = txtDiaChi.Text;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void ucTinhHuyenXa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView dr = (DataRowView)sender;
                setComboTinh_Huyen_Xa_byDiaPhuongID(dr[0].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void setComboTinh_Huyen_Xa_byDiaPhuongID(string DiaPhuongID)
        {
            try
            {
                DataTable dt = new DataTable();
                string sqlQuery = "";
                string idXa = "", idHuyen = "", idTinh = "";
                int cap = 4;
                while(cap > 1)
                {
                    sqlQuery = "SELECT DIAPHUONGID, TENDIAPHUONG, TENVIETTAT, CAP, DIAPHUONGCHAID FROM DMC_DIAPHUONG WHERE DIAPHUONGID = " + DiaPhuongID;
                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    if(dt.Rows.Count < 0) break;
                    DataRow dr = dt.Rows[0];
                    cap = int.Parse(dr["CAP"].ToString());

                    if(cap == 3){
                        idXa = dr["DIAPHUONGID"].ToString();
                    }
                    if(cap == 2){
                        idHuyen = dr["DIAPHUONGID"].ToString();
                    }
                    if(cap == 1){
                        idTinh = dr["DIAPHUONGID"].ToString();
                    }
                    DiaPhuongID = dr["DIAPHUONGCHAID"].ToString();
                }

                if (!string.IsNullOrEmpty(idTinh))
                {
                    DataTable dtTinh = (DataTable)cboTinh.lookUpEdit.Properties.DataSource;
                    for (int i = 0; i < dtTinh.Rows.Count; i++)
                        if (dtTinh.Rows[i]["DIAPHUONGID"].ToString() == idTinh)
                        {
                            cboTinh.SelectValue = dtTinh.Rows[i]["MATIMKIEM"].ToString();
                            break;
                        }
                }
                if (!string.IsNullOrEmpty(idHuyen))
                {
                    DataTable dtHuyen = (DataTable)cboHuyen.lookUpEdit.Properties.DataSource;
                    for (int i = 0; i < dtHuyen.Rows.Count; i++)
                        if (dtHuyen.Rows[i]["DIAPHUONGID"].ToString() == idHuyen)
                        {
                            cboHuyen.SelectValue = dtHuyen.Rows[i]["MATIMKIEM"].ToString();
                            break;
                        }
                }
                if (!string.IsNullOrEmpty(idXa)) 
                {
                    DataTable dtXa = (DataTable)cboXa.lookUpEdit.Properties.DataSource;
                    for (int i = 0; i < dtXa.Rows.Count; i++)
                        if (dtXa.Rows[i]["DIAPHUONGID"].ToString() == idXa)
                        {
                            cboXa.SelectValue = dtXa.Rows[i]["MATIMKIEM"].ToString();
                            break;
                        }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void txtNgaySinh_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNgaySinh.Text == "") return;
            TinhTuoi();
        }

        private void TinhTuoi()
        {
            try
            {
                DateTime dtimeNow = DateTime.Now;
                if (txtNgaySinh.DateTime > dtimeNow)
                {
                    MessageBox.Show(LocalConst.mess_erro_ngaysinh);
                    txtNgaySinh.Focus();
                    return;
                }
                txtNamSinh.TextChanged -= new EventHandler(txtNamSinh_TextChanged);
                txtNamSinh.Text = txtNgaySinh.DateTime.Year.ToString();
                txtNamSinh.TextChanged += new EventHandler(txtNamSinh_TextChanged);

                DateTime dtNhap = txtNgaySinh.DateTime.AddDays(0);
                int chenhNam = dtimeNow.Year - dtNhap.Year;

                DateTime dt2 = dtNhap.AddYears(chenhNam);
                int chenhThang = chenhNam * 12 + dtimeNow.Month - dtNhap.Month;

                if (chenhThang >= 36)// hiển thị số tuổi
                {
                    txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                    txtTuoi.Text = chenhNam.ToString();
                    txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                    cboDvTuoi.SelectedIndex = 0;
                }
                else
                {
                    if (chenhThang >= 1)// hiển thị số tháng
                    {
                        txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                        txtTuoi.Text = chenhThang.ToString();
                        txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                        cboDvTuoi.SelectedIndex = 1;
                    }
                    else
                    {
                        TimeSpan ts = TimeSpan.FromTicks(dtimeNow.Ticks - dtNhap.Ticks);
                        Double chenhNgay = ts.TotalDays + 1;
                        txtTuoi.TextChanged -= new EventHandler(txtTuoi_TextChanged);
                        txtTuoi.Text = (Math.Round(chenhNgay)).ToString();
                        txtTuoi.TextChanged += new EventHandler(txtTuoi_TextChanged);
                        cboDvTuoi.SelectedIndex = 2;
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void txtNamSinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtimeNow = DateTime.Now;
                int NhapNam = Convert.ToInt32(txtNamSinh.Text.Trim());
                if (NhapNam > dtimeNow.Year)
                {
                    MessageBox.Show(LocalConst.mess_erro_namsinh);
                    txtNamSinh.Text = "";
                    txtNamSinh.Focus();
                    return;
                }
                txtTuoi.Text = (dtimeNow.Year - NhapNam).ToString();
                cboDvTuoi.SelectedIndex = 0;
                txtNgaySinh.Text = "";
            }
            catch (Exception ex)
            {
                txtNgaySinh.Text = "";
                txtNamSinh.Text = "";
                txtTuoi.Text = "";
                cboDvTuoi.SelectedIndex = 0;
            }
        }

        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtimeNow = DateTime.Now;
                int NhapTuoi = Convert.ToInt32(txtTuoi.Text.Trim());
                txtNamSinh.Text = (dtimeNow.Year - NhapTuoi).ToString();
                cboDvTuoi.SelectedIndex = 0;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void cboDoiTuong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDoiTuong.SelectIndex > -1)
            {
                bool enable = true;
                if (cboDoiTuong.SelectValue == "1") //BHYT
                {
                    enable = true;
                    txtNgayBd.DateTime = new DateTime(DateTime.Now.Year, 1, 1);
                    txtNgayKt.DateTime = new DateTime(DateTime.Now.Year, 12, 31);
                    txtDiaChiBhyt.Text = txtDiaChi.Text;
                }
                else 
                {
                    enable = false;
                    txtMaThe.Text = "";
                    txtDiaChiBhyt.Text = "";
                    chkTheTE.Checked = false;
                    chkDu5Nam.Checked = false;
                    chkDu6Thang.Checked = false;
                    ucDKKCB.SelectedIndex = -1;
                    ucDKKCB.SelectedValue = "";
                    cboTuyen.SelectIndex = -1;
                    cboTuyen.SelectValue = "";
                    cboDTMienGiam.SelectIndex = -1;
                    cboDTMienGiam.SelectValue = "";
                }
                txtMaThe.Enabled = enable;
                chkTheTE.Enabled = enable;
                chkDu5Nam.Enabled = enable;
                chkDu6Thang.Enabled = enable;
                txtNgayBd.Enabled = enable;
                txtNgayKt.Enabled = enable;
                txtDiaChiBhyt.Enabled = enable;
                ucDKKCB.Enabled = enable; ucDKKCB.Option_CaptionValidate = enable;
                cboTuyen.Enabled = enable; cboTuyen.CaptionValidate = enable;
                cboDTMienGiam.Enabled = enable; cboDTMienGiam.CaptionValidate = enable;
            }
        }

        public void setButtonChanDoanPhu()
        {
            ucChanDoanPhu.btnEdit.Visible = false;
            ucChanDoanPhu.btnReset.Visible = true;
            ucChanDoanPhu.btnReset.Text = "Xóa";
            //ucChanDoanPhu.btnReset.Dock = DockStyle.None;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formThemSuaBN));
            ucChanDoanPhu.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            ucChanDoanPhu.btnReset.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            ucChanDoanPhu.btnReset.Width = 60;
        }

        private void ucChanDoan_Check (object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (ucChanDoanPhu.SelectList.ContainsKey(drv["ICD10CODE"].ToString()))
                {
                    ucChanDoan.searchLookUpEdit.Text = "";
                    ucChanDoan.messageError = "Bệnh chính trùng bệnh phụ";
                    return;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void ucChanDoanPhu_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (drv["ICD10CODE"].ToString() == ucChanDoan.SelectedValue)
                {
                    ucChanDoanPhu.searchLookUpEdit.Text = "";
                    ucChanDoanPhu.messageError = "Bệnh phụ trùng bệnh chính";
                    return;
                }
                if (ucChanDoanPhu.SelectList.ContainsKey(drv["ICD10CODE"].ToString()))
                {
                    ucChanDoanPhu.searchLookUpEdit.Text = "";
                    ucChanDoanPhu.messageError = "Bệnh phụ trùng";
                    return;
                }
                ucChanDoanPhu.searchLookUpEdit.Text = "";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            frm.Close();
        }

        private bool KiemTraNhap()
        {
            // Kiểm tra nhập thông tin bệnh nhân
            if (txtTenBN.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập Tên bệnh nhân");
                txtTenBN.Focus();
                return false;
            }
            if (txtNgaySinh.Text.Trim() == "" && cboDvTuoi.SelectedIndex != 0)
            {
                MessageBox.Show("Hãy nhập ngày sinh");
                txtNgaySinh.Focus();
                return false;
            }
            if (txtNamSinh.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập năm sinh");
                txtNamSinh.Focus();
                return false;
            }
            if (txtTuoi.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập tuổi");
                txtTuoi.Focus();
                return false;
            }
            if (cboTinh.SelectIndex < 0)
            {
                MessageBox.Show("Hãy chọn Tỉnh/TP");
                cboTinh.Focus();
                return false;
            }
            if (cboHuyen.SelectIndex < 0)
            {
                MessageBox.Show("Hãy chọn Quận/Huyện");
                cboHuyen.Focus();
                return false;
            }
            if (cboXa.SelectIndex < 0)
            {
                MessageBox.Show("Hãy chọn Xã/Phường");
                cboXa.Focus();
                return false;
            }
            if (txtDiaChi.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ");
                txtDiaChi.Focus();
                return false;
            }

            // Kiểm tra nhập thông tin khám
            if (txtNgayKham.DateTime > DateTime.Now)
            {
                txtNgayKham.Text = "";
                MessageBox.Show("Ngày khám không được lớn hơn ngày hiện tại");
                txtNgayKham.Focus();
                return false;
            }
            if (cboYeuCauKham.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập yêu cầu khám");
                cboYeuCauKham.Focus();
                return false;
            }
            if (cboPhongKham.Text.Trim() == "")
            {
                MessageBox.Show("Hãy nhập phòng khám");
                cboPhongKham.Focus();
                return false;
            }

            // Kiểm tra nhập thông tin bảo hiểm
            if (cboDoiTuong.SelectValue == "1")
            {
                if (txtMaThe.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập Mã thẻ BHYT");
                    txtMaThe.Focus();
                    return false;
                }
                if (txtMaThe.Text.Trim().Length != 15)
                {
                    MessageBox.Show("Sai Mã thẻ BHYT");
                    txtMaThe.Focus();
                    return false;
                }
                if (txtNgayBd.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập ngày bắt đầu thẻ BHYT");
                    txtNgayBd.Focus();
                    return false;
                }
                if (txtNgayKt.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập ngày kết thúc thẻ BHYT");
                    txtNgayKt.Focus();
                    return false;
                }
                if (txtNgayBd.DateTime > txtNgayKt.DateTime)
                {
                    MessageBox.Show("Thời gian BHYT chưa đến hạn");
                    txtNgayBd.Focus();
                    return false;
                }
                if (txtNgayKt.DateTime < txtNgayKham.DateTime)
                {
                    MessageBox.Show("Thời gian BHYT hết hạn");
                    txtNgayKt.Focus();
                    return false;
                }
                if (txtNgayKt.DateTime <= txtNgayBd.DateTime)
                {
                    MessageBox.Show("Từ ngày nhỏ hơn đến ngày");
                    txtNgayKt.Focus();
                    return false;
                }
                if (ucDKKCB.SelectedIndex < 0)
                {
                    MessageBox.Show("Hãy nhập Nơi ĐKKCB");
                    ucDKKCB.Focus();
                    return false;
                }
                if (txtDiaChiBhyt.Text.Trim() == "")
                {
                    MessageBox.Show("Hãy nhập địa chỉ BHYT");
                    txtDiaChiBhyt.Focus();
                    return false;
                }
                if (ucChanDoan.SelectedText == "")
                {
                    MessageBox.Show("Hãy nhập Chẩn đoán");
                    ucChanDoan.Focus();
                    return false;
                }
            }

            return true;
        }

        // Hàm gửi sự kiện để reload form tìm kiếm ở formMain khi lưu bệnh nhân thành công
        protected EventHandler evenChange;
        public void setEvent(EventHandler eventChangeValue)
        {
            evenChange = eventChangeValue;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = "";
                DataTable dt = new DataTable();
                if (KiemTraNhap() == false) return;
                ThongTinBenhNhan bn = new ThongTinBenhNhan();
                if (isEdit)
                    bn.BENHNHANID = drvBn["BENHNHANID"].ToString();
                if (txtMaBN.Text.Trim() == "")
                {
                    string valueMaBn = "";
                    sqlQuery = "SELECT MAX(BENHNHANID) MAXBN FROM DMC_BENHNHAN";
                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["MAXBN"].ToString()))
                        valueMaBn = (int.Parse(dt.Rows[0]["MAXBN"].ToString()) + 1).ToString();
                    else
                        valueMaBn = "1";
                    txtMaBN.Text = "BN" + valueMaBn.PadLeft(8, '0');
                }
                bn.MABENHNHAN = txtMaBN.Text.Trim();
                bn.TENBENHNHAN = txtTenBN.Text.Trim();
                bn.NGAYSINH = txtNgaySinh.Text.Trim();
                bn.NAMSINH = txtNamSinh.Text.Trim();
                bn.TUOI = txtTuoi.Text.Trim();
                bn.DVTUOI = (cboDvTuoi.SelectedIndex + 1).ToString();
                bn.GIOITINHID = cboGioiTinh.SelectValue;
                bn.NGHENGHIEPID = cboNgheNghiep.SelectValue;
                bn.DANTOCID = cboDanToc.SelectValue;
                bn.QUOCTICHID = cboQuocTich.SelectValue;
                bn.DIAPHUONGID = cboXa.SelectDataRowView["DIAPHUONGID"].ToString();
                bn.DIACHI = txtDiaChi.Text.Trim();
                bn.NGUOINHA = txtNguoiThan.Text.Trim();
                bn.NGAYKHAM = txtNgayKham.DateTime.ToString("dd/MM/yyyy HH:mm:ss");
                bn.PHONGKHAMID = cboPhongKham.SelectValue;
                bn.HINHTHUCVAOVIENID = rbtCapCuu.EditValue.ToString();
                bn.BACSIDIEUTRIID = cboBacSiDT.SelectValue;
                bn.DOITUONGBENHNHANID = cboDoiTuong.SelectValue;
                bn.MATHEBHYT = txtMaThe.Text.ToUpper();
                bn.THOIGIAN_BD = txtNgayBd.DateTime.ToString("dd/MM/yyyy");
                bn.THOIGIAN_KT = txtNgayKt.DateTime.ToString("dd/MM/yyyy");
                bn.SINHTHETE = Convert.ToInt32(chkTheTE.Checked).ToString();
                bn.DU5NAM = Convert.ToInt32(chkDu5Nam.Checked).ToString();
                bn.DU6THANG = Convert.ToInt32(chkDu6Thang.Checked).ToString();
                bn.DIACHIBHYT = txtDiaChiBhyt.Text.Trim();
                bn.DKKCBBDID = ucDKKCB.SelectedValue;
                bn.TUYENID = cboTuyen.SelectValue;
                bn.DOITUONGMIENGIAMID = cboDTMienGiam.SelectValue;
                bn.TYLEBH = hidMucHuongNgoai;
                bn.TYLEMIENGIAM = (cboDTMienGiam.SelectDataRowView != null) ? cboDTMienGiam.SelectDataRowView["TYLEMIENGIAM"].ToString() : "";
                bn.MACHANDOANRAVIEN = ucChanDoan.searchLookUpEdit.Text;
                bn.CHANDOANRAVIEN = ucChanDoan.SelectedText;
                bn.CHANDOANRAVIEN_KT = ucChanDoanPhu.SelectedText;

                MauBenhPhamObj mbp = new MauBenhPhamObj();
                mbp.MAUBENHPHAMID = isEdit ? mauBenhPhamId : "";
                string soPhieu = "";
                if (isEdit)
                {
                    sqlQuery = "SELECT SOPHIEU FROM KBH_MAUBENHPHAM WHERE MAUBENHPHAMID = " + mauBenhPhamId;
                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    mbp.SOPHIEU = dt.Rows[0]["SOPHIEU"].ToString();
                }
                else
                {
                    sqlQuery = "SELECT MAX(MAUBENHPHAMID) MAXMBP FROM KBH_MAUBENHPHAM";
                    LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                    if (dt.Rows.Count > 0 && !string.IsNullOrEmpty(dt.Rows[0]["MAXMBP"].ToString()))
                        soPhieu = (int.Parse(dt.Rows[0]["MAXMBP"].ToString()) + 1).ToString();
                    else
                        soPhieu = "1";
                    mbp.SOPHIEU = "P" + soPhieu.PadLeft(9, '0');
                }
                mbp.LOAINHOMMAUBENHPHAM = "3";
                mbp.BENHNHANID = "";
                mbp.NGAYMAUBENHPHAM = txtNgayKham.DateTime.ToString("dd/MM/yyyy");
                mbp.NGUOITAO = cboBacSiDT.SelectValue;
                mbp.KHOTHUOCID = "";
                mbp.PHONGTHUCHIENID = cboPhongKham.SelectValue;

                var dichVuId = cboYeuCauKham.SelectDataRowView["DICHVUID"].ToString();
                sqlQuery = "SELECT * FROM DMC_DICHVU WHERE DICHVUID=" + dichVuId;
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                DichVuKhamBenhObj dv = new DichVuKhamBenhObj();
                dv.DICHVUKHAMBENHID = isEdit ? dichVuKhamBenhId : "";
                dv.MAUBENHPHAMID = ""; 
                dv.BENHNHANID = "";
                dv.NGAYDICHVU = txtNgayKham.DateTime.ToString("dd/MM/yyyy");
                dv.DICHVUID = cboYeuCauKham.SelectDataRowView["DICHVUID"].ToString();
                dv.TENDICHVU = cboYeuCauKham.SelectDataRowView["TENDICHVU"].ToString();
                dv.SOLUONG = "1";
                 dv.DONGIA = dt.Rows[0]["GIABHYT"].ToString();
                dv.GIABHYT = dt.Rows[0]["GIABHYT"].ToString();
                dv.GIAVP = dt.Rows[0]["GIANHANDAN"].ToString();
                dv.GIADV = dt.Rows[0]["GIADICHVU"].ToString();
                dv.TYLEDV = "1";

                //Tính vienphi
                DataTable dtVp = new DataTable();
                DataTable r = new DataTable();
                dtVp.Columns.Add("DOITUONGBENHNHANID", typeof(String));
                dtVp.Columns.Add("MUCHUONG", typeof(String));
                dtVp.Columns.Add("GIATRANBH", typeof(String));
                dtVp.Columns.Add("GIABHYT", typeof(String));
                dtVp.Columns.Add("GIAND", typeof(String));
                dtVp.Columns.Add("GIADV", typeof(String));
                dtVp.Columns.Add("GIANN", typeof(String));
                dtVp.Columns.Add("DOITUONGCHUYEN", typeof(String));
                dtVp.Columns.Add("GIADVKTC", typeof(String));
                dtVp.Columns.Add("MANHOMBHYT", typeof(String));
                dtVp.Columns.Add("SOLUONG", typeof(String));
                dtVp.Columns.Add("CANTRENDVKTC", typeof(String));
                dtVp.Columns.Add("THEDUTHOIGIAN", typeof(String));
                dtVp.Columns.Add("DUOCVANCHUYEN", typeof(String));
                dtVp.Columns.Add("TYLETHUOCVATTU", typeof(String));
                dtVp.Columns.Add("NHOMDOITUONG", typeof(String));
                dtVp.Columns.Add("NGAYHANTHE", typeof(String));
                dtVp.Columns.Add("NGAYDICHVU", typeof(String));
                dtVp.Columns.Add("TYLE_MIENGIAM", typeof(String));
                DataRow dr = dtVp.NewRow();
                dr["DOITUONGBENHNHANID"] = cboDoiTuong.SelectValue;
                dr["MUCHUONG"] = hidMucHuongNgoai;
                //Lấy GIATRANBH = LUONGCOBAN * 15%
                //sqlQuery = "SELECT * FROM DMC_CAUHINH WHERE MACAUHINH = 'LUONGCOBAN'";
                //DataTable dt1 = new DataTable();
                //LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt1);
                //string luongCoBan = dt1.Rows[0]["GIATRI_MACDINH"].ToString();
                //dr["GIATRANBH"] = (float.Parse(luongCoBan) * 15/100).ToString();
                dr["GIATRANBH"] = !string.IsNullOrEmpty(dt.Rows[0]["DICHVU_BHYT_DINHMUC"].ToString()) ? dt.Rows[0]["DICHVU_BHYT_DINHMUC"].ToString() : "0";
                dr["GIABHYT"] = dt.Rows[0]["GIABHYT"].ToString();
                dr["GIAND"] = dt.Rows[0]["GIANHANDAN"].ToString();
                dr["GIADV"] = dt.Rows[0]["GIADICHVU"].ToString();
                dr["GIANN"] = "0";
                dr["DOITUONGCHUYEN"] = "0";
                dr["GIADVKTC"] = "0";

                sqlQuery = "SELECT * FROM DMC_NHOM_MABHYT WHERE NHOM_MABHYT_ID=" + dt.Rows[0]["NHOM_MABHYT_ID"].ToString();
                DataTable dtMaNhomBhyt = new DataTable();
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dtMaNhomBhyt);
                if (dtMaNhomBhyt.Rows.Count > 0)
                    dr["MANHOMBHYT"] = dtMaNhomBhyt.Rows[0]["MANHOM_BHYT"].ToString();
                dr["SOLUONG"] = "1";
                dr["CANTRENDVKTC"] = "0";
                dr["THEDUTHOIGIAN"] = "0";
                dr["DUOCVANCHUYEN"] = "0";
                dr["TYLETHUOCVATTU"] = "100";
                dr["NHOMDOITUONG"] = "0";
                dr["NGAYHANTHE"] = txtNgayKt.DateTime.ToString("dd/MM/yyyy");
                dr["NGAYDICHVU"] = txtNgayKham.DateTime.ToString("dd/MM/yyyy");
                dr["TYLE_MIENGIAM"] = bn.TYLEMIENGIAM;
                dtVp.Rows.InsertAt(dr, 0);
                r = VNPT.HIS.Common.Func.vienphi_tinhtien_dichvu(dtVp);
                DataRow r_dr = r.Rows[0];
                if (r_dr["bh_tra"].ToString() == "-1" && r_dr["nd_tra"].ToString() == "-1" && r_dr["tong_cp"].ToString() == "-1")
                {
                    MessageBox.Show("Giá tiền dịch vụ của bệnh nhân không thể bằng 0");
                    return;
                }
                dv.BHYTTRA = float.Parse(r_dr["bh_tra"].ToString()).ToString();
                dv.NHANDANTRA = float.Parse(r_dr["nd_tra"].ToString()).ToString();
                dv.LOAIDOITUONG = "";
                dv.GHICHU = "";
                bool result = LocalConst.LOCAL_SQLITE.sqliteTransaction_NhapBenhNhan(bn, mbp, dv, isEdit);
                if (result)
                {
                    if (isEdit)
                        MessageBox.Show("Cập nhật thông tin bệnh nhân thành công");
                    else
                    {
                        MessageBox.Show("Tiếp nhận bệnh nhân thành công");
                        ClearData();
                    }
                    evenChange(1, null);
                }
                else
                {
                    MessageBox.Show("Có lỗi khi tiếp nhận bệnh nhân");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void ClearData()
        {
            txtMaBN.Text = "";
            txtTenBN.Text = "";
            txtMaThe.Text = "";
        }

        // Xử lý các sự kiện bấm Enter = bấm phím tab
        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtTenBN.Focus();
        }

        private void txtTenBN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtNgaySinh.Focus();
        }

        private void txtNgaySinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtNamSinh.Focus();
        }

        private void txtNamSinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtTuoi.Focus();
        }

        private void txtTuoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) cboGioiTinh.Focus();
        }

        private void cboGioiTinh_KeyEnter(object sender, EventArgs e)
        {
            cboNgheNghiep.Focus();
        }

        private void cboNgheNghiep_KeyEnter(object sender, EventArgs e)
        {
            cboDanToc.Focus();
        }

        private void cboDanToc_KeyEnter(object sender, EventArgs e)
        {
            cboQuocTich.Focus();
        }

        private void cboQuocTich_KeyEnter(object sender, EventArgs e)
        {
            ucTinhHuyenXa.Focus();
        }

        private void ucTinhHuyenXa_KeyEnter(object sender, EventArgs e)
        {
            cboTinh.Focus();
        }

        private void cboTinh_KeyEnter(object sender, EventArgs e)
        {
            cboHuyen.Focus();
        }

        private void cboHuyen_KeyEnter(object sender, EventArgs e)
        {
            cboXa.Focus();
        }

        private void cboXa_KeyEnter(object sender, EventArgs e)
        {
            txtDiaChi.Focus();
        }

        private void txtDiaChi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtNguoiThan.Focus();
        }

        private void txtNguoiThan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) txtNgayKham.Focus();
        }

        private void txtNgayKham_KeyDown(object sender, KeyEventArgs e)
        {
            cboYeuCauKham.Focus();
        }

        private void cboYeuCauKham_KeyEnter(object sender, EventArgs e)
        {
            cboPhongKham.Focus();
        }

        private void cboPhongKham_KeyEnter(object sender, EventArgs e)
        {
            rbtCapCuu.Focus();
        }

        private void rbtCapCuu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboBacSiDT.Focus();
            }
        }

        private void cboBacSiDT_KeyEnter(object sender, EventArgs e)
        {
            cboDoiTuong.Focus();
        }

        private void cboDoiTuong_KeyEnter(object sender, EventArgs e)
        {
            txtMaThe.Focus();
        }

        private void txtMaThe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                chkTheTE.Focus();
        }

        private void chkTheTE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                chkDu5Nam.Focus();
        }

        private void chkDu5Nam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                chkDu6Thang.Focus();
        }

        private void chkDu6Thang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNgayBd.Focus();
        }

        private void txtNgayBd_KeyDown(object sender, KeyEventArgs e)
        {
            txtNgayKt.Focus();
        }

        private void txtNgayKt_KeyDown(object sender, KeyEventArgs e)
        {
            ucDKKCB.Focus();
        }

        private void ucDKKCB_KeyEnter(object sender, EventArgs e)
        {
            txtDiaChiBhyt.Focus();
        }

        private void txtDiaChiBhyt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cboTuyen.Focus();
        }

        private void cboTuyen_KeyEnter(object sender, EventArgs e)
        {
            cboDTMienGiam.Focus();
        }

        private void cboDTMienGiam_KeyEnter(object sender, EventArgs e)
        {
            ucChanDoan.Focus();
        }

        private void Lay_MucHuong_BHYT()
        {
            try
            {
                string sqlQuery = "";
                DataTable dt = new DataTable();

                //if (!(txtMaThe.Text.Length >= 14 && txtMaThe.Text.ToUpper().StartsWith("TE"))) return;
                if (txtMaThe.Text.Length < 14) return;
                string maDoiTuong = txtMaThe.Text.Substring(0, 3).ToUpper();
                if (cboTuyen.SelectIndex < 0) return;
                string tuyen = cboTuyen.SelectValue.ToString();
                string hinhThucVaoVien = rbtCapCuu.EditValue.ToString(); //Cấp cứu: "2" ; khám thường: "3";

                sqlQuery = "SELECT TYLE_MIENGIAM, DOITUONG_BHYT_ID FROM DMC_DOITUONG_BHYT WHERE MA_DOITUONG_BHYT = '" + maDoiTuong + "' LIMIT 1";
                LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt);
                if (dt.Rows.Count > 0)
                {
                    // Tinh mức hưởng thực tế: COM_MUC_HUONG
                    float tyLeMienGiam = float.Parse(dt.Rows[0]["TYLE_MIENGIAM"].ToString());
                    float mucHuongNoi = 0;
                    float mucHuongNgoai = 0;
                    if (tuyen.Equals("4") && !hinhThucVaoVien.Equals("2"))
                    {
                        DataTable dt1 = new DataTable();
                        sqlQuery = "SELECT TYLE_NOITRU, TYLE_NGOAITRU FROM DMC_HANG_BHTRAITUYEN WHERE HANG_BHTRAITUYEN_ID = 2 LIMIT 1";
                        LocalConst.LOCAL_SQLITE.SqliteTable_SelectSql(sqlQuery, out dt1);
                        mucHuongNoi = tyLeMienGiam * float.Parse(dt1.Rows[0]["TYLE_NOITRU"].ToString());
                        mucHuongNgoai = tyLeMienGiam * float.Parse(dt1.Rows[0]["TYLE_NGOAITRU"].ToString());
                    }
                    else
                    {
                        mucHuongNoi = tyLeMienGiam;
                        mucHuongNgoai = tyLeMienGiam;
                    }
                    hidMucHuongNgoai = mucHuongNgoai.ToString();
                }
                else
                {
                    MessageBox.Show("Mã thẻ không hợp lệ");
                    txtMaThe.Text = "";
                    txtMaThe.Focus();
                }
                
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
            }
        }

        private void chkTheTE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTheTE.Checked)
            {
                // Kiểm tra điều kiện sinh thẻ TE: <= 6 tuổi(đã bỏ?); nhập phần: ngày sinh; Huyện; Tỉnh.
                if (txtNgaySinh.DateTime == null || txtNgaySinh.Text == "")
                {
                    MessageBox.Show(Const.mess_erro_chuanhapngaysinh);
                    chkTheTE.Checked = false;
                    return;
                }
                DataRowView drvTinh = cboTinh.SelectDataRowView;
                if (drvTinh == null)
                {
                    MessageBox.Show(Const.mess_erro_chuanhaptinh);
                    chkTheTE.Checked = false;
                    return;
                }
                DataRowView drvHuyen = cboHuyen.SelectDataRowView;
                if (drvHuyen == null)
                {
                    MessageBox.Show(Const.mess_erro_chuanhaphuyen);
                    chkTheTE.Checked = false;
                    return;
                }
                // Sinh thẻ trẻ em: COM_SINHSOTHE_TREEM
                string maTinh = drvTinh["MADIAPHUONG"].ToString();
                string maHuyen = drvHuyen["MADIAPHUONG"].ToString();
                string maThe = "TE1" + maTinh + maHuyen + "00000000"; //để 8 số 0 ở cuối -> cắt chuỗi sinh thẻ đúng khi đồng bộ
                txtMaThe.Text = maThe;
                txtNgayBd.DateTime = txtNgaySinh.DateTime;
                txtNgayKt.DateTime = txtNgaySinh.DateTime.AddYears(6).AddDays(-1);
                Lay_MucHuong_BHYT();
            }
        }

        private void cboTuyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lay_MucHuong_BHYT();
        }

        private void txtMaThe_Leave(object sender, EventArgs e)
        {
            Lay_MucHuong_BHYT();
        }

        private void rbtCapCuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lay_MucHuong_BHYT();
        }
    }
}