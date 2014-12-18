namespace FlatFile.Core
{
    using System.Reflection;
    using FlatFile.Core.Base;

    public interface IFieldSettingsFactory<TFieldSettings, out TBuilder>
        where TBuilder : IFieldSettingsConstructor<TFieldSettings, TBuilder>
        where TFieldSettings : FieldSettingsBase
    {
        TBuilder CreateFieldSettings(PropertyInfo property);
    }
}