using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitHub.secile.Video;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace SpertroApp
{

    public partial class Form1 : Form
    {
        //Camera
        UsbCamera camera;

        //Live Stop
        public bool bCameraLive = false;

        //ImageBuff
        //private byte[] GrayBuffer;

        //ROI
        Point ROI_Point;
        const int ROI_Edge = 10;

        private object BufferLock = new object();
        private Bitmap CopyBitmap(byte[] Buffer,int width, int height)
        {
            
            var result = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            if (Buffer == null) return result;

            var bmp_data = result.LockBits(new Rectangle(Point.Empty, result.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            lock (BufferLock)
            {
                // copy from last row.
                for (int y = 0; y < height; y++)
                {
                    int stride = width* 3;

                    var src_idx = Buffer.Length - (stride * (y + 1));
                    var dst = IntPtr.Add(bmp_data.Scan0, stride * y);
                    
                    Marshal.Copy(Buffer, src_idx, dst, stride);

                }
            }
            result.UnlockBits(bmp_data);

            return result;
        }

        //convert image to bytearray
        public static byte[] ImageToBuffer(Image Image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            if (Image == null) { return null; }
            byte[] data = null;
            using (MemoryStream oMemoryStream = new MemoryStream())
            {
                //建立副本
                using (Bitmap oBitmap = new Bitmap(Image))
                {
                    //儲存圖片到 MemoryStream 物件，並且指定儲存影像之格式
                    oBitmap.Save(oMemoryStream, imageFormat);
                    //設定資料流位置
                    oMemoryStream.Position = 0;
                    //設定 buffer 長度
                    data = new byte[oMemoryStream.Length];
                    //將資料寫入 buffer
                    oMemoryStream.Read(data, 0, Convert.ToInt32(oMemoryStream.Length));
                    //將所有緩衝區的資料寫入資料流
                    oMemoryStream.Flush();
                }
            }
            return data;
        }
        delegate void FormUpdata(int i);
        void FormUpdataMethod(int i)
        {
            Text = i.ToString();

        }
        //private async Task<bool> SpertroImageProcess(int i)
        private async Task<bool> SpertroImageProcess(byte[] imgbuffer)
        {
            //await Thread.Sleep(2000);
            //await Task.Delay(3000);

            int i = 0;

            //Task.Run(() => ShowThreadInfo("Task"));
            await Task.Run(() =>
            {
                //影像處理程序 把Do While 刪除 寫在這
                //測試用
                do
                {
                    i++;
                    //Text = i.ToString();
                    FormUpdata formcontrl = new FormUpdata(FormUpdataMethod);
                    this.Invoke(formcontrl, i);

                } while (i < 1000000000);

            });

            return true;
        }

        public Form1()
        {
            InitializeComponent();

            DrawCanvas.Parent = CCDImage;
            ROIImage.Parent = panel1;
            //初始化 ROI位置
            ROI_Point.X = 10;
            ROI_Point.Y = 10;
            DrawCanvas.Left = ROI_Point.X;
            DrawCanvas.Top = ROI_Point.Y;

            CCDImage.Width = 640;
            CCDImage.Height = 360;
            panel1.Width = 640;
            panel1.Height = 360;
            ROIImage.Width = 640;
            ROIImage.Height = 360;

            /*
            DrawCanvas.Top = 0;
            DrawCanvas.Left = 0;
            DrawCanvas.Width = CCDImage.Width;
            DrawCanvas.Height = CCDImage.Height;
            */
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            

            if (bCameraLive == false)
            {
                bCameraLive = true;
                camera.Start();
                
                //var bmp = camera.GetBitmap();
                btnStart.Text = "Stop";
                // show image in PictureBox.
                timer1.Start();

            }
            else
            {
                bCameraLive = false;
                btnStart.Text = "Start";
                timer1.Stop();
                camera.Stop();
            }

        }
        private static int inTimer = 0;
        private async void timer1_Tick(object sender, EventArgs e)
        {
            //Text = camera.Size.Width.ToString() + "," + camera.Size.Height.ToString();
            Bitmap myBitmap = camera.GetBitmap();

            int scaleX = camera.Size.Width / CCDImage.Width;
            int scaleY = camera.Size.Height / CCDImage.Height;

            int x = DrawCanvas.Left * scaleX;
            int y = DrawCanvas.Top * scaleY;
            int w = DrawCanvas.Width * scaleX;
            int h = DrawCanvas.Height * scaleY;
            RectangleF cloneRect = new RectangleF(x, y, w, h);
            System.Drawing.Imaging.PixelFormat format = myBitmap.PixelFormat;

            Bitmap cloneBitmap = myBitmap.Clone(cloneRect, format);

            CCDImage.Image = myBitmap;
            ROIImage.Image = cloneBitmap;

            ROIImage.Left = DrawCanvas.Left;
            ROIImage.Top = DrawCanvas.Top;
            ROIImage.Height = DrawCanvas.Height;
            ROIImage.Width = DrawCanvas.Width;

            byte[] ROIBuffer = ImageToBuffer(ROIImage.Image, System.Drawing.Imaging.ImageFormat.Bmp);

            if (Interlocked.Exchange(ref inTimer, 1) == 1)
                return;

            if (ROIBuffer == null)
            {
                Interlocked.Exchange(ref inTimer, 0);
                return;
            }


            Text = "Process...";


            //Image Process
            var  processTask = SpertroImageProcess(ROIBuffer);
            Task processFinishTask = await Task.WhenAny(processTask);

            Interlocked.Exchange(ref inTimer, 0);

        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            int cameraIndex = 0;
            // check format.
            string[] devices = UsbCamera.FindDevices();
            if (devices.Length == 0) return; // no camera.

            UsbCamera.VideoFormat[] formats = UsbCamera.GetVideoFormat(cameraIndex);
            //for (int i = 0; i < formats.Length; i++) Console.WriteLine("{0}:{1}", i, formats[i]);

            // create usb camera and start.
            camera = new UsbCamera(cameraIndex, formats[0]);
        }

        int iDirectionLock = -1;
        private void DrawCanvas_MouseDown(object sender, MouseEventArgs e)
        {

            PictureBox pDrawROI = (PictureBox)sender;
            

            if (e.Button.Equals(MouseButtons.Left))
            {
                /*
                if (e.X < ROI_Edge)
                {
                    iDirectionLock = 1;
                }
                else */if (e.X > pDrawROI.Width - ROI_Edge)
                {
                    iDirectionLock = 2;
                }
                /*
                else if (e.Y < ROI_Edge)
                {
                    iDirectionLock = 3;
                }
                */
                else if (e.Y > pDrawROI.Height - ROI_Edge)
                {
                    iDirectionLock = 4;
                }
                else 
                {
                    //移動 Top Left

                    iDirectionLock = 0;
                }
                ROI_Point = e.Location;
            }
        }


        private void DrawCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            PictureBox pDrawROI = (PictureBox)sender;
            //bool bEdge = false;
            /*
            if (e.X < ROI_Edge)
            {
                pDrawROI.Cursor = Cursors.PanWest;
            }
            else */if (e.X > pDrawROI.Width - ROI_Edge)
            {
                pDrawROI.Cursor = Cursors.PanEast;
            }
            /*
            else if (e.Y < ROI_Edge)
            {
                pDrawROI.Cursor = Cursors.PanNorth;
            }
            */
            else if (e.Y > pDrawROI.Height - ROI_Edge)
            {
                pDrawROI.Cursor = Cursors.PanSouth;
            }
            else
            {
                pDrawROI.Cursor = Cursors.Cross;
            }
            int X = pDrawROI.Left;
            int Y = pDrawROI.Top;
            int W = pDrawROI.Width;
            int H = pDrawROI.Height; 

            if (e.Button.Equals(MouseButtons.Left))
            {
                if (iDirectionLock == 1)
                {

                    X -= ROI_Point.X - e.Location.X;
                    W += ROI_Point.X - e.Location.X;

                }
                else if (iDirectionLock == 2)
                {
                    W += e.Location.X - ROI_Point.X;
                }

                else if (iDirectionLock == 3)
                {
                    X -= ROI_Point.Y - e.Location.Y;
                    H += ROI_Point.Y - e.Location.Y;

                }

                else if (iDirectionLock == 4)
                {
                    H += e.Location.Y - ROI_Point.Y;

                }
                
                if (iDirectionLock == 0)
                {
                    X += e.Location.X - ROI_Point.X;
                    Y += e.Location.Y - ROI_Point.Y;

                }
                else
                {
                    if (iDirectionLock == 1)
                    {
                        
                        ROI_Point.Y = e.Location.Y;
                    }
                    else if (iDirectionLock == 2)
                    {
                        ROI_Point.X = e.Location.X;
                        ROI_Point.Y = e.Location.Y;
                    }
                    else if (iDirectionLock == 3)
                    {
                        ROI_Point.X = e.Location.X;
                       
                    }
                    else if (iDirectionLock == 4)
                    {
                        ROI_Point.X = e.Location.X;
                        ROI_Point.Y = e.Location.Y;
                    }     
                }

                if (X >= 0 && X+W <= CCDImage.Width)
                    pDrawROI.Left = X;
                if (Y >= 0 && Y+H <= CCDImage.Height)
                    pDrawROI.Top = Y;
                if (X + W >= 0 && X + W <= CCDImage.Width)
                    pDrawROI.Width = W;
                if (Y + H >= 0 && Y + H <= CCDImage.Height)
                    pDrawROI.Height = H;
            }
        }

        private void DrawCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            iDirectionLock = -1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    
     
}


