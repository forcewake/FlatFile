namespace FlatFile.Delimited.Implementation
{
    using System.Reflection;

    public class DelimitedFieldSettingsConstructor :
        DelimitedFieldSettings,
        IDelimitedFieldSettingsConstructor
    {
        public DelimitedFieldSettingsConstructor(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public IDelimitedFieldSettingsConstructor AllowNull(string nullValue)
        {
            this.IsNullable = true;
            this.NullValue = nullValue;
            return this;
        }

        public IDelimitedFieldSettingsConstructor WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}