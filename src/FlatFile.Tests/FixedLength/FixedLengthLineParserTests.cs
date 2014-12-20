namespace FlatFile.Tests.FixedLength
{
    using FlatFile.FixedLength;
    using FlatFile.FixedLength.Implementation;
    using FlatFile.Tests.Base.Entities;
    using FluentAssertions;
    using Xunit.Extensions;

    public class FixedLengthLineParserTests
    {
        protected const string TextSource = @"
00002Description 2            00003
00003Description 3            00003
00004Description 4            00003
00005Description 5            =Null
00006Description 6            00003
00007Description 7            00003
00008Description 8            00003
00009Description 9            00003
00010Description 10           =Null";

        private readonly FixedLengthLineParser<TestObject> parser;
        private readonly IFixedLayout<TestObject> layout; 

        public FixedLengthLineParserTests()
        {
            layout = new FixedLayout<TestObject>()
                    .WithMember(o => o.Id, set => set.WithLenght(5).WithLeftPadding('0'))
                    .WithMember(o => o.Description, set => set.WithLenght(25).WithRightPadding(' '))
                    .WithMember(o => o.NullableInt, set => set.WithLenght(5).AllowNull("=Null").WithLeftPadding('0'));

            parser = new FixedLengthLineParser<TestObject>(layout);
        }

        [Theory]
        [InlineData("00001Description 1            00003", 1, "Description 1", 3)]
        [InlineData("00005Description 5            =Null", 5, "Description 5", null)]
        public void Test(string inputString, int id, string description, int? nullableInt)
        {
            var entry = new TestObject();

            var parsedEntity = parser.ParseLine(inputString, entry);

            parsedEntity.Id.Should().Be(id);
            parsedEntity.Description.Should().Be(description);
            parsedEntity.NullableInt.Should().Be(nullableInt);
        }
    }
}
