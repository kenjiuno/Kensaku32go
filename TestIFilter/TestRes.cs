using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestIFilter
{
    public partial class TestRes : UserControl
    {
        public TestRes()
        {
            InitializeComponent();
        }

        public Image Icon
        {
            get => icon.Image;
            set => icon.Image = value;
        }

        public string Ext
        {
            get => ext.Text;
            set => ext.Text = value;
        }

        public string Error
        {
            get => errorText.Text;
            set => errorText.Text = value;
        }
    }
}
