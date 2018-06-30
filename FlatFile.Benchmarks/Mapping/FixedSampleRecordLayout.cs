namespace FlatFile.Benchmarks.Mapping
{
    using FlatFile.Benchmarks.Entities;
    using FlatFile.FixedLength.Implementation;

    public sealed class FixedSampleRecordLayout : FixedLayout<FixedSampleRecord>
    {
        public FixedSampleRecordLayout()
        {
            this.WithMember(x => x.Cuit, c => c.WithLength(11))
                .WithMember(x => x.Nombre, c => c.WithLength(160))
                .WithMember(x => x.Actividad, c => c.WithLength(6));
        }
    }
}