namespace FlatFile.FixedLength.Implementation
{
    using FlatFile.Core;

    public class FixedFieldSettingsBuilder : IFieldSettingsBuilder<FixedFieldSettings, IFixedFieldSettingsConstructor>
    {
        public FixedFieldSettings BuildSettings(IFixedFieldSettingsConstructor constructor)
        {
            return new FixedFieldSettings
            {
                IsNullable = constructor.IsNullable,
                Lenght = constructor.Lenght,
                NullValue = constructor.NullValue,
                PaddingChar = constructor.PaddingChar,
                PadLeft = constructor.PadLeft,
                PropertyInfo = constructor.PropertyInfo
            };
        }
    }
}