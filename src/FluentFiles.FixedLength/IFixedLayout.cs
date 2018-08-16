namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLayout<TTarget> :
        ILayout<TTarget, IFixedFieldSettingsContainer, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>
    {
    }
}