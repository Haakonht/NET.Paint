using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class TextDecorationsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var decorations = new TextDecorationCollection();

            if (values[0] is bool isUnderlined && isUnderlined)
            {
                decorations.Add(TextDecorations.Underline[0]);
            }

            if (values[1] is bool isStrikethrough && isStrikethrough)
            {
                decorations.Add(TextDecorations.Strikethrough[0]);
            }

            return decorations;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
