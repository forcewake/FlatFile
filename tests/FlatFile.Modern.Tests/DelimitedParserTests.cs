using FlatFile.Delimited.Implementation;
using Xunit;

namespace FlatFile.Modern.Tests;

public class DelimitedParserTests
{
    private sealed class Row
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    [Fact]
    public void ParseLine_ParsesQuotedDelimitedRow()
    {
        var layout = new DelimitedLayout<Row>()
            .WithDelimiter(";")
            .WithQuote("\"")
            .WithMember(x => x.Id)
            .WithMember(x => x.Name);

        var parser = new DelimitedLineParser(layout);
        var row = parser.ParseLine("\"1\";\"Alice\"", new Row());

        Assert.Equal(1, row.Id);
        Assert.Equal("Alice", row.Name);
    }

    [Fact]
    public void ParseLine_UnclosedQuote_DoesNotThrowAndParsesAvailableData()
    {
        var layout = new DelimitedLayout<Row>()
            .WithDelimiter(";")
            .WithQuote("\"")
            .WithMember(x => x.Id)
            .WithMember(x => x.Name);

        var parser = new DelimitedLineParser(layout);
        var row = parser.ParseLine("\"2;\"Bob\"", new Row());

        Assert.Equal(2, row.Id);
        Assert.Equal("Bob", row.Name);
    }
}
