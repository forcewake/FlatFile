namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    public interface IFixedLayout<TTarget> :
        ILayout<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsBuilder, IFixedLayout<TTarget>>
    {
    }
}