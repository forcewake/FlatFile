namespace FlatFile.FixedLength
{
    using FlatFile.Core;

    public interface IEventedFixedLengthFileEngine<T> : IEventedFlatFileEngine<T> where T : class, new()
    {
    }
}