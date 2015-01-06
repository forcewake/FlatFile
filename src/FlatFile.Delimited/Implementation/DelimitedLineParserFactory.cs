namespace FlatFile.Delimited.Implementation
{
    public class DelimitedLineParserFactory<TEntity> : IDelimitedLineParserFactory<TEntity> where TEntity : new()
    {
        public IDelimitedLineParser<TEntity> GetParser(IDelimitedLayoutDescriptor descriptor)
        {
            return new DelimitedLineParser<TEntity>(descriptor);
        }
    }
}