using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using VNPT.HIS.Common;

namespace VNPT.HIS.Duoc.subform
{
    public partial class DUC51S001_DSDonThuoc : DevExpress.XtraEditors.XtraForm
    {
        string nhapxuatid, gd;
        string hidYLenh;
        public DUC51S001_DSDonThuoc(string nhapxuatid, string gd)
        {
            InitializeComponent();

            this.nhapxuatid = nhapxuatid;
            this.gd = gd;

            hidYLenh = this.nhapxuatid;
        }
        private void DUC51S001_DSDonThuoc_Load(object sender, EventArgs e)
        {
            //Đơn thuốc  
            grvDonThuoc.SetReLoadWhenFilter(true);
            //2 hàm dưới để load grid ra, mặc định chọn view chi tiết vào row đầu tiên.
            grvDonThuoc.gridView.OptionsBehavior.Editable = false;
            //grvDonThuoc.gridView.Click += clickrow;
            grvDonThuoc.setEvent_FocusedRowChanged(grvDonThuoc_Rowchange);

            //[{"NGAYKETHUOC": "11/10/2018","MABENHNHAN": "BN00043001","HOTEN": "TEST _ 1510","MAPHIEU": "P000002226","KHOA": "Khoa Khám bệnh"
            //,"PHONG": "Phòng 6: Tai Mũi Họng (K106)","NHAPXUATID": "109906","MAUBENHPHAMID": "314092","BENHNHANID": "46787","KHOAID": "4902","PHONGID": "4952"}]
            DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("DUC51S001.01", hidYLenh);
            if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "MAPHIEU", "HOTEN","NGAYKETHUOC", "KHOA", "PHONG"
                });

            grvDonThuoc.setData(dt, dt.Rows.Count, 1);
            grvDonThuoc.setColumnAll(false);

            grvDonThuoc.onIndicator();
            grvDonThuoc.setColumn("MAPHIEU", "Mã phiếu");
            grvDonThuoc.setColumn("HOTEN", "Tên BN");
            grvDonThuoc.setColumn("NGAYKETHUOC", "Ngày kê");
            grvDonThuoc.setColumn("KHOA", "Khoa");
            grvDonThuoc.setColumn("PHONG", "Phòng");

            //Đơn thuốc chi tiết
            grvDonThuocCT.setEvent(load_DonThuoc_ChiTiet);
            grvDonThuocCT.SetReLoadWhenFilter(true);


            // Thuốc 
            grvThuoc.SetReLoadWhenFilter(true);
            //2 hàm dưới để load grid ra, mặc định chọn view chi tiết vào row đầu tiên.
            grvThuoc.gridView.OptionsBehavior.Editable = false; 
            grvThuoc.setEvent_FocusedRowChanged(grvThuoc_Rowchange);

            //[{"THUOCVATTUID": "281414","MADICHVU": "ACE00","TENDICHVU": "Acetazolamid","SUM(A.SOLUONGPHUTROI)": "0","SOLUONG": "2","DVT": "Viên","TIEN_BHYT_TRA": "2217.6","TIEN_CHITRA": "1108866"}]
            dt = RequestHTTP.call_ajaxCALL_SP_O("DUC51S001.02", hidYLenh);
            if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "MADICHVU", "TENDICHVU","SOLUONG", "DVT"
                });

            grvThuoc.setData(dt, dt.Rows.Count, 1);
            grvThuoc.setColumnAll(false);

            grvThuoc.onIndicator();
            grvThuoc.setColumn("MADICHVU", "Mã dịch vụ");
            grvThuoc.setColumn("TENDICHVU", "Thuốc/VT");
            grvThuoc.setColumn("SOLUONG", "Số lượng");
            grvThuoc.setColumn("DVT", "Đơn vị");


            //
            grvDonThuoc_Rowchange(null, null);
            grvThuoc_Rowchange(null, null);
        }
        string MAUBENHPHAMID = "";
        private void grvDonThuoc_Rowchange(object sender, EventArgs e)
        {
            int index = grvDonThuoc.gridView.FocusedRowHandle;
            DataRowView row = (DataRowView)grvDonThuoc.gridView.GetRow(index);
            if (row == null) return;

            MAUBENHPHAMID = row["MAUBENHPHAMID"].ToString();

            load_DonThuoc_ChiTiet(1, null);
        }
        private void load_DonThuoc_ChiTiet(object sender, EventArgs e)
        {
            int page = (int)sender;
            if (page > 0)
            {
                ResponsList responses = new ResponsList();
                responses = RequestHTTP.get_ajaxExecuteQueryPaging("NT.034.1", page, grvDonThuocCT.getNumberPerPage(),
                       new String[] { "[0]" },
                       new string[] { MAUBENHPHAMID },
                       grvDonThuocCT.jsonFilter());

                //{ "total": 1,"page": 1,"records": 1,"rows": [{"RN": "1","MAUBENHPHAMID": "314092","DICHVUKHAMBENHID": "662538","MADICHVU": "ACE00","TENDICHVU": "Acetazolamid"
                //,"SOLAN_SD_KHANGSINH": null,"SL_KELE": "","PHUTROI": "X","SOLUONGPHUTROI": "0","TIENCHITRA": "554433","SOLUONG": "2","DVT": "Viên","NGAYDUNG": "1"
                //,"LIEUDUNG": "1 Viên/Lần * 2lần/Ngày","NHAPXUATCTID": "636888","NDHL": "250mg","DUONGDUNG": "Uống","HUONGDANSUDUNG": "1 ngày, Sáng 1 Viên, Trưa 1 Viên"
                //,"SOLUONGTRA": "0","LOAIDOITUONG": "BHYT"}],"uuid": ""}
                DataTable dt = new DataTable();
                dt = MyJsonConvert.toDataTable(responses.rows);

                if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "MADICHVU", "TENDICHVU", "SOLUONG","DVT", "NGAYDUNG", "DUONGDUNG" , "HUONGDANSUDUNG"
                });

                grvDonThuocCT.setData(dt, responses.total, responses.page, responses.records);
                grvDonThuocCT.setColumnAll(false);

                grvDonThuocCT.onIndicator();
                grvDonThuocCT.setColumn("MADICHVU", "Mã dịch vụ");
                grvDonThuocCT.setColumn("TENDICHVU", "Thuốc/VT");
                grvDonThuocCT.setColumn("SOLUONG", "Số lượng");
                grvDonThuocCT.setColumn("DVT", "Đơn vị");
                grvDonThuocCT.setColumn("NGAYDUNG", "Ngày dùng");
                grvDonThuocCT.setColumn("DUONGDUNG", "Đường dùng");
                grvDonThuocCT.setColumn("HUONGDANSUDUNG", "Hướng dẫn sử dụng");

            }
        }


        private void grvThuoc_Rowchange(object sender, EventArgs e)
        {
            int index = grvThuoc.gridView.FocusedRowHandle;
            DataRowView row = (DataRowView)grvThuoc.gridView.GetRow(index);
            if (row == null) return;

            //[{"NGAYKETHUOC": "11/10/2018","MABENHNHAN": "BN00043001","HOTEN": "TEST _ 1510","MAPHIEU": "P000002226","NHAPXUATID": "109906","MAUBENHPHAMID": "314092","BENHNHANID": "46787"}]
            DataTable dt = RequestHTTP.call_ajaxCALL_SP_O("DUC51S001.03", hidYLenh + "$" + row["THUOCVATTUID"].ToString());

            if (dt.Rows.Count == 0) dt = Func.getTableEmpty(new String[] { "MAPHIEU", "HOTEN", "NGAYKETHUOC"
                });

            grvThuocCT.setData(dt, dt.Rows.Count, 1);
            grvThuocCT.setColumnAll(false);

            grvThuocCT.onIndicator();
            grvThuocCT.setColumn("MAPHIEU", "Mã phiếu");
            grvThuocCT.setColumn("HOTEN", "Tên BN");
            grvThuocCT.setColumn("NGAYKETHUOC", "Ngày kê");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}