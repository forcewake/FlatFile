namespace FluentFiles.Converters
{
    using FluentFiles.Core.Conversion;
    using System;
    using System.Globalization;

    /// <summary>
    /// Converts between byte values and strings.
    /// </summary>
    public sealed class ByteConverter : NumberConverterBase<byte>
    {
        /// <summary>
        /// Converts a string to a byte.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="format">The number format to use for parsing.</param>
        /// <returns>A parsed byte value.</returns>
        protected override byte Parse(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            byte.Parse(source, provider: format);

        /// <summary>
        /// Converts a byte to a string.
        /// </summary>
        /// <param name="value">The byte to serialize.</param>
        /// <param name="format">The number format to use for formatting.</param>
        /// <returns>A formatted byte.</returns>
        protected override string Format(byte value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
