using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Forms;

namespace ScreenshotApp.Code
{
    class CaptureList
    {
        static string namePrefix = "screenshot_";
        static string fileType = ".png";
        static string defaultPath;
        List<ScreenshotData> captures;
        int currentIndex = 0;

        public CaptureList(string path = "")
        {
            captures = new List<ScreenshotData>();
            SetPath(path);
        }


        private static void SetPath(string path)
        {
            if (path != string.Empty)
            {
                defaultPath = path;
            }
        }

        public void AddCapture(ScreenshotData data, string path = "", bool autosave = false)
        {
            captures.Add(data);
            SetPath(path);
            currentIndex = captures.Count - 1;
            if (autosave)
                SaveCurrentShot();
            CopyToClipboard(data);
        }

        /// <summary>
        /// Copies a screenshot to the clipboard.
        /// </summary>
        /// <param name="data"></param>
        private static void CopyToClipboard(ScreenshotData data)
        {
            Clipboard.SetImage(data.captureBitmap);
        }

        /// <summary>
        /// Copies the current selected screenshot to the clipboard
        /// </summary>
        private void CopyToClipboard()
        {
            Clipboard.SetImage(captures[currentIndex].captureBitmap);
        }

        internal ImageSource GetNext()
        {
            if (captures.Count == 0) return null;
            currentIndex++;
            currentIndex = currentIndex % captures.Count;
            CopyToClipboard();
            return captures[currentIndex].imageSource;
        }
        internal ImageSource GetPrevious()
        {
            if (captures.Count == 0) return null;
            currentIndex--;
            if (currentIndex < 0) currentIndex += captures.Count;
            CopyToClipboard();
            return captures[currentIndex].imageSource;
        }

        internal void SaveCurrentShot(string path = "")
        {
            SetPath(path);
            string finalPath = defaultPath + namePrefix + captures[currentIndex].time + fileType;
            captures[currentIndex].captureBitmap.Save(finalPath, ImageFormat.Png);
        }
    }
}
