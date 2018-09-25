using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public sealed class DoubleConverter : NumberConverterBase<double>
    {
        protected override double ConvertFromString(ReadOnlySpan<char> source, NumberFormatInfo format) =>
            double.Parse(source, provider: format);

        protected override ReadOnlySpan<char> ConvertToString(double value, NumberFormatInfo format) =>
            value.ToString("R", format);
    }
}
