namespace FlatFile.Core
{
    using FlatFile.Core.Base;

    public interface ILineParserFactory<TEntity, out TParser, in TLayoutDescriptor, TFieldSettings>
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
        where TFieldSettings : IFieldSettings
        where TParser : ILineParser<TEntity>
    {
        TParser GetParser(TLayoutDescriptor descriptor);
    }
}