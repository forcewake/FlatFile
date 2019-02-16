namespace FluentFiles.Core.Conversion
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Base class for types that convert between numbers and strings.
    /// It is partially based on <see cref="System.ComponentModel.BaseNumberConverter"/> located at 
    /// https://github.com/dotnet/corefx/blob/master/src/System.ComponentModel.TypeConverter/src/System/ComponentModel/BaseNumberConverter.cs.
    /// </summary>
    public abstract class NumberConverterBase<T> : IFieldValueConverter
    {
        internal NumberConverterBase() { }

        /// <summary>
        /// The numeric type to convert to and from.
        /// </summary>
        protected Type TargetType { get; } = typeof(T);

        /// <summary>
        /// Converts a string to an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed value.</returns>
        protected abstract T Parse(in ReadOnlySpan<char> source, NumberFormatInfo format);

        /// <summary>
        /// Converts an instance of <typeparamref name="T"/> to a string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted value.</returns>
        protected abstract string Format(T value, NumberFormatInfo format);

        /// <summary>
        /// Whether a value of a given type can be converted to another type.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a conversion can be performed between the two types.</returns>
        public bool CanConvert(Type from, Type to) =>
            (from == typeof(string) && to == TargetType) ||
            (from == TargetType && to == typeof(string));

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="context">Provides information about a field deserialization operation.</param>
        /// <returns>A deserialized value.</returns>
        public object ConvertFromString(in FieldDeserializationContext context)
        {
            var culture = CultureInfo.CurrentCulture;
            var numberFormat = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
            return Parse(context.Source.Trim(), numberFormat);
        }

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A serialized value.</returns>
        public string ConvertToString(in FieldSerializationContext context)
        {
            var culture = CultureInfo.CurrentCulture;
            var numberFormat = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
            return Format((T)context.Source, numberFormat);
        }
    }
}
