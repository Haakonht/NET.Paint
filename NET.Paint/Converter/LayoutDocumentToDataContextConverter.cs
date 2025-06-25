using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Xceed.Wpf.AvalonDock.Layout;

namespace NET.Paint.Converter
{
    public class LayoutDocumentToDataContextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LayoutDocument document)
            {
                return document.Content;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LayoutDocument document)
            {
                return document;
            }

            return value;
        }
    }
}
