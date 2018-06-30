namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;

    public class FixedLengthLineBuilderFactory : IFixedLengthLineBuilderFactory
    {
        public IFixedLengthLineBuilder GetBuilder(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
        {
            return new FixedLengthLineBuilder(descriptor);
        }
    }
}