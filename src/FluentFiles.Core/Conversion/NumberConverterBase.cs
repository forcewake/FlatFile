namespace FluentFiles.Core.Conversion
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Base class for types that convert between numbers and strings.
    /// It is partially based on <see cref="System.ComponentModel.BaseNumberConverter"/> located at 
    /// https://github.com/dotnet/corefx/blob/master/src/System.ComponentModel.TypeConverter/src/System/ComponentModel/BaseNumberConverter.cs.
    /// </summary>
    public abstract class NumberConverterBase<T> : IFieldValueConverter where T : struct, IEquatable<T>, IComparable<T>
    {
        internal NumberConverterBase() { }

        /// <summary>
        /// The numeric type to convert to and from.
        /// </summary>
        protected Type TargetType { get; } = typeof(T);

        /// <summary>
        /// Converts a string to an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed value.</returns>
        protected abstract T Parse(in ReadOnlySpan<char> source, NumberFormatInfo format);

        /// <summary>
        /// Converts an instance of <typeparamref name="T"/> to a string.
        /// </summary>
        /// <param name="value">The object to format.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted value.</returns>
        protected abstract string Format(T value, NumberFormatInfo format);

        /// <summary>
        /// Whether a value of a given type can be converted to a string.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanFormat(Type from) => from == TargetType;

        /// <summary>
        /// Whether a value of a given type can be converted from a string.
        /// </summary>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanParse(Type to) => to == TargetType;

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="context">Provides information about a field parsing operation.</param>
        /// <returns>A parsed value.</returns>
        public object Parse(in FieldParsingContext context)
        {
            var culture = CultureInfo.CurrentCulture;
            var numberFormat = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
            return Parse(context.Source.Trim(), numberFormat);
        }

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A formatted value.</returns>
        public string Format(in FieldFormattingContext context)
        {
            var culture = CultureInfo.CurrentCulture;
            var numberFormat = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
            return Format((T)context.Source, numberFormat);
        }
    }
}
