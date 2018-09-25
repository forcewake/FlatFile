namespace FluentFiles.Benchmark.Mapping
{
    using FluentFiles.Benchmark.Converters;
    using FluentFiles.Benchmark.Entities;
    using FluentFiles.Delimited.Implementation;

    public sealed class FlatFileMappingForCustomObject : DelimitedLayout<CustomObject>
    {
        public FlatFileMappingForCustomObject()
        {
            this.WithHeader()
                .WithDelimiter(",")
                .WithMember(m => m.StringColumn, c => c.WithName("String Column"))
                .WithMember(m => m.IntColumn, c => c.WithName("Int Column"))
                .WithMember(m => m.GuidColumn, c => c.WithName("Guid Column"))
                .WithMember(m => m.CustomTypeColumn, c => c.WithName("Custom Type Column").WithConverter<FlatFileConverterForCustomType>());
        }
    }
}