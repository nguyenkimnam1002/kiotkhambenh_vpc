using System; 
using System.Data; 
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.Duoc.subform
{
    public partial class DUC35T001_ThongTinPhieu : DevExpress.XtraEditors.XtraForm
    {
        string hid_NHAPXUATID;
        string nxid;
        string hid_kieu;

        string hid_NHACUNGCAPID, hid_NHAPID, hid_XUATID, loaiphieu;
        public DUC35T001_ThongTinPhieu(string nhapxuatid, string nxid, string kieu)
        {
            InitializeComponent();

            this.hid_NHAPXUATID = nhapxuatid;
            this.nxid = nxid;
            this.hid_kieu = kieu;
        } 
        private void DUC35T001_ThongTinPhieu_Load(object sender, EventArgs e)
        {
            if (hid_kieu == "THEKHO") cmdTaoPhieu.Visible = false;

            if (hid_NHAPXUATID != "0") loadEditorPhieu(hid_NHAPXUATID, 1);

            bindEvent();
        }

        private void cmdDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdTaoPhieu_Click(object sender, EventArgs e)
        {
            //result error_msg;
            ResponsObj _return = RequestHTTP.call_ajaxCALL_SP_S("DUC47T001.CREATE", this.nxid);
            if (_return != null)
            { 
                if (Func.Parse(_return.result) > 0)
                {
                    MessageBox.Show("Tạo phiếu thành công!");
                }
                else
                {
                    MessageBox.Show(_return.error_msg);
                }
            }
        }

        private void bindEvent()
        {

        }
        private void loadEditorPhieu(string _keyField, int isAddNew)
        {
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O("DUC04D001.04", _keyField);
            if (data_ar.Rows.Count > 0)
            {
                txtKHO_LAP.Text = data_ar.Rows[0]["NOI_LAP"].ToString();
                //txtNGUOI_LAP.Text = data_ar.Rows[0]["NGUOI_LAP"].ToString();
                txtNGAY_LAP.Text = data_ar.Rows[0]["NGAY_LAP"].ToString();
                //txtLAN_IN.Text = '0'"].ToString();
                txtSO_CHUNG_TU.Text = data_ar.Rows[0]["SO_CT"].ToString();
                txtNGAY_LAP_SCT.Text = data_ar.Rows[0]["NGAY_LAP_SCT"].ToString();

                txtMA_PHIEU.Text = data_ar.Rows[0]["MA_PHIEU"].ToString();

                txtNHA_CUNG_CAP.Text = data_ar.Rows[0]["NHA_CUNG_CAP"].ToString();
                txtNGUOI_GIAO.Text = data_ar.Rows[0]["NGUOIGIAO"].ToString();
                txtDIA_CHI.Text = data_ar.Rows[0]["DIACHI"].ToString();
                txtCHIET_KHAU.Text = data_ar.Rows[0]["CHIET_KHAU"].ToString();
                txtGHI_CHU.Text = data_ar.Rows[0]["GHI_CHU"].ToString();


                hid_kieu = data_ar.Rows[0]["KIEU"].ToString();

                hid_NHACUNGCAPID = data_ar.Rows[0]["NHACUNGCAPID"].ToString();
                //hid_NHAPID = data_ar.Rows[0]["NHAPID"].ToString(); ko có NHAPID
                //hid_XUATID = data_ar.Rows[0]["XUATID"].ToString(); ko có XUATID
                loaiphieu = data_ar.Rows[0]["LOAI_PHIEU"].ToString();

                float sumAll = Func.Parse_float(data_ar.Rows[0]["TIENDON"].ToString());
                float tien_chiet_khau = (sumAll * Func.Parse_float(txtCHIET_KHAU.Text.Trim())) / 100;
                float tien_thua_vat = Func.Parse_float(data_ar.Rows[0]["TIENTHUEVAT"].ToString());
                // h_thanhtoantong").val(sumAll);
                tiendon.Text = Func.formatMoneyEng_GiuThapPhan(sumAll.ToString()) + " đ";   //.format(2, 3, ',') + 'đ');	   
                thuegtgt.Text = Func.formatMoneyEng_GiuThapPhan(data_ar.Rows[0]["TIENTHUEVAT"].ToString()) + " đ";   // (2, 3, ',') + 'đ');

                tienchietkhau.Text = Func.formatMoneyEng_GiuThapPhan(tien_chiet_khau.ToString()) + " đ";
                layout_tongcong.Text = Func.formatMoneyEng_GiuThapPhan((sumAll - tien_chiet_khau).ToString()); //.format(2, 3, ',') + 'đ');
                conlai.Text = Func.formatMoneyEng_GiuThapPhan((sumAll - Func.Parse_float(txtTHANH_TOAN_TONG.Text) - tien_chiet_khau + tien_thua_vat).ToString());
                //- Func.Parse_float(tien_chiet_khau) + Func.Parse_float(data_ar.Rows[0]["TIENTHUEVAT"].ToString())).format(2, 3, ',') + 'đ');

                var tong = sumAll - tien_chiet_khau + tien_thua_vat;
                tongcong.Text = Func.formatMoneyEng_GiuThapPhan(tong.ToString()) + " đ";

                //hid_NHAPXUATID_CHA    ").val(data_ar.Rows[0]["NHAPXUATID_CHA);

                henthanhtoan.Text = data_ar.Rows[0]["HEN_THANH_TOAN"].ToString();
            }
            else
            {
                if (isAddNew != 0) MessageBox.Show("Không có dữ liệu");
                return;
            }

            _loadgrid(1, null);
        }

        private void _loadgrid(object sender, EventArgs e)
        {
            //postData: { "func":"ajaxExecuteQueryPaging","uuid":"e6e48192-55fc-4838-a508-f1e37e59038c","params":["DUC02N001.02"],"options":[{"name":"[0]","value":"109472"}]}
            //_search: false
            //nd: 1542870266874
            //rows: 1000
            //page: 1
            //sidx: 
            //sord: asc
            int page = (int)sender;
            if (page > 0)
            {
                ResponsList responses = new ResponsList();
                responses = RequestHTTP.get_ajaxExecuteQueryPaging("DUC02N001.02", page, gridList_THUOC.getNumberPerPage(),
                       new String[] { "[0]" },
                       new string[] { hid_NHAPXUATID },
                       gridList_THUOC.jsonFilter());

                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "RN", "MA_THUOC", "TEN_THUOC","BIET_DUOC", "DON_VI_TINH"
                    , "SL_YC", "DON_GIA", "VAT"
                    , "THANH_TIEN", "SO_LO", "NGUOC_CT","LIEULUONG", "SL_DUYET"
                });

                gridList_THUOC.setData(dt, responses.total, responses.page, responses.records);
                gridList_THUOC.setColumnAll(false);
                gridList_THUOC.onIndicator();

                if (hid_kieu == "0" || hid_kieu == "1")
                {
                    gridList_THUOC.setColumn("MA_THUOC", "Mã");
                    gridList_THUOC.setColumn("TEN_THUOC", "Tên");
                    gridList_THUOC.setColumn("BIET_DUOC", "Biệt dược");
                    gridList_THUOC.setColumn("DON_VI_TINH", "Đơn vị");

                    gridList_THUOC.setColumn("SL_YC", "Số lượng");
                    gridList_THUOC.setColumn("DON_GIA", "Đơn giá");
                    gridList_THUOC.setColumn("VAT", "VAT%");
                    gridList_THUOC.setColumn("THANH_TIEN", "Thành tiền");

                    gridList_THUOC.setColumn("SO_LO", "Số lô");
                    gridList_THUOC.setColumn("NGUOC_CT", "Nguồn CT");
                    gridList_THUOC.setColumn("LIEULUONG", "Hàm lượng");
                }
                else
                {
                    gridList_THUOC.setColumn("MA_THUOC", "Mã");
                    gridList_THUOC.setColumn("TEN_THUOC", "Tên");
                    gridList_THUOC.setColumn("BIET_DUOC", "Biệt dược");
                    gridList_THUOC.setColumn("DON_VI_TINH", "Đơn vị");

                    gridList_THUOC.setColumn("SL_YC", "Số lượng");
                    gridList_THUOC.setColumn("SL_DUYET", "SL Duyệt");
                    gridList_THUOC.setColumn("DON_GIA", "Đơn giá");
                    gridList_THUOC.setColumn("VAT", "VAT%");

                    gridList_THUOC.setColumn("THANH_TIEN", "Thành tiền");
                    gridList_THUOC.setColumn("SO_LO", "Số lô");
                    gridList_THUOC.setColumn("NGUOC_CT", "Nguồn CT");
                    gridList_THUOC.setColumn("LIEULUONG", "Hàm lượng");
                }
            }

        }
    }
}