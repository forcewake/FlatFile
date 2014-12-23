namespace FlatFile.FixedLength.Implementation
{
    public class FixedLengthLineBuilderFactory<TEntity> : IFixedLengthLineBuilderFactory<TEntity>
    {
        public IFixedLengthLineBuilder<TEntity> GetBuilder(IFixedLayout<TEntity> layout)
        {
            return new FixedLengthLineBuilder<TEntity>(layout);
        }
    }
}