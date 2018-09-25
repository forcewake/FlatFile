using FluentFiles.Core.Conversion;
using System;
using System.Reflection;

namespace FluentFiles.Converters
{
    public sealed class StringConverter : IValueConverter
    {
        public bool CanConvert(Type from, Type to) => from == typeof(string) && to == typeof(string);

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => source.Length == 0 ? string.Empty : source.ToString();

        public ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty) => ((string)source).AsSpan();
    }
}
