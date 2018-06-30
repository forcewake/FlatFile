namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLineParserFactory :
        ILineParserFactory<IDelimitedLineParser, IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
    }
}