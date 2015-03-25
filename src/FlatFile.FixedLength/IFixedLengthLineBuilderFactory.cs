namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthLineBuilderFactory :
        ILineBuilderFactory<IFixedLengthLineBuilder, ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>
    {
    }
}