using System;
using System.Reflection;

namespace FluentFiles.Core.Conversion
{
    /// <summary>
    /// A generic base class for converting between strings and a given type.
    /// </summary>
    /// <typeparam name="TValue">The type to convert to and from a string.</typeparam>
    public abstract class ConverterBase<TValue> : IFieldValueConverter
    {
        public virtual bool CanConvert(Type from, Type to) => 
            (from == typeof(string) && to == typeof(TValue)) || 
            (from == typeof(TValue) && to == typeof(string));

        public object ConvertFromString(in FieldDeserializationContext context) =>
            ConvertFrom(context);

        public string ConvertToString(in FieldSerializationContext context) =>
            ConvertTo(new FieldSerializationContext<TValue>((TValue)context.Source, context.SourceProperty));

        protected abstract TValue ConvertFrom(in FieldDeserializationContext context);

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
