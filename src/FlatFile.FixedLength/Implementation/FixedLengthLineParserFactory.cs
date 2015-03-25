namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;

    public class FixedLengthLineParserFactory : IFixedLengthLineParserFactory
    {
        public IFixedLengthLineParser GetParser(ILayoutDescriptor<IFixedFieldSettingsContainer> descriptor)
        {
            return new FixedLengthLineParser(descriptor);
        }
    }
}