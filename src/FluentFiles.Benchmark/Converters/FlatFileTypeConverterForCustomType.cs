namespace FluentFiles.Benchmark.Converters
{
    using FluentFiles.Benchmark.Entities;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    public class FlatFileConverterForCustomType : ConverterBase<CustomType>
    {
        protected override CustomType ConvertFrom(in FieldDeserializationContext context)
        {
            var obj = new CustomType();
            var values = context.Source.Split('|').GetEnumerator();

            values.MoveNext();
            obj.First = int.Parse(values.Current);

            values.MoveNext();
            obj.Second = int.Parse(values.Current);

            values.MoveNext();
            obj.Third = int.Parse(values.Current);

            return obj;
        }

        protected override string ConvertTo(in FieldSerializationContext<CustomType> context)
        {
            return string.Format("{0}|{1}|{2}", context.Source.First, context.Source.Second, context.Source.Third);
        }
    }
}