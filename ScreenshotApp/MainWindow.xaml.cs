using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using ScreenshotApp.Code;

namespace ScreenshotApp
{
    public partial class MainWindow : Window
    {
        static string txtPath = @"K:\Screenshots\";
        public static MainWindow instance;
        CaptureList captures;
        Image img;

        public MainWindow()
        {
            InitializeComponent();
            pathText.Text = txtPath;
            this.DataContext = new CaptureCommandContext();
            captures = new CaptureList();
            instance = this;
            img = new Image();
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
            img.Source = data.imageSource;
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

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            latestScreenshot.Source = captures.GetNext();
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            latestScreenshot.Source = captures.GetPrevious();
        }

        private void pathText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            captures.SaveCurrentShot();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    
   

}
