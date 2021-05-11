using System.Windows.Input;

namespace ScreenshotApp.Code
{
    public static class Command
    {
        public static RoutedCommand captureCommand = new RoutedCommand();
        public static RoutedCommand next = new RoutedCommand();
        public static RoutedCommand prev = new RoutedCommand();
    }
}
