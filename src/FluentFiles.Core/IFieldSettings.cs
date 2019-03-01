namespace FluentFiles.Core
{
    using System;
    using System.Reflection;
    using FluentFiles.Core.Conversion;

    /// <summary>
    /// A field mapping configuration.
    /// </summary>
    public interface IFieldSettings
    {
        /// <summary>
        /// An indicator of a field's ordered position.
        /// </summary>
        int? Index { get; set; }

        /// <summary>
        /// Whether a field may not have a value.
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        /// If <see cref="IsNullable"/> is true, the text that indicates the absence of a value.
        /// </summary>
        string NullValue { get; }

        /// <summary>
        /// Transforms a field's value to and from its textual file representation.
        /// </summary>
        IFieldValueConverter Converter { get; }
    }

    /// <summary>
    /// Extends <see cref="IFieldSettings"/> with functionality and data related to its storage in a class property.
    /// </summary>
    public interface IFieldSettingsContainer : IFieldSettings
    {
        /// <summary>
        /// The target type of a field.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the value of a field for a record instance.
        /// </summary>
        object GetValueOf(object instance);

        /// <summary>
        /// Sets the value of a field for a record instance.
        /// </summary>
        void SetValueOf(object instance, object value);

        /// <summary>
        /// The member underlying a field.
        /// </summary>
        MemberInfo Member { get; }

        /// <summary>
        /// A string that uniquely identifies a field within a layout.
        /// </summary>
        string UniqueKey { get; }
    }
}