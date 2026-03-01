namespace FlatFile.Tests.FixedLength
{
    using FlatFile.Core.Base;
    using FlatFile.FixedLength;
    using FlatFile.FixedLength.Implementation;
    using FlatFile.Tests.Base.Entities;
    using FluentAssertions;
    using System;
    using System.Globalization;
    using System.Reflection;
    using Xunit;

    public class FixedLengthLineBuilderTests
    {
        private readonly FixedLengthLineBuilder builder;
        private readonly IFixedLayout<TestObject> layout;

        public FixedLengthLineBuilderTests()
        {
            layout = new FixedLayout<TestObject>();

            builder = new FixedLengthLineBuilder(layout);
        }

        [Fact]
        public void BuilderShouldUseTypeConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(4).WithTypeConverter<IdHexConverter>());

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
            layout.WithMember(o => o.Id, set => set.WithLength(4).WithConversionToString((int id) => id.ToString("X")));

            var entry = new TestObject
            {
                Id = 48879
            };

            var line = builder.BuildLine(entry);

            line.Should().Be("BEEF");
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
