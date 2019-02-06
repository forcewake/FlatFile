namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    /// <summary>
    /// Describes the mapping from a fixed-length file record to a target type.
    /// </summary>
    /// <typeparam name="TTarget">The type that a record maps to.</typeparam>
    public interface IFixedLayout<TTarget> :
        IFixedLengthLayoutDescriptor,
        ILayout<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsBuilder, IFixedLayout<TTarget>>
    {
        /// <summary>
        /// Ignores a fixed width section of a record.
        /// </summary>
        /// <param name="length">The length of the section to ignore.</param>
        IFixedLayout<TTarget> Ignore(int length);
    }
}