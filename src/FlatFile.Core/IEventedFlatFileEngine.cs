namespace FlatFile.Core
{
    using FlatFile.Core.Events;

    public interface IEventedFlatFileEngine<T> : IFlatFileEngine<T>
        where T : class, new()
    {
        event BeforeReadHandler<T> BeforeReadRecord;

        event AfterReadHandler<T> AfterReadRecord;

        event BeforeWriteHandler<T> BeforeWriteRecord;

        event AfterWriteHandler<T> AfterWriteRecord;
    }
}