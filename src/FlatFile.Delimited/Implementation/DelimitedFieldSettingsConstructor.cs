namespace FlatFile.Delimited.Implementation
{
    using System.Reflection;
    using FlatFile.Core.Base;

    public class DelimitedFieldSettingsConstructor :
        FieldSettingsConstructorBase<DelimitedFieldSettings, IDelimitedFieldSettingsConstructor>,
        IDelimitedFieldSettingsConstructor
    {
        public string Name { get; private set; }

        public DelimitedFieldSettingsConstructor(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public override IDelimitedFieldSettingsConstructor AllowNull(string nullValue)
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