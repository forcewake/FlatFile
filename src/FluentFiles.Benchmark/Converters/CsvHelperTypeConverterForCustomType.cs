namespace FluentFiles.Benchmark.Converters
{
    using System;
    using CsvHelper;
    using CsvHelper.Configuration;
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
            return converter.Format(new FieldFormattingContext(value, memberMapData.Member, memberMapData.Member.MemberType()));
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return converter.Parse(new FieldParsingContext(text.AsSpan(), memberMapData.Member, memberMapData.Member.MemberType()));
        }
    }
}
