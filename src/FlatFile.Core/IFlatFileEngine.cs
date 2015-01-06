namespace FlatFile.Core
{
    using System.Collections.Generic;
    using System.IO;

    public interface IFlatFileEngine<TEntity>
        where TEntity : class, new()
    {
        IEnumerable<TEntity> Read(Stream stream);

        void Write(Stream stream, IEnumerable<TEntity> entries);
    }
}