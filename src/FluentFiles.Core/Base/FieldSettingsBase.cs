namespace FluentFiles.Core.Base
{
    using FluentFiles.Core.Extensions;
    using System;
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
        private ITypeConverter _converter;

        protected readonly ITypeConverter DefaultConverter;

        protected FieldSettingsBase(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            DefaultConverter = propertyInfo.PropertyType.GetConverter();
        }

        protected FieldSettingsBase(PropertyInfo propertyInfo, IFieldSettings settings)
            : this(propertyInfo)
        {
            Index = settings.Index;
            IsNullable = settings.IsNullable;
            NullValue = settings.NullValue;
            TypeConverter = settings.TypeConverter;
        }

        public int? Index { get; set; }
        public bool IsNullable { get; set; }
        public string NullValue { get; set; }
        public ITypeConverter TypeConverter
        {
            get => _converter ?? DefaultConverter;
            set => _converter = value;
        }
        public PropertyInfo PropertyInfo { get; set; }
    }

    public class PropertySettingsContainer<TPropertySettings> where TPropertySettings : IFieldSettings
    {
        public int Index { get; set; }
        public TPropertySettings PropertySettings { get; set; }
    }
}