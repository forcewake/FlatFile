namespace FluentFiles.Core
{
    using System.Reflection;

    public interface IFieldSettingsFactory<out TBuilder>
        where TBuilder : IFieldSettingsConstructor<TBuilder>
    {
        TBuilder CreateFieldSettings(PropertyInfo property);
    }
}