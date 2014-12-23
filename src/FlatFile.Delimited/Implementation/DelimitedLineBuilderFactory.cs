namespace FlatFile.Delimited.Implementation
{
    public class DelimitedLineBuilderFactory<TEntity> : IDelimitedLineBuilderFactory<TEntity>
    {
        public IDelimitedLineBuilder<TEntity> GetBuilder(IDelimitedLayout<TEntity> layout)
        {
            return new DelimitedLineBuilder<TEntity>(layout);
        }
    }
}