namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.IO;
    using System.Text;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    /// <summary>
    /// Class DelimitedFileEngine.
    /// </summary>
    public sealed class DelimitedFileEngine :
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
            FileReadErrorHandler handleEntryReadError = null)
                : base(handleEntryReadError)
        {
            _builderFactory = builderFactory;
            _parserFactory = parserFactory;
            _layoutDescriptor = new DelimitedImmutableLayoutDescriptor(layoutDescriptor);
        }

        /// <summary>
        /// Gets the layout descriptor for a record type.
        /// </summary>
        /// <param name="recordType">The record type.</param>
        /// <returns>The layout descriptor.</returns>
        protected override IDelimitedLayoutDescriptor GetLayoutDescriptor(Type recordType) => _layoutDescriptor;

        /// <summary>
        /// Gets a line builder for a record type.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <returns>The line builder.</returns>
        protected override ILineBuilder GetLineBuilder(IDelimitedLayoutDescriptor layoutDescriptor) => _builderFactory.GetBuilder(layoutDescriptor);

        /// <summary>
        /// Gets a line parser for a record type.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <returns>The line parser.</returns>
        protected override ILineParser GetLineParser(IDelimitedLayoutDescriptor layoutDescriptor) => _parserFactory.GetParser(layoutDescriptor);

        /// <summary>
        /// Writes the header.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteHeader(TextWriter writer)
        {
            var layoutDescriptor = _layoutDescriptor;
            if (!layoutDescriptor.HasHeader)
            {
                return;
            }

            var fields = layoutDescriptor.Fields;

            var stringBuilder = new StringBuilder();

            foreach (var field in fields)
            {
                stringBuilder
                    .Append(field.Name)
                    .Append(layoutDescriptor.Delimiter);
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            writer.WriteLine(stringBuilder.ToString());
        }
    }
}