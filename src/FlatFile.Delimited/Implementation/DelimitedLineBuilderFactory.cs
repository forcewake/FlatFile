namespace FlatFile.Delimited.Implementation
{
    public class DelimitedLineBuilderFactory<TEntity> : IDelimitedLineBuilderFactory<TEntity>
    {
        public IDelimitedLineBuilder<TEntity> GetBuilder(IDelimitedLayoutDescriptor descriptor)
        {
            return new DelimitedLineBuilder<TEntity>(descriptor);
        }
    }
}