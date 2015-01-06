namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthLineBuilderFactory<TEntity> :
        ILineBuilderFactory<
            TEntity,
            IFixedLengthLineBuilder<TEntity>,
            ILayoutDescriptor<FixedFieldSettings>,
            FixedFieldSettings>
    {
    }
}