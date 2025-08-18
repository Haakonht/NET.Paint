using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
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

namespace NET.Paint.View.Component.Tools.Controls
{
    /// <summary>
    /// Interaction logic for Colors.xaml
    /// </summary>
    public partial class Fill : UserControl
    {
        public Fill()
        {
            InitializeComponent();
        }

        private void SolidButton(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is XTools tools)
            {
                tools.ActiveFillType = XColorType.Solid;
            }
        }

        private void GradientButton(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is XTools tools)
            {
                tools.ActiveFillType = XColorType.Gradient;
            }
        }
    }
}
