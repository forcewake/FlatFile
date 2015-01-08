namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthFileEngine<T> : FlatFileEngine<T, IFixedFieldSettingsContainer, ILayoutDescriptor<IFixedFieldSettingsContainer>>
        where T : class, new()
    {
        private readonly IFixedLengthLineBuilderFactory<T> lineBuilderFactory;
        private readonly IFixedLengthLineParserFactory<T> lineParserFactory;
        private readonly ILayoutDescriptor<IFixedFieldSettingsContainer> layoutDescriptor;

        internal FixedLengthFileEngine(
            ILayoutDescriptor<IFixedFieldSettingsContainer> layoutDescriptor,
            IFixedLengthLineBuilderFactory<T> lineBuilderFactory,
            IFixedLengthLineParserFactory<T> lineParserFactory,
            Func<string, Exception, bool> handleEntryReadError = null) : base(handleEntryReadError)
        {
            this.lineBuilderFactory = lineBuilderFactory;
            this.lineParserFactory = lineParserFactory;
            this.layoutDescriptor = layoutDescriptor;
        }

        protected override ILineBulder<T> LineBuilder
        {
            get { return lineBuilderFactory.GetBuilder(LayoutDescriptor); }
        }

        protected override ILineParser<T> LineParser
        {
            get { return lineParserFactory.GetParser(LayoutDescriptor); }
        }

        protected override ILayoutDescriptor<IFixedFieldSettingsContainer> LayoutDescriptor
        {
            get { return layoutDescriptor; }
        }
    }
}