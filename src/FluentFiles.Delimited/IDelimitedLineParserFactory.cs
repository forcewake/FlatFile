namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    /// <summary>
    /// Interface for an object that can create parsers for delimited files.
    /// </summary>
    public interface IDelimitedLineParserFactory :
        ILineParserFactory<IDelimitedLineParser, IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
    }
}