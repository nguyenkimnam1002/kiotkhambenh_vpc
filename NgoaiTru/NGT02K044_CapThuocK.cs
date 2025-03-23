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
using VNPT.HIS.CommonForm;
using VNPT.HIS.Controls.SubForm;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K044_CapThuocK : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private string _doiTuongBNID;
        private string _ngayBHYT_KT;
        private string _tiepNhanID;
        private string _khamBenhID;
        private string _maBenhNhan;

        private string _hoSoBenhAnID;
        private string _khoaID;
        private string _phongID;

        private string _ngaySinh;
        private string _gioiTinhID;
        private string _benhNhanID;
        private string _tuDongInPhieuKham;
        private string _fileExportType;

        public NGT02K044_CapThuocK()
        {
            InitializeComponent();
        }
        
        private string khamBenhID = "";
        private string phongID = "";
        //private string tiepNhanID = "";
        //private string maBenhNhan = "";
        private string benhNhanID = "";
        private string doiTuongBenhNhanID = "";
        private string hoSoBenhAnID = "";
        private string mauBenhPhamID = "";

        public void Load_Data(string khamBenhID, string phongID,string benhNhanID, string doiTuongBenhNhanID, string hoSoBenhAnID, 
            string mauBenhPhamID)
        {
            this.khamBenhID = khamBenhID;
            this.phongID = phongID;
            //this.tiepNhanID = tiepNhanID;
            //this.maBenhNhan = maBenhNhan;
            this.benhNhanID = benhNhanID;
            this.doiTuongBenhNhanID = doiTuongBenhNhanID;
            this.hoSoBenhAnID = hoSoBenhAnID;
            this.mauBenhPhamID = mauBenhPhamID;
        }
        //dùng cho khi gọi form sửa đơn thuốc ko thuốc
        public void set_para_UPDATE(DataRowView drv)
        {
            this.Text = "Cập nhật phiếu thuốc không thuốc";

            //web ko dùng đến biến: OPTION DICHVUCHAID nên comment lại

            //string OPTION = "";
            //if (drv.DataView.Table.Columns.Contains("TENKHO") == false || drv["TENKHO"].ToString() == "")
            //    OPTION = "02D011";
            //if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() == "1") OPTION = "02D010";
            //else if (drv["LOAIPHIEUMAUBENHPHAM"].ToString() == "2") OPTION = "02D014";

            this.loaiDon = "0";
            if (drv["LOAIKEDON"].ToString() == "1") this.loaiDon = "0";
            else this.loaiDon = "1";

            //string DICHVUCHAID = ""; //trên web truyền rỗng  

            //lay loai thuoc 
            DataTable arr_loaithuoc = RequestHTTP.call_ajaxCALL_SP_O("NTU02D033_LOAITHUOC", drv["MAUBENHPHAMID"].ToString(), 0);
            if (arr_loaithuoc.Rows.Count > 0)
            {
                if (arr_loaithuoc.Rows[0]["LOAI"].ToString() == "3")
                {
                    //OPTION = "02D017";
                    this.loaiDon = "1";
                }
            }

            this.iAction = "Upd"; 
        }

        string coSoYTeID = "" + Const.local_user;
        string loaiDon = "1";
        private string iAction = "Add";
        
        private void NGT02K044_CapThuocK_Load(object sender, EventArgs e)
        {
            khoiTao();
            initControl();
            bindEvent();
        }

        private void khoiTao()
        {
            DataTable dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
            ucslookup_ICD10.setData(dt, "ICD10CODE", "ICD10NAME");
            ucslookup_ICD10.setColumn("RN", -1, "", 0);
            ucslookup_ICD10.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucslookup_ICD10.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            ucslookup_BenhKT.setData(dt, "ICD10CODE", "ICD10NAME");
            ucslookup_BenhKT.setEvent_Enter(ucslookup_BenhKT_KeyEnter);
            ucslookup_BenhKT.setColumn("RN", -1, "", 0);
            ucslookup_BenhKT.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            ucslookup_BenhKT.setColumn("ICD10NAME", 1, "Tên bệnh", 0);
            ucslookup_BenhKT.setEvent_Check(ucslookup_BenhKT_Check);
            ucslookup_BenhKT.btnReset.Visible = true;
            ucslookup_BenhKT.btnEdit.Visible = true;
            ucslookup_BenhKT.btnReset.Text = "Xóa bệnh KT";
            ucslookup_BenhKT.btnEdit.Text = "Sửa BP";

            ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging("DMC.LOIDANBS", 1, 100000, new string[] { }, new string[] { }, "");
            dt = MyJsonConvert.toDataTable(ds.rows);
            ucslookup_LoiDanBS.setData(dt, 0, 1);
            ucslookup_LoiDanBS.setColumn(0, 0, "Lời dặn", 0);
        }



        private void ucslookup_BenhKT_KeyEnter(object sender, EventArgs e)
        {
            if (iAction != "")
                doInsDonThuoc(iAction);
        }

        private void ucslookup_BenhKT_Check(object sender, EventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)sender;

                if (drv["ICD10CODE"].ToString() == ucslookup_ICD10.SelectedValue)
                    ucslookup_BenhKT.messageError = "Bệnh phụ vừa nhập không được trùng với bệnh chính.";
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        private void initControl()
        {
            if (iAction == "Add")
            { }
            else
            { }

            string ghiChu = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "NGT_GHICHU_BENHCHINH");
            if (ghiChu == "1")
            { }

            string dt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_FILEEXPORT_TYPE");
            if (dt != "rtf" && dt != "pdf" && dt != "docx")
                dt = "pdf";
            _fileExportType = dt;
        }

        private void bindEvent()
        {
            layThongTinBenhNhan();
        }

        private void echeck_DaCapPhieu_CheckedChanged(object sender, EventArgs e)
        {
            if (echeck_DaCapPhieu.Checked == true)
            {
                if (coSoYTeID == "902")
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("name");
                    table.Columns.Add("type");
                    table.Columns.Add("value");
                    table.Rows.Add("khambenhid", "String", khamBenhID);
                    table.Rows.Add("maubenhphamid", "String", mauBenhPhamID);

                    frmPrint frm = new frmPrint("In Đơn Thuốc", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();
                }
                else
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("name");
                    table.Columns.Add("type");
                    table.Columns.Add("value");
                    table.Rows.Add("khambenhid", "String", khamBenhID);

                    frmPrint frm = new frmPrint("In Đơn Thuốc", "NGT014_GIAYHENKHAMLAI_TT402015_A4", table);
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();
                }
            }
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (iAction != "")
                doInsDonThuoc(iAction);
        }

        private void btn_dong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //=============
        private void layThongTinBenhNhan()
        {
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("NTU02D010.10", khamBenhID + "$" + phongID, 0);
            DataTable data_ar1 = null;
            if (iAction == "Upd")
                data_ar1 = RequestHTTP.call_ajaxCALL_SP_O("NTU02D010.11", mauBenhPhamID, 0);

            if (data_ar != null)
            {
                etext_MaBN.Text = data_ar.Rows[0]["MABENHNHAN"].ToString();
                etext_NamSinh.Text = data_ar.Rows[0]["NAMSINH"].ToString();
                etext_GioiTinh.Text = data_ar.Rows[0]["GIOITINH"].ToString();
                etext_TenBN.Text = data_ar.Rows[0]["TENBENHNHAN"].ToString();
                etext_DoiTuong.Text = data_ar.Rows[0]["TEN_DTBN"].ToString();
                etext_TyLe.Text = data_ar.Rows[0]["TYLE_BHYT"].ToString();
                etext_SoThe.Text = data_ar.Rows[0]["MA_BHYT"].ToString();

                _doiTuongBNID = data_ar.Rows[0]["DOITUONGBENHNHANID"].ToString();
                _ngayBHYT_KT = data_ar.Rows[0]["BHYT_KT"].ToString();
                _tiepNhanID = data_ar.Rows[0]["TIEPNHANID"].ToString();
                _khamBenhID = data_ar.Rows[0]["KHAMBENHID"].ToString();
                _maBenhNhan = data_ar.Rows[0]["MABENHNHAN"].ToString();

                _hoSoBenhAnID = hoSoBenhAnID;
                _khoaID = "" + Const.local_khoaId;
                _phongID = phongID;

                _ngaySinh = "";
                _gioiTinhID = data_ar.Rows[0]["GIOITINH"].ToString() == "Nam" ? "1" : "0";
                _benhNhanID = benhNhanID;

                ucslookup_ICD10.SelectedValue = data_ar.Rows[0]["MACHANDOANRAVIEN" != "" ? "MACHANDOANRAVIEN" : "MACHANDOANVAOKHOA"].ToString();
                ucslookup_ICD10.SelectedText = data_ar.Rows[0]["CHANDOANRAVIEN" != "" ? "CHANDOANRAVIEN" : "CHANDOANVAOKHOA"].ToString();
                edtext_GhiChu.Text = data_ar.Rows[0]["GHICHU_BENHCHINH"].ToString();
                ucslookup_BenhKT.SelectedText = data_ar.Rows[0]["CHANDOANRAVIEN_KEMTHEO" != "" ? "CHANDOANRAVIEN_KEMTHEO" : "CHANDOANVAOKHOA_KEMTHEO"].ToString();
            }
            if (data_ar1 != null)
            {
                ucslookup_BenhKT.SelectedValue = data_ar1.Rows[0]["MACHANDOAN"].ToString();
                ucslookup_BenhKT.SelectedValue = data_ar1.Rows[0]["CHANDOAN"].ToString();
                edtext_GhiChu.Text = data_ar1.Rows[0]["GHICHU_BENHCHINH"].ToString();
                ucslookup_BenhKT.SelectedText = data_ar1.Rows[0]["CHANDOAN_KEMTHEO"].ToString();

                ucslookup_LoiDanBS.SelectedText = data_ar1.Rows[0]["LOIDANBACSI"].ToString();
                etext_SoNgayHen.Text = data_ar1.Rows[0]["SONGAYHEN"].ToString();

                if (data_ar1.Rows[0]["PHIEUHEN"].ToString() == "0")
                    echeck_DaCapPhieu.Checked = false;
                else
                    echeck_DaCapPhieu.Checked = true;
            }
        }

        private void doInsDonThuoc(string rAction)
        {
            if (ucslookup_ICD10.SelectedValue == "")
            {
                MessageBox.Show("Yêu cầu nhập thông tin chẩn đoán chính.");
                return;
            }
            
            string json = "";
            json += Func.json_item("DS_THUOC", null);
            json += Func.json_item("BENHNHANID", _benhNhanID);
            json += Func.json_item("MA_CHANDOAN", ucslookup_ICD10.SelectedValue.ToUpper());
            json += Func.json_item("CHANDOAN", ucslookup_ICD10.SelectedText);
            json += Func.json_item("CHANDOAN_KT", ucslookup_BenhKT.SelectedText);
            json += Func.json_item("DUONG_DUNG", null);
            json += Func.json_item("NGUOIDUNG_ID", Const.local_user.USER_ID);
            json += Func.json_item("CSYT_ID", coSoYTeID);
            json += Func.json_item("KHO_THUOCID", "");
            json += Func.json_item("INS_TYPE", loaiDon);
            json += Func.json_item("I_ACTION", rAction);
            json += Func.json_item("MAUBENHPHAMID", mauBenhPhamID);
            json += Func.json_item("PHIEUDIEUTRI_ID", "");
            json += Func.json_item("NGAYMAUBENHPHAM", "");
            json += Func.json_item("NGAYMAUBENHPHAM_SUDUNG", "");
            json += Func.json_item("DICHVUCHA_ID", null);
            json += Func.json_item("DOITUONG_BN_ID", _doiTuongBNID);
            json += Func.json_item("HINH_THUC_KE", "");
            json += Func.json_item("SONGAY_KE", null);
            json += Func.json_item("MAUBENHPHAMCHA_ID", null);
            json += Func.json_item("KHAMBENHID", khamBenhID);
            json += Func.json_item("GHICHU_BENHCHINH", edtext_GhiChu.Text);
            json += Func.json_item("TEMP_CODE", null);
            json += Func.json_item("YKIENBACSY", ucslookup_LoiDanBS.SelectedText);
            json += Func.json_item("SLTHANG", null);
            json += Func.json_item("NGAYHEN", etext_SoNgayHen.Text);
            json += Func.json_item("TIEPNHANID", _tiepNhanID);
            json += Func.json_item("HOSOBENHANID", _hoSoBenhAnID);
            json += Func.json_item("KHOAID", _khoaID);
            json += Func.json_item("PHONGID", _phongID);
            json += Func.json_item("NGAYSINH", "");
            json += Func.json_item("GIOITINHID", _gioiTinhID);
            json += Func.json_item("PHIEUHEN", echeck_DaCapPhieu.Checked ? "1" : "0");
            json = Func.json_item_end(json);
            json = json.Replace("\\", "\\\\").Replace("\"", "\\\"");

            //string hienThiThongBao = RequestHTTP.call_ajaxCALL_SP_S("COM.CAUHINH", "KBH_TUDONGLUU_KBHB");
            string ret = RequestHTTP.call_ajaxCALL_SP_S_result("NGT02K044.THEM", json);
            //string ret = RequestHTTP.call_ajaxCALL_SP_S("NGT02K044.THEM", json.Replace("\\", "\\\\").Replace("\"", "\\\""));
            if (int.Parse(ret) >= 1)
            {
                if (rAction == "Add")
                {
                    string hienThiThongBao = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "KBH_TUDONGLUU_KBHB");
                    if (hienThiThongBao != "1")
                        MessageBox.Show("Tạo phiếu thuốc không thuốc thành công");
                    else
                        MessageBox.Show("Cập nhật phiếu thuốc không thuốc thành công");
                }
                btn_Luu.Enabled = false;
                
                inDonThuoc(ret);
            }
            else
            {
                btn_Luu.Enabled = true;
                MessageBox.Show("Lỗi lưu phiếu thuốc không thuốc!");
            }
        }

        private void inDonThuoc(string _mauBenhPhamID)
        {
            bool existSplash = DevExpress.XtraSplashScreen.SplashScreenManager.Default == null;
            try
            {
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                
                DataTable table = new DataTable();
                table.Columns.Add("name");
                table.Columns.Add("type");
                table.Columns.Add("value");
                table.Rows.Add("maubenhphamid", "String", _mauBenhPhamID);

                frmPrint frm = new frmPrint("In Đơn Thuốc", "NGT006_DONTHUOCK_17DBV01_TT052016_A5", table);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
            finally
            {
                //Close Wait Form
                if (existSplash) DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }
        }
    }
}