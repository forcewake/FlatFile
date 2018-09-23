using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public class ByteConverter : NumberConverterBase<byte>
    {
        protected override byte ConvertFromString(ReadOnlySpan<char> source, NumberFormatInfo format) =>
            byte.Parse(source, provider: format);

        protected override ReadOnlySpan<char> ConvertToString(byte value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
