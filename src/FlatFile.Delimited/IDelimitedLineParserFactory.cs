namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLineParserFactory<TEntity> :
        ILineParserFactory<
            TEntity,
            IDelimitedLineParser<TEntity>,
            IDelimitedLayout<TEntity>,
            DelimitedFieldSettings,
            IDelimitedFieldSettingsConstructor>
    {
    }
}