namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLineBuilderFactory :
        ILineBuilderFactory<IDelimitedLineBuilder, IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer>
    {
    }
}