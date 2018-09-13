namespace FluentFiles.Tests.Delimited
{
    using FluentFiles.Core.Conversion;
    using FluentFiles.Delimited;
    using FluentFiles.Delimited.Implementation;
    using FluentFiles.Tests.Base.Entities;
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

            parsedEntity.Id.Should().Be(1);
            parsedEntity.Description.Should().Be("Description 1");
            parsedEntity.NullableInt.Should().Be(3);
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

        class IdHexConverter : ValueConverterBase<int>
        {
            protected override int ConvertFrom(ReadOnlySpan<char> source, PropertyInfo targetProperty)
            {
                return Int32.Parse(source, NumberStyles.AllowHexSpecifier);
            }

            protected override ReadOnlySpan<char> ConvertTo(int source, PropertyInfo sourceProperty)
            {
                return source.ToString("X");
            }
        }
    }
}
