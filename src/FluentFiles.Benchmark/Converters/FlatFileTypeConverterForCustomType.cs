namespace FluentFiles.Benchmark.Converters
{
    using System;
    using System.Reflection;
    using FluentFiles.Benchmark.Entities;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    public class FlatFileConverterForCustomType : ValueConverterBase<CustomType>
    {
        protected override CustomType ConvertFrom(ReadOnlySpan<char> source, PropertyInfo targetProperty)
        {
            var obj = new CustomType();
            var values = source.Split('|').GetEnumerator();

            values.MoveNext();
            obj.First = int.Parse(values.Current);

            values.MoveNext();
            obj.Second = int.Parse(values.Current);

            values.MoveNext();
            obj.Third = int.Parse(values.Current);

            return obj;
        }

        protected override string ConvertTo(CustomType source, PropertyInfo sourceProperty)
        {
            return string.Format("{0}|{1}|{2}", source.First, source.Second, source.Third);
        }
    }
}