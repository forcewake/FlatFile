namespace FlatFile.Tests.Base.Entities
{
    using System;

    public class TestObject : IEquatable<TestObject> {
        public int Id { get; set; }

        public string Description { get; set; }

        public int? NullableInt { get; set; }

        public int GetHashCode(TestObject obj) {
            var idHash = Id.GetHashCode();
            var descriptionHash = Object.ReferenceEquals(Description, null) ? 0 : Description.GetHashCode();
            var nullableIntHash = !NullableInt.HasValue ? 0 : NullableInt.Value.GetHashCode();
            return idHash ^ descriptionHash ^ nullableIntHash;
        }

        public bool Equals(TestObject other) {
            if (ReferenceEquals(other, null)) {
                return false;
            }

            if (ReferenceEquals(other, this)) {
                return true;
            }

            return Equals(Id, other.Id) && Equals(Description, other.Description) && Equals(NullableInt, other.NullableInt);
        }
    }
}