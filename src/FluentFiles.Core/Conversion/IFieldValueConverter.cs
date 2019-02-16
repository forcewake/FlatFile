namespace FluentFiles.Core.Conversion
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines a way to convert a field's string value to and from a custom type.
    /// </summary>
    public interface IFieldValueConverter : IFieldSerializer, IFieldDeserializer
    {
        /// <summary>
        /// Whether a value of a given type can be converted to another type.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a conversion can be performed between the two types.</returns>
        bool CanConvert(Type from, Type to);
    }

    /// <summary>
    /// Defines a way to convert a field's value to a string.
    /// </summary>
    public interface IFieldSerializer
    {
        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A serialized value.</returns>
        string ConvertToString(in FieldSerializationContext context);
    }

    /// <summary>
    /// Defines a way to convert a string to a field's target type.
    /// </summary>
    public interface IFieldDeserializer
    {
        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="context">Provides information about a field deserialization operation.</param>
        /// <returns>A deserialized value.</returns>
        object ConvertFromString(in FieldDeserializationContext context);
    }

    /// <summary>
    /// Provides information about a field serialization operation.
    /// </summary>
    public readonly ref struct FieldSerializationContext
    {
        /// <summary>
        /// Initializes a new <see cref="FieldSerializationContext"/>.
        /// </summary>
        /// <param name="source">The object to serialize.</param>
        /// <param name="sourceProperty">The property the source value is from.</param>
        public FieldSerializationContext(object source, PropertyInfo sourceProperty)
        {
            Source = source;
            SourceProperty = sourceProperty;
        }

        /// <summary>
        /// The object to serialize.
        /// </summary>
        public object Source { get; }

        /// <summary>
        /// The property the source value is from.
        /// </summary>
        public PropertyInfo SourceProperty { get; }
    }

    /// <summary>
    /// Provides information about a field deserialization operation.
    /// </summary>
    public readonly ref struct FieldDeserializationContext
    {
        /// <summary>
        /// Initializes a new <see cref="FieldDeserializationContext"/>.
        /// </summary>
        /// <param name="source">The string to deserialize.</param>
        /// <param name="targetProperty">The property the deserialized source value should be assigned to.</param>
        public FieldDeserializationContext(in ReadOnlySpan<char> source, PropertyInfo targetProperty)
        {
            Source = source;
            TargetProperty = targetProperty;
        }

        /// <summary>
        /// The string to deserialize.
        /// </summary>
        public ReadOnlySpan<char> Source { get; }

        /// <summary>
        /// The property the deserialized source value should be assigned to.
        /// </summary>
        public PropertyInfo TargetProperty { get; }
    }
}