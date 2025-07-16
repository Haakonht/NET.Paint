using NET.Paint.Drawing.Constant;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class CursorConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] is XToolType type)
            {
                if (value[1] is XSelectionMode mode)
                {
                    switch (type)
                    {
                        case XToolType.Selector:
                            switch (mode)
                            {
                                case XSelectionMode.Pointer:
                                    return Cursors.Arrow;
                                case XSelectionMode.Lasso:
                                    return Cursors.Pen;
                                default:
                                    return Cursors.Cross;
                            }
                        case XToolType.Pencil:
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
