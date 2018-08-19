namespace FluentFiles.Benchmark.Mapping
{
    using FluentFiles.Benchmark.Entities;
    using FluentFiles.Delimited.Implementation;

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