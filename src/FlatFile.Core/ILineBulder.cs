namespace FlatFile.Core
{
    public interface ILineBulder<in T>
    {
        string BuildLine(T entry);
    }
}