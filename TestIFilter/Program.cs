using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestIFilter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Environment.SetEnvironmentVariable(
                "PATH",
                Environment.GetEnvironmentVariable("PATH")
                + ";"
                + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, (IntPtr.Size == 4) ? "x86" : "x64")
            );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
