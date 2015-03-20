using System;

namespace FlatFile.Tests.FixedLength
{
    using FlatFile.Core;
    using FlatFile.FixedLength;
    using FlatFile.FixedLength.Attributes;
    using FlatFile.FixedLength.Implementation;
    using FlatFile.Tests.Base.Entities;

    public class FixedLengthAttriubteMappingIntegrationTests : FixedLengthIntegrationTests
    {
        private readonly IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer> _fileEngineFactory;

        public FixedLengthAttriubteMappingIntegrationTests()
        {
            _fileEngineFactory = new FixedLengthFileEngineFactory();
        }

        protected override IFlatFileEngine Engine
        {
            get { return _fileEngineFactory.GetEngine<TestObject>(); }
        }
    }
}