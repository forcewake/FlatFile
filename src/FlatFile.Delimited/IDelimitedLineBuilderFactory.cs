namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedLineBuilderFactory<TEntity> :
        ILineBuilderFactory<
            TEntity,
            IDelimitedLineBuilder<TEntity>,
            IDelimitedLayoutDescriptor,
            IDelimitedFieldSettingsContainer>
    {
    }
}