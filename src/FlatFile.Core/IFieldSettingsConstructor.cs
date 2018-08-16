namespace FlatFile.Core
{
    using FlatFile.Core.Base;
    using System;

    public interface IFieldSettingsConstructor<out TConstructor> : IFieldSettingsContainer
        where TConstructor : IFieldSettingsConstructor<TConstructor>
    {
        TConstructor AllowNull(string nullValue);
        TConstructor WithTypeConverter<TConverter>() where TConverter : ITypeConverter;
        TConstructor WithConversionFromString<TProperty>(Func<string, TProperty> conversion);
        TConstructor WithConversionToString<TProperty>(Func<TProperty, string> conversion);
    }
}