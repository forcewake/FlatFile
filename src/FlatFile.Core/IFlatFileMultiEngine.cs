using System;
using System.Collections.Generic;
using System.IO;

namespace FlatFile.Core
{
    public interface IFlatFileMultiEngine : IFlatFileEngine
    {
        void Read(Stream stream);
        IEnumerable<T> GetRecords<T>() where T : class, new();
    }
}