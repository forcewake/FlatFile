namespace FluentFiles.FixedLength.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;

    public class FixedFieldSettingsFactory : IFieldSettingsFactory<IFixedFieldSettingsConstructor>
    {
        public IFixedFieldSettingsConstructor CreateFieldSettings(PropertyInfo property)
        {
            return new FixedFieldSettingsConstructor(property);
        }
    }
}