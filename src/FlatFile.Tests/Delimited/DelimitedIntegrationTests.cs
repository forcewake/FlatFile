namespace FlatFile.Tests.Delimited
{
    using System;
    using System.IO;
    using FlatFile.Core;
    using FlatFile.Delimited;
    using FlatFile.Delimited.Implementation;
    using FlatFile.Tests.Base;
    using FlatFile.Tests.Base.Entities;

    public class DelimitedIntegrationTests :
        IntegrationTests<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor, IDelimitedLayout<TestObject>>
    {
        private DelimitedFileEngine<TestObject> _flatFileEngine;
        private readonly IDelimitedLayout<TestObject> _layout;

        private readonly Func<Stream, IFlatFileEngine<TestObject>> _engine;

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

            _engine = stream =>
            {
                _flatFileEngine = new DelimitedFileEngine<TestObject>(
                    Layout,
                    new DelimitedLineBuilderFactory<TestObject>(), 
                    new DelimitedLineParserFactory<TestObject>());
                return _flatFileEngine;
            };
        }

        protected override IDelimitedLayout<TestObject> Layout
        {
            get { return _layout; }
        }

        protected override Func<Stream, IFlatFileEngine<TestObject>> Engine
        {
            get { return _engine; }
        }

        public override string TestSource
        {
            get { return _testSource; }
        }
    }
}