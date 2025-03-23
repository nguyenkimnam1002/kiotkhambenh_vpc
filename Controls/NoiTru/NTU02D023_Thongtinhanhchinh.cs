using System; 
using System.Data; 
using VNPT.HIS.Common;

namespace VNPT.HIS.Controls.NoiTru
{
    public partial class NTU02D023_Thongtinhanhchinh : DevExpress.XtraEditors.XtraUserControl
    {
        
        public NTU02D023_Thongtinhanhchinh()
        {
            InitializeComponent();
        } 
        //public string khamBenhID = "";
        //public string hosobenhanid = "";
        //public string phongid = "";
        public void load_data(string khambenhid, string hosobenhanid, string phongid, string title)
        {
            //this.khamBenhID = khamBenhID; 
            label_BenhAn.Text = title.ToUpper();

            string sql_name = "";
            string sql_par = "";

            if (khambenhid != "")
            {
                sql_name = "NT.005";
                sql_par = khambenhid;
                if (phongid != "")
                {
                    sql_name = "NT.005.NGT";
                    sql_par = khambenhid + "$" + phongid;
                }
            }

            if (hosobenhanid != "")
            {
                sql_name = "NT.005.HSBA";
                sql_par = hosobenhanid;
                if (phongid != "")
                {
                    sql_name = "NT.005.NGT.HSBA";
                    sql_par =hosobenhanid + "$" + phongid;
                }
            }
             
            DataTable data_ar = RequestHTTP.call_ajaxCALL_SP_O(sql_name, sql_par, 0); 
              
            if (data_ar != null || data_ar.Rows.Count > 0)
            {   
                label_Khoa.Text = data_ar.Rows[0]["KHOA"].ToString();
                label_Phong.Text = data_ar.Rows[0]["PHONG"].ToString();
                label_MaBenhAn.Text = data_ar.Rows[0]["MAHOSOBENHAN"].ToString();
                label_SoVaoVien.Text = data_ar.Rows[0]["SOVAOVIEN"].ToString();
                label_MaDieuTri.Text = data_ar.Rows[0]["MAKHAMBENH"].ToString();
                label_MaThe.Text = data_ar.Rows[0]["MACHANDOANVAOVIEN"].ToString();
                label_HoTen.Text = data_ar.Rows[0]["TENBENHNHAN"].ToString();
                label_NgaySinh.Text = data_ar.Rows[0]["NGAYSINH"].ToString();
                label_GioiTinh.Text = data_ar.Rows[0]["GIOITINH"].ToString();
                label_NgheNghiep.Text = data_ar.Rows[0]["TENNGHENGHIEP"].ToString();
                label_DoiTuong.Text = data_ar.Rows[0]["TEN_DTBN"].ToString();
                label_MaTheBHYT.Text = data_ar.Rows[0]["MABHYT"].ToString();
                label_Tuyen.Text = data_ar.Rows[0]["TUYEN"].ToString();
                label_ThoiGian.Text = data_ar.Rows[0]["THOIGIAN"].ToString();
                label_NoiLamViec.Text = data_ar.Rows[0]["NOILAMVIEC"].ToString();
                label_BaoTinCho.Text = data_ar.Rows[0]["BAOTINCHO"].ToString();
                label_VaoKhoa.Text = data_ar.Rows[0]["THOIGIANVAOVIEN"].ToString();
                label_NoiGioiThieu.Text = data_ar.Rows[0]["NOIGIOITHIEU"].ToString();
                label_CDNoiGT.Text = data_ar.Rows[0]["TKNOICD"].ToString();
                label_Nhan.Text = data_ar.Rows[0]["NHANTU"].ToString();
                label_RaKhoa.Text = data_ar.Rows[0]["THOIGIANRAVIEN"].ToString();
                label_BenhChinh.Text = data_ar.Rows[0]["BENHCHINH"].ToString();
                label_BenhPhu.Text = data_ar.Rows[0]["BENHPHU"].ToString();
                label_KetQua.Text = data_ar.Rows[0]["KETQUA"].ToString();
                label_XuTri.Text = data_ar.Rows[0]["XUTRI"].ToString();
            }
        }

        private void NTU02D023_Thongtinhanhchinh_Load(object sender, EventArgs e) 
        {
            
        }

    }
}
