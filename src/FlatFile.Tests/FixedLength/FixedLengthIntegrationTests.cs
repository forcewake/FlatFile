namespace FlatFile.Tests.FixedLength
{
    using System;
    using System.IO;
    using FlatFile.Core;
    using FlatFile.FixedLength;
    using FlatFile.FixedLength.Implementation;
    using FlatFile.Tests.Base;
    using FlatFile.Tests.Base.Entities;

    public class FixedLengthIntegrationTests :
        IntegrationTests<FixedFieldSettings, IFixedFieldSettingsConstructor, IFixedLayout<TestObject>>
    {
        private FixedLengthFileEngine<TestObject> _flatFileEngine;
        private readonly IFixedLayout<TestObject> _layout;
        private readonly Func<Stream, IFlatFileEngine<TestObject, IFixedLayout<TestObject>, FixedFieldSettings, IFixedFieldSettingsConstructor>> _engine;
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
                .WithMember(o => o.Id, set => set.WithLenght(5).WithLeftPadding('0'))
                .WithMember(o => o.Description, set => set.WithLenght(25).WithRightPadding(' '))
                .WithMember(o => o.NullableInt, set => set.WithLenght(5).AllowNull("=Null").WithLeftPadding('0'));

            _engine = stream =>
            {
                _flatFileEngine = new FixedLengthFileEngine<TestObject>();

                return _flatFileEngine;
            };
        }

        protected override IFixedLayout<TestObject> Layout
        {
            get { return _layout; }
        }

        protected override Func<Stream, IFlatFileEngine<TestObject, IFixedLayout<TestObject>, FixedFieldSettings, IFixedFieldSettingsConstructor>> Engine
        {
            get { return _engine; }
        }

        public override string TestSource
        {
            get { return _testSource; }
        }
    }
}