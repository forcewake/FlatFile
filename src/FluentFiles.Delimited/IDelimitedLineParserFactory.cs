namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedLineParserFactory :
        ILineParserFactory<IDelimitedLineParser, IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
    }
}