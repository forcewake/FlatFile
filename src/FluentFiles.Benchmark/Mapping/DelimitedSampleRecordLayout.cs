namespace FlatFile.Benchmark.Mapping
{
    using FlatFile.Benchmark.Entities;
    using FlatFile.Delimited.Implementation;

    public sealed class DelimitedSampleRecordLayout : DelimitedLayout<FixedSampleRecord>
    {
        public DelimitedSampleRecordLayout()
        {
            this.WithDelimiter(";")
                .WithQuote("\"")
                .WithMember(x => x.Cuit)
                .WithMember(x => x.Nombre)
                .WithMember(x => x.Actividad, c => c.WithName("AnotherName"));
        }
    }
}