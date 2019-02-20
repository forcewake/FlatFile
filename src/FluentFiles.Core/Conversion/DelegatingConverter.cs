namespace FluentFiles.Core.Conversion
{
    using System;

    /// <summary>
    /// An implementation of <see cref="IFieldValueConverter"/> that uses delegates for conversion.
    /// </summary>
    internal class DelegatingConverter<TProperty> : IFieldValueConverter
    {
        internal FieldParser<TProperty> ParseValue { get; set; }

        internal FieldFormatter<TProperty> FormatValue { get; set; }

        public bool CanParse(Type to) => to == typeof(TProperty) && ParseValue != null;

        public bool CanFormat(Type from) => from == typeof(TProperty) && FormatValue != null;

        public object Parse(in FieldParsingContext context) => ParseValue(context.Source);

        public string Format(in FieldFormattingContext context) => FormatValue((TProperty)context.Source);
    }
}
