namespace FluentFiles.Core.Conversion
{
    using System;

#pragma warning disable CS0618 // Type or member is obsolete
    /// <summary>
    /// Adapts an <see cref="ITypeConverter"/> to an <see cref="IFieldValueConverter"/>.
    /// </summary>
    public class FieldValueConverterAdapter : IFieldValueConverter
    {
        private readonly ITypeConverter _converter;

        /// <summary>
        /// Intializes a new instance of <see cref="FieldValueConverterAdapter"/> with an <see cref="ITypeConverter"/>.
        /// </summary>
        /// <param name="converter">The <see cref="ITypeConverter"/> to wrap.</param>
        public FieldValueConverterAdapter(ITypeConverter converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Whether a value of a given type can be converted to another type.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a conversion can be performed between the two types.</returns>
        public bool CanConvert(Type from, Type to) => _converter.CanConvertFrom(from) && _converter.CanConvertTo(to);

        /// <summary>
        /// Converts a string to an object instance using <see cref="ITypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="context">Provides information about a field deserialization operation.</param>
        /// <returns>A deserialized value.</returns>
        public object ConvertFromString(in FieldDeserializationContext context) => _converter.ConvertFromString(context.Source.ToString());

        /// <summary>
        /// Converts an object to a string using <see cref="ITypeConverter.ConvertToString(object)"/>.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A serialized value.</returns>
        public string ConvertToString(in FieldSerializationContext context) => _converter.ConvertToString(context.Source);
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
