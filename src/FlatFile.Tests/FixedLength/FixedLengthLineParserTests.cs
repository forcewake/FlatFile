namespace FlatFile.Tests.FixedLength
{
    using FlatFile.FixedLength;
    using FlatFile.FixedLength.Implementation;
    using FlatFile.Tests.Base.Entities;
    using FluentAssertions;
    using Xunit.Extensions;

    public class FixedLengthLineParserTests
    {
        private readonly FixedLengthLineParser parser;
        private readonly IFixedLayout<TestObject> layout; 

        public FixedLengthLineParserTests()
        {
            layout = new FixedLayout<TestObject>()
                    .WithMember(o => o.Id, set => set.WithLength(5).WithLeftPadding('0'))
                    .WithMember(o => o.Description, set => set.WithLength(25).WithRightPadding(' '))
                    .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull("=Null").WithLeftPadding('0'));

            parser = new FixedLengthLineParser(layout);
        }

        [Theory]
        [InlineData("00001Description 1            00003", 1, "Description 1", 3)]
        [InlineData("00005Description 5            =Null", 5, "Description 5", null)]
        public void ParserShouldReadAnyValidString(string inputString, int id, string description, int? nullableInt)
        {
            var entry = new TestObject();

            var parsedEntity = parser.ParseLine(inputString, entry);

            parsedEntity.Id.Should().Be(id);
            parsedEntity.Description.Should().Be(description);
            parsedEntity.NullableInt.Should().Be(nullableInt);
        }
    }
}
