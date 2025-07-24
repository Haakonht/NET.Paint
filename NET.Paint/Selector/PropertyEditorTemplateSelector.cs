using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static NET.Paint.View.Component.Property.Converters.ObjectToPropertyInfoConverter;

namespace NET.Paint.Selector
{
    public class PropertyEditorTemplateSelector : DataTemplateSelector
    {
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
                    return StringTemplate;

                if (propertyType == typeof(XColor))
                    return ColorTemplate;

                if (propertyType == typeof(Point) || propertyType == typeof(Point?))
                    return PointTemplate;
            }

            return StringTemplate; // Default fallback
        }
    }
}
