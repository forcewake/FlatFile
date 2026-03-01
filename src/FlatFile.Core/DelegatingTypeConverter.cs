using System;
using System.Reflection;

namespace FlatFile.Core
{
    /// <summary>
    /// An implementation of <see cref="ITypeConverter"/> that uses delegates for conversion.
    /// </summary>
    class DelegatingTypeConverter<TProperty> : ITypeConverter
    {
        internal Func<string, TProperty> ConversionFromString { get; set; }

        internal Func<TProperty, string> ConversionToString { get; set; }

        public bool CanConvertFrom(Type type)
        {
            return (type == typeof(string) && ConversionFromString != null) ||
                   (type == typeof(TProperty) && ConversionToString != null);
        }

        public bool CanConvertTo(Type type)
        {
            return (type == typeof(string) && ConversionToString != null) ||
                   (type == typeof(TProperty) && ConversionFromString != null);
        }

        public object ConvertFromString(string source, PropertyInfo targetProperty)
        {
            return ConversionFromString(source);
        }

        public string ConvertToString(object source, PropertyInfo sourceProperty)
        {
            return ConversionToString((TProperty)source);
        }
    }
}
