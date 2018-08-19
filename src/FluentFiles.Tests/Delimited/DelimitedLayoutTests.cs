namespace FluentFiles.Tests.Delimited
{
    using FluentFiles.Delimited;
    using FluentFiles.Delimited.Implementation;
    using FluentFiles.Tests.Base.Entities;
    using FluentAssertions;
    using Xunit;

    public class DelimitedLayoutTests
    {
        private readonly IDelimitedLayout<TestObject> layout;

        public DelimitedLayoutTests()
        {
            layout = new DelimitedLayout<TestObject>()
                    .WithDelimiter(";")
                    .WithQuote("\"")
                    .WithMember(o => o.Id)
                    .WithMember(o => o.Description)
                    .WithMember(o => o.NullableInt, set => set.AllowNull("=Null"));
        }

        [Fact]
        public void FieldsCount()
        {
            layout.Fields.Should().HaveCount(3);
        }

        [Fact]
        public void WithHeader()
        {
            layout.WithHeader();

            layout.HasHeader.Should().BeTrue();
        }

        [Fact]
        public void WithoutHeader()
        {
            layout.HasHeader.Should().BeFalse();
        }

        [Fact]
        public void FieldsCountAfterReplacementShouldNotChange()
        {
            layout.WithMember(o => o.NullableInt, set => set.AllowNull(string.Empty));

            layout.Fields.Should().HaveCount(3);
        }
    }
}
