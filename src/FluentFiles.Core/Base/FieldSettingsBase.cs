namespace FluentFiles.Core.Base
{
    using System.Reflection;

    public interface IFieldSettings
    {
        int? Index { get; set; }
        bool IsNullable { get; }
        string NullValue { get; }
        ITypeConverter TypeConverter { get; }
    }

    public interface IFieldSettingsContainer : IFieldSettings
    {
        PropertyInfo PropertyInfo { get; }
    }

    public abstract class FieldSettingsBase : IFieldSettingsContainer
    {
        protected FieldSettingsBase(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
        }

        protected FieldSettingsBase(IFieldSettings settings)
        {
            Index = settings.Index;
            IsNullable = settings.IsNullable;
            NullValue = settings.NullValue;
        }

        protected FieldSettingsBase(PropertyInfo propertyInfo, IFieldSettings settings) : this(settings)
        {
            PropertyInfo = propertyInfo;
        }

        public int? Index { get; set; }
        public bool IsNullable { get; set; }
        public string NullValue { get; set; }
        public ITypeConverter TypeConverter { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
    }

    public class PropertySettingsContainer<TPropertySettings> where TPropertySettings : IFieldSettings
    {
        public int Index { get; set; }
        public TPropertySettings PropertySettings { get; set; }
    }
}