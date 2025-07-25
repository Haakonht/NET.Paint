using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Service;
using NET.Paint.View.Component.Drawing.Controls;
using NET.Paint.View.Component.Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using XSelectionMode = NET.Paint.Drawing.Constant.XSelectionMode;

namespace NET.Paint.View.Component.Drawing
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void OpenToolContextQuickSelect(object sender, MouseButtonEventArgs e)
        {
            if (Tag is XService service && service.Tools.ActiveTool == XToolType.Selector)
                ToolContextMenu.IsOpen = true;
            else
                ToolContextQuickSelect.IsOpen = true;
            e.Handled = true;
        }

        private void CloseToolContextQuickSelect(object sender, MouseEventArgs e)
        {
            ToolContextQuickSelect.IsOpen = false;
            e.Handled = true;
        }

        private void CloseToolContextQuickSelect(object sender, MouseButtonEventArgs e)
        {
            ToolContextQuickSelect.IsOpen = false;
            e.Handled = true;
        }

        private void OnOpenToolContextRequested(object sender, RoutedEventArgs e)
        {
            ToolContextMenu.IsOpen = true;
            ToolContextQuickSelect.IsOpen = false;
        }

        private void OnOpenSpecificToolContext(object sender, RoutedEventArgs e)
        {
            if (e is ToolContextEventArgs args && int.TryParse(args.TabIndex?.ToString(), out int index))
            {
                ToolContext.ContextIndex = index;
            }

            ToolContextMenu.IsOpen = true;
            ToolContextQuickSelect.IsOpen = false;
        }

        private void OnCloseToolContextRequested(object sender, RoutedEventArgs e)
        {
            ToolContextMenu.IsOpen = false;
        }

        private void CloseToolContext(object sender, MouseEventArgs e)
        {
            ToolContextMenu.IsOpen = false;
            e.Handled = true;
        }
    }
}
