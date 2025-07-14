using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<object> collection)
            {
                if (parameter is string str)
                {
                    if (str.StartsWith(">"))
                    {
                        // If parameter starts with '>', check if count is greater than the specified number
                        if (int.TryParse(str.Substring(1), out int count) && collection.Count() > count)
                            return Visibility.Visible;
                    }
                    else if (str.StartsWith("<"))
                    {
                        // If parameter starts with '<', check if count is less than the specified number
                        if (int.TryParse(str.Substring(1), out int count) && collection.Count() < count)
                            return Visibility.Visible;
                    }
                    else if (str.StartsWith("="))
                    {
                        // If parameter starts with '=', check if count is equal to the specified number
                        if (int.TryParse(str.Substring(1), out int count) && collection.Count() == count)
                            return Visibility.Visible;
                    }
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not typically used for this type of comparison converter
            throw new NotImplementedException();
        }
    }
}
