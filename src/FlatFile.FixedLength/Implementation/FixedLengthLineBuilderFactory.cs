namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;

    public class FixedLengthLineBuilderFactory<TEntity> : IFixedLengthLineBuilderFactory<TEntity>
    {
        public IFixedLengthLineBuilder<TEntity> GetBuilder(ILayoutDescriptor<FixedFieldSettings> descriptor)
        {
            return new FixedLengthLineBuilder<TEntity>(descriptor);
        }
    }
}