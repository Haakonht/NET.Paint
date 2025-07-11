﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace NET.Paint.Converter
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        // Convert bool to Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = false;

            if (value is bool)
                b = (bool)value;

            return b ? Visibility.Visible : Visibility.Collapsed;
        }

        // Convert back Visibility to bool
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
                return visibility == Visibility.Visible;

            return false;
        }
    }
}
