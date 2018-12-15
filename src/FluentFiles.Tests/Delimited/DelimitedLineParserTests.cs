namespace FluentFiles.Tests.Delimited
{
    using FluentFiles.Core.Conversion;
    using FluentFiles.Delimited;
    using FluentFiles.Delimited.Implementation;
    using FluentAssertions;
    using System;
    using System.Globalization;
    using System.Reflection;
    using Xunit;

    public class DelimitedLineParserTests
    {
        private readonly DelimitedLineParser parser;
        private readonly IDelimitedLayout<TestObject> layout;

        public DelimitedLineParserTests()
        {
            layout = new DelimitedLayout<TestObject>().WithDelimiter(",");

            parser = new DelimitedLineParser(layout);
        }

        [Fact]
        public void ParserShouldParseAllFields()
        {
            layout.WithQuote("\"")
                  .WithMember(o => o.Id)
                  .WithMember(o => o.Description)
                  .WithMember(o => o.NullableInt);

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("\"1\",\"Description 1\",\"3\"", entry);

            parsedEntity.Should().BeEquivalentTo(new TestObject { Id = 1, Description = "Description 1", NullableInt = 3 });
        }

        [Theory]
        [Trait("Issue", "10")]
        [InlineData("1,,", "", "")]
        [InlineData("1,a,", "a", "")]
        [InlineData("1,,b", "", "b")]
        public void ShouldHandleEmptyFields(string line, string description, string name)
        {
            // Arrange.
            layout.WithQuote("\"")
                  .WithMember(x => x.Id)
                  .WithMember(x => x.Description)
                  .WithMember(x => x.Name);

            var entry = new TestObject();

            // Act.
            var parsedEntity = parser.ParseLine(line, entry);

            // Assert.
            parsedEntity.Should().BeEquivalentTo(new TestObject { Id = 1, Description = description, Name = name });
        }

        [Fact]
        public void ParserShouldUseConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithConverter<IdHexConverter>());

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("BEEF", entry);

            parsedEntity.Id.Should().Be(48879);
        }

        [Fact]
        public void ParserShouldUseConversionFunction()
        {
            layout.WithMember(o => o.Id, set => set.WithConversionFromString(s => Int32.Parse(s, NumberStyles.AllowHexSpecifier)));

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("BEEF", entry);

            parsedEntity.Id.Should().Be(48879);
        }

        class IdHexConverter : ConverterBase<int>
        {
            protected override int ConvertFrom(ReadOnlySpan<char> source, PropertyInfo targetProperty)
            {
                return Int32.Parse(source, NumberStyles.AllowHexSpecifier);
            }

            protected override string ConvertTo(int source, PropertyInfo sourceProperty)
            {
                return source.ToString("X");
            }
        }

        class TestObject : IEquatable<TestObject>
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public int? NullableInt { get; set; }

            public int GetHashCode(TestObject obj)
            {
                var idHash = Id.GetHashCode();
                var descriptionHash = Object.ReferenceEquals(Description, null) ? 0 : Description.GetHashCode();
                var nullableIntHash = !NullableInt.HasValue ? 0 : NullableInt.Value.GetHashCode();
                return idHash ^ descriptionHash ^ nullableIntHash;
            }

            public bool Equals(TestObject other)
            {
                if (ReferenceEquals(other, null))
                {
                    return false;
                }

                if (ReferenceEquals(other, this))
                {
                    return true;
                }

                return Equals(Id, other.Id) && Equals(Description, other.Description) &&
                       Equals(NullableInt, other.NullableInt);
            }
        }
    }
}
