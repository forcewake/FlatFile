namespace FlatFile.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FlatFile.Core;
    using FlatFile.Core.Exceptions;

    public abstract class FlatFileEngine<TEntity, TFieldSettings, TLayoutDescriptor> : IFlatFileEngine<TEntity>
        where TEntity : class, new()
        where TFieldSettings : IFieldSettings
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
    {
        private readonly Func<string, Exception, bool> _handleEntryReadError;

        protected abstract ILineBulder LineBuilder { get; }

        protected abstract ILineParser LineParser { get; }

        protected abstract TLayoutDescriptor LayoutDescriptor { get; }

        protected FlatFileEngine(Func<string, Exception, bool> handleEntryReadError = null)
        {
            _handleEntryReadError = handleEntryReadError;
        }

        public virtual IEnumerable<TEntity> Read(Stream stream)
        {
            var reader = new StreamReader(stream);
            string line;
            int lineNumber = 0;

            if (LayoutDescriptor.HasHeader)
            {
                ProcessHeader(reader);
            }

            while ((line = reader.ReadLine()) != null)
            {
                bool ignoreEntry = false;
                TEntity entry = null;
                try
                {
                    if (!TryParseLine(line, lineNumber++, out entry))
                    {
                        throw new ParseLineException("Impossible to parse line", line, lineNumber);
                    }
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

        protected virtual void ProcessHeader(StreamReader reader)
        {
            reader.ReadLine();
        }

        protected virtual bool TryParseLine(string line, int lineNumber, out TEntity entity)
        {
            entity = new TEntity();

            LineParser.ParseLine(line, entity);

            return true;
        }

        protected virtual void WriteEntry(TextWriter writer, int lineNumber, TEntity entity)
        {
            var line = LineBuilder.BuildLine(entity);

            writer.WriteLine(line);
        }

        public virtual void Write(Stream stream, IEnumerable<TEntity> entries)
        {
            TextWriter writer = new StreamWriter(stream);

            this.WriteHeader(writer);

            int lineNumber = 0;

            foreach (var entry in entries)
            {
                this.WriteEntry(writer, lineNumber, entry);

                lineNumber += 1;
            }

            this.WriteFooter(writer);

            writer.Flush();
        }

        protected virtual void WriteHeader(TextWriter writer)
        {
        }

        protected virtual void WriteFooter(TextWriter writer)
        {
        }
    }
}