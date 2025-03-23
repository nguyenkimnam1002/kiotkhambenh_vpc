using System;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.NgoaiTru
{
    public partial class BAN01NT01_NgoaiTru : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                  log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        string title = string.Empty;
        public BAN01NT01_NgoaiTru(string title, string _sreenName)
        {
            InitializeComponent();

            this.Text = "Cập nhật " + title;
            this.title = title;
            // với Ngoại trú _sreenName có 4 giá trị sau: 
            // BAN01NT01_NgoaiTru     BAN01RHM02_NTRangHamMat     BAN01TMH02_NTTaiMuiHong     BAN01YHCTNT02_YHCTNgoaiTru
            // răng hàm mặt
            if (_sreenName == "BAN01RHM02_NTRangHamMat")
            {
                layout_NT_CacBoPhan.Text = "2. Bệnh chuyên khoa:";
                layout_NT_TomTatKQCLS.Text = "3. Tóm tắt bệnh án:";
                layout_NT_ChanDoanBanDau.Text = "4. Chẩn đoán của khoa khám bệnh:";
                layout_NT_DaXuLy.Text = "5. Đã xử lý của tuyến dưới:";
                layout_NT_ChanDoanRaVien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                btn_CopyBA.Location = btn_LuuBAMau.Location;
                btn_LuuBAMau.Visible = false;
                btn_XoaBAMau.Visible = false;
            }
            else if (_sreenName == "BAN01TMH02_NTTaiMuiHong")
            {
                layout_NT_CacBoPhan.Text = "2. Khám chuyên khoa:";
                layout_NT_TomTatKQCLS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layout_NT_ChanDoanBanDau.Text = "4. Chẩn đoán của phòng khám:";
                layout_NT_ChanDoanRaVien.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layout_NT_DaXuLy.Text = "IV. Đã xử lý:";
                layout_NT_DieuTriNgoaiTru.Text = "+ Điều trị ngoại trú";

                layout_NT_QTBLvaDBCLS.Text = "1. Tình trạng toàn thân:";
                layout_NT_TomTatKQN.Text = "2. Tại chỗ:";
                layout_NT_PPDieuTri.Text = "4. Tiên lượng bệnh:";
                layout_NT_TinhTrangNguoiBenh.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layout_NT_HuongDanDieuTri.Text = "VI. NHỮNG CHỈ DẪN ĐIỀU TRỊ TIẾP TỤC:";
            }
        }

        string khambenhid = string.Empty;
        string hosobenhanid = string.Empty;
        string benhnhanid = string.Empty;
        string loaibenhanid = string.Empty;
        string maloaibenhan = string.Empty; // chính = loaihsba_code
        string i_hid = Const.local_user.HOSPITAL_ID;

        public void Set_Data(string khambenhid, string hosobenhanid, string benhnhanid, string loaibenhanid, string maloaibenhan)
        {
            this.khambenhid = khambenhid;
            this.hosobenhanid = hosobenhanid;
            this.benhnhanid = benhnhanid;
            this.loaibenhanid = loaibenhanid;
            this.maloaibenhan = maloaibenhan;
        }

        private int checkInsert = 1;
        private DataTable arrMaHoiKham = new DataTable();

        private void BAN01NT01_NgoaiTru_Load(object sender, EventArgs e)
        {
            _initControl();

            _bindEvent();

            // Ko biết làm gì?
            if (hosobenhanid != null && hosobenhanid != "0")
            {
                //exportBan(hosobenhanid, loaibenhanid, benhnhanid, _opts._param[4], 'BAN003_NGOAITRU_QD4069_A4');
            }
        }
        private void load_cboMAUBAID()
        {
            cboMAUBAID.setEvent(comboBenhAnMau_Change);
            //{"func":"ajaxExecuteQuery","params":["","HSBA.MAU"],"options":[{"name":"[0]","value":"20"}],"uuid":"0674a428-9633-42b0-88e2-2518f280c95b"}
            //[["118380","AA"]]
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery("HSBA.MAU", new string[] { loaibenhanid });
            //if (dt.Rows.Count > 0)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr[0] = "";
            //    dr[1] = "-- Chọn --";
            //    dt.Rows.InsertAt(dr, 0);
            //}
            cboMAUBAID.setData(dt, 0, 1);
            cboMAUBAID.setColumnAll(false);
            cboMAUBAID.setColumn(1, true);
            cboMAUBAID.SelectIndex = 0;
        }
        private void _initControl()
        {
            txtLYDOVAOVIEN.Focus();

            load_cboMAUBAID();

            DataTable dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
            txtMACDBANDAU.setData(dt, "ICD10CODE", "ICD10NAME");
            //ucChanDoanBanDau.setEvent_Enter(ucCDTD_KeyEnter);
            txtMACDBANDAU.setColumn("RN", -1, "", 0);
            txtMACDBANDAU.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            txtMACDBANDAU.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            txtMACHANDOANRAVIEN.setData(dt, "ICD10CODE", "ICD10NAME");
            //ucChanDoanRaVien.setEvent_Enter(ucBenhChinh_KeyEnter);
            txtMACHANDOANRAVIEN.setColumn("RN", -1, "", 0);
            txtMACHANDOANRAVIEN.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            txtMACHANDOANRAVIEN.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            txtMABENHCHINH.setData(dt, "ICD10CODE", "ICD10NAME");
            //ucBenhChinh.setEvent_Enter(ucBenhChinh_KeyEnter);
            txtMABENHCHINH.setColumn("RN", -1, "", 0);
            txtMABENHCHINH.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            txtMABENHCHINH.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            txtBENHKEMTHEO.setData(dt, "ICD10CODE", "ICD10NAME");
            //ucBenhKemTheo.setEvent_Enter(ucBenhChinh_KeyEnter);
            txtBENHKEMTHEO.setColumn("RN", -1, "", 0);
            txtBENHKEMTHEO.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            txtBENHKEMTHEO.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            // Check xem da co du lieu ho so benh an khong
            loadData(benhnhanid, loaibenhanid, maloaibenhan, hosobenhanid, khambenhid);

            if (!string.IsNullOrEmpty(hosobenhanid))
            {
                checkInsert = 0;
            }

            // Check cấu hình để hiển thị/ ẩn phần Ghi chú của bệnh --> tạm bỏ qua
            //         var NGT_GHICHU_BENHCHINH = jsonrpc.AjaxJson.ajaxCALL_SP_I('COM.CAUHINH.THEOMA', 'NGT_GHICHU_BENHCHINH');
            //         if (NGT_GHICHU_BENHCHINH == '1')
            //         {
            //$("#divBc").addClass("col-md-6"); // cho ô bệnh ngắn lại
            //$('#divSuaBc').css('display', ''); // hiển thị thê ô Ghi chú
            //         }

            // lấy danh sách mã hỏi khám bệnh của khoa
            arrMaHoiKham = RequestHTTP.get_ajaxExecuteQueryO("BAN.02", loaibenhanid);


            // làm gì đây?
            //      var f2 = 113;
            //$(document).unbind('keydown').keydown(function(e) {
            //          if (e.keyCode == f2)
            //          {
            //              getIcd(e.target);
            //          }
            //      });

            //      EventUtil.setEvent("assignSevice_resultTK", function(e) {
            //          if (e.mode == '0')
            //          {
            //$('#' + e.ctrId).combogrid("setValue", e.text);
            //          }
            //          else if (e.mode == '1')
            //          {
            //$('#' + e.ctrTargetId).val($('#' + e.ctrTargetId).val() == '' ? "" + e.text : $('#' + e.ctrTargetId).val() + ";" + e.text);
            //          }
            //          DlgUtil.close(e.popupId);
            //      });
        }
        private void _bindEvent()
        {

        }
        private void loadData(string benhnhanid, string loaibenhanid, string loaihsba_code, string hosobenhanid, string khambenhid)
        {
            string i_hid = Const.local_user.HOSPITAL_ID;
            //widget thong tin hanh chinh
            ntU02D023_Thongtinhanhchinh1.load_data(khambenhid, "", "", title); // hosobenhanid, phongid truyền vào rỗng

            // Check xem da co du lieu ho so benh an khong
            if (hosobenhanid == null || hosobenhanid == "")
            {
                DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("BAN.01", new string[] { benhnhanid, loaibenhanid, i_hid });
                if (dt.Rows.Count > 0)
                {
                    hosobenhanid = dt.Rows[0]["HOSOBENHANID"].ToString().Trim();
                }
            }

            if (hosobenhanid != null && hosobenhanid != "")
            {
                // b1: Lay thong tin hoi kham benh va tong ket benh an
                setValueOnLoad("BAN.04", new string[] { hosobenhanid }, loaihsba_code);
                // b2: Lay thong tin hoi kham benh dang table(dang ma tran)
                setValueOnLoad("BAN.05", new string[] { hosobenhanid }, loaihsba_code);
                // b3: Lay thong tin ho so phim anh
                setValueOnLoad("BAN.06", new string[] { hosobenhanid }, loaihsba_code);

                // dành cho nội trú
                //fillDataMatrix();
            }
        }
        private void loadDataTemp(string benhnhanid, string loaibenhanid, string loaihsba_code, string hosobenhanid, string khambenhid)
        {
            ntU02D023_Thongtinhanhchinh1.load_data(khambenhid, "", "", title); // hosobenhanid, phongid truyền vào rỗng

            setValueOnLoad("BAN.04_MAU", new string[] { hosobenhanid, loaibenhanid }, loaihsba_code);
            // b2: Lay thong tin hoi kham benh dang table(dang ma tran)
            setValueOnLoad("BAN.05_MAU", new string[] { hosobenhanid }, loaihsba_code);
            // b3: Lay thong tin ho so phim anh
            setValueOnLoad("BAN.06_MAU", new string[] { hosobenhanid }, loaihsba_code);

            // dành cho nội trú
            //fillDataMatrix();
        }
        //set gia tri vao cac control input khi khoi tao trang
        private void setValueOnLoad(string _sql, string[] sql_par, string loaihsba_code)
        {
            DataTable dt = RequestHTTP.get_ajaxExecuteQuery(_sql, sql_par);
            if (dt.Rows.Count <= 0) return;

            // duyet va fill du lieu vao cac input hoi, kham benh va tong ket benh an 
            for (int k = 0; k < dt.Rows.Count; k++)
            {
                string noidung = dt.Rows[k][0].ToString();
                string maTieuDe = dt.Rows[k][1].ToString();
                string kieuDuLieu = dt.Rows[k][2].ToString();

                // xac dinh id cua input tren form
                if (kieuDuLieu == "1" || kieuDuLieu == "4") // textedit, memmoedit, uc
                {
                    maTieuDe = "txt" + maTieuDe.Replace(loaihsba_code + "_", "");
                    Control[] list = this.Controls.Find(maTieuDe, true);
                    if (list.Length > 0)
                    {
                        System.Console.WriteLine("   control: " + maTieuDe + " -> " + list[0].GetType().ToString());
                        // MemoEdit
                        if (list[0].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" &&
                            "txtQTBENHLY,txtBANTHAN,txtGIADINH,txtTOANTHAN,txtCACBOPHAN,txtTOMTATKQCLS,txtDXLTUYENDUOI,txtQTBENHLYDIENBIENLS,txtKETQUAXNCLS,txtPHUONGPHAPDIEUTRI,txtTTNGUOIBENHRAVIEN,txtHUONGDIEUTRICHEDO".IndexOf(list[0].Name) > -1)
                        {
                            MemoEdit txt = (MemoEdit)list[0];
                            txt.Text = Func.unescape(noidung);
                        }
                        //uc bệnh chính
                        else if (list[0].GetType().ToString() == "VNPT.HIS.UserControl.ucSearchLookup2" &&
                            "txtMACDBANDAU,txtMACHANDOANRAVIEN,txtMABENHCHINH".IndexOf(list[0].Name) > -1)
                        {
                            UserControl.ucSearchLookup2 uc = (UserControl.ucSearchLookup2)list[0];
                            uc.SelectedValue = noidung;
                        }
                        //uc bệnh kèm theo
                        else if (list[0].GetType().ToString() == "VNPT.HIS.UserControl.ucSearchLookup2" &&
                            "txtBENHKEMTHEO".IndexOf(list[0].Name) > -1)
                        {
                            UserControl.ucSearchLookup2 uc = (UserControl.ucSearchLookup2)list[0];
                            uc.SelectedText = noidung;
                        }
                        else if (list[0].GetType().ToString() == "DevExpress.XtraEditors.TextEdit")
                        {
                            TextEdit txt = (TextEdit)list[0];
                            txt.Text = Func.unescape(noidung);
                        }
                        else
                        {
                            System.Console.WriteLine(" Bo qua control: " + maTieuDe + " -> " + noidung);
                        }
                    }
                    else System.Console.WriteLine(" Ko tim thay control: " + maTieuDe + " -> " + noidung);
                }
                else if (kieuDuLieu == "2") // datetime
                {
                    maTieuDe = "cld" + maTieuDe.Replace(loaihsba_code + "_", "");
                    Control[] list = this.Controls.Find(maTieuDe, true);
                    if (list.Length > 0 && list[0].GetType().ToString() == "DevExpress.XtraEditors.DateEdit")
                    {
                        DateEdit dtime = (DateEdit)list[0];
                        dtime.Text = Func.unescape(noidung);
                    }
                    else System.Console.WriteLine(" Ko tim thay control DateEdit: " + maTieuDe + " -> " + noidung);
                }
                else if (kieuDuLieu == "5")
                {
                    maTieuDe = "chk" + maTieuDe.Replace(loaihsba_code + "_", "");
                    Control[] list = this.Controls.Find(maTieuDe, true);
                    if (list.Length > 0 && list[0].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                    {
                        CheckEdit chk = (CheckEdit)list[0];
                        chk.Checked = (noidung == "true");
                    }
                    else System.Console.WriteLine(" Ko tim thay control CheckEdit: " + maTieuDe + " -> " + noidung);
                }
            }
        }


        private void comboBenhAnMau_Change(object sender, EventArgs e)
        {
            string mauid = cboMAUBAID.SelectValue;
            if (!string.IsNullOrEmpty(mauid))
            {
                loadDataTemp(benhnhanid, loaibenhanid, maloaibenhan, mauid, khambenhid);
            }
            else
            {
                loadData(benhnhanid, loaibenhanid, maloaibenhan, hosobenhanid, khambenhid);
            }
        }
        string[] arrSoPhimAnh = { "txtSOXQUANG", "txtSOSCANNER", "txtSOSIEUAM", "txtSOXETNGHIEM", "txtSOKHAC", "txtSOTOANBOHOSO" };
        private void SoPhimAnh_EditValueChanged(object sender, EventArgs e)
        {
            // reset gia tri tong so ho so phim anh khi thay doi cac so
            // lieu(x-quang, scanner, xieu am,...)

            int sumSoPA = 0;
            for (int l = 0; l < arrSoPhimAnh.Length - 1; l++)
            {
                Control[] list = this.Controls.Find(arrSoPhimAnh[l], true);
                if (list.Length > 0 && list[0].GetType().ToString() == "DevExpress.XtraEditors.TextEdit")
                {
                    TextEdit txt = (TextEdit)list[0];
                    if (txt.Text.Trim() != "" && txt.Text.Trim() != "0")
                        sumSoPA = sumSoPA + Func.Parse(txt.Text.Trim());
                }
            }

            if (sumSoPA > 0) txtSOTOANBOHOSO.Text = sumSoPA.ToString();
        }
        private void txt_KeyPress_OnlyNumber(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //if (e.Handled == false && !char.IsControl(e.KeyChar) && txtNamsinh.Text.Length >= 4) e.Handled = true; 
        }

        private bool validateForm()
        {
            string SELECTOR_ERRORS = "";
            bool check = true;
            if (Func.getDatetime_Short(cldDTNTTUNGAY.DateTime) > Func.getDatetime_Short(cldDTNTDENNGAY.DateTime))
            {
                SELECTOR_ERRORS += "Điều trị ngoại trú từ ngày phải nhỏ hơn đến ngày \r\n";
                cldDTNTTUNGAY.Focus();
                check = false;
            }
            if (Func.Parse_float(txtCANNANG.Text) <= 0)
            {
                SELECTOR_ERRORS += "Cân nặng phải là số và lớn hơn 0 \r\n";
                txtCANNANG.Focus();
                check = false;
            }
            string[] cannang = txtCANNANG.Text.Trim().Split('.');
            if (cannang.Length > 1 && cannang[1].Length > 1)
            {
                SELECTOR_ERRORS += "Cân nặng chỉ nhập 1 số thập phân \r\n";
                txtCANNANG.Focus();
                check = false;
            }
            if (Func.Parse_float(txtNHIETDO.Text) <= 0)
            {
                SELECTOR_ERRORS += "Nhiệt độ phải là số và lớn hơn 0 \r\n";
                txtNHIETDO.Focus();
                check = false;
            }
            string[] nhietdo = txtNHIETDO.Text.Trim().Split('.');
            if (nhietdo.Length > 1 && nhietdo[1].Length > 1)
            {
                SELECTOR_ERRORS += "Nhiệt độ chỉ nhập 1 số thập phân \r\n";
                txtNHIETDO.Focus();
                check = false;
            }

            if (SELECTOR_ERRORS != "")
                MessageBox.Show(SELECTOR_ERRORS);

            return check;
        }
        private void callBack(string s_check, int s_insert)
        {
            string message = "";
            if (s_insert == 1)
            {
                message = "Thêm mới";
            }
            else
            {
                message = "Cập nhật";
            }
            if (s_check == "0")
            {
                MessageBox.Show(message + " hồ sơ bệnh án không thành công");
                return;
            }
            else if (s_check == "1")
            {
                MessageBox.Show(message + " hồ sơ bệnh án thành công");
                //location.reload();
            }
        }
        private string insHSBenhAn(string benhnhanid, string loaibenhanid, string khambenhid, string i_hid, int checkInsert)
        {
            string _return = RequestHTTP.call_ajaxCALL_SP_I("BAN.INS.04", benhnhanid + "$" + loaibenhanid + "$" + khambenhid);

            if (_return == "0")
            {
                // truong hop cap nhat or insert khong thanh // cong
                callBack(_return, checkInsert);
            }

            // lay id cua ho so benh an sau khi insert
            DataTable dt = RequestHTTP.get_ajaxExecuteQueryO("BAN.07", new string[] { benhnhanid, loaibenhanid, i_hid });
            string hosobenhanid = "";
            if (dt.Rows.Count > 0) hosobenhanid = dt.Rows[0]["HOSOBENHANID"].ToString();

            return hosobenhanid;
        }
        private string insHSPhimAnh(string[] arrSoPhimAnh, string hosobenhanid, int checkInsert, string sql)
        {
            //<hsba>
            //	<row><col_name>SOXQUANG</col_name><col_value>5</col_value></row>
            //	<row><col_name>SOSCANNER</col_name><col_value>1</col_value></row>
            //	<row><col_name>SOSIEUAM</col_name><col_value>1</col_value></row>
            //	<row><col_name>SOTOANBOHOSO</col_name><col_value>7</col_value></row>
            //</hsba>
            string _return = "1";
            string strBanXml = "<hsba>";
            for (int i = 0; i < arrSoPhimAnh.Length; i++)
            {
                Control[] list = this.Controls.Find(arrSoPhimAnh[i], true);
                if (list.Length > 0 && list[0].GetType().ToString() == "DevExpress.XtraEditors.TextEdit")
                {
                    TextEdit txt = (TextEdit)list[0];
                    if (txt.Text.Trim() !="")
                        strBanXml += "<row><col_name>" + arrSoPhimAnh[i].Substring(3) + "</col_name><col_value>" + txt.Text.Trim() + "</col_value></row>";
                }
            }
            strBanXml += "</hsba>";

            _return = RequestHTTP.call_ajaxCALL_SP_I(sql, hosobenhanid + "$" + strBanXml + "$" + checkInsert);

            return _return;
        }
        private string insHoiKham(string checkInput, string loaihsba_code, DataTable arrMaHoiKham, string hosobenhanid, int checkInsert, string sql)
        {
            // for lần lượt các control trong arrMaHoiKham để tạo ra chuỗi sau.
            // ở đây tạm làm fix cứng.
            //           <hsba>
            //<row><col_name>NT01_LYDOVAOVIEN</col_name><col_value>11111</col_value></row>
            //<row><col_name>NT01_TOANTHAN</col_name><col_value>bình thường</col_value></row>
            //<row><col_name>NT01_MACH</col_name><col_value>100</col_value></row>
            //<row><col_name>NT01_NHIETDO</col_name><col_value>37</col_value></row>
            //<row><col_name>NT01_HUYETAP1</col_name><col_value>80</col_value></row>
            //<row><col_name>NT01_HUYETAP2</col_name><col_value>120</col_value></row>
            //<row><col_name>NT01_NHIPTHO</col_name><col_value>100</col_value></row>
            //<row><col_name>NT01_CANNANG</col_name><col_value>60</col_value></row>
            //<row><col_name>NT01_CACBOPHAN</col_name><col_value>tai mũi họng</col_value></row>
            //<row><col_name>NT01_MACDBANDAU</col_name><col_value>J02</col_value></row>
            //<row><col_name>NT01_CDBANDAU</col_name><col_value>Viêm họng cấp</col_value></row>
            //<row><col_name>NT01_MABENHCHINH</col_name><col_value>J02</col_value></row>
            //<row><col_name>NT01_BENHCHINH</col_name><col_value>Viêm họng cấp</col_value></row>
            //               </hsba>
            string _return = "1";
            string strBanXml = "<hsba>" +
   "<row><col_name>NT01_LYDOVAOVIEN</col_name><col_value>" + txtLYDOVAOVIEN.Text + "</col_value></row>" +
   "<row><col_name>NT01_QTBENHLY</col_name><col_value>" + txtQTBENHLY.Text + "</col_value></row>" +
   "<row><col_name>NT01_BANTHAN</col_name><col_value>" + txtBANTHAN.Text + "</col_value></row>" +
   "<row><col_name>NT01_GIADINH</col_name><col_value>" + txtGIADINH.Text + "</col_value></row>" +
   "<row><col_name>NT01_TOANTHAN</col_name><col_value>" + txtTOANTHAN.Text + "</col_value></row>" + 

   "<row><col_name>NT01_MACH</col_name><col_value>" + txtMACH.Text + "</col_value></row>" +
   "<row><col_name>NT01_NHIETDO</col_name><col_value>" + txtNHIETDO.Text + "</col_value></row>" +
   "<row><col_name>NT01_HUYETAP1</col_name><col_value>" + txtHUYETAP1.Text + "</col_value></row>" +
   "<row><col_name>NT01_HUYETAP2</col_name><col_value>" + txtHUYETAP2.Text + "</col_value></row>" +
   "<row><col_name>NT01_NHIPTHO</col_name><col_value>" + txtNHIPTHO.Text + "</col_value></row>" +
   "<row><col_name>NT01_CANNANG</col_name><col_value>" + txtCANNANG.Text + "</col_value></row>" +

   "<row><col_name>NT01_CACBOPHAN</col_name><col_value>" + txtCACBOPHAN.Text + "</col_value></row>" +
   "<row><col_name>NT01_TOMTATKQCLS</col_name><col_value>" + txtTOMTATKQCLS.Text + "</col_value></row>" +
   "<row><col_name>NT01_MACDBANDAU</col_name><col_value>" + txtMACDBANDAU.SelectedValue + "</col_value></row>" +
   "<row><col_name>NT01_CDBANDAU</col_name><col_value>" + txtMACDBANDAU.SelectedText + "</col_value></row>" +
   "<row><col_name>NT01_DXLTUYENDUOI</col_name><col_value>" + txtDXLTUYENDUOI.Text + "</col_value></row>" +

   "<row><col_name>NT01_MACHANDOANRAVIEN</col_name><col_value>" + txtMACHANDOANRAVIEN.SelectedValue + "</col_value></row>" +
   "<row><col_name>NT01_CHANDOANRAVIEN</col_name><col_value>" + txtMACHANDOANRAVIEN.SelectedText + "</col_value></row>" +

   "<row><col_name>NT01_DTNTTUNGAY.</col_name><col_value>" + cldDTNTTUNGAY.DateTime.ToString("") + "</col_value></row>" +
   "<row><col_name>NT01_DTNTDENNGAY</col_name><col_value>" + cldDTNTDENNGAY.DateTime.ToString("") + "</col_value></row>" +


   "<row><col_name>NT01_MABENHCHINH</col_name><col_value>" + txtMABENHCHINH.SelectedValue + "</col_value></row>" +
   "<row><col_name>NT01_BENHCHINH</col_name><col_value>" + txtMABENHCHINH.SelectedText + "</col_value></row>" +


   "<row><col_name>NT01_QTBENHLYDIENBIENLS</col_name><col_value>" + txtQTBENHLYDIENBIENLS.Text + "</col_value></row>" +
   "<row><col_name>NT01_KETQUAXNCLS</col_name><col_value>" + txtKETQUAXNCLS.Text + "</col_value></row>" +
   "<row><col_name>NT01_MABENHKEMTHEO</col_name><col_value>" + txtBENHKEMTHEO.SelectedValue + "</col_value></row>" +
   "<row><col_name>NT01_BENHKEMTHEO</col_name><col_value>" + txtBENHKEMTHEO.SelectedText + "</col_value></row>" +
   "<row><col_name>NT01_PHUONGPHAPDIEUTRI</col_name><col_value>" + txtPHUONGPHAPDIEUTRI.Text + "</col_value></row>" +
   "<row><col_name>NT01_TTNGUOIBENHRAVIEN</col_name><col_value>" + txtTTNGUOIBENHRAVIEN.Text + "</col_value></row>" +
   "<row><col_name>NT01_HUONGDIEUTRICHEDO</col_name><col_value>" + txtHUONGDIEUTRICHEDO.Text + "</col_value></row>" +

   "</hsba>";    

            int checkHasDataHK = 1; // nếu có control checkbox nào trong arrMaHoiKham được check, thì gán giá trị này =1, hoặc ....??

            // insert du lieu thong tin hoi kham benh
            if (checkHasDataHK == 1)
            {
                _return = RequestHTTP.call_ajaxCALL_SP_I(sql, hosobenhanid + "$" + strBanXml + "$" + checkInsert);
            }
            return _return;
        }


        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (validateForm() == false) return;

            if (checkInsert == 1)
            {
                // buoc 1:insert thong tin ho so benh an
                hosobenhanid = insHSBenhAn(benhnhanid, loaibenhanid, khambenhid, i_hid, checkInsert);
            }

            // buoc 2: insert thong tin cho ho so phim anh
            string r1 = insHSPhimAnh(arrSoPhimAnh, hosobenhanid, checkInsert, "BAN.INS.01");

            // buoc 3 insert thong tin hoi kham benh va tong ket benh an
            string r2 = insHoiKham("", maloaibenhan, arrMaHoiKham, hosobenhanid, checkInsert, "BAN.INS.02");
            //dong bo du lieu 
            string _return = RequestHTTP.call_ajaxCALL_SP_I("BAN.DONGBO.RIGHT", hosobenhanid + "$" + khambenhid);

            if (txtMABENHCHINH.SelectedValue != "")
            {
                string _r_td = RequestHTTP.call_ajaxCALL_SP_I("BAN.ICD.TD", txtMABENHCHINH.SelectedValue + "$" + Const.local_khoaId);
            }

            if (_return == "1" && r1 == "1" && r2 == "1")
            {
                MessageBox.Show("Cập nhật hồ sơ bệnh án thành công");
            }
            else
            {
                callBack(_return, checkInsert);
            }
        }

        private void btn_LuuDong_Click(object sender, EventArgs e)
        {
            btn_Luu_Click(sender, e);
            this.Close();
        }

        private void btn_In_Click(object sender, EventArgs e)
        {
            string i_file = "BAN003_NGOAITRU_QD4069_A4";
            DataTable table = new DataTable();
            table.Columns.Add("name");
            table.Columns.Add("type");
            table.Columns.Add("value");
            table.Rows.Add("hosobenhanid", "String", hosobenhanid);
            table.Rows.Add("loaibenhanid", "String", loaibenhanid);
            table.Rows.Add("benhnhanid", "String", benhnhanid);
            table.Rows.Add("i_sch", "String", Const.local_user.DB_SCHEMA);

            string rpName = i_file + "_" + RequestHTTP.getSysDatetime() + "." + "pdf";

            VNPT.HIS.Controls.SubForm.frmPrint frm = new VNPT.HIS.Controls.SubForm.frmPrint(rpName, i_file, table, 800, 1024);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

//            0
//:
//{name: "hosobenhanid", type: "String", value: "118439"}
//1
//:
//{name: "loaibenhanid", type: "String", value: "20"}
//2
//:
//{name: "benhnhanid", type: "String", value: "45897"}
//3
//:
//{name: "i_sch", type: "String", value: "HIS_DATA_BVNT"}
//length
//:
//4
        }
        private void btn_Dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private string insHSBenhAnTemp(string khoaid, string phongid, string tenmau, string loaibenhanid)
        {
            string _return = "0"; 
            _return = RequestHTTP.call_ajaxCALL_SP_I("BAN.INS.04_MAU", khoaid + "$" + phongid + "$" + tenmau + "$" + loaibenhanid);
            if (_return == "0")
            {
                // truong hop cap nhat or insert khong thanh 
                MessageBox.Show("Lưu mẫu hồ sơ bệnh án thất bại");
            } 
            return _return; 
        } 
        
        private void btn_LuuBAMau_Click(object sender, EventArgs e)
        {
            string tenmau = txtTENBENHANMAU.Text.Trim();
            if (tenmau == "")
            {
                MessageBox.Show("Tên bệnh án mẫu không được để trống");
                txtTENBENHANMAU.Focus();
                return;
            }

            // buoc 1:insert thong tin ho so benh an				
            var mauid = insHSBenhAnTemp(Const.local_khoaId.ToString(), Const.local_phongId.ToString(), tenmau, loaibenhanid);
            // buoc 2: insert thong tin cho ho so phim anh
            var r1 = insHSPhimAnh(arrSoPhimAnh, mauid, checkInsert, "BAN.INS.01_MAU");

            // buoc 3 insert thong tin hoi kham benh va tong ket benh an
            var r2 = insHoiKham("", maloaibenhan, arrMaHoiKham, mauid, checkInsert, "BAN.INS.02_MAU");

            if (mauid == "-1")
            {
                MessageBox.Show("Tên mẫu bệnh án đã tồn tại trên hệ thống");
                return;
            }
            //xu ly su kien callback	
            if (r1 == "1" && r2 == "1")
            {
                MessageBox.Show("Cập nhật hồ sơ bệnh án mẫu thành công");
                load_cboMAUBAID();
            }
            else
            {
                callBack(r1, checkInsert);
            }
        }

        private void btn_XoaBAMau_Click(object sender, EventArgs e)
        {
            string mauid = cboMAUBAID.SelectValue;
             
            string _return = RequestHTTP.call_ajaxCALL_SP_I("DEL_MAU_BA", mauid);

            if (_return == "0")
            {
                // truong hop cap nhat or insert khong thanh 
                MessageBox.Show("Bạn phải chọn tên mẫu bệnh án cần xóa");
            }

            if (_return == "1")
            {
                MessageBox.Show("Xóa hồ sơ bệnh án mẫu thành công");
                load_cboMAUBAID();
            }
            else
            {
                MessageBox.Show("Xóa bệnh án mẫu không thành công");
            }
        }

        private void btn_CopyBA_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Thông tin bệnh án sau cùng sẽ được load lên, bạn có tiếp tục?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string _return = RequestHTTP.call_ajaxCALL_SP_I("BAN.20", benhnhanid + "$" + loaibenhanid);
                if (_return == "0")
                {
                    MessageBox.Show("Bệnh nhân chưa nhập thông tin bệnh án nào, không sao chép được.");
                    return;
                }
                else if (_return == "-1")
                {
                    MessageBox.Show("Lỗi trong quá trình xử lý.");
                    return;
                }
                else if (_return == hosobenhanid)
                {
                    MessageBox.Show("Bệnh án sau cùng chính là bệnh án đang mở.");
                }
                else
                {
                    loadData(benhnhanid, loaibenhanid, maloaibenhan, _return, khambenhid);
                    MessageBox.Show("Thông tin bệnh án cũ đã load lên form. Click Lưu để tạo mới bệnh án.");
                }
            }
        }
    }
}
