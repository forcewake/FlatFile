namespace FluentFiles.Core
{
    using System;

    public interface ITypeConverter
    {
        bool CanConvertFrom(Type type);

        bool CanConvertTo(Type type);

        string ConvertToString(object source);

        object ConvertFromString(string source);
    }
}