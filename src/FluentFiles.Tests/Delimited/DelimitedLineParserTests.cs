namespace FluentFiles.Tests.Delimited
{
    using FluentAssertions;
    using FluentFiles.Core;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Delimited;
    using FluentFiles.Delimited.Implementation;
    using System;
    using System.ComponentModel;
    using System.Globalization;
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
                  .WithMember(o => o.Note)
                  .WithMember(o => o.NullableInt);

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("\"1\",\"Description 1\",\"test notes\",\"3\"", entry);

            parsedEntity.Should().BeEquivalentTo(
                new TestObject { Id = 1, Description = "Description 1", Note = "test notes", NullableInt = 3 });
        }

        [Theory]
        [Trait("Issue", "10")]
        [InlineData("1,,", "", "")]
        [InlineData("1,a,", "a", "")]
        [InlineData("1,,b", "", "b")]
        public void ShouldHandleEmptyFields(string line, string description, string note)
        {
            // Arrange.
            layout.WithQuote("\"")
                  .WithMember(x => x.Id)
                  .WithMember(x => x.Description)
                  .WithMember(x => x.Note);

            var entry = new TestObject();

            // Act.
            var parsedEntity = parser.ParseLine(line, entry);

            // Assert.
            parsedEntity.Should().BeEquivalentTo(new TestObject { Id = 1, Description = description, Note = note });
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

        [Fact]
        public void ParserShouldUseTypeConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithTypeConverter(new Int32Converter()));

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("48879", entry);

            parsedEntity.Id.Should().Be(48879);
        }

        [Fact]
        public void ParserShouldUseITypeConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithTypeConverter(new IdHexConverter()));

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("BEEF", entry);

            parsedEntity.Id.Should().Be(48879);
        }

#pragma warning disable CS0618 // Type or member is obsolete
        class IdHexConverter : ConverterBase<int>, ITypeConverter
        {
            protected override int ParseValue(in FieldParsingContext context)
            {
                return Int32.Parse(context.Source, NumberStyles.AllowHexSpecifier);
            }

            protected override string FormatValue(in FieldFormattingContext<int> context)
            {
                return context.Source.ToString("X");
            }

            public bool CanConvertFrom(Type type) => type == typeof(string) || type == TargetType;

            public bool CanConvertTo(Type type) => type == typeof(string) || type == TargetType;

            public object ConvertFromString(string source) => Parse(new FieldParsingContext(source.AsSpan(), null, TargetType));

            public string ConvertToString(object source) => Format(new FieldFormattingContext(source, null, TargetType));
        }
#pragma warning restore CS0618 // Type or member is obsolete

        class TestObject : IEquatable<TestObject>
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public int? NullableInt { get; set; }

            public string Note;

            public override int GetHashCode() => HashCode.Combine(Id, Description, NullableInt, Note);

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
                       Equals(NullableInt, other.NullableInt) && Equals(Note, other.Note);
            }
        }
    }
}
