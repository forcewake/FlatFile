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

    public class DelimitedLineBuilderTests
    {
        private readonly DelimitedLineBuilder builder;
        private readonly IDelimitedLayout<TestObject> layout;

        public DelimitedLineBuilderTests()
        {
            layout = new DelimitedLayout<TestObject>().WithDelimiter(",");

            builder = new DelimitedLineBuilder(layout);
        }

        [Fact]
        public void BuilderShouldDelimitFields()
        {
            layout.WithMember(o => o.Id, set => set.WithConverter<IdHexConverter>())
                  .WithMember(o => o.Description);

            var entry = new TestObject
            {
                Id = 48879,
                Description = "testing"
            };

            var line = builder.BuildLine(entry);

            line.Should().Be("BEEF,testing");
        }

        [Fact]
        public void BuilderShouldUseConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithConverter<IdHexConverter>());

            var entry = new TestObject
            {
                Id = 48879
            };

            var line = builder.BuildLine(entry);

            line.Should().Be("BEEF");
        }

        [Fact]
        public void BuilderShouldUseConversionFunction()
        {
            layout.WithMember(o => o.Id, set => set.WithConversionToString((int id) => id.ToString("X")));

            var entry = new TestObject
            {
                Id = 48879
            };

            var line = builder.BuildLine(entry);

            line.Should().Be("BEEF");
        }

        class IdHexConverter : ConverterBase<int>
        {
            protected override int ConvertFrom(in FieldDeserializationContext context)
            {
                return Int32.Parse(context.Source, NumberStyles.AllowHexSpecifier);
            }

            protected override string ConvertTo(in FieldSerializationContext<int> context)
            {
                return context.Source.ToString("X");
            }
        }
    }
}
