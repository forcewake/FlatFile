namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between single values and strings.
    /// </summary>
    public sealed class SingleConverter : NumberConverterBase<float>
    {
        /// <summary>
        /// Converts a string to a single.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed single value.</returns>
        protected override float Parse(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            float.Parse(source, provider: format);

        /// <summary>
        /// Converts a single to a string.
        /// </summary>
        /// <param name="value">The single to serialize.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted single value.</returns>
        protected override string Format(float value, NumberFormatInfo format) =>
            value.ToString("R", format);
    }
}
