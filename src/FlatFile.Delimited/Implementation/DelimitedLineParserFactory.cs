namespace FlatFile.Delimited.Implementation
{
    public class DelimitedLineParserFactory<TEntity> : IDelimitedLineParserFactory<TEntity> where TEntity : new()
    {
        public IDelimitedLineParser<TEntity> GetParser(IDelimitedLayout<TEntity> layout)
        {
            return new DelimitedLineParser<TEntity>(layout);
        }
    }
}