using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentFiles.Core;
using FluentFiles.Core.Base;
using FluentFiles.Core.Exceptions;

namespace FluentFiles.FixedLength.Implementation
{
    /// <summary>
    /// Class FixedLengthFileMultiEngine.
    /// </summary>
    public class FixedLengthFileMultiEngine : FlatFileEngine<IFixedFieldSettingsContainer, ILayoutDescriptor<IFixedFieldSettingsContainer>>, IFlatFileMultiEngine
    {
        /// <summary>
        /// The layout descriptors for this engine
        /// </summary>
        readonly Dictionary<Type, IFixedLengthLayoutDescriptor> layoutDescriptors;
        /// <summary>
        /// The line builder factory
        /// </summary>
        readonly IFixedLengthLineBuilderFactory lineBuilderFactory;
        /// <summary>
        /// The line parser factory
        /// </summary>
        readonly IFixedLengthLineParserFactory lineParserFactory;
        /// <summary>
        /// The type selector function used to determine the layout for a given line
        /// </summary>
        readonly Func<string, int, Type> typeSelectorFunc;
        /// <summary>
        /// The results of a call to <see cref="Read"/> are stored in this Dictionary by type
        /// </summary>
        readonly Dictionary<Type, ArrayList> results;
        /// <summary>
        /// Determines how master-detail record relationships are handled.
        /// </summary>
        readonly IMasterDetailStrategy masterDetailStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFileMultiEngine"/> class.
        /// </summary>
        /// <param name="layoutDescriptors">The layout descriptors.</param>
        /// <param name="typeSelectorFunc">The type selector function.</param>
        /// <param name="lineBuilderFactory">The line builder factory.</param>
        /// <param name="lineParserFactory">The line parser factory.</param>
        /// <param name="masterDetailStrategy">Determines how master-detail record relationships are handled.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        /// <exception cref="System.ArgumentNullException">typeSelectorFunc</exception>
        internal FixedLengthFileMultiEngine(
            IEnumerable<IFixedLengthLayoutDescriptor> layoutDescriptors,
            Func<string, int, Type> typeSelectorFunc,
            IFixedLengthLineBuilderFactory lineBuilderFactory,
            IFixedLengthLineParserFactory lineParserFactory,
            IMasterDetailStrategy masterDetailStrategy,
            FileReadErrorHandler handleEntryReadError = null)
                : base(handleEntryReadError)
        {
            if (typeSelectorFunc == null) throw new ArgumentNullException(nameof(typeSelectorFunc));

            this.layoutDescriptors = layoutDescriptors.Select(ld => new FixedLengthImmutableLayoutDescriptor(ld))
                                                      .Cast<IFixedLengthLayoutDescriptor>()
                                                      .ToDictionary(ld => ld.TargetType, ld => ld);
            results = new Dictionary<Type, ArrayList>(this.layoutDescriptors.Count());
            foreach (var descriptor in this.layoutDescriptors)
            {
                results[descriptor.Value.TargetType] = new ArrayList();
            }
            this.typeSelectorFunc = typeSelectorFunc;
            this.lineBuilderFactory = lineBuilderFactory;
            this.lineParserFactory = lineParserFactory;
            this.masterDetailStrategy = masterDetailStrategy;
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
        /// Gets any records of type <typeparamref name="T" /> read by <see cref="Read" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> GetRecords<T>() where T : class, new()
        {
            return !results.ContainsKey(typeof (T)) ? new List<T>() : results[typeof(T)].Cast<T>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has a file header.
        /// </summary>
        /// <value><c>true</c> if this instance has a file header; otherwise, <c>false</c>.</value>
        public bool HasHeader { get; set; }

        /// <summary>
        /// Tries to parse the line.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="line">The line.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="entity">The entity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected override bool TryParseLine<TEntity>(string line, int lineNumber, ref TEntity entity)
        {
            var type = entity.GetType();
            var lineParser = lineParserFactory.GetParser(layoutDescriptors[type]);
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
                var type = typeSelectorFunc(line, lineNumber);
                if (type == null) continue;
                var entry = layoutDescriptors[type].InstanceFactory();

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

                bool isDetailRecord;
                masterDetailStrategy.HandleMasterDetail(entry, out isDetailRecord);

                if (isDetailRecord) continue;

                results[type].Add(entry);
            }
        }
    }
}