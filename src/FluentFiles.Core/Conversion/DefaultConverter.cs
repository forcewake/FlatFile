using FluentFiles.Core.Extensions;
using System;

namespace FluentFiles.Core.Conversion
{
    class DefaultConverter : IFieldValueConverter
    {
        public static IFieldValueConverter Instance = new DefaultConverter();

        public bool CanConvert(Type from, Type to) => from == typeof(string);

        public object ConvertFromString(in FieldDeserializationContext context) => context.TargetProperty.PropertyType.GetDefaultValue();

        public string ConvertToString(in FieldSerializationContext context) => context.Source.ToString();
    }
}
