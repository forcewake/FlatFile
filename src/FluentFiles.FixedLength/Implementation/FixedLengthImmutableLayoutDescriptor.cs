using FluentFiles.Core;
using FluentFiles.Core.Base;

namespace FluentFiles.FixedLength.Implementation
{
    public sealed class FixedLengthImmutableLayoutDescriptor : ImmutableLayoutDescriptor<IFixedFieldSettingsContainer>
    {
        public FixedLengthImmutableLayoutDescriptor(ILayoutDescriptor<IFixedFieldSettingsContainer> existing)
            : base(existing)
        {
        }
    }
}
