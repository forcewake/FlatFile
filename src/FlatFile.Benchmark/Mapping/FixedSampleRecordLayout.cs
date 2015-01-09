namespace FlatFile.Benchmark.Mapping
{
    using FlatFile.Benchmark.Entities;
    using FlatFile.FixedLength.Implementation;

    public sealed class FixedSampleRecordLayout : FixedLayout<FixedSampleRecord>
    {
        public FixedSampleRecordLayout()
        {
            this.WithMember(x => x.Cuit, c => c.WithLenght(11))
                .WithMember(x => x.Nombre, c => c.WithLenght(160))
                .WithMember(x => x.Actividad, c => c.WithLenght(6));
        }
    }
}