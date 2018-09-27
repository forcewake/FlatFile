namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    public interface IFixedLayout<TTarget> :
        ILayout<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsBuilder, IFixedLayout<TTarget>>
    {
        /// <summary>
        /// Ignores a fixed width section of a record.
        /// </summary>
        /// <param name="length">The length of the section to ignore.</param>
        IFixedLayout<TTarget> Ignore(int length);
    }
}