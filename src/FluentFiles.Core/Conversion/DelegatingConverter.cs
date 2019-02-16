namespace FluentFiles.Core.Conversion
{
    using System;

    /// <summary>
    /// An implementation of <see cref="IFieldValueConverter"/> that uses delegates for conversion.
    /// </summary>
    internal class DelegatingConverter<TProperty> : IFieldValueConverter
    {
        internal ConvertFromString<TProperty> ConversionFromString { get; set; }

        internal ConvertToString<TProperty> ConversionToString { get; set; }

        public bool CanConvert(Type from, Type to) =>
            (from == typeof(string) && to == typeof(TProperty) && ConversionFromString != null) ||
            (from == typeof(TProperty) && to == typeof(string) && ConversionToString != null);

        public object ConvertFromString(in FieldDeserializationContext context) => ConversionFromString(context.Source);

        public string ConvertToString(in FieldSerializationContext context) => ConversionToString((TProperty)context.Source);
    }
}
