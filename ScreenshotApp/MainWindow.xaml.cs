using ScreenshotApp.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;


namespace ScreenshotApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string txtPath = @"K:\Screenshots\";
        public static MainWindow instance;
        CaptureList captures;
        public MainWindow()
        {
            InitializeComponent();
            pathText.Text = txtPath;
            this.DataContext = new CaptureCommandContext();
            captures = new CaptureList();
            instance = this;
        }

        private void ScreenshotBtn_Click(object sender, RoutedEventArgs e)
        {
            CaptureScreen();
        }

        public void CaptureScreen()
        {
            ScreenshotData data = ScreenshotHandler.TakeScreenShot(txtPath);
            latestScreenshot.Source = data.imageSource;
            captures.AddCapture(data,txtPath, (bool)saveCheckBox.IsChecked);
        }

        private void SavePathBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog openFileDlg = new FolderBrowserDialog();
            openFileDlg.ShowNewFolderButton = true;
            openFileDlg.SelectedPath = txtPath;
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                pathText.Text = openFileDlg.SelectedPath;
                txtPath = pathText.Text;
                if (!txtPath.EndsWith(@"\"))
                    txtPath += @"\";
            }
        }

        private void pathText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            latestScreenshot.Source = captures.GetNext();
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            latestScreenshot.Source = captures.GetPrevious();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            captures.SaveCurrentShot();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
    public static class Command
    {
        public static RoutedCommand captureCommand = new RoutedCommand();
        public static RoutedCommand next = new RoutedCommand();
        public static RoutedCommand prev= new RoutedCommand();
    }
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
