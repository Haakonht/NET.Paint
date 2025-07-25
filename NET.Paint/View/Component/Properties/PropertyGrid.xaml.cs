using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static NET.Paint.View.Component.Property.Converters.ObjectToPropertyInfoConverter;

namespace NET.Paint.View.Component.Properties
{
    /// <summary>
    /// Interaction logic for PropertyGrid.xaml
    /// </summary>
    public partial class PropertyGrid : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty SelectedObjectProperty =
            DependencyProperty.Register("SelectedObject", typeof(XObject), typeof(PropertyGrid), new PropertyMetadata(new XText
            {
                Points = new ObservableCollection<Point> { new Point(200, 360) },
                Text = "Sample Text",
                FontFamily = "Arial",
                FontSize = 16,
                IsBold = true,
                TextColor = new XSolidColor { Color = Colors.DarkBlue }
            }));

        public object SelectedObject
        {
            get { return GetValue(SelectedObjectProperty); }
            set 
            { 
                SetValue(SelectedObjectProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Properties)));
            }
        }

        public static readonly DependencyProperty ShowHeaderProperty =
            DependencyProperty.Register("ShowHeader", typeof(bool), typeof(PropertyGrid), new PropertyMetadata(true));

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool ShowHeader
        {
            get { return (bool)GetValue(ShowHeaderProperty); }
            set { SetValue(ShowHeaderProperty, value); }
        }

        public ObservableCollection<PropertyInfo> Properties { get => new ObservableCollection<PropertyInfo>(DataContext.GetType().GetProperties().ToList()); }

        public PropertyGrid()
        {
            InitializeComponent();
        }
    }
}
