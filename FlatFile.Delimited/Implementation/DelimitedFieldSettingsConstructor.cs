namespace FlatFile.Delimited.Implementation
{
    using System.Reflection;
    using FlatFile.Core;
    using FlatFile.Core.Extensions;

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

        public IDelimitedFieldSettingsConstructor WithTypeConverter<TConverter>() where TConverter : ITypeConverter
        {
            this.TypeConverter = ReflectionHelper.CreateInstance<TConverter>(true);
            return this;
        }

        public IDelimitedFieldSettingsConstructor WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}