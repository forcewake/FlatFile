using FluentFiles.Delimited.Implementation;
using FluentFiles.Tests.Base.Entities;

namespace FluentFiles.Tests.Delimited
{
    public sealed class DelimitedWithHeaderIntegrationTests : DelimitedIntegrationTests
    {
        private const string _testSource = "\"Id\";\"Description\";\"NullableInt\"\r\n" +
                                           "\"1\";\"Description 1\";\"3\"\r\n" +
                                           "\"2\";\"Description 2\";\"3\"\r\n" +
                                           "\"3\";\"Description 3\";\"3\"\r\n" +
                                           "\"4\";\"Description 4\";\"3\"\r\n" +
                                           "\"5\";\"Description 5\";=Null\r\n" +
                                           "\"6\";\"Description 6\";\"3\"\r\n" +
                                           "\"7\";\"Description 7\";\"3\"\r\n" +
                                           "\"8\";\"Description 8\";\"3\"\r\n" +
                                           "\"9\";\"Description 9\";\"3\"\r\n" +
                                           "\"10\";\"Description 10\";=Null";

        public DelimitedWithHeaderIntegrationTests()
        {
            _layout = new DelimitedLayout<TestObject>()
                .WithHeader()
                .WithDelimiter(";")
                .WithQuote("\"")
                .WithMember(o => o.Id)
                .WithMember(o => o.Description)
                .WithMember(o => o.NullableInt, set => set.AllowNull("=Null"));
        }

        public override string TestSource
        {
            get { return _testSource; }
        }
    }
}