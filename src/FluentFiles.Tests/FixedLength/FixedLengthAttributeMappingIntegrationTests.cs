using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using FluentFiles.Core;
using FluentFiles.Core.Base;
using FluentFiles.Core.Conversion;
using FluentFiles.FixedLength;
using FluentFiles.FixedLength.Attributes;
using FluentFiles.FixedLength.Implementation;
using FluentFiles.Tests.Base.Entities;
using Xunit;

namespace FluentFiles.Tests.FixedLength
{
    public class FixedLengthAttributeMappingIntegrationTests : FixedLengthIntegrationTests
    {
        readonly IFlatFileEngineFactory<IFixedLengthLayoutDescriptor, IFixedFieldSettingsContainer> fileEngineFactory;

        class ConverterTestObject
        {
            public string Foo { get; set; }
        }

        class StubConverter : IValueConverter
        {
            public bool CanConvert(Type from, Type to) => true;
            public object ConvertFromString(ReadOnlySpan<char> source, PropertyInfo targetProperty) => "foo";
            public ReadOnlySpan<char> ConvertToString(object source, PropertyInfo sourceProperty) => source.ToString();
        }

        public FixedLengthAttributeMappingIntegrationTests()
        {
            fileEngineFactory = new FixedLengthFileEngineFactory();
        }

        [Fact]
        public void EngineShouldCallTypeConverterWhenConverterAttributeIsPresent()
        {
            var converter = new StubConverter();

            var attribute = A.Fake<IFixedFieldSettings>();
            A.CallTo(() => attribute.Index).Returns(1);
            A.CallTo(() => attribute.Length).Returns(1);
            A.CallTo(() => attribute.TypeConverter).Returns(converter);

            var properties = typeof (ConverterTestObject).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(info => info.Name);

            var container = new FieldsContainer<IFixedFieldSettingsContainer>();
            container.AddOrUpdate(properties["Foo"], new FixedFieldSettings(properties["Foo"], attribute));

            var descriptor = new FixedLayout<ConverterTestObject>(new FixedFieldSettingsBuilderFactory(), container) {HasHeader = false};

            var engine = fileEngineFactory.GetEngine(descriptor);

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("A");
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                // Capture first result to force enumerable to be iterated
                var result = engine.Read<ConverterTestObject>(stream).FirstOrDefault();

                result.Foo.Should().Be("foo");
            }
        }

        protected override IFlatFileEngine Engine { get { return fileEngineFactory.GetEngine<TestObject>(); } }
    }
}