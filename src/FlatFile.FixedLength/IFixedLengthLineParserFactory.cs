namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthLineParserFactory :
        ILineParserFactory<IFixedLengthLineParser, ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>
    {
    }
}