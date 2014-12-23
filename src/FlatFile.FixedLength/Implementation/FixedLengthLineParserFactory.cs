namespace FlatFile.FixedLength.Implementation
{
    public class FixedLengthLineParserFactory<TEntity> : IFixedLengthLineParserFactory<TEntity> where TEntity : new()
    {
        public IFixedLengthLineParser<TEntity> GetParser(IFixedLayout<TEntity> layout)
        {
            return new FixedLengthLineParser<TEntity>(layout);
        }
    }
}