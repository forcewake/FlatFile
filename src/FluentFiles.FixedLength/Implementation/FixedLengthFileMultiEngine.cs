namespace FluentFiles.FixedLength.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;
    using FluentFiles.Core.Exceptions;

    /// <summary>
    /// A fixed length file engine capable of handling files with multiple types of records.
    /// </summary>
    public class FixedLengthFileMultiEngine : FlatFileEngine<IFixedFieldSettingsContainer, ILayoutDescriptor<IFixedFieldSettingsContainer>>, IFlatFileMultiEngine
    {
        /// <summary>
        /// The layout descriptors for this engine.
        /// </summary>
        private readonly Dictionary<Type, IFixedLengthLayoutDescriptor> _layoutDescriptors;

        /// <summary>
        /// The line builder factory.
        /// </summary>
        private readonly IFixedLengthLineBuilderFactory _lineBuilderFactory;

        /// <summary>
        /// The line parser factory.
        /// </summary>
        private readonly IFixedLengthLineParserFactory _lineParserFactory;

        /// <summary>
        /// The type selector function used to determine the layout for a given line.
        /// </summary>
        private readonly Func<string, int, Type> _typeSelector;

        /// <summary>
        /// The results of a call to <see cref="Read(Stream)"/> or <see cref="Read(TextReader)"/> are stored by record type.
        /// </summary>
        private readonly Dictionary<Type, IList<object>> _results;

        /// <summary>
        /// Determines how master-detail record relationships are handled.
        /// </summary>
        private readonly IMasterDetailStrategy _masterDetailStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFileMultiEngine"/> class.
        /// </summary>
        /// <param name="layoutDescriptors">The layout descriptors.</param>
        /// <param name="typeSelector">The type selector function.</param>
        /// <param name="lineBuilderFactory">The line builder factory.</param>
        /// <param name="lineParserFactory">The line parser factory.</param>
        /// <param name="masterDetailStrategy">Determines how master-detail record relationships are handled.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        /// <exception cref="System.ArgumentNullException">typeSelectorFunc</exception>
        internal FixedLengthFileMultiEngine(
            IEnumerable<IFixedLengthLayoutDescriptor> layoutDescriptors,
            Func<string, int, Type> typeSelector,
            IFixedLengthLineBuilderFactory lineBuilderFactory,
            IFixedLengthLineParserFactory lineParserFactory,
            IMasterDetailStrategy masterDetailStrategy,
            FileReadErrorHandler handleEntryReadError = null)
                : base(handleEntryReadError)
        {
            _layoutDescriptors = layoutDescriptors.Select(ld => new FixedLengthImmutableLayoutDescriptor(ld))
                                                  .Cast<IFixedLengthLayoutDescriptor>()
                                                  .ToDictionary(ld => ld.TargetType, ld => ld);

            _results = _layoutDescriptors.ToDictionary(ld => ld.Value.TargetType, _ => (IList<object>)new List<object>());

            _typeSelector = typeSelector ?? throw new ArgumentNullException(nameof(typeSelector));
            _lineBuilderFactory = lineBuilderFactory;
            _lineParserFactory = lineParserFactory;
            _masterDetailStrategy = masterDetailStrategy;
        }

        /// <summary>
        /// Gets the line builder.
        /// </summary>
        /// <value>The line builder.</value>
        /// <remarks>The <see cref="FixedLengthFileMultiEngine"/> does not contain just a single line builder.</remarks>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override ILineBuilder LineBuilder { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Gets the line parser.
        /// </summary>
        /// <value>The line parser.</value>
        /// <remarks>The <see cref="FixedLengthFileMultiEngine"/> does not contain just a single line parser.</remarks>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override ILineParser LineParser { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Gets the layout descriptor.
        /// </summary>
        /// <remarks>The <see cref="FixedLengthFileMultiEngine"/> does not contain just a single layout.</remarks>
        /// <value>The layout descriptor.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override ILayoutDescriptor<IFixedFieldSettingsContainer> LayoutDescriptor { get { throw new NotImplementedException(); } }

        /// <summary>
        /// Gets any records of type <typeparamref name="T" /> read by <see cref="Read(Stream)"/> or <see cref="Read(TextReader)"/>.
        /// </summary>
        /// <typeparam name="T">The type of record to retrieve.</typeparam>
        /// <returns>Any records of type <typeparamref name="T"/> that were parsed.</returns>
        public IEnumerable<T> GetRecords<T>() where T : class, new()
        {
            return _results.TryGetValue(typeof(T), out var results) ? results.Cast<T>() : Enumerable.Empty<T>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a file header.
        /// </summary>
        /// <value><c>true</c> if this instance has a file header; otherwise, <c>false</c>.</value>
        public bool HasHeader { get; set; }

        /// <summary>
        /// Tries to parse a line.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="line">The line.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        protected override bool TryParseLine<TEntity>(string line, int lineNumber, ref TEntity entity)
        {
            var type = entity.GetType();
            var lineParser = _lineParserFactory.GetParser(_layoutDescriptors[type]);
            lineParser.ParseLine(line.AsSpan(), entity);

            return true;
        }

        /// <summary>
        /// Reads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ParseLineException">Impossible to parse line</exception>
        public void Read(Stream stream)
        {
            var reader = new StreamReader(stream);
            ReadInternal(reader);
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
        /// Internal method (private) to read from a text reader instead of stream
        /// This way the client code have a way to specify encoding.
        /// </summary>
        /// <param name="reader">The text reader to read.</param>
        /// <exception cref="ParseLineException">Impossible to parse line</exception>
        private void ReadInternal(TextReader reader)
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
                var type = _typeSelector(line, lineNumber);
                if (type == null) continue;
                var entry = _layoutDescriptors[type].InstanceFactory();

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

                _masterDetailStrategy.HandleMasterDetail(entry, out var isDetailRecord);

                if (isDetailRecord) continue;

                _results[type].Add(entry);
            }
        }
    }
}