namespace FlatFile.Benchmark.Entities
{
    using FlatFile.Delimited.Implementation;

    public class DelimitedSampleRecordLayout : DelimitedLayout<FixedSampleRecord>
    {
        protected override void MapLayout()
        {
            this.WithDelimiter(";")
                .WithQuote("\"")
                .WithMember(x => x.Cuit)
                .WithMember(x => x.Nombre)
                .WithMember(x => x.Actividad, c => c.WithName("AnotherName"));
        }
    }
}