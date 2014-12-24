namespace FlatFile.FixedLength.Implementation
{
    using System;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class FixedLengthFileEngine<T> :
        FlatFileEngine
            <T, IFixedLayout<T>, FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLengthLineBuilder<T>,
                IFixedLengthLineParser<T>>,
        IFixedLengthFileEngine<T> where T : class, new()
    {
        private readonly IFixedLengthLineBuilderFactory<T> builderFactory;

        private readonly IFixedLengthLineParserFactory<T> parserFactory;

        public FixedLengthFileEngine(Func<string, Exception, bool> handleEntryReadError = null)
            : this(new FixedLengthLineBuilderFactory<T>(), new FixedLengthLineParserFactory<T>(), handleEntryReadError)
        {
        }

        public FixedLengthFileEngine(IFixedLengthLineBuilderFactory<T> builderFactory,
            IFixedLengthLineParserFactory<T> parserFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            : base(handleEntryReadError)
        {
            this.builderFactory = builderFactory;
            this.parserFactory = parserFactory;
        }

        protected override
            ILineBuilderFactory
                <T, IFixedLengthLineBuilder<T>, IFixedLayout<T>, FixedFieldSettings, IFixedFieldSettingsConstructor>
            BuilderFactory
        {
            get { return builderFactory; }
        }

        protected override
            ILineParserFactory
                <T, IFixedLengthLineParser<T>, IFixedLayout<T>, FixedFieldSettings, IFixedFieldSettingsConstructor>
            ParserFactory
        {
            get { return parserFactory; }
        }
    }
}