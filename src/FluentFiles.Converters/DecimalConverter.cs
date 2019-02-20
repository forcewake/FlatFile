namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between decimal values and strings.
    /// </summary>
    public sealed class DecimalConverter : NumberConverterBase<decimal>
    {
        /// <summary>
        /// Converts a string to a decimal.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed decimal value.</returns>
        protected override decimal Parse(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            decimal.Parse(source, NumberStyles.Float, provider: format);

        /// <summary>
        /// Converts a decimal to a string.
        /// </summary>
        /// <param name="value">The decimal to format.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted decimal value.</returns>
        protected override string Format(decimal value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
