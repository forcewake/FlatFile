using FluentFiles.Converters;
using FluentFiles.Core.Conversion;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class Int32ConverterTests
    {
        private readonly Int32Converter _converter = new Int32Converter();

        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("-1", -1)]
        [InlineData("2147483647", int.MaxValue)]
        [InlineData("-2147483648", int.MinValue)]
        public void Test_Parse(string input, int expected)
        {
            // Act.
            var actual = _converter.Parse(new FieldParsingContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        [InlineData(-1, "-1")]
        [InlineData(int.MaxValue, "2147483647")]
        [InlineData(int.MinValue, "-2147483648")]
        public void Test_Format(int input, string expected)
        {
            // Act.
            var actual = _converter.Format(new FieldFormattingContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
