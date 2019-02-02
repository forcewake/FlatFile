using FluentFiles.Core.Conversion;
using System;

namespace FluentFiles.Converters
{
    public sealed class GuidConverter : ConverterBase<Guid>
    {
        protected override Guid ConvertFrom(in FieldDeserializationContext context) =>
            Guid.Parse(context.Source.Trim());

        protected override string ConvertTo(in FieldSerializationContext<Guid> context) =>
            context.Source.ToString();
    }
}
