namespace FlatFile.Core
{
    using FlatFile.Core.Base;

    public interface ILineBuilderFactory<TEntity, out TBuilder, in TLayout, TFieldSettings, TConstructor>
        where TLayout : ILayout<TEntity, TFieldSettings, TConstructor, TLayout>
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
        where TBuilder : ILineBulder<TEntity>
    {
        TBuilder GetBuilder(TLayout layout);
    }
}