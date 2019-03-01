using FluentFiles.Converters;
using FluentFiles.Core.Conversion;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class DoubleConverterTests
    {
        private readonly DoubleConverter _converter = new DoubleConverter();

        [Theory]
        [InlineData("1.5", 1.5d)]
        [InlineData("0.5", 0.5d)]
        [InlineData("-1.5", -1.5d)]
        [InlineData("1.7976931348623157E+308", double.MaxValue)]
        [InlineData("-1.7976931348623157E+308", double.MinValue)]
        public void Test_Parse(string input, double expected)
        {
            // Act.
            var actual = _converter.Parse(new FieldParsingContext(input, null, typeof(double)));

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.5d, "1.5")]
        [InlineData(0.5d, "0.5")]
        [InlineData(-1.5d, "-1.5")]
        [InlineData(double.MaxValue, "1.7976931348623157E+308")]
        [InlineData(double.MinValue, "-1.7976931348623157E+308")]
        public void Test_Format(double input, string expected)
        {
            // Act.
            var actual = _converter.Format(new FieldFormattingContext(input, null, typeof(double)));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
