namespace FlatFile.Tests.Delimited
{
    using FlatFile.Core;
    using FlatFile.Delimited;
    using FlatFile.Delimited.Attributes;
    using FlatFile.Delimited.Implementation;
    using FlatFile.Tests.Base.Entities;

    public class DelimitedAttriubteMappingIntegrationTests : DelimitedIntegrationTests
    {
        private readonly IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer> _fileEngineFactory;

        public DelimitedAttriubteMappingIntegrationTests()
        {
            _fileEngineFactory = new DelimitedFileEngineFactory();
        }

        protected override IFlatFileEngine<TestObject> Engine
        {
            get { return _fileEngineFactory.GetEngine<TestObject>(); }
        }
    }
}