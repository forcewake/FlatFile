namespace FlatFile.Core
{
    using System.Reflection;

    public interface IFieldSettingsConstructor<out TFieldSettings, out TConstructor>
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
    {
        bool IsNullable { get; }
        string NullValue { get; }
        PropertyInfo PropertyInfo { get; }

        TConstructor AllowNull(string nullValue);
    }
}