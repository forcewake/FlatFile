namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    /// <summary>
    /// Describes the mapping from a fixed-length file record to a target type.
    /// </summary>
    public interface IFixedLengthLayoutDescriptor : ILayoutDescriptor<IFixedFieldSettingsContainer>
    {
    }
}
