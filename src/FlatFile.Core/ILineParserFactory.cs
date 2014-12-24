namespace FlatFile.Core
{
    using FlatFile.Core.Base;

    public interface ILineParserFactory<TEntity, out TParser, in TLayout, TFieldSettings, TConstructor>
        where TLayout : ILayout<TEntity, TFieldSettings, TConstructor, TLayout>
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
        where TParser : ILineParser<TEntity>
    {
        TParser GetParser(TLayout layout);
    }
}