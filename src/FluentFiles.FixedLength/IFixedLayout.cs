namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    public interface IFixedLayout<TTarget> :
        IFixedLengthLayoutDescriptor,
        ILayout<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsBuilder, IFixedLayout<TTarget>>
    {
    }
}