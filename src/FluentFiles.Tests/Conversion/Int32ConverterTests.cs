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
        public void Test_ConvertFromString(string input, int expected)
        {
            // Act.
            var actual = _converter.ConvertFromString(new FieldDeserializationContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(0, "0")]
        [InlineData(-1, "-1")]
        [InlineData(int.MaxValue, "2147483647")]
        [InlineData(int.MinValue, "-2147483648")]
        public void Test_ConvertToString(int input, string expected)
        {
            // Act.
            var actual = _converter.ConvertToString(new FieldSerializationContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
