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
                .WithMember(x => x.Id)
                .WithMember(x => x.FirstName)
                .WithMember(x => x.LastName)
                .WithMember(x => x.Activity, c => c.WithName("AnotherName"));
        }
    }
}