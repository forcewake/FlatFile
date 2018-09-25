using FluentFiles.Converters;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class Int16ConverterTests
    {
        private readonly Int16Converter _converter = new Int16Converter();

        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("-1", -1)]
        [InlineData("32767", short.MaxValue)]
        [InlineData("-32768", short.MinValue)]
        public void Test_ConvertFromString(string input, short expected)
        {
            // Act.
            var actual = _converter.ConvertFromString(input, null);

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        [InlineData(-1, "-1")]
        [InlineData(short.MaxValue, "32767")]
        [InlineData(short.MinValue, "-32768")]
        public void Test_ConvertToString(short input, string expected)
        {
            // Act.
            var actual = _converter.ConvertToString(input, null);

            // Assert.
            Assert.Equal(expected, actual.ToString());
        }
    }
}
