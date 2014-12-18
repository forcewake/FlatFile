namespace FlatFile.Core.Events
{
    public abstract class WriteEventArgs<T>
        : FileHelpersEventArgs<T>
        where T : class, new()
    {
        internal WriteEventArgs(IFlatFileEngine<T> engine, T record, int lineNumber)
            : base(engine, lineNumber)
        {
            Record = record;
        }

        public T Record { get; private set; }
    }
}