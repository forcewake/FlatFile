namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between long values and strings.
    /// </summary>
    public sealed class Int64Converter : NumberConverterBase<long>
    {
        /// <summary>
        /// Converts a string to a long.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed long value.</returns>
        protected override long Parse(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            long.Parse(source, provider: format);

        /// <summary>
        /// Converts a long to a string.
        /// </summary>
        /// <param name="value">The long to serialize.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted long value.</returns>
        protected override string Format(long value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
