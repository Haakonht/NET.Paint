using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
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

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Shortcuts.xaml
    /// </summary>
    public partial class Shortcuts : UserControl
    {
        public Shortcuts()
        {
            InitializeComponent();
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
            {
                var shape = context.ActiveImage.ActiveLayer.Shapes.Last();
                context.ActiveImage.ActiveLayer.Shapes.Remove(shape);
                context.ActiveImage.Undo.Push(shape);
            }
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
            {
                var shape = context.ActiveImage.Undo.History.Last();
                context.ActiveImage.Undo.History.Remove(shape);
                context.ActiveImage.ActiveLayer.Shapes.Add(shape);
            }
        }
    }
}
