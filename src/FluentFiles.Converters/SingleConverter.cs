using FluentFiles.Core.Conversion;
using System;
using System.Globalization;

namespace FluentFiles.Converters
{
    public sealed class SingleConverter : NumberConverterBase<float>
    {
        protected override float ConvertFromString(in ReadOnlySpan<char> source, NumberFormatInfo format) =>
            float.Parse(source, provider: format);

        protected override string ConvertToString(float value, NumberFormatInfo format) =>
            value.ToString("R", format);
    }
}
