namespace FluentFiles.Delimited.Implementation
{
    using System.Reflection;
    using FluentFiles.Core;

    public class DelimitedFieldSettingsFactory :
        IFieldSettingsFactory<IDelimitedFieldSettingsConstructor>
    {
        public IDelimitedFieldSettingsConstructor CreateFieldSettings(PropertyInfo property)
        {
            return new DelimitedFieldSettingsConstructor(property);
        }
    }
}