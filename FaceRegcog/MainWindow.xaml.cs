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
using System.Diagnostics;
using System.IO;

namespace FaceRegcog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.OpenFileDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = dialog.FileName;
                    System.Diagnostics.Debug.Write(fileName);
                    Video.Source = new Uri(dialog.FileName);
                }
            }
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "video-tagging.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = fileName + " svm " + Rate.Text;
            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    DirectoryInfo di = Directory.CreateDirectory("tmp_crop_face");
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    exeProcess.WaitForExit();
                    ResultWindow res = new ResultWindow();
                    res.Show();
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}
