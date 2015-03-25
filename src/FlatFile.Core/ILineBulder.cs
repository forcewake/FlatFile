namespace FlatFile.Core
{
    public interface ILineBulder
    {
        string BuildLine<T>(T entry);
    }
}