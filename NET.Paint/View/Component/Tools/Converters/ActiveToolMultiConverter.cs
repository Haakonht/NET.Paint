using System.Globalization;
using System.Windows.Data;
using NET.Paint.Drawing.Constant;

namespace NET.Paint.View.Component.Tools.Converters
{
    public class ActiveToolMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] is not XToolType activeTool)
                return false;

            XToolType? thisTool = values[1] as XToolType?;
            if (thisTool == null && values[1] != null && Enum.TryParse(values[1].ToString(), out XToolType parsedTool))
                thisTool = parsedTool;

            return activeTool.Equals(thisTool);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                // The second targetType is the ToolType for this RadioButton
                // Set ActiveTool to this tool type
                return new object[] { parameter, Binding.DoNothing };
            }
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }
    }
}