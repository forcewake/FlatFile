using FluentFiles.Converters;
using FluentFiles.Core.Conversion;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class Int64ConverterTests
    {
        private readonly Int64Converter _converter = new Int64Converter();

        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("-1", -1)]
        [InlineData("9223372036854775807", long.MaxValue)]
        [InlineData("-9223372036854775808", long.MinValue)]
        public void Test_Parse(string input, long expected)
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
        [InlineData(long.MaxValue, "9223372036854775807")]
        [InlineData(long.MinValue, "-9223372036854775808")]
        public void Test_Format(long input, string expected)
        {
            // Act.
            var actual = _converter.Format(new FieldFormattingContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
