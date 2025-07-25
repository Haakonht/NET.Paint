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
            if (value == null) return new ObservableCollection<PropertyGroupWrapper>();

            var properties = value.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.Name != "Id") // Exclude Id property
                .Where(p => p.CanRead) // Only include readable properties
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false) // Exclude non-browsable
                .Select(p => new PropertyWrapper(value, p))
                .ToList();

            // Group properties by category
            var groups = properties
                .GroupBy(p => p.Category)
                .OrderBy(g => g.Key)
                .Select(g => new PropertyGroupWrapper(g.Key, g.OrderBy(p => p.DisplayName).ToList()))
                .ToList();

            return new ObservableCollection<PropertyGroupWrapper>(groups);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public class PropertyGroupWrapper
        {
            public string CategoryName { get; }
            public ObservableCollection<PropertyWrapper> Properties { get; }

            public PropertyGroupWrapper(string categoryName, IEnumerable<PropertyWrapper> properties)
            {
                CategoryName = categoryName;
                Properties = new ObservableCollection<PropertyWrapper>(properties);
            }
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
            public string DisplayName => _propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? _propertyInfo.Name;
            public string Category => _propertyInfo.GetCustomAttribute<CategoryAttribute>()?.Category ?? "Misc";
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