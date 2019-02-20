using FakeItEasy;
using FluentFiles.Core;
using FluentFiles.Core.Conversion;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class ITypeConverterAdapterTests
    {
        [Fact]
        public void Test_CanFormat()
        {
            // Arrange.
            var adapted = A.Fake<ITypeConverter>();
            A.CallTo(() => adapted.CanConvertTo(typeof(string))).Returns(true);
            A.CallTo(() => adapted.CanConvertFrom(typeof(int))).Returns(true);

            var adapter = new ITypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.CanFormat(typeof(int));

            // Assert.
            Assert.True(actual);
        }

        [Fact]
        public void Test_CanParse()
        {
            // Arrange.
            var adapted = A.Fake<ITypeConverter>();
            A.CallTo(() => adapted.CanConvertFrom(typeof(string))).Returns(true);
            A.CallTo(() => adapted.CanConvertTo(typeof(int))).Returns(true);

            var adapter = new ITypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.CanParse(typeof(int));

            // Assert.
            Assert.True(actual);
        }

        [Fact]
        public void Test_Parse_ShouldPassThrough()
        {
            // Arrange.
            var adapted = A.Fake<ITypeConverter>();
            A.CallTo(() => adapted.ConvertFromString("1")).Returns(1);

            var adapter = new ITypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.Parse(new FieldParsingContext("1", null));

            // Assert.
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Test_Format_ShouldPassThrough()
        {
            // Arrange.
            var adapted = A.Fake<ITypeConverter>();
            A.CallTo(() => adapted.ConvertToString(1)).Returns("1");

            var adapter = new ITypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.Format(new FieldFormattingContext(1, null));

            // Assert.
            Assert.Equal("1", actual);
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}
