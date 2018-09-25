using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public sealed class Int16Converter : NumberConverterBase<short>
    {
        protected override short ConvertFromString(ReadOnlySpan<char> source, NumberFormatInfo format) =>
            short.Parse(source, provider: format);

        protected override ReadOnlySpan<char> ConvertToString(short value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
