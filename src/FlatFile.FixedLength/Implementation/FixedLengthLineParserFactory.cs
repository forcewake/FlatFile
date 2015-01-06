namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;

    public class FixedLengthLineParserFactory<TEntity> : IFixedLengthLineParserFactory<TEntity> 
        where TEntity : new()
    {
        public IFixedLengthLineParser<TEntity> GetParser(ILayoutDescriptor<FixedFieldSettings> descriptor)
        {
            return new FixedLengthLineParser<TEntity>(descriptor);
        }
    }
}