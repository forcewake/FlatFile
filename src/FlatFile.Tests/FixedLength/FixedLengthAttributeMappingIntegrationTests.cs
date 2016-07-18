using System.IO;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using FlatFile.Core;
using FlatFile.Core.Base;
using FlatFile.FixedLength;
using FlatFile.FixedLength.Attributes;
using FlatFile.FixedLength.Implementation;
using FlatFile.Tests.Base.Entities;
using Xunit;

namespace FlatFile.Tests.FixedLength
{
    public class FixedLengthAttributeMappingIntegrationTests : FixedLengthIntegrationTests
    {
        readonly IFlatFileEngineFactory<ILayoutDescriptor<IFixedFieldSettingsContainer>, IFixedFieldSettingsContainer> fileEngineFactory;

        class ConverterTestObject
        {
            public string Foo { get; set; }
        }

        public FixedLengthAttributeMappingIntegrationTests() { fileEngineFactory = new FixedLengthFileEngineFactory(); }

        [Fact]
        public void EngineShouldCallTypeConverterWhenConverterAttributeIsPresent()
        {
            var converter = A.Fake<ITypeConverter>();
            A.CallTo(converter).WithReturnType<object>().Returns("foo");
            A.CallTo(converter).WithReturnType<bool>().Returns(true);

            var attribute = A.Fake<IFixedFieldSettings>();
            A.CallTo(() => attribute.Index).Returns(1);
            A.CallTo(() => attribute.Length).Returns(1);
            A.CallTo(() => attribute.TypeConverter).Returns(converter);

            var properties = typeof (ConverterTestObject).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(info => info.Name);

            var container = new FieldsContainer<IFixedFieldSettingsContainer>();
            container.AddOrUpdate(properties["Foo"], new FixedFieldSettings(properties["Foo"], attribute));

            var descriptor = new LayoutDescriptorBase<IFixedFieldSettingsContainer>(container) {HasHeader = false};

            var engine = fileEngineFactory.GetEngine(descriptor);

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("A");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                // Capture first result to force enumerable to be iterated
                var result = engine.Read<ConverterTestObject>(stream).FirstOrDefault();
            }

            A.CallTo(converter).WithReturnType<object>().MustHaveHappened();
        }

        protected override IFlatFileEngine Engine { get { return fileEngineFactory.GetEngine<TestObject>(); } }
    }
}