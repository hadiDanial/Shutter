using System;
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
    public class ScreenshotHandler
    {
        static string fileName = "screenshot_";
        static string fileType = ".png";

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        /// <summary>
        /// Converts from a Bitmap to an ImageSource
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }


        /// <summary>
        /// Takes a screenshot and optionally saves it.
        /// Adds it to a list?
        /// </summary>
        /// <param name="path"></param>
        /// <param name="saveFile"></param>
        /// <returns>All the data related to the screenshot.</returns>
        internal static ScreenshotData TakeScreenShot(string path, bool saveFile = false)
        {
            string screenshotPath;
            string time = GetCurrentTime();
            screenshotPath = path + fileName + time + fileType;

            Console.WriteLine(screenshotPath);
            Bitmap captureBitmap = null;

            ScreenshotData data;

            Point mousePoint = new Point(Control.MousePosition.X, Control.MousePosition.Y);

            // Returns the screen bounds of the screen that contains the mouse.
            Rectangle captureRectangle = Screen.FromPoint(mousePoint).Bounds;

            captureBitmap = new Bitmap(captureRectangle.Width, captureRectangle.Height, PixelFormat.Format32bppArgb);

            Graphics captureGraphics = Graphics.FromImage(captureBitmap);


            //Copying Image from The Screen
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

            if (saveFile)
                captureBitmap.Save(screenshotPath, ImageFormat.Png);


            data = new ScreenshotData(captureBitmap, captureGraphics, ImageSourceFromBitmap(captureBitmap), time, path);

            return data;
        }


        private static string GetCurrentTime()
        {
            DateTime date = DateTime.Now;
            return date.Day.ToString("D2") + "_" + date.Month.ToString("D2") + "_" + date.TimeOfDay.Hours.ToString("D2")
                + "_" + date.TimeOfDay.Minutes.ToString("D2") + "_" + date.TimeOfDay.Seconds.ToString("D2");
        }
    }
}
