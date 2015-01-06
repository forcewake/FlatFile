namespace FlatFile.Delimited.Implementation
{
    using System;
    using System.IO;
    using System.Text;
    using FlatFile.Core;
    using FlatFile.Core.Base;

    public class DelimitedFileEngine<T> :
        FlatFileEngine<T, DelimitedFieldSettings, IDelimitedLayoutDescriptor>
        where T : class, new()
    {
        private readonly IDelimitedLineBuilderFactory<T> _builderFactory;

        private readonly IDelimitedLineParserFactory<T> _parserFactory;

        private readonly IDelimitedLayoutDescriptor _layoutDescriptor;

        internal DelimitedFileEngine(
            IDelimitedLayoutDescriptor layoutDescriptor,
            IDelimitedLineBuilderFactory<T> builderFactory,
            IDelimitedLineParserFactory<T> parserFactory, 
            Func<string, Exception, bool> handleEntryReadError = null)
            : base(handleEntryReadError)
        {
            _builderFactory = builderFactory;
            _parserFactory = parserFactory;
            _layoutDescriptor = layoutDescriptor;
        }

        protected override ILineBulder<T> LineBuilder
        {
            get { return _builderFactory.GetBuilder(LayoutDescriptor); }
        }

        protected override ILineParser<T> LineParser
        {
            get { return _parserFactory.GetParser(LayoutDescriptor); }
        }

        protected override IDelimitedLayoutDescriptor LayoutDescriptor
        {
            get { return _layoutDescriptor; }
        }

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