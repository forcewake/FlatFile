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

    public class FieldCollectionTests
    {
        private readonly FieldCollection<FixedFieldSettings> _fieldCollection;
        private readonly TestObject _testObject;
        private readonly PropertyInfo _propertyInfo;
        private readonly FixedFieldSettings _fieldSettings;
        private readonly PropertyInfo[] _properties;

        public FieldCollectionTests()
        {
            _fieldCollection = new AutoOrderedFieldsCollection<FixedFieldSettings>();
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
            _fieldCollection.AddOrUpdate(_fieldSettings);

            _fieldCollection.Should().HaveCount(1);
        }

        [Fact]
        public void AddOrUpdateShouldAssingRightId()
        {
            _fieldCollection.AddOrUpdate(_fieldSettings);

            _fieldCollection.First().Index.Should().Be(0);
        }

        [Fact]
        public void OrderedFieldsShouldContainsOneItemAfterUpdate()
        {
            _fieldCollection.AddOrUpdate(_fieldSettings);

            _fieldSettings.IsNullable = true;

            _fieldCollection.AddOrUpdate(_fieldSettings);

            _fieldCollection.Should().HaveCount(1);
        }

        [Fact]
        public void AllAddedPropertyShouldBeInTheOrderedFields()
        {
            foreach (var property in _properties)
            {
                _fieldCollection.AddOrUpdate(new FixedFieldSettings(property));
            }

            _fieldCollection.Should().HaveCount(_properties.Length);
        }

        [Fact]
        public void OrderedFieldsShouldContainsOrderedProperties()
        {
            foreach (var property in _properties)
            {
                _fieldCollection.AddOrUpdate(new FixedFieldSettings(property));
            }

            int id = 0;

            foreach (var field in _fieldCollection)
            {
                field.Index.Should().Be(id++);
            }
        }
    }
}
