namespace FlatFile.Delimited.Implementation
{
    using System;
    using System.IO;
    using System.Text;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class DelimitedFileEngine<T> :
        FlatFileEngine
            <T, IDelimitedLayout<T>, DelimitedFieldSettings, IDelimitedFieldSettingsConstructor,
                IDelimitedLineBuilder<T>, IDelimitedLineParser<T>>,
        IDelimitedFileEngine<T> where T : class, new()
    {
        private readonly IDelimitedLineBuilderFactory<T> builderFactory;

        private readonly IDelimitedLineParserFactory<T> parserFactory;

        public DelimitedFileEngine(Func<string, Exception, bool> handleEntryReadError = null)
            : this(new DelimitedLineBuilderFactory<T>(), new DelimitedLineParserFactory<T>(), handleEntryReadError)
        {
        }

        public DelimitedFileEngine(IDelimitedLineBuilderFactory<T> builderFactory,
            IDelimitedLineParserFactory<T> parserFactory,
            Func<string, Exception, bool> handleEntryReadError = null)
            : base(handleEntryReadError)
        {
            this.builderFactory = builderFactory;
            this.parserFactory = parserFactory;
        }

        protected override
            ILineBuilderFactory
                <T, IDelimitedLineBuilder<T>, IDelimitedLayout<T>, DelimitedFieldSettings,
                    IDelimitedFieldSettingsConstructor> BuilderFactory
        {
            get { return builderFactory; }
        }

        protected override
            ILineParserFactory
                <T, IDelimitedLineParser<T>, IDelimitedLayout<T>, DelimitedFieldSettings,
                    IDelimitedFieldSettingsConstructor> ParserFactory
        {
            get { return parserFactory; }
        }

        protected override void WriteHeader(IDelimitedLayout<T> layout, TextWriter writer)
        {
            if (!layout.HasHeader)
            {
                return;
            }

            var fields = layout.Fields;

            var stringBuilder = new StringBuilder();

            foreach (var field in fields)
            {
                stringBuilder
                    .Append(field.Name)
                    .Append(layout.Delimiter);

            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            writer.WriteLine(stringBuilder.ToString());
        }
    }
}