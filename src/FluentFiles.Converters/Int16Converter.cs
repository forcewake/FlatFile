using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public sealed class Int16Converter : NumberConverterBase<short>
    {
        protected override short ConvertFromString(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            short.Parse(source, provider: format);

        protected override string ConvertToString(short value, NumberFormatInfo format) =>
            value.ToString("G", format);
    }
}
