using FluentFiles.Converters;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class CharConverterTests
    {
        private readonly CharConverter _converter = new CharConverter();

        [Theory]
        [InlineData("",       '\0')]
        [InlineData("a",       'a')]
        [InlineData("@",       '@')]
        [InlineData("6",       '6')]
        [InlineData("abc",     'a')]
        [InlineData("    4  ", '4')]
        [InlineData("    \t", '\0')]
        public void Test_ConvertFromString(string input, char expected)
        {
            // Act.
            var actual = _converter.ConvertFromString(input, null);

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('a', "a")]
        [InlineData('7', "7")]
        [InlineData('$', "$")]
        [InlineData('\0', "")]
        public void Test_ConvertToString(char input, string expected)
        {
            // Act.
            var actual = _converter.ConvertToString(input, null);

            // Assert.
            Assert.Equal(expected, actual.ToString());
        }
    }
}
