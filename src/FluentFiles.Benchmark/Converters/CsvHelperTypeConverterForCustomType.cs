namespace FluentFiles.Benchmark.Converters
{
    using System;
    using CsvHelper;
    using CsvHelper.Configuration;
    using System.Reflection;
    using CsvHelperTypeConversion = CsvHelper.TypeConversion;
    using FluentFiles.Core.Conversion;

    public class CsvHelperTypeConverterForCustomType : CsvHelperTypeConversion.ITypeConverter
    {
        private readonly FlatFileConverterForCustomType converter;

        public CsvHelperTypeConverterForCustomType()
        {
            converter = new FlatFileConverterForCustomType();
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return converter.Format(new FieldFormattingContext(value, (PropertyInfo)memberMapData.Member));
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return converter.Parse(new FieldParsingContext(text.AsSpan(), (PropertyInfo)memberMapData.Member));
        }
    }
}
