namespace FluentFiles.Benchmark.Entities
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
        private long _cuit;

        [FieldFixedLength(160)] 
        [FieldTrim(TrimMode.Both)] 
        private string _nombre;

        [FieldFixedLength(6)] 
        private int _actividad;

        public long Cuit
        {
            get { return _cuit; }
            set { _cuit = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int Actividad
        {
            get { return _actividad; }
            set { _actividad = value; }
        }

        public bool Equals(FixedSampleRecord other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _cuit == other._cuit && string.Equals(_nombre, other._nombre) && _actividad == other._actividad;
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
                int hashCode = _cuit.GetHashCode();
                hashCode = (hashCode*397) ^ (_nombre != null ? _nombre.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ _actividad;
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
