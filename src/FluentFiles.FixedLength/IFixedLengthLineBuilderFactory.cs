namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    /// <summary>
    /// Interface for an object that creates fixed-length line builders.
    /// </summary>
    public interface IFixedLengthLineBuilderFactory :
        ILineBuilderFactory<IFixedLengthLineBuilder, IFixedLengthLayoutDescriptor, IFixedFieldSettingsContainer>
    {
    }
}