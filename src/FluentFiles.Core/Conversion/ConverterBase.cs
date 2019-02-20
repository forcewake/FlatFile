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
        /// The type to convert to and from.
        /// </summary>
        protected Type TargetType { get; } = typeof(TValue);

        /// <summary>
        /// Whether a value of a given type can be converted from a string.
        /// </summary>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanParse(Type to) => to == TargetType;

        /// <summary>
        /// Whether a value of a given type can be converted to a string.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        public bool CanFormat(Type from) => from == TargetType;

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="context">Provides information about a field parsing operation.</param>
        /// <returns>A parsed value.</returns>
        public object Parse(in FieldParsingContext context) => ParseValue(context);

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="context">Provides information about a field formatting operation.</param>
        /// <returns>A formatted value.</returns>
        public string Format(in FieldFormattingContext context) => FormatValue(context);

        /// <summary>
        /// Converts a string to an instance of <typeparamref name="TValue"/>.
        /// </summary>
        /// <param name="context">Provides information about a field parsing operation.</param>
        /// <returns>A parsed value.</returns>
        protected abstract TValue ParseValue(in FieldParsingContext context);

        /// <summary>
        /// Converts an instance of <typeparamref name="TValue"/> to a string.
        /// </summary>
        /// <param name="context">Provides information about a field formatting operation.</param>
        /// <returns>A formatted value.</returns>
        protected abstract string FormatValue(in FieldFormattingContext<TValue> context);
    }

    /// <summary>
    /// Provides information about a field formatting operation.
    /// </summary>
    public readonly ref struct FieldFormattingContext<TValue>
    {
        /// <summary>
        /// Initializes a new <see cref="FieldFormattingContext"/>.
        /// </summary>
        /// <param name="source">The object to format as a string.</param>
        /// <param name="sourceProperty">The property the source value is from.</param>
        public FieldFormattingContext(TValue source, PropertyInfo sourceProperty)
        {
            Source = source;
            SourceProperty = sourceProperty;
        }

        /// <summary>
        /// The object to format as a string.
        /// </summary>
        public TValue Source { get; }

        /// <summary>
        /// The property the source value is from.
        /// </summary>
        public PropertyInfo SourceProperty { get; }

        /// <summary>
        /// Converts a <see cref="FieldFormattingContext"/> to a more specifically typed <see cref="FieldFormattingContext{TValue}"/>.
        /// </summary>
        /// <param name="context">The formatting context to convert.</param>
        public static implicit operator FieldFormattingContext<TValue>(FieldFormattingContext context)
        {
            return new FieldFormattingContext<TValue>((TValue)context.Source, context.SourceProperty);
        }
    }
}
