using FluentFiles.Core.Conversion;
using System;

namespace FluentFiles.Converters
{
    public sealed class CharConverter : ConverterBase<char>
    {
        protected override char ConvertFrom(in FieldDeserializationContext context)
        {
            var trimmed = context.Source;
            if (trimmed.Length > 1)
                trimmed = trimmed.Trim();

            if (trimmed.Length > 0)
                return trimmed[0];

            return char.MinValue;
        }

        protected override string ConvertTo(in FieldSerializationContext<char> context)
        {
            if (context.Source == char.MinValue)
                return string.Empty;

            return new string(context.Source, 1);
        }
    }
}
