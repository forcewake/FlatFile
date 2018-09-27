using FluentFiles.Core.Conversion;
using System;
using System.Reflection;

namespace FluentFiles.Converters
{
    public sealed class GuidConverter : ValueConverterBase<Guid>
    {
        protected override Guid ConvertFrom(ReadOnlySpan<char> source, PropertyInfo targetProperty) =>
            Guid.Parse(source.Trim());

        protected override ReadOnlySpan<char> ConvertTo(Guid source, PropertyInfo sourceProperty) =>
            source.ToString();
    }
}
