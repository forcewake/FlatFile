using System;
using System.Collections.Generic;
using System.IO;

namespace FlatFile.Core
{
    public interface IFlatFileMultiEngine : IFlatFileEngine
    {
        void Read(Stream stream);
        IEnumerable<TType> GetResults<TType>() where TType : Type;
    }
}