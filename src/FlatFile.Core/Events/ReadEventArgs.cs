namespace FlatFile.Core.Events
{
    public abstract class ReadEventArgs<T>
        : FileHelpersEventArgs<T>
        where T : class, new()
    {
        internal ReadEventArgs(IFlatFileEngine<T> engine, string line, int lineNumber)
            : base(engine, lineNumber)
        {
            RecordLineChanged = false;
            _recordLine = line;
        }

        private string _recordLine;

        /// <summary>The record line just read.</summary>
        public string RecordLine
        {
            get { return _recordLine; }
            set
            {
                if (_recordLine == value)
                    return;

                _recordLine = value;
                RecordLineChanged = true;
            }
        }

        public bool RecordLineChanged { get; protected set; }

        public bool SkipThisRecord { get; set; }
    }
}