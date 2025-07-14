using NET.Paint.Drawing.Constant;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace NET.Paint.Converter
{
    public class ToolToCursorConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] is ToolType type)
            {
                if (value[1] is SelectionMode mode)
                {
                    switch (type)
                    {
                        case ToolType.Selector:
                            switch (mode)
                            {
                                case SelectionMode.Pointer:
                                    return Cursors.Arrow;
                                case SelectionMode.Lasso:
                                    return Cursors.Pen;
                                default:
                                    return Cursors.Cross;
                            }
                        case ToolType.Pencil:
                            return Cursors.Pen;
                        default:
                            return Cursors.Cross;
                    }
                }
                
            }

            return Cursors.Cross;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
