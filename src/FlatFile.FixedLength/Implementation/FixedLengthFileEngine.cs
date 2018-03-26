namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    /// <summary>
    /// Class FixedLengthFileEngine.
    /// </summary>
    internal class FixedLengthFileEngine : FlatFileEngine<IFixedFieldSettingsContainer, ILayoutDescriptor<IFixedFieldSettingsContainer>>
    {
        /// <summary>
        /// The line builder factory
        /// </summary>
        private readonly IFixedLengthLineBuilderFactory lineBuilderFactory;
        /// <summary>
        /// The line parser factory
        /// </summary>
        private readonly IFixedLengthLineParserFactory lineParserFactory;
        /// <summary>
        /// The layout descriptor
        /// </summary>
        private readonly ILayoutDescriptor<IFixedFieldSettingsContainer> layoutDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedLengthFileEngine"/> class.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <param name="lineBuilderFactory">The line builder factory.</param>
        /// <param name="lineParserFactory">The line parser factory.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        internal FixedLengthFileEngine(
            ILayoutDescriptor<IFixedFieldSettingsContainer> layoutDescriptor,
            IFixedLengthLineBuilderFactory lineBuilderFactory,
            IFixedLengthLineParserFactory lineParserFactory,
            Func<string, Exception, bool> handleEntryReadError = null) : base(handleEntryReadError)
        {
            this.lineBuilderFactory = lineBuilderFactory;
            this.lineParserFactory = lineParserFactory;
            this.layoutDescriptor = layoutDescriptor;
        }

        /// <summary>
        /// Gets the line builder.
        /// </summary>
        /// <value>The line builder.</value>
        protected override ILineBulder LineBuilder
        {
            get { return lineBuilderFactory.GetBuilder(LayoutDescriptor); }
        }

        /// <summary>
        /// Gets the line parser.
        /// </summary>
        /// <value>The line parser.</value>
        protected override ILineParser LineParser
        {
            get { return lineParserFactory.GetParser(LayoutDescriptor); }
        }

        /// <summary>
        /// Gets the layout descriptor.
        /// </summary>
        /// <value>The layout descriptor.</value>
        protected override ILayoutDescriptor<IFixedFieldSettingsContainer> LayoutDescriptor
        {
            get { return layoutDescriptor; }
        }
    }
}
