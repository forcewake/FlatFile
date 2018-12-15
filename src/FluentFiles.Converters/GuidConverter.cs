using FluentFiles.Core.Conversion;
using System;
using System.Reflection;

namespace FluentFiles.Converters
{
    public sealed class GuidConverter : ConverterBase<Guid>
    {
        protected override Guid ConvertFrom(ReadOnlySpan<char> source, PropertyInfo targetProperty) =>
            Guid.Parse(source.Trim());

        protected override string ConvertTo(Guid source, PropertyInfo sourceProperty) =>
            source.ToString();
    }
}
