using FluentFiles.Core.Extensions;
using System;
using System.Reflection;

namespace FluentFiles.Core.Base
{
    class DefaultValueTypeConverter : ITypeConverter
    {
        public static ITypeConverter Instance = new DefaultValueTypeConverter();

        public bool CanConvertFrom(Type type) => type == typeof(string);

        public bool CanConvertTo(Type type) => false;

        public object ConvertFromString(string source, PropertyInfo targetProperty) => targetProperty.PropertyType.GetDefaultValue();

        public string ConvertToString(object source, PropertyInfo sourceProperty) => null;
    }
}
