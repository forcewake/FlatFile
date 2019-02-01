using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public sealed class Int64Converter : NumberConverterBase<long>
    {
        protected override long ConvertFromString(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            long.Parse(source, provider: format);

        protected override string ConvertToString(long value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
