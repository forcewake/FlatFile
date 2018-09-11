using FluentFiles.Core.Base;

namespace FluentFiles.Delimited.Implementation
{
    public sealed class DelimitedImmutableLayoutDescriptor : ImmutableLayoutDescriptor<IDelimitedFieldSettingsContainer>, IDelimitedLayoutDescriptor
    {
        public DelimitedImmutableLayoutDescriptor(IDelimitedLayoutDescriptor existing)
            : base(existing)
        {
            Delimiter = existing.Delimiter;
            Quotes = existing.Quotes;
        }

        public string Delimiter { get; }

        public string Quotes { get; }
    }
}
