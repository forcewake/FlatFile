using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public sealed class Int32Converter : NumberConverterBase<int>
    {
        protected override int ConvertFromString(in ReadOnlySpan<char> source, NumberFormatInfo format) => 
            int.Parse(source, provider: format);

        protected override string ConvertToString(int value, NumberFormatInfo format) => 
            value.ToString("G", format);
    }
}
