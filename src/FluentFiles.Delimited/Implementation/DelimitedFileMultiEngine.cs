namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentFiles.Core;
    using FluentFiles.Core.Base;

    /// <summary>
    /// A delimited file engine capable of handling files with multiple types of records.
    /// </summary>
    public sealed class DelimitedFileMultiEngine : MultiRecordFlatFileEngine<IDelimitedFieldSettingsContainer, IDelimitedLayoutDescriptor>, IFlatFileMultiEngine
    {
        /// <summary>
        /// The layout descriptors for this engine.
        /// </summary>
        private readonly Dictionary<Type, IDelimitedLayoutDescriptor> _layoutDescriptors;

        /// <summary>
        /// The line builder factory.
        /// </summary>
        private readonly IDelimitedLineBuilderFactory _lineBuilderFactory;

        /// <summary>
        /// The line parser factory.
        /// </summary>
        private readonly IDelimitedLineParserFactory _lineParserFactory;

        /// <summary>
        /// The type selector function used to determine the layout for a given line.
        /// </summary>
        private readonly Func<string, int, Type> _typeSelector;

        /// <summary>
        /// Determines how master-detail record relationships are handled.
        /// </summary>
        private readonly IMasterDetailStrategy _masterDetailStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelimitedFileMultiEngine"/> class.
        /// </summary>
        /// <param name="layoutDescriptors">The layout descriptors.</param>
        /// <param name="typeSelector">The type selector function.</param>
        /// <param name="lineBuilderFactory">The line builder factory.</param>
        /// <param name="lineParserFactory">The line parser factory.</param>
        /// <param name="masterDetailStrategy">Determines how master-detail record relationships are handled.</param>
        /// <param name="handleEntryReadError">The handle entry read error.</param>
        /// <exception cref="System.ArgumentNullException">typeSelectorFunc</exception>
        internal DelimitedFileMultiEngine(
            IEnumerable<IDelimitedLayoutDescriptor> layoutDescriptors,
            Func<string, int, Type> typeSelector,
            IDelimitedLineBuilderFactory lineBuilderFactory,
            IDelimitedLineParserFactory lineParserFactory,
            IMasterDetailStrategy masterDetailStrategy,
            FileReadErrorHandler handleEntryReadError = null)
                : base(layoutDescriptors, handleEntryReadError)
        {
            _layoutDescriptors = layoutDescriptors.Select(ld => new DelimitedImmutableLayoutDescriptor(ld))
                                                  .Cast<IDelimitedLayoutDescriptor>()
                                                  .ToDictionary(ld => ld.TargetType, ld => ld);

            _typeSelector = typeSelector ?? throw new ArgumentNullException(nameof(typeSelector));
            _lineBuilderFactory = lineBuilderFactory ?? throw new ArgumentNullException(nameof(lineBuilderFactory));
            _lineParserFactory = lineParserFactory ?? throw new ArgumentNullException(nameof(lineParserFactory));
            _masterDetailStrategy = masterDetailStrategy ?? throw new ArgumentNullException(nameof(masterDetailStrategy));
        }

        /// <summary>
        /// Gets the layout descriptor for a record type.
        /// </summary>
        /// <param name="recordType">The record type.</param>
        /// <returns>The layout descriptor.</returns>
        protected override IDelimitedLayoutDescriptor GetLayoutDescriptor(Type recordType) => _layoutDescriptors[recordType];

        /// <summary>
        /// Gets a line builder for a record type.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <returns>The line builder.</returns>
        protected override ILineBuilder GetLineBuilder(IDelimitedLayoutDescriptor layoutDescriptor) => _lineBuilderFactory.GetBuilder(layoutDescriptor);

        /// <summary>
        /// Gets a line parser for a record type.
        /// </summary>
        /// <param name="layoutDescriptor">The layout descriptor.</param>
        /// <returns>The line parser.</returns>
        protected override ILineParser GetLineParser(IDelimitedLayoutDescriptor layoutDescriptor) => _lineParserFactory.GetParser(layoutDescriptor);

        /// <summary>
        /// Determines the record type for a line.
        /// </summary>
        /// <param name="line">The current line.</param>
        /// <param name="lineNumber">The current line number.</param>
        /// <returns>The type of record to use for the line.</returns>
        protected override Type SelectRecordType(string line, int lineNumber) => _typeSelector(line, lineNumber);

        /// <summary>
        /// Handles a record and determines whether it is a master or detail record.
        /// </summary>
        /// <param name="record">The record to handle.</param>
        /// <returns>True if the record is a detail and false if it is a master record.</returns>
        protected override bool ProcessMasterDetail(object record) => _masterDetailStrategy.Handle(record);
    }
}
