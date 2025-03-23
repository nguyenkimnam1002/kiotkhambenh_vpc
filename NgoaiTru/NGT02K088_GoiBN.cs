using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using VNPT.HIS.Common;
using System.ComponentModel;

namespace VNPT.HIS.NgoaiTru
{
    public partial class NGT02K088_GoiBN : DevExpress.XtraEditors.XtraForm
    {
        private static readonly log4net.ILog log =
                   log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }

        public NGT02K088_GoiBN()
        {
            InitializeComponent();
        }

        private static int second = 30;
        private static int rows = 0;
        private static string urlPath = Application.StartupPath + @"\Resources\";
        private static string moiBenhNhanSoFile = "MoiBenhNhanSo";
        private static string denQuaySoFile = "DenQuaySo";
        private static string muteFile = "im_lang";
        private static string extension = ".m4a";
        private Thread thread;

        public void ThreadJob()
        {
            int count = 0;
            while (thread.IsAlive)
            {
                Thread.Sleep(1000);

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate ()
                    {
                        count = (count + 1) % second;
                        if (count == 0) RefreshData();
                    }));
                }
                else
                {
                    lock (this)
                    {
                        count = (count + 1) % second;
                        if (count == 0) RefreshData();
                    }
                }
            }
        }

        private void RefreshData()
        {
            try
            {
                //Get datafrom sv
                DataTable dt = new DataTable();
                dt = RequestHTTP.call_ajaxCALL_SP_O("NGT.GOIBN", string.Empty, 0);

                rows = dt.Rows.Count;

                string stt = string.Empty;
                string soPhong = string.Empty;

                if (rows > 0)
                {
                    object[] array = new object[2];
                    array[0] = dt.Rows[0]["STT"].ToString();
                    array[1] = dt.Rows[0]["SOPHONG"].ToString();
                    BackgroundWorker backgroundWorker = new BackgroundWorker();
                    backgroundWorker.DoWork += new DoWorkEventHandler(br_DoWork);
                    backgroundWorker.RunWorkerAsync(array);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message);
            }
        }

        private static void br_DoWork(object sender, DoWorkEventArgs e)
        {
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            WMPLib.IWMPPlaylist playlist = wplayer.playlistCollection.newPlaylist("playlist");
            playlist.appendItem((wplayer.newMedia(urlPath + moiBenhNhanSoFile + extension)));

            object[] array = (object[])e.Argument;
            var sttArr = LeftPad(array[0].ToString(), 4);
            for (var index = 0; index < sttArr.Length; index++)
            {
                playlist.appendItem(wplayer.newMedia(urlPath + sttArr[index] + extension));
            }

            playlist.appendItem(wplayer.newMedia(urlPath + denQuaySoFile + extension));

            var soPhongArr = LeftPad(array[1].ToString(), 2);
            for (var index = 0; index < soPhongArr.Length; index++)
            {
                playlist.appendItem(wplayer.newMedia(urlPath + soPhongArr[index] + extension));
            }

            playlist.appendItem(wplayer.newMedia(urlPath + muteFile + extension));
            wplayer.currentPlaylist = playlist;
            Thread.Sleep(15000);
        }

        private static string LeftPad(string number, int targetLength)
        {
            var output = number;
            if (output.Length == 4) return output;
            while (output.Length < targetLength)
            {
                output = '0' + output;
            }
            return output;
        }

        private void NGT02K088_GoiBN_Load(object sender, EventArgs e)
        {
            StatusButton(true, false);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            thread.Abort();
            StatusButton(true, false);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StatusButton(false, true);
            RefreshData();
            ThreadStart job = new ThreadStart(ThreadJob);
            thread = new Thread(job);
            thread.Start();
        }

        private void StatusButton(bool isStart, bool isStop)
        {
            btnStart.Enabled = isStart;
            btnStop.Enabled = isStop;
        }

        private void bbtnDong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }

            this.Close();
        }
    }
}