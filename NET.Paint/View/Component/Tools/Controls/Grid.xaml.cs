using NET.Paint.Drawing.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NET.Paint.View.Component.Tools.Controls
{
    /// <summary>
    /// Interaction logic for Grid.xaml
    /// </summary>
    public partial class Grid : UserControl
    {
        public Grid()
        {
            InitializeComponent();

            // Listen for DataContext changes to hook ViewModel property changes
            this.DataContextChanged += Grid_DataContextChanged;
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is INotifyPropertyChanged oldVm)
                oldVm.PropertyChanged -= Vm_PropertyChanged;

            if (e.NewValue is INotifyPropertyChanged newVm)
            {
                newVm.PropertyChanged += Vm_PropertyChanged;
                UpdateGridBackground();
            }
        }

        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ConfigurationViewModel.GridWidth) || e.PropertyName == nameof(ConfigurationViewModel.GridHeight) || e.PropertyName == nameof(ConfigurationViewModel.GridColor))
            {
                Dispatcher.Invoke(UpdateGridBackground);
            }
        }

        private void UpdateGridBackground()
        {
            if (DataContext is not ConfigurationViewModel vm) return;

            double cellWidth = vm.GridWidth;
            double cellHeight = vm.GridHeight;

            if (cellWidth <= 0 || cellHeight <= 0) return;

            var canvas = new Canvas()
            {
                Width = cellWidth,
                Height = cellHeight,
            };

            var rect = new Rectangle()
            {
                Width = cellWidth,
                Height = cellHeight,
                Stroke = new SolidColorBrush(vm.GridColor),
                Fill = Brushes.White,
                StrokeThickness = 1,
            };

            canvas.Children.Add(rect);

            var visualBrush = new VisualBrush(canvas)
            {
                TileMode = TileMode.Tile,
                Viewport = new Rect(0, 0, cellWidth, cellHeight),
                ViewportUnits = BrushMappingMode.Absolute,
                Viewbox = new Rect(0, 0, cellWidth, cellHeight),
                ViewboxUnits = BrushMappingMode.Absolute,
            };

            MyBorder.Background = visualBrush;
        }
    }
}
