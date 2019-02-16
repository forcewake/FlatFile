namespace FluentFiles.Core.Conversion
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Adapts a <see cref="TypeConverter"/> to an <see cref="IFieldValueConverter"/>.
    /// </summary>
    public class TypeConverterAdapter : IFieldValueConverter
    {
        private readonly TypeConverter _converter;

        /// <summary>
        /// Intializes a new instance of <see cref="TypeConverterAdapter"/> with a <see cref="TypeConverter"/>.
        /// </summary>
        /// <param name="converter">The <see cref="TypeConverter"/> to wrap.</param>
        public TypeConverterAdapter(TypeConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Whether a value of a given type can be converted to another type.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a conversion can be performed between the two types.</returns>
        public bool CanConvert(Type from, Type to) => _converter.CanConvertFrom(from) && (OverrideCanConvertTo || _converter.CanConvertTo(to));

        /// <summary>
        /// Converts a string to an object instance using <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="context">Provides information about a field deserialization operation.</param>
        /// <returns>A deserialized value.</returns>
        public object ConvertFromString(in FieldDeserializationContext context) => _converter.ConvertFromString(context.Source.ToString());

        /// <summary>
        /// Converts an object to a string using <see cref="TypeConverter.ConvertToString(object)"/>.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A serialized value.</returns>
        public string ConvertToString(in FieldSerializationContext context) => _converter.ConvertToString(context.Source);

        /// <summary>
        /// Some built-in <see cref="TypeConverter"/>s don't return as expected for <see cref="TypeConverter.CanConvertTo(Type)"/>.
        /// Setting this to true makes sure that call succeeds.
        /// </summary>
        public bool OverrideCanConvertTo { get; set; }
    }
}
