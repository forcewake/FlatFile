using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public class Int64Converter : NumberConverterBase<long>
    {
        protected override long ConvertFromString(ReadOnlySpan<char> source, NumberFormatInfo format) =>
            long.Parse(source, provider: format);

        protected override ReadOnlySpan<char> ConvertToString(long value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
