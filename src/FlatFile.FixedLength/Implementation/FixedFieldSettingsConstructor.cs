using System;

namespace FlatFile.FixedLength.Implementation
{
    using System.Reflection;
    using FlatFile.Core;
    using FlatFile.Core.Extensions;

    public class FixedFieldSettingsConstructor : FixedFieldSettings,
        IFixedFieldSettingsConstructor
    {
        public FixedFieldSettingsConstructor(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public IFixedFieldSettingsConstructor TruncateFieldContentIfExceedLength()
        {
            TruncateIfExceedFieldLength = true;
            return this;
        }

        public IFixedFieldSettingsConstructor WithStringNormalizer(Func<string, string> stringNormalizer)
        {
            StringNormalizer = stringNormalizer;
            return this;
        }

        public IFixedFieldSettingsConstructor WithLength(int length)
        {
            Length = length;
            return this;
        }

        public IFixedFieldSettingsConstructor WithLeftPadding(char paddingChar)
        {
            PaddingChar = paddingChar;
            PadLeft = true;
            return this;
        }

        public IFixedFieldSettingsConstructor WithRightPadding(char paddingChar)
        {
            PaddingChar = paddingChar;
            PadLeft = false;
            return this;
        }

        public IFixedFieldSettingsConstructor AllowNull(string nullValue)
        {
            IsNullable = true;
            NullValue = nullValue;
            return this;
        }

        public IFixedFieldSettingsConstructor WithTypeConverter<TConverter>() where TConverter : ITypeConverter
        {
            this.TypeConverter = ReflectionHelper.CreateInstance<TConverter>(true);
            return this;
        }

        public IFixedFieldSettingsConstructor WithConversionFromString<TProperty>(Func<string, TProperty> conversion)
        {
            if (TypeConverter == null)
                TypeConverter = new DelegatingTypeConverter<TProperty>();

            if (TypeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)TypeConverter).ConversionFromString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

            return this;
        }

        public IFixedFieldSettingsConstructor WithConversionToString<TProperty>(Func<TProperty, string> conversion)
        {
            if (TypeConverter == null)
                TypeConverter = new DelegatingTypeConverter<TProperty>();

            if (TypeConverter is DelegatingTypeConverter<TProperty>)
                ((DelegatingTypeConverter<TProperty>)TypeConverter).ConversionToString = conversion;
            else
                throw new InvalidOperationException("A type converter has already been explicitly set.");

            return this;
        }
    }
}