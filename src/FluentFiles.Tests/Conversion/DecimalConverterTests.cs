using FluentFiles.Converters;
using FluentFiles.Core.Conversion;
using System.Collections.Generic;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class DecimalConverterTests
    {
        private readonly DecimalConverter _converter = new DecimalConverter();

        public static IEnumerable<object[]> ConvertFromStringData =>
            new TheoryData<string, decimal>
            {
                { "1.5", 1.5m },
                { "0.5", 0.5m },
                { "-1.5", -1.5m },
                { "79228162514264337593543950335", decimal.MaxValue },
                { "-79228162514264337593543950335", decimal.MinValue }
            };

        [Theory]
        [MemberData(nameof(ConvertFromStringData))]
        public void Test_Parse(string input, decimal expected)
        {
            // Act.
            var actual = _converter.Parse(new FieldParsingContext(input, null, typeof(decimal)));

            // Assert.
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> ConvertToStringData =>
            new TheoryData<decimal, string>
            {
                { 1.5m, "1.5" },
                { 0.5m, "0.5" },
                { -1.5m, "-1.5" },
                { decimal.MaxValue ,"79228162514264337593543950335" },
                { decimal.MinValue, "-79228162514264337593543950335" }
            };

        [Theory]
        [MemberData(nameof(ConvertToStringData))]
        public void Test_Format(decimal input, string expected)
        {
            // Act.
            var actual = _converter.Format(new FieldFormattingContext(input, null, typeof(decimal)));

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
