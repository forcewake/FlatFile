namespace FlatFile.Tests.Specifications.Defenitions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using FlatFile.Core;
    using FlatFile.Core.Base;
    using FlatFile.FixedLength;
    using FlatFile.FixedLength.Implementation;
    using FlatFile.Tests.Base.Entities;
    using FlatFile.Tests.Specifications.Entities;
    using FlatFile.Tests.Specifications.Extensions;
    using FluentAssertions;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;

    [Binding]
    public class FixedLengthFileDefinitions
    {
        [Given(@"I have specification for '(.*)' fixed-length type")]
        public void GivenIHaveSpecificationForType(string type, Table table)
        {
            var targetType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.FullName.EndsWith(type));

            var properties = targetType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(info => info.Name);

            var typeMappings = table.CreateSet<FixedLengthTypeMapping>().ToArray();

            var container = new FieldsContainer<IFixedFieldSettingsContainer>();

            foreach (var typeMapping in typeMappings)
            {
                var propertyInfo = properties[typeMapping.Name];

                var settings = new FixedFieldSettingsConstructor(propertyInfo);

                settings.WithLength(typeMapping.Length);

                if (!string.IsNullOrEmpty(typeMapping.NullValue))
                {
                    settings.AllowNull(typeMapping.NullValue);
                }

                switch (typeMapping.Padding)
                {
                    case Padding.Right:
                        settings.WithRightPadding(typeMapping.PaddingCharElement);
                        break;
                    case Padding.Left:
                        settings.WithLeftPadding(typeMapping.PaddingCharElement);
                        break;
                }

                container.AddOrUpdate(propertyInfo, settings);
            }

            var descriptor = new LayoutDescriptorBase<IFixedFieldSettingsContainer>(container)
            {
                HasHeader = false
            };

            ScenarioContext.Current.Add(() => descriptor, descriptor);

        }

        [Given(@"I have several entities")]
        public void GivenIHaveSeveralEntities(IEnumerable<TestObject> testObjects)
        {
            var objects = testObjects.ToArray();
            ScenarioContext.Current.Add("testObjects", objects);
        }

        [When(@"I convert entities to the fixed-length format")]
        public void WhenIConvertEntitiesToTheFlatFormat()
        {
            var fileEngineFactory = new FixedLengthFileEngineFactory();

            var descriptor = ScenarioContext.Current.Get<ILayoutDescriptor<IFixedFieldSettingsContainer>>("descriptor");

            var fileEngine = fileEngineFactory.GetEngine<TestObject>(descriptor);

            var testObjects = ScenarioContext.Current.Get<TestObject[]>("testObjects");

            var fileContent = fileEngine.WriteToString(testObjects);

            ScenarioContext.Current.Add(() => fileContent, fileContent);
        }

        [Then(@"^the result should be$")]
        public void ThenTheResultShouldBe(string multilineText)
        {
            var strings = multilineText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            multilineText = string.Join(Environment.NewLine, strings);

            string fileContent = string.Empty;

            ScenarioContext.Current.TryGetValue(() => fileContent, out fileContent);

            fileContent.Should().Be(multilineText);
        }
    }
}
