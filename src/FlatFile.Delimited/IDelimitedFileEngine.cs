namespace FlatFile.Delimited
{
    using FlatFile.Core;

    public interface IDelimitedFileEngine<T> : IFlatFileEngine<T> where T : class, new()
    {
    }
}