using System.IO;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using FluentFiles.Core.Base;
using FluentFiles.Core;
using FluentFiles.Delimited;
using FluentFiles.Delimited.Attributes;
using FluentFiles.Delimited.Implementation;
using FluentFiles.Tests.Base.Entities;
using Xunit;
using System;
using FluentFiles.Core.Conversion;

namespace FluentFiles.Tests.Delimited
{
    public class DelimitedAttributeMappingIntegrationTests : DelimitedIntegrationTests
    {
        private readonly IFlatFileEngineFactory<IDelimitedLayoutDescriptor, IDelimitedFieldSettingsContainer> _fileEngineFactory;

        public DelimitedAttributeMappingIntegrationTests()
        {
            _fileEngineFactory = new DelimitedFileEngineFactory();
        }

        protected override IFlatFileEngine Engine
        {
            get { return _fileEngineFactory.GetEngine<TestObject>(); }
        }

        class ConverterTestObject
        {
            public string Foo { get; set; }
        }

        class StubConverter : IValueConverter
        {
            public bool CanConvertFrom(Type type) => true;
            public bool CanConvertTo(Type type) => true;
            public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => "foo";
            public ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty) => source.ToString();
        }

        [Fact]
        public void EngineShouldCallTypeConverterWhenConverterAttributeIsPresent()
        {
            // a converter to convert "A" to "foo"
            var converter = new StubConverter();

            // an attribute to assign the property
            var attribute = A.Fake<IDelimitedFieldSettings>();
            A.CallTo(() => attribute.Index).Returns(1);
            A.CallTo(() => attribute.TypeConverter).Returns(converter);

            // the properties of the class
            var properties = typeof(ConverterTestObject).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(info => info.Name);

            // assign the attribute to the Foo property
            var container = new FieldsContainer<IDelimitedFieldSettingsContainer>();
            container.AddOrUpdate(properties["Foo"], new DelimitedFieldSettings(properties["Foo"], attribute));

            var layout = new DelimitedLayout<ConverterTestObject>(new DelimitedFieldSettingsBuilderFactory(), container);
            var engine = _fileEngineFactory.GetEngine(layout);

            // write "A" to the stream and verify it is converted to "foo"
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("A");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                // Capture first result to force enumerable to be iterated
                var result = engine.Read<ConverterTestObject>(stream).FirstOrDefault();
                Assert.Equal("foo", result.Foo);
            }
        }
    }
}