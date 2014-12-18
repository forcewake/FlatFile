namespace FlatFile.Core.Events
{
    public delegate void AfterReadHandler<T>(IFlatFileEngine<T> engine, AfterReadEventArgs<T> e) where T : class, new();

    public delegate void BeforeWriteHandler<T>(IFlatFileEngine<T> engine, BeforeWriteEventArgs<T> e) where T : class, new();

    public delegate void BeforeReadHandler<T>(IFlatFileEngine<T> engine, BeforeReadEventArgs<T> e) where T : class, new();

    public delegate void AfterWriteHandler<T>(IFlatFileEngine<T> engine, AfterWriteEventArgs<T> e) where T : class, new();
}