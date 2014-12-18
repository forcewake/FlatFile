namespace FlatFile.Core
{
    public interface ILineParser<T>
    {
        T ParseLine(string line, T entry);
    }
}