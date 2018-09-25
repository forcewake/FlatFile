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

        public Type Converter { get; set; }

        public IValueConverter TypeConverter
        {
            get
            {
                if (typeof(ITypeConverter).IsAssignableFrom(Converter))
                    return new ValueConverterAdapter((ITypeConverter)ReflectionHelper.CreateInstance(Converter, true));

                return (IValueConverter)ReflectionHelper.CreateInstance(Converter, true);
            }
        }

        protected FieldSettingsBaseAttribute(int index)
        {
            Index = index;
        }
    }
}