namespace FlatFile.Core.Events
{
    public sealed class BeforeWriteEventArgs<T>
        : WriteEventArgs<T>
        where T : class, new()
    {
        public BeforeWriteEventArgs(IFlatFileEngine<T> engine, T record, int lineNumber)
            : base(engine, record, lineNumber)
        {
            SkipThisRecord = false;
        }

        public bool SkipThisRecord { get; set; }
    }
}