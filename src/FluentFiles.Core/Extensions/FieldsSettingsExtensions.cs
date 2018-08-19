namespace FluentFiles.Core.Extensions
{
    using System;
    using FluentFiles.Core.Base;

    public static class FieldsSettingsExtensions
    {
        public static bool IsNullablePropertyType<TFieldSettings>(this TFieldSettings fieldSettings)
            where TFieldSettings : IFieldSettingsContainer
        {
            return fieldSettings.PropertyInfo.PropertyType.IsGenericType
                   && fieldSettings.PropertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}