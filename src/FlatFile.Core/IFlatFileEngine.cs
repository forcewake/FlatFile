namespace FlatFile.Core
{
    using System.Collections.Generic;

    public interface IFlatFileEngine<T>
        where T : class, new()
    {
        IEnumerable<T> Read();

        void Write(IEnumerable<T> entries);
    }
}