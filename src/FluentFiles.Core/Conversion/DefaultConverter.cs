using FluentFiles.Core.Extensions;
using System;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    class DefaultConverter : IFieldValueConverter
    {
        public static IFieldValueConverter Instance = new DefaultConverter();

        public bool CanConvert(Type from, Type to) => from == typeof(string);

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => targetProperty.PropertyType.GetDefaultValue();

        public string ConvertToString(object source, PropertyInfo sourceProperty) => source.ToString();
    }
}
