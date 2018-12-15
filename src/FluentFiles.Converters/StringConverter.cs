using FluentFiles.Core.Conversion;
using System;
using System.Reflection;

namespace FluentFiles.Converters
{
    public sealed class StringConverter : IFieldValueConverter
    {
        public bool CanConvert(Type from, Type to) => from == typeof(string) && to == typeof(string);

        public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => source.Length == 0 ? string.Empty : source.ToString();

        public string ConvertToString(object source, PropertyInfo sourceProperty) => ((string)source);
    }
}
