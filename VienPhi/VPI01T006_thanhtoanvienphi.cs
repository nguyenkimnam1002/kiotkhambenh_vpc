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
using DevExpress.XtraGrid.Views.Grid;
using VNPT.HIS.Controls.SubForm;

namespace VNPT.HIS.VienPhi
{
    public partial class VPI01T006_thanhtoanvienphi : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                  log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public VPI01T006_thanhtoanvienphi()
        {
            InitializeComponent();
        }

        #region KHỞI TẠO GIÁ TRỊ
        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        string tiepNhanID = "";
        public void setParam(string tiepNhanID)
        {
            this.tiepNhanID = tiepNhanID; 
        }

        string VP_IN_PHOI_KHI_DONG_BENHAN = string.Empty;
        string VP_BS_CHUYEN_DOITUONG = string.Empty;
        string VP_CHUYEN_DOITUONG_HETHANTHE = string.Empty;
        string VPI_CHUYENHAOPHI_CONGKHAM = string.Empty;
        string VPI_CHUYENDV_THEOKHOA = string.Empty;

        string doiTuongBenhNhanID;
        string tTTN;
        string loaiTiepNhanID;
        #endregion

        #region LOAD DỮ LIỆU
        /// <summary>
        /// Load dữ liệu
        /// </summary>
        private void Init_Form()
        {
            gradio_CTTH.SelectedIndex = 0;

            VP_IN_PHOI_KHI_DONG_BENHAN = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_PHOI_KHI_DONG_BENHAN");
            VP_BS_CHUYEN_DOITUONG = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_BS_CHUYEN_DOITUONG");
            VP_CHUYEN_DOITUONG_HETHANTHE = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_CHUYEN_DOITUONG_HETHANTHE");
            VPI_CHUYENHAOPHI_CONGKHAM = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VPI_CHUYENHAOPHI_CONGKHAM");
            VPI_CHUYENDV_THEOKHOA = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VPI_CHUYENDV_THEOKHOA");
            if (VP_BS_CHUYEN_DOITUONG == "0")
                btn_ThemVienPhi.Visible = false;
            DataTable dt_NhomBHYT = RequestHTTP.get_ajaxExecuteQuery("VPI01T004.05");
            if (dt_NhomBHYT == null || dt_NhomBHYT.Rows.Count <= 0)
            {
                dt_NhomBHYT = Func.getTableEmpty(new String[] { "col1", "col2" });
            }

            DataRow dr_NhomBHYT = dt_NhomBHYT.NewRow();
            dr_NhomBHYT["col1"] = "-1";
            dr_NhomBHYT["col2"] = "--- Tất cả ---";
            dt_NhomBHYT.Rows.InsertAt(dr_NhomBHYT, 0);
            uccbox_NhomBHYT.setData(dt_NhomBHYT, 0, 1);
            uccbox_NhomBHYT.SelectIndex = 0;
            uccbox_NhomBHYT.setColumn(0, false);
            uccbox_NhomBHYT.setEvent(Load_SearchData);

            DataTable dt_Khoa = RequestHTTP.get_ajaxExecuteQuery("VPI01T004.11", new string[] { "[0]" }, new string[] { tiepNhanID });
            if (dt_Khoa == null || dt_Khoa.Rows.Count <= 0)
            {
                dt_Khoa = Func.getTableEmpty(new String[] { "col1", "col2" });
            }

            DataRow dr_Khoa = dt_Khoa.NewRow();
            dr_Khoa["col1"] = "-1";
            dr_Khoa["col2"] = "--- chọn ---";
            dt_Khoa.Rows.InsertAt(dr_Khoa, 0);
            uccbox_Khoa.setData(dt_Khoa, 0, 1);
            uccbox_Khoa.SelectValue = Const.local_khoaId + "";
            uccbox_Khoa.setColumn(0, false);
            uccbox_Khoa.setEvent(Load_SearchData);

            DataTable dt_MaTN = RequestHTTP.get_ajaxExecuteQuery("VPI01T004.06", new string[] { "[0]" }, new string[] { tiepNhanID });
            uccbox_MaTN.setData(dt_MaTN, 0, 1);
            uccbox_MaTN.SelectIndex = 0;
            uccbox_MaTN.setColumn(0, false);
            uccbox_MaTN.setEvent(Load_SearchData);

            DataTable dt_MaThe = RequestHTTP.get_ajaxExecuteQuery("VPI01T004.12", new string[] { "[0]" }, new string[] { tiepNhanID });
            uccbox_MaThe.setData(dt_MaThe, 0, 1);
            uccbox_MaThe.SelectIndex = 0;
            uccbox_MaThe.setColumn(0, false);
            uccbox_MaThe.setEvent(Load_SearchData);
        }

        private void Load_TiepNhan()
        {
            DataTable dt_TiepNhan = RequestHTTP.call_ajaxCALL_SP_O("VPI01T004.01", tiepNhanID + "$" + uccbox_MaThe.SelectValue, 0);
            etext_MaBN.Text = dt_TiepNhan.Rows[0]["MABN"].ToString();
            etext_TenBN.Text = dt_TiepNhan.Rows[0]["TENBN"].ToString();
            etext_NamSinh.Text = dt_TiepNhan.Rows[0]["NAMSINH"].ToString();
            etext_Tuoi.Text = dt_TiepNhan.Rows[0]["TUOI"].ToString();
            etext_DoiTuongBN.Text = dt_TiepNhan.Rows[0]["DOITUONGBN"].ToString();
            etext_SoThe.Text = dt_TiepNhan.Rows[0]["SOTHE"].ToString();
            edate_GTTheTu.Text = dt_TiepNhan.Rows[0]["GIATRITHETU"].ToString();
            edate_GTTheDen.Text = dt_TiepNhan.Rows[0]["GIATRITHEDEN"].ToString();

            doiTuongBenhNhanID = dt_TiepNhan.Rows[0]["DOITUONGBENHNHANID"].ToString();

            tTTN = dt_TiepNhan.Rows[0]["TRANGTHAITIEPNHAN"].ToString();
            loaiTiepNhanID = dt_TiepNhan.Rows[0]["LOAITIEPNHANID"].ToString();

            if (dt_TiepNhan != null && dt_TiepNhan.Rows.Count > 0)
            {
                if (tTTN == "0")
                {
                    if (VP_BS_CHUYEN_DOITUONG == "0")
                        btn_SuaTTBH.Visible = false;
                    else
                        btn_SuaTTBH.Visible = true;
                    if (VP_IN_PHOI_KHI_DONG_BENHAN == "0")
                    {
                        if (loaiTiepNhanID == "0")
                        {
                            btn_InPhoi.Enabled = true;
                            echeck_XemTruoc.Checked = true;
                        }
                        else
                        {
                            btn_InPhoi.Enabled = false;
                            echeck_XemTruoc.Checked = false;
                        }
                    }
                    else if (VP_IN_PHOI_KHI_DONG_BENHAN == "1")
                    {
                        btn_InPhoi.Enabled = true;
                        echeck_XemTruoc.Checked = true;
                    }
                    else if (VP_IN_PHOI_KHI_DONG_BENHAN == "2")
                    {
                        btn_InPhoi.Enabled = false;
                        echeck_XemTruoc.Checked = false;
                    }
                }
                else
                {
                    btn_SuaTTBH.Visible = false;
                    btn_InPhoi.Enabled = true;
                    echeck_XemTruoc.Checked = true;
                }
            }
        }

        private void Load_VienPhi()
        {
            DataTable dt_VienPhi = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.05", tiepNhanID, 0);
            DataTable dt_ThanhToan = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.06", tiepNhanID, 0);

            label_TongChiPhi.Text = String.Format("{0:0,0.00}", double.Parse(dt_VienPhi.Rows[0]["TONGTIENDV"].ToString())) + " đ";
            label_BHTra.Text = String.Format("{0:0,0.00}", double.Parse(dt_VienPhi.Rows[0]["BHYT_THANHTOAN"].ToString())) + " đ";
            label_BNTra.Text = String.Format("{0:0,0.00}", double.Parse(dt_VienPhi.Rows[0]["TONGTIENDV"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["BHYT_THANHTOAN"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["MIENGIAMDV"].ToString())) + " đ";
            label_TamUng.Text = String.Format("{0:0,0.00}", double.Parse(dt_ThanhToan.Rows[0]["TAMUNG"].ToString()) - double.Parse(dt_ThanhToan.Rows[0]["HOANUNG"].ToString())) + " đ";
            label_MienGiam.Text = String.Format("{0:0,0.00}", double.Parse(dt_ThanhToan.Rows[0]["MIENGIAMDV"].ToString()) + double.Parse(dt_ThanhToan.Rows[0]["MIENGIAM"].ToString())) + " đ";
            label_DaThanhToan.Text = String.Format("{0:0,0.00}", double.Parse(dt_ThanhToan.Rows[0]["DANOP"].ToString())) + " đ";
            label_ChenhLech.Text = String.Format("{0:0,0.00}", double.Parse(dt_ThanhToan.Rows[0]["TAMUNG"].ToString()) - (double.Parse(dt_VienPhi.Rows[0]["TONGTIENDV"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["BHYT_THANHTOAN"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["MIENGIAMDV"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["DANOP"].ToString()))) + " đ";
            label_ThanhToanThem.Text = String.Format("{0:0,0.00}", double.Parse(dt_VienPhi.Rows[0]["TONGTIENDV"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["BHYT_THANHTOAN"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["MIENGIAMDV"].ToString()) - double.Parse(dt_VienPhi.Rows[0]["DANOP"].ToString())) + " đ";
        }

        private void Load_DSCT()
        {
            try
            {
                ucgview_DSCT.gridView.OptionsView.ColumnAutoWidth = false;
                ucgview_DSCT.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucgview_DSCT.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucgview_DSCT.gridView.OptionsBehavior.Editable = false;
                ucgview_DSCT.setMultiSelectMode(true);

                ucgview_DSCT.setEvent(ucgview_DSCT_Load);
                ucgview_DSCT.SetReLoadWhenFilter(true);
                ucgview_DSCT.gridView.Click += ucgview_DSCT_Click;
                //ucgview_DSCT.gridView.ColumnFilterChanged += ucgview_DSCT_ColumnFilterChanged;

                ucgview_DSCT.setNumberPerPage(new int[] { 200, 300 });
                ucgview_DSCT.onIndicator();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Load_DSTH()
        {
            try
            {
                ucgview_DSTH.gridView.OptionsView.ColumnAutoWidth = true;
                ucgview_DSTH.gridView.OptionsView.ShowGroupPanel = false;//hiển thị phần header
                ucgview_DSTH.gridView.OptionsView.ShowViewCaption = false;// Hiển thị Tiêu đề của grid
                ucgview_DSTH.gridView.OptionsBehavior.Editable = false;
                ucgview_DSTH.gridView.GroupFormat = "[#image]{1} {2}";

                ucgview_DSTH.setEvent(ucgview_DSTH_Load);
                ucgview_DSTH.SetReLoadWhenFilter(true);
                ucgview_DSTH.gridView.Click += ucgview_DSTH_Click;
                //ucgview_DSTH.gridView.ColumnFilterChanged += ucgview_DSTH_ColumnFilterChanged;
                ucgview_DSTH.setNumberPerPage(new int[] { 20, 30, 50, 100, 200 });
                ucgview_DSTH.onIndicator();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void Load_DataGridCT(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                ResponsList responses = new ResponsList();

                //string jsonFilter = "";
                //if (ucgview_DSCT.ReLoadWhenFilter && ucgview_DSCT.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSCT.tableFlterColumn);
                //}

                DataTable dt_DSCT = new DataTable();
                uccbox_Khoa.Enabled = true;

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                    "VPI01T004.04", page, ucgview_DSCT.ucPage1.getNumberPerPage(),
                    new string[] { "[0]", "[0]", "[0]", "[0]", "[0]", "[0]", "[0]" },
                    new string[] { tiepNhanID, uccbox_NhomBHYT.SelectValue, etext_TTTTVienPhi.Text, uccbox_Khoa.SelectValue, "-1", "-1", uccbox_MaThe.SelectValue }
                    , ucgview_DSCT.jsonFilter());

                ucgview_DSCT.clearData();

                dt_DSCT = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSCT.Rows.Count == 0)
                    dt_DSCT = Func.getTableEmpty(new String[] { "NGAYDICHVU", "SOPHIEU", "MADICHVU", "TENNHOM", "TENDICHVU", "SOLUONG", "DONVI",
                            "TIENDICHVU", "THANHTIEN", "TIEN_BHYT_TRA", "TIEN_MIENGIAM", "TYLE", "LOAITTMOI", "KHOA", "PHONG", "NGUOICHIDINH",
                        "LOAIDOITUONG", "TRANGTHAIDICHVU", "LOAINHOMMAUBENHPHAM", "TYLE_BHYT_TRA", "TYLE_DV", "TIEN_DICHVU" });

                ucgview_DSCT.setData(dt_DSCT, responses.total, responses.page, responses.records);
                ucgview_DSCT.setColumnAll(false);

                ucgview_DSCT.setColumn("NGAYDICHVU", 1, "Ngày", 100);
                ucgview_DSCT.setColumn("SOPHIEU", 2, "Số phiếu", 100);
                ucgview_DSCT.setColumn("MADICHVU", 3, "Mã dịch vụ", 100);
                ucgview_DSCT.setColumn("TENNHOM", 4, "Nhóm BHYT", 100);
                ucgview_DSCT.setColumn("TENDICHVU", 5, "Tên thuốc + dịch vụ", 100);
                ucgview_DSCT.setColumn("SOLUONG", 6, "SL", 100);
                ucgview_DSCT.setColumn("DONVI", 7, "Đơn vị", 100);
                ucgview_DSCT.setColumn("TIENDICHVU", 8, "Giá tiền", 100);
                ucgview_DSCT.setColumn("THANHTIEN", 9, "Thành tiền", 100);
                ucgview_DSCT.setColumn("TIEN_BHYT_TRA", 10, "BHYT trả", 100);
                ucgview_DSCT.setColumn("TIEN_MIENGIAM", 11, "Miễn giảm", 100);
                ucgview_DSCT.setColumn("TYLE", 12, "Tỷ lệ TT", 100);
                ucgview_DSCT.setColumn("LOAITTMOI", 13, "Loại TT", 100);
                ucgview_DSCT.setColumn("KHOA", 14, "Khoa", 200);
                ucgview_DSCT.setColumn("PHONG", 15, "Phòng", 200);
                ucgview_DSCT.setColumn("NGUOICHIDINH", 16, "Người chỉ định", 100);

                ucgview_DSCT.gridView.BestFitColumns(true);

                for (int i = 0; i < dt_DSCT.Rows.Count; i++)
                {
                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "1" || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "2" || 
                        dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "3")
                        echeck_BHYT.Checked = true;
                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "4" || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "11" || 
                        dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "6" || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "5")
                        echeck_ThuPhi.Checked = true;
                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "7" || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "8" || 
                        dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "9")
                        echeck_HaoPhi.Checked = true;
                    if (dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "5")
                        echeck_DiKem.Checked = true;

                    if (dt_DSCT.Rows[0]["TRANGTHAIDICHVU"].ToString() == "3")
                        ucgview_DSCT.gridView.RowStyle += ucgview_DSCT_RowStyleBlue;

                    if ((dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "1" || dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "2" || 
                        dt_DSCT.Rows[0]["LOAIDOITUONG"].ToString() == "3") && dt_DSCT.Rows[0]["LOAINHOMMAUBENHPHAM"].ToString() != "16" 
                        && dt_DSCT.Rows[0]["TYLE"].ToString() != dt_DSCT.Rows[0]["TYLE_BHYT_TRA"].ToString() && 
                        dt_DSCT.Rows[0]["TYLE_DV"].ToString() != "0" && dt_DSCT.Rows[0]["SOLUONG"].ToString() != "0" 
                        && (dt_DSCT.Rows[0]["LOAINHOMMAUBENHPHAM"].ToString() != "3" || dt_DSCT.Rows[0]["TIEN_DICHVU"].ToString() != "0"))
                        ucgview_DSCT.gridView.RowStyle += ucgview_DSCT_RowStyleRed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }

        private void Load_DataGridTH(int page)
        {
            try
            {
                if (page <= 0)
                {
                    page = 1;
                }

                ResponsList responses = new ResponsList();

                //string jsonFilter = "";
                //if (ucgview_DSTH.ReLoadWhenFilter && ucgview_DSTH.tableFlterColumn.Rows.Count > 0)
                //{
                //    jsonFilter = Newtonsoft.Json.JsonConvert.SerializeObject(ucgview_DSTH.tableFlterColumn);
                //}

                DataTable dt_DSTH = new DataTable();
                uccbox_Khoa.Enabled = true;

                responses = RequestHTTP.get_ajaxExecuteQueryPaging(
                        "VPI01T001.02", page, ucgview_DSTH.ucPage1.getNumberPerPage(),
                        new string[] { "[0]", "[0]", "[0]" },
                        new string[] { tiepNhanID, uccbox_NhomBHYT.SelectValue, etext_TTTTVienPhi.Text }, ucgview_DSTH.jsonFilter());

                ucgview_DSTH.clearData();

                dt_DSTH = MyJsonConvert.toDataTable(responses.rows);

                if (dt_DSTH.Rows.Count == 0)
                    dt_DSTH = Func.getTableEmpty(new String[] { "DOITUONG", "NHOM_MABHYT", "TENDICHVU", "DVT", "SOLUONG", "TIENDICHVU", "KHOA", "PHONG", "LOAIDOITUONG" });

                ucgview_DSTH.setData(dt_DSTH, responses.total, responses.page, responses.records);
                ucgview_DSTH.setColumnAll(false);

                ucgview_DSTH.setColumn("TENDICHVU", 1, "Tên thuốc - Dịch vụ");
                ucgview_DSTH.setColumn("DVT", 2, "Đơn vị");
                ucgview_DSTH.setColumn("SOLUONG", 3, "SL");
                ucgview_DSTH.setColumn("TIENDICHVU", 4, "Giá tiền");
                ucgview_DSTH.setColumn("KHOA", 5, "Khoa");
                ucgview_DSTH.setColumn("PHONG", 6, "Phòng");

                ucgview_DSTH.gridView.Columns["DOITUONG"].GroupIndex = 0;
                ucgview_DSTH.gridView.Columns["NHOM_MABHYT"].GroupIndex = 1;
                ucgview_DSTH.gridView.ExpandGroupLevel(0);
                ucgview_DSTH.gridView.ExpandGroupLevel(1);


                ucgview_DSTH.gridView.BestFitColumns(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + ". " + ex.TargetSite.ToString());
                log.Fatal(ex.ToString());
            }
        }
        #endregion

        #region XỬ LÝ DỮ LIỆU
        /// <summary>
        /// Xử lý dữ liệu
        /// </summary>
        private void tinhTienVienPhi(string _tiepNhanID, string _dTBNID, string _loaiTiepNhanID)
        {
            string opt = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_TACH_BANGKE");
            string IN_BK_VP = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "c");
            string IN_GOP_BKNTRU = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VPI_GOP_BANGKENTRU");
            if (opt == "1")
            {
                string flag = RequestHTTP.call_ajaxCALL_SP_I("VPI01T004.10", _tiepNhanID);
                if (_loaiTiepNhanID == "0")
                {
                    if (IN_GOP_BKNTRU == "1")
                    {
                        //this.inPhoiVP('1', _tiepnhanid, 'NTU001_BKCPKCBBHYTNOITRU_02BV_QD3455_A4');
                    }
                    else
                    {
                        if (_dTBNID == "1")
                        {
                            RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                            inPhoi("1", tiepNhanID, "NTU001_BKCPKCBBHYTNOITRU_02BV_QD3455_A4");
                            if (IN_BK_VP == "0" && flag == "1")
                                inPhoi("1", tiepNhanID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                        }
                        else
                        {
                            if (flag == "1")
                                inPhoi("1", tiepNhanID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                        }
                    }
                }
                else
                {
                    if (_dTBNID == "1")
                    {
                        RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                        inPhoi("1", tiepNhanID, "NGT001_BKCPKCBBHYTNGOAITRU_01BV_QD3455_A4");
                        if (IN_BK_VP == "0" && flag == "1")
                            inPhoi("1", tiepNhanID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                    }
                    else
                    {
                        if (flag == "1")
                            inPhoi("1", tiepNhanID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                    }
                }
            }
            else
            {
                RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                if (_loaiTiepNhanID == "0")
                {
                    inPhoi("1", tiepNhanID, "NTU001_BKCPKCBNOITRU_02BV_QD3455_A4");
                }
                else
                {
                    inPhoi("1", tiepNhanID, "NGT001_BKCPKCBNGOAITRU_01BV_QD3455_A4");
                }
            }
            // in bang ke hao phi neu co
            string flag_HaoPhi = RequestHTTP.call_ajaxCALL_SP_I("VPI01T005.11", _tiepNhanID);
            string opt_HaoPhi = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_BANGKE_HAOPHI");
            if (opt_HaoPhi == "1")
            {
                if (flag_HaoPhi == "1")
                {
                    inPhoi("1", tiepNhanID, "NGT001_BKCPKCB_HAOPHI_01BV_QD3455_A4");
                }
            }
        }

        private void inPhoi(string _inBangKeChuan, string _tiepNhanID, string _report_code)
        {
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("inbangkechuan", "String", _inBangKeChuan);
            table.Rows.Add("tiepnhanid", "String", _tiepNhanID);

            frmPrint frm = new frmPrint(table, _report_code, "pdf", 700, 1000);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void Load_SearchData(object sender, EventArgs e)
        {
            Load_DataGridCT(1);
            Load_DataGridTH(1);
        }

        private void ucgview_DSCT_RowStyleBlue(object sender, RowStyleEventArgs e)
        {
            for (int i = 0; i < ucgview_DSCT.gridView.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)ucgview_DSCT.gridView.GetRow(i);
                e.Appearance.ForeColor = Color.Blue;
                e.HighPriority = true;
            }
        }

        private void ucgview_DSCT_RowStyleRed(object sender, RowStyleEventArgs e)
        {
            for (int i = 0; i < ucgview_DSCT.gridView.DataRowCount; i++)
            {
                DataRowView rowData = (DataRowView)ucgview_DSCT.gridView.GetRow(i);
                e.Appearance.ForeColor = Color.Red;
                e.HighPriority = true;
            }
        }
        #endregion

        #region SỰ KIỆN TRÊN DESIGN
        /// <summary>
        /// Sự kiện trên design
        /// </summary>
        private void VPI01T006_thanhtoanvienphi_Load(object sender, EventArgs e)
        {
            Init_Form();
            Load_TiepNhan();
            Load_VienPhi();

            Load_DSCT();
            Load_DSTH();
        }

        private void gradio_CTTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gradio_CTTH.SelectedIndex == 0)
            {
                uccbox_Khoa.Enabled = true;
                layout_DSCT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layout_DSTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            else
            {
                uccbox_Khoa.Enabled = false;
                layout_DSCT.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layout_DSTH.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            } 
        }

        private void etext_TTTTVienPhi_EditValueChanged(object sender, EventArgs e)
        {
            Load_DataGridCT(1);
            Load_DataGridTH(1);
        }

        private void btn_SuaTTBH_Click(object sender, EventArgs e)
        {
            if (VP_BS_CHUYEN_DOITUONG == "1")
            {
               
            }
            else
            {
                MessageBox.Show("Không cho phép sửa thông tin hành chính tại đây");
            }
        }

        private void btn_ThemVienPhi_Click(object sender, EventArgs e)
        {
            DialogResult result = XtraMessageBox.Show(this, "Bạn có muốn xóa chi tiết lời dặn này?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                string ret = RequestHTTP.call_ajaxCALL_SP_I("VPI01T004.07", tiepNhanID);
                if (int.Parse(ret) > 1)
                {
                    tiepNhanID = ret;
                    
                    Load_DataGridCT(1);
                    Load_DataGridTH(1);
                }
                else MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void btn_KiemTraVP_Click(object sender, EventArgs e)
        {

        }

        private void btn_InPhoi_Click(object sender, EventArgs e)
        {
            if (VP_IN_PHOI_KHI_DONG_BENHAN == "1" || tTTN == "1" || tTTN == "2" || (VP_IN_PHOI_KHI_DONG_BENHAN == "0" && loaiTiepNhanID == "0"))
            {
                tinhTienVienPhi(tiepNhanID, doiTuongBenhNhanID, loaiTiepNhanID);
            }
            else
            {
                MessageBox.Show("Chưa kết thúc khám bệnh/đóng bệnh án, không thể in bảng kê");
            }
        }

        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region SỰ KIỆN KHỞI TẠO CỦA GRIDVIEW
        /// <summary>
        /// Sự kiện khởi tạo của gridview
        /// </summary>
        private void ucgview_DSCT_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGridCT(pageNum);
        }

        private void ucgview_DSCT_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucgview_DSCT.gridView.FocusedColumn.FieldName))
            {
                int id = ucgview_DSCT.gridView.FocusedRowHandle;
                if (ucgview_DSCT.gridView.GetSelectedRows().Any(o => o == id))
                {
                    ucgview_DSCT.gridView.UnselectRow(id);
                }
                else
                {
                    ucgview_DSCT.gridView.SelectRow(id);
                }
            }
        }

        //private void ucgview_DSCT_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridView view = sender as GridView;
        //        if (view.ActiveEditor is TextEdit)
        //        {
        //            TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //            textEdit.Text = textEdit.Text.Trim();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }
        //}

        private void ucgview_DSTH_Load(object sender, EventArgs e)
        {
            int pageNum = sender != null ? (int)sender : 1;
            Load_DataGridTH(pageNum);
        }

        private void ucgview_DSTH_Click(object sender, EventArgs e)
        {
            if (!"DX$CheckboxSelectorColumn".Equals(ucgview_DSTH.gridView.FocusedColumn.FieldName))
            {
                int id = ucgview_DSTH.gridView.FocusedRowHandle;
                if (ucgview_DSTH.gridView.GetSelectedRows().Any(o => o == id))
                {
                    ucgview_DSTH.gridView.UnselectRow(id);
                }
                else
                {
                    ucgview_DSTH.gridView.SelectRow(id);
                }
            }
        }

        //private void ucgview_DSTH_ColumnFilterChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridView view = sender as GridView;
        //        if (view.ActiveEditor is TextEdit)
        //        {
        //            TextEdit textEdit = (TextEdit)view.ActiveEditor;
        //            textEdit.Text = textEdit.Text.Trim();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Fatal(ex.ToString());
        //    }
        //}
        #endregion
    }
}