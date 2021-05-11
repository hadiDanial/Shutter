using System.Windows.Input;

namespace ScreenshotApp.Code
{
    public class CaptureCommandContext
    {
        public ICommand captureCommand
        {
            get
            {
                return new CaptureKey();
            }
        }
    }
}
