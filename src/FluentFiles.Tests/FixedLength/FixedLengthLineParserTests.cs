using Xunit;

namespace FluentFiles.Tests.FixedLength
{
    using FluentFiles.Core.Conversion;
    using FluentFiles.FixedLength;
    using FluentFiles.FixedLength.Implementation;
    using FluentFiles.Tests.Base.Entities;
    using FluentAssertions;
    using System;
    using System.Globalization;
    using System.Reflection;

    public class FixedLengthLineParserTests
    {
        private readonly FixedLengthLineParser parser;
        private readonly IFixedLayout<TestObject> layout; 

        public FixedLengthLineParserTests()
        {
            layout = new FixedLayout<TestObject>();

            parser = new FixedLengthLineParser(layout);
        }

        [Theory]
        [InlineData("00001Description 1            00003", 1, "Description 1", 3)]
        [InlineData("00005Description 5            =Null", 5, "Description 5", null)]
        public void ParserShouldReadAnyValidString(string inputString, int id, string description, int? nullableInt)
        {
            layout.WithMember(o => o.Id, set => set.WithLength(5).WithLeftPadding('0'))
                  .WithMember(o => o.Description, set => set.WithLength(25).WithRightPadding(' '))
                  .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull("=Null").WithLeftPadding('0'));

            var entry = new TestObject();

            var parsedEntity = parser.ParseLine(inputString, entry);

            parsedEntity.Id.Should().Be(id);
            parsedEntity.Description.Should().Be(description);
            parsedEntity.NullableInt.Should().Be(nullableInt);
        }

        [Theory]
        [InlineData("00001Description 1            00003", 1, "Description 1", 3)]
        [InlineData("00005Description 5            ", 5, "Description 5", null)]
        [InlineData("00005Description 5            3", 5, "Description 5", 3)]
        public void ParserShouldSetValueNullValueIfStringIsToShort(string inputString, int id, string description, int? nullableInt)
        {
            layout.WithMember(o => o.Id, set => set.WithLength(5).WithLeftPadding('0'))
                  .WithMember(o => o.Description, set => set.WithLength(25).WithRightPadding(' '))
                  .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull("=Null").WithLeftPadding('0'));

            var entry = new TestObject();

            var parsedEntity = parser.ParseLine(inputString, entry);

            parsedEntity.Id.Should().Be(id);
            parsedEntity.Description.Should().Be(description);
            parsedEntity.NullableInt.Should().Be(nullableInt);
        }

        [Fact]
        public void ShouldIgnoreSections()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(3).WithLeftPadding('0'))
                  .WithMember(o => o.Description, set => set.WithLength(6).WithRightPadding(' '))
                  .Ignore(7)
                  .WithMember(o => o.NullableInt, set => set.WithLength(5).WithLeftPadding('0'));

            var entry = new TestObject();

            var parsedEntity = parser.ParseLine("001Descr 123456700003", entry);

            parsedEntity.Id.Should().Be(1);
            parsedEntity.Description.Should().Be("Descr");
            parsedEntity.NullableInt.Should().Be(3);
        }

        [Fact]
        public void ShouldIgnoreMultipleSections()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(3).WithLeftPadding('0'))
                  .Ignore(3)
                  .WithMember(o => o.Description, set => set.WithLength(6).WithRightPadding(' '))
                  .Ignore(7)
                  .WithMember(o => o.NullableInt, set => set.WithLength(5).WithLeftPadding('0'));

            var entry = new TestObject();

            var parsedEntity = parser.ParseLine("001aaaDescr 123456700003", entry);

            parsedEntity.Id.Should().Be(1);
            parsedEntity.Description.Should().Be("Descr");
            parsedEntity.NullableInt.Should().Be(3);
        }

        [Fact]
        public void ParserShouldUseConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(4).WithConverter<IdHexConverter>())
                  .WithMember(o => o.Description, set => set.WithLength(25).AllowNull(string.Empty))
                  .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull(string.Empty));

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("BEEF", entry);

            parsedEntity.Id.Should().Be(48879);
        }

        [Fact]
        public void ParserShouldUseConversionFunction()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(4).WithConversionFromString(s => Int32.Parse(s, NumberStyles.AllowHexSpecifier)))
                  .WithMember(o => o.Description, set => set.WithLength(25).AllowNull(string.Empty))
                  .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull(string.Empty));

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

        class TestObject : IEquatable<TestObject>
        {
            public int Id { get; set; }
            public string Description { get; set; }
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
