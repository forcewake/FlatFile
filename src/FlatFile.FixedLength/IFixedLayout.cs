namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLayout<TTarget> :
        ILayout<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>
    {
    }
}