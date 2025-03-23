using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using VNPT.HIS.Common;
using VNPT.HIS.Controls.Class;
using System.Reflection;
using VNPT.HIS.Controls.SubForm;

namespace VNPT.HIS.Controls.NgoaiTru
{
    public partial class ucTabVienPhi : DevExpress.XtraEditors.XtraUserControl
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private int LNMBP_XetNghiem = 0;
        private string hosobenhanid = "";
        private string CAU_HINH = "";
        //{"func":"ajaxCALL_SP_I","params":["COM.CAUHINH.THEOMA","VP_IN_PHOI_KHI_DONG_BENHAN"],"uuid":"89eb5722-3dd4-4c7c-abce-cc5be78afe97"}
        //{"func":"ajaxCALL_SP_I","params":["COM.CAUHINH.THEOMA","VP_BS_CHUYEN_DOITUONG"],"uuid":"89eb5722-3dd4-4c7c-abce-cc5be78afe97"}

        //{"func":"ajaxExecuteQuery","params":["","VPI01T004.05"],"options":[],"uuid":"89eb5722-3dd4-4c7c-abce-cc5be78afe97"}
        //[["1","Khám Bệnh"],["2","Ngày Điều Trị Ngoại Trú"],["3","Xét Nghiệm"],["4","Chẩn Đoán Hình Ảnh"],["5","Thăm Dò Chức Năng"],["6","PTTT"],["7","Dịch Vụ Kỹ Thuật Cao"],["8","Mẫu Chế Phẩm Máu"],["10","Thuốc Trong Danh Mục"],["12","Thuốc UT Ngoài Danh Mục"],["13","Thuốc Theo Tỷ Lệ"],["15","Vật Tư Trong Danh Mục"],["18","Vật Tư Theo Tỷ Lệ"],["19","Vận Chuyển"],["20","Ngày Giường Chuyên Khoa"],["23","Thuốc vật tư không thuộc danh mục quản lý của BV"]]

        //{"func":"ajaxExecuteQuery","params":["","VPI01T004.11"],"options":[{"name":"[0]","value":"8557"}],"uuid":"89eb5722-3dd4-4c7c-abce-cc5be78afe97"}
        //[["4001","Khoa Khám bệnh"]]

        //{"func":"ajaxExecuteQuery","params":["","VPI01T004.06"],"options":[{"name":"[0]","value":"8557"}],"uuid":"89eb5722-3dd4-4c7c-abce-cc5be78afe97"}
        //[["8557","TN000005597"]]

        //{"func":"ajaxCALL_SP_O","params":["VPI01T004.01","8557",0],"uuid":"89eb5722-3dd4-4c7c-abce-cc5be78afe97"}

        public ucTabVienPhi()
        {
            InitializeComponent();
        }

        private void ucTabVienPhi_Load(object sender, EventArgs e)
        {
            Xoa();

            ucGrid_DanhSach.setEvent_FocusedRowChanged(DanhSach_SelectRow);
            ucGrid_DanhSach.setEvent(getData_table);
            ucGrid_DanhSach.setMultiSelectMode(true);

            ucGrid_DanhSach.onIndicator();
            ucGrid_DanhSach.gridView.OptionsView.ColumnAutoWidth = false;
        }
        string KHAMBENHID = "";
        string TIEPNHANID = "";
        public void reload()
        {
            getData();
        }
        public bool allow_tab_reload = false;
        public void loadData(string _KHAMBENHID, string _TIEPNHANID)
        {
            if (allow_tab_reload == false)
                if (KHAMBENHID == _KHAMBENHID) return;

            KHAMBENHID = _KHAMBENHID;
            TIEPNHANID = _TIEPNHANID;

            getData();
        } 
        private void getData()
        {
            try
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
                DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(typeof(DevExpress.XtraWaitForm.AutoLayoutDemoWaitForm));
                getData_table(1, null);

                Xoa();
                DataTable result = VNPT.HIS.Common.RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.05", TIEPNHANID, 0);
                DataTable _dagiaodich = VNPT.HIS.Common.RequestHTTP.call_ajaxCALL_SP_O("VPI01T001.06", TIEPNHANID, 0);
                if (result.Rows.Count > 0)
                {
                    //txtTongChiPhi.Text = Func.formatMoney(dt.Rows[0]["TONGTIENDV"].ToString());
                    //txtBHTra.Text = Func.formatMoney(dt.Rows[0]["BHYT_THANHTOAN"].ToString());
                    //txtBenhNhanTra.Text = Func.formatMoney(dt.Rows[0]["BNTRA"].ToString());

                    //txtTamUng.Text = Func.formatMoney(dt2.Rows[0]["TAMUNG"].ToString());
                    //txtMienGiam.Text = Func.formatMoney(dt2.Rows[0]["MIENGIAM"].ToString());
                    //txtDaThanhToan.Text = Func.formatMoney(dt2.Rows[0]["DANOP"].ToString());

                    //txtPhaiNop.Text = Func.formatMoney(dt2.Rows[0]["TIEN_PHAINOP"].ToString());
                    //txtThanhToanThem.Text = Func.formatMoney(dt2.Rows[0]["VIENPHI"].ToString());
                    double DANOP = Func.Parse_double(_dagiaodich.Rows[0]["DANOP"].ToString());
                    double TAMUNG = Func.Parse_double(_dagiaodich.Rows[0]["TAMUNG"].ToString()) - Func.Parse_double(_dagiaodich.Rows[0]["HOANUNG"].ToString());
                    double HOANUNG = Func.Parse_double(_dagiaodich.Rows[0]["HOANUNG"].ToString());

                    double x = Func.Parse_double(result.Rows[0]["TONGTIENDV"].ToString());
                    double y = Func.Parse_double(result.Rows[0]["BHYT_THANHTOAN"].ToString());
                    double z = Func.Parse_double(result.Rows[0]["MIENGIAMDV"].ToString());

                    double VIENPHI = Func.Parse_double(result.Rows[0]["TONGTIENDV"].ToString())
                        - Func.Parse_double(result.Rows[0]["BHYT_THANHTOAN"].ToString())
                        - Func.Parse_double(result.Rows[0]["MIENGIAMDV"].ToString());

                    double NOPTHEM = VIENPHI - DANOP;
                    double CHENHLECH = TAMUNG - NOPTHEM;


                    txtTongChiPhi.Text = Func.formatMoneyEng(result.Rows[0]["TONGTIENDV"].ToString());
                    txtBHTra.Text = Func.formatMoneyEng(result.Rows[0]["BHYT_THANHTOAN"].ToString());

                    txtBenhNhanTra.Text = Func.formatMoneyEng(VIENPHI);

                    txtTamUng.Text = Func.formatMoneyEng(TAMUNG);

                    //txtMienGiam.Text = Func.formatMoneyEng(dt2.Rows[0]["MIENGIAM"].ToString());

                    txtDaThanhToan.Text = Func.formatMoneyEng(DANOP);

                    txtPhaiNop.Text = Func.formatMoneyEng(CHENHLECH);

                    txtThanhToanThem.Text = Func.formatMoneyEng(NOPTHEM);
                }
            }
            finally
            {
                //Close Wait Form
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            }

            //{"result": "[{\n\"TONGTIENDV\": \"52175660\",\n\"TONGTIENDV_BH\": \"51475660\",\n\"BHYT_THANHTOAN\": \"41180528\",\n\"BNTRA\": \"10995132\"
            // ,\n\"MIENGIAMDV\": \"0\",\n\"DAMIENGIAM\": \"0\",\n\"BHYT_DANOP\": \"0\",\n\"DANOP\": \"0\",\n\"TRAN_BHYT\": \"195000\",\n\"FREE\": \"0\"
            // ,\n\"TRAITUYEN\": \"0\",\n\"TYLE_BHYT_HIENTAI\": \"80\",\n\"TYLE_BHYT\": \"100\",\n\"TYLE_THE\": \"80\"}]",
        }
        private void getData_table(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ucGrid_DanhSach.clearData();

                string nhombhyt = "-1";// tìm kiếm theo mã nhóm bhyt,nay đã bỏ- để mặc định là tất cả
                string input = ""; // là từ khóa tìm kiếm, trước đây có phần nhập thêm điều kiện searcg-nay đã bỏ- để mặc định là tất cả
                string khoaid = "-1";// là từ khóa tìm kiếm,nay đã bỏ- để mặc định là tất cả

                //_sql_par =   [{ "name":"[0]", "value":_tiepnhanid},
                //       { "name":"[1]", "value": _nhombhyt},
                //       { "name":"[2]", "value": input},
                //       { "name":"[3]", "value": _khoaid},
                //       { "name":"[4]", "value":-1},
                //       { "name":"[5]", "value":-1},
                //       { "name":"[6]", "value":-1}
                //       ];
                //params":["VPI01T004.04"],"options":[{"name":"[0]","value":"96574"},{"name":"[1]","value":-1},{"name":"[2]","value":""},{"name":"[3]","value":"4902"},{"name":"[4]","value":-1},{"name":"[5]","value":-1},{"name":"[6]","value":-1}]}			                
                ResponsList ds = RequestHTTP.get_ajaxExecuteQueryPaging("VPI01T004.04", page, ucGrid_DanhSach.ucPage1.getNumberPerPage()
                    , new String[] { "[0]", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]" }
                    , new string[] { TIEPNHANID, nhombhyt, input, khoaid, "-1", "-1", "-1" }
                    , ucGrid_DanhSach.jsonFilter() 
                    
                    );

                //ResponsList ds = ServiceTabDanhSachBenhNhan.getDsVienPhi(page, ucGrid_DanhSach.ucPage1.getNumberPerPage(), TIEPNHANID, nhombhyt, input, khoaid);
                DataTable dt = MyJsonConvert.toDataTable(ds.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "NGAYDICHVU", "SOPHIEU", "TENDICHVU", "SOLUONG", "DONVI", "TIENDICHVU"
                        , "THANHTIEN", "TIEN_BHYT_TRA", "TIEN_MIENGIAM", "TYLE_BHYT_TRA", "LOAITTMOI", "KHOA", "PHONG", "NGUOICHIDINH" });

                
                    ucGrid_DanhSach.setData(dt, ds.total, ds.page, ds.records);
                    ucGrid_DanhSach.setColumnAll(false);

                    //"Ngày,NGAYDICHVU,110,0,f,c;" +
                    //"Số phiếu,SOPHIEU,80,0,f,l;" +
                    //"Mã dịch vụ,MADICHVU,100,0,f,l;" +
                    //"Nhóm BHYT,TENNHOM,150,0,f,l;" +
                    //"Tên thuốc + dịch vụ,TENDICHVU,200,0,f,l;" +
                    //"SL,SOLUONG,50,number!3,f,l;" +
                    //"Đơn vị,DONVI,60,0,f,l;" +
                    //"Giá tiền,TIEN_DICHVU,80,number,f,r;" +
                    //"Thành tiền,THANHTIEN,80,number,f,r;" +

                    //"BHYT trả,TIEN_BHYT_TRA,80,number,f,r;" +
                    //"Miễn giảm,TIEN_MIENGIAM,80,number,f,r;" +
                    //"Tỷ lệ TT,TYLE_BHYT_TRA,60,0,f,r;" +
                    //"Loại TT,LOAITTMOI,80,0,f,l;" +
                    //"Lý do thay loại thanh toán,LYDO,120,0,t,l;" +
                    //"Khoa,KHOA,150,0,f,l;" +
                    //"Phòng,PHONG,150,0,f,l;" +
                    //"Người chỉ định,NGUOICHIDINH,120,0,f,l;" +
                    //"ID dịch vụ đi kèm, IDDVDIKEM, 50,0,t,l;" +
                    //ew String[] {
                    //    "NGAYDICHVU", "SOPHIEU", "MADICHVU","TENNHOM", "TENDICHVU",  "SOLUONG", "DONVI", "TIEN_DICHVU"
                    //    , "THANHTIEN", "TIEN_BHYT_TRA", "TIEN_MIENGIAM", "TYLE_BHYT_TRA", "LOAITTMOI","LYDO", "KHOA", "PHONG", "NGUOICHIDINH", "IDDVDIKEM" });

                    ucGrid_DanhSach.setColumn("", "");

                    ucGrid_DanhSach.setColumn("NGAYDICHVU", "Ngày");
                    ucGrid_DanhSach.setColumn("SOPHIEU", "Số phiếu");
                    ucGrid_DanhSach.setColumn("MADICHVU", "Mã dịch vụ");
                    ucGrid_DanhSach.setColumn("TENNHOM", "Nhóm BHYT");
                    ucGrid_DanhSach.setColumn("TENDICHVU", "Tên thuốc + dịch vụ");
                    ucGrid_DanhSach.setColumn("SOLUONG", "SL");
                    ucGrid_DanhSach.setColumn("DONVI", "Đơn vị");
                    ucGrid_DanhSach.setColumn("TIEN_DICHVU", "Giá tiền");
                    ucGrid_DanhSach.setColumn("THANHTIEN", "Thành tiền");

                    ucGrid_DanhSach.setColumn("TIEN_BHYT_TRA", "BHYT trả");
                    ucGrid_DanhSach.setColumn("TIEN_MIENGIAM", "Miễn giảm");
                    ucGrid_DanhSach.setColumn("TYLE_BHYT_TRA", "Tỷ lệ TT");
                    ucGrid_DanhSach.setColumn("LOAITTMOI", "Loại TT");
                    ucGrid_DanhSach.setColumn("LYDO", "Lý do thay loại thanh toán");

                    ucGrid_DanhSach.setColumn("KHOA", "Khoa");
                    ucGrid_DanhSach.setColumn("PHONG", "Phòng");
                    ucGrid_DanhSach.setColumn("NGUOICHIDINH", "Người chỉ định");
                    ucGrid_DanhSach.setColumn("IDDVDIKEM", "ID dịch vụ đi kèm");

                ////ucGrid_DSXetNghiem.setColumn("", , "");
                //ucGrid_DanhSach.setColumnImage("TRANGTHAIMAUBENHPHAM", new String[] { "2", "3", "4" }
                //    , new String[] { "./Resources/Circle_Yellow.png", "./Resources/Circle_Green.png", "./Resources/Circle_Red.png" });

                //ucGrid_DanhSach.gridView.BestFitColumns(true);// phải đc gán sau khi set datasource

                ckbBHYT.Checked = false;
                ckbDiKem.Checked = false;
                ckbHaoPhi.Checked = false;
                ckbThuPhi.Checked = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    //var id = ids[i];
                    var _loaidoituong = dt.Rows[i]["LOAIDOITUONG"].ToString();
                    var _dathutien = dt.Rows[i]["TRANGTHAIDICHVU"].ToString();
                    var _tyle_the = dt.Rows[i]["TYLE"].ToString();
                    var _tyle_bhyt_tra = dt.Rows[i]["TYLE_BHYT_TRA"].ToString();
                    var _tyle_dv = dt.Rows[i]["TYLE_DV"].ToString();
                    var _loainhommbp = dt.Rows[i]["LOAINHOMMAUBENHPHAM"].ToString();
                    var _tien_dv = dt.Rows[i]["TIEN_DICHVU"].ToString();
                    var _soluong = dt.Rows[i]["SOLUONG"].ToString();

                    if (_loaidoituong == "1" || _loaidoituong == "2" || _loaidoituong == "3")
                    {
                        ckbBHYT.Checked = true;
                    }
                    if (_loaidoituong == "4" || _loaidoituong == "11" || _loaidoituong == "6" || _loaidoituong == "5")
                    {
                        ckbThuPhi.Checked = true; 
                    }
                    if (_loaidoituong == "7" || _loaidoituong == "8" || _loaidoituong == "9") 
                    {
                        ckbHaoPhi.Checked = true; 
                    }
                    if (_loaidoituong == "5")
                    {
                        ckbDiKem.Checked = true; 
                    } 

                    // Đánh dấy xanh đỏ các dòng
                    // if (_dathutien == 3)
                    //        {
                    //            grid.jqGrid('setRowData', ids[i], "", {
                    //                color: 'blue'

                    //                   });
                    //}

                    //if ((_loaidoituong == 1 || _loaidoituong == 2 || _loaidoituong == 3)
                    //        && _loainhommbp != 16
                    //        && _tyle_the != _tyle_bhyt_tra && _tyle_dv != 0 && _soluong != 0
                    //        && (_loainhommbp != 3 || _tien_dv != 0))
                    //{
                    //    grid.jqGrid('setRowData', ids[i], "", {
                    //        color: 'red'

                    //                   });
                    //} 
                }

    }
        } 
        public void DanhSach_SelectRow(object sender, EventArgs e)
        {
            //ckbBHYT.Checked = false;
            //ckbDiKem.Checked = false;
            //ckbHaoPhi.Checked = false;
            //ckbThuPhi.Checked = false;
            //DataRowView selected = (DataRowView)sender;
            //if (selected != null)
            //{
            //    string _loaidt = selected["LOAIDOITUONG"].ToString();

            //    if (_loaidt == "1" || _loaidt == "2" || _loaidt == "3")
            //    {
            //        ckbBHYT.Checked = true;
            //    }

            //    if (_loaidt == "4" || _loaidt == "11" || _loaidt == "6" || _loaidt == "5")
            //    {
            //        ckbThuPhi.Checked = true;
            //    }

            //    if (_loaidt == "7" || _loaidt == "8" || _loaidt == "9")
            //    {
            //        ckbHaoPhi.Checked = true;
            //    }

            //    if (_loaidt == "5")
            //    {
            //        ckbDiKem.Checked = true;
            //    }
            //}
        }
        private void Xoa()
        {
            txtTongChiPhi.Text = "";
            txtTamUng.Text = "";
            txtBHTra.Text = "";
            txtMienGiam.Text = "";
            txtBenhNhanTra.Text = "";
            txtDaThanhToan.Text = "";
            txtThanhToanThem.Text = "";

            txtPhaiNop.Text = "";

        }

        private void btnInPhoi_Click(object sender, EventArgs e)
        {  
            DataTable dt_MaThe = RequestHTTP.get_ajaxExecuteQuery("VPI01T004.12", new string[] { "[0]" }, new string[] { TIEPNHANID });
            var MA_BHYT = dt_MaThe.Rows[0]["col1"];
            DataTable result = RequestHTTP.call_ajaxCALL_SP_O("VPI01T004.01", TIEPNHANID + "$" + MA_BHYT, 0);

            var VP_IN_PHOI_KHI_DONG_BENHAN = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_PHOI_KHI_DONG_BENHAN");
            var TRANG_THAI = result.Rows[0]["TRANGTHAITIEPNHAN"].ToString();
            var LOAITIEPNHANID = result.Rows[0]["LOAITIEPNHANID"].ToString();
            var DOITUONGBENHNHANID = result.Rows[0]["DOITUONGBENHNHANID"].ToString();

            if (VP_IN_PHOI_KHI_DONG_BENHAN == "1" || TRANG_THAI == "1" || TRANG_THAI == "2" || (VP_IN_PHOI_KHI_DONG_BENHAN == "0" && LOAITIEPNHANID == "0"))
            {
                tinhTienVienPhi(TIEPNHANID, DOITUONGBENHNHANID, LOAITIEPNHANID);
            }
            else
            {
                MessageBox.Show("Chưa kết thúc khám bệnh/đóng bệnh án, không thể in bảng kê");
            }
        }

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
                            inPhoi("1", TIEPNHANID, "NTU001_BKCPKCBBHYTNOITRU_02BV_QD3455_A4");
                            if (IN_BK_VP == "0" && flag == "1")
                                inPhoi("1", TIEPNHANID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                        }
                        else
                        {
                            if (flag == "1")
                                inPhoi("1", TIEPNHANID, "NTU001_BKCPKCBTUTUCNOITRU_02BV_QD3455_A4");
                        }
                    }
                }
                else
                {
                    if (_dTBNID == "1")
                    {
                        RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                        inPhoi("1", TIEPNHANID, "NGT001_BKCPKCBBHYTNGOAITRU_01BV_QD3455_A4");
                        if (IN_BK_VP == "0" && flag == "1")
                            inPhoi("1", TIEPNHANID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                    }
                    else
                    {
                        if (flag == "1")
                            inPhoi("1", TIEPNHANID, "NGT035_BKCPKCBTUTUCNGOAITRU_A4");
                    }
                }
            }
            else
            {
                RequestHTTP.call_ajaxCALL_SP_I("VPI.SINH.STT", _tiepNhanID);
                if (_loaiTiepNhanID == "0")
                {
                    inPhoi("1", TIEPNHANID, "NTU001_BKCPKCBNOITRU_02BV_QD3455_A4");
                }
                else
                {
                    inPhoi("1", TIEPNHANID, "NGT001_BKCPKCBNGOAITRU_01BV_QD3455_A4");
                }
            }
            // in bang ke hao phi neu co
            string flag_HaoPhi = RequestHTTP.call_ajaxCALL_SP_I("VPI01T005.11", _tiepNhanID);
            string opt_HaoPhi = RequestHTTP.call_ajaxCALL_SP_I("COM.CAUHINH.THEOMA", "VP_IN_BANGKE_HAOPHI");
            if (opt_HaoPhi == "1")
            {
                if (flag_HaoPhi == "1")
                {
                    inPhoi("1", TIEPNHANID, "NGT001_BKCPKCB_HAOPHI_01BV_QD3455_A4");
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

            frmPrint frm = new frmPrint(_report_code, _report_code, table, 720, 1200);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
