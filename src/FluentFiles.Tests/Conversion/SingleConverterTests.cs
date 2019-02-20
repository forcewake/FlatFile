using FluentFiles.Converters;
using FluentFiles.Core.Conversion;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class SingleConverterTests
    {
        private readonly SingleConverter _converter = new SingleConverter();

        [Theory]
        [InlineData("1.5", 1.5f)]
        [InlineData("0.5", 0.5f)]
        [InlineData("-1.5", -1.5f)]
        [InlineData("3.40282347E+38", float.MaxValue)]
        [InlineData("-3.40282347E+38", float.MinValue)]
        public void Test_Parse(string input, float expected)
        {
            // Act.
            var actual = _converter.Parse(new FieldParsingContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.5f, "1.5")]
        [InlineData(0.5f, "0.5")]
        [InlineData(-1.5f, "-1.5")]
        [InlineData(float.MaxValue, "3.40282347E+38")]
        [InlineData(float.MinValue, "-3.40282347E+38")]
        public void Test_Format(float input, string expected)
        {
            // Act.
            var actual = _converter.Format(new FieldFormattingContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
