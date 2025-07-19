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
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : UserControl
    {
        public Gallery()
        {
            InitializeComponent();
        }

        private void AddBitmap(object sender, RoutedEventArgs e)
        {
            // Implementation for adding bitmap
        }

        private void AddBitmapFromFile(object sender, RoutedEventArgs e)
        {
            // Implementation for file dialog
        }

        private void CreateNewBitmap(object sender, RoutedEventArgs e)
        {
            // Implementation for creating new bitmap
        }

        private void SelectBitmap(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ImageSource bitmap)
            {
                if (XTools.Instance is XTools tools)
                {
                    tools.ActiveBitmap = bitmap;
                }
            }
        }
    }
}
