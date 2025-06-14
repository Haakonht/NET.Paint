using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.Converter
{
    public class PointsToPointCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var points = value as ObservableCollection<Point>;
            if (points == null) return null;

            var pointCollection = new PointCollection(points);
            return pointCollection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pointCollection = value as PointCollection;
            if (pointCollection == null) return null;

            return new ObservableCollection<Point>(pointCollection);
        }
    }
}
