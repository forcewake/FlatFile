namespace FlatFile.Tests.FixedLength
{
    using FlatFile.FixedLength.Implementation;
    using FlatFile.Tests.Base.Entities;
    using FluentAssertions;
    using Xunit;

    public class FixedLayoutTests
    {
        private readonly FixedLayout<TestObject> layout;

        public FixedLayoutTests()
        {
            layout = new FixedLayout<TestObject>()
                    .WithMember(o => o.Id, set => set.WithLenght(5).WithLeftPadding('0'))
                    .WithMember(o => o.Description, set => set.WithLenght(25).WithRightPadding(' '))
                    .WithMember(o => o.NullableInt, set => set.WithLenght(5).AllowNull("=Null").WithLeftPadding('0'));
        }

        [Fact]
        public void FieldsCount()
        {
            layout.Fields.Should().HaveCount(3);
        }

        [Fact]
        public void FieldsCountAfterReplacementShouldNotChange()
        {
            layout.WithMember(o => o.NullableInt, set => set.WithLenght(5).AllowNull(string.Empty).WithLeftPadding('0'));

            layout.Fields.Should().HaveCount(3);
        }
    }
}
