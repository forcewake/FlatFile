namespace FluentFiles.Benchmark.Converters
{
    using CsvHelper;
    using CsvHelper.Configuration;
    using System.Reflection;
    using CsvHelperTypeConversion = CsvHelper.TypeConversion;

    public class CsvHelperTypeConverterForCustomType : CsvHelperTypeConversion.ITypeConverter
    {
        private readonly FlatFileTypeConverterForCustomType converter;

        public CsvHelperTypeConverterForCustomType()
        {
            converter = new FlatFileTypeConverterForCustomType();
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return converter.ConvertToString(value, (PropertyInfo)memberMapData.Member);
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return converter.ConvertFromString(text, (PropertyInfo)memberMapData.Member);
        }
    }
}
