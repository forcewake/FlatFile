namespace FlatFile.Core
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFlatFileEngine
    {
        IEnumerable<TEntity> Read<TEntity>(Stream stream) where TEntity : class, new();

        void Write<TEntity>(Stream stream, IEnumerable<TEntity> entries) where TEntity : class, new();
    }
}