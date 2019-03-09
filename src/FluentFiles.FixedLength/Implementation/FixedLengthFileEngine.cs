namespace FluentFiles.FixedLength.Implementation
{
    using System;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    /// <summary>
    /// Class FixedLengthFileEngine.
    /// </summary>
    public sealed class FixedLengthFileEngine : FlatFileEngine<IFixedFieldSettingsContainer, IFixedLengthLayoutDescriptor>
    {
        /// <summary>
        /// The line builder factory
        /// </summary>
        private readonly IFixedLengthLineBuilderFactory _lineBuilderFactory;

        /// <summary>
        /// The line parser factory
        /// </summary>
        private readonly IFixedLengthLineParserFactory _lineParserFactory;

        /// <summary>
        /// The layout descriptor
        /// </summary>
        private readonly IFixedLengthLayoutDescriptor _layoutDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFileEngine"/> class.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <param name="lineBuilderFactory">The line builder factory.</param>
        /// <param name="lineParserFactory">The line parser factory.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        internal FixedLengthFileEngine(
            IFixedLengthLayoutDescriptor layoutDescriptor,
            IFixedLengthLineBuilderFactory lineBuilderFactory,
            IFixedLengthLineParserFactory lineParserFactory,
            FileReadErrorHandler handleEntryReadError = null) : base(handleEntryReadError)
        {
            _lineBuilderFactory = lineBuilderFactory;
            _lineParserFactory = lineParserFactory;
            _layoutDescriptor = new FixedLengthImmutableLayoutDescriptor(layoutDescriptor);
        }

        /// <summary>
        /// Gets the layout descriptor for a record type.
        /// </summary>
        /// <param name="recordType">The record type.</param>
        /// <returns>The layout descriptor.</returns>
        protected override IFixedLengthLayoutDescriptor GetLayoutDescriptor(Type recordType) => _layoutDescriptor;

        /// <summary>
        /// Gets a line builder for a record type.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <returns>The line builder.</returns>
        protected override ILineBuilder GetLineBuilder(IFixedLengthLayoutDescriptor layoutDescriptor) => _lineBuilderFactory.GetBuilder(layoutDescriptor);

        /// <summary>
        /// Gets a line parser for a record type.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <returns>The line parser.</returns>
        protected override ILineParser GetLineParser(IFixedLengthLayoutDescriptor layoutDescriptor) => _lineParserFactory.GetParser(layoutDescriptor);
    }
}