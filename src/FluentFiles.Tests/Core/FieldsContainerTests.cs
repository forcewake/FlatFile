namespace FluentFiles.Tests.Core
{
    using System.Linq;
    using System.Reflection;
    using FluentAssertions;
    using FluentFiles.Core.Base;
    using FluentFiles.Core.Extensions;
    using FluentFiles.FixedLength.Implementation;
    using FluentFiles.Tests.Base.Entities;
    using Xunit;

    public class FieldsContainerTests
    {
        private readonly FieldsContainer<FixedFieldSettings> _fieldsContainer;
        private readonly TestObject _testObject;
        private readonly PropertyInfo _propertyInfo;
        private readonly FixedFieldSettings _fieldSettings;
        private readonly PropertyInfo[] _properties;

        public FieldsContainerTests()
        {
            _fieldsContainer = new AutoOrderedFieldsContainer<FixedFieldSettings>();
            _testObject = new TestObject();

            _properties = new[]
            {
                ExpressionExtensions.GetPropertyInfo(() => _testObject.Id),
                ExpressionExtensions.GetPropertyInfo(() => _testObject.Description),
                ExpressionExtensions.GetPropertyInfo(() => _testObject.NullableInt)
            };

            _propertyInfo = ExpressionExtensions.GetPropertyInfo(() => _testObject.Description);

            _fieldSettings = new FixedFieldSettings(_propertyInfo);
        }

        [Fact]
        public void OrderedFieldsShouldContainsOneItemAfterAdd()
        {
            _fieldsContainer.AddOrUpdate(_fieldSettings);

            _fieldsContainer.OrderedFields.Should().HaveCount(1);
        }

        [Fact]
        public void AddOrUpdateShouldAssingRightId()
        {
            _fieldsContainer.AddOrUpdate(_fieldSettings);

            _fieldsContainer.OrderedFields.First().Index.Should().Be(0);
        }

        [Fact]
        public void OrderedFieldsShouldContainsOneItemAfterUpdate()
        {
            _fieldsContainer.AddOrUpdate(_fieldSettings);

            _fieldSettings.IsNullable = true;

            _fieldsContainer.AddOrUpdate(_fieldSettings);

            _fieldsContainer.OrderedFields.Should().HaveCount(1);
        }

        [Fact]
        public void AllAddedPropertyShouldBeInTheOrderedFields()
        {
            foreach (var property in _properties)
            {
                _fieldsContainer.AddOrUpdate(new FixedFieldSettings(property));
            }

            _fieldsContainer.OrderedFields.Should().HaveCount(_properties.Length);
        }

        [Fact]
        public void OrderedFieldsShouldContainsOrderedProperties()
        {
            foreach (var property in _properties)
            {
                _fieldsContainer.AddOrUpdate(new FixedFieldSettings(property));
            }

            int id = 0;

            foreach (var field in _fieldsContainer.OrderedFields)
            {
                field.Index.Should().Be(id++);
            }
        }
    }
}
