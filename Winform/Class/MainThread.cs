using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using System.Data;
using System.Windows.Forms;

using VNPT.HIS.Common;
using VNPT.HIS.MainForm.Class;
using VNPT.HIS.NgoaiTru.Class;

namespace VNPT.HIS.MainForm.Class
{
    class MainThread
    {
        
        private Thread thread;
        private bool status;
        public MainThread()
        {
            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            
        }
        public void start()
        {
            if (!thread.IsAlive)
            {
                status = true;
                thread.Start();
            }
        }
        public void stop()
        {
            if (thread.IsAlive)
            {
                status = false;
                thread.Abort();
            }
        }
        public void ThreadJob()
        { 
            //frmMain.Current.frmTiepNhan.Init_Form();

            //updateKhoaPhong();
            

            while (status)
            {
                setTime();

                if (Const.login_timeout == 2)
                {
                    Const.login_timeout = 0;// khóa check timeout, sẽ mở khi đn thành công trở lại
                    login_timeout();
                }

                Thread.Sleep(1000);
            }
        }
        public static void login_timeout()
        {
            if (frmMain.Current.InvokeRequired)
            {
                frmMain.Current.BeginInvoke(new MethodInvoker(delegate ()
                {                    
                    frmMain.Current.appLogout();
                }));
            }
            else
            {
                lock (frmMain.Current)
                {
                    frmMain.Current.appLogout();
                }
            }
        }
        
        public static void setTime()
        {
            frmMain.Current.lbTime.Caption = DateTime.Now.AddSeconds(Const.diffInSeconds).ToString("HH:mm:ss");

            //if (frmMain.Current.lbTime.InvokeRequired)
            //{
            //    frmMain.Current.lbTime.BeginInvoke(new MethodInvoker(delegate()
            //    {
            //        frmMain.Current.lbTime.Text = DateTime.Now.ToString("HH:mm:ss");
            //    }));
            //}
            //else
            //{
            //    lock (frmMain.Current.lbTime)
            //    {
            //        frmMain.Current.lbTime.Text = DateTime.Now.ToString("HH:mm:ss");
            //    }
            //}
        }
        private void updateKhoaPhong()
        {
            try
            {
                int khoaId_new = ServiceSelectDept.getIdKhoa();
                int phongId_new = ServiceSelectDept.getIdPhong();
                
                if (Const.local_khoaId != khoaId_new || Const.local_phongId != phongId_new)
                {
                    Const.local_khoaId = khoaId_new;
                    Const.local_phongId = phongId_new;

                    Const.local_khoa = "";
                    Const.local_phong = "";

                    frmMain.Current.Open_KhoaPhong();
                } 
            }
            catch (Exception ex) { }
        }
        

    }
}
