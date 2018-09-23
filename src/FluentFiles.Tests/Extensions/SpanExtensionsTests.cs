using System;
using System.Collections.Generic;
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

        [Theory]
        [InlineData("", ',')]
        [InlineData(",", ',')]
        [InlineData(",,,,", ',')]
        [InlineData(",a,a,a,a", ',')]
        [InlineData("b,b,b,b,", ',')]
        [InlineData(",,,,,a", ',')]
        [InlineData("a,,,,,", ',')]
        [InlineData("11111,2222,3333,444", ',')]
        public void Test_Split(string source, char separator)
        {
            // Act.
            var items = source.AsSpan().Split(separator);

            var parts = new List<string>();
            foreach (var item in items)
                parts.Add(item.ToString());

            // Assert.
            Assert.Equal(source.Split(separator), parts);
        }
    }
}
