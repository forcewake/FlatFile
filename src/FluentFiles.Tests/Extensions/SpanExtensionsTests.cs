using System;
using FluentFiles.Core.Extensions;
using Xunit;

namespace FluentFiles.Tests.Extensions
{
    public class SpanExtensionsTests
    {
        [Theory]
        [InlineData("123456", "3", 0,  2)]
        [InlineData("123456", "3", 2,  2)]
        [InlineData("123456", "3", 3, -1)]
        [InlineData("123456", "7", 0, -1)]
        [InlineData("123456", "4", 2,  3)]
        public void Test_IndexOf_With_StartIndex(string source, string value, int startIndex, int expected)
        {
            // Act.
            var actual = source.AsSpan().IndexOf(value.AsSpan(), startIndex, StringComparison.OrdinalIgnoreCase);

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
