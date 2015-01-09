namespace FlatFile.Benchmark.Mapping
{
    using FlatFile.Benchmark.Entities;
    using FlatFile.Delimited.Implementation;

    public sealed class FlatFileMappingForCustomObject : DelimitedLayout<CustomObject>
    {
        public FlatFileMappingForCustomObject()
        {
            this.WithHeader()
                .WithDelimiter(",")
                .WithMember(m => m.StringColumn, c => c.WithName("String Column"))
                .WithMember(m => m.IntColumn, c => c.WithName("Int Column"))
                .WithMember(m => m.GuidColumn, c => c.WithName("Guid Column"));
        }
    }
}