using System;
using System.Drawing;
using VNPT.HIS.CommonForm.Capture;
using AForge.Video.DirectShow;
using AForge.Video;
using VNPT.HIS.Common;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace VNPT.HIS.CommonForm
{
    public partial class NGT02K026_ChupAnhBenhNhan : DevExpress.XtraEditors.XtraForm
    {
        #region Variable

        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private FilterInfoCollection videoDevices = null;
        private VideoCaptureDevice videoSource = null;
        private string hoSoBenhNhanID = string.Empty;

        
        #endregion

        #region Delegate

        delegate void CallBackShowImg(Image img);
        protected EventHandler Event_Return;

        #endregion

        #region Private

        /// <summary>
        /// Khởi động capture khi load màn hình chụp ảnh
        /// </summary>
        private void InitForm ()
        {
            this.StartCapture();
        }

        /// <summary>
        /// Khởi động capture
        /// </summary>
        private void StartCapture()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                videoSource = new VideoCaptureDevice(videoDevices[Global.curDevice].MonikerString);
                this.StopCapture();
                videoSource.Start();
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                videoSource.Start();

                btnChupAnh.Focus();
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Tắt capture
        /// </summary>
        private bool StopCapture ()
        {
            try
            {
                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Lấy hình ảnh trên capture
        /// </summary>
        private void ChupAnh ()
        {
            try
            {
                Image imgSelected = this.CaptureImg();
                if (imgSelected != null)
                {
                    picHinhAnh.Image = imgSelected;
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Lưu hình ảnh đã bắt xuống DB
        /// </summary>
        private void LuuAnh ()
        {
            try
            {
                if (string.IsNullOrEmpty(hoSoBenhNhanID))
                {
                    MessageBox.Show(Const.mess_chupanh_chuanhaphosobenhnhan);
                    return;
                }

                Image img = picHinhAnh.Image;
                if (img == null)
                {
                    Bitmap bmp = new Bitmap(100, 100);
                    Graphics g = Graphics.FromImage(bmp);
                    g.Clear(Color.White);
                    img = bmp;
                }

                string base64String = ConvertImageToBase64(img);
                if (string.IsNullOrEmpty(base64String))
                {
                    MessageBox.Show(Const.mess_erro_sys);
                    return;
                }

                var base64Data = "data:image/png;base64," + base64String;
                var fileType = "png";

                var uploadId = RequestHTTP.call_uploadMediaBase64(base64Data, fileType);
                
                var dataRequest = new
                {
                    url = "../upload/getdata.jsp?id=" + uploadId,
                    hosobenhanid = hoSoBenhNhanID
                };

                var dataJson = JsonConvert.SerializeObject(dataRequest).Replace("\"", "\\\"");

                var rs = RequestHTTP.call_ajaxCALL_SP_S_result("NGT01T001.CHUPANH", dataJson);
                if ("1".Equals(rs))
                {
                    MessageBox.Show(Const.mess_chupanh_uploadanhthanhcong);
                    if (Event_Return != null)
                    {
                        Event_Return(img, null);
                    }

                    this.Close();
                }                      
                else
                {
                    MessageBox.Show(Const.mess_chupanh_uploadanhthatbai + rs);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex.ToString());
            }
        }

        /// <summary>
        /// Tắt capture
        /// Thoát khỏi màn hình chụp ảnh
        /// </summary>
        private void Thoat ()
        {
            this.Close();
        }

        private Image CaptureImg ()
        {
            int quality = 90;

            return Global.CompressImage(spicHinhAnhCapture.Image, quality);
        }

        /// <summary>
        /// Hiển thị hình ảnh đã chụp
        /// </summary>
        /// <param name="img"></param>
        private void ShowImage (Image img)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    CallBackShowImg cb = new CallBackShowImg(ShowImage);
                    this.Invoke(cb, new object[] { img });
                }
                else
                {
                    this.spicHinhAnhCapture.Image = img;
                }
            }
            catch { }
        }

        /// <summary>
        /// Chuyển kiểu image sang dạng chuỗi base64
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ConvertImageToBase64(Image img)
        {
            string rs = string.Empty;

            try
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var byteArray = ms.ToArray();

                if (byteArray != null)
                {
                    ms = new MemoryStream(byteArray);
                    Image returnImage = Image.FromStream(ms);
                    using (MemoryStream m = new MemoryStream())
                    {
                        returnImage.Save(m, returnImage.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        rs = Convert.ToBase64String(imageBytes);
                    }
                }

                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #endregion

        #region Public

        public NGT02K026_ChupAnhBenhNhan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Nhận param hồ sơ bệnh nhân từ màn hình cha
        /// </summary>
        /// <param name="hoSoBenhNhanID"></param>
        public void SetParams(string hoSoBenhNhanID)
        {
            this.hoSoBenhNhanID = hoSoBenhNhanID;
        }

        #endregion

        #region Events

        protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
        {
            return new UserControl.MyFormPainter(this, LookAndFeel);
        }
        
        private void NGT02K026_ChupAnhBenhNhan_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void btnChupAnh_Click(object sender, EventArgs e)
        {
            this.ChupAnh();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            this.LuuAnh();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Thoat();
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (eventArgs != null && eventArgs.Frame != null)
            {
                Bitmap img = (Bitmap)eventArgs.Frame.Clone();
                this.ShowImage((Bitmap)eventArgs.Frame.Clone());
            }
        }
        
        public void SetEvent_Return(EventHandler eventReturn)
        {
            this.Event_Return = eventReturn;
        }

        private void NGT02K026_ChupAnhBenhNhan_FormClosing(object sender, FormClosingEventArgs e)
        {
            var rs = this.StopCapture();
        }

        #endregion
    }
}