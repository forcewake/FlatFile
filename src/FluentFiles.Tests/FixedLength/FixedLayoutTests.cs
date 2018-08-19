namespace FluentFiles.Tests.FixedLength
{
    using FluentFiles.FixedLength;
    using FluentFiles.FixedLength.Implementation;
    using FluentFiles.Tests.Base.Entities;
    using FluentAssertions;
    using Xunit;

    public class FixedLayoutTests
    {
        private readonly IFixedLayout<TestObject> layout;

        public FixedLayoutTests()
        {
            layout = new FixedLayout<TestObject>()
                    .WithMember(o => o.Id, set => set.WithLength(5).WithLeftPadding('0'))
                    .WithMember(o => o.Description, set => set.WithLength(25).WithRightPadding(' '))
                    .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull("=Null").WithLeftPadding('0'));
        }

        [Fact]
        public void FieldsCount()
        {
            layout.Fields.Should().HaveCount(3);
        }

        [Fact]
        public void FieldsCountAfterReplacementShouldNotChange()
        {
            layout.WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull(string.Empty).WithLeftPadding('0'));

            layout.Fields.Should().HaveCount(3);
        }
    }
}
