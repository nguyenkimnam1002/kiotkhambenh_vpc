using System; 
using System.Data; 
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.Duoc.subform
{
    public partial class DUC47T001_NGT_YeuCauNhapThuoc : DevExpress.XtraEditors.XtraForm
    {
        string nhapxuatid;
        string kieu;
        string hinhthuc;
        string ht;
        string edit;
        string khoid;

        //string canhBao;
        //string duyetthuocchan = "0";
        DataTable dsthuoc =  new DataTable(); 
        public DUC47T001_NGT_YeuCauNhapThuoc(string nhapxuatid, string kieu, string hinhthuc, string ht, string edit, string khoid)
        {
            InitializeComponent();

            this.nhapxuatid = nhapxuatid;
            this.kieu = kieu;
            this.hinhthuc = hinhthuc;
            this.ht = ht;
            this.edit = edit;
            this.khoid = khoid;
        }
        private void DUC47T001_NGT_YeuCauNhapThuoc_Load(object sender, EventArgs e)
        {
            _initControl();
           // _bindEvent();

            if (this.edit != "1") btnTuChoiPhieu.Enabled = false;
        }
        private void checkRole()
        {
            //    var _parPQ = 'DUC01S002_PhieuYeuCau' + '%ht=' + this.hinhthuc + '$';
            //    var result = jsonrpc.AjaxJson.ajaxCALL_SP_O("DUC.PQSCREEN.03", _parPQ);
            //    for (var i = 0; i < result.length; i++)
            //    {
            //        if (result[i].ROLES == '1')
            //$('#' + result[i].ELEMENT_ID).show();
            //        if (result[i].ROLES == '0' || result[i].ROLES == '')
            //$('#' + result[i].ELEMENT_ID).hide();
            //    }

            //string _parPQ = "DUC01S002_PhieuYeuCau" + "%ht=" + this.ht + "$";
            //DataTable result = RequestHTTP.call_ajaxCALL_SP_O("DUC.PQSCREEN.03", _parPQ);
            //for (int i = 0; i < result.Rows.Count; i++)
            //{
            //    if (control_name == "btnGoDuyet" && result.Rows[i]["ELEMENT_ID"].ToString() == "btnGoDuyet")
            //    {
            //        if (result.Rows[i]["ROLES"].ToString() == "1")
            //            btnGoDuyet.Visible = true;
            //        //$('#' + result.Rows[i]["ELEMENT_ID"].ToString()).prop('disabled', false);

            //        if (result.Rows[i]["ROLES"].ToString() == "0" || result.Rows[i]["ROLES"].ToString() == "")
            //            btnGoDuyet.Visible = false;
            //        //$('#' + result.Rows[i]["ELEMENT_ID"].ToString()).prop('disabled', true);
            //    }
            //    else if (control_name == "btnGoNhapkho" && result.Rows[i]["ELEMENT_ID"].ToString() == "btnGoNhapkho")
            //    {
            //        if (result.Rows[i]["ROLES"].ToString() == "1")
            //            btnGoNhapkho.Visible = true;
            //        if (result.Rows[i]["ROLES"].ToString() == "0" || result.Rows[i]["ROLES"].ToString() == "")
            //            btnGoNhapkho.Visible = false;
            //    }
            //} 


        }
        private void cmdDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _initControl()
        {
            //string cauhinh = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH", "PHARMA_CANHBAO_DUYET_LOAITHUOC");
            //canhBao = cauhinh;// cauhinh.Split(',');

            //duyetthuocchan = RequestHTTP.call_ajaxCALL_SP_S_result("COM.CAUHINH.THEOMA", "PHARMA_DUYET_THUOC_CHAN");

            //if (this.hinhthuc == "13")
            //    layout_divBNKhongLayThuoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //else
            //    layout_divBNKhongLayThuoc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            dsthuoc = RequestHTTP.call_ajaxCALL_SP_O("DUC01S002_DS_GROUP", this.nhapxuatid);

            _loadgrid(1, null);

            _load_chitiet_phieu();

            gridComplete();
        }

        private void _loadgrid(object sender, EventArgs e)
        {
            //postData: { "func":"ajaxExecuteQueryPaging","uuid":"eyJhbGciOiJIUzUxMiJ9.eyJpYXQiOjE1NDQ0Mjc4MjIsInN1YiI6IlRUS0hBX0FETUlOIiwiaXNzIjoiIiwiZXhwIjoxNTQ0NDMxNDIyfQ.8Um39y9397U6yhefL7xkVntVCUPO69wAG3fN8XkdNIMczl5rJeW7Rlujom3ao5WvPITePqbtReodNGKsh_u9Qw","params":["DUC01S002_THUOC01"],"options":[{"name":"[0]","value":"285478"}]}
            //_search: false
            //nd: 1544427903513
            //rows: 1000
            //page: 1
            //sidx: 
            //sord: asc
            int page = (int)sender;
            if (page > 0)
            {
                ResponsList responses = new ResponsList();

                //if (duyetthuocchan == "1" && this.hinhthuc == "12")
                //    responses = RequestHTTP.get_ajaxExecuteQueryPaging("DUC47T001.20", page, gridList_THUOC.getNumberPerPage(),
                //        new String[] { "[0]" }, new string[] { nhapxuatid },
                //        gridList_THUOC.jsonFilter());
                //else 
                if (this.hinhthuc == "12" || this.hinhthuc == "13")
                    responses = RequestHTTP.get_ajaxExecuteQueryPaging("DUC01S002_THUOC01", page, gridList_THUOC.getNumberPerPage(),
                        new String[] { "[0]" }, new string[] { nhapxuatid },
                        gridList_THUOC.jsonFilter());
                else
                    responses = RequestHTTP.get_ajaxExecuteQueryPaging("DUC01S002.CTTHUOC", page, gridList_THUOC.getNumberPerPage(),
                        new String[] { "[0]" }, new string[] { nhapxuatid },
                        gridList_THUOC.jsonFilter());

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MA", "TEN","LIEULUONG", "TEN_DVT"
                    , "SOLUONG", "SLKHADUNG", "SOLUONGDUYET"
                    , "GIANHAP", "XUATVAT", "THANHTIEN","SOLO"
                });

                gridList_THUOC.setData(dt, responses.total, responses.page, responses.records);
                gridList_THUOC.setColumnAll(false);
                gridList_THUOC.onIndicator();


                gridList_THUOC.setColumn("MA", "Mã thuốc/VT");
                gridList_THUOC.setColumn("TEN", "Tên thuốc/VT");
                gridList_THUOC.setColumn("LIEULUONG", "Hàm lượng");
                gridList_THUOC.setColumn("TEN_DVT", "Đơn vị");
                gridList_THUOC.setColumn("SOLUONG", "SL yêu cầu");
                gridList_THUOC.setColumn("SLKHADUNG", "SL khả dụng");

                if (this.hinhthuc == "13" || this.hinhthuc == "12")
                    gridList_THUOC.setColumn("SOLUONGDUYET", "SL duyệt");

                gridList_THUOC.setColumn("GIANHAP", "Đơn giá");
                gridList_THUOC.setColumn("XUATVAT", "VAT");
                gridList_THUOC.setColumn("THANHTIEN", "Thành tiền");
                gridList_THUOC.setColumn("SOLO", "Số lô");



                //var sql_par1=[that.opt.nhapxuatId];
                //dsthuoc = jsonrpc.AjaxJson.ajaxCALL_SP_O("DUC01S002_DS_GROUP", sql_par1.join('$'));
                dsthuoc = RequestHTTP.call_ajaxCALL_SP_O("DUC01S002_DS_GROUP", nhapxuatid);

            }

        }

        private void _load_chitiet_phieu()
        {
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC01S002.CTPHIEU", this.nhapxuatid);
//NHAPID: null
//XUATID: null
//NOILAP: Khoa Khám bệnh
//NGAYNX: 25/10/2018 15:47
//NGUOINX: Quản trị hệ thống bệnh viện
//NGUOIDUYET: 
//LANIN: null
//SOCHUNGTU: 
//NGAYCHUNGTU: 
//MA: XK285479
//NHACUNGCAP: 
//CHIETKHAU: null
//GHICHU: BN:.TRẺ EM 5T. (MS:BV00000164;Phieu:BV000000271)
//TIENDON: null
//TONGCONG: 7350
//TONGTIENDATRA: null
//TRANGTHAIID: 5
//TRANGTHAI: Chờ duyệt
//MANHAP: 
//MAXUAT: 
//DUYETVIENPHI: 1
//SOPHIEUBN: 0
            if (data_ar.Rows.Count > 0)
            {
                txtNOILAP.Text = data_ar.Rows[0]["NOILAP"].ToString();
                txtNGAYNX.Text = data_ar.Rows[0]["NGAYNX"].ToString();
                txtNGUOINX.Text = data_ar.Rows[0]["NGUOINX"].ToString();
                txtLANIN.Text = data_ar.Rows[0]["LANIN"].ToString();
                txtSOCHUNGTU.Text = data_ar.Rows[0]["SOCHUNGTU"].ToString();
                txtNGAYCHUNGTU.Text = data_ar.Rows[0]["NGAYCHUNGTU"].ToString();

                txtMA.Text = data_ar.Rows[0]["MA"].ToString();
                txtNHACUNGCAP.Text = data_ar.Rows[0]["NHACUNGCAP"].ToString();
                txtCHIETKHAU.Text = data_ar.Rows[0]["CHIETKHAU"].ToString();
                txtGHICHU.Text = data_ar.Rows[0]["GHICHU"].ToString();

                txtTIENDON.Text = data_ar.Rows[0]["TIENDON"].ToString();
                txtTONGCONG.Text = data_ar.Rows[0]["TONGCONG"].ToString();
                txtTONGTIENDATRA.Text = data_ar.Rows[0]["TONGTIENDATRA"].ToString();

                if (data_ar.Rows[0]["TONGCONG"].ToString() != "" && data_ar.Rows[0]["TIENDON"].ToString() != "")
                    txtTIENCHIETKHAU.Text = (Func.Parse_float(data_ar.Rows[0]["TONGCONG"].ToString()) - Func.Parse_float(data_ar.Rows[0]["TIENDON"].ToString())).ToString();
                 
            }
            txtGHICHU.Focus();
        }

        private bool check_duyet()
        {
            DataTable dt = (DataTable)gridList_THUOC.gridControl.DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (this.hinhthuc == "13" || this.hinhthuc == "12")
                {
                    float Slyeucau = Func.Parse_float(dt.Rows[i]["SOLUONG"].ToString());    // cột 6
                    float SL_KhaDung = Func.Parse_float(dt.Rows[i]["SLKHADUNG"].ToString());// cột 7
                    float SL_Duyet = Func.Parse_float(dt.Rows[i]["SOLUONGDUYET"].ToString());// cột 8

                    if (this.kieu == "2" && (this.hinhthuc == "2" || this.hinhthuc == "9"))
                    {
                        if (SL_Duyet > Slyeucau)
                        { 
                            MessageBox.Show("Số lượng duyệt không được vượt quá số lượng yêu cầu");
                            return true;
                        }
                    }
                    else if (this.hinhthuc != "12" && this.hinhthuc != "13" && this.hinhthuc != "7"
                                && this.hinhthuc != "8" && this.hinhthuc != "6" && this.kieu == "3")
                    {
                        if (SL_Duyet > SL_KhaDung)
                        { 
                            MessageBox.Show("Số lượng duyệt không được vượt quá số lượng khả dụng");
                            return true;
                        }
                    }
                    else if (SL_Duyet < Slyeucau && this.hinhthuc == "12" && this.kieu == "3")
                    {
                        {
                            MessageBox.Show("Số lượng duyệt phải >= " + Slyeucau);
                            return true;
                        }
                    }
                    else
                    {
                        //$('#grdThuoc').jqGrid('setCell', rowid, 11, Func.Parse_float(value) * Func.Parse_float($("#grdThuoc").jqGrid('getCell', rowid, 9)));
                        //                   reloadCash();
                        //                   rowData = $('#grdThuoc').jqGrid('getRowData', rowid);
                        //                   if (Func.Parse_float(rowData['SOLUONG']) > Func.Parse_float(rowData['SLKHADUNG']))
                        //     		$("#grdThuoc").jqGrid('setRowData', rowid, "", {
                        //                       color: 'black'

                        //                   });
                        //       if (Func.Parse_float(rowData['SOLUONGDUYET']) != Func.Parse_float(rowData['SOLUONG']))
                        //     		$("#grdThuoc").jqGrid('setRowData', rowid, "", {
                        //           color: 'red'

                        //                   });
                        //     	else {
                        //     		$("#grdThuoc").jqGrid('setRowData', rowid, "", {
                        //               color: 'black'

                    }
                }
            }
            return true;
        }

        private void gridComplete()
        {

            checkLanhDaoDuyet();
//            var ids = $("#grdThuoc").getDataIDs();
//            for (var i = 0; i < ids.length; i++)
//            {
//                var id = ids[i];
//                var row = $("#grdThuoc").jqGrid(
//                        'getRowData', id);
//                if (this.hinhthuc != 12 && this.hinhthuc != 13 && this.hinhthuc != 7 && this.hinhthuc != 6 && this.kieu == '3')
//                {
//											$('#grdThuoc').jqGrid('setCell', id, 8, row.SOLUONG);
//                }
//                rowData = $('#grdThuoc').jqGrid(
//                        'getRowData', ids[i]);
//                //									if(this.kieu!='2' && this.hinhthuc != '2' &&this.hinhthuc != '9')
//                if (this.kieu != '2')
//                {
//                    if (Func.Parse_float(rowData['SOLUONG']) > Func.Parse_float(rowData['SLKHADUNG'])
//                            && this.hinhthuc != 12 && this.hinhthuc != 13 && this.hinhthuc != 7 && this.hinhthuc != 6 && this.kieu == '3')
//                    {
//											$("#grdThuoc").jqGrid('setCell', ids[i], 'SOLUONGDUYET', rowData['SLKHADUNG']);
//											$("#grdThuoc").jqGrid('setRowData', ids[i], "", { color: 'red' });
//        }
//										if ((this.hinhthuc==12||this.hinhthuc==13||this.hinhthuc==7||this.hinhthuc==6||this.kieu=='2') && 
//												(Func.Parse_float(rowData['SOLUONGDUYET']) > Func.Parse_float(rowData['SOLUONG'])
//														|| Func.Parse_float(rowData['SOLUONGDUYET']) > Func.Parse_float(rowData['SLKHADUNG'])
//												))
//										{ // noi tru thi khong thay doi so luong kha dung thi boi do
//											$("#grdThuoc").jqGrid('setRowData', ids[i], "", { color: 'red' });
//										}
//}
//								else 
//									{
//										if ( Func.Parse_float(rowData['SOLUONGDUYET']) > Func.Parse_float(rowData['SOLUONG']))
//											{
//											$("#grdThuoc").jqGrid('setCell', ids[i], 'SOLUONGDUYET', rowData['SOLUONG']);
//											$("#grdThuoc").jqGrid('setRowData', ids[i], "", { color: 'red' });
//											}
//									}
//								}
        }
        private void checkLanhDaoDuyet()
        {
            DataTable dt = (DataTable)gridList_THUOC.gridControl.DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (canhBao.IndexOf(dt.Rows[i]["LOAI"].ToString()) > -1)// && this.hinhthuc == "13")
                //{
                //    //           $('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td').each(function(index, element) {			        
                //    //$(element).css('background-color', _color);
                //    //$('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td').attr('title', rowData['TENLOAI']);
                //    //           });

                //    canhBaoloaithuoc = dt.Rows[i]["TENLOAI"].ToString(); 
                //}
            }

                    //        var rowIds = $('#grdThuoc').jqGrid('getDataIDs');
                    //        for (i = 0; i < rowIds.length; i++)
                    //        {
                    //            rowData = $('#grdThuoc').jqGrid('getRowData', rowIds[i]);
                    //            var _color = '#FF9900';
                    //            if (rowData['CHOLANHDAODUYET'] == '1')
                    //            {
                    //$("#" + rowIds[i]).find("td").css("background-color", _color);
                    //$('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td').attr('title', rowData['CHUY']);
                    //            }
                    //            if (canhBao.indexOf(rowData["LOAI"]) != -1 && this.hinhthuc == '13')
                    //            {
                    //$("#" + rowIds[i]).find("td").css("background-color", _color);
                    //$('#grdThuoc').find("tr[id='" + rowIds[i] + "']").find('td')
                    //                    .attr('title', rowData['TENLOAI']);
                    //                canhBaoloaithuoc = rowData['TENLOAI'];
                    //            }
                    //        }
                }
        //string canhBaoloaithuoc = "";

        private string _isvalidate()
        {
            DataTable rowIds = (DataTable)gridList_THUOC.gridControl.DataSource;
            string kq = "1";
            float _checksoluong = 0;
            for (var k = 0; k < rowIds.Rows.Count; k++)
            {
                DataRow rowData = rowIds.Rows[k];
                string sl_yeucau = rowData["SOLUONG"].ToString();
                string sl_duyet = rowData["SOLUONGDUYET"].ToString();
                string soDuKhaDungKhoChinh = rowData["SLKHADUNG"].ToString();
                if (Func.Parse_float(sl_duyet) < 0 || sl_duyet == "")
                {
                    MessageBox.Show("Số lượng duyệt không đúng!");
                    kq = "0";
                    _checksoluong = 1; 
                    return kq; 
                }
                if (Func.Parse_float(sl_duyet) > Func.Parse_float(sl_yeucau) && this.hinhthuc != "12" && this.kieu != "3")
                {
                    MessageBox.Show("Số lượng duyệt không được lớn hơn số lượng yêu cầu !");
                    _checksoluong = 1;
                    kq = "0";
                    return kq;
                }
                if (Func.Parse_float(sl_duyet) > Func.Parse_float(soDuKhaDungKhoChinh)
                        && this.hinhthuc != "12"
                        && this.hinhthuc != "13"
                        && this.hinhthuc != "7"
                        && this.hinhthuc != "8"
                        && this.hinhthuc != "6"
                        && this.kieu == "3")
                {
                    MessageBox.Show("Số lượng duyệt không được lớn hơn số lượng khả dụng !");
                    _checksoluong = 1;
                    kq = "0"; 
                    return kq; 
                }
                //bo check
                if ((this.kieu != "0" && this.kieu != "2")
                        && (this.hinhthuc == "2" || this.hinhthuc == "9" || this.hinhthuc == "4"))
                {
                    if (Func.Parse_float(sl_duyet) > Func.Parse_float(soDuKhaDungKhoChinh))
                    { 
                        MessageBox.Show("Thuốc/vật tư:" + rowData["TEN"].ToString() + " chỉ còn đủ để cấp " + soDuKhaDungKhoChinh + " " + rowData["TEN_DVT"].ToString());
                        rowIds.Rows[k][10] = soDuKhaDungKhoChinh;
	    		        //$("#grdThuoc").jqGrid("setCell", rowIds[k], 10, soDuKhaDungKhoChinh);
                        kq = "0";
                        return kq;
                    }
                }

                _checksoluong = _checksoluong + Func.Parse_float(sl_duyet);

            }
            if (_checksoluong == 0)
            {
                MessageBox.Show("Phải có ít nhất 1 thuốc được duyệt !");
                kq = "0"; 
                return kq;
            }
            return kq;
        }
        private void cmdTaoPhieu_Click(object sender, EventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Trong phiếu có thuốc " + canhBaoloaithuoc + ", bạn có muốn duyệt tiếp?", "", MessageBoxButtons.YesNo);
            //if (dialogResult != DialogResult.Yes) return;

            DataTable rows;
            if (this.hinhthuc == "12" || this.hinhthuc == "13")
                rows = dsthuoc;
            else
                rows = (DataTable)gridList_THUOC.gridControl.DataSource;

            DataTable drugs = new DataTable();
            drugs.Columns.Add("NHAPXUATCTID", System.Type.GetType("System.String"));
            drugs.Columns.Add("SOLUONGDUYET", System.Type.GetType("System.String"));
            drugs.Columns.Add("THANHTIEN", System.Type.GetType("System.String"));

            if (rows != null && rows.Rows.Count > 0)
            {
                for (var i = 0; i < rows.Rows.Count; i++)
                {
                    DataRow row = rows.Rows[i];
                    DataRow dr = drugs.NewRow();

                    if (row["SOLUONGDUYET"].ToString() == "")
                    {
                        MessageBox.Show("Số lượng duyệt phải >= 0");
                        return;
                    }
                    else
                    {
                        dr["SOLUONGDUYET"] = row["SOLUONGDUYET"].ToString();
                        dr["THANHTIEN"] = row["THANHTIEN"].ToString(); 
                    }

                    dr["NHAPXUATCTID"] =  row["NHAPXUATCTID"].ToString();
                    drugs.Rows.Add(dr);
                }
            }

            var _check = _isvalidate();

            if (_check == "1")
            {
                string sql_par = Newtonsoft.Json.JsonConvert.SerializeObject(drugs).Replace("\"","\\\"") + "$" + this.nhapxuatid + "$" + txtGHICHU.Text;
                DataTable ret = RequestHTTP.call_ajaxCALL_SP_S_table("DUC47T001.APPR", sql_par);
                if (ret.Rows.Count > 0)
                {
                    if (Func.Parse(ret.Rows[0]["SUCCESS"].ToString()) > 0)
                    {
                        MessageBox.Show("Duyệt phiếu thành công !");
                        { 
                            string[] mang = ret.Rows[0]["MESSAGE"].ToString().Split(',');

                            try
                            {
                                var _nhapid = mang[0];
                                var _xuatid = mang[1];
                                var _ttnhap = mang[2];
                                var _ttxuat = mang[3];

                                // trả lại form cha
                                //EventUtil.raiseEvent("appr_success",{ nhapid: _nhapid,xuatid: _xuatid,ttnhap: _ttnhap,ttxuat: _ttxuat});
                            }
                            catch(Exception ex) { }
                        }

                        //đóng:
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(ret.Rows[0]["MESSAGE"].ToString());
                    }
                }
            }
        }
        private void openForm(Form frm, string optionsPopup = "1")
        {
            if (optionsPopup == "0")
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
            else
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }
        private void txtKHO_LAP_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNGAY_LAP_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void bindEvent()
        {

        }
        private void loadEditorPhieu(string _keyField, int isAddNew)
        {
            //DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC04D001.04", _keyField);
            //if (data_ar.Rows.Count > 0)
            //{
            //    txtNOILAP.Text = data_ar.Rows[0]["NOI_LAP"].ToString();
            //    //txtNGUOI_LAP.Text = data_ar.Rows[0]["NGUOI_LAP"].ToString();
            //    txtNGAYNX.Text = data_ar.Rows[0]["NGAY_LAP"].ToString();
            //    //txtLAN_IN.Text = '0'"].ToString();
            //    txtLANIN.Text = data_ar.Rows[0]["SO_CT"].ToString();
            //    txtNGAYCHUNGTU.Text = data_ar.Rows[0]["NGAY_LAP_SCT"].ToString();

            //    txtNGUOINX.Text = data_ar.Rows[0]["MA_PHIEU"].ToString();

            //    txtSOCHUNGTU.Text = data_ar.Rows[0]["NHA_CUNG_CAP"].ToString();
            //    txtMA.Text = data_ar.Rows[0]["NGUOIGIAO"].ToString();
            //    txtPHIEUYC.Text = data_ar.Rows[0]["DIACHI"].ToString();
            //    txtCHIETKHAU.Text = data_ar.Rows[0]["CHIET_KHAU"].ToString();
            //    txtGHICHU.Text = data_ar.Rows[0]["GHI_CHU"].ToString();


            //    hid_kieu = data_ar.Rows[0]["KIEU"].ToString();

            //    hid_NHACUNGCAPID = data_ar.Rows[0]["NHACUNGCAPID"].ToString();
            //    hid_NHAPID = data_ar.Rows[0]["NHAPID"].ToString();
            //    hid_XUATID = data_ar.Rows[0]["XUATID"].ToString();
            //    loaiphieu = data_ar.Rows[0]["LOAI_PHIEU"].ToString();

            //    float sumAll = Func.Parse_float(data_ar.Rows[0]["TIENDON"].ToString());
            //    float tien_chiet_khau = (sumAll * Func.Parse_float(txtCHIETKHAU.Text.Trim())) / 100;
            //    float tien_thua_vat = Func.Parse_float(data_ar.Rows[0]["TIENTHUEVAT"].ToString());
            //    // h_thanhtoantong").val(sumAll);
            //    tiendon.Text = Func.formatMoneyEng_GiuThapPhan(sumAll.ToString()) + " đ";   //.format(2, 3, ',') + 'đ');	   
            //    thuegtgt.Text = Func.formatMoneyEng_GiuThapPhan(data_ar.Rows[0]["TIENTHUEVAT"].ToString()) + " đ";   // (2, 3, ',') + 'đ');

            //    tienchietkhau.Text = Func.formatMoneyEng_GiuThapPhan(tien_chiet_khau.ToString()) + " đ";
            //    //layout_tongcong.Text = Func.formatMoneyEng_GiuThapPhan((sumAll - tien_chiet_khau).ToString()); //.format(2, 3, ',') + 'đ');
            //    //conlai.Text = Func.formatMoneyEng_GiuThapPhan((sumAll - Func.Parse_float(txtTHANH_TOAN_TONG.Text) - tien_chiet_khau + tien_thua_vat).ToString());
            //    //- Func.Parse_float(tien_chiet_khau) + Func.Parse_float(data_ar.Rows[0]["TIENTHUEVAT"].ToString())).format(2, 3, ',') + 'đ');

            //    var tong = sumAll - tien_chiet_khau + tien_thua_vat;
            //    //tongcong.Text = Func.formatMoneyEng_GiuThapPhan(tong.ToString()) + " đ";

            //    //hid_NHAPXUATID_CHA    ").val(data_ar.Rows[0]["NHAPXUATID_CHA);

            //    henthanhtoan.Text = data_ar.Rows[0]["HEN_THANH_TOAN"].ToString();
            //}
            //else
            //{
            //    if (isAddNew != 0) MessageBox.Show("Không có dữ liệu");
            //    return;
            //}

            //_loadgrid(1, null);
        }

        private void btnTuChoiPhieu_Click(object sender, EventArgs e)
        {

        }
    }
}