namespace FluentFiles.Core
{
    public interface ILineParser
    {
        TEntity ParseLine<TEntity>(string line, TEntity entity) where TEntity : new();
    }
}