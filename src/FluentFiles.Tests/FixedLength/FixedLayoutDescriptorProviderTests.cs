using FluentAssertions;
using FluentFiles.FixedLength;
using FluentFiles.FixedLength.Attributes;
using FluentFiles.FixedLength.Attributes.Infrastructure;
using Xunit;

namespace FluentFiles.Tests.FixedLength
{
    public class FixedLayoutDescriptorProviderTests
    {
        private readonly FixedLayoutDescriptorProvider _provider = new FixedLayoutDescriptorProvider();

        [Fact]
        public void ShouldProvideLayout()
        {
            // Act.
            var descriptor = _provider.GetDescriptor<TestObject>();

            // Assert.
            Assert.Equal(typeof(TestObject), descriptor.TargetType);
            descriptor.Fields.Should().BeEquivalentTo(new object[] {
                new { Index = 1, Length = 5,  PaddingChar = '0' },
                new { Index = 2, Length = 25, PaddingChar = ' ' },
                new { Index = 3, Length = 7 },
                new { Index = 4, Length = 5,  PaddingChar = '0', NullValue = "=Null"} }, c => c.ExcludingMissingMembers());
        }

        [FixedLengthFile]
        [IgnoreFixedLengthField(3, 7)]
        class TestObject
        {
            [FixedLengthField(1, 5, PaddingChar = '0')]
            public int Id { get; set; }

            [FixedLengthField(2, 25, PaddingChar = ' ', Padding = Padding.Right)]
            public string Description { get; set; }

            [FixedLengthField(4, 5, PaddingChar = '0', NullValue = "=Null")]
            public int? NullableInt { get; set; }
        }
    }
}
