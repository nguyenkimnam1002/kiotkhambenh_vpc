using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.CommonForm.Class;
using VNPT.HIS.NgoaiTru.Class;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K002_KhamBenhHoiBenh : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static string SELECTED_VALUE;
        private static string SELECTED_TEXT;
        private static bool ISINITIAL;

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K002_KhamBenhHoiBenh()
        {
            InitializeComponent();
        }

        protected DataRowView BenhNhan = null;
        protected DataTable dtThongTinKhamBenh = new DataTable();
        protected bool isClose;


        public void loadData(DataRowView _drvBN)
        {
            BenhNhan = _drvBN;
            DataTable dt = BenhNhan.DataView.Table.Copy();
            string x= Newtonsoft.Json.JsonConvert.SerializeObject(dt);
        }



        protected string KhambenhID;
        protected string PhongID;
        protected string NgheNghiep;
        protected string NamSinh;
        protected string TenBenhNhan;

        protected string tudonginbangke;
        protected string tatpopupravien;
        protected DateTime dtimeNGAYTIEPNHAN;

        private void formPhieuKhamBenh_Shown(object sender, EventArgs e)
        {
        }
        private void NGT02K002_KhamBenhHoiBenh_Load(object sender, EventArgs e)
        {
            try
            {
                // Thông tin hành chính
                ucThongTinHanhChinh1.set_all_control_readonly(true);
                ucThongTinHanhChinh1.setType_KhamHoiBenh();
                //ucThongTinHanhChinh1.Height = 128; 

                ucThongTinHanhChinh1.load_benhnhan_theoMa(BenhNhan["MABENHNHAN"].ToString());

                KhambenhID = BenhNhan["KHAMBENHID"].ToString();
                PhongID = Const.local_phongId.ToString();
                NgheNghiep = ucThongTinHanhChinh1.ucNghenghiep.Text;
                NamSinh = ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["NAMSINH"].ToString();
                TenBenhNhan = ucThongTinHanhChinh1.dtTTHanhChinh.Rows[0]["TENBENHNHAN"].ToString();

                //Thông tin khám hỏi bệnh
                tabPhong.Caption = Const.local_phong;


                DataTable dt = new DataTable();

                dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
                ucCDTD.setData(dt, "ICD10CODE", "ICD10NAME");
                ucCDTD.setEvent_Enter(ucCDTD_KeyEnter);
                ucCDTD.setColumn("RN", -1, "", 0);
                ucCDTD.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucCDTD.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

                ucBenhChinh.setData(dt, "ICD10CODE", "ICD10NAME");
                ucBenhChinh.setEvent_Enter(ucBenhChinh_KeyEnter);
                ucBenhChinh.setColumn("RN", -1, "", 0);
                ucBenhChinh.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucBenhChinh.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
                //ucBenhChinh.setEvent_Check(ucBenhChinh_Check);

                ucBenhPhu.searchLookUpEdit1.EditValueChanged += EditValueUCChanged;
                ucBenhPhu.setData(dt, "ICD10CODE", "ICD10NAME");
                ucBenhPhu.setEvent_Enter(ucBenhPhu_KeyEnter);
                ucBenhPhu.setColumn("RN", -1, "", 0);
                ucBenhPhu.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
                ucBenhPhu.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
                ucBenhPhu.setEvent_Check(ucBenhPhu_Check);
                ISINITIAL = true;
                ucBenhPhu.btnReset.Visible = true;
                ucBenhPhu.btnEdit.Visible = true;
                ucBenhPhu.btnReset.Text = "Reset BP";
                ucBenhPhu.btnEdit.Text = "Sửa BP";

                ucMau1_KB.setEvent(Mau_KB_Change);
                ucMau2_HB.setEvent(Mau_HB_Change);
                btnXoaMau1.Enabled = false;

                loadMau_KhamBenh();
                ucMau1_KB.setEvent(EditValueChanged);

                load_ThongTin_KhamBenh(KhambenhID, PhongID);

                // Lấy số lượng phòng khám
                //{"func":"ajaxExecuteQueryO","params":["","NGT02K002.PHONGKHAM"],"options":[{"name":"[0]","value":"100731"},{"name":"[1]","value":"4970"}],"uuid":"7a4ac4e8-9b11-4e12-adb3-c2a94876e1de"}
                dt = RequestHTTP.get_ajaxExecuteQueryO("NGT02K002.PHONGKHAM", new string[] { "[0]", "[1]" }, new string[] { KhambenhID, PhongID });
                //[{"PHONGID": "4951","CHANDOANRAVIEN": "","MACHANDOANRAVIEN": "","CHANDOANRAVIEN_KEMTHEO": "","MACHANDOANRAVIEN_KEMTHEO": "","CHANDOANRAVIEN_KEMTHEO1": "","MACHANDOANRAVIEN_KEMTHEO1": "","CHANDOANRAVIEN_KEMTHEO2": "","MACHANDOANRAVIEN_KEMTHEO2": "","ORG_NAME": "Phòng 5: Tai Mũi Họng (K105)","KHAMBENH_TOANTHAN": "","KHAMBENH_BOPHAN": "","KHAMBENH_MACH": "","KHAMBENH_NHIETDO": "","KHAMBENH_HUYETAP_LOW": "","KHAMBENH_HUYETAP_HIGH": "","KHAMBENH_NHIPTHO": "","KHAMBENH_CANNANG": "","KHAMBENH_CHIEUCAO": "","DAXULY": "","KHAMBENH_VONGNGUC": "","KHAMBENH_VONGDAU": "","TOMTATKQCANLAMSANG": "","CHANDOANBANDAU": "","MACHANDOANBANDAU": ""},
                //{"PHONGID": "4952","CHANDOANRAVIEN": "","MACHANDOANRAVIEN": "","CHANDOANRAVIEN_KEMTHEO": "","MACHANDOANRAVIEN_KEMTHEO": "","CHANDOANRAVIEN_KEMTHEO1": "","MACHANDOANRAVIEN_KEMTHEO1": "","CHANDOANRAVIEN_KEMTHEO2": "","MACHANDOANRAVIEN_KEMTHEO2": "","ORG_NAME": "Phòng 6: Tai Mũi Họng (K106)","KHAMBENH_TOANTHAN": "","KHAMBENH_BOPHAN": "","KHAMBENH_MACH": "","KHAMBENH_NHIETDO": "","KHAMBENH_HUYETAP_LOW": "","KHAMBENH_HUYETAP_HIGH": "","KHAMBENH_NHIPTHO": "","KHAMBENH_CANNANG": "","KHAMBENH_CHIEUCAO": "","DAXULY": "","KHAMBENH_VONGNGUC": "","KHAMBENH_VONGDAU": "","TOMTATKQCANLAMSANG": "","CHANDOANBANDAU": "","MACHANDOANBANDAU": ""}]
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DevExpress.XtraBars.Navigation.TabNavigationPage tab = new DevExpress.XtraBars.Navigation.TabNavigationPage();
                    tab.Name = dt.Rows[i]["ORG_NAME"].ToString();
                    tabPane1.Controls.Add(tab);
                    tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { tab });

                    VNPT.HIS.Controls.NgoaiTru.ucTabNGT02K002_KBHB_PHONGKHAM ucTabPhong = new HIS.Controls.NgoaiTru.ucTabNGT02K002_KBHB_PHONGKHAM();
                    tab.Controls.Add(ucTabPhong);
                    ucTabPhong.Dock = DockStyle.Fill;
                    ucTabPhong.setData(dt.Rows[i]);
                }


                for (int i = 0; i < tabPane1.Pages.Count; i++)
                    tabPane1.ButtonsPanel.Buttons[i].Properties.Appearance.Font = Const.fontDefault;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void EditValueChanged(object sender, EventArgs e)
        {
            if (ucMau1_KB.SelectIndex == 0)
                btnXoaMau1.Enabled = false;
            else
                btnXoaMau1.Enabled = true;
        }

        DataTable dtThongTin = new DataTable();
        public void load_ThongTin_KhamBenh(string KHAMBENHID, string PHONGID)
        {
            try
            {
                dtThongTin = RequestHTTP.call_ajaxCALL_SP_O("NGT02K002.LAYDL", KHAMBENHID + "$" + PHONGID, 0);
                if (dtThongTin.Rows.Count > 0)
                {
                    ucCDTD.SelectedValue = dtThongTin.Rows[0]["CHANDOANTUYENDUOI"].ToString();
                    txtLYDOVAOVIEN.Text = dtThongTin.Rows[0]["LYDOVAOVIEN"].ToString();
                    txtQUATRINHBENHLY.Text = dtThongTin.Rows[0]["QUATRINHBENHLY"].ToString();
                    txtTIENSUBENH_BANTHAN.Text = dtThongTin.Rows[0]["TIENSUBENH_BANTHAN"].ToString();
                    txtTIENSUBENH_GIADINH.Text = dtThongTin.Rows[0]["TIENSUBENH_GIADINH"].ToString();

                    txtKHAMBENH_TOANTHAN.Text = dtThongTin.Rows[0]["KHAMBENH_TOANTHAN"].ToString();
                    txtKHAMBENH_MACH.Text = dtThongTin.Rows[0]["KHAMBENH_MACH"].ToString();
                    txtKHAMBENH_NHIETDO.Text = dtThongTin.Rows[0]["KHAMBENH_NHIETDO"].ToString();
                    txtKHAMBENH_HUYETAP_LOW.Text = dtThongTin.Rows[0]["KHAMBENH_HUYETAP_LOW"].ToString();
                    txtKHAMBENH_HUYETAP_HIGH.Text = dtThongTin.Rows[0]["KHAMBENH_HUYETAP_HIGH"].ToString();
                    txtKHAMBENH_BOPHAN.Text = dtThongTin.Rows[0]["KHAMBENH_BOPHAN"].ToString();
                    txtKHAMBENH_NHIPTHO.Text = dtThongTin.Rows[0]["KHAMBENH_NHIPTHO"].ToString();
                    txtKHAMBENH_CANNANG.Text = dtThongTin.Rows[0]["KHAMBENH_CANNANG"].ToString();
                    txtKHAMBENH_CHIEUCAO.Text = dtThongTin.Rows[0]["KHAMBENH_CHIEUCAO"].ToString();
                    ChiSo_BMI();

                    txtCHANDOANBANDAU.Text = dtThongTin.Rows[0]["CHANDOANBANDAU"].ToString();
                    txtTOMTATKQCANLAMSANG.Text = dtThongTin.Rows[0]["TOMTATKQCANLAMSANG"].ToString();
                    txtDAXULY.Text = dtThongTin.Rows[0]["DAXULY"].ToString();

                    ucBenhChinh.SelectedValue = dtThongTin.Rows[0]["MACHANDOANRAVIEN"].ToString();

                    ucBenhPhu.SelectedText = dtThongTin.Rows[0]["CHANDOANRAVIEN_KEMTHEO"].ToString();
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }
        private void ucBenhPhu_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;
                if (drv["ICD10CODE"].ToString() == ucBenhChinh.SelectedValue)
                {
                    ucBenhPhu.messageError = "Bệnh phụ vừa nhập không được trùng với bệnh chính.";
                }
                else if (ucBenhPhu.SelectedText.IndexOf(drv["ICD10CODE"].ToString() + "-" + drv["ICD10NAME"].ToString()) > -1)
                {
                    ucBenhPhu.messageError = "Bệnh phụ đã được nhập.";
                }
                else // check bệnh phụ trùng với phòng khám khác
                {
                    string check = RequestHTTP.checkTrungBenh(dtThongTinKhamBenh.Rows[0]["KhambenhID"].ToString(), dtThongTinKhamBenh.Rows[0]["PhongID"].ToString(), drv["ICD10CODE"].ToString());
                    if (check == "0") ucBenhPhu.messageError = "Đã tồn tại mã bệnh phụ trùng với phòng khám khác";
                }
            }
            catch (Exception ex) { log.Fatal(ex.ToString()); }
        }



        #region SỰ KIỆN PHẦN TT KHÁM BỆNH

        #endregion

        #region SỰ KIỆN enter

        //private void ucDKKCB_KeyEnter(object sender, EventArgs e)
        //{
        //    txtLichSuKham.Focus();
        //}
        //private void ucDoituong_KeyEnter(object sender, EventArgs e)
        //{
        //    txtYeuCauKhamID.Focus();
        //}
        ////private void ucYeuCauKham_KeyEnter(object sender, EventArgs e)
        ////{
        ////    //ucPhongKham.Focus();
        ////}
        private void ucCDTD_KeyEnter(object sender, EventArgs e)
        {
            ucBenhChinh.Focus();
        }
        //private void ucTuyen_KeyEnter(object sender, EventArgs e)
        //{
        //    dtimeTuNgay.Focus();
        //}
        //private void ucXuTri_KeyEnter(object sender, EventArgs e)
        //{
        //    btnHienThi.Focus();
        //}
        private void ucBenhPhu_KeyEnter(object sender, EventArgs e)
        {
            // ucXuTri.Focus();
        }
        private void ucBenhChinh_KeyEnter(object sender, EventArgs e)
        {
            //ucBenhPhu.Focus();
        }
        //private void ucToiKhoa_KeyEnter(object sender, EventArgs e)
        //{
        //    dtimeRaVien.Focus();
        //} 

        //private void dtimeDenLuc_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) ucDoituong.Focus();
        //}

        //private void txtYeuCauKhamID_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) txtYeuCauKhamName.Focus();
        //}

        //private void txtYeuCauKhamName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) txtSoThe.Focus();
        //}

        //private void txtSoThe_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) ucTuyen.Focus();
        //}

        //private void dtimeTuNgay_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) dtimeDenNgay.Focus();
        //}

        //private void dtimeDenNgay_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) ucDKKCB.Focus();
        //}

        //private void txtLichSuKham_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter) ucCDTD.Focus();
        //}

        private void btnHienThi_KeyDown(object sender, KeyEventArgs e)
        {
            // mở popup
            //openPopup();
        }
        #endregion

        private void btnLuuVaDong_Click(object sender, EventArgs e)
        {
            isClose = false;
            LUU();
            if (isClose)
                this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool bLoad_tab2 = true;
        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            if (tabPane1.SelectedPage == tabHoiBenh)
            {
                if (bLoad_tab2)
                {
                    bLoad_tab2 = false;
                    loadMau_HoiBenh();
                }
            }
        }
        private void loadMau_HoiBenh()
        {
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGT02K002.MAUHB_LST");
            ucMau2_HB.setData(dt, "col1", "col2");
            //ucMau2_HB.setEvent_Enter(ucMau1_Enter);
            ucMau2_HB.setColumn("col1", -1, "STT", 0);
        }
        private void loadMau_KhamBenh()
        {
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("NGT02K002.MAUKB_LST");

            foreach (DataRow item in dt.Rows)
            {
                if (string.IsNullOrEmpty(item["col2"].ToString()))
                    item["col2"] = "null";
            }

            if (dt.Rows.Count <= 0)
            {
                dt.Columns.Add("col1", typeof(string));
                dt.Columns.Add("col2", typeof(string));
            }

            DataRow dr = dt.NewRow();
            dr["col1"] = "-1";
            dr["col2"] = "--Chọn mẫu--";
            dt.Rows.InsertAt(dr, 0);

            ucMau1_KB.setData(dt, "col1", "col2");
            //ucMau1_KB.setEvent_Enter(ucMau2_Enter);
            ucMau1_KB.setColumn("col1", -1, "STT", 0);
            ucMau1_KB.SelectIndex = 0;
        }

        private void Mau_HB_Change(object sender, EventArgs e)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT02K002.MAUHB_SEL","1004",0],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}

            //"result": "[{\n\"LYDOVAOVIEN\": \"abc5\",\n\"QUATRINHBENHLY\": \"3454\",\n\"TIENSUBENH_BANTHAN\": \"3453\",\n\"TIENSUBENH_GIADINH\": \"34545\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
            DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K002.MAUHB_SEL", ucMau2_HB.SelectValue, 0);
            if (dt.Rows.Count > 0)
            {
                txtLYDOVAOVIEN.Text = dt.Rows[0]["LYDOVAOVIEN"].ToString();
                txtQUATRINHBENHLY.Text = dt.Rows[0]["QUATRINHBENHLY"].ToString();
                txtTIENSUBENH_BANTHAN.Text = dt.Rows[0]["TIENSUBENH_BANTHAN"].ToString();
                txtTIENSUBENH_GIADINH.Text = dt.Rows[0]["TIENSUBENH_GIADINH"].ToString();
            }
        }
        private void Mau_KB_Change(object sender, EventArgs e)
        {
            // {"func":"ajaxCALL_SP_O","params":["NGT02K002.MAUKB_SEL","12",0],"uuid":"559f28c4-31d4-44ad-8793-1da0b361f65a"}
            // "result": "[{\n\"KHAMBENH_TOANTHAN\": \"bệnh tả\",\n\"KHAMBENH_MACH\": \"10\",\n\"KHAMBENH_NHIETDO\": \"20\",\n\"KHAMBENH_HUYETAP_LOW\": \"30\",\n\"KHAMBENH_HUYETAP_HIGH\": \"40\",\n\"KHAMBENH_NHIPTHO\": \"50\",\n\"KHAMBENH_CANNANG\": \"60\",\n\"KHAMBENH_CHIEUCAO\": \"70\",\n\"KHAMBENH_BOPHAN\": \"bệnh tả\",\n\"TOMTATKQCANLAMSANG\": \"bệnh tả\",\n\"DAXULY\": \"Hướng xử lý 1\"}]",
            //"out_var": "[]",
            //"error_code": 0,
            //"error_msg": ""}
            DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("NGT02K002.MAUKB_SEL", ucMau1_KB.SelectValue, 0);
            if (dt.Rows.Count > 0)
            {
                txtKHAMBENH_TOANTHAN.Text = dt.Rows[0]["KHAMBENH_TOANTHAN"].ToString();
                txtKHAMBENH_MACH.Text = dt.Rows[0]["KHAMBENH_MACH"].ToString();
                txtKHAMBENH_NHIETDO.Text = dt.Rows[0]["KHAMBENH_NHIETDO"].ToString();
                txtKHAMBENH_HUYETAP_LOW.Text = dt.Rows[0]["KHAMBENH_HUYETAP_LOW"].ToString();
                txtKHAMBENH_HUYETAP_HIGH.Text = dt.Rows[0]["KHAMBENH_HUYETAP_HIGH"].ToString();
                txtKHAMBENH_BOPHAN.Text = dt.Rows[0]["KHAMBENH_BOPHAN"].ToString();
                txtKHAMBENH_NHIPTHO.Text = dt.Rows[0]["KHAMBENH_NHIPTHO"].ToString();
                txtKHAMBENH_CANNANG.Text = dt.Rows[0]["KHAMBENH_CANNANG"].ToString();
                txtKHAMBENH_CHIEUCAO.Text = dt.Rows[0]["KHAMBENH_CHIEUCAO"].ToString();

                txtTOMTATKQCANLAMSANG.Text = dt.Rows[0]["TOMTATKQCANLAMSANG"].ToString();
                txtDAXULY.Text = dt.Rows[0]["DAXULY"].ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            LUU();
        }
        private void LUU()
        {
            if (ucBenhChinh.SelectedIndex == -1)
            {
                ucBenhChinh.Focus();
                MessageBox.Show("Hãy nhập bệnh chính");
                return;
            }

            if (!ValidateForm()) return;

            string json_in = getJson(true, KhambenhID);

            string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K002.LUU", json_in);
            if (ret == "1")
            {
                MessageBox.Show("Cập nhật thành công");
                isClose = true;
            }
            else MessageBox.Show("Cập nhật không thành công");
        }

        private bool ValidateForm()
        {
            float value;
            if (!string.IsNullOrWhiteSpace(txtKHAMBENH_MACH.Text)
                && (!float.TryParse(txtKHAMBENH_MACH.Text, out value) || float.Parse(txtKHAMBENH_MACH.Text) < 0))
            {
                MessageBox.Show("Giá trị Mạch phải chứa giá trị là số và lớn hơn 0.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKHAMBENH_NHIETDO.Text)
                && (!float.TryParse(txtKHAMBENH_NHIETDO.Text, out value) || float.Parse(txtKHAMBENH_NHIETDO.Text) < 0))
            {
                MessageBox.Show("Giá trị Nhiệt độ phải chứa giá trị là số và lớn hơn 0.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKHAMBENH_HUYETAP_LOW.Text)
                && (!float.TryParse(txtKHAMBENH_HUYETAP_LOW.Text, out value) || float.Parse(txtKHAMBENH_HUYETAP_LOW.Text) < 0))
            {
                MessageBox.Show("Giá trị Huyết áp phải chứa giá trị là số và lớn hơn 0.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKHAMBENH_HUYETAP_HIGH.Text)
                && (!float.TryParse(txtKHAMBENH_HUYETAP_HIGH.Text, out value) || float.Parse(txtKHAMBENH_HUYETAP_HIGH.Text) < 0))
            {
                MessageBox.Show("Giá trị Huyết áp phải chứa giá trị là số và lớn hơn 0.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKHAMBENH_NHIPTHO.Text)
                && (!float.TryParse(txtKHAMBENH_NHIPTHO.Text, out value) || float.Parse(txtKHAMBENH_NHIPTHO.Text) < 0))
            {
                MessageBox.Show("Giá trị Nhịp thở phải chứa giá trị là số và lớn hơn 0.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKHAMBENH_CANNANG.Text)
                && (!float.TryParse(txtKHAMBENH_CANNANG.Text, out value) || float.Parse(txtKHAMBENH_CANNANG.Text) < 0))
            {
                MessageBox.Show("Giá trị Cân nặng phải chứa giá trị là số và lớn hơn 0.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKHAMBENH_CHIEUCAO.Text)
                && (!float.TryParse(txtKHAMBENH_CHIEUCAO.Text, out value) || float.Parse(txtKHAMBENH_CHIEUCAO.Text) < 0))
            {
                MessageBox.Show("Giá trị Chiều cao phải chứa giá trị là số và lớn hơn 0.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);
                return false;
            }
            return true;
        }

        private void btnXoaMau1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có thực sự muốn xóa", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.None);
            if (dialogResult == DialogResult.Yes)
            {
                string id = ucMau1_KB.SelectValue;
                if (id != null && id != "")
                {
                    string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K002.MAUKB_DEL", id);
                    if (ret == "1")
                    {
                        MessageBox.Show("Xóa mẫu khám bệnh thành công");
                        loadMau_KhamBenh();
                        //ucMau1_KB.clearData(ucMau1_KB.SelectIndex);
                    }
                    else MessageBox.Show("Xóa mẫu khám bệnh không thành công");
                }
            }
            
        }

        private void btnXoaMau2_Click(object sender, EventArgs e)
        {
            string id = ucMau2_HB.SelectValue;
            if (id != null && id != "")
            {
                string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K002.MAUHB_DEL", id);
                if (ret == "1")
                {
                    MessageBox.Show("Xóa mẫu hỏi bệnh thành công");
                    ucMau2_HB.clearData(ucMau2_HB.SelectIndex);
                }
                else MessageBox.Show("Xóa mẫu hỏi bệnh không thành công");
            }
        }

        private void btnLuuMau1_Click(object sender, EventArgs e)
        {
            if (txtTenMau1_KB.Text.Trim() == "")
            {
                MessageBox.Show("Tên mẫu không được để trống");
                txtTenMau1_KB.Focus();
                return;
            }

            string json_in = getJson(false, "");

            string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K002.LUUKB", json_in);
            if (ret == "1")
            {
                MessageBox.Show("Lưu mẫu thành công");
                loadMau_KhamBenh();
                ucMau1_KB.setEvent(EditValueChanged);
            }
            else MessageBox.Show("Lưu mẫu không thành công");
        }
        private void btnLuuMau2_Click(object sender, EventArgs e)
        {
            if (txtTenMau2_HB.Text.Trim() == "")
            {
                MessageBox.Show("Tên mẫu không được để trống");
                txtTenMau2_HB.Focus();
                return;
            }

            string json_in = getJson(true, "");

            string ret = RequestHTTP.call_ajaxCALL_SP_I("NGT02K002.LUUHB", json_in);
            if (ret == "1")
            {
                MessageBox.Show("Lưu mẫu thành công");
                loadMau_HoiBenh();
            }
            else MessageBox.Show("Lưu mẫu không thành công");
        }


        private string json_item(string name, string value)
        {
            return "\"" + name + "\":\"" + value + "\"" + ",";
        }

        private string getJson(bool mau_HoiBenh, string KHAMBENHID)
        {
            string json_in = "";
            json_in += json_item("MACHANDOANTUYENDUOI", ucCDTD.SelectedValue);
            json_in += json_item("CHANDOANTUYENDUOI", ucCDTD.SelectedText);
            json_in += json_item("LYDOVAOVIEN", txtLYDOVAOVIEN.Text);
            json_in += json_item("QUATRINHBENHLY", txtQUATRINHBENHLY.Text);
            json_in += json_item("TIENSUBENH_BANTHAN", txtTIENSUBENH_BANTHAN.Text);
            json_in += json_item("TIENSUBENH_GIADINH", txtTIENSUBENH_GIADINH.Text);
            json_in += json_item("KHAMBENH_TOANTHAN", txtKHAMBENH_TOANTHAN.Text);
            json_in += json_item("KHAMBENH_MACH", txtKHAMBENH_MACH.Text);
            json_in += json_item("KHAMBENH_NHIETDO", txtKHAMBENH_NHIETDO.Text);
            json_in += json_item("KHAMBENH_HUYETAP_LOW", txtKHAMBENH_HUYETAP_LOW.Text);
            json_in += json_item("KHAMBENH_HUYETAP_HIGH", txtKHAMBENH_HUYETAP_HIGH.Text);
            json_in += json_item("KHAMBENH_BOPHAN", txtKHAMBENH_BOPHAN.Text);
            json_in += json_item("KHAMBENH_NHIPTHO", txtKHAMBENH_NHIPTHO.Text);
            json_in += json_item("KHAMBENH_CANNANG", txtKHAMBENH_CANNANG.Text);
            json_in += json_item("KHAMBENH_CHIEUCAO", txtKHAMBENH_CHIEUCAO.Text);
            json_in += json_item("CHANDOANBANDAU", txtCHANDOANBANDAU.Text);
            json_in += json_item("TOMTATKQCANLAMSANG", txtTOMTATKQCANLAMSANG.Text);
            json_in += json_item("DAXULY", txtDAXULY.Text);
            json_in += json_item("MACHANDOANRAVIEN", ucBenhChinh.SelectedValue);
            json_in += json_item("CHANDOANRAVIEN", ucBenhChinh.SelectedText);
            json_in += json_item("MACHANDOANRAVIEN_KEMTHEO", "");
            json_in += json_item("CHANDOANRAVIEN_KEMTHEO", ucBenhPhu.SelectedText);
            json_in += json_item("MAU_HB", ucMau2_HB.SelectValue);
            json_in += json_item("MAU_KB", ucMau1_KB.SelectValue);
            json_in += json_item("PHONGID", Const.local_phongId + "");
            json_in += json_item("PKHAMDANGKYID", dtThongTin.Rows.Count == 0 ? "" : dtThongTin.Rows[0]["PKHAMDANGKYID"].ToString());
            json_in += json_item("KHOAID", Const.local_khoaId + "");

            if (mau_HoiBenh) json_in += json_item("TEN", txtTenMau2_HB.Text);
            else json_in += json_item("TEN_MAU_KHAMBENH", txtTenMau1_KB.Text);

            if (KHAMBENHID != "") json_in += json_item("KHAMBENHID", KHAMBENHID);

            json_in = "{" + json_in.Substring(0, json_in.Length - 1) + "}";
            json_in = json_in.Replace("\"", "\\\"");

            return json_in;
        }

        private void txtKHAMBENH_MACH_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.Equals(e.KeyChar, '.');
        }

        private void txtKHAMBENH_NHIETDO_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.Equals(e.KeyChar, '.');
        }

        private void txtKHAMBENH_HUYETAP_LOW_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.Equals(e.KeyChar, '.');
        }

        private void txtKHAMBENH_HUYETAP_HIGH_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.Equals(e.KeyChar, '.');
        }

        private void txtKHAMBENH_NHIPTHO_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.Equals(e.KeyChar, '.');
        }

        private void txtKHAMBENH_CANNANG_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.Equals(e.KeyChar, '.');
        }

        private void txtKHAMBENH_CHIEUCAO_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.Equals(e.KeyChar, '.');
        }

        private void txtKHAMBENH_CANNANG_TextChanged(object sender, EventArgs e)
        {
            ChiSo_BMI();
        }

        private void txtKHAMBENH_CHIEUCAO_TextChanged(object sender, EventArgs e)
        {
            ChiSo_BMI();
        }
        private void ChiSo_BMI()
        {
            if (txtKHAMBENH_CHIEUCAO.Text.Trim() != "" && txtKHAMBENH_CANNANG.Text.Trim() != "")
            {
                try
                {
                    float cannang = float.Parse(txtKHAMBENH_CANNANG.Text.Trim());
                    float chieucao = float.Parse(txtKHAMBENH_CHIEUCAO.Text.Trim());

                    float bmi = Func.BMI(cannang, chieucao);
                    lbBMI.Text = "MBI (kg/m2): " + bmi.ToString() + Func.BMI_Mess(bmi);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void btnKQ_Click(object sender, EventArgs e)
        {
            VNPT.HIS.CommonForm.NGT02K031_ChonKQCLS frm = new VNPT.HIS.CommonForm.NGT02K031_ChonKQCLS(KhambenhID);

            frm.setReturnData(ReturnData);
            frm.StartPosition = FormStartPosition.CenterParent;

            frm.ShowDialog();
        }
        private void ReturnData(object sender, EventArgs e)
        {
            string KQ = (string)sender;
            txtTOMTATKQCANLAMSANG.Text = KQ;
        }


        protected EventHandler event_ListenFrm_KetQua_Thuoc_ChiDinhDV;// dùng để quay lại form cha (Tiếp nhận / Khám bệnh) 
        public void setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(EventHandler event_BackParentForm)
        {
            this.event_ListenFrm_KetQua_Thuoc_ChiDinhDV = event_BackParentForm;
        }
        private void btnChiDinhDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default != null) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    this.Close();

                    NGT02K016_ChiDinhDichVu frm = new NGT02K016_ChiDinhDichVu();
                    frm.loadData("taophieuchidinhdichvu", Const.drvBenhNhan, null, "5");
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(event_ListenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void btnXuTri_Click(object sender, EventArgs e)
        {
            try
            {
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default != null) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null && Const.drvBenhNhan_ChiTiet != null)
                {
                    this.Close();

                    NGT02K005_PhieuKhamBenh frm = new NGT02K005_PhieuKhamBenh();
                    frm.loadData(Const.drvBenhNhan, Const.drvBenhNhan_ChiTiet);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }

        private void btnKetThucKham_Click(object sender, EventArgs e)
        {
            if (Const.drvBenhNhan != null)
            {
                string khambenhid = Const.drvBenhNhan["KHAMBENHID"].ToString();
                string phongkhamdangkyid = Const.drvBenhNhan["PHONGKHAMDANGKYID"].ToString();
                string json = "";
                json += Func.json_item("khambenhid", khambenhid);
                json += Func.json_item("phongkhamdangkyid", phongkhamdangkyid);
                json = Func.json_item_end(json);
                json = json.Replace("\"", "\\\"");

                string ret = RequestHTTP.call_ajaxCALL_SP_S_result("KETTHUC.KHAMPK", json);
                if (ret == "1")
                {
                    MessageBox.Show("Kết thúc khám thành công");
                    event_listenFrm_ReturnData(true, null);
                }
                else if (ret == "kocoxutri")
                {
                    MessageBox.Show("Bệnh nhân phải có xử trí mới kết thúc khám");
                }
                else
                {
                    MessageBox.Show("Kết thúc khám không thành công");
                }
            }
        }



        private void btnCapThuoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default != null) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                if (Const.drvBenhNhan != null)
                {
                    this.Close();

                    NTU02D010_CapThuoc frm = new NTU02D010_CapThuoc();
                    frm.loadData("chi_dinh_thuoc", Const.drvBenhNhan);
                    frm.setEvent_ListenFrm_KetQua_Thuoc_ChiDinhDV(event_ListenFrm_KetQua_Thuoc_ChiDinhDV);
                    openForm(frm, "1");
                }
            }
            finally
            {   //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
        private void openForm(Form frm, string optionsPopup)
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        // Bệnh phụ search lookup2
        private void EditValueUCChanged(object sender, EventArgs e)
        {
            try
            {
                if (ISINITIAL)
                {
                    SELECTED_TEXT = ucBenhPhu.SelectedText;
                    SELECTED_VALUE = ucBenhPhu.SelectedValue;
                }

                ISINITIAL = false;
                ucBenhPhu.SelectedValue = string.Empty;
                ucBenhPhu.SelectedText = SELECTED_TEXT;
                ISINITIAL = true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        protected EventHandler event_listenFrm_ReturnData;// dùng để quay lại form cha (Tiếp nhận / Khám bệnh) 
        public void SetEvent_ListenFrm_ReturnData(EventHandler listenFrm_ReturnData)
        {
            this.event_listenFrm_ReturnData = listenFrm_ReturnData;
        }
    }
}