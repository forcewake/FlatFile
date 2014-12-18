namespace FlatFile.Core.Base
{
    using System.Reflection;

    public abstract class FieldSettingsConstructorBase<TFieldSettings, TConstructor> :
        IFieldSettingsConstructor<TFieldSettings, TConstructor>
        where TFieldSettings : FieldSettingsBase
        where TConstructor : IFieldSettingsConstructor<TFieldSettings, TConstructor>
    {
        protected FieldSettingsConstructorBase(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
        }

        public bool IsNullable { get; protected set; }
        public string NullValue { get; protected set; }
        public PropertyInfo PropertyInfo { get; protected set; }
        public abstract TConstructor AllowNull(string nullValue);
    }
}