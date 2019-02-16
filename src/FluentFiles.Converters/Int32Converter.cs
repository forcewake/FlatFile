namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between integer values and strings.
    /// </summary>
    public sealed class Int32Converter : NumberConverterBase<int>
    {
        /// <summary>
        /// Converts a string to an integer.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed integer value.</returns>
        protected override int Parse(in ReadOnlySpan<char> source, NumberFormatInfo format) => 
            int.Parse(source, provider: format);

        /// <summary>
        /// Converts an integer to a string.
        /// </summary>
        /// <param name="value">The integer to serialize.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted integer value.</returns>
        protected override string Format(int value, NumberFormatInfo format) => 
            value.ToString("G", format);
    }
}
