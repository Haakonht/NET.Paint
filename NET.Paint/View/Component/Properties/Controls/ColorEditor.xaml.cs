using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using NET.Paint.Drawing.Model.Utility;
using System.Windows.Media;

namespace NET.Paint.View.Component.Properties.Controls
{
    /// <summary>
    /// Interaction logic for ColorEditor.xaml
    /// </summary>
    public partial class ColorEditor : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty XColorValueProperty =
            DependencyProperty.Register("XColorValue", typeof(XColor), typeof(ColorEditor),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnXColorValueChanged));

        public XColor XColorValue
        {
            get { return (XColor)GetValue(XColorValueProperty); }
            set { SetValue(XColorValueProperty, value); }
        }

        private static void OnXColorValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorEditor editor)
            {
                editor.PropertyChanged?.Invoke(editor, new PropertyChangedEventArgs(nameof(XColorValue)));
            }
        }

        public ColorEditor()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
