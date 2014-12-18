namespace FlatFile.Core.Events
{
    public sealed class BeforeReadEventArgs<T>
        : ReadEventArgs<T>
        where T : class, new()
    {
        public BeforeReadEventArgs(IFlatFileEngine<T> engine, T record, string line, int lineNumber)
            : base(engine, line, lineNumber)
        {
            Record = record;
            SkipThisRecord = false;
        }

        public T Record { get; private set; }
    }
}