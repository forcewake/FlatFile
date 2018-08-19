namespace FluentFiles.Benchmark.Mapping
{
    using CsvHelper.Configuration;
    using FluentFiles.Benchmark.Converters;
    using FluentFiles.Benchmark.Entities;

    public sealed class CsvHelperMappingForCustomObject : CsvClassMap<CustomObject>
    {
        public CsvHelperMappingForCustomObject()
        {
            Map(m => m.CustomTypeColumn).Name("Custom Type Column").Index(3).TypeConverter<CsvHelperTypeConverterForCustomType>();
            Map(m => m.GuidColumn).Name("Guid Column").Index(2);
            Map(m => m.IntColumn).Name("Int Column").Index(1);
            Map(m => m.StringColumn).Name("String Column").Index(0);
        }
    }
}