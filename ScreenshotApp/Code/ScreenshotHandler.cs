using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenshotApp.Code
{
    class ScreenshotHandler
    {
        static string fileName = "screenshot_";
        static string fileType = ".png";

        internal static void TakeScreenShot(string path)
        {
            string screenshotPath;
            screenshotPath = GetScreenshotPath(path);
            Console.WriteLine(screenshotPath);

            try
            {
                Point mousePoint = new Point(Control.MousePosition.X,Control.MousePosition.Y);
                Rectangle captureRectangle = Screen.FromPoint(mousePoint).Bounds;

                Bitmap captureBitmap = new Bitmap(captureRectangle.Width, captureRectangle.Height, PixelFormat.Format32bppArgb);

                Graphics captureGraphics = Graphics.FromImage(captureBitmap);


                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);

                //Saving the Image File (I am here Saving it in My E drive).
                captureBitmap.Save(screenshotPath, ImageFormat.Png);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
