namespace FlatFile.FixedLength
{
    using FlatFile.Core;
    using FlatFile.FixedLength.Implementation;

    public interface IFixedLayout<TTarget, out TLayout> :
        ILayout<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, TLayout>
        where TLayout : IFixedLayout<TTarget, TLayout>
    {
    }

    public interface IFixedLayout<TTarget> :
      ILayout<TTarget, FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLayout<TTarget>>
    {
    }
}