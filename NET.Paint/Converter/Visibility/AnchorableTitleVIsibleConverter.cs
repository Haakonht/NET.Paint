using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;
using NET.Paint.Helper;

namespace NET.Paint.Converter
{
    public class AnchorableTitleVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LayoutAnchorable anchorable)
            {
                if (AnchorableProperties.GetHideTitle(anchorable))
                    return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
                return visibility == Visibility.Visible;
            return false;
        }
    }
}
