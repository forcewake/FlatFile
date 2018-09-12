using FluentFiles.Core.Conversion;
using System.ComponentModel;
using Xunit;

namespace FluentFiles.Tests.Conversion
{
    public class TypeConverterAdapterTests
    {
        [Fact]
        public void CanConvertFromShouldPassThrough()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.CanConvertFrom(typeof(string));

            // Assert.
            Assert.True(actual);
        }

        [Fact]
        public void CanConvertToShouldPassThrough()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.CanConvertTo(typeof(string));

            // Assert.
            Assert.True(actual);
        }

        [Fact]
        public void ConvertFromStringShouldPassThrough()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.ConvertFromString("1", null);

            // Assert.
            Assert.Equal(1, actual);
        }

        [Fact]
        public void ConvertToStringShouldPassThrough()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.ConvertToString(1, null);

            // Assert.
            Assert.Equal("1", actual);
        }
    }
}
