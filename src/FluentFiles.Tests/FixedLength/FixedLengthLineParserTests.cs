using Xunit;

namespace FluentFiles.Tests.FixedLength
{
    using FluentFiles.Core.Conversion;
    using FluentFiles.FixedLength;
    using FluentFiles.FixedLength.Implementation;
    using FluentAssertions;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.ComponentModel;
    using FluentFiles.Core;

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

            var parsedEntity = parser.ParseLine(inputString, new TestObject());

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

            var parsedEntity = parser.ParseLine(inputString, new TestObject());

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

            var parsedEntity = parser.ParseLine("001Descr 123456700003", new TestObject());

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

            var parsedEntity = parser.ParseLine("001aaaDescr 123456700003", new TestObject());

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

            var parsedEntity = parser.ParseLine("BEEF", new TestObject());

            parsedEntity.Id.Should().Be(48879);
        }

        [Fact]
        public void ParserShouldUseConversionFunction()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(4).WithConversionFromString(s => Int32.Parse(s, NumberStyles.AllowHexSpecifier)))
                  .WithMember(o => o.Description, set => set.WithLength(25).AllowNull(string.Empty))
                  .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull(string.Empty));

            var parsedEntity = parser.ParseLine("BEEF", new TestObject());

            parsedEntity.Id.Should().Be(48879);
        }

        [Fact]
        public void ParserShouldUseTypeConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(5).WithTypeConverter(new Int32Converter()));

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("48879", entry);

            parsedEntity.Id.Should().Be(48879);
        }

        [Fact]
        public void ParserShouldUseITypeConverter()
        {
            layout.WithMember(o => o.Id, set => set.WithLength(4).WithTypeConverter(new IdHexConverter()));

            var entry = new TestObject();
            var parsedEntity = parser.ParseLine("BEEF", entry);

            parsedEntity.Id.Should().Be(48879);
        }

        [Theory]
        [InlineData("12345", 12345, '0')]
        [InlineData("01234", 1234, '0')]
        [InlineData("00123", 123, '0')]
        [InlineData("00012", 12, '0')]
        [InlineData("00001", 1, '0')]
        [InlineData("bbbb1", 1, 'b')]
        public void ShouldRespectLeftPadding(string input, int expected, char padding)
        {
            layout.WithMember(o => o.Id, set => set.WithLength(5).WithLeftPadding(padding));

            var parsedEntity = parser.ParseLine(input, new TestObject());

            parsedEntity.Id.Should().Be(expected);
        }

        [Theory]
        [InlineData("12345", 12345, '0')]
        [InlineData("12340", 1234, '0')]
        [InlineData("12300", 123, '0')]
        [InlineData("12000", 12, '0')]
        [InlineData("10000", 1, '0')]
        [InlineData("1aaaa", 1, 'a')]
        public void ShouldRespectRightPadding(string input, int expected, char padding)
        {
            layout.WithMember(o => o.Id, set => set.WithLength(5).WithRightPadding(padding));

            var parsedEntity = parser.ParseLine(input, new TestObject());

            parsedEntity.Id.Should().Be(expected);
        }

        [Theory]
        [InlineData("12345", 12345, 0)]
        [InlineData("01234", 1234, 1)]
        [InlineData("00123", 123, 2)]
        [InlineData("00012", 12, 3)]
        [InlineData("00001", 1, 4)]
        [InlineData("11111", 1, 4)]
        [InlineData("11111", 11, 3)]
        [InlineData("11111", 111, 2)]
        [InlineData("11111", 1111, 1)]
        public void ShouldRespectStartIndex(string input, int expected, ushort start)
        {
            layout.WithMember(o => o.Id, set => set.WithLength(5).StartsAt(start));

            var parsedEntity = parser.ParseLine(input, new TestObject());

            parsedEntity.Id.Should().Be(expected);
        }

        [Theory]
        [InlineData("12345", 12345, 4)]
        [InlineData("12340", 1234, 3)]
        [InlineData("12300", 123, 2)]
        [InlineData("12000", 12, 1)]
        [InlineData("10000", 1, 0)]
        [InlineData("10000", 10, 1)]
        [InlineData("10000", 100, 2)]
        [InlineData("10000", 1000, 3)]
        public void ShouldRespectEndIndex(string input, int expected, ushort end)
        {
            layout.WithMember(o => o.Id, set => set.WithLength(5).EndsAt(end));

            var parsedEntity = parser.ParseLine(input, new TestObject());

            parsedEntity.Id.Should().Be(expected);
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

            public bool CanConvertFrom(Type type) => type == typeof(string) || type == typeof(int);

            public bool CanConvertTo(Type type) => type == typeof(string) || type == typeof(int);

            public object ConvertFromString(string source) => Parse(new FieldParsingContext(source.AsSpan(), null));

            public string ConvertToString(object source) => Format(new FieldFormattingContext(source, null));
        }
#pragma warning restore CS0618 // Type or member is obsolete

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
