namespace FlatFile.Benchmark.Entities
{
    using FlatFile.FixedLength.Implementation;

    public class FixedSampleRecordLayout : FixedLayout<FixedSampleRecord>
    {
        protected override void MapLayout()
        {
            this.WithMember(x => x.Cuit, c => c.WithLenght(11))
                .WithMember(x => x.Nombre, c => c.WithLenght(160))
                .WithMember(x => x.Actividad, c => c.WithLenght(6));
        }
    }
}