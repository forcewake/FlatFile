namespace FluentFiles.Delimited
{
    using FluentFiles.Core;

    public interface IDelimitedLineBuilderFactory :
        ILineBuilderFactory<IDelimitedLineBuilder, IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
    }
}