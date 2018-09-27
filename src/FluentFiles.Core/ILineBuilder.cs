namespace FluentFiles.Core
{
    public interface ILineBuilder
    {
        string BuildLine<T>(T entry);
    }
}