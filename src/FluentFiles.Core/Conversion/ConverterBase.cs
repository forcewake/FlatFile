namespace FluentFiles.Core.Conversion
{
    using System;
    using System.Reflection;

    /// <summary>
    /// A generic base class for converting between strings and a given type.
    /// </summary>
    /// <typeparam name="TValue">The type to convert to and from a string.</typeparam>
    public abstract class ConverterBase<TValue> : IFieldValueConverter
    {
        /// <summary>
        /// Whether a value of a given type can be converted to another type.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a conversion can be performed between the two types.</returns>
        public virtual bool CanConvert(Type from, Type to) => 
            (from == typeof(string) && to == typeof(TValue)) || 
            (from == typeof(TValue) && to == typeof(string));

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="context">Provides information about a field deserialization operation.</param>
        /// <returns>A deserialized value.</returns>
        public object ConvertFromString(in FieldDeserializationContext context) =>
            ConvertFrom(context);

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A serialized value.</returns>
        public string ConvertToString(in FieldSerializationContext context) =>
            ConvertTo(new FieldSerializationContext<TValue>((TValue)context.Source, context.SourceProperty));

        /// <summary>
        /// Converts a string to an instance of <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="context">Provides information about a field deserialization operation.</param>
        /// <returns>A deserialized value.</returns>
        protected abstract TValue ConvertFrom(in FieldDeserializationContext context);

        /// <summary>
        /// Converts an instance of <typeparamref name="TValue"/> to a string.
        /// </summary>
        /// <param name="context">Provides information about a field serialization operation.</param>
        /// <returns>A serialized value.</returns>
        protected abstract string ConvertTo(in FieldSerializationContext<TValue> context);
    }

    /// <summary>
    /// Provides information about a field serialization operation.
    /// </summary>
    public readonly ref struct FieldSerializationContext<TValue>
    {
        /// <summary>
        /// Initializes a new <see cref="FieldSerializationContext"/>.
        /// </summary>
        /// <param name="source">The object to serialize.</param>
        /// <param name="sourceProperty">The property the source value is from.</param>
        public FieldSerializationContext(TValue source, PropertyInfo sourceProperty)
        {
            Source = source;
            SourceProperty = sourceProperty;
        }

        /// <summary>
        /// The object to serialize.
        /// </summary>
        public TValue Source { get; }

        /// <summary>
        /// The property the source value is from.
        /// </summary>
        public PropertyInfo SourceProperty { get; }
    }
}
