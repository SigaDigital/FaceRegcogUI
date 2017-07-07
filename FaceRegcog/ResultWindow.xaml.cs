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
using System.IO;

namespace FaceRegcog
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            char[] delimiter = { ' ' };
            InitializeComponent();
            string line;
            List<TodoItem> items = new List<TodoItem>();
            List<TodoItem> trash = new List<TodoItem>();
            System.IO.StreamReader file =
                new System.IO.StreamReader("tmp_crop_face/info.txt");
            var imgCropPath = Directory.GetFiles("./tmp_crop_face", "*.jpg");
            foreach (var imgPath in imgCropPath)
            {
              line = file.ReadLine();
              string[] words = line.Split(delimiter);
              System.Diagnostics.Debug.Write(imgPath + "\n");
              System.Diagnostics.Debug.Write(words[0] + "\n");
              System.Diagnostics.Debug.Write(words[1] + "\n");
              if(string.Compare(words[0], "known") == 0)
                items.Add(new TodoItem() { Name = words[1], Path = System.IO.Path.GetFullPath(imgPath) });      
              else
                trash.Add(new TodoItem() { Name = words[1], Path = System.IO.Path.GetFullPath(imgPath) });
            }
            file.Close();
            TodoList.ItemsSource = items;
            NotTodoList.ItemsSource = trash;
        }

        ~ResultWindow()
        {
            Directory.Delete("tmp_crop_face", true);
        }

        private void selectChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (NotTodoList.SelectedItem != null)
            {
                AskName ask = new AskName((NotTodoList.SelectedItem as TodoItem).Path);
                ask.Show();
            }
        }

    }

    public class TodoItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
