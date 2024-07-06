using LibIFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestIFilter.Properties;

namespace TestIFilter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Text += " " + ((IntPtr.Size == 4) ? "(32 ビット)" : "(64 ビット)");

            foreach (var file in Directory.GetFiles(Path.Combine(Application.StartupPath, "Files")))
            {
                var ctl = new TestRes();
                ctl.Ext = Path.GetExtension(file);
                ctl.Icon = Resources.ajax_loader;
                tests.Controls.Add(ctl);

                try
                {
                    var text = await TextExtractor.GetTextUsingIFilterAsync(file);
                    ctl.Icon = Resources.StatusAnnotations_Complete_and_ok_16xLG_color;
                    ctl.Error = "";
                }
                catch (Exception ex)
                {
                    ctl.Icon = Resources.StatusAnnotations_Critical_16xLG_color;
                    ctl.Error = ex.Message;
                }
            }
        }

        private void filterPack_Click(object sender, EventArgs e)
        {
            Process.Start(((ToolStripItem)sender).ToolTipText);
        }
    }
}
