using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using VNPT.HIS.Common;
using System.Text;
using System.Windows.Forms;

namespace L1_Mini
{
    public partial class DangKyKham : Form
    {
        public bool Hien_Thi_Chon_Bac_Si = false;
        public DangKyKham(int w, int h, bool parent_fullscreen = false)
        { 
            InitializeComponent(); 
            
            this.Width = w * 95 / 100;
            this.Height = h * 80 / 100;
            label2.Width = this.Width-20;
            if (parent_fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                TopMost = parent_fullscreen;
                //TopLevel = parent_fullscreen;
            }

            if (Hien_Thi_Chon_Bac_Si)
            if (Const.L1_BV_DEFAULT == 4 || (Const.L1_BV_DEFAULT == 0 && Const.default_test == 4)) //Nhiệt đới
            {
                ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging("COM.CHONBACSY", 1, 100000, new String[] { }, new string[] { }, "");
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);
                cboBacSi.DataSource = dt.DefaultView;
                cboBacSi.DisplayMember = "OFFICER_NAME";

                cboBacSi.SelectedIndex = -1;
                //cboBacSi.MaxDropDownItems = 10;
            }
        }
        private void FullScreen(bool full)
        {
            if (full)
            {
                FormBorderStyle = full ? FormBorderStyle.None : FormBorderStyle.Sizable;
                TopMost = full;
                //TopLevel = full;
                //this.WindowState = FormWindowState.Maximized;
            }
        }
        string maDKBD = "";
        string gtTheTu= "";
        string gtTheDen = "";
        string diaChi = "";
        string ngayDu5Nam = "";
        public void setTT_TheBHYT(string maDKBD, string gtTheTu, string gtTheDen, string diaChi, string ngayDu5Nam)
        {
            this.maDKBD = maDKBD;
            this.gtTheTu = gtTheTu;
            this.gtTheDen = gtTheDen;
            this.diaChi = diaChi;
            this.ngayDu5Nam = ngayDu5Nam;
        }

        public void setDt_BenhNhan(DataTable dt, string DOI_TUONG_ID, string hideBARCODE_BHYT)
          {
            dtTT_BenhNhan = dt;
            this.DOI_TUONG_ID = DOI_TUONG_ID;
            //this.hideBARCODE_BHYT = hideBARCODE_BHYT;
        }

        private DataTable dtTT_BenhNhan;
        private string DOI_TUONG_ID = "2";// BH, vien phi,... 2 vp 1 bh
        private string hideBARCODE_BHYT = "";

        int CHON; // = 1 đang chọn YCK; = 2 đang chọn PK
        DataTable dt_yeucaukham;
        DataTable dt_phongkham;
          
        private void DangKyKham_Load(object sender, EventArgs e)
        { 
            //LOAD_YEUCAUKHAM_THEO_DT = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "LOAD_YEUCAUKHAM_THEO_DT");


            // Chọn yêu cầu khám
            dt_yeucaukham = RequestHTTP.Cache_ajaxExecuteQuery(false, Const.tbl_YeuCauKham, DOI_TUONG_ID);
           

            CHON = 1;// = 1 đang chọn YCK; = 2 đang chọn PK
            if (dt_yeucaukham.Rows.Count > 0) 
                Load_Button(dt_yeucaukham, dt_yeucaukham.Rows[0]["col1"].ToString(), dt_yeucaukham.Rows[0]["col2"].ToString());
            else
                Load_Button(dt_yeucaukham, "", "");
        } 

        string thong_tin = "";
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (id == "") return;

            if (CHON == 1)
            {
                ID_YCKham = id;
                Ten_YCKham = name;
                
                if (Const.L1_BV_DEFAULT == 4 && Const.L1_CoSoID != "") // bệnh nhiệt đới
                {
                    // hàm mới chỉ lấy theo 1 cơ sở: BRANCH_ID Const.L1_CoSoID = GP: "7984"             KC: "7985";
                    dt_phongkham = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV.KIOS", new string[] { "[0]", "[1]" }, new string[] { ID_YCKham, Const.L1_CoSoID });
                }
                else
                {
                    // hàm cũ lấy phòng của tất cả: 
                    dt_phongkham = RequestHTTP.get_ajaxExecuteQuery("NGTPK.DV", new string[] { "[0]" }, new string[] { ID_YCKham});
                }

                CHON = 2; // = 1 đang chọn YCK; = 2 đang chọn PK
                Load_Button(dt_phongkham, "", "");
            }
            else if (CHON == 2)
            {
                if (ReturnData != null)
                {
                    ID_PKham = id;
                    Ten_PKham = name;
                    if (Ten_PKham.IndexOf("]") > -1)
                        Ten_PKham = Ten_PKham.Substring(Ten_PKham.IndexOf("]") + 1).Trim();

                    thong_tin = "";
                    BN_TiepNhan bn = initDLBenhNhan(ID_YCKham, Ten_YCKham, ID_PKham);
                               
                    string submit = LuuBN(bn);
                    
                    ReturnData(thong_tin + "|" + Ten_PKham + " f# " + submit, null); // // MABENHAN|GIOITINHID|NAMSINH|SDTBENHNHAN

                    this.Close();
                }
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            //this.Close();

            if (CHON == 1)
            {
                this.Close();
            }
            else if (CHON == 2)
            {
                CHON = 1;// = 1 đang chọn YCK; = 2 đang chọn PK
                Load_Button(dt_yeucaukham, "", "");
            }
        }


        #region Các hàm xử lý khi Submit chọn Đăng ký khám
        private BN_TiepNhan initDLBenhNhan(string ID_YCKham, string Ten_YCKham, string ID_PKham)
        {
            // {"result": "[{\"KHAMBENHID\": \"3492\",\"CHANDOANTUYENDUOI\": \"\",\"MACHANDOANTUYENDUOI\": \"\",\"MANOIGIOITHIEU\": \"\",\"HINHTHUCVAOVIENID\": \"3\",\"UUTIENKHAMID\": \"0\",\"TIEPNHANID\": \"3803\",\"NGAYTIEPNHAN\": \"17/11/2017 09:47\",\"PHONGID\": \"214\",\"DTBNID\": \"1\",\"BENHNHANID\": \"3832\",\"MABENHNHAN\": \"BN00002483\",\"TENBENHNHAN\": \"TEST AHIHIH\",\"HOSOBENHANID\": \"4006\",\"NGAY_SINH\": \"12/12/2015\",\"NAMSINH\": \"2015\",\"TUOI\": \"23\",\"BHYTID\": \"2545\",\"MA_BHYT\": \"TE1401100000277\",\"BHYT_BD\": \"12/12/2015\",\"BHYT_KT\": \"11/12/2021\",\"MA_KCBBD\": \"35148\",\"DIACHI_BHYT\": \"Xã Tân Hợp-Huyện Tân Kỳ-Nghệ An\",\"BHYT_LOAIID\": \"1\",\"DT_SINHSONG\": \"2\",\"DU5NAM6THANGLUONGCOBAN\": \"0\",\"TRADU6THANGLCB\": \"0\",\"QUYEN_LOI\": null,\"MUC_HUONG\": null,\"GIOITINHID\": \"2\",\"NGHENGHIEPID\": \"3\",\"DANTOCID\": \"25\",\"QUOCGIAID\": \"0\",\"SONHA\": \"\",\"DIAPHUONGID\": \"4042317269\",\"DIABANID\": null,\"TENDIAPHUONG\": \"Xã Tân Hợp-Huyện Tân Kỳ-Nghệ An\",\"NOILAMVIEC\": \"\",\"NGUOITHAN\": \"\",\"TENNGUOITHAN\": \"ffffffffffff\",\"DIENTHOAINGUOITHAN\": \"\",\"DIACHINGUOITHAN\": \"\",\"DICHVUKHAMBENHID\": \"10247\",\"DICHVUID\": \"1004\",\"PHONGKHAMDANGKYID\": \"2558\",\"TEN_KCBBD\": \"Bệnh viện sản nhi Hà Nam\",\"MAUBENHPHAMID\": \"11500\",\"SOTHUTU\": \"1\",\"TENNOIGIOITHIEU\": \"\",\"ORG_NAME\": \"PK Mắt P.314\",\"SLXN\": \"0\",\"SLCDHA\": \"0\",\"DIACHI\": \"Xã Tân Hợp-Huyện Tân Kỳ-Nghệ An\",\"THUKHAC\": \"0\",\"SLCHUYENKHOA\": \"0\",\"CONGKHAM\": \"1\",\"TKMACHANDOANTUYENDUOI\": \"\",\"TKMANOIGIOITHIEU\": \"\",\"TRANGTHAIKHAMBENH\": \"4\",\"NGAYTHUOC\": \"01/01/1990 00:00:00\",\"CHUADUYETKT\": \"0\",\"BHYT_DV\": \"0\",\"SUB_DTBNID\": \"0\",\"NGAYMAUBENHPHAM\": \"20171117\",\"PHONGKHAMID\": \"214\",\"SDTBENHNHAN\": \"\",\"SINHTHEBHYT\": \"1\"}]","out_var": "[]","error_code": 0,"error_msg": ""}


            //{ KHAMBENHID: 3492,CHANDOANTUYENDUOI: ,MACHANDOANTUYENDUOI: ,MANOIGIOITHIEU: ,HINHTHUCVAOVIENID: 3,UUTIENKHAMID: 0,TIEPNHANID: 3803
            // ,NGAYTIEPNHAN: 17 / 11 / 2017 09:47,PHONGID: 214,DTBNID: 1,BENHNHANID: 3832,MABENHNHAN: BN00002483,TENBENHNHAN: TEST AHIHIH, HOSOBENHANID: 4006
            // ,NGAY_SINH: 12 / 12 / 2015,NAMSINH: 2015,TUOI: 23,BHYTID: 2545,MA_BHYT: TE1401100000277,BHYT_BD: 12 / 12 / 2015,BHYT_KT: 11 / 12 / 2021
            //        ,MA_KCBBD: 35148,DIACHI_BHYT: Xã Tân Hợp - Huyện Tân Kỳ-Nghệ An,BHYT_LOAIID: 1,DT_SINHSONG: 2,DU5NAM6THANGLUONGCOBAN: 0,TRADU6THANGLCB: 0
            //        ,QUYEN_LOI: null,MUC_HUONG: null,GIOITINHID: 2,NGHENGHIEPID: 3,DANTOCID: 25,QUOCGIAID: 0,SONHA: ,DIAPHUONGID: 4042317269,DIABANID: null
            //        ,TENDIAPHUONG: Xã Tân Hợp - Huyện Tân Kỳ-Nghệ An,NOILAMVIEC: ,NGUOITHAN: ,TENNGUOITHAN: ffffffffffff,DIENTHOAINGUOITHAN: ,DIACHINGUOITHAN: 
            //    ,DICHVUKHAMBENHID: 10247,DICHVUID: 1004,PHONGKHAMDANGKYID: 2558,TEN_KCBBD: Bệnh viện sản nhi Hà Nam, MAUBENHPHAMID: 11500,SOTHUTU: 1,TENNOIGIOITHIEU: 
            //    ,ORG_NAME: PK Mắt P.314,SLXN: 0,SLCDHA: 0,DIACHI: Xã Tân Hợp - Huyện Tân Kỳ-Nghệ An,THUKHAC: 0,SLCHUYENKHOA: 0,CONGKHAM: 1,TKMACHANDOANTUYENDUOI: 
            //    ,TKMANOIGIOITHIEU: ,TRANGTHAIKHAMBENH: 4,NGAYTHUOC: 01 / 01 / 1990 00:00:00,CHUADUYETKT: 0,BHYT_DV: 0,SUB_DTBNID: 0,NGAYMAUBENHPHAM: 20171117
            //        ,PHONGKHAMID: 214,SDTBENHNHAN: ,SINHTHEBHYT: 1} DT

            //                CHANDOANTUYENDUOI: N17.0 - Suy thận cấp có hoại tử ống thận,MACHANDOANTUYENDUOI: n17,NGAYTIEPNHAN: 31 / 08 / 2018 14:00,DTBNID: 1
            //            ,BENHNHANID: 11480,MABENHNHAN: BN00000181,TENBENHNHAN: CAO THANH MAI,NGAY_SINH: ,NAMSINH: 1991,TUOI: 27,TRANGTHAIKHAMBENH: 1
            //            ,MA_BHYT: DN4819759022488,BHYT_BD: 01 / 01 / 2018,BHYT_KT: 31 / 08 / 2018,TRADU6THANGLCB: 1,MA_KCBBD: 36035
            //            ,DIACHI_BHYT: Xã Tam Thanh - Huyện Vụ Bản-Nam Định,BHYT_LOAIID: 2,DT_SINHSONG: 0,DU5NAM6THANGLUONGCOBAN: 1,QUYEN_LOI: null,MUC_HUONG: null
            //            ,GIOITINHID: 2,NGHENGHIEPID: 1,DANTOCID: 25,CHUCDANH: ,QUOCGIAID: 0,SONHA: ,DIAPHUONGID: 3635913789,DIABANID: null
            //            ,TENDIAPHUONG: Xã Tam Thanh - Huyện Vụ Bản-Nam Định,NOILAMVIEC: ,NGUOITHAN: ,TENNGUOITHAN: ,DIENTHOAINGUOITHAN: ,DIACHINGUOITHAN:
            //,TEN_KCBBD: Bệnh viện đa khoa huyện Vụ Bản,TENNOIGIOITHIEU: Bệnh viện Thanh Nhàn, SLXN: 0,SLCDHA: 0
            //            ,DIACHI: Xã Tam Thanh - Huyện Vụ Bản-Nam Định,ANHBENHNHAN: null,THUKHAC: 0,SLCHUYENKHOA: 0,CONGKHAM: 0,SDTBENHNHAN: ,SINHTHEBHYT: 0
            //            ,HENKHAM: 0,SOCMTND: ,NGAYTHUOC: 01 / 01 / 1990 00:00:00,SUB_DTBNID: 0,CHUADUYETKT: 0,NGAYMAUBENHPHAM: 20180831,BACSYYCID: -1
            //            ,MANOIGIOITHIEU: 01006,TKMANOIGIOITHIEU: 01006,HINHTHUCVAOVIENID: 3,UUTIENKHAMID: 0,DICHVUID: 417562,PHONGKHAMID: 8432}

            BN_TiepNhan bn = new BN_TiepNhan();

            #region Thông tin hành chính
            //ucThongTinHanhChinh1.load_benhnhan_theoMa(dtTT_BenhNhan);

            bn.USERID = Const.local_user.USER_ID;
            bn.KHOAID = Const.local_khoaId.ToString();
            bn.PHONGIDTIEPNHAN = Const.local_phongId.ToString();
            bn.PHONGID = Const.local_phongId.ToString();
            bn.TKPHONGID = bn.PHONGID;

            bn.MABENHNHAN = dtTT_BenhNhan.Rows[0]["MABENHNHAN"].ToString();
            bn.TENBENHNHAN = dtTT_BenhNhan.Rows[0]["TENBENHNHAN"].ToString();
            bn.NGAYSINH = dtTT_BenhNhan.Rows[0]["NGAY_SINH"].ToString();
            bn.NAMSINH = dtTT_BenhNhan.Rows[0]["NAMSINH"].ToString();
            bn.SOCMTND = dtTT_BenhNhan.Rows[0]["SOCMTND"].ToString();

            string TUOI, DVTUOI;
            Tinh_Tuoi(bn.NGAYSINH, bn.NAMSINH, dtTT_BenhNhan.Rows[0]["TUOI"].ToString(), out TUOI, out DVTUOI);
            
            bn.TUOI = TUOI;
            bn.DVTUOI = DVTUOI;

            bn.SONHA = dtTT_BenhNhan.Rows[0]["SONHA"].ToString();
            bn.SDTBENHNHAN = dtTT_BenhNhan.Rows[0]["SDTBENHNHAN"].ToString();

            bn.GIOITINHID = dtTT_BenhNhan.Rows[0]["GIOITINHID"].ToString();
            bn.NGHENGHIEPID = dtTT_BenhNhan.Rows[0]["NGHENGHIEPID"].ToString();
            bn.DANTOCID = dtTT_BenhNhan.Rows[0]["DANTOCID"].ToString();
            bn.QUOCGIAID = dtTT_BenhNhan.Rows[0]["QUOCGIAID"].ToString();
            //bn.TENQUOCGIA = ucThongTinHanhChinh1.ucQuoctich.Text;

            #region setComboTinh_Huyen_Xa_byDiaPhuongID(dtTT_BenhNhan.Rows[0]["DIAPHUONGID"].ToString());
            //Tỉnh huyện xã viết tắt
            //DataTable dtTHX = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_TinhhuyenxaViettat); 
            //foreach (DataRow row in dtTHX.Rows)
            //{
            //    if (row["VALUE"].ToString() == dtTT_BenhNhan.Rows[0]["DIAPHUONGID"].ToString())
            //    {
            //        //ucTinhHuyenXa.searchLookUpEdit.EditValue = row["TENVIETTATDAYDU"];
            //        break;
            //    }
            //}
            try
            {
                string DiaPhuongID = dtTT_BenhNhan.Rows[0]["DIAPHUONGID"].ToString();
                Dia_Chi dc = VNPT.HIS.Common.RequestHTTP.getDIACHI(DiaPhuongID);
                bn.HC_TINHID = dc.ID_TINH;
                bn.TENTINH = dc.TEN_TINH;
                bn.HC_HUYENID = dc.ID_HUYEN;
                bn.TENHUYEN = dc.TEN_HUYEN;
                bn.HC_XAID = dc.ID_XA;
                bn.TENXA = dc.TEN_XA;

                bn.DIACHI = dtTT_BenhNhan.Rows[0]["DIACHI"].ToString();

            }
            catch (Exception ex) { }
            #endregion

            bn.NOILAMVIEC = dtTT_BenhNhan.Rows[0]["NOILAMVIEC"].ToString();
            bn.TENNGUOITHAN = dtTT_BenhNhan.Rows[0]["TENNGUOITHAN"].ToString();
            bn.DIACHINGUOITHAN = dtTT_BenhNhan.Rows[0]["DIACHINGUOITHAN"].ToString();
            bn.DIENTHOAINGUOITHAN = dtTT_BenhNhan.Rows[0]["DIENTHOAINGUOITHAN"].ToString();
            #endregion


            #region Thông tin khám bệnh
            //loadThongTinKhamBenh(dtTT_BenhNhan); 
            DataRow BN_Sua = dtTT_BenhNhan.Rows[0];

            if (cboBacSi.Visible == true)
            {
                try
                {
                    if (cboBacSi.SelectedItem != null)
                    {
                        bn.BACSYKHAM1 = "";
                        bn.BACSYKHAM2 = ((DataRowView)cboBacSi.SelectedItem).Row.ItemArray[2].ToString();
                        bn.BACSYYCID = ((DataRowView)cboBacSi.SelectedItem).Row.ItemArray[1].ToString();
                    }
                }
                catch(Exception ex) { }
            }

            bn.NGAYTIEPNHAN = BN_Sua["NGAYTIEPNHAN"].ToString();
            bn.MA_BHYT = BN_Sua["MA_BHYT"].ToString();
            bn.MA_KCBBD = BN_Sua["MA_KCBBD"].ToString();
            bn.MAKCBBD = BN_Sua["MA_KCBBD"].ToString();
            bn.TEN_KCBBD = BN_Sua["TEN_KCBBD"].ToString();

            // Chỉ các TH BHYT cần, nếu ko bỏ qua cũng được
            bn.BHYT_BD = BN_Sua["BHYT_BD"].ToString();
            bn.BHYT_KT = BN_Sua["BHYT_KT"].ToString();
            bn.DIACHI_BHYT = BN_Sua["DIACHI_BHYT"].ToString();
            if (BN_Sua.Table.Columns.Contains("DU5NAM6THANGLUONGCOBAN"))
                bn.NGAYDU5NAM = BN_Sua["DU5NAM6THANGLUONGCOBAN"].ToString();
            if (bn.NGAYDU5NAM == "0") bn.NGAYDU5NAM = "";

            bn.CHANDOANTUYENDUOI = BN_Sua["CHANDOANTUYENDUOI"].ToString();

            bn.DT_SINHSONG = BN_Sua["DT_SINHSONG"].ToString();
            bn.DOITUONGBENHNHANID = DOI_TUONG_ID; // bhyt / viện phí,...

            bn.DOITUONGDB = "";// cboMienGiam.SelectValue == null ? "" : cboMienGiam.SelectValue.ToString();  // id của đt miễn giảm


            Lay_MucHuong_BHYT(BN_Sua["MA_BHYT"].ToString(), BN_Sua["BHYT_LOAIID"].ToString(), BN_Sua["HINHTHUCVAOVIENID"].ToString()
                , bn.DT_SINHSONG );

            bn.MUCHUONG_NGT = hidMUCHUONG_NGT;// trả về khi tính mức hưởng
            bn.MUCHUONG = BN_Sua["MUC_HUONG"].ToString();// hoặc hidMUCHUONG
            bn.BHYT_DOITUONG_ID = hidBHYT_DOITUONG_ID;// trả về khi tính mức hưởng 


            bn.DVTHUKHAC = BN_Sua["THUKHAC"].ToString();
            bn.BHYT_LOAIID = BN_Sua["BHYT_LOAIID"].ToString();//đúng tuyến...
            bn.TKBHYT_LOAIID = BN_Sua["BHYT_LOAIID"].ToString();

            bn.MANOIGIOITHIEU = BN_Sua["MANOIGIOITHIEU"].ToString();
            bn.TKMANOIGIOITHIEU = BN_Sua["TKMANOIGIOITHIEU"].ToString();
            bn.TENNOIGIOITHIEU = BN_Sua["TENNOIGIOITHIEU"].ToString();

            bn.TIEPNHANID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("TIEPNHANID") == true) ? "" : BN_Sua["TIEPNHANID"].ToString();
            bn.BENHNHANID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("BENHNHANID") == true) ? "" : BN_Sua["BENHNHANID"].ToString();
            bn.KHAMBENHID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("KHAMBENHID") == true) ? "" : BN_Sua["KHAMBENHID"].ToString();
            bn.HOSOBENHANID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("HOSOBENHANID") == true) ? "" : BN_Sua["HOSOBENHANID"].ToString();

            bn.LOAITIEPNHANID = "1";// 0 nội, 1 ngoại trú

            bn.TRANGTHAIKHAMBENH = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("TRANGTHAIKHAMBENH") == true) ? "1" : BN_Sua["TRANGTHAIKHAMBENH"].ToString();// 1 chờ khám

            DataTable dtKHAMBENH = new DataTable();
            if (dtTT_BenhNhan.Columns.Contains("KHAMBENHID"))
            {
                dtKHAMBENH = RequestHTTP.getChiTiet_KhamBenh(dtTT_BenhNhan.Columns["KHAMBENHID"].ToString());
                bn.MABENHAN = (dtKHAMBENH == null || dtKHAMBENH.Rows.Count == 0 || dtKHAMBENH.Columns.Contains("MABENHAN") == false) ? ""
                    : dtKHAMBENH.Rows[0]["MABENHAN"].ToString();  // mã bệnh án
            }
            else
                bn.MABENHAN = "";

            bn.PHONGKHAMDANGKYID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("PHONGKHAMDANGKYID") == true) ? "" : BN_Sua["PHONGKHAMDANGKYID"].ToString();
            bn.MAUBENHPHAMID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("MAUBENHPHAMID") == true) ? "" : BN_Sua["MAUBENHPHAMID"].ToString();
            bn.DICHVUKHAMBENHID = !(BN_Sua != null && BN_Sua.Table.Columns.Contains("DICHVUKHAMBENHID") == true) ? "" : BN_Sua["DICHVUKHAMBENHID"].ToString();

            bn.DIAPHUONGID = BN_Sua["DIAPHUONGID"].ToString();

            // ưu tiên Lấy mã của đơn vị (tỉnh, huyện, xã) nhỏ nhất nếu như nó khác rỗng
            if (bn.HC_XAID != null && bn.HC_XAID != "") bn.BNDIAPHUONGID = bn.HC_XAID;
            else if (bn.HC_HUYENID != null && bn.HC_HUYENID != "") bn.BNDIAPHUONGID = bn.HC_HUYENID;
            else if (bn.HC_TINHID != null && bn.HC_TINHID != "") bn.BNDIAPHUONGID = bn.HC_TINHID;

            bn.SINHTHEBHYT = BN_Sua["SINHTHEBHYT"].ToString();
            bn.DAGIUTHEBHYT = "0";//BN_Sua["DAGIUTHEBHYT"].ToString();

            bn.UUTIENKHAMID = BN_Sua["UUTIENKHAMID"].ToString();
            if (bn.UUTIENKHAMID == "1") bn.UUTIENKHAMID = "3";
            if (BN_Sua.Table.Columns.Contains("DU5NAM6THANGLUONGCOBAN"))
                bn.DU5NAM6THANGLUONGCOBAN = BN_Sua["DU5NAM6THANGLUONGCOBAN"].ToString();
            if (BN_Sua.Table.Columns.Contains("TRADU6THANGLCB")) bn.TRADU6THANGLCB = BN_Sua["TRADU6THANGLCB"].ToString();
            if (BN_Sua.Table.Columns.Contains("HINHTHUCVAOVIENID")) bn.HINHTHUCVAOVIENID = BN_Sua["HINHTHUCVAOVIENID"].ToString();

            //// Chuyển viện 
            //if (dtChuyenVien.Rows.Count > 0)
            //{
            //    bn.CV_CHUYENVIEN_HINHTHUCID = dtChuyenVien.Rows[0]["ucHinhThucChuyen"].ToString();
            //    bn.CV_CHUYENVIEN_LYDOID = dtChuyenVien.Rows[0]["ucLyDoChuyen"].ToString();

            //    bn.CV_CHUYENDUNGTUYEN = dtChuyenVien.Rows[0]["rbtChuyen"].ToString();
            //    bn.CV_CHUYENVUOTTUYEN = bn.CV_CHUYENDUNGTUYEN == "1" ? "0" : "1";
            //}

            //{ KHAMBENHID: 3492,CHANDOANTUYENDUOI: ,MACHANDOANTUYENDUOI: ,MANOIGIOITHIEU: ,HINHTHUCVAOVIENID: 3,UUTIENKHAMID: 0,TIEPNHANID: 3803
            // ,NGAYTIEPNHAN: 17 / 11 / 2017 09:47,PHONGID: 214,DTBNID: 1,BENHNHANID: 3832,MABENHNHAN: BN00002483,TENBENHNHAN: TEST AHIHIH, HOSOBENHANID: 4006
            // ,NGAY_SINH: 12 / 12 / 2015,NAMSINH: 2015,TUOI: 23,BHYTID: 2545,MA_BHYT: TE1401100000277,BHYT_BD: 12 / 12 / 2015,BHYT_KT: 11 / 12 / 2021
            //        ,MA_KCBBD: 35148,DIACHI_BHYT: Xã Tân Hợp - Huyện Tân Kỳ-Nghệ An,BHYT_LOAIID: 1,DT_SINHSONG: 2,DU5NAM6THANGLUONGCOBAN: 0,TRADU6THANGLCB: 0
            //        ,QUYEN_LOI: null,MUC_HUONG: null,GIOITINHID: 2,NGHENGHIEPID: 3,DANTOCID: 25,QUOCGIAID: 0,SONHA: ,DIAPHUONGID: 4042317269,DIABANID: null
            //        ,TENDIAPHUONG: Xã Tân Hợp - Huyện Tân Kỳ-Nghệ An,NOILAMVIEC: ,NGUOITHAN: ,TENNGUOITHAN: ffffffffffff,DIENTHOAINGUOITHAN: ,DIACHINGUOITHAN: 
            //    ,DICHVUKHAMBENHID: 10247,DICHVUID: 1004,PHONGKHAMDANGKYID: 2558,TEN_KCBBD: Bệnh viện sản nhi Hà Nam, MAUBENHPHAMID: 11500,SOTHUTU: 1,TENNOIGIOITHIEU: 
            //    ,ORG_NAME: PK Mắt P.314,SLXN: 0,SLCDHA: 0,DIACHI: Xã Tân Hợp - Huyện Tân Kỳ-Nghệ An,THUKHAC: 0,SLCHUYENKHOA: 0,CONGKHAM: 1,TKMACHANDOANTUYENDUOI: 
            //    ,TKMANOIGIOITHIEU: ,TRANGTHAIKHAMBENH: 4,NGAYTHUOC: 01 / 01 / 1990 00:00:00,CHUADUYETKT: 0,BHYT_DV: 0,SUB_DTBNID: 0,NGAYMAUBENHPHAM: 20171117
            //        ,PHONGKHAMID: 214,SDTBENHNHAN: ,SINHTHEBHYT: 1}

            bn.BARCODE = hideBARCODE_BHYT;
            bn.COGIAYKS = "0";  //ckbTheTE.Checked ? "1" : "0";

            bn.DTBNID = BN_Sua["DTBNID"].ToString();
            bn.CHECKBHYTDV = "0";
            if (bn.DTBNID == "6")
            {
                bn.DTBNID = "1";
                bn.CHECKBHYTDV = "1";
            }
            bn.CHECKCONG = "0"; // ckbCheckBHYT.Checked ? "1" : "0";

            DataTable dtCauHinh = new DataTable();
            dtCauHinh = RequestHTTP.get_ajaxExecuteQueryO("NGT_STT_DT", DateTime.Now.ToString(Const.FORMAT_date1));
            bn.URLWEBSITE = dtCauHinh.Rows[0]["URLWEBSITE"].ToString();
            bn.TECHPASS = dtCauHinh.Rows[0]["TECHPASS"].ToString();
            bn.TECHUSER = dtCauHinh.Rows[0]["TECHUSER"].ToString();
            bn.TENQUAY = "";// lbQuay.Text;
            bn.STT_BD1 = "1";//txtstt_bd1.Text; // bv NT
            bn.STT_KT1 = "1";//txtstt_kt1.Text; // bv NT 
            bn.KCBBD = dtCauHinh.Rows[0]["TECHKCBBD"].ToString();
            bn.MAHONGHEO = "";// txtMADOITUONGNGHEO.Text;
            //Phần Hợp đồng: có cbo Hợp đồng, khởi tạo khi load form nếu được thiết lập khi gọi.
            bn.HOPDONGID = "";// ucHopDong.SelectValue;


            bn.DICHVUID = ID_YCKham; // ucYeuCauKham.SelectValue;
            bn.TKDICHVUID = bn.DICHVUID;
            bn.DICHVUKHAMID = bn.DICHVUID;
            bn.YEUCAUKHAM = Ten_YCKham; // ucYeuCauKham.Text;

            bn.PHONGKHAMID = ID_PKham; //  ucPhongKham.SelectValue;
            #endregion



            //thông tin thẻ BH

            if (maDKBD != "" && gtTheTu != "" && gtTheDen != "")
            {
                bn.BHYT_BD = gtTheTu;
                bn.BHYT_KT = gtTheDen;
                bn.NGAYDU5NAM = ngayDu5Nam;
                bn.DIACHI_BHYT = diaChi;
                bn.MA_KCBBD = maDKBD;
                bn.MAKCBBD = maDKBD;
            }


            // khi in ra thêm một số thông tin gồm giới tính, năm sinh, mã bệnh án, số điện thoại của bệnh nhân.
            // MABENHAN|GIOITINHID|NAMSINH|SDTBENHNHAN
            thong_tin = ((dtKHAMBENH == null || dtKHAMBENH.Rows.Count == 0 || dtKHAMBENH.Columns.Contains("MABENHAN") == false) ? "" : dtKHAMBENH.Rows[0]["MABENHAN"].ToString())
                + "|" + (dtTT_BenhNhan.Columns.Contains("GIOITINHID") ? dtTT_BenhNhan.Rows[0]["GIOITINHID"].ToString() : "")
                + "|" + (dtTT_BenhNhan.Columns.Contains("NAMSINH") ? dtTT_BenhNhan.Rows[0]["NAMSINH"].ToString() : "")
                + "|" + (dtTT_BenhNhan.Columns.Contains("SDTBENHNHAN") ? dtTT_BenhNhan.Rows[0]["SDTBENHNHAN"].ToString() : "")
                + "|" + (dtTT_BenhNhan.Columns.Contains("DIACHI") ? dtTT_BenhNhan.Rows[0]["DIACHI"].ToString() : "")
                ;



            return bn;
        }
        private string LuuBN(BN_TiepNhan bn)
        {
            string ret = submitBenhNhanTiepNhan(bn);
            string[] retArr = ret.Split(',');

            if (retArr.Length > 1) //thành công
            {
                //MessageBox.Show("thành công");  18057,0000827,15468,16741,17256,16938,,315047,1,BA19010006,0,

                //if (Const.local_user.HOSPITAL_ID == "951") // BV Nhiệt đới
                //    return "ret_true" + ret;//thành công
                //else
                //    return "ret_true";//thành công

                return "ret_true" + ret;//thành công
            }
            else //lỗi
            {
                if (ret == "the_ko_hop_le")
                {
                    //setErrValidate('txtMA_BHYT');
                    return ("Thẻ BHYT không hợp lệ");

                }
                else if (ret == "trung_the")
                {
                    //setErrValidate('txtMA_BHYT");
                    return ("Trùng thẻ BHYT");
                }
                else if (ret == "da_tiepnhan_pk")
                {
                    //setErrValidate('txtMA_BHYT");
                    return ("Bệnh nhân đã tiếp nhận vào phòng khám trong ngày");
                }
                else if (ret == "dakhamtrongngay")
                {
                    //setErrValidate('txtMA_BHYT");
                    return ("Bệnh nhân đã đăng ký khám trong ngày");
                }
                else if (ret == "kocodvcon")
                {
                    return ("Không có dịch vụ con trong goi");
                }
                else if (ret == "vi_pham_tt_kham")
                {
                    return ("Bệnh nhân đang khám hoặc đang điều trị, không tiếp nhận lại được");
                }
                else if (ret == "dvdathanhtoan")
                {
                    return ("Dịch vụ yêu cầu đã thanh toán, phải hủy hóa đơn nếu muốn cập nhật dịch vụ khác");
                }
                else if (ret == "loitenbn")
                {
                    return ("Tên bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại");
                }
                //else if (ret == "loidiachibn")
                //{
                //    ucThongTinHanhChinh1.txtDcBN.Focus();
                //    return ("Địa chỉ bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại");
                //}
                else if (ret == "loidiachibhyt")
                {
                    return ("Địa chỉ BHYT bệnh nhân có chứa ký tự đặc biệt, hãy kiểm tra lại");
                }
                else if (ret == "kocapnhatbncu")
                {
                    return ("Không thể cập nhật lại thông tin bệnh nhân tiếp đón ngày hôm trước");
                }


                //thêm các mã lỗi ngàyy 21/06
                else if (ret.Length > 21 && ret.Substring(0, 20) == "chantiepnhannhapkhoa")
                {
                    string[] retts = ret.Split('@');
                    return ("Bệnh nhân này đang chuẩn bị nhập khoa, không được tiếp nhận. Mã bệnh án liên quan: " + retts[1]);
                }
                else if (ret.Length > 16 && ret.Substring(0, 15) == "dieutringoaitru")
                {
                    //    string[] retts = ret.Split('@');
                    //var msggg = $("#hidHisId").val() == retts[1]
                    //        ? "Bệnh nhân chưa kết thúc khám bệnh tại đơn vị này. "
                    //                : "Bệnh nhân chưa kết thúc khám bệnh tại đơn vị: " + retts[2];
                    return ("Bệnh nhân chưa kết thúc khám bệnh");
                }
                else if (ret.Length > 14 && ret.Substring(0, 13) == "dieutrinoitru")
                {
                    //    string[] retts = ret.split('@');
                    //var msggg = $("#hidHisId").val() == retts[1]
                    //        ? "Bệnh nhân chưa kết thúc điều trị nội trú tại đơn vị này. "
                    //                : "Bệnh nhân chưa kết thúc điều trị nội trú tại đơn vị: " + retts[2];
                    return ("Bệnh nhân chưa kết thúc điều trị nội trú");
                }
                else if (ret.Length > 16 && ret.Substring(0, 15) == "vi_pham_tt_kham")
                {
                    //string[] retts = ret.Split('@');
                    //var msggg = $("#hidHisId").val() == retts[1]
                    //        ? "Bệnh nhân chưa kết thúc khám tại đơn vị này. "
                    //                : "Bệnh nhân chưa kết thúc khám tại đơn vị: " + retts[2];
                    return ("Bệnh nhân chưa kết thúc khám");
                }
                else if (ret.Length > 13 && ret.Substring(0, 12) == "dieutribvcon")
                {
                    //    string[] retts = ret.split('@');
                    //var msggg = $("#hidHisId").val() == retts[1]
                    //        ? "Bệnh nhân đang điều trị ngoại trú / nhập viện tại đơn vị này. "
                    //                : "Bệnh nhân đang điều trị ngoại trú / nhập viện tại đơn vị: " + retts[2];
                    return ("Bệnh nhân đang điều trị ngoại trú / nhập viện");
                }
                else if (ret == "kothuphieudc")
                {
                    return ("Tiếp nhận bệnh nhân thành công nhưng chưa thu được tiền công khám, bệnh nhân qua quầy viện phí thanh toán");
                }
                else if (ret == "hetphieuthu")
                {
                    return ("Tiếp nhận bệnh nhân thành công nhưng chưa thu được tiền công khám, do hết phiếu thu/hóa đơn");
                }


                else
                {
                    return ("Cập nhật thông tin không thành công (mã lỗi: " + ret + ")");
                }
            }
            return "";
        }
        public string submitBenhNhanTiepNhan(BN_TiepNhan bn)
        {
            string ret = "";
                string strJson = Newtonsoft.Json.JsonConvert.SerializeObject(bn);
                  string request = "{\"func\":\"ajaxCALL_SP_S\",\"params\":[\"NGT01T002.LUUTT\",\""
                    + strJson.Replace("\\", "\\\\").Replace("\"", "\\\"")
                    + "$[]\"],\"uuid\":\""
                    + Const.local_user.UUID
                    + "\",\"code\":\"thu@nnc\"}";
            try
            {
                string resp = RequestHTTP.sendRequest(request);

                Func.set_log_file("Đký khám: request=" + request + " | resp=" + resp);

                ResponsObj resultSet = new ResponsObj();

                resultSet = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponsObj>(resp, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
                ret = resultSet.result;
            }
            catch (Exception ex)
            {
                Func.set_log_file("Đký khám: request=" + request + " | Exception=" + ex);
            }
            Func.set_log_file("");
            return ret;
            //13 biến: RETURN b_khambenhid||','||vmabenhnhan||','||b_phongkhamdangkyid||','||b_tiepnhanid||','||b_hosobenhanid||','||b_benhnhanid||','||b_bhytid||','||b_maubenhphamid||','||b_dichvukhambenhid || ',' || b_mahosobenhan || ',' || b_thukhac || ',' || r_socapcuu || ',' || b_sothutupkdk;	
            
        }

        public static DataTable getMucHuong_BHYT(string phongid, string madoituong, string tuyen, string objBH, string hinhthucvaovienid)
        {
            // {"func":"ajaxCALL_SP_O","params":["COM.MUCHUONG.BHYT","3559$TE1$1$1$3",0],"uuid":"c0a512e7-e407-4eb9-a262-09bfda6a1d42"}
            DataTable dt = new DataTable();
            try
            {
                string request = RequestHTTP.makeRequestParam("ajaxCALL_SP_O", new String[] { "COM.MUCHUONG.BHYT"
                    , phongid + "$" + madoituong + "$" + tuyen + "$"+ objBH + "$" + hinhthucvaovienid }, new int[] { 0 } );
                string resp = RequestHTTP.sendRequest(request);

                ResponsObj resultSet = new ResponsObj();
                resultSet = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponsObj>(resp, new Newtonsoft.Json.JsonSerializerSettings { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });

                dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(resultSet.result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();
            }
            catch (Exception ex)
            {
            }
            return dt;
            // {"result": "[{\n\"MUCHUONG_NOI\": \"100\",\n\"MUCHUONG_NGOAI\": \"100\",\n\"BHYT_DOITUONG_ID\": \"98\"}]","out_var": "[]","error_code": 0,"error_msg":""}
        }
        public static bool Tinh_Tuoi(string NGAY_SINH, string NAMSINH, string TUOI_in, out string TUOI, out string DVTUOI)
        {
            TUOI = "";
            DVTUOI = "";
            try
            {
                if (NGAY_SINH.Trim() == "")
                {
                    TUOI = (DateTime.Now.Year - Convert.ToInt16(NAMSINH.Trim()) + 1).ToString();
                    DVTUOI = "1"; // đv là 1:tuổi       2: tháng     3: ngày     4: giờ
                    return true;
                }

                DateTime sys_dtime = Func.getDatetime_Short(DateTime.Now);
                DateTime dtNhap;
                if (DateTime.TryParseExact(NGAY_SINH, Const.FORMAT_date1, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None
                                , out dtNhap) == false) return false;

                int chenhNam = sys_dtime.Year - dtNhap.Year;
                int chenhThang = chenhNam * 12 + sys_dtime.Month - dtNhap.Month;

                if (chenhThang >= 36)// hiển thị số tuổi
                {
                    TUOI = (chenhNam).ToString();
                    DVTUOI = "1"; // đv là 1:tuổi       2: tháng     3: ngày     4: giờ
                    return true;
                }
                else
                {
                    TimeSpan ts = TimeSpan.FromTicks(sys_dtime.Ticks - dtNhap.Ticks);
                    Double chenhNgay = ts.TotalDays;

                    if (chenhNgay >= 30)// hiển thị số tháng
                    {
                        if (chenhThang <= 0) chenhThang = 1;
                        TUOI = (chenhThang).ToString();
                        DVTUOI = "2"; // đv là 1:tuổi       2: tháng     3: ngày     4: giờ
                        return true;
                    }
                    else
                    {
                        if (chenhNgay <= 0) chenhNgay = 1;
                        TUOI = (chenhNgay).ToString();
                        DVTUOI = "3"; // đv là 1:tuổi       2: tháng     3: ngày     4: giờ
                        return true;
                    }
                }
            }
            catch(Exception ex) {
                // lỗi
                Func.set_log_file("LOI6: tính tuổi: " + NGAY_SINH + "|" + NAMSINH);
                //để mặc định
                TUOI = TUOI_in;
                DVTUOI = "1";
                return false;
            }
        }

        string hidMUCHUONG_NGT = "";
        string hidMUCHUONG = "";
        string hidBHYT_DOITUONG_ID = "";
        private void Lay_MucHuong_BHYT(string txtSoThe, string tuyen, string hinhthucvaovienid, string DT_SINHSONG)   // 
        {
            // {"func":"ajaxCALL_SP_O","params":["COM.MUCHUONG.BHYT","27$DN4$1$1$3",0],"uuid":"730afe7f-703a-4333-b158-897b7bf37043"}
            try
            {
                //var hinhthucvaovienid = $.find("[name='radHINHTHUCVAOVIENID']:checked")[0].value;
                //var objBH = new Object();
                //objBH.MATHE = $("#txtMA_BHYT").val();
                //objBH.DOITUONGSONG = $('#cboDT_SINHSONG').val();
                //var data_ar = jsonrpc.AjaxJson.ajaxCALL_SP_O("COM.MUCHUONG.BHYT", 
                //    i_phongid + '$' + i_madoituong.toUpperCase() + '$' + i_tuyen + '$' + JSON.stringify(objBH) + '$' + hinhthucvaovienid);

                // BN00109820
                object objBH = new
                {
                    MATHE = txtSoThe,
                    DOITUONGSONG = DT_SINHSONG
                };
                string str_objBH =  Newtonsoft.Json.JsonConvert.SerializeObject(objBH);

                string phongid = "7806"; 

                string madoituong = txtSoThe.Substring(0, 3).ToUpper();

                if (Func.Parse(tuyen) < 0) return; //BHYT_LOAIID": "1", đúng tuyến, 2 đúng tuyến gt, 3 đúng ccuu, 4 trái

                DataTable dt = getMucHuong_BHYT(phongid, madoituong, tuyen, str_objBH.Replace("\"", "\\\""), hinhthucvaovienid); ;
                if (dt.Rows.Count > 0)
                {
                    hidMUCHUONG = "Ngoại (" + dt.Rows[0]["MUCHUONG_NGOAI"].ToString() + "%) - Nội ("
                        + dt.Rows[0]["MUCHUONG_NOI"].ToString() + "%)";
                    hidMUCHUONG_NGT = dt.Rows[0]["MUCHUONG_NGOAI"].ToString();
                    hidBHYT_DOITUONG_ID = dt.Rows[0]["BHYT_DOITUONG_ID"].ToString();
                    // // {"result": "[{\n\"MUCHUONG_NOI\": \"100\",\n\"MUCHUONG_NGOAI\": \"100\",\n\"BHYT_DOITUONG_ID\": \"98\"}]
                }
            }
            catch (Exception ex) { }
        }
        #endregion


        #region Hàm load lưới các button trên form
        private void Load_Button(DataTable dt, string id_select, string name_select)
        {
            id = id_select;
            name = name_select;

            int number = 6;// 3;
            int pading = 15;// 100;
            int x0 = pading; int y0 = pading;
            int w = (Width - (number + 3) * pading) / number;
            int h = w * 70 / 100;


            panel2.Controls.Clear();
            

            for (int k = 0; k < dt.Rows.Count; k++)
            {
                int i = (k) / number;
                int j = k - number * i;

                Button btn = new Button();


                btn.Font = new System.Drawing.Font("Tahoma", 17F);
                //btn.UseFont = true;
                //btn.Appearance.Options.UseTextOptions = true;
                //btn.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                //btn.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
                //btn.LookAndFeel.UseDefaultLookAndFeel = false;
                btn.Size = new System.Drawing.Size(w, h);
                btn.Click += new EventHandler(btnSelect);
                btn.BackColor = Color.LightBlue;
                // btn.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
                //btn.BorderColor = System.Drawing.Color.Gray;

                btn.Location = new System.Drawing.Point(x0 + j * (w + pading), y0 + i * (h + pading));
                //btn.Text = dt.Rows[k]["TEN_HIENTHI"].ToString() + "\r\n(" + dt.Rows[k]["STT"].ToString() + ")"; // Name

                string ten = dt.Rows[k]["col2"].ToString();
                if (ten.IndexOf("]") > -1) ten = ten.Substring(ten.IndexOf("]") + 1).Trim();
                btn.Text = ten;// + "\r\n(" + dt.Rows[k]["STT"].ToString() + ")"; // Name
                btn.Tag = dt.Rows[k]["col1"].ToString(); // ID 


                if (CHON == 1)
                {
                    if (dtTT_BenhNhan.Columns.Contains("DICHVUID") && dtTT_BenhNhan.Rows[0]["DICHVUID"].ToString() == dt.Rows[k]["col1"].ToString())
                        btn.BackColor = System.Drawing.Color.Khaki;//DeepSkyBlue;
                }
                else if (CHON == 2)
                {
                    if (dtTT_BenhNhan.Columns.Contains("PHONGKHAMID") && dtTT_BenhNhan.Rows[0]["PHONGKHAMID"].ToString() == dt.Rows[k]["col1"].ToString())
                        btn.BackColor = System.Drawing.Color.Khaki;
                }


                panel2.Controls.Add(btn);


                //Label lb_YCK = new Label();
                //lb_YCK.AutoSize = true;   
                //lb_YCK.Text = "ấdasdasdasd"; lb_YCK.Location = new System.Drawing.Point(x0 + j * (w + pading), y0 + i * (h + pading));
                //Controls.Add(lb_YCK);
            }

            //int hh = (dt.Rows.Count-1+ number) / number;
            //btnSubmit.Location = new System.Drawing.Point(this.Size.Width/2 - btnSubmit.Size.Width-5, y0 + hh * (h + pading));
            //btnBack.Location = new System.Drawing.Point(this.Size.Width / 2 + 5, y0 + hh * (h + pading));

            if (Hien_Thi_Chon_Bac_Si 
                && (Const.L1_BV_DEFAULT == 4 || (Const.L1_BV_DEFAULT == 0 && Const.default_test == 4))
                ) //Nhiệt đới
            {
                if (CHON == 1)
                {
                    panel_BacSi.Visible = false;
                    cboBacSi.Visible = false;
                    btnXoaBS.Visible = false;
                    lbTieude1.Text = "Chọn yêu cầu khám".ToUpper();
                }
                else if (CHON == 2)
                {
                    cboBacSi.Visible = true;
                    btnXoaBS.Visible = true;
                    panel_BacSi.Visible = true;
                    lbTieude1.Text = "CHỌN BÁC SĨ";
                    cboBacSi.SelectedIndex = -1;

                    lbTieude2.Text = "Chọn phòng khám".ToUpper();
                }
            }
            else
            {
                panel_BacSi.Visible = false;
                cboBacSi.Visible = false;
                btnXoaBS.Visible = false;
                if (CHON == 1)
                {
                    //this.Text = "Chọn yêu cầu khám"; 
                    lbTieude1.Text = "Chọn yêu cầu khám".ToUpper();
                }
                else if (CHON == 2)
                {
                    //this.Text = "Chọn phòng khám"; 
                    lbTieude1.Text = "Chọn phòng khám".ToUpper();
                }
            }
            //label1.Text = this.Text.ToUpper();

            if (id == "") btnSubmit.Enabled = false;


            //Controls.Add(btnSubmit);
            //Controls.Add(btnBack);

            panel2.AutoScroll = true;
        }
        string ID_YCKham;
        string Ten_YCKham;
        string ID_PKham;
        string Ten_PKham;

        string id;
        string name;
        private void btnSelect(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            id = btn.Tag.ToString();
            name = btn.Text.ToString();

            btnSubmit.Enabled = true;

            btnSubmit_Click(null, null);
        }
        #endregion


        protected EventHandler ReturnData;
        public void setReturnData(EventHandler eventReturnData)
        {
            ReturnData = eventReturnData;
        }
  
        private void cboBacSi_Click(object sender, EventArgs e)
        {
            cboBacSi.DroppedDown = true;
        }

        private void btnXoaBS_Click(object sender, EventArgs e)
        {
            cboBacSi.SelectedIndex = -1;
        }
    }


}
