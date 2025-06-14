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

namespace NET.Paint.View.Component.Tools.Subcomponent
{
    /// <summary>
    /// Interaction logic for Shapes.xaml
    /// </summary>
    public partial class Shape : UserControl
    {
        public Shape()
        {
            InitializeComponent();
        }

        private void SelectTool(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XTools;
            var button = sender as RadioButton;

            if (context != null && button != null)
                context.ActiveTool = (ToolType)button.Tag;
        }
    }
}
