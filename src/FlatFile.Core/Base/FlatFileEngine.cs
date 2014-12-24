namespace FlatFile.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FlatFile.Core;
    using FlatFile.Core.Exceptions;

    public abstract class FlatFileEngine<TEntity, TLayout, TFieldSettings, TConstructor, TBuilder, TParser> :
        IFlatFileEngine<TEntity, TLayout, TFieldSettings, TConstructor>
        where TEntity : class, new()
        where TLayout : ILayout<TEntity, TFieldSettings, TConstructor, TLayout>
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
        where TBuilder : ILineBulder<TEntity>
        where TParser : ILineParser<TEntity>
    {
        private readonly Func<string, Exception, bool> _handleEntryReadError;

        protected abstract ILineBuilderFactory<TEntity, TBuilder, TLayout, TFieldSettings, TConstructor> BuilderFactory { get; }

        protected abstract ILineParserFactory<TEntity, TParser, TLayout, TFieldSettings, TConstructor> ParserFactory { get; }

        protected FlatFileEngine(Func<string, Exception, bool> handleEntryReadError = null)
        {
            _handleEntryReadError = handleEntryReadError;
        }

        public virtual IEnumerable<TEntity> Read(TLayout layout, Stream stream)
        {
            var reader = new StreamReader(stream);
            string line;
            int lineNumber = 0;

            if (layout.HasHeader)
            {
                ProcessHeader(layout, reader);
            }

            while ((line = reader.ReadLine()) != null)
            {
                bool ignoreEntry = false;
                TEntity entry = null;
                try
                {
                    if (!TryParseLine(layout, line, lineNumber++, out entry))
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

        protected virtual void ProcessHeader(TLayout layout, StreamReader reader)
        {
            reader.ReadLine();
        }

        protected virtual bool TryParseLine(TLayout layout, string line, int lineNumber, out TEntity entity)
        {
            var parser = ParserFactory.GetParser(layout);

            entity = new TEntity();

            parser.ParseLine(line, entity);

            return true;
        }

        protected virtual void WriteEntry(TLayout layout, TextWriter writer, int lineNumber, TEntity entity)
        {
            var builder = BuilderFactory.GetBuilder(layout);

            var line = builder.BuildLine(entity);

            writer.WriteLine(line);
        }

        public virtual void Write(TLayout layout, Stream stream, IEnumerable<TEntity> entries)
        {
            TextWriter writer = new StreamWriter(stream);

            this.WriteHeader(layout, writer);

            int lineNumber = 0;

            foreach (var entry in entries)
            {
                this.WriteEntry(layout, writer, lineNumber, entry);

                lineNumber += 1;
            }

            this.WriteFooter(layout, writer);

            writer.Flush();
        }

        protected virtual void WriteHeader(TLayout layout, TextWriter writer)
        {
        }

        protected virtual void WriteFooter(TLayout layout, TextWriter writer)
        {
        }
    }
}