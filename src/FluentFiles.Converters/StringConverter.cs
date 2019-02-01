using FluentFiles.Core.Conversion;
using System;

namespace FluentFiles.Converters
{
    public sealed class StringConverter : IFieldValueConverter
    {
        public bool CanConvert(Type from, Type to) => from == typeof(string) && to == typeof(string);

        public object ConvertFromString(in FieldDeserializationContext context) => context.Source.Length == 0 ? string.Empty : context.Source.ToString();

        public string ConvertToString(in FieldSerializationContext context) => (string)context.Source;
    }
}
