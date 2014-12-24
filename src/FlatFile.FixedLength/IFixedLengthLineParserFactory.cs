namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthLineParserFactory<TEntity> :
        ILineParserFactory<
            TEntity,
            IFixedLengthLineParser<TEntity>,
            IFixedLayout<TEntity>,
            FixedFieldSettings,
            IFixedFieldSettingsConstructor>
    {
    }
}