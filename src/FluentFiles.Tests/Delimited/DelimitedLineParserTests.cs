namespace FluentFiles.Tests.Delimited
{
    using FluentFiles.Core.Base;
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
        public void ParserShouldUseTypeConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithTypeConverter<IdHexConverter>());

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

        class IdHexConverter : TypeConverterBase<int>
        {
            protected override int ConvertFrom(string source, PropertyInfo targetProperty)
            {
                return Int32.Parse(source, NumberStyles.AllowHexSpecifier);
            }

            protected override string ConvertTo(int source, PropertyInfo sourceProperty)
            {
                return source.ToString("X");
            }
        }
    }
}
