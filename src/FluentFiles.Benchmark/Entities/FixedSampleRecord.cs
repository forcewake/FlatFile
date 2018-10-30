namespace FluentFiles.Benchmark.Entities
{
    using System;
    using FileHelpers;

    /// <summary>
    /// Sample fixed length record for testing
    /// </summary>
    [FixedLengthRecord]
    public class FixedSampleRecord : IEquatable<FixedSampleRecord>
    {
        [FieldFixedLength(11)]
        private long _id;

        [FieldFixedLength(80)]
        [FieldTrim(TrimMode.Both)]
        private string _firstName;

        [FieldFixedLength(80)]
        [FieldTrim(TrimMode.Both)]
        private string _lastName;

        [FieldFixedLength(6)]
        private int _activity;

        public long Id
        {
            get => _id;
            set => _id = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public int Activity
        {
            get => _activity;
            set => _activity = value;
        }

        public bool Equals(FixedSampleRecord other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id == other._id && string.Equals(_firstName, other._firstName) && string.Equals(_lastName, other._lastName) && _activity == other._activity;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FixedSampleRecord)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = _id.GetHashCode();
                hashCode = (hashCode * 397) ^ (_firstName != null ? _firstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_lastName != null ? _lastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _activity;
                return hashCode;
            }
        }

        public static bool operator ==(FixedSampleRecord left, FixedSampleRecord right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FixedSampleRecord left, FixedSampleRecord right)
        {
            return !Equals(left, right);
        }
    }
}
