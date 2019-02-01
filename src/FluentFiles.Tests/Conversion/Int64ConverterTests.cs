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
        public void Test_ConvertFromString(string input, long expected)
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
        [InlineData(long.MaxValue, "9223372036854775807")]
        [InlineData(long.MinValue, "-9223372036854775808")]
        public void Test_ConvertToString(long input, string expected)
        {
            // Act.
            var actual = _converter.ConvertToString(new FieldSerializationContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
