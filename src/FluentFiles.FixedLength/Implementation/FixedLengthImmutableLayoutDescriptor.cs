using FluentFiles.Core.Base;

namespace FluentFiles.FixedLength.Implementation
{
    public sealed class FixedLengthImmutableLayoutDescriptor : ImmutableLayoutDescriptor<IFixedFieldSettingsContainer>, IFixedLengthLayoutDescriptor
    {
        public FixedLengthImmutableLayoutDescriptor(IFixedLengthLayoutDescriptor existing)
            : base(existing)
        {
        }
    }
}
