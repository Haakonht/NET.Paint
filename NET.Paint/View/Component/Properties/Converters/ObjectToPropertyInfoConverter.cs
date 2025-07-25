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
            private object _currentValue;

            public PropertyWrapper(object instance, PropertyInfo propertyInfo)
            {
                _instance = instance;
                _propertyInfo = propertyInfo;
                _currentValue = _propertyInfo.GetValue(_instance);
                
                // Subscribe to property changes if the value implements INotifyPropertyChanged
                SubscribeToValueChanges(_currentValue);
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
                        var oldValue = _currentValue;
                        
                        // Unsubscribe from old value
                        UnsubscribeFromValueChanges(oldValue);
                        
                        // Convert the value to the target property type
                        var convertedValue = ConvertToTargetType(value, _propertyInfo.PropertyType);
                        
                        _propertyInfo.SetValue(_instance, convertedValue);
                        _currentValue = convertedValue;
                        
                        // Subscribe to new value
                        SubscribeToValueChanges(_currentValue);
                        
                        // Notify the PropertyWrapper Value change
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                        
                        // CRITICAL: Also notify the parent object that this property changed
                        // This ensures the artboard bindings get updated
                        if (_instance is INotifyPropertyChanged instanceWithNotification)
                        {
                            // Use reflection to call the protected OnPropertyChanged method
                            var instanceType = _instance.GetType();
                            var onPropertyChangedMethod = instanceType.GetMethod("OnPropertyChanged", 
                                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public,
                                null,
                                new[] { typeof(string) },
                                null);
                            
                            if (onPropertyChangedMethod != null)
                            {
                                onPropertyChangedMethod.Invoke(_instance, new object[] { _propertyInfo.Name });
                            }
                        }
                    }
                }
            }

            private object ConvertToTargetType(object value, Type targetType)
            {
                if (value is string stringValue)
                    return ConvertFromString(stringValue, targetType);

                return value;
            }

            private object ConvertFromString(string stringValue, Type targetType)
            {
                if (string.IsNullOrEmpty(stringValue))
                {
                    // Return default value for value types, null for reference types
                    return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
                }

                try
                {
                    if (targetType == typeof(bool))
                        return bool.Parse(stringValue);
                    
                    if (targetType == typeof(int))
                        return int.Parse(stringValue, CultureInfo.InvariantCulture);
                    
                    if (targetType == typeof(double))
                        return double.Parse(stringValue, CultureInfo.InvariantCulture);
                    
                    if (targetType == typeof(float))
                        return float.Parse(stringValue, CultureInfo.InvariantCulture);
                    
                    if (targetType == typeof(decimal))
                        return decimal.Parse(stringValue, CultureInfo.InvariantCulture);
                    
                    if (targetType == typeof(DateTime))
                        return DateTime.Parse(stringValue, CultureInfo.InvariantCulture);
                    
                    if (targetType == typeof(string))
                        return stringValue;

                    if (targetType.IsEnum)
                        return Enum.Parse(targetType, stringValue, true);

                    // Try using TypeConverter for complex types
                    var converter = TypeDescriptor.GetConverter(targetType);
                    if (converter != null && converter.CanConvertFrom(typeof(string)))
                    {
                        return converter.ConvertFromString(null, CultureInfo.InvariantCulture, stringValue);
                    }

                    return System.Convert.ChangeType(stringValue, targetType, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to convert '{stringValue}' to {targetType.Name}: {ex.Message}");
                    
                    return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
                }
            }

            private void SubscribeToValueChanges(object value)
            {
                if (value is INotifyPropertyChanged notifiable)
                {
                    notifiable.PropertyChanged += OnValuePropertyChanged;
                }
            }

            private void UnsubscribeFromValueChanges(object value)
            {
                if (value is INotifyPropertyChanged notifiable)
                {
                    notifiable.PropertyChanged -= OnValuePropertyChanged;
                }
            }

            private void OnValuePropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                // Forward the property change notification for the Value property
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
                
                // CRITICAL: When XColor internal properties change (like XSolidColor.Color),
                // we need to notify the parent shape that its property (like Stroke, Fill) changed
                if (_instance is INotifyPropertyChanged instanceWithNotification)
                {
                    var instanceType = _instance.GetType();
                    var onPropertyChangedMethod = instanceType.GetMethod("OnPropertyChanged", 
                        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public,
                        null,
                        new[] { typeof(string) },
                        null);
                    
                    if (onPropertyChangedMethod != null)
                    {
                        onPropertyChangedMethod.Invoke(_instance, new object[] { _propertyInfo.Name });
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }
    }
}