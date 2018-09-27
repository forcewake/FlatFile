using FluentFiles.Converters;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class ByteConverterTests
    {
        private readonly ByteConverter _converter = new ByteConverter();

        [Theory]
        [InlineData("1", 1)]
        [InlineData("0", 0)]
        [InlineData("255", byte.MaxValue)]
        [InlineData("0", byte.MinValue)]
        public void Test_ConvertFromString(string input, byte expected)
        {
            // Act.
            var actual = _converter.ConvertFromString(input, null);

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        [InlineData(byte.MaxValue, "255")]
        [InlineData(byte.MinValue, "0")]
        public void Test_ConvertToString(byte input, string expected)
        {
            // Act.
            var actual = _converter.ConvertToString(input, null);

            // Assert.
            Assert.Equal(expected, actual.ToString());
        }
    }
}
