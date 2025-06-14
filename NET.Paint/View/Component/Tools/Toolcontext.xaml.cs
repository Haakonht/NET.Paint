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

namespace NET.Paint.View.Component.Tools
{
    /// <summary>
    /// Interaction logic for Toolcontext.xaml
    /// </summary>
    public partial class Toolcontext : UserControl
    {
        public Toolcontext()
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
