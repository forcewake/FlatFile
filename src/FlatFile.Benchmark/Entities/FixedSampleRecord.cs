namespace FlatFile.Benchmark.Entities
{
    using System;
    using FileHelpers;

    /// <summary>
    /// Sample fixed length record for testing
    /// </summary>
    [FixedLengthRecord()]
    public class FixedSampleRecord : IEquatable<FixedSampleRecord>
    {
        [FieldFixedLength(11)]
        public long Cuit;

        [FieldFixedLength(160)]
        [FieldTrim(TrimMode.Both)]
        public string Nombre;

        [FieldFixedLength(6)]
        public int Actividad;

        public bool Equals(FixedSampleRecord other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Cuit == other.Cuit && string.Equals(Nombre, other.Nombre) && Actividad == other.Actividad;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FixedSampleRecord) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Cuit.GetHashCode();
                hashCode = (hashCode*397) ^ (Nombre != null ? Nombre.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Actividad;
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
