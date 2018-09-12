using System;

namespace FluentFiles.Core
{
    public interface ILineParser
    {
        TEntity ParseLine<TEntity>(in ReadOnlySpan<char> line, TEntity entity) where TEntity : new();
    }
}