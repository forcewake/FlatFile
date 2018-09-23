using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public class Int32Converter : NumberConverterBase<int>
    {
        protected override int ConvertFromString(ReadOnlySpan<char> source, NumberFormatInfo format) => 
            int.Parse(source, provider: format);

        protected override ReadOnlySpan<char> ConvertToString(int value, NumberFormatInfo format) => 
            value.ToString("G", format);
    }
}
