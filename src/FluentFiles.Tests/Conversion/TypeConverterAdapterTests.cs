using FluentFiles.Core.Conversion;
using Xunit;
using Int32Converter = System.ComponentModel.Int32Converter;

namespace FluentFiles.Tests.Conversion
{
    public class TypeConverterAdapterTests
    {
        [Fact]
        public void CanConvert()
        {
            // Arrange.
            var adapted = new Int32Converter();

            var adapter = new TypeConverterAdapter(adapted);

            // Act.
            var actual = adapter.CanConvert(from: typeof(string), to: typeof(int));

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
            Assert.Equal("1", actual.ToString());
        }
    }
}
