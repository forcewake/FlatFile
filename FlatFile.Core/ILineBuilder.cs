using System.IO;

namespace FlatFile.Core
{
    public interface ILineBuilder
    {
        void BuildLine<T>(T entry, TextWriter writer);
    }
}