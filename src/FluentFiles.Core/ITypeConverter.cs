namespace FluentFiles.Core
{
    using System;
    using System.Reflection;

    public interface ITypeConverter
    {
        bool CanConvertFrom(Type type);

        bool CanConvertTo(Type type);

        string ConvertToString(object source, PropertyInfo sourceProperty);

        object ConvertFromString(string source, PropertyInfo targetProperty);
    }
}