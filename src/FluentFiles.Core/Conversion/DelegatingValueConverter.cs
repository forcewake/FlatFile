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

        public bool CanConvert(Type from, Type to) =>
            (from == typeof(string) && to == typeof(TProperty) && ConversionFromString != null) ||
            (from == typeof(TProperty) && to == typeof(string) && ConversionToString != null);

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
