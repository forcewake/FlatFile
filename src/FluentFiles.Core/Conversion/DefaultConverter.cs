namespace FluentFiles.Core.Conversion
{
    using FluentFiles.Core.Extensions;
    using System;

    /// <summary>
    /// The last fallback for all field conversions.
    /// </summary>
    internal class DefaultConverter : IFieldValueConverter
    {
        public static IFieldValueConverter Instance = new DefaultConverter();

        public bool CanParse(Type to) => true;

        public bool CanFormat(Type from) => true;

        public object Parse(in FieldParsingContext context) => context.TargetType.GetDefaultValue();

        public string Format(in FieldFormattingContext context) => context.Source.ToString();
    }
}
