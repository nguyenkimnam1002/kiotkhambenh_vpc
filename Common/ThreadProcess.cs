using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;

 
using System.Windows.Forms;


namespace VNPT.HIS.Common
{
    public class ThreadProcess
    {
        private Thread thread;
        private bool status;
        public ThreadProcess()
        {
            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            status = true;
        }
        public void start()
        {
            thread.Start();
        }
        public void stop()
        {
            status = false;
        }
        public void ThreadJob()
        {
            Thread.Sleep(1000); 

            while (status)
            { 
            }
        }

        //
        // các hàm reset cache
        //
        //private void updateKhoaPhong()
        //{
        //    try
        //    {
        //        //Const.local_khoaId = Service_SelectDept.getIdKhoa();
        //        //Const.local_phongId = Service_SelectDept.getIdPhong();

        //        if (Const.local_khoaId > 0) Const.local_khoa = Const.SQLITE.CacheObject_SelectItem(Const.tbl_DsKhoa, "id", Const.local_khoaId + "");
        //        if (Const.local_phongId > 0) Const.local_phong = Const.SQLITE.CacheObject_SelectItem(Const.tbl_DsPhong + Const.local_khoaId, "id", Const.local_phongId + "");

        //        Const.SQLITE.SelectDept_updatename(Const.local_khoaId, Const.local_khoa, Const.local_phongId, Const.local_phong);

        //        //frmMain.Current.ReloadKhoaPhong();
        //    }
        //    catch (Exception ex) { System.Console.WriteLine(ex.ToString()); }
        //}

        //chạy lại lấy dl 
        public void reset_cache()
        { 
            //DataTable dt = new DataTable(); 

            ////FORM TIẾP NHẬN BN: TAB TÌM KIẾM DS BN ĐÃ TIẾP NHẬN
            ////
            //// Danh sách phòng khám
            //dt = Service_NGT01T001_tiepnhan_ngt.getDSPhongKham();
            //Const.SQLITE.ComboItem_update(dt);

            //// Các trạng thái khám
            //dt = Service_NGT01T001_tiepnhan_ngt.getTrangThaiKham();
            //Const.SQLITE.ComboItem_update(dt);


            ////FORM TIẾP NHẬN BN: TAB TIẾP NHẬN BN
            ////
            //// dịch vụ thu khác
            //dt = Service_NGT01T001_tiepnhan_ngt.getDVTHUKHAC();
            //Const.SQLITE.ComboItem_update(dt);

            //// Chọn yêu cầu khám
            ////dt = Service_NGT01T001_tiepnhan_ngt.getYeuCauKham();
            ////Const.SQLITE.ComboItem_update(dt);
             

        }
         
    }
     
}

