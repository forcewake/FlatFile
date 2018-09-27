namespace FluentFiles.Core
{
    using FluentFiles.Core.Base;
    using System.Reflection;

    public interface IFieldSettingsBuilderFactory<out TBuilder, out TSettings>
        where TBuilder : IFieldSettingsBuilder<TBuilder, TSettings>, IBuildable<TSettings>
        where TSettings : IFieldSettings
    {
        TBuilder CreateBuilder<TTarget, TProperty>(PropertyInfo property);
    }
}