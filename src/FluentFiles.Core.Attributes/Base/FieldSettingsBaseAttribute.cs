namespace FluentFiles.Core.Attributes.Base
{
    using System;
    using FluentFiles.Core.Conversion;
    using FluentFiles.Core.Extensions;

    /// <summary>
    /// Base for field attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class FieldSettingsBaseAttribute : Attribute, IFieldSettings
    {
        /// <summary>
        /// Where in a line a field appears.
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Whether a field may not have a value.
        /// </summary>
        public bool IsNullable
        {
            get { return !string.IsNullOrEmpty(NullValue); }
        }

        /// <summary>
        /// If <see cref="IFieldSettings.IsNullable" /> is true, the text that indicates the absence of a value.
        /// </summary>
        public string NullValue { get; set; }

        /// <summary>
        /// A type of converter to use when transforming a field's value into its target type.
        /// </summary>
        public Type Converter { get; set; }

#pragma warning disable CS0618 // Type or member is obsolete
        IFieldValueConverter IFieldSettings.Converter
        {
            get
            {
                if (typeof(ITypeConverter).IsAssignableFrom(Converter))
                    return new ITypeConverterAdapter((ITypeConverter)ReflectionHelper.CreateInstance(Converter, true));

                return (IFieldValueConverter)ReflectionHelper.CreateInstance(Converter, true);
            }
        }
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// Initializes a new <see cref="FieldSettingsBaseAttribute"/>.
        /// </summary>
        /// <param name="index">Where in a line a field appears.</param>
        protected FieldSettingsBaseAttribute(int index)
        {
            Index = index;
        }
    }
}