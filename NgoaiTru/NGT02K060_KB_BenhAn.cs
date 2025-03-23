using System;
using System.Data;
using System.Windows.Forms;
using VNPT.HIS.Common;
using VNPT.HIS.Controls.SubForm;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K060_KB_BenhAn : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        public NGT02K060_KB_BenhAn()
        {
            InitializeComponent();
            Init_Form();
        }

        #region KHỞI TẠO GIÁ TRỊ
        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        ResponsList ds = new ResponsList();
        DataTable dt = new DataTable();

        //string tiepNhanID = "";
        //string loaiTiepNhan = "";
        //string hoSoBenhAnID = "";
        //string trangThaiKhamBenh = "";
        //string benhNhanID = "";
        //string khamBenhID = "";
        //string doiTuongTiepNhanID = "";
        //string phongID = "";
        #endregion

        #region LOAD DỮ LIỆU
        /// <summary>
        /// Load dữ liệu
        /// </summary>
        public void Init_Form()
        {
            DateTime dtime = Func.ParseDate(System.DateTime.Now.ToString("dd/MM/yyyy"));

            edate_TuNgay.DateTime = dtime;// Const.sys_date;
            edate_DenNgay.DateTime = dtime;

            ucgview_DSBN.gridView.OptionsView.ColumnAutoWidth = true;
            ucgview_DSBN.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
            ucgview_DSBN.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
            ucgview_DSBN.setEvent(getData_table);
            ucgview_DSBN.setEvent_FocusedRowChanged(change_selectRow);
            ucgview_DSBN.gridView.OptionsBehavior.ReadOnly = true;
            ucgview_DSBN.SetReLoadWhenFilter(true);

            DataTable dt = new DataTable();

            dt = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_TrangThai_RV002, "87");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "Tất cả";
                dt.Rows.InsertAt(dr, 0);
            }

            ecbox_DoiTuong.SelectedIndex = 0;
            ecbox_Loai.SelectedIndex = 0;
        }

        private void getData_table(object sender, EventArgs e)
        {
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
                    //if (ucgview_DSBN.ReLoadWhenFilter)
                    //{
                    //    if (ucgview_DSBN.tableFlterColumn.Rows.Count > 0)
                    //        jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSBN.tableFlterColumn);
                    //}

                    ds = RequestHTTP.get_ajaxExecuteQueryPaging("NGT02K060.EV001", page, ucgview_DSBN.ucPage1.getNumberPerPage(),
                        new string[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]" },
                        new string[] { edate_TuNgay.DateTime.ToString("dd/MM/yyyy"), edate_DenNgay.DateTime.ToString("dd/MM/yyyy"), Const.local_phongId + "", "49", "",
                            (ecbox_DoiTuong.SelectedIndex == 0 ? -1 : ecbox_DoiTuong.SelectedIndex).ToString(), (ecbox_Loai.SelectedIndex == 0 ? -1 : ecbox_Loai.SelectedIndex).ToString() }, "");
                    dt = MyJsonConvert.toDataTable(ds.rows);

                    ucgview_DSBN.clearData();
                    reset();

                    if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "TRANGTHAIKHAMBENH", "KQCLS", "SOTHUTU", "MAHOSOBENHAN", "MABENHNHAN", "TENBENHNHAN", "MA_BHYT" });

                    {
                        ucgview_DSBN.setData(dt, ds.total, ds.page, ds.records);
                        ucgview_DSBN.setColumnAll(false);

                        ucgview_DSBN.setColumn("RN", 0, " ");
                        ucgview_DSBN.setColumn("TRANGTHAIKHAMBENH", 1, " ");
                        ucgview_DSBN.setColumn("KQCLS", 2, " ");
                        ucgview_DSBN.setColumn("SOTHUTU", 3, "Số TT");
                        ucgview_DSBN.setColumn("MAHOSOBENHAN", 4, "Mã BA");
                        ucgview_DSBN.setColumn("MABENHNHAN", 5, "Mã BN");
                        ucgview_DSBN.setColumn("TENBENHNHAN", 6, "Họ tên");
                        ucgview_DSBN.setColumn("MA_BHYT", 7, "Mã BHYT");

                        ucgview_DSBN.setColumnImage("TRANGTHAIKHAMBENH", new String[] { "1", "4", "9" }
                            , new String[] { "./Resources/metacontact_away.png", "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png" });

                        ucgview_DSBN.setColumnImage("KQCLS", new String[] { "1", "2" }
                           , new String[] { "./Resources/Flag_Red_New.png", "./Resources/True.png" });

                        ucgview_DSBN.gridView.BestFitColumns(true);
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
        #endregion

        #region XỬ LÝ DỮ LIỆU
        /// <summary>
        /// Xử lý dữ liệu
        /// </summary>
        public void Reload()
        {
            btn_TimKiem_Click(null, null);
        }

        private void reset()
        {
            etext_MaVienPhi.Text = "";
            etext_MaBN.Text = "";
            etext_TongChiPhi.Text = "";
            etext_TienTamUng.Text = "";

            etext_HoTen.Text = "";
            ememo_DiaChi.Text = "";
            edate_NgaySinh.Text = "";
            etext_GioiTinh.Text = "";

            etext_BaoTinCho.Text = "";
            etext_YeuCauKham.Text = "";
            etext_DoiTuong2.Text = "";
            etext_KCBBD.Text = "";

            edate_ThoiHanBHYT.Text = "";
            etext_Tuyen.Text = "";
            etext_SoTheBHYT.Text = "";
            edate_ThoiGianTu.Text = "";

            etext_XuTri.Text = "";
            etext_PhongDangKy.Text = "";
            ememo_ChanDoanTD.Text = "";
            ememo_ChanDoanChinh.Text = "";
            ememo_ChanDoanPhu.Text = "";

            bpic_HinhAnh.Image = null;
        }

        public void HienThi_AnhChup(bool hienthi)
        {
            if (hienthi)
                layout_HinhAnh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            else
                layout_HinhAnh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
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
        #endregion

        #region SỰ KIỆN TRÊN DESIGN
        /// <summary>
        /// Sự kiện trên design
        /// </summary>
        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            if (edate_TuNgay.DateTime > edate_DenNgay.DateTime)
            {
                MessageBox.Show("Sai điều kiện tìm kiếm, từ ngày không thể lớn hơn đến ngày!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            getData_table(1, null);
        }

        private void edate_TuNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_TimKiem_Click(null, null);
            }
        }

        private void edate_DenNgay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_TimKiem_Click(null, null);
            }
        }


        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucgview_DSBN.SelectedRow;
            if (selectedBenhNhan == null)
            {
                MessageBox.Show("Hãy chọn một bệnh nhân.");
                return;
            }

            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("i_benhnhanid", "String", selectedBenhNhan["BENHNHANID"].ToString());
            table.Rows.Add("i_khambenhid", "String", selectedBenhNhan["KHAMBENHID"].ToString());
            table.Rows.Add("i_phongid", "String", Const.local_phongId.ToString());

            string rpName = "NGT020_TODIEUTRI_39BV01_QD4069_A4_" + RequestHTTP.getSysDatetime() + "." + "rtf";

            frmPrint frm = new frmPrint(rpName, "NGT020_TODIEUTRI_39BV01_QD4069_A4_965", table);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRowView selectedBenhNhan = ucgview_DSBN.SelectedRow;
            if (selectedBenhNhan == null)
            {
                MessageBox.Show("Hãy chọn một bệnh nhân.");
                return;
            }

            if (selectedBenhNhan["TRANGTHAIKHAMBENH"].ToString() == "1")
            {
                MessageBox.Show("Chỉ thao tác với bệnh nhân đang khám hoặc kết thúc khám.");
                return;
            }

            NTU01H031_NhapBenhAn frm = new NTU01H031_NhapBenhAn();             
                frm.Load_Data(
                    selectedBenhNhan["KHAMBENHID"].ToString(),
                    selectedBenhNhan["HOSOBENHANID"].ToString(),
                    selectedBenhNhan["BENHNHANID"].ToString() 
                    );
            //frm.setReturnData(ReturnData_NTU01H031_NhapBenhAn);

            openForm(frm, "1");
        }

        //private void ReturnData_NTU01H031_NhapBenhAn(object sender, EventArgs e)
        //{
        //    string loaibenhanid = (string)sender;
        //    DataRowView selectedBenhNhan = ucgview_DSBN.SelectedRow;
       
        //    if (selectedBenhNhan == null) return;


        //    DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("NT.021.LOAI.BA", loaibenhanid);
        //    // [{"LOAIBENHANID": "22","MALOAIBENHAN": "TMH02","TENLOAIBENHAN": "Bệnh án ngoại trú tai mũi họng","VERSION": null
        //    // ,"SYNC_FLAG": null,"UPDATE_FLAG": null,"MEDICAL_TYPE": "1","URL": "BAN01TMH02_NTTaiMuiHong"}]
        //    if (dt.Rows.Count == 0) return;

        //    string _sreenName = dt.Rows[0]["URL"].ToString();
        //    string _tenloaibenhan = dt.Rows[0]["TENLOAIBENHAN"].ToString();
        //    string _maloaibenhan = dt.Rows[0]["MALOAIBENHAN"].ToString();

        //    if (_sreenName != "")
        //    {
        //        // với Ngoại trú _sreenName có 4 giá trị sau: BAN01NT01_NgoaiTru     BAN01RHM02_NTRangHamMat     BAN01TMH02_NTTaiMuiHong     BAN01YHCTNT02_YHCTNgoaiTru
        //        BAN01NT01_NgoaiTru frm = new BAN01NT01_NgoaiTru(_tenloaibenhan, _sreenName);
        //        frm.Set_Data(
        //            selectedBenhNhan["KHAMBENHID"].ToString(),
        //            selectedBenhNhan["HOSOBENHANID"].ToString(),
        //            selectedBenhNhan["BENHNHANID"].ToString(),
        //            loaibenhanid,
        //            _maloaibenhan
        //            );
        //        openForm(frm, "1");
        //        // "manager.jsp?func=../benhan/" + _sreenName, paramInput, "Cập nhật " + _tenloaibenhan, 1300, 610);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không tồn tại loại bệnh án này trong dữ liệu");
        //        return;
        //    }
        //}


        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        #endregion
        // $("#hidPHONGID").val(opt.phongid);  --> là phongid của user đăng nhập
        //     var_phongdkid = rowData.PHONGID;
        //     var _khambenhid = rowData.KHAMBENHID;
        //     var _hosobenhanid = rowData.HOSOBENHANID;
        //     var _phongkhamdangkyid = rowData.PHONGKHAMDANGKYID;
        //     var _benhnhanid = rowData.BENHNHANID;
        //     var _doituongbenhnhanid = rowData.DOITUONGBENHNHANID;
        //     var _tiepnhanid = rowData.TIEPNHANID;
        //     var _loaitiepnhanid = rowData.LOAITIEPNHANID;
        //     _trangthaikhambenh = rowData.TRANGTHAIKHAMBENH;
        //     var _xutrikhambenh = rowData.XUTRIKHAMBENHID;
        //     var _sothutu = rowData.SOTHUTU;

        //$("#hidKHAMBENHID").val(_khambenhid);
        //$("#hidHOSOBENHANID").val(_hosobenhanid);
        //$("#hidPHONGKHAMDANGKYID").val(_phongkhamdangkyid);
        //$("#hidBENHNHANID").val(_benhnhanid);
        //$("#hidDOITUONGBENHNHANID").val(_doituongbenhnhanid);
        //$("#hidTIEPNHANID").val(_tiepnhanid);
        //$("#hidLOAITIEPNHANID").val(_loaitiepnhanid);
        //$('#hidXUTRIKHAMBENHID').val(_xutrikhambenh);
        //$("#hidINDEX").val(item_id);
        //$("#hidSOTHUTU").val(_sothutu);
        //$("#hidHisId").val(opt.hospital_id);
        //$("#hidUserID").val(opt.user_id);
        //$("#hidMADICHVU").val(rowData.MADICHVU);
        //$('#lblSTT').html(_sothutu);

        #region SỰ KIỆN KHỞI TẠO TRÊN CODE
        /// <summary>
        /// Sự kiện khởi tạo trên code
        /// </summary>
        private void change_selectRow(object sender, EventArgs e)
        {
            ucgview_DSBN.setEvent_FocusedRowChanged(null); // bỏ event click, chống việc click liên tiếp

            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;

            try
            {
                if (existSplash)
                    DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));

                DataRowView selectedBenhNhan = (DataRowView)sender;
                if (selectedBenhNhan != null)
                {
                    DataTable dt_TongTienDV = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.VIENPHI", selectedBenhNhan["DOITUONGBENHNHANID"].ToString(), 0);

                    if (dt_TongTienDV.Rows.Count > 0)
                    {
                        etext_TongChiPhi.Text = dt_TongTienDV.Rows[0]["TONGTIENDV"].ToString() + " đ";
                        etext_TienTamUng.Text = dt_TongTienDV.Rows[0]["TAMUNG"].ToString() + " đ";
                    }

                    DataTable dt_ChiTiet = RequestHTTP.call_ajaxCALL_SP_O("NGT02K001.EV002", selectedBenhNhan["KHAMBENHID"].ToString() + "$" + selectedBenhNhan["PHONGID"].ToString(), 0);


                    etext_MaVienPhi.Text = dt_ChiTiet.Rows[0]["MATIEPNHAN"].ToString();
                    etext_MaBN.Text = dt_ChiTiet.Rows[0]["MABENHNHAN"].ToString();

                    etext_HoTen.Text = dt_ChiTiet.Rows[0]["TENBENHNHAN"].ToString();
                    ememo_DiaChi.Text = dt_ChiTiet.Rows[0]["DIACHI"].ToString();
                    edate_NgaySinh.Text = dt_ChiTiet.Rows[0]["NGAYSINH"].ToString();
                    etext_GioiTinh.Text = dt_ChiTiet.Rows[0]["GIOITINH"].ToString();

                    etext_BaoTinCho.Text = dt_ChiTiet.Rows[0]["BAOTINCHO"].ToString();
                    etext_YeuCauKham.Text = dt_ChiTiet.Rows[0]["YEUCAUKHAM"].ToString();
                    etext_DoiTuong2.Text = dt_ChiTiet.Rows[0]["DOITUONG"].ToString();
                    etext_KCBBD.Text = dt_ChiTiet.Rows[0]["KCBBD"].ToString();

                    edate_ThoiHanBHYT.Text = dt_ChiTiet.Rows[0]["BHYTDEN"].ToString();
                    etext_Tuyen.Text = dt_ChiTiet.Rows[0]["KCBBD"].ToString();
                    etext_SoTheBHYT.Text = dt_ChiTiet.Rows[0]["SOTHEBHYT"].ToString();
                    edate_ThoiGianTu.Text = dt_ChiTiet.Rows[0]["DENKHAMLUC"].ToString();

                    etext_XuTri.Text = dt_ChiTiet.Rows[0]["XUTRI"].ToString();
                    etext_PhongDangKy.Text = dt_ChiTiet.Rows[0]["PHONGKHAM"].ToString();
                    ememo_ChanDoanTD.Text = dt_ChiTiet.Rows[0]["CDTD"].ToString();
                    ememo_ChanDoanChinh.Text = dt_ChiTiet.Rows[0]["CDC"].ToString();
                    ememo_ChanDoanPhu.Text = dt_ChiTiet.Rows[0]["CDP"].ToString();

                    try
                    {
                        if (layout_HinhAnh.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        {
                            bpic_HinhAnh.Image = Func.getIcon("nobody.png");
                            if (dt_ChiTiet.Rows[0]["ANHBENHNHAN"] != null && dt_ChiTiet.Rows[0]["ANHBENHNHAN"].ToString() != "")
                                bpic_HinhAnh.Load(dt_ChiTiet.Rows[0]["ANHBENHNHAN"].ToString());
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            finally
            {   //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                ucgview_DSBN.setEvent_FocusedRowChanged(change_selectRow);
            }
        }

        #endregion
    }
}