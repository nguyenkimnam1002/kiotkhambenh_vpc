using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using VNPT.HIS.Common;
namespace VNPT.HIS.MainForm.HeThong
{
    public partial class ManageCache : DevExpress.XtraEditors.XtraForm
    {
        public ManageCache()
        {
            InitializeComponent();
        }

        private void ucGridview1_Load(object sender, EventArgs e)
        {

        }

        private void ManageCache_Load(object sender, EventArgs e)
        {

            // 1: tạo bảng
            if (!Const.SQLITE.existTable("tbl_cache"))
                Const.SQLITE.execute("CREATE TABLE tbl_cache (name TEXT, value TEXT, lastupdate TEXT);");

            DataTable dt = Const.SQLITE.manaAllTable();
            ucBang.setEvent_FocusedRowChanged(ucBang_SelectChange);
            ucBang.setMultiSelectMode(true);
            ucBang.setData(dt, 0, 0);

            ucBang.Set_HidePage(true);
            ucValue.Set_HidePage(true);
        }
        private void ucBang_SelectChange(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)sender;
            if (drv != null)
            {
                string tablename = drv[0].ToString();
                DataTable dt = new DataTable();
                Const.SQLITE.CacheObject_Select(tablename, out dt);
                //if (dt.Rows.Count > 0)
                {
                    //ucValue.setEvent_FocusedRowChanged(ucBang_SelectChange);
                    ucValue.clearData_frmTiepNhan();
                    ucValue.setData(dt, 1, 1, dt.Rows.Count);
                    ucValue.setColumnAll(true);
                }
            }
        }
        
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine(DateTime.Now.ToString("mm:ss:fff") + " Tạo cache");
            // Taojo cache mới

            Const.SQLITE.dropTable(Const.SQLITE.TBL_CACHE);
            // 1: tạo bảng
            if (!Const.SQLITE.existTable(Const.SQLITE.TBL_CACHE))
                Const.SQLITE.execute("CREATE TABLE "+ Const.SQLITE.TBL_CACHE + " (name TEXT, value TEXT, lastupdate TEXT);");

            #region Tên viết tắt Tỉnh huyện xã
            Const.SQLITE.test_cache_add(Const.tbl_TinhhuyenxaViettat, RequestHTTP.WS_ajaxExecuteQueryPaging(Const.tbl_TinhhuyenxaViettat));
            #endregion

            #region Danh sách các bệnh viện
            Const.SQLITE.test_cache_add(Const.tbl_NoiDKKCB, RequestHTTP.WS_ajaxExecuteQueryPaging(Const.tbl_NoiDKKCB));
            #endregion

            #region Danh sách các Bệnh 
            Const.SQLITE.test_cache_add(Const.tbl_DsBenh, RequestHTTP.WS_ajaxExecuteQueryPaging(Const.tbl_DsBenh));
            #endregion

            #region Dân tộc-Giới tính-Nghề nghiệp-Quốc tịch-Khu vực
            Const.SQLITE.test_cache_add(Const.tbl_Dantoc, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Dantoc));
            Const.SQLITE.test_cache_add(Const.tbl_Gioitinh, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Gioitinh, "1"));
            Const.SQLITE.test_cache_add(Const.tbl_Nghenghiep, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Nghenghiep));
            Const.SQLITE.test_cache_add(Const.tbl_Quoctich, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Quoctich));
            Const.SQLITE.test_cache_add(Const.tbl_Noisong, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Noisong, "76"));
            #endregion

            #region Các ds thuốc trong form Chỉ định thuốc 
            //khothuocID, company_id, _loainhommaubenhpham_id, loaithuocid	 
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] { "770", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"742", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"715", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"912", Const.local_user.COMPANY_ID.ToString(), "7", "-1" });

            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"770", Const.local_user.COMPANY_ID.ToString(), "7", "10" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"742", Const.local_user.COMPANY_ID.ToString(), "7", "10" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"715", Const.local_user.COMPANY_ID.ToString(), "7", "10" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13",new string[] { "912", Const.local_user.COMPANY_ID.ToString(), "7", "10" });

            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"770", Const.local_user.COMPANY_ID.ToString(), "7", "11" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"742", Const.local_user.COMPANY_ID.ToString(), "7", "11" });
            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.13", new string[] {"715", Const.local_user.COMPANY_ID.ToString(), "7", "11" });

            RequestHTTP.Cache_ajaxExecuteQueryPaging(true, "NTU02D010.02", new string[] {Const.local_user.COMPANY_ID.ToString() });
            #endregion

            #region getTuyenKhamBenh  getDTDACBIET
            Const.SQLITE.test_cache_add(Const.tbl_DTMienGiam, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DTMienGiam));
            Const.SQLITE.test_cache_add(Const.tbl_Phongkham, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Phongkham));
            Const.SQLITE.test_cache_add(Const.tbl_TrangThaiKham, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_TrangThaiKham));
            Const.SQLITE.test_cache_add(Const.tbl_XuTriKB, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_XuTriKB));
            Const.SQLITE.test_cache_add(Const.tbl_DichVuKhac, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DichVuKhac));
            Const.SQLITE.test_cache_add(Const.tbl_KhoaDTNgT, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_KhoaDTNgT));
            Const.SQLITE.test_cache_add(Const.tbl_KhoaDTNT, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_KhoaDTNT));
            Const.SQLITE.test_cache_add(Const.tbl_DSHopDong, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DSHopDong));
            Const.SQLITE.test_cache_add(Const.tbl_BacSyKham, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_BacSyKham));
            Const.SQLITE.test_cache_add(Const.tbl_DuongDung, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DuongDung));
            // Ko khởi tạo cache vì còn phụ thuộc thêm biến id
            //Const.SQLITE.test_cache_add(Const.tbl_Tuyen, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_Tuyen, "58"));
            //Const.SQLITE.test_cache_add(Const.tbl_YeuCauKham, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_YeuCauKham, ));
            //Const.SQLITE.test_cache_add(Const.tbl_TrangThai_RV002, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_TrangThai_RV002, ));
            //Const.SQLITE.test_cache_add(Const.tbl_TrangThai_RV003, RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_TrangThai_RV003, ));


            #endregion

            #region
            #endregion

            #region
            #endregion

            #region DS các tỉnh, huyện, xã 
            //tỉnh
            string ds_tinh = RequestHTTP.WS_getTinhTP();
            DataTable dt_tinh = Func.fill_ArrayStr_To_Datatable(ds_tinh, "");
            Const.SQLITE.test_cache_add(Const.tbl_DsTinh, ds_tinh);

            for (int i = 0; i < dt_tinh.Rows.Count; i++)
            {
                //huyện
                string id_tinh = dt_tinh.Rows[i]["col1"].ToString();
                string ds_huyen = RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DsHuyenXa, id_tinh);
                DataTable dt_huyen = Func.fill_ArrayStr_To_Datatable(ds_huyen, "");
                Const.SQLITE.test_cache_add(Const.tbl_DsHuyenXa + "_" + id_tinh, ds_huyen);

                for (int j = 0; j < dt_huyen.Rows.Count; j++)
                {
                    //xã
                    string id_huyen = dt_huyen.Rows[j]["col1"].ToString();
                    string ds_xa = RequestHTTP.WS_ajaxExecuteQuery(Const.tbl_DsHuyenXa, id_huyen);
                    Const.SQLITE.test_cache_add(Const.tbl_DsHuyenXa + "_" + id_huyen, ds_xa);
                }
            }
            #endregion

            #region ds khoa + phòng
            string ds_khoa = RequestHTTP.WS_getKhoa();
            DataTable dt_khoa = Func.fill_ArrayStr_To_Datatable(ds_khoa, "");
            Const.SQLITE.test_cache_add(Const.tbl_DsKhoa + "_" + Const.local_user.USER_ID + "_" + Const.local_user.HOSPITAL_ID, ds_khoa);

            for (int i = 0; i < dt_khoa.Rows.Count; i++)
            {
                string id_khoa = dt_khoa.Rows[i][0].ToString();
                string ds_phong = RequestHTTP.WS_getPhong(id_khoa);
                Const.SQLITE.test_cache_add(Const.tbl_DsKhoa + "_" + Const.local_user.USER_ID + "_" + id_khoa, ds_phong);
            }
            #endregion
        }

        private void XOA_Click(object sender, EventArgs e)
        {
            int[] idxSelectRows = ucBang.gridView.GetSelectedRows();
            DataRowView drView;
            for (int i = 0; i < idxSelectRows.Length; i++)
            {
                drView = (DataRowView)ucBang.gridView.GetRow(idxSelectRows[i]);
                Const.SQLITE.dropTable(drView[0].ToString());
            }
        }
        private void XoaDL_Click(object sender, EventArgs e)
        {
            int[] idxSelectRows = ucBang.gridView.GetSelectedRows();
            DataRowView drView;
            for (int i = 0; i < idxSelectRows.Length; i++)
            {
                drView = (DataRowView)ucBang.gridView.GetRow(idxSelectRows[i]);

                Const.SQLITE.truncateTable(drView[0].ToString());
            }
        }

        private void F5_Click(object sender, EventArgs e)
        {
            DataTable dt = Const.SQLITE.manaAllTable();
            ucBang.setData(dt, 0, 0);
        }

    }
}