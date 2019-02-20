using FluentFiles.Core.Conversion;
using Xunit;
using Int32Converter = System.ComponentModel.Int32Converter;

namespace FluentFiles.Tests.Conversion
{
    public class TypeConverterAdapterTests
    {
        [Fact]
        public void Test_CanFormat()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.CanFormat(typeof(int));

            // Assert.
            Assert.True(actual);
        }

        [Fact]
        public void Test_CanParse()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.CanParse(typeof(int));

            // Assert.
            Assert.True(actual);
        }

        [Fact]
        public void Test_Parse_ShouldPassThrough()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.Parse(new FieldParsingContext("1", null));

            // Assert.
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Test_Format_ShouldPassThrough()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.Format(new FieldFormattingContext(1, null));

            // Assert.
            Assert.Equal("1", actual);
        }
    }
}
