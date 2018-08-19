namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    public interface IFixedLengthLineBuilderFactory :
        ILineBuilderFactory<IFixedLengthLineBuilder, ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer>
    {
    }
}