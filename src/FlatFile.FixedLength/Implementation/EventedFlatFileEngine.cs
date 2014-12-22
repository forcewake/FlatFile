namespace FlatFile.FixedLength.Implementation
{
    using System;
    using System.IO;
    using FlatFile.Core.Base;
    using FlatFile.Core.Events;

    public class EventedFlatFileEngine<T> : FlatFileEngine<T>, IEventedFixedLengthFileEngine<T> where T : class, new()
    {
        public EventedFlatFileEngine(Stream innerStream, IFixedLengthLineParser<T> parser,
            IFixedLengthLineBuilder<T> builder, Func<string, Exception, bool> handleEntryReadError = null)
            : base(innerStream, parser, builder, handleEntryReadError)
        {
        }

        public event BeforeReadHandler<T> BeforeReadRecord = delegate { };
        public event AfterReadHandler<T> AfterReadRecord = delegate { };
        public event BeforeWriteHandler<T> BeforeWriteRecord = delegate { };
        public event AfterWriteHandler<T> AfterWriteRecord = delegate { };

        protected override T ParseLine(T entry, string line, int lineNumber)
        {
            BeforeReadRecord(this, new BeforeReadEventArgs<T>(this, entry, line, lineNumber));
            entry = base.ParseLine(entry, line, lineNumber);
            AfterReadRecord(this, new AfterReadEventArgs<T>(this, line, false, entry, lineNumber));
            return entry;
        }

        protected override string BuildLine(T entry, int lineNumber)
        {
            string line = base.BuildLine(entry, lineNumber);
            BeforeWriteRecord(this, new BeforeWriteEventArgs<T>(this, entry, lineNumber));
            return line;
        }

        protected override void WriteLine(TextWriter writer, string line, T entry, int lineNumber)
        {
            base.WriteLine(writer, line, entry, lineNumber);
            AfterWriteRecord(this, new AfterWriteEventArgs<T>(this, entry, lineNumber, line));
        }
    }
}