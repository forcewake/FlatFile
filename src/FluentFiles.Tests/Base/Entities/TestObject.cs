namespace FluentFiles.Tests.Base.Entities
{
    using System;
    using FluentFiles.Delimited.Attributes;
    using FluentFiles.FixedLength;
    using FluentFiles.FixedLength.Attributes;

    [FixedLengthFile]
    [DelimitedFile(Delimiter = ";", Quotes = "\"")]
    public class TestObject : IEquatable<TestObject>
    {
        [FixedLengthField(1, 5, PaddingChar = '0')]
        [DelimitedField(1)]
        public int Id { get; set; }

        [FixedLengthField(2, 25, PaddingChar = ' ', Padding = Padding.Right)]
        [DelimitedField(2)]
        public string Description { get; set; }

        [FixedLengthField(2, 5, PaddingChar = '0', NullValue = "=Null")]
        [DelimitedField(3, NullValue = "=Null")]
        public int? NullableInt { get; set; }

        public int GetHashCode(TestObject obj)
        {
            var idHash = Id.GetHashCode();
            var descriptionHash = Object.ReferenceEquals(Description, null) ? 0 : Description.GetHashCode();
            var nullableIntHash = !NullableInt.HasValue ? 0 : NullableInt.Value.GetHashCode();
            return idHash ^ descriptionHash ^ nullableIntHash;
        }

        public bool Equals(TestObject other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            return Equals(Id, other.Id) && Equals(Description, other.Description) &&
                   Equals(NullableInt, other.NullableInt);
        }
    }
}