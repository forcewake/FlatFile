namespace FlatFile.Core.Events
{
    using System;

    public abstract class FileHelpersEventArgs<T> : EventArgs
        where T : class, new()
    {
        protected FileHelpersEventArgs(IFlatFileEngine<T> engine, int lineNumber)
        {
            Engine = engine;
            LineNumber = lineNumber;
        }

        public IFlatFileEngine<T> Engine { get; set; }

        public int LineNumber { get; private set; }
    }
}