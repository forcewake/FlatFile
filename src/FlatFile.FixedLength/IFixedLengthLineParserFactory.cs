namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthLineParserFactory<TEntity> :
        ILineParserFactory<
            TEntity,
            IFixedLengthLineParser<TEntity>,
            ILayoutDescriptor<IFixedFieldSettingsContainer>,
            IFixedFieldSettingsContainer>
    {
    }
}