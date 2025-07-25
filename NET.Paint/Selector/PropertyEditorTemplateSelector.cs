using NET.Paint.Drawing.Model.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static NET.Paint.View.Component.Properties.Converters.ObjectToPropertyInfoConverter;

namespace NET.Paint.Selector
{
    public class PropertyEditorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FontTemplate { get; set; }
        public DataTemplate StrokeStyleTemplate { get; set; }
        public DataTemplate? NumberTemplate { get; set; }
        public DataTemplate? StringTemplate { get; set; }
        public DataTemplate? BooleanTemplate { get; set; }
        public DataTemplate? ColorTemplate { get; set; }
        public DataTemplate? PointTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PropertyWrapper wrapper)
            {
                var propertyType = wrapper.PropertyType;

                if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                    return BooleanTemplate;

                if (propertyType == typeof(double) || propertyType == typeof(double?))
                    return NumberTemplate;

                if (propertyType == typeof(int) || propertyType == typeof(int?))
                    return NumberTemplate;

                if (propertyType == typeof(string))
                    if (wrapper.Name == "FontFamily" || wrapper.Name == "Font")
                        return FontTemplate;
                    else
                        return StringTemplate;

                if (propertyType == typeof(XColor))
                    return ColorTemplate;

                if (propertyType == typeof(Point) || propertyType == typeof(Point?))
                    return PointTemplate;

                if (propertyType == typeof(DoubleCollection))
                    return StrokeStyleTemplate;
            }

            return StringTemplate; // Default fallback
        }
    }
}
