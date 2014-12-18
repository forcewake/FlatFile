namespace FlatFile.Delimited.Implementation
{
    using FlatFile.Core;

    public class DelimitedFieldSettingsBuilder :
        IFieldSettingsBuilder<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor>
    {
        public DelimitedFieldSettings BuildSettings(IDelimitedFieldSettingsConstructor constructor)
        {
            return new DelimitedFieldSettings
            {
                IsNullable = constructor.IsNullable,
                NullValue = constructor.NullValue,
                PropertyInfo = constructor.PropertyInfo
            };
        }
    }
}