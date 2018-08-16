namespace FlatFile.Delimited.Implementation
{
    using System.Reflection;
    using FlatFile.Core;

    public class DelimitedFieldSettingsFactory :
        IFieldSettingsFactory<IDelimitedFieldSettingsConstructor>
    {
        public IDelimitedFieldSettingsConstructor CreateFieldSettings(PropertyInfo property)
        {
            return new DelimitedFieldSettingsConstructor(property);
        }
    }
}