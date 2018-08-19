namespace FluentFiles.Benchmark.Converters
{
    using System;
    using System.Reflection;
    using FluentFiles.Benchmark.Entities;
    using FluentFiles.Core;

    public class FlatFileTypeConverterForCustomType : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            return type == typeof(string);
        }

        public bool CanConvertTo(Type type)
        {
            return type == typeof (CustomType);
        }

        public string ConvertToString(object source, PropertyInfo sourceProperty)
        {
            var obj = (CustomType)source;
            return string.Format("{0}|{1}|{2}", obj.First, obj.Second, obj.Third);
        }

        public object ConvertFromString(string source, PropertyInfo targetProperty)
        {
            var values = source.Split('|');

            var obj = new CustomType
            {
                First = int.Parse(values[0]),
                Second = int.Parse(values[1]),
                Third = int.Parse(values[2]),
            };
            return obj;
        }
    }
}