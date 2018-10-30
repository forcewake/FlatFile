using FluentFiles.Core.Conversion;
using System;
using System.Reflection;

namespace FluentFiles.Converters
{
    public sealed class CharConverter : ValueConverterBase<char>
    {
        protected override char ConvertFrom(ReadOnlySpan<char> source, PropertyInfo targetProperty)
        {
            if (source.Length > 1)
                source = source.Trim();

            if (source.Length > 0)
                return source[0];

            return char.MinValue;
        }

        protected override string ConvertTo(char source, PropertyInfo sourceProperty)
        {
            if (source == char.MinValue)
                return string.Empty;

            return new string(source, 1);
        }
    }
}
