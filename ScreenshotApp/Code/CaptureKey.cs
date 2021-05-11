using System;
using System.Windows.Input;

namespace ScreenshotApp.Code
{
    public class CaptureKey : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindow.instance.CaptureScreen();
        }
    }
}
