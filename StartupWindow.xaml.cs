using Kensaku32go.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Kensaku32go
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        private readonly string xmlFilePath;
        private readonly Config config = new Config();

        public StartupWindow()
        {
            InitializeComponent();

            xmlFilePath = Path.Combine(Environment.ExpandEnvironmentVariables(@"%APPDATA%\Kensaku32go\Default.xml"));
            Directory.CreateDirectory(Path.GetDirectoryName(xmlFilePath));

            if (File.Exists(xmlFilePath) && new FileInfo(xmlFilePath).Length >= 1)
            {
                using (var fs = File.OpenRead(xmlFilePath))
                {
                    config = (Config)new XmlSerializer(typeof(Config)).Deserialize(fs);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sources.ItemsSource = new string[0]
                .Concat(
                    new string[]
                    {
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    }
                )
                .Concat(
                    Enumerable.Range(0, 26)
                    .Select(index => ((char)('A' + index)) + ":\\")
                    .Where(Directory.Exists)
                )
                .ToArray();

            listDirs.ItemsSource = config.Dirs;
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var fs = File.Create(xmlFilePath))
            {
                new XmlSerializer(typeof(Config)).Serialize(fs, config);
            }

            App.fpdb = Path.ChangeExtension(xmlFilePath, ".db");
            App.dirs = config.Dirs.ToArray();

            App.Current.MainWindow = new MainWindow();
            App.Current.MainWindow.Show();
            Close();
        }

        private void listDirs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listDirs.SelectedItem != null)
            {
                while (config.Dirs.Remove(listDirs.SelectedItem as string))
                {
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dir = ((Button)sender).Content + "";
            while (config.Dirs.Remove(dir))
            {
            }
            config.Dirs.Add(dir);
        }

        private void listDirs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = e.AllowedEffects & (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
        }

        private void listDirs_Drop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null)
            {
                foreach (var dir in files.Where(it => Directory.Exists(it)))
                {
                    while (config.Dirs.Remove(dir))
                    {
                    }
                    config.Dirs.Add(dir);
                }
            }
        }
    }
}
