using NET.Paint.Drawing.Model.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace NET.Paint.View.Component.Dialog
{
    /// <summary>
    /// Interaction logic for FileDialog.xaml
    /// </summary>
    public partial class FileDialog : Window
    {
        public FileDialog()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var viewModel = DataContext as XFileDialog;

            if (viewModel != null)
            {
                viewModel.SelectedDirectory = e.NewValue as string;
            }
        }

        private void Close(object sender, RoutedEventArgs e) => Close();

        private void OnClose(object sender, CancelEventArgs e) => Close();
    }
}
