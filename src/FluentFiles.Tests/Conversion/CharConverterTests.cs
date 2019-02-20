using FluentFiles.Converters;
using FluentFiles.Core.Conversion;
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
        public void Test_Parse(string input, char expected)
        {
            // Act.
            var actual = _converter.Parse(new FieldParsingContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('a', "a")]
        [InlineData('7', "7")]
        [InlineData('$', "$")]
        [InlineData('\0', "")]
        public void Test_Format(char input, string expected)
        {
            // Act.
            var actual = _converter.Format(new FieldFormattingContext(input, null));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
