using FluentFiles.Converters;
using System;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class GuidConverterTests
    {
        private readonly GuidConverter _converter = new GuidConverter();

        [Theory]
        [InlineData("{30B8C102-A448-40C7-8B06-21577827D653}")]
        [InlineData("5BA14058-CD58-4FE5-AAB3-0863FD6C94D8")]
        [InlineData("B6ECD6EC94B0475F940B14BFAEE0294F")]
        public void Test_ConvertFromString(string input)
        {
            // Arrange.
            var expected = Guid.Parse(input);

            // Act.
            var actual = _converter.ConvertFromString(input, null);

            // Assert.
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("{30B8C102-A448-40C7-8B06-21577827D653}", "30b8c102-a448-40c7-8b06-21577827d653")]
        [InlineData("5BA14058-CD58-4FE5-AAB3-0863FD6C94D8",   "5ba14058-cd58-4fe5-aab3-0863fd6c94d8")]
        [InlineData("B6ECD6EC94B0475F940B14BFAEE0294F",       "b6ecd6ec-94b0-475f-940b-14bfaee0294f")]
        public void Test_ConvertToString(string input, string expected)
        {
            // Arrange.
            var guid = Guid.Parse(input);

            // Act.
            var actual = _converter.ConvertToString(guid, null);

            // Assert.
            Assert.Equal(expected, actual.ToString());
        }
    }
}
