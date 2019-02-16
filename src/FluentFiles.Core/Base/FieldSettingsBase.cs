namespace FluentFiles.Core.Base
{
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;
    using System;
    using System.Reflection;

    internal abstract class FieldSettingsBase : IFieldSettingsContainer
    {
        private IFieldValueConverter _converter;
        private Func<object, object> _getValue;
        private Action<object, object> _setValue;

        protected readonly IFieldValueConverter DefaultConverter;

        protected FieldSettingsBase(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            Type = PropertyInfo.PropertyType.Unwrap();
            UniqueKey = $"[{propertyInfo.DeclaringType.AssemblyQualifiedName}]:{propertyInfo.Name}";
            DefaultConverter = propertyInfo.PropertyType.GetConverter();
            _getValue = ReflectionHelper.CreatePropertyGetter(propertyInfo);
            _setValue = ReflectionHelper.CreatePropertySetter(propertyInfo);
        }

        protected FieldSettingsBase(PropertyInfo propertyInfo, IFieldSettings settings)
            : this(propertyInfo)
        {
            Index = settings.Index;
            IsNullable = settings.IsNullable;
            NullValue = settings.NullValue;
            Converter = settings.Converter;
        }

        public string UniqueKey { get; }
        public int? Index { get; set; }
        public bool IsNullable { get; set; }
        public string NullValue { get; set; }

        public IFieldValueConverter Converter
        {
            get => _converter ?? DefaultConverter;
            set => _converter = value;
        }

        public PropertyInfo PropertyInfo { get; }

        public Type Type { get; }

        public object GetValueOf(object instance)
        {
            if (_getValue != null)
                return _getValue(instance);
            else
                return PropertyInfo.GetValue(instance);
        }

        public void SetValueOf(object instance, object value)
        {
            if (_setValue != null)
                _setValue(instance, value);
            else
                PropertyInfo.SetValue(instance, value);
        }
    }
}