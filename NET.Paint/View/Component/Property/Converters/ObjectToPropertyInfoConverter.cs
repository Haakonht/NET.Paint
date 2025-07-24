using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace NET.Paint.View.Component.Property.Converters
{
    public class ObjectToPropertyInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new ObservableCollection<PropertyWrapper>();

            var properties = value.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead) // Only include readable properties
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false) // Exclude non-browsable
                .Select(p => new PropertyWrapper(value, p))
                .ToList();

            return new ObservableCollection<PropertyWrapper>(properties);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public class PropertyWrapper : INotifyPropertyChanged
        {
            private readonly object _instance;
            private readonly PropertyInfo _propertyInfo;

            public PropertyWrapper(object instance, PropertyInfo propertyInfo)
            {
                _instance = instance;
                _propertyInfo = propertyInfo;
            }

            public string Name => _propertyInfo.Name;
            public Type PropertyType => _propertyInfo.PropertyType;
            public bool CanWrite => _propertyInfo.CanWrite;
            public bool Browsable => _propertyInfo.GetCustomAttribute<BrowsableAttribute>()?.Browsable ?? true;

            public object Value
            {
                get => _propertyInfo.GetValue(_instance);
                set
                {
                    if (_propertyInfo.CanWrite)
                    {
                        _propertyInfo.SetValue(_instance, value);
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }
    }
}