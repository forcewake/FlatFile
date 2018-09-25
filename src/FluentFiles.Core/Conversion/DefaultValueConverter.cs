using FluentFiles.Core.Extensions;
using System;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    class DefaultValueConverter : IValueConverter
    {
        public static IValueConverter Instance = new DefaultValueConverter();

        public bool CanConvert(Type from, Type to) => from == typeof(string);

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => targetProperty.PropertyType.GetDefaultValue();

        public ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty) => default;
    }
}
