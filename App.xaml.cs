using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kensaku32go
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public static string fpdb = null;
        public static string[] dirs;

        protected override void OnStartup(StartupEventArgs e)
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            foreach (String a in e.Args)
            {
                if (a.StartsWith("/") || a.StartsWith("--")) continue;
                fpdb = a;
                break;
            }
            if (fpdb == null)
            {
                MainWindow = new StartupWindow();
                MainWindow.Show();
                return;
            }
            else
            {
                MainWindow = new MainWindow();
                MainWindow.Show();
                return;
            }
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("" + e.Exception, "検索32号", MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
