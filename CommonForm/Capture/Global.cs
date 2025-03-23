using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace VNPT.HIS.CommonForm.Capture
{
    public static class Global
    {
        public static float curBrightness = 1.2f;
        public static int curDevice = 0;
        public static double curFrameRate = 25.0;
        public static int curHeight = 480;
        public static int curWidth = 640;
        public static Size curSize = new Size();
        public static int curSource = -1;
        public static int nVideoCompressor = -1;
        public static string fileDevice = (Application.StartupPath + @"\DeviceConfig.txt");
        public static bool _LoadDevice = false;
        public static Filters filters = null;
        public static Capture capture = null;
        public static string dirVideo = "";
        public static Control _Owner = new Control();
        public static bool _FirstRC = false;

        public static void InitDevice()
        {
            if (_LoadDevice == false) return;
            if (Global.curDevice >= 0)
            {
                try
                {
                    if (capture != null) return;
                    filters = new Filters();
                    if (filters.VideoInputDevices.Count <= 0) return;
                    Filter videoDevice = null;
                    Filter audioDevice = null;
                    videoDevice = (Global.curDevice >= 0) ? filters.VideoInputDevices[Global.curDevice] : null;
                    //MessageBox.Show("VideoInputDevices: " + filters.VideoInputDevices.Count + "CurDV: " + Global.curDevice);
                    audioDevice = (filters.AudioInputDevices.Count > 0) ? filters.AudioInputDevices[0] : null;
                    //MessageBox.Show("AudioInputDevices: " + filters.AudioInputDevices.Count);
                    if (videoDevice != null)
                    {
                        capture = new Capture(videoDevice, audioDevice, false);
                        try
                        {
                            //SourceCollection sc = new SourceCollection();
                            //MessageBox.Show(sc.Count.ToString() + "#" + capture.VideoSources.Count + "#" );//+ capture.VideoSources.CurrentSource.Name);
                            if (curSource >= 0 && curSource < capture.VideoSources.Count)
                                capture.VideoSource = capture.VideoSources[curSource];
                            else if (capture.VideoSources.Count > 0)
                                capture.VideoSource = capture.VideoSources[0];

                            MessageBox.Show("VideoSources: " + capture.VideoSource.Name);
                        }
                        catch (Exception ex)
                        {
                            //try
                            //{                                
                            //    for (int i = 0; i < capture.PropertyPages.Count; i++)
                            //    {
                            //        if (capture.PropertyPages[i].Name.ToLower().Contains("video crossbar"))
                            //        {
                            //            capture.PropertyPages[i].Show(_Owner);
                            //        }
                            //    }
                            //}
                            //catch (Exception exception)
                            //{
                            //    MessageBox.Show(exception.Message);
                            //}
                            //MessageBox.Show("VideoSource error: " + ex.Message);
                        }
                        try
                        {

                            capture.FrameRate = Global.curFrameRate;
                            //MessageBox.Show(Global.curFrameRate + "<>" + capture.FrameRate);
                            //if (capture.FrameRate != Global.curFrameRate) {
                            //    MessageBox.Show(Global.curFrameRate + "<>" + capture.FrameRate);
                            //    try
                            //    {
                            //        for (int i = 0; i < capture.PropertyPages.Count; i++)
                            //        {
                            //            if (capture.PropertyPages[i].Name.ToLower().Contains("video capture device"))
                            //            {
                            //                capture.PropertyPages[i].Show(_Owner);
                            //            }
                            //        }
                            //    }
                            //    catch {}

                            //}
                        }
                        catch (Exception ex)
                        {
                            //try
                            //{
                            //    for (int i = 0; i < capture.PropertyPages.Count; i++)
                            //    {
                            //        if (capture.PropertyPages[i].Name.ToLower().Contains("video capture device"))
                            //        {
                            //            capture.PropertyPages[i].Show(_Owner);
                            //        }
                            //    }
                            //}
                            //catch (Exception exception)
                            //{
                            //    MessageBox.Show(exception.Message);
                            //}

                            //MessageBox.Show("FrameRate error: " + ex.Message); 
                        }
                        try
                        {
                            capture.FrameSize = Global.curSize;

                        }
                        catch { }//(Exception ex) { MessageBox.Show("FrameSize error: " + ex.Message); }
                        if (Global.nVideoCompressor >= 0)
                        {
                            try
                            {
                                capture.VideoCompressor = filters.VideoCompressors[Global.nVideoCompressor];
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine(ex.ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                    //MessageBox.Show(exception3.Message, "Th\x00f4ng b\x00e1o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Kh\x00f4ng/Chưa c\x00f3 thiết bị capture được chọn. \n Vui l\x00f2ng xem lại thiết bị gắn v\x00e0o.", "Th\x00f4ng b\x00e1o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void LoadDeviceConfig()
        {
            //if (_LoadDevice) return;
            if (File.Exists(fileDevice))
            {
                StreamReader reader = new StreamReader(fileDevice);
                string[] strArray = reader.ReadToEnd().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                reader.Close();
                reader.Dispose();
                if (strArray.Length >= 1)
                {
                    curDevice = string.IsNullOrEmpty(strArray[0].Trim()) ? 0 : int.Parse(strArray[0].Trim());
                }
                if (strArray.Length >= 2)
                {
                    string[] strArray2 = (string.IsNullOrEmpty(strArray[1].Trim()) ? "640 x 480" : strArray[1].Trim().ToLower()).Split(new char[] { 'x' });
                    try
                    {
                        curWidth = (strArray2.Length >= 1) ? int.Parse(strArray2[0].ToString().Trim()) : 640;
                    }
                    catch
                    {
                        curWidth = 640;
                    }
                    try
                    {
                        curHeight = (strArray2.Length >= 2) ? int.Parse(strArray2[1].ToString().Trim()) : 480;
                    }
                    catch
                    {
                        curHeight = 480;
                    }
                    curSize.Width = curWidth;
                    curSize.Height = curHeight;
                }
                if (strArray.Length >= 3)
                {
                    curSource = string.IsNullOrEmpty(strArray[2].Trim()) ? 0 : int.Parse(strArray[2].Trim());
                }
                if (strArray.Length >= 4)
                {
                    string s = string.IsNullOrEmpty(strArray[3].Trim()) ? "25" : strArray[3].Trim();
                    curFrameRate = double.Parse(s);
                }
                if (strArray.Length >= 5)
                {
                    curBrightness = (float)Convert.ToDouble(strArray[4]);
                }
                if (strArray.Length >= 6)
                {
                    nVideoCompressor = (int)Convert.ToDouble(strArray[5]);
                }
                _LoadDevice = true;
            }
        }

        private static void SaveConfig()
        {
            //Global.curDevice = capture.;
            Global.curFrameRate = capture.FrameRate;
            //string[] strArray = this.cboFramesize.Text.Split(new char[] { 'x' });
            Global.curWidth = capture.FrameSize.Width;// (strArray.Length >= 1) ? int.Parse(strArray[0]) : Global.curWidth;
            Global.curHeight = capture.FrameSize.Height; //(strArray.Length >= 2) ? int.Parse(strArray[1]) : Global.curHeight;
            Global.curSize.Width = Global.curWidth;
            Global.curSize.Height = Global.curHeight;
            //Global.curSource = this.cboSource.SelectedIndex;
            //Global.curBrightness = capture. //(float)((Convert.ToDouble(this.trbBrightness.Value) / 10.0) + 1.0);
            //if (this.cboCompressor.SelectedIndex >= 1)
            //{
            //    Global.nVideoCompressor = capture.VideoCompressor;
            //}
            //Global.filters.
            string str2 = Global.curDevice.ToString();
            string str = ((((str2 + "|" + Global.curWidth.ToString() + " x " + Global.curHeight.ToString()) + "|" + Global.curSource.ToString()) + "|" + Global.curFrameRate.ToString()) + "|" + Global.curBrightness.ToString()) + "|" + Global.nVideoCompressor.ToString();
            StreamWriter writer = new StreamWriter(Global.fileDevice);
            writer.Write(str);
            writer.Flush();
            writer.Close();
            writer.Dispose();

        }

        public static Image Crop(Image image, Rectangle selection)
        {
            Bitmap bitmap = image as Bitmap;
            if (bitmap == null)
            {
                throw new ArgumentException("Kein g\x00fcltiges Bild (Bitmap)");
            }
            Bitmap bitmap2 = bitmap.Clone(selection, bitmap.PixelFormat);
            image.Dispose();
            return bitmap2;
        }

        public static Image Fit2PictureBox(Image image, PictureBox picBox)
        {
            Bitmap bitmap = null;
            double num = ((double)image.Width) / ((double)picBox.Width);
            double num2 = ((double)image.Height) / ((double)picBox.Height);
            double num3 = (num < num2) ? num2 : num;
            bitmap = new Bitmap((int)(((double)image.Width) / num3), (int)(((double)image.Height) / num3));
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            image.Dispose();
            return bitmap;
        }

        public static Image ResizeImage(Image img, int per)
        {
            //giảm dung lượng mail ảnh
            int Width = img.Width * per;
            int Height = img.Height * per;
            //Image img = Image.FromFile(pathTmpMail);
            Bitmap bmp = new Bitmap(Width, Height);
            ////Bitmap bmp = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphic.DrawImage(img, 0, 0, Width, Height);


            graphic.Dispose();
            bmp.Dispose();
            img.Dispose();
            return (Image)bmp;
        }


        public static Image CompressImage(Image img, int quality)
        {
            if (img == null) return null;
            ////Setting the quality of the picture
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ////Seting the format to save
            ImageCodecInfo imageCodec = GetImageCoeInfo("image/jpeg");
            ////Used to contain the poarameters of the quality
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = qualityParam;
            ////Used to save the image to a  given path
            System.IO.MemoryStream stre = new System.IO.MemoryStream();
            Bitmap bm = new Bitmap(img);
            bm.Save(stre, imageCodec, parameters);
            //
            Image im = Image.FromStream(stre);
            stre.Close();
            return new Bitmap(im);
        }

        static ImageCodecInfo GetImageCoeInfo(string mimeType)
        {
            ImageCodecInfo[] codes = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codes.Length; i++)
            {
                if (codes[i].MimeType == mimeType)
                {
                    return codes[i];
                }
            }
            return null;
        }

        public static System.Drawing.Point GetRatioImg(Control ctrl, System.Drawing.Image img, ref double scaleW, ref double scaleH, ref bool zoomout, System.Drawing.Point pos)
        {

            System.Drawing.Point unscaled_p = new System.Drawing.Point();
            if (img == null) return unscaled_p;
            // image and container dimensions
            int wi = img.Width;
            int hi = img.Height;
            int wc = ctrl.ClientRectangle.Width;
            int hc = ctrl.ClientRectangle.Height;

            zoomout = wi < wc || hi < hc;

            //int wr = 0, hr = 0;
            //if (wi > hi)
            //{
            //    wr = wc;
            //    hr = (wc * hi) / wi;
            //}
            //else
            //{
            //    hr = hc;
            //    wr = (hc * wi) / hi;
            //}
            //scaleW = wr;// (double)((double)wi / (double)wr);
            //scaleH = hr;// (double)((double)hi / (double)hr);

            //System.Drawing.Point p = ctrl.PointToClient(pos);
            float imageRatio = wi / (float)hi; // image W:H ratio
            float containerRatio = wc / (float)hc; // container W:H ratio
            if (imageRatio >= containerRatio)
            {
                // horizontal image
                float scaleFactor = wc / (float)wi;
                float scaledHeight = hi * scaleFactor;
                // calculate gap between top of container and top of image
                float filler = Math.Abs(hc - scaledHeight) / 2;
                unscaled_p.X = (int)(pos.X / scaleFactor);
                unscaled_p.Y = (int)((pos.Y - filler) / scaleFactor);
                scaleW = (int)(scaleW / scaleFactor);

                int ye = (int)((pos.Y + scaleH - filler) / scaleFactor);
                scaleH = ye - unscaled_p.Y;
            }
            else
            {
                // vertical image
                float scaleFactor = hc / (float)hi;
                float scaledWidth = wi * scaleFactor;
                float filler = Math.Abs(wc - scaledWidth) / 2;
                unscaled_p.X = (int)((pos.X - filler) / scaleFactor);
                unscaled_p.Y = (int)(pos.Y / scaleFactor);
                int xe = (int)((pos.X + scaleW - filler) / scaleFactor);
                scaleW = xe - unscaled_p.X;
                scaleH = (int)(scaleH / scaleFactor);
            }

            return unscaled_p;
        }

    }

}
