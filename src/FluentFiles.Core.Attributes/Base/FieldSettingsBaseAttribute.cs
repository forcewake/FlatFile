namespace FluentFiles.Core.Attributes.Base
{
    using System;
    using FluentFiles.Core.Base;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FieldSettingsBaseAttribute : Attribute, IFieldSettings
    {
        public int? Index { get; set; }

        public bool IsNullable
        {
            get { return !string.IsNullOrEmpty(NullValue); }
        }

        public string NullValue { get; set; }

        public Type ConverterType { get; set; }

        public IFieldValueConverter Converter
        {
            get
            {
                if (typeof(ITypeConverter).IsAssignableFrom(ConverterType))
                    return new FieldValueConverterAdapter((ITypeConverter)ReflectionHelper.CreateInstance(ConverterType, true));

                return (IFieldValueConverter)ReflectionHelper.CreateInstance(ConverterType, true);
            }
        }

        protected FieldSettingsBaseAttribute(int index)
        {
            Index = index;
        }
    }
}