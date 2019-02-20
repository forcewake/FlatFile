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
        /// Whether a value of a given type can be converted to a string.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanFormat(Type from) => _converter.CanConvertTo(typeof(string));

        /// <summary>
        /// Whether a value of a given type can be converted from a string.
        /// </summary>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanParse(Type to) => _converter.CanConvertFrom(typeof(string));

        /// <summary>
        /// Converts a string to an object instance using <see cref="TypeConverter.ConvertFromString(string)"/>.
        /// </summary>
        /// <param name="context">Provides information about a field parsing operation.</param>
        /// <returns>A parsed value.</returns>
        public object Parse(in FieldParsingContext context) => _converter.ConvertFromString(context.Source.ToString());

        /// <summary>
        /// Converts an object to a string using <see cref="TypeConverter.ConvertToString(object)"/>.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A formatted value.</returns>
        public string Format(in FieldFormattingContext context) => _converter.ConvertToString(context.Source);
    }
}
