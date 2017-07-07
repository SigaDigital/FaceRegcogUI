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
using System.Windows.Shapes;

namespace FaceRegcog
{
    /// <summary>
    /// Interaction logic for AskName.xaml
    /// </summary>
    public partial class AskName : Window
    {
        private string fileName;
        private string targetPath;
        private string sourceFile;
        public AskName()
        {
            InitializeComponent();
        }

        ~AskName()
        {
        }

        public AskName(string pathFile)
        {
            InitializeComponent();

            sourceFile = pathFile;
            fileName = System.IO.Path.GetFileName(pathFile);
            targetPath = "save";
            System.IO.Directory.CreateDirectory(targetPath);

            System.Diagnostics.Debug.Write(pathFile + "\n");
        }

        private void Name_Click(object sender, RoutedEventArgs e)
        {
            string label = nameLabel.Text;
            string destPath = targetPath + "/" + label;
            System.IO.Directory.CreateDirectory(destPath);
            string destFile = System.IO.Path.Combine(destPath, fileName);
            System.IO.File.Copy(sourceFile, destFile, true);
            this.Close();
        }
    }
}
