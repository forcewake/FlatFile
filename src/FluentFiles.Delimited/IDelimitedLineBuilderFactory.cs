namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    /// <summary>
    /// Interface for an object that creates delimited line builders.
    /// </summary>
    public interface IDelimitedLineBuilderFactory :
        ILineBuilderFactory<IDelimitedLineBuilder, IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
    }
}