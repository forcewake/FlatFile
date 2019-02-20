namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between short values and strings.
    /// </summary>
    public sealed class Int16Converter : NumberConverterBase<short>
    {
        /// <summary>
        /// Converts a string to a short.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed short value.</returns>
        protected override short Parse(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            short.Parse(source, provider: format);

        /// <summary>
        /// Converts a short to a string.
        /// </summary>
        /// <param name="value">The short to format.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted short value.</returns>
        protected override string Format(short value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
