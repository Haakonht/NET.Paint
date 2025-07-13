using NET.Paint.Drawing.Constant;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace NET.Paint.Converter
{
    public class ToolToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ToolType tool)
            {
                switch (tool)
                {
                    case ToolType.Pointer:
                        return Cursors.Arrow;
                    case ToolType.Pencil:
                        return Cursors.Pen;
                    default:
                        return Cursors.Cross;
                }
            }
            return Cursors.Cross;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
