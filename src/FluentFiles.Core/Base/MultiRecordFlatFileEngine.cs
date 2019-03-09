namespace FluentFiles.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentFiles.Core;
    using FluentFiles.Core.Exceptions;

    /// <summary>
    /// Reads and writes files containing multiple types of records.
    /// </summary>
    /// <typeparam name="TFieldSettings">The type of field configuration.</typeparam>
    /// <typeparam name="TLayoutDescriptor">The type of layout descriptor.</typeparam>
    public abstract class MultiRecordFlatFileEngine<TFieldSettings, TLayoutDescriptor> : FlatFileEngine<TFieldSettings, TLayoutDescriptor>
        where TFieldSettings : IFieldSettings
        where TLayoutDescriptor : ILayoutDescriptor<TFieldSettings>
    {
        /// <summary>
        /// Stores the results of reading a file by record type.
        /// </summary>
        private readonly Dictionary<Type, IList<object>> _results;

        /// <summary>
        /// Initializes a new instance of <see cref="MultiRecordFlatFileEngine{TFieldSettings, TLayoutDescriptor}"/>.
        /// </summary>
        /// <param name="layoutDescriptors">The layout descriptors.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        protected MultiRecordFlatFileEngine(
            IEnumerable<TLayoutDescriptor> layoutDescriptors,
            FileReadErrorHandler handleEntryReadError)
                : base(handleEntryReadError)
        {
            _results = layoutDescriptors.ToDictionary(ld => ld.TargetType, _ => (IList<object>)new List<object>());
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a file header.
        /// </summary>
        /// <value><c>true</c> if this instance has a file header; otherwise, <c>false</c>.</value>
        public bool HasHeader { get; set; }

        /// <summary>
        /// Gets any records of type <typeparamref name="T" /> read by <see cref="Read(Stream)"/> or <see cref="Read(TextReader)"/>.
        /// </summary>
        /// <typeparam name="T">The type of record to retrieve.</typeparam>
        /// <returns>Any records of type <typeparamref name="T"/> that were parsed.</returns>
        public IEnumerable<T> GetRecords<T>() where T : class, new() =>
            _results.TryGetValue(typeof(T), out var results)
                ? results.Cast<T>()
                : Enumerable.Empty<T>();

        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ParseLineException">Impossible to parse line</exception>
        public void Read(Stream stream)
        {
            Read(new StreamReader(stream));
        }

        /// <summary>
        /// Reads from the specified text reader.
        /// </summary>
        /// <param name="reader">The text reader configured as the user wants.</param>
        /// <exception cref="ParseLineException">Impossible to parse line</exception>
        public void Read(TextReader reader)
        {
            ReadInternal(reader);
        }

        /// <summary>
        /// Determines the record type for a line.
        /// </summary>
        /// <param name="line">The current line.</param>
        /// <param name="lineNumber">The current line number.</param>
        /// <returns>The type of record to use for the line.</returns>
        protected abstract Type SelectRecordType(string line, int lineNumber);

        /// <summary>
        /// Handles a record and determines whether it is a master or detail record.
        /// </summary>
        /// <param name="record">The record to handle.</param>
        /// <returns>True if the record is a detail and false if it is a master record.</returns>
        protected abstract bool ProcessMasterDetail(object record);

        /// <summary>
        /// Internal method (private) to read from a text reader instead of stream
        /// This way the client code have a way to specify encoding.
        /// </summary>
        /// <param name="reader">The text reader to read.</param>
        /// <exception cref="ParseLineException">Impossible to parse line</exception>
        protected virtual void ReadInternal(TextReader reader)
        {
            string line;
            var lineNumber = 0;

            // Can't support this in a per layout manner, it has to be for the file/engine as a whole
            if (HasHeader)
            {
                ProcessHeader(reader);
            }

            while ((line = reader.ReadLine()) != null)
            {
                var ignoreEntry = false;

                // Use selector func to find type for this line, and by effect, its layout
                var type = SelectRecordType(line, lineNumber);
                if (type == null) continue;

                var layoutDescriptor = GetLayoutDescriptor(type);
                var entry = layoutDescriptor.InstanceFactory();

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

                if (ignoreEntry) continue;

                var isDetailRecord = ProcessMasterDetail(entry);
                if (isDetailRecord) continue;

                _results[type].Add(entry);
            }
        }
    }
}