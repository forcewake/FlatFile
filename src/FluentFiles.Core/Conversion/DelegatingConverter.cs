namespace FluentFiles.Core.Conversion
{
    using System;

    /// <summary>
    /// An implementation of <see cref="IFieldValueConverter"/> that uses delegates for conversion.
    /// </summary>
    internal class DelegatingConverter<TMember> : IFieldValueConverter
    {
        internal FieldParser<TMember> ParseValue { get; set; }

        internal FieldFormatter<TMember> FormatValue { get; set; }

        public bool CanParse(Type to) => to == typeof(TMember) && ParseValue != null;

        public bool CanFormat(Type from) => from == typeof(TMember) && FormatValue != null;

        public object Parse(in FieldParsingContext context) => ParseValue(context.Source);

        public string Format(in FieldFormattingContext context) => FormatValue((TMember)context.Source);
    }
}
