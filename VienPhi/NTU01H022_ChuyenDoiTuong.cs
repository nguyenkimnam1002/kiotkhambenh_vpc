using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.VienPhi
{
    public partial class NTU01H022_ChuyenDoiTuong : DevExpress.XtraEditors.XtraForm
    {
        public NTU01H022_ChuyenDoiTuong()
        {
            InitializeComponent();
        }
        private void NTU01H022_ChuyenDoiTuong_Load(object sender, EventArgs e)
        {
            _initControl();
            _bindEvent();
        }

        bool tuDongFillBhxh = false;
        string func_kt = "";
        private void _initControl()
        {
            //          //START -- hongdq -- HISL2TK-474 -- 18052018
            //          var pars = ['NTU_CHUYEN_DT_POPUP'];
            //          data_ar_2_ch_chuyendt = jsonrpc.AjaxJson.ajaxCALL_SP_S('COM.CAUHINH', pars_ch_chuyendt.join('$'));

            //          var _colDKKBD = "Mã bệnh viện,VALUE,30,0,f,l;Tên bệnh viện,NAME,70,0,f,l";
            //          var _colICD = "Mã bệnh,ICD10CODE,30,0,f,l;Tên bệnh,ICD10NAME,70,0,f,l";
            //          ComboUtil.initComboGrid("txtTKMANOIGIOITHIEU", "NGTBV.002",[], "600px", _colDKKBD, "txtTKMANOIGIOITHIEU=VALUE,cboMANOIGIOITHIEU=VALUE:NAME");
            ResponsList responses  = RequestHTTP.get_ajaxExecuteQueryPaging("NGTBV.002", 1, 100000, new string[] { }, new string[] { }, "");
             
            DataTable dt_cboMANOIGIOITHIEU = new DataTable();
            dt_cboMANOIGIOITHIEU = MyJsonConvert.toDataTable(responses.rows);

            cboMANOIGIOITHIEU.setData(dt_cboMANOIGIOITHIEU, "VALUE", "NAME"); 
            //cboMANOIGIOITHIEU.setEvent_Enter(ucDKKCB_KeyDown);
            cboMANOIGIOITHIEU.setColumn("RN", -1, "", 0);
            cboMANOIGIOITHIEU.setColumn("VALUE", 0, "Mã bệnh viện", 50);
            cboMANOIGIOITHIEU.setColumn("NAME", 1, "Tên bệnh viện", 0);

            //          ComboUtil.init("txtTKMACHANDOANTUYENDUOI", "NT.008",[], "600px", _colICD, function(event, ui) {
            //          var str = txtCHANDOANTUYENDUOI").val();
            //          if (str != '')
            //              str += ";";
            //       txtCHANDOANTUYENDUOI").val(str + ui.item.ICD10CODE + "-" + ui.item.ICD10NAME);
            //          return false;
            //      });
            DataTable dt = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_DsBenh);
            txtCHANDOANTUYENDUOI.setData(dt, "ICD10CODE", "ICD10NAME");
            // ucCHANDOANTUYENDUOI.setEvent_Enter(ucCHANDOANTUYENDUOI_KeyEnter);
            txtCHANDOANTUYENDUOI.setColumn("RN", -1, "", 0);
            txtCHANDOANTUYENDUOI.setColumn("ICD10CODE", 0, "Mã bệnh", 0);
            txtCHANDOANTUYENDUOI.setColumn("ICD10NAME", 1, "Tên bệnh", 0);

            //ucCHANDOANTUYENDUOI.setEvent_Check(ucCHANDOANTUYENDUOI_Check);
            txtCHANDOANTUYENDUOI.btnReset.Visible = true;
            //ucCHANDOANTUYENDUOI.btnEdit.Visible = true;
            txtCHANDOANTUYENDUOI.btnReset.Text = "Xóa bệnh";
            //ucCHANDOANTUYENDUOI.btnEdit.Text = "Sửa BP";



            //      validator = new DataValidator("divChuyenDoiTuong");
            //      type = getParameterByName('quyentiepnhan', window.location.search.substring(1));
            type = "null";
            if (type == "1")
            {
                chkBH5NAM.Enabled = false;
                chkSINHTHETE.Enabled = false;
                txtMABHYT.Enabled = false;
                chkNOTHE.Enabled = true;

                //chkBH5NAM").attr("disabled", true);
                //chkSINHTHETE").attr("disabled", true);
                //txtMABHYT').attr("disabled", true);
                //chkNOTHE").prop("disabled", false);
            }

            //cboLOAITIEPNHANID").focus();

            //		// lấy tài khoản công đơn vị.
            //		var _parram = ["1"];
            //var data_ar = jsonrpc.AjaxJson.ajaxCALL_SP_O("NGT02K047.TTTK", _parram.join('$'));
            //		if(data_ar != null && data_ar.length > 0){
            //			i_u = data_ar[0].I_U;
            //			i_p = data_ar[0].I_P;
            //			i_macskcb = data_ar[0].I_MACSKCB;
            //		}

            string _autoFill = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "NGT_TUDONGFILL_BHXH");
            if (_autoFill == "1")
            {
                tuDongFillBhxh = true;
            }

            config_ndkkcbbd = Func.Parse(RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_NOI_KHAM_CHUA_BENH_BAN_DAU"));


            #region Khởi tạo control tab2
            tabPane1.SelectedPage = tabNavigationPage2;

            chkKHONGCHUYEN_DICHVU.Checked = true;

            string _check_the_bhyt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "CHECK_THE_BHYT_TUCONG");
            if (_check_the_bhyt == "1")
            {
                chkCHECKCONG.Checked = true;
                layoutControlItem30.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem31.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem32.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                chkCHECKCONG.Checked = false;
                layoutControlItem30.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem31.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem32.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            config = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_CHUYENDOITUONG_DICHVU");
            if (config != "1")
            {
                layoutNgayApDung.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutKhongChuyenDV.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }

            DataTable gioiTinhDT = RequestHTTP.get_ajaxExecuteQuery("NT.0010", new string[] { "[0]" }, new string[] { "1" });
            cboGIOITINHID.setData(gioiTinhDT, 0, 1);
            cboGIOITINHID.SelectIndex = 0;
            cboGIOITINHID.setColumn(0, false);

            isDtkhaibao = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "HIS_SUDUNG_DOITUONG_KHAIBAO") == "1" ? true : false;
            if (isDtkhaibao)
            {
                DataTable doituongBNDT = RequestHTTP.get_ajaxExecuteQuery("NT.007.01", new string[] { }, new string[] { });
                cboDOITUONGBENHNHANID.setData(doituongBNDT, 0, 1);
                cboDOITUONGBENHNHANID.setColumn(0, false);

                cboDOITUONGBENHNHANID.SelectValue = "2";
                //cboDOITUONGBENHNHANID").find("option[extval0='2']").attr("selected", "selected");       // bhyt ngoai nganh					
            }
            else
            {
                DataTable doituongBNDT = Common.RequestHTTP.get_ajaxExecuteQuery("NT.007", new string[] { "[S0]", "[S1]", "[S2]", "[S3]" }
                   , new String[] { Const.local_user.HOSPITAL_ID, Const.local_user.USER_ID, Const.local_user.USER_GROUP_ID, Const.local_user.PROVINCE_ID });
                //ComboUtil.getComboTag("cboDOITUONGBENHNHANID", "NT.007",[],"","","sql",'', function()

                cboDOITUONGBENHNHANID.setData(doituongBNDT, 0, 1);
                cboDOITUONGBENHNHANID.setColumn("col1", -1, "", 0);

                cboDOITUONGBENHNHANID.SelectValue = "1";
            }
            // END SONDN 010118

            //ComboUtil.getComboTag("cboMAVUNGID","NT.0010",[{"name":"[0]","value":"76"}],'', {value:'',text:'Chọn'},"sql");
            DataTable dt_cboMAVUNGID = RequestHTTP.get_ajaxExecuteQuery("NT.0010", new string[] { "[0]" }, new string[] { "76" });
            DataRow dr = dt_cboMAVUNGID.NewRow();
            dr[0] = "";
            dr[1] = "Chọn";
            if (dt_cboMAVUNGID.Rows.Count > 0) dt_cboMAVUNGID.Rows.InsertAt(dr, 0);
            cboMAVUNGID.setData(dt_cboMAVUNGID, 0, 1);
            cboMAVUNGID.setColumnAll(false);
            cboMAVUNGID.setColumn(1, true);

            //ComboUtil.getComboTag("cboTUYENID","COM.LOAIBHYT",[],"", {value:'',text:'Chọn'},"sql");
            DataTable dt_cboTUYENID = RequestHTTP.get_ajaxExecuteQuery("COM.LOAIBHYT", new string[] { }, new string[] { });
            DataRow dr2 = dt_cboTUYENID.NewRow();
            dr2[0] = "";
            dr2[1] = "Chọn";
            if (dt_cboTUYENID.Rows.Count > 0) dt_cboTUYENID.Rows.InsertAt(dr2, 0);
            cboTUYENID.setData(dt_cboTUYENID, 0, 1);
            cboTUYENID.setColumnAll(false);
            cboTUYENID.setColumn(1, true);

            //ComboUtil.initComboGrid("txtTKDKKBBD","NT.009",[],"800px", _colDKKCBBD, "txtTKDKKBBD=BENHVIENKCBBD,cboDKKBBDID=BENHVIENKCBBD:TENBENHVIEN");
            DataTable dt_TKDKKBBD = RequestHTTP.Cache_ajaxExecuteQueryPaging(true, Const.tbl_NoiDKKCB);

            //ucDKKCB.setEvent(cboTinh_SelectedIndexChanged);
            cboDKKBBDID.setData(dt_TKDKKBBD, "BENHVIENKCBBD", "TENBENHVIEN");
            //ucDKKCB.setEvent_Enter(ucDKKCB_KeyDown);
            cboDKKBBDID.setColumn("RN", -1, "", 0);
            cboDKKBBDID.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 35);
            cboDKKBBDID.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
            cboDKKBBDID.setColumn("DIACHI", 2, "Địa chỉ", 0);

            //   ComboUtil.initComboGrid("txtTKNOICD","NT.009",[],"800px", _colDKKCBBD, "txtTKNOICD=BENHVIENKCBBD,cboNOICDID=BENHVIENKCBBD:TENBENHVIEN");
            cboNOICD.setData(dt_TKDKKBBD, "BENHVIENKCBBD", "TENBENHVIEN");
            //txtTKNOICD.setEvent_Enter(ucDKKCB_KeyDown);
            cboNOICD.setColumn("RN", -1, "", 0);
            cboNOICD.setColumn("BENHVIENKCBBD", 0, "Mã bệnh viện", 35);
            cboNOICD.setColumn("TENBENHVIEN", 1, "Tên bệnh viện", 0);
            cboNOICD.setColumn("DIACHI", 2, "Địa chỉ", 0);


            //ComboUtil.getComboTag("cboHINHTHUCVAOVIENID","NT.0010",[{"name":"[0]","value":"4"}], "", "","sql");
            DataTable dt_cboHINHTHUCVAOVIENID = RequestHTTP.get_ajaxExecuteQuery("NT.0010", new string[] { "[0]" }, new string[] { "4" });
            cboHINHTHUCVAOVIENID.setData(dt_cboHINHTHUCVAOVIENID, 0, 1);
            cboHINHTHUCVAOVIENID.SelectIndex = 0;
            cboHINHTHUCVAOVIENID.setColumn(0, false);


            //ComboUtil.getComboTag("cboDOITUONGDB", "DMC.DTDACBIET", [], "", {extval: true, value:0, text:''}, "sql", "","");
            DataTable dt_cboDOITUONGDB = RequestHTTP.Cache_ajaxExecuteQuery(true, Const.tbl_DTMienGiam);
            DataRow dr3 = dt_cboDOITUONGDB.NewRow();
            dr3[0] = "";
            dr3[1] = "";
            if (dt_cboDOITUONGDB.Rows.Count > 0) dt_cboDOITUONGDB.Rows.InsertAt(dr3, 0);
            cboDOITUONGDB.setData(dt_cboDOITUONGDB, "col1", "col2");
            //cboMienGiam.setEvent_Enter(cboMienGiam_Enter);
            cboDOITUONGDB.setColumnAll(false);
            cboDOITUONGDB.setColumn(1, true);

            Show_Hide_divChuyenTuyen();

            #endregion
            #region Khởi tạo control tab1
            tabPane1.SelectedPage = tabNavigationPage1;

            //cboLOAITIEPNHANID
            DataTable dt_cboLOAITIEPNHANID = Func.getTableEmpty(new string[] { "col1", "col2" });
            dt_cboLOAITIEPNHANID.Rows.Add("-1", "Chọn loại tiếp nhận");
            dt_cboLOAITIEPNHANID.Rows.Add("0", "Điều trị nội trú");
            dt_cboLOAITIEPNHANID.Rows.Add("1", "Khám bệnh");
            dt_cboLOAITIEPNHANID.Rows.Add("3", "Điều trị ngoại trú");
            cboLOAITIEPNHANID.setData(dt_cboLOAITIEPNHANID, 0, 1);
            cboLOAITIEPNHANID.setColumn(0, false);
            cboLOAITIEPNHANID.SelectIndex = 0;

            // func_kt = getParameterByName('ketoan',window.location.search.substring(1));
            func_kt = "null";
            if (func_kt == "1")
            {
                i_cdt = true;
                layoutKhoa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                DataTable dt_cboKHOAID = RequestHTTP.get_ajaxExecuteQuery("DMC04.03", new string[] { }, new string[] { });
                if (dt_cboKHOAID.Rows.Count > 0) dt_cboKHOAID.Rows.Add("", "Chọn khoa");
                cboKHOAID.setData(dt_cboKHOAID, 0, 1);
                cboKHOAID.setColumn(0, false);
                //ComboUtil.getComboTag("cboKHOAID", "DMC04.03",[], "",{ value: 0, text: 'Chọn khoa'},"sql");
            }
            #endregion

            //if(func_kt != '1')
            {
                tabPane1.SelectedPage = tabNavigationPage2;
                loadGridData(1, null);
                tabPane1.SelectedPage = tabNavigationPage1;
            }



            dtChuyenVien = new DataTable();
            dtChuyenVien.Columns.Add("ucBenhVien");
            dtChuyenVien.Columns.Add("ucCDTD");
            dtChuyenVien.Columns.Add("ucHinhThucChuyen");
            dtChuyenVien.Columns.Add("ucLyDoChuyen");
            dtChuyenVien.Columns.Add("rbtChuyen");
            dtChuyenVien.Rows.Add(
                ""
                , ""
                , ""
                , ""
                , "");
        }
        DataTable dtChuyenVien;


        #region EVENT

        private void _bindEvent()
        {
            cboLOAITIEPNHANID.setEvent(cboLOAITIEPNHANID_Change);

            cboMAVUNGID.setEvent(cboMAVUNGID_Change);


            ucGrid_DsBenhNhan.setEvent(loadGridData);
            ucGrid_DsBenhNhan.SetReLoadWhenFilter(true);
            //ucGrid_DsBenhNhan.setEvent_FocusedRowChanged(viewDetail);
            //2 hàm dưới để load grid ra, mặc định chọn view chi tiết vào row đầu tiên.
            ucGrid_DsBenhNhan.gridView.OptionsBehavior.Editable = false;
            ucGrid_DsBenhNhan.gridView.Click += viewDetail;

            GridDataDichVu.SetReLoadWhenFilter(true);
            GridDataDichVu.setEvent(loadGridDataDichVu);

            GridChuyenDoiTuong.SetReLoadWhenFilter(true);
            GridChuyenDoiTuong.setEvent(loadGridChuyenDoiTuong);

            // Đối tượng
            cboDOITUONGBENHNHANID.setEvent(cboDOITUONGBENHNHANID_Change);

            //Tuyến
            cboTUYENID.setEvent(cboTUYENID_Change);

            //Vào từ
            cboHINHTHUCVAOVIENID.setEvent(cboHINHTHUCVAOVIENID_Change);
        }
        private void cboLOAITIEPNHANID_Change(object sender, EventArgs e)
        {
            loadGridData(1, null);
        }
        private void cboMAVUNGID_Change(object sender, EventArgs e)
        {
            //        if (cboMAVUNGID').val() != null && cboMAVUNGID').val() != ''){
            //divTuvien').removeClass('required');
            //divCdtd').removeClass('required');
            //        }
        }
        private void cboDOITUONGBENHNHANID_Change(object sender, EventArgs e)
        {
            _clearObjectBHYT(cboDOITUONGBENHNHANID.SelectValue != "1");
            //divNOICD').removeClass('required');

            if (type == "1")
            {
                chkBH5NAM.Enabled = false;
                chkSINHTHETE.Enabled = false;
                txtMABHYT.Enabled = false;
                //chkBH5NAM").attr("disabled", true);
                //chkSINHTHETE").attr("disabled", true);
                //txtMABHYT').attr("disabled", true);
                if (cboDOITUONGBENHNHANID.SelectValue == "1")
                {
                    //chkNOTHE").prop("disabled", false);
                    chkNOTHE.Enabled = true;
                }
                else
                {
                    //chkNOTHE").prop("disabled", true);
                    chkNOTHE.Enabled = false;
                }
            }
            else
            {
                if (cboDOITUONGBENHNHANID.SelectValue == "1")
                {
                    chkBH5NAM.Enabled = true;
                    chkSINHTHETE.Enabled = true;
                    //chkBH5NAM.Enabled = true;
                    //chkSINHTHETE.Enabled = true;
                }
                else
                {
                    chkBH5NAM.Enabled = true;
                    chkSINHTHETE.Enabled = true;
                    //chkBH5NAM").attr("disabled", true);
                    //chkSINHTHETE").attr("disabled", true);
                }
                //chkNOTHE").prop("disabled", true);
                chkNOTHE.Enabled = false;
            }
        }
        string pars_ch_chuyendt = "NTU_CHUYEN_DT_POPUP";
        private void cboTUYENID_Change(object sender, EventArgs e)
        {
            if (cboTUYENID.SelectValue != "" && txtMABHYT.Text != "")
            {
                if (cboTUYENID.SelectValue == "3")
                {
                    cboHINHTHUCVAOVIENID.SelectValue = "2";
                }

                if (cboTUYENID.SelectValue == "4")
                {
                    //chkBH5NAM").prop("checked", false);
                    //chkBH5NAM").attr("disabled", true);
                    chkBH5NAM.Checked = false;
                    chkBH5NAM.Enabled = false;
                    //chkBH6THANG").prop("checked", false);
                    //chkBH6THANG").attr("disabled", true);
                    chkBH6THANG.Checked = false;
                    chkBH6THANG.Enabled = false;
                }
                else
                {
                    //chkBH5NAM.Enabled = true;
                    //chkBH6THANG.Enabled = true;
                    chkBH5NAM.Enabled = true;
                    chkBH6THANG.Enabled = true;
                }
            }

            Show_Hide_divChuyenTuyen();
        }
        private void Show_Hide_divChuyenTuyen()
        {
            //START - HISL2TK-411 -- hongdq-- 09052018
            if (cboTUYENID.SelectValue == "2")
            {
                string data_ar_ch_chuyendt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", pars_ch_chuyendt);
                if (data_ar_ch_chuyendt == "1")
                {
                    groupControl2.Size = new Size(groupControl2.Size.Width, 334);
                    layoutControlItem40.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    layoutControlItem41.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    //divChuyenTuyen').show();
                    btnChuyenTuyen.Visible = false;
                    //btnChuyenTuyen').hide();
                }
                else
                {
                    groupControl2.Size = new Size(groupControl2.Size.Width, 310);
                    layoutControlItem40.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    layoutControlItem41.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    //divChuyenTuyen').hide();
                    btnChuyenTuyen.Visible = true;
                    //btnChuyenTuyen').show();
                }
            }
            else
            {
                groupControl2.Size = new Size(groupControl2.Size.Width, 310);
                layoutControlItem40.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem41.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                //divChuyenTuyen').hide();
                btnChuyenTuyen.Visible = false;
                //btnChuyenTuyen').hide();
            }
        }
        private void cboHINHTHUCVAOVIENID_Change(object sender, EventArgs e)
        {
            if (cboTUYENID.SelectValue != "" && txtMABHYT.Text != "" && cboHINHTHUCVAOVIENID.SelectValue == "2")
            {
                cboTUYENID.SelectValue = "3";
            }
        }


        string hinhthucvaovienid = "1";
        bool isDtkhaibao = false;
        string type = "";
        int config_ndkkcbbd = 0;
        string config = "";

        string hidBENHNHANID = "";
        string hidTIEPNHANID = "";
        string hidHOSOBENHANID = "";
        string hidNGAYTIEPNHAN = "";
        string hidNGAYSINH = "";
        private void viewDetail(object sender, EventArgs e)
        {
            int index = ucGrid_DsBenhNhan.gridView.FocusedRowHandle;
            DataRowView rowData = (DataRowView)ucGrid_DsBenhNhan.gridView.GetRow(index);

            if (rowData == null) return;

            //  DataRowView rowData = ucGrid_DsBenhNhan.SelectedRow;
            if (rowData == null) return;
            //{"func":"ajaxCALL_SP_O","params":["NTU01H022.EV003","96974",0],"uuid":"086858e3-4dd0-46dc-9713-45cd4de0f4b6"}
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("NTU01H022.EV003", rowData["TIEPNHANID"].ToString());
            // { "result": "[{\"MABENHNHAN\": \"BN00042950\",\"SOVAOVIEN\": \"123\",\"MAHOSOBENHAN\": \"BA18000914\",\"SOLUUTRU\": \"\",\"MABHYT\": \"DN4938484333626\"
            //,\"TENBENHNHAN\": \"MTEST NHẬP VIỆN\",\"IFNGAYSINH\": \"1990 (28 Tuổi)\",\"NGAYSINH\": null,\"NAMSINH\": \"1990\",\"TUOI\": \"28\",\"DVTUOI\": \"1\"
            //    ,\"DIACHI\": \"Xã Thới Tam Thôn-Huyện Hóc Môn-TP Hồ Chí Minh\",\"GIOITINH\": \"Nữ\",\"TEN_DANTOC\": \"Kinh\",\"TENNGHENGHIEP\": \"Nhân dân\"
            //    ,\"TENDIAPHUONG\": \"Việt Nam\",\"TEN_DTBN\": \"BHYT\",\"TUYEN\": \"Đúng tuyến\",\"THOIGIAN\": \"01/01/2018-31/01/2019\",\"NOILAMVIEC\": \"\"
            //    ,\"TENNGUOITHAN\": \"\",\"NGAYTIEPNHAN\": \"17/09/2018 09:12:44\",\"MACHANDOANVAOVIEN\": \"\",\"CHUANDOANVAOVIEN\": \"\",\"NHANTU\": \"Khoa khám bệnh\"
            //    ,\"RAKHOA\": null,\"SONGAYDIEUTRI\": null,\"TENLOAIBENHAN\": \"Khám bệnh\",\"SINHTHETE\": \"0\",\"BAOTINCHO\": \" -  -  - \",\"TRANGTHAITIEPNHAN\": \"0\"
            //    ,\"DOITUONGDB\": null,\"TRANGTHAITIEPNHAN_VP\": \"0\",\"SUB_DTBNID\": null}]","out_var": "[]","error_code": 0,"error_msg": ""}
            // 
            if (data_ar.Rows.Count > 0)
            {
                //FormUtil.clearForm("divThongTinBenhNhan", "");
                //FormUtil.clearForm("divHid"); cboDOITUONGBENHNHANID

                lblMAHOSOBENHAN.Text = data_ar.Rows[0]["MAHOSOBENHAN"].ToString();
                lblSOVAOVIEN.Text = data_ar.Rows[0]["SOVAOVIEN"].ToString();
                lblTENBENHNHAN.Text = data_ar.Rows[0]["TENBENHNHAN"].ToString();
                lblIFNGAYSINH.Text = data_ar.Rows[0]["IFNGAYSINH"].ToString();
                lblGIOITINH.Text = data_ar.Rows[0]["GIOITINH"].ToString();
                lblTENNGHENGHIEP.Text = data_ar.Rows[0]["TENNGHENGHIEP"].ToString();
                lblMABHYT.Text = data_ar.Rows[0]["MABHYT"].ToString();
                lblTHOIGIAN.Text = data_ar.Rows[0]["THOIGIAN"].ToString();
                lblTEN_DTBN.Text = data_ar.Rows[0]["TEN_DTBN"].ToString();
                lblTUYEN.Text = data_ar.Rows[0]["TUYEN"].ToString();
                lblNOILAMVIEC.Text = data_ar.Rows[0]["NOILAMVIEC"].ToString();



                //FormUtil.setObjectToForm("divChuyenDoiTuong", "", data);
                lblCDTMABENHNHAN.Text = data_ar.Rows[0]["MABENHNHAN"].ToString();
                lblCDTTENBENHNHAN.Text = data_ar.Rows[0]["TENBENHNHAN"].ToString();
                lblCDTGIOITINH.Text = data_ar.Rows[0]["GIOITINH"].ToString();
                lblCDTNGAYSINH.Text = data_ar.Rows[0]["IFNGAYSINH"].ToString();
                lblCDTDIACHI.Text = data_ar.Rows[0]["DIACHI"].ToString();
                lblCDTMABHYT.Text = data_ar.Rows[0]["MABHYT"].ToString();
                lblCDTTHOIGIAN.Text = data_ar.Rows[0]["THOIGIAN"].ToString();

                if (data_ar.Rows[0]["TRANGTHAITIEPNHAN"].ToString() != "0")
                {
                    if (func_kt != "1")
                    {
                        //btnCHUYENDOITUONG").attr("disabled", true);
                        btnCHUYENDOITUONG.Visible = false;
                    }
                    else
                    {
                        if (data_ar.Rows[0]["TRANGTHAITIEPNHAN"].ToString() == "2" || data_ar.Rows[0]["TRANGTHAITIEPNHAN_VP"].ToString() == "1")
                        {
                            //btnCHUYENDOITUONG").attr("disabled", true);
                            btnCHUYENDOITUONG.Visible = false;
                        }
                        else
                        {
                            //btnCHUYENDOITUONG.Enabled = true;
                            btnCHUYENDOITUONG.Visible = true;
                        }
                    }
                }
                else
                {
                    //btnCHUYENDOITUONG.Enabled = true;
                    btnCHUYENDOITUONG.Visible = true;
                };

                hidBENHNHANID = rowData["BENHNHANID"].ToString();
                hidTIEPNHANID = rowData["TIEPNHANID"].ToString();
                hidHOSOBENHANID = rowData["HOSOBENHANID"].ToString();
                hidNGAYTIEPNHAN = rowData["NGAYTIEPNHAN"].ToString();

                string x = data_ar.Rows[0]["NGAYTIEPNHAN"].ToString();


                hidNGAYSINH = data_ar.Rows[0]["NGAYSINH"].ToString();

                //lblCDTMABENHNHAN').html(FormUtil.unescape(data.MABENHNHAN));
                //lblCDTTENBENHNHAN').html(FormUtil.unescape(data.TENBENHNHAN));
                //lblCDTGIOITINH').html(FormUtil.unescape(data.GIOITINH));
                //lblCDTNGAYSINH').html(FormUtil.unescape(data.IFNGAYSINH));
                //lblCDTDIACHI').html(FormUtil.unescape(data.DIACHI));
                //lblCDTMABHYT').html(FormUtil.unescape(data.MABHYT));
                //lblCDTTHOIGIAN').html(FormUtil.unescape(data.THOIGIAN));

                DataTable data_ar_2 = RequestHTTP.call_ajaxCALL_SP_O("NTU01H022.EV002", rowData["TIEPNHANID"].ToString());
                // "MABENHNHAN\": \"BN00042863\",\"TENBENHNHAN\": \"LUANNT1107_02\",\"NGAYSINH\": \"\",\"NAMSINH\": \"1989\",\"TUOI\": \"29\",\"GIOITINHID\": \"2\"
                //,\"NGHENGHIEPID\": \"3\",\"DANTOCID\": \"25\",\"QUOCGIAID\": \"0\",\"SONHA\": \"\",\"DIABANID\": null,\"DIAPHUONGID\": \"3131711920\",\"NOILAMVIEC\": \"\"
                //    ,\"DIACHI\": \"Xã Nghĩa Lộ-Huyện Cát Hải-TP Hải Phòng\",\"NGUOITHAN\": \"\",\"TENNGUOITHAN\": \"\",\"DIENTHOAINGUOITHAN\": \"\",\"MUCHUONG\": \"0%\"
                //    ,\"DIACHINGUOITHAN\": \"\",\"DOITUONGBENHNHANID\": \"2\",\"NGAYTIEPNHAN\": \"11/07/2018 10:41:30\",\"MABHYT\": \"\",\"TKDKKBBD\": \"\",\"MAVUNGID\": \"\"
                //    ,\"NGAY_SD\": null,\"BHYT_BD\": \"\",\"BHYT_KT\": \"\",\"BH5NAM\": null,\"TUYENID\": null,\"DIACHI_BHYT\": \"\",\"HINHTHUCVAOVIENID\": \"3\"
                //    ,\"SINHTHETE\": \"0\",\"SOLUUTRU\": \"\",\"SOVAOVIEN\": \"98\",\"MABENHAN\": \"BA18000817\",\"TKNOICD\": \"\",\"DOITUONGDB\": \"0\",\"DVTUOI\": \"1\"
                //    ,\"BH6THANG\": null,\"SUB_DTBNID\": \"0\"}  
                if (data_ar_2.Rows.Count > 0)
                {
                    if (data_ar_2.Rows[0]["DOITUONGBENHNHANID"].ToString() != "1")
                    {
                        _clearObjectBHYT(true);
                    }
                    else
                    {
                        _clearObjectBHYT(false);
                    }

                    //chkCHECKCONG.Checked = data_ar_2.Rows[0]["CHECKCONG"].ToString() == "1";
                    //chkTHEMTHE.Checked = data_ar_2.Rows[0]["THEMTHE"].ToString() == "1";
                    //chkGIAHANTHE.Checked = data_ar_2.Rows[0]["GIAHANTHE"].ToString() == "1";

                    cboDOITUONGBENHNHANID.SelectValue = data_ar_2.Rows[0]["DOITUONGBENHNHANID"].ToString();
                    txtMABHYT.Text = data_ar_2.Rows[0]["MABHYT"].ToString();
                    txtBHYT_BD.Text = data_ar_2.Rows[0]["BHYT_BD"].ToString();
                    txtBHYT_KT.Text = data_ar_2.Rows[0]["BHYT_KT"].ToString();
                    cboDKKBBDID.SelectedValue = data_ar_2.Rows[0]["TKDKKBBD"].ToString();
                    cboTUYENID.SelectValue = data_ar_2.Rows[0]["TUYENID"].ToString();
                    cboMAVUNGID.SelectValue = data_ar_2.Rows[0]["MAVUNGID"].ToString();
                    txtDIACHI_BHYT.Text = data_ar_2.Rows[0]["DIACHI_BHYT"].ToString();
                    cboHINHTHUCVAOVIENID.SelectValue = data_ar_2.Rows[0]["HINHTHUCVAOVIENID"].ToString();

                    //chkNOTHE
                    chkSINHTHETE.Checked = data_ar_2.Rows[0]["SINHTHETE"].ToString() == "1";
                    chkBH5NAM.Checked = data_ar_2.Rows[0]["BH5NAM"].ToString() == "1";
                    chkBH6THANG.Checked = data_ar_2.Rows[0]["BH6THANG"].ToString() == "1";

                    cboNOICD.SelectedValue = data_ar_2.Rows[0]["TKNOICD"].ToString();
                    txtTENNGUOITHAN.Text = data_ar_2.Rows[0]["TENNGUOITHAN"].ToString();
                    cboDOITUONGDB.SelectValue = data_ar_2.Rows[0]["DOITUONGDB"].ToString();
                    txtTENBENHNHAN.Text = data_ar_2.Rows[0]["TENBENHNHAN"].ToString();
                    txtNGAYSINH.Text = data_ar_2.Rows[0]["NGAYSINH"].ToString();
                    txtNAMSINH.Text = data_ar_2.Rows[0]["NAMSINH"].ToString();
                    txtTUOI.Text = data_ar_2.Rows[0]["TUOI"].ToString();

                    if (data_ar_2.Rows[0]["DVTUOI"].ToString() == "1") cboDVTUOI.Text = "Tuổi";
                    else if (data_ar_2.Rows[0]["DVTUOI"].ToString() == "2") cboDVTUOI.Text = "Tháng";
                    else if (data_ar_2.Rows[0]["DVTUOI"].ToString() == "3") cboDVTUOI.Text = "Ngày";
                    else if (data_ar_2.Rows[0]["DVTUOI"].ToString() == "4") cboDVTUOI.Text = "Giờ";
                    // < option value = "1" selected > Tuổi </ option >   
                    //< option value = "2" > Tháng </ option >    
                    // < option value = "3" > Ngày </ option >     
                    //  < option value = "4" > Giờ </ option >
                    cboGIOITINHID.SelectValue = data_ar_2.Rows[0]["GIOITINHID"].ToString();
                    //chkKHONGCHUYEN_DICHVU
                    //txtNGAYAPDUNGTu
                    //txtNGAYAPDUNGDEN
                    //txtTKMANOIGIOITHIEU
                    //    txtTKMACHANDOANTUYENDUOI

                    hinhthucvaovienid = data_ar_2.Rows[0]["HINHTHUCVAOVIENID"].ToString();

                    // START SONDN 010118
                    if (isDtkhaibao)
                    {
                        //cboDOITUONGBENHNHANID option').removeAttr('selected').filter('[value=val1]').attr('selected', true);
                        //cboDOITUONGBENHNHANID").find("option[extval0='" + data.SUB_DTBNID + "']").prop("selected", true);

                        cboDOITUONGBENHNHANID.SetIndexByColumnValue(2, data_ar_2.Rows[0]["SUB_DTBNID"].ToString());
                    }
                    // END SONDN 010118

                    if (data_ar_2.Rows[0]["SINHTHETE"].ToString() == "1")
                    {
                        //layoutControlItem34.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        //txtMABHYT').attr("disabled", true);
                        txtMABHYT.Enabled = false;
                    }
                    else
                    {
                        if (cboDOITUONGBENHNHANID.SelectValue == "1")
                        {
                            //layoutControlItem34.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            //txtMABHYT').attr("disabled", false);
                            txtMABHYT.Enabled = true;
                        }
                    }

                    if (type == "1")
                    {
                        //chkBH5NAM").attr("disabled", true);
                        chkBH5NAM.Enabled = false;
                        //layoutControlItem57.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                        //chkSINHTHETE").attr("disabled", true);
                        chkSINHTHETE.Enabled = false;
                        //layoutControlItem56.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                        //txtMABHYT').attr("disabled", true);
                        txtMABHYT.Enabled = false;

                        if (cboDOITUONGBENHNHANID.SelectValue == "1")
                        {
                            chkNOTHE.Enabled = true;
                            //chkNOTHE").prop("disabled", false);
                        }
                        else
                            chkNOTHE.Enabled = false;
                        //layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                    }
                    else
                    {
                        if (cboDOITUONGBENHNHANID.SelectValue == "1")
                        {
                            //chkBH5NAM.Enabled = true;
                            chkBH5NAM.Enabled = true;
                            //layoutControlItem57.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                            //chkSINHTHETE.Enabled = true;
                            chkSINHTHETE.Enabled = true;
                            //layoutControlItem56.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                        }
                        else
                        {
                            //chkBH5NAM").attr("disabled", true);
                            chkBH5NAM.Enabled = false;
                            //layoutControlItem57.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

                            chkSINHTHETE.Enabled = false;
                            //chkSINHTHETE").attr("disabled", true);
                            //layoutControlItem56.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                        }

                        //chkNOTHE").prop("disabled", true);
                        chkNOTHE.Enabled = false;
                        //layoutControlItem44.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                    }
                    loadGridChuyenDoiTuong(1, null);
                    loadGridDataDichVu(1, null);
                }

                //chkTHEMTHE").prop("checked", false);
                //chkTHEMTHE.Enabled = true;
                chkTHEMTHE.Checked = false;
                chkTHEMTHE.Enabled = true;
                //layoutControlItem31.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                //chkGIAHANTHE").prop("checked", false);
                //chkGIAHANTHE.Enabled = true;
                chkGIAHANTHE.Checked = false;
                chkGIAHANTHE.Enabled = true;
                //layoutControlItem32.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            }

            DataTable vp_ar = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.06", rowData["TIEPNHANID"].ToString());
            if (vp_ar.Rows.Count > 0)
            {
                lblTAMUNG.Text = Func.formatMoneyVN(vp_ar.Rows[0]["TAMUNG"].ToString()) + " đ";
            }

            vp_ar = RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.05", rowData["TIEPNHANID"].ToString());
            if (vp_ar.Rows.Count > 0)
            {
                lblTONGCHIPHI.Text = Func.formatMoneyVN(vp_ar.Rows[0]["TONGTIENDV"].ToString()) + " đ";
                lblBAOHIEMTHANHTOAN.Text = Func.formatMoneyVN(vp_ar.Rows[0]["BHYT_THANHTOAN"].ToString()) + " đ";
            }
            if (txtNGAYSINH.Text.Trim().Length <= 5)
            {
                txtNGAYSINH.Text = "";
            }

            //$('input:radio[name=NGAYAPDUNGBH]').filter('[value=3]').prop('checked', true).change();
            NGAYAPDUNGBH.SelectedIndex = 2;
        }
        private void _clearObjectBHYT(bool value)
        {
            txtMABHYT.Text = "";
            txtMABHYT.Enabled = !value;

            txtBHYT_BD.Text = "";
            txtBHYT_BD.Visible = !value;

            cboDKKBBDID.textEdit1.Text = "";
            cboDKKBBDID.Visible = !value;

            //_clearAndDisable("txtMABHYT", "", value);
            //_clearAndDisable("txtBHYT_BD", "", value);
            //_clearAndDisable("txtBHYT_KT", "", value);
            //_clearAndDisable("txtTKDKKBBD", "", value);
            txtTENNGUOITHAN.Visible = !value;
            txtTENBENHNHAN.Visible = !value;
            txtNGAYSINH.Visible = !value;
            txtNAMSINH.Visible = !value;
            txtTUOI.Visible = !value;
            cboDVTUOI.Visible = !value;
            cboGIOITINHID.Visible = !value;

            cboTUYENID.SelectIndex = -1;
            cboTUYENID.Visible = !value;


            cboHINHTHUCVAOVIENID.SelectValue = hinhthucvaovienid;
            cboHINHTHUCVAOVIENID.Visible = !value;


            cboNOICD.textEdit1.Text = "";
            cboNOICD.Visible = !value;

            cboMAVUNGID.SelectIndex = -1;
            cboMAVUNGID.Visible = !value;

            txtDIACHI_BHYT.Text = "";
            txtDIACHI_BHYT.Visible = !value;

            chkBH5NAM.Checked = false; chkBH5NAM.Visible = !value;
            chkNOTHE.Checked = false; chkNOTHE.Visible = !value;
            chkSINHTHETE.Checked = false; chkSINHTHETE.Visible = !value;

            cboDOITUONGDB.SelectValue = ""; cboDOITUONGDB.Visible = !value;

            if (!value)
            {
                layoutControlItem34.Text = "Số thẻ BHYT <color=Red>(*)</color>";
                layoutControlItem42.Text = "Ngày BĐ <color=Red>(*)</color>";
                layoutControlItem43.Text = "Ngày KT <color=Red>(*)</color>";
                layoutControlItem35.Text = "ĐKKCB BĐ <color=Red>(*)</color>";
                layoutControlItem45.Text = "Tuyến <color=Red>(*)</color>";
                layoutControlItem36.Text = "Địa chỉ BHYT <color=Red>(*)</color>";
                layoutControlItem46.Text = "Vào từ <color=Red>(*)</color>";
                //layoutControlItem37.Text = "Nơi CĐ <color=Red>(*)</color>";
                layoutControlItem38.Text = "Họ tên <color=Red>(*)</color>";
                layoutControlItem39.Text = "Giới tính <color=Red>(*)</color>";

                txtBHYT_BD.Text = "01/01/" + DateTime.Now.Year.ToString();
                txtBHYT_KT.Text = "31/12/" + DateTime.Now.Year.ToString();
                //txtDIACHI_BHYT').val(txtDIACHI').val());  txtDIACHI ko tồn tại
            }
            else
            {
                layoutControlItem34.Text = "Số thẻ BHYT";
                layoutControlItem42.Text = "Ngày BĐ";
                layoutControlItem43.Text = "Ngày KT";
                layoutControlItem35.Text = "ĐKKCB BĐ";
                layoutControlItem45.Text = "Tuyến";
                layoutControlItem36.Text = "Địa chỉ BHYT";
                layoutControlItem46.Text = "Vào từ";
                //layoutControlItem37.Text = "Nơi CĐ";
                layoutControlItem38.Text = "Họ tên";
                layoutControlItem39.Text = "Giới tính";
            }

            layoutControlItem37.Text = "Nơi CĐ";
        }



        #endregion

        bool i_cdt = false;
        private void loadGridData(object sender, EventArgs e)
        {
            //if (i_cdt)
            //{
            //    if (cboKHOAID').val() == 0){
            //        MessageBox.Show('Vui lòng chọn khoa');
            //        return;
            //    }
            //}
            //if ((opt._deptId != null && opt._deptId != '') || i_cdt)
            //{
            ucGrid_DsBenhNhan.clearData();

            int page = (int)sender;
            if (page > 0)
            {
                //{"func":"ajaxExecuteQueryPaging","uuid":"","params":["NTU01H022.EV001"],"options":[{"name":"[0]","value":"-1"},{"name":"[1]","value":"-1"},{"name":"[2]","value":"-1"},{"name":"[3]","value":"4902"},{"name":"[4]","value":"0"}]}
                ResponsList responses = new ResponsList();
                string tu_ngay = txtTG_NHAPVIEN_TU.Text == "" ? "-1" : txtTG_NHAPVIEN_TU.DateTime.ToString(Const.FORMAT_date1);
                string den_ngay = txtTG_NHAPVIEN_DEN.Text == "" ? "-1" : txtTG_NHAPVIEN_DEN.DateTime.ToString(Const.FORMAT_date1);
                string loai_tiepnhan = cboLOAITIEPNHANID.SelectValue;

                responses = RequestHTTP.get_ajaxExecuteQueryPaging("NTU01H022.EV001", page, 20,
                       new String[] { "[0]", "[1]", "[2]", "[3]", "[4]" },
                       new string[] { tu_ngay, den_ngay, loai_tiepnhan
                        , i_cdt ?cboKHOAID.SelectValue :Const.local_khoaId.ToString()
                        , chkDONGBENHAN.Checked? "1":"0"
                       },
                       ucGrid_DsBenhNhan.jsonFilter());

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MAUBENHPHAM_TEMPID", "TENMAUBENHPHAM_TEMP", "KHOID", "TENKHO" });

                ucGrid_DsBenhNhan.setData(dt, responses.total, responses.page, responses.records);
                ucGrid_DsBenhNhan.setColumnAll(false);

                ucGrid_DsBenhNhan.onIndicator();
                ucGrid_DsBenhNhan.setColumn("MAHOSOBENHAN", "Mã BA");
                ucGrid_DsBenhNhan.setColumn("MABENHNHAN", "Mã bệnh nhân");
                ucGrid_DsBenhNhan.setColumn("TENBENHNHAN", "Tên bệnh nhân");
                ucGrid_DsBenhNhan.setColumn("MA_BHYT", "Mã BHYT");
                ucGrid_DsBenhNhan.setColumn("NGAYTIEPNHAN", "Thời gian vào viện");
                ucGrid_DsBenhNhan.setColumn("LOAITIEPNHAN", "Loại tiếp nhận");

                //ucGrid_DsBenhNhan.gridView.SelectRow(0);

                viewDetail(null, null);
            }
            //}
            //else
            //{
            //    MessageBox.Show('Chưa thiết lập khoa phòng');
            //}
        }
        private void loadGridDataDichVu(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                // postData: {"func":"ajaxExecuteQueryPaging","uuid":"558aaf5c-ebb6-45db-a018-a270fc834ab0"
                // ,"params":["NTU01H022.EV008"],"options":[{"name":"[0]","value":"95408"},{"name":"[1]","value":"-1"}]}
                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging("NTU01H022.EV008", page, GridDataDichVu.getNumberPerPage()
                    , new string[] { "[0]", "[1]" }, new string[] { hidTIEPNHANID, "-1" }, GridDataDichVu.jsonFilter()
                    , "NGAYCD asc");

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                { "NGAYDICHVU", "MAUBENHPHAMID", "GHICHU", "TENDICHVU", "SOLUONG"
                , "DONVI", "TIEN_DICHVU", "LOAITTCU", "LOAITTMOI", "KHOA", "PHONG" });

                dt.Columns.Add("Ngay_cd", typeof(DateTime));
                for (int i = 0; i < dt.Rows.Count; i++)
                    dt.Rows[i]["Ngay_cd"] = Func.ParseDate(dt.Rows[i]["NGAYCD"].ToString());

                GridDataDichVu.setData(dt, responses.total, responses.page, responses.records);
                GridDataDichVu.setColumnAll(false);
                GridDataDichVu.gridView.Columns["Ngay_cd"].Group(); //tạo group dữ liệu
                GridDataDichVu.gridView.ExpandAllGroups();


                GridDataDichVu.setColumn("Ngay_cd", "Ngày");
                GridDataDichVu.setColumn("NGAYDICHVU", "Ngày");
                GridDataDichVu.setColumn("MAUBENHPHAMID", "Phiếu");
                GridDataDichVu.setColumn("GHICHU", "Nhóm BHYT");
                GridDataDichVu.setColumn("TENDICHVU", "Tên thuốc + dịch vụ");
                GridDataDichVu.setColumn("SOLUONG", "Số lượng");
                GridDataDichVu.setColumn("DONVI", "Đơn vị");
                GridDataDichVu.setColumn("TIEN_DICHVU", "Giá tiền");
                GridDataDichVu.setColumn("LOAITTCU", "Loại đối tượng");
                GridDataDichVu.setColumn("LOAITTMOI", "Loại TT");
                GridDataDichVu.setColumn("KHOA", "Khoa");
                GridDataDichVu.setColumn("PHONG", "Phòng");

            }
        }

        private void loadGridChuyenDoiTuong(object sender, EventArgs e)
        {


            //var _sql_par =   [{ "name":"[0]", "value":hidTIEPNHANID").val()}];
            //GridUtil.loadGridBySqlPage(_gridLsid, 'NTU01H022.EV007', _sql_par);

            int page = (int)sender;
            if (page > 0)
            {
                // postData: {"func":"ajaxExecuteQueryPaging","uuid":"012d8cce-6d32-4275-9337-cbbd54234daa",
                // "params":["NTU01H022.EV007"],"options":[{"name":"[0]","value":"97131"}]}
                ResponsList responses = RequestHTTP.get_ajaxExecuteQueryPaging("NTU01H022.EV007", page, GridChuyenDoiTuong.getNumberPerPage()
                    , new string[] { "[0]" }, new string[] { hidTIEPNHANID }, GridChuyenDoiTuong.jsonFilter());

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);
                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[]
                { "TEN_DTBN", "MA_BHYT", "MA_KCBBD", "BHYT_BD", "BHYT_KT"
                , "TENLOAIBHYT", "DIACHI_BHYT", "NGAYTHUCHIEN", "USER_NAME", "TY_LE" });

                GridChuyenDoiTuong.setData(dt, responses.total, responses.page, responses.records);
                GridChuyenDoiTuong.setColumnAll(false);

                GridChuyenDoiTuong.setColumn("TEN_DTBN", "Đối tượng");
                GridChuyenDoiTuong.setColumn("MA_BHYT", "Mã");
                GridChuyenDoiTuong.setColumn("MA_KCBBD", "Mã DKKCBBD");
                GridChuyenDoiTuong.setColumn("BHYT_BD", "Thời gian BĐ");
                GridChuyenDoiTuong.setColumn("BHYT_KT", "Thời gian KT");

                GridChuyenDoiTuong.setColumn("TENLOAIBHYT", "Tuyến");
                GridChuyenDoiTuong.setColumn("DIACHI_BHYT", "Địa chỉ BHYT");
                GridChuyenDoiTuong.setColumn("NGAYTHUCHIEN", "Ngày chuyển");
                GridChuyenDoiTuong.setColumn("USER_NAME", "Người chuyển");
                GridChuyenDoiTuong.setColumn("TY_LE", "Tỷ lệ hưởng BH");

            }
        }

        //string hidCV_CHUYENVIEN_HINHTHUCID1 = "";
        //string hidCV_CHUYENVIEN_LYDOID1 = "";
        //string hidCV_CHUANDOANGIOITHIEU = "";
        //    string hidCV_TKMANOIGIOITHIEU1 = "";
        //    string hidCV_CHUYENDUNGTUYEN1 = "";
        //    string hidCV_TKNOICD = ""; 
        private void btnChuyenTuyen_Click(object sender, EventArgs e)
        {
            dtChuyenVien.Rows[0]["ucBenhVien"] = cboMANOIGIOITHIEU.SelectedValue;   // hidCV_TKMANOIGIOITHIEU1  =  hidCV_TKNOICD
            dtChuyenVien.Rows[0]["ucCDTD"] = txtCHANDOANTUYENDUOI.SelectedText;     // hidCV_CHUANDOANGIOITHIEU
            //dtChuyenVien.Rows[0]["ucHinhThucChuyen"] =                            // hidCV_CHUYENVIEN_HINHTHUCID1
            //dtChuyenVien.Rows[0]["ucLyDoChuyen"] =                                // hidCV_CHUYENVIEN_LYDOID1
            //dtChuyenVien.Rows[0]["rbtChuyen"] =                                   // hidCV_CHUYENDUNGTUYEN1

            //chandoantuyenduoi = cboMANOIGIOITHIEU.SelectedText,
            //tkmanoigioithieu = cboNOICD.SelectedValue, 

            //_showDialog("ngoaitru/NGT01T001_chuyenvien", param, 'THÔNG TIN CHUYỂN TUYẾN',820,280);
            VNPT.HIS.CommonForm.NGT01T001_chuyenvien frm_NGT01T001_chuyenvien = new VNPT.HIS.CommonForm.NGT01T001_chuyenvien();
            frm_NGT01T001_chuyenvien.setReturnData(ReturnForm_NGT01T001_chuyenvien);
            frm_NGT01T001_chuyenvien.setData(dtChuyenVien);
            openForm(frm_NGT01T001_chuyenvien, "1");
        }
        private void ReturnForm_NGT01T001_chuyenvien(object sender, EventArgs e)
        {
            dtChuyenVien = (DataTable)sender;
            //ucNoichuyen.SelectedValue = dtChuyenVien.Rows[0]["ucBenhVien"].ToString();
            //ucChuandoan.SelectedText = dtChuyenVien.Rows[0]["ucCDTD"].ToString();

            //hidCV_CHUANDOANGIOITHIEU').val(objChuyenVien.CHANDOANTUYENDUOI);
            //hidCV_TKMANOIGIOITHIEU1').val(objChuyenVien.MANOIGIOITHIEU);
            //hidCV_CHUYENVIEN_HINHTHUCID1').val(objChuyenVien.CV_CHUYENVIEN_HINHTHUCID);
            //hidCV_CHUYENVIEN_LYDOID1').val(objChuyenVien.CV_CHUYENVIEN_LYDOID);
            //hidCV_CHUYENDUNGTUYEN1').val(objChuyenVien.CV_CHUYENDUNGTUYEN); 
            //hidCV_TKNOICD").val(objChuyenVien.MANOIGIOITHIEU); 
        }
        private void openForm(Form frm, string optionsPopup)
        {
            // optionsPopup==1 kiểu popup
            // optionsPopup==0 kiểu toàn màn hình
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


        private void btnCHUYENDOITUONG_Click(object sender, EventArgs e)
        {
            //  var valid = validator.validateForm(); ???

            if (cboTUYENID.SelectValue == "2" && cboMAVUNGID.SelectValue == "")
            {
                string data_ar_ch_chuyendt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "NTU_CHUYEN_DT_POPUP");
                if (data_ar_ch_chuyendt == "1")//START HISL2TK-474
                {
                    if (cboMANOIGIOITHIEU.SelectedValue == "" || txtCHANDOANTUYENDUOI.SelectedValue == "")
                    {
                        MessageBox.Show("Chưa nhập thông tin chuyển tuyến: Từ viện hoặc Chuẩn đoán TD ");
                        return;
                    }
                }
                else if (cboMANOIGIOITHIEU.SelectedValue == "" || dtChuyenVien.Rows[0]["ucHinhThucChuyen"].ToString() == ""
                    || dtChuyenVien.Rows[0]["ucLyDoChuyen"].ToString() == "" || cboNOICD.SelectedValue == "")
                {
                    MessageBox.Show("Yêu cầu nhập thông tin chuyển tuyến với bệnh nhân đúng tuyến giới thiệu. ");
                    return;
                }
            }

            //if (valid)
            {
                //if (validateCDT())
                {
                    if (cboDOITUONGBENHNHANID.SelectValue == "1" &&
                        ((Const.local_user.HOSPITAL_CODE != cboDKKBBDID.SelectedValue && cboTUYENID.SelectValue != "4")
                            || (Const.local_user.HOSPITAL_CODE == cboDKKBBDID.SelectedValue && cboTUYENID.SelectValue == "4")
                        )
                       )
                    {
                        DialogResult dialogResult = MessageBox.Show("Dữ liệu đúng tuyến/trái tuyến không hợp lệ. Có tiếp tục?", "", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            saveData();
                        }
                    }
                    else
                    {
                        saveData();
                    }
                }
            }
        }



        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NGAYAPDUNGBH.SelectedIndex == 2)
            { // ngày khác
              //divLbNgayKhac").show();
              //divLbNgayKhac1").show();
              //divVlNgayKhac").show();
              //divVlNgayKhac1").show();

                //txtNGAYAPDUNGTU").val(moment().format('DD/MM/YYYY'));
                //txtNGAYAPDUNGDEN").val(moment().format('DD/MM/YYYY'));
                txtNGAYAPDUNGTU.DateTime = Func.getSysDatetime();
                txtNGAYAPDUNGDEN.DateTime = Func.getSysDatetime();

                layoutControlItem62.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                layoutControlItem63.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                //divLbNgayKhac").hide();
                //divLbNgayKhac1").hide();
                //divVlNgayKhac").hide();
                //divVlNgayKhac1").hide();
                //txtNGAYAPDUNGTU").val('');
                //txtNGAYAPDUNGDEN").val('');
                txtNGAYAPDUNGTU.Text = "";
                txtNGAYAPDUNGDEN.Text = "";

                layoutControlItem62.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                layoutControlItem63.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void chkTHEMTHE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTHEMTHE.Checked)
            {
                //$('input:radio[name=NGAYAPDUNGBH]').filter('[value=3]').prop('checked', true).change();
                NGAYAPDUNGBH.SelectedIndex = 2;
                //chkGIAHANTHE").attr("disabled", true);
                //chkGIAHANTHE").prop("checked", false);
                chkGIAHANTHE.Enabled = false;
                chkGIAHANTHE.Checked = false;
            }
            else
            {
                //chkGIAHANTHE.Enabled = true;
                //chkGIAHANTHE").prop("checked", false);
                chkGIAHANTHE.Enabled = true;
                chkGIAHANTHE.Checked = false;
            }
        }

        private void chkGIAHANTHE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGIAHANTHE.Checked)
            {
                //chkTHEMTHE").attr("disabled", true);
                //chkTHEMTHE").prop("checked", false);
                chkTHEMTHE.Enabled = false;
                chkTHEMTHE.Checked = false;
                //txtMABHYT').attr("disabled", true);
                txtMABHYT.Enabled = false;
            }
            else
            {
                //chkTHEMTHE.Enabled = true;
                //chkTHEMTHE").prop("checked", false);
                chkTHEMTHE.Enabled = true;
                chkTHEMTHE.Checked = false;
                //txtMABHYT').attr("disabled", false);
                txtMABHYT.Enabled = true;
            }
        }

        private void chkNOTHE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNOTHE.Checked)
            {
                if (config_ndkkcbbd <= 0)
                {
                    config_ndkkcbbd = 35148;
                }
                _clearObjectBHYT(false);
                chkNOTHE.Checked = true;
                chkNOTHE.Enabled = true;
                txtMABHYT.Text = _sinhSoNoThe();
                // txtMABHYT').prop("disabled", true);
                txtMABHYT.Enabled = false;
                //chkSINHTHETE").prop("checked", false);
                chkSINHTHETE.Checked = false;
                if (type == "1")
                {
                    //chkBH5NAM").attr("disabled", true);
                    //chkSINHTHETE").attr("disabled", true);
                    chkBH5NAM.Enabled = false;
                    chkSINHTHETE.Enabled = false;
                }
                //txtTKDKKBBD').combogrid("setValue", config_ndkkcbbd);
                cboDKKBBDID.SelectedValue = config_ndkkcbbd.ToString();

                cboTUYENID.SelectValue = "1";
                txtDIACHI_BHYT.Text = lblCDTDIACHI.Text;
            }
            else
            {
                if (type == "1")
                {
                    //txtMABHYT').prop("disabled", true);
                    txtMABHYT.Enabled = false;
                    //txtMABHYT').val("");
                    txtMABHYT.Text = "";
                }
                else
                {
                    //txtMABHYT').prop("disabled", false);
                    txtMABHYT.Enabled = true;
                    //txtMABHYT').val("");
                    txtMABHYT.Text = "";
                }

            }
        }

        private void chkSINHTHETE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSINHTHETE.Checked)
            {
                if (_kiemTraSinhSoTheBHYT())
                {
                    txtMABHYT.Text = _sinhSoTheBHYT();
                    string[] comp = hidNGAYSINH.Split('/');
                    //d = parseInt(comp[0], 10);
                    //var m = parseInt(comp[1], 10);
                    //var y = parseInt(comp[2], 10);
                    //var dateFrom = new Date(y, m - 1, d);
                    //var dateFrom = new Date(y, m - 1, d);
                    //var dateTo = new Date(y + 6, m - 1, d - 1);
                    DateTime dateFrom = new DateTime(Func.Parse(comp[2]), Func.Parse(comp[1]) - 1, Func.Parse(comp[0]));
                    DateTime dateTo = new DateTime(Func.Parse(comp[2]) + 6, Func.Parse(comp[1]) - 1, Func.Parse(comp[0]) - 1);


                    txtBHYT_BD.DateTime = dateFrom;
                    txtBHYT_KT.DateTime = dateTo;

                    //txtMABHYT').prop("disabled", true);
                    //chkNOTHE").prop("checked", false);
                    txtMABHYT.Enabled = true;
                    chkNOTHE.Checked = false;

                    string madiaphuong = RequestHTTP.call_ajaxCALL_SP_S_result("NTU01H022.EV009", hidBENHNHANID);
                    if (madiaphuong != null && madiaphuong != "")
                    {
                        string kcbbd = madiaphuong + "000";
                        //txtTKDKKBBD').combogrid("setValue", kcbbd);
                        cboDKKBBDID.SelectedValue = kcbbd;
                    }
                }
                else
                {
                    //chkSINHTHETE").prop("checked", false);
                    chkSINHTHETE.Checked = false;
                    //txtMABHYT').prop("disabled", false);
                    //txtMABHYT').val("");
                    txtMABHYT.Enabled = true;
                    txtMABHYT.Text = "";
                }
            }
            else
            {
                if (cboDOITUONGBENHNHANID.SelectValue == "1")
                {
                    //txtMABHYT').prop("disabled", false);
                    //txtMABHYT').val("");
                    txtMABHYT.Enabled = true;
                    txtMABHYT.Text = "";
                }
            }
        }

        private void txtMABHYT_Leave(object sender, EventArgs e)
        {
            if (!chkSINHTHETE.Checked && !chkNOTHE.Checked)
            {
                string sobhyt = txtMABHYT.Text.Trim().ToUpper();
                if ((sobhyt.Length > 15) && (sobhyt.IndexOf("|") > -1))
                {
                    string[] sobhyt_catchuoi = sobhyt.Split('|');
                    txtMABHYT.Text = sobhyt_catchuoi[0].Trim();
                    string noidk = sobhyt_catchuoi[5].Trim().Replace(" – ", "").Replace("-", "").Replace(" ", "").Replace(" ", "");
                    //txtTKDKKBBD").val(noidk);
                    //txtTKDKKBBD').combogrid("setValue", noidk);
                    cboDKKBBDID.SelectedValue = noidk;

                    //txtBHYT_BD").val(sobhyt_catchuoi[6]);
                    ///txtBHYT_KT").val(sobhyt_catchuoi[7]);
                    txtBHYT_BD.Text = sobhyt_catchuoi[6];
                    txtBHYT_KT.Text = sobhyt_catchuoi[7];

                    //txtDIACHI_BHYT.Text  = convert_utf8totext(sobhyt_catchuoi[4]); 
                    txtDIACHI_BHYT.Text = Func.FromHex(sobhyt_catchuoi[4]);
                    //txtTENBENHNHAN.Text = convert_utf8totext(sobhyt_catchuoi[1]);
                    txtTENBENHNHAN.Text = Func.FromHex(sobhyt_catchuoi[1]);

                    if (sobhyt_catchuoi[2].Trim().Length > 4)
                    {
                        txtNGAYSINH.Text = sobhyt_catchuoi[2].Trim();
                    }
                    else
                    {
                        txtNAMSINH.Text = sobhyt_catchuoi[2].Trim();
                    }

                    tinhTuoi(sobhyt_catchuoi[2], null);

                    cboGIOITINHID.SelectValue = (sobhyt_catchuoi[3] == "1" ? "1" : "2");
                }
            }
        }


        //tinh tuoi, nam sinh cua benh nhan
        public void tinhTuoi(string bNgaySinh, string bNgayTiepNhan) // bNgaySinh truyền vào phải dạng dd/MM/yyyy
        {// , 'txtNGAYSINH', 'txtNAMSINH', 'txtTUOI', 'cboDVTUOI'
         //, ctrNgaySinh, ctrNamSinh, ctrTuoi, ctrDVTuoi, bNgayTiepNhan

            DateTime ngayHT;

            bool isNgayTn = false; // đánh dấu có truyền bNgayTiepNhan khác rỗng, null vào ko.
            DateTime today = Func.getSysDatetime();

            if (bNgayTiepNhan != null && bNgayTiepNhan.Length == 10)
            {
                ngayHT = Func.ParseDatetime(bNgayTiepNhan);
                isNgayTn = true;
            }
            else
            {
                ngayHT = Func.getSysDatetime();
            }

            //bỏ qua check txtNGAYSINH nhập đúng ddMMyy hoặc ddMMyyyy vì control datetime đã đúng định dạng

            // kiem tra ngay sinh co lon hon ngay hien tai hay khong
            DateTime NgaySinh_NhapVao = Func.ParseDate(bNgaySinh);  // dateParts
            if (NgaySinh_NhapVao > ngayHT)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày " + (isNgayTn ? "tiếp nhận" : "hiện tại"));
                txtNGAYSINH.Text = "";
                txtNAMSINH.Text = "";
                txtTUOI.Text = "";
                cboDVTUOI.SelectedIndex = 0;
                txtNGAYSINH.Focus();
                //$('#' + ctrNgaySinh).val("");
                //$('#' + ctrTuoi).val("");
                //$('#' + ctrNamSinh).val("");
                //$('#' + ctrDVTuoi).val("1");
                //$('#' + ctrNgaySinh).focus();

                return;
            }


            {
                txtNAMSINH.Text = NgaySinh_NhapVao.Year.ToString();

                if (ngayHT.Year > NgaySinh_NhapVao.Year)
                {
                    //DateTime birthDate = NgaySinh_NhapVao;
                    int diff = (ngayHT.Year - NgaySinh_NhapVao.Year) * 12 + ngayHT.Month - NgaySinh_NhapVao.Month;// số tháng chênh lệch

                    if (diff < 36)
                    {
                        //         if (diff == 0)
                        //         {
                        //             var diffDay = sysDate.diff(birthDate, 'days');
                        //$('#' + ctrTuoi).val(diffDay);
                        //$('#' + ctrDVTuoi).val('3');
                        //         }
                        //         else
                        {
                            //$('#' + ctrTuoi).val(diff);
                            //$('#' + ctrDVTuoi).val('2'); // Tháng
                            txtTUOI.Text = diff.ToString();
                            cboDVTUOI.SelectedIndex = 1;
                        }
                    }
                    else
                    {
                        //$('#' + ctrTuoi).val(bNamHT - bNam);
                        //$('#' + ctrDVTuoi).val('1');
                        txtTUOI.Text = (ngayHT.Year - NgaySinh_NhapVao.Year).ToString();
                        cboDVTUOI.SelectedIndex = 0;
                    }
                    //<option value = "1" selected="">Tuổi</option>
                    //<option value = "2" > Tháng </ option > 
                    //                              < option value="3">Ngày</option>
                    //<option value = "4" > Giờ </ option > 
                }
                else if (ngayHT.Month > NgaySinh_NhapVao.Month) // năm bằng nhau, tháng của HT lớn hơn
                {
                    TimeSpan ts = TimeSpan.FromTicks(ngayHT.Ticks - NgaySinh_NhapVao.Ticks);
                    Double diff = ts.TotalDays + 1; // chênh ngày

                    if (diff < 30)
                    {
                        //$('#' + ctrTuoi).val(diff);
                        //$('#' + ctrDVTuoi).val("3");
                        txtTUOI.Text = diff.ToString();
                        cboDVTUOI.SelectedIndex = 2;
                    }
                    else
                    {
                        //$('#' + ctrTuoi).val(bThangHT - bThang);
                        //$('#' + ctrDVTuoi).val("2");
                        txtTUOI.Text = (ngayHT.Month - NgaySinh_NhapVao.Month).ToString();
                        cboDVTUOI.SelectedIndex = 1;
                    }

                }
                else if (ngayHT.Day > NgaySinh_NhapVao.Day)// tháng bằng nhau, ngày của HT lớn hơn
                {
                    //$('#' + ctrTuoi).val(bNgayHT - bNgay);
                    //$('#' + ctrDVTuoi).val("3");
                    txtTUOI.Text = (ngayHT.Day - NgaySinh_NhapVao.Day).ToString();
                    cboDVTUOI.SelectedIndex = 2;
                }
                else
                {
                    //$('#' + ctrTuoi).val("1");
                    //$('#' + ctrDVTuoi).val("3");
                    txtTUOI.Text = "1";
                    cboDVTUOI.SelectedIndex = 2;
                }
            }
        }


        private void txtNAMSINH_EditValueChanged(object sender, EventArgs e)
        {
            if (txtNAMSINH.Text.Trim().Length == 4)
            {
                DateTime ngayHT = Func.getSysDatetime();
                if (Func.Parse(txtNAMSINH.Text.Trim()) > ngayHT.Year)
                {
                    MessageBox.Show("Năm sinh không được lớn hơn năm hiện tại");
                    txtNAMSINH.Text = "";
                    return;
                }
                else
                {
                    txtTUOI.Text = (ngayHT.Year - Func.Parse(txtNAMSINH.Text.Trim())).ToString();
                }
                cboDVTUOI.SelectedIndex = 0;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            loadGridData(1, null);
        }

        private void save()
        {
            string _NGAYAPDUNGTU = txtNGAYAPDUNGTU.Text;
            if (NGAYAPDUNGBH.SelectedIndex == 0) { // ngày vào viện
                _NGAYAPDUNGTU = hidNGAYTIEPNHAN;
            }
            else if (NGAYAPDUNGBH.SelectedIndex == 1)// ngày hiện tại
            {
                _NGAYAPDUNGTU = Func.getSysDatetime().ToString("dd/MM/yyyy HH:mm:ss");
            } else {
                _NGAYAPDUNGTU = _NGAYAPDUNGTU + " 00:00:00";
            }

            string _TYLEMIENGIAM = "", _DOITUONGDB = cboDOITUONGDB.SelectValue;
            if (cboDOITUONGDB.SelectValue != "0")
            {
                _TYLEMIENGIAM = cboDOITUONGDB.SelectValueByColumnName(2);
            } else {
                _TYLEMIENGIAM = "";
                _DOITUONGDB = "";
            }


            string _CV_CHUANDOANGIOITHIEU = "";
            string _CV_TKMANOIGIOITHIEU1 = "";
            string _CV_CHUYENVIEN_HINHTHUCID1 = "";
            string _CV_CHUYENVIEN_LYDOID1 = "";
            string _CV_CHUYENDUNGTUYEN1 = "";
            string _CV_CHUYENVUOTTUYEN1 = "";

            string data_ar_ch_chuyendt = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", pars_ch_chuyendt);
            if (data_ar_ch_chuyendt == "1")
            {
                _CV_CHUANDOANGIOITHIEU = txtCHANDOANTUYENDUOI.SelectedValue;
                _CV_TKMANOIGIOITHIEU1 = cboMANOIGIOITHIEU.SelectedValue;
            }
            else
            {
                // START - HISL2TK-411 -- hongdq-- 09052018
                _CV_CHUANDOANGIOITHIEU = dtChuyenVien.Rows[0]["ucCDTD"].ToString();
                _CV_TKMANOIGIOITHIEU1 = dtChuyenVien.Rows[0]["ucBenhVien"].ToString();
                _CV_CHUYENVIEN_HINHTHUCID1 = dtChuyenVien.Rows[0]["ucHinhThucChuyen"].ToString();
                _CV_CHUYENVIEN_LYDOID1 = dtChuyenVien.Rows[0]["ucLyDoChuyen"].ToString();

                if (dtChuyenVien.Rows[0]["rbtChuyen"].ToString() == "1")
                {
                    _CV_CHUYENDUNGTUYEN1 = "1";
                    _CV_CHUYENVUOTTUYEN1 = "0";
                }
                else
                {
                    _CV_CHUYENDUNGTUYEN1 = "0";
                    _CV_CHUYENVUOTTUYEN1 = "1";
                }
            }

            //{ "func":"ajaxCALL_SP_S","params":["NTU01H022.EV004",
            // "{"MABHYT":"TE1401100000559","BHYT_BD":"01/07/2018","BHYT_KT":"30/06/2024","TKDKKBBD":"40000"
            //,"DIACHI_BHYT":"111-Xã Phú Sơn-Huyện Tân Kỳ-Nghệ An","TKNOICD":"","TENNGUOITHAN":"A","TENBENHNHAN":"HANH_0207"
            //,"NGAYSINH":"01/07/2018","NAMSINH":"2018","TUOI":"1","NGAYAPDUNGTU":"30/10/2018 00:00:00"
            //,"NGAYAPDUNGDEN":"30/10/2018 23:59:59","TKMANOIGIOITHIEU":"","TKMACHANDOANTUYENDUOI":"","CHANDOANTUYENDUOI":""
            //,"DOITUONGBENHNHANID":"1","DKKBBDID":"40000","DKKBBD":"","TUYENID":"1","MAVUNGID":null,"HINHTHUCVAOVIENID":"3"
            //,"NOICDID":null,"DOITUONGDB":"","DVTUOI":"3","GIOITINHID":"2","MANOIGIOITHIEU":null,"TENNOIGIOITHIEU":""
            //,"HOSOBENHANID":"119029","TIEPNHANID":"96752","BENHNHANID":"46425","NGAYTIEPNHAN":"02/07/2018 08:20:23"
            //,"MATINH":"","CV_CHUANDOANGIOITHIEU":"","CV_TKMANOIGIOITHIEU1":null,"CV_CHUYENVIEN_HINHTHUCID1":""
            //,"CV_CHUYENVIEN_LYDOID1":"","CV_CHUYENDUNGTUYEN1":"","CV_TKNOICD":"","CHECKCONG":"1","THEMTHE":"0","GIAHANTHE":"0"
            //,"NOTHE":"0","SINHTHETE":"1","BH5NAM":"0","BH6THANG":"0","KHONGCHUYEN_DICHVU":"1","TYLEMIENGIAM":"","CONFIG":"1"
            //,"CDT_KT":null,"SUB_DTBNID":"38"}
            //"],"uuid":"7d81bed7-14cf-4b4d-8fba-10dee27557c9"}
            Object objData = new
            {
                MAVUNGID = cboMAVUNGID.SelectValue,
                HINHTHUCVAOVIENID = cboHINHTHUCVAOVIENID.SelectValue,
                NOICDID = cboNOICD.SelectedValue,
                DOITUONGDB = _DOITUONGDB,
                DVTUOI = (cboDVTUOI.SelectedIndex + 1).ToString(),
                GIOITINHID = cboGIOITINHID.SelectValue,
                MANOIGIOITHIEU = cboMANOIGIOITHIEU.SelectedValue,
                TENNOIGIOITHIEU = cboMANOIGIOITHIEU.SelectedText,
                HOSOBENHANID = hidHOSOBENHANID,
                TIEPNHANID = hidTIEPNHANID,
                BENHNHANID = hidBENHNHANID,
                NGAYTIEPNHAN = hidNGAYTIEPNHAN,
                MATINH = "",
                CV_CHUANDOANGIOITHIEU = _CV_CHUANDOANGIOITHIEU,
                CV_TKMANOIGIOITHIEU1 = _CV_TKMANOIGIOITHIEU1,
                CV_CHUYENVIEN_HINHTHUCID1 = _CV_CHUYENVIEN_HINHTHUCID1,
                CV_CHUYENVIEN_LYDOID1 = _CV_CHUYENVIEN_LYDOID1,
                CV_CHUYENDUNGTUYEN1 = _CV_CHUYENDUNGTUYEN1,
                CV_TKNOICD = dtChuyenVien.Rows[0]["ucBenhVien"].ToString(),

                CV_CHUYENVUOTTUYEN1 = _CV_CHUYENVUOTTUYEN1,

                CHECKCONG = chkCHECKCONG.Checked ? "1" : "0",
                THEMTHE = chkTHEMTHE.Checked ? "1" : "0",
                GIAHANTHE = chkGIAHANTHE.Checked ? "1" : "0",
                NOTHE = chkNOTHE.Checked ? "1" : "0",

                SINHTHETE = chkSINHTHETE.Checked ? "1" : "0",
                BH5NAM = chkBH5NAM.Checked ? "1" : "0",
                BH6THANG = chkBH6THANG.Checked ? "1" : "0",
                KHONGCHUYEN_DICHVU = chkKHONGCHUYEN_DICHVU.Checked ? "1" : "0",

                TYLEMIENGIAM = cboDOITUONGDB.SelectValue,
                CONFIG = (config != null && config != "") ? config : "-1",


                MABHYT = txtMABHYT.Text,
                BHYT_BD = txtBHYT_BD.Text,
                BHYT_KT = txtBHYT_KT.Text,
                TKDKKBBD = cboDKKBBDID.SelectedValue,
                DIACHI_BHYT = txtDIACHI_BHYT.Text,

                TKNOICD = cboNOICD.SelectedValue,
                TENNGUOITHAN = txtTENNGUOITHAN.Text,
                TENBENHNHAN = txtTENBENHNHAN.Text,
                NGAYSINH = txtNGAYSINH.Text,
                NAMSINH = txtNAMSINH.Text,
                CDT_KT = func_kt,

                TUOI = txtTUOI.Text,
                NGAYAPDUNGTU = _NGAYAPDUNGTU,
                NGAYAPDUNGDEN = txtNGAYAPDUNGDEN.Text == "" ? "-1" : txtNGAYAPDUNGDEN.Text + " 23:59:59",
                TKMANOIGIOITHIEU = cboMANOIGIOITHIEU.SelectedValue,
                TKMACHANDOANTUYENDUOI = txtCHANDOANTUYENDUOI.SelectedValue,

                CHANDOANTUYENDUOI = txtCHANDOANTUYENDUOI.SelectedText,
                DOITUONGBENHNHANID = cboDOITUONGBENHNHANID.SelectValue,
                DKKBBDID = cboDKKBBDID.SelectedValue,
                DKKBBD = cboDKKBBDID.SelectedText,
                TUYENID = cboTUYENID.SelectValue,

                SUB_DTBNID = isDtkhaibao ? cboDOITUONGBENHNHANID.SelectValueByColumnName(2) : "0",

            };

            string parJson = Newtonsoft.Json.JsonConvert.SerializeObject(objData).Replace("\"", "\\\"");

            //{"result": "{\"RESULT\":\"1\",\"DVTHUTIEN\":\"\",\"DVGIA\":\"\"}","out_var": "[]","error_code": 0,"error_msg": ""}
            string result = RequestHTTP.call_ajaxCALL_SP_S_result("NTU01H022.EV004", parJson);
            if (result == "0")
            {
                MessageBox.Show("Có lỗi khi chuyển đối tượng");
                /*} else if(result == 2){
                    MessageBox.Show('Tồn tại bệnh nhân đang điều trị sử dụng thẻ BHYT này. Vui lòng kiểm tra lại dữ liệu');*/
            }
            else if (result == "3")
            {
                MessageBox.Show("Tất cả các dịch vụ đã được thu tiền, không thể chuyển đối tượng bảo hiểm cho bệnh nhân");
            }
            else if (result == "4")
            {
                MessageBox.Show("Tồn tại ký tự đặc biệt trong địa chỉ hoặc tên bệnh nhân");
            }
            else if (result == "5")
            {
                MessageBox.Show("Đã tồn tại thẻ này trong lịch sử điều trị của bệnh nhân, không thể chuyển thông tin");
            }
            else
            {
                var msgAlert = "Chuyển đối tượng thành công";
                DataTable dt = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(result, (typeof(DataTable)));
                if (dt == null) dt = new DataTable();

                if (dt.Rows[0]["RESULT"].ToString() == "1")
                {
                    if (dt.Rows[0]["DVTHUTIEN"].ToString() != "")
                    {
                        msgAlert = msgAlert + ". Các dịch vụ đã thu tiền, không thể chuyển đối tượng:" + dt.Rows[0]["DVTHUTIEN"].ToString();
                    }

                    if (dt.Rows[0]["DVGIA"].ToString() != "")
                    {
                        msgAlert = msgAlert + ". Các dịch vụ có giá không hợp lệ để chuyển đối tượng:" + dt.Rows[0]["DVGIA"].ToString();
                    }

                    MessageBox.Show(msgAlert);
                    chkTHEMTHE.Checked = false;
                    chkGIAHANTHE.Checked = false;

                    loadGridChuyenDoiTuong(1, null);
                    loadGridDataDichVu(1, null);
                    loadGridData(1, null);

                }
                else if (dt.Rows[0]["RESULT"].ToString() == "0")
                {
                    msgAlert = "Chuyển đối tượng không thành công.";
                    if (dt.Rows[0]["DVTHUTIEN"].ToString() != "")
                    {
                        msgAlert = msgAlert + ". Các dịch vụ đã thu tiền, cần hủy để chuyển đối tượng:" + dt.Rows[0]["DVTHUTIEN"].ToString();
                    }

                    if (dt.Rows[0]["DVGIA"].ToString() != "")
                    {
                        msgAlert = msgAlert + ". Các dịch vụ có giá không hợp lệ để chuyển đối tượng:" + dt.Rows[0]["DVGIA"].ToString();
                    }

                    MessageBox.Show(msgAlert);
                }
                //Begin_HaNv_06072018: thong bao mabenhan trung the BHYT - L2DKHN-846
                else if (dt.Rows[0]["RESULT"].ToString() == "2")
                {
                    MessageBox.Show("Tồn tại bệnh nhân có mã bệnh án " + dt.Rows[0]["MABENHAN"].ToString() + " sử dụng thẻ BHYT này. Vui lòng kiểm tra lại dữ liệu");
                }
                //End_HaNv_06072018
                else
                {
                    MessageBox.Show("Có lỗi khi chuyển đối tượng");
                }
            }
        }
        private void saveData()
        {
            bool checkCongBhyt = chkCHECKCONG.Checked;
            if (checkCongBhyt == true && cboDOITUONGBENHNHANID.SelectValue == "1"
        && txtMABHYT.Text.Substring(0, 3) != "NTH" && chkSINHTHETE.Checked == false)
            {
                string mathe = txtMABHYT.Text.Trim();
                string tenbn = txtTENBENHNHAN.Text.Trim();
                string namsinh = txtNGAYSINH.Text.Trim() != "" ? txtNGAYSINH.Text.Trim() : txtNAMSINH.Text.Trim();
                string gioitinhid = cboGIOITINHID.SelectValue;

                string noidk = cboDKKBBDID.SelectedValue;
                string ngaybd = txtBHYT_BD.Text;
                string ngaykt = txtBHYT_KT.Text;

                VNPT.HIS.CommonForm.wsBHYT_LichSu_respons_2018 ret1 = VNPT.HIS.CommonForm.ServiceBHYT.Get_History010118(
                    mathe, tenbn, namsinh
                    , gioitinhid, noidk, ngaybd, ngaykt, "0"
                    );

                //string ret1 = _checkCongBHYT(i_u, i_p, mathe.trim(), tenbn, namsinh.trim(), gioitinhid, noidk, ngaybd, ngaykt, "0");
                if (ret1.maKetQua != "" && ret1.gtTheDen != "" && ret1.gtTheDen != "null" && ret1.gtTheDen != null)
                {
                    // confirm xac nhan fill 2 thong tin sau: hoten, ngaysinh
                    // tu dong fill cac thong tin sau: gioitinh, makv, ngaydu5nam, ngaytu, ngayden 
                    cboGIOITINHID.SelectValue = ret1.gioiTinh == "Nam" ? "1" : "2"; 
                    txtBHYT_BD.Text = ret1.gtTheTu;
                    txtBHYT_KT.Text = ret1.gtTheDen;
                }

                string msg = ret1.maKetQua == "004" ? "" : Func.LayNoiDungLoiCheckBHYT(ret1.maKetQua, "0");
                ret1.maKetQua = ret1.maKetQua;

                if (msg != "")
                {
                    if (!tuDongFillBhxh)
                    {
                        MessageBox.Show(ret1.ghiChu);
                        return;
                    }
                    else
                    {
                        if (ret1.maKetQua != "" && ret1.gtTheDen != "" && ret1.gtTheDen != "null" && ret1.gtTheDen != null)
                        {
                            if (ret1.maKetQua == "060" || ret1.maKetQua == "061" || ret1.maKetQua == "070")
                            {
                                DialogResult dialogResult = MessageBox.Show(ret1.ghiChu + ". Bạn có muốn cập nhật lại thông tin từ Cổng BHXH ?", "", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    txtTENBENHNHAN.Text = ret1.hoTen.ToUpper();
                                    if (ret1.ngaySinh.Length == 4)
                                    {
                                        txtNAMSINH.Text = ret1.ngaySinh;

                                        txtNGAYSINH.Text = "";

                                        //txtNAMSINH").change();
                                    }
                                    else
                                    {
                                        txtNGAYSINH.Text = ret1.ngaySinh;

                                        //txtNGAYSINH").change();
                                    }

                                    save();
                                }
                            }
                            else
                            {
                                MessageBox.Show(ret1.ghiChu);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show(ret1.ghiChu);
                            return;
                        }
                    }
                }
                else
                {
                    if (ret1.maKetQua == "004")
                    {
                        MessageBox.Show(ret1.ghiChu + ". Bệnh nhân sẽ được tiếp đón bình thường.");
                        save();
                    }
                    else
                    {
                        save();
                    }
                }
            }
            else
            {
                save();
            }
        }

        private string _sinhSoTheBHYT()
        {
            string ret = "";
            try
            {
                DateTime ngaysinh = Func.ParseDatetime(hidNGAYSINH);
                string namsinh = ngaysinh.ToString("ddMMyyyy");
                ret = RequestHTTP.call_ajaxCALL_SP_S_result("NTU01H022.EV006", hidTIEPNHANID + "$" + namsinh);
                return ret;
            }
            catch (Exception ex) { }

            return ret;
        }

        private bool _kiemTraSinhSoTheBHYT()
        {
            if (hidNGAYSINH.Trim() == "")
            {
                MessageBox.Show("Bệnh nhân chưa có ngày sinh. Vui lòng cập nhật thông tin hành chính cho bệnh nhân");

                return false;
            }
            else if (cboDOITUONGBENHNHANID.SelectValue != "1")
            {
                MessageBox.Show("Bệnh nhân phải là đối tượng bảo hiểm y tế");
                cboDOITUONGBENHNHANID.Focus();

                return false;
            }
            else
            {
                try
                {
                    DateTime ngaysinh = Func.ParseDatetime(hidNGAYSINH);
                    DateTime ngayHT = Func.getSysDatetime();
                    int diff = (ngayHT.Year - ngaysinh.Year) * 12 + ngayHT.Month - ngaysinh.Month;// số tháng chênh lệch

                    if (diff > 72)
                    {
                        MessageBox.Show("Bệnh nhân đã quá 6 tuổi");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }
        private string _sinhSoNoThe()
        {
            string ret = RequestHTTP.call_ajaxCALL_SP_S_result("NTU01H022.EV005", hidTIEPNHANID);
            return ret;
        }

        private bool _validateParam()
        {
            //        if (txtTG_NHAPVIEN_TU').val().trim().length > 0 && (!datetimeRegex.test(txtTG_NHAPVIEN_TU').val()) || !checkDate(txtTG_NHAPVIEN_TU').val()))){
            //            MessageBox.Show("Từ ngày " + $.i18n("date_type_invalid"), function(){
            //txtTG_NHAPVIEN_TU').focus();
            //            });
            //            return false;
            //        }

            //        if (txtTG_NHAPVIEN_DEN').val().trim().length > 0 && (!datetimeRegex.test(txtTG_NHAPVIEN_DEN').val()) || !checkDate(txtTG_NHAPVIEN_DEN').val()))){
            //            MessageBox.Show("Đến ngày " + $.i18n("date_type_invalid"), function(){
            //txtTG_NHAPVIEN_DEN').focus();
            //            });
            //            return false;
            //        }

            //        if (txtTG_NHAPVIEN_DEN').val().trim().length > 0 && txtTG_NHAPVIEN_TU').val().trim().length > 0 && !compareDate(txtTG_NHAPVIEN_TU').val().trim(),txtTG_NHAPVIEN_DEN').val().trim(), 'DD/MM/YYYY')){
            //            MessageBox.Show('Thời gian bắt đầu không được nhỏ hơn thời gian kết thúc');
            //            return false;
            //        }

            return true;

        }
        private bool validateCDT()
        {
            return true;
        }

        private void lblCDTDIACHI_Click(object sender, EventArgs e)
        {

        }

        private void ucGridview2_Load(object sender, EventArgs e)
        {

        }

        private void GridDataDichVu_Load(object sender, EventArgs e)
        {

        }

        private void txtNAMSINH_KeyPress(object sender, KeyPressEventArgs e)
        {
            // chỉ cho nhập số
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            //if (e.Handled == false && !char.IsControl(e.KeyChar) && txtNamsinh.Text.Length >= 4) e.Handled = true; 
        }

        private void txtNGAYSINH_EditValueChanged(object sender, EventArgs e)
        {
            tinhTuoi(txtNGAYSINH.DateTime.ToString("dd/MM/yyyy"), hidNGAYTIEPNHAN.Substring(0, 10));
        }
    }
}