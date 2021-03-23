using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace ScreenshotApp.Code
{
    class ScreenshotHandler
    {
        static string fileName = "screenshot_";
        static string fileType = ".png";

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        internal static ImageSource TakeScreenShot(string path, bool saveFile = false)
        {
            string screenshotPath;
            screenshotPath = GetScreenshotPath(path);
            Console.WriteLine(screenshotPath);
            Bitmap captureBitmap = null;
            try
            {
                Point mousePoint = new Point(Control.MousePosition.X,Control.MousePosition.Y);
                Rectangle captureRectangle = Screen.FromPoint(mousePoint).Bounds;

                captureBitmap = new Bitmap(captureRectangle.Width, captureRectangle.Height, PixelFormat.Format32bppArgb);

                Graphics captureGraphics = Graphics.FromImage(captureBitmap);


                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

                if (saveFile)
                    captureBitmap.Save(screenshotPath, ImageFormat.Png);


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return ImageSourceFromBitmap(captureBitmap);
        }

        private static string GetScreenshotPath(string path)
        {
            DateTime date = DateTime.Now;
            string time = date.Day.ToString("D2") + "_" + date.Month.ToString("D2") + "_" + date.TimeOfDay.Hours.ToString("D2")
                + "_" + date.TimeOfDay.Minutes.ToString("D2") + "_" + date.TimeOfDay.Seconds.ToString("D2");
            return path + fileName + time + fileType;
        }
    }
}
