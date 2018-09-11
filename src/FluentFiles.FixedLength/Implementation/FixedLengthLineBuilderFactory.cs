namespace FluentFiles.FixedLength.Implementation
{
    using FluentFiles.Core;

    public class FixedLengthLineBuilderFactory : IFixedLengthLineBuilderFactory
    {
        public IFixedLengthLineBuilder GetBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
        {
            return new FixedLengthLineBuilder(descriptor);
        }
    }
}