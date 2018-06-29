namespace FlatFile.Benchmark.Converters
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelper.TypeConversion;

    public class CsvHelperTypeConverterForCustomType : ITypeConverter
    {
        private readonly FlatFileTypeConverterForCustomType converter;

        public CsvHelperTypeConverterForCustomType()
        {
            converter = new FlatFileTypeConverterForCustomType();
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return converter.ConvertToString(value);
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return converter.ConvertFromString(text);
        }
    }
}
