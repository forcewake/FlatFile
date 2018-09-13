using System;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// An implementation of <see cref="IValueConverter"/> that uses delegates for conversion.
    /// </summary>
    class DelegatingValueConverter<TProperty> : IValueConverter
    {
        internal ConvertFromString<TProperty> ConversionFromString { get; set; }

        internal ConvertToString<TProperty> ConversionToString { get; set; }

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

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty)
        {
            return ConversionFromString(source);
        }

        public ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty)
        {
            return ConversionToString((TProperty)source);
        }
    }
}
