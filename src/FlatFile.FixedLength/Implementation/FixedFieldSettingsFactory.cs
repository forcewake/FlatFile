namespace FlatFile.FixedLength.Implementation
{
    using System.Reflection;
    using FlatFile.Core;

    public class FixedFieldSettingsFactory : IFieldSettingsFactory<IFixedFieldSettingsConstructor>
    {
        public IFixedFieldSettingsConstructor CreateFieldSettings(PropertyInfo property)
        {
            return new FixedFieldSettingsConstructor(property);
        }
    }
}