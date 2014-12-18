namespace FlatFile.Core.Events
{
    public sealed class AfterReadEventArgs<T>
        : ReadEventArgs<T>
        where T : class, new()
    {
        public AfterReadEventArgs(IFlatFileEngine<T> engine,
            string line,
            bool lineChanged,
            T newRecord,
            int lineNumber)
            : base(engine, line, lineNumber)
        {
            SkipThisRecord = false;
            Record = newRecord;
            RecordLineChanged = lineChanged;
        }

        public T Record { get; set; }
    }
}
