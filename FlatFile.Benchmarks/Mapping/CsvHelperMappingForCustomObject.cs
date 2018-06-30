namespace FlatFile.Benchmarks.Mapping
{
    using CsvHelper.Configuration;
    using FlatFile.Benchmarks.Converters;
    using FlatFile.Benchmarks.Entities;

    public sealed class CsvHelperMappingForCustomObject : ClassMap<CustomObject>
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