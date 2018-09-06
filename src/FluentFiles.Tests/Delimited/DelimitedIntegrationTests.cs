namespace FluentFiles.Tests.Delimited
{
    using FluentFiles.Core;
    using FluentFiles.Delimited;
    using FluentFiles.Delimited.Implementation;
    using FluentFiles.Tests.Base;
    using FluentFiles.Tests.Base.Entities;

    public class DelimitedIntegrationTests :
        IntegrationTests<IDelimitedFieldSettingsContainer, IDelimitedFieldSettingsBuilder, IDelimitedLayout<TestObject>>
    {
        private readonly IDelimitedLayout<TestObject> _layout;

        private readonly IFlatFileEngine _engine;

        private const string _testSource = "\"1\";\"Description 1\";\"3\"\r\n" +
                                           "\"2\";\"Description 2\";\"3\"\r\n" +
                                           "\"3\";\"Description 3\";\"3\"\r\n" +
                                           "\"4\";\"Description 4\";\"3\"\r\n" +
                                           "\"5\";\"Description 5\";=Null\r\n" +
                                           "\"6\";\"Description 6\";\"3\"\r\n" +
                                           "\"7\";\"Description 7\";\"3\"\r\n" +
                                           "\"8\";\"Description 8\";\"3\"\r\n" +
                                           "\"9\";\"Description 9\";\"3\"\r\n" +
                                           "\"10\";\"Description 10\";=Null";

        public DelimitedIntegrationTests()
        {
            _layout = new DelimitedLayout<TestObject>()
                .WithDelimiter(";")
                .WithQuote("\"")
                .WithMember(o => o.Id)
                .WithMember(o => o.Description)
                .WithMember(o => o.NullableInt, set => set.AllowNull("=Null"));

            _engine = new DelimitedFileEngine(
                Layout,
                new DelimitedLineBuilderFactory(),
                new DelimitedLineParserFactory());
        }

        protected override IDelimitedLayout<TestObject> Layout
        {
            get { return _layout; }
        }

        protected override IFlatFileEngine Engine
        {
            get { return _engine; }
        }

        public override string TestSource
        {
            get { return _testSource; }
        }
    }
}