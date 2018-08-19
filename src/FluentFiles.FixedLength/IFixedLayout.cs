namespace FluentFiles.FixedLength
{
    using FluentFiles.Core;

    public interface IFixedLayout<TTarget> :
        ILayout<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>
    {
    }
}