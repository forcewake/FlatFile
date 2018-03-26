namespace FlatFile.Delimited.Implementation
{
    using System;
    using System.IO;
    using System.Text;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    /// <summary>
    /// Class DelimitedFileEngine.
    /// </summary>
    internal class DelimitedFileEngine :
        FlatFileEngine<IDelimitedFieldSettingsContainer, IDelimitedLayoutDescriptor>
    {
        /// <summary>
        /// The line builder factory
        /// </summary>
        private readonly IDelimitedLineBuilderFactory _builderFactory;

        /// <summary>
        /// The line parser factory
        /// </summary>
        private readonly IDelimitedLineParserFactory _parserFactory;

        /// <summary>
        /// The layout descriptor
        /// </summary>
        private readonly IDelimitedLayoutDescriptor _layoutDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedFileEngine"/> class.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <param name="builderFactory">The builder factory.</param>
        /// <param name="parserFactory">The parser factory.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        internal DelimitedFileEngine(
            IDelimitedLayoutDescriptor layoutDescriptor,
            IDelimitedLineBuilderFactory builderFactory,
            IDelimitedLineParserFactory parserFactory, 
            Func<string, Exception, bool> handleEntryReadError = null)
            : base(handleEntryReadError)
        {
            _builderFactory = builderFactory;
            _parserFactory = parserFactory;
            _layoutDescriptor = layoutDescriptor;
        }

        /// <summary>
        /// Gets the line builder.
        /// </summary>
        /// <value>The line builder.</value>
        protected override ILineBulder LineBuilder
        {
            get { return _builderFactory.GetBuilder(LayoutDescriptor); }
        }

        /// <summary>
        /// Gets the line parser.
        /// </summary>
        /// <value>The line parser.</value>
        protected override ILineParser LineParser
        {
            get { return _parserFactory.GetParser(LayoutDescriptor); }
        }

        /// <summary>
        /// Gets the layout descriptor.
        /// </summary>
        /// <value>The layout descriptor.</value>
        protected override IDelimitedLayoutDescriptor LayoutDescriptor
        {
            get { return _layoutDescriptor; }
        }

        /// <summary>
        /// Writes the header.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteHeader(TextWriter writer)
        {
            if (!LayoutDescriptor.HasHeader)
            {
                return;
            }

            var fields = LayoutDescriptor.Fields;

            var stringBuilder = new StringBuilder();

            foreach (var field in fields)
            {
                stringBuilder
                    .Append(field.Name)
                    .Append(LayoutDescriptor.Delimiter);

            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            writer.WriteLine(stringBuilder.ToString());
        }
    }
}
