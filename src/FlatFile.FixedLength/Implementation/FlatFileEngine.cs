namespace FlatFile.FixedLength.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FlatFileEngine<T> : IDisposable, IFixedLengthFileEngine<T>
        where T : class, new()
    {
        private readonly IFixedLengthLineBuilder<T> _builder;
        private readonly Func<string, Exception, bool> _handleEntryReadError;
        private readonly Stream _innerStream;

        private readonly IFixedLengthLineParser<T> _parser;

        public FlatFileEngine(Stream innerStream, IFixedLengthLineParser<T> parser, IFixedLengthLineBuilder<T> builder,
            Func<string, Exception, bool> handleEntryReadError = null)
        {
            _innerStream = innerStream;
            _handleEntryReadError = handleEntryReadError;
            _parser = parser;
            _builder = builder;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<T> Read()
        {
            var reader = new StreamReader(_innerStream);
            string line;
            int lineNumber = 0;
            while ((line = reader.ReadLine()) != null)
            {
                bool ignoreEntry = false;
                var entry = new T();
                try
                {
                    lineNumber++;
                    entry = ParseLine(entry, line, lineNumber);
                }
                catch (Exception ex)
                {
                    if (_handleEntryReadError == null)
                    {
                        throw;
                    }

                    if (!_handleEntryReadError(line, ex))
                    {
                        throw;
                    }

                    ignoreEntry = true;
                }
                if (!ignoreEntry)
                {
                    yield return entry;
                }
            }
        }

        public void Write(IEnumerable<T> entries)
        {
            int lineNumber = 0;
            TextWriter writer = new StreamWriter(_innerStream);
            foreach (T entry in entries)
            {
                lineNumber++;
                string line = BuildLine(entry, lineNumber);
                WriteLine(writer, line, entry, lineNumber);
            }
            writer.Flush();
        }

        protected virtual T ParseLine(T entry, string line, int lineNumber)
        {
            entry = _parser.ParseLine(line, entry);
            return entry;
        }

        protected virtual void WriteLine(TextWriter writer, string line, T entry, int lineNumber)
        {
            writer.WriteLine(line);
        }

        protected virtual string BuildLine(T entry, int lineNumber)
        {
            string line = _builder.BuildLine(entry);
            return line;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_innerStream != null)
                {
                    _innerStream.Dispose();
                }
            }
        }

        ~FlatFileEngine()
        {
            Dispose(false);
        }
    }
}