using System.Drawing;
using System.Windows.Media;

namespace ScreenshotApp.Code
{
    struct ScreenshotData
    {
        public Bitmap captureBitmap;
        public Graphics captureGraphics;
        public ImageSource imageSource;
        public string name, path;

        public ScreenshotData(Bitmap captureBitmap, Graphics captureGraphics, ImageSource imageSource, string name, string path)
        {
            this.captureBitmap = captureBitmap;
            this.captureGraphics = captureGraphics;
            this.imageSource = imageSource;
            this.name = name;
            this.path = path;
        }
    }
}
