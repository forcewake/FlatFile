namespace FluentFiles.Tests.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
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
        private readonly MemberInfo _member;
        private readonly FixedFieldSettings _fieldSettings;
        private readonly MemberInfo[] _members;

        public FieldCollectionTests()
        {
            _fieldCollection = new AutoOrderedFieldsCollection<FixedFieldSettings>();
            _testObject = new TestObject();

            _members = new[]
            {
                MemberOf(_testObject, t => t.Id),
                MemberOf(_testObject, t => t.Description),
                MemberOf(_testObject, t => t.NullableInt)
            };

            _member = MemberOf(_testObject, t => t.Description);

            _fieldSettings = new FixedFieldSettings(_member);
        }

        private MemberInfo MemberOf<T, V>(T instance, Expression<Func<T, V>> memberAccess) => memberAccess.GetMemberInfo();

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
            foreach (var member in _members)
            {
                _fieldCollection.AddOrUpdate(new FixedFieldSettings(member));
            }

            _fieldCollection.Should().HaveCount(_members.Length);
        }

        [Fact]
        public void OrderedFieldsShouldContainsOrderedProperties()
        {
            foreach (var member in _members)
            {
                _fieldCollection.AddOrUpdate(new FixedFieldSettings(member));
            }

            int id = 0;

            foreach (var field in _fieldCollection)
            {
                field.Index.Should().Be(id++);
            }
        }
    }
}
