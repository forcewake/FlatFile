namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthFileEngine : FlatFileEngine<IFixedFieldSettingsContainer, ILayoutDescriptor<IFixedFieldSettingsContainer>>
    {
        private readonly IFixedLengthLineBuilderFactory lineBuilderFactory;
        private readonly IFixedLengthLineParserFactory lineParserFactory;
        private readonly ILayoutDescriptor<IFixedFieldSettingsContainer> layoutDescriptor;

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

        protected override ILineBulder LineBuilder
        {
            get { return lineBuilderFactory.GetBuilder(LayoutDescriptor); }
        }

        protected override ILineParser LineParser
        {
            get { return lineParserFactory.GetParser(LayoutDescriptor); }
        }

        protected override ILayoutDescriptor<IFixedFieldSettingsContainer> LayoutDescriptor
        {
            get { return layoutDescriptor; }
        }
    }
}