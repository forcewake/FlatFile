namespace FluentFiles.Tests.FixedLength
{
    using FluentFiles.Core;
    using FluentFiles.FixedLength;
    using FluentFiles.FixedLength.Implementation;
    using FluentFiles.Tests.Base;
    using FluentFiles.Tests.Base.Entities;

    public class FixedLengthIntegrationTests :
        IntegrationTests<IFixedFieldSettingsContainer, IFixedFieldSettingsBuilder, IFixedLayout<TestObject>>
    {
        private readonly IFixedLayout<TestObject> _layout;
        private readonly IFlatFileEngine _engine;
        private const string _testSource = @"00001Description 1            00003
00002Description 2            00003
00003Description 3            00003
00004Description 4            00003
00005Description 5            =Null
00006Description 6            00003
00007Description 7            00003
00008Description 8            00003
00009Description 9            00003
00010Description 10           =Null";

        public FixedLengthIntegrationTests()
        {
            _layout = new FixedLayout<TestObject>()
                .WithMember(o => o.Id, set => set.WithLength(5).WithLeftPadding('0'))
                .WithMember(o => o.Description, set => set.WithLength(25).WithRightPadding(' '))
                .WithMember(o => o.NullableInt, set => set.WithLength(5).AllowNull("=Null").WithLeftPadding('0'));

            _engine = new FixedLengthFileEngine(Layout, new FixedLengthLineBuilderFactory(),
                new FixedLengthLineParserFactory());
        }

        protected override IFixedLayout<TestObject> Layout
        {
            get { return _layout; }
        }

        protected override IFlatFileEngine Engine
        {
            get { return _engine; }
        }

        public override string TestSource
        {
            get { return _testSource; }
        }
    }
}