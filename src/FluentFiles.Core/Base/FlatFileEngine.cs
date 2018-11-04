namespace FluentFiles.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using FluentFiles.Core;
    using FluentFiles.Core.Exceptions;

    /// <summary>
    /// Class FlatFileEngine.
    /// </summary>
    /// <typeparam name="TFieldSettings">The type of the t field settings.</typeparam>
    /// <typeparam name="TLayoutDescriptor">The type of the t layout descriptor.</typeparam>
    public abstract class FlatFileEngine<TFieldSettings, TLayoutDescriptor> : IFlatFileEngine
        where TFieldSettings : IFieldSettings
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
    {
        /// <summary>
        /// Gets the line builder.
        /// </summary>
        /// <value>The line builder.</value>
        protected abstract ILineBuilder LineBuilder { get; }

        /// <summary>
        /// Gets the line parser.
        /// </summary>
        /// <value>The line parser.</value>
        protected abstract ILineParser LineParser { get; }

        /// <summary>
        /// Gets the layout descriptor.
        /// </summary>
        /// <value>The layout descriptor.</value>
        protected abstract TLayoutDescriptor LayoutDescriptor { get; }

        /// <summary>
        /// Handles file read errors.
        /// </summary>
        protected FileReadErrorHandler HandleEntryReadError { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatFileEngine{TFieldSettings, TLayoutDescriptor}"/> class.
        /// </summary>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        protected FlatFileEngine(FileReadErrorHandler handleEntryReadError = null)
        {
            HandleEntryReadError = handleEntryReadError;
        }

        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        /// <exception cref="ParseLineException">Impossible to parse line</exception>
        public virtual IEnumerable<TEntity> Read<TEntity>(Stream stream) where TEntity : class, new()
        {
            var reader = new StreamReader(stream);
            return Read<TEntity>(reader);
        }

        /// <summary>
        /// Reads the specified text reader.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns>IEnumerable&lt;TEntity&gt;.</returns>
        /// <exception cref="ParseLineException">Impossible to parse line</exception>
        public virtual IEnumerable<TEntity> Read<TEntity>(TextReader reader) where TEntity : class, new()
        {
            string line;
            int lineNumber = 0;

            if (LayoutDescriptor.HasHeader)
            {
                ProcessHeader(reader);
            }

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line) || string.IsNullOrEmpty(line.Trim())) continue;

                bool ignoreEntry = false;
                var entry = (TEntity)LayoutDescriptor.InstanceFactory();
                try
                {
                    if (!TryParseLine(line, lineNumber++, ref entry))
                    {
                        throw new ParseLineException("Impossible to parse line", line, lineNumber);
                    }
                }
                catch (Exception ex)
                {
                    if (HandleEntryReadError == null)
                    {
                        throw;
                    }

                    if (!HandleEntryReadError(new FlatFileErrorContext(line, lineNumber, ex)))
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

        /// <summary>
        /// Processes the header.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected virtual void ProcessHeader(TextReader reader)
        {
            reader.ReadLine();
        }

        /// <summary>
        /// Tries to parse the line.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="line">The line.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected virtual bool TryParseLine<TEntity>(string line, int lineNumber, ref TEntity entity) where TEntity : class, new()
        {
            LineParser.ParseLine(line.AsSpan(), entity);

            return true;
        }

        /// <summary>
        /// Writes the entry.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="writer">The writer.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="entity">The entity.</param>
        protected virtual void WriteEntry<TEntity>(TextWriter writer, int lineNumber, TEntity entity)
        {
            var line = LineBuilder.BuildLine(entity);

            writer.WriteLine(line);
        }

        /// <summary>
        /// Writes to the specified stream.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="entries">The entries.</param>
        public virtual void Write<TEntity>(Stream stream, IEnumerable<TEntity> entries) where TEntity : class, new()
        {
            TextWriter writer = new StreamWriter(stream);
            Write(writer, entries);
        }

        /// <summary>
        /// Writes to the specified text writer.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="writer">The text writer.</param>
        /// <param name="entries">The entries.</param>
        public void Write<TEntity>(TextWriter writer, IEnumerable<TEntity> entries) where TEntity : class, new()
        {
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

        /// <summary>
        /// Writes the header.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void WriteHeader(TextWriter writer)
        {
        }

        /// <summary>
        /// Writes the footer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void WriteFooter(TextWriter writer)
        {
        }
    }
}