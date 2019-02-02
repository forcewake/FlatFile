using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public sealed class ByteConverter : NumberConverterBase<byte>
    {
        protected override byte ConvertFromString(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            byte.Parse(source, provider: format);

        protected override string ConvertToString(byte value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
