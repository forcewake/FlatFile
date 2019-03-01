namespace FluentFiles.Core.Conversion
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Defines a way to convert a field's string value to and from a custom type.
    /// </summary>
    public interface IFieldValueConverter : IFieldFormatter, IFieldParser
    {
    }

    /// <summary>
    /// Defines a way to convert a field's value to a string.
    /// </summary>
    public interface IFieldFormatter
    {
        /// <summary>
        /// Whether a value of a given type can be converted to a string.
        /// </summary>
        /// <param name="from">The type to convert from.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        bool CanFormat(Type from);

        /// <summary>
        /// Converts an object to a string.
        /// </summary>
        /// <param name="context">Provides information about a field formatting operation.</param>
        /// <returns>A formatted value.</returns>
        string Format(in FieldFormattingContext context);
    }

    /// <summary>
    /// Defines a way to convert a string to a field's target type.
    /// </summary>
    public interface IFieldParser
    {
        /// <summary>
        /// Whether a value of a given type can be converted from a string.
        /// </summary>
        /// <param name="to">The type to convert to.</param>
        /// <returns>Whether a type can be converted to a string.</returns>
        bool CanParse(Type to);

        /// <summary>
        /// Converts a string to an object instance.
        /// </summary>
        /// <param name="context">Provides information about a field parsing operation.</param>
        /// <returns>A parsed value.</returns>
        object Parse(in FieldParsingContext context);
    }

    /// <summary>
    /// Provides information about a field formatting operation.
    /// </summary>
    public readonly ref struct FieldFormattingContext
    {
        /// <summary>
        /// Initializes a new <see cref="FieldFormattingContext"/>.
        /// </summary>
        /// <param name="source">The object to format as a string.</param>
        /// <param name="sourceMember">The member the source value is from.</param>
        /// <param name="sourceType">The type of the member the source value is from.</param>
        public FieldFormattingContext(object source, MemberInfo sourceMember, Type sourceType)
        {
            Source = source;
            SourceMember = sourceMember;
            SourceType = sourceType;
        }

        /// <summary>
        /// The object to format as a string.
        /// </summary>
        public object Source { get; }

        /// <summary>
        /// The member the source value is from.
        /// </summary>
        public MemberInfo SourceMember { get; }

        /// <summary>
        /// The type of the member the source value is from.
        /// </summary>
        public Type SourceType { get; }
    }

    /// <summary>
    /// Provides information about a field parsing operation.
    /// </summary>
    public readonly ref struct FieldParsingContext
    {
        /// <summary>
        /// Initializes a new <see cref="FieldParsingContext"/>.
        /// </summary>
        /// <param name="source">The string to parse.</param>
        /// <param name="targetMember">The member the parsed source value should be assigned to.</param>
        /// <param name="targetType">The type of the member the parsed source value should be assigned to.</param>
        public FieldParsingContext(in ReadOnlySpan<char> source, MemberInfo targetMember, Type targetType)
        {
            Source = source;
            TargetMember = targetMember;
            TargetType = targetType;
        }

        /// <summary>
        /// The string to parse.
        /// </summary>
        public ReadOnlySpan<char> Source { get; }

        /// <summary>
        /// The member the parsed source value should be assigned to.
        /// </summary>
        public MemberInfo TargetMember { get; }

        /// <summary>
        /// The type of the member the parsed source value should be assigned to.
        /// </summary>
        public Type TargetType { get; }
    }
}