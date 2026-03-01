using FlatFile.Delimited.Implementation;
using FlatFile.FixedLength.Implementation;
using Xunit;

namespace FlatFile.Modern.Tests;

public class LineBuilderTests
{
    private sealed class Row
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    [Fact]
    public void DelimitedLineBuilder_BuildsExpectedLine()
    {
        var layout = new DelimitedLayout<Row>()
            .WithDelimiter(";")
            .WithQuote("\"")
            .WithMember(x => x.Id)
            .WithMember(x => x.Name);

        var builder = new DelimitedLineBuilder(layout);
        var line = builder.BuildLine(new Row { Id = 10, Name = "Alice" });

        Assert.Equal("\"10\";\"Alice\"", line);
    }

    [Fact]
    public void FixedLengthLineBuilder_BuildsExpectedLine()
    {
        var layout = new FixedLayout<Row>()
            .WithMember(x => x.Id, c => c.WithLength(3).WithLeftPadding('0'))
            .WithMember(x => x.Name, c => c.WithLength(5).WithRightPadding(' '));

        var builder = new FixedLengthLineBuilder(layout);
        var line = builder.BuildLine(new Row { Id = 7, Name = "AB" });

        Assert.Equal("007AB   ", line);
    }
}
