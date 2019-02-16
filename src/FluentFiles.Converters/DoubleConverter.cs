namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between double values and strings.
    /// </summary>
    public sealed class DoubleConverter : NumberConverterBase<double>
    {
        /// <summary>
        /// Converts a string to a double.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed double value.</returns>
        protected override double Parse(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            double.Parse(source, provider: format);

        /// <summary>
        /// Converts a double to a string.
        /// </summary>
        /// <param name="value">The double to serialize.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted double value.</returns>
        protected override string Format(double value, NumberFormatInfo format) =>
            value.ToString("R", format);
    }
}
