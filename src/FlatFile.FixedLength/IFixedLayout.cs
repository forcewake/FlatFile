namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLayout<TTarget, out TLayout> :
        ILayout<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, TLayout>
        where TLayout : IFixedLayout<TTarget, TLayout>
    {
    }

    public interface IFixedLayout<TTarget> :
        IFixedLayout<TTarget, IFixedLayout<TTarget>>
    {
    }
}