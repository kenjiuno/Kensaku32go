using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kensaku32go {
    /// <summary>
    /// VWin.xaml の相互作用ロジック
    /// </summary>
    public partial class VWin : Window {
        public VWin() {
            InitializeComponent();
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var seli = cb.SelectedItem as Seli;
            if (seli != null && seli.Pos != null) {
                rtb.Selection.Select(seli.Pos.ElementStart, seli.Pos.ElementEnd);
                rtb.Focus();
                seli.Pos.BringIntoView();
            }
        }
    }
}
