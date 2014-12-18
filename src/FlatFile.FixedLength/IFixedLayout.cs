namespace FlatFile.FixedLength
{
    using FlatFile.Core;
    using FlatFile.FixedLength.Implementation;

    public interface IFixedLayout<TTarget> :
        ILayout<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>
    {
    }
}