namespace FluentFiles.Delimited.Implementation
{
    using System;
    using System.Reflection;
    using FluentFiles.Core;
    using FluentFiles.Core.Extensions;

    public class DelimitedFieldSettingsConstructor : DelimitedFieldSettings, IDelimitedFieldSettingsConstructor
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

        public IDelimitedFieldSettingsConstructor WithTypeConverter<TConverter>() where TConverter : ITypeConverter, new()
        {
            return WithTypeConverter(ReflectionHelper.CreateInstance<TConverter>(true));
        }

        public IDelimitedFieldSettingsConstructor WithTypeConverter(ITypeConverter converter)
        {
            this.TypeConverter = converter ?? throw new ArgumentNullException(nameof(converter));
            return this;
        }

        public IDelimitedFieldSettingsConstructor WithConversionFromString<TProperty>(Func<string, TProperty> conversion)
        {
            if (TypeConverter == null)
                TypeConverter = new DelegatingTypeConverter<TProperty>();

            if (TypeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)TypeConverter).ConversionFromString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

            return this;
        }

        public IDelimitedFieldSettingsConstructor WithConversionToString<TProperty>(Func<TProperty, string> conversion)
        {
            if (TypeConverter == null)
                TypeConverter = new DelegatingTypeConverter<TProperty>();

            if (TypeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)TypeConverter).ConversionToString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

            return this;
        }

        public IDelimitedFieldSettingsConstructor WithName(string name)
        {
            Name = name;
            return this;
        }
    }
}