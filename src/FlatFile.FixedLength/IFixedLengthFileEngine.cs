namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IFixedLengthFileEngine<T> : IFlatFileEngine<T> where T : class, new()
    {
    }
}