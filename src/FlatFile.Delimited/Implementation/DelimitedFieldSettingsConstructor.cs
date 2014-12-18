namespace FlatFile.Delimited.Implementation
{
    using System.Reflection;
    using FlatFile.Core.Base;

    public class DelimitedFieldSettingsConstructor :
        FieldSettingsConstructorBase<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor>,
        IDelimitedFieldSettingsConstructor
    {
        public DelimitedFieldSettingsConstructor(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public override IDelimitedFieldSettingsConstructor AllowNull(string nullValue)
        {
            this.IsNullable = true;
            this.NullValue = nullValue;
            return this;
        }
    }
}