using System;

namespace FluentFiles.Core
{
    public interface ILineParser
    {
        TEntity ParseLine<TEntity>(ReadOnlySpan<char> line, TEntity entity) where TEntity : new();
    }
}