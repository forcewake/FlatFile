namespace FlatFile.Core.Events
{
    public sealed class AfterWriteEventArgs<T>
        : WriteEventArgs<T>
        where T : class, new()
    {
        public AfterWriteEventArgs(IFlatFileEngine<T> engine, T record, int lineNumber, string line)
            : base(engine, record, lineNumber)
        {
            RecordLine = line;
        }

        public string RecordLine { get; set; }
    }
}