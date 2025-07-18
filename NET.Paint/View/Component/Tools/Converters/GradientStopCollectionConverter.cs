using NET.Paint.Drawing.Model.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.View.Component.Tools.Converters
{
    public class GradientStopCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gradientStops = value as ObservableCollection<XGradientStop>;
            if (gradientStops == null) return null;

            var gradientStopCollection = new GradientStopCollection(gradientStops.Select(x => new GradientStop(x.Color, x.Offset)));
            return gradientStopCollection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gradientStopCollection = value as GradientStopCollection;
            if (gradientStopCollection == null) return null;

            return new ObservableCollection<XGradientStop>(gradientStopCollection.Select(x => new XGradientStop{ Color = x.Color, Offset = x.Offset}));
        }
    }
}
