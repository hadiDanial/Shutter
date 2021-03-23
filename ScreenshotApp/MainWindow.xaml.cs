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

        public MainWindow()
        {
            InitializeComponent();
            pathText.Text = txtPath;
        }

        private void ScreenshotBtn_Click(object sender, RoutedEventArgs e)
        {
            ScreenshotData data = ScreenshotHandler.TakeScreenShot(txtPath, (bool)saveCheckBox.IsChecked);
            latestScreenshot.Source = data.imageSource;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
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
    }
}
