using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScreenshotApp.Code
{
    struct ScreenshotData
    {
        public Bitmap captureBitmap;
        public BitmapSource bitmapSource;
        public Graphics captureGraphics;
        public ImageSource imageSource;
        public string time;

        public ScreenshotData(Bitmap captureBitmap, BitmapSource bitmapSource, Graphics captureGraphics, ImageSource imageSource, string time)
        {
            this.captureBitmap = captureBitmap;
            this.bitmapSource = bitmapSource;
            this.captureGraphics = captureGraphics;
            this.imageSource = imageSource;
            this.time = time;
        }
    }
}
